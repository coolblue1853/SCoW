using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterButtonManage : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        ButtonUpdate();
        if (PushDownButton == false)
        {
            ButtonManage();
        }

        if (UpButton.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ClickUPButton();
            }
        }


        else if (DownButton.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ClickDownButton();
            }
        }

    }

    public GameObject UpButton;
    public GameObject DownButton;



    public GameObject Button_Ella;
    public GameObject Button_Aiden;
    public GameObject Button_Cane;
    public GameObject Button_Meiv;
    public GameObject Button_Albert;
    public GameObject Button_BlackWell;
    public GameObject Button_Swain;
    public GameObject Button_SewerWorker;
    public GameObject Button_Dave;

    bool ButtonRize_Ella = false;
    bool ButtonRize_Aiden = false;
    bool ButtonRize_Cane = false;
    bool ButtonRize_Albert = false;
    bool ButtonRize_Meiv = false;
    bool ButtonRize_BlackWell = false;
    bool ButtonRize_Swain = false;
    bool ButtonRize_SewerWorker = false;
    bool ButtonRize_Dave = false;
    void ButtonUpdate()
    {

        if ((DataBaseManager.Intel_Ella1 == true || DataBaseManager.Intel_Ella2 == true || DataBaseManager.Intel_Ella3 == true || DataBaseManager.Intel_Ella4 == true || DataBaseManager.Intel_Ella5 == true || DataBaseManager.Intel_Ella6 == true) && ButtonRize_Ella == false)
        {
            ButtonRize_Ella = true;
            DataBaseManager.ButtonCount_Ella = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_Aiden1 == true || DataBaseManager.Intel_Aiden2 == true || DataBaseManager.Intel_Aiden3 == true || DataBaseManager.Intel_Aiden4 == true || DataBaseManager.Intel_Aiden5 == true || DataBaseManager.Intel_Aiden6 == true) && ButtonRize_Aiden == false)
        {
            ButtonRize_Aiden = true;
            DataBaseManager.ButtonCount_Aiden = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }


        if ((DataBaseManager.Intel_Cane1 == true || DataBaseManager.Intel_Cane2 == true || DataBaseManager.Intel_Cane3 == true || DataBaseManager.Intel_Cane4 == true || DataBaseManager.Intel_Cane5 == true || DataBaseManager.Intel_Cane6 == true) && ButtonRize_Cane == false)
        {
            ButtonRize_Cane = true;
            DataBaseManager.ButtonCount_Cane = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_Albert1 == true || DataBaseManager.Intel_Albert2 == true || DataBaseManager.Intel_Albert3 == true || DataBaseManager.Intel_Albert4 == true || DataBaseManager.Intel_Albert5 == true || DataBaseManager.Intel_Albert6 == true) && ButtonRize_Albert == false)
        {
            ButtonRize_Albert = true;
            DataBaseManager.ButtonCount_Albert = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_Meiv1 == true || DataBaseManager.Intel_Meiv2 == true || DataBaseManager.Intel_Meiv3 == true || DataBaseManager.Intel_Meiv4 == true || DataBaseManager.Intel_Meiv5 == true || DataBaseManager.Intel_Meiv6 == true) && ButtonRize_Meiv == false)
        {
            ButtonRize_Meiv = true;
            DataBaseManager.ButtonCount_Meiv = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_Swain1 == true || DataBaseManager.Intel_Swain2 == true || DataBaseManager.Intel_Swain3 == true || DataBaseManager.Intel_Swain4 == true || DataBaseManager.Intel_Swain5 == true || DataBaseManager.Intel_Swain6 == true) && ButtonRize_Swain == false)
        {
            ButtonRize_Swain = true;
            DataBaseManager.ButtonCount_Swain = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_BlackWell1 == true || DataBaseManager.Intel_BlackWell2 == true || DataBaseManager.Intel_BlackWell3 == true || DataBaseManager.Intel_BlackWell4 == true || DataBaseManager.Intel_BlackWell5 == true || DataBaseManager.Intel_BlackWell5 == true) && ButtonRize_BlackWell == false)
        {
            ButtonRize_BlackWell = true;
            DataBaseManager.ButtonCount_BlackWell = DataBaseManager.GainCharacterInt;
            DataBaseManager.GainCharacterInt += 1;
        }
        if ((DataBaseManager.Intel_SewerWorker1 == true || DataBaseManager.Intel_SewerWorker2 == true || DataBaseManager.Intel_SewerWorker3 == true || DataBaseManager.Intel_SewerWorker4 == true || DataBaseManager.Intel_SewerWorker5 == true || DataBaseManager.Intel_SewerWorker6 == true) && ButtonRize_SewerWorker == false)
        {
            ButtonRize_SewerWorker = true;
            DataBaseManager.ButtonCount_SewerWorker = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }

        if ((DataBaseManager.Intel_Dave1 == true || DataBaseManager.Intel_Dave2 == true || DataBaseManager.Intel_Dave3 == true || DataBaseManager.Intel_Dave4 == true || DataBaseManager.Intel_Dave5 == true || DataBaseManager.Intel_Dave6 == true) && ButtonRize_Dave == false)
        {
            ButtonRize_Dave = true;
            DataBaseManager.ButtonCount_Dave = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }

    }



    void ButtonManage()
    {


        if (DataBaseManager.ButtonCount_Aiden != -1)
        {
            Button_Aiden.SetActive(true);
            Button_Aiden.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Aiden);
        }
        if (DataBaseManager.ButtonCount_Ella != -1)
        {
            Button_Ella.SetActive(true);
            Button_Ella.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Ella);
        }


        if (DataBaseManager.ButtonCount_Cane != -1)
        {
            Button_Cane.SetActive(true);
            Button_Cane.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Cane);
        }
        if (DataBaseManager.ButtonCount_Meiv != -1)
        {
            Button_Meiv.SetActive(true);
            Button_Meiv.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Meiv);
        }
        if (DataBaseManager.ButtonCount_Albert != -1)
        {
            Button_Albert.SetActive(true);
            Button_Albert.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Albert);
        }
        if (DataBaseManager.ButtonCount_BlackWell != -1)
        {
            Button_BlackWell.SetActive(true);
            Button_BlackWell.transform.SetSiblingIndex(DataBaseManager.ButtonCount_BlackWell);
        }
        if (DataBaseManager.ButtonCount_Swain != -1)
        {
            Button_Swain.SetActive(true);
            Button_Swain.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Swain);
        }
        if (DataBaseManager.GainCharacterInt > 6)
        {
            DownButton.SetActive(true);
        }
        if (DataBaseManager.ButtonCount_SewerWorker != -1)
        {
            Button_SewerWorker.SetActive(true);
            Button_SewerWorker.transform.SetSiblingIndex(DataBaseManager.ButtonCount_SewerWorker);
        }
        if (DataBaseManager.ButtonCount_Dave != -1)
        {
            Button_Dave.SetActive(true);
            Button_Dave.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Dave);
        }
    }

    public GameObject ButtonRect;
    bool PushDownButton = false;
    public void ClickUPButton()
    {
        PushDownButton = false;
        UpButton.SetActive(false);
        DownButton.SetActive(true);
    }

    public void ClickDownButton()
    {
        UpButton.SetActive(true);
        DownButton.SetActive(false);
        PushDownButton = true;
        for (int i = 0; i < 6; i++)
        {
            Transform trChild = ButtonRect.transform.GetChild(i);
            trChild.gameObject.SetActive(false);
        }

        if (DataBaseManager.GainCharacterInt == 7)
        {
            Transform trChild16 = ButtonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);

        }
        /*
        else if (DataBaseManager.현재획득한인물정보가지수 == 8)
        {
            Transform trC1hild6 = 버튼랙트.transform.GetChild(6);
            trC1hild6.gameObject.SetActive(true);
            Transform trC2hild7 = 버튼랙트.transform.GetChild(7);
            trC2hild7.gameObject.SetActive(true);
            Transform trChild8 = 버튼랙트.transform.GetChild(8);
            trChild8.gameObject.SetActive(false);
        }
        else if (DataBaseManager.현재획득한인물정보가지수 == 9)
        {
            Transform trChild6 = 버튼랙트.transform.GetChild(6);
            trChild6.gameObject.SetActive(true);
            Transform trChild7 = 버튼랙트.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild8 = 버튼랙트.transform.GetChild(8);
            trChild8.gameObject.SetActive(true);
        }
        */

    }
}
