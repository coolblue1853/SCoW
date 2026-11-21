using NUnit.Framework;
using SCoW.Core.Keyword;
using SCoW.Core.State;
using SCoW.Core.Story;
using UnityEngine;

namespace SCoW.Tests
{
    /// <summary>KEYWORD-1 K3: 질문 횟수 소모·오답 환불·이미 물은 키워드 기록.</summary>
    public sealed class KeywordAskTests
    {
        private const string Npc = "Ella Triss";

        [Test]
        public void Decide_ByReactionAskedAndBudget()
        {
            Assert.AreEqual(KeywordOutcome.Correct, KeywordAsk.Decide(KeywordReaction.Specific, false, 3));
            Assert.AreEqual(KeywordOutcome.Unrelated, KeywordAsk.Decide(KeywordReaction.Fallback, false, 3));
            Assert.AreEqual(KeywordOutcome.Unrelated, KeywordAsk.Decide(KeywordReaction.None, false, 1));
            Assert.AreEqual(KeywordOutcome.AlreadyAsked, KeywordAsk.Decide(KeywordReaction.Specific, true, 3));
        }

        [Test]
        public void Decide_ZeroBudgetWinsOverEverything()
        {
            Assert.AreEqual(KeywordOutcome.NoBudget, KeywordAsk.Decide(KeywordReaction.Specific, false, 0));
            Assert.AreEqual(KeywordOutcome.NoBudget, KeywordAsk.Decide(KeywordReaction.Fallback, false, 0));
            Assert.AreEqual(KeywordOutcome.NoBudget, KeywordAsk.Decide(KeywordReaction.Specific, true, 0));
        }

        [Test]
        public void Settle_SpecificSpendsOneAndRecords()
        {
            GameState s = GameState.New();
            int left = KeywordAsk.Settle(s, Npc, "Rumor", false, 3);
            Assert.AreEqual(2, left);
            Assert.IsTrue(KeywordAsk.IsAsked(s, Npc, "Rumor"));
            Assert.IsFalse(KeywordAsk.IsAsked(s, "Aiden Triss", "Rumor"), "NPC 마다 따로 기록");
        }

        [Test]
        public void Settle_FallbackRefundsAndRecordsNothing()
        {
            GameState s = GameState.New();
            int left = KeywordAsk.Settle(s, Npc, "Nonsense", true, 3);
            Assert.AreEqual(3, left);
            Assert.IsFalse(KeywordAsk.IsAsked(s, Npc, "Nonsense"));
        }

        [Test]
        public void Settle_NeverGoesBelowZero()
        {
            Assert.AreEqual(0, KeywordAsk.Settle(GameState.New(), Npc, "Rumor", false, 0));
        }

        [Test]
        public void OldSave_WithoutKeys_ReadsAsNotAsked()
        {
            GameState s = GameState.New();
            Assert.IsFalse(KeywordAsk.IsAsked(s, Npc, "Rumor"));
            Assert.IsFalse(KeywordAsk.IsAsked(s, Npc, null));
        }

        [Test]
        public void AskedRecord_SurvivesClone()
        {
            GameState s = GameState.New();
            KeywordAsk.Settle(s, Npc, "Rumor", false, 3);
            GameState c = s.Clone();
            Assert.IsTrue(KeywordAsk.IsAsked(c, Npc, "Rumor"));
            Assert.IsTrue(s.Equals(c));
        }

        [Test]
        public void RealKeywordJson_SpecificVsFallback()
        {
            TextAsset ta = Resources.Load<TextAsset>("Story/Keyword");
            Assert.IsNotNull(ta, "Story/Keyword.json 없음");
            StoryLoadResult r = StoryScriptLoader.Load(ta.text);
            Assert.AreEqual(KeywordReaction.Specific, KeywordBook.Reaction(r.Script, Npc, "Rumor"));
            Assert.AreEqual(KeywordReaction.Fallback, KeywordBook.Reaction(r.Script, Npc, "NoSuchKeyword"));
        }
    }
}
