using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Rollet : MonoBehaviour
{
    bool activeCheck = false;
    bool activeCheck2 = false;
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

    string  String;

    private void Update()
    {
        if (setUI.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            activeCheck = false;
            DataBaseManager.CancelJudge = true;
   

             if (BattleManager.Instance.PlayerAction == "PlayerMarkmen")
            {
                BattleManager.Instance.EnemySelectUI.SetActive(true);
                BattleManager.Instance.BattleState = "selectEnemy";
                BattleManager.Instance.Player_setMarkmens();
            }
            else if (BattleManager.Instance.PlayerAction == "PlayerDeftness")
            {
                BattleManager.Instance.EnemySelectUI.SetActive(true);
                BattleManager.Instance.BattleState = "selectEnemy";
                BattleManager.Instance.Player_setDeftness();
            }
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
                    Invoke("GetIntResult", 0.5f);
                }


            }
        }



        if (SanRollet.activeSelf == true && Input.GetKeyDown(KeyCode.E) && activeCheck2 == true)
        {
            activeCheck2 = false;
            activeSanRollet();
        }
        if (EndButton2.activeSelf == true && Input.GetKeyDown(KeyCode.E))
        {

            EndSanRollet();
        }
    }
    void Awake()
        {
            isClick = false;
            if (null == instance)
            {
                instance = this;

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
        SoundManager.Instance.PaperClip_Play();
        DataBaseManager.isRollet = true;
        Subject = subject;
        Sub_Dialog = skill; // 나중에 다이얼로그 결과 전송시 사용
        ResetString();
        RolletSetUi.SetActive(true);
        Skill.text = skill;
        Point.text = point_sting + " : " + point_int;
        skillPoint = point_int;
        String = point_sting;
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
        InvokeRepeating("ChangeRollet", 0.5f, 0.05f);
        Invoke("diceSound", 0.5f);
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

        Invoke("GetStringResult", 0.5f);
    }
    void GetStringResult()
    {
        SoundManager.Instance.pen_Circle();
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

   
        if(String == "Sanity")
        {
            Invoke("OpenSanRollet", 1f);

        }
        else
        {
            EndButton.SetActive(true);
        }
 

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
        if (Subject == "Deftness_attack")
        {
            if (EnemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(Subject, result_End.text, EnemySubject);
                if (DataBaseManager.BattleWeapon == "Molotov")
                {
                    DataBaseManager.Molotov -= 1;
                }


            }
        }

    }

    public GameObject EndButton2;

    public TextMeshProUGUI SanName;
    public TextMeshProUGUI Slot1_name;
    public TextMeshProUGUI Slot1_percent;
    public TextMeshProUGUI Slot2_name;
    public TextMeshProUGUI Slot2_percent;
    public TextMeshProUGUI Slot3_name;
    public TextMeshProUGUI Slot3_percent;
    public TextMeshProUGUI SanRolletText;
    public GameObject SanRollet;

    int slot1_int;
    int slot2_int;
    int slot3_int;

    public GameObject SanM10;
    public GameObject SanM5;

    //Level - 1 Debuff
    public GameObject ThinWallets;
    public GameObject Carelessness;
    public GameObject Debilitation;
    public GameObject Dizziness;
    public GameObject Sprains;
    public GameObject Unlucky;
    public GameObject MusclePain;
    public GameObject Migraines;
    public GameObject EyeDisease;

    //Level - 2 Debuff
    public GameObject Masochism;
    public GameObject Careless;
    public GameObject MentalWeakness;
    public GameObject Helplessness  ;
    public GameObject Extravagant;
    public GameObject PanicAttack;
    public GameObject Medicaldistrust;
    public GameObject Hallucinations;
    public GameObject Tightwad;

    //Level - 3 Debuff
    public GameObject NightPhobia;
    public GameObject Deafness;
    public GameObject VisionLoss;
    public GameObject ShortTempered;
    public GameObject Dyslexia;
    public GameObject DrugPhobia;
    public GameObject CognitiveDisorder;
    public GameObject CognitiveBreakdown;
    public GameObject HomicidalImpulse;
    public GameObject Perfectionism;

    //condition
    public GameObject Best;
    public GameObject Good;
    public GameObject Nomal;
    public GameObject Bad;
    public GameObject Worst;

    public TextMeshProUGUI Detail_San;
    public void OpenSanRollet()
    {
        SanRollet.SetActive(true);
        Invoke("waitOneSec2", 0.1f);
        if (result_End.text == "Result : Fumble" || result_End.text == "Result : Failure")
        {
            if(DataBaseManager.Debuff == 0)
            {
                if(DataBaseManager.Condition != "Worst")
                {
                    SanName.text = "!Sanity check failed!";
                    Slot1_name.text = "Sanity Decline";
                    Slot1_percent.text = "50%";
                    slot1_int = 50;
                    Slot2_name.text = "Manifest Insanity";
                    Slot2_percent.text = "30%";
                    slot2_int = slot1_int + 30;
                    Slot3_name.text = "Deconditioning";
                    Slot3_percent.text = "20%";
                    slot3_int = slot2_int + 20;
                }
                else
                {
                    SanName.text = "!Sanity check failed!";
                    Slot1_name.text = "Sanity Decline";
                    Slot1_percent.text = "50%";
                    slot1_int = 50;
                    Slot2_name.text = "Manifest Insanity";
                    Slot2_percent.text = "50%";
                    slot2_int = slot1_int + 50;
                    Slot3_name.text = "Deconditioning";
                    Slot3_percent.text = "---";
                    slot3_int = slot2_int + 999;
                }

            }
            else if (DataBaseManager.Debuff == 1)
            {
                if (DataBaseManager.Condition != "Worst")
                {
                    SanName.text = "!Sanity check failed!";
                    Slot1_name.text = "Sanity Decline";
                    Slot1_percent.text = "30%";
                    slot1_int = 30;
                    Slot2_name.text = "Manifest Insanity";
                    Slot2_percent.text = "50%";
                    slot2_int = slot1_int + 50;
                    Slot3_name.text = "Deconditioning";
                    Slot3_percent.text = "20%";
                    slot3_int = slot2_int + 20;
                }
                else
                {
                    SanName.text = "!Sanity check failed!";
                    Slot1_name.text = "Sanity Decline";
                    Slot1_percent.text = "30%";
                    slot1_int = 30;
                    Slot2_name.text = "Manifest Insanity";
                    Slot2_percent.text = "70%";
                    slot2_int = slot1_int + 70;
                    Slot3_name.text = "Deconditioning";
                    Slot3_percent.text = "---";
                    slot3_int = slot2_int + 999;
                }

            }
            else if (DataBaseManager.Debuff == 2)
            {
                if (DataBaseManager.Condition != "Worst")
                {
                    SanName.text = "!Sanity check failed!";
                    Slot1_name.text = "Sanity Decline";
                    Slot1_percent.text = "20%";
                    slot1_int = 20;
                    Slot2_name.text = "Manifest Insanity";
                    Slot2_percent.text = "60%";
                    slot2_int = slot1_int + 60;
                    Slot3_name.text = "Deconditioning";
                    Slot3_percent.text = "20%";
                    slot3_int = slot2_int + 20;
                }
                else
                {
                    SanName.text = "!Sanity check failed!";
                    Slot1_name.text = "Sanity Decline";
                    Slot1_percent.text = "20%";
                    slot1_int = 20;
                    Slot2_name.text = "Manifest Insanity";
                    Slot2_percent.text = "80%";
                    slot2_int = slot1_int + 80;
                    Slot3_name.text = "Deconditioning";
                    Slot3_percent.text = "---";
                    slot3_int = slot2_int + 999;
                }

            }
            else if (DataBaseManager.Debuff == 3)
            {
                if (DataBaseManager.Condition != "Worst")
                {
                    SanName.text = "!Sanity check failed!";
                    Slot1_name.text = "Sanity Decline";
                    Slot1_percent.text = "40%";
                    slot1_int = 40;
                    Slot2_name.text = "Manifest Insanity";
                    Slot2_percent.text = "---";
                    Slot3_name.text = "Deconditioning";
                    Slot3_percent.text = "60%";
                    slot3_int = slot2_int + 60;
                }
                else
                {
                    SanName.text = "!Sanity check failed!";
                    Slot1_name.text = "Sanity Decline";
                    Slot1_percent.text = "100%";
                    slot1_int = 100;
                    Slot2_name.text = "Manifest Insanity";
                    Slot2_percent.text = "---%";
                    slot2_int = slot1_int + 999;
                    Slot3_name.text = "Deconditioning";
                    Slot3_percent.text = "---";
                    slot3_int = slot2_int + 999;
                }

            }
        }
        else
        {
            SanName.text = "Sanity check Success";
            Slot1_name.text = "No change";
            Slot1_percent.text = "80%";
            slot1_int = 80;
            Slot2_name.text = "Rise sanity";
            Slot2_percent.text = "15%";
            slot2_int  = 95;
            Slot3_name.text = "Raise Condition";
            Slot3_percent.text = "5%";
            slot3_int = 100;
        }

    }
    public void activeSanRollet()
    {
        ChangeSanRollet();
    }
    public void waitOneSec2()
    {
        activeCheck2 = true;
    }
    void ChangeSanRollet()
    {
        if(SanName.text == "!Sanity check failed!")
        {
            int SanRollet = (Random.Range(1, 101));
            Debug.Log(SanRollet);

            if(DataBaseManager.Debuff == 3)
            {
                if (SanRollet <= slot1_int &&  SanRollet <= slot3_int)
                {
                    SanRolletText.text = "Sanity Decline";
                }
                else

                {
                    SanRolletText.text = "Deconditioning";
                }
            }
            else
            {
                if (SanRollet <= slot1_int && SanRollet <= slot2_int && SanRollet <= slot3_int)
                {
                    SanRolletText.text = "Sanity Decline";
                }
                else if (SanRollet >= slot1_int && SanRollet <= slot2_int && SanRollet <= slot3_int)
                {
                    SanRolletText.text = "Manifest Insanity";
                }
                else if (SanRollet >= slot1_int && SanRollet >= slot2_int && SanRollet <= slot3_int)

                {
                    SanRolletText.text = "Deconditioning";
                }
            }

        }
        else if (SanName.text == "Sanity check Success")
        {
            int SanRollet = (Random.Range(1, 101));
            Debug.Log(SanRollet);
            if (SanRollet <= slot1_int)
            {
                SanRolletText.text = "No change";
            }
            else if (SanRollet <= slot2_int)
            {
                SanRolletText.text = "Rise sanity";
            }
            else
            {
                SanRolletText.text = "Raise Condition";
            }
        }
        Invoke("ResultRollet",1);
    }

    void ResultRollet()
    {
        if (SanRolletText.text == "Sanity Decline")
        {
            int SanRollet = (Random.Range(0, 3));
            if(SanRollet == 1)
            {
                Detail_San.text = "Sanity Decline : 10\n\nThe current sanity reduced by 10 Point.";
                BillowUIManager.Instance.San_Down(10);
                SanM10.SetActive(true);
            }
            else
            {
                Detail_San.text = "Sanity Decline : 5\n\nThe current sanity reduced by 5 Point.";

                BillowUIManager.Instance.San_Down(5);
                SanM5.SetActive(true);
            }
        }
        else if (SanRolletText.text == "Manifest Insanity")
        {
            if (DataBaseManager.Debuff == 0)
            {
                DataBaseManager.Debuff += 1;
                int DebuffRollet = (Random.Range(1, 10));
                switch (DebuffRollet)
                {
                    case 1:
                        Detail_San.text = "Level 1 : ThinWallets\n\nYou lose 5 of your wealth Point.";
                        ThinWallets.SetActive(true);
                        if(DataBaseManager.weal > 5)
                        {
                            DataBaseManager.weal -= 5;
                        }
                        else
                        {
                            DataBaseManager.weal = 0;
                        }
                        break;
                    case 2:
                        Carelessness.SetActive(true);
                        DataBaseManager.hp -= 5;
                        BillowUIManager.Instance.HP_down(5);
                        Detail_San.text = "Level 1 : Carelessness\n\nYou lose 5 HP and max HP Point.";
                        break;
                    case 3:
                        Debilitation.SetActive(true);
                        DataBaseManager.str -= 10;
                        Detail_San.text = "Level 1 : Debilitation\n\nYou lose 10 Str Point";
                        break;
                    case 4:
                        Dizziness.SetActive(true);
                        DataBaseManager.intl -= 10;
                        Detail_San.text = "Level 1 : Dizziness\n\nYou lose 10 Int Point";
                        break;
                    case 5:
                        Sprains.SetActive(true);
                        DataBaseManager.dex -= 10;
                        Detail_San.text = "Level 1 : Sprains\n\nYou lose 10 Dex Point";
                        break;
                    case 6:
                        Unlucky.SetActive(true);
                        DataBaseManager.luk -= 10;
                        Detail_San.text = "Level 1 : Unlucky\n\nYou lose 10 Luk Point";
                        break;
                    case 7:
                        MusclePain.SetActive(true);
                        DataBaseManager.gunShotPoint -= 10;
                        DataBaseManager.martialArtsPoint -= 10;
                        DataBaseManager.swimingPoint -= 10;
                        DataBaseManager.swordPoint -= 10;
                        DataBaseManager.ObservationPoint -= 10;
                        Detail_San.text = "Level 1 : MusclePain\n\nAll skills based on Str are reduced by 10 Point.";
                        break;
                    case 8:
                        Migraines.SetActive(true);
                        DataBaseManager.medicinePoint -= 10;
                        DataBaseManager.analysisPoint -= 10;
                        DataBaseManager.listeningPoint -= 10;
                        if(DataBaseManager.psychotherapyPoint> 10)
                        {
                            DataBaseManager.psychotherapyPoint -= 10;
                        }
                        else
                        {
                            DataBaseManager.psychotherapyPoint = 0;
                        }

                        if (DataBaseManager.occultPoint > 10)
                        {
                            DataBaseManager.occultPoint -= 10;
                        }
                        else
                        {
                            DataBaseManager.occultPoint = 0;
                        }
                        Detail_San.text = "Level 1 : Migraines\n\nAll skills based on Int are reduced by 10 Point.";
                        break;
                    case 9:
                        EyeDisease.SetActive(true);
                        DataBaseManager.evasionPoint -= 10;
                        DataBaseManager.deftnessPoint -= 10;
                        DataBaseManager.rhetoricPoint -= 10;
                        DataBaseManager.stealthPoint -= 10;
                        DataBaseManager.DisguisePoint -= 10;
                        Detail_San.text = "Level 1 : EyeDisease\n\nAll skills based on Dex are reduced by 10 Point."; 
                        break;
                }
                    
            }
            else if (DataBaseManager.Debuff == 1)
            {
                DataBaseManager.Debuff += 1;
                int DebuffRollet = (Random.Range(1, 8));
                switch (DebuffRollet)
                {
                    case 1:
                        Detail_San.text = "LV : 1 : Masochism\n\nThe player takes an additional 5 damage when hit.(100%)";
                        Masochism.SetActive(true);
                        DataBaseManager.Masochism = true;
                        break;
                    case 2:
                        Careless.SetActive(true);
                        SIS_UIManager.Instance.CareLessActive();
                        break;
                    case 3:
                        MentalWeakness.SetActive(true);
                        DataBaseManager.MentalWeakness = true;
                        break;
                    case 4:
                        Helplessness.SetActive(true);
                        DataBaseManager.Helplessness = true;
                        break;
                    case 5:
                        Extravagant.SetActive(true);
                        ShopUI.Instance.ExtravoltON();
                        DataBaseManager.Extravagant = true;
                        break;
                    case 6:
                        PanicAttack.SetActive(true);
                        DataBaseManager.PanicAttack = true;
                        break;
                    case 7:
                        Tightwad.SetActive(true);
                        DataBaseManager.Tightwad = true;
                        break;
                    case 8:
                        Hallucinations.SetActive(true);      // 나중에 연출로 해서 넣기~~~~~~~~~~~~

                        break;
                    case 9:
                        Medicaldistrust.SetActive(true);   // 나중에 상점 추가된 뒤에 해서 넣기~~~~~~~~~~~~


                        break;
                }

            }
            else if (DataBaseManager.Debuff == 2)
            {
                DataBaseManager.Debuff += 1;
                int DebuffRollet = (Random.Range(1, 11));
                switch (DebuffRollet)
                {
                    case 1:
                        NightPhobia.SetActive(true);

                        break;
                    case 2:
                        Deafness.SetActive(true);

                        break;
                    case 3:
                        VisionLoss.SetActive(true);

                        break;
                    case 4:
                        ShortTempered.SetActive(true);

                        break;
                    case 5:
                        Dyslexia.SetActive(true);

                        break;
                    case 6:
                        DrugPhobia.SetActive(true);

                        break;
                    case 7:
                        CognitiveDisorder.SetActive(true);

                        break;
                    case 8:
                        CognitiveBreakdown.SetActive(true);

                        break;
                    case 9:
                        HomicidalImpulse.SetActive(true);

                        break;
                    case 10:
                        Perfectionism.SetActive(true);

                        break;
                }

            }
        }
        else if (SanRolletText.text == "Deconditioning")
        {
            if (DataBaseManager.Condition == "Best")
            {

                Good.SetActive(true);
                Detail_San.text = "Deconditioning : Good\n\nCondition has dropped to (Good).";

                DataBaseManager.Condition = "Good";
            }
            else if (DataBaseManager.Condition == "Good")
            {
                Nomal.SetActive(true);
                Detail_San.text = "Deconditioning : Nomal\n\nCondition has dropped to (Nomal).";
                DataBaseManager.Condition = "Nomal";
            }
            else if(DataBaseManager.Condition == "Nomal")
            {
                Bad.SetActive(true);
                Detail_San.text = "Deconditioning : Bad\n\nCondition has dropped to (Bad).";
                DataBaseManager.Condition = "Bad";
            }
            else if(DataBaseManager.Condition == "Bad")
            {
                Worst.SetActive(true);
                Detail_San.text = "Deconditioning : Worst\n\nCondition has dropped to (Worst).";
                DataBaseManager.Condition = "Worst";
            }
            else if(DataBaseManager.Condition == "Worst")
            {

            }
            
        }
        else if (SanRolletText.text == "Rise sanity")
        {
            int SanRollet = (Random.Range(0, 2));
            if (SanRollet == 1)
            {

                BillowUIManager.Instance.San_up(10);
                SanM10.SetActive(true);
            }
            else
            {

                BillowUIManager.Instance.San_up(5);
                SanM5.SetActive(true);
            }
        }
        else if (SanRolletText.text == "Raise Condition")
        {
            if (DataBaseManager.Condition == "Best")
            {
   
            }
            else if (DataBaseManager.Condition == "Good")
            {
                Best.SetActive(true);
                DataBaseManager.Condition = "Best";
            }
            else if (DataBaseManager.Condition == "Nomal")
            {
                Good.SetActive(true);
                DataBaseManager.Condition = "Good";
            }
            else if (DataBaseManager.Condition == "Bad")
            {
                Nomal.SetActive(true);
                DataBaseManager.Condition = "Nomal";
            }
            else if (DataBaseManager.Condition == "Worst")
            {
                Bad.SetActive(true);
                DataBaseManager.Condition = "Bad";
            }
        }
        else if (SanRolletText.text == "No change")
        {
            
        }
        EndButton2.SetActive(true);
    }

    public void EndSanRollet()
    {
        SoundManager.Instance.EndDice();



        RolletSetUi.SetActive(false);
        RolletCheckUI.SetActive(true);
        RolletRollUI.SetActive(false);


        activeCheck = false;
        DataBaseManager.isRollet = false;
        isClick = false;
        SanRollet.SetActive(false);
        EndButton2.SetActive(false);
        resetSanRollet();
        // 결과값을 전달하는 함수 필요

        if (Subject == "dialog")
        {
            InteractionController.Instance.RetrunDialogResult(Sub_Dialog, result_End.text);
            // 여기에 Dialog함수 적용   DialogManager.Instance.RetrunDialogResult(Sub_Dialog,result_End.text);
            // DilaogManager에서는 해당 함수에 interacitionMager를 연결해서 받은 주제와 결과값에 따른 문자를 출력하도록 함
        }


    }

    void resetSanRollet()
    {
        EndButton2.SetActive(false);
        Detail_San.text = "";
        SanName.text = "";
        Slot1_name.text = "";
        Slot1_percent.text = "";
        Slot2_name.text = "";
        Slot2_percent.text = "";
        Slot3_name.text = "";
        Slot3_percent.text = "";
        SanRolletText.text = "";

        slot1_int = 0;
        slot2_int = 0;
        slot3_int = 0;

        SanM10.SetActive(false);
        SanM5.SetActive(false);

        //Level - 1 Debuff
        ThinWallets.SetActive(false);
        Carelessness.SetActive(false);
        Debilitation.SetActive(false);
        Dizziness.SetActive(false);
        Sprains.SetActive(false);
        Unlucky.SetActive(false);
        MusclePain.SetActive(false);
        Migraines.SetActive(false);
        EyeDisease.SetActive(false);

             Masochism.SetActive(false);
        Careless.SetActive(false);
        MentalWeakness.SetActive(false);
        Helplessness.SetActive(false);
        Extravagant.SetActive(false);
        PanicAttack.SetActive(false);
        Medicaldistrust.SetActive(false);
        Hallucinations.SetActive(false);
        Tightwad.SetActive(false);

        //Level - 3 Debuff
        NightPhobia.SetActive(false);
        Deafness.SetActive(false);
        VisionLoss.SetActive(false);
        ShortTempered.SetActive(false);
        Dyslexia.SetActive(false);
        DrugPhobia.SetActive(false);
        CognitiveDisorder.SetActive(false);
        CognitiveBreakdown.SetActive(false);
        HomicidalImpulse.SetActive(false);
        Perfectionism.SetActive(false);
        Best.SetActive(false);
        Good.SetActive(false);
        Nomal.SetActive(false);
        Bad.SetActive(false);
        Worst.SetActive(false);
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