using System;
using System.Collections.Generic;
using SCoW.Core.Keyword;
using SCoW.Core.State;
using SCoW.Core.Story;
using SCoW.Core.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 키워드 선택 창(KEYWORD-1 K2, View). 옛 3단 순환 선택기 대신 한 화면 목록으로 NPC 에게 내밀 키워드를 고른다.
/// 코드로 만드는 오버레이라 씬 수정이 없다(BacklogView 와 같은 방식). UiStack 패널(PanelKind.Keyword). 상태를 소유하지 않는다:
/// 목록은 KeywordBook(Core) 이 GameState 에서 계산하고, 확정하면 콜백으로 돌려준다.
/// 입력 3경로: 이동 NavUp/NavDown(W S 십자) 또는 호버 · 탭 PagePrev/PageNext(A D 십자 좌우 어깨) 또는 탭 클릭 ·
/// 제시 Interact(E, 패드 A) 또는 항목 클릭 · 닫기 Cancel(Esc, 패드 B) 또는 바깥 클릭.
/// 그래픽은 임시(ART-2 에서 교체). TODO(I2): 문구·분류 이름을 새 I2 키로 옮긴다.
/// </summary>
public sealed class KeywordPicker : MonoBehaviour
{
    private const string CatalogResource = "Story/KeywordCatalog";
    private const float RowHeight = 64f;
    private static readonly KeywordCategory[] Tabs = { KeywordCategory.Event, KeywordCategory.Character, KeywordCategory.Place };

    private static KeywordPicker _current;
    private static KeywordBook _book;
    private static int _closedFrame = -1;

    public static bool IsOpen
    {
        get { return _current != null; }
    }

    /// <summary>이번 프레임에 닫혔나(같은 입력이 선택 행을 다시 열지 않게 호출측이 확인한다).</summary>
    public static bool ClosedThisFrame
    {
        get { return _closedFrame == Time.frameCount; }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _current = null;
        _book = null;
        _closedFrame = -1;
    }

    private string _npc;
    private int _remaining;
    private Action<KeywordEntry> _onPick;
    private TMP_FontAsset _font;
    private readonly List<KeywordEntry>[] _lists = new List<KeywordEntry>[3];
    private readonly HashSet<string> _specific = new HashSet<string>();
    private readonly List<Image> _tabImages = new List<Image>();
    private readonly List<Image> _rowImages = new List<Image>();
    private TextMeshProUGUI _emptyText;
    private TextMeshProUGUI _countText;
    private RectTransform _content;
    private ScrollRect _scroll;
    private int _tab;
    private int _index;
    private int _openedFrame;
    private bool _done;

    /// <summary>선택 목록을 연다. remaining 은 남은 질문 수(표시용). 확정하면 onPick(entry) 후 닫힌다. 열 수 없으면 false.</summary>
    public static bool Open(string npc, int remaining, Action<KeywordEntry> onPick)
    {
        if (_current != null || GameRoot.Instance == null)
        {
            return false;
        }
        KeywordBook book = GetBook();
        if (book == null)
        {
            return false;
        }
        GameObject go = new GameObject("KeywordPicker");
        KeywordPicker p = go.AddComponent<KeywordPicker>();
        p._npc = npc;
        p._remaining = remaining;
        p._onPick = onPick;
        p._openedFrame = Time.frameCount;
        p._font = DialogManager.Instance != null ? DialogManager.Instance.DialogFont : null;
        p.Collect(book, GameRoot.Instance.State);
        p.Build();
        _current = p;
        UiGate.Push(PanelKind.Keyword);
        return true;
    }

    public static void CloseCurrent()
    {
        if (_current != null)
        {
            _current.Close();
        }
    }

    // 닫기 한 곳: Destroy 는 프레임 끝이라, 스택·정적 상태·닫힌 프레임을 먼저 정리한다.
    private void Close()
    {
        _done = true;
        _closedFrame = Time.frameCount;
        if (GameRoot.Instance != null)
        {
            UiGate.Remove(PanelKind.Keyword);
        }
        if (_current == this)
        {
            _current = null;
        }
        Destroy(gameObject);
    }

