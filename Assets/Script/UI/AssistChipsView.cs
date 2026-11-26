using System;
using System.Collections.Generic;
using SCoW.Core.State;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// 판정 전 "도움/대신" 칩과 동료 반응 한 줄(PT-4, View). 룰렛 설정 창이 열려 있는 동안 칩 줄을 보이고(기본 선택은 "No help"),
/// 마우스 클릭, A/D(LB/RB: GameInput.PagePrev/Next)로 고른다. 확정은 룰렛의 기존 입력(Interact)이 한다.
/// 코드로 만드는 오버레이(씬 수정 없음). 포커스 링은 FocusScope.Highlight, 키 안내는 HintBar. 문구는 임시 영문이다.
/// TODO(I2): 문구를 새 I2 키(Party_*)로 옮긴다.
/// </summary>
public sealed class AssistChipsView : MonoBehaviour
{
    private const float ReactionSeconds = 6f;
    private const float ChipWidth = 400f;
    private const float ChipHeight = 70f;

    private static AssistChipsView _instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _instance = null;
    }

    public static AssistChipsView Get()
    {
        if (_instance == null)
        {
            GameObject go = new GameObject("AssistChipsView");
            Persist.Keep(go);
            _instance = go.AddComponent<AssistChipsView>();
            _instance.Build();
        }
        return _instance;
    }

    /// <summary>플레이 확인용: 지금 보이는 칩 글자들과 선택 번호(0 = 도움 없음).</summary>
    public static string DebugSummary
    {
        get { return _instance == null ? string.Empty : _instance.Summary(); }
    }

    private readonly List<Image> _chipBg = new List<Image>();
    private readonly List<RectTransform> _chipRect = new List<RectTransform>();
    private readonly List<string> _chipLabels = new List<string>();
    private GameObject _chipRow;
    private Text _reaction;
    private Text _die;
    private Font _font;
    private int _selected;
    private bool _open;
    private float _reactionUntil;
    private Action<AssistOption> _onChanged;
    private List<AssistOption> _options;

    public bool IsOpen
    {
        get { return _open; }
    }

    public AssistOption Selected
    {
        get { return _selected > 0 && _options != null && _selected <= _options.Count ? _options[_selected - 1] : null; }
    }

    public int SelectedIndex
    {
        get { return _selected; }
    }

    /// <summary>포인터가 칩 위인가. 룰렛이 같은 클릭으로 굴림을 시작하지 않게 막는 데 쓴다.</summary>
    public bool PointerOnChip
    {
        get { return _open && ChipAt(GameInput.PointerPosition) >= 0; }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    /// <summary>칩 줄을 연다. options 가 비어 있으면 아무것도 하지 않는다. 처음 선택은 "No help".</summary>
    public void Open(List<AssistOption> options, Action<AssistOption> onChanged)
    {
        Close();
        _reaction.gameObject.SetActive(false);
        if (options == null || options.Count == 0)
        {
            return;
        }
        _options = options;
        _onChanged = onChanged;
        _selected = 0;
        _open = true;
        BuildChips();
        _chipRow.SetActive(true);
        FocusScope.Enter(this, null);
        HintBar.Push(this, new Hint(HintAction.Page, "Helper"), new Hint(HintAction.Confirm, "Roll"));
        Refresh();
    }

    /// <summary>입력만 잠근다(굴림이 시작됐다). 칩 줄은 그대로 보이고 키 안내·포커스 링만 거둔다.</summary>
    public void Lock()
    {
        if (_open)
        {
            FocusScope.Exit(this);
            HintBar.Remove(this);
        }
        _open = false;
    }

    /// <summary>이번 프레임의 마우스 좌클릭이 칩 위인가(키보드·패드 입력은 막지 않는다).</summary>
    public static bool ClickOnAnyChip
    {
        get
        {
            Mouse mouse = Mouse.current;
            return _instance != null && mouse != null && mouse.leftButton.wasPressedThisFrame && _instance.PointerOnChip;
        }
    }

    public static void LockIfAny()
    {
        if (_instance != null)
        {
            _instance.Lock();
        }
    }

    public static void CloseIfAny()
    {
        if (_instance != null)
        {
            _instance.Close();
        }
    }

    /// <summary>칩 줄과 도움 다이스 표시를 닫는다(반응 줄은 제 시간에 사라진다). 열려 있지 않아도 안전하다.</summary>
    public void Close()
    {
        Lock();
        _options = null;
        _onChanged = null;
        _selected = 0;
        if (_chipRow != null)
        {
            _chipRow.SetActive(false);
        }
        if (_die != null)
        {
            _die.gameObject.SetActive(false);
        }
    }

    /// <summary>도움 다이스 눈(10의 자리)을 칩 줄 위에 보인다. 굴리는 동안 매 갱신마다 호출해도 된다.</summary>
    public void ShowAssistDie(int[] tens, int count)
    {
        if (_die == null || tens == null || count <= 0)
        {
            return;
        }
        string s = "Assist die :";
        for (int i = 0; i < count && i < tens.Length; i++)
        {
            s += " " + (tens[i] == 0 ? "00" : tens[i].ToString());
        }
        _die.text = s;
        _die.gameObject.SetActive(true);
    }

    /// <summary>동료 반응 한 줄(예: "Maeve: ...")을 몇 초 보인다. 빈 문자열이면 아무것도 하지 않는다.</summary>
    public void ShowReaction(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            return;
        }
        _reaction.text = line;
        _reaction.gameObject.SetActive(true);
        _reactionUntil = Time.unscaledTime + ReactionSeconds;
    }

    private void Update()
    {
        if (_reaction != null && _reaction.gameObject.activeSelf && Time.unscaledTime >= _reactionUntil)
        {
            _reaction.gameObject.SetActive(false);
        }
        if (!_open)
        {
            return;
        }
        int count = _options.Count + 1;
        int next = _selected;
        if (GameInput.PageNextDown)
        {
            next = (_selected + 1) % count;
        }
        else if (GameInput.PagePrevDown)
        {
            next = (_selected + count - 1) % count;
        }
        Mouse mouse = Mouse.current;
        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            int hit = ChipAt(GameInput.PointerPosition);
            if (hit >= 0)
            {
                next = hit;
            }
        }
        if (next != _selected)
        {
            _selected = next;
            Refresh();
            if (_onChanged != null)
            {
                _onChanged(Selected);
            }
        }
    }

    private int ChipAt(Vector2 screenPos)
    {
        for (int i = 0; i < _chipRect.Count; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_chipRect[i], screenPos, null))
            {
                return i;
            }
        }
        return -1;
    }

    private void Refresh()
    {
        for (int i = 0; i < _chipBg.Count; i++)
        {
            bool on = i == _selected;
            _chipBg[i].color = on ? new Color(0.55f, 0.42f, 0.18f, 0.95f) : new Color(0.1f, 0.09f, 0.08f, 0.88f);
        }
        if (_selected < _chipRect.Count)
        {
            FocusScope.Highlight(this, _chipRect[_selected]);
        }
    }

    private string Summary()
    {
        string s = _open ? "open sel=" + _selected + " |" : "closed |";
        for (int i = 0; i < _chipLabels.Count; i++)
        {
            s += " [" + _chipLabels[i] + "]";
        }
        return s;
    }

    private static string Label(AssistOption o)
    {
        return (o.Mode == AssistMode.Assist ? "Help: " : "Swap: ") + o.Def.DisplayName + " (" + o.Skill + ")";
    }

    private void BuildChips()
    {
        for (int i = _chipRow.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(_chipRow.transform.GetChild(i).gameObject);
        }
        _chipBg.Clear();
        _chipRect.Clear();
        _chipLabels.Clear();
        AddChip("No help");
        for (int i = 0; i < _options.Count; i++)
        {
            AddChip(Label(_options[i]));
        }
    }

    private void AddChip(string label)
    {
        GameObject chip = new GameObject("Chip", typeof(RectTransform), typeof(Image));
        chip.transform.SetParent(_chipRow.transform, false);
        RectTransform rt = (RectTransform)chip.transform;
        rt.sizeDelta = new Vector2(ChipWidth, ChipHeight);
        Image bg = chip.GetComponent<Image>();
        bg.raycastTarget = false;
        GameObject txt = new GameObject("Label", typeof(RectTransform));
        txt.transform.SetParent(chip.transform, false);
        Text t = MakeText(txt, 28, TextAnchor.MiddleCenter);
        t.text = label;
        Stretch(t.rectTransform);
        _chipBg.Add(bg);
        _chipRect.Add(rt);
        _chipLabels.Add(label);
    }

    private Text MakeText(GameObject go, int size, TextAnchor anchor)
    {
        Text t = go.AddComponent<Text>();
        t.font = _font;
        t.fontSize = size;
        t.alignment = anchor;
        t.horizontalOverflow = HorizontalWrapMode.Overflow;
        t.raycastTarget = false;
        return t;
    }

    private static void Stretch(RectTransform r)
    {
        r.anchorMin = Vector2.zero;
        r.anchorMax = Vector2.one;
        r.offsetMin = Vector2.zero;
        r.offsetMax = Vector2.zero;
    }

    private void Build()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 42;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;

        Sprite unused;
        if (settingManager.Instance != null)
        {
            settingManager.Instance.GetButtonStyle(out _font, out unused);
        }
        if (_font == null)
        {
            _font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        }

        _chipRow = new GameObject("ChipRow", typeof(RectTransform), typeof(HorizontalLayoutGroup));
        _chipRow.transform.SetParent(transform, false);
        RectTransform rr = (RectTransform)_chipRow.transform;
        rr.anchorMin = new Vector2(0.5f, 0f);
        rr.anchorMax = new Vector2(0.5f, 0f);
        rr.pivot = new Vector2(0.5f, 0f);
        rr.anchoredPosition = new Vector2(0f, 150f);
        rr.sizeDelta = new Vector2(1500f, ChipHeight);
        HorizontalLayoutGroup row = _chipRow.GetComponent<HorizontalLayoutGroup>();
        row.spacing = 16f;
        row.childAlignment = TextAnchor.MiddleCenter;
        row.childControlWidth = false;
        row.childControlHeight = false;
        row.childForceExpandWidth = false;
        row.childForceExpandHeight = false;
        _chipRow.SetActive(false);

        _die = NewLabel("AssistDie", 235f, 28, TextAnchor.MiddleCenter, new Color(0.95f, 0.85f, 0.55f, 1f));
        _reaction = NewLabel("Reaction", 300f, 32, TextAnchor.MiddleCenter, new Color(0.95f, 0.95f, 0.85f, 1f));
    }

    private Text NewLabel(string name, float y, int size, TextAnchor anchor, Color color)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(transform, false);
        Text t = MakeText(go, size, anchor);
        t.color = color;
        RectTransform r = t.rectTransform;
        r.anchorMin = new Vector2(0.5f, 0f);
        r.anchorMax = new Vector2(0.5f, 0f);
        r.pivot = new Vector2(0.5f, 0f);
        r.anchoredPosition = new Vector2(0f, y);
        r.sizeDelta = new Vector2(1500f, 50f);
        go.SetActive(false);
        return t;
    }
}
