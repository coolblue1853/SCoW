using SCoW.Core.Ui;

/// <summary>
/// 화면 하단 키 안내(UX-1) 입구. 패널은 열릴 때 Push(this, ...), 닫힐 때 Remove(this) 한다. 맨 위 패널의 안내만 보이고,
/// 글리프는 GameInput.LastDevice(키보드·마우스·패드)에 따라 바뀐다. 그리는 일은 UxOverlay 가 한다.
/// 문구는 임시 영문(TMP 정적 글꼴에 한글 없음). TODO(I2): 새 I2 키로 옮긴다.
/// </summary>
public static class HintBar
{
    internal static readonly HintStack Stack = new HintStack();

    [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        Stack.Clear();
    }

    public static void Push(object owner, params Hint[] hints)
    {
        Stack.Push(owner, hints);
    }

    public static void Remove(object owner)
    {
        Stack.Remove(owner);
    }

    /// <summary>전체 초기화(장소 이동·씬 전환). 닫히지 않고 사라진 패널의 안내가 남지 않게 한다.</summary>
    public static void Clear()
    {
        Stack.Clear();
    }
}