    private static KeywordBook GetBook()
    {
        if (_book != null)
        {
            return _book;
        }
        TextAsset ta = Resources.Load<TextAsset>(CatalogResource);
        if (ta == null)
        {
            Debug.LogError("KeywordPicker: Resources/" + CatalogResource + " 를 찾지 못했다.");
            return null;
        }
        KeywordCatalogLoadResult res = KeywordCatalog.Load(ta.text);
        if (!res.Ok)
        {
            Debug.LogError("KeywordPicker: 카탈로그 오류 - " + string.Join(" | ", res.Errors));
            return null;
        }
        _book = new KeywordBook(res.Catalog);
        return _book;
    }

    // 풀린 하위 키워드를 분류별로 모은다. 같은 id 는 한 번만(상위가 달라도 토픽 키가 같다).
    private void Collect(KeywordBook book, GameState state)
    {
        StoryScript script = StoryDatabase.KeywordScriptOrNull();
        HashSet<string> seen = new HashSet<string>();
        for (int i = 0; i < Tabs.Length; i++)
        {
            List<KeywordEntry> raw = book.GetUnlocked(Tabs[i], state);
            List<KeywordEntry> list = new List<KeywordEntry>();
            for (int k = 0; k < raw.Count; k++)
            {
                if (!seen.Add(raw[k].Id))
                {
                    continue;
                }
                list.Add(raw[k]);
                if (KeywordBook.Reaction(script, _npc, raw[k].Id) == KeywordReaction.Specific)
                {
                    _specific.Add(raw[k].Id);
                }
            }
            _lists[i] = list;
        }
        _tab = FirstNonEmptyTab();
    }

    private int FirstNonEmptyTab()
    {
        for (int i = 0; i < _lists.Length; i++)
        {
            if (_lists[i].Count > 0)
            {
                return i;
            }
        }
        return 0;
    }

    private void OnDestroy()
    {
        if (GameRoot.Instance != null)
        {
            UiGate.Remove(PanelKind.Keyword);
        }
        if (_current == this)
        {
            _current = null;
        }
    }

    private void Update()
    {
        if (_done)
        {
            return;
        }
        if (UiGate.TryCancel(PanelKind.Keyword, false))
        {
            Close();
            return;
        }
        if (GameInput.NavUpDown)
        {
            MoveIndex(-1);
        }
        else if (GameInput.NavDownDown)
        {
            MoveIndex(1);
        }
        if (GameInput.PagePrevDown)
        {
            ShiftTab(-1);
        }
        else if (GameInput.PageNextDown)
        {
            ShiftTab(1);
        }
        if (Time.frameCount != _openedFrame && GameInput.InteractDown)
        {
            Confirm();
        }
    }

    private void MoveIndex(int delta)
    {
        int n = _lists[_tab].Count;
        if (n == 0)
        {
            return;
        }
        _index = (_index + delta + n) % n;
        RefreshSelection();
    }

    // 비어 있는 탭은 건너뛴다(옛 upperEventKeywordList.Count != 0 분기의 일반화).
    private void ShiftTab(int delta)
    {
        for (int step = 1; step <= Tabs.Length; step++)
        {
            int t = ((_tab + delta * step) % Tabs.Length + Tabs.Length) % Tabs.Length;
            if (_lists[t].Count > 0)
            {
                SetTab(t);
                return;
            }
        }
    }

    private void SetTab(int t)
    {
        if (t == _tab && _rowImages.Count > 0)
        {
            return;
        }
        _tab = t;
        _index = 0;
        RebuildRows();
    }

    private void Confirm()
    {
        List<KeywordEntry> list = _lists[_tab];
        if (_done || list.Count == 0 || _remaining <= 0)
        {
            return;
        }
        KeywordEntry picked = list[Mathf.Clamp(_index, 0, list.Count - 1)];
        Close();
        if (_onPick != null)
        {
            _onPick(picked);
        }
    }

    // ---- 화면 만들기(임시 그래픽) ----

    private void Build()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 29;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        gameObject.AddComponent<GraphicRaycaster>();
        if (EventSystem.current == null)
        {
            GameObject es = new GameObject("EventSystem(KeywordPicker)");
            es.AddComponent<EventSystem>();
            es.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
        }

        RectTransform dim = MakeImage("Dim", transform, new Color(0f, 0f, 0f, 0.6f));
        Stretch(dim);
        Button dimBtn = dim.gameObject.AddComponent<Button>();
        dimBtn.transition = Selectable.Transition.None;
        dimBtn.onClick.AddListener(Close);

