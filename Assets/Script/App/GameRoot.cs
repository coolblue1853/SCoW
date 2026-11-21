using SCoW.Core.State;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 진행 상태 보유(P3) + 플레이 시간·불러오기 주입(P4). 씬 오브젝트·DontDestroyOnLoad 없이 첫 접근 때 만들어진다.
/// 이후 단계에서 읽은 기록 등을 얹는다(Architecture.md 2-8).
/// </summary>
public sealed class GameRoot
{
    private static GameRoot _instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        // Enter Play Mode Options(도메인 리로드 끔) 대비: 정적 상태를 처음처럼 되돌린다.
        _instance = null;
        StateChanged = null;
        SaveFlow.ResetStatics();
        UiGate.ResetStatics();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitBeforeScene()
    {
        // 일시 정적 필드를 비우고 저장된 설정(음량·텍스트 속도·해상도)을 적용한다. ES3 는 이 시점부터 안전하다.
        DataBaseManager.ResetTransientStatics();
        SettingsService.Apply();
        SceneRouter.SceneEntered -= OnSceneEntered;
        SceneRouter.SceneEntered += OnSceneEntered;
    }

    private static void OnSceneEntered(string sceneId)
    {
        // 플레이 시간은 Main 씬에 있는 동안만 센다(타이틀·캐릭터 생성·설정 대기 제외).
        Instance.SetClockRunning(sceneId == SceneIds.Main);
    }

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

    private double _playBaseSec;
    private double _playStartRealtime;
    private bool _clockRunning;

    private GameState _state;

    public GameState State
    {
        get { return _state; }
        private set { AttachState(value); }
    }

    /// <summary>
    /// 상태 변경 통지(C-2 P13). 값이 실제로 바뀐 키(플래그·카운터·텍스트 키, 또는 NotifyTransient 로 올린 일시 키)를 알린다.
    /// 상태를 통째로 갈아 끼우면(새 게임·불러오기) 키 null 로 한 번 알린다. 정적 이벤트라 GameRoot 가 다시 만들어져도 구독이 유지된다.
    /// 구독자는 OnEnable 에서 구독하고 OnDisable 에서 해제한다.
    /// </summary>
    public static event System.Action<string> StateChanged;

    /// <summary>상태에 저장하지 않는 일시 값(예: 아이템 UI 열림)의 변경을 같은 통지로 올린다.</summary>
    public static void NotifyTransient(string key)
    {
        System.Action<string> handler = StateChanged;
        if (handler != null)
        {
            handler(key);
        }
    }

    private GameRoot()
    {
        _state = GameState.New();
        _state.Changed += NotifyTransient;
    }

    private void AttachState(GameState next)
    {
        if (next == null)
        {
            throw new System.ArgumentNullException("next");
        }
        if (_state != null)
        {
            _state.Changed -= NotifyTransient;
        }
        _state = next;
        _state.Changed += NotifyTransient;
        NotifyTransient(null);
    }

    /// <summary>열린 패널 스택(P5). 조회·조작은 UiGate 로.</summary>
    public UiStack Panels { get; } = new UiStack();

    /// <summary>현재 슬롯의 누적 플레이 시간(초). 새 게임·불러오기 시점부터 실시간으로 센다.</summary>
    public long PlayTimeSec
    {
        get { return (long)(_playBaseSec + (_clockRunning ? Time.realtimeSinceStartupAsDouble - _playStartRealtime : 0.0)); }
    }

    /// <summary>진행 상태를 새 게임 기본값으로 되돌린다.</summary>
    public void NewGame()
    {
        State = GameState.New();
        RestartPlayClock(0);
    }

    /// <summary>불러온 상태를 주입한다. 일시 정적 필드도 함께 초기화한다.</summary>
    public void LoadState(GameState state, long playTimeSec)
    {
        State = state;
        DataBaseManager.ResetTransientStatics();
        RestartPlayClock(playTimeSec);
    }

    private void RestartPlayClock(long baseSec)
    {
        _playBaseSec = baseSec;
        _playStartRealtime = Time.realtimeSinceStartupAsDouble;
    }

    private void SetClockRunning(bool running)
    {
        if (running == _clockRunning)
        {
            return;
        }
        double now = Time.realtimeSinceStartupAsDouble;
        if (running)
        {
            _playStartRealtime = now;
        }
        else
        {
            _playBaseSec += now - _playStartRealtime;
        }
        _clockRunning = running;
    }
}
