using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 게임 전역 입력 래퍼(New Input System). 레거시 Input.* 대체.
/// 모든 조회는 "소비하지 않는" 순수 조회다(한 키를 여러 곳이 동시에 읽는 기존 동작 유지).
/// 에셋: Resources/Input/SCoWInput.inputactions. 설계: Docs/Playtest/InputMigration.md.
/// </summary>
public static class GameInput
{
    private const string AssetPath = "Input/SCoWInput";
    private const float MoveDeadZone = 0.2f;

    private static InputActionAsset _asset;
    private static InputAction _move, _interact, _present, _advance, _portal, _pointerPrimary;
    private static InputAction _cancel, _menu, _openIntel, _openMap, _openSIS;
    private static InputAction _navUp, _navDown, _pageNext, _pagePrev;
    private static InputAction _dlgBacklog, _dlgSkipHold, _dlgSkipToggle, _dlgAutoToggle;
    private static Vector2 _lastPointer;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        if (_asset != null)
        {
            _asset.Disable();
        }

        _asset = null;
        _lastPointer = Vector2.zero;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnableBeforeScene()
    {
        Ensure();
    }

    private static void Ensure()
    {
        if (_asset != null)
        {
            return;
        }

        _asset = Resources.Load<InputActionAsset>(AssetPath);
        if (_asset == null)
        {
            Debug.LogError("GameInput: Resources/" + AssetPath + " 를 찾지 못했다.");
            _asset = ScriptableObject.CreateInstance<InputActionAsset>();
        }

        InputActionMap play = _asset.FindActionMap("Gameplay");
        InputActionMap ui = _asset.FindActionMap("UI");
        _move = Find(play, "Move");
        _interact = Find(play, "Interact");
        _present = Find(play, "Present");
        _advance = Find(play, "Advance");
        _portal = Find(play, "Portal");
        _pointerPrimary = Find(play, "PointerPrimary");
        _dlgBacklog = Find(play, "DialogBacklog");
        _dlgSkipHold = Find(play, "DialogSkipHold");
        _dlgSkipToggle = Find(play, "DialogSkipToggle");
        _dlgAutoToggle = Find(play, "DialogAutoToggle");
        _cancel = Find(ui, "Cancel");
        _menu = Find(ui, "Menu");
        _openIntel = Find(ui, "OpenIntel");
        _openMap = Find(ui, "OpenMap");
        _openSIS = Find(ui, "OpenSIS");
        _navUp = Find(ui, "NavUp");
        _navDown = Find(ui, "NavDown");
        _pageNext = Find(ui, "PageNext");
        _pagePrev = Find(ui, "PagePrev");
        _asset.Enable();
    }

    private static InputAction Find(InputActionMap map, string name)
    {
        return map != null ? map.FindAction(name) : null;
    }

    private static bool Down(InputAction a)
    {
        Ensure();
        return a != null && a.WasPressedThisFrame();
    }

    public static bool InteractDown { get { return Down(_interact); } }
    /// <summary>대화 선택 행에서 키워드 제시 창을 바로 연다(F / 패드 Y). 입력 에셋에 없으면 항상 false.</summary>
    public static bool PresentDown { get { return Down(_present); } }
    public static bool AdvanceDown { get { return Down(_advance); } }
    public static bool PortalDown { get { return Down(_portal); } }
    public static bool CancelDown { get { return Down(_cancel); } }
    public static bool MenuDown { get { return Down(_menu); } }
    public static bool OpenIntelDown { get { return Down(_openIntel); } }
    public static bool OpenMapDown { get { return Down(_openMap); } }
    public static bool OpenSISDown { get { return Down(_openSIS); } }
    public static bool NavUpDown { get { return Down(_navUp); } }
    public static bool NavDownDown { get { return Down(_navDown); } }
    public static bool PageNextDown { get { return Down(_pageNext); } }
    public static bool PagePrevDown { get { return Down(_pagePrev); } }

    /// <summary>대사 백로그 열기(B / 마우스 휠 위 / 패드 LT).</summary>
    public static bool DialogBacklogDown { get { return Down(_dlgBacklog); } }
    /// <summary>스킵 토글(Tab / 패드 오른쪽 스틱 누름).</summary>
    public static bool DialogSkipToggleDown { get { return Down(_dlgSkipToggle); } }
    /// <summary>자동 진행 토글(T / 패드 왼쪽 스틱 누름).</summary>
    public static bool DialogAutoToggleDown { get { return Down(_dlgAutoToggle); } }

    /// <summary>스킵 홀드(Ctrl / 패드 RT): 누르고 있는 동안 true.</summary>
    public static bool DialogSkipHeld
    {
        get
        {
            Ensure();
            return _dlgSkipHold != null && _dlgSkipHold.IsPressed();
        }
    }

    /// <summary>누르고 있는 동안 true(레거시 GetMouseButton).</summary>
    public static bool PointerHeld
    {
        get
        {
            Ensure();
            return _pointerPrimary != null && _pointerPrimary.IsPressed();
        }
    }

    /// <summary>레거시 GetAxisRaw("Horizontal") 대응: -1, 0, 1 로 스냅.</summary>
    public static float MoveX
    {
        get
        {
            Ensure();
            if (_move == null)
            {
                return 0f;
            }

            float v = _move.ReadValue<float>();
            if (Mathf.Abs(v) < MoveDeadZone)
            {
                return 0f;
            }

            return Mathf.Sign(v);
        }
    }

    /// <summary>화면 좌표 포인터 위치. 마우스가 없으면 마지막 값.</summary>
    public static Vector2 PointerPosition
    {
        get
        {
            Mouse mouse = Mouse.current;
            if (mouse != null)
            {
                _lastPointer = mouse.position.ReadValue();
            }

            return _lastPointer;
        }
    }

    /// <summary>디버그 키(에디터·Development 빌드 전용). 그 외 빌드는 항상 false.</summary>
    public static bool DebugKeyDown(Key key)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Keyboard kb = Keyboard.current;
        return kb != null && kb[key].wasPressedThisFrame;
#else
        return false;
#endif
    }
}
