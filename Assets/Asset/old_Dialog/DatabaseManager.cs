using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DataBaseManager : MonoBehaviour
{
    //Judge
    public static bool fst_Detective_isNewspaper = false;


    //Battle



    //Map
    public static int TimeCount = 1;


    //Rollet
    public static string Condition = "Nomal";


    //dialog
    public static bool isSelect;
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
    public static int listeningPoint = 20;
    public static int swimingPoint = 10;

    //skill -int
    public static int intSkillPoint = 0;
    public static int medicinePoint = 10;
    public static int analysisPoint = 30;
    public static int psychologyPoint = 5;
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
    public static int NowPage_PlanetarySequence = 1;
    public static int NowPage_Nightmare = 1;
    public static int NowPage_Paranoia = 1;
    public static int NowPage_Insomnia = 1;
    public static int NowPage_SewerGhostStory = 1;
    public static int NowPage_MissingPeople = 1;
    public static int NowPage_FishySmell = 1;
    public static int NowPage_RiverWaterQuality = 1;
    public static int NowPage_CreepyEyes = 1;
    public static int NowPage_SewerWorker = 1;
    public static int NowPage_Safe = 1;
    public static int NowPage_SuddenChange = 1;

    public static int GainEventInt = 0;

    public static int ButtonCount_PlanetarySequence = -1;
    public static int ButtonCount_Nightmare = -1;
    public static int ButtonCount_Paranoia = -1;
    public static int ButtonCount_Insomnia = -1;
    public static int ButtonCount_SewerGhostStory = -1;
    public static int ButtonCount_MissingPeople = -1;
    public static int ButtonCount_FishySmell = -1;
    public static int ButtonCount_RiverWaterQuality = -1;
    public static int ButtonCount_CreepyEyes = -1;
    public static int ButtonCount_SewerWorker = -1;
    public static int ButtonCount_Safe = -1;
    public static int ButtonCount_SuddenChange = -1;



    public static bool Intel_PlanetarySequence1 = false;
    public static bool Intel_PlanetarySequence2 = false;
    public static bool Intel_PlanetarySequence3 = false;
    public static bool Intel_PlanetarySequence4 = false;
    public static bool Intel_PlanetarySequence5 = false;
    public static bool Intel_PlanetarySequence6 = false;

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

    public static bool Intel_Safe1 = false;
    public static bool Intel_Safe2 = false;
    public static bool Intel_Safe3 = false;
    public static bool Intel_Safe4 = false;
    public static bool Intel_Safe5 = false;
    public static bool Intel_Safe6 = false;

    public static bool Intel_SuddenChange1 = false;
    public static bool Intel_SuddenChange2 = false;
    public static bool Intel_SuddenChange3 = false;
    public static bool Intel_SuddenChange4 = false;
    public static bool Intel_SuddenChange5 = false;
    public static bool Intel_SuddenChange6 = false;

    //장소정보

    public static int ButtonCount_DetectiveOffice = -1;
    public static int ButtonCount_Hospital = -1;
    public static int ButtonCount_Newspaper = -1;
    public static int ButtonCount_QuestHouse = -1;
    public static int ButtonCount_Station = -1;
    public static int ButtonCount_Restaurant = -1;
    public static int ButtonCount_University = -1;
    public static int ButtonCount_Riverside = -1;
    public static int ButtonCount_Bar = -1;
    public static int ButtonCount_Slum = -1;
    public static int ButtonCount_PoliceOffice = -1;
    public static int ButtonCount_Gunsmith = -1;
    public static int ButtonCount_SewageMaintenanceOffice = -1;

    public static int GainPlaceInt = 0;

    public static int NowPage_DetectiveOffice = 1;
    public static int NowPage_Hospital = 1;
    public static int NowPage_Newspaper = 1;
    public static int NowPage_QuestHouse = 1;
    public static int NowPage_Station = 1;
    public static int NowPage_Restaurant = 1;
    public static int NowPage_University = 1;
    public static int NowPage_Riverside = 1;
    public static int NowPage_Bar = 1;
    public static int NowPage_Slum = 1;
    public static int NowPage_PoliceOffice = 1;
    public static int NowPage_Gunsmith = 1;
    public static int NowPage_SewageMaintenanceOffice = 1;



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


    public static bool Intel_Restaurant1 = false;
    public static bool Intel_Restaurant2 = false;
    public static bool Intel_Restaurant3 = false;
    public static bool Intel_Restaurant4 = false;
    public static bool Intel_Restaurant5 = false;
    public static bool Intel_Restaurant6 = false;


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
}
