using SCoW.Core.State;

namespace SCoW.Core.Sewer
{
    /// <summary>조우 한 번의 데이터(SEWER-1 S4). 방·도착 대사를 한 줄로 둔다.</summary>
    public sealed class SewerEncounterDef
    {
        public readonly int No;
        public readonly string RoomId;
        /// <summary>조우 방에 들어온 뒤 1초 뒤 띄우는 대사 노드(Story JSON ID).</summary>
        public readonly string Dialog;

        public SewerEncounterDef(int no, string roomId, string dialog)
        {
            No = no;
            RoomId = roomId;
            Dialog = dialog;
        }
    }

    /// <summary>
    /// 조우 3회 배치와 전조 규칙(순수 C#). 배치는 이 표 하나가 원본이고 SewerGraph 가 방의 HasEnemy 를 여기서 정한다.
    /// 전조 = 적이 있는 방 직전(오른쪽 문 기준 1~2칸 앞)의 방에서 내는 SAN 연동 연출 계획.
    /// </summary>
    public static class SewerEncounters
    {
        public const string DefaultDialog = "Sewer_EnemySpawn";
        /// <summary>전조가 닿는 칸 수(앞으로 가는 문 기준).</summary>
        public const int OmenReach = 2;
        /// <summary>SAN 비율이 이 미만이면 전조에 화면 흔들림이 붙는다(불안 이하).</summary>
        public const float ShakeBelowRatio = 0.7f;

        // E1 침수 구간 입구(첫 딥원 계열 연출을 여기 붙임), E2 허브(순찰 마주침), E3 문양실 앞(필수 전투).
        private static readonly SewerEncounterDef[] Defs =
        {
            new SewerEncounterDef(1, "z2b_1", "Sewer_FirstDeepOne"),
            new SewerEncounterDef(2, "z3_2", DefaultDialog),
            new SewerEncounterDef(3, "z5_1", DefaultDialog),
        };

        public static int Count
        {
            get { return Defs.Length; }
        }

        public static SewerEncounterDef ByNo(int no)
        {
            return no >= 1 && no <= Defs.Length ? Defs[no - 1] : null;
        }

        public static SewerEncounterDef ByRoom(string roomId)
        {
            for (int i = 0; i < Defs.Length; i++)
            {
                if (Defs[i].RoomId == roomId)
                {
                    return Defs[i];
                }
            }
            return null;
        }

        /// <summary>조우 번호의 도착 대사 노드. 모르는 번호는 기본 대사.</summary>
        public static string DialogFor(int no)
        {
            SewerEncounterDef d = ByNo(no);
            return d == null ? DefaultDialog : d.Dialog;
        }

        /// <summary>
        /// 이 방에서 앞(오른쪽 문)으로 1~2칸 안에 아직 처치 못 한 조우가 있으면 그 칸 수(1 또는 2), 없으면 0.
        /// 휴식 구역과 지금 방에 적이 있는 방(바로 전투)은 전조가 없다.
        /// </summary>
        public static int OmenDistance(GameState s, SewerGraph g, string roomId)
        {
            SewerRoomDef room = g.Get(roomId);
            if (room == null || room.Rest || Pending(s, room))
            {
                return 0;
            }
            SewerRoomDef cur = room;
            for (int step = 1; step <= OmenReach; step++)
            {
                SewerExit e = cur.ExitAt(SewerDoor.Right);
                cur = e == null ? null : g.Get(e.To);
                if (cur == null)
                {
                    return 0;
                }
                if (Pending(s, cur))
                {
                    return step;
                }
            }
            return 0;
        }

        private static bool Pending(GameState s, SewerRoomDef room)
        {
            return room.HasEnemy && !SewerState.IsDefeated(s, room.Id);
        }

        /// <summary>전조 연출 계획. 깜빡임 횟수와 화면 흔들림 세기(0 = 흔들림 없음)를 거리·SAN 으로 정한다.</summary>
        public static SewerOmen Omen(int distance, float sanRatio)
        {
            if (distance <= 0)
            {
                return new SewerOmen(0, 0f);
            }
            float r = sanRatio < 0f ? 0f : (sanRatio > 1f ? 1f : sanRatio);
            int dims = distance == 1 ? 2 : 1;
            float shake = 0f;
            if (r < ShakeBelowRatio)
            {
                float near = distance == 1 ? 1f : 0.5f;
                shake = near * (0.4f + 0.6f * (1f - r));
            }
            return new SewerOmen(dims, shake);
        }
    }

    /// <summary>전조 연출 계획(값). Dims 는 전구 깜빡임 횟수, Shake 는 0~1 흔들림 세기.</summary>
    public struct SewerOmen
    {
        public readonly int Dims;
        public readonly float Shake;

        public SewerOmen(int dims, float shake)
        {
            Dims = dims;
            Shake = shake;
        }

        public bool Any
        {
            get { return Dims > 0; }
        }
    }
}
