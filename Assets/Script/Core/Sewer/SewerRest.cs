using SCoW.Core.Rules;
using SCoW.Core.State;

namespace SCoW.Core.Sewer
{
    /// <summary>
    /// 휴식 구역 회복 규칙(SEWER-1 S4, 순수 C#). 세이브 키는 추가만 한다: Counters `sewer.rest`(회복을 받았나).
    /// </summary>
    public static class SewerRest
    {
        public const string RestKey = "sewer.rest";
        /// <summary>휴식 회복: 1d3 (임시 가정, Q-S2).</summary>
        public const int RestDie = 3;

        /// <summary>휴식 구역에 처음 들어왔을 때 SAN 을 1d3 회복한다(한 번만, 최대 SAN 을 넘지 않음). 회복량을 돌려준다.</summary>
        public static int Recover(GameState s, SewerRoomDef room, IRng rng)
        {
            if (room == null || !room.Rest || s.GetCounter(RestKey) != 0)
            {
                return 0;
            }
            int now = s.GetCounter(CounterKeys.nowSan);
            int max = s.GetCounter(CounterKeys.san);
            int gain = rng.Next(1, RestDie + 1);
            if (now + gain > max)
            {
                gain = max - now;
            }
            if (gain <= 0)
            {
                return 0;
            }
            s.SetCounter(RestKey, 1); // SAN 이 가득이라 못 받은 방문은 소진하지 않는다
            s.SetCounter(CounterKeys.nowSan, now + gain);
            return gain;
        }
    }
}
