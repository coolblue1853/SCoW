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
    float ChInRommSize = 2.383215f; // -�̸� ���� +�� ������


    public GameObject ChaingUi;
    public GameObject Ring1;
    public GameObject Ring2;
    public GameObject Ring3;
    public GameObject Ring4;

    public GameObject player;
    public GameObject camera;
    public GameObject MapChainingUI;
    public GameObject MapUI;

    //���б�
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

    public GameObject DetectiveOfficeButton;
    public GameObject ClientsHouseButton;
    public GameObject DailyNewsButton;
    public GameObject PoliceofficeButton;
    public GameObject UnivercityButton;
    public GameObject RiverSideButton;
    public GameObject HospitalButton;
    public GameObject SlumButton;
    public GameObject BarButton;
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
            if (DataBaseManager.nowPlace == "Univercity")
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
            if (DataBaseManager.nowPlace == "Univercity")
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


    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
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
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false)
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

   
    public void MapOn()
    {
        if ((DataBaseManager.TimeCount % 4) != 0)
        {
            FadingBackGround.Instance.FadeInOut();
            Invoke("OpenSpinUi", 2f);
            Invoke("SpinRing", 5f);
        }
        else if ((DataBaseManager.TimeCount % 4) == 0)
        {
            FadingBackGround.Instance.FadeInOut();
            Invoke("OpenSpinUi", 2f);
            Invoke("WaitFade", 7f);
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
            Ring1.transform.DORotate(new Vector3(0f, 0, 360f + 360f), 2f).SetAutoKill();
            Ring2.transform.DORotate(new Vector3(0f, 0, 360f + 360f), 3.5f).SetAutoKill();
            Ring3.transform.DORotate(new Vector3(0f, 0, 360f + 360f), 2.5f).SetAutoKill();
            Ring4.transform.DORotate(new Vector3(0f, 0, 360f + 360f), 3f).SetAutoKill();
        }
        if ((DataBaseManager.TimeCount % 4) == 2)
        {
            Ring1.transform.DORotate(new Vector3(0f, 0, 90f + 360f), 2f).SetAutoKill();
            Ring2.transform.DORotate(new Vector3(0f, 0, 90f + 360f), 3.5f).SetAutoKill();
            Ring3.transform.DORotate(new Vector3(0f, 0, 90f + 360f), 2.5f).SetAutoKill();
            Ring4.transform.DORotate(new Vector3(0f, 0, 90f + 360f), 3f).SetAutoKill();
        }
        if ((DataBaseManager.TimeCount % 4) == 3)
        {
            Ring1.transform.DORotate(new Vector3(0f, 0, 180f + 360f), 2f).SetAutoKill();
            Ring2.transform.DORotate(new Vector3(0f, 0, 180f + 360f), 3.5f).SetAutoKill();
            Ring3.transform.DORotate(new Vector3(0f, 0, 180f + 360f), 2.5f).SetAutoKill();
            Ring4.transform.DORotate(new Vector3(0f, 0, 180f + 360f), 3f).SetAutoKill();
        }
        if ((DataBaseManager.TimeCount % 4) == 0)
        {
            Ring1.transform.DORotate(new Vector3(0f, 0, 270f + 360f), 2f).SetAutoKill();
            Ring2.transform.DORotate(new Vector3(0f, 0, 270f + 360f), 3.5f).SetAutoKill();
            Ring3.transform.DORotate(new Vector3(0f, 0, 270f + 360f), 2.5f).SetAutoKill();
            Ring4.transform.DORotate(new Vector3(0f, 0, 270f + 360f), 3f).SetAutoKill();
        }
        Invoke("ChainingDate", 2.5f);
    }

    public void ChainingDate()
    {
        ChaingUiAnim.SetBool("Change", true);
        Invoke("ChainingDate2", 1f);
        Invoke("EndEvent", 2f);
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
        Invoke("MoveChar", 2f);
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
    }

    void FirstClientsHouseArrive()
    {
        DataBaseManager.firstClientsHouse = true;
    }
}
