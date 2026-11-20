using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.Save;
using SCoW.Core.State;

namespace SCoW.Tests
{
    public sealed class SaveCodecTests
    {
        private static GameState Sample()
        {
            GameState s = GameState.New();
            s.SetFlag(FlagKeys.endingDoorCheck, true);
            s.SetCounter(CounterKeys.timeCount, 7);
            s.SetCounter(CounterKeys.enemyDeathCount, -3);
            s.SetText(TextKeys.nowPlace, "Client'shouse");
            s.SetText(TextKeys.battleWeapon, "탭\t줄\n바꿈\\끝");
            return s;
        }

        [Test]
        public void State_RoundTrip_Equal()
        {
            GameState a = Sample();
            GameState b;
            Assert.AreEqual(SaveError.None, SaveCodec.TryDecodeState(SaveCodec.EncodeState(a), out b));
            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual("탭\t줄\n바꿈\\끝", b.GetText(TextKeys.battleWeapon));
        }

        [Test]
        public void State_Encode_IndependentOfInsertionOrder()
        {
            GameState a = new GameState();
            GameState b = new GameState();
            string[] keys = { "k1", "k2", "k3", "k4", "k5" };
            for (int i = 0; i < keys.Length; i++)
            {
                a.SetFlag(keys[i], true);
                a.SetCounter(keys[i], i + 1);
            }
            for (int i = keys.Length - 1; i >= 0; i--)
            {
                b.SetFlag(keys[i], true);
                b.SetCounter(keys[i], i + 1);
            }
            Assert.AreEqual(SaveCodec.EncodeState(a), SaveCodec.EncodeState(b));
        }

