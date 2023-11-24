using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionButton : MonoBehaviour
{
    public bool dontDestroy = false;
    public void ClickButton()
    {
        SoundManager.Instance.ClickSound_Play();
        selectionUIManager.Instance.ReciveRequestSelectionButton(this.transform.name);
        if(dontDestroy != true)
        {
            Invoke("Destroy", 1);
        }
    }
    void Destroy()
    {
        Destroy(this.gameObject); 
    }
}
