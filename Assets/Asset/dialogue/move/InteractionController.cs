using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionController : MonoBehaviour
{
    public DialogManager theDM;


    // 1st Detective Office
    public GameObject Start_1st_Detective;
    public GameObject NewsPaper;
    public GameObject NewsPaper_Look;
    public GameObject NewsPaper_Judege_True;
    public GameObject NewsPaper_Judege_False;
    public GameObject Desk;
    public GameObject Desk_Look;
    public GameObject Stove;
    public GameObject Stove_Look;
    public GameObject Stove_Judge_True;
    public GameObject Stove_Judge_False;
    public GameObject Directing_Nock;
    public GameObject Directing_Door;
    public GameObject Directing_AfterDoor;
    public GameObject Select_AboutCrime;
    public GameObject Select_AboutHospital;
    public GameObject Select_AboutRunAway;
    public GameObject Accept_Request;
    public GameObject Accept_AfterSelectYes;
    public GameObject Reject_Request;
    public GameObject Accept_AfterSelectNo;

    // Update is called once per frame


    private void Start()
    {
        //TestNar();
    }

    public void Start_1st_DetectiveOffice(string setDialog)
    {
        if(setDialog == "start")
        {
            theDM.ShowDialog(Start_1st_Detective.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "NewsPaper_Active")
        {
            theDM.ShowDialog(NewsPaper.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "NewsPaper_Look")
        {
            DataBaseManager.fst_Detectiv_TimeOn += 1;
            theDM.ShowDialog(NewsPaper_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Desk_Active")
        {
            theDM.ShowDialog(Desk.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Desk_Look")
        {
            DataBaseManager.fst_Detectiv_TimeOn += 1;
            theDM.ShowDialog(Desk_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Stove_Active")
        {
            theDM.ShowDialog(Stove.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Stove_Look")
        {
            DataBaseManager.fst_Detectiv_TimeOn += 1;
            theDM.ShowDialog(Stove_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Directing_Nock")
        {
            DataBaseManager.fst_Detectiv_TimeOn = -99;
            theDM.ShowDialog(Directing_Nock.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Directing_Door")
        {
            DataBaseManager.fst_Detectiv_TimeOn = -999;
            theDM.ShowDialog(Directing_Door.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Directing_AfterDoor")
        {
            theDM.ShowDialog(Directing_AfterDoor.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Select_AboutCrime")
        {
            Debug.Log(4);
            theDM.ShowDialog(Select_AboutCrime.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Select_AboutHospital")
        {
            theDM.ShowDialog(Select_AboutHospital.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Select_AboutRunAway")
        {
            theDM.ShowDialog(Select_AboutRunAway.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Accept_Request")
        {
            theDM.ShowDialog(Accept_Request.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Accept_AfterSelectYes")
        {
            theDM.ShowDialog(Accept_AfterSelectYes.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        
        else if (setDialog == "Reject_Request")
        {
            
            theDM.ShowDialog(Reject_Request.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Accept_AfterSelectNo")
        {

            theDM.ShowDialog(Accept_AfterSelectNo.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }



    void OnEnable()
    {

    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    void Update()
    {

    }


    /*
    public void 의뢰자등장후대화()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(의뢰자등장후.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
    */


   public void RetrunDialogResult(string Sub_Dialog, string result_End)
    {
        // 1일차 오전 탐정사무소 판정
        if(Sub_Dialog == "신문 : 살펴보기")
        {
            DataBaseManager.fst_Detectiv_TimeOn += 1;
            if (result_End == "판정 : 성공"|| result_End == "판정 : 대성공")
            {
                theDM.ShowDialog(NewsPaper_Judege_True.transform.GetComponent<interactionEvent>().GetDialogs());
  
            }
            else if (result_End == "판정 : 실패" || result_End == "판정 : 대실패")
            {
                theDM.ShowDialog(NewsPaper_Judege_False.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "스토브 : 사용하기")
        {
            DataBaseManager.fst_Detectiv_TimeOn += 1;
            if (result_End == "판정 : 성공" || result_End == "판정 : 대성공")
            {
                DataBaseManager.Condition = "Good";
                theDM.ShowDialog(Stove_Judge_True.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "판정 : 실패" || result_End == "판정 : 대실패")
            {
                theDM.ShowDialog(Stove_Judge_False.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
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


    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
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
