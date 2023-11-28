using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Animations;
public class MapManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Animator chaingUiAnim;
    public GameObject upper;
    public GameObject downer;
    float chInRommSize = 2.383215f; // -이면 왼쪽 +면 오른쪽
    public GameObject chaingUi;
    public GameObject ring1;
    public GameObject ring2;
    public GameObject ring3;
    public GameObject ring4;
    public GameObject player;
    public GameObject camera;
    public GameObject mapChainingUI;
    public GameObject mapUI;

    //대학교
    public GameObject student;

    Vector3 playerSewerOfficeMove = new Vector3(-1202.4f, -81f);
    Vector3 camSewerOfficeMove = new Vector3(-1215.2f, -81.1f, -15);

    Vector3 playerBarMeaveMove = new Vector3(-635.9305f, 1.532792f);
    Vector3 camBarMeaveMove = new Vector3(-634.0516f, 1.690001f, -15);

    Vector3 playerNoonEventMenhole = new Vector3(-976.4003f, 3.074435f);
    Vector3 camNoonEventMenhole = new Vector3(-993.87f, 1.5f, -15);

    Vector3 playerNoonEvent = new Vector3(-818.0412f, 2.183268f);
    Vector3 camNoonEvent = new Vector3(-814.3779f, 1.5f, -15);

    Vector3 playerDetectiveOffice = new Vector3(-774.2f, 2.6f);
    Vector3 camDetectiveOffice = new Vector3(-797.2f, 1.690001f, -15);

    Vector3 playerNoonEventFirst = new Vector3(-981.2974f, 2.86f);

    Vector3 playerClientsHouse = new Vector3(-990.2f, 2.86f);
    Vector3 camClientsHouse = new Vector3(-993.87f, 1.5f, -15);

    Vector3 playerDailyNews = new Vector3(-117.6f, -81f);
    Vector3 camDailyNews = new Vector3(-135.7f, -81.21f, -15);

    Vector3 playerPoliceoffice = new Vector3(-736.7f, -81f);
    Vector3 camPoliceoffice = new Vector3(-721.27f, -81.51f, -15);

    Vector3 playerUnivercity = new Vector3(-277f, -81.4f);
    Vector3 camUnivercity = new Vector3(-287.6f, -81.61f, -15);

    Vector3 playerRiverSide = new Vector3(-298.34f, 1.77f);
    Vector3 camRiverSide = new Vector3(-313.2f, 1.5f, -16);

    Vector3 playerHospital = new Vector3(-496.1f, -82.5f);
    Vector3 camHospital = new Vector3(-491.27f, -81.51f, -15);

    Vector3 playerSlum = new Vector3(-849f, -81f);
    Vector3 camSlum = new Vector3(-860.77f, -81.51f, -15);

    Vector3 playerBar = new Vector3(-666.8f, 2.6f);
    Vector3 camBar = new Vector3(-650.4f, 1.690001f, -15);

    Vector3 playerGunshop = new Vector3(-1036.56f, -82.12f);
    Vector3 camGunshop = new Vector3(-1056.2f, -82f, -15);
    public GameObject detectiveOfficeButton;
    public GameObject clientsHouseButton;
    public GameObject dailyNewsButton;
    public GameObject policeofficeButton;
    public GameObject univercityButton;
    public GameObject riverSideButton;
    public GameObject hospitalButton;
    public GameObject slumButton;
    public GameObject barButton;
    public GameObject gunshopButton;
    public GameObject sewerOfficeButton;
    
    private void buttonChecker() 
    {
        if ((DataBaseManager.timeCount % 4) == 1)
        {
            detectiveOfficeButton.SetActive(false);
            barButton.SetActive(false);
            if (DataBaseManager.nowPlace == "Client'shouse")
            {
                clientsHouseButton.SetActive(false);
            }
            else
            {
                if (DataBaseManager.intelQuestHouse1 == true)
                {
                    clientsHouseButton.SetActive(true);
                }
                else
                {
                    clientsHouseButton.SetActive(false);
                }
            }
            if (DataBaseManager.nowPlace == "DailyNews")
            {
                dailyNewsButton.SetActive(false);
            }
            else
            {
                dailyNewsButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "University")
            {
                univercityButton.SetActive(false);
            }
            else
            {
                univercityButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Riverside")
            {
                riverSideButton.SetActive(false);
            }
            else
            {
                riverSideButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Hospital")
            {
                hospitalButton.SetActive(false);
            }
            else
            {
                hospitalButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Policeoffice")
            {
                policeofficeButton.SetActive(false);
            }
            else
            {
                policeofficeButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Slum")
            {
                slumButton.SetActive(false);
            }
            else
            {
                slumButton.SetActive(true);
            }
            if (DataBaseManager.tightwad == true)
            {
                gunshopButton.SetActive(false);
            }
            else if (DataBaseManager.nowPlace == "Gunshop")
            {
                gunshopButton.SetActive(false);
            }
            else
            {
                gunshopButton.SetActive(true);
            }
            if (DataBaseManager.intelSewageMaintenanceOffice1 == false)
            {
                sewerOfficeButton.SetActive(false);
            }
            else if (DataBaseManager.nowPlace == "SewerOffice")
            {
                sewerOfficeButton.SetActive(false);
            }
            else
            {
                sewerOfficeButton.SetActive(true);
            }
        }
        if ((DataBaseManager.timeCount % 4) == 2)
        {
            detectiveOfficeButton.SetActive(false);
            barButton.SetActive(false);
            if (DataBaseManager.nowPlace == "Client'shouse")
            {
                clientsHouseButton.SetActive(false);
            }
            else
            {
                if(DataBaseManager.intelQuestHouse1 == true)
                {
                    clientsHouseButton.SetActive(true);
                }
                else
                {
                    clientsHouseButton.SetActive(false);
                }
            }
            if (DataBaseManager.nowPlace == "DailyNews")
            {
                dailyNewsButton.SetActive(false);
            }
            else
            {
                dailyNewsButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "University")
            {
                univercityButton.SetActive(false);
            }
            else
            {
                univercityButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Riverside")
            {
                riverSideButton.SetActive(false);
            }
            else
            {
                riverSideButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Hospital")
            {
                hospitalButton.SetActive(false);
            }
            else
            {
                hospitalButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Policeoffice")
            {
                policeofficeButton.SetActive(false);
            }
            else
            {
                policeofficeButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Slum")
            {
                slumButton.SetActive(false);
            }
            else
            {
                slumButton.SetActive(true);
            }
            if (DataBaseManager.tightwad == true)
            {
                gunshopButton.SetActive(false);
            }
            else if (DataBaseManager.nowPlace == "Gunshop")
            {
                gunshopButton.SetActive(false);
            }
            else
            {
                gunshopButton.SetActive(true);
            }
            if (DataBaseManager.intelSewageMaintenanceOffice1 == false)
            {
                sewerOfficeButton.SetActive(false);
            }
            else if (DataBaseManager.nowPlace == "SewerOffice")
            {
                sewerOfficeButton.SetActive(false);
            }
            else
            {
                sewerOfficeButton.SetActive(true);
            }
        }
        if ((DataBaseManager.timeCount % 4) == 3)
        {
            detectiveOfficeButton.SetActive(true);
            clientsHouseButton.SetActive(false);
            dailyNewsButton.SetActive(false);
            univercityButton.SetActive(false);
            riverSideButton.SetActive(false);
            hospitalButton.SetActive(false);
            policeofficeButton.SetActive(false);
            slumButton.SetActive(false);
            gunshopButton.SetActive(false);
            if(DataBaseManager.isDebuffNightPhobia == false)
            {
                barButton.SetActive(true);
                if (DataBaseManager.intelSewageMaintenanceOffice1 == false)
                {
                    sewerOfficeButton.SetActive(false);
                }
                else if (DataBaseManager.nowPlace == "SewerOffice")
                {
                    sewerOfficeButton.SetActive(false);
                }
                else
                {
                    sewerOfficeButton.SetActive(true);
                }
            }
            else
            {
                sewerOfficeButton.SetActive(false);
                barButton.SetActive(false);
            }
        }
        if ((DataBaseManager.timeCount % 4) == 0)
        {
            if(DataBaseManager.nowPlace == "Bar")
            {
                detectiveOfficeButton.SetActive(true);
                barButton.SetActive(false);
                clientsHouseButton.SetActive(false);
                dailyNewsButton.SetActive(false);
                univercityButton.SetActive(false);
                riverSideButton.SetActive(false);
                hospitalButton.SetActive(false);
                policeofficeButton.SetActive(false);
                slumButton.SetActive(false);
                gunshopButton.SetActive(false);
                sewerOfficeButton.SetActive(false);
            }
            if (DataBaseManager.nowPlace == "SewerOffice")
            {
                detectiveOfficeButton.SetActive(true);
                barButton.SetActive(false);
                clientsHouseButton.SetActive(false);
                dailyNewsButton.SetActive(false);
                univercityButton.SetActive(false);
                riverSideButton.SetActive(false);
                hospitalButton.SetActive(false);
                policeofficeButton.SetActive(false);
                slumButton.SetActive(false);
                gunshopButton.SetActive(false);
                sewerOfficeButton.SetActive(false);
            }
        }
    }
    public void OpenUpperMap()
    {
        SoundManager.Instance.ClickSound_Play();
        upper.SetActive(true);
        downer.SetActive(false);
    }
    public void OpenDownMap()
    {
        SoundManager.Instance.ClickSound_Play();
        upper.SetActive(false);
        downer.SetActive(true);
    }
    private static MapManager instance = null;
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static MapManager Instance
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
    public void OpenMap()
    {
        if(DataBaseManager.nowPlace != "InSewer")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.storyDirecting == false && DataBaseManager.potalWait == false && DataBaseManager.secondisDirecting == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false && DataBaseManager.nowPlace != "BattleRoad" && DataBaseManager.isOpenMap == false)
            {
                SoundManager.Instance.PaperClip_Play();
                DataBaseManager.workSound = false;
                DataBaseManager.isOpenUi = true;
                mapChainingUI.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.storyDirecting == false && DataBaseManager.potalWait == false && DataBaseManager.secondisDirecting == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false && DataBaseManager.nowPlace != "BattleRoad" && DataBaseManager.isOpenMap == false)
            {
                if (DataBaseManager.sewerMap == true)
                {
                    SoundManager.Instance.PaperClip_Play();
                    DataBaseManager.workSound = false;
                    DataBaseManager.isOpenUi = true;
                    mapChainingUI.SetActive(true);
                }

            }
        }
    }
    public void CloseMap()
    {
        SoundManager.Instance.ClickSound_Play();
        DataBaseManager.isOpenUi = false;
        mapChainingUI.SetActive(false);
    }
    public GameObject NomalMap;
    public GameObject SewerMap;
    private void Update()
    {
        if(NomalMap.activeSelf == false && DataBaseManager.nowPlace != "InSewer")
        {
            NomalMap.SetActive(true);
            SewerMap.SetActive(false);
        }
        else if (SewerMap.activeSelf == false && DataBaseManager.nowPlace == "InSewer")
        {
            NomalMap.SetActive(false);
            SewerMap.SetActive(true);
        }
        buttonChecker();
        if (DataBaseManager.isActiveDialog1 == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if(mapChainingUI.activeSelf == false)
                {
                    DataBaseManager.workSound = false;
                    OpenMap();
                }
                else if (mapChainingUI.activeSelf == true)
                {
                    CloseMap();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape) && mapChainingUI.activeSelf == true)
            {
                DataBaseManager.isOpenUi = false;
                mapChainingUI.SetActive(false);
            }
        }
    }
    void NoonEvent()
    {
        player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
        player.transform.localPosition = playerNoonEvent;
        camera.transform.localPosition = camNoonEvent;
        mapChainingUI.SetActive(false);
        mapUI.SetActive(false);
        DataBaseManager.storyDirecting = true;
        DataBaseManager.secondDetectiveNoonEvent = true;
    }
    void BattleDialog()
    {
        DirectingManager.Instance.BeforeBattle();
    }
    bool isNoonEvent = false;
    void TimeMinus()
    {
        DataBaseManager.timeCount -= 1;
    }
    public void MapOn()
    {
        DataBaseManager.bgmChangeChecker = true;

        if (DataBaseManager.timeCount == 13 && DataBaseManager.morningMove4th == false)
        {
            DataBaseManager.isMorningMapMove = true;
            DataBaseManager.secondisDirecting = true;
            DataBaseManager.morningMove4th = true;
            FadingBackGround.Instance.FadeInOut();
            CloseMap();
            Invoke("OpenSpinUi", 1f);
            Invoke("WaitFade", 4f);
            Invoke("TimeMinus", 1f);
        }
        else if (DataBaseManager.timeCount == 9 && DataBaseManager.morningMove_3rd == false)
        {
            DataBaseManager.isMorningMapMove = true;
            DataBaseManager.secondisDirecting = true;
            DataBaseManager.morningMove_3rd = true;
            FadingBackGround.Instance.FadeInOut();
            CloseMap();
            Invoke("OpenSpinUi", 1f);
            Invoke("WaitFade", 4f);
            Invoke("TimeMinus", 1f);
        }
        else if (DataBaseManager.timeCount == 5 && DataBaseManager.morningMove2nd == false)
        {
            DataBaseManager.isMorningMapMove = true;
            DataBaseManager.noonMorningMove = true;
            DataBaseManager.secondisDirecting = true;

            DataBaseManager.morningMove2nd = true;
          FadingBackGround.Instance.FadeInOut();
            CloseMap();
            Invoke("OpenSpinUi", 1f);
            Invoke("WaitFade", 4f);
            Invoke("TimeMinus", 1f);
        }
        else if (DataBaseManager.timeCount == 5 && isNoonEvent == false)
        {
            DataBaseManager.isMorningMapMove = false;
            isNoonEvent = true;
            DataBaseManager.nowPlace = "NoonEvent";
            DataBaseManager.isDirecting = true;
            DialogDatabaseManager.instance.check = true;
            FadingBackGround.Instance.FadeInOut();
            CloseMap();
            Invoke("NoonEvent", 1f);
        }
        else
        {
            DataBaseManager.isMorningMapMove = false;
            DataBaseManager.isOpenMap = true;
            if (DataBaseManager.isBar == true)
            {
                DataBaseManager.isSewerOffice = false;
                Debug.Log("Check1");
                DataBaseManager.isDirecting = true;
                DataBaseManager.isBar = false;
                DataBaseManager.nowPlace = "BattleRoad";
                DialogDatabaseManager.instance.check = true;
                CloseMap();
                //DirectingManager.Instance.GotoBattle();

                FadingBackGround.Instance.FadeInOut();
                Invoke("BattleDialog", 1f);
            }
            else if(DataBaseManager.afterBattle == true && DataBaseManager.timeCount % 4 == 3 && DataBaseManager.nowPlace == "DetectiveOffice")
            {
                FadingBackGround.Instance.FadeInOut();
                Invoke("OpenSpinUi", 1f);
                Invoke("SpinRing", 3f);
            }
            else if (DataBaseManager.isSewerOffice == true &&(DataBaseManager.timeCount %4 == 3 || DataBaseManager.timeCount % 4 == 0) && (DataBaseManager.nowPlace == "DetectiveOffice"|| DataBaseManager.nowPlace == "BattleRoad"))
            {
                Debug.Log("Check3");
                DataBaseManager.isDirecting = true;
                DataBaseManager.isSewerOffice = false;
                DataBaseManager.nowPlace = "BattleRoad";
                DialogDatabaseManager.instance.check = true;
                CloseMap();
                //DirectingManager.Instance.GotoBattle();
                FadingBackGround.Instance.FadeInOut();
                Invoke("BattleDialog", 1f);
            }
            else
            {
                if ((DataBaseManager.timeCount % 4) == 0 && DataBaseManager.nowPlace == "BattleRoad")
                {
                    FadingBackGround.Instance.FadeInOut();
                    Invoke("OpenSpinUi", 1f);
                    Invoke("WaitFade", 4f);
                }
                else
                {
                    FadingBackGround.Instance.FadeInOut();
                    Invoke("OpenSpinUi", 1f);
                    Invoke("SpinRing", 3f);

                }
            }
        }
    }
    public void OpenSpinUi()
    {
        chaingUi.SetActive(true);
    }
    void SpinRing()
    {
        if ((DataBaseManager.timeCount % 4) == 1)
        {
            ring1.transform.DORotate(new Vector3(0f, 0, 360f + 360f), 1f).SetAutoKill();
            ring2.transform.DORotate(new Vector3(0f, 0, 360f + 360f), 2.5f).SetAutoKill();
            ring3.transform.DORotate(new Vector3(0f, 0, 360f + 360f), 1.5f).SetAutoKill();
            ring4.transform.DORotate(new Vector3(0f, 0, 360f + 360f), 2f).SetAutoKill();
        }
        if ((DataBaseManager.timeCount % 4) == 2)
        {
            ring1.transform.DORotate(new Vector3(0f, 0, 90f + 360f), 1f).SetAutoKill();
            ring2.transform.DORotate(new Vector3(0f, 0, 90f + 360f), 2.5f).SetAutoKill();
            ring3.transform.DORotate(new Vector3(0f, 0, 90f + 360f), 1.5f).SetAutoKill();
            ring4.transform.DORotate(new Vector3(0f, 0, 90f + 360f), 2f).SetAutoKill();
        }
        if ((DataBaseManager.timeCount % 4) == 3)
        {
            ring1.transform.DORotate(new Vector3(0f, 0, 180f + 360f), 1f).SetAutoKill();
            ring2.transform.DORotate(new Vector3(0f, 0, 180f + 360f), 2.5f).SetAutoKill();
            ring3.transform.DORotate(new Vector3(0f, 0, 180f + 360f), 1.5f).SetAutoKill();
            ring4.transform.DORotate(new Vector3(0f, 0, 180f + 360f), 2f).SetAutoKill();
        }
        if ((DataBaseManager.timeCount % 4) == 0)
        {
            ring1.transform.DORotate(new Vector3(0f, 0, 270f + 360f), 1f).SetAutoKill();
            ring2.transform.DORotate(new Vector3(0f, 0, 270f + 360f), 2.5f).SetAutoKill();
            ring3.transform.DORotate(new Vector3(0f, 0, 270f + 360f), 1.5f).SetAutoKill();
            ring4.transform.DORotate(new Vector3(0f, 0, 270f + 360f), 2f).SetAutoKill();
        }
        Invoke("ChainingDate", 1.5f);
    }
    public void ChainingDate()
    {
        chaingUiAnim.SetBool("Change", true);
        Invoke("ChainingDate2", 1f);
        Invoke("EndEvent", 1f);
    }
    public void ChainingDate2()
    {
        if (DataBaseManager.timeCount == 1)
        {
            text.text = "11/7/1921-Noon";
        }
        else if (DataBaseManager.timeCount == 2)
        {
            text.text = "11/7/1921-Afternoon";
        }
        else if (DataBaseManager.timeCount == 3)
        {
            text.text = "11/7/1921-Night";
        }
        else if (DataBaseManager.timeCount == 4)
        {
            text.text = "12/7/1921-Morning";
        }
        else if (DataBaseManager.timeCount == 5)
        {
            text.text = "12/7/1921-Noon";
        }
        else if (DataBaseManager.timeCount == 6)
        {
            selectionUIManager.Instance.Ex_AddFunc9();
            text.text = "12/7/1921-Afternoon";
        }
        else if (DataBaseManager.timeCount == 7)
        {
            if(DataBaseManager.nowPlace == "Bar")
            {
                DataBaseManager.barArrive = true;
            }
            text.text = "12/7/1921-Night";
        }
        else if (DataBaseManager.timeCount == 8)
        {
            text.text = "13/7/1921-Morning";
        }
        else if (DataBaseManager.timeCount == 9)
        {
            text.text = "13/7/1921-Noon";
        }
        else if (DataBaseManager.timeCount == 10)
        {
            text.text = "13/7/1921-Afternoon";
        }
        else if (DataBaseManager.timeCount == 11)
        {
            text.text = "13/7/1921-Night";
        }
        else if (DataBaseManager.timeCount == 12)
        {
            text.text = "14/7/1921-Morning";
        }
        else if (DataBaseManager.timeCount == 13)
        {
            text.text = "14/7/1921-Noon";
        }
        else if (DataBaseManager.timeCount == 14)
        {
            text.text = "14/7/1921-Afternoon";
        }
        else if (DataBaseManager.timeCount == 15)
        {
            text.text = "14/7/1921-Night";
        }
    }
    Vector2 player_SewerInside = new Vector3(-1363.46f, -78.46f);
    Vector3 Cam_SewerInside = new Vector3(-1374.97f, -81.1f, -15);
    public void WindowFabianMove()
    {
        player.transform.localPosition = player_SewerInside;
        camera.transform.localPosition = Cam_SewerInside;
    }
    public void BarFabianMove()
    {
        player.transform.localPosition = playerBarMeaveMove;
        camera.transform.localPosition = camBarMeaveMove;
    }
    public void EndEvent()
    {
        Invoke("WaitFade", 2f);
    }
    void WaitFade()
    {
        FadingBackGround.Instance.FadeInOut();
        Invoke("MoveChar", 1f);
    }

    public void MoveChar()
    {
        if(DataBaseManager.timeCount >=5 && DataBaseManager.timeCount % 4 == 1 && DataBaseManager.nowPlace == "DetectiveOffice")
        {
            DataBaseManager.secondisDirecting = true;
        }
        DataBaseManager.isOpenMap = false;
        DataBaseManager.isOpenUi = false;
        CameraManager.Instance.isCheckEnd = true;
        if (DataBaseManager.nowPlace == "DetectiveOffice")
        {
            DataBaseManager.timeCount += 1;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
                player.transform.localPosition = playerDetectiveOffice;
                camera.transform.localPosition = camDetectiveOffice;
                mapChainingUI.SetActive(false);
                mapUI.SetActive(false);
                DialogDatabaseManager.instance.check = true;

            if (DataBaseManager.timeCount == 4)
            {
                DataBaseManager.secondisDirecting = true;
            }
        }
        else if (DataBaseManager.nowPlace == "Client'shouse")
        {
            DataBaseManager.timeCount += 1;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            player.transform.localPosition = playerClientsHouse;
            camera.transform.localPosition = camClientsHouse;
            mapChainingUI.SetActive(false);
            mapUI.SetActive(false);
            if(DataBaseManager.firstClientsHouseEnd == false && DataBaseManager.timeCount <6)
            {
                DataBaseManager.storyDirecting = true;
                Invoke("FirstClientsHouseArrive", 2);
            }
            DialogDatabaseManager.instance.check = true;
        }
        else if (DataBaseManager.nowPlace == "DailyNews")
        {
            DataBaseManager.timeCount += 1;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            player.transform.localPosition = playerDailyNews;
            camera.transform.localPosition = camDailyNews;
            mapChainingUI.SetActive(false);
            mapUI.SetActive(false);
            DialogDatabaseManager.instance.check = true;
        }
        else if (DataBaseManager.nowPlace == "University")
        {
            DataBaseManager.timeCount += 1;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            player.transform.localPosition = playerUnivercity;
            camera.transform.localPosition = camUnivercity;
            mapChainingUI.SetActive(false);
            mapUI.SetActive(false);
            DialogDatabaseManager.instance.check = true;

            if((DataBaseManager.timeCount % 4) == 3)
            {
                student.SetActive(false);
            }
            else
            {
                student.SetActive(true);
            }
        }
        else if (DataBaseManager.nowPlace == "Riverside")
        {
            DataBaseManager.timeCount += 1;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            player.transform.localPosition = playerRiverSide;
            camera.transform.localPosition = camRiverSide;
            mapChainingUI.SetActive(false);
            mapUI.SetActive(false);
            DialogDatabaseManager.instance.check = true;
        }
        else if (DataBaseManager.nowPlace == "Hospital")
        {
            DataBaseManager.timeCount += 1;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            player.transform.localPosition = playerHospital;
            camera.transform.localPosition = camHospital;
            mapChainingUI.SetActive(false);
            mapUI.SetActive(false);
            DialogDatabaseManager.instance.check = true;
        }
        else if (DataBaseManager.nowPlace == "Policeoffice")
        {
            DataBaseManager.timeCount += 1;
            player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
            player.transform.localPosition = playerPoliceoffice;
            camera.transform.localPosition = camPoliceoffice;
            mapChainingUI.SetActive(false);
            mapUI.SetActive(false);
            DialogDatabaseManager.instance.check = true;
        }
        else if (DataBaseManager.nowPlace == "Slum")
        {
            DataBaseManager.timeCount += 1;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            player.transform.localPosition = playerSlum;
            camera.transform.localPosition = camSlum;
            mapChainingUI.SetActive(false);
            mapUI.SetActive(false);
            DialogDatabaseManager.instance.check = true;
        }
        else if (DataBaseManager.nowPlace == "Bar")
        {
            DataBaseManager.battleEnemyCount += 1;
            DataBaseManager.timeCount += 1;
            DataBaseManager.isBar = true;
            player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
            player.transform.localPosition = playerBar;
            camera.transform.localPosition = camBar;
            mapChainingUI.SetActive(false);
            mapUI.SetActive(false);
            DialogDatabaseManager.instance.check = true;
        }
        else if (DataBaseManager.nowPlace == "Gunshop")
        {
            DataBaseManager.timeCount += 1;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            player.transform.localPosition = playerGunshop;
            camera.transform.localPosition = camGunshop;
            mapChainingUI.SetActive(false);
            mapUI.SetActive(false);
            DialogDatabaseManager.instance.check = true;
        }
        else if (DataBaseManager.nowPlace == "BattleRoad")
        {
            if(DataBaseManager.timeCount %4 == 3)
            {
                DataBaseManager.timeCount += 1;
            }
            DataBaseManager.secondisDirecting = true;
            DataBaseManager.nowPlace = "DetectiveOffice";
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            player.transform.localPosition = playerDetectiveOffice;
            camera.transform.localPosition = camDetectiveOffice;
            mapChainingUI.SetActive(false);
            mapUI.SetActive(false);
            DialogDatabaseManager.instance.check = true;

        }
        else if (DataBaseManager.nowPlace == "NoonEvent")
        {
            DataBaseManager.timeCount += 1;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            player.transform.localPosition = playerNoonEventFirst;
            camera.transform.localPosition = camClientsHouse;
            mapChainingUI.SetActive(false);
            mapUI.SetActive(false);
            DialogDatabaseManager.instance.check = true;
        }
        else if (DataBaseManager.nowPlace == "SewerOffice")
        {
            DataBaseManager.battleEnemyCount += 1;
            DataBaseManager.timeCount += 1;
            DataBaseManager.isSewerOffice = true;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            player.transform.localPosition = playerSewerOfficeMove;
            camera.transform.localPosition = camSewerOfficeMove;
            mapChainingUI.SetActive(false);
            mapUI.SetActive(false);
            DialogDatabaseManager.instance.check = true;
        }

        if (DataBaseManager.panicAttack == true)
        {
            Debug.Log("Panic!");
            int panic = Random.Range(0, 2);
            if(panic == 1)
            {
                Invoke("PanicAttack", 2f);
            }
            else
            {

            }
        }

        if(DataBaseManager.nowPlace != "DetectiveOffice")
        {
            DataBaseManager.secondisDirecting = false;
        }
    }
    public void SuccAnalysis()
    {
        FadingBackGround.Instance.FadeInOut();
        Invoke("MoveToSewer", 1.5f);
    }
    public void MoveToSewer()
    {
        player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = playerNoonEventMenhole;
        camera.transform.localPosition = camNoonEventMenhole;
        Invoke("SewerDialog", 1f);
    }
    public void SewerDialog()
    {
        if(DataBaseManager.timeCount < 7)
        {
            InteractionController.Instance.Start_2nd_NoonEvent("NoonEvent_DeadBody_analysisSuccAfter");
        }
        else
        {
            InteractionController.Instance.Start_2nd_NoonEvent("Site_analysisSuccAfter");
        }
    }
    void PanicAttack()
    {
        BillowUIManager.Instance.HP_down(5);
    }
    void FirstClientsHouseArrive()
    {
        DataBaseManager.firstClientsHouse = true;
    }
}
