using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SCoW.Core.Rules;
using UnityEngine;

namespace SCoW.Tests
{
    /// <summary>
    /// C-2 P14a: BattleEngine 규칙 테스트 + 옛 식 동치 검증.
    /// 옛 식(Legacy*)은 이행 전 BattleManager/EnemyClass/InSewerEnemy/BattleRollet 의 식을 그대로 옮긴 것이고,
    /// UnityEngine.Random.InitState 시드 동일 조건에서 엔진(UnityRng)과 값·난수 소비 순서가 같아야 한다.
    /// 전투 로그 골든(Golden/BattleP14a.golden.txt)은 헤드리스 시뮬레이션(SeededRng) 결과.
    /// </summary>
    public sealed class BattleEngineTests
    {
        private sealed class UnityEngineRng : IRng
        {
            public static readonly UnityEngineRng I = new UnityEngineRng();
            public int Next(int minInclusive, int maxExclusive) { return UnityEngine.Random.Range(minInclusive, maxExclusive); }
        }

        private const string GoldenPath = "Assets/Tests/EditMode/Golden/BattleP14a.golden.txt";

        // ---------- 옛 식 (이행 전 코드 그대로) ----------

        private static int[] LegacyDamageSequence()
        {
            return new[]
            {
                UnityEngine.Random.Range(1, 4) * 5,
                (UnityEngine.Random.Range(1, 4) + 1) * 5,
                (UnityEngine.Random.Range(1, 4) + UnityEngine.Random.Range(1, 4)) * 5,
                UnityEngine.Random.Range(1, 3) * 5,
                (UnityEngine.Random.Range(1, 5) + 2) * 5,
                (UnityEngine.Random.Range(1, 9)) * 5,
                (UnityEngine.Random.Range(1, 7)) * 5,
                (UnityEngine.Random.Range(1, 11) + 2) * 5,
                (UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7) + 2) * 5,
                (UnityEngine.Random.Range(1, 6) + UnityEngine.Random.Range(1, 6) + UnityEngine.Random.Range(1, 6) + UnityEngine.Random.Range(1, 6)) * 5,
                (UnityEngine.Random.Range(1, 3)) * 5
            };
        }

        private static int[] EngineDamageSequence()
        {
            IRng r = UnityEngineRng.I;
            return new[]
            {
                BattleDamage.Hit1D3.Roll(r),
                BattleDamage.Hit1D3Plus1.Roll(r),
                BattleDamage.Hit2D3.Roll(r),
                BattleDamage.Poison.Roll(r),
                BattleDamage.Dagger.Roll(r),
                BattleDamage.Bat.Roll(r),
                BattleDamage.SmallPistol.Roll(r),
                BattleDamage.Revolver.Roll(r),
                BattleDamage.Rifle.Roll(r),
                BattleDamage.Shotgun.Roll(r),
                BattleDamage.Rock.Roll(r)
            };
        }

        [Test]
        public void Damage_MatchesLegacyExpressions_SameSeedSameDraws()
        {
            for (int seed = 1; seed <= 200; seed++)
            {
                UnityEngine.Random.InitState(seed);
                int[] legacy = LegacyDamageSequence();
                int legacyNext = UnityEngine.Random.Range(0, 1000000);
                UnityEngine.Random.InitState(seed);
                int[] engine = EngineDamageSequence();
                int engineNext = UnityEngine.Random.Range(0, 1000000);
                CollectionAssert.AreEqual(legacy, engine, "seed " + seed);
                Assert.AreEqual(legacyNext, engineNext, "draw count differs, seed " + seed);
            }
        }

