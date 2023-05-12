using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class selectionUIManager : MonoBehaviour
{
    public GameObject Select_1st_DetectiveOiffce_1;
    public GameObject Select_1st_DetectiveOiffce_2;

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
    private void Update()
    {
        
    }

    public void Open_1st_DetectiveOffice_Select1()
    {
        DataBaseManager.isActiveDialog1 = true;
        Select_1st_DetectiveOiffce_1.SetActive(true);
    }


    public void ReciveRequest_SelectionButton(string SelectButton)
    {
        DataBaseManager.SelectionOn = false;
        if (SelectButton == "Select_AboutCrime")
        {
            DialogManager.Instance.ChoiceEx_NextPage_t();
            Select_1st_DetectiveOiffce_1.SetActive(false);
            InteractionController.Instance.Start_1st_DetectiveOffice("Select_AboutCrime");
        }
        if (SelectButton == "Select_AboutHospital")
        {
            DialogManager.Instance.ChoiceEx_NextPage_t();
            Select_1st_DetectiveOiffce_1.SetActive(false);
            InteractionController.Instance.Start_1st_DetectiveOffice("Select_AboutHospital");
        }
        if (SelectButton == "Select_AboutRunAway")
        {
            DialogManager.Instance.ChoiceEx_NextPage_t();
            Select_1st_DetectiveOiffce_1.SetActive(false);
            InteractionController.Instance.Start_1st_DetectiveOffice("Select_AboutRunAway");
        }
        if (SelectButton == "Select_GotoChoice")
        {
            DialogManager.Instance.ChoiceEx_NextPage_t();
            Select_1st_DetectiveOiffce_1.SetActive(false);
            Select_1st_DetectiveOiffce_2.SetActive(true);
        }
        if (SelectButton == "Accept_Request")
        {
            DialogManager.Instance.ChoiceEx_NextPage_t();
            Select_1st_DetectiveOiffce_2.SetActive(false);
            InteractionController.Instance.Start_1st_DetectiveOffice("Accept_Request");
        }
        if (SelectButton == "Reject_Request")
        {
            DialogManager.Instance.ChoiceEx_NextPage_t();
            Select_1st_DetectiveOiffce_2.SetActive(false);
            InteractionController.Instance.Start_1st_DetectiveOffice("Reject_Request");
        }
    }
}
