using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Rollet : MonoBehaviour
{
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
    private static Rollet instance = null;

    public GameObject EndButton;


    private void Update()
    {
        if(isClick == false && isActiveRollet == true)
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
    public void setRollet(string skill, string point_sting, int point_int)
    {
        ResetString();
        RolletSetUi.SetActive(true);
        Skill.text = skill;
        Point.text = point_sting + " : " + point_int;
        skillPoint = point_int;
        if (DataBaseManager.Condition == "Nomal")
        {
            ResetRolletDice();
            Conditon.text = "컨디션 : 보통";

            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = SkillPoint_10 * 0.1f + SkillPoint_1 * 0.01f;
        }
        else if (DataBaseManager.Condition == "Good")
        {
            ResetRolletDice();
            Conditon.text = "컨디션 : 좋음";
            Good_1.SetActive(true);
            Good_1_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = SkillPoint_10 * 0.1f + (SkillPoint_1 * SkillPoint_1 + 2* SkillPoint_1*(10- SkillPoint_1)) * 0.001f;
        }
        else if(DataBaseManager.Condition == "Best")
        {
            ResetRolletDice();
            Conditon.text = "컨디션 : 최상";
            Good_10.SetActive(true);
            Good_10_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = (SkillPoint_10* SkillPoint_10 + 2* SkillPoint_10*(10- SkillPoint_10)) * 0.01f + SkillPoint_1*0.001f;
        }
        else if(DataBaseManager.Condition == "Bad")
        {
            ResetRolletDice();
            Conditon.text = "컨디션 : 나쁨";
            Bad_1.SetActive(true);
            Bad_1_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = SkillPoint_10 * 0.1f + SkillPoint_1* SkillPoint_1*0.001f;
        }
        else if(DataBaseManager.Condition == "Worst")
        {
            ResetRolletDice();
            Conditon.text = "컨디션 : 최악";
            Bad_10.SetActive(true);
            Bad_10_Rollet.SetActive(true);
            SkillPoint_1 = point_int % 10;
            SkillPoint_10 = point_int / 10;
            SuccesProbability = (SkillPoint_10 * SkillPoint_10 *0.01f) + SkillPoint_1*0.001f;
        }
        
        if(SuccesProbability < 0.20)
        {
            Expect.text = "성공예상 : 매우 어려움";
        }
        else if(SuccesProbability < 0.40)
        {
            Expect.text = "성공예상 : 어려움";
        }
        else if (SuccesProbability < 0.60)
        {
            Expect.text = "성공예상 : 보통";
        }
        else if (SuccesProbability < 0.80)
        {
            Expect.text = "성공예상 : 쉬움";
        }
        else if (SuccesProbability < 1)
        {
            Expect.text = "성공예상 : 매우 쉬움";
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
        if (DataBaseManager.Condition == "Nomal")
        {
            result_int = int.Parse(NomalRollet_10t.text) + int.Parse(NomalRollet_1t.text);
            if(result_int == 0)
            {
                result_int = 100;
            }
            result_string.text = "결과 : "+result_int.ToString();
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
                result_string.text = "결과 : " + result_int.ToString();
            }
            else
            {
                result_int = int.Parse(NomalRollet_10t.text) + int.Parse(NomalRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "결과 : " + result_int.ToString();
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
                result_string.text = "결과 : " + result_int.ToString();
            }
            else
            {
                result_int = int.Parse(NomalRollet_10t.text) + int.Parse(NomalRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "결과 : " + result_int.ToString();
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
                result_string.text = "결과 : " + result_int.ToString();
            }
            else
            {
                result_int = int.Parse(NomalRollet_10t.text) + int.Parse(BadRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "결과 : " + result_int.ToString();
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
                result_string.text = "결과 : " + result_int.ToString();
            }
            else
            {
                result_int = int.Parse(BadRollet_10t.text) + int.Parse(NomalRollet_1t.text);
                if (result_int == 0)
                {
                    result_int = 100;
                }
                result_string.text = "결과 : " + result_int.ToString();
            }
        }

        Invoke("GetStringResult", 1f);
    }
    void GetStringResult()
    {
        if (result_int >= 95)
        {
            result_End.text = "판정 : 대실패";
        }
        else if(result_int < skillPoint / 3f)
        {
            result_End.text = "판정 : 대성공";
        }
        else if (result_int < skillPoint)
        {
            result_End.text = "판정 : 성공";
        }
        else
        {
            result_End.text = "판정 : 실패";
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
        result_string.text ="결과 : --";
        result_End.text = "판정 : --";
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