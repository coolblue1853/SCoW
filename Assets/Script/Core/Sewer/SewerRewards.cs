using SCoW.Core.State;

namespace SCoW.Core.Sewer
{
    /// <summary>
    /// 하수도 보상 규칙(SEWER-1 S7, 순수 C#). 조우를 처음 처치하면 표(SewerEncounters)에 적힌 만큼 붕대를 준다.
    /// 세이브 키는 추가만 한다: Counters `sewer.reward.&lt;조우번호&gt;`(그 조우 보상을 받았나). 붕대 수는 기존 `bandages` 를 그대로 쓴다.
    /// </summary>
    public static class SewerRewards
    {
        public const string RewardPrefix = "sewer.reward.";

        public static string RewardKey(int encounterNo)
        {
            return RewardPrefix + encounterNo;
        }

        /// <summary>조우 처치 보상. 조우마다 한 번만. 받은 붕대 수를 돌려준다(없거나 이미 받았으면 0).</summary>
        public static int GrantClear(GameState s, int encounterNo)
        {
            SewerEncounterDef def = SewerEncounters.ByNo(encounterNo);
            if (def == null || def.Bandages <= 0 || s.GetCounter(RewardKey(encounterNo)) != 0)
            {
                return 0;
            }
            s.SetCounter(RewardKey(encounterNo), 1);
            s.SetCounter(CounterKeys.bandages, s.GetCounter(CounterKeys.bandages) + def.Bandages);
            return def.Bandages;
        }
    }
}
