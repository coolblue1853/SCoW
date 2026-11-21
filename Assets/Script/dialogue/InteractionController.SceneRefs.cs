using UnityEngine;

/// <summary>
/// InteractionController 의 씬 직렬화 필드(interactionEvent 대사 오브젝트 참조). C-2 P9~P12 로 대사 본문은 Resources/Story JSON 으로 옮겨졌고
/// 이 필드들은 코드에서 읽지 않는다. Main 씬이 이름으로 직렬화 참조를 갖고 있어 지우거나 이름을 바꾸지 않는다(씬 YAML 을 직접 고치지 않음).
/// 대사 줄 범위는 이제 JSON 의 text(CSV파일:시작ID-끝ID)가 갖는다. 씬 참조를 정리하는 일은 별도 작업(P16 이후)이다.
/// </summary>
public partial class InteractionController
{
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
    // 1st Clinet'sOffice (ellaKey*·aidenKey* 필드 대부분은 C-2 P11 부터 코드에서 안 쓰지만 Main 씬 직렬화 참조가 있어 남긴다)
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
}
