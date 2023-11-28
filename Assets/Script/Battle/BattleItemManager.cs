using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;
public class BattleItemManager : MonoBehaviour
{
    public GameObject itemUse;
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
    public GameObject itemBoxSwords;
    public GameObject itemBoxMarkmes;
    public GameObject itemBoxReload;
    public GameObject itemBoxDeftness;
    public Image imageComponentUse;
    public Image imageComponentKey;
    public Sprite none;


    private static BattleItemManager instance = null;
    public static BattleItemManager Instance
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
    private void Update()
    {
        if (DataBaseManager.battleWeapon == "")
        {
           // Name.text = "";
           // Detail.text = "";
           // Use_Name.text = "";
           // Use_Detail.text = "";
           // Key_Name.text = "";
           // Key_Detail.text = "";
        }
    }
    public void ManageItem()
    {
        if (DataBaseManager.dagger > 0)
        {
            ActivateObjectWithName(itemBoxSwords, "Dagger");
        }
        else
        {
            DeactivateObjectWithName(itemBoxSwords, "Dagger");
        }
        if (DataBaseManager.bat > 0)
        {
            ActivateObjectWithName(itemBoxSwords, "Bat");
        }
        else
        {
            DeactivateObjectWithName(itemBoxSwords, "Bat");
        }
        if (DataBaseManager.axe > 0)
        {
            ActivateObjectWithName(itemBoxSwords, "Axe");
        }
        else
        {
            DeactivateObjectWithName(itemBoxSwords, "Axe");
        }
        if (DataBaseManager.shotgun > 0 && DataBaseManager.nowShotgun > 0)
        {
            ActivateObjectWithName(itemBoxMarkmes, "Shotgun");
        }
        else
        {
            DeactivateObjectWithName(itemBoxMarkmes, "Shotgun");
        }
        if (DataBaseManager.rifle > 0 && DataBaseManager.nowRifle > 0)
        {
            ActivateObjectWithName(itemBoxMarkmes, "Rifle");
        }
        else
        {
            DeactivateObjectWithName(itemBoxMarkmes, "Rifle");
        }
        if (DataBaseManager.revolver > 0 && DataBaseManager.nowRevolver > 0)
        {
            ActivateObjectWithName(itemBoxMarkmes, "Revolver");
        }
        else
        {
            DeactivateObjectWithName(itemBoxMarkmes, "Revolver");
        }
        if (DataBaseManager.smallPistol > 0 && DataBaseManager.nowSmallPistol > 0)
        {
            ActivateObjectWithName(itemBoxMarkmes, "SmallPistol");
        }
        else
        {
            DeactivateObjectWithName(itemBoxMarkmes, "SmallPistol");
        }
        if (DataBaseManager.shotgun > 0 && DataBaseManager.nowShotgun < 2 && DataBaseManager.shotgunAmmo > 0)
        {
            ActivateObjectWithName(itemBoxReload, "Shotgun");
        }
        else
        {
            DeactivateObjectWithName(itemBoxReload, "Shotgun");
        }
        if (DataBaseManager.rifle > 0 && DataBaseManager.nowRifle < 5 && DataBaseManager.rifleAmmo > 0)
        {
            ActivateObjectWithName(itemBoxReload, "Rifle");
        }
        else
        {
            DeactivateObjectWithName(itemBoxReload, "Rifle");
        }
        if (DataBaseManager.revolver > 0 && DataBaseManager.nowRevolver < 6 && DataBaseManager.pistolAmmo > 0)
        {
            ActivateObjectWithName(itemBoxReload, "Revolver");
        }
        else
        {
            DeactivateObjectWithName(itemBoxReload, "Revolver");
        }
        if (DataBaseManager.smallPistol > 0 && DataBaseManager.nowSmallPistol < 2 && DataBaseManager.pistolAmmo > 0)
        {
            ActivateObjectWithName(itemBoxReload, "SmallPistol");
        }
        else
        {
            DeactivateObjectWithName(itemBoxReload, "SmallPistol");
        }
        if (DataBaseManager.molotov > 0)
        {
            ActivateObjectWithName(itemBoxDeftness, "Molotov");
        }
        else
        {
            DeactivateObjectWithName(itemBoxDeftness, "Molotov");
        }
    }
    public void OpenItemDetail()
    {
        if (itemBoxSwords.activeSelf == true)
        {
            if (DataBaseManager.battleWeapon == "")
            {
                name.text = "";
                detail.text = "";
            }
            if (DataBaseManager.battleWeapon == "Bat")
            {
                name.text = "Bat";
                detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D8)x5";
            }
            if (DataBaseManager.battleWeapon == "Dagger")
            {
                name.text = "Dagger";
                detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D4 + 2)x5";
            }
            if (DataBaseManager.battleWeapon == "Axe")
            {
                name.text = "Axe";
                detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D6 + 2)x5";
            }

            if (DataBaseManager.battleWeapon == "Molotov")
            {
                name.text = "Molotov";
                detail.text = "Skill :\nDeftness\n\nDamage :\n(2D6 + 2)x5\n\nPcs : "+DataBaseManager.molotov;
            }

            if (DataBaseManager.battleWeapon == "Shotgun")
            {
                name.text = "Shotgun";
                detail.text = "Skill :\nMarkmanship\n\nDamage :\n(5D4)x5\n\nMagazine :\n2 Bullet";
            }
            if (DataBaseManager.battleWeapon == "Rifle")
            {
                name.text = "Rifle";
                detail.text = "Skill :\nMarkmanship\n\nDamage :\n(2D6 + 2)x5\n\nMagazine :\n5 Bullet";
            }
            if (DataBaseManager.battleWeapon == "Revolver")
            {
                name.text = "Revolver";
                detail.text = "Skill :\nMarkmanship\n\nDamage :\n(1D10 + 2)x5\n\nMagazine :\n6 Bullet";
            }
            if (DataBaseManager.battleWeapon == "SmallPistol")
            {
                name.text = "Small Pistol";
                detail.text = "Skill :\nMarkmanship\n\nDamage :\n(1D6)x5\n\nMagazine :\n2 Bullet";
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
