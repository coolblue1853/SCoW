using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : MonoBehaviour
{
     GameObject player;
     SpriteRenderer player_R;
    public Sprite Stand;
    public Sprite Dagger_Ready;
    public Sprite Axe_Ready;
    public Sprite Bat_Ready;

    public Sprite SmallPistol_Ready;
    public Sprite Revolver_Ready;
    public Sprite Shotgun_Ready;
    public Sprite Rifle_Ready;
    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<GameObject>();
        player_R = this.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if((BattleManager.Instance.BattleState == "PlayerAttack" || BattleManager.Instance.BattleState == "selectEnemy") && ( BattleManager.Instance.PlayerAction == "PlayerSwords" || BattleManager.Instance.PlayerAction == "PlayerMarkmen"))
        {
            if(DataBaseManager.BattleWeapon == "")
            {

            }
            else if (DataBaseManager.BattleWeapon == "Dagger" && player_R.sprite != Dagger_Ready)
            {
                BattleManager.Instance.BattleState = "selectEnemy";
                player_R.sprite = Dagger_Ready;
            }
            else if (DataBaseManager.BattleWeapon == "Bat" && player_R.sprite != Bat_Ready)
            {
                BattleManager.Instance.BattleState = "selectEnemy";
                player_R.sprite = Bat_Ready;
            }
            else if (DataBaseManager.BattleWeapon == "Axe" && player_R.sprite != Axe_Ready)
            {
                BattleManager.Instance.BattleState = "selectEnemy";
               player_R.sprite = Axe_Ready;
            }
            else if (DataBaseManager.BattleWeapon == "SmallPistol" && player_R.sprite != SmallPistol_Ready && DataBaseManager.nowSmallPistol >0)
            {
                BattleManager.Instance.BattleState = "selectEnemy";
                player_R.sprite = SmallPistol_Ready;
            }
            else if (DataBaseManager.BattleWeapon == "Revolver" && player_R.sprite != Revolver_Ready && DataBaseManager.nowRevolver > 0)
            {
                BattleManager.Instance.BattleState = "selectEnemy";
                player_R.sprite = Revolver_Ready;
            }
            else if (DataBaseManager.BattleWeapon == "Rifle" && player_R.sprite != Rifle_Ready && DataBaseManager.nowRifle > 0)
            {
                BattleManager.Instance.BattleState = "selectEnemy";
                player_R.sprite = Rifle_Ready;
            }
            else if (DataBaseManager.BattleWeapon == "Shotgun" && player_R.sprite != Shotgun_Ready && DataBaseManager.nowShotgun > 0)
            {
                BattleManager.Instance.BattleState = "selectEnemy";
                player_R.sprite = Shotgun_Ready;
            }
        }
    }
}