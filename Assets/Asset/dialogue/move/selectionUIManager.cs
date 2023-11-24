using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class selectionUIManager : MonoBehaviour
{
    public GameObject select1stDetectiveOiffce1;
    public GameObject select1stDetectiveOiffce2;
    public GameObject select1stClientsOfficeSafe;
    public GameObject selectSewerEnemy;
    //빈민가
    private List<System.Action> functionList = new List<System.Action>();
    public GameObject select1stSlum;
    private static selectionUIManager instance = null;
    void Awake()
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
        functionList.Add(Function1);
        functionList.Add(Function2);
        functionList.Add(Function3);
        functionList.Add(Function4);
        functionList.Add(Function5);
        functionList.Add(Function6);
        functionList.Add(Function7);
        functionList.Add(Function8);
    }
    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static selectionUIManager Instance
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
    public GameObject ladderSelectionUI;
    public void OpenLadderSelectUI()
    {
        DataBaseManager.isActiveDialog1 = true;
        ladderSelectionUI.SetActive(true);
    }
    public void Open1st_DetectiveOfficeSelect1()
    {
        DataBaseManager.isActiveDialog1 = true;
        select1stDetectiveOiffce1.SetActive(true);
    }
    public void OpenSelectSewerEnemy()
    {
        DataBaseManager.isActiveDialog1 = true;
        selectSewerEnemy.SetActive(true);
    }
    public void Open1stClientsOffic_SafeSelect()
    {
        DataBaseManager.isActiveDialog1 = true;
        select1stClientsOfficeSafe.SetActive(true);
    }
    //빈민가
    public void Open1stSlumDave()
    {
        DataBaseManager.isActiveDialog1 = true;
        select1stSlum.SetActive(true);
    }
    public void ReciveRequestSelectionButton(string SelectButton)
    {
        DataBaseManager.SelectionOn = false;
        if (SelectButton == "Select_AboutCrime")
        {
            DialogManager.Instance.ChoiceExNextPageT();
            select1stDetectiveOiffce1.SetActive(false);
            InteractionController.Instance.Start_1st_DetectiveOffice("Select_AboutCrime");
        }
        if (SelectButton == "Select_AboutHospital")
        {
            DialogManager.Instance.ChoiceExNextPageT();
            select1stDetectiveOiffce1.SetActive(false);
            InteractionController.Instance.Start_1st_DetectiveOffice("Select_AboutHospital");
        }
        if (SelectButton == "Select_AboutRunAway")
        {
            DialogManager.Instance.ChoiceExNextPageT();
            select1stDetectiveOiffce1.SetActive(false);
            InteractionController.Instance.Start_1st_DetectiveOffice("Select_AboutRunAway");
        }
        if (SelectButton == "Select_GotoChoice")
        {
            DialogManager.Instance.ChoiceExNextPageT();
            select1stDetectiveOiffce1.SetActive(false);
            select1stDetectiveOiffce2.SetActive(true);
        }
        if (SelectButton == "Accept_Request")
        {
            DataBaseManager.IfGotRequset = true;
            DialogManager.Instance.ChoiceExNextPageT();
            select1stDetectiveOiffce2.SetActive(false);
            InteractionController.Instance.Start_1st_DetectiveOffice("Accept_Request");
        }
        if (SelectButton == "Reject_Request")
        {
            DialogManager.Instance.ChoiceExNextPageT();
            select1stDetectiveOiffce2.SetActive(false);
            InteractionController.Instance.Start_1st_DetectiveOffice("Reject_Request");
        }
        //의뢰자의 집
        if (SelectButton == "Select_Str")
        {
            DialogManager.Instance.ChoiceExNextPageT();
            select1stClientsOfficeSafe.SetActive(false);
            InteractionController.Instance.Start_1st_ClientsHouse("Aiden_key_Safe_Str");
        }
        if (SelectButton == "Select_Rhetoric")
        {
            DialogManager.Instance.ChoiceExNextPageT();
            select1stClientsOfficeSafe.SetActive(false);
            Rollet.Instance.setRollet("Aiden : Persuasion", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
        }
        if (SelectButton == "Select_End")
        {
            DialogManager.Instance.ChoiceExNextPageT();
            select1stClientsOfficeSafe.SetActive(false);
            InteractionController.Instance.Start_1st_ClientsHouse("Aiden_key_Safe_Nothing");
        }
        //빈민가
        if (SelectButton == "GiveMoney")
        {
            if(DataBaseManager.weal >= 2)
            {
                DataBaseManager.DaveCheck = false;
                DialogManager.Instance.ChoiceExNextPageT();
                select1stSlum.SetActive(false);
                ActivateRandomFunction();
            }
            else
            {
                DataBaseManager.DaveCheck = false;
                DialogManager.Instance.ChoiceExNextPageT();
                select1stSlum.SetActive(false);
                InteractionController.Instance.Start_1st_Slum("Dave_NoMoney");
            }
        }
        if (SelectButton == "Reject")
        {
            DataBaseManager.DaveCheck = false;
            DialogManager.Instance.ChoiceExNextPageT();
            select1stSlum.SetActive(false);

        }
        // 하수도 내부
        if (SelectButton == "Stealth")
        {
            DialogManager.Instance.ChoiceExNextPageT();
            selectSewerEnemy.SetActive(false);
            Rollet.Instance.setRollet("Fabian : Sneak Out", "Stealth", DataBaseManager.stealthPoint, "dialog");

        }
        if (SelectButton == "Fight")
        {
            DialogManager.Instance.ChoiceExNextPageT();
            selectSewerEnemy.SetActive(false);
            DataBaseManager.StartSewerBattle = true;
            // 전투로 이동
        }
        if (SelectButton == "GoOut")
        {
            DialogManager.Instance.ChoiceExNextPageT();
            ladderSelectionUI.SetActive(false);
            InteractionController.Instance.InSewerDialog("Sewer_LadderUP");
            DataBaseManager.StoryDirecting = true;
        }
        if (SelectButton == "NotOut")
        {
            DialogManager.Instance.ChoiceExNextPageT();
            ladderSelectionUI.SetActive(false);
            DataBaseManager.SelectionOn = false;
        }
    }
    public void EndDialog ()
    {
        select1stSlum.SetActive(false);
    }
    private void ActivateRandomFunction()
    {
        if (functionList.Count > 0)
        {
            // 함수 리스트에서 무작위로 함수 선택
            int randomIndex = Random.Range(0, functionList.Count);
            System.Action selectedFunction = functionList[randomIndex];

            // 선택된 함수 실행
            selectedFunction();

            // 실행된 함수는 리스트에서 제거
            functionList.RemoveAt(randomIndex);
        }
        else
        {
            FunctionEx();
        }
    }
    private void Function1()
    {
        DataBaseManager.weal -= 2;
        InteractionController.Instance.Start_1st_Slum("Dave_Ran1");
    }

    private void Function2()
    {
        DataBaseManager.weal -= 2;
        InteractionController.Instance.Start_1st_Slum("Dave_Ran2");
    }

    private void Function3()
    {
        DataBaseManager.weal -= 2;
        InteractionController.Instance.Start_1st_Slum("Dave_Ran3");
    }
    private void Function4()
    {
        DataBaseManager.weal -= 2;
        InteractionController.Instance.Start_1st_Slum("Dave_Ran4");
    }
    private void Function5()
    {
        DataBaseManager.weal -= 2;
        InteractionController.Instance.Start_1st_Slum("Dave_Ran5");
    }
    private void Function6()
    {
        DataBaseManager.weal -= 2;
        InteractionController.Instance.Start_1st_Slum("Dave_Ran6");
    }
    private void Function7()
    {
        DataBaseManager.weal -= 2;
        InteractionController.Instance.Start_1st_Slum("Dave_Ran7");
    }
    private void Function8()
    {
        DataBaseManager.weal -= 2;
        InteractionController.Instance.Start_1st_Slum("Dave_Ran8");
    }
    private void Function9()
    {
        DataBaseManager.weal -= 2;
        InteractionController.Instance.Start_1st_Slum("Dave_Ran9");
    }
    private void FunctionEx()
    {
        InteractionController.Instance.Start_1st_Slum("Dave_RanEnd");
    }
    public void ExAddFunc9()
    {
        functionList.Add(Function9);
    }
}
