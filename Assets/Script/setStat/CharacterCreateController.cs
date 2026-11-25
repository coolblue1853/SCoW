using System;
using System.Collections.Generic;
using SCoW.Core.Rules;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 캐릭터 생성·스킬 분배를 키보드·패드로 끝까지 진행하게 하는 입력 층(UX-4). 씬을 고치지 않고, SetCharacter 씬이 열리면 코드로 붙는다.
/// 기존 버튼(onClick)과 setSkill 의 UP/Down 메서드를 그대로 호출하므로 굴림·점수 규칙·저장 키는 바뀌지 않는다.
/// 조작: W/S·십자키 위아래=항목 이동, E/A=실행(스킬 칸은 +1단계), A/D·LB/RB=스킬 칸 -/+·탭 전환, 패드 B=뒤로/아니오.
/// 화면 안내는 하단 키 안내(HintBar)와 코드로 만든 안내·요약 패널. 그래픽은 임시(ART-2). TODO(I2): 문구.
/// </summary>
[DefaultExecutionOrder(-500)]
public sealed class CharacterCreateController : MonoBehaviour
{
    private enum Mode
    {
        Stat,
        Skill,
        Confirm,
    }

    private struct Item
    {
        public RectTransform Rect;
        public Action Activate;
        public Action Inc;
        public Action Dec;
        public Action Focus;
    }

    private static readonly string[] StatRows = { "str", "int", "dex", "hp", "mp", "san", "luck", "wealth" };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        SceneRouter.SceneEntered -= OnSceneEntered;
        SceneRouter.SceneEntered += OnSceneEntered;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void BootFirstScene()
    {
        OnSceneEntered(SceneRouter.ActiveId);
    }

    private static void OnSceneEntered(string sceneId)
    {
        if (sceneId == SceneIds.SetCharacter && FindFirstObjectByType<CharacterCreateController>() == null)
        {
            new GameObject("CharacterCreateController").AddComponent<CharacterCreateController>();
        }
    }

    private setStat _stat;
    private setSkill _skill;
    private statExplain _statEx;
    private changeStatPage _pages;
    private Mode _mode = Mode.Stat;
    private int _index;
    private readonly List<Item> _items = new List<Item>(16);
    private Text _guide;
    private Mode _hintMode = (Mode)(-1);
    private bool _needBuild = true;
    private int _guideMode = -1;
    private bool _guideRolled;
    private int _guideS = -1, _guideI = -1, _guideD = -1, _guideStr = -1;

    private void Start()
    {
        _stat = FindFirstObjectByType<setStat>();
        if (_stat == null)
        {
            enabled = false;
            return;
        }
        _skill = _stat.setSkill;
        _statEx = _stat.GetComponent<statExplain>();
        _pages = _skill != null ? _skill.GetComponent<changeStatPage>() : null;
        BuildGuide();
    }

    private void OnDisable()
    {
        FocusScope.Highlight(this, null);
        HintBar.Remove(this);
    }

    private void Update()
    {
        if (_stat == null || UiGate.AnyOpen)
        {
            return;
        }
        DetectMode();
        bool nav = GameInput.NavUpDown || GameInput.NavDownDown;
        bool act = GameInput.InteractDown;
        bool right = GameInput.PageNextDown;
        bool left = GameInput.PagePrevDown;
        // 키보드 Esc 는 Cancel 과 Menu 를 겸한다. 생성 첫 화면에서는 설정창(Menu)에 양보하고, 뒤로 갈 곳이 있을 때만 Esc 로 뒤로 간다.
        bool back = GameInput.CancelDown && (_mode != Mode.Stat || !GameInput.MenuDown);
        if (nav || act || right || left || back || _needBuild)
        {
            Handle(nav, act, right, left, back);
        }
        RefreshGuide();
        RefreshHints();
    }

    private void DetectMode()
    {
        Mode now = _stat.checkUI != null && _stat.checkUI.activeSelf
            ? Mode.Confirm
            : (_stat.setSkillWindow != null && _stat.setSkillWindow.activeSelf ? Mode.Skill : Mode.Stat);
        if (now != _mode)
        {
            _mode = now;
            _index = 0;
            _items.Clear();
            _needBuild = true;
        }
    }

