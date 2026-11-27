using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.State;
using SCoW.Core.World;

namespace SCoW.Tests
{
    /// <summary>C-2 P13: WorldClock·WorldRules(상태 → 월드 목표)·GameState.Changed. 실제 씬 동치는 WorldSnapshotTool 골든(WorldP13.golden.txt)으로 에디터에서 대조한다.</summary>
    public sealed class WorldRulesTests
    {
        private sealed class FakeScene : IWorldScene
        {
            public Dictionary<string, bool> Active = new Dictionary<string, bool>();
            public Dictionary<string, float[]> Pos = new Dictionary<string, float[]>();
            public Dictionary<string, bool> Judge = new Dictionary<string, bool>();
            public Dictionary<string, bool> Keyword = new Dictionary<string, bool>();
            public float Light = -1f;
            public bool Messy;

            public bool IsActive(string id) { bool v; return Active.TryGetValue(id, out v) && v; }
            public void SetActive(string id, bool active) { Active[id] = active; }
            public void MoveTo(string id, float x, float y, float z) { Pos[id] = new[] { x, y, z }; }
            public float LocalY(string id) { float[] p; return Pos.TryGetValue(id, out p) ? p[1] : 0f; }
            public bool GetCanJudge(string id) { bool v; return Judge.TryGetValue(id, out v) && v; }
            public void SetCanJudge(string id, bool value) { Judge[id] = value; }
            public bool GetCanKeyword(string id) { bool v; return Keyword.TryGetValue(id, out v) && v; }
            public void SetCanKeyword(string id, bool value) { Keyword[id] = value; }
            public void SetLightTime(float time) { Light = time; }
            public void SetClientsHouseRoomMessy() { Messy = true; }
        }

        private static GameState At(int t)
        {
            GameState s = GameState.New();
            s.SetCounter(CounterKeys.timeCount, t);
            return s;
        }

        [TestCase(0, 0, true, 0.9f)]
        [TestCase(1, 1, false, 0.2f)]
        [TestCase(2, 2, false, 0.4f)]
        [TestCase(3, 3, false, 0.64f)]
        [TestCase(4, 0, true, 0.9f)]
        [TestCase(11, 3, false, 0.64f)]
        public void Clock_PhaseNightLight(int t, int phase, bool night, float light)
        {
            Assert.AreEqual(phase, WorldClock.Phase(t));
            Assert.AreEqual(night, WorldClock.IsNight(t));
            float v;
            Assert.IsTrue(WorldClock.TryLightTime(t, out v));
            Assert.AreEqual(light, v);
        }

        [Test]
        public void Clock_NegativeHasNoLight()
        {
            float v;
            Assert.IsFalse(WorldClock.TryLightTime(-1, out v));
        }

        [Test]
        public void NightBulb_OnlyAtNight()
        {
            FakeScene w = new FakeScene();
            WorldRules.Apply(At(4), w);
            Assert.IsTrue(w.IsActive(WorldIds.DetectiveNightBulb));
            WorldRules.Apply(At(5), w);
            Assert.IsFalse(w.IsActive(WorldIds.DetectiveNightBulb));
            Assert.AreEqual(0.2f, w.Light);
        }

        [Test]
        public void SewerDoor_MorningOpensAndLocksJudges()
        {
            FakeScene w = new FakeScene();
            w.Judge[WorldIds.SewerOfficeWindow] = true;
            GameState s = At(1);
            WorldRules.Apply(s, w);
            Assert.IsTrue(s.GetFlag(FlagKeys.sewerDoorOpen));
            Assert.AreEqual(5f, w.Pos[WorldIds.SewerDoorPotal][1]);
            Assert.AreEqual(33f, w.Pos[WorldIds.SewerDoorDialog][1]);
            Assert.AreEqual(4.08f, w.Pos[WorldIds.BlackWellOb][1]);
            Assert.IsFalse(w.GetCanJudge(WorldIds.SewerOfficeWindow));
        }

