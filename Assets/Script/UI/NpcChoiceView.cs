using System;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// NPC 앞 비판정 선택지 창(스토리 P1 C-4: 문 경찰 "설득 판정 / 알버트가 보냈다"). 코드로 만드는 임시 오버레이(씬 수정 없음).
/// 입력: 마우스(클릭·가리키기), NavUp/NavDown·Interact·Enter(키보드·패드), Cancel(Esc·패드 B)은 취소.
/// 취소하면 onCancel 이 불린다. 같은 프레임에 닫힌 Interact 가 월드 상호작용으로 다시 새지 않도록 ClosedThisFrame 을 제공한다.
/// </summary>
public sealed class NpcChoiceView : MonoBehaviour
{
    private static NpcChoiceView _current;
    private static int _closedFrame = -1;

    public static bool IsOpen
    {
        get { return _current != null; }
    }

    /// <summary>이번 프레임에 이 창이 닫혔다(같은 프레임의 Interact 를 월드가 다시 받지 않게).</summary>
    public static bool ClosedThisFrame
    {
        get { return _closedFrame == Time.frameCount; }
    }

    /// <summary>현재 보이는 선택지 문구(플레이 확인용). 닫혀 있으면 null.</summary>
    public static string[] CurrentLabels
    {
        get { return _current != null ? (string[])_current._labels.Clone() : null; }
    }

    /// <summary>플레이 확인용: 키보드·패드 선택/확정을 코드에서 대신 누른다. 열려 있으면 true.</summary>
    public static bool TestPick(int index)
    {
        if (_current == null || index < 0 || index >= _current._labels.Length)
        {
            return false;
        }
        _current.Choose(index);
        return true;
    }

    /// <summary>선택지를 연다. 이미 열려 있으면 false. 마지막 항목이 취소(onCancel)일 필요는 없다(Esc 가 취소).</summary>
    public static bool Show(string title, string[] labels, Action<int> onPick, Action onCancel)
    {
        if (_current != null || labels == null || labels.Length == 0)
        {
            return false;
        }
        GameObject go = new GameObject("NpcChoiceView");
        NpcChoiceView v = go.AddComponent<NpcChoiceView>();
        _current = v;
        v._labels = labels;
        v._onPick = onPick;
        v._onCancel = onCancel;
        v._openedFrame = Time.frameCount;
        v.Build(title);
        UiGate.Push(PanelKind.NpcChoice, v.Cancel, false);
        HintBar.Push(v, Hints);
        return true;
    }

    private static readonly Hint[] Hints =
    {
        new Hint(HintAction.Navigate, "Select"),
        new Hint(HintAction.Confirm, "Confirm"),
        new Hint(HintAction.Cancel, "Cancel"),
    };

    private string[] _labels;
    private Action<int> _onPick;
    private Action _onCancel;
    private Image[] _rows;
    private Font _font;
    private Sprite _sprite;
    private int _index;
    private int _openedFrame;
    private bool _done;

    private void OnDestroy()
    {
        if (GameRoot.Instance != null)
        {
            UiGate.Remove(PanelKind.NpcChoice);
        }
        HintBar.Remove(this);
        if (_current == this)
        {
            _current = null;
        }
    }

    private void Update()
    {
        if (_done || Time.frameCount == _openedFrame)
        {
            return;
        }
        if (GameInput.NavUpDown)
        {
            SetIndex((_index - 1 + _labels.Length) % _labels.Length);
        }
        else if (GameInput.NavDownDown)
        {
            SetIndex((_index + 1) % _labels.Length);
        }
        else if (GameInput.InteractDown || EnterPressed())
        {
            Choose(_index);
        }
    }

    private static bool EnterPressed()
    {
        UnityEngine.InputSystem.Keyboard kb = UnityEngine.InputSystem.Keyboard.current;
        return kb != null && (kb.enterKey.wasPressedThisFrame || kb.numpadEnterKey.wasPressedThisFrame);
    }

    private void Choose(int i)
    {
        if (_done)
        {
            return;
        }
        Action<int> pick = _onPick;
        Close();
        if (pick != null)
        {
            pick(i);
        }
    }

