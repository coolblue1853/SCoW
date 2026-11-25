using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SCoW.Core.Rules;
using SCoW.Core.Save;
using SCoW.Core.Sewer;
using SCoW.Core.State;
using UnityEngine;

namespace SCoW.Tests
{
    /// <summary>PT-7: 하수도 연동. 단체 은신, 방별 조우 전 환원, 조우 구성, 구세이브 기본값, 솔로 대 3인 승률 시뮬.</summary>
    public sealed class PartySewerTests
    {
        private static GameState Joined(bool albert, bool maeve)
        {
            GameState s = GameState.New();
            if (albert)
            {
                PartyState.Join(s, CompanionDefs.Albert);
            }
            if (maeve)
            {
                PartyState.Join(s, CompanionDefs.Maeve);
            }
            return s;
        }

        // ---------- 단체 은신 ----------

        [Test]
        public void Disguise_SingleValue_MatchesLegacyForManySeeds()
        {
            for (int dex = 10; dex <= 90; dex += 10)
            {
                for (int seed = 1; seed <= 60; seed++)
                {
                    bool legacy = BattleEngine.SewerDisguiseFails(dex, new SeededRng(seed));
                    bool group = BattleEngine.SewerPartyDisguiseFails(new[] { dex }, new SeededRng(seed));
                    Assert.AreEqual(legacy, group, "dex " + dex + " seed " + seed);
                }
            }
        }

        [Test]
        public void Disguise_UsesLowestValueAndEmptyPasses()
        {
            for (int seed = 1; seed <= 80; seed++)
            {
                Assert.AreEqual(BattleEngine.SewerDisguiseFails(15, new SeededRng(seed)),
                    BattleEngine.SewerPartyDisguiseFails(new[] { 60, 15, 40 }, new SeededRng(seed)), "seed " + seed);
            }
            Assert.IsFalse(BattleEngine.SewerPartyDisguiseFails(new int[0], new SeededRng(1)));
            Assert.IsFalse(BattleEngine.SewerPartyDisguiseFails(null, new SeededRng(1)));
        }

        [Test]
        public void StealthValues_CollectsFabianAndCompanions()
        {
            GameState solo = GameState.New();
            CollectionAssert.AreEqual(new[] { 60 }, SewerPartyRules.StealthValues(solo, 60, false));
            GameState both = Joined(true, true);
            CollectionAssert.AreEqual(new[] { 60, 15, 40 }, SewerPartyRules.StealthValues(both, 60, false), "대열 순서: 파비안, 알버트, 메이브");
            CollectionAssert.AreEqual(new[] { 60 }, SewerPartyRules.StealthValues(both, 60, true), "코트 착용 = 변장, 동료 제외");
        }

        [Test]
        public void StealthValues_SkipsDeadAndStandby()
        {
            GameState s = Joined(true, true);
            PartyState.Kill(s, "albert");
            s.SetFlag(PartyKeys.Standby("maeve"), true);
            CollectionAssert.AreEqual(new[] { 60 }, SewerPartyRules.StealthValues(s, 60, false));
            Assert.AreEqual(1, SewerPartyRules.PartySize(s));
        }

        [Test]
        public void Disguise_PartyWithAlbertFailsMoreThanSolo()
        {
            GameState solo = GameState.New();
            GameState party = Joined(true, false);
            int soloFails = 0;
            int partyFails = 0;
            for (int seed = 1; seed <= 400; seed++)
            {
                soloFails += SewerPartyRules.DisguiseFails(solo, 60, false, new SeededRng(seed)) ? 1 : 0;
                partyFails += SewerPartyRules.DisguiseFails(party, 60, false, new SeededRng(seed)) ? 1 : 0;
            }
            Assert.Greater(partyFails, soloFails, "알버트 은신 15 가 발목을 잡는다");
        }

        // ---------- 환원 ----------

