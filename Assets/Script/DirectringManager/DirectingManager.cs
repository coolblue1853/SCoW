using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class DirectingManager : MonoBehaviour
{
    public GameObject boxHybrid;
    public GameObject battleMap;
    public CameraManager cameraManager;
    public GameObject dialogManager;
    Vector2 playerSewerRight = new Vector3(-1194.19f, -193.57f);
    Vector3 camSewerRight = new Vector3(-1217.02f, -195f, -15);
    public GameObject backgroundOb;
    public Image backGroundImg;
    public GameObject sewerBattleBackground;
    public GameObject roadBattleBackground;
    public GameObject battleObject;
    Vector2 battlePlayerRoad = new Vector3(-213.58f, 209.2012f);
    Vector2 battlePlayerSewer = new Vector3(-220.9f, 204.5f);
    float chInRommSize = 2.383215f; // -이면 왼쪽 +면 오른쪽
    Vector2 playerOutsideTo1st = new Vector3(-1217.79f, 2.13f);
    Vector3 CamOutsideTo1st = new Vector3(-1198, 1.5f, -15);
    Vector2 player1stTo2st = new Vector3(-1380.6f, 7f);
    Vector3 Cam1stTo2st = new Vector3(-1368.6f, 1.5f, -15);
    Vector2 playerRoomTo2st = new Vector3(-1365.4f, 7f);
    Vector3 camRoomTo2st = new Vector3(-1368.6f, 1.5f, -15);
    Vector2 playerStartPos = new Vector3(-797.79f, 1.91f);
    Vector3 camStartPos = new Vector3(-797.5f, 1.5f, -15);
    Vector3 camBattlePos = new Vector3(-774.6f, 120.1f, -15);
    Vector3 camBattleSewerPos = new Vector3(-776.77f, 119.79f, -15);
    bool once = false;
    bool thrid;
    bool twice;
    private static DirectingManager instance = null;
    public GameObject player;
    public GameObject camera;
    //병원
    public GameObject kateNoon;
    //탐정사무소
    public GameObject sitPlayer;
    public GameObject sitPlayer2;
    public GameObject sitNoNewspaperPlayer;
    public GameObject sitElla;
    public SpriteRenderer newspaper;
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
            backgroundOb.SetActive(true);
            Tween fadeTween2 = backGroundImg.DOFade(0, 1.5f);
            player.SetActive(false);
            player.transform.position = playerStartPos;
            camera.transform.position = camStartPos;
            DataBaseManager.nowPlace = "DetectiveOffice";
            //원래라면 활성화 해 주어야 함.
            FadingBackGround.Instance.FadeOut();
            Invoke("StartGame", 2f);
            DataBaseManager.isDirecting = true;
        }
    }
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void StartGame()
    {
        if (DataBaseManager.isTest == false)
        {
            InteractionController.Instance.Start1stDetectiveOffice("start");
        }
        else
        {
            DataBaseManager.fstDetectiveEndFirstDialog = true;
        }

    }

    public GameObject endGame;
    bool isGo1stBattle;
    public GameObject visionLessField;
    public GameObject noteButton;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject battleUI;
    public BattleCameraMove cameraMove;
    public GameObject enemyTrunSymbol1;
    public GameObject enemyTrunSymbol2;
    public GameObject enemyTrunSymbol3;
    public GameObject battleRoadLight;
    public GameObject sewerEnemy1;
    public GameObject sewerEnemy2;
    public GameObject playerTrunSymbol;

    void Update()
    {
        if (DataBaseManager.isDebuffDyslexia == true && noteButton.activeSelf == true)
        {
            noteButton.SetActive(false);
        }
        else if (DataBaseManager.isDebuffDyslexia == false && noteButton.activeSelf == false)
        {
            noteButton.SetActive(true);
        }
        if (DataBaseManager.isDebuffVisionLoss == true && visionLessField.activeSelf == false)
        {
            visionLessField.SetActive(true);
        }
        if (DataBaseManager.nowHP <= 0 || DataBaseManager.nowSan <= 0)
        {
            endGame.SetActive(true);
        }
        else
        {
            endGame.SetActive(false);
        }
        if (DataBaseManager.fstDetectivTimeOn >= 2)
        {
            DataBaseManager.isDirecting = true;
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                SoundManager.Instance.NockSound();
                // 노크소리 발동
                DataBaseManager.isActiveDialog2 = true;
                Invoke("AfterNockSound", 1);
            }
        }
        if (DataBaseManager.fstDetectiveEndFirstDialog == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.fstDetectiveEndFirstDialog = false;
            DetectiveOfficeFadeIn("EndDirecting");
        }
        if (DataBaseManager.fstDetectiveNockDialog == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.fstDetectiveNockDialog = false;
            DetectiveOfficeFadeIn("NockDirecting");
        }
        if (DataBaseManager.fstDetectiveAfterSelect == true && DataBaseManager.isActiveDialog1 == false)
        {
            SoundManager.Instance.DoorSound();
            DataBaseManager.fstDetectiveAfterSelect = false;
            DetectiveOfficeFade("AfterSelectDirecting");
        }
        if (DataBaseManager.fstDetectiveAfterSelectNo == true && DataBaseManager.isActiveDialog1 == false)
        {
            SoundManager.Instance.DoorSound();
            DataBaseManager.fstDetectiveAfterSelectNo = false;
            DetectiveOfficeFade("AcceptAfterSelectNo");
        }
        if (DataBaseManager.fstDetectiveEndSelect == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.fstDetectiveEndSelect = false;
            DetectiveOfficeFadeOut("EndSelectDirecting");
        }
        // 의뢰자의 집
        if (DataBaseManager.firstClientsHouse == true && DataBaseManager.timeCount < 6)
        {
            DataBaseManager.firstClientsHouse = false;
            DataBaseManager.firstClientsHouseEnd = true;
            InteractionController.Instance.Start1stClientsHouse("FirstArrive");
        }
        if (DataBaseManager.strDialogOn == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.strDialogOn = false;
            Rollet.Instance.SetRollet("Aiden : TakeAway", "Str", DataBaseManager.str, "dialog");
        }
        if (DataBaseManager.failTwoKeyword == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.failTwoKeyword = false;
            DataBaseManager.isDirecting = true;
            DataBaseManager.isActiveDialog1 = true;
            DataBaseManager.aidenKeywordCount = 2;
            DetectiveOfficeFadeIn("Go2stForce");
        }
        //강가
        if (DataBaseManager.sewerObservation == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.sewerObservation = false;
            Rollet.Instance.SetRollet("Sewer : Look", "Observation", DataBaseManager.observationPoint, "dialog");
        }
        if (DataBaseManager.sewerSan == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.sewerSan = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "dialog");
        }
        //병원
        if (DataBaseManager.kateDisapear == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.kateDisapear = false;
            Kate_Disapear();
        }
        if (DataBaseManager.connectKey == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.connectKey = false;
            Invoke("KeyConnect", 0.5f);
        }
        if (DataBaseManager.endDay == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.storyDirecting = true;
            DataBaseManager.endDay = false;
            FadingBackGround.Instance.FadeIn();
            Invoke("EndDay", 2f);
        }
        if (DataBaseManager.endDemo == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.endDemo = false;
            // FadingBackGround.Instance.DemoEnd_FadeIn();
            MapManager.Instance.MapOn();
        }
        if ((DataBaseManager.timeCount == 4) && DataBaseManager.nowPlace == "DetectiveOffice" && once == false)
        {
            DirectingManager.Instance.OrganizeKeyword();
            once = true;
            player.SetActive(false);
            sitNoNewspaperPlayer.SetActive(true);
            if (DataBaseManager.isBar == true || DataBaseManager.isSewerOffice == true)
            {
                Invoke("KeyConnect", 12f);
            }
            else if (DataBaseManager.afterBattle == true)
            {
                Invoke("KeyConnect", 3f);
            }
            else
            {
                Invoke("KeyConnect", 3f);
            }
        }
        if ((DataBaseManager.timeCount == 8 && DataBaseManager.nowPlace == "DetectiveOffice" && twice == false))
        {
            DataBaseManager.storyDirecting = true;
            DirectingManager.Instance.OrganizeKeyword();
            twice = true;
            player.SetActive(false);
            sitNoNewspaperPlayer.SetActive(true);
            if (DataBaseManager.isBar == true || DataBaseManager.isSewerOffice == true)
            {
                Invoke("KeyConnect", 12f);
            }
            else if (DataBaseManager.afterBattle == true)
            {
                Invoke("KeyConnect", 3f);
            }
            else
            {
                Invoke("KeyConnect", 3f);
            }
        }
        if ((DataBaseManager.timeCount == 12 && DataBaseManager.nowPlace == "DetectiveOffice" && thrid == false))
        {
            DataBaseManager.storyDirecting = true;
            DirectingManager.Instance.OrganizeKeyword();
            thrid = true;
            player.SetActive(false);
            sitNoNewspaperPlayer.SetActive(true);
            if (DataBaseManager.isBar == true || DataBaseManager.isSewerOffice == true)
            {
                Invoke("KeyConnect", 12f);
            }
            else if (DataBaseManager.afterBattle == true)
            {
                Invoke("KeyConnect", 3f);
            }
            else
            {
                Invoke("KeyConnect", 3f);
            }
        }
        if (DataBaseManager.timeCount == 5 && DataBaseManager.secondDayDialog == false && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.secondDayDialog = true;
            Invoke("ScendDayStartDialog", 3f);
        }
        if (DataBaseManager.timeCount == 9 && DataBaseManager.secondDayDialog == false && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.secondDayDialog = true;
            Invoke("ThirdDayStartDialog", 3f);
        }
        if (DataBaseManager.timeCount == 13 && DataBaseManager.secondDayDialog == false && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.secondDayDialog = true;
            Invoke("FourthDayStartDialog", 3f);
        }
        if (DataBaseManager.afterSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.afterSanCheck = false;
            DataBaseManager.secondisDirecting = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "Fab_Case1");
        }
        if (DataBaseManager.case2IntCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.case2IntCheck = false;
            Rollet.Instance.SetRollet("INT : Check", "Association", DataBaseManager.intl, "Case2IntCheck");
        }
        if (DataBaseManager.daveCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.daveCheck = false;
            selectionUIManager.Instance.EndDialog();
        }
        //전투
        if (DataBaseManager.battleSan == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.battleSan = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "RoadBattle1");
        }
        if (DataBaseManager.isBattleBeforeDialog == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.isBattleBeforeDialog = false;
            FadingBackGround.Instance.FadeInOut();
            Invoke("GotoBattle", 1.1f);
            BattleManager.Instance.CloseButtonUI();
        }

        if (DataBaseManager.endBattle == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.afterBattle = true;
            DialogDatabaseManager.instance.check = true;
            DataBaseManager.endBattle = false;
            isGo1stBattle = true;
            DirectingManager.Instance.EndBattle();
            MapManager.Instance.MapOn();
        }
        //하수도 
        if (DataBaseManager.startSewerBattle == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.startSewerBattle = false;
            FadingBackGround.Instance.FadeInOut();
            Invoke("GotoBattleSewer", 1.1f);
            BattleManager.Instance.CloseButtonUI();
        }
        if (DataBaseManager.inSewerStealthSucc == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.inSewerStealthSucc = false;
            FadingBackGround.Instance.FadeInOut(); // 이거 문제 체크
            Invoke("InSewerFabianMove", 1.1f);
        }
        if (DataBaseManager.inSewerStealthFail == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.inSewerStealthFail = false;
            FadingBackGround.Instance.FadeInOut();
            Invoke("GotoBattleSewer", 1.1f);
            BattleManager.Instance.CloseButtonUI();
        }
        if (DataBaseManager.sewerFirstIn == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.storyDirecting = true;
            DataBaseManager.sewerFirstIn = false;
            InteractionController.Instance.InSewerDialog("Sewer_FirstIn");
        }
        if (DataBaseManager.sewerFirstInSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.storyDirecting = false;
            DataBaseManager.sewerFirstInSanCheck = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "Sewer_FirstInSan");
        }
        if (DataBaseManager.sewerDeepOneFirstSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.sewerDeepOneFirstSanCheck = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "DeepOneFirstSan");
        }

        //2일차 낮 강제 이벤트
        if (DataBaseManager.secondDetectiveNoonEvent == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.secondDetectiveNoonEvent = false;
            Invoke("NoonEventPhoneCall", 1f);
        }
        if (DataBaseManager.noonEventMovetoClient == true && DataBaseManager.isActiveDialog1 == false)
        {
            Invoke("NoonEventClientsFirst", 12f);
            DataBaseManager.noonEventMovetoClient = false;
            Invoke("MoveToClient", 0.7f);
        }
        if (DataBaseManager.noonEventSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.noonEventSanCheck = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "NoonEvent");
        }
        if (DataBaseManager.deadBodyFirstLook == true && DataBaseManager.isActiveDialog1 == false)
        {
            if ((DataBaseManager.intelMissingPeople1 == true || DataBaseManager.intelMissingPeople2 == true) && DataBaseManager.deadBodyLookMissingDialog == false)
            {
                DataBaseManager.deadBodyLookMissingDialog = true;
                InteractionController.Instance.Start2ndNoonEvent("NoonEvent_DeadBody_Look_Missing");
            }
            else if ((DataBaseManager.intelFishySmell3 == true || DataBaseManager.intelFishySmell4 == true || DataBaseManager.intelSewerWorker3 == true) && DataBaseManager.deadBodyLookFishySmellDialog == false)
            {
                DataBaseManager.deadBodyLookFishySmellDialog = true;
                InteractionController.Instance.Start2ndNoonEvent("NoonEvent_DeadBody_Look_FishySmell");
            }
        }
        if (DataBaseManager.deadBodyGotoSwere == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.deadBodyGotoSwere = false;
            DataBaseManager.storyDirecting = true;
            MapManager.Instance.SuccAnalysis();
        }
        if (DataBaseManager.kaneSlimeSan == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.kaneSlimeSan = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "KaneSan");
        }
        if (DataBaseManager.barArrive == true && DataBaseManager.isActiveDialog1 == false)
        {
            boxHybrid.SetActive(true);
            Invoke("ArriveBar2nd", 4f);
            DataBaseManager.barArrive = false;
            DataBaseManager.storyDirecting = true;
            DataBaseManager.maeveFirstDialog = true;
        }
        if (DataBaseManager.barMove == true && DataBaseManager.isActiveDialog1 == false)
        {
            Invoke("BarMove", 0.3f);
            DataBaseManager.barMove = false;
        }
        if (DataBaseManager.barDrinkSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.barDrinkSanCheck = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "DrinkSan");
        }
        if (DataBaseManager.thirdDayDreamSan == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.thirdDayDreamSan = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "DayTwoCase2San");
        }
        if (DataBaseManager.day4Case1San == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.day4Case1San = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "Fab_4th_QuestO_SanCheck");
        }
        if (DataBaseManager.day4Case1Health == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.day4Case1Health = false;
            Rollet.Instance.SetRollet("Health : Check", "Health", DataBaseManager.hp, "dialog", "Day_4_case1Health");
        }
        if (DataBaseManager.moveSewerOfficeWindow == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.moveSewerOfficeWindow = false;
            Invoke("WindowMove", 1.5f);
            FadingBackGround.Instance.FadeInOut();
        }
        if (DataBaseManager.afterDaveDeathFirstSlum == true && DataBaseManager.nowPlace == "Slum" && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.afterDaveDeathFirstSlum = false;
            if ((DataBaseManager.timeCount == 13 && DataBaseManager.isMorningMapMove == true) || (DataBaseManager.timeCount == 9 && DataBaseManager.isMorningMapMove == true) || (DataBaseManager.timeCount == 5 && DataBaseManager.isMorningMapMove == true))
            {
                Invoke("AfterDaveDeathDailog", 6f);
            }
            else
            {
                Invoke("AfterDaveDeathDailog", 10f);
            }
        }
        //하수도 내부
        if (DataBaseManager.insmusRhtoric == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.insmusRhtoric = false;
            Rollet.Instance.SetRollet("??? : Give Excuse", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
        }
        if (DataBaseManager.journalSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.journalSanCheck = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "JournalSan");
        }
        if (DataBaseManager.sewerBookSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.sewerBookSanCheck = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "BookSan");
        }
        if (DataBaseManager.sewerBookOccultCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.sewerBookOccultCheck = false;
            Rollet.Instance.SetRollet("Book : Decryption", "Occult", DataBaseManager.occultPoint, "dialog");
        }
        if (DataBaseManager.sewerSymbolOccultCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.sewerSymbolOccultCheck = false;
            Rollet.Instance.SetRollet("Symbol : Decryption", "Occult", DataBaseManager.occultPoint, "dialog");
        }
        if (DataBaseManager.symbolStrSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.symbolStrSanCheck = false;
        }
        if (DataBaseManager.nowHP <= 0 && DataBaseManager.timeCount > 1)
        {
            //죽었을때
            //DataBaseManager.EndDemo = true;
        }
        if (DataBaseManager.endingSwin == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.endingSwin = false;
            Rollet.Instance.SetRollet("Fabian : Swimming", "Swin", DataBaseManager.swimingPoint, "dialog");
        }
        if (DataBaseManager.endingLadderSan == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.endingLadderSan = false;
            Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "EndingLedderSan");
        }
        if (DataBaseManager.endingBreakSymbol == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.endingBreakSymbol = false;
            InteractionController.Instance.InSewerDialog("Ending_BreakSymbol");
        }
        if (DataBaseManager.endingisPromise == true && DataBaseManager.isActiveDialog1 == false)
        {
            camera.transform.position = new Vector3(-806.7f, 0.3f, -16);
            player.SetActive(false);
            sitNoNewspaperPlayer.SetActive(true);
            DataBaseManager.endingisPromise = false;
            InteractionController.Instance.InSewerDialog("Ending_isPromise");
        }
        if (DataBaseManager.endingAfterPromiseDialog == true && DataBaseManager.isActiveDialog1 == false && DataBaseManager.albertDeathKeyword == true)
        {
            DataBaseManager.endingAfterPromiseDialog = false;
            InteractionController.Instance.InSewerDialog("Ending_IfNoPromiseAlbertDeath");
        }
        if (DataBaseManager.endingSwainDeathCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.endingSwainDeathCheck = false;
            if (DataBaseManager.swainDeathKeyword == true)
            {
                InteractionController.Instance.InSewerDialog("Ending_SwainDeathCheck");
            }
            else
            {
                InteractionController.Instance.InSewerDialog("Ending_RequestCheck");
            }

        }
        if (DataBaseManager.endingRequestCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.endingRequestCheck = false;
            InteractionController.Instance.InSewerDialog("Ending_RequestCheck");
        }
        if (DataBaseManager.endingDeathCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.endingDeathCheck = false;
            if (DataBaseManager.enemyDeathCount == 0)
            {
                // 결말 장면 삽입
            }
            else
            {
                int DeathCheck = Random.Range(1, DataBaseManager.enemyDeathCount * 5);
                if (DeathCheck <= DataBaseManager.enemyDeathCount * 5)
                {
                    int LuckInt = Random.Range(1, DataBaseManager.luk);
                    if (LuckInt <= DataBaseManager.luk)
                    {
                        //  결말 장면 삽입.
                    }
                    else
                    {
                        DataBaseManager.endingDoorCheck = true;
                        player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
                        player.transform.position = new Vector3(-796.8f, 2.500006f, -1);
                        SoundManager.Instance.NockSound();
                        Invoke("EndingNockSound", 0.5f);

                    }
                }
            }
        }
    }

    void EndingNockSound()
    {
        player.SetActive(true);
        sitNoNewspaperPlayer.SetActive(false);
        DataBaseManager.storyDirecting = false;
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.InSewerDialog("Ending_Killing");
    }

    public void WindowMove()
    {
        MapManager.Instance.WindowFabianMove();
    }
    public void BarMove()
    {
        Invoke("MeaveDialog", 2.5f);
        Invoke("BarFabianMove", 1.5f);
        FadingBackGround.Instance.FadeInOut();
    }
    public void BarFabianMove()
    {
        boxHybrid.SetActive(false);
        MapManager.Instance.BarFabianMove();
    }
    public void InSewerFabianMove()
    {
        TimeManagere.Instance.MakeSewerMapNomal();
        DataBaseManager.isDirecting = false;
        player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = playerSewerRight;
        camera.transform.localPosition = camSewerRight;
        TimeManagere.Instance.DeletRightSewerPotal();
    }
    public void MeaveDialog()
    {
        InteractionController.Instance.Start1stBar("Meave2ndDialog");
    }
    public void AfterDaveDeathDailog()
    {
        InteractionController.Instance.Start1stSlum("AfterDaveDeath");
    }
    public void ArriveBar2nd()
    {
        InteractionController.Instance.Start1stBar("BarArrive");
    }
    public void NoonEventClientsFirst()
    {
        InteractionController.Instance.Start2ndNoonEvent("NoonEvent_ClientHouse");
        DataBaseManager.storyDirecting = false;
    }
    // 강제 이벤트
    public void MoveToClient()
    {
        MapManager.Instance.MapOn();
        DataBaseManager.nowPlace = "NoonEvent";
    }
    public void NoonEventPhoneCall()
    {
        InteractionController.Instance.Start2ndNoonEvent("NoonEvent_FirstDailog_Detective");
    }

    //2일차
    public void ScendDayStartDialog()
    {
        if (DataBaseManager.intelNightmare3 == true || DataBaseManager.intelNightmare3 == true)
        {
            InteractionController.Instance.Start2ndDetectiveOffice("Fab_Case1");
        }
        else if (DataBaseManager.intelQuestHouse1 == true)
        {
            InteractionController.Instance.Start2ndDetectiveOffice("Fab_Case2");
        }
        else
        {
            InteractionController.Instance.Start2ndDetectiveOffice("Fab_Case3");
        }
    }
    public void ThirdDayStartDialog()
    {
        Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "ThirdDayStartSan");
    }
    public void FourthDayStartDialog()
    {
        Rollet.Instance.SetRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "FourthDayStartSan");
    }


    // 1일차
    public void BeforeBattle()
    {
        battleObject.transform.localPosition = battlePlayerRoad;
        BattleManager.Instance.OriginPointChange();
        battleRoadLight.SetActive(true);
        if (DataBaseManager.timeCount < 5)
        {
            enemy1.SetActive(true);
            enemy2.SetActive(false);
            enemy3.SetActive(false);
            enemyTrunSymbol1.SetActive(true);
            enemyTrunSymbol2.SetActive(false);
            enemyTrunSymbol3.SetActive(false);
            BattleManager.Instance.SetDeepOneHybrid1();
        }
        else if (DataBaseManager.timeCount < 9)
        {
            enemy1.SetActive(true);
            enemy2.SetActive(true);
            enemy3.SetActive(false);
            enemyTrunSymbol1.SetActive(true);
            enemyTrunSymbol2.SetActive(true);
            enemyTrunSymbol3.SetActive(false);
            BattleManager.Instance.SetDeepOneHybrid1();
            BattleManager.Instance.SetDeepOneHybrid2();
        }
        else
        {
            enemy1.SetActive(false);
            enemy2.SetActive(true);
            enemy3.SetActive(true);
            enemyTrunSymbol1.SetActive(false);
            enemyTrunSymbol2.SetActive(true);
            enemyTrunSymbol3.SetActive(true);
            BattleManager.Instance.SetDeepOneHybrid2();
            BattleManager.Instance.SetDeepOneHybrid3();
        }
        player.SetActive(false);
        camera.transform.position = camBattlePos;
        Invoke("BattleDialog", 2f);
    }
    void BattleDialog()
    {
        InteractionController.Instance.BattleDialog("start");
    }

    //전투 
    public void GotoBattle()
    {
        roadBattleBackground.SetActive(true);
        sewerBattleBackground.SetActive(false);
        DataBaseManager.nowPlace = "BattleRoad";
        BattleManager.Instance.StartReload();
        battleUI.SetActive(true);
        cameraMove.enabled = true;
        //DialogManager.SetActive(false);
        cameraManager.enabled = false;
        camera.transform.position = camBattlePos;
        Invoke("SetBattle", 3);
        battleMap.SetActive(true);
    }
    public void GotoBattleSewer()
    {
        DataBaseManager.sewerBattleChecker = true;
        DataBaseManager.sewerBattleEndCheck = false;
        battleRoadLight.SetActive(false);
        battleObject.transform.localPosition = battlePlayerSewer;
        BattleManager.Instance.OriginPointChange();
        if (DataBaseManager.sewerEnemyCounter == 1)
        {
            sewerEnemy1.SetActive(true);
            sewerEnemy2.SetActive(false);
            enemyTrunSymbol1.SetActive(true);
            enemyTrunSymbol2.SetActive(false);
            enemyTrunSymbol3.SetActive(false);
            BattleManager.Instance.SetDeepOneHybrid4();
        }
        else if (DataBaseManager.sewerEnemyCounter == 2)
        {
            sewerEnemy1.SetActive(false);
            sewerEnemy2.SetActive(true);
            enemyTrunSymbol1.SetActive(false);
            enemyTrunSymbol2.SetActive(true);
            enemyTrunSymbol3.SetActive(false);
            BattleManager.Instance.SetDeepOne1();
        }
        else
        {
            sewerEnemy1.SetActive(true);
            sewerEnemy2.SetActive(true);
            enemyTrunSymbol1.SetActive(true);
            enemyTrunSymbol2.SetActive(true);
            enemyTrunSymbol3.SetActive(false);
            BattleManager.Instance.SetDeepOneHybrid4();
            BattleManager.Instance.SetDeepOne1();
        }
        roadBattleBackground.SetActive(false);
        sewerBattleBackground.SetActive(true);
        BattleManager.Instance.StartReload();
        battleUI.SetActive(true);
        cameraMove.enabled = false;
        //DialogManager.SetActive(false);
        cameraManager.enabled = false;
        camera.transform.position = camBattleSewerPos;
        Invoke("SetBattle", 3);
        battleMap.SetActive(true);
    }


    public void EndBattle()
    {
        battleUI.SetActive(false);
        Invoke("EndBattleInvoke", 1f);
    }
    public void EndBattleInvoke()
    {
        BattleManager.Instance.startBattle = false;
        BattleManager.Instance.battleState = "setTrun";
        enemyTrunSymbol1.transform.localPosition = new Vector2(enemyTrunSymbol1.transform.localPosition.x, -130);
        enemyTrunSymbol2.transform.localPosition = new Vector2(enemyTrunSymbol1.transform.localPosition.x, -130);
        enemyTrunSymbol3.transform.localPosition = new Vector2(enemyTrunSymbol1.transform.localPosition.x, -130);
        playerTrunSymbol.transform.localPosition = new Vector2(enemyTrunSymbol1.transform.localPosition.x, -130);
        DataBaseManager.isRollet = false;
        battleUI.SetActive(false);
        cameraMove.enabled = false;
        dialogManager.SetActive(true);
        cameraManager.enabled = true;
        battleMap.SetActive(false);
    }
    public void SetBattle()
    {
        DataBaseManager.iSRoadBattleStart = true;
        DataBaseManager.isDirecting = false;
        BattleManager.Instance.startBattle = true;
    }

    public void KeyConnect()
    {
        OrganizeKeywordFunc();
    }
    public void EndDay()
    {
        DataBaseManager.isRollet = false;
        DataBaseManager.secondisDirecting = false;
        if (DataBaseManager.timeCount == 4)
        {
            InteractionController.Instance.Start1stDetectiveOffice("Fab_EndDay");
        }
        else if (DataBaseManager.timeCount == 8)
        {
            InteractionController.Instance.Start1stDetectiveOffice("SecondDream");
        }
        else if (DataBaseManager.timeCount == 12)
        {
            InteractionController.Instance.Start1stDetectiveOffice("ThirdNightmare");
        }
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
        kateNoon.SetActive(false);
        DataBaseManager.isDirecting = false;
    }
    // 1일차 낮 의뢰자의 집 연출
    public void DoorNocking()
    {
        DataBaseManager.isDirecting = true;
        FadingBackGround.Instance.FadeInOut();
        // SoundManager.Instance.Door_Sound();
        Invoke("MoveInside", 1f);
    }
    void MoveInside()
    {
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
        player.transform.localPosition = playerOutsideTo1st;
        camera.transform.localPosition = CamOutsideTo1st;
        Invoke("InsiedDialog", 1f);
    }
    void InsiedDialog()
    {
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.Start1stClientsHouse("NockDoor");
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
        player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = player1stTo2st;
        camera.transform.localPosition = Cam1stTo2st;
        Invoke("UpstairDialog", 1f);
    }
    void UpstairDialog()
    {
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.Start1stClientsHouse("Ella_FirstUpstair");
    }
    void FirstRoomDialog()
    {
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.Start1stClientsHouse("Ella_FirstUpstair");
    }
    void Go2stForce()
    {
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = playerRoomTo2st;
        camera.transform.localPosition = camRoomTo2st;
        DataBaseManager.isActiveDialog1 = false;
        Invoke("ForceRoom", 1f);
    }
    void ForceRoom()
    {
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.Start1stClientsHouse("Aiden_key_FailForce");
    }
    // 1일차 오전 탐정사무소 연출
    void AfterNockSound()
    {
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.Start1stDetectiveOffice("Directing_Nock");
    }
    public void DetectiveOfficeFade(string Connect)
    {
        FadingBackGround.Instance.FadeIn();
        Invoke(Connect, 1f);
    }
    public void DetectiveOfficeFadeOut(string Connect)
    {
        FadingBackGround.Instance.FadeOut();
        Invoke(Connect, 1f);
    }
    public void DetectiveOfficeFadeIn(string Connect)
    {
        FadingBackGround.Instance.FadeInOut();
        Invoke(Connect, 1.5f);
    }
    public void EndDirecting()
    {
        sitPlayer.SetActive(false);
        sitPlayer2.SetActive(false);
        player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
        player.transform.localPosition = new Vector3(-797.556f, 2.183268f);
        player.SetActive(true);
        if (DataBaseManager.timeCount == 1)
        {
            newspaper.DOFade(1, 0.1f);
        }
    }
    public void NockDirecting()
    {
        camera.transform.DOMoveX(-790.3f, 1);
        player.transform.position = new Vector2(-796.7f, player.transform.position.y);
        player.SetActive(false);
        sitNoNewspaperPlayer.SetActive(true);
        sitElla.SetActive(true);
        Invoke("AfterNockDirecting", 1f);
    }
    void AfterNockDirecting()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Directing_AfterDoor");
    }
    public void AfterSelectDirecting()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Accept_AfterSelectYes");
    }
    public void AcceptAfterSelectNo()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Accept_AfterSelectNo");
    }
    public void EndSelectDirecting()
    {
        player.SetActive(true);
        sitNoNewspaperPlayer.SetActive(false);
        sitElla.SetActive(false);
        DataBaseManager.isDirecting = false;
    }

    private List<System.Action> functionList = new List<System.Action>();
    public void OrganizeKeyword()
    {
        if (DataBaseManager.timeCount == 4)
        {
            functionList.Add(FabFirstDialog);
        }
        else if (DataBaseManager.timeCount == 8)
        {
            functionList.Add(Fab2ndConnectStart);
        }
        else if (DataBaseManager.timeCount == 12)
        {
            functionList.Add(FabFirstDialog);
        }
        if (DataBaseManager.intelAiden1 == true && intelAiden1KeyCheck == false)
        {
            intelAiden1KeyCheck = true;
            functionList.Add(FabAiden1);
        }
        if (DataBaseManager.intelSafe1 == true && intelSafe1KeyCheck == false)
        {
            intelSafe1KeyCheck = true;
            functionList.Add(FabSafe1);
        }
        if (DataBaseManager.intelNightmare1 == true && intelNightmare1KeyCheck == false)
        {
            intelNightmare1KeyCheck = true;
            functionList.Add(FabNightmare1);
        }
        if (DataBaseManager.intelNightmare2 == true && intelNightmare2KeyCheck == false)
        {
            intelNightmare2KeyCheck = true;
            functionList.Add(FabNightmare2);
        }
        if (DataBaseManager.intelNightmare3 == true && intelNightmare3KeyCheck == false)
        {
            intelNightmare3KeyCheck = true;
            functionList.Add(FabNightmare3);
        }
        if (DataBaseManager.intelNightmare4 == true && intelNightmare4KeyCheck == false)
        {
            intelNightmare4KeyCheck = true;
            functionList.Add(FabNightmare4);
        }
        if (DataBaseManager.intelInsomnia1 == true && intelInsomnia1KeyCheck == false)
        {
            intelInsomnia1KeyCheck = true;
            functionList.Add(FabInsomnia1);
        }
        if (DataBaseManager.intelInsomnia2 == true && intelInsomnia2KeyCheck == false)
        {
            intelInsomnia2KeyCheck = true;
            functionList.Add(FabInsomnia2);
        }
        if (DataBaseManager.intelPlanetaryParade1 == true && intelPlanetaryParade1KeyCheck == false)
        {
            intelPlanetaryParade1KeyCheck = true;
            functionList.Add(FabPlanetarySequence1);
        }
        if (DataBaseManager.intelUniversity1 == true && intelUniversity1KeyCheck == false)
        {
            intelUniversity1KeyCheck = true;
            functionList.Add(FabUniv1);
        }
        if (DataBaseManager.intelMeiv2 == true && intelMeiv2KeyCheck == false)
        {
            intelMeiv2KeyCheck = true;
            functionList.Add(FabMeiv2);
        }
        if (DataBaseManager.intelMissingPeople1 == true && intelMissingPeople1KeyCheck == false)
        {
            intelMissingPeople1KeyCheck = true;
            functionList.Add(FabMissingPeople);
        }
        if (DataBaseManager.intelSewerGhostStory1 == true && intelSewerGhostStory1KeyCheck == false)
        {
            intelSewerGhostStory1KeyCheck = true;
            functionList.Add(FabGhostStory1);
        }
        if (DataBaseManager.intelSewerGhostStory2 == true && intelSewerGhostStory2KeyCheck == false)
        {
            intelSewerGhostStory2KeyCheck = true;
            functionList.Add(FabGhostStory2);
        }
        if (DataBaseManager.intelCreepyEyes1 == true && intelCreepyEyes1KeyCheck == false)
        {
            intelCreepyEyes1KeyCheck = true;
            functionList.Add(FabCreepyEye);
        }
        if (DataBaseManager.intelSewerWorker1 == true && intelSewerWorker1KeyCheck == false)
        {
            intelSewerWorker1KeyCheck = true;
            functionList.Add(FabWorker1);
        }
        if (DataBaseManager.intelFishySmell1 == true && intelFishySmell1KeyCheck == false)
        {
            intelFishySmell1KeyCheck = true;
            functionList.Add(FabFishySmell1);
        }
        if (DataBaseManager.intelFishySmell2 == true && intelFishySmell2KeyCheck == false)
        {
            intelFishySmell2KeyCheck = true;
            functionList.Add(FabFishySmell2);
        }
        if (DataBaseManager.intelFishySmell3 == true && intelFishySmell3KeyCheck == false)
        {
            intelFishySmell3KeyCheck = true;
            functionList.Add(FabFishySmell3);
        }
        if (DataBaseManager.intelMurderCase1 == true && intelMurderCase1KeyCheck == false)
        {
            intelMurderCase1KeyCheck = true;
            functionList.Add(FabWitness);
        }
        if (DataBaseManager.intelSewerWorker4 == true && intelSewerWorker4KeyCheck == false)
        {
            intelSewerWorker4KeyCheck = true;
            functionList.Add(FabOwnerofUnifrom);
        }
        if (DataBaseManager.intelMurderCase4 == true && intelMurderCase4KeyCheck == false)
        {
            intelMurderCase4KeyCheck = true;
            functionList.Add(FabCauseofDeath);
        }
        if (DataBaseManager.intelMurderCase5 == true && intelMurderCase5KeyCheck == false)
        {
            intelMurderCase5KeyCheck = true;
            functionList.Add(FabWebbedFeet);
        }
        if (DataBaseManager.intelMurderCase6 == true && intelMurderCase6KeyCheck == false)
        {
            intelMurderCase6KeyCheck = true;
            functionList.Add(FabRunway);
        }
        if (DataBaseManager.intelMurderCase2 == true && intelMurderCase2KeyCheck == false)
        {
            intelMurderCase2KeyCheck = true;
            functionList.Add(FabFishySmellMissing);
        }
        if (DataBaseManager.intelMurderCase3 == true && intelMurderCase3KeyCheck == false)
        {
            intelMurderCase3KeyCheck = true;
            functionList.Add(FabTheClupet);
        }
        if ((DataBaseManager.intelMurderCase2 == true || DataBaseManager.intelMurderCase3 == true) && bothKeyCheck == false)
        {
            bothKeyCheck = true;
            functionList.Add(FabBoth);
        }
        if ((DataBaseManager.intelSewer1 == true || DataBaseManager.intelSewer2 == true) && intelSewer2KeyCheck == false)
        {
            intelSewer2KeyCheck = true;
            functionList.Add(FabWarnOrRoot);
        }
        if ((DataBaseManager.swainDeathKeyword == true) && swainSewerKeyCheck == false)
        {
            swainSewerKeyCheck = true;
            functionList.Add(FabSwainSewer);
        }
    }
    public void OrganizeKeywordFunc()
    {
        if (functionList.Count > 0)
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
            DataBaseManager.endDay = true;
        }
        // 함수 리스트에서 무작위로 함수 선택
    }

    private void FabFirstDialog()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_FirstDialog");
    }
    private void Fab2ndConnectStart()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_2ndConnectStart");
    }
    private void FabAiden1()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Aiden1");
    }
    private void FabSafe1()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Safe1");
    }
    private void FabNightmare1()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Nightmare1");
    }
    private void FabNightmare2()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Nightmare2");
    }
    private void FabNightmare3()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Nightmare3");
    }
    private void FabNightmare4()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Nightmare4");
    }
    private void FabInsomnia1()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Insomnia1");
    }
    private void FabInsomnia2()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Insomnia2");
    }
    private void FabPlanetarySequence1()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_PlanetarySequence1");
    }
    private void FabUniv1()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Univ1");
    }
    private void FabMeiv2()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Meiv2");
    }
    private void FabMissingPeople()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_MissingPeople");
    }
    private void FabGhostStory1()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_GhostStory1");
    }
    private void FabGhostStory2()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_GhostStory2");
    }
    private void FabCreepyEye()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_CreepyEye");
    }
    private void FabWorker1()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Worker1");
    }
    private void FabFishySmell1()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_FishySmell1");
    }
    private void FabFishySmell2()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_FishySmell2");
    }
    private void FabFishySmell3()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_FishySmell3");
    }
    private void FabWitness()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Witness");
    }
    private void FabOwnerofUnifrom()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_OwnerofUnifrom");
    }
    private void FabCauseofDeath()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_CauseofDeath");
    }
    private void FabWebbedFeet()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_WebbedFeet");
    }
    private void FabRunway()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Runway");
    }
    private void FabFishySmellMissing()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_FishySmellMissing");
    }
    private void FabTheClupet()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_TheClupet");
    }
    private void FabBoth()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_Both");
    }
    private void FabWarnOrRoot()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_WarnOrRoot");
    }
    private void FabSwainSewer()
    {
        InteractionController.Instance.Start1stDetectiveOffice("Fab_SwainSewer");
    }

    bool intelAiden1KeyCheck = false;
    bool intelSafe1KeyCheck = false;
    bool intelNightmare1KeyCheck = false;
    bool intelNightmare2KeyCheck = false;
    bool intelNightmare3KeyCheck = false;
    bool intelNightmare4KeyCheck = false;
    bool intelInsomnia1KeyCheck = false;
    bool intelInsomnia2KeyCheck = false;
    bool intelPlanetaryParade1KeyCheck = false;
    bool intelUniversity1KeyCheck = false;
    bool intelMeiv2KeyCheck = false;
    bool intelMissingPeople1KeyCheck = false;
    bool intelSewerGhostStory1KeyCheck = false;
    bool intelSewerGhostStory2KeyCheck = false;
    bool intelCreepyEyes1KeyCheck = false;
    bool intelSewerWorker1KeyCheck = false;
    bool intelFishySmell1KeyCheck = false;
    bool intelFishySmell2KeyCheck = false;
    bool intelFishySmell3KeyCheck = false;
    bool intelMurderCase1KeyCheck = false;
    bool intelSewerWorker4KeyCheck = false;
    bool intelMurderCase4KeyCheck = false;
    bool intelMurderCase5KeyCheck = false;
    bool intelMurderCase6KeyCheck = false;
    bool intelMurderCase2KeyCheck = false;
    bool intelMurderCase3KeyCheck = false;
    bool bothKeyCheck = false;
    bool intelSewer2KeyCheck = false;
    bool swainSewerKeyCheck = false;
}
