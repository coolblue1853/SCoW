using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SIS_UIManager : MonoBehaviour
{
    public GameObject SIS_UI;

    public GameObject Stat_UI;
    public GameObject Inventory_UI;
    public GameObject Skill_UI;



    public GameObject Item_use;
    public GameObject Item_key;
    public GameObject Item_Eqip;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Detail;

    public TextMeshProUGUI Use_Name;
    public TextMeshProUGUI Use_Detail;

    public TextMeshProUGUI Key_Name;
    public TextMeshProUGUI Key_Detail;

    public GameObject ItemBox;
    public GameObject Use_ItemBox;
    public GameObject Key_ItemBox;

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


    public Image imageComponent_Use;
    public Image imageComponent_Key;
    public Sprite None;



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


    public void Open_UseItemUI()
    {

        DataBaseManager.nowItem = "";
        Item_use.transform.SetSiblingIndex(-1);

    }
    public void Open_KeyItemUI()
    {
        DataBaseManager.nowItem = "";
        Item_key.transform.SetSiblingIndex(-1);

    }
    public void Open_NomalItemUI()
    {
        Debug.Log("11");
        DataBaseManager.nowItem = "";
        Item_Eqip.transform.SetSiblingIndex(-1);
    }

    public void Open_StatUI()
    {
        Stat_UI.SetActive(true);
        Inventory_UI.SetActive(false);
        Skill_UI.SetActive(false);
    }
    public void Open_InventoryUI()
    {
        Stat_UI.SetActive(false);
        Inventory_UI.SetActive(true);
        Skill_UI.SetActive(false);
    }
    public void Open_SkillUI()
    {
        Stat_UI.SetActive(false);
        Inventory_UI.SetActive(false);
        Skill_UI.SetActive(true);
    }
    // Update is called once per frame
    public void OpenMap()
    {
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false)
        {
            OpenItemDetail();
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

        if (DataBaseManager.nowItem == "")
        {
            Name.text = "";
            Detail.text = "";
            Use_Name.text = "";
            Use_Detail.text = "";
            Key_Name.text = "";
            Key_Detail.text = "";
        }

        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isDirecting == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (SIS_UI.activeSelf == false)
                {
                    DataBaseManager.isItemUI = true;
                    Okay2CheckUI();
                    DataBaseManager.workSound = false;
                    OpenMap();
                }
                else if (SIS_UI.activeSelf == true)
                {
                    DataBaseManager.isItemUI = false;
                    CloseMap();
                }

            }
            if (Input.GetKeyDown(KeyCode.Escape) && SIS_UI.activeSelf == true)
            {
                DataBaseManager.isItemUI = false;
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

        if (DataBaseManager.Shotgun > 0)
        {
            ActivateObjectWithName(ItemBox_Nomal, "Shotgun");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Nomal, "Shotgun");
        }
        if (DataBaseManager.Rifle > 0)
        {
            ActivateObjectWithName(ItemBox_Nomal, "Rifle");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Nomal, "Rifle");
        }
        if (DataBaseManager.Revolver > 0)
        {
            ActivateObjectWithName(ItemBox_Nomal, "Revolver");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Nomal, "Revolver");
        }
        if (DataBaseManager.SmallPistol > 0)
        {
            ActivateObjectWithName(ItemBox_Nomal, "SmallPistol");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Nomal, "SmallPistol");
        }
        if (DataBaseManager.Bat > 0)
        {
            ActivateObjectWithName(ItemBox_Nomal, "Bat");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Nomal, "Bat");
        }
        if (DataBaseManager.Axe > 0)
        {
            ActivateObjectWithName(ItemBox_Nomal, "Axe");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Nomal, "Axe");
        }
        if (DataBaseManager.Molotov > 0)
        {
            ActivateObjectWithName(ItemBox_Nomal, "Molotov");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Nomal, "Molotov");
        }
        if (DataBaseManager.Dagger > 0)
        {
            ActivateObjectWithName(ItemBox_Nomal, "Dagger");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Nomal, "Dagger");
        }
        if (DataBaseManager.PistolAmmo > 0)
        {
            ActivateObjectWithName(ItemBox_Nomal, "PistolAmmo");
        }
        else
        {

            if (DataBaseManager.nowItem == "PistolAmmo")
            {
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(ItemBox_Nomal, "PistolAmmo");
        }
        if (DataBaseManager.RifleAmmo > 0)
        {

            ActivateObjectWithName(ItemBox_Nomal, "RifleAmmo");
        }
        else
        {
            if (DataBaseManager.nowItem == "RifleAmmo")
            {
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(ItemBox_Nomal, "RifleAmmo");
        }
        if (DataBaseManager.ShotgunAmmo > 0)
        {
            ActivateObjectWithName(ItemBox_Nomal, "ShotgunAmmo");
        }
        else
        {
            if (DataBaseManager.nowItem == "ShotgunAmmo")
            {
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(ItemBox_Nomal, "ShotgunAmmo");
        }


        if (DataBaseManager.Firstaidkit > 0)
        {
            ActivateObjectWithName(Use_ItemBox, "First aid kit");
        }
        else
        {
            if (DataBaseManager.nowItem == "First aid kit")
            {
                imageComponent_Use.sprite = None;
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(Use_ItemBox, "First aid kit");
        }
        if (DataBaseManager.Bandages > 0)
        {
            ActivateObjectWithName(Use_ItemBox, "Bandages");
        }
        else
        {
            if (DataBaseManager.nowItem == "Bandages")
            {
                imageComponent_Use.sprite = None;
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(Use_ItemBox, "Bandages");
        }
        if (DataBaseManager.Painkillers > 0)
        {
            ActivateObjectWithName(Use_ItemBox, "Painkillers");
        }
        else
        {

            if (DataBaseManager.nowItem == "Painkillers")
            {
                imageComponent_Use.sprite = None;
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(Use_ItemBox, "Painkillers");
        }

        if (DataBaseManager.Intel_Safe2 == true)
        {
            ActivateObjectWithName(Key_ItemBox, "Safe");
        }
        else
        {

            if (DataBaseManager.nowItem == "Safe")
            {
                imageComponent_Key.sprite = None;
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(Key_ItemBox, "Safe");
        }
    }

    public void OpenItemDetail()
    {
        if (ItemBox_Nomal.activeSelf == true)
        {
            if (DataBaseManager.nowItem == "")
            {
                Name.text = "";
                Detail.text = "";
            }
            if (DataBaseManager.nowItem == "Bat")
            {
                Name.text = "Bat";
                Detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D8)x5";
            }
            if (DataBaseManager.nowItem == "Dagger")
            {
                Name.text = "Dagger";
                Detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D4 + 2)x5";
            }
            if (DataBaseManager.nowItem == "Axe")
            {
                Name.text = "Axe";
                Detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D6 + 2)x5";
            }

            if (DataBaseManager.nowItem == "Molotov")
            {
                Name.text = "Molotov";
                Detail.text = "Skill :\nDeftness\n\nDamage :\n(2D6 + Fire)x5\n\nPcs : "+DataBaseManager.Molotov;
            }

            if (DataBaseManager.nowItem == "Shotgun")
            {
                Name.text = "Shotgun";
                Detail.text = "Skill :\nMarkmanship\n\nDamage :\n(6D4)x5\n\nMagazine :\n2 Bullet";
            }
            if (DataBaseManager.nowItem == "Rifle")
            {
                Name.text = "Rifle";
                Detail.text = "Skill :\nMarkmanship\n\nDamage :\n(2D6 + 2)x5\n\nMagazine :\n5 Bullet";
            }
            if (DataBaseManager.nowItem == "Revolver")
            {
                Name.text = "Revolver";
                Detail.text = "Skill :\nMarkmanship\n\nDamage :\n(1D10 + 2)x5\n\nMagazine :\n6 Bullet";
            }
            if (DataBaseManager.nowItem == "SmallPistol")
            {
                Name.text = "Small Pistol";
                Detail.text = "Skill :\nMarkmanship\n\nDamage :\n(1D6)x5\n\nMagazine :\n2 Bullet";
            }

            if (DataBaseManager.nowItem == "PistolAmmo")
            {
                Name.text = "PistolAmmo";
                Detail.text = "Used for :\nRevolver\nSmallPistol\n\nPcs : " + DataBaseManager.PistolAmmo;
            }
            if (DataBaseManager.nowItem == "RifleAmmo")
            {
                Name.text = "RifleAmmo";
                Detail.text = "Used for :\nRifle\n\nPcs : " + DataBaseManager.RifleAmmo;
            }
            if (DataBaseManager.nowItem == "ShotgunAmmo")
            {
                Name.text = "ShotgunAmmo";
                Detail.text = "Used for :\nShotgun\n\nPcs : " + DataBaseManager.ShotgunAmmo;
            }

        }

        if (Use_ItemBox.activeSelf == true)
        {
            if (DataBaseManager.nowItem == "")
            {
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            if (DataBaseManager.nowItem == "First aid kit" && DataBaseManager.Firstaidkit > 0)
            {
                Use_Name.text = "First aid kit";
                Use_Detail.text = "Skill :\nMedicine+20\n\nEffects:\nSuccessful Function checks restore(1D6)x5 HP points.\n\nPcs : " + DataBaseManager.Firstaidkit;
            }
            if (DataBaseManager.nowItem == "Bandages" && DataBaseManager.Bandages > 0)
            {
                Use_Name.text = "Bandages";
                Use_Detail.text = "Skill :\nMedicine+20\n\nEffects:\nSuccessful Function checks restore(1D3)x5 HP points.\n\nPcs : " + DataBaseManager.Bandages;
            }
            if (DataBaseManager.nowItem == "Painkillers" && DataBaseManager.Painkillers > 0)
            {
                Use_Name.text = "Painkillers";
                Use_Detail.text = "Skill :\nPsychotherapy+20\n\nEffects:\nSuccessful Function checks restore(1D3)x5 SAN points.\n\nPcs : " + DataBaseManager.Painkillers;
            }

        }

        if (Key_ItemBox.activeSelf == true)
        {
            if (DataBaseManager.nowItem == "")
            {
                Key_Name.text = "";
                Key_Detail.text = "";
            }
            if (DataBaseManager.nowItem == "Safe" && DataBaseManager.Intel_Safe2 == true)
            {
                Key_Name.text = "Safe";
                Key_Detail.text = "Skill :\nDeftness-20\n\nDetail:\nAiden's safe. It's locked solidly.";
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
            if (child.name == objectName && child.gameObject.activeSelf == false)
            {
                // 오브젝트를 활성화합니다.
                child.transform.SetAsLastSibling();
                child.gameObject.SetActive(true);
                return;
            }
        }

    }
    public void DeactivateObjectWithName(GameObject subject, string objectName)
    {
        // 자식 오브젝트들을 모두 가져옵니다.
        Transform[] children = subject.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            // 자식 오브젝트의 이름이 지정한 이름과 일치하는지 확인합니다.
            if (child.name == objectName )
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
