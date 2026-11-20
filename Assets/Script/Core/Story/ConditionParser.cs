using System.Collections.Generic;

namespace SCoW.Core.Story
{
    /// <summary>
    /// 조건 DSL. 원자: flag:이름, counter:이름 op 정수(공백 없이), time op 정수(카운터 "time"), place:이름(텍스트 "nowPlace"), true, false.
    /// 조합: ! , &amp;&amp; , || (&amp;&amp; 가 먼저), 괄호, any(a,b,..) = OR, all(a,b,..) = AND. op = &gt;= &lt;= &gt; &lt; == !=.
    /// 빈 문자열은 항상 참.
    /// </summary>
    public static class ConditionParser
    {
        public const string TimeCounter = "time";
        public const string PlaceText = "nowPlace";

        public static ParseOutcome<Condition> Parse(string src)
        {
            ParseOutcome<Condition> r = new ParseOutcome<Condition>();
            if (string.IsNullOrWhiteSpace(src))
            {
                r.Value = Condition.Always;
                return r;
            }
            Cursor c = new Cursor(src);
            try
            {
                Condition v = ParseOr(c);
                c.SkipWs();
                if (!c.End)
                {
                    throw new DslError("예상 밖 글자 '" + c.Peek + "' (위치 " + c.Pos + ")");
                }
                r.Value = v;
            }
            catch (DslError e)
            {
                r.Error = e.Message + ": " + src;
            }
            return r;
        }

        internal sealed class DslError : System.Exception
        {
            public DslError(string m) : base(m) { }
        }

        private sealed class Cursor
        {
            public readonly string S;
            public int Pos;
            public Cursor(string s) { S = s; }
            public bool End { get { return Pos >= S.Length; } }
            public char Peek { get { return S[Pos]; } }
            public void SkipWs()
            {
                while (Pos < S.Length && char.IsWhiteSpace(S[Pos])) { Pos++; }
            }
            public bool Take(string t)
            {
                SkipWs();
                if (string.CompareOrdinal(S, Pos, t, 0, t.Length) == 0)
                {
                    Pos += t.Length;
                    return true;
                }
                return false;
            }
        }

        private static Condition ParseOr(Cursor c)
        {
            List<Condition> items = new List<Condition> { ParseAnd(c) };
            while (c.Take("||"))
            {
                items.Add(ParseAnd(c));
            }
            return items.Count == 1 ? items[0] : new Or(items);
        }

        private static Condition ParseAnd(Cursor c)
        {
            List<Condition> items = new List<Condition> { ParseUnary(c) };
            while (c.Take("&&"))
            {
                items.Add(ParseUnary(c));
            }
            return items.Count == 1 ? items[0] : new And(items);
        }

        private static Condition ParseUnary(Cursor c)
        {
            c.SkipWs();
            if (!c.End && c.Peek == '!')
            {
                c.Pos++;
                return new Not(ParseUnary(c));
            }
            if (!c.End && c.Peek == '(')
            {
                c.Pos++;
                Condition v = ParseOr(c);
                if (!c.Take(")")) { throw new DslError("')' 누락"); }
                return v;
            }
            string atom = ReadAtom(c);
            if (atom.Length == 0)
            {
                throw new DslError("조건이 비어 있음 (위치 " + c.Pos + ")");
            }
            if ((atom == "any" || atom == "all") && !c.End && c.Peek == '(')
            {
                c.Pos++;
                List<Condition> items = new List<Condition> { ParseOr(c) };
                while (c.Take(",")) { items.Add(ParseOr(c)); }
                if (!c.Take(")")) { throw new DslError("')' 누락"); }
                if (atom == "any") { return new Or(items); }
                return new And(items);
            }
            return ParseAtom(atom);
        }

        private static string ReadAtom(Cursor c)
        {
            int start = c.Pos;
            while (c.Pos < c.S.Length)
            {
                char ch = c.S[c.Pos];
                if (char.IsWhiteSpace(ch) || ch == '(' || ch == ')' || ch == ',' || ch == '&' || ch == '|')
                {
                    break;
                }
                c.Pos++;
            }
            return c.S.Substring(start, c.Pos - start);
        }

        private static Condition ParseAtom(string a)
        {
            if (a == "true") { return new ConstCond(true); }
            if (a == "false") { return new ConstCond(false); }
            if (a.StartsWith("flag:", System.StringComparison.Ordinal))
            {
                string k = a.Substring(5);
                if (k.Length == 0) { throw new DslError("flag 이름 없음"); }
                return new FlagIs(k);
            }
            if (a.StartsWith("place:", System.StringComparison.Ordinal))
            {
                string k = a.Substring(6);
                if (k.Length == 0) { throw new DslError("place 이름 없음"); }
                return new TextIs(PlaceText, k);
            }
            if (a.StartsWith("counter:", System.StringComparison.Ordinal))
            {
                return ParseCmp(a.Substring(8), a);
            }
            if (a.StartsWith("time", System.StringComparison.Ordinal))
            {
                return ParseCmp(a, a);
            }
            throw new DslError("알 수 없는 조건 '" + a + "'");
        }

        private static Condition ParseCmp(string body, string whole)
        {
            int i = body.IndexOfAny(new[] { '>', '<', '=', '!' });
            if (i <= 0) { throw new DslError("비교 연산자 없음 '" + whole + "'"); }
            string key = body.Substring(0, i);
            CmpOp op;
            int len;
            if (!ReadOp(body, i, out op, out len)) { throw new DslError("잘못된 연산자 '" + whole + "'"); }
            int val;
            if (!int.TryParse(body.Substring(i + len), System.Globalization.NumberStyles.AllowLeadingSign,
                    System.Globalization.CultureInfo.InvariantCulture, out val))
            {
                throw new DslError("정수 아님 '" + whole + "'");
            }
            return new CounterCmp(key, op, val);
        }

        private static bool ReadOp(string s, int i, out CmpOp op, out int len)
        {
            string two = i + 2 <= s.Length ? s.Substring(i, 2) : "";
            len = 2;
            switch (two)
            {
                case ">=": op = CmpOp.Ge; return true;
                case "<=": op = CmpOp.Le; return true;
                case "==": op = CmpOp.Eq; return true;
                case "!=": op = CmpOp.Ne; return true;
            }
            len = 1;
            if (s[i] == '>') { op = CmpOp.Gt; return true; }
            if (s[i] == '<') { op = CmpOp.Lt; return true; }
            op = CmpOp.Eq;
            return false;
        }
    }
}
