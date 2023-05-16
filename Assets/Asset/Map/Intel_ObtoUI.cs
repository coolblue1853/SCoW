using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intel_ObtoUI : MonoBehaviour
{
    private static Intel_ObtoUI instance = null;

    public int KeywordCounter;
    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
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

        if(this.name == "Aiden Triss")
        {
            Set_Aiden();

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

        if (Input.GetKeyDown(KeyCode.E) && DataBaseManager.isActiveDialog2 == false )
        {

            if (this.transform.name == DataBaseManager.Select_Object)
            {
            
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
                    DataBaseManager.isJudge = false;
                }
                else if (isJudge == true)
                {
                    DataBaseManager.isJudge = true;
                }


                if (CanKeyword == false)
                {
                    isKeyword = false;
                }
                else
                {
                    isKeyword = true;
                }

                if (isKeyword == false || KeywordCounter ==0)
                {
                    DataBaseManager.isKeyword = false;
                }
                else if (isKeyword == true)
                {
                    DataBaseManager.isKeyword = true;
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
                            InteractionController.Instance.Start_1st_ClientsHouse(Look_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "DailyNews")
                        {
                            InteractionController.Instance.Start_1st_DailyNews(Look_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "University")
                        {
                            InteractionController.Instance.Start_1st_University(Look_Dilaog);
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
                            InteractionController.Instance.Start_1st_ClientsHouse(Active_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "DailyNews")
                        {
                            InteractionController.Instance.Start_1st_DailyNews(Active_Dilaog);
                        }
                        if (DataBaseManager.nowPlace == "University")
                        {
                            InteractionController.Instance.Start_1st_University(Active_Dilaog);
                        }
                    }
                }


            }
        }

    }


    void SetJudge()
    {
        if(DataBaseManager.Select_Object == "NewsPaper_Active")
        {
            CanJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("�Ź� : ���캸��", "�ڷ�����", DataBaseManager.analysisPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "Stove_Active")
        {
            CanJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("����� : ����ϱ�", "���", DataBaseManager.luk, "dialog");
        }
        if (DataBaseManager.Select_Object == "AidenTriss")
        {
            if (DataBaseManager.Aiden_medicine == false)
            {
                AidenJudge = "medicine";
                Rollet.Instance.setRollet("���̵� : ����", "����", DataBaseManager.medicinePoint, "dialog");
            }
            else if (DataBaseManager.Aiden_Observation == false)
            {
                AidenJudge = "Observation";
                Rollet.Instance.setRollet("���̵� : ���캸��", "������", DataBaseManager.ObservationPoint, "dialog");
            }
            else if (DataBaseManager.Aiden_psychotherapy == false)
            {
                AidenJudge = "psychotherapy";
                Rollet.Instance.setRollet("���̵� : ����", "����ġ�� / 2" , (DataBaseManager.psychotherapyPoint / 2), "dialog");
            }
        }
        if (DataBaseManager.Select_Object == "Berkeley Swain")
        {
            CanJudge = false;
            isJudge = true;
            Rollet.Instance.setRollet("Swain : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
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
                        Rollet.Instance.setRollet("���̵� : ���캸��", "������", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.Aiden_psychotherapy == false)
                    {
                        AidenJudge = "psychotherapy";
                        Rollet.Instance.setRollet("���̵� : ����", "����ġ�� / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                }
                else if (AidenJudge == "Observation")
                {
                    if (DataBaseManager.Aiden_psychotherapy == false)
                    {
                        AidenJudge = "psychotherapy";
                        Rollet.Instance.setRollet("���̵� : ����", "����ġ�� / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                    else if (DataBaseManager.Aiden_medicine == false)
                    {
                        AidenJudge = "medicine";
                        Rollet.Instance.setRollet("���̵� : ����", "����", DataBaseManager.medicinePoint, "dialog");
                    }
                }
                else if(AidenJudge == "psychotherapy")
                {

                     if (DataBaseManager.Aiden_medicine == false)
                    {
                        AidenJudge = "medicine";
                        Rollet.Instance.setRollet("���̵� : ����", "����", DataBaseManager.medicinePoint, "dialog");
                    }

                    else if(DataBaseManager.Aiden_Observation == false)
                    {
                        AidenJudge = "Observation";
                        Rollet.Instance.setRollet("���̵� : ���캸��", "������", DataBaseManager.ObservationPoint, "dialog");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (AidenJudge == "medicine")
                {              if (DataBaseManager.Aiden_psychotherapy == false)
                    {
                        AidenJudge = "psychotherapy";
                        Rollet.Instance.setRollet("���̵� : ����", "����ġ�� / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
                    else if (DataBaseManager.Aiden_Observation == false)
                    {
                        AidenJudge = "Observation";
                        Rollet.Instance.setRollet("���̵� : ���캸��", "������", DataBaseManager.ObservationPoint, "dialog");
                    }
       
                }
                else if (AidenJudge == "Observation")
                {              if (DataBaseManager.Aiden_medicine == false)
                    {
                        AidenJudge = "medicine";
                        Rollet.Instance.setRollet("���̵� : ����", "����", DataBaseManager.medicinePoint, "dialog");
                    }
                    else if(DataBaseManager.Aiden_psychotherapy == false)
                    {
                        AidenJudge = "psychotherapy";
                        Rollet.Instance.setRollet("���̵� : ����", "����ġ�� / 2", (DataBaseManager.psychotherapyPoint / 2), "dialog");
                    }
       
                }
                else if (AidenJudge == "psychotherapy")
                {
                            if (DataBaseManager.Aiden_Observation == false)
                    {
                        AidenJudge = "Observation";
                        Rollet.Instance.setRollet("���̵� : ���캸��", "������", DataBaseManager.ObservationPoint, "dialog");
                    }
                    else if (DataBaseManager.Aiden_medicine == false)
                    {
                        AidenJudge = "medicine";
                        Rollet.Instance.setRollet("���̵� : ����", "����", DataBaseManager.medicinePoint, "dialog");
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
    private void Awake()
    {
        KeywordCounter = 3;
    }
}
