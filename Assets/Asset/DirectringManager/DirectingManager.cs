using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DirectingManager : MonoBehaviour
{
    float ChInRommSize = 2.383215f; // -이면 왼쪽 +면 오른쪽
    Vector2 player_OutsideTo1st = new Vector3(-1217.79f, 2.13f);
    Vector3 Cam_OutsideTo1st = new Vector3(-1198, 1.5f, -15);

    Vector2 player_1stTo2st = new Vector3(-1380.6f, 7f);
    Vector3 Cam_1stTo2st = new Vector3(-1368.6f, 1.5f, -15);

    Vector2 player_RoomTo2st = new Vector3(-1365.4f, 7f);
    Vector3 Cam_RoomTo2st = new Vector3(-1368.6f, 1.5f, -15);


    private static DirectingManager instance = null;
    public GameObject player;
    public GameObject Camera;
    //병원
    public GameObject Kate_noon;
    //탐정사무소
    public GameObject sit_player;
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
    void Awake()
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
    // Start is called before the first frame update
    void Start()
    {

        //원래라면 활성화 해 주어야 함.
        FadingBackGround.Instance.FadeOut();
        Invoke("startGame", 2f);
    }



    void startGame()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("start");
    }

    // Update is called once per frame
    void Update()
    {
        if(DataBaseManager.fst_Detectiv_TimeOn >= 2)
        {
            DataBaseManager.isDirecting = true;
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
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
            DataBaseManager.fst_Detective_AfterSelect = false;
            fst_DetectiveOffice_Fade("AfterSelect_Directing");
        }
        if (DataBaseManager.fst_Detective_AfterSelectNo == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.fst_Detective_AfterSelectNo = false;
            fst_DetectiveOffice_Fade("Accept_AfterSelectNo");
        }
        if (DataBaseManager.fst_Detective_EndSelect == true && DataBaseManager.isActiveDialog1 == false)
        {
            DataBaseManager.fst_Detective_EndSelect = false;
            fst_DetectiveOffice_FadeOut("EndSelect_Directing");
        }
        // 의뢰자의 집
        if (DataBaseManager.firstClientsHouse == true)
        {
            DataBaseManager.firstClientsHouse = false;
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
            Rollet.Instance.setRollet("SAN : Check", "Sanity", DataBaseManager.san, "dialog");
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
            FadingBackGround.Instance.DemoEnd_FadeIn();
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
            else
            {
                Invoke("KeyConnect", 3f);
            }
        }
    }
    bool once = false;


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
        Invoke("moveInside",2f);

    }
    void moveInside()
    {
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
        player.transform.localPosition = player_OutsideTo1st;
        Camera.transform.localPosition = Cam_OutsideTo1st;
        Invoke("InsiedDialog", 2f);
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
        Invoke("MoveUpsair", 2f);
        // DataBaseManager.isDirecting = true;
    }
    void MoveUpsair()
    {
        player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = player_1stTo2st;
        Camera.transform.localPosition = Cam_1stTo2st;
        Invoke("UpstairDialog", 2f);
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
        Invoke("ForceRoom", 2f);
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
        Invoke(Connect, 1.5f);
    }
    public void fst_DetectiveOffice_FadeOut(string Connect)
    {
        FadingBackGround.Instance.FadeOut();
        Invoke(Connect, 1.5f);
    }
    public void fst_DetectiveOffice_FadeIn(string Connect)
    {
        FadingBackGround.Instance.FadeInOut();
        Invoke(Connect, 2.5f);
    }

    public void End_Directing()
    {
        sit_player.SetActive(false);
        player.SetActive(true);
        Newspaper.DOFade(1, 0.1f);
    }
    public void Nock_Directing()
    {
        Camera.transform.DOMoveX(-790.3f, 1);
        player.transform.position = new Vector2(-796.7f,player.transform.position.y);
        player.SetActive(false);
        sit_NoNewsPaperPlayer.SetActive(true);
        sit_Ella.SetActive(true);
        Invoke("AfterNock_Directing", 1.5f);

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
        if (DataBaseManager.Intel_PlanetarySequence1 == true)
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
