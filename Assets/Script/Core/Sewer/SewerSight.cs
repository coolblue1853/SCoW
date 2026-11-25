using System;
using SCoW.Core.Rules;
using SCoW.Core.Save;
using SCoW.Core.Ui;

namespace SCoW.Core.Sewer
{
    /// <summary>
    /// 하수도 시야 마스크 규칙(SEWER-1 S3, 순수 C#). SAN 비율로 시야 반경·어둠 정도를 정하고,
    /// 마스크 텍스처의 계단형 알파를 돌려준다. 수치는 임시 값(SewerImprove.md 6절 S3 메모).
    /// </summary>
    public static class SewerSight
    {
        /// <summary>SAN 0 일 때 시야 반경(화면 높이 절반 대비).</summary>
        public const float MinRadius = 0.55f;
        /// <summary>SAN 가득일 때 시야 반경.</summary>
        public const float MaxRadius = 0.9f;
        public const float BulbOnBonus = 0.15f;
        public const float BulbOffPenalty = 0.1f;
        /// <summary>마스크 텍스처에서 완전히 뚫린 반경(텍스처 반폭 대비).</summary>
        public const float HoleEdge = 0.15f;
        /// <summary>계단 한 칸 폭(텍스처 반폭 대비).</summary>
        public const float StepWidth = 0.02f;
        public const int StepCount = 4;
        /// <summary>바깥 어둠의 최대 알파. SAN 이 위험 구간이면 줄여 위험 비네트와 겹쳐도 너무 어둡지 않게 한다.</summary>
        public const float DarknessNormal = 0.85f;
        public const float DarknessLow = 0.65f;
        public const float LowRatio = 0.3f;

        public static float Ratio(int san, int sanMax)
        {
            if (sanMax <= 0)
            {
                return 1f;
            }
            float r = (float)san / sanMax;
            return r < 0f ? 0f : (r > 1f ? 1f : r);
        }

        /// <summary>시야 반경 = lerp(0.55, 0.9, r) + (전구 켜짐 +0.15 / 꺼짐 -0.1). 화면 높이 절반이 1.</summary>
        public static float Radius(float sanRatio, bool bulbOn)
        {
            float r = sanRatio < 0f ? 0f : (sanRatio > 1f ? 1f : sanRatio);
            float baseR = MinRadius + (MaxRadius - MinRadius) * r;
            return baseR + (bulbOn ? BulbOnBonus : -BulbOffPenalty);
        }

        /// <summary>바깥 어둠의 최대 알파.</summary>
        public static float Darkness(float sanRatio)
        {
            return sanRatio <= LowRatio ? DarknessLow : DarknessNormal;
        }

        /// <summary>하수도 안의 맵 화면에서만 켠다(전투·다른 장소·다른 씬은 끈다).</summary>
        public static bool Active(bool inSewer, bool inMainScene, bool inBattle)
        {
            return inSewer && inMainScene && !inBattle;
        }

        /// <summary>깜빡임 허용 여부. 설정의 화면 흔들림 끄기를 따른다(광과민 배려).</summary>
        public static bool FlickerAllowed(SettingsData settings)
        {
            return ShakeGate.Allowed(settings);
        }

        /// <summary>
        /// 마스크 텍스처 알파(0~1). d = 중심에서의 거리/텍스처 반폭.
        /// HoleEdge 안쪽은 투명, 바깥으로 StepCount 칸 계단으로 불투명해진다(도트 격자 느낌).
        /// </summary>
        public static float MaskAlpha(float d)
        {
            if (d <= HoleEdge)
            {
                return 0f;
            }
            int step = (int)Math.Floor((d - HoleEdge) / StepWidth) + 1;
            return step >= StepCount ? 1f : (float)step / StepCount;
        }
    }

    /// <summary>
    /// 전구 깜빡임 타이머(SEWER-1 S3, 순수 C#). SAN 이 안정(0.7 이상)이면 없고, 낮을수록 잦다.
    /// 간격 = Random(8,20) x r 초(최소 1.5초), 꺼지는 시간 0.1~0.3초. Tick 은 밝기 배율(1=정상)을 돌려준다.
    /// </summary>
    public sealed class BulbFlicker
    {
        public const float StableRatio = 0.7f;
        public const float MinGap = 1.5f;
        public const float DimLevel = 0.25f;

        private readonly IRng _rng;
        private float _timer;
        private bool _armed;
        private bool _dimming;

        public BulbFlicker(IRng rng)
        {
            _rng = rng;
        }

        public bool Dimming
        {
            get { return _dimming; }
        }

        public void Reset()
        {
            _armed = false;
            _dimming = false;
            _timer = 0f;
        }

        public float Tick(float dt, float sanRatio, bool allowed)
        {
            if (!allowed || sanRatio >= StableRatio)
            {
                Reset();
                return 1f;
            }
            if (!_armed)
            {
                _armed = true;
                _timer = NextGap(sanRatio);
            }
            _timer -= dt;
            if (_timer <= 0f)
            {
                if (_dimming)
                {
                    _dimming = false;
                    _timer = NextGap(sanRatio);
                }
                else
                {
                    _dimming = true;
                    _timer = _rng.Next(100, 301) / 1000f;
                }
            }
            return _dimming ? DimLevel : 1f;
        }

        private float NextGap(float sanRatio)
        {
            float gap = _rng.Next(8000, 20001) / 1000f * sanRatio;
            return gap < MinGap ? MinGap : gap;
        }
    }
}
