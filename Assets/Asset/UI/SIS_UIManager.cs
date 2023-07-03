using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SIS_UIManager : MonoBehaviour
{
    public GameObject SIS_UI;

    public Text str_txt;
    public Text intl_txt;
    public Text dex_txt;
    public Text hp_txt;
    public Text mp_txt;
    public Text san_txt;
    public Text luk_txt;
    public Text weal_txt;

    public Image str_image;
    public Image intl_image;
    public Image dex_image;
    public Image hp_image;
    public Image mp_image;
    public Image san_image;
    public Image luk_image;
    public Image weal_image;


    //skill - str
    public Text martialArtsPoint_t;
    public Text gunShotPoint_t;
    public Text swordPoint_t;
    public Text ObservationPoint_t;
    public Text swimingPoint_t;

    public Image martialArtsPoint_g;
    public Image gunShotPoint_g;
    public Image swordPoint_g;
    public Image ObservationPoint_g;
    public Image swimingPoint_g;

    //skill - int
    public Text medicine_t;
    public Text analysis_t;
    public Text listening_t;
    public Text psychotherapy_t;
    public Text occult_t;

    public Image medicine_g;
    public Image analysis_g;
    public Image listening_g;
    public Image psychotherapy_g;
    public Image occult_g;

    //skill - dex
    public Text evasion_t;
    public Text deftness_t;
    public Text rhetoric_t;
    public Text stealth_t;
    public Text Disguise_t;

    public Image evasion_g;
    public Image deftness_g;
    public Image rhetoric_g;
    public Image stealth_g;
    public Image Disguise_g;

    private static SIS_UIManager instance = null;
    public static SIS_UIManager Instance
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

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OpenMap()
    {
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false)
        {
            DataBaseManager.workSound = false;
            DataBaseManager.isOpenUi = true;
            SIS_UI.SetActive(true);
        }

    }
    public void CloseMap()
    {
        DataBaseManager.isOpenUi = false;
        SIS_UI.SetActive(false);
    }
    private void Update()
    {
        ManageItem();


        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isDirecting == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (SIS_UI.activeSelf == false)
                {
                    Okay2CheckUI();
                    DataBaseManager.workSound = false;
                    OpenMap();
                }
                else if (SIS_UI.activeSelf == true)
                {
                    CloseMap();
                }

            }
            if (Input.GetKeyDown(KeyCode.Escape) && SIS_UI.activeSelf == true)
            {
                DataBaseManager.isOpenUi = false;
                SIS_UI.SetActive(false);
            }
        }
    }
    public void Okay2CheckUI()
    {

        str_txt.text = DataBaseManager.str.ToString();
        str_image.fillAmount = ((float)DataBaseManager.str / 100);



        intl_txt.text = DataBaseManager.intl.ToString();
        intl_image.fillAmount = ((float)DataBaseManager.intl / 100);


        dex_txt.text = DataBaseManager.dex.ToString();
        dex_image.fillAmount = ((float)DataBaseManager.dex / 100);

        hp_txt.text = DataBaseManager.hp.ToString();
        hp_image.fillAmount = ((float)DataBaseManager.hp / 100);

        mp_txt.text = DataBaseManager.mp.ToString();
        mp_image.fillAmount = ((float)DataBaseManager.mp / 100);


        san_txt.text = DataBaseManager.san.ToString();
        san_image.fillAmount = ((float)DataBaseManager.san / 100);


        luk_txt.text = DataBaseManager.luk.ToString();
        luk_image.fillAmount = ((float)DataBaseManager.luk / 100);

        weal_txt.text = DataBaseManager.weal.ToString();
        weal_image.fillAmount = ((float)DataBaseManager.weal / 25);




        martialArtsPoint_t.text = DataBaseManager.martialArtsPoint.ToString();
        martialArtsPoint_g.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
        gunShotPoint_t.text = DataBaseManager.gunShotPoint.ToString();
        gunShotPoint_g.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
        swordPoint_t.text = DataBaseManager.swordPoint.ToString();
        swordPoint_g.fillAmount = (float)DataBaseManager.swordPoint / 100;
        ObservationPoint_t.text = DataBaseManager.ObservationPoint.ToString();
        ObservationPoint_g.fillAmount = (float)DataBaseManager.ObservationPoint / 100;
        swimingPoint_t.text = DataBaseManager.swimingPoint.ToString();
        swimingPoint_g.fillAmount = (float)DataBaseManager.swimingPoint / 100;


        medicine_t.text = DataBaseManager.medicinePoint.ToString();
        medicine_g.fillAmount = (float)DataBaseManager.medicinePoint / 100;
        analysis_t.text = DataBaseManager.analysisPoint.ToString();
        analysis_g.fillAmount = (float)DataBaseManager.analysisPoint / 100;
        occult_t.text = DataBaseManager.occultPoint.ToString();
        occult_g.fillAmount = (float)DataBaseManager.occultPoint / 100;
        listening_t.text = DataBaseManager.listeningPoint.ToString();
        listening_g.fillAmount = (float)DataBaseManager.listeningPoint / 100;
        psychotherapy_t.text = DataBaseManager.psychotherapyPoint.ToString();
        psychotherapy_g.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;


        evasion_t.text = DataBaseManager.evasionPoint.ToString();
        evasion_g.fillAmount = (float)DataBaseManager.evasionPoint / 100;
        deftness_t.text = DataBaseManager.deftnessPoint.ToString();
        deftness_g.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
        rhetoric_t.text = DataBaseManager.rhetoricPoint.ToString();
        rhetoric_g.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
        stealth_t.text = DataBaseManager.stealthPoint.ToString();
        stealth_g.fillAmount = (float)DataBaseManager.stealthPoint / 100;
        Disguise_t.text = DataBaseManager.DisguisePoint.ToString();
        Disguise_g.fillAmount = (float)DataBaseManager.DisguisePoint / 100;

    }

    public GameObject ItemBox_Nomal;

    public void ManageItem()
    {

        if (DataBaseManager.Bullet > 0)
        {
            ActivateObjectWithName(ItemBox_Nomal, "Bullet");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Nomal, "Bullet");
        }



    }

    public void OpenItemDetail()
    {
        if (ItemBox_Nomal.activeSelf == true)
        {
            if(DataBaseManager.nowItem == "Bullet")
            {
                Debug.Log(DataBaseManager.nowItem);
                // 여기에 클릭시 정보 표기하는 것을 표기.
            }

        }

    }


    public void ActivateObjectWithName(GameObject subject ,string objectName)
    {
        // 자식 오브젝트들을 모두 가져옵니다.
        Transform[] children = subject.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            // 자식 오브젝트의 이름이 지정한 이름과 일치하는지 확인합니다.
            if (child.name == objectName)
            {
                // 오브젝트를 활성화합니다.
                child.gameObject.SetActive(true);
                return;
            }
        }

        // 지정한 이름을 가진 오브젝트를 찾지 못한 경우에는 오류 메시지를 출력합니다.
        Debug.LogError("Object with name " + objectName + " not found in children.");
    }
    public void DeactivateObjectWithName(GameObject subject, string objectName)
    {
        // 자식 오브젝트들을 모두 가져옵니다.
        Transform[] children = subject.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            // 자식 오브젝트의 이름이 지정한 이름과 일치하는지 확인합니다.
            if (child.name == objectName)
            {
                // 오브젝트를 활성화합니다.
                child.gameObject.SetActive(false);
                return;
            }
        }

        // 지정한 이름을 가진 오브젝트를 찾지 못한 경우에는 오류 메시지를 출력합니다.
        Debug.LogError("Object with name " + objectName + " not found in children.");
    }
}
