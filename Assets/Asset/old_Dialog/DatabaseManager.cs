using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DataBaseManager : MonoBehaviour
{
   
    // stat
    public static int str;
    public static int intl;
    public static int dex;
    public static int hp;
    public static int mp;
    public static int san;
    public static int luk;
    public static int weal;


    //skill -str
    public static int strSkillPoint;
    public static int martialArtsPoint = 25;
    public static int gunShotPoint = 15;
    public static int swordPoint = 20;
    public static int listeningPoint = 20;
    public static int swimingPoint = 10;

    //skill -int
    public static int intSkillPoint;
    public static int medicinePoint = 10;
    public static int analysisPoint = 30;
    public static int psychologyPoint = 5;
    public static int psychotherapyPoint = 5;
    public static int occultPoint = 5;

    //skill -dex
    public static int dexSkillPoint;
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
