using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.Rules;
using SCoW.Core.Save;
using SCoW.Core.State;

namespace SCoW.Tests
{
    public sealed class PartyTests
    {
        private static GameState RoundTrip(GameState a)
        {
            GameState b;
            Assert.AreEqual(SaveError.None, SaveCodec.TryDecodeState(SaveCodec.EncodeState(a), out b));
            return b;
        }

        [Test]
        public void Defs_TwoCompanions_FixedValuesFromDesign()
        {
            Assert.AreEqual(2, CompanionDefs.All.Length);
            Assert.AreSame(CompanionDefs.Albert, CompanionDefs.Get("albert"));
            Assert.AreSame(CompanionDefs.Maeve, CompanionDefs.Get("maeve"));
            Assert.IsNull(CompanionDefs.Get("fabian"));
            Assert.AreEqual(75, CompanionDefs.Albert.MaxHp);
            Assert.AreEqual(65, CompanionDefs.Maeve.MaxSan);
            Assert.AreEqual(60, CompanionDefs.Albert.GetSkill(CounterKeys.gunShotPoint));
            Assert.AreEqual(70, CompanionDefs.Maeve.GetSkill(CounterKeys.rhetoricPoint));
            Assert.IsTrue(CompanionDefs.Albert.HasTag("Authority"));
            Assert.IsTrue(CompanionDefs.Maeve.HasTag("Underworld"));
        }

        [Test]
        public void Defs_AllSkillsPresent_WithinCapAndWeaponsKnown()
        {
            HashSet<string> legacy = new HashSet<string>(CounterKeys.All);
            foreach (CompanionDef d in CompanionDefs.All)
            {
                Assert.AreEqual(15, d.Skills.Count, d.Id);
                foreach (KeyValuePair<string, int> kv in d.Skills)
                {
                    Assert.IsTrue(legacy.Contains(kv.Key), kv.Key);
                    Assert.IsTrue(kv.Value >= 1 && kv.Value <= 90, d.Id + ":" + kv.Key);
                }
                foreach (string sp in d.Specialties)
                {
                    Assert.GreaterOrEqual(d.GetSkill(sp), 40, d.Id + ":" + sp);
                }
                foreach (string w in d.Weapons)
                {
                    Assert.AreNotSame(BattleDamage.Martial, BattleDamage.ForWeapon(w), w);
                }
            }
        }

        [Test]
        public void Keys_UniqueAndDoNotCollideWithLegacyKeys()
        {
            HashSet<string> legacy = new HashSet<string>(FlagKeys.All);
            legacy.UnionWith(CounterKeys.All);
            legacy.UnionWith(TextKeys.All);
            HashSet<string> seen = new HashSet<string> { PartyKeys.Order };
            foreach (CompanionDef d in CompanionDefs.All)
            {
                string[] keys =
                {
                    PartyKeys.Joined(d.Id), PartyKeys.Dead(d.Id), PartyKeys.Declined(d.Id), PartyKeys.Standby(d.Id),
                    PartyKeys.Hp(d.Id), PartyKeys.San(d.Id), PartyKeys.Status(d.Id), PartyKeys.BackAt(d.Id),
                };
                foreach (string k in keys)
                {
                    StringAssert.StartsWith(PartyKeys.Prefix, k);
                    Assert.IsFalse(legacy.Contains(k), k);
                    Assert.IsTrue(seen.Add(k), k);
                }
            }
        }

        [Test]
        public void OldSave_NoPartyKeys_ReadsAsNoParty()
        {
            GameState a = GameState.New();
            a.SetFlag(FlagKeys.endingDoorCheck, true);
            a.SetCounter(CounterKeys.timeCount, 5);
            GameState b = RoundTrip(a);
            Assert.AreEqual(0, PartyState.ActiveCompanions(b).Count);
            Assert.AreEqual(0, PartyState.GetOrder(b).Count);
            foreach (CompanionDef d in CompanionDefs.All)
            {
                Assert.IsFalse(PartyState.IsJoined(b, d.Id));
                Assert.IsFalse(PartyState.IsDead(b, d.Id));
                Assert.AreEqual(0, PartyState.GetHp(b, d.Id));
                Assert.AreEqual(UnitStatus.Ready, PartyState.GetStatus(b, d.Id));
            }
            foreach (string k in b.Flags) { StringAssert.DoesNotStartWith(PartyKeys.Prefix, k); }
            foreach (string k in b.Counters.Keys) { StringAssert.DoesNotStartWith(PartyKeys.Prefix, k); }
            Assert.AreEqual(GameState.CurrentSchemaVersion, b.SchemaVersion);
        }

