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



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
