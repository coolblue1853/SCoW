using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using SCoW.Core.Rules;
using SCoW.Core.State;
using SCoW.Core.Story;
using Condition = SCoW.Core.Rules.Condition;

namespace SCoW.Tests
{
    /// <summary>PT-4 탐색 보조: 보너스 다이스 규칙, 도움 0개 회귀, 칩 후보, 대신 판정 대가, 동료 SAN 자동 판정, 반응 대사 CSV.</summary>
    public sealed class PartyAssistTests
    {
        private sealed class CountingRng : IRng
        {
            public int Draws;
            public int Next(int minInclusive, int maxExclusive)
            {
                Draws++;
                return minInclusive;
            }
        }

        private static GameState WithParty(params CompanionDef[] defs)
        {
            GameState s = GameState.New();
            for (int i = 0; i < defs.Length; i++)
            {
                PartyState.Join(s, defs[i]);
            }
            return s;
        }

        [Test]
        public void RollWithAssist_ZeroDice_SameResultAndRngStateAsRoll()
        {
            Condition[] conditions = { Condition.Nomal, Condition.Good, Condition.Best, Condition.Bad, Condition.Worst };
            for (int seed = 0; seed < 300; seed++)
            {
                foreach (Condition c in conditions)
                {
                    SeededRng a = new SeededRng(seed);
                    SeededRng b = new SeededRng(seed);
                    RollResult x = SkillCheck.Roll(a, 55, c);
                    RollResult y = SkillCheck.RollWithAssist(b, 55, c, 0);
                    Assert.AreEqual(x.Value, y.Value, "seed " + seed + " " + c);
                    Assert.AreEqual(x.Level, y.Level);
                    Assert.AreEqual(a.Next(0, 1000000), b.Next(0, 1000000), "난수 상태가 달라짐 seed " + seed);
                }
            }
        }

        [Test]
        public void RollWithAssist_ConsumesExactlyRollFacesPlusAssistDraws()
        {
            CountingRng r0 = new CountingRng();
            SkillCheck.Roll(r0, 50, Condition.Nomal);
            CountingRng r1 = new CountingRng();
            SkillCheck.RollWithAssist(r1, 50, Condition.Nomal, 1);
            CountingRng r2 = new CountingRng();
            SkillCheck.RollWithAssist(r2, 50, Condition.Nomal, 2);
            CountingRng r9 = new CountingRng();
            SkillCheck.RollWithAssist(r9, 50, Condition.Nomal, 9);
            Assert.AreEqual(6, r0.Draws);
            Assert.AreEqual(7, r1.Draws);
            Assert.AreEqual(8, r2.Draws);
            Assert.AreEqual(8, r9.Draws, "도움은 최대 2개로 클램프");
        }

        [Test]
        public void ResolveValueWithBonus_ZeroAssist_EqualsResolveValue()
        {
            DiceFaces f = new DiceFaces(7, 60, 3, 20, 2, 40);
            foreach (Condition c in new[] { Condition.Nomal, Condition.Good, Condition.Best, Condition.Bad, Condition.Worst })
            {
                Assert.AreEqual(SkillCheck.ResolveValue(c, f), SkillCheck.ResolveValueWithBonus(c, f, 0, null), c.ToString());
            }
        }

        [Test]
        public void ResolveValueWithBonus_TakesLowerTens()
        {
            DiceFaces f = new DiceFaces(5, 60, 9, 90, 0, 40);
            Assert.AreEqual(25, SkillCheck.ResolveValueWithBonus(Condition.Nomal, f, 1, new[] { 20 }));
            Assert.AreEqual(65, SkillCheck.ResolveValueWithBonus(Condition.Nomal, f, 1, new[] { 80 }), "큰 눈은 채택 안 함");
            Assert.AreEqual(5, SkillCheck.ResolveValueWithBonus(Condition.Nomal, f, 2, new[] { 0, 30 }));
            Assert.AreEqual(25, SkillCheck.ResolveValueWithBonus(Condition.Best, f, 1, new[] { 20 }), "Best(GoodTens 40)+도움(20): 보너스 2개");
            Assert.AreEqual(100, SkillCheck.ResolveValueWithBonus(Condition.Nomal, new DiceFaces(0, 50, 0, 0, 0, 0), 1, new[] { 0 }), "00+0 은 100");
        }

        [Test]
        public void ResolveValueWithBonus_AssistCancelsWorstPenaltyDie()
        {
            DiceFaces f = new DiceFaces(5, 60, 9, 90, 0, 40);
            Assert.AreEqual(65, SkillCheck.ResolveValueWithBonus(Condition.Worst, f, 1, new[] { 10 }), "Worst(-1)+도움(+1)=0: 10의 자리 그대로");
            Assert.AreEqual(35, SkillCheck.ResolveValueWithBonus(Condition.Worst, f, 2, new[] { 30, 10 }), "순 보너스 1개: 첫 추가 눈만");
        }

