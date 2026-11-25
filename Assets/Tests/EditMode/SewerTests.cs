using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.Rules;
using SCoW.Core.Save;
using SCoW.Core.Sewer;
using SCoW.Core.State;

namespace SCoW.Tests
{
    /// <summary>SEWER-1 S1: 고정 그래프 연결·되돌아오기·구세이브 방 매핑·SewerState 저장 왕복.</summary>
    public sealed class SewerTests
    {
        private static GameState NewRun(out SewerGraph g)
        {
            GameState s = GameState.New();
            s.SetText(TextKeys.nowPlace, SewerLegacy.SewerPlace);
            g = SewerGraph.Create(SewerState.EnsureSeed(s, new SeededRng(5)));
            SewerNav.Begin(s, g);
            return s;
        }

        private static SewerMove Go(GameState s, SewerGraph g, SewerDoor door)
        {
            SewerMove m = SewerNav.Move(s, g, door, false);
            Assert.AreEqual(SewerMoveStatus.Moved, m.Status, "move " + door + " from " + SewerState.Room(s));
            return m;
        }

        [Test]
        public void Graph_every_exit_has_a_matching_return_door()
        {
            SewerGraph g = SewerGraph.Create(3);
            foreach (SewerRoomDef r in g.Rooms)
            {
                foreach (SewerExit e in r.Exits)
                {
                    SewerDoor? back = g.ArrivalDoor(r.Id, e.Door);
                    Assert.IsTrue(back.HasValue, r.Id + " " + e.Door);
                    Assert.AreEqual(r.Id, g.Get(e.To).ExitAt(back.Value).To);
                }
            }
        }

        [Test]
        public void Graph_all_rooms_reachable_and_sideroad_rooms_are_flat()
        {
            SewerGraph g = SewerGraph.Create(11);
            foreach (SewerRoomDef r in g.Rooms)
            {
                Assert.IsTrue(g.PathTo(r.Id).Count > 0, r.Id);
                if (r.SideRoad)
                {
                    Assert.AreEqual(SewerTunnel.Flat, r.Tunnel, r.Id);
                    Assert.IsFalse(r.Hole, r.Id);
                }
            }
            Assert.AreEqual(3, CountEnemyRooms(g));
        }

        private static int CountEnemyRooms(SewerGraph g)
        {
            int n = 0;
            foreach (SewerRoomDef r in g.Rooms)
            {
                n += r.HasEnemy ? 1 : 0;
            }
            return n;
        }

        [Test]
        public void Graph_same_seed_same_look_different_seed_differs()
        {
            SewerGraph a = SewerGraph.Create(42);
            SewerGraph b = SewerGraph.Create(42);
            SewerGraph c = SewerGraph.Create(43);
            bool anyDiff = false;
            for (int i = 0; i < a.Rooms.Count; i++)
            {
                Assert.AreEqual(a.Rooms[i].Tunnel, b.Rooms[i].Tunnel);
                Assert.AreEqual(a.Rooms[i].Bulb1, b.Rooms[i].Bulb1);
                Assert.AreEqual(a.Rooms[i].Pipe, b.Rooms[i].Pipe);
                anyDiff |= a.Rooms[i].Tunnel != c.Rooms[i].Tunnel || a.Rooms[i].Bulb1 != c.Rooms[i].Bulb1 || a.Rooms[i].Pipe != c.Rooms[i].Pipe || a.Rooms[i].Hole != c.Rooms[i].Hole;
            }
            Assert.IsTrue(anyDiff);
        }

        [Test]
        public void Route_right_branch_reaches_symbol_room_after_clue()
        {
            SewerGraph g;
            GameState s = NewRun(out g);
            SewerDoor[] toHub = { SewerDoor.Right, SewerDoor.Right, SewerDoor.Right, SewerDoor.Right, SewerDoor.Right, SewerDoor.Right, SewerDoor.Right, SewerDoor.Right };
            for (int i = 0; i < toHub.Length; i++)
            {
                Go(s, g, toHub[i]);
            }
            Assert.AreEqual(SewerGraph.GateRoomId, SewerState.Room(s));
            Assert.AreEqual(SewerMoveStatus.Locked, SewerNav.Move(s, g, SewerDoor.Right, false).Status);
            Assert.AreEqual(SewerGraph.GateRoomId, SewerState.Room(s));
            Go(s, g, SewerDoor.Middle);
            Assert.AreEqual(SewerGraph.ClueRoomId, SewerState.Room(s));
            Go(s, g, SewerDoor.Left);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            Assert.AreEqual(SewerGraph.SymbolRoomId, SewerState.Room(s));
        }

