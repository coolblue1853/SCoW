using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using DamageNumbersPro;
using UnityEngine.Serialization;
using SCoW.Core.Rules;
public class BattleManager : MonoBehaviour
{
    public GameObject uiButton;
    public GameObject cam;
    public GameObject roundGameObject;
    [System.NonSerialized] public BattlePhase battleState;
    private int _actingEnemy;
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
    // 적 배열: 인덱스는 EnemyDefs.All 과 같다(0~3 혼혈 1~4, 4 심해인 1). 슬롯 상수는 옛 필드 이름과 1:1.
    private const int SlotHybrid1 = 0;
    private const int SlotHybrid2 = 1;
    private const int SlotHybrid3 = 2;
    private const int SlotHybrid4 = 3;
    private const int SlotDeepOne1 = 4;
    private readonly EnemyState[] _enemies = CreateEnemies();
    private GameObject[] _enemyObs;
    private EnemySpriteChanger[] _enemySCs;
    private GameObject[] _enemySymbols;
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

    private static EnemyState[] CreateEnemies()
    {
        EnemyState[] states = new EnemyState[EnemyDefs.All.Length];
        for (int i = 0; i < states.Length; i++)
        {
            states[i] = new EnemyState(EnemyDefs.All[i]);
        }
        return states;
    }
    private bool IsEnemyAttack(int slot)
    {
        return battleState == BattlePhase.EnemyAttack && _actingEnemy == slot;
    }
    private bool IsPointer(int slot)
    {
        return onPointerEnemy == EnemyDefs.All[slot].Id;
    }
    // 적 인덱스 <-> 턴 게이지 칸(0~2) 매핑은 이 표 한 곳에만 둔다.
    // 지금(1:1 외형 유지)은 장소별 고정표다: 귀가길 혼혈1~3 이 칸0~2, 하수도 혼혈4 가 칸0, 심해인1 이 칸1 을 쓴다(칸이 겹친다).
    // PT-2 에서 BattleField.Enemies[3]/Rank 로 "등장하는 적 -> 칸" 을 매 전투 만들며 이 표를 없앤다.
    private static readonly int[] RoadSymbolEnemy = { SlotHybrid1, SlotHybrid2, SlotHybrid3 };
    private static readonly int[] SewerSymbolEnemy = { SlotHybrid4, SlotDeepOne1, SlotHybrid3 }; // 3칸째는 파티 조우(PT-7)만 켠다
    private static readonly int[] EnemySymbolSlot = { 0, 1, 2, 0, 1 };
    private static int EnemyIndexForSymbol(int symbolSlot, bool inSewer)
    {
        return (inSewer ? SewerSymbolEnemy : RoadSymbolEnemy)[symbolSlot];
    }
    private static int SymbolSlotOf(int enemy)
    {
        return EnemySymbolSlot[enemy];
    }
    private static string KindName(EnemyDef def)
    {
        return def.Kind == EnemyKind.DeepOne ? "DeepOne" : "DeepOneHybrid";
    }
    public void OriginPointChange()
    {
        originPoint = player.transform.position;
    }
    void Update()
    {
        if (runAwayButton.activeSelf == true && DataBaseManager.isDebuffShortTempred == true)
        {
            runAwayButton.SetActive(false);
        }
        else if (runAwayButton.activeSelf == false && DataBaseManager.isDebuffShortTempred == false)
        {
            runAwayButton.SetActive(true);
        }
        if (startBattle == true && (enemyTrunSymbol1.activeSelf == true || enemyTrunSymbol2.activeSelf == true || enemyTrunSymbol3.activeSelf == true))
        {
            if (_partyChecked == false)
            {
                _partyChecked = true;
                if (PartyBattleDirector.TryStart(this))
                {
                    return;
                }
            }
            else if (PartyBattleDirector.Running)
            {
                return;
            }
            if (BattleEngine.IsPlayerDown(DataBaseManager.nowHP, DataBaseManager.nowSan))
            {
                // 사망 화면이 뜬 뒤에는 새 차례를 열지 않는다(사망 오버레이 밑에서 적이 계속 행동하던 문제).
                return;
            }
            EnemyHealthCheck();
            TimeWait();
            StateChecker();
        }
        else
        {
            _partyChecked = false;
        }
        if (DataBaseManager.nowPlace == "BattleRoad")
        {
            if (enemyTrunSymbol1.activeSelf == false && enemyTrunSymbol2.activeSelf == false && enemyTrunSymbol3.activeSelf == false && DataBaseManager.roadBattleEnd == false && DataBaseManager.iSRoadBattleStart == true)
            {
                DataBaseManager.iSRoadBattleStart = false;
                DataBaseManager.isDirecting = true;
                DataBaseManager.roadBattleEnd = true;
                FadingBackGround.Instance.FadeIn();
                Invoke("EndRoadBattle", 1f);
            }
        }
        if (enemyTrunSymbol1.activeSelf == false && enemyTrunSymbol2.activeSelf == false && enemyTrunSymbol3.activeSelf == false && DataBaseManager.sewerBattleChecker == true && DataBaseManager.nowPlace == "InSewer" && DataBaseManager.sewerBattleEndCheck == false)
        {
            DataBaseManager.sewerBattleChecker = false;
            DataBaseManager.sewerBattleEndCheck = true; SewerFlow.OnBattleWon(); // SEWER-1: 이 방의 적 처치 기록
            DataBaseManager.storyDirecting = false;
            FadingBackGround.Instance.FadeInOut();
            Invoke("EndSewerBattle", 1f);
        }
    }
    // ---- PT-5: 다인원 전투 View(PartyBattleDirector)가 칸·적을 가져가는 입구. 솔로(동료 없음)는 쓰지 않는다. ----
    private bool _partyChecked;
    /// <summary>지금 켜져 있는 턴 심볼의 적 상태와 적 인덱스(턴 심볼 칸 순서).</summary>
    public List<EnemyState> PartyEnemies(List<int> indices)
    {
        bool inSewer = DataBaseManager.nowPlace == "InSewer";
        List<EnemyState> list = new List<EnemyState>();
        for (int i = 0; i < _enemySymbols.Length; i++)
        {
            int enemy = EnemyIndexForSymbol(i, inSewer);
            if (enemy >= 0 && _enemySymbols[i].activeSelf == true)
            {
                list.Add(_enemies[enemy]);
                indices.Add(enemy);
            }
        }
        return list;
    }
    public Sprite PlayerSprite()
    {
        return playerRender != null ? playerRender.sprite : null;
    }
    public Sprite EnemySprite(int enemy)
    {
        SpriteRenderer r = enemy >= 0 && enemy < _enemyObs.Length && _enemyObs[enemy] != null ? _enemyObs[enemy].GetComponentInChildren<SpriteRenderer>(true) : null;
        return r != null ? r.sprite : null;
    }
    /// <summary>파티 전투 종료 정산: 죽은 적을 옛 EnemyHealthCheck 와 같이 처리하고, 승리면 옛 종료 흐름(턴 심볼 꺼짐 감지)에 맡기고, 도주는 옛 도주 성공 흐름을 부른다.</summary>
    public void PartyBattleEnd(List<int> enemyIndices, BattleOutcome outcome)
    {
        startBattle = false;
        for (int i = 0; i < enemyIndices.Count; i++)
        {
            int enemy = enemyIndices[i];
            if (_enemies[enemy].IsDead)
            {
                _enemies[enemy].DeathHandled = true;
                _enemyObs[enemy].SetActive(false);
                _enemySymbols[SymbolSlotOf(enemy)].SetActive(false);
                DataBaseManager.enemyDeathCount += 1;
            }
        }
        if (outcome == BattleOutcome.Win)
        {
            for (int i = 0; i < _enemySymbols.Length; i++)
            {
                _enemySymbols[i].SetActive(false);
            }
        }
        else if (outcome == BattleOutcome.Fled)
        {
            if (DataBaseManager.nowPlace != "InSewer")
            {
                ExBattleEnd();
            }
            else
            {
                EndSewerBattle();
            }
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
        battleState = BattlePhase.SetTurn;
        enemyTrunSymbol1.transform.localPosition = new Vector2(enemyTrunSymbol1.transform.localPosition.x, -130);
        enemyTrunSymbol2.transform.localPosition = new Vector2(enemyTrunSymbol1.transform.localPosition.x, -130);
        enemyTrunSymbol3.transform.localPosition = new Vector2(enemyTrunSymbol1.transform.localPosition.x, -130);
        playerTrunSymbol.transform.localPosition = new Vector2(enemyTrunSymbol1.transform.localPosition.x, -130);
    }
    public void EndSewerBattle()
    {
        playerActionUi.SetActive(false);
        uiButton.SetActive(true);
        DataBaseManager.inSewerStealthSucc = true;
        DialogDatabaseManager.instance.check = true;
        DataBaseManager.isDirecting = false;
        DataBaseManager.storyDirecting = false;
        DataBaseManager.isRollet = false;
        DataBaseManager.endBattle = false;
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
    public void ExBattleEnd()
    {
        DataBaseManager.roadBattleEnd = true;
        FadingBackGround.Instance.FadeIn();
        Invoke("EndRoadBattle", 1f);
    }
    void Awake()
    {
        playerRender = player.GetComponent<SpriteRenderer>();
        _enemyObs = new[] { deepOneHybrid1Ob, deepOneHybrid2Ob, deepOneHybrid3Ob, deepOneHybrid4Ob, deepOne1Ob };
        _enemySCs = new[] { deepOneHybrid1SC, deepOneHybrid2SC, deepOneHybrid3SC, deepOneHybrid4SC, deepOne1SC };
        _enemySymbols = new[] { enemyTrunSymbol1, enemyTrunSymbol2, enemyTrunSymbol3 };
        for (int i = 0; i < _enemies.Length; i++)
        {
            RollEnemy(i);
        }
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    /// <summary>적 한 마리 능력치 재굴림(옛 SetDeepOneHybrid1~4/SetDeepOne1 과 같은 난수 순서).</summary>
    public void RollEnemy(int slot)
    {
        _enemies[slot].Roll(UnityRng.Instance);
    }
    public void SetDeepOneHybrid1()
    {
        RollEnemy(SlotHybrid1);
    }
    public void SetDeepOneHybrid2()
    {
        RollEnemy(SlotHybrid2);
    }
    public void SetDeepOneHybrid3()
    {
        RollEnemy(SlotHybrid3);
    }
    public void SetDeepOneHybrid4()
    {
        RollEnemy(SlotHybrid4);
    }
    public void SetDeepOne1()
    {
        RollEnemy(SlotDeepOne1);
    }
    public void TimeWait()
    {
        if (battleState == BattlePhase.SetTurn)
        {
            fTickTime += Time.deltaTime;
            if (fTickTime >= fDestroyTime)
            {
                fTickTime = 0;
                fDestroyTime = fDestroyTime * 0.98f;
                if (playerTrunSymbol.activeSelf == true)
                {
                    SymbolUp(playerTrunSymbol, DataBaseManager.dex, true, -1);
                }
                bool inSewer = DataBaseManager.nowPlace == "InSewer";
                for (int i = 0; i < _enemySymbols.Length; i++)
                {
                    int enemy = EnemyIndexForSymbol(i, inSewer);
                    if (enemy >= 0 && _enemySymbols[i].activeSelf == true)
                    {
                        SymbolUp(_enemySymbols[i], _enemies[enemy].Dex, false, enemy);
                    }
                }
            }
        }
    }

    // 턴 최소지점 -130, 최대지점 +130
    // Start is called before the first frame update
    void Start()
    {
        battleState = BattlePhase.SetTurn;
    }
    public void SymbolUp(GameObject symbol, float dex, bool isPlayer, int enemyIndex)
    {
        if (battleState == BattlePhase.SetTurn)
        {
            if (!BattleEngine.IsGaugeFull(symbol.transform.localPosition.y))
            {
                symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, BattleEngine.StepGauge(symbol.transform.localPosition.y, dex, isPlayer));
            }
            else
            {
                SoundManager.Instance.SetTrunSoundPlay();
                symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, BattleEngine.GaugeReset);
                battleState = isPlayer ? BattlePhase.PlayerTurn : BattlePhase.EnemyTurn;
                _actingEnemy = enemyIndex;

                if (isPlayer)
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
        if (battleState == BattlePhase.EnemyTurn)
        {
            battleState = BattlePhase.EnemyAttack;
            Invoke("EnemySetAction", 1);
        }
        if (battleState == BattlePhase.PlayerTurn)
        {
            if (DataBaseManager.posionDebuff > 0)
            {
                DataBaseManager.posionDebuff -= 1;
                OnDamageObject("player", BattleDamage.Poison.Roll(UnityRng.Instance));
            }
            BattleItemManager.Instance.ManageItem();
            battleState = BattlePhase.PlayerChoice;
            Invoke("PlayerSetAction", 1);
        }
    }
    void PlayerSetAction()
    {
        roundGameObject.SetActive(false);
        playerActionUi.SetActive(true);
    }
    public void PlayerBackAction()
    {
        SoundManager.Instance.ClickSoundPlay();
        enemySelectUI.SetActive(false);
        playerAttackUi.SetActive(false);
        battleState = BattlePhase.PlayerChoice;
        playerActionUi.SetActive(true);
    }
    public void PlayerSetAttack()
    {
        SoundManager.Instance.ClickSoundPlay();
        playerActionUi.SetActive(false);
        battleState = BattlePhase.PlayerAttack;
        playerAttackUi.SetActive(true);
    }
    public void PlayerSetSwords()
    {
        SoundManager.Instance.ClickSoundPlay();
        playerAttackUi.SetActive(false);
        playerAction = "PlayerSwords";
        playerSwordsUi.SetActive(true);
    }
    public void PlayerBackSwords()
    {
        SoundManager.Instance.ClickSoundPlay();
        enemySelectUI.SetActive(false);
        playerSwordsUi.SetActive(false);
        playerAction = "PlayerAttack";
        playerAttackUi.SetActive(true);
        playerRender.sprite = stand;
    }
    public void PlayerSetMarkmenship()
    {
        SoundManager.Instance.ClickSoundPlay();
        playerAttackUi.SetActive(false);
        playerAction = "PlayerMarkmen";
        playerMarkmenUi.SetActive(true);
    }
    public void PlayerBackMarkmenship()
    {
        SoundManager.Instance.ClickSoundPlay();
        enemySelectUI.SetActive(false);
        playerAttackUi.SetActive(true);
        playerAction = "PlayerAttack";
        playerMarkmenUi.SetActive(false);
        playerRender.sprite = stand;
    }
    public void PlayerSetDeftness()
    {
        SoundManager.Instance.ClickSoundPlay();
        playerAttackUi.SetActive(false);
        playerAction = "PlayerDeftness";
        playerDeftnessUi.SetActive(true);
    }
    public void PlayerBackDeftness()
    {
        SoundManager.Instance.ClickSoundPlay();
        enemySelectUI.SetActive(false);
        playerAttackUi.SetActive(true);
        playerAction = "PlayerAttack";
        playerDeftnessUi.SetActive(false);
        playerRender.sprite = stand;
    }
    public void PlayerSetReload()
    {
        SoundManager.Instance.ClickSoundPlay();
        playerActionUi.SetActive(false);
        playerAction = "PlayerReload";
        playerReloadUi.SetActive(true);
        playerReloadItemUI.SetActive(true);
    }
    public void PlayerBackReload()
    {
        SoundManager.Instance.ClickSoundPlay();
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
    public void StartReload()
    {
        while (DataBaseManager.pistolAmmo > 0)
        {
            if (DataBaseManager.nowSmallPistol == 2)
            {
                break;
            }
            DataBaseManager.nowSmallPistol += 1;
            DataBaseManager.pistolAmmo -= 1;
        }
        while (DataBaseManager.pistolAmmo > 0)
        {
            if (DataBaseManager.nowRevolver == 6)
            {
                break;
            }
            DataBaseManager.nowRevolver += 1;
            DataBaseManager.pistolAmmo -= 1;
        }
        while (DataBaseManager.rifleAmmo > 0)
        {
            if (DataBaseManager.nowRifle == 5)
            {
                break;
            }
            DataBaseManager.nowRifle += 1;
            DataBaseManager.rifleAmmo -= 1;
        }
        while (DataBaseManager.shotgunAmmo > 0)
        {
            if (DataBaseManager.nowShotgun == 2)
            {
                break;
            }
            DataBaseManager.nowShotgun += 1;
            DataBaseManager.shotgunAmmo -= 1;
        }
    }

    public void ReloadOk()
    {
        playerReloadCheckUi.SetActive(false);
        BattleCameraMove.Instance.ZoomPlayer();
        if (nowReloadWeapon == "SmallPistol")
        {
            SoundManager.Instance.BattleSound("SmallPisol_Reload");
            while (DataBaseManager.pistolAmmo > 0)
            {
                if (DataBaseManager.nowSmallPistol == 2)
                {
                    break;
                }
                DataBaseManager.nowSmallPistol += 1;
                DataBaseManager.pistolAmmo -= 1;
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
            SoundManager.Instance.BattleSound("Revolver_Reload");
            while (DataBaseManager.pistolAmmo > 0)
            {
                if (DataBaseManager.nowRevolver == 6)
                {
                    break;
                }
                DataBaseManager.nowRevolver += 1;
                DataBaseManager.pistolAmmo -= 1;
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
            SoundManager.Instance.BattleSound("Rifle_Reload");
            while (DataBaseManager.rifleAmmo > 0)
            {
                if (DataBaseManager.nowRifle == 5)
                {
                    break;
                }
                DataBaseManager.nowRifle += 1;
                DataBaseManager.rifleAmmo -= 1;
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
            SoundManager.Instance.BattleSound("Shotgun_Reload");
            while (DataBaseManager.shotgunAmmo > 0)
            {
                if (DataBaseManager.nowShotgun == 2)
                {
                    break;
                }
                DataBaseManager.nowShotgun += 1;
                DataBaseManager.shotgunAmmo -= 1;
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
        SoundManager.Instance.ClickSoundPlay();
        enemyCount = 0;
        runAwayObject.SetActive(true);
        for (int i = 0; i < _enemySymbols.Length; i++)
        {
            if (_enemySymbols[i].activeSelf == true)
            {
                enemyCount += 1;
            }
        }
        runAwayText.text = "Attempt to Runaway. You are penalized by the number of enemies.\n\nDex(" + DataBaseManager.dex + ") / " + enemyCount + " = " + DataBaseManager.dex / enemyCount;
    }
    public void RunYes()
    {
        SoundManager.Instance.ClickSoundPlay();
        BattleRollet.Instance.SetBattleRollet("Fabian : Runaway", "DEX / " + enemyCount, DataBaseManager.dex / enemyCount, "RunAway", "DeepOneHybrid", "??? : Hold", "Dex", _enemies[SlotHybrid1].Dex);
        runAwayObject.SetActive(false);
    }
    public void RunNo()
    {
        SoundManager.Instance.ClickSoundPlay();
        runAwayObject.SetActive(false);
    }
    void EnemySetAction()
    {
        // 행동 종류는 슬롯 Def 의 IntentWeights/UsesDeftness 데이터가 정한다(BattleEngine.ChooseIntent).
        roundGameObject.SetActive(false);
        EnemyDef def = _enemies[_actingEnemy].Def;
        EnemyIntent intent = BattleEngine.ChooseIntent(def, UnityRng.Instance);
        if (intent == EnemyIntent.Deftness)
        {
            OpenRoundObject("An enemy tries to attack you with a Deftness.");
            Invoke("EnemyAttackWithDeftness", 1.5f);
        }
        else if (intent == EnemyIntent.SanAttack) // 이성 공격
        {
            OpenRoundObject("An enemy tries to initiate suspicious behavior.");
            Invoke("DeepOneSanAttack", 1.5f);
        }
        else if (intent == EnemyIntent.PoisonAttack) // 독 도트 공격
        {
            OpenRoundObject("An enemy tries to initiate suspicious behavior.");
            Invoke("DeepOnePosionAttack", 1.5f);
        }
        else if (def.Kind == EnemyKind.DeepOne) // 일반 공격(심해인)
        {
            OpenRoundObject("An enemy tries to attack you with a Swordsmanship.");
            playerChoiceUi.SetActive(true);
        }
        else // 일반 공격(혼혈)
        {
            OpenRoundObject("An enemy tries to attack you with a MartialArt.");
            playerChoiceUi.SetActive(true);
        }
    }
    public void DeepOneSanAttack()
    {
        if (_actingEnemy < 0) { return; }
        _enemySCs[_actingEnemy].DeepOneSanAttackChange();
        cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
        // 연출 삽입
        Sequence sequence = DOTween.Sequence()
        .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
        .AppendCallback(() => battlePlayer.PlayerHittedSanAttack())
        .AppendCallback(() => SoundManager.Instance.BattleSound("San_Attack"))
        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
        .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
        .AppendInterval(0.5f) // 2초 대기
        .AppendCallback(() => Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "BattleSan_DeepOne1"));
    }
    public void EndDeepOneSanAttack()
    {
        Sequence sequence = DOTween.Sequence()
        .AppendCallback(() => { if (_actingEnemy >= 0) { _enemySCs[_actingEnemy].StandChange(); } })
        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
        .AppendCallback(() => battlePlayer.resetPlayer())
        .AppendCallback(() => TurnEnd())
        .AppendCallback(() => CamRotate(0));

    }
    public void DeepOnePosionAttack()
    {
        if (_actingEnemy < 0) { return; }
        Rollet.Instance.SetRollet("??? : Deftness", "Deftness", _enemies[_actingEnemy].Dex, "DeepOneDeftness", "DeepOne");
    }
    public void EnemyAttackWithDeftness()
    {
        if (_actingEnemy < 0) { return; }
        Rollet.Instance.SetRollet("??? : Deftness", "Deftness", _enemies[_actingEnemy].Dex, "EnemyDeftness", "DeepOneHybrid");
    }
    public void ChoiceEvasion()
    {
        playerAction = "evasion";
        if (battleState == BattlePhase.EnemyAttack)
        {
            playerChoiceUi.SetActive(false);
            CloseRoundObject();
            EnemyState e = _enemies[_actingEnemy];
            if (e.Def.Kind == EnemyKind.DeepOne)
            {
                BattleRollet.Instance.SetBattleRollet("Fabian : Evasion", "Evasion", DataBaseManager.evasionPoint, "evasion", KindName(e.Def), "??? : Poking", "SwordsManship", e.Swordsmanship);
            }
            else
            {
                BattleRollet.Instance.SetBattleRollet("Fabian : Evasion", "Evasion", DataBaseManager.evasionPoint, "evasion", KindName(e.Def), "??? : MartialArt", "MartialArt", e.MartialArts);
            }
        }
    }
    public void ChoiceCounterAttack()
    {
        playerAction = "counterattack";
        if (battleState == BattlePhase.EnemyAttack)
        {
            playerChoiceUi.SetActive(false);
            CloseRoundObject();
            EnemyState e = _enemies[_actingEnemy];
            if (e.Def.Kind == EnemyKind.DeepOne)
            {
                BattleRollet.Instance.SetBattleRollet("Fabian : CounterAttack", "MartialArt", DataBaseManager.martialArtsPoint, "counterattack", KindName(e.Def), "??? : Poking", "SwordsManship", e.Swordsmanship);
            }
            else
            {
                BattleRollet.Instance.SetBattleRollet("Fabian : CounterAttack", "MartialArt", DataBaseManager.martialArtsPoint, "counterattack", KindName(e.Def), "??? : MartialArt", "MartialArt", e.MartialArts);
            }
        }
    }
    private static void ApplyZoom(CamZoom zoom)
    {
        switch (zoom)
        {
            case CamZoom.Up: BattleCameraMove.Instance.ZoomUp(); break;
            case CamZoom.Midle: BattleCameraMove.Instance.ZoomMidle(); break;
            case CamZoom.Down: BattleCameraMove.Instance.ZoomDown(); break;
        }
    }
    // P14b F1: evasion 5슬롯 x 성공/실패 복제 블록을 통합. 체인 순서·값은 출시판 그대로이고 슬롯 간 차이는 EnemyFxProfiles 값으로 재현한다.
    private void PlayEvade(int slot, bool success)
    {
        EnemyFxProfile fx = EnemyFxProfiles.All[slot];
        GameObject obj = _enemyObs[slot];
        EnemySpriteChanger sc = _enemySCs[slot];
        Vector3 playerOrigin = player.transform.position;
        Vector3 enemyOrigin = obj.transform.position;
        if (success || fx.ZoomInEvadeFail)
        {
            ApplyZoom(fx.Zoom);
        }
        sc.DeepOneHybridPunchChange();
        cam.transform.DORotate(new Vector3(0, 0, -fx.RotateDeg), 0.5f);
        Sequence sequence = DOTween.Sequence()
        .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f));
        if (success)
        {
            // 옛 동작 보존: H1 만 이동 y 가 적 오브젝트 y 이고 z 는 0(나머지는 playerOrigin.y, z -1)
            Vector3 dodgeTo = fx.EvadeYFromEnemy
                ? new Vector3(player.transform.position.x - 4f, enemyOrigin.y)
                : new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1);
            sequence
            .AppendCallback(() => SoundManager.Instance.BattleSound("Evaision"))
            .Append(player.transform.DOMove(dodgeTo, fx.EvadeMoveSec))
            .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1.5f, player.transform.position.y - 0.2f, -1), 0.7f))
            .AppendInterval(0.5f); // 2초 대기
        }
        else
        {
            AppendPlayerHit(sequence, slot, obj, fx.EvadeFailHit);
        }
        sequence
        .AppendCallback(() => sc.StandChange())
        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand));
        if (!success && fx.EvadeFailHit.ResetPlayer)
        {
            sequence.AppendCallback(() => battlePlayer.resetPlayer());
        }
        sequence
        .AppendCallback(() => TurnEnd())
        .AppendCallback(() => CamRotate(0))
        .Append(player.transform.DOMove(originPoint, 0.5f))
        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
    }
    // evasion·counterattack 실패(플레이어가 맞음) 공통 체인. 슬롯별 피격 스타일은 PlayerHitReaction 으로 재현한다.
    private void AppendPlayerHit(Sequence sequence, int slot, GameObject obj, PlayerHitReaction hit)
    {
        // 옛 동작 보존: hitted 스프라이트가 없는 슬롯(H4)은 PlayerHittedDagger 가 피해 앞, 있는 슬롯은 피해 뒤
        bool daggerFirst = hit.PlayerHit == PlayerHitStyle.Dagger && !hit.HittedSprite;
        if (daggerFirst)
        {
            sequence.AppendCallback(() => battlePlayer.PlayerHittedDagger());
        }
        if (hit.HittedSprite)
        {
            sequence.AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted));
        }
        sequence.AppendCallback(() => OnDamageObject("player", _enemies[slot].Def.AttackDamage.Roll(UnityRng.Instance)));
        if (hit.PlayerHit == PlayerHitStyle.Dagger && !daggerFirst)
        {
            sequence.AppendCallback(() => battlePlayer.PlayerHittedDagger());
        }
        sequence
        .AppendCallback(() => SoundManager.Instance.BattleSound(hit.HitSound))
        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
        .Join(ShakeFx.Position(player.transform, 1, 2, 90))
        .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
        .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
        .AppendInterval(0.5f); // 2초 대기
    }
    // P14b F2: counterattack 5슬롯 x 성공/실패 복제 블록을 통합. 줌은 성공·실패 모두 슬롯 분기 첫 줄(evasion 과 달리 항상).
    private void PlayCounter(int slot, bool success)
    {
        EnemyFxProfile fx = EnemyFxProfiles.All[slot];
        GameObject obj = _enemyObs[slot];
        EnemySpriteChanger sc = _enemySCs[slot];
        Vector3 playerOrigin = player.transform.position;
        ApplyZoom(fx.Zoom);
        Vector3 enemyOrigin = obj.transform.position;
        sc.DeepOneHybridPunchChange();
        cam.transform.DORotate(new Vector3(0, 0, -fx.RotateDeg), 0.5f);
        Sequence sequence = DOTween.Sequence()
        .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f));
        if (success)
        {
            string enemyId = _enemies[slot].Def.Id;
            int rotateDeg = (int)fx.RotateDeg;
            sequence
            .AppendCallback(() => SoundManager.Instance.BattleSound("Evaision"))
            .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 4f, playerOrigin.y, -1), 0.5f))
            .AppendCallback(() => OnSpriteChangeComplete(playerRender, punch))
            .Append(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 0.2f))
            .AppendCallback(() => sc.HittedByPunchChange())
            .AppendCallback(() => CamRotate(+rotateDeg))
            .AppendCallback(() => OnDamageObject(enemyId, BattleDamage.Hit1D3.Roll(UnityRng.Instance)))
            .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
            .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
            .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
            .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 8f, player.transform.position.y), 1.5f))
            .Join(player.transform.DOMove(new Vector3(player.transform.position.x + 2, player.transform.position.y, -1), 1f))
            .AppendInterval(0.5f); // 2초 대기
        }
        else
        {
            AppendPlayerHit(sequence, slot, obj, fx.EvadeFailHit);
        }
        sequence
        .AppendCallback(() => sc.StandChange())
        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand));
        if (!success && fx.EvadeFailHit.ResetPlayer)
        {
            sequence.AppendCallback(() => battlePlayer.resetPlayer());
        }
        sequence
        .AppendCallback(() => TurnEnd())
        .AppendCallback(() => CamRotate(0))
        .Append(player.transform.DOMove(originPoint, 0.5f))
        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
    }
    public void RetrunRolletResult(string PlayerAction, string Success, string Enemy)
    {
        if (PlayerAction == "evasion")
        {
            for (int slot = 0; slot < _enemies.Length && slot < EnemyFxProfiles.All.Length; slot++)
            {
                if (IsEnemyAttack(slot))
                {
                    PlayEvade(slot, Success == "Success");
                    break;
                }
            }
        }
        if (PlayerAction == "counterattack")
        {
            for (int slot = 0; slot < _enemies.Length && slot < EnemyFxProfiles.All.Length; slot++)
            {
                if (IsEnemyAttack(slot))
                {
                    PlayCounter(slot, Success == "Success");
                    break;
                }
            }
        }
        if (PlayerAction == "MA_attack_Counter")
        {
            if (IsPointer(SlotHybrid1))
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
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid1", BattleDamage.Hit1D3.Roll(UnityRng.Instance)))
                    .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                    .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                    .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    .AppendCallback(() => SoundManager.Instance.BattleSound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x + 2f, deepOneHybrid1Ob.transform.position.y), 0.5f))
                    .AppendCallback(() => deepOneHybrid1SC.DeepOneHybridPunchChange())
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 2f, deepOneHybrid1Ob.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid1].Def.AttackDamage.Roll(UnityRng.Instance)))
                    .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                    .AppendCallback(() => CamRotate(-5))
                    .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                    .Join(ShakeFx.Position(deepOneHybrid1Ob.transform, 1, 2, 90))
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
            else if (IsPointer(SlotHybrid2))
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
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid2", BattleDamage.Hit1D3.Roll(UnityRng.Instance)))
                    .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                    .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                    .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    .AppendCallback(() => SoundManager.Instance.BattleSound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x + 2f, deepOneHybrid2Ob.transform.position.y), 0.5f))
                    .AppendCallback(() => deepOneHybrid2SC.DeepOneHybridPunchChange())
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 2f, deepOneHybrid2Ob.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid2].Def.AttackDamage.Roll(UnityRng.Instance)))
                    .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                    .AppendCallback(() => CamRotate(-5))
                    .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                    .Join(ShakeFx.Position(deepOneHybrid2Ob.transform, 1, 2, 90))
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
            else if (IsPointer(SlotHybrid3))
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
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid3", BattleDamage.Hit1D3.Roll(UnityRng.Instance)))
                    .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                    .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                    .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    .AppendCallback(() => SoundManager.Instance.BattleSound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x + 2f, deepOneHybrid3Ob.transform.position.y), 0.5f))
                    .AppendCallback(() => deepOneHybrid3SC.DeepOneHybridPunchChange())
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 2f, deepOneHybrid3Ob.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid3].Def.AttackDamage.Roll(UnityRng.Instance)))
                    .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                    .AppendCallback(() => CamRotate(-5))
                    .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                    .Join(ShakeFx.Position(deepOneHybrid3Ob.transform, 1, 2, 90))
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
            else if (IsPointer(SlotHybrid4))
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
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid4", BattleDamage.Hit1D3.Roll(UnityRng.Instance)))
                    .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                    .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                    .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    .AppendCallback(() => SoundManager.Instance.BattleSound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x + 2f, deepOneHybrid4Ob.transform.position.y), 0.5f))
                    .AppendCallback(() => deepOneHybrid4SC.DeepOneHybridPunchChange())
                    .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 2f, deepOneHybrid4Ob.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                    .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid4].Def.AttackDamage.Roll(UnityRng.Instance)))
                    .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                    .AppendCallback(() => CamRotate(-1))
                    .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                    .Join(ShakeFx.Position(deepOneHybrid4Ob.transform, 1, 2, 90))
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
            else if (IsPointer(SlotDeepOne1))
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
                    .AppendCallback(() => OnDamageObject("DeepOne1", BattleDamage.Hit1D3.Roll(UnityRng.Instance)))
                    .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                    .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                    .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    .AppendCallback(() => SoundManager.Instance.BattleSound("Evaision"))
                    .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x + 2f, deepOne1Ob.transform.position.y), 0.5f))
                    .AppendCallback(() => deepOne1SC.DeepOneHybridPunchChange())
                    .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 2f, deepOne1Ob.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                    .AppendCallback(() => OnDamageObject("player", _enemies[SlotDeepOne1].Def.AttackDamage.Roll(UnityRng.Instance)))
                    .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                    .AppendCallback(() => CamRotate(-1))
                    .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                    .Join(ShakeFx.Position(deepOne1Ob.transform, 1, 2, 90))
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
            if (IsPointer(SlotHybrid1))
            {
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 enemyOrigin = deepOneHybrid1Ob.transform.position;
                GameObject obj = deepOneHybrid1Ob;
                if (Success == "Success")
                {
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid1SC.HittedByNifeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", BattleDamage.Dagger.Roll(UnityRng.Instance)))//1D4 + 2
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid1SC.HittedByBatChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", BattleDamage.Bat.Roll(UnityRng.Instance)))//1D8
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Bat_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid1SC.HittedByAxeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", BattleDamage.Axe.Roll(UnityRng.Instance)))//1D8
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Axe_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 3.5f, deepOneHybrid1Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x + 2f, deepOneHybrid1Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid1SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 2f, deepOneHybrid1Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid1].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOneHybrid1Ob.transform, 1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid1SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                   .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 3.5f, deepOneHybrid1Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x + 2f, deepOneHybrid1Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid1SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 2f, deepOneHybrid1Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid1].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOneHybrid1Ob.transform, 1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid1SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                   .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 3.5f, deepOneHybrid1Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x + 2f, deepOneHybrid1Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid1SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid1Ob.transform.position.x - 2f, deepOneHybrid1Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid1].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOneHybrid1Ob.transform, 1, 2, 90))
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
            else if (IsPointer(SlotHybrid2))
            {
                BattleCameraMove.Instance.ZoomDown();
                Vector3 enemyOrigin = deepOneHybrid2Ob.transform.position;
                GameObject obj = deepOneHybrid2Ob;
                if (Success == "Success")
                {
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid2SC.HittedByNifeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", BattleDamage.Dagger.Roll(UnityRng.Instance)))//1D4 + 2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid2SC.HittedByBatChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", BattleDamage.Bat.Roll(UnityRng.Instance)))//1D8
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Bat_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid2SC.HittedByAxeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", BattleDamage.Axe.Roll(UnityRng.Instance)))//1D8
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Axe_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 3.5f, deepOneHybrid2Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x + 2f, deepOneHybrid2Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid2SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 2f, deepOneHybrid2Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid2].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOneHybrid2Ob.transform, 1, 2, 90))
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
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 3.5f, deepOneHybrid2Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x + 2f, deepOneHybrid2Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid2SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 2f, deepOneHybrid2Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid2].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOneHybrid2Ob.transform, 1, 2, 90))
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
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 3.5f, deepOneHybrid2Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x + 2f, deepOneHybrid2Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid2SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid2Ob.transform.position.x - 2f, deepOneHybrid2Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid2].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOneHybrid2Ob.transform, 1, 2, 90))
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
            else if (IsPointer(SlotHybrid3))
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = deepOneHybrid3Ob.transform.position;
                GameObject obj = deepOneHybrid3Ob;
                if (Success == "Success")
                {
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid3SC.HittedByNifeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", BattleDamage.Dagger.Roll(UnityRng.Instance)))//1D4 + 2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid3SC.HittedByBatChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", BattleDamage.Bat.Roll(UnityRng.Instance)))//1D8
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Bat_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid3SC.HittedByAxeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", BattleDamage.Axe.Roll(UnityRng.Instance)))//1D8
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Axe_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 3.5f, deepOneHybrid3Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x + 2f, deepOneHybrid3Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid3SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 2f, deepOneHybrid3Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid3].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOneHybrid3Ob.transform, 1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid3SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 3.5f, deepOneHybrid3Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x + 2f, deepOneHybrid3Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid3SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 2f, deepOneHybrid3Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid3].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOneHybrid3Ob.transform, 1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))
                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => deepOneHybrid3SC.StandChange())
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                   .AppendCallback(() => TurnEnd())
                   .Append(player.transform.DOMove(originPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 3.5f, deepOneHybrid3Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x + 2f, deepOneHybrid3Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid3SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid3Ob.transform.position.x - 2f, deepOneHybrid3Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid3].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOneHybrid3Ob.transform, 1, 2, 90))
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
            else if (IsPointer(SlotHybrid4))
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 enemyOrigin = deepOneHybrid4Ob.transform.position;
                GameObject obj = deepOneHybrid4Ob;
                if (Success == "Success")
                {
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);

                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid4SC.HittedByNifeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", BattleDamage.Dagger.Roll(UnityRng.Instance)))//1D4 + 2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid4SC.HittedByBatChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", BattleDamage.Bat.Roll(UnityRng.Instance)))//1D8
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Bat_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOneHybrid4SC.HittedByAxeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", BattleDamage.Axe.Roll(UnityRng.Instance)))//1D8
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Axe_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 3.5f, deepOneHybrid4Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x + 2f, deepOneHybrid4Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid4SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 2f, deepOneHybrid4Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid4].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOneHybrid4Ob.transform, 1, 2, 90))
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
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 3.5f, deepOneHybrid4Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x + 2f, deepOneHybrid4Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid4SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 2f, deepOneHybrid4Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid4].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOneHybrid4Ob.transform, 1, 2, 90))
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
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 3.5f, deepOneHybrid4Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x + 2f, deepOneHybrid4Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOneHybrid4SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOneHybrid4Ob.transform.position.x - 2f, deepOneHybrid4Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => battlePlayer.PlayerHittedDagger())
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid4].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOneHybrid4Ob.transform, 1, 2, 90))
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
            else if (IsPointer(SlotDeepOne1))
            {
                Vector3 enemyOrigin = deepOne1Ob.transform.position;
                GameObject obj = deepOne1Ob;
                if (Success == "Success")
                {
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOne1SC.HittedByNifeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", BattleDamage.Dagger.Roll(UnityRng.Instance)))//1D4 + 2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOne1SC.HittedByBatChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", BattleDamage.Bat.Roll(UnityRng.Instance)))//1D8
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Bat_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                        .AppendCallback(() => deepOne1SC.HittedByAxeChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", BattleDamage.Axe.Roll(UnityRng.Instance)))//1D8
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Axe_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        playerRender.sprite = daggerReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        playerRender.sprite = batReady;
                    }
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        playerRender.sprite = axeReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.battleWeapon == "Dagger")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 3.5f, deepOne1Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, daggerAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x + 2f, deepOne1Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOne1SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 2f, deepOne1Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotDeepOne1].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOne1Ob.transform, 1, 2, 90))
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
                    else if (DataBaseManager.battleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 3.5f, deepOne1Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, batAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x + 2f, deepOne1Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOne1SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 2f, deepOne1Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotDeepOne1].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOne1Ob.transform, 1, 2, 90))
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
                    else if (DataBaseManager.battleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 3.5f, deepOne1Ob.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, axeAttack))
                   .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x + 2f, deepOne1Ob.transform.position.y), 0.5f))
                   .AppendCallback(() => deepOne1SC.DeepOneHybridPunchChange())
                   .Append(obj.transform.DOMove(new Vector3(deepOne1Ob.transform.position.x - 2f, deepOne1Ob.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                   .AppendCallback(() => OnDamageObject("player", _enemies[SlotDeepOne1].Def.AttackDamage.Roll(UnityRng.Instance)))
                   .AppendCallback(() => SoundManager.Instance.BattleSound("Dagger_Attack"))
                   .AppendCallback(() => CamRotate(-1))
                   .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                   .Join(ShakeFx.Position(deepOne1Ob.transform, 1, 2, 90))
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
            if (IsPointer(SlotHybrid1))
            {
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 enemyOrigin = deepOneHybrid1Ob.transform.position;
                Vector3 playerOrigin = player.transform.position;
                GameObject obj = deepOneHybrid1Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.battleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid1SC.HittedBySmallPistolChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", BattleDamage.SmallPistol.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid1SC.HittedByRevolverChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", BattleDamage.Revolver.Roll(UnityRng.Instance)))//1d10+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid1SC.HittedByRifleChnage())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", BattleDamage.Rifle.Roll(UnityRng.Instance)))//2D6+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid1SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", BattleDamage.Shotgun.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Attack"))
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
            else if (IsPointer(SlotHybrid2))
            {
                BattleCameraMove.Instance.ZoomDown();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid2Ob.transform.position;
                GameObject obj = deepOneHybrid2Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.battleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid2SC.HittedBySmallPistolChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", BattleDamage.SmallPistol.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Attack"))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid2SC.HittedByRevolverChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", BattleDamage.Revolver.Roll(UnityRng.Instance)))//1d10+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid2SC.HittedByRifleChnage())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", BattleDamage.Rifle.Roll(UnityRng.Instance)))//2D6+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid2SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", BattleDamage.Shotgun.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Attack"))
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
            else if (IsPointer(SlotHybrid3))
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid3Ob.transform.position;
                GameObject obj = deepOneHybrid3Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.battleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedBySmallPistolChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", BattleDamage.SmallPistol.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Attack"))//1D6
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedByRevolverChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", BattleDamage.Revolver.Roll(UnityRng.Instance)))//1d10+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedByRifleChnage())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", BattleDamage.Rifle.Roll(UnityRng.Instance)))//2D6+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", BattleDamage.Shotgun.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Attack"))
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
            else if (IsPointer(SlotHybrid4))
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid4Ob.transform.position;
                GameObject obj = deepOneHybrid4Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.battleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.battleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid4SC.HittedBySmallPistolChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", BattleDamage.SmallPistol.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Attack"))//1D6
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid4SC.HittedByRevolverChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", BattleDamage.Revolver.Roll(UnityRng.Instance)))//1d10+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid4SC.HittedByRifleChnage())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", BattleDamage.Rifle.Roll(UnityRng.Instance)))//2D6+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid4SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", BattleDamage.Shotgun.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }

                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.battleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Attack"))
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
            else if (IsPointer(SlotDeepOne1))
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOne1Ob.transform.position;
                GameObject obj = deepOne1Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.battleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.battleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOne1SC.HittedBySmallPistolChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", BattleDamage.SmallPistol.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Attack"))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOne1SC.HittedByRevolverChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", BattleDamage.Revolver.Roll(UnityRng.Instance)))//1d10+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOne1SC.HittedByRifleChnage())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", BattleDamage.Rifle.Roll(UnityRng.Instance)))//2D6+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOne1SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", BattleDamage.Shotgun.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Attack"))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Hit"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "SmallPistoll")
                    {
                        playerRender.sprite = smallPistolReady;
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        playerRender.sprite = revolverReady;
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        playerRender.sprite = rifleReady;
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        playerRender.sprite = shotgunReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.battleWeapon == "SmallPistol")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, smallPistolAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("SmallPisol_Attack"))//1D6
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 1f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Revolver")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, revolverAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Revolver_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Rifle")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, rifleAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rifle_Attack"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x - 3f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Shotgun")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, shotgunAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Shotgun_Attack"))
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
            if (IsPointer(SlotHybrid1))
            {
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid1Ob.transform.position;
                GameObject obj = deepOneHybrid1Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid1SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", BattleDamage.Rock.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rock_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid1SC.HittedByMolotovChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid1", BattleDamage.Molotov.Roll(UnityRng.Instance)))//2d6+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Molotov_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("DeftNess_Miss"))
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
            else if (IsPointer(SlotHybrid2))
            {
                BattleCameraMove.Instance.ZoomDown();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid2Ob.transform.position;
                GameObject obj = deepOneHybrid2Ob;

                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid2SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", BattleDamage.Rock.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rock_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid2SC.HittedByMolotovChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid2", BattleDamage.Molotov.Roll(UnityRng.Instance)))//2d6+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Molotov_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid2SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("DeftNess_Miss"))
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
            else if (IsPointer(SlotHybrid3))
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid3Ob.transform.position;
                GameObject obj = deepOneHybrid3Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", BattleDamage.Rock.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rock_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedByMolotovChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid3", BattleDamage.Molotov.Roll(UnityRng.Instance)))//2d6+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Molotov_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid3SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("DeftNess_Miss"))
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
            else if (IsPointer(SlotHybrid4))
            {
                BattleCameraMove.Instance.ZoomUp();
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOneHybrid4Ob.transform.position;
                GameObject obj = deepOneHybrid4Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid3SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", BattleDamage.Rock.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rock_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOneHybrid4SC.HittedByMolotovChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid4", BattleDamage.Molotov.Roll(UnityRng.Instance)))//2d6+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Molotov_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOneHybrid4SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => CamRotate(0))
                        .AppendCallback(() => TurnEnd())
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("DeftNess_Miss"))
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
            else if (IsPointer(SlotDeepOne1))
            {
                Vector3 playerOrigin = player.transform.position;
                Vector3 enemyOrigin = deepOne1Ob.transform.position;
                GameObject obj = deepOne1Ob;
                if (Success == "Result : Success" || Success == "Result : Critical Success")
                {
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOne1SC.HittedByShotgunChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", BattleDamage.Rock.Roll(UnityRng.Instance)))//1D6
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Rock_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => deepOne1SC.HittedByMolotovChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => OnDamageObject("DeepOne1", BattleDamage.Molotov.Roll(UnityRng.Instance)))//2d6+2
                        .AppendCallback(() => SoundManager.Instance.BattleSound("Molotov_Attack"))
                        .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                        .Join(ShakeFx.Position(obj.transform, 1, 2, 90))
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
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        playerRender.sprite = rockReady;
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        playerRender.sprite = molotovReady;
                    }
                    cam.transform.DORotate(new Vector3(0, 0, 1), 0.5f);
                    if (DataBaseManager.battleWeapon == "Rock")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("DeftNess_Miss"))
                        .Join(player.transform.DOMove(new Vector3(playerOrigin.x + 2f, playerOrigin.y, -1), 1f))
                        .AppendInterval(0.5f) // 2초 대기
                        .AppendCallback(() => deepOne1SC.StandChange())
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, stand))
                        .AppendCallback(() => TurnEnd())
                        .AppendCallback(() => CamRotate(0))
                        .Append(player.transform.DOMove(originPoint, 0.5f))
                        .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    if (DataBaseManager.battleWeapon == "Molotov")
                    {
                        Sequence sequence = DOTween.Sequence()
                        .AppendCallback(() => OnSpriteChangeComplete(playerRender, deftnessAttack))
                        .AppendCallback(() => SoundManager.Instance.BattleSound("DeftNess_Miss"))
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
                deepOneHybrid3SC.DeepOneHybridPunchChange();
                cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                // 연출 삽입
                Sequence sequence = DOTween.Sequence()
                .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                .AppendCallback(() => OnDamageObject("player", _enemies[SlotHybrid3].Def.AttackDamage.Roll(UnityRng.Instance)))
                .AppendCallback(() => battlePlayer.PlayerHittedRock())
                .AppendCallback(() => SoundManager.Instance.BattleSound("MartialArts"))
                .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                .Join(ShakeFx.Position(player.transform, 1, 2, 90))
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
                deepOneHybrid3SC.DeepOneHybridPunchChange();
                cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                // 연출 삽입
                Sequence sequence = DOTween.Sequence()
                .AppendCallback(() => SoundManager.Instance.BattleSound("Evaision"))
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
                deepOne1SC.DeepOnePoisonChange();
                cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                // 연출 삽입
                Sequence sequence = DOTween.Sequence()
                .AppendCallback(() => OnSpriteChangeComplete(playerRender, hitted))
                .AppendCallback(() => DataBaseManager.posionDebuff = 3)
                .AppendCallback(() => battlePlayer.PlayerHittedPosion())
                .AppendCallback(() => SoundManager.Instance.BattleSound("Posion_Attack"))
                .Join(ShakeFx.Position(cam.transform, 1, 2, 90))
                .Join(ShakeFx.Position(player.transform, 1, 2, 90))
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
                deepOne1SC.DeepOneHybridPunchChange();
                cam.transform.DORotate(new Vector3(0, 0, -1), 0.5f);
                // 연출 삽입
                Sequence sequence = DOTween.Sequence()
                .AppendCallback(() => SoundManager.Instance.BattleSound("Evaision"))
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
        int enemyIndex = EnemyDefs.IndexOf(subject);
        if (enemyIndex >= 0)
        {
            _enemies[enemyIndex].TakeDamage(damage);
            DamageNumber damageNumber = numberPrefab.Spawn(_enemyObs[enemyIndex].transform.position, damage);
        }
        if (subject == "player")
        {
            if (DataBaseManager.masochism == true)
            {
                BillowUIManager.Instance.HpBattleDown(damage);
            }
            else
            {
                BillowUIManager.Instance.HpBattleDown(damage);
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
        for (int i = 0; i < _enemies.Length; i++)
        {
            if (_enemies[i].IsDead && !_enemies[i].DeathHandled && battleState == BattlePhase.SetTurn)
            {
                // 의도적 차이: 옛 코드는 죽은 적을 매 프레임 다시 처리해 사망 수가 계속 늘고, 이전 전투에서 죽은 적의 칸이 다른 적의 턴 심볼을 껐다. 한 번만 처리한다.
                _enemies[i].DeathHandled = true;
                _enemyObs[i].SetActive(false);
                _enemySymbols[SymbolSlotOf(i)].SetActive(false);
                DataBaseManager.enemyDeathCount += 1;
            }
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
        DataBaseManager.battleWeapon = "";
        battleResetUI.EndTrunReset();
        onPointerEnemy = "";
        playerAction = "";
        selectEnemy = "";
        battleState = BattlePhase.SetTurn;
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
        SoundManager.Instance.ClickSoundPlay();
        BattleManager.Instance.enemySelectUI.SetActive(true);
        playerActionUi.SetActive(false);
        playerAttackUi.SetActive(false);
        playerMAUI.SetActive(true);
        battleState = BattlePhase.SelectEnemy;
        playerAction = action;
    }

    public void BackMA()
    {
        maBackButton.SetActive(false);
        SoundManager.Instance.ClickSoundPlay();
        enemySelectUI.SetActive(false);
        playerAttackUi.SetActive(true);
        playerMAUI.SetActive(false);
        battleState = BattlePhase.PlayerAttack;
    }
    public void SetEnemy(string enemy)
    {
        enemySelectUI.SetActive(false);
        int slot = EnemyDefs.IndexOf(enemy);
        if (slot < 0)
        {
            return;
        }
        EnemyState target = _enemies[slot];
        string kind = KindName(target.Def);
        {
            if (playerAction == "martialarts")
            {
                if (slot == SlotHybrid1)
                {
                    // 옛 코드는 혼혈1 만 여기서 무술 UI 를 닫았다(동치 유지).
                    maBackButton.SetActive(false);
                    playerMAUI.SetActive(false);
                    BattleManager.Instance.enemySelectUI.SetActive(false);
                }
                battleState = BattlePhase.Rollet;
                BattleRollet.Instance.SetBattleRollet("Fabian : Punching", "martialArtsPoint", DataBaseManager.martialArtsPoint, "MA_attack_Counter", kind, "??? : CounterAttack", "CounterAttack", target.MartialArts);
            }
            if (playerAction == "PlayerSwords")
            {
                playerSwordsUi.SetActive(false);
                if (DataBaseManager.battleWeapon == "Dagger")
                {
                    battleState = BattlePhase.Rollet;
                    BattleRollet.Instance.SetBattleRollet("Fabian : Stab", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", kind, "??? : CounterAttack", "CounterAttack", target.MartialArts);
                }
                if (DataBaseManager.battleWeapon == "Bat")
                {
                    battleState = BattlePhase.Rollet;
                    BattleRollet.Instance.SetBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", kind, "??? : CounterAttack", "CounterAttack", target.MartialArts);
                }
                if (DataBaseManager.battleWeapon == "Axe")
                {
                    battleState = BattlePhase.Rollet;
                    BattleRollet.Instance.SetBattleRollet("Fabian : Strike", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", kind, "??? : CounterAttack", "CounterAttack", target.MartialArts);
                }
            }
            if (playerAction == "PlayerMarkmen")
            {
                playerMarkmenUi.SetActive(false);
                if (DataBaseManager.battleWeapon == "SmallPistol")
                {
                    battleState = BattlePhase.Rollet;
                    Rollet.Instance.SetRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", kind);
                }
                if (DataBaseManager.battleWeapon == "Rifle")
                {
                    battleState = BattlePhase.Rollet;
                    Rollet.Instance.SetRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", kind);
                }
                if (DataBaseManager.battleWeapon == "Revolver")
                {
                    battleState = BattlePhase.Rollet;
                    Rollet.Instance.SetRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", kind);
                }
                if (DataBaseManager.battleWeapon == "Shotgun")
                {
                    battleState = BattlePhase.Rollet;
                    Rollet.Instance.SetRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.gunShotPoint, "MS_attack", kind);
                }
            }
            if (playerAction == "PlayerDeftness")
            {
                playerDeftnessUi.SetActive(false);
                if (DataBaseManager.battleWeapon == "Rock")
                {
                    battleState = BattlePhase.Rollet;
                    Rollet.Instance.SetRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", kind);
                }
                if (DataBaseManager.battleWeapon == "Molotov")
                {
                    battleState = BattlePhase.Rollet;
                    Rollet.Instance.SetRollet("Fabian : Throw", "DeftnessPoint", DataBaseManager.deftnessPoint, "Deftness_attack", kind);
                }

            }
        
        }
    }
}
