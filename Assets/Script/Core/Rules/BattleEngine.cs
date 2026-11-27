using System.Collections.Generic;

namespace SCoW.Core.Rules
{
    /// <summary>룰렛에서 플레이어·적 판정 단계를 비교하는 대립 판정 종류(옛 BattleRollet.subject).</summary>
    public enum ContestKind { Evasion, CounterAttack, MartialArts, Swordsmanship, RunAway }

    /// <summary>
    /// 피해 주사위식 모음. 모두 옛 Random.Range 식 그대로 이행(동치 우선): 상한 제외값 때문에
    /// 주석과 실제 면 수가 다른 곳(산탄총 4d5, 돌 1d2)도 보존한다. 전부 x5 배율(HP 단위).
    /// </summary>
    public static class BattleDamage
    {
        // 적 -> 플레이어 (회피/반격/공격 실패)
        public static readonly DiceExpr Hit1D3 = new DiceExpr(1, 3, 0, 5);
        public static readonly DiceExpr Hit1D3Plus1 = new DiceExpr(1, 3, 1, 5);
        public static readonly DiceExpr Hit2D3 = new DiceExpr(2, 3, 0, 5);
        /// <summary>독 도트: Random.Range(1, 3) * 5 (1d2).</summary>
        public static readonly DiceExpr Poison = new DiceExpr(1, 2, 0, 5);

        // 플레이어 -> 적
        public static readonly DiceExpr Martial = Hit1D3;
        public static readonly DiceExpr Dagger = new DiceExpr(1, 4, 2, 5);
        public static readonly DiceExpr Bat = new DiceExpr(1, 8, 0, 5);
        public static readonly DiceExpr Axe = new DiceExpr(1, 8, 0, 5);
        public static readonly DiceExpr SmallPistol = new DiceExpr(1, 6, 0, 5);
        public static readonly DiceExpr Revolver = new DiceExpr(1, 10, 2, 5);
        public static readonly DiceExpr Rifle = new DiceExpr(2, 6, 2, 5);
        /// <summary>옛 코드 주석은 1D6 이나 실제 식은 Range(1,6) 4회 = 4d5. 동치 위해 보존.</summary>
        public static readonly DiceExpr Shotgun = new DiceExpr(4, 5, 0, 5);
        /// <summary>옛 코드 주석은 1D6 이나 실제 식은 Range(1,3) = 1d2. 동치 위해 보존.</summary>
        public static readonly DiceExpr Rock = new DiceExpr(1, 2, 0, 5);
        public static readonly DiceExpr Molotov = new DiceExpr(2, 6, 2, 5);

        public static DiceExpr ForWeapon(string weapon)
        {
            switch (weapon)
            {
                case "Dagger": return Dagger;
                case "Bat": return Bat;
                case "Axe": return Axe;
                case "SmallPistol": return SmallPistol;
                case "Revolver": return Revolver;
                case "Rifle": return Rifle;
                case "Shotgun": return Shotgun;
                case "Rock": return Rock;
                case "Molotov": return Molotov;
                default: return Martial;
            }
        }
    }

    /// <summary>전투 규칙(순수). BattleManager 가 연출 쪽에서 부르고, 테스트가 헤드리스로 돌린다.</summary>
    public static class BattleEngine
    {
        public const float GaugeMax = 130f;
        public const float GaugeReset = -130f;
        private const float PlayerGaugeSpeed = 2f;
        private const float EnemyGaugeSpeed = 1.9f;

        /// <summary>플레이어가 쓰러졌는가(HP 또는 SAN 이 0 이하). 사망 화면(DirectingManager)과 같은 조건이다.</summary>
        public static bool IsPlayerDown(int hp, int san)
        {
            return hp <= 0 || san <= 0;
        }

        /// <summary>게이지가 가득 찼는가(턴 시작). 옛 SymbolUp 의 `y < 130` 의 반대.</summary>
        public static bool IsGaugeFull(float y)
        {
            return !(y < GaugeMax);
        }

        /// <summary>턴 게이지 한 칸 상승. 옛 SymbolUp 과 같은 float 연산 순서. 가득 찬 뒤 처리(GaugeReset)는 호출 측.</summary>
        public static float StepGauge(float y, float dex, bool isPlayer)
        {
            return y + ((1 + (dex / 100)) * (isPlayer ? PlayerGaugeSpeed : EnemyGaugeSpeed));
        }

        /// <summary>
        /// 적 행동 선택. 손재주 슬롯은 난수 없이 손재주. 그 외에는 def.Intent 가중치 합이 0 이면 난수 없이 일반 공격,
        /// 아니면 Range(0, 합) 한 번으로 이성 -> 독 -> 일반 구간을 고른다(심해인 1:1:2 = 옛 Range(0,4)).
        /// </summary>
        public static EnemyIntent ChooseIntent(EnemyDef def, IRng rng)
        {
            if (def.UsesDeftness)
            {
                return EnemyIntent.Deftness;
            }
            int total = def.Intent.Total;
            if (total <= 0)
            {
                return EnemyIntent.Normal;
            }
            int r = rng.Next(0, total);
            if (r < def.Intent.SanAttack)
            {
                return EnemyIntent.SanAttack;
            }
            if (r < def.Intent.SanAttack + def.Intent.PoisonAttack)
            {
                return EnemyIntent.PoisonAttack;
            }
            return EnemyIntent.Normal;
        }

