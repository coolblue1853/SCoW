using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using DamageNumbersPro;
public class BattleManager : MonoBehaviour
{
    public GameObject UIButton;

    public BattleResetUI battleResetUI;
    public DamageNumber numberPrefab;

    public bool StartBattle = false;
    public GameObject Cam;

    private static BattleManager instance = null;

    public string BattleState;
    private float fDestroyTime = 0.01f;
    private float fTickTime;

    public TextMeshProUGUI RoundText;
    public GameObject RoundGameObject;

    //플레이어 관련
    public GameObject player;
    Vector3 OriginPoint;
    public SpriteRenderer player_R;
    public Sprite Stand;
    public Sprite Punch;
    public Sprite Hitted;
    public Sprite Dagger_Ready;
    public Sprite Dagger_Attack;
    public Sprite Bat_Ready;
    public Sprite Bat_Attack;
    public Sprite Axe_Ready;
    public Sprite Axe_Attack;

    public Sprite SmallPistol_Ready;
    public Sprite SmallPistol_Attack;
    public Sprite Revolver_Ready;
    public Sprite Revolver_Attack;
    public Sprite Rifle_Ready;
    public Sprite Rifle_Attack;
    public Sprite Shotgun_Ready;
    public Sprite Shotgun_Attack;

    public Sprite Rock_Ready;
    public Sprite Molotov_Ready;
    public Sprite Deftness_Attack;


    public Sprite SmallPistol_Reload;
    public Sprite Revolver_Reload;
    public Sprite Rifle_Reload;
    public Sprite Shotgun_Reload;

    public GameObject PlayerTrunSymbol;
    public GameObject EnemyTrunSymbol_1;
    public GameObject EnemyTrunSymbol_2;
    public GameObject EnemyTrunSymbol_3;

    public GameObject PlayerChoiceUi;
    public GameObject PlayerActionUi;
    public GameObject PlayerAttackUi;
    public GameObject PlayerSwordsUi;
    public GameObject PlayerMarkmenUi;
    public GameObject PlayerReloadUi;
    public GameObject PlayerReloadItemUI;
    public GameObject PlayerReloadCheckUi;
    public GameObject PlayerDeftnessUi;
    public GameObject EnemySelectUI;
    EnemyClass.DeepOneHybrid DeepOneHybrid1 = new EnemyClass.DeepOneHybrid();
    EnemyClass.DeepOneHybrid DeepOneHybrid2 = new EnemyClass.DeepOneHybrid();
    EnemyClass.DeepOneHybrid DeepOneHybrid3 = new EnemyClass.DeepOneHybrid();
    EnemyClass.DeepOneHybrid DeepOneHybrid4 = new EnemyClass.DeepOneHybrid();
    //적 오브젝트 관련
    public GameObject DeepOneHybrid1_Object;
    public SpriteRenderer DeepOneHybrid1_Render;
    public GameObject DeepOneHybrid2_Object;
    public SpriteRenderer DeepOneHybrid2_Render;
    public GameObject DeepOneHybrid3_Object;
    public SpriteRenderer DeepOneHybrid3_Render;
    public GameObject DeepOneHybrid4_Object;
    public SpriteRenderer DeepOneHybrid4_Render;


    public Sprite DeepOneHybrid_Stand;
    public Sprite DeepOneHybrid_HittedByPunch;
    public Sprite DeepOneHybrid_HittedByNife;
    public Sprite DeepOneHybrid_HittedByBat;
    public Sprite DeepOneHybrid_HittedByAxe;
    public Sprite DeepOneHybrid_HittedBySmallPistol;
    public Sprite DeepOneHybrid_HittedByRevolver;
    public Sprite DeepOneHybrid_HittedByRifle;
    public Sprite DeepOneHybrid_HittedByShotgun;
    public Sprite DeepOneHybrid_HittedByRock;
    public Sprite DeepOneHybrid_HittedByMolotov;
    public Sprite DeepOneHybrid_Punch;



    public string PlayerAction = "";
    public string SelectEnemy = "";
    public string onPointerEnemy = "";



    void Update()
    {

        if(StartBattle == true &&(EnemyTrunSymbol_1.activeSelf == true || EnemyTrunSymbol_2.activeSelf == true || EnemyTrunSymbol_3.activeSelf == true))
        {
            EnemyHealthCheck();
            TimeWait();
            StateChecker();

        }

        if(DataBaseManager.nowPlace == "BattleRoad")
        {
            if (EnemyTrunSymbol_1.activeSelf == false && EnemyTrunSymbol_2.activeSelf == false && EnemyTrunSymbol_3.activeSelf == false && DataBaseManager.RoadBattleEnd == false)
            {

                DataBaseManager.isDirecting = true;

                DataBaseManager.RoadBattleEnd = true;
                FadingBackGround.Instance.FadeIn();
                Invoke("EndRoadBattle", 1f);


            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.K)){
            FadingBackGround.Instance.FadeIn();
            Invoke("EndRoadBattle", 1f);
        }
                  */
    }

