using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Rollet : MonoBehaviour
{
    bool activeCheck = false;
    bool activeCheck2 = false;
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
    public string enemySubject;
    public bool isClick;
    public bool isActiveRollet;
    private static Rollet instance = null;
    public GameObject endButton;
    string @string;
    public GameObject rolletUI;
    private void Update()
    {
        if (rolletUI.activeSelf == true && setUI.activeSelf == true && Input.GetKeyDown(KeyCode.Escape) && (subDialog != "SAN : Check" || subDialog != "Fabian : Sneak Out"))
        {
            activeCheck = false;
            DataBaseManager.cancelJudge = true;
            if (BattleManager.Instance.playerAction == "PlayerMarkmen")
            {
                BattleManager.Instance.enemySelectUI.SetActive(true);
                BattleManager.Instance.battleState = "selectEnemy";
                BattleManager.Instance.Player_setMarkmens();
            }
            else if (BattleManager.Instance.playerAction == "PlayerDeftness")
            {
                BattleManager.Instance.enemySelectUI.SetActive(true);
                BattleManager.Instance.battleState = "selectEnemy";
                BattleManager.Instance.Player_setDeftness();
            }
            rolletSetUi.SetActive(false);
            DataBaseManager.isRollet = false;
            DataBaseManager.isActiveDialog1 = false;
            DataBaseManager.isDirecting = false;
            DataBaseManager.isOpenUi = false;
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
        if (sanRollet.activeSelf == true && Input.GetKeyDown(KeyCode.E) && activeCheck2 == true)
        {
            activeCheck2 = false;
            activeSanRollet();
        }
        if (endButton2.activeSelf == true && Input.GetKeyDown(KeyCode.E))
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
        if (point_int >= 90)
        {
            point_int = 90;
        }
        if (point_int < 0)
        {
            point_int = 0;
        }
        enemySubject = Enemy;
        SoundManager.Instance.PaperClip_Play();
        DataBaseManager.isRollet = true;
        DataBaseManager.isOpenUi = true;
        this.subject = subject;
        subDialog = skill; // 나중에 다이얼로그 결과 전송시 사용
        ResetString();
        rolletSetUi.SetActive(true);
        this.skill.text = skill;
        if (skill != "??? : Deftness")
        {
            point.text = point_sting + " : " + point_int;
        }
        else
        {
            point.text = point_sting + " : ???";
        }
        skillPoint = point_int;
        @string = point_sting;
        if (DataBaseManager.condition == "Nomal")
        {
            ResetRolletDice();
            conditon.text = "Condition : Nomal";
            skillPoint1 = point_int % 10;
            skillPoint10 = point_int / 10;
            succesProbability = skillPoint10 * 0.1f + skillPoint1 * 0.01f;
        }
        else if (DataBaseManager.condition == "Good")
        {
            ResetRolletDice();
            conditon.text = "Condition : Good";
            good1.SetActive(true);
            good1Rollet.SetActive(true);
            skillPoint1 = point_int % 10;
            skillPoint10 = point_int / 10;
            succesProbability = skillPoint10 * 0.1f + (skillPoint1 * skillPoint1 + 2 * skillPoint1 * (10 - skillPoint1)) * 0.001f;
        }
        else if (DataBaseManager.condition == "Best")
        {
            ResetRolletDice();
            conditon.text = "Condition : Best";
            good10.SetActive(true);
            good10Rollet.SetActive(true);
            skillPoint1 = point_int % 10;
            skillPoint10 = point_int / 10;
            succesProbability = (skillPoint10 * skillPoint10 + 2 * skillPoint10 * (10 - skillPoint10)) * 0.01f + skillPoint1 * 0.001f;
        }
        else if (DataBaseManager.condition == "Bad")
        {
            ResetRolletDice();
            conditon.text = "Condition : Bad";
            bad1.SetActive(true);
            bad1Rollet.SetActive(true);
            skillPoint1 = point_int % 10;
            skillPoint10 = point_int / 10;
            succesProbability = skillPoint10 * 0.1f + skillPoint1 * skillPoint1 * 0.001f;
        }
        else if (DataBaseManager.condition == "Worst")
        {
            ResetRolletDice();
            conditon.text = "Condition : Worst";
            bad10.SetActive(true);
            bad10Rollet.SetActive(true);
            skillPoint1 = point_int % 10;
            skillPoint10 = point_int / 10;
            succesProbability = (skillPoint10 * skillPoint10 * 0.01f) + skillPoint1 * 0.001f;
        }
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
    public void WaitOneSec()
    {
        activeCheck = true;
    }
    public void activeRollet()
    {
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
        if (@string == "Sanity")
        {
            Invoke("OpenSanRollet", 1f);
        }
        else
        {
            endButton.SetActive(true);
        }
    }
    public void EndRollet()
    {
        SoundManager.Instance.EndDice();
        DataBaseManager.isOpenUi = false;
        DataBaseManager.isRollet = false;
        activeCheck = false;
        DataBaseManager.isRollet = false;
        isClick = false;
        rolletSetUi.SetActive(false);
        rolletCheckUI.SetActive(true);
        rolletRollUI.SetActive(false);
        endButton.SetActive(false);

        // 결과값을 전달하는 함수 필요
        if (subject == "dialog")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, enemySubject);
        }
        if (subject == "Item")
        {
            if (DataBaseManager.nowItem == "First aid kit")
            {
                DataBaseManager.firstaidkit -= 1;
                if (resultEnd.text == "Result : Success")
                {
                    BillowUIManager.Instance.HP_up((Random.Range(1, 7)) * 5);
                }
                else if (resultEnd.text == "Result : Critical Success")
                {
                    BillowUIManager.Instance.HP_up((Random.Range(1, 7)) * 5);
                }
            }
            if (DataBaseManager.nowItem == "Bandages")
            {
                DataBaseManager.bandages -= 1;
                if (resultEnd.text == "Result : Success")
                {
                    BillowUIManager.Instance.HP_up((Random.Range(1, 4)) * 5);
                }
                else if (resultEnd.text == "Result : Critical Success")
                {
                    BillowUIManager.Instance.HP_up((Random.Range(1, 4)) * 5);
                }
            }
            if (DataBaseManager.nowItem == "Painkillers")
            {
                DataBaseManager.painkillers -= 1;
                if (resultEnd.text == "Result : Success")
                {
                    BillowUIManager.Instance.San_up((Random.Range(1, 4)) * 5);
                }
                else if (resultEnd.text == "Result : Critical Success")
                {
                    BillowUIManager.Instance.San_up((Random.Range(1, 4)) * 5);
                }
            }
            if (DataBaseManager.nowItem == "Safe")
            {
                InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text);
            }
            if (DataBaseManager.nowItem == "Coat")
            {
                InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text);
            }
        }

        if (subject == "MS_attack")
        {
            if (enemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(subject, resultEnd.text, enemySubject);
                if (DataBaseManager.battleWeapon == "SmallPistol")
                {
                    DataBaseManager.nowSmallPistol -= 1;
                }
                if (DataBaseManager.battleWeapon == "Rifle")
                {
                    DataBaseManager.nowRifle -= 1;
                }
                if (DataBaseManager.battleWeapon == "Shotgun")
                {
                    DataBaseManager.nowShotgun -= 1;
                }
                if (DataBaseManager.battleWeapon == "Revolver")
                {
                    DataBaseManager.nowRevolver -= 1;
                }
            }
            if (enemySubject == "DeepOne")
            {
                BattleManager.Instance.RetrunRolletResult(subject, resultEnd.text, enemySubject);
                if (DataBaseManager.battleWeapon == "SmallPistol")
                {
                    DataBaseManager.nowSmallPistol -= 1;
                }
                if (DataBaseManager.battleWeapon == "Rifle")
                {
                    DataBaseManager.nowRifle -= 1;
                }
                if (DataBaseManager.battleWeapon == "Shotgun")
                {
                    DataBaseManager.nowShotgun -= 1;
                }
                if (DataBaseManager.battleWeapon == "Revolver")
                {
                    DataBaseManager.nowRevolver -= 1;
                }
            }
        }
        if (subject == "Deftness_attack")
        {
            if (enemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(subject, resultEnd.text, enemySubject);
                if (DataBaseManager.battleWeapon == "Molotov")
                {
                    DataBaseManager.molotov -= 1;
                }
            }
            if (enemySubject == "DeepOne")
            {
                BattleManager.Instance.RetrunRolletResult(subject, resultEnd.text, enemySubject);
                if (DataBaseManager.battleWeapon == "Molotov")
                {
                    DataBaseManager.molotov -= 1;
                }
            }
        }
        if (subject == "EnemyDeftness")
        {
            BattleManager.Instance.RetrunRolletResult(subject, resultEnd.text, enemySubject);
        }
        if (subject == "DeepOneDeftness")
        {
            BattleManager.Instance.RetrunRolletResult(subject, resultEnd.text, enemySubject);
        }
        if (subject == "Case2IntCheck") // 이성판정 광련
        {
            InteractionController.Instance.RetrunDialogResult("Case2IntCheck", resultEnd.text);
        }
    }

    public GameObject endButton2;
    public TextMeshProUGUI sanName;
    public TextMeshProUGUI slot1Name;
    public TextMeshProUGUI slot1Percent;
    public TextMeshProUGUI slot2Name;
    public TextMeshProUGUI slot2Percent;
    public TextMeshProUGUI slot3Name;
    public TextMeshProUGUI slot3Percent;
    public TextMeshProUGUI sanRolletText;
    public GameObject sanRollet;
    int slot1Int;
    int slot2Int;
    int slot3Int;
    public GameObject sanM10;
    public GameObject sanM5;

    //Level - 1 Debuff
    public GameObject thinWallets;
    public GameObject carelessness;
    public GameObject debilitation;
    public GameObject dizziness;
    public GameObject sprains;
    public GameObject unlucky;
    public GameObject musclePain;
    public GameObject migraines;
    public GameObject eyeDisease;

    //Level - 2 Debuff
    public GameObject masochism;
    public GameObject careless;
    public GameObject mentalWeakness;
    public GameObject helplessness;
    public GameObject extravagant;
    public GameObject panicAttack;
    public GameObject medicaldistrust;
    public GameObject hallucinations;
    public GameObject tightwad;

    //Level - 3 Debuff
    public GameObject nightPhobia;
    public GameObject deafness;
    public GameObject visionLoss;
    public GameObject shortTempered;
    public GameObject dyslexia;
    public GameObject drugPhobia;
    public GameObject cognitiveDisorder;
    public GameObject cognitiveBreakdown;
    public GameObject homicidalImpulse;
    public GameObject perfectionism;

    //condition
    public GameObject best;
    public GameObject good;
    public GameObject nomal;
    public GameObject bad;
    public GameObject worst;
    public TextMeshProUGUI detailSan;
    public void OpenSanRollet()
    {
        activeButton.SetActive(true);
        SoundManager.Instance.PaperClip_Play();
        sanRollet.SetActive(true);
        Invoke("WaitOneSec2", 0.1f);
        if (resultEnd.text == "Result : Fumble" || resultEnd.text == "Result : Failure")
        {
            if (DataBaseManager.debuff == 0)
            {
                if (DataBaseManager.condition != "Worst")
                {
                    sanName.text = "!Sanity check failed!";
                    slot1Name.text = "Sanity Decline";
                    slot1Percent.text = "50%";
                    slot1Int = 50;
                    slot2Name.text = "Manifest Insanity";
                    slot2Percent.text = "30%";
                    slot2Int = slot1Int + 30;
                    slot3Name.text = "Deconditioning";
                    slot3Percent.text = "20%";
                    slot3Int = slot2Int + 20;
                }
                else
                {
                    sanName.text = "!Sanity check failed!";
                    slot1Name.text = "Sanity Decline";
                    slot1Percent.text = "50%";
                    slot1Int = 50;
                    slot2Name.text = "Manifest Insanity";
                    slot2Percent.text = "50%";
                    slot2Int = slot1Int + 50;
                    slot3Name.text = "Deconditioning";
                    slot3Percent.text = "---";
                    slot3Int = slot2Int + 999;
                }

            }
            else if (DataBaseManager.debuff == 1)
            {
                if (DataBaseManager.condition != "Worst")
                {
                    sanName.text = "!Sanity check failed!";
                    slot1Name.text = "Sanity Decline";
                    slot1Percent.text = "30%";
                    slot1Int = 30;
                    slot2Name.text = "Manifest Insanity";
                    slot2Percent.text = "50%";
                    slot2Int = slot1Int + 50;
                    slot3Name.text = "Deconditioning";
                    slot3Percent.text = "20%";
                    slot3Int = slot2Int + 20;
                }
                else
                {
                    sanName.text = "!Sanity check failed!";
                    slot1Name.text = "Sanity Decline";
                    slot1Percent.text = "30%";
                    slot1Int = 30;
                    slot2Name.text = "Manifest Insanity";
                    slot2Percent.text = "70%";
                    slot2Int = slot1Int + 70;
                    slot3Name.text = "Deconditioning";
                    slot3Percent.text = "---";
                    slot3Int = slot2Int + 999;
                }
            }
            else if (DataBaseManager.debuff == 2)
            {
                if (DataBaseManager.condition != "Worst")
                {
                    sanName.text = "!Sanity check failed!";
                    slot1Name.text = "Sanity Decline";
                    slot1Percent.text = "20%";
                    slot1Int = 20;
                    slot2Name.text = "Manifest Insanity";
                    slot2Percent.text = "60%";
                    slot2Int = slot1Int + 60;
                    slot3Name.text = "Deconditioning";
                    slot3Percent.text = "20%";
                    slot3Int = slot2Int + 20;
                }
                else
                {
                    sanName.text = "!Sanity check failed!";
                    slot1Name.text = "Sanity Decline";
                    slot1Percent.text = "20%";
                    slot1Int = 20;
                    slot2Name.text = "Manifest Insanity";
                    slot2Percent.text = "80%";
                    slot2Int = slot1Int + 80;
                    slot3Name.text = "Deconditioning";
                    slot3Percent.text = "---";
                    slot3Int = slot2Int + 999;
                }
            }
            else if (DataBaseManager.debuff == 3)
            {
                if (DataBaseManager.condition != "Worst")
                {
                    sanName.text = "!Sanity check failed!";
                    slot1Name.text = "Sanity Decline";
                    slot1Percent.text = "40%";
                    slot1Int = 40;
                    slot2Name.text = "Manifest Insanity";
                    slot2Percent.text = "---";
                    slot3Name.text = "Deconditioning";
                    slot3Percent.text = "60%";
                    slot3Int = slot2Int + 60;
                }
                else
                {
                    sanName.text = "!Sanity check failed!";
                    slot1Name.text = "Sanity Decline";
                    slot1Percent.text = "100%";
                    slot1Int = 100;
                    slot2Name.text = "Manifest Insanity";
                    slot2Percent.text = "---%";
                    slot2Int = slot1Int + 999;
                    slot3Name.text = "Deconditioning";
                    slot3Percent.text = "---";
                    slot3Int = slot2Int + 999;
                }
            }
        }
        else
        {
            sanName.text = "Sanity check Success";
            slot1Name.text = "No change";
            slot1Percent.text = "80%";
            slot1Int = 80;
            slot2Name.text = "Rise sanity";
            slot2Percent.text = "15%";
            slot2Int = 95;
            slot3Name.text = "Raise Condition";
            slot3Percent.text = "5%";
            slot3Int = 100;
        }
    }
    public GameObject activeButton;
    public void activeSanRollet()
    {
        activeButton.SetActive(false);
        ChangeSanRollet();
    }
    public void WaitOneSec2()
    {
        activeCheck2 = true;
    }
    void ChangeSanRollet()
    {
        SoundManager.Instance.pen_Line();
        if (sanName.text == "!Sanity check failed!")
        {
            int SanRollet = (Random.Range(1, 101));
            Debug.Log(SanRollet);

            if (DataBaseManager.debuff == 3)
            {
                if (SanRollet <= slot1Int && SanRollet <= slot3Int)
                {
                    sanRolletText.text = "Sanity Decline";
                }
                else
                {
                    sanRolletText.text = "Deconditioning";
                }
            }
            else
            {
                if (SanRollet <= slot1Int && SanRollet <= slot2Int && SanRollet <= slot3Int)
                {
                    sanRolletText.text = "Sanity Decline";
                }
                else if (SanRollet >= slot1Int && SanRollet <= slot2Int && SanRollet <= slot3Int)
                {
                    sanRolletText.text = "Manifest Insanity";
                }
                else if (SanRollet >= slot1Int && SanRollet >= slot2Int && SanRollet <= slot3Int)
                {
                    sanRolletText.text = "Deconditioning";
                }
            }
        }
        else if (sanName.text == "Sanity check Success")
        {
            int SanRollet = (Random.Range(1, 101));
            Debug.Log(SanRollet);
            if (SanRollet <= slot1Int)
            {
                sanRolletText.text = "No change";
            }
            else if (SanRollet <= slot2Int)
            {
                sanRolletText.text = "Rise sanity";
            }
            else
            {
                sanRolletText.text = "Raise Condition";
            }
        }
        Invoke("ResultRollet", 1);
    }
    void ResultRollet()
    {
        SoundManager.Instance.pen_Circle();
        if (sanRolletText.text == "Sanity Decline")
        {
            int SanRollet = (Random.Range(0, 3));
            if (SanRollet == 1)
            {
                detailSan.text = "Sanity Decline : 10\n\nThe current sanity reduced by 10 Point.";
                BillowUIManager.Instance.San_Down(10);
                sanM10.SetActive(true);
            }
            else
            {
                detailSan.text = "Sanity Decline : 5\n\nThe current sanity reduced by 5 Point.";
                BillowUIManager.Instance.San_Down(5);
                sanM5.SetActive(true);
            }
        }
        else if (sanRolletText.text == "Manifest Insanity")
        {
            if (DataBaseManager.debuff == 0)
            {
                DataBaseManager.debuff += 1;
                int DebuffRollet = (Random.Range(1, 10));
                switch (DebuffRollet)
                {
                    case 1:
                        detailSan.text = "Level 1 : ThinWallets\n\nYou lose 5 of your wealth Point.";
                        DataBaseManager.thinWallets = true;

                        thinWallets.SetActive(true);
                        if (DataBaseManager.weal > 5)
                        {
                            DataBaseManager.weal -= 5;
                        }
                        else
                        {
                            DataBaseManager.weal = 0;
                        }
                        SIS_UIManager.Instance.ThinWallets_Open();
                        break;
                    case 2:
                        carelessness.SetActive(true);
                        DataBaseManager.hp -= 5;
                        DataBaseManager.nowHP -= 5;
                        BillowUIManager.Instance.resetHP();
                        BillowUIManager.Instance.HP_down(5);
                        detailSan.text = "Level 1 : Carelessness\n\nYou lose 5 HP and max HP Point.";
                        DataBaseManager.carelessness = true;
                        SIS_UIManager.Instance.Carelessness_Open();
                        break;
                    case 3:
                        debilitation.SetActive(true);
                        DataBaseManager.str -= 10;
                        detailSan.text = "Level 1 : Debilitation\n\nYou lose 10 Str Point";
                        DataBaseManager.debilitation = true;
                        SIS_UIManager.Instance.Debilitation_Open();
                        break;
                    case 4:
                        dizziness.SetActive(true);
                        DataBaseManager.intl -= 10;
                        detailSan.text = "Level 1 : Dizziness\n\nYou lose 10 Int Point";
                        DataBaseManager.dizziness = true;
                        SIS_UIManager.Instance.Dizziness_Open();
                        break;
                    case 5:
                        sprains.SetActive(true);
                        DataBaseManager.dex -= 10;
                        detailSan.text = "Level 1 : Sprains\n\nYou lose 10 Dex Point";
                        DataBaseManager.sprains = true;
                        SIS_UIManager.Instance.Sprains_Open();
                        break;
                    case 6:
                        unlucky.SetActive(true);
                        DataBaseManager.luk -= 10;
                        detailSan.text = "Level 1 : Unlucky\n\nYou lose 10 Luk Point";
                        DataBaseManager.unlucky = true;
                        SIS_UIManager.Instance.Unlucky_Open();
                        break;
                    case 7:
                        musclePain.SetActive(true);
                        DataBaseManager.gunShotPoint -= 10;
                        DataBaseManager.martialArtsPoint -= 10;
                        DataBaseManager.swimingPoint -= 10;
                        DataBaseManager.swordPoint -= 10;
                        DataBaseManager.observationPoint -= 10;
                        detailSan.text = "Level 1 : MusclePain\n\nAll skills based on Str are reduced by 10 Point.";
                        DataBaseManager.musclePain = true;
                        SIS_UIManager.Instance.MusclePain_Open();
                        break;
                    case 8:
                        migraines.SetActive(true);
                        DataBaseManager.medicinePoint -= 10;
                        DataBaseManager.analysisPoint -= 10;
                        DataBaseManager.listeningPoint -= 10;
                        if (DataBaseManager.psychotherapyPoint > 10)
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
                        detailSan.text = "Level 1 : Migraines\n\nAll skills based on Int are reduced by 10 Point.";
                        DataBaseManager.migraines = true;
                        SIS_UIManager.Instance.Migraines_Open();
                        break;
                    case 9:
                        eyeDisease.SetActive(true);
                        DataBaseManager.evasionPoint -= 10;
                        DataBaseManager.deftnessPoint -= 10;
                        DataBaseManager.rhetoricPoint -= 10;
                        DataBaseManager.stealthPoint -= 10;
                        DataBaseManager.disguisePoint -= 10;
                        detailSan.text = "Level 1 : EyeDisease\n\nAll skills based on Dex are reduced by 10 Point.";
                        DataBaseManager.eyeDisease = true;
                        SIS_UIManager.Instance.EyeDisease_Open();
                        break;
                }
            }
            else if (DataBaseManager.debuff == 1)
            {
                DataBaseManager.debuff += 1;
                int DebuffRollet = (Random.Range(1, 8));
                switch (DebuffRollet)
                {
                    case 1:
                        detailSan.text = "Level 2 : Masochism\n\nThe player takes an additional 5 HP damage when hit.(100%)";
                        masochism.SetActive(true);
                        DataBaseManager.masochism = true;
                        SIS_UIManager.Instance.Masochism_Open();
                        break;
                    case 2:
                        detailSan.text = "Level 2 : Careless\n\nYou lose one of the items you're currently carrying.";
                        careless.SetActive(true);
                        DataBaseManager.nowItem = "";
                        DataBaseManager.careless = true;
                        SIS_UIManager.Instance.CareLessActive();
                        SIS_UIManager.Instance.Careless_Open();
                        break;
                    case 3:
                        detailSan.text = "Level 2 : MentalWeakness\n\nThe player takes an additional 5 SAN damage when hit.(100%)";
                        mentalWeakness.SetActive(true);
                        DataBaseManager.mentalWeakness = true;
                        SIS_UIManager.Instance.MentalWeakness_Open();
                        break;
                    case 4:
                        detailSan.text = "Level 2 : Helplessness\n\nMovement speed is reduced by 30%.";
                        helplessness.SetActive(true);
                        DataBaseManager.helplessness = true;
                        SIS_UIManager.Instance.Helplessness_Open();
                        break;
                    case 5:
                        detailSan.text = "Level 2 : Extravagant\n\nThe cost of store purchases increases.";
                        extravagant.SetActive(true);
                        ShopUI.Instance.ExtravoltON();
                        DataBaseManager.extravagant = true;
                        SIS_UIManager.Instance.Extravagant_Open();
                        break;
                    case 6:
                        detailSan.text = "Level 2 : PanicAttack\n\nChance to 5 HP damage when traveling through an area.(50%)";
                        panicAttack.SetActive(true);
                        DataBaseManager.panicAttack = true;
                        SIS_UIManager.Instance.PanicAttack_Open();
                        break;
                    case 7:
                        detailSan.text = "Level 2 : Tightwad\n\nThe store is no longer available.";
                        tightwad.SetActive(true);
                        DataBaseManager.tightwad = true;
                        SIS_UIManager.Instance.Tightwad_Open();
                        break;
                    case 8:
                        hallucinations.SetActive(true);      // 나중에 연출로 해서 넣기~~~~~~~~~~~~
                        SIS_UIManager.Instance.Hallucinations_Open();
                        break;
                    case 9:
                        medicaldistrust.SetActive(true);   // 나중에 상점 추가된 뒤에 해서 넣기~~~~~~~~~~~~
                        SIS_UIManager.Instance.Medicaldistrust_Open();
                        break;
                }
            }
            else if (DataBaseManager.debuff == 2)
            {
                DataBaseManager.debuff += 1;
                int DebuffRollet = (Random.Range(1, 10));
                switch (DebuffRollet)
                {
                    case 1:
                        detailSan.text = "Level 3 : NightPhobia\n\nThe player can't walk around at night anymore.";
                        nightPhobia.SetActive(true);
                        DataBaseManager.isDebuffNightPhobia = true;
                        SIS_UIManager.Instance.NightPhobia_Open();
                        break;
                    case 2:
                        detailSan.text = "Level 3 : Deafness\n\nThe player can no longer hear the sound.";
                        deafness.SetActive(true);
                        DataBaseManager.isDebuffDeftness = true;
                        SIS_UIManager.Instance.Deafness_Open();
                        break;
                    case 3:
                        detailSan.text = "Level 3 : VisionLoss\n\nThe player narrows the field of view.";
                        visionLoss.SetActive(true);
                        DataBaseManager.isDebuffVisionLoss = true;
                        SIS_UIManager.Instance.VisionLoss_Open();
                        break;
                    case 4:
                        detailSan.text = "Level 3 : Short-Tempered\n\nThe player can't run away from the battle.";
                        shortTempered.SetActive(true);
                        DataBaseManager.isDebuffShortTempred = true;
                        SIS_UIManager.Instance.ShortTempered_Open();
                        break;
                    case 5:
                        detailSan.text = "Level 3 : Dyslexia\n\nThe Detective Notebook is no longer available.";
                        dyslexia.SetActive(true);
                        DataBaseManager.isDebuffDyslexia = true;
                        SIS_UIManager.Instance.Dyslexia_Open();
                        break;
                    case 6:
                        detailSan.text = "Level 3 : DrugPhobia\n\nCure items are no longer available.";
                        drugPhobia.SetActive(true);
                        DataBaseManager.isDebuffDrugPhobia = true;
                        SIS_UIManager.Instance.DrugPhobia_Open();
                        break;
                    case 7:
                        detailSan.text = "Level 3 : CognitiveDisorder\n\nThe player will be taken to a random place when using the map.";
                        cognitiveDisorder.SetActive(true);
                        DataBaseManager.isDebuffCognitiveDisorder = true;
                        SIS_UIManager.Instance.CognitiveDisorder_Open();
                        break;
                    case 8:
                        detailSan.text = "Level 3 : CognitiveBreakdown\n\nThe Player information is no longer available.";
                        cognitiveBreakdown.SetActive(true);
                        SIS_UIManager.Instance.CognitiveBreakdown_Open();
                        DataBaseManager.isDebuffCognitiveBreakdown = true;
                        break;
                    case 9:
                        detailSan.text = "Level 3 : Perfectionism\n\nUnless it's an Critical Success, the judgment will fail.";
                        perfectionism.SetActive(true);
                        SIS_UIManager.Instance.Perfectionism_Open();
                        DataBaseManager.isDebuffPerfectionism = true;
                        break;
                    case 10:
                        detailSan.text = "Level 3 : NightPhobia\n\n";
                        homicidalImpulse.SetActive(true);
                        SIS_UIManager.Instance.HomicidalImpulse_Open();
                        break;
                }
            }
        }
        else if (sanRolletText.text == "Deconditioning")
        {
            if (DataBaseManager.condition == "Best")
            {
                good.SetActive(true);
                detailSan.text = "Deconditioning : Good\n\nCondition has dropped to (Good).";
                DataBaseManager.condition = "Good";
            }
            else if (DataBaseManager.condition == "Good")
            {
                nomal.SetActive(true);
                detailSan.text = "Deconditioning : Nomal\n\nCondition has dropped to (Nomal).";
                DataBaseManager.condition = "Nomal";
            }
            else if (DataBaseManager.condition == "Nomal")
            {
                bad.SetActive(true);
                detailSan.text = "Deconditioning : Bad\n\nCondition has dropped to (Bad).";
                DataBaseManager.condition = "Bad";
            }
            else if (DataBaseManager.condition == "Bad")
            {
                worst.SetActive(true);
                detailSan.text = "Deconditioning : Worst   \n\nCondition has dropped to (Worst).";
                DataBaseManager.condition = "Worst";
            }
            else if (DataBaseManager.condition == "Worst")
            {
            }
        }
        else if (sanRolletText.text == "Rise sanity")
        {
            int SanRollet = (Random.Range(0, 3));
            if (SanRollet == 1)
            {
                detailSan.text = "Rise sanity : 10\n\nThe current sanity rised by 10 Point.";
                BillowUIManager.Instance.San_up(10);
                sanM10.SetActive(true);
            }
            else
            {
                detailSan.text = "Rise sanity : 5\n\nThe current sanity rised by 5 Point.";
                BillowUIManager.Instance.San_up(5);
                sanM5.SetActive(true);
            }
        }
        else if (sanRolletText.text == "Raise Condition")
        {
            if (DataBaseManager.condition == "Best")
            {
            }
            else if (DataBaseManager.condition == "Good")
            {
                detailSan.text = "Raise Condition : Best\n\nCondition has raised to (Best).";
                best.SetActive(true);
                DataBaseManager.condition = "Best";
            }
            else if (DataBaseManager.condition == "Nomal")
            {
                detailSan.text = "Raise Condition : Good\n\nCondition has raised to (Good).";
                good.SetActive(true);
                DataBaseManager.condition = "Good";
            }
            else if (DataBaseManager.condition == "Bad")
            {
                detailSan.text = "Raise Condition : Nomal\n\nCondition has raised to (Nomal).";
                nomal.SetActive(true);
                DataBaseManager.condition = "Nomal";
            }
            else if (DataBaseManager.condition == "Worst")
            {
                detailSan.text = "Raise Condition : Bad\n\nCondition has raised to (Bad).";
                bad.SetActive(true);
                DataBaseManager.condition = "Bad";
            }
        }
        else if (sanRolletText.text == "No change")
        {
        }
        endButton2.SetActive(true);
    }
    public void EndSanRollet()
    {
        SoundManager.Instance.EndDice();
        rolletSetUi.SetActive(false);
        rolletCheckUI.SetActive(true);
        rolletRollUI.SetActive(false);

        activeCheck = false;
        DataBaseManager.isOpenUi = false;
        DataBaseManager.isRollet = false;
        isClick = false;
        sanRollet.SetActive(false);
        endButton2.SetActive(false);
        resetSanRollet();
        // 결과값을 전달하는 함수 필요
        if (subject == "RoadBattle1")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text);
        }
        if (subject == "Fab_Case1")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text);
        }
        if (subject == "NoonEvent")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "NoonEvent_FirstDoor");
        }
        if (subject == "KaneSan")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "KaneSan");
        }
        if (subject == "DrinkSan")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "DrinkSan");
        }
        if (subject == "ThirdDayStartSan")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "ThirdDayStartSan");
        }
        if (subject == "DayTwoCase2San")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "DayTwoCase2San");
        }
        if (subject == "JournalSan")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "JournalSan");
        }
        if (subject == "BookSan")

        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "BookSan");
        }
        if (subject == "FourthDayStartSan")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "FourthDayStartSan");
        }
        if (subject == "Fab_4th_QuestO_SanCheck")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "Fab_4th_QuestO_SanCheck");
        }
        if (subject == "SymbolStrSan")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "SymbolStrSan");
        }
        if (subject == "BattleSan_DeepOne1")
        {
            BattleManager.Instance.EndDeepOneSanAttack();
        }
        if (subject == "DeepOneFirstSan")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "DeepOneFirstSan");
        }
        if (subject == "Sewer_FirstInSan")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "Sewer_FirstInSan");
        }
        if (subject == "EndingLedderSan")
        {
            InteractionController.Instance.RetrunDialogResult(subDialog, resultEnd.text, "EndingLedderSan");
        }
    }
    void resetSanRollet()
    {
        endButton2.SetActive(false);
        detailSan.text = "";
        sanName.text = "";
        slot1Name.text = "";
        slot1Percent.text = "";
        slot2Name.text = "";
        slot2Percent.text = "";
        slot3Name.text = "";
        slot3Percent.text = "";
        sanRolletText.text = "";

        slot1Int = 0;
        slot2Int = 0;
        slot3Int = 0;

        sanM10.SetActive(false);
        sanM5.SetActive(false);

        //Level - 1 Debuff
        thinWallets.SetActive(false);
        carelessness.SetActive(false);
        debilitation.SetActive(false);
        dizziness.SetActive(false);
        sprains.SetActive(false);
        unlucky.SetActive(false);
        musclePain.SetActive(false);
        migraines.SetActive(false);
        eyeDisease.SetActive(false);

        masochism.SetActive(false);
        careless.SetActive(false);
        mentalWeakness.SetActive(false);
        helplessness.SetActive(false);
        extravagant.SetActive(false);
        panicAttack.SetActive(false);
        medicaldistrust.SetActive(false);
        hallucinations.SetActive(false);
        tightwad.SetActive(false);

        //Level - 3 Debuff
        nightPhobia.SetActive(false);
        deafness.SetActive(false);
        visionLoss.SetActive(false);
        shortTempered.SetActive(false);
        dyslexia.SetActive(false);
        drugPhobia.SetActive(false);
        cognitiveDisorder.SetActive(false);
        cognitiveBreakdown.SetActive(false);
        homicidalImpulse.SetActive(false);
        perfectionism.SetActive(false);
        best.SetActive(false);
        good.SetActive(false);
        nomal.SetActive(false);
        bad.SetActive(false);
        worst.SetActive(false);
    }

    // 중요!!!!!!!
    private void ResetString()
    {
        nomalRollet1Txt.text = "-";
        nomalRollet10Txt.text = "--";
        badRollet1Txt.text = "-";
        badRollet10Txt.text = "--";
        goodRollet1Txt.text = "-";
        goodRollet10Txt.text = "--";
        resultString.text = "Dice : --";
        resultEnd.text = "Result : --";
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