using NUnit.Framework;
using SCoW.Core.Ui;

namespace SCoW.Tests
{
    /// <summary>UiStack·BusyRules: 옛 8플래그 AND 가 막던 상황을 그대로 고정한다(Architecture.md P5).</summary>
    public class UiStackTests
    {
        [Test]
        public void PopTopClosesOnlyTopPanel()
        {
            UiStack s = new UiStack();
            s.Push(PanelKind.Setting);
            s.Push(PanelKind.SlotMenu);
            PanelKind popped;
            Assert.IsTrue(s.PopTop(out popped));
            Assert.AreEqual(PanelKind.SlotMenu, popped);
            Assert.IsTrue(s.IsTop(PanelKind.Setting));
            Assert.AreEqual(1, s.Count);
        }

        [Test]
        public void PushTwiceKeepsOneAndMovesToTop()
        {
            UiStack s = new UiStack();
            s.Push(PanelKind.Setting);
            s.Push(PanelKind.SlotMenu);
            s.Push(PanelKind.Setting);
            Assert.AreEqual(2, s.Count);
            Assert.IsTrue(s.IsTop(PanelKind.Setting));
        }

        [Test]
        public void RemoveMiddleKeepsOthers()
        {
            UiStack s = new UiStack();
            s.Push(PanelKind.Setting);
            s.Push(PanelKind.SlotMenu);
            Assert.IsTrue(s.Remove(PanelKind.Setting));
            Assert.IsFalse(s.Remove(PanelKind.Setting));
            Assert.IsTrue(s.IsTop(PanelKind.SlotMenu));
        }

        [Test]
        public void EmptyStackPopFails()
        {
            UiStack s = new UiStack();
            PanelKind p;
            Assert.IsFalse(s.PopTop(out p));
            Assert.IsFalse(s.Any);
        }

        [Test]
        public void RolletDoesNotBlockDialogueInputButOthersDo()
        {
            UiStack s = new UiStack();
            s.Push(PanelKind.Rollet);
            Assert.IsFalse(s.BlocksDialogueInput);
            s.Push(PanelKind.Setting);
            Assert.IsTrue(s.BlocksDialogueInput);
            s.Clear();
            Assert.IsFalse(s.Any);
        }
    }

    public class BusyRulesTests
    {
        private static BusyFlags With(string flag)
        {
            object boxed = new BusyFlags();
            typeof(BusyFlags).GetField(flag).SetValue(boxed, true);
            return (BusyFlags)boxed;
        }

        private static readonly string[] AllFlags =
        {
            "Dialog1", "Dialog2", "StoryDirecting", "SecondDirecting", "Directing", "Rollet", "BattleRollet",
            "PortalWait", "MapMoving", "Scene", "Select", "SelectionOn", "BattleSan", "StrDialogOn", "BarMove",
            "BattleBeforeDialog", "AfterBattle",
        };

        // 막히는 플래그 표(옛 코드 줄별 AND 조건). 표에 없는 플래그 하나만 켜면 열려야 한다.
        private static void Check(PanelKind kind, string[] blockers)
        {
            UiStack empty = new UiStack();
            Assert.IsTrue(BusyRules.CanOpen(kind, new BusyFlags(), empty, "Bar", false), kind + " 빈 상태에서 열려야 함");
            foreach (string flag in AllFlags)
            {
                bool shouldBlock = System.Array.IndexOf(blockers, flag) >= 0;
                bool opened = BusyRules.CanOpen(kind, With(flag), empty, "Bar", false);
                Assert.AreEqual(!shouldBlock, opened, kind + " / " + flag);
            }
        }

        [Test]
        public void SettingBlockedOnlyByRolletPortalDirectingMapMoving()
        {
            // 옛 규칙은 대사·스토리·2차 연출도 막았다. B-4: 이 셋은 이제 연다.
            Check(PanelKind.Setting, new[] { "Rollet", "PortalWait", "Directing", "MapMoving" });
        }

        [Test]
        public void IntelBlockedByLegacyConditions()
        {
            Check(PanelKind.Intel, new[] { "Dialog1", "StoryDirecting", "PortalWait", "Rollet", "Directing", "SecondDirecting", "MapMoving" });
            Assert.IsFalse(BusyRules.CanOpen(PanelKind.Intel, new BusyFlags(), new UiStack(), "Bar", true), "난독 디버프");
        }

