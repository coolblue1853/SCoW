using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using SCoW.Core.Save;
using SCoW.Core.Ui;

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
    private const float SummaryGap = 36f; // 슬롯 요약 한 줄이 들어갈 이어하기·옵션 사이 여백

    // 키보드·패드 탐색: 시작·(이어하기)·옵션·종료 순. NavUp/Down 으로 고르고 Interact 로 누른다.
    private readonly List<Transform> _navItems = new List<Transform>();
    private int _navIndex = -1;

    private void Start()
    {
        BuildContinueButton();
        BuildNavItems();
        BuildVersionLabel();
        // TODO(I2): 임시 영문 문구. 새 I2 키로 옮긴다.
        HintBar.Push(this,
            new Hint(HintAction.Navigate, "Select"),
            new Hint(HintAction.Confirm, "OK"));
    }

    private void OnDestroy()
    {
        HintBar.Remove(this);
        FocusScope.Highlight(this, null);
    }

    // 최근 슬롯 요약(일차·시간·장소·저장 시각)을 이어하기 버튼 아래에 작게 보인다.
    private void BuildSlotSummary(GameObject button, TMPro.TextMeshProUGUI label)
    {
        int slot = SaveFlow.LatestSlot();
        SaveMeta meta;
        if (slot < 0 || SaveService.ReadMeta(slot, out meta) != SaveError.None)
        {
            return;
        }
        GameObject go = new GameObject("SlotSummary", typeof(RectTransform));
        go.transform.SetParent(button.transform, false);
        TMPro.TextMeshProUGUI t = go.AddComponent<TMPro.TextMeshProUGUI>();
        if (label != null)
        {
            t.font = label.font;
            t.fontSharedMaterial = label.fontSharedMaterial;
        }
        t.raycastTarget = false;
        t.fontSize = 13f;
        t.alignment = TMPro.TextAlignmentOptions.Center;
        t.color = new Color(0.85f, 0.85f, 0.85f, 0.9f);
        t.enableWordWrapping = false;
        t.overflowMode = TMPro.TextOverflowModes.Overflow;
        // 요약은 영문 포맷. TMP 글꼴에 한글이 없으면 장소·시간 이름도 영문으로 맞춘다.
        bool prevForce = SaveText.ForceEnglish;
        SaveText.ForceEnglish = t.font == null || !t.font.HasCharacter('가', true);
        t.text = SlotSummary.Line(meta.Day, SaveText.TimeOfDay(meta.TimeCount), SaveText.PlaceName(meta.PlaceId), meta.SavedAtUtc.ToLocalTime());
        SaveText.ForceEnglish = prevForce;
        RectTransform rt = (RectTransform)go.transform;
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = new Vector2(0f, -16f); // 버튼 자식 단위(버튼 배율이 커서 값이 작다)
        rt.sizeDelta = new Vector2(600f, 24f);
    }

    // 화면 우하단 버전 표기(Player Settings 의 Version).
    private void BuildVersionLabel()
    {
        Canvas canvas = select1 != null ? select1.GetComponentInParent<Canvas>() : null;
        if (canvas == null)
        {
            return;
        }
        TMPro.TextMeshProUGUI src = select1.transform.parent.GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
        GameObject go = new GameObject("VersionLabel", typeof(RectTransform));
        go.transform.SetParent(canvas.transform, false);
        TMPro.TextMeshProUGUI t = go.AddComponent<TMPro.TextMeshProUGUI>();
        if (src != null)
        {
            t.font = src.font;
            t.fontSharedMaterial = src.fontSharedMaterial;
        }
        t.raycastTarget = false;
        t.fontSize = 14f;
        t.alignment = TMPro.TextAlignmentOptions.BottomRight;
        t.color = new Color(1f, 1f, 1f, 0.7f);
        t.text = SlotSummary.VersionLabel(Application.version);
        RectTransform rt = (RectTransform)go.transform;
        rt.anchorMin = new Vector2(1f, 0f);
        rt.anchorMax = new Vector2(1f, 0f);
        rt.pivot = new Vector2(1f, 0f);
        rt.anchoredPosition = new Vector2(-24f, 72f);
        rt.sizeDelta = new Vector2(300f, 36f);
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
        _navItems.Add(select3.transform.parent);
        _navItems.Add(select2.transform.parent);
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
        FocusScope.Highlight(this, _navItems[_navIndex] as RectTransform);
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
        Transform option = select3.transform.parent; // select3 = OptionButton, select2 = QuitButton
        Transform quit = select2.transform.parent;
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
        ((RectTransform)option).anchoredPosition = new Vector2(x, top - step * 2f - SummaryGap);
        ((RectTransform)quit).anchoredPosition = new Vector2(x, top - step * 3f - SummaryGap);
        _continueButton = go;
        BuildSlotSummary(go, label);
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
