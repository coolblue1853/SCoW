using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SIS_UIManager : MonoBehaviour
{
    public GameObject debuffDetail;
    public GameObject sisUI;
    public GameObject statUI;
    public GameObject inventoryUI;
    public GameObject skillUI;
    public GameObject itemuse;
    public GameObject itemKey;
    public GameObject itemEqip;
    public TextMeshProUGUI name;
    public TextMeshProUGUI detail;
    public TextMeshProUGUI useName;
    public TextMeshProUGUI useDetail;
    public TextMeshProUGUI keyName;
    public TextMeshProUGUI keyDetail;
    public GameObject itemBox;
    public GameObject useItemBox;
    public GameObject keyItemBox;
    public Text strTxt;
    public Text intlTxt;
    public Text dexTxt;
    public Text hpTxt;
    public Text mpTxt;
    public Text sanTxt;
    public Text lukTxt;
    public Text wealTxt;
    public Image strImage;
    public Image intlImage;
    public Image dexImage;
    public Image hpImage;
    public Image mpImage;
    public Image sanImage;
    public Image lukImage;
    public Image wealImage;

    //skill - str
    public Text martialArtsPointTxt;
    public Text gunShotPointTxt;
    public Text swordPointTxt;
    public Text ObservationPointTxt;
    public Text swimingPointTxt;
    public Image martialArtsPointImg;
    public Image gunShotPointImg;
    public Image swordPointImg;
    public Image ObservationPointImg;
    public Image swimingPointImg;

    //skill - int
    public Text medicineTxt;
    public Text analysisTxt;
    public Text listeningTxt;
    public Text psychotherapyTxt;
    public Text occultTxt;
    public Image medicineImg;
    public Image analysisImg;
    public Image listeningImg;
    public Image psychotherapyImg;
    public Image occultImg;

    //skill - dex
    public Text evasionTxt;
    public Text deftnessTxt;
    public Text rhetoricTxt;
    public Text stealthTxt;
    public Text DisguiseTxt;
    public Image evasionImg;
    public Image deftnessImg;
    public Image rhetoricImg;
    public Image stealthImg;
    public Image DisguiseImg;
    public Image imageComponentEqip;
    public Image imageComponentUse;
    public Image imageComponentKey;
    public Sprite none;
    public GameObject disorder;
    public GameObject disorder2;
    public GameObject disorder3;
    public GameObject itemBoxNomal;
    string inputString; // 사용자가 입력한 문자열
    string[] resultArray; // 문자열을 저장할 배열
    int arrayInt = 0;
    public GameObject visionLoss;
    public GameObject thinWallets;
    public GameObject carelessness;
    public GameObject debilitation;
    public GameObject sprains;
    public GameObject musclePain;
    public GameObject dizziness;
    public GameObject unlucky;
    public GameObject migraines;
    public GameObject eyeDisease;
    public GameObject masochism;
    public GameObject careless;
    public GameObject mentalWeakness;
    public GameObject helplessness;
    public GameObject extravagant;
    public GameObject panicAttack;
    public GameObject medicaldistrust;
    public GameObject hallucinations;
    public GameObject nightPhobia;
    public GameObject deafness;
    public GameObject shortTempered;
    public GameObject dyslexia;
    public GameObject drugPhobia;
    public GameObject cognitiveDisorder;
    public GameObject cognitiveBreakdown;
    public GameObject homicidalImpulse;
    public GameObject perfectionism;
    public GameObject tightwad;
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
    public void OpenUseItemUI()
    {
        SoundManager.Instance.ClickSoundPlay();
        DataBaseManager.nowItem = "";
        itemuse.transform.SetSiblingIndex(-1);
    }
    public void OpenKeyItemUI()
    {
        SoundManager.Instance.ClickSoundPlay();
        DataBaseManager.nowItem = "";
        itemKey.transform.SetSiblingIndex(-1);
    }
    public void OpenNomalItemUI()
    {
        SoundManager.Instance.ClickSoundPlay();
        DataBaseManager.nowItem = "";
        itemEqip.transform.SetSiblingIndex(-1);
    }
    public void OpenStatUI()
    {
        SoundManager.Instance.ClickSoundPlay();
        statUI.SetActive(true);
        inventoryUI.SetActive(false);
        skillUI.SetActive(false);
    }
    public void OpenInventoryUI()
    {
        SoundManager.Instance.ClickSoundPlay();
        statUI.SetActive(false);
        inventoryUI.SetActive(true);
        skillUI.SetActive(false);
    }
    public void OpenSkillUI()
    {
        SoundManager.Instance.ClickSoundPlay();
        statUI.SetActive(false);
        inventoryUI.SetActive(false);
        skillUI.SetActive(true);
    }
    // Update is called once per frame
    public void OpenMap()
    {
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.storyDirecting == false && DataBaseManager.potalWait == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.secondisDirecting == false && DataBaseManager.isOpenUi == false && DataBaseManager.nowPlace != "BattleRoad" && DataBaseManager.isOpenMap == false)
        {
            SoundManager.Instance.PaperClipPlay();
            DataBaseManager.isItemUI = true;
            OpenItemDetail();
            DataBaseManager.workSound = false;
            DataBaseManager.isOpenUi = true;
            sisUI.SetActive(true);
        }
    }
    public void ResetDetail()
    {
        imageComponentEqip.sprite = none;
        imageComponentUse.sprite = none;
        imageComponentKey.sprite = none;
        keyName.text = "";
        keyDetail.text = "";
        name.text = "";
        detail.text = "";
        useName.text = "";
        useDetail.text = "";
    }
    public void CloseMap()
    {
        SoundManager.Instance.ClickSoundPlay();
        ResetDetail();
        debuffDetail.SetActive(false);
        DataBaseManager.isItemUI = false;
        DataBaseManager.isOpenUi = false;
        sisUI.SetActive(false);
    }

    private void Update()
    {
        if (DataBaseManager.isDebuffCognitiveBreakdown == true && disorder.activeSelf == false)
        {
            disorder.SetActive(true);
            disorder2.SetActive(true);
            disorder3.SetActive(true);
        }
        ManageItem();
        if (DataBaseManager.nowItem == "")
        {
            name.text = "";
            detail.text = "";
            useName.text = "";
            useDetail.text = "";
            keyName.text = "";
            keyDetail.text = "";
        }
        if (DataBaseManager.isActiveDialog1 == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (sisUI.activeSelf == false)
                {
                    Okay2CheckUI();
                    DataBaseManager.workSound = false;
                    OpenMap();
                }
                else if (sisUI.activeSelf == true)
                {
                    CloseMap();
                }

            }
            if (Input.GetKeyDown(KeyCode.Escape) && sisUI.activeSelf == true)
            {
                CloseMap();
            }
            if (sisUI.activeSelf == false && keyName.text != "" && name.text != "" && useName.text != "")
            {
                imageComponentEqip.sprite = none;
                imageComponentUse.sprite = none;
                imageComponentKey.sprite = none;
                keyName.text = "";
                keyDetail.text = "";
                name.text = "";
                detail.text = "";
                useName.text = "";
                useDetail.text = "";
            }
        }
    }
    public void Okay2CheckUI()
    {
        strTxt.text = DataBaseManager.str.ToString();
        strImage.fillAmount = ((float)DataBaseManager.str / 100);

        intlTxt.text = DataBaseManager.intl.ToString();
        intlImage.fillAmount = ((float)DataBaseManager.intl / 100);

        dexTxt.text = DataBaseManager.dex.ToString();
        dexImage.fillAmount = ((float)DataBaseManager.dex / 100);

        hpTxt.text = DataBaseManager.hp.ToString();
        hpImage.fillAmount = ((float)DataBaseManager.hp / 100);

        mpTxt.text = DataBaseManager.mp.ToString();
        mpImage.fillAmount = ((float)DataBaseManager.mp / 100);

        sanTxt.text = DataBaseManager.san.ToString();
        sanImage.fillAmount = ((float)DataBaseManager.san / 100);

        lukTxt.text = DataBaseManager.luk.ToString();
        lukImage.fillAmount = ((float)DataBaseManager.luk / 100);

        wealTxt.text = DataBaseManager.weal.ToString();
        wealImage.fillAmount = ((float)DataBaseManager.weal / 25);

        martialArtsPointTxt.text = DataBaseManager.martialArtsPoint.ToString();
        martialArtsPointImg.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
        gunShotPointTxt.text = DataBaseManager.gunShotPoint.ToString();
        gunShotPointImg.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
        swordPointTxt.text = DataBaseManager.swordPoint.ToString();
        swordPointImg.fillAmount = (float)DataBaseManager.swordPoint / 100;
        ObservationPointTxt.text = DataBaseManager.observationPoint.ToString();
        ObservationPointImg.fillAmount = (float)DataBaseManager.observationPoint / 100;
        swimingPointTxt.text = DataBaseManager.swimingPoint.ToString();
        swimingPointImg.fillAmount = (float)DataBaseManager.swimingPoint / 100;

        medicineTxt.text = DataBaseManager.medicinePoint.ToString();
        medicineImg.fillAmount = (float)DataBaseManager.medicinePoint / 100;
        analysisTxt.text = DataBaseManager.analysisPoint.ToString();
        analysisImg.fillAmount = (float)DataBaseManager.analysisPoint / 100;
        occultTxt.text = DataBaseManager.occultPoint.ToString();
        occultImg.fillAmount = (float)DataBaseManager.occultPoint / 100;
        listeningTxt.text = DataBaseManager.listeningPoint.ToString();
        listeningImg.fillAmount = (float)DataBaseManager.listeningPoint / 100;
        psychotherapyTxt.text = DataBaseManager.psychotherapyPoint.ToString();
        psychotherapyImg.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;

        evasionTxt.text = DataBaseManager.evasionPoint.ToString();
        evasionImg.fillAmount = (float)DataBaseManager.evasionPoint / 100;
        deftnessTxt.text = DataBaseManager.deftnessPoint.ToString();
        deftnessImg.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
        rhetoricTxt.text = DataBaseManager.rhetoricPoint.ToString();
        rhetoricImg.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
        stealthTxt.text = DataBaseManager.stealthPoint.ToString();
        stealthImg.fillAmount = (float)DataBaseManager.stealthPoint / 100;
        DisguiseTxt.text = DataBaseManager.disguisePoint.ToString();
        DisguiseImg.fillAmount = (float)DataBaseManager.disguisePoint / 100;
    }

    public void ManageItem()
    {
        if (DataBaseManager.shotgun > 0)
        {
            ActivateObjectWithName(itemBoxNomal, "Shotgun");
        }
        else
        {
            DeactivateObjectWithName(itemBoxNomal, "Shotgun");
        }
        if (DataBaseManager.rifle > 0)
        {
            ActivateObjectWithName(itemBoxNomal, "Rifle");
        }
        else
        {
            DeactivateObjectWithName(itemBoxNomal, "Rifle");
        }
        if (DataBaseManager.revolver > 0)
        {
            ActivateObjectWithName(itemBoxNomal, "Revolver");
        }
        else
        {
            DeactivateObjectWithName(itemBoxNomal, "Revolver");
        }
        if (DataBaseManager.smallPistol > 0)
        {
            ActivateObjectWithName(itemBoxNomal, "SmallPistol");
        }
        else
        {
            DeactivateObjectWithName(itemBoxNomal, "SmallPistol");
        }
        if (DataBaseManager.bat > 0)
        {
            ActivateObjectWithName(itemBoxNomal, "Bat");
        }
        else
        {
            DeactivateObjectWithName(itemBoxNomal, "Bat");
        }
        if (DataBaseManager.axe > 0)
        {
            ActivateObjectWithName(itemBoxNomal, "Axe");
        }
        else
        {
            DeactivateObjectWithName(itemBoxNomal, "Axe");
        }
        if (DataBaseManager.molotov > 0)
        {
            ActivateObjectWithName(itemBoxNomal, "Molotov");
        }
        else
        {
            DeactivateObjectWithName(itemBoxNomal, "Molotov");
        }
        if (DataBaseManager.dagger > 0)
        {
            ActivateObjectWithName(itemBoxNomal, "Dagger");
        }
        else
        {
            DeactivateObjectWithName(itemBoxNomal, "Dagger");
        }
        if (DataBaseManager.pistolAmmo > 0)
        {
            ActivateObjectWithName(itemBoxNomal, "PistolAmmo");
        }
        else
        {
            if (DataBaseManager.nowItem == "PistolAmmo")
            {
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(itemBoxNomal, "PistolAmmo");
        }
        if (DataBaseManager.rifleAmmo > 0)
        {

            ActivateObjectWithName(itemBoxNomal, "RifleAmmo");
        }
        else
        {
            if (DataBaseManager.nowItem == "RifleAmmo")
            {
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(itemBoxNomal, "RifleAmmo");
        }
        if (DataBaseManager.shotgunAmmo > 0)
        {
            ActivateObjectWithName(itemBoxNomal, "ShotgunAmmo");
        }
        else
        {
            if (DataBaseManager.nowItem == "ShotgunAmmo")
            {
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(itemBoxNomal, "ShotgunAmmo");
        }
        if (DataBaseManager.firstaidkit > 0)
        {
            ActivateObjectWithName(useItemBox, "First aid kit");
        }
        else
        {
            if (DataBaseManager.nowItem == "First aid kit")
            {
                imageComponentUse.sprite = none;
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(useItemBox, "First aid kit");
        }
        if (DataBaseManager.bandages > 0)
        {
            ActivateObjectWithName(useItemBox, "Bandages");
        }
        else
        {
            if (DataBaseManager.nowItem == "Bandages")
            {
                imageComponentUse.sprite = none;
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(useItemBox, "Bandages");
        }
        if (DataBaseManager.painkillers > 0)
        {
            ActivateObjectWithName(useItemBox, "Painkillers");
        }
        else
        {
            if (DataBaseManager.nowItem == "Painkillers")
            {
                imageComponentUse.sprite = none;
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(useItemBox, "Painkillers");
        }
        if (DataBaseManager.getSafe == true)
        {
            ActivateObjectWithName(keyItemBox, "Safe");
        }
        else
        {
            if (DataBaseManager.nowItem == "Safe")
            {
                imageComponentKey.sprite = none;
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(keyItemBox, "Safe");
        }
        if (DataBaseManager.gainOldMap == true)
        {
            ActivateObjectWithName(keyItemBox, "OldMap");
        }
        else
        {
            if (DataBaseManager.nowItem == "OldMap")
            {
                imageComponentKey.sprite = none;
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(keyItemBox, "OldMap");
        }
        if (DataBaseManager.safeDocument == true)
        {
            ActivateObjectWithName(keyItemBox, "Document");
        }
        else
        {
            if (DataBaseManager.nowItem == "Document")
            {
                imageComponentKey.sprite = none;
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(keyItemBox, "Document");
        }
        if (DataBaseManager.gainMap == true)
        {
            ActivateObjectWithName(keyItemBox, "Map");
        }
        else
        {
            if (DataBaseManager.nowItem == "Map")
            {
                imageComponentKey.sprite = none;
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(keyItemBox, "Map");
        }
        if (DataBaseManager.sewerageKey == true)
        {
            ActivateObjectWithName(keyItemBox, "Sewerge Key");
        }
        else
        {
            if (DataBaseManager.nowItem == "Sewerge Key")
            {
                imageComponentKey.sprite = none;
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(keyItemBox, "Sewerge Key");
        }
        if (DataBaseManager.intelDave1 == true)
        {
            ActivateObjectWithName(keyItemBox, "Bracelet");
        }
        else
        {
            if (DataBaseManager.nowItem == "Bracelet")
            {
                imageComponentKey.sprite = none;
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(keyItemBox, "Bracelet");
        }
        if (DataBaseManager.gainCoat == true)
        {
            ActivateObjectWithName(keyItemBox, "Coat");
        }
        else
        {
            if (DataBaseManager.nowItem == "Coat")
            {
                imageComponentKey.sprite = none;
                DataBaseManager.nowItem = "";
                useName.text = "";
                useDetail.text = "";
            }
            DeactivateObjectWithName(keyItemBox, "Coat");
        }
    }
    public void OpenItemDetail()
    {
        if (itemBoxNomal.activeSelf == true)
        {
            if (DataBaseManager.nowItem == "")
            {
                name.text = "";
                detail.text = "";
            }
            if (DataBaseManager.nowItem == "Bat")
            {
                name.text = "Bat";
                detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D8)x5";
            }
            if (DataBaseManager.nowItem == "Dagger")
            {
                name.text = "Dagger";
                detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D4 + 2)x5";
            }
            if (DataBaseManager.nowItem == "Axe")
            {
                name.text = "Axe";
                detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D6 + 2)x5";
            }
            if (DataBaseManager.nowItem == "Molotov")
            {
                name.text = "Molotov";
                detail.text = "Skill :\nDeftness\n\nDamage :\n(2D6 + 2)x5\n\nPcs : " + DataBaseManager.molotov;
            }
            if (DataBaseManager.nowItem == "Shotgun")
            {
                name.text = "Shotgun";
                detail.text = "Skill :\nMarkmanship\n\nDamage :\n(5D4)x5\n\nMagazine :\n2 Bullet";
            }
            if (DataBaseManager.nowItem == "Rifle")
            {
                name.text = "Rifle";
                detail.text = "Skill :\nMarkmanship\n\nDamage :\n(2D6 + 2)x5\n\nMagazine :\n5 Bullet";
            }
            if (DataBaseManager.nowItem == "Revolver")
            {
                name.text = "Revolver";
                detail.text = "Skill :\nMarkmanship\n\nDamage :\n(1D10 + 2)x5\n\nMagazine :\n6 Bullet";
            }
            if (DataBaseManager.nowItem == "SmallPistol")
            {
                name.text = "Small Pistol";
                detail.text = "Skill :\nMarkmanship\n\nDamage :\n(1D6)x5\n\nMagazine :\n2 Bullet";
            }
            if (DataBaseManager.nowItem == "PistolAmmo")
            {
                name.text = "PistolAmmo";
                detail.text = "Used for :\nRevolver\nSmallPistol\n\nPcs : " + DataBaseManager.pistolAmmo;
            }
            if (DataBaseManager.nowItem == "RifleAmmo")
            {
                name.text = "RifleAmmo";
                detail.text = "Used for :\nRifle\n\nPcs : " + DataBaseManager.rifleAmmo;
            }
            if (DataBaseManager.nowItem == "ShotgunAmmo")
            {
                name.text = "ShotgunAmmo";
                detail.text = "Used for :\nShotgun\n\nPcs : " + DataBaseManager.shotgunAmmo;
            }
        }
        if (useItemBox.activeSelf == true)
        {
            if (DataBaseManager.nowItem == "")
            {
                useName.text = "";
                useDetail.text = "";
            }
            if (DataBaseManager.nowItem == "First aid kit" && DataBaseManager.firstaidkit > 0)
            {
                useName.text = "First aid kit";
                useDetail.text = "Skill :\nMedicine+20\n\nEffects:\nSuccessful Function checks restore(1D6)x5 HP points.\n\nPcs : " + DataBaseManager.firstaidkit;
            }
            if (DataBaseManager.nowItem == "Bandages" && DataBaseManager.bandages > 0)
            {
                useName.text = "Bandages";
                useDetail.text = "Skill :\nMedicine+20\n\nEffects:\nSuccessful Function checks restore(1D3)x5 HP points.\n\nPcs : " + DataBaseManager.bandages;
            }
            if (DataBaseManager.nowItem == "Painkillers" && DataBaseManager.painkillers > 0)
            {
                useName.text = "Painkillers";
                useDetail.text = "Skill :\nNoSkill\n\nEffects:\nSuccessful Function checks restore(1D3)x5 SAN points.\n\nPcs : " + DataBaseManager.painkillers;
            }
        }
        if (keyItemBox.activeSelf == true)
        {
            if (DataBaseManager.nowItem == "")
            {
                keyName.text = "";
                keyDetail.text = "";
            }
            if (DataBaseManager.nowItem == "Safe" && DataBaseManager.getSafe == true)
            {
                keyName.text = "Safe";
                keyDetail.text = "Skill :\nDeftness-20\n\nDetail:\nAiden's safe. It's locked solidly.";
            }
            if (DataBaseManager.nowItem == "OldMap" && DataBaseManager.gainOldMap == true)
            {
                keyName.text = "Old Map";
                keyDetail.text = "Detail:\nAn old map picked up from inside a sewer.";
            }
            if (DataBaseManager.nowItem == "Document" && DataBaseManager.safeDocument == true)
            {
                keyName.text = "Document";
                keyDetail.text = "Detail:\nThe documents in Aiden's safe.";
            }
            if (DataBaseManager.nowItem == "Map" && DataBaseManager.gainMap == true)
            {
                keyName.text = "Map";
                keyDetail.text = "Detail:\nMap of the sewer interior.";
            }
            if (DataBaseManager.nowItem == "Bracelet" && DataBaseManager.intelDave1 == true)
            {
                keyName.text = "Bracelet";
                keyDetail.text = "Detail:\nElla's Bracelet.\nIt seems to be expensive.";
            }
            if (DataBaseManager.nowItem == "Sewerge Key" && DataBaseManager.sewerageKey == true)
            {
                keyName.text = "Sewerge Key";
                keyDetail.text = "Detail:\nKeys used in sewer maintenance facilities.)";
            }
            if (DataBaseManager.nowItem == "Coat" && DataBaseManager.gainCoat == true)
            {
                keyName.text = "Coat";
                keyDetail.text = "Skill :\nDisguise\n\nDetail:\nA sewer worker's Outfit.";
            }
        }
    }
    public void OpenSetting()
    {
        settingManager.Instance.OpenSetting();
    }
    public void ActivateObjectWithName(GameObject subject, string objectName)
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

    public void CareLessActive()
    {
        // 배열 초기화
        int maxWeapons = 13; // 무기와 아이템의 총 개수
        resultArray = new string[maxWeapons];

        if (DataBaseManager.shotgun > 0)
        {
            resultArray[arrayInt] = "Shotgun";
            arrayInt += 1;
        }
        if (DataBaseManager.rifle > 0)
        {
            resultArray[arrayInt] = "Rifle";
            arrayInt += 1;
        }
        if (DataBaseManager.revolver > 0)
        {
            resultArray[arrayInt] = "Revolver";
            arrayInt += 1;
        }
        if (DataBaseManager.smallPistol > 0)
        {
            resultArray[arrayInt] = "SmallPistol";
            arrayInt += 1;
        }
        if (DataBaseManager.bat > 0)
        {
            resultArray[arrayInt] = "Bat";
            arrayInt += 1;
        }
        if (DataBaseManager.axe > 0)
        {
            resultArray[arrayInt] = "Axe";
            arrayInt += 1;
        }
        if (DataBaseManager.dagger > 0)
        {
            resultArray[arrayInt] = "Dagger";
            arrayInt += 1;
        }

        if (DataBaseManager.molotov > 0)
        {
            resultArray[arrayInt] = "Molotov";
            arrayInt += 1;
        }
        if (DataBaseManager.pistolAmmo > 0)
        {
            resultArray[arrayInt] = "PistolAmmo";
            arrayInt += 1;
        }
        if (DataBaseManager.rifleAmmo > 0)
        {
            resultArray[arrayInt] = "RifleAmmo";
            arrayInt += 1;
        }
        if (DataBaseManager.shotgunAmmo > 0)
        {
            resultArray[arrayInt] = "ShotgunAmmo";
            arrayInt += 1;
        }
        if (DataBaseManager.firstaidkit > 0)
        {
            resultArray[arrayInt] = "Firstaidkit";
            arrayInt += 1;
        }
        if (DataBaseManager.bandages > 0)
        {
            resultArray[arrayInt] = "Bandages";
            arrayInt += 1;
        }
        if (DataBaseManager.painkillers > 0)
        {
            resultArray[arrayInt] = "Painkillers";
            arrayInt += 1;
        }

        int i = Random.Range(0, arrayInt);
        Debug.Log(resultArray[i]);

        if (resultArray[i] == "Shotgun")
        {
            DataBaseManager.shotgun -= 1;
        }
        else if (resultArray[i] == "Rifle")
        {
            DataBaseManager.rifle -= 1;
        }
        else if (resultArray[i] == "Revolver")
        {
            DataBaseManager.revolver -= 1;
        }
        else if (resultArray[i] == "SmallPistol")
        {
            DataBaseManager.smallPistol -= 1;
        }
        else if (resultArray[i] == "Bat")
        {
            DataBaseManager.bat -= 1;
        }
        else if (resultArray[i] == "Axe")
        {
            DataBaseManager.axe -= 1;
        }
        else if (resultArray[i] == "Dagger")
        {
            DataBaseManager.dagger -= 1;
        }
        else if (resultArray[i] == "Molotov")
        {
            DataBaseManager.molotov -= 1;
        }
        else if (resultArray[i] == "PistolAmmo")
        {
            DataBaseManager.pistolAmmo -= 1;
        }
        else if (resultArray[i] == "RifleAmmo")
        {
            DataBaseManager.rifleAmmo -= 1;
        }
        else if (resultArray[i] == "ShotgunAmmo")
        {
            DataBaseManager.shotgunAmmo -= 1;
        }
        else if (resultArray[i] == "ShotgunAmmo")
        {
            DataBaseManager.shotgunAmmo -= 1;
        }
        else if (resultArray[i] == "Firstaidkit")
        {
            DataBaseManager.firstaidkit -= 1;
        }
        else if (resultArray[i] == "Bandages")
        {
            DataBaseManager.bandages -= 1;
        }
        else if (resultArray[i] == "Painkillers")
        {
            DataBaseManager.painkillers -= 1;
        }
    }
    public void ThinWalletsOpen()
    {
        thinWallets.SetActive(true);
    }

    public void CarelessnessOpen()
    {
        carelessness.SetActive(true);
    }

    public void DebilitationOpen()
    {
        debilitation.SetActive(true);
    }

    public void DizzinessOpen()
    {
        dizziness.SetActive(true);
    }
    public void SprainsOpen()
    {
        sprains.SetActive(true);
    }

    public void UnluckyOpen()
    {
        unlucky.SetActive(true);
    }

    public void MusclePainOpen()
    {
        musclePain.SetActive(true);
    }

    public void MigrainesOpen()
    {
        migraines.SetActive(true);
    }

    public void EyeDiseaseOpen()
    {
        eyeDisease.SetActive(true);
    }

    //Level - 2 Debuff

    public void MasochismOpen()
    {
        masochism.SetActive(true);
    }

    public void CarelessOpen()
    {
        careless.SetActive(true);
    }

    public void MentalWeaknessOpen()
    {
        mentalWeakness.SetActive(true);
    }

    public void HelplessnessOpen()
    {
        helplessness.SetActive(true);
    }

    public void ExtravagantOpen()
    {
        extravagant.SetActive(true);
    }

    public void PanicAttackOpen()
    {
        panicAttack.SetActive(true);
    }

    public void MedicaldistrustOpen()
    {
        medicaldistrust.SetActive(true);
    }

    public void HallucinationsOpen()
    {
        hallucinations.SetActive(true);
    }

    public void TightwadOpen()
    {
        tightwad.SetActive(true);
    }
    //Level - 3 Debuff
    public void NightPhobiaOpen()
    {
        nightPhobia.SetActive(true);
    }
    public void DeafnessOpen()
    {
        deafness.SetActive(true);
    }
    public void VisionLossOpen()
    {
        visionLoss.SetActive(true);
    }
    public void ShortTemperedOpen()
    {
        shortTempered.SetActive(true);
    }
    public void DyslexiaOpen()
    {
        dyslexia.SetActive(true);
    }
    public void DrugPhobiaOpen()
    {
        drugPhobia.SetActive(true);
    }
    public void CognitiveDisorderOpen()
    {
        cognitiveDisorder.SetActive(true);
    }
    public void CognitiveBreakdownOpen()
    {
        cognitiveBreakdown.SetActive(true);
    }
    public void HomicidalImpulseOpen()
    {
        homicidalImpulse.SetActive(true);
    }
    public void PerfectionismOpen()
    {
        perfectionism.SetActive(true);
    }
}
