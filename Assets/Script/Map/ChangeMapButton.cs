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
        else if (DataBaseManager.isDebuff_CognitiveDisorder == true && DataBaseManager.nowPlace != "BattleRoad")
        {
            if (DataBaseManager.TimeCount % 4 == 1) // 아침
            {
                Debug.Log("인지장애 작동중");
                List<string> unityList = new List<string> { "Client'shouse", "DailyNews", "University", "Riverside", "Hospital", "Policeoffice", "Slum", "Gunshop" };
                if (DataBaseManager.Intel_SewageMaintenanceOffice1 == true)
                {
                    unityList.Add("SewerOffice");
                }
                if (unityList.Contains(DataBaseManager.nowPlace))
                {
                    unityList.Remove(DataBaseManager.nowPlace);
                }
                int randomIndex = Random.Range(0, unityList.Count);
                string selectedElement = unityList[randomIndex];
                DataBaseManager.nowPlace = selectedElement;

            }
            else if (DataBaseManager.TimeCount % 4 == 2) // 낮
            {
                List<string> unityList = new List<string> { "Client'shouse", "DailyNews", "University", "Riverside", "Hospital", "Policeoffice", "Slum", "Gunshop" };
                if (DataBaseManager.Intel_SewageMaintenanceOffice1 == true)
                {
                    unityList.Add("SewerOffice");
                }
                if (unityList.Contains(DataBaseManager.nowPlace))
                {
                    unityList.Remove(DataBaseManager.nowPlace);
                }
                int randomIndex = Random.Range(0, unityList.Count);
                string selectedElement = unityList[randomIndex];
                DataBaseManager.nowPlace = selectedElement;

            }
            else if (DataBaseManager.TimeCount % 4 == 3) // 오후
            {
                List<string> unityList = new List<string> { "DetectiveOffice", "Bar" };
                if (DataBaseManager.Intel_SewageMaintenanceOffice1 == true)
                {
                    unityList.Add("SewerOffice");
                }
                if (unityList.Contains(DataBaseManager.nowPlace))
                {
                    unityList.Remove(DataBaseManager.nowPlace);
                }
                int randomIndex = Random.Range(0, unityList.Count);
                string selectedElement = unityList[randomIndex];
                DataBaseManager.nowPlace = selectedElement;

            }
            else if (DataBaseManager.TimeCount % 4 == 0) // 오후
            {


                DataBaseManager.nowPlace = "DetectiveOffice";

            }
            MapManager.Instance.MapOn();
        }
        else
        {
            MapManager.Instance.MapOn();
            DataBaseManager.nowPlace = this.name;
        }
        //DataBaseManager.nowPlace = this.name;

    }

    public void SewerMapButton()
    {
        FadingBackGround.Instance.FadeInOut();
        Invoke("InvokeSewer",1f);

    }
    public void InvokeSewer()
    {
        MapManager.Instance.CloseMap();
        TimeManagere.Instance.SewerMapMove();
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
