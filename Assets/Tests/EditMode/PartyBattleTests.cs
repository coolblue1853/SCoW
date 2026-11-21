using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SCoW.Core.Rules;
using SCoW.Core.State;
using UnityEngine;

namespace SCoW.Tests
{
    /// <summary>PT-2: 파티 전투 Core(BattleTimeline/BattleField/PartyMember). 순서 동치·3v3 골든·위치 규칙·쓰러짐·정산·발작 분포.</summary>
    public sealed class PartyBattleTests
    {
        private const string GoldenPath = "Assets/Tests/EditMode/Golden/PartyBattlePT2.golden.txt";

        /// <summary>정해 둔 값을 순서대로 돌려주는 난수. 범위 밖 값은 범위로 맞춘다. 소비 횟수를 센다.</summary>
        private sealed class ListRng : IRng
        {
            private readonly int[] _values;
            public int Consumed;

            public ListRng(params int[] values)
            {
                _values = values;
            }

            public int Next(int minInclusive, int maxExclusive)
            {
                if (maxExclusive <= minInclusive)
                {
                    return minInclusive;
                }
                int x = Consumed < _values.Length ? _values[Consumed] : minInclusive;
                Consumed++;
                return Math.Min(Math.Max(x, minInclusive), maxExclusive - 1);
            }
        }

        private sealed class CountingRng : IRng
        {
            public int Draws;
            public int Next(int minInclusive, int maxExclusive)
            {
                Draws++;
                return minInclusive;
            }
        }

        // 판정 한 번(SkillCheck.Roll, 난수 6개) 결과 고정: 값 1(대성공) / 99(대실패)
        private static readonly int[] RollCrit = { 1, 0, 0, 0, 0, 0 };
        private static readonly int[] RollFumble = { 9, 9, 0, 0, 0, 0 };
        // 적 단순 판정(난수 2개): 5(기능 45~50 에서 대성공) / 99(대실패)
        private static readonly int[] PlainCrit = { 5, 0 };
        private static readonly int[] PlainFumble = { 9, 9 };

        private static int[] Cat(params int[][] parts)
        {
            List<int> all = new List<int>();
            for (int i = 0; i < parts.Length; i++)
            {
                all.AddRange(parts[i]);
            }
            return all.ToArray();
        }

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
            return PartyMember.CreateLeader("fabian", 52, 52, 50, hp, 52, san, skills, new string[0]);
        }

        private static PartyMember Albert(int hp = 75, int san = 55)
        {
            return PartyMember.FromCompanion(CompanionDefs.Albert, hp, san);
        }

        private static PartyMember Maeve(int hp = 55, int san = 65)
        {
            return PartyMember.FromCompanion(CompanionDefs.Maeve, hp, san);
        }

        private static EnemyUnit Enemy(EnemyDef def, string id, int dex = 50, int hp = 100)
        {
            EnemyState s = new EnemyState(def);
            s.Str = 50;
            s.Intl = 50;
            s.Dex = dex;
            s.Mp = 10;
            s.FullHp = hp;
            s.NowHp = hp;
            s.San = 40;
            s.MartialArts = def.MartialArts;
            s.Swordsmanship = def.Swordsmanship;
            s.Evasion = def.Evasion;
            return new EnemyUnit(s, id);
        }

        private static BattleField Field(PartyMember[] allies, params EnemyUnit[] enemies)
        {
            return new BattleField(allies, enemies);
        }

        private static BattleEvent[] Do(BattleField f, IBattleUnit actor, ActionKind kind, int rank, IRng rng, SuccessLevel? pre = null)
        {
            List<BattleEvent> events = new List<BattleEvent>();
            f.Resolve(new BattleCommand(actor, kind, rank, pre), rng, events);
            return events.ToArray();
        }

        private static List<BattleEvent> Settle(BattleField f)
        {
            List<BattleEvent> events = new List<BattleEvent>();
            f.Settle(events);
            return events;
        }

        private static bool Has(BattleEvent[] events, BattleEventKind kind)
        {
            for (int i = 0; i < events.Length; i++)
            {
                if (events[i].Kind == kind)
                {
                    return true;
                }
            }
            return false;
        }

        // ---------- ① 1v1 은 옛 StepGauge 차례 순서와 같다 ----------

        /// <summary>옛 BattleManager.TimeWait/SymbolUp 의 틱 처리(플레이어 -> 적 슬롯 순, 한 틱에 한 명만 차례). 값은 플레이어 -1, 적 인덱스.</summary>
        private static List<int> LegacyOrder(float playerDex, float[] enemyDex, int count)
        {
            float py = -130f;
            float[] gy = new float[enemyDex.Length];
            for (int i = 0; i < gy.Length; i++)
            {
                gy[i] = -130f;
            }
            List<int> seq = new List<int>();
            while (seq.Count < count)
            {
                int who = -2;
                if (BattleEngine.IsGaugeFull(py))
                {
                    py = BattleEngine.GaugeReset;
                    who = -1;
                }
                else
                {
                    py = BattleEngine.StepGauge(py, playerDex, true);
                }
                for (int i = 0; i < enemyDex.Length && who == -2; i++)
                {
                    if (BattleEngine.IsGaugeFull(gy[i]))
                    {
                        gy[i] = BattleEngine.GaugeReset;
                        who = i;
                    }
                    else
                    {
                        gy[i] = BattleEngine.StepGauge(gy[i], enemyDex[i], false);
                    }
                }
                if (who != -2)
                {
                    seq.Add(who);
                }
            }
            return seq;
        }

        [Test]
        public void Timeline_OneAlly_MatchesLegacyTurnOrder()
        {
            float[][] enemyDexSets = { new float[] { 62 }, new float[] { 50, 60, 45 }, new float[] { 79, 79 }, new float[] { 45, 62, 50 }, new float[] { 100 } };
            float[] playerDexes = { 70, 40, 100, 52, 0 };
            for (int c = 0; c < enemyDexSets.Length; c++)
            {
                float[] dexes = enemyDexSets[c];
                List<int> legacy = LegacyOrder(playerDexes[c], dexes, 120);
                PartyMember leader = PartyMember.CreateLeader("fabian", (int)playerDexes[c], 50, 50, 50, 50, 50, Fabian().Skills, new string[0]);
                EnemyUnit[] enemies = new EnemyUnit[dexes.Length];
                for (int i = 0; i < dexes.Length; i++)
                {
                    enemies[i] = Enemy(EnemyDefs.DeepOneHybrid1, "e" + i, (int)dexes[i]);
                }
                BattleField f = Field(new[] { leader }, enemies);
                List<int> now = new List<int>();
                for (int i = 0; i < legacy.Count; i++)
                {
                    IBattleUnit u = f.NextActor();
                    now.Add(u.Side == Side.Ally ? -1 : u.Rank);
                }
                CollectionAssert.AreEqual(legacy, now, "case " + c);
            }
        }

