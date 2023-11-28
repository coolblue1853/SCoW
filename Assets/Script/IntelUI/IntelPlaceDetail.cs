using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IntelPlaceDetail : MonoBehaviour
{
    GameObject detailDetectiveOffice1;
    GameObject detailDetectiveOffice2;
    GameObject detailDetectiveOffice3;
    GameObject detailDetectiveOffice4;
    GameObject detailDetectiveOffice5;
    GameObject detailDetectiveOffice6;
    bool onDetectiveOffice1;
    bool onDetectiveOffice2;
    bool onDetectiveOffice3;
    bool onDetectiveOffice4;
    bool onDetectiveOffice5;
    bool onDetectiveOffice6;
    int countDetectiveOffice = 0;
    public GameObject detectiveOfficeContents;
    public GameObject detectiveOfficeDetail;
    public static int detectiveOfficeIntelInt;
    public GameObject nextButtonDetectiveOffice;
    public GameObject prevButtonDetectiveOffice;
    string nowPage;

    GameObject detailHospital1;
    GameObject detailHospital2;
    GameObject detailHospital3;
    GameObject detailHospital4;
    GameObject detailHospital5;
    GameObject detailHospital6;
    bool onHospital1;
    bool onHospital2;
    bool onHospital3;
    bool onHospital4;
    bool onHospital5;
    bool onHospital6;
    int countHospital = 0;
    public GameObject hospitalContents;
    public GameObject hospitalDetail;
    public static int hospitalIntelInt;
    public GameObject nextButtonHospital;
    public GameObject prevButtonHospital;

    GameObject detailNewspaper1;
    GameObject detailNewspaper2;
    GameObject detailNewspaper3;
    GameObject detailNewspaper4;
    GameObject detailNewspaper5;
    GameObject detailNewspaper6;
    bool onNewspaper1;
    bool onNewspaper2;
    bool onNewspaper3;
    bool onNewspaper4;
    bool onNewspaper5;
    bool onNewspaper6;
    int countNewspaper = 0;
    public GameObject newspaperContents;
    public GameObject newspaperDetail;
    public static int newspaperIntelInt;
    public GameObject nextButtonNewspaper;
    public GameObject prevButtonNewspaper;

    GameObject detailQuestHouse1;
    GameObject detailQuestHouse2;
    GameObject detailQuestHouse3;
    GameObject detailQuestHouse4;
    GameObject detailQuestHouse5;
    GameObject detailQuestHouse6;
    bool onQuestHouse1;
    bool onQuestHouse2;
    bool onQuestHouse3;
    bool onQuestHouse4;
    bool onQuestHouse5;
    bool onQuestHouse6;
    int countQuestHouse = 0;
    public GameObject questHouseContents;
    public GameObject questHouseDetail;
    public static int questHouseIntelInt;
    public GameObject nextButtonQuestHouse;
    public GameObject prevButtonQuestHouse;

    GameObject detailStation1;
    GameObject detailStation2;
    GameObject detailStation3;
    GameObject detailStation4;
    GameObject detailStation5;
    GameObject detailStation6;
    bool onStation1;
    bool onStation2;
    bool onStation3;
    bool onStation4;
    bool onStation5;
    bool onStation6;
    int countStation = 0;
    public GameObject stationContents;
    public GameObject stationDetail;
    public static int stationIntelInt;
    public GameObject nextButtonStation;
    public GameObject prevButtonStation;

    GameObject detailSewer1;
    GameObject detailSewer2;
    GameObject detailSewer3;
    GameObject detailSewer4;
    GameObject detailSewer5;
    GameObject detailSewer6;
    bool onSewer1;
    bool onSewer2;
    bool onSewer3;
    bool onSewer4;
    bool onSewer5;
    bool onSewer6;
    int countSewer = 0;
    public GameObject sewerContents;
    public GameObject sewerDetail;
    public static int sewerIntelInt;
    public GameObject nextButtonSewer;
    public GameObject prevButtonSewer;

    GameObject detailUniversity1;
    GameObject detailUniversity2;
    GameObject detailUniversity3;
    GameObject detailUniversity4;
    GameObject detailUniversity5;
    GameObject detailUniversity6;
    bool onUniversity1;
    bool onUniversity2;
    bool onUniversity3;
    bool onUniversity4;
    bool onUniversity5;
    bool onUniversity6;
    int countUniversity = 0;
    public GameObject universityContents;
    public GameObject universityDetail;
    public static int universityIntelInt;
    public GameObject nextButtonUniversity;
    public GameObject prevButtonUniversity;

    GameObject detailRiverside1;
    GameObject detailRiverside2;
    GameObject detailRiverside3;
    GameObject detailRiverside4;
    GameObject detailRiverside5;
    GameObject detailRiverside6;
    bool onRiverside1;
    bool onRiverside2;
    bool onRiverside3;
    bool onRiverside4;
    bool onRiverside5;
    bool onRiverside6;
    int Count_Riverside = 0;
    public GameObject riversideContents;
    public GameObject riversideDetail;
    public static int riversideIntelInt;
    public GameObject nextButtonRiverside;
    public GameObject prevButtonRiverside;

    GameObject detailBar1;
    GameObject detailBar2;
    GameObject detailBar3;
    GameObject detailBar4;
    GameObject detailBar5;
    GameObject detailBar6;
    bool onBar1;
    bool onBar2;
    bool onBar3;
    bool onBar4;
    bool onBar5;
    bool onBar6;
    int countBar = 0;
    public GameObject barContents;
    public GameObject barDetail;
    public static int barIntelInt;
    public GameObject nextButtonBar;
    public GameObject prevButtonBar;


    GameObject detailSlum1;
    GameObject detailSlum2;
    GameObject detailSlum3;
    GameObject detailSlum4;
    GameObject detailSlum5;
    GameObject detailSlum6;
    bool onSlum1;
    bool onSlum2;
    bool onSlum3;
    bool onSlum4;
    bool onSlum5;
    bool onSlum6;
    int countSlum = 0;
    public GameObject slumContents;
    public GameObject slumDetail;
    public static int slumIntelInt;
    public GameObject nextButtonSlum;
    public GameObject prevButtonSlum;

    GameObject detailPoliceOffice1;
    GameObject detailPoliceOffice2;
    GameObject detailPoliceOffice3;
    GameObject detailPoliceOffice4;
    GameObject detailPoliceOffice5;
    GameObject detailPoliceOffice6;
    bool onPoliceOffice1;
    bool onPoliceOffice2;
    bool onPoliceOffice3;
    bool onPoliceOffice4;
    bool onPoliceOffice5;
    bool onPoliceOffice6;
    int countPoliceOffice = 0;
    public GameObject policeOfficeContents;
    public GameObject policeOfficeDetail;
    public static int policeOfficeIntelInt;
    public GameObject nextButtonPoliceOffice;
    public GameObject prevButtonPoliceOffice;

    GameObject detailGunsmith1;
    GameObject detailGunsmith2;
    GameObject detailGunsmith3;
    GameObject detailGunsmith4;
    GameObject detailGunsmith5;
    GameObject detailGunsmith6;
    bool onGunsmith1;
    bool onGunsmith2;
    bool onGunsmith3;
    bool onGunsmith4;
    bool onGunsmith5;
    bool onGunsmith6;
    int countGunsmith = 0;
    public GameObject gunsmithContents;
    public GameObject gunsmithDetail;
    public static int gunsmithIntelInt;
    public GameObject nextButtonGunsmith;
    public GameObject prevButtonGunsmith;

    GameObject detailSewageMaintenanceOffice1;
    GameObject detailSewageMaintenanceOffice2;
    GameObject detailSewageMaintenanceOffice3;
    GameObject detailSewageMaintenanceOffice4;
    GameObject detailSewageMaintenanceOffice5;
    GameObject detailSewageMaintenanceOffice6;
    bool onSewageMaintenanceOffice1;
    bool onSewageMaintenanceOffice2;
    bool onSewageMaintenanceOffice3;
    bool onSewageMaintenanceOffice4;
    bool onSewageMaintenanceOffice5;
    bool onSewageMaintenanceOffice6;
    int countSewageMaintenanceOffice = 0;
    public GameObject sewageMaintenanceOfficeContents;
    public GameObject sewageMaintenanceOfficeDetail;
    public static int sewageMaintenanceOfficeIntelInt;
    public GameObject nextButtonSewageMaintenanceOffice;
    public GameObject prevButtonSewageMaintenanceOffice;

    GameObject detailWharf1;
    GameObject detailWharf2;
    GameObject detailWharf3;
    GameObject detailWharf4;
    GameObject detailWharf5;
    GameObject detailWharf6;
    bool onWharf1;
    bool onWharf2;
    bool onWharf3;
    bool onWharf4;
    bool onWharf5;
    bool onWharf6;
    int countWharf = 0;
    public GameObject wharfContents;
    public GameObject wharfDetail;
    public static int wharfIntelInt;
    public GameObject nextButtonWharf;
    public GameObject prevButtonWharf;
    void Awake()
    {
        setGameObecject();
    }
    void setGameObecject()
    {
        detailDetectiveOffice1 = detectiveOfficeContents.transform.GetChild(0).gameObject;
        detailDetectiveOffice2 = detectiveOfficeContents.transform.GetChild(1).gameObject;
        detailDetectiveOffice3 = detectiveOfficeContents.transform.GetChild(2).gameObject;
        detailDetectiveOffice4 = detectiveOfficeContents.transform.GetChild(3).gameObject;
        detailDetectiveOffice5 = detectiveOfficeContents.transform.GetChild(4).gameObject;
        detailDetectiveOffice6 = detectiveOfficeContents.transform.GetChild(5).gameObject;

        detailHospital1 = hospitalContents.transform.GetChild(0).gameObject;
        detailHospital2 = hospitalContents.transform.GetChild(1).gameObject;
        detailHospital3 = hospitalContents.transform.GetChild(2).gameObject;
        detailHospital4 = hospitalContents.transform.GetChild(3).gameObject;
        detailHospital5 = hospitalContents.transform.GetChild(4).gameObject;
        detailHospital6 = hospitalContents.transform.GetChild(5).gameObject;

        detailNewspaper1 = newspaperContents.transform.GetChild(0).gameObject;
        detailNewspaper2 = newspaperContents.transform.GetChild(1).gameObject;
        detailNewspaper3 = newspaperContents.transform.GetChild(2).gameObject;
        detailNewspaper4 = newspaperContents.transform.GetChild(3).gameObject;
        detailNewspaper5 = newspaperContents.transform.GetChild(4).gameObject;
        detailNewspaper6 = newspaperContents.transform.GetChild(5).gameObject;

        detailQuestHouse1 = questHouseContents.transform.GetChild(0).gameObject;
        detailQuestHouse2 = questHouseContents.transform.GetChild(1).gameObject;
        detailQuestHouse3 = questHouseContents.transform.GetChild(2).gameObject;
        detailQuestHouse4 = questHouseContents.transform.GetChild(3).gameObject;
        detailQuestHouse5 = questHouseContents.transform.GetChild(4).gameObject;
        detailQuestHouse6 = questHouseContents.transform.GetChild(5).gameObject;

        detailStation1 = stationContents.transform.GetChild(0).gameObject;
        detailStation2 = stationContents.transform.GetChild(1).gameObject;
        detailStation3 = stationContents.transform.GetChild(2).gameObject;
        detailStation4 = stationContents.transform.GetChild(3).gameObject;
        detailStation5 = stationContents.transform.GetChild(4).gameObject;
        detailStation6 = stationContents.transform.GetChild(5).gameObject;

        detailSewer1 = sewerContents.transform.GetChild(0).gameObject;
        detailSewer2 = sewerContents.transform.GetChild(1).gameObject;
        detailSewer3 = sewerContents.transform.GetChild(2).gameObject;
        detailSewer4 = sewerContents.transform.GetChild(3).gameObject;
        detailSewer5 = sewerContents.transform.GetChild(4).gameObject;
        detailSewer6 = sewerContents.transform.GetChild(5).gameObject;

        detailUniversity1 = universityContents.transform.GetChild(0).gameObject;
        detailUniversity2 = universityContents.transform.GetChild(1).gameObject;
        detailUniversity3 = universityContents.transform.GetChild(2).gameObject;
        detailUniversity4 = universityContents.transform.GetChild(3).gameObject;
        detailUniversity5 = universityContents.transform.GetChild(4).gameObject;
        detailUniversity6 = universityContents.transform.GetChild(5).gameObject;

        detailRiverside1 = riversideContents.transform.GetChild(0).gameObject;
        detailRiverside2 = riversideContents.transform.GetChild(1).gameObject;
        detailRiverside3 = riversideContents.transform.GetChild(2).gameObject;
        detailRiverside4 = riversideContents.transform.GetChild(3).gameObject;
        detailRiverside5 = riversideContents.transform.GetChild(4).gameObject;
        detailRiverside6 = riversideContents.transform.GetChild(5).gameObject;

        detailBar1 = barContents.transform.GetChild(0).gameObject;
        detailBar2 = barContents.transform.GetChild(1).gameObject;
        detailBar3 = barContents.transform.GetChild(2).gameObject;
        detailBar4 = barContents.transform.GetChild(3).gameObject;
        detailBar5 = barContents.transform.GetChild(4).gameObject;
        detailBar6 = barContents.transform.GetChild(5).gameObject;

        detailSlum1 = slumContents.transform.GetChild(0).gameObject;
        detailSlum2 = slumContents.transform.GetChild(1).gameObject;
        detailSlum3 = slumContents.transform.GetChild(2).gameObject;
        detailSlum4 = slumContents.transform.GetChild(3).gameObject;
        detailSlum5 = slumContents.transform.GetChild(4).gameObject;
        detailSlum6 = slumContents.transform.GetChild(5).gameObject;

        detailPoliceOffice1 = policeOfficeContents.transform.GetChild(0).gameObject;
        detailPoliceOffice2 = policeOfficeContents.transform.GetChild(1).gameObject;
        detailPoliceOffice3 = policeOfficeContents.transform.GetChild(2).gameObject;
        detailPoliceOffice4 = policeOfficeContents.transform.GetChild(3).gameObject;
        detailPoliceOffice5 = policeOfficeContents.transform.GetChild(4).gameObject;
        detailPoliceOffice6 = policeOfficeContents.transform.GetChild(5).gameObject;

        detailGunsmith1 = gunsmithContents.transform.GetChild(0).gameObject;
        detailGunsmith2 = gunsmithContents.transform.GetChild(1).gameObject;
        detailGunsmith3 = gunsmithContents.transform.GetChild(2).gameObject;
        detailGunsmith4 = gunsmithContents.transform.GetChild(3).gameObject;
        detailGunsmith5 = gunsmithContents.transform.GetChild(4).gameObject;
        detailGunsmith6 = gunsmithContents.transform.GetChild(5).gameObject;

        detailSewageMaintenanceOffice1 = sewageMaintenanceOfficeContents.transform.GetChild(0).gameObject;
        detailSewageMaintenanceOffice2 = sewageMaintenanceOfficeContents.transform.GetChild(1).gameObject;
        detailSewageMaintenanceOffice3 = sewageMaintenanceOfficeContents.transform.GetChild(2).gameObject;
        detailSewageMaintenanceOffice4 = sewageMaintenanceOfficeContents.transform.GetChild(3).gameObject;
        detailSewageMaintenanceOffice5 = sewageMaintenanceOfficeContents.transform.GetChild(4).gameObject;
        detailSewageMaintenanceOffice6 = sewageMaintenanceOfficeContents.transform.GetChild(5).gameObject;

        detailWharf1 = wharfContents.transform.GetChild(0).gameObject;
        detailWharf2 = wharfContents.transform.GetChild(1).gameObject;
        detailWharf3 = wharfContents.transform.GetChild(2).gameObject;
        detailWharf4 = wharfContents.transform.GetChild(3).gameObject;
        detailWharf5 = wharfContents.transform.GetChild(4).gameObject;
        detailWharf6 = wharfContents.transform.GetChild(5).gameObject;
    }
    void Update()
    {
        if (nextButtonBar.activeSelf == true || nextButtonDetectiveOffice.activeSelf == true || nextButtonGunsmith.activeSelf == true || nextButtonHospital.activeSelf == true || nextButtonNewspaper.activeSelf == true || nextButtonPoliceOffice.activeSelf == true || nextButtonQuestHouse.activeSelf == true || nextButtonSewer.activeSelf == true || nextButtonRiverside.activeSelf == true || nextButtonSewageMaintenanceOffice.activeSelf == true || nextButtonSlum.activeSelf == true || nextButtonStation.activeSelf == true || nextButtonUniversity.activeSelf == true || nextButtonWharf.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                NextPage();
            }
        }
        if (prevButtonBar.activeSelf == true || prevButtonDetectiveOffice.activeSelf == true || prevButtonGunsmith.activeSelf == true || prevButtonHospital.activeSelf == true || prevButtonNewspaper.activeSelf == true || prevButtonPoliceOffice.activeSelf == true || prevButtonQuestHouse.activeSelf == true || prevButtonSewer.activeSelf == true || prevButtonRiverside.activeSelf == true || prevButtonSewageMaintenanceOffice.activeSelf == true || prevButtonSlum.activeSelf == true || prevButtonStation.activeSelf == true || prevButtonUniversity.activeSelf == true || prevButtonWharf.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                PrevPage();
            }
        }
        DetectiveOfficeDetail();
        HospitalDetail();
        NewspaperDetail();
        QuestHouseDetail();
        StationDetail();
        SewerDetail();
        UniversityDetail();
        RiversideDetail();
        BarDetail();
        SlumDetail();
        PoliceOfficeDetail();
        GunsmithDetail();
        SewageMaintenanceOfficeDetail();
        WharfDetail();
        if (detectiveOfficeDetail.activeSelf == true)
        {
            nowPage = "DetectiveOffice";
        }
        if (hospitalDetail.activeSelf == true)
        {
            nowPage = "Hospital";
        }
        if (newspaperDetail.activeSelf == true)
        {
            nowPage = "Newspaper";
        }
        if (questHouseDetail.activeSelf == true)
        {
            nowPage = "QuestHouse";
        }
        if (stationDetail.activeSelf == true)
        {
            nowPage = "Station";
        }
        if (sewerDetail.activeSelf == true)
        {
            nowPage = "Sewer";
        }
        if (universityDetail.activeSelf == true)
        {
            nowPage = "University";
        }
        if (riversideDetail.activeSelf == true)
        {
            nowPage = "Riverside";
        }
        if (barDetail.activeSelf == true)
        {
            nowPage = "Bar";
        }
        if (slumDetail.activeSelf == true)
        {
            nowPage = "Slum";
        }
        if (policeOfficeDetail.activeSelf == true)
        {
            nowPage = "PoliceOffice";
        }
        if (gunsmithDetail.activeSelf == true)
        {
            nowPage = "Gunsmith";
        }
        if (sewageMaintenanceOfficeDetail.activeSelf == true)
        {
            nowPage = "SewageMaintenanceOffice";
        }
        if (wharfDetail.activeSelf == true)
        {
            nowPage = "Wharf";
        }
    }
    void DetectiveOfficeDetail()
    {
        if (onDetectiveOffice1 == false && DataBaseManager.Intel_DetectiveOffice1 == true)
        {
            onDetectiveOffice1 = true;
            detailDetectiveOffice1.SetActive(true);
            detailDetectiveOffice1.transform.SetAsLastSibling();
            countDetectiveOffice += 1;
        }
        if (onDetectiveOffice2 == false && DataBaseManager.Intel_DetectiveOffice2 == true)
        {
            onDetectiveOffice2 = true;
            detailDetectiveOffice2.SetActive(true);
            detailDetectiveOffice2.transform.SetAsLastSibling();
            countDetectiveOffice += 1;
        }
        if (onDetectiveOffice3 == false && DataBaseManager.Intel_DetectiveOffice3 == true)
        {
            onDetectiveOffice3 = true;
            detailDetectiveOffice3.SetActive(true);
            detailDetectiveOffice3.transform.SetAsLastSibling();
            countDetectiveOffice += 1;
        }
        if (onDetectiveOffice4 == false && DataBaseManager.Intel_DetectiveOffice4 == true)
        {
            onDetectiveOffice4 = true;
            detailDetectiveOffice4.SetActive(true);
            detailDetectiveOffice4.transform.SetAsLastSibling();
            countDetectiveOffice += 1;
        }
        if (onDetectiveOffice5 == false && DataBaseManager.Intel_DetectiveOffice5 == true)
        {
            onDetectiveOffice5 = true;
            detailDetectiveOffice5.SetActive(true);
            detailDetectiveOffice5.transform.SetAsLastSibling();
            countDetectiveOffice += 1;
        }
        if (onDetectiveOffice6 == false && DataBaseManager.Intel_DetectiveOffice6 == true)
        {
            onDetectiveOffice6 = true;
            detailDetectiveOffice6.SetActive(true);
            detailDetectiveOffice6.transform.SetAsLastSibling();
            countDetectiveOffice += 1;
        }
        if (countDetectiveOffice > 2 && DataBaseManager.NowPage_DetectiveOffice == 1)
        {
            nextButtonDetectiveOffice.SetActive(true);
        }
        else if (countDetectiveOffice > 4 && DataBaseManager.NowPage_DetectiveOffice == 2)
        {
            nextButtonDetectiveOffice.SetActive(true);
        }
        else
        {
            nextButtonDetectiveOffice.SetActive(false);
        }
        if (DataBaseManager.NowPage_DetectiveOffice == 2)
        {
            prevButtonDetectiveOffice.SetActive(true);
        }
        else if (DataBaseManager.NowPage_DetectiveOffice == 3)
        {
            prevButtonDetectiveOffice.SetActive(true);
        }
        else
        {
            prevButtonDetectiveOffice.SetActive(false);
        }
    }
    void HospitalDetail()
    {
        if (onHospital1 == false && DataBaseManager.Intel_Hospital1 == true)
        {
            onHospital1 = true;
            detailHospital1.SetActive(true);
            detailHospital1.transform.SetAsLastSibling();
            countHospital += 1;
        }
        if (onHospital2 == false && DataBaseManager.Intel_Hospital2 == true)
        {
            onHospital2 = true;
            detailHospital2.SetActive(true);
            detailHospital2.transform.SetAsLastSibling();
            countHospital += 1;
        }
        if (onHospital3 == false && DataBaseManager.Intel_Hospital3 == true)
        {
            onHospital3 = true;
            detailHospital3.SetActive(true);
            detailHospital3.transform.SetAsLastSibling();
            countHospital += 1;
        }
        if (onHospital4 == false && DataBaseManager.Intel_Hospital4 == true)
        {
            onHospital4 = true;
            detailHospital4.SetActive(true);
            detailHospital4.transform.SetAsLastSibling();
            countHospital += 1;
        }
        if (onHospital5 == false && DataBaseManager.Intel_Hospital5 == true)
        {
            onHospital5 = true;
            detailHospital5.SetActive(true);
            detailHospital5.transform.SetAsLastSibling();
            countHospital += 1;
        }
        if (onHospital6 == false && DataBaseManager.Intel_Hospital6 == true)
        {
            onHospital6 = true;
            detailHospital6.SetActive(true);
            detailHospital6.transform.SetAsLastSibling();
            countHospital += 1;
        }
        if (countHospital > 2 && DataBaseManager.NowPage_Hospital == 1)
        {
            nextButtonHospital.SetActive(true);
        }
        else if (countHospital > 4 && DataBaseManager.NowPage_Hospital == 2)
        {
            nextButtonHospital.SetActive(true);
        }
        else
        {
            nextButtonHospital.SetActive(false);
        }
        if (DataBaseManager.NowPage_Hospital == 2)
        {
            prevButtonHospital.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Hospital == 3)
        {
            prevButtonHospital.SetActive(true);
        }
        else
        {
            prevButtonHospital.SetActive(false);
        }
    }
    void NewspaperDetail()
    {
        if (onNewspaper1 == false && DataBaseManager.Intel_Newspaper1 == true)
        {
            onNewspaper1 = true;
            detailNewspaper1.SetActive(true);
            detailNewspaper1.transform.SetAsLastSibling();
            countNewspaper += 1;
        }
        if (onNewspaper2 == false && DataBaseManager.Intel_Newspaper2 == true)
        {
            onNewspaper2 = true;
            detailNewspaper2.SetActive(true);
            detailNewspaper2.transform.SetAsLastSibling();
            countNewspaper += 1;
        }
        if (onNewspaper3 == false && DataBaseManager.Intel_Newspaper3 == true)
        {
            onNewspaper3 = true;
            detailNewspaper3.SetActive(true);
            detailNewspaper3.transform.SetAsLastSibling();
            countNewspaper += 1;
        }
        if (onNewspaper4 == false && DataBaseManager.Intel_Newspaper4 == true)
        {
            onNewspaper4 = true;
            detailNewspaper4.SetActive(true);
            detailNewspaper4.transform.SetAsLastSibling();
            countNewspaper += 1;
        }
        if (onNewspaper5 == false && DataBaseManager.Intel_Newspaper5 == true)
        {
            onNewspaper5 = true;
            detailNewspaper5.SetActive(true);
            detailNewspaper5.transform.SetAsLastSibling();
            countNewspaper += 1;
        }
        if (onNewspaper6 == false && DataBaseManager.Intel_Newspaper6 == true)
        {
            onNewspaper6 = true;
            detailNewspaper6.SetActive(true);
            detailNewspaper6.transform.SetAsLastSibling();
            countNewspaper += 1;
        }
        if (countNewspaper > 2 && DataBaseManager.NowPage_Newspaper == 1)
        {
            nextButtonNewspaper.SetActive(true);
        }
        else if (countNewspaper > 4 && DataBaseManager.NowPage_Newspaper == 2)
        {
            nextButtonNewspaper.SetActive(true);
        }
        else
        {
            nextButtonNewspaper.SetActive(false);
        }

        if (DataBaseManager.NowPage_Newspaper == 2)
        {
            prevButtonNewspaper.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Newspaper == 3)
        {
            prevButtonNewspaper.SetActive(true);
        }
        else
        {
            prevButtonNewspaper.SetActive(false);
        }
    }
    void QuestHouseDetail()
    {
        if (onQuestHouse1 == false && DataBaseManager.Intel_QuestHouse1 == true)
        {
            onQuestHouse1 = true;
            detailQuestHouse1.SetActive(true);
            detailQuestHouse1.transform.SetAsLastSibling();
            countQuestHouse += 1;
        }
        if (onQuestHouse2 == false && DataBaseManager.Intel_QuestHouse2 == true)
        {
            onQuestHouse2 = true;
            detailQuestHouse2.SetActive(true);
            detailQuestHouse2.transform.SetAsLastSibling();
            countQuestHouse += 1;
        }
        if (onQuestHouse3 == false && DataBaseManager.Intel_QuestHouse3 == true)
        {
            onQuestHouse3 = true;
            detailQuestHouse3.SetActive(true);
            detailQuestHouse3.transform.SetAsLastSibling();
            countQuestHouse += 1;
        }
        if (onQuestHouse4 == false && DataBaseManager.Intel_QuestHouse4 == true)
        {
            onQuestHouse4 = true;
            detailQuestHouse4.SetActive(true);
            detailQuestHouse4.transform.SetAsLastSibling();
            countQuestHouse += 1;
        }
        if (onQuestHouse5 == false && DataBaseManager.Intel_QuestHouse5 == true)
        {
            onQuestHouse5 = true;
            detailQuestHouse5.SetActive(true);
            detailQuestHouse5.transform.SetAsLastSibling();
            countQuestHouse += 1;
        }
        if (onQuestHouse6 == false && DataBaseManager.Intel_QuestHouse6 == true)
        {
            onQuestHouse6 = true;
            detailQuestHouse6.SetActive(true);
            detailQuestHouse6.transform.SetAsLastSibling();
            countQuestHouse += 1;
        }

        if (countQuestHouse > 2 && DataBaseManager.NowPage_QuestHouse == 1)
        {
            nextButtonQuestHouse.SetActive(true);
        }
        else if (countQuestHouse > 4 && DataBaseManager.NowPage_QuestHouse == 2)
        {
            nextButtonQuestHouse.SetActive(true);
        }
        else
        {
            nextButtonQuestHouse.SetActive(false);
        }

        if (DataBaseManager.NowPage_QuestHouse == 2)
        {
            prevButtonQuestHouse.SetActive(true);
        }
        else if (DataBaseManager.NowPage_QuestHouse == 3)
        {
            prevButtonQuestHouse.SetActive(true);
        }
        else
        {
            prevButtonQuestHouse.SetActive(false);
        }
    }
    void StationDetail()
    {
        if (onStation1 == false && DataBaseManager.Intel_Station1 == true)
        {
            onStation1 = true;
            detailStation1.SetActive(true);
            detailStation1.transform.SetAsLastSibling();
            countStation += 1;
        }
        if (onStation2 == false && DataBaseManager.Intel_Station2 == true)
        {
            onStation2 = true;
            detailStation2.SetActive(true);
            detailStation2.transform.SetAsLastSibling();
            countStation += 1;
        }
        if (onStation3 == false && DataBaseManager.Intel_Station3 == true)
        {
            onStation3 = true;
            detailStation3.SetActive(true);
            detailStation3.transform.SetAsLastSibling();
            countStation += 1;
        }
        if (onStation4 == false && DataBaseManager.Intel_Station4 == true)
        {
            onStation4 = true;
            detailStation4.SetActive(true);
            detailStation4.transform.SetAsLastSibling();
            countStation += 1;
        }
        if (onStation5 == false && DataBaseManager.Intel_Station5 == true)
        {
            onStation5 = true;
            detailStation5.SetActive(true);
            detailStation5.transform.SetAsLastSibling();
            countStation += 1;
        }
        if (onStation6 == false && DataBaseManager.Intel_Station6 == true)
        {
            onStation6 = true;
            detailStation6.SetActive(true);
            detailStation6.transform.SetAsLastSibling();
            countStation += 1;
        }

        if (countStation > 2 && DataBaseManager.NowPage_Station == 1)
        {
            nextButtonStation.SetActive(true);
        }
        else if (countStation > 4 && DataBaseManager.NowPage_Station == 2)
        {
            nextButtonStation.SetActive(true);
        }
        else
        {
            nextButtonStation.SetActive(false);
        }

        if (DataBaseManager.NowPage_Station == 2)
        {
            prevButtonStation.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Station == 3)
        {
            prevButtonStation.SetActive(true);
        }
        else
        {
            prevButtonStation.SetActive(false);
        }
    }
    void SewerDetail()
    {
        if (onSewer1 == false && DataBaseManager.Intel_Sewer1 == true)
        {
            onSewer1 = true;
            detailSewer1.SetActive(true);
            detailSewer1.transform.SetAsLastSibling();
            countSewer += 1;
        }
        if (onSewer2 == false && DataBaseManager.Intel_Sewer2 == true)
        {
            onSewer2 = true;
            detailSewer2.SetActive(true);
            detailSewer2.transform.SetAsLastSibling();
            countSewer += 1;
        }
        if (onSewer3 == false && DataBaseManager.Intel_Sewer3 == true)
        {
            onSewer3 = true;
            detailSewer3.SetActive(true);
            detailSewer3.transform.SetAsLastSibling();
            countSewer += 1;
        }
        if (onSewer4 == false && DataBaseManager.Intel_Sewer4 == true)
        {
            onSewer4 = true;
            detailSewer4.SetActive(true);
            detailSewer4.transform.SetAsLastSibling();
            countSewer += 1;
        }
        if (onSewer5 == false && DataBaseManager.Intel_Sewer5 == true)
        {
            onSewer5 = true;
            detailSewer5.SetActive(true);
            detailSewer5.transform.SetAsLastSibling();
            countSewer += 1;
        }
        if (onSewer6 == false && DataBaseManager.Intel_Sewer6 == true)
        {
            onSewer6 = true;
            detailSewer6.SetActive(true);
            detailSewer6.transform.SetAsLastSibling();
            countSewer += 1;
        }

        if (countSewer > 2 && DataBaseManager.NowPage_Sewer == 1)
        {
            nextButtonSewer.SetActive(true);
        }
        else if (countSewer > 4 && DataBaseManager.NowPage_Sewer == 2)
        {
            nextButtonSewer.SetActive(true);
        }
        else
        {
            nextButtonSewer.SetActive(false);
        }

        if (DataBaseManager.NowPage_Sewer == 2)
        {
            prevButtonSewer.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Sewer
            == 3)
        {
            prevButtonSewer.SetActive(true);
        }
        else
        {
            prevButtonSewer.SetActive(false);
        }
    }
    void UniversityDetail()
    {
        if (onUniversity1 == false && DataBaseManager.Intel_University1 == true)
        {
            onUniversity1 = true;
            detailUniversity1.SetActive(true);
            detailUniversity1.transform.SetAsLastSibling();
            countUniversity += 1;
        }
        if (onUniversity2 == false && DataBaseManager.Intel_University2 == true)
        {
            onUniversity2 = true;
            detailUniversity2.SetActive(true);
            detailUniversity2.transform.SetAsLastSibling();
            countUniversity += 1;
        }
        if (onUniversity3 == false && DataBaseManager.Intel_University3 == true)
        {
            onUniversity3 = true;
            detailUniversity3.SetActive(true);
            detailUniversity3.transform.SetAsLastSibling();
            countUniversity += 1;
        }
        if (onUniversity4 == false && DataBaseManager.Intel_University4 == true)
        {
            onUniversity4 = true;
            detailUniversity4.SetActive(true);
            detailUniversity4.transform.SetAsLastSibling();
            countUniversity += 1;
        }
        if (onUniversity5 == false && DataBaseManager.Intel_University5 == true)
        {
            onUniversity5 = true;
            detailUniversity5.SetActive(true);
            detailUniversity5.transform.SetAsLastSibling();
            countUniversity += 1;
        }
        if (onUniversity6 == false && DataBaseManager.Intel_University6 == true)
        {
            onUniversity6 = true;
            detailUniversity6.SetActive(true);
            detailUniversity6.transform.SetAsLastSibling();
            countUniversity += 1;
        }

        if (countUniversity > 2 && DataBaseManager.NowPage_University == 1)
        {
            nextButtonUniversity.SetActive(true);
        }
        else if (countUniversity > 4 && DataBaseManager.NowPage_University == 2)
        {
            nextButtonUniversity.SetActive(true);
        }
        else
        {
            nextButtonUniversity.SetActive(false);
        }

        if (DataBaseManager.NowPage_University == 2)
        {
            prevButtonUniversity.SetActive(true);
        }
        else if (DataBaseManager.NowPage_University
            == 3)
        {
            prevButtonUniversity.SetActive(true);
        }
        else
        {
            prevButtonUniversity.SetActive(false);
        }
    }
    void RiversideDetail()
    {
        if (onRiverside1 == false && DataBaseManager.Intel_Riverside1 == true)
        {
            onRiverside1 = true;
            detailRiverside1.SetActive(true);
            detailRiverside1.transform.SetAsLastSibling();
            Count_Riverside += 1;
        }
        if (onRiverside2 == false && DataBaseManager.Intel_Riverside2 == true)
        {
            onRiverside2 = true;
            detailRiverside2.SetActive(true);
            detailRiverside2.transform.SetAsLastSibling();
            Count_Riverside += 1;
        }
        if (onRiverside3 == false && DataBaseManager.Intel_Riverside3 == true)
        {
            onRiverside3 = true;
            detailRiverside3.SetActive(true);
            detailRiverside3.transform.SetAsLastSibling();
            Count_Riverside += 1;
        }
        if (onRiverside4 == false && DataBaseManager.Intel_Riverside4 == true)
        {
            onRiverside4 = true;
            detailRiverside4.SetActive(true);
            detailRiverside4.transform.SetAsLastSibling();
            Count_Riverside += 1;
        }
        if (onRiverside5 == false && DataBaseManager.Intel_Riverside5 == true)
        {
            onRiverside5 = true;
            detailRiverside5.SetActive(true);
            detailRiverside5.transform.SetAsLastSibling();
            Count_Riverside += 1;
        }
        if (onRiverside6 == false && DataBaseManager.Intel_Riverside6 == true)
        {
            onRiverside6 = true;
            detailRiverside6.SetActive(true);
            detailRiverside6.transform.SetAsLastSibling();
            Count_Riverside += 1;
        }

        if (Count_Riverside > 2 && DataBaseManager.NowPage_Riverside == 1)
        {
            nextButtonRiverside.SetActive(true);
        }
        else if (Count_Riverside > 4 && DataBaseManager.NowPage_Riverside == 2)
        {
            nextButtonRiverside.SetActive(true);
        }
        else
        {
            nextButtonRiverside.SetActive(false);
        }

        if (DataBaseManager.NowPage_Riverside == 2)
        {
            prevButtonRiverside.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Riverside
            == 3)
        {
            prevButtonRiverside.SetActive(true);
        }
        else
        {
            prevButtonRiverside.SetActive(false);
        }
    }
    void BarDetail()
    {
        if (onBar1 == false && DataBaseManager.Intel_Bar1 == true)
        {
            onBar1 = true;
            detailBar1.SetActive(true);
            detailBar1.transform.SetAsLastSibling();
            countBar += 1;
        }
        if (onBar2 == false && DataBaseManager.Intel_Bar2 == true)
        {
            onBar2 = true;
            detailBar2.SetActive(true);
            detailBar2.transform.SetAsLastSibling();
            countBar += 1;
        }
        if (onBar3 == false && DataBaseManager.Intel_Bar3 == true)
        {
            onBar3 = true;
            detailBar3.SetActive(true);
            detailBar3.transform.SetAsLastSibling();
            countBar += 1;
        }
        if (onBar4 == false && DataBaseManager.Intel_Bar4 == true)
        {
            onBar4 = true;
            detailBar4.SetActive(true);
            detailBar4.transform.SetAsLastSibling();
            countBar += 1;
        }
        if (onBar5 == false && DataBaseManager.Intel_Bar5 == true)
        {
            onBar5 = true;
            detailBar5.SetActive(true);
            detailBar5.transform.SetAsLastSibling();
            countBar += 1;
        }
        if (onBar6 == false && DataBaseManager.Intel_Bar6 == true)
        {
            onBar6 = true;
            detailBar6.SetActive(true);
            detailBar6.transform.SetAsLastSibling();
            countBar += 1;
        }

        if (countBar > 2 && DataBaseManager.NowPage_Bar == 1)
        {
            nextButtonBar.SetActive(true);
        }
        else if (countBar > 4 && DataBaseManager.NowPage_Bar == 2)
        {
            nextButtonBar.SetActive(true);
        }
        else
        {
            nextButtonBar.SetActive(false);
        }

        if (DataBaseManager.NowPage_Bar == 2)
        {
            prevButtonBar.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Bar == 3)
        {
            prevButtonBar.SetActive(true);
        }
        else
        {
            prevButtonBar.SetActive(false);
        }
    }
    void SlumDetail()
    {
        if (onSlum1 == false && DataBaseManager.Intel_Slum1 == true)
        {
            onSlum1 = true;
            detailSlum1.SetActive(true);
            detailSlum1.transform.SetAsLastSibling();
            countSlum += 1;
        }
        if (onSlum2 == false && DataBaseManager.Intel_Slum2 == true)
        {
            onSlum2 = true;
            detailSlum2.SetActive(true);
            detailSlum2.transform.SetAsLastSibling();
            countSlum += 1;
        }
        if (onSlum3 == false && DataBaseManager.Intel_Slum3 == true)
        {
            onSlum3 = true;
            detailSlum3.SetActive(true);
            detailSlum3.transform.SetAsLastSibling();
            countSlum += 1;
        }
        if (onSlum4 == false && DataBaseManager.Intel_Slum4 == true)
        {
            onSlum4 = true;
            detailSlum4.SetActive(true);
            detailSlum4.transform.SetAsLastSibling();
            countSlum += 1;
        }
        if (onSlum5 == false && DataBaseManager.Intel_Slum5 == true)
        {
            onSlum5 = true;
            detailSlum5.SetActive(true);
            detailSlum5.transform.SetAsLastSibling();
            countSlum += 1;
        }
        if (onSlum6 == false && DataBaseManager.Intel_Slum6 == true)
        {
            onSlum6 = true;
            detailSlum6.SetActive(true);
            detailSlum6.transform.SetAsLastSibling();
            countSlum += 1;
        }

        if (countSlum > 2 && DataBaseManager.NowPage_Slum == 1)
        {
            nextButtonSlum.SetActive(true);
        }
        else if (countSlum > 4 && DataBaseManager.NowPage_Slum == 2)
        {
            nextButtonSlum.SetActive(true);
        }
        else
        {
            nextButtonSlum.SetActive(false);
        }

        if (DataBaseManager.NowPage_Slum == 2)
        {
            prevButtonSlum.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Slum == 3)
        {
            prevButtonSlum.SetActive(true);
        }
        else
        {
            prevButtonSlum.SetActive(false);
        }
    }
    void PoliceOfficeDetail()
    {
        if (onPoliceOffice1 == false && DataBaseManager.Intel_PoliceOffice1 == true)
        {
            onPoliceOffice1 = true;
            detailPoliceOffice1.SetActive(true);
            detailPoliceOffice1.transform.SetAsLastSibling();
            countPoliceOffice += 1;
        }
        if (onPoliceOffice2 == false && DataBaseManager.Intel_PoliceOffice2 == true)
        {
            onPoliceOffice2 = true;
            detailPoliceOffice2.SetActive(true);
            detailPoliceOffice2.transform.SetAsLastSibling();
            countPoliceOffice += 1;
        }
        if (onPoliceOffice3 == false && DataBaseManager.Intel_PoliceOffice3 == true)
        {
            onPoliceOffice3 = true;
            detailPoliceOffice3.SetActive(true);
            detailPoliceOffice3.transform.SetAsLastSibling();
            countPoliceOffice += 1;
        }
        if (onPoliceOffice4 == false && DataBaseManager.Intel_PoliceOffice4 == true)
        {
            onPoliceOffice4 = true;
            detailPoliceOffice4.SetActive(true);
            detailPoliceOffice4.transform.SetAsLastSibling();
            countPoliceOffice += 1;
        }
        if (onPoliceOffice5 == false && DataBaseManager.Intel_PoliceOffice5 == true)
        {
            onPoliceOffice5 = true;
            detailPoliceOffice5.SetActive(true);
            detailPoliceOffice5.transform.SetAsLastSibling();
            countPoliceOffice += 1;
        }
        if (onPoliceOffice6 == false && DataBaseManager.Intel_PoliceOffice6 == true)
        {
            onPoliceOffice6 = true;
            detailPoliceOffice6.SetActive(true);
            detailPoliceOffice6.transform.SetAsLastSibling();
            countPoliceOffice += 1;
        }

        if (countPoliceOffice > 2 && DataBaseManager.NowPage_PoliceOffice == 1)
        {
            nextButtonPoliceOffice.SetActive(true);
        }
        else if (countPoliceOffice > 4 && DataBaseManager.NowPage_PoliceOffice == 2)
        {
            nextButtonPoliceOffice.SetActive(true);
        }
        else
        {
            nextButtonPoliceOffice.SetActive(false);
        }

        if (DataBaseManager.NowPage_PoliceOffice == 2)
        {
            prevButtonPoliceOffice.SetActive(true);
        }
        else if (DataBaseManager.NowPage_PoliceOffice == 3)
        {
            prevButtonPoliceOffice.SetActive(true);
        }
        else
        {
            prevButtonPoliceOffice.SetActive(false);
        }
    }
    void GunsmithDetail()
    {
        if (onGunsmith1 == false && DataBaseManager.Intel_Gunsmith1 == true)
        {
            onGunsmith1 = true;
            detailGunsmith1.SetActive(true);
            detailGunsmith1.transform.SetAsLastSibling();
            countGunsmith += 1;
        }
        if (onGunsmith2 == false && DataBaseManager.Intel_Gunsmith2 == true)
        {
            onGunsmith2 = true;
            detailGunsmith2.SetActive(true);
            detailGunsmith2.transform.SetAsLastSibling();
            countGunsmith += 1;
        }
        if (onGunsmith3 == false && DataBaseManager.Intel_Gunsmith3 == true)
        {
            onGunsmith3 = true;
            detailGunsmith3.SetActive(true);
            detailGunsmith3.transform.SetAsLastSibling();
            countGunsmith += 1;
        }
        if (onGunsmith4 == false && DataBaseManager.Intel_Gunsmith4 == true)
        {
            onGunsmith4 = true;
            detailGunsmith4.SetActive(true);
            detailGunsmith4.transform.SetAsLastSibling();
            countGunsmith += 1;
        }
        if (onGunsmith5 == false && DataBaseManager.Intel_Gunsmith5 == true)
        {
            onGunsmith5 = true;
            detailGunsmith5.SetActive(true);
            detailGunsmith5.transform.SetAsLastSibling();
            countGunsmith += 1;
        }
        if (onGunsmith6 == false && DataBaseManager.Intel_Gunsmith6 == true)
        {
            onGunsmith6 = true;
            detailGunsmith6.SetActive(true);
            detailGunsmith6.transform.SetAsLastSibling();
            countGunsmith += 1;
        }

        if (countGunsmith > 2 && DataBaseManager.NowPage_Gunsmith == 1)
        {
            nextButtonGunsmith.SetActive(true);
        }
        else if (countGunsmith > 4 && DataBaseManager.NowPage_Gunsmith == 2)
        {
            nextButtonGunsmith.SetActive(true);
        }
        else
        {
            nextButtonGunsmith.SetActive(false);
        }

        if (DataBaseManager.NowPage_Gunsmith == 2)
        {
            prevButtonGunsmith.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Gunsmith == 3)
        {
            prevButtonGunsmith.SetActive(true);
        }
        else
        {
            prevButtonGunsmith.SetActive(false);
        }
    }
    void SewageMaintenanceOfficeDetail()
    {
        if (onSewageMaintenanceOffice1 == false && DataBaseManager.Intel_SewageMaintenanceOffice1 == true)
        {
            onSewageMaintenanceOffice1 = true;
            detailSewageMaintenanceOffice1.SetActive(true);
            detailSewageMaintenanceOffice1.transform.SetAsLastSibling();
            countSewageMaintenanceOffice += 1;
        }
        if (onSewageMaintenanceOffice2 == false && DataBaseManager.Intel_SewageMaintenanceOffice2 == true)
        {
            onSewageMaintenanceOffice2 = true;
            detailSewageMaintenanceOffice2.SetActive(true);
            detailSewageMaintenanceOffice2.transform.SetAsLastSibling();
            countSewageMaintenanceOffice += 1;
        }
        if (onSewageMaintenanceOffice3 == false && DataBaseManager.Intel_SewageMaintenanceOffice3 == true)
        {
            onSewageMaintenanceOffice3 = true;
            detailSewageMaintenanceOffice3.SetActive(true);
            detailSewageMaintenanceOffice3.transform.SetAsLastSibling();
            countSewageMaintenanceOffice += 1;
        }
        if (onSewageMaintenanceOffice4 == false && DataBaseManager.Intel_SewageMaintenanceOffice4 == true)
        {
            onSewageMaintenanceOffice4 = true;
            detailSewageMaintenanceOffice4.SetActive(true);
            detailSewageMaintenanceOffice4.transform.SetAsLastSibling();
            countSewageMaintenanceOffice += 1;
        }
        if (onSewageMaintenanceOffice5 == false && DataBaseManager.Intel_SewageMaintenanceOffice5 == true)
        {
            onSewageMaintenanceOffice5 = true;
            detailSewageMaintenanceOffice5.SetActive(true);
            detailSewageMaintenanceOffice5.transform.SetAsLastSibling();
            countSewageMaintenanceOffice += 1;
        }
        if (onSewageMaintenanceOffice6 == false && DataBaseManager.Intel_SewageMaintenanceOffice6 == true)
        {
            onSewageMaintenanceOffice6 = true;
            detailSewageMaintenanceOffice6.SetActive(true);
            detailSewageMaintenanceOffice6.transform.SetAsLastSibling();
            countSewageMaintenanceOffice += 1;
        }

        if (countSewageMaintenanceOffice > 2 && DataBaseManager.NowPage_SewageMaintenanceOffice == 1)
        {
            nextButtonSewageMaintenanceOffice.SetActive(true);
        }
        else if (countSewageMaintenanceOffice > 4 && DataBaseManager.NowPage_SewageMaintenanceOffice == 2)
        {
            nextButtonSewageMaintenanceOffice.SetActive(true);
        }
        else
        {
            nextButtonSewageMaintenanceOffice.SetActive(false);
        }

        if (DataBaseManager.NowPage_SewageMaintenanceOffice == 2)
        {
            prevButtonSewageMaintenanceOffice.SetActive(true);
        }
        else if (DataBaseManager.NowPage_SewageMaintenanceOffice == 3)
        {
            prevButtonSewageMaintenanceOffice.SetActive(true);
        }
        else
        {
            prevButtonSewageMaintenanceOffice.SetActive(false);
        }
    }
    void WharfDetail()
    {
        if (onWharf1 == false && DataBaseManager.Intel_Wharf1 == true)
        {
            onWharf1 = true;
            detailWharf1.SetActive(true);
            detailWharf1.transform.SetAsLastSibling();
            countWharf += 1;
        }
        if (onWharf2 == false && DataBaseManager.Intel_Wharf2 == true)
        {
            onWharf2 = true;
            detailWharf2.SetActive(true);
            detailWharf2.transform.SetAsLastSibling();
            countWharf += 1;
        }
        if (onWharf3 == false && DataBaseManager.Intel_Wharf3 == true)
        {
            onWharf3 = true;
            detailWharf3.SetActive(true);
            detailWharf3.transform.SetAsLastSibling();
            countWharf += 1;
        }
        if (onWharf4 == false && DataBaseManager.Intel_Wharf4 == true)
        {
            onWharf4 = true;
            detailWharf4.SetActive(true);
            detailWharf4.transform.SetAsLastSibling();
            countWharf += 1;
        }
        if (onWharf5 == false && DataBaseManager.Intel_Wharf5 == true)
        {
            onWharf5 = true;
            detailWharf5.SetActive(true);
            detailWharf5.transform.SetAsLastSibling();
            countWharf += 1;
        }
        if (onWharf6 == false && DataBaseManager.Intel_Wharf6 == true)
        {
            onWharf6 = true;
            detailWharf6.SetActive(true);
            detailWharf6.transform.SetAsLastSibling();
            countWharf += 1;
        }

        if (countWharf > 2 && DataBaseManager.NowPage_Wharf == 1)
        {
            nextButtonWharf.SetActive(true);
        }
        else if (countWharf > 4 && DataBaseManager.NowPage_Wharf == 2)
        {
            nextButtonWharf.SetActive(true);
        }
        else
        {
            nextButtonWharf.SetActive(false);
        }

        if (DataBaseManager.NowPage_Wharf == 2)
        {
            prevButtonWharf.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Wharf == 3)
        {
            prevButtonWharf.SetActive(true);
        }
        else
        {
            prevButtonWharf.SetActive(false);
        }
    }
    public void NextPage()
    {
        if (nowPage == "DetectiveOffice")
        {
            if (DataBaseManager.NowPage_DetectiveOffice == 1)
            {
                if (countDetectiveOffice >= 6)
                {
                    Reset_DetectiveOffice();
                    detectiveOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    detectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countDetectiveOffice >= 5)
                {
                    Reset_DetectiveOffice();
                    detectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    detectiveOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countDetectiveOffice >= 4)
                {
                    Reset_DetectiveOffice();
                    detectiveOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                    detectiveOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countDetectiveOffice >= 3)
                {
                    Reset_DetectiveOffice();
                    detectiveOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_DetectiveOffice = 2;
            }
            else if (DataBaseManager.NowPage_DetectiveOffice == 2)
            {
                if (countDetectiveOffice >= 6)
                {
                    Reset_DetectiveOffice();
                    detectiveOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                    detectiveOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countDetectiveOffice >= 5)
                {
                    Reset_DetectiveOffice();
                    detectiveOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_DetectiveOffice = 3;
            }
        }
        if (nowPage == "Hospital")
        {
            if (DataBaseManager.NowPage_Hospital == 1)
            {
                if (countHospital >= 6)
                {
                    Reset_Hospital();
                    hospitalContents.transform.GetChild(2).gameObject.SetActive(true);
                    hospitalContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countHospital >= 5)
                {
                    Reset_Hospital();
                    hospitalContents.transform.GetChild(3).gameObject.SetActive(true);
                    hospitalContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countHospital >= 4)
                {
                    Reset_Hospital();
                    hospitalContents.transform.GetChild(4).gameObject.SetActive(true);
                    hospitalContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countHospital >= 3)
                {
                    Reset_Hospital();
                    hospitalContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Hospital = 2;
            }
            else if (DataBaseManager.NowPage_Hospital == 2)
            {
                if (countHospital >= 6)
                {
                    Reset_Hospital();
                    hospitalContents.transform.GetChild(4).gameObject.SetActive(true);
                    hospitalContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countHospital >= 5)
                {
                    Reset_Hospital();
                    hospitalContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Hospital = 3;
            }
        }
        if (nowPage == "Newspaper")
        {
            if (DataBaseManager.NowPage_Newspaper == 1)
            {
                if (countNewspaper >= 6)
                {
                    Reset_Newspaper();
                    newspaperContents.transform.GetChild(2).gameObject.SetActive(true);
                    newspaperContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countNewspaper >= 5)
                {
                    Reset_Newspaper();
                    newspaperContents.transform.GetChild(3).gameObject.SetActive(true);
                    newspaperContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countNewspaper >= 4)
                {
                    Reset_Newspaper();
                    newspaperContents.transform.GetChild(4).gameObject.SetActive(true);
                    newspaperContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countNewspaper >= 3)
                {
                    Reset_Newspaper();
                    newspaperContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Newspaper = 2;
            }
            else if (DataBaseManager.NowPage_Newspaper == 2)
            {
                if (countNewspaper >= 6)
                {
                    Reset_Newspaper();
                    newspaperContents.transform.GetChild(4).gameObject.SetActive(true);
                    newspaperContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countNewspaper >= 5)
                {
                    Reset_Newspaper();
                    newspaperContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Newspaper = 3;
            }
        }
        if (nowPage == "QuestHouse")
        {
            if (DataBaseManager.NowPage_QuestHouse == 1)
            {
                if (countQuestHouse >= 6)
                {
                    Reset_QuestHouse();
                    questHouseContents.transform.GetChild(2).gameObject.SetActive(true);
                    questHouseContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countQuestHouse >= 5)
                {
                    Reset_QuestHouse();
                    questHouseContents.transform.GetChild(3).gameObject.SetActive(true);
                    questHouseContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countQuestHouse >= 4)
                {
                    Reset_QuestHouse();
                    questHouseContents.transform.GetChild(4).gameObject.SetActive(true);
                    questHouseContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countQuestHouse >= 3)
                {
                    Reset_QuestHouse();
                    questHouseContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_QuestHouse = 2;
            }
            else if (DataBaseManager.NowPage_QuestHouse == 2)
            {
                if (countQuestHouse >= 6)
                {
                    Reset_QuestHouse();
                    questHouseContents.transform.GetChild(4).gameObject.SetActive(true);
                    questHouseContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countQuestHouse >= 5)
                {
                    Reset_QuestHouse();
                    questHouseContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_QuestHouse = 3;
            }
        }
        if (nowPage == "Station")
        {
            if (DataBaseManager.NowPage_Station == 1)
            {
                if (countStation >= 6)
                {
                    Reset_Station();
                    stationContents.transform.GetChild(2).gameObject.SetActive(true);
                    stationContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countStation >= 5)
                {
                    Reset_Station();
                    stationContents.transform.GetChild(3).gameObject.SetActive(true);
                    stationContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countStation >= 4)
                {
                    Reset_Station();
                    stationContents.transform.GetChild(4).gameObject.SetActive(true);
                    stationContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countStation >= 3)
                {
                    Reset_Station();
                    stationContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Station = 2;
            }
            else if (DataBaseManager.NowPage_Station == 2)
            {
                if (countStation >= 6)
                {
                    Reset_Station();
                    stationContents.transform.GetChild(4).gameObject.SetActive(true);
                    stationContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countStation >= 5)
                {
                    Reset_Station();
                    stationContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Station = 3;
            }
        }
        if (nowPage == "Sewer")
        {
            if (DataBaseManager.NowPage_Sewer == 1)
            {
                if (countSewer >= 6)
                {
                    Reset_Sewer();
                    sewerContents.transform.GetChild(2).gameObject.SetActive(true);
                    sewerContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSewer >= 5)
                {
                    Reset_Sewer();
                    sewerContents.transform.GetChild(3).gameObject.SetActive(true);
                    sewerContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countSewer >= 4)
                {
                    Reset_Sewer();
                    sewerContents.transform.GetChild(4).gameObject.SetActive(true);
                    sewerContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSewer >= 3)
                {
                    Reset_Sewer();
                    sewerContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Sewer = 2;
            }
            else if (DataBaseManager.NowPage_Sewer == 2)
            {
                if (countSewer >= 6)
                {
                    Reset_Sewer();
                    sewerContents.transform.GetChild(4).gameObject.SetActive(true);
                    sewerContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSewer >= 5)
                {
                    Reset_Sewer();
                    sewerContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Sewer = 3;
            }
        }
        if (nowPage == "University")
        {
            if (DataBaseManager.NowPage_University == 1)
            {
                if (countUniversity >= 6)
                {
                    Reset_University();
                    universityContents.transform.GetChild(2).gameObject.SetActive(true);
                    universityContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countUniversity >= 5)
                {
                    Reset_University();
                    universityContents.transform.GetChild(3).gameObject.SetActive(true);
                    universityContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countUniversity >= 4)
                {
                    Reset_University();
                    universityContents.transform.GetChild(4).gameObject.SetActive(true);
                    universityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countUniversity >= 3)
                {
                    Reset_University();
                    universityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_University = 2;
            }
            else if (DataBaseManager.NowPage_University == 2)
            {
                if (countUniversity >= 6)
                {
                    Reset_University();
                    universityContents.transform.GetChild(4).gameObject.SetActive(true);
                    universityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countUniversity >= 5)
                {
                    Reset_University();
                    universityContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_University = 3;
            }
        }
        if (nowPage == "Riverside")
        {
            if (DataBaseManager.NowPage_Riverside == 1)
            {
                if (Count_Riverside >= 6)
                {
                    Reset_Riverside();
                    riversideContents.transform.GetChild(2).gameObject.SetActive(true);
                    riversideContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 5)
                {
                    Reset_Riverside();
                    riversideContents.transform.GetChild(3).gameObject.SetActive(true);
                    riversideContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 4)
                {
                    Reset_Riverside();
                    riversideContents.transform.GetChild(4).gameObject.SetActive(true);
                    riversideContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 3)
                {
                    Reset_Riverside();
                    riversideContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Riverside = 2;
            }
            else if (DataBaseManager.NowPage_Riverside == 2)
            {
                if (Count_Riverside >= 6)
                {
                    Reset_Riverside();
                    riversideContents.transform.GetChild(4).gameObject.SetActive(true);
                    riversideContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 5)
                {
                    Reset_Riverside();
                    riversideContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Riverside = 3;
            }
        }
        if (nowPage == "Bar")
        {
            if (DataBaseManager.NowPage_Bar == 1)
            {
                if (countBar >= 6)
                {
                    Reset_Bar();
                    barContents.transform.GetChild(2).gameObject.SetActive(true);
                    barContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countBar >= 5)
                {
                    Reset_Bar();
                    barContents.transform.GetChild(3).gameObject.SetActive(true);
                    barContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countBar >= 4)
                {
                    Reset_Bar();
                    barContents.transform.GetChild(4).gameObject.SetActive(true);
                    barContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countBar >= 3)
                {
                    Reset_Bar();
                    barContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Bar = 2;
            }
            else if (DataBaseManager.NowPage_Bar == 2)
            {
                if (countBar >= 6)
                {
                    Reset_Bar();
                    barContents.transform.GetChild(4).gameObject.SetActive(true);
                    barContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countBar >= 5)
                {
                    Reset_Bar();
                    barContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Bar = 3;
            }
        }
        if (nowPage == "Slum")
        {
            if (DataBaseManager.NowPage_Slum == 1)
            {
                if (countSlum >= 6)
                {
                    Reset_Slum();
                    slumContents.transform.GetChild(2).gameObject.SetActive(true);
                    slumContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSlum >= 5)
                {
                    Reset_Slum();
                    slumContents.transform.GetChild(3).gameObject.SetActive(true);
                    slumContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countSlum >= 4)
                {
                    Reset_Slum();
                    slumContents.transform.GetChild(4).gameObject.SetActive(true);
                    slumContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSlum >= 3)
                {
                    Reset_Slum();
                    slumContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Slum = 2;
            }
            else if (DataBaseManager.NowPage_Slum == 2)
            {
                if (countSlum >= 6)
                {
                    Reset_Slum();
                    slumContents.transform.GetChild(4).gameObject.SetActive(true);
                    slumContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSlum >= 5)
                {
                    Reset_Slum();
                    slumContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Slum = 3;
            }
        }
        if (nowPage == "PoliceOffice")
        {
            if (DataBaseManager.NowPage_PoliceOffice == 1)
            {
                if (countPoliceOffice >= 6)
                {
                    Reset_PoliceOffice();
                    policeOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    policeOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countPoliceOffice >= 5)
                {
                    Reset_PoliceOffice();
                    policeOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    policeOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countPoliceOffice >= 4)
                {
                    Reset_PoliceOffice();
                    policeOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                    policeOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countPoliceOffice >= 3)
                {
                    Reset_PoliceOffice();
                    policeOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_PoliceOffice = 2;
            }
            else if (DataBaseManager.NowPage_PoliceOffice == 2)
            {
                if (countPoliceOffice >= 6)
                {
                    Reset_PoliceOffice();
                    policeOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                    policeOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countPoliceOffice >= 5)
                {
                    Reset_PoliceOffice();
                    policeOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_PoliceOffice = 3;
            }
        }
        if (nowPage == "Gunsmith")
        {
            if (DataBaseManager.NowPage_Gunsmith == 1)
            {
                if (countGunsmith >= 6)
                {
                    Reset_Gunsmith();
                    gunsmithContents.transform.GetChild(2).gameObject.SetActive(true);
                    gunsmithContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countGunsmith >= 5)
                {
                    Reset_Gunsmith();
                    gunsmithContents.transform.GetChild(3).gameObject.SetActive(true);
                    gunsmithContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countGunsmith >= 4)
                {
                    Reset_Gunsmith();
                    gunsmithContents.transform.GetChild(4).gameObject.SetActive(true);
                    gunsmithContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countGunsmith >= 3)
                {
                    Reset_Gunsmith();
                    gunsmithContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Gunsmith = 2;
            }
            else if (DataBaseManager.NowPage_Gunsmith == 2)
            {
                if (countGunsmith >= 6)
                {
                    Reset_Gunsmith();
                    gunsmithContents.transform.GetChild(4).gameObject.SetActive(true);
                    gunsmithContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countGunsmith >= 5)
                {
                    Reset_Gunsmith();
                    gunsmithContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Gunsmith = 3;
            }
        }
        if (nowPage == "SewageMaintenanceOffice")
        {
            if (DataBaseManager.NowPage_SewageMaintenanceOffice == 1)
            {
                if (countSewageMaintenanceOffice >= 6)
                {
                    Reset_SewageMaintenanceOffice();
                    sewageMaintenanceOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    sewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSewageMaintenanceOffice >= 5)
                {
                    Reset_SewageMaintenanceOffice();
                    sewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    sewageMaintenanceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countSewageMaintenanceOffice >= 4)
                {
                    Reset_SewageMaintenanceOffice();
                    sewageMaintenanceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                    sewageMaintenanceOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSewageMaintenanceOffice >= 3)
                {
                    Reset_SewageMaintenanceOffice();
                    sewageMaintenanceOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewageMaintenanceOffice = 2;
            }
            else if (DataBaseManager.NowPage_SewageMaintenanceOffice == 2)
            {
                if (countSewageMaintenanceOffice >= 6)
                {
                    Reset_SewageMaintenanceOffice();
                    sewageMaintenanceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                    sewageMaintenanceOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSewageMaintenanceOffice >= 5)
                {
                    Reset_SewageMaintenanceOffice();
                    sewageMaintenanceOfficeContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewageMaintenanceOffice = 3;
            }
        }
        if (nowPage == "Wharf")
        {
            if (DataBaseManager.NowPage_Wharf == 1)
            {
                if (countWharf >= 6)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(2).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countWharf >= 5)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(3).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countWharf >= 4)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(4).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countWharf >= 3)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Wharf = 2;
            }
            else if (DataBaseManager.NowPage_Wharf == 2)
            {
                if (countWharf >= 6)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(4).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countWharf >= 5)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Wharf = 3;
            }
        }
    }
    public void PrevPage()
    {
        if (nowPage == "DetectiveOffice")
        {
            if (DataBaseManager.NowPage_DetectiveOffice == 2)
            {
                if (countDetectiveOffice >= 6)
                {
                    Reset_DetectiveOffice();
                    detectiveOfficeContents.transform.GetChild(0).gameObject.SetActive(true);
                    detectiveOfficeContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countDetectiveOffice >= 5)
                {
                    Reset_DetectiveOffice();
                    detectiveOfficeContents.transform.GetChild(1).gameObject.SetActive(true);
                    detectiveOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countDetectiveOffice >= 4)
                {
                    Reset_DetectiveOffice();
                    detectiveOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    detectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countDetectiveOffice >= 3)
                {
                    Reset_DetectiveOffice();
                    detectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    detectiveOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_DetectiveOffice = 1;
            }
            else if (DataBaseManager.NowPage_DetectiveOffice == 3)
            {
                if (countDetectiveOffice >= 6)
                {
                    Reset_DetectiveOffice();
                    detectiveOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    detectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countDetectiveOffice >= 5)
                {
                    Reset_DetectiveOffice();
                    detectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    detectiveOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_DetectiveOffice = 2;
            }
        }
        if (nowPage == "Hospital")
        {
            if (DataBaseManager.NowPage_Hospital == 2)
            {
                if (countHospital >= 6)
                {
                    Reset_Hospital();
                    hospitalContents.transform.GetChild(0).gameObject.SetActive(true);
                    hospitalContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countHospital >= 5)
                {
                    Reset_Hospital();
                    hospitalContents.transform.GetChild(1).gameObject.SetActive(true);
                    hospitalContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countHospital >= 4)
                {
                    Reset_Hospital();
                    hospitalContents.transform.GetChild(2).gameObject.SetActive(true);
                    hospitalContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countHospital >= 3)
                {
                    Reset_Hospital();
                    hospitalContents.transform.GetChild(3).gameObject.SetActive(true);
                    hospitalContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Hospital = 1;
            }
            else if (DataBaseManager.NowPage_Hospital == 3)
            {
                if (countHospital >= 6)
                {
                    Reset_Hospital();
                    hospitalContents.transform.GetChild(2).gameObject.SetActive(true);
                    hospitalContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countHospital >= 5)
                {
                    Reset_Hospital();
                    hospitalContents.transform.GetChild(3).gameObject.SetActive(true);
                    hospitalContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Hospital = 2;
            }
        }
        if (nowPage == "Newspaper")
        {
            if (DataBaseManager.NowPage_Newspaper == 2)
            {
                if (countNewspaper >= 6)
                {
                    Reset_Newspaper();
                    newspaperContents.transform.GetChild(0).gameObject.SetActive(true);
                    newspaperContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countNewspaper >= 5)
                {
                    Reset_Newspaper();
                    newspaperContents.transform.GetChild(1).gameObject.SetActive(true);
                    newspaperContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countNewspaper >= 4)
                {
                    Reset_Newspaper();
                    newspaperContents.transform.GetChild(2).gameObject.SetActive(true);
                    newspaperContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countNewspaper >= 3)
                {
                    Reset_Newspaper();
                    newspaperContents.transform.GetChild(3).gameObject.SetActive(true);
                    newspaperContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Newspaper = 1;
            }
            else if (DataBaseManager.NowPage_Newspaper == 3)
            {
                if (countNewspaper >= 6)
                {
                    Reset_Newspaper();
                    newspaperContents.transform.GetChild(2).gameObject.SetActive(true);
                    newspaperContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countNewspaper >= 5)
                {
                    Reset_Newspaper();
                    newspaperContents.transform.GetChild(3).gameObject.SetActive(true);
                    newspaperContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Newspaper = 2;
            }
        }
        if (nowPage == "QuestHouse")
        {
            if (DataBaseManager.NowPage_QuestHouse == 2)
            {
                if (countQuestHouse >= 6)
                {
                    Reset_QuestHouse();
                    questHouseContents.transform.GetChild(0).gameObject.SetActive(true);
                    questHouseContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countQuestHouse >= 5)
                {
                    Reset_QuestHouse();
                    questHouseContents.transform.GetChild(1).gameObject.SetActive(true);
                    questHouseContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countQuestHouse >= 4)
                {
                    Reset_QuestHouse();
                    questHouseContents.transform.GetChild(2).gameObject.SetActive(true);
                    questHouseContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countQuestHouse >= 3)
                {
                    Reset_QuestHouse();
                    questHouseContents.transform.GetChild(3).gameObject.SetActive(true);
                    questHouseContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_QuestHouse = 1;
            }
            else if (DataBaseManager.NowPage_QuestHouse == 3)
            {
                if (countQuestHouse >= 6)
                {
                    Reset_QuestHouse();
                    questHouseContents.transform.GetChild(2).gameObject.SetActive(true);
                    questHouseContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countQuestHouse >= 5)
                {
                    Reset_QuestHouse();
                    questHouseContents.transform.GetChild(3).gameObject.SetActive(true);
                    questHouseContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_QuestHouse = 2;
            }
        }
        if (nowPage == "Station")
        {
            if (DataBaseManager.NowPage_Station == 2)
            {
                if (countStation >= 6)
                {
                    Reset_Station();
                    stationContents.transform.GetChild(0).gameObject.SetActive(true);
                    stationContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countStation >= 5)
                {
                    Reset_Station();
                    stationContents.transform.GetChild(1).gameObject.SetActive(true);
                    stationContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countStation >= 4)
                {
                    Reset_Station();
                    stationContents.transform.GetChild(2).gameObject.SetActive(true);
                    stationContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countStation >= 3)
                {
                    Reset_Station();
                    stationContents.transform.GetChild(3).gameObject.SetActive(true);
                    stationContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Station = 1;
            }
            else if (DataBaseManager.NowPage_Station == 3)
            {
                if (countStation >= 6)
                {
                    Reset_Station();
                    stationContents.transform.GetChild(2).gameObject.SetActive(true);
                    stationContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countStation >= 5)
                {
                    Reset_Station();
                    stationContents.transform.GetChild(3).gameObject.SetActive(true);
                    stationContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Station = 2;
            }
        }
        if (nowPage == "Sewer")
        {
            if (DataBaseManager.NowPage_Sewer == 2)
            {
                if (countSewer >= 6)
                {
                    Reset_Sewer();
                    sewerContents.transform.GetChild(0).gameObject.SetActive(true);
                    sewerContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countSewer >= 5)
                {
                    Reset_Sewer();
                    sewerContents.transform.GetChild(1).gameObject.SetActive(true);
                    sewerContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countSewer >= 4)
                {
                    Reset_Sewer();
                    sewerContents.transform.GetChild(2).gameObject.SetActive(true);
                    sewerContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSewer >= 3)
                {
                    Reset_Sewer();
                    sewerContents.transform.GetChild(3).gameObject.SetActive(true);
                    sewerContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Sewer = 1;
            }
            else if (DataBaseManager.NowPage_Sewer == 3)
            {
                if (countSewer >= 6)
                {
                    Reset_Sewer();
                    sewerContents.transform.GetChild(2).gameObject.SetActive(true);
                    sewerContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSewer >= 5)
                {
                    Reset_Sewer();
                    sewerContents.transform.GetChild(3).gameObject.SetActive(true);
                    sewerContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Sewer = 2;
            }
        }
        if (nowPage == "University")
        {
            if (DataBaseManager.NowPage_University == 2)
            {
                if (countUniversity >= 6)
                {
                    Reset_University();
                    universityContents.transform.GetChild(0).gameObject.SetActive(true);
                    universityContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countUniversity >= 5)
                {
                    Reset_University();
                    universityContents.transform.GetChild(1).gameObject.SetActive(true);
                    universityContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countUniversity >= 4)
                {
                    Reset_University();
                    universityContents.transform.GetChild(2).gameObject.SetActive(true);
                    universityContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countUniversity >= 3)
                {
                    Reset_University();
                    universityContents.transform.GetChild(3).gameObject.SetActive(true);
                    universityContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_University = 1;
            }
            else if (DataBaseManager.NowPage_University == 3)
            {
                if (countUniversity >= 6)
                {
                    Reset_University();
                    universityContents.transform.GetChild(2).gameObject.SetActive(true);
                    universityContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countUniversity >= 5)
                {
                    Reset_University();
                    universityContents.transform.GetChild(3).gameObject.SetActive(true);
                    universityContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_University = 2;
            }
        }
        if (nowPage == "Riverside")
        {
            if (DataBaseManager.NowPage_Riverside == 2)
            {
                if (Count_Riverside >= 6)
                {
                    Reset_Riverside();
                    riversideContents.transform.GetChild(0).gameObject.SetActive(true);
                    riversideContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 5)
                {
                    Reset_Riverside();
                    riversideContents.transform.GetChild(1).gameObject.SetActive(true);
                    riversideContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 4)
                {
                    Reset_Riverside();
                    riversideContents.transform.GetChild(2).gameObject.SetActive(true);
                    riversideContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 3)
                {
                    Reset_Riverside();
                    riversideContents.transform.GetChild(3).gameObject.SetActive(true);
                    riversideContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Riverside = 1;
            }
            else if (DataBaseManager.NowPage_Riverside == 3)
            {
                if (Count_Riverside >= 6)
                {
                    Reset_Riverside();
                    riversideContents.transform.GetChild(2).gameObject.SetActive(true);
                    riversideContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Riverside >= 5)
                {
                    Reset_Riverside();
                    riversideContents.transform.GetChild(3).gameObject.SetActive(true);
                    riversideContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Riverside = 2;
            }
        }
        if (nowPage == "Bar")
        {
            if (DataBaseManager.NowPage_Bar == 2)
            {
                if (countBar >= 6)
                {
                    Reset_Bar();
                    barContents.transform.GetChild(0).gameObject.SetActive(true);
                    barContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countBar >= 5)
                {
                    Reset_Bar();
                    barContents.transform.GetChild(1).gameObject.SetActive(true);
                    barContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countBar >= 4)
                {
                    Reset_Bar();
                    barContents.transform.GetChild(2).gameObject.SetActive(true);
                    barContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countBar >= 3)
                {
                    Reset_Bar();
                    barContents.transform.GetChild(3).gameObject.SetActive(true);
                    barContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Bar = 1;
            }
            else if (DataBaseManager.NowPage_Bar == 3)
            {
                if (countBar >= 6)
                {
                    Reset_Bar();
                    barContents.transform.GetChild(2).gameObject.SetActive(true);
                    barContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countBar >= 5)
                {
                    Reset_Bar();
                    barContents.transform.GetChild(3).gameObject.SetActive(true);
                    barContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Bar = 2;
            }
        }
        if (nowPage == "Slum")
        {
            if (DataBaseManager.NowPage_Slum == 2)
            {
                if (countSlum >= 6)
                {
                    Reset_Slum();
                    slumContents.transform.GetChild(0).gameObject.SetActive(true);
                    slumContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countSlum >= 5)
                {
                    Reset_Slum();
                    slumContents.transform.GetChild(1).gameObject.SetActive(true);
                    slumContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countSlum >= 4)
                {
                    Reset_Slum();
                    slumContents.transform.GetChild(2).gameObject.SetActive(true);
                    slumContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSlum >= 3)
                {
                    Reset_Slum();
                    slumContents.transform.GetChild(3).gameObject.SetActive(true);
                    slumContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Slum = 1;
            }
            else if (DataBaseManager.NowPage_Slum == 3)
            {
                if (countSlum >= 6)
                {
                    Reset_Slum();
                    slumContents.transform.GetChild(2).gameObject.SetActive(true);
                    slumContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSlum >= 5)
                {
                    Reset_Slum();
                    slumContents.transform.GetChild(3).gameObject.SetActive(true);
                    slumContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Slum = 2;
            }
        }
        if (nowPage == "PoliceOffice")
        {
            if (DataBaseManager.NowPage_PoliceOffice == 2)
            {
                if (countPoliceOffice >= 6)
                {
                    Reset_PoliceOffice();
                    policeOfficeContents.transform.GetChild(0).gameObject.SetActive(true);
                    policeOfficeContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countPoliceOffice >= 5)
                {
                    Reset_PoliceOffice();
                    policeOfficeContents.transform.GetChild(1).gameObject.SetActive(true);
                    policeOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countPoliceOffice >= 4)
                {
                    Reset_PoliceOffice();
                    policeOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    policeOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countPoliceOffice >= 3)
                {
                    Reset_PoliceOffice();
                    policeOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    policeOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_PoliceOffice = 1;
            }
            else if (DataBaseManager.NowPage_PoliceOffice == 3)
            {
                if (countPoliceOffice >= 6)
                {
                    Reset_PoliceOffice();
                    policeOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    policeOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countPoliceOffice >= 5)
                {
                    Reset_PoliceOffice();
                    policeOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    policeOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_PoliceOffice = 2;
            }
        }
        if (nowPage == "Gunsmith")
        {
            if (DataBaseManager.NowPage_Gunsmith == 2)
            {
                if (countGunsmith >= 6)
                {
                    Reset_Gunsmith();
                    gunsmithContents.transform.GetChild(0).gameObject.SetActive(true);
                    gunsmithContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countGunsmith >= 5)
                {
                    Reset_Gunsmith();
                    gunsmithContents.transform.GetChild(1).gameObject.SetActive(true);
                    gunsmithContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countGunsmith >= 4)
                {
                    Reset_Gunsmith();
                    gunsmithContents.transform.GetChild(2).gameObject.SetActive(true);
                    gunsmithContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countGunsmith >= 3)
                {
                    Reset_Gunsmith();
                    gunsmithContents.transform.GetChild(3).gameObject.SetActive(true);
                    gunsmithContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Gunsmith = 1;
            }
            else if (DataBaseManager.NowPage_Gunsmith == 3)
            {
                if (countGunsmith >= 6)
                {
                    Reset_Gunsmith();
                    gunsmithContents.transform.GetChild(2).gameObject.SetActive(true);
                    gunsmithContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countGunsmith >= 5)
                {
                    Reset_Gunsmith();
                    gunsmithContents.transform.GetChild(3).gameObject.SetActive(true);
                    gunsmithContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Gunsmith = 2;
            }
        }
        if (nowPage == "SewageMaintenanceOffice")
        {
            if (DataBaseManager.NowPage_SewageMaintenanceOffice == 2)
            {
                if (countSewageMaintenanceOffice >= 6)
                {
                    Reset_SewageMaintenanceOffice();
                    sewageMaintenanceOfficeContents.transform.GetChild(0).gameObject.SetActive(true);
                    sewageMaintenanceOfficeContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countSewageMaintenanceOffice >= 5)
                {
                    Reset_SewageMaintenanceOffice();
                    sewageMaintenanceOfficeContents.transform.GetChild(1).gameObject.SetActive(true);
                    sewageMaintenanceOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countSewageMaintenanceOffice >= 4)
                {
                    Reset_SewageMaintenanceOffice();
                    sewageMaintenanceOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    sewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSewageMaintenanceOffice >= 3)
                {
                    Reset_SewageMaintenanceOffice();
                    sewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    sewageMaintenanceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_SewageMaintenanceOffice = 1;
            }
            else if (DataBaseManager.NowPage_SewageMaintenanceOffice == 3)
            {
                if (countSewageMaintenanceOffice >= 6)
                {
                    Reset_SewageMaintenanceOffice();
                    sewageMaintenanceOfficeContents.transform.GetChild(2).gameObject.SetActive(true);
                    sewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSewageMaintenanceOffice >= 5)
                {
                    Reset_SewageMaintenanceOffice();
                    sewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(true);
                    sewageMaintenanceOfficeContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewageMaintenanceOffice = 2;
            }
        }
        if (nowPage == "Wharf")
        {
            if (DataBaseManager.NowPage_Wharf == 2)
            {
                if (countWharf >= 6)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(0).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countWharf >= 5)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(1).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countWharf >= 4)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(2).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countWharf >= 3)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(3).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Wharf = 1;
            }
            else if (DataBaseManager.NowPage_Wharf == 3)
            {
                if (countWharf >= 6)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(2).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countWharf >= 5)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(3).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Wharf = 2;
            }
        }
    }
    void Reset_DetectiveOffice()
    {
        detectiveOfficeContents.transform.GetChild(0).gameObject.SetActive(false);
        detectiveOfficeContents.transform.GetChild(1).gameObject.SetActive(false);
        detectiveOfficeContents.transform.GetChild(2).gameObject.SetActive(false);
        detectiveOfficeContents.transform.GetChild(3).gameObject.SetActive(false);
        detectiveOfficeContents.transform.GetChild(4).gameObject.SetActive(false);
        detectiveOfficeContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Hospital()
    {
        hospitalContents.transform.GetChild(0).gameObject.SetActive(false);
        hospitalContents.transform.GetChild(1).gameObject.SetActive(false);
        hospitalContents.transform.GetChild(2).gameObject.SetActive(false);
        hospitalContents.transform.GetChild(3).gameObject.SetActive(false);
        hospitalContents.transform.GetChild(4).gameObject.SetActive(false);
        hospitalContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Newspaper()
    {
        newspaperContents.transform.GetChild(0).gameObject.SetActive(false);
        newspaperContents.transform.GetChild(1).gameObject.SetActive(false);
        newspaperContents.transform.GetChild(2).gameObject.SetActive(false);
        newspaperContents.transform.GetChild(3).gameObject.SetActive(false);
        newspaperContents.transform.GetChild(4).gameObject.SetActive(false);
        newspaperContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_QuestHouse()
    {
        questHouseContents.transform.GetChild(0).gameObject.SetActive(false);
        questHouseContents.transform.GetChild(1).gameObject.SetActive(false);
        questHouseContents.transform.GetChild(2).gameObject.SetActive(false);
        questHouseContents.transform.GetChild(3).gameObject.SetActive(false);
        questHouseContents.transform.GetChild(4).gameObject.SetActive(false);
        questHouseContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Station()
    {
        stationContents.transform.GetChild(0).gameObject.SetActive(false);
        stationContents.transform.GetChild(1).gameObject.SetActive(false);
        stationContents.transform.GetChild(2).gameObject.SetActive(false);
        stationContents.transform.GetChild(3).gameObject.SetActive(false);
        stationContents.transform.GetChild(4).gameObject.SetActive(false);
        stationContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Sewer()
    {
        sewerContents.transform.GetChild(0).gameObject.SetActive(false);
        sewerContents.transform.GetChild(1).gameObject.SetActive(false);
        sewerContents.transform.GetChild(2).gameObject.SetActive(false);
        sewerContents.transform.GetChild(3).gameObject.SetActive(false);
        sewerContents.transform.GetChild(4).gameObject.SetActive(false);
        sewerContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_University()
    {
        universityContents.transform.GetChild(0).gameObject.SetActive(false);
        universityContents.transform.GetChild(1).gameObject.SetActive(false);
        universityContents.transform.GetChild(2).gameObject.SetActive(false);
        universityContents.transform.GetChild(3).gameObject.SetActive(false);
        universityContents.transform.GetChild(4).gameObject.SetActive(false);
        universityContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Riverside()
    {
        riversideContents.transform.GetChild(0).gameObject.SetActive(false);
        riversideContents.transform.GetChild(1).gameObject.SetActive(false);
        riversideContents.transform.GetChild(2).gameObject.SetActive(false);
        riversideContents.transform.GetChild(3).gameObject.SetActive(false);
        riversideContents.transform.GetChild(4).gameObject.SetActive(false);
        riversideContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Bar()
    {
        barContents.transform.GetChild(0).gameObject.SetActive(false);
        barContents.transform.GetChild(1).gameObject.SetActive(false);
        barContents.transform.GetChild(2).gameObject.SetActive(false);
        barContents.transform.GetChild(3).gameObject.SetActive(false);
        barContents.transform.GetChild(4).gameObject.SetActive(false);
        barContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Slum()
    {
        slumContents.transform.GetChild(0).gameObject.SetActive(false);
        slumContents.transform.GetChild(1).gameObject.SetActive(false);
        slumContents.transform.GetChild(2).gameObject.SetActive(false);
        slumContents.transform.GetChild(3).gameObject.SetActive(false);
        slumContents.transform.GetChild(4).gameObject.SetActive(false);
        slumContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_PoliceOffice()
    {
        policeOfficeContents.transform.GetChild(0).gameObject.SetActive(false);
        policeOfficeContents.transform.GetChild(1).gameObject.SetActive(false);
        policeOfficeContents.transform.GetChild(2).gameObject.SetActive(false);
        policeOfficeContents.transform.GetChild(3).gameObject.SetActive(false);
        policeOfficeContents.transform.GetChild(4).gameObject.SetActive(false);
        policeOfficeContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Gunsmith()
    {
        gunsmithContents.transform.GetChild(0).gameObject.SetActive(false);
        gunsmithContents.transform.GetChild(1).gameObject.SetActive(false);
        gunsmithContents.transform.GetChild(2).gameObject.SetActive(false);
        gunsmithContents.transform.GetChild(3).gameObject.SetActive(false);
        gunsmithContents.transform.GetChild(4).gameObject.SetActive(false);
        gunsmithContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_SewageMaintenanceOffice()
    {
        sewageMaintenanceOfficeContents.transform.GetChild(0).gameObject.SetActive(false);
        sewageMaintenanceOfficeContents.transform.GetChild(1).gameObject.SetActive(false);
        sewageMaintenanceOfficeContents.transform.GetChild(2).gameObject.SetActive(false);
        sewageMaintenanceOfficeContents.transform.GetChild(3).gameObject.SetActive(false);
        sewageMaintenanceOfficeContents.transform.GetChild(4).gameObject.SetActive(false);
        sewageMaintenanceOfficeContents.transform.GetChild(5).gameObject.SetActive(false);
    }

    void Reset_Wharf()
    {
        wharfContents.transform.GetChild(0).gameObject.SetActive(false);
        wharfContents.transform.GetChild(1).gameObject.SetActive(false);
        wharfContents.transform.GetChild(2).gameObject.SetActive(false);
        wharfContents.transform.GetChild(3).gameObject.SetActive(false);
        wharfContents.transform.GetChild(4).gameObject.SetActive(false);
        wharfContents.transform.GetChild(5).gameObject.SetActive(false);
    }

    public void Open_DetectiveOffice()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        detectiveOfficeDetail.SetActive(true);
    }
    public void Open_Hospital()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        hospitalDetail.SetActive(true);
    }
    public void Open_Newspaper()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        newspaperDetail.SetActive(true);
    }
    public void Open_QuestHouse()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        questHouseDetail.SetActive(true);
    }
    public void Open_Station()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        stationDetail.SetActive(true);
    }
    public void Open_Sewer()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        sewerDetail.SetActive(true);
    }
    public void Open_University()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        universityDetail.SetActive(true);
    }
    public void Open_Riverside()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        riversideDetail.SetActive(true);
    }
    public void Open_Bar()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        barDetail.SetActive(true);
    }
    public void Open_Slum()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        slumDetail.SetActive(true);
    }
    public void Open_PoliceOffice()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        policeOfficeDetail.SetActive(true);
    }
    public void Open_Gunsmith()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        gunsmithDetail.SetActive(true);
    }
    public void Open_SewageMaintenanceOffice()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        sewageMaintenanceOfficeDetail.SetActive(true);
    }
    public void Open_Wharf()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        wharfDetail.SetActive(true);
    }
    void CloseAllContents()
    {
        detectiveOfficeDetail.SetActive(false);
        newspaperDetail.SetActive(false);
        hospitalDetail.SetActive(false);
        questHouseDetail.SetActive(false);
        stationDetail.SetActive(false);
        sewerDetail.SetActive(false);
        universityDetail.SetActive(false);
        riversideDetail.SetActive(false);
        barDetail.SetActive(false);
        slumDetail.SetActive(false);
        policeOfficeDetail.SetActive(false);
        gunsmithDetail.SetActive(false);
        sewageMaintenanceOfficeDetail.SetActive(false);
        wharfDetail.SetActive(false);
    }
}