    private void Handle(bool nav, bool act, bool right, bool left, bool back)
    {
        BuildItems();
        _needBuild = false;
        if (_items.Count == 0)
        {
            return;
        }
        if (back && TryBack())
        {
            UiGate.ConsumeCancel();
            _needBuild = true;
            return;
        }
        if (GameInput.NavUpDown)
        {
            _index = (_index + _items.Count - 1) % _items.Count;
        }
        else if (GameInput.NavDownDown)
        {
            _index = (_index + 1) % _items.Count;
        }
        _index = Mathf.Clamp(_index, 0, _items.Count - 1);
        Item item = _items[_index];
        if (act && item.Activate != null)
        {
            item.Activate();
        }
        else if (right && item.Inc != null)
        {
            item.Inc();
        }
        else if (left && item.Dec != null)
        {
            item.Dec();
        }
        // 실행 결과로 창이 바뀌었을 수 있으니 항목을 다시 만들고 현재 칸을 표시한다.
        DetectMode();
        BuildItems();
        if (_items.Count > 0)
        {
            _index = Mathf.Clamp(_index, 0, _items.Count - 1);
            Item cur = _items[_index];
            FocusScope.Highlight(this, cur.Rect);
            if (cur.Focus != null && (nav || act || right || left))
            {
                cur.Focus();
            }
        }
    }

    private bool TryBack()
    {
        if (_mode == Mode.Confirm)
        {
            Click(_stat.checkUI.transform.Find("NoButton"));
            return true;
        }
        if (_mode == Mode.Skill)
        {
            _skill.BackToStat();
            return true;
        }
        return false;
    }

    // ---- 항목 구성 ----

    private void BuildItems()
    {
        _items.Clear();
        if (_mode == Mode.Confirm)
        {
            AddButton(_stat.checkUI.transform.Find("YesButton"));
            AddButton(_stat.checkUI.transform.Find("NoButton"));
        }
        else if (_mode == Mode.Stat)
        {
            BuildStatItems();
        }
        else
        {
            BuildSkillItems();
        }
    }

    private void BuildStatItems()
    {
        AddButton(_stat.setStatButton != null ? _stat.setStatButton.transform : null);
        AddButton(_stat.allRandButton != null ? _stat.allRandButton.transform : null);
        if (_stat.nextButton != null && _stat.nextButton.activeInHierarchy)
        {
            AddButton(_stat.nextButton.transform);
        }
        Action[] explain = _statEx == null ? null : new Action[]
        {
            _statEx.StrExplain, _statEx.IntExplain, _statEx.DexExplain, _statEx.HpExplain,
            _statEx.MpExplain, _statEx.SanExplain, _statEx.LuckExplain, _statEx.WealthExplain,
        };
        for (int i = 0; i < StatRows.Length; i++)
        {
            Transform row = _stat.transform.Find(StatRows[i]);
            if (row != null)
            {
                Item it = new Item { Rect = row as RectTransform };
                it.Focus = explain != null ? explain[i] : null;
                _items.Add(it);
            }
        }
    }

    private void BuildSkillItems()
    {
        if (_skill == null || _pages == null)
        {
            return;
        }
        int page = _pages.dexPage != null && _pages.dexPage.activeSelf ? 2 : (_pages.intPage != null && _pages.intPage.activeSelf ? 1 : 0);
        string[] tabs = { "strSkill", "intSkill", "dexSkill" };
        Transform tab = _skill.transform.Find(tabs[page]);
        Action[] switchTo = { _pages.StrPageButton, _pages.IntPageButton, _pages.DexPageButton };
        Item tabItem = new Item { Rect = tab as RectTransform };
        tabItem.Inc = switchTo[(page + 1) % 3];
        tabItem.Dec = switchTo[(page + 2) % 3];
        tabItem.Activate = switchTo[(page + 1) % 3];
        _items.Add(tabItem);

        string[] pageObj = { "str_skill", "int_skill", "dex_skill" };
        Transform root = _skill.transform.Find(pageObj[page]);
        AddSkillRows(root, page);
        string[] rand = { "leftStrStatRand", "leftIntStatRand", "leftDexStatRand" };
        string[] reset = { "resetStr", "resetInt", "resetDex" };
        AddButton(root != null ? root.Find(rand[page]) : null);
        AddButton(root != null ? root.Find(reset[page]) : null);
        AddButton(_skill.transform.Find("beforeButton"));
        Transform complete = _skill.transform.Find("SkillMark/CompleteCharacterButton");
        if (complete != null && complete.gameObject.activeInHierarchy)
        {
            AddButton(complete);
        }
    }

