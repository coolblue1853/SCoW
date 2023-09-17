using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolletTest : MonoBehaviour
{

    // setRollet(string skill, string point_sting, int point_int)


    private void Awake()
    {


        /*
DataBaseManager.Axe += 1;
DataBaseManager.Bat += 1;
DataBaseManager.Dagger += 1;


DataBaseManager.SmallPistol += 1;
 DataBaseManager.Shotgun += 1;
 DataBaseManager.Rifle += 1;
DataBaseManager.Revolver += 1;


DataBaseManager.PistolAmmo = 20;
DataBaseManager.RifleAmmo += 10;
DataBaseManager.ShotgunAmmo += 10;

DataBaseManager.Molotov += 1;

DataBaseManager.dex = 999;


DataBaseManager.gunShotPoint = 50;

DataBaseManager.Intel_Aiden1 = true;
DataBaseManager.Intel_Safe1 = true;
DataBaseManager.Intel_Nightmare1 = true;
DataBaseManager.Intel_Nightmare2 = true;
DataBaseManager.Intel_Nightmare3 = true;
DataBaseManager.Intel_Nightmare4 = true;
DataBaseManager.Intel_Insomnia1 = true;
DataBaseManager.Intel_Insomnia2 =true;

DataBaseManager.Intel_PlanetarySequence1 = true;
DataBaseManager.Intel_University1 = true;
DataBaseManager.Intel_Meiv1 = true;
DataBaseManager.Intel_MissingPeople1 = true;
DataBaseManager.Intel_SewerGhostStory1 = true;
DataBaseManager.Intel_SewerGhostStory2 = true;
DataBaseManager.Intel_CreepyEyes1 = true;
DataBaseManager.Intel_SewerWorker1 = true;
DataBaseManager.Intel_FishySmell1 = true;
DataBaseManager.Intel_FishySmell2 = true;
DataBaseManager.Intel_FishySmell3 = true;

        DataBaseManager.nowPlace = "DetectiveOffice";
        //DataBaseManager.nowPlace = "DetectiveOffice";
        //   DataBaseManager.nowPlace = "Slum";
        //DataBaseManager.nowPlace = "University";

        // DataBaseManager.firstClientsHouse = true; 
        // DataBaseManager.Intel_Ella1 = true;
        //DataBaseManager.Intel_Aiden1 = true;

        //DataBaseManager.Intel_Nightmare1 = true;
        //DataBaseManager.Intel_Paranoia1 = true;
        //DataBaseManager.Intel_Newspaper1 = true;


        //DataBaseManager.Intel_Hospital1 = true;
       DataBaseManager.Condition = "Nomal";
       // DataBaseManager.martialArtsPoint = 60;
       // DataBaseManager.Condition = "Worst";


        */
        //DataBaseManager.TimeCount = 1;
        DataBaseManager.str = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.intl = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.dex = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.hp = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.mp = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.san = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.luk = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.weal = (Random.Range(5, 26));
        DataBaseManager.nowHP = DataBaseManager.hp;
        DataBaseManager.nowSan = DataBaseManager.san;
    }
    public void NewsRollet()
    {
        //BattleRollet.Instance.setBattleRollet("파비안 : 회피", "회피", 50, "evasion", "??? : 공격", "근접공격", 40);
        //Rollet.Instance.setRollet("신문 : 살펴보기", "자료조사", 55, "dialog");
    }
    private void Start()
    {


        //  DataBaseManager.weal = 99999;
        //    DataBaseManager.nowPlace = "BattleRoad";
        //  DataBaseManager.deftnessPoint =90;
        //   DataBaseManager.swordPoint = 90;
        //   DataBaseManager.martialArtsPoint = 90;
        //  DataBaseManager.gunShotPoint = 90;
        //  BattleManager.Instance.StartBattle = true;
    }
    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.F10))
        {
        }

            if (Input.GetKeyDown(KeyCode.F10))
        {
            DataBaseManager.Sewer_San = true;

            //   DataBaseManager.Shotgun += 1;
            // DataBaseManager.Rifle += 1;
            // DataBaseManager.Revolver += 1;
            // DataBaseManager.SmallPistol += 1;

            // DataBaseManager.PistolAmmo += 1;

        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            DataBaseManager.Masochism = true;
      
          //  DataBaseManager.Bandages += 1;
           // DataBaseManager.Intel_Safe2 = true;
            //DataBaseManager.Firstaidkit += 1;

        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            //BillowUIManager.Instance.HP_Battledown(5);
            BillowUIManager.Instance.HP_down(5);

        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
             DataBaseManager.Painkillers += 1;

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            

           // One();
           // Two();
          // Three();
          // Four();
           // Five();
           // Six();
            //  DirectingManager.Instance.OrganizeKeywordFunc();
            //DataBaseManager.카메라왼쪽워킹온오프 = true;
            //One();
            // NewsRollet();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {



        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {

        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            DataBaseManager.Condition = "Worst";

        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            DataBaseManager.Tightwad = true;
           // BillowUIManager.Instance.HP_down(5);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            BillowUIManager.Instance.HP_up(5);

        }
        if (Input.GetKeyDown(KeyCode.F4))
        {

            BillowUIManager.Instance.San_up(10);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {


        }
        if (Input.GetKeyDown(KeyCode.F6))
        {

        }

        if (Input.GetKeyDown(KeyCode.P))
        {

        }
    }

    public void One()
    {
        DataBaseManager.Intel_Ella1 = true;
        DataBaseManager.Intel_Aiden1 = true;
        DataBaseManager.Intel_Cane1 = true;
        DataBaseManager.Intel_Albert1 = true;
        DataBaseManager.Intel_Meiv1 = true;
        DataBaseManager.Intel_BlackWell1 = true;
        DataBaseManager.Intel_Swain1 = true;

        DataBaseManager.Intel_PlanetaryParade1 = true;
        DataBaseManager.Intel_Nightmare1 = true;
        DataBaseManager.Intel_Paranoia1 = true;
        DataBaseManager.Intel_Insomnia1 = true;
        DataBaseManager.Intel_SewerGhostStory1 = true;
        DataBaseManager.Intel_MissingPeople1 = true;
        DataBaseManager.Intel_FishySmell1 = true;
        DataBaseManager.Intel_RiverWaterQuality1 = true;
        DataBaseManager.Intel_CreepyEyes1 = true;
        DataBaseManager.Intel_SewerWorker1 = true;
        DataBaseManager.Intel_Safe1 = true;


        DataBaseManager.Intel_DetectiveOffice1 = true;
        DataBaseManager.Intel_Hospital1 = true;
        DataBaseManager.Intel_Newspaper1 = true;
        DataBaseManager.Intel_QuestHouse1 = true;
        DataBaseManager.Intel_Station1 = true;
        DataBaseManager.Intel_Restaurant1 = true;
        DataBaseManager.Intel_University1 = true;
        DataBaseManager.Intel_Riverside1 = true;
        DataBaseManager.Intel_Bar1 = true;
        DataBaseManager.Intel_Slum1 = true;
        DataBaseManager.Intel_PoliceOffice1 = true;
        DataBaseManager.Intel_Gunsmith1 = true;
        DataBaseManager.Intel_SewageMaintenanceOffice1 = true;
        DataBaseManager.Intel_Wharf1 = true;
    }
    public void Two()
    {
        DataBaseManager.Intel_Ella2 = true;
        DataBaseManager.Intel_Aiden2 = true;
        DataBaseManager.Intel_Cane2 = true;
        DataBaseManager.Intel_Albert2 = true;
        DataBaseManager.Intel_Meiv2 = true;
        DataBaseManager.Intel_BlackWell2 = true;
        DataBaseManager.Intel_Swain2 = true;

        DataBaseManager.Intel_PlanetaryParade2 = true;
        DataBaseManager.Intel_Nightmare2 = true;
        DataBaseManager.Intel_Paranoia2 = true;
        DataBaseManager.Intel_Insomnia2 = true;
        DataBaseManager.Intel_SewerGhostStory2 = true;
        DataBaseManager.Intel_MissingPeople2 = true;
        DataBaseManager.Intel_FishySmell2 = true;
        DataBaseManager.Intel_RiverWaterQuality2 = true;
        DataBaseManager.Intel_CreepyEyes2 = true;
        DataBaseManager.Intel_SewerWorker2 = true;
        DataBaseManager.Intel_Safe2 = true;


        DataBaseManager.Intel_DetectiveOffice2 = true;
        DataBaseManager.Intel_Hospital2 = true;
        DataBaseManager.Intel_Newspaper2 = true;
        DataBaseManager.Intel_QuestHouse2 = true;
        DataBaseManager.Intel_Station2 = true;
        DataBaseManager.Intel_Restaurant2 = true;
        DataBaseManager.Intel_University2 = true;
        DataBaseManager.Intel_Riverside2 = true;
        DataBaseManager.Intel_Bar2 = true;
        DataBaseManager.Intel_Slum2 = true;
        DataBaseManager.Intel_PoliceOffice2 = true;
        DataBaseManager.Intel_Gunsmith2 = true;
        DataBaseManager.Intel_SewageMaintenanceOffice2 = true;
        DataBaseManager.Intel_Wharf2 = true;


    }
    public void Three()
    {
        DataBaseManager.Intel_Ella3 = true;
        DataBaseManager.Intel_Aiden3 = true;
        DataBaseManager.Intel_Cane3 = true;
        DataBaseManager.Intel_Albert3 = true;
        DataBaseManager.Intel_Meiv3 = true;
        DataBaseManager.Intel_BlackWell3 = true;
        DataBaseManager.Intel_Swain3 = true;

        DataBaseManager.Intel_PlanetaryParade3 = true;
        DataBaseManager.Intel_Nightmare3 = true;
        DataBaseManager.Intel_Paranoia3 = true;
        DataBaseManager.Intel_Insomnia3 = true;
        DataBaseManager.Intel_SewerGhostStory3 = true;
        DataBaseManager.Intel_MissingPeople3 = true;
        DataBaseManager.Intel_FishySmell3 = true;
        DataBaseManager.Intel_RiverWaterQuality3 = true;
        DataBaseManager.Intel_CreepyEyes3 = true;
        DataBaseManager.Intel_SewerWorker3 = true;
        DataBaseManager.Intel_Safe3 = true;

        DataBaseManager.Intel_DetectiveOffice3 = true;
        DataBaseManager.Intel_Hospital3 = true;
        DataBaseManager.Intel_Newspaper3 = true;
        DataBaseManager.Intel_QuestHouse3 = true;
        DataBaseManager.Intel_Station3 = true;
        DataBaseManager.Intel_Restaurant3 = true;
        DataBaseManager.Intel_University3 = true;
        DataBaseManager.Intel_Riverside3 = true;
        DataBaseManager.Intel_Bar3 = true;
        DataBaseManager.Intel_Slum3 = true;
        DataBaseManager.Intel_PoliceOffice3 = true;
        DataBaseManager.Intel_Gunsmith3 = true;
        DataBaseManager.Intel_SewageMaintenanceOffice3 = true;
        DataBaseManager.Intel_Wharf3 = true;
    }
    public void Four()
    {
        DataBaseManager.Intel_Ella4 = true;
        DataBaseManager.Intel_Aiden4 = true;
        DataBaseManager.Intel_Cane4 = true;
        DataBaseManager.Intel_Albert4 = true;
        DataBaseManager.Intel_Meiv4 = true;
        DataBaseManager.Intel_BlackWell4 = true;
        DataBaseManager.Intel_Swain4 = true;

        DataBaseManager.Intel_PlanetaryParade4 = true;
        DataBaseManager.Intel_Nightmare4 = true;
        DataBaseManager.Intel_Paranoia4 = true;
        DataBaseManager.Intel_Insomnia4 = true;
        DataBaseManager.Intel_SewerGhostStory4 = true;
        DataBaseManager.Intel_MissingPeople4 = true;
        DataBaseManager.Intel_FishySmell4 = true;
        DataBaseManager.Intel_RiverWaterQuality4 = true;
        DataBaseManager.Intel_CreepyEyes4 = true;
        DataBaseManager.Intel_SewerWorker4 = true;
        DataBaseManager.Intel_Safe4 = true;

        DataBaseManager.Intel_DetectiveOffice4 = true;
        DataBaseManager.Intel_Hospital4 = true;
        DataBaseManager.Intel_Newspaper4 = true;
        DataBaseManager.Intel_QuestHouse4 = true;
        DataBaseManager.Intel_Station4 = true;
        DataBaseManager.Intel_Restaurant4 = true;
        DataBaseManager.Intel_University4 = true;
        DataBaseManager.Intel_Riverside4 = true;
        DataBaseManager.Intel_Bar4 = true;
        DataBaseManager.Intel_Slum4 = true;
        DataBaseManager.Intel_PoliceOffice4 = true;
        DataBaseManager.Intel_Gunsmith4 = true;
        DataBaseManager.Intel_SewageMaintenanceOffice4 = true;
        DataBaseManager.Intel_Wharf4 = true;
    }
    public void Five()
    {
        DataBaseManager.Intel_Ella5 = true;
        DataBaseManager.Intel_Aiden5 = true;
        DataBaseManager.Intel_Cane5 = true;
        DataBaseManager.Intel_Albert5 = true;
        DataBaseManager.Intel_Meiv5 = true;
        DataBaseManager.Intel_BlackWell5 = true;
        DataBaseManager.Intel_Swain5 = true;

        DataBaseManager.Intel_PlanetaryParade5 = true;
        DataBaseManager.Intel_Nightmare5 = true;
        DataBaseManager.Intel_Paranoia5 = true;
        DataBaseManager.Intel_Insomnia5 = true;
        DataBaseManager.Intel_SewerGhostStory5 = true;
        DataBaseManager.Intel_MissingPeople5 = true;
        DataBaseManager.Intel_FishySmell5 = true;
        DataBaseManager.Intel_RiverWaterQuality5 = true;
        DataBaseManager.Intel_CreepyEyes5 = true;
        DataBaseManager.Intel_SewerWorker5 = true;
        DataBaseManager.Intel_Safe5 = true;


        DataBaseManager.Intel_DetectiveOffice5 = true;
        DataBaseManager.Intel_Hospital5 = true;
        DataBaseManager.Intel_Newspaper5 = true;
        DataBaseManager.Intel_QuestHouse5 = true;
        DataBaseManager.Intel_Station5 = true;
        DataBaseManager.Intel_Restaurant5 = true;
        DataBaseManager.Intel_University5 = true;
        DataBaseManager.Intel_Riverside5 = true;
        DataBaseManager.Intel_Bar5 = true;
        DataBaseManager.Intel_Slum5 = true;
        DataBaseManager.Intel_PoliceOffice5 = true;
        DataBaseManager.Intel_Gunsmith5 = true;
        DataBaseManager.Intel_SewageMaintenanceOffice5 = true;
        DataBaseManager.Intel_Wharf5 = true;
    }

    public void Six()
    {
        DataBaseManager.Intel_Ella6 = true;
        DataBaseManager.Intel_Aiden6 = true;
        DataBaseManager.Intel_Cane6 = true;
        DataBaseManager.Intel_Albert6 = true;
        DataBaseManager.Intel_Meiv6 = true;
        DataBaseManager.Intel_BlackWell6 = true;
        DataBaseManager.Intel_Swain6 = true;

        DataBaseManager.Intel_PlanetaryParade6 = true;
        DataBaseManager.Intel_Nightmare6 = true;
        DataBaseManager.Intel_Paranoia6 = true;
        DataBaseManager.Intel_Insomnia6 = true;
        DataBaseManager.Intel_SewerGhostStory6 = true;
        DataBaseManager.Intel_MissingPeople6 = true;
        DataBaseManager.Intel_FishySmell6 = true;
        DataBaseManager.Intel_RiverWaterQuality6 = true;
        DataBaseManager.Intel_CreepyEyes6 = true;
        DataBaseManager.Intel_SewerWorker6 = true;
        DataBaseManager.Intel_Safe6 = true;


        DataBaseManager.Intel_DetectiveOffice6 = true;
        DataBaseManager.Intel_Hospital6 = true;
        DataBaseManager.Intel_Newspaper6 = true;
        DataBaseManager.Intel_QuestHouse6 = true;
        DataBaseManager.Intel_Station6 = true;
        DataBaseManager.Intel_Restaurant6 = true;
        DataBaseManager.Intel_University6 = true;
        DataBaseManager.Intel_Riverside6 = true;
        DataBaseManager.Intel_Bar6 = true;
        DataBaseManager.Intel_Slum6 = true;
        DataBaseManager.Intel_PoliceOffice6 = true;
        DataBaseManager.Intel_Gunsmith6 = true;
        DataBaseManager.Intel_SewageMaintenanceOffice6 = true;
        DataBaseManager.Intel_Wharf6 = true;
    }

}