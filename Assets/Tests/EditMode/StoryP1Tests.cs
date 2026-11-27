using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.State;
using SCoW.Core.Story;

namespace SCoW.Tests
{
    /// <summary>
    /// 스토리 P1 S 묶음(StoryImprove A-6~A-12). 실제 Resources/Story JSON + CSV 를 StoryResolver 로 풀어 줄 ID·문구·플래그를 확인한다.
    /// </summary>
    public sealed class StoryP1Tests
    {
        private static StoryResolution Resolve(string scriptId, string topic, GameState gs)
        {
            StoryScript sc = DialogPilotEquivalenceTests.Script(scriptId);
            GameStateAccess acc = new GameStateAccess(gs);
            List<string> errors = new List<string>();
            StoryResolution res = StoryResolver.Resolve(sc, topic, acc, acc, DialogPilotEquivalenceTests.Csv, errors);
            Assert.AreEqual(0, errors.Count, string.Join(" | ", errors));
            return res;
        }

        private static string Ids(StoryResolution res)
        {
            List<string> ids = new List<string>();
            for (int i = 0; i < res.Lines.Count; i++) { ids.Add(res.Lines[i].File + ":" + res.Lines[i].Id); }
            return string.Join(",", ids.ToArray());
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

        // A-6: 메이브 술 설득 성공이 경찰 설득 보상 플래그를 세우지 않는다. 경찰 설득 성공은 그대로 세운다.
        [Test]
        public void A6_MaevePersuasion_DoesNotGrantPoliceAccess()
        {
            StoryScript judge = DialogPilotEquivalenceTests.Script("Judge");
            Assert.AreEqual(0, judge.Nodes["Maeve : Persuasion@0.ok"].OnEnter.Length, "Maeve 노드 onEnter");
            Assert.AreEqual(1, judge.Nodes["Police : Persuasion@0.ok"].OnEnter.Length);
        }

        // A-7: 알버트에게 먼저 들었으면 파비안 54 대신 새 줄(322), 아니면 원래 줄.
        [Test]
        public void A7_SwainMissingPeople_AlbertToldVariant()
        {
            GameState told = At(3);
            told.SetFlag("intelMissingPeople2", true);
            string ids = Ids(Resolve("Keyword", "Berkeley Swain/Husband's job", told));
            Assert.IsTrue(ids.Contains("1st_DailyNews:53,1st_DailyNews:322,1st_DailyNews:55"), ids);
            Assert.IsFalse(ids.Contains("1st_DailyNews:54,"), ids);
            Assert.IsTrue(Line("1st_DailyNews", "322").Text.Contains("Albert mentioned"));
            string plain = Ids(Resolve("Keyword", "Berkeley Swain/Husband's job", At(3)));
            Assert.IsTrue(plain.Contains("1st_DailyNews:53,1st_DailyNews:54,1st_DailyNews:55"), plain);
            Assert.IsFalse(plain.Contains("322"), plain);
        }

        // A-8: 천문쇼 날짜는 15일 저녁이고 에필로그 약속도 저녁 약속이다.
        [Test]
        public void A8_DateUnified_15thEvening()
        {
            Assert.IsTrue(Line("1st_PoliceOffice", "108").Text.Contains("evening of the 15th"));
            Assert.IsFalse(Line("1st_PoliceOffice", "108").Text.Contains("14th"));
            Assert.IsTrue(Line("InSewer", "146").Text.Contains("evening appointment"));
            Assert.IsTrue(Line("InSewer", "145").Text.Contains("sun was already high"));
        }

        // A-9: 성별 대명사.
        [Test]
        public void A9_Pronouns()
        {
            Assert.IsTrue(Line("1st_Bar", "71").Text.Contains("his shoulder"));
            Assert.IsTrue(Line("1st_PoliceOffice", "106").Text.Contains("seen her much"));
        }

        // A-10: 하수도 정비소 표기 통일(키워드 표식 ●1ⓢ 는 그대로).
        [TestCase("1st_Bar", "175")]
        [TestCase("1st_DailyNews", "101")]
        [TestCase("1st_DailyNews", "152")]
        [TestCase("1st_DailyNews", "186")]
        [TestCase("1st_Hospital", "115")]
        [TestCase("1st_PoliceOffice", "143")]
        [TestCase("1st_PoliceOffice", "152")]
        [TestCase("1st_Riverside", "46")]
        [TestCase("1st_Slum", "37")]
        [TestCase("2nd_NoonEvent", "146")]
        public void A10_OfficeName_Unified(string file, string id)
        {
            string t = Line(file, id).Text;
            Assert.IsTrue(t.Contains("●1ⓢSewerage Maintenance Office"), t);
        }

        [Test]
        public void A10_NoOldNamesLeft()
        {
            string[] files = { "1st_Bar", "1st_DailyNews", "1st_Hospital", "1st_Item", "1st_PoliceOffice", "1st_Riverside", "1st_Slum", "2nd_NoonEvent", "1st_DetectiveOffice", "InSewer", "SewerOffice" };
            string[] bad = { "sewage maintenance", "sewerage office", "sewage office", "sewerge", "sewage treatment office" };
            for (int f = 0; f < files.Length; f++)
            {
                IList<StoryLine> lines = DialogPilotEquivalenceTests.Csv(files[f]);
                for (int i = 0; i < lines.Count; i++)
                {
                    string t = lines[i].Text == null ? "" : lines[i].Text.ToLowerInvariant();
                    for (int b = 0; b < bad.Length; b++)
                    {
                        Assert.IsFalse(t.Contains(bad[b]), files[f] + ":" + lines[i].Id + " " + bad[b]);
                    }
                }
            }
        }

        // A-12: 시신 팔찌 절도 성공이 braceletStolen 을 세우고, 세웠으면 알버트 3일차 대사가 203 대신 새 줄(330-331)을 쓴다.
        [Test]
        public void A12_BraceletStolen_FlagAndAlbertLine()
        {
            StoryScript judge = DialogPilotEquivalenceTests.Script("Judge");
            Assert.AreEqual(1, judge.Nodes["DeadBody : Stealing@0.ok"].OnEnter.Length);
            Assert.AreEqual(0, judge.Nodes["DeadBody : Stealing@0.fail"].OnEnter.Length);
            GameState stolen = At(9);
            stolen.SetFlag("braceletStolen", true);
            string ids = Ids(Resolve("1st_PoliceOffice", "Albert_FirstDialog", stolen));
            Assert.IsTrue(ids.Contains("1st_PoliceOffice:202,1st_PoliceOffice:330,1st_PoliceOffice:331,1st_PoliceOffice:204"), ids);
            Assert.IsFalse(ids.Contains("203"), ids);
            string plain = Ids(Resolve("1st_PoliceOffice", "Albert_FirstDialog", At(9)));
            Assert.IsTrue(plain.Contains("1st_PoliceOffice:202,1st_PoliceOffice:203,1st_PoliceOffice:204"), plain);
        }
    }
}
