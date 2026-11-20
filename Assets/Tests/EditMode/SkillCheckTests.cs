using NUnit.Framework;
using SCoW.Core.Rules;

namespace SCoW.Tests
{
    public class SkillCheckTests
    {
        // 현 동작 골든: 대실패 95+ 우선, 대성공 value <= target/3f, 성공 value <= target.
        [TestCase(0, 1, SuccessLevel.Failure)]
        [TestCase(0, 5, SuccessLevel.Failure)]
        [TestCase(0, 95, SuccessLevel.Fumble)]
        [TestCase(0, 96, SuccessLevel.Fumble)]
        [TestCase(0, 100, SuccessLevel.Fumble)]
        [TestCase(1, 1, SuccessLevel.Success)]
        [TestCase(1, 5, SuccessLevel.Failure)]
        [TestCase(50, 1, SuccessLevel.CriticalSuccess)]
        [TestCase(50, 16, SuccessLevel.CriticalSuccess)]
        [TestCase(50, 17, SuccessLevel.Success)]
        [TestCase(50, 50, SuccessLevel.Success)]
        [TestCase(50, 51, SuccessLevel.Failure)]
        [TestCase(50, 94, SuccessLevel.Failure)]
        [TestCase(50, 95, SuccessLevel.Fumble)]
        [TestCase(89, 29, SuccessLevel.CriticalSuccess)]
        [TestCase(89, 30, SuccessLevel.Success)]
        [TestCase(89, 89, SuccessLevel.Success)]
        [TestCase(89, 94, SuccessLevel.Failure)]
        [TestCase(89, 95, SuccessLevel.Fumble)]
        [TestCase(90, 30, SuccessLevel.CriticalSuccess)]
        [TestCase(90, 90, SuccessLevel.Success)]
        [TestCase(90, 94, SuccessLevel.Failure)]
        [TestCase(90, 95, SuccessLevel.Fumble)]
        [TestCase(99, 95, SuccessLevel.Fumble)]
        [TestCase(99, 96, SuccessLevel.Fumble)]
        public void Evaluate_Boundaries(int target, int value, SuccessLevel expected)
        {
            Assert.AreEqual(expected, SkillCheck.Evaluate(value, target));
        }

        [Test]
        public void Evaluate_Perfectionism_DowngradesPlainSuccessOnly()
        {
            Assert.AreEqual(SuccessLevel.Failure, SkillCheck.Evaluate(40, 50, true));
            Assert.AreEqual(SuccessLevel.CriticalSuccess, SkillCheck.Evaluate(10, 50, true));
            Assert.AreEqual(SuccessLevel.Fumble, SkillCheck.Evaluate(99, 50, true));
        }

        [Test]
        public void LegacyStrings_AreTheFourOldValues()
        {
            Assert.AreEqual("Result : Fumble", SkillCheck.ToLegacyString(SuccessLevel.Fumble));
            Assert.AreEqual("Result : Failure", SkillCheck.ToLegacyString(SuccessLevel.Failure));
            Assert.AreEqual("Result : Success", SkillCheck.ToLegacyString(SuccessLevel.Success));
            Assert.AreEqual("Result : Critical Success", SkillCheck.ToLegacyString(SuccessLevel.CriticalSuccess));
        }

        [Test]
        public void ClampTarget_0to90()
        {
            Assert.AreEqual(90, SkillCheck.ClampTarget(99));
            Assert.AreEqual(90, SkillCheck.ClampTarget(90));
            Assert.AreEqual(50, SkillCheck.ClampTarget(50));
            Assert.AreEqual(0, SkillCheck.ClampTarget(-5));
        }

        [Test]
        public void ParseCondition_KnownAndUnknown()
        {
            Assert.AreEqual(Condition.Good, SkillCheck.ParseCondition("Good"));
            Assert.AreEqual(Condition.Best, SkillCheck.ParseCondition("Best"));
            Assert.AreEqual(Condition.Bad, SkillCheck.ParseCondition("Bad"));
            Assert.AreEqual(Condition.Worst, SkillCheck.ParseCondition("Worst"));
            Assert.AreEqual(Condition.Nomal, SkillCheck.ParseCondition("Nomal"));
            Assert.AreEqual(Condition.Nomal, SkillCheck.ParseCondition("???"));
        }

        [Test]
        public void ResolveValue_ZeroIs100()
        {
            var f = new DiceFaces(0, 0, 0, 0, 0, 0);
            Assert.AreEqual(100, SkillCheck.ResolveValue(Condition.Nomal, f));
        }

