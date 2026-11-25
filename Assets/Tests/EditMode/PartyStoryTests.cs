using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.Rules;
using SCoW.Core.State;
using SCoW.Core.Story;
using SCoW.Core.World;

namespace SCoW.Tests
{
    /// <summary>PT-3: 동료 합류(키워드 제시 → join 효과), NPC 자리 숨김, 엔딩 변주·albertDead 합집합, 구세이브 기본값.</summary>
    public sealed class PartyStoryTests
    {
        private sealed class FakeScene : IWorldScene
        {
            public Dictionary<string, bool> Active = new Dictionary<string, bool>();
            public bool IsActive(string id) { bool v; return Active.TryGetValue(id, out v) && v; }
            public void SetActive(string id, bool active) { Active[id] = active; }
            public void MoveTo(string id, float x, float y, float z) { }
            public float LocalY(string id) { return 0f; }
            public bool GetCanJudge(string id) { return false; }
            public void SetCanJudge(string id, bool value) { }
            public bool GetCanKeyword(string id) { return false; }
            public void SetCanKeyword(string id, bool value) { }
            public void SetLightTime(float time) { }
            public void SetClientsHouseRoomMessy() { }
        }

        private static GameState At(int t)
        {
            GameState s = GameState.New();
            s.SetCounter(CounterKeys.timeCount, t);
            return s;
        }

        private static StoryResolution Run(string script, string topic, GameState s)
        {
            GameStateAccess a = new GameStateAccess(s);
            List<string> errors = new List<string>();
            StoryResolution r = StoryResolver.Resolve(DialogPilotEquivalenceTests.Script(script), topic, a, a, DialogPilotEquivalenceTests.Csv, errors);
            Assert.AreEqual(0, errors.Count, string.Join("|", errors));
            return r;
        }

        [Test]
        public void OldSave_DefaultsToNoCompanion()
        {
            GameState s = At(9);
            Assert.IsFalse(PartyState.IsJoined(s, CompanionDefs.AlbertId));
            Assert.IsFalse(PartyState.IsJoined(s, CompanionDefs.MaeveId));
            Assert.AreEqual(0, PartyState.ActiveCompanions(s).Count);
        }

        [Test]
        public void Albert_JoinsOnKeyword_OnDay3()
        {
            GameState s = At(9);
            StoryResolution r = Run("Keyword", "Albert Bradley/Location", s);
            Assert.AreEqual("albertKeyJoin", r.NodeId);
            Assert.IsTrue(PartyState.IsJoined(s, CompanionDefs.AlbertId));
            Assert.AreEqual(CompanionDefs.Albert.MaxHp, PartyState.GetHp(s, CompanionDefs.AlbertId));
            Assert.AreEqual("fabian,albert", s.GetText(PartyKeys.Order));
        }

        [TestCase(8)]
        [TestCase(5)]
        public void Albert_NoJoinBeforeDay3(int t)
        {
            GameState s = At(t);
            Assert.AreEqual("albertKeyNothing", Run("Keyword", "Albert Bradley/Location", s).NodeId);
            Assert.IsFalse(PartyState.IsJoined(s, CompanionDefs.AlbertId));
        }

        [Test]
        public void Albert_NoJoinAfterDeathKeyword()
        {
            GameState s = At(9);
            s.SetFlag(FlagKeys.albertDeathKeyword, true);
            Assert.AreEqual("albertKeyNothing", Run("Keyword", "Albert Bradley/Location", s).NodeId);
            Assert.IsFalse(PartyState.IsJoined(s, CompanionDefs.AlbertId));
        }

        [Test]
        public void Maeve_JoinsFromT10_NotT9_AndKeepsOrder()
        {
            GameState s = At(9);
            Assert.AreEqual("maeveKeyNothing", Run("Keyword", "Maeve/Location", s).NodeId);
            s.SetCounter(CounterKeys.timeCount, 10);
            Run("Keyword", "Albert Bradley/Location", s);
            Assert.AreEqual("maeveKeyJoin", Run("Keyword", "Maeve/Location", s).NodeId);
            Assert.AreEqual("fabian,albert,maeve", s.GetText(PartyKeys.Order));
        }