        [Test]
        public void MapAndInventoryBlockedByLegacyConditionsAndBattleRoad()
        {
            string[] b = { "Dialog1", "StoryDirecting", "PortalWait", "SecondDirecting", "Rollet", "Directing", "MapMoving" };
            Check(PanelKind.Map, b);
            Check(PanelKind.Inventory, b);
            Assert.IsFalse(BusyRules.CanOpen(PanelKind.Map, new BusyFlags(), new UiStack(), "BattleRoad", false));
            Assert.IsFalse(BusyRules.CanOpen(PanelKind.Inventory, new BusyFlags(), new UiStack(), "BattleRoad", false));
        }

        [Test]
        public void ShopBlockedByDialogRolletDirecting()
        {
            Check(PanelKind.Shop, new[] { "Dialog1", "Rollet", "Directing" });
        }

        [Test]
        public void AnyOpenPanelBlocksOtherPanelsExceptSlotMenuOverSetting()
        {
            PanelKind[] opens = { PanelKind.Setting, PanelKind.Intel, PanelKind.Map, PanelKind.Inventory, PanelKind.Shop, PanelKind.Rollet };
            PanelKind[] wants = { PanelKind.Setting, PanelKind.Intel, PanelKind.Map, PanelKind.Inventory, PanelKind.Shop };
            foreach (PanelKind open in opens)
            {
                UiStack s = new UiStack();
                s.Push(open);
                foreach (PanelKind want in wants)
                {
                    Assert.IsFalse(BusyRules.CanOpen(want, new BusyFlags(), s, "Bar", false), open + " 위에 " + want);
                }
                bool slotOk = open == PanelKind.Setting;
                Assert.AreEqual(slotOk, BusyRules.CanOpen(PanelKind.SlotMenu, new BusyFlags(), s, "Bar", false), open + " 위에 SlotMenu");
            }
        }

        [Test]
        public void SlotMenuOpensFromTitleWithEmptyStackButNotTwice()
        {
            UiStack s = new UiStack();
            Assert.IsTrue(BusyRules.CanOpen(PanelKind.SlotMenu, new BusyFlags(), s, "", false));
            s.Push(PanelKind.Setting);
            s.Push(PanelKind.SlotMenu);
            Assert.IsFalse(BusyRules.CanOpen(PanelKind.SlotMenu, new BusyFlags(), s, "", false));
        }

        [Test]
        public void MoveBlockedByDialog1StoryRolletDirectingOrAnyPanel()
        {
            UiStack empty = new UiStack();
            string[] blockers = { "Dialog1", "StoryDirecting", "Rollet", "Directing" };
            Assert.IsTrue(BusyRules.CanMove(new BusyFlags(), empty));
            foreach (string flag in AllFlags)
            {
                bool shouldBlock = System.Array.IndexOf(blockers, flag) >= 0;
                Assert.AreEqual(!shouldBlock, BusyRules.CanMove(With(flag), empty), "이동 / " + flag);
            }
            UiStack s = new UiStack();
            s.Push(PanelKind.Intel);
            Assert.IsFalse(BusyRules.CanMove(new BusyFlags(), s));
        }

        [Test]
        public void WorldInteractBlockedByDialog2StoryDirectingOrAnyPanel()
        {
            UiStack empty = new UiStack();
            string[] blockers = { "Dialog2", "StoryDirecting", "Directing" };
            foreach (string flag in AllFlags)
            {
                bool shouldBlock = System.Array.IndexOf(blockers, flag) >= 0;
                Assert.AreEqual(!shouldBlock, BusyRules.CanInteractWorld(With(flag), empty), "상호작용 / " + flag);
            }
            UiStack s = new UiStack();
            s.Push(PanelKind.Setting);
            Assert.IsFalse(BusyRules.CanInteractWorld(new BusyFlags(), s));
        }

        [Test]
        public void PortalBlockedByDirectingDialog1PortalWait()
        {
            string[] blockers = { "Directing", "Dialog1", "PortalWait" };
            foreach (string flag in AllFlags)
            {
                bool shouldBlock = System.Array.IndexOf(blockers, flag) >= 0;
                Assert.AreEqual(!shouldBlock, BusyRules.CanUsePortal(With(flag)), "포탈 / " + flag);
            }
        }

        [Test]
        public void IsBusyCoversEveryFlag()
        {
            Assert.IsFalse(BusyRules.IsBusy(new BusyFlags()));
            foreach (string flag in AllFlags)
            {
                Assert.IsTrue(BusyRules.IsBusy(With(flag)), "저장 금지 / " + flag);
            }
        }
    }
}