    private void Cancel()
    {
        if (_done)
        {
            return;
        }
        Action cancel = _onCancel;
        Close();
        if (cancel != null)
        {
            cancel();
        }
    }

    private void Close()
    {
        _done = true;
        _closedFrame = Time.frameCount;
        if (GameRoot.Instance != null)
        {
            UiGate.Remove(PanelKind.NpcChoice);
        }
        Destroy(gameObject);
    }

    private void SetIndex(int i)
    {
        _index = i;
        RefreshHighlight();
    }

    private void RefreshHighlight()
    {
        for (int i = 0; i < _rows.Length; i++)
        {
            _rows[i].color = i == _index ? new Color(1f, 0.85f, 0.45f, 1f) : new Color(0.75f, 0.7f, 0.65f, 1f);
        }
    }

    private void Build(string title)
    {
        if (settingManager.Instance != null)
        {
            settingManager.Instance.GetButtonStyle(out _font, out _sprite);
        }
        if (_font == null)
        {
            _font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        }
        _rows = new Image[_labels.Length];

        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 90;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        gameObject.AddComponent<GraphicRaycaster>();
        if (EventSystem.current == null)
        {
            GameObject es = new GameObject("EventSystem(NpcChoice)");
            es.AddComponent<EventSystem>();
            es.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
        }

        RectTransform dim = MakeImage("Dim", transform, new Color(0f, 0f, 0f, 0.6f), null);
        dim.anchorMin = Vector2.zero;
        dim.anchorMax = Vector2.one;
        dim.offsetMin = Vector2.zero;
        dim.offsetMax = Vector2.zero;
        Text head = MakeText("Title", transform, title, 44, TextAnchor.MiddleCenter);
        head.color = new Color(1f, 0.9f, 0.6f, 1f);
        SetRect(head.rectTransform, new Vector2(0f, 190f), new Vector2(1400f, 80f));

        for (int i = 0; i < _labels.Length; i++)
        {
            int idx = i;
            RectTransform row = MakeImage("Choice" + i, transform, Color.white, _sprite);
            SetRect(row, new Vector2(0f, 70f - i * 110f), new Vector2(1000f, 90f));
            _rows[i] = row.GetComponent<Image>();
            Button b = row.gameObject.AddComponent<Button>();
            b.transition = Selectable.Transition.None;
            b.navigation = new Navigation { mode = Navigation.Mode.None };
            b.onClick.AddListener(() => Choose(idx));
            EventTrigger trig = row.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry enter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            enter.callback.AddListener(_ => SetIndex(idx));
            trig.triggers.Add(enter);
            Text t = MakeText("Text", row, _labels[i], 34, TextAnchor.MiddleCenter);
            t.rectTransform.anchorMin = Vector2.zero;
            t.rectTransform.anchorMax = Vector2.one;
            t.rectTransform.offsetMin = Vector2.zero;
            t.rectTransform.offsetMax = Vector2.zero;
        }
        RefreshHighlight();
    }

    private static RectTransform MakeImage(string name, Transform parent, Color color, Sprite sprite)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        go.transform.SetParent(parent, false);
        Image img = go.GetComponent<Image>();
        img.color = color;
        img.sprite = sprite;
        img.type = sprite != null ? Image.Type.Sliced : Image.Type.Simple;
        return (RectTransform)go.transform;
    }

    private Text MakeText(string name, Transform parent, string text, int size, TextAnchor anchor)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
        go.transform.SetParent(parent, false);
        Text t = go.GetComponent<Text>();
        t.font = _font;
        t.fontSize = size;
        t.alignment = anchor;
        t.color = Color.white;
        t.text = text;
        t.raycastTarget = false;
        t.horizontalOverflow = HorizontalWrapMode.Overflow;
        t.verticalOverflow = VerticalWrapMode.Overflow;
        return t;
    }

    // 화면 중앙 기준 배치.
    private static void SetRect(RectTransform rt, Vector2 pos, Vector2 size)
    {
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos;
        rt.sizeDelta = size;
    }
}
