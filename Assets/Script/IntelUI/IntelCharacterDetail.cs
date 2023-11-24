using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IntelCharacterDetail : MonoBehaviour
{
    // Start is called before the first frame update

    void setGameObecject()
    {
        DataBaseManager.Set_IntelCharDetail = true;
        Detail_Ella1 = EllaContents.transform.GetChild(0).gameObject;
        Detail_Ella2 = EllaContents.transform.GetChild(1).gameObject;
        Detail_Ella3 = EllaContents.transform.GetChild(2).gameObject;
        Detail_Ella4 = EllaContents.transform.GetChild(3).gameObject;
        Detail_Ella5 = EllaContents.transform.GetChild(4).gameObject;
        Detail_Ella6 = EllaContents.transform.GetChild(5).gameObject;

        Detail_Aiden1 = AidenContents.transform.GetChild(0).gameObject;
        Detail_Aiden2 = AidenContents.transform.GetChild(1).gameObject;
        Detail_Aiden3 = AidenContents.transform.GetChild(2).gameObject;
        Detail_Aiden4 = AidenContents.transform.GetChild(3).gameObject;
        Detail_Aiden5 = AidenContents.transform.GetChild(4).gameObject;
        Detail_Aiden6 = AidenContents.transform.GetChild(5).gameObject;

        Detail_Swain1 = SwainContents.transform.GetChild(0).gameObject;
        Detail_Swain2 = SwainContents.transform.GetChild(1).gameObject;
        Detail_Swain3 = SwainContents.transform.GetChild(2).gameObject;
        Detail_Swain4 = SwainContents.transform.GetChild(3).gameObject;
        Detail_Swain5 = SwainContents.transform.GetChild(4).gameObject;
        Detail_Swain6 = SwainContents.transform.GetChild(5).gameObject;

        Detail_Cane1 = CaneContents.transform.GetChild(0).gameObject;
        Detail_Cane2 = CaneContents.transform.GetChild(1).gameObject;
        Detail_Cane3 = CaneContents.transform.GetChild(2).gameObject;
        Detail_Cane4 = CaneContents.transform.GetChild(3).gameObject;
        Detail_Cane5 = CaneContents.transform.GetChild(4).gameObject;
        Detail_Cane6 = CaneContents.transform.GetChild(5).gameObject;

        Detail_Albert1 = AlbertContents.transform.GetChild(0).gameObject;
        Detail_Albert2 = AlbertContents.transform.GetChild(1).gameObject;
        Detail_Albert3 = AlbertContents.transform.GetChild(2).gameObject;
        Detail_Albert4 = AlbertContents.transform.GetChild(3).gameObject;
        Detail_Albert5 = AlbertContents.transform.GetChild(4).gameObject;
        Detail_Albert6 = AlbertContents.transform.GetChild(5).gameObject;

        Detail_Meiv1 = MeivContents.transform.GetChild(0).gameObject;
        Detail_Meiv2 = MeivContents.transform.GetChild(1).gameObject;
        Detail_Meiv3 = MeivContents.transform.GetChild(2).gameObject;
        Detail_Meiv4 = MeivContents.transform.GetChild(3).gameObject;
        Detail_Meiv5 = MeivContents.transform.GetChild(4).gameObject;
        Detail_Meiv6 = MeivContents.transform.GetChild(5).gameObject;

        Detail_BlackWell1 = BlackWellContents.transform.GetChild(0).gameObject;
        Detail_BlackWell2 = BlackWellContents.transform.GetChild(1).gameObject;
        Detail_BlackWell3 = BlackWellContents.transform.GetChild(2).gameObject;
        Detail_BlackWell4 = BlackWellContents.transform.GetChild(3).gameObject;
        Detail_BlackWell5 = BlackWellContents.transform.GetChild(4).gameObject;
        Detail_BlackWell6 = BlackWellContents.transform.GetChild(5).gameObject;

        Detail_SewerWorker1 = SewerWorkerContents.transform.GetChild(0).gameObject;
        Detail_SewerWorker2 = SewerWorkerContents.transform.GetChild(1).gameObject;
        Detail_SewerWorker3 = SewerWorkerContents.transform.GetChild(2).gameObject;
        Detail_SewerWorker4 = SewerWorkerContents.transform.GetChild(3).gameObject;
        Detail_SewerWorker5 = SewerWorkerContents.transform.GetChild(4).gameObject;
        Detail_SewerWorker6 = SewerWorkerContents.transform.GetChild(5).gameObject;


        Detail_Dave1 = DaveContents.transform.GetChild(0).gameObject;
        Detail_Dave2 = DaveContents.transform.GetChild(1).gameObject;
        Detail_Dave3 = DaveContents.transform.GetChild(2).gameObject;
        Detail_Dave4 = DaveContents.transform.GetChild(3).gameObject;
        Detail_Dave5 = DaveContents.transform.GetChild(4).gameObject;
        Detail_Dave6 = DaveContents.transform.GetChild(5).gameObject;
    }








    // Update is called once per frame
    void Update()
    {


        DaveDetail();
        SewerWorkerDetail();
        DaveDetail();
        EllaDetail();
        AidenDetail();
        SwainDetail();
        CaneDetail();
        AlbertDetail();
        MeivDetail();
        BlackWellDetail();


        if (NextButton_Aiden.activeSelf == true || NextButton_Albert.activeSelf == true || NextButton_BlackWell.activeSelf == true || NextButton_Cane.activeSelf == true || NextButton_Ella.activeSelf == true || NextButton_Meiv.activeSelf == true || NextButton_SewerWorker.activeSelf == true || NextButton_Dave.activeSelf == true || NextButton_Swain.activeSelf == true)    {
            if (Input.GetKeyDown(KeyCode.D))
            {
                NextPage();
            }
        }

        if (PrevButton_Aiden.activeSelf == true || PrevButton_Albert.activeSelf == true || PrevButton_BlackWell.activeSelf == true || PrevButton_Cane.activeSelf == true || PrevButton_Ella.activeSelf == true || PrevButton_Meiv.activeSelf == true || PrevButton_SewerWorker.activeSelf == true || NextButton_Dave.activeSelf == true || PrevButton_Swain.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                PrevPage();
            }
        }


        if (Ella_Detail.activeSelf == true)
        {
            NowPage = "Ella";
        }
        else if (Aiden_Detail.activeSelf == true)
        {
            NowPage = "Aiden";
        }
        else if (Swain_Detail.activeSelf == true)
        {
            NowPage = "Swain";
        }
        else if (Cane_Detail.activeSelf == true)
        {
            NowPage = "Cane";
        }
        else if (Albert_Detail.activeSelf == true)
        {
            NowPage = "Albert";
        }
        else if (Meiv_Detail.activeSelf == true)
        {
            NowPage = "Meiv";
        }
        else if (BlackWell_Detail.activeSelf == true)
        {
            NowPage = "BlackWell";
        }
        if (SewerWorker_Detail.activeSelf == true)
        {
            NowPage = "SewerWorker";
        }
        if (Dave_Detail.activeSelf == true)
        {
            NowPage = "Dave";
        }

    }

     string NowPage;

    //엘라
     GameObject Detail_Ella1;
     GameObject Detail_Ella2;
     GameObject Detail_Ella3;
     GameObject Detail_Ella4;
     GameObject Detail_Ella5;
     GameObject Detail_Ella6;
    bool ON_Ella1;
    bool ON_Ella2;
    bool ON_Ella3;
    bool ON_Ella4;
    bool ON_Ella5;
    bool ON_Ella6;
    int Count_Ella = 0;
    public GameObject EllaContents;
    public GameObject Ella_Detail;
    public static int EllaIntelInt;
    public GameObject NextButton_Ella;
    public GameObject PrevButton_Ella;

    //에이든
     GameObject Detail_Aiden1;
     GameObject Detail_Aiden2;
     GameObject Detail_Aiden3;
     GameObject Detail_Aiden4;
     GameObject Detail_Aiden5;
     GameObject Detail_Aiden6;
    bool ON_Aiden1;
    bool ON_Aiden2;
    bool ON_Aiden3;
    bool ON_Aiden4;
    bool ON_Aiden5;
    bool ON_Aiden6;
     int Count_Aiden = 0;
    public GameObject AidenContents;
    public GameObject Aiden_Detail;
    public static int AidenIntelInt;
    public GameObject NextButton_Aiden;
    public GameObject PrevButton_Aiden;
    //

    //스와인
     GameObject Detail_Swain1;
     GameObject Detail_Swain2;
     GameObject Detail_Swain3;
     GameObject Detail_Swain4;
     GameObject Detail_Swain5;
     GameObject Detail_Swain6;
    bool ON_Swain1;
    bool ON_Swain2;
    bool ON_Swain3;
    bool ON_Swain4;
    bool ON_Swain5;
    bool ON_Swain6;
    int Count_Swain = 0;
    public GameObject SwainContents;
    public GameObject Swain_Detail;
    public static int SwainIntelInt;
    public GameObject NextButton_Swain;
    public GameObject PrevButton_Swain;

    //케인
     GameObject Detail_Cane1;
     GameObject Detail_Cane2;
     GameObject Detail_Cane3;
     GameObject Detail_Cane4;
     GameObject Detail_Cane5;
     GameObject Detail_Cane6;
    bool ON_Cane1;
    bool ON_Cane2;
    bool ON_Cane3;
    bool ON_Cane4;
    bool ON_Cane5;
    bool ON_Cane6;
    int Count_Cane = 0;
    public GameObject CaneContents;
    public GameObject Cane_Detail;
    public static int CaneIntelInt;
    public GameObject NextButton_Cane;
    public GameObject PrevButton_Cane;

     GameObject Detail_Albert1;
     GameObject Detail_Albert2;
     GameObject Detail_Albert3;
     GameObject Detail_Albert4;
     GameObject Detail_Albert5;
     GameObject Detail_Albert6;
    bool ON_Albert1;
    bool ON_Albert2;
    bool ON_Albert3;
    bool ON_Albert4;
    bool ON_Albert5;
    bool ON_Albert6;
    int Count_Albert = 0;
    public GameObject AlbertContents;
    public GameObject Albert_Detail;
    public static int AlbertIntelInt;
    public GameObject NextButton_Albert;
    public GameObject PrevButton_Albert;

    GameObject Detail_Meiv1;
    GameObject Detail_Meiv2;
    GameObject Detail_Meiv3;
    GameObject Detail_Meiv4;
    GameObject Detail_Meiv5;
    GameObject Detail_Meiv6;
    bool ON_Meiv1;
    bool ON_Meiv2;
    bool ON_Meiv3;
    bool ON_Meiv4;
    bool ON_Meiv5;
    bool ON_Meiv6;
    int Count_Meiv = 0;
    public GameObject MeivContents;
    public GameObject Meiv_Detail;
    public static int MeivIntelInt;
    public GameObject NextButton_Meiv;
    public GameObject PrevButton_Meiv;


    GameObject Detail_BlackWell1;
    GameObject Detail_BlackWell2;
    GameObject Detail_BlackWell3;
    GameObject Detail_BlackWell4;
    GameObject Detail_BlackWell5;
    GameObject Detail_BlackWell6;
    bool ON_BlackWell1;
    bool ON_BlackWell2;
    bool ON_BlackWell3;
    bool ON_BlackWell4;
    bool ON_BlackWell5;
    bool ON_BlackWell6;
    int Count_BlackWell = 0;
    public GameObject BlackWellContents;
    public GameObject BlackWell_Detail;
    public static int BlackWellIntelInt;
    public GameObject NextButton_BlackWell;
    public GameObject PrevButton_BlackWell;

    //하수도 노동자
    GameObject Detail_SewerWorker1;
    GameObject Detail_SewerWorker2;
    GameObject Detail_SewerWorker3;
    GameObject Detail_SewerWorker4;
    GameObject Detail_SewerWorker5;
    GameObject Detail_SewerWorker6;
    bool ON_SewerWorker1;
    bool ON_SewerWorker2;
    bool ON_SewerWorker3;
    bool ON_SewerWorker4;
    bool ON_SewerWorker5;
    bool ON_SewerWorker6;
    int Count_SewerWorker = 0;
    public GameObject SewerWorkerContents;
    public GameObject SewerWorker_Detail;
    public static int SewerWorkerIntelInt;
    public GameObject NextButton_SewerWorker;
    public GameObject PrevButton_SewerWorker;

    //데이브
    public GameObject Detail_Dave1;
    public GameObject Detail_Dave2;
    public GameObject Detail_Dave3;
    public GameObject Detail_Dave4;
    public GameObject Detail_Dave5;
    public GameObject Detail_Dave6;
    bool ON_Dave1;
    bool ON_Dave2;
    bool ON_Dave3;
    bool ON_Dave4;
    bool ON_Dave5;
    bool ON_Dave6;
    public int Count_Dave = 0;
    public GameObject DaveContents;
    public GameObject Dave_Detail;
    public static int DaveIntelInt;
    public GameObject NextButton_Dave;
    public GameObject PrevButton_Dave;


    private static IntelCharacterDetail instance = null;
    private void Awake()
    {
        setGameObecject();
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
    public static IntelCharacterDetail Instance
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


    void AidenDetail()
    {
        if (ON_Aiden1 == false && DataBaseManager.Intel_Aiden1 == true)
        {
            ON_Aiden1 = true;
            Detail_Aiden1.SetActive(true);
            Detail_Aiden1.transform.SetAsLastSibling();
            Count_Aiden += 1;
        }
        if (ON_Aiden2 == false && DataBaseManager.Intel_Aiden2 == true)
        {
            ON_Aiden2 = true;
            Detail_Aiden2.SetActive(true);
            Detail_Aiden2.transform.SetAsLastSibling();
            Count_Aiden += 1;
        }
        if (ON_Aiden3 == false && DataBaseManager.Intel_Aiden3 == true)
        {
            ON_Aiden3 = true;
            Detail_Aiden3.SetActive(true);
            Detail_Aiden3.transform.SetAsLastSibling();
            Count_Aiden += 1;
        }
        if (ON_Aiden4 == false && DataBaseManager.Intel_Aiden4 == true)
        {
            ON_Aiden4 = true;
            Detail_Aiden4.SetActive(true);
            Detail_Aiden4.transform.SetAsLastSibling();
            Count_Aiden += 1;
        }
        if (ON_Aiden5 == false && DataBaseManager.Intel_Aiden5 == true)
        {
            ON_Aiden5 = true;
            Detail_Aiden5.SetActive(true);
            Detail_Aiden5.transform.SetAsLastSibling();
            Count_Aiden += 1;
        }
        if (ON_Aiden6 == false && DataBaseManager.Intel_Aiden6 == true)
        {
            ON_Aiden6 = true;
            Detail_Aiden6.SetActive(true);
            Detail_Aiden6.transform.SetAsLastSibling();
            Count_Aiden += 1;
        }

        if (Count_Aiden > 2 && DataBaseManager.NowPage_Aiden == 1)
        {
            NextButton_Aiden.SetActive(true);
        }
        else if (Count_Aiden > 4 && DataBaseManager.NowPage_Aiden == 2)
        {
            NextButton_Aiden.SetActive(true);
        }
        else
        {
            NextButton_Aiden.SetActive(false);
        }

        if (DataBaseManager.NowPage_Aiden == 2)
        {
            PrevButton_Aiden.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Aiden == 3)
        {
            PrevButton_Aiden.SetActive(true);
        }
        else
        {
            PrevButton_Aiden.SetActive(false);
        }
    }
    void EllaDetail()
    {
        if (ON_Ella1 == false && DataBaseManager.Intel_Ella1 == true)
        {
            ON_Ella1 = true;
            Detail_Ella1.SetActive(true);
            Detail_Ella1.transform.SetAsLastSibling();
            Count_Ella += 1;
        }
        if (ON_Ella2 == false && DataBaseManager.Intel_Ella2 == true)
        {
            ON_Ella2 = true;
            Detail_Ella2.SetActive(true);
            Detail_Ella2.transform.SetAsLastSibling();
            Count_Ella += 1;
        }
        if (ON_Ella3 == false && DataBaseManager.Intel_Ella3 == true)
        {
            ON_Ella3 = true;
            Detail_Ella3.SetActive(true);
            Detail_Ella3.transform.SetAsLastSibling();
            Count_Ella += 1;
        }
        if (ON_Ella4 == false && DataBaseManager.Intel_Ella4 == true)
        {
            ON_Ella4 = true;
            Detail_Ella4.SetActive(true);
            Detail_Ella4.transform.SetAsLastSibling();
            Count_Ella += 1;
        }
        if (ON_Ella5 == false && DataBaseManager.Intel_Ella5 == true)
        {
            ON_Ella5 = true;
            Detail_Ella5.SetActive(true);
            Detail_Ella5.transform.SetAsLastSibling();
            Count_Ella += 1;
        }
        if (ON_Ella6 == false && DataBaseManager.Intel_Ella6 == true)
        {
            ON_Ella6 = true;
            Detail_Ella6.SetActive(true);
            Detail_Ella6.transform.SetAsLastSibling();
            Count_Ella += 1;
        }

        if (Count_Ella > 2 && DataBaseManager.NowPage_Ella == 1)
        {
            NextButton_Ella.SetActive(true);
        }
        else if (Count_Ella > 4 && DataBaseManager.NowPage_Ella == 2)
        {
            NextButton_Ella.SetActive(true);
        }
        else
        {
            NextButton_Ella.SetActive(false);
        }

        if (DataBaseManager.NowPage_Ella == 2)
        {
            PrevButton_Ella.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Ella == 3)
        {
            PrevButton_Ella.SetActive(true);
        }
        else
        {
            PrevButton_Ella.SetActive(false);
        }
    }
    void SwainDetail()
    {
        if (ON_Swain1 == false && DataBaseManager.Intel_Swain1 == true)
        {
            ON_Swain1 = true;
            Detail_Swain1.SetActive(true);
            Detail_Swain1.transform.SetAsLastSibling();
            Count_Swain += 1;
        }
        if (ON_Swain2 == false && DataBaseManager.Intel_Swain2 == true)
        {
            ON_Swain2 = true;
            Detail_Swain2.SetActive(true);
            Detail_Swain2.transform.SetAsLastSibling();
            Count_Swain += 1;
        }
        if (ON_Swain3 == false && DataBaseManager.Intel_Swain3 == true)
        {
            ON_Swain3 = true;
            Detail_Swain3.SetActive(true);
            Detail_Swain3.transform.SetAsLastSibling();
            Count_Swain += 1;
        }
        if (ON_Swain4 == false && DataBaseManager.Intel_Swain4 == true)
        {
            ON_Swain4 = true;
            Detail_Swain4.SetActive(true);
            Detail_Swain4.transform.SetAsLastSibling();
            Count_Swain += 1;
        }
        if (ON_Swain5 == false && DataBaseManager.Intel_Swain5 == true)
        {
            ON_Swain5 = true;
            Detail_Swain5.SetActive(true);
            Detail_Swain5.transform.SetAsLastSibling();
            Count_Swain += 1;
        }
        if (ON_Swain6 == false && DataBaseManager.Intel_Swain6 == true)
        {
            ON_Swain6 = true;
            Detail_Swain6.SetActive(true);
            Detail_Swain6.transform.SetAsLastSibling();
            Count_Swain += 1;
        }

        if (Count_Swain > 2 && DataBaseManager.NowPage_Swain == 1)
        {
            NextButton_Swain.SetActive(true);
        }
        else if (Count_Swain > 4 && DataBaseManager.NowPage_Swain == 2)
        {
            NextButton_Swain.SetActive(true);
        }
        else
        {
            NextButton_Swain.SetActive(false);
        }

        if (DataBaseManager.NowPage_Swain == 2)
        {
            PrevButton_Swain.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Swain == 3)
        {
            PrevButton_Swain.SetActive(true);
        }
        else
        {
            PrevButton_Swain.SetActive(false);
        }
    }
    void CaneDetail()
    {
        if (ON_Cane1 == false && DataBaseManager.Intel_Cane1 == true)
        {
            ON_Cane1 = true;
            Detail_Cane1.SetActive(true);
            Detail_Cane1.transform.SetAsLastSibling();
            Count_Cane += 1;
        }
        if (ON_Cane2 == false && DataBaseManager.Intel_Cane2 == true)
        {
            ON_Cane2 = true;
            Detail_Cane2.SetActive(true);
            Detail_Cane2.transform.SetAsLastSibling();
            Count_Cane += 1;
        }
        if (ON_Cane3 == false && DataBaseManager.Intel_Cane3 == true)
        {
            ON_Cane3 = true;
            Detail_Cane3.SetActive(true);
            Detail_Cane3.transform.SetAsLastSibling();
            Count_Cane += 1;
        }
        if (ON_Cane4 == false && DataBaseManager.Intel_Cane4 == true)
        {
            ON_Cane4 = true;
            Detail_Cane4.SetActive(true);
            Detail_Cane4.transform.SetAsLastSibling();
            Count_Cane += 1;
        }
        if (ON_Cane5 == false && DataBaseManager.Intel_Cane5 == true)
        {
            ON_Cane5 = true;
            Detail_Cane5.SetActive(true);
            Detail_Cane5.transform.SetAsLastSibling();
            Count_Cane += 1;
        }
        if (ON_Cane6 == false && DataBaseManager.Intel_Cane6 == true)
        {
            ON_Cane6 = true;
            Detail_Cane6.SetActive(true);
            Detail_Cane6.transform.SetAsLastSibling();
            Count_Cane += 1;
        }

        if (Count_Cane > 2 && DataBaseManager.NowPage_Cane == 1)
        {
            NextButton_Cane.SetActive(true);
        }
        else if (Count_Cane > 4 && DataBaseManager.NowPage_Cane == 2)
        {
            NextButton_Cane.SetActive(true);
        }
        else
        {
            NextButton_Cane.SetActive(false);
        }

        if (DataBaseManager.NowPage_Cane == 2)
        {
            PrevButton_Cane.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Cane == 3)
        {
            PrevButton_Cane.SetActive(true);
        }
        else
        {
            PrevButton_Cane.SetActive(false);
        }
    }
    void AlbertDetail()
    {
        if (ON_Albert1 == false && DataBaseManager.Intel_Albert1 == true)
        {
            ON_Albert1 = true;
            Detail_Albert1.SetActive(true);
            Detail_Albert1.transform.SetAsLastSibling();
            Count_Albert += 1;
        }
        if (ON_Albert2 == false && DataBaseManager.Intel_Albert2 == true)
        {
            ON_Albert2 = true;
            Detail_Albert2.SetActive(true);
            Detail_Albert2.transform.SetAsLastSibling();
            Count_Albert += 1;
        }
        if (ON_Albert3 == false && DataBaseManager.Intel_Albert3 == true)
        {
            ON_Albert3 = true;
            Detail_Albert3.SetActive(true);
            Detail_Albert3.transform.SetAsLastSibling();
            Count_Albert += 1;
        }
        if (ON_Albert4 == false && DataBaseManager.Intel_Albert4 == true)
        {
            ON_Albert4 = true;
            Detail_Albert4.SetActive(true);
            Detail_Albert4.transform.SetAsLastSibling();
            Count_Albert += 1;
        }
        if (ON_Albert5 == false && DataBaseManager.Intel_Albert5 == true)
        {
            ON_Albert5 = true;
            Detail_Albert5.SetActive(true);
            Detail_Albert5.transform.SetAsLastSibling();
            Count_Albert += 1;
        }
        if (ON_Albert6 == false && DataBaseManager.Intel_Albert6 == true)
        {
            ON_Albert6 = true;
            Detail_Albert6.SetActive(true);
            Detail_Albert6.transform.SetAsLastSibling();
            Count_Albert += 1;
        }

        if (Count_Albert > 2 && DataBaseManager.NowPage_Albert == 1)
        {
            NextButton_Albert.SetActive(true);
        }
        else if (Count_Albert > 4 && DataBaseManager.NowPage_Albert == 2)
        {
            NextButton_Albert.SetActive(true);
        }
        else
        {
            NextButton_Albert.SetActive(false);
        }

        if (DataBaseManager.NowPage_Albert == 2)
        {
            PrevButton_Albert.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Albert == 3)
        {
            PrevButton_Albert.SetActive(true);
        }
        else
        {
            PrevButton_Albert.SetActive(false);
        }
    }
    void MeivDetail()
    {
        if (ON_Meiv1 == false && DataBaseManager.Intel_Meiv1 == true)
        {
            ON_Meiv1 = true;
            Detail_Meiv1.SetActive(true);
            Detail_Meiv1.transform.SetAsLastSibling();
            Count_Meiv += 1;
        }
        if (ON_Meiv2 == false && DataBaseManager.Intel_Meiv2 == true)
        {
            ON_Meiv2 = true;
            Detail_Meiv2.SetActive(true);
            Detail_Meiv2.transform.SetAsLastSibling();
            Count_Meiv += 1;
        }
        if (ON_Meiv3 == false && DataBaseManager.Intel_Meiv3 == true)
        {
            ON_Meiv3 = true;
            Detail_Meiv3.SetActive(true);
            Detail_Meiv3.transform.SetAsLastSibling();
            Count_Meiv += 1;
        }
        if (ON_Meiv4 == false && DataBaseManager.Intel_Meiv4 == true)
        {
            ON_Meiv4 = true;
            Detail_Meiv4.SetActive(true);
            Detail_Meiv4.transform.SetAsLastSibling();
            Count_Meiv += 1;
        }
        if (ON_Meiv5 == false && DataBaseManager.Intel_Meiv5 == true)
        {
            ON_Meiv5 = true;
            Detail_Meiv5.SetActive(true);
            Detail_Meiv5.transform.SetAsLastSibling();
            Count_Meiv += 1;
        }
        if (ON_Meiv6 == false && DataBaseManager.Intel_Meiv6 == true)
        {
            ON_Meiv6 = true;
            Detail_Meiv6.SetActive(true);
            Detail_Meiv6.transform.SetAsLastSibling();
            Count_Meiv += 1;
        }

        if (Count_Meiv > 2 && DataBaseManager.NowPage_Meiv == 1)
        {
            NextButton_Meiv.SetActive(true);
        }
        else if (Count_Meiv > 4 && DataBaseManager.NowPage_Meiv == 2)
        {
            NextButton_Meiv.SetActive(true);
        }
        else
        {
            NextButton_Meiv.SetActive(false);
        }

        if (DataBaseManager.NowPage_Meiv == 2)
        {
            PrevButton_Meiv.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Meiv == 3)
        {
            PrevButton_Meiv.SetActive(true);
        }
        else
        {
            PrevButton_Meiv.SetActive(false);
        }
    }
    void BlackWellDetail()
    {
        if (ON_BlackWell1 == false && DataBaseManager.Intel_BlackWell1 == true)
        {
            ON_BlackWell1 = true;
            Detail_BlackWell1.SetActive(true);
            Detail_BlackWell1.transform.SetAsLastSibling();
            Count_BlackWell += 1;
        }
        if (ON_BlackWell2 == false && DataBaseManager.Intel_BlackWell2 == true)
        {
            ON_BlackWell2 = true;
            Detail_BlackWell2.SetActive(true);
            Detail_BlackWell2.transform.SetAsLastSibling();
            Count_BlackWell += 1;
        }
        if (ON_BlackWell3 == false && DataBaseManager.Intel_BlackWell3 == true)
        {
            ON_BlackWell3 = true;
            Detail_BlackWell3.SetActive(true);
            Detail_BlackWell3.transform.SetAsLastSibling();
            Count_BlackWell += 1;
        }
        if (ON_BlackWell4 == false && DataBaseManager.Intel_BlackWell4 == true)
        {
            ON_BlackWell4 = true;
            Detail_BlackWell4.SetActive(true);
            Detail_BlackWell4.transform.SetAsLastSibling();
            Count_BlackWell += 1;
        }
        if (ON_BlackWell5 == false && DataBaseManager.Intel_BlackWell5 == true)
        {
            ON_BlackWell5 = true;
            Detail_BlackWell5.SetActive(true);
            Detail_BlackWell5.transform.SetAsLastSibling();
            Count_BlackWell += 1;
        }
        if (ON_BlackWell6 == false && DataBaseManager.Intel_BlackWell6 == true)
        {
            ON_BlackWell6 = true;
            Detail_BlackWell6.SetActive(true);
            Detail_BlackWell6.transform.SetAsLastSibling();
            Count_BlackWell += 1;
        }

        if (Count_BlackWell > 2 && DataBaseManager.NowPage_BlackWell == 1)
        {
            NextButton_BlackWell.SetActive(true);
        }
        else if (Count_BlackWell > 4 && DataBaseManager.NowPage_BlackWell == 2)
        {
            NextButton_BlackWell.SetActive(true);
        }
        else
        {
            NextButton_BlackWell.SetActive(false);
        }

        if (DataBaseManager.NowPage_BlackWell == 2)
        {
            PrevButton_BlackWell.SetActive(true);
        }
        else if (DataBaseManager.NowPage_BlackWell == 3)
        {
            PrevButton_BlackWell.SetActive(true);
        }
        else
        {
            PrevButton_BlackWell.SetActive(false);
        }
    }
    void SewerWorkerDetail()
    {
        if (ON_SewerWorker1 == false && DataBaseManager.Intel_SewerWorker1 == true)
        {
            ON_SewerWorker1 = true;
            Detail_SewerWorker1.SetActive(true);
            Detail_SewerWorker1.transform.SetAsLastSibling();
            Count_SewerWorker += 1;
        }
        if (ON_SewerWorker2 == false && DataBaseManager.Intel_SewerWorker2 == true)
        {
            ON_SewerWorker2 = true;
            Detail_SewerWorker2.SetActive(true);
            Detail_SewerWorker2.transform.SetAsLastSibling();
            Count_SewerWorker += 1;
        }
        if (ON_SewerWorker3 == false && DataBaseManager.Intel_SewerWorker3 == true)
        {
            ON_SewerWorker3 = true;
            Detail_SewerWorker3.SetActive(true);
            Detail_SewerWorker3.transform.SetAsLastSibling();
            Count_SewerWorker += 1;
        }
        if (ON_SewerWorker4 == false && DataBaseManager.Intel_SewerWorker4 == true)
        {
            ON_SewerWorker4 = true;
            Detail_SewerWorker4.SetActive(true);
            Detail_SewerWorker4.transform.SetAsLastSibling();
            Count_SewerWorker += 1;
        }
        if (ON_SewerWorker5 == false && DataBaseManager.Intel_SewerWorker5 == true)
        {
            ON_SewerWorker5 = true;
            Detail_SewerWorker5.SetActive(true);
            Detail_SewerWorker5.transform.SetAsLastSibling();
            Count_SewerWorker += 1;
        }
        if (ON_SewerWorker6 == false && DataBaseManager.Intel_SewerWorker6 == true)
        {
            ON_SewerWorker6 = true;
            Detail_SewerWorker6.SetActive(true);
            Detail_SewerWorker6.transform.SetAsLastSibling();
            Count_SewerWorker += 1;
        }

        if (Count_SewerWorker > 2 && DataBaseManager.NowPage_SewerWorker == 1)
        {
            NextButton_SewerWorker.SetActive(true);
        }
        else if (Count_SewerWorker > 4 && DataBaseManager.NowPage_SewerWorker == 2)
        {
            NextButton_SewerWorker.SetActive(true);
        }
        else
        {
            NextButton_SewerWorker.SetActive(false);
        }

        if (DataBaseManager.NowPage_SewerWorker == 2)
        {
            PrevButton_SewerWorker.SetActive(true);
        }
        else if (DataBaseManager.NowPage_SewerWorker == 3)
        {
            PrevButton_SewerWorker.SetActive(true);
        }
        else
        {
            PrevButton_SewerWorker.SetActive(false);
        }
    }

    void DaveDetail()
    {
        if (ON_Dave1 == false && DataBaseManager.Intel_Dave1 == true)
        {
            ON_Dave1 = true;
            Detail_Dave1.SetActive(true);
            Detail_Dave1.transform.SetAsLastSibling();
            Count_Dave += 1;
        }
        if (ON_Dave2 == false && DataBaseManager.Intel_Dave2 == true)
        {
            ON_Dave2 = true;
            Detail_Dave2.SetActive(true);
            Detail_Dave2.transform.SetAsLastSibling();
            Count_Dave += 1;
        }
        if (ON_Dave3 == false && DataBaseManager.Intel_Dave3 == true)
        {
            ON_Dave3 = true;
            Detail_Dave3.SetActive(true);
            Detail_Dave3.transform.SetAsLastSibling();
            Count_Dave += 1;
        }
        if (ON_Dave4 == false && DataBaseManager.Intel_Dave4 == true)
        {
            ON_Dave4 = true;
            Detail_Dave4.SetActive(true);
            Detail_Dave4.transform.SetAsLastSibling();
            Count_Dave += 1;
        }
        if (ON_Dave5 == false && DataBaseManager.Intel_Dave5 == true)
        {
            ON_Dave5 = true;
            Detail_Dave5.SetActive(true);
            Detail_Dave5.transform.SetAsLastSibling();
            Count_Dave += 1;
        }
        if (ON_Dave6 == false && DataBaseManager.Intel_Dave6 == true)
        {
            ON_Dave6 = true;
            Detail_Dave6.SetActive(true);
            Detail_Dave6.transform.SetAsLastSibling();
            Count_Dave += 1;
        }

        if (Count_Dave > 2 && DataBaseManager.NowPage_Dave == 1)
        {
            NextButton_Dave.SetActive(true);
        }
        else if (Count_Dave > 4 && DataBaseManager.NowPage_Dave == 2)
        {
            NextButton_Dave.SetActive(true);
        }
        else
        {
            NextButton_Dave.SetActive(false);
        }

        if (DataBaseManager.NowPage_Dave == 2)
        {
            PrevButton_Dave.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Dave == 3)
        {
            PrevButton_Dave.SetActive(true);
        }
        else
        {
            PrevButton_Dave.SetActive(false);
        }
    }
    public void NextPage()
    {
        if(NowPage == "Ella")
        {
            if(DataBaseManager.NowPage_Ella == 1)
            {
                if (Count_Ella >= 6)
                {
                    Reset_Ella();
                    EllaContents.transform.GetChild(2).gameObject.SetActive(true);
                    EllaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 5)
                {
                    Reset_Ella();
                    EllaContents.transform.GetChild(3).gameObject.SetActive(true);
                    EllaContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 4)
                {
                    Reset_Ella();
                    EllaContents.transform.GetChild(4).gameObject.SetActive(true);
                    EllaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 3)
                {
                    Reset_Ella();
                    EllaContents.transform.GetChild(5).gameObject.SetActive(true);   
                }
                DataBaseManager.NowPage_Ella = 2;
            }
            else if (DataBaseManager.NowPage_Ella == 2)
            {
                if (Count_Ella >= 6)
                {
                    Reset_Ella();
                    EllaContents.transform.GetChild(4).gameObject.SetActive(true);
                    EllaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 5)
                {
                    Reset_Ella();
                    EllaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Ella = 3;
            }
        }
        if (NowPage == "Aiden")
        {
            if (DataBaseManager.NowPage_Aiden == 1)
            {
                if (Count_Aiden >= 6)
                {
                    Aiden_Ella();
                    AidenContents.transform.GetChild(2).gameObject.SetActive(true);
                    AidenContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 5)
                {
                    Aiden_Ella();
                    AidenContents.transform.GetChild(3).gameObject.SetActive(true);
                    AidenContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 4)
                {
                    Aiden_Ella();
                    AidenContents.transform.GetChild(4).gameObject.SetActive(true);
                    AidenContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 3)
                {
                    Aiden_Ella();
                    AidenContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Aiden = 2;
            }
            else if (DataBaseManager.NowPage_Aiden == 2)
            {
                if (Count_Aiden >= 6)
                {
                    Aiden_Ella();
                    AidenContents.transform.GetChild(4).gameObject.SetActive(true);
                    AidenContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 5)
                {
                    Aiden_Ella();
                    AidenContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Aiden = 3;
            }
        }
        if (NowPage == "Swain")
        {
            if (DataBaseManager.NowPage_Swain == 1)
            {
                if (Count_Swain >= 6)
                {
                    Reset_Swain();
                    SwainContents.transform.GetChild(2).gameObject.SetActive(true);
                    SwainContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Swain >= 5)
                {
                    Reset_Swain();
                    SwainContents.transform.GetChild(3).gameObject.SetActive(true);
                    SwainContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Swain >= 4)
                {
                    Reset_Swain();
                    SwainContents.transform.GetChild(4).gameObject.SetActive(true);
                    SwainContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Swain >= 3)
                {
                    Reset_Swain();
                    SwainContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Swain = 2;
            }
            else if (DataBaseManager.NowPage_Swain == 2)
            {
                if (Count_Swain >= 6)
                {
                    Reset_Swain();
                    SwainContents.transform.GetChild(4).gameObject.SetActive(true);
                    SwainContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Swain >= 5)
                {
                    Reset_Swain();
                    SwainContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Swain = 3;
            }
        }
        if (NowPage == "Cane")
        {
            if (DataBaseManager.NowPage_Cane == 1)
            {
                if (Count_Cane >= 6)
                {
                    Reset_Cane();
                    CaneContents.transform.GetChild(2).gameObject.SetActive(true);
                    CaneContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Cane >= 5)
                {
                    Reset_Cane();
                    CaneContents.transform.GetChild(3).gameObject.SetActive(true);
                    CaneContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Cane >= 4)
                {
                    Reset_Cane();
                    CaneContents.transform.GetChild(4).gameObject.SetActive(true);
                    CaneContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Cane >= 3)
                {
                    Reset_Cane();
                    CaneContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Cane = 2;
            }
            else if (DataBaseManager.NowPage_Cane == 2)
            {
                if (Count_Cane >= 6)
                {
                    Reset_Cane();
                    CaneContents.transform.GetChild(4).gameObject.SetActive(true);
                    CaneContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Cane >= 5)
                {
                    Reset_Cane();
                    CaneContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Cane = 3;
            }
        }
        if (NowPage == "Albert")
        {
            if (DataBaseManager.NowPage_Albert == 1)
            {
                if (Count_Albert >= 6)
                {
                    Reset_Albert();
                    AlbertContents.transform.GetChild(2).gameObject.SetActive(true);
                    AlbertContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Albert >= 5)
                {
                    Reset_Albert();
                    AlbertContents.transform.GetChild(3).gameObject.SetActive(true);
                    AlbertContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Albert >= 4)
                {
                    Reset_Albert();
                    AlbertContents.transform.GetChild(4).gameObject.SetActive(true);
                    AlbertContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Albert >= 3)
                {
                    Reset_Albert();
                    AlbertContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Albert = 2;
            }
            else if (DataBaseManager.NowPage_Albert == 2)
            {
                if (Count_Albert >= 6)
                {
                    Reset_Albert();
                    AlbertContents.transform.GetChild(4).gameObject.SetActive(true);
                    AlbertContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Albert >= 5)
                {
                    Reset_Albert();
                    AlbertContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Albert = 3;
            }
        }
        if (NowPage == "Meiv")
        {
            if (DataBaseManager.NowPage_Meiv == 1)
            {
                if (Count_Meiv >= 6)
                {
                    Reset_Meiv();
                    MeivContents.transform.GetChild(2).gameObject.SetActive(true);
                    MeivContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Meiv >= 5)
                {
                    Reset_Meiv();
                    MeivContents.transform.GetChild(3).gameObject.SetActive(true);
                    MeivContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Meiv >= 4)
                {
                    Reset_Meiv();
                    MeivContents.transform.GetChild(4).gameObject.SetActive(true);
                    MeivContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Meiv >= 3)
                {
                    Reset_Meiv();
                    MeivContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Meiv = 2;
            }
            else if (DataBaseManager.NowPage_Meiv == 2)
            {
                if (Count_Meiv >= 6)
                {
                    Reset_Meiv();
                    MeivContents.transform.GetChild(4).gameObject.SetActive(true);
                    MeivContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Meiv >= 5)
                {
                    Reset_Meiv();
                    MeivContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Meiv = 3;
            }
        }
        if (NowPage == "BlackWell")
        {
            if (DataBaseManager.NowPage_BlackWell == 1)
            {
                if (Count_BlackWell >= 6)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(2).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 5)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 4)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 3)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_BlackWell = 2;
            }
            else if (DataBaseManager.NowPage_BlackWell == 2)
            {
                if (Count_BlackWell >= 6)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 5)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_BlackWell = 3;
            }
        }
        if (NowPage == "SewerWorker")
        {
            if (DataBaseManager.NowPage_SewerWorker == 1)
            {
                if (Count_SewerWorker >= 6)
                {
                    Reset_SewerWorker();
                    SewerWorkerContents.transform.GetChild(2).gameObject.SetActive(true);
                    SewerWorkerContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_SewerWorker >= 5)
                {
                    Reset_SewerWorker();
                    SewerWorkerContents.transform.GetChild(3).gameObject.SetActive(true);
                    SewerWorkerContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_SewerWorker >= 4)
                {
                    Reset_SewerWorker();
                    SewerWorkerContents.transform.GetChild(4).gameObject.SetActive(true);
                    SewerWorkerContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_SewerWorker >= 3)
                {
                    Reset_SewerWorker();
                    SewerWorkerContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewerWorker = 2;
            }
            else if (DataBaseManager.NowPage_SewerWorker == 2)
            {
                if (Count_SewerWorker >= 6)
                {
                    Reset_SewerWorker();
                    SewerWorkerContents.transform.GetChild(4).gameObject.SetActive(true);
                    SewerWorkerContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_SewerWorker >= 5)
                {
                    Reset_SewerWorker();
                    SewerWorkerContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewerWorker = 3;
            }
        }
        if (NowPage == "Dave")
        {
            if (DataBaseManager.NowPage_Dave == 1)
            {
                if (Count_Dave >= 6)
                {
                    Reset_Dave();
                    DaveContents.transform.GetChild(2).gameObject.SetActive(true);
                    DaveContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Dave >= 5)
                {
                    Reset_Dave();
                    DaveContents.transform.GetChild(3).gameObject.SetActive(true);
                    DaveContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Dave >= 4)
                {
                    Reset_Dave();
                    DaveContents.transform.GetChild(4).gameObject.SetActive(true);
                    DaveContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Dave >= 3)
                {
                    Reset_Dave();
                    DaveContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Dave = 2;
            }
            else if (DataBaseManager.NowPage_Dave == 2)
            {
                if (Count_Dave >= 6)
                {
                    Reset_Dave();
                    DaveContents.transform.GetChild(4).gameObject.SetActive(true);
                    DaveContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Dave >= 5)
                {
                    Reset_Dave();
                    DaveContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Dave = 3;
            }
        }
    }

    public void PrevPage()
    {
        if (NowPage == "Ella")
        {
            if (DataBaseManager.NowPage_Ella == 2)
            {
                if (Count_Ella >= 6)
                {
                    Reset_Ella();
                    EllaContents.transform.GetChild(0).gameObject.SetActive(true);
                    EllaContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 5)
                {
                    Reset_Ella();
                    EllaContents.transform.GetChild(1).gameObject.SetActive(true);
                    EllaContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 4)
                {
                    Reset_Ella();
                    EllaContents.transform.GetChild(2).gameObject.SetActive(true);
                    EllaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 3)
                {
                    Reset_Ella();
                    EllaContents.transform.GetChild(3).gameObject.SetActive(true);
                    EllaContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Ella = 1;
            }
            else if (DataBaseManager.NowPage_Ella == 3)
            {
                if (Count_Ella >= 6)
                {
                    Reset_Ella();
                    EllaContents.transform.GetChild(2).gameObject.SetActive(true);
                    EllaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 5)
                {
                    Reset_Ella();
                    EllaContents.transform.GetChild(3).gameObject.SetActive(true);
                    EllaContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Ella = 2;
            }
        }
        if (NowPage == "Aiden")
        {
            if (DataBaseManager.NowPage_Aiden == 2)
            {
                if (Count_Aiden >= 6)
                {
                    Aiden_Ella();
                    AidenContents.transform.GetChild(0).gameObject.SetActive(true);
                    AidenContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 5)
                {
                    Aiden_Ella();
                    AidenContents.transform.GetChild(1).gameObject.SetActive(true);
                    AidenContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 4)
                {
                    Aiden_Ella();
                    AidenContents.transform.GetChild(2).gameObject.SetActive(true);
                    AidenContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 3)
                {
                    Aiden_Ella();
                    AidenContents.transform.GetChild(3).gameObject.SetActive(true);
                    AidenContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Aiden = 1;
            }
            else if (DataBaseManager.NowPage_Aiden == 3)
            {
                if (Count_Aiden >= 6)
                {
                    Aiden_Ella();
                    AidenContents.transform.GetChild(2).gameObject.SetActive(true);
                    AidenContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 5)
                {
                    Aiden_Ella();
                    AidenContents.transform.GetChild(3).gameObject.SetActive(true);
                    AidenContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Aiden = 2;
            }
        }
        if (NowPage == "Swain")
        {
            if (DataBaseManager.NowPage_Swain == 2)
            {
                if (Count_Swain >= 6)
                {
                    Reset_Swain();
                    SwainContents.transform.GetChild(0).gameObject.SetActive(true);
                    SwainContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Swain >= 5)
                {
                    Reset_Swain();
                    SwainContents.transform.GetChild(1).gameObject.SetActive(true);
                    SwainContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Swain >= 4)
                {
                    Reset_Swain();
                    SwainContents.transform.GetChild(2).gameObject.SetActive(true);
                    SwainContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Swain >= 3)
                {
                    Reset_Swain();
                    SwainContents.transform.GetChild(3).gameObject.SetActive(true);
                    SwainContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Swain = 1;
            }
            else if (DataBaseManager.NowPage_Swain == 3)
            {
                if (Count_Swain >= 6)
                {
                    Reset_Swain();
                    SwainContents.transform.GetChild(2).gameObject.SetActive(true);
                    SwainContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Swain >= 5)
                {
                    Reset_Swain();
                    SwainContents.transform.GetChild(3).gameObject.SetActive(true);
                    SwainContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Swain = 2;
            }
        }
        if (NowPage == "Cane")
        {
            if (DataBaseManager.NowPage_Cane == 2)
            {
                if (Count_Cane >= 6)
                {
                    Reset_Cane();
                    CaneContents.transform.GetChild(0).gameObject.SetActive(true);
                    CaneContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Cane >= 5)
                {
                    Reset_Cane();
                    CaneContents.transform.GetChild(1).gameObject.SetActive(true);
                    CaneContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Cane >= 4)
                {
                    Reset_Cane();
                    CaneContents.transform.GetChild(2).gameObject.SetActive(true);
                    CaneContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Cane >= 3)
                {
                    Reset_Cane();
                    CaneContents.transform.GetChild(3).gameObject.SetActive(true);
                    CaneContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Cane = 1;
            }
            else if (DataBaseManager.NowPage_Cane == 3)
            {
                if (Count_Cane >= 6)
                {
                    Reset_Cane();
                    CaneContents.transform.GetChild(2).gameObject.SetActive(true);
                    CaneContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Cane >= 5)
                {
                    Reset_Cane();
                    CaneContents.transform.GetChild(3).gameObject.SetActive(true);
                    CaneContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Cane = 2;
            }
        }
        if (NowPage == "Albert")
        {
            if (DataBaseManager.NowPage_Albert == 2)
            {
                if (Count_Albert >= 6)
                {
                    Reset_Albert();
                    AlbertContents.transform.GetChild(0).gameObject.SetActive(true);
                    AlbertContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Albert >= 5)
                {
                    Reset_Albert();
                    AlbertContents.transform.GetChild(1).gameObject.SetActive(true);
                    AlbertContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Albert >= 4)
                {
                    Reset_Albert();
                    AlbertContents.transform.GetChild(2).gameObject.SetActive(true);
                    AlbertContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Albert >= 3)
                {
                    Reset_Albert();
                    AlbertContents.transform.GetChild(3).gameObject.SetActive(true);
                    AlbertContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Albert = 1;
            }
            else if (DataBaseManager.NowPage_Albert == 3)
            {
                if (Count_Albert >= 6)
                {
                    Reset_Albert();
                    AlbertContents.transform.GetChild(2).gameObject.SetActive(true);
                    AlbertContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Albert >= 5)
                {
                    Reset_Albert();
                    AlbertContents.transform.GetChild(3).gameObject.SetActive(true);
                    AlbertContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Albert = 2;
            }
        }
        if (NowPage == "Meiv")
        {
            if (DataBaseManager.NowPage_Meiv == 2)
            {
                if (Count_Meiv >= 6)
                {
                    Reset_Meiv();
                    MeivContents.transform.GetChild(0).gameObject.SetActive(true);
                    MeivContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Meiv >= 5)
                {
                    Reset_Meiv();
                    MeivContents.transform.GetChild(1).gameObject.SetActive(true);
                    MeivContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Meiv >= 4)
                {
                    Reset_Meiv();
                    MeivContents.transform.GetChild(2).gameObject.SetActive(true);
                    MeivContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Meiv >= 3)
                {
                    Reset_Meiv();
                    MeivContents.transform.GetChild(3).gameObject.SetActive(true);
                    MeivContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Meiv = 1;
            }
            else if (DataBaseManager.NowPage_Meiv == 3)
            {
                if (Count_Meiv >= 6)
                {
                    Reset_Meiv();
                    MeivContents.transform.GetChild(2).gameObject.SetActive(true);
                    MeivContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Meiv >= 5)
                {
                    Reset_Meiv();
                    MeivContents.transform.GetChild(3).gameObject.SetActive(true);
                    MeivContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Meiv = 2;
            }
        }
        if (NowPage == "BlackWell")
        {
            if (DataBaseManager.NowPage_BlackWell == 2)
            {
                if (Count_BlackWell >= 6)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(0).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 5)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(1).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 4)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(2).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 3)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_BlackWell = 1;
            }
            else if (DataBaseManager.NowPage_BlackWell == 3)
            {
                if (Count_BlackWell >= 6)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(2).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_BlackWell >= 5)
                {
                    Reset_BlackWell();
                    BlackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                    BlackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_BlackWell = 2;
            }
        }
        if (NowPage == "SewerWorker")
        {
            if (DataBaseManager.NowPage_SewerWorker == 2)
            {
                if (Count_SewerWorker >= 6)
                {
                    Reset_SewerWorker();
                    SewerWorkerContents.transform.GetChild(0).gameObject.SetActive(true);
                    SewerWorkerContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_SewerWorker >= 5)
                {
                    Reset_SewerWorker();
                    SewerWorkerContents.transform.GetChild(1).gameObject.SetActive(true);
                    SewerWorkerContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_SewerWorker >= 4)
                {
                    Reset_SewerWorker();
                    SewerWorkerContents.transform.GetChild(2).gameObject.SetActive(true);
                    SewerWorkerContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_SewerWorker >= 3)
                {
                    Reset_SewerWorker();
                    SewerWorkerContents.transform.GetChild(3).gameObject.SetActive(true);
                    SewerWorkerContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_SewerWorker = 1;
            }
            else if (DataBaseManager.NowPage_SewerWorker == 3)
            {
                if (Count_SewerWorker >= 6)
                {
                    Reset_SewerWorker();
                    SewerWorkerContents.transform.GetChild(2).gameObject.SetActive(true);
                    SewerWorkerContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_SewerWorker >= 5)
                {
                    Reset_SewerWorker();
                    SewerWorkerContents.transform.GetChild(3).gameObject.SetActive(true);
                    SewerWorkerContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_SewerWorker = 2;
            }
        }
        if (NowPage == "Dave")
        {
            if (DataBaseManager.NowPage_Dave == 2)
            {
                if (Count_Dave >= 6)
                {
                    Reset_Dave();
                    DaveContents.transform.GetChild(0).gameObject.SetActive(true);
                    DaveContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Dave >= 5)
                {
                    Reset_Dave();
                    DaveContents.transform.GetChild(1).gameObject.SetActive(true);
                    DaveContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Dave >= 4)
                {
                    Reset_Dave();
                    DaveContents.transform.GetChild(2).gameObject.SetActive(true);
                    DaveContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Dave >= 3)
                {
                    Reset_Dave();
                    DaveContents.transform.GetChild(3).gameObject.SetActive(true);
                    DaveContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Dave = 1;
            }
            else if (DataBaseManager.NowPage_Dave == 3)
            {
                if (Count_Dave >= 6)
                {
                    Reset_Dave();
                    DaveContents.transform.GetChild(2).gameObject.SetActive(true);
                    DaveContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Dave >= 5)
                {
                    Reset_Dave();
                    DaveContents.transform.GetChild(3).gameObject.SetActive(true);
                    DaveContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Dave = 2;
            }
        }
    }






    void Reset_Ella()
    {
        EllaContents.transform.GetChild(0).gameObject.SetActive(false);
        EllaContents.transform.GetChild(1).gameObject.SetActive(false);
        EllaContents.transform.GetChild(2).gameObject.SetActive(false);
        EllaContents.transform.GetChild(3).gameObject.SetActive(false);
        EllaContents.transform.GetChild(4).gameObject.SetActive(false);
        EllaContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Aiden_Ella()
    {
        AidenContents.transform.GetChild(0).gameObject.SetActive(false);
        AidenContents.transform.GetChild(1).gameObject.SetActive(false);
        AidenContents.transform.GetChild(2).gameObject.SetActive(false);
        AidenContents.transform.GetChild(3).gameObject.SetActive(false);
        AidenContents.transform.GetChild(4).gameObject.SetActive(false);
        AidenContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Swain()
    {
        SwainContents.transform.GetChild(0).gameObject.SetActive(false);
        SwainContents.transform.GetChild(1).gameObject.SetActive(false);
        SwainContents.transform.GetChild(2).gameObject.SetActive(false);
        SwainContents.transform.GetChild(3).gameObject.SetActive(false);
        SwainContents.transform.GetChild(4).gameObject.SetActive(false);
        SwainContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Cane()
    {
        CaneContents.transform.GetChild(0).gameObject.SetActive(false);
        CaneContents.transform.GetChild(1).gameObject.SetActive(false);
        CaneContents.transform.GetChild(2).gameObject.SetActive(false);
        CaneContents.transform.GetChild(3).gameObject.SetActive(false);
        CaneContents.transform.GetChild(4).gameObject.SetActive(false);
        CaneContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Albert()
    {
        AlbertContents.transform.GetChild(0).gameObject.SetActive(false);
        AlbertContents.transform.GetChild(1).gameObject.SetActive(false);
        AlbertContents.transform.GetChild(2).gameObject.SetActive(false);
        AlbertContents.transform.GetChild(3).gameObject.SetActive(false);
        AlbertContents.transform.GetChild(4).gameObject.SetActive(false);
        AlbertContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Meiv()
    {
        MeivContents.transform.GetChild(0).gameObject.SetActive(false);
        MeivContents.transform.GetChild(1).gameObject.SetActive(false);
        MeivContents.transform.GetChild(2).gameObject.SetActive(false);
        MeivContents.transform.GetChild(3).gameObject.SetActive(false);
        MeivContents.transform.GetChild(4).gameObject.SetActive(false);
        MeivContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_BlackWell()
    {
        BlackWellContents.transform.GetChild(0).gameObject.SetActive(false);
        BlackWellContents.transform.GetChild(1).gameObject.SetActive(false);
        BlackWellContents.transform.GetChild(2).gameObject.SetActive(false);
        BlackWellContents.transform.GetChild(3).gameObject.SetActive(false);
        BlackWellContents.transform.GetChild(4).gameObject.SetActive(false);
        BlackWellContents.transform.GetChild(5).gameObject.SetActive(false);
    }

    void Reset_SewerWorker()
    {
        SewerWorkerContents.transform.GetChild(0).gameObject.SetActive(false);
        SewerWorkerContents.transform.GetChild(1).gameObject.SetActive(false);
        SewerWorkerContents.transform.GetChild(2).gameObject.SetActive(false);
        SewerWorkerContents.transform.GetChild(3).gameObject.SetActive(false);
        SewerWorkerContents.transform.GetChild(4).gameObject.SetActive(false);
        SewerWorkerContents.transform.GetChild(5).gameObject.SetActive(false);
    }

    void Reset_Dave()
    {
        DaveContents.transform.GetChild(0).gameObject.SetActive(false);
        DaveContents.transform.GetChild(1).gameObject.SetActive(false);
        DaveContents.transform.GetChild(2).gameObject.SetActive(false);
        DaveContents.transform.GetChild(3).gameObject.SetActive(false);
        DaveContents.transform.GetChild(4).gameObject.SetActive(false);
        DaveContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    public void Open_Ella()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        Ella_Detail.SetActive(true);
    }
    public void Open_Swain()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        Swain_Detail.SetActive(true);
    }
    public void Open_Aiden()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        Aiden_Detail.SetActive(true);
    }
    public void Open_Cane()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        Cane_Detail.SetActive(true);
    }
    public void Open_Albert()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        Albert_Detail.SetActive(true);
    }
    public void Open_Meiv()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        Meiv_Detail.SetActive(true);
    }
    public void Open_BlackWell()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        BlackWell_Detail.SetActive(true);
    }

    public void Open_SewerWorker()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        SewerWorker_Detail.SetActive(true);
    }

    public void Open_Dave()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        Dave_Detail.SetActive(true);
    }
    void CloseAllContents()
    {
        Ella_Detail.SetActive(false);
        Aiden_Detail.SetActive(false);
        Cane_Detail.SetActive(false);
        Swain_Detail.SetActive(false);
        Albert_Detail.SetActive(false);
        Meiv_Detail.SetActive(false);
        BlackWell_Detail.SetActive(false);
        SewerWorker_Detail.SetActive(false);
        Dave_Detail.SetActive(false);
    }

}
