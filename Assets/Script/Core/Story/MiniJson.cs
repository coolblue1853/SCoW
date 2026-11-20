using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SCoW.Core.Story
{
    /// <summary>
    /// 최소 JSON 파서(Core 는 엔진 참조 없음, Unity 6 netstandard 프로필에 System.Text.Json 이 기본 포함되지 않아 수제).
    /// 결과: Dictionary&lt;string,object&gt;, List&lt;object&gt;, string, double, bool, null. 실패 시 error 에 사유(예외 금지).
    /// </summary>
    public static class MiniJson
    {
        public static object Parse(string text, out string error)
        {
            error = null;
            if (text == null) { error = "JSON 텍스트가 null"; return null; }
            int p = text.Length > 0 && text[0] == '\uFEFF' ? 1 : 0;
            try
            {
                object v = ReadValue(text, ref p, 0);
                SkipWs(text, ref p);
                if (p != text.Length) { throw new System.FormatException("끝에 남은 글자"); }
                return v;
            }
            catch (System.FormatException e)
            {
                error = "JSON 오류(위치 " + p + "): " + e.Message;
                return null;
            }
        }

        private static void SkipWs(string s, ref int p)
        {
            while (p < s.Length && char.IsWhiteSpace(s[p])) { p++; }
        }

        private static object ReadValue(string s, ref int p, int depth)
        {
            if (depth > 64) { throw new System.FormatException("중첩이 너무 깊음"); }
            SkipWs(s, ref p);
            if (p >= s.Length) { throw new System.FormatException("값이 필요함"); }
            char c = s[p];
            if (c == '{') { return ReadObject(s, ref p, depth); }
            if (c == '[') { return ReadArray(s, ref p, depth); }
            if (c == '"') { return ReadString(s, ref p); }
            if (Match(s, ref p, "true")) { return true; }
            if (Match(s, ref p, "false")) { return false; }
            if (Match(s, ref p, "null")) { return null; }
            return ReadNumber(s, ref p);
        }

        private static bool Match(string s, ref int p, string w)
        {
            if (string.CompareOrdinal(s, p, w, 0, w.Length) == 0)
            {
                p += w.Length;
                return true;
            }
            return false;
        }

        private static object ReadNumber(string s, ref int p)
        {
            int st = p;
            while (p < s.Length && "+-.eE0123456789".IndexOf(s[p]) >= 0) { p++; }
            double d;
            if (p == st || !double.TryParse(s.Substring(st, p - st), NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                throw new System.FormatException("잘못된 값");
            }
            return d;
        }

        private static string ReadString(string s, ref int p)
        {
            p++;
            StringBuilder sb = new StringBuilder();
            while (p < s.Length)
            {
                char c = s[p++];
                if (c == '"') { return sb.ToString(); }
                if (c != '\\') { sb.Append(c); continue; }
                if (p >= s.Length) { break; }
                char e = s[p++];
                switch (e)
                {
                    case '"': sb.Append('"'); break;
                    case '\\': sb.Append('\\'); break;
                    case '/': sb.Append('/'); break;
                    case 'b': sb.Append('\b'); break;
                    case 'f': sb.Append('\f'); break;
                    case 'n': sb.Append('\n'); break;
                    case 'r': sb.Append('\r'); break;
                    case 't': sb.Append('\t'); break;
                    case 'u':
                        int code = 0;
                        if (p + 4 > s.Length)
                        {
                            throw new System.FormatException("잘못된 \\u 이스케이프");
                        }
                        for (int k = 0; k < 4; k++)
                        {
                            int h = HexVal(s[p + k]);
                            if (h < 0) { throw new System.FormatException("잘못된 \\u 이스케이프"); }
                            code = code * 16 + h;
                        }
                        sb.Append((char)code);
                        p += 4;
                        break;
                    default: throw new System.FormatException("잘못된 이스케이프");
                }
            }
            throw new System.FormatException("닫히지 않은 문자열");
        }

        private static int HexVal(char c)
        {
            if (c >= '0' && c <= '9') { return c - '0'; }
            if (c >= 'a' && c <= 'f') { return c - 'a' + 10; }
            if (c >= 'A' && c <= 'F') { return c - 'A' + 10; }
            return -1;
        }

        private static List<object> ReadArray(string s, ref int p, int depth)
        {
            p++;
            List<object> list = new List<object>();
            SkipWs(s, ref p);
            if (p < s.Length && s[p] == ']') { p++; return list; }
            while (true)
            {
                list.Add(ReadValue(s, ref p, depth + 1));
                SkipWs(s, ref p);
                if (p >= s.Length) { throw new System.FormatException("']' 누락"); }
                if (s[p] == ',') { p++; continue; }
                if (s[p] == ']') { p++; return list; }
                throw new System.FormatException("',' 또는 ']' 필요");
            }
        }

        private static Dictionary<string, object> ReadObject(string s, ref int p, int depth)
        {
            p++;
            Dictionary<string, object> d = new Dictionary<string, object>();
            SkipWs(s, ref p);
            if (p < s.Length && s[p] == '}') { p++; return d; }
            while (true)
            {
                SkipWs(s, ref p);
                if (p >= s.Length || s[p] != '"') { throw new System.FormatException("키(문자열) 필요"); }
                string k = ReadString(s, ref p);
                SkipWs(s, ref p);
                if (p >= s.Length || s[p] != ':') { throw new System.FormatException("':' 필요"); }
                p++;
                d[k] = ReadValue(s, ref p, depth + 1);
                SkipWs(s, ref p);
                if (p >= s.Length) { throw new System.FormatException("'}' 누락"); }
                if (s[p] == ',') { p++; continue; }
                if (s[p] == '}') { p++; return d; }
                throw new System.FormatException("',' 또는 '}' 필요");
            }
        }
    }
}
