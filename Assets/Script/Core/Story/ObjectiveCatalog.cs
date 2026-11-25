using System.Collections.Generic;

namespace SCoW.Core.Story
{
    /// <summary>현재 목표 한 줄의 규칙 하나. 규칙 순서가 우선순위다(앞이 먼저).</summary>
    public sealed class ObjectiveRule
    {
        public string Id = "";
        public Condition When = Condition.Always;
        public string Ko = "";
        public string En = "";
        /// <summary>true 면 다른 규칙이 하나도 맞지 않을 때만 쓴다.</summary>
        public bool Fallback;
        /// <summary>true 면 이 규칙이 맞는 순간 뒤 규칙은 보지 않는다(마감 임박 등).</summary>
        public bool Exclusive;
    }

    public sealed class ObjectiveCatalogLoadResult
    {
        /// <summary>오류가 하나라도 있으면 null.</summary>
        public ObjectiveCatalog Catalog;
        public List<string> Errors = new List<string>();
        public bool Ok { get { return Errors.Count == 0; } }
    }

    /// <summary>
    /// 목표 선택 규칙(UX-3, 순수 C#). Resources/Story/objectives.json 구조:
    /// { "version":1, "rules":[ { "id", "when":"조건 DSL", "ko", "en", "fallback":bool, "exclusive":bool } ] }.
    /// 힌트 수준(Q-UX3 임시 가정): "아직 안 간 곳" 정도만, 정답 단서는 말하지 않는다. 조건은 ConditionParser 를 그대로 쓴다.
    /// </summary>
    public sealed class ObjectiveCatalog
    {
        public readonly List<ObjectiveRule> Rules = new List<ObjectiveRule>();

        public static ObjectiveCatalogLoadResult Load(string json)
        {
            ObjectiveCatalogLoadResult res = new ObjectiveCatalogLoadResult();
            string err;
            object root = MiniJson.Parse(json, out err);
            if (err != null) { res.Errors.Add(err); return res; }
            Dictionary<string, object> o = root as Dictionary<string, object>;
            object rawRules;
            List<object> list = o != null && o.TryGetValue("rules", out rawRules) ? rawRules as List<object> : null;
            if (list == null) { res.Errors.Add("rules 배열 없음"); return res; }

            ObjectiveCatalog cat = new ObjectiveCatalog();
            HashSet<string> ids = new HashSet<string>();
            for (int i = 0; i < list.Count; i++)
            {
                Dictionary<string, object> r = list[i] as Dictionary<string, object>;
                if (r == null) { res.Errors.Add("rules[" + i + "] 가 객체가 아님"); continue; }
                ObjectiveRule rule = new ObjectiveRule();
                rule.Id = Str(r, "id");
                rule.Ko = Str(r, "ko");
                rule.En = Str(r, "en");
                rule.Fallback = Bool(r, "fallback");
                rule.Exclusive = Bool(r, "exclusive");
                if (rule.Id.Length == 0) { res.Errors.Add("rules[" + i + "] id 없음"); }
                else if (!ids.Add(rule.Id)) { res.Errors.Add("rule 중복: " + rule.Id); }
                if (rule.En.Length == 0) { res.Errors.Add("rule '" + rule.Id + "' en 없음"); }
                ParseOutcome<Condition> cond = ConditionParser.Parse(Str(r, "when"));
                if (!cond.Ok) { res.Errors.Add("rule '" + rule.Id + "' 조건 오류: " + cond.Error); }
                else { rule.When = cond.Value; }
                cat.Rules.Add(rule);
            }
            res.Catalog = res.Errors.Count == 0 ? cat : null;
            return res;
        }

        /// <summary>지금 맞는 규칙 전부(우선순위 순). Exclusive 가 맞으면 거기서 끊는다. 하나도 없으면 Fallback 규칙들.</summary>
        public List<ObjectiveRule> Active(IStateReader s)
        {
            List<ObjectiveRule> hit = new List<ObjectiveRule>();
            for (int i = 0; i < Rules.Count; i++)
            {
                ObjectiveRule r = Rules[i];
                if (r.Fallback || !r.When.Eval(s)) { continue; }
                hit.Add(r);
                if (r.Exclusive) { return hit; }
            }
            if (hit.Count == 0)
            {
                for (int i = 0; i < Rules.Count; i++)
                {
                    if (Rules[i].Fallback && Rules[i].When.Eval(s)) { hit.Add(Rules[i]); }
                }
            }
            return hit;
        }

        /// <summary>HUD 한 줄에 쓸 가장 우선인 목표. 없으면 null.</summary>
        public ObjectiveRule Current(IStateReader s)
        {
            List<ObjectiveRule> a = Active(s);
            return a.Count > 0 ? a[0] : null;
        }

        private static string Str(Dictionary<string, object> o, string key)
        {
            object v;
            return o.TryGetValue(key, out v) ? (v as string ?? "") : "";
        }

        private static bool Bool(Dictionary<string, object> o, string key)
        {
            object v;
            return o.TryGetValue(key, out v) && v is bool && (bool)v;
        }
    }
}
