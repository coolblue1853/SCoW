using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intel_ObtoUI : MonoBehaviour
{
    private static Intel_ObtoUI instance = null;


    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.

    public bool CanJudge;
    public string name;
    [TextArea]
    public string detail;

    bool PlayerInTrigger;
    private string previousState;

    bool isJudge = false;

    public string Active_Dilaog;
    public string Look_Dilaog;
  //  public string Select_Object;
    public void Update()
    {
        if (DataBaseManager.CancelJudge == true)
        {
            CancelJudge();
        }

        
        if (DataBaseManager.isActiveDialog2 == true && previousState != DataBaseManager.NowSelecter)
        {
            Res();
        }
        previousState = DataBaseManager.NowSelecter;

        if (Input.GetKeyDown(KeyCode.E) && DataBaseManager.isActiveDialog2 == false)
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

                if(DataBaseManager.isRollet == false)
                {
                    if (DataBaseManager.NowSelecter == "Look")
                    {
                        DataBaseManager.isSelect = true;
                        InteractionController.Instance.Start_1st_DetectiveOffice(Look_Dilaog);
                    }
                    else if (DataBaseManager.NowSelecter == "Judge")
                    {
                        DialogManager.Instance.EndDialog();
                        CanJudge = false;
                        isJudge = true;
                        SetJudge();
                    }
                    else if (DataBaseManager.NowSelecter == "End" && DataBaseManager.isActiveDialog1 == false)
                    {

                        InteractionController.Instance.Start_1st_DetectiveOffice(Active_Dilaog);
                    }
                }


            }
        }

    }


    void SetJudge()
    {
        if(DataBaseManager.Select_Object == "NewsPaper_Active")
        {
            Rollet.Instance.setRollet("�Ź� : ���캸��", "�ڷ�����", DataBaseManager.analysisPoint, "dialog");
        }
        if (DataBaseManager.Select_Object == "Stove_Active")
        {
            Rollet.Instance.setRollet("����� : ����ϱ�", "���", DataBaseManager.luk, "dialog");
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
}
