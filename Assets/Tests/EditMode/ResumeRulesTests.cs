using System.IO;
using System.Text;
using NUnit.Framework;
using SCoW.Core.Save;
using SCoW.Core.State;
using UnityEngine;

namespace SCoW.Tests
{
    public sealed class ResumeRulesTests
    {
        private const string GoldenPath = "Assets/Tests/EditMode/Golden/DeathResume.golden.txt";

        [Test]
        public void LatestIndex_PicksNewest_IgnoresNone()
        {
            Assert.AreEqual(2, ResumeRules.LatestIndex(new long[] { 100, 0, 300, 200 }));
            Assert.AreEqual(-1, ResumeRules.LatestIndex(new long[] { 0, 0, -5 }));
            Assert.AreEqual(-1, ResumeRules.LatestIndex(new long[0]));
        }

        [Test]
        public void LatestIndex_Tie_FirstWins()
        {
            Assert.AreEqual(1, ResumeRules.LatestIndex(new long[] { 0, 500, 500 }));
        }

        [Test]
        public void DeathChoices_ResumeOnlyWithSave()
        {
            CollectionAssert.AreEqual(new[] { DeathChoice.ResumeLast, DeathChoice.Title, DeathChoice.Quit }, ResumeRules.DeathChoices(true));
            CollectionAssert.AreEqual(new[] { DeathChoice.Title, DeathChoice.Quit }, ResumeRules.DeathChoices(false));
        }

        private static GameState SavedPoint()
        {
            GameState s = GameState.New();
            s.SetText(TextKeys.nowPlace, "Policeoffice");
            s.SetCounter(CounterKeys.timeCount, 6);
            s.SetCounter(CounterKeys.nowHP, 9);
            s.SetCounter(CounterKeys.nowSan, 40);
            s.SetFlag(FlagKeys.endingDoorCheck, true);
            return s;
        }

        // 저장 → 죽음(HP·SAN 0, 진행 변경) → 마지막 저장 슬롯 디코드 = 저장 시점 상태. 골든(SaveCodec 텍스트)과 대조한다.
        [Test]
        public void DeathThenResume_StateEqualsSavedPoint_Golden()
        {
            GameState live = SavedPoint();
            SaveMeta meta = new SaveMeta { SchemaVersion = live.SchemaVersion, SavedAtUtcTicks = 123456789L, PlayTimeSec = 321, PlaceId = "Policeoffice", TimeCount = 6 };
            string slotText = SaveCodec.EncodeSlot(meta, live);

            live.SetCounter(CounterKeys.nowHP, 0);
            live.SetCounter(CounterKeys.nowSan, 0);
            live.SetCounter(CounterKeys.timeCount, 7);
            live.SetText(TextKeys.nowPlace, "Slum");

            SaveMeta m2;
            GameState resumed;
            Assert.AreEqual(SaveError.None, SaveCodec.TryDecodeSlot(slotText, out m2, out resumed));
            Assert.IsTrue(SavedPoint().Equals(resumed));
            Assert.IsFalse(live.Equals(resumed));
            Assert.AreEqual(9, resumed.GetCounter(CounterKeys.nowHP));

            string text = SaveCodec.EncodeState(resumed);
            string full = Path.Combine(Path.GetDirectoryName(Application.dataPath), GoldenPath);
            if (!File.Exists(full))
            {
                File.WriteAllText(full, text, new UTF8Encoding(false));
                Assert.Inconclusive("골든 파일을 새로 만들었다. 다시 실행해 대조한다: " + GoldenPath);
            }
            Assert.AreEqual(File.ReadAllText(full).Replace("\r\n", "\n"), text);
        }
    }
}
