using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMapButton : MonoBehaviour
{

    public void ButtonClick()
    {
        if(this.name == "DetectiveOffice" && DataBaseManager.isBar == true)
        {
            MapManager.Instance.MapOn();
            DataBaseManager.nowPlace = "BattleRoad";
        }
        else
        {
            MapManager.Instance.MapOn();
            DataBaseManager.nowPlace = this.name;
        }
        //DataBaseManager.nowPlace = this.name;

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
