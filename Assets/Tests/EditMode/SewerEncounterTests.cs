using NUnit.Framework;
using SCoW.Core.Rules;
using SCoW.Core.Save;
using SCoW.Core.Sewer;
using SCoW.Core.State;

namespace SCoW.Tests
{
    /// <summary>SEWER-1 S4: 조우 3회 배치, 전조 거리·연출 계획, 휴식 구역 규칙(시야·깜빡임·SAN 회복).</summary>
    public sealed class SewerEncounterTests
    {
        private static SewerGraph NewGraph(out GameState s)
        {
            s = GameState.New();
            SewerGraph g = SewerGraph.Create(SewerState.EnsureSeed(s, new SeededRng(7)));
            SewerNav.Begin(s, g);
            return g;
        }

        [Test]
        public void Encounters_are_placed_three_times_on_data_rooms_only()
        {
            SewerGraph g;
            GameState s;
            g = NewGraph(out s);
            int count = 0;
            for (int i = 0; i < g.Rooms.Count; i++)
            {
                if (g.Rooms[i].HasEnemy)
                {
                    count++;
                    Assert.IsNotNull(SewerEncounters.ByRoom(g.Rooms[i].Id));
                }
            }
            Assert.AreEqual(3, count);
            Assert.AreEqual(3, SewerEncounters.Count);
            Assert.AreEqual("z2b_1", SewerGraph.EncounterRoom(1));
            Assert.AreEqual("z3_2", SewerGraph.EncounterRoom(2));
            Assert.AreEqual("z5_1", SewerGraph.EncounterRoom(3));
        }

        [Test]
        public void First_deep_one_dialog_is_on_encounter_one()
        {
            Assert.AreEqual("Sewer_FirstDeepOne", SewerEncounters.DialogFor(1));
            Assert.AreEqual("Sewer_EnemySpawn", SewerEncounters.DialogFor(2));
            Assert.AreEqual("Sewer_EnemySpawn", SewerEncounters.DialogFor(3));
            Assert.AreEqual("Sewer_EnemySpawn", SewerEncounters.DialogFor(99));
        }

        [Test]
        public void Omen_distance_counts_rooms_before_an_undefeated_encounter()
        {
            GameState s;
            SewerGraph g = NewGraph(out s);
            Assert.AreEqual(0, SewerEncounters.OmenDistance(s, g, "z1_1"));
            Assert.AreEqual(2, SewerEncounters.OmenDistance(s, g, "z1_2"));
            Assert.AreEqual(1, SewerEncounters.OmenDistance(s, g, "z1_3"));
            Assert.AreEqual(0, SewerEncounters.OmenDistance(s, g, "z2b_1"), "조우 방 자신은 전조 없이 바로 전투");
            Assert.AreEqual(1, SewerEncounters.OmenDistance(s, g, "z3_1"));
            Assert.AreEqual(2, SewerEncounters.OmenDistance(s, g, "z2b_3"));
            Assert.AreEqual(0, SewerEncounters.OmenDistance(s, g, "z2a_1"), "뒤로 가는 문은 전조에 안 센다");
        }

        [Test]
        public void Omen_moves_on_after_defeat_and_points_to_next_encounter()
        {
            GameState s;
            SewerGraph g = NewGraph(out s);
            SewerState.Defeat(s, "z2b_1");
            Assert.AreEqual(0, SewerEncounters.OmenDistance(s, g, "z1_3"));
            SewerState.Defeat(s, "z3_2");
            Assert.AreEqual(2, SewerEncounters.OmenDistance(s, g, "z3_1"), "E2 를 잡으면 허브 앞 E3 가 두 칸 앞");
            Assert.AreEqual(1, SewerEncounters.OmenDistance(s, g, "z3_2"), "E2 를 잡은 허브는 E3 가 한 칸 앞");
            SewerState.Defeat(s, "z5_1");
            Assert.AreEqual(0, SewerEncounters.OmenDistance(s, g, "z3_2"));
        }