    public void CloseButtonUI()
    {
        UIButton.SetActive(false);
    }
    void EndRoadBattle()
    {
        UIButton.SetActive(true);
        InteractionController.Instance.BattleDialog("End");
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

    public void Ex_BattleEnd()
    {
        DataBaseManager.RoadBattleEnd = true;
        FadingBackGround.Instance.FadeIn();
        Invoke("EndRoadBattle", 1f);
    }
    void Awake()
    {

        player_R = player.GetComponent<SpriteRenderer>();
        DeepOneHybrid1_Render = DeepOneHybrid1_Object.GetComponent<SpriteRenderer>();
        DeepOneHybrid2_Render = DeepOneHybrid2_Object.GetComponent<SpriteRenderer>();
        DeepOneHybrid3_Render = DeepOneHybrid3_Object.GetComponent<SpriteRenderer>();
        DeepOneHybrid4_Render = DeepOneHybrid4_Object.GetComponent<SpriteRenderer>();
        OriginPoint = player.transform.position;
        set_DeepOneHybrid1();
        set_DeepOneHybrid2();
        set_DeepOneHybrid3();
        set_DeepOneHybrid4();
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


    public void set_DeepOneHybrid1()
    {
        DeepOneHybrid1.SetStatDeepOneHybrid();
    }
    public void set_DeepOneHybrid2()
    {
        DeepOneHybrid2.SetStatDeepOneHybrid();
    }
    public void set_DeepOneHybrid3()
    {
        DeepOneHybrid3.SetStatDeepOneHybrid();
    }
    public void set_DeepOneHybrid4()
    {
        DeepOneHybrid4.SetStatDeepOneHybrid();
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
                    SymbolUp(PlayerTrunSymbol, DataBaseManager.dex  , "Player");
                }
                if(DataBaseManager.nowPlace != "InSewer")
                {
                    if (EnemyTrunSymbol_1.activeSelf == true)
                    {
                        SymbolUp(EnemyTrunSymbol_1, DeepOneHybrid1.dex, "DeepOneHybrid1");
                    }
                    if (EnemyTrunSymbol_2.activeSelf == true)
                    {
                        SymbolUp(EnemyTrunSymbol_2, DeepOneHybrid2.dex, "DeepOneHybrid2");
                    }
                    if (EnemyTrunSymbol_3.activeSelf == true)
                    {
                        SymbolUp(EnemyTrunSymbol_3, DeepOneHybrid3.dex, "DeepOneHybrid3");
                    }
                }
                else
                {
                    if (EnemyTrunSymbol_1.activeSelf == true)
                    {
                        SymbolUp(EnemyTrunSymbol_1, DeepOneHybrid4.dex, "DeepOneHybrid4");
                    }
                    if (EnemyTrunSymbol_2.activeSelf == true)
                    {
                      //  SymbolUp(EnemyTrunSymbol_2, DeepOneHybrid2.dex, "DeepOneHybrid2");
                    }
                    if (EnemyTrunSymbol_3.activeSelf == true)
                    {
                       // SymbolUp(EnemyTrunSymbol_3, DeepOneHybrid3.dex, "DeepOneHybrid3");
                    }
                }
   

            }
        }
    }

    // 턴 최소지점 -130, 최대지점 +130
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
                    symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, symbol.transform.localPosition.y + ((1 + (dex / 100)) * 2f));
                }
                else
                {
                    symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, symbol.transform.localPosition.y + ((1 + (dex / 100) )* 1.9f));
                }
            }
            else
            {
                SoundManager.Instance.SetTrunSound_Play();
                symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, -130);
                BattleState = Target+"Trun";

                RoundText.text = Target + " Trun";
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
        if (BattleState == "DeepOneHybrid2Trun")
        {
            BattleState = "DeepOneHybrid2Attack";
            Invoke("Enemy_setAction", 1);
        }
        if (BattleState == "DeepOneHybrid3Trun")
        {
            BattleState = "DeepOneHybrid3Attack";
            Invoke("Enemy_setAction", 1);
        }
        if (BattleState == "DeepOneHybrid4Trun")
        {
            BattleState = "DeepOneHybrid4Attack";
            Invoke("Enemy_setAction", 1);
        }
        if (BattleState == "PlayerTrun")
        {
            BattleItemManager.Instance.ManageItem();
            BattleState = "PlayerChoice";
            Invoke("Player_setAction", 1);
        }
    }
    void Player_setAction()
    {
        //SoundManager.Instance.ClickSound_Play();
        RoundGameObject.SetActive(false);
        PlayerActionUi.SetActive(true);
    }

    public void Player_BackAction()
    {
        SoundManager.Instance.ClickSound_Play();
        EnemySelectUI.SetActive(false);
        PlayerAttackUi.SetActive(false);
        BattleState = "PlayerChoice";
        PlayerActionUi.SetActive(true);

    }

    public void Player_setAttack()
    {
        SoundManager.Instance.ClickSound_Play();
        PlayerActionUi.SetActive(false);
        BattleState = "PlayerAttack";
        PlayerAttackUi.SetActive(true);
    }


    public void Player_setSwords()
    {
        SoundManager.Instance.ClickSound_Play();
        PlayerAttackUi.SetActive(false);
        PlayerAction = "PlayerSwords";
        PlayerSwordsUi.SetActive(true);
    }

    public void Player_BackSwords()
    {
        SoundManager.Instance.ClickSound_Play();
        EnemySelectUI.SetActive(false);
        PlayerSwordsUi.SetActive(false);
        PlayerAction = "PlayerAttack";
        PlayerAttackUi.SetActive(true);
        player_R.sprite = Stand;
    }

    public void Player_setMarkmens()
    {
        SoundManager.Instance.ClickSound_Play();
        PlayerAttackUi.SetActive(false);
        PlayerAction = "PlayerMarkmen";
        PlayerMarkmenUi.SetActive(true);
    }
    public void Player_BackMarkmens()
    {
        SoundManager.Instance.ClickSound_Play();
        EnemySelectUI.SetActive(false);
        PlayerAttackUi.SetActive(true);
        PlayerAction = "PlayerAttack";
        PlayerMarkmenUi.SetActive(false);
        player_R.sprite = Stand;
    }

    public void Player_setDeftness()
    {
        SoundManager.Instance.ClickSound_Play();
        PlayerAttackUi.SetActive(false);
        PlayerAction = "PlayerDeftness";
        PlayerDeftnessUi.SetActive(true);
    }
    public void Player_BackDeftness()
    {
        SoundManager.Instance.ClickSound_Play();
        EnemySelectUI.SetActive(false);
        PlayerAttackUi.SetActive(true);
        PlayerAction = "PlayerAttack";
        PlayerDeftnessUi.SetActive(false);
        player_R.sprite = Stand;
    }


    public void Player_setReload()
    {
        SoundManager.Instance.ClickSound_Play();
        PlayerActionUi.SetActive(false);
        PlayerAction = "PlayerReload";
        PlayerReloadUi.SetActive(true);
        PlayerReloadItemUI.SetActive(true);
    }
    public void Player_BackReload()
    {
        SoundManager.Instance.ClickSound_Play();
        PlayerActionUi.SetActive(true);
        PlayerAction = "PlayerChoice";
        PlayerReloadUi.SetActive(false);
        player_R.sprite = Stand;
    }
    
    public string nowReloadWeapon;

    public void OpenReloadUI(string subject)
    {
        nowReloadWeapon = subject;
        PlayerReloadItemUI.SetActive(false);
        PlayerReloadCheckUi.SetActive(true);
    }

    public void startReload()
    {

        while (DataBaseManager.PistolAmmo > 0)
        {
            if (DataBaseManager.nowSmallPistol == 2)
            {
                break;
            }
            Debug.Log("22");
            DataBaseManager.nowSmallPistol += 1;
            DataBaseManager.PistolAmmo -= 1;
        }
        while (DataBaseManager.PistolAmmo > 0)
        {
            if (DataBaseManager.nowRevolver == 6)
            {
                break;
            }
            Debug.Log("3");
            DataBaseManager.nowRevolver += 1;
            DataBaseManager.PistolAmmo -= 1;
        }
        while (DataBaseManager.RifleAmmo > 0)
        {
            if (DataBaseManager.nowRifle == 5)
            {
                break;
            }
            Debug.Log("4");
            DataBaseManager.nowRifle += 1;
            DataBaseManager.RifleAmmo -= 1;
        }
        while (DataBaseManager.ShotgunAmmo > 0)
        {
            if (DataBaseManager.nowShotgun == 2)
            {
                Debug.Log("5");
                break;
            }
            DataBaseManager.nowShotgun += 1;
            DataBaseManager.ShotgunAmmo -= 1;
        }
    }
    public void ReloadOk()
    {
        PlayerReloadCheckUi.SetActive(false);
        BattleCameraMove.Instance.ZoomMe();
        if (nowReloadWeapon == "SmallPistol")
        {
            SoundManager.Instance.Battle_Sound("SmallPisol_Reload");
            while (DataBaseManager.PistolAmmo > 0)
            {
                if(DataBaseManager.nowSmallPistol == 2)
                {
                    break;
                }
                DataBaseManager.nowSmallPistol += 1;
                DataBaseManager.PistolAmmo -= 1;
            }
            Sequence sequence = DOTween.Sequence()
            .AppendCallback(() => OnSpriteChangeComplete(player_R, SmallPistol_Reload))
            .AppendInterval(2f) // 2초 대기
            .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
            .AppendCallback(() => BattleCameraMove.Instance.ResetCam())
            .AppendCallback(() => TurnEnd());
        }
        if (nowReloadWeapon == "Revolver")
        {
            SoundManager.Instance.Battle_Sound("Revolver_Reload");
            while (DataBaseManager.PistolAmmo > 0)
            {
                if (DataBaseManager.nowRevolver == 6)
                {
                    break;
                }
                DataBaseManager.nowRevolver += 1;
                DataBaseManager.PistolAmmo -= 1;
            }
            Sequence sequence = DOTween.Sequence()
            .AppendCallback(() => OnSpriteChangeComplete(player_R, Revolver_Reload))
            .AppendInterval(2f) // 2초 대기
            .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
            .AppendCallback(() => BattleCameraMove.Instance.ResetCam())
            .AppendCallback(() => TurnEnd());
        }
        if (nowReloadWeapon == "Rifle")
        {
            SoundManager.Instance.Battle_Sound("Rifle_Reload");
            while (DataBaseManager.RifleAmmo > 0)
            {
                if (DataBaseManager.nowRifle == 5)
                {
                    break;
                }
                DataBaseManager.nowRifle += 1;
                DataBaseManager.RifleAmmo -= 1;
            }
            Sequence sequence = DOTween.Sequence()
            .AppendCallback(() => OnSpriteChangeComplete(player_R, Rifle_Reload))
            .AppendInterval(2f) // 2초 대기
            .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
            .AppendCallback(() => BattleCameraMove.Instance.ResetCam())
            .AppendCallback(() => TurnEnd());
        }
        if (nowReloadWeapon == "Shotgun")
        {
            SoundManager.Instance.Battle_Sound("Shotgun_Reload");
            while (DataBaseManager.ShotgunAmmo > 0)
            {
                if (DataBaseManager.nowShotgun == 2)
                {
                    break;
                }
                DataBaseManager.nowShotgun += 1;
                DataBaseManager.ShotgunAmmo -= 1;
            }
            Sequence sequence = DOTween.Sequence()
            .AppendCallback(() => OnSpriteChangeComplete(player_R, Shotgun_Reload))
            .AppendInterval(2f) // 2초 대기
            .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
            .AppendCallback(() => BattleCameraMove.Instance.ResetCam())
            .AppendCallback(() => TurnEnd());
        }
    }
    public void ReloadClose()
    {
        PlayerReloadItemUI.SetActive(true);
        PlayerReloadCheckUi.SetActive(false);
        nowReloadWeapon = "";
    }
    int EnemyCount = 0;
    public GameObject RunAwayObject;
    public TextMeshProUGUI RunAwayText;
    public void RunAway()
    {
        SoundManager.Instance.ClickSound_Play();
        EnemyCount = 0;
        RunAwayObject.SetActive(true);
        if (EnemyTrunSymbol_1.activeSelf == true)
        {
            EnemyCount += 1;
        }
        if(EnemyTrunSymbol_2.activeSelf == true) 
        {
            EnemyCount += 1;
        }
        if(EnemyTrunSymbol_3.activeSelf == true)
        {
            EnemyCount += 1;
        }

        RunAwayText.text = "Attempt to Runaway. You are penalized by the number of enemies.\n\nDex(" + DataBaseManager.dex+") / "+EnemyCount+" = "+DataBaseManager.dex/EnemyCount;
    }

    public void RunYes()
    {
        SoundManager.Instance.ClickSound_Play();
        BattleRollet.Instance.setBattleRollet("Fabian : Runaway", "DEX / "+ EnemyCount, DataBaseManager.dex / EnemyCount, "RunAway", "DeepOneHybrid", "??? : Hold", "Dex", DeepOneHybrid1.dex);
        RunAwayObject.SetActive(false);
    }
    public void RunNo()
    {
        SoundManager.Instance.ClickSound_Play();
        RunAwayObject.SetActive(false);
    }


    void Enemy_setAction()
    {
        // 추후 이곳에 적의 행동 요령을 통해 어떤식으로 작동하는지 써둬야 할듯
        RoundGameObject.SetActive(false);
        OpenRoundObject("An enemy tries to attack you with a MartialArt.");
        PlayerChoiceUi.SetActive(true);
    }


    public void PlayerChoiceButton_Evasion()
    {
        PlayerAction = "evasion";
        if (BattleState == "DeepOneHybrid1Attack")
        {
           // BattleState = "Rollet";
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : Evasion", "Evasion", DataBaseManager.evasionPoint, "evasion","DeepOneHybrid" , "??? : MartialArt", "MartialArt", DeepOneHybrid1.DeepOneHybrid_MatialArts);
        }
        if (BattleState == "DeepOneHybrid2Attack")
        {
           // BattleState = "Rollet";
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : Evasion", "Evasion", DataBaseManager.evasionPoint, "evasion", "DeepOneHybrid", "??? : MartialArt", "MartialArt", DeepOneHybrid2.DeepOneHybrid_MatialArts);
        }
        if (BattleState == "DeepOneHybrid3Attack")
        {
           // BattleState = "Rollet";
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : Evasion", "Evasion", DataBaseManager.evasionPoint, "evasion", "DeepOneHybrid", "??? : MartialArt", "MartialArt", DeepOneHybrid3.DeepOneHybrid_MatialArts);
        }
        if (BattleState == "DeepOneHybrid4Attack")
        {
            // BattleState = "Rollet";
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : Evasion", "Evasion", DataBaseManager.evasionPoint, "evasion", "DeepOneHybrid", "??? : MartialArt", "MartialArt", DeepOneHybrid4.DeepOneHybrid_MatialArts);
        }
    }
    public void PlayerChoiceButton_CounterAttack()
    {
        PlayerAction = "counterattack";
        if (BattleState == "DeepOneHybrid1Attack")
        {
          //  BattleState = "Rollet";
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : CounterAttack", "MartialArt", DataBaseManager.martialArtsPoint, "counterattack", "DeepOneHybrid", "??? : MartialArt", "MartialArt", DeepOneHybrid1.DeepOneHybrid_MatialArts);
        }
        if (BattleState == "DeepOneHybrid2Attack")
        {
          //  BattleState = "Rollet";
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : CounterAttack", "MartialArt", DataBaseManager.martialArtsPoint, "counterattack", "DeepOneHybrid", "??? : MartialArt", "MartialArt", DeepOneHybrid2.DeepOneHybrid_MatialArts);
        }
        if (BattleState == "DeepOneHybrid3Attack")
        {
          //  BattleState = "Rollet";
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : CounterAttack", "MartialArt", DataBaseManager.martialArtsPoint, "counterattack", "DeepOneHybrid", "??? : MartialArt", "MartialArt", DeepOneHybrid3.DeepOneHybrid_MatialArts);
        }
        if (BattleState == "DeepOneHybrid4Attack")
        {
            //  BattleState = "Rollet";
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : CounterAttack", "MartialArt", DataBaseManager.martialArtsPoint, "counterattack", "DeepOneHybrid", "??? : MartialArt", "MartialArt", DeepOneHybrid4.DeepOneHybrid_MatialArts);
        }
    }

    public void RetrunRolletResult(string PlayerAction, string Success, string Enemy)
    {

        if (PlayerAction == "evasion")
        {
            if(BattleState == "DeepOneHybrid1Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = DeepOneHybrid1_Object.transform.position;
                GameObject obj = DeepOneHybrid1_Object;
                SpriteRenderer spR = DeepOneHybrid1_Render;
                if (Success == "Success")
                {
                    BattleCameraMove.Instance.ZoomMidle();
                    DeepOneHybrid1_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(player.transform.position.x - 4f, DeepOneHybrid1_Object.transform.position.y), 0.7f))

                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1.5f, player.transform.position.y - 0.2f, -1), 0.7f))
                     .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid1_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                     .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
       
                }
                else
                {
                    BattleCameraMove.Instance.ZoomMidle();
                    DeepOneHybrid1_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                                         .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (BattleState == "DeepOneHybrid2Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = DeepOneHybrid2_Object.transform.position;
                GameObject obj = DeepOneHybrid2_Object;
                SpriteRenderer spR = DeepOneHybrid2_Render;
                if (Success == "Success")
                {
                    BattleCameraMove.Instance.ZoomDown();
                    DeepOneHybrid2_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                        .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))

                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1.5f, player.transform.position.y - 0.2f, -1), 0.7f))
                     .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid2_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                     .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }
                else
                {
                    BattleCameraMove.Instance.ZoomDown();
                    DeepOneHybrid2_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                                         .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (BattleState == "DeepOneHybrid3Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = DeepOneHybrid3_Object.transform.position;
                GameObject obj = DeepOneHybrid3_Object;
                SpriteRenderer spR = DeepOneHybrid3_Render;
                if (Success == "Success")
                {
                    BattleCameraMove.Instance.ZoomUp();
                    DeepOneHybrid3_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                        .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))

                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1.5f, player.transform.position.y - 0.2f, -1), 0.7f))
                     .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid3_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                     .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }
                else
                {
                    BattleCameraMove.Instance.ZoomUp();
                    DeepOneHybrid3_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                                         .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (BattleState == "DeepOneHybrid4Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = DeepOneHybrid4_Object.transform.position;
                GameObject obj = DeepOneHybrid4_Object;
                SpriteRenderer spR = DeepOneHybrid4_Render;
                if (Success == "Success")
                {
                    BattleCameraMove.Instance.ZoomUp();
                    DeepOneHybrid4_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                        .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))

                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1.5f, player.transform.position.y - 0.2f, -1), 0.7f))
                     .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid4_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                     .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }
                else
                {
                  //  BattleCameraMove.Instance.ZoomUp();
                    DeepOneHybrid4_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                                      .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
        }
        if (PlayerAction == "counterattack")
        {
            if (BattleState == "DeepOneHybrid1Attack")
            {                Vector3 playerOrigin = player.transform.position;
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 enemyOrigin = DeepOneHybrid1_Object.transform.position;
                GameObject obj = DeepOneHybrid1_Object;
                SpriteRenderer spR = DeepOneHybrid1_Render;
                if (Success == "Success")
                {
                    DeepOneHybrid1_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Punch))

                   .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByPunch))

                    .AppendCallback(() => CamRotate(+5))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid1", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                     .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(DeepOneHybrid1_Object.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 8f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x +2, player.transform.position.y, -1), 1f))

                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid1_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                     .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {

                    DeepOneHybrid1_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                                         .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if(BattleState == "DeepOneHybrid2Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                BattleCameraMove.Instance.ZoomDown();
                Vector3 enemyOrigin = DeepOneHybrid2_Object.transform.position;
                GameObject obj = DeepOneHybrid2_Object;
                SpriteRenderer spR = DeepOneHybrid2_Render;
                if (Success == "Success")
                {
                    DeepOneHybrid2_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Punch))

                   .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByPunch))

                    .AppendCallback(() => CamRotate(+5))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid2", Random.Range(1, 4) * 5))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                     .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(DeepOneHybrid2_Object.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 8f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x + 2, player.transform.position.y, -1), 1f))

                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid2_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                     .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {

                    DeepOneHybrid2_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                                         .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (BattleState == "DeepOneHybrid3Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = DeepOneHybrid3_Object.transform.position;
                GameObject obj = DeepOneHybrid3_Object;
                SpriteRenderer spR = DeepOneHybrid3_Render;
                if (Success == "Success")
                {
                    DeepOneHybrid3_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                                                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Punch))

                   .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByPunch))

                    .AppendCallback(() => CamRotate(+5))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid3", Random.Range(1, 4) * 5))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                     .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(DeepOneHybrid3_Object.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 8f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x + 2, player.transform.position.y, -1), 1f))

                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid3_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                     .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {

                    DeepOneHybrid3_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                                         .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (BattleState == "DeepOneHybrid4Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = DeepOneHybrid4_Object.transform.position;
                GameObject obj = DeepOneHybrid4_Object;
                SpriteRenderer spR = DeepOneHybrid4_Render;
                if (Success == "Success")
                {
                    DeepOneHybrid4_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                                                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Punch))

                   .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByPunch))

                   .AppendCallback(() => CamRotate(+1))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid4", Random.Range(1, 4) * 5))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                     .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(DeepOneHybrid4_Object.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 8f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x + 2, player.transform.position.y, -1), 1f))

                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid4_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                     .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {

                    DeepOneHybrid4_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                                         .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
        }

        if (PlayerAction == "MA_attack_Counter")
        {
            if (onPointerEnemy == "DeepOneHybrid1")
            {
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 enemyOrigin = DeepOneHybrid1_Object.transform.position;
                GameObject obj = DeepOneHybrid1_Object;
                SpriteRenderer spR = DeepOneHybrid1_Render;
                if (Success == "Success")
                {
                    player_R.sprite = Punch;
                    Cam.transform.DORotate(new Vector3(0,0,5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 5.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByPunch))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid1", Random.Range(1, 4) * 5))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }
                else
                {
                    player_R.sprite = Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(DeepOneHybrid1_Object.transform.position.x - 5.5f, DeepOneHybrid1_Object.transform.position.y - 0.2f, -1), 0.5f))
                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid1_Object.transform.position.x + 2f, DeepOneHybrid1_Object.transform.position.y), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid1_Render, DeepOneHybrid_Punch))

                    .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid1_Object.transform.position.x - 2f, DeepOneHybrid1_Object.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .AppendCallback(() => CamRotate(-5))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(DeepOneHybrid1_Object.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x  - 3f, obj.transform.position.y), 1.5f))

                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid1_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())

                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                     .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }
            }
            else if (onPointerEnemy == "DeepOneHybrid2")
            {
                BattleCameraMove.Instance.ZoomDown();
                Vector3 enemyOrigin = DeepOneHybrid2_Object.transform.position;
                GameObject obj = DeepOneHybrid2_Object;
                SpriteRenderer spR = DeepOneHybrid2_Render;
                if (Success == "Success")
                {
                    player_R.sprite = Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 5.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByPunch))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid2", Random.Range(1, 4) * 5))
                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }
                else
                {
                    player_R.sprite = Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(DeepOneHybrid2_Object.transform.position.x - 5.5f, DeepOneHybrid2_Object.transform.position.y - 0.2f, -1), 0.5f))
                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid2_Object.transform.position.x + 2f, DeepOneHybrid2_Object.transform.position.y), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid2_Render, DeepOneHybrid_Punch))

                    .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid2_Object.transform.position.x - 2f, DeepOneHybrid2_Object.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .AppendCallback(() => CamRotate(-5))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(DeepOneHybrid2_Object.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid2_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())

                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                     .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }
            }
            else if (onPointerEnemy == "DeepOneHybrid3")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = DeepOneHybrid3_Object.transform.position;
                GameObject obj = DeepOneHybrid3_Object;
                SpriteRenderer spR = DeepOneHybrid3_Render;
                if (Success == "Success")
                {
                    player_R.sprite = Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 5.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByPunch))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid3", Random.Range(1, 4) * 5))
                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }
                else
                {
                    player_R.sprite = Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(DeepOneHybrid3_Object.transform.position.x - 5.5f, DeepOneHybrid3_Object.transform.position.y - 0.2f, -1), 0.5f))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid3_Object.transform.position.x + 2f, DeepOneHybrid3_Object.transform.position.y), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid3_Render, DeepOneHybrid_Punch))

                    .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid3_Object.transform.position.x - 2f, DeepOneHybrid3_Object.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .AppendCallback(() => CamRotate(-5))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(DeepOneHybrid3_Object.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid3_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())

                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                     .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }

            }
            else if (onPointerEnemy == "DeepOneHybrid4")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = DeepOneHybrid4_Object.transform.position;
                GameObject obj = DeepOneHybrid4_Object;
                SpriteRenderer spR = DeepOneHybrid4_Render;
                if (Success == "Success")
                {
                    player_R.sprite = Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 5.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByPunch))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid4", Random.Range(1, 4) * 5))
                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                     .AppendCallback(() => CamRotate(0))
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }
                else
                {
                    player_R.sprite = Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(DeepOneHybrid4_Object.transform.position.x - 5.5f, DeepOneHybrid4_Object.transform.position.y - 0.2f, -1), 0.5f))
                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid4_Object.transform.position.x + 2f, DeepOneHybrid4_Object.transform.position.y), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid4_Render, DeepOneHybrid_Punch))

                    .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid4_Object.transform.position.x - 2f, DeepOneHybrid4_Object.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .AppendCallback(() => CamRotate(-1))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(DeepOneHybrid4_Object.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid4_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                     .AppendCallback(() => CamRotate(0))
                    .AppendCallback(() => TurnEnd())

                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                     .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }

            }
        }
        if (PlayerAction == "SS_attack_Counter")
        {
            if (onPointerEnemy == "DeepOneHybrid1")
            {
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 enemyOrigin = DeepOneHybrid1_Object.transform.position;
                GameObject obj = DeepOneHybrid1_Object;
                SpriteRenderer spR = DeepOneHybrid1_Render;
                if (Success == "Success")
                {
                    if(DataBaseManager.BattleWeapon == "Dagger")
                    {
                        player_R.sprite = Dagger_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        player_R.sprite = Bat_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        player_R.sprite = Axe_Ready;
                    }
                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);

                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByNife))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Dagger_Attack))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 5) + 2) * 5))//1D4 + 2
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByBat))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Bat_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 9))  * 5))//1D8
                                             .AppendCallback(() => SoundManager.Instance.Battle_Sound("Bat_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByAxe))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Axe_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 7) +2) * 5))//1D8
                                             .AppendCallback(() => SoundManager.Instance.Battle_Sound("Axe_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }

                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        player_R.sprite = Dagger_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        player_R.sprite = Bat_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        player_R.sprite = Axe_Ready;
                    }



                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid1_Object.transform.position.x - 3.5f, DeepOneHybrid1_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Dagger_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid1_Object.transform.position.x + 2f, DeepOneHybrid1_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid1_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid1_Object.transform.position.x - 2f, DeepOneHybrid1_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                 .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid1_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid1_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid1_Object.transform.position.x - 3.5f, DeepOneHybrid1_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Bat_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid1_Object.transform.position.x + 2f, DeepOneHybrid1_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid1_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid1_Object.transform.position.x - 2f, DeepOneHybrid1_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                       .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid1_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid1_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid1_Object.transform.position.x - 3.5f, DeepOneHybrid1_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Axe_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid1_Object.transform.position.x + 2f, DeepOneHybrid1_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid1_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid1_Object.transform.position.x - 2f, DeepOneHybrid1_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                       .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid1_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid1_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    // 연출 삽입


                }
            }
            else if (onPointerEnemy == "DeepOneHybrid2")
            {
                BattleCameraMove.Instance.ZoomDown();
                Vector3 enemyOrigin = DeepOneHybrid2_Object.transform.position;
                GameObject obj = DeepOneHybrid2_Object;
                SpriteRenderer spR = DeepOneHybrid2_Render;
                if (Success == "Success")
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        player_R.sprite = Dagger_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        player_R.sprite = Bat_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        player_R.sprite = Axe_Ready;
                    }
                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);

                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByNife))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Dagger_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 5) + 2) * 5))//1D4 + 2
                                                                     .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByBat))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Bat_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 9)) * 5))//1D8
                                                                     .AppendCallback(() => SoundManager.Instance.Battle_Sound("Bat_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByAxe))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Axe_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 7) + 2) * 5))//1D8
                                                                     .AppendCallback(() => SoundManager.Instance.Battle_Sound("Axe_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }

                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        player_R.sprite = Dagger_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        player_R.sprite = Bat_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        player_R.sprite = Axe_Ready;
                    }



                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid2_Object.transform.position.x - 3.5f, DeepOneHybrid2_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Dagger_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid2_Object.transform.position.x + 2f, DeepOneHybrid2_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid2_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid2_Object.transform.position.x - 2f, DeepOneHybrid2_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid2_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid2_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid2_Object.transform.position.x - 3.5f, DeepOneHybrid2_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Bat_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid2_Object.transform.position.x + 2f, DeepOneHybrid2_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid2_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid2_Object.transform.position.x - 2f, DeepOneHybrid2_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                      .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid2_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid2_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid2_Object.transform.position.x - 3.5f, DeepOneHybrid2_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Axe_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid2_Object.transform.position.x + 2f, DeepOneHybrid2_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid2_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid2_Object.transform.position.x - 2f, DeepOneHybrid2_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                      .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid2_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid2_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    // 연출 삽입


                }
            }
            else if (onPointerEnemy == "DeepOneHybrid3")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = DeepOneHybrid3_Object.transform.position;
                GameObject obj = DeepOneHybrid3_Object;
                SpriteRenderer spR = DeepOneHybrid3_Render;
                if (Success == "Success")
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        player_R.sprite = Dagger_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        player_R.sprite = Bat_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        player_R.sprite = Axe_Ready;
                    }
                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);

                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByNife))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Dagger_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 5) + 2) * 5))//1D4 + 2
                                           .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByBat))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Bat_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 9)) * 5))//1D8
                                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Bat_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByAxe))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Axe_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 7) + 2) * 5))//1D8
                                                                     .AppendCallback(() => SoundManager.Instance.Battle_Sound("Axe_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }

                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        player_R.sprite = Dagger_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        player_R.sprite = Bat_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        player_R.sprite = Axe_Ready;
                    }



                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid3_Object.transform.position.x - 3.5f, DeepOneHybrid3_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Dagger_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid3_Object.transform.position.x + 2f, DeepOneHybrid3_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid3_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid3_Object.transform.position.x - 2f, DeepOneHybrid3_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                                     .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid3_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid3_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid3_Object.transform.position.x - 3.5f, DeepOneHybrid3_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Bat_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid3_Object.transform.position.x + 2f, DeepOneHybrid3_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid3_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid3_Object.transform.position.x - 2f, DeepOneHybrid3_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid3_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid3_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid3_Object.transform.position.x - 3.5f, DeepOneHybrid3_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Axe_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid3_Object.transform.position.x + 2f, DeepOneHybrid3_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid3_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid3_Object.transform.position.x - 2f, DeepOneHybrid3_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid3_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid3_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    // 연출 삽입


                }
            }
            else if (onPointerEnemy == "DeepOneHybrid4")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = DeepOneHybrid4_Object.transform.position;
                GameObject obj = DeepOneHybrid4_Object;
                SpriteRenderer spR = DeepOneHybrid4_Render;
                if (Success == "Success")
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        player_R.sprite = Dagger_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        player_R.sprite = Bat_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        player_R.sprite = Axe_Ready;
                    }
                    Cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);

                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByNife))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Dagger_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 5) + 2) * 5))//1D4 + 2
                                           .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByBat))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Bat_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 9)) * 5))//1D8
                                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Bat_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByAxe))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Axe_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 7) + 2) * 5))//1D8
                                                                     .AppendCallback(() => SoundManager.Instance.Battle_Sound("Axe_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }

                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        player_R.sprite = Dagger_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        player_R.sprite = Bat_Ready;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        player_R.sprite = Axe_Ready;
                    }



                    Cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid4_Object.transform.position.x - 3.5f, DeepOneHybrid4_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Dagger_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid4_Object.transform.position.x + 2f, DeepOneHybrid4_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid4_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid4_Object.transform.position.x - 2f, DeepOneHybrid4_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                                     .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid4_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid4_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid4_Object.transform.position.x - 3.5f, DeepOneHybrid4_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Bat_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid4_Object.transform.position.x + 2f, DeepOneHybrid4_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid4_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid4_Object.transform.position.x - 2f, DeepOneHybrid4_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid4_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid4_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid4_Object.transform.position.x - 3.5f, DeepOneHybrid4_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Axe_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid4_Object.transform.position.x + 2f, DeepOneHybrid4_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid4_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid4_Object.transform.position.x - 2f, DeepOneHybrid4_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                                                                            .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid4_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid4_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    // 연출 삽입


                }
            }

        }
        if (PlayerAction == "MS_attack")
        {
            if (onPointerEnemy == "DeepOneHybrid1")
            {
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 enemyOrigin = DeepOneHybrid1_Object.transform.position;
                Vector3 playerOrigin = player.transform.position;
                GameObject obj = DeepOneHybrid1_Object;
                SpriteRenderer spR = DeepOneHybrid1_Render;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        player_R.sprite = SmallPistol_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        player_R.sprite = Revolver_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        player_R.sprite = Rifle_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        player_R.sprite = Shotgun_Ready;
                    }

                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);

                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedBySmallPistol))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, SmallPistol_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 7)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByRevolver))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Revolver_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 11)+2) * 5))//1d10+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByRifle))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Rifle_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 7)+ Random.Range(1, 7) + 2) * 5))//2D6+2
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByShotgun))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Shotgun_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 6)+ Random.Range(1, 6)+ Random.Range(1, 6)+ Random.Range(1, 6)) * 5))//1D6
                           .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }


                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        player_R.sprite = SmallPistol_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        player_R.sprite = Revolver_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        player_R.sprite = Rifle_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        player_R.sprite = Shotgun_Ready;
                    }




                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, SmallPistol_Attack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                             
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Revolver_Attack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))

                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Rifle_Attack))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Shotgun_Attack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }

                    // 연출 삽입


                }
            }
            else if (onPointerEnemy == "DeepOneHybrid2")
            {
                BattleCameraMove.Instance.ZoomDown();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = DeepOneHybrid2_Object.transform.position;
                GameObject obj = DeepOneHybrid2_Object;
                SpriteRenderer spR = DeepOneHybrid2_Render;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        player_R.sprite = SmallPistol_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        player_R.sprite = Revolver_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        player_R.sprite = Rifle_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        player_R.sprite = Shotgun_Ready;
                    }

                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);

                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedBySmallPistol))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, SmallPistol_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 7)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByRevolver))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Revolver_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 11) + 2) * 5))//1d10+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByRifle))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Rifle_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2D6+2
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByShotgun))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Shotgun_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6)) * 5))//1D6
                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }


                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        player_R.sprite = SmallPistol_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        player_R.sprite = Revolver_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        player_R.sprite = Rifle_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        player_R.sprite = Shotgun_Ready;
                    }




                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, SmallPistol_Attack))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Revolver_Attack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))

                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Rifle_Attack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x -3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Shotgun_Attack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }

                    // 연출 삽입


                }
            }
            else if (onPointerEnemy == "DeepOneHybrid3")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = DeepOneHybrid3_Object.transform.position;
                GameObject obj = DeepOneHybrid3_Object;
                SpriteRenderer spR = DeepOneHybrid3_Render;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        player_R.sprite = SmallPistol_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        player_R.sprite = Revolver_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        player_R.sprite = Rifle_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        player_R.sprite = Shotgun_Ready;
                    }

                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);

                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedBySmallPistol))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, SmallPistol_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 7)) * 5))//1D6
                                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByRevolver))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Revolver_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 11) + 2) * 5))//1d10+2
                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByRifle))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Rifle_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2D6+2
                          .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByShotgun))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Shotgun_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6)) * 5))//1D6
                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }


                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        player_R.sprite = SmallPistol_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        player_R.sprite = Revolver_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        player_R.sprite = Rifle_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        player_R.sprite = Shotgun_Ready;
                    }




                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, SmallPistol_Attack))
                                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Revolver_Attack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Rifle_Attack))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Shotgun_Attack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }

                    // 연출 삽입


                }
            }
            else if (onPointerEnemy == "DeepOneHybrid4")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = DeepOneHybrid4_Object.transform.position;
                GameObject obj = DeepOneHybrid4_Object;
                SpriteRenderer spR = DeepOneHybrid4_Render;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        player_R.sprite = SmallPistol_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        player_R.sprite = Revolver_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        player_R.sprite = Rifle_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        player_R.sprite = Shotgun_Ready;
                    }

                    Cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);

                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedBySmallPistol))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, SmallPistol_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 7)) * 5))//1D6
                                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByRevolver))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Revolver_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 11) + 2) * 5))//1d10+2
                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByRifle))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Rifle_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2D6+2
                          .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Hit"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByShotgun))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Shotgun_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6)) * 5))//1D6
                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Hit"))
                       .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }


                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        player_R.sprite = SmallPistol_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        player_R.sprite = Revolver_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        player_R.sprite = Rifle_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        player_R.sprite = Shotgun_Ready;
                    }




                    Cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, SmallPistol_Attack))
                                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Revolver_Attack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Rifle_Attack))
                         .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Shotgun_Attack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }

                    // 연출 삽입


                }
            }
        }
        if (PlayerAction == "Deftness_attack")
        {
            if (onPointerEnemy == "DeepOneHybrid1")
            {
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = DeepOneHybrid1_Object.transform.position;
                GameObject obj = DeepOneHybrid1_Object;
                SpriteRenderer spR = DeepOneHybrid1_Render;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        player_R.sprite = Rock_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        player_R.sprite = Molotov_Ready;
                    }
         

                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);

                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByRock))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 3)) * 5))//1D6
                                                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rock_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByMolotov))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2d6+2
                                       .AppendCallback(() => SoundManager.Instance.Battle_Sound("Molotov_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }



                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        player_R.sprite = Rock_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        player_R.sprite = Molotov_Ready;
                    }
                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                                                               .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                                                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }


                    // 연출 삽입


                }
            }
            else if (onPointerEnemy == "DeepOneHybrid2")
            {
                BattleCameraMove.Instance.ZoomDown();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = DeepOneHybrid2_Object.transform.position;
                GameObject obj = DeepOneHybrid2_Object;
                SpriteRenderer spR = DeepOneHybrid2_Render;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        player_R.sprite = Rock_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        player_R.sprite = Molotov_Ready;
                    }


                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);

                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByRock))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 3)) * 5))//1D6
                                                             .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rock_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByMolotov))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2d6+2
                                                             .AppendCallback(() => SoundManager.Instance.Battle_Sound("Molotov_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }



                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        player_R.sprite = Rock_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        player_R.sprite = Molotov_Ready;
                    }
                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                                                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                                                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }


                    // 연출 삽입


                }
            }
            else if (onPointerEnemy == "DeepOneHybrid3")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = DeepOneHybrid3_Object.transform.position;
                GameObject obj = DeepOneHybrid3_Object;
                SpriteRenderer spR = DeepOneHybrid3_Render;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        player_R.sprite = Rock_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        player_R.sprite = Molotov_Ready;
                    }


                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);

                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByRock))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 3)) * 5))//1D6
                                                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rock_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByMolotov))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2d6+2
                                                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Molotov_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }



                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        player_R.sprite = Rock_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        player_R.sprite = Molotov_Ready;
                    }
                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                                                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                                                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }


                    // 연출 삽입


                }
            }
            else if (onPointerEnemy == "DeepOneHybrid4")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = DeepOneHybrid4_Object.transform.position;
                GameObject obj = DeepOneHybrid4_Object;
                SpriteRenderer spR = DeepOneHybrid4_Render;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        player_R.sprite = Rock_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        player_R.sprite = Molotov_Ready;
                    }


                    Cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);

                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByRock))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 3)) * 5))//1D6
                                                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rock_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByMolotov))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2d6+2
                                                                                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Molotov_Attack"))
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }



                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        player_R.sprite = Rock_Ready;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        player_R.sprite = Molotov_Ready;
                    }
                    Cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                                                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Deftness_Attack))
                                                                                .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                        .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                                         .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(OriginPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }


                    // 연출 삽입


                }
            }
        }
    }






    void OnDamageObject(string subject, int damage, string Weapon ="")
    {
        if(subject == "DeepOneHybrid1")
        {
            DeepOneHybrid1.NowHP -= damage;
            DamageNumber damageNumber = numberPrefab.Spawn( DeepOneHybrid1_Object.transform.position, damage);

        }
        if (subject == "DeepOneHybrid2")
        {
            DeepOneHybrid2.NowHP -= damage;
            DamageNumber damageNumber = numberPrefab.Spawn(DeepOneHybrid2_Object.transform.position, damage);

        }
        if (subject == "DeepOneHybrid3")
        {
            DeepOneHybrid3.NowHP -= damage;
            DamageNumber damageNumber = numberPrefab.Spawn(DeepOneHybrid3_Object.transform.position, damage);

        }
        if (subject == "DeepOneHybrid4")
        {
            DeepOneHybrid4.NowHP -= damage;
            DamageNumber damageNumber = numberPrefab.Spawn(DeepOneHybrid4_Object.transform.position, damage);

        }
        if (subject == "player")
        {
            if(DataBaseManager.Masochism == true)
            {
              //  DamageNumber damageNumber = numberPrefab.Spawn(player.transform.position, damage);
               // DamageNumber damageNumber2 = numberPrefab.Spawn(player.transform.position, 5);
                BillowUIManager.Instance.HP_Battledown(damage);
            }
            else
            {
               // DamageNumber damageNumber = numberPrefab.Spawn(player.transform.position, damage);
                BillowUIManager.Instance.HP_Battledown(damage);
            }

        }
    }
    void OnSpriteChangeComplete(SpriteRenderer subject, Sprite sprite)
    {
        subject.sprite = sprite;

        // 변경된 스프라이트 복원 트윈 애니메이션 실행
        subject.DOBlendableColor(Color.white, 1);
    }
    void CamRotate(int R)
    {
        Cam.transform.DORotate(new Vector3(0, 0, R), 0.5f);
    }


    void EnemyHealthCheck()
    {
        if(DeepOneHybrid1.NowHP <= 0 && BattleState == "setTrun")
        {
            DeepOneHybrid1_Object.SetActive(false);
            EnemyTrunSymbol_1.SetActive(false);

        }
        if (DeepOneHybrid2.NowHP <= 0 && BattleState == "setTrun")
        {
            DeepOneHybrid2_Object.SetActive(false);
            EnemyTrunSymbol_2.SetActive(false);

        }
        if (DeepOneHybrid3.NowHP <= 0 && BattleState == "setTrun")
        {
            DeepOneHybrid3_Object.SetActive(false);
            EnemyTrunSymbol_3.SetActive(false);
        }
        if (DeepOneHybrid4.NowHP <= 0 && BattleState == "setTrun")
        {
            DeepOneHybrid4_Object.SetActive(false);
            EnemyTrunSymbol_1.SetActive(false);
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
        DataBaseManager.BattleWeapon = "";
        battleResetUI.EndTrun_Reset();
           onPointerEnemy = "";
        PlayerAction = "";
        SelectEnemy = "";
        BattleState = "setTrun";
        fDestroyTime = 0.01f;
        BattleCameraMove.Instance.ResetCam();

              PlayerChoiceUi.SetActive(false);
      PlayerActionUi.SetActive(false);
      PlayerAttackUi.SetActive(false);
      PlayerSwordsUi.SetActive(false);
        PlayerMarkmenUi.SetActive(false); 
        PlayerReloadUi.SetActive(false); 
        PlayerReloadCheckUi.SetActive(false);
        PlayerDeftnessUi.SetActive(false);

        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        DeepOneHybrid1_Object.transform.position = new Vector3(DeepOneHybrid1_Object.transform.position.x, DeepOneHybrid1_Object.transform.position.y, 0);
    }

    public GameObject PlayerMAUI;
    public void SetAciton(string action)
    {
        MABackButton.SetActive(true);
        SoundManager.Instance.ClickSound_Play();
        BattleManager.Instance.EnemySelectUI.SetActive(true);
        PlayerActionUi.SetActive(false);
        PlayerAttackUi.SetActive(false);
        PlayerMAUI.SetActive(true);
        BattleState = "selectEnemy";
        PlayerAction = action;
    }
    public GameObject MABackButton;
    public void Back_MA()
    {

        MABackButton.SetActive(false);
        SoundManager.Instance.ClickSound_Play();
        EnemySelectUI.SetActive(false);
        PlayerAttackUi.SetActive(true);
        PlayerMAUI.SetActive(false);
        BattleState = "PlayerAttack";
    }
    public void SetEnemy(string enemy)
    {
        EnemySelectUI.SetActive(false);
        if (enemy == "DeepOneHybrid1")
        {
            if (PlayerAction == "martialarts")
            {

                MABackButton.SetActive(false);
                PlayerMAUI.SetActive(false);
                BattleManager.Instance.EnemySelectUI.SetActive(false);
                BattleState = "Rollet";
                BattleRollet.Instance.setBattleRollet("Fabian : Punching", "martialArtsPoint", DataBaseManager.martialArtsPoint, "MA_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid1.DeepOneHybrid_MatialArts);
            }
            if (PlayerAction == "PlayerSwords")
            {
                PlayerSwordsUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Dagger")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Stab", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid1.DeepOneHybrid_MatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Bat")
                {
                   BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid1.DeepOneHybrid_MatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Axe")
                {
                   BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid1.DeepOneHybrid_MatialArts);
                }
            }
            if (PlayerAction == "PlayerMarkmen")
            {
                PlayerMarkmenUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "SmallPistol")
                {
                   BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Rifle")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Revolver")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Shotgun")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
            }
            if (PlayerAction == "PlayerDeftness")
            {
                PlayerDeftnessUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Rock")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Molotov")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }
               
            }
        }
        if (enemy == "DeepOneHybrid2")
        {
            if (PlayerAction == "martialarts")
            {
                BattleState = "Rollet";
                BattleRollet.Instance.setBattleRollet("Fabian : Punching", "martialArtsPoint", DataBaseManager.martialArtsPoint, "MA_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid2.DeepOneHybrid_MatialArts);
            }
            if (PlayerAction == "PlayerSwords")
            {
                PlayerSwordsUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Dagger")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Stab", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid2.DeepOneHybrid_MatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Bat")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid2.DeepOneHybrid_MatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Axe")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid2.DeepOneHybrid_MatialArts);
                }
            }
            if (PlayerAction == "PlayerMarkmen")
            {
                PlayerMarkmenUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "SmallPistol")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Rifle")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Revolver")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Shotgun")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
            }
            if (PlayerAction == "PlayerDeftness")
            {
                PlayerDeftnessUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Rock")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Molotov")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }

            }
        }
        if (enemy == "DeepOneHybrid3")
        {
            if (PlayerAction == "martialarts")
            {
                BattleState = "Rollet";
                BattleRollet.Instance.setBattleRollet("Fabian : Punching", "martialArtsPoint", DataBaseManager.martialArtsPoint, "MA_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid3.DeepOneHybrid_MatialArts);
            }
            if (PlayerAction == "PlayerSwords")
            {
                PlayerSwordsUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Dagger")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Stab", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid3.DeepOneHybrid_MatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Bat")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid3.DeepOneHybrid_MatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Axe")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid3.DeepOneHybrid_MatialArts);
                }
            }
            if (PlayerAction == "PlayerMarkmen")
            {
                PlayerMarkmenUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "SmallPistol")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Rifle")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Revolver")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Shotgun")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
            }
            if (PlayerAction == "PlayerDeftness")
            {
                PlayerDeftnessUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Rock")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Molotov")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }

            }
        }
        if (enemy == "DeepOneHybrid4")
        {
            if (PlayerAction == "martialarts")
            {
                BattleState = "Rollet";
                BattleRollet.Instance.setBattleRollet("Fabian : Punching", "martialArtsPoint", DataBaseManager.martialArtsPoint, "MA_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid4.DeepOneHybrid_MatialArts);
            }
            if (PlayerAction == "PlayerSwords")
            {
                PlayerSwordsUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Dagger")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Stab", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid4.DeepOneHybrid_MatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Bat")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid4.DeepOneHybrid_MatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Axe")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", DeepOneHybrid4.DeepOneHybrid_MatialArts);
                }
            }
            if (PlayerAction == "PlayerMarkmen")
            {
                PlayerMarkmenUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "SmallPistol")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Rifle")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Revolver")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Shotgun")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
            }
            if (PlayerAction == "PlayerDeftness")
            {
                PlayerDeftnessUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Rock")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Molotov")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }

            }
        }
    }
}
