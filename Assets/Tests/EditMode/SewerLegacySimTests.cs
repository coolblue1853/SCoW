using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SCoW.Core.Rules;
using UnityEngine;

namespace SCoW.Tests
{
    /// <summary>SEWER-1 S0: 출시판 하수도 방 생성(TimeManagere.MakeSewer 의 결정 트리)을 그대로 옮겨 이동 한 번당 결과 분포를 잰다.
    /// 게임 코드는 건드리지 않는다. 이 시뮬은 옛 판정 순서·난수 소비 수를 보존한 사본이고, S1 이후 옛 코드가 사라져도 "현행 실측" 근거로 남긴다.</summary>
    public sealed class SewerLegacySimTests
    {
        private enum Room { Symbol, House, Item, Empty, Cage, Enemy }

        private sealed class Run
        {
            public int Moves;
            public int Enemies;
            public int House;
            public int Cage;
            public int Item;
            public int Empty;
            public bool Reached;
            public int FirstEnemyMove;
        }

        // 옛 MakeSewer 와 같은 결정. luk/intl 은 DataBaseManager 값, hasMap 은 gainMap.
        private static Room Roll(IRng rng, int luk, int intl, bool hasMap, ref int enemyCounter, ref bool houseIn)
        {
            int randLuk = rng.Next(1, 101);
            if (!hasMap)
            {
                if (randLuk < luk)
                {
                    if (randLuk < luk / 3)
                    {
                        return Room.Symbol;
                    }
                    int r = rng.Next(0, 6);
                    if (r == 0)
                    {
                        return Room.House;
                    }
                    if (r == 1)
                    {
                        return Room.Item;
                    }
                    if (r == 2)
                    {
                        return Room.Cage;
                    }
                    return Room.Empty;
                }
                return Fail(rng, ref enemyCounter, ref houseIn);
            }
            int randIntel = rng.Next(1, 101);
            if (randLuk < luk || randIntel < intl)
            {
                if (randLuk < luk / 3 || randIntel < intl / 3)
                {
                    return Room.Symbol;
                }
                int r = rng.Next(0, 5);
                if (r == 0)
                {
                    return Room.House;
                }
                return r == 1 ? Room.Item : Room.Empty;
            }
            return Fail(rng, ref enemyCounter, ref houseIn);
        }

        private static Room Fail(IRng rng, ref int enemyCounter, ref bool houseIn)
        {
            if (enemyCounter > 2 && !houseIn)
            {
                return Room.House;
            }
            if (enemyCounter > 2)
            {
                return rng.Next(0, 3) == 0 ? Room.House : Room.Empty;
            }
            enemyCounter += 1;
            rng.Next(0, 3);
            return Room.Enemy;
        }

        private static Run Simulate(int seed, int luk, int intl, bool hasMap, int cap)
        {
            SeededRng rng = new SeededRng(seed);
            Run run = new Run();
            int enemyCounter = 0;
            bool houseIn = false;
            while (run.Moves < cap && !run.Reached)
            {
                run.Moves++;
                Room room = Roll(rng, luk, intl, hasMap, ref enemyCounter, ref houseIn);
                switch (room)
                {
                    case Room.Symbol: run.Reached = true; break;
                    case Room.House: run.House++; houseIn = true; break;
                    case Room.Cage: run.Cage++; break;
                    case Room.Item: run.Item++; break;
                    case Room.Empty: run.Empty++; break;
                    case Room.Enemy:
                        run.Enemies++;
                        if (run.FirstEnemyMove == 0)
                        {
                            run.FirstEnemyMove = run.Moves;
                        }
                        break;
                }
            }
            return run;
        }

        private static string Table(string title, int luk, int intl, bool hasMap, out double mean)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("### " + title);
            sb.AppendLine("run | moves | enemy | house | cage | item | empty | firstEnemyMove");
            int total = 0;
            for (int seed = 1; seed <= 20; seed++)
            {
                Run r = Simulate(seed, luk, intl, hasMap, 500);
                total += r.Moves;
                sb.AppendLine(seed + " | " + r.Moves + " | " + r.Enemies + " | " + r.House + " | " + r.Cage + " | " + r.Item + " | " + r.Empty + " | " + r.FirstEnemyMove);
            }
            mean = total / 20.0;
            sb.AppendLine("mean moves " + mean.ToString("0.0"));
            return sb.ToString();
        }

        [Test]
        public void Legacy_enemy_cap_is_three_and_symbol_is_reachable_in_20_runs()
        {
            for (int seed = 1; seed <= 20; seed++)
            {
                Run r = Simulate(seed, 50, 50, false, 500);
                Assert.IsTrue(r.Reached, "seed " + seed + " symbol not reached");
                Assert.LessOrEqual(r.Enemies, 3, "seed " + seed + " enemy cap");
            }
        }

        [Test]
        public void Legacy_distribution_report()
        {
            double a;
            double b;
            double c;
            double d;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[SewerLegacySim] 20 runs each, seeds 1..20");
            sb.Append(Table("luk 50, no map", 50, 50, false, out a));
            sb.Append(Table("luk 30, no map", 30, 50, false, out b));
            sb.Append(Table("luk 70, no map", 70, 50, false, out c));
            sb.Append(Table("luk 50 + intl 50, map", 50, 50, true, out d));
            Debug.Log(sb.ToString());
            Assert.Greater(a, 1.0);
            Assert.Greater(b, a * 0.8);
            Assert.Less(d, a * 1.5);
        }
    }
}
