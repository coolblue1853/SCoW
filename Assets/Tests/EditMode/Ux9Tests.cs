using NUnit.Framework;
using SCoW.Core.Ui;

namespace SCoW.Tests
{
    /// <summary>UX-9: 목록 스크롤 입력 매핑, 상호작용 힌트 글리프, 방향 포커스.</summary>
    public class Ux9Tests
    {
        [Test]
        public void KeysMapToUpDown()
        {
            Assert.AreEqual(-1, ScrollMapping.Resolve(true, false, 0f));
            Assert.AreEqual(1, ScrollMapping.Resolve(false, true, 0f));
            Assert.AreEqual(0, ScrollMapping.Resolve(false, false, 0f));
        }

        [Test]
        public void WheelMapsSameAsKeys()
        {
            Assert.AreEqual(-1, ScrollMapping.Resolve(false, false, 120f));
            Assert.AreEqual(1, ScrollMapping.Resolve(false, false, -120f));
            Assert.AreEqual(0, ScrollMapping.Resolve(false, false, 0.05f));
        }

        [Test]
        public void KeyWinsOverWheelAndBothKeysCancel()
        {
            Assert.AreEqual(1, ScrollMapping.Resolve(false, true, 120f));
            Assert.AreEqual(0, ScrollMapping.Resolve(true, true, 120f));
        }

        [Test]
        public void InteractGlyphFollowsDevice()
        {
            Assert.AreEqual("E", HintGlyphs.Get(HintAction.Interact, InputDeviceKind.Keyboard));
            Assert.AreEqual("E", HintGlyphs.Get(HintAction.Interact, InputDeviceKind.Mouse));
            Assert.AreEqual("A", HintGlyphs.Get(HintAction.Interact, InputDeviceKind.Gamepad));
            Assert.AreEqual("F", HintGlyphs.Get(HintAction.Portal, InputDeviceKind.Keyboard));
            Assert.AreEqual("D-Pad Up", HintGlyphs.Get(HintAction.Portal, InputDeviceKind.Gamepad));
        }

        [Test]
        public void DirectionalFocusPicksNeighbour()
        {
            // 2x2 격자: 0(0,1) 1(1,1) / 2(0,0) 3(1,0)
            float[] xs = { 0f, 1f, 0f, 1f };
            float[] ys = { 1f, 1f, 0f, 0f };
            Assert.AreEqual(0, DirectionalFocus.First(xs, ys, 4));
            Assert.AreEqual(1, DirectionalFocus.Pick(xs, ys, 4, 0, 1, 0));
            Assert.AreEqual(2, DirectionalFocus.Pick(xs, ys, 4, 0, 0, -1));
            Assert.AreEqual(0, DirectionalFocus.Pick(xs, ys, 4, 0, -1, 0));
            Assert.AreEqual(3, DirectionalFocus.Pick(xs, ys, 4, 1, 0, -1));
        }
    }
}
