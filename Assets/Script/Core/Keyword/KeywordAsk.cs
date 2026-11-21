using SCoW.Core.State;

namespace SCoW.Core.Keyword
{
    /// <summary>키워드를 내밀었을 때의 결과 4종. Correct 만 질문 횟수를 쓴다.</summary>
    public enum KeywordOutcome
    {
        /// <summary>NPC 에게 이 키워드 전용 응답이 있고 처음 묻는다(횟수 1 소모, 물음 기록).</summary>
        Correct,
        /// <summary>전용 응답이 없다(폴백 대사). 횟수 환불.</summary>
        Unrelated,
        /// <summary>이미 물은 키워드다. 대사 없이 안내만, 횟수 불변.</summary>
        AlreadyAsked,
        /// <summary>남은 질문이 없다. 안내만.</summary>
        NoBudget
    }

    /// <summary>
    /// 질문 횟수·오답 환불·이미 물은 키워드(KEYWORD-1 K3, 순수 C#). 물음 기록은 GameState 플래그 "kwAsked:NPC/키워드" 로 얹는다
    /// (새 키만 추가, 구세이브는 키가 없으면 "안 물었음"). 남은 횟수 자체는 호출측(Intel_ObtoUI.keywordCounter)이 갖는다.
    /// </summary>
    public static class KeywordAsk
    {
        public const string AskedPrefix = "kwAsked:";

        public static string AskedKey(string npc, string id)
        {
            return AskedPrefix + npc + "/" + id;
        }

        public static bool IsAsked(GameState state, string npc, string id)
        {
            return id != null && state.GetFlag(AskedKey(npc, id));
        }

        /// <summary>제시 전에 결과를 미리 판단한다(선택 창 안내용). 횟수 0 이 최우선.</summary>
        public static KeywordOutcome Decide(KeywordReaction reaction, bool asked, int remaining)
        {
            if (remaining <= 0) { return KeywordOutcome.NoBudget; }
            if (asked) { return KeywordOutcome.AlreadyAsked; }
            return reaction == KeywordReaction.Specific ? KeywordOutcome.Correct : KeywordOutcome.Unrelated;
        }

        /// <summary>
        /// 대사를 낸 뒤 정산한다. 폴백(usedFallback)이면 횟수 불변·기록 없음(환불), 전용 응답이면 1 소모하고 물음을 기록한다.
        /// 반환값은 새 남은 횟수(0 밑으로 내려가지 않는다).
        /// </summary>
        public static int Settle(GameState state, string npc, string id, bool usedFallback, int remaining)
        {
            if (usedFallback) { return remaining; }
            if (id != null) { state.SetFlag(AskedKey(npc, id), true); }
            return remaining > 0 ? remaining - 1 : 0;
        }
    }
}
