using System.Text;
using SCoW.Core.Rules;

namespace SCoW.Core.Ui
{
    /// <summary>
    /// 전투 툴팁 글(UX-11, 임시 영문). 순수 C#. TODO(I2): 문구를 I2 키로 옮긴다.
    /// 적에게 포커스하면 선택한 공격의 예상 성공률(룰렛과 같은 SkillCheck.SuccessPercent)을 한 줄 덧붙인다.
    /// </summary>
    public static class BattleTooltipText
    {
        public static string UnitName(IBattleUnit u)
        {
            PartyMember m = u as PartyMember;
            if (m != null)
            {
                return m.IsLeader ? "Fabian" : m.DisplayName;
            }
            EnemyUnit e = u as EnemyUnit;
            if (e != null)
            {
                return e.State.Def.Kind == EnemyKind.DeepOne ? "Deep One" : "Hybrid";
            }
            return u == null ? string.Empty : u.Id;
        }

        public static string StatusLabel(IBattleUnit u)
        {
            PartyMember m = u as PartyMember;
            if (m == null)
            {
                return string.Empty;
            }
            switch (m.Status)
            {
                case UnitStatus.Downed: return "Down (" + m.DownedCountdown + ")";
                case UnitStatus.Stable: return "Stable";
                case UnitStatus.Fallen: return "Fallen";
                case UnitStatus.Broken: return "Broken";
                case UnitStatus.Recovering: return "Recovering";
            }
            if (m.Bout != BoutKind.None)
            {
                return "Panic: " + m.Bout;
            }
            if (m.Poison > 0)
            {
                return "Poisoned";
            }
            return m.Guarding ? "Guard" : string.Empty;
        }

        public static string Vitals(IBattleUnit u)
        {
            PartyMember m = u as PartyMember;
            if (m != null)
            {
                return "HP " + m.Hp + "/" + m.MaxHp + "  SAN " + m.San + "/" + m.MaxSan;
            }
            EnemyUnit e = u as EnemyUnit;
            return e == null ? string.Empty : "HP " + e.State.NowHp + "/" + e.State.FullHp;
        }

        /// <summary>유닛 툴팁. preview 가 Valid 면 마지막 줄에 예상 성공률·피해 범위를 적는다.</summary>
        public static string ForUnit(IBattleUnit u, AttackPreview preview = default)
        {
            if (u == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(UnitName(u));
            string status = StatusLabel(u);
            if (status.Length > 0)
            {
                sb.Append("  [").Append(status).Append(']');
            }
            sb.Append('\n').Append(Vitals(u));
            if (preview.Valid)
            {
                sb.Append("\nHit chance: ").Append(preview.Percent).Append('%');
                if (preview.Condition == SCoW.Core.Rules.Condition.Worst)
                {
                    sb.Append(" (penalty)");
                }
                else if (preview.Condition == SCoW.Core.Rules.Condition.Best)
                {
                    sb.Append(" (bonus)");
                }
                sb.Append("\nDamage: ").Append(preview.MinDamage).Append('-').Append(preview.MaxDamage);
            }
            return sb.ToString();
        }

        /// <summary>툴팁 상자를 화면 안으로 넣는다(좌하단 기준 좌표, 앵커 아래쪽 우선, 넘치면 위쪽).</summary>
        public static void Place(float anchorX, float anchorTop, float anchorBottom, float width, float height, float screenW, float screenH,
            out float x, out float y)
        {
            x = anchorX - width * 0.5f;
            if (x + width > screenW)
            {
                x = screenW - width;
            }
            if (x < 0f)
            {
                x = 0f;
            }
            y = anchorBottom - height;
            if (y < 0f)
            {
                y = anchorTop;
            }
            if (y + height > screenH)
            {
                y = screenH - height;
            }
        }
    }
}
