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
    public Image MPBar;

    public HealthBarShrink healthBar;
    public HealthBarShrink mentalBar;


    public float setMP;
    public float FullMP;
    public float nomalizedMP;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;



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

    private void ResetMHP()
    {
        setHP = 1;
        FullHP = DataBaseManager.hp;
        nomalizedHP = 1 / FullHP;


        setMP = 1;
        FullMP = DataBaseManager.mp;
        nomalizedMP = 1 / FullMP;
    }

    // Update is called once per frame
    void Update()
    {
        HPIntMagaer();
    }



    void HPIntMagaer()
    {
        hpText.text = "ü�� : " + DataBaseManager.nowHP;
        mpText.text = "���ŷ� : " + DataBaseManager.nowMP;
    }

    private static BillowUIManager instance = null;

    public void HP_down(int damage)
    {
        DataBaseManager.nowHP -= damage;
        setHP = (setHP - nomalizedHP * damage);
        healthBar.healthSystem.Damage(5);
    }
    public void MP_Down(int damage)
    {
        DataBaseManager.nowMP -= damage;
        setMP = setMP - nomalizedMP * damage;
        mentalBar.healthSystem.Damage(5);
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
