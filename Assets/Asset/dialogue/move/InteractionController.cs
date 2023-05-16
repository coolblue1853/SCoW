using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class InteractionController : MonoBehaviour
{
    public DialogManager theDM;



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


    // 1st Clinet'sOffice
    public GameObject FirstArrive;
    public GameObject NockDoor;
    public GameObject Ella_fistDialog;
    public GameObject Ella_Dialog1;
    public GameObject Ella_Dialog2;
    public GameObject Ella_key_Aiden;
    public GameObject Ella_key_disable;
    public GameObject Ella_FirstUpstair;
    public GameObject into_FirstRoom;
    public GameObject Aiden_Active;
    public GameObject Aiden_Look;
    public GameObject Aiden_LookJudge;
    public GameObject Aiden_FirstDialog;
    public GameObject Aiden_Dialog1;
    public GameObject Aiden_Dialog2;
    public GameObject Aiden_key_Nightmare1;
    public GameObject Aiden_key_EllaTriss1;
    public GameObject Aiden_key_Safe1;
    public GameObject Aiden_key_Safe_Str;
    public GameObject Aiden_key_Safe_Str_Sucsses;
    public GameObject Aiden_key_Safe_Str_Fail;
    public GameObject Aiden_key_Safe_rhetoric_sucsses;
    public GameObject Aiden_key_Safe_rhetoric_Fail;
    public GameObject Aiden_key_Safe_Nothing;
    public GameObject Aiden_key_Fail1;
    public GameObject Aiden_key_Fail2;
    public GameObject Aiden_Judge_medicine_Sucsses;
    public GameObject Aiden_Judge_medicine_Fail;
    public GameObject Aiden_Judge_Observation_Sucsses;
    public GameObject Aiden_Judge_Observation_Fail;
    public GameObject Aiden_Judge_psychotherapy_Sucsses;
    public GameObject Aiden_Judge_psychotherapy_Sucsses_Safe;
    public GameObject Aiden_Judge_psychotherapy_Sucsses_NoSafe;
    public GameObject Aiden_Judge_psychotherapy_Sucsses_End;
    public GameObject Aiden_Judge_psychotherapy_Fail;
    public GameObject Aiden_key_FailForce;
    public GameObject EllaEnd_NoSafe;
    public GameObject EllaEnd_GetSafe;

    // 1st Clinet'sOffice
    public GameObject Swain_FirstDialog; //
    public GameObject Swain_Dialog; //
    public GameObject Swain_Look; //
    public GameObject Swain_LookJudge_Sucsses; //
    public GameObject Swain_LookJudge_Fail; //
    public GameObject Swain_Key_PlanetarySequence1;//
    public GameObject Swain_Key_Insomnia1;
    public GameObject Swain_Key_Nightmare2;
    public GameObject Swain_Key_Paranoia1;
    public GameObject Swain_Key_EllaTriss1;
    public GameObject Swain_Key_AidenTriss1;
    public GameObject Swain_Key_DailyNews1;
    public GameObject Swain_Key_Nothing;

    // 1st University
    public GameObject Univ_FirstDialog; //
    public GameObject Univ_Dialog; //
    public GameObject Univ_Look; //
    public GameObject Univ_Key_PlanetarySequence1;//
    public GameObject Univ_Key_Insomnia1;
    public GameObject Univ_Key_Nothing;

    // 1st Riverside
    public GameObject Kane_FirstDialog; //
    public GameObject Kane_Dialog; //
    public GameObject Kane_Look;
    public GameObject Kane_Nothing;
    public GameObject Sewer_FirstDialog; //
    public GameObject Sewer_Dialog; //
    public GameObject Sewer_Look;
    public GameObject Sewer_Observation_Sucsses;
    public GameObject Sewer_Observation_SucssesAfter;
    public GameObject Sewer_Observation_Fail;
    public GameObject Sewer_Observation_SucssesAfterSanSuc;


    // 1st Riverside
    public GameObject Hospital_NoonFirst; //
    public GameObject Kate_FirstDialog; //
    public GameObject Alan_FirstDialog; //
    public GameObject Alan_Dialog; //


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

    public void Start_1st_ClientsHouse(string setDialog)
    {
        if (setDialog == "FirstArrive")
        {
            theDM.ShowDialog(FirstArrive.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NockDoor")
        {
            theDM.ShowDialog(NockDoor.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ella_fistDialog")
        {
            theDM.ShowDialog(Ella_fistDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ella_Dialog1")
        {
            if(DataBaseManager.Ella_EndDialog == false && DataBaseManager.Aiden_FirstDialog == true){
                DataBaseManager.Ella_EndDialog = true;

                if(DataBaseManager.getSafe == false)
                {
                    theDM.ShowDialog(EllaEnd_NoSafe.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(EllaEnd_GetSafe.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                theDM.ShowDialog(Ella_Dialog1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
      
        }
        if (setDialog == "Ella_Dialog2")
        {
            theDM.ShowDialog(Ella_Dialog2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ella_FirstUpstair")
        {
            theDM.ShowDialog(Ella_FirstUpstair.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        //
        if (setDialog == "into_FirstRoom")
        {
            theDM.ShowDialog(into_FirstRoom.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        
        if (setDialog == "Aiden_FirstDialog")
        {
            DataBaseManager.Aiden_FirstDialog = true;
            theDM.ShowDialog(Aiden_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Active")
        {
            theDM.ShowDialog(Aiden_Active.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Look")
        {
            theDM.ShowDialog(Aiden_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_LookJudge")
        {
            theDM.ShowDialog(Aiden_LookJudge.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Dialog1")
        {
            theDM.ShowDialog(Aiden_Dialog1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Dialog2")
        {
            theDM.ShowDialog(Aiden_Dialog2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Nightmare1")
        {
            theDM.ShowDialog(Aiden_key_Nightmare1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_EllaTriss1")
        {
            theDM.ShowDialog(Aiden_key_EllaTriss1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe1")
        {
            theDM.ShowDialog(Aiden_key_Safe1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe_Str")
        {
            theDM.ShowDialog(Aiden_key_Safe_Str.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe_Str_Sucsses")
        {
            theDM.ShowDialog(Aiden_key_Safe_Str_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe_Str_Fail")
        {
            theDM.ShowDialog(Ella_FirstUpstair.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe_rhetoric_sucsses")
        {
            theDM.ShowDialog(Aiden_key_Safe_rhetoric_sucsses.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe_rhetoric_Fail")
        {
            theDM.ShowDialog(Aiden_key_Safe_rhetoric_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe_Nothing")
        {
            theDM.ShowDialog(Aiden_key_Safe_Nothing.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Fail1")
        {
            theDM.ShowDialog(Aiden_key_Fail1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Fail2")
        {
            theDM.ShowDialog(Aiden_key_Fail2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_medicine_Sucsses")
        {
            theDM.ShowDialog(Aiden_Judge_medicine_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_medicine_Fail")
        {
            theDM.ShowDialog(Aiden_Judge_medicine_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_psychotherapy_Sucsses")
        {
            theDM.ShowDialog(Aiden_Judge_psychotherapy_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_psychotherapy_Sucsses_Safe")
        {
            theDM.ShowDialog(Aiden_Judge_psychotherapy_Sucsses_Safe.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_psychotherapy_Sucsses_NoSafe")
        {
            theDM.ShowDialog(Aiden_Judge_psychotherapy_Sucsses_NoSafe.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_psychotherapy_Sucsses_End")
        {
            theDM.ShowDialog(Aiden_Judge_psychotherapy_Sucsses_End.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_psychotherapy_Fail")
        {
            theDM.ShowDialog(Aiden_Judge_psychotherapy_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_Observation_Sucsses")
        {
            theDM.ShowDialog(Aiden_Judge_Observation_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_Observation_Fail")
        {
            theDM.ShowDialog(Aiden_Judge_Observation_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_FailForce")
        {
            theDM.ShowDialog(Aiden_key_FailForce.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "EllaEnd_NoSafe")
        {
            theDM.ShowDialog(EllaEnd_NoSafe.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "EllaEnd_GetSafe")
        {
            theDM.ShowDialog(EllaEnd_GetSafe.transform.GetComponent<interactionEvent>().GetDialogs());
        }

}

    public void Start_1st_DailyNews(string setDialog)
    {
    
        if (setDialog == "Swain_FirstDialog")
        {
            if(DataBaseManager.Aiden_FirstDialog == false)
            {
                DataBaseManager.Aiden_FirstDialog = true;
                theDM.ShowDialog(Swain_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Swain_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (setDialog == "Swain_Look")
        {
            theDM.ShowDialog(Swain_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }


        if (setDialog == "Swain_LookJudge_Sucsses")
        {
            theDM.ShowDialog(Swain_LookJudge_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_LookJudge_Fail")
        {
            theDM.ShowDialog(Swain_LookJudge_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_PlanetarySequence1")
        {
            theDM.ShowDialog(Swain_Key_PlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_Insomnia1")
        {
            theDM.ShowDialog(Swain_Key_Insomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_Nightmare2")
        {
            theDM.ShowDialog(Swain_Key_Nightmare2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_Paranoia1")
        {
            theDM.ShowDialog(Swain_Key_Paranoia1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_EllaTriss1")
        {
            theDM.ShowDialog(Swain_Key_EllaTriss1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_AidenTriss1")
        {
            theDM.ShowDialog(Swain_Key_AidenTriss1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_DailyNews1")
        {
            theDM.ShowDialog(Swain_Key_DailyNews1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_Nothing")
        {
            theDM.ShowDialog(Swain_Key_Nothing.transform.GetComponent<interactionEvent>().GetDialogs());
        }

    }
    public void Start_1st_University(string setDialog)
    {

        if (setDialog == "Univ_FirstDialog")
        {
            if (DataBaseManager.Univ_FirstDialog == false)
            {
                DataBaseManager.Univ_FirstDialog = true;
                theDM.ShowDialog(Univ_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Univ_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (setDialog == "Univ_Look")
        {
            theDM.ShowDialog(Univ_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Univ_Key_PlanetarySequence1")
        {
            theDM.ShowDialog(Univ_Key_PlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Univ_Key_Insomnia1")
        {
            theDM.ShowDialog(Univ_Key_Insomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
        }



}
    public void Start_1st_Riverside(string setDialog)
    {

        if (setDialog == "Kane_FirstDialog")
        {
            if (DataBaseManager.Kane_FirstDialog == false)
            {
                DataBaseManager.Kane_FirstDialog = true;
                theDM.ShowDialog(Kane_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Kane_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (setDialog == "Kane_Look")
        {
            theDM.ShowDialog(Kane_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Kane_Nothing")
        {
            theDM.ShowDialog(Kane_Nothing.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_FirstDialog")
        {
            if (DataBaseManager.Sewer_FirstDialog == false)
            {
                DataBaseManager.Sewer_FirstDialog = true;
                theDM.ShowDialog(Sewer_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Sewer_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (setDialog == "Sewer_Look")
        {
            if(DataBaseManager.Sewer_ObservationEnd == false)
            {
                theDM.ShowDialog(Sewer_Look.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Sewer_Observation_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }

        }
        if (setDialog == "Sewer_Observation_SucssesAfter")
        {
            theDM.ShowDialog(Sewer_Observation_SucssesAfter.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_Observation_Sucsses")
        {
            theDM.ShowDialog(Sewer_Observation_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_Observation_Fail")
        {
            theDM.ShowDialog(Sewer_Observation_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_Observation_SucssesAfterSanSuc")
        {
            theDM.ShowDialog(Sewer_Observation_SucssesAfterSanSuc.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        
}

    public void Start_1st_Hospital(string setDialog)
    {
        if (setDialog == "Hospital_NoonFirst")
        {
            theDM.ShowDialog(Hospital_NoonFirst.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Kate_FirstDialog")
        {
            theDM.ShowDialog(Kate_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Alan_FirstDialog")
        {
            if(DataBaseManager.Alan_FirstDialog == false)
            {
                DataBaseManager.Alan_FirstDialog = true;
                theDM.ShowDialog(Alan_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Alan_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }

        }

}


    public void KeywordDialog(string setDialog)
    {
        if (setDialog  == "Ella Triss")
        {
            if (DataBaseManager.keyword_downer == "Subject to request" && DataBaseManager.Intel_SewerGhostStory1 == false)
            {
                theDM.ShowDialog(Ella_key_Aiden.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {

                theDM.ShowDialog(Ella_key_disable.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Aiden Triss")
        {
            if (DataBaseManager.keyword_downer == "Aiden's Nightmare")
            {
                theDM.ShowDialog(Aiden_key_Nightmare1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Client")
            {
                theDM.ShowDialog(Aiden_key_EllaTriss1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's Safe")
            {
                if(DataBaseManager.getSafe == false)
                    theDM.ShowDialog(Aiden_key_Safe1.transform.GetComponent<interactionEvent>().GetDialogs());
                else
                {
                    theDM.ShowDialog(Aiden_Judge_Observation_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else 
            {
                if(DataBaseManager.AidenKeywordCount == 0)
                {
                    DataBaseManager.AidenKeywordCount += 1;
                    theDM.ShowDialog(Aiden_key_Fail1.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Aiden_key_Fail2.transform.GetComponent<interactionEvent>().GetDialogs());
                }
      
            }
        }
        if (setDialog == "Berkeley Swain")
        {
            if (DataBaseManager.keyword_downer == "Contents of a Newspaper")
            {
                theDM.ShowDialog(Swain_Key_PlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Newspaper Article")
            {
                theDM.ShowDialog(Swain_Key_Insomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's Nightmare")
            {
                theDM.ShowDialog(Swain_Key_Nightmare2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Husband's Condition")
            {
                theDM.ShowDialog(Swain_Key_Paranoia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Client")
            {
                theDM.ShowDialog(Swain_Key_EllaTriss1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Subject to request")
            {
                theDM.ShowDialog(Swain_Key_AidenTriss1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Husband's job")
            {
                theDM.ShowDialog(Swain_Key_DailyNews1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {

                theDM.ShowDialog(Swain_Key_Nothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "University student")
        {
            if (DataBaseManager.keyword_downer == "Contents of a Newspaper")
            {
                theDM.ShowDialog(Univ_Key_PlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Newspaper Article")
            {
                theDM.ShowDialog(Univ_Key_Insomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }

            else
            {

                theDM.ShowDialog(Univ_Key_Nothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Kane")
        {
            if (DataBaseManager.keyword_downer == "")
            {
                //theDM.ShowDialog(Univ_Key_PlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
            }

            else
            {

                theDM.ShowDialog(Kane_Nothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
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
        if (Sub_Dialog == "에이든 : 진단")
        {
            DataBaseManager.Aiden_medicine = true;
            if (result_End == "판정 : 성공" || result_End == "판정 : 대성공")
            {

               theDM.ShowDialog(Aiden_Judge_medicine_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "판정 : 실패" || result_End == "판정 : 대실패")
            {
                theDM.ShowDialog(Aiden_Judge_medicine_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "에이든 : 살펴보기")
        {
            DataBaseManager.Aiden_Observation = true;
            if (result_End == "판정 : 성공" || result_End == "판정 : 대성공")
            {

               theDM.ShowDialog(Aiden_Judge_Observation_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "판정 : 실패" || result_End == "판정 : 대실패")
            {
                theDM.ShowDialog(Aiden_Judge_Observation_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "에이든 : 안정")
        {
            DataBaseManager.Aiden_psychotherapy = true;

            if (result_End == "판정 : 성공" || result_End == "판정 : 대성공")
            {
                if(DataBaseManager.getSafe == false)
                {
                    theDM.ShowDialog(Aiden_Judge_psychotherapy_Sucsses_NoSafe.transform.GetComponent<interactionEvent>().GetDialogs());
                    DataBaseManager.getSafe = true;
                }
                else
                {
                    theDM.ShowDialog(Aiden_Judge_psychotherapy_Sucsses_Safe.transform.GetComponent<interactionEvent>().GetDialogs());
                    DataBaseManager.getSafe = true;
                }

            }
            else if (result_End == "판정 : 실패" || result_End == "판정 : 대실패")
            {
                theDM.ShowDialog(Aiden_Judge_medicine_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "에이든 : 설득")
        {

            if (result_End == "판정 : 성공" || result_End == "판정 : 대성공")
            {
                DataBaseManager.getSafe = true;
                theDM.ShowDialog(Aiden_key_Safe_rhetoric_sucsses.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "판정 : 실패" || result_End == "판정 : 대실패")
            {
                theDM.ShowDialog(Aiden_key_Safe_rhetoric_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Aiden : TakeAway")
        {

            if (result_End == "판정 : 성공" || result_End == "판정 : 대성공")
            {
                DataBaseManager.getSafe = true;
                theDM.ShowDialog(Aiden_key_Safe_Str_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "판정 : 실패" || result_End == "판정 : 대실패")
            {
                BillowUIManager.Instance.HP_down(5);
                theDM.ShowDialog(Aiden_key_Safe_Str_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Swain : Look")
        {

            if (result_End == "판정 : 성공" || result_End == "판정 : 대성공")
            {

                theDM.ShowDialog(Swain_LookJudge_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "판정 : 실패" || result_End == "판정 : 대실패")
            {

                theDM.ShowDialog(Swain_LookJudge_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        // 강가 Sewer : Look
        if (Sub_Dialog == "Sewer : Look")
        {

            if (result_End == "판정 : 성공" || result_End == "판정 : 대성공")
            {

                theDM.ShowDialog(Sewer_Observation_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "판정 : 실패" || result_End == "판정 : 대실패")
            {

                theDM.ShowDialog(Sewer_Observation_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "SAN : Check")
        {

            if (result_End == "판정 : 성공" || result_End == "판정 : 대성공")
            {

                theDM.ShowDialog(Sewer_Observation_SucssesAfterSanSuc.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "판정 : 실패" || result_End == "판정 : 대실패")
            {
                BillowUIManager.Instance.San_Down(10);
                theDM.ShowDialog(Sewer_Observation_SucssesAfter.transform.GetComponent<interactionEvent>().GetDialogs());
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

