using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BattleManager : MonoBehaviour
{
    private static BattleManager instance = null;

    public string BattleState;
    private float fDestroyTime = 0.01f;
    private float fTickTime;

    public TextMeshProUGUI RoundText;
    public GameObject RoundGameObject;

    public GameObject PlayerTrunSymbol;
    public GameObject EnemyTrunSymbol_1;
    public GameObject EnemyTrunSymbol_2;

    public GameObject PlayerChoiceUi;
    public GameObject PlayerActionUi;
    EnemyClass.DeepOneHybrid DeepOneHybrid = new EnemyClass.DeepOneHybrid();
    public GameObject DeepOneHybrid_Object;


    public string PlayerAction = "";
    public string SelectEnemy = "";
    public string onPointerEnemy = "";

    void Update()
    {
        EnemyHealthCheck();
        TimeWait();
        StateChecker();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fDestroyTime = 0.01f;
            RoundGameObject.SetActive(false);
            BattleState = "setTrun";
        }


    }
    public static BattleManager Instance
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
    void Awake()
    {
        set_DeepOneHybrid();
        if (null == instance)
        {
            instance = this;
         //   DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void set_DeepOneHybrid()
    {
        DeepOneHybrid.SetStatDeepOneHybrid();
    }


    public void TimeWait()
    {
        if (BattleState == "setTrun")
        {
            fTickTime += Time.deltaTime;

            if (fTickTime >= fDestroyTime)
            {
                fTickTime = 0;
                fDestroyTime = fDestroyTime * 0.98f;
                if (PlayerTrunSymbol.activeSelf == true)
                {
                    SymbolUp(PlayerTrunSymbol, 90, "Player");
                }
                if (EnemyTrunSymbol_1.activeSelf == true)
                {
                    SymbolUp(EnemyTrunSymbol_1, 60, "DeepOneHybrid1");
                }
                if (EnemyTrunSymbol_2.activeSelf == true)
                {
                    SymbolUp(EnemyTrunSymbol_2, 30, "Enemy2");
                }

            }
        }
    }

    // �� �ּ����� -130, �ִ����� +130
    // Start is called before the first frame update
    void Start()
    {
       BattleState = "setTrun";

    }


    public void SymbolUp(GameObject symbol , float dex, string Target)
    {
        if(BattleState == "setTrun")
        {
            if (symbol.transform.localPosition.y < 130)
            {
                if(Target == "Player")
                {
                    symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, symbol.transform.localPosition.y + (1 + (dex / 100) * 1.5f));
                }
                else
                {
                    symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, symbol.transform.localPosition.y + (1 + (dex / 100) * 1.1f));
                }
            }
            else
            {
                symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, -130);
                BattleState = Target+"Trun";

                RoundText.text = Target + "�� ����";
                RoundGameObject.SetActive(true);
            }
        }
    }


    public void StateChecker()
    {
        if(BattleState == "DeepOneHybrid1Trun")
        {
            BattleState = "DeepOneHybrid1Attack";
            Invoke("Enemy_setAction", 1);
        }
        if (BattleState == "PlayerTrun")
        {
            BattleState = "PlayerChoice";
            Invoke("Player_setAction", 1);
        }
    }
    void Player_setAction()
    {
        RoundGameObject.SetActive(false);
       // OpenRoundObject("�ൿ�������ϼ���");
        PlayerActionUi.SetActive(true);
    }






    void Enemy_setAction()
    {
        // ���� �̰��� ���� �ൿ ����� ���� ������� �۵��ϴ��� ��־� �ҵ�
        RoundGameObject.SetActive(false);
        OpenRoundObject("���� ����� ���������� �����Ϸ� �մϴ�.");
        PlayerChoiceUi.SetActive(true);
    }


    public void PlayerChoiceButton_Evasion()
    {
        if (BattleState == "DeepOneHybrid1Attack")
        {
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("�ĺ�� : ȸ��", "ȸ��", 50, "evasion","DeepOneHybrid" , "??? : ������", "������", DeepOneHybrid.DeepOneHybrid_MatialArts);
        }
    }
    public void PlayerChoiceButton_CounterAttack()
    {
        if (BattleState == "DeepOneHybrid1Attack")
        {
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("�ĺ�� : �ݰ�", "������", 50, "counterattack", "DeepOneHybrid", "??? : ������", "������", DeepOneHybrid.DeepOneHybrid_MatialArts);
        }
    }

    public void RetrunRolletResult(string PlayerAction, string Success, string Enemy)
    {

        onPointerEnemy = "";
        if (PlayerAction == "evasion")
        {
            if(Enemy == "DeepOneHybrid")
            {
                if (Success == "����")
                {
                    TurnEnd();
                }
                else
                {
                    BillowUIManager.Instance.HP_down(10);
                    TurnEnd();
                }
            }

        }
        if (PlayerAction == "counterattack")
        {
            if (Enemy == "DeepOneHybrid")
            {
                if (Success == "����")
                {
                    DeepOneHybrid.NowHP -= 10;
                    Debug.Log(DeepOneHybrid.NowHP);
                    TurnEnd();
                }
                else
                {
                    BillowUIManager.Instance.HP_down(10);
                    TurnEnd();
                }
            }

        }

        if (PlayerAction == "MA_attack_Counter")
        {
            if (Enemy == "DeepOneHybrid")
            {
                if (Success == "����")
                {
                    DeepOneHybrid.NowHP -= 10;
                    Debug.Log(DeepOneHybrid.NowHP);
                    TurnEnd();
                }
                else
                {
                    BillowUIManager.Instance.HP_down(10);
                    TurnEnd();
                }
            }

        }
    }



    void EnemyHealthCheck()
    {
        if(DeepOneHybrid.NowHP <= 0)
        {
            DeepOneHybrid_Object.SetActive(false);
        }
    }


    public void CloseRoundObject()
    {
        RoundText.text = "";
        RoundGameObject.SetActive(false);
    }
    public void OpenRoundObject(string text)
    {
        RoundText.text = text;
        RoundGameObject.SetActive(true);
    }
    public void TurnEnd()
    {
        PlayerAction = "";
        SelectEnemy = "";
        BattleState = "setTrun";
        fDestroyTime = 0.01f;
    }
    public void SetAciton(string action)
    {
        PlayerActionUi.SetActive(false);
        BattleState = "selectEnemy";
        PlayerAction = action;
    }
    public void SetEnemy(string enemy)
    {
        if(enemy == "DeepOneHybrid")
        {
            if (PlayerAction == "martialarts")
            {
                BattleRollet.Instance.setBattleRollet("�ĺ�� : ������", "������", 50, "MA_attack_Counter", "DeepOneHybrid", "??? : �ݰ�", "�ݰ�", DeepOneHybrid.DeepOneHybrid_MatialArts);
            }
        }

    }
}
