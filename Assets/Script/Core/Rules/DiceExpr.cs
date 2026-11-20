using System;

namespace SCoW.Core.Rules
{
    /// <summary>"NdM[+K|-K][*X]" 주사위식. 예 "1d6+2", "3d6*5", "1d100".</summary>
    public readonly struct DiceExpr
    {
        public readonly int Count;
        public readonly int Sides;
        public readonly int Modifier;
        public readonly int Multiplier;

        public DiceExpr(int count, int sides, int modifier = 0, int multiplier = 1)
        {
            Count = count;
            Sides = sides;
            Modifier = modifier;
            Multiplier = multiplier;
        }

        public int Min => (Count + Modifier) * Multiplier;
        public int Max => (Count * Sides + Modifier) * Multiplier;

        public int Roll(IRng rng)
        {
            int sum = 0;
            for (int i = 0; i < Count; i++)
            {
                sum += rng.Next(1, Sides + 1);
            }
            return (sum + Modifier) * Multiplier;
        }

        public static bool TryParse(string text, out DiceExpr expr)
        {
            expr = default;
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            string s = text.Replace(" ", "").ToLowerInvariant();
            int mult = 1;
            int star = s.IndexOf('*');
            if (star >= 0)
            {
                if (!int.TryParse(s.Substring(star + 1), out mult) || mult < 1)
                {
                    return false;
                }
                s = s.Substring(0, star);
            }
            int mod = 0;
            int sign = s.IndexOfAny(new[] { '+', '-' });
            if (sign >= 0)
            {
                if (!int.TryParse(s.Substring(sign), out mod))
                {
                    return false;
                }
                s = s.Substring(0, sign);
            }
            int d = s.IndexOf('d');
            if (d <= 0)
            {
                return false;
            }
            if (!int.TryParse(s.Substring(0, d), out int count) || !int.TryParse(s.Substring(d + 1), out int sides)
                || count < 1 || sides < 1)
            {
                return false;
            }
            expr = new DiceExpr(count, sides, mod, mult);
            return true;
        }

        public static DiceExpr Parse(string text)
        {
            if (!TryParse(text, out DiceExpr e))
            {
                throw new FormatException("주사위식 형식 오류: " + text);
            }
            return e;
        }
    }
}