        [Test]
        public void Timeline_Preview_MatchesNextAndDoesNotMutate()
        {
            BattleField f = Field(new[] { Albert(), Fabian(), Maeve() }, Enemy(EnemyDefs.DeepOne1, "a", 50), Enemy(EnemyDefs.DeepOneHybrid1, "b", 70));
            float[] before = new float[5];
            List<IBattleUnit> order = f.Order();
            for (int i = 0; i < order.Count; i++)
            {
                before[i] = order[i].Gauge;
            }
            List<IBattleUnit> preview = f.Preview(30);
            for (int i = 0; i < order.Count; i++)
            {
                Assert.AreEqual(before[i], order[i].Gauge, "preview mutated " + order[i].Id);
            }
            for (int i = 0; i < preview.Count; i++)
            {
                Assert.AreSame(preview[i], f.NextActor(), "turn " + i);
            }
        }

        [Test]
        public void Timeline_OnlyUnitsWithTurns_Participate()
        {
            PartyMember albert = Albert();
            PartyMember maeve = Maeve();
            BattleField f = Field(new[] { Fabian(), albert, maeve }, Enemy(EnemyDefs.DeepOne1, "a"));
            albert.Status = UnitStatus.Downed;
            maeve.Status = UnitStatus.Stable;
            List<IBattleUnit> next = f.Preview(60);
            Assert.IsTrue(next.Contains(albert), "Downed 는 카운트다운 차례를 받는다");
            Assert.IsFalse(next.Contains(maeve), "Stable 은 게이지 정지");
            Assert.IsNull(BattleTimeline.Next(new List<IBattleUnit> { maeve }));
        }

        // ---------- ② 3v3 시드 로그 골든 ----------

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

