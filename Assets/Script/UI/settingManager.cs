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
        SceneManager.LoadScene("Insert");
    }

    public GameObject setting;

    public bool isDalogOn = false;
    public void OpenSetting()
    {
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.storyDirecting == false && DataBaseManager.isRollet == false && DataBaseManager.potalWait == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false && DataBaseManager.secondisDirecting == false && DataBaseManager.isOpenMap == false)
        {
            SoundManager.Instance.PaperClipPlay();
            DataBaseManager.isOpenUi = true;
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
        DataBaseManager.isOpenUi = false;
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

    public void CloseSetting()
    {
        PersistSettings();
        SoundManager.Instance.ClickSoundPlay();
        DataBaseManager.isOpenUi = false;
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
        if (SceneManager.GetActiveScene().name == "Insert")
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
        else if (GameInput.MenuDown && setting.activeSelf == true)
        {
            CloseSetting();
        }
        else if (GameInput.MenuDown && setting.activeSelf == false)
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
