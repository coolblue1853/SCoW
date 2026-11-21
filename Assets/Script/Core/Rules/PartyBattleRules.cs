using SCoW.Core.State;

namespace SCoW.Core.Rules
{
    /// <summary>파티 전투의 순수 규칙 조각(무기 분류·발작 판정·대상 추첨). 상태 없음.</summary>
    public static class PartyBattleRules
    {
        /// <summary>한 번에 이만큼 이상 잃어야 발작 판정이 열린다(7판 "5 이상"을 x5 스케일로 환산, Party.md 4-6절).</summary>
        public const int BoutLossThreshold = 10;
        public const int WitnessSanLoss = 5;
        public const int CalmSanGain = 5;
        public const int FirstAidHeal = 5;
        public const int StableHp = 5;

        /// <summary>행동의 대상 쪽: 공격류는 반대편, 그 외는 자기편.</summary>
        public static Side TargetSideOf(Side actorSide, ActionKind kind)
        {
            bool attack = kind == ActionKind.Melee || kind == ActionKind.Ranged || kind == ActionKind.Throw
                || kind == ActionKind.SanAttack || kind == ActionKind.PoisonAttack;
            return attack == (actorSide == Side.Ally) ? Side.Enemy : Side.Ally;
        }

        public static bool IsSuccess(SuccessLevel level)
        {
            return level == SuccessLevel.Success || level == SuccessLevel.CriticalSuccess;
        }

        public static bool IsRangedWeapon(string weapon)
        {
            return weapon == "SmallPistol" || weapon == "Revolver" || weapon == "Rifle" || weapon == "Shotgun";
        }

        public static bool IsMeleeWeapon(string weapon)
        {
            return weapon == "Dagger" || weapon == "Bat" || weapon == "Axe";
        }

        /// <summary>쓸 무기. 없으면 null(근접은 맨손 무술).</summary>
        public static string FindWeapon(PartyMember m, bool ranged)
        {
            for (int i = 0; i < m.Weapons.Count; i++)
            {
                string w = m.Weapons[i];
                if (ranged ? IsRangedWeapon(w) : IsMeleeWeapon(w))
                {
                    return w;
                }
            }
            return null;
        }

        /// <summary>공격 기능 키. 사격 = 사격, 단검 = 검술, 그 외 근접 = 무술.</summary>
        public static string AttackSkillKey(PartyMember m, bool ranged)
        {
            if (ranged)
            {
                return CounterKeys.gunShotPoint;
            }
            return FindWeapon(m, false) == "Dagger" ? CounterKeys.swordPoint : CounterKeys.martialArtsPoint;
        }

        public static DiceExpr AttackDamage(PartyMember m, bool ranged)
        {
            return BattleDamage.ForWeapon(FindWeapon(m, ranged));
        }

        /// <summary>보너스 다이스 순합(+/-)을 컨디션으로. 이 단계의 판정은 컨디션 한 단계만 쓴다(PT-4 에서 다중 보너스 다이스).</summary>
        public static Condition ConditionFromNet(int net)
        {
            return net > 0 ? Condition.Best : (net < 0 ? Condition.Worst : Condition.Nomal);
        }

        /// <summary>적의 단순 판정(옛 시뮬 EnemyRoll). 난수 2회, 컨디션 없음.</summary>
        public static SuccessLevel RollPlain(IRng rng, int skill)
        {
            int u = rng.Next(0, 10);
            int t = rng.Next(0, 10) * 10;
            int v = SkillCheck.ResolveValue(Condition.Nomal, new DiceFaces(u, t, 0, 0, 0, 0));
            return SkillCheck.Evaluate(v, SkillCheck.ClampTarget(skill));
        }

        /// <summary>
        /// 발작 판정. 손실이 10 미만이면 난수 없이 false. 아니면 INT 판정(실패 = 발작), 종류 1d4, 지속 1d3 턴.
        /// </summary>
        public static bool RollBout(IRng rng, int intl, int loss, out BoutKind kind, out int turns)
        {
            kind = BoutKind.None;
            turns = 0;
            if (loss < BoutLossThreshold)
            {
                return false;
            }
            if (SkillCheck.Roll(rng, SkillCheck.ClampTarget(intl), Condition.Nomal).IsSuccess)
            {
                return false;
            }
            kind = (BoutKind)rng.Next(1, 5);
            turns = rng.Next(1, 4);
            return true;
        }

        /// <summary>
        /// 가중 추첨으로 칸을 고른다. 가중치는 점유된 칸만 합산한다(빈 칸이면 정규화). 점유 칸이 하나면 난수를 쓰지 않는다.
        /// 합이 0 이면(독 가중치 {1,0,0} 인데 앞이 비었을 때 등) 점유 칸 균등. 점유 칸이 없으면 -1.
        /// </summary>
        public static int PickWeighted(int[] weights, bool[] occupied, IRng rng)
        {
            int count = 0;
            int only = -1;
            int total = 0;
            for (int i = 0; i < occupied.Length; i++)
            {
                if (!occupied[i])
                {
                    continue;
                }
                count++;
                only = i;
                total += i < weights.Length ? weights[i] : 0;
            }
            if (count == 0)
            {
                return -1;
            }
            if (count == 1)
            {
                return only;
            }
            if (total <= 0)
            {
                int k = rng.Next(0, count);
                for (int i = 0; i < occupied.Length; i++)
                {
                    if (occupied[i] && k-- == 0)
                    {
                        return i;
                    }
                }
                return only;
            }
            int r = rng.Next(0, total);
            for (int i = 0; i < occupied.Length; i++)
            {
                if (!occupied[i])
                {
                    continue;
                }
                int w = i < weights.Length ? weights[i] : 0;
                if (r < w)
                {
                    return i;
                }
                r -= w;
            }
            return only;
        }
    }
}
