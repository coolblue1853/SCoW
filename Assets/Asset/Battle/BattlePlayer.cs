using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : MonoBehaviour
{
    GameObject player;
    SpriteRenderer playerRenderer;
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
    public GameObject posionAcctive;
    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<GameObject>();
        playerRenderer = this.GetComponent<SpriteRenderer>();
    }
    public void PlayerHittedDagger()
    {
        playerRenderer.sprite = hitted;
        hittedDagger.SetActive(true);
    }
    public void PlayerHittedRock()
    {
        playerRenderer.sprite = hitted;
        hittedRcok.SetActive(true);
    }
    public void PlayerHittedPosion()
    {
        playerRenderer.sprite = hitted;
        hittedPosion.SetActive(true);
    }
    public void PlayerHittedSanAttack()
    {
        playerRenderer.sprite = hitted;
        hittedSanAttack.SetActive(true);
    }
    public void ResetPlayer()
    {
        hittedDagger.SetActive(false);
        hittedRcok.SetActive(false);
        hittedPosion.SetActive(false);
        hittedSanAttack.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(DataBaseManager.PosionDebuff > 0 && posionAcctive.activeSelf == false)
        {
            posionAcctive.SetActive(true);
        }
        else if (DataBaseManager.PosionDebuff <= 0 && posionAcctive.activeSelf == true)
        {
            posionAcctive.SetActive(false);
        }
        if((BattleManager.Instance.battleState == "PlayerAttack" || BattleManager.Instance.battleState == "selectEnemy"))
        {
            if(BattleManager.Instance.playerAction == "PlayerSwords")
            { 
             if (DataBaseManager.BattleWeapon == "Dagger" && playerRenderer.sprite != daggerReady)
            {
                SoundManager.Instance.Battle_Sound("Dagger_Reddy");
                BattleManager.Instance.enemySelectUi.SetActive(true);
                BattleManager.Instance.battleState = "selectEnemy";
                playerRenderer.sprite = daggerReady;
            }
            else if (DataBaseManager.BattleWeapon == "Bat" && playerRenderer.sprite != batReady)
            {
                SoundManager.Instance.Battle_Sound("Bat_Reddy");
                BattleManager.Instance.enemySelectUi.SetActive(true);
                BattleManager.Instance.battleState = "selectEnemy";
                playerRenderer.sprite = batReady;
            }
            else if (DataBaseManager.BattleWeapon == "Axe" && playerRenderer.sprite != axeReady)
            {
                SoundManager.Instance.Battle_Sound("Axe_Reddy");
                BattleManager.Instance.enemySelectUi.SetActive(true);
                BattleManager.Instance.battleState = "selectEnemy";
                playerRenderer.sprite = axeReady;
            }
        }
            if (BattleManager.Instance.playerAction == "PlayerMarkmen")
            {
                  if (DataBaseManager.BattleWeapon == "SmallPistol" && playerRenderer.sprite != smallPistolReady && DataBaseManager.nowSmallPistol > 0)
                {
                    SoundManager.Instance.Battle_Sound("SmallPisol_Reddy");
                    BattleManager.Instance.enemySelectUi.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRenderer.sprite = smallPistolReady;
                }
                else if (DataBaseManager.BattleWeapon == "Revolver" && playerRenderer.sprite != revolverReady && DataBaseManager.nowRevolver > 0)
                {
                    SoundManager.Instance.Battle_Sound("Revolver_Reddy");
                    BattleManager.Instance.enemySelectUi.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRenderer.sprite = revolverReady;
                }
                else if (DataBaseManager.BattleWeapon == "Rifle" && playerRenderer.sprite != rifleReady && DataBaseManager.nowRifle > 0)
                {
                    SoundManager.Instance.Battle_Sound("Rifle_Reddy");
                    BattleManager.Instance.enemySelectUi.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRenderer.sprite = rifleReady;
                }
                else if (DataBaseManager.BattleWeapon == "Shotgun" && playerRenderer.sprite != shotgunReady && DataBaseManager.nowShotgun > 0)
                {
                    SoundManager.Instance.Battle_Sound("Shotgun_Reddy");
                    BattleManager.Instance.enemySelectUi.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRenderer.sprite = shotgunReady;
                }
            }
            if (BattleManager.Instance.playerAction == "PlayerDeftness")
            {
                if (DataBaseManager.BattleWeapon == "Rock" && playerRenderer.sprite != rockReady)
                {
                    SoundManager.Instance.Battle_Sound("Rock_Reddy");
                    BattleManager.Instance.enemySelectUi.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRenderer.sprite = rockReady;
                }
                else if (DataBaseManager.BattleWeapon == "Molotov" && playerRenderer.sprite != molotovReady && DataBaseManager.Molotov > 0)
                {
                    SoundManager.Instance.Battle_Sound("Molotov_Reddy");
                    BattleManager.Instance.enemySelectUi.SetActive(true);
                    BattleManager.Instance.battleState = "selectEnemy";
                    playerRenderer.sprite = molotovReady;
                }
            }
        }
    }
}
