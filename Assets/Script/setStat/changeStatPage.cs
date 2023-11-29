using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeStatPage : MonoBehaviour
{
    public GameObject strPage;
    public GameObject intPage;
    public GameObject dexPage;
    public void StrPageButton()
    {
        SoundManager.Instance.PaperClipPlay();
        strPage.SetActive(true);
        intPage.SetActive(false);
        dexPage.SetActive(false);
    }
    public void IntPageButton()
    {
        SoundManager.Instance.PaperClipPlay();
        strPage.SetActive(false);
        intPage.SetActive(true);
        dexPage.SetActive(false);
    }
    public void DexPageButton()
    {
        SoundManager.Instance.PaperClipPlay();
        strPage.SetActive(false);
        intPage.SetActive(false);
        dexPage.SetActive(true);
    }
}