        [Test]
        public void Route_left_branch_reaches_symbol_room_after_clue()
        {
            SewerGraph g;
            GameState s = NewRun(out g);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Middle);
            Assert.AreEqual("z2a_1", SewerState.Room(s));
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            SewerMove m = Go(s, g, SewerDoor.Right);
            Assert.AreEqual("z3_1", m.To.Id);
            Assert.AreEqual(SewerDoor.Middle, m.Arrival);
            Go(s, g, SewerDoor.Right);
            Assert.AreEqual(SewerGraph.GateRoomId, SewerState.Room(s));
            Go(s, g, SewerDoor.Middle);
            Go(s, g, SewerDoor.Left);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            Assert.AreEqual(SewerGraph.SymbolRoomId, SewerState.Room(s));
        }

        [Test]
        public void Gate_opens_with_known_clue_flag()
        {
            SewerGraph g;
            GameState s = NewRun(out g);
            SewerState.SetRoom(s, SewerGraph.GateRoomId);
            Assert.AreEqual(SewerMoveStatus.Locked, SewerNav.Move(s, g, SewerDoor.Right, false).Status);
            Assert.AreEqual(SewerMoveStatus.Moved, SewerNav.Move(s, g, SewerDoor.Right, true).Status);
        }

        [Test]
        public void Entrance_has_no_left_door_and_missing_door_is_NoExit()
        {
            SewerGraph g;
            GameState s = NewRun(out g);
            Assert.AreEqual(SewerMoveStatus.NoExit, SewerNav.Move(s, g, SewerDoor.Left, false).Status);
            Assert.AreEqual(SewerMoveStatus.NoExit, SewerNav.Move(s, g, SewerDoor.Middle, false).Status);
            Assert.AreEqual(SewerGraph.EntranceId, SewerState.Room(s));
        }

        [Test]
        public void Return_trip_arrives_at_same_room_with_same_look()
        {
            SewerGraph g;
            GameState s = NewRun(out g);
            Go(s, g, SewerDoor.Right);
            SewerRoomDef first = g.Get(SewerState.Room(s));
            Go(s, g, SewerDoor.Right);
            SewerMove back = Go(s, g, SewerDoor.Left);
            Assert.AreSame(first, back.To);
            Assert.IsFalse(back.FirstVisit);
            Assert.AreEqual(SewerDoor.Right, back.Arrival);
            SewerMove entrance = Go(s, g, SewerDoor.Left);
            Assert.AreEqual(SewerGraph.EntranceId, entrance.To.Id);
        }

        [Test]
        public void Encounters_number_in_order_persist_until_defeated_and_set_old_counter()
        {
            SewerGraph g;
            GameState s = NewRun(out g);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            SewerMove e1 = Go(s, g, SewerDoor.Right);
            Assert.AreEqual("z2b_1", e1.To.Id);
            Assert.AreEqual(1, e1.Encounter);
            Assert.IsTrue(e1.NewEncounter);
            Assert.AreEqual(1, s.GetCounter(CounterKeys.sewerEnemyCounter));
            // 도망쳤다가 다시 와도 같은 조우, 새 조우 아님.
            Go(s, g, SewerDoor.Left);
            SewerMove again = Go(s, g, SewerDoor.Right);
            Assert.AreEqual(1, again.Encounter);
            Assert.IsFalse(again.NewEncounter);
            Assert.IsTrue(SewerNav.DefeatCurrent(s, g));
            Go(s, g, SewerDoor.Left);
            SewerMove cleared = Go(s, g, SewerDoor.Right);
            Assert.AreEqual(0, cleared.Encounter);
            Assert.IsFalse(SewerNav.DefeatCurrent(s, g));
        }

