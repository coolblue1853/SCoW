using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class interactionEvent : MonoBehaviour
{
    [SerializeField] DialogEvent dialog;

    public Dialog[] GetDialogs()
    {
        dialog.dialogs = DialogDatabaseManager.instance.GetDialog((int)dialog.line.x, (int)dialog.line.y);

        return dialog.dialogs;  
    }
}
