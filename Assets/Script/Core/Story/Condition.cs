using System.Collections.Generic;

namespace SCoW.Core.Story
{
    public enum CmpOp { Ge, Le, Gt, Lt, Eq, Ne }

    public abstract class Condition
    {
        public abstract bool Eval(IStateReader s);

        /// <summary>조건 없음 = 항상 참.</summary>
        public static readonly Condition Always = new ConstCond(true);

        internal static bool Compare(int left, CmpOp op, int right)
        {
            switch (op)
            {
                case CmpOp.Ge: return left >= right;
                case CmpOp.Le: return left <= right;
                case CmpOp.Gt: return left > right;
                case CmpOp.Lt: return left < right;
                case CmpOp.Eq: return left == right;
                default: return left != right;
            }
        }

        internal static Condition[] Copy(IList<Condition> l)
        {
            Condition[] a = new Condition[l.Count];
            l.CopyTo(a, 0);
            return a;
        }
    }

    public sealed class ConstCond : Condition
    {
        private readonly bool _v;
        public ConstCond(bool v) { _v = v; }
        public override bool Eval(IStateReader s) { return _v; }
    }

    public sealed class FlagIs : Condition
    {
        public readonly string Key;
        public FlagIs(string key) { Key = key; }
        public override bool Eval(IStateReader s) { return s.GetFlag(Key); }
    }

    public sealed class CounterCmp : Condition
    {
        public readonly string Key;
        public readonly CmpOp Op;
        public readonly int Value;
        /// <summary>0 이면 나머지 연산 없음. 양수면 카운터 % Mod 를 비교한다(예: timeCount%4==2 = 낮).</summary>
        public readonly int Mod;
        public CounterCmp(string key, CmpOp op, int value) : this(key, op, value, 0) { }
        public CounterCmp(string key, CmpOp op, int value, int mod) { Key = key; Op = op; Value = value; Mod = mod; }
        public override bool Eval(IStateReader s)
        {
            int v = s.GetCounter(Key);
            if (Mod > 0) { v = ((v % Mod) + Mod) % Mod; }
            return Compare(v, Op, Value);
        }
    }

    public sealed class TextIs : Condition
    {
        public readonly string Key;
        public readonly string Value;
        public TextIs(string key, string value) { Key = key; Value = value; }
        public override bool Eval(IStateReader s) { return s.GetText(Key) == Value; }
    }

    public sealed class Not : Condition
    {
        public readonly Condition Inner;
        public Not(Condition inner) { Inner = inner; }
        public override bool Eval(IStateReader s) { return !Inner.Eval(s); }
    }

    public sealed class And : Condition
    {
        public readonly Condition[] Items;
        public And(IList<Condition> items) { Items = Copy(items); }
        public override bool Eval(IStateReader s)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (!Items[i].Eval(s)) { return false; }
            }
            return true;
        }
    }

    public sealed class Or : Condition
    {
        public readonly Condition[] Items;
        public Or(IList<Condition> items) { Items = Copy(items); }
        public override bool Eval(IStateReader s)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i].Eval(s)) { return true; }
            }
            return false;
        }
    }
}
