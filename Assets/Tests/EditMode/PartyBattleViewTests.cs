using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.Rules;
using SCoW.Core.State;
using SCoW.Core.Ui;

namespace SCoW.Tests
{
    /// <summary>PT-5·UX-11: BattleEvent 재생 순서 규칙(BattleCueBuilder), 툴팁 예상 성공률(PreviewAttack), 솔로 회귀.</summary>
    public sealed class PartyBattleViewTests
    {
        private sealed class FixedRng : IRng
        {
            private readonly int _value;
            public FixedRng(int value) { _value = value; }
            public int Next(int minInclusive, int maxExclusive)
            {
                return _value < minInclusive ? minInclusive : (_value >= maxExclusive ? maxExclusive - 1 : _value);
            }
        }

        private static PartyMember Fabian(int martial = 50)
        {
            Dictionary<string, int> skills = new Dictionary<string, int>();
            for (int i = 0; i < CompanionDefs.SkillKeys.Length; i++)
            {
                skills[CompanionDefs.SkillKeys[i]] = 20;
            }
            skills[CounterKeys.martialArtsPoint] = martial;
            return PartyMember.CreateLeader("fabian", 52, 52, 50, 50, 52, 52, skills, new string[0]);
        }

        private static EnemyUnit Enemy(string id, int hp = 100)
        {
            EnemyState s = new EnemyState(EnemyDefs.DeepOneHybrid1);
            s.Dex = 50;
            s.FullHp = hp;
            s.NowHp = hp;
            s.San = 40;
            s.MartialArts = 50;
            s.Evasion = 25;
            return new EnemyUnit(s, id);
        }

        private static BattleEvent[] Ev(params BattleEvent[] events)
        {
            return events;
        }

        [Test]
        public void Cues_HitPlaysLungeThenFlashThenNumbersTogether_DownComesAfter()
        {
            BattleEvent[] events = Ev(
                new BattleEvent(BattleEventKind.Hit, "e1", "fabian", 4, BattleDetail.None, 2, 1, 1, 46, 50),
                new BattleEvent(BattleEventKind.Down, "e1", "fabian", 3, BattleDetail.None, -1, -1, 1, 0, 50));
            List<BattleCue> cues = BattleCueBuilder.Build(events);
            int lunge = cues.FindIndex(c => c.Kind == BattleCueKind.Lunge);
            int hit = cues.FindIndex(c => c.Kind == BattleCueKind.Hit);
            int popup = cues.FindIndex(c => c.Kind == BattleCueKind.Popup);
            int bars = cues.FindIndex(c => c.Kind == BattleCueKind.Bars);
            int down = cues.FindIndex(c => c.Kind == BattleCueKind.Down);
            Assert.Less(lunge, hit);
            Assert.Less(hit, popup);
            Assert.Less(hit, bars);
            Assert.Less(popup, down);
            Assert.Less(bars, down);
            Assert.IsTrue(cues[popup].Parallel && cues[bars].Parallel, "숫자·막대는 번쩍과 같은 박자");
            Assert.AreEqual(46, cues[bars].Hp);
            Assert.AreEqual("-4", cues[popup].Text);
        }

        [Test]
        public void Cues_KeepEventOrder_SourceNeverDecreases()
        {
            BattleEvent[] events = Ev(
                new BattleEvent(BattleEventKind.Turn, "fabian", "fabian"),
                new BattleEvent(BattleEventKind.Hit, "fabian", "e1", 6, BattleDetail.None, 2, 1, 0, 94, -1),
                new BattleEvent(BattleEventKind.EnemyDown, "fabian", "e1"),
                new BattleEvent(BattleEventKind.Compact, "e2", "e2", 1, BattleDetail.None, -1, -1, 0),
                new BattleEvent(BattleEventKind.Compact, "e3", "e3", 2, BattleDetail.None, -1, -1, 1),
                new BattleEvent(BattleEventKind.Miss, "e2", "albert", 0, BattleDetail.None, 1, 2));
            List<BattleCue> cues = BattleCueBuilder.Build(events);
            for (int i = 1; i < cues.Count; i++)
            {
                Assert.GreaterOrEqual(cues[i].Source, cues[i - 1].Source, "cue " + i);
            }
            Assert.AreEqual(BattleCueKind.Banner, cues[0].Kind);
            int remove = cues.FindIndex(c => c.Kind == BattleCueKind.Remove);
            int firstMove = cues.FindIndex(c => c.Kind == BattleCueKind.Move);
            Assert.Less(remove, firstMove, "처치 뒤에 빈 칸 압축");
        }

