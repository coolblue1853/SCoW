using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelPlaceDetail : MonoBehaviour
{
    GameObject Detail_DetectiveOffice1;
    GameObject Detail_DetectiveOffice2;
    GameObject Detail_DetectiveOffice3;
    GameObject Detail_DetectiveOffice4;
    GameObject Detail_DetectiveOffice5;
    GameObject Detail_DetectiveOffice6;
    bool ON_DetectiveOffice1;
    bool ON_DetectiveOffice2;
    bool ON_DetectiveOffice3;
    bool ON_DetectiveOffice4;
    bool ON_DetectiveOffice5;
    bool ON_DetectiveOffice6;
    int Count_DetectiveOffice = 0;
    public GameObject DetectiveOfficeContents;
    public GameObject DetectiveOffice_Detail;
    public static int DetectiveOfficeIntelInt;
    public GameObject NextButton_DetectiveOffice;
    public GameObject PrevButton_DetectiveOffice;
    string NowPage;

    GameObject Detail_Hospital1;
    GameObject Detail_Hospital2;
    GameObject Detail_Hospital3;
    GameObject Detail_Hospital4;
    GameObject Detail_Hospital5;
    GameObject Detail_Hospital6;
    bool ON_Hospital1;
    bool ON_Hospital2;
    bool ON_Hospital3;
    bool ON_Hospital4;
    bool ON_Hospital5;
    bool ON_Hospital6;
    int Count_Hospital = 0;
    public GameObject HospitalContents;
    public GameObject Hospital_Detail;
    public static int HospitalIntelInt;
    public GameObject NextButton_Hospital;
    public GameObject PrevButton_Hospital;

    GameObject Detail_Newspaper1;
    GameObject Detail_Newspaper2;
    GameObject Detail_Newspaper3;
    GameObject Detail_Newspaper4;
    GameObject Detail_Newspaper5;
    GameObject Detail_Newspaper6;
    bool ON_Newspaper1;
    bool ON_Newspaper2;
    bool ON_Newspaper3;
    bool ON_Newspaper4;
    bool ON_Newspaper5;
    bool ON_Newspaper6;
    int Count_Newspaper = 0;
    public GameObject NewspaperContents;
    public GameObject Newspaper_Detail;
    public static int NewspaperIntelInt;
    public GameObject NextButton_Newspaper;
    public GameObject PrevButton_Newspaper;

    GameObject Detail_QuestHouse1;
    GameObject Detail_QuestHouse2;
    GameObject Detail_QuestHouse3;
    GameObject Detail_QuestHouse4;
    GameObject Detail_QuestHouse5;
    GameObject Detail_QuestHouse6;
    bool ON_QuestHouse1;
    bool ON_QuestHouse2;
    bool ON_QuestHouse3;
    bool ON_QuestHouse4;
    bool ON_QuestHouse5;
    bool ON_QuestHouse6;
    int Count_QuestHouse = 0;
    public GameObject QuestHouseContents;
    public GameObject QuestHouse_Detail;
    public static int QuestHouseIntelInt;
    public GameObject NextButton_QuestHouse;
    public GameObject PrevButton_QuestHouse;

    GameObject Detail_Station1;
    GameObject Detail_Station2;
    GameObject Detail_Station3;
    GameObject Detail_Station4;
    GameObject Detail_Station5;
    GameObject Detail_Station6;
    bool ON_Station1;
    bool ON_Station2;
    bool ON_Station3;
    bool ON_Station4;
    bool ON_Station5;
    bool ON_Station6;
    int Count_Station = 0;
    public GameObject StationContents;
    public GameObject Station_Detail;
    public static int StationIntelInt;
    public GameObject NextButton_Station;
    public GameObject PrevButton_Station;

    GameObject Detail_Restaurant1;
    GameObject Detail_Restaurant2;
    GameObject Detail_Restaurant3;
    GameObject Detail_Restaurant4;
    GameObject Detail_Restaurant5;
    GameObject Detail_Restaurant6;
    bool ON_Restaurant1;
    bool ON_Restaurant2;
    bool ON_Restaurant3;
    bool ON_Restaurant4;
    bool ON_Restaurant5;
    bool ON_Restaurant6;
    int Count_Restaurant = 0;
    public GameObject RestaurantContents;
    public GameObject Restaurant_Detail;
    public static int RestaurantIntelInt;
    public GameObject NextButton_Restaurant;
    public GameObject PrevButton_Restaurant;

    GameObject Detail_University1;
    GameObject Detail_University2;
    GameObject Detail_University3;
    GameObject Detail_University4;
    GameObject Detail_University5;
    GameObject Detail_University6;
    bool ON_University1;
    bool ON_University2;
    bool ON_University3;
    bool ON_University4;
    bool ON_University5;
    bool ON_University6;
    int Count_University = 0;
    public GameObject UniversityContents;
    public GameObject University_Detail;
    public static int UniversityIntelInt;
    public GameObject NextButton_University;
    public GameObject PrevButton_University;

    GameObject Detail_Riverside1;
    GameObject Detail_Riverside2;
    GameObject Detail_Riverside3;
    GameObject Detail_Riverside4;
    GameObject Detail_Riverside5;
    GameObject Detail_Riverside6;
    bool ON_Riverside1;
    bool ON_Riverside2;
    bool ON_Riverside3;
    bool ON_Riverside4;
    bool ON_Riverside5;
    bool ON_Riverside6;
    int Count_Riverside = 0;
    public GameObject RiversideContents;
    public GameObject Riverside_Detail;
    public static int RiversideIntelInt;
    public GameObject NextButton_Riverside;
    public GameObject PrevButton_Riverside;

    GameObject Detail_Bar1;
    GameObject Detail_Bar2;
    GameObject Detail_Bar3;
    GameObject Detail_Bar4;
    GameObject Detail_Bar5;
    GameObject Detail_Bar6;
    bool ON_Bar1;
    bool ON_Bar2;
    bool ON_Bar3;
    bool ON_Bar4;
    bool ON_Bar5;
    bool ON_Bar6;
    int Count_Bar = 0;
    public GameObject BarContents;
    public GameObject Bar_Detail;
    public static int BarIntelInt;
    public GameObject NextButton_Bar;
    public GameObject PrevButton_Bar;

    GameObject Detail_Slum1;
    GameObject Detail_Slum2;
    GameObject Detail_Slum3;
    GameObject Detail_Slum4;
    GameObject Detail_Slum5;
    GameObject Detail_Slum6;
    bool ON_Slum1;
    bool ON_Slum2;
    bool ON_Slum3;
    bool ON_Slum4;
    bool ON_Slum5;
    bool ON_Slum6;
    int Count_Slum = 0;
    public GameObject SlumContents;
    public GameObject Slum_Detail;
    public static int SlumIntelInt;
    public GameObject NextButton_Slum;
    public GameObject PrevButton_Slum;

    GameObject Detail_PoliceOffice1;
    GameObject Detail_PoliceOffice2;
    GameObject Detail_PoliceOffice3;
    GameObject Detail_PoliceOffice4;
    GameObject Detail_PoliceOffice5;
    GameObject Detail_PoliceOffice6;
    bool ON_PoliceOffice1;
    bool ON_PoliceOffice2;
    bool ON_PoliceOffice3;
    bool ON_PoliceOffice4;
    bool ON_PoliceOffice5;
    bool ON_PoliceOffice6;
    int Count_PoliceOffice = 0;
    public GameObject PoliceOfficeContents;
    public GameObject PoliceOffice_Detail;
    public static int PoliceOfficeIntelInt;
    public GameObject NextButton_PoliceOffice;
    public GameObject PrevButton_PoliceOffice;

    GameObject Detail_Gunsmith1;
    GameObject Detail_Gunsmith2;
    GameObject Detail_Gunsmith3;
    GameObject Detail_Gunsmith4;
    GameObject Detail_Gunsmith5;
    GameObject Detail_Gunsmith6;
    bool ON_Gunsmith1;
    bool ON_Gunsmith2;
    bool ON_Gunsmith3;
    bool ON_Gunsmith4;
    bool ON_Gunsmith5;
    bool ON_Gunsmith6;
    int Count_Gunsmith = 0;
    public GameObject GunsmithContents;
    public GameObject Gunsmith_Detail;
    public static int GunsmithIntelInt;
    public GameObject NextButton_Gunsmith;
    public GameObject PrevButton_Gunsmith;

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
    void Awake()
    {
        setGameObecject();
    }

    void setGameObecject()
    {
        Detail_DetectiveOffice1 = DetectiveOfficeContents.transform.GetChild(0).gameObject;
        Detail_DetectiveOffice2 = DetectiveOfficeContents.transform.GetChild(1).gameObject;
        Detail_DetectiveOffice3 = DetectiveOfficeContents.transform.GetChild(2).gameObject;
        Detail_DetectiveOffice4 = DetectiveOfficeContents.transform.GetChild(3).gameObject;
        Detail_DetectiveOffice5 = DetectiveOfficeContents.transform.GetChild(4).gameObject;
        Detail_DetectiveOffice6 = DetectiveOfficeContents.transform.GetChild(5).gameObject;

        Detail_Hospital1 = HospitalContents.transform.GetChild(0).gameObject;
        Detail_Hospital2 = HospitalContents.transform.GetChild(1).gameObject;
        Detail_Hospital3 = HospitalContents.transform.GetChild(2).gameObject;
        Detail_Hospital4 = HospitalContents.transform.GetChild(3).gameObject;
        Detail_Hospital5 = HospitalContents.transform.GetChild(4).gameObject;
        Detail_Hospital6 = HospitalContents.transform.GetChild(5).gameObject;

        Detail_Newspaper1 = NewspaperContents.transform.GetChild(0).gameObject;
        Detail_Newspaper2 = NewspaperContents.transform.GetChild(1).gameObject;
        Detail_Newspaper3 = NewspaperContents.transform.GetChild(2).gameObject;
        Detail_Newspaper4 = NewspaperContents.transform.GetChild(3).gameObject;
        Detail_Newspaper5 = NewspaperContents.transform.GetChild(4).gameObject;
        Detail_Newspaper6 = NewspaperContents.transform.GetChild(5).gameObject;

        Detail_QuestHouse1 = QuestHouseContents.transform.GetChild(0).gameObject;
        Detail_QuestHouse2 = QuestHouseContents.transform.GetChild(1).gameObject;
        Detail_QuestHouse3 = QuestHouseContents.transform.GetChild(2).gameObject;
        Detail_QuestHouse4 = QuestHouseContents.transform.GetChild(3).gameObject;
        Detail_QuestHouse5 = QuestHouseContents.transform.GetChild(4).gameObject;
        Detail_QuestHouse6 = QuestHouseContents.transform.GetChild(5).gameObject;

        Detail_Station1 = StationContents.transform.GetChild(0).gameObject;
        Detail_Station2 = StationContents.transform.GetChild(1).gameObject;
        Detail_Station3 = StationContents.transform.GetChild(2).gameObject;
        Detail_Station4 = StationContents.transform.GetChild(3).gameObject;
        Detail_Station5 = StationContents.transform.GetChild(4).gameObject;
        Detail_Station6 = StationContents.transform.GetChild(5).gameObject;

        Detail_Restaurant1 = RestaurantContents.transform.GetChild(0).gameObject;
        Detail_Restaurant2 = RestaurantContents.transform.GetChild(1).gameObject;
        Detail_Restaurant3 = RestaurantContents.transform.GetChild(2).gameObject;
        Detail_Restaurant4 = RestaurantContents.transform.GetChild(3).gameObject;
        Detail_Restaurant5 = RestaurantContents.transform.GetChild(4).gameObject;
        Detail_Restaurant6 = RestaurantContents.transform.GetChild(5).gameObject;

        Detail_University1 = UniversityContents.transform.GetChild(0).gameObject;
        Detail_University2 = UniversityContents.transform.GetChild(1).gameObject;
        Detail_University3 = UniversityContents.transform.GetChild(2).gameObject;
        Detail_University4 = UniversityContents.transform.GetChild(3).gameObject;
        Detail_University5 = UniversityContents.transform.GetChild(4).gameObject;
        Detail_University6 = UniversityContents.transform.GetChild(5).gameObject;

        Detail_Riverside1 = RiversideContents.transform.GetChild(0).gameObject;
        Detail_Riverside2 = RiversideContents.transform.GetChild(1).gameObject;
        Detail_Riverside3 = RiversideContents.transform.GetChild(2).gameObject;
        Detail_Riverside4 = RiversideContents.transform.GetChild(3).gameObject;
        Detail_Riverside5 = RiversideContents.transform.GetChild(4).gameObject;
        Detail_Riverside6 = RiversideContents.transform.GetChild(5).gameObject;

        Detail_Bar1 = BarContents.transform.GetChild(0).gameObject;
        Detail_Bar2 = BarContents.transform.GetChild(1).gameObject;
        Detail_Bar3 = BarContents.transform.GetChild(2).gameObject;
        Detail_Bar4 = BarContents.transform.GetChild(3).gameObject;
        Detail_Bar5 = BarContents.transform.GetChild(4).gameObject;
        Detail_Bar6 = BarContents.transform.GetChild(5).gameObject;

        Detail_Slum1 = SlumContents.transform.GetChild(0).gameObject;
        Detail_Slum2 = SlumContents.transform.GetChild(1).gameObject;
        Detail_Slum3 = SlumContents.transform.GetChild(2).gameObject;
        Detail_Slum4 = SlumContents.transform.GetChild(3).gameObject;
        Detail_Slum5 = SlumContents.transform.GetChild(4).gameObject;
        Detail_Slum6 = SlumContents.transform.GetChild(5).gameObject;

        Detail_PoliceOffice1 = PoliceOfficeContents.transform.GetChild(0).gameObject;
        Detail_PoliceOffice2 = PoliceOfficeContents.transform.GetChild(1).gameObject;
        Detail_PoliceOffice3 = PoliceOfficeContents.transform.GetChild(2).gameObject;
        Detail_PoliceOffice4 = PoliceOfficeContents.transform.GetChild(3).gameObject;
        Detail_PoliceOffice5 = PoliceOfficeContents.transform.GetChild(4).gameObject;
        Detail_PoliceOffice6 = PoliceOfficeContents.transform.GetChild(5).gameObject;

        Detail_Gunsmith1 = GunsmithContents.transform.GetChild(0).gameObject;
        Detail_Gunsmith2 = GunsmithContents.transform.GetChild(1).gameObject;
        Detail_Gunsmith3 = GunsmithContents.transform.GetChild(2).gameObject;
        Detail_Gunsmith4 = GunsmithContents.transform.GetChild(3).gameObject;
        Detail_Gunsmith5 = GunsmithContents.transform.GetChild(4).gameObject;
        Detail_Gunsmith6 = GunsmithContents.transform.GetChild(5).gameObject;

        Detail_SewageMaintenanceOffice1 = SewageMaintenanceOfficeContents.transform.GetChild(0).gameObject;
        Detail_SewageMaintenanceOffice2 = SewageMaintenanceOfficeContents.transform.GetChild(1).gameObject;
        Detail_SewageMaintenanceOffice3 = SewageMaintenanceOfficeContents.transform.GetChild(2).gameObject;
        Detail_SewageMaintenanceOffice4 = SewageMaintenanceOfficeContents.transform.GetChild(3).gameObject;
        Detail_SewageMaintenanceOffice5 = SewageMaintenanceOfficeContents.transform.GetChild(4).gameObject;
        Detail_SewageMaintenanceOffice6 = SewageMaintenanceOfficeContents.transform.GetChild(5).gameObject;

        Detail_Wharf1 = WharfContents.transform.GetChild(0).gameObject;
        Detail_Wharf2 = WharfContents.transform.GetChild(1).gameObject;
        Detail_Wharf3 = WharfContents.transform.GetChild(2).gameObject;
        Detail_Wharf4 = WharfContents.transform.GetChild(3).gameObject;
        Detail_Wharf5 = WharfContents.transform.GetChild(4).gameObject;
        Detail_Wharf6 = WharfContents.transform.GetChild(5).gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DetectiveOfficeDetail();
        HospitalDetail();
        NewspaperDetail();
        QuestHouseDetail();
        StationDetail();
        RestaurantDetail();
        UniversityDetail();
        RiversideDetail();
        BarDetail();
        SlumDetail();
        PoliceOfficeDetail();
        GunsmithDetail();
        SewageMaintenanceOfficeDetail();
        WharfDetail();
        if (DetectiveOffice_Detail.activeSelf == true)
        {
            NowPage = "DetectiveOffice";
        }
        if (Hospital_Detail.activeSelf == true)
        {
            NowPage = "Hospital";
        }
        if (Newspaper_Detail.activeSelf == true)
        {
            NowPage = "Newspaper";
        }
        if (QuestHouse_Detail.activeSelf == true)
        {
            NowPage = "QuestHouse";
        }
        if (Station_Detail.activeSelf == true)
        {
            NowPage = "Station";
        }
        if (Restaurant_Detail.activeSelf == true)
        {
            NowPage = "Restaurant";
        }
        if (University_Detail.activeSelf == true)
        {
            NowPage = "University";
        }
        if (Riverside_Detail.activeSelf == true)
        {
            NowPage = "Riverside";
        }
        if (Bar_Detail.activeSelf == true)
        {
            NowPage = "Bar";
        }
        if (Slum_Detail.activeSelf == true)
        {
            NowPage = "Slum";
        }
        if (PoliceOffice_Detail.activeSelf == true)
        {
            NowPage = "PoliceOffice";
        }
        if (Gunsmith_Detail.activeSelf == true)
        {
            NowPage = "Gunsmith";
        }
        if (SewageMaintenanceOffice_Detail.activeSelf == true)
        {
            NowPage = "SewageMaintenanceOffice";
        }
        if (Wharf_Detail.activeSelf == true)
        {
            NowPage = "Wharf";
        }
    }




    void DetectiveOfficeDetail()
    {
        if (ON_DetectiveOffice1 == false && DataBaseManager.Intel_DetectiveOffice1 == true)
        {
            ON_DetectiveOffice1 = true;
            Detail_DetectiveOffice1.SetActive(true);
            Detail_DetectiveOffice1.transform.SetAsLastSibling();
            Count_DetectiveOffice += 1;
        }
        if (ON_DetectiveOffice2 == false && DataBaseManager.Intel_DetectiveOffice2 == true)
        {
            ON_DetectiveOffice2 = true;
            Detail_DetectiveOffice2.SetActive(true);
            Detail_DetectiveOffice2.transform.SetAsLastSibling();
            Count_DetectiveOffice += 1;
        }
        if (ON_DetectiveOffice3 == false && DataBaseManager.Intel_DetectiveOffice3 == true)
        {
            ON_DetectiveOffice3 = true;
            Detail_DetectiveOffice3.SetActive(true);
            Detail_DetectiveOffice3.transform.SetAsLastSibling();
            Count_DetectiveOffice += 1;
        }
        if (ON_DetectiveOffice4 == false && DataBaseManager.Intel_DetectiveOffice4 == true)
        {
            ON_DetectiveOffice4 = true;
            Detail_DetectiveOffice4.SetActive(true);
            Detail_DetectiveOffice4.transform.SetAsLastSibling();
            Count_DetectiveOffice += 1;
        }
        if (ON_DetectiveOffice5 == false && DataBaseManager.Intel_DetectiveOffice5 == true)
        {
            ON_DetectiveOffice5 = true;
            Detail_DetectiveOffice5.SetActive(true);
            Detail_DetectiveOffice5.transform.SetAsLastSibling();
            Count_DetectiveOffice += 1;
        }
        if (ON_DetectiveOffice6 == false && DataBaseManager.Intel_DetectiveOffice6 == true)
        {
            ON_DetectiveOffice6 = true;
            Detail_DetectiveOffice6.SetActive(true);
            Detail_DetectiveOffice6.transform.SetAsLastSibling();
            Count_DetectiveOffice += 1;
        }

        if (Count_DetectiveOffice > 2 && DataBaseManager.NowPage_DetectiveOffice == 1)
        {
            NextButton_DetectiveOffice.SetActive(true);
        }
        else if (Count_DetectiveOffice > 4 && DataBaseManager.NowPage_DetectiveOffice == 2)
        {
            NextButton_DetectiveOffice.SetActive(true);
        }
        else
        {
            NextButton_DetectiveOffice.SetActive(false);
        }

        if (DataBaseManager.NowPage_DetectiveOffice == 2)
        {
            PrevButton_DetectiveOffice.SetActive(true);
        }
        else if (DataBaseManager.NowPage_DetectiveOffice == 3)
        {
            PrevButton_DetectiveOffice.SetActive(true);
        }
        else
        {
            PrevButton_DetectiveOffice.SetActive(false);
        }
    }
    void HospitalDetail()
    {
        if (ON_Hospital1 == false && DataBaseManager.Intel_Hospital1 == true)
        {
            ON_Hospital1 = true;
            Detail_Hospital1.SetActive(true);
            Detail_Hospital1.transform.SetAsLastSibling();
            Count_Hospital += 1;
        }
        if (ON_Hospital2 == false && DataBaseManager.Intel_Hospital2 == true)
        {
            ON_Hospital2 = true;
            Detail_Hospital2.SetActive(true);
            Detail_Hospital2.transform.SetAsLastSibling();
            Count_Hospital += 1;
        }
        if (ON_Hospital3 == false && DataBaseManager.Intel_Hospital3 == true)
        {
            ON_Hospital3 = true;
            Detail_Hospital3.SetActive(true);
            Detail_Hospital3.transform.SetAsLastSibling();
            Count_Hospital += 1;
        }
        if (ON_Hospital4 == false && DataBaseManager.Intel_Hospital4 == true)
        {
            ON_Hospital4 = true;
            Detail_Hospital4.SetActive(true);
            Detail_Hospital4.transform.SetAsLastSibling();
            Count_Hospital += 1;
        }
        if (ON_Hospital5 == false && DataBaseManager.Intel_Hospital5 == true)
        {
            ON_Hospital5 = true;
            Detail_Hospital5.SetActive(true);
            Detail_Hospital5.transform.SetAsLastSibling();
            Count_Hospital += 1;
        }
        if (ON_Hospital6 == false && DataBaseManager.Intel_Hospital6 == true)
        {
            ON_Hospital6 = true;
            Detail_Hospital6.SetActive(true);
            Detail_Hospital6.transform.SetAsLastSibling();
            Count_Hospital += 1;
        }

        if (Count_Hospital > 2 && DataBaseManager.NowPage_Hospital == 1)
        {
            NextButton_Hospital.SetActive(true);
        }
        else if (Count_Hospital > 4 && DataBaseManager.NowPage_Hospital == 2)
        {
            NextButton_Hospital.SetActive(true);
        }
        else
        {
            NextButton_Hospital.SetActive(false);
        }

        if (DataBaseManager.NowPage_Hospital == 2)
        {
            PrevButton_Hospital.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Hospital == 3)
        {
            PrevButton_Hospital.SetActive(true);
        }
        else
        {
            PrevButton_Hospital.SetActive(false);
        }
    }
    void NewspaperDetail()
    {
        if (ON_Newspaper1 == false && DataBaseManager.Intel_Newspaper1 == true)
        {
            ON_Newspaper1 = true;
            Detail_Newspaper1.SetActive(true);
            Detail_Newspaper1.transform.SetAsLastSibling();
            Count_Newspaper += 1;
        }
        if (ON_Newspaper2 == false && DataBaseManager.Intel_Newspaper2 == true)
        {
            ON_Newspaper2 = true;
            Detail_Newspaper2.SetActive(true);
            Detail_Newspaper2.transform.SetAsLastSibling();
            Count_Newspaper += 1;
        }
        if (ON_Newspaper3 == false && DataBaseManager.Intel_Newspaper3 == true)
        {
            ON_Newspaper3 = true;
            Detail_Newspaper3.SetActive(true);
            Detail_Newspaper3.transform.SetAsLastSibling();
            Count_Newspaper += 1;
        }
        if (ON_Newspaper4 == false && DataBaseManager.Intel_Newspaper4 == true)
        {
            ON_Newspaper4 = true;
            Detail_Newspaper4.SetActive(true);
            Detail_Newspaper4.transform.SetAsLastSibling();
            Count_Newspaper += 1;
        }
        if (ON_Newspaper5 == false && DataBaseManager.Intel_Newspaper5 == true)
        {
            ON_Newspaper5 = true;
            Detail_Newspaper5.SetActive(true);
            Detail_Newspaper5.transform.SetAsLastSibling();
            Count_Newspaper += 1;
        }
        if (ON_Newspaper6 == false && DataBaseManager.Intel_Newspaper6 == true)
        {
            ON_Newspaper6 = true;
            Detail_Newspaper6.SetActive(true);
            Detail_Newspaper6.transform.SetAsLastSibling();
            Count_Newspaper += 1;
        }

        if (Count_Newspaper > 2 && DataBaseManager.NowPage_Newspaper == 1)
        {
            NextButton_Newspaper.SetActive(true);
        }
        else if (Count_Newspaper > 4 && DataBaseManager.NowPage_Newspaper == 2)
        {
            NextButton_Newspaper.SetActive(true);
        }
        else
        {
            NextButton_Newspaper.SetActive(false);
        }

        if (DataBaseManager.NowPage_Newspaper == 2)
        {
            PrevButton_Newspaper.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Newspaper == 3)
        {
            PrevButton_Newspaper.SetActive(true);
        }
        else
        {
            PrevButton_Newspaper.SetActive(false);
        }
    }
    void QuestHouseDetail()
    {
        if (ON_QuestHouse1 == false && DataBaseManager.Intel_QuestHouse1 == true)
        {
            ON_QuestHouse1 = true;
            Detail_QuestHouse1.SetActive(true);
            Detail_QuestHouse1.transform.SetAsLastSibling();
            Count_QuestHouse += 1;
        }
        if (ON_QuestHouse2 == false && DataBaseManager.Intel_QuestHouse2 == true)
        {
            ON_QuestHouse2 = true;
            Detail_QuestHouse2.SetActive(true);
            Detail_QuestHouse2.transform.SetAsLastSibling();
            Count_QuestHouse += 1;
        }
        if (ON_QuestHouse3 == false && DataBaseManager.Intel_QuestHouse3 == true)
        {
            ON_QuestHouse3 = true;
            Detail_QuestHouse3.SetActive(true);
            Detail_QuestHouse3.transform.SetAsLastSibling();
            Count_QuestHouse += 1;
        }
        if (ON_QuestHouse4 == false && DataBaseManager.Intel_QuestHouse4 == true)
        {
            ON_QuestHouse4 = true;
            Detail_QuestHouse4.SetActive(true);
            Detail_QuestHouse4.transform.SetAsLastSibling();
            Count_QuestHouse += 1;
        }
        if (ON_QuestHouse5 == false && DataBaseManager.Intel_QuestHouse5 == true)
        {
            ON_QuestHouse5 = true;
            Detail_QuestHouse5.SetActive(true);
            Detail_QuestHouse5.transform.SetAsLastSibling();
            Count_QuestHouse += 1;
        }
        if (ON_QuestHouse6 == false && DataBaseManager.Intel_QuestHouse6 == true)
        {
            ON_QuestHouse6 = true;
            Detail_QuestHouse6.SetActive(true);
            Detail_QuestHouse6.transform.SetAsLastSibling();
            Count_QuestHouse += 1;
        }

        if (Count_QuestHouse > 2 && DataBaseManager.NowPage_QuestHouse == 1)
        {
            NextButton_QuestHouse.SetActive(true);
        }
        else if (Count_QuestHouse > 4 && DataBaseManager.NowPage_QuestHouse == 2)
        {
            NextButton_QuestHouse.SetActive(true);
        }
        else
        {
            NextButton_QuestHouse.SetActive(false);
        }

        if (DataBaseManager.NowPage_QuestHouse == 2)
        {
            PrevButton_QuestHouse.SetActive(true);
        }
        else if (DataBaseManager.NowPage_QuestHouse == 3)
        {
            PrevButton_QuestHouse.SetActive(true);
        }
        else
        {
            PrevButton_QuestHouse.SetActive(false);
        }
    }
    void StationDetail()
    {
        if (ON_Station1 == false && DataBaseManager.Intel_Station1 == true)
        {
            ON_Station1 = true;
            Detail_Station1.SetActive(true);
            Detail_Station1.transform.SetAsLastSibling();
            Count_Station += 1;
        }
        if (ON_Station2 == false && DataBaseManager.Intel_Station2 == true)
        {
            ON_Station2 = true;
            Detail_Station2.SetActive(true);
            Detail_Station2.transform.SetAsLastSibling();
            Count_Station += 1;
        }
        if (ON_Station3 == false && DataBaseManager.Intel_Station3 == true)
        {
            ON_Station3 = true;
            Detail_Station3.SetActive(true);
            Detail_Station3.transform.SetAsLastSibling();
            Count_Station += 1;
        }
        if (ON_Station4 == false && DataBaseManager.Intel_Station4 == true)
        {
            ON_Station4 = true;
            Detail_Station4.SetActive(true);
            Detail_Station4.transform.SetAsLastSibling();
            Count_Station += 1;
        }
        if (ON_Station5 == false && DataBaseManager.Intel_Station5 == true)
        {
            ON_Station5 = true;
            Detail_Station5.SetActive(true);
            Detail_Station5.transform.SetAsLastSibling();
            Count_Station += 1;
        }
        if (ON_Station6 == false && DataBaseManager.Intel_Station6 == true)
        {
            ON_Station6 = true;
            Detail_Station6.SetActive(true);
            Detail_Station6.transform.SetAsLastSibling();
            Count_Station += 1;
        }

        if (Count_Station > 2 && DataBaseManager.NowPage_Station == 1)
        {
            NextButton_Station.SetActive(true);
        }
        else if (Count_Station > 4 && DataBaseManager.NowPage_Station == 2)
        {
            NextButton_Station.SetActive(true);
        }
        else
        {
            NextButton_Station.SetActive(false);
        }

        if (DataBaseManager.NowPage_Station == 2)
        {
            PrevButton_Station.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Station == 3)
        {
            PrevButton_Station.SetActive(true);
        }
        else
        {
            PrevButton_Station.SetActive(false);
        }
    }
    void RestaurantDetail()
    {
        if (ON_Restaurant1 == false && DataBaseManager.Intel_Restaurant1 == true)
        {
            ON_Restaurant1 = true;
            Detail_Restaurant1.SetActive(true);
            Detail_Restaurant1.transform.SetAsLastSibling();
            Count_Restaurant += 1;
        }
        if (ON_Restaurant2 == false && DataBaseManager.Intel_Restaurant2 == true)
        {
            ON_Restaurant2 = true;
            Detail_Restaurant2.SetActive(true);
            Detail_Restaurant2.transform.SetAsLastSibling();
            Count_Restaurant += 1;
        }
        if (ON_Restaurant3 == false && DataBaseManager.Intel_Restaurant3 == true)
        {
            ON_Restaurant3 = true;
            Detail_Restaurant3.SetActive(true);
            Detail_Restaurant3.transform.SetAsLastSibling();
            Count_Restaurant += 1;
        }
        if (ON_Restaurant4 == false && DataBaseManager.Intel_Restaurant4 == true)
        {
            ON_Restaurant4 = true;
            Detail_Restaurant4.SetActive(true);
            Detail_Restaurant4.transform.SetAsLastSibling();
            Count_Restaurant += 1;
        }
        if (ON_Restaurant5 == false && DataBaseManager.Intel_Restaurant5 == true)
        {
            ON_Restaurant5 = true;
            Detail_Restaurant5.SetActive(true);
            Detail_Restaurant5.transform.SetAsLastSibling();
            Count_Restaurant += 1;
        }
        if (ON_Restaurant6 == false && DataBaseManager.Intel_Restaurant6 == true)
        {
            ON_Restaurant6 = true;
            Detail_Restaurant6.SetActive(true);
            Detail_Restaurant6.transform.SetAsLastSibling();
            Count_Restaurant += 1;
        }

        if (Count_Restaurant > 2 && DataBaseManager.NowPage_Restaurant == 1)
        {
            NextButton_Restaurant.SetActive(true);
        }
        else if (Count_Restaurant > 4 && DataBaseManager.NowPage_Restaurant == 2)
        {
            NextButton_Restaurant.SetActive(true);
        }
        else
        {
            NextButton_Restaurant.SetActive(false);
        }

        if (DataBaseManager.NowPage_Restaurant == 2)
        {
            PrevButton_Restaurant.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Restaurant
            == 3)
        {
            PrevButton_Restaurant.SetActive(true);
        }
        else
        {
            PrevButton_Restaurant.SetActive(false);
        }
    }
    void UniversityDetail()
    {
        if (ON_University1 == false && DataBaseManager.Intel_University1 == true)
        {
            ON_University1 = true;
            Detail_University1.SetActive(true);
            Detail_University1.transform.SetAsLastSibling();
            Count_University += 1;
        }
        if (ON_University2 == false && DataBaseManager.Intel_University2 == true)
        {
            ON_University2 = true;
            Detail_University2.SetActive(true);
            Detail_University2.transform.SetAsLastSibling();
            Count_University += 1;
        }
        if (ON_University3 == false && DataBaseManager.Intel_University3 == true)
        {
            ON_University3 = true;
            Detail_University3.SetActive(true);
            Detail_University3.transform.SetAsLastSibling();
            Count_University += 1;
        }
        if (ON_University4 == false && DataBaseManager.Intel_University4 == true)
        {
            ON_University4 = true;
            Detail_University4.SetActive(true);
            Detail_University4.transform.SetAsLastSibling();
            Count_University += 1;
        }
        if (ON_University5 == false && DataBaseManager.Intel_University5 == true)
        {
            ON_University5 = true;
            Detail_University5.SetActive(true);
            Detail_University5.transform.SetAsLastSibling();
            Count_University += 1;
        }
        if (ON_University6 == false && DataBaseManager.Intel_University6 == true)
        {
            ON_University6 = true;
            Detail_University6.SetActive(true);
            Detail_University6.transform.SetAsLastSibling();
            Count_University += 1;
        }

        if (Count_University > 2 && DataBaseManager.NowPage_University == 1)
        {
            NextButton_University.SetActive(true);
        }
        else if (Count_University > 4 && DataBaseManager.NowPage_University == 2)
        {
            NextButton_University.SetActive(true);
        }
        else
        {
            NextButton_University.SetActive(false);
        }

        if (DataBaseManager.NowPage_University == 2)
        {
            PrevButton_University.SetActive(true);
        }
        else if (DataBaseManager.NowPage_University
            == 3)
        {
            PrevButton_University.SetActive(true);
        }
        else
        {
            PrevButton_University.SetActive(false);
        }
    }
    void RiversideDetail()
    {
        if (ON_Riverside1 == false && DataBaseManager.Intel_Riverside1 == true)
        {
            ON_Riverside1 = true;
            Detail_Riverside1.SetActive(true);
            Detail_Riverside1.transform.SetAsLastSibling();
            Count_Riverside += 1;
        }
        if (ON_Riverside2 == false && DataBaseManager.Intel_Riverside2 == true)
        {
            ON_Riverside2 = true;
            Detail_Riverside2.SetActive(true);
            Detail_Riverside2.transform.SetAsLastSibling();
            Count_Riverside += 1;
        }
        if (ON_Riverside3 == false && DataBaseManager.Intel_Riverside3 == true)
        {
            ON_Riverside3 = true;
            Detail_Riverside3.SetActive(true);
            Detail_Riverside3.transform.SetAsLastSibling();
            Count_Riverside += 1;
        }
        if (ON_Riverside4 == false && DataBaseManager.Intel_Riverside4 == true)
        {
            ON_Riverside4 = true;
            Detail_Riverside4.SetActive(true);
            Detail_Riverside4.transform.SetAsLastSibling();
            Count_Riverside += 1;
        }
        if (ON_Riverside5 == false && DataBaseManager.Intel_Riverside5 == true)
        {
            ON_Riverside5 = true;
            Detail_Riverside5.SetActive(true);
            Detail_Riverside5.transform.SetAsLastSibling();
            Count_Riverside += 1;
        }
        if (ON_Riverside6 == false && DataBaseManager.Intel_Riverside6 == true)
        {
            ON_Riverside6 = true;
            Detail_Riverside6.SetActive(true);
            Detail_Riverside6.transform.SetAsLastSibling();
            Count_Riverside += 1;
        }

        if (Count_Riverside > 2 && DataBaseManager.NowPage_Riverside == 1)
        {
            NextButton_Riverside.SetActive(true);
        }
        else if (Count_Riverside > 4 && DataBaseManager.NowPage_Riverside == 2)
        {
            NextButton_Riverside.SetActive(true);
        }
        else
        {
            NextButton_Riverside.SetActive(false);
        }

        if (DataBaseManager.NowPage_Riverside == 2)
        {
            PrevButton_Riverside.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Riverside
            == 3)
        {
            PrevButton_Riverside.SetActive(true);
        }
        else
        {
            PrevButton_Riverside.SetActive(false);
        }
    }
    void BarDetail()
    {
        if (ON_Bar1 == false && DataBaseManager.Intel_Bar1 == true)
        {
            ON_Bar1 = true;
            Detail_Bar1.SetActive(true);
            Detail_Bar1.transform.SetAsLastSibling();
            Count_Bar += 1;
        }
        if (ON_Bar2 == false && DataBaseManager.Intel_Bar2 == true)
        {
            ON_Bar2 = true;
            Detail_Bar2.SetActive(true);
            Detail_Bar2.transform.SetAsLastSibling();
            Count_Bar += 1;
        }
        if (ON_Bar3 == false && DataBaseManager.Intel_Bar3 == true)
        {
            ON_Bar3 = true;
            Detail_Bar3.SetActive(true);
            Detail_Bar3.transform.SetAsLastSibling();
            Count_Bar += 1;
        }
        if (ON_Bar4 == false && DataBaseManager.Intel_Bar4 == true)
        {
            ON_Bar4 = true;
            Detail_Bar4.SetActive(true);
            Detail_Bar4.transform.SetAsLastSibling();
            Count_Bar += 1;
        }
        if (ON_Bar5 == false && DataBaseManager.Intel_Bar5 == true)
        {
            ON_Bar5 = true;
            Detail_Bar5.SetActive(true);
            Detail_Bar5.transform.SetAsLastSibling();
            Count_Bar += 1;
        }
        if (ON_Bar6 == false && DataBaseManager.Intel_Bar6 == true)
        {
            ON_Bar6 = true;
            Detail_Bar6.SetActive(true);
            Detail_Bar6.transform.SetAsLastSibling();
            Count_Bar += 1;
        }

        if (Count_Bar > 2 && DataBaseManager.NowPage_Bar == 1)
        {
            NextButton_Bar.SetActive(true);
        }
        else if (Count_Bar > 4 && DataBaseManager.NowPage_Bar == 2)
        {
            NextButton_Bar.SetActive(true);
        }
        else
        {
            NextButton_Bar.SetActive(false);
        }

        if (DataBaseManager.NowPage_Bar == 2)
        {
            PrevButton_Bar.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Bar == 3)
        {
            PrevButton_Bar.SetActive(true);
        }
        else
        {
            PrevButton_Bar.SetActive(false);
        }
    }
    void SlumDetail()
    {
        if (ON_Slum1 == false && DataBaseManager.Intel_Slum1 == true)
        {
            ON_Slum1 = true;
            Detail_Slum1.SetActive(true);
            Detail_Slum1.transform.SetAsLastSibling();
            Count_Slum += 1;
        }
        if (ON_Slum2 == false && DataBaseManager.Intel_Slum2 == true)
        {
            ON_Slum2 = true;
            Detail_Slum2.SetActive(true);
            Detail_Slum2.transform.SetAsLastSibling();
            Count_Slum += 1;
        }
        if (ON_Slum3 == false && DataBaseManager.Intel_Slum3 == true)
        {
            ON_Slum3 = true;
            Detail_Slum3.SetActive(true);
            Detail_Slum3.transform.SetAsLastSibling();
            Count_Slum += 1;
        }
        if (ON_Slum4 == false && DataBaseManager.Intel_Slum4 == true)
        {
            ON_Slum4 = true;
            Detail_Slum4.SetActive(true);
            Detail_Slum4.transform.SetAsLastSibling();
            Count_Slum += 1;
        }
        if (ON_Slum5 == false && DataBaseManager.Intel_Slum5 == true)
        {
            ON_Slum5 = true;
            Detail_Slum5.SetActive(true);
            Detail_Slum5.transform.SetAsLastSibling();
            Count_Slum += 1;
        }
        if (ON_Slum6 == false && DataBaseManager.Intel_Slum6 == true)
        {
            ON_Slum6 = true;
            Detail_Slum6.SetActive(true);
            Detail_Slum6.transform.SetAsLastSibling();
            Count_Slum += 1;
        }

        if (Count_Slum > 2 && DataBaseManager.NowPage_Slum == 1)
        {
            NextButton_Slum.SetActive(true);
        }
        else if (Count_Slum > 4 && DataBaseManager.NowPage_Slum == 2)
        {
            NextButton_Slum.SetActive(true);
        }
        else
        {
            NextButton_Slum.SetActive(false);
        }

        if (DataBaseManager.NowPage_Slum == 2)
        {
            PrevButton_Slum.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Slum == 3)
        {
            PrevButton_Slum.SetActive(true);
        }
        else
        {
            PrevButton_Slum.SetActive(false);
        }
    }
    void PoliceOfficeDetail()
    {
        if (ON_PoliceOffice1 == false && DataBaseManager.Intel_PoliceOffice1 == true)
        {
            ON_PoliceOffice1 = true;
            Detail_PoliceOffice1.SetActive(true);
            Detail_PoliceOffice1.transform.SetAsLastSibling();
            Count_PoliceOffice += 1;
        }
        if (ON_PoliceOffice2 == false && DataBaseManager.Intel_PoliceOffice2 == true)
        {
            ON_PoliceOffice2 = true;
            Detail_PoliceOffice2.SetActive(true);
            Detail_PoliceOffice2.transform.SetAsLastSibling();
            Count_PoliceOffice += 1;
        }
        if (ON_PoliceOffice3 == false && DataBaseManager.Intel_PoliceOffice3 == true)
        {
            ON_PoliceOffice3 = true;
            Detail_PoliceOffice3.SetActive(true);
            Detail_PoliceOffice3.transform.SetAsLastSibling();
            Count_PoliceOffice += 1;
        }
        if (ON_PoliceOffice4 == false && DataBaseManager.Intel_PoliceOffice4 == true)
        {
            ON_PoliceOffice4 = true;
            Detail_PoliceOffice4.SetActive(true);
            Detail_PoliceOffice4.transform.SetAsLastSibling();
            Count_PoliceOffice += 1;
        }
        if (ON_PoliceOffice5 == false && DataBaseManager.Intel_PoliceOffice5 == true)
        {
            ON_PoliceOffice5 = true;
            Detail_PoliceOffice5.SetActive(true);
            Detail_PoliceOffice5.transform.SetAsLastSibling();
            Count_PoliceOffice += 1;
        }
        if (ON_PoliceOffice6 == false && DataBaseManager.Intel_PoliceOffice6 == true)
        {
            ON_PoliceOffice6 = true;
            Detail_PoliceOffice6.SetActive(true);
            Detail_PoliceOffice6.transform.SetAsLastSibling();
            Count_PoliceOffice += 1;
        }

        if (Count_PoliceOffice > 2 && DataBaseManager.NowPage_PoliceOffice == 1)
        {
            NextButton_PoliceOffice.SetActive(true);
        }
        else if (Count_PoliceOffice > 4 && DataBaseManager.NowPage_PoliceOffice == 2)
        {
            NextButton_PoliceOffice.SetActive(true);
        }
        else
        {
            NextButton_PoliceOffice.SetActive(false);
        }

        if (DataBaseManager.NowPage_PoliceOffice == 2)
        {
            PrevButton_PoliceOffice.SetActive(true);
        }
        else if (DataBaseManager.NowPage_PoliceOffice == 3)
        {
            PrevButton_PoliceOffice.SetActive(true);
        }
        else
        {
            PrevButton_PoliceOffice.SetActive(false);
        }
    }
    void GunsmithDetail()
    {
        if (ON_Gunsmith1 == false && DataBaseManager.Intel_Gunsmith1 == true)
        {
            ON_Gunsmith1 = true;
            Detail_Gunsmith1.SetActive(true);
            Detail_Gunsmith1.transform.SetAsLastSibling();
            Count_Gunsmith += 1;
        }
        if (ON_Gunsmith2 == false && DataBaseManager.Intel_Gunsmith2 == true)
        {
            ON_Gunsmith2 = true;
            Detail_Gunsmith2.SetActive(true);
            Detail_Gunsmith2.transform.SetAsLastSibling();
            Count_Gunsmith += 1;
        }
        if (ON_Gunsmith3 == false && DataBaseManager.Intel_Gunsmith3 == true)
        {
            ON_Gunsmith3 = true;
            Detail_Gunsmith3.SetActive(true);
            Detail_Gunsmith3.transform.SetAsLastSibling();
            Count_Gunsmith += 1;
        }
        if (ON_Gunsmith4 == false && DataBaseManager.Intel_Gunsmith4 == true)
        {
            ON_Gunsmith4 = true;
            Detail_Gunsmith4.SetActive(true);
            Detail_Gunsmith4.transform.SetAsLastSibling();
            Count_Gunsmith += 1;
        }
        if (ON_Gunsmith5 == false && DataBaseManager.Intel_Gunsmith5 == true)
        {
            ON_Gunsmith5 = true;
            Detail_Gunsmith5.SetActive(true);
            Detail_Gunsmith5.transform.SetAsLastSibling();
            Count_Gunsmith += 1;
        }
        if (ON_Gunsmith6 == false && DataBaseManager.Intel_Gunsmith6 == true)
        {
            ON_Gunsmith6 = true;
            Detail_Gunsmith6.SetActive(true);
            Detail_Gunsmith6.transform.SetAsLastSibling();
            Count_Gunsmith += 1;
        }

        if (Count_Gunsmith > 2 && DataBaseManager.NowPage_Gunsmith == 1)
        {
            NextButton_Gunsmith.SetActive(true);
        }
        else if (Count_Gunsmith > 4 && DataBaseManager.NowPage_Gunsmith == 2)
        {
            NextButton_Gunsmith.SetActive(true);
        }
        else
        {
            NextButton_Gunsmith.SetActive(false);
        }

        if (DataBaseManager.NowPage_Gunsmith == 2)
        {
            PrevButton_Gunsmith.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Gunsmith == 3)
        {
            PrevButton_Gunsmith.SetActive(true);
        }
        else
        {
            PrevButton_Gunsmith.SetActive(false);
        }
    }
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
        if (NowPage == "DetectiveOffice")
        {
            if (DataBaseManager.NowPage_DetectiveOffice == 1)
            {
                if (Count_DetectiveOffice >= 6)
                {
                    Reset_DetectiveOffice();
                    DetectiveOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    DetectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_DetectiveOffice >= 5)
                {
                    Reset_DetectiveOffice();
                    DetectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    DetectiveOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_DetectiveOffice >= 4)
                {
                    Reset_DetectiveOffice();
                    DetectiveOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                    DetectiveOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_DetectiveOffice >= 3)
                {
                    Reset_DetectiveOffice();
                    DetectiveOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_DetectiveOffice = 2;
            }
            else if (DataBaseManager.NowPage_DetectiveOffice == 2)
            {
                if (Count_DetectiveOffice >= 6)
                {
                    Reset_DetectiveOffice();
                    DetectiveOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                    DetectiveOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_DetectiveOffice >= 5)
                {
                    Reset_DetectiveOffice();
                    DetectiveOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_DetectiveOffice = 3;
            }
        }
        if (NowPage == "Hospital")
        {
            if (DataBaseManager.NowPage_Hospital == 1)
            {
                if (Count_Hospital >= 6)
                {
                    Reset_Hospital();
                    HospitalContents.transform.GetChild(2).gameObject.SetActive(true);
                    HospitalContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Hospital >= 5)
                {
                    Reset_Hospital();
                    HospitalContents.transform.GetChild(3).gameObject.SetActive(true);
                    HospitalContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Hospital >= 4)
                {
                    Reset_Hospital();
                    HospitalContents.transform.GetChild(4).gameObject.SetActive(true);
                    HospitalContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Hospital >= 3)
                {
                    Reset_Hospital();
                    HospitalContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Hospital = 2;
            }
            else if (DataBaseManager.NowPage_Hospital == 2)
            {
                if (Count_Hospital >= 6)
                {
                    Reset_Hospital();
                    HospitalContents.transform.GetChild(4).gameObject.SetActive(true);
                    HospitalContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Hospital >= 5)
                {
                    Reset_Hospital();
                    HospitalContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Hospital = 3;
            }
        }
        if (NowPage == "Newspaper")
        {
            if (DataBaseManager.NowPage_Newspaper == 1)
            {
                if (Count_Newspaper >= 6)
                {
                    Reset_Newspaper();
                    NewspaperContents.transform.GetChild(2).gameObject.SetActive(true);
                    NewspaperContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Newspaper >= 5)
                {
                    Reset_Newspaper();
                    NewspaperContents.transform.GetChild(3).gameObject.SetActive(true);
                    NewspaperContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Newspaper >= 4)
                {
                    Reset_Newspaper();
                    NewspaperContents.transform.GetChild(4).gameObject.SetActive(true);
                    NewspaperContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Newspaper >= 3)
                {
                    Reset_Newspaper();
                    NewspaperContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Newspaper = 2;
            }
            else if (DataBaseManager.NowPage_Newspaper == 2)
            {
                if (Count_Newspaper >= 6)
                {
                    Reset_Newspaper();
                    NewspaperContents.transform.GetChild(4).gameObject.SetActive(true);
                    NewspaperContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Newspaper >= 5)
                {
                    Reset_Newspaper();
                    NewspaperContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Newspaper = 3;
            }
        }
        if (NowPage == "QuestHouse")
        {
            if (DataBaseManager.NowPage_QuestHouse == 1)
            {
                if (Count_QuestHouse >= 6)
                {
                    Reset_QuestHouse();
                    QuestHouseContents.transform.GetChild(2).gameObject.SetActive(true);
                    QuestHouseContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_QuestHouse >= 5)
                {
                    Reset_QuestHouse();
                    QuestHouseContents.transform.GetChild(3).gameObject.SetActive(true);
                    QuestHouseContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_QuestHouse >= 4)
                {
                    Reset_QuestHouse();
                    QuestHouseContents.transform.GetChild(4).gameObject.SetActive(true);
                    QuestHouseContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_QuestHouse >= 3)
                {
                    Reset_QuestHouse();
                    QuestHouseContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_QuestHouse = 2;
            }
            else if (DataBaseManager.NowPage_QuestHouse == 2)
            {
                if (Count_QuestHouse >= 6)
                {
                    Reset_QuestHouse();
                    QuestHouseContents.transform.GetChild(4).gameObject.SetActive(true);
                    QuestHouseContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_QuestHouse >= 5)
                {
                    Reset_QuestHouse();
                    QuestHouseContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_QuestHouse = 3;
            }
        }
        if (NowPage == "Station")
        {
            if (DataBaseManager.NowPage_Station == 1)
            {
                if (Count_Station >= 6)
                {
                    Reset_Station();
                    StationContents.transform.GetChild(2).gameObject.SetActive(true);
                    StationContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Station >= 5)
                {
                    Reset_Station();
                    StationContents.transform.GetChild(3).gameObject.SetActive(true);
                    StationContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Station >= 4)
                {
                    Reset_Station();
                    StationContents.transform.GetChild(4).gameObject.SetActive(true);
                    StationContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Station >= 3)
                {
                    Reset_Station();
                    StationContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Station = 2;
            }
            else if (DataBaseManager.NowPage_Station == 2)
            {
                if (Count_Station >= 6)
                {
                    Reset_Station();
                    StationContents.transform.GetChild(4).gameObject.SetActive(true);
                    StationContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Station >= 5)
                {
                    Reset_Station();
                    StationContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Station = 3;
            }
        }
        if (NowPage == "Restaurant")
        {
            if (DataBaseManager.NowPage_Restaurant == 1)
            {
                if (Count_Restaurant >= 6)
                {
                    Reset_Restaurant();
                    RestaurantContents.transform.GetChild(2).gameObject.SetActive(true);
                    RestaurantContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Restaurant >= 5)
                {
                    Reset_Restaurant();
                    RestaurantContents.transform.GetChild(3).gameObject.SetActive(true);
                    RestaurantContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Restaurant >= 4)
                {
                    Reset_Restaurant();
                    RestaurantContents.transform.GetChild(4).gameObject.SetActive(true);
                    RestaurantContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Restaurant >= 3)
                {
                    Reset_Restaurant();
                    RestaurantContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Restaurant = 2;
            }
            else if (DataBaseManager.NowPage_Restaurant == 2)
            {
                if (Count_Restaurant >= 6)
                {
                    Reset_Restaurant();
                    RestaurantContents.transform.GetChild(4).gameObject.SetActive(true);
                    RestaurantContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Restaurant >= 5)
                {
                    Reset_Restaurant();
                    RestaurantContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Restaurant = 3;
            }
        }
        if (NowPage == "University")
        {
            if (DataBaseManager.NowPage_University == 1)
            {
                if (Count_University >= 6)
                {
                    Reset_University();
                    UniversityContents.transform.GetChild(2).gameObject.SetActive(true);
                    UniversityContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_University >= 5)
                {
                    Reset_University();
                    UniversityContents.transform.GetChild(3).gameObject.SetActive(true);
                    UniversityContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_University >= 4)
                {
                    Reset_University();
                    UniversityContents.transform.GetChild(4).gameObject.SetActive(true);
                    UniversityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_University >= 3)
                {
                    Reset_University();
                    UniversityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_University = 2;
            }
            else if (DataBaseManager.NowPage_University == 2)
            {
                if (Count_University >= 6)
                {
                    Reset_University();
                    UniversityContents.transform.GetChild(4).gameObject.SetActive(true);
                    UniversityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_University >= 5)
                {
                    Reset_University();
                    UniversityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_University = 3;
            }
        }
        if (NowPage == "Riverside")
        {
            if (DataBaseManager.NowPage_Riverside == 1)
            {
                if (Count_Riverside >= 6)
                {
                    Reset_Riverside();
                    RiversideContents.transform.GetChild(2).gameObject.SetActive(true);
                    RiversideContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 5)
                {
                    Reset_Riverside();
                    RiversideContents.transform.GetChild(3).gameObject.SetActive(true);
                    RiversideContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 4)
                {
                    Reset_Riverside();
                    RiversideContents.transform.GetChild(4).gameObject.SetActive(true);
                    RiversideContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 3)
                {
                    Reset_Riverside();
                    RiversideContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Riverside = 2;
            }
            else if (DataBaseManager.NowPage_Riverside == 2)
            {
                if (Count_Riverside >= 6)
                {
                    Reset_Riverside();
                    RiversideContents.transform.GetChild(4).gameObject.SetActive(true);
                    RiversideContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 5)
                {
                    Reset_Riverside();
                    RiversideContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Riverside = 3;
            }
        }
        if (NowPage == "Bar")
        {
            if (DataBaseManager.NowPage_Bar == 1)
            {
                if (Count_Bar >= 6)
                {
                    Reset_Bar();
                    BarContents.transform.GetChild(2).gameObject.SetActive(true);
                    BarContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Bar >= 5)
                {
                    Reset_Bar();
                    BarContents.transform.GetChild(3).gameObject.SetActive(true);
                    BarContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Bar >= 4)
                {
                    Reset_Bar();
                    BarContents.transform.GetChild(4).gameObject.SetActive(true);
                    BarContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Bar >= 3)
                {
                    Reset_Bar();
                    BarContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Bar = 2;
            }
            else if (DataBaseManager.NowPage_Bar == 2)
            {
                if (Count_Bar >= 6)
                {
                    Reset_Bar();
                    BarContents.transform.GetChild(4).gameObject.SetActive(true);
                    BarContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Bar >= 5)
                {
                    Reset_Bar();
                    BarContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Bar = 3;
            }
        }
        if (NowPage == "Slum")
        {
            if (DataBaseManager.NowPage_Slum == 1)
            {
                if (Count_Slum >= 6)
                {
                    Reset_Slum();
                    SlumContents.transform.GetChild(2).gameObject.SetActive(true);
                    SlumContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Slum >= 5)
                {
                    Reset_Slum();
                    SlumContents.transform.GetChild(3).gameObject.SetActive(true);
                    SlumContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Slum >= 4)
                {
                    Reset_Slum();
                    SlumContents.transform.GetChild(4).gameObject.SetActive(true);
                    SlumContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Slum >= 3)
                {
                    Reset_Slum();
                    SlumContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Slum = 2;
            }
            else if (DataBaseManager.NowPage_Slum == 2)
            {
                if (Count_Slum >= 6)
                {
                    Reset_Slum();
                    SlumContents.transform.GetChild(4).gameObject.SetActive(true);
                    SlumContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Slum >= 5)
                {
                    Reset_Slum();
                    SlumContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Slum = 3;
            }
        }
        if (NowPage == "PoliceOffice")
        {
            if (DataBaseManager.NowPage_PoliceOffice == 1)
            {
                if (Count_PoliceOffice >= 6)
                {
                    Reset_PoliceOffice();
                    PoliceOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    PoliceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_PoliceOffice >= 5)
                {
                    Reset_PoliceOffice();
                    PoliceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    PoliceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_PoliceOffice >= 4)
                {
                    Reset_PoliceOffice();
                    PoliceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                    PoliceOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_PoliceOffice >= 3)
                {
                    Reset_PoliceOffice();
                    PoliceOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_PoliceOffice = 2;
            }
            else if (DataBaseManager.NowPage_PoliceOffice == 2)
            {
                if (Count_PoliceOffice >= 6)
                {
                    Reset_PoliceOffice();
                    PoliceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                    PoliceOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_PoliceOffice >= 5)
                {
                    Reset_PoliceOffice();
                    PoliceOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_PoliceOffice = 3;
            }
        }
        if (NowPage == "Gunsmith")
        {
            if (DataBaseManager.NowPage_Gunsmith == 1)
            {
                if (Count_Gunsmith >= 6)
                {
                    Reset_Gunsmith();
                    GunsmithContents.transform.GetChild(2).gameObject.SetActive(true);
                    GunsmithContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Gunsmith >= 5)
                {
                    Reset_Gunsmith();
                    GunsmithContents.transform.GetChild(3).gameObject.SetActive(true);
                    GunsmithContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Gunsmith >= 4)
                {
                    Reset_Gunsmith();
                    GunsmithContents.transform.GetChild(4).gameObject.SetActive(true);
                    GunsmithContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Gunsmith >= 3)
                {
                    Reset_Gunsmith();
                    GunsmithContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Gunsmith = 2;
            }
            else if (DataBaseManager.NowPage_Gunsmith == 2)
            {
                if (Count_Gunsmith >= 6)
                {
                    Reset_Gunsmith();
                    GunsmithContents.transform.GetChild(4).gameObject.SetActive(true);
                    GunsmithContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Gunsmith >= 5)
                {
                    Reset_Gunsmith();
                    GunsmithContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Gunsmith = 3;
            }
        }
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
        if (NowPage == "DetectiveOffice")
        {
            if (DataBaseManager.NowPage_DetectiveOffice == 2)
            {
                if (Count_DetectiveOffice >= 6)
                {
                    Reset_DetectiveOffice();
                    DetectiveOfficeContents.transform.GetChild(0).gameObject.SetActive(true);
                    DetectiveOfficeContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_DetectiveOffice >= 5)
                {
                    Reset_DetectiveOffice();
                    DetectiveOfficeContents.transform.GetChild(1).gameObject.SetActive(true);
                    DetectiveOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_DetectiveOffice >= 4)
                {
                    Reset_DetectiveOffice();
                    DetectiveOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    DetectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_DetectiveOffice >= 3)
                {
                    Reset_DetectiveOffice();
                    DetectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    DetectiveOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_DetectiveOffice = 1;
            }
            else if (DataBaseManager.NowPage_DetectiveOffice == 3)
            {
                if (Count_DetectiveOffice >= 6)
                {
                    Reset_DetectiveOffice();
                    DetectiveOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    DetectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_DetectiveOffice >= 5)
                {
                    Reset_DetectiveOffice();
                    DetectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    DetectiveOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_DetectiveOffice = 2;
            }
        }
        if (NowPage == "Hospital")
        {
            if (DataBaseManager.NowPage_Hospital == 2)
            {
                if (Count_Hospital >= 6)
                {
                    Reset_Hospital();
                    HospitalContents.transform.GetChild(0).gameObject.SetActive(true);
                    HospitalContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Hospital >= 5)
                {
                    Reset_Hospital();
                    HospitalContents.transform.GetChild(1).gameObject.SetActive(true);
                    HospitalContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Hospital >= 4)
                {
                    Reset_Hospital();
                    HospitalContents.transform.GetChild(2).gameObject.SetActive(true);
                    HospitalContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Hospital >= 3)
                {
                    Reset_Hospital();
                    HospitalContents.transform.GetChild(3).gameObject.SetActive(true);
                    HospitalContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Hospital = 1;
            }
            else if (DataBaseManager.NowPage_Hospital == 3)
            {
                if (Count_Hospital >= 6)
                {
                    Reset_Hospital();
                    HospitalContents.transform.GetChild(2).gameObject.SetActive(true);
                    HospitalContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Hospital >= 5)
                {
                    Reset_Hospital();
                    HospitalContents.transform.GetChild(3).gameObject.SetActive(true);
                    HospitalContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Hospital = 2;
            }
        }
        if (NowPage == "Newspaper")
        {
            if (DataBaseManager.NowPage_Newspaper == 2)
            {
                if (Count_Newspaper >= 6)
                {
                    Reset_Newspaper();
                    NewspaperContents.transform.GetChild(0).gameObject.SetActive(true);
                    NewspaperContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Newspaper >= 5)
                {
                    Reset_Newspaper();
                    NewspaperContents.transform.GetChild(1).gameObject.SetActive(true);
                    NewspaperContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Newspaper >= 4)
                {
                    Reset_Newspaper();
                    NewspaperContents.transform.GetChild(2).gameObject.SetActive(true);
                    NewspaperContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Newspaper >= 3)
                {
                    Reset_Newspaper();
                    NewspaperContents.transform.GetChild(3).gameObject.SetActive(true);
                    NewspaperContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Newspaper = 1;
            }
            else if (DataBaseManager.NowPage_Newspaper == 3)
            {
                if (Count_Newspaper >= 6)
                {
                    Reset_Newspaper();
                    NewspaperContents.transform.GetChild(2).gameObject.SetActive(true);
                    NewspaperContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Newspaper >= 5)
                {
                    Reset_Newspaper();
                    NewspaperContents.transform.GetChild(3).gameObject.SetActive(true);
                    NewspaperContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Newspaper = 2;
            }
        }
        if (NowPage == "QuestHouse")
        {
            if (DataBaseManager.NowPage_QuestHouse == 2)
            {
                if (Count_QuestHouse >= 6)
                {
                    Reset_QuestHouse();
                    QuestHouseContents.transform.GetChild(0).gameObject.SetActive(true);
                    QuestHouseContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_QuestHouse >= 5)
                {
                    Reset_QuestHouse();
                    QuestHouseContents.transform.GetChild(1).gameObject.SetActive(true);
                    QuestHouseContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_QuestHouse >= 4)
                {
                    Reset_QuestHouse();
                    QuestHouseContents.transform.GetChild(2).gameObject.SetActive(true);
                    QuestHouseContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_QuestHouse >= 3)
                {
                    Reset_QuestHouse();
                    QuestHouseContents.transform.GetChild(3).gameObject.SetActive(true);
                    QuestHouseContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_QuestHouse = 1;
            }
            else if (DataBaseManager.NowPage_QuestHouse == 3)
            {
                if (Count_QuestHouse >= 6)
                {
                    Reset_QuestHouse();
                    QuestHouseContents.transform.GetChild(2).gameObject.SetActive(true);
                    QuestHouseContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_QuestHouse >= 5)
                {
                    Reset_QuestHouse();
                    QuestHouseContents.transform.GetChild(3).gameObject.SetActive(true);
                    QuestHouseContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_QuestHouse = 2;
            }
        }
        if (NowPage == "Station")
        {
            if (DataBaseManager.NowPage_Station == 2)
            {
                if (Count_Station >= 6)
                {
                    Reset_Station();
                    StationContents.transform.GetChild(0).gameObject.SetActive(true);
                    StationContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Station >= 5)
                {
                    Reset_Station();
                    StationContents.transform.GetChild(1).gameObject.SetActive(true);
                    StationContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Station >= 4)
                {
                    Reset_Station();
                    StationContents.transform.GetChild(2).gameObject.SetActive(true);
                    StationContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Station >= 3)
                {
                    Reset_Station();
                    StationContents.transform.GetChild(3).gameObject.SetActive(true);
                    StationContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Station = 1;
            }
            else if (DataBaseManager.NowPage_Station == 3)
            {
                if (Count_Station >= 6)
                {
                    Reset_Station();
                    StationContents.transform.GetChild(2).gameObject.SetActive(true);
                    StationContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Station >= 5)
                {
                    Reset_Station();
                    StationContents.transform.GetChild(3).gameObject.SetActive(true);
                    StationContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Station = 2;
            }
        }
        if (NowPage == "Restaurant")
        {
            if (DataBaseManager.NowPage_Restaurant == 2)
            {
                if (Count_Restaurant >= 6)
                {
                    Reset_Restaurant();
                    RestaurantContents.transform.GetChild(0).gameObject.SetActive(true);
                    RestaurantContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Restaurant >= 5)
                {
                    Reset_Restaurant();
                    RestaurantContents.transform.GetChild(1).gameObject.SetActive(true);
                    RestaurantContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Restaurant >= 4)
                {
                    Reset_Restaurant();
                    RestaurantContents.transform.GetChild(2).gameObject.SetActive(true);
                    RestaurantContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Restaurant >= 3)
                {
                    Reset_Restaurant();
                    RestaurantContents.transform.GetChild(3).gameObject.SetActive(true);
                    RestaurantContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Restaurant = 1;
            }
            else if (DataBaseManager.NowPage_Restaurant == 3)
            {
                if (Count_Restaurant >= 6)
                {
                    Reset_Restaurant();
                    RestaurantContents.transform.GetChild(2).gameObject.SetActive(true);
                    RestaurantContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Restaurant >= 5)
                {
                    Reset_Restaurant();
                    RestaurantContents.transform.GetChild(3).gameObject.SetActive(true);
                    RestaurantContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Restaurant = 2;
            }
        }
        if (NowPage == "University")
        {
            if (DataBaseManager.NowPage_University == 2)
            {
                if (Count_University >= 6)
                {
                    Reset_University();
                    UniversityContents.transform.GetChild(0).gameObject.SetActive(true);
                    UniversityContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_University >= 5)
                {
                    Reset_University();
                    UniversityContents.transform.GetChild(1).gameObject.SetActive(true);
                    UniversityContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_University >= 4)
                {
                    Reset_University();
                    UniversityContents.transform.GetChild(2).gameObject.SetActive(true);
                    UniversityContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_University >= 3)
                {
                    Reset_University();
                    UniversityContents.transform.GetChild(3).gameObject.SetActive(true);
                    UniversityContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_University = 1;
            }
            else if (DataBaseManager.NowPage_University == 3)
            {
                if (Count_University >= 6)
                {
                    Reset_University();
                    UniversityContents.transform.GetChild(2).gameObject.SetActive(true);
                    UniversityContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_University >= 5)
                {
                    Reset_University();
                    UniversityContents.transform.GetChild(3).gameObject.SetActive(true);
                    UniversityContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_University = 2;
            }
        }
        if (NowPage == "Riverside")
        {
            if (DataBaseManager.NowPage_Riverside == 2)
            {
                if (Count_Riverside >= 6)
                {
                    Reset_Riverside();
                    RiversideContents.transform.GetChild(0).gameObject.SetActive(true);
                    RiversideContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 5)
                {
                    Reset_Riverside();
                    RiversideContents.transform.GetChild(1).gameObject.SetActive(true);
                    RiversideContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 4)
                {
                    Reset_Riverside();
                    RiversideContents.transform.GetChild(2).gameObject.SetActive(true);
                    RiversideContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 3)
                {
                    Reset_Riverside();
                    RiversideContents.transform.GetChild(3).gameObject.SetActive(true);
                    RiversideContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Riverside = 1;
            }
            else if (DataBaseManager.NowPage_Riverside == 3)
            {
                if (Count_Riverside >= 6)
                {
                    Reset_Riverside();
                    RiversideContents.transform.GetChild(2).gameObject.SetActive(true);
                    RiversideContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 5)
                {
                    Reset_Riverside();
                    RiversideContents.transform.GetChild(3).gameObject.SetActive(true);
                    RiversideContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Riverside = 2;
            }
        }
        if (NowPage == "Bar")
        {
            if (DataBaseManager.NowPage_Bar == 2)
            {
                if (Count_Bar >= 6)
                {
                    Reset_Bar();
                    BarContents.transform.GetChild(0).gameObject.SetActive(true);
                    BarContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Bar >= 5)
                {
                    Reset_Bar();
                    BarContents.transform.GetChild(1).gameObject.SetActive(true);
                    BarContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Bar >= 4)
                {
                    Reset_Bar();
                    BarContents.transform.GetChild(2).gameObject.SetActive(true);
                    BarContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Bar >= 3)
                {
                    Reset_Bar();
                    BarContents.transform.GetChild(3).gameObject.SetActive(true);
                    BarContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Bar = 1;
            }
            else if (DataBaseManager.NowPage_Bar == 3)
            {
                if (Count_Bar >= 6)
                {
                    Reset_Bar();
                    BarContents.transform.GetChild(2).gameObject.SetActive(true);
                    BarContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Bar >= 5)
                {
                    Reset_Bar();
                    BarContents.transform.GetChild(3).gameObject.SetActive(true);
                    BarContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Bar = 2;
            }
        }
        if (NowPage == "Slum")
        {
            if (DataBaseManager.NowPage_Slum == 2)
            {
                if (Count_Slum >= 6)
                {
                    Reset_Slum();
                    SlumContents.transform.GetChild(0).gameObject.SetActive(true);
                    SlumContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Slum >= 5)
                {
                    Reset_Slum();
                    SlumContents.transform.GetChild(1).gameObject.SetActive(true);
                    SlumContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Slum >= 4)
                {
                    Reset_Slum();
                    SlumContents.transform.GetChild(2).gameObject.SetActive(true);
                    SlumContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Slum >= 3)
                {
                    Reset_Slum();
                    SlumContents.transform.GetChild(3).gameObject.SetActive(true);
                    SlumContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Slum = 1;
            }
            else if (DataBaseManager.NowPage_Slum == 3)
            {
                if (Count_Slum >= 6)
                {
                    Reset_Slum();
                    SlumContents.transform.GetChild(2).gameObject.SetActive(true);
                    SlumContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Slum >= 5)
                {
                    Reset_Slum();
                    SlumContents.transform.GetChild(3).gameObject.SetActive(true);
                    SlumContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Slum = 2;
            }
        }
        if (NowPage == "PoliceOffice")
        {
            if (DataBaseManager.NowPage_PoliceOffice == 2)
            {
                if (Count_PoliceOffice >= 6)
                {
                    Reset_PoliceOffice();
                    PoliceOfficeContents.transform.GetChild(0).gameObject.SetActive(true);
                    PoliceOfficeContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_PoliceOffice >= 5)
                {
                    Reset_PoliceOffice();
                    PoliceOfficeContents.transform.GetChild(1).gameObject.SetActive(true);
                    PoliceOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_PoliceOffice >= 4)
                {
                    Reset_PoliceOffice();
                    PoliceOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    PoliceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_PoliceOffice >= 3)
                {
                    Reset_PoliceOffice();
                    PoliceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    PoliceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_PoliceOffice = 1;
            }
            else if (DataBaseManager.NowPage_PoliceOffice == 3)
            {
                if (Count_PoliceOffice >= 6)
                {
                    Reset_PoliceOffice();
                    PoliceOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    PoliceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_PoliceOffice >= 5)
                {
                    Reset_PoliceOffice();
                    PoliceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    PoliceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_PoliceOffice = 2;
            }
        }
        if (NowPage == "Gunsmith")
        {
            if (DataBaseManager.NowPage_Gunsmith == 2)
            {
                if (Count_Gunsmith >= 6)
                {
                    Reset_Gunsmith();
                    GunsmithContents.transform.GetChild(0).gameObject.SetActive(true);
                    GunsmithContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Gunsmith >= 5)
                {
                    Reset_Gunsmith();
                    GunsmithContents.transform.GetChild(1).gameObject.SetActive(true);
                    GunsmithContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Gunsmith >= 4)
                {
                    Reset_Gunsmith();
                    GunsmithContents.transform.GetChild(2).gameObject.SetActive(true);
                    GunsmithContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Gunsmith >= 3)
                {
                    Reset_Gunsmith();
                    GunsmithContents.transform.GetChild(3).gameObject.SetActive(true);
                    GunsmithContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Gunsmith = 1;
            }
            else if (DataBaseManager.NowPage_Gunsmith == 3)
            {
                if (Count_Gunsmith >= 6)
                {
                    Reset_Gunsmith();
                    GunsmithContents.transform.GetChild(2).gameObject.SetActive(true);
                    GunsmithContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Gunsmith >= 5)
                {
                    Reset_Gunsmith();
                    GunsmithContents.transform.GetChild(3).gameObject.SetActive(true);
                    GunsmithContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Gunsmith = 2;
            }
        }
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

    void Reset_DetectiveOffice()
    {
        DetectiveOfficeContents.transform.GetChild(0).gameObject.SetActive(false);
        DetectiveOfficeContents.transform.GetChild(1).gameObject.SetActive(false);
        DetectiveOfficeContents.transform.GetChild(2).gameObject.SetActive(false);
        DetectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(false);
        DetectiveOfficeContents.transform.GetChild(4).gameObject.SetActive(false);
        DetectiveOfficeContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Hospital()
    {
        HospitalContents.transform.GetChild(0).gameObject.SetActive(false);
        HospitalContents.transform.GetChild(1).gameObject.SetActive(false);
        HospitalContents.transform.GetChild(2).gameObject.SetActive(false);
        HospitalContents.transform.GetChild(3).gameObject.SetActive(false);
        HospitalContents.transform.GetChild(4).gameObject.SetActive(false);
        HospitalContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Newspaper()
    {
        NewspaperContents.transform.GetChild(0).gameObject.SetActive(false);
        NewspaperContents.transform.GetChild(1).gameObject.SetActive(false);
        NewspaperContents.transform.GetChild(2).gameObject.SetActive(false);
        NewspaperContents.transform.GetChild(3).gameObject.SetActive(false);
        NewspaperContents.transform.GetChild(4).gameObject.SetActive(false);
        NewspaperContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_QuestHouse()
    {
        QuestHouseContents.transform.GetChild(0).gameObject.SetActive(false);
        QuestHouseContents.transform.GetChild(1).gameObject.SetActive(false);
        QuestHouseContents.transform.GetChild(2).gameObject.SetActive(false);
        QuestHouseContents.transform.GetChild(3).gameObject.SetActive(false);
        QuestHouseContents.transform.GetChild(4).gameObject.SetActive(false);
        QuestHouseContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Station()
    {
        StationContents.transform.GetChild(0).gameObject.SetActive(false);
        StationContents.transform.GetChild(1).gameObject.SetActive(false);
        StationContents.transform.GetChild(2).gameObject.SetActive(false);
        StationContents.transform.GetChild(3).gameObject.SetActive(false);
        StationContents.transform.GetChild(4).gameObject.SetActive(false);
        StationContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Restaurant()
    {
        RestaurantContents.transform.GetChild(0).gameObject.SetActive(false);
        RestaurantContents.transform.GetChild(1).gameObject.SetActive(false);
        RestaurantContents.transform.GetChild(2).gameObject.SetActive(false);
        RestaurantContents.transform.GetChild(3).gameObject.SetActive(false);
        RestaurantContents.transform.GetChild(4).gameObject.SetActive(false);
        RestaurantContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_University()
    {
        UniversityContents.transform.GetChild(0).gameObject.SetActive(false);
        UniversityContents.transform.GetChild(1).gameObject.SetActive(false);
        UniversityContents.transform.GetChild(2).gameObject.SetActive(false);
        UniversityContents.transform.GetChild(3).gameObject.SetActive(false);
        UniversityContents.transform.GetChild(4).gameObject.SetActive(false);
        UniversityContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Riverside()
    {
        RiversideContents.transform.GetChild(0).gameObject.SetActive(false);
        RiversideContents.transform.GetChild(1).gameObject.SetActive(false);
        RiversideContents.transform.GetChild(2).gameObject.SetActive(false);
        RiversideContents.transform.GetChild(3).gameObject.SetActive(false);
        RiversideContents.transform.GetChild(4).gameObject.SetActive(false);
        RiversideContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Bar()
    {
        BarContents.transform.GetChild(0).gameObject.SetActive(false);
        BarContents.transform.GetChild(1).gameObject.SetActive(false);
        BarContents.transform.GetChild(2).gameObject.SetActive(false);
        BarContents.transform.GetChild(3).gameObject.SetActive(false);
        BarContents.transform.GetChild(4).gameObject.SetActive(false);
        BarContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Slum()
    {
        SlumContents.transform.GetChild(0).gameObject.SetActive(false);
        SlumContents.transform.GetChild(1).gameObject.SetActive(false);
        SlumContents.transform.GetChild(2).gameObject.SetActive(false);
        SlumContents.transform.GetChild(3).gameObject.SetActive(false);
        SlumContents.transform.GetChild(4).gameObject.SetActive(false);
        SlumContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_PoliceOffice()
    {
        PoliceOfficeContents.transform.GetChild(0).gameObject.SetActive(false);
        PoliceOfficeContents.transform.GetChild(1).gameObject.SetActive(false);
        PoliceOfficeContents.transform.GetChild(2).gameObject.SetActive(false);
        PoliceOfficeContents.transform.GetChild(3).gameObject.SetActive(false);
        PoliceOfficeContents.transform.GetChild(4).gameObject.SetActive(false);
        PoliceOfficeContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Gunsmith()
    {
        GunsmithContents.transform.GetChild(0).gameObject.SetActive(false);
        GunsmithContents.transform.GetChild(1).gameObject.SetActive(false);
        GunsmithContents.transform.GetChild(2).gameObject.SetActive(false);
        GunsmithContents.transform.GetChild(3).gameObject.SetActive(false);
        GunsmithContents.transform.GetChild(4).gameObject.SetActive(false);
        GunsmithContents.transform.GetChild(5).gameObject.SetActive(false);
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

    void Reset_Wharf()
    {
        WharfContents.transform.GetChild(0).gameObject.SetActive(false);
        WharfContents.transform.GetChild(1).gameObject.SetActive(false);
        WharfContents.transform.GetChild(2).gameObject.SetActive(false);
        WharfContents.transform.GetChild(3).gameObject.SetActive(false);
        WharfContents.transform.GetChild(4).gameObject.SetActive(false);
        WharfContents.transform.GetChild(5).gameObject.SetActive(false);
    }

    public void Open_DetectiveOffice()
    {
        CloseAllContents();
        DetectiveOffice_Detail.SetActive(true);
    }
    public void Open_Hospital()
    {
        CloseAllContents();
        Hospital_Detail.SetActive(true);
    }
    public void Open_Newspaper()
    {
        CloseAllContents();
        Newspaper_Detail.SetActive(true);
    }
    public void Open_QuestHouse()
    {
        CloseAllContents();
        QuestHouse_Detail.SetActive(true);
    }
    public void Open_Station()
    {
        CloseAllContents();
        Station_Detail.SetActive(true);
    }
    public void Open_Restaurant()
    {
        CloseAllContents();
        Restaurant_Detail.SetActive(true);
    }
    public void Open_University()
    {
        CloseAllContents();
        University_Detail.SetActive(true);
    }
    public void Open_Riverside()
    {
        CloseAllContents();
        Riverside_Detail.SetActive(true);
    }
    public void Open_Bar()
    {
        CloseAllContents();
        Bar_Detail.SetActive(true);
    }
    public void Open_Slum()
    {
        CloseAllContents();
        Slum_Detail.SetActive(true);
    }
    public void Open_PoliceOffice()
    {
        CloseAllContents();
        PoliceOffice_Detail.SetActive(true);
    }
    public void Open_Gunsmith()
    {
        CloseAllContents();
        Gunsmith_Detail.SetActive(true);
    }
    public void Open_SewageMaintenanceOffice()
    {
        CloseAllContents();
        SewageMaintenanceOffice_Detail.SetActive(true);
    }
    public void Open_Wharf()
    {
        CloseAllContents();
        Wharf_Detail.SetActive(true);
    }
    void CloseAllContents()
    {
        DetectiveOffice_Detail.SetActive(false);
        Newspaper_Detail.SetActive(false);
        Hospital_Detail.SetActive(false);
        QuestHouse_Detail.SetActive(false);
        Station_Detail.SetActive(false);
        Restaurant_Detail.SetActive(false);
        University_Detail.SetActive(false);
        Riverside_Detail.SetActive(false);
        Bar_Detail.SetActive(false);
        Slum_Detail.SetActive(false);
        PoliceOffice_Detail.SetActive(false);
        Gunsmith_Detail.SetActive(false);
        SewageMaintenanceOffice_Detail.SetActive(false);
        Wharf_Detail.SetActive(false);
    }
}



