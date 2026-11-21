using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SCoW.Core.Keyword;
using SCoW.Core.State;
using SCoW.Core.Story;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 키워드 동치 골든 도구(KEYWORD-1 K1). 플레이 모드에서 옛 KeyWordManager 를 임시 오브젝트로 만들어, 플래그 프리셋마다
/// KeywordUpdate·DownKeywordUpdater 를 한 번 돌린 뒤 상위·하위 목록을 한 줄로 덤프한다(KeywordBook 이 같은 목록을 내는지 EditMode 테스트가 대조).
/// 줄 형식: 이름|프리셋|U.Event=a,b|U.Character=..|U.Place=..|D.<상위id>=하위,하위 ... (상위는 카탈로그 order 순)
/// 프리셋: 없음, 전부, 플래그 하나씩, 상위별 전체, 코드 순서 누적. 옛 목록이 매 프레임 갱신되는 점은 같은 프레임에 모든 플래그가 켜진 것으로 본다.
/// </summary>
public static class KeywordGoldenTool
{
    public const string GoldenPath = "Assets/Tests/EditMode/Golden/KeywordBook.golden.txt";

    [MenuItem("SCoW/Keyword/Dump Golden (Play Mode)")]
    public static void DumpGolden()
    {
        if (!EditorApplication.isPlaying || GameRoot.Instance == null)
        {
            Debug.LogError("KeywordGoldenTool: 플레이 모드(Main 씬)에서 실행해야 함");
            return;
        }
        KeywordCatalogExporter.BuildResult build = KeywordCatalogExporter.Build(
            File.ReadAllText(KeywordCatalogExporter.SourcePath), KeywordCatalogExporter.LoadKeywordScript());
        KeywordCatalog cat = build.Catalog;
        List<KeywordUpperDef> uppers = new List<KeywordUpperDef>(cat.Uppers);
        uppers.Sort((a, b) => a.Order.CompareTo(b.Order));

        List<string> allFlags = new List<string>();
        for (int i = 0; i < uppers.Count; i++) { allFlags.AddRange(uppers[i].UnlockFlags); }

        List<string[]> presets = new List<string[]>();
        presets.Add(new[] { "none", "" });
        presets.Add(new[] { "all", Spec(allFlags) });
        for (int i = 0; i < allFlags.Count; i++) { presets.Add(new[] { "only:" + allFlags[i], Spec(new List<string> { allFlags[i] }) }); }
        for (int i = 0; i < uppers.Count; i++) { presets.Add(new[] { "upper:" + uppers[i].Id, Spec(new List<string>(uppers[i].UnlockFlags)) }); }
        for (int k = 1; k < allFlags.Count; k++) { presets.Add(new[] { "cum:" + k, Spec(allFlags.GetRange(0, k)) }); }

        GameState saved = GameRoot.Instance.State.Clone();
        long savedPlay = GameRoot.Instance.PlayTimeSec;
        StringBuilder sb = new StringBuilder();
        try
        {
            for (int p = 0; p < presets.Count; p++)
            {
                GameRoot.Instance.LoadState(StatePreset.Build(presets[p][1]), 0);
                sb.Append(presets[p][0]).Append('|').Append(presets[p][1]).Append('|');
                AppendOldManager(sb, build, uppers);
                sb.Append('\n');
            }
        }
        finally
        {
            GameRoot.Instance.LoadState(saved, savedPlay);
        }
        File.WriteAllText(GoldenPath, sb.ToString(), new UTF8Encoding(false));
        AssetDatabase.ImportAsset(GoldenPath);
        Debug.Log("KeywordGoldenTool: " + presets.Count + "개 프리셋 -> " + GoldenPath);
    }

    private static string Spec(List<string> flags)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < flags.Count; i++) { sb.Append(i > 0 ? ";" : "").Append("flag:").Append(flags[i]).Append("=1"); }
        return sb.ToString();
    }

    private static void AppendOldManager(StringBuilder sb, KeywordCatalogExporter.BuildResult build, List<KeywordUpperDef> uppers)
    {
        GameObject go = new GameObject("KeywordGoldenTemp");
        go.hideFlags = HideFlags.HideAndDontSave;
        try
        {
            KeyWordManager m = go.AddComponent<KeyWordManager>();
            m.mainSetting = NewText(go.transform);
            m.upperSetting = NewText(go.transform);
            m.downerSetting = NewText(go.transform);
            m.DownKeywordUpdater();
            m.KeywordUpdate();
            sb.Append("U.Event=").Append(Join(m.upperEventKeywordList)).Append("|U.Character=").Append(Join(m.upperCharacterKewordList));
            sb.Append("|U.Place=").Append(Join(m.upperPlaceKeywordList));
            for (int i = 0; i < uppers.Count; i++)
            {
                string field = FieldFor(build, uppers[i].Id);
                List<string> list = field == null ? new List<string>() : (List<string>)typeof(KeyWordManager).GetField(field).GetValue(m);
                sb.Append("|D.").Append(uppers[i].Id).Append('=').Append(Join(list));
            }
        }
        finally
        {
            UnityEngine.Object.DestroyImmediate(go);
        }
    }

    private static Text NewText(Transform parent)
    {
        GameObject t = new GameObject("t");
        t.transform.SetParent(parent, false);
        return t.AddComponent<Text>();
    }

    /// <summary>상위 id 에 해당하는 옛 하위 목록 필드명(하위가 하나도 없는 상위는 null).</summary>
    private static string FieldFor(KeywordCatalogExporter.BuildResult build, string upperId)
    {
        foreach (KeyValuePair<string, string> kv in build.ListToUpper)
        {
            if (kv.Value == upperId) { return kv.Key; }
        }
        return null;
    }

    private static string Join(List<string> list)
    {
        return string.Join(",", list);
    }
}
