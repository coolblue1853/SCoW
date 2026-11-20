namespace SCoW.Core.Rules
{
    /// <summary>난수 주입 이음매. Core 는 UnityEngine 을 모르므로 구현은 바깥(UnityRng)·테스트(SeededRng)가 맡는다.</summary>
    public interface IRng
    {
        /// <summary>minInclusive 이상 maxExclusive 미만. Random.Range(int,int) 와 같은 의미.</summary>
        int Next(int minInclusive, int maxExclusive);
    }

    /// <summary>시드 고정 결정적 난수(LCG). 골든 테스트·재현용.</summary>
    public sealed class SeededRng : IRng
    {
        private ulong _state;

        public SeededRng(int seed)
        {
            _state = (ulong)(uint)seed * 6364136223846793005UL + 1442695040888963407UL;
        }

        public int Next(int minInclusive, int maxExclusive)
        {
            if (maxExclusive <= minInclusive)
            {
                return minInclusive;
            }
            _state = _state * 6364136223846793005UL + 1442695040888963407UL;
            uint r = (uint)(_state >> 33);
            return minInclusive + (int)(r % (uint)(maxExclusive - minInclusive));
        }
    }
}
