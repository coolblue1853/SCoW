using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class ChangeMapButton : MonoBehaviour
{
    public void ButtonClick()
    {
        if (DataBaseManager.timeCount == 5 && DataBaseManager.noonMorningMove == true)
        {
            MapManager.Instance.MapOn();
            DataBaseManager.nowPlace = "NoonEvent";
        }
        else if(this.name == "DetectiveOffice" && (DataBaseManager.isBar == true || DataBaseManager.isSewerOffice == true))
        {
            DataBaseManager.nowPlace = "BattleRoad";
            MapManager.Instance.MapOn();
        }
        else if (DataBaseManager.isDebuffCognitiveDisorder == true && DataBaseManager.nowPlace != "BattleRoad")
        {
            if (DataBaseManager.timeCount % 4 == 1) // 아침
            {
                Debug.Log("인지장애 작동중");
                List<string> unityList = new List<string> { "Client'shouse", "DailyNews", "University", "Riverside", "Hospital", "Policeoffice", "Slum", "Gunshop" };
                if (DataBaseManager.intelSewageMaintenanceOffice1 == true)
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
            else if (DataBaseManager.timeCount % 4 == 2) // 낮
            {
                List<string> unityList = new List<string> { "Client'shouse", "DailyNews", "University", "Riverside", "Hospital", "Policeoffice", "Slum", "Gunshop" };
                if (DataBaseManager.intelSewageMaintenanceOffice1 == true)
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
            else if (DataBaseManager.timeCount % 4 == 3) // 오후
            {
                List<string> unityList = new List<string> { "DetectiveOffice", "Bar" };
                if (DataBaseManager.intelSewageMaintenanceOffice1 == true)
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
            else if (DataBaseManager.timeCount % 4 == 0) // 오후
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
}
