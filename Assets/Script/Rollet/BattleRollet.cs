using SCoW.Core.Rules;
using SCoW.Core.Ui;
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
        if (rolletUI.activeSelf == true && setUI.activeSelf == true && UiGate.TryCancel(PanelKind.Rollet) && subDialog != "SAN : Check")
        {
            activeCheck = false;
            DataBaseManager.cancelJudge = true;
            if (BattleManager.Instance.playerAction == "PlayerSwords")
            {
                BattleManager.Instance.enemySelectUI.SetActive(true);
                BattleManager.Instance.battleState = "selectEnemy";
                BattleManager.Instance.PlayerSetSwords();
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
        if (setUI.activeSelf == true && GameInput.InteractDown && activeCheck == true)
        {
            activeCheck = false;
            ActiveRollet();
        }
        if (endButton.activeSelf == true && GameInput.InteractDown)
        {
            EndRollet();
        }
        {
            if (isClick == false && isActiveRollet == true)
            {
                if (GameInput.AdvanceDown || GameInput.InteractDown)
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
    public void SetBattleRollet(string skill, string point_sting, int point_int, string subject, string EnemyName, string E_skill, string E_point_string, int E_Point_int)
    {
        SoundManager.Instance.PaperClipPlay();
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

    public void ActiveRollet()
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
        DiceFaces f = SkillCheck.RollFaces(UnityRng.Instance);
        nomalRollet1Txt.text = f.Units.ToString();
        nomalRollet10Txt.text = TensText(f.Tens);
        badRollet1Txt.text = f.BadUnits.ToString();
        badRollet10Txt.text = TensText(f.BadTens);
        goodRollet1Txt.text = f.GoodUnits.ToString();
        goodRollet10Txt.text = TensText(f.GoodTens);
        //적
        enemyNomalRollet1Txt.text = UnityRng.Instance.Next(0, 10).ToString();
        enemyNomalRollet10Txt.text = TensText(UnityRng.Instance.Next(0, 10) * 10);
    }
    static string TensText(int tens)
    {
        return tens != 0 ? tens.ToString() : "00";
    }
    void GetIntResult()
    {
        SoundManager.Instance.PenLine();
        DiceFaces faces = new DiceFaces(int.Parse(nomalRollet1Txt.text), int.Parse(nomalRollet10Txt.text),
            int.Parse(badRollet1Txt.text), int.Parse(badRollet10Txt.text),
            int.Parse(goodRollet1Txt.text), int.Parse(goodRollet10Txt.text));
        resultInt = SkillCheck.ResolveValue(SkillCheck.ParseCondition(DataBaseManager.condition), faces);
        resultString.text = "Dice : " + resultInt.ToString();
        //적 관련
        enemyResultInt = SkillCheck.ResolveValue(Condition.Nomal, new DiceFaces(int.Parse(enemyNomalRollet1Txt.text), int.Parse(enemyNomalRollet10Txt.text), 0, 0, 0, 0));
        enemyResultString.text = "Dice : " + enemyResultInt.ToString();
        Invoke("GetStringResult", 0.5f);
    }
    void GetStringResult()
    {
        SoundManager.Instance.PenCircle();
        resultEnd.text = SkillCheck.ToLegacyString(SkillCheck.Evaluate(resultInt, skillPoint, DataBaseManager.isDebuffPerfectionism));
        enemyResultEnd.text = SkillCheck.ToLegacyString(SkillCheck.Evaluate(enemyResultInt, enemySkillPoint));
        Invoke("GetFinalResult", 0.5f);
    }

    public string finalResult;
    public TextMeshProUGUI finalResultTxt;
    void GetFinalResult()
    {
        // 대립 판정(회피·반격·무술·검술·도주) 규칙은 Core BattleEngine.ResolveContest 로 이행(P14a). 그 외 subject 는 finalResult 를 건드리지 않는다(옛 동작).
        ContestKind kind;
        if (BattleEngine.TryParseContest(subject, out kind))
        {
            bool success = BattleEngine.ResolveContest(kind, BattleEngine.ParseLegacyLevel(resultEnd.text), BattleEngine.ParseLegacyLevel(enemyResultEnd.text));
            finalResult = success ? "Success" : "Failure";
            finalResultTxt.text = BattleEngine.ContestLabel(kind) + " : " + finalResult;
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
                    BattleManager.Instance.ExBattleEnd();
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