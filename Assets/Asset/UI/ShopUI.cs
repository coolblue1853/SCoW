using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopUI : MonoBehaviour
{
    public GameObject ShopUI_object;

    public GameObject Firearm_Page;
    public GameObject Ammo_Page;
    public GameObject Swords_Page;
    public GameObject Throwable_Page;
    public GameObject AidKit_Page;

    public GameObject SO_SmallPistol;
    public GameObject SO_Revolver;
    public GameObject SO_Shotgun;
    public GameObject SO_Rifle;

    public GameObject SO_Dagger;
    public GameObject SO_Axe;
    public GameObject SO_Bat;

    public Text WealthText;

    public Text PistolAmmoText;
    public Text RifleAmmoText;
    public Text ShotgunAmmoText;
    public Text MolotovText;
    public Text BandageText;
    public Text AidKitText;
    public Text PainkillerText;

    private static ShopUI instance = null;
    public static ShopUI Instance
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ShopUI_object.activeSelf == true)
        {
            DataBaseManager.isOpenUi = false;
            ShopUI_object.SetActive(false);
        }
        if(DataBaseManager.SmallPistol > 0 && SO_SmallPistol.activeSelf == false)
        {
            SO_SmallPistol.SetActive(true);
        }
        if(ShopUI_object.activeSelf == true)
        {
            WealthText.text = "wealth : " + DataBaseManager.weal;
            PistolAmmoText.text = "Hold:"+DataBaseManager.PistolAmmo+"Pcs";
            RifleAmmoText.text = "Hold:" + DataBaseManager.RifleAmmo + "Pcs";
            ShotgunAmmoText.text = "Hold:" + DataBaseManager.ShotgunAmmo + "Pcs";
            MolotovText.text = "Hold:" + DataBaseManager.Molotov + "Pcs";
            BandageText.text = "Hold:" + DataBaseManager.Bandages + "Pcs";
            AidKitText.text = "Hold:" + DataBaseManager.Firstaidkit + "Pcs";
            PainkillerText.text = "Hold:" + DataBaseManager.Painkillers + "Pcs";
        }

    }

    //총기류 구매 버튼
    public void BuySmallPistol()
    {
        if(DataBaseManager.weal >= 10 && DataBaseManager.SmallPistol == 0)
        {
            DataBaseManager.weal -= 10;
            DataBaseManager.SmallPistol = 1;
            SO_SmallPistol.SetActive(true);
        }
    }
    public void BuyRevolver()
    {
        if (DataBaseManager.weal >= 12 && DataBaseManager.Revolver == 0)
        {
            DataBaseManager.weal -= 12;
            DataBaseManager.Revolver = 1;
            SO_Revolver.SetActive(true);
        }
    }
    public void BuyRifle()
    {
        if (DataBaseManager.weal >= 20 && DataBaseManager.Rifle == 0)
        {
            DataBaseManager.weal -= 20;
            DataBaseManager.Rifle = 1;
            SO_Rifle.SetActive(true);
        }
    }
    public void BuyShotgun()
    {
        if (DataBaseManager.weal >= 18 && DataBaseManager.Shotgun == 0)
        {
            DataBaseManager.weal -= 18;
            DataBaseManager.Shotgun = 1;
            SO_Shotgun.SetActive(true);
        }
    }

    public void BuyPistolAmmo()
    {
        if (DataBaseManager.weal >= 2)
        {
            DataBaseManager.weal -= 2;
            DataBaseManager.PistolAmmo += 12;
        }
    }
    public void BuyRifleAmmo()
    {
        if (DataBaseManager.weal >= 2)
        {
            DataBaseManager.weal -= 2;
            DataBaseManager.RifleAmmo += 5;
        }
    }
    public void BuyShotgunAmmo()
    {
        if (DataBaseManager.weal >= 4)
        {
            DataBaseManager.weal -= 4;
            DataBaseManager.ShotgunAmmo += 8;
        }
    }

    public void BuyBat()
    {
        if (DataBaseManager.weal >= 5 && DataBaseManager.Bat == 0)
        {
            DataBaseManager.weal -= 5;
            DataBaseManager.Bat = 1;
            SO_Bat.SetActive(true);
        }
    }
    public void BuyDagger()
    {
        if (DataBaseManager.weal >= 8 && DataBaseManager.Dagger == 0)
        {
            DataBaseManager.weal -= 8;
            DataBaseManager.Dagger = 1;
            SO_Dagger.SetActive(true);
        }
    }
    public void BuyAxe()
    {
        if (DataBaseManager.weal >= 10 && DataBaseManager.Axe == 0)
        {
            DataBaseManager.weal -= 10;
            DataBaseManager.Axe = 1;
            SO_Axe.SetActive(true);
        }
    }

    public void BuyMolotov()
    {
        if (DataBaseManager.weal >= 2)
        {
            DataBaseManager.weal -= 2;
            DataBaseManager.Molotov += 1;
        }
    }

    public void BuyBandage()
    {
        if (DataBaseManager.weal >= 2)
        {
            DataBaseManager.weal -= 2;
            DataBaseManager.Bandages += 1;
        }
    }
    public void BuyAidkit()
    {
        if (DataBaseManager.weal >= 5)
        {
            DataBaseManager.weal -= 5;
            DataBaseManager.Firstaidkit += 1;
        }
    }
    public void BuyPainkiller()
    {
        if (DataBaseManager.weal >= 3)
        {
            DataBaseManager.weal -= 3;
            DataBaseManager.Painkillers += 1;
        }
    }


    //상점페이지 열기
    public void OpenPage_Firearm()
    {
      Firearm_Page.SetActive(true);
      Ammo_Page.SetActive(false);
      Swords_Page.SetActive(false);
      Throwable_Page.SetActive(false);
      AidKit_Page.SetActive(false);
    }
    public void OpenPage_Ammo()
    {
        Firearm_Page.SetActive(false);
        Ammo_Page.SetActive(true);
        Swords_Page.SetActive(false);
        Throwable_Page.SetActive(false);
        AidKit_Page.SetActive(false);
    }
    public void OpenPage_Sowrds()
    {
        Firearm_Page.SetActive(false);
        Ammo_Page.SetActive(false);
        Swords_Page.SetActive(true);
        Throwable_Page.SetActive(false);
        AidKit_Page.SetActive(false);
    }
    public void OpenPage_AidKit()
    {
        Firearm_Page.SetActive(false);
        Ammo_Page.SetActive(false);
        Swords_Page.SetActive(false);
        Throwable_Page.SetActive(false);
        AidKit_Page.SetActive(true);
    }
    public void OpenPage_Throwable()
    {
        Firearm_Page.SetActive(false);
        Ammo_Page.SetActive(false);
        Swords_Page.SetActive(false);
        Throwable_Page.SetActive(true);
        AidKit_Page.SetActive(false);
    }
    public void OpenShopUI()
    {
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false)
        {
            DataBaseManager.workSound = false;
            DataBaseManager.isOpenUi = true;
            ShopUI_object.SetActive(true);
        }

    }
    public void CloseShop()
    {
        DataBaseManager.isOpenUi = false;
        ShopUI_object.SetActive(false);
    }
}
