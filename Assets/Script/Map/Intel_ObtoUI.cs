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

        if ((Input.GetKeyDown(KeyCode.E) && DataBaseManager.isActiveDialog2 == false && DataBaseManager.storyDirecting == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false && DataBaseManager.isOpenUi == false))
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
                if (isKeyword == false || keywordCounter == 0)
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
                        DialogManager.Instance.ChoiceExNextPage();
                        keywordCounter -= 1;
                        DataBaseManager.isSelect = true;
                        InteractionController.Instance.KeywordDialog(this.name);
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
    public void Onkeyword()
    {
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
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
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
            if (Input.GetKeyDown(KeyCode.D))
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
            if (Input.GetKeyDown(KeyCode.A))
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
            if (Input.GetKeyDown(KeyCode.D))
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
            if (Input.GetKeyDown(KeyCode.A))
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
        }
    }
    public void CancelJudge()
    {
        if (DataBaseManager.selectObject == this.transform.name)
        {
            DataBaseManager.cancelJudge = false;
            isJudge = true;
            canJudge = true;
        }
    }
    bool resetCheck = false;

    public void ResetKeyCount()
    {
        isJudge = true;
        canJudge = true;
        if (this.name == "Ella Triss")
        {
            keywordCounter = 3;
        }
        else if (this.name == "Aiden Triss")
        {
            keywordCounter = 3;
        }
        else if (this.name == "Berkeley Swain")
        {
            keywordCounter = 4;
        }
        else if (this.name == "University student")
        {
            keywordCounter = 3;
        }
        else if (this.name == "Kane")
        {
            keywordCounter = 3;
        }
        else if (this.name == "Maeve")
        {
            keywordCounter = 5;
        }
        else if (this.name == "Kate Lizzie")
        {
            keywordCounter = 4;
        }
        else if (this.name == "Alan Parks")
        {
            keywordCounter = 4;
        }
        else if (this.name == "Albert Bradley")
        {
            keywordCounter = 4;
        }
        else if (this.name == "BlackWell")
        {
            keywordCounter = 3;
        }
        else if (this.name == "Dusty Dave")
        {
            keywordCounter = 3;
        }
    }
}
