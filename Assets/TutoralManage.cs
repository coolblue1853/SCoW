using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoralManage : MonoBehaviour
{
    public GameObject Set;
    public GameObject Main;
    public GameObject Tut;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Quit()
    {
        Tut.SetActive(false);
    }

    public void openSet()
    {
        Set.SetActive(true);
        Main.SetActive(false);
    }

    public void openMain()
    {
        Set.SetActive(false);
        Main.SetActive(true);
    }

}