        [Test]
        public void SewerDoor_EveningClosesByDeftness()
        {
            FakeScene w = new FakeScene();
            GameState s = At(3);
            s.SetFlag(FlagKeys.sewerDoorOpen, true);
            s.SetFlag(FlagKeys.tryBrokenWindow, true);
            WorldRules.Apply(s, w);
            Assert.IsFalse(s.GetFlag(FlagKeys.sewerDoorOpen));
            Assert.AreEqual(33f, w.Pos[WorldIds.SewerDoorPotal][1]);
            Assert.AreEqual(5f, w.Pos[WorldIds.SewerDoorDialog][1]);
            Assert.AreEqual(29.6f, w.Pos[WorldIds.BlackWellOb][1]);
            Assert.IsFalse(w.GetCanJudge(WorldIds.SewerOfficeWindow));
            Assert.IsTrue(w.GetCanJudge(WorldIds.SewerOfficeLocker));
        }

        [Test]
        public void SewerDoor_NightWithClosedDoorDoesNothing()
        {
            // 옛 식 (A) || (B && 문 열림): 밤이어도 문이 닫혀 있으면 두 번째 블록은 건너뛴다.
            FakeScene w = new FakeScene();
            GameState s = At(4);
            WorldRules.Apply(s, w);
            Assert.IsFalse(w.Pos.ContainsKey(WorldIds.BlackWellOb));
        }

        [Test]
        public void SewerDoorOpenCheck_IsConsumed()
        {
            FakeScene w = new FakeScene();
            GameState s = At(4);
            s.SetFlag(FlagKeys.sewerDoorOpenCheck, true);
            WorldRules.Apply(s, w);
            Assert.IsFalse(s.GetFlag(FlagKeys.sewerDoorOpenCheck));
            Assert.AreEqual(5f, w.Pos[WorldIds.SewerDoorPotal][1]);
        }

        [Test]
        public void NoonEvent_AppliesOnceFromDay2()
        {
            FakeScene w = new FakeScene();
            w.Pos[WorldIds.AfNoonEvent] = new[] { 0f, 60f, 0f };
            WorldRules.Apply(At(5), w);
            Assert.IsFalse(w.Messy);
            WorldRules.Apply(At(6), w);
            Assert.IsTrue(w.Messy);
            Assert.AreEqual(0f, w.LocalY(WorldIds.AfNoonEvent));
            Assert.AreEqual(4.06f, w.Pos[WorldIds.PoliceLine][1]);
        }

        [Test]
        public void NoonEvent_DelayedWaitsOneSlot()
        {
            // C-1: 에이든 접촉을 놓쳐 사건이 미뤄졌으면 6 에서는 아직 평소 상태, 7 에서 사건 직후 상태가 된다.
            FakeScene w = new FakeScene();
            w.Pos[WorldIds.AfNoonEvent] = new[] { 0f, 60f, 0f };
            GameState s = At(6);
            s.SetFlag(FlagKeys.noonEventDelayed, true);
            WorldRules.Apply(s, w);
            Assert.IsFalse(w.Messy);
            Assert.AreEqual(60f, w.LocalY(WorldIds.AfNoonEvent));
            s.SetCounter(CounterKeys.timeCount, 7);
            WorldRules.Apply(s, w);
            Assert.IsTrue(w.Messy);
            Assert.AreEqual(0f, w.LocalY(WorldIds.AfNoonEvent));
        }

        [Test]
        public void Witness_RemovedAt7_MeaveJudgeOffAt11()
        {
            FakeScene w = new FakeScene();
            w.Active[WorldIds.Witness] = true;
            WorldRules.Apply(At(7), w);
            Assert.IsFalse(w.IsActive(WorldIds.Witness));
            Assert.IsTrue(w.GetCanJudge(WorldIds.Meave));
            WorldRules.Apply(At(11), w);
            Assert.IsFalse(w.GetCanJudge(WorldIds.Meave));
        }

