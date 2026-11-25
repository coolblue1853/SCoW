using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.State;
using SCoW.Core.Story;
using SCoW.Core.World;

namespace SCoW.Tests
{
    /// <summary>
    /// 스토리 P0 묶음(StoryImprove V-1~V-5). 실제 Resources/Story JSON + CSV 를 StoryResolver 로 풀어 줄 ID·화자·상태를 확인한다.
    /// 골든(옛 경로 동치)은 의도된 변경 행만 고쳤고, 이 테스트가 새 동작을 따로 못 박는다.
    /// </summary>
    public sealed class StoryP0Tests
    {
        private static string Ids(string scriptId, string topic, GameState gs)
        {
            StoryResolution res = Resolve(scriptId, topic, gs);
            if (res == null) { return "-"; }
            List<string> ids = new List<string>();
            for (int i = 0; i < res.Lines.Count; i++) { ids.Add(res.Lines[i].File + ":" + res.Lines[i].Id); }
            return string.Join(",", ids.ToArray());
        }

        private static StoryResolution Resolve(string scriptId, string topic, GameState gs)
        {
            StoryScript sc = DialogPilotEquivalenceTests.Script(scriptId);
            GameStateAccess acc = new GameStateAccess(gs);
            List<string> errors = new List<string>();
            StoryResolution res = StoryResolver.Resolve(sc, topic, acc, acc, DialogPilotEquivalenceTests.Csv, errors);
            Assert.AreEqual(0, errors.Count, string.Join(" | ", errors));
            return res;
        }

