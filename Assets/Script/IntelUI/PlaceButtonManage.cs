using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlaceButtonManage : MonoBehaviour
{
    void Update()
    {
        ButtonUpdate();
        if (clickDownButton == false)
        {
            ButtonDetail();
        }
        if (upButton2.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ClickUpButton2();
            }
        }
        else if (upButton.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ClickUpButton();
            }
        }
        if (downButton2.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ClickDownButton_2();
            }
        }
        else if (downButton.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ClickDownButton_();
            }
        }
    }

    public GameObject upButton;
    public GameObject downButton;
    public GameObject buttonDetectiveOffice;
    public GameObject buttonHospital;
    public GameObject buttonNewspaper;
    public GameObject buttonQuestHouse;
    public GameObject buttonStation;
    public GameObject buttonRestaurant;
    public GameObject buttonUniversity;
    public GameObject buttonRiverside;
    public GameObject buttonBar;
    public GameObject buttonSlum;
    public GameObject buttonPoliceOffice;
    public GameObject buttonGunsmith;
    public GameObject buttonSewageMaintenanceOffice;
    public GameObject buttonWharf;
    bool isButtonDetectiveOffice = false;
    bool isButtonHospital = false;
    bool isButtonNewspaper = false;
    bool isButtonQuestHouse = false;
    bool isButtonStation = false;
    bool isButtonRestaurant = false;
    bool isButtonUniversity = false;
    bool isButtonRiverside = false;
    bool isButtonBar = false;
    bool isButtonSlum = false;
    bool isButtonPoliceOffice = false;
    bool isButtonGunsmith = false;
    bool isButtonSewageMaintenanceOffice = false;
    bool isButtonWharf = false;
    public GameObject buttonRect;
    bool clickDownButton = false;
    bool clickDownButton2 = false;
    public GameObject upButton2;
    public GameObject downButton2;
    
    void ButtonUpdate()
    {
        if ((DataBaseManager.Intel_DetectiveOffice1 == true || DataBaseManager.Intel_DetectiveOffice2 == true || DataBaseManager.Intel_DetectiveOffice3 == true || DataBaseManager.Intel_DetectiveOffice4 == true || DataBaseManager.Intel_DetectiveOffice5 == true || DataBaseManager.Intel_DetectiveOffice6 == true) && isButtonDetectiveOffice == false)
        {
            isButtonDetectiveOffice = true;
            DataBaseManager.ButtonCount_DetectiveOffice = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Hospital1 == true || DataBaseManager.Intel_Hospital2 == true || DataBaseManager.Intel_Hospital3 == true || DataBaseManager.Intel_Hospital4 == true || DataBaseManager.Intel_Hospital5 == true || DataBaseManager.Intel_Hospital6 == true) && isButtonHospital == false)
        {
            isButtonHospital = true;
            DataBaseManager.ButtonCount_Hospital = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Newspaper1 == true || DataBaseManager.Intel_Newspaper2 == true || DataBaseManager.Intel_Newspaper3 == true || DataBaseManager.Intel_Newspaper4 == true || DataBaseManager.Intel_Newspaper5 == true || DataBaseManager.Intel_Newspaper6 == true) && isButtonNewspaper == false)
        {
            isButtonNewspaper = true;
            DataBaseManager.ButtonCount_Newspaper = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_QuestHouse1 == true || DataBaseManager.Intel_QuestHouse2 == true || DataBaseManager.Intel_QuestHouse3 == true || DataBaseManager.Intel_QuestHouse4 == true || DataBaseManager.Intel_QuestHouse5 == true || DataBaseManager.Intel_QuestHouse6 == true) && isButtonQuestHouse == false)
        {
            isButtonQuestHouse = true;
            DataBaseManager.ButtonCount_QuestHouse = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Station1 == true || DataBaseManager.Intel_Station2 == true || DataBaseManager.Intel_Station3 == true || DataBaseManager.Intel_Station4 == true || DataBaseManager.Intel_Station5 == true || DataBaseManager.Intel_Station6 == true) && isButtonStation == false)
        {
            isButtonStation = true;
            DataBaseManager.ButtonCount_Station = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Sewer1 == true || DataBaseManager.Intel_Sewer2 == true || DataBaseManager.Intel_Sewer3 == true || DataBaseManager.Intel_Sewer4 == true || DataBaseManager.Intel_Sewer5 == true || DataBaseManager.Intel_Sewer6 == true) && isButtonRestaurant == false)
        {
            isButtonRestaurant = true;
            DataBaseManager.ButtonCount_Sewer = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_University1 == true || DataBaseManager.Intel_University2 == true || DataBaseManager.Intel_University3 == true || DataBaseManager.Intel_University4 == true || DataBaseManager.Intel_University5 == true || DataBaseManager.Intel_University6 == true) && isButtonUniversity == false)
        {
            isButtonUniversity = true;
            DataBaseManager.ButtonCount_University = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Riverside1 == true || DataBaseManager.Intel_Riverside2 == true || DataBaseManager.Intel_Riverside3 == true || DataBaseManager.Intel_Riverside4 == true || DataBaseManager.Intel_Riverside5 == true || DataBaseManager.Intel_Riverside6 == true) && isButtonRiverside == false)
        {
            isButtonRiverside = true;
            DataBaseManager.ButtonCount_Riverside = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Bar1 == true || DataBaseManager.Intel_Bar2 == true || DataBaseManager.Intel_Bar3 == true || DataBaseManager.Intel_Bar4 == true || DataBaseManager.Intel_Bar5 == true || DataBaseManager.Intel_Bar6 == true) && isButtonBar == false)
        {
            isButtonBar = true;
            DataBaseManager.ButtonCount_Bar = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Slum1 == true || DataBaseManager.Intel_Slum2 == true || DataBaseManager.Intel_Slum3 == true || DataBaseManager.Intel_Slum4 == true || DataBaseManager.Intel_Slum5 == true || DataBaseManager.Intel_Slum6 == true) && isButtonSlum == false)
        {
            isButtonSlum = true;
            DataBaseManager.ButtonCount_Slum = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_PoliceOffice1 == true || DataBaseManager.Intel_PoliceOffice2 == true || DataBaseManager.Intel_PoliceOffice3 == true || DataBaseManager.Intel_PoliceOffice4 == true || DataBaseManager.Intel_PoliceOffice5 == true || DataBaseManager.Intel_PoliceOffice6 == true) && isButtonPoliceOffice == false)
        {
            isButtonPoliceOffice = true;
            DataBaseManager.ButtonCount_PoliceOffice = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Gunsmith1 == true || DataBaseManager.Intel_Gunsmith2 == true || DataBaseManager.Intel_Gunsmith3 == true || DataBaseManager.Intel_Gunsmith4 == true || DataBaseManager.Intel_Gunsmith5 == true || DataBaseManager.Intel_Gunsmith6 == true) && isButtonGunsmith == false)
        {
            isButtonGunsmith = true;
            DataBaseManager.ButtonCount_Gunsmith = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_SewageMaintenanceOffice1 == true || DataBaseManager.Intel_SewageMaintenanceOffice2 == true || DataBaseManager.Intel_SewageMaintenanceOffice3 == true || DataBaseManager.Intel_SewageMaintenanceOffice4 == true || DataBaseManager.Intel_SewageMaintenanceOffice5 == true || DataBaseManager.Intel_SewageMaintenanceOffice6 == true) && isButtonSewageMaintenanceOffice == false)
        {
            isButtonSewageMaintenanceOffice = true;
            DataBaseManager.ButtonCount_SewageMaintenanceOffice = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
        if ((DataBaseManager.Intel_Wharf1 == true || DataBaseManager.Intel_Wharf2 == true || DataBaseManager.Intel_Wharf3 == true || DataBaseManager.Intel_Wharf4 == true || DataBaseManager.Intel_Wharf5 == true || DataBaseManager.Intel_Wharf6 == true) && isButtonWharf == false)
        {
            isButtonWharf = true;
            DataBaseManager.ButtonCount_Wharf = DataBaseManager.GainPlaceInt;
            DataBaseManager.GainPlaceInt += 1;
        }
    }
    void ButtonDetail()
    {
        if (DataBaseManager.ButtonCount_DetectiveOffice != -1)
        {
            buttonDetectiveOffice.SetActive(true);
            buttonDetectiveOffice.transform.SetSiblingIndex(DataBaseManager.ButtonCount_DetectiveOffice);
        }
        if (DataBaseManager.ButtonCount_Hospital != -1)
        {
            buttonHospital.SetActive(true);
            buttonHospital.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Hospital);
        }
        if (DataBaseManager.ButtonCount_Newspaper != -1)
        {
            buttonNewspaper.SetActive(true);
            buttonNewspaper.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Newspaper);
        }
        if (DataBaseManager.ButtonCount_QuestHouse != -1)
        {
            buttonQuestHouse.SetActive(true);
            buttonQuestHouse.transform.SetSiblingIndex(DataBaseManager.ButtonCount_QuestHouse);
        }
        if (DataBaseManager.ButtonCount_Station != -1)
        {
            buttonStation.SetActive(true);
            buttonStation.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Station);
        }
        if (DataBaseManager.ButtonCount_Sewer != -1)
        {
            buttonRestaurant.SetActive(true);
            buttonRestaurant.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Sewer);
        }
        if (DataBaseManager.ButtonCount_University != -1)
        {
            buttonUniversity.SetActive(true);
            buttonUniversity.transform.SetSiblingIndex(DataBaseManager.ButtonCount_University);
        }
        if (DataBaseManager.ButtonCount_Riverside != -1)
        {
            buttonRiverside.SetActive(true);
            buttonRiverside.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Riverside);
        }
        if (DataBaseManager.ButtonCount_Bar != -1)
        {
            buttonBar.SetActive(true);
            buttonBar.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Bar);
        }
        if (DataBaseManager.ButtonCount_Slum != -1)
        {
            buttonSlum.SetActive(true);
            buttonSlum.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Slum);
        }
        if (DataBaseManager.ButtonCount_PoliceOffice != -1)
        {
            buttonPoliceOffice.SetActive(true);
            buttonPoliceOffice.transform.SetSiblingIndex(DataBaseManager.ButtonCount_PoliceOffice);
        }
        if (DataBaseManager.ButtonCount_Gunsmith != -1)
        {
            buttonGunsmith.SetActive(true);
            buttonGunsmith.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Gunsmith);
        }
        if (DataBaseManager.ButtonCount_SewageMaintenanceOffice != -1)
        {
            buttonSewageMaintenanceOffice.SetActive(true);
            buttonSewageMaintenanceOffice.transform.SetSiblingIndex(DataBaseManager.ButtonCount_SewageMaintenanceOffice);
        }
        if (DataBaseManager.ButtonCount_Wharf != -1)
        {
            buttonWharf.SetActive(true);
            buttonWharf.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Wharf);
        }
        if (DataBaseManager.GainPlaceInt > 6)
        {
            downButton.SetActive(true);
        }
    }
    public void ClickUpButton()
    {
        SoundManager.Instance.ClickSound_Play();
        clickDownButton = false;
        upButton.SetActive(false);
        downButton.SetActive(true);
    }
    public void ClickUpButton2()
    {
        SoundManager.Instance.ClickSound_Play();
        upButton.SetActive(true);
        upButton2.SetActive(false);
        downButton.SetActive(true);
        downButton2.SetActive(false);
        Transform trChild16 = buttonRect.transform.GetChild(6);
        trChild16.gameObject.SetActive(true);
        Transform trChild7 = buttonRect.transform.GetChild(7);
        trChild7.gameObject.SetActive(true);
        Transform trChild9 = buttonRect.transform.GetChild(8);
        trChild9.gameObject.SetActive(true);
        Transform trChild10 = buttonRect.transform.GetChild(9);
        trChild10.gameObject.SetActive(true);
        Transform trChild11 = buttonRect.transform.GetChild(10);
        trChild11.gameObject.SetActive(true);
        Transform trChild12 = buttonRect.transform.GetChild(11);
        trChild12.gameObject.SetActive(true);
    }
    public void ClickDownButton_()
    {
        SoundManager.Instance.ClickSound_Play();
        upButton.SetActive(true);
        downButton.SetActive(false);
        clickDownButton = true;
        for (int i = 0; i < 6; i++)
        {
            Transform trChild = buttonRect.transform.GetChild(i);
            trChild.gameObject.SetActive(false);
        }
        if (DataBaseManager.GainPlaceInt == 7)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
        }
        else if (DataBaseManager.GainPlaceInt == 8)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = buttonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
        }
        else if (DataBaseManager.GainPlaceInt == 9)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = buttonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild9 = buttonRect.transform.GetChild(8);
            trChild9.gameObject.SetActive(true);
        }
        else if (DataBaseManager.GainPlaceInt == 10)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = buttonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild9 = buttonRect.transform.GetChild(8);
            trChild9.gameObject.SetActive(true);
            Transform trChild10 = buttonRect.transform.GetChild(9);
            trChild10.gameObject.SetActive(true);
        }
        else if (DataBaseManager.GainPlaceInt == 11)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = buttonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild9 = buttonRect.transform.GetChild(8);
            trChild9.gameObject.SetActive(true);
            Transform trChild10 = buttonRect.transform.GetChild(9);
            trChild10.gameObject.SetActive(true);
            Transform trChild11 = buttonRect.transform.GetChild(10);
            trChild11.gameObject.SetActive(true);
        }
        else if (DataBaseManager.GainPlaceInt == 12)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = buttonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild9 = buttonRect.transform.GetChild(8);
            trChild9.gameObject.SetActive(true);
            Transform trChild10 = buttonRect.transform.GetChild(9);
            trChild10.gameObject.SetActive(true);
            Transform trChild11 = buttonRect.transform.GetChild(10);
            trChild11.gameObject.SetActive(true);
            Transform trChild12 = buttonRect.transform.GetChild(11);
            trChild12.gameObject.SetActive(true);
        }
        if (DataBaseManager.GainPlaceInt > 12)
        {
            downButton2.SetActive(true);
        }
    }
    public void ClickDownButton_2()
    {
        SoundManager.Instance.ClickSound_Play();
        upButton.SetActive(false);
        upButton2.SetActive(true);
        downButton.SetActive(false);
        downButton2.SetActive(false);

        clickDownButton2 = true;
        for (int i = 0; i < 12; i++)
        {
            Transform trChild = buttonRect.transform.GetChild(i);
            trChild.gameObject.SetActive(false);
        }
        if (DataBaseManager.GainPlaceInt == 13)
        {
            Transform trChild12 = buttonRect.transform.GetChild(12);
            trChild12.gameObject.SetActive(true);
        }
    }
}
