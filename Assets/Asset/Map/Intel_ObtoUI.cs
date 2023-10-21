using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intel_ObtoUI : MonoBehaviour
{
    private static Intel_ObtoUI instance = null;
    public GameObject Arrow;

    public int KeywordCounter;
    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public bool CanKeyword;
    public bool CanJudge;
    public string name;
    [TextArea]
    public string detail;

    bool PlayerInTrigger;
    private string previousState;

    bool isJudge = false;
    bool isKeyword = false;
    public string Active_Dilaog;
    public string Look_Dilaog;
  //  public string Select_Object;

    public void Update()
    {

        if (DataBaseManager.TimeCount == 8 && this.name == "Meave" && isJudge == false)
        {
            isJudge = true;
        }
 

        if (DataBaseManager.TimeCount == 5 && ResetCheck_1 == false)
        {
            ResetCheck_1 = true;
            ResetKeyCount();
        }

        if(this.name == "Aiden Triss")
        {
            Set_Aiden();
        }
        else if (this.name == "DeadBody")
        {
            Set_DeadBody();
        }


        if (DataBaseManager.CancelJudge == true)
        {
            CancelJudge();
        }

        
        if (DataBaseManager.isActiveDialog2 == true && previousState != DataBaseManager.NowSelecter )
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

                    Arrow.SetActive(true);
                }
                else if (this.name == "DeadBody")
                {

                    Arrow.SetActive(true);
                }
                else
                {
                    Arrow.SetActive(false);
                }





                DataBaseManager.workSound = false;

                if (CanJudge == false)
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


                if (CanKeyword == false)
                {
                    isKeyword = false;
                }
                else
                {
                    isKeyword = true;
                }

                if (isKeyword == false || KeywordCounter == 0)
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
                        KeywordCounter -= 1;
                        DataBaseManager.isSelect = true;
                        InteractionController.Instance.KeywordDialog(this.name);
                    }
                    else if (DataBaseManager.NowSelecter == "Look")
                    {
                        DialogManager.Instance.ChoiceEx_NextPage();
                        DataBaseManager.isSelect = true;
                        if (DataBaseManager.nowPlace == "DetectiveOffice")
                        {
                            InteractionController.Instance.Start_1st_DetectiveOffice(Look_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "Client'shouse")
                        {
                            if (DataBaseManager.TimeCount < 7)
                            {
                                InteractionController.Instance.Start_1st_ClientsHouse(Look_Dilaog);
                            }
                            else
                            {
                                InteractionController.Instance.Start_2nd_NoonEvent(Look_Dilaog);
                            }

                        }
                        if (DataBaseManager.nowPlace == "DailyNews")
                        {
                            InteractionController.Instance.Start_1st_DailyNews(Look_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "University")
                        {
                            
                            InteractionController.Instance.Start_1st_University(Look_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "Riverside")
                        {
                            InteractionController.Instance.Start_1st_Riverside(Look_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "Hospital")
                        {
                            InteractionController.Instance.Start_1st_Hospital(Look_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "Policeoffice")
                        {
                            InteractionController.Instance.Start_1st_Policeoffice(Look_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "Slum")
                        {
                            InteractionController.Instance.Start_1st_Slum(Look_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "Bar")
                        {
                            InteractionController.Instance.Start_1st_Bar(Look_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "NoonEvent")
                        {
                            InteractionController.Instance.Start_2nd_NoonEvent(Look_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "SewerOffice")
                        {
                            InteractionController.Instance.Start_SewerOffice(Look_Dilaog);
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
                            InteractionController.Instance.Start_1st_DetectiveOffice(Active_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "Client'shouse")
                        {
                            if(DataBaseManager.TimeCount < 7)
                            {
                                InteractionController.Instance.Start_1st_ClientsHouse(Active_Dilaog);
                            }
                            else
                            {
                                InteractionController.Instance.Start_2nd_NoonEvent(Active_Dilaog);
                            }
                    
                        }
                        if (DataBaseManager.nowPlace == "DailyNews")
                        {
                            InteractionController.Instance.Start_1st_DailyNews(Active_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "University")
                        {
                            InteractionController.Instance.Start_1st_University(Active_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "Riverside")
                        {
                            InteractionController.Instance.Start_1st_Riverside(Active_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "Hospital")
                        {
                            InteractionController.Instance.Start_1st_Hospital(Active_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "Policeoffice")
                        {
                            InteractionController.Instance.Start_1st_Policeoffice(Active_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "Slum")
                        {
  
                            InteractionController.Instance.Start_1st_Slum(Active_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "Bar")
                        {
                            InteractionController.Instance.Start_1st_Bar(Active_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "NoonEvent")
                        {
                            InteractionController.Instance.Start_2nd_NoonEvent(Active_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "SewerOffice")
                        {

                            InteractionController.Instance.Start_SewerOffice(Active_Dilaog);
                        }
                    }
        
                }

            }
        }
     
    }


    public GameObject JudgeOb;
    public GameObject JudgeBackOb;
    public GameObject KeywordOb;
    public GameObject KeywordBackOb;

    
    public void OnJudge()
    {
        JudgeOb.SetActive(true);
        JudgeBackOb.SetActive(true);
        DataBaseManager.isJudge = true;
    }
    public void Onkeyword()
    {
        KeywordOb.SetActive(true);
        KeywordBackOb.SetActive(true);
        DataBaseManager.isKeyword = true;
    }



    void SetJudge()
    {
        if(DataBaseManager.Select_Object == "NewsPaper_Active")
        {
            CanJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Newspaper : Read", "Analysis", DataBaseManager.analysisPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "Stove_Active")
        {
            CanJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Stove : Making coffee", "Luck", DataBaseManager.luk, "dialog");
        }
        if (DataBaseManager.Select_Object == "Aiden Triss")
        {
            if (DataBaseManager.Aiden_medicine == false)
            {
                AidenJudge = "medicine";
                Rollet.Instance.setRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
            }
            else if (DataBaseManager.Aiden_Observation == false)
            {
                AidenJudge = "Observation";
                Rollet.Instance.setRollet("Aiden : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
            }
            else if (DataBaseManager.Aiden_psychotherapy == false)
            {
                AidenJudge = "psychotherapy";
                Rollet.Instance.setRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
            }
        }
        if (DataBaseManager.Select_Object == "Berkeley Swain")
        {
            CanJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Swain : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "Witness")
        {
            CanJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Witness : Eavesdropping", "Listening", DataBaseManager.listeningPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "DoorPolice")
        {
            CanJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Police : Persuasion", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
        }


        if (DataBaseManager.Select_Object == "DeadBody")
        {
            if (DataBaseManager.DeadBody_medicine == false)
            {
                DeadBodyJudge = "medicine";
                Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
            }
            else if (DataBaseManager.DeadBody_observational == false)
            {
                DeadBodyJudge = "Observation";
                Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
            }
            else if (DataBaseManager.DeadBody_Analyzing == false)
            {
                DeadBodyJudge = "analysis";
                Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
            }
            else if (DataBaseManager.DeadBody_deftness == false)
            {
                DeadBodyJudge = "deftness";
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
            CanJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Maeve : Persuasion", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "GoInside_Dialog")
        {
            CanJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Door : Picking doors", "Deftness", DataBaseManager.deftnessPoint, "dialog");
        }

    }

    string AidenJudge;

    void Set_Aiden()
    {
        if(DataBaseManager.Aiden_FirstDialog == false)
        {
            Active_Dilaog = "Aiden_FirstDialog";
        }
        else
        {
            DataBaseManager.Aiden_FirstDialog = true;
            Active_Dilaog = "Aiden_Dialog1";
        }

        if (DataBaseManager.Aiden_medicine == true && DataBaseManager.Aiden_Observation == true && DataBaseManager.Aiden_psychotherapy == true)
        {
            CanJudge = false;
            isJudge = true;
        }

        if(Rollet.Instance.RolletSetUi.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (AidenJudge == "medicine")
                {
                    if(DataBaseManager.Aiden_Observation == false)
                    {
                        AidenJudge = "Observation";
                        Rollet.Instance.setRollet("Aiden : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.Aiden_psychotherapy == false)
                    {
                        AidenJudge = "psychotherapy";
                        Rollet.Instance.setRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                }
                else if (AidenJudge == "Observation")
                {
                    if (DataBaseManager.Aiden_psychotherapy == false)
                    {
                        AidenJudge = "psychotherapy";
                        Rollet.Instance.setRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                    else if (DataBaseManager.Aiden_medicine == false)
                    {
                        AidenJudge = "medicine";
                        Rollet.Instance.setRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                }
                else if(AidenJudge == "psychotherapy")
                {

                     if (DataBaseManager.Aiden_medicine == false)
                    {
                        AidenJudge = "medicine";
                        Rollet.Instance.setRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }

                    else if(DataBaseManager.Aiden_Observation == false)
                    {
                        AidenJudge = "Observation";
                        Rollet.Instance.setRollet("Aiden : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (AidenJudge == "medicine")
                {              if (DataBaseManager.Aiden_psychotherapy == false)
                    {
                        AidenJudge = "psychotherapy";
                        Rollet.Instance.setRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                    else if (DataBaseManager.Aiden_Observation == false)
                    {
                        AidenJudge = "Observation";
                        Rollet.Instance.setRollet("Aiden : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
       
                }
                else if (AidenJudge == "Observation")
                {              if (DataBaseManager.Aiden_medicine == false)
                    {
                        AidenJudge = "medicine";
                        Rollet.Instance.setRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if(DataBaseManager.Aiden_psychotherapy == false)
                    {
                        AidenJudge = "psychotherapy";
                        Rollet.Instance.setRollet("Aiden : Calming down", "Psychotherapy / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
       
                }
                else if (AidenJudge == "psychotherapy")
                {
                            if (DataBaseManager.Aiden_Observation == false)
                    {
                        AidenJudge = "Observation";
                        Rollet.Instance.setRollet("Aiden : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.Aiden_medicine == false)
                    {
                        AidenJudge = "medicine";
                        Rollet.Instance.setRollet("Aiden : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }

             
                }
            }
        }
    }

    string DeadBodyJudge;

    void Set_DeadBody()
    {
   
        if (DataBaseManager.DeadBody_medicine == true && DataBaseManager.DeadBody_observational == true && DataBaseManager.DeadBody_Analyzing == true && DataBaseManager.DeadBody_deftness == true)
        {
            CanJudge = false;
            isJudge = true;
        }

        if (Rollet.Instance.RolletSetUi.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (DeadBodyJudge == "medicine")
                {
                    if (DataBaseManager.DeadBody_observational == false)
                    {
                        DeadBodyJudge = "Observation";
                        Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_Analyzing == false)
                    {
                        DeadBodyJudge = "analysis";
                        Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_deftness == false)
                    {
                        DeadBodyJudge = "deftness";
                        Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                }
                else if (DeadBodyJudge == "Observation")
                {
                    if (DataBaseManager.DeadBody_Analyzing == false)
                    {
                        DeadBodyJudge = "analysis";
                        Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_deftness == false)
                    {
                        DeadBodyJudge = "deftness";
                        Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_medicine == false)
                    {
                        DeadBodyJudge = "medicine";
                        Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                }
                else if (DeadBodyJudge == "analysis")
                {

                    if (DataBaseManager.DeadBody_deftness == false)
                    {
                        DeadBodyJudge = "deftness";
                        Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }

                    else if (DataBaseManager.DeadBody_medicine == false)
                    {
                        DeadBodyJudge = "medicine";
                        Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_observational == false)
                    {
                        DeadBodyJudge = "Observation";
                        Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                }
                else if (DeadBodyJudge == "deftness")
                {

                    if (DataBaseManager.DeadBody_medicine == false)
                    {
                        DeadBodyJudge = "medicine";
                        Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }

                    else if (DataBaseManager.DeadBody_observational == false)
                    {
                        DeadBodyJudge = "Observation";
                        Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_Analyzing == false)
                    {
                        DeadBodyJudge = "analysis";
                        Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (DeadBodyJudge == "medicine")
                {
                    if (DataBaseManager.DeadBody_deftness == false)
                    {
                        DeadBodyJudge = "deftness";
                        Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_Analyzing == false)
                    {
                        DeadBodyJudge = "analysis";
                        Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_observational == false)
                    {
                        DeadBodyJudge = "Observation";
                        Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }

                }
                else if (DeadBodyJudge == "Observation")
                {
                    if (DataBaseManager.DeadBody_medicine == false)
                    {
                        DeadBodyJudge = "medicine";
                        Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_deftness == false)
                    {
                        DeadBodyJudge = "deftness";
                        Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_Analyzing == false)
                    {
                        DeadBodyJudge = "analysis";
                        Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }

                }
                else if (DeadBodyJudge == "analysis")
                {
                    if (DataBaseManager.DeadBody_observational == false)
                    {
                        DeadBodyJudge = "Observation";
                        Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_medicine == false)
                    {
                        DeadBodyJudge = "medicine";
                        Rollet.Instance.setRollet("DeadBody : Diagnosis", "Medicine", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_deftness == false)
                    {
                        DeadBodyJudge = "deftness";
                        Rollet.Instance.setRollet("DeadBody : Stealing", "Deftness", (DataBaseManager.deftnessPoint), "dialog");
                    }

                }
                else if (DeadBodyJudge == "deftness")
                {
                    if (DataBaseManager.DeadBody_Analyzing == false)
                    {
                        DeadBodyJudge = "analysis";
                        Rollet.Instance.setRollet("DeadBody : Check", "Analysis", (DataBaseManager.analysisPoint), "dialog");
                    }
                    else if (DataBaseManager.DeadBody_observational == false)
                    {
                        DeadBodyJudge = "Observation";
                        Rollet.Instance.setRollet("DeadBody : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.DeadBody_medicine == false)
                    {
                        DeadBodyJudge = "medicine";
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
        PlayerInTrigger = true;
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
        PlayerInTrigger = false;
        if (collision.tag == "Player")
        {
            DataBaseManager.Select_Object = "";
            BillowUIManager.Instance.ResetIntelUi();
        }
    }


    public void CancelJudge()
    {
        if(DataBaseManager.Select_Object == this.transform.name)
        {

            DataBaseManager.CancelJudge = false;
            isJudge = true;
            CanJudge = true;
        }
    }

    bool ResetCheck_1 = false;

    public void ResetKeyCount()
    {
        if(this.name == "Ella Triss")
        {
            KeywordCounter = 3;
        }
        else if (this.name == "Aiden Triss")
        {
            KeywordCounter = 3;
        }

        else if (this.name == "Berkeley Swain")
        {
            KeywordCounter = 4;
        }

        else if (this.name == "University student")
        {
            KeywordCounter = 3;
        }

        else if (this.name == "Kane")
        {
            KeywordCounter = 3;
        }

        else if (this.name == "Maeve")
        {
            KeywordCounter = 5;
        }

        else if (this.name == "Kate Lizzie")
        {
            KeywordCounter = 4;
        }

        else if (this.name == "Alan Parks")
        {
            KeywordCounter = 4;
        }

        else if (this.name == "Albert Bradley")
        {
            KeywordCounter = 4;
        }

    }

    private void Awake()
    {



    }
}
