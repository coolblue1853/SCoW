using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DamageNumbersPro;
public class BillowUIManager : MonoBehaviour
{
    public DamageNumber numberPrefab;
    public GameObject BattlePlayer;
    public GameObject Player;

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

    // 정보 관련.

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



public void resetHP()
    {
        healthBar.resetHP();

    }


    //체력바 관련
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
        CameraManager.Instance.ShakeCam();
        if (DataBaseManager.Masochism == true)
        {
            int updamage = damage + 5;
            DamageNumber damageNumber = numberPrefab.Spawn(Player.transform.position, updamage);
            DamageNumber damageNumber2 = numberPrefab.Spawn(Player.transform.position, 5);
            DataBaseManager.nowHP -= updamage;
            setHP = (setHP - nomalizedHP * updamage);
            healthBar.healthSystem.Damage(updamage);
        }
        else
        {
            DamageNumber damageNumber = numberPrefab.Spawn(Player.transform.position, damage);
            DataBaseManager.nowHP -= damage;
            setHP = (setHP - nomalizedHP * damage);
            healthBar.healthSystem.Damage(damage);
        }

    }
    public void HP_Battledown(int damage)
    {
        if (DataBaseManager.Masochism == true)
        {
            int updamage = damage + 5;
            DamageNumber damageNumber = numberPrefab.Spawn(BattlePlayer.transform.position, updamage);
            DamageNumber damageNumber2 = numberPrefab.Spawn(BattlePlayer.transform.position, 5);
            DataBaseManager.nowHP -= updamage;
            setHP = (setHP - nomalizedHP * updamage);
            healthBar.healthSystem.Damage(updamage);
        }
        else
        {
            DamageNumber damageNumber = numberPrefab.Spawn(BattlePlayer.transform.position, damage);
            DataBaseManager.nowHP -= damage;
            setHP = (setHP - nomalizedHP * damage);
            healthBar.healthSystem.Damage(damage);
        }

    }
    public void San_Down(int damage)
    {
        CameraManager.Instance.ShakeCam();
        if (DataBaseManager.MentalWeakness == true)
        {
            damage += 5;
            DataBaseManager.nowSan -= damage;
            setSan = setSan - nomalizedSan * damage;
            mentalBar.healthSystem.Damage(damage);
        }
        else
        {
            DataBaseManager.nowSan -= damage;
            setSan = setSan - nomalizedSan * damage;
            mentalBar.healthSystem.Damage(damage);
        }

    }

    public void HP_up(int healed)
    {
        if (healed > DataBaseManager.hp - DataBaseManager.nowHP)
        {
            healed = DataBaseManager.hp - DataBaseManager.nowHP;
            DataBaseManager.nowHP += healed;
            setHP = (setHP - nomalizedHP * healed);
            healthBar.healthSystem.Heal(healed);
        }
        else
        {
            DataBaseManager.nowHP += healed;
            setHP = (setHP - nomalizedHP * healed);
            healthBar.healthSystem.Heal(healed);
        }

    }
    public void San_up(int damage)
    {

        if(damage > DataBaseManager.san - DataBaseManager.nowSan)
        {
            damage = DataBaseManager.san - DataBaseManager.nowSan;
            DataBaseManager.nowSan += damage;
            setSan = setSan - nomalizedSan * damage;
            mentalBar.healthSystem.Heal(damage);
        }
        else
        {
            DataBaseManager.nowSan += damage;
            setSan = setSan - nomalizedSan * damage;
            mentalBar.healthSystem.Heal(damage);
        }

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
