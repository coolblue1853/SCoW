using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.Keyword;
using SCoW.Core.State;
using UnityEngine;

namespace SCoW.Tests
{
    /// <summary>KEYWORD-1 K4: 새 키워드 알림·획득 순서·기준선·배지.</summary>
    public sealed class KeywordNoticeTests
    {
        private static KeywordBook LoadBook()
        {
            TextAsset ta = Resources.Load<TextAsset>("Story/KeywordCatalog");
            Assert.IsNotNull(ta, "Story/KeywordCatalog.json 없음");
            KeywordCatalogLoadResult r = KeywordCatalog.Load(ta.text);
            Assert.IsTrue(r.Ok, string.Join(" | ", r.Errors));
            return new KeywordBook(r.Catalog);
        }

        // 기준선만 깔린 새 게임 상태.
        private static GameState Baselined(KeywordBook book)
        {
            GameState s = GameState.New();
            KeywordNotice.Sync(book, s);
            return s;
        }

        [Test]
        public void FirstSync_IsBaseline_NoToastForAlreadyUnlocked()
        {
            KeywordBook book = LoadBook();
            GameState s = GameState.New();
            s.SetFlag(FlagKeys.intelPlanetaryParade1, true);
            int unlocked = book.GetUnlocked(s).Count;
            Assert.Greater(unlocked, 0);
            List<KeywordEntry> fresh = KeywordNotice.Sync(book, s);
            Assert.AreEqual(0, fresh.Count, "기준선은 알림 없음");
            Assert.AreEqual(0, KeywordNotice.BadgeCount(s));
            Assert.IsFalse(KeywordNotice.IsNew(s, book.GetUnlocked(s)[0].Id));
        }

        [Test]
        public void NewlyUnlocked_ReportedOnce_ThenNew_UntilSeen()
        {
            KeywordBook book = LoadBook();
            GameState s = Baselined(book);
            s.SetFlag(FlagKeys.intelPlanetaryParade1, true);
            List<KeywordEntry> fresh = KeywordNotice.Sync(book, s);
            Assert.Greater(fresh.Count, 0);
            Assert.AreEqual(0, KeywordNotice.Sync(book, s).Count, "같은 키워드는 두 번 알리지 않는다");
            Assert.IsTrue(KeywordNotice.IsNew(s, fresh[0].Id));
            Assert.AreEqual(fresh.Count, KeywordNotice.BadgeCount(s));
            KeywordNotice.MarkAllSeen(book, s);
            Assert.IsFalse(KeywordNotice.IsNew(s, fresh[0].Id));
            Assert.AreEqual(0, KeywordNotice.BadgeCount(s));
        }

        [Test]
        public void AckBadge_ClearsBadgeButKeepsNew()
        {
            KeywordBook book = LoadBook();
            GameState s = Baselined(book);
            s.SetFlag(FlagKeys.intelPlanetaryParade1, true);
            List<KeywordEntry> fresh = KeywordNotice.Sync(book, s);
            KeywordNotice.AckBadge(s);
            Assert.AreEqual(0, KeywordNotice.BadgeCount(s));
            Assert.IsTrue(KeywordNotice.IsNew(s, fresh[0].Id), "수첩을 열어도 선택 창 NEW 는 남는다");
        }

        [Test]
        public void AcquisitionOrder_FollowsUnlockTime_NotCatalogOrder()
        {
            KeywordBook book = LoadBook();
            GameState s = Baselined(book);
            s.SetFlag(FlagKeys.intelSewer3, true);
            List<KeywordEntry> first = KeywordNotice.Sync(book, s);
            s.SetFlag(FlagKeys.intelPlanetaryParade1, true);
            List<KeywordEntry> second = KeywordNotice.Sync(book, s);
            Assert.Greater(first.Count, 0);
            Assert.Greater(second.Count, 0);
            List<KeywordEntry> all = new List<KeywordEntry>(book.GetUnlocked(s));
            // 일부러 카탈로그 순서를 뒤집어 넣어도 획득 순서로 돌아온다.
            all.Reverse();
            List<KeywordEntry> sorted = KeywordNotice.SortByAcquisition(all, s);
            int posFirst = sorted.FindIndex(e => e.Id == first[0].Id);
            int posSecond = sorted.FindIndex(e => e.Id == second[0].Id);
            Assert.Less(posFirst, posSecond, "먼저 얻은 키워드가 앞");
        }

        [Test]
        public void BaselinedOldSave_KeepsExistingAndOrdersThemFirst()
        {
            KeywordBook book = LoadBook();
            GameState s = GameState.New();
            s.SetFlag(FlagKeys.intelSewer3, true);
            KeywordNotice.Sync(book, s);
            s.SetFlag(FlagKeys.intelPlanetaryParade1, true);
            List<KeywordEntry> fresh = KeywordNotice.Sync(book, s);
            Assert.Greater(fresh.Count, 0, "기준선 뒤에 얻은 것만 알림");;
            List<KeywordEntry> sorted = KeywordNotice.SortByAcquisition(book.GetUnlocked(s), s);
            Assert.Less(sorted.FindIndex(e => e.Id == book.FindById("Manhole")[0].Id), sorted.FindIndex(e => e.Id == fresh[0].Id), "기준선 키워드가 앞");
            foreach (KeywordEntry e in book.GetUnlocked(s))
            {
                Assert.Greater(s.GetCounter(KeywordNotice.OrderKey(e.Id)), 0, "풀린 키워드는 전부 순번을 가진다");
            }
        }

        [Test]
        public void Notice_RoundTripsThroughSave()
        {
            KeywordBook book = LoadBook();
            GameState s = Baselined(book);
            s.SetFlag(FlagKeys.intelPlanetaryParade1, true);
            List<KeywordEntry> fresh = KeywordNotice.Sync(book, s);
            GameState back;
            SCoW.Core.Save.SaveError err = SCoW.Core.Save.SaveCodec.TryDecodeState(SCoW.Core.Save.SaveCodec.EncodeState(s), out back);
            Assert.IsNotNull(back, err.ToString());
            Assert.IsTrue(KeywordNotice.IsNew(back, fresh[0].Id));
            Assert.AreEqual(KeywordNotice.BadgeCount(s), KeywordNotice.BadgeCount(back));
            Assert.AreEqual(0, KeywordNotice.Sync(book, back).Count, "복원 뒤 다시 알리지 않는다");
        }

        [Test]
        public void IsNoticeKey_OnlyOwnKeys()
        {
            Assert.IsTrue(KeywordNotice.IsNoticeKey(KeywordNotice.SeqKey));
            Assert.IsTrue(KeywordNotice.IsNoticeKey(KeywordNotice.SeenKey("x")));
            Assert.IsFalse(KeywordNotice.IsNoticeKey(FlagKeys.intelSewer3));
            Assert.IsFalse(KeywordNotice.IsNoticeKey(null));
            Assert.IsFalse(KeywordNotice.IsNoticeKey(KeywordAsk.AskedKey("Ella Triss", "x")), "kwAsked 는 알림 키가 아니다");
        }
    }
}
