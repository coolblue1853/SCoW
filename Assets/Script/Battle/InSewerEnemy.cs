using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCoW.Core.Rules;
public class InSewerEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && DataBaseManager.isInsmusMeetSewer == false)
        {
            DataBaseManager.isInsmusMeetSewer = true;
            if (BattleEngine.SewerDisguiseFails(DataBaseManager.dex, UnityRng.Instance)) // 적 판정 성공
            {
                InteractionController.Instance.InSewerDialog("Sewer_DisgauseFail");
            }
        }
    }
}
