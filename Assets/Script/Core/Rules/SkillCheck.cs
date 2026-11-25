namespace SCoW.Core.Rules
{
    /// <summary>컨디션(전역 문자열 DataBaseManager.condition 과 1:1).</summary>
    public enum Condition { Nomal, Good, Best, Bad, Worst }

    public enum SuccessLevel { Fumble, Failure, Success, CriticalSuccess }

    /// <summary>룰렛 눈 6개. 1의 자리 0~9, 10의 자리 0~90. 굴리는 순서는 UI 의 ChangeRollet 과 같다.</summary>
    public readonly struct DiceFaces
    {
        public readonly int Units;
        public readonly int Tens;
        public readonly int BadUnits;
        public readonly int BadTens;
        public readonly int GoodUnits;
        public readonly int GoodTens;

        public DiceFaces(int units, int tens, int badUnits, int badTens, int goodUnits, int goodTens)
        {
            Units = units;
            Tens = tens;
            BadUnits = badUnits;
            BadTens = badTens;
            GoodUnits = goodUnits;
            GoodTens = goodTens;
        }
    }

    public readonly struct RollResult
    {
        public readonly int Value;
        public readonly int Target;
        public readonly SuccessLevel Level;

        public RollResult(int value, int target, SuccessLevel level)
        {
            Value = value;
            Target = target;
            Level = level;
        }

        public bool IsSuccess => Level == SuccessLevel.Success || Level == SuccessLevel.CriticalSuccess;
        public string LegacyString => SkillCheck.ToLegacyString(Level);
    }

    /// <summary>
    /// 현 판정 규칙 그대로의 포팅(7판 정정 금지). 대실패 95+ 고정, 대성공 = 기능치/3,
    /// 컨디션은 1의/10의 자리 보너스·페널티 눈. 00+0 = 100.
    /// </summary>
    public static class SkillCheck
    {
        public const int MaxTarget = 90;
        public const int FumbleFrom = 95;

        public static int ClampTarget(int target)
        {
            return target > MaxTarget ? MaxTarget : (target < 0 ? 0 : target);
        }

        public static Condition ParseCondition(string text)
        {
            switch (text)
            {
                case "Good": return Condition.Good;
                case "Best": return Condition.Best;
                case "Bad": return Condition.Bad;
                case "Worst": return Condition.Worst;
                default: return Condition.Nomal;
            }
        }

        public static DiceFaces RollFaces(IRng rng)
        {
            int u = rng.Next(0, 10);
            int t = rng.Next(0, 10) * 10;
            int bu = rng.Next(0, 10);
            int bt = rng.Next(0, 10) * 10;
            int gu = rng.Next(0, 10);
            int gt = rng.Next(0, 10) * 10;
            return new DiceFaces(u, t, bu, bt, gu, gt);
        }

        /// <summary>컨디션을 반영한 최종 d100 값(1~100).</summary>
        public static int ResolveValue(Condition condition, DiceFaces f)
        {
            int units = f.Units;
            int tens = f.Tens;
            switch (condition)
            {
                case Condition.Good:
                    if (f.Units > f.GoodUnits) { units = f.GoodUnits; }
                    break;
                case Condition.Best:
                    if (f.Tens > f.GoodTens) { tens = f.GoodTens; }
                    break;
                case Condition.Bad:
                    if (f.Units <= f.BadUnits) { units = f.BadUnits; }
                    break;
                case Condition.Worst:
                    if (f.Tens <= f.BadTens) { tens = f.BadTens; }
                    break;
            }
            int v = tens + units;
            return v == 0 ? 100 : v;
        }

        /// <param name="perfectionism">완벽주의 디버프: 일반 Success 를 Failure 로 강등(Critical 은 유지)</param>
        public static SuccessLevel Evaluate(int value, int target, bool perfectionism = false)
        {
            if (value >= FumbleFrom)
            {
                return SuccessLevel.Fumble;
            }
            if (value <= target / 3f)
            {
                return SuccessLevel.CriticalSuccess;
            }
            if (value <= target)
            {
                return perfectionism ? SuccessLevel.Failure : SuccessLevel.Success;
            }
            return SuccessLevel.Failure;
        }

        public static RollResult Resolve(Condition condition, DiceFaces faces, int target, bool perfectionism = false)
        {
            int v = ResolveValue(condition, faces);
            return new RollResult(v, target, Evaluate(v, target, perfectionism));
        }

        public static RollResult Roll(IRng rng, int target, Condition condition, bool perfectionism = false)
        {
            return Resolve(condition, RollFaces(rng), target, perfectionism);
        }

        /// <summary>
        /// 성공(일반+대성공) 확률 0~1. Evaluate 를 컨디션별 주사위 눈 10^4 가지(1·10의 자리 + 보너스/페널티 눈 한 쌍)에 전부 돌려 센다.
        /// 룰렛 표시는 이 값을 쓴다(손으로 쓴 식과 어긋나지 않게). 난수는 쓰지 않는다.
        /// </summary>
        public static double SuccessChance(Condition condition, int target, bool perfectionism = false)
        {
            int clamped = ClampTarget(target);
            int hits = 0;
            for (int u = 0; u < 10; u++)
            {
                for (int t = 0; t < 10; t++)
                {
                    for (int xu = 0; xu < 10; xu++)
                    {
                        for (int xt = 0; xt < 10; xt++)
                        {
                            DiceFaces f = new DiceFaces(u, t * 10, xu, xt * 10, xu, xt * 10);
                            SuccessLevel level = Evaluate(ResolveValue(condition, f), clamped, perfectionism);
                            if (level == SuccessLevel.Success || level == SuccessLevel.CriticalSuccess)
                            {
                                hits++;
                            }
                        }
                    }
                }
            }
            return hits / 10000.0;
        }

        /// <summary>룰렛에 표시하는 성공 확률 정수 %. SuccessChance 를 반올림한다.</summary>
        public static int SuccessPercent(Condition condition, int target, bool perfectionism = false)
        {
            return PercentOf(SuccessChance(condition, target, perfectionism));
        }

        /// <summary>SuccessChance 값(0~1)을 표시용 정수 %로. 표시 경로는 모두 이 한 곳을 지난다.</summary>
        public static int PercentOf(double chance)
        {
            return (int)System.Math.Round(chance * 100.0);
        }

        /// <summary>기존 InteractionController 등이 비교하는 문자열 4종.</summary>
        public static string ToLegacyString(SuccessLevel level)
        {
            switch (level)
            {
                case SuccessLevel.Fumble: return "Result : Fumble";
                case SuccessLevel.CriticalSuccess: return "Result : Critical Success";
                case SuccessLevel.Success: return "Result : Success";
                default: return "Result : Failure";
            }
        }
    }
}
