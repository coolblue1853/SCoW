using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Animations;
public class MapManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Animator ChaingUiAnim;
    public GameObject Upper;
    public GameObject Downer;
    float ChInRommSize = 2.383215f; // -이면 왼쪽 +면 오른쪽


    public GameObject ChaingUi;
    public GameObject Ring1;
    public GameObject Ring2;
    public GameObject Ring3;
    public GameObject Ring4;
    
    public GameObject player;
    public GameObject camera;
    public GameObject MapChainingUI;
    public GameObject MapUI;

    //대학교
    public GameObject Student;

    Vector3 player_SewerOfficeMove = new Vector3(-1202.4f, -81f);
    Vector3 Cam_SewerOfficeMove = new Vector3(-1215.2f, -81.1f, -15);

    Vector3 player_BarMeaveMove = new Vector3(-635.9305f, 1.532792f);
    Vector3 Cam_BarMeaveMove = new Vector3(-634.0516f, 1.690001f, -15);


    Vector3 player_NoonEventMenhole = new Vector3(-976.4003f, 3.074435f);
    Vector3 Cam_NoonEventMenhole = new Vector3(-993.87f, 1.5f, -15);

    Vector3 player_NoonEvent = new Vector3(-818.0412f, 2.183268f);
    Vector3 Cam_NoonEvent = new Vector3(-814.3779f, 1.5f, -15);

    Vector3 player_DetectiveOffice = new Vector3(-774.2f, 2.6f);
    Vector3 Cam_DetectiveOffice = new Vector3(-797.2f, 1.690001f, -15);

    Vector3 player_NoonEventFirst = new Vector3(-981.2974f, 2.86f);

    Vector3 player_ClientsHouse = new Vector3(-990.2f, 2.86f);
    Vector3 Cam_ClientsHouse = new Vector3(-993.87f, 1.5f, -15);

    Vector3 player_DailyNews = new Vector3(-117.6f, -81f);
    Vector3 Cam_DailyNews = new Vector3(-135.7f, -81.21f, -15);

    Vector3 player_Policeoffice = new Vector3(-736.7f, -81f);
    Vector3 Cam_Policeoffice = new Vector3(-721.27f, -81.51f, -15);



    Vector3 player_Univercity = new Vector3(-277f, -81.4f);
    Vector3 Cam_Univercity = new Vector3(-287.6f, -81.61f, -15);

    Vector3 player_RiverSide = new Vector3(-298.34f, 1.77f);
    Vector3 Cam_RiverSide = new Vector3(-313.2f, 1.5f, -16);

    Vector3 player_Hospital = new Vector3(-496.1f, -82.5f);
    Vector3 Cam_Hospital = new Vector3(-491.27f, -81.51f, -15);


    Vector3 player_Slum = new Vector3(-849f, -81f);
    Vector3 Cam_Slum = new Vector3(-860.77f, -81.51f, -15);


    Vector3 player_Bar = new Vector3(-666.8f, 2.6f);
    Vector3 Cam_Bar = new Vector3(-650.4f, 1.690001f, -15);

    Vector3 player_Gunshop = new Vector3(-1036.56f, -82.12f);
    Vector3 Cam_Gunshop = new Vector3(-1056.2f, -82f, -15);



    public GameObject DetectiveOfficeButton;
    public GameObject ClientsHouseButton;
    public GameObject DailyNewsButton;
    public GameObject PoliceofficeButton;
    public GameObject UnivercityButton;
    public GameObject RiverSideButton;
    public GameObject HospitalButton;
    public GameObject SlumButton;
    public GameObject BarButton;
    public GameObject GunshopButton;
    public GameObject SewerOfficeButton;
    private void buttonChecker() 
    {
        if ((DataBaseManager.TimeCount % 4) == 1)
        {
            DetectiveOfficeButton.SetActive(false);
            BarButton.SetActive(false);
            if (DataBaseManager.nowPlace == "Client'shouse")
            {
                ClientsHouseButton.SetActive(false);
            }
            else
            {
                if (DataBaseManager.Intel_QuestHouse1 == true)
                {
                    ClientsHouseButton.SetActive(true);
                }
                else
                {
                    ClientsHouseButton.SetActive(false);
                }
            }
            if (DataBaseManager.nowPlace == "DailyNews")
            {
                DailyNewsButton.SetActive(false);
            }
            else
            {
                DailyNewsButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "University")
            {
                UnivercityButton.SetActive(false);
            }
            else
            {
                UnivercityButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Riverside")
            {
                RiverSideButton.SetActive(false);
            }
            else
            {
                RiverSideButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Hospital")
            {
                HospitalButton.SetActive(false);
            }
            else
            {
                HospitalButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Policeoffice")
            {
                PoliceofficeButton.SetActive(false);
            }
            else
            {
                PoliceofficeButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Slum")
            {
                SlumButton.SetActive(false);
            }
            else
            {
                SlumButton.SetActive(true);
            }
            if (DataBaseManager.Tightwad == true)
            {
                GunshopButton.SetActive(false);
            }
            else if (DataBaseManager.nowPlace == "Gunshop")
            {
                GunshopButton.SetActive(false);
            }
            else
            {
                GunshopButton.SetActive(true);
            }
            if (DataBaseManager.Intel_SewageMaintenanceOffice1 == false)
            {
                SewerOfficeButton.SetActive(false);
            }
            else if (DataBaseManager.nowPlace == "SewerOffice")
            {
                SewerOfficeButton.SetActive(false);
            }
            else
            {
                SewerOfficeButton.SetActive(true);
            }

            
        }
        if ((DataBaseManager.TimeCount % 4) == 2)
        {
            DetectiveOfficeButton.SetActive(false);
            BarButton.SetActive(false);
            if (DataBaseManager.nowPlace == "Client'shouse")
            {
                ClientsHouseButton.SetActive(false);
            }
            else
            {
                if(DataBaseManager.Intel_QuestHouse1 == true)
                {
                    ClientsHouseButton.SetActive(true);
                }
                else
                {
                    ClientsHouseButton.SetActive(false);
                }
            }
            if (DataBaseManager.nowPlace == "DailyNews")
            {
                DailyNewsButton.SetActive(false);
            }
            else
            {
                DailyNewsButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "University")
            {
                UnivercityButton.SetActive(false);
            }
            else
            {
                UnivercityButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Riverside")
            {
                RiverSideButton.SetActive(false);
            }
            else
            {
                RiverSideButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Hospital")
            {
                HospitalButton.SetActive(false);
            }
            else
            {
                HospitalButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Policeoffice")
            {
                PoliceofficeButton.SetActive(false);
            }
            else
            {
                PoliceofficeButton.SetActive(true);
            }
            if (DataBaseManager.nowPlace == "Slum")
            {
                SlumButton.SetActive(false);
            }
            else
            {
                SlumButton.SetActive(true);
            }
    

            if (DataBaseManager.Tightwad == true)
            {
                GunshopButton.SetActive(false);
            }
            else if (DataBaseManager.nowPlace == "Gunshop")
            {
                GunshopButton.SetActive(false);
            }
            else
            {
                GunshopButton.SetActive(true);
            }
            if (DataBaseManager.Intel_SewageMaintenanceOffice1 == false)
            {
                SewerOfficeButton.SetActive(false);
            }
            else if (DataBaseManager.nowPlace == "SewerOffice")
            {
                SewerOfficeButton.SetActive(false);
            }
            else
            {
                SewerOfficeButton.SetActive(true);
            }
        }
        if ((DataBaseManager.TimeCount % 4) == 3)
        {
            DetectiveOfficeButton.SetActive(true);


            ClientsHouseButton.SetActive(false);
            DailyNewsButton.SetActive(false);
            UnivercityButton.SetActive(false);
            RiverSideButton.SetActive(false);
            HospitalButton.SetActive(false);
            PoliceofficeButton.SetActive(false);
            SlumButton.SetActive(false);
            GunshopButton.SetActive(false);

            if(DataBaseManager.isDebuff_NightPhobia == false)
            {
                BarButton.SetActive(true);
                if (DataBaseManager.Intel_SewageMaintenanceOffice1 == false)
                {
                    SewerOfficeButton.SetActive(false);
                }
                else if (DataBaseManager.nowPlace == "SewerOffice")
                {
                    SewerOfficeButton.SetActive(false);
                }
                else
                {
                    SewerOfficeButton.SetActive(true);
                }
            }
            else
            {
                SewerOfficeButton.SetActive(false);
                BarButton.SetActive(false);
            }




        }
        if ((DataBaseManager.TimeCount % 4) == 0)
        {
            if(DataBaseManager.nowPlace == "Bar")
            {
                DetectiveOfficeButton.SetActive(true);
                BarButton.SetActive(false);
                ClientsHouseButton.SetActive(false);
                DailyNewsButton.SetActive(false);
                UnivercityButton.SetActive(false);
                RiverSideButton.SetActive(false);
                HospitalButton.SetActive(false);
                PoliceofficeButton.SetActive(false);
                SlumButton.SetActive(false);
                GunshopButton.SetActive(false);
                SewerOfficeButton.SetActive(false);

            }
            if (DataBaseManager.nowPlace == "SewerOffice")
            {
                DetectiveOfficeButton.SetActive(true);
                BarButton.SetActive(false);
                ClientsHouseButton.SetActive(false);
                DailyNewsButton.SetActive(false);
                UnivercityButton.SetActive(false);
                RiverSideButton.SetActive(false);
                HospitalButton.SetActive(false);
                PoliceofficeButton.SetActive(false);
                SlumButton.SetActive(false);
                GunshopButton.SetActive(false);
                SewerOfficeButton.SetActive(false);
            }

        }


    }

    public void OpenUpperMap()
    {
        SoundManager.Instance.ClickSound_Play();
        Upper.SetActive(true);
        Downer.SetActive(false);
    }
    public void OpenDownMap()
    {
        SoundManager.Instance.ClickSound_Play();
        Upper.SetActive(false);
        Downer.SetActive(true);
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


    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
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

    private void Start()
    {

        //MapOn();

    }

    public void OpenMap()
    {
        if(DataBaseManager.nowPlace != "InSewer")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.StoryDirecting == false && DataBaseManager.potalWait == false && DataBaseManager.secondisDirecting == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false && DataBaseManager.nowPlace != "BattleRoad" && DataBaseManager.isOpenMap == false)
            {
                SoundManager.Instance.PaperClip_Play();
                DataBaseManager.workSound = false;
                DataBaseManager.isOpenUi = true;
                MapChainingUI.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.StoryDirecting == false && DataBaseManager.potalWait == false && DataBaseManager.secondisDirecting == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false && DataBaseManager.nowPlace != "BattleRoad" && DataBaseManager.isOpenMap == false)
            {
                if (DataBaseManager.sewerMap == true)
                {
                    SoundManager.Instance.PaperClip_Play();
                    DataBaseManager.workSound = false;
                    DataBaseManager.isOpenUi = true;
                    MapChainingUI.SetActive(true);
                }

            }

        }


    }
    public void CloseMap()
    {
        SoundManager.Instance.ClickSound_Play();
        DataBaseManager.isOpenUi = false;
        MapChainingUI.SetActive(false);
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
                if(MapChainingUI.activeSelf == false)
                {
                    DataBaseManager.workSound = false;
                    OpenMap();
                }
                else if (MapChainingUI.activeSelf == true)
                {
                    CloseMap();
                }

            }
            if (Input.GetKeyDown(KeyCode.Escape) && MapChainingUI.activeSelf == true)
            {
                DataBaseManager.isOpenUi = false;
                MapChainingUI.SetActive(false);
            }
        }
    }


    void NoonEvent()
    {
        player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
        player.transform.localPosition = player_NoonEvent;
        camera.transform.localPosition = Cam_NoonEvent;
        MapChainingUI.SetActive(false);
        MapUI.SetActive(false);
        DataBaseManager.StoryDirecting = true;
        DataBaseManager.snd_Detective_NoonEvent = true;
    }

    void BattleDialog()
    {
        DirectingManager.Instance.BeforeBattle();
    }
    bool isNoonEvent = false;

    void TimeMinus()
    {
        DataBaseManager.TimeCount -= 1;
    }

    public void MapOn()
    {






        DataBaseManager.BGMChangeChecker = true;

        if (DataBaseManager.TimeCount == 13 && DataBaseManager.MorningMove_4th == false)
        {
            DataBaseManager.IsMorningMapMove = true;
            DataBaseManager.secondisDirecting = true;
            DataBaseManager.MorningMove_4th = true;
            FadingBackGround.Instance.FadeInOut();
            CloseMap();
            Invoke("OpenSpinUi", 1f);
            Invoke("WaitFade", 4f);
            Invoke("TimeMinus", 1f);
        }
        else if (DataBaseManager.TimeCount == 9 && DataBaseManager.MorningMove_3rd == false)
        {
            DataBaseManager.IsMorningMapMove = true;
            DataBaseManager.secondisDirecting = true;
            DataBaseManager.MorningMove_3rd = true;
            FadingBackGround.Instance.FadeInOut();
            CloseMap();
            Invoke("OpenSpinUi", 1f);
            Invoke("WaitFade", 4f);
            Invoke("TimeMinus", 1f);
        }
        else if (DataBaseManager.TimeCount == 5 && DataBaseManager.MorningMove_2nd == false)
        {
            DataBaseManager.IsMorningMapMove = true;
            DataBaseManager.NoonMorningMove = true;
            DataBaseManager.secondisDirecting = true;

            DataBaseManager.MorningMove_2nd = true;
          FadingBackGround.Instance.FadeInOut();
            CloseMap();
            Invoke("OpenSpinUi", 1f);
            Invoke("WaitFade", 4f);
            Invoke("TimeMinus", 1f);
        }
        else if (DataBaseManager.TimeCount == 5 && isNoonEvent == false)
        {
            DataBaseManager.IsMorningMapMove = false;
            isNoonEvent = true;
            DataBaseManager.nowPlace = "NoonEvent";
            DataBaseManager.isDirecting = true;
            DialogDatabaseManager.instance.Check = true;
            FadingBackGround.Instance.FadeInOut();
            CloseMap();
            Invoke("NoonEvent", 1f);
        }
        else
        {
            DataBaseManager.IsMorningMapMove = false;
            DataBaseManager.isOpenMap = true;
            if (DataBaseManager.isBar == true)
            {
                DataBaseManager.isSewerOffice = false;
                Debug.Log("Check1");
                DataBaseManager.isDirecting = true;
                DataBaseManager.isBar = false;
                DataBaseManager.nowPlace = "BattleRoad";
                DialogDatabaseManager.instance.Check = true;
                CloseMap();
                //DirectingManager.Instance.GotoBattle();

                FadingBackGround.Instance.FadeInOut();
                Invoke("BattleDialog", 1f);
            }
            else if(DataBaseManager.AfterBattle == true && DataBaseManager.TimeCount % 4 == 3 && DataBaseManager.nowPlace == "DetectiveOffice")
            {
                Debug.Log("Check2");
                FadingBackGround.Instance.FadeInOut();
                Invoke("OpenSpinUi", 1f);
                Invoke("SpinRing", 3f);
            }
            else if (DataBaseManager.isSewerOffice == true &&(DataBaseManager.TimeCount %4 == 3 || DataBaseManager.TimeCount % 4 == 0) && (DataBaseManager.nowPlace == "DetectiveOffice"|| DataBaseManager.nowPlace == "BattleRoad"))
            {
                Debug.Log("Check3");
                /*
                if(DataBaseManager.TimeCount % 4 == 3)
                {
                    DataBaseManager.TimeCount += 1;
                }
                */
                DataBaseManager.isDirecting = true;
                DataBaseManager.isSewerOffice = false;
                DataBaseManager.nowPlace = "BattleRoad";
                DialogDatabaseManager.instance.Check = true;
                CloseMap();
                //DirectingManager.Instance.GotoBattle();

                FadingBackGround.Instance.FadeInOut();
                Invoke("BattleDialog", 1f);
            }
            else
            {
                if ((DataBaseManager.TimeCount % 4) == 0 && DataBaseManager.nowPlace == "BattleRoad")
                {
                    Debug.Log("Check4");

                    FadingBackGround.Instance.FadeInOut();
                    Invoke("OpenSpinUi", 1f);
                    Invoke("WaitFade", 4f);
                }
                else
                {
                    Debug.Log("Check5");
                    FadingBackGround.Instance.FadeInOut();
                    Invoke("OpenSpinUi", 1f);
                    Invoke("SpinRing", 3f);

                }

            }
        }
      
    }


    public void OpenSpinUi()
    {
        ChaingUi.SetActive(true);
    }
    void SpinRing()
    {
        if ((DataBaseManager.TimeCount % 4) == 1)
        {
            Ring1.transform.DORotate(new Vector3(0f, 0, 360f + 360f), 1f).SetAutoKill();
            Ring2.transform.DORotate(new Vector3(0f, 0, 360f + 360f), 2.5f).SetAutoKill();
            Ring3.transform.DORotate(new Vector3(0f, 0, 360f + 360f), 1.5f).SetAutoKill();
            Ring4.transform.DORotate(new Vector3(0f, 0, 360f + 360f), 2f).SetAutoKill();
        }
        if ((DataBaseManager.TimeCount % 4) == 2)
        {
            Ring1.transform.DORotate(new Vector3(0f, 0, 90f + 360f), 1f).SetAutoKill();
            Ring2.transform.DORotate(new Vector3(0f, 0, 90f + 360f), 2.5f).SetAutoKill();
            Ring3.transform.DORotate(new Vector3(0f, 0, 90f + 360f), 1.5f).SetAutoKill();
            Ring4.transform.DORotate(new Vector3(0f, 0, 90f + 360f), 2f).SetAutoKill();
        }
        if ((DataBaseManager.TimeCount % 4) == 3)
        {
            Ring1.transform.DORotate(new Vector3(0f, 0, 180f + 360f), 1f).SetAutoKill();
            Ring2.transform.DORotate(new Vector3(0f, 0, 180f + 360f), 2.5f).SetAutoKill();
            Ring3.transform.DORotate(new Vector3(0f, 0, 180f + 360f), 1.5f).SetAutoKill();
            Ring4.transform.DORotate(new Vector3(0f, 0, 180f + 360f), 2f).SetAutoKill();
        }
        if ((DataBaseManager.TimeCount % 4) == 0)
        {
            Ring1.transform.DORotate(new Vector3(0f, 0, 270f + 360f), 1f).SetAutoKill();
            Ring2.transform.DORotate(new Vector3(0f, 0, 270f + 360f), 2.5f).SetAutoKill();
            Ring3.transform.DORotate(new Vector3(0f, 0, 270f + 360f), 1.5f).SetAutoKill();
            Ring4.transform.DORotate(new Vector3(0f, 0, 270f + 360f), 2f).SetAutoKill();
        }
        Invoke("ChainingDate", 1.5f);
    }

    public void ChainingDate()
    {
        ChaingUiAnim.SetBool("Change", true);
        Invoke("ChainingDate2", 1f);
        Invoke("EndEvent", 1f);
    }
    public void ChainingDate2()
    {
        if (DataBaseManager.TimeCount == 1)
        {
            text.text = "11/7/1921-Noon";
        }
        else if (DataBaseManager.TimeCount == 2)
        {
            text.text = "11/7/1921-Afternoon";
        }
        else if (DataBaseManager.TimeCount == 3)
        {
            text.text = "11/7/1921-Night";
        }
        else if (DataBaseManager.TimeCount == 4)
        {
            text.text = "12/7/1921-Morning";
        }
        else if (DataBaseManager.TimeCount == 5)
        {
            text.text = "12/7/1921-Noon";
        }
        else if (DataBaseManager.TimeCount == 6)
        {
            selectionUIManager.Instance.Ex_AddFunc9();
            text.text = "12/7/1921-Afternoon";
        }
        else if (DataBaseManager.TimeCount == 7)
        {
            if(DataBaseManager.nowPlace == "Bar")
            {
                DataBaseManager.BarArrive = true;
            }
            text.text = "12/7/1921-Night";
        }
        else if (DataBaseManager.TimeCount == 8)
        {
            text.text = "13/7/1921-Morning";
        }
        else if (DataBaseManager.TimeCount == 9)
        {
            text.text = "13/7/1921-Noon";
        }
        else if (DataBaseManager.TimeCount == 10)
        {
            text.text = "13/7/1921-Afternoon";
        }
        else if (DataBaseManager.TimeCount == 11)
        {
            text.text = "13/7/1921-Night";
        }
        else if (DataBaseManager.TimeCount == 12)
        {
            text.text = "14/7/1921-Morning";
        }
        else if (DataBaseManager.TimeCount == 13)
        {
            text.text = "14/7/1921-Noon";
        }
        else if (DataBaseManager.TimeCount == 14)
        {
            text.text = "14/7/1921-Afternoon";
        }
        else if (DataBaseManager.TimeCount == 15)
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
        player.transform.localPosition = player_BarMeaveMove;
        camera.transform.localPosition = Cam_BarMeaveMove;
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
        

        if(DataBaseManager.TimeCount >=5 && DataBaseManager.TimeCount % 4 == 1 && DataBaseManager.nowPlace == "DetectiveOffice")
        {
            DataBaseManager.secondisDirecting = true;
        }




        DataBaseManager.isOpenMap = false;
        DataBaseManager.isOpenUi = false;
        CameraManager.Instance.isCheckEnd = true;


        if (DataBaseManager.nowPlace == "DetectiveOffice")
        {
            DataBaseManager.TimeCount += 1;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
                player.transform.localPosition = player_DetectiveOffice;
                camera.transform.localPosition = Cam_DetectiveOffice;
                MapChainingUI.SetActive(false);
                MapUI.SetActive(false);
                DialogDatabaseManager.instance.Check = true;

            if (DataBaseManager.TimeCount == 4)
            {
                DataBaseManager.secondisDirecting = true;
            }

        }
        else if (DataBaseManager.nowPlace == "Client'shouse")
        {
         

            DataBaseManager.TimeCount += 1;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_ClientsHouse;
            camera.transform.localPosition = Cam_ClientsHouse;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            if(DataBaseManager.firstClientsHouseEnd == false && DataBaseManager.TimeCount <6)
            {
                DataBaseManager.StoryDirecting = true;
                Invoke("FirstClientsHouseArrive", 2);
            }

            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "DailyNews")
        {
            DataBaseManager.TimeCount += 1;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_DailyNews;
            camera.transform.localPosition = Cam_DailyNews;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "University")
        {
            DataBaseManager.TimeCount += 1;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_Univercity;
            camera.transform.localPosition = Cam_Univercity;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;

            if((DataBaseManager.TimeCount % 4) == 3)
            {
                Student.SetActive(false);
            }
            else
            {
                Student.SetActive(true);
            }
        }
        else if (DataBaseManager.nowPlace == "Riverside")
        {
            DataBaseManager.TimeCount += 1;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_RiverSide;
            camera.transform.localPosition = Cam_RiverSide;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "Hospital")
        {
            DataBaseManager.TimeCount += 1;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_Hospital;
            camera.transform.localPosition = Cam_Hospital;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "Policeoffice")
        {
            DataBaseManager.TimeCount += 1;
            player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_Policeoffice;
            camera.transform.localPosition = Cam_Policeoffice;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "Slum")
        {
            DataBaseManager.TimeCount += 1;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_Slum;
            camera.transform.localPosition = Cam_Slum;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "Bar")
        {
            DataBaseManager.battleEnemyCount += 1;
            DataBaseManager.TimeCount += 1;
            DataBaseManager.isBar = true;
            player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_Bar;
            camera.transform.localPosition = Cam_Bar;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "Gunshop")
        {
            DataBaseManager.TimeCount += 1;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_Gunshop;
            camera.transform.localPosition = Cam_Gunshop;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "BattleRoad")
        {
            if(DataBaseManager.TimeCount %4 == 3)
            {
                DataBaseManager.TimeCount += 1;
            }
            DataBaseManager.secondisDirecting = true;
            DataBaseManager.nowPlace = "DetectiveOffice";
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_DetectiveOffice;
            camera.transform.localPosition = Cam_DetectiveOffice;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;

        }
        else if (DataBaseManager.nowPlace == "NoonEvent")
        {
            DataBaseManager.TimeCount += 1;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_NoonEventFirst;
            camera.transform.localPosition = Cam_ClientsHouse;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;

        }
        else if (DataBaseManager.nowPlace == "SewerOffice")
        {
            DataBaseManager.battleEnemyCount += 1;
            DataBaseManager.TimeCount += 1;
            DataBaseManager.isSewerOffice = true;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_SewerOfficeMove;
            camera.transform.localPosition = Cam_SewerOfficeMove;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;

        }

        if (DataBaseManager.PanicAttack == true)
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
        player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = player_NoonEventMenhole;
        camera.transform.localPosition = Cam_NoonEventMenhole;
        Invoke("SewerDialog", 1f);
    }
    public void SewerDialog()
    {
        if(DataBaseManager.TimeCount < 7)
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
