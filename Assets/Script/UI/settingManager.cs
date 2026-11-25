using SCoW.Core.Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using SCoW.Core.Save;

public class settingManager : MonoBehaviour
{
    public GameObject toMainButton;
    private static settingManager instance = null;
    public static settingManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            // P4: 저장된 설정(해상도·전체화면)을 화면 UI 상태에 반영하고 저장/불러오기 버튼을 만든다.
            SettingsData saved = SettingsService.Current;
            changeInt = saved.ResolutionIndex;
            isFullScrean = saved.FullScreen;
            BuildSaveButtons();
            BuildDialogOptions();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void GoToMain()
    {
        DataBaseManager.instance.ResetData();
        setting.SetActive(false);
        SceneRouter.Go(SceneIds.Insert);
    }

    public GameObject setting;

    public bool isDalogOn = false;
    public void OpenSetting()
    {
        if (UiGate.CanOpen(PanelKind.Setting))
        {
            SoundManager.Instance.PaperClipPlay();
            UiGate.Push(PanelKind.Setting, CloseSetting, true);
            HintBar.Push(this, SettingHints);
            setting.SetActive(true);

        }
    }
    /// <summary>현재 설정(음량·해상도·전체화면)을 파일에 쓴다. 설정창을 닫을 때 호출.</summary>
    private void PersistSettings()
    {
        SettingsData d = SettingsService.Current;
        d.FullScreen = isFullScrean;
        if (changeInt >= 0 && changeInt < SettingsData.ResolutionCount)
        {
            d.ResolutionIndex = changeInt;
        }
        SettingsService.Save();
    }

    /// <summary>SlotMenuView 가 기존 버튼 모양(글꼴·스프라이트)을 빌려 갈 때 쓴다.</summary>
    public void GetButtonStyle(out Font font, out Sprite sprite)
    {
        font = null;
        sprite = null;
        Transform key = settingUi != null ? settingUi.transform.Find("Key") : null;
        if (key == null)
        {
            return;
        }
        Text txt = key.GetComponentInChildren<Text>(true);
        Image img = key.GetComponent<Image>();
        font = txt != null ? txt.font : null;
        sprite = img != null ? img.sprite : null;
    }

    /// <summary>불러오기로 씬이 바뀌기 직전 설정창을 소리 없이 닫는다.</summary>
    public void HideForLoad()
    {
        UiGate.Remove(PanelKind.Setting);
        HintBar.Remove(this);
        setting.SetActive(false);
        settingUi.SetActive(true);
        tutorial.SetActive(false);
        keySetting.SetActive(false);
    }

    private void BuildSaveButtons()
    {
        Transform key = settingUi != null ? settingUi.transform.Find("Key") : null;
        if (key == null)
        {
            return;
        }
        MakeSlotButton(key, "SaveButton", SaveText.Pick("저장 ▶", "Save ▶"), -63f, SlotMenuView.Mode.Save);
        MakeSlotButton(key, "LoadButton", SaveText.Pick("◀ 불러오기", "◀ Load"), 22f, SlotMenuView.Mode.Load);
    }

    // P8: 글자 속도 슬라이더(5단계)와 스킵 범위(읽은 것만/전부) 버튼을 사운드 칸 아래에 코드로 만든다.
    private Text _skipModeLabel;

