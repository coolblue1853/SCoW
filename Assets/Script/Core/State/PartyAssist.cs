using System.Collections.Generic;
using SCoW.Core.Rules;
using SCoW.Core.Story;

namespace SCoW.Core.State
{
    public enum AssistMode { None = 0, Assist = 1, Swap = 2 }

    /// <summary>동료 반응 대사 종류(Party.csv). 값은 CSV ID 계산에 쓰이므로 바꾸지 않는다.</summary>
    public enum ReactionKind { Assist = 0, Swap = 1, San = 2 }

    /// <summary>판정 전 칩 하나: 어느 동료가 어떤 방식으로, 어떤 기능치로 나서는가.</summary>
    public sealed class AssistOption
    {
        public readonly CompanionDef Def;
        public readonly AssistMode Mode;
        public readonly int Skill;

        public AssistOption(CompanionDef def, AssistMode mode, int skill)
        {
            Def = def;
            Mode = mode;
            Skill = skill;
        }
    }

    /// <summary>동료 SAN 자동 판정 한 명 분의 결과.</summary>
    public sealed class SanAutoResult
    {
        public CompanionDef Def;
        public int Target;
        public int Roll;
        public bool Success;
        public int Loss;
    }

    /// <summary>
    /// 탐색 판정 보조(Party.md 3절, PT-4). 후보 계산, 대신 판정 대가, 동료 SAN 자동 판정, 반응 대사 ID. 순수 C#(난수는 IRng 주입).
    /// 임시 가정: 대신 판정은 파비안 컨디션을 쓰지 않는다(Nomal). 실패 대가는 일반 실패 1, 대실패 3 HP(최소 1 남김).
    /// </summary>
    public static class PartyAssist
    {
        public const int AssistMinSkill = 40;
        public const int SwapMargin = 10;
        public const int SwapFailureDamageNormal = 1;
        public const int SwapFailureDamageFumble = 3;
        private const int LinesPerCompanion = 6;

        /// <summary>룰렛 기능 이름(SetRollet 의 point_sting)에서 동료 기능 키를 얻는다. 도움·대신이 안 되는 이름(Sanity, Luck, Health, Stealth 등)은 null.
        /// Stealth 는 하수도 단체 은신 판정(Party.md 3-3)이라 제외. "Str" 은 능력치라 키가 없다(TryGetSkill 이 따로 읽는다).</summary>
        public static string SkillKeyOf(string pointName)
        {
            switch (pointName)
            {
                case "Rhetoric": return CounterKeys.rhetoricPoint;
                case "Swin": return CounterKeys.swimingPoint;
                case "Observation": return CounterKeys.observationPoint;
                case "Occult": return CounterKeys.occultPoint;
                case "Analysis": return CounterKeys.analysisPoint;
                case "Medicine": return CounterKeys.medicinePoint;
                case "Deftness": return CounterKeys.deftnessPoint;
                default: return null;
            }
        }

        /// <summary>도움·대신이 열리는 판정인가: 대사 판정(subject "dialog")이고 지원하는 기능이다. 전투·SAN·행운은 아니다.</summary>
        public static bool IsEligible(string subject, string pointName)
        {
            return subject == "dialog" && (pointName == "Str" || SkillKeyOf(pointName) != null);
        }

        public static bool TryGetSkill(CompanionDef def, string pointName, out int skill)
        {
            skill = 0;
            if (def == null)
            {
                return false;
            }
            if (pointName == "Str")
            {
                skill = def.Str;
                return true;
            }
            string key = SkillKeyOf(pointName);
            if (key == null)
            {
                return false;
            }
            skill = def.GetSkill(key);
            return true;
        }

        /// <summary>
        /// 이 판정의 칩 후보. 합류·생존·대기 아님·Ready 인 동료만. 도움: 동료 기능 >= 40. 대신: 동료 기능 >= 파비안 목표치 + 10.
        /// 같은 동료가 둘 다 되면 도움 칩 다음에 대신 칩. 후보가 없으면 빈 목록(칩 UI 안 뜸).
        /// </summary>
        public static List<AssistOption> Options(GameState s, string subject, string pointName, int fabianTarget)
        {
            List<AssistOption> list = new List<AssistOption>();
            if (s == null || !IsEligible(subject, pointName))
            {
                return list;
            }
            List<CompanionDef> active = PartyState.ActiveCompanions(s);
            for (int i = 0; i < active.Count; i++)
            {
                CompanionDef def = active[i];
                int skill;
                if (PartyState.IsStandby(s, def.Id) || PartyState.GetStatus(s, def.Id) != UnitStatus.Ready || !TryGetSkill(def, pointName, out skill))
                {
                    continue;
                }
                if (skill >= AssistMinSkill)
                {
                    list.Add(new AssistOption(def, AssistMode.Assist, skill));
                }
                if (skill >= fabianTarget + SwapMargin)
                {
                    list.Add(new AssistOption(def, AssistMode.Swap, skill));
                }
            }
            return list;
        }

