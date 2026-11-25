using System;
using System.Collections.Generic;
using SCoW.Core.Save;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// 설정창 본문(UX-2). 옛 SettingUI 의 Sound·SetResolution 겹침 배치를 대신하는, 코드로 만든 탭 3개(소리·화면·게임) 목록.
/// 행 종류: 슬라이더(A/D 증감)·토글(E)·선택(A/D 돌리기, E 다음)·버튼(오른쪽 열의 기존 버튼). W/S 로 행을 옮기고 맨 위 탭 줄에서 A/D 로 탭을 바꾼다.
/// 마우스: 슬라이더 드래그, 행 클릭(선택 행은 좌/우 절반), 탭 클릭, 호버로 포커스. 포커스 링은 FocusScope.Highlight → UxOverlay.
/// 값은 SettingsService.Current(SettingsData) 와 settingManager 가 소유한다. 그래픽은 임시(ART-2 교체), 문구는 SaveText.Pick 임시(TODO I2).
/// </summary>
public sealed partial class SettingsView : MonoBehaviour
{
    private const float RowH = 60f;
    private const float RowStep = 74f;
    private const float RowW = 580f;
    private const int TabCount = 5;
    private const int KeysTab = 3;
    private const int EndingsTab = 4;
    private static readonly Color RowColor = new Color(0.13f, 0.12f, 0.14f, 1f);
    private static readonly Color FocusColor = new Color(0.36f, 0.27f, 0.1f, 1f);
    private static readonly Color TabOnColor = new Color(0.45f, 0.1f, 0.1f, 1f);
    private static readonly Color TextColor = new Color(0.92f, 0.9f, 0.85f, 1f);

    private sealed class Row
    {
        public GameObject Root;
        public Image Bg;
        public Text Label;
        public Text Value;
        public Slider Slide;
        public Action<int> Adjust;
        public Action Activate;
        public Func<string> ValueText;
        public Action Refresh;
    }

    private settingManager _owner;
    private Font _font;
    private readonly List<Row>[] _tabRows = { new List<Row>(), new List<Row>(), new List<Row>(), new List<Row>(), new List<Row>() };
    private readonly List<Row> _side = new List<Row>();
    private readonly List<Row> _focus = new List<Row>();
    private GameObject[] _tabRoots;
    private Image[] _tabBg;
    private Row _tabRow;
    private int _tab;
    private int _index;
    private bool _syncing;

    /// <summary>parent(옛 SettingUI) 아래에 만든다. sideButtons 는 오른쪽 열의 기존 버튼(튜토리얼·키·불러오기·저장·메인).</summary>
    public static SettingsView Create(RectTransform parent, settingManager owner, Font font, GameObject[] sideButtons)
    {
        GameObject go = new GameObject("SettingsView", typeof(RectTransform));
        go.SetActive(false); // 구성이 끝나기 전에 OnEnable 이 돌지 않게
        go.transform.SetParent(parent, false);
        RectTransform rt = (RectTransform)go.transform;
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;
        rt.sizeDelta = new Vector2(RowW, 560f);
        SettingsView v = go.AddComponent<SettingsView>();
        v._owner = owner;
        v._font = font;
        v.Build(sideButtons);
        go.SetActive(true);
        return v;
    }

    private void OnEnable()
    {
        SyncAll();
        SetTab(0);
        _index = 0;
        FocusScope.Enter(this, null);
        ApplyFocus();
    }

    private void OnDisable()
    {
        GameInput.CancelRebind();
        FocusScope.Exit(this);
    }

    // ---------- 구성 ----------

    private void Build(GameObject[] sideButtons)
    {
        _tabRoots = new GameObject[TabCount];
        _tabBg = new Image[TabCount];
        BuildTabBar();
        for (int t = 0; t < TabCount; t++)
        {
            GameObject root = new GameObject("Tab" + t, typeof(RectTransform));
            root.transform.SetParent(transform, false);
            RectTransform r = (RectTransform)root.transform;
            r.anchorMin = r.anchorMax = r.pivot = new Vector2(0.5f, 0.5f);
            r.anchoredPosition = Vector2.zero;
            r.sizeDelta = new Vector2(RowW, 400f);
            _tabRoots[t] = root;
        }
        BuildSoundTab();
        BuildScreenTab();
        BuildGameTab();
        BuildKeysTab();
        BuildEndingsTab();
        for (int i = 0; sideButtons != null && i < sideButtons.Length; i++)
        {
            AddSideButton(sideButtons[i]);
        }
    }

