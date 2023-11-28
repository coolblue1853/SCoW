using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterButtonManage : MonoBehaviour
{

    void Update()
    {
        ButtonUpdate();
        if (ispushDownButton == false)
        {
            ButtonManage();
        }
        if (upButton.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ClickUPButton();
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
    public GameObject buttonElla;
    public GameObject buttonAiden;
    public GameObject buttonCane;
    public GameObject buttonMeiv;
    public GameObject buttonAlbert;
    public GameObject buttonBlackWell;
    public GameObject buttonSwain;
    public GameObject buttonSewerWorker;
    public GameObject buttonDave;
    public GameObject buttonRect;
    bool ispushDownButton = false;
    bool isButtonElla = false;
    bool isButtonAiden = false;
    bool isButtonCane = false;
    bool isButtonAlbert = false;
    bool isButtonMeiv = false;
    bool isButtonBlackWell = false;
    bool isButtonSwain = false;
    bool isButtonSewerWorker = false;
    bool isButtonDave = false;
    
    void ButtonUpdate()
    {
        if ((DataBaseManager.Intel_Ella1 == true || DataBaseManager.Intel_Ella2 == true || DataBaseManager.Intel_Ella3 == true || DataBaseManager.Intel_Ella4 == true || DataBaseManager.Intel_Ella5 == true || DataBaseManager.Intel_Ella6 == true) && isButtonElla == false)
        {
            isButtonElla = true;
            DataBaseManager.ButtonCount_Ella = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_Aiden1 == true || DataBaseManager.Intel_Aiden2 == true || DataBaseManager.Intel_Aiden3 == true || DataBaseManager.Intel_Aiden4 == true || DataBaseManager.Intel_Aiden5 == true || DataBaseManager.Intel_Aiden6 == true) && isButtonAiden == false)
        {
            isButtonAiden = true;
            DataBaseManager.ButtonCount_Aiden = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_Cane1 == true || DataBaseManager.Intel_Cane2 == true || DataBaseManager.Intel_Cane3 == true || DataBaseManager.Intel_Cane4 == true || DataBaseManager.Intel_Cane5 == true || DataBaseManager.Intel_Cane6 == true) && isButtonCane == false)
        {
            isButtonCane = true;
            DataBaseManager.ButtonCount_Cane = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_Albert1 == true || DataBaseManager.Intel_Albert2 == true || DataBaseManager.Intel_Albert3 == true || DataBaseManager.Intel_Albert4 == true || DataBaseManager.Intel_Albert5 == true || DataBaseManager.Intel_Albert6 == true) && isButtonAlbert == false)
        {
            isButtonAlbert = true;
            DataBaseManager.ButtonCount_Albert = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_Meiv1 == true || DataBaseManager.Intel_Meiv2 == true || DataBaseManager.Intel_Meiv3 == true || DataBaseManager.Intel_Meiv4 == true || DataBaseManager.Intel_Meiv5 == true || DataBaseManager.Intel_Meiv6 == true) && isButtonMeiv == false)
        {
            isButtonMeiv = true;
            DataBaseManager.ButtonCount_Meiv = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_Swain1 == true || DataBaseManager.Intel_Swain2 == true || DataBaseManager.Intel_Swain3 == true || DataBaseManager.Intel_Swain4 == true || DataBaseManager.Intel_Swain5 == true || DataBaseManager.Intel_Swain6 == true) && isButtonSwain == false)
        {
            isButtonSwain = true;
            DataBaseManager.ButtonCount_Swain = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_BlackWell1 == true || DataBaseManager.Intel_BlackWell2 == true || DataBaseManager.Intel_BlackWell3 == true || DataBaseManager.Intel_BlackWell4 == true || DataBaseManager.Intel_BlackWell5 == true || DataBaseManager.Intel_BlackWell5 == true) && isButtonBlackWell == false)
        {
            isButtonBlackWell = true;
            DataBaseManager.ButtonCount_BlackWell = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_SewerWorker1 == true || DataBaseManager.Intel_SewerWorker2 == true || DataBaseManager.Intel_SewerWorker3 == true || DataBaseManager.Intel_SewerWorker4 == true || DataBaseManager.Intel_SewerWorker5 == true || DataBaseManager.Intel_SewerWorker6 == true) && isButtonSewerWorker == false)
        {
            isButtonSewerWorker = true;
            DataBaseManager.ButtonCount_SewerWorker = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_Dave1 == true || DataBaseManager.Intel_Dave2 == true || DataBaseManager.Intel_Dave3 == true || DataBaseManager.Intel_Dave4 == true || DataBaseManager.Intel_Dave5 == true || DataBaseManager.Intel_Dave6 == true) && isButtonDave == false)
        {
            isButtonDave = true;
            DataBaseManager.ButtonCount_Dave = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
    }
    void ButtonManage()
    {
        if (DataBaseManager.ButtonCount_Aiden != -1)
        {
            buttonAiden.SetActive(true);
            buttonAiden.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Aiden);
        }
        if (DataBaseManager.ButtonCount_Ella != -1)
        {
            buttonElla.SetActive(true);
            buttonElla.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Ella);
        }
        if (DataBaseManager.ButtonCount_Cane != -1)
        {
            buttonCane.SetActive(true);
            buttonCane.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Cane);
        }
        if (DataBaseManager.ButtonCount_Meiv != -1)
        {
            buttonMeiv.SetActive(true);
            buttonMeiv.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Meiv);
        }
        if (DataBaseManager.ButtonCount_Albert != -1)
        {
            buttonAlbert.SetActive(true);
            buttonAlbert.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Albert);
        }
        if (DataBaseManager.ButtonCount_BlackWell != -1)
        {
            buttonBlackWell.SetActive(true);
            buttonBlackWell.transform.SetSiblingIndex(DataBaseManager.ButtonCount_BlackWell);
        }
        if (DataBaseManager.ButtonCount_Swain != -1)
        {
            buttonSwain.SetActive(true);
            buttonSwain.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Swain);
        }
        if (DataBaseManager.GainCharacterInt > 6)
        {
            downButton.SetActive(true);
        }
        if (DataBaseManager.ButtonCount_SewerWorker != -1)
        {
            buttonSewerWorker.SetActive(true);
            buttonSewerWorker.transform.SetSiblingIndex(DataBaseManager.ButtonCount_SewerWorker);
        }
        if (DataBaseManager.ButtonCount_Dave != -1)
        {
            buttonDave.SetActive(true);
            buttonDave.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Dave);
        }
    }
    public void ClickUPButton()
    {
        ispushDownButton = false;
        upButton.SetActive(false);
        downButton.SetActive(true);
    }
    public void ClickDownButton()
    {
        upButton.SetActive(true);
        downButton.SetActive(false);
        ispushDownButton = true;
        for (int i = 0; i < 6; i++)
        {
            Transform trChild = buttonRect.transform.GetChild(i);
            trChild.gameObject.SetActive(false);
        }

        if (DataBaseManager.GainCharacterInt == 7)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);

        }
    }
}
