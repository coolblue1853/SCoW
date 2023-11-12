using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : MonoBehaviour
{
     GameObject player;
     SpriteRenderer player_R;
    public Sprite Hitted;
    public Sprite Stand;
    public Sprite Dagger_Ready;
    public Sprite Axe_Ready;
    public Sprite Bat_Ready;

    public Sprite SmallPistol_Ready;
    public Sprite Revolver_Ready;
    public Sprite Shotgun_Ready;
    public Sprite Rifle_Ready;

    public Sprite Rock_Ready;
    public Sprite Molotov_Ready;





    public GameObject HittedDagger;
    public GameObject HittedRcok;
    public GameObject HittedPosion;
    public GameObject HittedSanAttack;
    public GameObject PosionAcctive;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<GameObject>();
        player_R = this.GetComponent<SpriteRenderer>();

    }

    public void PlayerHittedDagger()
    {
        player_R.sprite = Hitted;
        HittedDagger.SetActive(true);
    }
    public void PlayerHittedRock()
    {
        player_R.sprite = Hitted;
        HittedRcok.SetActive(true);
    }
    public void PlayerHittedPosion()
    {
        player_R.sprite = Hitted;
        HittedPosion.SetActive(true);
    }
    public void PlayerHittedSanAttack()
    {
        player_R.sprite = Hitted;
        HittedSanAttack.SetActive(true);
    }

    public void resetPlayer()
    {
        HittedDagger.SetActive(false);
        HittedRcok.SetActive(false);
        HittedPosion.SetActive(false);
        HittedSanAttack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(DataBaseManager.PosionDebuff > 0 && PosionAcctive.activeSelf == false)
        {
            PosionAcctive.SetActive(true);
        }
        else if (DataBaseManager.PosionDebuff <= 0 && PosionAcctive.activeSelf == true)
        {
            PosionAcctive.SetActive(false);
        }

        if((BattleManager.Instance.BattleState == "PlayerAttack" || BattleManager.Instance.BattleState == "selectEnemy"))
        {
            if(BattleManager.Instance.PlayerAction == "PlayerSwords")
            { 
             if (DataBaseManager.BattleWeapon == "Dagger" && player_R.sprite != Dagger_Ready)
            {
                SoundManager.Instance.Battle_Sound("Dagger_Reddy");
                BattleManager.Instance.EnemySelectUI.SetActive(true);
                BattleManager.Instance.BattleState = "selectEnemy";
                player_R.sprite = Dagger_Ready;
            }
            else if (DataBaseManager.BattleWeapon == "Bat" && player_R.sprite != Bat_Ready)
            {
                SoundManager.Instance.Battle_Sound("Bat_Reddy");
                BattleManager.Instance.EnemySelectUI.SetActive(true);
                BattleManager.Instance.BattleState = "selectEnemy";
                player_R.sprite = Bat_Ready;
            }
            else if (DataBaseManager.BattleWeapon == "Axe" && player_R.sprite != Axe_Ready)
            {
                SoundManager.Instance.Battle_Sound("Axe_Reddy");
                BattleManager.Instance.EnemySelectUI.SetActive(true);
                BattleManager.Instance.BattleState = "selectEnemy";
                player_R.sprite = Axe_Ready;
            }
        }
            if (BattleManager.Instance.PlayerAction == "PlayerMarkmen")
            {
                  if (DataBaseManager.BattleWeapon == "SmallPistol" && player_R.sprite != SmallPistol_Ready && DataBaseManager.nowSmallPistol > 0)
                {
                    SoundManager.Instance.Battle_Sound("SmallPisol_Reddy");
                    BattleManager.Instance.EnemySelectUI.SetActive(true);
                    BattleManager.Instance.BattleState = "selectEnemy";
                    player_R.sprite = SmallPistol_Ready;
                }
                else if (DataBaseManager.BattleWeapon == "Revolver" && player_R.sprite != Revolver_Ready && DataBaseManager.nowRevolver > 0)
                {
                    SoundManager.Instance.Battle_Sound("Revolver_Reddy");
                    BattleManager.Instance.EnemySelectUI.SetActive(true);
                    BattleManager.Instance.BattleState = "selectEnemy";
                    player_R.sprite = Revolver_Ready;
                }
                else if (DataBaseManager.BattleWeapon == "Rifle" && player_R.sprite != Rifle_Ready && DataBaseManager.nowRifle > 0)
                {
                    SoundManager.Instance.Battle_Sound("Rifle_Reddy");
                    BattleManager.Instance.EnemySelectUI.SetActive(true);
                    BattleManager.Instance.BattleState = "selectEnemy";
                    player_R.sprite = Rifle_Ready;
                }
                else if (DataBaseManager.BattleWeapon == "Shotgun" && player_R.sprite != Shotgun_Ready && DataBaseManager.nowShotgun > 0)
                {
                    SoundManager.Instance.Battle_Sound("Shotgun_Reddy");
                    BattleManager.Instance.EnemySelectUI.SetActive(true);
                    BattleManager.Instance.BattleState = "selectEnemy";
                    player_R.sprite = Shotgun_Ready;
                }
            }
            if (BattleManager.Instance.PlayerAction == "PlayerDeftness")
            {
                         if (DataBaseManager.BattleWeapon == "Rock" && player_R.sprite != Rock_Ready)
                {
                    SoundManager.Instance.Battle_Sound("Rock_Reddy");
                    BattleManager.Instance.EnemySelectUI.SetActive(true);
                    BattleManager.Instance.BattleState = "selectEnemy";
                    player_R.sprite = Rock_Ready;
                }
                else if (DataBaseManager.BattleWeapon == "Molotov" && player_R.sprite != Molotov_Ready && DataBaseManager.Molotov > 0)
                {
                    SoundManager.Instance.Battle_Sound("Molotov_Reddy");
                    BattleManager.Instance.EnemySelectUI.SetActive(true);
                    BattleManager.Instance.BattleState = "selectEnemy";
                    player_R.sprite = Molotov_Ready;
                }
            }
    
        }
    }
}
