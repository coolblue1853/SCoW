using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAutoClose : MonoBehaviour
{
    public GameObject NameText;
    public GameObject Text;
    void Update()
    {
        if (Text.activeSelf == false)
        {
            NameText.SetActive(false);
        }
    }
}
