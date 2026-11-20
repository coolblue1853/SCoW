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
    public GameObject fourthNightmare;

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
            if (DataBaseManager.bookLookFirst == false)
            {
                DataBaseManager.bookLookFirst = true;
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
            if (DataBaseManager.journalFirstSanCheck == true)
            {
                theDM.ShowDialog(sewerSymbolLookYseJournal.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else
            {
                if (DataBaseManager.symbolOccultFirst == false)
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
            if (DataBaseManager.intelMeiv2 == true)
            {
                if (DataBaseManager.albertDeathKeyword == false)
                {
                    theDM.ShowDialog(endingIfGotPromiseAndAblertAlive.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else
                {
                    theDM.ShowDialog(endingIfGotPromiseAndAblertDeath.transform.GetComponent<interactionEvent>().GetDialogs());
                }
            }
            else if (DataBaseManager.intelMeiv2 == false)
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
            if (DataBaseManager.ifGotRequset == true)
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
    public void StartSewerOffice(string setDialog)
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
            if (DataBaseManager.timeCount % 4 == 1 || DataBaseManager.timeCount % 4 == 2)
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
            if (DataBaseManager.timeCount % 4 == 1 || DataBaseManager.timeCount % 4 == 2)
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
            if (DataBaseManager.timeCount % 4 == 1 || DataBaseManager.timeCount % 4 == 2)
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
            if (DataBaseManager.timeCount % 4 == 1 || DataBaseManager.timeCount % 4 == 2)
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
            if (DataBaseManager.timeCount % 4 == 1 || DataBaseManager.timeCount % 4 == 2)
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
            if (DataBaseManager.timeCount % 4 == 1 || DataBaseManager.timeCount % 4 == 2)
            {
                if (DataBaseManager.blackRechSuccCanTalck == true)
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
            if (DataBaseManager.blackFirstDialog == false)
            {
                DataBaseManager.blackFirstDialog = true;
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
    /// <summary>낮 사건 대사(C-2 P10b 이후 데이터 주도: Resources/Story/2nd_NoonEvent.json).</summary>
    public void Start2ndNoonEvent(string setDialog)
    {
        RunStory("2nd_NoonEvent", setDialog);
    }
    /// <summary>2일차 탐정사무소 시작 대사(C-2 P10b 이후 데이터 주도: Resources/Story/2nd_DetectiveOffice.json, 줄은 1st_DetectiveOffice CSV).</summary>
    public void Start2ndDetectiveOffice(string setDialog)
    {
        RunStory("2nd_DetectiveOffice", setDialog);
    }
    /// <summary>금고·아이템 살펴보기 대사(C-2 P10b 이후 데이터 주도: Resources/Story/SafeLook.json, 줄은 1st_Item CSV).</summary>
    public void StartSafeLook(string setDialog)
    {
        RunStory("SafeLook", setDialog);
    }
    // 1st
    /// <summary>StoryDatabase 로 topic 을 풀어 대사를 띄운다. 맞는 Topic 이 없으면 아무것도 안 한다(옛 if 체인과 같음).</summary>
    private void RunStory(string scriptId, string topicId)
    {
        Dialog[] dialogs;
        if (StoryDatabase.TryRun(scriptId, topicId, out dialogs))
        {
            theDM.ShowDialog(dialogs);
        }
    }
    /// <summary>1일차 탐정사무소 대사(C-2 P9 이후 데이터 주도: Resources/Story/1st_DetectiveOffice.json). 씬 interactionEvent 오브젝트·인스펙터 범위는 남기되 이 경로는 읽지 않는다.</summary>
    public void Start1stDetectiveOffice(string setDialog)
    {
        RunStory("1st_DetectiveOffice", setDialog);
    }
    public void Start1stClientsHouse(string setDialog)
    {
        RunStory("1st_Client'sHouse", setDialog);
    }
    public void Start1stDailyNews(string setDialog)
    {
        RunStory("1st_DailyNews", setDialog);
    }
    public void Start1stUniversity(string setDialog)
    {
        RunStory("1st_University", setDialog);
    }
    public void Start1stRiverside(string setDialog)
    {
        RunStory("1st_Riverside", setDialog);
    }
    public void Start1stHospital(string setDialog)
    {
        RunStory("1st_Hospital", setDialog);
    }
    /// <summary>경찰서 대사(C-2 P10b 이후 데이터 주도: Resources/Story/1st_PoliceOffice.json).</summary>
    public void Start1stPoliceoffice(string setDialog)
    {
        RunStory("1st_PoliceOffice", setDialog);
    }
    /// <summary>빈민가 대사(C-2 P10b 이후 데이터 주도: Resources/Story/1st_Slum.json). 데이브 랜덤 대사는 대사 전에 키워드 UI 를 켜는 씬 부수효과가 있어 여기서 먼저 한다(옛 순서 그대로).</summary>
    public void Start1stSlum(string setDialog)
    {
        switch (setDialog)
        {
            case "Dave_Ran1":
            case "Dave_Ran2":
            case "Dave_Ran3":
            case "Dave_Ran4":
            case "Dave_Ran5":
            case "Dave_Ran6":
            case "Dave_Ran7":
            case "Dave_Ran8":
            case "Dave_Ran9":
            case "Dave_NoMoney":
            case "Dave_RanEnd":
                End_Look_Judge.Instance.DaveRandActive();
                break;
        }
        RunStory("1st_Slum", setDialog);
    }
    /// <summary>바 대사(C-2 P9 이후 데이터 주도: Resources/Story/1st_Bar.json).</summary>
    public void Start1stBar(string setDialog)
    {
        RunStory("1st_Bar", setDialog);
    }
    /// <summary>
    /// 키워드 선택 대사(C-2 P11 이후 데이터 주도: Resources/Story/Keyword.json, 토픽 "NPC/키워드"). NPC 이름은 setDialog,
    /// 키워드는 DataBaseManager.keywordDowner. 씬 interactionEvent 오브젝트·인스펙터 범위는 남기되 이 경로는 읽지 않는다.
    /// </summary>
    public void KeywordDialog(string setDialog)
    {
        if (setDialog == "Dusty Dave")
        {
            // 대사 전 씬 부수효과(JSON 에 못 옮김): 빈민가 선택지 UI 닫기.
            selectionUIManager.Instance.EndDialog();
        }
        Dialog[] dialogs;
        if (StoryDatabase.TryRunKeyword(setDialog, DataBaseManager.keywordDowner, out dialogs))
        {
            theDM.ShowDialog(dialogs);
        }
    }
    /// <summary>전투 시작·종료 대사(C-2 P11 이후 데이터 주도: Resources/Story/1st_Battle.json).</summary>
    public void BattleDialog(string setDialog)
    {
        RunStory("1st_Battle", setDialog);
    }
    public void RetrunDialogResult(string Sub_Dialog, string result_End, string SanCheck = "")
    {
        // 1일차 오전 탐정사무소 판정
        if (Sub_Dialog == "Newspaper : Read")
        {
            DataBaseManager.fstDetectivTimeOn += 1;
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
            DataBaseManager.fstDetectivTimeOn += 1;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.condition = "Good";
                theDM.ShowDialog(stoveJudgeTrue.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(stoveJudgeFalse.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Aiden : Diagnosis")
        {
            DataBaseManager.aidenMedicine = true;
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
            DataBaseManager.aidenObservation = true;
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
            DataBaseManager.aidenPsychotherapy = true;

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
                BillowUIManager.Instance.HpDown(5);
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
                if (DataBaseManager.intelQuestHouse1 == false)
                {
                    theDM.ShowDialog(noonEventWitnessListeningSuccNoRequest.transform.GetComponent<interactionEvent>().GetDialogs());
                }
                else if (DataBaseManager.intelMissingPeople1 == true || DataBaseManager.intelMissingPeople2 == true)
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
                DataBaseManager.noonEventAccessAuthorization = true;
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
                DataBaseManager.noonEventAccessAuthorization = true;
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
                DataBaseManager.sewerDoorOpenDeftness = true;
                DataBaseManager.sewerDoorOpenCheck = true;
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(sewerDoorPickFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Window : Break")
        {
            DataBaseManager.sewerDoorOpenDeftness = true;
            DataBaseManager.sewerDoorOpenCheck = true;
            DataBaseManager.isSewerOfficeBrokenWindow = true;
            DataBaseManager.tryBrokenWindow = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                theDM.ShowDialog(windowJudgeBreakSucc.transform.GetComponent<interactionEvent>().GetDialogs());
                DataBaseManager.moveSewerOfficeWindow = true;
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(windowJudgeBreakFail.transform.GetComponent<interactionEvent>().GetDialogs());
                DataBaseManager.moveSewerOfficeWindow = true;
            }
        }
        if (Sub_Dialog == "Locker : Take a Look")
        {
            DataBaseManager.tryObserLokcer = true;
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
            DataBaseManager.deadBodyMedicine = true;
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
            DataBaseManager.deadBodyObservational = true;
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
            DataBaseManager.deadBodyAnalyzing = true;
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
            DataBaseManager.deadBodyDeftness = true;
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
            DataBaseManager.siteAnalyzing = true;
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
            if (DataBaseManager.timeCount == 5)
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
            DataBaseManager.judgeCoatTryDisguise = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.wearCoat = true;
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
            if (DataBaseManager.intelNightmare3 == true || DataBaseManager.intelNightmare4 == true)
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
            DataBaseManager.journalFirstSanCheck = true;
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
            if (DataBaseManager.intelNightmare3 == true || DataBaseManager.intelNightmare4 == true)
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
                DataBaseManager.morningMove4th = true;
                DataBaseManager.morningMove_3rd = true;
                DataBaseManager.morningMove2nd = true;
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
            if (DataBaseManager.endingLadderSanInt == 0)
            {
                DataBaseManager.endingLadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan1.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.endingLadderSanInt == 1)
            {
                DataBaseManager.endingLadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan2.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.endingLadderSanInt == 2)
            {
                DataBaseManager.endingLadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan3.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.endingLadderSanInt == 3)
            {
                DataBaseManager.endingLadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan4.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.endingLadderSanInt == 4)
            {
                DataBaseManager.endingLadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan5.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.endingLadderSanInt == 5)
            {
                DataBaseManager.endingLadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan6.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.endingLadderSanInt == 6)
            {
                DataBaseManager.endingLadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan7.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.endingLadderSanInt == 7)
            {
                DataBaseManager.endingLadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan8.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (DataBaseManager.endingLadderSanInt == 8)
            {
                DataBaseManager.endingLadderSanInt += 1;
                theDM.ShowDialog(sewerLadderSan9.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Warden : Steal")
        {
            DataBaseManager.blackDeftness = true;
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
            DataBaseManager.blackRhethic = true;
            if (result_End == "Result : Success" || result_End == "Result : Critical Success")
            {
                DataBaseManager.blackRechSuccCanTalck = true;
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
                DataBaseManager.sewerPicingSucc = true;
                theDM.ShowDialog(riverDeftnessSucc.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (result_End == "Result : Failure" || result_End == "Result : Fumble")
            {
                theDM.ShowDialog(riverDeftnessFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
        }
        if (Sub_Dialog == "Symbol : Decryption")
        {
            DataBaseManager.symbolOccultFirst = true;
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
            DataBaseManager.storyDirecting = true;
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
                BillowUIManager.Instance.HpDown(5);
                Invoke("SwinCheckAgain", 0.5f);
            }
        }
    }

    void SwinCheckAgain()
    {
        Rollet.Instance.SetRollet("Fabian : Swimming", "Swin", DataBaseManager.swimingPoint, "dialog");
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

