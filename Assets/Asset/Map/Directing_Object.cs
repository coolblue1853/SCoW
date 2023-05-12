using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directing_Object : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(DataBaseManager.fst_Detectiv_TimeOn < -50 && DataBaseManager.fst_Detectiv_TimeOn > -500 )
            {
                InteractionController.Instance.Start_1st_DetectiveOffice("Directing_Door");
            }
        }
    }


    void destory()
    {
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
