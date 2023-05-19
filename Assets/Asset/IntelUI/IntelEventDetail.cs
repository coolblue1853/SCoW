using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelEventDetail : MonoBehaviour
{
    GameObject Detail_PlanetarySequence1;
    GameObject Detail_PlanetarySequence2;
    GameObject Detail_PlanetarySequence3;
    GameObject Detail_PlanetarySequence4;
    GameObject Detail_PlanetarySequence5;
    GameObject Detail_PlanetarySequence6;
    bool ON_PlanetarySequence1;
    bool ON_PlanetarySequence2;
    bool ON_PlanetarySequence3;
    bool ON_PlanetarySequence4;
    bool ON_PlanetarySequence5;
    bool ON_PlanetarySequence6;
    int Count_PlanetarySequence = 0;
    public GameObject PlanetarySequenceContents;
    public GameObject PlanetarySequence_Detail;
    public static int PlanetarySequenceIntelInt;
    public GameObject NextButton_PlanetarySequence;
    public GameObject PrevButton_PlanetarySequence;
    string NowPage;

    GameObject Detail_Nightmare1;
    GameObject Detail_Nightmare2;
    GameObject Detail_Nightmare3;
    GameObject Detail_Nightmare4;
    GameObject Detail_Nightmare5;
    GameObject Detail_Nightmare6;
    bool ON_Nightmare1;
    bool ON_Nightmare2;
    bool ON_Nightmare3;
    bool ON_Nightmare4;
    bool ON_Nightmare5;
    bool ON_Nightmare6;
    int Count_Nightmare = 0;
    public GameObject NightmareContents;
    public GameObject Nightmare_Detail;
    public static int NightmareIntelInt;
    public GameObject NextButton_Nightmare;
    public GameObject PrevButton_Nightmare;

    GameObject Detail_Paranoia1;
    GameObject Detail_Paranoia2;
    GameObject Detail_Paranoia3;
    GameObject Detail_Paranoia4;
    GameObject Detail_Paranoia5;
    GameObject Detail_Paranoia6;
    bool ON_Paranoia1;
    bool ON_Paranoia2;
    bool ON_Paranoia3;
    bool ON_Paranoia4;
    bool ON_Paranoia5;
    bool ON_Paranoia6;
    int Count_Paranoia = 0;
    public GameObject ParanoiaContents;
    public GameObject Paranoia_Detail;
    public static int ParanoiaIntelInt;
    public GameObject NextButton_Paranoia;
    public GameObject PrevButton_Paranoia;

    GameObject Detail_Insomnia1;
    GameObject Detail_Insomnia2;
    GameObject Detail_Insomnia3;
    GameObject Detail_Insomnia4;
    GameObject Detail_Insomnia5;
    GameObject Detail_Insomnia6;
    bool ON_Insomnia1;
    bool ON_Insomnia2;
    bool ON_Insomnia3;
    bool ON_Insomnia4;
    bool ON_Insomnia5;
    bool ON_Insomnia6;
    int Count_Insomnia = 0;
    public GameObject InsomniaContents;
    public GameObject Insomnia_Detail;
    public static int InsomniaIntelInt;
    public GameObject NextButton_Insomnia;
    public GameObject PrevButton_Insomnia;

    GameObject Detail_SewerGhostStory1;
    GameObject Detail_SewerGhostStory2;
    GameObject Detail_SewerGhostStory3;
    GameObject Detail_SewerGhostStory4;
    GameObject Detail_SewerGhostStory5;
    GameObject Detail_SewerGhostStory6;
    bool ON_SewerGhostStory1;
    bool ON_SewerGhostStory2;
    bool ON_SewerGhostStory3;
    bool ON_SewerGhostStory4;
    bool ON_SewerGhostStory5;
    bool ON_SewerGhostStory6;
    int Count_SewerGhostStory = 0;
    public GameObject SewerGhostStoryContents;
    public GameObject SewerGhostStory_Detail;
    public static int SewerGhostStoryIntelInt;
    public GameObject NextButton_SewerGhostStory;
    public GameObject PrevButton_SewerGhostStory;

    GameObject Detail_MissingPeople1;
    GameObject Detail_MissingPeople2;
    GameObject Detail_MissingPeople3;
    GameObject Detail_MissingPeople4;
    GameObject Detail_MissingPeople5;
    GameObject Detail_MissingPeople6;
    bool ON_MissingPeople1;
    bool ON_MissingPeople2;
    bool ON_MissingPeople3;
    bool ON_MissingPeople4;
    bool ON_MissingPeople5;
    bool ON_MissingPeople6;
    int Count_MissingPeople = 0;
    public GameObject MissingPeopleContents;
    public GameObject MissingPeople_Detail;
    public static int MissingPeopleIntelInt;
    public GameObject NextButton_MissingPeople;
    public GameObject PrevButton_MissingPeople;

    GameObject Detail_FishySmell1;
    GameObject Detail_FishySmell2;
    GameObject Detail_FishySmell3;
    GameObject Detail_FishySmell4;
    GameObject Detail_FishySmell5;
    GameObject Detail_FishySmell6;
    bool ON_FishySmell1;
    bool ON_FishySmell2;
    bool ON_FishySmell3;
    bool ON_FishySmell4;
    bool ON_FishySmell5;
    bool ON_FishySmell6;
    int Count_FishySmell = 0;
    public GameObject FishySmellContents;
    public GameObject FishySmell_Detail;
    public static int FishySmellIntelInt;
    public GameObject NextButton_FishySmell;
    public GameObject PrevButton_FishySmell;

    GameObject Detail_RiverWaterQuality1;
    GameObject Detail_RiverWaterQuality2;
    GameObject Detail_RiverWaterQuality3;
    GameObject Detail_RiverWaterQuality4;
    GameObject Detail_RiverWaterQuality5;
    GameObject Detail_RiverWaterQuality6;
    bool ON_RiverWaterQuality1;
    bool ON_RiverWaterQuality2;
    bool ON_RiverWaterQuality3;
    bool ON_RiverWaterQuality4;
    bool ON_RiverWaterQuality5;
    bool ON_RiverWaterQuality6;
    int Count_RiverWaterQuality = 0;
    public GameObject RiverWaterQualityContents;
    public GameObject RiverWaterQuality_Detail;
    public static int RiverWaterQualityIntelInt;
    public GameObject NextButton_RiverWaterQuality;
    public GameObject PrevButton_RiverWaterQuality;

    GameObject Detail_CreepyEyes1;
    GameObject Detail_CreepyEyes2;
    GameObject Detail_CreepyEyes3;
    GameObject Detail_CreepyEyes4;
    GameObject Detail_CreepyEyes5;
    GameObject Detail_CreepyEyes6;
    bool ON_CreepyEyes1;
    bool ON_CreepyEyes2;
    bool ON_CreepyEyes3;
    bool ON_CreepyEyes4;
    bool ON_CreepyEyes5;
    bool ON_CreepyEyes6;
    int Count_CreepyEyes = 0;
    public GameObject CreepyEyesContents;
    public GameObject CreepyEyes_Detail;
    public static int CreepyEyesIntelInt;
    public GameObject NextButton_CreepyEyes;
    public GameObject PrevButton_CreepyEyes;

   

    GameObject Detail_Safe1;
    GameObject Detail_Safe2;
    GameObject Detail_Safe3;
    GameObject Detail_Safe4;
    GameObject Detail_Safe5;
    GameObject Detail_Safe6;
    bool ON_Safe1;
    bool ON_Safe2;
    bool ON_Safe3;
    bool ON_Safe4;
    bool ON_Safe5;
    bool ON_Safe6;
    int Count_Safe = 0;
    public GameObject SafeContents;
    public GameObject Safe_Detail;
    public static int SafeIntelInt;
    public GameObject NextButton_Safe;
    public GameObject PrevButton_Safe;

    GameObject Detail_SuddenChange1;
    GameObject Detail_SuddenChange2;
    GameObject Detail_SuddenChange3;
    GameObject Detail_SuddenChange4;
    GameObject Detail_SuddenChange5;
    GameObject Detail_SuddenChange6;
    bool ON_SuddenChange1;
    bool ON_SuddenChange2;
    bool ON_SuddenChange3;
    bool ON_SuddenChange4;
    bool ON_SuddenChange5;
    bool ON_SuddenChange6;
    int Count_SuddenChange = 0;
    public GameObject SuddenChangeContents;
    public GameObject SuddenChange_Detail;
    public static int SuddenChangeIntelInt;
    public GameObject NextButton_SuddenChange;
    public GameObject PrevButton_SuddenChange;
    void Awake()
    {
        setGameObecject();
    }

    void setGameObecject()
    {
        Detail_PlanetarySequence1 = PlanetarySequenceContents.transform.GetChild(0).gameObject;
        Detail_PlanetarySequence2 = PlanetarySequenceContents.transform.GetChild(1).gameObject;
        Detail_PlanetarySequence3 = PlanetarySequenceContents.transform.GetChild(2).gameObject;
        Detail_PlanetarySequence4 = PlanetarySequenceContents.transform.GetChild(3).gameObject;
        Detail_PlanetarySequence5 = PlanetarySequenceContents.transform.GetChild(4).gameObject;
        Detail_PlanetarySequence6 = PlanetarySequenceContents.transform.GetChild(5).gameObject;

        Detail_Nightmare1 = NightmareContents.transform.GetChild(0).gameObject;
        Detail_Nightmare2 = NightmareContents.transform.GetChild(1).gameObject;
        Detail_Nightmare3 = NightmareContents.transform.GetChild(2).gameObject;
        Detail_Nightmare4 = NightmareContents.transform.GetChild(3).gameObject;
        Detail_Nightmare5 = NightmareContents.transform.GetChild(4).gameObject;
        Detail_Nightmare6 = NightmareContents.transform.GetChild(5).gameObject;

        Detail_Paranoia1 = ParanoiaContents.transform.GetChild(0).gameObject;
        Detail_Paranoia2 = ParanoiaContents.transform.GetChild(1).gameObject;
        Detail_Paranoia3 = ParanoiaContents.transform.GetChild(2).gameObject;
        Detail_Paranoia4 = ParanoiaContents.transform.GetChild(3).gameObject;
        Detail_Paranoia5 = ParanoiaContents.transform.GetChild(4).gameObject;
        Detail_Paranoia6 = ParanoiaContents.transform.GetChild(5).gameObject;

        Detail_Insomnia1 = InsomniaContents.transform.GetChild(0).gameObject;
        Detail_Insomnia2 = InsomniaContents.transform.GetChild(1).gameObject;
        Detail_Insomnia3 = InsomniaContents.transform.GetChild(2).gameObject;
        Detail_Insomnia4 = InsomniaContents.transform.GetChild(3).gameObject;
        Detail_Insomnia5 = InsomniaContents.transform.GetChild(4).gameObject;
        Detail_Insomnia6 = InsomniaContents.transform.GetChild(5).gameObject;

        Detail_SewerGhostStory1 = SewerGhostStoryContents.transform.GetChild(0).gameObject;
        Detail_SewerGhostStory2 = SewerGhostStoryContents.transform.GetChild(1).gameObject;
        Detail_SewerGhostStory3 = SewerGhostStoryContents.transform.GetChild(2).gameObject;
        Detail_SewerGhostStory4 = SewerGhostStoryContents.transform.GetChild(3).gameObject;
        Detail_SewerGhostStory5 = SewerGhostStoryContents.transform.GetChild(4).gameObject;
        Detail_SewerGhostStory6 = SewerGhostStoryContents.transform.GetChild(5).gameObject;

        Detail_MissingPeople1 = MissingPeopleContents.transform.GetChild(0).gameObject;
        Detail_MissingPeople2 = MissingPeopleContents.transform.GetChild(1).gameObject;
        Detail_MissingPeople3 = MissingPeopleContents.transform.GetChild(2).gameObject;
        Detail_MissingPeople4 = MissingPeopleContents.transform.GetChild(3).gameObject;
        Detail_MissingPeople5 = MissingPeopleContents.transform.GetChild(4).gameObject;
        Detail_MissingPeople6 = MissingPeopleContents.transform.GetChild(5).gameObject;

        Detail_FishySmell1 = FishySmellContents.transform.GetChild(0).gameObject;
        Detail_FishySmell2 = FishySmellContents.transform.GetChild(1).gameObject;
        Detail_FishySmell3 = FishySmellContents.transform.GetChild(2).gameObject;
        Detail_FishySmell4 = FishySmellContents.transform.GetChild(3).gameObject;
        Detail_FishySmell5 = FishySmellContents.transform.GetChild(4).gameObject;
        Detail_FishySmell6 = FishySmellContents.transform.GetChild(5).gameObject;

        Detail_RiverWaterQuality1 = RiverWaterQualityContents.transform.GetChild(0).gameObject;
        Detail_RiverWaterQuality2 = RiverWaterQualityContents.transform.GetChild(1).gameObject;
        Detail_RiverWaterQuality3 = RiverWaterQualityContents.transform.GetChild(2).gameObject;
        Detail_RiverWaterQuality4 = RiverWaterQualityContents.transform.GetChild(3).gameObject;
        Detail_RiverWaterQuality5 = RiverWaterQualityContents.transform.GetChild(4).gameObject;
        Detail_RiverWaterQuality6 = RiverWaterQualityContents.transform.GetChild(5).gameObject;

        Detail_CreepyEyes1 = CreepyEyesContents.transform.GetChild(0).gameObject;
        Detail_CreepyEyes2 = CreepyEyesContents.transform.GetChild(1).gameObject;
        Detail_CreepyEyes3 = CreepyEyesContents.transform.GetChild(2).gameObject;
        Detail_CreepyEyes4 = CreepyEyesContents.transform.GetChild(3).gameObject;
        Detail_CreepyEyes5 = CreepyEyesContents.transform.GetChild(4).gameObject;
        Detail_CreepyEyes6 = CreepyEyesContents.transform.GetChild(5).gameObject;


        Detail_Safe1 = SafeContents.transform.GetChild(0).gameObject;
        Detail_Safe2 = SafeContents.transform.GetChild(1).gameObject;
        Detail_Safe3 = SafeContents.transform.GetChild(2).gameObject;
        Detail_Safe4 = SafeContents.transform.GetChild(3).gameObject;
        Detail_Safe5 = SafeContents.transform.GetChild(4).gameObject;
        Detail_Safe6 = SafeContents.transform.GetChild(5).gameObject;

        Detail_SuddenChange1 = SuddenChangeContents.transform.GetChild(0).gameObject;
        Detail_SuddenChange2 = SuddenChangeContents.transform.GetChild(1).gameObject;
        Detail_SuddenChange3 = SuddenChangeContents.transform.GetChild(2).gameObject;
        Detail_SuddenChange4 = SuddenChangeContents.transform.GetChild(3).gameObject;
        Detail_SuddenChange5 = SuddenChangeContents.transform.GetChild(4).gameObject;
        Detail_SuddenChange6 = SuddenChangeContents.transform.GetChild(5).gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlanetarySequenceDetail();
        NightmareDetail();
        ParanoiaDetail();
        InsomniaDetail();
        SewerGhostStoryDetail();
        MissingPeopleDetail();
        FishySmellDetail();
        RiverWaterQualityDetail();
        CreepyEyesDetail();

        SafeDetail();
        SuddenChangeDetail();
        if (PlanetarySequence_Detail.activeSelf == true)
        {
            NowPage = "PlanetarySequence";
        }
        if (Nightmare_Detail.activeSelf == true)
        {
            NowPage = "Nightmare";
        }
        if (Paranoia_Detail.activeSelf == true)
        {
            NowPage = "Paranoia";
        }
        if (Insomnia_Detail.activeSelf == true)
        {
            NowPage = "Insomnia";
        }
        if (SewerGhostStory_Detail.activeSelf == true)
        {
            NowPage = "SewerGhostStory";
        }
        if (MissingPeople_Detail.activeSelf == true)
        {
            NowPage = "MissingPeople";
        }
        if (FishySmell_Detail.activeSelf == true)
        {
            NowPage = "FishySmell";
        }
        if (RiverWaterQuality_Detail.activeSelf == true)
        {
            NowPage = "RiverWaterQuality";
        }
        if (CreepyEyes_Detail.activeSelf == true)
        {
            NowPage = "CreepyEyes";
        }

        if (Safe_Detail.activeSelf == true)
        {
            NowPage = "Safe";
        }
        if (SuddenChange_Detail.activeSelf == true)
        {
            NowPage = "SuddenChange";
        }

    }




    void PlanetarySequenceDetail()
    {
        if (ON_PlanetarySequence1 == false && DataBaseManager.Intel_PlanetarySequence1 == true)
        {
            ON_PlanetarySequence1 = true;
            Detail_PlanetarySequence1.SetActive(true);
            Detail_PlanetarySequence1.transform.SetAsLastSibling();
            Count_PlanetarySequence += 1;
        }
        if (ON_PlanetarySequence2 == false && DataBaseManager.Intel_PlanetarySequence2 == true)
        {
            ON_PlanetarySequence2 = true;
            Detail_PlanetarySequence2.SetActive(true);
            Detail_PlanetarySequence2.transform.SetAsLastSibling();
            Count_PlanetarySequence += 1;
        }
        if (ON_PlanetarySequence3 == false && DataBaseManager.Intel_PlanetarySequence3 == true)
        {
            ON_PlanetarySequence3 = true;
            Detail_PlanetarySequence3.SetActive(true);
            Detail_PlanetarySequence3.transform.SetAsLastSibling();
            Count_PlanetarySequence += 1;
        }
        if (ON_PlanetarySequence4 == false && DataBaseManager.Intel_PlanetarySequence4 == true)
        {
            ON_PlanetarySequence4 = true;
            Detail_PlanetarySequence4.SetActive(true);
            Detail_PlanetarySequence4.transform.SetAsLastSibling();
            Count_PlanetarySequence += 1;
        }
        if (ON_PlanetarySequence5 == false && DataBaseManager.Intel_PlanetarySequence5 == true)
        {
            ON_PlanetarySequence5 = true;
            Detail_PlanetarySequence5.SetActive(true);
            Detail_PlanetarySequence5.transform.SetAsLastSibling();
            Count_PlanetarySequence += 1;
        }
        if (ON_PlanetarySequence6 == false && DataBaseManager.Intel_PlanetarySequence6 == true)
        {
            ON_PlanetarySequence6 = true;
            Detail_PlanetarySequence6.SetActive(true);
            Detail_PlanetarySequence6.transform.SetAsLastSibling();
            Count_PlanetarySequence += 1;
        }

        if (Count_PlanetarySequence > 2 && DataBaseManager.NowPage_PlanetarySequence == 1)
        {
            NextButton_PlanetarySequence.SetActive(true);
        }
        else if (Count_PlanetarySequence > 4 && DataBaseManager.NowPage_PlanetarySequence == 2)
        {
            NextButton_PlanetarySequence.SetActive(true);
        }
        else
        {
            NextButton_PlanetarySequence.SetActive(false);
        }

        if (DataBaseManager.NowPage_PlanetarySequence == 2)
        {
            PrevButton_PlanetarySequence.SetActive(true);
        }
        else if (DataBaseManager.NowPage_PlanetarySequence == 3)
        {
            PrevButton_PlanetarySequence.SetActive(true);
        }
        else
        {
            PrevButton_PlanetarySequence.SetActive(false);
        }
    }
    void NightmareDetail()
    {
        if (ON_Nightmare1 == false && DataBaseManager.Intel_Nightmare1 == true)
        {
            ON_Nightmare1 = true;
            Detail_Nightmare1.SetActive(true);
            Detail_Nightmare1.transform.SetAsLastSibling();
            Count_Nightmare += 1;
        }
        if (ON_Nightmare2 == false && DataBaseManager.Intel_Nightmare2 == true)
        {
            ON_Nightmare2 = true;
            Detail_Nightmare2.SetActive(true);
            Detail_Nightmare2.transform.SetAsLastSibling();
            Count_Nightmare += 1;
        }
        if (ON_Nightmare3 == false && DataBaseManager.Intel_Nightmare3 == true)
        {
            ON_Nightmare3 = true;
            Detail_Nightmare3.SetActive(true);
            Detail_Nightmare3.transform.SetAsLastSibling();
            Count_Nightmare += 1;
        }
        if (ON_Nightmare4 == false && DataBaseManager.Intel_Nightmare4 == true)
        {
            ON_Nightmare4 = true;
            Detail_Nightmare4.SetActive(true);
            Detail_Nightmare4.transform.SetAsLastSibling();
            Count_Nightmare += 1;
        }
        if (ON_Nightmare5 == false && DataBaseManager.Intel_Nightmare5 == true)
        {
            ON_Nightmare5 = true;
            Detail_Nightmare5.SetActive(true);
            Detail_Nightmare5.transform.SetAsLastSibling();
            Count_Nightmare += 1;
        }
        if (ON_Nightmare6 == false && DataBaseManager.Intel_Nightmare6 == true)
        {
            ON_Nightmare6 = true;
            Detail_Nightmare6.SetActive(true);
            Detail_Nightmare6.transform.SetAsLastSibling();
            Count_Nightmare += 1;
        }

        if (Count_Nightmare > 2 && DataBaseManager.NowPage_Nightmare == 1)
        {
            NextButton_Nightmare.SetActive(true);
        }
        else if (Count_Nightmare > 4 && DataBaseManager.NowPage_Nightmare == 2)
        {
            NextButton_Nightmare.SetActive(true);
        }
        else
        {
            NextButton_Nightmare.SetActive(false);
        }

        if (DataBaseManager.NowPage_Nightmare == 2)
        {
            PrevButton_Nightmare.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Nightmare == 3)
        {
            PrevButton_Nightmare.SetActive(true);
        }
        else
        {
            PrevButton_Nightmare.SetActive(false);
        }
    }
    void ParanoiaDetail()
    {
        if (ON_Paranoia1 == false && DataBaseManager.Intel_Paranoia1 == true)
        {
            ON_Paranoia1 = true;
            Detail_Paranoia1.SetActive(true);
            Detail_Paranoia1.transform.SetAsLastSibling();
            Count_Paranoia += 1;
        }
        if (ON_Paranoia2 == false && DataBaseManager.Intel_Paranoia2 == true)
        {
            ON_Paranoia2 = true;
            Detail_Paranoia2.SetActive(true);
            Detail_Paranoia2.transform.SetAsLastSibling();
            Count_Paranoia += 1;
        }
        if (ON_Paranoia3 == false && DataBaseManager.Intel_Paranoia3 == true)
        {
            ON_Paranoia3 = true;
            Detail_Paranoia3.SetActive(true);
            Detail_Paranoia3.transform.SetAsLastSibling();
            Count_Paranoia += 1;
        }
        if (ON_Paranoia4 == false && DataBaseManager.Intel_Paranoia4 == true)
        {
            ON_Paranoia4 = true;
            Detail_Paranoia4.SetActive(true);
            Detail_Paranoia4.transform.SetAsLastSibling();
            Count_Paranoia += 1;
        }
        if (ON_Paranoia5 == false && DataBaseManager.Intel_Paranoia5 == true)
        {
            ON_Paranoia5 = true;
            Detail_Paranoia5.SetActive(true);
            Detail_Paranoia5.transform.SetAsLastSibling();
            Count_Paranoia += 1;
        }
        if (ON_Paranoia6 == false && DataBaseManager.Intel_Paranoia6 == true)
        {
            ON_Paranoia6 = true;
            Detail_Paranoia6.SetActive(true);
            Detail_Paranoia6.transform.SetAsLastSibling();
            Count_Paranoia += 1;
        }

        if (Count_Paranoia > 2 && DataBaseManager.NowPage_Paranoia == 1)
        {
            NextButton_Paranoia.SetActive(true);
        }
        else if (Count_Paranoia > 4 && DataBaseManager.NowPage_Paranoia == 2)
        {
            NextButton_Paranoia.SetActive(true);
        }
        else
        {
            NextButton_Paranoia.SetActive(false);
        }

        if (DataBaseManager.NowPage_Paranoia == 2)
        {
            PrevButton_Paranoia.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Paranoia == 3)
        {
            PrevButton_Paranoia.SetActive(true);
        }
        else
        {
            PrevButton_Paranoia.SetActive(false);
        }
    }
    void InsomniaDetail()
    {
        if (ON_Insomnia1 == false && DataBaseManager.Intel_Insomnia1 == true)
        {
            ON_Insomnia1 = true;
            Detail_Insomnia1.SetActive(true);
            Detail_Insomnia1.transform.SetAsLastSibling();
            Count_Insomnia += 1;
        }
        if (ON_Insomnia2 == false && DataBaseManager.Intel_Insomnia2 == true)
        {
            ON_Insomnia2 = true;
            Detail_Insomnia2.SetActive(true);
            Detail_Insomnia2.transform.SetAsLastSibling();
            Count_Insomnia += 1;
        }
        if (ON_Insomnia3 == false && DataBaseManager.Intel_Insomnia3 == true)
        {
            ON_Insomnia3 = true;
            Detail_Insomnia3.SetActive(true);
            Detail_Insomnia3.transform.SetAsLastSibling();
            Count_Insomnia += 1;
        }
        if (ON_Insomnia4 == false && DataBaseManager.Intel_Insomnia4 == true)
        {
            ON_Insomnia4 = true;
            Detail_Insomnia4.SetActive(true);
            Detail_Insomnia4.transform.SetAsLastSibling();
            Count_Insomnia += 1;
        }
        if (ON_Insomnia5 == false && DataBaseManager.Intel_Insomnia5 == true)
        {
            ON_Insomnia5 = true;
            Detail_Insomnia5.SetActive(true);
            Detail_Insomnia5.transform.SetAsLastSibling();
            Count_Insomnia += 1;
        }
        if (ON_Insomnia6 == false && DataBaseManager.Intel_Insomnia6 == true)
        {
            ON_Insomnia6 = true;
            Detail_Insomnia6.SetActive(true);
            Detail_Insomnia6.transform.SetAsLastSibling();
            Count_Insomnia += 1;
        }

        if (Count_Insomnia > 2 && DataBaseManager.NowPage_Insomnia == 1)
        {
            NextButton_Insomnia.SetActive(true);
        }
        else if (Count_Insomnia > 4 && DataBaseManager.NowPage_Insomnia == 2)
        {
            NextButton_Insomnia.SetActive(true);
        }
        else
        {
            NextButton_Insomnia.SetActive(false);
        }

        if (DataBaseManager.NowPage_Insomnia == 2)
        {
            PrevButton_Insomnia.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Insomnia == 3)
        {
            PrevButton_Insomnia.SetActive(true);
        }
        else
        {
            PrevButton_Insomnia.SetActive(false);
        }
    }
    void SewerGhostStoryDetail()
    {
        if (ON_SewerGhostStory1 == false && DataBaseManager.Intel_SewerGhostStory1 == true)
        {
            ON_SewerGhostStory1 = true;
            Detail_SewerGhostStory1.SetActive(true);
            Detail_SewerGhostStory1.transform.SetAsLastSibling();
            Count_SewerGhostStory += 1;
        }
        if (ON_SewerGhostStory2 == false && DataBaseManager.Intel_SewerGhostStory2 == true)
        {
            ON_SewerGhostStory2 = true;
            Detail_SewerGhostStory2.SetActive(true);
            Detail_SewerGhostStory2.transform.SetAsLastSibling();
            Count_SewerGhostStory += 1;
        }
        if (ON_SewerGhostStory3 == false && DataBaseManager.Intel_SewerGhostStory3 == true)
        {
            ON_SewerGhostStory3 = true;
            Detail_SewerGhostStory3.SetActive(true);
            Detail_SewerGhostStory3.transform.SetAsLastSibling();
            Count_SewerGhostStory += 1;
        }
        if (ON_SewerGhostStory4 == false && DataBaseManager.Intel_SewerGhostStory4 == true)
        {
            ON_SewerGhostStory4 = true;
            Detail_SewerGhostStory4.SetActive(true);
            Detail_SewerGhostStory4.transform.SetAsLastSibling();
            Count_SewerGhostStory += 1;
        }
        if (ON_SewerGhostStory5 == false && DataBaseManager.Intel_SewerGhostStory5 == true)
        {
            ON_SewerGhostStory5 = true;
            Detail_SewerGhostStory5.SetActive(true);
            Detail_SewerGhostStory5.transform.SetAsLastSibling();
            Count_SewerGhostStory += 1;
        }
        if (ON_SewerGhostStory6 == false && DataBaseManager.Intel_SewerGhostStory6 == true)
        {
            ON_SewerGhostStory6 = true;
            Detail_SewerGhostStory6.SetActive(true);
            Detail_SewerGhostStory6.transform.SetAsLastSibling();
            Count_SewerGhostStory += 1;
        }

        if (Count_SewerGhostStory > 2 && DataBaseManager.NowPage_SewerGhostStory == 1)
        {
            NextButton_SewerGhostStory.SetActive(true);
        }
        else if (Count_SewerGhostStory > 4 && DataBaseManager.NowPage_SewerGhostStory == 2)
        {
            NextButton_SewerGhostStory.SetActive(true);
        }
        else
        {
            NextButton_SewerGhostStory.SetActive(false);
        }

        if (DataBaseManager.NowPage_SewerGhostStory == 2)
        {
            PrevButton_SewerGhostStory.SetActive(true);
        }
        else if (DataBaseManager.NowPage_SewerGhostStory == 3)
        {
            PrevButton_SewerGhostStory.SetActive(true);
        }
        else
        {
            PrevButton_SewerGhostStory.SetActive(false);
        }
    }
    void MissingPeopleDetail()
    {
        if (ON_MissingPeople1 == false && DataBaseManager.Intel_MissingPeople1 == true)
        {
            ON_MissingPeople1 = true;
            Detail_MissingPeople1.SetActive(true);
            Detail_MissingPeople1.transform.SetAsLastSibling();
            Count_MissingPeople += 1;
        }
        if (ON_MissingPeople2 == false && DataBaseManager.Intel_MissingPeople2 == true)
        {
            ON_MissingPeople2 = true;
            Detail_MissingPeople2.SetActive(true);
            Detail_MissingPeople2.transform.SetAsLastSibling();
            Count_MissingPeople += 1;
        }
        if (ON_MissingPeople3 == false && DataBaseManager.Intel_MissingPeople3 == true)
        {
            ON_MissingPeople3 = true;
            Detail_MissingPeople3.SetActive(true);
            Detail_MissingPeople3.transform.SetAsLastSibling();
            Count_MissingPeople += 1;
        }
        if (ON_MissingPeople4 == false && DataBaseManager.Intel_MissingPeople4 == true)
        {
            ON_MissingPeople4 = true;
            Detail_MissingPeople4.SetActive(true);
            Detail_MissingPeople4.transform.SetAsLastSibling();
            Count_MissingPeople += 1;
        }
        if (ON_MissingPeople5 == false && DataBaseManager.Intel_MissingPeople5 == true)
        {
            ON_MissingPeople5 = true;
            Detail_MissingPeople5.SetActive(true);
            Detail_MissingPeople5.transform.SetAsLastSibling();
            Count_MissingPeople += 1;
        }
        if (ON_MissingPeople6 == false && DataBaseManager.Intel_MissingPeople6 == true)
        {
            ON_MissingPeople6 = true;
            Detail_MissingPeople6.SetActive(true);
            Detail_MissingPeople6.transform.SetAsLastSibling();
            Count_MissingPeople += 1;
        }

        if (Count_MissingPeople > 2 && DataBaseManager.NowPage_MissingPeople == 1)
        {
            NextButton_MissingPeople.SetActive(true);
        }
        else if (Count_MissingPeople > 4 && DataBaseManager.NowPage_MissingPeople == 2)
        {
            NextButton_MissingPeople.SetActive(true);
        }
        else
        {
            NextButton_MissingPeople.SetActive(false);
        }

        if (DataBaseManager.NowPage_MissingPeople == 2)
        {
            PrevButton_MissingPeople.SetActive(true);
        }
        else if (DataBaseManager.NowPage_MissingPeople
            == 3)
        {
            PrevButton_MissingPeople.SetActive(true);
        }
        else
        {
            PrevButton_MissingPeople.SetActive(false);
        }
    }
    void FishySmellDetail()
    {
        if (ON_FishySmell1 == false && DataBaseManager.Intel_FishySmell1 == true)
        {
            ON_FishySmell1 = true;
            Detail_FishySmell1.SetActive(true);
            Detail_FishySmell1.transform.SetAsLastSibling();
            Count_FishySmell += 1;
        }
        if (ON_FishySmell2 == false && DataBaseManager.Intel_FishySmell2 == true)
        {
            ON_FishySmell2 = true;
            Detail_FishySmell2.SetActive(true);
            Detail_FishySmell2.transform.SetAsLastSibling();
            Count_FishySmell += 1;
        }
        if (ON_FishySmell3 == false && DataBaseManager.Intel_FishySmell3 == true)
        {
            ON_FishySmell3 = true;
            Detail_FishySmell3.SetActive(true);
            Detail_FishySmell3.transform.SetAsLastSibling();
            Count_FishySmell += 1;
        }
        if (ON_FishySmell4 == false && DataBaseManager.Intel_FishySmell4 == true)
        {
            ON_FishySmell4 = true;
            Detail_FishySmell4.SetActive(true);
            Detail_FishySmell4.transform.SetAsLastSibling();
            Count_FishySmell += 1;
        }
        if (ON_FishySmell5 == false && DataBaseManager.Intel_FishySmell5 == true)
        {
            ON_FishySmell5 = true;
            Detail_FishySmell5.SetActive(true);
            Detail_FishySmell5.transform.SetAsLastSibling();
            Count_FishySmell += 1;
        }
        if (ON_FishySmell6 == false && DataBaseManager.Intel_FishySmell6 == true)
        {
            ON_FishySmell6 = true;
            Detail_FishySmell6.SetActive(true);
            Detail_FishySmell6.transform.SetAsLastSibling();
            Count_FishySmell += 1;
        }

        if (Count_FishySmell > 2 && DataBaseManager.NowPage_FishySmell == 1)
        {
            NextButton_FishySmell.SetActive(true);
        }
        else if (Count_FishySmell > 4 && DataBaseManager.NowPage_FishySmell == 2)
        {
            NextButton_FishySmell.SetActive(true);
        }
        else
        {
            NextButton_FishySmell.SetActive(false);
        }

        if (DataBaseManager.NowPage_FishySmell == 2)
        {
            PrevButton_FishySmell.SetActive(true);
        }
        else if (DataBaseManager.NowPage_FishySmell
            == 3)
        {
            PrevButton_FishySmell.SetActive(true);
        }
        else
        {
            PrevButton_FishySmell.SetActive(false);
        }
    }
    void RiverWaterQualityDetail()
    {
        if (ON_RiverWaterQuality1 == false && DataBaseManager.Intel_RiverWaterQuality1 == true)
        {
            ON_RiverWaterQuality1 = true;
            Detail_RiverWaterQuality1.SetActive(true);
            Detail_RiverWaterQuality1.transform.SetAsLastSibling();
            Count_RiverWaterQuality += 1;
        }
        if (ON_RiverWaterQuality2 == false && DataBaseManager.Intel_RiverWaterQuality2 == true)
        {
            ON_RiverWaterQuality2 = true;
            Detail_RiverWaterQuality2.SetActive(true);
            Detail_RiverWaterQuality2.transform.SetAsLastSibling();
            Count_RiverWaterQuality += 1;
        }
        if (ON_RiverWaterQuality3 == false && DataBaseManager.Intel_RiverWaterQuality3 == true)
        {
            ON_RiverWaterQuality3 = true;
            Detail_RiverWaterQuality3.SetActive(true);
            Detail_RiverWaterQuality3.transform.SetAsLastSibling();
            Count_RiverWaterQuality += 1;
        }
        if (ON_RiverWaterQuality4 == false && DataBaseManager.Intel_RiverWaterQuality4 == true)
        {
            ON_RiverWaterQuality4 = true;
            Detail_RiverWaterQuality4.SetActive(true);
            Detail_RiverWaterQuality4.transform.SetAsLastSibling();
            Count_RiverWaterQuality += 1;
        }
        if (ON_RiverWaterQuality5 == false && DataBaseManager.Intel_RiverWaterQuality5 == true)
        {
            ON_RiverWaterQuality5 = true;
            Detail_RiverWaterQuality5.SetActive(true);
            Detail_RiverWaterQuality5.transform.SetAsLastSibling();
            Count_RiverWaterQuality += 1;
        }
        if (ON_RiverWaterQuality6 == false && DataBaseManager.Intel_RiverWaterQuality6 == true)
        {
            ON_RiverWaterQuality6 = true;
            Detail_RiverWaterQuality6.SetActive(true);
            Detail_RiverWaterQuality6.transform.SetAsLastSibling();
            Count_RiverWaterQuality += 1;
        }

        if (Count_RiverWaterQuality > 2 && DataBaseManager.NowPage_RiverWaterQuality == 1)
        {
            NextButton_RiverWaterQuality.SetActive(true);
        }
        else if (Count_RiverWaterQuality > 4 && DataBaseManager.NowPage_RiverWaterQuality == 2)
        {
            NextButton_RiverWaterQuality.SetActive(true);
        }
        else
        {
            NextButton_RiverWaterQuality.SetActive(false);
        }

        if (DataBaseManager.NowPage_RiverWaterQuality == 2)
        {
            PrevButton_RiverWaterQuality.SetActive(true);
        }
        else if (DataBaseManager.NowPage_RiverWaterQuality
            == 3)
        {
            PrevButton_RiverWaterQuality.SetActive(true);
        }
        else
        {
            PrevButton_RiverWaterQuality.SetActive(false);
        }
    }
    void CreepyEyesDetail()
    {
        if (ON_CreepyEyes1 == false && DataBaseManager.Intel_CreepyEyes1 == true)
        {
            ON_CreepyEyes1 = true;
            Detail_CreepyEyes1.SetActive(true);
            Detail_CreepyEyes1.transform.SetAsLastSibling();
            Count_CreepyEyes += 1;
        }
        if (ON_CreepyEyes2 == false && DataBaseManager.Intel_CreepyEyes2 == true)
        {
            ON_CreepyEyes2 = true;
            Detail_CreepyEyes2.SetActive(true);
            Detail_CreepyEyes2.transform.SetAsLastSibling();
            Count_CreepyEyes += 1;
        }
        if (ON_CreepyEyes3 == false && DataBaseManager.Intel_CreepyEyes3 == true)
        {
            ON_CreepyEyes3 = true;
            Detail_CreepyEyes3.SetActive(true);
            Detail_CreepyEyes3.transform.SetAsLastSibling();
            Count_CreepyEyes += 1;
        }
        if (ON_CreepyEyes4 == false && DataBaseManager.Intel_CreepyEyes4 == true)
        {
            ON_CreepyEyes4 = true;
            Detail_CreepyEyes4.SetActive(true);
            Detail_CreepyEyes4.transform.SetAsLastSibling();
            Count_CreepyEyes += 1;
        }
        if (ON_CreepyEyes5 == false && DataBaseManager.Intel_CreepyEyes5 == true)
        {
            ON_CreepyEyes5 = true;
            Detail_CreepyEyes5.SetActive(true);
            Detail_CreepyEyes5.transform.SetAsLastSibling();
            Count_CreepyEyes += 1;
        }
        if (ON_CreepyEyes6 == false && DataBaseManager.Intel_CreepyEyes6 == true)
        {
            ON_CreepyEyes6 = true;
            Detail_CreepyEyes6.SetActive(true);
            Detail_CreepyEyes6.transform.SetAsLastSibling();
            Count_CreepyEyes += 1;
        }

        if (Count_CreepyEyes > 2 && DataBaseManager.NowPage_CreepyEyes == 1)
        {
            NextButton_CreepyEyes.SetActive(true);
        }
        else if (Count_CreepyEyes > 4 && DataBaseManager.NowPage_CreepyEyes == 2)
        {
            NextButton_CreepyEyes.SetActive(true);
        }
        else
        {
            NextButton_CreepyEyes.SetActive(false);
        }

        if (DataBaseManager.NowPage_CreepyEyes == 2)
        {
            PrevButton_CreepyEyes.SetActive(true);
        }
        else if (DataBaseManager.NowPage_CreepyEyes == 3)
        {
            PrevButton_CreepyEyes.SetActive(true);
        }
        else
        {
            PrevButton_CreepyEyes.SetActive(false);
        }
    }

    void SafeDetail()
    {
        if (ON_Safe1 == false && DataBaseManager.Intel_Safe1 == true)
        {
            ON_Safe1 = true;
            Detail_Safe1.SetActive(true);
            Detail_Safe1.transform.SetAsLastSibling();
            Count_Safe += 1;
        }
        if (ON_Safe2 == false && DataBaseManager.Intel_Safe2 == true)
        {
            ON_Safe2 = true;
            Detail_Safe2.SetActive(true);
            Detail_Safe2.transform.SetAsLastSibling();
            Count_Safe += 1;
        }
        if (ON_Safe3 == false && DataBaseManager.Intel_Safe3 == true)
        {
            ON_Safe3 = true;
            Detail_Safe3.SetActive(true);
            Detail_Safe3.transform.SetAsLastSibling();
            Count_Safe += 1;
        }
        if (ON_Safe4 == false && DataBaseManager.Intel_Safe4 == true)
        {
            ON_Safe4 = true;
            Detail_Safe4.SetActive(true);
            Detail_Safe4.transform.SetAsLastSibling();
            Count_Safe += 1;
        }
        if (ON_Safe5 == false && DataBaseManager.Intel_Safe5 == true)
        {
            ON_Safe5 = true;
            Detail_Safe5.SetActive(true);
            Detail_Safe5.transform.SetAsLastSibling();
            Count_Safe += 1;
        }
        if (ON_Safe6 == false && DataBaseManager.Intel_Safe6 == true)
        {
            ON_Safe6 = true;
            Detail_Safe6.SetActive(true);
            Detail_Safe6.transform.SetAsLastSibling();
            Count_Safe += 1;
        }

        if (Count_Safe > 2 && DataBaseManager.NowPage_Safe == 1)
        {
            NextButton_Safe.SetActive(true);
        }
        else if (Count_Safe > 4 && DataBaseManager.NowPage_Safe == 2)
        {
            NextButton_Safe.SetActive(true);
        }
        else
        {
            NextButton_Safe.SetActive(false);
        }

        if (DataBaseManager.NowPage_Safe == 2)
        {
            PrevButton_Safe.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Safe == 3)
        {
            PrevButton_Safe.SetActive(true);
        }
        else
        {
            PrevButton_Safe.SetActive(false);
        }
    }
    void SuddenChangeDetail()
    {
        if (ON_SuddenChange1 == false && DataBaseManager.Intel_SuddenChange1 == true)
        {
            ON_SuddenChange1 = true;
            Detail_SuddenChange1.SetActive(true);
            Detail_SuddenChange1.transform.SetAsLastSibling();
            Count_SuddenChange += 1;
        }
        if (ON_SuddenChange2 == false && DataBaseManager.Intel_SuddenChange2 == true)
        {
            ON_SuddenChange2 = true;
            Detail_SuddenChange2.SetActive(true);
            Detail_SuddenChange2.transform.SetAsLastSibling();
            Count_SuddenChange += 1;
        }
        if (ON_SuddenChange3 == false && DataBaseManager.Intel_SuddenChange3 == true)
        {
            ON_SuddenChange3 = true;
            Detail_SuddenChange3.SetActive(true);
            Detail_SuddenChange3.transform.SetAsLastSibling();
            Count_SuddenChange += 1;
        }
        if (ON_SuddenChange4 == false && DataBaseManager.Intel_SuddenChange4 == true)
        {
            ON_SuddenChange4 = true;
            Detail_SuddenChange4.SetActive(true);
            Detail_SuddenChange4.transform.SetAsLastSibling();
            Count_SuddenChange += 1;
        }
        if (ON_SuddenChange5 == false && DataBaseManager.Intel_SuddenChange5 == true)
        {
            ON_SuddenChange5 = true;
            Detail_SuddenChange5.SetActive(true);
            Detail_SuddenChange5.transform.SetAsLastSibling();
            Count_SuddenChange += 1;
        }
        if (ON_SuddenChange6 == false && DataBaseManager.Intel_SuddenChange6 == true)
        {
            ON_SuddenChange6 = true;
            Detail_SuddenChange6.SetActive(true);
            Detail_SuddenChange6.transform.SetAsLastSibling();
            Count_SuddenChange += 1;
        }

        if (Count_SuddenChange > 2 && DataBaseManager.NowPage_SuddenChange == 1)
        {
            NextButton_SuddenChange.SetActive(true);
        }
        else if (Count_SuddenChange > 4 && DataBaseManager.NowPage_SuddenChange == 2)
        {
            NextButton_SuddenChange.SetActive(true);
        }
        else
        {
            NextButton_SuddenChange.SetActive(false);
        }

        if (DataBaseManager.NowPage_SuddenChange == 2)
        {
            PrevButton_SuddenChange.SetActive(true);
        }
        else if (DataBaseManager.NowPage_SuddenChange == 3)
        {
            PrevButton_SuddenChange.SetActive(true);
        }
        else
        {
            PrevButton_SuddenChange.SetActive(false);
        }
    }


    public void NextPage()
    {
        if (NowPage == "PlanetarySequence")
        {
            if (DataBaseManager.NowPage_PlanetarySequence == 1)
            {
                if (Count_PlanetarySequence >= 6)
                {
                    Reset_PlanetarySequence();
                    PlanetarySequenceContents.transform.GetChild(2).gameObject.SetActive(true);
                    PlanetarySequenceContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_PlanetarySequence >= 5)
                {
                    Reset_PlanetarySequence();
                    PlanetarySequenceContents.transform.GetChild(3).gameObject.SetActive(true);
                    PlanetarySequenceContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_PlanetarySequence >= 4)
                {
                    Reset_PlanetarySequence();
                    PlanetarySequenceContents.transform.GetChild(4).gameObject.SetActive(true);
                    PlanetarySequenceContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_PlanetarySequence >= 3)
                {
                    Reset_PlanetarySequence();
                    PlanetarySequenceContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_PlanetarySequence = 2;
            }
            else if (DataBaseManager.NowPage_PlanetarySequence == 2)
            {
                if (Count_PlanetarySequence >= 6)
                {
                    Reset_PlanetarySequence();
                    PlanetarySequenceContents.transform.GetChild(4).gameObject.SetActive(true);
                    PlanetarySequenceContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_PlanetarySequence >= 5)
                {
                    Reset_PlanetarySequence();
                    PlanetarySequenceContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_PlanetarySequence = 3;
            }
        }
        if (NowPage == "Nightmare")
        {
            if (DataBaseManager.NowPage_Nightmare == 1)
            {
                if (Count_Nightmare >= 6)
                {
                    Reset_Nightmare();
                    NightmareContents.transform.GetChild(2).gameObject.SetActive(true);
                    NightmareContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Nightmare >= 5)
                {
                    Reset_Nightmare();
                    NightmareContents.transform.GetChild(3).gameObject.SetActive(true);
                    NightmareContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Nightmare >= 4)
                {
                    Reset_Nightmare();
                    NightmareContents.transform.GetChild(4).gameObject.SetActive(true);
                    NightmareContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Nightmare >= 3)
                {
                    Reset_Nightmare();
                    NightmareContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Nightmare = 2;
            }
            else if (DataBaseManager.NowPage_Nightmare == 2)
            {
                if (Count_Nightmare >= 6)
                {
                    Reset_Nightmare();
                    NightmareContents.transform.GetChild(4).gameObject.SetActive(true);
                    NightmareContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Nightmare >= 5)
                {
                    Reset_Nightmare();
                    NightmareContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Nightmare = 3;
            }
        }
        if (NowPage == "Paranoia")
        {
            if (DataBaseManager.NowPage_Paranoia == 1)
            {
                if (Count_Paranoia >= 6)
                {
                    Reset_Paranoia();
                    ParanoiaContents.transform.GetChild(2).gameObject.SetActive(true);
                    ParanoiaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Paranoia >= 5)
                {
                    Reset_Paranoia();
                    ParanoiaContents.transform.GetChild(3).gameObject.SetActive(true);
                    ParanoiaContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Paranoia >= 4)
                {
                    Reset_Paranoia();
                    ParanoiaContents.transform.GetChild(4).gameObject.SetActive(true);
                    ParanoiaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Paranoia >= 3)
                {
                    Reset_Paranoia();
                    ParanoiaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Paranoia = 2;
            }
            else if (DataBaseManager.NowPage_Paranoia == 2)
            {
                if (Count_Paranoia >= 6)
                {
                    Reset_Paranoia();
                    ParanoiaContents.transform.GetChild(4).gameObject.SetActive(true);
                    ParanoiaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Paranoia >= 5)
                {
                    Reset_Paranoia();
                    ParanoiaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Paranoia = 3;
            }
        }
        if (NowPage == "Insomnia")
        {
            if (DataBaseManager.NowPage_Insomnia == 1)
            {
                if (Count_Insomnia >= 6)
                {
                    Reset_Insomnia();
                    InsomniaContents.transform.GetChild(2).gameObject.SetActive(true);
                    InsomniaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Insomnia >= 5)
                {
                    Reset_Insomnia();
                    InsomniaContents.transform.GetChild(3).gameObject.SetActive(true);
                    InsomniaContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Insomnia >= 4)
                {
                    Reset_Insomnia();
                    InsomniaContents.transform.GetChild(4).gameObject.SetActive(true);
                    InsomniaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Insomnia >= 3)
                {
                    Reset_Insomnia();
                    InsomniaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Insomnia = 2;
            }
            else if (DataBaseManager.NowPage_Insomnia == 2)
            {
                if (Count_Insomnia >= 6)
                {
                    Reset_Insomnia();
                    InsomniaContents.transform.GetChild(4).gameObject.SetActive(true);
                    InsomniaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Insomnia >= 5)
                {
                    Reset_Insomnia();
                    InsomniaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Insomnia = 3;
            }
        }
        if (NowPage == "SewerGhostStory")
        {
            if (DataBaseManager.NowPage_SewerGhostStory == 1)
            {
                if (Count_SewerGhostStory >= 6)
                {
                    Reset_SewerGhostStory();
                    SewerGhostStoryContents.transform.GetChild(2).gameObject.SetActive(true);
                    SewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_SewerGhostStory >= 5)
                {
                    Reset_SewerGhostStory();
                    SewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(true);
                    SewerGhostStoryContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_SewerGhostStory >= 4)
                {
                    Reset_SewerGhostStory();
                    SewerGhostStoryContents.transform.GetChild(4).gameObject.SetActive(true);
                    SewerGhostStoryContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_SewerGhostStory >= 3)
                {
                    Reset_SewerGhostStory();
                    SewerGhostStoryContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewerGhostStory = 2;
            }
            else if (DataBaseManager.NowPage_SewerGhostStory == 2)
            {
                if (Count_SewerGhostStory >= 6)
                {
                    Reset_SewerGhostStory();
                    SewerGhostStoryContents.transform.GetChild(4).gameObject.SetActive(true);
                    SewerGhostStoryContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_SewerGhostStory >= 5)
                {
                    Reset_SewerGhostStory();
                    SewerGhostStoryContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewerGhostStory = 3;
            }
        }
        if (NowPage == "MissingPeople")
        {
            if (DataBaseManager.NowPage_MissingPeople == 1)
            {
                if (Count_MissingPeople >= 6)
                {
                    Reset_MissingPeople();
                    MissingPeopleContents.transform.GetChild(2).gameObject.SetActive(true);
                    MissingPeopleContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_MissingPeople >= 5)
                {
                    Reset_MissingPeople();
                    MissingPeopleContents.transform.GetChild(3).gameObject.SetActive(true);
                    MissingPeopleContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_MissingPeople >= 4)
                {
                    Reset_MissingPeople();
                    MissingPeopleContents.transform.GetChild(4).gameObject.SetActive(true);
                    MissingPeopleContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_MissingPeople >= 3)
                {
                    Reset_MissingPeople();
                    MissingPeopleContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_MissingPeople = 2;
            }
            else if (DataBaseManager.NowPage_MissingPeople == 2)
            {
                if (Count_MissingPeople >= 6)
                {
                    Reset_MissingPeople();
                    MissingPeopleContents.transform.GetChild(4).gameObject.SetActive(true);
                    MissingPeopleContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_MissingPeople >= 5)
                {
                    Reset_MissingPeople();
                    MissingPeopleContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_MissingPeople = 3;
            }
        }
        if (NowPage == "FishySmell")
        {
            if (DataBaseManager.NowPage_FishySmell == 1)
            {
                if (Count_FishySmell >= 6)
                {
                    Reset_FishySmell();
                    FishySmellContents.transform.GetChild(2).gameObject.SetActive(true);
                    FishySmellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_FishySmell >= 5)
                {
                    Reset_FishySmell();
                    FishySmellContents.transform.GetChild(3).gameObject.SetActive(true);
                    FishySmellContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_FishySmell >= 4)
                {
                    Reset_FishySmell();
                    FishySmellContents.transform.GetChild(4).gameObject.SetActive(true);
                    FishySmellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_FishySmell >= 3)
                {
                    Reset_FishySmell();
                    FishySmellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_FishySmell = 2;
            }
            else if (DataBaseManager.NowPage_FishySmell == 2)
            {
                if (Count_FishySmell >= 6)
                {
                    Reset_FishySmell();
                    FishySmellContents.transform.GetChild(4).gameObject.SetActive(true);
                    FishySmellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_FishySmell >= 5)
                {
                    Reset_FishySmell();
                    FishySmellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_FishySmell = 3;
            }
        }
        if (NowPage == "RiverWaterQuality")
        {
            if (DataBaseManager.NowPage_RiverWaterQuality == 1)
            {
                if (Count_RiverWaterQuality >= 6)
                {
                    Reset_RiverWaterQuality();
                    RiverWaterQualityContents.transform.GetChild(2).gameObject.SetActive(true);
                    RiverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_RiverWaterQuality >= 5)
                {
                    Reset_RiverWaterQuality();
                    RiverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(true);
                    RiverWaterQualityContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_RiverWaterQuality >= 4)
                {
                    Reset_RiverWaterQuality();
                    RiverWaterQualityContents.transform.GetChild(4).gameObject.SetActive(true);
                    RiverWaterQualityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_RiverWaterQuality >= 3)
                {
                    Reset_RiverWaterQuality();
                    RiverWaterQualityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_RiverWaterQuality = 2;
            }
            else if (DataBaseManager.NowPage_RiverWaterQuality == 2)
            {
                if (Count_RiverWaterQuality >= 6)
                {
                    Reset_RiverWaterQuality();
                    RiverWaterQualityContents.transform.GetChild(4).gameObject.SetActive(true);
                    RiverWaterQualityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_RiverWaterQuality >= 5)
                {
                    Reset_RiverWaterQuality();
                    RiverWaterQualityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_RiverWaterQuality = 3;
            }
        }
        if (NowPage == "CreepyEyes")
        {
            if (DataBaseManager.NowPage_CreepyEyes == 1)
            {
                if (Count_CreepyEyes >= 6)
                {
                    Reset_CreepyEyes();
                    CreepyEyesContents.transform.GetChild(2).gameObject.SetActive(true);
                    CreepyEyesContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_CreepyEyes >= 5)
                {
                    Reset_CreepyEyes();
                    CreepyEyesContents.transform.GetChild(3).gameObject.SetActive(true);
                    CreepyEyesContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_CreepyEyes >= 4)
                {
                    Reset_CreepyEyes();
                    CreepyEyesContents.transform.GetChild(4).gameObject.SetActive(true);
                    CreepyEyesContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_CreepyEyes >= 3)
                {
                    Reset_CreepyEyes();
                    CreepyEyesContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_CreepyEyes = 2;
            }
            else if (DataBaseManager.NowPage_CreepyEyes == 2)
            {
                if (Count_CreepyEyes >= 6)
                {
                    Reset_CreepyEyes();
                    CreepyEyesContents.transform.GetChild(4).gameObject.SetActive(true);
                    CreepyEyesContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_CreepyEyes >= 5)
                {
                    Reset_CreepyEyes();
                    CreepyEyesContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_CreepyEyes = 3;
            }
        }

        if (NowPage == "Safe")
        {
            if (DataBaseManager.NowPage_Safe == 1)
            {
                if (Count_Safe >= 6)
                {
                    Reset_Safe();
                    SafeContents.transform.GetChild(2).gameObject.SetActive(true);
                    SafeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Safe >= 5)
                {
                    Reset_Safe();
                    SafeContents.transform.GetChild(3).gameObject.SetActive(true);
                    SafeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Safe >= 4)
                {
                    Reset_Safe();
                    SafeContents.transform.GetChild(4).gameObject.SetActive(true);
                    SafeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Safe >= 3)
                {
                    Reset_Safe();
                    SafeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Safe = 2;
            }
            else if (DataBaseManager.NowPage_Safe == 2)
            {
                if (Count_Safe >= 6)
                {
                    Reset_Safe();
                    SafeContents.transform.GetChild(4).gameObject.SetActive(true);
                    SafeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Safe >= 5)
                {
                    Reset_Safe();
                    SafeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Safe = 3;
            }
        }
        if (NowPage == "SuddenChange")
        {
            if (DataBaseManager.NowPage_SuddenChange == 1)
            {
                if (Count_SuddenChange >= 6)
                {
                    Reset_SuddenChange();
                    SuddenChangeContents.transform.GetChild(2).gameObject.SetActive(true);
                    SuddenChangeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_SuddenChange >= 5)
                {
                    Reset_SuddenChange();
                    SuddenChangeContents.transform.GetChild(3).gameObject.SetActive(true);
                    SuddenChangeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_SuddenChange >= 4)
                {
                    Reset_SuddenChange();
                    SuddenChangeContents.transform.GetChild(4).gameObject.SetActive(true);
                    SuddenChangeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_SuddenChange >= 3)
                {
                    Reset_SuddenChange();
                    SuddenChangeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SuddenChange = 2;
            }
            else if (DataBaseManager.NowPage_SuddenChange == 2)
            {
                if (Count_SuddenChange >= 6)
                {
                    Reset_SuddenChange();
                    SuddenChangeContents.transform.GetChild(4).gameObject.SetActive(true);
                    SuddenChangeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_SuddenChange >= 5)
                {
                    Reset_SuddenChange();
                    SuddenChangeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SuddenChange = 3;
            }
        }
    }
    public void PrevPage()
    {
        if (NowPage == "PlanetarySequence")
        {
            if (DataBaseManager.NowPage_PlanetarySequence == 2)
            {
                if (Count_PlanetarySequence >= 6)
                {
                    Reset_PlanetarySequence();
                    PlanetarySequenceContents.transform.GetChild(0).gameObject.SetActive(true);
                    PlanetarySequenceContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_PlanetarySequence >= 5)
                {
                    Reset_PlanetarySequence();
                    PlanetarySequenceContents.transform.GetChild(1).gameObject.SetActive(true);
                    PlanetarySequenceContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_PlanetarySequence >= 4)
                {
                    Reset_PlanetarySequence();
                    PlanetarySequenceContents.transform.GetChild(2).gameObject.SetActive(true);
                    PlanetarySequenceContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_PlanetarySequence >= 3)
                {
                    Reset_PlanetarySequence();
                    PlanetarySequenceContents.transform.GetChild(3).gameObject.SetActive(true);
                    PlanetarySequenceContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_PlanetarySequence = 1;
            }
            else if (DataBaseManager.NowPage_PlanetarySequence == 3)
            {
                if (Count_PlanetarySequence >= 6)
                {
                    Reset_PlanetarySequence();
                    PlanetarySequenceContents.transform.GetChild(2).gameObject.SetActive(true);
                    PlanetarySequenceContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_PlanetarySequence >= 5)
                {
                    Reset_PlanetarySequence();
                    PlanetarySequenceContents.transform.GetChild(3).gameObject.SetActive(true);
                    PlanetarySequenceContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_PlanetarySequence = 2;
            }
        }
        if (NowPage == "Nightmare")
        {
            if (DataBaseManager.NowPage_Nightmare == 2)
            {
                if (Count_Nightmare >= 6)
                {
                    Reset_Nightmare();
                    NightmareContents.transform.GetChild(0).gameObject.SetActive(true);
                    NightmareContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Nightmare >= 5)
                {
                    Reset_Nightmare();
                    NightmareContents.transform.GetChild(1).gameObject.SetActive(true);
                    NightmareContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Nightmare >= 4)
                {
                    Reset_Nightmare();
                    NightmareContents.transform.GetChild(2).gameObject.SetActive(true);
                    NightmareContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Nightmare >= 3)
                {
                    Reset_Nightmare();
                    NightmareContents.transform.GetChild(3).gameObject.SetActive(true);
                    NightmareContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Nightmare = 1;
            }
            else if (DataBaseManager.NowPage_Nightmare == 3)
            {
                if (Count_Nightmare >= 6)
                {
                    Reset_Nightmare();
                    NightmareContents.transform.GetChild(2).gameObject.SetActive(true);
                    NightmareContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Nightmare >= 5)
                {
                    Reset_Nightmare();
                    NightmareContents.transform.GetChild(3).gameObject.SetActive(true);
                    NightmareContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Nightmare = 2;
            }
        }
        if (NowPage == "Paranoia")
        {
            if (DataBaseManager.NowPage_Paranoia == 2)
            {
                if (Count_Paranoia >= 6)
                {
                    Reset_Paranoia();
                    ParanoiaContents.transform.GetChild(0).gameObject.SetActive(true);
                    ParanoiaContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Paranoia >= 5)
                {
                    Reset_Paranoia();
                    ParanoiaContents.transform.GetChild(1).gameObject.SetActive(true);
                    ParanoiaContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Paranoia >= 4)
                {
                    Reset_Paranoia();
                    ParanoiaContents.transform.GetChild(2).gameObject.SetActive(true);
                    ParanoiaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Paranoia >= 3)
                {
                    Reset_Paranoia();
                    ParanoiaContents.transform.GetChild(3).gameObject.SetActive(true);
                    ParanoiaContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Paranoia = 1;
            }
            else if (DataBaseManager.NowPage_Paranoia == 3)
            {
                if (Count_Paranoia >= 6)
                {
                    Reset_Paranoia();
                    ParanoiaContents.transform.GetChild(2).gameObject.SetActive(true);
                    ParanoiaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Paranoia >= 5)
                {
                    Reset_Paranoia();
                    ParanoiaContents.transform.GetChild(3).gameObject.SetActive(true);
                    ParanoiaContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Paranoia = 2;
            }
        }
        if (NowPage == "Insomnia")
        {
            if (DataBaseManager.NowPage_Insomnia == 2)
            {
                if (Count_Insomnia >= 6)
                {
                    Reset_Insomnia();
                    InsomniaContents.transform.GetChild(0).gameObject.SetActive(true);
                    InsomniaContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Insomnia >= 5)
                {
                    Reset_Insomnia();
                    InsomniaContents.transform.GetChild(1).gameObject.SetActive(true);
                    InsomniaContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Insomnia >= 4)
                {
                    Reset_Insomnia();
                    InsomniaContents.transform.GetChild(2).gameObject.SetActive(true);
                    InsomniaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Insomnia >= 3)
                {
                    Reset_Insomnia();
                    InsomniaContents.transform.GetChild(3).gameObject.SetActive(true);
                    InsomniaContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Insomnia = 1;
            }
            else if (DataBaseManager.NowPage_Insomnia == 3)
            {
                if (Count_Insomnia >= 6)
                {
                    Reset_Insomnia();
                    InsomniaContents.transform.GetChild(2).gameObject.SetActive(true);
                    InsomniaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Insomnia >= 5)
                {
                    Reset_Insomnia();
                    InsomniaContents.transform.GetChild(3).gameObject.SetActive(true);
                    InsomniaContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Insomnia = 2;
            }
        }
        if (NowPage == "SewerGhostStory")
        {
            if (DataBaseManager.NowPage_SewerGhostStory == 2)
            {
                if (Count_SewerGhostStory >= 6)
                {
                    Reset_SewerGhostStory();
                    SewerGhostStoryContents.transform.GetChild(0).gameObject.SetActive(true);
                    SewerGhostStoryContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_SewerGhostStory >= 5)
                {
                    Reset_SewerGhostStory();
                    SewerGhostStoryContents.transform.GetChild(1).gameObject.SetActive(true);
                    SewerGhostStoryContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_SewerGhostStory >= 4)
                {
                    Reset_SewerGhostStory();
                    SewerGhostStoryContents.transform.GetChild(2).gameObject.SetActive(true);
                    SewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_SewerGhostStory >= 3)
                {
                    Reset_SewerGhostStory();
                    SewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(true);
                    SewerGhostStoryContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_SewerGhostStory = 1;
            }
            else if (DataBaseManager.NowPage_SewerGhostStory == 3)
            {
                if (Count_SewerGhostStory >= 6)
                {
                    Reset_SewerGhostStory();
                    SewerGhostStoryContents.transform.GetChild(2).gameObject.SetActive(true);
                    SewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_SewerGhostStory >= 5)
                {
                    Reset_SewerGhostStory();
                    SewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(true);
                    SewerGhostStoryContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewerGhostStory = 2;
            }
        }
        if (NowPage == "MissingPeople")
        {
            if (DataBaseManager.NowPage_MissingPeople == 2)
            {
                if (Count_MissingPeople >= 6)
                {
                    Reset_MissingPeople();
                    MissingPeopleContents.transform.GetChild(0).gameObject.SetActive(true);
                    MissingPeopleContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_MissingPeople >= 5)
                {
                    Reset_MissingPeople();
                    MissingPeopleContents.transform.GetChild(1).gameObject.SetActive(true);
                    MissingPeopleContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_MissingPeople >= 4)
                {
                    Reset_MissingPeople();
                    MissingPeopleContents.transform.GetChild(2).gameObject.SetActive(true);
                    MissingPeopleContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_MissingPeople >= 3)
                {
                    Reset_MissingPeople();
                    MissingPeopleContents.transform.GetChild(3).gameObject.SetActive(true);
                    MissingPeopleContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_MissingPeople = 1;
            }
            else if (DataBaseManager.NowPage_MissingPeople == 3)
            {
                if (Count_MissingPeople >= 6)
                {
                    Reset_MissingPeople();
                    MissingPeopleContents.transform.GetChild(2).gameObject.SetActive(true);
                    MissingPeopleContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_MissingPeople >= 5)
                {
                    Reset_MissingPeople();
                    MissingPeopleContents.transform.GetChild(3).gameObject.SetActive(true);
                    MissingPeopleContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_MissingPeople = 2;
            }
        }
        if (NowPage == "FishySmell")
        {
            if (DataBaseManager.NowPage_FishySmell == 2)
            {
                if (Count_FishySmell >= 6)
                {
                    Reset_FishySmell();
                    FishySmellContents.transform.GetChild(0).gameObject.SetActive(true);
                    FishySmellContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_FishySmell >= 5)
                {
                    Reset_FishySmell();
                    FishySmellContents.transform.GetChild(1).gameObject.SetActive(true);
                    FishySmellContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_FishySmell >= 4)
                {
                    Reset_FishySmell();
                    FishySmellContents.transform.GetChild(2).gameObject.SetActive(true);
                    FishySmellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_FishySmell >= 3)
                {
                    Reset_FishySmell();
                    FishySmellContents.transform.GetChild(3).gameObject.SetActive(true);
                    FishySmellContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_FishySmell = 1;
            }
            else if (DataBaseManager.NowPage_FishySmell == 3)
            {
                if (Count_FishySmell >= 6)
                {
                    Reset_FishySmell();
                    FishySmellContents.transform.GetChild(2).gameObject.SetActive(true);
                    FishySmellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_FishySmell >= 5)
                {
                    Reset_FishySmell();
                    FishySmellContents.transform.GetChild(3).gameObject.SetActive(true);
                    FishySmellContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_FishySmell = 2;
            }
        }
        if (NowPage == "RiverWaterQuality")
        {
            if (DataBaseManager.NowPage_RiverWaterQuality == 2)
            {
                if (Count_RiverWaterQuality >= 6)
                {
                    Reset_RiverWaterQuality();
                    RiverWaterQualityContents.transform.GetChild(0).gameObject.SetActive(true);
                    RiverWaterQualityContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_RiverWaterQuality >= 5)
                {
                    Reset_RiverWaterQuality();
                    RiverWaterQualityContents.transform.GetChild(1).gameObject.SetActive(true);
                    RiverWaterQualityContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_RiverWaterQuality >= 4)
                {
                    Reset_RiverWaterQuality();
                    RiverWaterQualityContents.transform.GetChild(2).gameObject.SetActive(true);
                    RiverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_RiverWaterQuality >= 3)
                {
                    Reset_RiverWaterQuality();
                    RiverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(true);
                    RiverWaterQualityContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_RiverWaterQuality = 1;
            }
            else if (DataBaseManager.NowPage_RiverWaterQuality == 3)
            {
                if (Count_RiverWaterQuality >= 6)
                {
                    Reset_RiverWaterQuality();
                    RiverWaterQualityContents.transform.GetChild(2).gameObject.SetActive(true);
                    RiverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_RiverWaterQuality >= 5)
                {
                    Reset_RiverWaterQuality();
                    RiverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(true);
                    RiverWaterQualityContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_RiverWaterQuality = 2;
            }
        }
        if (NowPage == "CreepyEyes")
        {
            if (DataBaseManager.NowPage_CreepyEyes == 2)
            {
                if (Count_CreepyEyes >= 6)
                {
                    Reset_CreepyEyes();
                    CreepyEyesContents.transform.GetChild(0).gameObject.SetActive(true);
                    CreepyEyesContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_CreepyEyes >= 5)
                {
                    Reset_CreepyEyes();
                    CreepyEyesContents.transform.GetChild(1).gameObject.SetActive(true);
                    CreepyEyesContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_CreepyEyes >= 4)
                {
                    Reset_CreepyEyes();
                    CreepyEyesContents.transform.GetChild(2).gameObject.SetActive(true);
                    CreepyEyesContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_CreepyEyes >= 3)
                {
                    Reset_CreepyEyes();
                    CreepyEyesContents.transform.GetChild(3).gameObject.SetActive(true);
                    CreepyEyesContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_CreepyEyes = 1;
            }
            else if (DataBaseManager.NowPage_CreepyEyes == 3)
            {
                if (Count_CreepyEyes >= 6)
                {
                    Reset_CreepyEyes();
                    CreepyEyesContents.transform.GetChild(2).gameObject.SetActive(true);
                    CreepyEyesContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_CreepyEyes >= 5)
                {
                    Reset_CreepyEyes();
                    CreepyEyesContents.transform.GetChild(3).gameObject.SetActive(true);
                    CreepyEyesContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_CreepyEyes = 2;
            }
        }

        if (NowPage == "Safe")
        {
            if (DataBaseManager.NowPage_Safe == 2)
            {
                if (Count_Safe >= 6)
                {
                    Reset_Safe();
                    SafeContents.transform.GetChild(0).gameObject.SetActive(true);
                    SafeContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Safe >= 5)
                {
                    Reset_Safe();
                    SafeContents.transform.GetChild(1).gameObject.SetActive(true);
                    SafeContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Safe >= 4)
                {
                    Reset_Safe();
                    SafeContents.transform.GetChild(2).gameObject.SetActive(true);
                    SafeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Safe >= 3)
                {
                    Reset_Safe();
                    SafeContents.transform.GetChild(3).gameObject.SetActive(true);
                    SafeContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Safe = 1;
            }
            else if (DataBaseManager.NowPage_Safe == 3)
            {
                if (Count_Safe >= 6)
                {
                    Reset_Safe();
                    SafeContents.transform.GetChild(2).gameObject.SetActive(true);
                    SafeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Safe >= 5)
                {
                    Reset_Safe();
                    SafeContents.transform.GetChild(3).gameObject.SetActive(true);
                    SafeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Safe = 2;
            }
        }
        if (NowPage == "SuddenChange")
        {
            if (DataBaseManager.NowPage_SuddenChange == 2)
            {
                if (Count_SuddenChange >= 6)
                {
                    Reset_SuddenChange();
                    SuddenChangeContents.transform.GetChild(0).gameObject.SetActive(true);
                    SuddenChangeContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_SuddenChange >= 5)
                {
                    Reset_SuddenChange();
                    SuddenChangeContents.transform.GetChild(1).gameObject.SetActive(true);
                    SuddenChangeContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_SuddenChange >= 4)
                {
                    Reset_SuddenChange();
                    SuddenChangeContents.transform.GetChild(2).gameObject.SetActive(true);
                    SuddenChangeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_SuddenChange >= 3)
                {
                    Reset_SuddenChange();
                    SuddenChangeContents.transform.GetChild(3).gameObject.SetActive(true);
                    SuddenChangeContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_SuddenChange = 1;
            }
            else if (DataBaseManager.NowPage_SuddenChange == 3)
            {
                if (Count_SuddenChange >= 6)
                {
                    Reset_SuddenChange();
                    SuddenChangeContents.transform.GetChild(2).gameObject.SetActive(true);
                    SuddenChangeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_SuddenChange >= 5)
                {
                    Reset_SuddenChange();
                    SuddenChangeContents.transform.GetChild(3).gameObject.SetActive(true);
                    SuddenChangeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SuddenChange = 2;
            }
        }
    }

    void Reset_PlanetarySequence()
    {
        PlanetarySequenceContents.transform.GetChild(0).gameObject.SetActive(false);
        PlanetarySequenceContents.transform.GetChild(1).gameObject.SetActive(false);
        PlanetarySequenceContents.transform.GetChild(2).gameObject.SetActive(false);
        PlanetarySequenceContents.transform.GetChild(3).gameObject.SetActive(false);
        PlanetarySequenceContents.transform.GetChild(4).gameObject.SetActive(false);
        PlanetarySequenceContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Nightmare()
    {
        NightmareContents.transform.GetChild(0).gameObject.SetActive(false);
        NightmareContents.transform.GetChild(1).gameObject.SetActive(false);
        NightmareContents.transform.GetChild(2).gameObject.SetActive(false);
        NightmareContents.transform.GetChild(3).gameObject.SetActive(false);
        NightmareContents.transform.GetChild(4).gameObject.SetActive(false);
        NightmareContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Paranoia()
    {
        ParanoiaContents.transform.GetChild(0).gameObject.SetActive(false);
        ParanoiaContents.transform.GetChild(1).gameObject.SetActive(false);
        ParanoiaContents.transform.GetChild(2).gameObject.SetActive(false);
        ParanoiaContents.transform.GetChild(3).gameObject.SetActive(false);
        ParanoiaContents.transform.GetChild(4).gameObject.SetActive(false);
        ParanoiaContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Insomnia()
    {
        InsomniaContents.transform.GetChild(0).gameObject.SetActive(false);
        InsomniaContents.transform.GetChild(1).gameObject.SetActive(false);
        InsomniaContents.transform.GetChild(2).gameObject.SetActive(false);
        InsomniaContents.transform.GetChild(3).gameObject.SetActive(false);
        InsomniaContents.transform.GetChild(4).gameObject.SetActive(false);
        InsomniaContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_SewerGhostStory()
    {
        SewerGhostStoryContents.transform.GetChild(0).gameObject.SetActive(false);
        SewerGhostStoryContents.transform.GetChild(1).gameObject.SetActive(false);
        SewerGhostStoryContents.transform.GetChild(2).gameObject.SetActive(false);
        SewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(false);
        SewerGhostStoryContents.transform.GetChild(4).gameObject.SetActive(false);
        SewerGhostStoryContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_MissingPeople()
    {
        MissingPeopleContents.transform.GetChild(0).gameObject.SetActive(false);
        MissingPeopleContents.transform.GetChild(1).gameObject.SetActive(false);
        MissingPeopleContents.transform.GetChild(2).gameObject.SetActive(false);
        MissingPeopleContents.transform.GetChild(3).gameObject.SetActive(false);
        MissingPeopleContents.transform.GetChild(4).gameObject.SetActive(false);
        MissingPeopleContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_FishySmell()
    {
        FishySmellContents.transform.GetChild(0).gameObject.SetActive(false);
        FishySmellContents.transform.GetChild(1).gameObject.SetActive(false);
        FishySmellContents.transform.GetChild(2).gameObject.SetActive(false);
        FishySmellContents.transform.GetChild(3).gameObject.SetActive(false);
        FishySmellContents.transform.GetChild(4).gameObject.SetActive(false);
        FishySmellContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_RiverWaterQuality()
    {
        RiverWaterQualityContents.transform.GetChild(0).gameObject.SetActive(false);
        RiverWaterQualityContents.transform.GetChild(1).gameObject.SetActive(false);
        RiverWaterQualityContents.transform.GetChild(2).gameObject.SetActive(false);
        RiverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(false);
        RiverWaterQualityContents.transform.GetChild(4).gameObject.SetActive(false);
        RiverWaterQualityContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_CreepyEyes()
    {
        CreepyEyesContents.transform.GetChild(0).gameObject.SetActive(false);
        CreepyEyesContents.transform.GetChild(1).gameObject.SetActive(false);
        CreepyEyesContents.transform.GetChild(2).gameObject.SetActive(false);
        CreepyEyesContents.transform.GetChild(3).gameObject.SetActive(false);
        CreepyEyesContents.transform.GetChild(4).gameObject.SetActive(false);
        CreepyEyesContents.transform.GetChild(5).gameObject.SetActive(false);
    }

    void Reset_Safe()
    {
        SafeContents.transform.GetChild(0).gameObject.SetActive(false);
        SafeContents.transform.GetChild(1).gameObject.SetActive(false);
        SafeContents.transform.GetChild(2).gameObject.SetActive(false);
        SafeContents.transform.GetChild(3).gameObject.SetActive(false);
        SafeContents.transform.GetChild(4).gameObject.SetActive(false);
        SafeContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_SuddenChange()
    {
        SuddenChangeContents.transform.GetChild(0).gameObject.SetActive(false);
        SuddenChangeContents.transform.GetChild(1).gameObject.SetActive(false);
        SuddenChangeContents.transform.GetChild(2).gameObject.SetActive(false);
        SuddenChangeContents.transform.GetChild(3).gameObject.SetActive(false);
        SuddenChangeContents.transform.GetChild(4).gameObject.SetActive(false);
        SuddenChangeContents.transform.GetChild(5).gameObject.SetActive(false);
    }

    public void Open_PlanetarySequence()
    {
        CloseAllContents();
        PlanetarySequence_Detail.SetActive(true);
    }
    public void Open_Nightmare()
    {
        CloseAllContents();
        Nightmare_Detail.SetActive(true);
    }
    public void Open_Paranoia()
    {
        CloseAllContents();
        Paranoia_Detail.SetActive(true);
    }
    public void Open_Insomnia()
    {
        CloseAllContents();
        Insomnia_Detail.SetActive(true);
    }
    public void Open_SewerGhostStory()
    {
        CloseAllContents();
        SewerGhostStory_Detail.SetActive(true);
    }
    public void Open_MissingPeople()
    {
        CloseAllContents();
        MissingPeople_Detail.SetActive(true);
    }
    public void Open_FishySmell()
    {
        CloseAllContents();
        FishySmell_Detail.SetActive(true);
    }
    public void Open_RiverWaterQuality()
    {
        CloseAllContents();
        RiverWaterQuality_Detail.SetActive(true);
    }
    public void Open_CreepyEyes()
    {
        CloseAllContents();
        CreepyEyes_Detail.SetActive(true);
    }

    public void Open_Safe()
    {
        CloseAllContents();
        Safe_Detail.SetActive(true);
    }
    public void Open_SuddenChange()
    {
        CloseAllContents();
        SuddenChange_Detail.SetActive(true);
    }


    void CloseAllContents()
    {
        PlanetarySequence_Detail.SetActive(false);
        Paranoia_Detail.SetActive(false);
        Nightmare_Detail.SetActive(false);
        Insomnia_Detail.SetActive(false);
        SewerGhostStory_Detail.SetActive(false);
        MissingPeople_Detail.SetActive(false);
        FishySmell_Detail.SetActive(false);
        RiverWaterQuality_Detail.SetActive(false);
        CreepyEyes_Detail.SetActive(false);

        Safe_Detail.SetActive(false);
        SuddenChange_Detail.SetActive(false);
    }
}



