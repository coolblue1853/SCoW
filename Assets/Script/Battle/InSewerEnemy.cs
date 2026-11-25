using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCoW.Core.Rules;
using SCoW.Core.Sewer;
public class InSewerEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && DataBaseManager.isInsmusMeetSewer == false)
        {
            DataBaseManager.isInsmusMeetSewer = true;
            // PT-7: 동료가 있으면 전원 중 가장 낮은 은신치로 한 번 판정(동료 없음 = 옛 판정 그대로, 코트 착용이면 동료 가려짐)
            if (SewerPartyRules.DisguiseFails(GameRoot.Instance.State, DataBaseManager.dex, DataBaseManager.wearCoat, UnityRng.Instance)) // 적 판정 성공
            {
                InteractionController.Instance.InSewerDialog("Sewer_DisgauseFail");
            }
        }
    }
}