        [Test]
        public void State_Decode_RejectsGarbageTruncatedAndEmpty()
        {
            string good = SaveCodec.EncodeState(Sample());
            GameState s;
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeState(null, out s));
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeState("", out s));
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeState("hello", out s));
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeState(good.Substring(0, good.Length / 2), out s));
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeState(good.Replace("C\ttimeCount\t7", "C\ttimeCount\tabc"), out s));
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeState(good + "F\textra\n", out s));
            Assert.IsNull(s);
        }

        [Test]
        public void Meta_RoundTrip_AndDay()
        {
            SaveMeta m = new SaveMeta { SchemaVersion = 1, SavedAtUtcTicks = 638000000000000000L, PlayTimeSec = 3725, PlaceId = "Bar", TimeCount = 6 };
            SaveMeta r;
            Assert.AreEqual(SaveError.None, SaveCodec.TryDecodeMeta(SaveCodec.EncodeMeta(m), out r));
            Assert.AreEqual(m.SavedAtUtcTicks, r.SavedAtUtcTicks);
            Assert.AreEqual(3725, r.PlayTimeSec);
            Assert.AreEqual("Bar", r.PlaceId);
            Assert.AreEqual(2, r.Day);
            Assert.AreEqual(1, new SaveMeta { TimeCount = 4 }.Day);
            Assert.AreEqual(2, new SaveMeta { TimeCount = 5 }.Day);
        }

        [Test]
        public void Meta_Decode_RejectsBroken()
        {
            string good = SaveCodec.EncodeMeta(new SaveMeta { SchemaVersion = 1, SavedAtUtcTicks = 1, PlaceId = "Bar", TimeCount = 2 });
            SaveMeta r;
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeMeta(good.Replace("time\t2\n", ""), out r));
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeMeta(good.Replace("saved\t1", "saved\tx"), out r));
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeMeta("SCOW-STATE 1\n", out r));
        }

        private sealed class AddFlagMigration : ISaveMigration
        {
            public int FromVersion { get { return 1; } }
            public void Apply(GameState state) { state.SetFlag("migrated1to2", true); }
        }

        private sealed class AddCounterMigration : ISaveMigration
        {
            public int FromVersion { get { return 2; } }
            public void Apply(GameState state) { state.SetCounter("migrated2to3", 9); }
        }

        [Test]
        public void Migrator_AppliesChainInOrder()
        {
            SaveMigrator mig = new SaveMigrator(3, new ISaveMigration[] { new AddCounterMigration(), new AddFlagMigration() });
            GameState s = new GameState { SchemaVersion = 1 };
            Assert.AreEqual(SaveError.None, mig.Migrate(s));
            Assert.AreEqual(3, s.SchemaVersion);
            Assert.IsTrue(s.GetFlag("migrated1to2"));
            Assert.AreEqual(9, s.GetCounter("migrated2to3"));
        }

        [Test]
        public void Migrator_NewerVersion_And_BrokenChain()
        {
            SaveMigrator mig = new SaveMigrator(3, new ISaveMigration[] { new AddFlagMigration() });
            Assert.AreEqual(SaveError.NewerVersion, mig.Migrate(new GameState { SchemaVersion = 4 }));
            Assert.AreEqual(SaveError.Corrupt, mig.Migrate(new GameState { SchemaVersion = 1 }));
            Assert.AreEqual(SaveError.None, SaveMigrator.Default.Migrate(new GameState()));
        }

        [Test]
        public void Settings_Sanitize_ClampsBadValues()
        {
            SettingsData d = new SettingsData { SoundVolume = 5f, SfxVolume = float.NaN, TextDelay = -1f, ResolutionIndex = 99 };
            d.Sanitize();
            Assert.AreEqual(1f, d.SoundVolume);
            Assert.AreEqual(SettingsData.DefaultVolume, d.SfxVolume);
            Assert.AreEqual(SettingsData.DefaultTextDelay, d.TextDelay);
            Assert.AreEqual(1, d.ResolutionIndex);
        }

        [Test]
        public void Slot_RoundTrip_SingleString()
        {
            GameState a = Sample();
            SaveMeta m = new SaveMeta { SchemaVersion = a.SchemaVersion, SavedAtUtcTicks = 123456789L, PlayTimeSec = 99, PlaceId = "Bar", TimeCount = 7 };
            string text = SaveCodec.EncodeSlot(m, a);
            SaveMeta m2;
            GameState b;
            Assert.AreEqual(SaveError.None, SaveCodec.TryDecodeSlot(text, out m2, out b));
            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(99, m2.PlayTimeSec);
            Assert.AreEqual("Bar", m2.PlaceId);
        }

        [Test]
        public void Slot_Truncated_Or_MissingState_IsCorrupt()
        {
            GameState a = Sample();
            SaveMeta m = new SaveMeta { SchemaVersion = a.SchemaVersion, PlaceId = "Bar", TimeCount = 1 };
            string text = SaveCodec.EncodeSlot(m, a);
            SaveMeta m2;
            GameState b;
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeSlot(text.Substring(0, text.Length - 8), out m2, out b));
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeSlot(SaveCodec.EncodeMeta(m), out m2, out b));
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeSlot(string.Empty, out m2, out b));
        }

        [Test]
        public void State_Decode_IgnoresUnknownRecordKinds_ForwardCompat()
        {
            GameState a = Sample();
            string text = SaveCodec.EncodeState(a);
            int n = a.Flags.Count + a.Counters.Count + a.Texts.Count;
            string withExtra = text.Replace("END " + n + "\n", "X\tfuture\tvalue\nEND " + (n + 1) + "\n");
            Assert.AreNotEqual(text, withExtra);
            GameState b;
            Assert.AreEqual(SaveError.None, SaveCodec.TryDecodeState(withExtra, out b));
            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(SaveError.Corrupt, SaveCodec.TryDecodeState(text.Replace("END " + n + "\n", "garbage line\nEND " + (n + 1) + "\n"), out b));
        }

        [Test]
        public void Migrator_Default_ChainCoversEveryVersionUpToCurrent()
        {
            for (int v = 1; v <= SaveMigrator.Default.CurrentVersion; v++)
            {
                GameState s = new GameState { SchemaVersion = v };
                Assert.AreEqual(SaveError.None, SaveMigrator.Default.Migrate(s), "v" + v);
                Assert.AreEqual(SaveMigrator.Default.CurrentVersion, s.SchemaVersion);
            }
        }

        [Test]
        public void Migrator_GapInChain_IsCorrupt_EvenIfLaterStepsExist()
        {
            SaveMigrator mig = new SaveMigrator(4, new ISaveMigration[] { new AddCounterMigration() }); // 2→3 만 있고 1→2, 3→4 없음
            Assert.AreEqual(SaveError.Corrupt, mig.Migrate(new GameState { SchemaVersion = 1 }));
            Assert.AreEqual(SaveError.Corrupt, mig.Migrate(new GameState { SchemaVersion = 2 }));
        }
    }
}
