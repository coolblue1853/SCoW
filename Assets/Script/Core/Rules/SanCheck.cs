namespace SCoW.Core.Rules
{
    public enum SanFailOutcome { Decline, Insanity, Deconditioning }
    public enum SanSuccessOutcome { NoChange, Rise, RaiseCondition }

    /// <summary>SAN 체크 후속 룰렛 규칙(Rollet 의 OpenSanRollet·ChangeSanRollet·ResultRollet 포팅).</summary>
    public static class SanCheck
    {
        public const int SuccessNoChangePct = 80;
        public const int SuccessRisePct = 15;
        public const int SuccessRaiseConditionPct = 5;

        /// <summary>실패 룰렛 3칸 비율(%). debuff 0~3, worst 면 컨디션 칸 0. 합 100.</summary>
        public static void FailurePercents(int debuff, bool worst, out int decline, out int insanity, out int decon)
        {
            switch (debuff)
            {
                case 0:
                    decline = 50; insanity = worst ? 50 : 30; decon = worst ? 0 : 20;
                    break;
                case 1:
                    decline = 30; insanity = worst ? 70 : 50; decon = worst ? 0 : 20;
                    break;
                case 2:
                    decline = 20; insanity = worst ? 80 : 60; decon = worst ? 0 : 20;
                    break;
                default:
                    decline = worst ? 100 : 40; insanity = 0; decon = worst ? 0 : 60;
                    break;
            }
        }

        /// <param name="roll">1~100 굴림</param>
        public static SanFailOutcome ResolveFailure(int roll, int debuff, bool worst)
        {
            FailurePercents(debuff, worst, out int decline, out int insanity, out _);
            if (roll <= decline)
            {
                return SanFailOutcome.Decline;
            }
            if (roll <= decline + insanity)
            {
                return SanFailOutcome.Insanity;
            }
            return SanFailOutcome.Deconditioning;
        }

        public static SanSuccessOutcome ResolveSuccess(int roll)
        {
            if (roll <= SuccessNoChangePct)
            {
                return SanSuccessOutcome.NoChange;
            }
            if (roll <= SuccessNoChangePct + SuccessRisePct)
            {
                return SanSuccessOutcome.Rise;
            }
            return SanSuccessOutcome.RaiseCondition;
        }

        public static SanFailOutcome RollFailure(IRng rng, int debuff, bool worst)
        {
            return ResolveFailure(rng.Next(1, 101), debuff, worst);
        }

        public static SanSuccessOutcome RollSuccess(IRng rng)
        {
            return ResolveSuccess(rng.Next(1, 101));
        }

        /// <summary>SAN 감소량: Range(0,3)==1 이면 10, 아니면 5(확률 1/3).</summary>
        public static int RollLoss(IRng rng)
        {
            return rng.Next(0, 3) == 1 ? 10 : 5;
        }
    }
}
