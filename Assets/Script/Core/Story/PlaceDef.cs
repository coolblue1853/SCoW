using System.Collections.Generic;

namespace SCoW.Core.Story
{
    public sealed class PlaceScriptDef
    {
        public Condition When = Condition.Always;
        /// <summary>StoryScript id. 대사 본문 CSV 의 이름도 같다(장소 도착 때 DialogDatabaseManager 가 이 CSV 를 읽는다).</summary>
        public string ScriptId = "";
    }

    /// <summary>장소 하나의 데이터. Id 는 옛 nowPlace 문자열 그대로(PlaceIds). Scripts 는 위에서부터 첫 번째로 맞는 항목을 쓴다.</summary>
    public sealed class PlaceDef
    {
        public string Id = "";
        public List<PlaceScriptDef> Scripts = new List<PlaceScriptDef>();
    }

    public sealed class PlaceCatalog
    {
        public readonly Dictionary<string, PlaceDef> Places = new Dictionary<string, PlaceDef>();
        public readonly List<string> Errors = new List<string>();
        public bool Ok { get { return Errors.Count == 0; } }

        /// <summary>장소에서 지금 쓸 ScriptId. 장소가 없거나 맞는 항목이 없으면 null.</summary>
        public string ScriptFor(string placeId, IStateReader s)
        {
            PlaceDef p;
            if (placeId == null || !Places.TryGetValue(placeId, out p)) { return null; }
            for (int i = 0; i < p.Scripts.Count; i++)
            {
                if (p.Scripts[i].When.Eval(s)) { return p.Scripts[i].ScriptId; }
            }
            return null;
        }

        /// <summary>{ "places":[{ "id", "scripts":[{ "when", "script" }] }] }. 오류는 Errors(예외 없음).</summary>
        public static PlaceCatalog Load(string json)
        {
            PlaceCatalog cat = new PlaceCatalog();
            string err;
            object root = MiniJson.Parse(json, out err);
            if (err != null) { cat.Errors.Add(err); return cat; }
            Dictionary<string, object> o = root as Dictionary<string, object>;
            List<object> list = o != null && o.ContainsKey("places") ? o["places"] as List<object> : null;
            if (list == null) { cat.Errors.Add("places 배열 없음"); return cat; }
            for (int i = 0; i < list.Count; i++) { LoadPlace(list[i], i, cat); }
            return cat;
        }

        private static void LoadPlace(object raw, int idx, PlaceCatalog cat)
        {
            Dictionary<string, object> p = raw as Dictionary<string, object>;
            if (p == null) { cat.Errors.Add("places[" + idx + "] 가 객체가 아님"); return; }
            PlaceDef def = new PlaceDef();
            def.Id = Str(p, "id");
            if (def.Id.Length == 0) { cat.Errors.Add("places[" + idx + "] id 없음"); return; }
            List<object> scripts = p.ContainsKey("scripts") ? p["scripts"] as List<object> : null;
            if (scripts != null)
            {
                for (int i = 0; i < scripts.Count; i++)
                {
                    Dictionary<string, object> s = scripts[i] as Dictionary<string, object>;
                    if (s == null) { cat.Errors.Add("place '" + def.Id + "' scripts[" + i + "] 가 객체가 아님"); continue; }
                    PlaceScriptDef sd = new PlaceScriptDef();
                    sd.ScriptId = Str(s, "script");
                    if (sd.ScriptId.Length == 0) { cat.Errors.Add("place '" + def.Id + "' scripts[" + i + "] script 없음"); continue; }
                    ParseOutcome<Condition> cond = ConditionParser.Parse(Str(s, "when"));
                    if (!cond.Ok) { cat.Errors.Add("place '" + def.Id + "' when: " + cond.Error); continue; }
                    sd.When = cond.Value;
                    def.Scripts.Add(sd);
                }
            }
            cat.Places[def.Id] = def;
        }

        private static string Str(Dictionary<string, object> o, string key)
        {
            object v;
            string s = o.TryGetValue(key, out v) ? v as string : null;
            return s ?? "";
        }
    }
}
