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
            int randInt = Random.Range(1, 101);
            if (randInt < 55) 
            {
                int RandIntTwo = Random.Range(1, 101);
                if(DataBaseManager.dex > RandIntTwo)
                {
                }
                else // �� ���� ����
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
