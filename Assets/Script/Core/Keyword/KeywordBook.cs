using System.Collections.Generic;
using SCoW.Core.State;
using SCoW.Core.Story;

namespace SCoW.Core.Keyword
{
    /// <summary>키워드를 NPC 에게 내밀었을 때 응답 종류. Keyword.json 의 "NPC/id" 토픽이 있으면 Specific, 없고 "NPC/*" 가 있으면 Fallback.</summary>
    public enum KeywordReaction
    {
        /// <summary>NPC 에게 이 키워드 전용 토픽이 있다.</summary>
        Specific,
        /// <summary>전용 토픽이 없어 "NPC/*" 폴백 대사로 간다.</summary>
        Fallback,
        /// <summary>폴백도 없다(옛 코드처럼 아무것도 안 띄움).</summary>
        None
    }

    /// <summary>
    /// 키워드 획득 여부·분류·NPC 별 제시 가능 목록(순수 C#, 상태를 갖지 않는다). 옛 KeyWordManager 3단 선택이 내던 목록과 같은 결과를 낸다.
    /// 목록 순서는 카탈로그(옛 코드의 검사 순서)이다. 옛 코드는 플래그가 켜진 순서대로 Add 했으므로 실제 플레이에서는 획득 순서였다.
    /// 획득 순서 표시가 필요하면 K4 의 "본 키워드" 집합으로 얹는다.
    /// </summary>
    public sealed class KeywordBook
    {
        private readonly KeywordCatalog _catalog;
        private readonly List<KeywordUpperDef> _uppers = new List<KeywordUpperDef>();
        private readonly List<KeywordEntry> _entries = new List<KeywordEntry>();

        public KeywordBook(KeywordCatalog catalog)
        {
            _catalog = catalog;
            _uppers.AddRange(catalog.Uppers);
            _uppers.Sort(CompareUpper);
            _entries.AddRange(catalog.Entries);
            _entries.Sort(CompareEntry);
        }

        public KeywordCatalog Catalog { get { return _catalog; } }

        private static int CompareUpper(KeywordUpperDef a, KeywordUpperDef b) { return a.Order.CompareTo(b.Order); }

        private static int CompareEntry(KeywordEntry a, KeywordEntry b) { return a.Order.CompareTo(b.Order); }

        /// <summary>하위 키워드가 풀렸는지. 전용 플래그 하나로 정해진다.</summary>
        public bool IsUnlocked(KeywordEntry entry, GameState state)
        {
            return state.GetFlag(entry.UnlockFlag);
        }

        /// <summary>상위 키워드가 풀렸는지. 플래그 중 하나라도 켜지면 true(옛 KeywordUpdate 와 같음).</summary>
        public bool IsUnlocked(KeywordUpperDef upper, GameState state)
        {
            for (int i = 0; i < upper.UnlockFlags.Length; i++)
            {
                if (state.GetFlag(upper.UnlockFlags[i])) { return true; }
            }
            return false;
        }

        /// <summary>한 분류의 풀린 상위 키워드(옛 upperEventKeywordList 등).</summary>
        public List<KeywordUpperDef> GetUppers(KeywordCategory category, GameState state)
        {
            List<KeywordUpperDef> list = new List<KeywordUpperDef>();
            for (int i = 0; i < _uppers.Count; i++)
            {
                if (_uppers[i].Category == category && IsUnlocked(_uppers[i], state)) { list.Add(_uppers[i]); }
            }
            return list;
        }

        /// <summary>한 상위 키워드 아래 풀린 하위 키워드(옛 planetaryParadeList 등).</summary>
        public List<KeywordEntry> GetEntries(string upperId, GameState state)
        {
            List<KeywordEntry> list = new List<KeywordEntry>();
            for (int i = 0; i < _entries.Count; i++)
            {
                if (_entries[i].Upper == upperId && IsUnlocked(_entries[i], state)) { list.Add(_entries[i]); }
            }
            return list;
        }

        /// <summary>풀린 하위 키워드 전부(카탈로그 순서).</summary>
        public List<KeywordEntry> GetUnlocked(GameState state)
        {
            List<KeywordEntry> list = new List<KeywordEntry>();
            for (int i = 0; i < _entries.Count; i++)
            {
                if (IsUnlocked(_entries[i], state)) { list.Add(_entries[i]); }
            }
            return list;
        }

        /// <summary>한 분류의 풀린 하위 키워드.</summary>
        public List<KeywordEntry> GetUnlocked(KeywordCategory category, GameState state)
        {
            List<KeywordEntry> list = new List<KeywordEntry>();
            for (int i = 0; i < _entries.Count; i++)
            {
                if (_entries[i].Category == category && IsUnlocked(_entries[i], state)) { list.Add(_entries[i]); }
            }
            return list;
        }

        /// <summary>id 와 같은 하위 키워드(상위가 달라도 같은 id 가 있을 수 있어 목록).</summary>
        public List<KeywordEntry> FindById(string id)
        {
            List<KeywordEntry> list = new List<KeywordEntry>();
            for (int i = 0; i < _entries.Count; i++)
            {
                if (_entries[i].Id == id) { list.Add(_entries[i]); }
            }
            return list;
        }

        /// <summary>NPC 에게 이 키워드를 내밀었을 때의 응답 종류. StoryDatabase.TryRunKeyword 의 토픽 선택과 같은 규칙.</summary>
        public static KeywordReaction Reaction(StoryScript keywordScript, string npc, string id)
        {
            if (keywordScript == null) { return KeywordReaction.None; }
            if (id != null && keywordScript.HasTopic(npc + "/" + id)) { return KeywordReaction.Specific; }
            return keywordScript.HasTopic(npc + "/*") ? KeywordReaction.Fallback : KeywordReaction.None;
        }

        /// <summary>이 NPC 에게 전용 반응이 있는, 풀린 키워드(같은 id 는 한 번만).</summary>
        public List<KeywordEntry> GetPresentable(GameState state, StoryScript keywordScript, string npc)
        {
            List<KeywordEntry> list = new List<KeywordEntry>();
            HashSet<string> seen = new HashSet<string>();
            for (int i = 0; i < _entries.Count; i++)
            {
                KeywordEntry e = _entries[i];
                if (!IsUnlocked(e, state) || seen.Contains(e.Id)) { continue; }
                if (Reaction(keywordScript, npc, e.Id) == KeywordReaction.Specific)
                {
                    seen.Add(e.Id);
                    list.Add(e);
                }
            }
            return list;
        }
    }
}
