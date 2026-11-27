using System.Collections.Generic;
using SCoW.Core.State;

namespace SCoW.Core.Sewer
{
    /// <summary>지도에 그릴 방 하나의 상태. Unknown 은 지도를 입수했을 때만 나온다(윤곽만).</summary>
    public enum SewerMapNodeState
    {
        Unknown,
        Visited,
        Current,
    }

    /// <summary>미니맵 노드. X/Y 는 격자 좌표(Y 는 아래로 증가).</summary>
    public sealed class SewerMapNode
    {
        public readonly string Id;
        public readonly string Zone;
        public readonly int X;
        public readonly int Y;
        public readonly SewerMapNodeState State;
        /// <summary>지도 없이 기억만으로 그리는 방(희미하게).</summary>
        public readonly bool Faint;

        public SewerMapNode(string id, string zone, int x, int y, SewerMapNodeState state, bool faint)
        {
            Id = id;
            Zone = zone;
            X = x;
            Y = y;
            State = state;
            Faint = faint;
        }
    }

    public struct SewerMapEdge
    {
        public readonly string A;
        public readonly string B;

        public SewerMapEdge(string a, string b)
        {
            A = a;
            B = b;
        }
    }

    /// <summary>구역 라벨. X/Y 는 구역 방들의 격자 좌표 평균.</summary>
    public sealed class SewerMapZoneLabel
    {
        public readonly string Zone;
        public readonly string Name;
        public readonly float X;
        public readonly float Y;

        public SewerMapZoneLabel(string zone, string name, float x, float y)
        {
            Zone = zone;
            Name = name;
            X = x;
            Y = y;
        }
    }

    /// <summary>
    /// 하수도 미니맵 배치·가시성 규칙(SEWER-1 S5, 순수 C#). 좌표는 그래프 구조에서 계산한다(Right +x, Left -x, Middle +x/+y).
    /// 지도(FlagKeys.sewerMap) 입수 후: 모든 방(미방문은 윤곽)·연결선·구역 라벨. 미입수: 방문한 방과 그 사이 연결선만 희미하게, 라벨 없음.
    /// </summary>
    public sealed class SewerMapLayout
    {
        public readonly List<SewerMapNode> Nodes = new List<SewerMapNode>();
        public readonly List<SewerMapEdge> Edges = new List<SewerMapEdge>();
        public readonly List<SewerMapZoneLabel> Labels = new List<SewerMapZoneLabel>();
        public bool HasMap { get; private set; }
        public int MinX { get; private set; }
        public int MaxX { get; private set; }
        public int MinY { get; private set; }
        public int MaxY { get; private set; }

        /// <summary>구역 이름(영문 임시). TODO(I2): 현지화 용어로 교체.</summary>
        public static string ZoneName(string zone)
        {
            switch (zone)
            {
                case "Z0": return "Manhole";
                case "Z1": return "Main Drain";
                case "Z2a": return "Maintenance";
                case "Z2b": return "Flooded";
                case "Z3": return "Hub";
                case "Z4": return "Hideout";
                case "Z5": return "Sigil Chamber";
                default: return zone;
            }
        }

        /// <summary>입구에서 BFS 로 정한 방 격자 좌표. 오른쪽 문부터 따라가 본 길이 같은 줄에 놓인다. 이미 놓인 방은 건드리지 않는다.</summary>
        public static Dictionary<string, KeyValuePair<int, int>> Grid(SewerGraph g)
        {
            Dictionary<string, KeyValuePair<int, int>> pos = new Dictionary<string, KeyValuePair<int, int>>();
            Queue<string> q = new Queue<string>();
            pos[SewerGraph.EntranceId] = new KeyValuePair<int, int>(0, 0);
            q.Enqueue(SewerGraph.EntranceId);
            SewerDoor[] order = { SewerDoor.Right, SewerDoor.Middle, SewerDoor.Left };
            while (q.Count > 0)
            {
                string cur = q.Dequeue();
                SewerRoomDef r = g.Get(cur);
                KeyValuePair<int, int> p = pos[cur];
                for (int i = 0; i < order.Length; i++)
                {
                    SewerExit e = r.ExitAt(order[i]);
                    if (e == null || pos.ContainsKey(e.To))
                    {
                        continue;
                    }
                    int dx = order[i] == SewerDoor.Left ? -1 : 1;
                    int dy = order[i] == SewerDoor.Middle ? 1 : 0;
                    pos[e.To] = new KeyValuePair<int, int>(p.Key + dx, p.Value + dy);
                    q.Enqueue(e.To);
                }
            }
            return pos;
        }

