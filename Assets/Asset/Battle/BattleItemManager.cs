using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleItemManager : MonoBehaviour
{



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

    public GameObject ItemBox_Swords;
    public GameObject ItemBox_Markmes;
    public GameObject ItemBox_Reload;
    public GameObject ItemBox_Deftness;



    public Image imageComponent_Use;
    public Image imageComponent_Key;
    public Sprite None;



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


        if (DataBaseManager.BattleWeapon == "")
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
        if (DataBaseManager.Dagger > 0)
        {
            ActivateObjectWithName(ItemBox_Swords, "Dagger");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Swords, "Dagger");
        }

        if (DataBaseManager.Bat > 0)
        {
            ActivateObjectWithName(ItemBox_Swords, "Bat");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Swords, "Bat");
        }
        if (DataBaseManager.Axe > 0)
        {
            ActivateObjectWithName(ItemBox_Swords, "Axe");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Swords, "Axe");
        }

        if (DataBaseManager.Shotgun > 0 && DataBaseManager.nowShotgun > 0)
        {
            ActivateObjectWithName(ItemBox_Markmes, "Shotgun");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Markmes, "Shotgun");
        }
        if (DataBaseManager.Rifle > 0 && DataBaseManager.nowRifle > 0)
        {
            ActivateObjectWithName(ItemBox_Markmes, "Rifle");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Markmes, "Rifle");
        }
        if (DataBaseManager.Revolver > 0 && DataBaseManager.nowRevolver > 0)
        {
            ActivateObjectWithName(ItemBox_Markmes, "Revolver");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Markmes, "Revolver");
        }
        if (DataBaseManager.SmallPistol > 0 && DataBaseManager.nowSmallPistol > 0)
        {
            ActivateObjectWithName(ItemBox_Markmes, "SmallPistol");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Markmes, "SmallPistol");
        }




        if (DataBaseManager.Shotgun > 0 && DataBaseManager.nowShotgun < 2 && DataBaseManager.ShotgunAmmo > 0)
        {
            ActivateObjectWithName(ItemBox_Reload, "Shotgun");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Reload, "Shotgun");
        }
        if (DataBaseManager.Rifle > 0 && DataBaseManager.nowRifle < 2 && DataBaseManager.RifleAmmo > 0)
        {
            ActivateObjectWithName(ItemBox_Reload, "Rifle");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Reload, "Rifle");
        }
        if (DataBaseManager.Revolver > 0 && DataBaseManager.nowRevolver < 2 && DataBaseManager.PistolAmmo > 0)
        {
            ActivateObjectWithName(ItemBox_Reload, "Revolver");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Reload, "Revolver");
        }
        if (DataBaseManager.SmallPistol > 0 && DataBaseManager.nowSmallPistol < 2 && DataBaseManager.PistolAmmo > 0)
        {
            ActivateObjectWithName(ItemBox_Reload, "SmallPistol");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Reload, "SmallPistol");
        }




        if (DataBaseManager.Molotov > 0)
        {
            ActivateObjectWithName(ItemBox_Deftness, "Molotov");
        }
        else
        {
            DeactivateObjectWithName(ItemBox_Deftness, "Molotov");
        }

     

    }


    public void OpenItemDetail()
    {
        if (ItemBox_Swords.activeSelf == true)
        {
            if (DataBaseManager.BattleWeapon == "")
            {
                Name.text = "";
                Detail.text = "";
            }
            if (DataBaseManager.BattleWeapon == "Bat")
            {
                Name.text = "Bat";
                Detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D8)x5";
            }
            if (DataBaseManager.BattleWeapon == "Dagger")
            {
                Name.text = "Dagger";
                Detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D4 + 2)x5";
            }
            if (DataBaseManager.BattleWeapon == "Axe")
            {
                Name.text = "Axe";
                Detail.text = "Skill :\nSwordsmanship\n\nDamage :\n(1D6 + 2)x5";
            }

            if (DataBaseManager.BattleWeapon == "Molotov")
            {
                Name.text = "Molotov";
                Detail.text = "Skill :\nDeftness\n\nDamage :\n(2D6 + 2)x5\n\nPcs : "+DataBaseManager.Molotov;
            }

            if (DataBaseManager.BattleWeapon == "Shotgun")
            {
                Name.text = "Shotgun";
                Detail.text = "Skill :\nMarkmanship\n\nDamage :\n(5D4)x5\n\nMagazine :\n2 Bullet";
            }
            if (DataBaseManager.BattleWeapon == "Rifle")
            {
                Name.text = "Rifle";
                Detail.text = "Skill :\nMarkmanship\n\nDamage :\n(2D6 + 2)x5\n\nMagazine :\n5 Bullet";
            }
            if (DataBaseManager.BattleWeapon == "Revolver")
            {
                Name.text = "Revolver";
                Detail.text = "Skill :\nMarkmanship\n\nDamage :\n(1D10 + 2)x5\n\nMagazine :\n6 Bullet";
            }
            if (DataBaseManager.BattleWeapon == "SmallPistol")
            {
                Name.text = "Small Pistol";
                Detail.text = "Skill :\nMarkmanship\n\nDamage :\n(1D6)x5\n\nMagazine :\n2 Bullet";
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
