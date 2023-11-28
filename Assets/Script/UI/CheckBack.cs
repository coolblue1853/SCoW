using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class CheckBack : MonoBehaviour
{
    public GameObject dialogBack;
    public GameObject back;
    // Update is called once per frame
    void Update()
    {
        if(dialogBack.activeSelf == true)
        {
            back.SetActive(true);
        }
        else
        {
            back.SetActive(false);
        }
    }
}
