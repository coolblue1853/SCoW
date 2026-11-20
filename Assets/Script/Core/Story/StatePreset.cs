using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SCoW.Core.State;

namespace SCoW.Core.Story
{
    /// <summary>
    /// 동치 도구용 상태 프리셋. 새 게임 기본값(GameState.New) 위에 `flag:이름=1|0`, `counter:이름=정수` 를 `;` 로 이어 얹는다.
    /// 빈 문자열은 기본값 그대로.
    /// </summary>
    public static class StatePreset
    {
        public static GameState Build(string spec)
        {
            GameState s = GameState.New();
            if (string.IsNullOrWhiteSpace(spec)) { return s; }
            string[] parts = spec.Split(';');
            for (int i = 0; i < parts.Length; i++)
            {
                string p = parts[i].Trim();
                if (p.Length == 0) { continue; }
                int eq = p.IndexOf('=');
                if (eq <= 0) { throw new FormatException("프리셋 항목 형식 오류: " + p); }
                string key = p.Substring(0, eq);
                string val = p.Substring(eq + 1);
                if (key.StartsWith("flag:", StringComparison.Ordinal))
                {
                    s.SetFlag(key.Substring(5), val == "1");
                }
                else if (key.StartsWith("counter:", StringComparison.Ordinal))
                {
                    s.SetCounter(key.Substring(8), int.Parse(val, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture));
                }
                else
                {
                    throw new FormatException("프리셋 항목 형식 오류: " + p);
                }
            }
            return s;
        }

        /// <summary>전·후 상태의 플래그·카운터 차이를 정렬된 한 줄 문자열로. 차이 없으면 "-".</summary>
        public static string Diff(GameState before, GameState after)
        {
            List<string> items = new List<string>();
            foreach (string f in after.Flags) { if (!before.Flags.Contains(f)) { items.Add("+flag:" + f); } }
            foreach (string f in before.Flags) { if (!after.Flags.Contains(f)) { items.Add("-flag:" + f); } }
            HashSet<string> keys = new HashSet<string>(before.Counters.Keys);
            keys.UnionWith(after.Counters.Keys);
            foreach (string k in keys)
            {
                int b = before.GetCounter(k);
                int a = after.GetCounter(k);
                if (a != b) { items.Add("counter:" + k + "=" + a.ToString(CultureInfo.InvariantCulture)); }
            }
            if (items.Count == 0) { return "-"; }
            items.Sort(StringComparer.Ordinal);
            return string.Join(",", items.ToArray());
        }

        /// <summary>ID 목록을 이어진 숫자 구간으로 줄여 적는다("1-20,25"). 숫자가 아니거나 이어지지 않으면 그대로 나열.</summary>
        public static string CompactIds(IList<string> ids)
        {
            if (ids.Count == 0) { return "-"; }
            StringBuilder sb = new StringBuilder();
            int i = 0;
            while (i < ids.Count)
            {
                int j = i;
                int start;
                bool numeric = int.TryParse(ids[i], NumberStyles.None, CultureInfo.InvariantCulture, out start);
                if (numeric)
                {
                    while (j + 1 < ids.Count)
                    {
                        int next;
                        if (!int.TryParse(ids[j + 1], NumberStyles.None, CultureInfo.InvariantCulture, out next) || next != start + (j + 1 - i)) { break; }
                        j++;
                    }
                }
                if (sb.Length > 0) { sb.Append(','); }
                sb.Append(ids[i]);
                if (j > i) { sb.Append('-').Append(ids[j]); }
                i = j + 1;
            }
            return sb.ToString();
        }
    }
}
