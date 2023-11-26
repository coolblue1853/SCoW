using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TutoralManage : MonoBehaviour
{
    public GameObject setting;
    public GameObject main;
    public GameObject tutorial;
    public void Quit()
    {
        SoundManager.Instance.ClickSound_Play();
        tutorial.SetActive(false);
    }
    public void openSet()
    {
        SoundManager.Instance.ClickSound_Play();
        setting.SetActive(true);
        main.SetActive(false);
    }
    public void openMain()
    {
        SoundManager.Instance.ClickSound_Play();
        setting.SetActive(false);
        main.SetActive(true);
    }
}
