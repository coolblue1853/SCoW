using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InSewerEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && DataBaseManager.IsInsmusMeetSewer == false)
        {
            DataBaseManager.IsInsmusMeetSewer = true;
            int RandInt = Random.Range(1, 101);
            if (RandInt < 55)
            {
                int RandIntTwo = Random.Range(1, 101);
                if (DataBaseManager.dex > RandIntTwo)
                {

                }
                else // 적 판정 성공
                {
                    InteractionController.Instance.InSewerDialog("Sewer_DisgauseFail");
                }
            }
            else
            {
            }
        }
    }
}
