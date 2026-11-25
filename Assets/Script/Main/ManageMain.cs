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

    // 키보드·패드 탐색: 시작·(이어하기)·옵션·종료 순. NavUp/Down 으로 고르고 Interact 로 누른다.
    private readonly List<Transform> _navItems = new List<Transform>();
    private int _navIndex = -1;

    private void Start()
    {
        BuildContinueButton();
        BuildNavItems();
    }

    private void BuildNavItems()
    {
        _navItems.Clear();
        if (select1 == null || select2 == null || select3 == null)
        {
            return;
        }
        _navItems.Add(select1.transform.parent);
        if (_continueButton != null)
        {
            _navItems.Add(_continueButton.transform);
        }
        _navItems.Add(select2.transform.parent);
        _navItems.Add(select3.transform.parent);
    }

    private void MoveNav(int dir)
    {
        if (_navItems.Count == 0)
        {
            return;
        }
        SetNavHighlight(_navIndex, false);
        _navIndex = _navIndex < 0 ? (dir > 0 ? 0 : _navItems.Count - 1) : (_navIndex + dir + _navItems.Count) % _navItems.Count;
        SetNavHighlight(_navIndex, true);
    }

    private void SetNavHighlight(int index, bool on)
    {
        if (index < 0 || index >= _navItems.Count || _navItems[index] == null)
        {
            return;
        }
        Transform h = _navItems[index].Find("Select");
        if (h != null)
        {
            h.gameObject.SetActive(on);
        }
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
        // 설정창·슬롯창이 열려 있으면 무시.
        if (SlotMenuView.IsOpen || UiGate.AnyOpen)
        {
            return;
        }
        if (GameInput.NavUpDown)
        {
            MoveNav(-1);
        }
        else if (GameInput.NavDownDown)
        {
            MoveNav(1);
        }
        else if (GameInput.InteractDown)
        {
            if (_navIndex >= 0 && _navIndex < _navItems.Count && _navItems[_navIndex] != null)
            {
                UnityEngine.EventSystems.ExecuteEvents.Execute(_navItems[_navIndex].gameObject,
                    new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current),
                    UnityEngine.EventSystems.ExecuteEvents.pointerClickHandler);
            }
            else if (_continueButton != null)
            {
                OpenContinue(); // 선택 없이 E/패드 남쪽: 이어하기(기존 동작)
            }
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
        // 타이틀 Start 가 확정된 새 판의 시작점(FadeIn 완료). 캐릭터 생성에는 타이틀로 돌아가는 길이 없다.
        SaveFlow.InvalidateQuickSlot();
        SceneRouter.Go(SceneIds.SetCharacter);
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