        public static int SwapFailureDamage(SuccessLevel level)
        {
            return level == SuccessLevel.Fumble ? SwapFailureDamageFumble : (level == SuccessLevel.Failure ? SwapFailureDamageNormal : 0);
        }

        /// <summary>대신 판정이 실패했을 때 동료가 받는 HP 손실을 적용한다(최소 1 남김: 탐색 중 죽이지 않는다). 실제로 깎은 양을 돌려준다.</summary>
        public static int ApplySwapPenalty(GameState s, CompanionDef def, SuccessLevel level)
        {
            int want = SwapFailureDamage(level);
            if (s == null || def == null || want <= 0)
            {
                return 0;
            }
            int hp = PartyState.GetHp(s, def.Id);
            int next = hp - want < 1 ? 1 : hp - want;
            if (next >= hp)
            {
                return 0;
            }
            PartyState.SetHp(s, def, next);
            return hp - next;
        }

        /// <summary>동료 SAN 이 함께 판정되는 SAN 체크 장면인가(Shared). 임시 가정: 하수도 첫 진입 2곳과 전투 SAN. 나머지는 파비안 단독(Solo).</summary>
        public static bool IsSharedSanSubject(string subject)
        {
            if (string.IsNullOrEmpty(subject))
            {
                return false;
            }
            return subject == "Sewer_FirstInSan" || subject == "DeepOneFirstSan" || subject.StartsWith("BattleSan_", System.StringComparison.Ordinal);
        }

        /// <summary>
        /// 동료 전원 SAN 자동 판정(Party.md 3-4). 각자 자기 현재 SAN 을 목표치로 SkillCheck.Roll, 실패하면 SanCheck.RollLoss 만큼 깎는다.
        /// 합류한 동료가 없으면 난수를 쓰지 않는다(기존 장면 회귀 불변).
        /// </summary>
        public static List<SanAutoResult> AutoSanChecks(GameState s, IRng rng)
        {
            List<SanAutoResult> results = new List<SanAutoResult>();
            if (s == null)
            {
                return results;
            }
            List<CompanionDef> active = PartyState.ActiveCompanions(s);
            for (int i = 0; i < active.Count; i++)
            {
                CompanionDef def = active[i];
                if (PartyState.IsStandby(s, def.Id))
                {
                    continue;
                }
                int san = PartyState.GetSan(s, def.Id);
                RollResult r = SkillCheck.Roll(rng, san, SCoW.Core.Rules.Condition.Nomal);
                SanAutoResult res = new SanAutoResult();
                res.Def = def;
                res.Target = SkillCheck.ClampTarget(san);
                res.Roll = r.Value;
                res.Success = r.IsSuccess;
                res.Loss = res.Success ? 0 : SanCheck.RollLoss(rng);
                if (res.Loss > 0)
                {
                    PartyState.SetSan(s, def, san - res.Loss);
                }
                results.Add(res);
            }
            return results;
        }

        /// <summary>Party.csv 의 한 줄 ID. 동료별 6줄(도움 성공·실패, 대신 성공·실패, SAN 성공·실패) 순서로 1부터. 모르는 동료는 null.</summary>
        public static string ReactionId(string companionId, ReactionKind kind, bool success)
        {
            for (int i = 0; i < CompanionDefs.All.Length; i++)
            {
                if (CompanionDefs.All[i].Id == companionId)
                {
                    return (i * LinesPerCompanion + (int)kind * 2 + (success ? 0 : 1) + 1).ToString();
                }
            }
            return null;
        }

        /// <summary>CSV 줄 목록에서 ID 로 대사 본문(@ 표기 제거)을 찾는다. 없으면 빈 문자열.</summary>
        public static string FindReactionText(IList<StoryLine> lines, string id)
        {
            if (lines == null || id == null)
            {
                return string.Empty;
            }
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Id == id)
                {
                    return lines[i].Text.Replace("@", string.Empty).Trim();
                }
            }
            return string.Empty;
        }
    }
}