        [Test]
        public void Cues_ConsecutiveCompactsShareOneBeat()
        {
            BattleEvent[] events = Ev(
                new BattleEvent(BattleEventKind.Compact, "e2", "e2", 1, BattleDetail.None, -1, -1, 0),
                new BattleEvent(BattleEventKind.Compact, "e3", "e3", 2, BattleDetail.None, -1, -1, 1));
            List<BattleCue> moves = BattleCueBuilder.Build(events).FindAll(c => c.Kind == BattleCueKind.Move);
            Assert.AreEqual(2, moves.Count);
            Assert.IsFalse(moves[0].Parallel);
            Assert.IsTrue(moves[1].Parallel);
            Assert.AreEqual(0, moves[0].Rank);
            Assert.AreEqual(1, moves[1].Rank);
        }

        [Test]
        public void Cues_MissShowsPopupNotBars_LogLineRidesFirstCue()
        {
            BattleEvent[] events = Ev(new BattleEvent(BattleEventKind.Miss, "fabian", "e1", 0, BattleDetail.None, 3, 2));
            List<BattleCue> cues = BattleCueBuilder.Build(events, id => id.ToUpperInvariant());
            Assert.IsFalse(cues.Exists(c => c.Kind == BattleCueKind.Bars || c.Kind == BattleCueKind.Hit));
            BattleCue text = cues.Find(c => c.Kind == BattleCueKind.Text);
            Assert.IsTrue(text.Parallel);
            Assert.AreEqual("FABIAN misses E1 (CriticalSuccess)", text.Text);
        }

        [Test]
        public void Cues_SwapMovesBothUnitsInOneBeat()
        {
            BattleEvent[] events = Ev(new BattleEvent(BattleEventKind.Swap, "albert", "fabian", 1, BattleDetail.None, -1, -1, 0));
            List<BattleCue> moves = BattleCueBuilder.Build(events).FindAll(c => c.Kind == BattleCueKind.Move);
            Assert.AreEqual("albert", moves[0].UnitId);
            Assert.AreEqual(1, moves[0].Rank);
            Assert.AreEqual("fabian", moves[1].UnitId);
            Assert.AreEqual(0, moves[1].Rank);
            Assert.IsTrue(moves[1].Parallel);
        }

        [Test]
        public void Cues_FromRealResolve_NoDownBeforeItsHit()
        {
            PartyMember fabian = Fabian();
            PartyMember albert = PartyMember.FromCompanion(CompanionDefs.Albert, 1, 55);
            BattleField f = new BattleField(new[] { albert, fabian }, new[] { Enemy("e1") });
            List<BattleEvent> events = new List<BattleEvent>();
            EnemyUnit e = (EnemyUnit)f.At(Side.Enemy, 0);
            f.Resolve(new BattleCommand(e, ActionKind.Melee, 0), new FixedRng(0), events);
            List<BattleCue> cues = BattleCueBuilder.Build(events);
            int down = cues.FindIndex(c => c.Kind == BattleCueKind.Down);
            if (down >= 0)
            {
                Assert.Less(cues.FindIndex(c => c.Kind == BattleCueKind.Hit), down);
            }
            Assert.IsTrue(cues.Count > 0);
        }

        [Test]
        public void Preview_MeleePercentEqualsRolletPercent()
        {
            PartyMember fabian = Fabian(50);
            BattleField f = new BattleField(new[] { fabian }, new[] { Enemy("e1") });
            AttackPreview p = f.PreviewAttack(fabian, ActionKind.Melee, 0);
            Assert.IsTrue(p.Valid);
            Assert.AreEqual(SkillCheck.SuccessPercent(Condition.Nomal, 50), p.Percent);
            Assert.AreEqual(50, p.DefenseSkill);
        }

