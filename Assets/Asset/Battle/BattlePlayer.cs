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
    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<GameObject>();
        player_R = this.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.Instance.BattleState == "PlayerAttack" && ( BattleManager.Instance.PlayerAction == "PlayerSwords"))
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
        }
    }
}
