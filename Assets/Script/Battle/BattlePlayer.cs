using UnityEngine;
public class BattlePlayer : MonoBehaviour
{
    GameObject player;
    SpriteRenderer playerRender;
    public Sprite hitted;
    public Sprite stand;
    public Sprite daggerReady;
    public Sprite axeReady;
    public Sprite batReady;
    public Sprite smallPistolReady;
    public Sprite revolverReady;
    public Sprite shotgunReady;
    public Sprite rifleReady;
    public Sprite rockReady;
    public Sprite molotovReady;
    public GameObject hittedDagger;
    public GameObject hittedRcok;
    public GameObject hittedPosion;
    public GameObject hittedSanAttack;
    public GameObject posionActive;

    void Start()
    {
        player = this.GetComponent<GameObject>();
        playerRender = this.GetComponent<SpriteRenderer>();
    }
    public void PlayerHittedDagger()
    {
        playerRender.sprite = hitted;
        hittedDagger.SetActive(true);
    }
    public void PlayerHittedRock()
    {
        playerRender.sprite = hitted;
        hittedRcok.SetActive(true);
    }
    public void PlayerHittedPosion()
    {
        playerRender.sprite = hitted;
        hittedPosion.SetActive(true);
    }
    public void PlayerHittedSanAttack()
    {
        playerRender.sprite = hitted;
        hittedSanAttack.SetActive(true);
    }
    public void resetPlayer()
    {
        hittedDagger.SetActive(false);
        hittedRcok.SetActive(false);
        hittedPosion.SetActive(false);
        hittedSanAttack.SetActive(false);
    }
    void Update()
    {
        if (DataBaseManager.posionDebuff > 0 && posionActive.activeSelf == false)
        {
            posionActive.SetActive(true);
        }
        else if (DataBaseManager.posionDebuff <= 0 && posionActive.activeSelf == true)
        {
            posionActive.SetActive(false);
        }
        if ((BattleManager.Instance.battleState == "PlayerAttack" || BattleManager.Instance.battleState == "selectEnemy"))
        {
            if (BattleManager.Instance.playerAction == "PlayerSwords")
            {
                if (DataBaseManager.battleWeapon == "Dagger" && playerRender.sprite != daggerReady)
                {
                    SoundManager.Instance.Battle_Sound("Dagger_Reddy");
                    BattleManager.Instance.enemySelectUI.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRender.sprite = daggerReady;
                }
                else if (DataBaseManager.battleWeapon == "Bat" && playerRender.sprite != batReady)
                {
                    SoundManager.Instance.Battle_Sound("Bat_Reddy");
                    BattleManager.Instance.enemySelectUI.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRender.sprite = batReady;
                }
                else if (DataBaseManager.battleWeapon == "Axe" && playerRender.sprite != axeReady)
                {
                    SoundManager.Instance.Battle_Sound("Axe_Reddy");
                    BattleManager.Instance.enemySelectUI.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRender.sprite = axeReady;
                }
            }
            if (BattleManager.Instance.playerAction == "PlayerMarkmen")
            {
                if (DataBaseManager.battleWeapon == "SmallPistol" && playerRender.sprite != smallPistolReady && DataBaseManager.nowSmallPistol > 0)
                {
                    SoundManager.Instance.Battle_Sound("SmallPisol_Reddy");
                    BattleManager.Instance.enemySelectUI.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRender.sprite = smallPistolReady;
                }
                else if (DataBaseManager.battleWeapon == "Revolver" && playerRender.sprite != revolverReady && DataBaseManager.nowRevolver > 0)
                {
                    SoundManager.Instance.Battle_Sound("Revolver_Reddy");
                    BattleManager.Instance.enemySelectUI.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRender.sprite = revolverReady;
                }
                else if (DataBaseManager.battleWeapon == "Rifle" && playerRender.sprite != rifleReady && DataBaseManager.nowRifle > 0)
                {
                    SoundManager.Instance.Battle_Sound("Rifle_Reddy");
                    BattleManager.Instance.enemySelectUI.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRender.sprite = rifleReady;
                }
                else if (DataBaseManager.battleWeapon == "Shotgun" && playerRender.sprite != shotgunReady && DataBaseManager.nowShotgun > 0)
                {
                    SoundManager.Instance.Battle_Sound("Shotgun_Reddy");
                    BattleManager.Instance.enemySelectUI.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRender.sprite = shotgunReady;
                }
            }
            if (BattleManager.Instance.playerAction == "PlayerDeftness")
            {
                if (DataBaseManager.battleWeapon == "Rock" && playerRender.sprite != rockReady)
                {
                    SoundManager.Instance.Battle_Sound("Rock_Reddy");
                    BattleManager.Instance.enemySelectUI.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRender.sprite = rockReady;
                }
                else if (DataBaseManager.battleWeapon == "Molotov" && playerRender.sprite != molotovReady && DataBaseManager.molotov > 0)
                {
                    SoundManager.Instance.Battle_Sound("Molotov_Reddy");
                    BattleManager.Instance.enemySelectUI.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRender.sprite = molotovReady;
                }
            }
        }
    }
}
