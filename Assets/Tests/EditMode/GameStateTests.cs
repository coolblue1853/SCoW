using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.State;

namespace SCoW.Tests
{
    public sealed class GameStateTests
    {
        [Test]
        public void New_AllFlagsFalseExceptDefaultTrue()
        {
            GameState s = GameState.New();
            foreach (string k in FlagKeys.All)
            {
                bool expected = System.Array.IndexOf(FlagKeys.DefaultTrue, k) >= 0;
                Assert.AreEqual(expected, s.GetFlag(k), k);
            }
        }

        [Test]
        public void New_KeyDefaultsMatchLegacyFieldInitializers()
        {
            GameState s = GameState.New();
            Assert.IsTrue(s.GetFlag(FlagKeys.bgmChangeChecker));
            Assert.AreEqual(0, s.GetCounter(CounterKeys.enemyDeathCount));
            Assert.AreEqual(0, s.GetCounter(CounterKeys.posionDebuff));
            Assert.IsFalse(s.GetFlag(FlagKeys.endingDoorCheck));
            Assert.AreEqual(25, s.GetCounter(CounterKeys.martialArtsPoint));
            Assert.AreEqual(30, s.GetCounter(CounterKeys.evasionPoint));
            Assert.AreEqual("DetectiveOffice", s.GetText(TextKeys.nowPlace));
            Assert.AreEqual(string.Empty, s.GetText(TextKeys.battleWeapon));
            Assert.AreEqual(GameState.CurrentSchemaVersion, s.SchemaVersion);
        }

        [Test]
        public void New_AllCountersMatchTable()
        {
            GameState s = GameState.New();
            foreach (string k in CounterKeys.All)
            {
                int expected = 0;
                foreach (KeyValuePair<string, int> kv in CounterKeys.NonZeroDefaults)
                {
                    if (kv.Key == k)
                    {
                        expected = kv.Value;
                    }
                }
                Assert.AreEqual(expected, s.GetCounter(k), k);
            }
        }

        [Test]
        public void Keys_AreUnique()
        {
            HashSet<string> all = new HashSet<string>();
            foreach (string k in FlagKeys.All) { Assert.IsTrue(all.Add(k), k); }
            foreach (string k in CounterKeys.All) { Assert.IsTrue(all.Add(k), k); }
            foreach (string k in TextKeys.All) { Assert.IsTrue(all.Add(k), k); }
        }

        [Test]
        public void SetBackToDefault_EqualsNew()
        {
            GameState s = GameState.New();
            s.SetFlag(FlagKeys.endingDoorCheck, true);
            s.SetCounter(CounterKeys.enemyDeathCount, 7);
            s.SetCounter(CounterKeys.evasionPoint, 0);
            s.SetText(TextKeys.nowPlace, "Bar");
            Assert.AreNotEqual(GameState.New(), s);
            s.SetFlag(FlagKeys.endingDoorCheck, false);
            s.SetCounter(CounterKeys.enemyDeathCount, 0);
            s.SetCounter(CounterKeys.evasionPoint, 30);
            s.SetText(TextKeys.nowPlace, "DetectiveOffice");
            Assert.AreEqual(GameState.New(), s);
        }

        [Test]
        public void Clone_RoundTripEqualsAndIsIndependent()
        {
            GameState s = GameState.New();
            s.SetFlag(FlagKeys.maeveFirstDialog, true);
            s.SetCounter(CounterKeys.posionDebuff, 3);
            s.SetText(TextKeys.battleWeapon, "bat");
            GameState c = s.Clone();
            Assert.AreEqual(s, c);
            Assert.AreNotSame(s.Flags, c.Flags);
            c.SetFlag(FlagKeys.maeveFirstDialog, false);
            c.SetCounter(CounterKeys.posionDebuff, 4);
            Assert.IsTrue(s.GetFlag(FlagKeys.maeveFirstDialog));
            Assert.AreEqual(3, s.GetCounter(CounterKeys.posionDebuff));
            Assert.AreNotEqual(s, c);
        }

        [Test]
        public void NewAfterMutation_ResetsEverything()
        {
            GameState s = GameState.New();
            s.SetCounter(CounterKeys.enemyDeathCount, 5);
            s.SetCounter(CounterKeys.posionDebuff, 2);
            s.SetFlag(FlagKeys.endingSwin, true);
            s = GameState.New();
            Assert.AreEqual(0, s.GetCounter(CounterKeys.enemyDeathCount));
            Assert.AreEqual(0, s.GetCounter(CounterKeys.posionDebuff));
            Assert.IsFalse(s.GetFlag(FlagKeys.endingSwin));
        }
    }
}
