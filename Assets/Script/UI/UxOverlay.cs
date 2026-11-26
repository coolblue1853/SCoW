using SCoW.Core.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 공통 UI 뼈대 호스트(UX-1). DontDestroyOnLoad 오버레이 하나가 (1) 프레임 맨 앞에서 Esc/B 로 최상단 패널만 닫고(UiGate.PumpCancel),
/// (2) 하단 키 안내(HintBar)와 (3) 포커스 링을 그린다. 코드로 만들어 씬 수정이 없다. 그래픽은 임시(ART-2 교체).
/// 실행 순서를 가장 앞으로 둬서, 이번 프레임에 닫힌 Cancel 을 다른 패널이 다시 읽지 못하게(UiGate.CancelConsumedThisFrame) 한다.
/// </summary>
[DefaultExecutionOrder(-1000)]
public sealed class UxOverlay : MonoBehaviour
{
    private const float RingPad = 6f;
    private const float RingThick = 4f;

    private static UxOverlay _instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _instance = null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Boot()
    {
        if (_instance != null)
        {
            return;
        }
        GameObject go = new GameObject("UxOverlay");
        Persist.Keep(go);
        go.AddComponent<UxOverlay>();
    }

    private static readonly Vector3[] Corners = new Vector3[4];

    private RectTransform _root;
    private GameObject _barRoot;
    private TextMeshProUGUI _barText;
    private int _barVersion = -1;
    private InputDeviceKind _barDevice;
    private RectTransform[] _ring;
    private GameObject _ringRoot;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        Build();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void Update()
    {
        UiGate.PumpCancel();
        RefreshBar();
        RefreshRing();
    }

    private void RefreshBar()
    {
        HintStack stack = HintBar.Stack;
        bool show = stack.Count > 0 && SettingsService.Current.ShowHints;
        if (_barRoot.activeSelf != show)
        {
            _barRoot.SetActive(show);
        }
        if (!show)
        {
            _barVersion = -1;
            return;
        }
        InputDeviceKind device = GameInput.LastDevice;
        if (stack.Version == _barVersion && device == _barDevice)
        {
            return;
        }
        _barVersion = stack.Version;
        _barDevice = device;
        if (_barText.font == null || _barText.font == TMP_Settings.defaultFontAsset)
        {
            TMP_FontAsset font = DialogManager.Instance != null ? DialogManager.Instance.DialogFont : null;
            if (font != null)
            {
                _barText.font = font;
            }
        }
        _barText.text = stack.Format(device);
    }

    private void RefreshRing()
    {
        EventSystem es = EventSystem.current;
        GameObject sel = es != null ? es.currentSelectedGameObject : null;
        RectTransform target = FocusScope.Target;
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            target = sel != null && sel.activeInHierarchy ? sel.transform as RectTransform : null;
        }
        bool show = target != null && GameInput.LastDevice != InputDeviceKind.Mouse;
        if (_ringRoot.activeSelf != show)
        {
            _ringRoot.SetActive(show);
        }
        if (!show)
        {
            return;
        }
        Canvas canvas = target.GetComponentInParent<Canvas>();
        Camera cam = canvas != null && canvas.rootCanvas.renderMode != RenderMode.ScreenSpaceOverlay ? canvas.rootCanvas.worldCamera : null;
        target.GetWorldCorners(Corners);
        Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 max = new Vector2(float.MinValue, float.MinValue);
        for (int i = 0; i < 4; i++)
        {
            Vector2 screen = RectTransformUtility.WorldToScreenPoint(cam, Corners[i]);
            Vector2 local;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_root, screen, null, out local);
            min = Vector2.Min(min, local);
            max = Vector2.Max(max, local);
        }
        min -= new Vector2(RingPad, RingPad);
        max += new Vector2(RingPad, RingPad);
        Vector2 size = max - min;
        Vector2 center = (min + max) * 0.5f;
        Place(_ring[0], new Vector2(center.x, max.y), new Vector2(size.x, RingThick)); // 위
        Place(_ring[1], new Vector2(center.x, min.y), new Vector2(size.x, RingThick)); // 아래
        Place(_ring[2], new Vector2(min.x, center.y), new Vector2(RingThick, size.y)); // 왼쪽
        Place(_ring[3], new Vector2(max.x, center.y), new Vector2(RingThick, size.y)); // 오른쪽
    }

    private static void Place(RectTransform rt, Vector2 pos, Vector2 size)
    {
        rt.anchoredPosition = pos;
        rt.sizeDelta = size;
    }

    private void Build()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 300;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        _root = (RectTransform)transform;

        _barRoot = new GameObject("HintBar", typeof(RectTransform), typeof(Image));
        _barRoot.transform.SetParent(transform, false);
        RectTransform bar = (RectTransform)_barRoot.transform;
        bar.anchorMin = new Vector2(0.5f, 0f);
        bar.anchorMax = new Vector2(0.5f, 0f);
        bar.pivot = new Vector2(0.5f, 0f);
        bar.anchoredPosition = new Vector2(0f, 14f);
        bar.sizeDelta = new Vector2(1500f, 44f);
        Image bg = _barRoot.GetComponent<Image>();
        bg.color = new Color(0.05f, 0.05f, 0.05f, 0.7f);
        bg.raycastTarget = false;

        GameObject textGo = new GameObject("Text", typeof(RectTransform));
        textGo.transform.SetParent(_barRoot.transform, false);
        _barText = textGo.AddComponent<TextMeshProUGUI>();
        _barText.fontSize = 24;
        _barText.alignment = TextAlignmentOptions.Center;
        _barText.color = new Color(0.85f, 0.85f, 0.85f, 1f);
        _barText.textWrappingMode = TextWrappingModes.NoWrap;
        _barText.raycastTarget = false;
        RectTransform tr = _barText.rectTransform;
        tr.anchorMin = Vector2.zero;
        tr.anchorMax = Vector2.one;
        tr.offsetMin = Vector2.zero;
        tr.offsetMax = Vector2.zero;
        _barRoot.SetActive(false);

        _ringRoot = new GameObject("FocusRing", typeof(RectTransform));
        _ringRoot.transform.SetParent(transform, false);
        _ring = new RectTransform[4];
        for (int i = 0; i < 4; i++)
        {
            GameObject edge = new GameObject("Edge" + i, typeof(RectTransform), typeof(Image));
            edge.transform.SetParent(_ringRoot.transform, false);
            RectTransform rt = (RectTransform)edge.transform;
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            Image img = edge.GetComponent<Image>();
            img.color = new Color(1f, 0.85f, 0.3f, 1f);
            img.raycastTarget = false;
            _ring[i] = rt;
        }
        _ringRoot.SetActive(false);
    }
}