    private void BuildSoundTab()
    {
        AddSlider(0, SaveText.Pick("배경음", "BGM"), 0f, 1f, 0.1f, false, v => SoundManager.Instance.bgmVolum.value = v, () => DataBaseManager.soundVolume, v => Mathf.RoundToInt(v * 100f) + "%");
        AddSlider(0, SaveText.Pick("효과음", "SFX"), 0f, 1f, 0.1f, false, v => SoundManager.Instance.sfxVolum.value = v, () => DataBaseManager.sfxVolume, v => Mathf.RoundToInt(v * 100f) + "%");
    }

    private void BuildScreenTab()
    {
        AddToggle(1, SaveText.Pick("전체 화면", "Fullscreen"), () => _owner.FullScreen, v => _owner.SetFullScreen(v));
        string[] res = new string[SettingsData.ResolutionCount];
        for (int i = 0; i < res.Length; i++)
        {
            res[i] = SettingsService.ResolutionWidth[i] + "x" + SettingsService.ResolutionHeight[i];
        }
        AddChoice(1, SaveText.Pick("해상도", "Resolution"), res, () => _owner.ResolutionIndex, i => _owner.SetResolutionIndex(i));
        string[] sizes = { SaveText.Pick("작게", "Small"), SaveText.Pick("보통", "Medium"), SaveText.Pick("크게", "Large") };
        AddChoice(1, SaveText.Pick("글자 크기", "Text Size"), sizes, () => SettingsService.Current.TextScaleLevel, i => SaveSettings(d => d.TextScaleLevel = i));
        AddToggle(1, SaveText.Pick("화면 흔들림", "Screen Shake"), () => SettingsService.Current.ShakeEnabled, v => SaveSettings(d => d.ShakeEnabled = v));
    }

    private void BuildGameTab()
    {
        AddSlider(2, SaveText.Pick("글자 속도", "Text Speed"), 1f, 5f, 1f, true, v => _owner.ApplyTextSpeed(Mathf.RoundToInt(v)), () => SettingsService.Current.TextSpeedLevel, v => Mathf.RoundToInt(v).ToString());
        AddToggle(2, SaveText.Pick("스킵: 안 읽은 것도", "Skip Unread"), () => SettingsService.Current.SkipAll, v => _owner.SetSkipAll(v));
        string[] waits = new string[SettingsChoices.AutoWaitSec.Length];
        for (int i = 0; i < waits.Length; i++)
        {
            waits[i] = SettingsChoices.AutoWaitSec[i].ToString("0.0") + "s";
        }
        AddChoice(2, SaveText.Pick("자동 진행 대기", "Auto Wait"), waits, () => SettingsChoices.NearestAutoWait(SettingsService.Current.AutoWaitSec), i => SaveSettings(d => d.AutoWaitSec = SettingsChoices.AutoWaitSec[i]));
        AddToggle(2, SaveText.Pick("키 안내 표시", "Key Hints"), () => SettingsService.Current.ShowHints, v => SaveSettings(d => d.ShowHints = v));
        AddToggle(2, SaveText.Pick("시간 표시", "Time HUD"), () => SettingsService.Current.ShowTimeHud, v => SaveSettings(d => d.ShowTimeHud = v));
        AddToggle(2, SaveText.Pick("빠른 판정", "Fast Roll"), () => SettingsService.Current.FastRoll, v => SaveSettings(d => d.FastRoll = v));
    }

    private static void SaveSettings(Action<SettingsData> change)
    {
        change(SettingsService.Current);
        SettingsService.Save();
    }

