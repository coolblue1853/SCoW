using System.Collections.Generic;

namespace SCoW.Core.Story
{
    public sealed class StoryLoadResult
    {
        /// <summary>오류가 하나라도 있으면 null. Ok 를 먼저 확인한다.</summary>
        public StoryScript Script;
        public List<string> Errors = new List<string>();
        public bool Ok { get { return Errors.Count == 0; } }
    }

    /// <summary>
    /// StoryScript JSON 로더. 구조:
    /// { "id", "topics":[{"id","cases":[{"when","pre","node"}]}],
    ///   "nodes":{ "<id>":{"speaker","text","sprite","onEnter","choices":[{"text","when","next"}],"next","check","onSuccess","onFail","onCritFail"}} }
    /// when/pre/onEnter 는 조건·효과 DSL 문자열. 노드 참조가 없는 id 를 가리키면 오류. 예외 대신 Errors.
    /// </summary>
    public static class StoryScriptLoader
    {
        public static StoryLoadResult Load(string json)
        {
            StoryLoadResult res = new StoryLoadResult();
            string err;
            object root = MiniJson.Parse(json, out err);
            if (err != null) { res.Errors.Add(err); return res; }
            Dictionary<string, object> o = root as Dictionary<string, object>;
            if (o == null) { res.Errors.Add("최상위가 객체가 아님"); return res; }

            StoryScript sc = new StoryScript();
            sc.Id = Str(o, "id");
            List<object> topics = Get<List<object>>(o, "topics");
            if (topics != null)
            {
                for (int i = 0; i < topics.Count; i++) { LoadTopic(topics[i], i, sc, res.Errors); }
            }
            Dictionary<string, object> nodes = Get<Dictionary<string, object>>(o, "nodes");
            if (nodes != null)
            {
                foreach (KeyValuePair<string, object> kv in nodes) { LoadNode(kv.Key, kv.Value, sc, res.Errors); }
            }
            CheckRefs(sc, res.Errors);
            // 오류가 하나라도 있으면 불완전한 모델을 내보내지 않는다(실패한 조건이 참으로 보이는 일을 막는 계약).
            res.Script = res.Errors.Count == 0 ? sc : null;
            return res;
        }

        private static T Get<T>(Dictionary<string, object> o, string key) where T : class
        {
            object v;
            return o.TryGetValue(key, out v) ? v as T : null;
        }

        private static string Str(Dictionary<string, object> o, string key)
        {
            object v;
            string s = o.TryGetValue(key, out v) ? v as string : null;
            return s ?? "";
        }

        private static Condition Cond(Dictionary<string, object> o, string key, string where, List<string> errors)
        {
            ParseOutcome<Condition> r = ConditionParser.Parse(Str(o, key));
            if (!r.Ok) { errors.Add(where + " " + key + ": " + r.Error); return new ConstCond(false); }
            return r.Value;
        }

        private static Effect[] Eff(Dictionary<string, object> o, string key, string where, List<string> errors)
        {
            ParseOutcome<Effect[]> r = EffectParser.Parse(Str(o, key));
            if (!r.Ok) { errors.Add(where + " " + key + ": " + r.Error); return new Effect[0]; }
            return r.Value;
        }

        private static void LoadTopic(object raw, int idx, StoryScript sc, List<string> errors)
        {
            Dictionary<string, object> t = raw as Dictionary<string, object>;
            if (t == null) { errors.Add("topics[" + idx + "] 가 객체가 아님"); return; }
            StoryTopic topic = new StoryTopic();
            topic.Id = Str(t, "id");
            string w = "topic '" + topic.Id + "'";
            if (topic.Id.Length == 0) { errors.Add("topics[" + idx + "] id 없음"); }
            List<object> cases = Get<List<object>>(t, "cases");
            if (cases != null)
            {
                for (int i = 0; i < cases.Count; i++)
                {
                    Dictionary<string, object> c = cases[i] as Dictionary<string, object>;
                    if (c == null) { errors.Add(w + " cases[" + i + "] 가 객체가 아님"); continue; }
                    string cw = w + " case " + i;
                    StoryCase sCase = new StoryCase();
                    sCase.When = Cond(c, "when", cw, errors);
                    sCase.Pre = Eff(c, "pre", cw, errors);
                    sCase.NodeId = Str(c, "node");
                    topic.Cases.Add(sCase);
                }
            }
            sc.Topics.Add(topic);
        }

        private static void LoadNode(string id, object raw, StoryScript sc, List<string> errors)
        {
            Dictionary<string, object> n = raw as Dictionary<string, object>;
            string w = "node '" + id + "'";
            if (n == null) { errors.Add(w + " 가 객체가 아님"); return; }
            StoryNode node = new StoryNode();
            node.Id = id;
            node.Speaker = Str(n, "speaker");
            node.TextKey = Str(n, "text");
            node.Sprite = Str(n, "sprite");
            node.OnEnter = Eff(n, "onEnter", w, errors);
            node.Next = Str(n, "next");
            node.CheckId = Str(n, "check");
            node.OnSuccess = Str(n, "onSuccess");
            node.OnFail = Str(n, "onFail");
            node.OnCritSuccess = Str(n, "onCritSuccess");
            node.OnCritFail = Str(n, "onCritFail");
            List<object> choices = Get<List<object>>(n, "choices");
            if (choices != null)
            {
                List<StoryChoice> list = new List<StoryChoice>();
                for (int i = 0; i < choices.Count; i++)
                {
                    Dictionary<string, object> c = choices[i] as Dictionary<string, object>;
                    if (c == null) { errors.Add(w + " choices[" + i + "] 가 객체가 아님"); continue; }
                    StoryChoice ch = new StoryChoice();
                    ch.TextKey = Str(c, "text");
                    ch.When = Cond(c, "when", w + " choice " + i, errors);
                    ch.Next = Str(c, "next");
                    list.Add(ch);
                }
                node.Choices = list.ToArray();
            }
            sc.Nodes[id] = node;
        }

        private static void CheckRefs(StoryScript sc, List<string> errors)
        {
            foreach (StoryTopic t in sc.Topics)
            {
                foreach (StoryCase c in t.Cases)
                {
                    Ref(sc, c.NodeId, "topic '" + t.Id + "' case", true, errors);
                }
            }
            foreach (StoryNode n in sc.Nodes.Values)
            {
                string w = "node '" + n.Id + "'";
                Ref(sc, n.Next, w + " next", false, errors);
                RefNodeOrTopic(sc, n.OnSuccess, w + " onSuccess", errors);
                RefNodeOrTopic(sc, n.OnFail, w + " onFail", errors);
                RefNodeOrTopic(sc, n.OnCritSuccess, w + " onCritSuccess", errors);
                RefNodeOrTopic(sc, n.OnCritFail, w + " onCritFail", errors);
                foreach (StoryChoice ch in n.Choices) { Ref(sc, ch.Next, w + " choice next", true, errors); }
            }
        }

        private static void RefNodeOrTopic(StoryScript sc, string id, string where, List<string> errors)
        {
            if (string.IsNullOrEmpty(id)) { return; }
            if (!sc.Nodes.ContainsKey(id) && !sc.HasTopic(id)) { errors.Add(where + ": 없는 노드·토픽 '" + id + "'"); }
        }

        private static void Ref(StoryScript sc, string id, string where, bool required, List<string> errors)
        {
            if (string.IsNullOrEmpty(id))
            {
                if (required) { errors.Add(where + ": 노드 id 없음"); }
                return;
            }
            if (!sc.Nodes.ContainsKey(id)) { errors.Add(where + ": 없는 노드 '" + id + "'"); }
        }
    }
}
