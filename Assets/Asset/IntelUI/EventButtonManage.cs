using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventButtonManage : MonoBehaviour
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
            DetailUpdate();
        }

    }

    public GameObject UpButton;
    public GameObject DownButton;

    public GameObject Button_PlanetarySequence;
    public GameObject Button_Nightmare;
    public GameObject Button_Paranoia;
    public GameObject Button_Insomnia;

    public GameObject Button_SewerGhostStory;
    public GameObject Button_MissingPeople;
    public GameObject Button_FishySmell;

    public GameObject Button_RiverWaterQuality;
    public GameObject Button_CreepyEyes;
    public GameObject Button_SewerWorker;
    public GameObject Button_Safe;
    public GameObject Button_SuddenChange;


    bool ButtonRize_PlanetarySequence = false;
    bool ButtonRize_Nightmare = false;
    bool ButtonRize_Paranoia = false;
    bool ButtonRize_Insomnia = false;
    bool ButtonRize_SewerGhostStory = false;
    bool ButtonRize_MissingPeople = false;

    bool ButtonRize_FishySmell = false;

    bool ButtonRize_RiverWaterQuality = false;
    bool ButtonRize_CreepyEyes = false;
    bool ButtonRize_SewerWorker = false;
    bool ButtonRize_Safe = false;
    bool ButtonRize_SuddenChange = false;


    void ButtonUpdate()
    {
        if ((DataBaseManager.Intel_PlanetarySequence1 == true || DataBaseManager.Intel_PlanetarySequence2 == true || DataBaseManager.Intel_PlanetarySequence3 == true || DataBaseManager.Intel_PlanetarySequence4 == true || DataBaseManager.Intel_PlanetarySequence5 == true || DataBaseManager.Intel_PlanetarySequence6 == true) && ButtonRize_PlanetarySequence == false)
        {
            ButtonRize_PlanetarySequence = true;
            DataBaseManager.ButtonCount_PlanetarySequence = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_Nightmare1 == true || DataBaseManager.Intel_Nightmare2 == true || DataBaseManager.Intel_Nightmare3 == true || DataBaseManager.Intel_Nightmare4 == true || DataBaseManager.Intel_Nightmare5 == true || DataBaseManager.Intel_Nightmare6 == true) && ButtonRize_Nightmare == false)
        {
            ButtonRize_Nightmare = true;
            DataBaseManager.ButtonCount_Nightmare = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_Paranoia1 == true || DataBaseManager.Intel_Paranoia2 == true || DataBaseManager.Intel_Paranoia3 == true || DataBaseManager.Intel_Paranoia4 == true || DataBaseManager.Intel_Paranoia5 == true || DataBaseManager.Intel_Paranoia6 == true) && ButtonRize_Paranoia == false)
        {
            ButtonRize_Paranoia = true;
            DataBaseManager.ButtonCount_Paranoia = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_Insomnia1 == true || DataBaseManager.Intel_Insomnia2 == true || DataBaseManager.Intel_Insomnia3 == true || DataBaseManager.Intel_Insomnia4 == true || DataBaseManager.Intel_Insomnia5 == true || DataBaseManager.Intel_Insomnia6 == true) && ButtonRize_Insomnia == false)
        {
            ButtonRize_Insomnia = true;
            DataBaseManager.ButtonCount_Insomnia = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_SewerGhostStory1 == true || DataBaseManager.Intel_SewerGhostStory2 == true || DataBaseManager.Intel_SewerGhostStory3 == true || DataBaseManager.Intel_SewerGhostStory4 == true || DataBaseManager.Intel_SewerGhostStory5 == true || DataBaseManager.Intel_SewerGhostStory6 == true) && ButtonRize_SewerGhostStory == false)
        {
            ButtonRize_SewerGhostStory = true;
            DataBaseManager.ButtonCount_SewerGhostStory = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_MissingPeople1 == true || DataBaseManager.Intel_MissingPeople2 == true || DataBaseManager.Intel_MissingPeople3 == true || DataBaseManager.Intel_MissingPeople4 == true || DataBaseManager.Intel_MissingPeople5 == true || DataBaseManager.Intel_MissingPeople6 == true) && ButtonRize_MissingPeople == false)
        {
            ButtonRize_MissingPeople = true;
            DataBaseManager.ButtonCount_MissingPeople = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_FishySmell1 == true || DataBaseManager.Intel_FishySmell2 == true || DataBaseManager.Intel_FishySmell3 == true || DataBaseManager.Intel_FishySmell4 == true || DataBaseManager.Intel_FishySmell5 == true || DataBaseManager.Intel_FishySmell6 == true) && ButtonRize_FishySmell == false)
        {
            ButtonRize_FishySmell = true;
            DataBaseManager.ButtonCount_FishySmell = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }


        if ((DataBaseManager.Intel_RiverWaterQuality1 == true || DataBaseManager.Intel_RiverWaterQuality2 == true || DataBaseManager.Intel_RiverWaterQuality3 == true || DataBaseManager.Intel_RiverWaterQuality4 == true || DataBaseManager.Intel_RiverWaterQuality5 == true || DataBaseManager.Intel_RiverWaterQuality6 == true) && ButtonRize_RiverWaterQuality == false)
        {
            ButtonRize_RiverWaterQuality = true;
            DataBaseManager.ButtonCount_RiverWaterQuality = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_CreepyEyes1 == true || DataBaseManager.Intel_CreepyEyes2 == true || DataBaseManager.Intel_CreepyEyes3 == true || DataBaseManager.Intel_CreepyEyes4 == true || DataBaseManager.Intel_CreepyEyes5 == true || DataBaseManager.Intel_CreepyEyes6 == true) && ButtonRize_CreepyEyes == false)
        {
            ButtonRize_CreepyEyes = true;
            DataBaseManager.ButtonCount_CreepyEyes = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_SewerWorker1 == true || DataBaseManager.Intel_SewerWorker2 == true || DataBaseManager.Intel_SewerWorker3 == true || DataBaseManager.Intel_SewerWorker4 == true || DataBaseManager.Intel_SewerWorker5 == true || DataBaseManager.Intel_SewerWorker6 == true) && ButtonRize_SewerWorker == false)
        {
            ButtonRize_SewerWorker = true;
            DataBaseManager.ButtonCount_SewerWorker = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }
        if ((DataBaseManager.Intel_Safe1 == true || DataBaseManager.Intel_Safe2 == true || DataBaseManager.Intel_Safe3 == true || DataBaseManager.Intel_Safe4 == true || DataBaseManager.Intel_Safe5 == true || DataBaseManager.Intel_Safe6 == true) && ButtonRize_Safe == false)
        {
            ButtonRize_Safe = true;
            DataBaseManager.ButtonCount_SuddenChange = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }

        if ((DataBaseManager.Intel_SuddenChange1 == true || DataBaseManager.Intel_SuddenChange2 == true || DataBaseManager.Intel_SuddenChange3 == true || DataBaseManager.Intel_SuddenChange4 == true || DataBaseManager.Intel_SuddenChange5 == true || DataBaseManager.Intel_SuddenChange6 == true) && ButtonRize_SuddenChange == false)
        {
            ButtonRize_SuddenChange = true;
            DataBaseManager.ButtonCount_Safe = DataBaseManager.GainEventInt;
            DataBaseManager.GainEventInt += 1;
        }



    }



    void DetailUpdate()
    {

        if (DataBaseManager.ButtonCount_PlanetarySequence != -1)
        {
            Button_PlanetarySequence.SetActive(true);
            Button_PlanetarySequence.transform.SetSiblingIndex(DataBaseManager.ButtonCount_PlanetarySequence);
        }
        if (DataBaseManager.ButtonCount_Nightmare != -1)
        {
            Button_Nightmare.SetActive(true);
            Button_Nightmare.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Nightmare);
        }
        if (DataBaseManager.ButtonCount_Paranoia != -1)
        {
            Button_Paranoia.SetActive(true);
            Button_Paranoia.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Paranoia);
        }
        if (DataBaseManager.ButtonCount_Insomnia != -1)
        {
            Button_Insomnia.SetActive(true);
            Button_Insomnia.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Insomnia);
        }
        if (DataBaseManager.ButtonCount_SewerGhostStory != -1)
        {
            Button_SewerGhostStory.SetActive(true);
            Button_SewerGhostStory.transform.SetSiblingIndex(DataBaseManager.ButtonCount_SewerGhostStory);
        }
        if (DataBaseManager.ButtonCount_FishySmell != -1)
        {
            Button_FishySmell.SetActive(true);
            Button_FishySmell.transform.SetSiblingIndex(DataBaseManager.ButtonCount_FishySmell);
        }
        if (DataBaseManager.ButtonCount_MissingPeople != -1)
        {
            Button_MissingPeople.SetActive(true);
            Button_MissingPeople.transform.SetSiblingIndex(DataBaseManager.ButtonCount_MissingPeople);
        }


        if (DataBaseManager.ButtonCount_RiverWaterQuality != -1)
        {
            Button_RiverWaterQuality.SetActive(true);
            Button_RiverWaterQuality.transform.SetSiblingIndex(DataBaseManager.ButtonCount_RiverWaterQuality);
        }
        if (DataBaseManager.ButtonCount_CreepyEyes != -1)
        {
            Button_CreepyEyes.SetActive(true);
            Button_CreepyEyes.transform.SetSiblingIndex(DataBaseManager.ButtonCount_CreepyEyes);
        }
        if (DataBaseManager.ButtonCount_SewerWorker != -1)
        {
            Button_SewerWorker.SetActive(true);
            Button_SewerWorker.transform.SetSiblingIndex(DataBaseManager.ButtonCount_SewerWorker);
        }
        if (DataBaseManager.ButtonCount_SuddenChange != -1)
        {
            Button_Safe.SetActive(true);
            Button_Safe.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Safe);
        }
        if (DataBaseManager.ButtonCount_Safe != -1)
        {
            Button_SuddenChange.SetActive(true);
            Button_SuddenChange.transform.SetSiblingIndex(DataBaseManager.ButtonCount_Safe);
        }


        if (DataBaseManager.GainEventInt > 6)
        {
            DownButton.SetActive(true);
        }
    }

    public GameObject ButtonRect;
    bool PushDownButton = false;
    public void ClickUpButton()
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

        if (DataBaseManager.GainEventInt == 7)
        {
            Transform trChild16 = ButtonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);

        }

        else if (DataBaseManager.GainEventInt == 8)
        {
            Transform trChild16 = ButtonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = ButtonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);

        }
        else if (DataBaseManager.GainEventInt == 9)
        {
            Transform trChild16 = ButtonRect.transform.GetChild(6);
            trChild16.gameObject.SetActive(true);
            Transform trChild7 = ButtonRect.transform.GetChild(7);
            trChild7.gameObject.SetActive(true);
            Transform trChild9 = ButtonRect.transform.GetChild(8);
            trChild9.gameObject.SetActive(true);

        }
        else if (DataBaseManager.GainEventInt == 10)
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
        else if (DataBaseManager.GainEventInt == 11)
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
        else if (DataBaseManager.GainEventInt == 12)
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
    }


}