        [Test]
        public void Damage_Bounds()
        {
            AssertBounds(BattleDamage.Hit1D3, 5, 15);
            AssertBounds(BattleDamage.Hit1D3Plus1, 10, 20);
            AssertBounds(BattleDamage.Hit2D3, 10, 30);
            AssertBounds(BattleDamage.Poison, 5, 10);
            AssertBounds(BattleDamage.Dagger, 15, 30);
            AssertBounds(BattleDamage.Bat, 5, 40);
            AssertBounds(BattleDamage.SmallPistol, 5, 30);
            AssertBounds(BattleDamage.Revolver, 15, 60);
            AssertBounds(BattleDamage.Rifle, 20, 70);
            AssertBounds(BattleDamage.Shotgun, 20, 100);
            AssertBounds(BattleDamage.Rock, 5, 10);
            AssertBounds(BattleDamage.Molotov, 20, 70);
        }

        private static void AssertBounds(DiceExpr expr, int min, int max)
        {
            Assert.AreEqual(min, expr.Min);
            Assert.AreEqual(max, expr.Max);
            SeededRng rng = new SeededRng(7);
            for (int i = 0; i < 2000; i++)
            {
                int v = expr.Roll(rng);
                Assert.That(v, Is.InRange(min, max));
                Assert.AreEqual(0, v % 5);
            }
        }

        [Test]
        public void EnemyRoll_MatchesLegacyStatOrder()
        {
            for (int seed = 1; seed <= 50; seed++)
            {
                UnityEngine.Random.InitState(seed);
                int[] hybrid =
                {
                    UnityEngine.Random.Range(45, 80), UnityEngine.Random.Range(45, 80), UnityEngine.Random.Range(45, 80),
                    UnityEngine.Random.Range(5, 10), UnityEngine.Random.Range(40, 55), UnityEngine.Random.Range(30, 60)
                };
                int[] deep =
                {
                    UnityEngine.Random.Range(60, 80), UnityEngine.Random.Range(55, 75), UnityEngine.Random.Range(45, 55),
                    UnityEngine.Random.Range(40, 60), UnityEngine.Random.Range(100, 120), UnityEngine.Random.Range(30, 60)
                };
                UnityEngine.Random.InitState(seed);
                EnemyState h = new EnemyState(EnemyDefs.DeepOneHybrid1);
                h.Roll(UnityEngineRng.I);
                EnemyState d = new EnemyState(EnemyDefs.DeepOne1);
                d.Roll(UnityEngineRng.I);
                CollectionAssert.AreEqual(hybrid, new[] { h.Str, h.Intl, h.Dex, h.Mp, h.FullHp, h.San });
                CollectionAssert.AreEqual(deep, new[] { d.Str, d.Intl, d.Dex, d.Mp, d.FullHp, d.San });
                Assert.AreEqual(h.FullHp, h.NowHp);
                Assert.AreEqual(50, h.MartialArts);
                Assert.AreEqual(25, h.Evasion);
                Assert.AreEqual(45, d.MartialArts);
                Assert.AreEqual(50, d.Swordsmanship);
                Assert.AreEqual(25, d.Evasion);
            }
        }

        [Test]
        public void Intent_MatchesLegacyBranching()
        {
            for (int seed = 1; seed <= 100; seed++)
            {
                UnityEngine.Random.InitState(seed);
                int r = UnityEngine.Random.Range(0, 4);
                EnemyIntent expected = r == 0 ? EnemyIntent.SanAttack : (r == 1 ? EnemyIntent.PoisonAttack : EnemyIntent.Normal);
                UnityEngine.Random.InitState(seed);
                Assert.AreEqual(expected, BattleEngine.ChooseIntent(EnemyDefs.DeepOne1, UnityEngineRng.I));
                int after = UnityEngine.Random.Range(0, 1000000);
                UnityEngine.Random.InitState(seed);
                UnityEngine.Random.Range(0, 4);
                Assert.AreEqual(UnityEngine.Random.Range(0, 1000000), after);
            }
            // 난수를 쓰지 않는 슬롯
            UnityEngine.Random.InitState(3);
            int probe = UnityEngine.Random.Range(0, 1000000);
            UnityEngine.Random.InitState(3);
            Assert.AreEqual(EnemyIntent.Deftness, BattleEngine.ChooseIntent(EnemyDefs.DeepOneHybrid3, UnityEngineRng.I));
            Assert.AreEqual(EnemyIntent.Normal, BattleEngine.ChooseIntent(EnemyDefs.DeepOneHybrid1, UnityEngineRng.I));
            Assert.AreEqual(probe, UnityEngine.Random.Range(0, 1000000));
        }

