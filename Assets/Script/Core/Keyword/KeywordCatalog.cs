using System.Collections.Generic;
using SCoW.Core.Story;

namespace SCoW.Core.Keyword
{
    /// <summary>키워드 분류. 옛 DataBaseManager.keywordMain 문자열("Event"/"Character"/"Place")과 이름이 같다.</summary>
    public enum KeywordCategory
    {
        Event,
        Character,
        Place
    }

    /// <summary>상위 키워드(옛 3단 선택의 Upper). 켜진 unlockFlags 가 하나라도 있으면 목록에 나온다.</summary>
    public sealed class KeywordUpperDef
    {
        public string Id = "";
        public KeywordCategory Category;
        public string[] UnlockFlags = new string[0];
        public string DisplayKey = "";
        public int Order;
        /// <summary>비어 있지 않으면 이 상위에 관한 "의심" 사유(기록만, 동작은 안 바꿈).</summary>
        public string Suspect = "";
    }

    /// <summary>하위 키워드(옛 Downer). Id 는 Keyword.json 토픽 "NPC/Id" 와 일치하므로 절대 바꾸지 않는다.</summary>
    public sealed class KeywordEntry
    {
        public string Id = "";
        public KeywordCategory Category;
        public string Upper = "";
        public string UnlockFlag = "";
        public string DisplayKey = "";
        public int Order;
        public string Suspect = "";
    }

    /// <summary>Keyword.json 토픽 중 내용이 의심스러운 것(기록용). 고치는 일은 스토리 P0 작업 몫.</summary>
    public sealed class KeywordSuspectTopic
    {
        public string Topic = "";
        public string Reason = "";
    }

    public sealed class KeywordCatalogLoadResult
    {
        /// <summary>오류가 하나라도 있으면 null. Ok 를 먼저 확인한다.</summary>
        public KeywordCatalog Catalog;
        public List<string> Errors = new List<string>();
        public bool Ok { get { return Errors.Count == 0; } }
    }

    /// <summary>
    /// 키워드 카탈로그(Resources/Story/KeywordCatalog.json). 순수 데이터. 구조:
    /// { "version", "uppers":[{id,category,unlockFlags[],displayKey,order,suspect}],
    ///   "entries":[{id,category,upper,unlockFlag,displayKey,order,suspect}], "suspectTopics":[{topic,reason}] }
    /// 오류가 있으면 Catalog 는 null(불완전한 목록을 내보내지 않는다).
    /// </summary>
    public sealed class KeywordCatalog
    {
        public int Version = 1;
        public List<KeywordUpperDef> Uppers = new List<KeywordUpperDef>();
        public List<KeywordEntry> Entries = new List<KeywordEntry>();
        public List<KeywordSuspectTopic> SuspectTopics = new List<KeywordSuspectTopic>();

        public static KeywordCatalogLoadResult Load(string json)
        {
            KeywordCatalogLoadResult res = new KeywordCatalogLoadResult();
            string err;
            object root = MiniJson.Parse(json, out err);
            if (err != null) { res.Errors.Add(err); return res; }
            Dictionary<string, object> o = root as Dictionary<string, object>;
            if (o == null) { res.Errors.Add("최상위가 객체가 아님"); return res; }

            KeywordCatalog cat = new KeywordCatalog();
            cat.Version = (int)Num(o, "version");
            List<object> uppers = Arr(o, "uppers");
            List<object> entries = Arr(o, "entries");
            if (uppers == null) { res.Errors.Add("uppers 배열 없음"); }
            if (entries == null) { res.Errors.Add("entries 배열 없음"); }
            if (res.Errors.Count > 0) { return res; }

            for (int i = 0; i < uppers.Count; i++) { LoadUpper(uppers[i], i, cat, res.Errors); }
            for (int i = 0; i < entries.Count; i++) { LoadEntry(entries[i], i, cat, res.Errors); }
            List<object> suspects = Arr(o, "suspectTopics");
            if (suspects != null)
            {
                for (int i = 0; i < suspects.Count; i++)
                {
                    Dictionary<string, object> s = suspects[i] as Dictionary<string, object>;
                    if (s == null) { res.Errors.Add("suspectTopics[" + i + "] 가 객체가 아님"); continue; }
                    cat.SuspectTopics.Add(new KeywordSuspectTopic { Topic = Str(s, "topic"), Reason = Str(s, "reason") });
                }
            }
            CheckRefs(cat, res.Errors);
            res.Catalog = res.Errors.Count == 0 ? cat : null;
            return res;
        }