    private void AddSkillRows(Transform root, int page)
    {
        if (root == null)
        {
            return;
        }
        string[] names;
        Action[] up;
        Action[] down;
        Action[] explain;
        switch (page)
        {
            case 0:
                names = new[] { "martial arts", "gunshot", "sword", "observation", "swiming" };
                up = new Action[] { _skill.MartialArtsUP, _skill.GunShotUP, _skill.SwordUP, _skill.ObservationUP, _skill.SwimingUP };
                down = new Action[] { _skill.MartialArtsDown, _skill.GunShotDown, _skill.SwordDown, _skill.ObservationDown, _skill.SwimingDown };
                strSkillExplain s = root.GetComponent<strSkillExplain>();
                explain = s == null ? null : new Action[] { s.MartialartsExplain, s.GunshotExplain, s.SwordExplain, s.ObservationExplain, s.SwimingExplain };
                break;
            case 1:
                names = new[] { "medicine", "analysis", "listening", "psychotherapy", "occult" };
                up = new Action[] { _skill.MedicineUP, _skill.AnalysisUP, _skill.ListeningUP, _skill.PsychotherapyUP, _skill.OccultUP };
                down = new Action[] { _skill.MedicineDown, _skill.AnalysisDown, _skill.ListeningDown, _skill.PsychotherapyDown, _skill.OccultDown };
                intSkillExplain i = root.GetComponent<intSkillExplain>();
                explain = i == null ? null : new Action[] { i.MedicineExplain, i.AnalysisExplain, i.ListeningExplain, i.PsychotherapyExplain, i.OccultExplain };
                break;
            default:
                names = new[] { "evasion", "deftness", "rhetoric", "stealth", "disguise" };
                up = new Action[] { _skill.EvasionUP, _skill.DeftnessUP, _skill.RhetoricUP, _skill.StealthUP, _skill.DisguiseUP };
                down = new Action[] { _skill.EvasionDown, _skill.DeftnessDown, _skill.RhetoricDown, _skill.StealthDown, _skill.DisguiseDown };
                dexSkillExplain d = root.GetComponent<dexSkillExplain>();
                explain = d == null ? null : new Action[] { d.EvasionExplain, d.DeftnessExplain, d.RhetoricExplain, d.StealthExplain, d.DisguiseExplain };
                break;
        }
        for (int k = 0; k < names.Length; k++)
        {
            Transform row = FindChildIgnoreCase(root, names[k]);
            if (row == null)
            {
                continue;
            }
            Action u = up[k];
            Action dn = down[k];
            Item it = new Item { Rect = row as RectTransform };
            it.Inc = () => Step(u);
            it.Dec = () => Step(dn);
            it.Activate = it.Inc;
            it.Focus = explain != null ? explain[k] : null;
            _items.Add(it);
        }
    }

    // UP/Down 은 누르고 있는 동안 반복하도록 상태(isClick)를 켠다. 키 한 번이므로 바로 놓음(ButtonUP)으로 반복을 막는다.
    private void Step(Action change)
    {
        change();
        _skill.ButtonUP();
    }

