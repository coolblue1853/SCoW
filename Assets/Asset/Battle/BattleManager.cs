using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using DamageNumbersPro;
public class BattleManager : MonoBehaviour
{
    public DamageNumber numberPrefab;


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

    public Sprite SmallPistol_Reload;
    public Sprite Revolver_Reload;
    public Sprite Rifle_Reload;
    public Sprite Shotgun_Reload;

    public GameObject PlayerTrunSymbol;
    public GameObject EnemyTrunSymbol_1;
    public GameObject EnemyTrunSymbol_2;

    public GameObject PlayerChoiceUi;
    public GameObject PlayerActionUi;
    public GameObject PlayerAttackUi;
    public GameObject PlayerSwordsUi;
    public GameObject PlayerMarkmenUi;
    public GameObject PlayerReloadUi;
    public GameObject PlayerReloadCheckUi;

    EnemyClass.DeepOneHybrid DeepOneHybrid = new EnemyClass.DeepOneHybrid();

    //적 오브젝트 관련
    public GameObject DeepOneHybrid_Object;
    public SpriteRenderer DeepOneHybrid_Render;
    public Sprite DeepOneHybrid_Stand;
    public Sprite DeepOneHybrid_HittedByPunch;
    public Sprite DeepOneHybrid_HittedByNife;
    public Sprite DeepOneHybrid_HittedByBat;
    public Sprite DeepOneHybrid_HittedByAxe;
    public Sprite DeepOneHybrid_HittedBySmallPistol;
    public Sprite DeepOneHybrid_HittedByRevolver;
    public Sprite DeepOneHybrid_HittedByRifle;
    public Sprite DeepOneHybrid_HittedByShotgun;
    public Sprite DeepOneHybrid_Punch;



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
        player_R = player.GetComponent<SpriteRenderer>();
        DeepOneHybrid_Render = DeepOneHybrid_Object.GetComponent<SpriteRenderer>();
        OriginPoint = player.transform.position;
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
                    SymbolUp(PlayerTrunSymbol, DataBaseManager.dex, "Player");
                }
                if (EnemyTrunSymbol_1.activeSelf == true)
                {
                    SymbolUp(EnemyTrunSymbol_1, DeepOneHybrid.dex, "DeepOneHybrid1");
                }
                if (EnemyTrunSymbol_2.activeSelf == true)
                {
                    SymbolUp(EnemyTrunSymbol_2, 1, "Enemy2");
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
                    symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, symbol.transform.localPosition.y + ((1 + (dex / 100)) * 3f));
                }
                else
                {
                    symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, symbol.transform.localPosition.y + ((1 + (dex / 100) )* 2.9f));
                }
            }
            else
            {
                symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, -130);
                BattleState = Target+"Trun";

                RoundText.text = Target + "의 차례";
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
            BattleItemManager.Instance.ManageItem();
            BattleState = "PlayerChoice";
            Invoke("Player_setAction", 1);
        }
    }
    void Player_setAction()
    {
        RoundGameObject.SetActive(false);
        PlayerActionUi.SetActive(true);
    }

    public void Player_BackAction()
    {
        PlayerAttackUi.SetActive(false);
        BattleState = "PlayerChoice";
        PlayerActionUi.SetActive(true);
    }

    public void Player_setAttack()
    {
        PlayerActionUi.SetActive(false);
        BattleState = "PlayerAttack";
        PlayerAttackUi.SetActive(true);
    }


    public void Player_setSwords()
    {
        PlayerAttackUi.SetActive(false);
        PlayerAction = "PlayerSwords";
        PlayerSwordsUi.SetActive(true);
    }

    public void Player_BackSwords()
    {
        PlayerSwordsUi.SetActive(false);
        PlayerAction = "PlayerAttack";
        PlayerAttackUi.SetActive(true);
    }

    public void Player_setMarkmens()
    {
        PlayerAttackUi.SetActive(false);
        PlayerAction = "PlayerMarkmen";
        PlayerMarkmenUi.SetActive(true);
    }
    public void Player_BackMarkmens()
    {
        PlayerAttackUi.SetActive(true);
        PlayerAction = "PlayerAttack";
        PlayerMarkmenUi.SetActive(false);
    }

    public void Player_setReload()
    {
        PlayerActionUi.SetActive(false);
        PlayerAction = "PlayerReload";
        PlayerReloadUi.SetActive(true);
    }
    public void Player_BackReload()
    {
        PlayerActionUi.SetActive(true);
        PlayerAction = "PlayerChoice";
        PlayerReloadUi.SetActive(false);
    }
    
    public string nowReloadWeapon;

    public void OpenReloadUI(string subject)
    {
        nowReloadWeapon = subject;
        PlayerReloadUi.SetActive(false);
        PlayerReloadCheckUi.SetActive(true);
    }
    public void ReloadOk()
    {
        PlayerReloadCheckUi.SetActive(false);
        BattleCameraMove.Instance.ZoomMe();
        if (nowReloadWeapon == "SmallPistol")
        {
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
        PlayerReloadUi.SetActive(true);
        PlayerReloadCheckUi.SetActive(false);
    }



    void Enemy_setAction()
    {
        // 추후 이곳에 적의 행동 요령을 통해 어떤식으로 작동하는지 써둬야 할듯
        RoundGameObject.SetActive(false);
        OpenRoundObject("적이 당신을 근접전으로 공격하려 합니다.");
        PlayerChoiceUi.SetActive(true);
    }


    public void PlayerChoiceButton_Evasion()
    {
        if (BattleState == "DeepOneHybrid1Attack")
        {
            BattleState = "Rollet";
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("파비안 : 회피", "회피", DataBaseManager.evasionPoint, "evasion","DeepOneHybrid" , "??? : 격투술", "격투술", DeepOneHybrid.DeepOneHybrid_MatialArts);
        }
    }
    public void PlayerChoiceButton_CounterAttack()
    {
        if (BattleState == "DeepOneHybrid1Attack")
        {
            BattleState = "Rollet";
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("파비안 : 반격", "격투술", DataBaseManager.martialArtsPoint, "counterattack", "DeepOneHybrid", "??? : 격투술", "격투술", DeepOneHybrid.DeepOneHybrid_MatialArts);
        }
    }

    public void RetrunRolletResult(string PlayerAction, string Success, string Enemy)
    {


        if (PlayerAction == "evasion")
        {
            if(Enemy == "DeepOneHybrid")
            {
                Vector3 enemyOrigin = DeepOneHybrid_Object.transform.position;
                GameObject obj = DeepOneHybrid_Object;
                SpriteRenderer spR = DeepOneHybrid_Render;
                if (Success == "성공")
                {
                    BattleCameraMove.Instance.ZoomMidle();
                    DeepOneHybrid_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .Append(player.transform.DOMove(new Vector3(player.transform.position.x - 4f, DeepOneHybrid_Object.transform.position.y), 0.7f))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1.5f, player.transform.position.y - 0.2f, -1), 0.7f))
                     .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                     .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
       
                }
                else
                {
                    BattleCameraMove.Instance.ZoomMidle();
                    DeepOneHybrid_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
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
            if (Enemy == "DeepOneHybrid")
            {
                BattleCameraMove.Instance.ZoomMidle();
                Vector3 enemyOrigin = DeepOneHybrid_Object.transform.position;
                GameObject obj = DeepOneHybrid_Object;
                SpriteRenderer spR = DeepOneHybrid_Render;
                if (Success == "성공")
                {
                    DeepOneHybrid_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .Append(player.transform.DOMove(new Vector3(player.transform.position.x - 5f, DeepOneHybrid_Object.transform.position.y), 1f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Punch))
                    .Append(player.transform.DOMove(new Vector3(player.transform.position.x -2, player.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByPunch))
                    .AppendCallback(() => CamRotate(+5))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid", Random.Range(1, 4) * 5))
                     .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(DeepOneHybrid_Object.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 8f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x +2, player.transform.position.y, -1), 1f))

                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                    .AppendCallback(() => CamRotate(0))
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                     .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                }
                else
                {
                    BattleCameraMove.Instance.ZoomMidle();
                    DeepOneHybrid_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
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
            if (Enemy == "DeepOneHybrid")
            {
                Vector3 enemyOrigin = DeepOneHybrid_Object.transform.position;
                GameObject obj = DeepOneHybrid_Object;
                SpriteRenderer spR = DeepOneHybrid_Render;
                if (Success == "성공")
                {
                    player_R.sprite = Punch;
                    Cam.transform.DORotate(new Vector3(0,0,5), 0.5f);
                    // 연출 삽입
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 5.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByPunch))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid", Random.Range(1, 4) * 5))
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
                    .Append(player.transform.DOMove(new Vector3(DeepOneHybrid_Object.transform.position.x - 5.5f, DeepOneHybrid_Object.transform.position.y - 0.2f, -1), 0.5f))
                    .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid_Object.transform.position.x + 2f, DeepOneHybrid_Object.transform.position.y), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid_Render, DeepOneHybrid_Punch))
                    .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid_Object.transform.position.x - 2f, DeepOneHybrid_Object.transform.position.y, -2), 0.2f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                    .AppendCallback(() => CamRotate(-5))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(DeepOneHybrid_Object.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                   .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x  - 3f, obj.transform.position.y), 1.5f))

                    .AppendInterval(0.5f) // 2초 대기
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())

                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                     .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }
            }

        }

        if (PlayerAction == "SS_attack_Counter")
        {
            if (Enemy == "DeepOneHybrid")
            {
                Vector3 enemyOrigin = DeepOneHybrid_Object.transform.position;
                GameObject obj = DeepOneHybrid_Object;
                SpriteRenderer spR = DeepOneHybrid_Render;
                if (Success == "성공")
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
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid", (Random.Range(1, 5) + 2) * 5))//1D4 + 2
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
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid", (Random.Range(1, 9))  * 5))//1D8
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
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid", (Random.Range(1, 7) +2) * 5))//1D8
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
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid_Object.transform.position.x - 3.5f, DeepOneHybrid_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Dagger_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid_Object.transform.position.x + 2f, DeepOneHybrid_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid_Object.transform.position.x - 2f, DeepOneHybrid_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Bat")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid_Object.transform.position.x - 3.5f, DeepOneHybrid_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Bat_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid_Object.transform.position.x + 2f, DeepOneHybrid_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid_Object.transform.position.x - 2f, DeepOneHybrid_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid_Render, DeepOneHybrid_Stand))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                   .AppendCallback(() => TurnEnd())

                   .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));
                    }
                    else if (DataBaseManager.BattleWeapon == "Axe")
                    {
                        Sequence sequence = DOTween.Sequence()
                   .Append(player.transform.DOMove(new Vector3(DeepOneHybrid_Object.transform.position.x - 3.5f, DeepOneHybrid_Object.transform.position.y - 0.2f, -1), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Axe_Attack))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid_Object.transform.position.x + 2f, DeepOneHybrid_Object.transform.position.y), 0.5f))
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid_Render, DeepOneHybrid_Punch))
                   .Append(obj.transform.DOMove(new Vector3(DeepOneHybrid_Object.transform.position.x - 2f, DeepOneHybrid_Object.transform.position.y, -2), 0.2f))
                   .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                   .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                   .AppendCallback(() => CamRotate(-5))
                   .Join(Cam.transform.DOShakePosition(1, 2, 90))
                   .Join(DeepOneHybrid_Object.transform.DOShakePosition(1, 2, 90))
                   .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 7f, obj.transform.position.y, -1), 1f))
                  .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y), 1.5f))

                   .AppendInterval(0.5f) // 2초 대기
                   .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid_Render, DeepOneHybrid_Stand))
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
            if (Enemy == "DeepOneHybrid")
            {
                Vector3 enemyOrigin = DeepOneHybrid_Object.transform.position;
                GameObject obj = DeepOneHybrid_Object;
                SpriteRenderer spR = DeepOneHybrid_Render;
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
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid", (Random.Range(1, 7)) * 5))//1D6
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 22f, obj.transform.position.y, -1), 1f))
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
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid", (Random.Range(1, 11)+2) * 5))//1d10+2
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 23, obj.transform.position.y, -1), 1f))
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
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid", (Random.Range(1, 7)+ Random.Range(1, 7) + 2) * 5))//2D6+2
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 25, obj.transform.position.y, -1), 1f))
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
                        .AppendCallback(() => OnDamageObject("DeepOneHybrid", (Random.Range(1, 6)+ Random.Range(1, 6)+ Random.Range(1, 6)+ Random.Range(1, 6)) * 5))//1D6
                        .Join(Cam.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOShakePosition(1, 2, 90))
                        .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 27, obj.transform.position.y, -1), 1f))
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
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 22f, obj.transform.position.y, -1), 1f))
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
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 23, obj.transform.position.y, -1), 1f))
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
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 25, obj.transform.position.y, -1), 1f))
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
                        .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 27, obj.transform.position.y, -1), 1f))
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

        }
    }






    void OnDamageObject(string subject, int damage, string Weapon ="")
    {
        if(subject == "DeepOneHybrid")
        {
            DeepOneHybrid.NowHP -= damage;
            DamageNumber damageNumber = numberPrefab.Spawn( DeepOneHybrid_Object.transform.position, damage);

        }
        if (subject == "player")
        {
            DamageNumber damageNumber = numberPrefab.Spawn(player.transform.position, damage);
            BillowUIManager.Instance.HP_down(damage);
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
        if(DeepOneHybrid.NowHP <= 0 && BattleState == "setTrun")
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

    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        DeepOneHybrid_Object.transform.position = new Vector3(DeepOneHybrid_Object.transform.position.x, DeepOneHybrid_Object.transform.position.y, 0);
    }
    public void SetAciton(string action)
    {
        PlayerActionUi.SetActive(false);
        BattleState = "selectEnemy";
        PlayerAction = action;
    }
    public void SetEnemy(string enemy)
    {

        if (enemy == "DeepOneHybrid")
        {
            if (PlayerAction == "martialarts")
            {
                BattleState = "Rollet";
                BattleRollet.Instance.setBattleRollet("Fabian : Punching", "martialArtsPoint", DataBaseManager.martialArtsPoint, "MA_attack_Counter", "DeepOneHybrid", "??? : 반격", "반격", DeepOneHybrid.DeepOneHybrid_MatialArts);
            }
            if (PlayerAction == "PlayerSwords")
            {
                PlayerSwordsUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "Dagger")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Stab", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : 반격", "반격", DeepOneHybrid.DeepOneHybrid_MatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Bat")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Hit", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : 반격", "반격", DeepOneHybrid.DeepOneHybrid_MatialArts);
                }
                if (DataBaseManager.BattleWeapon == "Axe")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Hit", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : 반격", "반격", DeepOneHybrid.DeepOneHybrid_MatialArts);
                }
            }
            if (PlayerAction == "PlayerMarkmen")
            {
                PlayerMarkmenUi.SetActive(false);
                if (DataBaseManager.BattleWeapon == "SmallPistol")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.martialArtsPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Rifle")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.martialArtsPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Revolver")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.martialArtsPoint, "MS_attack", "DeepOneHybrid");
                }
                if (DataBaseManager.BattleWeapon == "Shotgun")
                {
                    BattleState = "Rollet";
                    Rollet.Instance.setRollet("Fabian : Shot", "MarkmenshipPoint", DataBaseManager.martialArtsPoint, "MS_attack", "DeepOneHybrid");
                }
            }
        }

    }
}
