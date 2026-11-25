namespace SCoW.Core.Ui
{
    /// <summary>
    /// 화면에 흩어진 단추들 사이에서 방향(상하좌우)으로 다음 포커스를 고른다(UX-9 지도 포커스).
    /// 좌표는 화면 좌표(위쪽이 y 큼). dx/dy 는 -1·0·+1 중 하나(한 축만 쓴다). 순수 C#.
    /// </summary>
    public static class DirectionalFocus
    {
        /// <summary>처음 포커스: 가장 위쪽, 같으면 가장 왼쪽. 항목이 없으면 -1.</summary>
        public static int First(float[] xs, float[] ys, int count)
        {
            int best = -1;
            for (int i = 0; i < count; i++)
            {
                if (best < 0 || ys[i] > ys[best] + 0.5f || (ys[i] > ys[best] - 0.5f && xs[i] < xs[best]))
                {
                    best = i;
                }
            }
            return best;
        }

        /// <summary>current 에서 (dx,dy) 방향으로 가장 가까운 항목. 그 방향에 없으면 current 그대로.</summary>
        public static int Pick(float[] xs, float[] ys, int count, int current, int dx, int dy)
        {
            if (current < 0 || current >= count)
            {
                return First(xs, ys, count);
            }
            int best = current;
            float bestScore = float.MaxValue;
            for (int i = 0; i < count; i++)
            {
                if (i == current)
                {
                    continue;
                }
                float rx = xs[i] - xs[current];
                float ry = ys[i] - ys[current];
                float along = rx * dx + ry * dy;
                if (along <= 0.5f)
                {
                    continue;
                }
                float across = dx != 0 ? ry : rx;
                if (across < 0f)
                {
                    across = -across;
                }
                float score = along + across * 2f;
                if (score < bestScore)
                {
                    bestScore = score;
                    best = i;
                }
            }
            return best;
        }
    }
}