        [Test]
        public void Rest_room_has_no_omen_even_next_to_an_encounter()
        {
            GameState s;
            SewerGraph g = NewGraph(out s);
            Assert.IsTrue(g.Get("z4_1").Rest);
            Assert.AreEqual(0, SewerEncounters.OmenDistance(s, g, "z4_1"));
        }

        [Test]
        public void Omen_plan_gets_stronger_as_san_drops_and_is_closer()
        {
            Assert.IsFalse(SewerEncounters.Omen(0, 0.1f).Any);
            SewerOmen stable = SewerEncounters.Omen(1, 0.9f);
            Assert.IsTrue(stable.Any);
            Assert.AreEqual(0f, stable.Shake, "안정 구간은 깜빡임만");
            SewerOmen mid = SewerEncounters.Omen(1, 0.5f);
            SewerOmen low = SewerEncounters.Omen(1, 0.1f);
            Assert.Greater(mid.Shake, 0f);
            Assert.Greater(low.Shake, mid.Shake);
            Assert.Greater(low.Shake, SewerEncounters.Omen(2, 0.1f).Shake);
            Assert.Greater(SewerEncounters.Omen(1, 0.5f).Dims, SewerEncounters.Omen(2, 0.5f).Dims);
            Assert.LessOrEqual(low.Shake, 1f);
        }

        [Test]
        public void Only_the_clue_room_is_a_rest_room()
        {
            GameState s;
            SewerGraph g = NewGraph(out s);
            for (int i = 0; i < g.Rooms.Count; i++)
            {
                Assert.AreEqual(g.Rooms[i].Id == SewerGraph.ClueRoomId, g.Rooms[i].Rest, g.Rooms[i].Id);
                Assert.IsFalse(g.Rooms[i].Rest && g.Rooms[i].HasEnemy);
            }
        }

        [Test]
        public void Rest_radius_covers_width_at_any_san_and_flicker_stops()
        {
            Assert.GreaterOrEqual(SewerSight.Radius(0f, false, true), 1f);
            Assert.AreEqual(SewerSight.Radius(0.5f, true), SewerSight.Radius(0.5f, true, false));
            Assert.Less(SewerSight.Radius(0f, false, false), 1f);
            Assert.GreaterOrEqual(SewerSight.Radius(1f, true, true), SewerSight.Radius(1f, true, false));
            SettingsData on = new SettingsData();
            Assert.IsTrue(SewerSight.FlickerActive(on, false));
            Assert.IsFalse(SewerSight.FlickerActive(on, true));
        }

        [Test]
        public void Rest_recovers_san_once_up_to_max_and_only_in_rest_room()
        {
            GameState s;
            SewerGraph g = NewGraph(out s);
            s.SetCounter(CounterKeys.san, 60);
            s.SetCounter(CounterKeys.nowSan, 40);
            Assert.AreEqual(0, SewerRest.Recover(s, g.Get("z3_1"), new SeededRng(1)));
            int gain = SewerRest.Recover(s, g.Get("z4_1"), new SeededRng(1));
            Assert.That(gain, Is.InRange(1, 3));
            Assert.AreEqual(40 + gain, s.GetCounter(CounterKeys.nowSan));
            Assert.AreEqual(0, SewerRest.Recover(s, g.Get("z4_1"), new SeededRng(2)), "한 번만");
        }

        [Test]
        public void Rest_does_not_exceed_max_san_and_resets_on_clear()
        {
            GameState s;
            SewerGraph g = NewGraph(out s);
            s.SetCounter(CounterKeys.san, 60);
            s.SetCounter(CounterKeys.nowSan, 60);
            Assert.AreEqual(0, SewerRest.Recover(s, g.Get("z4_1"), new SeededRng(3)));
            Assert.AreEqual(60, s.GetCounter(CounterKeys.nowSan));
            SewerState.Clear(s);
            Assert.AreEqual(0, s.GetCounter(SewerRest.RestKey));
        }
    }
}