        [Test]
        public void JoinEffect_IsIdempotent_AndRejectsUnknownId()
        {
            GameState s = At(9);
            Run("Keyword", "Albert Bradley/Location", s);
            PartyState.SetHp(s, CompanionDefs.Albert, 3);
            GameStateAccess a = new GameStateAccess(s);
            new JoinParty(CompanionDefs.AlbertId).Apply(a, a, null);
            Assert.AreEqual(3, PartyState.GetHp(s, CompanionDefs.AlbertId));
            Assert.IsNotNull(EffectParser.Parse("join:nobody").Error);
            Assert.IsNull(EffectParser.Parse("join:maeve").Error);
        }

        [Test]
        public void WorldRules_HidesJoinedNpcs_AndSkipsAlbertNightRule()
        {
            FakeScene w = new FakeScene();
            w.Active[WorldIds.Albert] = true;
            w.Active[WorldIds.MeaveNpc] = true;
            GameState s = At(12);
            WorldRules.Apply(s, w);
            Assert.IsTrue(w.IsActive(WorldIds.Albert));
            Assert.IsTrue(w.IsActive(WorldIds.MeaveNpc));
            PartyState.Join(s, CompanionDefs.Albert);
            s.SetFlag(FlagKeys.albertDeathKeyword, true);
            WorldRules.Apply(s, w);
            Assert.IsFalse(w.IsActive(WorldIds.Albert));
            Assert.IsTrue(w.IsActive(WorldIds.MeaveNpc));
            Assert.IsFalse(s.GetFlag(FlagKeys.thirdDayPoliceADialog), "합류 시 밤 소멸 부수효과 건너뜀");
            PartyState.Join(s, CompanionDefs.Maeve);
            WorldRules.Apply(s, w);
            Assert.IsFalse(w.IsActive(WorldIds.MeaveNpc));
        }

        [Test]
        public void WorldRules_Affects_JoinedKeys()
        {
            Assert.IsTrue(WorldRules.Affects(PartyKeys.Joined("albert")));
            Assert.IsTrue(WorldRules.Affects(PartyKeys.Joined("maeve")));
            Assert.IsFalse(WorldRules.Affects(PartyKeys.Hp("albert")));
        }

        private static string Ending(GameState s)
        {
            return Run("InSewer", "Ending_isPromise", s).NodeId;
        }

        [Test]
        public void Ending_Original_WithoutParty()
        {
            GameState s = At(14);
            s.SetFlag(FlagKeys.intelMeiv2, true);
            Assert.AreEqual("Ending_isPromise.0", Ending(s));
            s.SetFlag(FlagKeys.albertDeathKeyword, true);
            Assert.AreEqual("Ending_isPromise.1", Ending(s));
            s.SetFlag(FlagKeys.intelMeiv2, false);
            Assert.AreEqual("Ending_isPromise.2", Ending(s));
        }

        [Test]
        public void Ending_AlbertDead_UnionWithPartyDeath()
        {
            GameState s = At(14);
            s.SetFlag(FlagKeys.intelMeiv2, true);
            PartyState.Kill(s, CompanionDefs.AlbertId);
            Assert.AreEqual("Ending_isPromise.1", Ending(s));
        }

        [Test]
        public void Ending_PartyVariants()
        {
            GameState s = At(14);
            s.SetFlag(FlagKeys.intelMeiv2, true);
            PartyState.Join(s, CompanionDefs.Albert);
            Assert.AreEqual("Ending_PartyAlbert", Ending(s));
            PartyState.Kill(s, CompanionDefs.AlbertId);
            PartyState.Join(s, CompanionDefs.Maeve);
            Assert.AreEqual("Ending_PartyMaeve", Ending(s));
            PartyState.Kill(s, CompanionDefs.MaeveId);
            Assert.AreEqual("Ending_isPromise.1", Ending(s));
        }

        [Test]
        public void Ending_MaeveDead_AlbertAlive()
        {
            GameState s = At(14);
            s.SetFlag(FlagKeys.intelMeiv2, true);
            PartyState.Join(s, CompanionDefs.Maeve);
            PartyState.Kill(s, CompanionDefs.MaeveId);
            Assert.AreEqual("Ending_PartyMaeveDead", Ending(s));
        }
    }
}
