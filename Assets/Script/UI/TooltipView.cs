using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 공용 툴팁(UX-11). 호버(TooltipTarget)와 키보드·패드 포커스(호출 측이 Show) 모두 같은 입구를 쓴다.
/// 코드로 만드는 오버레이(씬 수정 없음). owner 가 맞을 때만 Hide 가 먹는다(두 곳이 번갈아 켜고 꺼도 서로 지우지 않게).
/// 문구는 호출 측이 준다(전투는 BattleTooltipText). TODO(I2): 고정 문구는 I2 키로.
/// </summary>
public sealed class TooltipView : MonoBehaviour
{
    private const float PadX = 14f;
    private const float PadY = 10f;
    private const float MaxWidth = 460f;

    private static TooltipView _instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _instance = null;
    }

    /// <summary>플레이 확인용: 지금 보이는 툴팁 글(없으면 빈 문자열).</summary>
    public static string DebugText
    {
        get { return _instance == null || !_instance._panel.gameObject.activeSelf ? string.Empty : _instance._text.text; }
    }

    public static void Show(object owner, string text, RectTransform anchor)
    {
        if (owner == null || anchor == null || string.IsNullOrEmpty(text))
        {
            Hide(owner);
            return;
        }
        Get().Place(owner, text, anchor);
    }

    public static void Hide(object owner)
    {
        if (_instance != null && ReferenceEquals(_instance._owner, owner))
        {
            _instance._owner = null;
            _instance._panel.gameObject.SetActive(false);
        }
    }

    /// <summary>모든 툴팁 닫기(패널이 사라질 때 owner 를 몰라도).</summary>
    public static void HideAll()
    {
        if (_instance != null)
        {
            _instance._owner = null;
            _instance._panel.gameObject.SetActive(false);
        }
    }

    internal static Font SharedFont()
    {
        Font font = null;
        Sprite unused;
        if (settingManager.Instance != null)
        {
            settingManager.Instance.GetButtonStyle(out font, out unused);
        }
        return font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
    }

    private static TooltipView Get()
    {
        if (_instance == null)
        {
            GameObject go = new GameObject("TooltipView");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<TooltipView>();
            _instance.Build();
        }
        return _instance;
    }

    private Canvas _canvas;
    private RectTransform _panel;
    private Text _text;
    private object _owner;
    private readonly Vector3[] _corners = new Vector3[4];

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void Build()
    {
        _canvas = gameObject.AddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        _canvas.sortingOrder = 90;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;

        GameObject panel = new GameObject("Panel", typeof(RectTransform), typeof(Image));
        panel.transform.SetParent(transform, false);
        _panel = (RectTransform)panel.transform;
        _panel.anchorMin = Vector2.zero;
        _panel.anchorMax = Vector2.zero;
        _panel.pivot = Vector2.zero;
        Image bg = panel.GetComponent<Image>();
        bg.color = new Color(0.05f, 0.05f, 0.08f, 0.94f);
        bg.raycastTarget = false;
        Outline outline = panel.AddComponent<Outline>();
        outline.effectColor = new Color(0.85f, 0.75f, 0.45f, 0.9f);
        outline.effectDistance = new Vector2(2f, -2f);

        GameObject label = new GameObject("Text", typeof(RectTransform), typeof(Text));
        label.transform.SetParent(panel.transform, false);
        _text = label.GetComponent<Text>();
        _text.font = SharedFont();
        _text.fontSize = 26;
        _text.color = new Color(0.95f, 0.93f, 0.85f, 1f);
        _text.alignment = TextAnchor.UpperLeft;
        _text.horizontalOverflow = HorizontalWrapMode.Wrap;
        _text.verticalOverflow = VerticalWrapMode.Overflow;
        _text.raycastTarget = false;
        RectTransform tr = (RectTransform)label.transform;
        tr.anchorMin = Vector2.zero;
        tr.anchorMax = Vector2.one;
        tr.offsetMin = new Vector2(PadX, PadY);
        tr.offsetMax = new Vector2(-PadX, -PadY);
        panel.SetActive(false);
    }

    private void Place(object owner, string text, RectTransform anchor)
    {
        _owner = owner;
        _text.text = text;
        float w = Mathf.Min(MaxWidth, _text.preferredWidth + PadX * 2f + 2f);
        TextGenerationSettings settings = _text.GetGenerationSettings(new Vector2(w - PadX * 2f, 0f));
        float h = _text.cachedTextGeneratorForLayout.GetPreferredHeight(text, settings) / _text.pixelsPerUnit + PadY * 2f;
        _panel.sizeDelta = new Vector2(w, h);
        anchor.GetWorldCorners(_corners);
        float scale = _canvas.scaleFactor > 0f ? _canvas.scaleFactor : 1f;
        float cx = (_corners[0].x + _corners[2].x) * 0.5f / scale;
        float bottom = _corners[0].y / scale;
        float top = _corners[2].y / scale;
        float screenW = Screen.width / scale;
        float screenH = Screen.height / scale;
        float x;
        float y;
        BattleTooltipText.Place(cx, top, bottom, w, h, screenW, screenH, out x, out y);
        _panel.anchoredPosition = new Vector2(x, y);
        _panel.gameObject.SetActive(true);
        _panel.SetAsLastSibling();
    }
}
