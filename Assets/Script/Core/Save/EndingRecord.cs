using System;
using System.Collections.Generic;

namespace SCoW.Core.Save
{
    /// <summary>엔딩 한 종류의 표시 정보(UX-12). Id 는 저장 키라 바꾸지 않는다(추가만).</summary>
    public sealed class EndingInfo
    {
        public readonly string Id;
        public readonly string TitleKo;
        public readonly string TitleEn;

        public EndingInfo(string id, string ko, string en)
        {
            Id = id;
            TitleKo = ko;
            TitleEn = en;
        }
    }

    /// <summary>엔딩 목록과 "어떤 대사가 시작되면 어느 엔딩인가" 표(Story.md 7절 기준).</summary>
    public static class EndingCatalog
    {
        public const string Symbol = "end_symbol";
        public const string Blood = "end_blood";
        public const string Ladder = "end_ladder";
        public const string Nightmare = "end_nightmare";
        public const string Arrest = "end_arrest";

        public static readonly EndingInfo[] All =
        {
            new EndingInfo(Symbol, "A. 문양 파괴", "A. The Symbol Broken"),
            new EndingInfo(Blood, "B. 피의 엔딩", "B. Blood Ending"),
            new EndingInfo(Ladder, "C. 사다리 엔딩", "C. Ladder Ending"),
            new EndingInfo(Nightmare, "D. 악몽 엔딩", "D. Nightmare Ending"),
            new EndingInfo(Arrest, "E. 체포", "E. Arrest"),
        };

        /// <summary>대사 스크립트·토픽이 엔딩 장면의 시작이면 엔딩 Id, 아니면 null. StoryDatabase 가 대사를 풀 때 부른다.</summary>
        public static string FromTopic(string scriptId, string topicId)
        {
            if (scriptId == "InSewer" && topicId == "Ending_BreakSymbol")
            {
                return Symbol;
            }
            if (scriptId == "InSewer" && topicId == "Ending_Killing")
            {
                return Arrest;
            }
            if (scriptId == "Keyword" && topicId == "Symbol/Advent")
            {
                return Blood;
            }
            if (scriptId == "1st_DetectiveOffice" && topicId == "FourthNightmare")
            {
                return Nightmare;
            }
            return null;
        }

        /// <summary>판정 결과 대사 키가 엔딩 장면이면 엔딩 Id, 아니면 null.</summary>
        public static string FromJudge(string key)
        {
            return key == "SanCheck/EndingLedderSan" ? Ladder : null;
        }

        public static int IndexOf(string id)
        {
            for (int i = 0; i < All.Length; i++)
            {
                if (All[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    /// <summary>
    /// 본 엔딩 기록(세이브 슬롯과 독립인 전역 기록). endings.es3 의 "endingsSeen" 키에 저장된다(ES3 가 공개 필드를 직렬화).
    /// 필드는 추가만 한다.
    /// </summary>
    [Serializable]
    public sealed class EndingRecord
    {
        public List<string> Seen = new List<string>();

        /// <summary>처음 본 엔딩이면 true. 목록에 없는 Id 는 무시한다.</summary>
        public bool Mark(string id)
        {
            if (EndingCatalog.IndexOf(id) < 0 || IsSeen(id))
            {
                return false;
            }
            Seen.Add(id);
            return true;
        }

        public bool IsSeen(string id)
        {
            return Seen != null && Seen.Contains(id);
        }

        /// <summary>손상된 파일(null 목록·빈 값·중복)을 정리한다. 모르는 Id 는 새 버전 기록일 수 있어 남긴다.</summary>
        public void Sanitize()
        {
            List<string> clean = new List<string>();
            if (Seen != null)
            {
                for (int i = 0; i < Seen.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Seen[i]) && !clean.Contains(Seen[i]))
                    {
                        clean.Add(Seen[i]);
                    }
                }
            }
            Seen = clean;
        }
    }
}
