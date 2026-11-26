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
            Persist.Keep(this.gameObject);
            // P4: 저장된 설정(해상도·전체화면)을 화면 UI 상태에 반영하고 저장/불러오기 버튼을 만든다.
            SettingsData saved = SettingsService.Current;
            changeInt = saved.ResolutionIndex;
            isFullScrean = saved.FullScreen;
            BuildSaveButtons();
            BuildView();
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

    // UX-2: 설정 본문(탭 3개)은 SettingsView 가 코드로 만든다. 옛 Sound·SetResolution 칸은 숨기고(겹침 원인), 오른쪽 열 버튼은 그대로 쓴다.
    private SettingsView _view;

    private void BuildView()
    {
        if (settingUi == null)
        {
            return;
        }
        Font font;
        Sprite sprite;
        GetButtonStyle(out font, out sprite);
        Transform sound = settingUi.transform.Find("Sound");
        Transform res = settingUi.transform.Find("SetResolution");
        if (sound != null)
        {
            sound.gameObject.SetActive(false);
        }
        if (res != null)
        {
            res.gameObject.SetActive(false);
        }
        GameObject[] side =
        {
            FindChildObject("Tuto"),
            FindChildObject("Key"),
            FindChildObject("LoadButton"),
            FindChildObject("SaveButton"),
            toMainButton,
        };
        _view = SettingsView.Create((RectTransform)settingUi.transform, this, font, side);
    }

    private GameObject FindChildObject(string childName)
    {
        Transform t = settingUi.transform.Find(childName);
        return t != null ? t.gameObject : null;
    }

    public bool FullScreen
    {
        get { return isFullScrean; }
    }

    public int ResolutionIndex
    {
        get { return changeInt; }
    }

    public void SetFullScreen(bool on)
    {
        isFullScrean = on;
        ChangeResolution();
        PersistSettings();
    }

    public void SetResolutionIndex(int index)
    {
        changeInt = Mathf.Clamp(index, 0, SettingsData.ResolutionCount - 1);
        ChangeResolution();
        PersistSettings();
    }

    /// <summary>글자 속도 1~5단계(P8).</summary>
    public void ApplyTextSpeed(int level)
    {
        SettingsService.Current.TextSpeedLevel = level;
        if (DialogManager.Instance != null)
        {
            DialogManager.Instance.SetTextSpeedLevel(level);
        }
        SettingsService.Save();
    }

    /// <summary>스킵 범위(읽은 것만/전부).</summary>
    public void SetSkipAll(bool all)
    {
        SettingsData d = SettingsService.Current;
        d.SkipAll = all;
        if (DialogManager.Instance != null && DialogManager.Instance.Runner != null)
        {
            DialogManager.Instance.Runner.SkipAll = all;
        }
        SettingsService.Save();
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
        new SCoW.Core.Ui.Hint(SCoW.Core.Ui.HintAction.Navigate, "Select"),
        new SCoW.Core.Ui.Hint(SCoW.Core.Ui.HintAction.Adjust, "Change"),
        new SCoW.Core.Ui.Hint(SCoW.Core.Ui.HintAction.Confirm, "Toggle / Press"),
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
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ClickSoundPlay();
        }
        int i = Mathf.Clamp(changeInt, 0, SettingsData.ResolutionCount - 1);
        Screen.SetResolution(SettingsService.ResolutionWidth[i], SettingsService.ResolutionHeight[i], isFullScrean);
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
        // 키보드·패드 조작은 SettingsView 가 맡는다(저장·불러오기는 오른쪽 열 버튼으로 W/S 이동 후 E).
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
