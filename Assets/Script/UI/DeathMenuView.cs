using SCoW.Core.Save;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 사망(HP·SAN 0) 화면 3지선다(UX-5): 마지막 저장에서 재개 / 타이틀 / 종료. 선택지 구성과 "마지막 저장" 고르기는 Core ResumeRules.
/// 코드로 만드는 임시 오버레이(씬 수정 없음). 입력: 마우스, NavUp/NavDown·Interact·Enter(키보드·패드). 문구는 임시, TODO(I2).
/// DirectingManager 가 사망 여부에 따라 Show/Hide 를 매 프레임 불러도 안전하다(이미 떠 있으면 무시).
/// </summary>
public sealed class DeathMenuView : MonoBehaviour
{
    private static DeathMenuView _current;

    public static bool IsOpen
    {
        get { return _current != null; }
    }

    /// <summary>현재 선택지(플레이 확인용). 열려 있지 않으면 null.</summary>
    public static DeathChoice[] CurrentChoices
    {
        get { return _current != null ? _current._choices : null; }
    }

    public static void Show()
    {
        if (_current != null || SceneRouter.IsActive(SceneIds.Insert))
        {
            return;
        }
        GameObject go = new GameObject("DeathMenuView");
        DeathMenuView v = go.AddComponent<DeathMenuView>();
        _current = v;
        v._openedFrame = Time.frameCount;
        v.Build();
        UiGate.Push(PanelKind.Death, () => { }, false);
        HintBar.Push(v, DeathHints);
    }

    public static void Hide()
    {
        if (_current != null)
        {
            Destroy(_current.gameObject);
        }
    }

    private static readonly Hint[] DeathHints =
    {
        new Hint(HintAction.Navigate, "Select"),
        new Hint(HintAction.Confirm, "Confirm"),
    };

    private DeathChoice[] _choices;
    private Image[] _rows;
    private Text _message;
    private Font _font;
    private Sprite _sprite;
    private int _index;
    private int _openedFrame;
    private int _resumeSlot;
    private bool _busy;

    private void OnDestroy()
    {
        UiGate.Remove(PanelKind.Death);
        HintBar.Remove(this);
        if (_current == this)
        {
            _current = null;
        }
    }

    private void Update()
    {
        if (_busy || Time.frameCount == _openedFrame)
        {
            return;
        }
        if (GameInput.NavUpDown)
        {
            SetIndex((_index - 1 + _choices.Length) % _choices.Length);
        }
        else if (GameInput.NavDownDown)
        {
            SetIndex((_index + 1) % _choices.Length);
        }
        else if (GameInput.InteractDown || EnterPressed())
        {
            Activate(_index);
        }
    }

    private static bool EnterPressed()
    {
        UnityEngine.InputSystem.Keyboard kb = UnityEngine.InputSystem.Keyboard.current;
        return kb != null && (kb.enterKey.wasPressedThisFrame || kb.numpadEnterKey.wasPressedThisFrame);
    }

    private void Activate(int i)
    {
        if (_busy)
        {
            return;
        }
        switch (_choices[i])
        {
            case DeathChoice.ResumeLast:
                SaveError e = SaveFlow.LoadAndEnter(_resumeSlot);
                if (e != SaveError.None)
                {
                    _message.text = SaveText.ErrorMessage(e);
                    return;
                }
                _busy = true;
                break;
            case DeathChoice.Title:
                _busy = true;
                if (DataBaseManager.instance != null)
                {
                    DataBaseManager.instance.ResetData();
                }
                SceneRouter.Go(SceneIds.Insert);
                break;
            case DeathChoice.Quit:
                _busy = true;
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                break;
        }
    }

    private static string Label(DeathChoice c)
    {
        switch (c)
        {
            case DeathChoice.ResumeLast: return SaveText.Pick("마지막 저장에서 재개", "Resume from last save");
            case DeathChoice.Title: return SaveText.Pick("타이틀로", "Back to title");
            default: return SaveText.Pick("종료", "Quit");
        }
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

    private void Build()
    {
        if (settingManager.Instance != null)
        {
            settingManager.Instance.GetButtonStyle(out _font, out _sprite);
        }
        if (_font == null)
        {
            _font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        }
        _resumeSlot = SaveFlow.LatestResumeSlot();
        _choices = ResumeRules.DeathChoices(_resumeSlot >= 0);
        _rows = new Image[_choices.Length];

        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 60;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        gameObject.AddComponent<GraphicRaycaster>();
        if (EventSystem.current == null)
        {
            GameObject es = new GameObject("EventSystem(Death)");
            es.AddComponent<EventSystem>();
            es.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
        }

        RectTransform dim = MakeImage("Dim", transform, new Color(0f, 0f, 0f, 0.85f), null);
        dim.anchorMin = Vector2.zero;
        dim.anchorMax = Vector2.one;
        dim.offsetMin = Vector2.zero;
        dim.offsetMax = Vector2.zero;
        Text title = MakeText("Title", transform, "You Died!", 96, TextAnchor.MiddleCenter);
        title.color = new Color(0.8f, 0.12f, 0.1f, 1f);
        SetRect(title.rectTransform, new Vector2(0f, 260f), new Vector2(1200f, 140f));

        for (int i = 0; i < _choices.Length; i++)
        {
            int idx = i;
            RectTransform row = MakeImage("Choice" + i, transform, Color.white, _sprite);
            SetRect(row, new Vector2(0f, 80f - i * 120f), new Vector2(760f, 96f));
            _rows[i] = row.GetComponent<Image>();
            Button b = row.gameObject.AddComponent<Button>();
            b.transition = Selectable.Transition.None;
            b.navigation = new Navigation { mode = Navigation.Mode.None };
            b.onClick.AddListener(() => Activate(idx));
            EventTrigger trig = row.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry enter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            enter.callback.AddListener(_ => SetIndex(idx));
            trig.triggers.Add(enter);
            Text t = MakeText("Text", row, Label(_choices[i]), 36, TextAnchor.MiddleCenter);
            t.rectTransform.anchorMin = Vector2.zero;
            t.rectTransform.anchorMax = Vector2.one;
            t.rectTransform.offsetMin = Vector2.zero;
            t.rectTransform.offsetMax = Vector2.zero;
        }
        _message = MakeText("Message", transform, string.Empty, 28, TextAnchor.MiddleCenter);
        _message.color = new Color(1f, 0.45f, 0.4f, 1f);
        SetRect(_message.rectTransform, new Vector2(0f, -330f), new Vector2(1200f, 60f));
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
