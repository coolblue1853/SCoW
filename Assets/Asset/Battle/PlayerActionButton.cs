using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionButton : MonoBehaviour
{
    public void ClickButton()
    {
        SoundManager.Instance.ClickSound_Play();
        BattleManager.Instance.SetAciton(this.transform.name);
    }

}
