using SCoW.Core.State;

namespace SCoW.Core.Rules
{
    /// <summary>공격을 하기 전에 보여 주는 예상(툴팁, UX-11). 난수를 쓰지 않고 상태도 바꾸지 않는다.</summary>
    public readonly struct AttackPreview
    {
        /// <summary>대상 칸이 이 행동으로 닿는가.</summary>
        public readonly bool Valid;
        /// <summary>행위자 자신의 판정 성공 확률(정수 %, 룰렛과 같은 SkillCheck.SuccessPercent).</summary>
        public readonly int Percent;
        public readonly Condition Condition;
        /// <summary>적이 막는 쪽 기능치(근접 = 무술, 사격 = 회피).</summary>
        public readonly int DefenseSkill;
        public readonly int MinDamage;
        public readonly int MaxDamage;

        public AttackPreview(bool valid, int percent, Condition condition, int defenseSkill, int minDamage, int maxDamage)
        {
            Valid = valid;
            Percent = percent;
            Condition = condition;
            DefenseSkill = defenseSkill;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }
    }

    public sealed partial class BattleField
    {
        /// <summary>아군 공격(근접/사격)의 예상 성공률. 규칙은 AllyAttack 과 같은 컨디션(앞 칸 사격 불리, 절망 이웃 불리).</summary>
        public AttackPreview PreviewAttack(PartyMember actor, ActionKind kind, int targetRank)
        {
            bool ranged = kind == ActionKind.Ranged;
            if (actor == null || (kind != ActionKind.Melee && !ranged) || !IsValidTarget(actor, kind, targetRank))
            {
                return new AttackPreview(false, 0, Condition.Nomal, 0, 0, 0);
            }
            EnemyUnit t = (EnemyUnit)Enemies[targetRank].Unit;
            int net = NeighborPenalty(actor) - (ranged && actor.Rank == 0 ? 1 : 0);
            Condition cond = PartyBattleRules.ConditionFromNet(net);
            int skill = SkillCheck.ClampTarget(actor.GetSkill(PartyBattleRules.AttackSkillKey(actor, ranged)));
            DiceExpr dmg = PartyBattleRules.AttackDamage(actor, ranged);
            return new AttackPreview(true, SkillCheck.SuccessPercent(cond, skill), cond, ranged ? t.State.Evasion : t.State.MartialArts, dmg.Min, dmg.Max);
        }
    }
}
