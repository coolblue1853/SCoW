using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using DamageNumbersPro;
using UnityEngine.Serialization;
public class BattleManager : MonoBehaviour
{
    public GameObject uiButton;
    public GameObject cam;
    public GameObject roundGameObject;
    public string battleState;
    private float fDestroyTime = 0.01f;
    private float fTickTime;
    public bool startBattle = false;
    public TextMeshProUGUI roundText;
    private static BattleManager instance = null;
    public BattleResetUI battleResetUI;
    public DamageNumber numberPrefab;

    //플레이어 관련
    public GameObject player;
    public Vector3 originPoint;
    public SpriteRenderer playerRender;
    public Sprite stand;
    public Sprite punch;
    public Sprite hitted;
    public Sprite daggerReady;
    public Sprite daggerAttack;
    public Sprite batReady;
    public Sprite batAttack;
    public Sprite axeReady;
    public Sprite axeAttack;
    public Sprite smallPistolReady;
    public Sprite smallPistolAttack;
    public Sprite revolverReady;
    public Sprite revolverAttack;
    public Sprite rifleReady;
    public Sprite rifleAttack;
    public Sprite shotgunReady;
    public Sprite shotgunAttack;
    public Sprite rockReady;
    public Sprite molotovReady;
    public Sprite deftnessAttack;
    public Sprite smallPistolReload;
    public Sprite revolverReload;
    public Sprite rifleReload;
    public Sprite shotgunReload;

    public GameObject playerTrunSymbol;
    public GameObject enemyTrunSymbol1;
    public GameObject enemyTrunSymbol2;
    public GameObject enemyTrunSymbol3;
    public GameObject playerChoiceUi;
    public GameObject playerActionUi;
    public GameObject playerAttackUi;
    public GameObject playerSwordsUi;
    public GameObject playerMarkmenUi;
    public GameObject playerReloadUi;
    public GameObject playerReloadItemUI;
    public GameObject playerReloadCheckUi;
    public GameObject playerDeftnessUi;
    public GameObject enemySelectUI;
    public BattlePlayer battlePlayer;
    EnemyClass.DeepOneHybrid deepOneHybrid1 = new EnemyClass.DeepOneHybrid();
    EnemyClass.DeepOneHybrid deepOneHybrid2 = new EnemyClass.DeepOneHybrid();
    EnemyClass.DeepOneHybrid deepOneHybrid3 = new EnemyClass.DeepOneHybrid();
    EnemyClass.DeepOneHybrid deepOneHybrid4 = new EnemyClass.DeepOneHybrid();
    EnemyClass.DeepOne deepOne1 = new EnemyClass.DeepOne();
    //적 오브젝트 관련
    public GameObject deepOneHybrid1Ob;
    public GameObject deepOneHybrid2Ob;
    public GameObject deepOneHybrid3Ob;
    public GameObject deepOneHybrid4Ob;
    public GameObject deepOne1Ob;
    public EnemySpriteChanger deepOneHybrid1SC;
    public EnemySpriteChanger deepOneHybrid2SC;
    public EnemySpriteChanger deepOneHybrid3SC;
    public EnemySpriteChanger deepOneHybrid4SC;
    public EnemySpriteChanger deepOne1SC;
    public string playerAction = "";
    public string selectEnemy = "";
    public string onPointerEnemy = "";
    public GameObject runAwayButton;
    int enemyCount = 0;
    public GameObject runAwayObject;
    public TextMeshProUGUI runAwayText;
    public GameObject playerMAUI;
    public GameObject maBackButton;

