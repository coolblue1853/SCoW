using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeStatPage : MonoBehaviour
{


    public GameObject strPage;
    public GameObject intPage;
    public GameObject dexPage;

    public void strPageButton()
    {
        strPage.SetActive(true);
        intPage.SetActive(false);
        dexPage.SetActive(false);

    }
    public void intPageButton()
    {
        strPage.SetActive(false);
        intPage.SetActive(true);
        dexPage.SetActive(false);
    }
    public void dexPageButton()
    {
        strPage.SetActive(false);
        intPage.SetActive(false);
        dexPage.SetActive(true);
    }




}
