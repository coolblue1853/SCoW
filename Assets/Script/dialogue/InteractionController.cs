using UnityEngine;
using SCoW.Core.Story;

public partial class InteractionController : MonoBehaviour
{
    public DialogManager theDM;

    /// <summary>하수도 안 대사(C-2 P12 이후 데이터 주도: Resources/Story/InSewer.json). 씬 interactionEvent 오브젝트·인스펙터 범위는 남기되 이 경로는 읽지 않는다.</summary>
    public void InSewerDialog(string setDialog)
    {
        RunStory("InSewer", setDialog);
    }
    /// <summary>하수도 사무실 대사(C-2 P12 이후 데이터 주도: Resources/Story/SewerOffice.json).</summary>
    public void StartSewerOffice(string setDialog)
    {
        RunStory("SewerOffice", setDialog);
    }
    /// <summary>낮 사건 대사(C-2 P10b 이후 데이터 주도: Resources/Story/2nd_NoonEvent.json).</summary>
    public void Start2ndNoonEvent(string setDialog)
    {
        RunStory("2nd_NoonEvent", setDialog);
    }
    /// <summary>2일차 탐정사무소 시작 대사(C-2 P10b 이후 데이터 주도: Resources/Story/2nd_DetectiveOffice.json, 줄은 1st_DetectiveOffice CSV).</summary>
    public void Start2ndDetectiveOffice(string setDialog)
    {
        RunStory("2nd_DetectiveOffice", setDialog);
    }
    /// <summary>금고·아이템 살펴보기 대사(C-2 P10b 이후 데이터 주도: Resources/Story/SafeLook.json, 줄은 1st_Item CSV).</summary>
    public void StartSafeLook(string setDialog)
    {
        RunStory("SafeLook", setDialog);
    }
    // 1st
    /// <summary>StoryDatabase 로 topic 을 풀어 대사를 띄운다. 맞는 Topic 이 없으면 아무것도 안 한다(옛 if 체인과 같음).</summary>
    private void RunStory(string scriptId, string topicId)
    {
        Dialog[] dialogs;
        if (StoryDatabase.TryRun(scriptId, topicId, out dialogs))
        {
            theDM.ShowDialog(dialogs);
        }
    }
    /// <summary>1일차 탐정사무소 대사(C-2 P9 이후 데이터 주도: Resources/Story/1st_DetectiveOffice.json). 씬 interactionEvent 오브젝트·인스펙터 범위는 남기되 이 경로는 읽지 않는다.</summary>
    public void Start1stDetectiveOffice(string setDialog)
    {
        RunStory("1st_DetectiveOffice", setDialog);
    }
    public void Start1stClientsHouse(string setDialog)
    {
        RunStory("1st_Client'sHouse", setDialog);
    }
    public void Start1stDailyNews(string setDialog)
    {
        RunStory("1st_DailyNews", setDialog);
    }
    public void Start1stUniversity(string setDialog)
    {
        RunStory("1st_University", setDialog);
    }
    public void Start1stRiverside(string setDialog)
    {
        RunStory("1st_Riverside", setDialog);
    }
    public void Start1stHospital(string setDialog)
    {
        RunStory("1st_Hospital", setDialog);
    }
    /// <summary>경찰서 대사(C-2 P10b 이후 데이터 주도: Resources/Story/1st_PoliceOffice.json).</summary>
    public void Start1stPoliceoffice(string setDialog)
    {
        RunStory("1st_PoliceOffice", setDialog);
    }
    /// <summary>빈민가 대사(C-2 P10b 이후 데이터 주도: Resources/Story/1st_Slum.json). 데이브 랜덤 대사는 대사 전에 키워드 UI 를 켜는 씬 부수효과가 있어 여기서 먼저 한다(옛 순서 그대로).</summary>
    public void Start1stSlum(string setDialog)
    {
        switch (setDialog)
        {
            case "Dave_Ran1":
            case "Dave_Ran2":
            case "Dave_Ran3":
            case "Dave_Ran4":
            case "Dave_Ran5":
            case "Dave_Ran6":
            case "Dave_Ran7":
            case "Dave_Ran8":
            case "Dave_Ran9":
            case "Dave_NoMoney":
            case "Dave_RanEnd":
                End_Look_Judge.Instance.DaveRandActive();
                break;
        }
        RunStory("1st_Slum", setDialog);
    }
    /// <summary>바 대사(C-2 P9 이후 데이터 주도: Resources/Story/1st_Bar.json).</summary>
    public void Start1stBar(string setDialog)
    {
        RunStory("1st_Bar", setDialog);
    }
    /// <summary>
    /// 키워드 선택 대사(C-2 P11 이후 데이터 주도: Resources/Story/Keyword.json, 토픽 "NPC/키워드"). NPC 이름은 setDialog,
    /// 키워드는 DataBaseManager.keywordDowner. 씬 interactionEvent 오브젝트·인스펙터 범위는 남기되 이 경로는 읽지 않는다.
    /// </summary>
    public void KeywordDialog(string setDialog)
    {
        if (setDialog == "Dusty Dave")
        {
            // 대사 전 씬 부수효과(JSON 에 못 옮김): 빈민가 선택지 UI 닫기.
            selectionUIManager.Instance.EndDialog();
        }
        Dialog[] dialogs;
        if (StoryDatabase.TryRunKeyword(setDialog, DataBaseManager.keywordDowner, out dialogs))
        {
            theDM.ShowDialog(dialogs);
        }
    }
    /// <summary>전투 시작·종료 대사(C-2 P11 이후 데이터 주도: Resources/Story/1st_Battle.json).</summary>
    public void BattleDialog(string setDialog)
    {
        RunStory("1st_Battle", setDialog);
    }
    /// <summary>
    /// 판정 결과 어댑터(C-2 P12 이후 데이터 주도: Resources/Story/Judge.json, 규칙은 Core StoryResolver.ResolveJudge).
    /// Sub_Dialog = 판정 이름, result_End = Rollet 결과 문자열(LegacyString 그대로), SanCheck = 이성 판정 키.
    /// 옛 순서대로 판정 이름 블록 -> SAN 키 블록 순으로 각각 대사를 띄운다(둘 다 맞으면 두 번 — 옛 동작 보존).
    /// </summary>
    public void RetrunDialogResult(string Sub_Dialog, string result_End, string SanCheck = "")
    {
        ApplyJudgeScene(Sub_Dialog, JudgeOutcomes.FromLegacy(result_End));
        RunJudge(Sub_Dialog, result_End);
        if (!string.IsNullOrEmpty(SanCheck))
        {
            RunJudge("SanCheck/" + SanCheck, result_End);
        }
    }

