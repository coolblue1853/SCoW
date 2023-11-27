using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceButtonManage : MonoBehaviour
{
    void Update()
    {
        ButtonUpdate();
        if (ClickDownButton == false)
        {
            ButtonDetail();
        }
        if (Upbutton2.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ClickUpButton2();
            }
        }
        else if (UPButton.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ClickUpButton();
            }
        }
        if (DownButton2.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ClickDownButton_2();
            }
        }
        else if (DownButton.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ClickDownButton_();
            }
        }
    }

    public GameObject UPButton;
    public GameObject DownButton;
    public GameObject Button_DetectiveOffice;
    public GameObject Button_Hospital;
    public GameObject Button_Newspaper;
    public GameObject Button_QuestHouse;
    public GameObject Button_Station;
    public GameObject Button_Restaurant;
    public GameObject Button_University;
    public GameObject Button_Riverside;
    public GameObject Button_Bar;
    public GameObject Button_Slum;
    public GameObject Button_PoliceOffice;
    public GameObject Button_Gunsmith;
    public GameObject Button_SewageMaintenanceOffice;
    public GameObject Button_Wharf;
    bool ButtonRize_DetectiveOffice = false;
    bool ButtonRize_Hospital = false;
    bool ButtonRize_Newspaper = false;
    bool ButtonRize_QuestHouse = false;
    bool ButtonRize_Station = false;
    bool ButtonRize_Restaurant = false;
    bool ButtonRize_University = false;
    bool ButtonRize_Riverside = false;
    bool ButtonRize_Bar = false;
    bool ButtonRize_Slum = false;
    bool ButtonRize_PoliceOffice = false;
    bool ButtonRize_Gunsmith = false;
    bool ButtonRize_SewageMaintenanceOffice = false;
    bool ButtonRize_Wharf = false;
    public GameObject ButtonRect;
    bool ClickDownButton = false;
    bool ClickDownButton2 = false;
    public GameObject Upbutton2;
    public GameObject DownButton2;
    void ButtonUpdate()
    {
        if ((DataBaseManager.Intel_DetectiveOffice1 == true || DataBaseManager.Intel_DetectiveOffice2 == true || DataBaseManager.Intel_DetectiveOffice3 == true || DataBaseManager.Intel_DetectiveOffice4 == true || DataBaseManager.Intel_DetectiveOffice5 == true || DataBaseManager.Intel_DetectiveOffice6 == true) && ButtonRize_DetectiveOffice == false)
        {
            ButtonRize_DetectiveOffice = true;
            DataBaseManager.ButtonCount_DetectiveOffice = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Hospital1 == true || DataBaseManager.Intel_Hospital2 == true || DataBaseManager.Intel_Hospital3 == true || DataBaseManager.Intel_Hospital4 == true || DataBaseManager.Intel_Hospital5 == true || DataBaseManager.Intel_Hospital6 == true) && ButtonRize_Hospital == false)
        {
            ButtonRize_Hospital = true;
            DataBaseManager.ButtonCount_Hospital = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Newspaper1 == true || DataBaseManager.Intel_Newspaper2 == true || DataBaseManager.Intel_Newspaper3 == true || DataBaseManager.Intel_Newspaper4 == true || DataBaseManager.Intel_Newspaper5 == true || DataBaseManager.Intel_Newspaper6 == true) && ButtonRize_Newspaper == false)
        {
            ButtonRize_Newspaper = true;
            DataBaseManager.ButtonCount_Newspaper = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_QuestHouse1 == true || DataBaseManager.Intel_QuestHouse2 == true || DataBaseManager.Intel_QuestHouse3 == true || DataBaseManager.Intel_QuestHouse4 == true || DataBaseManager.Intel_QuestHouse5 == true || DataBaseManager.Intel_QuestHouse6 == true) && ButtonRize_QuestHouse == false)
        {
            ButtonRize_QuestHouse = true;
            DataBaseManager.ButtonCount_QuestHouse = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Station1 == true || DataBaseManager.Intel_Station2 == true || DataBaseManager.Intel_Station3 == true || DataBaseManager.Intel_Station4 == true || DataBaseManager.Intel_Station5 == true || DataBaseManager.Intel_Station6 == true) && ButtonRize_Station == false)
        {
            ButtonRize_Station = true;
            DataBaseManager.ButtonCount_Station = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Sewer1 == true || DataBaseManager.Intel_Sewer2 == true || DataBaseManager.Intel_Sewer3 == true || DataBaseManager.Intel_Sewer4 == true || DataBaseManager.Intel_Sewer5 == true || DataBaseManager.Intel_Sewer6 == true) && ButtonRize_Restaurant == false)
        {
            ButtonRize_Restaurant = true;
            DataBaseManager.ButtonCount_Sewer = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_University1 == true || DataBaseManager.Intel_University2 == true || DataBaseManager.Intel_University3 == true || DataBaseManager.Intel_University4 == true || DataBaseManager.Intel_University5 == true || DataBaseManager.Intel_University6 == true) && ButtonRize_University == false)
        {
            ButtonRize_University = true;
            DataBaseManager.ButtonCount_University = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Riverside1 == true || DataBaseManager.Intel_Riverside2 == true || DataBaseManager.Intel_Riverside3 == true || DataBaseManager.Intel_Riverside4 == true || DataBaseManager.Intel_Riverside5 == true || DataBaseManager.Intel_Riverside6 == true) && ButtonRize_Riverside == false)
        {
            ButtonRize_Riverside = true;
            DataBaseManager.ButtonCount_Riverside = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Bar1 == true || DataBaseManager.Intel_Bar2 == true || DataBaseManager.Intel_Bar3 == true || DataBaseManager.Intel_Bar4 == true || DataBaseManager.Intel_Bar5 == true || DataBaseManager.Intel_Bar6 == true) && ButtonRize_Bar == false)
        {
            ButtonRize_Bar = true;
            DataBaseManager.ButtonCount_Bar = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Slum1 == true || DataBaseManager.Intel_Slum2 == true || DataBaseManager.Intel_Slum3 == true || DataBaseManager.Intel_Slum4 == true || DataBaseManager.Intel_Slum5 == true || DataBaseManager.Intel_Slum6 == true) && ButtonRize_Slum == false)
        {
            ButtonRize_Slum = true;
            DataBaseManager.ButtonCount_Slum = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_PoliceOffice1 == true || DataBaseManager.Intel_PoliceOffice2 == true || DataBaseManager.Intel_PoliceOffice3 == true || DataBaseManager.Intel_PoliceOffice4 == true || DataBaseManager.Intel_PoliceOffice5 == true || DataBaseManager.Intel_PoliceOffice6 == true) && ButtonRize_PoliceOffice == false)
        {
            ButtonRize_PoliceOffice = true;
            DataBaseManager.ButtonCount_PoliceOffice = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Gunsmith1 == true || DataBaseManager.Intel_Gunsmith2 == true || DataBaseManager.Intel_Gunsmith3 == true || DataBaseManager.Intel_Gunsmith4 == true || DataBaseManager.Intel_Gunsmith5 == true || DataBaseManager.Intel_Gunsmith6 == true) && ButtonRize_Gunsmith == false)
        {
            ButtonRize_Gunsmith = true;
            DataBaseManager.ButtonCount_Gunsmith = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_SewageMaintenanceOffice1 == true || DataBaseManager.Intel_SewageMaintenanceOffice2 == true || DataBaseManager.Intel_SewageMaintenanceOffice3 == true || DataBaseManager.Intel_SewageMaintenanceOffice4 == true || DataBaseManager.Intel_SewageMaintenanceOffice5 == true || DataBaseManager.Intel_SewageMaintenanceOffice6 == true) && ButtonRize_SewageMaintenanceOffice == false)
        {
            ButtonRize_SewageMaintenanceOffice = true;
            DataBaseManager.ButtonCount_SewageMaintenanceOffice = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Wharf1 == true || DataBaseManager.Intel_Wharf2 == true || DataBaseManager.Intel_Wharf3 == true || DataBaseManager.Intel_Wharf4 == true || DataBaseManager.Intel_Wharf5 == true || DataBaseManager.Intel_Wharf6 == true) && ButtonRize_Wharf == false)
        {
            ButtonRize_Wharf = true;
            DataBaseManager.ButtonCount_Wharf = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
    }
    void ButtonDetail()
    {
        if (DataBaseManager.ButtonCount_DetectiveOffice != -1)
        {
            Button_DetectiveOffice.SetActive(true);
            Button_DetectiveOffice.transform.SetSiblingIndex(DataBaseManager.ButtonCount_DetectiveOffice);
        }
        if (DataBaseManager.ButtonCount_Hospital != -1)
        {
            Button_Hospital.SetActive(true);
            Button_Hospital.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Hospital);
        }
        if (DataBaseManager.ButtonCount_Newspaper != -1)
        {
            Button_Newspaper.SetActive(true);
            Button_Newspaper.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Newspaper);
        }
        if (DataBaseManager.ButtonCount_QuestHouse != -1)
        {
            Button_QuestHouse.SetActive(true);
            Button_QuestHouse.transform.SetSiblingIndex(DataBaseManager.ButtonCount_QuestHouse);
        }
        if (DataBaseManager.ButtonCount_Station != -1)
        {
            Button_Station.SetActive(true);
            Button_Station.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Station);
        }
        if (DataBaseManager.ButtonCount_Sewer != -1)
        {
            Button_Restaurant.SetActive(true);
            Button_Restaurant.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Sewer);
        }
        if (DataBaseManager.ButtonCount_University != -1)
        {
            Button_University.SetActive(true);
            Button_University.transform.SetSiblingIndex(DataBaseManager.ButtonCount_University);
        }
        if (DataBaseManager.ButtonCount_Riverside != -1)
        {
            Button_Riverside.SetActive(true);
            Button_Riverside.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Riverside);
        }
        if (DataBaseManager.ButtonCount_Bar != -1)
        {
            Button_Bar.SetActive(true);
            Button_Bar.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Bar);
        }
        if (DataBaseManager.ButtonCount_Slum != -1)
        {
            Button_Slum.SetActive(true);
            Button_Slum.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Slum);
        }
        if (DataBaseManager.ButtonCount_PoliceOffice != -1)
        {
            Button_PoliceOffice.SetActive(true);
            Button_PoliceOffice.transform.SetSiblingIndex(DataBaseManager.ButtonCount_PoliceOffice);
        }
        if (DataBaseManager.ButtonCount_Gunsmith != -1)
        {
            Button_Gunsmith.SetActive(true);
            Button_Gunsmith.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Gunsmith);
        }
        if (DataBaseManager.ButtonCount_SewageMaintenanceOffice != -1)
        {
            Button_SewageMaintenanceOffice.SetActive(true);
            Button_SewageMaintenanceOffice.transform.SetSiblingIndex(DataBaseManager.ButtonCount_SewageMaintenanceOffice);
        }
        if (DataBaseManager.ButtonCount_Wharf != -1)
        {
            Button_Wharf.SetActive(true);
            Button_Wharf.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Wharf);
        }
        if (DataBaseManager.GainPlaceInt > 6)
        {
            DownButton.SetActive(true);
        }
    }

    public void ClickUpButton()
    {
        SoundManager.Instance.ClickSound_Play();
        ClickDownButton = false;
        UPButton.SetActive(false);
        DownButton.SetActive(true);
    }
    public void ClickUpButton2()
    {
        SoundManager.Instance.ClickSound_Play();
        UPButton.SetActive(true);
        Upbutton2.SetActive(false);
        DownButton.SetActive(true);
        DownButton2.SetActive(false);
        Transform trChild16 = ButtonRect.transform.GetChild(6);
        trChild16.gameObject.SetActive(true);
        Transform trChild7 = ButtonRect.transform.GetChild(7);
        trChild7.gameObject.SetActive(true);
        Transform trChild9 = ButtonRect.transform.GetChild(8);
        trChild9.gameObject.SetActive(true);
        Transform trChild10 = ButtonRect.transform.GetChild(9);
        trChild10.gameObject.SetActive(true);
        Transform trChild11 = ButtonRect.transform.GetChild(10);
        trChild11.gameObject.SetActive(true);
        Transform trChild12 = ButtonRect.transform.GetChild(11);
        trChild12.gameObject.SetActive(true);
    }
    public void ClickDownButton_()
    {
        SoundManager.Instance.ClickSound_Play();
        UPButton.SetActive(true);
        DownButton.SetActive(false);
        ClickDownButton = true;
        for (int i = 0; i < 6; i++)
        {
            Transform trChild = ButtonRect.transform.GetChild(i);
            trChild.gameObject.SetActive(false);
        }
        if (DataBaseManager.GainPlaceInt == 7)
        {
            Transform trChild16 = ButtonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
        }
        else if (DataBaseManager.GainPlaceInt == 8)
        {
            Transform trChild16 = ButtonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = ButtonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
        }
        else if (DataBaseManager.GainPlaceInt == 9)
        {
            Transform trChild16 = ButtonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = ButtonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild9 = ButtonRect.transform.GetChild(8);
            trChild9.gameObject.SetActive(true);
        }
        else if (DataBaseManager.GainPlaceInt == 10)
        {
            Transform trChild16 = ButtonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = ButtonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild9 = ButtonRect.transform.GetChild(8);
            trChild9.gameObject.SetActive(true);
            Transform trChild10 = ButtonRect.transform.GetChild(9);
            trChild10.gameObject.SetActive(true);
        }
        else if (DataBaseManager.GainPlaceInt == 11)
        {
            Transform trChild16 = ButtonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = ButtonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild9 = ButtonRect.transform.GetChild(8);
            trChild9.gameObject.SetActive(true);
            Transform trChild10 = ButtonRect.transform.GetChild(9);
            trChild10.gameObject.SetActive(true);
            Transform trChild11 = ButtonRect.transform.GetChild(10);
            trChild11.gameObject.SetActive(true);
        }
        else if (DataBaseManager.GainPlaceInt == 12)
        {
            Transform trChild16 = ButtonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = ButtonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild9 = ButtonRect.transform.GetChild(8);
            trChild9.gameObject.SetActive(true);
            Transform trChild10 = ButtonRect.transform.GetChild(9);
            trChild10.gameObject.SetActive(true);
            Transform trChild11 = ButtonRect.transform.GetChild(10);
            trChild11.gameObject.SetActive(true);
            Transform trChild12 = ButtonRect.transform.GetChild(11);
            trChild12.gameObject.SetActive(true);
        }
        if (DataBaseManager.GainPlaceInt > 12)
        {
            DownButton2.SetActive(true);
        }
    }
    public void ClickDownButton_2()
    {
        SoundManager.Instance.ClickSound_Play();
        UPButton.SetActive(false);
        Upbutton2.SetActive(true);
        DownButton.SetActive(false);
        DownButton2.SetActive(false);

        ClickDownButton2 = true;
        for (int i = 0; i < 12; i++)
        {
            Transform trChild = ButtonRect.transform.GetChild(i);
            trChild.gameObject.SetActive(false);
        }
        if (DataBaseManager.GainPlaceInt == 13)
        {
            Transform trChild12 = ButtonRect.transform.GetChild(12);
            trChild12.gameObject.SetActive(true);
        }
    }
}
