using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelEventDetail : MonoBehaviour
{
    GameObject detailPlanetarySequence1;
    GameObject detailPlanetarySequence2;
    GameObject detailPlanetarySequence3;
    GameObject detailPlanetarySequence4;
    GameObject detailPlanetarySequence5;
    GameObject detailPlanetarySequence6;
    bool onPlanetarySequence1;
    bool onPlanetarySequence2;
    bool onPlanetarySequence3;
    bool onPlanetarySequence4;
    bool onPlanetarySequence5;
    bool onPlanetarySequence6;
    int countPlanetarySequence = 0;
    public GameObject planetarySequenceContents;
    public GameObject planetarySequenceDetail;
    public static int planetarySequenceIntelInt;
    public GameObject nextButtonPlanetarySequence;
    public GameObject prevButtonPlanetarySequence;
    string nowPage;

    GameObject detailNightmare1;
    GameObject detailNightmare2;
    GameObject detailNightmare3;
    GameObject detailNightmare4;
    GameObject detailNightmare5;
    GameObject detailNightmare6;
    bool onNightmare1;
    bool onNightmare2;
    bool onNightmare3;
    bool onNightmare4;
    bool onNightmare5;
    bool onNightmare6;
    int countNightmare = 0;
    public GameObject nightmareContents;
    public GameObject nightmareDetail;
    public static int nightmareIntelInt;
    public GameObject nextButtonNightmare;
    public GameObject prevButtonNightmare;

    GameObject detailParanoia1;
    GameObject detailParanoia2;
    GameObject detailParanoia3;
    GameObject detailParanoia4;
    GameObject detailParanoia5;
    GameObject detailParanoia6;
    bool onParanoia1;
    bool onParanoia2;
    bool onParanoia3;
    bool onParanoia4;
    bool onParanoia5;
    bool onParanoia6;
    int countParanoia = 0;
    public GameObject paranoiaContents;
    public GameObject paranoiaDetail;
    public static int paranoiaIntelInt;
    public GameObject nextButtonParanoia;
    public GameObject prevButtonParanoia;

    GameObject detailInsomnia1;
    GameObject detailInsomnia2;
    GameObject detailInsomnia3;
    GameObject detailInsomnia4;
    GameObject detailInsomnia5;
    GameObject detailInsomnia6;
    bool onInsomnia1;
    bool onInsomnia2;
    bool onInsomnia3;
    bool onInsomnia4;
    bool onInsomnia5;
    bool onInsomnia6;
    int countInsomnia = 0;
    public GameObject insomniaContents;
    public GameObject insomniaDetail;
    public static int insomniaIntelInt;
    public GameObject nextButtonInsomnia;
    public GameObject prevButtonInsomnia;

    GameObject detailSewerGhostStory1;
    GameObject detailSewerGhostStory2;
    GameObject detailSewerGhostStory3;
    GameObject detailSewerGhostStory4;
    GameObject detailSewerGhostStory5;
    GameObject detailSewerGhostStory6;
    bool onSewerGhostStory1;
    bool onSewerGhostStory2;
    bool onSewerGhostStory3;
    bool onSewerGhostStory4;
    bool onSewerGhostStory5;
    bool onSewerGhostStory6;
    int countSewerGhostStory = 0;
    public GameObject sewerGhostStoryContents;
    public GameObject sewerGhostStoryDetail;
    public static int sewerGhostStoryIntelInt;
    public GameObject nextButtonSewerGhostStory;
    public GameObject prevButtonSewerGhostStory;

    GameObject detailMissingPeople1;
    GameObject detailMissingPeople2;
    GameObject detailMissingPeople3;
    GameObject detailMissingPeople4;
    GameObject detailMissingPeople5;
    GameObject detailMissingPeople6;
    bool onMissingPeople1;
    bool onMissingPeople2;
    bool onMissingPeople3;
    bool onMissingPeople4;
    bool onMissingPeople5;
    bool onMissingPeople6;
    int countMissingPeople = 0;
    public GameObject missingPeopleContents;
    public GameObject missingPeopleDetail;
    public static int missingPeopleIntelInt;
    public GameObject nextButtonMissingPeople;
    public GameObject prevButtonMissingPeople;

    GameObject detailFishySmell1;
    GameObject detailFishySmell2;
    GameObject detailFishySmell3;
    GameObject detailFishySmell4;
    GameObject detailFishySmell5;
    GameObject detailFishySmell6;
    bool onFishySmell1;
    bool onFishySmell2;
    bool onFishySmell3;
    bool onFishySmell4;
    bool onFishySmell5;
    bool onFishySmell6;
    int countFishySmell = 0;
    public GameObject fishySmellContents;
    public GameObject fishySmellDetail;
    public static int fishySmellIntelInt;
    public GameObject nextButtonFishySmell;
    public GameObject prevButtonFishySmell;

    GameObject detailRiverWaterQuality1;
    GameObject detailRiverWaterQuality2;
    GameObject detailRiverWaterQuality3;
    GameObject detailRiverWaterQuality4;
    GameObject detailRiverWaterQuality5;
    GameObject detailRiverWaterQuality6;
    bool onRiverWaterQuality1;
    bool onRiverWaterQuality2;
    bool onRiverWaterQuality3;
    bool onRiverWaterQuality4;
    bool onRiverWaterQuality5;
    bool onRiverWaterQuality6;
    int countRiverWaterQuality = 0;
    public GameObject riverWaterQualityContents;
    public GameObject riverWaterQualityDetail;
    public static int riverWaterQualityIntelInt;
    public GameObject nextButtonRiverWaterQuality;
    public GameObject prevButtonRiverWaterQuality;

    GameObject detailCreepyEyes1;
    GameObject detailCreepyEyes2;
    GameObject detailCreepyEyes3;
    GameObject detailCreepyEyes4;
    GameObject detailCreepyEyes5;
    GameObject detailCreepyEyes6;
    bool onCreepyEyes1;
    bool onCreepyEyes2;
    bool onCreepyEyes3;
    bool onCreepyEyes4;
    bool onCreepyEyes5;
    bool onCreepyEyes6;
    int countCreepyEyes = 0;
    public GameObject creepyEyesContents;
    public GameObject creepyEyes_etail;
    public static int creepyEyesIntelInt;
    public GameObject nextButtonCreepyEyes;
    public GameObject prevButtonCreepyEyes;

    GameObject detailSafe1;
    GameObject detailSafe2;
    GameObject detailSafe3;
    GameObject detailSafe4;
    GameObject detailSafe5;
    GameObject detailSafe6;
    bool onSafe1;
    bool onSafe2;
    bool onSafe3;
    bool onSafe4;
    bool onSafe5;
    bool onSafe6;
    int countSafe = 0;
    public GameObject safeContents;
    public GameObject safeDetail;
    public static int safeIntelInt;
    public GameObject nextButtonSafe;
    public GameObject prevButtonSafe;

    GameObject detailMurderCase1;
    GameObject detailMurderCase2;
    GameObject detailMurderCase3;
    GameObject detailMurderCase4;
    GameObject detailMurderCase5;
    GameObject detailMurderCase6;
    bool onMurderCase1;
    bool onMurderCase2;
    bool onMurderCase3;
    bool onMurderCase4;
    bool onMurderCase5;
    bool onMurderCase6;
    int countMurderCase = 0;
    public GameObject murderCaseContents;
    public GameObject murderCaseDetail;
    public static int murderCaseIntelInt;
    public GameObject nextButtonMurderCase;
    public GameObject prevButtonMurderCase;
    void Awake()
    {
        setGameObecject();
    }

    void setGameObecject()
    {
        detailPlanetarySequence1 = planetarySequenceContents.transform.GetChild(0).gameObject;
        detailPlanetarySequence2 = planetarySequenceContents.transform.GetChild(1).gameObject;
        detailPlanetarySequence3 = planetarySequenceContents.transform.GetChild(2).gameObject;
        detailPlanetarySequence4 = planetarySequenceContents.transform.GetChild(3).gameObject;
        detailPlanetarySequence5 = planetarySequenceContents.transform.GetChild(4).gameObject;
        detailPlanetarySequence6 = planetarySequenceContents.transform.GetChild(5).gameObject;

        detailNightmare1 = nightmareContents.transform.GetChild(0).gameObject;
        detailNightmare2 = nightmareContents.transform.GetChild(1).gameObject;
        detailNightmare3 = nightmareContents.transform.GetChild(2).gameObject;
        detailNightmare4 = nightmareContents.transform.GetChild(3).gameObject;
        detailNightmare5 = nightmareContents.transform.GetChild(4).gameObject;
        detailNightmare6 = nightmareContents.transform.GetChild(5).gameObject;

        detailParanoia1 = paranoiaContents.transform.GetChild(0).gameObject;
        detailParanoia2 = paranoiaContents.transform.GetChild(1).gameObject;
        detailParanoia3 = paranoiaContents.transform.GetChild(2).gameObject;
        detailParanoia4 = paranoiaContents.transform.GetChild(3).gameObject;
        detailParanoia5 = paranoiaContents.transform.GetChild(4).gameObject;
        detailParanoia6 = paranoiaContents.transform.GetChild(5).gameObject;

        detailInsomnia1 = insomniaContents.transform.GetChild(0).gameObject;
        detailInsomnia2 = insomniaContents.transform.GetChild(1).gameObject;
        detailInsomnia3 = insomniaContents.transform.GetChild(2).gameObject;
        detailInsomnia4 = insomniaContents.transform.GetChild(3).gameObject;
        detailInsomnia5 = insomniaContents.transform.GetChild(4).gameObject;
        detailInsomnia6 = insomniaContents.transform.GetChild(5).gameObject;

        detailSewerGhostStory1 = sewerGhostStoryContents.transform.GetChild(0).gameObject;
        detailSewerGhostStory2 = sewerGhostStoryContents.transform.GetChild(1).gameObject;
        detailSewerGhostStory3 = sewerGhostStoryContents.transform.GetChild(2).gameObject;
        detailSewerGhostStory4 = sewerGhostStoryContents.transform.GetChild(3).gameObject;
        detailSewerGhostStory5 = sewerGhostStoryContents.transform.GetChild(4).gameObject;
        detailSewerGhostStory6 = sewerGhostStoryContents.transform.GetChild(5).gameObject;

        detailMissingPeople1 = missingPeopleContents.transform.GetChild(0).gameObject;
        detailMissingPeople2 = missingPeopleContents.transform.GetChild(1).gameObject;
        detailMissingPeople3 = missingPeopleContents.transform.GetChild(2).gameObject;
        detailMissingPeople4 = missingPeopleContents.transform.GetChild(3).gameObject;
        detailMissingPeople5 = missingPeopleContents.transform.GetChild(4).gameObject;
        detailMissingPeople6 = missingPeopleContents.transform.GetChild(5).gameObject;

        detailFishySmell1 = fishySmellContents.transform.GetChild(0).gameObject;
        detailFishySmell2 = fishySmellContents.transform.GetChild(1).gameObject;
        detailFishySmell3 = fishySmellContents.transform.GetChild(2).gameObject;
        detailFishySmell4 = fishySmellContents.transform.GetChild(3).gameObject;
        detailFishySmell5 = fishySmellContents.transform.GetChild(4).gameObject;
        detailFishySmell6 = fishySmellContents.transform.GetChild(5).gameObject;

        detailRiverWaterQuality1 = riverWaterQualityContents.transform.GetChild(0).gameObject;
        detailRiverWaterQuality2 = riverWaterQualityContents.transform.GetChild(1).gameObject;
        detailRiverWaterQuality3 = riverWaterQualityContents.transform.GetChild(2).gameObject;
        detailRiverWaterQuality4 = riverWaterQualityContents.transform.GetChild(3).gameObject;
        detailRiverWaterQuality5 = riverWaterQualityContents.transform.GetChild(4).gameObject;
        detailRiverWaterQuality6 = riverWaterQualityContents.transform.GetChild(5).gameObject;

        detailCreepyEyes1 = creepyEyesContents.transform.GetChild(0).gameObject;
        detailCreepyEyes2 = creepyEyesContents.transform.GetChild(1).gameObject;
        detailCreepyEyes3 = creepyEyesContents.transform.GetChild(2).gameObject;
        detailCreepyEyes4 = creepyEyesContents.transform.GetChild(3).gameObject;
        detailCreepyEyes5 = creepyEyesContents.transform.GetChild(4).gameObject;
        detailCreepyEyes6 = creepyEyesContents.transform.GetChild(5).gameObject;


        detailSafe1 = safeContents.transform.GetChild(0).gameObject;
        detailSafe2 = safeContents.transform.GetChild(1).gameObject;
        detailSafe3 = safeContents.transform.GetChild(2).gameObject;
        detailSafe4 = safeContents.transform.GetChild(3).gameObject;
        detailSafe5 = safeContents.transform.GetChild(4).gameObject;
        detailSafe6 = safeContents.transform.GetChild(5).gameObject;

        detailMurderCase1 = murderCaseContents.transform.GetChild(0).gameObject;
        detailMurderCase2 = murderCaseContents.transform.GetChild(1).gameObject;
        detailMurderCase3 = murderCaseContents.transform.GetChild(2).gameObject;
        detailMurderCase4 = murderCaseContents.transform.GetChild(3).gameObject;
        detailMurderCase5 = murderCaseContents.transform.GetChild(4).gameObject;
        detailMurderCase6 = murderCaseContents.transform.GetChild(5).gameObject;
    }

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
        MurderCaseDetail();

        if (nextButtonCreepyEyes.activeSelf == true || nextButtonFishySmell.activeSelf == true || nextButtonInsomnia.activeSelf == true || nextButtonMissingPeople.activeSelf == true || nextButtonNightmare.activeSelf == true || nextButtonParanoia.activeSelf == true || nextButtonPlanetarySequence.activeSelf == true || nextButtonRiverWaterQuality.activeSelf == true || nextButtonSafe.activeSelf == true || nextButtonSewerGhostStory.activeSelf == true || nextButtonMurderCase.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                NextPage();
            }
        }
        if (prevButtonCreepyEyes.activeSelf == true || prevButtonFishySmell.activeSelf == true || prevButtonInsomnia.activeSelf == true || prevButtonMissingPeople.activeSelf == true || prevButtonNightmare.activeSelf == true || prevButtonParanoia.activeSelf == true || prevButtonPlanetarySequence.activeSelf == true || prevButtonRiverWaterQuality.activeSelf == true || prevButtonSafe.activeSelf == true || prevButtonSewerGhostStory.activeSelf == true || prevButtonMurderCase.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                PrevPage();
            }
        }
        if (planetarySequenceDetail.activeSelf == true)
        {
            nowPage = "PlanetarySequence";
        }
        if (nightmareDetail.activeSelf == true)
        {
            nowPage = "Nightmare";
        }
        if (paranoiaDetail.activeSelf == true)
        {
            nowPage = "Paranoia";
        }
        if (insomniaDetail.activeSelf == true)
        {
            nowPage = "Insomnia";
        }
        if (sewerGhostStoryDetail.activeSelf == true)
        {
            nowPage = "SewerGhostStory";
        }
        if (missingPeopleDetail.activeSelf == true)
        {
            nowPage = "MissingPeople";
        }
        if (fishySmellDetail.activeSelf == true)
        {
            nowPage = "FishySmell";
        }
        if (riverWaterQualityDetail.activeSelf == true)
        {
            nowPage = "RiverWaterQuality";
        }
        if (creepyEyes_etail.activeSelf == true)
        {
            nowPage = "CreepyEyes";
        }

        if (safeDetail.activeSelf == true)
        {
            nowPage = "Safe";
        }
        if (murderCaseDetail.activeSelf == true)
        {
            nowPage = "MurderCase";
        }

    }
    void PlanetarySequenceDetail()
    {
        if (onPlanetarySequence1 == false && DataBaseManager.Intel_PlanetaryParade1 == true)
        {
            onPlanetarySequence1 = true;
            detailPlanetarySequence1.SetActive(true);
            detailPlanetarySequence1.transform.SetAsLastSibling();
            countPlanetarySequence += 1;
        }
        if (onPlanetarySequence2 == false && DataBaseManager.Intel_PlanetaryParade2 == true)
        {
            onPlanetarySequence2 = true;
            detailPlanetarySequence2.SetActive(true);
            detailPlanetarySequence2.transform.SetAsLastSibling();
            countPlanetarySequence += 1;
        }
        if (onPlanetarySequence3 == false && DataBaseManager.Intel_PlanetaryParade3 == true)
        {
            onPlanetarySequence3 = true;
            detailPlanetarySequence3.SetActive(true);
            detailPlanetarySequence3.transform.SetAsLastSibling();
            countPlanetarySequence += 1;
        }
        if (onPlanetarySequence4 == false && DataBaseManager.Intel_PlanetaryParade4 == true)
        {
            onPlanetarySequence4 = true;
            detailPlanetarySequence4.SetActive(true);
            detailPlanetarySequence4.transform.SetAsLastSibling();
            countPlanetarySequence += 1;
        }
        if (onPlanetarySequence5 == false && DataBaseManager.Intel_PlanetaryParade5 == true)
        {
            onPlanetarySequence5 = true;
            detailPlanetarySequence5.SetActive(true);
            detailPlanetarySequence5.transform.SetAsLastSibling();
            countPlanetarySequence += 1;
        }
        if (onPlanetarySequence6 == false && DataBaseManager.Intel_PlanetaryParade6 == true)
        {
            onPlanetarySequence6 = true;
            detailPlanetarySequence6.SetActive(true);
            detailPlanetarySequence6.transform.SetAsLastSibling();
            countPlanetarySequence += 1;
        }

        if (countPlanetarySequence > 2 && DataBaseManager.NowPage_PlanetaryParade == 1)
        {
            nextButtonPlanetarySequence.SetActive(true);
        }
        else if (countPlanetarySequence > 4 && DataBaseManager.NowPage_PlanetaryParade == 2)
        {
            nextButtonPlanetarySequence.SetActive(true);
        }
        else
        {
            nextButtonPlanetarySequence.SetActive(false);
        }

        if (DataBaseManager.NowPage_PlanetaryParade == 2)
        {
            prevButtonPlanetarySequence.SetActive(true);
        }
        else if (DataBaseManager.NowPage_PlanetaryParade == 3)
        {
            prevButtonPlanetarySequence.SetActive(true);
        }
        else
        {
            prevButtonPlanetarySequence.SetActive(false);
        }
    }
    void NightmareDetail()
    {
        if (onNightmare1 == false && DataBaseManager.Intel_Nightmare1 == true)
        {
            onNightmare1 = true;
            detailNightmare1.SetActive(true);
            detailNightmare1.transform.SetAsLastSibling();
            countNightmare += 1;
        }
        if (onNightmare2 == false && DataBaseManager.Intel_Nightmare2 == true)
        {
            onNightmare2 = true;
            detailNightmare2.SetActive(true);
            detailNightmare2.transform.SetAsLastSibling();
            countNightmare += 1;
        }
        if (onNightmare3 == false && DataBaseManager.Intel_Nightmare3 == true)
        {
            onNightmare3 = true;
            detailNightmare3.SetActive(true);
            detailNightmare3.transform.SetAsLastSibling();
            countNightmare += 1;
        }
        if (onNightmare4 == false && DataBaseManager.Intel_Nightmare4 == true)
        {
            onNightmare4 = true;
            detailNightmare4.SetActive(true);
            detailNightmare4.transform.SetAsLastSibling();
            countNightmare += 1;
        }
        if (onNightmare5 == false && DataBaseManager.Intel_Nightmare5 == true)
        {
            onNightmare5 = true;
            detailNightmare5.SetActive(true);
            detailNightmare5.transform.SetAsLastSibling();
            countNightmare += 1;
        }
        if (onNightmare6 == false && DataBaseManager.Intel_Nightmare6 == true)
        {
            onNightmare6 = true;
            detailNightmare6.SetActive(true);
            detailNightmare6.transform.SetAsLastSibling();
            countNightmare += 1;
        }

        if (countNightmare > 2 && DataBaseManager.NowPage_Nightmare == 1)
        {
            nextButtonNightmare.SetActive(true);
        }
        else if (countNightmare > 4 && DataBaseManager.NowPage_Nightmare == 2)
        {
            nextButtonNightmare.SetActive(true);
        }
        else
        {
            nextButtonNightmare.SetActive(false);
        }

        if (DataBaseManager.NowPage_Nightmare == 2)
        {
            prevButtonNightmare.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Nightmare == 3)
        {
            prevButtonNightmare.SetActive(true);
        }
        else
        {
            prevButtonNightmare.SetActive(false);
        }
    }
    void ParanoiaDetail()
    {
        if (onParanoia1 == false && DataBaseManager.Intel_Paranoia1 == true)
        {
            onParanoia1 = true;
            detailParanoia1.SetActive(true);
            detailParanoia1.transform.SetAsLastSibling();
            countParanoia += 1;
        }
        if (onParanoia2 == false && DataBaseManager.Intel_Paranoia2 == true)
        {
            onParanoia2 = true;
            detailParanoia2.SetActive(true);
            detailParanoia2.transform.SetAsLastSibling();
            countParanoia += 1;
        }
        if (onParanoia3 == false && DataBaseManager.Intel_Paranoia3 == true)
        {
            onParanoia3 = true;
            detailParanoia3.SetActive(true);
            detailParanoia3.transform.SetAsLastSibling();
            countParanoia += 1;
        }
        if (onParanoia4 == false && DataBaseManager.Intel_Paranoia4 == true)
        {
            onParanoia4 = true;
            detailParanoia4.SetActive(true);
            detailParanoia4.transform.SetAsLastSibling();
            countParanoia += 1;
        }
        if (onParanoia5 == false && DataBaseManager.Intel_Paranoia5 == true)
        {
            onParanoia5 = true;
            detailParanoia5.SetActive(true);
            detailParanoia5.transform.SetAsLastSibling();
            countParanoia += 1;
        }
        if (onParanoia6 == false && DataBaseManager.Intel_Paranoia6 == true)
        {
            onParanoia6 = true;
            detailParanoia6.SetActive(true);
            detailParanoia6.transform.SetAsLastSibling();
            countParanoia += 1;
        }

        if (countParanoia > 2 && DataBaseManager.NowPage_Paranoia == 1)
        {
            nextButtonParanoia.SetActive(true);
        }
        else if (countParanoia > 4 && DataBaseManager.NowPage_Paranoia == 2)
        {
            nextButtonParanoia.SetActive(true);
        }
        else
        {
            nextButtonParanoia.SetActive(false);
        }
        if (DataBaseManager.NowPage_Paranoia == 2)
        {
            prevButtonParanoia.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Paranoia == 3)
        {
            prevButtonParanoia.SetActive(true);
        }
        else
        {
            prevButtonParanoia.SetActive(false);
        }
    }
    void InsomniaDetail()
    {
        if (onInsomnia1 == false && DataBaseManager.Intel_Insomnia1 == true)
        {
            onInsomnia1 = true;
            detailInsomnia1.SetActive(true);
            detailInsomnia1.transform.SetAsLastSibling();
            countInsomnia += 1;
        }
        if (onInsomnia2 == false && DataBaseManager.Intel_Insomnia2 == true)
        {
            onInsomnia2 = true;
            detailInsomnia2.SetActive(true);
            detailInsomnia2.transform.SetAsLastSibling();
            countInsomnia += 1;
        }
        if (onInsomnia3 == false && DataBaseManager.Intel_Insomnia3 == true)
        {
            onInsomnia3 = true;
            detailInsomnia3.SetActive(true);
            detailInsomnia3.transform.SetAsLastSibling();
            countInsomnia += 1;
        }
        if (onInsomnia4 == false && DataBaseManager.Intel_Insomnia4 == true)
        {
            onInsomnia4 = true;
            detailInsomnia4.SetActive(true);
            detailInsomnia4.transform.SetAsLastSibling();
            countInsomnia += 1;
        }
        if (onInsomnia5 == false && DataBaseManager.Intel_Insomnia5 == true)
        {
            onInsomnia5 = true;
            detailInsomnia5.SetActive(true);
            detailInsomnia5.transform.SetAsLastSibling();
            countInsomnia += 1;
        }
        if (onInsomnia6 == false && DataBaseManager.Intel_Insomnia6 == true)
        {
            onInsomnia6 = true;
            detailInsomnia6.SetActive(true);
            detailInsomnia6.transform.SetAsLastSibling();
            countInsomnia += 1;
        }

        if (countInsomnia > 2 && DataBaseManager.NowPage_Insomnia == 1)
        {
            nextButtonInsomnia.SetActive(true);
        }
        else if (countInsomnia > 4 && DataBaseManager.NowPage_Insomnia == 2)
        {
            nextButtonInsomnia.SetActive(true);
        }
        else
        {
            nextButtonInsomnia.SetActive(false);
        }

        if (DataBaseManager.NowPage_Insomnia == 2)
        {
            prevButtonInsomnia.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Insomnia == 3)
        {
            prevButtonInsomnia.SetActive(true);
        }
        else
        {
            prevButtonInsomnia.SetActive(false);
        }
    }
    void SewerGhostStoryDetail()
    {
        if (onSewerGhostStory1 == false && DataBaseManager.Intel_SewerGhostStory1 == true)
        {
            onSewerGhostStory1 = true;
            detailSewerGhostStory1.SetActive(true);
            detailSewerGhostStory1.transform.SetAsLastSibling();
            countSewerGhostStory += 1;
        }
        if (onSewerGhostStory2 == false && DataBaseManager.Intel_SewerGhostStory2 == true)
        {
            onSewerGhostStory2 = true;
            detailSewerGhostStory2.SetActive(true);
            detailSewerGhostStory2.transform.SetAsLastSibling();
            countSewerGhostStory += 1;
        }
        if (onSewerGhostStory3 == false && DataBaseManager.Intel_SewerGhostStory3 == true)
        {
            onSewerGhostStory3 = true;
            detailSewerGhostStory3.SetActive(true);
            detailSewerGhostStory3.transform.SetAsLastSibling();
            countSewerGhostStory += 1;
        }
        if (onSewerGhostStory4 == false && DataBaseManager.Intel_SewerGhostStory4 == true)
        {
            onSewerGhostStory4 = true;
            detailSewerGhostStory4.SetActive(true);
            detailSewerGhostStory4.transform.SetAsLastSibling();
            countSewerGhostStory += 1;
        }
        if (onSewerGhostStory5 == false && DataBaseManager.Intel_SewerGhostStory5 == true)
        {
            onSewerGhostStory5 = true;
            detailSewerGhostStory5.SetActive(true);
            detailSewerGhostStory5.transform.SetAsLastSibling();
            countSewerGhostStory += 1;
        }
        if (onSewerGhostStory6 == false && DataBaseManager.Intel_SewerGhostStory6 == true)
        {
            onSewerGhostStory6 = true;
            detailSewerGhostStory6.SetActive(true);
            detailSewerGhostStory6.transform.SetAsLastSibling();
            countSewerGhostStory += 1;
        }

        if (countSewerGhostStory > 2 && DataBaseManager.NowPage_SewerGhostStory == 1)
        {
            nextButtonSewerGhostStory.SetActive(true);
        }
        else if (countSewerGhostStory > 4 && DataBaseManager.NowPage_SewerGhostStory == 2)
        {
            nextButtonSewerGhostStory.SetActive(true);
        }
        else
        {
            nextButtonSewerGhostStory.SetActive(false);
        }

        if (DataBaseManager.NowPage_SewerGhostStory == 2)
        {
            prevButtonSewerGhostStory.SetActive(true);
        }
        else if (DataBaseManager.NowPage_SewerGhostStory == 3)
        {
            prevButtonSewerGhostStory.SetActive(true);
        }
        else
        {
            prevButtonSewerGhostStory.SetActive(false);
        }
    }
    void MissingPeopleDetail()
    {
        if (onMissingPeople1 == false && DataBaseManager.Intel_MissingPeople1 == true)
        {
            onMissingPeople1 = true;
            detailMissingPeople1.SetActive(true);
            detailMissingPeople1.transform.SetAsLastSibling();
            countMissingPeople += 1;
        }
        if (onMissingPeople2 == false && DataBaseManager.Intel_MissingPeople2 == true)
        {
            onMissingPeople2 = true;
            detailMissingPeople2.SetActive(true);
            detailMissingPeople2.transform.SetAsLastSibling();
            countMissingPeople += 1;
        }
        if (onMissingPeople3 == false && DataBaseManager.Intel_MissingPeople3 == true)
        {
            onMissingPeople3 = true;
            detailMissingPeople3.SetActive(true);
            detailMissingPeople3.transform.SetAsLastSibling();
            countMissingPeople += 1;
        }
        if (onMissingPeople4 == false && DataBaseManager.Intel_MissingPeople4 == true)
        {
            onMissingPeople4 = true;
            detailMissingPeople4.SetActive(true);
            detailMissingPeople4.transform.SetAsLastSibling();
            countMissingPeople += 1;
        }
        if (onMissingPeople5 == false && DataBaseManager.Intel_MissingPeople5 == true)
        {
            onMissingPeople5 = true;
            detailMissingPeople5.SetActive(true);
            detailMissingPeople5.transform.SetAsLastSibling();
            countMissingPeople += 1;
        }
        if (onMissingPeople6 == false && DataBaseManager.Intel_MissingPeople6 == true)
        {
            onMissingPeople6 = true;
            detailMissingPeople6.SetActive(true);
            detailMissingPeople6.transform.SetAsLastSibling();
            countMissingPeople += 1;
        }

        if (countMissingPeople > 2 && DataBaseManager.NowPage_MissingPeople == 1)
        {
            nextButtonMissingPeople.SetActive(true);
        }
        else if (countMissingPeople > 4 && DataBaseManager.NowPage_MissingPeople == 2)
        {
            nextButtonMissingPeople.SetActive(true);
        }
        else
        {
            nextButtonMissingPeople.SetActive(false);
        }

        if (DataBaseManager.NowPage_MissingPeople == 2)
        {
            prevButtonMissingPeople.SetActive(true);
        }
        else if (DataBaseManager.NowPage_MissingPeople
            == 3)
        {
            prevButtonMissingPeople.SetActive(true);
        }
        else
        {
            prevButtonMissingPeople.SetActive(false);
        }
    }
    void FishySmellDetail()
    {
        if (onFishySmell1 == false && DataBaseManager.Intel_FishySmell1 == true)
        {
            onFishySmell1 = true;
            detailFishySmell1.SetActive(true);
            detailFishySmell1.transform.SetAsLastSibling();
            countFishySmell += 1;
        }
        if (onFishySmell2 == false && DataBaseManager.Intel_FishySmell2 == true)
        {
            onFishySmell2 = true;
            detailFishySmell2.SetActive(true);
            detailFishySmell2.transform.SetAsLastSibling();
            countFishySmell += 1;
        }
        if (onFishySmell3 == false && DataBaseManager.Intel_FishySmell3 == true)
        {
            onFishySmell3 = true;
            detailFishySmell3.SetActive(true);
            detailFishySmell3.transform.SetAsLastSibling();
            countFishySmell += 1;
        }
        if (onFishySmell4 == false && DataBaseManager.Intel_FishySmell4 == true)
        {
            onFishySmell4 = true;
            detailFishySmell4.SetActive(true);
            detailFishySmell4.transform.SetAsLastSibling();
            countFishySmell += 1;
        }
        if (onFishySmell5 == false && DataBaseManager.Intel_FishySmell5 == true)
        {
            onFishySmell5 = true;
            detailFishySmell5.SetActive(true);
            detailFishySmell5.transform.SetAsLastSibling();
            countFishySmell += 1;
        }
        if (onFishySmell6 == false && DataBaseManager.Intel_FishySmell6 == true)
        {
            onFishySmell6 = true;
            detailFishySmell6.SetActive(true);
            detailFishySmell6.transform.SetAsLastSibling();
            countFishySmell += 1;
        }

        if (countFishySmell > 2 && DataBaseManager.NowPage_FishySmell == 1)
        {
            nextButtonFishySmell.SetActive(true);
        }
        else if (countFishySmell > 4 && DataBaseManager.NowPage_FishySmell == 2)
        {
            nextButtonFishySmell.SetActive(true);
        }
        else
        {
            nextButtonFishySmell.SetActive(false);
        }

        if (DataBaseManager.NowPage_FishySmell == 2)
        {
            prevButtonFishySmell.SetActive(true);
        }
        else if (DataBaseManager.NowPage_FishySmell
            == 3)
        {
            prevButtonFishySmell.SetActive(true);
        }
        else
        {
            prevButtonFishySmell.SetActive(false);
        }
    }
    void RiverWaterQualityDetail()
    {
        if (onRiverWaterQuality1 == false && DataBaseManager.Intel_RiverWaterQuality1 == true)
        {
            onRiverWaterQuality1 = true;
            detailRiverWaterQuality1.SetActive(true);
            detailRiverWaterQuality1.transform.SetAsLastSibling();
            countRiverWaterQuality += 1;
        }
        if (onRiverWaterQuality2 == false && DataBaseManager.Intel_RiverWaterQuality2 == true)
        {
            onRiverWaterQuality2 = true;
            detailRiverWaterQuality2.SetActive(true);
            detailRiverWaterQuality2.transform.SetAsLastSibling();
            countRiverWaterQuality += 1;
        }
        if (onRiverWaterQuality3 == false && DataBaseManager.Intel_RiverWaterQuality3 == true)
        {
            onRiverWaterQuality3 = true;
            detailRiverWaterQuality3.SetActive(true);
            detailRiverWaterQuality3.transform.SetAsLastSibling();
            countRiverWaterQuality += 1;
        }
        if (onRiverWaterQuality4 == false && DataBaseManager.Intel_RiverWaterQuality4 == true)
        {
            onRiverWaterQuality4 = true;
            detailRiverWaterQuality4.SetActive(true);
            detailRiverWaterQuality4.transform.SetAsLastSibling();
            countRiverWaterQuality += 1;
        }
        if (onRiverWaterQuality5 == false && DataBaseManager.Intel_RiverWaterQuality5 == true)
        {
            onRiverWaterQuality5 = true;
            detailRiverWaterQuality5.SetActive(true);
            detailRiverWaterQuality5.transform.SetAsLastSibling();
            countRiverWaterQuality += 1;
        }
        if (onRiverWaterQuality6 == false && DataBaseManager.Intel_RiverWaterQuality6 == true)
        {
            onRiverWaterQuality6 = true;
            detailRiverWaterQuality6.SetActive(true);
            detailRiverWaterQuality6.transform.SetAsLastSibling();
            countRiverWaterQuality += 1;
        }

        if (countRiverWaterQuality > 2 && DataBaseManager.NowPage_RiverWaterQuality == 1)
        {
            nextButtonRiverWaterQuality.SetActive(true);
        }
        else if (countRiverWaterQuality > 4 && DataBaseManager.NowPage_RiverWaterQuality == 2)
        {
            nextButtonRiverWaterQuality.SetActive(true);
        }
        else
        {
            nextButtonRiverWaterQuality.SetActive(false);
        }

        if (DataBaseManager.NowPage_RiverWaterQuality == 2)
        {
            prevButtonRiverWaterQuality.SetActive(true);
        }
        else if (DataBaseManager.NowPage_RiverWaterQuality
            == 3)
        {
            prevButtonRiverWaterQuality.SetActive(true);
        }
        else
        {
            prevButtonRiverWaterQuality.SetActive(false);
        }
    }
    void CreepyEyesDetail()
    {
        if (onCreepyEyes1 == false && DataBaseManager.Intel_CreepyEyes1 == true)
        {
            onCreepyEyes1 = true;
            detailCreepyEyes1.SetActive(true);
            detailCreepyEyes1.transform.SetAsLastSibling();
            countCreepyEyes += 1;
        }
        if (onCreepyEyes2 == false && DataBaseManager.Intel_CreepyEyes2 == true)
        {
            onCreepyEyes2 = true;
            detailCreepyEyes2.SetActive(true);
            detailCreepyEyes2.transform.SetAsLastSibling();
            countCreepyEyes += 1;
        }
        if (onCreepyEyes3 == false && DataBaseManager.Intel_CreepyEyes3 == true)
        {
            onCreepyEyes3 = true;
            detailCreepyEyes3.SetActive(true);
            detailCreepyEyes3.transform.SetAsLastSibling();
            countCreepyEyes += 1;
        }
        if (onCreepyEyes4 == false && DataBaseManager.Intel_CreepyEyes4 == true)
        {
            onCreepyEyes4 = true;
            detailCreepyEyes4.SetActive(true);
            detailCreepyEyes4.transform.SetAsLastSibling();
            countCreepyEyes += 1;
        }
        if (onCreepyEyes5 == false && DataBaseManager.Intel_CreepyEyes5 == true)
        {
            onCreepyEyes5 = true;
            detailCreepyEyes5.SetActive(true);
            detailCreepyEyes5.transform.SetAsLastSibling();
            countCreepyEyes += 1;
        }
        if (onCreepyEyes6 == false && DataBaseManager.Intel_CreepyEyes6 == true)
        {
            onCreepyEyes6 = true;
            detailCreepyEyes6.SetActive(true);
            detailCreepyEyes6.transform.SetAsLastSibling();
            countCreepyEyes += 1;
        }

        if (countCreepyEyes > 2 && DataBaseManager.NowPage_CreepyEyes == 1)
        {
            nextButtonCreepyEyes.SetActive(true);
        }
        else if (countCreepyEyes > 4 && DataBaseManager.NowPage_CreepyEyes == 2)
        {
            nextButtonCreepyEyes.SetActive(true);
        }
        else
        {
            nextButtonCreepyEyes.SetActive(false);
        }

        if (DataBaseManager.NowPage_CreepyEyes == 2)
        {
            prevButtonCreepyEyes.SetActive(true);
        }
        else if (DataBaseManager.NowPage_CreepyEyes == 3)
        {
            prevButtonCreepyEyes.SetActive(true);
        }
        else
        {
            prevButtonCreepyEyes.SetActive(false);
        }
    }

    void SafeDetail()
    {
        if (onSafe1 == false && DataBaseManager.Intel_Safe1 == true)
        {
            onSafe1 = true;
            detailSafe1.SetActive(true);
            detailSafe1.transform.SetAsLastSibling();
            countSafe += 1;
        }
        if (onSafe2 == false && DataBaseManager.Intel_Safe2 == true)
        {
            onSafe2 = true;
            detailSafe2.SetActive(true);
            detailSafe2.transform.SetAsLastSibling();
            countSafe += 1;
        }
        if (onSafe3 == false && DataBaseManager.Intel_Safe3 == true)
        {
            onSafe3 = true;
            detailSafe3.SetActive(true);
            detailSafe3.transform.SetAsLastSibling();
            countSafe += 1;
        }
        if (onSafe4 == false && DataBaseManager.Intel_Safe4 == true)
        {
            onSafe4 = true;
            detailSafe4.SetActive(true);
            detailSafe4.transform.SetAsLastSibling();
            countSafe += 1;
        }
        if (onSafe5 == false && DataBaseManager.Intel_Safe5 == true)
        {
            onSafe5 = true;
            detailSafe5.SetActive(true);
            detailSafe5.transform.SetAsLastSibling();
            countSafe += 1;
        }
        if (onSafe6 == false && DataBaseManager.Intel_Safe6 == true)
        {
            onSafe6 = true;
            detailSafe6.SetActive(true);
            detailSafe6.transform.SetAsLastSibling();
            countSafe += 1;
        }

        if (countSafe > 2 && DataBaseManager.NowPage_Safe == 1)
        {
            nextButtonSafe.SetActive(true);
        }
        else if (countSafe > 4 && DataBaseManager.NowPage_Safe == 2)
        {
            nextButtonSafe.SetActive(true);
        }
        else
        {
            nextButtonSafe.SetActive(false);
        }

        if (DataBaseManager.NowPage_Safe == 2)
        {
            prevButtonSafe.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Safe == 3)
        {
            prevButtonSafe.SetActive(true);
        }
        else
        {
            prevButtonSafe.SetActive(false);
        }
    }
    void MurderCaseDetail()
    {
        if (onMurderCase1 == false && DataBaseManager.Intel_MurderCase1 == true)
        {
            onMurderCase1 = true;
            detailMurderCase1.SetActive(true);
            detailMurderCase1.transform.SetAsLastSibling();
            countMurderCase += 1;
        }
        if (onMurderCase2 == false && DataBaseManager.Intel_MurderCase2 == true)
        {
            onMurderCase2 = true;
            detailMurderCase2.SetActive(true);
            detailMurderCase2.transform.SetAsLastSibling();
            countMurderCase += 1;
        }
        if (onMurderCase3 == false && DataBaseManager.Intel_MurderCase3 == true)
        {
            onMurderCase3 = true;
            detailMurderCase3.SetActive(true);
            detailMurderCase3.transform.SetAsLastSibling();
            countMurderCase += 1;
        }
        if (onMurderCase4 == false && DataBaseManager.Intel_MurderCase4 == true)
        {
            onMurderCase4 = true;
            detailMurderCase4.SetActive(true);
            detailMurderCase4.transform.SetAsLastSibling();
            countMurderCase += 1;
        }
        if (onMurderCase5 == false && DataBaseManager.Intel_MurderCase5 == true)
        {
            onMurderCase5 = true;
            detailMurderCase5.SetActive(true);
            detailMurderCase5.transform.SetAsLastSibling();
            countMurderCase += 1;
        }
        if (onMurderCase6 == false && DataBaseManager.Intel_MurderCase6 == true)
        {
            onMurderCase6 = true;
            detailMurderCase6.SetActive(true);
            detailMurderCase6.transform.SetAsLastSibling();
            countMurderCase += 1;
        }

        if (countMurderCase > 2 && DataBaseManager.NowPage_MurderCase == 1)
        {
            nextButtonMurderCase.SetActive(true);
        }
        else if (countMurderCase > 4 && DataBaseManager.NowPage_MurderCase == 2)
        {
            nextButtonMurderCase.SetActive(true);
        }
        else
        {
            nextButtonMurderCase.SetActive(false);
        }

        if (DataBaseManager.NowPage_MurderCase == 2)
        {
            prevButtonMurderCase.SetActive(true);
        }
        else if (DataBaseManager.NowPage_MurderCase == 3)
        {
            prevButtonMurderCase.SetActive(true);
        }
        else
        {
            prevButtonMurderCase.SetActive(false);
        }
    }
    public void NextPage()
    {
        if (nowPage == "PlanetarySequence")
        {
            if (DataBaseManager.NowPage_PlanetaryParade == 1)
            {
                if (countPlanetarySequence >= 6)
                {
                    Reset_PlanetarySequence();
                    planetarySequenceContents.transform.GetChild(2).gameObject.SetActive(true);
                    planetarySequenceContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countPlanetarySequence >= 5)
                {
                    Reset_PlanetarySequence();
                    planetarySequenceContents.transform.GetChild(3).gameObject.SetActive(true);
                    planetarySequenceContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countPlanetarySequence >= 4)
                {
                    Reset_PlanetarySequence();
                    planetarySequenceContents.transform.GetChild(4).gameObject.SetActive(true);
                    planetarySequenceContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countPlanetarySequence >= 3)
                {
                    Reset_PlanetarySequence();
                    planetarySequenceContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_PlanetaryParade = 2;
            }
            else if (DataBaseManager.NowPage_PlanetaryParade == 2)
            {
                if (countPlanetarySequence >= 6)
                {
                    Reset_PlanetarySequence();
                    planetarySequenceContents.transform.GetChild(4).gameObject.SetActive(true);
                    planetarySequenceContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countPlanetarySequence >= 5)
                {
                    Reset_PlanetarySequence();
                    planetarySequenceContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_PlanetaryParade = 3;
            }
        }
        if (nowPage == "Nightmare")
        {
            if (DataBaseManager.NowPage_Nightmare == 1)
            {
                if (countNightmare >= 6)
                {
                    Reset_Nightmare();
                    nightmareContents.transform.GetChild(2).gameObject.SetActive(true);
                    nightmareContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countNightmare >= 5)
                {
                    Reset_Nightmare();
                    nightmareContents.transform.GetChild(3).gameObject.SetActive(true);
                    nightmareContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countNightmare >= 4)
                {
                    Reset_Nightmare();
                    nightmareContents.transform.GetChild(4).gameObject.SetActive(true);
                    nightmareContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countNightmare >= 3)
                {
                    Reset_Nightmare();
                    nightmareContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Nightmare = 2;
            }
            else if (DataBaseManager.NowPage_Nightmare == 2)
            {
                if (countNightmare >= 6)
                {
                    Reset_Nightmare();
                    nightmareContents.transform.GetChild(4).gameObject.SetActive(true);
                    nightmareContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countNightmare >= 5)
                {
                    Reset_Nightmare();
                    nightmareContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Nightmare = 3;
            }
        }
        if (nowPage == "Paranoia")
        {
            if (DataBaseManager.NowPage_Paranoia == 1)
            {
                if (countParanoia >= 6)
                {
                    Reset_Paranoia();
                    paranoiaContents.transform.GetChild(2).gameObject.SetActive(true);
                    paranoiaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countParanoia >= 5)
                {
                    Reset_Paranoia();
                    paranoiaContents.transform.GetChild(3).gameObject.SetActive(true);
                    paranoiaContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countParanoia >= 4)
                {
                    Reset_Paranoia();
                    paranoiaContents.transform.GetChild(4).gameObject.SetActive(true);
                    paranoiaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countParanoia >= 3)
                {
                    Reset_Paranoia();
                    paranoiaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Paranoia = 2;
            }
            else if (DataBaseManager.NowPage_Paranoia == 2)
            {
                if (countParanoia >= 6)
                {
                    Reset_Paranoia();
                    paranoiaContents.transform.GetChild(4).gameObject.SetActive(true);
                    paranoiaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countParanoia >= 5)
                {
                    Reset_Paranoia();
                    paranoiaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Paranoia = 3;
            }
        }
        if (nowPage == "Insomnia")
        {
            if (DataBaseManager.NowPage_Insomnia == 1)
            {
                if (countInsomnia >= 6)
                {
                    Reset_Insomnia();
                    insomniaContents.transform.GetChild(2).gameObject.SetActive(true);
                    insomniaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countInsomnia >= 5)
                {
                    Reset_Insomnia();
                    insomniaContents.transform.GetChild(3).gameObject.SetActive(true);
                    insomniaContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countInsomnia >= 4)
                {
                    Reset_Insomnia();
                    insomniaContents.transform.GetChild(4).gameObject.SetActive(true);
                    insomniaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countInsomnia >= 3)
                {
                    Reset_Insomnia();
                    insomniaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Insomnia = 2;
            }
            else if (DataBaseManager.NowPage_Insomnia == 2)
            {
                if (countInsomnia >= 6)
                {
                    Reset_Insomnia();
                    insomniaContents.transform.GetChild(4).gameObject.SetActive(true);
                    insomniaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countInsomnia >= 5)
                {
                    Reset_Insomnia();
                    insomniaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Insomnia = 3;
            }
        }
        if (nowPage == "SewerGhostStory")
        {
            if (DataBaseManager.NowPage_SewerGhostStory == 1)
            {
                if (countSewerGhostStory >= 6)
                {
                    Reset_SewerGhostStory();
                    sewerGhostStoryContents.transform.GetChild(2).gameObject.SetActive(true);
                    sewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSewerGhostStory >= 5)
                {
                    Reset_SewerGhostStory();
                    sewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(true);
                    sewerGhostStoryContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countSewerGhostStory >= 4)
                {
                    Reset_SewerGhostStory();
                    sewerGhostStoryContents.transform.GetChild(4).gameObject.SetActive(true);
                    sewerGhostStoryContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSewerGhostStory >= 3)
                {
                    Reset_SewerGhostStory();
                    sewerGhostStoryContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewerGhostStory = 2;
            }
            else if (DataBaseManager.NowPage_SewerGhostStory == 2)
            {
                if (countSewerGhostStory >= 6)
                {
                    Reset_SewerGhostStory();
                    sewerGhostStoryContents.transform.GetChild(4).gameObject.SetActive(true);
                    sewerGhostStoryContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSewerGhostStory >= 5)
                {
                    Reset_SewerGhostStory();
                    sewerGhostStoryContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewerGhostStory = 3;
            }
        }
        if (nowPage == "MissingPeople")
        {
            if (DataBaseManager.NowPage_MissingPeople == 1)
            {
                if (countMissingPeople >= 6)
                {
                    Reset_MissingPeople();
                    missingPeopleContents.transform.GetChild(2).gameObject.SetActive(true);
                    missingPeopleContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countMissingPeople >= 5)
                {
                    Reset_MissingPeople();
                    missingPeopleContents.transform.GetChild(3).gameObject.SetActive(true);
                    missingPeopleContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countMissingPeople >= 4)
                {
                    Reset_MissingPeople();
                    missingPeopleContents.transform.GetChild(4).gameObject.SetActive(true);
                    missingPeopleContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countMissingPeople >= 3)
                {
                    Reset_MissingPeople();
                    missingPeopleContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_MissingPeople = 2;
            }
            else if (DataBaseManager.NowPage_MissingPeople == 2)
            {
                if (countMissingPeople >= 6)
                {
                    Reset_MissingPeople();
                    missingPeopleContents.transform.GetChild(4).gameObject.SetActive(true);
                    missingPeopleContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countMissingPeople >= 5)
                {
                    Reset_MissingPeople();
                    missingPeopleContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_MissingPeople = 3;
            }
        }
        if (nowPage == "FishySmell")
        {
            if (DataBaseManager.NowPage_FishySmell == 1)
            {
                if (countFishySmell >= 6)
                {
                    Reset_FishySmell();
                    fishySmellContents.transform.GetChild(2).gameObject.SetActive(true);
                    fishySmellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countFishySmell >= 5)
                {
                    Reset_FishySmell();
                    fishySmellContents.transform.GetChild(3).gameObject.SetActive(true);
                    fishySmellContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countFishySmell >= 4)
                {
                    Reset_FishySmell();
                    fishySmellContents.transform.GetChild(4).gameObject.SetActive(true);
                    fishySmellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countFishySmell >= 3)
                {
                    Reset_FishySmell();
                    fishySmellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_FishySmell = 2;
            }
            else if (DataBaseManager.NowPage_FishySmell == 2)
            {
                if (countFishySmell >= 6)
                {
                    Reset_FishySmell();
                    fishySmellContents.transform.GetChild(4).gameObject.SetActive(true);
                    fishySmellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countFishySmell >= 5)
                {
                    Reset_FishySmell();
                    fishySmellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_FishySmell = 3;
            }
        }
        if (nowPage == "RiverWaterQuality")
        {
            if (DataBaseManager.NowPage_RiverWaterQuality == 1)
            {
                if (countRiverWaterQuality >= 6)
                {
                    Reset_RiverWaterQuality();
                    riverWaterQualityContents.transform.GetChild(2).gameObject.SetActive(true);
                    riverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countRiverWaterQuality >= 5)
                {
                    Reset_RiverWaterQuality();
                    riverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(true);
                    riverWaterQualityContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countRiverWaterQuality >= 4)
                {
                    Reset_RiverWaterQuality();
                    riverWaterQualityContents.transform.GetChild(4).gameObject.SetActive(true);
                    riverWaterQualityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countRiverWaterQuality >= 3)
                {
                    Reset_RiverWaterQuality();
                    riverWaterQualityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_RiverWaterQuality = 2;
            }
            else if (DataBaseManager.NowPage_RiverWaterQuality == 2)
            {
                if (countRiverWaterQuality >= 6)
                {
                    Reset_RiverWaterQuality();
                    riverWaterQualityContents.transform.GetChild(4).gameObject.SetActive(true);
                    riverWaterQualityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countRiverWaterQuality >= 5)
                {
                    Reset_RiverWaterQuality();
                    riverWaterQualityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_RiverWaterQuality = 3;
            }
        }
        if (nowPage == "CreepyEyes")
        {
            if (DataBaseManager.NowPage_CreepyEyes == 1)
            {
                if (countCreepyEyes >= 6)
                {
                    Reset_CreepyEyes();
                    creepyEyesContents.transform.GetChild(2).gameObject.SetActive(true);
                    creepyEyesContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countCreepyEyes >= 5)
                {
                    Reset_CreepyEyes();
                    creepyEyesContents.transform.GetChild(3).gameObject.SetActive(true);
                    creepyEyesContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countCreepyEyes >= 4)
                {
                    Reset_CreepyEyes();
                    creepyEyesContents.transform.GetChild(4).gameObject.SetActive(true);
                    creepyEyesContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countCreepyEyes >= 3)
                {
                    Reset_CreepyEyes();
                    creepyEyesContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_CreepyEyes = 2;
            }
            else if (DataBaseManager.NowPage_CreepyEyes == 2)
            {
                if (countCreepyEyes >= 6)
                {
                    Reset_CreepyEyes();
                    creepyEyesContents.transform.GetChild(4).gameObject.SetActive(true);
                    creepyEyesContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countCreepyEyes >= 5)
                {
                    Reset_CreepyEyes();
                    creepyEyesContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_CreepyEyes = 3;
            }
        }

        if (nowPage == "Safe")
        {
            if (DataBaseManager.NowPage_Safe == 1)
            {
                if (countSafe >= 6)
                {
                    Reset_Safe();
                    safeContents.transform.GetChild(2).gameObject.SetActive(true);
                    safeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSafe >= 5)
                {
                    Reset_Safe();
                    safeContents.transform.GetChild(3).gameObject.SetActive(true);
                    safeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countSafe >= 4)
                {
                    Reset_Safe();
                    safeContents.transform.GetChild(4).gameObject.SetActive(true);
                    safeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSafe >= 3)
                {
                    Reset_Safe();
                    safeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Safe = 2;
            }
            else if (DataBaseManager.NowPage_Safe == 2)
            {
                if (countSafe >= 6)
                {
                    Reset_Safe();
                    safeContents.transform.GetChild(4).gameObject.SetActive(true);
                    safeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSafe >= 5)
                {
                    Reset_Safe();
                    safeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Safe = 3;
            }
        }
        if (nowPage == "MurderCase")
        {
            if (DataBaseManager.NowPage_MurderCase == 1)
            {
                if (countMurderCase >= 6)
                {
                    Reset_MurderCase();
                    murderCaseContents.transform.GetChild(2).gameObject.SetActive(true);
                    murderCaseContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countMurderCase >= 5)
                {
                    Reset_MurderCase();
                    murderCaseContents.transform.GetChild(3).gameObject.SetActive(true);
                    murderCaseContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countMurderCase >= 4)
                {
                    Reset_MurderCase();
                    murderCaseContents.transform.GetChild(4).gameObject.SetActive(true);
                    murderCaseContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countMurderCase >= 3)
                {
                    Reset_MurderCase();
                    murderCaseContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_MurderCase = 2;
            }
            else if (DataBaseManager.NowPage_MurderCase == 2)
            {
                if (countMurderCase >= 6)
                {
                    Reset_MurderCase();
                    murderCaseContents.transform.GetChild(4).gameObject.SetActive(true);
                    murderCaseContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countMurderCase >= 5)
                {
                    Reset_MurderCase();
                    murderCaseContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_MurderCase = 3;
            }
        }
    }
    public void PrevPage()
    {
        if (nowPage == "PlanetarySequence")
        {
            if (DataBaseManager.NowPage_PlanetaryParade == 2)
            {
                if (countPlanetarySequence >= 6)
                {
                    Reset_PlanetarySequence();
                    planetarySequenceContents.transform.GetChild(0).gameObject.SetActive(true);
                    planetarySequenceContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countPlanetarySequence >= 5)
                {
                    Reset_PlanetarySequence();
                    planetarySequenceContents.transform.GetChild(1).gameObject.SetActive(true);
                    planetarySequenceContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countPlanetarySequence >= 4)
                {
                    Reset_PlanetarySequence();
                    planetarySequenceContents.transform.GetChild(2).gameObject.SetActive(true);
                    planetarySequenceContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countPlanetarySequence >= 3)
                {
                    Reset_PlanetarySequence();
                    planetarySequenceContents.transform.GetChild(3).gameObject.SetActive(true);
                    planetarySequenceContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_PlanetaryParade = 1;
            }
            else if (DataBaseManager.NowPage_PlanetaryParade == 3)
            {
                if (countPlanetarySequence >= 6)
                {
                    Reset_PlanetarySequence();
                    planetarySequenceContents.transform.GetChild(2).gameObject.SetActive(true);
                    planetarySequenceContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countPlanetarySequence >= 5)
                {
                    Reset_PlanetarySequence();
                    planetarySequenceContents.transform.GetChild(3).gameObject.SetActive(true);
                    planetarySequenceContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_PlanetaryParade = 2;
            }
        }
        if (nowPage == "Nightmare")
        {
            if (DataBaseManager.NowPage_Nightmare == 2)
            {
                if (countNightmare >= 6)
                {
                    Reset_Nightmare();
                    nightmareContents.transform.GetChild(0).gameObject.SetActive(true);
                    nightmareContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countNightmare >= 5)
                {
                    Reset_Nightmare();
                    nightmareContents.transform.GetChild(1).gameObject.SetActive(true);
                    nightmareContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countNightmare >= 4)
                {
                    Reset_Nightmare();
                    nightmareContents.transform.GetChild(2).gameObject.SetActive(true);
                    nightmareContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countNightmare >= 3)
                {
                    Reset_Nightmare();
                    nightmareContents.transform.GetChild(3).gameObject.SetActive(true);
                    nightmareContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Nightmare = 1;
            }
            else if (DataBaseManager.NowPage_Nightmare == 3)
            {
                if (countNightmare >= 6)
                {
                    Reset_Nightmare();
                    nightmareContents.transform.GetChild(2).gameObject.SetActive(true);
                    nightmareContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countNightmare >= 5)
                {
                    Reset_Nightmare();
                    nightmareContents.transform.GetChild(3).gameObject.SetActive(true);
                    nightmareContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Nightmare = 2;
            }
        }
        if (nowPage == "Paranoia")
        {
            if (DataBaseManager.NowPage_Paranoia == 2)
            {
                if (countParanoia >= 6)
                {
                    Reset_Paranoia();
                    paranoiaContents.transform.GetChild(0).gameObject.SetActive(true);
                    paranoiaContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countParanoia >= 5)
                {
                    Reset_Paranoia();
                    paranoiaContents.transform.GetChild(1).gameObject.SetActive(true);
                    paranoiaContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countParanoia >= 4)
                {
                    Reset_Paranoia();
                    paranoiaContents.transform.GetChild(2).gameObject.SetActive(true);
                    paranoiaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countParanoia >= 3)
                {
                    Reset_Paranoia();
                    paranoiaContents.transform.GetChild(3).gameObject.SetActive(true);
                    paranoiaContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Paranoia = 1;
            }
            else if (DataBaseManager.NowPage_Paranoia == 3)
            {
                if (countParanoia >= 6)
                {
                    Reset_Paranoia();
                    paranoiaContents.transform.GetChild(2).gameObject.SetActive(true);
                    paranoiaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countParanoia >= 5)
                {
                    Reset_Paranoia();
                    paranoiaContents.transform.GetChild(3).gameObject.SetActive(true);
                    paranoiaContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Paranoia = 2;
            }
        }
        if (nowPage == "Insomnia")
        {
            if (DataBaseManager.NowPage_Insomnia == 2)
            {
                if (countInsomnia >= 6)
                {
                    Reset_Insomnia();
                    insomniaContents.transform.GetChild(0).gameObject.SetActive(true);
                    insomniaContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countInsomnia >= 5)
                {
                    Reset_Insomnia();
                    insomniaContents.transform.GetChild(1).gameObject.SetActive(true);
                    insomniaContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countInsomnia >= 4)
                {
                    Reset_Insomnia();
                    insomniaContents.transform.GetChild(2).gameObject.SetActive(true);
                    insomniaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countInsomnia >= 3)
                {
                    Reset_Insomnia();
                    insomniaContents.transform.GetChild(3).gameObject.SetActive(true);
                    insomniaContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Insomnia = 1;
            }
            else if (DataBaseManager.NowPage_Insomnia == 3)
            {
                if (countInsomnia >= 6)
                {
                    Reset_Insomnia();
                    insomniaContents.transform.GetChild(2).gameObject.SetActive(true);
                    insomniaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countInsomnia >= 5)
                {
                    Reset_Insomnia();
                    insomniaContents.transform.GetChild(3).gameObject.SetActive(true);
                    insomniaContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Insomnia = 2;
            }
        }
        if (nowPage == "SewerGhostStory")
        {
            if (DataBaseManager.NowPage_SewerGhostStory == 2)
            {
                if (countSewerGhostStory >= 6)
                {
                    Reset_SewerGhostStory();
                    sewerGhostStoryContents.transform.GetChild(0).gameObject.SetActive(true);
                    sewerGhostStoryContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countSewerGhostStory >= 5)
                {
                    Reset_SewerGhostStory();
                    sewerGhostStoryContents.transform.GetChild(1).gameObject.SetActive(true);
                    sewerGhostStoryContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countSewerGhostStory >= 4)
                {
                    Reset_SewerGhostStory();
                    sewerGhostStoryContents.transform.GetChild(2).gameObject.SetActive(true);
                    sewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSewerGhostStory >= 3)
                {
                    Reset_SewerGhostStory();
                    sewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(true);
                    sewerGhostStoryContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_SewerGhostStory = 1;
            }
            else if (DataBaseManager.NowPage_SewerGhostStory == 3)
            {
                if (countSewerGhostStory >= 6)
                {
                    Reset_SewerGhostStory();
                    sewerGhostStoryContents.transform.GetChild(2).gameObject.SetActive(true);
                    sewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSewerGhostStory >= 5)
                {
                    Reset_SewerGhostStory();
                    sewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(true);
                    sewerGhostStoryContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewerGhostStory = 2;
            }
        }
        if (nowPage == "MissingPeople")
        {
            if (DataBaseManager.NowPage_MissingPeople == 2)
            {
                if (countMissingPeople >= 6)
                {
                    Reset_MissingPeople();
                    missingPeopleContents.transform.GetChild(0).gameObject.SetActive(true);
                    missingPeopleContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countMissingPeople >= 5)
                {
                    Reset_MissingPeople();
                    missingPeopleContents.transform.GetChild(1).gameObject.SetActive(true);
                    missingPeopleContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countMissingPeople >= 4)
                {
                    Reset_MissingPeople();
                    missingPeopleContents.transform.GetChild(2).gameObject.SetActive(true);
                    missingPeopleContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countMissingPeople >= 3)
                {
                    Reset_MissingPeople();
                    missingPeopleContents.transform.GetChild(3).gameObject.SetActive(true);
                    missingPeopleContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_MissingPeople = 1;
            }
            else if (DataBaseManager.NowPage_MissingPeople == 3)
            {
                if (countMissingPeople >= 6)
                {
                    Reset_MissingPeople();
                    missingPeopleContents.transform.GetChild(2).gameObject.SetActive(true);
                    missingPeopleContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countMissingPeople >= 5)
                {
                    Reset_MissingPeople();
                    missingPeopleContents.transform.GetChild(3).gameObject.SetActive(true);
                    missingPeopleContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_MissingPeople = 2;
            }
        }
        if (nowPage == "FishySmell")
        {
            if (DataBaseManager.NowPage_FishySmell == 2)
            {
                if (countFishySmell >= 6)
                {
                    Reset_FishySmell();
                    fishySmellContents.transform.GetChild(0).gameObject.SetActive(true);
                    fishySmellContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countFishySmell >= 5)
                {
                    Reset_FishySmell();
                    fishySmellContents.transform.GetChild(1).gameObject.SetActive(true);
                    fishySmellContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countFishySmell >= 4)
                {
                    Reset_FishySmell();
                    fishySmellContents.transform.GetChild(2).gameObject.SetActive(true);
                    fishySmellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countFishySmell >= 3)
                {
                    Reset_FishySmell();
                    fishySmellContents.transform.GetChild(3).gameObject.SetActive(true);
                    fishySmellContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_FishySmell = 1;
            }
            else if (DataBaseManager.NowPage_FishySmell == 3)
            {
                if (countFishySmell >= 6)
                {
                    Reset_FishySmell();
                    fishySmellContents.transform.GetChild(2).gameObject.SetActive(true);
                    fishySmellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countFishySmell >= 5)
                {
                    Reset_FishySmell();
                    fishySmellContents.transform.GetChild(3).gameObject.SetActive(true);
                    fishySmellContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_FishySmell = 2;
            }
        }
        if (nowPage == "RiverWaterQuality")
        {
            if (DataBaseManager.NowPage_RiverWaterQuality == 2)
            {
                if (countRiverWaterQuality >= 6)
                {
                    Reset_RiverWaterQuality();
                    riverWaterQualityContents.transform.GetChild(0).gameObject.SetActive(true);
                    riverWaterQualityContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countRiverWaterQuality >= 5)
                {
                    Reset_RiverWaterQuality();
                    riverWaterQualityContents.transform.GetChild(1).gameObject.SetActive(true);
                    riverWaterQualityContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countRiverWaterQuality >= 4)
                {
                    Reset_RiverWaterQuality();
                    riverWaterQualityContents.transform.GetChild(2).gameObject.SetActive(true);
                    riverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countRiverWaterQuality >= 3)
                {
                    Reset_RiverWaterQuality();
                    riverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(true);
                    riverWaterQualityContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_RiverWaterQuality = 1;
            }
            else if (DataBaseManager.NowPage_RiverWaterQuality == 3)
            {
                if (countRiverWaterQuality >= 6)
                {
                    Reset_RiverWaterQuality();
                    riverWaterQualityContents.transform.GetChild(2).gameObject.SetActive(true);
                    riverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countRiverWaterQuality >= 5)
                {
                    Reset_RiverWaterQuality();
                    riverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(true);
                    riverWaterQualityContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_RiverWaterQuality = 2;
            }
        }
        if (nowPage == "CreepyEyes")
        {
            if (DataBaseManager.NowPage_CreepyEyes == 2)
            {
                if (countCreepyEyes >= 6)
                {
                    Reset_CreepyEyes();
                    creepyEyesContents.transform.GetChild(0).gameObject.SetActive(true);
                    creepyEyesContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countCreepyEyes >= 5)
                {
                    Reset_CreepyEyes();
                    creepyEyesContents.transform.GetChild(1).gameObject.SetActive(true);
                    creepyEyesContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countCreepyEyes >= 4)
                {
                    Reset_CreepyEyes();
                    creepyEyesContents.transform.GetChild(2).gameObject.SetActive(true);
                    creepyEyesContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countCreepyEyes >= 3)
                {
                    Reset_CreepyEyes();
                    creepyEyesContents.transform.GetChild(3).gameObject.SetActive(true);
                    creepyEyesContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_CreepyEyes = 1;
            }
            else if (DataBaseManager.NowPage_CreepyEyes == 3)
            {
                if (countCreepyEyes >= 6)
                {
                    Reset_CreepyEyes();
                    creepyEyesContents.transform.GetChild(2).gameObject.SetActive(true);
                    creepyEyesContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countCreepyEyes >= 5)
                {
                    Reset_CreepyEyes();
                    creepyEyesContents.transform.GetChild(3).gameObject.SetActive(true);
                    creepyEyesContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_CreepyEyes = 2;
            }
        }

        if (nowPage == "Safe")
        {
            if (DataBaseManager.NowPage_Safe == 2)
            {
                if (countSafe >= 6)
                {
                    Reset_Safe();
                    safeContents.transform.GetChild(0).gameObject.SetActive(true);
                    safeContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countSafe >= 5)
                {
                    Reset_Safe();
                    safeContents.transform.GetChild(1).gameObject.SetActive(true);
                    safeContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countSafe >= 4)
                {
                    Reset_Safe();
                    safeContents.transform.GetChild(2).gameObject.SetActive(true);
                    safeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSafe >= 3)
                {
                    Reset_Safe();
                    safeContents.transform.GetChild(3).gameObject.SetActive(true);
                    safeContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Safe = 1;
            }
            else if (DataBaseManager.NowPage_Safe == 3)
            {
                if (countSafe >= 6)
                {
                    Reset_Safe();
                    safeContents.transform.GetChild(2).gameObject.SetActive(true);
                    safeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSafe >= 5)
                {
                    Reset_Safe();
                    safeContents.transform.GetChild(3).gameObject.SetActive(true);
                    safeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Safe = 2;
            }
        }
        if (nowPage == "MurderCase")
        {
            if (DataBaseManager.NowPage_MurderCase == 2)
            {
                if (countMurderCase >= 6)
                {
                    Reset_MurderCase();
                    murderCaseContents.transform.GetChild(0).gameObject.SetActive(true);
                    murderCaseContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countMurderCase >= 5)
                {
                    Reset_MurderCase();
                    murderCaseContents.transform.GetChild(1).gameObject.SetActive(true);
                    murderCaseContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countMurderCase >= 4)
                {
                    Reset_MurderCase();
                    murderCaseContents.transform.GetChild(2).gameObject.SetActive(true);
                    murderCaseContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countMurderCase >= 3)
                {
                    Reset_MurderCase();
                    murderCaseContents.transform.GetChild(3).gameObject.SetActive(true);
                    murderCaseContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_MurderCase = 1;
            }
            else if (DataBaseManager.NowPage_MurderCase == 3)
            {
                if (countMurderCase >= 6)
                {
                    Reset_MurderCase();
                    murderCaseContents.transform.GetChild(2).gameObject.SetActive(true);
                    murderCaseContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countMurderCase >= 5)
                {
                    Reset_MurderCase();
                    murderCaseContents.transform.GetChild(3).gameObject.SetActive(true);
                    murderCaseContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_MurderCase = 2;
            }
        }
    }

    void Reset_PlanetarySequence()
    {
        planetarySequenceContents.transform.GetChild(0).gameObject.SetActive(false);
        planetarySequenceContents.transform.GetChild(1).gameObject.SetActive(false);
        planetarySequenceContents.transform.GetChild(2).gameObject.SetActive(false);
        planetarySequenceContents.transform.GetChild(3).gameObject.SetActive(false);
        planetarySequenceContents.transform.GetChild(4).gameObject.SetActive(false);
        planetarySequenceContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Nightmare()
    {
        nightmareContents.transform.GetChild(0).gameObject.SetActive(false);
        nightmareContents.transform.GetChild(1).gameObject.SetActive(false);
        nightmareContents.transform.GetChild(2).gameObject.SetActive(false);
        nightmareContents.transform.GetChild(3).gameObject.SetActive(false);
        nightmareContents.transform.GetChild(4).gameObject.SetActive(false);
        nightmareContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Paranoia()
    {
        paranoiaContents.transform.GetChild(0).gameObject.SetActive(false);
        paranoiaContents.transform.GetChild(1).gameObject.SetActive(false);
        paranoiaContents.transform.GetChild(2).gameObject.SetActive(false);
        paranoiaContents.transform.GetChild(3).gameObject.SetActive(false);
        paranoiaContents.transform.GetChild(4).gameObject.SetActive(false);
        paranoiaContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Insomnia()
    {
        insomniaContents.transform.GetChild(0).gameObject.SetActive(false);
        insomniaContents.transform.GetChild(1).gameObject.SetActive(false);
        insomniaContents.transform.GetChild(2).gameObject.SetActive(false);
        insomniaContents.transform.GetChild(3).gameObject.SetActive(false);
        insomniaContents.transform.GetChild(4).gameObject.SetActive(false);
        insomniaContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_SewerGhostStory()
    {
        sewerGhostStoryContents.transform.GetChild(0).gameObject.SetActive(false);
        sewerGhostStoryContents.transform.GetChild(1).gameObject.SetActive(false);
        sewerGhostStoryContents.transform.GetChild(2).gameObject.SetActive(false);
        sewerGhostStoryContents.transform.GetChild(3).gameObject.SetActive(false);
        sewerGhostStoryContents.transform.GetChild(4).gameObject.SetActive(false);
        sewerGhostStoryContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_MissingPeople()
    {
        missingPeopleContents.transform.GetChild(0).gameObject.SetActive(false);
        missingPeopleContents.transform.GetChild(1).gameObject.SetActive(false);
        missingPeopleContents.transform.GetChild(2).gameObject.SetActive(false);
        missingPeopleContents.transform.GetChild(3).gameObject.SetActive(false);
        missingPeopleContents.transform.GetChild(4).gameObject.SetActive(false);
        missingPeopleContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_FishySmell()
    {
        fishySmellContents.transform.GetChild(0).gameObject.SetActive(false);
        fishySmellContents.transform.GetChild(1).gameObject.SetActive(false);
        fishySmellContents.transform.GetChild(2).gameObject.SetActive(false);
        fishySmellContents.transform.GetChild(3).gameObject.SetActive(false);
        fishySmellContents.transform.GetChild(4).gameObject.SetActive(false);
        fishySmellContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_RiverWaterQuality()
    {
        riverWaterQualityContents.transform.GetChild(0).gameObject.SetActive(false);
        riverWaterQualityContents.transform.GetChild(1).gameObject.SetActive(false);
        riverWaterQualityContents.transform.GetChild(2).gameObject.SetActive(false);
        riverWaterQualityContents.transform.GetChild(3).gameObject.SetActive(false);
        riverWaterQualityContents.transform.GetChild(4).gameObject.SetActive(false);
        riverWaterQualityContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_CreepyEyes()
    {
        creepyEyesContents.transform.GetChild(0).gameObject.SetActive(false);
        creepyEyesContents.transform.GetChild(1).gameObject.SetActive(false);
        creepyEyesContents.transform.GetChild(2).gameObject.SetActive(false);
        creepyEyesContents.transform.GetChild(3).gameObject.SetActive(false);
        creepyEyesContents.transform.GetChild(4).gameObject.SetActive(false);
        creepyEyesContents.transform.GetChild(5).gameObject.SetActive(false);
    }

    void Reset_Safe()
    {
        safeContents.transform.GetChild(0).gameObject.SetActive(false);
        safeContents.transform.GetChild(1).gameObject.SetActive(false);
        safeContents.transform.GetChild(2).gameObject.SetActive(false);
        safeContents.transform.GetChild(3).gameObject.SetActive(false);
        safeContents.transform.GetChild(4).gameObject.SetActive(false);
        safeContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_MurderCase()
    {
        murderCaseContents.transform.GetChild(0).gameObject.SetActive(false);
        murderCaseContents.transform.GetChild(1).gameObject.SetActive(false);
        murderCaseContents.transform.GetChild(2).gameObject.SetActive(false);
        murderCaseContents.transform.GetChild(3).gameObject.SetActive(false);
        murderCaseContents.transform.GetChild(4).gameObject.SetActive(false);
        murderCaseContents.transform.GetChild(5).gameObject.SetActive(false);
    }

    public void Open_PlanetarySequence()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        planetarySequenceDetail.SetActive(true);
    }
    public void Open_Nightmare()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        nightmareDetail.SetActive(true);
    }
    public void Open_Paranoia()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        paranoiaDetail.SetActive(true);
    }
    public void Open_Insomnia()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        insomniaDetail.SetActive(true);
    }
    public void Open_SewerGhostStory()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        sewerGhostStoryDetail.SetActive(true);
    }
    public void Open_MissingPeople()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        missingPeopleDetail.SetActive(true);
    }
    public void Open_FishySmell()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        fishySmellDetail.SetActive(true);
    }
    public void Open_RiverWaterQuality()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        riverWaterQualityDetail.SetActive(true);
    }
    public void Open_CreepyEyes()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        creepyEyes_etail.SetActive(true);
    }

    public void Open_Safe()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        safeDetail.SetActive(true);
    }
    public void Open_MurderCase()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        murderCaseDetail.SetActive(true);
    }
    void CloseAllContents()
    {
        planetarySequenceDetail.SetActive(false);
        paranoiaDetail.SetActive(false);
        nightmareDetail.SetActive(false);
        insomniaDetail.SetActive(false);
        sewerGhostStoryDetail.SetActive(false);
        missingPeopleDetail.SetActive(false);
        fishySmellDetail.SetActive(false);
        riverWaterQualityDetail.SetActive(false);
        creepyEyes_etail.SetActive(false);
        safeDetail.SetActive(false);
        murderCaseDetail.SetActive(false);
    }
}



