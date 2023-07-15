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

    //�÷��̾� ����
    public GameObject player;
    Vector3 OriginPoint;
    public SpriteRenderer player_R;
    public Sprite Stand;
    public Sprite Punch;
    public Sprite Hitted;
    public Sprite Dagger_Ready;
    public Sprite Dagger_Attack;


    public GameObject PlayerTrunSymbol;
    public GameObject EnemyTrunSymbol_1;
    public GameObject EnemyTrunSymbol_2;

    public GameObject PlayerChoiceUi;
    public GameObject PlayerActionUi;
    public GameObject PlayerAttackUi;
    public GameObject PlayerSwordsUi;


    EnemyClass.DeepOneHybrid DeepOneHybrid = new EnemyClass.DeepOneHybrid();

    //�� ������Ʈ ����
    public GameObject DeepOneHybrid_Object;
    public SpriteRenderer DeepOneHybrid_Render;
    public Sprite DeepOneHybrid_Stand;
    public Sprite DeepOneHybrid_HittedByPunch;
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

    // �� �ּ����� -130, �ִ����� +130
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
                    symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, symbol.transform.localPosition.y + (1 + (dex / 100) * 1.5f));
                }
                else
                {
                    symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, symbol.transform.localPosition.y + (1 + (dex / 100) * 1.1f));
                }
            }
            else
            {
                symbol.transform.localPosition = new Vector2(symbol.transform.localPosition.x, -130);
                BattleState = Target+"Trun";

                RoundText.text = Target + "�� ����";
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








    void Enemy_setAction()
    {
        // ���� �̰��� ���� �ൿ ����� ���� ������� �۵��ϴ��� ��־� �ҵ�
        RoundGameObject.SetActive(false);
        OpenRoundObject("���� ����� ���������� �����Ϸ� �մϴ�.");
        PlayerChoiceUi.SetActive(true);
    }


    public void PlayerChoiceButton_Evasion()
    {
        if (BattleState == "DeepOneHybrid1Attack")
        {
            BattleState = "Rollet";
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("�ĺ�� : ȸ��", "ȸ��", DataBaseManager.evasionPoint, "evasion","DeepOneHybrid" , "??? : ������", "������", DeepOneHybrid.DeepOneHybrid_MatialArts);
        }
    }
    public void PlayerChoiceButton_CounterAttack()
    {
        if (BattleState == "DeepOneHybrid1Attack")
        {
            BattleState = "Rollet";
            PlayerChoiceUi.SetActive(false);
            CloseRoundObject();
            BattleRollet.Instance.setBattleRollet("�ĺ�� : �ݰ�", "������", DataBaseManager.martialArtsPoint, "counterattack", "DeepOneHybrid", "??? : ������", "������", DeepOneHybrid.DeepOneHybrid_MatialArts);
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
                if (Success == "����")
                {
                    BattleCameraMove.Instance.ZoomMidle();
                    DeepOneHybrid_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // ���� ����
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .Append(player.transform.DOMove(new Vector3(player.transform.position.x - 4f, DeepOneHybrid_Object.transform.position.y), 1f))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1.5f, player.transform.position.y - 0.2f, -1), 2))
                     .AppendInterval(0.5f) // 2�� ���
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
                    // ���� ����
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2�� ���
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
                if (Success == "����")
                {
                    DeepOneHybrid_Render.sprite = DeepOneHybrid_Punch;
                    Cam.transform.DORotate(new Vector3(0, 0, -5), 0.5f);
                    // ���� ����
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

                    .AppendInterval(0.5f) // 2�� ���
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
                    // ���� ����
                    Sequence sequence = DOTween.Sequence()
                    .Append(obj.transform.DOMove(new Vector3(player.transform.position.x + 2.5f, player.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Hitted))
                    .AppendCallback(() => OnDamageObject("player", Random.Range(1, 4) * 5))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(player.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(player.transform.position.x + 1f, player.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(player.transform.position.x - 3f, player.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2�� ���
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
                if (Success == "����")
                {
                    player_R.sprite = Punch;
                    Cam.transform.DORotate(new Vector3(0,0,5), 0.5f);
                    // ���� ����
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 5.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByPunch))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid", Random.Range(1, 4) * 5))
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2�� ���
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
                    // ���� ����
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

                    .AppendInterval(0.5f) // 2�� ���
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
                if (Success == "����")
                {
                    if(DataBaseManager.BattleWeapon == "Dagger")
                    {
                        player_R.sprite = Dagger_Ready;
                    }


                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // ���� ����
                    Sequence sequence = DOTween.Sequence()
                    .Append(player.transform.DOMove(new Vector3(obj.transform.position.x - 3.5f, obj.transform.position.y - 0.2f, -1), 0.5f))
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_HittedByPunch))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Dagger_Attack))
                    .AppendCallback(() => OnDamageObject("DeepOneHybrid", (Random.Range(1, 5)+2) * 5))//1D4 + 2
                    .Join(Cam.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOShakePosition(1, 2, 90))
                    .Join(obj.transform.DOMove(new Vector3(obj.transform.position.x + 3f, obj.transform.position.y), 1.5f))
                    .Join(player.transform.DOMove(new Vector3(obj.transform.position.x - 3f, obj.transform.position.y, -1), 1f))
                    .AppendInterval(0.5f) // 2�� ���
                    .AppendCallback(() => OnSpriteChangeComplete(spR, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())
                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                    .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }
                else
                {
                    if (DataBaseManager.BattleWeapon == "Dagger")
                    {
                        player_R.sprite = Dagger_Ready;
                    }
                    Cam.transform.DORotate(new Vector3(0, 0, 5), 0.5f);
                    // ���� ����
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

                    .AppendInterval(0.5f) // 2�� ���
                    .AppendCallback(() => OnSpriteChangeComplete(DeepOneHybrid_Render, DeepOneHybrid_Stand))
                    .AppendCallback(() => OnSpriteChangeComplete(player_R, Stand))
                    .AppendCallback(() => TurnEnd())

                    .Append(player.transform.DOMove(OriginPoint, 0.5f))
                     .Join(obj.transform.DOMove(enemyOrigin, 0.5f));

                }
            }

        }
    }






    void OnDamageObject(string subject, int damage)
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

        // ����� ��������Ʈ ���� Ʈ�� �ִϸ��̼� ����
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
                BattleRollet.Instance.setBattleRollet("Fabian : Punching", "martialArtsPoint", DataBaseManager.martialArtsPoint, "MA_attack_Counter", "DeepOneHybrid", "??? : �ݰ�", "�ݰ�", DeepOneHybrid.DeepOneHybrid_MatialArts);
            }
            if (PlayerAction == "PlayerSwords")
            {
                if (DataBaseManager.BattleWeapon == "Dagger")
                {
                    BattleState = "Rollet";
                    BattleRollet.Instance.setBattleRollet("Fabian : Stab", "swordPoint", DataBaseManager.swordPoint, "SS_attack_Counter", "DeepOneHybrid", "??? : �ݰ�", "�ݰ�", DeepOneHybrid.DeepOneHybrid_MatialArts);
                }
               
            }
        }

    }
}
