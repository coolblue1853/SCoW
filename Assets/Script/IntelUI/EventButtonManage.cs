using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventButtonManage : MonoBehaviour
{

    void Update()
    {
        ButtonUpdate();
        if (ispushDownButton == false)
        {
            DetailUpdate();
        }

         if (upButton.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ClickUpButton();
            }
        }
         if (downButton.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ClickDownButton();
            }
        }
    }
    public GameObject upButton;
    public GameObject downButton;
    public GameObject buttonPlanetarySequence;
    public GameObject buttonNightmare;
    public GameObject buttonParanoia;
    public GameObject buttonInsomnia;
    public GameObject buttonSewerGhostStory;
    public GameObject buttonMissingPeople;
    public GameObject buttonFishySmell;
    public GameObject buttonRiverWaterQuality;
    public GameObject buttonCreepyEyes;
    public GameObject buttonSafe;
    public GameObject buttonMurderCase;
    public GameObject buttonRect;
    bool ispushDownButton = false;
    bool isButtonPlanetarySequence = false;
    bool isButtonNightmare = false;
    bool isButtonParanoia = false;
    bool isButtonInsomnia = false;
    bool isButtonSewerGhostStory = false;
    bool isButtonMissingPeople = false;
    bool isButtonFishySmell = false;
    bool isButtonRiverWaterQuality = false;
    bool isButtonCreepyEyes = false;
    bool isButtonSafe = false;
    bool isButtonMurderCase = false;

    void ButtonUpdate()
    {
        if ((DataBaseManager.intelPlanetaryParade1 == true || DataBaseManager.intelPlanetaryParade2 == true || DataBaseManager.intelPlanetaryParade3 == true || DataBaseManager.intelPlanetaryParade4 == true || DataBaseManager.intelPlanetaryParade5 == true || DataBaseManager.intelPlanetaryParade6 == true) && isButtonPlanetarySequence == false)
        {
            isButtonPlanetarySequence = true;
            DataBaseManager.buttonCountPlanetaryParade = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }
        if ((DataBaseManager.intelNightmare1 == true || DataBaseManager.intelNightmare2 == true || DataBaseManager.intelNightmare3 == true || DataBaseManager.intelNightmare4 == true || DataBaseManager.intelNightmare5 == true || DataBaseManager.intelNightmare6 == true) && isButtonNightmare == false)
        {
            isButtonNightmare = true;
            DataBaseManager.buttonCountNightmare = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }
        if ((DataBaseManager.intelParanoia1 == true || DataBaseManager.intelParanoia2 == true || DataBaseManager.intelParanoia3 == true || DataBaseManager.intelParanoia4 == true || DataBaseManager.intelParanoia5 == true || DataBaseManager.intelParanoia6 == true) && isButtonParanoia == false)
        {
            isButtonParanoia = true;
            DataBaseManager.buttonCountParanoia = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }
        if ((DataBaseManager.intelInsomnia1 == true || DataBaseManager.intelInsomnia2 == true || DataBaseManager.intelInsomnia3 == true || DataBaseManager.intelInsomnia4 == true || DataBaseManager.intelInsomnia5 == true || DataBaseManager.intelInsomnia6 == true) && isButtonInsomnia == false)
        {
            isButtonInsomnia = true;
            DataBaseManager.buttonCountInsomnia = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }
        if ((DataBaseManager.intelSewerGhostStory1 == true || DataBaseManager.intelSewerGhostStory2 == true || DataBaseManager.intelSewerGhostStory3 == true || DataBaseManager.intelSewerGhostStory4 == true || DataBaseManager.intelSewerGhostStory5 == true || DataBaseManager.intelSewerGhostStory6 == true) && isButtonSewerGhostStory == false)
        {
            isButtonSewerGhostStory = true;
            DataBaseManager.buttonCountSewerGhostStory = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }
        if ((DataBaseManager.intelMissingPeople1 == true || DataBaseManager.intelMissingPeople2 == true || DataBaseManager.intelMissingPeople3 == true || DataBaseManager.intelMissingPeople4 == true || DataBaseManager.intelMissingPeople5 == true || DataBaseManager.intelMissingPeople6 == true) && isButtonMissingPeople == false)
        {
            isButtonMissingPeople = true;
            DataBaseManager.buttonCountMissingPeople = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }
        if ((DataBaseManager.intelFishySmell1 == true || DataBaseManager.intelFishySmell2 == true || DataBaseManager.intelFishySmell3 == true || DataBaseManager.intelFishySmell4 == true || DataBaseManager.intelFishySmell5 == true || DataBaseManager.intelFishySmell6 == true) && isButtonFishySmell == false)
        {
            isButtonFishySmell = true;
            DataBaseManager.buttonCountFishySmell = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }
        if ((DataBaseManager.intelRiverWaterQuality1 == true || DataBaseManager.intelRiverWaterQuality2 == true || DataBaseManager.intelRiverWaterQuality3 == true || DataBaseManager.intelRiverWaterQuality4 == true || DataBaseManager.intelRiverWaterQuality5 == true || DataBaseManager.intelRiverWaterQuality6 == true) && isButtonRiverWaterQuality == false)
        {
            isButtonRiverWaterQuality = true;
            DataBaseManager.buttonCountRiverWaterQuality = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }
        if ((DataBaseManager.intelCreepyEyes1 == true || DataBaseManager.intelCreepyEyes2 == true || DataBaseManager.intelCreepyEyes3 == true || DataBaseManager.intelCreepyEyes4 == true || DataBaseManager.intelCreepyEyes5 == true || DataBaseManager.intelCreepyEyes6 == true) && isButtonCreepyEyes == false)
        {
            isButtonCreepyEyes = true;
            DataBaseManager.buttonCountCreepyEyes = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }

        if ((DataBaseManager.intelSafe1 == true || DataBaseManager.intelSafe2 == true || DataBaseManager.intelSafe3 == true || DataBaseManager.intelSafe4 == true || DataBaseManager.intelSafe5 == true || DataBaseManager.intelSafe6 == true) && isButtonSafe == false)
        {
            isButtonSafe = true;
            DataBaseManager.buttonCountSafe = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }

        if ((DataBaseManager.intelMurderCase1 == true || DataBaseManager.intelMurderCase2 == true || DataBaseManager.intelMurderCase3 == true || DataBaseManager.intelMurderCase4 == true || DataBaseManager.intelMurderCase5 == true || DataBaseManager.intelMurderCase6 == true) && isButtonMurderCase == false)
        {
            isButtonMurderCase = true;
            DataBaseManager.buttonCountMurderCase = DataBaseManager.gainEventInt;
            DataBaseManager.gainEventInt += 1;
        }
    }
    void DetailUpdate()
    {

        if (DataBaseManager.buttonCountPlanetaryParade != -1)
        {
            buttonPlanetarySequence.SetActive(true);
            buttonPlanetarySequence.transform.SetSiblingIndex(DataBaseManager.buttonCountPlanetaryParade);
        }
        if (DataBaseManager.buttonCountNightmare != -1)
        {
            buttonNightmare.SetActive(true);
            buttonNightmare.transform.SetSiblingIndex(DataBaseManager.buttonCountNightmare);
        }
        if (DataBaseManager.buttonCountParanoia != -1)
        {
            buttonParanoia.SetActive(true);
            buttonParanoia.transform.SetSiblingIndex(DataBaseManager.buttonCountParanoia);
        }
        if (DataBaseManager.buttonCountInsomnia != -1)
        {
            buttonInsomnia.SetActive(true);
            buttonInsomnia.transform.SetSiblingIndex(DataBaseManager.buttonCountInsomnia);
        }
        if (DataBaseManager.buttonCountSewerGhostStory != -1)
        {
            buttonSewerGhostStory.SetActive(true);
            buttonSewerGhostStory.transform.SetSiblingIndex(DataBaseManager.buttonCountSewerGhostStory);
        }
        if (DataBaseManager.buttonCountFishySmell != -1)
        {
            buttonFishySmell.SetActive(true);
            buttonFishySmell.transform.SetSiblingIndex(DataBaseManager.buttonCountFishySmell);
        }
        if (DataBaseManager.buttonCountMissingPeople != -1)
        {
            buttonMissingPeople.SetActive(true);
            buttonMissingPeople.transform.SetSiblingIndex(DataBaseManager.buttonCountMissingPeople);
        }


        if (DataBaseManager.buttonCountRiverWaterQuality != -1)
        {
            buttonRiverWaterQuality.SetActive(true);
            buttonRiverWaterQuality.transform.SetSiblingIndex(DataBaseManager.buttonCountRiverWaterQuality);
        }
        if (DataBaseManager.buttonCountCreepyEyes != -1)
        {
            buttonCreepyEyes.SetActive(true);
            buttonCreepyEyes.transform.SetSiblingIndex(DataBaseManager.buttonCountCreepyEyes);
        }
 
        if (DataBaseManager.buttonCountMurderCase != -1)
        {
            buttonMurderCase.SetActive(true);
            buttonMurderCase.transform.SetSiblingIndex(DataBaseManager.buttonCountMurderCase);
        }
        if (DataBaseManager.buttonCountSafe != -1)
        {
            buttonSafe.SetActive(true);
            buttonSafe.transform.SetSiblingIndex(DataBaseManager.buttonCountSafe);

        }
        if (DataBaseManager.gainEventInt > 6)
        {
            downButton.SetActive(true);
        }
    }


    public void ClickUpButton()
    {
        SoundManager.Instance.ClickSoundPlay();
        ispushDownButton = false;
        upButton.SetActive(false);
        downButton.SetActive(true);
    }
    public void ClickDownButton()
    {
        SoundManager.Instance.ClickSoundPlay();
        upButton.SetActive(true);
        downButton.SetActive(false);
        ispushDownButton = true;
        for (int i = 0; i < 6; i++)
        {
            Transform trChild = buttonRect.transform.GetChild(i);
            trChild.gameObject.SetActive(false);
        }
        if (DataBaseManager.gainEventInt == 7)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);

        }
        else if (DataBaseManager.gainEventInt == 8)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = buttonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);

        }
        else if (DataBaseManager.gainEventInt == 9)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = buttonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild9 = buttonRect.transform.GetChild(8);
            trChild9.gameObject.SetActive(true);
        }
        else if (DataBaseManager.gainEventInt == 10)
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
        else if (DataBaseManager.gainEventInt == 11)
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
        else if (DataBaseManager.gainEventInt == 12)
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
    }
}
