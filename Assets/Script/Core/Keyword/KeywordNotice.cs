using System.Collections.Generic;
using System.Linq;
using SCoW.Core.State;

namespace SCoW.Core.Keyword
{
    /// <summary>
    /// 새 키워드 알림·획득 순서(KEYWORD-1 K4, 순수 C#). GameState 에 새 키만 얹는다(구세이브 호환, 기존 키 불변):
    /// 카운터 "kwSeq"(마지막 획득 순번)·"kwOrder:id"(그 키워드의 획득 순번), 플래그 "kwSeen:id"(선택 창에서 확인함)·"kwNoticeBase"(기준선 기록됨),
    /// 카운터 "kwBadge"(수첩 배지를 확인한 순번). 키워드는 id 로 구분한다(상위가 달라도 같은 id 는 하나).
    /// 기준선: 처음 동기화할 때 이미 풀려 있는 키워드는 알림 없이 확인한 것으로 친다(구세이브가 알림 폭주하지 않게).
    /// </summary>
    public static class KeywordNotice
    {
        public const string SeqKey = "kwSeq";
        public const string BadgeKey = "kwBadge";
        public const string BaselineKey = "kwNoticeBase";
        public const string OrderPrefix = "kwOrder:";
        public const string SeenPrefix = "kwSeen:";

        /// <summary>토스트를 지금 띄워도 되나(UI 겹침 방지). 이미 표시 중·큐 비어 있음·연출 중·전체 화면 패널(설정·수첩 등)이 열려 있으면 미룬다.</summary>
        public static bool CanShowToast(bool showing, int queued, bool busy, bool panelOpen)
        {
            return !showing && queued > 0 && !busy && !panelOpen;
        }

        public static string OrderKey(string id)
        {
            return OrderPrefix + id;
        }

        public static string SeenKey(string id)
        {
            return SeenPrefix + id;
        }

        /// <summary>이 키가 알림 자체가 쓰는 키인가(상태 변경 알림을 다시 처리하지 않게 거른다).</summary>
        public static bool IsNoticeKey(string key)
        {
            return key != null && (key == SeqKey || key == BadgeKey || key == BaselineKey
                || key.StartsWith(OrderPrefix, System.StringComparison.Ordinal) || key.StartsWith(SeenPrefix, System.StringComparison.Ordinal));
        }

        /// <summary>
        /// 풀린 키워드에 획득 순번을 붙이고, 이번에 새로 풀린 것(id 중복 제거, 카탈로그 순서)을 돌려준다. 기준선이 없으면 알림 없이 확인 처리하고 빈 목록.
        /// </summary>
        public static List<KeywordEntry> Sync(KeywordBook book, GameState state)
        {
            List<KeywordEntry> fresh = new List<KeywordEntry>();
            bool baseline = !state.GetFlag(BaselineKey);
            HashSet<string> ids = new HashSet<string>();
            List<KeywordEntry> unlocked = book.GetUnlocked(state);
            for (int i = 0; i < unlocked.Count; i++)
            {
                KeywordEntry e = unlocked[i];
                if (!ids.Add(e.Id) || state.GetCounter(OrderKey(e.Id)) > 0)
                {
                    continue;
                }
                int seq = state.GetCounter(SeqKey) + 1;
                state.SetCounter(SeqKey, seq);
                state.SetCounter(OrderKey(e.Id), seq);
                if (baseline)
                {
                    state.SetFlag(SeenKey(e.Id), true);
                }
                else
                {
                    fresh.Add(e);
                }
            }
            if (baseline)
            {
                state.SetFlag(BaselineKey, true);
                state.SetCounter(BadgeKey, state.GetCounter(SeqKey));
            }
            return fresh;
        }

        /// <summary>선택 창 목록에 NEW 를 붙일지(획득했고 아직 확인하지 않음).</summary>
        public static bool IsNew(GameState state, string id)
        {
            return state.GetCounter(OrderKey(id)) > 0 && !state.GetFlag(SeenKey(id));
        }

        /// <summary>선택 창을 열었다: 풀린 키워드를 전부 확인한 것으로 하고 배지도 끈다.</summary>
        public static void MarkAllSeen(KeywordBook book, GameState state)
        {
            List<KeywordEntry> unlocked = book.GetUnlocked(state);
            for (int i = 0; i < unlocked.Count; i++)
            {
                state.SetFlag(SeenKey(unlocked[i].Id), true);
            }
            AckBadge(state);
        }

        /// <summary>수첩을 열었다: 배지만 끈다(선택 창 NEW 는 남는다).</summary>
        public static void AckBadge(GameState state)
        {
            state.SetCounter(BadgeKey, state.GetCounter(SeqKey));
        }

        /// <summary>수첩 배지에 표시할 새 키워드 수(0 이면 배지 없음).</summary>
        public static int BadgeCount(GameState state)
        {
            int n = state.GetCounter(SeqKey) - state.GetCounter(BadgeKey);
            return n > 0 ? n : 0;
        }

        /// <summary>획득 순서대로 정렬한 새 목록(순번 없는 것은 뒤, 같은 순번·없음끼리는 원래 순서).</summary>
        public static List<KeywordEntry> SortByAcquisition(List<KeywordEntry> list, GameState state)
        {
            return list.OrderBy(e =>
            {
                int o = state.GetCounter(OrderKey(e.Id));
                return o > 0 ? o : int.MaxValue;
            }).ToList();
        }
    }
}