        [Test]
        public void Preview_RangedFromFrontIsPenalized_InvalidTargetsAreNotValid()
        {
            Dictionary<string, int> skills = new Dictionary<string, int>();
            for (int i = 0; i < CompanionDefs.SkillKeys.Length; i++)
            {
                skills[CompanionDefs.SkillKeys[i]] = 20;
            }
            skills[CounterKeys.gunShotPoint] = 60;
            PartyMember shooter = PartyMember.CreateLeader("fabian", 52, 52, 50, 50, 52, 52, skills, new[] { "Revolver" });
            BattleField f = new BattleField(new[] { shooter }, new[] { Enemy("e1"), Enemy("e2") });
            AttackPreview p = f.PreviewAttack(shooter, ActionKind.Ranged, 1);
            Assert.IsTrue(p.Valid);
            Assert.AreEqual(Condition.Worst, p.Condition);
            Assert.AreEqual(SkillCheck.SuccessPercent(Condition.Worst, 60), p.Percent);
            Assert.AreEqual(25, p.DefenseSkill);
            Assert.IsFalse(f.PreviewAttack(shooter, ActionKind.Melee, 2).Valid, "빈 칸");
            Assert.IsFalse(f.PreviewAttack(shooter, ActionKind.Guard, 0).Valid);
        }

        [Test]
        public void Tooltip_ShowsHitChanceOnlyWithPreview_AndClampsInsideScreen()
        {
            PartyMember fabian = Fabian(50);
            EnemyUnit e = Enemy("e1");
            BattleField f = new BattleField(new[] { fabian }, new[] { e });
            string plain = BattleTooltipText.ForUnit(e);
            StringAssert.DoesNotContain("Hit chance", plain);
            StringAssert.Contains("HP 100/100", plain);
            string withHit = BattleTooltipText.ForUnit(e, f.PreviewAttack(fabian, ActionKind.Melee, 0));
            StringAssert.Contains("Hit chance: " + SkillCheck.SuccessPercent(Condition.Nomal, 50) + "%", withHit);
            float x;
            float y;
            BattleTooltipText.Place(1900f, 500f, 450f, 300f, 100f, 1920f, 1080f, out x, out y);
            Assert.AreEqual(1620f, x);
            Assert.AreEqual(350f, y);
            BattleTooltipText.Place(10f, 80f, 20f, 300f, 100f, 1920f, 1080f, out x, out y);
            Assert.AreEqual(0f, x);
            Assert.AreEqual(80f, y, "아래가 모자라면 앵커 위쪽");
            // 옆 배치: 오른쪽 우선, 세로는 앵커 위끝 맞춤, 바닥 아래로 못 나감
            BattleTooltipText.PlaceBeside(40f, 340f, 90f, 400f, 120f, 1920f, 1080f, out x, out y);
            Assert.AreEqual(352f, x);
            Assert.AreEqual(0f, y, "위끝 맞춤이 바닥 아래면 0");
            BattleTooltipText.PlaceBeside(1500f, 1800f, 600f, 400f, 120f, 1920f, 1080f, out x, out y);
            Assert.AreEqual(1088f, x, "오른쪽이 모자라면 왼쪽");
            Assert.AreEqual(480f, y);
        }

        [Test]
        public void Solo_NoCompanions_FieldBehavesAsOneVsOne()
        {
            PartyMember fabian = Fabian();
            BattleField f = new BattleField(new[] { fabian }, new[] { Enemy("e1") });
            Assert.AreEqual(1, f.CountOn(Side.Ally));
            Assert.IsFalse(f.LeaderDownRule, "솔로는 HP 0 즉시 패배(옛 경로 불변)");
            Assert.AreSame(fabian, f.At(Side.Ally, 0));
            Assert.AreEqual(1, f.Order().FindAll(u => u.Side == Side.Ally).Count);
        }
    }
}
