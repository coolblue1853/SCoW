using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using SCoW.Core.Keyword;
using SCoW.Core.State;
using SCoW.Core.Story;
using UnityEngine;

namespace SCoW.Tests
{
    /// <summary>
    /// KEYWORD-1 K1. 옛 KeyWordManager(3단 선택)가 플래그 프리셋마다 낸 상위·하위 목록 골든(Golden/KeywordBook.golden.txt,
    /// 도구: SCoW/Keyword/Dump Golden)을 KeywordBook(Resources/Story/KeywordCatalog.json)이 같게 내는지 대조하고, 카탈로그 정합을 확인한다.
    /// </summary>
    public sealed class KeywordBookTests
    {
        private const string GoldenPath = "Assets/Tests/EditMode/Golden/KeywordBook.golden.txt";

        private static KeywordCatalog LoadCatalog()
        {
            TextAsset ta = Resources.Load<TextAsset>("Story/KeywordCatalog");
            Assert.IsNotNull(ta, "Story/KeywordCatalog.json 없음");
            KeywordCatalogLoadResult r = KeywordCatalog.Load(ta.text);
            Assert.IsTrue(r.Ok, string.Join(" | ", r.Errors));
            return r.Catalog;
        }

        private static string Join(IEnumerable<string> items)
        {
            return string.Join(",", items);
        }

        /// <summary>골든 한 줄과 같은 형식으로 KeywordBook 결과를 만든다(U.Event|U.Character|U.Place|D.상위 ...).</summary>
        private static string Describe(KeywordBook book, GameState state)
        {
            List<string> parts = new List<string>();
            KeywordCategory[] cats = { KeywordCategory.Event, KeywordCategory.Character, KeywordCategory.Place };
            for (int c = 0; c < cats.Length; c++)
            {
                List<string> ids = new List<string>();
                foreach (KeywordUpperDef u in book.GetUppers(cats[c], state)) { ids.Add(u.Id); }
                parts.Add("U." + cats[c] + "=" + Join(ids));
            }
            List<KeywordUpperDef> uppers = new List<KeywordUpperDef>(book.Catalog.Uppers);
            uppers.Sort((a, b) => a.Order.CompareTo(b.Order));
            foreach (KeywordUpperDef u in uppers)
            {
                List<string> ids = new List<string>();
                foreach (KeywordEntry e in book.GetEntries(u.Id, state)) { ids.Add(e.Id); }
                parts.Add("D." + u.Id + "=" + Join(ids));
            }
            return string.Join("|", parts);
        }

