using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Rollet : MonoBehaviour
{
    bool activeCheck = false;

    string Subject;
    string Sub_Dialog;
    public GameObject RolletSetUi;
    public GameObject RolletCheckUI;
    public GameObject RolletRollUI;


    public GameObject Bad_1;
    public GameObject Bad_10;
    public GameObject Good_1;
    public GameObject Good_10;
    public GameObject Bad_1_Rollet;
    public GameObject Bad_10_Rollet;
    public GameObject Good_1_Rollet;
    public GameObject Good_10_Rollet;


    public GameObject setUI;
    public GameObject activeUI;


    public float SkillPoint_1;
    public float SkillPoint_10;
    public float SuccesProbability;


    public TextMeshProUGUI Skill;
    public TextMeshProUGUI Point;
    public TextMeshProUGUI Conditon;
    public TextMeshProUGUI Expect;

    public TextMeshProUGUI NomalRollet_1t;
    public TextMeshProUGUI NomalRollet_10t;
    public TextMeshProUGUI BadRollet_1t;
    public TextMeshProUGUI BadRollet_10t;
    public TextMeshProUGUI GoodRollet_1t;
    public TextMeshProUGUI GoodRollet_10t;

    int skillPoint;
    int result_int;
    public TextMeshProUGUI result_string;
    public TextMeshProUGUI result_End;

    public string EnemySubject;
    public bool isClick;
    public bool isActiveRollet;
    private static Rollet instance = null;

    public GameObject EndButton;


    private void Update()
    {
        if (setUI.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            activeCheck = false;
            DataBaseManager.CancelJudge = true;
            RolletSetUi.SetActive(false);
            DataBaseManager.isRollet = false;
        }
        if (setUI.activeSelf == true && Input.GetKeyDown(KeyCode.E) && activeCheck== true)
        {
            activeCheck = false;
            activeRollet();
        }

        if (EndButton.activeSelf == true && Input.GetKeyDown(KeyCode.E))
        {

            EndRollet();
        }
        {
            if (isClick == false && isActiveRollet == true)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E))
                {
                    SoundManager.Instance.EndDice();
                    CancelInvoke();
                    isClick = true;
                    isActiveRollet = false;
                    Invoke("GetIntResult", 1f);
                }


            }
        }
    }
    void Awake()
        {
            isClick = false;
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
 

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static Rollet Instance
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
    public void setRollet(string skill, string point_sting, int point_int, string subject, string Enemy = "")
    {
        if(point_int >= 90)
        {
            point_int = 90;
        }
        if (point_int < 0)
        {
            point_int = 0;
        }
        EnemySubject = Enemy;
        SoundManager.Instance.pen_Line();
        DataBaseManager.isRollet = true;
        Subject = subject;
        Sub_Dialog = skill; // 나중에 다이얼로그 결과 전송시 사용
        ResetString();
        RolletSetUi.SetActive(true);
        Skill.text = skill;
        Point.text = point_sting + " : " + point_int;
        skillPoint = point_int;
        if (DataBaseManager.Condition == "Nomal")
        {
            ResetRolletDice();
            Conditon.text = "Condition : Nomal";

            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = SkillPoint_10 * 0.1f + SkillPoint_1 * 0.01f;
        }
        else if (DataBaseManager.Condition == "Good")
        {
            ResetRolletDice();
            Conditon.text = "Condition : Good";
            Good_1.SetActive(true);
            Good_1_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = SkillPoint_10 * 0.1f + (SkillPoint_1 * SkillPoint_1 + 2* SkillPoint_1*(10- SkillPoint_1)) * 0.001f;
        }
        else if(DataBaseManager.Condition == "Best")
        {
            ResetRolletDice();
            Conditon.text = "Condition : Best";
            Good_10.SetActive(true);
            Good_10_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = (SkillPoint_10* SkillPoint_10 + 2* SkillPoint_10*(10- SkillPoint_10)) * 0.01f + SkillPoint_1*0.001f;
        }
        else if(DataBaseManager.Condition == "Bad")
        {
            ResetRolletDice();
            Conditon.text = "Condition : Bad";
            Bad_1.SetActive(true);
            Bad_1_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = SkillPoint_10 * 0.1f + SkillPoint_1* SkillPoint_1*0.001f;
        }
        else if(DataBaseManager.Condition == "Worst")
        {
            ResetRolletDice();
            Conditon.text = "Condition : Worst";
            Bad_10.SetActive(true);
            Bad_10_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = (SkillPoint_10 * SkillPoint_10 *0.01f) + SkillPoint_1*0.001f;
        }
        
        if(SuccesProbability < 0.20)
        {
            Expect.text = "Probability : Very Hard";
        }
        else if(SuccesProbability < 0.40)
        {
            Expect.text = "Probability : Hard";
        }
        else if (SuccesProbability < 0.60)
        {
            Expect.text = "Probability : Nomal";
        }
        else if (SuccesProbability < 0.80)
        {
            Expect.text = "Probability : Easy";
        }
        else if (SuccesProbability < 1)
        {
            Expect.text = "Probability : Very Easy";
        }
        Invoke("waitOneSec", 0.1f);
    }
    public void waitOneSec()
    {
        activeCheck = true;
    }
    public void activeRollet()
    {

        setUI.SetActive(false);
        activeUI.SetActive(true);
        InvokeRepeating("ChangeRollet", 1, 0.05f);
        Invoke("diceSound", 1);
    }
    void diceSound()
    {
        SoundManager.Instance.PlayDice();
    }

    void ChangeRollet()
    {
        isActiveRollet = true;
        NomalRollet_1t.text = Random.Range(0, 10).ToString();
        int nom_10t = (Random.Range(0, 10)*10);
        if(nom_10t != 0)
        {
            NomalRollet_10t.text = nom_10t.ToString();
        }
        else
        {
            NomalRollet_10t.text = "00";
        }


        BadRollet_1t.text = Random.Range(0, 10).ToString();
        int bad_10t = (Random.Range(0, 10) * 10);
        if (bad_10t != 0)
        {
            BadRollet_10t.text = bad_10t.ToString();
        }
        else
        {
            BadRollet_10t.text = "00";
        }

        GoodRollet_1t.text = Random.Range(0, 10).ToString();
        int good_10t = (Random.Range(0, 10) * 10);
        if (good_10t != 0)
        {
            GoodRollet_10t.text = good_10t.ToString();
        }
        else
        {
            GoodRollet_10t.text = "00";
        }

    }
    

    void GetIntResult()
    {

        SoundManager.Instance.pen_Line();
        if (DataBaseManager.Condition == "Nomal")
        {
            result_int = int.Parse(NomalRollet_10t.text) + int.Parse(NomalRollet_1t.text);
            if(result_int == 0)
            {
                result_int = 100;
            }
            result_string.text = "Dice : " + result_int.ToString();
        }
        else if (DataBaseManager.Condition == "Good")
        {
            if(int.Parse(NomalRollet_1t.text) > int.Parse(GoodRollet_1t.text))
            {
                result_int = int.Parse(NomalRollet_10t.text) + int.Parse(GoodRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "Dice : " + result_int.ToString();
            }
            else
            {
                result_int = int.Parse(NomalRollet_10t.text) + int.Parse(NomalRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "Dice : " + result_int.ToString();
            }
        }
        else if (DataBaseManager.Condition == "Best")
        {
            if (int.Parse(NomalRollet_10t.text) > int.Parse(GoodRollet_10t.text))
            {
                result_int = int.Parse(GoodRollet_10t.text) + int.Parse(NomalRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "Dice : " + result_int.ToString();
            }
            else
            {
                result_int = int.Parse(NomalRollet_10t.text) + int.Parse(NomalRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "Dice : " + result_int.ToString();
            }
        }
        else if (DataBaseManager.Condition == "Bad")
        {
            if (int.Parse(NomalRollet_1t.text) > int.Parse(BadRollet_1t.text))
            {
                result_int = int.Parse(NomalRollet_10t.text) + int.Parse(NomalRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "Dice : " + result_int.ToString();
            }
            else
            {
                result_int = int.Parse(NomalRollet_10t.text) + int.Parse(BadRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "Dice : " + result_int.ToString();
            }
        }
        else if (DataBaseManager.Condition == "Worst")
        {
            if (int.Parse(NomalRollet_10t.text) > int.Parse(BadRollet_10t.text))
            {
                result_int = int.Parse(NomalRollet_10t.text) + int.Parse(NomalRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "Dice : " + result_int.ToString();
            }
            else
            {
                result_int = int.Parse(BadRollet_10t.text) + int.Parse(NomalRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "Dice : " + result_int.ToString();
            }
        }

        Invoke("GetStringResult", 1f);
    }
    void GetStringResult()
    {
        SoundManager.Instance.pen_Line();
        if (result_int >= 95)
        {
            result_End.text = "Result : Fumble";
        }
        else if(result_int <= skillPoint / 3f)
        {
            result_End.text = "Result : Critical Success";
        }
        else if (result_int <= skillPoint)
        {
            result_End.text = "Result : Success";
        }
        else
        {
            result_End.text = "Result : Failure";
        }
        EndButton.SetActive(true);

    }

    public void EndRollet()
    {
        SoundManager.Instance.EndDice();



        activeCheck = false;
        DataBaseManager.isRollet = false;
        isClick = false;
        RolletSetUi.SetActive(false);
        RolletCheckUI.SetActive(true);
        RolletRollUI.SetActive(false) ;
        EndButton.SetActive(false);

        // 결과값을 전달하는 함수 필요

        //스트래스 전달
        if(Subject == "stress")
        {
            StressManager.Instance.Result(result_End.text);
        }
        if(Subject == "dialog")
        {
            InteractionController.Instance.RetrunDialogResult(Sub_Dialog, result_End.text);
            // 여기에 Dialog함수 적용   DialogManager.Instance.RetrunDialogResult(Sub_Dialog,result_End.text);
            // DilaogManager에서는 해당 함수에 interacitionMager를 연결해서 받은 주제와 결과값에 따른 문자를 출력하도록 함
        }
        if (Subject == "Item")
        {
            if(DataBaseManager.nowItem == "First aid kit")
            {
                DataBaseManager.Firstaidkit -= 1;
                if (result_End.text == "Result : Success")
                {

                    BillowUIManager.Instance.HP_up((Random.Range(1, 7)) * 5);
                }
                else if (result_End.text == "Result : Critical Success")
                {
                    BillowUIManager.Instance.HP_up((Random.Range(1, 7)) * 5);
                }
            }
            if (DataBaseManager.nowItem == "Bandages")
            {
                DataBaseManager.Bandages -= 1;
                if (result_End.text == "Result : Success")
                {

                    BillowUIManager.Instance.HP_up((Random.Range(1, 4)) * 5);
                }
                else if (result_End.text == "Result : Critical Success")
                {
                    BillowUIManager.Instance.HP_up((Random.Range(1, 4)) * 5);
                }
            }
            if (DataBaseManager.nowItem == "Painkillers")
            {
                DataBaseManager.Painkillers -= 1;
                if (result_End.text == "Result : Success")
                {

                    BillowUIManager.Instance.San_up((Random.Range(1, 4)) * 5);
                }
                else if (result_End.text == "Result : Critical Success")
                {
                    BillowUIManager.Instance.San_up((Random.Range(1, 4)) * 5);
                }
            }
            if (DataBaseManager.nowItem == "Safe")
            {
                DataBaseManager.isUseSafe = true;
                InteractionController.Instance.RetrunDialogResult(Sub_Dialog, result_End.text);
            }
        }
        if (Subject == "MS_attack")
        {
            if (EnemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(Subject, result_End.text, EnemySubject);
                if(DataBaseManager.BattleWeapon == "SmallPistol")
                {
                    DataBaseManager.nowSmallPistol -= 1;
                }
                if (DataBaseManager.BattleWeapon == "Rifle")
                {
                    DataBaseManager.nowRifle -= 1;
                }
                if (DataBaseManager.BattleWeapon == "Shotgun")
                {
                    DataBaseManager.nowShotgun -= 1;
                }
                if (DataBaseManager.BattleWeapon == "Revolver")
                {
                    DataBaseManager.nowRevolver -= 1;
                }

            }
        }


    }





    // 중요!!!!!!!
    private void ResetString()
    {
        NomalRollet_1t.text = "-";
        NomalRollet_10t.text = "--";
        BadRollet_1t.text = "-";
        BadRollet_10t.text = "--";
        GoodRollet_1t.text = "-";
        GoodRollet_10t.text = "--";
        result_string.text ="Dice : --";
        result_End.text = "Result : --";
    }
    private void ResetRolletDice()
    {
        Good_1.SetActive(false);
        Good_10.SetActive(false);
        Bad_1.SetActive(false);
        Bad_10.SetActive(false);
        Good_1_Rollet.SetActive(false);
        Good_10_Rollet.SetActive(false);
        Bad_1_Rollet.SetActive(false);
        Bad_10_Rollet.SetActive(false);

    }
}