        private static void LoadUpper(object raw, int idx, KeywordCatalog cat, List<string> errors)
        {
            Dictionary<string, object> o = raw as Dictionary<string, object>;
            if (o == null) { errors.Add("uppers[" + idx + "] 가 객체가 아님"); return; }
            KeywordUpperDef u = new KeywordUpperDef();
            u.Id = Str(o, "id");
            if (u.Id.Length == 0) { errors.Add("uppers[" + idx + "] id 없음"); }
            if (!ParseCategory(Str(o, "category"), out u.Category)) { errors.Add("upper '" + u.Id + "' 분류 오류"); }
            u.DisplayKey = Str(o, "displayKey");
            u.Order = (int)Num(o, "order");
            u.Suspect = Str(o, "suspect");
            List<object> flags = Arr(o, "unlockFlags");
            if (flags == null || flags.Count == 0)
            {
                errors.Add("upper '" + u.Id + "' unlockFlags 없음");
            }
            else
            {
                u.UnlockFlags = new string[flags.Count];
                for (int i = 0; i < flags.Count; i++) { u.UnlockFlags[i] = flags[i] as string ?? ""; }
            }
            cat.Uppers.Add(u);
        }

        private static void LoadEntry(object raw, int idx, KeywordCatalog cat, List<string> errors)
        {
            Dictionary<string, object> o = raw as Dictionary<string, object>;
            if (o == null) { errors.Add("entries[" + idx + "] 가 객체가 아님"); return; }
            KeywordEntry e = new KeywordEntry();
            e.Id = Str(o, "id");
            e.Upper = Str(o, "upper");
            e.UnlockFlag = Str(o, "unlockFlag");
            e.DisplayKey = Str(o, "displayKey");
            e.Order = (int)Num(o, "order");
            e.Suspect = Str(o, "suspect");
            if (e.Id.Length == 0) { errors.Add("entries[" + idx + "] id 없음"); }
            if (e.UnlockFlag.Length == 0) { errors.Add("entry '" + e.Id + "' unlockFlag 없음"); }
            if (!ParseCategory(Str(o, "category"), out e.Category)) { errors.Add("entry '" + e.Id + "' 분류 오류"); }
            cat.Entries.Add(e);
        }

        private static void CheckRefs(KeywordCatalog cat, List<string> errors)
        {
            HashSet<string> upperIds = new HashSet<string>();
            foreach (KeywordUpperDef u in cat.Uppers)
            {
                if (!upperIds.Add(u.Id)) { errors.Add("upper 중복: " + u.Id); }
            }
            HashSet<string> seen = new HashSet<string>();
            foreach (KeywordEntry e in cat.Entries)
            {
                if (!upperIds.Contains(e.Upper)) { errors.Add("entry '" + e.Id + "' 의 상위 '" + e.Upper + "' 가 uppers 에 없음"); }
                if (!seen.Add(e.Upper + "/" + e.Id)) { errors.Add("entry 중복: " + e.Upper + "/" + e.Id); }
            }
        }

        public static bool ParseCategory(string s, out KeywordCategory c)
        {
            switch (s)
            {
                case "Event": c = KeywordCategory.Event; return true;
                case "Character": c = KeywordCategory.Character; return true;
                case "Place": c = KeywordCategory.Place; return true;
            }
            c = KeywordCategory.Event;
            return false;
        }

        private static List<object> Arr(Dictionary<string, object> o, string key)
        {
            object v;
            return o.TryGetValue(key, out v) ? v as List<object> : null;
        }

        private static string Str(Dictionary<string, object> o, string key)
        {
            object v;
            return o.TryGetValue(key, out v) ? (v as string ?? "") : "";
        }

        private static double Num(Dictionary<string, object> o, string key)
        {
            object v;
            return o.TryGetValue(key, out v) && v is double ? (double)v : 0d;
        }
    }
}
