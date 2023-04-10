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
        hpText.text = "체력 : " + DataBaseManager.nowHP;
        mpText.text = "정신력 : " + DataBaseManager.nowMP;
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

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
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
