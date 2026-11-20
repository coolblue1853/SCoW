using SCoW.Core.Ui;
using System;
using SCoW.Core.Save;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 저장/불러오기 슬롯 창(자동 1 + 수동 3). 코드로 만드는 임시 오버레이라 씬 수정이 없다.
/// 입력: 마우스 클릭·호버, 키보드·패드는 GameInput(NavUp/NavDown/Interact/Cancel·Menu) + Enter. Advance 는 마우스 클릭을 포함해 쓰지 않는다.
/// 글꼴·스프라이트는 settingManager 의 기존 버튼에서 빌려 와 기존 UI 와 같은 모양을 쓴다.
/// </summary>
public sealed class SlotMenuView : MonoBehaviour
{
    public enum Mode
    {
        Save,
        Load,
    }

    private static SlotMenuView _current;

    public static bool IsOpen
    {
        get { return _current != null; }
    }

    private Mode _mode;
    private Action _onClosed;
    private Font _font;
    private Sprite _sprite;
    private readonly Image[] _rows = new Image[SaveService.SlotCount];
    private readonly Text[] _rowTexts = new Text[SaveService.SlotCount];
    private readonly SaveError[] _slotErrors = new SaveError[SaveService.SlotCount];
    private Text _message;
    private int _index;
    private int _confirmIndex = -1;
    private bool _closing;

    public static void Open(Mode mode, Action onClosed)
    {
        if (_current != null)
        {
            return;
        }
        GameObject go = new GameObject("SlotMenuView");
        SlotMenuView v = go.AddComponent<SlotMenuView>();
        v._mode = mode;
        v._onClosed = onClosed;
        v.Build();
        _current = v;
        UiGate.Push(PanelKind.SlotMenu);
    }

    private void OnDestroy()
    {
        UiGate.Remove(PanelKind.SlotMenu);
        if (_current == this)
        {
            _current = null;
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

        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 30;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        gameObject.AddComponent<GraphicRaycaster>();
        EnsureEventSystem();

        RectTransform dim = MakeImage("Dim", transform, new Color(0f, 0f, 0f, 0.75f), null);
        Stretch(dim);
        RectTransform panel = MakeImage("Panel", transform, new Color(0.12f, 0.1f, 0.09f, 0.96f), null);
        SetRect(panel, new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(1240f, 900f));

        string title = _mode == Mode.Save ? SaveText.Pick("저장하기", "Save Game") : SaveText.Pick("불러오기", "Load Game");
        Text t = MakeText("Title", panel, title, 44, TextAnchor.MiddleCenter);
        SetRect(t.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -54f), new Vector2(1100f, 70f));

        for (int i = 0; i < SaveService.SlotCount; i++)
        {
            int slot = i;
            RectTransform row = MakeImage("Slot" + i, panel, Color.white, _sprite);
            SetRect(row, new Vector2(0.5f, 1f), new Vector2(0f, -150f - i * 118f), new Vector2(1100f, 106f));
            Image img = row.GetComponent<Image>();
            _rows[i] = img;
            Button b = row.gameObject.AddComponent<Button>();
            b.transition = Selectable.Transition.None;
            b.navigation = new Navigation { mode = Navigation.Mode.None };
            b.onClick.AddListener(() => Activate(slot));
            EventTrigger trig = row.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry enter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            enter.callback.AddListener(_ => SetIndex(slot));
            trig.triggers.Add(enter);
            Text rt = MakeText("Text", row, string.Empty, 25, TextAnchor.MiddleLeft);
            Stretch(rt.rectTransform);
            rt.rectTransform.offsetMin = new Vector2(28f, 0f);
            rt.rectTransform.offsetMax = new Vector2(-28f, 0f);
            _rowTexts[i] = rt;
        }

        _message = MakeText("Message", panel, string.Empty, 28, TextAnchor.MiddleCenter);
        SetRect(_message.rectTransform, new Vector2(0.5f, 0f), new Vector2(0f, 100f), new Vector2(1100f, 70f));
        Text hint = MakeText("Hint", panel, SaveText.Pick("위아래: 선택   확인: 결정   취소: 닫기", "Up/Down: Select   Confirm: OK   Cancel: Close"), 24, TextAnchor.MiddleCenter);
        hint.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        SetRect(hint.rectTransform, new Vector2(0.5f, 0f), new Vector2(0f, 40f), new Vector2(1100f, 44f));

        Refresh();
        _index = FirstSelectable();
        RefreshHighlight();
    }

    private static void EnsureEventSystem()
    {
        if (EventSystem.current == null)
        {
            GameObject es = new GameObject("EventSystem(SlotMenu)");
            es.AddComponent<EventSystem>();
            es.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
        }
    }

    private bool IsSelectable(int slot)
    {
        return !(_mode == Mode.Save && slot == SaveService.AutoSlot);
    }

    private int FirstSelectable()
    {
        if (_mode == Mode.Save)
        {
            return 1;
        }
        int latest = SaveFlow.LatestSlot();
        return latest >= 0 ? latest : 0;
    }