        [Test]
        public void Restore_RevivesRecoveringAndBrokenInsideSewerWithoutWaiting()
        {
            GameState s = Joined(true, true);
            PartyState.SetHp(s, CompanionDefs.Albert, 1);
            PartyState.SetStatus(s, "albert", UnitStatus.Recovering);
            s.SetCounter(PartyKeys.BackAt("albert"), 99);
            PartyState.SetSan(s, CompanionDefs.Maeve, 0);
            PartyState.SetStatus(s, "maeve", UnitStatus.Broken);
            s.SetCounter(PartyKeys.BackAt("maeve"), 99);

            Assert.AreEqual(2, PartyBattleBridge.RestoreForSewer(s));

            Assert.AreEqual(UnitStatus.Ready, PartyState.GetStatus(s, "albert"));
            Assert.AreEqual(CompanionDefs.Albert.MaxHp / 2, PartyState.GetHp(s, "albert"), "복귀 HP 절반(임시 가정)");
            Assert.AreEqual(UnitStatus.Ready, PartyState.GetStatus(s, "maeve"));
            Assert.AreEqual(PartyBattleBridge.BrokenReturnSan, PartyState.GetSan(s, "maeve"));
            Assert.AreEqual(0, s.GetCounter(PartyKeys.BackAt("albert")));
            Assert.AreEqual(0, PartyBattleBridge.RestoreForSewer(s), "두 번째 호출은 되살릴 사람이 없다");
        }

        [Test]
        public void Restore_KeepsHighHpAndLeavesDeadAndStandbyAlone()
        {
            GameState s = Joined(true, true);
            PartyState.SetHp(s, CompanionDefs.Albert, 70);
            PartyState.SetStatus(s, "albert", UnitStatus.Recovering);
            PartyState.Kill(s, "maeve");
            Assert.AreEqual(1, PartyBattleBridge.RestoreForSewer(s));
            Assert.AreEqual(70, PartyState.GetHp(s, "albert"), "절반보다 높으면 그대로");
            Assert.IsTrue(PartyState.IsDead(s, "maeve"));
            Assert.AreEqual(UnitStatus.Fallen, PartyState.GetStatus(s, "maeve"));

            GameState t = Joined(true, false);
            PartyState.SetStatus(t, "albert", UnitStatus.Recovering);
            PartyState.SetHp(t, CompanionDefs.Albert, 1);
            t.SetFlag(PartyKeys.Standby("albert"), true);
            Assert.AreEqual(0, PartyBattleBridge.RestoreForSewer(t));
            Assert.AreEqual(UnitStatus.Recovering, PartyState.GetStatus(t, "albert"));
        }

        [Test]
        public void Restore_DownedLeftOutsideBattleBecomesReadyWithMinimumHp()
        {
            GameState s = Joined(true, false);
            PartyState.SetHp(s, CompanionDefs.Albert, 0);
            PartyState.SetStatus(s, "albert", UnitStatus.Downed);
            Assert.AreEqual(1, PartyBattleBridge.RestoreForSewer(s));
            Assert.AreEqual(UnitStatus.Ready, PartyState.GetStatus(s, "albert"));
            Assert.AreEqual(PartyBattleBridge.DownedReturnHp, PartyState.GetHp(s, "albert"));
        }

        [Test]
        public void Restore_ThenRosterIncludesCompanionsDespiteBackAt()
        {
            GameState s = Joined(true, true);
            PartyState.SetStatus(s, "albert", UnitStatus.Recovering);
            s.SetCounter(PartyKeys.BackAt("albert"), 999);
            PartyMember leader = PartyMember.CreateLeader("fabian", 60, 60, 50, 50, 50, 50, new Dictionary<string, int>(), new string[0]);
            Assert.AreEqual(2, PartyBattleBridge.BuildRoster(s, leader, 5).Count, "환원 전에는 알버트가 빠진다");
            PartyBattleBridge.RestoreForSewer(s);
            Assert.AreEqual(3, PartyBattleBridge.BuildRoster(s, leader, 5).Count);
        }

        // ---------- 조우 구성 ----------

        [Test]
        public void Composition_SoloKeepsLegacyAndPartyAddsOne()
        {
            CollectionAssert.AreEqual(new[] { SewerPartyRules.Hybrid4 }, SewerPartyRules.EnemyIndices(1, 1));
            CollectionAssert.AreEqual(new[] { SewerPartyRules.DeepOne1 }, SewerPartyRules.EnemyIndices(2, 1));
            CollectionAssert.AreEqual(new[] { SewerPartyRules.Hybrid4, SewerPartyRules.DeepOne1 }, SewerPartyRules.EnemyIndices(3, 1));
            for (int no = 1; no <= 3; no++)
            {
                Assert.AreEqual(SewerPartyRules.EnemyIndices(no, 1).Length + 1, SewerPartyRules.EnemyIndices(no, 2).Length);
                CollectionAssert.AreEqual(SewerPartyRules.EnemyIndices(no, 2), SewerPartyRules.EnemyIndices(no, 3));
                Assert.LessOrEqual(SewerPartyRules.EnemyIndices(no, 3).Length, BattleField.Ranks, "턴 심볼 3칸");
            }
            CollectionAssert.AreEqual(SewerPartyRules.EnemyIndices(3, 1), SewerPartyRules.EnemyIndices(0, 1), "0·범위 밖 = 3번 구성(옛 else 가지)");
        }

