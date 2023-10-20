using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DataBaseManager : MonoBehaviour
{
    public static bool Set_IntelCharDetail = false;

    public static bool isOpenMap = false;
    public static bool secondisDirecting = false;
    public static bool StoryDirecting = false;


    //3일차
    public static bool ThirdDayDreamSan = false;
    public static bool ThirdDayAlbertDialog = false;
    public static bool ThirdDayPoliceADialog = false;
    public static bool ThirdDayPoliceBDialog = false;

    //2일차
    public static bool BarDrinkSanCheck = false;
    public static bool BarMove = false;
    public static bool BarArrive = false;

    public static bool DaveDeathKeyword = false;
    public static bool AlbertDeathKeyword = false;
    public static bool SwainDeathKeyword = false;


    public static bool AlbertFirstAfterEvent = false;

    public static bool DaveSafeOpen = false;
    public static bool DaveAfterSafe= false;
    public static bool DaveAfterBreclet = false;
    public static bool DaveCheck = false;

    public static bool SecondDayDialog = false;
   
    public static bool AfterSanCheck = false;
    public static bool Case2IntCheck = false;
    public static bool Case2SanCheck = false;

    public static bool SwainDIE = false;

    public static bool KaneSlimeSan = false;




    //낮 강제 이벤트
    public static bool NoonMorningMove = false;
    public static bool snd_Detective_NoonEvent = false;
    public static bool NoonEvent_MovetoClient = false;
    public static bool NoonEvent_ClientsFirst = false;
    public static bool NoonEvent_AccessAuthorization = false;
    public static bool Witness_FirstDailog = false;
    public static bool DoorPolice_FirstDailog = false;
    public static bool NoonEventFirstDoor = false;
    public static bool NoonEventSanCheck = false;
    public static bool InsidePolice_FirstDailog = false;
    public static bool DeadBodyFirstLook = false;
    public static bool DeadBodyLook_MissingDialog = false;
    public static bool DeadBodyLook_FishySmellDialog = false;

    public static bool DeadBody_medicine = false;
    public static bool DeadBody_observational = false;
    public static bool DeadBody_Analyzing = false;
    public static bool DeadBody_deftness = false;
    public static bool DeadBody_GotoSwere = false;

    public static bool Site_Analyzing = false;
    //Sanity
    public static int Debuff = 0;

    public static bool ThinWallets = false;
    public static bool Carelessness = false;
    public static bool Debilitation = false;
    public static bool Dizziness = false;
    public static bool Sprains = false;
    public static bool Unlucky = false;
    public static bool MusclePain = false;
    public static bool Migraines = false;
    public static bool EyeDisease = false;
    
    public static bool Masochism = false;
    public static bool Careless = false;
    public static bool MentalWeakness = false;
    public static bool Helplessness = false;
    public static bool Extravagant = false;
    public static bool PanicAttack = false;
    public static bool Medicaldistrust = false;
    public static bool Hallucinations = false;
    public static bool Tightwad = false;

    public static bool NightPhobia = false;
    public static bool Deafness = false;
    public static bool VisionLoss = false;
    public static bool Short_Tempered = false;
    public static bool Dyslexia = false;
    public static bool DrugPhobia = false;
    public static bool CognitiveDisorder = false;
    public static bool CognitiveBreakdown = false;
    public static bool HomicidalImpulse = false;
    public static bool Perfectionism = false;
    //Inventory
    public static bool isItemUI = false;
    public static bool isUseSafe = false;

    public static string nowItem = "";
    public static int Bullet = 0;
    public static int Shotgun = 0;
    public static int Rifle = 0;
    public static int Revolver = 0;
    public static int SmallPistol = 0;
    public static int Bat = 0;
    public static int Axe = 0;
    public static int Molotov = 0;
    public static int Dagger = 0;

    public static int PistolAmmo = 0;
    public static int RifleAmmo = 0;
    public static int ShotgunAmmo = 0;

    public static int Firstaidkit = 0;
    public static int Bandages = 0;
    public static int Painkillers = 0;

    public static bool  ConntectMouse = false;

    //SoundManager

    public static float sound_Volume =0.5f;
    public static float sfx_Volume = 0.5f;
    public static bool workSound = false;
    //Judge

    //Battle
    public static bool Battle_San = false;
    public static bool isBattleBeforeDialog = false;
    public static bool AfterBattle = false;
    public static bool isBattleRollet = false;
    public static bool EndBattle = false;

    public static bool RoadBattleEnd = false;
    // Map
    public static string nowPlace = "DetectiveOffice";
    public static string PotalPlace = "";


    //keyword
    public static string keyword_main;
    public static string keyword_upper;
    public static string keyword_downer;

    //Directing
    public static bool isDirecting = false;
    public static bool firstClientsHouse = false;
    public static bool firstClientsHouseEnd = false;
    //1일차 술집
    public static bool isBar = false;
    public static bool Maeve_FirstDialog = false;
    public static bool Worker_FirstDialog = false;
    //1일차 빈민가
    public static bool Dave_FirstDialog = false;

    //1일차 경찰서
    public static bool A_FirstDialog = false;
    public static bool B_FirstDialog = false;
    public static bool Albert_FirstDialog = false;
    //1일차 병원
    public static bool Alan_FirstDialog = false;
    public static bool Kate_Disapear = false;
    public static bool Kate_NoonFirstDialog = false;
    public static bool Alan_NoonFirstDialog = false;
    //1일차 강가
    public static bool Kane_FirstDialog = false;
    public static bool Sewer_FirstDialog = false;
    public static bool Sewer_Observation = false;
    public static bool Sewer_ObservationEnd = false;
    public static bool Sewer_San = false;
    //1일차 대학
    public static bool Univ_FirstDialog = false;
    //1일차 신문사
    public static bool Swain_FirstDialog = false;
    public static bool Swain_isTalkMissingpeople = false;
    //1일차 의뢰자의 집
    public static bool Aiden_FirstDialog = false;
    public static bool Aiden_Observation = false;
    public static bool Aiden_medicine = false;
    public static bool Aiden_psychotherapy = false;
    public static bool Ella_EndDialog = false;
    public static bool isFirstDoor = false;
    public static bool isFirst2st = false;
    public static bool isFirstRoom = false;
    public static bool getSafe = false;
    public static bool StrDialogOn = false;
    public static int AidenKeywordCount = 0;
    public static bool FailTwoKeyword = false;


    //1일차 오전 탐정사무소
    public static bool EndDemo = false;
    public static bool EndDay = false;
    public static bool ConnectKey = false;
    public static bool fst_Detective_EndFirstDialog;
    public static bool fst_Detective_NockDialog;
    public static bool fst_Detective_AfterSelect;
    public static bool fst_Detective_AfterSelectNo;
    public static bool fst_Detective_EndSelect;
    //Select
    public static bool SelectionOn = false;

    //Judge
    public static bool fst_Detective_isNewspaper = false;


    //Battle
    public static string BattleWeapon = "";
    public static int nowSmallPistol = 0;
    public static int nowRevolver = 0;
    public static int nowRifle = 0;
    public static int nowShotgun = 0;


    public static bool isOpenUi = false;

    //Map
    public static int TimeCount = 1;
    public static int fst_Detectiv_TimeOn = 0;

    //Rollet
    public static bool isRollet;
    public static string Condition = "Nomal";


    //dialog
    public static string Select_Object;
    public static bool isSelect;
    public static bool CancelJudge;
    public static bool isKeyword;
    public static bool isJudge;
    public static string LookObject = "";
    public static string NowSelecter = "End"; // End_Look_Judge 존재
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
    public static int ObservationPoint = 20;
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
    public static int DisguisePoint = 10;



    [SerializeField] string csv_FileName;

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

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;


    // Intel Database

    //Event
    // 현재 획득한 사건정보가지수
    // 행성대직렬 현재 페이지
    //행성대직렬 정보

    //인물정보

    public static int NowPage_Ella = 1;
    public static int NowPage_Aiden = 1;
    public static int NowPage_Cane = 1;
    public static int NowPage_Albert = 1;
    public static int NowPage_Meiv = 1;
    public static int NowPage_BlackWell = 1;
    public static int NowPage_Swain = 1;

    public static int GainCharacterInt = 0;

    public static int ButtonCount_Ella = -1;
    public static int ButtonCount_Aiden = -1;
    public static int ButtonCount_Cane = -1;
    public static int ButtonCount_Albert = -1;
    public static int ButtonCount_Meiv = -1;
    public static int ButtonCount_BlackWell = -1;
    public static int ButtonCount_Swain = -1;

    public static bool Intel_Aiden1 = false;
    public static bool Intel_Aiden2 = false;
    public static bool Intel_Aiden3 = false;
    public static bool Intel_Aiden4 = false;
    public static bool Intel_Aiden5 = false;
    public static bool Intel_Aiden6 = false;

    public static bool Intel_Ella1 = false;
    public static bool Intel_Ella2 = false;
    public static bool Intel_Ella3 = false;
    public static bool Intel_Ella4 = false;
    public static bool Intel_Ella5 = false;
    public static bool Intel_Ella6 = false;

    public static bool Intel_Swain1 = false;
    public static bool Intel_Swain2 = false;
    public static bool Intel_Swain3 = false;
    public static bool Intel_Swain4 = false;
    public static bool Intel_Swain5 = false;
    public static bool Intel_Swain6 = false;

    public static bool Intel_Cane1 = false;
    public static bool Intel_Cane2 = false;
    public static bool Intel_Cane3 = false;
    public static bool Intel_Cane4 = false;
    public static bool Intel_Cane5 = false;
    public static bool Intel_Cane6 = false;

    public static bool Intel_Albert1 = false;
    public static bool Intel_Albert2 = false;
    public static bool Intel_Albert3 = false;
    public static bool Intel_Albert4 = false;
    public static bool Intel_Albert5 = false;
    public static bool Intel_Albert6 = false;

    public static bool Intel_Meiv1 = false;
    public static bool Intel_Meiv2 = false;
    public static bool Intel_Meiv3 = false;
    public static bool Intel_Meiv4 = false;
    public static bool Intel_Meiv5 = false;
    public static bool Intel_Meiv6 = false;

    public static bool Intel_BlackWell1 = false;
    public static bool Intel_BlackWell2 = false;
    public static bool Intel_BlackWell3 = false;
    public static bool Intel_BlackWell4 = false;
    public static bool Intel_BlackWell5 = false;
    public static bool Intel_BlackWell6 = false;

    //사건 정보
    public static int NowPage_PlanetaryParade = 1;
    public static int NowPage_Nightmare = 1;
    public static int NowPage_Paranoia = 1;
    public static int NowPage_Insomnia = 1;
    public static int NowPage_SewerGhostStory = 1;
    public static int NowPage_MissingPeople = 1;
    public static int NowPage_FishySmell = 1;
    public static int NowPage_RiverWaterQuality = 1;
    public static int NowPage_CreepyEyes = 1;
    public static int NowPage_SewerWorker = 1;
    public static int NowPage_Dave = 1;
    public static int NowPage_Safe = 1;
    public static int NowPage_MurderCase = 1;

    public static int GainEventInt = 0;

    public static int ButtonCount_PlanetaryParade = -1;
    public static int ButtonCount_Nightmare = -1;
    public static int ButtonCount_Paranoia = -1;
    public static int ButtonCount_Insomnia = -1;
    public static int ButtonCount_SewerGhostStory = -1;
    public static int ButtonCount_MissingPeople = -1;
    public static int ButtonCount_FishySmell = -1;
    public static int ButtonCount_RiverWaterQuality = -1;
    public static int ButtonCount_CreepyEyes = -1;
    public static int ButtonCount_SewerWorker = -1;
    public static int ButtonCount_Dave = -1;
    public static int ButtonCount_Safe = -1;
    public static int ButtonCount_MurderCase = -1;



    public static bool Intel_PlanetaryParade1 = false;
    public static bool Intel_PlanetaryParade2 = false;
    public static bool Intel_PlanetaryParade3 = false;
    public static bool Intel_PlanetaryParade4 = false;
    public static bool Intel_PlanetaryParade5 = false;
    public static bool Intel_PlanetaryParade6 = false;

    public static bool Intel_Nightmare1 = false;
    public static bool Intel_Nightmare2 = false;
    public static bool Intel_Nightmare3 = false;
    public static bool Intel_Nightmare4 = false;
    public static bool Intel_Nightmare5 = false;
    public static bool Intel_Nightmare6 = false;

    public static bool Intel_Paranoia1 = false;
    public static bool Intel_Paranoia2 = false;
    public static bool Intel_Paranoia3 = false;
    public static bool Intel_Paranoia4 = false;
    public static bool Intel_Paranoia5 = false;
    public static bool Intel_Paranoia6 = false;

    public static bool Intel_Insomnia1 = false;
    public static bool Intel_Insomnia2 = false;
    public static bool Intel_Insomnia3 = false;
    public static bool Intel_Insomnia4 = false;
    public static bool Intel_Insomnia5 = false;
    public static bool Intel_Insomnia6 = false;

    public static bool Intel_SewerGhostStory1 = false;
    public static bool Intel_SewerGhostStory2 = false;
    public static bool Intel_SewerGhostStory3 = false;
    public static bool Intel_SewerGhostStory4 = false;
    public static bool Intel_SewerGhostStory5 = false;
    public static bool Intel_SewerGhostStory6 = false;


    public static bool Intel_MissingPeople1 = false;
    public static bool Intel_MissingPeople2 = false;
    public static bool Intel_MissingPeople3 = false;
    public static bool Intel_MissingPeople4 = false;
    public static bool Intel_MissingPeople5 = false;
    public static bool Intel_MissingPeople6 = false;

    public static bool Intel_FishySmell1 = false;
    public static bool Intel_FishySmell2 = false;
    public static bool Intel_FishySmell3 = false;
    public static bool Intel_FishySmell4 = false;
    public static bool Intel_FishySmell5 = false;
    public static bool Intel_FishySmell6 = false;


    public static bool Intel_RiverWaterQuality1 = false;
    public static bool Intel_RiverWaterQuality2 = false;
    public static bool Intel_RiverWaterQuality3 = false;
    public static bool Intel_RiverWaterQuality4 = false;
    public static bool Intel_RiverWaterQuality5 = false;
    public static bool Intel_RiverWaterQuality6 = false;


    public static bool Intel_CreepyEyes1 = false;
    public static bool Intel_CreepyEyes2 = false;
    public static bool Intel_CreepyEyes3 = false;
    public static bool Intel_CreepyEyes4 = false;
    public static bool Intel_CreepyEyes5 = false;
    public static bool Intel_CreepyEyes6 = false;

    public static bool Intel_SewerWorker1 = false;
    public static bool Intel_SewerWorker2 = false;
    public static bool Intel_SewerWorker3 = false;
    public static bool Intel_SewerWorker4 = false;
    public static bool Intel_SewerWorker5 = false;
    public static bool Intel_SewerWorker6 = false;

    public static bool Intel_Dave1 = false;
    public static bool Intel_Dave2 = false;
    public static bool Intel_Dave3 = false;
    public static bool Intel_Dave4 = false;
    public static bool Intel_Dave5 = false;
    public static bool Intel_Dave6 = false;

    public static bool Intel_Safe1 = false;
    public static bool Intel_Safe2 = false;
    public static bool Intel_Safe3 = false;
    public static bool Intel_Safe4 = false;
    public static bool Intel_Safe5 = false;
    public static bool Intel_Safe6 = false;

    public static bool Intel_MurderCase1 = false;
    public static bool Intel_MurderCase2 = false;
    public static bool Intel_MurderCase3 = false;
    public static bool Intel_MurderCase4 = false;
    public static bool Intel_MurderCase5 = false;
    public static bool Intel_MurderCase6 = false;

    //장소정보

    public static int ButtonCount_DetectiveOffice = -1;
    public static int ButtonCount_Hospital = -1;
    public static int ButtonCount_Newspaper = -1;
    public static int ButtonCount_QuestHouse = -1;
    public static int ButtonCount_Station = -1;
    public static int ButtonCount_Sewer = -1;
    public static int ButtonCount_University = -1;
    public static int ButtonCount_Riverside = -1;
    public static int ButtonCount_Bar = -1;
    public static int ButtonCount_Slum = -1;
    public static int ButtonCount_PoliceOffice = -1;
    public static int ButtonCount_Gunsmith = -1;
    public static int ButtonCount_SewageMaintenanceOffice = -1;
    public static int ButtonCount_Wharf = -1;



    public static int GainPlaceInt = 0;

    public static int NowPage_DetectiveOffice = 1;
    public static int NowPage_Hospital = 1;
    public static int NowPage_Newspaper = 1;
    public static int NowPage_QuestHouse = 1;
    public static int NowPage_Station = 1;
    public static int NowPage_Sewer = 1;
    public static int NowPage_University = 1;
    public static int NowPage_Riverside = 1;
    public static int NowPage_Bar = 1;
    public static int NowPage_Slum = 1;
    public static int NowPage_PoliceOffice = 1;
    public static int NowPage_Gunsmith = 1;
    public static int NowPage_SewageMaintenanceOffice = 1;
    public static int NowPage_Wharf = 1;


    public static bool Intel_DetectiveOffice1 = false;
    public static bool Intel_DetectiveOffice2 = false;
    public static bool Intel_DetectiveOffice3 = false;
    public static bool Intel_DetectiveOffice4 = false;
    public static bool Intel_DetectiveOffice5 = false;
    public static bool Intel_DetectiveOffice6 = false;

    public static bool Intel_Hospital1 = false;
    public static bool Intel_Hospital2 = false;
    public static bool Intel_Hospital3 = false;
    public static bool Intel_Hospital4 = false;
    public static bool Intel_Hospital5 = false;
    public static bool Intel_Hospital6 = false;

    public static bool Intel_Newspaper1 = false;
    public static bool Intel_Newspaper2 = false;
    public static bool Intel_Newspaper3 = false;
    public static bool Intel_Newspaper4 = false;
    public static bool Intel_Newspaper5 = false;
    public static bool Intel_Newspaper6 = false;

    public static bool Intel_QuestHouse1 = false;
    public static bool Intel_QuestHouse2 = false;
    public static bool Intel_QuestHouse3 = false;
    public static bool Intel_QuestHouse4 = false;
    public static bool Intel_QuestHouse5 = false;
    public static bool Intel_QuestHouse6 = false;


    public static bool Intel_Station1 = false;
    public static bool Intel_Station2 = false;
    public static bool Intel_Station3 = false;
    public static bool Intel_Station4 = false;
    public static bool Intel_Station5 = false;
    public static bool Intel_Station6 = false;


    public static bool Intel_Sewer1 = false;
    public static bool Intel_Sewer2 = false;
    public static bool Intel_Sewer3 = false;
    public static bool Intel_Sewer4 = false;
    public static bool Intel_Sewer5 = false;
    public static bool Intel_Sewer6 = false;


    public static bool Intel_University1 = false;
    public static bool Intel_University2 = false;
    public static bool Intel_University3 = false;
    public static bool Intel_University4 = false;
    public static bool Intel_University5 = false;
    public static bool Intel_University6 = false;

    public static bool Intel_Riverside1 = false;
    public static bool Intel_Riverside2 = false;
    public static bool Intel_Riverside3 = false;
    public static bool Intel_Riverside4 = false;
    public static bool Intel_Riverside5 = false;
    public static bool Intel_Riverside6 = false;

    public static bool Intel_Bar1 = false;
    public static bool Intel_Bar2 = false;
    public static bool Intel_Bar3 = false;
    public static bool Intel_Bar4 = false;
    public static bool Intel_Bar5 = false;
    public static bool Intel_Bar6 = false;


    public static bool Intel_Slum1 = false;
    public static bool Intel_Slum2 = false;
    public static bool Intel_Slum3 = false;
    public static bool Intel_Slum4 = false;
    public static bool Intel_Slum5 = false;
    public static bool Intel_Slum6 = false;


    public static bool Intel_PoliceOffice1 = false;
    public static bool Intel_PoliceOffice2 = false;
    public static bool Intel_PoliceOffice3 = false;
    public static bool Intel_PoliceOffice4 = false;
    public static bool Intel_PoliceOffice5 = false;
    public static bool Intel_PoliceOffice6 = false;

    public static bool Intel_Gunsmith1 = false;
    public static bool Intel_Gunsmith2 = false;
    public static bool Intel_Gunsmith3 = false;
    public static bool Intel_Gunsmith4 = false;
    public static bool Intel_Gunsmith5 = false;
    public static bool Intel_Gunsmith6 = false;

    public static bool Intel_SewageMaintenanceOffice1 = false;
    public static bool Intel_SewageMaintenanceOffice2 = false;
    public static bool Intel_SewageMaintenanceOffice3 = false;
    public static bool Intel_SewageMaintenanceOffice4 = false;
    public static bool Intel_SewageMaintenanceOffice5 = false;
    public static bool Intel_SewageMaintenanceOffice6 = false;

    public static bool Intel_Wharf1 = false;
    public static bool Intel_Wharf2 = false;
    public static bool Intel_Wharf3 = false;
    public static bool Intel_Wharf4 = false;
    public static bool Intel_Wharf5 = false;
    public static bool Intel_Wharf6 = false;











    public void ResetData()
    {

        Debuff = 0;
        Masochism = false;
        MentalWeakness = false;
        Helplessness = false;
        Extravagant = false;
        PanicAttack = false;
        Medicaldistrust = false;
        Hallucinations = false;
        Tightwad = false;
        //battle
        isItemUI = false;
      isUseSafe = false;

      nowItem = "";
      Bullet = 0;
      Shotgun = 0;
      Rifle = 0;
      Revolver = 0;
      SmallPistol = 0;
      Bat = 0;
      Axe = 0;
      Molotov = 0;
      Dagger = 0;

      PistolAmmo = 0;
      RifleAmmo = 0;
      ShotgunAmmo = 0;

      Firstaidkit = 0;
      Bandages = 0;
      Painkillers = 0;

      ConntectMouse = false;

      Battle_San = false;
      isBattleBeforeDialog = false;
      AfterBattle = false;
      isBattleRollet = false;
      EndBattle = false;
    //SoundManager

    workSound = false;
    //Judge


    // Map
nowPlace = "DetectiveOffice";
PotalPlace = "";



    //Directing
    isDirecting = false;
     firstClientsHouse = false;
    //1일차 술집
     isBar = false;
     Maeve_FirstDialog = false;
     Worker_FirstDialog = false;
    //1일차 빈민가
     Dave_FirstDialog = false;

    //1일차 경찰서
     A_FirstDialog = false;
     B_FirstDialog = false;
     Albert_FirstDialog = false;
    //1일차 병원
     Alan_FirstDialog = false;
     Kate_Disapear = false;
     Kate_NoonFirstDialog = false;
     Alan_NoonFirstDialog = false;
    //1일차 강가
     Kane_FirstDialog = false;
     Sewer_FirstDialog = false;
     Sewer_Observation = false;
     Sewer_ObservationEnd = false;
     Sewer_San = false;
    //1일차 대학
     Univ_FirstDialog = false;
    //1일차 신문사
     Swain_FirstDialog = false;
     Swain_isTalkMissingpeople = false;
    //1일차 의뢰자의 집
     Aiden_FirstDialog = false;
     Aiden_Observation = false;
     Aiden_medicine = false;
     Aiden_psychotherapy = false;
     Ella_EndDialog = false;
     isFirstDoor = false;
     isFirst2st = false;
     isFirstRoom = false;
     getSafe = false;
     StrDialogOn = false;
     AidenKeywordCount = 0;
     FailTwoKeyword = false;


    //1일차 오전 탐정사무소
     EndDemo = false;
     EndDay = false;
     ConnectKey = false;
        fst_Detective_EndFirstDialog = false;
        fst_Detective_NockDialog = false;
        fst_Detective_AfterSelect = false;
        fst_Detective_AfterSelectNo = false;
        fst_Detective_EndSelect = false;
        //Select
        SelectionOn = false;

    //Judge
     fst_Detective_isNewspaper = false;


    //Battle


     isOpenUi = false;

    //Map
     TimeCount = 1;
     fst_Detectiv_TimeOn = 0;

        //Rollet
        isRollet = false;
        Condition = "Nomal";


        //dialog
        Select_Object = "";
        isSelect = false;
        CancelJudge = false;
        isKeyword = false;
        isJudge = false;
        LookObject = "";
NowSelecter = "End"; // End_Look_Judge 존재
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
     hp =0;
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
     ObservationPoint = 20;
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
     DisguisePoint = 10;




    NowPage_Ella = 1;
     NowPage_Aiden = 1;
     NowPage_Cane = 1;
     NowPage_Albert = 1;
     NowPage_Meiv = 1;
     NowPage_BlackWell = 1;
     NowPage_Swain = 1;

     GainCharacterInt = 0;

     ButtonCount_Ella = -1;
     ButtonCount_Aiden = -1;
     ButtonCount_Cane = -1;
     ButtonCount_Albert = -1;
     ButtonCount_Meiv = -1;
     ButtonCount_BlackWell = -1;
     ButtonCount_Swain = -1;

     Intel_Aiden1 = false;
     Intel_Aiden2 = false;
     Intel_Aiden3 = false;
     Intel_Aiden4 = false;
     Intel_Aiden5 = false;
     Intel_Aiden6 = false;

     Intel_Ella1 = false;
     Intel_Ella2 = false;
     Intel_Ella3 = false;
     Intel_Ella4 = false;
     Intel_Ella5 = false;
     Intel_Ella6 = false;

     Intel_Swain1 = false;
     Intel_Swain2 = false;
     Intel_Swain3 = false;
     Intel_Swain4 = false;
     Intel_Swain5 = false;
     Intel_Swain6 = false;

     Intel_Cane1 = false;
     Intel_Cane2 = false;
     Intel_Cane3 = false;
     Intel_Cane4 = false;
     Intel_Cane5 = false;
     Intel_Cane6 = false;

     Intel_Albert1 = false;
     Intel_Albert2 = false;
     Intel_Albert3 = false;
     Intel_Albert4 = false;
     Intel_Albert5 = false;
     Intel_Albert6 = false;

     Intel_Meiv1 = false;
     Intel_Meiv2 = false;
     Intel_Meiv3 = false;
     Intel_Meiv4 = false;
     Intel_Meiv5 = false;
     Intel_Meiv6 = false;

     Intel_BlackWell1 = false;
     Intel_BlackWell2 = false;
     Intel_BlackWell3 = false;
     Intel_BlackWell4 = false;
     Intel_BlackWell5 = false;
     Intel_BlackWell6 = false;

    //사건 정보
     NowPage_PlanetaryParade = 1;
     NowPage_Nightmare = 1;
     NowPage_Paranoia = 1;
     NowPage_Insomnia = 1;
     NowPage_SewerGhostStory = 1;
     NowPage_MissingPeople = 1;
     NowPage_FishySmell = 1;
     NowPage_RiverWaterQuality = 1;
     NowPage_CreepyEyes = 1;
     NowPage_SewerWorker = 1;
        NowPage_Dave = 1;
        NowPage_Safe = 1;
     NowPage_MurderCase = 1;

     GainEventInt = 0;

     ButtonCount_PlanetaryParade = -1;
     ButtonCount_Nightmare = -1;
     ButtonCount_Paranoia = -1;
     ButtonCount_Insomnia = -1;
     ButtonCount_SewerGhostStory = -1;
     ButtonCount_MissingPeople = -1;
     ButtonCount_FishySmell = -1;
     ButtonCount_RiverWaterQuality = -1;
     ButtonCount_CreepyEyes = -1;
     ButtonCount_SewerWorker = -1;
        ButtonCount_Dave = -1;
        ButtonCount_Safe = -1;
     ButtonCount_MurderCase = -1;



     Intel_PlanetaryParade1 = false;
     Intel_PlanetaryParade2 = false;
     Intel_PlanetaryParade3 = false;
     Intel_PlanetaryParade4 = false;
     Intel_PlanetaryParade5 = false;
     Intel_PlanetaryParade6 = false;

     Intel_Nightmare1 = false;
     Intel_Nightmare2 = false;
     Intel_Nightmare3 = false;
     Intel_Nightmare4 = false;
     Intel_Nightmare5 = false;
     Intel_Nightmare6 = false;

     Intel_Paranoia1 = false;
     Intel_Paranoia2 = false;
     Intel_Paranoia3 = false;
     Intel_Paranoia4 = false;
     Intel_Paranoia5 = false;
     Intel_Paranoia6 = false;

     Intel_Insomnia1 = false;
     Intel_Insomnia2 = false;
     Intel_Insomnia3 = false;
     Intel_Insomnia4 = false;
     Intel_Insomnia5 = false;
     Intel_Insomnia6 = false;

     Intel_SewerGhostStory1 = false;
     Intel_SewerGhostStory2 = false;
     Intel_SewerGhostStory3 = false;
     Intel_SewerGhostStory4 = false;
     Intel_SewerGhostStory5 = false;
     Intel_SewerGhostStory6 = false;


     Intel_MissingPeople1 = false;
     Intel_MissingPeople2 = false;
     Intel_MissingPeople3 = false;
     Intel_MissingPeople4 = false;
     Intel_MissingPeople5 = false;
     Intel_MissingPeople6 = false;

     Intel_FishySmell1 = false;
     Intel_FishySmell2 = false;
     Intel_FishySmell3 = false;
     Intel_FishySmell4 = false;
     Intel_FishySmell5 = false;
     Intel_FishySmell6 = false;


     Intel_RiverWaterQuality1 = false;
     Intel_RiverWaterQuality2 = false;
     Intel_RiverWaterQuality3 = false;
     Intel_RiverWaterQuality4 = false;
     Intel_RiverWaterQuality5 = false;
     Intel_RiverWaterQuality6 = false;


     Intel_CreepyEyes1 = false;
     Intel_CreepyEyes2 = false;
     Intel_CreepyEyes3 = false;
     Intel_CreepyEyes4 = false;
     Intel_CreepyEyes5 = false;
     Intel_CreepyEyes6 = false;

     Intel_SewerWorker1 = false;
     Intel_SewerWorker2 = false;
     Intel_SewerWorker3 = false;
     Intel_SewerWorker4 = false;
     Intel_SewerWorker5 = false;
     Intel_SewerWorker6 = false;

        Intel_Dave1 = false;
        Intel_Dave2 = false;
        Intel_Dave3 = false;
        Intel_Dave4 = false;
        Intel_Dave5 = false;
        Intel_Dave6 = false;

        Intel_Safe1 = false;
     Intel_Safe2 = false;
     Intel_Safe3 = false;
     Intel_Safe4 = false;
     Intel_Safe5 = false;
     Intel_Safe6 = false;

     Intel_MurderCase1 = false;
     Intel_MurderCase2 = false;
     Intel_MurderCase3 = false;
     Intel_MurderCase4 = false;
     Intel_MurderCase5 = false;
     Intel_MurderCase6 = false;

    //장소정보

     ButtonCount_DetectiveOffice = -1;
     ButtonCount_Hospital = -1;
     ButtonCount_Newspaper = -1;
     ButtonCount_QuestHouse = -1;
     ButtonCount_Station = -1;
     ButtonCount_Sewer = -1;
     ButtonCount_University = -1;
     ButtonCount_Riverside = -1;
     ButtonCount_Bar = -1;
     ButtonCount_Slum = -1;
     ButtonCount_PoliceOffice = -1;
     ButtonCount_Gunsmith = -1;
     ButtonCount_SewageMaintenanceOffice = -1;
     ButtonCount_Wharf = -1;



     GainPlaceInt = 0;

     NowPage_DetectiveOffice = 1;
     NowPage_Hospital = 1;
     NowPage_Newspaper = 1;
     NowPage_QuestHouse = 1;
     NowPage_Station = 1;
     NowPage_Sewer = 1;
     NowPage_University = 1;
     NowPage_Riverside = 1;
     NowPage_Bar = 1;
     NowPage_Slum = 1;
     NowPage_PoliceOffice = 1;
     NowPage_Gunsmith = 1;
     NowPage_SewageMaintenanceOffice = 1;
     NowPage_Wharf = 1;


     Intel_DetectiveOffice1 = false;
     Intel_DetectiveOffice2 = false;
     Intel_DetectiveOffice3 = false;
     Intel_DetectiveOffice4 = false;
     Intel_DetectiveOffice5 = false;
     Intel_DetectiveOffice6 = false;

     Intel_Hospital1 = false;
     Intel_Hospital2 = false;
     Intel_Hospital3 = false;
     Intel_Hospital4 = false;
     Intel_Hospital5 = false;
     Intel_Hospital6 = false;

     Intel_Newspaper1 = false;
     Intel_Newspaper2 = false;
     Intel_Newspaper3 = false;
     Intel_Newspaper4 = false;
     Intel_Newspaper5 = false;
     Intel_Newspaper6 = false;

     Intel_QuestHouse1 = false;
     Intel_QuestHouse2 = false;
     Intel_QuestHouse3 = false;
     Intel_QuestHouse4 = false;
     Intel_QuestHouse5 = false;
     Intel_QuestHouse6 = false;


     Intel_Station1 = false;
     Intel_Station2 = false;
     Intel_Station3 = false;
     Intel_Station4 = false;
     Intel_Station5 = false;
     Intel_Station6 = false;


     Intel_Sewer1 = false;
     Intel_Sewer2 = false;
     Intel_Sewer3 = false;
     Intel_Sewer4 = false;
     Intel_Sewer5 = false;
     Intel_Sewer6 = false;


     Intel_University1 = false;
     Intel_University2 = false;
     Intel_University3 = false;
     Intel_University4 = false;
     Intel_University5 = false;
     Intel_University6 = false;

     Intel_Riverside1 = false;
     Intel_Riverside2 = false;
     Intel_Riverside3 = false;
     Intel_Riverside4 = false;
     Intel_Riverside5 = false;
     Intel_Riverside6 = false;

     Intel_Bar1 = false;
     Intel_Bar2 = false;
     Intel_Bar3 = false;
     Intel_Bar4 = false;
     Intel_Bar5 = false;
     Intel_Bar6 = false;


     Intel_Slum1 = false;
     Intel_Slum2 = false;
     Intel_Slum3 = false;
     Intel_Slum4 = false;
     Intel_Slum5 = false;
     Intel_Slum6 = false;


     Intel_PoliceOffice1 = false;
     Intel_PoliceOffice2 = false;
     Intel_PoliceOffice3 = false;
     Intel_PoliceOffice4 = false;
     Intel_PoliceOffice5 = false;
     Intel_PoliceOffice6 = false;

     Intel_Gunsmith1 = false;
     Intel_Gunsmith2 = false;
     Intel_Gunsmith3 = false;
     Intel_Gunsmith4 = false;
     Intel_Gunsmith5 = false;
     Intel_Gunsmith6 = false;

     Intel_SewageMaintenanceOffice1 = false;
     Intel_SewageMaintenanceOffice2 = false;
     Intel_SewageMaintenanceOffice3 = false;
     Intel_SewageMaintenanceOffice4 = false;
     Intel_SewageMaintenanceOffice5 = false;
     Intel_SewageMaintenanceOffice6 = false;

     Intel_Wharf1 = false;
     Intel_Wharf2 = false;
     Intel_Wharf3 = false;
     Intel_Wharf4 = false;
     Intel_Wharf5 = false;
     Intel_Wharf6 = false;
}
}