        [Test]
        public void SewerDisguise_MatchesLegacy()
        {
            for (int seed = 1; seed <= 300; seed++)
            {
                int dex = 20 + seed % 70;
                UnityEngine.Random.InitState(seed);
                bool legacyFail = false;
                if (UnityEngine.Random.Range(1, 101) < 55)
                {
                    if (!(dex > UnityEngine.Random.Range(1, 101)))
                    {
                        legacyFail = true;
                    }
                }
                int legacyNext = UnityEngine.Random.Range(0, 1000000);
                UnityEngine.Random.InitState(seed);
                Assert.AreEqual(legacyFail, BattleEngine.SewerDisguiseFails(dex, UnityEngineRng.I), "seed " + seed);
                Assert.AreEqual(legacyNext, UnityEngine.Random.Range(0, 1000000));
            }
        }

        [Test]
        public void Gauge_MatchesLegacyFloatMath()
        {
            float[] dexes = { 45f, 62f, 79f, 50f, 0f, 100f };
            foreach (float dex in dexes)
            {
                foreach (bool player in new[] { true, false })
                {
                    float yLegacy = -130f;
                    float yNew = -130f;
                    int stepsLegacy = 0;
                    int stepsNew = 0;
                    while (true)
                    {
                        if (yLegacy < 130)
                        {
                            yLegacy = yLegacy + ((1 + (dex / 100)) * (player ? 2f : 1.9f));
                            stepsLegacy++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    while (true)
                    {
                        if (BattleEngine.IsGaugeFull(yNew))
                        {
                            break;
                        }
                        yNew = BattleEngine.StepGauge(yNew, dex, player);
                        stepsNew++;
                    }
                    Assert.AreEqual(stepsLegacy, stepsNew, "dex " + dex);
                }
            }
        }

        // 옛 BattleRollet.GetFinalResult if 사슬(유리/불리 두 종류만 옮김)
        private static string LegacyFavorable(string p, string e)
        {
            if (p == "Result : Critical Success") { return "Success"; }
            if (p == "Result : Success" && e == "Result : Success") { return "Success"; }
            if (p == "Result : Success" && e == "Result : Failure") { return "Success"; }
            if (p == "Result : Success" && e == "Result : Fumble") { return "Success"; }
            if (p == "Result : Failure" && e == "Result : Failure") { return "Success"; }
            if (p == "Result : Failure" && e == "Result : Fumble") { return "Success"; }
            if (p == "Result : Fumble" && e == "Result : Fumble") { return "Success"; }
            return "Failure";
        }

        private static string LegacyCounter(string p, string e)
        {
            if (p == "Result : Critical Success" && e != "Result : Critical Success") { return "Success"; }
            if (p == "Result : Success" && e != "Result : Critical Success" && e != "Result : Success") { return "Success"; }
            if (p == "Result : Failure" && e != "Result : Critical Success" && e != "Result : Success" && e != "Result : Failure") { return "Success"; }
            return "Failure";
        }

        [Test]
        public void Contest_AllSixteenCombos_MatchLegacyChains()
        {
            SuccessLevel[] all = { SuccessLevel.Fumble, SuccessLevel.Failure, SuccessLevel.Success, SuccessLevel.CriticalSuccess };
            ContestKind[] favorable = { ContestKind.Evasion, ContestKind.MartialArts, ContestKind.Swordsmanship, ContestKind.RunAway };
            foreach (SuccessLevel p in all)
            {
                foreach (SuccessLevel e in all)
                {
                    string ps = SkillCheck.ToLegacyString(p);
                    string es = SkillCheck.ToLegacyString(e);
                    foreach (ContestKind k in favorable)
                    {
                        Assert.AreEqual(LegacyFavorable(ps, es) == "Success", BattleEngine.ResolveContest(k, p, e), k + " " + ps + " vs " + es);
                    }
                    Assert.AreEqual(LegacyCounter(ps, es) == "Success", BattleEngine.ResolveContest(ContestKind.CounterAttack, p, e), "counter " + ps + " vs " + es);
                }
            }
        }

        [Test]
        public void Contest_ParseAndLabels()
        {
            ContestKind k;
            Assert.IsTrue(BattleEngine.TryParseContest("evasion", out k));
            Assert.AreEqual("Evasion", BattleEngine.ContestLabel(k));
            Assert.IsTrue(BattleEngine.TryParseContest("MA_attack_Counter", out k));
            Assert.AreEqual("MartialArt", BattleEngine.ContestLabel(k));
            Assert.IsTrue(BattleEngine.TryParseContest("RunAway", out k));
            Assert.AreEqual("Runaway", BattleEngine.ContestLabel(k));
            Assert.IsFalse(BattleEngine.TryParseContest("dialog", out k));
            foreach (SuccessLevel l in new[] { SuccessLevel.Fumble, SuccessLevel.Failure, SuccessLevel.Success, SuccessLevel.CriticalSuccess })
            {
                Assert.AreEqual(l, BattleEngine.ParseLegacyLevel(SkillCheck.ToLegacyString(l)));
            }
        }

        [Test]
        public void EnemyState_DamageAndDeathBoundary()
        {
            EnemyState e = new EnemyState(EnemyDefs.DeepOneHybrid1);
            e.Roll(new SeededRng(1));
            int hp = e.NowHp;
            e.TakeDamage(hp - 1);
            Assert.IsFalse(e.IsDead);
            e.TakeDamage(1);
            Assert.IsTrue(e.IsDead);
            e.TakeDamage(5);
            Assert.IsTrue(e.IsDead);
        }

        [Test]
        public void Poison_DamageIs1d2Times5()
        {
            SeededRng rng = new SeededRng(5);
            for (int i = 0; i < 50; i++)
            {
                Assert.That(BattleDamage.Poison.Roll(rng), Is.InRange(5, 10));
            }
        }

        [Test]
        public void EnemyDefs_LookupAndOrder()
        {
            Assert.AreEqual(5, EnemyDefs.All.Length);
            for (int i = 0; i < EnemyDefs.All.Length; i++)
            {
                Assert.AreSame(EnemyDefs.All[i], EnemyDefs.Get(EnemyDefs.All[i].Id));
                Assert.AreEqual(i, EnemyDefs.IndexOf(EnemyDefs.All[i].Id));
                Assert.AreEqual(3, EnemyDefs.All[i].TargetWeights.Length);
            }
            Assert.IsNull(EnemyDefs.Get("Nope"));
            Assert.AreEqual(-1, EnemyDefs.IndexOf("Nope"));
            Assert.AreSame(EnemyDefs.DeepOne1, EnemyDefs.All[4]);
        }

        [Test]
        public void Intent_WeightsSplitIntervals()
        {
            EnemyDef d = new EnemyDef { Intent = new IntentWeights(2, 3, 5) };
            int[] counts = new int[3];
            SeededRng rng = new SeededRng(9);
            for (int i = 0; i < 10000; i++)
            {
                EnemyIntent intent = BattleEngine.ChooseIntent(d, rng);
                counts[intent == EnemyIntent.SanAttack ? 0 : (intent == EnemyIntent.PoisonAttack ? 1 : 2)]++;
            }
            Assert.That(counts[0], Is.InRange(1800, 2200));
            Assert.That(counts[1], Is.InRange(2800, 3200));
            Assert.That(counts[2], Is.InRange(4800, 5200));
            Assert.AreEqual(EnemyIntent.Normal, BattleEngine.ChooseIntent(new EnemyDef(), rng));
        }

        // ---------- 헤드리스 시뮬레이션 + 골든 ----------

        private sealed class PlayerCfg
        {
            public int Hp;
            public int Dex;
            public int MartialArts;
            public int Evasion;
        }

        private static string Lv(SuccessLevel l)
        {
            return l.ToString();
        }

        private static SuccessLevel EnemyRoll(IRng rng, int skill)
        {
            int u = rng.Next(0, 10);
            int t = rng.Next(0, 10) * 10;
            int v = SkillCheck.ResolveValue(Condition.Nomal, new DiceFaces(u, t, 0, 0, 0, 0));
            return SkillCheck.Evaluate(v, skill);
        }

        /// <summary>플레이어는 항상 무술로 첫 생존 적을 치고, 적 차례엔 항상 회피. 게이지는 옛 TimeWait 처럼 플레이어 -> 적 슬롯 순.</summary>
        private static List<string> Simulate(string name, int seed, PlayerCfg pc, EnemyDef[] defs)
        {
            SeededRng rng = new SeededRng(seed);
            List<string> log = new List<string>();
            log.Add("== " + name + " seed=" + seed);
            EnemyState[] es = new EnemyState[defs.Length];
            float[] gy = new float[defs.Length];
            for (int i = 0; i < defs.Length; i++)
            {
                es[i] = new EnemyState(defs[i]);
                es[i].Roll(rng);
                gy[i] = -130f;
                log.Add(defs[i].Id + " hp=" + es[i].FullHp + " dex=" + es[i].Dex);
            }
            int php = pc.Hp;
            float py = -130f;
            int poison = 0;
            string result = "TIMEOUT";
            for (int tick = 0; tick < 20000; tick++)
            {
                bool allDead = true;
                for (int i = 0; i < es.Length; i++)
                {
                    if (!es[i].IsDead) { allDead = false; }
                }
                if (allDead) { result = "WIN"; break; }
                if (php <= 0) { result = "LOSE"; break; }
                int who = -2;
                if (BattleEngine.IsGaugeFull(py))
                {
                    py = BattleEngine.GaugeReset;
                    who = -1;
                }
                else
                {
                    py = BattleEngine.StepGauge(py, pc.Dex, true);
                }
                for (int i = 0; i < es.Length && who == -2; i++)
                {
                    if (es[i].IsDead) { continue; }
                    if (BattleEngine.IsGaugeFull(gy[i]))
                    {
                        gy[i] = BattleEngine.GaugeReset;
                        who = i;
                    }
                    else
                    {
                        gy[i] = BattleEngine.StepGauge(gy[i], es[i].Dex, false);
                    }
                }
                if (who == -2) { continue; }
                if (who == -1)
                {
                    int tickDmg = poison > 0 ? BattleDamage.Poison.Roll(rng) : 0;
                    if (tickDmg > 0)
                    {
                        poison--;
                        php -= tickDmg;
                        log.Add("t" + tick + " poison -" + tickDmg + " php=" + php);
                    }
                    int target = 0;
                    while (es[target].IsDead) { target++; }
                    SuccessLevel pl = SkillCheck.Roll(rng, pc.MartialArts, Condition.Nomal).Level;
                    SuccessLevel el = EnemyRoll(rng, es[target].MartialArts);
                    bool ok = BattleEngine.ResolveContest(ContestKind.MartialArts, pl, el);
                    if (ok)
                    {
                        int dmg = BattleDamage.Martial.Roll(rng);
                        es[target].TakeDamage(dmg);
                        log.Add("t" + tick + " P->" + defs[target].Id + " " + Lv(pl) + "/" + Lv(el) + " hit " + dmg + " hp=" + es[target].NowHp);
                    }
                    else
                    {
                        int dmg = es[target].Def.AttackDamage.Roll(rng);
                        php -= dmg;
                        log.Add("t" + tick + " P->" + defs[target].Id + " " + Lv(pl) + "/" + Lv(el) + " counter " + dmg + " php=" + php);
                    }
                }
                else
                {
                    EnemyState e = es[who];
                    EnemyIntent intent = BattleEngine.ChooseIntent(e.Def, rng);
                    if (intent == EnemyIntent.SanAttack)
                    {
                        log.Add("t" + tick + " " + e.Def.Id + " san-attack");
                    }
                    else if (intent == EnemyIntent.PoisonAttack)
                    {
                        poison = 3;
                        log.Add("t" + tick + " " + e.Def.Id + " poison-attack");
                    }
                    else
                    {
                        SuccessLevel pl = SkillCheck.Roll(rng, pc.Evasion, Condition.Nomal).Level;
                        SuccessLevel el = EnemyRoll(rng, e.MartialArts);
                        bool ok = BattleEngine.ResolveContest(ContestKind.Evasion, pl, el);
                        if (ok)
                        {
                            log.Add("t" + tick + " " + e.Def.Id + " " + intent + " evaded " + Lv(pl) + "/" + Lv(el));
                        }
                        else
                        {
                            int dmg = e.Def.AttackDamage.Roll(rng);
                            php -= dmg;
                            log.Add("t" + tick + " " + e.Def.Id + " " + intent + " hit " + dmg + " " + Lv(pl) + "/" + Lv(el) + " php=" + php);
                        }
                    }
                }
            }
            log.Add("RESULT " + result + " php=" + php);
            return log;
        }

        private static readonly EnemyDef[] Road = { EnemyDefs.DeepOneHybrid1, EnemyDefs.DeepOneHybrid2, EnemyDefs.DeepOneHybrid3 };
        private static readonly EnemyDef[] Sewer = { EnemyDefs.DeepOneHybrid4, EnemyDefs.DeepOne1 };

        private static List<string> BuildAll(out string[] results)
        {
            PlayerCfg strong = new PlayerCfg { Hp = 600, Dex = 70, MartialArts = 90, Evasion = 80 };
            PlayerCfg weak = new PlayerCfg { Hp = 20, Dex = 40, MartialArts = 5, Evasion = 5 };
            List<string> all = new List<string>();
            string[] names = { "road-win", "road-lose", "sewer-win", "sewer-lose" };
            List<string>[] runs =
            {
                Simulate(names[0], 11, strong, Road),
                Simulate(names[1], 12, weak, Road),
                Simulate(names[2], 13, strong, Sewer),
                Simulate(names[3], 14, weak, Sewer)
            };
            results = new string[4];
            for (int i = 0; i < runs.Length; i++)
            {
                all.AddRange(runs[i]);
                results[i] = runs[i][runs[i].Count - 1];
            }
            return all;
        }

        [Test]
        public void Simulation_EndsInExpectedWinAndLose()
        {
            string[] r;
            BuildAll(out r);
            StringAssert.StartsWith("RESULT WIN", r[0]);
            StringAssert.StartsWith("RESULT LOSE", r[1]);
            StringAssert.StartsWith("RESULT WIN", r[2]);
            StringAssert.StartsWith("RESULT LOSE", r[3]);
        }

        [Test]
        public void BattleLog_MatchesGolden()
        {
            string[] r;
            string text = string.Join("\n", BuildAll(out r).ToArray()) + "\n";
            string full = Path.Combine(Path.GetDirectoryName(Application.dataPath), GoldenPath);
            if (!File.Exists(full))
            {
                File.WriteAllText(full, text, new UTF8Encoding(false));
                Assert.Inconclusive("골든 파일을 새로 만들었다. 다시 실행해 대조한다: " + GoldenPath);
            }
            string golden = File.ReadAllText(full).Replace("\r\n", "\n");
            Assert.AreEqual(golden, text);
        }
    }
}