    private static Transform FindChildIgnoreCase(Transform parent, string name)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform c = parent.GetChild(i);
            if (string.Equals(c.name.Trim(), name, StringComparison.OrdinalIgnoreCase))
            {
                return c;
            }
        }
        return null;
    }

    private void AddButton(Transform t)
    {
        if (t == null)
        {
            return;
        }
        Item it = new Item { Rect = t as RectTransform };
        Transform captured = t;
        it.Activate = () => Click(captured);
        _items.Add(it);
    }

    private static void Click(Transform t)
    {
        Button b = t != null ? t.GetComponent<Button>() : null;
        if (b != null && b.interactable)
        {
            b.onClick.Invoke();
        }
    }

    // ---- 안내·요약 패널 ----

    private void BuildGuide()
    {
        Font font = null;
        Sprite sprite;
        if (settingManager.Instance != null)
        {
            settingManager.Instance.GetButtonStyle(out font, out sprite);
        }
        Canvas canvas = _stat.GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            return;
        }
        GameObject go = new GameObject("CreateGuide", typeof(RectTransform), typeof(Text));
        go.transform.SetParent(canvas.rootCanvas.transform, false);
        RectTransform rt = (RectTransform)go.transform;
        // 캔버스 기준 해상도 800x450(중앙 기준). 왼쪽 설명 상자(x -366~0, y -198~-15)의 아래쪽 빈 칸에 둔다.
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0f, 0f);
        rt.anchoredPosition = new Vector2(-350f, -196f);
        rt.sizeDelta = new Vector2(336f, 66f);
        _guide = go.GetComponent<Text>();
        _guide.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        _guide.fontSize = 11;
        _guide.alignment = TextAnchor.LowerLeft;
        _guide.horizontalOverflow = HorizontalWrapMode.Wrap;
        _guide.verticalOverflow = VerticalWrapMode.Overflow;
        _guide.color = new Color(0.25f, 0.12f, 0.05f, 1f);
        _guide.raycastTarget = false;
    }

    private void RefreshGuide()
    {
        if (_guide == null)
        {
            return;
        }
        int s = DataBaseManager.strSkillPoint;
        int i = DataBaseManager.intSkillPoint;
        int d = DataBaseManager.dexSkillPoint;
        bool rolled = DataBaseManager.str > 5;
        int str = DataBaseManager.str;
        if (_guideMode == (int)_mode && _guideRolled == rolled && _guideS == s && _guideI == i && _guideD == d && _guideStr == str)
        {
            return;
        }
        _guideMode = (int)_mode;
        _guideRolled = rolled;
        _guideS = s;
        _guideI = i;
        _guideD = d;
        _guideStr = str;
        bool ko = SaveText.IsKorean;
        string text;
        if (_mode == Mode.Skill)
        {
            string warn = CharacterCreation.Remaining(s, i, d, ko);
            text = Summary() + "\n" + (warn.Length > 0
                ? warn + (ko ? " - 모두 쓰면 시작할 수 있습니다." : " - spend them all to start.")
                : SaveText.Pick("점수를 모두 썼습니다. 완료를 선택하세요.", "All points spent. Choose Complete."));
        }
        else if (!rolled)
        {
            text = SaveText.Pick("먼저 'Creating stats' 를 선택해 능력치를 굴리세요.", "First choose 'Creating stats' to roll your stats.");
        }
        else
        {
            text = Summary() + "\n" + SaveText.Pick("마음에 들면 > 로 스킬 점수를 나누세요. 다시 굴려도 됩니다.", "Happy? Press > to assign skill points, or roll again.");
        }
        _guide.text = text;
    }

    private static string Summary()
    {
        return CharacterCreation.Summary(DataBaseManager.str, DataBaseManager.intl, DataBaseManager.dex, DataBaseManager.hp,
            DataBaseManager.mp, DataBaseManager.san, DataBaseManager.luk, DataBaseManager.weal);
    }

    private void RefreshHints()
    {
        if (_hintMode == _mode)
        {
            return;
        }
        _hintMode = _mode;
        HintBar.Remove(this);
        if (_mode == Mode.Skill)
        {
            HintBar.Push(this,
                new Hint(HintAction.Navigate, "Select"),
                new Hint(HintAction.Adjust, "-/+"),
                new Hint(HintAction.Confirm, "+1"),
                new Hint(HintAction.Cancel, "Back"));
        }
        else
        {
            HintBar.Push(this,
                new Hint(HintAction.Navigate, "Select"),
                new Hint(HintAction.Confirm, "OK"),
                _mode == Mode.Confirm ? new Hint(HintAction.Cancel, "No") : new Hint(HintAction.Menu, "Settings"));
        }
    }
}
