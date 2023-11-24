using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoralManage : MonoBehaviour
{
    public GameObject set;
    public GameObject main;
    public GameObject tutorial;
    // Start is called before the first frame update
    public void Quit()
    {
        SoundManager.Instance.ClickSound_Play();
        tutorial.SetActive(false);
    }
    public void openSet()
    {
        SoundManager.Instance.ClickSound_Play();
        set.SetActive(true);
        main.SetActive(false);
    }
    public void openMain()
    {
        SoundManager.Instance.ClickSound_Play();
        set.SetActive(false);
        main.SetActive(true);
    }

}
