using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intel_ObtoUI : MonoBehaviour
{
    
    public string name;
    [TextArea]
    public string detail;


    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.tag == "Player")
        {
            BillowUIManager.Instance.SetIntelUi(name, detail);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            BillowUIManager.Instance.ResetIntelUi();
        }
    }

}
