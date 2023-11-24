using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionButton : MonoBehaviour
{
    public bool DontDestroy = false;
    public void ClickButton()
    {
        SoundManager.Instance.ClickSound_Play();
        selectionUIManager.Instance.ReciveRequest_SelectionButton(this.transform.name);
        if (DontDestroy != true)
        {
            Invoke("Destroy", 1);
        }
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
