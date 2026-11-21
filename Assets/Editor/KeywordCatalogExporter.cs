using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using SCoW.Core.Keyword;
using SCoW.Core.Story;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 키워드 카탈로그 생성 도구(KEYWORD-1 K1). 옛 KeyWordManager.cs 의 KeywordUpdate(상위)·DownKeywordUpdater(하위) 소스를 읽어
/// Resources/Story/KeywordCatalog.json 을 만든다(손으로 옮기면 오타가 나므로 소스에서 뽑는다). 옛 코드는 고치지 않는다.
/// 메뉴: SCoW/Keyword/Export Catalog. 결과가 바뀌면 KeywordBookTests 의 골든(SCoW/Keyword/Dump Golden)도 다시 뜬다.
/// "의심" 표시: 옛 3단 선택이 상위 이름을 다르게 비교하는 곳, Keyword.json 어디에도 전용 토픽이 없는 키워드, 스토리 문서가 지적한 범위 오류 토픽.
/// </summary>
public static class KeywordCatalogExporter
{
    public const string SourcePath = "Assets/Script/BillowUI/KeyWordManager.cs";
    public const string KeywordJsonPath = "Assets/Resources/Story/Keyword.json";
    public const string CatalogPath = "Assets/Resources/Story/KeywordCatalog.json";

    /// <summary>생성 결과. ListToUpper 는 옛 하위 목록 필드명(예 planetaryParadeList) → 상위 id(골든 덤프용).</summary>
    public sealed class BuildResult
    {
        public KeywordCatalog Catalog = new KeywordCatalog();
        public Dictionary<string, string> ListToUpper = new Dictionary<string, string>();
        public Dictionary<string, string> UpperToField = new Dictionary<string, string>();
        /// <summary>하위 키워드 → 옛 목록 필드명(불일치 의심 표시를 항목 단위로 좁히는 데 쓴다).</summary>
        public Dictionary<KeywordEntry, string> EntryToList = new Dictionary<KeywordEntry, string>();
        public List<string> Warnings = new List<string>();
    }

    private static readonly Regex FlagRegex = new Regex(@"DataBaseManager\.(intel\w+)");
    private static readonly Regex UpperAddRegex = new Regex(@"(upper(Event|Character|Place)Ke\w*List)\.Add\(""([^""]*)""\)");
    private static readonly Regex EntryRegex = new Regex(
        @"DataBaseManager\.(intel\w+) == true && \w+ == false\)\s*\{\s*\w+ = true;\s*(?://[^\n]*\n\s*)?(\w+)\.Add\(""([^""]*)""\);");
    private static readonly Regex LegacyCheckRegex = new Regex(
        @"keywordUpper == ""([^""]*)"" && \w+ >= 0\)\s*\{\s*DataBaseManager\.keywordDowner = (\w+)\[");
    private static readonly Regex TrailingDigits = new Regex(@"\d+$");

    [MenuItem("SCoW/Keyword/Export Catalog")]
    public static void Export()
    {
        string source = File.ReadAllText(SourcePath);
        StoryScript keyword = LoadKeywordScript();
        BuildResult r = Build(source, keyword);
        string json = ToJson(r.Catalog);
        File.WriteAllText(CatalogPath, json, new UTF8Encoding(false));
        AssetDatabase.ImportAsset(CatalogPath);
        for (int i = 0; i < r.Warnings.Count; i++) { Debug.LogWarning("KeywordCatalogExporter: " + r.Warnings[i]); }
        Debug.Log("KeywordCatalogExporter: 상위 " + r.Catalog.Uppers.Count + "개, 하위 " + r.Catalog.Entries.Count + "개, 의심 토픽 "
            + r.Catalog.SuspectTopics.Count + "개 -> " + CatalogPath);
    }

    public static StoryScript LoadKeywordScript()
    {
        StoryLoadResult lr = StoryScriptLoader.Load(File.ReadAllText(KeywordJsonPath));
        if (!lr.Ok) { throw new InvalidOperationException("Keyword.json 로드 실패: " + string.Join(" | ", lr.Errors)); }
        return lr.Script;
    }

