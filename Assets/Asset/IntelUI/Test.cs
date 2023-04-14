using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    string NowPage;

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



    void Reset_SuddenChange()
    {
        SuddenChangeContents.transform.GetChild(0).gameObject.SetActive(false);
        SuddenChangeContents.transform.GetChild(1).gameObject.SetActive(false);
        SuddenChangeContents.transform.GetChild(2).gameObject.SetActive(false);
        SuddenChangeContents.transform.GetChild(3).gameObject.SetActive(false);
        SuddenChangeContents.transform.GetChild(4).gameObject.SetActive(false);
        SuddenChangeContents.transform.GetChild(5).gameObject.SetActive(false);
    }
}
