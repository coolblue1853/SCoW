using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Intel_ObtoUI : MonoBehaviour
{
    private static Intel_ObtoUI instance = null;
    public GameObject arrow;
    public int keywordCounter;
    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public bool canKeyword;
    public bool canJudge;
    public string name;
    [TextArea]
    public string detail;
    bool playerInTrigger;
    private string previousState;

    bool isJudge = false;
    bool isKeyword = false;
    public string activeDilaog;
    public string lookDilaog;
    //  public string Select_Object;

    public void Update()
    {
        if (DataBaseManager.timeCount == 8 && this.name == "Meave" && isJudge == false)
        {
            isJudge = true;
        }
        if (DataBaseManager.timeCount == 5 && resetCheck == false)
        {
            resetCheck = true;
            ResetKeyCount();
        }
        if (this.name == "Aiden Triss")
        {
            SetAiden();
        }
        else if (this.name == "DeadBody")
        {
            SetDeadBody();
        }
        else if (this.name == "BlackWell")
        {
            SetBlackWell();
        }
        if (DataBaseManager.cancelJudge == true)
        {
            CancelJudge();
        }
        if (DataBaseManager.isActiveDialog2 == true && previousState != DataBaseManager.nowSelecter)
        {
            Reset();
        }
        previousState = DataBaseManager.nowSelecter;

        if (GameInput.PresentDown && UiGate.CanInteractWorld)
        {
            PresentDirect();
        }
        if ((GameInput.InteractDown && UiGate.CanInteractWorld) && !KeywordPicker.IsOpen)
        {
            if (this.transform.name == DataBaseManager.selectObject)
            {
                if (this.name == "Aiden Triss")
                {
                    arrow.SetActive(true);
                }
                else if (this.name == "DeadBody")
                {

                    arrow.SetActive(true);
                }
                else if (this.name == "BlackWell")
                {
                    arrow.SetActive(true);
                }
                else
                {
                    arrow.SetActive(false);
                }
                DataBaseManager.workSound = false;
                if (canJudge == false)
                {
                    isJudge = false;
                }
                else
                {
                    isJudge = true;
                }
                if (isJudge == false)
                {
                    //  DataBaseManager.isJudge = false;
                }
                else if (isJudge == true)
                {
                    // DataBaseManager.isJudge = true;
                    OnJudge();
                }
                if (canKeyword == false)
                {
                    isKeyword = false;
                }
                else
                {
                    isKeyword = true;
                }
                if (isKeyword == false)
                {
                    //    DataBaseManager.isKeyword = false;
                }
                else if (isKeyword == true)
                {
                    // DataBaseManager.isKeyword = true;
                    Onkeyword();
                }
                if (this.name == "GunshopMan")
                {
                    ShopUI.Instance.OpenShopUI();
                }
                if (DataBaseManager.isRollet == false)
                {
                    if (DataBaseManager.nowSelecter == "Keyword")
                    {
                        // K2: 옛 3단 선택기 대신 한 화면 목록(KeywordPicker). 확정하면 옛 흐름(차감·대사)을 그대로 탄다.
                        if (!KeywordPicker.ClosedThisFrame)
                        {
                            if (!KeywordPicker.Open(this.name, keywordCounter, OnKeywordPicked))
                            {
                                Debug.LogError("Intel_ObtoUI: 키워드 선택 창을 열지 못했다(" + this.name + ")");
                            }
                        }
                    }
                    else if (DataBaseManager.nowSelecter == "Look")
                    {
                        DialogManager.Instance.ChoiceExNextPage();
                        DataBaseManager.isSelect = true;
                        if (DataBaseManager.nowPlace == "DetectiveOffice")
                        {
                            InteractionController.Instance.Start1stDetectiveOffice(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Client'shouse")
                        {
                            if (DataBaseManager.timeCount < 7)
                            {
                                InteractionController.Instance.Start1stClientsHouse(lookDilaog);
                            }
                            else
                            {
                                InteractionController.Instance.Start2ndNoonEvent(lookDilaog);
                            }
                        }
                        if (DataBaseManager.nowPlace == "DailyNews")
                        {
                            InteractionController.Instance.Start1stDailyNews(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "University")
                        {
                            InteractionController.Instance.Start1stUniversity(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Riverside")
                        {
                            InteractionController.Instance.Start1stRiverside(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Hospital")
                        {
                            InteractionController.Instance.Start1stHospital(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Policeoffice")
                        {
                            InteractionController.Instance.Start1stPoliceoffice(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Slum")
                        {
                            InteractionController.Instance.Start1stSlum(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Bar")
                        {
                            InteractionController.Instance.Start1stBar(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "NoonEvent")
                        {
                            InteractionController.Instance.Start2ndNoonEvent(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "SewerOffice")
                        {
                            InteractionController.Instance.StartSewerOffice(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "InSewer")
                        {
                            InteractionController.Instance.InSewerDialog(lookDilaog);
                        }
                    }
                    else if (DataBaseManager.nowSelecter == "Judge")
                    {
                        DialogManager.Instance.EndDialog();
                        SetJudge();
                    }
                    else if (DataBaseManager.nowSelecter == "End" && DataBaseManager.isActiveDialog1 == false)
                    {
                        if (DataBaseManager.nowPlace == "DetectiveOffice")
                        {
                            InteractionController.Instance.Start1stDetectiveOffice(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Client'shouse")
                        {
                            if (DataBaseManager.timeCount < 7)
                            {
                                InteractionController.Instance.Start1stClientsHouse(activeDilaog);
                            }
                            else
                            {
                                InteractionController.Instance.Start2ndNoonEvent(activeDilaog);
                            }
                        }
                        if (DataBaseManager.nowPlace == "DailyNews")
                        {
                            InteractionController.Instance.Start1stDailyNews(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "University")
                        {
                            InteractionController.Instance.Start1stUniversity(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Riverside")
                        {
                            InteractionController.Instance.Start1stRiverside(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Hospital")
                        {
                            InteractionController.Instance.Start1stHospital(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Policeoffice")
                        {
                            InteractionController.Instance.Start1stPoliceoffice(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Slum")
                        {
                            InteractionController.Instance.Start1stSlum(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Bar")
                        {
                            InteractionController.Instance.Start1stBar(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "NoonEvent")
                        {
                            InteractionController.Instance.Start2ndNoonEvent(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "SewerOffice")
                        {
                            InteractionController.Instance.StartSewerOffice(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "InSewer")
                        {
                            InteractionController.Instance.InSewerDialog(activeDilaog);
                        }
                    }
                }
            }
        }
    }
    public GameObject judgeOb;
    public GameObject judgeBackOb;
    public GameObject keywordOb;
    public GameObject keywordBackOb;
    public void OnJudge()
    {
        judgeOb.SetActive(true);
        judgeBackOb.SetActive(true);
        DataBaseManager.isJudge = true;
    }
    private void OnKeywordPicked(SCoW.Core.Keyword.KeywordEntry entry)
    {
        DataBaseManager.keywordDowner = entry.Id;
        DialogManager.Instance.ChoiceExNextPage();
        DataBaseManager.isSelect = true;
        // K3: 전용 응답일 때만 횟수 소모(폴백은 환불)하고 물음을 기록한다. 정답 경로의 대사는 그대로.
        bool usedFallback = InteractionController.Instance.KeywordDialog(this.name);
        if (GameRoot.Instance == null) { return; }
        keywordCounter = SCoW.Core.Keyword.KeywordAsk.Settle(GameRoot.Instance.State, this.name, entry.Id, usedFallback, keywordCounter);
        RefreshKeywordLabel();
    }
    // 대화 선택 행의 키워드 칸에 남은 질문 수를 보인다(씬 텍스트라 NPC 가 바뀔 때마다 다시 쓴다).
    private void RefreshKeywordLabel()
    {
        if (keywordOb == null) { return; }
        TMPro.TMP_Text label = keywordOb.GetComponentInChildren<TMPro.TMP_Text>(true);
        if (label != null) { label.text = "keyword (" + keywordCounter + ")"; }
    }
    // K5: 선택 행에서 Present(F / 패드 Y) 를 누르면 탭 순환 없이 제시 창을 바로 연다. 대사 진행 중에는 선택 행이 없어 열리지 않는다.
    private void PresentDirect()
    {
        End_Look_Judge row = End_Look_Judge.Instance;
        if (row == null || !row.CanPresentKeyword || !canKeyword || this.transform.name != DataBaseManager.selectObject)
        {
            return;
        }
        if (DataBaseManager.isRollet || KeywordPicker.IsOpen || KeywordPicker.ClosedThisFrame)
        {
            return;
        }
        row.HighlightKeywordRow();
        if (!KeywordPicker.Open(this.name, keywordCounter, OnKeywordPicked))
        {
            Debug.LogError("Intel_ObtoUI: 키워드 선택 창을 열지 못했다(" + this.name + ")");
        }
    }

    public void Onkeyword()
    {
        RefreshKeywordLabel();
        keywordOb.SetActive(true);
        keywordBackOb.SetActive(true);
        DataBaseManager.isKeyword = true;
    }
    void SetJudge()
    {
        if (DataBaseManager.selectObject == "Sewer")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.SetRollet("Sewer : Picking", "Deftness", DataBaseManager.deftnessPoint, "dialog");
        }
        if (DataBaseManager.selectObject == "NewsPaper_Active")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.SetRollet("Newspaper : Read", "Analysis", DataBaseManager.analysisPoint, "dialog");
        }
        if (DataBaseManager.selectObject == "Stove_Active")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.SetRollet("Stove : Making coffee", "Luck", DataBaseManager.luk, "dialog");
        }
        if (DataBaseManager.selectObject == "Aiden Triss")
        {
            if (DataBaseManager.aidenMedicine == false)
            {
                aidenJudge = "medicine";
                Rollet.Instance.SetRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
            }
            else if (DataBaseManager.aidenObservation == false)
            {
                aidenJudge = "Observation";
                Rollet.Instance.SetRollet("Aiden : Look", "Observation", DataBaseManager.observationPoint, "dialog");
            }
            else if (DataBaseManager.aidenPsychotherapy == false)
            {
                aidenJudge = "psychotherapy";
                Rollet.Instance.SetRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
            }
        }
        if (DataBaseManager.selectObject == "Berkeley Swain")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.SetRollet("Swain : Look", "Observation", DataBaseManager.observationPoint, "dialog");
        }
        if (DataBaseManager.selectObject == "Witness")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.SetRollet("Witness : Eavesdropping", "Listening", DataBaseManager.listeningPoint, "dialog");
        }
        if (DataBaseManager.selectObject == "DoorPolice")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.SetRollet("Police : Persuasion", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
        }
        if (DataBaseManager.selectObject == "DeadBody")
        {
            if (DataBaseManager.deadBodyMedicine == false)
            {
                deadBodyJudge = "medicine";
                Rollet.Instance.SetRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
            }
            else if (DataBaseManager.deadBodyObservational == false)
            {
                deadBodyJudge = "Observation";
                Rollet.Instance.SetRollet("DeadBody : Look", "Observation", DataBaseManager.observationPoint, "dialog");
            }
            else if (DataBaseManager.deadBodyAnalyzing == false)
            {
                deadBodyJudge = "analysis";
                Rollet.Instance.SetRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
            }
            else if (DataBaseManager.deadBodyDeftness == false)
            {
                deadBodyJudge = "deftness";
                Rollet.Instance.SetRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
            }
        }
        if (DataBaseManager.selectObject == "Accident Site")
        {
            if (DataBaseManager.siteAnalyzing == false)
            {
                Rollet.Instance.SetRollet("Accident Site : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
            }
        }
        if (DataBaseManager.selectObject == "Maeve" && DataBaseManager.timeCount == 8)
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.SetRollet("Maeve : Persuasion", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
        }
        if (DataBaseManager.selectObject == "GoInside_Dialog")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.SetRollet("Door : Picking doors", "Deftness", DataBaseManager.deftnessPoint, "dialog");
        }
        if (DataBaseManager.selectObject == "Window")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.SetRollet("Window : Break", "STR", DataBaseManager.str, "dialog");
        }
        if (DataBaseManager.selectObject == "Locker")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.SetRollet("Locker : Take a Look", "Observation", DataBaseManager.observationPoint, "dialog");
        }
        if (DataBaseManager.selectObject == "BlackWell")
        {
            if (DataBaseManager.blackDeftness == false)
            {
                blackWellJudge = "deftness";
                Rollet.Instance.SetRollet("Warden : Steal", "Deftness", DataBaseManager.deftnessPoint, "dialog");
            }
            else if (DataBaseManager.blackRhethic == false)
            {
                blackWellJudge = "rhetoric";
                Rollet.Instance.SetRollet("Warden : Persuade", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
            }
        }
        if (DataBaseManager.selectObject == "Symbol")
        {
            Rollet.Instance.SetRollet("Symbol : Break", "STR", DataBaseManager.str, "dialog");
        }
    }
    string blackWellJudge;
    void SetBlackWell()
    {
        if (DataBaseManager.blackRhethic == true && DataBaseManager.blackDeftness == true)
        {
            canJudge = false;
            isJudge = true;
        }
        if (Rollet.Instance.rolletSetUi.activeSelf == true)
        {
            if (GameInput.PageNextDown || GameInput.PagePrevDown)
            {
                if (blackWellJudge == "deftness")
                {
                    if (DataBaseManager.blackRhethic == false)
                    {
                        blackWellJudge = "rhetoric";
                        Rollet.Instance.SetRollet("Warden : Persuade", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
                    }
                }
                else if (blackWellJudge == "rhetoric")
                {
                    if (DataBaseManager.blackDeftness == false)
                    {
                        blackWellJudge = "deftness";
                        Rollet.Instance.SetRollet("Warden : Steal", "Deftness", DataBaseManager.deftnessPoint, "dialog");
                    }
                }
            }
        }
    }
    string aidenJudge;
    void SetAiden()
    {
        if (DataBaseManager.aidenFirstDialog == false)
        {
            activeDilaog = "Aiden_FirstDialog";
        }
        else
        {
            DataBaseManager.aidenFirstDialog = true;
            activeDilaog = "Aiden_Dialog1";
        }
        if (DataBaseManager.aidenMedicine == true && DataBaseManager.aidenObservation == true && DataBaseManager.aidenPsychotherapy == true)
        {
            canJudge = false;
            isJudge = true;
        }
        if (Rollet.Instance.rolletSetUi.activeSelf == true)
        {
            if (GameInput.PageNextDown)
            {
                if (aidenJudge == "medicine")
                {
                    if (DataBaseManager.aidenObservation == false)
                    {
                        aidenJudge = "Observation";
                        Rollet.Instance.SetRollet("Aiden : Look", "Observation", DataBaseManager.observationPoint, "dialog");
                    }
                    else if (DataBaseManager.aidenPsychotherapy == false)
                    {
                        aidenJudge = "psychotherapy";
                        Rollet.Instance.SetRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                }
                else if (aidenJudge == "Observation")
                {
                    if (DataBaseManager.aidenPsychotherapy == false)
                    {
                        aidenJudge = "psychotherapy";
                        Rollet.Instance.SetRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                    else if (DataBaseManager.aidenMedicine == false)
                    {
                        aidenJudge = "medicine";
                        Rollet.Instance.SetRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                }
                else if (aidenJudge == "psychotherapy")
                {

                    if (DataBaseManager.aidenMedicine == false)
                    {
                        aidenJudge = "medicine";
                        Rollet.Instance.SetRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.aidenObservation == false)
                    {
                        aidenJudge = "Observation";
                        Rollet.Instance.SetRollet("Aiden : Look", "Observation", DataBaseManager.observationPoint, "dialog");
                    }
                }
            }
            if (GameInput.PagePrevDown)
            {
                if (aidenJudge == "medicine")
                {
                    if (DataBaseManager.aidenPsychotherapy == false)
                    {
                        aidenJudge = "psychotherapy";
                        Rollet.Instance.SetRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                    else if (DataBaseManager.aidenObservation == false)
                    {
                        aidenJudge = "Observation";
                        Rollet.Instance.SetRollet("Aiden : Look", "Observation", DataBaseManager.observationPoint, "dialog");
                    }
                }
                else if (aidenJudge == "Observation")
                {
                    if (DataBaseManager.aidenMedicine == false)
                    {
                        aidenJudge = "medicine";
                        Rollet.Instance.SetRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.aidenPsychotherapy == false)
                    {
                        aidenJudge = "psychotherapy";
                        Rollet.Instance.SetRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                }
                else if (aidenJudge == "psychotherapy")
                {
                    if (DataBaseManager.aidenObservation == false)
                    {
                        aidenJudge = "Observation";
                        Rollet.Instance.SetRollet("Aiden : Look", "Observation", DataBaseManager.observationPoint, "dialog");
                    }
                    else if (DataBaseManager.aidenMedicine == false)
                    {
                        aidenJudge = "medicine";
                        Rollet.Instance.SetRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                }
            }
        }
    }
    string deadBodyJudge;
    void SetDeadBody()
    {
        if (DataBaseManager.deadBodyMedicine == true && DataBaseManager.deadBodyObservational == true && DataBaseManager.deadBodyAnalyzing == true && DataBaseManager.deadBodyDeftness == true)
        {
            canJudge = false;
            isJudge = true;
        }
        if (Rollet.Instance.rolletSetUi.activeSelf == true)
        {
            if (GameInput.PageNextDown)
            {
                if (deadBodyJudge == "medicine")
                {
                    if (DataBaseManager.deadBodyObservational == false)
                    {
                        deadBodyJudge = "Observation";
                        Rollet.Instance.SetRollet("DeadBody : Look", "Observation", DataBaseManager.observationPoint, "dialog");
                    }
                    else if (DataBaseManager.deadBodyAnalyzing == false)
                    {
                        deadBodyJudge = "analysis";
                        Rollet.Instance.SetRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                    else if (DataBaseManager.deadBodyDeftness == false)
                    {
                        deadBodyJudge = "deftness";
                        Rollet.Instance.SetRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                }
                else if (deadBodyJudge == "Observation")
                {
                    if (DataBaseManager.deadBodyAnalyzing == false)
                    {
                        deadBodyJudge = "analysis";
                        Rollet.Instance.SetRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                    else if (DataBaseManager.deadBodyDeftness == false)
                    {
                        deadBodyJudge = "deftness";
                        Rollet.Instance.SetRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                    else if (DataBaseManager.deadBodyMedicine == false)
                    {
                        deadBodyJudge = "medicine";
                        Rollet.Instance.SetRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                }
                else if (deadBodyJudge == "analysis")
                {
                    if (DataBaseManager.deadBodyDeftness == false)
                    {
                        deadBodyJudge = "deftness";
                        Rollet.Instance.SetRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }

                    else if (DataBaseManager.deadBodyMedicine == false)
                    {
                        deadBodyJudge = "medicine";
                        Rollet.Instance.SetRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.deadBodyObservational == false)
                    {
                        deadBodyJudge = "Observation";
                        Rollet.Instance.SetRollet("DeadBody : Look", "Observation", DataBaseManager.observationPoint, "dialog");
                    }
                }
                else if (deadBodyJudge == "deftness")
                {
                    if (DataBaseManager.deadBodyMedicine == false)
                    {
                        deadBodyJudge = "medicine";
                        Rollet.Instance.SetRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.deadBodyObservational == false)
                    {
                        deadBodyJudge = "Observation";
                        Rollet.Instance.SetRollet("DeadBody : Look", "Observation", DataBaseManager.observationPoint, "dialog");
                    }
                    else if (DataBaseManager.deadBodyAnalyzing == false)
                    {
                        deadBodyJudge = "analysis";
                        Rollet.Instance.SetRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                }
            }
            if (GameInput.PagePrevDown)
            {
                if (deadBodyJudge == "medicine")
                {
                    if (DataBaseManager.deadBodyDeftness == false)
                    {
                        deadBodyJudge = "deftness";
                        Rollet.Instance.SetRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                    else if (DataBaseManager.deadBodyAnalyzing == false)
                    {
                        deadBodyJudge = "analysis";
                        Rollet.Instance.SetRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                    else if (DataBaseManager.deadBodyObservational == false)
                    {
                        deadBodyJudge = "Observation";
                        Rollet.Instance.SetRollet("DeadBody : Look", "Observation", DataBaseManager.observationPoint, "dialog");
                    }
                }
                else if (deadBodyJudge == "Observation")
                {
                    if (DataBaseManager.deadBodyMedicine == false)
                    {
                        deadBodyJudge = "medicine";
                        Rollet.Instance.SetRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.deadBodyDeftness == false)
                    {
                        deadBodyJudge = "deftness";
                        Rollet.Instance.SetRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                    else if (DataBaseManager.deadBodyAnalyzing == false)
                    {
                        deadBodyJudge = "analysis";
                        Rollet.Instance.SetRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                }
                else if (deadBodyJudge == "analysis")
                {
                    if (DataBaseManager.deadBodyObservational == false)
                    {
                        deadBodyJudge = "Observation";
                        Rollet.Instance.SetRollet("DeadBody : Look", "Observation", DataBaseManager.observationPoint, "dialog");
                    }
                    else if (DataBaseManager.deadBodyMedicine == false)
                    {
                        deadBodyJudge = "medicine";
                        Rollet.Instance.SetRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.deadBodyDeftness == false)
                    {
                        deadBodyJudge = "deftness";
                        Rollet.Instance.SetRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                }
                else if (deadBodyJudge == "deftness")
                {
                    if (DataBaseManager.deadBodyAnalyzing == false)
                    {
                        deadBodyJudge = "analysis";
                        Rollet.Instance.SetRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                    else if (DataBaseManager.deadBodyObservational == false)
                    {
                        deadBodyJudge = "Observation";
                        Rollet.Instance.SetRollet("DeadBody : Look", "Observation", DataBaseManager.observationPoint, "dialog");
                    }
                    else if (DataBaseManager.deadBodyMedicine == false)
                    {
                        deadBodyJudge = "medicine";
                        Rollet.Instance.SetRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                }
            }
        }
    }
    private void Reset()
    {
        // DataBaseManager.NowSelecter = "End";
        DataBaseManager.isActiveDialog2 = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInTrigger = true;
        if (collision.tag == "Player")
        {
            DataBaseManager.selectObject = this.transform.name;
            BillowUIManager.Instance.SetIntelUi(name, detail);
            // TODO(I2): 문구를 I2 키로.
            InteractHintView.Enter(transform, SCoW.Core.Ui.HintAction.Interact, "Inspect", 2.2f);
        }
    }
    public void ExDetailChange()
    {
        BillowUIManager.Instance.SetIntelUi(name, detail);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInTrigger = false;
        if (collision.tag == "Player")
        {
            DataBaseManager.selectObject = "";
            BillowUIManager.Instance.ResetIntelUi();
            InteractHintView.Exit(transform);
        }
    }
    public void CancelJudge()
    {
        if (DataBaseManager.selectObject == this.transform.name)
        {
            DataBaseManager.cancelJudge = false;
            isJudge = true;
            canJudge = true;
            // 옛 TimeManagere.Update 는 매 프레임 사무실 문 상태에 맞춰 canJudge 를 덮어썼다. 지금은 알림이 올 때만 평가한다(C-2 P13).
            GameRoot.NotifyTransient(SCoW.Core.World.WorldIds.RefreshKey);
        }
    }
    bool resetCheck = false;

    public void ResetKeyCount()
    {
        isJudge = true;
        canJudge = true;
        GameRoot.NotifyTransient(SCoW.Core.World.WorldIds.RefreshKey);
        // K7: NPC 별 횟수는 Resources/Story/KeywordBudget.json(KeywordBudget). 표에 없는 NPC 는 그대로 둔다.
        int budget;
        SCoW.Core.Keyword.KeywordBudget table = GetBudgetTable();
        if (table != null && table.TryGet(this.name, out budget))
        {
            keywordCounter = budget;
        }
    }

    private static SCoW.Core.Keyword.KeywordBudget _budgetTable;

    private static SCoW.Core.Keyword.KeywordBudget GetBudgetTable()
    {
        if (_budgetTable != null)
        {
            return _budgetTable;
        }
        TextAsset ta = Resources.Load<TextAsset>("Story/KeywordBudget");
        if (ta == null)
        {
            Debug.LogError("Intel_ObtoUI: Resources/Story/KeywordBudget.json 를 찾지 못했다.");
            return null;
        }
        System.Collections.Generic.List<string> errors = new System.Collections.Generic.List<string>();
        _budgetTable = SCoW.Core.Keyword.KeywordBudget.Load(ta.text, errors);
        if (_budgetTable == null)
        {
            Debug.LogError("Intel_ObtoUI: KeywordBudget.json 오류 - " + string.Join(" | ", errors.ToArray()));
        }
        return _budgetTable;
    }
}
