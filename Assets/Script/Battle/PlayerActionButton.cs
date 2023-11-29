using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerActionButton : MonoBehaviour
{
    public void ClickButton()
    {
        SoundManager.Instance.ClickSoundPlay();
        BattleManager.Instance.SetAciton(this.transform.name);
    }
}
