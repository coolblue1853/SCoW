using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    string NowPage;

     GameObject Detail_BlackWell1;
     GameObject Detail_BlackWell2;
     GameObject Detail_BlackWell3;
     GameObject Detail_BlackWell4;
     GameObject Detail_BlackWell5;
     GameObject Detail_BlackWell6;
    bool ON_BlackWell1;
    bool ON_BlackWell2;
    bool ON_BlackWell3;
    bool ON_BlackWell4;
    bool ON_BlackWell5;
    bool ON_BlackWell6;
    int Count_BlackWell = 0;
    public GameObject BlackWellContents;
    public GameObject BlackWell_Detail; 
    public static int BlackWellIntelInt;
    public GameObject NextButton_BlackWell;
    public GameObject PrevButton_BlackWell;

    void BlackWellDetail()
    {
        if (ON_BlackWell1 == false && DataBaseManager.Intel_BlackWell1 == true)
        {
            ON_BlackWell1 = true;
            Detail_BlackWell1.SetActive(true);
            Detail_BlackWell1.transform.SetAsLastSibling();
            Count_BlackWell += 1;
        }
        if (ON_BlackWell2 == false && DataBaseManager.Intel_BlackWell2 == true)
        {
            ON_BlackWell2 = true;
            Detail_BlackWell2.SetActive(true);
            Detail_BlackWell2.transform.SetAsLastSibling();
            Count_BlackWell += 1;
        }
        if (ON_BlackWell3 == false && DataBaseManager.Intel_BlackWell3 == true)
        {
            ON_BlackWell3 = true;
            Detail_BlackWell3.SetActive(true);
            Detail_BlackWell3.transform.SetAsLastSibling();
            Count_BlackWell += 1;
        }
        if (ON_BlackWell4 == false && DataBaseManager.Intel_BlackWell4 == true)
        {
            ON_BlackWell4 = true;
            Detail_BlackWell4.SetActive(true);
            Detail_BlackWell4.transform.SetAsLastSibling();
            Count_BlackWell += 1;
        }
        if (ON_BlackWell5 == false && DataBaseManager.Intel_BlackWell5 == true)
        {
            ON_BlackWell5 = true;
            Detail_BlackWell5.SetActive(true);
            Detail_BlackWell5.transform.SetAsLastSibling();
            Count_BlackWell += 1;
        }
        if (ON_BlackWell6 == false && DataBaseManager.Intel_BlackWell6 == true)
        {
            ON_BlackWell6 = true;
            Detail_BlackWell6.SetActive(true);
            Detail_BlackWell6.transform.SetAsLastSibling();
            Count_BlackWell += 1;
        }

        if (Count_BlackWell > 2 && DataBaseManager.NowPage_BlackWell == 1)
        {
            NextButton_BlackWell.SetActive(true);
        }
        else if (Count_BlackWell > 4 && DataBaseManager.NowPage_BlackWell == 2)
        {
            NextButton_BlackWell.SetActive(true);
        }
        else
        {
            NextButton_BlackWell.SetActive(false);
        }

        if (DataBaseManager.NowPage_BlackWell == 2)
        {
            PrevButton_BlackWell.SetActive(true);
        }
        else if (DataBaseManager.NowPage_BlackWell == 3)
        {
            PrevButton_BlackWell.SetActive(true);
        }
        else
        {
            PrevButton_BlackWell.SetActive(false);
        }
    }


    public void NextPage()
    {

        if (NowPage == "BlackWell")
        {
            if (DataBaseManager.NowPage_BlackWell == 1)
            {
                if (Count_BlackWell >= 6)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(2).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 5)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 4)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 3)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_BlackWell = 2;
            }
            else if (DataBaseManager.NowPage_BlackWell == 2)
            {
                if (Count_BlackWell >= 6)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 5)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_BlackWell = 3;
            }
        }
    }

    public void PrevPage()
    {
     
        if (NowPage == "BlackWell")
        {
            if (DataBaseManager.NowPage_BlackWell == 2)
            {
                if (Count_BlackWell >= 6)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(0).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 5)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(1).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 4)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(2).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 3)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_BlackWell = 1;
            }
            else if (DataBaseManager.NowPage_BlackWell == 3)
            {
                if (Count_BlackWell >= 6)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(2).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 5)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_BlackWell = 2;
            }
        }
    }



    void Reset_BlackWell()
    {
        BlackWellContents.transform.GetChild(0).gameObject.SetActive(false);
        BlackWellContents.transform.GetChild(1).gameObject.SetActive(false);
        BlackWellContents.transform.GetChild(2).gameObject.SetActive(false);
        BlackWellContents.transform.GetChild(3).gameObject.SetActive(false);
        BlackWellContents.transform.GetChild(4).gameObject.SetActive(false);
        BlackWellContents.transform.GetChild(5).gameObject.SetActive(false);
    }
}
