using NUnit.Framework;
using SCoW.Core.Rules;
using SCoW.Core.Save;
using SCoW.Core.Sewer;

namespace SCoW.Tests
{
    /// <summary>SEWER-1 S3: 시야 반경 공식, 계단형 마스크, 전구 깜빡임, 하수도 밖 비활성.</summary>
    public sealed class SewerSightTests
    {
        [Test]
        public void Radius_shrinks_as_san_drops()
        {
            float prev = float.MaxValue;
            for (int san = 100; san >= 0; san -= 10)
            {
                float r = SewerSight.Radius(SewerSight.Ratio(san, 100), true);
                Assert.Less(r, prev + 0.0001f);
                prev = r;
            }
            Assert.AreEqual(1.1f, SewerSight.Radius(1f, true), 0.0001f);
            Assert.AreEqual(0.1f, SewerSight.Radius(0f, false), 0.0001f);
            Assert.AreEqual(0.25f, SewerSight.Radius(0f, true), 0.0001f);
        }

        [Test]
        public void Full_san_radius_covers_screen_width_even_at_the_edge()
        {
            // 플레이어가 화면 끝에 서도 반대편 끝까지: 반경 픽셀 >= 화면 너비(전구 꺼짐이어도).
            foreach (float width in new float[] { 1280f, 1920f, 3840f })
            {
                Assert.GreaterOrEqual(SewerSight.RadiusPixels(SewerSight.Radius(1f, false), width), width);
                Assert.GreaterOrEqual(SewerSight.RadiusPixels(SewerSight.Radius(1f, true), width), width);
            }
            // 낮은 SAN 은 너비를 못 덮는다(곡선 유지).
            Assert.Less(SewerSight.Radius(0.5f, true), 1f);
        }

        [Test]
        public void Ratio_is_clamped_and_safe_without_max()
        {
            Assert.AreEqual(1f, SewerSight.Ratio(5, 0));
            Assert.AreEqual(0f, SewerSight.Ratio(-3, 50));
            Assert.AreEqual(1f, SewerSight.Ratio(80, 50));
        }

        [Test]
        public void Darkness_is_lighter_in_low_san()
        {
            Assert.Less(SewerSight.Darkness(0.2f), SewerSight.Darkness(0.8f));
        }

        [Test]
        public void MaskAlpha_is_stepped_and_monotonic()
        {
            Assert.AreEqual(0f, SewerSight.MaskAlpha(0f));
            Assert.AreEqual(0f, SewerSight.MaskAlpha(SewerSight.HoleEdge));
            Assert.AreEqual(1f, SewerSight.MaskAlpha(1f));
            float prev = 0f;
            for (float d = 0f; d <= 1f; d += 0.005f)
            {
                float a = SewerSight.MaskAlpha(d);
                Assert.GreaterOrEqual(a, prev);
                prev = a;
            }
            Assert.AreEqual(0.25f, SewerSight.MaskAlpha(0.16f), 0.0001f);
        }

        [Test]
        public void Active_only_inside_sewer_map_not_in_battle()
        {
            Assert.IsTrue(SewerSight.Active(true, true, false));
            Assert.IsFalse(SewerSight.Active(false, true, false));
            Assert.IsFalse(SewerSight.Active(true, false, false));
            Assert.IsFalse(SewerSight.Active(true, true, true));
        }

        [Test]
        public void Flicker_off_when_shake_setting_off_or_san_stable()
        {
            BulbFlicker f = new BulbFlicker(new SeededRng(1));
            for (int i = 0; i < 2000; i++)
            {
                Assert.AreEqual(1f, f.Tick(0.1f, 0.2f, false));
                Assert.AreEqual(1f, f.Tick(0.1f, 0.9f, true));
            }
            SettingsData off = new SettingsData();
            off.ShakeEnabled = false;
            Assert.IsFalse(SewerSight.FlickerAllowed(off));
            Assert.IsTrue(SewerSight.FlickerAllowed(new SettingsData()));
            Assert.IsTrue(SewerSight.FlickerAllowed(null));
        }

        [Test]
        public void Flicker_dims_more_often_when_san_low()
        {
            Assert.Greater(DimCount(0.2f), DimCount(0.65f));
            Assert.Greater(DimCount(0.2f), 0);
        }

        [Test]
        public void Flicker_dim_lasts_at_most_a_third_second()
        {
            BulbFlicker f = new BulbFlicker(new SeededRng(7));
            float run = 0f;
            float maxRun = 0f;
            for (int i = 0; i < 20000; i++)
            {
                if (f.Tick(0.01f, 0.2f, true) < 1f)
                {
                    run += 0.01f;
                    maxRun = maxRun > run ? maxRun : run;
                }
                else
                {
                    run = 0f;
                }
            }
            Assert.LessOrEqual(maxRun, 0.32f);
            Assert.GreaterOrEqual(maxRun, 0.09f);
        }

        private static int DimCount(float ratio)
        {
            BulbFlicker f = new BulbFlicker(new SeededRng(3));
            int count = 0;
            bool was = false;
            for (int i = 0; i < 60000; i++)
            {
                bool dim = f.Tick(0.01f, ratio, true) < 1f;
                if (dim && !was)
                {
                    count++;
                }
                was = dim;
            }
            return count;
        }
    }
}
