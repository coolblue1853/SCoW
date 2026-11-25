using System.Collections.Generic;
using SCoW.Core.Story;

namespace SCoW.Core.Keyword
{
    /// <summary>
    /// NPC 별 키워드 질문 횟수 표(KEYWORD-1 K7). 옛 Intel_ObtoUI.ResetKeyCount 의 NPC 이름 11분기를 JSON 으로 옮겼다
    /// (Resources/Story/KeywordBudget.json). 표에 없는 NPC 는 횟수를 바꾸지 않는다. 순수 C#.
    /// </summary>
    public sealed class KeywordBudget
    {
        private readonly Dictionary<string, int> _budgets = new Dictionary<string, int>();

        /// <summary>실패하면 null 을 돌려주고 errors 에 이유를 담는다.</summary>
        public static KeywordBudget Load(string json, List<string> errors)
        {
            string err;
            object root = MiniJson.Parse(json, out err);
            if (err != null)
            {
                errors.Add(err);
                return null;
            }
            Dictionary<string, object> o = root as Dictionary<string, object>;
            object raw;
            Dictionary<string, object> map = null;
            if (o != null && o.TryGetValue("budgets", out raw))
            {
                map = raw as Dictionary<string, object>;
            }
            if (map == null)
            {
                errors.Add("budgets 객체 없음");
                return null;
            }
            KeywordBudget b = new KeywordBudget();
            foreach (KeyValuePair<string, object> kv in map)
            {
                if (!(kv.Value is double) || (double)kv.Value < 0 || (double)kv.Value != System.Math.Floor((double)kv.Value))
                {
                    errors.Add("횟수가 0 이상의 정수가 아님: " + kv.Key);
                    continue;
                }
                b._budgets[kv.Key] = (int)(double)kv.Value;
            }
            return errors.Count == 0 ? b : null;
        }

        public bool TryGet(string npc, out int count)
        {
            return _budgets.TryGetValue(npc ?? string.Empty, out count);
        }

        public int Count
        {
            get { return _budgets.Count; }
        }
    }
}
