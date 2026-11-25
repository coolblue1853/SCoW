using System;
using System.Collections.Generic;
using DG.Tweening;
using SCoW.Core.Rules;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>행동 메뉴 한 줄(PT-5).</summary>
public struct ActionOption
{
    public string Label;
    public string Tip;
    public bool Enabled;
    public ActionKind Kind;
    /// <summary>true 면 칸을 고르지 않는다(방어·도주).</summary>
    public bool NoTarget;
}

/// <summary>행동·대상 선택 결과를 코루틴에 돌려주는 상자.</summary>
public sealed class ViewPick
{
    public int Index = -1;
    public Side Side;
    public int Rank = -1;
    public bool Cancelled;
    public bool Done;
}

/// <summary>
/// 다인원 전투 View(PT-5): 3칸 아군 + 3칸 적 초상화·HP·SAN, 순서 바, 행동·대상 선택, 전투 로그.
/// 코드로 만드는 오버레이(씬 수정 없음). 입력은 GameInput(마우스·키보드·패드 같은 경로), 레거시 Input 없음.
/// 칸 교체·대열 드래그는 PT-6 범위라 여기서는 행동 메뉴의 Swap 항목까지만 쓴다. 문구는 임시 영문. TODO(I2).
/// </summary>
public sealed partial class PartyBattleView : MonoBehaviour
{
    private const float AllyX = 150f;
    private const float RankStep = 250f;
    private const float RowY = 105f; // 패널 아래끝이 대사창 이름표·버튼 위(참조 해상도 중앙 기준 -40)에 오도록
    private const int OrderChips = 8;
    private const int LogLines = 4;

    private enum Mode { None, Menu, Target }

    public static PartyBattleView Create()
    {
        GameObject go = new GameObject("PartyBattleView");
        DontDestroyOnLoad(go);
        PartyBattleView v = go.AddComponent<PartyBattleView>();
        v.Build();
        return v;
    }

    /// <summary>플레이 확인용 요약(한 줄).</summary>
    public string DebugSummary()
    {
        return "panels=" + Panels.Count + " chips=" + _chips.Count + " mode=" + _mode + " logLines=" + _log.Count;
    }

    public readonly Dictionary<string, PartyBattlePanel> Panels = new Dictionary<string, PartyBattlePanel>();
    private readonly List<GameObject> _chips = new List<GameObject>();
    private readonly List<string> _log = new List<string>();
    private readonly List<RectTransform> _menuRects = new List<RectTransform>();
    private readonly List<Image> _menuBgs = new List<Image>();
    private readonly List<ActionOption> _menu = new List<ActionOption>();
    private readonly List<PartyBattlePanel> _targets = new List<PartyBattlePanel>();
    private readonly List<(Side side, int rank)> _targetKeys = new List<(Side, int)>();

    private BattleField _field;
    private RectTransform _root;
    private RectTransform _menuRoot;
    private RectTransform _orderRoot;
    private Text _prompt;
    private Text _logText;
    private Font _font;
    private Mode _mode;
    private int _cursor;
    private ViewPick _pick;
    private float _prevMoveX;
    private int _armedFrame;
    private Vector2 _lastPointer;
    private PartyMember _actor;
    private ActionKind _targetKind;
    private string _activeId;
    private bool _blocked;


    private void Build()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 20; // 대사 UI(4)보다 위, 룰렛 중에는 통째로 숨긴다(룰렛 캔버스 5)
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        _font = TooltipView.SharedFont();

        _root = NewAnchored("Root", transform, Vector2.zero, Vector2.one);
        _root.offsetMin = Vector2.zero;
        _root.offsetMax = Vector2.zero;
        Image dim = _root.gameObject.AddComponent<Image>();
        dim.color = new Color(0f, 0f, 0f, 0.55f);
        dim.raycastTarget = false;

