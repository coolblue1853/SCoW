using System.Collections.Generic;

namespace SCoW.Core.Rules
{
    /// <summary>
    /// 속도 게이지 턴 순서(옛 BattleManager.SymbolUp 을 행위자 전원으로 넓힘).
    /// 한 틱에 order 순서대로 각 행위자를 본다: 게이지가 가득이면 -130 으로 리셋하고 그 행위자가 차례를 받으며(뒤 순서는 이번 틱에 오르지 않는다),
    /// 아니면 StepGauge 로 오른다. 동시 도달은 order 앞쪽이 먼저다(옛 1:1 로그와 동치. DEX 우선 타이브레이크는 쓰지 않는다).
    /// </summary>
    public static class BattleTimeline
    {
        public const int MaxTicks = 100000;

        /// <summary>다음 차례 행위자. HasTurns 가 아무도 없으면 null. 호출하면 게이지가 실제로 갱신된다.</summary>
        public static IBattleUnit Next(IReadOnlyList<IBattleUnit> order)
        {
            float[] g = Copy(order);
            int index = Advance(order, g);
            for (int i = 0; i < order.Count; i++)
            {
                order[i].Gauge = g[i];
            }
            return index < 0 ? null : order[index];
        }

        /// <summary>다음 n 명(순서 바용). 게이지 복사본으로 시뮬레이트하므로 상태를 바꾸지 않는다.</summary>
        public static List<IBattleUnit> Preview(IReadOnlyList<IBattleUnit> order, int n)
        {
            List<IBattleUnit> result = new List<IBattleUnit>();
            float[] g = Copy(order);
            for (int i = 0; i < n; i++)
            {
                int index = Advance(order, g);
                if (index < 0)
                {
                    break;
                }
                result.Add(order[index]);
            }
            return result;
        }

        private static float[] Copy(IReadOnlyList<IBattleUnit> order)
        {
            float[] g = new float[order.Count];
            for (int i = 0; i < g.Length; i++)
            {
                g[i] = order[i].Gauge;
            }
            return g;
        }

        private static int Advance(IReadOnlyList<IBattleUnit> order, float[] g)
        {
            for (int tick = 0; tick < MaxTicks; tick++)
            {
                bool any = false;
                for (int i = 0; i < order.Count; i++)
                {
                    IBattleUnit u = order[i];
                    if (!u.HasTurns)
                    {
                        continue;
                    }
                    any = true;
                    if (BattleEngine.IsGaugeFull(g[i]))
                    {
                        g[i] = BattleEngine.GaugeReset;
                        return i;
                    }
                    g[i] = BattleEngine.StepGauge(g[i], u.Dex, u.Side == Side.Ally);
                }
                if (!any)
                {
                    return -1;
                }
            }
            return -1;
        }
    }
}
