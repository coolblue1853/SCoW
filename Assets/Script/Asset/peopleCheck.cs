using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peopleCheck : MonoBehaviour
{
    public GameObject people;
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