using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SCoW.Core.State;

namespace SCoW.Core.Save
{
    /// <summary>
    /// GameState·SaveMeta 의 결정적 텍스트 직렬화. 키를 정렬해 쓰므로 Dictionary·HashSet 열거 순서와 무관하게 같은 문자열이 나온다.
    /// ES3 는 이 문자열을 "state"/"meta" 키로 보관만 한다(어댑터는 Data/SaveService).
    /// 형식: 첫 줄 "SCOW-STATE v", 항목 줄(F/C/T + 탭 구분), 끝 줄 "END n"(항목 수, 잘림 검출). 개행은 LF.
    /// </summary>
    public static class SaveCodec
    {
        private const string StateHeader = "SCOW-STATE";
        private const string MetaHeader = "SCOW-META";
        private const int MetaFieldCount = 4;

        public static string EncodeState(GameState s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(StateHeader).Append(' ').Append(s.SchemaVersion.ToString(CultureInfo.InvariantCulture)).Append('\n');
            int n = 0;
            foreach (string k in Sorted(s.Flags))
            {
                sb.Append("F\t").Append(Esc(k)).Append('\n');
                n++;
            }
            foreach (string k in Sorted(s.Counters.Keys))
            {
                sb.Append("C\t").Append(Esc(k)).Append('\t').Append(s.Counters[k].ToString(CultureInfo.InvariantCulture)).Append('\n');
                n++;
            }
            foreach (string k in Sorted(s.Texts.Keys))
            {
                sb.Append("T\t").Append(Esc(k)).Append('\t').Append(Esc(s.Texts[k])).Append('\n');
                n++;
            }
            sb.Append("END ").Append(n.ToString(CultureInfo.InvariantCulture)).Append('\n');
            return sb.ToString();
        }

        /// <summary>실패해도 예외를 던지지 않는다. 성공 시 state 는 새 객체.</summary>
        public static SaveError TryDecodeState(string text, out GameState state)
        {
            state = null;
            if (string.IsNullOrEmpty(text))
            {
                return SaveError.Corrupt;
            }
            string[] lines = text.Split('\n');
            int version;
            if (!TryHeader(lines[0], StateHeader, out version))
            {
                return SaveError.Corrupt;
            }
            GameState s = new GameState();
            s.SchemaVersion = version;
            int n = 0;
            int endCount = -1;
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Length == 0)
                {
                    continue;
                }
                if (endCount >= 0)
                {
                    return SaveError.Corrupt;
                }
                string[] f = line.Split('\t');
                if (f[0] == "F" && f.Length == 2)
                {
                    s.Flags.Add(Unesc(f[1]));
                    n++;
                }
                else if (f[0] == "C" && f.Length == 3)
                {
                    int v;
                    if (!int.TryParse(f[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out v))
                    {
                        return SaveError.Corrupt;
                    }
                    s.Counters[Unesc(f[1])] = v;
                    n++;
                }
                else if (f[0] == "T" && f.Length == 3)
                {
                    s.Texts[Unesc(f[1])] = Unesc(f[2]);
                    n++;
                }
                else if (f.Length == 1 && f[0].StartsWith("END ", StringComparison.Ordinal))
                {
                    if (!int.TryParse(f[0].Substring(4), NumberStyles.Integer, CultureInfo.InvariantCulture, out endCount) || endCount < 0)
                    {
                        return SaveError.Corrupt;
                    }
                }
                else
                {
                    return SaveError.Corrupt;
                }
            }
            if (endCount != n)
            {
                return SaveError.Corrupt;
            }
            state = s;
            return SaveError.None;
        }

        public static string EncodeMeta(SaveMeta m)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(MetaHeader).Append(' ').Append(m.SchemaVersion.ToString(CultureInfo.InvariantCulture)).Append('\n');
            sb.Append("saved\t").Append(m.SavedAtUtcTicks.ToString(CultureInfo.InvariantCulture)).Append('\n');
            sb.Append("play\t").Append(m.PlayTimeSec.ToString(CultureInfo.InvariantCulture)).Append('\n');
            sb.Append("place\t").Append(Esc(m.PlaceId ?? string.Empty)).Append('\n');
            sb.Append("time\t").Append(m.TimeCount.ToString(CultureInfo.InvariantCulture)).Append('\n');
            sb.Append("END ").Append(MetaFieldCount.ToString(CultureInfo.InvariantCulture)).Append('\n');
            return sb.ToString();
        }

        public static SaveError TryDecodeMeta(string text, out SaveMeta meta)
        {
            meta = null;
            if (string.IsNullOrEmpty(text))
            {
                return SaveError.Corrupt;
            }
            string[] lines = text.Split('\n');
            int version;
            if (!TryHeader(lines[0], MetaHeader, out version))
            {
                return SaveError.Corrupt;
            }
            SaveMeta m = new SaveMeta();
            m.SchemaVersion = version;
            int seen = 0;
            bool end = false;
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Length == 0)
                {
                    continue;
                }
                if (line == "END " + MetaFieldCount.ToString(CultureInfo.InvariantCulture))
                {
                    end = true;
                    continue;
                }
                string[] f = line.Split('\t');
                if (f.Length != 2 || end)
                {
                    return SaveError.Corrupt;
                }
                long v;
                switch (f[0])
                {
                    case "saved":
                        if (!long.TryParse(f[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out v) || v < DateTime.MinValue.Ticks || v > DateTime.MaxValue.Ticks)
                        {
                            return SaveError.Corrupt;
                        }
                        m.SavedAtUtcTicks = v;
                        break;
                    case "play":
                        if (!long.TryParse(f[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out v) || v < 0)
                        {
                            return SaveError.Corrupt;
                        }
                        m.PlayTimeSec = v;
                        break;
                    case "place":
                        m.PlaceId = Unesc(f[1]);
                        break;
                    case "time":
                        if (!long.TryParse(f[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out v) || v < int.MinValue || v > int.MaxValue)
                        {
                            return SaveError.Corrupt;
                        }
                        m.TimeCount = (int)v;
                        break;
                    default:
                        return SaveError.Corrupt;
                }
                seen++;
            }
            if (!end || seen != MetaFieldCount)
            {
                return SaveError.Corrupt;
            }
            meta = m;
            return SaveError.None;
        }

        private static bool TryHeader(string line, string name, out int version)
        {
            version = 0;
            string prefix = name + " ";
            if (!line.StartsWith(prefix, StringComparison.Ordinal))
            {
                return false;
            }
            return int.TryParse(line.Substring(prefix.Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out version) && version >= 1;
        }

        private static List<string> Sorted(IEnumerable<string> keys)
        {
            List<string> list = new List<string>(keys);
            list.Sort(StringComparer.Ordinal);
            return list;
        }

        private static string Esc(string s)
        {
            return s.Replace("\\", "\\\\").Replace("\t", "\\t").Replace("\n", "\\n").Replace("\r", "\\r");
        }

        private static string Unesc(string s)
        {
            if (s.IndexOf('\\') < 0)
            {
                return s;
            }
            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == '\\' && i + 1 < s.Length)
                {
                    i++;
                    char d = s[i];
                    sb.Append(d == 't' ? '\t' : d == 'n' ? '\n' : d == 'r' ? '\r' : d);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
