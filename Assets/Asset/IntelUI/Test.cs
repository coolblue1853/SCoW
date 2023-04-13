using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    string NowPage;

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
