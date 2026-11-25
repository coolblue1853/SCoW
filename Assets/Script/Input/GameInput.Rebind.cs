using System;
using System.Collections.Generic;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 키 재지정(UX-12). 설정창 "키" 탭이 쓴다. 키보드·패드 각각 PerformInteractiveRebinding 으로 바꾸고,
/// 결과는 SettingsData.BindingOverridesJson 에 저장한다. 키 안내(HintGlyphs)는 Resolver 로 재지정 결과를 따라간다.
/// 재지정 대상은 동작마다 첫 키보드 바인딩·첫 패드 바인딩 하나씩(마우스·합성 바인딩·이동·확인 취소 등 고정 키는 제외).
/// </summary>
public static partial class GameInput
{
    public struct RebindSlot
    {
        public readonly string Map;
        public readonly string Action;
        public readonly string LabelKo;
        public readonly string LabelEn;

        public RebindSlot(string map, string action, string ko, string en)
        {
            Map = map;
            Action = action;
            LabelKo = ko;
            LabelEn = en;
        }
    }

    /// <summary>키 탭에 나오는 순서.</summary>
    public static readonly RebindSlot[] RebindSlots =
    {
        new RebindSlot("Gameplay", "Interact", "상호작용·확인", "Interact"),
        new RebindSlot("Gameplay", "Portal", "이동(포털)", "Portal"),
        new RebindSlot("Gameplay", "Present", "키워드 제시", "Present"),
        new RebindSlot("Gameplay", "DialogBacklog", "대사 기록", "Backlog"),
        new RebindSlot("Gameplay", "DialogSkipToggle", "스킵 전환", "Skip Toggle"),
        new RebindSlot("Gameplay", "DialogAutoToggle", "자동 전환", "Auto Toggle"),
        new RebindSlot("UI", "OpenIntel", "수첩", "Notebook"),
        new RebindSlot("UI", "OpenMap", "지도", "Map"),
        new RebindSlot("UI", "OpenSIS", "상태·소지품", "Status"),
        new RebindSlot("UI", "TodoTab", "할 일 탭", "To-Do Tab"),
        new RebindSlot("UI", "SwapLeft", "대열 교체(왼쪽)", "Swap Left"),
        new RebindSlot("UI", "SwapRight", "대열 교체(오른쪽)", "Swap Right"),
    };

    private static InputActionRebindingExtensions.RebindingOperation _rebindOp;
    private static bool _rebindHoldsInput;
    private static readonly Dictionary<int, string> GlyphCache = new Dictionary<int, string>();

    /// <summary>재지정 입력을 기다리는 동안 true. 이때 입력 에셋은 꺼져 있어(모든 조회가 false) 설정창 밖 동작이 반응하지 않는다.</summary>
    public static bool IsRebinding
    {
        get { return _rebindHoldsInput; }
    }

    internal static InputActionAsset Asset
    {
        get
        {
            Ensure();
            return _asset;
        }
    }

    private static InputAction SlotAction(int slot)
    {
        Ensure();
        RebindSlot s = RebindSlots[slot];
        InputActionMap map = _asset.FindActionMap(s.Map);
        return Find(map, s.Action);
    }

    /// <summary>슬롯의 현재 키 글자(예 "E", "A"). 바인딩이 없으면 "-".</summary>
    public static string SlotLabel(int slot, bool pad)
    {
        return BindingLabel.Friendly(BindingStore.EffectivePath(SlotAction(slot), pad));
    }

    private static void RestoreSavedBindings()
    {
        string json = string.Empty;
        try
        {
            json = SettingsService.Current.BindingOverridesJson;
        }
        catch (Exception e)
        {
            Debug.LogWarning("GameInput: 저장된 키 설정을 읽지 못함 - 기본 키 사용: " + e.Message);
        }
        BindingStore.Restore(_asset, json);
        GlyphCache.Clear();
    }

    /// <summary>현재 재지정을 설정 파일에 저장한다.</summary>
    public static void SaveBindings()
    {
        Ensure();
        SettingsService.Current.BindingOverridesJson = BindingStore.Capture(_asset);
        SettingsService.Save();
    }

    /// <summary>모든 키를 기본값으로 되돌리고 저장한다.</summary>
    public static void ResetBindings()
    {
        Ensure();
        BindingStore.Restore(_asset, string.Empty);
        GlyphCache.Clear();
        SaveBindings();
    }

    /// <summary>기본 키에는 없던 새 충돌을 "A = B (E)" 꼴 문장으로. 없으면 빈 목록.</summary>
    public static List<string> ConflictLines()
    {
        Ensure();
        List<BindingConflict> found = BindingRules.NewConflicts(BindingStore.Entries(_asset));
        List<string> lines = new List<string>(found.Count);
        for (int i = 0; i < found.Count; i++)
        {
            BindingConflict c = found[i];
            lines.Add(c.ActionA + " = " + c.ActionB + " (" + BindingLabel.Friendly(c.Path) + ")");
        }
        return lines;
    }

