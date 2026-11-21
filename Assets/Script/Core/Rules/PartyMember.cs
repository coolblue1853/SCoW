using System.Collections.Generic;

namespace SCoW.Core.Rules
{
    /// <summary>아군 한 명(파비안 또는 동료)의 전투 중 가변 상태(Party.md 4-7절). 전투 종료 때만 세이브 키로 환원한다(PT-7).</summary>
    public sealed class PartyMember : IBattleUnit
    {
        public const int DownedCountdownStart = 3;
        public const int PoisonStacks = 3;

        public readonly string Id;
        public readonly string DisplayName;
        public readonly bool IsLeader;
        public readonly int Dex;
        public readonly int Intl;
        public readonly int MaxHp;
        public readonly int MaxSan;
        public readonly IReadOnlyDictionary<string, int> Skills;
        public readonly IReadOnlyList<string> Weapons;

        public int Hp;
        public int San;
        public UnitStatus Status;
        public int DownedCountdown;
        public BoutKind Bout;
        public int BoutTurns;
        /// <summary>방어 태세. 자기 다음 차례 시작에 풀린다. 회피 보너스 다이스 +1.</summary>
        public bool Guarding;
        /// <summary>독 남은 틱 수. 자기 차례 시작마다 1d2x5 피해.</summary>
        public int Poison;
        /// <summary>정산(Settle) 결과 영구 사망.</summary>
        public bool Dead;

        public PartyMember(string id, string displayName, bool isLeader, int dex, int intl, int maxHp, int hp, int maxSan, int san,
            UnitStatus status, IReadOnlyDictionary<string, int> skills, IReadOnlyList<string> weapons)
        {
            Id = id;
            DisplayName = displayName;
            IsLeader = isLeader;
            Dex = dex;
            Intl = intl;
            MaxHp = maxHp;
            Hp = hp;
            MaxSan = maxSan;
            San = san;
            Status = status;
            Skills = skills;
            Weapons = weapons;
            Gauge = BattleEngine.GaugeReset;
        }

        public static PartyMember FromCompanion(CompanionDef def, int hp, int san, UnitStatus status = UnitStatus.Ready)
        {
            return new PartyMember(def.Id, def.DisplayName, false, def.Dex, def.Intl, def.MaxHp, hp, def.MaxSan, san, status, def.Skills, def.Weapons);
        }

        public static PartyMember FromCompanion(CompanionDef def)
        {
            return FromCompanion(def, def.MaxHp, def.MaxSan);
        }

        /// <summary>파비안. 능력치는 호출 측(전역 DataBaseManager 어댑터)이 넣는다.</summary>
        public static PartyMember CreateLeader(string id, int dex, int intl, int maxHp, int hp, int maxSan, int san,
            IReadOnlyDictionary<string, int> skills, IReadOnlyList<string> weapons)
        {
            return new PartyMember(id, id, true, dex, intl, maxHp, hp, maxSan, san, UnitStatus.Ready, skills, weapons);
        }

        string IBattleUnit.Id => Id;
        public Side Side => Side.Ally;
        int IBattleUnit.Dex => Dex;
        int IBattleUnit.Hp => Hp;
        UnitStatus IBattleUnit.Status => Status;
        public bool IsActing => Status == UnitStatus.Ready;
        public bool HasTurns => Status == UnitStatus.Ready || Status == UnitStatus.Downed;
        public float Gauge { get; set; }
        public int Rank { get; set; }

        public int GetSkill(string counterKey)
        {
            int v;
            return Skills.TryGetValue(counterKey, out v) ? v : 0;
        }
    }
}
