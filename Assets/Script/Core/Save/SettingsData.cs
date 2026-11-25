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
        public const float DefaultAutoWaitSec = 1.5f;
        public const int TextScaleCount = 3;

        public float SoundVolume = DefaultVolume;
        public float SfxVolume = DefaultVolume;
        public float TextDelay = DefaultTextDelay;
        public bool FullScreen = true;
        public int ResolutionIndex = 1;
        /// <summary>대사 글자 속도 1~5단계(P8). 추가 필드: 옛 설정 파일에는 없고 기본값 3.</summary>
        public int TextSpeedLevel = 3;
        /// <summary>스킵이 안 읽은 줄도 넘기나(P8). 기본은 읽은 것만.</summary>
        public bool SkipAll = false;
        /// <summary>화면 흔들림(카메라·대사창 떨림) 허용. UX-2 에서 필드·창 추가, 적용은 UX-7. 옛 파일은 기본값 true.</summary>
        public bool ShakeEnabled = true;
        /// <summary>글자 크기 0=작게 1=보통 2=크게. 적용은 UX-8. 옛 파일은 기본값 1.</summary>
        public int TextScaleLevel = 1;
        /// <summary>자동 진행 대기 시간(초). 적용은 UX-8. 옛 파일은 기본값 1.5.</summary>
        public float AutoWaitSec = DefaultAutoWaitSec;
        /// <summary>화면 하단 키 안내(HintBar) 표시.</summary>
        public bool ShowHints = true;
        /// <summary>시간·목표 HUD 표시(UX-3, Q-UX1 임시 가정: 항상 표시·설정에서 숨김). 옛 파일은 기본값 true.</summary>
        public bool ShowTimeHud = true;
        /// <summary>빠른 판정(룰렛 연출 생략). 적용은 UX-6.</summary>
        public bool FastRoll = false;
        /// <summary>키 재지정 결과(Input System SaveBindingOverridesAsJson). UX-12 추가 필드: 옛 파일에는 없고 기본값 빈 문자열(= 기본 키).</summary>
        public string BindingOverridesJson = string.Empty;
        /// <summary>손상·수동 편집으로 범위를 벗어난 값을 안전한 값으로 되돌린다.</summary>
        public void Sanitize()
        {
            if (BindingOverridesJson == null)
            {
                BindingOverridesJson = string.Empty;
            }
            SoundVolume = Clamp01(SoundVolume, DefaultVolume);
            SfxVolume = Clamp01(SfxVolume, DefaultVolume);
            if (float.IsNaN(TextDelay) || float.IsInfinity(TextDelay) || TextDelay < 0f || TextDelay > 1f)
            {
                TextDelay = DefaultTextDelay;
            }
            if (TextSpeedLevel < 1 || TextSpeedLevel > 5)
            {
                TextSpeedLevel = 3;
            }
            if (ResolutionIndex < 0 || ResolutionIndex >= ResolutionCount)
            {
                ResolutionIndex = 1;
            }
            if (TextScaleLevel < 0 || TextScaleLevel >= TextScaleCount)
            {
                TextScaleLevel = 1;
            }
            if (float.IsNaN(AutoWaitSec) || float.IsInfinity(AutoWaitSec) || AutoWaitSec < 0.3f || AutoWaitSec > 10f)
            {
                AutoWaitSec = DefaultAutoWaitSec;
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