        private static List<string> RunBattle(string name, int seed, PartyMember[] allies, EnemyDef[] defs, HashSet<BattleEventKind> seen)
        {
            SeededRng rng = new SeededRng(seed);
            List<string> log = new List<string> { "== " + name + " seed=" + seed };
            EnemyUnit[] enemies = new EnemyUnit[defs.Length];
            for (int i = 0; i < defs.Length; i++)
            {
                EnemyState s = new EnemyState(defs[i]);
                s.Roll(rng);
                enemies[i] = new EnemyUnit(s, defs[i].Id + "#" + i);
                log.Add(enemies[i].Id + " hp=" + s.FullHp + " dex=" + s.Dex);
            }
            BattleField f = Field(allies, enemies);
            BattleOutcome outcome = BattleOutcome.None;
            for (int turn = 0; turn < 400 && !f.IsOver(out outcome); turn++)
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
                for (int i = 0; i < events.Count; i++)
                {
                    log.Add("t" + turn + " " + events[i]);
                    seen.Add(events[i].Kind);
                }
            }
            f.IsOver(out outcome);
            List<BattleEvent> settle = Settle(f);
            for (int i = 0; i < settle.Count; i++)
            {
                log.Add("settle " + settle[i]);
                seen.Add(settle[i].Kind);
            }
            StringBuilder sb = new StringBuilder("RESULT " + outcome);
            for (int i = 0; i < f.Roster.Count; i++)
            {
                PartyMember m = f.Roster[i];
                sb.Append(" " + m.Id + "=" + m.Status + "/" + m.Hp + "/" + m.San + (m.Dead ? "/dead" : string.Empty));
            }
            log.Add(sb.ToString());
            return log;
        }

        private static List<string> BuildAll(HashSet<BattleEventKind> seen)
        {
            List<string> all = new List<string>();
            all.AddRange(RunBattle("road-strong", 31, new[] { Albert(), Fabian(), Maeve() },
                new[] { EnemyDefs.DeepOneHybrid1, EnemyDefs.DeepOneHybrid2, EnemyDefs.DeepOneHybrid3 }, seen));
            all.AddRange(RunBattle("sewer-weak", 32, new[] { Albert(30, 30), Fabian(25, 30), Maeve(20, 20) },
                new[] { EnemyDefs.DeepOneHybrid4, EnemyDefs.DeepOne1, EnemyDefs.DeepOne1 }, seen));
            all.AddRange(RunBattle("sewer-san", 33, new[] { Albert(), Fabian(), Maeve() },
                new[] { EnemyDefs.DeepOne1, EnemyDefs.DeepOne1, EnemyDefs.DeepOne1 }, seen));
            for (int seed = 35; seed <= 37; seed++)
            {
                all.AddRange(RunBattle("sewer-fragile-" + seed, seed, new[] { Albert(75, 25), Fabian(50, 40), Maeve(55, 25) },
                    new[] { EnemyDefs.DeepOne1, EnemyDefs.DeepOne1, EnemyDefs.DeepOne1 }, seen));
            }
            all.AddRange(RunBattle("duo", 34, new[] { Fabian(), Maeve() },
                new[] { EnemyDefs.DeepOneHybrid1, EnemyDefs.DeepOne1 }, seen));
            return all;
        }

        [Test]
        public void ThreeVsThree_LogMatchesGolden()
        {
            string text = string.Join("\n", BuildAll(new HashSet<BattleEventKind>()).ToArray()) + "\n";
            string full = Path.Combine(Path.GetDirectoryName(Application.dataPath), GoldenPath);
            if (!File.Exists(full))
            {
                File.WriteAllText(full, text, new UTF8Encoding(false));
                Assert.Inconclusive("골든 파일을 새로 만들었다. 다시 실행해 대조한다: " + GoldenPath);
            }
            Assert.AreEqual(File.ReadAllText(full).Replace("\r\n", "\n"), text);
        }

        [Test]
        public void ThreeVsThree_ExercisesDownFallenAndBouts()
        {
            HashSet<BattleEventKind> seen = new HashSet<BattleEventKind>();
            BuildAll(seen);
            foreach (BattleEventKind k in new[] { BattleEventKind.Hit, BattleEventKind.Miss, BattleEventKind.Down, BattleEventKind.Fallen,
                BattleEventKind.SanLoss, BattleEventKind.EnemyDown, BattleEventKind.BoutStart })
            {
                Assert.IsTrue(seen.Contains(k), "시뮬이 " + k + " 를 거치지 않는다. 시드·정책을 바꿔야 한다");
            }
        }

        // ---------- ③ 근접 사거리·교환 ----------

        [Test]
        public void Melee_RangeByRankAndEnemyFront()
        {
            PartyMember albert = Albert();
            PartyMember fabian = Fabian();
            PartyMember maeve = Maeve();
            BattleField f = Field(new[] { albert, fabian, maeve },
                Enemy(EnemyDefs.DeepOne1, "e0", 50, 5), Enemy(EnemyDefs.DeepOne1, "e1", 50, 5), Enemy(EnemyDefs.DeepOne1, "e2", 50, 100));
            CollectionAssert.AreEqual(new[] { 0, 1 }, f.ValidTargets(albert, ActionKind.Melee));
            CollectionAssert.AreEqual(new[] { 0, 1 }, f.ValidTargets(fabian, ActionKind.Melee));
            Assert.IsEmpty(f.ValidTargets(maeve, ActionKind.Melee), "뒤 칸은 근접 불가");
            CollectionAssert.AreEqual(new[] { 0, 1, 2 }, f.ValidTargets(maeve, ActionKind.Ranged));
            CollectionAssert.AreEqual(new[] { 0, 1, 2 }, f.ValidTargets(albert, ActionKind.Ranged));
            Assert.IsEmpty(f.ValidTargets(fabian, ActionKind.Ranged), "사격 무기 없음");
            CollectionAssert.AreEqual(new[] { 2 }, f.ValidTargets(maeve, ActionKind.Guard));

            // 앞 적이 죽으면 뒤가 앞당겨져 근접이 닿는 칸이 줄어든다
            IBattleUnit front = f.At(Side.Enemy, 0);
            ListRng rng = new ListRng(Cat(RollCrit, PlainFumble, new[] { 1, 1 }));
            BattleEvent[] ev = Do(f, albert, ActionKind.Melee, 0, rng);
            Assert.IsTrue(Has(ev, BattleEventKind.EnemyDown));
            Assert.AreEqual(2, f.CountOn(Side.Enemy));
            Assert.AreNotSame(front, f.At(Side.Enemy, 0));
            Assert.AreEqual(0, f.At(Side.Enemy, 0).Rank);
            Assert.IsNull(f.At(Side.Enemy, 2));
            ev = Do(f, albert, ActionKind.Melee, 0, new ListRng(Cat(RollCrit, PlainFumble, new[] { 1, 1 })));
            Assert.AreEqual(1, f.CountOn(Side.Enemy));
            CollectionAssert.AreEqual(new[] { 0 }, f.ValidTargets(albert, ActionKind.Melee));
        }

        [Test]
        public void Swap_AdjacentOccupiedOnly_AndFollowsOrder()
        {
            PartyMember albert = Albert();
            PartyMember fabian = Fabian();
            BattleField f = Field(new[] { albert, fabian }, Enemy(EnemyDefs.DeepOne1, "e0"));
            Assert.IsFalse(f.Swap(Side.Ally, 0, 2), "비인접");
            Assert.IsFalse(f.Swap(Side.Ally, 1, 2), "빈 칸");
            CollectionAssert.AreEqual(new[] { 1 }, f.ValidTargets(albert, ActionKind.Swap));
            BattleEvent[] ev = Do(f, albert, ActionKind.Swap, 1, new ListRng());
            Assert.IsTrue(Has(ev, BattleEventKind.Swap));
            Assert.AreSame(fabian, f.At(Side.Ally, 0));
            Assert.AreEqual(1, albert.Rank);
            Assert.IsTrue(Has(Do(f, albert, ActionKind.Swap, 1, new ListRng()), BattleEventKind.Invalid), "자기 칸으로 교환 불가");
            // 쓰러진 사람도 교환 가능
            fabian.Status = UnitStatus.Downed;
            Assert.IsTrue(f.Swap(Side.Ally, 0, 1));
        }

        [Test]
        public void FleeBout_ForcesSwapToBack()
        {
            PartyMember albert = Albert();
            albert.Bout = BoutKind.Flee;
            albert.BoutTurns = 1;
            BattleField f = Field(new[] { albert, Fabian(), Maeve() }, Enemy(EnemyDefs.DeepOne1, "e0"));
            List<BattleEvent> ev = new List<BattleEvent>();
            Assert.IsFalse(f.BeginTurn(albert, new ListRng(), ev), "광기 차례는 지시를 받지 않는다");
            Assert.AreEqual(2, albert.Rank);
            Assert.AreEqual("maeve", f.At(Side.Ally, 0).Id);
            Assert.AreEqual(BoutKind.None, albert.Bout);
            Assert.IsTrue(ev.Exists(e => e.Kind == BattleEventKind.BoutEnd));
        }

        // ---------- ④ 쓰러짐·Fallen·승/도주 사망 ----------

        private static BattleField DuoWithAlbertHp(int hp, out PartyMember albert)
        {
            albert = Albert(hp);
            return Field(new[] { Fabian(), albert }, Enemy(EnemyDefs.DeepOne1, "e0"));
        }

        private static ListRng HitThatCannotBeEvaded()
        {
            // 아군 회피 대실패 + 적 대성공 -> 맞음, 피해 1d3+1d3 최대 = 30
            return new ListRng(Cat(RollFumble, PlainCrit, new[] { 3, 3 }));
        }

        [Test]
        public void Downed_CountdownThree_ThenFallen_AndSlotCompacts()
        {
            PartyMember albert;
            BattleField f = DuoWithAlbertHp(30, out albert);
            ListRng rng = HitThatCannotBeEvaded();
            BattleEvent[] ev = Do(f, f.At(Side.Enemy, 0), ActionKind.Melee, 1, rng);
            Assert.AreEqual(10, rng.Consumed);
            Assert.IsTrue(Has(ev, BattleEventKind.Down));
            Assert.AreEqual(UnitStatus.Downed, albert.Status);
            Assert.AreEqual(0, albert.Hp);
            Assert.AreEqual(3, albert.DownedCountdown);
            Assert.AreSame(albert, f.At(Side.Ally, 1), "쓰러져도 칸 유지");

            List<BattleEvent> log = new List<BattleEvent>();
            Assert.IsFalse(f.BeginTurn(albert, new ListRng(), log));
            Assert.AreEqual(2, albert.DownedCountdown);
            Assert.IsFalse(f.BeginTurn(albert, new ListRng(), log));
            Assert.AreEqual(1, albert.DownedCountdown);
            Assert.AreEqual(UnitStatus.Downed, albert.Status);
            Assert.IsFalse(f.BeginTurn(albert, new ListRng(Cat(RollCrit)), log));
            Assert.AreEqual(UnitStatus.Fallen, albert.Status);
            Assert.IsNull(f.At(Side.Ally, 1), "Fallen 은 칸에서 빠진다");
            Assert.AreEqual(1, f.CountOn(Side.Ally));
        }

        [Test]
        public void Downed_HitAgain_IsInstantFallen_AndWitnessesCheckSan()
        {
            PartyMember albert;
            BattleField f = DuoWithAlbertHp(30, out albert);
            albert.Status = UnitStatus.Downed;
            albert.DownedCountdown = 2;
            PartyMember fabian = f.Leader;
            // 목격 SAN 판정: 파비안 판정 실패 -> -5
            ListRng rng = new ListRng(Cat(new[] { 3, 3 }, RollFumble));
            BattleEvent[] ev = Do(f, f.At(Side.Enemy, 0), ActionKind.Melee, 1, rng);
            Assert.AreEqual(UnitStatus.Fallen, albert.Status);
            Assert.IsTrue(Has(ev, BattleEventKind.Fallen));
            Assert.AreEqual(47, fabian.San);
            Assert.IsTrue(Has(ev, BattleEventKind.SanLoss));
        }

        [Test]
        public void FirstAid_Success_MakesStable_AndStableDoesNotCountDown()
        {
            PartyMember albert;
            BattleField f = DuoWithAlbertHp(30, out albert);
            albert.Status = UnitStatus.Downed;
            albert.DownedCountdown = 2;
            PartyMember fabian = f.Leader;
            CollectionAssert.AreEqual(new[] { 1 }, f.ValidTargets(fabian, ActionKind.FirstAid));
            // 파비안 의술 10: 값 1 이면 대성공
            Assert.IsTrue(Has(Do(f, fabian, ActionKind.FirstAid, 1, new ListRng(RollFumble)), BattleEventKind.Miss), "실패는 그대로");
            Assert.AreEqual(UnitStatus.Downed, albert.Status);
            BattleEvent[] ev = Do(f, fabian, ActionKind.FirstAid, 1, new ListRng(RollCrit));
            Assert.IsTrue(Has(ev, BattleEventKind.Stable));
            Assert.AreEqual(UnitStatus.Stable, albert.Status);
            Assert.AreEqual(PartyBattleRules.StableHp, albert.Hp);
            Assert.IsFalse(albert.HasTurns);
        }

        [Test]
        public void FirstAid_OnWounded_HealsFive()
        {
            PartyMember maeve = Maeve(40);
            BattleField f = Field(new[] { Fabian(), maeve }, Enemy(EnemyDefs.DeepOne1, "e0"));
            BattleEvent[] ev = Do(f, f.Leader, ActionKind.FirstAid, 1, new ListRng(RollCrit));
            Assert.IsTrue(Has(ev, BattleEventKind.Heal));
            Assert.AreEqual(45, maeve.Hp);
        }

        [Test]
        public void Settle_Win_FallenRecovers_DownedAndStableGetUp()
        {
            PartyMember albert = Albert();
            PartyMember maeve = Maeve();
            PartyMember fabian = Fabian();
            BattleField f = Field(new[] { fabian, albert, maeve }, Enemy(EnemyDefs.DeepOne1, "e0", 50, 5));
            albert.Status = UnitStatus.Downed;
            albert.Hp = 0;
            maeve.Status = UnitStatus.Fallen;
            maeve.Hp = 0;
            BattleOutcome o;
            Assert.IsFalse(f.IsOver(out o));
            Assert.IsEmpty(Settle(f), "전투 중 정산은 무시");
            // 파비안이 적을 쓰러뜨림: 무술 대성공 + 적 대실패, 피해 1d3
            BattleEvent[] ev = Do(f, fabian, ActionKind.Melee, 0, new ListRng(Cat(RollCrit, PlainFumble, new[] { 3 })));
            Assert.IsTrue(Has(ev, BattleEventKind.EnemyDown));
            Assert.IsTrue(f.IsOver(out o));
            Assert.AreEqual(BattleOutcome.Win, o);
            List<BattleEvent> settle = Settle(f);
            Assert.AreEqual(UnitStatus.Recovering, maeve.Status);
            Assert.AreEqual(1, maeve.Hp);
            Assert.IsFalse(maeve.Dead);
            Assert.AreEqual(UnitStatus.Ready, albert.Status);
            Assert.AreEqual(PartyBattleRules.StableHp, albert.Hp);
            Assert.IsEmpty(Settle(f), "정산은 한 번만");
            Assert.AreEqual(1, settle.Count);
        }

        [Test]
        public void Settle_Fled_AbandonedAlliesDie()
        {
            PartyMember albert = Albert();
            PartyMember maeve = Maeve();
            PartyMember fabian = Fabian();
            BattleField f = Field(new[] { fabian, albert, maeve }, Enemy(EnemyDefs.DeepOne1, "e0", 50, 100));
            albert.Status = UnitStatus.Downed;
            maeve.Status = UnitStatus.Stable;
            CollectionAssert.AreEqual(new[] { 0 }, f.ValidTargets(fabian, ActionKind.Run));
            Assert.IsEmpty(f.ValidTargets(maeve, ActionKind.Run), "도주는 파비안만");
            BattleEvent[] ev = Do(f, fabian, ActionKind.Run, 0, new ListRng(Cat(RollCrit, PlainFumble)));
            Assert.IsTrue(Has(ev, BattleEventKind.RunSuccess));
            BattleOutcome o;
            Assert.IsTrue(f.IsOver(out o));
            Assert.AreEqual(BattleOutcome.Fled, o);
            List<BattleEvent> settle = Settle(f);
            Assert.IsTrue(albert.Dead);
            Assert.IsTrue(maeve.Dead);
            Assert.IsFalse(fabian.Dead);
            Assert.AreEqual(2, settle.Count);
            Assert.IsEmpty(Settle(f));
        }

        [Test]
        public void Run_Failure_KeepsFighting()
        {
            BattleField f = Field(new[] { Fabian(), Albert() }, Enemy(EnemyDefs.DeepOne1, "e0", 50, 100));
            BattleEvent[] ev = Do(f, f.Leader, ActionKind.Run, 0, new ListRng(Cat(RollFumble, PlainCrit)));
            Assert.IsTrue(Has(ev, BattleEventKind.RunFail));
            BattleOutcome o;
            Assert.IsFalse(f.IsOver(out o));
        }

        [Test]
        public void Loss_SoloLeaderAtZeroHp_IsImmediate_PartyLeaderGetsCountdown()
        {
            PartyMember solo = Fabian(20);
            BattleField fs = Field(new[] { solo }, Enemy(EnemyDefs.DeepOne1, "e0"));
            Assert.IsFalse(fs.LeaderDownRule);
            Do(fs, fs.At(Side.Enemy, 0), ActionKind.Melee, 0, HitThatCannotBeEvaded());
            Assert.AreEqual(UnitStatus.Ready, solo.Status, "솔로는 Downed 없이 즉시 패배(옛 경로)");
            BattleOutcome o;
            Assert.IsTrue(fs.IsOver(out o));
            Assert.AreEqual(BattleOutcome.Loss, o);

            PartyMember leader = Fabian(20);
            BattleField fp = Field(new[] { leader, Albert() }, Enemy(EnemyDefs.DeepOne1, "e0"));
            Assert.IsTrue(fp.LeaderDownRule);
            Do(fp, fp.At(Side.Enemy, 0), ActionKind.Melee, 0, HitThatCannotBeEvaded());
            Assert.AreEqual(UnitStatus.Downed, leader.Status);
            Assert.IsFalse(fp.IsOver(out o), "동료가 서 있는 동안은 카운트다운");
            leader.Status = UnitStatus.Fallen;
            Assert.IsTrue(fp.IsOver(out o));
            Assert.AreEqual(BattleOutcome.Loss, o);
        }

        [Test]
        public void Loss_WhenNobodyCanAct()
        {
            PartyMember albert = Albert();
            BattleField f = Field(new[] { Fabian(), albert }, Enemy(EnemyDefs.DeepOne1, "e0"));
            f.Leader.Status = UnitStatus.Downed;
            albert.Status = UnitStatus.Stable;
            BattleOutcome o;
            Assert.IsTrue(f.IsOver(out o));
            Assert.AreEqual(BattleOutcome.Loss, o);
        }

        // ---------- ⑤ SAN·발작 ----------

        [Test]
        public void Bout_BelowTen_NeverRollsAndDrawsNothing()
        {
            CountingRng rng = new CountingRng();
            BoutKind k;
            int turns;
            Assert.IsFalse(PartyBattleRules.RollBout(rng, 65, 5, out k, out turns));
            Assert.AreEqual(0, rng.Draws);
            Assert.AreEqual(BoutKind.None, k);
        }

        [Test]
        public void Bout_Distribution_IntFailureKindAndTurns()
        {
            const int n = 20000;
            SeededRng rng = new SeededRng(77);
            int[] kinds = new int[5];
            int[] turnCounts = new int[4];
            int triggers = 0;
            for (int i = 0; i < n; i++)
            {
                BoutKind k;
                int turns;
                if (PartyBattleRules.RollBout(rng, 65, 10, out k, out turns))
                {
                    triggers++;
                    kinds[(int)k]++;
                    turnCounts[turns]++;
                }
            }
            // INT 65: 1d100 이 66 이상이면 실패 = 35%
            Assert.That(triggers / (double)n, Is.InRange(0.32, 0.38));
            Assert.AreEqual(0, kinds[0]);
            for (int k = 1; k <= 4; k++)
            {
                Assert.That(kinds[k] / (double)triggers, Is.InRange(0.22, 0.28), "kind " + k);
            }
            for (int t = 1; t <= 3; t++)
            {
                Assert.That(turnCounts[t] / (double)triggers, Is.InRange(0.30, 0.37), "turns " + t);
            }
            // INT 가 낮을수록 더 자주
            int low = 0;
            for (int i = 0; i < n; i++)
            {
                BoutKind k;
                int turns;
                if (PartyBattleRules.RollBout(rng, 30, 10, out k, out turns))
                {
                    low++;
                }
            }
            Assert.That(low, Is.GreaterThan(triggers + 3000));
        }

        [Test]
        public void SanAttack_BigLoss_StartsBout_ThenBrokenAtZero()
        {
            PartyMember maeve = Maeve();
            BattleField f = Field(new[] { Fabian(), maeve }, Enemy(EnemyDefs.DeepOne1, "e0"));
            // 메이브 SAN 판정 실패, 손실 10, INT 판정 실패, 종류 3(난동), 2턴
            ListRng rng = new ListRng(Cat(RollFumble, new[] { 1 }, RollFumble, new[] { 3, 2 }));
            BattleEvent[] ev = Do(f, f.At(Side.Enemy, 0), ActionKind.SanAttack, 1, rng);
            Assert.AreEqual(15, rng.Consumed);
            Assert.AreEqual(55, maeve.San);
            Assert.AreEqual(BoutKind.Lash, maeve.Bout);
            Assert.AreEqual(2, maeve.BoutTurns);
            Assert.IsTrue(Has(ev, BattleEventKind.BoutStart));

            PartyMember weak = Albert(75, 5);
            BattleField g = Field(new[] { Fabian(), weak }, Enemy(EnemyDefs.DeepOne1, "e0"));
            ev = Do(g, g.At(Side.Enemy, 0), ActionKind.SanAttack, 1, new ListRng(Cat(RollFumble, new[] { 2 })));
            Assert.AreEqual(0, weak.San);
            Assert.AreEqual(UnitStatus.Broken, weak.Status);
            Assert.IsTrue(Has(ev, BattleEventKind.Broken));
            Assert.IsFalse(weak.IsActing);
            Assert.IsFalse(weak.HasTurns);
        }

        [Test]
        public void SanAttack_Resisted_NoChange_LeaderNeverBouts()
        {
            PartyMember fabian = Fabian();
            BattleField f = Field(new[] { fabian, Maeve() }, Enemy(EnemyDefs.DeepOne1, "e0"));
            Assert.IsTrue(Has(Do(f, f.At(Side.Enemy, 0), ActionKind.SanAttack, 0, new ListRng(RollCrit)), BattleEventKind.SanResist));
            Assert.AreEqual(52, fabian.San);
            // 손실 10 이어도 파비안은 발작 판정 자체를 하지 않는다(난수 6+1 만 소비)
            ListRng rng = new ListRng(Cat(RollFumble, new[] { 1 }));
            Do(f, f.At(Side.Enemy, 0), ActionKind.SanAttack, 0, rng);
            Assert.AreEqual(7, rng.Consumed);
            Assert.AreEqual(42, fabian.San);
            Assert.AreEqual(BoutKind.None, fabian.Bout);
        }

        [Test]
        public void Calm_ClearsBout_FromMiddleOrBackOnly()
        {
            PartyMember albert = Albert(75, 40);
            PartyMember maeve = Maeve();
            BattleField f = Field(new[] { albert, Fabian(), maeve }, Enemy(EnemyDefs.DeepOne1, "e0"));
            albert.Bout = BoutKind.Freeze;
            albert.BoutTurns = 2;
            CollectionAssert.AreEqual(new[] { 0 }, f.ValidTargets(maeve, ActionKind.Calm));
            Assert.IsEmpty(f.ValidTargets(albert, ActionKind.Calm), "앞 칸은 진정 불가");
            Assert.IsTrue(Has(Do(f, maeve, ActionKind.Calm, 0, new ListRng(RollFumble)), BattleEventKind.Miss));
            Assert.AreEqual(BoutKind.Freeze, albert.Bout);
            BattleEvent[] ev = Do(f, maeve, ActionKind.Calm, 0, new ListRng(RollCrit));
            Assert.IsTrue(Has(ev, BattleEventKind.Calm));
            Assert.AreEqual(BoutKind.None, albert.Bout);
            Assert.AreEqual(45, albert.San);
        }

        [Test]
        public void Despair_GuardsAndPenalizesNeighbors()
        {
            PartyMember albert = Albert();
            PartyMember maeve = Maeve();
            albert.Bout = BoutKind.Despair;
            albert.BoutTurns = 2;
            BattleField f = Field(new[] { albert, Fabian(), maeve }, Enemy(EnemyDefs.DeepOne1, "e0"));
            List<BattleEvent> ev = new List<BattleEvent>();
            Assert.IsFalse(f.BeginTurn(albert, new ListRng(), ev));
            Assert.IsTrue(albert.Guarding);
            Assert.AreEqual(1, albert.BoutTurns);
            // 인접한 파비안(칸1)의 회피는 불리 다이스(10의 자리 50)로 실패해 맞는다. 인접하지 않은 메이브(칸2)는 같은 눈이어도 대성공으로 피한다.
            int[] rolls = { 1, 0, 0, 5, 0, 0 };
            Do(f, f.At(Side.Enemy, 0), ActionKind.Melee, 1, new ListRng(Cat(rolls, PlainCrit, new[] { 1, 1 })));
            Assert.AreEqual(40, f.Leader.Hp);
            Do(f, f.At(Side.Enemy, 0), ActionKind.Melee, 2, new ListRng(Cat(rolls, PlainCrit, new[] { 1, 1 })));
            Assert.AreEqual(55, maeve.Hp);
        }

        [Test]
        public void Lash_HitsAnotherAlly_AndCostsSan()
        {
            PartyMember albert = Albert();
            albert.Bout = BoutKind.Lash;
            albert.BoutTurns = 1;
            PartyMember maeve = Maeve();
            BattleField f = Field(new[] { albert, maeve }, Enemy(EnemyDefs.DeepOne1, "e0"));
            List<BattleEvent> ev = new List<BattleEvent>();
            Assert.IsFalse(f.BeginTurn(albert, new ListRng(2), ev));
            Assert.AreEqual(55 - 10, maeve.Hp);
            Assert.AreEqual(60, maeve.San);
            Assert.AreEqual(BoutKind.None, albert.Bout);
        }

        [Test]
        public void Poison_AppliedThenTicksOnOwnTurns()
        {
            PartyMember maeve = Maeve();
            BattleField f = Field(new[] { Fabian(), maeve }, Enemy(EnemyDefs.DeepOne1, "e0"));
            Assert.IsTrue(Has(Do(f, f.At(Side.Enemy, 0), ActionKind.PoisonAttack, 1, new ListRng()), BattleEventKind.PoisonApplied));
            Assert.AreEqual(PartyMember.PoisonStacks, maeve.Poison);
            List<BattleEvent> ev = new List<BattleEvent>();
            Assert.IsTrue(f.BeginTurn(maeve, new ListRng(2), ev));
            Assert.AreEqual(2, maeve.Poison);
            Assert.AreEqual(55 - 10, maeve.Hp);
            Assert.IsTrue(ev.Exists(e => e.Kind == BattleEventKind.PoisonTick));
        }

        // ---------- 적 행동 선택·대상 ----------

        [Test]
        public void DecideEnemy_NormalTargetsWeighted_532()
        {
            BattleField f = Field(new[] { Albert(), Fabian(), Maeve() }, Enemy(EnemyDefs.DeepOneHybrid1, "h"));
            EnemyUnit e = (EnemyUnit)f.At(Side.Enemy, 0);
            SeededRng rng = new SeededRng(5);
            int[] counts = new int[3];
            for (int i = 0; i < 6000; i++)
            {
                BattleCommand c = f.DecideEnemy(e, rng);
                Assert.AreEqual(ActionKind.Melee, c.Kind);
                counts[c.TargetRank]++;
            }
            Assert.That(counts[0], Is.InRange(2800, 3200));
            Assert.That(counts[1], Is.InRange(1650, 1950));
            Assert.That(counts[2], Is.InRange(1050, 1350));
        }

        [Test]
        public void DecideEnemy_SanUniform_PoisonFrontFirst_EmptyRanksNormalize()
        {
            BattleField f = Field(new[] { Albert(), Fabian(), Maeve() }, Enemy(EnemyDefs.DeepOne1, "d"));
            EnemyUnit e = (EnemyUnit)f.At(Side.Enemy, 0);
            SeededRng rng = new SeededRng(6);
            int[] san = new int[3];
            int sanCount = 0;
            for (int i = 0; i < 12000; i++)
            {
                BattleCommand c = f.DecideEnemy(e, rng);
                if (c.Kind == ActionKind.SanAttack)
                {
                    san[c.TargetRank]++;
                    sanCount++;
                }
                else if (c.Kind == ActionKind.PoisonAttack)
                {
                    Assert.AreEqual(0, c.TargetRank, "독은 앞 칸 우선");
                }
            }
            for (int r = 0; r < 3; r++)
            {
                Assert.That(san[r] / (double)sanCount, Is.InRange(0.30, 0.37), "san rank " + r);
            }
            // 앞 칸이 비면(쓰러져 빠짐) 독은 남은 칸 균등, 일반은 남은 칸 5:3 정규화
            int[] w = { 1, 0, 0 };
            bool[] occ = { false, true, true };
            int[] pick = new int[3];
            SeededRng r2 = new SeededRng(7);
            for (int i = 0; i < 4000; i++)
            {
                pick[PartyBattleRules.PickWeighted(w, occ, r2)]++;
            }
            Assert.AreEqual(0, pick[0]);
            Assert.That(pick[1], Is.InRange(1800, 2200));
            Assert.AreEqual(-1, PartyBattleRules.PickWeighted(w, new bool[3], r2));
        }

        [Test]
        public void DecideEnemy_SingleTarget_DrawsNothingExtra()
        {
            BattleField f = Field(new[] { Fabian() }, Enemy(EnemyDefs.DeepOneHybrid1, "h"));
            CountingRng rng = new CountingRng();
            BattleCommand c = f.DecideEnemy((EnemyUnit)f.At(Side.Enemy, 0), rng);
            Assert.AreEqual(0, rng.Draws, "혼혈 의도 합 0 + 대상 1칸 = 난수 없음(옛 1:1 시퀀스 보존)");
            Assert.AreEqual(0, c.TargetRank);
        }

        [Test]
        public void Resolve_InvalidCommands_ReturnInvalidWithoutRolling()
        {
            PartyMember maeve = Maeve();
            BattleField f = Field(new[] { Albert(), Fabian(), maeve }, Enemy(EnemyDefs.DeepOne1, "e0"));
            CountingRng rng = new CountingRng();
            Assert.IsTrue(Has(Do(f, maeve, ActionKind.Melee, 0, rng), BattleEventKind.Invalid));
            Assert.IsTrue(Has(Do(f, maeve, ActionKind.Ranged, 2, rng), BattleEventKind.Invalid), "빈 칸");
            Assert.IsTrue(Has(Do(f, f.At(Side.Ally, 0), ActionKind.Calm, 1, rng), BattleEventKind.Invalid));
            maeve.Status = UnitStatus.Downed;
            Assert.IsTrue(Has(Do(f, maeve, ActionKind.Guard, 2, rng), BattleEventKind.Invalid), "행동 불가");
            Assert.IsTrue(Has(Do(f, null, ActionKind.Guard, 0, rng), BattleEventKind.Invalid));
            Assert.AreEqual(0, rng.Draws);
        }

        [Test]
        public void Field_RejectsMoreThanThree()
        {
            Assert.Throws<ArgumentException>(() => Field(new[] { Albert(), Fabian(), Maeve(), Albert() }, Enemy(EnemyDefs.DeepOne1, "e0")));
        }

        // ---------- PT-2 리뷰 반영: 룰렛 주입·대응 대기·이벤트·정산·환원 ----------

        private static BattleField ManualField(PartyMember leader, PartyMember other, EnemyUnit enemy)
        {
            return new BattleField(new[] { leader, other }, new[] { enemy }, new BattleOptions { ManualLeaderDefense = true });
        }

        [Test]
        public void PreRolled_SkipsOwnRoll_ButEnemyStillRolls()
        {
            BattleField f = Field(new[] { Albert(), Fabian() }, Enemy(EnemyDefs.DeepOne1, "e0", 50, 100));
            ListRng rng = new ListRng(Cat(PlainFumble, new[] { 1 }));
            BattleEvent[] ev = Do(f, f.At(Side.Ally, 0), ActionKind.Ranged, 0, rng, SuccessLevel.CriticalSuccess);
            Assert.AreEqual(3, rng.Consumed, "자기 판정 6개를 건너뛰고 적 2 + 피해 1 만 소비");
            Assert.AreEqual(BattleEventKind.Hit, ev[0].Kind);
            Assert.AreEqual((int)SuccessLevel.CriticalSuccess, ev[0].ActorLevel);
            Assert.AreEqual((int)SuccessLevel.Fumble, ev[0].TargetLevel);
            ListRng miss = new ListRng(PlainCrit);
            Assert.IsTrue(Has(Do(f, f.Leader, ActionKind.Melee, 0, miss, SuccessLevel.Failure), BattleEventKind.Miss));
            Assert.AreEqual(2, miss.Consumed);
        }

        [Test]
        public void ManualDefense_WaitsForResponse_ThenAppliesEvasionOrCounter()
        {
            PartyMember leader = Fabian();
            BattleField f = ManualField(leader, Albert(), Enemy(EnemyDefs.DeepOne1, "e0", 50, 5));
            CountingRng none = new CountingRng();
            IBattleUnit enemy = f.At(Side.Enemy, 0);
            BattleEvent[] ev = Do(f, enemy, ActionKind.Melee, 0, none);
            Assert.AreEqual(BattleEventKind.ResponseNeeded, ev[0].Kind);
            Assert.AreEqual(0, none.Draws);
            Assert.IsTrue(f.HasPendingResponse);
            Assert.IsTrue(Has(Do(f, enemy, ActionKind.Melee, 0, none), BattleEventKind.Invalid), "대기 중에는 다른 지시 불가");
            List<BattleEvent> r = new List<BattleEvent>();
            f.ResolveResponse(DefenseChoice.Evasion, SuccessLevel.CriticalSuccess, new ListRng(PlainFumble), r);
            Assert.IsTrue(r.Exists(e => e.Kind == BattleEventKind.Evade));
            Assert.IsFalse(f.HasPendingResponse);

            Do(f, enemy, ActionKind.Melee, 0, none);
            r.Clear();
            f.ResolveResponse(DefenseChoice.Counter, SuccessLevel.CriticalSuccess, new ListRng(Cat(PlainFumble, new[] { 3 })), r);
            Assert.IsTrue(r.Exists(e => e.Kind == BattleEventKind.Hit && e.Detail == BattleDetail.Counter && e.ActorId == "fabian"));
            Assert.IsTrue(r.Exists(e => e.Kind == BattleEventKind.EnemyDown));

            r.Clear();
            f.ResolveResponse(DefenseChoice.Evasion, null, none, r);
            Assert.AreEqual(BattleEventKind.Invalid, r[0].Kind, "대기가 없으면 Invalid");
        }

        [Test]
        public void ManualDefense_CounterFailure_LeaderTakesTheHit()
        {
            PartyMember leader = Fabian();
            BattleField f = ManualField(leader, Albert(), Enemy(EnemyDefs.DeepOne1, "e0"));
            Do(f, f.At(Side.Enemy, 0), ActionKind.Melee, 0, new CountingRng());
            List<BattleEvent> r = new List<BattleEvent>();
            f.ResolveResponse(DefenseChoice.Counter, SuccessLevel.Fumble, new ListRng(Cat(PlainCrit, new[] { 2, 2 })), r);
            Assert.AreEqual(50 - 20, leader.Hp);
            BattleEvent hit = r.Find(e => e.Kind == BattleEventKind.Hit);
            Assert.AreEqual("fabian", hit.TargetId);
            Assert.AreEqual(30, hit.Hp, "이벤트에 변화 후 HP");
            Assert.AreEqual(52, hit.San);
            Assert.AreEqual(0, hit.Rank);
        }

        [Test]
        public void Events_CarryRankHpSanCauseAndLevels()
        {
            PartyMember maeve = Maeve();
            BattleField f = Field(new[] { Albert(), Fabian(), maeve }, Enemy(EnemyDefs.DeepOne1, "d", 50, 100));
            BattleEvent[] ev = Do(f, f.At(Side.Enemy, 0), ActionKind.SanAttack, 2, new ListRng(Cat(RollFumble, new[] { 2 })));
            Assert.AreEqual(BattleEventKind.ActionStart, ev[0].Kind);
            Assert.AreEqual(BattleDetail.SanAttack, ev[0].Detail);
            BattleEvent loss = ev[1];
            Assert.AreEqual(BattleEventKind.SanLoss, loss.Kind);
            Assert.AreEqual("d", loss.ActorId, "SanLoss 에 원인");
            Assert.AreEqual(60, loss.San);
            Assert.AreEqual(2, loss.Rank);
            Assert.AreEqual(5, loss.Value);
            BattleEvent[] poison = Do(f, f.At(Side.Enemy, 0), ActionKind.PoisonAttack, 0, new ListRng());
            Assert.AreEqual(BattleDetail.PoisonAttack, poison[0].Detail);
            Assert.AreEqual(BattleEventKind.Invalid, Do(f, f.At(Side.Ally, 0), ActionKind.Throw, 0, new ListRng())[0].Kind, "자리만 확정한 행동");
        }

        [Test]
        public void Events_EnemyDeath_EmitsCompactWithOldRank()
        {
            BattleField f = Field(new[] { Albert(), Fabian() }, Enemy(EnemyDefs.DeepOne1, "e0", 50, 5), Enemy(EnemyDefs.DeepOne1, "e1"));
            BattleEvent[] ev = Do(f, f.At(Side.Ally, 0), ActionKind.Melee, 0, new ListRng(Cat(RollCrit, PlainFumble, new[] { 1 })));
            int down = Array.FindIndex(ev, e => e.Kind == BattleEventKind.EnemyDown);
            int compact = Array.FindIndex(ev, e => e.Kind == BattleEventKind.Compact);
            Assert.That(down, Is.GreaterThanOrEqualTo(0));
            Assert.That(compact, Is.GreaterThan(down));
            Assert.AreEqual("e1", ev[compact].TargetId);
            Assert.AreEqual(1, ev[compact].Value);
            Assert.AreEqual(0, ev[compact].Rank);
        }

        [Test]
        public void Events_SwapReportsNewRank_AndHitComesBeforeDown()
        {
            PartyMember albert = Albert();
            BattleField f = Field(new[] { albert, Fabian() }, Enemy(EnemyDefs.DeepOne1, "e0"));
            BattleEvent[] ev = Do(f, albert, ActionKind.Swap, 1, new ListRng());
            Assert.AreEqual(1, ev[0].Value);
            Assert.AreEqual(0, ev[0].Rank);
            PartyMember weak = Albert(30);
            BattleField g = Field(new[] { Fabian(), weak }, Enemy(EnemyDefs.DeepOne1, "e0"));
            BattleEvent[] hit = Do(g, g.At(Side.Enemy, 0), ActionKind.Melee, 1, HitThatCannotBeEvaded());
            Assert.AreEqual(BattleEventKind.Hit, hit[0].Kind);
            Assert.AreEqual(0, hit[0].Hp);
            Assert.AreEqual(BattleEventKind.Down, hit[1].Kind);
        }

        [Test]
        public void Settle_Leader_DownedWinsToReady_LossAndFledLeaveLeader()
        {
            PartyMember leader = Fabian();
            PartyMember albert = Albert();
            BattleField f = Field(new[] { leader, albert }, Enemy(EnemyDefs.DeepOne1, "e0", 50, 5));
            leader.Status = UnitStatus.Downed;
            leader.Hp = 0;
            Do(f, albert, ActionKind.Melee, 0, new ListRng(Cat(RollCrit, PlainFumble, new[] { 3 })));
            Settle(f);
            Assert.AreEqual(UnitStatus.Ready, leader.Status);
            Assert.AreEqual(PartyBattleRules.StableHp, leader.Hp);
            Assert.IsFalse(leader.Dead);

            PartyMember l2 = Fabian();
            PartyMember a2 = Albert();
            BattleField g = Field(new[] { l2, a2 }, Enemy(EnemyDefs.DeepOne1, "e0"));
            l2.Status = UnitStatus.Fallen;
            Settle(g);
            Assert.AreEqual(UnitStatus.Fallen, l2.Status, "패배한 파비안은 게임오버 경로: 상태 유지");
            Assert.IsFalse(l2.Dead);
        }

        [Test]
        public void Settle_BrokenCompanion_Departs_StatusKept()
        {
            PartyMember maeve = Maeve(55, 0);
            maeve.Status = UnitStatus.Broken;
            PartyMember albert = Albert();
            BattleField f = Field(new[] { Fabian(), albert, maeve }, Enemy(EnemyDefs.DeepOne1, "e0", 50, 5));
            Do(f, f.Leader, ActionKind.Melee, 0, new ListRng(Cat(RollCrit, PlainFumble, new[] { 3 })));
            List<BattleEvent> s = Settle(f);
            Assert.IsTrue(s.Exists(e => e.Kind == BattleEventKind.Departed && e.TargetId == "maeve"));
            Assert.AreEqual(UnitStatus.Broken, maeve.Status);
            Assert.IsFalse(maeve.Dead);
        }

        [Test]
        public void Constructor_RejectsNullDuplicateAndTwoLeaders_OptionsOverride()
        {
            EnemyUnit e = Enemy(EnemyDefs.DeepOne1, "e0");
            Assert.Throws<ArgumentNullException>(() => new BattleField(null, new[] { e }));
            Assert.Throws<ArgumentException>(() => Field(new PartyMember[] { Albert(), null }, e));
            Assert.Throws<ArgumentException>(() => Field(new[] { Albert(), Albert() }, e), "같은 Id");
            Assert.Throws<ArgumentException>(() => Field(new[] { Fabian(), Fabian() }, e), "파비안 둘");
            Assert.Throws<ArgumentException>(() => Field(new[] { Albert() }, e, Enemy(EnemyDefs.DeepOne1, "e0")), "적 Id 중복");
            BattleField solo = new BattleField(new[] { Fabian() }, new[] { e }, new BattleOptions { LeaderDownRule = true, LeaderBoutEnabled = true });
            Assert.IsTrue(solo.LeaderDownRule);
            Assert.IsTrue(solo.LeaderBoutEnabled);
            Assert.IsFalse(solo.ManualLeaderDefense);
            Assert.IsTrue(Field(new[] { Fabian(), Albert() }, Enemy(EnemyDefs.DeepOne1, "x")).LeaderDownRule);
        }

        // ---------- 환원(PartyBattleBridge) ----------

        [Test]
        public void Bridge_NextMorning_FromEveryPhase()
        {
            Assert.AreEqual(5, PartyBattleBridge.NextMorning(4));
            Assert.AreEqual(9, PartyBattleBridge.NextMorning(5));
            Assert.AreEqual(9, PartyBattleBridge.NextMorning(6));
            Assert.AreEqual(9, PartyBattleBridge.NextMorning(7));
            Assert.AreEqual(9, PartyBattleBridge.NextMorning(8));
        }

        private static GameState JoinedBoth()
        {
            GameState s = GameState.New();
            PartyState.Join(s, CompanionDefs.Albert);
            PartyState.Join(s, CompanionDefs.Maeve);
            return s;
        }

        private static GameState RoundTrip(GameState a)
        {
            GameState b;
            Assert.AreEqual(SCoW.Core.Save.SaveError.None, SCoW.Core.Save.SaveCodec.TryDecodeState(SCoW.Core.Save.SaveCodec.EncodeState(a), out b));
            return b;
        }

        [Test]
        public void Bridge_BuildRoster_FollowsOrder_SkipsStandbyDeadAndNotDue()
        {
            GameState s = JoinedBoth();
            PartyMember leader = Fabian();
            List<PartyMember> roster = PartyBattleBridge.BuildRoster(s, leader, 10);
            CollectionAssert.AreEqual(new[] { "fabian", "albert", "maeve" }, roster.ConvertAll(m => m.Id));
            Assert.AreSame(leader, roster[0]);
            Assert.AreEqual(75, roster[1].Hp);
            s.SetFlag(PartyKeys.Standby("albert"), true);
            PartyState.Kill(s, "maeve");
            CollectionAssert.AreEqual(new[] { "fabian" }, PartyBattleBridge.BuildRoster(s, leader, 10).ConvertAll(m => m.Id));
            s.SetFlag(PartyKeys.Standby("albert"), false);
            PartyState.SetStatus(s, "albert", UnitStatus.Recovering);
            s.SetCounter(PartyKeys.BackAt("albert"), 12);
            PartyState.SetHp(s, CompanionDefs.Albert, 1);
            Assert.AreEqual(1, PartyBattleBridge.BuildRoster(s, leader, 11).Count, "복귀 시각 전");
            List<PartyMember> back = PartyBattleBridge.BuildRoster(s, leader, 12);
            Assert.AreEqual(2, back.Count);
            Assert.AreEqual(75 / 2, back[1].Hp);
            Assert.AreEqual(UnitStatus.Ready, PartyState.GetStatus(s, "albert"));
        }

        [Test]
        public void Bridge_WriteBack_RecoveringBrokenAndDeath_ThenSurvivesSave()
        {
            GameState s = JoinedBoth();
            PartyMember leader = Fabian();
            List<PartyMember> roster = PartyBattleBridge.BuildRoster(s, leader, 10);
            PartyMember albert = roster[1];
            PartyMember maeve = roster[2];
            albert.Status = UnitStatus.Recovering;
            albert.Hp = 1;
            maeve.San = 0;
            maeve.Status = UnitStatus.Broken;
            PartyBattleBridge.WriteBack(s, roster, 10);
            Assert.AreEqual(UnitStatus.Recovering, PartyState.GetStatus(s, "albert"));
            Assert.AreEqual(1, PartyState.GetHp(s, "albert"));
            Assert.AreEqual(11, s.GetCounter(PartyKeys.BackAt("albert")), "다음 시간대");
            Assert.AreEqual(UnitStatus.Broken, PartyState.GetStatus(s, "maeve"));
            Assert.AreEqual(13, s.GetCounter(PartyKeys.BackAt("maeve")), "t=10(낮) -> 다음 일차 아침 13");
            GameState loaded = RoundTrip(s);
            Assert.AreEqual(UnitStatus.Broken, PartyState.GetStatus(loaded, "maeve"));
            List<PartyMember> next = PartyBattleBridge.BuildRoster(loaded, leader, 13);
            Assert.AreEqual(3, next.Count, "알버트(다음 시간대)·메이브(다음 아침) 모두 복귀");
            Assert.AreEqual(PartyBattleBridge.BrokenReturnSan, next[2].San, "재합류 SAN +5");

            albert.Dead = true;
            PartyBattleBridge.WriteBack(s, roster, 14);
            Assert.IsTrue(PartyState.IsDead(s, "albert"));
            Assert.AreEqual(0, PartyState.GetHp(s, "albert"));
            Assert.IsFalse(PartyState.ActiveCompanions(s).Contains(CompanionDefs.Albert));
        }
    }
}