        RectTransform panel = MakeImage("Panel", transform, new Color(0.1f, 0.09f, 0.08f, 0.96f));
        Anchor(panel, new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(1000f, 800f));
        panel.gameObject.AddComponent<Button>().transition = Selectable.Transition.None; // 패널 안 클릭이 Dim 으로 새지 않게

        TextMeshProUGUI title = MakeText("Title", panel, _npc, 40, TextAlignmentOptions.Left);
        Anchor(title.rectTransform, new Vector2(0f, 1f), new Vector2(40f, -20f), new Vector2(560f, 60f));
        title.rectTransform.pivot = new Vector2(0f, 1f);
        _countText = MakeText("Remaining", panel, "Questions left: " + _remaining, 24, TextAlignmentOptions.Right);
        _countText.color = _remaining > 0 ? new Color(1f, 0.85f, 0.4f, 1f) : new Color(0.9f, 0.4f, 0.4f, 1f);
        Anchor(_countText.rectTransform, new Vector2(1f, 1f), new Vector2(-40f, -26f), new Vector2(460f, 50f));
        _countText.rectTransform.pivot = new Vector2(1f, 1f);

        BuildTabs(panel);

        RectTransform viewport = MakeImage("Viewport", panel, new Color(0f, 0f, 0f, 0.25f));
        Anchor(viewport, new Vector2(0.5f, 0.5f), new Vector2(0f, -45f), new Vector2(920f, 560f));
        viewport.gameObject.AddComponent<RectMask2D>();
        GameObject contentGo = new GameObject("Content", typeof(RectTransform));
        _content = (RectTransform)contentGo.transform;
        _content.SetParent(viewport, false);
        _content.anchorMin = new Vector2(0f, 1f);
        _content.anchorMax = new Vector2(1f, 1f);
        _content.pivot = new Vector2(0.5f, 1f);
        _content.anchoredPosition = Vector2.zero;
        _content.sizeDelta = Vector2.zero;
        VerticalLayoutGroup vlg = contentGo.AddComponent<VerticalLayoutGroup>();
        vlg.padding = new RectOffset(10, 10, 10, 10);
        vlg.spacing = 6f;
        vlg.childControlWidth = true;
        vlg.childControlHeight = false;
        vlg.childForceExpandWidth = true;
        vlg.childForceExpandHeight = false;
        contentGo.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        _scroll = viewport.gameObject.AddComponent<ScrollRect>();
        _scroll.content = _content;
        _scroll.viewport = viewport;
        _scroll.horizontal = false;
        _scroll.vertical = true;
        _scroll.movementType = ScrollRect.MovementType.Clamped;
        _scroll.scrollSensitivity = 40f;

        _emptyText = MakeText("Empty", viewport, "No clues yet.", 34, TextAlignmentOptions.Center);
        _emptyText.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        Stretch(_emptyText.rectTransform);

        TextMeshProUGUI hint = MakeText("Hint", panel, "W S: Select   A D: Tab   E: Present   Esc: Close   * = reacts", 20, TextAlignmentOptions.Center);
        hint.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        Anchor(hint.rectTransform, new Vector2(0.5f, 0f), new Vector2(0f, 22f), new Vector2(960f, 36f));

