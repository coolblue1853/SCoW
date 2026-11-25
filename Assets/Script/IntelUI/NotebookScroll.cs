using SCoW.Core.Ui;

/// <summary>
/// 수첩 3목록(인물·사건·장소) 공통 스크롤 입력(UX-9). 휠·W/S·방향키·패드 십자키가 모두 같은 한 칸 이동이 된다.
/// 수첩이 맨 위 패널일 때만 반응한다. 매핑 규칙은 순수 C#(ScrollMapping)에 있다.
/// </summary>
public static class NotebookScroll
{
    /// <summary>-1 = 위, +1 = 아래, 0 = 없음.</summary>
    public static int Step()
    {
        if (!UiGate.Stack.IsTop(PanelKind.Intel))
        {
            return 0;
        }
        return ScrollMapping.Resolve(GameInput.NavUpDown, GameInput.NavDownDown, GameInput.ScrollY);
    }
}
