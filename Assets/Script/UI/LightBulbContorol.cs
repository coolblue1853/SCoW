using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbContorol : MonoBehaviour
{
    public GameObject bulb_1;
    public GameObject bulb_2;
    public GameObject bulb_3;
    public GameObject bulb_4;
    public GameObject bulb_5;
    public GameObject bulb_6;
    public GameObject bulb_7;
    public GameObject bulb_8;
    public GameObject bulb_9;
    public GameObject bulb_10;
    public GameObject bulb_11;
    public GameObject bulb_12;
    public GameObject bulb_13;
    public GameObject bulb_14;
    public GameObject bulb_15;
    public GameObject bulb_16;
    public GameObject bulb_17;
    public GameObject bulb_18;

    void Update()
    {
        if ((DataBaseManager.TimeCount % 4) == 3 || (DataBaseManager.TimeCount % 4) == 0)
        {
            bulb_1.SetActive(true);
            bulb_2.SetActive(true);
            bulb_3.SetActive(true);
            bulb_4.SetActive(true);
            bulb_5.SetActive(true);
            bulb_6.SetActive(true);
            bulb_7.SetActive(true);
            bulb_8.SetActive(true);
            bulb_9.SetActive(true);
            bulb_10.SetActive(true);
            bulb_12.SetActive(true);
            bulb_13.SetActive(true);
            bulb_14.SetActive(true);
            bulb_15.SetActive(true);
            bulb_16.SetActive(true);
            bulb_17.SetActive(true);
            bulb_18.SetActive(true);
        }
        else
        {
            bulb_1.SetActive(false);
            bulb_2.SetActive(false);
            bulb_3.SetActive(false);
            bulb_4.SetActive(false);
            bulb_5.SetActive(false);
            bulb_6.SetActive(false);
            bulb_7.SetActive(false);
            bulb_8.SetActive(false);
            bulb_9.SetActive(false);
            bulb_10.SetActive(false);
            bulb_12.SetActive(false);
            bulb_13.SetActive(false);
            bulb_14.SetActive(false);
            bulb_15.SetActive(false);
            bulb_16.SetActive(false);
            bulb_17.SetActive(false);
            bulb_18.SetActive(false);
        }
    }
}
