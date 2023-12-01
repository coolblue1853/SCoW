using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightBulbContorol : MonoBehaviour
{
    public GameObject bulb1;
    public GameObject bulb2;
    public GameObject bulb3;
    public GameObject bulb4;
    public GameObject bulb5;
    public GameObject bulb6;
    public GameObject bulb7;
    public GameObject bulb8;
    public GameObject bulb9;
    public GameObject bulb10;
    public GameObject bulb11;
    public GameObject bulb12;
    public GameObject bulb13;
    public GameObject bulb14;
    public GameObject bulb15;
    public GameObject bulb16;
    public GameObject bulb17;
    public GameObject bulb18;
    public GameObject bulb19;
    public GameObject bulb20;
    public GameObject bulb21;

    void Update()
    {
        if ((DataBaseManager.timeCount % 4) == 3 || (DataBaseManager.timeCount % 4) == 0)
        {
            bulb1.SetActive(true);
            bulb2.SetActive(true);
            bulb3.SetActive(true);
            bulb4.SetActive(true);
            bulb5.SetActive(true);
            bulb6.SetActive(true);
            bulb7.SetActive(true);
            bulb8.SetActive(true);
            bulb9.SetActive(true);
            bulb10.SetActive(true);
            bulb12.SetActive(true);
            bulb13.SetActive(true);
            bulb14.SetActive(true);
            bulb15.SetActive(true);
            bulb16.SetActive(true);
            bulb17.SetActive(true);
            bulb18.SetActive(true);
            bulb19.SetActive(true);
            bulb20.SetActive(true);
            bulb21.SetActive(true);
        }
        else
        {
            bulb1.SetActive(false);
            bulb2.SetActive(false);
            bulb3.SetActive(false);
            bulb4.SetActive(false);
            bulb5.SetActive(false);
            bulb6.SetActive(false);
            bulb7.SetActive(false);
            bulb8.SetActive(false);
            bulb9.SetActive(false);
            bulb10.SetActive(false);
            bulb12.SetActive(false);
            bulb13.SetActive(false);
            bulb14.SetActive(false);
            bulb15.SetActive(false);
            bulb16.SetActive(false);
            bulb17.SetActive(false);
            bulb18.SetActive(false);
            bulb19.SetActive(false);
            bulb20.SetActive(false);
            bulb21.SetActive(false);
        }
    }
}   