    /// <summary>
    /// 키 입력을 기다린다. 키보드는 Esc, 패드는 Start 로 취소. done(true)=바뀜, done(false)=취소·시간 초과.
    /// 끝난 뒤에도 입력 에셋은 꺼진 채이므로 호출측이 한 프레임 뒤 EndRebind 로 켠다(취소 키가 같은 프레임에 다른 곳을 닫지 않게).
    /// </summary>
    public static bool BeginRebind(int slot, bool pad, Action<bool> done)
    {
        Ensure();
        if (_rebindHoldsInput || slot < 0 || slot >= RebindSlots.Length)
        {
            return false;
        }
        InputAction action = SlotAction(slot);
        int index = BindingStore.FindIndex(action, pad);
        if (action == null || index < 0)
        {
            return false;
        }
        _asset.Disable();
        _rebindHoldsInput = true;
        _rebindOp = action.PerformInteractiveRebinding(index)
            .WithControlsHavingToMatchPath(pad ? "<Gamepad>" : "<Keyboard>")
            .WithCancelingThrough(pad ? "<Gamepad>/start" : "<Keyboard>/escape")
            .WithControlsExcluding("<Gamepad>/leftStick")
            .WithControlsExcluding("<Gamepad>/rightStick")
            .WithControlsExcluding("<Keyboard>/anyKey")
            .WithMagnitudeHavingToBeGreaterThan(0.5f)
            .WithTimeout(15f)
            .OnComplete(op => FinishRebind(true, done))
            .OnCancel(op => FinishRebind(false, done))
            .Start();
        return true;
    }

    private static void FinishRebind(bool changed, Action<bool> done)
    {
        if (_rebindOp != null)
        {
            _rebindOp.Dispose();
            _rebindOp = null;
        }
        GlyphCache.Clear();
        if (changed)
        {
            SaveBindings();
        }
        if (done != null)
        {
            done(changed);
        }
    }

    /// <summary>재지정이 끝난 뒤 입력 에셋을 다시 켠다.</summary>
    public static void EndRebind()
    {
        if (!_rebindHoldsInput)
        {
            return;
        }
        _rebindHoldsInput = false;
        if (_asset != null)
        {
            _asset.Enable();
        }
    }

    /// <summary>진행 중인 재지정을 즉시 취소하고 입력을 켠다(창이 꺼질 때).</summary>
    public static void CancelRebind()
    {
        if (_rebindOp != null)
        {
            _rebindOp.Dispose();
            _rebindOp = null;
        }
        EndRebind();
    }

    // 도메인 리로드를 끈 에디터 재생에서 이전 재생의 재지정 상태가 남지 않게 한다(GameInput.ResetStatics 가 부른다).
    private static void ResetRebindStatics()
    {
        if (_rebindOp != null)
        {
            _rebindOp.Dispose();
            _rebindOp = null;
        }
        _rebindHoldsInput = false;
        GlyphCache.Clear();
    }

    /// <summary>키 안내 글리프: 재지정 결과를 따른다. 해당 없으면 null(기본 표 사용).</summary>
    private static string ResolveGlyph(HintAction action, InputDeviceKind device)
    {
        Ensure();
        bool pad = device == InputDeviceKind.Gamepad;
        if (action == HintAction.Confirm && device == InputDeviceKind.Mouse)
        {
            return null;
        }
        int key = ((int)action << 1) | (pad ? 1 : 0);
        string cached;
        if (GlyphCache.TryGetValue(key, out cached))
        {
            return cached;
        }
        string result = BuildGlyph(action, pad);
        GlyphCache[key] = result;
        return result;
    }

    private static string BuildGlyph(HintAction action, bool pad)
    {
        switch (action)
        {
            case HintAction.Confirm:
            case HintAction.Interact:
                return ActionLabel("Gameplay", "Interact", pad);
            case HintAction.Notebook:
                return ActionLabel("UI", "OpenIntel", pad);
            case HintAction.Present:
                return ActionLabel("Gameplay", "Present", pad);
            case HintAction.Backlog:
                return ActionLabel("Gameplay", "DialogBacklog", pad);
            case HintAction.Portal:
                return ActionLabel("Gameplay", "Portal", pad);
            case HintAction.Swap:
            {
                string l = ActionLabel("UI", "SwapLeft", pad);
                string r = ActionLabel("UI", "SwapRight", pad);
                return l != null && r != null ? l + "/" + r : null;
            }
            default:
                return null;
        }
    }

    private static string ActionLabel(string mapName, string actionName, bool pad)
    {
        string path = BindingStore.EffectivePath(Find(_asset.FindActionMap(mapName), actionName), pad);
        return path == null ? null : BindingLabel.Friendly(path);
    }
}