        private static StoryLine Line(string file, string id)
        {
            IList<StoryLine> lines = DialogPilotEquivalenceTests.Csv(file);
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Id == id) { return lines[i]; }
            }
            Assert.Fail(file + ":" + id + " 없음");
            return null;
        }

        private static GameState At(int t)
        {
            GameState s = GameState.New();
            s.SetCounter(CounterKeys.timeCount, t);
            return s;
        }

        // V-1 / A-1: 대학생 키워드 범위가 학생 대사(24-42)를, 케인 점액이 University 53-74 를 가리킨다.
        [TestCase("Content of a Nightmare", "24", "27")]
        [TestCase("Water and Nightmare", "28", "31")]
        [TestCase("Rumor", "32", "34")]
        [TestCase("Source speculation", "35", "38")]
        [TestCase("Sewer & FishySmell", "35", "38")]
        [TestCase("Cause of Fishy smell", "35", "38")]
        [TestCase("Details", "39", "42")]
        public void V1_StudentKeyword_PlaysStudentLines(string keyword, string from, string to)
        {
            StoryResolution res = Resolve("Keyword", "University student/" + keyword, At(1));
            Assert.AreEqual("1st_University", res.Lines[0].File);
            Assert.AreEqual(from, res.Lines[0].Id);
            Assert.AreEqual(to, res.Lines[res.Lines.Count - 1].Id);
            for (int i = 0; i < res.Lines.Count; i++)
            {
                Assert.AreNotEqual("Kane", Line(res.Lines[i].File, res.Lines[i].Id).Speaker, keyword + " 에 케인 대사가 섞임");
            }
        }

        [Test]
        public void V1_KaneSlime_PlaysSlimeLines()
        {
            GameState eyes = At(1);
            eyes.SetFlag("intelCreepyEyes1", true);
            StoryResolution a = Resolve("Keyword", "Kane/Fishy Slime", eyes);
            Assert.AreEqual("1st_University", a.Lines[0].File);
            Assert.AreEqual("53", a.Lines[0].Id);
            Assert.AreEqual("65", a.Lines[a.Lines.Count - 1].Id);
            StoryResolution b = Resolve("Keyword", "Kane/Fishy Slime", At(1));
            Assert.AreEqual("66", b.Lines[0].Id);
            Assert.AreEqual("74", b.Lines[b.Lines.Count - 1].Id);
            Assert.IsTrue(Line("1st_University", "54").Text.Contains("gel-like"), "점액 설명 줄");
        }

        // A-2 ~ A-4: CSV 문구 수정.
        [Test]
        public void A2toA4_CsvTextFixes()
        {
            Assert.IsFalse(Line("1st_PoliceOffice", "29").Text.Contains("all men"), "A-2: 실종자 성별 단정 제거");
            Assert.AreEqual("Fabian", Line("1st_PoliceOffice", "13").Speaker, "A-4");
            Assert.AreEqual("Albert", Line("1st_PoliceOffice", "14").Speaker, "A-4");
            Assert.AreEqual("Albert", Line("2nd_NoonEvent", "4").Speaker, "A-3");
            Assert.IsTrue(Line("2nd_NoonEvent", "4").Text.Contains("Do you remember"), "A-3: 알버트가 엘라를 묻는다");
        }

        // V-2 / A-3: 5번 줄(파비안 질문) 바로 앞이 알버트의 질문 4번이다.
        [Test]
        public void V2_NoonEventPhone_AlbertAsksBeforeFabianWhy()
        {
            IList<StoryLine> lines = DialogPilotEquivalenceTests.Csv("2nd_NoonEvent");
            int i4 = lines.IndexOf(Line("2nd_NoonEvent", "4"));
            Assert.AreEqual("5", lines[i4 + 1].Id);
            Assert.IsTrue(lines[i4 + 1].Text.Contains("why are you asking"));
        }

        // A-5 임시안: 빈 대사창을 띄우던 Sucsses 가 Safe/NoSafe 로 직결, OldMap_Look 은 숨김.
        [Test]
        public void A5_PsychotherapySuccess_RoutesToSafeOrNoSafe()
        {
            GameState safe = At(1);
            safe.SetFlag("getSafe", true);
            Assert.AreEqual("1st_Client'sHouse:149", Ids("1st_Client'sHouse", "Aiden_Judge_psychotherapy_Sucsses", safe).Split(',')[0]);
            Assert.AreEqual("1st_Client'sHouse:170", Ids("1st_Client'sHouse", "Aiden_Judge_psychotherapy_Sucsses", At(1)).Split(',')[0]);
            Assert.AreEqual("-", Ids("SafeLook", "OldMap_Look", At(1)), "OldMap_Look 은 비어 있어 숨김");
        }

        // V-3 / B-1: 에이든 미방문 안내(1일차 밤, 2일차 아침). 방문했으면 원래 대사 그대로.
        [Test]
        public void V3_AidenNotVisited_ShowsHintNightAndMorning()
        {
            string night = Ids("1st_DetectiveOffice", "Fab_FirstDialog", At(4));
            Assert.AreEqual("1st_DetectiveOffice:182,1st_DetectiveOffice:416,1st_DetectiveOffice:417", night);
            GameState met = At(4);
            met.SetFlag("aidenFirstDialog", true);
            Assert.AreEqual("1st_DetectiveOffice:182", Ids("1st_DetectiveOffice", "Fab_FirstDialog", met));
            Assert.IsTrue(Ids("2nd_DetectiveOffice", "Fab_Case1After", At(5)).Contains("1st_DetectiveOffice:418"));
            Assert.IsTrue(Ids("2nd_DetectiveOffice", "Fab_Case2SuccAfter", At(5)).Contains("1st_DetectiveOffice:418"));
            Assert.IsTrue(Ids("2nd_DetectiveOffice", "Fab_Case2Fail", At(5)).Contains("1st_DetectiveOffice:418"));
            GameState met5 = At(5);
            met5.SetFlag("aidenFirstDialog", true);
            Assert.IsFalse(Ids("2nd_DetectiveOffice", "Fab_Case1After", met5).Contains("418"));
            Assert.IsFalse(Ids("2nd_DetectiveOffice", "Fab_Case2Fail", met5).Contains("418"));
            // 마지막 줄(◎ 표식)은 여전히 마지막.
            string[] c1 = Ids("2nd_DetectiveOffice", "Fab_Case1After", At(5)).Split(',');
            Assert.AreEqual("1st_DetectiveOffice:235", c1[c1.Length - 1]);
        }

        // V-4 / B-2: 3일차 아침 구제 우편. 열쇠(또는 강변 자물쇠 성공)가 없을 때만 나오고 마지막 줄이 열쇠(⊙②)다.
        [Test]
        public void V4_MorningMail_OnlyWithoutKey()
        {
            string mail = Ids("Judge", "SanCheck/ThirdDayStartSan", At(9));
            Assert.IsTrue(mail.StartsWith("1st_DetectiveOffice:423,1st_DetectiveOffice:424,1st_DetectiveOffice:425,"), mail);
            Assert.IsTrue(Line("1st_DetectiveOffice", "425").Text.Contains("⊙②"), "열쇠 지급 표식 ⊙②");
            GameState nm = At(9);
            nm.SetFlag("intelNightmare3", true);
            Assert.IsTrue(Ids("Judge", "SanCheck/ThirdDayStartSan", nm).EndsWith("1st_DetectiveOffice:309"));
            GameState hasKey = At(9);
            hasKey.SetFlag("sewerageKey", true);
            Assert.IsFalse(Ids("Judge", "SanCheck/ThirdDayStartSan", hasKey).Contains("423"));
            GameState picked = At(9);
            picked.SetFlag("sewerPicingSucc", true);
            Assert.IsFalse(Ids("Judge", "SanCheck/ThirdDayStartSan", picked).Contains("423"));
            Assert.IsFalse(Ids("Judge", "SanCheck/ThirdDayStartSan", At(1)).Contains("423"), "3일차 아침 전에는 안 나온다");
        }

        // 우편 case 가 SAN 줄 앞에 있어도 SAN 줄(306-309 악몽 / 313-316 평소)은 어느 경우든 한 번만 나온다(중복 재생 없음).
        [Test]
        public void V4_MorningMail_SanLinesPlayExactlyOnce()
        {
            GameState nightmareMail = At(9);
            nightmareMail.SetFlag("intelNightmare3", true);
            AssertSanGroupsOnce(Ids("Judge", "SanCheck/ThirdDayStartSan", nightmareMail), "306", "313");
            AssertSanGroupsOnce(Ids("Judge", "SanCheck/ThirdDayStartSan", At(9)), "313", "306");
            GameState nightmareNoMail = At(1);
            nightmareNoMail.SetFlag("intelNightmare3", true);
            AssertSanGroupsOnce(Ids("Judge", "SanCheck/ThirdDayStartSan", nightmareNoMail), "306", "313");
            AssertSanGroupsOnce(Ids("Judge", "SanCheck/ThirdDayStartSan", At(1)), "313", "306");
        }

        private static void AssertSanGroupsOnce(string ids, string present, string absent)
        {
            Assert.AreEqual(1, CountOf(ids, "1st_DetectiveOffice:" + present + ","), ids);
            Assert.AreEqual(0, CountOf(ids + ",", "1st_DetectiveOffice:" + absent + ","), ids);
        }

        private static int CountOf(string text, string needle)
        {
            int n = 0;
            int i = 0;
            while ((i = text.IndexOf(needle, i, System.StringComparison.Ordinal)) >= 0)
            {
                n++;
                i += needle.Length;
            }
            return n;
        }

        // V-4 / B-2: 사물함 관찰 실패 → 다음 시간대에 다시 시도 가능. 열쇠를 얻으면 풀지 않는다.
        [Test]
        public void V4_LockerRetry_NextSlot_WhenNoKey()
        {
            GameState s = At(3);
            s.SetFlag(FlagKeys.tryObserLokcer, true);
            WorldRules.Apply(s, new NullScene());
            Assert.IsTrue(s.GetFlag(FlagKeys.tryObserLokcer), "같은 칸에서는 그대로");
            Assert.AreEqual(3, s.GetCounter(CounterKeys.lockerTryTime));
            s.SetCounter(CounterKeys.timeCount, 4);
            WorldRules.Apply(s, new NullScene());
            Assert.IsFalse(s.GetFlag(FlagKeys.tryObserLokcer), "다음 칸에 풀림");
            Assert.AreEqual(0, s.GetCounter(CounterKeys.lockerTryTime));
            GameState k = At(3);
            k.SetFlag(FlagKeys.tryObserLokcer, true);
            k.SetFlag(FlagKeys.sewerageKey, true);
            WorldRules.Apply(k, new NullScene());
            k.SetCounter(CounterKeys.timeCount, 4);
            WorldRules.Apply(k, new NullScene());
            Assert.IsTrue(k.GetFlag(FlagKeys.tryObserLokcer), "열쇠를 얻었으면 풀지 않는다");
        }

        // V-5 / B-3: 2·3일차 밤 D-day 대사와 수첩 날짜 한 줄.
        [Test]
        public void V5_DDayLines_Night2And3()
        {
            Assert.AreEqual("1st_DetectiveOffice:254,1st_DetectiveOffice:419,1st_DetectiveOffice:420,1st_DetectiveOffice:255,1st_DetectiveOffice:256",
                Ids("1st_DetectiveOffice", "Fab_2ndConnectStart", At(8)));
            Assert.AreEqual("1st_DetectiveOffice:182,1st_DetectiveOffice:421,1st_DetectiveOffice:422", Ids("1st_DetectiveOffice", "Fab_FirstDialog", At(12)));
            Assert.AreEqual("1st_DetectiveOffice:182", Ids("1st_DetectiveOffice", "Fab_FirstDialog", At(16)));
            Assert.IsTrue(Line("1st_DetectiveOffice", "420").Text.Contains("15th"));
        }

        [TestCase(1, "날짜: 7/11~7/14 (오늘 7/11)", "Date: Jul 11-14 (today Jul 11)")]
        [TestCase(8, "날짜: 7/11~7/14 (오늘 7/12)", "Date: Jul 11-14 (today Jul 12)")]
        [TestCase(16, "날짜: 7/11~7/14 (오늘 7/14)", "Date: Jul 11-14 (today Jul 14)")]
        [TestCase(30, "날짜: 7/11~7/14 (오늘 7/14)", "Date: Jul 11-14 (today Jul 14)")]
        public void V5_NotebookDateLine(int t, string ko, string en)
        {
            Assert.AreEqual(ko, WorldClock.DateLine(t, true));
            Assert.AreEqual(en, WorldClock.DateLine(t, false));
        }

        // 여러 구간 본문(쉼표)의 오류 처리.
        [Test]
        public void MultiRangeText_ErrorsOnMissingSegment()
        {
            StoryLoadResult r = StoryScriptLoader.Load("{\"topics\":[{\"id\":\"t\",\"cases\":[{\"node\":\"a\"}]}],\"nodes\":{\"a\":{\"text\":\"1st_Bar:1,1st_Bar:99999\"}}}");
            Assert.IsTrue(r.Ok, string.Join("|", r.Errors));
            List<string> errors = new List<string>();
            GameState gs = At(1);
            GameStateAccess acc = new GameStateAccess(gs);
            StoryResolution res = StoryResolver.Resolve(r.Script, "t", acc, acc, DialogPilotEquivalenceTests.Csv, errors);
            Assert.IsNull(res);
            Assert.AreEqual(1, errors.Count);
        }

        private sealed class NullScene : IWorldScene
        {
            public bool IsActive(string id) { return false; }
            public void SetActive(string id, bool active) { }
            public void MoveTo(string id, float x, float y, float z) { }
            public float LocalY(string id) { return 0f; }
            public bool GetCanJudge(string id) { return false; }
            public void SetCanJudge(string id, bool value) { }
            public bool GetCanKeyword(string id) { return false; }
            public void SetCanKeyword(string id, bool value) { }
            public void SetLightTime(float time) { }
            public void SetClientsHouseRoomMessy() { }
        }
    }
}
