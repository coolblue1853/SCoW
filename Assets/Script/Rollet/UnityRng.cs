using SCoW.Core.Rules;

/// <summary>IRng 의 UnityEngine.Random 구현. Random.InitState 시드가 그대로 적용된다.</summary>
public sealed class UnityRng : IRng
{
    public static readonly UnityRng Instance = new UnityRng();

    public int Next(int minInclusive, int maxExclusive)
    {
        return UnityEngine.Random.Range(minInclusive, maxExclusive);
    }
}
