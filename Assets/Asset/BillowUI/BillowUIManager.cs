using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BillowUIManager : MonoBehaviour
{    
    
    public float setHP;
    public float  FullHP;
    public float  nomalizedHP;

    public Image HPBar;
    public Image FadeBar;
    public Image SanBar;

    public HealthBarShrink healthBar;
    public HealthBarShrink mentalBar;


    public float setSan;
    public float FullSan;
    public float nomalizedSan;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI SanText;

    public TextMeshProUGUI intelNameText;
    public TextMeshProUGUI intelDetailText;



    // Start is called before the first frame update
    void Start()
    {
        if(DataBaseManager.hp ==0)
        {
            Invoke("ResetMHP", 1);
        }
        else
        {
            ResetMHP();
        }

    }

    // ���� ����.

    public void SetIntelUi(string name, string detail)
    {
        intelNameText.text = name;
        intelDetailText.text = detail;
        
    }
    public void ResetIntelUi()
    {
        intelNameText.text = "";
        intelDetailText.text = "";

    }







    //ü�¹� ����
    private void ResetMHP()
    {
        setHP = 1;
        FullHP = DataBaseManager.hp;
        nomalizedHP = 1 / FullHP;


        setSan = 1;
        FullSan = DataBaseManager.san;
        nomalizedSan = 1 / FullSan;
    }

    // Update is called once per frame
    void Update()
    {

        HPIntMagaer();


    }



    void HPIntMagaer()
    {
        hpText.text = "HP : " + DataBaseManager.nowHP;
        SanText.text = "SAN : " + DataBaseManager.nowSan;
    }

    private static BillowUIManager instance = null;

    public void HP_down(int damage)
    {
        DataBaseManager.nowHP -= damage;
        setHP = (setHP - nomalizedHP * damage);
        healthBar.healthSystem.Damage(damage);
    }
    public void San_Down(int damage)
    {
        DataBaseManager.nowSan -= damage;
        setSan = setSan - nomalizedSan * damage;
        mentalBar.healthSystem.Damage(damage);
    }

    public void HP_up(int healed)
    {
        DataBaseManager.nowHP += healed;
        setHP = (setHP - nomalizedHP * healed);
        healthBar.healthSystem.Heal(healed);
    }
    public void San_up(int damage)
    {
        DataBaseManager.nowSan += damage;
        setSan = setSan - nomalizedSan * damage;
        mentalBar.healthSystem.Damage(damage);
    }
    void Awake()
    {
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
    public static BillowUIManager Instance
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

}
