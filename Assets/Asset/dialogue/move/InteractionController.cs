using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class InteractionController : MonoBehaviour
{
    public DialogManager theDM;

    //Item_Box
    public GameObject Safe_Look;
    public GameObject Safe_Fail;
    public GameObject Safe_Succ;
    public GameObject Bracelet_Look;
    public GameObject OldMap_Look;
    
    public GameObject Document_Look;
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

    // 전투
    public GameObject Battle_StartDialog;
    public GameObject Battle_SanCheck;
    public GameObject Battle_SanCheckFail;
    public GameObject Battle_EndBattle;
    //public GameObject Sewer_Observation_SucssesAfterSanSuc;

    //2st 탐정사무소
    public GameObject Fab_Case1;
    public GameObject Fab_Case1After;
    public GameObject Fab_Case2;
    public GameObject Fab_Case2Succ;
    public GameObject Fab_Case2SuccAfter;
    public GameObject Fab_Case2Fail;
    public GameObject Fab_Case3;

    //2st 의뢰자
    public GameObject Ella_key_ContentsofaNewspaper;
    public GameObject Ella_key_ContentofaNightmare;
    public GameObject Ella_key_AidensDetail;
    public GameObject Ella_key_WaterandNightmare;
    public GameObject Ella_key_Doctorsopinion;
    public GameObject Ella_key_AidensSafe;
    public GameObject Ella_key_AcquiringaSafe;
    public GameObject Ella_key_AidenandPandemic;

    public GameObject Aiden_key_Missing;
    public GameObject Aiden_key_FishySmellOrigin;
    public GameObject Aiden_key_ContentsofaGhostStory;
    public GameObject Aiden_key_OwnerofEye;

    //2st 신문사
    public GameObject Swain_key_ContentofaNightmare;
    public GameObject Swain_key_WaterandNightmare;
    public GameObject Swain_key_FishySmell_Reort;
    public GameObject Swain_key_FishySmell_None;
    public GameObject Swain_key_Details;
    public GameObject Swain_key_AidenandPandemic;
    public GameObject Swain_key_Unfairdismissal;



    //2st 대학교
    public GameObject Student_key_ContentofaNightmare;
    public GameObject Student_key_WaterandNightmare;
    public GameObject Student_key_Rumor;
    public GameObject Student_key_FishySmell;
    public GameObject Student_key_Details;


    //2st 경찰서
    public GameObject Albert_key_WaterandNightmare;
    public GameObject Albert_key_FishySmell;
    public GameObject Albert_key_Details_noOrigin;
    public GameObject Albert_key_Details_Origin;
    public GameObject Albert_key_Promise;
    public GameObject Albert_key_Unfairdismissal;

    //2st 병원
    public GameObject Kate_key_ContentsofaGhostStory;
    public GameObject Kate_key_Sawinthesewer;
    public GameObject Kate_key_Subjecttorequest;
    public GameObject Alan_key_Details;
    public GameObject Alan_key_Stateofcongestion;

    //2st 술집
    public GameObject Meave_key_WaterandNightmare;
    public GameObject Meave_key_ContentsofaGhostStory;
    public GameObject Meave_key_Truthofarumor;
    public GameObject Meave_key_Details;

    //2st 강제 이벤트
    public GameObject NoonEvent_FirstDailog_NoMissing;
    public GameObject NoonEvent_FirstDailog_MissingPeople;
    public GameObject NoonEvent_FirstDailog_Noquest;
    public GameObject NoonEvent_ClientHouse;

    public GameObject NoonEvent_Witness_FirstDialog;
    public GameObject NoonEvent_Witness_RepeatDialog;
    public GameObject NoonEvent_Witness_Look;
    public GameObject NoonEvent_Witness_ListeningFail;
    public GameObject NoonEvent_Witness_ListeningSucc_Nomal;
    public GameObject NoonEvent_Witness_ListeningSucc_MissingPeople;
    public GameObject NoonEvent_Witness_ListeningSucc_NoRequest;
    public GameObject NoonEvent_DoorPolice_FirstDialog;
    public GameObject NoonEvent_DoorPolice_RepeatDialog;
    public GameObject NoonEvent_DoorPolice_Look;
    public GameObject NoonEvent_DoorPolice_PersuasionSucc;
    public GameObject NoonEvent_DoorPolice_PersuasionFail;
    public GameObject NoonEvent_FirstDoor;
    public GameObject NoonEvent_FirstDoorAfterSan;
    public GameObject NoonEvent_InsidePolice_FirstDialog;
    public GameObject NoonEvent_InsidePolice_RepeatDialog;
    public GameObject NoonEvent_InsidePolice_Look;
    public GameObject NoonEvent_DeadBody_Dialog;
    public GameObject NoonEvent_DeadBody_Look_NoRumor;
    public GameObject NoonEvent_DeadBody_Look_YesRumor;
    public GameObject NoonEvent_DeadBody_Look_Missing;
    public GameObject NoonEvent_DeadBody_Look_FishySmell;

    public GameObject NoonEvent_DeadBody_medicineSucc;
    public GameObject NoonEvent_DeadBody_medicineFail;
    public GameObject NoonEvent_DeadBody_ObservationSucc;
    public GameObject NoonEvent_DeadBody_ObservationFail;
    public GameObject NoonEvent_DeadBody_analysisSucc;
    public GameObject NoonEvent_DeadBody_analysisSuccAfter;
    public GameObject NoonEvent_DeadBody_analysisFail;
    public GameObject NoonEvent_DeadBody_deftnessSucc;
    public GameObject NoonEvent_DeadBody_deftnessFail;

    // 강제 이번트 이후 추가 키워드 
    // 신문사
    public GameObject Swain_NoContect_FirstDialog;
    public GameObject Swain_Kidnap;
    public GameObject Swain_fishySlime;
    public GameObject Swain_ManholeOrWarn;
    public GameObject Swain_MurderedElla;
    public GameObject Swain_MissingAiden_Nomal;
    public GameObject Swain_MissingAiden_MissingPeopleOk;
    public GameObject Swain_witness;
    public GameObject Swain_culprit;
    public GameObject Swain_MissingandFishy;
    public GameObject Swain_EllaDeathCause;
    public GameObject Swain_WebbedFeet;
    public GameObject Swain_RunawayRoute;

    // 의뢰자의 집
    public GameObject Site_Dialog;
    public GameObject Site_Look;
    public GameObject Site_analysisSucc;
    public GameObject Site_analysisSuccAfter;
    public GameObject Site_analysisFail;

    //대학교
    public GameObject Kane_FirstMeatatSev;
    public GameObject Kane_DetailOB;
    public GameObject Kane_AfterSevDilaog;
    public GameObject Kane_AfterSevLook;
    public GameObject Kane_FishySlime_CreepyEye;
    public GameObject Kane_FishySlime_CreepyEyeAfterSans;
    public GameObject Kane_FishySlime_Nomal;

    //빈민가
    public GameObject Dave_Rand9;
    public GameObject Dave_Key_SafeAquire;
    public GameObject Dave_Key_Breclet;
    public GameObject Dave_Key_Witness;
    public GameObject Dave_Key_Fail;
    public GameObject Dave_Key_BrecletAfter;
    public GameObject Dave_Key_SafeAfter;
    public GameObject Dave_Key_OpenSafe;

    //병원
    public GameObject Kate_Key_EllaBody;
    public GameObject Kate_Key_Witness;
    public GameObject Alan_Key_EllaBody;

    // 경찰서
    public GameObject Albert_FirstDialogAfterEvent;
    public GameObject Albert_FirstDialogNew;
    public GameObject Albert_Key_WarnOfAiden;
    public GameObject Albert_Key_Rootofproblem;
    public GameObject Albert_Key_DeadElla;
    public GameObject Albert_Key_MissingAiden;
    public GameObject Albert_Key_Witness;
    public GameObject Albert_Key_Culprit;
    public GameObject Albert_Key_CauseOfDeath;
    public GameObject Albert_Key_WebbedFeet;
    public GameObject Albert_Key_RunwayMenhole;

    // 술집
    public GameObject Day2Bar_fishysmellO;
    public GameObject Day2Bar_fishysmellX;
    public GameObject Day2Bar_1stBarIn;
    public GameObject Day2Bar_firstBar;
    public GameObject Meave_key_CauseOfProblem;
    public GameObject Meave_key_DEllaMAiden;
    public GameObject Meave_key_Witness;
    public GameObject Meave_key_MissingFishyCauseX;
    public GameObject Meave_key_MissingFishyCauseO;
    public GameObject Meave_Judge_RhetoricSucc;
    public GameObject Meave_Judge_RhetoricSanAfter;
    public GameObject Meave_Judge_RhetoricFail;

    //탐정사무소 추가 정리
    
    public GameObject Fab_2ndConnectStart;
    public GameObject Fab_Witness;
    public GameObject Fab_OwnerofUnifrom;
    public GameObject Fab_CauseofDeath;
    public GameObject Fab_WebbedFeet;
    public GameObject Fab_Runway;
    public GameObject Fab_FishySmellMissing;
    public GameObject Fab_TheClupet;
    public GameObject Fab_Both;
    public GameObject Fab_WarnOrRoot;
    public GameObject Fab_SwainSewer;
    public GameObject SecondDream;
    public GameObject ThirdDayStartCase1;
    public GameObject ThirdDayStartCase1AfterSan;
    public GameObject ThirdDayStartCase2;

    // 3일차 추가
    // 경찰서
    public GameObject Albert_ThirdFirstDialog;
    public GameObject Albert_Key_OwnerOfOutfit;
    public GameObject Albert_KeyChange_MissingFishySmell;
    public GameObject Albert_KeyChange_WebbedFeet;
    public GameObject PoliceA_NoAlbert;
    public GameObject PoliceB_NoAlbert;

    // 병원
    public GameObject Alan_ThirdFirstDialog;
    public GameObject Alan_Key_MurderdElla;
    public GameObject Alan_Key_CauseOfDeath;
    public GameObject Alan_Key_WebbedFeet;


    //술집
    public GameObject Meave_ThirdDay_FirstO;
    public GameObject Meave_ThirdDay_FirstX;

    //밤 악몽 연출
    public GameObject ThirdNightmare;
    public GameObject Fab_4th_QuestO;
    public GameObject Fab_4th_QuestO_SanCheck;
    public GameObject Fab_4thQuestO_HealthCehckSucc;
    public GameObject Fab_4th_QuestO_HealthCehckFail;
    public GameObject Fab_4thQuestX;
    public GameObject Fab_4thQuestX_HealthCehckSucc;
    public GameObject Fab_4th_QuestX_HealthCehckFail;

    //하수정비사무소
    public GameObject SewerDoordialog;
    public GameObject SewerDoorLook;
    public GameObject SewerDoorPickSucc;
    public GameObject SewerDoorPickFail;
    public GameObject Window_dialog_BFNoon;
    public GameObject Window_lookBFNoon;
    public GameObject Window_dialog_AFNoon;
    public GameObject Window_lookAFNoon;
    public GameObject Window_JudgeBreakSucc;
    public GameObject Window_JudgeBreakFail;

    public GameObject Loker_dialog_BFNoon;
    public GameObject Loker_look_BFNoon;
    public GameObject Loker_dialog_AFNoon;
    public GameObject Loker_look_AFNoon;
    public GameObject Loker_Judge_ObservationSucc;
    public GameObject Loker_Judge_ObservationFail;

    public GameObject Map_dialog_BFNoon;
    public GameObject Map_look_BFNoon;
    public GameObject Map_dialog_AFNoon;
    public GameObject Map_look_AFNoon;
    public GameObject Item_CoatLook;
    public GameObject Item_DiaguiseSucc;
    public GameObject Item_DiaguiseFail;
    public GameObject Item_MapLook;

    public GameObject BlackWell_FirstDialog;
    public GameObject BlackWell_Dialog;
    public GameObject BlackWell_DialogLook;
    public GameObject BlackWell_Judege_RheticSucc;
    public GameObject BlackWell_Judege_RheticFail;
    public GameObject BlackWell_Judege_DeftnessSucc;
    public GameObject BlackWell_Judege_DeftnessFail;
    public GameObject BlackWell_Key_WarnRoot;
    public GameObject BlackWell_Key_Witness;
    public GameObject BlackWell_Key_runway;
    public GameObject BlackWell_Key_Fishysmell;
    public GameObject BlackWell_Key_Fail;
    public GameObject AfterJudgeLookLocker;
    public GameObject SewergeKey_Look;


    //하수도 내부
    public GameObject Sewer_EnemySpawn;
    public GameObject Sewer_StealthSucc;
    public GameObject Sewer_StealthFail;

    public GameObject Sewer_DisgauseFail;
    public GameObject Sewer_RhethicSucc;
    public GameObject Sewer_RhethicFail;

    public GameObject Sewer_MapDialog;
    public GameObject Sewer_MapLook;
    public GameObject Sewer_JournalDialog;
    public GameObject Sewer_JornalLook;
    public GameObject Sewer_JornalLookSanCheck;
    public GameObject Sewer_JornalLookAfterSanCheck;
    public GameObject Sewer_BookDialog;
    public GameObject Sewer_BookLook;
    public GameObject Sewer_BookLookSanCheck;
    public GameObject Sewer_BookLookOccultSucc;
    public GameObject Sewer_BookLookOccultFail;
    public GameObject Sewer_BookLookAfterCheck;

    public GameObject Sewer_SymbolDialog;
    public GameObject Sewer_SymbolLook_YseJournal;
    public GameObject Sewer_SymbolLook_NoJournal;
    public GameObject Sewer_SymbolLook_OccultSucc;
    public GameObject Sewer_SymbolLook_OccultFail;
    public GameObject Sewer_JudgeStrSucc;
    public GameObject Sewer_JudgeStrFail;
    public GameObject Sewer_SanChecck;

    public GameObject Sewer_SymbolLook_NoJournalAfterOccult;
    public GameObject Sewer_SymbolKeyWorkSucc;
    public GameObject Sewer_SymbolKeyWorkFail;

    public GameObject AfterDaveDeath;

    public GameObject RiverDeftnessSucc;
    public GameObject RiverDeftnessFail;

    public GameObject Journalist_FirstDialog;


    //하수도 내부
    public GameObject Sewer_FirstIn;
    public GameObject Sewer_FirstInAfterSan;
    public GameObject Sewer_FirstDeepOne;
    public GameObject Sewer_FirstDeepOneAfterSan;

    public GameObject Sewer_LadderDialog;
    public GameObject Sewer_LadderUP;
    public GameObject Sewer_LadderSwin;
    public GameObject Sewer_LadderSan1;
    public GameObject Sewer_LadderSan2;
    public GameObject Sewer_LadderSan3;
    public GameObject Sewer_LadderSan4;
    public GameObject Sewer_LadderSan5;
    public GameObject Sewer_LadderSan6;
    public GameObject Sewer_LadderSan7;
    public GameObject Sewer_LadderSan8;
    public GameObject Sewer_LadderSan9;

    public GameObject BloodEnding;


    public GameObject Ending_BreakSymbol;
    public GameObject Ending_IfGotPromiseAndAblertAlive;
    public GameObject Ending_IfGotPromiseAndAblertDeath;
    public GameObject Ending_IfNoPromise;
    public GameObject Ending_IfNoPromiseAlbertDeath;
    public GameObject Ending_IfSwainDeath;
    public GameObject Ending_LastIfGotRequest;
    public GameObject Ending_LastIfNoRequest;
    public GameObject Ending_Killing;
    public GameObject Ending_DoorEnd;


    private void Start()
    {
        //TestNar();
    }

    public void InSewerDialog(string setDialog)
    {
        
             if (setDialog == "Sewer_LadderUP")
        {
            theDM.ShowDialog(Sewer_LadderUP.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        if (setDialog == "Sewer_EnemySpawn")
        {
            theDM.ShowDialog(Sewer_EnemySpawn.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_StealthSucc")
        {
            theDM.ShowDialog(Sewer_StealthSucc.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_StealthFail")
        {
            theDM.ShowDialog(Sewer_StealthFail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_DisgauseFail")
        {
            theDM.ShowDialog(Sewer_DisgauseFail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
       
        if (setDialog == "Sewer_MapDialog")
        {
            theDM.ShowDialog(Sewer_MapDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_MapLook")
        {
            theDM.ShowDialog(Sewer_MapLook.transform.GetComponent<interactionEvent>().GetDialogs());


        }


        if (setDialog == "Sewer_JournalDialog")
        {
            theDM.ShowDialog(Sewer_JournalDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_JornalLook")
        {
            if (DataBaseManager.firstLookJournal == false)
            {
                DataBaseManager.firstLookJournal = true;
                theDM.ShowDialog(Sewer_JornalLook.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else
            {
                theDM.ShowDialog(Sewer_JornalLookAfterSanCheck.transform.GetComponent<interactionEvent>().GetDialogs());

            }
        }
        if (setDialog == "Sewer_JornalLookAfterSanCheck")
        {
            theDM.ShowDialog(Sewer_JornalLookAfterSanCheck.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_BookDialog")
        {
            theDM.ShowDialog(Sewer_BookDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_BookLook")
        {
            if(DataBaseManager.BookLookFirst == false)
            {
                DataBaseManager.BookLookFirst = true;
                theDM.ShowDialog(Sewer_BookLook.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Sewer_BookLookAfterCheck.transform.GetComponent<interactionEvent>().GetDialogs());
            }

        }
        if (setDialog == "Sewer_JornalLookSanCheck")
        {
            theDM.ShowDialog(Sewer_JornalLookSanCheck.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        if (setDialog == "Sewer_SymbolDialog")
        {
            theDM.ShowDialog(Sewer_SymbolDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        } 
        if (setDialog == "Sewer_SymbolLook")
        {
            if(DataBaseManager.JournalFirstSanCheck == true)
            {
                theDM.ShowDialog(Sewer_SymbolLook_YseJournal.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                if(DataBaseManager.SymbolOccultFirst == false)
                {
                    theDM.ShowDialog(Sewer_SymbolLook_NoJournal.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Sewer_SymbolLook_NoJournalAfterOccult.transform.GetComponent<interactionEvent>().GetDialogs());
                }

            }



        }
        if (setDialog == "Sewer_FirstIn")
        {
            theDM.ShowDialog(Sewer_FirstIn.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_FirstDeepOne")
        {
            theDM.ShowDialog(Sewer_FirstDeepOne.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_LadderDialog")
        {
            theDM.ShowDialog(Sewer_LadderDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        if (setDialog == "Ending_BreakSymbol")
        {
            theDM.ShowDialog(Ending_BreakSymbol.transform.GetComponent<interactionEvent>().GetDialogs());
        }


        if (setDialog == "Ending_isPromise")
        {
            if(DataBaseManager.Intel_Meiv2 == true)
            {
                if(DataBaseManager.AlbertDeathKeyword == false)
                {
                    theDM.ShowDialog(Ending_IfGotPromiseAndAblertAlive.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Ending_IfGotPromiseAndAblertDeath.transform.GetComponent<interactionEvent>().GetDialogs());
                }


            }
            else if (DataBaseManager.Intel_Meiv2 == false)
            {
                theDM.ShowDialog(Ending_IfNoPromise.transform.GetComponent<interactionEvent>().GetDialogs());
            }


        }
        if (setDialog == "Ending_IfNoPromiseAlbertDeath")
        {
            theDM.ShowDialog(Ending_IfNoPromiseAlbertDeath.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ending_SwainDeathCheck")
        {
            theDM.ShowDialog(Ending_IfSwainDeath.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ending_RequestCheck")
        {
            if(DataBaseManager.IfGotRequset == true)
            {
                theDM.ShowDialog(Ending_LastIfGotRequest.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Ending_LastIfNoRequest.transform.GetComponent<interactionEvent>().GetDialogs());
            }

        }
        if (setDialog == "Ending_Killing")
        {
            theDM.ShowDialog(Ending_Killing.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ending_DoorEnd")
        {
            theDM.ShowDialog(Ending_DoorEnd.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        
    }

    public void Start_SewerOffice(string setDialog)
    {
        if (setDialog == "SewerDoordialog")
        {
            theDM.ShowDialog(SewerDoordialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "SewerDoorLook")
        {
            theDM.ShowDialog(SewerDoorLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        if(setDialog == "Window_dialog")
        {
            if(DataBaseManager.TimeCount%4 == 1|| DataBaseManager.TimeCount % 4 == 2)
            {
                theDM.ShowDialog(Window_dialog_BFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Window_dialog_AFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Window_look")
        {
            if (DataBaseManager.TimeCount % 4 == 1 || DataBaseManager.TimeCount % 4 == 2)
            {
                theDM.ShowDialog(Window_lookBFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Window_lookAFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Map_dialog")
        {
            if (DataBaseManager.TimeCount % 4 == 1 || DataBaseManager.TimeCount % 4 == 2)
            {
                theDM.ShowDialog(Map_dialog_BFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Map_dialog_AFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Map_look")
        {
            if (DataBaseManager.TimeCount % 4 == 1 || DataBaseManager.TimeCount % 4 == 2)
            {
                theDM.ShowDialog(Map_look_BFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Map_look_AFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (setDialog == "Locker_dialog")
        {
            if (DataBaseManager.TimeCount % 4 == 1 || DataBaseManager.TimeCount % 4 == 2)
            {
                theDM.ShowDialog(Loker_dialog_BFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Loker_dialog_AFNoon.transform.GetComponent<interactionEvent>().GetDialogs());

            }
        }
        if (setDialog == "Locker_look")
        {
            if (DataBaseManager.TimeCount % 4 == 1 || DataBaseManager.TimeCount % 4 == 2)
            {
                if(DataBaseManager.Black_RechSucc_CanTalck == true)
                {
                    theDM.ShowDialog(AfterJudgeLookLocker.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Loker_look_BFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
                }
       
            }
            else
            {
                theDM.ShowDialog(Loker_look_AFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "BlackWell_dialog")
        {
            if(DataBaseManager.Black_FirstDialog == false)
            {
                DataBaseManager.Black_FirstDialog = true;
                theDM.ShowDialog(BlackWell_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(BlackWell_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }

        }
        if (setDialog == "BlackWell_look")
        {
            theDM.ShowDialog(BlackWell_DialogLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void Start_2nd_NoonEvent(string setDialog)
    {
        if (setDialog == "NoonEvent_FirstDailog_Detective")
        {
            if(DataBaseManager.Intel_QuestHouse1 == false)
            {
                theDM.ShowDialog(NoonEvent_FirstDailog_Noquest.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Intel_MissingPeople1 == true || DataBaseManager.Intel_MissingPeople2 == true)
            {
                theDM.ShowDialog(NoonEvent_FirstDailog_MissingPeople.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(NoonEvent_FirstDailog_NoMissing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "NoonEvent_ClientHouse")
        {
            theDM.ShowDialog(NoonEvent_ClientHouse.transform.GetComponent<interactionEvent>().GetDialogs());
        }


        if (setDialog == "NoonEvent_Witness_Dialog")
        {

            if (DataBaseManager.Witness_FirstDailog == false)
            {
                DataBaseManager.Witness_FirstDailog = true;
                theDM.ShowDialog(NoonEvent_Witness_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(NoonEvent_Witness_RepeatDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
    
        if (setDialog == "NoonEvent_Witness_Look")
        {
            theDM.ShowDialog(NoonEvent_Witness_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        if (setDialog == "NoonEvent_DoorPolice_Dialog")
        {

            if (DataBaseManager.DoorPolice_FirstDailog == false)
            {
                DataBaseManager.DoorPolice_FirstDailog = true;
                theDM.ShowDialog(NoonEvent_DoorPolice_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(NoonEvent_DoorPolice_RepeatDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "NoonEvent_DoorPolice_Look")
        {
            theDM.ShowDialog(NoonEvent_DoorPolice_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NoonEvent_FirstDoor")
        {
            theDM.ShowDialog(NoonEvent_FirstDoor.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        if (setDialog == "NoonEvent_InsidePolice_Dialog")
        {

            if (DataBaseManager.InsidePolice_FirstDailog == false)
            {
                DataBaseManager.InsidePolice_FirstDailog = true;
                theDM.ShowDialog(NoonEvent_InsidePolice_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(NoonEvent_InsidePolice_RepeatDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "NoonEvent_InsidePolice_Look")
        {
            theDM.ShowDialog(NoonEvent_InsidePolice_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }


        if (setDialog == "NoonEvent_DeadBody_Dialog")
        {
            theDM.ShowDialog(NoonEvent_DeadBody_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NoonEvent_DeadBody_Look")
        {
            DataBaseManager.DeadBodyFirstLook = true;

            if (DataBaseManager.Intel_FishySmell1 == true)
            {
                theDM.ShowDialog(NoonEvent_DeadBody_Look_YesRumor.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(NoonEvent_DeadBody_Look_NoRumor.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "NoonEvent_DeadBody_Look_Missing")
        {
            theDM.ShowDialog(NoonEvent_DeadBody_Look_Missing.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NoonEvent_DeadBody_Look_FishySmell")
        {
            theDM.ShowDialog(NoonEvent_DeadBody_Look_FishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NoonEvent_DeadBody_analysisSuccAfter")
        {
            theDM.ShowDialog(NoonEvent_DeadBody_analysisSuccAfter.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        if (setDialog == "Accident Site_Dialog")
        {
            theDM.ShowDialog(Site_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Accident Site_Look")
        {
            theDM.ShowDialog(Site_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Site_analysisSuccAfter")
        {
            theDM.ShowDialog(Site_analysisSuccAfter.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        
    }
    public void Start_2nd_DetectiveOffice(string setDialog)
    {
        if (setDialog == "Fab_Case1")
        {
            theDM.ShowDialog(Fab_Case1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Case1After")
        {
            theDM.ShowDialog(Fab_Case1After.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Case2")
        {
            theDM.ShowDialog(Fab_Case2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Case2Succ")
        {
            theDM.ShowDialog(Fab_Case2Succ.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Case2SuccAfter")
        {
            theDM.ShowDialog(Fab_Case2SuccAfter.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Case2Fail")
        {
            theDM.ShowDialog(Fab_Case2Fail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Case3")
        {
            theDM.ShowDialog(Fab_Case3.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }

    public void Start_Safe_Look(string setDialog)
    {
        
                   if (setDialog == "OldMap_Look")
        {
            theDM.ShowDialog(OldMap_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Document_Look")
        {
            theDM.ShowDialog(Document_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Safe_Look")
        {
            theDM.ShowDialog(Safe_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Safe_Fail")
        {
            theDM.ShowDialog(Safe_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Bracelet_Look")
        {
            theDM.ShowDialog(Bracelet_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Coat_Look")
        {
            theDM.ShowDialog(Item_CoatLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "SewergeKey_Look")
        {
            theDM.ShowDialog(SewergeKey_Look.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        if (setDialog == "Map_Look")
        {
            theDM.ShowDialog(Item_MapLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }


}
    // 1st

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

        else if (setDialog == "Fab_Witness")
        {
            theDM.ShowDialog(Fab_Witness.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_OwnerofUnifrom")
        {
            theDM.ShowDialog(Fab_OwnerofUnifrom.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_CauseofDeath")
        {
            theDM.ShowDialog(Fab_CauseofDeath.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_WebbedFeet")
        {
            theDM.ShowDialog(Fab_WebbedFeet.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Runway")
        {
            theDM.ShowDialog(Fab_Runway.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_FishySmellMissing")
        {
            theDM.ShowDialog(Fab_FishySmellMissing.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_TheClupet")
        {
            theDM.ShowDialog(Fab_TheClupet.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Both")
        {
            theDM.ShowDialog(Fab_Both.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_WarnOrRoot")
        {
            theDM.ShowDialog(Fab_WarnOrRoot.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_SwainSewer")
        {
            theDM.ShowDialog(Fab_SwainSewer.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        else if (setDialog == "Fab_2ndConnectStart")
        {
            theDM.ShowDialog(Fab_2ndConnectStart.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "SecondDream")
        {

            theDM.ShowDialog(SecondDream.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "ThirdNightmare")
        {

            theDM.ShowDialog(ThirdNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
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

                if(DataBaseManager.TimeCount < 6)
                {
                    theDM.ShowDialog(Swain_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Swain_NoContect_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
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
        if (setDialog == "Journalist_FirstDialog")
        {
            theDM.ShowDialog(Journalist_FirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
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




        if (setDialog == "Kane_FirstDialog")
        {
            if(DataBaseManager.Kane_FirstDialog == false && DataBaseManager.Intel_RiverWaterQuality2 == true)
            {
                DataBaseManager.Kane_FirstDialog = true;
                theDM.ShowDialog(Kane_DetailOB.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Kane_FirstDialog == false)
            {
                DataBaseManager.Kane_FirstDialog = true;
                theDM.ShowDialog(Kane_FirstMeatatSev.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Kane_AfterSevDilaog.transform.GetComponent<interactionEvent>().GetDialogs());
            }    

        }

        if (setDialog == "Kane_Look")
        {

            theDM.ShowDialog(Kane_AfterSevLook.transform.GetComponent<interactionEvent>().GetDialogs());
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
                if (DataBaseManager.TimeCount > 8 && DataBaseManager.ThirdDayAlanDialog == false)
                {
                    DataBaseManager.ThirdDayAlanDialog = true;
                    DataBaseManager.Alan_NoonFirstDialog = true;
                    theDM.ShowDialog(Alan_ThirdFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }

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

            if(DataBaseManager.AlbertDeathKeyword == true && DataBaseManager.TimeCount > 8 && DataBaseManager.ThirdDayPoliceADialog == true)
            {
                DataBaseManager.ThirdDayPoliceADialog = false;
                DataBaseManager.A_FirstDialog = true;
                theDM.ShowDialog(PoliceA_NoAlbert.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.A_FirstDialog == false)
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
            if (DataBaseManager.AlbertDeathKeyword == true && DataBaseManager.TimeCount > 8 && DataBaseManager.ThirdDayPoliceBDialog == true)
            {
                DataBaseManager.ThirdDayPoliceBDialog = false;
                DataBaseManager.B_FirstDialog = true;
                theDM.ShowDialog(PoliceB_NoAlbert.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.B_FirstDialog == false)
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
            if(DataBaseManager.TimeCount < 7)
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
            else if (DataBaseManager.TimeCount > 8)
            {
                if (DataBaseManager.ThirdDayAlbertDialog == false)
                {
                    DataBaseManager.ThirdDayAlbertDialog = true;
                    theDM.ShowDialog(Albert_ThirdFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Albert_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                
            }
            else
            {
                if (DataBaseManager.Albert_FirstDialog == false)
                {
                    Debug.Log("11111");
                    DataBaseManager.Albert_FirstDialog = true;
                    DataBaseManager.AlbertFirstAfterEvent = true;
                    theDM.ShowDialog(Albert_FirstDialogAfterEvent.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if (DataBaseManager.AlbertFirstAfterEvent == false)
                {
                    Debug.Log("22222222");
                    DataBaseManager.AlbertFirstAfterEvent = true;
                    theDM.ShowDialog(Albert_FirstDialogNew.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Albert_Dialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
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
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(Dave_Ran1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran2")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(Dave_Ran2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran3")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(Dave_Ran3.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran4")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(Dave_Ran4.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran5")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(Dave_Ran5.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran6")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(Dave_Ran6.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran7")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(Dave_Ran7.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran8")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(Dave_Ran8.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran9")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(Dave_Rand9.transform.GetComponent<interactionEvent>().GetDialogs());
        }


        if (setDialog == "Dave_NoMoney")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(Dave_NoMoney.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_RanEnd")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(Dave_RanEnd.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "AfterDaveDeath")
        {
            theDM.ShowDialog(AfterDaveDeath.transform.GetComponent<interactionEvent>().GetDialogs());
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
                    if(DataBaseManager.TimeCount == 12)
                    {
                        theDM.ShowDialog(Meave_ThirdDay_FirstO.transform.GetComponent<interactionEvent>().GetDialogs());
                    }
                    else
                    {
                        theDM.ShowDialog(Maeve_FirstDialog_JobO.transform.GetComponent<interactionEvent>().GetDialogs());
                    }

         
                }
                else
                {
                    if (DataBaseManager.TimeCount == 12)
                    {
                        theDM.ShowDialog(Meave_ThirdDay_FirstX.transform.GetComponent<interactionEvent>().GetDialogs());
                    }
                    else
                    {
                        theDM.ShowDialog(Maeve_FirstDialog_JobX.transform.GetComponent<interactionEvent>().GetDialogs());
                    }

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

        if (setDialog == "BarArrive")
        {
            if (DataBaseManager.Intel_FishySmell1 == true || DataBaseManager.Intel_FishySmell2 == true|| DataBaseManager.Intel_FishySmell3 == true|| DataBaseManager.Intel_FishySmell4 == true)
            {
                theDM.ShowDialog(Day2Bar_fishysmellO.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Day2Bar_fishysmellX.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Meave2ndDialog")
        {
            if (DataBaseManager.isBar == true)
            {
                theDM.ShowDialog(Day2Bar_1stBarIn.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Day2Bar_firstBar.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

    }


 
    public void Start_2nd_ClientHouse(string setDialog)
    {
        if (setDialog == "Ella_key_ContentsofaNewspaper")
        {

        }


    }



    public void KeywordDialog(string setDialog)
    {
        if (setDialog == "Ella Triss")
        {
            if (DataBaseManager.keyword_downer == "Subject to request" && DataBaseManager.Intel_SewerGhostStory1 == false)
            {
                theDM.ShowDialog(Ella_key_Aiden.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Rumor")
            {
                if (DataBaseManager.Intel_SewerGhostStory1 == true)
                {
                    theDM.ShowDialog(Ella_Key_FishySmell1_yesGhosty1.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Ella_Key_FishySmell1_noGhosty1.transform.GetComponent<interactionEvent>().GetDialogs());
                }

            }
            else if (DataBaseManager.keyword_downer == "Contents of a Newspaper")
            {
                theDM.ShowDialog(Ella_key_ContentsofaNewspaper.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Content of a Nightmare")
            {
                theDM.ShowDialog(Ella_key_ContentofaNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's Detail")
            {
                theDM.ShowDialog(Ella_key_AidensDetail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Water and Nightmare")
            {
                theDM.ShowDialog(Ella_key_WaterandNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Doctor's opinion")
            {
                theDM.ShowDialog(Ella_key_Doctorsopinion.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's Safe")
            {
                theDM.ShowDialog(Ella_key_AidensSafe.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Acquiring a Safe")
            {
                theDM.ShowDialog(Ella_key_AcquiringaSafe.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden & Pandemic")
            {

                theDM.ShowDialog(Ella_key_AidenandPandemic.transform.GetComponent<interactionEvent>().GetDialogs());
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


            else if (DataBaseManager.keyword_downer == "Albert's Information" || DataBaseManager.keyword_downer == "Reporting by Swain")
            {
                theDM.ShowDialog(Aiden_key_Missing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Contents of a GhostStory")
            {
                theDM.ShowDialog(Aiden_key_FishySmellOrigin.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Source speculation" || DataBaseManager.keyword_downer == "Sewer & FishySmell" || DataBaseManager.keyword_downer == "Cause of Fishy smell")
            {
                theDM.ShowDialog(Aiden_key_ContentsofaGhostStory.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Owner of Eye?")
            {
                theDM.ShowDialog(Aiden_key_OwnerofEye.transform.GetComponent<interactionEvent>().GetDialogs());
            }



            else if (DataBaseManager.keyword_downer == "Aiden's Safe")
            {
                if (DataBaseManager.getSafe == false)
                    theDM.ShowDialog(Aiden_key_Safe1.transform.GetComponent<interactionEvent>().GetDialogs());
                else
                {
                    theDM.ShowDialog(Aiden_Judge_Observation_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                if (DataBaseManager.AidenKeywordCount == 0)
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
                if (DataBaseManager.Intel_MissingPeople1 == false || DataBaseManager.Swain_isTalkMissingpeople == false)
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
                if (DataBaseManager.Intel_MissingPeople2 == false && DataBaseManager.Swain_isTalkMissingpeople == false)
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



            else if (DataBaseManager.keyword_downer == "Content of a Nightmare")
            {
                theDM.ShowDialog(Swain_key_ContentofaNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Water and Nightmare")
            {
                theDM.ShowDialog(Swain_key_WaterandNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Source speculation" || DataBaseManager.keyword_downer == "Sewer & FishySmell" || DataBaseManager.keyword_downer == "Cause of Fishy smell")
            {
                if (DataBaseManager.Intel_MissingPeople2 == true)
                {
                    theDM.ShowDialog(Swain_key_FishySmell_Reort.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Swain_key_FishySmell_None.transform.GetComponent<interactionEvent>().GetDialogs());
                }


            }
            else if (DataBaseManager.keyword_downer == "Details")
            {
                theDM.ShowDialog(Swain_key_Details.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden & Pandemic")
            {
                theDM.ShowDialog(Swain_key_AidenandPandemic.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Unfair dismissal")
            {
                theDM.ShowDialog(Swain_key_Unfairdismissal.transform.GetComponent<interactionEvent>().GetDialogs());
            }


            else if (DataBaseManager.keyword_downer == "Kidnap")
            {
                theDM.ShowDialog(Swain_Kidnap.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Fishy Slime")
            {
                theDM.ShowDialog(Swain_fishySlime.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Manhole" || DataBaseManager.keyword_downer == "Aiden's warning")
            {
                DataBaseManager.SwainDeathKeyword = true;
                theDM.ShowDialog(Swain_ManholeOrWarn.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Murdered Clients")
            {
                theDM.ShowDialog(Swain_MurderedElla.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Missing Subject")
            {
                if (DataBaseManager.Intel_MissingPeople1 == true || DataBaseManager.Intel_MissingPeople2 == true)
                {
                    theDM.ShowDialog(Swain_MissingAiden_MissingPeopleOk.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Swain_MissingAiden_Nomal.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }

            else if (DataBaseManager.keyword_downer == "Witness Testimony")
            {
                DataBaseManager.SwainDeathKeyword = true;
                theDM.ShowDialog(Swain_witness.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "The Culprit")
            {
                DataBaseManager.SwainDeathKeyword = true;
                theDM.ShowDialog(Swain_culprit.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Missing People & Fishy Smell")
            {
                theDM.ShowDialog(Swain_MissingandFishy.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Cause of Death")
            {
                theDM.ShowDialog(Swain_EllaDeathCause.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Webbed Feet")
            {
                theDM.ShowDialog(Swain_WebbedFeet.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Way to Escape")
            {
                DataBaseManager.SwainDeathKeyword = true;
                theDM.ShowDialog(Swain_RunawayRoute.transform.GetComponent<interactionEvent>().GetDialogs());
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
            else if (DataBaseManager.keyword_downer == "Content of a Nightmare")
            {
                theDM.ShowDialog(Student_key_ContentofaNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Water and Nightmare")
            {
                theDM.ShowDialog(Student_key_WaterandNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Rumor")
            {
                theDM.ShowDialog(Student_key_Rumor.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Source speculation" || DataBaseManager.keyword_downer == "Sewer & FishySmell" || DataBaseManager.keyword_downer == "Cause of Fishy smell")
            {
                theDM.ShowDialog(Student_key_FishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Details")
            {
                theDM.ShowDialog(Student_key_Details.transform.GetComponent<interactionEvent>().GetDialogs());
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
                if (DataBaseManager.Intel_FishySmell1 == true)
                {
                    theDM.ShowDialog(Kane_key_waterQuality1_yesFishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Kane_key_waterQuality1_noFishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }

            else if (DataBaseManager.keyword_downer == "Fishy Slime")
            {
                if (DataBaseManager.Intel_CreepyEyes1 == true)
                {
                    theDM.ShowDialog(Kane_FishySlime_CreepyEye.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Kane_FishySlime_Nomal.transform.GetComponent<interactionEvent>().GetDialogs());
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

            //2일차 추가
            else if (DataBaseManager.keyword_downer == "Water and Nightmare")
            {
                theDM.ShowDialog(Albert_key_WaterandNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Source speculation" || DataBaseManager.keyword_downer == "Sewer & FishySmell" || DataBaseManager.keyword_downer == "Cause of Fishy smell")
            {
                theDM.ShowDialog(Albert_key_FishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
            }

            else if (DataBaseManager.keyword_downer == "Details")
            {
                if (DataBaseManager.Intel_FishySmell3 == true || DataBaseManager.Intel_FishySmell4 == true || DataBaseManager.Intel_SewerWorker3 == true)
                {
                    theDM.ShowDialog(Albert_key_Details_Origin.transform.GetComponent<interactionEvent>().GetDialogs());


                }
                else
                {
                    theDM.ShowDialog(Albert_key_Details_noOrigin.transform.GetComponent<interactionEvent>().GetDialogs());

                }
            }

            else if (DataBaseManager.keyword_downer == "Promise")
            {
                theDM.ShowDialog(Albert_key_Promise.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Unfair dismissal")
            {
                theDM.ShowDialog(Albert_key_Unfairdismissal.transform.GetComponent<interactionEvent>().GetDialogs());
            }

            // 낮 이벤트 이후 추가
            else if (DataBaseManager.keyword_downer == "Root of problem")
            {
                DataBaseManager.AlbertDeathKeyword = true;
                theDM.ShowDialog(Albert_Key_Rootofproblem.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Murdered Clients")
            {
                theDM.ShowDialog(Albert_Key_DeadElla.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Missing Subject")
            {
                theDM.ShowDialog(Albert_Key_MissingAiden.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Witness Testimony")
            {
                theDM.ShowDialog(Albert_Key_Witness.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "The Culprit")
            {
                DataBaseManager.AlbertDeathKeyword = true;
                theDM.ShowDialog(Albert_Key_Culprit.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Cause of Death")
            {
                theDM.ShowDialog(Albert_Key_CauseOfDeath.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Webbed Feet")
            {
                if(DataBaseManager.TimeCount > 8)
                {
                    theDM.ShowDialog(Albert_KeyChange_WebbedFeet.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Albert_Key_WebbedFeet.transform.GetComponent<interactionEvent>().GetDialogs());
                }

            }
            else if (DataBaseManager.keyword_downer == "Way to Escape")
            {
                DataBaseManager.AlbertDeathKeyword = true;
                theDM.ShowDialog(Albert_Key_RunwayMenhole.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's warning")
            {
                DataBaseManager.AlbertDeathKeyword = true;
                theDM.ShowDialog(Albert_Key_WarnOfAiden.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Owner of Uniform")
            {
                DataBaseManager.AlbertDeathKeyword = true;
                theDM.ShowDialog(Albert_Key_OwnerOfOutfit.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Missing People & Fishy Smell" && DataBaseManager.TimeCount > 8)
            {
                theDM.ShowDialog(Albert_KeyChange_MissingFishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
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

            else if (DataBaseManager.keyword_downer == "Contents of a GhostStory")
            {
                theDM.ShowDialog(Kate_key_ContentsofaGhostStory.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Saw in the sewer")
            {
                theDM.ShowDialog(Kate_key_Sawinthesewer.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Subject to request")
            {
                theDM.ShowDialog(Kate_key_Subjecttorequest.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Murdered Clients" || DataBaseManager.keyword_downer == "Cause of Death" || DataBaseManager.keyword_downer == "" || DataBaseManager.keyword_downer == "Webbed Feet")
            {
                theDM.ShowDialog(Kate_Key_EllaBody.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Witness Testimony")
            {
                theDM.ShowDialog(Kate_Key_Witness.transform.GetComponent<interactionEvent>().GetDialogs());
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


            else if (DataBaseManager.keyword_downer == "Details")
            {
                theDM.ShowDialog(Alan_key_Details.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "State of congestion")
            {
                theDM.ShowDialog(Alan_key_Stateofcongestion.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Murdered Clients" || DataBaseManager.keyword_downer == "Cause of Death" || DataBaseManager.keyword_downer == "" || DataBaseManager.keyword_downer == "Webbed Feet")
            {
                theDM.ShowDialog(Alan_Key_EllaBody.transform.GetComponent<interactionEvent>().GetDialogs());
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
            else if (DataBaseManager.keyword_downer == "Water and Nightmare")
            {
                theDM.ShowDialog(Meave_key_WaterandNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Contents of a GhostStory")
            {
                theDM.ShowDialog(Meave_key_ContentsofaGhostStory.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Truth of a rumor")
            {
                theDM.ShowDialog(Meave_key_Truthofarumor.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Details")
            {
                theDM.ShowDialog(Meave_key_Details.transform.GetComponent<interactionEvent>().GetDialogs());
            }

            else if (DataBaseManager.keyword_downer == "Root of problem")
            {
                theDM.ShowDialog(Meave_key_CauseOfProblem.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Murdered Clients" || DataBaseManager.keyword_downer == "Missing Subject")
            {
                theDM.ShowDialog(Meave_key_DEllaMAiden.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Witness Testimony")
            {
                theDM.ShowDialog(Meave_key_Witness.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Missing People & Fishy Smell")
            {
             if (DataBaseManager.keyword_downer == "Source speculation" || DataBaseManager.keyword_downer == "Sewer & FishySmell" || DataBaseManager.keyword_downer == "Cause of Fishy smell")
                {
                    theDM.ShowDialog(Meave_key_MissingFishyCauseO.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Meave_key_MissingFishyCauseX.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
  



            else
            {
                theDM.ShowDialog(Maeve_Key_Nothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Dusty Dave")
        {
            selectionUIManager.Instance.EndDialog();
            if (DataBaseManager.keyword_downer == "Witness Testimony")
            {
                theDM.ShowDialog(Dave_Key_Witness.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "")
            {
                theDM.ShowDialog(Dave_Key_SafeAquire.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Acquiring a Safe")
            {
                if (DataBaseManager.DaveSafeOpen == true)
                {
                    theDM.ShowDialog(Dave_Key_OpenSafe.transform.GetComponent<interactionEvent>().GetDialogs());

                }
                else if (DataBaseManager.DaveAfterSafe == false)
                {
                    DataBaseManager.DaveAfterSafe = true;
                    theDM.ShowDialog(Dave_Key_SafeAquire.transform.GetComponent<interactionEvent>().GetDialogs());
                }
       
                else
                {
                    theDM.ShowDialog(Dave_Key_SafeAfter.transform.GetComponent<interactionEvent>().GetDialogs());
                }

            }
            else if (DataBaseManager.keyword_downer == "Bracelet")
            {
                if(DataBaseManager.DaveAfterBreclet == false)
                {
                    DataBaseManager.DaveAfterBreclet = true;
                    theDM.ShowDialog(Dave_Key_Breclet.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(Dave_Key_BrecletAfter.transform.GetComponent<interactionEvent>().GetDialogs());
                }

            }
            else
            {
                theDM.ShowDialog(Dave_Key_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "BlackWell")
        {

            if(DataBaseManager.Black_RechSucc_CanTalck == true)
            {
                if (DataBaseManager.keyword_downer == "Source speculation" || DataBaseManager.keyword_downer == "Sewer & FishySmell" || DataBaseManager.keyword_downer == "Cause of Fishy smell")
                {
                    theDM.ShowDialog(BlackWell_Key_Fishysmell.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if(DataBaseManager.keyword_downer == "Root of problem" || DataBaseManager.keyword_downer == "Aiden's warning")
                {
                    theDM.ShowDialog(BlackWell_Key_WarnRoot.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if (DataBaseManager.keyword_downer == "Witness Testimony")
                {
                    theDM.ShowDialog(BlackWell_Key_Witness.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if (DataBaseManager.keyword_downer == "Way to Escape")
                {
                    theDM.ShowDialog(BlackWell_Key_runway.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(BlackWell_Key_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                theDM.ShowDialog(BlackWell_Key_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (setDialog == "Symbol")
        {

            if (DataBaseManager.keyword_downer == "Advent")
            {
                theDM.ShowDialog(BloodEnding.transform.GetComponent<interactionEvent>().GetDialogs());
            }

            else
            {

                theDM.ShowDialog(Sewer_SymbolKeyWorkFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
    }

    //전투
    public void BattleDialog(string setDialog)
    {
        if (setDialog == "start")
        {
            theDM.ShowDialog(Battle_StartDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if(setDialog == "End")
        {
            theDM.ShowDialog(Battle_EndBattle.transform.GetComponent<interactionEvent>().GetDialogs());
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


   public void RetrunDialogResult(string Sub_Dialog, string result_End, string SanCheck = "")
    {
        // 1일차 오전 탐정사무소 판정
        if(Sub_Dialog == "Newspaper : Read")
        {
            DataBaseManager.fst_Detectiv_TimeOn += 1;
            if (result_End == "Result : Success"|| result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(NewsPaper_Judege_True.transform.GetComponent<interactionEvent>().GetDialogs());
  
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(NewsPaper_Judege_False.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Stove : Making coffee")
        {
            DataBaseManager.fst_Detectiv_TimeOn += 1;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.Condition = "Good";
                theDM.ShowDialog(Stove_Judge_True.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Stove_Judge_False.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Aiden : Diagnosis")
        {
            DataBaseManager.Aiden_medicine = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {

               theDM.ShowDialog(Aiden_Judge_medicine_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Aiden_Judge_medicine_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Aiden : Look")
        {
            DataBaseManager.Aiden_Observation = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {

               theDM.ShowDialog(Aiden_Judge_Observation_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Aiden_Judge_Observation_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Aiden : Calming down")
        {
            DataBaseManager.Aiden_psychotherapy = true;

            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
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
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Aiden_Judge_medicine_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Aiden : Persuasion")
        {

            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.getSafe = true;
                theDM.ShowDialog(Aiden_key_Safe_rhetoric_sucsses.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Aiden_key_Safe_rhetoric_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Aiden : TakeAway")
        {

            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.getSafe = true;
                theDM.ShowDialog(Aiden_key_Safe_Str_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                BillowUIManager.Instance.HP_down(5);
                theDM.ShowDialog(Aiden_key_Safe_Str_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Swain : Look")
        {

            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {

                theDM.ShowDialog(Swain_LookJudge_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {

                theDM.ShowDialog(Swain_LookJudge_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Witness : Eavesdropping")
        {

            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                if(DataBaseManager.Intel_QuestHouse1 == false)
                {
                    theDM.ShowDialog(NoonEvent_Witness_ListeningSucc_NoRequest.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if(DataBaseManager.Intel_MissingPeople1 == true || DataBaseManager.Intel_MissingPeople2 == true)
                {
                    theDM.ShowDialog(NoonEvent_Witness_ListeningSucc_MissingPeople.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(NoonEvent_Witness_ListeningSucc_Nomal.transform.GetComponent<interactionEvent>().GetDialogs());
                }

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {

                theDM.ShowDialog(NoonEvent_Witness_ListeningFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Police : Persuasion")
        {

            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(NoonEvent_DoorPolice_PersuasionSucc.transform.GetComponent<interactionEvent>().GetDialogs());

                DataBaseManager.NoonEvent_AccessAuthorization = true;
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {

                theDM.ShowDialog(NoonEvent_DoorPolice_PersuasionFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (Sub_Dialog == "Maeve : Persuasion")
        {

            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(Meave_Judge_RhetoricSucc.transform.GetComponent<interactionEvent>().GetDialogs());

                DataBaseManager.NoonEvent_AccessAuthorization = true;
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {

                theDM.ShowDialog(Meave_Judge_RhetoricFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Door : Picking doors")
        {

            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(SewerDoorPickSucc.transform.GetComponent<interactionEvent>().GetDialogs());
                DataBaseManager.SewerDoorOpenDeftness = true;
                DataBaseManager.SewerDoorOpenCheck = true;
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {

                theDM.ShowDialog(SewerDoorPickFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Window : Break")
        {
            DataBaseManager.SewerDoorOpenDeftness = true;
            DataBaseManager.SewerDoorOpenCheck = true;
            DataBaseManager.isSewerOfficeBrokenWindow = true;
            DataBaseManager.TryBrokenWindow = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(Window_JudgeBreakSucc.transform.GetComponent<interactionEvent>().GetDialogs());
                DataBaseManager.MoveSewerOfficeWindow = true;

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Window_JudgeBreakFail.transform.GetComponent<interactionEvent>().GetDialogs());
                DataBaseManager.MoveSewerOfficeWindow = true;
            }
        }
        if (Sub_Dialog == "Locker : Take a Look")
        {
            DataBaseManager.TryObserLokcer = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(Loker_Judge_ObservationSucc.transform.GetComponent<interactionEvent>().GetDialogs());


            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Loker_Judge_ObservationFail.transform.GetComponent<interactionEvent>().GetDialogs());

            }
        }
        // 2일차 강제이벤트
        if (Sub_Dialog == "DeadBody : Diagnosis")
        {
            DataBaseManager.DeadBody_medicine = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(NoonEvent_DeadBody_medicineSucc.transform.GetComponent<interactionEvent>().GetDialogs());


            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {

                theDM.ShowDialog(NoonEvent_DeadBody_medicineFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "DeadBody : Look")
        {
            DataBaseManager.DeadBody_observational = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(NoonEvent_DeadBody_ObservationSucc.transform.GetComponent<interactionEvent>().GetDialogs());


            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {

                theDM.ShowDialog(NoonEvent_DeadBody_ObservationFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "DeadBody : Check")
        {
            DataBaseManager.DeadBody_Analyzing = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(NoonEvent_DeadBody_analysisSucc.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {

                theDM.ShowDialog(NoonEvent_DeadBody_analysisFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "DeadBody : Stealing")
        {
            DataBaseManager.DeadBody_deftness = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(NoonEvent_DeadBody_deftnessSucc.transform.GetComponent<interactionEvent>().GetDialogs());


            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {

                theDM.ShowDialog(NoonEvent_DeadBody_deftnessFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (Sub_Dialog == "Accident Site : Check")
        {
            DataBaseManager.Site_Analyzing = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(Site_analysisSucc.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {

                theDM.ShowDialog(Site_analysisFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        // 강가 Sewer : Look
        if (DataBaseManager.nowPlace == "Riverside")
        {
            if (Sub_Dialog == "Sewer : Look")
            {

                if (result_End == "Result : Success" || result_End == "Result : Critical Success")
                {

                    theDM.ShowDialog(Sewer_Observation_Sucsses.transform.GetComponent<interactionEvent>().GetDialogs());

                }
                else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
                {

                    theDM.ShowDialog(Sewer_Observation_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            if (Sub_Dialog == "SAN : Check")
            {

                if (result_End == "Result : Success" || result_End == "Result : Critical Success")
                {

                    theDM.ShowDialog(Sewer_Observation_SucssesAfterSanSuc.transform.GetComponent<interactionEvent>().GetDialogs());

                }
                else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
                {
                    //BillowUIManager.Instance.San_Down(10);
                    theDM.ShowDialog(Sewer_Observation_SucssesAfter.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
        }

        if (DataBaseManager.nowPlace == "BattleRoad")
        {
       
            if (Sub_Dialog == "SAN : Check")
            {

                if (result_End == "Result : Success" || result_End == "Result : Critical Success")
                {

                    theDM.ShowDialog(Battle_SanCheck.transform.GetComponent<interactionEvent>().GetDialogs());

                }
                else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
                {
                    //BillowUIManager.Instance.San_Down(10);
                    theDM.ShowDialog(Battle_SanCheckFail.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
        }
        if (DataBaseManager.nowPlace == "DetectiveOffice")
        {
            if(DataBaseManager.TimeCount == 5)
            {
                if (Sub_Dialog == "SAN : Check")
                {
                    theDM.ShowDialog(Fab_Case1After.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
        
        }

        if(SanCheck == "NoonEvent_FirstDoor")
        {
            theDM.ShowDialog(NoonEvent_FirstDoorAfterSan.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        

        //Item
        if (Sub_Dialog == "Safe : Open")
        {
            DataBaseManager.isUseSafe = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.getSafe = false;
                theDM.ShowDialog(Safe_Succ.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Safe_Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (Sub_Dialog == "Coat : Disguise")
        {
            DataBaseManager.JudgeCoat_TryDisguise = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.WearCoat = true;
                theDM.ShowDialog(Item_DiaguiseSucc.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Item_DiaguiseFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        // 이성판정

        if (Sub_Dialog == "Case2IntCheck")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {

                theDM.ShowDialog(Fab_Case2Succ.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {

                theDM.ShowDialog(Fab_Case2Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }

        }

        if (SanCheck == "KaneSan")
        {
            theDM.ShowDialog(Kane_FishySlime_CreepyEyeAfterSans.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "DrinkSan")
        {
            theDM.ShowDialog(Meave_Judge_RhetoricSanAfter.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "ThirdDayStartSan")
        {

            if (DataBaseManager.Intel_Nightmare3 == true || DataBaseManager.Intel_Nightmare4 == true)
            {
                theDM.ShowDialog(ThirdDayStartCase1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(ThirdDayStartCase2.transform.GetComponent<interactionEvent>().GetDialogs());
            }

        }
        if (SanCheck == "DayTwoCase2San")
        {
            theDM.ShowDialog(ThirdDayStartCase1AfterSan.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "JournalSan")
        {
            DataBaseManager.JournalFirstSanCheck = true;
            theDM.ShowDialog(Sewer_JornalLookSanCheck.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "SymbolStrSan")
        {
            theDM.ShowDialog(Sewer_SanChecck.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "Sewer_FirstInSan")
        {

            theDM.ShowDialog(Sewer_FirstInAfterSan.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        

        if (SanCheck == "FourthDayStartSan")
        {

            if (DataBaseManager.Intel_Nightmare3 == true || DataBaseManager.Intel_Nightmare4 == true)
            {
                theDM.ShowDialog(Fab_4th_QuestO.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(Fab_4thQuestX.transform.GetComponent<interactionEvent>().GetDialogs());
            }

        }
        if (SanCheck == "Fab_4th_QuestO_SanCheck")
        {
            theDM.ShowDialog(Fab_4th_QuestO_SanCheck.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        if (SanCheck == "Day_4_case1Health")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(Fab_4thQuestO_HealthCehckSucc.transform.GetComponent<interactionEvent>().GetDialogs());


            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                DataBaseManager.MorningMove_4th = true;
                DataBaseManager.MorningMove_3rd = true;
                DataBaseManager.MorningMove_2nd = true;
                theDM.ShowDialog(Fab_4th_QuestO_HealthCehckFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (SanCheck == "BookSan")
        {
            theDM.ShowDialog(Sewer_BookLookSanCheck.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "DeepOneFirstSan")
        {
            theDM.ShowDialog(Sewer_FirstDeepOneAfterSan.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        if (SanCheck == "EndingLedderSan")
        {
            if(DataBaseManager.Ending_LadderSanInt == 0)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(Sewer_LadderSan1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 1)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(Sewer_LadderSan2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 2)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(Sewer_LadderSan3.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 3)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(Sewer_LadderSan4.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 4)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(Sewer_LadderSan5.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 5)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(Sewer_LadderSan6.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 6)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(Sewer_LadderSan7.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 7)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(Sewer_LadderSan8.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 8)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(Sewer_LadderSan9.transform.GetComponent<interactionEvent>().GetDialogs());
            }   
        }

        


        if (Sub_Dialog == "Warden : Steal")
        {
            DataBaseManager.Black_Deftness = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(BlackWell_Judege_DeftnessSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(BlackWell_Judege_DeftnessFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Warden : Persuade")
        {
            DataBaseManager.Black_Rhethic = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.Black_RechSucc_CanTalck = true;
                theDM.ShowDialog(BlackWell_Judege_RheticSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(BlackWell_Judege_RheticFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        // 하수도 내부 
        if (Sub_Dialog == "Fabian : Sneak Out")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(Sewer_StealthSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Sewer_StealthFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "??? : Give Excuse")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(Sewer_RhethicSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Sewer_RhethicFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (Sub_Dialog == "Book : Decryption")
        {

            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(Sewer_BookLookOccultSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Sewer_BookLookOccultFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Sewer : Picking")
        {

            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.SewerPicingSucc = true;
                theDM.ShowDialog(RiverDeftnessSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(RiverDeftnessFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }


        if (Sub_Dialog == "Symbol : Decryption")
        {
            DataBaseManager.SymbolOccultFirst = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(Sewer_SymbolLook_OccultSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Sewer_SymbolLook_OccultFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Symbol : Break")
        {
            DataBaseManager.StoryDirecting = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(Sewer_JudgeStrSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(Sewer_JudgeStrFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Fabian : Swimming")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(Sewer_LadderSwin.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                 BillowUIManager.Instance.HP_down(5);
                Invoke("SwinCheckAgain", 0.5f);
            }
        }


   

    }


    void SwinCheckAgain()
    {
        Rollet.Instance.setRollet("Fabian : Swimming", "Swin", DataBaseManager.swimingPoint, "dialog");
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