        _orderRoot = NewAnchored("Order", _root, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f));
        _orderRoot.anchoredPosition = new Vector2(0f, -70f);
        _orderRoot.sizeDelta = new Vector2(OrderChips * 76f, 64f);

        _prompt = PartyBattleUi.Label(NewAnchored("Prompt", _root, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f)), _font, "PromptText",
            new Vector2(900f, 40f), Vector2.zero, 30, TextAnchor.MiddleCenter);
        ((RectTransform)_prompt.transform.parent).anchoredPosition = new Vector2(0f, -140f);

        RectTransform logRoot = NewAnchored("Log", _root, new Vector2(0.5f, 0f), new Vector2(0.5f, 0f));
        // 대사창 안쪽 오른쪽(▼ 표시 왼쪽), 창 폭 안에서 줄바꿈한다
        logRoot.anchoredPosition = new Vector2(400f, 165f); // 아래끝은 HintBar 위
        logRoot.sizeDelta = new Vector2(640f, 180f);
        _logText = PartyBattleUi.Label(logRoot, _font, "LogText", new Vector2(640f, 180f), Vector2.zero, 22, TextAnchor.LowerLeft);
        _logText.horizontalOverflow = HorizontalWrapMode.Wrap;
        _logText.color = new Color(0.9f, 0.9f, 0.85f);

        _menuRoot = NewAnchored("Menu", _root, new Vector2(0f, 0f), new Vector2(0f, 0f));
        _menuRoot.anchoredPosition = new Vector2(40f, 40f);
        _menuRoot.pivot = new Vector2(0f, 0f);
        _root.gameObject.SetActive(false);
    }

    private static RectTransform NewAnchored(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        RectTransform r = (RectTransform)go.transform;
        r.anchorMin = anchorMin;
        r.anchorMax = anchorMax;
        r.pivot = new Vector2(0.5f, 0.5f);
        return r;
    }

    // ---------- 판 구성 ----------

    public void Setup(BattleField field, Func<IBattleUnit, Sprite> spriteOf)
    {
        ClearPanels();
        _field = field;
        _root.gameObject.SetActive(true);
        TimeHudView.Compact = true;
        _log.Clear();
        _logText.text = string.Empty;
        for (int side = 0; side < 2; side++)
        {
            BattleSlot[] slots = side == 0 ? field.Allies : field.Enemies;
            for (int i = 0; i < BattleField.Ranks; i++)
            {
                IBattleUnit u = slots[i].Unit;
                if (u == null)
                {
                    continue;
                }
                Color c = u.Side == Side.Ally ? new Color(0.3f, 0.45f, 0.7f) : new Color(0.65f, 0.3f, 0.3f);
                PartyBattlePanel p = new PartyBattlePanel(_root, _font, u, spriteOf == null ? null : spriteOf(u), c);
                p.Rect.anchoredPosition = SlotPos(u.Side, u.Rank);
                Panels[u.Id] = p;
            }
        }
        RefreshOrder();
    }

    private void ClearPanels()
    {
        foreach (KeyValuePair<string, PartyBattlePanel> kv in Panels)
        {
            kv.Value.Rect.DOKill();
            Destroy(kv.Value.Rect.gameObject);
        }
        Panels.Clear();
    }

    public Vector2 SlotPos(Side side, int rank)
    {
        float x = AllyX + rank * RankStep;
        return new Vector2(side == Side.Ally ? -x : x, RowY);
    }

    public PartyBattlePanel PanelOf(string id)
    {
        PartyBattlePanel p;
        return id != null && Panels.TryGetValue(id, out p) ? p : null;
    }

    public void Close()
    {
        SetMode(Mode.None);
        TooltipView.HideAll();
        ClearPanels();
        HintBar.Remove(this);
        TimeHudView.Compact = false;
        _root.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        HintBar.Remove(this);
        TooltipView.HideAll();
    }

    // ---------- 표시 ----------

    public void AppendLog(string line)
    {
        _log.Add(line);
        while (_log.Count > LogLines)
        {
            _log.RemoveAt(0);
        }
        _logText.text = string.Join("\n", _log);
    }

    public void SetPrompt(string text)
    {
        _prompt.text = text ?? string.Empty;
    }

    public void SetActiveUnit(string id)
    {
        _activeId = id;
        foreach (KeyValuePair<string, PartyBattlePanel> kv in Panels)
        {
            kv.Value.SetActive(kv.Key == id);
        }
        RefreshOrder();
    }

    public void RefreshAll()
    {
        foreach (KeyValuePair<string, PartyBattlePanel> kv in Panels)
        {
            IBattleUnit u = FindUnit(kv.Key);
            if (u != null)
            {
                kv.Value.Refresh(u);
                kv.Value.SetDimmed(u.Status == UnitStatus.Downed || u.Status == UnitStatus.Stable);
            }
        }
        RefreshOrder();
    }

    public IBattleUnit FindUnit(string id)
    {
        if (_field == null || id == null)
        {
            return null;
        }
        for (int i = 0; i < BattleField.Ranks; i++)
        {
            if (_field.Allies[i].Unit != null && _field.Allies[i].Unit.Id == id)
            {
                return _field.Allies[i].Unit;
            }
            if (_field.Enemies[i].Unit != null && _field.Enemies[i].Unit.Id == id)
            {
                return _field.Enemies[i].Unit;
            }
        }
        for (int i = 0; i < _field.Roster.Count; i++)
        {
            if (_field.Roster[i].Id == id)
            {
                return _field.Roster[i];
            }
        }
        return null;
    }

    /// <summary>순서 바: 현재 행위자 + 다음 차례 예측(BattleTimeline.Preview, 게이지를 건드리지 않는다).</summary>
    public void RefreshOrder()
    {
        for (int i = 0; i < _chips.Count; i++)
        {
            Destroy(_chips[i]);
        }
        _chips.Clear();
        if (_field == null)
        {
            return;
        }
        List<IBattleUnit> next = _field.Preview(OrderChips);
        for (int i = 0; i < next.Count; i++)
        {
            IBattleUnit u = next[i];
            bool ally = u.Side == Side.Ally;
            Image chip = PartyBattleUi.Box(_orderRoot, "Chip" + i, new Vector2(64f, 56f), new Vector2((i - (OrderChips - 1) * 0.5f) * 76f, 0f),
                ally ? new Color(0.25f, 0.4f, 0.65f, 0.95f) : new Color(0.6f, 0.25f, 0.25f, 0.95f));
            chip.raycastTarget = true;
            string name = BattleTooltipText.UnitName(u);
            Text t = PartyBattleUi.Label((RectTransform)chip.transform, _font, "L", new Vector2(64f, 56f), Vector2.zero, 18, TextAnchor.MiddleCenter);
            t.text = name.Length > 3 ? name.Substring(0, 3) : name;
            if (u.Id == _activeId)
            {
                chip.color = new Color(0.9f, 0.75f, 0.2f, 1f);
                t.color = Color.black;
            }
            TooltipTarget tt = chip.gameObject.AddComponent<TooltipTarget>();
            IBattleUnit captured = u;
            tt.Provider = () => BattleTooltipText.ForUnit(captured);
            _chips.Add(chip.gameObject);
        }
    }

    public void SpawnPopup(PartyBattlePanel panel, string text, CueTone tone)
    {
        Color c = tone == CueTone.Damage ? new Color(1f, 0.35f, 0.3f)
            : tone == CueTone.SanLoss ? new Color(0.45f, 0.65f, 1f)
            : tone == CueTone.Heal ? new Color(0.4f, 1f, 0.5f)
            : tone == CueTone.Miss ? new Color(0.8f, 0.8f, 0.8f) : Color.white;
        Text t = PartyBattleUi.Label(_root, _font, "Popup", new Vector2(260f, 50f), panel.Rect.anchoredPosition + new Vector2(0f, 150f), 40, TextAnchor.MiddleCenter);
        t.text = text;
        t.color = c;
        t.gameObject.AddComponent<Outline>().effectColor = Color.black;
        RectTransform r = t.rectTransform;
        Sequence s = DOTween.Sequence().SetLink(t.gameObject);
        s.Join(r.DOAnchorPosY(r.anchoredPosition.y + 70f, 0.8f).SetEase(Ease.OutQuad));
        s.Insert(0.45f, t.DOFade(0f, 0.35f));
        s.OnComplete(() => Destroy(t.gameObject));
    }

    // ---------- 선택 ----------

    /// <summary>행동 메뉴를 연다. 결과는 pick.Index(선택한 줄). 비활성 줄은 고를 수 없다.</summary>
    public void OpenMenu(PartyMember actor, List<ActionOption> options, ViewPick pick)
    {
        _actor = actor;
        _pick = pick;
        _menu.Clear();
        _menu.AddRange(options);
        for (int i = 0; i < _menuRects.Count; i++)
        {
            Destroy(_menuRects[i].gameObject);
        }
        _menuRects.Clear();
        _menuBgs.Clear();
        for (int i = 0; i < _menu.Count; i++)
        {
            Image bg = PartyBattleUi.Box(_menuRoot, "Opt" + i, new Vector2(300f, 46f), new Vector2(150f, 30f + (_menu.Count - 1 - i) * 52f), Color.gray);
            bg.raycastTarget = true;
            Text t = PartyBattleUi.Label((RectTransform)bg.transform, _font, "L", new Vector2(290f, 46f), Vector2.zero, 28, TextAnchor.MiddleLeft);
            t.text = (i + 1) + ". " + _menu[i].Label;
            t.color = _menu[i].Enabled ? Color.white : new Color(1f, 1f, 1f, 0.35f);
            _menuRects.Add((RectTransform)bg.transform);
            _menuBgs.Add(bg);
        }
        _cursor = FirstEnabled();
        _menuRoot.gameObject.SetActive(true);
        SetPrompt(BattleTooltipText.UnitName(actor) + ": choose an action");
        SetMode(Mode.Menu);
        PaintMenu();
    }

    /// <summary>대상 고르기. 대상은 (편, 칸). 결과는 pick.Side/Rank, 취소는 pick.Cancelled.</summary>
    public void OpenTargets(PartyMember actor, ActionKind kind, Side side, List<int> ranks, ViewPick pick)
    {
        _actor = actor;
        _targetKind = kind;
        _pick = pick;
        _targets.Clear();
        _targetKeys.Clear();
        for (int i = 0; i < ranks.Count; i++)
        {
            PartyBattlePanel p = PanelOf(_field.At(side, ranks[i]) == null ? null : _field.At(side, ranks[i]).Id);
            if (p != null)
            {
                _targets.Add(p);
                _targetKeys.Add((side, ranks[i]));
            }
        }
        _cursor = 0;
        SetPrompt("Choose a target");
        SetMode(Mode.Target);
        PaintTarget();
    }

    private void SetMode(Mode mode)
    {
        _mode = mode;
        _armedFrame = Time.frameCount + 1;
        _prevMoveX = GameInput.MoveX;
        _menuRoot.gameObject.SetActive(mode == Mode.Menu);
        if (mode == Mode.None)
        {
            ClearFocus();
            HintBar.Remove(this);
            SetPrompt(string.Empty);
            return;
        }
        HintBar.Push(this, new Hint(HintAction.Navigate, mode == Mode.Menu ? "Choose" : "Target"), new Hint(HintAction.Confirm, "Select"),
            new Hint(HintAction.Cancel, "Back"));
    }

    private int FirstEnabled()
    {
        for (int i = 0; i < _menu.Count; i++)
        {
            if (_menu[i].Enabled)
            {
                return i;
            }
        }
        return 0;
    }

    private void ClearFocus()
    {
        foreach (KeyValuePair<string, PartyBattlePanel> kv in Panels)
        {
            kv.Value.SetFocus(false);
        }
        TooltipView.Hide(this);
    }

    private void PaintMenu()
    {
        for (int i = 0; i < _menuBgs.Count; i++)
        {
            _menuBgs[i].color = i == _cursor ? new Color(0.75f, 0.6f, 0.2f, 0.95f) : new Color(0.15f, 0.15f, 0.2f, 0.9f);
        }
        if (_cursor >= 0 && _cursor < _menu.Count)
        {
            TooltipView.Show(this, _menu[_cursor].Label + "\n" + _menu[_cursor].Tip, _menuRects[_cursor]);
        }
    }

    private void PaintTarget()
    {
        ClearFocus();
        if (_targets.Count == 0)
        {
            return;
        }
        PartyBattlePanel p = _targets[_cursor];
        p.SetFocus(true);
        IBattleUnit u = FindUnit(p.Id);
        AttackPreview preview = default;
        if (_actor != null && (_targetKind == ActionKind.Melee || _targetKind == ActionKind.Ranged))
        {
            preview = _field.PreviewAttack(_actor, _targetKind, _targetKeys[_cursor].rank);
        }
        TooltipView.Show(this, BattleTooltipText.ForUnit(u, preview), p.Rect);
    }

}
