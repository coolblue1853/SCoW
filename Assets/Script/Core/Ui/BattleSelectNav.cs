using System.Collections.Generic;
using SCoW.Core.Rules;

namespace SCoW.Core.Ui
{
    /// <summary>
    /// 다인원 전투 선택 입력의 순수 규칙(PT-6): 메뉴 커서 이동, 대상 커서 이동, 대열 교체 단축키 해석.
    /// View 는 입력 장치를 읽어 이 규칙에 방향만 넘긴다(마우스·키보드·패드가 같은 경로).
    /// 화면 방향: 아군은 칸 번호가 클수록 화면 왼쪽, 적은 칸 번호가 클수록 화면 오른쪽.
    /// </summary>
    public static class BattleSelectNav
    {
        /// <summary>메뉴 커서를 dir(+1 아래, -1 위)만큼 옮긴다. 비활성 줄은 건너뛰고, 전부 비활성이면 그대로.</summary>
        public static int MoveMenu(IList<bool> enabled, int cursor, int dir)
        {
            int count = enabled == null ? 0 : enabled.Count;
            if (count == 0 || dir == 0)
            {
                return cursor;
            }
            int next = cursor;
            for (int n = 0; n < count; n++)
            {
                next = (next + dir + count) % count;
                if (enabled[next])
                {
                    return next;
                }
            }
            return cursor;
        }

        /// <summary>대상 커서를 화면 방향(+1 오른쪽, -1 왼쪽)으로 옮긴다. 대상 목록은 칸 번호 오름차순.</summary>
        public static int MoveTarget(int cursor, int count, int screenDir, Side side)
        {
            if (count <= 0)
            {
                return 0;
            }
            int step = side == Side.Ally ? -screenDir : screenDir;
            int next = cursor + step;
            return next < 0 ? 0 : (next >= count ? count - 1 : next);
        }

        /// <summary>
        /// 대열 교체 단축키: 현재 행위자 칸에서 화면 방향(-1 왼쪽, +1 오른쪽)의 이웃 칸 번호.
        /// 그 칸이 유효 대상 목록(ValidTargets)에 없으면 -1(교체 불가).
        /// </summary>
        public static int SwapTargetRank(int actorRank, int screenDir, IList<int> validRanks)
        {
            if (validRanks == null || screenDir == 0)
            {
                return -1;
            }
            int rank = actorRank - screenDir; // 아군은 화면 왼쪽(-1)이 칸 번호 +1
            for (int i = 0; i < validRanks.Count; i++)
            {
                if (validRanks[i] == rank)
                {
                    return rank;
                }
            }
            return -1;
        }

        /// <summary>행동 목록에서 켜져 있는 해당 종류의 줄 번호. 없으면 -1.</summary>
        public static int FindEnabledKind(IList<ActionKind> kinds, IList<bool> enabled, ActionKind kind)
        {
            for (int i = 0; i < kinds.Count; i++)
            {
                if (kinds[i] == kind && enabled[i])
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