        // 눈: 1의 자리 7/10의 자리 50, 나쁜 눈 1의 3/10의 80, 좋은 눈 1의 2/10의 20
        [TestCase(Condition.Nomal, 57)]
        [TestCase(Condition.Good, 52)]
        [TestCase(Condition.Best, 27)]
        [TestCase(Condition.Bad, 57)]
        [TestCase(Condition.Worst, 87)]
        public void ResolveValue_ConditionPicksDice(Condition c, int expected)
        {
            var f = new DiceFaces(7, 50, 3, 80, 2, 20);
            Assert.AreEqual(expected, SkillCheck.ResolveValue(c, f));
        }

        [Test]
        public void ResolveValue_BadAndWorst_TakeHigherFace()
        {
            var f = new DiceFaces(2, 30, 9, 70, 5, 10);
            Assert.AreEqual(39, SkillCheck.ResolveValue(Condition.Bad, f));
            Assert.AreEqual(72, SkillCheck.ResolveValue(Condition.Worst, f));
            Assert.AreEqual(32, SkillCheck.ResolveValue(Condition.Good, f));
            Assert.AreEqual(12, SkillCheck.ResolveValue(Condition.Best, f));
        }

        [Test]
        public void Roll_SameSeed_SameResult()
        {
            for (int seed = 0; seed < 50; seed++)
            {
                RollResult a = SkillCheck.Roll(new SeededRng(seed), 60, Condition.Nomal);
                RollResult b = SkillCheck.Roll(new SeededRng(seed), 60, Condition.Nomal);
                Assert.AreEqual(a.Value, b.Value);
                Assert.AreEqual(a.Level, b.Level);
                Assert.AreEqual(SkillCheck.Evaluate(a.Value, 60), a.Level);
            }
        }

        [Test]
        public void Roll_ConsumesSixDrawsInUiOrder()
        {
            var rng = new SeededRng(7);
            DiceFaces f = SkillCheck.RollFaces(rng);
            var rng2 = new SeededRng(7);
            Assert.AreEqual(rng2.Next(0, 10), f.Units);
            Assert.AreEqual(rng2.Next(0, 10) * 10, f.Tens);
            Assert.AreEqual(rng2.Next(0, 10), f.BadUnits);
            Assert.AreEqual(rng2.Next(0, 10) * 10, f.BadTens);
            Assert.AreEqual(rng2.Next(0, 10), f.GoodUnits);
            Assert.AreEqual(rng2.Next(0, 10) * 10, f.GoodTens);
        }

        [Test]
        public void Distribution_Nomal_Target50_About50PercentSuccess()
        {
            var rng = new SeededRng(12345);
            int n = 100000;
            int success = 0;
            int fumble = 0;
            for (int i = 0; i < n; i++)
            {
                RollResult r = SkillCheck.Roll(rng, 50, Condition.Nomal);
                Assert.That(r.Value, Is.InRange(1, 100));
                if (r.IsSuccess) { success++; }
                if (r.Level == SuccessLevel.Fumble) { fumble++; }
            }
            Assert.AreEqual(0.50, success / (double)n, 0.01);
            Assert.AreEqual(0.06, fumble / (double)n, 0.01); // 95~100 = 6%
        }

        [Test]
        public void Distribution_Conditions_OrderedBySuccessRate()
        {
            double Rate(Condition c)
            {
                var rng = new SeededRng(99);
                int ok = 0;
                for (int i = 0; i < 50000; i++)
                {
                    if (SkillCheck.Roll(rng, 55, c).IsSuccess) { ok++; }
                }
                return ok / 50000.0;
            }
            Assert.Greater(Rate(Condition.Best), Rate(Condition.Good));
            Assert.Greater(Rate(Condition.Good), Rate(Condition.Nomal));
            Assert.Greater(Rate(Condition.Nomal), Rate(Condition.Bad));
            Assert.Greater(Rate(Condition.Bad), Rate(Condition.Worst));
        }
    }

    public class DiceExprTests
    {
        [Test]
        public void Parse_Variants()
        {
            DiceExpr e = DiceExpr.Parse("1d6+2");
            Assert.AreEqual(3, e.Min);
            Assert.AreEqual(8, e.Max);
            e = DiceExpr.Parse("3d6*5");
            Assert.AreEqual(15, e.Min);
            Assert.AreEqual(90, e.Max);
            e = DiceExpr.Parse("1D100");
            Assert.AreEqual(1, e.Min);
            Assert.AreEqual(100, e.Max);
            e = DiceExpr.Parse("2d4 - 1");
            Assert.AreEqual(1, e.Min);
            Assert.AreEqual(7, e.Max);
        }