        [Test]
        public void SuccessChance_AssistRaisesChance_AndMatchesSimulation()
        {
            foreach (int target in new[] { 20, 50, 75 })
            {
                double none = SkillCheck.SuccessChance(Condition.Nomal, target);
                double one = SkillCheck.SuccessChance(Condition.Nomal, target, false, 1);
                double two = SkillCheck.SuccessChance(Condition.Nomal, target, false, 2);
                Assert.Greater(one, none, "target " + target);
                Assert.Greater(two, one, "target " + target);
            }
            SeededRng rng = new SeededRng(4242);
            int hits = 0;
            const int n = 60000;
            for (int i = 0; i < n; i++)
            {
                if (SkillCheck.RollWithAssist(rng, 50, Condition.Nomal, 1).IsSuccess)
                {
                    hits++;
                }
            }
            Assert.AreEqual(SkillCheck.SuccessChance(Condition.Nomal, 50, false, 1), hits / (double)n, 0.01);
        }

        [Test]
        public void SuccessPercent_ReflectsAssist_AndDefaultUnchanged()
        {
            Assert.AreEqual(50, SkillCheck.SuccessPercent(Condition.Nomal, 50));
            Assert.AreEqual(50, SkillCheck.SuccessPercent(Condition.Nomal, 50, false, 0));
            Assert.AreEqual(SkillCheck.PercentOf(SkillCheck.SuccessChance(Condition.Nomal, 50, false, 1)), SkillCheck.SuccessPercent(Condition.Nomal, 50, false, 1));
            Assert.Greater(SkillCheck.SuccessPercent(Condition.Nomal, 50, false, 1), 50);
        }

        [Test]
        public void Options_AssistNeeds40_SwapNeedsFabianPlus10()
        {
            GameState s = WithParty(CompanionDefs.Albert, CompanionDefs.Maeve);
            List<AssistOption> o = PartyAssist.Options(s, "dialog", "Rhetoric", 60);
            Assert.AreEqual(3, o.Count, "알버트 도움(50), 메이브 도움(70)+대신(70>=70)");
            Assert.AreEqual(AssistMode.Assist, o[0].Mode);
            Assert.AreEqual(CompanionDefs.AlbertId, o[0].Def.Id);
            Assert.AreEqual(AssistMode.Assist, o[1].Mode);
            Assert.AreEqual(AssistMode.Swap, o[2].Mode);
            Assert.AreEqual(1, PartyAssist.Options(s, "dialog", "Occult", 10).Count, "오컬트 알버트 5 / 메이브 20: 둘 다 도움 불가, 메이브만 대신(20>=20)");
            Assert.AreEqual(0, PartyAssist.Options(s, "dialog", "Occult", 30).Count);
        }

        [Test]
        public void Options_NotEligible_Empty()
        {
            GameState s = WithParty(CompanionDefs.Albert, CompanionDefs.Maeve);
            Assert.AreEqual(0, PartyAssist.Options(s, "MS_attack", "Rhetoric", 10).Count, "전투");
            Assert.AreEqual(0, PartyAssist.Options(s, "dialog", "Sanity", 10).Count, "SAN");
            Assert.AreEqual(0, PartyAssist.Options(s, "dialog", "Luck", 10).Count, "행운");
            Assert.AreEqual(0, PartyAssist.Options(s, "dialog", "Stealth", 10).Count, "은신은 단체 판정");
            Assert.AreEqual(0, PartyAssist.Options(GameState.New(), "dialog", "Rhetoric", 10).Count, "합류 없음");
            Assert.AreEqual(0, PartyAssist.Options(null, "dialog", "Rhetoric", 10).Count);
        }

        [Test]
        public void Options_ExcludesDeadStandbyAndDowned()
        {
            GameState s = WithParty(CompanionDefs.Albert, CompanionDefs.Maeve);
            PartyState.Kill(s, CompanionDefs.AlbertId);
            PartyState.SetStatus(s, CompanionDefs.MaeveId, UnitStatus.Downed);
            Assert.AreEqual(0, PartyAssist.Options(s, "dialog", "Rhetoric", 10).Count);
            PartyState.SetStatus(s, CompanionDefs.MaeveId, UnitStatus.Ready);
            Assert.AreEqual(2, PartyAssist.Options(s, "dialog", "Rhetoric", 10).Count);
        }

        [Test]
        public void Options_StrengthUsesStatNotSkill()
        {
            GameState s = WithParty(CompanionDefs.Albert);
            List<AssistOption> o = PartyAssist.Options(s, "dialog", "Str", 50);
            Assert.AreEqual(2, o.Count);
            Assert.AreEqual(65, o[1].Skill);
            Assert.AreEqual(AssistMode.Swap, o[1].Mode);
        }

