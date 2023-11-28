using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IntelManager : MonoBehaviour
{
    public GameObject intelUI;
    public GameObject characterButton;
    public GameObject eventButton;
    public GameObject placeButton;
    public GameObject characterWindow;
    public GameObject eventWindow;
    public GameObject placeWindow;
    public GameObject uiObject;
    public GameObject intelWindow;
    public GameObject floatingTextObject;
    public GameObject floatingPlace;
    public GameObject floatingPlace2;
    public GameObject eventWindowOn;
    public GameObject characterWindowOn;
    public GameObject placeWindowOn;
    public void AddIntelFloating( string Category, string detail)
    {
        if (Category == "Character")
        {
            if(floatingPlace.transform.childCount == 0)
            {
                var clone = Instantiate(floatingTextObject, floatingPlace.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<IntelFloating>().text.text = "<color=#1A6800>(" + detail + ")</color> Acquisition".ToString();
                clone.transform.SetParent(floatingPlace.transform);
                clone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            else if(floatingPlace.transform.childCount == 1)
            {
                var clone = Instantiate(floatingTextObject, floatingPlace2.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<IntelFloating>().text.text = "<color=#1A6800>(" + detail + ")</color> Acquisition".ToString();
                clone.transform.SetParent(floatingPlace2.transform);
                clone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }

        }
        if (Category == "Event")
        {
            if (floatingPlace.transform.childCount == 0)
            {
                var clone = Instantiate(floatingTextObject, floatingPlace.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<IntelFloating>().text.text = "<color=#B30000>(" + detail + ")</color> Acquisition".ToString();
                clone.transform.SetParent(floatingPlace.transform);
                clone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            else if (floatingPlace.transform.childCount == 1)
            {
                var clone = Instantiate(floatingTextObject, floatingPlace2.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<IntelFloating>().text.text = "<color=#B30000>(" + detail + ")</color> Acquisition".ToString();
                clone.transform.SetParent(floatingPlace2.transform);
                clone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
        }
        if (Category == "Place")
        {
            if (floatingPlace.transform.childCount == 0)
            {
                var clone = Instantiate(floatingTextObject, floatingPlace.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<IntelFloating>().text.text = "<color=#0026A9>(" + detail + ")</color> Acquisition".ToString();
                clone.transform.SetParent(floatingPlace.transform);
                clone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            else if (floatingPlace.transform.childCount == 1)
            {
                var clone = Instantiate(floatingTextObject, floatingPlace2.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<IntelFloating>().text.text = "<color=#0026A9>(" + detail + ")</color> Acquisition".ToString();
                clone.transform.SetParent(floatingPlace2.transform);
                clone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
        }
        if (Category == "Item")
        {
            if (floatingPlace.transform.childCount == 0)
            {
                var clone = Instantiate(floatingTextObject, floatingPlace.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<IntelFloating>().text.text = "(" + detail + ")".ToString();
                clone.transform.SetParent(floatingPlace.transform);
                clone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            else if (floatingPlace.transform.childCount == 1)
            {
                var clone = Instantiate(floatingTextObject, floatingPlace2.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<IntelFloating>().text.text = "(" + detail + ")".ToString();
                clone.transform.SetParent(floatingPlace2.transform);
                clone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
        }
    }
    public void QuitIntelWindow()
    {
        SoundManager.Instance.ClickSound_Play();
        //DataBaseManager.옵션창여부 = false;
        intelWindow.SetActive(false);
    }

    public void Open_Character()
    {
        SoundManager.Instance.ClickSound_Play();
        characterWindowOn.SetActive(true);
        eventWindowOn.SetActive(false);
        placeWindowOn.SetActive(false);

        characterWindow.SetActive(true);
        eventWindow.SetActive(false);
        placeWindow.SetActive(false);
    }

    public void Open_Event()
    {
        SoundManager.Instance.ClickSound_Play();
        characterWindowOn.SetActive(false);
        eventWindowOn.SetActive(true);
        placeWindowOn.SetActive(false);

        characterWindow.SetActive(false);
        eventWindow.SetActive(true);
        placeWindow.SetActive(false);
    }
    public void OpenPlace()
    {
        SoundManager.Instance.ClickSound_Play();
        characterWindowOn.SetActive(false);        
        eventWindowOn.SetActive(false);
        placeWindowOn.SetActive(true);

        characterWindow.SetActive(false);
        eventWindow.SetActive(false);
        placeWindow.SetActive(true);
    }
    public void OpenSetting()
    {
        if (DataBaseManager.isActiveDialog1 == false  && DataBaseManager.isDebuffDyslexia != true && DataBaseManager.storyDirecting == false && DataBaseManager.potalWait == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false && DataBaseManager.secondisDirecting == false && DataBaseManager.isOpenMap == false)
        {
            SoundManager.Instance.PaperClip_Play();
            DataBaseManager.isOpenUi = true;
            intelUI.SetActive(true);
        }
    }
    public void CloseSetting()
    {
        DataBaseManager.isOpenUi = false;
        intelUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1)|| Input.GetKeyDown(KeyCode.Escape))  && intelUI.activeSelf == true)
        {
            CloseSetting();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && intelUI.activeSelf == false)
        {
            OpenSetting();
        }
    }

    private static IntelManager instance = null;
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

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static IntelManager Instance
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
}