        [Test]
        public void Join_SetsFullVitals_OrderAndSurvivesRoundTrip()
        {
            GameState a = GameState.New();
            PartyState.Join(a, CompanionDefs.Albert);
            PartyState.Join(a, CompanionDefs.Maeve);
            PartyState.Join(a, CompanionDefs.Albert);
            Assert.AreEqual("fabian,albert,maeve", a.GetText(PartyKeys.Order));
            GameState b = RoundTrip(a);
            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(75, PartyState.GetHp(b, "albert"));
            Assert.AreEqual(65, PartyState.GetSan(b, "maeve"));
            List<CompanionDef> active = PartyState.ActiveCompanions(b);
            Assert.AreEqual(2, active.Count);
            Assert.AreSame(CompanionDefs.Albert, active[0]);
            Assert.AreSame(CompanionDefs.Maeve, active[1]);
        }

        [Test]
        public void ZeroHp_AfterJoin_StaysZeroNotMax()
        {
            GameState a = GameState.New();
            PartyState.Join(a, CompanionDefs.Maeve);
            PartyState.SetHp(a, CompanionDefs.Maeve, 0);
            PartyState.SetStatus(a, "maeve", UnitStatus.Downed);
            GameState b = RoundTrip(a);
            Assert.AreEqual(0, PartyState.GetHp(b, "maeve"));
            Assert.AreEqual(UnitStatus.Downed, PartyState.GetStatus(b, "maeve"));
            PartyState.SetHp(b, CompanionDefs.Maeve, 999);
            Assert.AreEqual(55, PartyState.GetHp(b, "maeve"));
        }

        [Test]
        public void Kill_RemovesFromActive_KeepsJoinedRecord()
        {
            GameState a = GameState.New();
            PartyState.Join(a, CompanionDefs.Albert);
            PartyState.Join(a, CompanionDefs.Maeve);
            PartyState.Kill(a, "albert");
            GameState b = RoundTrip(a);
            Assert.IsTrue(PartyState.IsDead(b, "albert"));
            Assert.IsTrue(PartyState.IsJoined(b, "albert"));
            List<CompanionDef> active = PartyState.ActiveCompanions(b);
            Assert.AreEqual(1, active.Count);
            Assert.AreSame(CompanionDefs.Maeve, active[0]);
        }

        [Test]
        public void DeclinedAndStandby_FlagsRoundTrip_JoinClearsThem()
        {
            GameState a = GameState.New();
            a.SetFlag(PartyKeys.Declined("albert"), true);
            a.SetFlag(PartyKeys.Standby("maeve"), true);
            GameState b = RoundTrip(a);
            Assert.IsTrue(PartyState.IsDeclined(b, "albert"));
            Assert.IsTrue(PartyState.IsStandby(b, "maeve"));
            Assert.AreEqual(0, PartyState.ActiveCompanions(b).Count);
            PartyState.Join(b, CompanionDefs.Albert);
            Assert.IsFalse(PartyState.IsDeclined(b, "albert"));
        }

        [Test]
        public void Order_MissingJoinedMember_IsAppendedAndUnknownIdsIgnored()
        {
            GameState a = GameState.New();
            a.SetFlag(PartyKeys.Joined("maeve"), true);
            a.SetText(PartyKeys.Order, "fabian,ghost,albert");
            List<CompanionDef> active = PartyState.ActiveCompanions(a);
            Assert.AreEqual(1, active.Count);
            Assert.AreSame(CompanionDefs.Maeve, active[0]);
        }
    }
}
