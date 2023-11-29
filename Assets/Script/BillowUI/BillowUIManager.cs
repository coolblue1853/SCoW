using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DamageNumbersPro;

using UnityEngine.Serialization;
public class BillowUIManager : MonoBehaviour
{
    public DamageNumber numberPrefab;
    public GameObject battlePlayer;
    public GameObject player;

    public float setHP;
    public float fullHP;
    public float nomalizedHP;
    public Image hpBar;
    public Image fadeBar;
    public Image sanBar;

    public HealthBarShrink healthBar;
    public HealthBarShrink mentalBar;

    public float setSan;
    public float fullSan;
    public float nomalizedSan;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI sanText;

    public TextMeshProUGUI intelNameText;
    public TextMeshProUGUI intelDetailText;
    void Start()
    {
        if (DataBaseManager.hp == 0)
        {
            Invoke("ResetMHP", 1);
        }
        else
        {
            ResetMHp();
        }
    }
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
    public void ResetHp()
    {
        healthBar.ResetHp();
    }
    //ü�¹� ����
    private void ResetMHp()
    {
        setHP = 1;
        fullHP = DataBaseManager.hp;
        nomalizedHP = 1 / fullHP;
        setSan = 1;
        fullSan = DataBaseManager.san;
        nomalizedSan = 1 / fullSan;
    }

    // Update is called once per frame
    void Update()
    {
        HpIntMagaer();
    }
    void HpIntMagaer()
    {
        hpText.text = "HP : " + DataBaseManager.nowHP;
        sanText.text = "SAN : " + DataBaseManager.nowSan;
    }
    private static BillowUIManager instance = null;

    public void HpDown(int damage)
    {
        CameraManager.Instance.ShakeCam();
        if (DataBaseManager.masochism == true)
        {
            int updamage = damage + 5;
            DamageNumber damageNumber = numberPrefab.Spawn(player.transform.position, updamage);
            DamageNumber damageNumber2 = numberPrefab.Spawn(player.transform.position, 5);
            DataBaseManager.nowHP -= updamage;
            setHP = (setHP - nomalizedHP * updamage);
            healthBar.healthSystem.Damage(updamage);
        }
        else
        {
            DamageNumber damageNumber = numberPrefab.Spawn(player.transform.position, damage);
            DataBaseManager.nowHP -= damage;
            setHP = (setHP - nomalizedHP * damage);
            healthBar.healthSystem.Damage(damage);
        }
    }
    public void HpBattleDown(int damage)
    {
        if (DataBaseManager.masochism == true)
        {
            int updamage = damage + 5;
            DamageNumber damageNumber = numberPrefab.Spawn(battlePlayer.transform.position, updamage);
            DamageNumber damageNumber2 = numberPrefab.Spawn(battlePlayer.transform.position, 5);
            DataBaseManager.nowHP -= updamage;
            setHP = (setHP - nomalizedHP * updamage);
            healthBar.healthSystem.Damage(updamage);
        }
        else
        {
            DamageNumber damageNumber = numberPrefab.Spawn(battlePlayer.transform.position, damage);
            DataBaseManager.nowHP -= damage;
            setHP = (setHP - nomalizedHP * damage);
            healthBar.healthSystem.Damage(damage);
        }
    }
    public void SanDown(int damage)
    {
        CameraManager.Instance.ShakeCam();
        if (DataBaseManager.mentalWeakness == true)
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
    public void HpUp(int healed)
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
    public void SanUp(int damage)
    {
        if (damage > DataBaseManager.san - DataBaseManager.nowSan)
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