        [Test]
        public void SwapPenalty_FailureAndFumble_NeverBelowOneHp()
        {
            GameState s = WithParty(CompanionDefs.Albert);
            Assert.AreEqual(0, PartyAssist.ApplySwapPenalty(s, CompanionDefs.Albert, SuccessLevel.Success));
            Assert.AreEqual(1, PartyAssist.ApplySwapPenalty(s, CompanionDefs.Albert, SuccessLevel.Failure));
            Assert.AreEqual(3, PartyAssist.ApplySwapPenalty(s, CompanionDefs.Albert, SuccessLevel.Fumble));
            Assert.AreEqual(CompanionDefs.Albert.MaxHp - 4, PartyState.GetHp(s, CompanionDefs.AlbertId));
            PartyState.SetHp(s, CompanionDefs.Albert, 2);
            Assert.AreEqual(1, PartyAssist.ApplySwapPenalty(s, CompanionDefs.Albert, SuccessLevel.Fumble));
            Assert.AreEqual(1, PartyState.GetHp(s, CompanionDefs.AlbertId));
            Assert.AreEqual(0, PartyAssist.ApplySwapPenalty(s, CompanionDefs.Albert, SuccessLevel.Fumble));
        }

        [Test]
        public void AutoSan_NoCompanions_ConsumesNoRandomness()
        {
            CountingRng rng = new CountingRng();
            Assert.AreEqual(0, PartyAssist.AutoSanChecks(GameState.New(), rng).Count);
            Assert.AreEqual(0, rng.Draws);
            Assert.AreEqual(0, PartyAssist.AutoSanChecks(null, rng).Count);
        }

        [Test]
        public void AutoSan_FailureAppliesLossToEachCompanion()
        {
            GameState s = WithParty(CompanionDefs.Albert, CompanionDefs.Maeve);
            CountingRng rng = new CountingRng();
            List<SanAutoResult> r = PartyAssist.AutoSanChecks(s, rng);
            Assert.AreEqual(2, r.Count);
            for (int i = 0; i < r.Count; i++)
            {
                Assert.IsFalse(r[i].Success, "굴림이 전부 0 이면 100(대실패)");
                Assert.AreEqual(5, r[i].Loss);
            }
            Assert.AreEqual(CompanionDefs.Albert.MaxSan - 5, PartyState.GetSan(s, CompanionDefs.AlbertId));
            Assert.AreEqual(CompanionDefs.Maeve.MaxSan - 5, PartyState.GetSan(s, CompanionDefs.MaeveId));
            Assert.AreEqual(2 * 7, rng.Draws, "동료당 판정 6 + 손실 1");
        }

        [Test]
        public void AutoSan_Success_LosesNothing_AndIsSeedDeterministic()
        {
            GameState a = WithParty(CompanionDefs.Albert, CompanionDefs.Maeve);
            GameState b = WithParty(CompanionDefs.Albert, CompanionDefs.Maeve);
            List<SanAutoResult> ra = PartyAssist.AutoSanChecks(a, new SeededRng(7));
            List<SanAutoResult> rb = PartyAssist.AutoSanChecks(b, new SeededRng(7));
            for (int i = 0; i < ra.Count; i++)
            {
                Assert.AreEqual(ra[i].Roll, rb[i].Roll);
                Assert.AreEqual(ra[i].Loss, rb[i].Loss);
                if (ra[i].Success)
                {
                    Assert.AreEqual(0, ra[i].Loss);
                }
            }
        }

        [Test]
        public void SharedSanSubjects_OnlySewerAndBattle()
        {
            Assert.IsTrue(PartyAssist.IsSharedSanSubject("Sewer_FirstInSan"));
            Assert.IsTrue(PartyAssist.IsSharedSanSubject("DeepOneFirstSan"));
            Assert.IsTrue(PartyAssist.IsSharedSanSubject("BattleSan_DeepOne1"));
            Assert.IsFalse(PartyAssist.IsSharedSanSubject("dialog"));
            Assert.IsFalse(PartyAssist.IsSharedSanSubject("Fab_Case1"));
            Assert.IsFalse(PartyAssist.IsSharedSanSubject(null));
        }

        [Test]
        public void PartyCsv_HasEveryReactionLine()
        {
            string path = "Assets/Script/Asset/Resources/Party.csv";
            StoryCsvResult csv = StoryCsvParser.Parse(File.ReadAllText(path));
            Assert.IsTrue(csv.Ok, string.Join("; ", csv.Errors));
            HashSet<string> ids = new HashSet<string>();
            for (int i = 0; i < CompanionDefs.All.Length; i++)
            {
                foreach (ReactionKind kind in new[] { ReactionKind.Assist, ReactionKind.Swap, ReactionKind.San })
                {
                    foreach (bool ok in new[] { true, false })
                    {
                        string id = PartyAssist.ReactionId(CompanionDefs.All[i].Id, kind, ok);
                        Assert.IsTrue(ids.Add(id), "중복 ID " + id);
                        Assert.IsNotEmpty(PartyAssist.FindReactionText(csv.Lines, id), CompanionDefs.All[i].Id + " " + kind + " " + ok);
                    }
                }
            }
            Assert.IsNull(PartyAssist.ReactionId("nobody", ReactionKind.Assist, true));
        }
    }
}
