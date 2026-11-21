using System.Collections.Generic;
using SCoW.Core.State;

namespace SCoW.Core.Rules
{
    /// <summary>동료·파비안 공통 상태(Party.md 4-7절). 정수 값이 세이브에 저장되므로 순서를 바꾸지 않는다.</summary>
    public enum UnitStatus { Ready = 0, Downed = 1, Stable = 2, Fallen = 3, Broken = 4, Recovering = 5 }

    /// <summary>동료 한 명의 고정 정의(Party.md 2절). 난수 없음. 기능 키는 CounterKeys 의 기능 상수를 쓴다.</summary>
    public sealed class CompanionDef
    {
        public readonly string Id;
        public readonly string DisplayName;
        public readonly int Str;
        public readonly int Intl;
        public readonly int Dex;
        public readonly int Mp;
        public readonly int MaxHp;
        public readonly int MaxSan;
        public readonly int Luk;
        public readonly IReadOnlyDictionary<string, int> Skills;
        public readonly HashSet<string> Specialties;
        public readonly IReadOnlyList<string> Weapons;
        public readonly HashSet<string> Tags;

        public CompanionDef(string id, string displayName, int str, int intl, int dex, int mp, int maxHp, int maxSan, int luk,
            IReadOnlyDictionary<string, int> skills, string[] specialties, string[] weapons, string[] tags)
        {
            Id = id;
            DisplayName = displayName;
            Str = str;
            Intl = intl;
            Dex = dex;
            Mp = mp;
            MaxHp = maxHp;
            MaxSan = maxSan;
            Luk = luk;
            Skills = skills;
            Specialties = new HashSet<string>(specialties);
            Weapons = weapons;
            Tags = new HashSet<string>(tags);
        }

        public int GetSkill(string counterKey)
        {
            int v;
            return Skills.TryGetValue(counterKey, out v) ? v : 0;
        }

        /// <summary>전문 분야(도움·대신 판정 대상, Party.md 3절).</summary>
        public bool IsSpecialty(string counterKey)
        {
            return Specialties.Contains(counterKey);
        }

        public bool HasTag(string tag)
        {
            return Tags.Contains(tag);
        }
    }

    /// <summary>알버트·메이브 고정 능력치(Party.md 2절). 한 번 낸 값은 밸런스 조정 외에 바꾸지 않는다.</summary>
    public static class CompanionDefs
    {
        public const string LeaderId = "fabian";
        public const string AlbertId = "albert";
        public const string MaeveId = "maeve";

        /// <summary>기능 15종 키(기본값 표와 같은 순서). Skills 인자 순서도 이 순서다.</summary>
        public static readonly string[] SkillKeys =
        {
            CounterKeys.martialArtsPoint, CounterKeys.gunShotPoint, CounterKeys.swordPoint, CounterKeys.observationPoint,
            CounterKeys.swimingPoint, CounterKeys.medicinePoint, CounterKeys.analysisPoint, CounterKeys.listeningPoint,
            CounterKeys.psychotherapyPoint, CounterKeys.occultPoint, CounterKeys.evasionPoint, CounterKeys.deftnessPoint,
            CounterKeys.rhetoricPoint, CounterKeys.stealthPoint, CounterKeys.disguisePoint,
        };

        public static readonly CompanionDef Albert = new CompanionDef(
            AlbertId, "Albert", 65, 55, 55, 45, 75, 55, 40,
            Skills(55, 60, 20, 45, 25, 25, 35, 40, 10, 5, 35, 25, 50, 15, 10),
            new[] { CounterKeys.martialArtsPoint, CounterKeys.gunShotPoint, CounterKeys.observationPoint, CounterKeys.rhetoricPoint },
            new[] { "Revolver" },
            new[] { "Authority" });

        public static readonly CompanionDef Maeve = new CompanionDef(
            MaeveId, "Maeve", 45, 65, 60, 55, 55, 65, 55,
            Skills(30, 45, 40, 40, 10, 15, 50, 55, 45, 20, 45, 50, 70, 40, 35),
            new[]
            {
                CounterKeys.analysisPoint, CounterKeys.listeningPoint, CounterKeys.psychotherapyPoint, CounterKeys.evasionPoint,
                CounterKeys.deftnessPoint, CounterKeys.rhetoricPoint, CounterKeys.stealthPoint,
            },
            new[] { "SmallPistol", "Dagger" },
            new[] { "Smuggler", "Underworld" });

        /// <summary>합류 순서(알버트, 메이브). 대열과는 별개.</summary>
        public static readonly CompanionDef[] All = { Albert, Maeve };

        public static CompanionDef Get(string id)
        {
            for (int i = 0; i < All.Length; i++)
            {
                if (All[i].Id == id)
                {
                    return All[i];
                }
            }
            return null;
        }

        private static Dictionary<string, int> Skills(params int[] values)
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            for (int i = 0; i < SkillKeys.Length; i++)
            {
                d[SkillKeys[i]] = values[i];
            }
            return d;
        }
    }
}
