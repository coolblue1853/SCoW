using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    string NowPage;

     GameObject Detail_Wharf1;
     GameObject Detail_Wharf2;
     GameObject Detail_Wharf3;
     GameObject Detail_Wharf4;
     GameObject Detail_Wharf5;
     GameObject Detail_Wharf6;
    bool ON_Wharf1;
    bool ON_Wharf2;
    bool ON_Wharf3;
    bool ON_Wharf4;
    bool ON_Wharf5;
    bool ON_Wharf6;
    int Count_Wharf = 0;
    public GameObject WharfContents;
    public GameObject Wharf_Detail; 
    public static int WharfIntelInt;
    public GameObject NextButton_Wharf;
    public GameObject PrevButton_Wharf;

    void WharfDetail()
    {
        if (ON_Wharf1 == false && DataBaseManager.Intel_Wharf1 == true)
        {
            ON_Wharf1 = true;
            Detail_Wharf1.SetActive(true);
            Detail_Wharf1.transform.SetAsLastSibling();
            Count_Wharf += 1;
        }
        if (ON_Wharf2 == false && DataBaseManager.Intel_Wharf2 == true)
        {
            ON_Wharf2 = true;
            Detail_Wharf2.SetActive(true);
            Detail_Wharf2.transform.SetAsLastSibling();
            Count_Wharf += 1;
        }
        if (ON_Wharf3 == false && DataBaseManager.Intel_Wharf3 == true)
        {
            ON_Wharf3 = true;
            Detail_Wharf3.SetActive(true);
            Detail_Wharf3.transform.SetAsLastSibling();
            Count_Wharf += 1;
        }
        if (ON_Wharf4 == false && DataBaseManager.Intel_Wharf4 == true)
        {
            ON_Wharf4 = true;
            Detail_Wharf4.SetActive(true);
            Detail_Wharf4.transform.SetAsLastSibling();
            Count_Wharf += 1;
        }
        if (ON_Wharf5 == false && DataBaseManager.Intel_Wharf5 == true)
        {
            ON_Wharf5 = true;
            Detail_Wharf5.SetActive(true);
            Detail_Wharf5.transform.SetAsLastSibling();
            Count_Wharf += 1;
        }
        if (ON_Wharf6 == false && DataBaseManager.Intel_Wharf6 == true)
        {
            ON_Wharf6 = true;
            Detail_Wharf6.SetActive(true);
            Detail_Wharf6.transform.SetAsLastSibling();
            Count_Wharf += 1;
        }

        if (Count_Wharf > 2 && DataBaseManager.NowPage_Wharf == 1)
        {
            NextButton_Wharf.SetActive(true);
        }
        else if (Count_Wharf > 4 && DataBaseManager.NowPage_Wharf == 2)
        {
            NextButton_Wharf.SetActive(true);
        }
        else
        {
            NextButton_Wharf.SetActive(false);
        }

        if (DataBaseManager.NowPage_Wharf == 2)
        {
            PrevButton_Wharf.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Wharf == 3)
        {
            PrevButton_Wharf.SetActive(true);
        }
        else
        {
            PrevButton_Wharf.SetActive(false);
        }
    }


    public void NextPage()
    {

        if (NowPage == "Wharf")
        {
            if (DataBaseManager.NowPage_Wharf == 1)
            {
                if (Count_Wharf >= 6)
                {
                    Reset_Wharf();
                    WharfContents.transform.GetChild(2).gameObject.SetActive(true);
                    WharfContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Wharf >= 5)
                {
                    Reset_Wharf();
                    WharfContents.transform.GetChild(3).gameObject.SetActive(true);
                    WharfContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Wharf >= 4)
                {
                    Reset_Wharf();
                    WharfContents.transform.GetChild(4).gameObject.SetActive(true);
                    WharfContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Wharf >= 3)
                {
                    Reset_Wharf();
                    WharfContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Wharf = 2;
            }
            else if (DataBaseManager.NowPage_Wharf == 2)
            {
                if (Count_Wharf >= 6)
                {
                    Reset_Wharf();
                    WharfContents.transform.GetChild(4).gameObject.SetActive(true);
                    WharfContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Wharf >= 5)
                {
                    Reset_Wharf();
                    WharfContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Wharf = 3;
            }
        }
    }

    public void PrevPage()
    {
     
        if (NowPage == "Wharf")
        {
            if (DataBaseManager.NowPage_Wharf == 2)
            {
                if (Count_Wharf >= 6)
                {
                    Reset_Wharf();
                    WharfContents.transform.GetChild(0).gameObject.SetActive(true);
                    WharfContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Wharf >= 5)
                {
                    Reset_Wharf();
                    WharfContents.transform.GetChild(1).gameObject.SetActive(true);
                    WharfContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Wharf >= 4)
                {
                    Reset_Wharf();
                    WharfContents.transform.GetChild(2).gameObject.SetActive(true);
                    WharfContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Wharf >= 3)
                {
                    Reset_Wharf();
                    WharfContents.transform.GetChild(3).gameObject.SetActive(true);
                    WharfContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Wharf = 1;
            }
            else if (DataBaseManager.NowPage_Wharf == 3)
            {
                if (Count_Wharf >= 6)
                {
                    Reset_Wharf();
                    WharfContents.transform.GetChild(2).gameObject.SetActive(true);
                    WharfContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Wharf >= 5)
                {
                    Reset_Wharf();
                    WharfContents.transform.GetChild(3).gameObject.SetActive(true);
                    WharfContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Wharf = 2;
            }
        }
    }



    void Reset_Wharf()
    {
        WharfContents.transform.GetChild(0).gameObject.SetActive(false);
        WharfContents.transform.GetChild(1).gameObject.SetActive(false);
        WharfContents.transform.GetChild(2).gameObject.SetActive(false);
        WharfContents.transform.GetChild(3).gameObject.SetActive(false);
        WharfContents.transform.GetChild(4).gameObject.SetActive(false);
        WharfContents.transform.GetChild(5).gameObject.SetActive(false);
    }
}
