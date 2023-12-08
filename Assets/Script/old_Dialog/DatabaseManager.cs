using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DataBaseManager : MonoBehaviour
{
    public static bool isNightmare = false;

    public static bool endingDoorCheck = false;
    public static bool endingDeathCheck = false;
    public static int enemyDeathCount = 0;
    public static bool endingRequestCheck = false;
    public static bool endingSwainDeathCheck = false;
    public static bool endingAfterPromiseDialog = false;
    public static bool endingisPromise = false;
    public static bool endingBreakSymbol = false;

    public static int endingLadderSanInt = 0;
    public static bool endingLadderSan;
    public static bool endingSwin;

    public static bool sewerMap;
    public static bool ifGotRequset = false;
    //디버프 3단계
    public static bool sewerFirstIn;
    public static bool sewerFirstInSanCheck;
    public static bool sewerDeepOneFirstSanCheck;

    public static bool isDebuffNightPhobia = false;
    public static bool isDebuffDeftness = false;
    public static bool isDebuffVisionLoss = false;
    public static bool isDebuffShortTempred = false;
    public static bool isDebuffDyslexia = false;
    public static bool isDebuffDrugPhobia = false;
    public static bool isDebuffCognitiveDisorder = false;
    public static bool isDebuffCognitiveBreakdown = false;
    public static bool isDebuffPerfectionism = false;

    public static bool sewerPicingSucc = false;

    public static bool isMorningMapMove = false;
    public static bool afterDaveDeathFirstSlum = false;

    public static bool iSRoadBattleStart = false;

    public static bool bgmChangeChecker = true;
    public static bool sewerBattleChecker = false;
    public static int posionDebuff = 0;

    public static bool symbolStrSanCheck = false;

    public static bool symbolOccultFirst = false;
    public static bool sewerSymbolOccultCheck = false;

    public static bool bookLookFirst = false;
    public static bool sewerBookOccultCheck = false;
    public static bool sewerBookSanCheck = false;

    public static bool journalFirstSanCheck = false;
    public static bool journalSanCheck = false;
    public static bool firstLookJournal = false;

    public static bool gainOldMap = false;

    public static bool safeDocument = false;
    public static bool insmusRhtoric = false;
    public static bool isInsmusMeetSewer = false;

    public static bool sewerBattleEndCheck = false;
    public static bool inSewerStealthFail = false;
    public static bool inSewerStealthSucc = false;
    public static bool startSewerBattle = false;

    public static bool sewerHouseIn = false;
    public static int sewerEnemyCounter = 0;

    public static bool potalWait = false;
    public static bool blackRechSuccCanTalck = false;
    public static bool blackDeftness = false;
    public static bool blackRhethic = false;

    public static bool blackFirstDialog = false;
    public static bool mapLooked = false;
    public static bool mapFirstDialog = false;
    public static bool sewerageKey = false;
    public static bool isTest = false;
    public static bool judgeSafeTryOpen = false;
    public static bool judgeCoatTryDisguise = false;
    public static bool gainCoat = false;
    public static bool gainMap = false;
    public static bool wearCoat = false;
    public static bool setIntelCharDetail = false;

    public static bool isOpenMap = false;
    public static bool secondisDirecting = false;
    public static bool storyDirecting = false;

    public static bool isSewerOffice = false;
    public static bool isSewerOfficeBrokenWindow = false;
    public static bool tryBrokenWindow = false;
    public static bool tryObserLokcer = false;
    public static bool moveSewerOfficeWindow = false;

    // 4일차MorningMove_2nd
    public static int battleEnemyCount = 0;
    public static bool morningMove2nd = false;
    public static bool morningMove4th = false;
    public static bool day4Case1San = false;
    public static bool day4Case1Health = false;

    public static bool sewerDoorOpen = false;
    public static bool sewerDoorOpenDeftness = false;
    public static bool sewerDoorOpenCheck = false;
    //
    public static bool morningMove_3rd = false;
    public static bool thirdDayDreamSan = false;
    public static bool thirdDayAlbertDialog = false;
    public static bool thirdDayPoliceADialog = false;
    public static bool thirdDayPoliceBDialog = false;
    public static bool thirdDayAlanDialog = false;
    //2일차
    public static bool barDrinkSanCheck = false;
    public static bool barMove = false;
    public static bool barArrive = false;

    public static bool daveDeathKeyword = false;
    public static bool albertDeathKeyword = false;
    public static bool swainDeathKeyword = false;

    public static bool albertFirstAfterEvent = false;

    public static bool daveSafeOpen = false;
    public static bool daveAfterSafe = false;
    public static bool daveAfterBreclet = false;
    public static bool daveCheck = false;

    public static bool secondDayDialog = false;

    public static bool afterSanCheck = false;
    public static bool case2IntCheck = false;
    public static bool case2SanCheck = false;

    public static bool swainDie = false;
    public static bool kaneSlimeSan = false;

    //낮 강제 이벤트
    public static bool noonMorningMove = false;
    public static bool secondDetectiveNoonEvent = false;
    public static bool noonEventMovetoClient = false;
    public static bool noonEventClientsFirst = false;
    public static bool noonEventAccessAuthorization = false;
    public static bool witnessFirstDailog = false;
    public static bool doorPoliceFirstDailog = false;
    public static bool noonEventFirstDoor = false;
    public static bool noonEventSanCheck = false;
    public static bool insidePoliceFirstDailog = false;
    public static bool deadBodyFirstLook = false;
    public static bool deadBodyLookMissingDialog = false;
    public static bool deadBodyLookFishySmellDialog = false;

    public static bool deadBodyMedicine = false;
    public static bool deadBodyObservational = false;
    public static bool deadBodyAnalyzing = false;
    public static bool deadBodyDeftness = false;
    public static bool deadBodyGotoSwere = false;

    public static bool siteAnalyzing = false;
    //Sanity
    public static int debuff = 0;
    public static bool thinWallets = false;
    public static bool carelessness = false;
    public static bool debilitation = false;
    public static bool dizziness = false;
    public static bool sprains = false;
    public static bool unlucky = false;
    public static bool musclePain = false;
    public static bool migraines = false;
    public static bool eyeDisease = false;

    public static bool masochism = false;
    public static bool careless = false;
    public static bool mentalWeakness = false;
    public static bool helplessness = false;
    public static bool extravagant = false;
    public static bool panicAttack = false;
    public static bool medicaldistrust = false;
    public static bool hallucinations = false;
    public static bool tightwad = false;

    public static bool nightPhobia = false;
    public static bool deafness = false;
    public static bool visionLoss = false;
    public static bool shortTempered = false;
    public static bool dyslexia = false;
    public static bool drugPhobia = false;
    public static bool cognitiveDisorder = false;
    public static bool cognitiveBreakdown = false;
    public static bool homicidalImpulse = false;
    public static bool perfectionism = false;

    //Inventory
    public static bool isItemUI = false;
    public static bool isUseSafe = false;

    public static string nowItem = "";
    public static int bullet = 0;
    public static int shotgun = 0;
    public static int rifle = 0;
    public static int revolver = 0;
    public static int smallPistol = 0;
    public static int bat = 0;
    public static int axe = 0;
    public static int molotov = 0;
    public static int dagger = 0;

    public static int pistolAmmo = 0;
    public static int rifleAmmo = 0;
    public static int shotgunAmmo = 0;

    public static int firstaidkit = 0;
    public static int bandages = 0;
    public static int painkillers = 0;

    public static bool conntectMouse = false;

    //SoundManager
    public static float soundVolume = 0.5f;
    public static float sfxVolume = 0.5f;
    public static bool workSound = false;

    //Battle
    public static bool battleSan = false;
    public static bool isBattleBeforeDialog = false;
    public static bool afterBattle = false;
    public static bool isBattleRollet = false;
    public static bool endBattle = false;
    public static bool roadBattleEnd = false;

    // Map
    public static string nowPlace = "DetectiveOffice";
    public static string potalPlace = "";

    //keyword
    public static string keywordMain;
    public static string keywordUpper;
    public static string keywordDowner;

    //Directing
    public static bool isDirecting = false;
    public static bool firstClientsHouse = false;
    public static bool firstClientsHouseEnd = false;

    //1일차 술집
    public static bool isBar = false;
    public static bool maeveFirstDialog = false;
    public static bool workerFirstDialog = false;

    //1일차 빈민가
    public static bool daveFirstDialog = false;

    //1일차 경찰서
    public static bool aFirstDialog = false;
    public static bool bFirstDialog = false;
    public static bool albertFirstDialog = false;

    //1일차 병원
    public static bool alanFirstDialog = false;
    public static bool kateDisapear = false;
    public static bool kateNoonFirstDialog = false;
    public static bool alanNoonFirstDialog = false;

    //1일차 강가
    public static bool kaneFirstDialog = false;
    public static bool sewerFirstDialog = false;
    public static bool sewerObservation = false;
    public static bool sewerObservationEnd = false;
    public static bool sewerSan = false;

    //1일차 대학
    public static bool univFirstDialog = false;

    //1일차 신문사
    public static bool swainFirstDialog = false;
    public static bool isSwainTalkMissingpeople = false;

    //1일차 의뢰자의 집
    public static bool aidenFirstDialog = false;
    public static bool aidenObservation = false;
    public static bool aidenMedicine = false;
    public static bool aidenPsychotherapy = false;
    public static bool ellaEndDialog = false;
    public static bool isFirstDoor = false;
    public static bool isFirst2st = false;
    public static bool isFirstRoom = false;
    public static bool getSafe = false;
    public static bool strDialogOn = false;
    public static int aidenKeywordCount = 0;
    public static bool failTwoKeyword = false;

    //1일차 오전 탐정사무소
    public static bool endDemo = false;
    public static bool endDay = false;
    public static bool connectKey = false;
    public static bool fstDetectiveEndFirstDialog;
    public static bool fstDetectiveNockDialog;
    public static bool fstDetectiveAfterSelect;
    public static bool fstDetectiveAfterSelectNo;
    public static bool fstDetectiveEndSelect;

    //Select
    public static bool selectionOn = false;

    //Judge
    public static bool fstDetectiveNewspaper = false;

    //Battle
    public static string battleWeapon = "";
    public static int nowSmallPistol = 0;
    public static int nowRevolver = 0;
    public static int nowRifle = 0;
    public static int nowShotgun = 0;
    public static bool isOpenUi = false;

    //Map
    public static int timeCount = 1;
    public static int fstDetectivTimeOn = 0;

    //Rollet
    public static bool isRollet;
    public static string condition = "Nomal";

    //dialog
    public static string selectObject;
    public static bool isSelect;
    public static bool cancelJudge;
    public static bool isKeyword;
    public static bool isJudge;
    public static string lookObject = "";
    public static string nowSelecter = "End"; // End_Look_Judge 존재
    public static bool isScene;
    public static bool isPresentation;
    public static bool isOption;
    public static bool endDialogLine;
    public static bool skipActive;
    public static float textDelay = 0.07f;
    public static bool isActiveDialog1;
    public static bool isActiveDialog2;

    // stat
    public static int str = 0;
    public static int intl = 0;
    public static int dex = 0;
    public static int hp;
    public static int mp;
    public static int san;
    public static int luk;
    public static int weal;
    public static int nowHP;
    public static int nowSan;

    //skill -str
    public static int strSkillPoint = 0;
    public static int martialArtsPoint = 25;
    public static int gunShotPoint = 15;
    public static int swordPoint = 20;
    public static int observationPoint = 20;
    public static int swimingPoint = 10;

    //skill -int
    public static int intSkillPoint = 0;
    public static int medicinePoint = 10;
    public static int analysisPoint = 30;
    public static int listeningPoint = 30;
    public static int psychotherapyPoint = 5;
    public static int occultPoint = 5;

    //skill -dex
    public static int dexSkillPoint = 0;
    public static int evasionPoint = 30;
    public static int deftnessPoint = 20;
    public static int rhetoricPoint = 20;
    public static int stealthPoint = 15;
    public static int disguisePoint = 10;

    [SerializeField] string csvFileName;
    Dictionary<int, Dialog> dialogDic = new Dictionary<int, Dialog>();
    public static bool isFinish;
    static public DataBaseManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        isPresentation = false;

    }

    public string[] varName;
    public float[] var;

    public string[] switchName;
    public bool[] switches;

    //Event
    // 현재 획득한 사건정보가지수
    // 행성대직렬 현재 페이지
    //행성대직렬 정보
    //인물정보
    public static int nowPageElla = 1;
    public static int nowPageAiden = 1;
    public static int nowPageCane = 1;
    public static int nowPageAlbert = 1;
    public static int nowPageMeiv = 1;
    public static int nowPageBlackWell = 1;
    public static int nowPageSwain = 1;

    public static int gainCharacterInt = 0;

    public static int buttonCountElla = -1;
    public static int buttonCountAiden = -1;
    public static int buttonCountCane = -1;
    public static int buttonCountAlbert = -1;
    public static int buttonCountMeiv = -1;
    public static int buttonCountBlackWell = -1;
    public static int buttonCountSwain = -1;

    public static bool intelAiden1 = false;
    public static bool intelAiden2 = false;
    public static bool intelAiden3 = false;
    public static bool intelAiden4 = false;
    public static bool intelAiden5 = false;
    public static bool intelAiden6 = false;

    public static bool intelElla1 = false;
    public static bool intelElla2 = false;
    public static bool intelElla3 = false;
    public static bool intelElla4 = false;
    public static bool intelElla5 = false;
    public static bool intelElla6 = false;

    public static bool intelSwain1 = false;
    public static bool intelSwain2 = false;
    public static bool intelSwain3 = false;
    public static bool intelSwain4 = false;
    public static bool intelSwain5 = false;
    public static bool intelSwain6 = false;

    public static bool intelCane1 = false;
    public static bool intelCane2 = false;
    public static bool intelCane3 = false;
    public static bool intelCane4 = false;
    public static bool intelCane5 = false;
    public static bool intelCane6 = false;

    public static bool intelAlbert1 = false;
    public static bool intelAlbert2 = false;
    public static bool intelAlbert3 = false;
    public static bool intelAlbert4 = false;
    public static bool intelAlbert5 = false;
    public static bool intelAlbert6 = false;

    public static bool intelMeiv1 = false;
    public static bool intelMeiv2 = false;
    public static bool intelMeiv3 = false;
    public static bool intelMeiv4 = false;
    public static bool intelMeiv5 = false;
    public static bool intelMeiv6 = false;

    public static bool intelBlackWell1 = false;
    public static bool intelBlackWell2 = false;
    public static bool intelBlackWell3 = false;
    public static bool intelBlackWell4 = false;
    public static bool intelBlackWell5 = false;
    public static bool intelBlackWell6 = false;

    //사건 정보
    public static int nowPagePlanetaryParade = 1;
    public static int nowPageNightmare = 1;
    public static int nowPageParanoia = 1;
    public static int nowPageInsomnia = 1;
    public static int nowPageSewerGhostStory = 1;
    public static int nowPageMissingPeople = 1;
    public static int nowPageFishySmell = 1;
    public static int nowPageRiverWaterQuality = 1;
    public static int nowPageCreepyEyes = 1;
    public static int nowPageSewerWorker = 1;
    public static int nowPageDave = 1;
    public static int nowPageSafe = 1;
    public static int nowPageMurderCase = 1;

    public static int gainEventInt = 0;

    public static int buttonCountPlanetaryParade = -1;
    public static int buttonCountNightmare = -1;
    public static int buttonCountParanoia = -1;
    public static int buttonCountInsomnia = -1;
    public static int buttonCountSewerGhostStory = -1;
    public static int buttonCountMissingPeople = -1;
    public static int buttonCountFishySmell = -1;
    public static int buttonCountRiverWaterQuality = -1;
    public static int buttonCountCreepyEyes = -1;
    public static int buttonCountSewerWorker = -1;
    public static int buttonCountDave = -1;
    public static int buttonCountSafe = -1;
    public static int buttonCountMurderCase = -1;

    public static bool intelPlanetaryParade1 = false;
    public static bool intelPlanetaryParade2 = false;
    public static bool intelPlanetaryParade3 = false;
    public static bool intelPlanetaryParade4 = false;
    public static bool intelPlanetaryParade5 = false;
    public static bool intelPlanetaryParade6 = false;

    public static bool intelNightmare1 = false;
    public static bool intelNightmare2 = false;
    public static bool intelNightmare3 = false;
    public static bool intelNightmare4 = false;
    public static bool intelNightmare5 = false;
    public static bool intelNightmare6 = false;

    public static bool intelParanoia1 = false;
    public static bool intelParanoia2 = false;
    public static bool intelParanoia3 = false;
    public static bool intelParanoia4 = false;
    public static bool intelParanoia5 = false;
    public static bool intelParanoia6 = false;

    public static bool intelInsomnia1 = false;
    public static bool intelInsomnia2 = false;
    public static bool intelInsomnia3 = false;
    public static bool intelInsomnia4 = false;
    public static bool intelInsomnia5 = false;
    public static bool intelInsomnia6 = false;

    public static bool intelSewerGhostStory1 = false;
    public static bool intelSewerGhostStory2 = false;
    public static bool intelSewerGhostStory3 = false;
    public static bool intelSewerGhostStory4 = false;
    public static bool intelSewerGhostStory5 = false;
    public static bool intelSewerGhostStory6 = false;

    public static bool intelMissingPeople1 = false;
    public static bool intelMissingPeople2 = false;
    public static bool intelMissingPeople3 = false;
    public static bool intelMissingPeople4 = false;
    public static bool intelMissingPeople5 = false;
    public static bool intelMissingPeople6 = false;

    public static bool intelFishySmell1 = false;
    public static bool intelFishySmell2 = false;
    public static bool intelFishySmell3 = false;
    public static bool intelFishySmell4 = false;
    public static bool intelFishySmell5 = false;
    public static bool intelFishySmell6 = false;

    public static bool intelRiverWaterQuality1 = false;
    public static bool intelRiverWaterQuality2 = false;
    public static bool intelRiverWaterQuality3 = false;
    public static bool intelRiverWaterQuality4 = false;
    public static bool intelRiverWaterQuality5 = false;
    public static bool intelRiverWaterQuality6 = false;

    public static bool intelCreepyEyes1 = false;
    public static bool intelCreepyEyes2 = false;
    public static bool intelCreepyEyes3 = false;
    public static bool intelCreepyEyes4 = false;
    public static bool intelCreepyEyes5 = false;
    public static bool intelCreepyEyes6 = false;

    public static bool intelSewerWorker1 = false;
    public static bool intelSewerWorker2 = false;
    public static bool intelSewerWorker3 = false;
    public static bool intelSewerWorker4 = false;
    public static bool intelSewerWorker5 = false;
    public static bool intelSewerWorker6 = false;

    public static bool intelDave1 = false;
    public static bool intelDave2 = false;
    public static bool intelDave3 = false;
    public static bool intelDave4 = false;
    public static bool intelDave5 = false;
    public static bool intelDave6 = false;

    public static bool intelSafe1 = false;
    public static bool intelSafe2 = false;
    public static bool intelSafe3 = false;
    public static bool intelSafe4 = false;
    public static bool intelSafe5 = false;
    public static bool intelSafe6 = false;

    public static bool intelMurderCase1 = false;
    public static bool intelMurderCase2 = false;
    public static bool intelMurderCase3 = false;
    public static bool intelMurderCase4 = false;
    public static bool intelMurderCase5 = false;
    public static bool intelMurderCase6 = false;

    //장소정보
    public static int buttonCountDetectiveOffice = -1;
    public static int buttonCountHospital = -1;
    public static int buttonCountNewspaper = -1;
    public static int buttonCountQuestHouse = -1;
    public static int buttonCountStation = -1;
    public static int buttonCountSewer = -1;
    public static int buttonCountUniversity = -1;
    public static int buttonCountRiverside = -1;
    public static int buttonCountBar = -1;
    public static int buttonCountSlum = -1;
    public static int buttonCountPoliceOffice = -1;
    public static int buttonCountGunsmith = -1;
    public static int buttonCountSewageMaintenanceOffice = -1;
    public static int buttonCountWharf = -1;

    public static int gainPlaceInt = 0;

    public static int nowPageDetectiveOffice = 1;
    public static int nowPageHospital = 1;
    public static int nowPageNewspaper = 1;
    public static int nowPageQuestHouse = 1;
    public static int nowPageStation = 1;
    public static int nowPageSewer = 1;
    public static int nowPageUniversity = 1;
    public static int nowPageRiverside = 1;
    public static int nowPageBar = 1;
    public static int nowPageSlum = 1;
    public static int nowPagePoliceOffice = 1;
    public static int nowPageGunsmith = 1;
    public static int nowPageSewageMaintenanceOffice = 1;
    public static int nowPageWharf = 1;

    public static bool intelDetectiveOffice1 = false;
    public static bool intelDetectiveOffice2 = false;
    public static bool intelDetectiveOffice3 = false;
    public static bool intelDetectiveOffice4 = false;
    public static bool intelDetectiveOffice5 = false;
    public static bool intelDetectiveOffice6 = false;

    public static bool intelHospital1 = false;
    public static bool intelHospital2 = false;
    public static bool intelHospital3 = false;
    public static bool intelHospital4 = false;
    public static bool intelHospital5 = false;
    public static bool intelHospital6 = false;

    public static bool intelNewspaper1 = false;
    public static bool intelNewspaper2 = false;
    public static bool intelNewspaper3 = false;
    public static bool intelNewspaper4 = false;
    public static bool intelNewspaper5 = false;
    public static bool intelNewspaper6 = false;

    public static bool intelQuestHouse1 = false;
    public static bool intelQuestHouse2 = false;
    public static bool intelQuestHouse3 = false;
    public static bool intelQuestHouse4 = false;
    public static bool intelQuestHouse5 = false;
    public static bool intelQuestHouse6 = false;

    public static bool intelStation1 = false;
    public static bool intelStation2 = false;
    public static bool intelStation3 = false;
    public static bool intelStation4 = false;
    public static bool intelStation5 = false;
    public static bool intelStation6 = false;

    public static bool intelSewer1 = false;
    public static bool intelSewer2 = false;
    public static bool intelSewer3 = false;
    public static bool intelSewer4 = false;
    public static bool intelSewer5 = false;
    public static bool intelSewer6 = false;

    public static bool intelUniversity1 = false;
    public static bool intelUniversity2 = false;
    public static bool intelUniversity3 = false;
    public static bool intelUniversity4 = false;
    public static bool intelUniversity5 = false;
    public static bool intelUniversity6 = false;

    public static bool intelRiverside1 = false;
    public static bool intelRiverside2 = false;
    public static bool intelRiverside3 = false;
    public static bool intelRiverside4 = false;
    public static bool intelRiverside5 = false;
    public static bool intelRiverside6 = false;

    public static bool intelBar1 = false;
    public static bool intelBar2 = false;
    public static bool intelBar3 = false;
    public static bool intelBar4 = false;
    public static bool intelBar5 = false;
    public static bool intelBar6 = false;

    public static bool intelSlum1 = false;
    public static bool intelSlum2 = false;
    public static bool intelSlum3 = false;
    public static bool intelSlum4 = false;
    public static bool intelSlum5 = false;
    public static bool intelSlum6 = false;

    public static bool intelPoliceOffice1 = false;
    public static bool intelPoliceOffice2 = false;
    public static bool intelPoliceOffice3 = false;
    public static bool intelPoliceOffice4 = false;
    public static bool intelPoliceOffice5 = false;
    public static bool intelPoliceOffice6 = false;

    public static bool intelGunsmith1 = false;
    public static bool intelGunsmith2 = false;
    public static bool intelGunsmith3 = false;
    public static bool intelGunsmith4 = false;
    public static bool intelGunsmith5 = false;
    public static bool intelGunsmith6 = false;

    public static bool intelSewageMaintenanceOffice1 = false;
    public static bool intelSewageMaintenanceOffice2 = false;
    public static bool intelSewageMaintenanceOffice3 = false;
    public static bool intelSewageMaintenanceOffice4 = false;
    public static bool intelSewageMaintenanceOffice5 = false;
    public static bool intelSewageMaintenanceOffice6 = false;

    public static bool intelWharf1 = false;
    public static bool intelWharf2 = false;
    public static bool intelWharf3 = false;
    public static bool intelWharf4 = false;
    public static bool intelWharf5 = false;
    public static bool intelWharf6 = false;
    public void ResetData()
    {
        debuff = 0;
        masochism = false;
        mentalWeakness = false;
        helplessness = false;
        extravagant = false;
        panicAttack = false;
        medicaldistrust = false;
        hallucinations = false;
        tightwad = false;
        //battle
        isItemUI = false;
        isUseSafe = false;

        nowItem = "";
        bullet = 0;
        shotgun = 0;
        rifle = 0;
        revolver = 0;
        smallPistol = 0;
        bat = 0;
        axe = 0;
        molotov = 0;
        dagger = 0;

        pistolAmmo = 0;
        rifleAmmo = 0;
        shotgunAmmo = 0;

        firstaidkit = 0;
        bandages = 0;
        painkillers = 0;

        conntectMouse = false;

        battleSan = false;
        isBattleBeforeDialog = false;
        afterBattle = false;
        isBattleRollet = false;
        endBattle = false;
        //SoundManager

        workSound = false;
        //Judge


        // Map
        nowPlace = "DetectiveOffice";
        potalPlace = "";

        //Directing
        isDirecting = false;
        firstClientsHouse = false;
        //1일차 술집
        isBar = false;
        maeveFirstDialog = false;
        workerFirstDialog = false;
        //1일차 빈민가
        daveFirstDialog = false;

        //1일차 경찰서
        aFirstDialog = false;
        bFirstDialog = false;
        albertFirstDialog = false;
        //1일차 병원
        alanFirstDialog = false;
        kateDisapear = false;
        kateNoonFirstDialog = false;
        alanNoonFirstDialog = false;
        //1일차 강가
        kaneFirstDialog = false;
        sewerFirstDialog = false;
        sewerObservation = false;
        sewerObservationEnd = false;
        sewerSan = false;
        //1일차 대학
        univFirstDialog = false;
        //1일차 신문사
        swainFirstDialog = false;
        isSwainTalkMissingpeople = false;
        //1일차 의뢰자의 집
        aidenFirstDialog = false;
        aidenObservation = false;
        aidenMedicine = false;
        aidenPsychotherapy = false;
        ellaEndDialog = false;
        isFirstDoor = false;
        isFirst2st = false;
        isFirstRoom = false;
        getSafe = false;
        strDialogOn = false;
        aidenKeywordCount = 0;
        failTwoKeyword = false;

        //1일차 오전 탐정사무소
        endDemo = false;
        endDay = false;
        connectKey = false;
        fstDetectiveEndFirstDialog = false;
        fstDetectiveNockDialog = false;
        fstDetectiveAfterSelect = false;
        fstDetectiveAfterSelectNo = false;
        fstDetectiveEndSelect = false;
        //Select
        selectionOn = false;

        //Judge
        fstDetectiveNewspaper = false;


        //Battle
        isOpenUi = false;

        //Map
        timeCount = 1;
        fstDetectivTimeOn = 0;

        //Rollet
        isRollet = false;
        condition = "Nomal";

        //dialog
        selectObject = "";
        isSelect = false;
        cancelJudge = false;
        isKeyword = false;
        isJudge = false;
        lookObject = "";
        nowSelecter = "End"; // End_Look_Judge 존재
        isScene = false;
        isPresentation = false;
        isOption = false;
        endDialogLine = false;
        skipActive = false;
        textDelay = 0.07f;

        isActiveDialog1 = false;
        isActiveDialog2 = false;

        // stat
        str = 0;
        intl = 0;
        dex = 0;
        hp = 0;
        mp = 0;
        san = 0;
        luk = 0;
        weal = 0;

        nowHP = 0;
        nowSan = 0;

        //skill -str
        strSkillPoint = 0;
        martialArtsPoint = 25;
        gunShotPoint = 15;
        swordPoint = 20;
        observationPoint = 20;
        swimingPoint = 10;

        //skill -int
        intSkillPoint = 0;
        medicinePoint = 10;
        analysisPoint = 30;
        listeningPoint = 30;
        psychotherapyPoint = 5;
        occultPoint = 5;

        //skill -dex
        dexSkillPoint = 0;
        evasionPoint = 30;
        deftnessPoint = 20;
        rhetoricPoint = 20;
        stealthPoint = 15;
        disguisePoint = 10;

        nowPageElla = 1;
        nowPageAiden = 1;
        nowPageCane = 1;
        nowPageAlbert = 1;
        nowPageMeiv = 1;
        nowPageBlackWell = 1;
        nowPageSwain = 1;

        gainCharacterInt = 0;

        buttonCountElla = -1;
        buttonCountAiden = -1;
        buttonCountCane = -1;
        buttonCountAlbert = -1;
        buttonCountMeiv = -1;
        buttonCountBlackWell = -1;
        buttonCountSwain = -1;

        intelAiden1 = false;
        intelAiden2 = false;
        intelAiden3 = false;
        intelAiden4 = false;
        intelAiden5 = false;
        intelAiden6 = false;

        intelElla1 = false;
        intelElla2 = false;
        intelElla3 = false;
        intelElla4 = false;
        intelElla5 = false;
        intelElla6 = false;

        intelSwain1 = false;
        intelSwain2 = false;
        intelSwain3 = false;
        intelSwain4 = false;
        intelSwain5 = false;
        intelSwain6 = false;

        intelCane1 = false;
        intelCane2 = false;
        intelCane3 = false;
        intelCane4 = false;
        intelCane5 = false;
        intelCane6 = false;

        intelAlbert1 = false;
        intelAlbert2 = false;
        intelAlbert3 = false;
        intelAlbert4 = false;
        intelAlbert5 = false;
        intelAlbert6 = false;

        intelMeiv1 = false;
        intelMeiv2 = false;
        intelMeiv3 = false;
        intelMeiv4 = false;
        intelMeiv5 = false;
        intelMeiv6 = false;

        intelBlackWell1 = false;
        intelBlackWell2 = false;
        intelBlackWell3 = false;
        intelBlackWell4 = false;
        intelBlackWell5 = false;
        intelBlackWell6 = false;

        //사건 정보
        nowPagePlanetaryParade = 1;
        nowPageNightmare = 1;
        nowPageParanoia = 1;
        nowPageInsomnia = 1;
        nowPageSewerGhostStory = 1;
        nowPageMissingPeople = 1;
        nowPageFishySmell = 1;
        nowPageRiverWaterQuality = 1;
        nowPageCreepyEyes = 1;
        nowPageSewerWorker = 1;
        nowPageDave = 1;
        nowPageSafe = 1;
        nowPageMurderCase = 1;

        gainEventInt = 0;

        buttonCountPlanetaryParade = -1;
        buttonCountNightmare = -1;
        buttonCountParanoia = -1;
        buttonCountInsomnia = -1;
        buttonCountSewerGhostStory = -1;
        buttonCountMissingPeople = -1;
        buttonCountFishySmell = -1;
        buttonCountRiverWaterQuality = -1;
        buttonCountCreepyEyes = -1;
        buttonCountSewerWorker = -1;
        buttonCountDave = -1;
        buttonCountSafe = -1;
        buttonCountMurderCase = -1;

        intelPlanetaryParade1 = false;
        intelPlanetaryParade2 = false;
        intelPlanetaryParade3 = false;
        intelPlanetaryParade4 = false;
        intelPlanetaryParade5 = false;
        intelPlanetaryParade6 = false;

        intelNightmare1 = false;
        intelNightmare2 = false;
        intelNightmare3 = false;
        intelNightmare4 = false;
        intelNightmare5 = false;
        intelNightmare6 = false;

        intelParanoia1 = false;
        intelParanoia2 = false;
        intelParanoia3 = false;
        intelParanoia4 = false;
        intelParanoia5 = false;
        intelParanoia6 = false;

        intelInsomnia1 = false;
        intelInsomnia2 = false;
        intelInsomnia3 = false;
        intelInsomnia4 = false;
        intelInsomnia5 = false;
        intelInsomnia6 = false;

        intelSewerGhostStory1 = false;
        intelSewerGhostStory2 = false;
        intelSewerGhostStory3 = false;
        intelSewerGhostStory4 = false;
        intelSewerGhostStory5 = false;
        intelSewerGhostStory6 = false;

        intelMissingPeople1 = false;
        intelMissingPeople2 = false;
        intelMissingPeople3 = false;
        intelMissingPeople4 = false;
        intelMissingPeople5 = false;
        intelMissingPeople6 = false;

        intelFishySmell1 = false;
        intelFishySmell2 = false;
        intelFishySmell3 = false;
        intelFishySmell4 = false;
        intelFishySmell5 = false;
        intelFishySmell6 = false;

        intelRiverWaterQuality1 = false;
        intelRiverWaterQuality2 = false;
        intelRiverWaterQuality3 = false;
        intelRiverWaterQuality4 = false;
        intelRiverWaterQuality5 = false;
        intelRiverWaterQuality6 = false;

        intelCreepyEyes1 = false;
        intelCreepyEyes2 = false;
        intelCreepyEyes3 = false;
        intelCreepyEyes4 = false;
        intelCreepyEyes5 = false;
        intelCreepyEyes6 = false;

        intelSewerWorker1 = false;
        intelSewerWorker2 = false;
        intelSewerWorker3 = false;
        intelSewerWorker4 = false;
        intelSewerWorker5 = false;
        intelSewerWorker6 = false;

        intelDave1 = false;
        intelDave2 = false;
        intelDave3 = false;
        intelDave4 = false;
        intelDave5 = false;
        intelDave6 = false;

        intelSafe1 = false;
        intelSafe2 = false;
        intelSafe3 = false;
        intelSafe4 = false;
        intelSafe5 = false;
        intelSafe6 = false;

        intelMurderCase1 = false;
        intelMurderCase2 = false;
        intelMurderCase3 = false;
        intelMurderCase4 = false;
        intelMurderCase5 = false;
        intelMurderCase6 = false;

        //장소정보
        buttonCountDetectiveOffice = -1;
        buttonCountHospital = -1;
        buttonCountNewspaper = -1;
        buttonCountQuestHouse = -1;
        buttonCountStation = -1;
        buttonCountSewer = -1;
        buttonCountUniversity = -1;
        buttonCountRiverside = -1;
        buttonCountBar = -1;
        buttonCountSlum = -1;
        buttonCountPoliceOffice = -1;
        buttonCountGunsmith = -1;
        buttonCountSewageMaintenanceOffice = -1;
        buttonCountWharf = -1;

        gainPlaceInt = 0;

        nowPageDetectiveOffice = 1;
        nowPageHospital = 1;
        nowPageNewspaper = 1;
        nowPageQuestHouse = 1;
        nowPageStation = 1;
        nowPageSewer = 1;
        nowPageUniversity = 1;
        nowPageRiverside = 1;
        nowPageBar = 1;
        nowPageSlum = 1;
        nowPagePoliceOffice = 1;
        nowPageGunsmith = 1;
        nowPageSewageMaintenanceOffice = 1;
        nowPageWharf = 1;

        intelDetectiveOffice1 = false;
        intelDetectiveOffice2 = false;
        intelDetectiveOffice3 = false;
        intelDetectiveOffice4 = false;
        intelDetectiveOffice5 = false;
        intelDetectiveOffice6 = false;

        intelHospital1 = false;
        intelHospital2 = false;
        intelHospital3 = false;
        intelHospital4 = false;
        intelHospital5 = false;
        intelHospital6 = false;

        intelNewspaper1 = false;
        intelNewspaper2 = false;
        intelNewspaper3 = false;
        intelNewspaper4 = false;
        intelNewspaper5 = false;
        intelNewspaper6 = false;

        intelQuestHouse1 = false;
        intelQuestHouse2 = false;
        intelQuestHouse3 = false;
        intelQuestHouse4 = false;
        intelQuestHouse5 = false;
        intelQuestHouse6 = false;

        intelStation1 = false;
        intelStation2 = false;
        intelStation3 = false;
        intelStation4 = false;
        intelStation5 = false;
        intelStation6 = false;

        intelSewer1 = false;
        intelSewer2 = false;
        intelSewer3 = false;
        intelSewer4 = false;
        intelSewer5 = false;
        intelSewer6 = false;

        intelUniversity1 = false;
        intelUniversity2 = false;
        intelUniversity3 = false;
        intelUniversity4 = false;
        intelUniversity5 = false;
        intelUniversity6 = false;

        intelRiverside1 = false;
        intelRiverside2 = false;
        intelRiverside3 = false;
        intelRiverside4 = false;
        intelRiverside5 = false;
        intelRiverside6 = false;

        intelBar1 = false;
        intelBar2 = false;
        intelBar3 = false;
        intelBar4 = false;
        intelBar5 = false;
        intelBar6 = false;

        intelSlum1 = false;
        intelSlum2 = false;
        intelSlum3 = false;
        intelSlum4 = false;
        intelSlum5 = false;
        intelSlum6 = false;

        intelPoliceOffice1 = false;
        intelPoliceOffice2 = false;
        intelPoliceOffice3 = false;
        intelPoliceOffice4 = false;
        intelPoliceOffice5 = false;
        intelPoliceOffice6 = false;

        intelGunsmith1 = false;
        intelGunsmith2 = false;
        intelGunsmith3 = false;
        intelGunsmith4 = false;
        intelGunsmith5 = false;
        intelGunsmith6 = false;

        intelSewageMaintenanceOffice1 = false;
        intelSewageMaintenanceOffice2 = false;
        intelSewageMaintenanceOffice3 = false;
        intelSewageMaintenanceOffice4 = false;
        intelSewageMaintenanceOffice5 = false;
        intelSewageMaintenanceOffice6 = false;

        intelWharf1 = false;
        intelWharf2 = false;
        intelWharf3 = false;
        intelWharf4 = false;
        intelWharf5 = false;
        intelWharf6 = false;
    }
}
