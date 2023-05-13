using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MapManager : MonoBehaviour
{
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

    Vector2 player_RiverSide = new Vector3(-298.34f, 1.77f);
    Vector2 Cam_RiverSide = new Vector3(-313.2f, 1.5f,-16);
    Vector2 player_Univercity = new Vector3(-48.07f, 3.45f);
    Vector2 Cam_Univercity = new Vector3(-38.3f, 1.5f,-16);
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

    private void Update()
    {
        if(DataBaseManager.isActiveDialog1 == false && DataBaseManager.isDirecting == false)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if(MapChainingUI.activeSelf == false)
                {
                    MapChainingUI.SetActive(true);
                }
                else if (MapChainingUI.activeSelf == true)
                {
                    MapChainingUI.SetActive(false);
                }

            }
            if (Input.GetKeyDown(KeyCode.Escape) && MapChainingUI.activeSelf == true)
            {
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
        CameraManager.Instance.isCheckEnd = false;
        DataBaseManager.TimeCount += 1;
        if (DataBaseManager.nowPlace == "RiverSide")
        {
            player.transform.localPosition = player_RiverSide;
            camera.transform.localPosition = Cam_RiverSide;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
        }
        if (DataBaseManager.nowPlace == "Univercity")
        {
            player.transform.localPosition = player_Univercity;
            camera.transform.localPosition = Cam_Univercity;
            MapChainingUI.SetActive(false);
            MapUI.SetActive(false);
        }
    }
}
