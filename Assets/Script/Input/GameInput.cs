using System;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

/// <summary>
/// 게임 전역 입력 래퍼(New Input System). 레거시 Input.* 대체.
/// 모든 조회는 "소비하지 않는" 순수 조회다(한 키를 여러 곳이 동시에 읽는 기존 동작 유지).
/// 에셋: Resources/Input/SCoWInput.inputactions. 설계: Docs/Playtest/InputMigration.md.
/// </summary>
public static partial class GameInput
{
    private const string AssetPath = "Input/SCoWInput";
    private const float MoveDeadZone = 0.2f;

    private static InputActionAsset _asset;
    private static InputAction _move, _interact, _present, _advance, _portal, _pointerPrimary;
    private static InputAction _cancel, _menu, _openIntel, _openMap, _openSIS;
    private static InputAction _navUp, _navDown, _pageNext, _pagePrev, _todoTab, _quickSave, _quickLoad;
    private static InputAction _swapLeft, _swapRight, _sewerMinimap;
    private static InputAction _dlgBacklog, _dlgSkipHold, _dlgSkipToggle, _dlgAutoToggle;
    private static Vector2 _lastPointer;
    private static IDisposable _anyPress;
    private static InputDeviceKind _lastDevice = InputDeviceKind.Keyboard;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        if (_asset != null)
        {
            _asset.Disable();
        }

        if (_anyPress != null)
        {
            _anyPress.Dispose();
            _anyPress = null;
        }

        ResetRebindStatics();
        _asset = null;
        _lastPointer = Vector2.zero;
        _lastDevice = InputDeviceKind.Keyboard;
    }

    /// <summary>
    /// 마지막으로 입력이 들어온 장치(키보드·마우스·패드). 키 안내 글리프와 포커스 링 표시가 쓴다.
    /// 버튼 입력은 이벤트로, 마우스 이동·스틱 기울임은 조회할 때 감지한다(조회만 하고 입력은 소비하지 않는다).
    /// </summary>
    public static InputDeviceKind LastDevice
    {
        get
        {
            Ensure();
            Mouse mouse = Mouse.current;
            if (mouse != null && mouse.delta.ReadValue().sqrMagnitude > 1f)
            {
                _lastDevice = InputDeviceKind.Mouse;
            }
            Gamepad pad = Gamepad.current;
            if (pad != null && (pad.leftStick.ReadValue().sqrMagnitude > 0.25f || pad.dpad.ReadValue().sqrMagnitude > 0.1f))
            {
                _lastDevice = InputDeviceKind.Gamepad;
            }
            return _lastDevice;
        }
    }

    private static void OnAnyButton(InputControl control)
    {
        InputDevice device = control.device;
        if (device is Keyboard)
        {
            _lastDevice = InputDeviceKind.Keyboard;
        }
        else if (device is Mouse)
        {
            _lastDevice = InputDeviceKind.Mouse;
        }
        else if (device is Gamepad || device is Joystick)
        {
            _lastDevice = InputDeviceKind.Gamepad;
        }
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
        _todoTab = Find(ui, "TodoTab");
        _quickSave = Find(ui, "QuickSave");
        _quickLoad = Find(ui, "QuickLoad");
        _swapLeft = Find(ui, "SwapLeft");
        _swapRight = Find(ui, "SwapRight");
        _sewerMinimap = Find(ui, "SewerMinimap");
        RestoreSavedBindings();
        HintGlyphs.Resolver = ResolveGlyph;
        _asset.Enable();
        if (_anyPress == null)
        {
            _anyPress = InputSystem.onAnyButtonPress.Call(OnAnyButton);
        }
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
    /// <summary>빠른 저장(F5 / 패드 L1+십자 아래). 입력 에셋에 없으면 항상 false.</summary>
    public static bool QuickSaveDown { get { return Down(_quickSave); } }
    /// <summary>빠른 불러오기(F9 / 패드 L1+십자 위).</summary>
    public static bool QuickLoadDown { get { return Down(_quickLoad); } }
    /// <summary>수첩 "할 일" 탭 열기·닫기(Q / 패드 오른쪽 스틱 누름, UX-3). 입력 에셋에 없으면 항상 false.</summary>
    // 패드 오른쪽 스틱 누름은 DialogSkipToggle(Play 맵)과 같은 버튼이다. 겹침 방지: 수첩이 열리면(UiStack) DialogManager 는
    // blocked(UiGate.AnyOpen)로 HandleControls 를 건너뛰고, TodoTab 은 수첩이 맨 위(IsTop(Intel))일 때만 읽는다. 둘은 동시에 반응하지 않는다.
    public static bool TodoTabDown { get { return Down(_todoTab); } }

    /// <summary>전투 대열 교체 단축키, 화면 왼쪽(Q / 패드 LB, PT-6). 입력 에셋에 없으면 항상 false.</summary>
    public static bool SwapLeftDown { get { return Down(_swapLeft); } }
    /// <summary>전투 대열 교체 단축키, 화면 오른쪽(R / 패드 RB, PT-6).</summary>
    public static bool SwapRightDown { get { return Down(_swapRight); } }
    /// <summary>하수도 미니맵 크게/작게(SEWER-1 S5). 키보드 M, 패드 십자 아래. 패드 LB 를 누른 채(QuickSave = LB+십자 아래)면 false.</summary>
    public static bool SewerMinimapDown
    {
        get
        {
            Gamepad pad = Gamepad.current;
            if (pad != null && pad.leftShoulder.isPressed)
            {
                return false;
            }
            return Down(_sewerMinimap);
        }
    }

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

    /// <summary>이번 프레임 마우스 휠 세로 변화(위로 굴리면 양수). 마우스가 없으면 0. 수첩 목록 스크롤(UX-9)이 쓴다.</summary>
    public static float ScrollY
    {
        get
        {
            Mouse mouse = Mouse.current;
            return mouse != null ? mouse.scroll.ReadValue().y : 0f;
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