    /// <summary>KeyWordManager 소스 텍스트와 Keyword.json 스크립트로 카탈로그를 만든다(파일 입출력 없음).</summary>
    public static BuildResult Build(string source, StoryScript keyword)
    {
        BuildResult r = new BuildResult();
        int downStart = source.IndexOf("public void DownKeywordUpdater()", StringComparison.Ordinal);
        int upStart = source.IndexOf("public void KeywordUpdate()", StringComparison.Ordinal);
        if (downStart < 0 || upStart < 0) { throw new InvalidOperationException("KeyWordManager 소스에서 갱신 메서드를 못 찾음"); }
        string upBody = source.Substring(upStart, downStart - upStart);
        string downBody = source.Substring(downStart);

        Dictionary<string, KeywordUpperDef> byBase = ParseUppers(upBody, r);
        Dictionary<string, string> legacyCheck = new Dictionary<string, string>();
        foreach (Match m in LegacyCheckRegex.Matches(downBody)) { legacyCheck[m.Groups[2].Value] = m.Groups[1].Value; }

        int order = 0;
        foreach (Match m in EntryRegex.Matches(downBody))
        {
            string flag = m.Groups[1].Value;
            string list = m.Groups[2].Value;
            string id = m.Groups[3].Value;
            string baseName = TrailingDigits.Replace(flag, "");
            KeywordUpperDef upper;
            if (!byBase.TryGetValue(baseName, out upper))
            {
                r.Warnings.Add("하위 키워드 '" + id + "' 의 플래그 " + flag + " 가 어느 상위에도 속하지 않음");
                continue;
            }
            KeywordEntry e = new KeywordEntry { Id = id, Category = upper.Category, Upper = upper.Id, UnlockFlag = flag, Order = order++ };
            e.DisplayKey = "Keyword/" + Slug(upper.Id, "u" + upper.Order) + "/" + Slug(id, "e" + e.Order);
            r.Catalog.Entries.Add(e);
            r.EntryToList[e] = list;
            r.ListToUpper[list] = upper.Id;
        }
        MarkLegacyMismatch(r, legacyCheck);
        MarkNoTopic(r, keyword);
        AddSuspectTopics(r, keyword);
        return r;
    }

