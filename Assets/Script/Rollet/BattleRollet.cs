using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleRollet : MonoBehaviour
{
    bool activeCheck = false;
    string subject;
    string subDialog;
    public GameObject rolletSetUi;
    public GameObject rolletCheckUI;
    public GameObject rolletRollUI;
    public GameObject bad1;
    public GameObject bad10;
    public GameObject good1;
    public GameObject good10;
    public GameObject bad1Rollet;
    public GameObject bad10Rollet;
    public GameObject good1Rollet;
    public GameObject good10Rollet;

    public GameObject setUI;
    public GameObject activeUI;
    public float skillPoint1;
    public float skillPoint10;
    public float succesProbability;
    public TextMeshProUGUI skill;
    public TextMeshProUGUI point;
    public TextMeshProUGUI conditon;
    public TextMeshProUGUI expect;
    public TextMeshProUGUI nomalRollet1Txt;
    public TextMeshProUGUI nomalRollet10Txt;
    public TextMeshProUGUI badRollet1Txt;
    public TextMeshProUGUI badRollet10Txt;
    public TextMeshProUGUI goodRollet1Txt;
    public TextMeshProUGUI goodRollet10Txt;


    int skillPoint;
    int resultInt;
    public TextMeshProUGUI resultString;
    public TextMeshProUGUI resultEnd;

    public bool isClick;
    public bool isActiveRollet;
    private static BattleRollet instance = null;
    public GameObject endButton;

    // 적 칸
    public string enemySubject;
    public TextMeshProUGUI enemySkill;
    public TextMeshProUGUI enemyPoint;
    public TextMeshProUGUI enemyExpect;
    public TextMeshProUGUI enemyNomalRollet1Txt;
    public TextMeshProUGUI enemyNomalRollet10Txt;

    int enemySkillPoint;
    int enemyResultInt;
    public TextMeshProUGUI enemyResultString;
    public TextMeshProUGUI enemyResultEnd;
    public GameObject rolletUI;

    private void Update()
    {
        if (rolletUI.activeSelf == true && setUI.activeSelf == true && Input.GetKeyDown(KeyCode.Escape) && subDialog != "SAN : Check")
        {
            activeCheck = false;
            DataBaseManager.cancelJudge = true;
            if (BattleManager.Instance.playerAction == "PlayerSwords")
            {
                BattleManager.Instance.enemySelectUI.SetActive(true);
                BattleManager.Instance.battleState = "selectEnemy";
                BattleManager.Instance.Player_setSwords();
            }
            else if (BattleManager.Instance.playerAction == "martialarts")
            {
                BattleManager.Instance.enemySelectUI.SetActive(true);
                BattleManager.Instance.battleState = "selectEnemy";
                BattleManager.Instance.SetAciton("martialarts");
            }
            else if (BattleManager.Instance.playerAction == "evasion")
            {
                BattleManager.Instance.playerChoiceUi.SetActive(true);
            }
            else if (BattleManager.Instance.playerAction == "counterattack")
            {
                BattleManager.Instance.playerChoiceUi.SetActive(true);
            }
            rolletSetUi.SetActive(false);
            DataBaseManager.isRollet = false;
        }
        if (setUI.activeSelf == true && Input.GetKeyDown(KeyCode.E) && activeCheck == true)
        {
            activeCheck = false;
            activeRollet();
        }
        if (endButton.activeSelf == true && Input.GetKeyDown(KeyCode.E))
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
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void WaitOneSec()
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
    public void setBattleRollet(string skill, string point_sting, int point_int, string subject, string EnemyName, string E_skill, string E_point_string, int E_Point_int)
    {
        SoundManager.Instance.PaperClip_Play();
        DataBaseManager.isRollet = true;
        this.subject = subject;
        subDialog = skill; // 나중에 다이얼로그 Result 전송시 사용
        ResetString();
        rolletSetUi.SetActive(true);
        //플레이어
        this.skill.text = skill;
        point.text = point_sting + " : " + point_int;
        //적
        enemySubject = EnemyName;
        enemySkill.text = E_skill;
        enemyPoint.text = E_point_string + " :  ??";

        skillPoint = point_int;
        enemySkillPoint = E_Point_int;
        if (DataBaseManager.condition == "Nomal")
        {
            ResetRolletDice();
            conditon.text = "Condition: Nomal";

            skillPoint1 = point_int % 10;
            skillPoint10 = point_int / 10;
            succesProbability = skillPoint10 * 0.1f + skillPoint1 * 0.01f;
        }
        else if (DataBaseManager.condition == "Good")
        {
            ResetRolletDice();
            conditon.text = "Condition: Good";
            good1.SetActive(true);
            good1Rollet.SetActive(true);
            skillPoint1 = point_int % 10;
            skillPoint10 = point_int / 10;
            succesProbability = skillPoint10 * 0.1f + (skillPoint1 * skillPoint1 + 2 * skillPoint1 * (10 - skillPoint1)) * 0.001f;
        }
        else if (DataBaseManager.condition == "Best")
        {
            ResetRolletDice();
            conditon.text = "Condition: Best";
            good10.SetActive(true);
            good10Rollet.SetActive(true);
            skillPoint1 = point_int % 10;
            skillPoint10 = point_int / 10;
            succesProbability = (skillPoint10 * skillPoint10 + 2 * skillPoint10 * (10 - skillPoint10)) * 0.01f + skillPoint1 * 0.001f;
        }
        else if (DataBaseManager.condition == "Bad")
        {
            ResetRolletDice();
            conditon.text = "Condition: Bad";
            bad1.SetActive(true);
            bad1Rollet.SetActive(true);
            skillPoint1 = point_int % 10;
            skillPoint10 = point_int / 10;
            succesProbability = skillPoint10 * 0.1f + skillPoint1 * skillPoint1 * 0.001f;
        }
        else if (DataBaseManager.condition == "Worst")
        {
            ResetRolletDice();
            conditon.text = "Condition: Worst";
            bad10.SetActive(true);
            bad10Rollet.SetActive(true);
            skillPoint1 = point_int % 10;
            skillPoint10 = point_int / 10;
            succesProbability = (skillPoint10 * skillPoint10 * 0.01f) + skillPoint1 * 0.001f;
        }

        // 적 확률 계산.
        enemyExpect.text = "Probability : ???";

        if (succesProbability < 0.20)
        {
            expect.text = "Probability : Very Hard";
        }
        else if (succesProbability < 0.40)
        {
            expect.text = "Probability : Hard";
        }
        else if (succesProbability < 0.60)
        {
            expect.text = "Probability : Nomal";
        }
        else if (succesProbability < 0.80)
        {
            expect.text = "Probability : Easy";
        }
        else if (succesProbability < 1)
        {
            expect.text = "Probability : Very Easy";
        }
        Invoke("WaitOneSec", 0.1f);
    }

    public void activeRollet()
    {
        BattleManager.Instance.playerMAUI.SetActive(false);
        BattleManager.Instance.playerActionUi.SetActive(false);
        setUI.SetActive(false);
        activeUI.SetActive(true);
        InvokeRepeating("ChangeRollet", 0.5f, 0.05f);
        Invoke("DiceSound", 0.5f);
    }
    void DiceSound()
    {
        SoundManager.Instance.PlayDice();
    }
    void ChangeRollet()
    {
        isActiveRollet = true;
        nomalRollet1Txt.text = Random.Range(0, 10).ToString();
        int nom_10t = (Random.Range(0, 10) * 10);
        if (nom_10t != 0)
        {
            nomalRollet10Txt.text = nom_10t.ToString();
        }
        else
        {
            nomalRollet10Txt.text = "00";
        }
        badRollet1Txt.text = Random.Range(0, 10).ToString();
        int bad_10t = (Random.Range(0, 10) * 10);
        if (bad_10t != 0)
        {
            badRollet10Txt.text = bad_10t.ToString();
        }
        else
        {
            badRollet10Txt.text = "00";
        }
        goodRollet1Txt.text = Random.Range(0, 10).ToString();
        int good_10t = (Random.Range(0, 10) * 10);
        if (good_10t != 0)
        {
            goodRollet10Txt.text = good_10t.ToString();
        }
        else
        {
            goodRollet10Txt.text = "00";
        }
        //적
        enemyNomalRollet1Txt.text = Random.Range(0, 10).ToString();
        int E_nom_10t = (Random.Range(0, 10) * 10);
        if (E_nom_10t != 0)
        {
            enemyNomalRollet10Txt.text = E_nom_10t.ToString();
        }
        else
        {
            enemyNomalRollet10Txt.text = "00";
        }
    }
    void GetIntResult()
    {
        SoundManager.Instance.pen_Line();
        if (DataBaseManager.condition == "Nomal")
        {
            resultInt = int.Parse(nomalRollet10Txt.text) + int.Parse(nomalRollet1Txt.text);
            if (resultInt == 0)
            {
                resultInt = 100;
            }
            resultString.text = "Dice : " + resultInt.ToString();
        }
        else if (DataBaseManager.condition == "Good")
        {
            if (int.Parse(nomalRollet1Txt.text) > int.Parse(goodRollet1Txt.text))
            {
                resultInt = int.Parse(nomalRollet10Txt.text) + int.Parse(goodRollet1Txt.text);
                if (resultInt == 0)
                {
                    resultInt = 100;
                }
                resultString.text = "Dice : " + resultInt.ToString();
            }
            else
            {
                resultInt = int.Parse(nomalRollet10Txt.text) + int.Parse(nomalRollet1Txt.text);
                if (resultInt == 0)
                {
                    resultInt = 100;
                }
                resultString.text = "Dice : " + resultInt.ToString();
            }
        }
        else if (DataBaseManager.condition == "Best")
        {
            if (int.Parse(nomalRollet10Txt.text) > int.Parse(goodRollet10Txt.text))
            {
                resultInt = int.Parse(goodRollet10Txt.text) + int.Parse(nomalRollet1Txt.text);
                if (resultInt == 0)
                {
                    resultInt = 100;
                }
                resultString.text = "Dice : " + resultInt.ToString();
            }
            else
            {
                resultInt = int.Parse(nomalRollet10Txt.text) + int.Parse(nomalRollet1Txt.text);
                if (resultInt == 0)
                {
                    resultInt = 100;
                }
                resultString.text = "Dice : " + resultInt.ToString();
            }
        }
        else if (DataBaseManager.condition == "Bad")
        {
            if (int.Parse(nomalRollet1Txt.text) > int.Parse(badRollet1Txt.text))
            {
                resultInt = int.Parse(nomalRollet10Txt.text) + int.Parse(nomalRollet1Txt.text);
                if (resultInt == 0)
                {
                    resultInt = 100;
                }
                resultString.text = "Dice : " + resultInt.ToString();
            }
            else
            {
                resultInt = int.Parse(nomalRollet10Txt.text) + int.Parse(badRollet1Txt.text);
                if (resultInt == 0)
                {
                    resultInt = 100;
                }
                resultString.text = "Dice : " + resultInt.ToString();
            }
        }
        else if (DataBaseManager.condition == "Worst")
        {
            if (int.Parse(nomalRollet10Txt.text) > int.Parse(badRollet10Txt.text))
            {
                resultInt = int.Parse(nomalRollet10Txt.text) + int.Parse(nomalRollet1Txt.text);
                if (resultInt == 0)
                {
                    resultInt = 100;
                }
                resultString.text = "Dice : " + resultInt.ToString();
            }
            else
            {
                resultInt = int.Parse(badRollet10Txt.text) + int.Parse(nomalRollet1Txt.text);
                if (resultInt == 0)
                {
                    resultInt = 100;
                }
                resultString.text = "Dice : " + resultInt.ToString();
            }
        }
        //적 관련
        enemyResultInt = int.Parse(enemyNomalRollet10Txt.text) + int.Parse(enemyNomalRollet1Txt.text);
        if (enemyResultInt == 0)
        {
            enemyResultInt = 100;
        }
        enemyResultString.text = "Dice : " + enemyResultInt.ToString();
        Invoke("GetStringResult", 0.5f);
    }
    void GetStringResult()
    {
        SoundManager.Instance.pen_Circle();
        if (resultInt >= 95)
        {
            resultEnd.text = "Result : Fumble";
        }
        else if (resultInt <= skillPoint / 3f)
        {
            resultEnd.text = "Result : Critical Success";
        }
        else if (resultInt <= skillPoint)
        {
            if (DataBaseManager.isDebuffPerfectionism == true)
            {
                resultEnd.text = "Result : Failure";
            }
            else
            {
                resultEnd.text = "Result : Success";
            }
        }
        else
        {
            resultEnd.text = "Result : Failure";
        }
        if (enemyResultInt >= 95)
        {
            enemyResultEnd.text = "Result : Fumble";
        }
        else if (enemyResultInt <= enemySkillPoint / 3f)
        {
            enemyResultEnd.text = "Result : Critical Success";
        }
        else if (enemyResultInt <= enemySkillPoint)
        {
            enemyResultEnd.text = "Result : Success";
        }
        else
        {
            enemyResultEnd.text = "Result : Failure";
        }
        Invoke("GetFinalResult", 0.5f);
    }

    public string finalResult;
    public TextMeshProUGUI finalResultTxt;
    void GetFinalResult()
    {
        if (subject == "evasion")
        {
            if (resultEnd.text == "Result : Critical Success")
            {
                finalResult = "Success";
                finalResultTxt.text = "Evasion : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text == "Result : Success")
            {
                finalResult = "Success";
                finalResultTxt.text = "Evasion : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text == "Result : Failure")
            {
                finalResult = "Success";
                finalResultTxt.text = "Evasion : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text == "Result : Fumble")
            {
                finalResult = "Success";
                finalResultTxt.text = "Evasion : " + finalResult;
            }
            else if (resultEnd.text == "Result : Failure" && enemyResultEnd.text == "Result : Failure")
            {
                finalResult = "Success";
                finalResultTxt.text = "Evasion : " + finalResult;
            }
            else if (resultEnd.text == "Result : Failure" && enemyResultEnd.text == "Result : Fumble")
            {
                finalResult = "Success";
                finalResultTxt.text = "Evasion : " + finalResult;
            }
            else if (resultEnd.text == "Result : Fumble" && enemyResultEnd.text == "Result : Fumble")
            {
                finalResult = "Success";
                finalResultTxt.text = "Evasion : " + finalResult;
            }
            else
            {
                finalResult = "Failure";
                finalResultTxt.text = "Evasion : " + finalResult;
            }
        } // 유리Result 예제
        if (subject == "counterattack")
        {
            if (resultEnd.text == "Result : Critical Success" && enemyResultEnd.text != "Result : Critical Success")
            {
                finalResult = "Success";
                finalResultTxt.text = "CounterAttack : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text != "Result : Critical Success" && enemyResultEnd.text != "Result : Success")
            {
                finalResult = "Success";
                finalResultTxt.text = "CounterAttack : " + finalResult;
            }
            else if (resultEnd.text == "Result : Failure" && enemyResultEnd.text != "Result : Critical Success" && enemyResultEnd.text != "Result : Success" && enemyResultEnd.text != "Result : Failure")
            {
                finalResult = "Success";
                finalResultTxt.text = "CounterAttack : " + finalResult;
            }
            else
            {
                finalResult = "Failure";
                finalResultTxt.text = "CounterAttack : " + finalResult;
            }
        } //불리Result 예제
        // 플레이어행동
        if (subject == "MA_attack_Counter")
        {
            if (resultEnd.text == "Result : Critical Success")
            {
                finalResult = "Success";
                finalResultTxt.text = "MartialArt : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text == "Result : Success")
            {
                finalResult = "Success";
                finalResultTxt.text = "MartialArt : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text == "Result : Failure")
            {
                finalResult = "Success";
                finalResultTxt.text = "MartialArt : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text == "Result : Fumble")
            {
                finalResult = "Success";
                finalResultTxt.text = "MartialArt : " + finalResult;
            }
            else if (resultEnd.text == "Result : Failure" && enemyResultEnd.text == "Result : Failure")
            {
                finalResult = "Success";
                finalResultTxt.text = "MartialArt : " + finalResult;
            }
            else if (resultEnd.text == "Result : Failure" && enemyResultEnd.text == "Result : Fumble")
            {
                finalResult = "Success";
                finalResultTxt.text = "MartialArt : " + finalResult;
            }
            else if (resultEnd.text == "Result : Fumble" && enemyResultEnd.text == "Result : Fumble")
            {
                finalResult = "Success";
                finalResultTxt.text = "MartialArt : " + finalResult;
            }
            else
            {
                finalResult = "Failure";
                finalResultTxt.text = "MartialArt : " + finalResult;
            }
        }
        if (subject == "SS_attack_Counter")
        {
            if (resultEnd.text == "Result : Critical Success")
            {
                finalResult = "Success";
                finalResultTxt.text = "Swordsmanship : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text == "Result : Success")
            {
                finalResult = "Success";
                finalResultTxt.text = "Swordsmanship : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text == "Result : Failure")
            {
                finalResult = "Success";
                finalResultTxt.text = "Swordsmanship : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text == "Result : Fumble")
            {
                finalResult = "Success";
                finalResultTxt.text = "Swordsmanship : " + finalResult;
            }
            else if (resultEnd.text == "Result : Failure" && enemyResultEnd.text == "Result : Failure")
            {
                finalResult = "Success";
                finalResultTxt.text = "Swordsmanship : " + finalResult;
            }
            else if (resultEnd.text == "Result : Failure" && enemyResultEnd.text == "Result : Fumble")
            {
                finalResult = "Success";
                finalResultTxt.text = "Swordsmanship : " + finalResult;
            }
            else if (resultEnd.text == "Result : Fumble" && enemyResultEnd.text == "Result : Fumble")
            {
                finalResult = "Success";
                finalResultTxt.text = "Swordsmanship : " + finalResult;
            }
            else
            {
                finalResult = "Failure";
                finalResultTxt.text = "Swordsmanship : " + finalResult;
            }
        }
        if (subject == "RunAway")
        {
            if (resultEnd.text == "Result : Critical Success")
            {
                finalResult = "Success";
                finalResultTxt.text = "Runaway : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text == "Result : Success")
            {
                finalResult = "Success";
                finalResultTxt.text = "Runaway : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text == "Result : Failure")
            {
                finalResult = "Success";
                finalResultTxt.text = "Runaway : " + finalResult;
            }
            else if (resultEnd.text == "Result : Success" && enemyResultEnd.text == "Result : Fumble")
            {
                finalResult = "Success";
                finalResultTxt.text = "Runaway : " + finalResult;
            }
            else if (resultEnd.text == "Result : Failure" && enemyResultEnd.text == "Result : Failure")
            {
                finalResult = "Success";
                finalResultTxt.text = "Runaway : " + finalResult;
            }
            else if (resultEnd.text == "Result : Failure" && enemyResultEnd.text == "Result : Fumble")
            {
                finalResult = "Success";
                finalResultTxt.text = "Runaway : " + finalResult;
            }
            else if (resultEnd.text == "Result : Fumble" && enemyResultEnd.text == "Result : Fumble")
            {
                finalResult = "Success";
                finalResultTxt.text = "Runaway : " + finalResult;
            }
            else
            {
                finalResult = "Failure";
                finalResultTxt.text = "Runaway : " + finalResult;
            }
        }
        endButton.SetActive(true);
    }
    public void EndRollet()
    {
        activeCheck = false;
        isClick = false;
        rolletSetUi.SetActive(false);
        rolletCheckUI.SetActive(true);
        rolletRollUI.SetActive(false);
        endButton.SetActive(false);

        if (subject == "evasion")
        {
            if (enemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(subject, finalResult, enemySubject);
            }
            else if (enemySubject == "DeepOne")
            {
                BattleManager.Instance.RetrunRolletResult(subject, finalResult, enemySubject);
            }
        }
        if (subject == "counterattack")
        {
            if (enemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(subject, finalResult, enemySubject);
            }
            else if (enemySubject == "DeepOne")
            {
                BattleManager.Instance.RetrunRolletResult(subject, finalResult, enemySubject);
            }
        }

        //플레이어행동
        if (subject == "MA_attack_Counter")
        {
            if (enemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(subject, finalResult, enemySubject);
            }
            else if (enemySubject == "DeepOne")
            {
                BattleManager.Instance.RetrunRolletResult(subject, finalResult, enemySubject);
            }
        }
        if (subject == "SS_attack_Counter")
        {
            if (enemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(subject, finalResult, enemySubject);
            }
            else if (enemySubject == "DeepOne")
            {
                BattleManager.Instance.RetrunRolletResult(subject, finalResult, enemySubject);
            }
        }

        if (subject == "dialog")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text);
            // 여기에 Dialog함수 적용   DialogManager.Instance.RetrunDialogResult(Sub_Dialog,result_End.text);
            // DilaogManager에서는 해당 함수에 interacitionMager를 연결해서 받은 주제와 Result값에 따른 문자를 출력하도록 함
        }
        if (subject == "RunAway")
        {
            if (finalResult == "Success")
            {
                if (DataBaseManager.nowPlace != "InSewer")
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
        }
    }

    // 중요!!!!!!!
    private void ResetString()
    {
        enemyNomalRollet1Txt.text = "-";
        enemyNomalRollet10Txt.text = "--";
        nomalRollet1Txt.text = "-";
        nomalRollet10Txt.text = "--";
        badRollet1Txt.text = "-";
        badRollet10Txt.text = "--";
        goodRollet1Txt.text = "-";
        goodRollet10Txt.text = "--";
        resultString.text = "Dice : --";
        resultEnd.text = "Result : --";
        enemyResultString.text = "Dice : --";
        enemyResultEnd.text = "Result : --";
        finalResultTxt.text = "---";
    }
    private void ResetRolletDice()
    {
        good1.SetActive(false);
        good10.SetActive(false);
        bad1.SetActive(false);
        bad10.SetActive(false);
        good1Rollet.SetActive(false);
        good10Rollet.SetActive(false);
        bad1Rollet.SetActive(false);
        bad10Rollet.SetActive(false);
    }
}