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
        if ((DataBaseManager.Intel_PlanetaryParade1 == true || DataBaseManager.Intel_PlanetaryParade2 == true || DataBaseManager.Intel_PlanetaryParade3 == true || DataBaseManager.Intel_PlanetaryParade4 == true || DataBaseManager.Intel_PlanetaryParade5 == true || DataBaseManager.Intel_PlanetaryParade6 == true) && isButtonPlanetarySequence == false)
        {
            isButtonPlanetarySequence = true;
            DataBaseManager.ButtonCount_PlanetaryParade = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_Nightmare1 == true || DataBaseManager.Intel_Nightmare2 == true || DataBaseManager.Intel_Nightmare3 == true || DataBaseManager.Intel_Nightmare4 == true || DataBaseManager.Intel_Nightmare5 == true || DataBaseManager.Intel_Nightmare6 == true) && isButtonNightmare == false)
        {
            isButtonNightmare = true;
            DataBaseManager.ButtonCount_Nightmare = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_Paranoia1 == true || DataBaseManager.Intel_Paranoia2 == true || DataBaseManager.Intel_Paranoia3 == true || DataBaseManager.Intel_Paranoia4 == true || DataBaseManager.Intel_Paranoia5 == true || DataBaseManager.Intel_Paranoia6 == true) && isButtonParanoia == false)
        {
            isButtonParanoia = true;
            DataBaseManager.ButtonCount_Paranoia = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_Insomnia1 == true || DataBaseManager.Intel_Insomnia2 == true || DataBaseManager.Intel_Insomnia3 == true || DataBaseManager.Intel_Insomnia4 == true || DataBaseManager.Intel_Insomnia5 == true || DataBaseManager.Intel_Insomnia6 == true) && isButtonInsomnia == false)
        {
            isButtonInsomnia = true;
            DataBaseManager.ButtonCount_Insomnia = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_SewerGhostStory1 == true || DataBaseManager.Intel_SewerGhostStory2 == true || DataBaseManager.Intel_SewerGhostStory3 == true || DataBaseManager.Intel_SewerGhostStory4 == true || DataBaseManager.Intel_SewerGhostStory5 == true || DataBaseManager.Intel_SewerGhostStory6 == true) && isButtonSewerGhostStory == false)
        {
            isButtonSewerGhostStory = true;
            DataBaseManager.ButtonCount_SewerGhostStory = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_MissingPeople1 == true || DataBaseManager.Intel_MissingPeople2 == true || DataBaseManager.Intel_MissingPeople3 == true || DataBaseManager.Intel_MissingPeople4 == true || DataBaseManager.Intel_MissingPeople5 == true || DataBaseManager.Intel_MissingPeople6 == true) && isButtonMissingPeople == false)
        {
            isButtonMissingPeople = true;
            DataBaseManager.ButtonCount_MissingPeople = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_FishySmell1 == true || DataBaseManager.Intel_FishySmell2 == true || DataBaseManager.Intel_FishySmell3 == true || DataBaseManager.Intel_FishySmell4 == true || DataBaseManager.Intel_FishySmell5 == true || DataBaseManager.Intel_FishySmell6 == true) && isButtonFishySmell == false)
        {
            isButtonFishySmell = true;
            DataBaseManager.ButtonCount_FishySmell = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_RiverWaterQuality1 == true || DataBaseManager.Intel_RiverWaterQuality2 == true || DataBaseManager.Intel_RiverWaterQuality3 == true || DataBaseManager.Intel_RiverWaterQuality4 == true || DataBaseManager.Intel_RiverWaterQuality5 == true || DataBaseManager.Intel_RiverWaterQuality6 == true) && isButtonRiverWaterQuality == false)
        {
            isButtonRiverWaterQuality = true;
            DataBaseManager.ButtonCount_RiverWaterQuality = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_CreepyEyes1 == true || DataBaseManager.Intel_CreepyEyes2 == true || DataBaseManager.Intel_CreepyEyes3 == true || DataBaseManager.Intel_CreepyEyes4 == true || DataBaseManager.Intel_CreepyEyes5 == true || DataBaseManager.Intel_CreepyEyes6 == true) && isButtonCreepyEyes == false)
        {
            isButtonCreepyEyes = true;
            DataBaseManager.ButtonCount_CreepyEyes = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }

        if ((DataBaseManager.Intel_Safe1 == true || DataBaseManager.Intel_Safe2 == true || DataBaseManager.Intel_Safe3 == true || DataBaseManager.Intel_Safe4 == true || DataBaseManager.Intel_Safe5 == true || DataBaseManager.Intel_Safe6 == true) && isButtonSafe == false)
        {
            isButtonSafe = true;
            DataBaseManager.ButtonCount_Safe = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }

        if ((DataBaseManager.Intel_MurderCase1 == true || DataBaseManager.Intel_MurderCase2 == true || DataBaseManager.Intel_MurderCase3 == true || DataBaseManager.Intel_MurderCase4 == true || DataBaseManager.Intel_MurderCase5 == true || DataBaseManager.Intel_MurderCase6 == true) && isButtonMurderCase == false)
        {
            isButtonMurderCase = true;
            DataBaseManager.ButtonCount_MurderCase = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
    }
    void DetailUpdate()
    {

        if (DataBaseManager.ButtonCount_PlanetaryParade != -1)
        {
            buttonPlanetarySequence.SetActive(true);
            buttonPlanetarySequence.transform.SetSiblingIndex(DataBaseManager.ButtonCount_PlanetaryParade);
        }
        if (DataBaseManager.ButtonCount_Nightmare != -1)
        {
            buttonNightmare.SetActive(true);
            buttonNightmare.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Nightmare);
        }
        if (DataBaseManager.ButtonCount_Paranoia != -1)
        {
            buttonParanoia.SetActive(true);
            buttonParanoia.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Paranoia);
        }
        if (DataBaseManager.ButtonCount_Insomnia != -1)
        {
            buttonInsomnia.SetActive(true);
            buttonInsomnia.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Insomnia);
        }
        if (DataBaseManager.ButtonCount_SewerGhostStory != -1)
        {
            buttonSewerGhostStory.SetActive(true);
            buttonSewerGhostStory.transform.SetSiblingIndex(DataBaseManager.ButtonCount_SewerGhostStory);
        }
        if (DataBaseManager.ButtonCount_FishySmell != -1)
        {
            buttonFishySmell.SetActive(true);
            buttonFishySmell.transform.SetSiblingIndex(DataBaseManager.ButtonCount_FishySmell);
        }
        if (DataBaseManager.ButtonCount_MissingPeople != -1)
        {
            buttonMissingPeople.SetActive(true);
            buttonMissingPeople.transform.SetSiblingIndex(DataBaseManager.ButtonCount_MissingPeople);
        }


        if (DataBaseManager.ButtonCount_RiverWaterQuality != -1)
        {
            buttonRiverWaterQuality.SetActive(true);
            buttonRiverWaterQuality.transform.SetSiblingIndex(DataBaseManager.ButtonCount_RiverWaterQuality);
        }
        if (DataBaseManager.ButtonCount_CreepyEyes != -1)
        {
            buttonCreepyEyes.SetActive(true);
            buttonCreepyEyes.transform.SetSiblingIndex(DataBaseManager.ButtonCount_CreepyEyes);
        }
 
        if (DataBaseManager.ButtonCount_MurderCase != -1)
        {
            buttonMurderCase.SetActive(true);
            buttonMurderCase.transform.SetSiblingIndex(DataBaseManager.ButtonCount_MurderCase);
        }
        if (DataBaseManager.ButtonCount_Safe != -1)
        {
            buttonSafe.SetActive(true);
            buttonSafe.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Safe);

        }
        if (DataBaseManager.GainEventInt > 6)
        {
            downButton.SetActive(true);
        }
    }


    public void ClickUpButton()
    {
        SoundManager.Instance.ClickSound_Play();
        ispushDownButton = false;
        upButton.SetActive(false);
        downButton.SetActive(true);
    }
    public void ClickDownButton()
    {
        SoundManager.Instance.ClickSound_Play();
        upButton.SetActive(true);
        downButton.SetActive(false);
        ispushDownButton = true;
        for (int i = 0; i < 6; i++)
        {
            Transform trChild = buttonRect.transform.GetChild(i);
            trChild.gameObject.SetActive(false);
        }
        if (DataBaseManager.GainEventInt == 7)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);

        }
        else if (DataBaseManager.GainEventInt == 8)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = buttonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);

        }
        else if (DataBaseManager.GainEventInt == 9)
        {
            Transform trChild16 = buttonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = buttonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild9 = buttonRect.transform.GetChild(8);
            trChild9.gameObject.SetActive(true);
        }
        else if (DataBaseManager.GainEventInt == 10)
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
        else if (DataBaseManager.GainEventInt == 11)
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
        else if (DataBaseManager.GainEventInt == 12)
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
