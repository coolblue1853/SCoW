using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    string NowPage;

     GameObject Detail_SewageMaintenanceOffice1;
     GameObject Detail_SewageMaintenanceOffice2;
     GameObject Detail_SewageMaintenanceOffice3;
     GameObject Detail_SewageMaintenanceOffice4;
     GameObject Detail_SewageMaintenanceOffice5;
     GameObject Detail_SewageMaintenanceOffice6;
    bool ON_SewageMaintenanceOffice1;
    bool ON_SewageMaintenanceOffice2;
    bool ON_SewageMaintenanceOffice3;
    bool ON_SewageMaintenanceOffice4;
    bool ON_SewageMaintenanceOffice5;
    bool ON_SewageMaintenanceOffice6;
    int Count_SewageMaintenanceOffice = 0;
    public GameObject SewageMaintenanceOfficeContents;
    public GameObject SewageMaintenanceOffice_Detail; 
    public static int SewageMaintenanceOfficeIntelInt;
    public GameObject NextButton_SewageMaintenanceOffice;
    public GameObject PrevButton_SewageMaintenanceOffice;

    void SewageMaintenanceOfficeDetail()
    {
        if (ON_SewageMaintenanceOffice1 == false && DataBaseManager.Intel_SewageMaintenanceOffice1 == true)
        {
            ON_SewageMaintenanceOffice1 = true;
            Detail_SewageMaintenanceOffice1.SetActive(true);
            Detail_SewageMaintenanceOffice1.transform.SetAsLastSibling();
            Count_SewageMaintenanceOffice += 1;
        }
        if (ON_SewageMaintenanceOffice2 == false && DataBaseManager.Intel_SewageMaintenanceOffice2 == true)
        {
            ON_SewageMaintenanceOffice2 = true;
            Detail_SewageMaintenanceOffice2.SetActive(true);
            Detail_SewageMaintenanceOffice2.transform.SetAsLastSibling();
            Count_SewageMaintenanceOffice += 1;
        }
        if (ON_SewageMaintenanceOffice3 == false && DataBaseManager.Intel_SewageMaintenanceOffice3 == true)
        {
            ON_SewageMaintenanceOffice3 = true;
            Detail_SewageMaintenanceOffice3.SetActive(true);
            Detail_SewageMaintenanceOffice3.transform.SetAsLastSibling();
            Count_SewageMaintenanceOffice += 1;
        }
        if (ON_SewageMaintenanceOffice4 == false && DataBaseManager.Intel_SewageMaintenanceOffice4 == true)
        {
            ON_SewageMaintenanceOffice4 = true;
            Detail_SewageMaintenanceOffice4.SetActive(true);
            Detail_SewageMaintenanceOffice4.transform.SetAsLastSibling();
            Count_SewageMaintenanceOffice += 1;
        }
        if (ON_SewageMaintenanceOffice5 == false && DataBaseManager.Intel_SewageMaintenanceOffice5 == true)
        {
            ON_SewageMaintenanceOffice5 = true;
            Detail_SewageMaintenanceOffice5.SetActive(true);
            Detail_SewageMaintenanceOffice5.transform.SetAsLastSibling();
            Count_SewageMaintenanceOffice += 1;
        }
        if (ON_SewageMaintenanceOffice6 == false && DataBaseManager.Intel_SewageMaintenanceOffice6 == true)
        {
            ON_SewageMaintenanceOffice6 = true;
            Detail_SewageMaintenanceOffice6.SetActive(true);
            Detail_SewageMaintenanceOffice6.transform.SetAsLastSibling();
            Count_SewageMaintenanceOffice += 1;
        }

        if (Count_SewageMaintenanceOffice > 2 && DataBaseManager.NowPage_SewageMaintenanceOffice == 1)
        {
            NextButton_SewageMaintenanceOffice.SetActive(true);
        }
        else if (Count_SewageMaintenanceOffice > 4 && DataBaseManager.NowPage_SewageMaintenanceOffice == 2)
        {
            NextButton_SewageMaintenanceOffice.SetActive(true);
        }
        else
        {
            NextButton_SewageMaintenanceOffice.SetActive(false);
        }

        if (DataBaseManager.NowPage_SewageMaintenanceOffice == 2)
        {
            PrevButton_SewageMaintenanceOffice.SetActive(true);
        }
        else if (DataBaseManager.NowPage_SewageMaintenanceOffice == 3)
        {
            PrevButton_SewageMaintenanceOffice.SetActive(true);
        }
        else
        {
            PrevButton_SewageMaintenanceOffice.SetActive(false);
        }
    }


    public void NextPage()
    {

        if (NowPage == "SewageMaintenanceOffice")
        {
            if (DataBaseManager.NowPage_SewageMaintenanceOffice == 1)
            {
                if (Count_SewageMaintenanceOffice >= 6)
                {
                    Reset_SewageMaintenanceOffice();
                    SewageMaintenanceOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    SewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_SewageMaintenanceOffice >= 5)
                {
                    Reset_SewageMaintenanceOffice();
                    SewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    SewageMaintenanceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_SewageMaintenanceOffice >= 4)
                {
                    Reset_SewageMaintenanceOffice();
                    SewageMaintenanceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                    SewageMaintenanceOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_SewageMaintenanceOffice >= 3)
                {
                    Reset_SewageMaintenanceOffice();
                    SewageMaintenanceOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewageMaintenanceOffice = 2;
            }
            else if (DataBaseManager.NowPage_SewageMaintenanceOffice == 2)
            {
                if (Count_SewageMaintenanceOffice >= 6)
                {
                    Reset_SewageMaintenanceOffice();
                    SewageMaintenanceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                    SewageMaintenanceOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_SewageMaintenanceOffice >= 5)
                {
                    Reset_SewageMaintenanceOffice();
                    SewageMaintenanceOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewageMaintenanceOffice = 3;
            }
        }
    }

    public void PrevPage()
    {
     
        if (NowPage == "SewageMaintenanceOffice")
        {
            if (DataBaseManager.NowPage_SewageMaintenanceOffice == 2)
            {
                if (Count_SewageMaintenanceOffice >= 6)
                {
                    Reset_SewageMaintenanceOffice();
                    SewageMaintenanceOfficeContents.transform.GetChild(0).gameObject.SetActive(true);
                    SewageMaintenanceOfficeContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_SewageMaintenanceOffice >= 5)
                {
                    Reset_SewageMaintenanceOffice();
                    SewageMaintenanceOfficeContents.transform.GetChild(1).gameObject.SetActive(true);
                    SewageMaintenanceOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_SewageMaintenanceOffice >= 4)
                {
                    Reset_SewageMaintenanceOffice();
                    SewageMaintenanceOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    SewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_SewageMaintenanceOffice >= 3)
                {
                    Reset_SewageMaintenanceOffice();
                    SewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    SewageMaintenanceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_SewageMaintenanceOffice = 1;
            }
            else if (DataBaseManager.NowPage_SewageMaintenanceOffice == 3)
            {
                if (Count_SewageMaintenanceOffice >= 6)
                {
                    Reset_SewageMaintenanceOffice();
                    SewageMaintenanceOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    SewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_SewageMaintenanceOffice >= 5)
                {
                    Reset_SewageMaintenanceOffice();
                    SewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    SewageMaintenanceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewageMaintenanceOffice = 2;
            }
        }
    }



    void Reset_SewageMaintenanceOffice()
    {
        SewageMaintenanceOfficeContents.transform.GetChild(0).gameObject.SetActive(false);
        SewageMaintenanceOfficeContents.transform.GetChild(1).gameObject.SetActive(false);
        SewageMaintenanceOfficeContents.transform.GetChild(2).gameObject.SetActive(false);
        SewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(false);
        SewageMaintenanceOfficeContents.transform.GetChild(4).gameObject.SetActive(false);
        SewageMaintenanceOfficeContents.transform.GetChild(5).gameObject.SetActive(false);
    }
}