        // ---------- 구세이브 ----------

        [Test]
        public void OldSave_WithoutPartyKeysRunsAsSolo()
        {
            GameState fresh = GameState.New();
            GameState loaded;
            Assert.AreEqual(SaveError.None, SaveCodec.TryDecodeState(SaveCodec.EncodeState(fresh), out loaded));
            Assert.AreEqual(1, SewerPartyRules.PartySize(loaded));
            Assert.AreEqual(0, PartyState.GetOrder(loaded).Count);
            Assert.AreEqual(0, PartyBattleBridge.RestoreForSewer(loaded));
            CollectionAssert.AreEqual(new[] { 55 }, SewerPartyRules.StealthValues(loaded, 55, false));
            CollectionAssert.AreEqual(new[] { SewerPartyRules.DeepOne1 }, SewerPartyRules.EnemyIndices(2, SewerPartyRules.PartySize(loaded)));
        }

        [Test]
        public void PartyState_SurvivesSaveRoundTripWithStatus()
        {
            GameState s = Joined(true, true);
            PartyState.SetStatus(s, "maeve", UnitStatus.Broken);
            GameState loaded;
            Assert.AreEqual(SaveError.None, SaveCodec.TryDecodeState(SaveCodec.EncodeState(s), out loaded));
            Assert.AreEqual(3, SewerPartyRules.PartySize(loaded));
            Assert.AreEqual(1, PartyBattleBridge.RestoreForSewer(loaded));
        }

        // ---------- 밸런스 1차: 솔로 대 3인 승률 ----------

        private const int Trials = 200;

        private static PartyMember Fabian(int hp = 50, int san = 52)
        {
            Dictionary<string, int> skills = new Dictionary<string, int>();
            for (int i = 0; i < CompanionDefs.SkillKeys.Length; i++)
            {
                skills[CompanionDefs.SkillKeys[i]] = 20;
            }
            skills[CounterKeys.martialArtsPoint] = 50;
            skills[CounterKeys.evasionPoint] = 30;
            skills[CounterKeys.medicinePoint] = 10;
            skills[CounterKeys.psychotherapyPoint] = 5;
            skills[CounterKeys.gunShotPoint] = 40;
            return PartyMember.CreateLeader("fabian", 52, 52, 50, hp, 52, san, skills, new[] { "Revolver" });
        }

        private static BattleCommand AllyPolicy(BattleField f, PartyMember m)
        {
            List<int> aid = f.ValidTargets(m, ActionKind.FirstAid);
            if (aid.Count > 0 && ((PartyMember)f.At(Side.Ally, aid[0])).Status == UnitStatus.Downed)
            {
                return new BattleCommand(m, ActionKind.FirstAid, aid[0]);
            }
            List<int> calm = f.ValidTargets(m, ActionKind.Calm);
            if (calm.Count > 0)
            {
                return new BattleCommand(m, ActionKind.Calm, calm[0]);
            }
            List<int> ranged = f.ValidTargets(m, ActionKind.Ranged);
            if (ranged.Count > 0)
            {
                return new BattleCommand(m, ActionKind.Ranged, ranged[0]);
            }
            List<int> melee = f.ValidTargets(m, ActionKind.Melee);
            if (melee.Count > 0)
            {
                return new BattleCommand(m, ActionKind.Melee, melee[0]);
            }
            return new BattleCommand(m, ActionKind.Guard, m.Rank);
        }

