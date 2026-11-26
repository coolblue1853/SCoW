using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DamageNumbersPro;
using SCoW.Core.State;
using SCoW.Core.Ui;

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
            Invoke("ResetMHp", 1);
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
    //체력바 관련
    private void ResetMHp()
    {
        setHP = 1;
        fullHP = DataBaseManager.hp;
        nomalizedHP = 1 / fullHP;
        setSan = 1;
        fullSan = DataBaseManager.san;
        nomalizedSan = 1 / fullSan;
    }

    // UX-7: HP·SAN 문자열은 값이 바뀔 때(GameRoot.StateChanged)만 다시 만든다. Update 는 변화량 만료만 본다.
    private readonly VitalsTracker _vitals = new VitalsTracker();

    /// <summary>HUD 값 추적(검증용 ChangeCount·변화량).</summary>
    public VitalsTracker Vitals
    {
        get { return _vitals; }
    }

    /// <summary>HP·SAN 이 바뀌어 표시가 갱신될 때(HP 변화량, SAN 변화량). 비네트 연출이 구독한다.</summary>
    public static event System.Action<int, int> VitalsChanged;

    void OnEnable()
    {
        GameRoot.StateChanged += OnStateChanged;
        RefreshVitals(true);
    }

    void OnDisable()
    {
        GameRoot.StateChanged -= OnStateChanged;
    }

    void OnStateChanged(string key)
    {
        if (key == null)
        {
            RefreshVitals(true);
        }
        else if (key == CounterKeys.nowHP || key == CounterKeys.nowSan || key == CounterKeys.hp || key == CounterKeys.san)
        {
            RefreshVitals(false);
        }
    }

    void Update()
    {
        if (_vitals.HasDelta && _vitals.Tick(Time.unscaledDeltaTime))
        {
            ApplyVitalsText();
        }
    }

    void RefreshVitals(bool silent)
    {
        if (!_vitals.Observe(DataBaseManager.nowHP, DataBaseManager.nowSan, DataBaseManager.hp, DataBaseManager.san, silent))
        {
            return;
        }
        ApplyVitalsText();
        System.Action<int, int> handler = VitalsChanged;
        if (handler != null)
        {
            handler(_vitals.HpDelta, _vitals.SanDelta);
        }
    }

    void ApplyVitalsText()
    {
        if (hpText != null)
        {
            hpText.text = _vitals.HpText;
        }
        if (sanText != null)
        {
            sanText.text = _vitals.SanText;
        }
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
            Persist.Keep(this.gameObject);
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
