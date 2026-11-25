using DG.Tweening;
using SCoW.Core.Ui;
using UnityEngine;

/// <summary>
/// 화면 흔들림의 단일 입구(UX-7). 설정의 "화면 흔들림" 이 꺼져 있으면 흔들지 않고,
/// 시퀀스 길이가 어긋나지 않도록 같은 길이의 빈 트윈을 돌려준다.
/// </summary>
public static class ShakeFx
{
    public static bool Allowed
    {
        get { return ShakeGate.Allowed(SettingsService.Current); }
    }

    public static Tween Position(Transform target, float duration, float strength, int vibrato)
    {
        if (!Allowed)
        {
            return DOVirtual.DelayedCall(duration, Noop, false);
        }
        return target.DOShakePosition(duration, strength, vibrato);
    }

    private static void Noop()
    {
    }
}