        /// <summary>한 전투를 끝까지 돌려 결과를 돌려준다(정산까지). 동료 자동·파비안도 같은 정책(룰렛 없이 Core 가 판정).</summary>
        private static BattleOutcome Fight(List<PartyMember> allies, int encounterNo, int partySize, IRng rng)
        {
            int[] idx = SewerPartyRules.EnemyIndices(encounterNo, partySize);
            List<EnemyUnit> enemies = new List<EnemyUnit>();
            for (int i = 0; i < idx.Length; i++)
            {
                EnemyState es = new EnemyState(EnemyDefs.All[idx[i]]);
                es.Roll(rng);
                enemies.Add(new EnemyUnit(es, EnemyDefs.All[idx[i]].Id + "#" + i));
            }
            BattleField f = new BattleField(allies, enemies);
            BattleOutcome outcome = BattleOutcome.None;
            for (int turn = 0; turn < 600 && !f.IsOver(out outcome); turn++)
            {
                IBattleUnit actor = f.NextActor();
                if (actor == null)
                {
                    break;
                }
                List<BattleEvent> events = new List<BattleEvent>();
                if (f.BeginTurn(actor, rng, events) && !f.IsOver(out outcome))
                {
                    BattleCommand cmd = actor.Side == Side.Enemy ? f.DecideEnemy((EnemyUnit)actor, rng) : AllyPolicy(f, (PartyMember)actor);
                    f.Resolve(cmd, rng, events);
                }
            }
            f.IsOver(out outcome);
            f.Settle(new List<BattleEvent>());
            return outcome;
        }

        private static List<PartyMember> Roster(int size)
        {
            List<PartyMember> r = new List<PartyMember> { Fabian() };
            if (size >= 2)
            {
                r.Add(PartyMember.FromCompanion(CompanionDefs.Albert));
            }
            if (size >= 3)
            {
                r.Add(PartyMember.FromCompanion(CompanionDefs.Maeve));
            }
            return r;
        }

        private static int WinsSingle(int encounterNo, int size, int baseSeed)
        {
            int wins = 0;
            for (int t = 0; t < Trials; t++)
            {
                SeededRng rng = new SeededRng(baseSeed + t);
                wins += Fight(Roster(size), encounterNo, size, rng) == BattleOutcome.Win ? 1 : 0;
            }
            return wins;
        }

        /// <summary>E1 -> E2 -> E3 를 이어 싸운다. 동료 HP·SAN·상태는 GameState 로 넘기고 조우 사이에 RestoreForSewer 를 부른다.</summary>
        private static int WinsChain(int size, int baseSeed)
        {
            int wins = 0;
            for (int t = 0; t < Trials; t++)
            {
                SeededRng rng = new SeededRng(baseSeed + t);
                GameState s = Joined(size >= 2, size >= 3);
                PartyMember leader = Fabian();
                bool ok = true;
                for (int no = 1; no <= 3 && ok; no++)
                {
                    PartyBattleBridge.RestoreForSewer(s);
                    List<PartyMember> roster = PartyBattleBridge.BuildRoster(s, leader, 5);
                    if (size == 1)
                    {
                        roster = new List<PartyMember> { leader };
                    }
                    BattleOutcome o = Fight(roster, no, roster.Count, rng);
                    PartyBattleBridge.WriteBack(s, roster, 5);
                    ok = o == BattleOutcome.Win && !leader.Dead;
                }
                wins += ok ? 1 : 0;
            }
            return wins;
        }

        [Test]
        public void Balance_SoloVsPartyWinRates_WritesTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("조우 | 솔로 | 2인 | 3인  (각 " + Trials + "판, 시드 고정, 승리 수)");
            int[] solo = new int[3];
            int[] duo = new int[3];
            int[] trio = new int[3];
            for (int no = 1; no <= 3; no++)
            {
                solo[no - 1] = WinsSingle(no, 1, 1000 * no);
                duo[no - 1] = WinsSingle(no, 2, 1000 * no);
                trio[no - 1] = WinsSingle(no, 3, 1000 * no);
                sb.AppendLine("E" + no + " | " + solo[no - 1] + " | " + duo[no - 1] + " | " + trio[no - 1]);
            }
            int cs = WinsChain(1, 5000);
            int cd = WinsChain(2, 5000);
            int ct = WinsChain(3, 5000);
            sb.AppendLine("E1~E3 연속 | " + cs + " | " + cd + " | " + ct);
            string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Temp", "PartySewerBalance.txt");
            File.WriteAllText(path, sb.ToString());
            Debug.Log(sb.ToString());
            for (int i = 0; i < 3; i++)
            {
                Assert.GreaterOrEqual(trio[i], solo[i], "E" + (i + 1) + ": 3인이 솔로보다 낮지 않다(동료는 편의)");
            }
            Assert.GreaterOrEqual(ct, cs, "연속 클리어도 3인이 솔로 이상");
        }
    }
}
