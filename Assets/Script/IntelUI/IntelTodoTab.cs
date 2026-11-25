using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 수첩 "할 일" 탭(UX-3). 기존 사건·장소·인물 탭 옆에 코드로 탭 버튼과 목록 창을 덧붙인다(씬 수정 없음).
/// 목록은 ObjectiveBoard 의 현재 목표 전부. 열면 기존 세 창을 잠시 가렸다가, 수첩을 닫거나 같은 키로 다시 누르면 원래 창을 되돌린다(다른 탭을 누르면 그 탭이 직접 켠다).
/// 마우스는 탭 클릭, 키보드·패드는 TodoTab 액션(Q / 패드 오른쪽 스틱 누름). 그래픽은 임시(ART-2). TODO(I2): 문구.
/// </summary>
public static class IntelTodoTab
{
    private static readonly Vector2 TabPos = new Vector2(60f, 184.35f);
    private static readonly Vector2 TabSize = new Vector2(92f, 62f);
    private static readonly Color TabOff = new Color(0.36f, 0.27f, 0.18f, 1f);
    private static readonly Color TabOn = new Color(0.62f, 0.46f, 0.26f, 1f);

    private static GameObject _tab;
    private static Image _tabImage;
    private static GameObject _panel;
    private static TextMeshProUGUI _text;
    private static bool _open;
    private static bool _hadEvent, _hadChar, _hadPlace;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _tab = null;
        _tabImage = null;
        _panel = null;
        _text = null;
        _open = false;
    }

    /// <summary>할 일 창이 열려 있나(검증용).</summary>
    public static bool IsOpen
    {
        get { return _open; }
    }

    /// <summary>할 일 창에 지금 그려진 글(검증용).</summary>
    public static string ShownText
    {
        get { return _text != null ? _text.text : string.Empty; }
    }

    /// <summary>수첩을 열 때 한 번 호출. 탭·창이 없으면 만든다.</summary>
    public static void Ensure(IntelManager im)
    {
        if (_tab != null && _panel != null)
        {
            return;
        }
        if (_tab != null)
        {
            Object.Destroy(_tab);
        }
        if (_panel != null)
        {
            Object.Destroy(_panel);
        }
        Transform sprite = im.intelUI.transform.Find("Sprite");
        Transform tabParent = sprite != null ? sprite : im.intelUI.transform;

        _tab = new GameObject("TodoTab", typeof(RectTransform), typeof(Image), typeof(Button));
        _tab.transform.SetParent(tabParent, false);
        RectTransform rt = (RectTransform)_tab.transform;
        rt.anchoredPosition = TabPos;
        rt.sizeDelta = TabSize;
        _tabImage = _tab.GetComponent<Image>();
        _tabImage.color = TabOff;
        _tab.GetComponent<Button>().onClick.AddListener(() => Toggle(im));
        TextMeshProUGUI label = MakeText(_tab.transform, "Label", 18, TextAlignmentOptions.Center);
        label.textWrappingMode = TextWrappingModes.NoWrap;
        label.text = "To-Do";
        label.color = Color.white;
        Stretch(label.rectTransform, 0f);

        _panel = new GameObject("TodoPanel", typeof(RectTransform), typeof(Image));
        _panel.transform.SetParent(im.intelUI.transform, false);
        RectTransform prt = (RectTransform)_panel.transform;
        prt.anchoredPosition = new Vector2(31f, -15f);
        prt.sizeDelta = new Vector2(600f, 340f);
        Image bg = _panel.GetComponent<Image>();
        bg.color = new Color(0.86f, 0.80f, 0.68f, 1f);
        _text = MakeText(_panel.transform, "Text", 20, TextAlignmentOptions.TopLeft);
        _text.enableAutoSizing = true;
        _text.fontSizeMin = 10f;
        _text.fontSizeMax = 20f;
        _text.color = new Color(0.12f, 0.08f, 0.04f, 1f);
        Stretch(_text.rectTransform, 24f);
        _panel.SetActive(false);
        _open = false;
    }

    private static TextMeshProUGUI MakeText(Transform parent, string name, float size, TextAlignmentOptions align)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        TextMeshProUGUI t = go.AddComponent<TextMeshProUGUI>();
        t.fontSize = size;
        t.alignment = align;
        t.textWrappingMode = TextWrappingModes.Normal;
        t.raycastTarget = false;
        if (DialogManager.Instance != null && DialogManager.Instance.DialogFont != null)
        {
            t.font = DialogManager.Instance.DialogFont;
        }
        return t;
    }

    private static void Stretch(RectTransform rt, float pad)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = new Vector2(pad, pad);
        rt.offsetMax = new Vector2(-pad, -pad);
    }

    public static void Toggle(IntelManager im)
    {
        if (_open)
        {
            Hide(im, true);
        }
        else
        {
            Show(im);
        }
    }

    public static void Show(IntelManager im)
    {
        Ensure(im);
        if (!_open)
        {
            _hadEvent = im.eventWindow.activeSelf;
            _hadChar = im.characterWindow.activeSelf;
            _hadPlace = im.placeWindow.activeSelf;
        }
        im.eventWindow.SetActive(false);
        im.characterWindow.SetActive(false);
        im.placeWindow.SetActive(false);
        im.eventWindowOn.SetActive(false);
        im.characterWindowOn.SetActive(false);
        im.placeWindowOn.SetActive(false);
        SoundManager.Instance.ClickSoundPlay();
        _open = true;
        _tabImage.color = TabOn;
        Refresh();
        _panel.SetActive(true);
    }

    /// <summary>할 일 창을 닫는다. restore 가 true 면 열기 전 창을 되돌린다(수첩 닫기). 다른 탭이 열릴 때는 false(그 탭이 직접 켠다).</summary>
    public static void Hide(IntelManager im, bool restore)
    {
        if (!_open)
        {
            return;
        }
        _open = false;
        _panel.SetActive(false);
        _tabImage.color = TabOff;
        if (restore)
        {
            im.eventWindow.SetActive(_hadEvent);
            im.characterWindow.SetActive(_hadChar);
            im.placeWindow.SetActive(_hadPlace);
            im.eventWindowOn.SetActive(_hadEvent);
            im.characterWindowOn.SetActive(_hadChar);
            im.placeWindowOn.SetActive(_hadPlace);
        }
    }

    private static void Refresh()
    {
        List<string> lines = ObjectiveBoard.AllLines();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(ObjectiveBoard.Pick("할 일", "To-Do")).Append("\n");
        int now = DataBaseManager.timeCount;
        // TODO(I2): 날짜 줄 ko/en 은 WorldClock.DateLine 이 직접 만든다. I2 키로 옮기면 Pick 두 번 호출을 없앤다(할 일 제목 줄과 같은 임시 방식).
        sb.Append(ObjectiveBoard.Pick(SCoW.Core.World.WorldClock.DateLine(now, true), SCoW.Core.World.WorldClock.DateLine(now, false))).Append("\n\n");
        if (lines.Count == 0)
        {
            sb.Append(ObjectiveBoard.Pick("지금 할 일이 없다.", "Nothing to do right now."));
        }
        for (int i = 0; i < lines.Count; i++)
        {
            sb.Append("- ").Append(lines[i]).Append("\n");
        }
        _text.text = sb.ToString();
    }
}
