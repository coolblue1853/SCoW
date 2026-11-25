using NUnit.Framework;
using SCoW.Core.Save;
using SCoW.Core.Ui;
using UnityEngine;

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

        // ---- UX-2 설정창 ----

        [Test]
        public void CycleWrapsBothWays()
        {
            Assert.AreEqual(0, SettingsChoices.Cycle(2, 1, 3));
            Assert.AreEqual(2, SettingsChoices.Cycle(0, -1, 3));
            Assert.AreEqual(1, SettingsChoices.Cycle(0, 4, 3));
            Assert.AreEqual(0, SettingsChoices.Cycle(5, 1, 1));
        }

        [Test]
        public void StepSliderClampsAndRoundsToStep()
        {
            Assert.AreEqual(0.4f, SettingsChoices.StepSlider(0.3f, 1, 0.1f, 0f, 1f), 0.0001f);
            Assert.AreEqual(1f, SettingsChoices.StepSlider(1f, 1, 0.1f, 0f, 1f), 0.0001f);
            Assert.AreEqual(0f, SettingsChoices.StepSlider(0.05f, -1, 0.1f, 0f, 1f), 0.0001f);
            Assert.AreEqual(4f, SettingsChoices.StepSlider(3f, 1, 1f, 1f, 5f), 0.0001f);
        }

        [Test]
        public void NearestAutoWaitPicksClosestOption()
        {
            Assert.AreEqual(1, SettingsChoices.NearestAutoWait(1.5f));
            Assert.AreEqual(3, SettingsChoices.NearestAutoWait(9f));
            Assert.AreEqual(0, SettingsChoices.NearestAutoWait(0f));
        }

        [Test]
        public void OldSettingsFileGetsNewFieldDefaults()
        {
            // 옛 설정 파일(새 필드 없음)을 읽은 것과 같은 상황: 생성자 기본값이 남는다.
            SettingsData d = JsonUtility.FromJson<SettingsData>("{\"SoundVolume\":0.3,\"SfxVolume\":0.4,\"FullScreen\":false,\"ResolutionIndex\":2,\"TextSpeedLevel\":4}");
            d.Sanitize();
            Assert.AreEqual(0.3f, d.SoundVolume, 0.0001f);
            Assert.AreEqual(2, d.ResolutionIndex);
            Assert.IsTrue(d.ShakeEnabled);
            Assert.AreEqual(1, d.TextScaleLevel);
            Assert.AreEqual(SettingsData.DefaultAutoWaitSec, d.AutoWaitSec, 0.0001f);
            Assert.IsTrue(d.ShowHints);
            Assert.IsFalse(d.FastRoll);
        }

        [Test]
        public void SanitizeFixesBrokenNewFields()
        {
            SettingsData d = new SettingsData();
            d.TextScaleLevel = 9;
            d.AutoWaitSec = float.NaN;
            d.Sanitize();
            Assert.AreEqual(1, d.TextScaleLevel);
            Assert.AreEqual(SettingsData.DefaultAutoWaitSec, d.AutoWaitSec, 0.0001f);
            d.AutoWaitSec = 99f;
            d.Sanitize();
            Assert.AreEqual(SettingsData.DefaultAutoWaitSec, d.AutoWaitSec, 0.0001f);
        }
    }
}
