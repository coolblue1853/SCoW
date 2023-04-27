using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionButton : MonoBehaviour
{
    public void ClickButton()
    {
        BattleManager.Instance.SetAciton(this.transform.name);
    }

}
