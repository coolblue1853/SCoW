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
        SoundManager.Instance.ClickSoundPlay();
        tutorial.SetActive(false);
    }
    public void OpenSet()
    {
        SoundManager.Instance.ClickSoundPlay();
        setting.SetActive(true);
        main.SetActive(false);
    }
    public void OpenMain()
    {
        SoundManager.Instance.ClickSoundPlay();
        setting.SetActive(false);
        main.SetActive(true);
    }
}
