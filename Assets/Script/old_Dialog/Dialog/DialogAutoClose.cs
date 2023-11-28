using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class DialogAutoClose : MonoBehaviour
{
    public GameObject nameText;
    public GameObject text;
    void Update()
    {
        if (text.activeSelf == false)
        {
            nameText.SetActive(false);
        }
    }
}
