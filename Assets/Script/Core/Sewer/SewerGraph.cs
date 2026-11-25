using System.Collections.Generic;
using SCoW.Core.Rules;

namespace SCoW.Core.Sewer
{
    /// <summary>
    /// 고정 그래프(SewerImprove 3-1). 구조는 항상 같고, 터널·전구·소품 외형만 시드로 정한다.
    /// 앞으로 가는 문은 Right, 돌아가는 문은 Left, 갈래는 Middle(샛길). 모든 출구는 반대쪽 출구가 짝으로 있다.
    ///   z0 입구 - z1_1..z1_3 본관 -(Right) z2b_1(적1)..z2b_3 -(Right) z3_1(Left)
    ///                              \(Middle) z2a_1..z2a_3(우리) -(Right) z3_1(Middle)
    ///   z3_1 -(Right) z3_2 허브(적2) -(Middle) z4_1 비밀기지 / -(Right, 단서 필요) z5_1(적3) - z5_2 문양실
    /// </summary>
    public sealed class SewerGraph
    {
        public const string EntranceId = "z0";
        public const string GateRoomId = "z3_2";
        public const SewerDoor GateDoor = SewerDoor.Right;
        public const string ClueRoomId = "z4_1";
        public const string SymbolRoomId = "z5_2";

        private static readonly string[] EncounterRooms = { "z2b_1", "z3_2", "z5_1" };

        private readonly Dictionary<string, SewerRoomDef> _byId = new Dictionary<string, SewerRoomDef>();
        private readonly List<SewerRoomDef> _rooms = new List<SewerRoomDef>();

        public int Seed { get; private set; }

        public IList<SewerRoomDef> Rooms
        {
            get { return _rooms; }
        }

        public static string EncounterRoom(int encounterNo)
        {
            return encounterNo >= 1 && encounterNo <= EncounterRooms.Length ? EncounterRooms[encounterNo - 1] : null;
        }

        public static int EncounterCount
        {
            get { return EncounterRooms.Length; }
        }

        /// <summary>시드로 외형을 정한 그래프를 만든다. 같은 시드는 같은 외형.</summary>
        public static SewerGraph Create(int seed)
        {
            SewerGraph g = new SewerGraph();
            g.Seed = seed;
            g.Build();
            return g;
        }

        public SewerRoomDef Get(string id)
        {
            SewerRoomDef r;
            return id != null && _byId.TryGetValue(id, out r) ? r : null;
        }

        public bool Contains(string id)
        {
            return Get(id) != null;
        }

        /// <summary>from 의 door 로 나간 사람이 to 에서 서게 되는 문(= to 에서 from 으로 되돌아가는 문). 짝이 없으면 null.</summary>
        public SewerDoor? ArrivalDoor(string from, SewerDoor door)
        {
            SewerRoomDef a = Get(from);
            SewerExit e = a == null ? null : a.ExitAt(door);
            SewerRoomDef b = e == null ? null : Get(e.To);
            if (b == null)
            {
                return null;
            }
            for (int i = 0; i < b.Exits.Count; i++)
            {
                if (b.Exits[i].To == from)
                {
                    return b.Exits[i].Door;
                }
            }
            return null;
        }

        /// <summary>입구에서 target 까지 가장 짧은 방 ID 목록(양끝 포함). 닿지 않으면 빈 목록.</summary>
        public List<string> PathTo(string target)
        {
            List<string> result = new List<string>();
            if (!Contains(target))
            {
                return result;
            }
            Dictionary<string, string> prev = new Dictionary<string, string>();
            Queue<string> q = new Queue<string>();
            prev[EntranceId] = null;
            q.Enqueue(EntranceId);
            while (q.Count > 0)
            {
                string cur = q.Dequeue();
                if (cur == target)
                {
                    break;
                }
                SewerRoomDef r = Get(cur);
                for (int i = 0; i < r.Exits.Count; i++)
                {
                    string to = r.Exits[i].To;
                    if (!prev.ContainsKey(to))
                    {
                        prev[to] = cur;
                        q.Enqueue(to);
                    }
                }
            }
            if (!prev.ContainsKey(target))
            {
                return result;
            }
            for (string c = target; c != null; c = prev[c])
            {
                result.Insert(0, c);
            }
            return result;
        }

