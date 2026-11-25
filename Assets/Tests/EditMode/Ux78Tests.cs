using NUnit.Framework;
using SCoW.Core.Save;
using SCoW.Core.Story;
using SCoW.Core.Ui;

namespace SCoW.Tests
{
    /// <summary>UX-7(HUD HP·SAN 이벤트 갱신·흔들림 끄기)·UX-8(글자 크기·자동 대기) 순수 규칙.</summary>
    public class Ux78Tests
    {
        [Test]
        public void ShakeOffSettingBlocksShake()
        {
            SettingsData d = new SettingsData();
            Assert.IsTrue(ShakeGate.Allowed(d));
            d.ShakeEnabled = false;
            Assert.IsFalse(ShakeGate.Allowed(d));
            Assert.IsTrue(ShakeGate.Allowed(null));
        }

        [Test]
        public void VitalsRefreshOnlyWhenValueChanges()
        {
            VitalsTracker v = new VitalsTracker();
            Assert.IsTrue(v.Observe(20, 60, 20, 60, false));
            for (int i = 0; i < 200; i++)
            {
                Assert.IsFalse(v.Observe(20, 60, 20, 60, false));
            }
            Assert.AreEqual(1, v.ChangeCount);
            Assert.IsTrue(v.Observe(17, 60, 20, 60, false));
            Assert.AreEqual(2, v.ChangeCount);
            Assert.AreEqual(-3, v.HpDelta);
            Assert.AreEqual(0, v.SanDelta);
        }

        [Test]
        public void DeltaShowsSignAndExpires()
        {
            VitalsTracker v = new VitalsTracker();
            v.Observe(20, 40, 20, 60, false);
            v.Observe(20, 43, 20, 60, false);
            StringAssert.Contains("(+3)", v.SanText);
            v.Observe(20, 40, 20, 60, false);
            StringAssert.Contains("(-3)", v.SanText);
            Assert.IsFalse(v.Tick(1.0f));
            Assert.IsTrue(v.Tick(0.6f));
            Assert.AreEqual("SAN : 40/60", v.SanText);
            Assert.IsFalse(v.HasDelta);
        }

        [Test]
        public void SilentObserveSkipsDelta()
        {
            VitalsTracker v = new VitalsTracker();
            v.Observe(20, 60, 20, 60, false);
            Assert.IsTrue(v.Observe(5, 10, 20, 60, true));
            Assert.AreEqual(0, v.HpDelta);
            Assert.AreEqual(0, v.SanDelta);
            Assert.AreEqual("HP : 5/20", v.HpText);
        }

        [Test]
        public void LowSanThresholdIsThirtyPercent()
        {
            VitalsTracker v = new VitalsTracker();
            v.Observe(20, 19, 20, 60, false);
            Assert.IsFalse(v.IsSanLow);
            v.Observe(20, 18, 20, 60, false);
            Assert.IsTrue(v.IsSanLow);
            Assert.IsFalse(new VitalsTracker().IsSanLow);
        }

        [Test]
        public void TextScaleHasThreeIncreasingSizesAndClamps()
        {
            Assert.Less(TextScale.Scaled(40f, 0), TextScale.Scaled(40f, 1));
            Assert.Less(TextScale.Scaled(40f, 1), TextScale.Scaled(40f, 2));
            Assert.AreEqual(40f, TextScale.Scaled(40f, 1));
            Assert.AreEqual(TextScale.Scaled(40f, 0), TextScale.Scaled(40f, -5));
            Assert.AreEqual(TextScale.Scaled(40f, 2), TextScale.Scaled(40f, 9));
            Assert.AreEqual(SettingsData.TextScaleCount, 3);
        }

        [Test]
        public void TextSpeedFiveLevelsUnaffectedByTextScale()
        {
            for (int l = 1; l <= 5; l++)
            {
                float d = TextSpeed.DelayOf(l);
                Assert.Greater(d, 0f);
                if (l > 1)
                {
                    Assert.Less(d, TextSpeed.DelayOf(l - 1));
                }
            }
        }

        [Test]
        public void AutoWaitSettingKeepsDefaultBehavior()
        {
            Assert.AreEqual(DialogueRunner.AutoBase, SettingsChoices.AutoBaseOf(SettingsData.DefaultAutoWaitSec), 0.001f);
            Assert.AreEqual(DialogueRunner.AutoWait(10), DialogueRunner.AutoWait(10, SettingsChoices.AutoBaseOf(1.5f)), 0.001f);
            Assert.Less(SettingsChoices.AutoBaseOf(1f), SettingsChoices.AutoBaseOf(3f));
        }
    }
}
