using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : MonoBehaviour
{
     GameObject player;
     SpriteRenderer player_R;
    public Sprite Stand;
    public Sprite Dagger_Ready;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<GameObject>();
        player_R = this.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.Instance.BattleState == "PlayerSwords")
        {
            if(DataBaseManager.BattleWeapon == "")
            {

            }
            else if (DataBaseManager.BattleWeapon == "Dagger")
            {
                player_R.sprite = Dagger_Ready;
            }
        }
    }
}
