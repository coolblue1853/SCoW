using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ManageMain : MonoBehaviour
{
    public GameObject back;
    public Image backGroundImg;
    public GameObject select1;
    public GameObject select2;
    public GameObject select3;
    bool isSelectOn1 = false;
    bool isSelectOn2 = false;
    bool isSelectOn3 = false;
    Sequence mySequence;

    // P4: 타이틀 "이어하기"(저장 파일이 있을 때만). 씬을 고치지 않고 시작 버튼을 복제해 코드로 만든다.
    private GameObject _continueButton;

    private void Start()
    {
        BuildContinueButton();
    }

    private void BuildContinueButton()
    {
        if (select1 == null || !SaveFlow.AnySave())
        {
            return;
        }
        Transform start = select1.transform.parent;
        Transform option = select2.transform.parent;
        Transform quit = select3.transform.parent;
        GameObject go = Instantiate(start.gameObject, start.parent);
        go.name = "ContinueButton";
        Button oldButton = go.GetComponent<Button>();
        if (oldButton != null)
        {
            DestroyImmediate(oldButton);
        }
        UnityEngine.EventSystems.EventTrigger oldTrigger = go.GetComponent<UnityEngine.EventSystems.EventTrigger>();
        if (oldTrigger != null)
        {
            DestroyImmediate(oldTrigger);
        }
        Button b = go.AddComponent<Button>();
        b.targetGraphic = go.GetComponent<Image>();
        b.onClick.AddListener(OpenContinue);
        Transform highlight = go.transform.Find("Select");
        UnityEngine.EventSystems.EventTrigger trig = go.AddComponent<UnityEngine.EventSystems.EventTrigger>();
        AddHover(trig, UnityEngine.EventSystems.EventTriggerType.PointerEnter, highlight, true);
        AddHover(trig, UnityEngine.EventSystems.EventTriggerType.PointerExit, highlight, false);
        TMPro.TextMeshProUGUI label = go.GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
        if (label != null)
        {
            // TMP 글꼴(pcsenior SDF)은 정적이라 한글 글리프가 없다. 없으면 영문으로 둔다.
            label.text = label.font != null && label.font.HasCharacter('이', true) ? SaveText.Pick("이어하기", "Continue") : "Continue";
        }
        // 4개 버튼을 같은 간격으로 다시 배치(위에서부터 시작·이어하기·옵션·종료).
        float x = ((RectTransform)start).anchoredPosition.x;
        float top = ((RectTransform)start).anchoredPosition.y + 22f;
        float step = 84f;
        ((RectTransform)start).anchoredPosition = new Vector2(x, top);
        ((RectTransform)go.transform).anchoredPosition = new Vector2(x, top - step);
        ((RectTransform)option).anchoredPosition = new Vector2(x, top - step * 2f);
        ((RectTransform)quit).anchoredPosition = new Vector2(x, top - step * 3f);
        _continueButton = go;
    }

    private static void AddHover(UnityEngine.EventSystems.EventTrigger trig, UnityEngine.EventSystems.EventTriggerType type, Transform target, bool on)
    {
        if (target == null)
        {
            return;
        }
        UnityEngine.EventSystems.EventTrigger.Entry e = new UnityEngine.EventSystems.EventTrigger.Entry { eventID = type };
        e.callback.AddListener(_ => target.gameObject.SetActive(on));
        trig.triggers.Add(e);
    }

    private void Update()
    {
        // 키보드·패드: E / 패드 남쪽 버튼으로 이어하기. 설정창·슬롯창이 열려 있으면 무시.
        if (_continueButton != null && GameInput.InteractDown && !SlotMenuView.IsOpen && !DataBaseManager.isOpenUi)
        {
            OpenContinue();
        }
    }

    public void OpenContinue()
    {
        if (SlotMenuView.IsOpen)
        {
            return;
        }
        SoundManager.Instance.PenCircle();
        SlotMenuView.Open(SlotMenuView.Mode.Load, null);
    }

    public void FadeIn()
    {
        SoundManager.Instance.PenCircle();
        back.SetActive(true);
        Tween fadeTween = backGroundImg.DOFade(1, 1.5f);
        fadeTween.OnComplete(GoSetCharacter);
    }
    public void GameQuit()
    {
        SoundManager.Instance.PenCircle();
        Application.Quit();
    }
    public void GoSetCharacter()
    {
        SceneManager.LoadScene("SetCharacter");
    }
    public void OpenTile()
    {
        if (isSelectOn1 == false)
        {
            isSelectOn1 = true;
            select1.SetActive(true);
        }
    }
    public void CloseTile()
    {
        if (isSelectOn1 == true)
        {
            isSelectOn1 = false;
            select1.SetActive(false);
        }
    }
    public void OpenTile2()
    {
        if (isSelectOn2 == false)
        {
            isSelectOn2 = true;
            select2.SetActive(true);
        }
    }
    public void CloseTile2()
    {
        if (isSelectOn2 == true)
        {
            isSelectOn2 = false;
            select2.SetActive(false);
        }
    }
    public void OpenTile3()
    {
        if (isSelectOn3 == false)
        {
            isSelectOn3 = true;
            select3.SetActive(true);
        }
    }
    public void CloseTile3()
    {
        if (isSelectOn3 == true)
        {
            isSelectOn3 = false;
            select3.SetActive(false);
        }
    }
}
