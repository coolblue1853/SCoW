using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MapManager : MonoBehaviour
{
    public GameObject Upper;
    public GameObject Downer;


    public GameObject ChaingUi;
    public Animator ChaingUiAnim;
    public GameObject Ring1;
    public GameObject Ring2;
    public GameObject Ring3;
    public GameObject Ring4;

    public GameObject player;
    public GameObject camera;
    public GameObject MapChainingUI;
    public GameObject MapUI;

    Vector3 player_DetectiveOffice = new Vector3(-774.2f, 2.6f);
    Vector3 Cam_DetectiveOffice = new Vector3(-784.6f, 1.690001f, -15);

    Vector3 player_ClientsHouse = new Vector3(-990.2f, 2.86f);
    Vector3 Cam_ClientsHouse = new Vector3(-993.87f, 1.5f, -15);

    Vector3 player_DailyNews = new Vector3(-117.6f, -81f);
    Vector3 Cam_DailyNews = new Vector3(-135.7f, -81.21f, -15);

    Vector3 player_Policeoffice = new Vector3(-736.7f, -81f);
    Vector3 Cam_Policeoffice = new Vector3(-721.27f, -81.51f, -15);



    Vector3 player_Univercity = new Vector3(-48.07f, 15);
    Vector3 Cam_Univercity = new Vector3(-38.3f, 1.5f,-15);

    Vector3 player_RiverSide = new Vector3(-298.34f, 1.77f);
    Vector3 Cam_RiverSide = new Vector3(-313.2f, 1.5f, -16);

    Vector3 player_Hospital = new Vector3(-496.1f, -82.5f);
    Vector3 Cam_Hospital = new Vector3(-491.27f, -81.51f, -15);


    Vector3 player_Slum = new Vector3(-849f, -81f);
    Vector3 Cam_Slum = new Vector3(-860.77f, -81.51f, -15);


    Vector3 player_Bar = new Vector3(-666.8f, 2.6f);
    Vector3 Cam_Bar = new Vector3(-650.4f, 1.690001f, -15);
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
        DataBaseManager.isOpenUi = true;
        MapChainingUI.SetActive(true);
    }
    public void CloseMap()
    {
        DataBaseManager.isOpenUi = false;
        MapChainingUI.SetActive(false);
    }
    private void Update()
    {


        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isDirecting == false)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if(MapChainingUI.activeSelf == false)
                {
                    DataBaseManager.isOpenUi = true;
                    MapChainingUI.SetActive(true);
                }
                else if (MapChainingUI.activeSelf == true)
                {
                    DataBaseManager.isOpenUi = false;
                    MapChainingUI.SetActive(false);
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
        FadingBackGround.Instance.FadeInOut();
        Invoke("OpenSpinUi", 2f);
        Invoke("SpinRing", 5f);

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
    }

    public void MoveChar()
    {
        CameraManager.Instance.isCheckEnd = true;
        DataBaseManager.TimeCount += 1;
        if (DataBaseManager.nowPlace == "DetectiveOffice")
        {
            player.transform.localPosition = player_DetectiveOffice;
            camera.transform.localPosition = Cam_DetectiveOffice;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        if (DataBaseManager.nowPlace == "Client'shouse")
        {
            player.transform.localPosition = player_ClientsHouse;
            camera.transform.localPosition = Cam_ClientsHouse;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            Invoke("FirstClientsHouseArrive", 2);
            DialogDatabaseManager.instance.Check = true;
        }
        if (DataBaseManager.nowPlace == "DailyNews")
        {
            player.transform.localPosition = player_DailyNews;
            camera.transform.localPosition = Cam_DailyNews;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        if (DataBaseManager.nowPlace == "Univercity")
        {
            player.transform.localPosition = player_Univercity;
            camera.transform.localPosition = Cam_Univercity;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        if (DataBaseManager.nowPlace == "Riverside")
        {
            player.transform.localPosition = player_RiverSide;
            camera.transform.localPosition = Cam_RiverSide;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        if (DataBaseManager.nowPlace == "Hospital")
        {
            player.transform.localPosition = player_Hospital;
            camera.transform.localPosition = Cam_Hospital;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        if (DataBaseManager.nowPlace == "Policeoffice")
        {
            player.transform.localPosition = player_Policeoffice;
            camera.transform.localPosition = Cam_Policeoffice;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
            DialogDatabaseManager.instance.Check = true;
        }
        if (DataBaseManager.nowPlace == "Slum")
        {
            player.transform.localPosition = player_Slum;
            camera.transform.localPosition = Cam_Slum;
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
