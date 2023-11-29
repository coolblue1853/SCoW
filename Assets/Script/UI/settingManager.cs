using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
            SoundManager.Instance.PaperClip_Play();
            DataBaseManager.isOpenUi = true;
            setting.SetActive(true);

        }
    }
    public void CloseSetting()
    {
        SoundManager.Instance.ClickSound_Play();
        DataBaseManager.isOpenUi = false;
        setting.SetActive(false);
        settingUi.SetActive(true);
        tutorial.SetActive(false);
        keySetting.SetActive(false);
    }


    public Text resolution;
    int changeInt = 1;
    bool isFullScrean = true;

    public void OnFullScrean()
    {
        SoundManager.Instance.ClickSound_Play();
        isFullScrean = true;
    }
    public void OffFullScrean()
    {
        SoundManager.Instance.ClickSound_Play();
        isFullScrean = false;
    }
    public void ChangeResolution()
    {
        SoundManager.Instance.ClickSound_Play();
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
        SoundManager.Instance.ClickSound_Play();
        changeInt++;
    }
    public void SubInt()
    {
        SoundManager.Instance.ClickSound_Play();
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
        if (Input.GetKeyDown(KeyCode.Escape) && setting.activeSelf == true)
        {
            CloseSetting();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && setting.activeSelf == false)
        {
            OpenSetting();
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
        SoundManager.Instance.ClickSound_Play();
        settingUi.SetActive(false);
        tutorial.SetActive(true);
    }
    public void OpenKey()
    {
        SoundManager.Instance.ClickSound_Play();
        settingUi.SetActive(false);
        keySetting.SetActive(true);
    }
}