    public void OriginPointChange()
    {
        originPoint = player.transform.position;
    }
    void Update()
    {
        if (runAwayButton.activeSelf == true && DataBaseManager.isDebuff_ShortTempred == true)
        {
            runAwayButton.SetActive(false);
        }
        else if (runAwayButton.activeSelf == false && DataBaseManager.isDebuff_ShortTempred == false)
        {
            runAwayButton.SetActive(true);
        }
        if (startBattle == true && (enemyTrunSymbol1.activeSelf == true || enemyTrunSymbol2.activeSelf == true || enemyTrunSymbol3.activeSelf == true))
        {
            EnemyHealthCheck();
            TimeWait();
            StateChecker();
        }
        if (DataBaseManager.nowPlace == "BattleRoad")
        {
            if (enemyTrunSymbol1.activeSelf == false && enemyTrunSymbol2.activeSelf == false && enemyTrunSymbol3.activeSelf == false && DataBaseManager.RoadBattleEnd == false && DataBaseManager.ISRoadBattleStart == true)
            {
                DataBaseManager.ISRoadBattleStart = false;
                DataBaseManager.isDirecting = true;
                DataBaseManager.RoadBattleEnd = true;
                FadingBackGround.Instance.FadeIn();
                Invoke("EndRoadBattle", 1f);
            }
        }
        if (enemyTrunSymbol1.activeSelf == false && enemyTrunSymbol2.activeSelf == false && enemyTrunSymbol3.activeSelf == false && DataBaseManager.SewerBattleChecker == true && DataBaseManager.nowPlace == "InSewer" && DataBaseManager.SewerBattleEndCheck == false)
        {
            DataBaseManager.SewerBattleChecker = false;
            DataBaseManager.SewerBattleEndCheck = true;
            DataBaseManager.StoryDirecting = false;
            FadingBackGround.Instance.FadeInOut();
            Invoke("EndSewerBattle", 1f);
        }
    }
    public void CloseButtonUI()
    {
        uiButton.SetActive(false);
    }
    void EndRoadBattle()
    {
        playerActionUi.SetActive(false);
        uiButton.SetActive(true);
        InteractionController.Instance.BattleDialog("End");
        battleState = "setTrun";
        enemyTrunSymbol1.transform.localPosition = new Vector2(enemyTrunSymbol1.transform.localPosition.x, -130);
        enemyTrunSymbol2.transform.localPosition = new Vector2(enemyTrunSymbol1.transform.localPosition.x, -130);
        enemyTrunSymbol3.transform.localPosition = new Vector2(enemyTrunSymbol1.transform.localPosition.x, -130);
        playerTrunSymbol.transform.localPosition = new Vector2(enemyTrunSymbol1.transform.localPosition.x, -130);
    }
    public void EndSewerBattle()
    {
        playerActionUi.SetActive(false);
        uiButton.SetActive(true);
        DataBaseManager.InSewer_StealthSucc = true;
        DialogDatabaseManager.instance.check = true;
        DataBaseManager.isDirecting = false;
        DataBaseManager.StoryDirecting = false;
        DataBaseManager.isRollet = false;
        DataBaseManager.EndBattle = false;
        DirectingManager.Instance.EndBattle();
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
        playerRender = player.GetComponent<SpriteRenderer>();
        set_DeepOneHybrid1();
        set_DeepOneHybrid2();
        set_DeepOneHybrid3();
        set_DeepOneHybrid4();
        set_DeepOne1();
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void set_DeepOneHybrid1()
    {
        deepOneHybrid1.SetStatDeepOneHybrid();
    }
    public void set_DeepOneHybrid2()
    {
        deepOneHybrid2.SetStatDeepOneHybrid();
    }
    public void set_DeepOneHybrid3()
    {
        deepOneHybrid3.SetStatDeepOneHybrid();
    }
    public void set_DeepOneHybrid4()
    {
        deepOneHybrid4.SetStatDeepOneHybrid();
    }
    public void set_DeepOne1()
    {
        deepOne1.SetStatDeepOne();
    }
    public void TimeWait()
    {
        if (battleState == "setTrun")
        {
            fTickTime += Time.deltaTime;
            if (fTickTime >= fDestroyTime)
            {
                fTickTime = 0;
                fDestroyTime = fDestroyTime * 0.98f;
                if (playerTrunSymbol.activeSelf == true)
                {
                    SymbolUp(playerTrunSymbol, DataBaseManager.dex, "Player");
                }
                if (DataBaseManager.nowPlace != "InSewer")
                {
                    if (enemyTrunSymbol1.activeSelf == true)
                    {
                        SymbolUp(enemyTrunSymbol1, deepOneHybrid1.dex, "DeepOneHybrid1");
                    }
                    if (enemyTrunSymbol2.activeSelf == true)
                    {
                        SymbolUp(enemyTrunSymbol2, deepOneHybrid2.dex, "DeepOneHybrid2");
                    }
                    if (enemyTrunSymbol3.activeSelf == true)
                    {
                        SymbolUp(enemyTrunSymbol3, deepOneHybrid3.dex, "DeepOneHybrid3");
                    }
                }
                else
                {
                    if (enemyTrunSymbol1.activeSelf == true)
                    {
                        SymbolUp(enemyTrunSymbol1, deepOneHybrid4.dex, "DeepOneHybrid4");
                    }
                    if (enemyTrunSymbol2.activeSelf == true)
                    {
                        SymbolUp(enemyTrunSymbol2, deepOne1.dex, "DeepOne1");
                    }
                    if (enemyTrunSymbol3.activeSelf == true)
                    {
                    }
                }
            }
        }
    }

    // 턴 최소지점 -130, 최대지점 +130
    // Start is called before the first frame update
    void Start()
    {
        battleState = "setTrun";
    }
    public void SymbolUp(GameObject symbol, float dex, string Target)
    {
        if (battleState == "setTrun")
        {
            if (symbol.transform.localPosition.y < 130)
            {
                if (Target == "Player")
                {
                    symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, symbol.transform.localPosition.y + ((1 + (dex / 100)) * 2f));
                }
                else
                {
                    symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, symbol.transform.localPosition.y + ((1 + (dex / 100)) * 1.9f));
                }
            }
            else
            {
                SoundManager.Instance.SetTrunSound_Play();
                symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, -130);
                battleState = Target + "Trun";

                if (Target == "Player")
                {
                    roundText.text = "Player Trun";
                }
                else
                {
                    roundText.text = "Enemy Trun";
                }

                roundGameObject.SetActive(true);
            }
        }
    }
    public void StateChecker()
    {
        if (battleState == "DeepOneHybrid1Trun")
        {
            battleState = "DeepOneHybrid1Attack";
            Invoke("EnemySetAction", 1);
        }
        if (battleState == "DeepOneHybrid2Trun")
        {
            battleState = "DeepOneHybrid2Attack";
            Invoke("EnemySetAction", 1);
        }
        if (battleState == "DeepOneHybrid3Trun")
        {
            battleState = "DeepOneHybrid3Attack";
            Invoke("EnemySetAction", 1);
        }
        if (battleState == "DeepOneHybrid4Trun")
        {
            battleState = "DeepOneHybrid4Attack";
            Invoke("EnemySetAction", 1);
        }
        if (battleState == "DeepOne1Trun")
        {
            battleState = "DeepOne1Attack";
            Invoke("EnemySetAction", 1);
        }
        if (battleState == "PlayerTrun")
        {
            if (DataBaseManager.PosionDebuff > 0)
            {
                DataBaseManager.PosionDebuff -= 1;
                OnDamageObject("player", Random.Range(1, 2) * 5);
            }
            BattleItemManager.Instance.ManageItem();
            battleState = "PlayerChoice";
            Invoke("PlayerSetAction", 1);
        }
    }
    void PlayerSetAction()
    {
        roundGameObject.SetActive(false);
        playerActionUi.SetActive(true);
    }
    public void Player_BackAction()
    {
        SoundManager.Instance.ClickSound_Play();
        enemySelectUI.SetActive(false);
        playerAttackUi.SetActive(false);
        battleState = "PlayerChoice";
        playerActionUi.SetActive(true);
    }
    public void Player_setAttack()
    {
        SoundManager.Instance.ClickSound_Play();
        playerActionUi.SetActive(false);
        battleState = "PlayerAttack";
        playerAttackUi.SetActive(true);
    }
    public void Player_setSwords()
    {
        SoundManager.Instance.ClickSound_Play();
        playerAttackUi.SetActive(false);
        playerAction = "PlayerSwords";
        playerSwordsUi.SetActive(true);
    }
    public void Player_BackSwords()
    {
        SoundManager.Instance.ClickSound_Play();
        enemySelectUI.SetActive(false);
        playerSwordsUi.SetActive(false);
        playerAction = "PlayerAttack";
        playerAttackUi.SetActive(true);
        playerRender.sprite = stand;
    }
    public void Player_setMarkmens()
    {
        SoundManager.Instance.ClickSound_Play();
        playerAttackUi.SetActive(false);
        playerAction = "PlayerMarkmen";
        playerMarkmenUi.SetActive(true);
    }
    public void Player_BackMarkmens()
    {
        SoundManager.Instance.ClickSound_Play();
        enemySelectUI.SetActive(false);
        playerAttackUi.SetActive(true);
        playerAction = "PlayerAttack";
        playerMarkmenUi.SetActive(false);
        playerRender.sprite = stand;
    }
    public void Player_setDeftness()
    {
        SoundManager.Instance.ClickSound_Play();
        playerAttackUi.SetActive(false);
        playerAction = "PlayerDeftness";
        playerDeftnessUi.SetActive(true);
    }
    public void Player_BackDeftness()
    {
        SoundManager.Instance.ClickSound_Play();
        enemySelectUI.SetActive(false);
        playerAttackUi.SetActive(true);
        playerAction = "PlayerAttack";
        playerDeftnessUi.SetActive(false);
        playerRender.sprite = stand;
    }
    public void Player_setReload()
    {
        SoundManager.Instance.ClickSound_Play();
        playerActionUi.SetActive(false);
        playerAction = "PlayerReload";
        playerReloadUi.SetActive(true);
        playerReloadItemUI.SetActive(true);
    }
    public void Player_BackReload()
    {
        SoundManager.Instance.ClickSound_Play();
        playerActionUi.SetActive(true);
        playerAction = "PlayerChoice";
        playerReloadUi.SetActive(false);
        playerRender.sprite = stand;
    }
    public string nowReloadWeapon;
    public void OpenReloadUI(string subject)
    {
        nowReloadWeapon = subject;
        playerReloadItemUI.SetActive(false);
        playerReloadCheckUi.SetActive(true);
    }
    public void startReload()
    {
        while (DataBaseManager.PistolAmmo > 0)
        {
            if (DataBaseManager.nowSmallPistol == 2)
            {
                break;
            }
            DataBaseManager.nowSmallPistol += 1;
            DataBaseManager.PistolAmmo -= 1;
        }
        while (DataBaseManager.PistolAmmo > 0)
        {
            if (DataBaseManager.nowRevolver == 6)
            {
                break;
            }
            DataBaseManager.nowRevolver += 1;
            DataBaseManager.PistolAmmo -= 1;
        }
        while (DataBaseManager.RifleAmmo > 0)
        {
            if (DataBaseManager.nowRifle == 5)
            {
                break;
            }
            DataBaseManager.nowRifle += 1;
            DataBaseManager.RifleAmmo -= 1;
        }
        while (DataBaseManager.ShotgunAmmo > 0)
        {
            if (DataBaseManager.nowShotgun == 2)
            {
                break;
            }
            DataBaseManager.nowShotgun += 1;
            DataBaseManager.ShotgunAmmo -= 1;
        }
    }

    public void ReloadOk()
    {
        playerReloadCheckUi.SetActive(false);
        BattleCameraMove.Instance.ZoomMe();
        if (nowReloadWeapon == "SmallPistol")
        {
            SoundManager.Instance.Battle_Sound("SmallPisol_Reload");
            while (DataBaseManager.PistolAmmo > 0)
            {
                if (DataBaseManager.nowSmallPistol == 2)
                {
                    break;
                }
                DataBaseManager.nowSmallPistol += 1;
                DataBaseManager.PistolAmmo -= 1;
            }
            Sequence sequence = DOTween.Sequence()
            .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolReload))
            .AppendInterval(2f) // 2초 대기
            .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
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
            .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverReload))
            .AppendInterval(2f) // 2초 대기
            .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
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
            .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleReload))
            .AppendInterval(2f) // 2초 대기
            .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
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
            .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunReload))
            .AppendInterval(2f) // 2초 대기
            .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
            .AppendCallback(() => BattleCameraMove.Instance.ResetCam())
            .AppendCallback(() => TurnEnd());
        }
    }
    public void ReloadClose()
    {
        playerReloadItemUI.SetActive(true);
        playerReloadCheckUi.SetActive(false);
        nowReloadWeapon = "";
    }

    public void RunAway()
    {
        Debug.Log("Check");
        SoundManager.Instance.ClickSound_Play();
        enemyCount = 0;
        runAwayObject.SetActive(true);
        if (enemyTrunSymbol1.activeSelf == true)
        {
            enemyCount += 1;
        }
        if (enemyTrunSymbol2.activeSelf == true)
        {
            enemyCount += 1;
        }
        if (enemyTrunSymbol3.activeSelf == true)
        {
            enemyCount += 1;
        }
        runAwayText.text = "Attempt to Runaway. You are penalized by the number of enemies.\n\nDex(" + DataBaseManager.dex + ") / " + enemyCount + " = " + DataBaseManager.dex / enemyCount;
    }
    public void RunYes()
    {
        SoundManager.Instance.ClickSound_Play();
        BattleRollet.Instance.setBattleRollet("Fabian : Runaway", "DEX / " + enemyCount, DataBaseManager.dex / enemyCount, "RunAway", "DeepOneHybrid", "??? : Hold", "Dex", deepOneHybrid1.dex);
        runAwayObject.SetActive(false);
    }
    public void RunNo()
    {
        SoundManager.Instance.ClickSound_Play();
        runAwayObject.SetActive(false);
    }
    void EnemySetAction()
    {
        // 추후 이곳에 적의 행동 요령을 통해 어떤식으로 작동하는지 써둬야 할듯
        roundGameObject.SetActive(false);
        if (battleState == "DeepOneHybrid3Attack")
        {
            OpenRoundObject("An enemy tries to attack you with a Deftness.");
            Invoke("EnemyAttackWithDeftness", 1.5f);
        }
        else if (battleState == "DeepOne1Attack")
        {
            // 심해인 공격
            int Randint = Random.Range(0, 4);
            if (Randint == 0) // 이성 공격
            {
                OpenRoundObject("An enemy tries to initiate suspicious behavior.");
                Invoke("DeepOneSanAttack", 1.5f);
            }
            else if (Randint == 1) // 독 도트 공격
            {
                OpenRoundObject("An enemy tries to initiate suspicious behavior.");
                Invoke("DeepOnePosionAttack", 1.5f);
            }
            else // 일반 공격
            {
                OpenRoundObject("An enemy tries to attack you with a Swordsmanship.");
                playerChoiceUi.SetActive(true);
            }
        }
        else
        {
            OpenRoundObject("An enemy tries to attack you with a MartialArt.");
            playerChoiceUi.SetActive(true);
        }
    }
    public void DeepOneSanAttack()
    {
        deepOne1SC.DeepOne_SanAttackChange();
        cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
        // 연출 삽입
        Sequence sequence = DOTween.Sequence()
        .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
        .AppendCallback(() => battlePlayer.PlayerHittedSanAttack())
        .AppendCallback(() => SoundManager.Instance.Battle_Sound("San_Attack"))
        .Join(cam.transform.DOShakePosition(1, 2, 90))
        .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
        .AppendInterval(0.5f) // 2초 대기
        .AppendCallback(() => Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "BattleSan_DeepOne1"));
    }
    public void EndDeepOneSanAttack()
    {
        Sequence sequence = DOTween.Sequence()
        .AppendCallback(() => deepOne1SC.StandChange())
        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
        .AppendCallback(() => battlePlayer.resetPlayer())
        .AppendCallback(() => TurnEnd())
        .AppendCallback(() => CamRotate(0));

    }
    public void DeepOnePosionAttack()
    {
        Rollet.Instance.setRollet("??? : Deftness", "Deftness", deepOne1.dex, "DeepOneDeftness", "DeepOne");
    }
    public void EnemyAttackWithDeftness()
    {
        Rollet.Instance.setRollet("??? : Deftness", "Deftness", deepOneHybrid3.dex, "EnemyDeftness", "DeepOneHybrid");
    }
    public void PlayerChoiceButton_Evasion()
    {
        playerAction = "evasion";
        if (battleState == "DeepOneHybrid1Attack")
        {
            playerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : Evasion", "Evasion", DataBaseManager.evasionPoint, "evasion", "DeepOneHybrid", "??? : MartialArt", "MartialArt", deepOneHybrid1.deepOneHybridMatialArts);
        }
        if (battleState == "DeepOneHybrid2Attack")
        {
            playerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : Evasion", "Evasion", DataBaseManager.evasionPoint, "evasion", "DeepOneHybrid", "??? : MartialArt", "MartialArt", deepOneHybrid2.deepOneHybridMatialArts);
        }
        if (battleState == "DeepOneHybrid3Attack")
        {
            playerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : Evasion", "Evasion", DataBaseManager.evasionPoint, "evasion", "DeepOneHybrid", "??? : MartialArt", "MartialArt", deepOneHybrid3.deepOneHybridMatialArts);
        }
        if (battleState == "DeepOneHybrid4Attack")
        {
            playerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : Evasion", "Evasion", DataBaseManager.evasionPoint, "evasion", "DeepOneHybrid", "??? : MartialArt", "MartialArt", deepOneHybrid4.deepOneHybridMatialArts);
        }
        if (battleState == "DeepOne1Attack")
        {
            playerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : Evasion", "Evasion", DataBaseManager.evasionPoint, "evasion", "DeepOne", "??? : Poking", "SwordsManship", deepOne1.deepOneSewerdManShip);
        }
    }
    public void PlayerChoiceButton_CounterAttack()
    {
        playerAction = "counterattack";
        if (battleState == "DeepOneHybrid1Attack")
        {
            playerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : CounterAttack", "MartialArt", DataBaseManager.martialArtsPoint, "counterattack", "DeepOneHybrid", "??? : MartialArt", "MartialArt", deepOneHybrid1.deepOneHybridMatialArts);
        }
        if (battleState == "DeepOneHybrid2Attack")
        {
            playerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : CounterAttack", "MartialArt", DataBaseManager.martialArtsPoint, "counterattack", "DeepOneHybrid", "??? : MartialArt", "MartialArt", deepOneHybrid2.deepOneHybridMatialArts);
        }
        if (battleState == "DeepOneHybrid3Attack")
        {
            playerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : CounterAttack", "MartialArt", DataBaseManager.martialArtsPoint, "counterattack", "DeepOneHybrid", "??? : MartialArt", "MartialArt", deepOneHybrid3.deepOneHybridMatialArts);
        }
        if (battleState == "DeepOneHybrid4Attack")
        {
            playerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : CounterAttack", "MartialArt", DataBaseManager.martialArtsPoint, "counterattack", "DeepOneHybrid", "??? : MartialArt", "MartialArt", deepOneHybrid4.deepOneHybridMatialArts);
        }
        if (battleState == "DeepOne1Attack")
        {
            playerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("Fabian : CounterAttack", "MartialArt", DataBaseManager.martialArtsPoint, "counterattack", "DeepOne", "??? : Poking", "SwordsManship", deepOne1.deepOneSewerdManShip);
        }
    }
    public void RetrunRolletResult(string PlayerAction, string Success, string Enemy)
    {
        if (PlayerAction == "evasion")
        {
            if (battleState == "DeepOneHybrid1Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid1Ob.transform.position;
                GameObject obj = deepOneHybrid1Ob;
                if (Success == "Success")
                {
                    BattleCameraMove.Instance.ZoomMidle();
                    deepOneHybrid1SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(player.transform.position.x - 4f, deepOneHybrid1Ob.transform.position.y), 0.7f))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1.5f, player.transform.position.y - 0.2f, -1), 0.7f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid1SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    BattleCameraMove.Instance.ZoomMidle();
                    deepOneHybrid1SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid1SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (battleState == "DeepOneHybrid2Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid2Ob.transform.position;
                GameObject obj = deepOneHybrid2Ob;
                if (Success == "Success")
                {
                    BattleCameraMove.Instance.ZoomDown();
                    deepOneHybrid2SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1.5f, player.transform.position.y - 0.2f, -1), 0.7f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid2SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    BattleCameraMove.Instance.ZoomDown();
                    deepOneHybrid2SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + 1) * 5))
                    .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid2SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => battlePlayer.resetPlayer())
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (battleState == "DeepOneHybrid3Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid3Ob.transform.position;
                GameObject obj = deepOneHybrid3Ob;

                if (Success == "Success")
                {
                    BattleCameraMove.Instance.ZoomUp();
                    deepOneHybrid3SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1.5f, player.transform.position.y - 0.2f, -1), 0.7f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid3SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    BattleCameraMove.Instance.ZoomUp();
                    deepOneHybrid3SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid3SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (battleState == "DeepOneHybrid4Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid4Ob.transform.position;
                GameObject obj = deepOneHybrid4Ob;
                if (Success == "Success")
                {
                    BattleCameraMove.Instance.ZoomUp();
                    deepOneHybrid4SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1.5f, player.transform.position.y - 0.2f, -1), 0.7f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid4SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    //  BattleCameraMove.Instance.ZoomUp();
                    deepOneHybrid4SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                    .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + 1) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid4SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => battlePlayer.resetPlayer())
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (battleState == "DeepOne1Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOne1Ob.transform.position;
                GameObject obj = deepOne1Ob;
                if (Success == "Success")
                {

                    deepOne1SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1.5f, player.transform.position.y - 0.2f, -1), 0.7f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOne1SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    deepOne1SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + Random.Range(1, 4)) * 5))
                    .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOne1SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => battlePlayer.resetPlayer())
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
        }
        if (PlayerAction == "counterattack")
        {
            if (battleState == "DeepOneHybrid1Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 enemyOrigin = deepOneHybrid1Ob.transform.position;
                GameObject obj = deepOneHybrid1Ob;
                if (Success == "Success")
                {
                    deepOneHybrid1SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, punch))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 0.2f))
                    .AppendCallback(() => deepOneHybrid1SC.HittedByPunchChange())
                    .AppendCallback(() => CamRotate(+5))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid1", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(deepOneHybrid1Ob.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 8f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x + 2, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid1SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    deepOneHybrid1SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid1SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (battleState == "DeepOneHybrid2Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                BattleCameraMove.Instance.ZoomDown();
                Vector3 enemyOrigin = deepOneHybrid2Ob.transform.position;
                GameObject obj = deepOneHybrid2Ob;
                if (Success == "Success")
                {
                    deepOneHybrid2SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, punch))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 0.2f))
                    .AppendCallback(() => deepOneHybrid2SC.HittedByPunchChange())
                    .AppendCallback(() => CamRotate(+5))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid2", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(deepOneHybrid2Ob.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 8f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x + 2, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid2SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    deepOneHybrid2SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + 1) * 5))
                    .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid2SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => battlePlayer.resetPlayer())
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (battleState == "DeepOneHybrid3Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = deepOneHybrid3Ob.transform.position;
                GameObject obj = deepOneHybrid3Ob;
                if (Success == "Success")
                {
                    deepOneHybrid3SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, punch))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 0.2f))
                    .AppendCallback(() => deepOneHybrid3SC.HittedByPunchChange())
                    .AppendCallback(() => CamRotate(+5))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid3", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(deepOneHybrid3Ob.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 8f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x + 2, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid3SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    deepOneHybrid3SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid3SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (battleState == "DeepOneHybrid4Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = deepOneHybrid4Ob.transform.position;
                GameObject obj = deepOneHybrid4Ob;
                if (Success == "Success")
                {
                    deepOneHybrid4SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, punch))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 0.2f))
                    .AppendCallback(() => deepOneHybrid4SC.HittedByPunchChange())
                    .AppendCallback(() => CamRotate(+1))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid4", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(deepOneHybrid4Ob.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 8f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x + 2, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid4SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    deepOneHybrid4SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                    .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + 1) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid4SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => battlePlayer.resetPlayer())
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (battleState == "DeepOne1Attack")
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOne1Ob.transform.position;
                GameObject obj = deepOne1Ob;
                if (Success == "Success")
                {
                    deepOne1SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, punch))
                    .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 0.2f))
                    .AppendCallback(() => deepOne1SC.HittedByPunchChange())
                    .AppendCallback(() => CamRotate(+1))
                    .AppendCallback(() => OnDamageObject("DeepOne1", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(deepOne1Ob.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 8f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x + 2, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOne1SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    deepOne1SC.DeepOneHybrid_PunchChange();
                    cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + Random.Range(1, 4)) * 5))
                    .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOne1SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => battlePlayer.resetPlayer())
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
        }
        if (PlayerAction == "MA_attack_Counter")
        {
            if (onPointerEnemy == "DeepOneHybrid1")
            {
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 enemyOrigin = deepOneHybrid1Ob.transform.position;
                GameObject obj = deepOneHybrid1Ob;
                if (Success == "Success")
                {
                    playerRender.sprite = punch;
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 5.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => deepOneHybrid1SC.HittedByPunchChange())
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid1", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid1SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    playerRender.sprite = punch;
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 5.5f, deepOneHybrid1Ob.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x + 2f, deepOneHybrid1Ob.transform.position.y), 0.5f))
                    .AppendCallback(() => deepOneHybrid1SC.DeepOneHybrid_PunchChange())
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 2f, deepOneHybrid1Ob.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .AppendCallback(() => CamRotate(-5))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(deepOneHybrid1Ob.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid1SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (onPointerEnemy == "DeepOneHybrid2")
            {
                BattleCameraMove.Instance.ZoomDown();
                Vector3 enemyOrigin = deepOneHybrid2Ob.transform.position;
                GameObject obj = deepOneHybrid2Ob;

                if (Success == "Success")
                {
                    playerRender.sprite = punch;
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 5.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => deepOneHybrid2SC.HittedByPunchChange())
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid2", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid2SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    playerRender.sprite = punch;
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 5.5f, deepOneHybrid2Ob.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x + 2f, deepOneHybrid2Ob.transform.position.y), 0.5f))
                    .AppendCallback(() => deepOneHybrid2SC.DeepOneHybrid_PunchChange())
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 2f, deepOneHybrid2Ob.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + 1) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                    .AppendCallback(() => CamRotate(-5))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(deepOneHybrid2Ob.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid2SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => battlePlayer.resetPlayer())
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (onPointerEnemy == "DeepOneHybrid3")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = deepOneHybrid3Ob.transform.position;
                GameObject obj = deepOneHybrid3Ob;
                if (Success == "Success")
                {
                    playerRender.sprite = punch;
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 5.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => deepOneHybrid3SC.HittedByPunchChange())
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid3", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid3SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    playerRender.sprite = punch;
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 5.5f, deepOneHybrid3Ob.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x + 2f, deepOneHybrid3Ob.transform.position.y), 0.5f))
                    .AppendCallback(() => deepOneHybrid3SC.DeepOneHybrid_PunchChange())
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 2f, deepOneHybrid3Ob.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .AppendCallback(() => CamRotate(-5))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(deepOneHybrid3Ob.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid3SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (onPointerEnemy == "DeepOneHybrid4")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = deepOneHybrid4Ob.transform.position;
                GameObject obj = deepOneHybrid4Ob;
                if (Success == "Success")
                {
                    playerRender.sprite = punch;
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 5.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => deepOneHybrid4SC.HittedByPunchChange())
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid4", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid4SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => CamRotate(0))
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    playerRender.sprite = punch;
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 5.5f, deepOneHybrid4Ob.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x + 2f, deepOneHybrid4Ob.transform.position.y), 0.5f))
                    .AppendCallback(() => deepOneHybrid4SC.DeepOneHybrid_PunchChange())
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 2f, deepOneHybrid4Ob.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                    .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + 1) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                    .AppendCallback(() => CamRotate(-1))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(deepOneHybrid4Ob.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOneHybrid4SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => battlePlayer.resetPlayer())
                    .AppendCallback(() => CamRotate(0))
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
            else if (onPointerEnemy == "DeepOne1")
            {
                Vector3 enemyOrigin = deepOne1Ob.transform.position;
                GameObject obj = deepOne1Ob;
                if (Success == "Success")
                {
                    playerRender.sprite = punch;
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 5.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => deepOne1SC.HittedByPunchChange())
                    .AppendCallback(() => OnDamageObject("DeepOne1", Random.Range(1, 4) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOne1SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    playerRender.sprite = punch;
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 5.5f, deepOne1Ob.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x + 2f, deepOne1Ob.transform.position.y), 0.5f))
                    .AppendCallback(() => deepOne1SC.DeepOneHybrid_PunchChange())
                    .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 2f, deepOne1Ob.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + Random.Range(1, 4)) * 5))
                    .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                    .AppendCallback(() => CamRotate(-1))
                    .Join(cam.transform.DOShakePosition(1, 2, 90))
                    .Join(deepOne1Ob.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => deepOne1SC.StandChange())
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                    .AppendCallback(() => battlePlayer.resetPlayer())
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
            }
        }
        if (PlayerAction == "SS_attack_Counter")
        {
            if (onPointerEnemy == "DeepOneHybrid1")
            {
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 enemyOrigin = deepOneHybrid1Ob.transform.position;
                GameObject obj = deepOneHybrid1Ob;
                if (Success == "Success")
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid1SC.HittedByNifeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 5) + 2) * 5))//1D4 + 2
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid1SC.HittedByBatChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 9)) * 5))//1D8
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Bat_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid1SC.HittedByAxeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 7) + 2) * 5))//1D8
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Axe_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 3.5f, deepOneHybrid1Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x + 2f, deepOneHybrid1Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid1SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 2f, deepOneHybrid1Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOneHybrid1Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid1SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                   .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 3.5f, deepOneHybrid1Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x + 2f, deepOneHybrid1Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid1SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 2f, deepOneHybrid1Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOneHybrid1Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid1SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                   .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 3.5f, deepOneHybrid1Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x + 2f, deepOneHybrid1Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid1SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 2f, deepOneHybrid1Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOneHybrid1Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid1SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
            else if (onPointerEnemy == "DeepOneHybrid2")
            {
                BattleCameraMove.Instance.ZoomDown();
                Vector3 enemyOrigin = deepOneHybrid2Ob.transform.position;
                GameObject obj = deepOneHybrid2Ob;
                if (Success == "Success")
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid2SC.HittedByNifeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 5) + 2) * 5))//1D4 + 2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid2SC.HittedByBatChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 9)) * 5))//1D8
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Bat_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid2SC.HittedByAxeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 7) + 2) * 5))//1D8
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Axe_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 3.5f, deepOneHybrid2Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x + 2f, deepOneHybrid2Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid2SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 2f, deepOneHybrid2Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + 1) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOneHybrid2Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid2SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => battlePlayer.resetPlayer())
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 3.5f, deepOneHybrid2Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x + 2f, deepOneHybrid2Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid2SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 2f, deepOneHybrid2Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + 1) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOneHybrid2Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid2SC.StandChange())
                   .AppendCallback(() => battlePlayer.resetPlayer())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 3.5f, deepOneHybrid2Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x + 2f, deepOneHybrid2Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid2SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 2f, deepOneHybrid2Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + 1) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOneHybrid2Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid2SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => battlePlayer.resetPlayer())
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                   .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
            else if (onPointerEnemy == "DeepOneHybrid3")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = deepOneHybrid3Ob.transform.position;
                GameObject obj = deepOneHybrid3Ob;
                if (Success == "Success")
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid3SC.HittedByNifeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 5) + 2) * 5))//1D4 + 2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid3SC.HittedByBatChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 9)) * 5))//1D8
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Bat_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid3SC.HittedByAxeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 7) + 2) * 5))//1D8
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Axe_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 3.5f, deepOneHybrid3Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x + 2f, deepOneHybrid3Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid3SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 2f, deepOneHybrid3Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOneHybrid3Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid3SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 3.5f, deepOneHybrid3Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x + 2f, deepOneHybrid3Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid3SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 2f, deepOneHybrid3Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOneHybrid3Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid3SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 3.5f, deepOneHybrid3Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x + 2f, deepOneHybrid3Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid3SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 2f, deepOneHybrid3Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOneHybrid3Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid3SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                   .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
            else if (onPointerEnemy == "DeepOneHybrid4")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = deepOneHybrid4Ob.transform.position;
                GameObject obj = deepOneHybrid4Ob;
                if (Success == "Success")
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);

                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid4SC.HittedByNifeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 5) + 2) * 5))//1D4 + 2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid4SC.HittedByBatChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 9)) * 5))//1D8
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Bat_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid4SC.HittedByAxeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 7) + 2) * 5))//1D8
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Axe_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 3.5f, deepOneHybrid4Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x + 2f, deepOneHybrid4Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid4SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 2f, deepOneHybrid4Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                   .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + 1) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOneHybrid4Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid4SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => battlePlayer.resetPlayer())
                   .AppendCallback(() => CamRotate(0))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                   .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 3.5f, deepOneHybrid4Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x + 2f, deepOneHybrid4Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid4SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 2f, deepOneHybrid4Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                   .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + 1) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOneHybrid4Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid4SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => battlePlayer.resetPlayer())
                   .AppendCallback(() => CamRotate(0))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                   .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 3.5f, deepOneHybrid4Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x + 2f, deepOneHybrid4Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid4SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 2f, deepOneHybrid4Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                   .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + 1) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOneHybrid4Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid4SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => battlePlayer.resetPlayer())
                   .AppendCallback(() => CamRotate(0))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                   .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
            else if (onPointerEnemy == "DeepOne1")
            {
                Vector3 enemyOrigin = deepOne1Ob.transform.position;
                GameObject obj = deepOne1Ob;
                if (Success == "Success")
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOne1SC.HittedByNifeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", (Random.Range(1, 5) + 2) * 5))//1D4 + 2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOne1SC.HittedByBatChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", (Random.Range(1, 9)) * 5))//1D8
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Bat_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOne1SC.HittedByAxeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", (Random.Range(1, 7) + 2) * 5))//1D8
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Axe_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 3.5f, deepOne1Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x + 2f, deepOne1Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOne1SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 2f, deepOne1Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + Random.Range(1, 4)) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOne1Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOne1SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => battlePlayer.resetPlayer())
                   .AppendCallback(() => TurnEnd())
                   .AppendCallback(() => CamRotate(0))
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                   .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 3.5f, deepOne1Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x + 2f, deepOne1Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOne1SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 2f, deepOne1Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + Random.Range(1, 4)) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOne1Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOne1SC.StandChange())
                   .AppendCallback(() => battlePlayer.resetPlayer())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => TurnEnd())
                   .AppendCallback(() => CamRotate(0))
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                   .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 3.5f, deepOne1Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x + 2f, deepOne1Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOne1SC.DeepOneHybrid_PunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 2f, deepOne1Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", (Random.Range(1, 4) + Random.Range(1, 4)) * 5))
                   .AppendCallback(() => SoundManager.Instance.Battle_Sound("Dagger_Attack"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(cam.transform.DOShakePosition(1, 2, 90))
                   .Join(deepOne1Ob.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOne1SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => battlePlayer.resetPlayer())
                   .AppendCallback(() => TurnEnd())
                   .AppendCallback(() => CamRotate(0))
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
        }
        if (PlayerAction == "MS_attack")
        {
            if (onPointerEnemy == "DeepOneHybrid1")
            {
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 enemyOrigin = deepOneHybrid1Ob.transform.position;
                Vector3 playerOrigin = player.transform.position;
                GameObject obj = deepOneHybrid1Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid1SC.HittedBySmallPistolChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 7)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid1SC.HittedByRevolverChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 11) + 2) * 5))//1d10+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid1SC.HittedByRifleChnage())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2D6+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid1SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
            else if (onPointerEnemy == "DeepOneHybrid2")
            {
                BattleCameraMove.Instance.ZoomDown();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid2Ob.transform.position;
                GameObject obj = deepOneHybrid2Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid2SC.HittedBySmallPistolChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 7)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid2SC.HittedByRevolverChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 11) + 2) * 5))//1d10+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid2SC.HittedByRifleChnage())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2D6+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid2SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
            else if (onPointerEnemy == "DeepOneHybrid3")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid3Ob.transform.position;
                GameObject obj = deepOneHybrid3Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedBySmallPistolChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 7)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedByRevolverChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 11) + 2) * 5))//1d10+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedByRifleChnage())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2D6+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
            else if (onPointerEnemy == "DeepOneHybrid4")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid4Ob.transform.position;
                GameObject obj = deepOneHybrid4Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid4SC.HittedBySmallPistolChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 7)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid4SC.HittedByRevolverChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 11) + 2) * 5))//1d10+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid4SC.HittedByRifleChnage())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2D6+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid4SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }

                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
            else if (onPointerEnemy == "DeepOne1")
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOne1Ob.transform.position;
                GameObject obj = deepOne1Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOne1SC.HittedBySmallPistolChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", (Random.Range(1, 7)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOne1SC.HittedByRevolverChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", (Random.Range(1, 11) + 2) * 5))//1d10+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOne1SC.HittedByRifleChnage())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2D6+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOne1SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", (Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Hit"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Revolver_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Shotgun_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
        }
        if (PlayerAction == "Deftness_attack")
        {
            if (onPointerEnemy == "DeepOneHybrid1")
            {
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid1Ob.transform.position;
                GameObject obj = deepOneHybrid1Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid1SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 3)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rock_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid1SC.HittedByMolotovChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2d6+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Molotov_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
            else if (onPointerEnemy == "DeepOneHybrid2")
            {
                BattleCameraMove.Instance.ZoomDown();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid2Ob.transform.position;
                GameObject obj = deepOneHybrid2Ob;

                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid2SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 3)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rock_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid2SC.HittedByMolotovChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2d6+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Molotov_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
            else if (onPointerEnemy == "DeepOneHybrid3")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid3Ob.transform.position;
                GameObject obj = deepOneHybrid3Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 3)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rock_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedByMolotovChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2d6+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Molotov_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
            else if (onPointerEnemy == "DeepOneHybrid4")
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid4Ob.transform.position;
                GameObject obj = deepOneHybrid4Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 3)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rock_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid4SC.HittedByMolotovChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2d6+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Molotov_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
            else if (onPointerEnemy == "DeepOne1")
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOne1Ob.transform.position;
                GameObject obj = deepOne1Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOne1SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", (Random.Range(1, 3)) * 5))//1D6
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Rock_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOne1SC.HittedByMolotovChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", (Random.Range(1, 7) + Random.Range(1, 7) + 2) * 5))//2d6+2
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("Molotov_Attack"))
                        .Join(cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.BattleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.BattleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.Battle_Sound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                }
            }
        }

        if (PlayerAction == "EnemyDeftness")
        {

            BattleCameraMove.Instance.ZoomUp();
            Vector3 playerOrigin = player.transform.position;
            Vector3 enemyOrigin = deepOneHybrid3Ob.transform.position;
            GameObject obj = deepOneHybrid3Ob;
            if (Success == "Result : Success" || Success == "Result : Critical Success")
            {
                deepOneHybrid3SC.DeepOneHybrid_PunchChange();
                cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                // 연출 삽입
                Sequence sequence = DOTween.Sequence()
                .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                .AppendCallback(() => battlePlayer.PlayerHittedRock())
                .AppendCallback(() => SoundManager.Instance.Battle_Sound("MartialArts"))
                .Join(cam.transform.DOShakePosition(1, 2, 90))
                .Join(player.transform.DOShakePosition(1, 2, 90))
                .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                .AppendInterval(0.5f) // 2초 대기
                .AppendCallback(() => deepOneHybrid3SC.StandChange())
                .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                .AppendCallback(() => battlePlayer.resetPlayer())
                .AppendCallback(() => TurnEnd())
                .AppendCallback(() => CamRotate(0))
                .Append(player.transform.DOMove(originPoint, 0.5f))
                .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
            }
            else
            {
                deepOneHybrid3SC.DeepOneHybrid_PunchChange();
                cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                // 연출 삽입
                Sequence sequence = DOTween.Sequence()
                .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                .AppendInterval(0.5f) // 2초 대기
                .AppendCallback(() => deepOneHybrid3SC.StandChange())
                .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                .AppendCallback(() => TurnEnd())
                .AppendCallback(() => CamRotate(0))
                .Append(player.transform.DOMove(originPoint, 0.5f))
                .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
            }
        }
        if (PlayerAction == "DeepOneDeftness")
        {
            Vector3 playerOrigin = player.transform.position;
            Vector3 enemyOrigin = deepOne1Ob.transform.position;
            GameObject obj = deepOne1Ob;
            if (Success == "Result : Success" || Success == "Result : Critical Success")
            {
                deepOne1SC.DeepOne_PoisonChange();
                cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                // 연출 삽입
                Sequence sequence = DOTween.Sequence()
                .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                .AppendCallback(() => DataBaseManager.PosionDebuff = 3)
                .AppendCallback(() => battlePlayer.PlayerHittedPosion())
                .AppendCallback(() => SoundManager.Instance.Battle_Sound("Posion_Attack"))
                .Join(cam.transform.DOShakePosition(1, 2, 90))
                .Join(player.transform.DOShakePosition(1, 2, 90))
                .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                .AppendInterval(0.5f) // 2초 대기
                .AppendCallback(() => deepOne1SC.StandChange())
                .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                .AppendCallback(() => battlePlayer.resetPlayer())
                .AppendCallback(() => TurnEnd())
                .AppendCallback(() => CamRotate(0))
                .Append(player.transform.DOMove(originPoint, 0.5f))
                .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
            }
            else
            {
                deepOne1SC.DeepOneHybrid_PunchChange();
                cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                // 연출 삽입
                Sequence sequence = DOTween.Sequence()
                .AppendCallback(() => SoundManager.Instance.Battle_Sound("Evaision"))
                .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 1f))
                .AppendInterval(0.5f) // 2초 대기
                .AppendCallback(() => deepOne1SC.StandChange())
                .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                .AppendCallback(() => TurnEnd())
                .AppendCallback(() => CamRotate(0))
                .Append(player.transform.DOMove(originPoint, 0.5f))
                .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
            }
        }
    }
    void OnDamageObject(string subject, int damage, string Weapon = "")
    {
        if (subject == "DeepOneHybrid1")
        {
            deepOneHybrid1.nowHP -= damage;
            DamageNumber damageNumber = numberPrefab.Spawn(deepOneHybrid1Ob.transform.position, damage);
        }
        if (subject == "DeepOneHybrid2")
        {
            deepOneHybrid2.nowHP -= damage;
            DamageNumber damageNumber = numberPrefab.Spawn(deepOneHybrid2Ob.transform.position, damage);
        }
        if (subject == "DeepOneHybrid3")
        {
            deepOneHybrid3.nowHP -= damage;
            DamageNumber damageNumber = numberPrefab.Spawn(deepOneHybrid3Ob.transform.position, damage);
        }
        if (subject == "DeepOneHybrid4")
        {
            deepOneHybrid4.nowHP -= damage;
            DamageNumber damageNumber = numberPrefab.Spawn(deepOneHybrid4Ob.transform.position, damage);
        }
        if (subject == "DeepOne1")
        {
            deepOne1.nowHP -= damage;
            DamageNumber damageNumber = numberPrefab.Spawn(deepOne1Ob.transform.position, damage);
        }
        if (subject == "player")
        {
            if (DataBaseManager.Masochism == true)
            {
                BillowUIManager.Instance.HP_Battledown(damage);
            }
            else
            {
                BillowUIManager.Instance.HP_Battledown(damage);
            }
        }
    }
    void OnSpriteChangeComplete(SpriteRenderer subject, Sprite sprite)
    {
        subject.sprite = sprite;
        subject.DOBlendableColor(Color.white, 1);
    }
    void CamRotate(int R)
    {
        cam.transform.DORotate(new Vector3(0, 0, R), 0.5f);
    }
    void EnemyHealthCheck()
    {
        if (deepOneHybrid1.nowHP <= 0 && battleState == "setTrun")
        {
            deepOneHybrid1Ob.SetActive(false);
            enemyTrunSymbol1.SetActive(false);
            DataBaseManager.EnemyDeathCount += 1;
        }
        if (deepOneHybrid2.nowHP <= 0 && battleState == "setTrun")
        {
            deepOneHybrid2Ob.SetActive(false);
            enemyTrunSymbol2.SetActive(false);
            DataBaseManager.EnemyDeathCount += 1;
        }
        if (deepOneHybrid3.nowHP <= 0 && battleState == "setTrun")
        {
            deepOneHybrid3Ob.SetActive(false);
            enemyTrunSymbol3.SetActive(false);
            DataBaseManager.EnemyDeathCount += 1;
        }
        if (deepOneHybrid4.nowHP <= 0 && battleState == "setTrun")
        {
            deepOneHybrid4Ob.SetActive(false);
            enemyTrunSymbol1.SetActive(false);
            DataBaseManager.EnemyDeathCount += 1;
        }
        if (deepOne1.nowHP <= 0 && battleState == "setTrun")
        {
            deepOne1Ob.SetActive(false);
            enemyTrunSymbol2.SetActive(false);
            DataBaseManager.EnemyDeathCount += 1;
        }
    }
    public void CloseRoundObject()
    {
        roundText.text = "";
        roundGameObject.SetActive(false);
    }
    public void OpenRoundObject(string text)
    {
        roundText.text = text;
        roundGameObject.SetActive(true);
    }
    public void TurnEnd()
    {
        DataBaseManager.BattleWeapon = "";
        battleResetUI.EndTrun_Reset();
        onPointerEnemy = "";
        playerAction = "";
        selectEnemy = "";
        battleState = "setTrun";
        fDestroyTime = 0.01f;
        BattleCameraMove.Instance.ResetCam();

        playerChoiceUi.SetActive(false);
        playerActionUi.SetActive(false);
        playerAttackUi.SetActive(false);
        playerSwordsUi.SetActive(false);
        playerMarkmenUi.SetActive(false);
        playerReloadUi.SetActive(false);
        playerReloadCheckUi.SetActive(false);
        playerDeftnessUi.SetActive(false);

        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        deepOneHybrid1Ob.transform.position = new Vector3(deepOneHybrid1Ob.transform.position.x, deepOneHybrid1Ob.transform.position.y, 0);
    }

    public void SetAciton(string action)
    {
        maBackButton.SetActive(true);
        SoundManager.Instance.ClickSound_Play();
        BattleManager.Instance.enemySelectUI.SetActive(true);
        playerActionUi.SetActive(false);
        playerAttackUi.SetActive(false);
        playerMAUI.SetActive(true);
        battleState = "selectEnemy";
        playerAction = action;
    }

    public void Back_MA()
    {
        maBackButton.SetActive(false);
        SoundManager.Instance.ClickSound_Play();
        enemySelectUI.SetActive(false);
        playerAttackUi.SetActive(true);
        playerMAUI.SetActive(false);
        battleState = "PlayerAttack";
    }
    public void SetEnemy(string enemy)
    {
        enemySelectUI.SetActive(false);
        if (enemy == "DeepOneHybrid1")
        {
            if (playerAction == "martialarts")
            {
                maBackButton.SetActive(false);
                playerMAUI.SetActive(false);
                BattleManager.Instance.enemySelectUI.SetActive(false);
                battleState = "Rollet";
                BattleRollet.Instance.setBattleRollet("Fabian : Punching", "martialArtsPoint", DataBaseManager.martialArtsPoint, "MA_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid1.deepOneHybridMatialArts);
            }
            if (playerAction == "PlayerSwords")
            {
                playerSwordsUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Dagger")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Stab", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid1.deepOneHybridMatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Bat")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid1.deepOneHybridMatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Axe")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid1.deepOneHybridMatialArts);
                }
            }
            if (playerAction == "PlayerMarkmen")
            {
                playerMarkmenUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "SmallPistol")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Rifle")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Revolver")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Shotgun")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
            }
            if (playerAction == "PlayerDeftness")
            {
                playerDeftnessUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Rock")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Molotov")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }

            }
        }
        if (enemy == "DeepOneHybrid2")
        {
            if (playerAction == "martialarts")
            {
                battleState = "Rollet";
                BattleRollet.Instance.setBattleRollet("Fabian : Punching", "martialArtsPoint", DataBaseManager.martialArtsPoint, "MA_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid2.deepOneHybridMatialArts);
            }
            if (playerAction == "PlayerSwords")
            {
                playerSwordsUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Dagger")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Stab", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid2.deepOneHybridMatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Bat")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid2.deepOneHybridMatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Axe")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid2.deepOneHybridMatialArts);
                }
            }
            if (playerAction == "PlayerMarkmen")
            {
                playerMarkmenUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "SmallPistol")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Rifle")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Revolver")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Shotgun")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
            }
            if (playerAction == "PlayerDeftness")
            {
                playerDeftnessUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Rock")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Molotov")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }
            }
        }
        if (enemy == "DeepOneHybrid3")
        {
            if (playerAction == "martialarts")
            {
                battleState = "Rollet";
                BattleRollet.Instance.setBattleRollet("Fabian : Punching", "martialArtsPoint", DataBaseManager.martialArtsPoint, "MA_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid3.deepOneHybridMatialArts);
            }
            if (playerAction == "PlayerSwords")
            {
                playerSwordsUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Dagger")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Stab", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid3.deepOneHybridMatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Bat")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid3.deepOneHybridMatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Axe")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid3.deepOneHybridMatialArts);
                }
            }
            if (playerAction == "PlayerMarkmen")
            {
                playerMarkmenUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "SmallPistol")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Rifle")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Revolver")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Shotgun")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
            }
            if (playerAction == "PlayerDeftness")
            {
                playerDeftnessUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Rock")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Molotov")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }

            }
        }
        if (enemy == "DeepOneHybrid4")
        {
            if (playerAction == "martialarts")
            {
                battleState = "Rollet";
                BattleRollet.Instance.setBattleRollet("Fabian : Punching", "martialArtsPoint", DataBaseManager.martialArtsPoint, "MA_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid4.deepOneHybridMatialArts);
            }
            if (playerAction == "PlayerSwords")
            {
                playerSwordsUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Dagger")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Stab", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid4.deepOneHybridMatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Bat")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid4.deepOneHybridMatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Axe")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : CounterAttack", "CounterAttack", deepOneHybrid4.deepOneHybridMatialArts);
                }
            }
            if (playerAction == "PlayerMarkmen")
            {
                playerMarkmenUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "SmallPistol")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Rifle")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Revolver")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Shotgun")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOneHybrid");
                }
            }
            if (playerAction == "PlayerDeftness")
            {
                playerDeftnessUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Rock")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Molotov")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOneHybrid");
                }

            }
        }


        if (enemy == "DeepOne1")
        {
            if (playerAction == "martialarts")
            {
                battleState = "Rollet";
                BattleRollet.Instance.setBattleRollet("Fabian : Punching", "martialArtsPoint", DataBaseManager.martialArtsPoint, "MA_attack_Counter", "DeepOne", "??? : CounterAttack", "CounterAttack", deepOne1.deepOneMatialArts);
            }
            if (playerAction == "PlayerSwords")
            {
                playerSwordsUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Dagger")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Stab", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOne", "??? : CounterAttack", "CounterAttack", deepOne1.deepOneMatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Bat")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOne", "??? : CounterAttack", "CounterAttack", deepOne1.deepOneMatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Axe")
                {
                    battleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOne", "??? : CounterAttack", "CounterAttack", deepOne1.deepOneMatialArts);
                }
            }
            if (playerAction == "PlayerMarkmen")
            {
                playerMarkmenUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "SmallPistol")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOne");
                }
                if (DataBaseManager.BattleWeapon == "Rifle")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOne");
                }
                if (DataBaseManager.BattleWeapon == "Revolver")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOne");
                }
                if (DataBaseManager.BattleWeapon == "Shotgun")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", "DeepOne");
                }
            }
            if (playerAction == "PlayerDeftness")
            {
                playerDeftnessUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Rock")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOne");
                }
                if (DataBaseManager.BattleWeapon == "Molotov")
                {
                    battleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", "DeepOne");
                }
            }
        }
    }
}
