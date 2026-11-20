using SCoW.Core.State;

/// <summary>
/// 최소판(P3): 진행 상태 보유만. 씬 오브젝트·DontDestroyOnLoad 없이 첫 접근 때 만들어진다.
/// 이후 단계에서 설정·읽은 기록·저장 서비스 등을 얹는다(Architecture.md 2-8).
/// </summary>
public sealed class GameRoot
{
    private static GameRoot _instance;

    public static GameRoot Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameRoot();
            }
            return _instance;
        }
    }

    public GameState State { get; private set; } = GameState.New();

    /// <summary>진행 상태를 새 게임 기본값으로 되돌린다.</summary>
    public void NewGame()
    {
        State = GameState.New();
    }
}
