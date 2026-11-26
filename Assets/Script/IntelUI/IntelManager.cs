using SCoW.Core.Ui;
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
        SoundManager.Instance.ClickSoundPlay();
        //DataBaseManager.옵션창여부 = false;
        intelWindow.SetActive(false);
    }

    public void OpenCharacter()
    {
        IntelTodoTab.Hide(this, false);
        SoundManager.Instance.ClickSoundPlay();
        characterWindowOn.SetActive(true);
        eventWindowOn.SetActive(false);
        placeWindowOn.SetActive(false);

        characterWindow.SetActive(true);
        eventWindow.SetActive(false);
        placeWindow.SetActive(false);
    }

    public void OpenEvent()
    {
        IntelTodoTab.Hide(this, false);
        SoundManager.Instance.ClickSoundPlay();
        characterWindowOn.SetActive(false);
        eventWindowOn.SetActive(true);
        placeWindowOn.SetActive(false);

        characterWindow.SetActive(false);
        eventWindow.SetActive(true);
        placeWindow.SetActive(false);
    }
    public void OpenPlace()
    {
        IntelTodoTab.Hide(this, false);
        SoundManager.Instance.ClickSoundPlay();
        characterWindowOn.SetActive(false);        
        eventWindowOn.SetActive(false);
        placeWindowOn.SetActive(true);

        characterWindow.SetActive(false);
        eventWindow.SetActive(false);
        placeWindow.SetActive(true);
    }
    public void OpenSetting()
    {
        if (UiGate.CanOpen(PanelKind.Intel))
        {
            SoundManager.Instance.PaperClipPlay();
            UiGate.Push(PanelKind.Intel, CloseSetting);
            HintBar.Push(this, IntelHints);
            intelUI.SetActive(true);
            IntelTodoTab.Ensure(this);
            KeywordToast.OnIntelOpened();
        }
    }
    private static readonly SCoW.Core.Ui.Hint[] IntelHints =
    {
        new SCoW.Core.Ui.Hint(SCoW.Core.Ui.HintAction.Scroll, "Scroll"),
        new SCoW.Core.Ui.Hint(SCoW.Core.Ui.HintAction.Page, "Page"),
        new SCoW.Core.Ui.Hint(SCoW.Core.Ui.HintAction.Cancel, "Close"),
    };

    public void CloseSetting()
    {
        UiGate.Remove(PanelKind.Intel);
        HintBar.Remove(this);
        IntelTodoTab.Hide(this, true);
        intelUI.SetActive(false);
        IntelKeywordUseButton.Hide();
        KeywordPicker.OnIntelClosed();
    }

    private int _fromPickerFrame = -1;

    /// <summary>K6: 키워드 제시 창 위로 수첩을 연다(스택: 제시 창 → 수첩). 열었으면 true.</summary>
    public bool OpenFromPicker(SCoW.Core.Keyword.KeywordCategory category)
    {
        if (intelUI.activeSelf || !UiGate.Stack.IsTop(PanelKind.Keyword))
        {
            return false;
        }
        SoundManager.Instance.PaperClipPlay();
        UiGate.Push(PanelKind.Intel, CloseSetting);
        HintBar.Push(this, IntelHints);
        intelUI.SetActive(true);
        IntelTodoTab.Ensure(this);
        _fromPickerFrame = Time.frameCount;
        switch (category)
        {
            case SCoW.Core.Keyword.KeywordCategory.Character:
                OpenCharacter();
                break;
            case SCoW.Core.Keyword.KeywordCategory.Place:
                OpenPlace();
                break;
            default:
                OpenEvent();
                break;
        }
        KeywordToast.OnIntelOpened();
        IntelKeywordUseButton.Show(UseForQuestion);
        return true;
    }

    /// <summary>K6: 지금 펼친 수첩 페이지의 상위 키워드를 제시 창에서 선택 상태로 두고 수첩을 닫는다.</summary>
    public void UseForQuestion()
    {
        if (!intelUI.activeSelf || !KeywordPicker.HiddenForIntel)
        {
            return;
        }
        string upper = null;
        Transform[] all = intelUI.GetComponentsInChildren<Transform>(false);
        for (int i = 0; i < all.Length && upper == null; i++)
        {
            SCoW.Core.Keyword.IntelUpperMap.TryGetUpper(all[i].name, out upper);
        }
        if (upper == null)
        {
            return; // 펼친 페이지가 없거나 키워드 없는 페이지: 수첩을 그대로 둔다
        }
        CloseSetting();
        KeywordPicker.FocusUpper(upper);
    }
    // Update is called once per frame
    void Update()
    {
        if (intelUI.activeSelf == true && GameInput.OpenIntelDown && UiGate.Stack.IsTop(PanelKind.Intel) && Time.frameCount != _fromPickerFrame)
        {
            CloseSetting();
        }
        else if (intelUI.activeSelf == true && KeywordPicker.HiddenForIntel && GameInput.PresentDown && Time.frameCount != _fromPickerFrame)
        {
            UseForQuestion();
        }
        else if (intelUI.activeSelf && GameInput.TodoTabDown && UiGate.Stack.IsTop(PanelKind.Intel))
        {
            IntelTodoTab.Toggle(this);
        }
        else if (GameInput.OpenIntelDown && intelUI.activeSelf == false && !IsPresentKeyDown())
        {
            OpenSetting();
        }
    }

    // K5: 패드 Y 는 OpenIntel 과 Present 가 같은 버튼이다. 키워드 제시가 가능한 선택 행에서는 Present 가 우선이다.
    private static bool IsPresentKeyDown()
    {
        End_Look_Judge row = End_Look_Judge.Instance;
        return GameInput.PresentDown && row != null && row.CanPresentKeyword;
    }

    private static IntelManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            Persist.Keep(this.gameObject);
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
