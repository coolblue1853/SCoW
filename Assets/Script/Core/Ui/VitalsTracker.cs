using System;

namespace SCoW.Core.Ui
{
    /// <summary>
    /// HUD 의 HP·SAN 표시용 변화 추적(UX-7). 값이 실제로 바뀐 때만 true 를 돌려 문자열 갱신을 줄이고,
    /// 직전 변화량(+/-)을 1.5초 동안 들고 있다. 순수 C#.
    /// </summary>
    public sealed class VitalsTracker
    {
        public const float DeltaSeconds = 1.5f;
        /// <summary>SAN 이 최대의 30% 이하이면 위험 비네트.</summary>
        public const int LowSanPercent = 30;

        private bool _init;
        private float _hpTimer;
        private float _sanTimer;

        public int Hp { get; private set; }
        public int San { get; private set; }
        public int HpMax { get; private set; }
        public int SanMax { get; private set; }
        public int HpDelta { get; private set; }
        public int SanDelta { get; private set; }

        /// <summary>값이 바뀌어 화면 문자열을 다시 만든 횟수(검증용 카운터).</summary>
        public int ChangeCount { get; private set; }

        /// <summary>
        /// 현재 값을 알린다. 첫 호출·silent(새 게임·불러오기)는 변화량 없이 값만 맞춘다.
        /// 아무것도 안 바뀌었으면 false.
        /// </summary>
        public bool Observe(int hp, int san, int hpMax, int sanMax, bool silent)
        {
            bool first = !_init;
            if (!first && hp == Hp && san == San && hpMax == HpMax && sanMax == SanMax && !silent)
            {
                return false;
            }
            if (first || silent)
            {
                HpDelta = 0;
                SanDelta = 0;
                _hpTimer = 0f;
                _sanTimer = 0f;
            }
            else
            {
                if (hp != Hp)
                {
                    HpDelta = hp - Hp;
                    _hpTimer = DeltaSeconds;
                }
                if (san != San)
                {
                    SanDelta = san - San;
                    _sanTimer = DeltaSeconds;
                }
            }
            _init = true;
            Hp = hp;
            San = san;
            HpMax = hpMax;
            SanMax = sanMax;
            ChangeCount++;
            return true;
        }

        /// <summary>시간 경과. 변화량이 사라지면 true(문자열을 다시 만들어야 한다).</summary>
        public bool Tick(float dt)
        {
            bool expired = false;
            if (HpDelta != 0)
            {
                _hpTimer -= dt;
                if (_hpTimer <= 0f)
                {
                    HpDelta = 0;
                    expired = true;
                }
            }
            if (SanDelta != 0)
            {
                _sanTimer -= dt;
                if (_sanTimer <= 0f)
                {
                    SanDelta = 0;
                    expired = true;
                }
            }
            return expired;
        }

        public bool HasDelta
        {
            get { return HpDelta != 0 || SanDelta != 0; }
        }

        /// <summary>SAN 이 위험 구간인가(최대치를 모르면 false).</summary>
        public bool IsSanLow
        {
            get { return SanMax > 0 && San * 100 <= SanMax * LowSanPercent; }
        }

        public string HpText
        {
            get { return Format("HP", Hp, HpMax, HpDelta); }
        }

        public string SanText
        {
            get { return Format("SAN", San, SanMax, SanDelta); }
        }

        /// <summary>"HP : 18/20 (-3)". 최대치가 0 이하면 /최대 를 생략, 변화량이 0 이면 괄호 생략.</summary>
        public static string Format(string label, int now, int max, int delta)
        {
            string s = label + " : " + now;
            if (max > 0)
            {
                s += "/" + max;
            }
            if (delta != 0)
            {
                string col = delta < 0 ? "#ff7a70" : "#7fe08a";
                s += " <color=" + col + ">(" + (delta > 0 ? "+" : "-") + Math.Abs(delta) + ")</color>";
            }
            return s;
        }
    }

    /// <summary>화면 흔들림 끄기 설정 판정(UX-7). 카메라·전투 흔들림 호출이 모두 이 규칙을 거친다.</summary>
    public static class ShakeGate
    {
        /// <summary>설정이 없으면(null) 허용. 흔들림 끄기면 거부.</summary>
        public static bool Allowed(SCoW.Core.Save.SettingsData settings)
        {
            return settings == null || settings.ShakeEnabled;
        }
    }

    /// <summary>대사 글자 크기 3단계(UX-8). 0 작게 1 보통 2 크게.</summary>
    public static class TextScale
    {
        public const float LineSpacingBonus = 15f;
        private static readonly float[] Multipliers = { 0.85f, 1f, 1.25f };

        public static int Clamp(int level)
        {
            return level < 0 ? 0 : (level >= Multipliers.Length ? Multipliers.Length - 1 : level);
        }

        public static float Multiplier(int level)
        {
            return Multipliers[Clamp(level)];
        }

        public static float Scaled(float baseSize, int level)
        {
            return (float)Math.Round(baseSize * Multiplier(level));
        }
    }
}