    private static Dictionary<string, KeywordUpperDef> ParseUppers(string body, BuildResult r)
    {
        Dictionary<string, KeywordUpperDef> byBase = new Dictionary<string, KeywordUpperDef>();
        string[] lines = body.Split('\n');
        List<string> flags = new List<string>();
        int order = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (line.Contains("if (DataBaseManager.intel"))
            {
                flags.Clear();
                foreach (Match fm in FlagRegex.Matches(line)) { if (!flags.Contains(fm.Groups[1].Value)) { flags.Add(fm.Groups[1].Value); } }
                continue;
            }
            Match am = UpperAddRegex.Match(line);
            if (!am.Success || line.TrimStart().StartsWith("//", StringComparison.Ordinal)) { continue; }
            KeywordCategory cat;
            KeywordCatalog.ParseCategory(am.Groups[2].Value, out cat);
            flags.Sort(StringComparer.Ordinal);
            KeywordUpperDef u = new KeywordUpperDef { Id = am.Groups[3].Value, Category = cat, UnlockFlags = flags.ToArray(), Order = order++ };
            u.DisplayKey = "Keyword/Upper/" + Slug(u.Id, "u" + u.Order);
            r.Catalog.Uppers.Add(u);
            r.UpperToField[u.Id] = am.Groups[1].Value;
            byBase[TrailingDigits.Replace(flags[0], "")] = u;
        }
        return byBase;
    }

    private static void MarkLegacyMismatch(BuildResult r, Dictionary<string, string> legacyCheck)
    {
        foreach (KeyValuePair<string, string> kv in legacyCheck)
        {
            string upperId;
            if (!r.ListToUpper.TryGetValue(kv.Key, out upperId) || upperId == kv.Value) { continue; }
            string why = "옛 3단 선택이 하위를 keywordUpper == \"" + kv.Value + "\" 로 고르는데 상위 이름은 \"" + upperId
                + "\" 라 옛 UI 로는 이 하위를 선택할 수 없음(옛 코드 불일치, 고치지 않음)";
            foreach (KeywordEntry e in r.Catalog.Entries)
            {
                string entryList;
                if (r.EntryToList.TryGetValue(e, out entryList) && entryList == kv.Key) { e.Suspect = why; }
            }
        }
    }

    private static void MarkNoTopic(BuildResult r, StoryScript keyword)
    {
        if (keyword == null) { return; }
        HashSet<string> ids = new HashSet<string>();
        for (int i = 0; i < keyword.Topics.Count; i++)
        {
            string id = keyword.Topics[i].Id;
            int slash = id.IndexOf('/');
            if (slash > 0) { ids.Add(id.Substring(slash + 1)); }
        }
        foreach (KeywordEntry e in r.Catalog.Entries)
        {
            if (ids.Contains(e.Id)) { continue; }
            string why = "Keyword.json 어느 NPC 에도 전용 토픽이 없어 어디서든 폴백 응답";
            e.Suspect = e.Suspect.Length == 0 ? why : e.Suspect + "; " + why;
        }
    }

    private static void AddSuspectTopics(BuildResult r, StoryScript keyword)
    {
        string a1 = "StoryImprove A-1: 노드가 가리키는 CSV 범위가 실제 대사와 다름(대학생 노드가 케인 대사를 가리킴 / 케인 점액 범위 오류). 스토리 P0 에서 수정, 카탈로그는 기록만";
        string[] topics =
        {
            "University student/Content of a Nightmare", "University student/Water and Nightmare", "University student/Rumor",
            "University student/Source speculation", "University student/Sewer & FishySmell", "University student/Cause of Fishy smell",
            "University student/Details", "Kane/Strange Point", "Kane/Fishy Slime"
        };
        for (int i = 0; i < topics.Length; i++)
        {
            if (keyword != null && !keyword.HasTopic(topics[i]))
            {
                r.Warnings.Add("의심 토픽 목록의 " + topics[i] + " 가 Keyword.json 에 없음(건너뜀)");
                continue;
            }
            r.Catalog.SuspectTopics.Add(new KeywordSuspectTopic { Topic = topics[i], Reason = a1 });
        }
    }

    private static string Slug(string s, string fallback)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')) { sb.Append(c); }
            else if (sb.Length > 0 && sb[sb.Length - 1] != '_') { sb.Append('_'); }
        }
        string slug = sb.ToString().Trim('_');
        return slug.Length == 0 ? fallback : slug;
    }

    public static string ToJson(KeywordCatalog c)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{\n  \"version\": ").Append(c.Version).Append(",\n  \"uppers\": [\n");
        for (int i = 0; i < c.Uppers.Count; i++)
        {
            KeywordUpperDef u = c.Uppers[i];
            sb.Append("    { \"id\": ").Append(Q(u.Id)).Append(", \"category\": ").Append(Q(u.Category.ToString()));
            sb.Append(", \"unlockFlags\": [");
            for (int f = 0; f < u.UnlockFlags.Length; f++) { sb.Append(f > 0 ? ", " : "").Append(Q(u.UnlockFlags[f])); }
            sb.Append("], \"displayKey\": ").Append(Q(u.DisplayKey)).Append(", \"order\": ").Append(u.Order);
            if (u.Suspect.Length > 0) { sb.Append(", \"suspect\": ").Append(Q(u.Suspect)); }
            sb.Append(" }").Append(i < c.Uppers.Count - 1 ? ",\n" : "\n");
        }
        sb.Append("  ],\n  \"entries\": [\n");
        for (int i = 0; i < c.Entries.Count; i++)
        {
            KeywordEntry e = c.Entries[i];
            sb.Append("    { \"id\": ").Append(Q(e.Id)).Append(", \"category\": ").Append(Q(e.Category.ToString()));
            sb.Append(", \"upper\": ").Append(Q(e.Upper)).Append(", \"unlockFlag\": ").Append(Q(e.UnlockFlag));
            sb.Append(", \"displayKey\": ").Append(Q(e.DisplayKey)).Append(", \"order\": ").Append(e.Order);
            if (e.Suspect.Length > 0) { sb.Append(", \"suspect\": ").Append(Q(e.Suspect)); }
            sb.Append(" }").Append(i < c.Entries.Count - 1 ? ",\n" : "\n");
        }
        sb.Append("  ],\n  \"suspectTopics\": [\n");
        for (int i = 0; i < c.SuspectTopics.Count; i++)
        {
            sb.Append("    { \"topic\": ").Append(Q(c.SuspectTopics[i].Topic)).Append(", \"reason\": ").Append(Q(c.SuspectTopics[i].Reason));
            sb.Append(" }").Append(i < c.SuspectTopics.Count - 1 ? ",\n" : "\n");
        }
        sb.Append("  ]\n}\n");
        return sb.ToString();
    }

    private static string Q(string s)
    {
        StringBuilder sb = new StringBuilder("\"");
        for (int i = 0; i < s.Length; i++)
        {
            char ch = s[i];
            if (ch == '"') { sb.Append("\\\""); }
            else if (ch == '\\') { sb.Append("\\\\"); }
            else if (ch == '\n') { sb.Append("\\n"); }
            else if (ch < ' ') { sb.Append("\\u").Append(((int)ch).ToString("x4")); }
            else { sb.Append(ch); }
        }
        return sb.Append('"').ToString();
    }
}
