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
        if (DataBaseManager.TimeCount == 8 && this.name == "Meave" && isJudge == false)
        {
            isJudge = true;
        }
        if (DataBaseManager.TimeCount == 5 && resetCheck == false)
        {
            resetCheck = true;
            ResetKeyCount();
        }
        if (this.name == "Aiden Triss")
        {
            Set_Aiden();
        }
        else if (this.name == "DeadBody")
        {
            Set_DeadBody();
        }
        else if (this.name == "BlackWell")
        {
            Set_BlackWell();
        }
        if (DataBaseManager.CancelJudge == true)
        {
            CancelJudge();
        }
        if (DataBaseManager.isActiveDialog2 == true && previousState != DataBaseManager.NowSelecter)
        {
            Res();
        }
        previousState = DataBaseManager.NowSelecter;

        if ((Input.GetKeyDown(KeyCode.E) && DataBaseManager.isActiveDialog2 == false && DataBaseManager.StoryDirecting == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false && DataBaseManager.isOpenUi == false))
        {
            if (this.transform.name == DataBaseManager.Select_Object)
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
                    if (DataBaseManager.NowSelecter == "Keyword")
                    {
                        DialogManager.Instance.ChoiceEx_NextPage();
                        keywordCounter -= 1;
                        DataBaseManager.isSelect = true;
                        InteractionController.Instance.KeywordDialog(this.name);
                    }
                    else if (DataBaseManager.NowSelecter == "Look")
                    {
                        DialogManager.Instance.ChoiceEx_NextPage();
                        DataBaseManager.isSelect = true;
                        if (DataBaseManager.nowPlace == "DetectiveOffice")
                        {
                            InteractionController.Instance.Start_1st_DetectiveOffice(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Client'shouse")
                        {
                            if (DataBaseManager.TimeCount < 7)
                            {
                                InteractionController.Instance.Start_1st_ClientsHouse(lookDilaog);
                            }
                            else
                            {
                                InteractionController.Instance.Start_2nd_NoonEvent(lookDilaog);
                            }
                        }
                        if (DataBaseManager.nowPlace == "DailyNews")
                        {
                            InteractionController.Instance.Start_1st_DailyNews(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "University")
                        {
                            InteractionController.Instance.Start_1st_University(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Riverside")
                        {
                            InteractionController.Instance.Start_1st_Riverside(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Hospital")
                        {
                            InteractionController.Instance.Start_1st_Hospital(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Policeoffice")
                        {
                            InteractionController.Instance.Start_1st_Policeoffice(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Slum")
                        {
                            InteractionController.Instance.Start_1st_Slum(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Bar")
                        {
                            InteractionController.Instance.Start_1st_Bar(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "NoonEvent")
                        {
                            InteractionController.Instance.Start_2nd_NoonEvent(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "SewerOffice")
                        {
                            InteractionController.Instance.Start_SewerOffice(lookDilaog);
                        }
                        if (DataBaseManager.nowPlace == "InSewer")
                        {
                            InteractionController.Instance.InSewerDialog(lookDilaog);
                        }
                    }
                    else if (DataBaseManager.NowSelecter == "Judge")
                    {
                        DialogManager.Instance.EndDialog();
                        SetJudge();
                    }
                    else if (DataBaseManager.NowSelecter == "End" && DataBaseManager.isActiveDialog1 == false)
                    {
                        if (DataBaseManager.nowPlace == "DetectiveOffice")
                        {
                            InteractionController.Instance.Start_1st_DetectiveOffice(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Client'shouse")
                        {
                            if (DataBaseManager.TimeCount < 7)
                            {
                                InteractionController.Instance.Start_1st_ClientsHouse(activeDilaog);
                            }
                            else
                            {
                                InteractionController.Instance.Start_2nd_NoonEvent(activeDilaog);
                            }
                        }
                        if (DataBaseManager.nowPlace == "DailyNews")
                        {
                            InteractionController.Instance.Start_1st_DailyNews(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "University")
                        {
                            InteractionController.Instance.Start_1st_University(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Riverside")
                        {
                            InteractionController.Instance.Start_1st_Riverside(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Hospital")
                        {
                            InteractionController.Instance.Start_1st_Hospital(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Policeoffice")
                        {
                            InteractionController.Instance.Start_1st_Policeoffice(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Slum")
                        {
                            InteractionController.Instance.Start_1st_Slum(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "Bar")
                        {
                            InteractionController.Instance.Start_1st_Bar(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "NoonEvent")
                        {
                            InteractionController.Instance.Start_2nd_NoonEvent(activeDilaog);
                        }
                        if (DataBaseManager.nowPlace == "SewerOffice")
                        {
                            InteractionController.Instance.Start_SewerOffice(activeDilaog);
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
        if (DataBaseManager.Select_Object == "Sewer")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Sewer : Picking", "Deftness", DataBaseManager.deftnessPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "NewsPaper_Active")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Newspaper : Read", "Analysis", DataBaseManager.analysisPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "Stove_Active")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Stove : Making coffee", "Luck", DataBaseManager.luk, "dialog");
        }
        if (DataBaseManager.Select_Object == "Aiden Triss")
        {
            if (DataBaseManager.Aiden_medicine == false)
            {
                aidenJudge = "medicine";
                Rollet.Instance.setRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
            }
            else if (DataBaseManager.Aiden_Observation == false)
            {
                aidenJudge = "Observation";
                Rollet.Instance.setRollet("Aiden : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
            }
            else if (DataBaseManager.Aiden_psychotherapy == false)
            {
                aidenJudge = "psychotherapy";
                Rollet.Instance.setRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
            }
        }
        if (DataBaseManager.Select_Object == "Berkeley Swain")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Swain : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "Witness")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Witness : Eavesdropping", "Listening", DataBaseManager.listeningPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "DoorPolice")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Police : Persuasion", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "DeadBody")
        {
            if (DataBaseManager.DeadBody_medicine == false)
            {
                deadBodyJudge = "medicine";
                Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
            }
            else if (DataBaseManager.DeadBody_observational == false)
            {
                deadBodyJudge = "Observation";
                Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
            }
            else if (DataBaseManager.DeadBody_Analyzing == false)
            {
                deadBodyJudge = "analysis";
                Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
            }
            else if (DataBaseManager.DeadBody_deftness == false)
            {
                deadBodyJudge = "deftness";
                Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
            }
        }
        if (DataBaseManager.Select_Object == "Accident Site")
        {
            if (DataBaseManager.Site_Analyzing == false)
            {
                Rollet.Instance.setRollet("Accident Site : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
            }
        }
        if (DataBaseManager.Select_Object == "Maeve" && DataBaseManager.TimeCount == 8)
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Maeve : Persuasion", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "GoInside_Dialog")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Door : Picking doors", "Deftness", DataBaseManager.deftnessPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "Window")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Window : Break", "STR", DataBaseManager.str, "dialog");
        }
        if (DataBaseManager.Select_Object == "Locker")
        {
            canJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Locker : Take a Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "BlackWell")
        {
            if (DataBaseManager.Black_Deftness == false)
            {
                blackWellJudge = "deftness";
                Rollet.Instance.setRollet("Warden : Steal", "Deftness", DataBaseManager.deftnessPoint, "dialog");
            }
            else if (DataBaseManager.Black_Rhethic == false)
            {
                blackWellJudge = "rhetoric";
                Rollet.Instance.setRollet("Warden : Persuade", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
            }
        }
        if (DataBaseManager.Select_Object == "Symbol")
        {
            Rollet.Instance.setRollet("Symbol : Break", "STR", DataBaseManager.str, "dialog");
        }
    }
    string blackWellJudge;
    void Set_BlackWell()
    {
        if (DataBaseManager.Black_Rhethic == true && DataBaseManager.Black_Deftness == true)
        {
            canJudge = false;
            isJudge = true;
        }
        if (Rollet.Instance.RolletSetUi.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
            {
                if (blackWellJudge == "deftness")
                {
                    if (DataBaseManager.Black_Rhethic == false)
                    {
                        blackWellJudge = "rhetoric";
                        Rollet.Instance.setRollet("Warden : Persuade", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
                    }
                }
                else if (blackWellJudge == "rhetoric")
                {
                    if (DataBaseManager.Black_Deftness == false)
                    {
                        blackWellJudge = "deftness";
                        Rollet.Instance.setRollet("Warden : Steal", "Deftness", DataBaseManager.deftnessPoint, "dialog");
                    }
                }
            }
        }
    }
    string aidenJudge;
    void Set_Aiden()
    {
        if (DataBaseManager.Aiden_FirstDialog == false)
        {
            activeDilaog = "Aiden_FirstDialog";
        }
        else
        {
            DataBaseManager.Aiden_FirstDialog = true;
            activeDilaog = "Aiden_Dialog1";
        }
        if (DataBaseManager.Aiden_medicine == true && DataBaseManager.Aiden_Observation == true && DataBaseManager.Aiden_psychotherapy == true)
        {
            canJudge = false;
            isJudge = true;
        }
        if (Rollet.Instance.RolletSetUi.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (aidenJudge == "medicine")
                {
                    if (DataBaseManager.Aiden_Observation == false)
                    {
                        aidenJudge = "Observation";
                        Rollet.Instance.setRollet("Aiden : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.Aiden_psychotherapy == false)
                    {
                        aidenJudge = "psychotherapy";
                        Rollet.Instance.setRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                }
                else if (aidenJudge == "Observation")
                {
                    if (DataBaseManager.Aiden_psychotherapy == false)
                    {
                        aidenJudge = "psychotherapy";
                        Rollet.Instance.setRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                    else if (DataBaseManager.Aiden_medicine == false)
                    {
                        aidenJudge = "medicine";
                        Rollet.Instance.setRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                }
                else if (aidenJudge == "psychotherapy")
                {

                    if (DataBaseManager.Aiden_medicine == false)
                    {
                        aidenJudge = "medicine";
                        Rollet.Instance.setRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.Aiden_Observation == false)
                    {
                        aidenJudge = "Observation";
                        Rollet.Instance.setRollet("Aiden : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (aidenJudge == "medicine")
                {
                    if (DataBaseManager.Aiden_psychotherapy == false)
                    {
                        aidenJudge = "psychotherapy";
                        Rollet.Instance.setRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                    else if (DataBaseManager.Aiden_Observation == false)
                    {
                        aidenJudge = "Observation";
                        Rollet.Instance.setRollet("Aiden : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                }
                else if (aidenJudge == "Observation")
                {
                    if (DataBaseManager.Aiden_medicine == false)
                    {
                        aidenJudge = "medicine";
                        Rollet.Instance.setRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.Aiden_psychotherapy == false)
                    {
                        aidenJudge = "psychotherapy";
                        Rollet.Instance.setRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                }
                else if (aidenJudge == "psychotherapy")
                {
                    if (DataBaseManager.Aiden_Observation == false)
                    {
                        aidenJudge = "Observation";
                        Rollet.Instance.setRollet("Aiden : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.Aiden_medicine == false)
                    {
                        aidenJudge = "medicine";
                        Rollet.Instance.setRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                }
            }
        }
    }
    string deadBodyJudge;
    void Set_DeadBody()
    {
        if (DataBaseManager.DeadBody_medicine == true && DataBaseManager.DeadBody_observational == true && DataBaseManager.DeadBody_Analyzing == true && DataBaseManager.DeadBody_deftness == true)
        {
            canJudge = false;
            isJudge = true;
        }
        if (Rollet.Instance.RolletSetUi.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (deadBodyJudge == "medicine")
                {
                    if (DataBaseManager.DeadBody_observational == false)
                    {
                        deadBodyJudge = "Observation";
                        Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_Analyzing == false)
                    {
                        deadBodyJudge = "analysis";
                        Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_deftness == false)
                    {
                        deadBodyJudge = "deftness";
                        Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                }
                else if (deadBodyJudge == "Observation")
                {
                    if (DataBaseManager.DeadBody_Analyzing == false)
                    {
                        deadBodyJudge = "analysis";
                        Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_deftness == false)
                    {
                        deadBodyJudge = "deftness";
                        Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_medicine == false)
                    {
                        deadBodyJudge = "medicine";
                        Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                }
                else if (deadBodyJudge == "analysis")
                {
                    if (DataBaseManager.DeadBody_deftness == false)
                    {
                        deadBodyJudge = "deftness";
                        Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }

                    else if (DataBaseManager.DeadBody_medicine == false)
                    {
                        deadBodyJudge = "medicine";
                        Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_observational == false)
                    {
                        deadBodyJudge = "Observation";
                        Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                }
                else if (deadBodyJudge == "deftness")
                {
                    if (DataBaseManager.DeadBody_medicine == false)
                    {
                        deadBodyJudge = "medicine";
                        Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_observational == false)
                    {
                        deadBodyJudge = "Observation";
                        Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_Analyzing == false)
                    {
                        deadBodyJudge = "analysis";
                        Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (deadBodyJudge == "medicine")
                {
                    if (DataBaseManager.DeadBody_deftness == false)
                    {
                        deadBodyJudge = "deftness";
                        Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_Analyzing == false)
                    {
                        deadBodyJudge = "analysis";
                        Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_observational == false)
                    {
                        deadBodyJudge = "Observation";
                        Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                }
                else if (deadBodyJudge == "Observation")
                {
                    if (DataBaseManager.DeadBody_medicine == false)
                    {
                        deadBodyJudge = "medicine";
                        Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_deftness == false)
                    {
                        deadBodyJudge = "deftness";
                        Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_Analyzing == false)
                    {
                        deadBodyJudge = "analysis";
                        Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                }
                else if (deadBodyJudge == "analysis")
                {
                    if (DataBaseManager.DeadBody_observational == false)
                    {
                        deadBodyJudge = "Observation";
                        Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_medicine == false)
                    {
                        deadBodyJudge = "medicine";
                        Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_deftness == false)
                    {
                        deadBodyJudge = "deftness";
                        Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                }
                else if (deadBodyJudge == "deftness")
                {
                    if (DataBaseManager.DeadBody_Analyzing == false)
                    {
                        deadBodyJudge = "analysis";
                        Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_observational == false)
                    {
                        deadBodyJudge = "Observation";
                        Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_medicine == false)
                    {
                        deadBodyJudge = "medicine";
                        Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                }
            }
        }
    }
    private void Res()
    {
        // DataBaseManager.NowSelecter = "End";
        DataBaseManager.isActiveDialog2 = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInTrigger = true;
        if (collision.tag == "Player")
        {
            DataBaseManager.Select_Object = this.transform.name;
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
            DataBaseManager.Select_Object = "";
            BillowUIManager.Instance.ResetIntelUi();
        }
    }
    public void CancelJudge()
    {
        if (DataBaseManager.Select_Object == this.transform.name)
        {
            DataBaseManager.CancelJudge = false;
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
