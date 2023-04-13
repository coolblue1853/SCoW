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
}



