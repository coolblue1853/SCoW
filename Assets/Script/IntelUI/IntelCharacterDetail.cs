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
        DataBaseManager.setIntelCharDetail = true;
        detailElla1 = ellaContents.transform.GetChild(0).gameObject;
        detailElla2 = ellaContents.transform.GetChild(1).gameObject;
        detailElla3 = ellaContents.transform.GetChild(2).gameObject;
        detailElla4 = ellaContents.transform.GetChild(3).gameObject;
        detailElla5 = ellaContents.transform.GetChild(4).gameObject;
        detailElla6 = ellaContents.transform.GetChild(5).gameObject;

        detailAiden1 = aidenContents.transform.GetChild(0).gameObject;
        detailAiden2 = aidenContents.transform.GetChild(1).gameObject;
        detailAiden3 = aidenContents.transform.GetChild(2).gameObject;
        detailAiden4 = aidenContents.transform.GetChild(3).gameObject;
        detailAiden5 = aidenContents.transform.GetChild(4).gameObject;
        detailAiden6 = aidenContents.transform.GetChild(5).gameObject;

        detailSwain1 = swainContents.transform.GetChild(0).gameObject;
        detailSwain2 = swainContents.transform.GetChild(1).gameObject;
        detailSwain3 = swainContents.transform.GetChild(2).gameObject;
        detailSwain4 = swainContents.transform.GetChild(3).gameObject;
        detailSwain5 = swainContents.transform.GetChild(4).gameObject;
        detailSwain6 = swainContents.transform.GetChild(5).gameObject;

        detailCane1 = caneContents.transform.GetChild(0).gameObject;
        detailCane2 = caneContents.transform.GetChild(1).gameObject;
        detailCane3 = caneContents.transform.GetChild(2).gameObject;
        detailCane4 = caneContents.transform.GetChild(3).gameObject;
        detailCane5 = caneContents.transform.GetChild(4).gameObject;
        detailCane6 = caneContents.transform.GetChild(5).gameObject;

        detailAlbert1 = albertContents.transform.GetChild(0).gameObject;
        detailAlbert2 = albertContents.transform.GetChild(1).gameObject;
        detailAlbert3 = albertContents.transform.GetChild(2).gameObject;
        detailAlbert4 = albertContents.transform.GetChild(3).gameObject;
        detailAlbert5 = albertContents.transform.GetChild(4).gameObject;
        detailAlbert6 = albertContents.transform.GetChild(5).gameObject;

        detailMeiv1 = meivContents.transform.GetChild(0).gameObject;
        detailMeiv2 = meivContents.transform.GetChild(1).gameObject;
        detailMeiv3 = meivContents.transform.GetChild(2).gameObject;
        detailMeiv4 = meivContents.transform.GetChild(3).gameObject;
        detailMeiv5 = meivContents.transform.GetChild(4).gameObject;
        detailMeiv6 = meivContents.transform.GetChild(5).gameObject;

        detailBlackWell1 = blackWellContents.transform.GetChild(0).gameObject;
        detailBlackWell2 = blackWellContents.transform.GetChild(1).gameObject;
        detailBlackWell3 = blackWellContents.transform.GetChild(2).gameObject;
        detailBlackWell4 = blackWellContents.transform.GetChild(3).gameObject;
        detailBlackWell5 = blackWellContents.transform.GetChild(4).gameObject;
        detailBlackWell6 = blackWellContents.transform.GetChild(5).gameObject;

        detailSewerWorker1 = sewerWorkerContents.transform.GetChild(0).gameObject;
        detailSewerWorker2 = sewerWorkerContents.transform.GetChild(1).gameObject;
        detailSewerWorker3 = sewerWorkerContents.transform.GetChild(2).gameObject;
        detailSewerWorker4 = sewerWorkerContents.transform.GetChild(3).gameObject;
        detailSewerWorker5 = sewerWorkerContents.transform.GetChild(4).gameObject;
        detailSewerWorker6 = sewerWorkerContents.transform.GetChild(5).gameObject;


        detailDave1 = daveContents.transform.GetChild(0).gameObject;
        detailDave2 = daveContents.transform.GetChild(1).gameObject;
        detailDave3 = daveContents.transform.GetChild(2).gameObject;
        detailDave4 = daveContents.transform.GetChild(3).gameObject;
        detailDave5 = daveContents.transform.GetChild(4).gameObject;
        detailDave6 = daveContents.transform.GetChild(5).gameObject;
    }
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

        if (nextButtonAiden.activeSelf == true || nextButtonAlbert.activeSelf == true || nextButtonBlackWell.activeSelf == true || nextButtonCane.activeSelf == true || nextButtonElla.activeSelf == true || nextButtonMeiv.activeSelf == true || nextButtonSewerWorker.activeSelf == true || nextButtonDave.activeSelf == true || nextButtonSwain.activeSelf == true)    {
            if (Input.GetKeyDown(KeyCode.D))
            {
                NextPage();
            }
        }

        if (prevButtonAiden.activeSelf == true || prevButtonAlbert.activeSelf == true || prevButtonBlackWell.activeSelf == true || prevButtonCane.activeSelf == true || prevButtonElla.activeSelf == true || prevButtonMeiv.activeSelf == true || prevButtonSewerWorker.activeSelf == true || nextButtonDave.activeSelf == true || prevButtonSwain.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                PrevPage();
            }
        }

        if (ellaDetail.activeSelf == true)
        {
            nowPage = "Ella";
        }
        else if (aidenDetail.activeSelf == true)
        {
            nowPage = "Aiden";
        }
        else if (swainDetail.activeSelf == true)
        {
            nowPage = "Swain";
        }
        else if (caneDetail.activeSelf == true)
        {
            nowPage = "Cane";
        }
        else if (albertDetail.activeSelf == true)
        {
            nowPage = "Albert";
        }
        else if (meivDetail.activeSelf == true)
        {
            nowPage = "Meiv";
        }
        else if (blackWellDetail.activeSelf == true)
        {
            nowPage = "BlackWell";
        }
        if (sewerWorkerDetail.activeSelf == true)
        {
            nowPage = "SewerWorker";
        }
        if (daveDetail.activeSelf == true)
        {
            nowPage = "Dave";
        }

    }
     string nowPage;

    //엘라
     GameObject detailElla1;
     GameObject detailElla2;
     GameObject detailElla3;
     GameObject detailElla4;
     GameObject detailElla5;
     GameObject detailElla6;
    bool onElla1;
    bool onElla2;
    bool onElla3;
    bool onElla4;
    bool onElla5;
    bool onElla6;
    int countElla = 0;
    public GameObject ellaContents;
    public GameObject ellaDetail;
    public static int ellaIntelInt;
    public GameObject nextButtonElla;
    public GameObject prevButtonElla;

    //에이든
     GameObject detailAiden1;
     GameObject detailAiden2;
     GameObject detailAiden3;
     GameObject detailAiden4;
     GameObject detailAiden5;
     GameObject detailAiden6;
    bool onAiden1;
    bool onAiden2;
    bool onAiden3;
    bool onAiden4;
    bool onAiden5;
    bool onAiden6;
     int countAiden = 0;
    public GameObject aidenContents;
    public GameObject aidenDetail;
    public static int aidenIntelInt;
    public GameObject nextButtonAiden;
    public GameObject prevButtonAiden;
    //

    //스와인
     GameObject detailSwain1;
     GameObject detailSwain2;
     GameObject detailSwain3;
     GameObject detailSwain4;
     GameObject detailSwain5;
     GameObject detailSwain6;
    bool onSwain1;
    bool onSwain2;
    bool onSwain3;
    bool onSwain4;
    bool onSwain5;
    bool onSwain6;
    int countSwain = 0;
    public GameObject swainContents;
    public GameObject swainDetail;
    public static int swainIntelInt;
    public GameObject nextButtonSwain;
    public GameObject prevButtonSwain;


    //케인
     GameObject detailCane1;
     GameObject detailCane2;
     GameObject detailCane3;
     GameObject detailCane4;
     GameObject detailCane5;
     GameObject detailCane6;
    bool onCane1;
    bool onCane2;
    bool onCane3;
    bool onCane4;
    bool onCane5;
    bool onCane6;
    int countCane = 0;
    public GameObject caneContents;
    public GameObject caneDetail;
    public static int caneIntelInt;
    public GameObject nextButtonCane;
    public GameObject prevButtonCane;

     GameObject detailAlbert1;
     GameObject detailAlbert2;
     GameObject detailAlbert3;
     GameObject detailAlbert4;
     GameObject detailAlbert5;
     GameObject detailAlbert6;
    bool onAlbert1;
    bool onAlbert2;
    bool onAlbert3;
    bool onAlbert4;
    bool onAlbert5;
    bool onAlbert6;
    int countAlbert = 0;
    public GameObject albertContents;
    public GameObject albertDetail;
    public static int albertIntelInt;
    public GameObject nextButtonAlbert;
    public GameObject prevButtonAlbert;

    GameObject detailMeiv1;
    GameObject detailMeiv2;
    GameObject detailMeiv3;
    GameObject detailMeiv4;
    GameObject detailMeiv5;
    GameObject detailMeiv6;
    bool onMeiv1;
    bool onMeiv2;
    bool onMeiv3;
    bool onMeiv4;
    bool onMeiv5;
    bool onMeiv6;
    int countMeiv = 0;
    public GameObject meivContents;
    public GameObject meivDetail;
    public static int meivIntelInt;
    public GameObject nextButtonMeiv;
    public GameObject prevButtonMeiv;

    GameObject detailBlackWell1;
    GameObject detailBlackWell2;
    GameObject detailBlackWell3;
    GameObject detailBlackWell4;
    GameObject detailBlackWell5;
    GameObject detailBlackWell6;
    bool onBlackWell1;
    bool onBlackWell2;
    bool onBlackWell3;
    bool onBlackWell4;
    bool onBlackWell5;
    bool onBlackWell6;
    int countBlackWell = 0;
    public GameObject blackWellContents;
    public GameObject blackWellDetail;
    public static int blackWellIntelInt;
    public GameObject nextButtonBlackWell;
    public GameObject prevButtonBlackWell;

    //하수도 노동자
    GameObject detailSewerWorker1;
    GameObject detailSewerWorker2;
    GameObject detailSewerWorker3;
    GameObject detailSewerWorker4;
    GameObject detailSewerWorker5;
    GameObject detailSewerWorker6;
    bool onSewerWorker1;
    bool onSewerWorker2;
    bool onSewerWorker3;
    bool onSewerWorker4;
    bool onSewerWorker5;
    bool onSewerWorker6;
    int countSewerWorker = 0;
    public GameObject sewerWorkerContents;
    public GameObject sewerWorkerDetail;
    public static int sewerWorkerIntelInt;
    public GameObject nextButtonSewerWorker;
    public GameObject prevButtonSewerWorker;

    //데이브
    public GameObject detailDave1;
    public GameObject detailDave2;
    public GameObject detailDave3;
    public GameObject detailDave4;
    public GameObject detailDave5;
    public GameObject detailDave6;
    bool onDave1;
    bool onDave2;
    bool onDave3;
    bool onDave4;
    bool onDave5;
    bool onDave6;
    public int countDave = 0;
    public GameObject daveContents;
    public GameObject daveDetail;
    public static int daveIntelInt;
    public GameObject nextButtonDave;
    public GameObject prevButtonDave;

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
        if (onAiden1 == false && DataBaseManager.intelAiden1 == true)
        {
            onAiden1 = true;
            detailAiden1.SetActive(true);
            detailAiden1.transform.SetAsLastSibling();
            countAiden += 1;
        }
        if (onAiden2 == false && DataBaseManager.intelAiden2 == true)
        {
            onAiden2 = true;
            detailAiden2.SetActive(true);
            detailAiden2.transform.SetAsLastSibling();
            countAiden += 1;
        }
        if (onAiden3 == false && DataBaseManager.intelAiden3 == true)
        {
            onAiden3 = true;
            detailAiden3.SetActive(true);
            detailAiden3.transform.SetAsLastSibling();
            countAiden += 1;
        }
        if (onAiden4 == false && DataBaseManager.intelAiden4 == true)
        {
            onAiden4 = true;
            detailAiden4.SetActive(true);
            detailAiden4.transform.SetAsLastSibling();
            countAiden += 1;
        }
        if (onAiden5 == false && DataBaseManager.intelAiden5 == true)
        {
            onAiden5 = true;
            detailAiden5.SetActive(true);
            detailAiden5.transform.SetAsLastSibling();
            countAiden += 1;
        }
        if (onAiden6 == false && DataBaseManager.intelAiden6 == true)
        {
            onAiden6 = true;
            detailAiden6.SetActive(true);
            detailAiden6.transform.SetAsLastSibling();
            countAiden += 1;
        }

        if (countAiden > 2 && DataBaseManager.nowPageAiden == 1)
        {
            nextButtonAiden.SetActive(true);
        }
        else if (countAiden > 4 && DataBaseManager.nowPageAiden == 2)
        {
            nextButtonAiden.SetActive(true);
        }
        else
        {
            nextButtonAiden.SetActive(false);
        }

        if (DataBaseManager.nowPageAiden == 2)
        {
            prevButtonAiden.SetActive(true);
        }
        else if (DataBaseManager.nowPageAiden == 3)
        {
            prevButtonAiden.SetActive(true);
        }
        else
        {
            prevButtonAiden.SetActive(false);
        }
    }
    void EllaDetail()
    {
        if (onElla1 == false && DataBaseManager.intelElla1 == true)
        {
            onElla1 = true;
            detailElla1.SetActive(true);
            detailElla1.transform.SetAsLastSibling();
            countElla += 1;
        }
        if (onElla2 == false && DataBaseManager.intelElla2 == true)
        {
            onElla2 = true;
            detailElla2.SetActive(true);
            detailElla2.transform.SetAsLastSibling();
            countElla += 1;
        }
        if (onElla3 == false && DataBaseManager.intelElla3 == true)
        {
            onElla3 = true;
            detailElla3.SetActive(true);
            detailElla3.transform.SetAsLastSibling();
            countElla += 1;
        }
        if (onElla4 == false && DataBaseManager.intelElla4 == true)
        {
            onElla4 = true;
            detailElla4.SetActive(true);
            detailElla4.transform.SetAsLastSibling();
            countElla += 1;
        }
        if (onElla5 == false && DataBaseManager.intelElla5 == true)
        {
            onElla5 = true;
            detailElla5.SetActive(true);
            detailElla5.transform.SetAsLastSibling();
            countElla += 1;
        }
        if (onElla6 == false && DataBaseManager.intelElla6 == true)
        {
            onElla6 = true;
            detailElla6.SetActive(true);
            detailElla6.transform.SetAsLastSibling();
            countElla += 1;
        }

        if (countElla > 2 && DataBaseManager.nowPageElla == 1)
        {
            nextButtonElla.SetActive(true);
        }
        else if (countElla > 4 && DataBaseManager.nowPageElla == 2)
        {
            nextButtonElla.SetActive(true);
        }
        else
        {
            nextButtonElla.SetActive(false);
        }

        if (DataBaseManager.nowPageElla == 2)
        {
            prevButtonElla.SetActive(true);
        }
        else if (DataBaseManager.nowPageElla == 3)
        {
            prevButtonElla.SetActive(true);
        }
        else
        {
            prevButtonElla.SetActive(false);
        }
    }
    void SwainDetail()
    {
        if (onSwain1 == false && DataBaseManager.intelSwain1 == true)
        {
            onSwain1 = true;
            detailSwain1.SetActive(true);
            detailSwain1.transform.SetAsLastSibling();
            countSwain += 1;
        }
        if (onSwain2 == false && DataBaseManager.intelSwain2 == true)
        {
            onSwain2 = true;
            detailSwain2.SetActive(true);
            detailSwain2.transform.SetAsLastSibling();
            countSwain += 1;
        }
        if (onSwain3 == false && DataBaseManager.intelSwain3 == true)
        {
            onSwain3 = true;
            detailSwain3.SetActive(true);
            detailSwain3.transform.SetAsLastSibling();
            countSwain += 1;
        }
        if (onSwain4 == false && DataBaseManager.intelSwain4 == true)
        {
            onSwain4 = true;
            detailSwain4.SetActive(true);
            detailSwain4.transform.SetAsLastSibling();
            countSwain += 1;
        }
        if (onSwain5 == false && DataBaseManager.intelSwain5 == true)
        {
            onSwain5 = true;
            detailSwain5.SetActive(true);
            detailSwain5.transform.SetAsLastSibling();
            countSwain += 1;
        }
        if (onSwain6 == false && DataBaseManager.intelSwain6 == true)
        {
            onSwain6 = true;
            detailSwain6.SetActive(true);
            detailSwain6.transform.SetAsLastSibling();
            countSwain += 1;
        }

        if (countSwain > 2 && DataBaseManager.nowPageSwain == 1)
        {
            nextButtonSwain.SetActive(true);
        }
        else if (countSwain > 4 && DataBaseManager.nowPageSwain == 2)
        {
            nextButtonSwain.SetActive(true);
        }
        else
        {
            nextButtonSwain.SetActive(false);
        }

        if (DataBaseManager.nowPageSwain == 2)
        {
            prevButtonSwain.SetActive(true);
        }
        else if (DataBaseManager.nowPageSwain == 3)
        {
            prevButtonSwain.SetActive(true);
        }
        else
        {
            prevButtonSwain.SetActive(false);
        }
    }
    void CaneDetail()
    {
        if (onCane1 == false && DataBaseManager.intelCane1 == true)
        {
            onCane1 = true;
            detailCane1.SetActive(true);
            detailCane1.transform.SetAsLastSibling();
            countCane += 1;
        }
        if (onCane2 == false && DataBaseManager.intelCane2 == true)
        {
            onCane2 = true;
            detailCane2.SetActive(true);
            detailCane2.transform.SetAsLastSibling();
            countCane += 1;
        }
        if (onCane3 == false && DataBaseManager.intelCane3 == true)
        {
            onCane3 = true;
            detailCane3.SetActive(true);
            detailCane3.transform.SetAsLastSibling();
            countCane += 1;
        }
        if (onCane4 == false && DataBaseManager.intelCane4 == true)
        {
            onCane4 = true;
            detailCane4.SetActive(true);
            detailCane4.transform.SetAsLastSibling();
            countCane += 1;
        }
        if (onCane5 == false && DataBaseManager.intelCane5 == true)
        {
            onCane5 = true;
            detailCane5.SetActive(true);
            detailCane5.transform.SetAsLastSibling();
            countCane += 1;
        }
        if (onCane6 == false && DataBaseManager.intelCane6 == true)
        {
            onCane6 = true;
            detailCane6.SetActive(true);
            detailCane6.transform.SetAsLastSibling();
            countCane += 1;
        }

        if (countCane > 2 && DataBaseManager.nowPageCane == 1)
        {
            nextButtonCane.SetActive(true);
        }
        else if (countCane > 4 && DataBaseManager.nowPageCane == 2)
        {
            nextButtonCane.SetActive(true);
        }
        else
        {
            nextButtonCane.SetActive(false);
        }

        if (DataBaseManager.nowPageCane == 2)
        {
            prevButtonCane.SetActive(true);
        }
        else if (DataBaseManager.nowPageCane == 3)
        {
            prevButtonCane.SetActive(true);
        }
        else
        {
            prevButtonCane.SetActive(false);
        }
    }
    void AlbertDetail()
    {
        if (onAlbert1 == false && DataBaseManager.intelAlbert1 == true)
        {
            onAlbert1 = true;
            detailAlbert1.SetActive(true);
            detailAlbert1.transform.SetAsLastSibling();
            countAlbert += 1;
        }
        if (onAlbert2 == false && DataBaseManager.intelAlbert2 == true)
        {
            onAlbert2 = true;
            detailAlbert2.SetActive(true);
            detailAlbert2.transform.SetAsLastSibling();
            countAlbert += 1;
        }
        if (onAlbert3 == false && DataBaseManager.intelAlbert3 == true)
        {
            onAlbert3 = true;
            detailAlbert3.SetActive(true);
            detailAlbert3.transform.SetAsLastSibling();
            countAlbert += 1;
        }
        if (onAlbert4 == false && DataBaseManager.intelAlbert4 == true)
        {
            onAlbert4 = true;
            detailAlbert4.SetActive(true);
            detailAlbert4.transform.SetAsLastSibling();
            countAlbert += 1;
        }
        if (onAlbert5 == false && DataBaseManager.intelAlbert5 == true)
        {
            onAlbert5 = true;
            detailAlbert5.SetActive(true);
            detailAlbert5.transform.SetAsLastSibling();
            countAlbert += 1;
        }
        if (onAlbert6 == false && DataBaseManager.intelAlbert6 == true)
        {
            onAlbert6 = true;
            detailAlbert6.SetActive(true);
            detailAlbert6.transform.SetAsLastSibling();
            countAlbert += 1;
        }

        if (countAlbert > 2 && DataBaseManager.nowPageAlbert == 1)
        {
            nextButtonAlbert.SetActive(true);
        }
        else if (countAlbert > 4 && DataBaseManager.nowPageAlbert == 2)
        {
            nextButtonAlbert.SetActive(true);
        }
        else
        {
            nextButtonAlbert.SetActive(false);
        }

        if (DataBaseManager.nowPageAlbert == 2)
        {
            prevButtonAlbert.SetActive(true);
        }
        else if (DataBaseManager.nowPageAlbert == 3)
        {
            prevButtonAlbert.SetActive(true);
        }
        else
        {
            prevButtonAlbert.SetActive(false);
        }
    }
    void MeivDetail()
    {
        if (onMeiv1 == false && DataBaseManager.intelMeiv1 == true)
        {
            onMeiv1 = true;
            detailMeiv1.SetActive(true);
            detailMeiv1.transform.SetAsLastSibling();
            countMeiv += 1;
        }
        if (onMeiv2 == false && DataBaseManager.intelMeiv2 == true)
        {
            onMeiv2 = true;
            detailMeiv2.SetActive(true);
            detailMeiv2.transform.SetAsLastSibling();
            countMeiv += 1;
        }
        if (onMeiv3 == false && DataBaseManager.intelMeiv3 == true)
        {
            onMeiv3 = true;
            detailMeiv3.SetActive(true);
            detailMeiv3.transform.SetAsLastSibling();
            countMeiv += 1;
        }
        if (onMeiv4 == false && DataBaseManager.intelMeiv4 == true)
        {
            onMeiv4 = true;
            detailMeiv4.SetActive(true);
            detailMeiv4.transform.SetAsLastSibling();
            countMeiv += 1;
        }
        if (onMeiv5 == false && DataBaseManager.intelMeiv5 == true)
        {
            onMeiv5 = true;
            detailMeiv5.SetActive(true);
            detailMeiv5.transform.SetAsLastSibling();
            countMeiv += 1;
        }
        if (onMeiv6 == false && DataBaseManager.intelMeiv6 == true)
        {
            onMeiv6 = true;
            detailMeiv6.SetActive(true);
            detailMeiv6.transform.SetAsLastSibling();
            countMeiv += 1;
        }

        if (countMeiv > 2 && DataBaseManager.nowPageMeiv == 1)
        {
            nextButtonMeiv.SetActive(true);
        }
        else if (countMeiv > 4 && DataBaseManager.nowPageMeiv == 2)
        {
            nextButtonMeiv.SetActive(true);
        }
        else
        {
            nextButtonMeiv.SetActive(false);
        }

        if (DataBaseManager.nowPageMeiv == 2)
        {
            prevButtonMeiv.SetActive(true);
        }
        else if (DataBaseManager.nowPageMeiv == 3)
        {
            prevButtonMeiv.SetActive(true);
        }
        else
        {
            prevButtonMeiv.SetActive(false);
        }
    }
    void BlackWellDetail()
    {
        if (onBlackWell1 == false && DataBaseManager.intelBlackWell1 == true)
        {
            onBlackWell1 = true;
            detailBlackWell1.SetActive(true);
            detailBlackWell1.transform.SetAsLastSibling();
            countBlackWell += 1;
        }
        if (onBlackWell2 == false && DataBaseManager.intelBlackWell2 == true)
        {
            onBlackWell2 = true;
            detailBlackWell2.SetActive(true);
            detailBlackWell2.transform.SetAsLastSibling();
            countBlackWell += 1;
        }
        if (onBlackWell3 == false && DataBaseManager.intelBlackWell3 == true)
        {
            onBlackWell3 = true;
            detailBlackWell3.SetActive(true);
            detailBlackWell3.transform.SetAsLastSibling();
            countBlackWell += 1;
        }
        if (onBlackWell4 == false && DataBaseManager.intelBlackWell4 == true)
        {
            onBlackWell4 = true;
            detailBlackWell4.SetActive(true);
            detailBlackWell4.transform.SetAsLastSibling();
            countBlackWell += 1;
        }
        if (onBlackWell5 == false && DataBaseManager.intelBlackWell5 == true)
        {
            onBlackWell5 = true;
            detailBlackWell5.SetActive(true);
            detailBlackWell5.transform.SetAsLastSibling();
            countBlackWell += 1;
        }
        if (onBlackWell6 == false && DataBaseManager.intelBlackWell6 == true)
        {
            onBlackWell6 = true;
            detailBlackWell6.SetActive(true);
            detailBlackWell6.transform.SetAsLastSibling();
            countBlackWell += 1;
        }

        if (countBlackWell > 2 && DataBaseManager.nowPageBlackWell == 1)
        {
            nextButtonBlackWell.SetActive(true);
        }
        else if (countBlackWell > 4 && DataBaseManager.nowPageBlackWell == 2)
        {
            nextButtonBlackWell.SetActive(true);
        }
        else
        {
            nextButtonBlackWell.SetActive(false);
        }

        if (DataBaseManager.nowPageBlackWell == 2)
        {
            prevButtonBlackWell.SetActive(true);
        }
        else if (DataBaseManager.nowPageBlackWell == 3)
        {
            prevButtonBlackWell.SetActive(true);
        }
        else
        {
            prevButtonBlackWell.SetActive(false);
        }
    }
    void SewerWorkerDetail()
    {
        if (onSewerWorker1 == false && DataBaseManager.intelSewerWorker1 == true)
        {
            onSewerWorker1 = true;
            detailSewerWorker1.SetActive(true);
            detailSewerWorker1.transform.SetAsLastSibling();
            countSewerWorker += 1;
        }
        if (onSewerWorker2 == false && DataBaseManager.intelSewerWorker2 == true)
        {
            onSewerWorker2 = true;
            detailSewerWorker2.SetActive(true);
            detailSewerWorker2.transform.SetAsLastSibling();
            countSewerWorker += 1;
        }
        if (onSewerWorker3 == false && DataBaseManager.intelSewerWorker3 == true)
        {
            onSewerWorker3 = true;
            detailSewerWorker3.SetActive(true);
            detailSewerWorker3.transform.SetAsLastSibling();
            countSewerWorker += 1;
        }
        if (onSewerWorker4 == false && DataBaseManager.intelSewerWorker4 == true)
        {
            onSewerWorker4 = true;
            detailSewerWorker4.SetActive(true);
            detailSewerWorker4.transform.SetAsLastSibling();
            countSewerWorker += 1;
        }
        if (onSewerWorker5 == false && DataBaseManager.intelSewerWorker5 == true)
        {
            onSewerWorker5 = true;
            detailSewerWorker5.SetActive(true);
            detailSewerWorker5.transform.SetAsLastSibling();
            countSewerWorker += 1;
        }
        if (onSewerWorker6 == false && DataBaseManager.intelSewerWorker6 == true)
        {
            onSewerWorker6 = true;
            detailSewerWorker6.SetActive(true);
            detailSewerWorker6.transform.SetAsLastSibling();
            countSewerWorker += 1;
        }

        if (countSewerWorker > 2 && DataBaseManager.nowPageSewerWorker == 1)
        {
            nextButtonSewerWorker.SetActive(true);
        }
        else if (countSewerWorker > 4 && DataBaseManager.nowPageSewerWorker == 2)
        {
            nextButtonSewerWorker.SetActive(true);
        }
        else
        {
            nextButtonSewerWorker.SetActive(false);
        }

        if (DataBaseManager.nowPageSewerWorker == 2)
        {
            prevButtonSewerWorker.SetActive(true);
        }
        else if (DataBaseManager.nowPageSewerWorker == 3)
        {
            prevButtonSewerWorker.SetActive(true);
        }
        else
        {
            prevButtonSewerWorker.SetActive(false);
        }
    }

    void DaveDetail()
    {
        if (onDave1 == false && DataBaseManager.intelDave1 == true)
        {
            onDave1 = true;
            detailDave1.SetActive(true);
            detailDave1.transform.SetAsLastSibling();
            countDave += 1;
        }
        if (onDave2 == false && DataBaseManager.intelDave2 == true)
        {
            onDave2 = true;
            detailDave2.SetActive(true);
            detailDave2.transform.SetAsLastSibling();
            countDave += 1;
        }
        if (onDave3 == false && DataBaseManager.intelDave3 == true)
        {
            onDave3 = true;
            detailDave3.SetActive(true);
            detailDave3.transform.SetAsLastSibling();
            countDave += 1;
        }
        if (onDave4 == false && DataBaseManager.intelDave4 == true)
        {
            onDave4 = true;
            detailDave4.SetActive(true);
            detailDave4.transform.SetAsLastSibling();
            countDave += 1;
        }
        if (onDave5 == false && DataBaseManager.intelDave5 == true)
        {
            onDave5 = true;
            detailDave5.SetActive(true);
            detailDave5.transform.SetAsLastSibling();
            countDave += 1;
        }
        if (onDave6 == false && DataBaseManager.intelDave6 == true)
        {
            onDave6 = true;
            detailDave6.SetActive(true);
            detailDave6.transform.SetAsLastSibling();
            countDave += 1;
        }

        if (countDave > 2 && DataBaseManager.nowPageDave == 1)
        {
            nextButtonDave.SetActive(true);
        }
        else if (countDave > 4 && DataBaseManager.nowPageDave == 2)
        {
            nextButtonDave.SetActive(true);
        }
        else
        {
            nextButtonDave.SetActive(false);
        }

        if (DataBaseManager.nowPageDave == 2)
        {
            prevButtonDave.SetActive(true);
        }
        else if (DataBaseManager.nowPageDave == 3)
        {
            prevButtonDave.SetActive(true);
        }
        else
        {
            prevButtonDave.SetActive(false);
        }
    }
    public void NextPage()
    {
        if(nowPage == "Ella")
        {
            if(DataBaseManager.nowPageElla == 1)
            {
                if (countElla >= 6)
                {
                    Reset_Ella();
                    ellaContents.transform.GetChild(2).gameObject.SetActive(true);
                    ellaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countElla >= 5)
                {
                    Reset_Ella();
                    ellaContents.transform.GetChild(3).gameObject.SetActive(true);
                    ellaContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countElla >= 4)
                {
                    Reset_Ella();
                    ellaContents.transform.GetChild(4).gameObject.SetActive(true);
                    ellaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countElla >= 3)
                {
                    Reset_Ella();
                    ellaContents.transform.GetChild(5).gameObject.SetActive(true);   
                }
                DataBaseManager.nowPageElla = 2;
            }
            else if (DataBaseManager.nowPageElla == 2)
            {
                if (countElla >= 6)
                {
                    Reset_Ella();
                    ellaContents.transform.GetChild(4).gameObject.SetActive(true);
                    ellaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countElla >= 5)
                {
                    Reset_Ella();
                    ellaContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageElla = 3;
            }
        }
        if (nowPage == "Aiden")
        {
            if (DataBaseManager.nowPageAiden == 1)
            {
                if (countAiden >= 6)
                {
                    Aiden_Ella();
                    aidenContents.transform.GetChild(2).gameObject.SetActive(true);
                    aidenContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countAiden >= 5)
                {
                    Aiden_Ella();
                    aidenContents.transform.GetChild(3).gameObject.SetActive(true);
                    aidenContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countAiden >= 4)
                {
                    Aiden_Ella();
                    aidenContents.transform.GetChild(4).gameObject.SetActive(true);
                    aidenContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countAiden >= 3)
                {
                    Aiden_Ella();
                    aidenContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageAiden = 2;
            }
            else if (DataBaseManager.nowPageAiden == 2)
            {
                if (countAiden >= 6)
                {
                    Aiden_Ella();
                    aidenContents.transform.GetChild(4).gameObject.SetActive(true);
                    aidenContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countAiden >= 5)
                {
                    Aiden_Ella();
                    aidenContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageAiden = 3;
            }
        }
        if (nowPage == "Swain")
        {
            if (DataBaseManager.nowPageSwain == 1)
            {
                if (countSwain >= 6)
                {
                    Reset_Swain();
                    swainContents.transform.GetChild(2).gameObject.SetActive(true);
                    swainContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSwain >= 5)
                {
                    Reset_Swain();
                    swainContents.transform.GetChild(3).gameObject.SetActive(true);
                    swainContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countSwain >= 4)
                {
                    Reset_Swain();
                    swainContents.transform.GetChild(4).gameObject.SetActive(true);
                    swainContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSwain >= 3)
                {
                    Reset_Swain();
                    swainContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageSwain = 2;
            }
            else if (DataBaseManager.nowPageSwain == 2)
            {
                if (countSwain >= 6)
                {
                    Reset_Swain();
                    swainContents.transform.GetChild(4).gameObject.SetActive(true);
                    swainContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSwain >= 5)
                {
                    Reset_Swain();
                    swainContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageSwain = 3;
            }
        }
        if (nowPage == "Cane")
        {
            if (DataBaseManager.nowPageCane == 1)
            {
                if (countCane >= 6)
                {
                    Reset_Cane();
                    caneContents.transform.GetChild(2).gameObject.SetActive(true);
                    caneContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countCane >= 5)
                {
                    Reset_Cane();
                    caneContents.transform.GetChild(3).gameObject.SetActive(true);
                    caneContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countCane >= 4)
                {
                    Reset_Cane();
                    caneContents.transform.GetChild(4).gameObject.SetActive(true);
                    caneContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countCane >= 3)
                {
                    Reset_Cane();
                    caneContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageCane = 2;
            }
            else if (DataBaseManager.nowPageCane == 2)
            {
                if (countCane >= 6)
                {
                    Reset_Cane();
                    caneContents.transform.GetChild(4).gameObject.SetActive(true);
                    caneContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countCane >= 5)
                {
                    Reset_Cane();
                    caneContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageCane = 3;
            }
        }
        if (nowPage == "Albert")
        {
            if (DataBaseManager.nowPageAlbert == 1)
            {
                if (countAlbert >= 6)
                {
                    Reset_Albert();
                    albertContents.transform.GetChild(2).gameObject.SetActive(true);
                    albertContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countAlbert >= 5)
                {
                    Reset_Albert();
                    albertContents.transform.GetChild(3).gameObject.SetActive(true);
                    albertContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countAlbert >= 4)
                {
                    Reset_Albert();
                    albertContents.transform.GetChild(4).gameObject.SetActive(true);
                    albertContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countAlbert >= 3)
                {
                    Reset_Albert();
                    albertContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageAlbert = 2;
            }
            else if (DataBaseManager.nowPageAlbert == 2)
            {
                if (countAlbert >= 6)
                {
                    Reset_Albert();
                    albertContents.transform.GetChild(4).gameObject.SetActive(true);
                    albertContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countAlbert >= 5)
                {
                    Reset_Albert();
                    albertContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageAlbert = 3;
            }
        }
        if (nowPage == "Meiv")
        {
            if (DataBaseManager.nowPageMeiv == 1)
            {
                if (countMeiv >= 6)
                {
                    Reset_Meiv();
                    meivContents.transform.GetChild(2).gameObject.SetActive(true);
                    meivContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countMeiv >= 5)
                {
                    Reset_Meiv();
                    meivContents.transform.GetChild(3).gameObject.SetActive(true);
                    meivContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countMeiv >= 4)
                {
                    Reset_Meiv();
                    meivContents.transform.GetChild(4).gameObject.SetActive(true);
                    meivContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countMeiv >= 3)
                {
                    Reset_Meiv();
                    meivContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageMeiv = 2;
            }
            else if (DataBaseManager.nowPageMeiv == 2)
            {
                if (countMeiv >= 6)
                {
                    Reset_Meiv();
                    meivContents.transform.GetChild(4).gameObject.SetActive(true);
                    meivContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countMeiv >= 5)
                {
                    Reset_Meiv();
                    meivContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageMeiv = 3;
            }
        }
        if (nowPage == "BlackWell")
        {
            if (DataBaseManager.nowPageBlackWell == 1)
            {
                if (countBlackWell >= 6)
                {
                    Reset_BlackWell();
                    blackWellContents.transform.GetChild(2).gameObject.SetActive(true);
                    blackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countBlackWell >= 5)
                {
                    Reset_BlackWell();
                    blackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                    blackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countBlackWell >= 4)
                {
                    Reset_BlackWell();
                    blackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                    blackWellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countBlackWell >= 3)
                {
                    Reset_BlackWell();
                    blackWellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageBlackWell = 2;
            }
            else if (DataBaseManager.nowPageBlackWell == 2)
            {
                if (countBlackWell >= 6)
                {
                    Reset_BlackWell();
                    blackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                    blackWellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countBlackWell >= 5)
                {
                    Reset_BlackWell();
                    blackWellContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageBlackWell = 3;
            }
        }
        if (nowPage == "SewerWorker")
        {
            if (DataBaseManager.nowPageSewerWorker == 1)
            {
                if (countSewerWorker >= 6)
                {
                    Reset_SewerWorker();
                    sewerWorkerContents.transform.GetChild(2).gameObject.SetActive(true);
                    sewerWorkerContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSewerWorker >= 5)
                {
                    Reset_SewerWorker();
                    sewerWorkerContents.transform.GetChild(3).gameObject.SetActive(true);
                    sewerWorkerContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countSewerWorker >= 4)
                {
                    Reset_SewerWorker();
                    sewerWorkerContents.transform.GetChild(4).gameObject.SetActive(true);
                    sewerWorkerContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSewerWorker >= 3)
                {
                    Reset_SewerWorker();
                    sewerWorkerContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageSewerWorker = 2;
            }
            else if (DataBaseManager.nowPageSewerWorker == 2)
            {
                if (countSewerWorker >= 6)
                {
                    Reset_SewerWorker();
                    sewerWorkerContents.transform.GetChild(4).gameObject.SetActive(true);
                    sewerWorkerContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countSewerWorker >= 5)
                {
                    Reset_SewerWorker();
                    sewerWorkerContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageSewerWorker = 3;
            }
        }
        if (nowPage == "Dave")
        {
            if (DataBaseManager.nowPageDave == 1)
            {
                if (countDave >= 6)
                {
                    Reset_Dave();
                    daveContents.transform.GetChild(2).gameObject.SetActive(true);
                    daveContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countDave >= 5)
                {
                    Reset_Dave();
                    daveContents.transform.GetChild(3).gameObject.SetActive(true);
                    daveContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countDave >= 4)
                {
                    Reset_Dave();
                    daveContents.transform.GetChild(4).gameObject.SetActive(true);
                    daveContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countDave >= 3)
                {
                    Reset_Dave();
                    daveContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageDave = 2;
            }
            else if (DataBaseManager.nowPageDave == 2)
            {
                if (countDave >= 6)
                {
                    Reset_Dave();
                    daveContents.transform.GetChild(4).gameObject.SetActive(true);
                    daveContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countDave >= 5)
                {
                    Reset_Dave();
                    daveContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageDave = 3;
            }
        }
    }

    public void PrevPage()
    {
        if (nowPage == "Ella")
        {
            if (DataBaseManager.nowPageElla == 2)
            {
                if (countElla >= 6)
                {
                    Reset_Ella();
                    ellaContents.transform.GetChild(0).gameObject.SetActive(true);
                    ellaContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countElla >= 5)
                {
                    Reset_Ella();
                    ellaContents.transform.GetChild(1).gameObject.SetActive(true);
                    ellaContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countElla >= 4)
                {
                    Reset_Ella();
                    ellaContents.transform.GetChild(2).gameObject.SetActive(true);
                    ellaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countElla >= 3)
                {
                    Reset_Ella();
                    ellaContents.transform.GetChild(3).gameObject.SetActive(true);
                    ellaContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.nowPageElla = 1;
            }
            else if (DataBaseManager.nowPageElla == 3)
            {
                if (countElla >= 6)
                {
                    Reset_Ella();
                    ellaContents.transform.GetChild(2).gameObject.SetActive(true);
                    ellaContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countElla >= 5)
                {
                    Reset_Ella();
                    ellaContents.transform.GetChild(3).gameObject.SetActive(true);
                    ellaContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageElla = 2;
            }
        }
        if (nowPage == "Aiden")
        {
            if (DataBaseManager.nowPageAiden == 2)
            {
                if (countAiden >= 6)
                {
                    Aiden_Ella();
                    aidenContents.transform.GetChild(0).gameObject.SetActive(true);
                    aidenContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countAiden >= 5)
                {
                    Aiden_Ella();
                    aidenContents.transform.GetChild(1).gameObject.SetActive(true);
                    aidenContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countAiden >= 4)
                {
                    Aiden_Ella();
                    aidenContents.transform.GetChild(2).gameObject.SetActive(true);
                    aidenContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countAiden >= 3)
                {
                    Aiden_Ella();
                    aidenContents.transform.GetChild(3).gameObject.SetActive(true);
                    aidenContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.nowPageAiden = 1;
            }
            else if (DataBaseManager.nowPageAiden == 3)
            {
                if (countAiden >= 6)
                {
                    Aiden_Ella();
                    aidenContents.transform.GetChild(2).gameObject.SetActive(true);
                    aidenContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countAiden >= 5)
                {
                    Aiden_Ella();
                    aidenContents.transform.GetChild(3).gameObject.SetActive(true);
                    aidenContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageAiden = 2;
            }
        }
        if (nowPage == "Swain")
        {
            if (DataBaseManager.nowPageSwain == 2)
            {
                if (countSwain >= 6)
                {
                    Reset_Swain();
                    swainContents.transform.GetChild(0).gameObject.SetActive(true);
                    swainContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countSwain >= 5)
                {
                    Reset_Swain();
                    swainContents.transform.GetChild(1).gameObject.SetActive(true);
                    swainContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countSwain >= 4)
                {
                    Reset_Swain();
                    swainContents.transform.GetChild(2).gameObject.SetActive(true);
                    swainContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSwain >= 3)
                {
                    Reset_Swain();
                    swainContents.transform.GetChild(3).gameObject.SetActive(true);
                    swainContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.nowPageSwain = 1;
            }
            else if (DataBaseManager.nowPageSwain == 3)
            {
                if (countSwain >= 6)
                {
                    Reset_Swain();
                    swainContents.transform.GetChild(2).gameObject.SetActive(true);
                    swainContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSwain >= 5)
                {
                    Reset_Swain();
                    swainContents.transform.GetChild(3).gameObject.SetActive(true);
                    swainContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageSwain = 2;
            }
        }
        if (nowPage == "Cane")
        {
            if (DataBaseManager.nowPageCane == 2)
            {
                if (countCane >= 6)
                {
                    Reset_Cane();
                    caneContents.transform.GetChild(0).gameObject.SetActive(true);
                    caneContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countCane >= 5)
                {
                    Reset_Cane();
                    caneContents.transform.GetChild(1).gameObject.SetActive(true);
                    caneContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countCane >= 4)
                {
                    Reset_Cane();
                    caneContents.transform.GetChild(2).gameObject.SetActive(true);
                    caneContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countCane >= 3)
                {
                    Reset_Cane();
                    caneContents.transform.GetChild(3).gameObject.SetActive(true);
                    caneContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.nowPageCane = 1;
            }
            else if (DataBaseManager.nowPageCane == 3)
            {
                if (countCane >= 6)
                {
                    Reset_Cane();
                    caneContents.transform.GetChild(2).gameObject.SetActive(true);
                    caneContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countCane >= 5)
                {
                    Reset_Cane();
                    caneContents.transform.GetChild(3).gameObject.SetActive(true);
                    caneContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageCane = 2;
            }
        }
        if (nowPage == "Albert")
        {
            if (DataBaseManager.nowPageAlbert == 2)
            {
                if (countAlbert >= 6)
                {
                    Reset_Albert();
                    albertContents.transform.GetChild(0).gameObject.SetActive(true);
                    albertContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countAlbert >= 5)
                {
                    Reset_Albert();
                    albertContents.transform.GetChild(1).gameObject.SetActive(true);
                    albertContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countAlbert >= 4)
                {
                    Reset_Albert();
                    albertContents.transform.GetChild(2).gameObject.SetActive(true);
                    albertContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countAlbert >= 3)
                {
                    Reset_Albert();
                    albertContents.transform.GetChild(3).gameObject.SetActive(true);
                    albertContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.nowPageAlbert = 1;
            }
            else if (DataBaseManager.nowPageAlbert == 3)
            {
                if (countAlbert >= 6)
                {
                    Reset_Albert();
                    albertContents.transform.GetChild(2).gameObject.SetActive(true);
                    albertContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countAlbert >= 5)
                {
                    Reset_Albert();
                    albertContents.transform.GetChild(3).gameObject.SetActive(true);
                    albertContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageAlbert = 2;
            }
        }
        if (nowPage == "Meiv")
        {
            if (DataBaseManager.nowPageMeiv == 2)
            {
                if (countMeiv >= 6)
                {
                    Reset_Meiv();
                    meivContents.transform.GetChild(0).gameObject.SetActive(true);
                    meivContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countMeiv >= 5)
                {
                    Reset_Meiv();
                    meivContents.transform.GetChild(1).gameObject.SetActive(true);
                    meivContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countMeiv >= 4)
                {
                    Reset_Meiv();
                    meivContents.transform.GetChild(2).gameObject.SetActive(true);
                    meivContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countMeiv >= 3)
                {
                    Reset_Meiv();
                    meivContents.transform.GetChild(3).gameObject.SetActive(true);
                    meivContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.nowPageMeiv = 1;
            }
            else if (DataBaseManager.nowPageMeiv == 3)
            {
                if (countMeiv >= 6)
                {
                    Reset_Meiv();
                    meivContents.transform.GetChild(2).gameObject.SetActive(true);
                    meivContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countMeiv >= 5)
                {
                    Reset_Meiv();
                    meivContents.transform.GetChild(3).gameObject.SetActive(true);
                    meivContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageMeiv = 2;
            }
        }
        if (nowPage == "BlackWell")
        {
            if (DataBaseManager.nowPageBlackWell == 2)
            {
                if (countBlackWell >= 6)
                {
                    Reset_BlackWell();
                    blackWellContents.transform.GetChild(0).gameObject.SetActive(true);
                    blackWellContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countBlackWell >= 5)
                {
                    Reset_BlackWell();
                    blackWellContents.transform.GetChild(1).gameObject.SetActive(true);
                    blackWellContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countBlackWell >= 4)
                {
                    Reset_BlackWell();
                    blackWellContents.transform.GetChild(2).gameObject.SetActive(true);
                    blackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countBlackWell >= 3)
                {
                    Reset_BlackWell();
                    blackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                    blackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.nowPageBlackWell = 1;
            }
            else if (DataBaseManager.nowPageBlackWell == 3)
            {
                if (countBlackWell >= 6)
                {
                    Reset_BlackWell();
                    blackWellContents.transform.GetChild(2).gameObject.SetActive(true);
                    blackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countBlackWell >= 5)
                {
                    Reset_BlackWell();
                    blackWellContents.transform.GetChild(3).gameObject.SetActive(true);
                    blackWellContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageBlackWell = 2;
            }
        }
        if (nowPage == "SewerWorker")
        {
            if (DataBaseManager.nowPageSewerWorker == 2)
            {
                if (countSewerWorker >= 6)
                {
                    Reset_SewerWorker();
                    sewerWorkerContents.transform.GetChild(0).gameObject.SetActive(true);
                    sewerWorkerContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countSewerWorker >= 5)
                {
                    Reset_SewerWorker();
                    sewerWorkerContents.transform.GetChild(1).gameObject.SetActive(true);
                    sewerWorkerContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countSewerWorker >= 4)
                {
                    Reset_SewerWorker();
                    sewerWorkerContents.transform.GetChild(2).gameObject.SetActive(true);
                    sewerWorkerContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSewerWorker >= 3)
                {
                    Reset_SewerWorker();
                    sewerWorkerContents.transform.GetChild(3).gameObject.SetActive(true);
                    sewerWorkerContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.nowPageSewerWorker = 1;
            }
            else if (DataBaseManager.nowPageSewerWorker == 3)
            {
                if (countSewerWorker >= 6)
                {
                    Reset_SewerWorker();
                    sewerWorkerContents.transform.GetChild(2).gameObject.SetActive(true);
                    sewerWorkerContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countSewerWorker >= 5)
                {
                    Reset_SewerWorker();
                    sewerWorkerContents.transform.GetChild(3).gameObject.SetActive(true);
                    sewerWorkerContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageSewerWorker = 2;
            }
        }
        if (nowPage == "Dave")
        {
            if (DataBaseManager.nowPageDave == 2)
            {
                if (countDave >= 6)
                {
                    Reset_Dave();
                    daveContents.transform.GetChild(0).gameObject.SetActive(true);
                    daveContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countDave >= 5)
                {
                    Reset_Dave();
                    daveContents.transform.GetChild(1).gameObject.SetActive(true);
                    daveContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countDave >= 4)
                {
                    Reset_Dave();
                    daveContents.transform.GetChild(2).gameObject.SetActive(true);
                    daveContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countDave >= 3)
                {
                    Reset_Dave();
                    daveContents.transform.GetChild(3).gameObject.SetActive(true);
                    daveContents.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.nowPageDave = 1;
            }
            else if (DataBaseManager.nowPageDave == 3)
            {
                if (countDave >= 6)
                {
                    Reset_Dave();
                    daveContents.transform.GetChild(2).gameObject.SetActive(true);
                    daveContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countDave >= 5)
                {
                    Reset_Dave();
                    daveContents.transform.GetChild(3).gameObject.SetActive(true);
                    daveContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageDave = 2;
            }
        }
    }
    void Reset_Ella()
    {
        ellaContents.transform.GetChild(0).gameObject.SetActive(false);
        ellaContents.transform.GetChild(1).gameObject.SetActive(false);
        ellaContents.transform.GetChild(2).gameObject.SetActive(false);
        ellaContents.transform.GetChild(3).gameObject.SetActive(false);
        ellaContents.transform.GetChild(4).gameObject.SetActive(false);
        ellaContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Aiden_Ella()
    {
        aidenContents.transform.GetChild(0).gameObject.SetActive(false);
        aidenContents.transform.GetChild(1).gameObject.SetActive(false);
        aidenContents.transform.GetChild(2).gameObject.SetActive(false);
        aidenContents.transform.GetChild(3).gameObject.SetActive(false);
        aidenContents.transform.GetChild(4).gameObject.SetActive(false);
        aidenContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Swain()
    {
        swainContents.transform.GetChild(0).gameObject.SetActive(false);
        swainContents.transform.GetChild(1).gameObject.SetActive(false);
        swainContents.transform.GetChild(2).gameObject.SetActive(false);
        swainContents.transform.GetChild(3).gameObject.SetActive(false);
        swainContents.transform.GetChild(4).gameObject.SetActive(false);
        swainContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Cane()
    {
        caneContents.transform.GetChild(0).gameObject.SetActive(false);
        caneContents.transform.GetChild(1).gameObject.SetActive(false);
        caneContents.transform.GetChild(2).gameObject.SetActive(false);
        caneContents.transform.GetChild(3).gameObject.SetActive(false);
        caneContents.transform.GetChild(4).gameObject.SetActive(false);
        caneContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Albert()
    {
        albertContents.transform.GetChild(0).gameObject.SetActive(false);
        albertContents.transform.GetChild(1).gameObject.SetActive(false);
        albertContents.transform.GetChild(2).gameObject.SetActive(false);
        albertContents.transform.GetChild(3).gameObject.SetActive(false);
        albertContents.transform.GetChild(4).gameObject.SetActive(false);
        albertContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_Meiv()
    {
        meivContents.transform.GetChild(0).gameObject.SetActive(false);
        meivContents.transform.GetChild(1).gameObject.SetActive(false);
        meivContents.transform.GetChild(2).gameObject.SetActive(false);
        meivContents.transform.GetChild(3).gameObject.SetActive(false);
        meivContents.transform.GetChild(4).gameObject.SetActive(false);
        meivContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    void Reset_BlackWell()
    {
        blackWellContents.transform.GetChild(0).gameObject.SetActive(false);
        blackWellContents.transform.GetChild(1).gameObject.SetActive(false);
        blackWellContents.transform.GetChild(2).gameObject.SetActive(false);
        blackWellContents.transform.GetChild(3).gameObject.SetActive(false);
        blackWellContents.transform.GetChild(4).gameObject.SetActive(false);
        blackWellContents.transform.GetChild(5).gameObject.SetActive(false);
    }

    void Reset_SewerWorker()
    {
        sewerWorkerContents.transform.GetChild(0).gameObject.SetActive(false);
        sewerWorkerContents.transform.GetChild(1).gameObject.SetActive(false);
        sewerWorkerContents.transform.GetChild(2).gameObject.SetActive(false);
        sewerWorkerContents.transform.GetChild(3).gameObject.SetActive(false);
        sewerWorkerContents.transform.GetChild(4).gameObject.SetActive(false);
        sewerWorkerContents.transform.GetChild(5).gameObject.SetActive(false);
    }

    void Reset_Dave()
    {
        daveContents.transform.GetChild(0).gameObject.SetActive(false);
        daveContents.transform.GetChild(1).gameObject.SetActive(false);
        daveContents.transform.GetChild(2).gameObject.SetActive(false);
        daveContents.transform.GetChild(3).gameObject.SetActive(false);
        daveContents.transform.GetChild(4).gameObject.SetActive(false);
        daveContents.transform.GetChild(5).gameObject.SetActive(false);
    }
    public void OpenElla()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        ellaDetail.SetActive(true);
    }
    public void OpenSwain()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        swainDetail.SetActive(true);
    }
    public void OpenAiden()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        aidenDetail.SetActive(true);
    }
    public void OpenKane()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        caneDetail.SetActive(true);
    }
    public void OpenAlbert()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        albertDetail.SetActive(true);
    }
    public void OpenMeiv()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        meivDetail.SetActive(true);
    }
    public void OpenBlackWell()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        blackWellDetail.SetActive(true);
    }

    public void OpenSewerWorker()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        sewerWorkerDetail.SetActive(true);
    }

    public void OpenDave()
    {
        SoundManager.Instance.ClickSound_Play();
        CloseAllContents();
        daveDetail.SetActive(true);
    }
    void CloseAllContents()
    {
        ellaDetail.SetActive(false);
        aidenDetail.SetActive(false);
        caneDetail.SetActive(false);
        swainDetail.SetActive(false);
        albertDetail.SetActive(false);
        meivDetail.SetActive(false);
        blackWellDetail.SetActive(false);
        sewerWorkerDetail.SetActive(false);
        daveDetail.SetActive(false);
    }

}
