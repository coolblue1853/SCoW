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

    Vector3 player_DetectiveOffice = new Vector3(-774.2f, 2.6f);
    Vector3 Cam_DetectiveOffice = new Vector3(-797.2f, 1.690001f, -15);

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
            if (DataBaseManager.nowPlace == "Gunshop")
            {
                GunshopButton.SetActive(false);
            }
            else
            {
                GunshopButton.SetActive(true);
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


            if(DataBaseManager.Tightwad == false)
            {
                if (DataBaseManager.nowPlace == "Gunshop")
                {
                    GunshopButton.SetActive(false);
                }
                else
                {
                    GunshopButton.SetActive(true);
                }
            }
  
        }
        if ((DataBaseManager.TimeCount % 4) == 3)
        {
            DetectiveOfficeButton.SetActive(true);
            BarButton.SetActive(true);
            ClientsHouseButton.SetActive(false);
            DailyNewsButton.SetActive(false);
            UnivercityButton.SetActive(false);
            RiverSideButton.SetActive(false);
            HospitalButton.SetActive(false);
            PoliceofficeButton.SetActive(false);
            SlumButton.SetActive(false);
            GunshopButton.SetActive(false);

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
            }

        }


    }

    public void OpenUpperMap()
    {
        Upper.SetActive(true);
        Downer.SetActive(false);
    }
    public void OpenDownMap()
    {
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
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false && DataBaseManager.nowPlace != "BattleRoad")
        {
            DataBaseManager.workSound = false;
            DataBaseManager.isOpenUi = true;
            MapChainingUI.SetActive(true);
        }

    }
    public void CloseMap()
    {
        DataBaseManager.isOpenUi = false;
        MapChainingUI.SetActive(false);
    }
    private void Update()
    {
        buttonChecker();

        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isDirecting == false)
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

    void BattleDialog()
    {
        DirectingManager.Instance.BeforeBattle();
    }

    public void MapOn()
    {
        if (DataBaseManager.isBar == true)
        {
            DataBaseManager.isDirecting = true;
            DataBaseManager.isBar = false;
            DataBaseManager.nowPlace = "BattleRoad";
            DialogDatabaseManager.instance.Check = true;
            CloseMap();
            //DirectingManager.Instance.GotoBattle();

            FadingBackGround.Instance.FadeInOut();
            Invoke("BattleDialog", 1f);
        }
        else
        {
            if ((DataBaseManager.TimeCount % 4) != 0)
            {
                FadingBackGround.Instance.FadeInOut();
                Invoke("OpenSpinUi", 1f);
                Invoke("SpinRing", 3f);
            }
            else if ((DataBaseManager.TimeCount % 4) == 0)
            {
                FadingBackGround.Instance.FadeInOut();
                Invoke("OpenSpinUi", 1f);
                Invoke("WaitFade", 4f);
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
            text.text = "11/7/1921-Morning";
        }
        else if (DataBaseManager.TimeCount == 5)
        {
            text.text = "11/7/1921-Noon";
        }
        else if (DataBaseManager.TimeCount == 6)
        {
            text.text = "11/7/1921-Afternoon";
        }
        else if (DataBaseManager.TimeCount == 7)
        {
            text.text = "11/7/1921-Night";
        }
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

        DataBaseManager.isOpenUi = false;
        CameraManager.Instance.isCheckEnd = true;
        DataBaseManager.TimeCount += 1;
        if (DataBaseManager.nowPlace == "DetectiveOffice")
        {

                player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
                player.transform.localPosition = player_DetectiveOffice;
                camera.transform.localPosition = Cam_DetectiveOffice;
                MapChainingUI.SetActive(false);
                MapUI.SetActive(false);
                DialogDatabaseManager.instance.Check = true;

        }
        else if (DataBaseManager.nowPlace == "Client'shouse")
        {
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_ClientsHouse;
            camera.transform.localPosition = Cam_ClientsHouse;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            Invoke("FirstClientsHouseArrive", 2);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "DailyNews")
        {
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_DailyNews;
            camera.transform.localPosition = Cam_DailyNews;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "University")
        {
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
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_RiverSide;
            camera.transform.localPosition = Cam_RiverSide;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "Hospital")
        {
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_Hospital;
            camera.transform.localPosition = Cam_Hospital;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "Policeoffice")
        {
            player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_Policeoffice;
            camera.transform.localPosition = Cam_Policeoffice;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "Slum")
        {
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_Slum;
            camera.transform.localPosition = Cam_Slum;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        else if (DataBaseManager.nowPlace == "Bar")
        {
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

            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            player.transform.localPosition = player_Gunshop;
            camera.transform.localPosition = Cam_Gunshop;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }


        if(DataBaseManager.PanicAttack == true)
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
