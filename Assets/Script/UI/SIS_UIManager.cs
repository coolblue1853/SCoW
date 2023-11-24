using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SIS_UIManager : MonoBehaviour
{
    

    public GameObject debuffDetail;

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


    public Image imageComponent_Eqip;
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
        SoundManager.Instance.ClickSound_Play();
        DataBaseManager.nowItem = "";
        Item_use.transform.SetSiblingIndex(-1);

    }
    public void Open_KeyItemUI()
    {
        SoundManager.Instance.ClickSound_Play();
        DataBaseManager.nowItem = "";
        Item_key.transform.SetSiblingIndex(-1);

    }
    public void Open_NomalItemUI()
    {
        SoundManager.Instance.ClickSound_Play();
        DataBaseManager.nowItem = "";
        Item_Eqip.transform.SetSiblingIndex(-1);
    }

    public void Open_StatUI()
    {
        SoundManager.Instance.ClickSound_Play();
        Stat_UI.SetActive(true);
        Inventory_UI.SetActive(false);
        Skill_UI.SetActive(false);
    }
    public void Open_InventoryUI()
    {
        SoundManager.Instance.ClickSound_Play();
        Stat_UI.SetActive(false);
        Inventory_UI.SetActive(true);
        Skill_UI.SetActive(false);
    }
    public void Open_SkillUI()
    {
        SoundManager.Instance.ClickSound_Play();
        Stat_UI.SetActive(false);
        Inventory_UI.SetActive(false);
        Skill_UI.SetActive(true);
    }
    // Update is called once per frame
    public void OpenMap()
    {
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.StoryDirecting == false && DataBaseManager.potalWait == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.secondisDirecting == false && DataBaseManager.isOpenUi == false && DataBaseManager.nowPlace != "BattleRoad" && DataBaseManager.isOpenMap == false)
        {
            SoundManager.Instance.PaperClip_Play();
            DataBaseManager.isItemUI = true;
            OpenItemDetail();
            DataBaseManager.workSound = false;
            DataBaseManager.isOpenUi = true;
            SIS_UI.SetActive(true);
        }

    }
    public void ResetDetail()
    {
        imageComponent_Eqip.sprite = None;
        imageComponent_Use.sprite = None;
        imageComponent_Key.sprite = None;
        Key_Name.text = "";
        Key_Detail.text = "";
        Name.text = "";
        Detail.text = "";
        Use_Name.text = "";
        Use_Detail.text = "";
    }

    public void CloseMap()
    {
        SoundManager.Instance.ClickSound_Play();
        ResetDetail();
        debuffDetail.SetActive(false);
        DataBaseManager.isItemUI = false;
        DataBaseManager.isOpenUi = false;
        SIS_UI.SetActive(false);
    }


    public GameObject Disorder;
    public GameObject Disorder2;
    public GameObject Disorder3;

    private void Update()
    {
        if(DataBaseManager.isDebuff_CognitiveBreakdown == true && Disorder.activeSelf == false)
        {
            Disorder.SetActive(true);
            Disorder2.SetActive(true);
            Disorder3.SetActive(true);
        }

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

        if (DataBaseManager.isActiveDialog1 == false)
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
                CloseMap();
            }


            if(SIS_UI.activeSelf == false && Key_Name.text != "" && Name.text != "" && Use_Name.text != "")
            {
                imageComponent_Eqip.sprite = None;
                imageComponent_Use.sprite = None;
                imageComponent_Key.sprite = None;
                Key_Name.text = "";
                Key_Detail.text = "";
                Name.text = "";
                Detail.text = "";
                Use_Name.text = "";
                Use_Detail.text = "";
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

        if (DataBaseManager.getSafe == true)
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
        if (DataBaseManager.GainOldMap == true)
        {
            ActivateObjectWithName(Key_ItemBox, "OldMap");
        }
        else
        {

            if (DataBaseManager.nowItem == "OldMap")
            {
                imageComponent_Key.sprite = None;
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(Key_ItemBox, "OldMap");
        }

        if (DataBaseManager.SafeDocument == true)
        {
            ActivateObjectWithName(Key_ItemBox, "Document");
        }
        else
        {

            if (DataBaseManager.nowItem == "Document")
            {
                imageComponent_Key.sprite = None;
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(Key_ItemBox, "Document");
        }
        if (DataBaseManager.GainMap == true)
        {
            ActivateObjectWithName(Key_ItemBox, "Map");
        }
        else
        {

            if (DataBaseManager.nowItem == "Map")
            {
                imageComponent_Key.sprite = None;
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(Key_ItemBox, "Map");
        }
        if (DataBaseManager.SewerageKey == true)
        {
            ActivateObjectWithName(Key_ItemBox, "Sewerge Key");
        }
        else
        {

            if (DataBaseManager.nowItem == "Sewerge Key")
            {
                imageComponent_Key.sprite = None;
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(Key_ItemBox, "Sewerge Key");
        }

        if (DataBaseManager.Intel_Dave1 == true)
        {
            ActivateObjectWithName(Key_ItemBox, "Bracelet");
        }
        else
        {

            if (DataBaseManager.nowItem == "Bracelet")
            {
                imageComponent_Key.sprite = None;
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(Key_ItemBox, "Bracelet");
        }

        if (DataBaseManager.GainCoat == true)
        {
            ActivateObjectWithName(Key_ItemBox, "Coat");
        }
        else
        {

            if (DataBaseManager.nowItem == "Coat")
            {
                imageComponent_Key.sprite = None;
                DataBaseManager.nowItem = "";
                Use_Name.text = "";
                Use_Detail.text = "";
            }
            DeactivateObjectWithName(Key_ItemBox, "Coat");
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
                Detail.text = "Skill :\nDeftness\n\nDamage :\n(2D6 + 2)x5\n\nPcs : "+DataBaseManager.Molotov;
            }

            if (DataBaseManager.nowItem == "Shotgun")
            {
                Name.text = "Shotgun";
                Detail.text = "Skill :\nMarkmanship\n\nDamage :\n(5D4)x5\n\nMagazine :\n2 Bullet";
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
                Use_Detail.text = "Skill :\nNoSkill\n\nEffects:\nSuccessful Function checks restore(1D3)x5 SAN points.\n\nPcs : " + DataBaseManager.Painkillers;
            }

        }

        if (Key_ItemBox.activeSelf == true )
        {
            if (DataBaseManager.nowItem == "")
            {
                Key_Name.text = "";
                Key_Detail.text = "";
            }
            if (DataBaseManager.nowItem == "Safe" && DataBaseManager.getSafe == true)
            {
                Key_Name.text = "Safe";
                Key_Detail.text = "Skill :\nDeftness-20\n\nDetail:\nAiden's safe. It's locked solidly.";
            }
            if (DataBaseManager.nowItem == "OldMap" && DataBaseManager.GainOldMap == true)
            {
                Key_Name.text = "Old Map";
                Key_Detail.text = "Detail:\nAn old map picked up from inside a sewer.";
            }
            if (DataBaseManager.nowItem == "Document" && DataBaseManager.SafeDocument == true)
            {
                Key_Name.text = "Document";
                Key_Detail.text = "Detail:\nThe documents in Aiden's safe.";
            }
            if (DataBaseManager.nowItem == "Map" && DataBaseManager.GainMap == true)
            {
                Key_Name.text = "Map";
                Key_Detail.text = "Detail:\nMap of the sewer interior.";
            }
            if (DataBaseManager.nowItem == "Bracelet" && DataBaseManager.Intel_Dave1 == true)
            {
                Key_Name.text = "Bracelet";
                Key_Detail.text = "Detail:\nElla's Bracelet.\nIt seems to be expensive.";
            }
            if (DataBaseManager.nowItem == "Sewerge Key" && DataBaseManager.SewerageKey == true)
            {
                Key_Name.text = "Sewerge Key";
                Key_Detail.text = "Detail:\nKeys used in sewer maintenance facilities.)";
            }
            if (DataBaseManager.nowItem == "Coat" && DataBaseManager.GainCoat == true)
            {
                Key_Name.text = "Coat";
                Key_Detail.text = "Skill :\nDisguise\n\nDetail:\nA sewer worker's Outfit.";
            }

        }



    }

    public void openSetting()
    {
        settingManager.Instance.OpenSetting();
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

     string inputString; // 사용자가 입력한 문자열
     string[] resultArray; // 문자열을 저장할 배열
    int arrayInt=0;
    public void CareLessActive()
    {
        // 배열 초기화
        int maxWeapons = 13; // 무기와 아이템의 총 개수
        resultArray = new string[maxWeapons];

        if (DataBaseManager.Shotgun > 0 )
        {
            resultArray[arrayInt] = "Shotgun";
            arrayInt += 1;


        }
        if (DataBaseManager.Rifle > 0)
        {
            resultArray[arrayInt] = "Rifle";
            arrayInt += 1;


        }
        if (DataBaseManager.Revolver > 0)
        {
            resultArray[arrayInt] = "Revolver";
            arrayInt += 1;

        }
        if (DataBaseManager.SmallPistol > 0)
        {
            resultArray[arrayInt] = "SmallPistol";
            arrayInt += 1;

        }
        if (DataBaseManager.Bat > 0)
        {
            resultArray[arrayInt] = "Bat";
            arrayInt += 1;

        }
        if (DataBaseManager.Axe > 0)
        {
            resultArray[arrayInt] = "Axe";
            arrayInt += 1;

        }
        if (DataBaseManager.Dagger > 0)
        {
            resultArray[arrayInt] = "Dagger";
            arrayInt += 1;

        }

        if (DataBaseManager.Molotov > 0)
        {
            resultArray[arrayInt] = "Molotov";
            arrayInt += 1;

        }
        if (DataBaseManager.PistolAmmo > 0)
        {
            resultArray[arrayInt] = "PistolAmmo";
            arrayInt += 1;

        }
        if (DataBaseManager.RifleAmmo > 0)
        {
            resultArray[arrayInt] = "RifleAmmo";
            arrayInt += 1;

        }
        if (DataBaseManager.ShotgunAmmo > 0)
        {
            resultArray[arrayInt] = "ShotgunAmmo";
            arrayInt += 1;

        }
        if (DataBaseManager.Firstaidkit > 0)
        {
            resultArray[arrayInt] = "Firstaidkit";
            arrayInt += 1;

        }
        if (DataBaseManager.Bandages > 0)
        {
            resultArray[arrayInt] = "Bandages";
            arrayInt += 1;

        }
        if (DataBaseManager.Painkillers > 0)
        {
            resultArray[arrayInt] = "Painkillers";
            arrayInt += 1;

        }

        int i = Random.Range(0, arrayInt);
        Debug.Log(resultArray[i]);

        if (resultArray[i] == "Shotgun")
        {
            DataBaseManager.Shotgun -= 1;


        }
        else if (resultArray[i] == "Rifle")
        {
            DataBaseManager.Rifle -= 1;


        }
        else if (resultArray[i] == "Revolver")
        {
            DataBaseManager.Revolver -= 1;


        }
        else if (resultArray[i] == "SmallPistol")
        {
            DataBaseManager.SmallPistol -= 1;


        }
        else if (resultArray[i] == "Bat")
        {
            DataBaseManager.Bat -= 1;


        }
        else if (resultArray[i] == "Axe")
        {
            DataBaseManager.Axe -= 1;


        }
        else if (resultArray[i] == "Dagger")
        {
            DataBaseManager.Dagger -= 1;


        }
        else if (resultArray[i] == "Molotov")
        {
            DataBaseManager.Molotov -= 1;


        }
        else if (resultArray[i] == "PistolAmmo")
        {
            DataBaseManager.PistolAmmo -= 1;


        }
        else if (resultArray[i] == "RifleAmmo")
        {
            DataBaseManager.RifleAmmo -= 1;


        }
        else if (resultArray[i] == "ShotgunAmmo")
        {
            DataBaseManager.ShotgunAmmo -= 1;


        }
        else if (resultArray[i] == "ShotgunAmmo")
        {
            DataBaseManager.ShotgunAmmo -= 1;


        }
        else if (resultArray[i] == "Firstaidkit")
        {
            DataBaseManager.Firstaidkit -= 1;


        }
        else if (resultArray[i] == "Bandages")
        {
            DataBaseManager.Bandages -= 1;
        }
        else if (resultArray[i] == "Painkillers")
        {
            DataBaseManager.Painkillers -= 1;
        }

  
    }
    public GameObject VisionLoss;

    public GameObject ThinWallets;
    public void ThinWallets_Open()
    {
        ThinWallets.SetActive(true);
    }
    public GameObject Carelessness;
    public void Carelessness_Open()
    {
        Carelessness.SetActive(true);
    }
    public GameObject Debilitation;
    public void Debilitation_Open()
    {
        Debilitation.SetActive(true);
    }
    public GameObject Dizziness;
    public void Dizziness_Open()
    {
        Dizziness.SetActive(true);
    }
    public GameObject Sprains;
    public void Sprains_Open()
    {
        Sprains.SetActive(true);
    }
    public GameObject Unlucky;
    public void Unlucky_Open()
    {
        Unlucky.SetActive(true);
    }
    public GameObject MusclePain;
    public void MusclePain_Open()
    {
        MusclePain.SetActive(true);
    }
    public GameObject Migraines;
    public void Migraines_Open()
    {
        Migraines.SetActive(true);
    }
    public GameObject EyeDisease;
    public void EyeDisease_Open()
    {
        EyeDisease.SetActive(true);
    }

    //Level - 2 Debuff
    public GameObject Masochism;
    public void Masochism_Open()
    {
        Masochism.SetActive(true);
    }
    public GameObject Careless;
    public void Careless_Open()
    {
        Careless.SetActive(true);
    }
    public GameObject MentalWeakness;
    public void MentalWeakness_Open()
    {
        MentalWeakness.SetActive(true);
    }
    public GameObject Helplessness;
    public void Helplessness_Open()
    {
        Helplessness.SetActive(true);
    }
    public GameObject Extravagant;
    public void Extravagant_Open()
    {
        Extravagant.SetActive(true);
    }
    public GameObject PanicAttack;
    public void PanicAttack_Open()
    {
        PanicAttack.SetActive(true);
    }
    public GameObject Medicaldistrust;
    public void Medicaldistrust_Open()
    {
        Medicaldistrust.SetActive(true);
    }
    public GameObject Hallucinations;
    public void Hallucinations_Open()
    {
        Hallucinations.SetActive(true);
    }
    public GameObject Tightwad;
    public void Tightwad_Open()
    {
        Tightwad.SetActive(true);
    }

    //Level - 3 Debuff
    public GameObject NightPhobia;
    public void NightPhobia_Open()
    {
        NightPhobia.SetActive(true);
    }
    public GameObject Deafness;
    public void Deafness_Open()
    {
        Deafness.SetActive(true);
    }
    public void VisionLoss_Open()
    {
        VisionLoss.SetActive(true);
    }
    public GameObject ShortTempered;
    public void ShortTempered_Open()
    {
        ShortTempered.SetActive(true);
    }
    public GameObject Dyslexia;
    public void Dyslexia_Open()
    {
        Dyslexia.SetActive(true);
    }
    public GameObject DrugPhobia;
    public void DrugPhobia_Open()
    {
        DrugPhobia.SetActive(true);
    }
    public GameObject CognitiveDisorder;
    public void CognitiveDisorder_Open()
    {
        CognitiveDisorder.SetActive(true);
    }
    public GameObject CognitiveBreakdown;
    public void CognitiveBreakdown_Open()
    {
        CognitiveBreakdown.SetActive(true);
    }
    public GameObject HomicidalImpulse;
    public void HomicidalImpulse_Open()
    {
        HomicidalImpulse.SetActive(true);
    }
    public GameObject Perfectionism;
    public void Perfectionism_Open()
    {
        Perfectionism.SetActive(true);
    }

    // 배열에서 랜덤 변수를 선택하는 함수

}
