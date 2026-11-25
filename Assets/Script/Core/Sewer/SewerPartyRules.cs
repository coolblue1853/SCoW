using System.Collections.Generic;
using SCoW.Core.Rules;
using SCoW.Core.State;

namespace SCoW.Core.Sewer
{
    /// <summary>
    /// 하수도와 동료 파티의 접점(PT-7, 순수 C#). 단체 은신 값 모으기, 파티 인원, 조우별 적 구성.
    /// 환원(쓰러짐·SAN 0)은 PartyBattleBridge.RestoreForSewer.
    /// </summary>
    public static class SewerPartyRules
    {
        /// <summary>적 인덱스(EnemyDefs.All 순서): 혼혈1~4 = 0~3, 심해인1 = 4.</summary>
        public const int Hybrid3 = 2;
        public const int Hybrid4 = 3;
        public const int DeepOne1 = 4;

        // 조우 번호(1~3) x 편성(0 = 솔로, 1 = 파티) -> 적 인덱스. 하수도 턴 심볼은 3칸이고 칸마다 적 하나다(혼혈4 = 칸0, 심해인1 = 칸1, 혼혈3 = 칸2).
        // 솔로는 옛 구성 그대로(E1 혼혈4 / E2 심해인1 / E3 둘). 파티는 한 마리씩 늘린다(밸런스 1차, Party.md PT-7 표).
        private static readonly int[][][] Compositions =
        {
            new[] { new[] { Hybrid4 }, new[] { Hybrid4, Hybrid3 } },
            new[] { new[] { DeepOne1 }, new[] { DeepOne1, Hybrid4 } },
            new[] { new[] { Hybrid4, DeepOne1 }, new[] { Hybrid4, DeepOne1, Hybrid3 } },
        };

        /// <summary>
        /// 은신 판정에 들어가는 값. 파비안은 DEX(옛 판정 그대로), 동료는 은신 기능치(알버트 15·메이브 40).
        /// disguised(코트 착용)면 변장으로 동료를 가려 파비안 값만 쓴다. 죽은 동료·대기 동료는 제외.
        /// </summary>
        public static List<int> StealthValues(GameState s, int fabianDex, bool disguised)
        {
            List<int> values = new List<int> { fabianDex };
            if (disguised)
            {
                return values;
            }
            List<CompanionDef> members = Members(s);
            for (int i = 0; i < members.Count; i++)
            {
                values.Add(members[i].GetSkill(CounterKeys.stealthPoint));
            }
            return values;
        }

        /// <summary>하수도 입구 단체 은신 판정. 위장 실패(true)면 호출 측이 Sewer_DisgauseFail 대화를 연다.</summary>
        public static bool DisguiseFails(GameState s, int fabianDex, bool disguised, IRng rng)
        {
            return BattleEngine.SewerPartyDisguiseFails(StealthValues(s, fabianDex, disguised), rng);
        }

        /// <summary>하수도에 함께 있는 동료(합류·생존·대기 아님), 대열 순서, 파비안 자리를 빼고 최대 2명.</summary>
        public static List<CompanionDef> Members(GameState s)
        {
            List<CompanionDef> list = new List<CompanionDef>();
            List<CompanionDef> active = PartyState.ActiveCompanions(s);
            for (int i = 0; i < active.Count && list.Count < BattleField.Ranks - 1; i++)
            {
                if (!PartyState.IsStandby(s, active[i].Id))
                {
                    list.Add(active[i]);
                }
            }
            return list;
        }

        /// <summary>파비안 포함 인원(1 = 솔로).</summary>
        public static int PartySize(GameState s)
        {
            return 1 + Members(s).Count;
        }

        /// <summary>조우 번호와 파티 인원에 맞는 적 인덱스. 모르는 번호는 3번 구성. 솔로(1명)만 옛 구성이다.</summary>
        public static int[] EnemyIndices(int encounterNo, int partySize)
        {
            int no = encounterNo >= 1 && encounterNo <= Compositions.Length ? encounterNo : Compositions.Length;
            return Compositions[no - 1][partySize >= 2 ? 1 : 0];
        }
    }
}