        [TestCase("")]
        [TestCase("d6")]
        [TestCase("1d")]
        [TestCase("abc")]
        [TestCase("0d6")]
        [TestCase("1d6*0")]
        public void TryParse_Rejects(string text)
        {
            Assert.IsFalse(DiceExpr.TryParse(text, out _));
        }

        [Test]
        public void Roll_StaysInRange()
        {
            var rng = new SeededRng(3);
            DiceExpr e = DiceExpr.Parse("3d6*5");
            for (int i = 0; i < 2000; i++)
            {
                int v = e.Roll(rng);
                Assert.That(v, Is.InRange(15, 90));
                Assert.AreEqual(0, v % 5);
            }
        }
    }

    public class SanCheckTests
    {
        [TestCase(0, false, 50, 30, 20)]
        [TestCase(0, true, 50, 50, 0)]
        [TestCase(1, false, 30, 50, 20)]
        [TestCase(1, true, 30, 70, 0)]
        [TestCase(2, false, 20, 60, 20)]
        [TestCase(2, true, 20, 80, 0)]
        [TestCase(3, false, 40, 0, 60)]
        [TestCase(3, true, 100, 0, 0)]
        public void FailurePercents_MatchOldTable(int debuff, bool worst, int a, int b, int c)
        {
            SanCheck.FailurePercents(debuff, worst, out int d, out int i, out int k);
            Assert.AreEqual(a, d);
            Assert.AreEqual(b, i);
            Assert.AreEqual(c, k);
            Assert.AreEqual(100, d + i + k);
        }

        [Test]
        public void ResolveFailure_Boundaries_Debuff0()
        {
            Assert.AreEqual(SanFailOutcome.Decline, SanCheck.ResolveFailure(1, 0, false));
            Assert.AreEqual(SanFailOutcome.Decline, SanCheck.ResolveFailure(50, 0, false));
            Assert.AreEqual(SanFailOutcome.Insanity, SanCheck.ResolveFailure(51, 0, false));
            Assert.AreEqual(SanFailOutcome.Insanity, SanCheck.ResolveFailure(80, 0, false));
            Assert.AreEqual(SanFailOutcome.Deconditioning, SanCheck.ResolveFailure(81, 0, false));
            Assert.AreEqual(SanFailOutcome.Deconditioning, SanCheck.ResolveFailure(100, 0, false));
        }

        [Test]
        public void ResolveFailure_Debuff3_NeverInsanity()
        {
            for (int r = 1; r <= 100; r++)
            {
                SanFailOutcome o = SanCheck.ResolveFailure(r, 3, false);
                Assert.AreEqual(r <= 40 ? SanFailOutcome.Decline : SanFailOutcome.Deconditioning, o);
                Assert.AreEqual(SanFailOutcome.Decline, SanCheck.ResolveFailure(r, 3, true));
            }
        }

        [Test]
        public void ResolveFailure_Worst_NeverDeconditioning()
        {
            for (int debuff = 0; debuff <= 3; debuff++)
            {
                for (int r = 1; r <= 100; r++)
                {
                    Assert.AreNotEqual(SanFailOutcome.Deconditioning, SanCheck.ResolveFailure(r, debuff, true));
                }
            }
        }

        [Test]
        public void ResolveSuccess_Boundaries()
        {
            Assert.AreEqual(SanSuccessOutcome.NoChange, SanCheck.ResolveSuccess(80));
            Assert.AreEqual(SanSuccessOutcome.Rise, SanCheck.ResolveSuccess(81));
            Assert.AreEqual(SanSuccessOutcome.Rise, SanCheck.ResolveSuccess(95));
            Assert.AreEqual(SanSuccessOutcome.RaiseCondition, SanCheck.ResolveSuccess(96));
            Assert.AreEqual(SanSuccessOutcome.RaiseCondition, SanCheck.ResolveSuccess(100));
        }

        [Test]
        public void RollLoss_Is5Or10_About1In3Is10()
        {
            var rng = new SeededRng(5);
            int ten = 0;
            int n = 30000;
            for (int i = 0; i < n; i++)
            {
                int v = SanCheck.RollLoss(rng);
                Assert.IsTrue(v == 5 || v == 10);
                if (v == 10) { ten++; }
            }
            Assert.AreEqual(1.0 / 3, ten / (double)n, 0.02);
        }
    }
}
