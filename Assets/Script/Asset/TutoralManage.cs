using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoralManage : MonoBehaviour
{
    public GameObject Set;
    public GameObject Main;
    public GameObject Tut;
    public void Quit()
    {
        SoundManager.Instance.ClickSound_Play();
        Tut.SetActive(false);
    }
    public void openSet()
    {
        SoundManager.Instance.ClickSound_Play();
        Set.SetActive(true);
        Main.SetActive(false);
    }
    public void openMain()
    {
        SoundManager.Instance.ClickSound_Play();
        Set.SetActive(false);
        Main.SetActive(true);
    }
}
