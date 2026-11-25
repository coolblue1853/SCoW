using SCoW.Core.Rules;
using SCoW.Core.State;

namespace SCoW.Core.Sewer
{
    /// <summary>
    /// 출시판(랜덤 방) 하수도 세이브를 고정 그래프로 옮긴다. 옛 세이브에는 방 정보가 없고 진행 단서만 있다:
    /// sewerHouseIn(비밀기지 도착), sewerEnemyCounter(적 조우 수 0~3). 이것으로 "대응 방"을 정하고,
    /// 입구에서 그 방까지의 길을 방문한 것으로, 조우 수만큼의 조우 방을 처치한 것으로 기록한다.
    /// 기록은 GameState 키 추가뿐이라 옛 키는 건드리지 않는다.
    /// </summary>
    public static class SewerLegacy
    {
        public const string SewerPlace = "InSewer";

        /// <summary>옛 진행 -> 대응 방. 비밀기지 도착 > 조우 3 이상(허브) > 2(허브 앞) > 1(침수 구간 안쪽) > 0(입구).</summary>
        public static string MapRoom(bool houseIn, int enemyCounter)
        {
            if (houseIn)
            {
                return SewerGraph.ClueRoomId;
            }
            if (enemyCounter >= 3)
            {
                return SewerGraph.GateRoomId;
            }
            if (enemyCounter == 2)
            {
                return "z3_1";
            }
            if (enemyCounter == 1)
            {
                return "z2b_2";
            }
            return SewerGraph.EntranceId;
        }

        /// <summary>
        /// 하수도에서 불러온 상태에 하수도 진행이 없으면(또는 가리키는 방이 없으면) 옛 단서로 만들어 넣는다.
        /// 하수도가 아니거나 이미 유효한 진행이 있으면 아무것도 하지 않고 false.
        /// </summary>
        public static bool Adopt(GameState s, IRng rng)
        {
            if (s.GetText(TextKeys.nowPlace) != SewerPlace)
            {
                return false;
            }
            SewerGraph g = SewerGraph.Create(SewerState.EnsureSeed(s, rng));
            if (SewerState.HasRoom(s) && g.Contains(SewerState.Room(s)))
            {
                return false;
            }
            string target = MapRoom(s.GetFlag(FlagKeys.sewerHouseIn), s.GetCounter(CounterKeys.sewerEnemyCounter));
            System.Collections.Generic.List<string> path = g.PathTo(target);
            for (int i = 0; i < path.Count; i++)
            {
                SewerState.Visit(s, path[i]);
            }
            int fought = s.GetCounter(CounterKeys.sewerEnemyCounter);
            if (fought > SewerGraph.EncounterCount)
            {
                fought = SewerGraph.EncounterCount;
            }
            for (int no = 1; no <= fought; no++)
            {
                string room = SewerGraph.EncounterRoom(no);
                SewerState.SetEncounter(s, room, no);
                SewerState.Defeat(s, room);
            }
            SewerState.SetRoom(s, target);
            return true;
        }
    }
}
