using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolletTest : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;

    // setRollet(string skill, string point_sting, int point_int)

    Vector2 playerSecretHouse = new Vector3(-1291.44f, -194.02f);
    Vector3 camSecretHouse = new Vector3(-1303f, -195f, -15);

    Vector2 playerSymbol = new Vector3(-1203.16f, -192.8f);
    Vector3 camSymbol = new Vector3(-1217.2f, -195f, -15);
    private void Awake()
    {

        DataBaseManager.intl = 99;
        DataBaseManager.str = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.dex = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.hp = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.mp = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.san = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.luk = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.weal = (Random.Range(5, 26));
        DataBaseManager.nowHP = DataBaseManager.hp;
        DataBaseManager.nowSan = DataBaseManager.san;
        DataBaseManager.intelQuestHouse1 = true;
        DataBaseManager.rhetoricPoint = 80;


        /*
        DataBaseManager.Intel_MurderCase1 = true;
        DataBaseManager.Intel_MurderCase2 = true;
        DataBaseManager.Intel_MurderCase3 = true;
        DataBaseManager.Intel_MurderCase4 = true;
        DataBaseManager.Intel_MurderCase5 = true;
        DataBaseManager.Intel_MurderCase6 = true;

        DataBaseManager.Intel_Dave1 = true;
        DataBaseManager.Intel_Dave2 = true;
        DataBaseManager.Intel_Dave3 = true;
        DataBaseManager.Intel_Dave4 = true;
        DataBaseManager.Intel_Dave5 = true;

        DataBaseManager.Intel_Dave1 = true;
        DataBaseManager.Intel_MissingPeople3 = true;
        DataBaseManager.Intel_RiverWaterQuality3 = true;
        DataBaseManager.Intel_Sewer3 = true;
        DataBaseManager.Intel_Ella2 = true;
        DataBaseManager.Intel_Aiden3 = true;
        DataBaseManager.Intel_CreepyEyes1 = true;
        DataBaseManager.Intel_RiverWaterQuality2 = true;

        DataBaseManager.Intel_RiverWaterQuality3 = true;

        DataBaseManager.Intel_Safe2 = true;

        DataBaseManager.getSafe = true;
        DataBaseManager.Intel_Safe2 = true;
        DataBaseManager.Intel_Sewer1 = true;

        DataBaseManager.Intel_Sewer2 = true;
        DataBaseManager.Intel_SewerWorker4 = true;

        DataBaseManager.Intel_Aiden1 = true;
        DataBaseManager.Intel_Safe1 = true;
        DataBaseManager.Intel_Nightmare1 = true;
        DataBaseManager.Intel_Nightmare2 = true;

        DataBaseManager.Intel_Insomnia1 = true;
        DataBaseManager.Intel_Insomnia2 = true;

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
        // 테스트 진행중 삭제
   
        DataBaseManager.ObservationPoint = 80;
        DataBaseManager.listeningPoint = 80;
        DataBaseManager.medicinePoint = 80;
        DataBaseManager.analysisPoint = 80;
        DataBaseManager.NoonEvent_AccessAuthorization = true;



        DataBaseManager.getSafe = true;
        DataBaseManager.GainCoat = true;

        DataBaseManager.gunShotPoint = 50;
        DataBaseManager.swordPoint = 40;
        DataBaseManager.martialArtsPoint = 30;
        DataBaseManager.stealthPoint = 90;


        DataBaseManager.rhetoricPoint = 90;
        DataBaseManager.luk = 25;
        //   DataBaseManager.Intel_Nightmare3 = true;
        //      DataBaseManager.Intel_Nightmare4 = true;
        DataBaseManager.swimingPoint = 50;
        DataBaseManager.DisguisePoint = 90;
        DataBaseManager.dex = 80;
        DataBaseManager.ObservationPoint = 90;
        DataBaseManager.occultPoint = 80;
        //     DataBaseManager.GainMap = true;

        DataBaseManager.Intel_SewageMaintenanceOffice1 = true;

        DataBaseManager.Intel_Sewer4 = true;


        //DataBaseManager.isDebuff_Deftness = true;
        // DataBaseManager.DaveDeathKeyword = true;
        DataBaseManager.Revolver += 1;

        DataBaseManager.Dagger += 1; DataBaseManager.Molotov += 2;
        DataBaseManager.TimeCount = 1;
        DataBaseManager.SewerageKey = true;
        //  DataBaseManager.SecondDayDialog = true;
        DataBaseManager.AlbertDeathKeyword = true; DataBaseManager.SwainDeathKeyword = true;
        DataBaseManager.PistolAmmo += 20;
        DataBaseManager.SafeDocument = true;
        DataBaseManager.Condition = "Best";
        //   DataBaseManager.Debuff = 2;
        DataBaseManager.SewerEnemyCounter = 1;
        DataBaseManager.Bandages = 5;
        DataBaseManager.Firstaidkit = 5;
        DataBaseManager.Painkillers = 5;


        DataBaseManager.EnemyDeathCount = 500;
        DataBaseManager.luk = 0;

      //  DataBaseManager.isDebuff_CognitiveBreakdown = true;
             */
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

        /*   
           Debug.Log("isActiveDialog1 :" + DataBaseManager.isActiveDialog1);
           Debug.Log("StoryDirecting :" + DataBaseManager.StoryDirecting);
           Debug.Log("secondisDirecting :" + DataBaseManager.secondisDirecting);
           Debug.Log("isRollet :" + DataBaseManager.isRollet);
           Debug.Log("isDirecting :" + DataBaseManager.isDirecting);
           Debug.Log("isOpenUi :" + DataBaseManager.isOpenUi);
           Debug.Log("isOpenMap :" + DataBaseManager.isOpenMap);
          */

        if (Input.GetKeyDown(KeyCode.F12))
        {
            //   player.transform.position = player_SecretHouse;
            // cam.transform.position = Cam_SecretHouse;
            player.transform.position = playerSymbol;
            cam.transform.position = camSymbol;
            TimeManagere.Instance.MakeSighnLand();
            DataBaseManager.nowPlace = "InSewer";
            DialogDatabaseManager.instance.check = true;

        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            DataBaseManager.endingDeathCheck = true;

            DataBaseManager.nowPlace = "InSewer";

        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            DataBaseManager.nowPlace = "InSewer";
            DataBaseManager.startSewerBattle = true;

        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            //BillowUIManager.Instance.HP_Battledown(5);


        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            TimeManagere.Instance.MakeSewerMapEnemy();

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
            DataBaseManager.condition = "Worst";

        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            DataBaseManager.tightwad = true;
            // BillowUIManager.Instance.HP_down(5);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            BillowUIManager.Instance.HpUp(5);

        }
        if (Input.GetKeyDown(KeyCode.F4))
        {

            //BillowUIManager.Instance.San_up(10);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {

            DataBaseManager.intelMurderCase1 = true;
            DataBaseManager.intelSewerWorker4 = true;
            DataBaseManager.intelMurderCase5 = true;
            DataBaseManager.intelMurderCase6 = true;
            DataBaseManager.intelMurderCase2 = true;
            DataBaseManager.intelMurderCase3 = true;
            DataBaseManager.intelMurderCase2 = true;
            DataBaseManager.intelSewer1 = true;
            DataBaseManager.swainDeathKeyword = true;

        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            DataBaseManager.wearCoat = true;
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            DataBaseManager.wearCoat = false;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {

        }
    }

    public void One()
    {
        DataBaseManager.intelElla1 = true;
        DataBaseManager.intelAiden1 = true;
        DataBaseManager.intelCane1 = true;
        DataBaseManager.intelAlbert1 = true;
        DataBaseManager.intelMeiv1 = true;
        DataBaseManager.intelBlackWell1 = true;
        DataBaseManager.intelSwain1 = true;

        DataBaseManager.intelPlanetaryParade1 = true;
        DataBaseManager.intelNightmare1 = true;
        DataBaseManager.intelParanoia1 = true;
        DataBaseManager.intelInsomnia1 = true;
        DataBaseManager.intelSewerGhostStory1 = true;
        DataBaseManager.intelMissingPeople1 = true;
        DataBaseManager.intelFishySmell1 = true;
        DataBaseManager.intelRiverWaterQuality1 = true;
        DataBaseManager.intelCreepyEyes1 = true;
        DataBaseManager.intelSewerWorker1 = true;
        DataBaseManager.intelSafe1 = true;


        DataBaseManager.intelDetectiveOffice1 = true;
        DataBaseManager.intelHospital1 = true;
        DataBaseManager.intelNewspaper1 = true;
        DataBaseManager.intelQuestHouse1 = true;
        DataBaseManager.intelStation1 = true;
        DataBaseManager.intelSewer1 = true;
        DataBaseManager.intelUniversity1 = true;
        DataBaseManager.intelRiverside1 = true;
        DataBaseManager.intelBar1 = true;
        DataBaseManager.intelSlum1 = true;
        DataBaseManager.intelPoliceOffice1 = true;
        DataBaseManager.intelGunsmith1 = true;
        DataBaseManager.intelSewageMaintenanceOffice1 = true;
        DataBaseManager.intelWharf1 = true;
    }
    public void Two()
    {
        DataBaseManager.intelElla2 = true;
        DataBaseManager.intelAiden2 = true;
        DataBaseManager.intelCane2 = true;
        DataBaseManager.intelAlbert2 = true;
        DataBaseManager.intelMeiv2 = true;
        DataBaseManager.intelBlackWell2 = true;
        DataBaseManager.intelSwain2 = true;

        DataBaseManager.intelPlanetaryParade2 = true;
        DataBaseManager.intelNightmare2 = true;
        DataBaseManager.intelParanoia2 = true;
        DataBaseManager.intelInsomnia2 = true;
        DataBaseManager.intelSewerGhostStory2 = true;
        DataBaseManager.intelMissingPeople2 = true;
        DataBaseManager.intelFishySmell2 = true;
        DataBaseManager.intelRiverWaterQuality2 = true;
        DataBaseManager.intelCreepyEyes2 = true;
        DataBaseManager.intelSewerWorker2 = true;
        DataBaseManager.intelSafe2 = true;


        DataBaseManager.intelDetectiveOffice2 = true;
        DataBaseManager.intelHospital2 = true;
        DataBaseManager.intelNewspaper2 = true;
        DataBaseManager.intelQuestHouse2 = true;
        DataBaseManager.intelStation2 = true;
        DataBaseManager.intelSewer2 = true;
        DataBaseManager.intelUniversity2 = true;
        DataBaseManager.intelRiverside2 = true;
        DataBaseManager.intelBar2 = true;
        DataBaseManager.intelSlum2 = true;
        DataBaseManager.intelPoliceOffice2 = true;
        DataBaseManager.intelGunsmith2 = true;
        DataBaseManager.intelSewageMaintenanceOffice2 = true;
        DataBaseManager.intelWharf2 = true;


    }
    public void Three()
    {
        DataBaseManager.intelElla3 = true;
        DataBaseManager.intelAiden3 = true;
        DataBaseManager.intelCane3 = true;
        DataBaseManager.intelAlbert3 = true;
        DataBaseManager.intelMeiv3 = true;
        DataBaseManager.intelBlackWell3 = true;
        DataBaseManager.intelSwain3 = true;

        DataBaseManager.intelPlanetaryParade3 = true;
        DataBaseManager.intelNightmare3 = true;
        DataBaseManager.intelParanoia3 = true;
        DataBaseManager.intelInsomnia3 = true;
        DataBaseManager.intelSewerGhostStory3 = true;
        DataBaseManager.intelMissingPeople3 = true;
        DataBaseManager.intelFishySmell3 = true;
        DataBaseManager.intelRiverWaterQuality3 = true;
        DataBaseManager.intelCreepyEyes3 = true;
        DataBaseManager.intelSewerWorker3 = true;
        DataBaseManager.intelSafe3 = true;

        DataBaseManager.intelDetectiveOffice3 = true;
        DataBaseManager.intelHospital3 = true;
        DataBaseManager.intelNewspaper3 = true;
        DataBaseManager.intelQuestHouse3 = true;
        DataBaseManager.intelStation3 = true;
        DataBaseManager.intelSewer3 = true;
        DataBaseManager.intelUniversity3 = true;
        DataBaseManager.intelRiverside3 = true;
        DataBaseManager.intelBar3 = true;
        DataBaseManager.intelSlum3 = true;
        DataBaseManager.intelPoliceOffice3 = true;
        DataBaseManager.intelGunsmith3 = true;
        DataBaseManager.intelSewageMaintenanceOffice3 = true;
        DataBaseManager.intelWharf3 = true;
    }
    public void Four()
    {
        DataBaseManager.intelElla4 = true;
        DataBaseManager.intelAiden4 = true;
        DataBaseManager.intelCane4 = true;
        DataBaseManager.intelAlbert4 = true;
        DataBaseManager.intelMeiv4 = true;
        DataBaseManager.intelBlackWell4 = true;
        DataBaseManager.intelSwain4 = true;

        DataBaseManager.intelPlanetaryParade4 = true;
        DataBaseManager.intelNightmare4 = true;
        DataBaseManager.intelParanoia4 = true;
        DataBaseManager.intelInsomnia4 = true;
        DataBaseManager.intelSewerGhostStory4 = true;
        DataBaseManager.intelMissingPeople4 = true;
        DataBaseManager.intelFishySmell4 = true;
        DataBaseManager.intelRiverWaterQuality4 = true;
        DataBaseManager.intelCreepyEyes4 = true;
        DataBaseManager.intelSewerWorker4 = true;
        DataBaseManager.intelSafe4 = true;

        DataBaseManager.intelDetectiveOffice4 = true;
        DataBaseManager.intelHospital4 = true;
        DataBaseManager.intelNewspaper4 = true;
        DataBaseManager.intelQuestHouse4 = true;
        DataBaseManager.intelStation4 = true;
        DataBaseManager.intelSewer4 = true;
        DataBaseManager.intelUniversity4 = true;
        DataBaseManager.intelRiverside4 = true;
        DataBaseManager.intelBar4 = true;
        DataBaseManager.intelSlum4 = true;
        DataBaseManager.intelPoliceOffice4 = true;
        DataBaseManager.intelGunsmith4 = true;
        DataBaseManager.intelSewageMaintenanceOffice4 = true;
        DataBaseManager.intelWharf4 = true;
    }
    public void Five()
    {
        DataBaseManager.intelElla5 = true;
        DataBaseManager.intelAiden5 = true;
        DataBaseManager.intelCane5 = true;
        DataBaseManager.intelAlbert5 = true;
        DataBaseManager.intelMeiv5 = true;
        DataBaseManager.intelBlackWell5 = true;
        DataBaseManager.intelSwain5 = true;

        DataBaseManager.intelPlanetaryParade5 = true;
        DataBaseManager.intelNightmare5 = true;
        DataBaseManager.intelParanoia5 = true;
        DataBaseManager.intelInsomnia5 = true;
        DataBaseManager.intelSewerGhostStory5 = true;
        DataBaseManager.intelMissingPeople5 = true;
        DataBaseManager.intelFishySmell5 = true;
        DataBaseManager.intelRiverWaterQuality5 = true;
        DataBaseManager.intelCreepyEyes5 = true;
        DataBaseManager.intelSewerWorker5 = true;
        DataBaseManager.intelSafe5 = true;


        DataBaseManager.intelDetectiveOffice5 = true;
        DataBaseManager.intelHospital5 = true;
        DataBaseManager.intelNewspaper5 = true;
        DataBaseManager.intelQuestHouse5 = true;
        DataBaseManager.intelStation5 = true;
        DataBaseManager.intelSewer5 = true;
        DataBaseManager.intelUniversity5 = true;
        DataBaseManager.intelRiverside5 = true;
        DataBaseManager.intelBar5 = true;
        DataBaseManager.intelSlum5 = true;
        DataBaseManager.intelPoliceOffice5 = true;
        DataBaseManager.intelGunsmith5 = true;
        DataBaseManager.intelSewageMaintenanceOffice5 = true;
        DataBaseManager.intelWharf5 = true;
    }

    public void Six()
    {
        DataBaseManager.intelElla6 = true;
        DataBaseManager.intelAiden6 = true;
        DataBaseManager.intelCane6 = true;
        DataBaseManager.intelAlbert6 = true;
        DataBaseManager.intelMeiv6 = true;
        DataBaseManager.intelBlackWell6 = true;
        DataBaseManager.intelSwain6 = true;

        DataBaseManager.intelPlanetaryParade6 = true;
        DataBaseManager.intelNightmare6 = true;
        DataBaseManager.intelParanoia6 = true;
        DataBaseManager.intelInsomnia6 = true;
        DataBaseManager.intelSewerGhostStory6 = true;
        DataBaseManager.intelMissingPeople6 = true;
        DataBaseManager.intelFishySmell6 = true;
        DataBaseManager.intelRiverWaterQuality6 = true;
        DataBaseManager.intelCreepyEyes6 = true;
        DataBaseManager.intelSewerWorker6 = true;
        DataBaseManager.intelSafe6 = true;


        DataBaseManager.intelDetectiveOffice6 = true;
        DataBaseManager.intelHospital6 = true;
        DataBaseManager.intelNewspaper6 = true;
        DataBaseManager.intelQuestHouse6 = true;
        DataBaseManager.intelStation6 = true;
        DataBaseManager.intelSewer6 = true;
        DataBaseManager.intelUniversity6 = true;
        DataBaseManager.intelRiverside6 = true;
        DataBaseManager.intelBar6 = true;
        DataBaseManager.intelSlum6 = true;
        DataBaseManager.intelPoliceOffice6 = true;
        DataBaseManager.intelGunsmith6 = true;
        DataBaseManager.intelSewageMaintenanceOffice6 = true;
        DataBaseManager.intelWharf6 = true;
    }

}