    private void Refresh()
    {
        for (int i = 0; i < SaveService.SlotCount; i++)
        {
            SaveMeta m;
            SaveError e = SaveService.ReadMeta(i, out m);
            _slotErrors[i] = e;
            string body;
            if (e == SaveError.None)
            {
                body = SaveText.MetaLine(m);
            }
            else if (e == SaveError.Missing)
            {
                body = SaveText.Pick("비어 있음", "Empty");
            }
            else
            {
                body = SaveText.ErrorMessage(e);
            }
            _rowTexts[i].text = SaveText.SlotName(i) + "\n" + body;
            _rowTexts[i].color = IsSelectable(i) ? Color.white : new Color(1f, 1f, 1f, 0.4f);
        }
    }

    private void RefreshHighlight()
    {
        for (int i = 0; i < _rows.Length; i++)
        {
            _rows[i].color = i == _index ? new Color(1f, 0.85f, 0.45f, 1f) : new Color(0.75f, 0.7f, 0.65f, 1f);
        }
    }

    private void SetIndex(int slot)
    {
        if (IsSelectable(slot) && slot != _index)
        {
            _index = slot;
            _confirmIndex = -1;
            RefreshHighlight();
        }
    }

    private void Move(int dir)
    {
        int i = _index;
        for (int n = 0; n < _rows.Length; n++)
        {
            i = (i + dir + _rows.Length) % _rows.Length;
            if (IsSelectable(i))
            {
                SetIndex(i);
                return;
            }
        }
    }

    private void Update()
    {
        if (_closing)
        {
            return;
        }
        if (GameInput.NavUpDown)
        {
            Move(-1);
        }
        else if (GameInput.NavDownDown)
        {
            Move(1);
        }
        else if (GameInput.InteractDown || EnterPressed())
        {
            Activate(_index);
        }
        else if (UiGate.TryCancel(PanelKind.SlotMenu, true))
        {
            Close();
        }
    }

    private static bool EnterPressed()
    {
        UnityEngine.InputSystem.Keyboard kb = UnityEngine.InputSystem.Keyboard.current;
        return kb != null && (kb.enterKey.wasPressedThisFrame || kb.numpadEnterKey.wasPressedThisFrame);
    }

    private void Activate(int slot)
    {
        if (_closing || !IsSelectable(slot))
        {
            return;
        }
        if (_mode == Mode.Save)
        {
            DoSave(slot);
        }
        else
        {
            DoLoad(slot);
        }
    }

    private void DoSave(int slot)
    {
        string ko;
        string en;
        if (!SaveFlow.CanSave(out ko, out en))
        {
            Say(SaveText.Pick(ko, en), true);
            return;
        }
        if (_slotErrors[slot] != SaveError.Missing && _confirmIndex != slot)
        {
            _confirmIndex = slot;
            Say(SaveText.Pick("이미 저장이 있습니다. 한 번 더 선택하면 덮어씁니다.", "A save exists. Select again to overwrite."), false);
            return;
        }
        _confirmIndex = -1;
        SaveError e = SaveFlow.Save(slot);
        if (e == SaveError.None)
        {
            Refresh();
            Say(SaveText.Pick("저장했습니다.", "Saved."), false);
        }
        else
        {
            Say(SaveText.ErrorMessage(e), true);
        }
    }

    private void DoLoad(int slot)
    {
        SaveError known = _slotErrors[slot];
        if (known == SaveError.Missing || known == SaveError.NewerVersion)
        {
            Say(SaveText.ErrorMessage(known), true);
            return;
        }
        bool inGame = SaveFlow.InMainScene;
        if (inGame && _confirmIndex != slot)
        {
            _confirmIndex = slot;
            Say(SaveText.Pick("저장하지 않은 진행은 사라집니다. 한 번 더 선택하면 불러옵니다.", "Unsaved progress will be lost. Select again to load."), false);
            return;
        }
        SaveError e = SaveFlow.LoadAndEnter(slot);
        if (e != SaveError.None)
        {
            Refresh();
            Say(SaveText.ErrorMessage(e), true);
            return;
        }
        _closing = true;
        if (settingManager.Instance != null)
        {
            settingManager.Instance.HideForLoad();
        }
        Destroy(gameObject);
    }

    private void Say(string text, bool error)
    {
        _message.text = text;
        _message.color = error ? new Color(1f, 0.45f, 0.4f, 1f) : new Color(0.85f, 0.95f, 0.8f, 1f);
    }

    public void Close()
    {
        if (_closing)
        {
            return;
        }
        _closing = true;
        Action cb = _onClosed;
        Destroy(gameObject);
        if (cb != null)
        {
            cb();
        }
    }

    private RectTransform MakeImage(string name, Transform parent, Color color, Sprite sprite)
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
        t.horizontalOverflow = HorizontalWrapMode.Wrap;
        t.verticalOverflow = VerticalWrapMode.Overflow;
        return t;
    }

    private static void Stretch(RectTransform rt)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    private static void SetRect(RectTransform rt, Vector2 anchor, Vector2 pos, Vector2 size)
    {
        rt.anchorMin = anchor;
        rt.anchorMax = anchor;
        rt.pivot = new Vector2(0.5f, anchor.y == 0f ? 0f : (anchor.y == 1f ? 1f : 0.5f));
        rt.anchoredPosition = pos;
        rt.sizeDelta = size;
    }
}
