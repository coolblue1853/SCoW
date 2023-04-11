using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DataBaseManager : MonoBehaviour
{
    //Rollet
    public static string Condition = "Nomal";


    //dialog
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
    public static int nowMP;


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

    static public DataBaseManager instance;

    [SerializeField] string csv_FileName;

    Dictionary<int, Dialog> dialogDic = new Dictionary<int, Dialog>();

    public static bool isFinish;

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
    public static int PlanetarySequenceNowPage;
    //행성대직렬 정보

    //인물정보

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


}
