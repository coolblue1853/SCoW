using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionButton : MonoBehaviour
{
    public void ClickButton()
    {

        selectionUIManager.Instance.ReciveRequest_SelectionButton(this.transform.name);
        Invoke("Destroy", 1);   
    }

    void Destroy()
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
