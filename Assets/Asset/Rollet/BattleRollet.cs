using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleRollet : MonoBehaviour
{
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


    // �� ĭ
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
        if (isClick == false && isActiveRollet == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CancelInvoke();
                isClick = true;
                isActiveRollet = false;
                Invoke("GetIntResult", 1f);
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

    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
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
        Subject = subject;
        Sub_Dialog = skill; // ���߿� ���̾�α� ��� ���۽� ���
        ResetString();
        RolletSetUi.SetActive(true);
        //�÷��̾�
        Skill.text = skill;
        Point.text = point_sting + " : " + point_int;
        //��
        EnemySubject = EnemyName;
        E_Skill.text = E_skill;
        E_Point.text = E_point_string + " :  ??";

        skillPoint = point_int;
        E_skillPoint = E_Point_int;
        if (DataBaseManager.Condition == "Nomal")
        {
            ResetRolletDice();
            Conditon.text = "����� : ����";

            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = SkillPoint_10 * 0.1f + SkillPoint_1 * 0.01f;
        }
        else if (DataBaseManager.Condition == "Good")
        {
            ResetRolletDice();
            Conditon.text = "����� : ����";
            Good_1.SetActive(true);
            Good_1_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = SkillPoint_10 * 0.1f + (SkillPoint_1 * SkillPoint_1 + 2 * SkillPoint_1 * (10 - SkillPoint_1)) * 0.001f;
        }
        else if (DataBaseManager.Condition == "Best")
        {
            ResetRolletDice();
            Conditon.text = "����� : �ֻ�";
            Good_10.SetActive(true);
            Good_10_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = (SkillPoint_10 * SkillPoint_10 + 2 * SkillPoint_10 * (10 - SkillPoint_10)) * 0.01f + SkillPoint_1 * 0.001f;
        }
        else if (DataBaseManager.Condition == "Bad")
        {
            ResetRolletDice();
            Conditon.text = "����� : ����";
            Bad_1.SetActive(true);
            Bad_1_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = SkillPoint_10 * 0.1f + SkillPoint_1 * SkillPoint_1 * 0.001f;
        }
        else if (DataBaseManager.Condition == "Worst")
        {
            ResetRolletDice();
            Conditon.text = "����� : �־�";
            Bad_10.SetActive(true);
            Bad_10_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = (SkillPoint_10 * SkillPoint_10 * 0.01f) + SkillPoint_1 * 0.001f;
        }

        // �� Ȯ�� ���.
        E_Expect.text = "�������� : ����Ұ�";

        if (SuccesProbability < 0.20)
        {
            Expect.text = "�������� : �ſ� �����";
        }
        else if (SuccesProbability < 0.40)
        {
            Expect.text = "�������� : �����";
        }
        else if (SuccesProbability < 0.60)
        {
            Expect.text = "�������� : ����";
        }
        else if (SuccesProbability < 0.80)
        {
            Expect.text = "�������� : ����";
        }
        else if (SuccesProbability < 1)
        {
            Expect.text = "�������� : �ſ� ����";
        }
    }
    public void activeRollet()
    {

        setUI.SetActive(false);
        activeUI.SetActive(true);
        InvokeRepeating("ChangeRollet", 1, 0.05f);

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


        //��
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
        if (DataBaseManager.Condition == "Nomal")
        {
            result_int = int.Parse(NomalRollet_10t.text) + int.Parse(NomalRollet_1t.text);
            if (result_int == 0)
            {
                result_int = 100;
            }
            result_string.text = "��� : " + result_int.ToString();
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
                result_string.text = "��� : " + result_int.ToString();
            }
            else
            {
                result_int = int.Parse(NomalRollet_10t.text) + int.Parse(NomalRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "��� : " + result_int.ToString();
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
                result_string.text = "��� : " + result_int.ToString();
            }
            else
            {
                result_int = int.Parse(NomalRollet_10t.text) + int.Parse(NomalRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "��� : " + result_int.ToString();
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
                result_string.text = "��� : " + result_int.ToString();
            }
            else
            {
                result_int = int.Parse(NomalRollet_10t.text) + int.Parse(BadRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "��� : " + result_int.ToString();
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
                result_string.text = "��� : " + result_int.ToString();
            }
            else
            {
                result_int = int.Parse(BadRollet_10t.text) + int.Parse(NomalRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "��� : " + result_int.ToString();
            }
        }

        //�� ����
        E_result_int = int.Parse(E_NomalRollet_10t.text) + int.Parse(E_NomalRollet_1t.text);
        if (E_result_int == 0)
        {
            E_result_int = 100;
        }
        E_result_string.text = "��� : " + E_result_int.ToString();

        Invoke("GetStringResult", 1f);
    }
    void GetStringResult()
    {
        if (result_int >= 95)
        {
            result_End.text = "���� : �����";
        }
        else if (result_int < skillPoint / 3f)
        {
            result_End.text = "���� : �뼺��";
        }
        else if (result_int < skillPoint)
        {
            result_End.text = "���� : ����";
        }
        else
        {
            result_End.text = "���� : ����";
        }

        if (E_result_int >= 95)
        {
            E_result_End.text = "���� : �����";
        }
        else if (E_result_int < E_skillPoint / 3f)
        {
            E_result_End.text = "���� : �뼺��";
        }
        else if (E_result_int < E_skillPoint)
        {
            E_result_End.text = "���� : ����";
        }
        else
        {
            E_result_End.text = "���� : ����";
        }

        Invoke("GetFinalResult", 1f);
    }

    public string FinalResult;
    public TextMeshProUGUI FinalResult_t;
    void GetFinalResult()
    {
        if (Subject == "evasion")
        {
            if (result_End.text == "���� : �뼺��")
            {
                FinalResult = "����";
                FinalResult_t.text = "ȸ�� : " + FinalResult;
            }
            else if (result_End.text == "���� : ����" && E_result_End.text == "���� : ����")
            {
                FinalResult = "����";
                FinalResult_t.text = "ȸ�� : " + FinalResult;
            }
            else if (result_End.text == "���� : ����" && E_result_End.text == "���� : ����")
            {
                FinalResult = "����";
                FinalResult_t.text = "ȸ�� : " + FinalResult;
            }
            else if (result_End.text == "���� : ����" && E_result_End.text == "���� : �����")
            {
                FinalResult = "����";
                FinalResult_t.text = "ȸ�� : " + FinalResult;
            }
            else if (result_End.text == "���� : ����" && E_result_End.text == "���� : ����")
            {
                FinalResult = "����";
                FinalResult_t.text = "ȸ�� : " + FinalResult;
            }
            else if (result_End.text == "���� : ����" && E_result_End.text == "���� : �����")
            {
                FinalResult = "����";
                FinalResult_t.text = "ȸ�� : " + FinalResult;
            }
            else if (result_End.text == "���� : �����" && E_result_End.text == "���� : �����")
            {
                FinalResult = "����";
                FinalResult_t.text = "ȸ�� : " + FinalResult;
            }
            else
            {
                FinalResult = "����";
                FinalResult_t.text = "ȸ�� : " + FinalResult;
            }

            
        } // �������� ����
        if (Subject == "counterattack")
        {
            if (result_End.text == "���� : �뼺��" && E_result_End.text != "���� : �뼺��")
            {
                FinalResult = "����";
                FinalResult_t.text = "�ݰ� : " + FinalResult;
            }
            else if (result_End.text == "���� : ����" && E_result_End.text != "���� : �뼺��" && E_result_End.text != "���� : ����")
            {
                FinalResult = "����";
                FinalResult_t.text = "�ݰ� : " + FinalResult;
            }
            else if (result_End.text == "���� : ����" && E_result_End.text != "���� : �뼺��" && E_result_End.text != "���� : ����" && E_result_End.text != "���� : ����")
            {
                FinalResult = "����";
                FinalResult_t.text = "�ݰ� : " + FinalResult;
            }
            else
            {
                FinalResult = "����";
                FinalResult_t.text = "�ݰ� : " + FinalResult;
            }


        } //�Ҹ����� ����

        // �÷��̾��ൿ
        if (Subject == "MA_attack_Counter")
        {
            if (result_End.text == "���� : �뼺��")
            {
                FinalResult = "����";
                FinalResult_t.text = "������ : " + FinalResult;
            }
            else if (result_End.text == "���� : ����" && E_result_End.text == "���� : ����")
            {
                FinalResult = "����";
                FinalResult_t.text = "������ : " + FinalResult;
            }
            else if (result_End.text == "���� : ����" && E_result_End.text == "���� : ����")
            {
                FinalResult = "����";
                FinalResult_t.text = "������ : " + FinalResult;
            }
            else if (result_End.text == "���� : ����" && E_result_End.text == "���� : �����")
            {
                FinalResult = "����";
                FinalResult_t.text = "������ : " + FinalResult;
            }
            else if (result_End.text == "���� : ����" && E_result_End.text == "���� : ����")
            {
                FinalResult = "����";
                FinalResult_t.text = "������ : " + FinalResult;
            }
            else if (result_End.text == "���� : ����" && E_result_End.text == "���� : �����")
            {
                FinalResult = "����";
                FinalResult_t.text = "������ : " + FinalResult;
            }
            else if (result_End.text == "���� : �����" && E_result_End.text == "���� : �����")
            {
                FinalResult = "����";
                FinalResult_t.text = "������ : " + FinalResult;
            }
            else
            {
                FinalResult = "����";
                FinalResult_t.text = "������ : " + FinalResult;
            }



        }

        EndButton.SetActive(true);
    }

    public void EndRollet()
    {
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
        }
        if (Subject == "counterattack")
        {
            if (EnemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(Subject, FinalResult, EnemySubject);
            }
        }

        //�÷��̾��ൿ
        if (Subject == "MA_attack_Counter")
        {
            if (EnemySubject == "DeepOneHybrid")
            {
                BattleManager.Instance.RetrunRolletResult(Subject, FinalResult, EnemySubject);
            }
        }
        //��Ʈ���� ����
        if (Subject == "stress")
        {
            StressManager.Instance.Result(result_End.text);
        }
        if (Subject == "dialog")
        {
            InteractionController.Instance.RetrunDialogResult(Sub_Dialog, result_End.text);
            // ���⿡ Dialog�Լ� ����   DialogManager.Instance.RetrunDialogResult(Sub_Dialog,result_End.text);
            // DilaogManager������ �ش� �Լ��� interacitionMager�� �����ؼ� ���� ������ ������� ���� ���ڸ� ����ϵ��� ��
        }
    }

    // �߿�!!!!!!!
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
        result_string.text ="��� : --";
        result_End.text = "���� : --";
        E_result_string.text = "��� : --";
        E_result_End.text = "���� : --";
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