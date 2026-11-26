using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 저장 알림 토스트(UX-5, View). 자동 저장 성공은 짧게, 실패는 빨간 문구로 길게 보인다. KeywordToast 바로 아래 칸을 써서 겹치지 않는다.
/// 코드로 만드는 오버레이(씬 수정 없음). 글꼴은 슬롯 창과 같은 UI 글꼴(한글 글리프 있음; 대사용 TMP 글꼴에는 없다). 그래픽은 임시(ART-2). TODO(I2): 문구를 새 I2 키로 옮긴다.
/// </summary>
public sealed class SaveToast : MonoBehaviour
{
    private const float FadeIn = 0.15f;
    private const float HoldOk = 1.2f;
    private const float HoldError = 3.5f;
    private const float FadeOut = 0.3f;

    private static SaveToast _instance;

    /// <summary>마지막으로 띄운 문구(플레이 확인용).</summary>
    public static string LastMessage { get; private set; }

    /// <summary>지금까지 띄운 횟수(플레이 확인용).</summary>
    public static int ShownCount { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _instance = null;
        LastMessage = null;
        ShownCount = 0;
    }

    public static void Show(string message, bool error)
    {
        LastMessage = message;
        ShownCount++;
        if (_instance == null)
        {
            GameObject go = new GameObject("SaveToast");
            Persist.Keep(go);
            _instance = go.AddComponent<SaveToast>();
            _instance.Build();
        }
        _instance.Play(message, error);
    }

    private CanvasGroup _group;
    private Text _text;
    private Sequence _seq;

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void Play(string message, bool error)
    {
        _text.text = message;
        _text.color = error ? new Color(1f, 0.45f, 0.4f, 1f) : new Color(0.75f, 1f, 0.75f, 1f);
        if (_seq != null)
        {
            _seq.Kill();
        }
        _group.alpha = 0f;
        _seq = DOTween.Sequence();
        _seq.Append(_group.DOFade(1f, FadeIn));
        _seq.AppendInterval(error ? HoldError : HoldOk);
        _seq.Append(_group.DOFade(0f, FadeOut));
        _seq.SetLink(gameObject);
    }

    private void Build()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 41;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;

        GameObject panel = new GameObject("Toast", typeof(RectTransform), typeof(Image), typeof(CanvasGroup));
        panel.transform.SetParent(transform, false);
        RectTransform rt = (RectTransform)panel.transform;
        rt.anchorMin = new Vector2(1f, 1f);
        rt.anchorMax = new Vector2(1f, 1f);
        rt.pivot = new Vector2(1f, 1f);
        rt.anchoredPosition = new Vector2(-40f, -130f);
        rt.sizeDelta = new Vector2(800f, 64f);
        Image bg = panel.GetComponent<Image>();
        bg.color = new Color(0.1f, 0.09f, 0.08f, 0.92f);
        bg.raycastTarget = false;
        _group = panel.GetComponent<CanvasGroup>();
        _group.alpha = 0f;
        _group.blocksRaycasts = false;
        _group.interactable = false;

        GameObject label = new GameObject("Label", typeof(RectTransform));
        label.transform.SetParent(panel.transform, false);
        _text = label.AddComponent<Text>();
        Font font = null;
        Sprite unused;
        if (settingManager.Instance != null)
        {
            settingManager.Instance.GetButtonStyle(out font, out unused);
        }
        _text.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        _text.fontSize = 28;
        _text.alignment = TextAnchor.MiddleCenter;
        _text.horizontalOverflow = HorizontalWrapMode.Overflow;
        _text.raycastTarget = false;
        RectTransform lr = _text.rectTransform;
        lr.anchorMin = Vector2.zero;
        lr.anchorMax = Vector2.one;
        lr.offsetMin = Vector2.zero;
        lr.offsetMax = Vector2.zero;
    }
}