    private void BuildDialogOptions()
    {
        Transform sound = settingUi != null ? settingUi.transform.Find("Sound") : null;
        Transform key = settingUi != null ? settingUi.transform.Find("Key") : null;
        Transform sfx = sound != null ? sound.Find("SFX") : null;
        Transform label = sound != null ? sound.Find("Upper3") : null;
        if (sfx == null || label == null || key == null)
        {
            return;
        }
        GameObject s = Instantiate(sfx.gameObject, sound);
        s.name = "TextSpeed";
        Slider sl = s.GetComponent<Slider>();
        sl.onValueChanged = new Slider.SliderEvent(); // 복제본이 SFX 슬라이더의 이벤트를 물려받지 않게
        sl.minValue = 1f;
        sl.maxValue = 5f;
        sl.wholeNumbers = true;
        sl.SetValueWithoutNotify(SettingsService.Current.TextSpeedLevel);
        sl.onValueChanged.AddListener(OnTextSpeedChanged);
        ((RectTransform)s.transform).anchoredPosition += new Vector2(0f, -65f);

        GameObject l = Instantiate(label.gameObject, sound);
        l.name = "Upper4";
        ((RectTransform)l.transform).anchoredPosition += new Vector2(0f, -65f);
        Text lt = l.GetComponentInChildren<Text>(true);
        if (lt != null)
        {
            lt.text = UiText.SettingTextSpeed;
        }

        GameObject b = Instantiate(key.gameObject, key.parent);
        b.name = "SkipModeButton";
        Button old = b.GetComponent<Button>();
        if (old != null)
        {
            DestroyImmediate(old);
        }
        UnityEngine.EventSystems.EventTrigger oldTrigger = b.GetComponent<UnityEngine.EventSystems.EventTrigger>();
        if (oldTrigger != null)
        {
            DestroyImmediate(oldTrigger);
        }
        Button nb = b.AddComponent<Button>();
        nb.targetGraphic = b.GetComponent<Image>();
        nb.onClick.AddListener(ToggleSkipAll);
        RectTransform rt = (RectTransform)b.transform;
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -148f); // 저장(-63)·불러오기(22) 버튼 아래 같은 열
        Transform res = settingUi.transform.Find("SetResolution");
        if (res != null)
        {
            ((RectTransform)res).anchoredPosition += new Vector2(0f, -55f); // 글자 속도 줄이 들어갈 자리를 만든다
        }
        _skipModeLabel = b.GetComponentInChildren<Text>(true);
        RefreshSkipModeLabel();
    }

    private void OnTextSpeedChanged(float value)
    {
        int level = Mathf.RoundToInt(value);
        SettingsService.Current.TextSpeedLevel = level;
        if (DialogManager.Instance != null)
        {
            DialogManager.Instance.SetTextSpeedLevel(level);
        }
    }

    private void ToggleSkipAll()
    {
        SoundManager.Instance.ClickSoundPlay();
        SettingsData d = SettingsService.Current;
        d.SkipAll = !d.SkipAll;
        if (DialogManager.Instance != null && DialogManager.Instance.Runner != null)
        {
            DialogManager.Instance.Runner.SkipAll = d.SkipAll;
        }
        RefreshSkipModeLabel();
        SettingsService.Save();
    }

    private void RefreshSkipModeLabel()
    {
        if (_skipModeLabel != null)
        {
            _skipModeLabel.text = SaveText.Pick(SettingsService.Current.SkipAll ? "스킵: 전부" : "스킵: 읽은 것만", SettingsService.Current.SkipAll ? "Skip: All" : "Skip: Read only");
        }
    }

    private void MakeSlotButton(Transform template, string objName, string label, float y, SlotMenuView.Mode mode)
    {
        GameObject go = Instantiate(template.gameObject, template.parent);
        go.name = objName;
        Button old = go.GetComponent<Button>();
        if (old != null)
        {
            DestroyImmediate(old);
        }
        UnityEngine.EventSystems.EventTrigger oldTrigger = go.GetComponent<UnityEngine.EventSystems.EventTrigger>();
        if (oldTrigger != null)
        {
            DestroyImmediate(oldTrigger); // 복제본이 원본의 설정 진입/호버 이벤트를 물려받지 않게
        }
        Button b = go.AddComponent<Button>();
        b.targetGraphic = go.GetComponent<Image>();
        b.onClick.AddListener(() => OpenSlotMenu(mode));
        RectTransform rt = (RectTransform)go.transform;
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, y);
        Text t = go.GetComponentInChildren<Text>(true);
        if (t != null)
        {
            t.text = label;
        }
    }

    public void OpenSlotMenu(SlotMenuView.Mode mode)
    {
        if (SlotMenuView.IsOpen)
        {
            return;
        }
        SoundManager.Instance.ClickSoundPlay();
        SlotMenuView.Open(mode, null);
    }

    private static readonly SCoW.Core.Ui.Hint[] SettingHints =
    {
        new SCoW.Core.Ui.Hint(SCoW.Core.Ui.HintAction.Page, "Load / Save"),
        new SCoW.Core.Ui.Hint(SCoW.Core.Ui.HintAction.Cancel, "Close"),
    };

    public void CloseSetting()
    {
        PersistSettings();
        SoundManager.Instance.ClickSoundPlay();
        UiGate.Remove(PanelKind.Setting);
        HintBar.Remove(this);
        setting.SetActive(false);
        settingUi.SetActive(true);
        tutorial.SetActive(false);
        keySetting.SetActive(false);
    }


    public Text resolution;
    int changeInt = 1;
    bool isFullScrean = true;

    // (OnFullScrean/OffFullScrean/AddInt/SubInt 로 바꾼 값은 CloseSetting → PersistSettings 에서 저장된다.)
    public void OnFullScrean()
    {
        SoundManager.Instance.ClickSoundPlay();
        isFullScrean = true;
    }
    public void OffFullScrean()
    {
        SoundManager.Instance.ClickSoundPlay();
        isFullScrean = false;
    }
    public void ChangeResolution()
    {
        SoundManager.Instance.ClickSoundPlay();
        if (changeInt == 0 && isFullScrean == false)
        {
            Screen.SetResolution(2560, 1440, false);
        }
        else if (changeInt == 1 && isFullScrean == false)
        {
            Screen.SetResolution(1920, 1080, false);
        }
        else if (changeInt == 2 && isFullScrean == false)
        {
            Screen.SetResolution(1600, 900, false);
        }
        else if (changeInt == 3 && isFullScrean == false)
        {
            Screen.SetResolution(1280, 720, false);
        }
        else if (changeInt == 4 && isFullScrean == false)
        {
            Screen.SetResolution(960, 540, false);
        }
        else if (changeInt == 5 && isFullScrean == false)
        {
            Screen.SetResolution(640, 360, false);
        }
        if (changeInt == 0 && isFullScrean == true)
        {
            Screen.SetResolution(2560, 1440, true);
        }
        else if (changeInt == 1 && isFullScrean == true)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if (changeInt == 2 && isFullScrean == true)
        {
            Screen.SetResolution(1600, 900, true);
        }
        else if (changeInt == 3 && isFullScrean == true)
        {
            Screen.SetResolution(1280, 720, true);
        }
        else if (changeInt == 4 && isFullScrean == true)
        {
            Screen.SetResolution(960, 540, true);
        }
        else if (changeInt == 5 && isFullScrean == true)
        {
            Screen.SetResolution(640, 360, true);
        }
    }
    public void AddInt()
    {
        SoundManager.Instance.ClickSoundPlay();
        changeInt++;
    }
    public void SubInt()
    {
        SoundManager.Instance.ClickSoundPlay();
        changeInt--;
    }
    public GameObject fullCheck;
    public GameObject windowCheck;
    void Update()
    {
        if (SceneRouter.IsActive(SceneIds.Insert))
        {
            toMainButton.SetActive(false);
        }
        else
        {
            toMainButton.SetActive(true);
        }
        if (SlotMenuView.IsOpen)
        {
            // 저장/불러오기 창이 입력(메뉴·취소 키)을 쓰는 동안 설정창은 가만히 둔다.
        }
        else if (GameInput.MenuDown && setting.activeSelf == false && !UiGate.CancelConsumedThisFrame)
        {
            OpenSetting();
        }
        else if (setting.activeSelf == true && settingUi.activeSelf == true)
        {
            // 키보드·패드 경로: 페이지 다음(D/→/RB)=저장, 이전(A/←/LB)=불러오기
            if (GameInput.PageNextDown)
            {
                OpenSlotMenu(SlotMenuView.Mode.Save);
            }
            else if (GameInput.PagePrevDown)
            {
                OpenSlotMenu(SlotMenuView.Mode.Load);
            }
        }
        if (isFullScrean == true)
        {
            fullCheck.SetActive(true);
            windowCheck.SetActive(false);
        }
        else if (isFullScrean == false)
        {
            fullCheck.SetActive(false);
            windowCheck.SetActive(true);
        }

        if (changeInt == 6)
        {
            changeInt = 0;
        }

        if (changeInt == 0)
        {
            resolution.text = "2560 x 1440";
        }
        else if (changeInt == 1)
        {
            resolution.text = "1920 x 1080";
        }
        else if (changeInt == 2)
        {
            resolution.text = "1600 x 900";
        }
        else if (changeInt == 3)
        {
            resolution.text = "1280 x 720";
        }
        else if (changeInt == 4)
        {
            resolution.text = "960 x 540";
        }
        else if (changeInt == 5)
        {
            resolution.text = "640 x 360";
        }
    }
    public GameObject settingUi;
    public GameObject tutorial;
    public GameObject keySetting;
    public void OpenTut()
    {
        SoundManager.Instance.ClickSoundPlay();
        settingUi.SetActive(false);
        tutorial.SetActive(true);
    }
    public void OpenKey()
    {
        SoundManager.Instance.ClickSoundPlay();
        settingUi.SetActive(false);
        keySetting.SetActive(true);
    }
}
