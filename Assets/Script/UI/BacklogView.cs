using SCoW.Core.Story;
using SCoW.Core.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 대사 백로그 창(P8). 코드로 만드는 오버레이라 씬 수정이 없다(SlotMenuView 와 같은 방식). UiStack 패널(PanelKind.Backlog).
/// 열기: DialogManager 가 GameInput.DialogBacklogDown(B / 휠 위 / 패드 LT) 에서 호출.
/// 닫기: 같은 키 다시·Esc·패드 B·바깥 클릭·닫기 버튼. 스크롤: 마우스 휠·드래그, NavUp/NavDown(W S 위아래 패드 십자), PagePrev/PageNext(A D 어깨) 는 한 쪽씩.
/// 글꼴은 대사창 TMP(pcsenior)를 빌려 와 같은 톤을 쓴다.
/// </summary>
public sealed class BacklogView : MonoBehaviour
{
    private const float ArrowStep = 90f;

    private static BacklogView _current;

    public static bool IsOpen
    {
        get { return _current != null; }
    }

    private ScrollRect _scroll;
    private RectTransform _content;
    private int _openedFrame;

    public static void Open(Backlog log, TMP_FontAsset font)
    {
        if (_current != null || log == null)
        {
            return;
        }
        GameObject go = new GameObject("BacklogView");
        BacklogView v = go.AddComponent<BacklogView>();
        v._openedFrame = Time.frameCount;
        v.Build(log, font);
        _current = v;
        UiGate.Push(PanelKind.Backlog);
    }

    public static void CloseCurrent()
    {
        if (_current != null)
        {
            Destroy(_current.gameObject);
        }
    }

    private void OnDestroy()
    {
        UiGate.Remove(PanelKind.Backlog);
        if (_current == this)
        {
            _current = null;
        }
    }

    private void Update()
    {
        if (UiGate.TryCancel(PanelKind.Backlog, true))
        {
            Destroy(gameObject);
            return;
        }
        if (Time.frameCount != _openedFrame && GameInput.DialogBacklogDown && !ScrollWheelUp())
        {
            Destroy(gameObject);
            return;
        }
        float dy = 0f;
        if (GameInput.NavUpDown)
        {
            dy = ArrowStep;
        }
        else if (GameInput.NavDownDown)
        {
            dy = -ArrowStep;
        }
        else if (GameInput.PagePrevDown)
        {
            dy = ArrowStep * 5f;
        }
        else if (GameInput.PageNextDown)
        {
            dy = -ArrowStep * 5f;
        }
        if (dy != 0f && _content != null)
        {
            float range = _content.rect.height - ((RectTransform)_scroll.transform).rect.height;
            if (range > 1f)
            {
                _scroll.verticalNormalizedPosition = Mathf.Clamp01(_scroll.verticalNormalizedPosition + dy / range);
            }
        }
    }

    // 휠 위로 백로그가 열리는 설정이라, 창이 열린 뒤 휠 위는 스크롤로만 쓰고 닫기에 쓰지 않는다.
    private static bool ScrollWheelUp()
    {
        UnityEngine.InputSystem.Mouse m = UnityEngine.InputSystem.Mouse.current;
        return m != null && m.scroll.ReadValue().y > 0f;
    }

    private void Build(Backlog log, TMP_FontAsset font)
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 28;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        gameObject.AddComponent<GraphicRaycaster>();
        if (EventSystem.current == null)
        {
            GameObject es = new GameObject("EventSystem(Backlog)");
            es.AddComponent<EventSystem>();
            es.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
        }

        RectTransform dim = MakeImage("Dim", transform, new Color(0f, 0f, 0f, 0.7f));
        Stretch(dim);
        Button dimBtn = dim.gameObject.AddComponent<Button>();
        dimBtn.transition = Selectable.Transition.None;
        dimBtn.onClick.AddListener(() => Destroy(gameObject));

        RectTransform panel = MakeImage("Panel", transform, new Color(0.1f, 0.09f, 0.08f, 0.96f));
        Anchor(panel, new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(1500f, 900f));
        panel.gameObject.AddComponent<Button>().transition = Selectable.Transition.None; // 패널 안 클릭이 Dim 으로 새지 않게

        TextMeshProUGUI title = MakeText("Title", panel, "Log", 40, font, TextAlignmentOptions.Center);
        Anchor(title.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -45f), new Vector2(1400f, 70f));

        RectTransform viewport = MakeImage("Viewport", panel, new Color(0f, 0f, 0f, 0.25f));
        Anchor(viewport, new Vector2(0.5f, 0.5f), new Vector2(0f, -40f), new Vector2(1420f, 690f));
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
        vlg.padding = new RectOffset(30, 30, 20, 20);
        vlg.spacing = 18f;
        vlg.childControlWidth = true;
        vlg.childControlHeight = true;
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

        if (log.Count == 0)
        {
            AddEntry("", "(No lines yet)", font);
        }
        for (int i = 0; i < log.Count; i++)
        {
            AddEntry(log[i].Speaker, log[i].Text, font);
        }

        TextMeshProUGUI hint = MakeText("Hint", panel, "B: Close   W S / Wheel: Scroll   Esc: Close", 26, font, TextAlignmentOptions.Center);
        hint.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        Anchor(hint.rectTransform, new Vector2(0.5f, 0f), new Vector2(0f, 28f), new Vector2(1400f, 40f));

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
        _scroll.verticalNormalizedPosition = 0f; // 가장 최근 줄이 보이게
    }

    private void AddEntry(string speaker, string text, TMP_FontAsset font)
    {
        GameObject go = new GameObject("Entry", typeof(RectTransform));
        go.transform.SetParent(_content, false);
        TextMeshProUGUI t = go.AddComponent<TextMeshProUGUI>();
        if (font != null)
        {
            t.font = font;
        }
        t.fontSize = 30;
        t.enableWordWrapping = true;
        t.raycastTarget = false;
        t.alignment = TextAlignmentOptions.TopLeft;
        t.color = Color.white;
        string name = string.IsNullOrEmpty(speaker) ? string.Empty : "<color=#FFD966>" + speaker + "</color>  ";
        t.text = name + text;
    }

    private static RectTransform MakeImage(string name, Transform parent, Color color)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        go.GetComponent<Image>().color = color;
        return (RectTransform)go.transform;
    }

    private static TextMeshProUGUI MakeText(string name, Transform parent, string text, int size, TMP_FontAsset font, TextAlignmentOptions align)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        TextMeshProUGUI t = go.AddComponent<TextMeshProUGUI>();
        if (font != null)
        {
            t.font = font;
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