        public static bool HasMapItem(GameState s)
        {
            return s.GetFlag(FlagKeys.sewerMap);
        }

        /// <summary>지금 상태로 미니맵 구성. 현재 방이 없으면(진입 전) 입구를 현재로 본다.</summary>
        public static SewerMapLayout Build(SewerGraph g, GameState s)
        {
            SewerMapLayout m = new SewerMapLayout();
            m.HasMap = HasMapItem(s);
            string current = SewerState.HasRoom(s) ? SewerState.Room(s) : SewerGraph.EntranceId;
            Dictionary<string, KeyValuePair<int, int>> grid = Grid(g);
            HashSet<string> shown = new HashSet<string>();
            bool first = true;
            for (int i = 0; i < g.Rooms.Count; i++)
            {
                SewerRoomDef r = g.Rooms[i];
                KeyValuePair<int, int> p;
                if (!grid.TryGetValue(r.Id, out p))
                {
                    continue;
                }
                bool isCur = r.Id == current;
                bool visited = isCur || SewerState.IsVisited(s, r.Id);
                if (!visited && !m.HasMap)
                {
                    continue;
                }
                SewerMapNodeState st = isCur ? SewerMapNodeState.Current : (visited ? SewerMapNodeState.Visited : SewerMapNodeState.Unknown);
                m.Nodes.Add(new SewerMapNode(r.Id, r.Zone, p.Key, p.Value, st, !m.HasMap && !isCur));
                shown.Add(r.Id);
                if (first)
                {
                    m.MinX = m.MaxX = p.Key;
                    m.MinY = m.MaxY = p.Value;
                    first = false;
                }
                else
                {
                    m.MinX = System.Math.Min(m.MinX, p.Key);
                    m.MaxX = System.Math.Max(m.MaxX, p.Key);
                    m.MinY = System.Math.Min(m.MinY, p.Value);
                    m.MaxY = System.Math.Max(m.MaxY, p.Value);
                }
            }
            m.BuildEdges(g, shown);
            if (m.HasMap)
            {
                m.BuildLabels();
            }
            return m;
        }

        private void BuildEdges(SewerGraph g, HashSet<string> shown)
        {
            HashSet<string> seen = new HashSet<string>();
            for (int i = 0; i < g.Rooms.Count; i++)
            {
                SewerRoomDef r = g.Rooms[i];
                if (!shown.Contains(r.Id))
                {
                    continue;
                }
                for (int k = 0; k < r.Exits.Count; k++)
                {
                    string to = r.Exits[k].To;
                    if (!shown.Contains(to))
                    {
                        continue;
                    }
                    string key = string.CompareOrdinal(r.Id, to) < 0 ? r.Id + "|" + to : to + "|" + r.Id;
                    if (seen.Add(key))
                    {
                        Edges.Add(new SewerMapEdge(r.Id, to));
                    }
                }
            }
        }

        private void BuildLabels()
        {
            List<string> zones = new List<string>();
            Dictionary<string, float[]> sum = new Dictionary<string, float[]>();
            for (int i = 0; i < Nodes.Count; i++)
            {
                SewerMapNode n = Nodes[i];
                float[] a;
                if (!sum.TryGetValue(n.Zone, out a))
                {
                    a = new float[3];
                    sum[n.Zone] = a;
                    zones.Add(n.Zone);
                }
                a[0] += n.X;
                a[1] += n.Y;
                a[2] += 1f;
            }
            for (int i = 0; i < zones.Count; i++)
            {
                float[] a = sum[zones[i]];
                Labels.Add(new SewerMapZoneLabel(zones[i], ZoneName(zones[i]), a[0] / a[2], a[1] / a[2]));
            }
        }
    }
}
