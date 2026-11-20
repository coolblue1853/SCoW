using NUnit.Framework;
using SCoW.Core.State;
using SCoW.Core.Story;

namespace SCoW.Tests
{
    public sealed class StoryRulesTests
    {
        private static GameStateAccess Acc(out GameState gs)
        {
            gs = new GameState();
            return new GameStateAccess(gs);
        }

        private static bool Eval(string dsl, IStateReader s)
        {
            ParseOutcome<Condition> r = ConditionParser.Parse(dsl);
            Assert.IsTrue(r.Ok, r.Error);
            return r.Value.Eval(s);
        }

        [Test]
        public void Flag_Not_AndOr_Precedence()
        {
            GameState gs;
            GameStateAccess a = Acc(out gs);
            gs.SetFlag("a", true);
            Assert.IsTrue(Eval("flag:a", a));
            Assert.IsFalse(Eval("flag:b", a));
            Assert.IsTrue(Eval("!flag:b", a));
            Assert.IsFalse(Eval("flag:a && flag:b", a));
            Assert.IsTrue(Eval("flag:a || flag:b", a));
            Assert.IsTrue(Eval("flag:b || flag:a && !flag:b", a));
            Assert.IsFalse(Eval("(flag:b || flag:a) && flag:b", a));
            Assert.IsTrue(Eval("!(flag:b && flag:a)", a));
            Assert.IsTrue(Eval("!!flag:a", a));
        }

        [Test]
        public void Counter_Time_Place_AnyAll()
        {
            GameState gs;
            GameStateAccess a = Acc(out gs);
            gs.SetCounter("enemyDeathCount", 3);
            gs.SetCounter("timeCount", 12);
            gs.SetText("nowPlace", "Bar");
            Assert.IsTrue(Eval("counter:enemyDeathCount>=3", a));
            Assert.IsFalse(Eval("counter:enemyDeathCount>3", a));
            Assert.IsTrue(Eval("counter:enemyDeathCount==3 && counter:enemyDeathCount!=4", a));
            Assert.IsTrue(Eval("counter:x<1", a));
            Assert.IsTrue(Eval("counter:enemyDeathCount>-1", a));
            Assert.IsTrue(Eval("flag:z && time>=12 || place:Bar", a));
            Assert.IsFalse(Eval("time<12", a));
            Assert.IsTrue(Eval("time<=12", a));
            Assert.IsFalse(Eval("place:Office", a));
            Assert.IsTrue(Eval("any(flag:a,flag:b,place:Bar)", a));
            Assert.IsFalse(Eval("all(time>=12,flag:b)", a));
            Assert.IsTrue(Eval("true", a));
            Assert.IsFalse(Eval("false", a));
            Assert.IsTrue(Eval("", a));
            Assert.IsTrue(Eval("flag:intelQuestHouse1 && time>=12 || !flag:x", a));
        }

        [TestCase("flag:")]
        [TestCase("flag:a &&")]
        [TestCase("(flag:a")]
        [TestCase("flag:a)")]
        [TestCase("counter:x")]
        [TestCase("counter:x>=abc")]
        [TestCase("counter:x=3")]
        [TestCase("nope:x")]
        [TestCase("any(flag:a")]
        [TestCase("flag:a flag:b")]
        public void BadCondition_ReturnsError(string dsl)
        {
            Assert.IsFalse(ConditionParser.Parse(dsl).Ok, dsl);
        }

        [TestCase("time")]
        [TestCase("timex>=3")]
        [TestCase("flag:a$b")]
        [TestCase("counter:x>=3#")]
        public void BadCondition_TimeExactAndAtomChars(string dsl)
        {
            Assert.IsFalse(ConditionParser.Parse(dsl).Ok, dsl);
        }

        [Test]
        public void Condition_DeepNesting_FailsWithoutStackOverflow()
        {
            string deep = new string('(', 5000) + "flag:a" + new string(')', 5000);
            Assert.IsFalse(ConditionParser.Parse(deep).Ok);
            string ok = new string('(', 30) + "flag:a" + new string(')', 30);
            Assert.IsTrue(ConditionParser.Parse(ok).Ok);
            string nots = new string('!', 5000) + "flag:a";
            Assert.IsFalse(ConditionParser.Parse(nots).Ok);
        }

        [Test]
        public void Effects_SetClearAddAndHostCalls()
        {
            GameState gs;
            GameStateAccess a = Acc(out gs);
            FakeHost host = new FakeHost();
            ParseOutcome<Effect[]> r = EffectParser.Parse(
                "set:maeveFirstDialog; add:san,-1; add:san,-2; setc:x,7; goto:n2; battle:sewer; check:observation,hard,ok,fail; set:tmp; clear:tmp");
            Assert.IsTrue(r.Ok, r.Error);
            EffectRunner.ApplyAll(r.Value, a, a, host);
            Assert.IsTrue(gs.GetFlag("maeveFirstDialog"));
            Assert.IsFalse(gs.GetFlag("tmp"));
            Assert.AreEqual(-3, gs.GetCounter("san"));
            Assert.AreEqual(7, gs.GetCounter("x"));
            Assert.AreEqual("n2", host.Went);
            Assert.AreEqual("sewer", host.Battle);
            Assert.AreEqual("observation/hard/ok/fail", host.Check);
        }

        [Test]
        public void Effects_EmptyAndNullHost_AreSafe()
        {
            GameState gs;
            GameStateAccess a = Acc(out gs);
            Assert.AreEqual(0, EffectParser.Parse("").Value.Length);
            EffectRunner.ApplyAll(EffectParser.Parse("goto:n;battle:b").Value, a, a, null);
            EffectRunner.ApplyAll(null, a, a, null);
        }

        [TestCase("noColon")]
        [TestCase("add:san")]
        [TestCase("add:san,x")]
        [TestCase("check:a,b,c")]
        [TestCase("zap:x")]
        [TestCase("set:")]
        public void BadEffect_ReturnsError(string dsl)
        {
            Assert.IsFalse(EffectParser.Parse(dsl).Ok, dsl);
        }

        private sealed class FakeHost : IStoryHost
        {
            public string Went, Battle, Check;
            public void Goto(string nodeId) { Went = nodeId; }
            public void StartBattle(string battleId) { Battle = battleId; }
            public void RequestCheck(string skill, string difficulty, string okNode, string failNode)
            {
                Check = skill + "/" + difficulty + "/" + okNode + "/" + failNode;
            }
        }
    }
}
