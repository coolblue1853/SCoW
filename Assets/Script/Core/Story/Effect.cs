using System.Collections.Generic;
using System.Globalization;

namespace SCoW.Core.Story
{
    public abstract class Effect
    {
        public abstract void Apply(IStateWriter w, IStateReader r, IStoryHost host);
    }

    public sealed class SetFlag : Effect
    {
        public readonly string Key;
        public readonly bool Value;
        public SetFlag(string key, bool value) { Key = key; Value = value; }
        public override void Apply(IStateWriter w, IStateReader r, IStoryHost host) { w.SetFlag(Key, Value); }
    }

    public sealed class AddCounter : Effect
    {
        public readonly string Key;
        public readonly int Delta;
        public AddCounter(string key, int delta) { Key = key; Delta = delta; }
        public override void Apply(IStateWriter w, IStateReader r, IStoryHost host)
        {
            w.SetCounter(Key, r.GetCounter(Key) + Delta);
        }
    }

    public sealed class SetCounter : Effect
    {
        public readonly string Key;
        public readonly int Value;
        public SetCounter(string key, int value) { Key = key; Value = value; }
        public override void Apply(IStateWriter w, IStateReader r, IStoryHost host) { w.SetCounter(Key, Value); }
    }

    public sealed class GotoNode : Effect
    {
        public readonly string NodeId;
        public GotoNode(string nodeId) { NodeId = nodeId; }
        public override void Apply(IStateWriter w, IStateReader r, IStoryHost host)
        {
            if (host != null) { host.Goto(NodeId); }
        }
    }

    public sealed class StartBattle : Effect
    {
        public readonly string BattleId;
        public StartBattle(string battleId) { BattleId = battleId; }
        public override void Apply(IStateWriter w, IStateReader r, IStoryHost host)
        {
            if (host != null) { host.StartBattle(BattleId); }
        }
    }

    public sealed class RunCheck : Effect
    {
        public readonly string Skill;
        public readonly string Difficulty;
        public readonly string OkNode;
        public readonly string FailNode;
        public RunCheck(string skill, string difficulty, string okNode, string failNode)
        {
            Skill = skill; Difficulty = difficulty; OkNode = okNode; FailNode = failNode;
        }
        public override void Apply(IStateWriter w, IStateReader r, IStoryHost host)
        {
            if (host != null) { host.RequestCheck(Skill, Difficulty, OkNode, FailNode); }
        }
    }

    public static class EffectRunner
    {
        public static void ApplyAll(Effect[] effects, IStateWriter w, IStateReader r, IStoryHost host)
        {
            if (effects == null) { return; }
            for (int i = 0; i < effects.Length; i++)
            {
                effects[i].Apply(w, r, host);
            }
        }
    }

    /// <summary>
    /// 효과 DSL. ';' 로 여러 개 구분. set:flag, clear:flag, add:counter,정수, setc:counter,정수,
    /// goto:node, battle:id, check:skill,difficulty,okNode,failNode. 빈 문자열은 효과 없음.
    /// </summary>
    public static class EffectParser
    {
        public static ParseOutcome<Effect[]> Parse(string src)
        {
            ParseOutcome<Effect[]> r = new ParseOutcome<Effect[]>();
            List<Effect> list = new List<Effect>();
            if (!string.IsNullOrWhiteSpace(src))
            {
                string[] parts = src.Split(';');
                for (int i = 0; i < parts.Length; i++)
                {
                    string p = parts[i].Trim();
                    if (p.Length == 0) { continue; }
                    string err = ParseOne(p, list);
                    if (err != null)
                    {
                        r.Error = err + ": " + p;
                        return r;
                    }
                }
            }
            r.Value = list.ToArray();
            return r;
        }

        private static string ParseOne(string p, List<Effect> list)
        {
            int colon = p.IndexOf(':');
            if (colon <= 0) { return "':' 없음"; }
            string verb = p.Substring(0, colon);
            string[] a = p.Substring(colon + 1).Split(',');
            for (int i = 0; i < a.Length; i++) { a[i] = a[i].Trim(); }
            int n;
            switch (verb)
            {
                case "set":
                    if (a.Length != 1 || a[0].Length == 0) { return "set 인자 1개 필요"; }
                    list.Add(new SetFlag(a[0], true));
                    return null;
                case "clear":
                    if (a.Length != 1 || a[0].Length == 0) { return "clear 인자 1개 필요"; }
                    list.Add(new SetFlag(a[0], false));
                    return null;
                case "add":
                    if (a.Length != 2 || a[0].Length == 0 || !TryInt(a[1], out n)) { return "add:이름,정수 형식"; }
                    list.Add(new AddCounter(a[0], n));
                    return null;
                case "setc":
                    if (a.Length != 2 || a[0].Length == 0 || !TryInt(a[1], out n)) { return "setc:이름,정수 형식"; }
                    list.Add(new SetCounter(a[0], n));
                    return null;
                case "goto":
                    if (a.Length != 1 || a[0].Length == 0) { return "goto 인자 1개 필요"; }
                    list.Add(new GotoNode(a[0]));
                    return null;
                case "battle":
                    if (a.Length != 1 || a[0].Length == 0) { return "battle 인자 1개 필요"; }
                    list.Add(new StartBattle(a[0]));
                    return null;
                case "check":
                    if (a.Length != 4 || a[0].Length == 0 || a[2].Length == 0 || a[3].Length == 0)
                    {
                        return "check:skill,difficulty,okNode,failNode 형식";
                    }
                    list.Add(new RunCheck(a[0], a[1], a[2], a[3]));
                    return null;
            }
            return "알 수 없는 효과 '" + verb + "'";
        }

        private static bool TryInt(string s, out int n)
        {
            return int.TryParse(s, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out n);
        }
    }
}
