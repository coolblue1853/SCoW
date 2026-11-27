using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.State;
using SCoW.Core.Story;

namespace SCoW.Tests
{
    /// <summary>스토리 P1 C-1(사건 미루기)·C-4(문 경찰 "알버트가 보냈다") 규칙과 데이터.</summary>
    public sealed class NoonEventRulesTests
    {
        // C-1: 사건 칸 = 6, 에이든 접촉을 놓치고 아침(5)을 맞으면 7.
        [Test]
        public void EventSlot_NormalIs6_DelayedIs7()
        {
            Assert.IsFalse(NoonEventRules.ShouldDelay(true));
            Assert.IsTrue(NoonEventRules.ShouldDelay(false));
            Assert.AreEqual(6, NoonEventRules.EventSlot(false));
            Assert.AreEqual(7, NoonEventRules.EventSlot(true));
        }

        // 옛 식 `time>=6 && (flag || time>=7)` 와의 대응: 이동 후 시각 t 에서 강제 이동은 t-1 에서 발동한다.
        [TestCase(false, 5, true)]
        [TestCase(false, 6, false)]
        [TestCase(false, 4, false)]
        [TestCase(true, 5, false)]
        [TestCase(true, 6, true)]
        [TestCase(true, 7, false)]
        public void ForcedDeparture_FollowsSlot(bool delayed, int t, bool expected)
        {
            Assert.AreEqual(expected, NoonEventRules.IsForcedDeparture(t, true, delayed));
            Assert.IsFalse(NoonEventRules.IsForcedDeparture(t, false, delayed), "아침 지도 이동을 거치지 않았으면 강제하지 않는다");
        }

        [Test]
        public void EventTime_And_AfterSlot()
        {
            Assert.IsFalse(NoonEventRules.IsEventTime(5, false));
            Assert.IsTrue(NoonEventRules.IsEventTime(6, false));
            Assert.IsFalse(NoonEventRules.IsEventTime(6, true));
            Assert.IsTrue(NoonEventRules.IsEventTime(7, true));
            Assert.IsFalse(NoonEventRules.IsAfterEventSlot(6, false));
            Assert.IsTrue(NoonEventRules.IsAfterEventSlot(7, false));
            Assert.IsFalse(NoonEventRules.IsAfterEventSlot(7, true));
            Assert.IsTrue(NoonEventRules.IsAfterEventSlot(8, true));
        }

        // C-4: 문 경찰 앞 선택지.
        [Test]
        public void DoorPoliceOptions_Table()
        {
            Assert.AreEqual(DoorPoliceOption.Persuade, NoonEventRules.DoorPoliceOptions(false, false, false), "평소: 설득 판정만(옛 흐름)");
            Assert.AreEqual(DoorPoliceOption.Persuade | DoorPoliceOption.AlbertSent, NoonEventRules.DoorPoliceOptions(false, true, false));
            Assert.AreEqual(DoorPoliceOption.AlbertSent, NoonEventRules.DoorPoliceOptions(false, true, true), "설득 실패 뒤에도 알버트 경로는 남는다");
            Assert.AreEqual(DoorPoliceOption.None, NoonEventRules.DoorPoliceOptions(false, false, true), "설득 실패 + 알버트 정보 없음: 방법 없음");
            Assert.AreEqual(DoorPoliceOption.None, NoonEventRules.DoorPoliceOptions(true, true, false), "이미 입장 권한이 있으면 없다");
        }

        private static StoryResolution ResolveNoon(string topic, GameState gs)
        {
            StoryScript sc = DialogPilotEquivalenceTests.Script("2nd_NoonEvent");
            GameStateAccess acc = new GameStateAccess(gs);
            List<string> errors = new List<string>();
            StoryResolution res = StoryResolver.Resolve(sc, topic, acc, acc, DialogPilotEquivalenceTests.Csv, errors);
            Assert.AreEqual(0, errors.Count, string.Join(" | ", errors));
            return res;
        }

        [Test]
        public void AlbertSentTopic_PlaysLinesAndGrantsAccess()
        {
            GameState gs = GameState.New();
            gs.SetFlag("intelMissingPeople2", true);
            Assert.IsFalse(gs.GetFlag(FlagKeys.noonEventAccessAuthorization));
            StoryResolution res = ResolveNoon("NoonEvent_DoorPolice_AlbertSent", gs);
            Assert.IsNotNull(res);
            Assert.AreEqual(6, res.Lines.Count);
            Assert.AreEqual("276", res.Lines[0].Id);
            Assert.AreEqual("281", res.Lines[5].Id);
            Assert.AreEqual("2nd_NoonEvent", res.Lines[0].File);
            IList<StoryLine> csv = DialogPilotEquivalenceTests.Csv("2nd_NoonEvent");
            bool found = false;
            for (int i = 0; i < csv.Count; i++)
            {
                found |= csv[i].Id == "276" && csv[i].Text.Contains("Albert sent me");
            }
            Assert.IsTrue(found, "CSV 276 에 새 대사");
            Assert.IsTrue(gs.GetFlag(FlagKeys.noonEventAccessAuthorization), "입장 권한을 세운다");
        }

        [Test]
        public void PersuasionJudge_MarksTriedForEitherResult()
        {
            StoryScript judge = DialogPilotEquivalenceTests.Script("Judge");
            Assert.AreEqual(1, judge.Nodes["Police : Persuasion@0"].OnEnter.Length);
            Assert.AreEqual(1, judge.Nodes["Police : Persuasion@0.ok"].OnEnter.Length);
        }

        [Test]
        public void NewFlags_AreSavedAndDefaultFalse()
        {
            GameState gs = GameState.New();
            Assert.IsFalse(gs.GetFlag(FlagKeys.noonEventDelayed));
            Assert.IsFalse(gs.GetFlag(FlagKeys.doorPolicePersuadeTried));
            CollectionAssert.Contains(FlagKeys.All, FlagKeys.noonEventDelayed);
            CollectionAssert.Contains(FlagKeys.All, FlagKeys.doorPolicePersuadeTried);
        }
    }
}
