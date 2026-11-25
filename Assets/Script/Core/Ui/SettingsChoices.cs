using System;

namespace SCoW.Core.Ui
{
    /// <summary>설정창 선택지 표와 돌려 고르기 계산(UX-2). 순수 C#.</summary>
    public static class SettingsChoices
    {
        /// <summary>자동 진행 대기 시간 후보(초).</summary>
        public static readonly float[] AutoWaitSec = { 1f, 1.5f, 2f, 3f };

        /// <summary>index 에 delta 를 더해 0..count-1 안에서 돌린다. count 가 1 이하면 0.</summary>
        public static int Cycle(int index, int delta, int count)
        {
            if (count <= 1)
            {
                return 0;
            }
            int v = (index + delta) % count;
            return v < 0 ? v + count : v;
        }

        /// <summary>value 를 0..1 로 조여 step 만큼 움직인다(슬라이더 키보드 조작). 부동소수 오차는 step 단위로 반올림.</summary>
        public static float StepSlider(float value, int dir, float step, float min, float max)
        {
            float v = value + dir * step;
            v = (float)Math.Round(v / step) * step;
            return Math.Min(max, Math.Max(min, v));
        }

        /// <summary>AutoWaitSec 후보 중 sec 에 가장 가까운 칸 번호.</summary>
        public static int NearestAutoWait(float sec)
        {
            int best = 0;
            float bestGap = float.MaxValue;
            for (int i = 0; i < AutoWaitSec.Length; i++)
            {
                float gap = Math.Abs(AutoWaitSec[i] - sec);
                if (gap < bestGap)
                {
                    bestGap = gap;
                    best = i;
                }
            }
            return best;
        }
    }
}
