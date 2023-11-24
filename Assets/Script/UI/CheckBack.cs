using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBack : MonoBehaviour
{

    public GameObject DialogBack;
    public GameObject Back;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogBack.activeSelf == true)
        {
            Back.SetActive(true);
        }
        else
        {
            Back.SetActive(false);
        }
        
    }
}
