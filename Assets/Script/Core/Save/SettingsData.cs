using System;

namespace SCoW.Core.Save
{
    /// <summary>슬롯과 무관한 설정. settings.es3 의 "settings" 키(ES3 가 공개 필드를 직렬화). 필드는 추가만 한다.</summary>
    [Serializable]
    public sealed class SettingsData
    {
        public const float DefaultVolume = 0.5f;
        public const float DefaultTextDelay = 0.07f;
        public const int ResolutionCount = 6;

        public float SoundVolume = DefaultVolume;
        public float SfxVolume = DefaultVolume;
        public float TextDelay = DefaultTextDelay;
        public bool FullScreen = true;
        public int ResolutionIndex = 1;

        /// <summary>손상·수동 편집으로 범위를 벗어난 값을 안전한 값으로 되돌린다.</summary>
        public void Sanitize()
        {
            SoundVolume = Clamp01(SoundVolume, DefaultVolume);
            SfxVolume = Clamp01(SfxVolume, DefaultVolume);
            if (float.IsNaN(TextDelay) || float.IsInfinity(TextDelay) || TextDelay < 0f || TextDelay > 1f)
            {
                TextDelay = DefaultTextDelay;
            }
            if (ResolutionIndex < 0 || ResolutionIndex >= ResolutionCount)
            {
                ResolutionIndex = 1;
            }
        }

        private static float Clamp01(float v, float fallback)
        {
            if (float.IsNaN(v) || float.IsInfinity(v))
            {
                return fallback;
            }
            return v < 0f ? 0f : (v > 1f ? 1f : v);
        }
    }
}
