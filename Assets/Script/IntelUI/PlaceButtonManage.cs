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
                ClickDownButton2();
            }
        }
        else if (downButton.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ClickDownButton();
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
        if ((DataBaseManager.intelDetectiveOffice1 == true || DataBaseManager.intelDetectiveOffice2 == true || DataBaseManager.intelDetectiveOffice3 == true || DataBaseManager.intelDetectiveOffice4 == true || DataBaseManager.intelDetectiveOffice5 == true || DataBaseManager.intelDetectiveOffice6 == true) && isButtonDetectiveOffice == false)
        {
            isButtonDetectiveOffice = true;
            DataBaseManager.buttonCountDetectiveOffice = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelHospital1 == true || DataBaseManager.intelHospital2 == true || DataBaseManager.intelHospital3 == true || DataBaseManager.intelHospital4 == true || DataBaseManager.intelHospital5 == true || DataBaseManager.intelHospital6 == true) && isButtonHospital == false)
        {
            isButtonHospital = true;
            DataBaseManager.buttonCountHospital = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelNewspaper1 == true || DataBaseManager.intelNewspaper2 == true || DataBaseManager.intelNewspaper3 == true || DataBaseManager.intelNewspaper4 == true || DataBaseManager.intelNewspaper5 == true || DataBaseManager.intelNewspaper6 == true) && isButtonNewspaper == false)
        {
            isButtonNewspaper = true;
            DataBaseManager.buttonCountNewspaper = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelQuestHouse1 == true || DataBaseManager.intelQuestHouse2 == true || DataBaseManager.intelQuestHouse3 == true || DataBaseManager.intelQuestHouse4 == true || DataBaseManager.intelQuestHouse5 == true || DataBaseManager.intelQuestHouse6 == true) && isButtonQuestHouse == false)
        {
            isButtonQuestHouse = true;
            DataBaseManager.buttonCountQuestHouse = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelStation1 == true || DataBaseManager.intelStation2 == true || DataBaseManager.intelStation3 == true || DataBaseManager.intelStation4 == true || DataBaseManager.intelStation5 == true || DataBaseManager.intelStation6 == true) && isButtonStation == false)
        {
            isButtonStation = true;
            DataBaseManager.buttonCountStation = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelSewer1 == true || DataBaseManager.intelSewer2 == true || DataBaseManager.intelSewer3 == true || DataBaseManager.intelSewer4 == true || DataBaseManager.intelSewer5 == true || DataBaseManager.intelSewer6 == true) && isButtonRestaurant == false)
        {
            isButtonRestaurant = true;
            DataBaseManager.buttonCountSewer = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelUniversity1 == true || DataBaseManager.intelUniversity2 == true || DataBaseManager.intelUniversity3 == true || DataBaseManager.intelUniversity4 == true || DataBaseManager.intelUniversity5 == true || DataBaseManager.intelUniversity6 == true) && isButtonUniversity == false)
        {
            isButtonUniversity = true;
            DataBaseManager.buttonCountUniversity = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelRiverside1 == true || DataBaseManager.intelRiverside2 == true || DataBaseManager.intelRiverside3 == true || DataBaseManager.intelRiverside4 == true || DataBaseManager.intelRiverside5 == true || DataBaseManager.intelRiverside6 == true) && isButtonRiverside == false)
        {
            isButtonRiverside = true;
            DataBaseManager.buttonCountRiverside = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelBar1 == true || DataBaseManager.intelBar2 == true || DataBaseManager.intelBar3 == true || DataBaseManager.intelBar4 == true || DataBaseManager.intelBar5 == true || DataBaseManager.intelBar6 == true) && isButtonBar == false)
        {
            isButtonBar = true;
            DataBaseManager.buttonCountBar = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelSlum1 == true || DataBaseManager.intelSlum2 == true || DataBaseManager.intelSlum3 == true || DataBaseManager.intelSlum4 == true || DataBaseManager.intelSlum5 == true || DataBaseManager.intelSlum6 == true) && isButtonSlum == false)
        {
            isButtonSlum = true;
            DataBaseManager.buttonCountSlum = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelPoliceOffice1 == true || DataBaseManager.intelPoliceOffice2 == true || DataBaseManager.intelPoliceOffice3 == true || DataBaseManager.intelPoliceOffice4 == true || DataBaseManager.intelPoliceOffice5 == true || DataBaseManager.intelPoliceOffice6 == true) && isButtonPoliceOffice == false)
        {
            isButtonPoliceOffice = true;
            DataBaseManager.buttonCountPoliceOffice = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelGunsmith1 == true || DataBaseManager.intelGunsmith2 == true || DataBaseManager.intelGunsmith3 == true || DataBaseManager.intelGunsmith4 == true || DataBaseManager.intelGunsmith5 == true || DataBaseManager.intelGunsmith6 == true) && isButtonGunsmith == false)
        {
            isButtonGunsmith = true;
            DataBaseManager.buttonCountGunsmith = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelSewageMaintenanceOffice1 == true || DataBaseManager.intelSewageMaintenanceOffice2 == true || DataBaseManager.intelSewageMaintenanceOffice3 == true || DataBaseManager.intelSewageMaintenanceOffice4 == true || DataBaseManager.intelSewageMaintenanceOffice5 == true || DataBaseManager.intelSewageMaintenanceOffice6 == true) && isButtonSewageMaintenanceOffice == false)
        {
            isButtonSewageMaintenanceOffice = true;
            DataBaseManager.buttonCountSewageMaintenanceOffice = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
        if ((DataBaseManager.intelWharf1 == true || DataBaseManager.intelWharf2 == true || DataBaseManager.intelWharf3 == true || DataBaseManager.intelWharf4 == true || DataBaseManager.intelWharf5 == true || DataBaseManager.intelWharf6 == true) && isButtonWharf == false)
        {
            isButtonWharf = true;
            DataBaseManager.buttonCountWharf = DataBaseManager.gainPlaceInt;
            DataBaseManager.gainPlaceInt += 1;
        }
    }
    void ButtonDetail()
    {
        if (DataBaseManager.buttonCountDetectiveOffice != -1)
        {
            buttonDetectiveOffice.SetActive(true);
            buttonDetectiveOffice.transform.SetSiblingIndex(DataBaseManager.buttonCountDetectiveOffice);
        }
        if (DataBaseManager.buttonCountHospital != -1)
        {
            buttonHospital.SetActive(true);
            buttonHospital.transform.SetSiblingIndex(DataBaseManager.buttonCountHospital);
        }
        if (DataBaseManager.buttonCountNewspaper != -1)
        {
            buttonNewspaper.SetActive(true);
            buttonNewspaper.transform.SetSiblingIndex(DataBaseManager.buttonCountNewspaper);
        }
        if (DataBaseManager.buttonCountQuestHouse != -1)
        {
            buttonQuestHouse.SetActive(true);
            buttonQuestHouse.transform.SetSiblingIndex(DataBaseManager.buttonCountQuestHouse);
        }
        if (DataBaseManager.buttonCountStation != -1)
        {
            buttonStation.SetActive(true);
            buttonStation.transform.SetSiblingIndex(DataBaseManager.buttonCountStation);
        }
        if (DataBaseManager.buttonCountSewer != -1)
        {
            buttonRestaurant.SetActive(true);
            buttonRestaurant.transform.SetSiblingIndex(DataBaseManager.buttonCountSewer);
        }
        if (DataBaseManager.buttonCountUniversity != -1)
        {
            buttonUniversity.SetActive(true);
            buttonUniversity.transform.SetSiblingIndex(DataBaseManager.buttonCountUniversity);
        }
        if (DataBaseManager.buttonCountRiverside != -1)
        {
            buttonRiverside.SetActive(true);
            buttonRiverside.transform.SetSiblingIndex(DataBaseManager.buttonCountRiverside);
        }
        if (DataBaseManager.buttonCountBar != -1)
        {
            buttonBar.SetActive(true);
            buttonBar.transform.SetSiblingIndex(DataBaseManager.buttonCountBar);
        }
        if (DataBaseManager.buttonCountSlum != -1)
        {
            buttonSlum.SetActive(true);
            buttonSlum.transform.SetSiblingIndex(DataBaseManager.buttonCountSlum);
        }
        if (DataBaseManager.buttonCountPoliceOffice != -1)
        {
            buttonPoliceOffice.SetActive(true);
            buttonPoliceOffice.transform.SetSiblingIndex(DataBaseManager.buttonCountPoliceOffice);
        }
        if (DataBaseManager.buttonCountGunsmith != -1)
        {
            buttonGunsmith.SetActive(true);
            buttonGunsmith.transform.SetSiblingIndex(DataBaseManager.buttonCountGunsmith);
        }
        if (DataBaseManager.buttonCountSewageMaintenanceOffice != -1)
        {
            buttonSewageMaintenanceOffice.SetActive(true);
            buttonSewageMaintenanceOffice.transform.SetSiblingIndex(DataBaseManager.buttonCountSewageMaintenanceOffice);
        }
        if (DataBaseManager.buttonCountWharf != -1)
        {
            buttonWharf.SetActive(true);
            buttonWharf.transform.SetSiblingIndex(DataBaseManager.buttonCountWharf);
        }
        if (DataBaseManager.gainPlaceInt > 6)
        {
            downButton.SetActive(true);
        }
    }
    public void ClickUpButton()
    {
        SoundManager.Instance.ClickSoundPlay();
        clickDownButton = false;
        upButton.SetActive(false);
        downButton.SetActive(true);
    }
    public void ClickUpButton2()
    {
        SoundManager.Instance.ClickSoundPlay();
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
    public void ClickDownButton()
    {
        SoundManager.Instance.ClickSoundPlay();
        upButton.SetActive(true);
        downButton.SetActive(false);
        clickDownButton = true;
        for (int i = 0; i < 6; i++)
        {
            Transform trChild = buttonRect.transform.GetChild(i);
            trChild.gameObject.SetActive(false);
        }
        if (DataBaseManager.gainPlaceInt == 7)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
        }
        else if (DataBaseManager.gainPlaceInt == 8)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = buttonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
        }
        else if (DataBaseManager.gainPlaceInt == 9)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = buttonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild9 = buttonRect.transform.GetChild(8);
            trChild9.gameObject.SetActive(true);
        }
        else if (DataBaseManager.gainPlaceInt == 10)
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
        else if (DataBaseManager.gainPlaceInt == 11)
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
        else if (DataBaseManager.gainPlaceInt == 12)
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
        if (DataBaseManager.gainPlaceInt > 12)
        {
            downButton2.SetActive(true);
        }
    }
    public void ClickDownButton2()
    {
        SoundManager.Instance.ClickSoundPlay();
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
        if (DataBaseManager.gainPlaceInt == 13)
        {
            Transform trChild12 = buttonRect.transform.GetChild(12);
            trChild12.gameObject.SetActive(true);
        }
    }
}