    private void RunJudge(string key, string result)
    {
        Dialog[] dialogs;
        if (StoryDatabase.TryRunJudge(key, result, out dialogs))
        {
            theDM.ShowDialog(dialogs);
        }
    }

    /// <summary>상태(플래그·카운터)로 못 옮긴 씬 쪽 부수효과(JudgeSceneRules 가 정한 것)를 실행한다.</summary>
    private void ApplyJudgeScene(string sub, JudgeOutcome outcome)
    {
        JudgeScene plan = JudgeSceneRules.Plan(sub, outcome);
        if ((plan & JudgeScene.ConditionGood) != 0) { DataBaseManager.condition = "Good"; }
        if ((plan & JudgeScene.StoryDirecting) != 0) { DataBaseManager.storyDirecting = true; }
        if ((plan & JudgeScene.HpDown5) != 0) { BillowUIManager.Instance.HpDown(5); }
        if ((plan & JudgeScene.SwimRetry) != 0) { Invoke("SwinCheckAgain", 0.5f); }
    }

    void SwinCheckAgain()
    {
        Rollet.Instance.SetRollet("Fabian : Swimming", "Swin", DataBaseManager.swimingPoint, "dialog");
    }
    private static InteractionController instance = null;
    private void Awake()
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
    public static InteractionController Instance
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

