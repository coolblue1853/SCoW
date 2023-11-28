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
        if ((DataBaseManager.intelElla1 == true || DataBaseManager.intelElla2 == true || DataBaseManager.intelElla3 == true || DataBaseManager.intelElla4 == true || DataBaseManager.intelElla5 == true || DataBaseManager.intelElla6 == true) && isButtonElla == false)
        {
            isButtonElla = true;
            DataBaseManager.buttonCountElla = DataBaseManager.gainCharacterInt;
            DataBaseManager.gainCharacterInt += 1;
        }
        if ((DataBaseManager.intelAiden1 == true || DataBaseManager.intelAiden2 == true || DataBaseManager.intelAiden3 == true || DataBaseManager.intelAiden4 == true || DataBaseManager.intelAiden5 == true || DataBaseManager.intelAiden6 == true) && isButtonAiden == false)
        {
            isButtonAiden = true;
            DataBaseManager.buttonCountAiden = DataBaseManager.gainCharacterInt;
            DataBaseManager.gainCharacterInt += 1;
        }
        if ((DataBaseManager.intelCane1 == true || DataBaseManager.intelCane2 == true || DataBaseManager.intelCane3 == true || DataBaseManager.intelCane4 == true || DataBaseManager.intelCane5 == true || DataBaseManager.intelCane6 == true) && isButtonCane == false)
        {
            isButtonCane = true;
            DataBaseManager.buttonCountCane = DataBaseManager.gainCharacterInt;
            DataBaseManager.gainCharacterInt += 1;
        }
        if ((DataBaseManager.intelAlbert1 == true || DataBaseManager.intelAlbert2 == true || DataBaseManager.intelAlbert3 == true || DataBaseManager.intelAlbert4 == true || DataBaseManager.intelAlbert5 == true || DataBaseManager.intelAlbert6 == true) && isButtonAlbert == false)
        {
            isButtonAlbert = true;
            DataBaseManager.buttonCountAlbert = DataBaseManager.gainCharacterInt;
            DataBaseManager.gainCharacterInt += 1;
        }
        if ((DataBaseManager.intelMeiv1 == true || DataBaseManager.intelMeiv2 == true || DataBaseManager.intelMeiv3 == true || DataBaseManager.intelMeiv4 == true || DataBaseManager.intelMeiv5 == true || DataBaseManager.intelMeiv6 == true) && isButtonMeiv == false)
        {
            isButtonMeiv = true;
            DataBaseManager.buttonCountMeiv = DataBaseManager.gainCharacterInt;
            DataBaseManager.gainCharacterInt += 1;
        }
        if ((DataBaseManager.intelSwain1 == true || DataBaseManager.intelSwain2 == true || DataBaseManager.intelSwain3 == true || DataBaseManager.intelSwain4 == true || DataBaseManager.intelSwain5 == true || DataBaseManager.intelSwain6 == true) && isButtonSwain == false)
        {
            isButtonSwain = true;
            DataBaseManager.buttonCountSwain = DataBaseManager.gainCharacterInt;
            DataBaseManager.gainCharacterInt += 1;
        }
        if ((DataBaseManager.intelBlackWell1 == true || DataBaseManager.intelBlackWell2 == true || DataBaseManager.intelBlackWell3 == true || DataBaseManager.intelBlackWell4 == true || DataBaseManager.intelBlackWell5 == true || DataBaseManager.intelBlackWell5 == true) && isButtonBlackWell == false)
        {
            isButtonBlackWell = true;
            DataBaseManager.buttonCountBlackWell = DataBaseManager.gainCharacterInt;
            DataBaseManager.gainCharacterInt += 1;
        }
        if ((DataBaseManager.intelSewerWorker1 == true || DataBaseManager.intelSewerWorker2 == true || DataBaseManager.intelSewerWorker3 == true || DataBaseManager.intelSewerWorker4 == true || DataBaseManager.intelSewerWorker5 == true || DataBaseManager.intelSewerWorker6 == true) && isButtonSewerWorker == false)
        {
            isButtonSewerWorker = true;
            DataBaseManager.buttonCountSewerWorker = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }
        if ((DataBaseManager.intelDave1 == true || DataBaseManager.intelDave2 == true || DataBaseManager.intelDave3 == true || DataBaseManager.intelDave4 == true || DataBaseManager.intelDave5 == true || DataBaseManager.intelDave6 == true) && isButtonDave == false)
        {
            isButtonDave = true;
            DataBaseManager.buttonCountDave = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }
    }
    void ButtonManage()
    {
        if (DataBaseManager.buttonCountAiden != -1)
        {
            buttonAiden.SetActive(true);
            buttonAiden.transform.SetSiblingIndex(DataBaseManager.buttonCountAiden);
        }
        if (DataBaseManager.buttonCountElla != -1)
        {
            buttonElla.SetActive(true);
            buttonElla.transform.SetSiblingIndex(DataBaseManager.buttonCountElla);
        }
        if (DataBaseManager.buttonCountCane != -1)
        {
            buttonCane.SetActive(true);
            buttonCane.transform.SetSiblingIndex(DataBaseManager.buttonCountCane);
        }
        if (DataBaseManager.buttonCountMeiv != -1)
        {
            buttonMeiv.SetActive(true);
            buttonMeiv.transform.SetSiblingIndex(DataBaseManager.buttonCountMeiv);
        }
        if (DataBaseManager.buttonCountAlbert != -1)
        {
            buttonAlbert.SetActive(true);
            buttonAlbert.transform.SetSiblingIndex(DataBaseManager.buttonCountAlbert);
        }
        if (DataBaseManager.buttonCountBlackWell != -1)
        {
            buttonBlackWell.SetActive(true);
            buttonBlackWell.transform.SetSiblingIndex(DataBaseManager.buttonCountBlackWell);
        }
        if (DataBaseManager.buttonCountSwain != -1)
        {
            buttonSwain.SetActive(true);
            buttonSwain.transform.SetSiblingIndex(DataBaseManager.buttonCountSwain);
        }
        if (DataBaseManager.gainCharacterInt > 6)
        {
            downButton.SetActive(true);
        }
        if (DataBaseManager.buttonCountSewerWorker != -1)
        {
            buttonSewerWorker.SetActive(true);
            buttonSewerWorker.transform.SetSiblingIndex(DataBaseManager.buttonCountSewerWorker);
        }
        if (DataBaseManager.buttonCountDave != -1)
        {
            buttonDave.SetActive(true);
            buttonDave.transform.SetSiblingIndex(DataBaseManager.buttonCountDave);
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

        if (DataBaseManager.gainCharacterInt == 7)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);

        }
    }
}
