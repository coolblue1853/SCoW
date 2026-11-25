using DG.Tweening;
using SCoW.Core.State;
using SCoW.Core.World;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 시간·목표 HUD(UX-3, View). Main 씬 왼쪽 위에 "N일차 시간대 · 오늘 칸 점 4개 · 마감까지 M칸 · 목표 한 줄"을 그린다.
/// 값은 상태 변경 통지(GameRoot.StateChanged)로만 다시 계산한다(매 프레임 폴링 없음, Update 는 표시 여부 bool 과 dirty 플래그만 본다).
/// timeCount 가 올라가면 패널을 한 번 펀치하고 글자를 잠깐 강조한다. 코드로 만드는 오버레이라 씬 수정이 없다(KeywordToast 와 같은 방식).
/// 그래픽·효과음은 임시(ART-2). TODO(I2): 문구를 새 I2 키로 옮긴다.
/// </summary>
public sealed class TimeHudView : MonoBehaviour
{
    private const int PipCount = WorldClock.PhasesPerDay;
    private const float PunchTime = 0.3f;
    private const float FlashTime = 0.9f;

    private static readonly Color PipOn = new Color(1f, 0.85f, 0.35f, 1f);
    private static readonly Color PipOff = new Color(1f, 1f, 1f, 0.25f);
    private static readonly Color Normal = new Color(0.92f, 0.92f, 0.92f, 1f);
    private static readonly Color Flash = new Color(1f, 0.85f, 0.35f, 1f);
    private static readonly Color Danger = new Color(1f, 0.45f, 0.4f, 1f);

    private static TimeHudView _instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _instance = null;
        PunchCount = 0;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Boot()
    {
        if (_instance != null)
        {
            return;
        }
        GameObject go = new GameObject("TimeHudView");
        DontDestroyOnLoad(go);
        go.AddComponent<TimeHudView>();
    }

    /// <summary>패널이 지금 보이는지(검증용).</summary>
    public static bool IsShown
    {
        get { return _instance != null && _instance._panel != null && _instance._panel.activeSelf; }
    }

    /// <summary>화면에 그려진 문구(검증용).</summary>
    public static string ShownText
    {
        get { return _instance != null && _instance._text != null ? _instance._text.text : string.Empty; }
    }

    /// <summary>지금까지 펀치 연출이 나간 횟수(검증용).</summary>
    public static int PunchCount { get; private set; }