        RebuildRows();
    }

    private void BuildTabs(RectTransform panel)
    {
        string[] names = { "Event", "Character", "Place" };
        for (int i = 0; i < Tabs.Length; i++)
        {
            int idx = i;
            RectTransform tab = MakeImage("Tab" + names[i], panel, Color.clear);
            Anchor(tab, new Vector2(0f, 1f), new Vector2(40f + i * 300f, -100f), new Vector2(280f, 56f));
            tab.pivot = new Vector2(0f, 1f);
            Button b = tab.gameObject.AddComponent<Button>();
            b.transition = Selectable.Transition.None;
            b.onClick.AddListener(() => ClickTab(idx));
            TextMeshProUGUI t = MakeText("Label", tab, names[i], 30, TextAlignmentOptions.Center);
            Stretch(t.rectTransform);
            _tabImages.Add(tab.GetComponent<Image>());
        }
    }

    private void ClickTab(int t)
    {
        if (_lists[t].Count > 0)
        {
            SetTab(t);
        }
    }

    private void RebuildRows()
    {
        for (int i = _content.childCount - 1; i >= 0; i--)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
        _rowImages.Clear();
        List<KeywordEntry> list = _lists[_tab];
        _emptyText.gameObject.SetActive(AllEmpty());
        for (int i = 0; i < list.Count; i++)
        {
            AddRow(i, list[i]);
        }
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
        RefreshSelection();
    }

    private bool AllEmpty()
    {
        for (int i = 0; i < _lists.Length; i++)
        {
            if (_lists[i].Count > 0)
            {
                return false;
            }
        }
        return true;
    }

    private void AddRow(int index, KeywordEntry entry)
    {
        RectTransform row = MakeImage("Row" + index, _content, Color.clear);
        row.sizeDelta = new Vector2(0f, RowHeight);
        LayoutElement le = row.gameObject.AddComponent<LayoutElement>();
        le.preferredHeight = RowHeight;
        Button b = row.gameObject.AddComponent<Button>();
        b.transition = Selectable.Transition.None;
        int idx = index;
        b.onClick.AddListener(() => { _index = idx; Confirm(); });
        row.gameObject.AddComponent<RowHover>().Init(() => { _index = idx; RefreshSelection(); });
        string mark = _specific.Contains(entry.Id) ? "* " : "   ";
        TextMeshProUGUI t = MakeText("Label", row, mark + entry.Id, 32, TextAlignmentOptions.Left);
        Stretch(t.rectTransform);
        t.rectTransform.offsetMin = new Vector2(24f, 0f);
        _rowImages.Add(row.GetComponent<Image>());
    }

    private void RefreshSelection()
    {
        for (int i = 0; i < _tabImages.Count; i++)
        {
            Color c = i == _tab ? new Color(0.45f, 0.35f, 0.15f, 1f) : new Color(0.2f, 0.18f, 0.16f, 1f);
            if (_lists[i].Count == 0)
            {
                c = new Color(0.12f, 0.11f, 0.1f, 1f);
            }
            _tabImages[i].color = c;
        }
        for (int i = 0; i < _rowImages.Count; i++)
        {
            _rowImages[i].color = i == _index ? new Color(0.55f, 0.42f, 0.15f, 0.9f) : Color.clear;
        }
        EnsureVisible();
    }

    private void EnsureVisible()
    {
        if (_rowImages.Count == 0 || _scroll == null)
        {
            return;
        }
        float viewH = ((RectTransform)_scroll.transform).rect.height;
        float range = _content.rect.height - viewH;
        if (range <= 1f)
        {
            return;
        }
        float rowTop = 10f + _index * (RowHeight + 6f);
        float rowBottom = rowTop + RowHeight;
        float scrollY = (1f - _scroll.verticalNormalizedPosition) * range;
        if (rowTop < scrollY)
        {
            scrollY = rowTop;
        }
        else if (rowBottom > scrollY + viewH)
        {
            scrollY = rowBottom - viewH;
        }
        _scroll.verticalNormalizedPosition = 1f - Mathf.Clamp01(scrollY / range);
    }

    private sealed class RowHover : MonoBehaviour, IPointerEnterHandler
    {
        private Action _onEnter;

        public void Init(Action onEnter)
        {
            _onEnter = onEnter;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_onEnter != null)
            {
                _onEnter();
            }
        }
    }

    private RectTransform MakeImage(string name, Transform parent, Color color)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        go.GetComponent<Image>().color = color;
        return (RectTransform)go.transform;
    }

    private TextMeshProUGUI MakeText(string name, Transform parent, string text, int size, TextAlignmentOptions align)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        TextMeshProUGUI t = go.AddComponent<TextMeshProUGUI>();
        if (_font != null)
        {
            t.font = _font;
        }
        t.text = text;
        t.fontSize = size;
        t.alignment = align;
        t.color = Color.white;
        t.raycastTarget = false;
        return t;
    }

    private static void Stretch(RectTransform rt)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    private static void Anchor(RectTransform rt, Vector2 anchor, Vector2 pos, Vector2 size)
    {
        rt.anchorMin = anchor;
        rt.anchorMax = anchor;
        rt.pivot = new Vector2(0.5f, anchor.y == 1f ? 1f : (anchor.y == 0f ? 0f : 0.5f));
        rt.anchoredPosition = pos;
        rt.sizeDelta = size;
    }
}
