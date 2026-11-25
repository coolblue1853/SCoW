using System.Collections.Generic;

namespace SCoW.Core.Keyword
{
    /// <summary>
    /// KEYWORD-1 K6. 수첩 상세 패널 오브젝트 이름(`Ella_Detail`)을 키워드 카탈로그의 상위 id 로 옮기는 표.
    /// 수첩은 씬에 손으로 만든 패널이라 이름이 카탈로그와 다른 것이 있어 표로 잇는다. 순수 C#(엔진 무관).
    /// </summary>
    public static class IntelUpperMap
    {
        private const string DetailSuffix = "_Detail";

        private static readonly Dictionary<string, string> Map = new Dictionary<string, string>
        {
            // 사건
            { "PlanetarySequence", "PlanetaryParade" },
            { "Nightmare", "Nightmare" },
            { "Insomnia", "Insomnia" },
            { "Paranoia", "Paranoia" },
            { "MissingPeople", "MissingPeople" },
            { "SewerGhostStory", "SewerGhostStory" },
            { "FishySmell", "FishySmell" },
            { "RiverWater", "RiverWaterQuality" },
            { "CreepyEyes", "CreepyEyes" },
            { "MurderCase", "MurderCase" },
            { "Safe", "Safe" },
            // 장소(Gunsmith·PoliceOffice·Slum·Bar·Station·DetectiveOffice 는 키워드 없음)
            { "Hospital", "Hospital" },
            { "QuestHouse", "Client'shouse" },
            { "Newspaper", "DailyNews" },
            { "Riverside", "Riverside" },
            { "SewageMaintenanceOffice", "Sewage MA Office" },
            { "Sewer", "Sewer" },
            { "Wharf", "Wharf" },
            { "University", "Univ of Miscatonic" },
            // 인물
            { "Aiden", "Aiden Triss" },
            { "Ella", "Ella Triss" },
            { "Swain", "Berkeley Swain" },
            { "Cane", "Osborn Louis Kane" },
            { "Kane", "Osborn Louis Kane" }, // 씬에 Kane_Detail 이름의 패널도 있다
            { "Meiv", "Maeve A Rossi" },
            { "Albert", "Albert Bradley" },
            { "SewerWorker", "SewerWorker" },
            { "Dave", "Dave" },
        };

        /// <summary>수첩 상세 패널 이름 → 상위 키워드 id. 키워드가 없는 패널이면 false.</summary>
        public static bool TryGetUpper(string detailObjectName, out string upperId)
        {
            upperId = null;
            if (string.IsNullOrEmpty(detailObjectName) || !detailObjectName.EndsWith(DetailSuffix))
            {
                return false;
            }
            string key = detailObjectName.Substring(0, detailObjectName.Length - DetailSuffix.Length);
            return Map.TryGetValue(key, out upperId);
        }

        /// <summary>표에 있는 모든 상위 id(검증용).</summary>
        public static IEnumerable<string> AllUpperIds()
        {
            return Map.Values;
        }
    }
}
