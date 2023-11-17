using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleRollet : MonoBehaviour
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


    public bool isClick;
    public bool isActiveRollet;
    private static BattleRollet instance = null;

    public GameObject EndButton;


    // 적 칸
    public string EnemySubject;
    
    public TextMeshProUGUI E_Skill;
    public TextMeshProUGUI E_Point;
    public TextMeshProUGUI E_Expect;

    public TextMeshProUGUI E_NomalRollet_1t;
    public TextMeshProUGUI E_NomalRollet_10t;

    int E_skillPoint;
    int E_result_int;
    public TextMeshProUGUI E_result_string;
    public TextMeshProUGUI E_result_End;
    private void Update()
    {
        if (setUI.activeSelf == true && Input.GetKeyDown(KeyCode.Escape) && Sub_Dialog != "SAN : Check")
        {
            activeCheck = false;
            DataBaseManager.CancelJudge = true;
            if (BattleManager.Instance.PlayerAction == "PlayerSwords")
            {
                BattleManager.Instance.EnemySelectUI.SetActive(true);
                BattleManager.Instance.BattleState = "selectEnemy";
                BattleManager.Instance.Player_setSwords();
            }
            else if (BattleManager.Instance.PlayerAction == "martialarts")
            {
                BattleManager.Instance.EnemySelectUI.SetActive(true);
                BattleManager.Instance.BattleState = "selectEnemy";
                BattleManager.Instance.SetAciton("martialarts");
            }
            else if (BattleManager.Instance.PlayerAction == "evasion")
            {
                BattleManager.Instance.PlayerChoiceUi.SetActive(true);
            }
            else if (BattleManager.Instance.PlayerAction == "counterattack")
            {
                BattleManager.Instance.PlayerChoiceUi.SetActive(true);
            }
            RolletSetUi.SetActive(false);
            DataBaseManager.isRollet = false;


        }
        if (setUI.activeSelf == true && Input.GetKeyDown(KeyCode.E) && activeCheck == true)
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
    }

    void Awake()
    {
     
        isClick = false;
        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    public void waitOneSec()
    {
        activeCheck = true;
    }
    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static BattleRollet Instance
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
    public void setBattleRollet(string skill, string point_sting, int point_int, string subject,string EnemyName , string E_skill, string E_point_string, int E_Point_int)
    {
        SoundManager.Instance.PaperClip_Play();
        DataBaseManager.isRollet = true;
        Subject = subject;
        Sub_Dialog = skill; // 나중에 다이얼로그 Result 전송시 사용
        ResetString();
        RolletSetUi.SetActive(true);
        //플레이어
        Skill.text = skill;
        Point.text = point_sting + " : " + point_int;
        //적
        EnemySubject = EnemyName;
        E_Skill.text = E_skill;
        E_Point.text = E_point_string + " :  ??";

        skillPoint = point_int;
        E_skillPoint = E_Point_int;
        if (DataBaseManager.Condition == "Nomal")
        {
            ResetRolletDice();
            Conditon.text = "Condition: Nomal";

            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = SkillPoint_10 * 0.1f + SkillPoint_1 * 0.01f;
        }
        else if (DataBaseManager.Condition == "Good")
        {
            ResetRolletDice();
            Conditon.text = "Condition: Good";
            Good_1.SetActive(true);
            Good_1_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = SkillPoint_10 * 0.1f + (SkillPoint_1 * SkillPoint_1 + 2 * SkillPoint_1 * (10 - SkillPoint_1)) * 0.001f;
        }
        else if (DataBaseManager.Condition == "Best")
        {
            ResetRolletDice();
            Conditon.text = "Condition: Best";
            Good_10.SetActive(true);
            Good_10_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = (SkillPoint_10 * SkillPoint_10 + 2 * SkillPoint_10 * (10 - SkillPoint_10)) * 0.01f + SkillPoint_1 * 0.001f;
        }
        else if (DataBaseManager.Condition == "Bad")
        {
            ResetRolletDice();
            Conditon.text = "Condition: Bad";
            Bad_1.SetActive(true);
            Bad_1_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = SkillPoint_10 * 0.1f + SkillPoint_1 * SkillPoint_1 * 0.001f;
        }
        else if (DataBaseManager.Condition == "Worst")
        {
            ResetRolletDice();
            Conditon.text = "Condition: Worst";
            Bad_10.SetActive(true);
            Bad_10_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = (SkillPoint_10 * SkillPoint_10 * 0.01f) + SkillPoint_1 * 0.001f;
        }

        // 적 확률 계산.
        E_Expect.text = "Probability : ???";

        if (SuccesProbability < 0.20)
        {
            Expect.text = "Probability : Very Hard";
        }
        else if (SuccesProbability < 0.40)
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
    
    public void activeRollet()
    {
        BattleManager.Instance.PlayerMAUI.SetActive(false);
        BattleManager.Instance.PlayerActionUi.SetActive(false);




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
        int nom_10t = (Random.Range(0, 10) * 10);
        if (nom_10t != 0)
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


        //적
        E_NomalRollet_1t.text = Random.Range(0, 10).ToString();
        int E_nom_10t = (Random.Range(0, 10) * 10);
        if (E_nom_10t != 0)
        {
            E_NomalRollet_10t.text = E_nom_10t.ToString();
        }
        else
        {
            E_NomalRollet_10t.text = "00";
        }
    }


    void GetIntResult()
    {
        SoundManager.Instance.pen_Line();
        if (DataBaseManager.Condition == "Nomal")
        {
            result_int = int.Parse(NomalRollet_10t.text) + int.Parse(NomalRollet_1t.text);
            if (result_int == 0)
            {
                result_int = 100;
            }
            result_string.text = "Dice : " + result_int.ToString();
        }
        else if (DataBaseManager.Condition == "Good")
        {
            if (int.Parse(NomalRollet_1t.text) > int.Parse(GoodRollet_1t.text))
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

        //적 관련
        E_result_int = int.Parse(E_NomalRollet_10t.text) + int.Parse(E_NomalRollet_1t.text);
        if (E_result_int == 0)
        {
            E_result_int = 100;
        }
        E_result_string.text = "Dice : " + E_result_int.ToString();

        Invoke("GetStringResult", 0.5f);
    }
    void GetStringResult()
    {
        SoundManager.Instance.pen_Circle();
        if (result_int >= 95)
        {
            result_End.text = "Result : Fumble";
        }
        else if (result_int <= skillPoint / 3f)
        {
            result_End.text = "Result : Critical Success";
        }
        else if (result_int <= skillPoint)
        {
            if (DataBaseManager.isDebuff_Perfectionism == true)
            {
                result_End.text = "Result : Failure";
            }
            else
            {
                result_End.text = "Result : Success";
            }
        }
        else
        {
            result_End.text = "Result : Failure";
        }

        if (E_result_int >= 95)
        {
            E_result_End.text = "Result : Fumble";
        }
        else if (E_result_int <= E_skillPoint / 3f)
        {
            E_result_End.text = "Result : Critical Success";
        }
        else if (E_result_int <= E_skillPoint)
        {
            E_result_End.text = "Result : Success";
        }
        else
        {
            E_result_End.text = "Result : Failure";
        }

        Invoke("GetFinalResult", 0.5f);
    }

    public string FinalResult;
    public TextMeshProUGUI FinalResult_t;
    void GetFinalResult()
    {
        if (Subject == "evasion")
        {
            if (result_End.text == "Result : Critical Success")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Evasion : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text == "Result : Success")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Evasion : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text == "Result : Failure")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Evasion : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text == "Result : Fumble")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Evasion : " + FinalResult;
            }
            else if (result_End.text == "Result : Failure" && E_result_End.text == "Result : Failure")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Evasion : " + FinalResult;
            }
            else if (result_End.text == "Result : Failure" && E_result_End.text == "Result : Fumble")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Evasion : " + FinalResult;
            }
            else if (result_End.text == "Result : Fumble" && E_result_End.text == "Result : Fumble")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Evasion : " + FinalResult;
            }
            else
            {
                FinalResult = "Failure";
                FinalResult_t.text = "Evasion : " + FinalResult;
            }

            
        } // 유리Result 예제
        if (Subject == "counterattack")
        {
            if (result_End.text == "Result : Critical Success" && E_result_End.text != "Result : Critical Success")
            {
                FinalResult = "Success";
                FinalResult_t.text = "CounterAttack : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text != "Result : Critical Success" && E_result_End.text != "Result : Success")
            {
                FinalResult = "Success";
                FinalResult_t.text = "CounterAttack : " + FinalResult;
            }
            else if (result_End.text == "Result : Failure" && E_result_End.text != "Result : Critical Success" && E_result_End.text != "Result : Success" && E_result_End.text != "Result : Failure")
            {
                FinalResult = "Success";
                FinalResult_t.text = "CounterAttack : " + FinalResult;
            }
            else
            {
                FinalResult = "Failure";
                FinalResult_t.text = "CounterAttack : " + FinalResult;
            }


        } //불리Result 예제

        // 플레이어행동
        if (Subject == "MA_attack_Counter")
        {
            if (result_End.text == "Result : Critical Success")
            {
                FinalResult = "Success";
                FinalResult_t.text = "MartialArt : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text == "Result : Success")
            {
                FinalResult = "Success";
                FinalResult_t.text = "MartialArt : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text == "Result : Failure")
            {
                FinalResult = "Success";
                FinalResult_t.text = "MartialArt : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text == "Result : Fumble")
            {
                FinalResult = "Success";
                FinalResult_t.text = "MartialArt : " + FinalResult;
            }
            else if (result_End.text == "Result : Failure" && E_result_End.text == "Result : Failure")
            {
                FinalResult = "Success";
                FinalResult_t.text = "MartialArt : " + FinalResult;
            }
            else if (result_End.text == "Result : Failure" && E_result_End.text == "Result : Fumble")
            {
                FinalResult = "Success";
                FinalResult_t.text = "MartialArt : " + FinalResult;
            }
            else if (result_End.text == "Result : Fumble" && E_result_End.text == "Result : Fumble")
            {
                FinalResult = "Success";
                FinalResult_t.text = "MartialArt : " + FinalResult;
            }
            else
            {
                FinalResult = "Failure";
                FinalResult_t.text = "MartialArt : " + FinalResult;
            }



        }
        if (Subject == "SS_attack_Counter")
        {
            if (result_End.text == "Result : Critical Success")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Swordsmanship : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text == "Result : Success")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Swordsmanship : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text == "Result : Failure")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Swordsmanship : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text == "Result : Fumble")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Swordsmanship : " + FinalResult;
            }
            else if (result_End.text == "Result : Failure" && E_result_End.text == "Result : Failure")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Swordsmanship : " + FinalResult;
            }
            else if (result_End.text == "Result : Failure" && E_result_End.text == "Result : Fumble")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Swordsmanship : " + FinalResult;
            }
            else if (result_End.text == "Result : Fumble" && E_result_End.text == "Result : Fumble")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Swordsmanship : " + FinalResult;
            }
            else
            {
                FinalResult = "Failure";
                FinalResult_t.text = "Swordsmanship : " + FinalResult;
            }



        }
        if(Subject == "RunAway")
        {
            if (result_End.text == "Result : Critical Success")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Runaway : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text == "Result : Success")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Runaway : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text == "Result : Failure")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Runaway : " + FinalResult;
            }
            else if (result_End.text == "Result : Success" && E_result_End.text == "Result : Fumble")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Runaway : " + FinalResult;
            }
            else if (result_End.text == "Result : Failure" && E_result_End.text == "Result : Failure")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Runaway : " + FinalResult;
            }
            else if (result_End.text == "Result : Failure" && E_result_End.text == "Result : Fumble")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Runaway : " + FinalResult;
            }
            else if (result_End.text == "Result : Fumble" && E_result_End.text == "Result : Fumble")
            {
                FinalResult = "Success";
                FinalResult_t.text = "Runaway : " + FinalResult;
            }
            else
            {
                FinalResult = "Failure";
                FinalResult_t.text = "Runaway : " + FinalResult;
            }
        }
        EndButton.SetActive(true);
    }

    public void EndRollet()
    {
        activeCheck = false;
        isClick = false;
        RolletSetUi.SetActive(false);
        RolletCheckUI.SetActive(true);
        RolletRollUI.SetActive(false) ;
        EndButton.SetActive(false);

        if (Subject == "evasion")
        {
            if(EnemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(Subject,FinalResult, EnemySubject);   
            }
            else if (EnemySubject == "DeepOne")
            {
                BattleManager.Instance.RetrunRolletResult(Subject, FinalResult, EnemySubject);
            }
        }
        if (Subject == "counterattack")
        {
            if (EnemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(Subject, FinalResult, EnemySubject);
            }
            else if (EnemySubject == "DeepOne")
            {
                BattleManager.Instance.RetrunRolletResult(Subject, FinalResult, EnemySubject);
            }
        }

        //플레이어행동
        if (Subject == "MA_attack_Counter")
        {
            if (EnemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(Subject, FinalResult, EnemySubject);
            }
            else if (EnemySubject == "DeepOne")
            {
                BattleManager.Instance.RetrunRolletResult(Subject, FinalResult, EnemySubject);
            }
        }
        if (Subject == "SS_attack_Counter")
        {
            if (EnemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(Subject, FinalResult, EnemySubject);
            }
            else if (EnemySubject == "DeepOne")
            {
                BattleManager.Instance.RetrunRolletResult(Subject, FinalResult, EnemySubject);
            }
        }

        //스트래스 전달
        if (Subject == "stress")
        {
            StressManager.Instance.Result(result_End.text);
        }
        if (Subject == "dialog")
        {
            InteractionController.Instance.RetrunDialogResult(Sub_Dialog, result_End.text);
            // 여기에 Dialog함수 적용   DialogManager.Instance.RetrunDialogResult(Sub_Dialog,result_End.text);
            // DilaogManager에서는 해당 함수에 interacitionMager를 연결해서 받은 주제와 Result값에 따른 문자를 출력하도록 함
        }
        if (Subject == "RunAway")
        {
            if (FinalResult == "Success")
            {
                if(DataBaseManager.nowPlace != "InSewer")
                {
                    BattleManager.Instance.Ex_BattleEnd();
                }
                else
                {
                    BattleManager.Instance.EndSewerBattle();

                }

            }
            else
            {
               BattleManager.Instance.TurnEnd();
            }

            // 여기에 Dialog함수 적용   DialogManager.Instance.RetrunDialogResult(Sub_Dialog,result_End.text);
            // DilaogManager에서는 해당 함수에 interacitionMager를 연결해서 받은 주제와 Result값에 따른 문자를 출력하도록 함
        }
    }

    // 중요!!!!!!!
    private void ResetString()
    {
        E_NomalRollet_1t.text = "-";
        E_NomalRollet_10t.text = "--";
        NomalRollet_1t.text = "-";
        NomalRollet_10t.text = "--";
        BadRollet_1t.text = "-";
        BadRollet_10t.text = "--";
        GoodRollet_1t.text = "-";
        GoodRollet_10t.text = "--";
        result_string.text ="Dice : --";
        result_End.text = "Result : --";
        E_result_string.text = "Dice : --";
        E_result_End.text = "Result : --";
        FinalResult_t.text = "---";
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