using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.Save;
using SCoW.Core.Sewer;
using SCoW.Core.State;

namespace SCoW.Tests
{
    /// <summary>SEWER-1 S2: 조사 포인트 배치·조사 기록 저장 왕복·단서 플래그.</summary>
    public sealed class SewerPointTests
    {
        private static List<KeyValuePair<string, SewerPoint>> AllPoints(SewerGraph g)
        {
            List<KeyValuePair<string, SewerPoint>> list = new List<KeyValuePair<string, SewerPoint>>();
            foreach (SewerRoomDef r in g.Rooms)
            {
                foreach (SewerPoint p in r.Points)
                {
                    list.Add(new KeyValuePair<string, SewerPoint>(r.Id, p));
                }
            }
            return list;
        }

        [Test]
        public void Points_count_is_4_to_6_and_ids_are_unique()
        {
            List<KeyValuePair<string, SewerPoint>> all = AllPoints(SewerGraph.Create(3));
            Assert.GreaterOrEqual(all.Count, 4);
            Assert.LessOrEqual(all.Count, 6);
            HashSet<string> ids = new HashSet<string>();
            foreach (KeyValuePair<string, SewerPoint> kv in all)
            {
                Assert.IsTrue(ids.Add(kv.Key + ":" + kv.Value.Id), kv.Key + ":" + kv.Value.Id);
            }
        }

        [Test]
        public void Points_do_not_overlap_doors_and_flags_are_registered()
        {
            List<string> flags = new List<string>(FlagKeys.All);
            foreach (KeyValuePair<string, SewerPoint> kv in AllPoints(SewerGraph.Create(9)))
            {
                SewerPoint p = kv.Value;
                Assert.Greater(p.X, -1236f, "left door clearance " + p.Id);
                Assert.Less(p.X, -1218f, "side road clearance " + p.Id);
                Assert.IsTrue(flags.Contains(p.ClueFlag), p.ClueFlag);
                Assert.IsFalse(string.IsNullOrEmpty(p.Topic));
            }
        }

        [Test]
        public void Points_are_same_for_every_seed()
        {
            Assert.AreEqual(AllPoints(SewerGraph.Create(1)).Count, AllPoints(SewerGraph.Create(77)).Count);
        }

        [Test]
        public void Look_is_remembered_per_room_and_point_and_survives_save()
        {
            GameState s = GameState.New();
            Assert.IsFalse(SewerState.IsLooked(s, "z1_2", "graffiti"));
            SewerState.Look(s, "z1_2", "graffiti");
            SewerState.Look(s, "z1_2", "graffiti");
            Assert.IsTrue(SewerState.IsLooked(s, "z1_2", "graffiti"));
            Assert.IsFalse(SewerState.IsLooked(s, "z2a_1", "graffiti"));
            s.SetFlag(FlagKeys.intelSewerClueGraffiti, true);

            GameState back;
            Assert.AreEqual(SaveError.None, SaveCodec.TryDecodeState(SaveCodec.EncodeState(s), out back));
            Assert.IsTrue(SewerState.IsLooked(back, "z1_2", "graffiti"));
            Assert.IsTrue(back.GetFlag(FlagKeys.intelSewerClueGraffiti));

            SewerState.Clear(back);
            Assert.IsFalse(SewerState.IsLooked(back, "z1_2", "graffiti"));
        }
    }
}