        [Test]
        public void Left_branch_first_fight_is_encounter_one()
        {
            SewerGraph g;
            GameState s = NewRun(out g);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Middle);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            SewerMove m = Go(s, g, SewerDoor.Right);
            Assert.AreEqual(SewerGraph.GateRoomId, m.To.Id);
            Assert.AreEqual(1, m.Encounter);
        }

        [Test]
        public void Jump_to_symbol_room_arrives_right_without_encounter()
        {
            SewerGraph g;
            GameState s = NewRun(out g);
            SewerMove m = SewerNav.Jump(s, g, SewerGraph.SymbolRoomId);
            Assert.AreEqual(SewerMoveStatus.Moved, m.Status);
            Assert.AreEqual(SewerDoor.Right, m.Arrival);
            Assert.AreEqual(0, m.Encounter);
            Assert.AreEqual(SewerGraph.SymbolRoomId, SewerState.Room(s));
        }

        [TestCase(false, 0, "z0")]
        [TestCase(false, 1, "z2b_2")]
        [TestCase(false, 2, "z3_1")]
        [TestCase(false, 3, "z3_2")]
        [TestCase(true, 0, "z4_1")]
        [TestCase(true, 3, "z4_1")]
        public void Legacy_save_in_sewer_maps_to_corresponding_room(bool houseIn, int counter, string expected)
        {
            GameState s = GameState.New();
            s.SetText(TextKeys.nowPlace, SewerLegacy.SewerPlace);
            s.SetFlag(FlagKeys.sewerHouseIn, houseIn);
            s.SetCounter(CounterKeys.sewerEnemyCounter, counter);
            Assert.IsFalse(SewerState.HasRoom(s));
            Assert.IsTrue(SewerLegacy.Adopt(s, new SeededRng(9)));
            Assert.AreEqual(expected, SewerState.Room(s));
            SewerGraph g = SewerGraph.Create(SewerState.Seed(s));
            foreach (string id in g.PathTo(expected))
            {
                Assert.IsTrue(SewerState.IsVisited(s, id), id);
            }
            for (int no = 1; no <= counter; no++)
            {
                Assert.IsTrue(SewerState.IsDefeated(s, SewerGraph.EncounterRoom(no)));
            }
            // 옛 키는 그대로.
            Assert.AreEqual(counter, s.GetCounter(CounterKeys.sewerEnemyCounter));
            Assert.AreEqual(houseIn, s.GetFlag(FlagKeys.sewerHouseIn));
        }

        [Test]
        public void Legacy_adopt_is_idempotent_and_ignores_other_places()
        {
            GameState s = GameState.New();
            s.SetText(TextKeys.nowPlace, "Riverside");
            Assert.IsFalse(SewerLegacy.Adopt(s, new SeededRng(1)));
            Assert.IsFalse(SewerState.HasRoom(s));
            s.SetText(TextKeys.nowPlace, SewerLegacy.SewerPlace);
            Assert.IsTrue(SewerLegacy.Adopt(s, new SeededRng(1)));
            string room = SewerState.Room(s);
            SewerState.SetRoom(s, "z1_2");
            Assert.IsFalse(SewerLegacy.Adopt(s, new SeededRng(2)));
            Assert.AreEqual("z1_2", SewerState.Room(s));
            Assert.AreNotEqual(null, room);
        }

        [Test]
        public void Legacy_adopt_repairs_unknown_room_id()
        {
            GameState s = GameState.New();
            s.SetText(TextKeys.nowPlace, SewerLegacy.SewerPlace);
            SewerState.SetRoom(s, "gone");
            Assert.IsTrue(SewerLegacy.Adopt(s, new SeededRng(1)));
            Assert.AreEqual(SewerGraph.EntranceId, SewerState.Room(s));
        }

        [Test]
        public void SewerState_survives_save_round_trip_and_old_save_has_no_sewer_keys()
        {
            SewerGraph g;
            GameState s = NewRun(out g);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            Go(s, g, SewerDoor.Right);
            SewerNav.DefeatCurrent(s, g);
            GameState back;
            Assert.AreEqual(SaveError.None, SaveCodec.TryDecodeState(SaveCodec.EncodeState(s), out back));
            Assert.IsTrue(s.Equals(back));
            Assert.AreEqual("z2b_1", SewerState.Room(back));
            Assert.AreEqual(SewerState.Seed(s), SewerState.Seed(back));
            Assert.IsTrue(SewerState.IsVisited(back, "z1_2"));
            Assert.IsTrue(SewerState.IsDefeated(back, "z2b_1"));
            Assert.AreEqual(1, SewerState.EncounterOf(back, "z2b_1"));

            GameState old = GameState.New();
            string text = SaveCodec.EncodeState(old);
            Assert.IsFalse(text.Contains("sewer."));
            GameState oldBack;
            Assert.AreEqual(SaveError.None, SaveCodec.TryDecodeState(text, out oldBack));
            Assert.IsFalse(SewerState.HasRoom(oldBack));
        }

        [Test]
        public void Begin_keeps_progress_and_seed_when_reentered()
        {
            SewerGraph g;
            GameState s = NewRun(out g);
            Go(s, g, SewerDoor.Right);
            int seed = SewerState.Seed(s);
            SewerNav.Begin(s, g);
            Assert.AreEqual(SewerGraph.EntranceId, SewerState.Room(s));
            Assert.IsTrue(SewerState.IsVisited(s, "z1_1"));
            Assert.AreEqual(seed, SewerState.Seed(s));
        }

        [Test]
        public void Done_path_from_entrance_to_symbol_fits_target_length()
        {
            SewerGraph g = SewerGraph.Create(1);
            List<string> p = g.PathTo(SewerGraph.SymbolRoomId);
            Assert.GreaterOrEqual(p.Count - 1, 10);
        }
    }
}