        [Test]
        public void Golden_OldManagerLists_MatchKeywordBook()
        {
            KeywordBook book = new KeywordBook(LoadCatalog());
            string[] lines = File.ReadAllText(GoldenPath).Split('\n');
            int checkedRows = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].TrimEnd('\r');
                if (line.Length == 0) { continue; }
                int a = line.IndexOf('|');
                int b = line.IndexOf('|', a + 1);
                string name = line.Substring(0, a);
                string spec = line.Substring(a + 1, b - a - 1);
                string expected = line.Substring(b + 1);
                string actual = Describe(book, StatePreset.Build(spec));
                Assert.AreEqual(expected, actual, "프리셋 " + name);
                checkedRows++;
            }
            Assert.AreEqual(352, checkedRows, "골든 행 수");
        }

        [Test]
        public void Golden_CoversEveryEntryAndUpper()
        {
            KeywordCatalog cat = LoadCatalog();
            string all = null;
            foreach (string raw in File.ReadAllText(GoldenPath).Split('\n'))
            {
                if (raw.StartsWith("all|", System.StringComparison.Ordinal)) { all = raw; }
            }
            Assert.IsNotNull(all, "전부 프리셋 행");
            foreach (KeywordUpperDef u in cat.Uppers) { Assert.IsTrue(all.Contains("D." + u.Id + "="), u.Id); }
            KeywordBook book = new KeywordBook(cat);
            GameState everything = StatePreset.Build(all.Split('|')[1]);
            Assert.AreEqual(cat.Entries.Count, book.GetUnlocked(everything).Count, "전부 켜면 모든 하위가 풀림");
        }

        [Test]
        public void Catalog_Counts_AndNewGame()
        {
            KeywordCatalog cat = LoadCatalog();
            Assert.AreEqual(27, cat.Uppers.Count);
            KeywordBook book = new KeywordBook(cat);
            GameState fresh = GameState.New();
            Assert.AreEqual(0, book.GetUnlocked(fresh).Count, "새 게임에는 풀린 키워드 없음");
            Assert.AreEqual(0, book.GetUppers(KeywordCategory.Event, fresh).Count);
        }

        [Test]
        public void Catalog_SingleFlag_UnlocksOneEntryAndItsUpper()
        {
            KeywordBook book = new KeywordBook(LoadCatalog());
            GameState s = GameState.New();
            s.SetFlag(FlagKeys.intelPlanetaryParade2, true);
            List<KeywordEntry> got = book.GetUnlocked(s);
            Assert.AreEqual(1, got.Count);
            Assert.AreEqual("Meetings", got[0].Id);
            Assert.AreEqual(KeywordCategory.Event, got[0].Category);
            Assert.AreEqual("PlanetaryParade", got[0].Upper);
            Assert.AreEqual(1, book.GetUppers(KeywordCategory.Event, s).Count);
            Assert.AreEqual(0, book.GetUppers(KeywordCategory.Place, s).Count);
        }

        [Test]
        public void Catalog_UpperWithoutEntry_StillListedByFlagFive()
        {
            // intelPlanetaryParade5 는 하위가 없지만 옛 KeywordUpdate 는 1~6 중 하나만 켜져도 상위를 추가했다.
            KeywordBook book = new KeywordBook(LoadCatalog());
            GameState s = GameState.New();
            s.SetFlag(FlagKeys.intelPlanetaryParade5, true);
            Assert.AreEqual(1, book.GetUppers(KeywordCategory.Event, s).Count);
            Assert.AreEqual(0, book.GetUnlocked(s).Count);
        }

        [Test]
        public void Catalog_RecordsLegacyMismatchAsSuspect_NotFixed()
        {
            KeywordCatalog cat = LoadCatalog();
            bool kane = false;
            bool river = false;
            foreach (KeywordEntry e in cat.Entries)
            {
                if (e.Upper == "Osborn Louis Kane" && e.Suspect.Length > 0) { kane = true; }
                if (e.Upper == "Riverside" && e.Suspect.Length > 0) { river = true; }
            }
            Assert.IsTrue(kane, "케인 하위 의심 표시");
            Assert.IsTrue(river, "강가 하위 의심 표시");
            Assert.IsTrue(cat.SuspectTopics.Count >= 9, "Keyword.json 범위 오류 토픽 기록");
        }

        [Test]
        public void Reaction_SpecificFallbackNone_FollowsTopicRules()
        {
            StoryScript kw = DialogPilotEquivalenceTests.Script("Keyword");
            Assert.AreEqual(KeywordReaction.Specific, KeywordBook.Reaction(kw, "Ella Triss", "Contents of a Newspaper"));
            Assert.AreEqual(KeywordReaction.Fallback, KeywordBook.Reaction(kw, "Ella Triss", "Manhole"));
            Assert.AreEqual(KeywordReaction.Fallback, KeywordBook.Reaction(kw, "Ella Triss", null));
            Assert.AreEqual(KeywordReaction.None, KeywordBook.Reaction(kw, "No Such Npc", "Manhole"));
            Assert.AreEqual(KeywordReaction.None, KeywordBook.Reaction(null, "Ella Triss", "Manhole"));
        }

        [Test]
        public void Presentable_PerNpc_UnlockedWithSpecificTopicOnly()
        {
            KeywordBook book = new KeywordBook(LoadCatalog());
            StoryScript kw = DialogPilotEquivalenceTests.Script("Keyword");
            GameState s = GameState.New();
            s.SetFlag(FlagKeys.intelPlanetaryParade1, true);
            s.SetFlag(FlagKeys.intelSewer3, true);
            List<KeywordEntry> ella = book.GetPresentable(s, kw, "Ella Triss");
            Assert.AreEqual(1, ella.Count, "Ella 는 신문 내용만 전용 반응, Manhole 은 폴백");
            Assert.AreEqual("Contents of a Newspaper", ella[0].Id);
            Assert.AreEqual(0, book.GetPresentable(GameState.New(), kw, "Ella Triss").Count);
        }

        [Test]
        public void Catalog_EveryEntryHasSpecificTopicForSomeNpc_OrIsMarkedSuspect()
        {
            KeywordCatalog cat = LoadCatalog();
            StoryScript kw = DialogPilotEquivalenceTests.Script("Keyword");
            List<string> npcs = new List<string>();
            foreach (StoryTopic t in kw.Topics)
            {
                int slash = t.Id.IndexOf('/');
                if (slash > 0 && !npcs.Contains(t.Id.Substring(0, slash))) { npcs.Add(t.Id.Substring(0, slash)); }
            }
            foreach (KeywordEntry e in cat.Entries)
            {
                bool any = false;
                foreach (string npc in npcs)
                {
                    if (KeywordBook.Reaction(kw, npc, e.Id) == KeywordReaction.Specific) { any = true; break; }
                }
                Assert.IsTrue(any || e.Suspect.Length > 0, e.Upper + "/" + e.Id + ": 전용 토픽이 없는데 의심 표시도 없음");
            }
        }

        [Test]
        public void Load_RejectsBrokenCatalog()
        {
            Assert.IsFalse(KeywordCatalog.Load("{").Ok);
            Assert.IsFalse(KeywordCatalog.Load("{\"uppers\":[],\"entries\":[{\"id\":\"x\",\"category\":\"Place\",\"upper\":\"none\",\"unlockFlag\":\"f\"}]}").Ok);
            Assert.IsTrue(KeywordCatalog.Load("{\"uppers\":[{\"id\":\"U\",\"category\":\"Place\",\"unlockFlags\":[\"f\"]}],\"entries\":[{\"id\":\"x\",\"category\":\"Place\",\"upper\":\"U\",\"unlockFlag\":\"f\"}]}").Ok);
        }
    }
}
