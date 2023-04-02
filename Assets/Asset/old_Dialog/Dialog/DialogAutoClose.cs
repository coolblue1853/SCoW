using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAutoClose : MonoBehaviour
{
    public GameObject NameText;
    public GameObject Text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Text.activeSelf== false)
        {
            NameText.SetActive(false);
        }
    }
}
