using SCoW.Core.Rules;
using SCoW.Core.State;

namespace SCoW.Core.Sewer
{
    public enum SewerMoveStatus
    {
        Moved,
        NoExit,
        Locked,
    }

    /// <summary>문 하나를 지난 결과. Moved 일 때만 To·Arrival 이 채워진다.</summary>
    public sealed class SewerMove
    {
        public SewerMoveStatus Status;
        public SewerRoomDef To;
        public SewerDoor Arrival;
        public bool FirstVisit;
        /// <summary>도착 방의 조우 번호(1~3). 적이 없거나 이미 처치했으면 0.</summary>
        public int Encounter;
        /// <summary>이 이동으로 조우가 새로 시작됐다(예고 대사를 띄울 때).</summary>
        public bool NewEncounter;
    }

    /// <summary>하수도 이동 규칙(순수 C#). 현재 방은 GameState 에 두고, 그래프는 시드로 만든다.</summary>
    public static class SewerNav
    {
        /// <summary>강변 맨홀 진입: 항상 입구에서 시작. 진행(방문·처치·시드)은 이미 있으면 보존한다.</summary>
        public static SewerRoomDef Begin(GameState s, SewerGraph g)
        {
            SewerState.SetRoom(s, SewerGraph.EntranceId);
            SewerState.Visit(s, SewerGraph.EntranceId);
            return g.Get(SewerGraph.EntranceId);
        }

        /// <summary>문양실로 가는 길(허브 오른쪽 문)이 열렸나: 비밀기지를 가봤거나, 옛 지도/단서를 이미 얻었다.</summary>
        public static bool GateOpen(GameState s, bool clueKnown)
        {
            return clueKnown || SewerState.IsVisited(s, SewerGraph.ClueRoomId);
        }

        /// <summary>이동하지 않고 이 문을 지날 수 있는지만 본다(문 입력 단계에서 잠긴 문에 페이드를 걸지 않기 위해).</summary>
        public static SewerMoveStatus Check(GameState s, SewerGraph g, SewerDoor door, bool clueKnown)
        {
            string from = SewerState.Room(s);
            SewerRoomDef cur = g.Get(from);
            SewerExit exit = cur == null ? null : cur.ExitAt(door);
            if (exit == null)
            {
                return SewerMoveStatus.NoExit;
            }
            if (from == SewerGraph.GateRoomId && door == SewerGraph.GateDoor && !GateOpen(s, clueKnown))
            {
                return SewerMoveStatus.Locked;
            }
            return SewerMoveStatus.Moved;
        }

        public static SewerMove Move(GameState s, SewerGraph g, SewerDoor door, bool clueKnown)
        {
            SewerMove m = new SewerMove();
            m.Status = Check(s, g, door, clueKnown);
            if (m.Status != SewerMoveStatus.Moved)
            {
                return m;
            }
            string from = SewerState.Room(s);
            SewerExit exit = g.Get(from).ExitAt(door);
            SewerDoor? arrival = g.ArrivalDoor(from, door);
            if (!arrival.HasValue)
            {
                m.Status = SewerMoveStatus.NoExit;
                return m;
            }
            Arrive(s, g, exit.To, arrival.Value, m);
            return m;
        }

        /// <summary>지도 이동 같은 순간이동(문양실 등). 도착은 오른쪽(옛 SewerMapMove 와 같다). 조우는 만들지 않는다.</summary>
        public static SewerMove Jump(GameState s, SewerGraph g, string roomId)
        {
            SewerMove m = new SewerMove();
            if (!g.Contains(roomId))
            {
                m.Status = SewerMoveStatus.NoExit;
                return m;
            }
            m.Status = SewerMoveStatus.Moved;
            m.To = g.Get(roomId);
            m.Arrival = SewerDoor.Right;
            m.FirstVisit = !SewerState.IsVisited(s, roomId);
            SewerState.SetRoom(s, roomId);
            SewerState.Visit(s, roomId);
            return m;
        }

        /// <summary>저장된 방에 다시 서기(불러오기). 새 조우는 만들지 않고, 처치 못 한 적이 있으면 번호만 돌려준다.</summary>
        public static int PendingEncounter(GameState s, SewerRoomDef room)
        {
            if (room == null || !room.HasEnemy || SewerState.IsDefeated(s, room.Id))
            {
                return 0;
            }
            return SewerState.EncounterOf(s, room.Id);
        }

        /// <summary>현재 방의 적을 처치했다고 기록한다. 조우가 없던 방이면 아무 일도 없다.</summary>
        public static bool DefeatCurrent(GameState s, SewerGraph g)
        {
            SewerRoomDef room = g.Get(SewerState.Room(s));
            if (room == null || !room.HasEnemy || SewerState.IsDefeated(s, room.Id))
            {
                return false;
            }
            SewerState.Defeat(s, room.Id);
            return true;
        }

        private static void Arrive(GameState s, SewerGraph g, string roomId, SewerDoor arrival, SewerMove m)
        {
            SewerRoomDef room = g.Get(roomId);
            m.Status = SewerMoveStatus.Moved;
            m.To = room;
            m.Arrival = arrival;
            m.FirstVisit = !SewerState.IsVisited(s, roomId);
            SewerState.SetRoom(s, roomId);
            SewerState.Visit(s, roomId);
            if (!room.HasEnemy || SewerState.IsDefeated(s, roomId))
            {
                return;
            }
            int no = SewerState.EncounterOf(s, roomId);
            if (no == 0)
            {
                no = SewerState.MaxEncounter(s) + 1;
                if (no > SewerGraph.EncounterCount)
                {
                    no = SewerGraph.EncounterCount;
                }
                SewerState.SetEncounter(s, roomId, no);
                m.NewEncounter = true;
            }
            // 옛 전투 구성(DirectingManager.GotoBattleSewer)이 이 카운터로 적을 고른다. 1=혼혈, 2=심해인, 3=둘.
            s.SetCounter(CounterKeys.sewerEnemyCounter, no);
            m.Encounter = no;
        }
    }
}
