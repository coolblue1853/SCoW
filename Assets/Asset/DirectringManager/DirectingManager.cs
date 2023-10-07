using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class DirectingManager : MonoBehaviour
{
    public GameObject BattleM;
    public CameraManager cameraManager;
    public GameObject DialogManager;

    public GameObject Back;
    public Image BackGround;

    float ChInRommSize = 2.383215f; // -이면 왼쪽 +면 오른쪽
    Vector2 player_OutsideTo1st = new Vector3(-1217.79f, 2.13f);
    Vector3 Cam_OutsideTo1st = new Vector3(-1198, 1.5f, -15);

    Vector2 player_1stTo2st = new Vector3(-1380.6f, 7f);
    Vector3 Cam_1stTo2st = new Vector3(-1368.6f, 1.5f, -15);

    Vector2 player_RoomTo2st = new Vector3(-1365.4f, 7f);
    Vector3 Cam_RoomTo2st = new Vector3(-1368.6f, 1.5f, -15);

    Vector2 player_StartPos = new Vector3(-797.79f, 1.91f);
    Vector3 Cam_StartPos = new Vector3(-797.5f, 1.5f, -15);


    Vector3 Cam_BattlePos = new Vector3(-774.6f, 120.1f, -15);

    private static DirectingManager instance = null;
    public GameObject player;
    public GameObject Camera;
    //병원
    public GameObject Kate_noon;
    //탐정사무소
    public GameObject sit_player;
    public GameObject sit_player2;
    public GameObject sit_NoNewsPaperPlayer;
    public GameObject sit_Ella;
    public SpriteRenderer Newspaper;
    public static DirectingManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 호출될 때마다 초기화할 코드를 여기에 작성하세요.
        /* -- 다시 활성화 해 주어야 함         */
        if (scene.name == "Main")
        {
            Back.SetActive(true);
            Tween fadeTween2 = BackGround.DOFade(0, 1.5f);
            player.SetActive(false);
            player.transform.position = player_StartPos;
            Camera.transform.position = Cam_StartPos;
            DataBaseManager.nowPlace = "DetectiveOffice";
            //원래라면 활성화 해 주어야 함.
            FadingBackGround.Instance.FadeOut();
            Invoke("startGame", 2f);
            DataBaseManager.isDirecting = true;
        }
   
    }

    void Awake()
    {


        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {


    }

    void startGame()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("start");
    }
    bool isGo1stBattle;
    // Update is called once per frame
    void Update()
    {
  

        if (DataBaseManager.fst_Detectiv_TimeOn >= 2)
        {
            DataBaseManager.isDirecting = true;
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                SoundManager.Instance.Nock_Sound();
                // 노크소리 발동
                DataBaseManager.isActiveDialog2 = true;
                Invoke("After_NockSound", 1);
            }
        }
        if(DataBaseManager.fst_Detective_EndFirstDialog == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.fst_Detective_EndFirstDialog = false;
            fst_DetectiveOffice_FadeIn("End_Directing");
        }
        if (DataBaseManager.fst_Detective_NockDialog == true && DataBaseManager.isActiveDialog1 == false)
        {

            DataBaseManager.fst_Detective_NockDialog = false;
            fst_DetectiveOffice_FadeIn("Nock_Directing");
        }
        if (DataBaseManager.fst_Detective_AfterSelect == true && DataBaseManager.isActiveDialog1 == false)
        {
            SoundManager.Instance.Door_Sound();
            DataBaseManager.fst_Detective_AfterSelect = false;
            fst_DetectiveOffice_Fade("AfterSelect_Directing");
        }
        if (DataBaseManager.fst_Detective_AfterSelectNo == true && DataBaseManager.isActiveDialog1 == false)
        {
            SoundManager.Instance.Door_Sound();
            DataBaseManager.fst_Detective_AfterSelectNo = false;
            fst_DetectiveOffice_Fade("Accept_AfterSelectNo");
        }
        if (DataBaseManager.fst_Detective_EndSelect == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.fst_Detective_EndSelect = false;
            fst_DetectiveOffice_FadeOut("EndSelect_Directing");
        }
        // 의뢰자의 집
        if (DataBaseManager.firstClientsHouse == true && DataBaseManager.TimeCount <6)
        {
            DataBaseManager.firstClientsHouse = false;
            DataBaseManager.firstClientsHouseEnd = true;
            InteractionController.Instance.Start_1st_ClientsHouse("FirstArrive");
        }
        if(DataBaseManager.StrDialogOn == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.StrDialogOn = false;
            Rollet.Instance.setRollet("Aiden : TakeAway", "Str", DataBaseManager.str, "dialog");
        }
        if (DataBaseManager.FailTwoKeyword == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.FailTwoKeyword = false;
            DataBaseManager.isDirecting = true;
            DataBaseManager.isActiveDialog1 = true;
            DataBaseManager.AidenKeywordCount = 2;
            fst_DetectiveOffice_FadeIn("Go2stForce");
        }
        //강가
        if (DataBaseManager.Sewer_Observation == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Sewer_Observation = false;
            Rollet.Instance.setRollet("Sewer : Look", "Observation", DataBaseManager.ObservationPoint, "dialog");
        }
        if (DataBaseManager.Sewer_San == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Sewer_San = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "dialog");
        }
        //병원
        if (DataBaseManager.Kate_Disapear == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Kate_Disapear = false;
            Kate_Disapear();
        }
        if (DataBaseManager.ConnectKey == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.ConnectKey = false;
            Invoke("KeyConnect",0.5f);
        }
        if (DataBaseManager.EndDay == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.EndDay = false;
            FadingBackGround.Instance.FadeIn();

            Invoke("EndDay", 2f);
        }

        
        if (DataBaseManager.EndDemo == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.EndDemo = false;
            // FadingBackGround.Instance.DemoEnd_FadeIn();
            MapManager.Instance.MapOn();
        }
        
         if((DataBaseManager.TimeCount % 4) == 0 && DataBaseManager.nowPlace  == "DetectiveOffice" && once == false)
        {
            DirectingManager.Instance.OrganizeKeyword();
            once = true;
            player.SetActive(false);
            sit_NoNewsPaperPlayer.SetActive(true);
            if (DataBaseManager.isBar == true)
            {
                Invoke("KeyConnect", 12f);
            }
            else if (DataBaseManager.AfterBattle == true)
            {
                Invoke("KeyConnect", 3f);
            }
            else
            {
                Invoke("KeyConnect", 3f);
            }
        }




        //  << 2일차 이후 >>

        if (DataBaseManager.TimeCount == 5 && DataBaseManager.SecondDayDialog == false && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.SecondDayDialog = true;
            Invoke("ScendDayStartDialog", 3f);
        }

 
        if (DataBaseManager.AfterSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.AfterSanCheck = false;
            DataBaseManager.secondisDirecting = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "Fab_Case1");
            Debug.Log("11!!11");
        }
        if (DataBaseManager.Case2IntCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Case2IntCheck = false;
            Rollet.Instance.setRollet("INT : Check", "Association", DataBaseManager.intl, "Case2IntCheck");
        }



        

        //전투
        if (DataBaseManager.Battle_San == true && DataBaseManager.isActiveDialog1 == false)
        {
 
            DataBaseManager.Battle_San = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "RoadBattle1");
        }
        if(DataBaseManager.isBattleBeforeDialog == true &&DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.isBattleBeforeDialog = false;
            FadingBackGround.Instance.CastInOut();
            Invoke("GotoBattle",0.8f);
            BattleManager.Instance.CloseButtonUI();
        }

        if (DataBaseManager.EndBattle == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.AfterBattle = true;
            DialogDatabaseManager.instance.Check = true;
            DataBaseManager.EndBattle = false;
            isGo1stBattle = true;
            DirectingManager.Instance.EndBattle();
           // DataBaseManager.nowPlace = "DetectiveOffice";
            MapManager.Instance.MapOn();
        }

        //2일차 낮 강제 이벤트

        if (DataBaseManager.snd_Detective_NoonEvent == true && DataBaseManager.isActiveDialog1 == false)
        {
            
            DataBaseManager.snd_Detective_NoonEvent = false;
            Invoke("NoonEvent_PhoneCall", 1f);

        }
        if (DataBaseManager.NoonEvent_MovetoClient == true && DataBaseManager.isActiveDialog1 == false)
        {
            Invoke("NoonEvent_ClientsFirst", 12f);
            DataBaseManager.NoonEvent_MovetoClient = false;
            Invoke("MoveToClient", 0.7f);

        }
        if (DataBaseManager.NoonEventSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {

            DataBaseManager.NoonEventSanCheck = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "NoonEvent");
        }
        if(DataBaseManager.DeadBodyFirstLook == true && DataBaseManager.isActiveDialog1 == false)
        {
            if((DataBaseManager.Intel_MissingPeople1 == true || DataBaseManager.Intel_MissingPeople2 == true) && DataBaseManager.DeadBodyLook_MissingDialog == false)
            {
                DataBaseManager.DeadBodyLook_MissingDialog = true;
                InteractionController.Instance.Start_2nd_NoonEvent("NoonEvent_DeadBody_Look_Missing");
            }
            else if ((DataBaseManager.Intel_FishySmell3 == true || DataBaseManager.Intel_FishySmell4 == true || DataBaseManager.Intel_SewerWorker3 == true) && DataBaseManager.DeadBodyLook_FishySmellDialog == false)
            {
                DataBaseManager.DeadBodyLook_FishySmellDialog = true;
                InteractionController.Instance.Start_2nd_NoonEvent("NoonEvent_DeadBody_Look_Missing");
            }
        }






        if (DataBaseManager.nowHP <= 0 && DataBaseManager.TimeCount >1)  
        {

            DataBaseManager.EndDemo = true;
        }


        
    }
    bool once = false;

    public void NoonEvent_ClientsFirst()
    {
        InteractionController.Instance.Start_2nd_NoonEvent("NoonEvent_ClientHouse");
        DataBaseManager.StoryDirecting = false;
    }
    // 강제 이벤트
    public void MoveToClient()
    {
        MapManager.Instance.MapOn();
        DataBaseManager.nowPlace = "NoonEvent";
    }

    public void NoonEvent_PhoneCall()
    {
        InteractionController.Instance.Start_2nd_NoonEvent("NoonEvent_FirstDailog_Detective");
    }

    //2일차

    public void ScendDayStartDialog()
    {
        if(DataBaseManager.Intel_Nightmare3 == true || DataBaseManager.Intel_Nightmare3 == true)
        {
            InteractionController.Instance.Start_2nd_DetectiveOffice("Fab_Case1");
        }
        else if (DataBaseManager.Intel_Aiden1)
        {
            InteractionController.Instance.Start_2nd_DetectiveOffice("Fab_Case2");
        }
        else
        {
            InteractionController.Instance.Start_2nd_DetectiveOffice("Fab_Case3");
        }
            
    }










    // 1일차


    public void BeforeBattle()
    {
        player.SetActive(false);
        Camera.transform.position = Cam_BattlePos;
        Invoke("BattleDialog", 2f);
    }
    public GameObject battleUI;
    public BattleCameraMove cameraMove;
    void BattleDialog()
    {

        InteractionController.Instance.BattleDialog("start");
    }
    //전투 
    public void GotoBattle()
    {

        DataBaseManager.nowPlace = "BattleRoad";
        BattleManager.Instance.startReload();
        battleUI.SetActive(true);
        cameraMove.enabled = true;
        //DialogManager.SetActive(false);
        cameraManager.enabled = false;
        Camera.transform.position = Cam_BattlePos;
        Invoke("setBattle", 3);
        BattleM.SetActive(true);
    }
    public void EndBattle()
    {
        Invoke("EndBattleInvoke", 1f);
    }
    public void EndBattleInvoke()
    {

        battleUI.SetActive(false);
        cameraMove.enabled = false;
        DialogManager.SetActive(true);
        cameraManager.enabled = true;
        BattleM.SetActive(false);
    }


    public void setBattle()
    {
        DataBaseManager.isDirecting = false;
        BattleManager.Instance.StartBattle = true;


    }

    public void KeyConnect()
    {

        OrganizeKeywordFunc();
    }
    public void EndDay()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_EndDay");
    }

    //병원 연출
    public void Kate_Disapear()
    {
        DataBaseManager.isDirecting = true;
        FadingBackGround.Instance.FadeInOut();
        Invoke("Kate", 2f);
    }
    public void Kate()
    {
        Kate_noon.SetActive(false);
        DataBaseManager.isDirecting = false;
    }






    // 1일차 낮 의뢰자의 집 연출
    public void DoorNocking()
    {
        DataBaseManager.isDirecting = true;
        FadingBackGround.Instance.FadeInOut();
       // SoundManager.Instance.Door_Sound();
        Invoke("moveInside",1f);

    }
    void moveInside()
    {
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
        player.transform.localPosition = player_OutsideTo1st;
        Camera.transform.localPosition = Cam_OutsideTo1st;
        Invoke("InsiedDialog", 1f);
    }
    void InsiedDialog()
    {
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.Start_1st_ClientsHouse("NockDoor");
        
    }
    public void FirstUpsair()
    {
        DataBaseManager.isDirecting = true;
        FadingBackGround.Instance.FadeInOut();
        Invoke("MoveUpsair", 1f);
        // DataBaseManager.isDirecting = true;
    }
    void MoveUpsair()
    {
        player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = player_1stTo2st;
        Camera.transform.localPosition = Cam_1stTo2st;
        Invoke("UpstairDialog", 1f);
    }
    void UpstairDialog()
    {
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.Start_1st_ClientsHouse("Ella_FirstUpstair");

    }
    void FirstRoomDialog()
    {
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.Start_1st_ClientsHouse("Ella_FirstUpstair");

    }
    void Go2stForce()
    {
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = player_RoomTo2st;
        Camera.transform.localPosition = Cam_RoomTo2st;
        DataBaseManager.isActiveDialog1 = false;
        Invoke("ForceRoom", 1f);
    }
    void ForceRoom()
    {
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.Start_1st_ClientsHouse("Aiden_key_FailForce");

    }




    // 1일차 오전 탐정사무소 연출
    void After_NockSound()
    {
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.Start_1st_DetectiveOffice("Directing_Nock");
    }

    public void fst_DetectiveOffice_Fade(string Connect)
    {
        FadingBackGround.Instance.FadeIn();
        Invoke(Connect, 1f);
    }
    public void fst_DetectiveOffice_FadeOut(string Connect)
    {
        FadingBackGround.Instance.FadeOut();
        Invoke(Connect, 1f);
    }
    public void fst_DetectiveOffice_FadeIn(string Connect)
    {
        FadingBackGround.Instance.FadeInOut();
        Invoke(Connect, 1.5f);
    }

    public void End_Directing()
    {
        sit_player.SetActive(false);
        sit_player2.SetActive(false);
        player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
        player.transform.localPosition = new Vector3(-797.556f, 2.183268f);
        player.SetActive(true);
        
        if(DataBaseManager.TimeCount == 1)
        {
            Newspaper.DOFade(1, 0.1f);
        }
   
    }
    public void Nock_Directing()
    {
        Camera.transform.DOMoveX(-790.3f, 1);
        player.transform.position = new Vector2(-796.7f,player.transform.position.y);
        player.SetActive(false);
        sit_NoNewsPaperPlayer.SetActive(true);
        sit_Ella.SetActive(true);
        Invoke("AfterNock_Directing", 1f);

    }


    void AfterNock_Directing()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Directing_AfterDoor");
    }
    
    public void AfterSelect_Directing()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Accept_AfterSelectYes");
    }
    public void Accept_AfterSelectNo()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Accept_AfterSelectNo");
    }
    public void EndSelect_Directing()
    {
        player.SetActive(true);
        sit_NoNewsPaperPlayer.SetActive(false);
        sit_Ella.SetActive(false);
        DataBaseManager.isDirecting = false;
    }

    private List<System.Action> functionList = new List<System.Action>();
    public void OrganizeKeyword()
    {
        functionList.Add(Fab_FirstDialog);
        if(DataBaseManager.Intel_Aiden1 == true)
        {
            functionList.Add(Fab_Aiden1);
        }
        if (DataBaseManager.Intel_Safe1 == true)
        {
            functionList.Add(Fab_Safe1);
        }
        if (DataBaseManager.Intel_Nightmare1 == true)
        {
            functionList.Add(Fab_Nightmare1);
        }
        if (DataBaseManager.Intel_Nightmare2 == true)
        {
            functionList.Add(Fab_Nightmare2);
        }
        if (DataBaseManager.Intel_Nightmare3 == true)
        {
            functionList.Add(Fab_Nightmare3);
        }
        if (DataBaseManager.Intel_Nightmare4 == true)
        {
            functionList.Add(Fab_Nightmare4);
        }
        if (DataBaseManager.Intel_Insomnia1 == true)
        {
            functionList.Add(Fab_Insomnia1);
        }
        if (DataBaseManager.Intel_Insomnia2 == true)
        {
            functionList.Add(Fab_Insomnia2);
        }
        if (DataBaseManager.Intel_PlanetaryParade1 == true)
        {
            functionList.Add(Fab_PlanetarySequence1);
        }
        if (DataBaseManager.Intel_University1 == true)
        {
            functionList.Add(Fab_Univ1);
        }
        if (DataBaseManager.Intel_Meiv2 == true)
        {
            functionList.Add(Fab_Meiv2);
        }
        if (DataBaseManager.Intel_MissingPeople1 == true)
        {
            functionList.Add(Fab_MissingPeople);
        }
        if (DataBaseManager.Intel_SewerGhostStory1 == true)
        {
            functionList.Add(Fab_GhostStory1);
        }
        if (DataBaseManager.Intel_SewerGhostStory2 == true)
        {
            functionList.Add(Fab_GhostStory2);
        }
        if (DataBaseManager.Intel_CreepyEyes1 == true)
        {
            functionList.Add(Fab_CreepyEye);
        }
        if (DataBaseManager.Intel_SewerWorker1 == true)
        {
            functionList.Add(Fab_Worker1);
        }
        if (DataBaseManager.Intel_FishySmell1 == true)
        {
            functionList.Add(Fab_FishySmell1);
        }
        if (DataBaseManager.Intel_FishySmell2 == true)
        {
            functionList.Add(Fab_FishySmell2);
        }
        if (DataBaseManager.Intel_FishySmell3 == true)
        {
            functionList.Add(Fab_FishySmell3);
        }
    }
    public void OrganizeKeywordFunc()
    {
        if(functionList.Count > 0)
        {
            int randomIndex = 0;
            System.Action selectedFunction = functionList[randomIndex];
            // 선택된 함수 실행
            selectedFunction();
            // 실행된 함수는 리스트에서 제거
            functionList.RemoveAt(randomIndex);
        }
        else
        {

            DataBaseManager.EndDay = true;

        }
        // 함수 리스트에서 무작위로 함수 선택

    }
    private void Fab_FirstDialog()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_FirstDialog");
    }
    private void Fab_Aiden1()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Aiden1");
    }
    private void Fab_Safe1()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Safe1");
    }
    private void Fab_Nightmare1()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Nightmare1");
    }
    private void Fab_Nightmare2()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Nightmare2");
    }
    private void Fab_Nightmare3()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Nightmare3");
    }
    private void Fab_Nightmare4()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Nightmare4");
    }
    private void Fab_Insomnia1()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Insomnia1");
    }
    private void Fab_Insomnia2()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Insomnia2");
    }
    private void Fab_PlanetarySequence1()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_PlanetarySequence1");
    }
    private void Fab_Univ1()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Univ1");
    }
    private void Fab_Meiv2()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Meiv2");
    }
    private void Fab_MissingPeople()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_MissingPeople");
    }
    private void Fab_GhostStory1()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_GhostStory1");
    }
    private void Fab_GhostStory2()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_GhostStory2");
    }
    private void Fab_CreepyEye()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_CreepyEye");
    }
    private void Fab_Worker1()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Worker1");
    }
    private void Fab_FishySmell1()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_FishySmell1");
    }
    private void Fab_FishySmell2()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_FishySmell2");
    }
    private void Fab_FishySmell3()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_FishySmell3");
    }


}
