using System.IO;
using NUnit.Framework;
using SCoW.Core.State;
using SCoW.Core.Story;
using SCoW.Core.World;
using UnityEngine;

namespace SCoW.Tests
{
    /// <summary>UX-3: 목표 선택 규칙(조건 샘플 5개)·시간 계산·실제 objectives.json 로드.</summary>
    public sealed class ObjectiveCatalogTests
    {
        private const string Json = @"{ ""version"":1, ""rules"":[
            { ""id"":""last"", ""when"":""time>=15"", ""exclusive"":true, ""en"":""LAST"" },
            { ""id"":""a"", ""when"":""!flag:fa && time<8"", ""en"":""A"" },
            { ""id"":""b"", ""when"":""!flag:fb"", ""en"":""B"" },
            { ""id"":""c"", ""when"":""counter:x>=2 || flag:fc"", ""en"":""C"" },
            { ""id"":""fb"", ""fallback"":true, ""en"":""FALLBACK"" } ] }";

        private static ObjectiveCatalog Load()
        {
            ObjectiveCatalogLoadResult r = ObjectiveCatalog.Load(Json);
            Assert.IsTrue(r.Ok, string.Join("|", r.Errors));
            return r.Catalog;
        }

        private static string[] Ids(ObjectiveCatalog c, GameState gs)
        {
            System.Collections.Generic.List<ObjectiveRule> a = c.Active(new GameStateAccess(gs));
            string[] ids = new string[a.Count];
            for (int i = 0; i < ids.Length; i++) { ids[i] = a[i].Id; }
            return ids;
        }

        [Test]
        public void Sample1_Early_PicksFirstMatchInOrder()
        {
            GameState gs = new GameState();
            gs.SetCounter("timeCount", 1);
            Assert.AreEqual(new[] { "a", "b" }, Ids(Load(), gs));
            Assert.AreEqual("a", Load().Current(new GameStateAccess(gs)).Id);
        }

        [Test]
        public void Sample2_FlagCleared_DropsRule()
        {
            GameState gs = new GameState();
            gs.SetCounter("timeCount", 3);
            gs.SetFlag("fa", true);
            Assert.AreEqual(new[] { "b" }, Ids(Load(), gs));
        }

        [Test]
        public void Sample3_TimeLimit_RuleStopsAfterTime8()
        {
            GameState gs = new GameState();
            gs.SetCounter("timeCount", 8);
            Assert.AreEqual(new[] { "b" }, Ids(Load(), gs));
        }

        [Test]
        public void Sample4_Exclusive_DeadlineHidesOthers()
        {
            GameState gs = new GameState();
            gs.SetCounter("timeCount", 15);
            Assert.AreEqual(new[] { "last" }, Ids(Load(), gs));
        }

        [Test]
        public void Sample5_AllDone_UsesFallbackOnlyWhenNothingElse()
        {
            GameState gs = new GameState();
            gs.SetCounter("timeCount", 10);
            gs.SetFlag("fa", true);
            gs.SetFlag("fb", true);
            Assert.AreEqual(new[] { "fb" }, Ids(Load(), gs));
            gs.SetFlag("fc", true);
            Assert.AreEqual(new[] { "c" }, Ids(Load(), gs));
        }

        [Test]
        public void BadData_ReportsErrors()
        {
            Assert.IsFalse(ObjectiveCatalog.Load("{ }").Ok);
            Assert.IsFalse(ObjectiveCatalog.Load(@"{ ""rules"":[ { ""id"":""x"", ""when"":""flag:"", ""en"":""e"" } ] }").Ok);
            Assert.IsFalse(ObjectiveCatalog.Load(@"{ ""rules"":[ { ""id"":""x"", ""en"":""e"" }, { ""id"":""x"", ""en"":""e"" } ] }").Ok);
        }

        [Test]
        public void WorldClock_DayAndDeadline()
        {
            Assert.AreEqual(1, WorldClock.Day(1));
            Assert.AreEqual(1, WorldClock.Day(4));
            Assert.AreEqual(2, WorldClock.Day(5));
            Assert.AreEqual(4, WorldClock.Day(16));
            Assert.AreEqual(15, WorldClock.SlotsLeft(1));
            Assert.AreEqual(0, WorldClock.SlotsLeft(16));
            Assert.AreEqual(0, WorldClock.SlotsLeft(20));
            Assert.AreEqual(1, WorldClock.SlotInDay(1));
            Assert.AreEqual(4, WorldClock.SlotInDay(4));
            Assert.AreEqual(1, WorldClock.SlotInDay(5));
        }

        [Test]
        public void RealObjectivesJson_LoadsAndNewGameHasObjective()
        {
            string path = Path.Combine(Application.dataPath, "Resources/Story/objectives.json");
            ObjectiveCatalogLoadResult r = ObjectiveCatalog.Load(File.ReadAllText(path));
            Assert.IsTrue(r.Ok, string.Join("|", r.Errors));
            GameState gs = GameState.New();
            Assert.IsNotNull(r.Catalog.Current(new GameStateAccess(gs)));
            gs.SetCounter("timeCount", 16);
            Assert.AreEqual("deadline_last_night", r.Catalog.Current(new GameStateAccess(gs)).Id);
        }
    }
}
