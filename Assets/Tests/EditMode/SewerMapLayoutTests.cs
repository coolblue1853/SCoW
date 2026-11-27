using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.Sewer;
using SCoW.Core.State;

namespace SCoW.Tests
{
    /// <summary>SEWER-1 S5: 미니맵 노드 좌표·가시성(지도 유무)·구역 라벨.</summary>
    public sealed class SewerMapLayoutTests
    {
        private static GameState Fresh(bool map, params string[] visited)
        {
            GameState s = GameState.New();
            SewerState.SetRoom(s, SewerGraph.EntranceId);
            SewerState.Visit(s, SewerGraph.EntranceId);
            for (int i = 0; i < visited.Length; i++)
            {
                SewerState.Visit(s, visited[i]);
            }
            s.SetFlag(FlagKeys.sewerMap, map);
            return s;
        }

        private static SewerMapNode Node(SewerMapLayout m, string id)
        {
            return m.Nodes.Find(n => n.Id == id);
        }

        [Test]
        public void Grid_places_every_room_on_a_unique_cell()
        {
            SewerGraph g = SewerGraph.Create(1);
            Dictionary<string, KeyValuePair<int, int>> grid = SewerMapLayout.Grid(g);
            Assert.AreEqual(g.Rooms.Count, grid.Count);
            HashSet<string> cells = new HashSet<string>();
            foreach (KeyValuePair<string, KeyValuePair<int, int>> kv in grid)
            {
                Assert.IsTrue(cells.Add(kv.Value.Key + "," + kv.Value.Value), kv.Key);
            }
        }

        [Test]
        public void Grid_is_the_same_for_every_seed()
        {
            Dictionary<string, KeyValuePair<int, int>> a = SewerMapLayout.Grid(SewerGraph.Create(1));
            Dictionary<string, KeyValuePair<int, int>> b = SewerMapLayout.Grid(SewerGraph.Create(99));
            foreach (KeyValuePair<string, KeyValuePair<int, int>> kv in a)
            {
                Assert.AreEqual(kv.Value, b[kv.Key]);
            }
        }

        [Test]
        public void Grid_moves_right_through_the_main_route()
        {
            Dictionary<string, KeyValuePair<int, int>> grid = SewerMapLayout.Grid(SewerGraph.Create(1));
            Assert.AreEqual(new KeyValuePair<int, int>(0, 0), grid["z0"]);
            Assert.AreEqual(1, grid["z1_1"].Key);
            Assert.Greater(grid["z5_2"].Key, grid["z3_2"].Key);
            Assert.Greater(grid["z4_1"].Value, grid["z3_2"].Value);
        }

        [Test]
        public void Without_map_only_visited_rooms_are_shown_faint_without_labels()
        {
            SewerMapLayout m = SewerMapLayout.Build(SewerGraph.Create(1), Fresh(false, "z1_1"));
            Assert.IsFalse(m.HasMap);
            Assert.AreEqual(2, m.Nodes.Count);
            Assert.IsNull(Node(m, "z1_2"));
            Assert.AreEqual(1, m.Edges.Count);
            Assert.AreEqual(0, m.Labels.Count);
            Assert.IsTrue(Node(m, "z1_1").Faint);
            Assert.IsFalse(Node(m, "z0").Faint);
        }

        [Test]
        public void With_map_every_room_is_shown_with_zone_labels()
        {
            SewerGraph g = SewerGraph.Create(1);
            SewerMapLayout m = SewerMapLayout.Build(g, Fresh(true, "z1_1"));
            Assert.AreEqual(g.Rooms.Count, m.Nodes.Count);
            Assert.AreEqual(SewerMapNodeState.Unknown, Node(m, "z5_2").State);
            Assert.AreEqual(SewerMapNodeState.Visited, Node(m, "z1_1").State);
            Assert.IsFalse(Node(m, "z5_2").Faint);
            Assert.AreEqual(7, m.Labels.Count);
            Assert.AreEqual(15, m.Edges.Count);
        }

        [Test]
        public void Exactly_one_node_is_current_and_follows_the_room()
        {
            GameState s = Fresh(true, "z1_1");
            SewerState.SetRoom(s, "z1_1");
            SewerMapLayout m = SewerMapLayout.Build(SewerGraph.Create(1), s);
            Assert.AreEqual(1, m.Nodes.FindAll(n => n.State == SewerMapNodeState.Current).Count);
            Assert.AreEqual(SewerMapNodeState.Current, Node(m, "z1_1").State);
            Assert.AreEqual(SewerMapNodeState.Visited, Node(m, "z0").State);
        }

        [Test]
        public void Missing_room_falls_back_to_entrance()
        {
            SewerMapLayout m = SewerMapLayout.Build(SewerGraph.Create(1), GameState.New());
            Assert.AreEqual(1, m.Nodes.Count);
            Assert.AreEqual(SewerMapNodeState.Current, m.Nodes[0].State);
        }

        [Test]
        public void Edges_have_no_duplicates_and_bounds_cover_nodes()
        {
            SewerMapLayout m = SewerMapLayout.Build(SewerGraph.Create(1), Fresh(true));
            HashSet<string> seen = new HashSet<string>();
            foreach (SewerMapEdge e in m.Edges)
            {
                string k = string.CompareOrdinal(e.A, e.B) < 0 ? e.A + e.B : e.B + e.A;
                Assert.IsTrue(seen.Add(k), k);
            }
            foreach (SewerMapNode n in m.Nodes)
            {
                Assert.That(n.X, Is.InRange(m.MinX, m.MaxX));
                Assert.That(n.Y, Is.InRange(m.MinY, m.MaxY));
            }
        }

        [Test]
        public void Zone_label_sits_at_the_zone_center_and_has_a_name()
        {
            SewerMapLayout m = SewerMapLayout.Build(SewerGraph.Create(1), Fresh(true));
            SewerMapZoneLabel z1 = m.Labels.Find(l => l.Zone == "Z1");
            Assert.AreEqual("Main Drain", z1.Name);
            Assert.AreEqual((Node(m, "z1_1").X + Node(m, "z1_3").X) * 0.5f, z1.X, 0.001f);
            Assert.AreEqual("Zq", SewerMapLayout.ZoneName("Zq"));
        }
    }
}