    private GameObject _panel;
    private RectTransform _panelRt;
    private TextMeshProUGUI _text;
    private Image[] _pips;
    private Sequence _seq;
    private bool _dirty = true;
    private int _lastTime = -1;
    private bool _fontSet;

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
        _dirty = true;
    }

    private void OnDisable()
    {
        GameRoot.StateChanged -= OnState;
        KillTween();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void OnState(string key)
    {
        // 새 게임·불러오기(key null)는 값만 맞추고 연출 없이 갱신한다.
        if (key == null)
        {
            _lastTime = -1;
        }
        else if (key == CounterKeys.timeCount)
        {
            int now = TimeNow();
            if (_lastTime >= 0 && now > _lastTime && _panel.activeSelf)
            {
                Refresh();
                PlayPunch();
                return;
            }
        }
        _dirty = true;
    }

    private void Update()
    {
        bool show = SceneRouter.IsActive(SceneIds.Main) && SettingsService.Current.ShowTimeHud;
        if (_panel.activeSelf != show)
        {
            _panel.SetActive(show);
            if (show)
            {
                _dirty = true;
            }
        }
        if (show && _dirty)
        {
            Refresh();
        }
    }

    private static int TimeNow()
    {
        return GameRoot.Instance.State.GetCounter(CounterKeys.timeCount);
    }

    private void Refresh()
    {
        _dirty = false;
        if (!_fontSet && DialogManager.Instance != null && DialogManager.Instance.DialogFont != null)
        {
            _text.font = DialogManager.Instance.DialogFont;
            _fontSet = true;
        }
        int t = TimeNow();
        _lastTime = t;
        int day = WorldClock.Day(t);
        int left = WorldClock.SlotsLeft(t);
        string head = ObjectiveBoard.Pick(day + "일차 ", "Day " + day + " ") + TimeName(t);
        string deadline = left == 0
            ? ObjectiveBoard.Pick("마감 직전", "Deadline now")
            : ObjectiveBoard.Pick("마감까지 " + left + "칸", "Deadline in " + left);
        string goal = ObjectiveBoard.CurrentLine();
        _text.text = head + "\n" + deadline + (goal.Length > 0 ? "\n<size=75%>" + goal + "</size>" : string.Empty);
        int filled = WorldClock.SlotInDay(t);
        for (int i = 0; i < _pips.Length; i++)
        {
            _pips[i].color = i < filled ? PipOn : PipOff;
        }
        _text.color = left <= 4 ? Danger : Normal;
    }

    private static string TimeName(int t)
    {
        switch (((t % 4) + 4) % 4)
        {
            case 1: return ObjectiveBoard.Pick("아침", "Morning");
            case 2: return ObjectiveBoard.Pick("낮", "Noon");
            case 3: return ObjectiveBoard.Pick("오후", "Afternoon");
            default: return ObjectiveBoard.Pick("밤", "Night");
        }
    }

    private void PlayPunch()
    {
        PunchCount++;
        KillTween();
        Color baseColor = _text.color;
        _panelRt.localScale = Vector3.one;
        _text.color = Flash;
        _seq = DOTween.Sequence();
        _seq.Append(_panelRt.DOPunchScale(new Vector3(0.12f, 0.12f, 0f), PunchTime, 6, 0.5f));
        _seq.Join(_text.DOColor(baseColor, FlashTime));
        _seq.SetUpdate(true);
        _seq.SetLink(gameObject);
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ClickSoundPlay();
        }
    }

    private void KillTween()
    {
        if (_seq != null)
        {
            _seq.Kill();
            _seq = null;
        }
    }

    private void Build()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 80;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;

        _panel = new GameObject("TimeHud", typeof(RectTransform), typeof(Image));
        _panel.transform.SetParent(transform, false);
        _panelRt = (RectTransform)_panel.transform;
        _panelRt.anchorMin = new Vector2(0f, 1f);
        _panelRt.anchorMax = new Vector2(0f, 1f);
        _panelRt.pivot = new Vector2(0f, 1f);
        _panelRt.anchoredPosition = new Vector2(24f, -150f);
        _panelRt.sizeDelta = new Vector2(640f, 190f);
        Image bg = _panel.GetComponent<Image>();
        bg.color = new Color(0.05f, 0.05f, 0.05f, 0.65f);
        bg.raycastTarget = false;

        GameObject textGo = new GameObject("Text", typeof(RectTransform));
        textGo.transform.SetParent(_panel.transform, false);
        _text = textGo.AddComponent<TextMeshProUGUI>();
        _text.fontSize = 28;
        _text.alignment = TextAlignmentOptions.TopLeft;
        _text.color = Normal;
        _text.textWrappingMode = TextWrappingModes.Normal;
        _text.overflowMode = TextOverflowModes.Overflow;
        _text.lineSpacing = 6f;
        _text.raycastTarget = false;
        RectTransform tr = _text.rectTransform;
        tr.anchorMin = Vector2.zero;
        tr.anchorMax = Vector2.one;
        tr.offsetMin = new Vector2(14f, 8f);
        tr.offsetMax = new Vector2(-14f, -8f);

        _pips = new Image[PipCount];
        for (int i = 0; i < PipCount; i++)
        {
            GameObject pip = new GameObject("Pip" + i, typeof(RectTransform), typeof(Image));
            pip.transform.SetParent(_panel.transform, false);
            RectTransform rt = (RectTransform)pip.transform;
            rt.anchorMin = new Vector2(1f, 1f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.pivot = new Vector2(1f, 1f);
            rt.sizeDelta = new Vector2(16f, 16f);
            rt.anchoredPosition = new Vector2(-14f - (PipCount - 1 - i) * 24f, -14f);
            Image img = pip.GetComponent<Image>();
            img.color = PipOff;
            img.raycastTarget = false;
            _pips[i] = img;
        }
        _panel.SetActive(false);
    }
}
