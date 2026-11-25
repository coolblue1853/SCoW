using System;

namespace SCoW.Core.Save
{
    /// <summary>타이틀 "이어하기" 슬롯 요약 한 줄 포맷(UX-10, 순수 C#, EditMode 테스트 대상).</summary>
    public static class SlotSummary
    {
        /// <summary>"Day 3 Noon / Slum / 11-25 14:30" 꼴. 이름은 호출 쪽이 현지화해서 넘긴다. 저장 시각은 로컬 시각.</summary>
        public static string Line(int day, string timeOfDay, string place, DateTime savedLocal)
        {
            return "Day " + day + " " + timeOfDay + " / " + place + " / " + savedLocal.ToString("MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>버전 표기. 비어 있으면 빈 문자열.</summary>
        public static string VersionLabel(string version)
        {
            return string.IsNullOrEmpty(version) ? string.Empty : "v" + version;
        }
    }
}
