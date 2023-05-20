using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class InteractionController : MonoBehaviour
{
    public DialogManager theDM;


    //의뢰자의 집
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
    //밤
    public GameObject Fab_FirstDialog;
    public GameObject Fab_Aiden1;
    public GameObject Fab_Safe1;
    public GameObject Fab_Nightmare1;
    public GameObject Fab_Nightmare2;
    public GameObject Fab_Nightmare3;
    public GameObject Fab_Nightmare4;
    public GameObject Fab_Insomnia1;
    public GameObject Fab_Insomnia2;
    public GameObject Fab_PlanetarySequence1;
    public GameObject Fab_Univ1;
    public GameObject Fab_Meiv2;
    public GameObject Fab_MissingPeople;
    public GameObject Fab_GhostStory1;
    public GameObject Fab_GhostStory2;
    public GameObject Fab_CreepyEye;
    public GameObject Fab_Worker1;
    public GameObject Fab_FishySmell1;
    public GameObject Fab_FishySmell2;
    public GameObject Fab_FishySmell3;
    public GameObject Fab_EndDay;
    public GameObject Fab_Connect;
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
    //오후 추가
    public GameObject Aiden_key_CreepyEye1;
    public GameObject Ella_Key_FishySmell1_noGhosty1;
    public GameObject Ella_Key_FishySmell1_yesGhosty1;

    // 1st DailyNews
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
    //오후추가
    public GameObject Swain_Key_MissingPeople1;
    public GameObject Swain_Key_FishySmell2;
    public GameObject Swain_Key_GhostStory1;
    public GameObject Swain_Key_CreepyEye1;


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
    public GameObject Kane_key_waterQuality1_noFishySmell;
    public GameObject Kane_key_waterQuality1_yesFishySmell;

    public GameObject Sewer_FirstDialog; //
    public GameObject Sewer_Dialog; //
    public GameObject Sewer_Look;
    public GameObject Sewer_Observation_Sucsses;
    public GameObject Sewer_Observation_SucssesAfter;
    public GameObject Sewer_Observation_Fail;
    public GameObject Sewer_Observation_SucssesAfterSanSuc;

    // 1st Hospital
    public GameObject Hospital_NoonFirst; //
    public GameObject Kate_FirstDialog; //
    public GameObject Alan_FirstDialog; //
    public GameObject Alan_Dialog; //
    //오후
    public GameObject Kate_NoonFirstDialog;
    public GameObject Kate_NoonDialog;
    public GameObject Kate_NoonLook;
    public GameObject Kate_NoonInsomnia1;
    public GameObject Kate_NoonNightmare1;
    public GameObject Kate_NoonParanoia1;
    public GameObject Kate_NoonHospital1;
    public GameObject Kate_NoonFishySmell1;
    public GameObject Kate_NoonNothing;
    public GameObject Alan_NoonFirstDialog;
    public GameObject Alan_NoonDialog;
    public GameObject Alan_NoonLook;
    public GameObject Alan_NoonNightmare1;
    public GameObject Alan_NoonNightmare2;
    public GameObject Alan_NoonNightmare3;
    public GameObject Alan_NoonInsomnia1;
    public GameObject Alan_NoonParanoia1;
    public GameObject Alan_NoonAiden1;
    public GameObject Alan_NoonNothing;
    // 1st PoliceOffice
    public GameObject A_FirstDialog; 
    public GameObject A_Dialog;
    public GameObject B_FirstDialog; 
    public GameObject B_Dialog; 
    public GameObject Albert_FirstDialog; 
    public GameObject Albert_Dialog; 
    public GameObject Albert_Key_PlanetarySequence1;
    public GameObject Albert_Key_Insomnia1;
    public GameObject Albert_Key_Meiv1;
    public GameObject Albert_Key_Nothing;
    public GameObject A_Look;
    public GameObject B_Look;
    public GameObject Albert_Look;
    //오후
    public GameObject Albert_Key_SewerGhostStory1;
    public GameObject Albert_Key_CreepyEye1;
    public GameObject Albert_Key_FishySmell1;
    public GameObject Albert_Key_MissingPeople2;
    // 1st Slum
    public GameObject Dave_FirstDialog;
    public GameObject Dave_Dialog;
    public GameObject Dave_Ran1;
    public GameObject Dave_Ran2;
    public GameObject Dave_Ran3;
    public GameObject Dave_Ran4;
    public GameObject Dave_Ran5;
    public GameObject Dave_Ran6;
    public GameObject Dave_Ran7;
    public GameObject Dave_Ran8;
    public GameObject Dave_NoMoney;
    public GameObject Dave_RanEnd;

    // 1st Slum
    public GameObject Maeve_FirstDialog_JobO;
    public GameObject Maeve_FirstDialog_JobX;
    public GameObject Maeve_Dialog;
    public GameObject Maeve_Look;
    public GameObject Maeve_Key_Intel_PlanetarySequence1;
    public GameObject Maeve_Key_Insomnia1;
    public GameObject Maeve_Key_Albert1;
    public GameObject Maeve_Key_MissingPeople;
    public GameObject Maeve_Key_CreepyEye;
    public GameObject Maeve_Key_FishySmell1;
    public GameObject Maeve_Key_FishySmell2;
    public GameObject Maeve_Key_Aiden1;
    public GameObject Maeve_Key_Nothing;
    public GameObject Worker_FirstDialog;
    public GameObject Worker_Dialog;
    public GameObject Worker_Look_O;
    public GameObject Worker_Look_X;

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
        else if (setDialog == "Fab_FirstDialog")
        {

            theDM.ShowDialog(Fab_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Aiden1")
        {

            theDM.ShowDialog(Fab_Aiden1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Safe1")
        {

            theDM.ShowDialog(Fab_Safe1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Nightmare1")
        {

            theDM.ShowDialog(Fab_Nightmare1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Nightmare2")
        {

            theDM.ShowDialog(Fab_Nightmare2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Nightmare3")
        {

            theDM.ShowDialog(Fab_Nightmare3.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Nightmare4")
        {

            theDM.ShowDialog(Fab_Nightmare4.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Insomnia1")
        {

            theDM.ShowDialog(Fab_Insomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Insomnia2")
        {

            theDM.ShowDialog(Fab_Insomnia2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_PlanetarySequence1")
        {

            theDM.ShowDialog(Fab_PlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Univ1")
        {

            theDM.ShowDialog(Fab_Univ1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Meiv2")
        {

            theDM.ShowDialog(Fab_Meiv2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_MissingPeople")
        {

            theDM.ShowDialog(Fab_MissingPeople.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        
        else if (setDialog == "Fab_GhostStory1")
        {

            theDM.ShowDialog(Fab_GhostStory1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_GhostStory2")
        {

            theDM.ShowDialog(Fab_GhostStory2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_CreepyEye")
        {

            theDM.ShowDialog(Fab_CreepyEye.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Worker1")
        {

            theDM.ShowDialog(Fab_Worker1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_FishySmell1")
        {

            theDM.ShowDialog(Fab_FishySmell1.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        else if (setDialog == "Fab_FishySmell1")
        {

            theDM.ShowDialog(Fab_FishySmell1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_FishySmell2")
        {

            theDM.ShowDialog(Fab_FishySmell2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_FishySmell3")
        {

            theDM.ShowDialog(Fab_FishySmell3.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_EndDay")
        {

            theDM.ShowDialog(Fab_EndDay.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Connect")
        {

            theDM.ShowDialog(Fab_Connect.transform.GetComponent<interactionEvent>().GetDialogs());
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
        if (setDialog == "Kate_NoonLook")
        {
            theDM.ShowDialog(Kate_NoonLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Alan_NoonLook")
        {
            theDM.ShowDialog(Alan_NoonLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Hospital_NoonFirst")
        {
            theDM.ShowDialog(Hospital_NoonFirst.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Kate_FirstDialog")
        {
            if ((DataBaseManager.TimeCount % 4) == 2)
            {
                theDM.ShowDialog(Kate_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                if (DataBaseManager.Kate_NoonFirstDialog == false)
                {
                    DataBaseManager.Kate_NoonFirstDialog = true;
                    theDM.ShowDialog(Kate_NoonFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Kate_NoonDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }

        }
        if (setDialog == "Alan_FirstDialog")
        {
            if ((DataBaseManager.TimeCount % 4) == 2)
            {
                if (DataBaseManager.Alan_FirstDialog == false)
                {
                    DataBaseManager.Alan_FirstDialog = true;
                    theDM.ShowDialog(Alan_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Alan_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                if (DataBaseManager.Alan_NoonFirstDialog == false)
                {
                    DataBaseManager.Alan_NoonFirstDialog = true;
                    theDM.ShowDialog(Alan_NoonFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Alan_NoonDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
        }
    }

    public void Start_1st_Policeoffice(string setDialog)
    {

        if (setDialog == "A_FirstDialog")
        {
            if (DataBaseManager.A_FirstDialog == false)
            {
                DataBaseManager.A_FirstDialog = true;
                theDM.ShowDialog(A_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(A_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "B_FirstDialog")
        {
            if (DataBaseManager.B_FirstDialog == false)
            {
                DataBaseManager.B_FirstDialog = true;
                theDM.ShowDialog(B_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(B_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Albert_FirstDialog")
        {
            if (DataBaseManager.Albert_FirstDialog == false)
            {
                DataBaseManager.Albert_FirstDialog = true;
                theDM.ShowDialog(Albert_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Albert_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "A_Look")
        {
            theDM.ShowDialog(A_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "B_Look")
        {
            theDM.ShowDialog(B_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Albert_Look")
        {
            theDM.ShowDialog(Albert_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }

}
    public void Start_1st_Slum(string setDialog)
    {

        if (setDialog == "Dave_FirstDialog")
        {
            if (DataBaseManager.Dave_FirstDialog == false)
            {
                DataBaseManager.Dave_FirstDialog = true;
                theDM.ShowDialog(Dave_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Dave_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (setDialog == "Dave_Ran1")
        {
            theDM.ShowDialog(Dave_Ran1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran2")
        {
            theDM.ShowDialog(Dave_Ran2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran3")
        {
            theDM.ShowDialog(Dave_Ran3.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran4")
        {
            theDM.ShowDialog(Dave_Ran4.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran5")
        {
            theDM.ShowDialog(Dave_Ran5.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran6")
        {
            theDM.ShowDialog(Dave_Ran6.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran7")
        {
            theDM.ShowDialog(Dave_Ran7.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran8")
        {
            theDM.ShowDialog(Dave_Ran8.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_NoMoney")
        {
            theDM.ShowDialog(Dave_NoMoney.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_RanEnd")
        {
            theDM.ShowDialog(Dave_RanEnd.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        
    }
    public void Start_1st_Bar(string setDialog)
    {

        if (setDialog == "Maeve_FirstDialog")
        {
            if (DataBaseManager.Maeve_FirstDialog == false)
            {
                DataBaseManager.Maeve_FirstDialog = true;
                if(DataBaseManager.Intel_QuestHouse1 == true)
                {
                    theDM.ShowDialog(Maeve_FirstDialog_JobO.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Maeve_FirstDialog_JobX.transform.GetComponent<interactionEvent>().GetDialogs());
                }

            }
            else
            {
                theDM.ShowDialog(Maeve_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (setDialog == "Maeve_Look")
        {
            theDM.ShowDialog(Maeve_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        if (setDialog == "Worker_FirstDialog")
        {
            if (DataBaseManager.Worker_FirstDialog == false)
            {
                DataBaseManager.Worker_FirstDialog = true;
                theDM.ShowDialog(Worker_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else
            {
                theDM.ShowDialog(Worker_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (setDialog == "Worker_Look")
        {
            if (DataBaseManager.Intel_FishySmell1 == true || DataBaseManager.Intel_FishySmell2 == true|| DataBaseManager.Intel_FishySmell3 == true|| DataBaseManager.Intel_FishySmell4 == true)
            {
                theDM.ShowDialog(Worker_Look_O.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Worker_Look_X.transform.GetComponent<interactionEvent>().GetDialogs());
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
            else if (DataBaseManager.keyword_downer == "Rumor")
            {
                if(DataBaseManager.Intel_SewerGhostStory1 == true)
                {
                    theDM.ShowDialog(Ella_Key_FishySmell1_yesGhosty1.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Ella_Key_FishySmell1_noGhosty1.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                
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
            else if (DataBaseManager.keyword_downer == "Saw in the sewer")
            {
                theDM.ShowDialog(Aiden_key_CreepyEye1.transform.GetComponent<interactionEvent>().GetDialogs());
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
                if(DataBaseManager.Intel_MissingPeople1 == false || DataBaseManager.Swain_isTalkMissingpeople == false)
                {
                    DataBaseManager.Swain_isTalkMissingpeople = true;
                    theDM.ShowDialog(Swain_Key_DailyNews1.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Swain_Key_Nothing.transform.GetComponent<interactionEvent>().GetDialogs());
                }

            }
            else if (DataBaseManager.keyword_downer == "Albert's Information")
            {
                if (DataBaseManager.Intel_MissingPeople2 == false &&DataBaseManager.Swain_isTalkMissingpeople == false)
                {
                    DataBaseManager.Swain_isTalkMissingpeople = true;
                    theDM.ShowDialog(Swain_Key_MissingPeople1.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Swain_Key_Nothing.transform.GetComponent<interactionEvent>().GetDialogs());
                }


            }
            else if (DataBaseManager.keyword_downer == "Source of the smell?")
            {
                theDM.ShowDialog(Swain_Key_FishySmell2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Contents of a GhostStory")
            {
                theDM.ShowDialog(Swain_Key_GhostStory1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Saw in the sewer")
            {
                theDM.ShowDialog(Swain_Key_CreepyEye1.transform.GetComponent<interactionEvent>().GetDialogs());
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

            if (DataBaseManager.keyword_downer == "Strange Point")
            {
                if(DataBaseManager.Intel_FishySmell1 == true)
                {
                    theDM.ShowDialog(Kane_key_waterQuality1_yesFishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Kane_key_waterQuality1_noFishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }

            else
            {

                theDM.ShowDialog(Kane_Nothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Albert Bradley")
        {
            if (DataBaseManager.keyword_downer == "Contents of a Newspaper")
            {
                theDM.ShowDialog(Albert_Key_PlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Newspaper Article")
            {
                theDM.ShowDialog(Albert_Key_Insomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Mafia friend")
            {
                theDM.ShowDialog(Albert_Key_Meiv1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Contents of a GhostStory")
            {
                theDM.ShowDialog(Albert_Key_SewerGhostStory1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Rumor")
            {
                theDM.ShowDialog(Albert_Key_FishySmell1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Saw in the sewer")
            {
                theDM.ShowDialog(Albert_Key_CreepyEye1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Reporting by Swain")
            {
                theDM.ShowDialog(Albert_Key_MissingPeople2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {

                theDM.ShowDialog(Albert_Key_Nothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Kate Lizzie")
        {
            if (DataBaseManager.keyword_downer == "Newspaper Article")
            {
                theDM.ShowDialog(Kate_NoonInsomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's Nightmare")
            {
                theDM.ShowDialog(Kate_NoonNightmare1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Husband's Condition")
            {
                theDM.ShowDialog(Kate_NoonParanoia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "State of congestion")
            {
                theDM.ShowDialog(Kate_NoonHospital1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Rumor")
            {
                theDM.ShowDialog(Kate_NoonFishySmell1.transform.GetComponent<interactionEvent>().GetDialogs());
            }

            else
            {

                theDM.ShowDialog(Kate_NoonNothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Alan Parks")
        {
            if (DataBaseManager.keyword_downer == "Aiden's Nightmare")
            {
                theDM.ShowDialog(Alan_NoonNightmare1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Content of a Nightmare")
            {
                theDM.ShowDialog(Alan_NoonNightmare2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's Detail")
            {
                theDM.ShowDialog(Alan_NoonNightmare3.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Newspaper Article")
            {
                theDM.ShowDialog(Alan_NoonInsomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Husband's Condition")
            {
                theDM.ShowDialog(Alan_NoonParanoia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Subject to request")
            {
                theDM.ShowDialog(Alan_NoonAiden1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Alan_NoonNothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (setDialog == "Maeve")
        {
            if (DataBaseManager.keyword_downer == "Contents of a Newspaper" || DataBaseManager.keyword_downer == "Astronomical show")
            {
                theDM.ShowDialog(Maeve_Key_Intel_PlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Newspaper Article")
            {
                theDM.ShowDialog(Maeve_Key_Insomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Police friend")
            {
                theDM.ShowDialog(Maeve_Key_Albert1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Reporting by Swain" || DataBaseManager.keyword_downer == "Albert's Information")
            {
                theDM.ShowDialog(Maeve_Key_MissingPeople.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Saw in the sewer")
            {
                theDM.ShowDialog(Maeve_Key_CreepyEye.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Rumor")
            {
                theDM.ShowDialog(Maeve_Key_FishySmell1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Source of the smell?")
            {
                theDM.ShowDialog(Maeve_Key_FishySmell2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Subject to request")
            {
                theDM.ShowDialog(Maeve_Key_Aiden1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Maeve_Key_Nothing.transform.GetComponent<interactionEvent>().GetDialogs());
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

