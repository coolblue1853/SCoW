using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMapButton : MonoBehaviour
{

    public void ButtonClick()
    {
        if (DataBaseManager.TimeCount == 5 && DataBaseManager.NoonMorningMove == true)
        {
            MapManager.Instance.MapOn();
            DataBaseManager.nowPlace = "NoonEvent";
        }

        else if(this.name == "DetectiveOffice" && (DataBaseManager.isBar == true || DataBaseManager.isSewerOffice == true))
        {
            
            DataBaseManager.nowPlace = "BattleRoad";
            MapManager.Instance.MapOn();

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
