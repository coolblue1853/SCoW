using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public DialogManager theDM;

    //Item_Box
    public GameObject safeLook;
    public GameObject safeFail;
    public GameObject safeSucc;
    public GameObject braceletLook;
    public GameObject oldMapLook;
    public GameObject documentLook;
    //의뢰자의 집
    public GameObject start1stDetective;
    public GameObject newsPaper;
    public GameObject newsPaperLook;
    public GameObject newsPaperJudegeTrue;
    public GameObject newsPaperJudegeFalse;
    public GameObject desk;
    public GameObject deskLook;
    public GameObject stove;
    public GameObject stoveLook;
    public GameObject stoveJudgeTrue;
    public GameObject stoveJudgeFalse;
    public GameObject directingNock;
    public GameObject directingDoor;
    public GameObject directingAfterDoor;
    public GameObject selectAboutCrime;
    public GameObject selectAboutHospital;
    public GameObject selectAboutRunAway;
    public GameObject acceptRequest;
    public GameObject acceptAfterSelectYes;
    public GameObject rejectRequest;
    public GameObject acceptAfterSelectNo;
    //밤
    public GameObject fabFirstDialog;
    public GameObject fabAiden1;
    public GameObject fabSafe1;
    public GameObject fabNightmare1;
    public GameObject fabNightmare2;
    public GameObject fabNightmare3;
    public GameObject fabNightmare4;
    public GameObject fabInsomnia1;
    public GameObject fabInsomnia2;
    public GameObject fabPlanetarySequence1;
    public GameObject fabUniv1;
    public GameObject fabMeiv2;
    public GameObject fabMissingPeople;
    public GameObject fabGhostStory1;
    public GameObject fabGhostStory2;
    public GameObject fabCreepyEye;
    public GameObject fabWorker1;
    public GameObject fabFishySmell1;
    public GameObject fabFishySmell2;
    public GameObject fabFishySmell3;
    public GameObject fabEndDay;
    public GameObject fabConnect;
    // 1st Clinet'sOffice
    public GameObject firstArrive;
    public GameObject nockDoor;
    public GameObject ellaFirstDialog;
    public GameObject ellaDialog1;
    public GameObject ellaDialog2;
    public GameObject ellaKeyAiden;
    public GameObject ellaKeyDisable;
    public GameObject ellaFirstUpstair;
    public GameObject intoFirstRoom;
    public GameObject aidenActive;
    public GameObject aidenLook;
    public GameObject aidenLookJudge;
    public GameObject aidenFirstDialog;
    public GameObject aidenDialog1;
    public GameObject aidenDialog2;
    public GameObject aidenKeyNightmare1;
    public GameObject aidenkeyEllaTriss1;
    public GameObject aidenKeySafe1;
    public GameObject aidenKeySafeStr;
    public GameObject aidenKeySafeStrSucc;
    public GameObject aidenKeySafeStrFail;
    public GameObject aidenkeySafeRhetoricSucc;
    public GameObject aidenKeySafeRhetoricFail;
    public GameObject aidenKeySafeNothing;
    public GameObject aidenKeyFail1;
    public GameObject aidenKeyFail2;
    public GameObject aidenJudgeMedicineSucc;
    public GameObject aidenJudgeMedicineFail;
    public GameObject aidenJudgeObservationSucc;
    public GameObject aidenJudgeObservationFail;
    public GameObject aidenJudgePsychotherapySucc;
    public GameObject aidenJudgePsychotherapySuccSafe;
    public GameObject aidenJudgePsychotherapySuccNoSafe;
    public GameObject aidenJudgePsychotherapySuccEnd;
    public GameObject aidenJudgePsychotherapyFail;
    public GameObject aidenKeyFailForce;
    public GameObject ellaEndNoSafe;
    public GameObject ellaEndGetSafe;
    //오후 추가
    public GameObject aidenKeyCreepyEye1;
    public GameObject ellaKeyFishySmell1NoGhosty1;
    public GameObject ellaKeyFishySmell1YesGhosty1;

    // 1st DailyNews
    public GameObject swainFirstDialog;
    public GameObject swainDialog;
    public GameObject swainLook;
    public GameObject swainLookJudgeSucsses;
    public GameObject swainLookJudgeFail;
    public GameObject swainKeyPlanetarySequence1;
    public GameObject swainKeyInsomnia1;
    public GameObject swainKeyNightmare2;
    public GameObject swainKeyParanoia1;
    public GameObject swainKeyEllaTriss1;
    public GameObject swainKeyAidenTriss1;
    public GameObject swainKeyDailyNews1;
    public GameObject swainKeyNothing;
    //오후추가
    public GameObject swainKeyMissingPeople1;
    public GameObject swainKeyFishySmell2;
    public GameObject swainKeyGhostStory1;
    public GameObject swainKeyCreepyEye1;

    // 1st University
    public GameObject univFirstDialog;
    public GameObject univDialog;
    public GameObject univLook;
    public GameObject univKeyPlanetarySequence1;
    public GameObject univKeyInsomnia1;
    public GameObject univKeyNothing;

    // 1st Riverside
    public GameObject kaneFirstDialog;
    public GameObject kaneDialog;
    public GameObject kaneLook;
    public GameObject kaneNothing;
    public GameObject kaneKeyWaterQuality1NoFishySmell;
    public GameObject kaneKeyWaterQuality1YesFishySmell;
    public GameObject sewerFirstDialog;
    public GameObject sewerDialog;
    public GameObject sewerLook;
    public GameObject sewerObservationSucc;
    public GameObject sewerObservationSuccAfter;
    public GameObject sewerObservationFail;
    public GameObject sewerObservationSuccAfterSanSuc;

    // 1st Hospital
    public GameObject hospitalNoonFirst;
    public GameObject kateFirstDialog;
    public GameObject alanFirstDialog;
    public GameObject alanDialog;
    //오후
    public GameObject kateNoonFirstDialog;
    public GameObject kateNoonDialog;
    public GameObject kateNoonLook;
    public GameObject kateNoonInsomnia1;
    public GameObject kateNoonNightmare1;
    public GameObject kateNoonParanoia1;
    public GameObject kateNoonHospital1;
    public GameObject kateNoonFishySmell1;
    public GameObject kateNoonNothing;
    public GameObject alanNoonFirstDialog;
    public GameObject alanNoonDialog;
    public GameObject alanNoonLook;
    public GameObject alanNoonNightmare1;
    public GameObject alanNoonNightmare2;
    public GameObject alanNoonNightmare3;
    public GameObject alanNoonInsomnia1;
    public GameObject alanNoonParanoia1;
    public GameObject alanNoonAiden1;
    public GameObject alanNoonNothing;
    // 1st PoliceOffice
    public GameObject aFirstDialog;
    public GameObject aDialog;
    public GameObject bFirstDialog;
    public GameObject bDialog;
    public GameObject albertFirstDialog;
    public GameObject albertDialog;
    public GameObject albertKeyPlanetarySequence1;
    public GameObject albertKeyInsomnia1;
    public GameObject albertKeyMeiv1;
    public GameObject albertKeyNothing;
    public GameObject aLook;
    public GameObject bLook;
    public GameObject albertLook;

    //오후
    public GameObject albertKeySewerGhostStory1;
    public GameObject albertKeyCreepyEye1;
    public GameObject albertKeyFishySmell1;
    public GameObject albertKeyMissingPeople2;

    // 1st Slum
    public GameObject daveFirstDialog;
    public GameObject daveDialog;
    public GameObject daveRand1;
    public GameObject daveRand2;
    public GameObject daveRand3;
    public GameObject daveRand4;
    public GameObject daveRand5;
    public GameObject daveRand6;
    public GameObject daveRand7;
    public GameObject daveRand8;
    public GameObject daveNoMoney;
    public GameObject daveRandEnd;


    // 1st Slum
    public GameObject maeveFirstDialogJobO;
    public GameObject maeveFirstDialogJobX;
    public GameObject maeveDialog;
    public GameObject maeveLook;
    public GameObject maeveKeyIntelPlanetarySequence1;
    public GameObject maeveKeyInsomnia1;
    public GameObject maeveKeyAlbert1;
    public GameObject maeveKeyMissingPeople;
    public GameObject maeveKeyCreepyEye;
    public GameObject maeveKeyFishySmell1;
    public GameObject maeveKeyFishySmell2;
    public GameObject maeveKeyAiden1;
    public GameObject maeveKeyNothing;
    public GameObject workerFirstDialog;
    public GameObject workerDialog;
    public GameObject workerLookO;
    public GameObject workerLookX;


    // Update is called once per frame

    // 전투
    public GameObject battleStartDialog;
    public GameObject battleSanCheck;
    public GameObject battleSanCheckFail;
    public GameObject battleEndBattle;
    //public GameObject Sewer_Observation_SucssesAfterSanSuc;

    //2st 탐정사무소
    public GameObject fabCase1;
    public GameObject fabCase1After;
    public GameObject fabCase2;
    public GameObject fabCase2Succ;
    public GameObject fabCase2SuccAfter;
    public GameObject fabCase2Fail;
    public GameObject fabCase3;

    //2st 의뢰자
    public GameObject ellaKeyContentNewspaper;
    public GameObject ellaKeyContentNightmare;
    public GameObject ellaKeyAidensDetail;
    public GameObject ellaKeyWaterNightmare;
    public GameObject ellaKeyDoctorsOpinion;
    public GameObject ellaKeyAidenSafe;
    public GameObject ellaKeyAcquiringaSafe;
    public GameObject ellaKeyAidenPandemic;
    public GameObject aidenKeyMissing;
    public GameObject aidenKeyFishySmellOrigin;
    public GameObject aidenKeyContentGhostStory;
    public GameObject aidenKeyOwnerOfEye;

    //2st 신문사
    public GameObject swainKeyContentNightmare;
    public GameObject swainKeyWaterNightmare;
    public GameObject swainKeyFishySmellReport;
    public GameObject swainKeyFishySmellNone;
    public GameObject swainKeyDetails;
    public GameObject swainKeyAidenPandemic;
    public GameObject swainKeyUnfairDismissal;

    //2st 대학교
    public GameObject studentKeyContentNightmare;
    public GameObject studentKeyWaterNightmare;
    public GameObject studentKeyRumor;
    public GameObject studentKeyFishySmell;
    public GameObject studentKeyDetails;


    //2st 경찰서
    public GameObject albertKeyWaterNightmare;
    public GameObject albertKeyFishySmell;
    public GameObject albertKeyDetailsNoOrigin;
    public GameObject albertKeyDetailsOrigin;
    public GameObject albertKeyPromise;
    public GameObject albertKeyUnfairDismissal;

    //2st 병원
    public GameObject kateKeyContentGhostStory;
    public GameObject kateKeySawinSewer;
    public GameObject kateKeySubjectRequest;
    public GameObject alanKeyDetails;
    public GameObject alanKeyStateCongestion;

    //2st 술집
    public GameObject meaveKeyWaterNightmare;
    public GameObject meaveKeyContentGhostStory;
    public GameObject meaveKeyTruthRumor;
    public GameObject meaveKeyDetails;

    //2st 강제 이벤트
    public GameObject noonEventFirstDailogNoMissing;
    public GameObject noonEventFirstDailogMissingPeople;
    public GameObject noonEventFirstDailogNoquest;
    public GameObject noonEventClientHouse;
    public GameObject noonEventWitnessFirstDialog;
    public GameObject noonEventWitnessRepeatDialog;
    public GameObject noonEventWitnessLook;
    public GameObject noonEventWitnessListeningFail;
    public GameObject noonEventWitnessListeningSuccNomal;
    public GameObject noonEventWitnessListeningSuccMissingPeople;
    public GameObject noonEventWitnessListeningSuccNoRequest;
    public GameObject noonEventDoorPoliceFirstDialog;
    public GameObject noonEventDoorPoliceRepeatDialog;
    public GameObject noonEventDoorPoliceLook;
    public GameObject noonEventDoorPolicePersuasionSucc;
    public GameObject noonEventDoorPolicePersuasionFail;
    public GameObject noonEventFirstDoor;
    public GameObject noonEventFirstDoorAfterSan;
    public GameObject noonEventInsidePoliceFirstDialog;
    public GameObject noonEventInsidePoliceRepeatDialog;
    public GameObject noonEventInsidePoliceLook;
    public GameObject noonEventDeadBodyDialog;
    public GameObject noonEventDeadBodyLookNoRumor;
    public GameObject noonEventDeadBodyLookYesRumor;
    public GameObject noonEventDeadBodyLookMissing;
    public GameObject noonEventDeadBodyLookFishySmell;
    public GameObject noonEventDeadBodyMedicineSucc;
    public GameObject noonEventDeadBodyMedicineFail;
    public GameObject noonEventDeadBodyObservationSucc;
    public GameObject noonEventDeadBodyObservationFail;
    public GameObject noonEventDeadBodyAnalysisSucc;
    public GameObject noonEventDeadBodyAnalysisSuccAfter;
    public GameObject noonEventDeadBodyAnalysisFail;
    public GameObject noonEventDeadBodyDeftnessSucc;
    public GameObject noonEventDeadBodyDeftnessFail;

    // 강제 이번트 이후 추가 키워드 
    // 신문사
    public GameObject swainNoContectFirstDialog;
    public GameObject swainKidnap;
    public GameObject swainFishySlime;
    public GameObject swainManholeWarn;
    public GameObject swainMurderedElla;
    public GameObject swainMissingAidenNomal;
    public GameObject swainMissingAidenMissingPeopleOk;
    public GameObject swainWitness;
    public GameObject swainCulprit;
    public GameObject swainMissingFishy;
    public GameObject swainEllaDeathCause;
    public GameObject swainWebbedFeet;
    public GameObject swainRunawayRoute;

    // 의뢰자의 집
    public GameObject siteDialog;
    public GameObject siteLook;
    public GameObject siteAnalysisSucc;
    public GameObject siteAnalysisSuccAfter;
    public GameObject SiteAnalysisFail;

    //대학교
    public GameObject kaneFirstMeatSev;
    public GameObject kaneDetailOb;
    public GameObject kaneAfterSevDialog;
    public GameObject kaneAfterSevLook;
    public GameObject kaneFishySlimeCreepyEye;
    public GameObject kaneFishySlimeCreepyEyeAfterSans;
    public GameObject kaneFishySlimeNomal;

    //빈민가
    public GameObject daveRand9;
    public GameObject daveKeySafeAquire;
    public GameObject daveKeyBreclet;
    public GameObject daveKeyWitness;
    public GameObject daveKeyFail;
    public GameObject daveKeyBrecletAfter;
    public GameObject daveKeySafeAfter;
    public GameObject daveKeyOpenSafe;

    //병원
    public GameObject kateKeyEllaBody;
    public GameObject kateKeyWitness;
    public GameObject alanKeyEllaBody;

    // 경찰서
    public GameObject albertFirstDialogAfterEvent;
    public GameObject albertFirstDialogNew;
    public GameObject albertKeyWarnAiden;
    public GameObject albertKeyRootProblem;
    public GameObject albertKeyDeadElla;
    public GameObject albertKeyMissingAiden;
    public GameObject albertKeyWitness;
    public GameObject albertKeyCulprit;
    public GameObject albertKeyCauseOfDeath;
    public GameObject albertKeyWebbedFeet;
    public GameObject albertKeyRunwayMenhole;


    // 술집
    public GameObject day2BarFishysmellO;
    public GameObject day2BarFishysmellX;
    public GameObject day2Bar1stBarIn;
    public GameObject day2BarFirstBar;
    public GameObject meaveKeyCauseProblem;
    public GameObject meaveKey_EllaAiden;
    public GameObject meaveKeyWitness;
    public GameObject meaveKeyMissingFishyCauseX;
    public GameObject meaveKeyMissingFishyCauseO;
    public GameObject meaveJudgeRhetoricSucc;
    public GameObject meaveJudgeRhetoricSanAfter;
    public GameObject meaveJudgeRhetoricFail;

    //탐정사무소 추가 정리
    public GameObject fab2ndConnectStart;
    public GameObject fabWitness;
    public GameObject fabOwnerUnifrom;
    public GameObject fabCauseDeath;
    public GameObject fabWebbedFeet;
    public GameObject fabRunway;
    public GameObject fabFishySmellMissing;
    public GameObject fabTheClupet;
    public GameObject fabBoth;
    public GameObject fabWarnOrRoot;
    public GameObject fabSwainSewer;
    public GameObject secondDream;
    public GameObject thirdDayStartCase1;
    public GameObject thirdDayStartCase1AfterSan;
    public GameObject thirdDayStartCase2;


    // 3일차 추가
    // 경찰서
    public GameObject albertThirdFirstDialog;
    public GameObject albertKeyOwnerOfOutfit;
    public GameObject albertKeyChangeMissingFishySmell;
    public GameObject albertKeyChangeWebbedFeet;
    public GameObject policeANoAlbert;
    public GameObject policeBNoAlbert;

    // 병원
    public GameObject alanThirdFirstDialog;
    public GameObject alanKeyMurderdElla;
    public GameObject alanKeyCauseOfDeath;
    public GameObject alanKeyWebbedFeet;

    //술집
    public GameObject meaveThirdDayFirstO;
    public GameObject meaveThirdDayFirstX;


    //밤 악몽 연출
    public GameObject thirdNightmare;
    public GameObject fab4thQuestO;
    public GameObject fab4thQuestOSanCheck;
    public GameObject fab4thQuestOHealthCheckSucc;
    public GameObject fab4thQuestOHealthCheckFail;
    public GameObject fab4thQuestX;
    public GameObject fab4thQuestXHealthCheckSucc;
    public GameObject fab4thQuestXHealthCheckFail;


    //하수정비사무소
    public GameObject sewerDoorDialog;
    public GameObject sewerDoorLook;
    public GameObject sewerDoorPickSucc;
    public GameObject sewerDoorPickFail;
    public GameObject windowDialogBFNoon;
    public GameObject windowLookBFNoon;
    public GameObject windowDialogAFNoon;
    public GameObject windowLookAFNoon;
    public GameObject windowJudgeBreakSucc;
    public GameObject windowJudgeBreakFail;
    public GameObject lockerDialogBFNoon;
    public GameObject lockerLookBFNoon;
    public GameObject lockerDialogAFNoon;
    public GameObject lockerLookAFNoon;
    public GameObject lockerJudgeObservationSucc;
    public GameObject lockerJudgeObservationFail;
    public GameObject mapDialogBFNoon;
    public GameObject mapLookBFNoon;
    public GameObject mapDialogAFNoon;
    public GameObject mapLookAFNoon;
    public GameObject itemCoatLook;
    public GameObject itemDiaguiseSucc;
    public GameObject itemDiaguiseFail;
    public GameObject itemMapLook;
    public GameObject blackWellFirstDialog;
    public GameObject blackWellDialog;
    public GameObject blackWellDialogLook;
    public GameObject blackWellJudegeRheticSucc;
    public GameObject blackWellJudegeRheticFail;
    public GameObject blackWellJudegeDeftnessSucc;
    public GameObject blackWellJudegeDeftnessFail;
    public GameObject blackWellKeyWarnRoot;
    public GameObject blackWellKeyWitness;
    public GameObject blackWellKeyRunway;
    public GameObject blackWellKeyFishysmell;
    public GameObject blackWellKeyFail;
    public GameObject afterJudgeLookLocker;
    public GameObject sewergeKeyLook;

    //하수도 내부
    public GameObject sewerEnemySpawn;
    public GameObject sewerStealthSucc;
    public GameObject sewerStealthFail;
    public GameObject sewerDisgauseFail;
    public GameObject sewerRhethicSucc;
    public GameObject sewerRhethicFail;
    public GameObject sewerMapDialog;
    public GameObject sewerMapLook;
    public GameObject sewerJournalDialog;
    public GameObject sewerJornalLook;
    public GameObject sewerJornalLookSanCheck;
    public GameObject sewerJornalLookAfterSanCheck;
    public GameObject sewerBookDialog;
    public GameObject sewerBookLook;
    public GameObject sewerBookLookSanCheck;
    public GameObject sewerBookLookOccultSucc;
    public GameObject sewerBookLookOccultFail;
    public GameObject sewerBookLookAfterCheck;
    public GameObject sewerSymbolDialog;
    public GameObject sewerSymbolLookYseJournal;
    public GameObject sewerSymbolLookNoJournal;
    public GameObject sewerSymbolLookOccultSucc;
    public GameObject sewerSymbolLookOccultFail;
    public GameObject sewerJudgeStrSucc;
    public GameObject sewerJudgeStrFail;
    public GameObject sewerSanChecck;
    public GameObject sewerSymbolLookNoJournalAfterOccult;
    public GameObject sewerSymbolKeyWorkSucc;
    public GameObject sewerSymbolKeyWorkFail;
    public GameObject afterDaveDeath;
    public GameObject riverDeftnessSucc;
    public GameObject riverDeftnessFail;
    public GameObject journalistFirstDialog;

    //하수도 내부
    public GameObject sewerFirstIn;
    public GameObject sewerFirstInAfterSan;
    public GameObject sewerFirstDeepOne;
    public GameObject sewerFirstDeepOneAfterSan;
    public GameObject sewerLadderDialog;
    public GameObject sewerLadderUP;
    public GameObject sewerLadderSwim;
    public GameObject sewerLadderSan1;
    public GameObject sewerLadderSan2;
    public GameObject sewerLadderSan3;
    public GameObject sewerLadderSan4;
    public GameObject sewerLadderSan5;
    public GameObject sewerLadderSan6;
    public GameObject sewerLadderSan7;
    public GameObject sewerLadderSan8;
    public GameObject sewerLadderSan9;
    public GameObject bloodEnding;
    public GameObject endingBreakSymbol;
    public GameObject endingIfGotPromiseAndAblertAlive;
    public GameObject endingIfGotPromiseAndAblertDeath;
    public GameObject endingIfNoPromise;
    public GameObject endingIfNoPromiseAlbertDeath;
    public GameObject endingIfSwainDeath;
    public GameObject endingLastIfGotRequest;
    public GameObject endingLastIfNoRequest;
    public GameObject endingKilling;
    public GameObject endingDoorEnd;

    public void InSewerDialog(string setDialog)
    {
        if (setDialog == "Sewer_LadderUP")
        {
            theDM.ShowDialog(sewerLadderUP.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_EnemySpawn")
        {
            theDM.ShowDialog(sewerEnemySpawn.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_StealthSucc")
        {
            theDM.ShowDialog(sewerStealthSucc.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_StealthFail")
        {
            theDM.ShowDialog(sewerStealthFail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_DisgauseFail")
        {
            theDM.ShowDialog(sewerDisgauseFail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_MapDialog")
        {
            theDM.ShowDialog(sewerMapDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_MapLook")
        {
            theDM.ShowDialog(sewerMapLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }

        if (setDialog == "Sewer_JournalDialog")
        {
            theDM.ShowDialog(sewerJournalDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_JornalLook")
        {
            if (DataBaseManager.firstLookJournal == false)
            {
                DataBaseManager.firstLookJournal = true;
                theDM.ShowDialog(sewerJornalLook.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(sewerJornalLookAfterSanCheck.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Sewer_JornalLookAfterSanCheck")
        {
            theDM.ShowDialog(sewerJornalLookAfterSanCheck.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_BookDialog")
        {
            theDM.ShowDialog(sewerBookDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_BookLook")
        {
            if (DataBaseManager.BookLookFirst == false)
            {
                DataBaseManager.BookLookFirst = true;
                theDM.ShowDialog(sewerBookLook.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(sewerBookLookAfterCheck.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Sewer_JornalLookSanCheck")
        {
            theDM.ShowDialog(sewerJornalLookSanCheck.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_SymbolDialog")
        {
            theDM.ShowDialog(sewerSymbolDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_SymbolLook")
        {
            if (DataBaseManager.JournalFirstSanCheck == true)
            {
                theDM.ShowDialog(sewerSymbolLookYseJournal.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                if (DataBaseManager.SymbolOccultFirst == false)
                {
                    theDM.ShowDialog(sewerSymbolLookNoJournal.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(sewerSymbolLookNoJournalAfterOccult.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
        }
        if (setDialog == "Sewer_FirstIn")
        {
            theDM.ShowDialog(sewerFirstIn.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_FirstDeepOne")
        {
            theDM.ShowDialog(sewerFirstDeepOne.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_LadderDialog")
        {
            theDM.ShowDialog(sewerLadderDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ending_BreakSymbol")
        {
            theDM.ShowDialog(endingBreakSymbol.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ending_isPromise")
        {
            if (DataBaseManager.Intel_Meiv2 == true)
            {
                if (DataBaseManager.AlbertDeathKeyword == false)
                {
                    theDM.ShowDialog(endingIfGotPromiseAndAblertAlive.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(endingIfGotPromiseAndAblertDeath.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else if (DataBaseManager.Intel_Meiv2 == false)
            {
                theDM.ShowDialog(endingIfNoPromise.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Ending_IfNoPromiseAlbertDeath")
        {
            theDM.ShowDialog(endingIfNoPromiseAlbertDeath.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ending_SwainDeathCheck")
        {
            theDM.ShowDialog(endingIfSwainDeath.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ending_RequestCheck")
        {
            if (DataBaseManager.IfGotRequset == true)
            {
                theDM.ShowDialog(endingLastIfGotRequest.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(endingLastIfNoRequest.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Ending_Killing")
        {
            theDM.ShowDialog(endingKilling.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ending_DoorEnd")
        {
            theDM.ShowDialog(endingDoorEnd.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void Start_SewerOffice(string setDialog)
    {
        if (setDialog == "SewerDoordialog")
        {
            theDM.ShowDialog(sewerDoorDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "SewerDoorLook")
        {
            theDM.ShowDialog(sewerDoorLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Window_dialog")
        {
            if (DataBaseManager.TimeCount % 4 == 1 || DataBaseManager.TimeCount % 4 == 2)
            {
                theDM.ShowDialog(windowDialogBFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(windowDialogAFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Window_look")
        {
            if (DataBaseManager.TimeCount % 4 == 1 || DataBaseManager.TimeCount % 4 == 2)
            {
                theDM.ShowDialog(windowLookBFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(windowLookAFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Map_dialog")
        {
            if (DataBaseManager.TimeCount % 4 == 1 || DataBaseManager.TimeCount % 4 == 2)
            {
                theDM.ShowDialog(mapDialogBFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(mapDialogAFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Map_look")
        {
            if (DataBaseManager.TimeCount % 4 == 1 || DataBaseManager.TimeCount % 4 == 2)
            {
                theDM.ShowDialog(mapLookBFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(mapLookAFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Locker_dialog")
        {
            if (DataBaseManager.TimeCount % 4 == 1 || DataBaseManager.TimeCount % 4 == 2)
            {
                theDM.ShowDialog(lockerDialogBFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(lockerDialogAFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Locker_look")
        {
            if (DataBaseManager.TimeCount % 4 == 1 || DataBaseManager.TimeCount % 4 == 2)
            {
                if (DataBaseManager.Black_RechSucc_CanTalck == true)
                {
                    theDM.ShowDialog(afterJudgeLookLocker.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(lockerLookBFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                theDM.ShowDialog(lockerLookAFNoon.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "BlackWell_dialog")
        {
            if (DataBaseManager.Black_FirstDialog == false)
            {
                DataBaseManager.Black_FirstDialog = true;
                theDM.ShowDialog(blackWellFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(blackWellDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "BlackWell_look")
        {
            theDM.ShowDialog(blackWellDialogLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void Start_2nd_NoonEvent(string setDialog)
    {
        if (setDialog == "NoonEvent_FirstDailog_Detective")
        {
            if (DataBaseManager.Intel_QuestHouse1 == false)
            {
                theDM.ShowDialog(noonEventFirstDailogNoquest.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Intel_MissingPeople1 == true || DataBaseManager.Intel_MissingPeople2 == true)
            {
                theDM.ShowDialog(noonEventFirstDailogMissingPeople.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(noonEventFirstDailogNoMissing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "NoonEvent_ClientHouse")
        {
            theDM.ShowDialog(noonEventClientHouse.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NoonEvent_Witness_Dialog")
        {
            if (DataBaseManager.Witness_FirstDailog == false)
            {
                DataBaseManager.Witness_FirstDailog = true;
                theDM.ShowDialog(noonEventWitnessFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(noonEventWitnessRepeatDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "NoonEvent_Witness_Look")
        {
            theDM.ShowDialog(noonEventWitnessLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NoonEvent_DoorPolice_Dialog")
        {
            if (DataBaseManager.DoorPolice_FirstDailog == false)
            {
                DataBaseManager.DoorPolice_FirstDailog = true;
                theDM.ShowDialog(noonEventDoorPoliceFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(noonEventDoorPoliceRepeatDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "NoonEvent_DoorPolice_Look")
        {
            theDM.ShowDialog(noonEventDoorPoliceLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NoonEvent_FirstDoor")
        {
            theDM.ShowDialog(noonEventFirstDoor.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NoonEvent_InsidePolice_Dialog")
        {
            if (DataBaseManager.InsidePolice_FirstDailog == false)
            {
                DataBaseManager.InsidePolice_FirstDailog = true;
                theDM.ShowDialog(noonEventInsidePoliceFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(noonEventInsidePoliceRepeatDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "NoonEvent_InsidePolice_Look")
        {
            theDM.ShowDialog(noonEventInsidePoliceLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NoonEvent_DeadBody_Dialog")
        {
            theDM.ShowDialog(noonEventDeadBodyDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NoonEvent_DeadBody_Look")
        {
            DataBaseManager.DeadBodyFirstLook = true;
            if (DataBaseManager.Intel_FishySmell1 == true)
            {
                theDM.ShowDialog(noonEventDeadBodyLookYesRumor.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(noonEventDeadBodyLookNoRumor.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "NoonEvent_DeadBody_Look_Missing")
        {
            theDM.ShowDialog(noonEventDeadBodyLookMissing.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NoonEvent_DeadBody_Look_FishySmell")
        {
            theDM.ShowDialog(noonEventDeadBodyLookFishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NoonEvent_DeadBody_analysisSuccAfter")
        {
            theDM.ShowDialog(noonEventDeadBodyAnalysisSuccAfter.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Accident Site_Dialog")
        {
            theDM.ShowDialog(siteDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Accident Site_Look")
        {
            theDM.ShowDialog(siteLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Site_analysisSuccAfter")
        {
            theDM.ShowDialog(siteAnalysisSuccAfter.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void Start_2nd_DetectiveOffice(string setDialog)
    {
        if (setDialog == "Fab_Case1")
        {
            theDM.ShowDialog(fabCase1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Case1After")
        {
            theDM.ShowDialog(fabCase1After.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Case2")
        {
            theDM.ShowDialog(fabCase2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Case2Succ")
        {
            theDM.ShowDialog(fabCase2Succ.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Case2SuccAfter")
        {
            theDM.ShowDialog(fabCase2SuccAfter.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Case2Fail")
        {
            theDM.ShowDialog(fabCase2Fail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Case3")
        {
            theDM.ShowDialog(fabCase3.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void Start_Safe_Look(string setDialog)
    {
        if (setDialog == "OldMap_Look")
        {
            theDM.ShowDialog(oldMapLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Document_Look")
        {
            theDM.ShowDialog(documentLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Safe_Look")
        {
            theDM.ShowDialog(safeLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Safe_Fail")
        {
            theDM.ShowDialog(safeFail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Bracelet_Look")
        {
            theDM.ShowDialog(braceletLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Coat_Look")
        {
            theDM.ShowDialog(itemCoatLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "SewergeKey_Look")
        {
            theDM.ShowDialog(sewergeKeyLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Map_Look")
        {
            theDM.ShowDialog(itemMapLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    // 1st
    public void Start_1st_DetectiveOffice(string setDialog)
    {
        if (setDialog == "start")
        {
            theDM.ShowDialog(start1stDetective.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "NewsPaper_Active")
        {
            theDM.ShowDialog(newsPaper.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "NewsPaper_Look")
        {
            DataBaseManager.fst_Detectiv_TimeOn += 1;
            theDM.ShowDialog(newsPaperLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Desk_Active")
        {
            theDM.ShowDialog(desk.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Desk_Look")
        {
            DataBaseManager.fst_Detectiv_TimeOn += 1;
            theDM.ShowDialog(deskLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Stove_Active")
        {
            theDM.ShowDialog(stove.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Stove_Look")
        {
            DataBaseManager.fst_Detectiv_TimeOn += 1;
            theDM.ShowDialog(stoveLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Directing_Nock")
        {
            DataBaseManager.fst_Detectiv_TimeOn = -99;
            theDM.ShowDialog(directingNock.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Directing_Door")
        {
            DataBaseManager.fst_Detectiv_TimeOn = -999;
            theDM.ShowDialog(directingDoor.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Directing_AfterDoor")
        {
            theDM.ShowDialog(directingAfterDoor.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Select_AboutCrime")
        {
            Debug.Log(4);
            theDM.ShowDialog(selectAboutCrime.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Select_AboutHospital")
        {
            theDM.ShowDialog(selectAboutHospital.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Select_AboutRunAway")
        {
            theDM.ShowDialog(selectAboutRunAway.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Accept_Request")
        {
            theDM.ShowDialog(acceptRequest.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Accept_AfterSelectYes")
        {
            theDM.ShowDialog(acceptAfterSelectYes.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Reject_Request")
        {
            theDM.ShowDialog(rejectRequest.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Accept_AfterSelectNo")
        {
            theDM.ShowDialog(acceptAfterSelectNo.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_FirstDialog")
        {
            theDM.ShowDialog(fabFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Aiden1")
        {
            theDM.ShowDialog(fabAiden1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Safe1")
        {
            theDM.ShowDialog(fabSafe1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Nightmare1")
        {
            theDM.ShowDialog(fabNightmare1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Nightmare2")
        {
            theDM.ShowDialog(fabNightmare2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Nightmare3")
        {
            theDM.ShowDialog(fabNightmare3.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Nightmare4")
        {
            theDM.ShowDialog(fabNightmare4.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Insomnia1")
        {
            theDM.ShowDialog(fabInsomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Insomnia2")
        {
            theDM.ShowDialog(fabInsomnia2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_PlanetarySequence1")
        {
            theDM.ShowDialog(fabPlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Univ1")
        {
            theDM.ShowDialog(fabUniv1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Meiv2")
        {
            theDM.ShowDialog(fabMeiv2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_MissingPeople")
        {
            theDM.ShowDialog(fabMissingPeople.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_GhostStory1")
        {
            theDM.ShowDialog(fabGhostStory1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_GhostStory2")
        {
            theDM.ShowDialog(fabGhostStory2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_CreepyEye")
        {
            theDM.ShowDialog(fabCreepyEye.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Worker1")
        {
            theDM.ShowDialog(fabWorker1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_FishySmell1")
        {
            theDM.ShowDialog(fabFishySmell1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_FishySmell1")
        {
            theDM.ShowDialog(fabFishySmell1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_FishySmell2")
        {
            theDM.ShowDialog(fabFishySmell2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_FishySmell3")
        {
            theDM.ShowDialog(fabFishySmell3.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_EndDay")
        {
            theDM.ShowDialog(fabEndDay.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Connect")
        {
            theDM.ShowDialog(fabConnect.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Witness")
        {
            theDM.ShowDialog(fabWitness.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_OwnerofUnifrom")
        {
            theDM.ShowDialog(fabOwnerUnifrom.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_CauseofDeath")
        {
            theDM.ShowDialog(fabCauseDeath.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_WebbedFeet")
        {
            theDM.ShowDialog(fabWebbedFeet.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Runway")
        {
            theDM.ShowDialog(fabRunway.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_FishySmellMissing")
        {
            theDM.ShowDialog(fabFishySmellMissing.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_TheClupet")
        {
            theDM.ShowDialog(fabTheClupet.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_Both")
        {
            theDM.ShowDialog(fabBoth.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_WarnOrRoot")
        {
            theDM.ShowDialog(fabWarnOrRoot.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_SwainSewer")
        {
            theDM.ShowDialog(fabSwainSewer.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "Fab_2ndConnectStart")
        {
            theDM.ShowDialog(fab2ndConnectStart.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "SecondDream")
        {
            theDM.ShowDialog(secondDream.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        else if (setDialog == "ThirdNightmare")
        {
            theDM.ShowDialog(thirdNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void Start_1st_ClientsHouse(string setDialog)
    {
        if (setDialog == "FirstArrive")
        {
            theDM.ShowDialog(firstArrive.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "NockDoor")
        {
            theDM.ShowDialog(nockDoor.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ella_fistDialog")
        {
            theDM.ShowDialog(ellaFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ella_Dialog1")
        {
            if (DataBaseManager.Ella_EndDialog == false && DataBaseManager.Aiden_FirstDialog == true)
            {
                DataBaseManager.Ella_EndDialog = true;

                if (DataBaseManager.getSafe == false)
                {
                    theDM.ShowDialog(ellaEndNoSafe.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(ellaEndGetSafe.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                theDM.ShowDialog(ellaDialog1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Ella_Dialog2")
        {
            theDM.ShowDialog(ellaDialog2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Ella_FirstUpstair")
        {
            theDM.ShowDialog(ellaFirstUpstair.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        //
        if (setDialog == "into_FirstRoom")
        {
            theDM.ShowDialog(intoFirstRoom.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_FirstDialog")
        {
            DataBaseManager.Aiden_FirstDialog = true;
            theDM.ShowDialog(aidenFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Active")
        {
            theDM.ShowDialog(aidenActive.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Look")
        {
            theDM.ShowDialog(aidenLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_LookJudge")
        {
            theDM.ShowDialog(aidenLookJudge.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Dialog1")
        {
            theDM.ShowDialog(aidenDialog1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Dialog2")
        {
            theDM.ShowDialog(aidenDialog2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Nightmare1")
        {
            theDM.ShowDialog(aidenKeyNightmare1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_EllaTriss1")
        {
            theDM.ShowDialog(aidenkeyEllaTriss1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe1")
        {
            theDM.ShowDialog(aidenKeySafe1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe_Str")
        {
            theDM.ShowDialog(aidenKeySafeStr.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe_Str_Sucsses")
        {
            theDM.ShowDialog(aidenKeySafeStrSucc.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe_Str_Fail")
        {
            theDM.ShowDialog(ellaFirstUpstair.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe_rhetoric_sucsses")
        {
            theDM.ShowDialog(aidenkeySafeRhetoricSucc.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe_rhetoric_Fail")
        {
            theDM.ShowDialog(aidenKeySafeRhetoricFail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Safe_Nothing")
        {
            theDM.ShowDialog(aidenKeySafeNothing.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Fail1")
        {
            theDM.ShowDialog(aidenKeyFail1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_Fail2")
        {
            theDM.ShowDialog(aidenKeyFail2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_medicine_Sucsses")
        {
            theDM.ShowDialog(aidenJudgeMedicineSucc.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_medicine_Fail")
        {
            theDM.ShowDialog(aidenJudgeMedicineFail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_psychotherapy_Sucsses")
        {
            theDM.ShowDialog(aidenJudgePsychotherapySucc.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_psychotherapy_Sucsses_Safe")
        {
            theDM.ShowDialog(aidenJudgePsychotherapySuccSafe.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_psychotherapy_Sucsses_NoSafe")
        {
            theDM.ShowDialog(aidenJudgePsychotherapySuccNoSafe.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_psychotherapy_Sucsses_End")
        {
            theDM.ShowDialog(aidenJudgePsychotherapySuccEnd.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_psychotherapy_Fail")
        {
            theDM.ShowDialog(aidenJudgePsychotherapyFail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_Observation_Sucsses")
        {
            theDM.ShowDialog(aidenJudgeObservationSucc.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_Judge_Observation_Fail")
        {
            theDM.ShowDialog(aidenJudgeObservationFail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Aiden_key_FailForce")
        {
            theDM.ShowDialog(aidenKeyFailForce.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "EllaEnd_NoSafe")
        {
            theDM.ShowDialog(ellaEndNoSafe.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "EllaEnd_GetSafe")
        {
            theDM.ShowDialog(ellaEndGetSafe.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void Start_1st_DailyNews(string setDialog)
    {
        if (setDialog == "Swain_FirstDialog")
        {
            if (DataBaseManager.Aiden_FirstDialog == false)
            {
                DataBaseManager.Aiden_FirstDialog = true;
                if (DataBaseManager.TimeCount < 6)
                {
                    theDM.ShowDialog(swainFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(swainNoContectFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                theDM.ShowDialog(swainDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Swain_Look")
        {
            theDM.ShowDialog(swainLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_LookJudge_Sucsses")
        {
            theDM.ShowDialog(swainLookJudgeSucsses.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_LookJudge_Fail")
        {
            theDM.ShowDialog(swainLookJudgeFail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_PlanetarySequence1")
        {
            theDM.ShowDialog(swainKeyPlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_Insomnia1")
        {
            theDM.ShowDialog(swainKeyInsomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_Nightmare2")
        {
            theDM.ShowDialog(swainKeyNightmare2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_Paranoia1")
        {
            theDM.ShowDialog(swainKeyParanoia1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_EllaTriss1")
        {
            theDM.ShowDialog(swainKeyEllaTriss1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_AidenTriss1")
        {
            theDM.ShowDialog(swainKeyAidenTriss1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_DailyNews1")
        {
            theDM.ShowDialog(swainKeyDailyNews1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Swain_Key_Nothing")
        {
            theDM.ShowDialog(swainKeyNothing.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Journalist_FirstDialog")
        {
            theDM.ShowDialog(journalistFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void Start_1st_University(string setDialog)
    {
        if (setDialog == "Univ_FirstDialog")
        {
            if (DataBaseManager.Univ_FirstDialog == false)
            {
                DataBaseManager.Univ_FirstDialog = true;
                theDM.ShowDialog(univFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(univDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Univ_Look")
        {

            theDM.ShowDialog(univLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Univ_Key_PlanetarySequence1")
        {
            theDM.ShowDialog(univKeyPlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Univ_Key_Insomnia1")
        {
            theDM.ShowDialog(univKeyInsomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Kane_FirstDialog")
        {
            if (DataBaseManager.Kane_FirstDialog == false && DataBaseManager.Intel_RiverWaterQuality2 == true)
            {
                DataBaseManager.Kane_FirstDialog = true;
                theDM.ShowDialog(kaneDetailOb.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Kane_FirstDialog == false)
            {
                DataBaseManager.Kane_FirstDialog = true;
                theDM.ShowDialog(kaneFirstMeatSev.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(kaneAfterSevDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Kane_Look")
        {
            theDM.ShowDialog(kaneAfterSevLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void Start_1st_Riverside(string setDialog)
    {
        if (setDialog == "Kane_FirstDialog")
        {
            if (DataBaseManager.Kane_FirstDialog == false)
            {
                DataBaseManager.Kane_FirstDialog = true;
                theDM.ShowDialog(kaneFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(kaneDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Kane_Look")
        {
            theDM.ShowDialog(kaneLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Kane_Nothing")
        {
            theDM.ShowDialog(kaneNothing.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_FirstDialog")
        {
            if (DataBaseManager.Sewer_FirstDialog == false)
            {
                DataBaseManager.Sewer_FirstDialog = true;
                theDM.ShowDialog(sewerFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(sewerDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Sewer_Look")
        {
            if (DataBaseManager.Sewer_ObservationEnd == false)
            {
                theDM.ShowDialog(sewerLook.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(sewerObservationFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Sewer_Observation_SucssesAfter")
        {
            theDM.ShowDialog(sewerObservationSuccAfter.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_Observation_Sucsses")
        {
            theDM.ShowDialog(sewerObservationSucc.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_Observation_Fail")
        {
            theDM.ShowDialog(sewerObservationFail.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Sewer_Observation_SucssesAfterSanSuc")
        {
            theDM.ShowDialog(sewerObservationSuccAfterSanSuc.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void Start_1st_Hospital(string setDialog)
    {
        if (setDialog == "Kate_NoonLook")
        {
            theDM.ShowDialog(kateNoonLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Alan_NoonLook")
        {
            theDM.ShowDialog(alanNoonLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Hospital_NoonFirst")
        {
            theDM.ShowDialog(hospitalNoonFirst.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Kate_FirstDialog")
        {
            if ((DataBaseManager.TimeCount % 4) == 2)
            {
                theDM.ShowDialog(kateFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                if (DataBaseManager.Kate_NoonFirstDialog == false)
                {
                    DataBaseManager.Kate_NoonFirstDialog = true;
                    theDM.ShowDialog(kateNoonFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(kateNoonDialog.transform.GetComponent<interactionEvent>().GetDialogs());
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
                    theDM.ShowDialog(alanFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(alanDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                if (DataBaseManager.TimeCount > 8 && DataBaseManager.ThirdDayAlanDialog == false)
                {
                    DataBaseManager.ThirdDayAlanDialog = true;
                    DataBaseManager.Alan_NoonFirstDialog = true;
                    theDM.ShowDialog(alanThirdFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                if (DataBaseManager.Alan_NoonFirstDialog == false)
                {
                    DataBaseManager.Alan_NoonFirstDialog = true;
                    theDM.ShowDialog(alanNoonFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(alanNoonDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
        }
    }
    public void Start_1st_Policeoffice(string setDialog)
    {

        if (setDialog == "A_FirstDialog")
        {
            if (DataBaseManager.AlbertDeathKeyword == true && DataBaseManager.TimeCount > 8 && DataBaseManager.ThirdDayPoliceADialog == true)
            {
                DataBaseManager.ThirdDayPoliceADialog = false;
                DataBaseManager.A_FirstDialog = true;
                theDM.ShowDialog(policeANoAlbert.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.A_FirstDialog == false)
            {
                DataBaseManager.A_FirstDialog = true;
                theDM.ShowDialog(aFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(aDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "B_FirstDialog")
        {
            if (DataBaseManager.AlbertDeathKeyword == true && DataBaseManager.TimeCount > 8 && DataBaseManager.ThirdDayPoliceBDialog == true)
            {
                DataBaseManager.ThirdDayPoliceBDialog = false;
                DataBaseManager.B_FirstDialog = true;
                theDM.ShowDialog(policeBNoAlbert.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.B_FirstDialog == false)
            {
                DataBaseManager.B_FirstDialog = true;
                theDM.ShowDialog(bFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(bDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Albert_FirstDialog")
        {
            if (DataBaseManager.TimeCount < 7)
            {
                if (DataBaseManager.Albert_FirstDialog == false)
                {
                    DataBaseManager.Albert_FirstDialog = true;
                    theDM.ShowDialog(albertFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(albertDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else if (DataBaseManager.TimeCount > 8)
            {
                if (DataBaseManager.ThirdDayAlbertDialog == false)
                {
                    DataBaseManager.ThirdDayAlbertDialog = true;
                    theDM.ShowDialog(albertThirdFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(albertDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                if (DataBaseManager.Albert_FirstDialog == false)
                {
                    DataBaseManager.Albert_FirstDialog = true;
                    DataBaseManager.AlbertFirstAfterEvent = true;
                    theDM.ShowDialog(albertFirstDialogAfterEvent.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if (DataBaseManager.AlbertFirstAfterEvent == false)
                {
                    DataBaseManager.AlbertFirstAfterEvent = true;
                    theDM.ShowDialog(albertFirstDialogNew.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(albertDialog.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
        }
        if (setDialog == "A_Look")
        {
            theDM.ShowDialog(aLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "B_Look")
        {
            theDM.ShowDialog(bLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Albert_Look")
        {
            theDM.ShowDialog(albertLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void Start_1st_Slum(string setDialog)
    {
        if (setDialog == "Dave_FirstDialog")
        {
            if (DataBaseManager.Dave_FirstDialog == false)
            {
                DataBaseManager.Dave_FirstDialog = true;
                theDM.ShowDialog(daveFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(daveDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Dave_Ran1")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(daveRand1.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran2")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(daveRand2.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran3")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(daveRand3.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran4")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(daveRand4.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran5")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(daveRand5.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran6")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(daveRand6.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran7")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(daveRand7.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran8")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(daveRand8.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_Ran9")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(daveRand9.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_NoMoney")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(daveNoMoney.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Dave_RanEnd")
        {
            End_Look_Judge.Instance.DaveRandActive();
            theDM.ShowDialog(daveRandEnd.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "AfterDaveDeath")
        {
            theDM.ShowDialog(afterDaveDeath.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void Start_1st_Bar(string setDialog)
    {
        if (setDialog == "Maeve_FirstDialog")
        {
            if (DataBaseManager.Maeve_FirstDialog == false)
            {
                DataBaseManager.Maeve_FirstDialog = true;
                if (DataBaseManager.Intel_QuestHouse1 == true)
                {
                    if (DataBaseManager.TimeCount == 12)
                    {
                        theDM.ShowDialog(meaveThirdDayFirstO.transform.GetComponent<interactionEvent>().GetDialogs());
                    }
                    else
                    {
                        theDM.ShowDialog(maeveFirstDialogJobO.transform.GetComponent<interactionEvent>().GetDialogs());
                    }
                }
                else
                {
                    if (DataBaseManager.TimeCount == 12)
                    {
                        theDM.ShowDialog(meaveThirdDayFirstX.transform.GetComponent<interactionEvent>().GetDialogs());
                    }
                    else
                    {
                        theDM.ShowDialog(maeveFirstDialogJobX.transform.GetComponent<interactionEvent>().GetDialogs());
                    }
                }
            }
            else
            {
                theDM.ShowDialog(maeveDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Maeve_Look")
        {
            theDM.ShowDialog(maeveLook.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "Worker_FirstDialog")
        {
            if (DataBaseManager.Worker_FirstDialog == false)
            {
                DataBaseManager.Worker_FirstDialog = true;
                theDM.ShowDialog(workerFirstDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(workerDialog.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Worker_Look")
        {
            if (DataBaseManager.Intel_FishySmell1 == true || DataBaseManager.Intel_FishySmell2 == true || DataBaseManager.Intel_FishySmell3 == true || DataBaseManager.Intel_FishySmell4 == true)
            {
                theDM.ShowDialog(workerLookO.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(workerLookX.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "BarArrive")
        {
            if (DataBaseManager.Intel_FishySmell1 == true || DataBaseManager.Intel_FishySmell2 == true || DataBaseManager.Intel_FishySmell3 == true || DataBaseManager.Intel_FishySmell4 == true)
            {
                theDM.ShowDialog(day2BarFishysmellO.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(day2BarFishysmellX.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Meave2ndDialog")
        {
            if (DataBaseManager.isBar == true)
            {
                theDM.ShowDialog(day2Bar1stBarIn.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(day2BarFirstBar.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
    }
    public void KeywordDialog(string setDialog)
    {
        if (setDialog == "Ella Triss")
        {
            if (DataBaseManager.keyword_downer == "Subject to request" && DataBaseManager.Intel_SewerGhostStory1 == false)
            {
                theDM.ShowDialog(ellaKeyAiden.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Rumor")
            {
                if (DataBaseManager.Intel_SewerGhostStory1 == true)
                {
                    theDM.ShowDialog(ellaKeyFishySmell1YesGhosty1.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(ellaKeyFishySmell1NoGhosty1.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else if (DataBaseManager.keyword_downer == "Contents of a Newspaper")
            {
                theDM.ShowDialog(ellaKeyContentNewspaper.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Content of a Nightmare")
            {
                theDM.ShowDialog(ellaKeyContentNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's Detail")
            {
                theDM.ShowDialog(ellaKeyAidensDetail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Water and Nightmare")
            {
                theDM.ShowDialog(ellaKeyWaterNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Doctor's opinion")
            {
                theDM.ShowDialog(ellaKeyDoctorsOpinion.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's Safe")
            {
                theDM.ShowDialog(ellaKeyAidenSafe.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Acquiring a Safe")
            {
                theDM.ShowDialog(ellaKeyAcquiringaSafe.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden & Pandemic")
            {
                theDM.ShowDialog(ellaKeyAidenPandemic.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(ellaKeyDisable.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Aiden Triss")
        {
            if (DataBaseManager.keyword_downer == "Aiden's Nightmare")
            {
                theDM.ShowDialog(aidenKeyNightmare1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Client")
            {
                theDM.ShowDialog(aidenkeyEllaTriss1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Saw in the sewer")
            {
                theDM.ShowDialog(aidenKeyCreepyEye1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Albert's Information" || DataBaseManager.keyword_downer == "Reporting by Swain")
            {
                theDM.ShowDialog(aidenKeyMissing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Contents of a GhostStory")
            {
                theDM.ShowDialog(aidenKeyFishySmellOrigin.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Source speculation" || DataBaseManager.keyword_downer == "Sewer & FishySmell" || DataBaseManager.keyword_downer == "Cause of Fishy smell")
            {
                theDM.ShowDialog(aidenKeyContentGhostStory.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Owner of Eye?")
            {
                theDM.ShowDialog(aidenKeyOwnerOfEye.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's Safe")
            {
                if (DataBaseManager.getSafe == false)
                    theDM.ShowDialog(aidenKeySafe1.transform.GetComponent<interactionEvent>().GetDialogs());
                else
                {
                    theDM.ShowDialog(aidenJudgeObservationFail.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                if (DataBaseManager.AidenKeywordCount == 0)
                {
                    DataBaseManager.AidenKeywordCount += 1;
                    theDM.ShowDialog(aidenKeyFail1.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(aidenKeyFail2.transform.GetComponent<interactionEvent>().GetDialogs());
                }

            }
        }
        if (setDialog == "Berkeley Swain")
        {
            if (DataBaseManager.keyword_downer == "Contents of a Newspaper")
            {
                theDM.ShowDialog(swainKeyPlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Newspaper Article")
            {
                theDM.ShowDialog(swainKeyInsomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's Nightmare")
            {
                theDM.ShowDialog(swainKeyNightmare2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Husband's Condition")
            {
                theDM.ShowDialog(swainKeyParanoia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Client")
            {
                theDM.ShowDialog(swainKeyEllaTriss1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Subject to request")
            {
                theDM.ShowDialog(swainKeyAidenTriss1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Husband's job")
            {
                if (DataBaseManager.Intel_MissingPeople1 == false || DataBaseManager.Swain_isTalkMissingpeople == false)
                {
                    DataBaseManager.Swain_isTalkMissingpeople = true;
                    theDM.ShowDialog(swainKeyDailyNews1.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(swainKeyNothing.transform.GetComponent<interactionEvent>().GetDialogs());
                }

            }
            else if (DataBaseManager.keyword_downer == "Albert's Information")
            {
                if (DataBaseManager.Intel_MissingPeople2 == false && DataBaseManager.Swain_isTalkMissingpeople == false)
                {
                    DataBaseManager.Swain_isTalkMissingpeople = true;
                    theDM.ShowDialog(swainKeyMissingPeople1.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(swainKeyNothing.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else if (DataBaseManager.keyword_downer == "Source of the smell?")
            {
                theDM.ShowDialog(swainKeyFishySmell2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Contents of a GhostStory")
            {
                theDM.ShowDialog(swainKeyGhostStory1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Saw in the sewer")
            {
                theDM.ShowDialog(swainKeyCreepyEye1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Content of a Nightmare")
            {
                theDM.ShowDialog(swainKeyContentNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Water and Nightmare")
            {
                theDM.ShowDialog(swainKeyWaterNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Source speculation" || DataBaseManager.keyword_downer == "Sewer & FishySmell" || DataBaseManager.keyword_downer == "Cause of Fishy smell")
            {
                if (DataBaseManager.Intel_MissingPeople2 == true)
                {
                    theDM.ShowDialog(swainKeyFishySmellReport.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(swainKeyFishySmellNone.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else if (DataBaseManager.keyword_downer == "Details")
            {
                theDM.ShowDialog(swainKeyDetails.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden & Pandemic")
            {
                theDM.ShowDialog(swainKeyAidenPandemic.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Unfair dismissal")
            {
                theDM.ShowDialog(swainKeyUnfairDismissal.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Kidnap")
            {
                theDM.ShowDialog(swainKidnap.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Fishy Slime")
            {
                theDM.ShowDialog(swainFishySlime.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Manhole" || DataBaseManager.keyword_downer == "Aiden's warning")
            {
                DataBaseManager.SwainDeathKeyword = true;
                theDM.ShowDialog(swainManholeWarn.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Murdered Clients")
            {
                theDM.ShowDialog(swainMurderedElla.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Missing Subject")
            {
                if (DataBaseManager.Intel_MissingPeople1 == true || DataBaseManager.Intel_MissingPeople2 == true)
                {
                    theDM.ShowDialog(swainMissingAidenMissingPeopleOk.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(swainMissingAidenNomal.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else if (DataBaseManager.keyword_downer == "Witness Testimony")
            {
                DataBaseManager.SwainDeathKeyword = true;
                theDM.ShowDialog(swainWitness.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "The Culprit")
            {
                DataBaseManager.SwainDeathKeyword = true;
                theDM.ShowDialog(swainCulprit.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Missing People & Fishy Smell")
            {
                theDM.ShowDialog(swainMissingFishy.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Cause of Death")
            {
                theDM.ShowDialog(swainEllaDeathCause.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Webbed Feet")
            {
                theDM.ShowDialog(swainWebbedFeet.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Way to Escape")
            {
                DataBaseManager.SwainDeathKeyword = true;
                theDM.ShowDialog(swainRunawayRoute.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(swainKeyNothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "University student")
        {
            if (DataBaseManager.keyword_downer == "Contents of a Newspaper")
            {
                theDM.ShowDialog(univKeyPlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Newspaper Article")
            {
                theDM.ShowDialog(univKeyInsomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Content of a Nightmare")
            {
                theDM.ShowDialog(studentKeyContentNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Water and Nightmare")
            {
                theDM.ShowDialog(studentKeyWaterNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Rumor")
            {
                theDM.ShowDialog(studentKeyRumor.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Source speculation" || DataBaseManager.keyword_downer == "Sewer & FishySmell" || DataBaseManager.keyword_downer == "Cause of Fishy smell")
            {
                theDM.ShowDialog(studentKeyFishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Details")
            {
                theDM.ShowDialog(studentKeyDetails.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(univKeyNothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Kane")
        {
            if (DataBaseManager.keyword_downer == "Strange Point")
            {
                if (DataBaseManager.Intel_FishySmell1 == true)
                {
                    theDM.ShowDialog(kaneKeyWaterQuality1YesFishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(kaneKeyWaterQuality1NoFishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }

            else if (DataBaseManager.keyword_downer == "Fishy Slime")
            {
                if (DataBaseManager.Intel_CreepyEyes1 == true)
                {
                    theDM.ShowDialog(kaneFishySlimeCreepyEye.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(kaneFishySlimeNomal.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                theDM.ShowDialog(kaneNothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Albert Bradley")
        {
            if (DataBaseManager.keyword_downer == "Contents of a Newspaper")
            {
                theDM.ShowDialog(albertKeyPlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Newspaper Article")
            {
                theDM.ShowDialog(albertKeyInsomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Mafia friend")
            {
                theDM.ShowDialog(albertKeyMeiv1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Contents of a GhostStory")
            {
                theDM.ShowDialog(albertKeySewerGhostStory1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Rumor")
            {
                theDM.ShowDialog(albertKeyFishySmell1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Saw in the sewer")
            {
                theDM.ShowDialog(albertKeyCreepyEye1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Reporting by Swain")
            {
                theDM.ShowDialog(albertKeyMissingPeople2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            //2일차 추가
            else if (DataBaseManager.keyword_downer == "Water and Nightmare")
            {
                theDM.ShowDialog(albertKeyWaterNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Source speculation" || DataBaseManager.keyword_downer == "Sewer & FishySmell" || DataBaseManager.keyword_downer == "Cause of Fishy smell")
            {
                theDM.ShowDialog(albertKeyFishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Details")
            {
                if (DataBaseManager.Intel_FishySmell3 == true || DataBaseManager.Intel_FishySmell4 == true || DataBaseManager.Intel_SewerWorker3 == true)
                {
                    theDM.ShowDialog(albertKeyDetailsOrigin.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(albertKeyDetailsNoOrigin.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else if (DataBaseManager.keyword_downer == "Promise")
            {
                theDM.ShowDialog(albertKeyPromise.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Unfair dismissal")
            {
                theDM.ShowDialog(albertKeyUnfairDismissal.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            // 낮 이벤트 이후 추가
            else if (DataBaseManager.keyword_downer == "Root of problem")
            {
                DataBaseManager.AlbertDeathKeyword = true;
                theDM.ShowDialog(albertKeyRootProblem.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Murdered Clients")
            {
                theDM.ShowDialog(albertKeyDeadElla.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Missing Subject")
            {
                theDM.ShowDialog(albertKeyMissingAiden.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Witness Testimony")
            {
                theDM.ShowDialog(albertKeyWitness.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "The Culprit")
            {
                DataBaseManager.AlbertDeathKeyword = true;
                theDM.ShowDialog(albertKeyCulprit.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Cause of Death")
            {
                theDM.ShowDialog(albertKeyCauseOfDeath.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Webbed Feet")
            {
                if (DataBaseManager.TimeCount > 8)
                {
                    theDM.ShowDialog(albertKeyChangeWebbedFeet.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(albertKeyWebbedFeet.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else if (DataBaseManager.keyword_downer == "Way to Escape")
            {
                DataBaseManager.AlbertDeathKeyword = true;
                theDM.ShowDialog(albertKeyRunwayMenhole.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's warning")
            {
                DataBaseManager.AlbertDeathKeyword = true;
                theDM.ShowDialog(albertKeyWarnAiden.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Owner of Uniform")
            {
                DataBaseManager.AlbertDeathKeyword = true;
                theDM.ShowDialog(albertKeyOwnerOfOutfit.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Missing People & Fishy Smell" && DataBaseManager.TimeCount > 8)
            {
                theDM.ShowDialog(albertKeyChangeMissingFishySmell.transform.GetComponent<interactionEvent>().GetDialogs());
            }

            else
            {
                theDM.ShowDialog(albertKeyNothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Kate Lizzie")
        {
            if (DataBaseManager.keyword_downer == "Newspaper Article")
            {
                theDM.ShowDialog(kateNoonInsomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's Nightmare")
            {
                theDM.ShowDialog(kateNoonNightmare1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Husband's Condition")
            {
                theDM.ShowDialog(kateNoonParanoia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "State of congestion")
            {
                theDM.ShowDialog(kateNoonHospital1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Rumor")
            {
                theDM.ShowDialog(kateNoonFishySmell1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Contents of a GhostStory")
            {
                theDM.ShowDialog(kateKeyContentGhostStory.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Saw in the sewer")
            {
                theDM.ShowDialog(kateKeySawinSewer.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Subject to request")
            {
                theDM.ShowDialog(kateKeySubjectRequest.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Murdered Clients" || DataBaseManager.keyword_downer == "Cause of Death" || DataBaseManager.keyword_downer == "" || DataBaseManager.keyword_downer == "Webbed Feet")
            {
                theDM.ShowDialog(kateKeyEllaBody.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Witness Testimony")
            {
                theDM.ShowDialog(kateKeyWitness.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(kateNoonNothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Alan Parks")
        {
            if (DataBaseManager.keyword_downer == "Aiden's Nightmare")
            {
                theDM.ShowDialog(alanNoonNightmare1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Content of a Nightmare")
            {
                theDM.ShowDialog(alanNoonNightmare2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Aiden's Detail")
            {
                theDM.ShowDialog(alanNoonNightmare3.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Newspaper Article")
            {
                theDM.ShowDialog(alanNoonInsomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Husband's Condition")
            {
                theDM.ShowDialog(alanNoonParanoia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Subject to request")
            {
                theDM.ShowDialog(alanNoonAiden1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Details")
            {
                theDM.ShowDialog(alanKeyDetails.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "State of congestion")
            {
                theDM.ShowDialog(alanKeyStateCongestion.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Murdered Clients" || DataBaseManager.keyword_downer == "Cause of Death" || DataBaseManager.keyword_downer == "" || DataBaseManager.keyword_downer == "Webbed Feet")
            {
                theDM.ShowDialog(alanKeyEllaBody.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(alanNoonNothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Maeve")
        {
            if (DataBaseManager.keyword_downer == "Contents of a Newspaper" || DataBaseManager.keyword_downer == "Astronomical show")
            {
                theDM.ShowDialog(maeveKeyIntelPlanetarySequence1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Newspaper Article")
            {
                theDM.ShowDialog(maeveKeyInsomnia1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Police friend")
            {
                theDM.ShowDialog(maeveKeyAlbert1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Reporting by Swain" || DataBaseManager.keyword_downer == "Albert's Information")
            {
                theDM.ShowDialog(maeveKeyMissingPeople.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Saw in the sewer")
            {
                theDM.ShowDialog(maeveKeyCreepyEye.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Rumor")
            {
                theDM.ShowDialog(maeveKeyFishySmell1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Source of the smell?")
            {
                theDM.ShowDialog(maeveKeyFishySmell2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Subject to request")
            {
                theDM.ShowDialog(maeveKeyAiden1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Water and Nightmare")
            {
                theDM.ShowDialog(meaveKeyWaterNightmare.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Contents of a GhostStory")
            {
                theDM.ShowDialog(meaveKeyContentGhostStory.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Truth of a rumor")
            {
                theDM.ShowDialog(meaveKeyTruthRumor.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Details")
            {
                theDM.ShowDialog(meaveKeyDetails.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Root of problem")
            {
                theDM.ShowDialog(meaveKeyCauseProblem.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Murdered Clients" || DataBaseManager.keyword_downer == "Missing Subject")
            {
                theDM.ShowDialog(meaveKey_EllaAiden.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Witness Testimony")
            {
                theDM.ShowDialog(meaveKeyWitness.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Missing People & Fishy Smell")
            {
                if (DataBaseManager.keyword_downer == "Source speculation" || DataBaseManager.keyword_downer == "Sewer & FishySmell" || DataBaseManager.keyword_downer == "Cause of Fishy smell")
                {
                    theDM.ShowDialog(meaveKeyMissingFishyCauseO.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(meaveKeyMissingFishyCauseX.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                theDM.ShowDialog(maeveKeyNothing.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Dusty Dave")
        {
            selectionUIManager.Instance.EndDialog();
            if (DataBaseManager.keyword_downer == "Witness Testimony")
            {
                theDM.ShowDialog(daveKeyWitness.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "")
            {
                theDM.ShowDialog(daveKeySafeAquire.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.keyword_downer == "Acquiring a Safe")
            {
                if (DataBaseManager.DaveSafeOpen == true)
                {
                    theDM.ShowDialog(daveKeyOpenSafe.transform.GetComponent<interactionEvent>().GetDialogs());

                }
                else if (DataBaseManager.DaveAfterSafe == false)
                {
                    DataBaseManager.DaveAfterSafe = true;
                    theDM.ShowDialog(daveKeySafeAquire.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(daveKeySafeAfter.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else if (DataBaseManager.keyword_downer == "Bracelet")
            {
                if (DataBaseManager.DaveAfterBreclet == false)
                {
                    DataBaseManager.DaveAfterBreclet = true;
                    theDM.ShowDialog(daveKeyBreclet.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(daveKeyBrecletAfter.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                theDM.ShowDialog(daveKeyFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "BlackWell")
        {
            if (DataBaseManager.Black_RechSucc_CanTalck == true)
            {
                if (DataBaseManager.keyword_downer == "Source speculation" || DataBaseManager.keyword_downer == "Sewer & FishySmell" || DataBaseManager.keyword_downer == "Cause of Fishy smell")
                {
                    theDM.ShowDialog(blackWellKeyFishysmell.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if (DataBaseManager.keyword_downer == "Root of problem" || DataBaseManager.keyword_downer == "Aiden's warning")
                {
                    theDM.ShowDialog(blackWellKeyWarnRoot.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if (DataBaseManager.keyword_downer == "Witness Testimony")
                {
                    theDM.ShowDialog(blackWellKeyWitness.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if (DataBaseManager.keyword_downer == "Way to Escape")
                {
                    theDM.ShowDialog(blackWellKeyRunway.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(blackWellKeyFail.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else
            {
                theDM.ShowDialog(blackWellKeyFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (setDialog == "Symbol")
        {

            if (DataBaseManager.keyword_downer == "Advent")
            {
                theDM.ShowDialog(bloodEnding.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {

                theDM.ShowDialog(sewerSymbolKeyWorkFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
    }
    //전투
    public void BattleDialog(string setDialog)
    {
        if (setDialog == "start")
        {
            theDM.ShowDialog(battleStartDialog.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (setDialog == "End")
        {
            theDM.ShowDialog(battleEndBattle.transform.GetComponent<interactionEvent>().GetDialogs());
        }
    }
    public void RetrunDialogResult(string Sub_Dialog, string result_End, string SanCheck = "")
    {
        // 1일차 오전 탐정사무소 판정
        if (Sub_Dialog == "Newspaper : Read")
        {
            DataBaseManager.fst_Detectiv_TimeOn += 1;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(newsPaperJudegeTrue.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(newsPaperJudegeFalse.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Stove : Making coffee")
        {
            DataBaseManager.fst_Detectiv_TimeOn += 1;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.Condition = "Good";
                theDM.ShowDialog(stoveJudgeTrue.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(stoveJudgeFalse.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Aiden : Diagnosis")
        {
            DataBaseManager.Aiden_medicine = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(aidenJudgeMedicineSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(aidenJudgeMedicineFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Aiden : Look")
        {
            DataBaseManager.Aiden_Observation = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(aidenJudgeObservationSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(aidenJudgeObservationFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Aiden : Calming down")
        {
            DataBaseManager.Aiden_psychotherapy = true;

            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                if (DataBaseManager.getSafe == false)
                {
                    theDM.ShowDialog(aidenJudgePsychotherapySuccNoSafe.transform.GetComponent<interactionEvent>().GetDialogs());
                    DataBaseManager.getSafe = true;
                }
                else
                {
                    theDM.ShowDialog(aidenJudgePsychotherapySuccSafe.transform.GetComponent<interactionEvent>().GetDialogs());
                    DataBaseManager.getSafe = true;
                }

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(aidenJudgeMedicineFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Aiden : Persuasion")
        {

            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.getSafe = true;
                theDM.ShowDialog(aidenkeySafeRhetoricSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(aidenKeySafeRhetoricFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Aiden : TakeAway")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.getSafe = true;
                theDM.ShowDialog(aidenKeySafeStrSucc.transform.GetComponent<interactionEvent>().GetDialogs());

            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                BillowUIManager.Instance.HP_down(5);
                theDM.ShowDialog(aidenKeySafeStrFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Swain : Look")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(swainLookJudgeSucsses.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(swainLookJudgeFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Witness : Eavesdropping")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                if (DataBaseManager.Intel_QuestHouse1 == false)
                {
                    theDM.ShowDialog(noonEventWitnessListeningSuccNoRequest.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if (DataBaseManager.Intel_MissingPeople1 == true || DataBaseManager.Intel_MissingPeople2 == true)
                {
                    theDM.ShowDialog(noonEventWitnessListeningSuccMissingPeople.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(noonEventWitnessListeningSuccNomal.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(noonEventWitnessListeningFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Police : Persuasion")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(noonEventDoorPolicePersuasionSucc.transform.GetComponent<interactionEvent>().GetDialogs());
                DataBaseManager.NoonEvent_AccessAuthorization = true;
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(noonEventDoorPolicePersuasionFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (Sub_Dialog == "Maeve : Persuasion")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(meaveJudgeRhetoricSucc.transform.GetComponent<interactionEvent>().GetDialogs());
                DataBaseManager.NoonEvent_AccessAuthorization = true;
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(meaveJudgeRhetoricFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Door : Picking doors")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(sewerDoorPickSucc.transform.GetComponent<interactionEvent>().GetDialogs());
                DataBaseManager.SewerDoorOpenDeftness = true;
                DataBaseManager.SewerDoorOpenCheck = true;
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(sewerDoorPickFail.transform.GetComponent<interactionEvent>().GetDialogs());
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
                theDM.ShowDialog(windowJudgeBreakSucc.transform.GetComponent<interactionEvent>().GetDialogs());
                DataBaseManager.MoveSewerOfficeWindow = true;
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(windowJudgeBreakFail.transform.GetComponent<interactionEvent>().GetDialogs());
                DataBaseManager.MoveSewerOfficeWindow = true;
            }
        }
        if (Sub_Dialog == "Locker : Take a Look")
        {
            DataBaseManager.TryObserLokcer = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(lockerJudgeObservationSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(lockerJudgeObservationFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        // 2일차 강제이벤트
        if (Sub_Dialog == "DeadBody : Diagnosis")
        {
            DataBaseManager.DeadBody_medicine = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(noonEventDeadBodyMedicineSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(noonEventDeadBodyMedicineFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "DeadBody : Look")
        {
            DataBaseManager.DeadBody_observational = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(noonEventDeadBodyObservationSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(noonEventDeadBodyObservationFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "DeadBody : Check")
        {
            DataBaseManager.DeadBody_Analyzing = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(noonEventDeadBodyAnalysisSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(noonEventDeadBodyAnalysisFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "DeadBody : Stealing")
        {
            DataBaseManager.DeadBody_deftness = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(noonEventDeadBodyDeftnessSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(noonEventDeadBodyDeftnessFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }

        if (Sub_Dialog == "Accident Site : Check")
        {
            DataBaseManager.Site_Analyzing = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(siteAnalysisSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(SiteAnalysisFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        // 강가 Sewer : Look
        if (DataBaseManager.nowPlace == "Riverside")
        {
            if (Sub_Dialog == "Sewer : Look")
            {
                if (result_End == "Result : Success" || result_End == "Result : Critical Success")
                {
                    theDM.ShowDialog(sewerObservationSucc.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
                {
                    theDM.ShowDialog(sewerObservationFail.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            if (Sub_Dialog == "SAN : Check")
            {
                if (result_End == "Result : Success" || result_End == "Result : Critical Success")
                {
                    theDM.ShowDialog(sewerObservationSuccAfterSanSuc.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
                {
                    theDM.ShowDialog(sewerObservationSuccAfter.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
        }
        if (DataBaseManager.nowPlace == "BattleRoad")
        {
            if (Sub_Dialog == "SAN : Check")
            {
                if (result_End == "Result : Success" || result_End == "Result : Critical Success")
                {
                    theDM.ShowDialog(battleSanCheck.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
                {
                    theDM.ShowDialog(battleSanCheckFail.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
        }
        if (DataBaseManager.nowPlace == "DetectiveOffice")
        {
            if (DataBaseManager.TimeCount == 5)
            {
                if (Sub_Dialog == "SAN : Check")
                {
                    theDM.ShowDialog(fabCase1After.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
        }
        if (SanCheck == "NoonEvent_FirstDoor")
        {
            theDM.ShowDialog(noonEventFirstDoorAfterSan.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        //Item
        if (Sub_Dialog == "Safe : Open")
        {
            DataBaseManager.isUseSafe = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.getSafe = false;
                theDM.ShowDialog(safeSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(safeFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Coat : Disguise")
        {
            DataBaseManager.JudgeCoat_TryDisguise = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.WearCoat = true;
                theDM.ShowDialog(itemDiaguiseSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(itemDiaguiseFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        // 이성판정
        if (Sub_Dialog == "Case2IntCheck")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(fabCase2Succ.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(fabCase2Fail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (SanCheck == "KaneSan")
        {
            theDM.ShowDialog(kaneFishySlimeCreepyEyeAfterSans.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "DrinkSan")
        {
            theDM.ShowDialog(meaveJudgeRhetoricSanAfter.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "ThirdDayStartSan")
        {
            if (DataBaseManager.Intel_Nightmare3 == true || DataBaseManager.Intel_Nightmare4 == true)
            {
                theDM.ShowDialog(thirdDayStartCase1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(thirdDayStartCase2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (SanCheck == "DayTwoCase2San")
        {
            theDM.ShowDialog(thirdDayStartCase1AfterSan.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "JournalSan")
        {
            DataBaseManager.JournalFirstSanCheck = true;
            theDM.ShowDialog(sewerJornalLookSanCheck.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "SymbolStrSan")
        {
            theDM.ShowDialog(sewerSanChecck.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "Sewer_FirstInSan")
        {

            theDM.ShowDialog(sewerFirstInAfterSan.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "FourthDayStartSan")
        {
            if (DataBaseManager.Intel_Nightmare3 == true || DataBaseManager.Intel_Nightmare4 == true)
            {
                theDM.ShowDialog(fab4thQuestO.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                theDM.ShowDialog(fab4thQuestX.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (SanCheck == "Fab_4th_QuestO_SanCheck")
        {
            theDM.ShowDialog(fab4thQuestOSanCheck.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "Day_4_case1Health")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(fab4thQuestOHealthCheckSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                DataBaseManager.MorningMove_4th = true;
                DataBaseManager.MorningMove_3rd = true;
                DataBaseManager.MorningMove_2nd = true;
                theDM.ShowDialog(fab4thQuestOHealthCheckFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (SanCheck == "BookSan")
        {
            theDM.ShowDialog(sewerBookLookSanCheck.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "DeepOneFirstSan")
        {
            theDM.ShowDialog(sewerFirstDeepOneAfterSan.transform.GetComponent<interactionEvent>().GetDialogs());
        }
        if (SanCheck == "EndingLedderSan")
        {
            if (DataBaseManager.Ending_LadderSanInt == 0)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 1)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 2)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan3.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 3)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan4.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 4)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan5.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 5)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan6.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 6)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan7.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 7)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan8.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.Ending_LadderSanInt == 8)
            {
                DataBaseManager.Ending_LadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan9.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Warden : Steal")
        {
            DataBaseManager.Black_Deftness = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(blackWellJudegeDeftnessSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(blackWellJudegeDeftnessFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Warden : Persuade")
        {
            DataBaseManager.Black_Rhethic = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.Black_RechSucc_CanTalck = true;
                theDM.ShowDialog(blackWellJudegeRheticSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(blackWellJudegeRheticFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        // 하수도 내부 
        if (Sub_Dialog == "Fabian : Sneak Out")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(sewerStealthSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(sewerStealthFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "??? : Give Excuse")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(sewerRhethicSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(sewerRhethicFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Book : Decryption")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(sewerBookLookOccultSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(sewerBookLookOccultFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Sewer : Picking")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.SewerPicingSucc = true;
                theDM.ShowDialog(riverDeftnessSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(riverDeftnessFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Symbol : Decryption")
        {
            DataBaseManager.SymbolOccultFirst = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(sewerSymbolLookOccultSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(sewerSymbolLookOccultFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Symbol : Break")
        {
            DataBaseManager.StoryDirecting = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(sewerJudgeStrSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(sewerJudgeStrFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Fabian : Swimming")
        {
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(sewerLadderSwim.transform.GetComponent<interactionEvent>().GetDialogs());
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

