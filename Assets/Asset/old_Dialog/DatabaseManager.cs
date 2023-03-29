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


    //skill
    public static int strSkillPoint;
    public static int martialArtsPoint = 25;


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
