using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleItemManager : MonoBehaviour
{
    public GameObject ItemUse;
    public GameObject ItemKey;
    public GameObject ItemEqip;

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
    public void ManageItem()
    {
        if (DataBaseManager.Dagger > 0)
        {
            ActivateObjectWithName(itemBoxSwords, "Dagger");
        }
        else
        {
            DeactivateObjectWithName(itemBoxSwords, "Dagger");
        }
        if (DataBaseManager.Bat > 0)
        {
            ActivateObjectWithName(itemBoxSwords, "Bat");
        }
        else
        {
            DeactivateObjectWithName(itemBoxSwords, "Bat");
        }
        if (DataBaseManager.Axe > 0)
        {
            ActivateObjectWithName(itemBoxSwords, "Axe");
        }
        else
        {
            DeactivateObjectWithName(itemBoxSwords, "Axe");
        }
        if (DataBaseManager.Shotgun > 0 && DataBaseManager.nowShotgun > 0)
        {
            ActivateObjectWithName(itemBoxMarkmes, "Shotgun");
        }
        else
        {
            DeactivateObjectWithName(itemBoxMarkmes, "Shotgun");
        }
        if (DataBaseManager.Rifle > 0 && DataBaseManager.nowRifle > 0)
        {
            ActivateObjectWithName(itemBoxMarkmes, "Rifle");
        }
        else
        {
            DeactivateObjectWithName(itemBoxMarkmes, "Rifle");
        }
        if (DataBaseManager.Revolver > 0 && DataBaseManager.nowRevolver > 0)
        {
            ActivateObjectWithName(itemBoxMarkmes, "Revolver");
        }
        else
        {
            DeactivateObjectWithName(itemBoxMarkmes, "Revolver");
        }
        if (DataBaseManager.SmallPistol > 0 && DataBaseManager.nowSmallPistol > 0)
        {
            ActivateObjectWithName(itemBoxMarkmes, "SmallPistol");
        }
        else
        {
            DeactivateObjectWithName(itemBoxMarkmes, "SmallPistol");
        }
        if (DataBaseManager.Shotgun > 0 && DataBaseManager.nowShotgun < 2 && DataBaseManager.ShotgunAmmo > 0)
        {
            ActivateObjectWithName(itemBoxReload, "Shotgun");
        }
        else
        {
            DeactivateObjectWithName(itemBoxReload, "Shotgun");
        }
        if (DataBaseManager.Rifle > 0 && DataBaseManager.nowRifle < 5 && DataBaseManager.RifleAmmo > 0)
        {
            ActivateObjectWithName(itemBoxReload, "Rifle");
        }
        else
        {
            DeactivateObjectWithName(itemBoxReload, "Rifle");
        }
        if (DataBaseManager.Revolver > 0 && DataBaseManager.nowRevolver < 6 && DataBaseManager.PistolAmmo > 0)
        {
            ActivateObjectWithName(itemBoxReload, "Revolver");
        }
        else
        {
            DeactivateObjectWithName(itemBoxReload, "Revolver");
        }
        if (DataBaseManager.SmallPistol > 0 && DataBaseManager.nowSmallPistol < 2 && DataBaseManager.PistolAmmo > 0)
        {
            ActivateObjectWithName(itemBoxReload, "SmallPistol");
        }
        else
        {
            DeactivateObjectWithName(itemBoxReload, "SmallPistol");
        }
        if (DataBaseManager.Molotov > 0)
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
            if (DataBaseManager.BattleWeapon == "")
            {
                name.text = "";
                detail.text = "";
            }
            if (DataBaseManager.BattleWeapon == "Bat")
            {
                name.text = "Bat";
                detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D8)x5";
            }
            if (DataBaseManager.BattleWeapon == "Dagger")
            {
                name.text = "Dagger";
                detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D4 + 2)x5";
            }
            if (DataBaseManager.BattleWeapon == "Axe")
            {
                name.text = "Axe";
                detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D6 + 2)x5";
            }
            if (DataBaseManager.BattleWeapon == "Molotov")
            {
                name.text = "Molotov";
                detail.text = "Skill :\nDeftness\n\nDamage :\n(2D6 + 2)x5\n\nPcs : "+DataBaseManager.Molotov;
            }
            if (DataBaseManager.BattleWeapon == "Shotgun")
            {
                name.text = "Shotgun";
                detail.text = "Skill :\nMarkmanship\n\nDamage :\n(5D4)x5\n\nMagazine :\n2 Bullet";
            }
            if (DataBaseManager.BattleWeapon == "Rifle")
            {
                name.text = "Rifle";
                detail.text = "Skill :\nMarkmanship\n\nDamage :\n(2D6 + 2)x5\n\nMagazine :\n5 Bullet";
            }
            if (DataBaseManager.BattleWeapon == "Revolver")
            {
                name.text = "Revolver";
                detail.text = "Skill :\nMarkmanship\n\nDamage :\n(1D10 + 2)x5\n\nMagazine :\n6 Bullet";
            }
            if (DataBaseManager.BattleWeapon == "SmallPistol")
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
