using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMapButton : MonoBehaviour
{

    public void ButtonClick()
    {
        //DataBaseManager.nowPlace = this.name;
        MapManager.Instance.MapOn();
        DataBaseManager.nowPlace = this.name;
    }

    void Chage()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