    private void BuildTabBar()
    {
        GameObject bar = new GameObject("TabBar", typeof(RectTransform));
        bar.transform.SetParent(transform, false);
        RectTransform r = (RectTransform)bar.transform;
        r.anchorMin = r.anchorMax = r.pivot = new Vector2(0.5f, 0.5f);
        r.anchoredPosition = new Vector2(0f, 100f);
        r.sizeDelta = new Vector2(RowW, 58f);
        string[] names = { SaveText.Pick("소리", "Sound"), SaveText.Pick("화면", "Screen"), SaveText.Pick("게임", "Game"), SaveText.Pick("키", "Keys"), SaveText.Pick("엔딩", "Endings") };
        float w = RowW / TabCount;
        for (int t = 0; t < TabCount; t++)
        {
            int tab = t;
            GameObject b = new GameObject("TabButton" + t, typeof(RectTransform), typeof(Image), typeof(RowPointer));
            b.transform.SetParent(bar.transform, false);
            RectTransform br = (RectTransform)b.transform;
            br.anchorMin = br.anchorMax = br.pivot = new Vector2(0.5f, 0.5f);
            br.sizeDelta = new Vector2(w - 8f, 52f);
            br.anchoredPosition = new Vector2((t - (TabCount - 1) * 0.5f) * w, 0f);
            _tabBg[t] = b.GetComponent<Image>();
            Text label = MakeText("Label", b.transform, names[t], TextAnchor.MiddleCenter);
            label.resizeTextForBestFit = true; // 탭 5개: 좁은 칸에 영문 이름이 들어가게 줄인다
            label.resizeTextMinSize = 12;
            label.resizeTextMaxSize = 26;
            Stretch((RectTransform)label.transform);
            RowPointer p = b.GetComponent<RowPointer>();
            p.OnClick = e => { SetTab(tab); _index = 0; RebuildFocus(); ApplyFocus(); };
        }
        _tabRow = new Row { Root = bar, Bg = null };
        _tabRow.Adjust = dir => { SetTab(SettingsChoices.Cycle(_tab, dir, TabCount)); RebuildFocus(); ApplyFocus(); };
        _tabRow.Activate = () => _tabRow.Adjust(1);
    }

    private Row NewRow(int tab, string label)
    {
        List<Row> list = _tabRows[tab];
        GameObject go = new GameObject("Row" + list.Count, typeof(RectTransform), typeof(Image), typeof(RowPointer));
        go.transform.SetParent(_tabRoots[tab].transform, false);
        RectTransform rt = (RectTransform)go.transform;
        rt.anchorMin = rt.anchorMax = rt.pivot = new Vector2(0.5f, 0.5f);
        bool compact = tab == KeysTab; // 키 탭은 행이 14개라 촘촘하게
        float h = compact ? KeyRowH : RowH;
        rt.sizeDelta = new Vector2(RowW, h);
        rt.anchoredPosition = new Vector2(0f, compact ? KeyRowY0 - list.Count * KeyRowStep : 20f - list.Count * RowStep);
        Row row = new Row { Root = go, Bg = go.GetComponent<Image>() };
        row.Bg.color = RowColor;
        Text lt = MakeText("Label", go.transform, label, TextAnchor.MiddleLeft);
        row.Label = lt;
        RectTransform lr = (RectTransform)lt.transform;
        lr.anchorMin = lr.anchorMax = lr.pivot = new Vector2(0f, 0.5f);
        lr.anchoredPosition = new Vector2(18f, 0f);
        lr.sizeDelta = new Vector2(250f, h);
        RowPointer p = go.GetComponent<RowPointer>();
        p.OnEnter = () => FocusRow(row);
        list.Add(row);
        return row;
    }

    private void AddToggle(int tab, string label, Func<bool> get, Action<bool> set)
    {
        Row row = NewRow(tab, label);
        row.Value = MakeValueText(row.Root.transform);
        row.ValueText = () => get() ? SaveText.Pick("켜짐", "ON") : SaveText.Pick("꺼짐", "OFF");
        row.Activate = () => { Click(); set(!get()); RefreshRow(row); };
        row.Adjust = dir => row.Activate();
        row.Refresh = () => RefreshRow(row);
        row.Root.GetComponent<RowPointer>().OnClick = e => row.Activate();
    }

