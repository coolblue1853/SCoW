using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


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
        Screen.SetResolution(1920, 1080, true);
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

    // Start is called before the first frame update


    public void goToMain()
    {
        DataBaseManager.instance.ResetData();
        Setting.SetActive(false);
        SceneManager.LoadScene("Insert");
    }


    public GameObject Setting;

    public bool 대화창켜야하나 = false;
    public void OpenSetting()
    {
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false)
        {
            DataBaseManager.isOpenUi = true;
            Setting.SetActive(true);

        }


    }
    public void CloseSetting()
    {
        DataBaseManager.isOpenUi = false;
        Setting.SetActive(false);
        settingUi.SetActive(true);
        Tutorial.SetActive(false);
        keySetting.SetActive(false);
    }
    public void 게임종료버튼()
    {
        Application.Quit();


    }

    public Text 해상도;
    int 화면체인지인트값 = 1;
    bool 전체화면 = true;

    public void 전체화면온()
    {
        전체화면 = true;
    }
    public void 전체화면오프()
    {
        전체화면 = false;
    }
    public void 화면체인저엔터()
    {
        if (화면체인지인트값 == 0 && 전체화면 == false)
        {
            Screen.SetResolution(2560, 1440, false);
        }
        else if (화면체인지인트값 == 1 && 전체화면 == false)
        {
            Screen.SetResolution(1920, 1080, false);
        }
        else if (화면체인지인트값 == 2 && 전체화면 == false)
        {
            Screen.SetResolution(1600, 900, false);
        }

        else if (화면체인지인트값 == 3 && 전체화면 == false)
        {
            Screen.SetResolution(1280, 720, false);
        }

        else if (화면체인지인트값 == 4 && 전체화면 == false)
        {
            Screen.SetResolution(960, 540, false);
        }

        else if (화면체인지인트값 == 5 && 전체화면 == false)
        {
            Screen.SetResolution(640, 360, false);
        }


        if (화면체인지인트값 == 0 && 전체화면 == true)
        {
            Screen.SetResolution(2560, 1440, true);
        }
        else if (화면체인지인트값 == 1 && 전체화면 == true)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if (화면체인지인트값 == 2 && 전체화면 == true)
        {
            Screen.SetResolution(1600, 900, true);
        }

        else if (화면체인지인트값 == 3 && 전체화면 == true)
        {
            Screen.SetResolution(1280, 720, true);
        }

        else if (화면체인지인트값 == 4 && 전체화면 == true)
        {
            Screen.SetResolution(960, 540, true);
        }

        else if (화면체인지인트값 == 5 && 전체화면 == true)
        {
            Screen.SetResolution(640, 360, true);
        }
    }

    public void 인트더하기()
    {
        화면체인지인트값++;
    }

    public void 빼기()
    {
        화면체인지인트값--;
    }


    void Start()
    {


    }

    public GameObject 전체레드;
    public GameObject 창레드;

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Insert")
        {
            toMainButton.SetActive(false);
        }
        else
        {
            toMainButton.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.Escape) && Setting.activeSelf == true)
        {
            CloseSetting();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Setting.activeSelf == false)
        {
            OpenSetting();

        }


        if (전체화면 == true)
        {
            전체레드.SetActive(true);
            창레드.SetActive(false);

        }
        else if (전체화면 == false)
        {
            전체레드.SetActive(false);
            창레드.SetActive(true);

        }

        if (화면체인지인트값 == 6)
        {
            화면체인지인트값 = 0;
        }

        if (화면체인지인트값 == 0)
        {
            해상도.text = "2560 x 1440";
        }
        else if (화면체인지인트값 == 1)
        {
            해상도.text = "1920 x 1080";
        }
        else if (화면체인지인트값 == 2)
        {
            해상도.text = "1600 x 900";
        }
        else if (화면체인지인트값 == 3)
        {
            해상도.text = "1280 x 720";
        }
        else if (화면체인지인트값 == 4)
        {
            해상도.text = "960 x 540";
        }
        else if (화면체인지인트값 == 5)
        {
            해상도.text = "640 x 360";
        }

    }

    public GameObject settingUi;
    public GameObject Tutorial;
    public GameObject keySetting;
    public void OpenTut()
    {
        settingUi.SetActive(false);
        Tutorial.SetActive(true);
    }

    public void OpenKey()
    {
        settingUi.SetActive(false);
        keySetting.SetActive(true);
    }
}
