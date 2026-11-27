using System.Collections.Generic;
using DG.Tweening;
using SCoW.Core.Keyword;
using SCoW.Core.State;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 새 키워드 알림(KEYWORD-1 K4, View). 상태 변경 통지(GameRoot.StateChanged)를 구독해 새로 풀린 키워드를 KeywordNotice(Core)로 가려내고,
/// 우상단 토스트("New keyword: …")와 수첩 버튼 배지를 보여 준다. 코드로 만드는 오버레이라 씬 수정이 없다(KeywordPicker 와 같은 방식).
/// 대사·판정 등 연출 중(UiGate.IsBusy)이거나 설정·수첩 등 패널이 열려 있는(UiGate.AnyOpen) 동안 얻은 알림은 큐에 쌓았다가 끝나면 한 개씩 띄운다. 폴링하지 않는다(큐가 비어 있으면 Update 는 바로 반환).
/// 그래픽·효과음은 임시(ART-2). TODO(I2): 문구를 새 I2 키로 옮긴다.
/// </summary>
public sealed class KeywordToast : MonoBehaviour
{
    private const float FadeIn = 0.2f;
    private const float Hold = 1.5f;
    private const float FadeOut = 0.3f;

    private static KeywordToast _instance;

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
        GameObject go = new GameObject("KeywordToast");
        Persist.Keep(go);
        go.AddComponent<KeywordToast>();
    }

    /// <summary>수첩을 열었다: 배지만 끈다(선택 창 NEW 는 그대로).</summary>
    public static void OnIntelOpened()
    {
        if (GameRoot.Instance != null)
        {
            KeywordNotice.AckBadge(GameRoot.Instance.State);
        }
    }

    private readonly Queue<string> _queue = new Queue<string>();
    private CanvasGroup _group;
    private TextMeshProUGUI _text;
    private Sequence _seq;
    private bool _showing;
    private bool _syncing;
    private GameObject _badge;
    private bool _hostSearched;
    private TextMeshProUGUI _badgeText;

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

    private void OnEnable()
    {
        GameRoot.StateChanged += OnState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SyncNow();
    }

    private void OnDisable()
    {
        GameRoot.StateChanged -= OnState;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (_seq != null)
        {
            _seq.Kill();
            _seq = null;
        }
        _showing = false;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _badge = null;
        _hostSearched = false;
        RefreshBadge();
    }

    private void OnState(string key)
    {
        if (_syncing)
        {
            return;
        }
        if (KeywordNotice.IsNoticeKey(key))
        {
            if (key == KeywordNotice.SeqKey || key == KeywordNotice.BadgeKey)
            {
                RefreshBadge();
            }
            return;
        }
        SyncNow();
    }

    // 새로 풀린 키워드를 찾아 큐에 쌓고 배지를 갱신한다. Sync 가 상태를 바꾸면 다시 통지가 오므로 _syncing 으로 막는다.
    private void SyncNow()
    {
        KeywordBook book = KeywordPicker.GetBook();
        if (book == null || GameRoot.Instance == null)
        {
            return;
        }
        List<KeywordEntry> fresh;
        _syncing = true;
        try
        {
            fresh = KeywordNotice.Sync(book, GameRoot.Instance.State);
        }
        finally
        {
            _syncing = false;
        }
        if (fresh.Count > 0)
        {
            _queue.Enqueue(MakeMessage(fresh));
        }
        RefreshBadge();
    }

    // 한 번에 여러 개가 풀리면 첫 이름 + 개수로 줄인다. TODO(I2)
    private static string MakeMessage(List<KeywordEntry> fresh)
    {
        if (fresh.Count == 1)
        {
            return "New keyword: " + fresh[0].Id;
        }
        return "New keywords: " + fresh[0].Id + " +" + (fresh.Count - 1);
    }

    private void Update()
    {
        if (!KeywordNotice.CanShowToast(_showing, _queue.Count, UiGate.IsBusy, UiGate.AnyOpen))
        {
            return;
        }
        Show(_queue.Dequeue());
    }

    private void Show(string message)
    {
        _showing = true;
        if (DialogManager.Instance != null && DialogManager.Instance.DialogFont != null)
        {
            _text.font = DialogManager.Instance.DialogFont;
        }
        _text.text = message;
        _group.alpha = 0f;
        if (_seq != null)
        {
            _seq.Kill();
        }
        _seq = DOTween.Sequence();
        _seq.Append(_group.DOFade(1f, FadeIn));
        _seq.AppendInterval(Hold);
        _seq.Append(_group.DOFade(0f, FadeOut));
        _seq.OnComplete(() => _showing = false);
        _seq.SetLink(gameObject);
    }

    // ---- 수첩 배지 ----

    private void RefreshBadge()
    {
        if (GameRoot.Instance == null)
        {
            return;
        }
        int n = KeywordNotice.BadgeCount(GameRoot.Instance.State);
        if (n > 0 && _badge == null && !_hostSearched)
        {
            _hostSearched = true;
            FindHostAndMakeBadge();
        }
        if (_badge == null)
        {
            return;
        }
        _badge.SetActive(n > 0);
        if (n > 0)
        {
            _badgeText.text = n > 9 ? "9+" : n.ToString();
        }
    }

    // 수첩 버튼 = IntelManager.OpenSetting 을 부르는 씬 버튼. 씬이 바뀌면 다시 찾는다.
    private void FindHostAndMakeBadge()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (IsIntelButton(buttons[i]))
            {
                MakeBadge(buttons[i].transform);
                return;
            }
        }
    }

    private static bool IsIntelButton(Button b)
    {
        int count = b.onClick.GetPersistentEventCount();
        for (int i = 0; i < count; i++)
        {
            if (b.onClick.GetPersistentMethodName(i) == "OpenSetting" && b.onClick.GetPersistentTarget(i) is IntelManager)
            {
                return true;
            }
        }
        return false;
    }

    private void MakeBadge(Transform host)
    {
        GameObject go = new GameObject("KeywordBadge", typeof(RectTransform), typeof(Image));
        go.transform.SetParent(host, false);
        RectTransform rt = (RectTransform)go.transform;
        rt.anchorMin = new Vector2(1f, 1f);
        rt.anchorMax = new Vector2(1f, 1f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = new Vector2(-8f, -8f);
        rt.sizeDelta = new Vector2(34f, 34f);
        Image img = go.GetComponent<Image>();
        img.color = new Color(0.85f, 0.15f, 0.15f, 1f);
        img.raycastTarget = false;
        _badgeText = MakeText("Count", go.transform, "1", 22, TextAlignmentOptions.Center);
        Stretch(_badgeText.rectTransform);
        _badge = go;
    }

    // ---- 토스트 화면(임시 그래픽) ----

    private void Build()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 40;
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
        rt.anchoredPosition = new Vector2(-40f, -40f);
        rt.sizeDelta = new Vector2(800f, 76f);
        Image bg = panel.GetComponent<Image>();
        bg.color = new Color(0.1f, 0.09f, 0.08f, 0.92f);
        bg.raycastTarget = false;
        _group = panel.GetComponent<CanvasGroup>();
        _group.alpha = 0f;
        _group.blocksRaycasts = false;
        _group.interactable = false;

        _text = MakeText("Label", panel.transform, string.Empty, 30, TextAlignmentOptions.Center);
        _text.color = new Color(1f, 0.85f, 0.4f, 1f);
        _text.textWrappingMode = TextWrappingModes.NoWrap;
        Stretch(_text.rectTransform);
    }

    private TextMeshProUGUI MakeText(string name, Transform parent, string text, int size, TextAlignmentOptions align)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        TextMeshProUGUI t = go.AddComponent<TextMeshProUGUI>();
        TMP_FontAsset font = DialogManager.Instance != null ? DialogManager.Instance.DialogFont : null;
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
}