        private void Build()
        {
            Add("z0", "Z0", SewerLook.Normal, false);
            Add("z1_1", "Z1", SewerLook.Normal, false);
            Add("z1_2", "Z1", SewerLook.Normal, false);
            Add("z1_3", "Z1", SewerLook.Normal, false);
            Add("z2a_1", "Z2a", SewerLook.Normal, false);
            Add("z2a_2", "Z2a", SewerLook.Cage, false);
            Add("z2a_3", "Z2a", SewerLook.Normal, false);
            Add("z2b_1", "Z2b", SewerLook.Normal, true);
            Add("z2b_2", "Z2b", SewerLook.Normal, false);
            Add("z2b_3", "Z2b", SewerLook.Normal, false);
            Add("z3_1", "Z3", SewerLook.Normal, false);
            Add("z3_2", "Z3", SewerLook.Normal, true);
            Add("z4_1", "Z4", SewerLook.House, false);
            Add("z5_1", "Z5", SewerLook.Normal, true);
            Add("z5_2", "Z5", SewerLook.Symbol, false);

            Link("z0", SewerDoor.Right, "z1_1", SewerDoor.Left);
            Link("z1_1", SewerDoor.Right, "z1_2", SewerDoor.Left);
            Link("z1_2", SewerDoor.Right, "z1_3", SewerDoor.Left);
            Link("z1_3", SewerDoor.Middle, "z2a_1", SewerDoor.Left);
            Link("z1_3", SewerDoor.Right, "z2b_1", SewerDoor.Left);
            Link("z2a_1", SewerDoor.Right, "z2a_2", SewerDoor.Left);
            Link("z2a_2", SewerDoor.Right, "z2a_3", SewerDoor.Left);
            Link("z2a_3", SewerDoor.Right, "z3_1", SewerDoor.Middle);
            Link("z2b_1", SewerDoor.Right, "z2b_2", SewerDoor.Left);
            Link("z2b_2", SewerDoor.Right, "z2b_3", SewerDoor.Left);
            Link("z2b_3", SewerDoor.Right, "z3_1", SewerDoor.Left);
            Link("z3_1", SewerDoor.Right, "z3_2", SewerDoor.Left);
            Link("z3_2", SewerDoor.Middle, "z4_1", SewerDoor.Left);
            Link("z3_2", SewerDoor.Right, "z5_1", SewerDoor.Left);
            Link("z5_1", SewerDoor.Right, "z5_2", SewerDoor.Left);

            for (int i = 0; i < _rooms.Count; i++)
            {
                Dress(_rooms[i], i);
            }
        }

        private void Add(string id, string zone, SewerLook look, bool enemy)
        {
            SewerRoomDef r = new SewerRoomDef(id, zone);
            r.Look = look;
            r.HasEnemy = enemy;
            _byId[id] = r;
            _rooms.Add(r);
        }

        private void Link(string a, SewerDoor doorA, string b, SewerDoor doorB)
        {
            _byId[a].AddExit(doorA, b);
            _byId[b].AddExit(doorB, a);
        }

        // 외형 소품: 시드·방 순번으로만 정한다. 샛길 문이 있는 방은 씬 제약상 Flat·구멍 없음.
        private void Dress(SewerRoomDef r, int index)
        {
            SeededRng rng = new SeededRng(Seed * 7919 + index * 104729 + 17);
            r.Tunnel = rng.Next(0, 2) == 1 ? SewerTunnel.Flat : SewerTunnel.Round;
            r.Bulb1 = rng.Next(0, 2) == 0;
            r.Bulb2 = rng.Next(0, 2) == 0;
            r.Pipe = rng.Next(0, 2) == 0;
            r.Hole = rng.Next(0, 2) == 0;
            r.LadderHigh = rng.Next(0, 2) == 0;
            if (r.SideRoad)
            {
                r.Tunnel = SewerTunnel.Flat;
            }
            if (r.Tunnel == SewerTunnel.Round || r.SideRoad)
            {
                r.Hole = false;
            }
        }
    }
}
