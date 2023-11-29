using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class Directing_Object : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && this.name == "Directing_Nock")
        {
            if (DataBaseManager.fstDetectivTimeOn < -50 && DataBaseManager.fstDetectivTimeOn > -500)
            {
                InteractionController.Instance.Start1stDetectiveOffice("Directing_Door");
            }
        }
        if (collision.tag == "Player" && this.name == "Directing_NockEnd")
        {
            DataBaseManager.storyDirecting = true;
            InteractionController.Instance.InSewerDialog("Ending_DoorEnd");
        }
    }
    void Destory()
    {
        Destroy(this.gameObject);
    }
}
