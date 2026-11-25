using NUnit.Framework;
using SCoW.Core.Ui;

namespace SCoW.Tests
{
    /// <summary>UX-1 공통 뼈대: UiStack 닫기 일원화(TryCloseTop)와 HintStack 안내 스택.</summary>
    public class UxFoundationTests
    {
        [Test]
        public void TryCloseTopClosesOnlyTopAndKeepsOrder()
        {
            UiStack s = new UiStack();
            string log = string.Empty;
            s.Push(PanelKind.Rollet);
            s.Push(PanelKind.Setting, () => log += "S", true);
            s.Push(PanelKind.Backlog, () => log += "B", true);
            Assert.IsTrue(s.TryCloseTop());
            Assert.AreEqual("B", log);
            Assert.IsTrue(s.IsTop(PanelKind.Setting));
            Assert.IsTrue(s.TryCloseTop());
            Assert.AreEqual("BS", log);
            Assert.IsTrue(s.IsTop(PanelKind.Rollet));
            Assert.AreEqual(1, s.Count);
        }

        [Test]
        public void TryCloseTopLeavesUnregisteredTopAlone()
        {
            UiStack s = new UiStack();
            int called = 0;
            s.Push(PanelKind.Setting, () => called++);
            s.Push(PanelKind.Rollet); // 닫기 미등록
            Assert.IsFalse(s.TryCloseTop());
            Assert.AreEqual(0, called);
            Assert.AreEqual(2, s.Count);
        }

        [Test]
        public void TryCloseTopOnEmptyStackIsFalse()
        {
            Assert.IsFalse(new UiStack().TryCloseTop());
        }

        [Test]
        public void MenuClosesOnlyPanelsThatAskedForIt()
        {
            UiStack s = new UiStack();
            int a = 0;
            int b = 0;
            s.Push(PanelKind.Keyword, () => a++, false);
            Assert.IsFalse(s.TryCloseTop(false, true));
            Assert.AreEqual(0, a);
            s.Push(PanelKind.Setting, () => b++, true);
            Assert.IsTrue(s.TryCloseTop(false, true));
            Assert.AreEqual(1, b);
            Assert.IsFalse(s.TryCloseTop(false, false));
        }

        [Test]
        public void CloserThatRemovesItselfIsNotDoubleRemoved()
        {
            UiStack s = new UiStack();
            s.Push(PanelKind.Intel);
            s.Push(PanelKind.Backlog, () => s.Remove(PanelKind.Backlog));
            Assert.IsTrue(s.TryCloseTop());
            Assert.AreEqual(1, s.Count);
            Assert.IsTrue(s.IsTop(PanelKind.Intel));
        }

        [Test]
        public void RePushMovesToTopAndRemoveDropsCloser()
        {
            UiStack s = new UiStack();
            s.Push(PanelKind.Setting, () => { }, true);
            s.Push(PanelKind.Intel);
            s.Push(PanelKind.Setting);
            Assert.IsTrue(s.IsTop(PanelKind.Setting));
            Assert.IsTrue(s.HasCloser(PanelKind.Setting));
            s.Remove(PanelKind.Setting);
            Assert.IsFalse(s.HasCloser(PanelKind.Setting));
        }

        [Test]
        public void HintStackShowsTopAndRestoresOnRemove()
        {
            HintStack h = new HintStack();
            object a = new object();
            object b = new object();
            h.Push(a, new[] { new Hint(HintAction.Cancel, "Back") });
            h.Push(b, new[] { new Hint(HintAction.Confirm, "Select"), new Hint(HintAction.Cancel, "Close") });
            Assert.AreEqual("[E] Select    [Esc] Close", h.Format(InputDeviceKind.Keyboard));
            Assert.AreEqual("[A] Select    [B] Close", h.Format(InputDeviceKind.Gamepad));
            h.Remove(b);
            Assert.AreEqual("[Esc] Back", h.Format(InputDeviceKind.Mouse));
            h.Remove(a);
            Assert.AreEqual(string.Empty, h.Format(InputDeviceKind.Keyboard));
        }

        [Test]
        public void HintStackVersionChangesOnlyOnRealChange()
        {
            HintStack h = new HintStack();
            object a = new object();
            int v0 = h.Version;
            Assert.IsFalse(h.Remove(a));
            Assert.AreEqual(v0, h.Version);
            h.Push(a, new[] { new Hint(HintAction.Cancel, "Back") });
            Assert.AreNotEqual(v0, h.Version);
        }
    }
}
