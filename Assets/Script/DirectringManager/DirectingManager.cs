using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class DirectingManager : MonoBehaviour
{
    public GameObject BoxMus;
    public GameObject BattleM;
    public CameraManager cameraManager;
    public GameObject DialogManager;
    Vector2 player_SewerRight = new Vector3(-1194.19f, -193.57f);
    Vector3 Cam_SewerRight = new Vector3(-1217.02f, -195f, -15);
    public GameObject Back;
    public Image BackGround;
    public GameObject SewerBattleBackGround;
    public GameObject RoadBattleBackGround;
    public GameObject battleObject;
    Vector2 BattlePlayer_Road = new Vector3(-213.58f, 209.2012f);
    Vector2 BattlePlayer_Sewer = new Vector3(-220.9f, 204.5f);
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
    Vector3 Cam_BattleSewerPos = new Vector3(-776.77f, 119.79f, -15);

    bool Thrid;
    bool twice;
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
            InteractionController.Instance.Start_1st_DetectiveOffice("start");
        }
        else
        {
            DataBaseManager.fst_Detective_EndFirstDialog = true;
        }

    }
    public GameObject endGame;
    bool isGo1stBattle;
    public GameObject VisionLessField;
    public GameObject NoteButton;
    void Update()
    {
        if (DataBaseManager.isDebuff_Dyslexia == true && NoteButton.activeSelf == true)
        {
            NoteButton.SetActive(false);
        }
        else if (DataBaseManager.isDebuff_Dyslexia == false && NoteButton.activeSelf == false)
        {
            NoteButton.SetActive(true);
        }
        if (DataBaseManager.isDebuff_VisionLoss == true && VisionLessField.activeSelf == false)
        {
            VisionLessField.SetActive(true);
        }
        if (DataBaseManager.nowHP <= 0 || DataBaseManager.nowSan <= 0)
        {
            endGame.SetActive(true);
        }
        else
        {
            endGame.SetActive(false);
        }
        if (DataBaseManager.fst_Detectiv_TimeOn >= 2)
        {
            DataBaseManager.isDirecting = true;
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                SoundManager.Instance.Nock_Sound();
                // 노크소리 발동
                DataBaseManager.isActiveDialog2 = true;
                Invoke("AfterNockSound", 1);
            }
        }
        if (DataBaseManager.fst_Detective_EndFirstDialog == true && DataBaseManager.isActiveDialog1 == false)
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
        if (DataBaseManager.firstClientsHouse == true && DataBaseManager.TimeCount < 6)
        {
            DataBaseManager.firstClientsHouse = false;
            DataBaseManager.firstClientsHouseEnd = true;
            InteractionController.Instance.Start_1st_ClientsHouse("FirstArrive");
        }
        if (DataBaseManager.StrDialogOn == true && DataBaseManager.isActiveDialog1 == false)
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
            Invoke("KeyConnect", 0.5f);
        }
        if (DataBaseManager.EndDay == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.StoryDirecting = true;
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
        if ((DataBaseManager.TimeCount == 4) && DataBaseManager.nowPlace == "DetectiveOffice" && once == false)
        {
            DirectingManager.Instance.OrganizeKeyword();
            once = true;
            player.SetActive(false);
            sit_NoNewsPaperPlayer.SetActive(true);
            if (DataBaseManager.isBar == true || DataBaseManager.isSewerOffice == true)
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
        if ((DataBaseManager.TimeCount == 8 && DataBaseManager.nowPlace == "DetectiveOffice" && twice == false))
        {
            DataBaseManager.StoryDirecting = true;
            DirectingManager.Instance.OrganizeKeyword();
            twice = true;
            player.SetActive(false);
            sit_NoNewsPaperPlayer.SetActive(true);
            if (DataBaseManager.isBar == true || DataBaseManager.isSewerOffice == true)
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
        if ((DataBaseManager.TimeCount == 12 && DataBaseManager.nowPlace == "DetectiveOffice" && Thrid == false))
        {
            DataBaseManager.StoryDirecting = true;
            DirectingManager.Instance.OrganizeKeyword();
            Thrid = true;
            player.SetActive(false);
            sit_NoNewsPaperPlayer.SetActive(true);
            if (DataBaseManager.isBar == true || DataBaseManager.isSewerOffice == true)
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
        if (DataBaseManager.TimeCount == 5 && DataBaseManager.SecondDayDialog == false && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.SecondDayDialog = true;
            Invoke("ScendDayStartDialog", 3f);
        }
        if (DataBaseManager.TimeCount == 9 && DataBaseManager.SecondDayDialog == false && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.SecondDayDialog = true;
            Invoke("ThirdDayStartDialog", 3f);
        }
        if (DataBaseManager.TimeCount == 13 && DataBaseManager.SecondDayDialog == false && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.SecondDayDialog = true;
            Invoke("FourthDayStartDialog", 3f);
        }
        if (DataBaseManager.AfterSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.AfterSanCheck = false;
            DataBaseManager.secondisDirecting = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "Fab_Case1");
        }
        if (DataBaseManager.Case2IntCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Case2IntCheck = false;
            Rollet.Instance.setRollet("INT : Check", "Association", DataBaseManager.intl, "Case2IntCheck");
        }
        if (DataBaseManager.DaveCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.DaveCheck = false;
            selectionUIManager.Instance.EndDialog();
        }
        //전투
        if (DataBaseManager.Battle_San == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Battle_San = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "RoadBattle1");
        }
        if (DataBaseManager.isBattleBeforeDialog == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.isBattleBeforeDialog = false;
            FadingBackGround.Instance.FadeInOut();
            Invoke("GotoBattle", 1.1f);
            BattleManager.Instance.CloseButtonUI();
        }

        if (DataBaseManager.EndBattle == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.AfterBattle = true;
            DialogDatabaseManager.instance.Check = true;
            DataBaseManager.EndBattle = false;
            isGo1stBattle = true;
            DirectingManager.Instance.EndBattle();
            MapManager.Instance.MapOn();
        }
        //하수도 
        if (DataBaseManager.StartSewerBattle == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.StartSewerBattle = false;
            FadingBackGround.Instance.FadeInOut();
            Invoke("GotoBattleSewer", 1.1f);
            BattleManager.Instance.CloseButtonUI();
        }
        if (DataBaseManager.InSewer_StealthSucc == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.InSewer_StealthSucc = false;
            FadingBackGround.Instance.FadeInOut(); // 이거 문제 체크
            Invoke("InSewerFabianMove", 1.1f);
        }
        if (DataBaseManager.InSewer_StealthFail == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.InSewer_StealthFail = false;
            FadingBackGround.Instance.FadeInOut();
            Invoke("GotoBattleSewer", 1.1f);
            BattleManager.Instance.CloseButtonUI();
        }
        if (DataBaseManager.Sewer_FirstIn == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.StoryDirecting = true;
            DataBaseManager.Sewer_FirstIn = false;
            InteractionController.Instance.InSewerDialog("Sewer_FirstIn");
        }
        if (DataBaseManager.Sewer_FirstInSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.StoryDirecting = false;
            DataBaseManager.Sewer_FirstInSanCheck = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "Sewer_FirstInSan");
        }
        if (DataBaseManager.Sewer_DeepOneFirstSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Sewer_DeepOneFirstSanCheck = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "DeepOneFirstSan");
        }

        //2일차 낮 강제 이벤트
        if (DataBaseManager.snd_Detective_NoonEvent == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.snd_Detective_NoonEvent = false;
            Invoke("NoonEventPhoneCall", 1f);
        }
        if (DataBaseManager.NoonEvent_MovetoClient == true && DataBaseManager.isActiveDialog1 == false)
        {
            Invoke("NoonEventClientsFirst", 12f);
            DataBaseManager.NoonEvent_MovetoClient = false;
            Invoke("MoveToClient", 0.7f);
        }
        if (DataBaseManager.NoonEventSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.NoonEventSanCheck = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "NoonEvent");
        }
        if (DataBaseManager.DeadBodyFirstLook == true && DataBaseManager.isActiveDialog1 == false)
        {
            if ((DataBaseManager.Intel_MissingPeople1 == true || DataBaseManager.Intel_MissingPeople2 == true) && DataBaseManager.DeadBodyLook_MissingDialog == false)
            {
                DataBaseManager.DeadBodyLook_MissingDialog = true;
                InteractionController.Instance.Start_2nd_NoonEvent("NoonEvent_DeadBody_Look_Missing");
            }
            else if ((DataBaseManager.Intel_FishySmell3 == true || DataBaseManager.Intel_FishySmell4 == true || DataBaseManager.Intel_SewerWorker3 == true) && DataBaseManager.DeadBodyLook_FishySmellDialog == false)
            {
                DataBaseManager.DeadBodyLook_FishySmellDialog = true;
                InteractionController.Instance.Start_2nd_NoonEvent("NoonEvent_DeadBody_Look_FishySmell");
            }
        }
        if (DataBaseManager.DeadBody_GotoSwere == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.DeadBody_GotoSwere = false;
            DataBaseManager.StoryDirecting = true;
            MapManager.Instance.SuccAnalysis();
        }
        if (DataBaseManager.KaneSlimeSan == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.KaneSlimeSan = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "KaneSan");
        }
        if (DataBaseManager.BarArrive == true && DataBaseManager.isActiveDialog1 == false)
        {
            BoxMus.SetActive(true);
            Invoke("ArriveBar2nd", 4f);
            DataBaseManager.BarArrive = false;
            DataBaseManager.StoryDirecting = true;
            DataBaseManager.Maeve_FirstDialog = true;
        }
        if (DataBaseManager.BarMove == true && DataBaseManager.isActiveDialog1 == false)
        {
            Invoke("BarMove", 0.3f);
            DataBaseManager.BarMove = false;
        }
        if (DataBaseManager.BarDrinkSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.BarDrinkSanCheck = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "DrinkSan");
        }
        if (DataBaseManager.ThirdDayDreamSan == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.ThirdDayDreamSan = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "DayTwoCase2San");
        }
        if (DataBaseManager.Day_4_case1San == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Day_4_case1San = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "Fab_4th_QuestO_SanCheck");
        }
        if (DataBaseManager.Day_4_case1Health == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Day_4_case1Health = false;
            Rollet.Instance.setRollet("Health : Check", "Health", DataBaseManager.hp, "dialog", "Day_4_case1Health");
        }
        if (DataBaseManager.MoveSewerOfficeWindow == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.MoveSewerOfficeWindow = false;
            Invoke("WindowMove", 1.5f);
            FadingBackGround.Instance.FadeInOut();
        }
        if (DataBaseManager.AfterDaveDeathFirstSlum == true && DataBaseManager.nowPlace == "Slum" && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.AfterDaveDeathFirstSlum = false;
            if ((DataBaseManager.TimeCount == 13 && DataBaseManager.IsMorningMapMove == true) || (DataBaseManager.TimeCount == 9 && DataBaseManager.IsMorningMapMove == true) || (DataBaseManager.TimeCount == 5 && DataBaseManager.IsMorningMapMove == true))
            {
                Invoke("AfterDaveDeathDailog", 6f);
            }
            else
            {
                Invoke("AfterDaveDeathDailog", 10f);
            }
        }
        //하수도 내부
        if (DataBaseManager.InsmusRhtoric == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.InsmusRhtoric = false;
            Rollet.Instance.setRollet("??? : Give Excuse", "Rhetoric", DataBaseManager.rhetoricPoint, "dialog");
        }
        if (DataBaseManager.JournalSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.JournalSanCheck = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "JournalSan");
        }
        if (DataBaseManager.SewerBookSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.SewerBookSanCheck = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "BookSan");
        }
        if (DataBaseManager.SewerBookOccultCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.SewerBookOccultCheck = false;
            Rollet.Instance.setRollet("Book : Decryption", "Occult", DataBaseManager.occultPoint, "dialog");
        }
        if (DataBaseManager.SewerSymbolOccultCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.SewerSymbolOccultCheck = false;
            Rollet.Instance.setRollet("Symbol : Decryption", "Occult", DataBaseManager.occultPoint, "dialog");
        }
        if (DataBaseManager.SymbolStrSanCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.SymbolStrSanCheck = false;
        }
        if (DataBaseManager.nowHP <= 0 && DataBaseManager.TimeCount > 1)
        {
            //죽었을때
            //DataBaseManager.EndDemo = true;
        }
        if (DataBaseManager.Ending_Swin == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Ending_Swin = false;
            Rollet.Instance.setRollet("Fabian : Swimming", "Swin", DataBaseManager.swimingPoint, "dialog");
        }
        if (DataBaseManager.Ending_LadderSan == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Ending_LadderSan = false;
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "EndingLedderSan");
        }
        if (DataBaseManager.Ending_BreakSymbol == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Ending_BreakSymbol = false;
            InteractionController.Instance.InSewerDialog("Ending_BreakSymbol");
        }
        if (DataBaseManager.Ending_isPromise == true && DataBaseManager.isActiveDialog1 == false)
        {
            Camera.transform.position = new Vector3(-806.7f, 0.3f, -16);
            player.SetActive(false);
            sit_NoNewsPaperPlayer.SetActive(true);
            DataBaseManager.Ending_isPromise = false;
            InteractionController.Instance.InSewerDialog("Ending_isPromise");
        }
        if (DataBaseManager.Ending_AfterPromiseDialog == true && DataBaseManager.isActiveDialog1 == false && DataBaseManager.AlbertDeathKeyword == true)
        {
            DataBaseManager.Ending_AfterPromiseDialog = false;
            InteractionController.Instance.InSewerDialog("Ending_IfNoPromiseAlbertDeath");
        }
        if (DataBaseManager.Ending_SwainDeathCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Ending_SwainDeathCheck = false;
            if (DataBaseManager.SwainDeathKeyword == true)
            {
                InteractionController.Instance.InSewerDialog("Ending_SwainDeathCheck");
            }
            else
            {
                InteractionController.Instance.InSewerDialog("Ending_RequestCheck");
            }

        }
        if (DataBaseManager.Ending_RequestCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.Ending_RequestCheck = false;
            InteractionController.Instance.InSewerDialog("Ending_RequestCheck");
        }
        if (DataBaseManager.EndingDeathCheck == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.EndingDeathCheck = false;
            if (DataBaseManager.EnemyDeathCount == 0)
            {
                // 결말 장면 삽입
            }
            else
            {
                int DeathCheck = Random.Range(1, DataBaseManager.EnemyDeathCount * 5);
                if (DeathCheck <= DataBaseManager.EnemyDeathCount * 5)
                {
                    int LuckInt = Random.Range(1, DataBaseManager.luk);
                    if (LuckInt <= DataBaseManager.luk)
                    {
                        //  결말 장면 삽입.
                    }
                    else
                    {
                        DataBaseManager.EndingDoorCheck = true;
                        player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
                        player.transform.position = new Vector3(-796.8f, 2.500006f, -1);
                        SoundManager.Instance.Nock_Sound();
                        Invoke("EndingNockSound", 0.5f);

                    }
                }
            }
        }
    }
    bool once = false;
    void EndingNockSound()
    {
        player.SetActive(true);
        sit_NoNewsPaperPlayer.SetActive(false);
        DataBaseManager.StoryDirecting = false;
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
        BoxMus.SetActive(false);
        MapManager.Instance.BarFabianMove();
    }
    public void InSewerFabianMove()
    {
        TimeManagere.Instance.MakeSewerMap_Nomal();
        DataBaseManager.isDirecting = false;
        player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = player_SewerRight;
        Camera.transform.localPosition = Cam_SewerRight;
        TimeManagere.Instance.DeletRightSewerPotal();
    }
    public void MeaveDialog()
    {
        InteractionController.Instance.Start_1st_Bar("Meave2ndDialog");
    }
    public void AfterDaveDeathDailog()
    {
        InteractionController.Instance.Start_1st_Slum("AfterDaveDeath");
    }
    public void ArriveBar2nd()
    {
        InteractionController.Instance.Start_1st_Bar("BarArrive");
    }
    public void NoonEventClientsFirst()
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
    public void NoonEventPhoneCall()
    {
        InteractionController.Instance.Start_2nd_NoonEvent("NoonEvent_FirstDailog_Detective");
    }

    //2일차
    public void ScendDayStartDialog()
    {
        if (DataBaseManager.Intel_Nightmare3 == true || DataBaseManager.Intel_Nightmare3 == true)
        {
            InteractionController.Instance.Start_2nd_DetectiveOffice("Fab_Case1");
        }
        else if (DataBaseManager.Intel_QuestHouse1 == true)
        {
            InteractionController.Instance.Start_2nd_DetectiveOffice("Fab_Case2");
        }
        else
        {
            InteractionController.Instance.Start_2nd_DetectiveOffice("Fab_Case3");
        }
    }
    public void ThirdDayStartDialog()
    {
        Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "ThirdDayStartSan");
    }
    public void FourthDayStartDialog()
    {
        Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.nowSan, "FourthDayStartSan");
    }
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;

    // 1일차
    public void BeforeBattle()
    {
        battleObject.transform.localPosition = BattlePlayer_Road;
        BattleManager.Instance.OriginPointChange();
        BattleRoadLight.SetActive(true);
        if (DataBaseManager.TimeCount < 5)
        {
            Enemy1.SetActive(true);
            Enemy2.SetActive(false);
            Enemy3.SetActive(false);
            EnemyTrunSymbol_1.SetActive(true);
            EnemyTrunSymbol_2.SetActive(false);
            EnemyTrunSymbol_3.SetActive(false);
            BattleManager.Instance.set_DeepOneHybrid1();
        }
        else if (DataBaseManager.TimeCount < 9)
        {
            Enemy1.SetActive(true);
            Enemy2.SetActive(true);
            Enemy3.SetActive(false);
            EnemyTrunSymbol_1.SetActive(true);
            EnemyTrunSymbol_2.SetActive(true);
            EnemyTrunSymbol_3.SetActive(false);
            BattleManager.Instance.set_DeepOneHybrid1();
            BattleManager.Instance.set_DeepOneHybrid2();
        }
        else
        {
            Enemy1.SetActive(false);
            Enemy2.SetActive(true);
            Enemy3.SetActive(true);
            EnemyTrunSymbol_1.SetActive(false);
            EnemyTrunSymbol_2.SetActive(true);
            EnemyTrunSymbol_3.SetActive(true);
            BattleManager.Instance.set_DeepOneHybrid2();
            BattleManager.Instance.set_DeepOneHybrid3();
        }
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
    public GameObject EnemyTrunSymbol_1;
    public GameObject EnemyTrunSymbol_2;
    public GameObject EnemyTrunSymbol_3;
    public GameObject BattleRoadLight;
    //전투 
    public void GotoBattle()
    {
        RoadBattleBackGround.SetActive(true);
        SewerBattleBackGround.SetActive(false);
        DataBaseManager.nowPlace = "BattleRoad";
        BattleManager.Instance.startReload();
        battleUI.SetActive(true);
        cameraMove.enabled = true;
        //DialogManager.SetActive(false);
        cameraManager.enabled = false;
        Camera.transform.position = Cam_BattlePos;
        Invoke("SetBattle", 3);
        BattleM.SetActive(true);
    }
    public GameObject SewerEnemy1;
    public GameObject SewerEnemy2;
    public void GotoBattleSewer()
    {
        DataBaseManager.SewerBattleChecker = true;
        DataBaseManager.SewerBattleEndCheck = false;
        BattleRoadLight.SetActive(false);
        battleObject.transform.localPosition = BattlePlayer_Sewer;
        BattleManager.Instance.OriginPointChange();
        if (DataBaseManager.SewerEnemyCounter == 1)
        {
            SewerEnemy1.SetActive(true);
            SewerEnemy2.SetActive(false);
            EnemyTrunSymbol_1.SetActive(true);
            EnemyTrunSymbol_2.SetActive(false);
            EnemyTrunSymbol_3.SetActive(false);
            BattleManager.Instance.set_DeepOneHybrid4();
        }
        else if (DataBaseManager.SewerEnemyCounter == 2)
        {
            SewerEnemy1.SetActive(false);
            SewerEnemy2.SetActive(true);
            EnemyTrunSymbol_1.SetActive(false);
            EnemyTrunSymbol_2.SetActive(true);
            EnemyTrunSymbol_3.SetActive(false);
            BattleManager.Instance.set_DeepOne1();
        }
        else
        {
            SewerEnemy1.SetActive(true);
            SewerEnemy2.SetActive(true);
            EnemyTrunSymbol_1.SetActive(true);
            EnemyTrunSymbol_2.SetActive(true);
            EnemyTrunSymbol_3.SetActive(false);
            BattleManager.Instance.set_DeepOneHybrid4();
            BattleManager.Instance.set_DeepOne1();
        }
        RoadBattleBackGround.SetActive(false);
        SewerBattleBackGround.SetActive(true);
        BattleManager.Instance.startReload();
        battleUI.SetActive(true);
        cameraMove.enabled = false;
        //DialogManager.SetActive(false);
        cameraManager.enabled = false;
        Camera.transform.position = Cam_BattleSewerPos;
        Invoke("SetBattle", 3);
        BattleM.SetActive(true);
    }

    public GameObject PlayerTrunSymbol;
    public void EndBattle()
    {
        battleUI.SetActive(false);
        Invoke("EndBattleInvoke", 1f);
    }
    public void EndBattleInvoke()
    {
        BattleManager.Instance.startBattle = false;
        BattleManager.Instance.battleState = "setTrun";
        EnemyTrunSymbol_1.transform.localPosition = new Vector2(EnemyTrunSymbol_1.transform.localPosition.x, -130);
        EnemyTrunSymbol_2.transform.localPosition = new Vector2(EnemyTrunSymbol_1.transform.localPosition.x, -130);
        EnemyTrunSymbol_3.transform.localPosition = new Vector2(EnemyTrunSymbol_1.transform.localPosition.x, -130);
        PlayerTrunSymbol.transform.localPosition = new Vector2(EnemyTrunSymbol_1.transform.localPosition.x, -130);
        DataBaseManager.isRollet = false;
        battleUI.SetActive(false);
        cameraMove.enabled = false;
        DialogManager.SetActive(true);
        cameraManager.enabled = true;
        BattleM.SetActive(false);
    }
    public void SetBattle()
    {
        DataBaseManager.ISRoadBattleStart = true;
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
        if (DataBaseManager.TimeCount == 4)
        {
            InteractionController.Instance.Start_1st_DetectiveOffice("Fab_EndDay");
        }
        else if (DataBaseManager.TimeCount == 8)
        {
            InteractionController.Instance.Start_1st_DetectiveOffice("SecondDream");
        }
        else if (DataBaseManager.TimeCount == 12)
        {
            InteractionController.Instance.Start_1st_DetectiveOffice("ThirdNightmare");
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
        Kate_noon.SetActive(false);
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
    void AfterNockSound()
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
        if (DataBaseManager.TimeCount == 1)
        {
            Newspaper.DOFade(1, 0.1f);
        }
    }
    public void Nock_Directing()
    {
        Camera.transform.DOMoveX(-790.3f, 1);
        player.transform.position = new Vector2(-796.7f, player.transform.position.y);
        player.SetActive(false);
        sit_NoNewsPaperPlayer.SetActive(true);
        sit_Ella.SetActive(true);
        Invoke("AfterNockDirecting", 1f);
    }
    void AfterNockDirecting()
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
        if (DataBaseManager.TimeCount == 4)
        {
            functionList.Add(Fab_FirstDialog);
        }
        else if (DataBaseManager.TimeCount == 8)
        {
            functionList.Add(Fab_2ndConnectStart);
        }
        else if (DataBaseManager.TimeCount == 12)
        {
            functionList.Add(Fab_FirstDialog);
        }
        if (DataBaseManager.Intel_Aiden1 == true && Intel_Aiden1KeyCheck == false)
        {
            Intel_Aiden1KeyCheck = true;
            functionList.Add(Fab_Aiden1);
        }
        if (DataBaseManager.Intel_Safe1 == true && Intel_Safe1KeyCheck == false)
        {
            Intel_Safe1KeyCheck = true;
            functionList.Add(Fab_Safe1);
        }
        if (DataBaseManager.Intel_Nightmare1 == true && Intel_Nightmare1KeyCheck == false)
        {
            Intel_Nightmare1KeyCheck = true;
            functionList.Add(Fab_Nightmare1);
        }
        if (DataBaseManager.Intel_Nightmare2 == true && Intel_Nightmare2KeyCheck == false)
        {
            Intel_Nightmare2KeyCheck = true;
            functionList.Add(Fab_Nightmare2);
        }
        if (DataBaseManager.Intel_Nightmare3 == true && Intel_Nightmare3KeyCheck == false)
        {
            Intel_Nightmare3KeyCheck = true;
            functionList.Add(Fab_Nightmare3);
        }
        if (DataBaseManager.Intel_Nightmare4 == true && Intel_Nightmare4KeyCheck == false)
        {
            Intel_Nightmare4KeyCheck = true;
            functionList.Add(Fab_Nightmare4);
        }
        if (DataBaseManager.Intel_Insomnia1 == true && Intel_Insomnia1KeyCheck == false)
        {
            Intel_Insomnia1KeyCheck = true;
            functionList.Add(Fab_Insomnia1);
        }
        if (DataBaseManager.Intel_Insomnia2 == true && Intel_Insomnia2KeyCheck == false)
        {
            Intel_Insomnia2KeyCheck = true;
            functionList.Add(Fab_Insomnia2);
        }
        if (DataBaseManager.Intel_PlanetaryParade1 == true && Intel_PlanetaryParade1KeyCheck == false)
        {
            Intel_PlanetaryParade1KeyCheck = true;
            functionList.Add(Fab_PlanetarySequence1);
        }
        if (DataBaseManager.Intel_University1 == true && Intel_University1KeyCheck == false)
        {
            Intel_University1KeyCheck = true;
            functionList.Add(Fab_Univ1);
        }
        if (DataBaseManager.Intel_Meiv2 == true && Intel_Meiv2KeyCheck == false)
        {
            Intel_Meiv2KeyCheck = true;
            functionList.Add(Fab_Meiv2);
        }
        if (DataBaseManager.Intel_MissingPeople1 == true && Intel_MissingPeople1KeyCheck == false)
        {
            Intel_MissingPeople1KeyCheck = true;
            functionList.Add(Fab_MissingPeople);
        }
        if (DataBaseManager.Intel_SewerGhostStory1 == true && Intel_SewerGhostStory1KeyCheck == false)
        {
            Intel_SewerGhostStory1KeyCheck = true;
            functionList.Add(Fab_GhostStory1);
        }
        if (DataBaseManager.Intel_SewerGhostStory2 == true && Intel_SewerGhostStory2KeyCheck == false)
        {
            Intel_SewerGhostStory2KeyCheck = true;
            functionList.Add(Fab_GhostStory2);
        }
        if (DataBaseManager.Intel_CreepyEyes1 == true && Intel_CreepyEyes1KeyCheck == false)
        {
            Intel_CreepyEyes1KeyCheck = true;
            functionList.Add(Fab_CreepyEye);
        }
        if (DataBaseManager.Intel_SewerWorker1 == true && Intel_SewerWorker1KeyCheck == false)
        {
            Intel_SewerWorker1KeyCheck = true;
            functionList.Add(Fab_Worker1);
        }
        if (DataBaseManager.Intel_FishySmell1 == true && Intel_FishySmell1KeyCheck == false)
        {
            Intel_FishySmell1KeyCheck = true;
            functionList.Add(Fab_FishySmell1);
        }
        if (DataBaseManager.Intel_FishySmell2 == true && Intel_FishySmell2KeyCheck == false)
        {
            Intel_FishySmell2KeyCheck = true;
            functionList.Add(Fab_FishySmell2);
        }
        if (DataBaseManager.Intel_FishySmell3 == true && Intel_FishySmell3KeyCheck == false)
        {
            Intel_FishySmell3KeyCheck = true;
            functionList.Add(Fab_FishySmell3);
        }
        if (DataBaseManager.Intel_MurderCase1 == true && Intel_MurderCase1KeyCheck == false)
        {
            Intel_MurderCase1KeyCheck = true;
            functionList.Add(Fab_Witness);
        }
        if (DataBaseManager.Intel_SewerWorker4 == true && Intel_SewerWorker4KeyCheck == false)
        {
            Intel_SewerWorker4KeyCheck = true;
            functionList.Add(Fab_OwnerofUnifrom);
        }
        if (DataBaseManager.Intel_MurderCase4 == true && Intel_MurderCase4KeyCheck == false)
        {
            Intel_MurderCase4KeyCheck = true;
            functionList.Add(Fab_CauseofDeath);
        }
        if (DataBaseManager.Intel_MurderCase5 == true && Intel_MurderCase5KeyCheck == false)
        {
            Intel_MurderCase5KeyCheck = true;
            functionList.Add(Fab_WebbedFeet);
        }
        if (DataBaseManager.Intel_MurderCase6 == true && Intel_MurderCase6KeyCheck == false)
        {
            Intel_MurderCase6KeyCheck = true;
            functionList.Add(Fab_Runway);
        }
        if (DataBaseManager.Intel_MurderCase2 == true && Intel_MurderCase2KeyCheck == false)
        {
            Intel_MurderCase2KeyCheck = true;
            functionList.Add(Fab_FishySmellMissing);
        }
        if (DataBaseManager.Intel_MurderCase3 == true && Intel_MurderCase3KeyCheck == false)
        {
            Intel_MurderCase3KeyCheck = true;
            functionList.Add(Fab_TheClupet);
        }
        if ((DataBaseManager.Intel_MurderCase2 == true || DataBaseManager.Intel_MurderCase3 == true) && BothKeyCheck == false)
        {
            BothKeyCheck = true;
            functionList.Add(Fab_Both);
        }
        if ((DataBaseManager.Intel_Sewer1 == true || DataBaseManager.Intel_Sewer2 == true) && Intel_Sewer2KeyCheck == false)
        {
            Intel_Sewer2KeyCheck = true;
            functionList.Add(Fab_WarnOrRoot);
        }
        if ((DataBaseManager.SwainDeathKeyword == true) && SwainSewerKeyCheck == false)
        {
            SwainSewerKeyCheck = true;
            functionList.Add(Fab_SwainSewer);
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
            DataBaseManager.EndDay = true;
        }
        // 함수 리스트에서 무작위로 함수 선택
    }

    private void Fab_FirstDialog()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_FirstDialog");
    }
    private void Fab_2ndConnectStart()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_2ndConnectStart");
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
    private void Fab_Witness()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Witness");
    }
    private void Fab_OwnerofUnifrom()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_OwnerofUnifrom");
    }
    private void Fab_CauseofDeath()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_CauseofDeath");
    }
    private void Fab_WebbedFeet()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_WebbedFeet");
    }
    private void Fab_Runway()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Runway");
    }
    private void Fab_FishySmellMissing()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_FishySmellMissing");
    }
    private void Fab_TheClupet()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_TheClupet");
    }
    private void Fab_Both()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_Both");
    }
    private void Fab_WarnOrRoot()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_WarnOrRoot");
    }
    private void Fab_SwainSewer()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Fab_SwainSewer");
    }
    bool Intel_Aiden1KeyCheck = false;
    bool Intel_Safe1KeyCheck = false;
    bool Intel_Nightmare1KeyCheck = false;
    bool Intel_Nightmare2KeyCheck = false;
    bool Intel_Nightmare3KeyCheck = false;
    bool Intel_Nightmare4KeyCheck = false;
    bool Intel_Insomnia1KeyCheck = false;
    bool Intel_Insomnia2KeyCheck = false;
    bool Intel_PlanetaryParade1KeyCheck = false;
    bool Intel_University1KeyCheck = false;
    bool Intel_Meiv2KeyCheck = false;
    bool Intel_MissingPeople1KeyCheck = false;
    bool Intel_SewerGhostStory1KeyCheck = false;
    bool Intel_SewerGhostStory2KeyCheck = false;
    bool Intel_CreepyEyes1KeyCheck = false;
    bool Intel_SewerWorker1KeyCheck = false;
    bool Intel_FishySmell1KeyCheck = false;
    bool Intel_FishySmell2KeyCheck = false;
    bool Intel_FishySmell3KeyCheck = false;
    bool Intel_MurderCase1KeyCheck = false;
    bool Intel_SewerWorker4KeyCheck = false;
    bool Intel_MurderCase4KeyCheck = false;
    bool Intel_MurderCase5KeyCheck = false;
    bool Intel_MurderCase6KeyCheck = false;
    bool Intel_MurderCase2KeyCheck = false;
    bool Intel_MurderCase3KeyCheck = false;
    bool BothKeyCheck = false;
    bool Intel_Sewer2KeyCheck = false;
    bool SwainSewerKeyCheck = false;
}