    private void AddChoice(int tab, string label, string[] options, Func<int> get, Action<int> set)
    {
        Row row = NewRow(tab, label);
        row.Value = MakeValueText(row.Root.transform);
        row.ValueText = () => "< " + options[Mathf.Clamp(get(), 0, options.Length - 1)] + " >";
        row.Adjust = dir => { Click(); set(SettingsChoices.Cycle(get(), dir, options.Length)); RefreshRow(row); };
        row.Activate = () => row.Adjust(1);
        row.Refresh = () => RefreshRow(row);
        row.Root.GetComponent<RowPointer>().OnClick = e =>
        {
            RectTransform rt = (RectTransform)row.Root.transform;
            Vector2 local;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, e.position, e.pressEventCamera, out local);
            row.Adjust(local.x < 80f ? -1 : 1);
        };
    }

    private void AddSlider(int tab, string label, float min, float max, float step, bool whole, Action<float> set, Func<float> get, Func<float, string> format)
    {
        Row row = NewRow(tab, label);
        row.Value = MakeValueText(row.Root.transform);
        RectTransform vr = row.Value.rectTransform;
        vr.anchorMin = vr.anchorMax = vr.pivot = new Vector2(1f, 0.5f);
        vr.anchoredPosition = new Vector2(-14f, 0f);
        vr.sizeDelta = new Vector2(80f, RowH);
        row.Value.alignment = TextAnchor.MiddleRight;
        Slider s = MakeSlider(row.Root.transform);
        s.minValue = min;
        s.maxValue = max;
        s.wholeNumbers = whole;
        row.Slide = s;
        s.onValueChanged.AddListener(v =>
        {
            if (_syncing)
            {
                return;
            }
            set(v);
            row.Value.text = format(v);
        });
        row.Refresh = () =>
        {
            _syncing = true;
            s.SetValueWithoutNotify(Mathf.Clamp(get(), min, max));
            _syncing = false;
            row.Value.text = format(s.value);
        };
        row.Adjust = dir => s.value = SettingsChoices.StepSlider(s.value, dir, step, min, max);
    }

    private void AddSideButton(GameObject go)
    {
        Button b = go != null ? go.GetComponent<Button>() : null;
        if (b == null)
        {
            return;
        }
        Row row = new Row { Root = go };
        row.Activate = () => b.onClick.Invoke();
        _side.Add(row);
    }

    // ---------- 갱신·포커스 ----------

    private void RefreshRow(Row row)
    {
        if (row.Value != null && row.ValueText != null)
        {
            row.Value.text = row.ValueText();
        }
    }

    private void SyncAll()
    {
        for (int t = 0; t < TabCount; t++)
        {
            for (int i = 0; i < _tabRows[t].Count; i++)
            {
                Row r = _tabRows[t][i];
                if (r.Refresh != null)
                {
                    r.Refresh();
                }
            }
        }
    }

    private void SetTab(int tab)
    {
        _tab = tab;
        for (int t = 0; t < TabCount; t++)
        {
            _tabRoots[t].SetActive(t == tab);
            _tabBg[t].color = t == tab ? TabOnColor : RowColor;
        }
        RebuildFocus();
    }

    private void RebuildFocus()
    {
        _focus.Clear();
        _focus.Add(_tabRow);
        _focus.AddRange(_tabRows[_tab]);
        for (int i = 0; i < _side.Count; i++)
        {
            if (_side[i].Root != null && _side[i].Root.activeInHierarchy)
            {
                _focus.Add(_side[i]);
            }
        }
        _index = Mathf.Clamp(_index, 0, _focus.Count - 1);
    }

    private void FocusRow(Row row)
    {
        int i = _focus.IndexOf(row);
        if (i >= 0 && i != _index)
        {
            _index = i;
            ApplyFocus();
        }
    }

    private void ApplyFocus()
    {
        for (int t = 0; t < TabCount; t++)
        {
            List<Row> list = _tabRows[t];
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Bg.color = _focus.Count > _index && _focus[_index] == list[i] ? FocusColor : RowColor;
            }
        }
        if (_focus.Count > _index)
        {
            FocusScope.Highlight(this, _focus[_index].Root.transform as RectTransform);
        }
    }

    private void Update()
    {
        if (!UiGate.Stack.IsTop(PanelKind.Setting) || SlotMenuView.IsOpen || GameInput.IsRebinding)
        {
            return;
        }
        if (_focus.Count == 0)
        {
            RebuildFocus();
        }
        if (GameInput.NavUpDown)
        {
            Move(-1);
        }
        else if (GameInput.NavDownDown)
        {
            Move(1);
        }
        else if (GameInput.PagePrevDown)
        {
            Invoke(_focus[_index].Adjust, -1);
        }
        else if (GameInput.PageNextDown)
        {
            Invoke(_focus[_index].Adjust, 1);
        }
        else if (GameInput.InteractDown || EnterPressed())
        {
            Action a = _focus[_index].Activate;
            if (a != null)
            {
                a();
            }
        }
    }

    private void Move(int dir)
    {
        _index = SettingsChoices.Cycle(_index, dir, _focus.Count);
        ApplyFocus();
    }

    private static void Invoke(Action<int> a, int dir)
    {
        if (a != null)
        {
            a(dir);
        }
    }

    private static bool EnterPressed()
    {
        Keyboard kb = Keyboard.current;
        return kb != null && (kb.enterKey.wasPressedThisFrame || kb.numpadEnterKey.wasPressedThisFrame);
    }

    private static void Click()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ClickSoundPlay();
        }
    }

    // ---------- 임시 UI 부품 ----------

    private Text MakeText(string name, Transform parent, string text, TextAnchor anchor)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Text));
        go.transform.SetParent(parent, false);
        Text t = go.GetComponent<Text>();
        t.font = _font;
        t.fontSize = 26;
        t.alignment = anchor;
        t.color = TextColor;
        t.text = text;
        t.raycastTarget = false;
        t.horizontalOverflow = HorizontalWrapMode.Overflow;
        t.verticalOverflow = VerticalWrapMode.Overflow;
        return t;
    }

    private Text MakeValueText(Transform parent)
    {
        Text t = MakeText("Value", parent, string.Empty, TextAnchor.MiddleCenter);
        RectTransform r = t.rectTransform;
        r.anchorMin = r.anchorMax = r.pivot = new Vector2(1f, 0.5f);
        r.anchoredPosition = new Vector2(-20f, 0f);
        r.sizeDelta = new Vector2(300f, RowH);
        t.resizeTextForBestFit = true; // 픽셀 글꼴은 폭이 넓어 긴 값(해상도)이 행을 넘지 않게 줄인다
        t.resizeTextMinSize = 14;
        t.resizeTextMaxSize = 26;
        return t;
    }

    private static void Stretch(RectTransform r)
    {
        r.anchorMin = Vector2.zero;
        r.anchorMax = Vector2.one;
        r.offsetMin = Vector2.zero;
        r.offsetMax = Vector2.zero;
    }

    private static Slider MakeSlider(Transform parent)
    {
        GameObject go = new GameObject("Slider", typeof(RectTransform), typeof(Slider));
        go.transform.SetParent(parent, false);
        RectTransform rt = (RectTransform)go.transform;
        rt.anchorMin = rt.anchorMax = rt.pivot = new Vector2(1f, 0.5f);
        rt.anchoredPosition = new Vector2(-110f, 0f);
        rt.sizeDelta = new Vector2(190f, 30f);
        Image bg = NewImage("Background", go.transform, new Color(0.28f, 0.27f, 0.3f, 1f));
        RectTransform br = bg.rectTransform;
        br.anchorMin = new Vector2(0f, 0.5f);
        br.anchorMax = new Vector2(1f, 0.5f);
        br.offsetMin = new Vector2(0f, -7f);
        br.offsetMax = new Vector2(0f, 7f);
        GameObject fillArea = new GameObject("FillArea", typeof(RectTransform));
        fillArea.transform.SetParent(go.transform, false);
        RectTransform fa = (RectTransform)fillArea.transform;
        fa.anchorMin = new Vector2(0f, 0.5f);
        fa.anchorMax = new Vector2(1f, 0.5f);
        fa.offsetMin = new Vector2(0f, -7f);
        fa.offsetMax = new Vector2(0f, 7f);
        Image fill = NewImage("Fill", fillArea.transform, new Color(0.6f, 0.1f, 0.1f, 1f));
        fill.rectTransform.anchorMin = Vector2.zero;
        fill.rectTransform.anchorMax = new Vector2(0f, 1f);
        fill.rectTransform.offsetMin = Vector2.zero;
        fill.rectTransform.offsetMax = Vector2.zero;
        GameObject handleArea = new GameObject("HandleArea", typeof(RectTransform));
        handleArea.transform.SetParent(go.transform, false);
        Stretch((RectTransform)handleArea.transform);
        Image handle = NewImage("Handle", handleArea.transform, new Color(0.85f, 0.83f, 0.8f, 1f));
        handle.rectTransform.sizeDelta = new Vector2(18f, 32f);
        Slider s = go.GetComponent<Slider>();
        s.fillRect = fill.rectTransform;
        s.handleRect = handle.rectTransform;
        s.targetGraphic = handle;
        s.direction = Slider.Direction.LeftToRight;
        Navigation nav = new Navigation();
        nav.mode = Navigation.Mode.None; // 이동은 SettingsView 가 직접 처리한다(EventSystem 이동과 겹치지 않게)
        s.navigation = nav;
        return s;
    }

    private static Image NewImage(string name, Transform parent, Color color)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        Image img = go.GetComponent<Image>();
        img.color = color;
        return img;
    }

    /// <summary>행 호버·클릭 전달용(마우스 경로).</summary>
    private sealed class RowPointer : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        public Action OnEnter;
        public Action<PointerEventData> OnClick;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (OnEnter != null)
            {
                OnEnter();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClick != null)
            {
                OnClick(eventData);
            }
        }
    }
}