        /// <summary>
        /// 대립 판정 최종 성공 여부. 옛 BattleRollet.GetFinalResult 의 if 사슬 이식.
        /// 유리(회피·무술·검술·도주): 대성공이면 무조건 성공, 아니면 플레이어 단계 >= 적 단계(동급 승).
        /// 불리(반격): 플레이어 단계 > 적 단계일 때만(대성공 동급 불가).
        /// </summary>
        public static bool ResolveContest(ContestKind kind, SuccessLevel player, SuccessLevel enemy)
        {
            if (kind == ContestKind.CounterAttack)
            {
                if (player == SuccessLevel.CriticalSuccess && enemy != SuccessLevel.CriticalSuccess)
                {
                    return true;
                }
                if (player == SuccessLevel.Success && enemy != SuccessLevel.CriticalSuccess && enemy != SuccessLevel.Success)
                {
                    return true;
                }
                if (player == SuccessLevel.Failure && enemy != SuccessLevel.CriticalSuccess && enemy != SuccessLevel.Success && enemy != SuccessLevel.Failure)
                {
                    return true;
                }
                return false;
            }
            if (player == SuccessLevel.CriticalSuccess)
            {
                return true;
            }
            if (player == SuccessLevel.Success)
            {
                return enemy != SuccessLevel.CriticalSuccess;
            }
            if (player == SuccessLevel.Failure)
            {
                return enemy == SuccessLevel.Failure || enemy == SuccessLevel.Fumble;
            }
            return enemy == SuccessLevel.Fumble;
        }

        /// <summary>룰렛 subject 문자열 -> 대립 판정 종류. 이 5개 외의 subject 는 대립 판정을 건드리지 않는다.</summary>
        public static bool TryParseContest(string subject, out ContestKind kind)
        {
            switch (subject)
            {
                case "evasion": kind = ContestKind.Evasion; return true;
                case "counterattack": kind = ContestKind.CounterAttack; return true;
                case "MA_attack_Counter": kind = ContestKind.MartialArts; return true;
                case "SS_attack_Counter": kind = ContestKind.Swordsmanship; return true;
                case "RunAway": kind = ContestKind.RunAway; return true;
                default: kind = ContestKind.Evasion; return false;
            }
        }

        /// <summary>룰렛 결과창의 라벨(옛 finalResultTxt 접두어).</summary>
        public static string ContestLabel(ContestKind kind)
        {
            switch (kind)
            {
                case ContestKind.Evasion: return "Evasion";
                case ContestKind.CounterAttack: return "CounterAttack";
                case ContestKind.MartialArts: return "MartialArt";
                case ContestKind.Swordsmanship: return "Swordsmanship";
                default: return "Runaway";
            }
        }

        /// <summary>SkillCheck.ToLegacyString 의 역. 알 수 없는 문자열은 Failure(옛 else 분기와 동일).</summary>
        public static SuccessLevel ParseLegacyLevel(string text)
        {
            switch (text)
            {
                case "Result : Critical Success": return SuccessLevel.CriticalSuccess;
                case "Result : Success": return SuccessLevel.Success;
                case "Result : Fumble": return SuccessLevel.Fumble;
                default: return SuccessLevel.Failure;
            }
        }

        /// <summary>
        /// 하수도 입구 조우: 1d100 이 55 미만일 때만 두 번째 굴림(dex 가 1d100 보다 크면 무사통과).
        /// 위장 실패(true)면 호출 측이 Sewer_DisgauseFail 대화를 연다. 옛 InSewerEnemy 그대로.
        /// </summary>
        public static bool SewerDisguiseFails(int dex, IRng rng)
        {
            int first = rng.Next(1, 101);
            if (first < 55)
            {
                int second = rng.Next(1, 101);
                return !(dex > second);
            }
            return false;
        }

        /// <summary>
        /// 하수도 입구 단체 은신(Party.md 3-3): 전원 중 가장 낮은 은신치로 SewerDisguiseFails 를 한 번만 판정한다.
        /// 값이 하나면 SewerDisguiseFails(values[0]) 와 난수 소비·결과가 같다. 값이 없으면 판정하지 않고 통과(false).
        /// </summary>
        public static bool SewerPartyDisguiseFails(IReadOnlyList<int> values, IRng rng)
        {
            if (values == null || values.Count == 0)
            {
                return false;
            }
            int lowest = values[0];
            for (int i = 1; i < values.Count; i++)
            {
                lowest = values[i] < lowest ? values[i] : lowest;
            }
            return SewerDisguiseFails(lowest, rng);
        }
    }
}
