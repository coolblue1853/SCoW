using System.Collections.Generic;

namespace SCoW.Core.Save
{
    /// <summary>사망 화면 선택지(UX-5). 순서가 곧 화면 순서다.</summary>
    public enum DeathChoice
    {
        ResumeLast,
        Title,
        Quit,
    }

    /// <summary>"마지막 저장" 고르기와 사망 선택지 구성(순수 C#, EditMode 테스트 대상).</summary>
    public static class ResumeRules
    {
        /// <summary>저장 시각(UTC ticks) 목록에서 가장 최근 인덱스. 0 이하(없음·읽기 실패)는 후보가 아니다. 동률이면 앞쪽. 없으면 -1.</summary>
        public static int LatestIndex(IList<long> savedTicks)
        {
            int best = -1;
            long bestTicks = 0;
            for (int i = 0; i < savedTicks.Count; i++)
            {
                if (savedTicks[i] > bestTicks)
                {
                    best = i;
                    bestTicks = savedTicks[i];
                }
            }
            return best;
        }

        /// <summary>저장이 없으면 "마지막 저장에서 재개"를 뺀다.</summary>
        public static DeathChoice[] DeathChoices(bool hasSave)
        {
            if (hasSave)
            {
                return new[] { DeathChoice.ResumeLast, DeathChoice.Title, DeathChoice.Quit };
            }
            return new[] { DeathChoice.Title, DeathChoice.Quit };
        }
    }
}