        [Test]
        public void DeathKeywords_ApplyOnlyAtNight()
        {
            FakeScene w = new FakeScene();
            w.Active[WorldIds.Albert] = true;
            w.Active[WorldIds.Dave] = true;
            GameState s = At(7);
            s.SetFlag(FlagKeys.albertDeathKeyword, true);
            s.SetFlag(FlagKeys.daveDeathKeyword, true);
            WorldRules.Apply(s, w);
            Assert.IsTrue(w.IsActive(WorldIds.Albert));
            Assert.IsTrue(w.IsActive(WorldIds.Dave));
            s.SetCounter(CounterKeys.timeCount, 8);
            WorldRules.Apply(s, w);
            Assert.IsFalse(w.IsActive(WorldIds.Albert));
            Assert.IsFalse(w.IsActive(WorldIds.Dave));
            Assert.IsTrue(s.GetFlag(FlagKeys.thirdDayPoliceADialog));
            Assert.IsTrue(s.GetFlag(FlagKeys.afterDaveDeathFirstSlum));
        }

        [Test]
        public void Apply_IsIdempotent()
        {
            for (int t = 0; t <= 12; t++)
            {
                FakeScene w = new FakeScene();
                w.Active[WorldIds.Witness] = true;
                w.Active[WorldIds.Albert] = true;
                GameState s = At(t);
                s.SetFlag(FlagKeys.albertDeathKeyword, true);
                s.SetFlag(FlagKeys.sewerDoorOpenDeftness, true);
                WorldRules.Apply(s, w);
                GameState after1 = s.Clone();
                Dictionary<string, bool> active1 = new Dictionary<string, bool>(w.Active);
                Dictionary<string, bool> judge1 = new Dictionary<string, bool>(w.Judge);
                WorldRules.Apply(s, w);
                Assert.IsTrue(after1.Equals(s), "t=" + t);
                CollectionAssert.AreEquivalent(active1, w.Active, "t=" + t);
                CollectionAssert.AreEquivalent(judge1, w.Judge, "t=" + t);
            }
        }

        [Test]
        public void Affects_OnlyWorldKeys()
        {
            Assert.IsTrue(WorldRules.Affects(CounterKeys.timeCount));
            Assert.IsTrue(WorldRules.Affects(TextKeys.nowPlace));
            Assert.IsTrue(WorldRules.Affects(FlagKeys.sewerDoorOpen));
            Assert.IsFalse(WorldRules.Affects(FlagKeys.isNightmare));
            Assert.IsFalse(WorldRules.Affects(null));
        }

        [Test]
        public void GameState_Changed_FiresOnlyOnRealChange()
        {
            GameState s = GameState.New();
            List<string> keys = new List<string>();
            s.Changed += k => keys.Add(k);
            s.SetFlag(FlagKeys.sewerDoorOpen, true);
            s.SetFlag(FlagKeys.sewerDoorOpen, true);
            s.SetFlag(FlagKeys.sewerDoorOpen, false);
            s.SetFlag(FlagKeys.sewerDoorOpen, false);
            s.SetCounter(CounterKeys.timeCount, 5);
            s.SetCounter(CounterKeys.timeCount, 5);
            s.SetText(TextKeys.nowPlace, "Bar");
            s.SetText(TextKeys.nowPlace, "Bar");
            CollectionAssert.AreEqual(new[] { FlagKeys.sewerDoorOpen, FlagKeys.sewerDoorOpen, CounterKeys.timeCount, TextKeys.nowPlace }, keys);
        }

        [Test]
        public void GameState_CloneDoesNotCarryHandlers()
        {
            GameState s = GameState.New();
            int n = 0;
            s.Changed += k => n++;
            GameState c = s.Clone();
            c.SetFlag(FlagKeys.sewerDoorOpen, true);
            Assert.AreEqual(0, n);
        }
    }
}
