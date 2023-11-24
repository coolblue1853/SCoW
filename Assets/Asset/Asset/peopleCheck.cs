using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peopleCheck : MonoBehaviour
{
    public GameObject people;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if ((DataBaseManager.TimeCount % 4) == 2)
        {
            people.SetActive(true);
        }
        else
        {
            people.SetActive(false);
        }
    }
}
