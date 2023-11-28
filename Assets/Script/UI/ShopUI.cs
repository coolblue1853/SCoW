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

    public Text SmallPistolPrice;
    public Text RevolverPrice;
    public Text ShotgunPrice;
    public Text RiflePrice;
    public Text DaggerPrice;
    public Text AxePrice;
    public Text BatPrice;
    public Text PistolAmmoPrice;
    public Text RifleAmmoPrice;
    public Text ShotgunAmmoPrice;
    public Text MolotovPrice;
    public Text BandagePrice;
    public Text AidKitPrice;
    public Text PainkillerPrice;

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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ShopUI_object.activeSelf == true)
        {
            SoundManager.Instance.ClickSound_Play();
            DataBaseManager.isOpenUi = false;
            ShopUI_object.SetActive(false);
        }
        if (DataBaseManager.smallPistol > 0 && SO_SmallPistol.activeSelf == false)
        {
            SO_SmallPistol.SetActive(true);
        }
        if (ShopUI_object.activeSelf == true)
        {
            WealthText.text = "wealth : " + DataBaseManager.weal;
            PistolAmmoText.text = "Hold:" + DataBaseManager.pistolAmmo + "Pcs";
            RifleAmmoText.text = "Hold:" + DataBaseManager.rifleAmmo + "Pcs";
            ShotgunAmmoText.text = "Hold:" + DataBaseManager.shotgunAmmo + "Pcs";
            MolotovText.text = "Hold:" + DataBaseManager.molotov + "Pcs";
            BandageText.text = "Hold:" + DataBaseManager.bandages + "Pcs";
            AidKitText.text = "Hold:" + DataBaseManager.firstaidkit + "Pcs";
            PainkillerText.text = "Hold:" + DataBaseManager.painkillers + "Pcs";
        }
    }
    public void ExtravoltON()
    {
        SmallPistolPrice.text = "Price : 12$";
        RevolverPrice.text = "Price : 14$";
        ShotgunPrice.text = "Price : 20$";
        RiflePrice.text = "Price : 22$";
        DaggerPrice.text = "Price : 10$";
        AxePrice.text = "Price : 12$";
        BatPrice.text = "Price : 7$";
        PistolAmmoPrice.text = "Price : 3$";
        RifleAmmoPrice.text = "Price : 3$";
        ShotgunAmmoPrice.text = "Price : 5$";
        MolotovPrice.text = "Price : 4$";
        BandagePrice.text = "Price : 3$";
        AidKitPrice.text = "Price : 6$";
        PainkillerPrice.text = "Price : 4$";
    }
    public void BuySmallPistol()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 12 && DataBaseManager.smallPistol == 0)
            {
                DataBaseManager.weal -= 12;
                DataBaseManager.smallPistol = 1;
                SO_SmallPistol.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 10 && DataBaseManager.smallPistol == 0)
            {
                DataBaseManager.weal -= 10;
                DataBaseManager.smallPistol = 1;
                SO_SmallPistol.SetActive(true);
            }
        }
    }
    public void BuyRevolver()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 14 && DataBaseManager.revolver == 0)
            {
                DataBaseManager.weal -= 14;
                DataBaseManager.revolver = 1;
                SO_Revolver.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 12 && DataBaseManager.revolver == 0)
            {
                DataBaseManager.weal -= 12;
                DataBaseManager.revolver = 1;
                SO_Revolver.SetActive(true);
            }
        }
    }
    public void BuyRifle()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 22 && DataBaseManager.rifle == 0)
            {
                DataBaseManager.weal -= 22;
                DataBaseManager.rifle = 1;
                SO_Rifle.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 20 && DataBaseManager.rifle == 0)
            {
                DataBaseManager.weal -= 20;
                DataBaseManager.rifle = 1;
                SO_Rifle.SetActive(true);
            }
        }
    }
    public void BuyShotgun()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 20 && DataBaseManager.shotgun == 0)
            {
                DataBaseManager.weal -= 20;
                DataBaseManager.shotgun = 1;
                SO_Shotgun.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 18 && DataBaseManager.shotgun == 0)
            {
                DataBaseManager.weal -= 18;
                DataBaseManager.shotgun = 1;
                SO_Shotgun.SetActive(true);
            }
        }
    }
    public void BuyPistolAmmo()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 3)
            {
                DataBaseManager.weal -= 3;
                DataBaseManager.pistolAmmo += 12;
            }
        }
        else
        {
            if (DataBaseManager.weal >= 2)
            {
                DataBaseManager.weal -= 2;
                DataBaseManager.pistolAmmo += 12;
            }
        }
    }
    public void BuyRifleAmmo()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 3)
            {
                DataBaseManager.weal -= 3;
                DataBaseManager.rifleAmmo += 5;
            }
        }
        else
        {
            if (DataBaseManager.weal >= 2)
            {
                DataBaseManager.weal -= 2;
                DataBaseManager.rifleAmmo += 5;
            }
        }
    }
    public void BuyShotgunAmmo()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 5)
            {
                DataBaseManager.weal -= 5;
                DataBaseManager.shotgunAmmo += 8;
            }
        }
        else
        {

            if (DataBaseManager.weal >= 4)
            {
                DataBaseManager.weal -= 4;
                DataBaseManager.shotgunAmmo += 8;
            }
        }
    }
    public void BuyBat()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 7 && DataBaseManager.bat == 0)
            {
                DataBaseManager.weal -= 7;
                DataBaseManager.bat = 1;
                SO_Bat.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 5 && DataBaseManager.bat == 0)
            {
                DataBaseManager.weal -= 5;
                DataBaseManager.bat = 1;
                SO_Bat.SetActive(true);
            }
        }
    }
    public void BuyDagger()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 10 && DataBaseManager.dagger == 0)
            {
                DataBaseManager.weal -= 10;
                DataBaseManager.dagger = 1;
                SO_Dagger.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 8 && DataBaseManager.dagger == 0)
            {
                DataBaseManager.weal -= 8;
                DataBaseManager.dagger = 1;
                SO_Dagger.SetActive(true);
            }
        }
    }
    public void BuyAxe()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 12 && DataBaseManager.axe == 0)
            {
                DataBaseManager.weal -= 12;
                DataBaseManager.axe = 1;
                SO_Axe.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 10 && DataBaseManager.axe == 0)
            {
                DataBaseManager.weal -= 10;
                DataBaseManager.axe = 1;
                SO_Axe.SetActive(true);
            }
        }
    }
    public void BuyMolotov()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 4)
            {
                DataBaseManager.weal -= 4;
                DataBaseManager.molotov += 1;
            }
        }
        else
        {
            if (DataBaseManager.weal >= 2)
            {
                DataBaseManager.weal -= 2;
                DataBaseManager.molotov += 1;
            }
        }
    }
    public void BuyBandage()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 3)
            {
                DataBaseManager.weal -= 3;
                DataBaseManager.bandages += 1;
            }
        }
        else
        {
            if (DataBaseManager.weal >= 2)
            {
                DataBaseManager.weal -= 2;
                DataBaseManager.bandages += 1;
            }
        }
    }
    public void BuyAidkit()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 6)
            {
                DataBaseManager.weal -= 6;
                DataBaseManager.firstaidkit += 1;
            }
        }
        else
        {
            if (DataBaseManager.weal >= 5)
            {
                DataBaseManager.weal -= 5;
                DataBaseManager.firstaidkit += 1;
            }
        }
    }
    public void BuyPainkiller()
    {
        SoundManager.Instance.ClickSound_Play();
        if (DataBaseManager.extravagant == true)
        {
            if (DataBaseManager.weal >= 4)
            {
                DataBaseManager.weal -= 4;
                DataBaseManager.painkillers += 1;
            }
        }
        else
        {
            if (DataBaseManager.weal >= 3)
            {
                DataBaseManager.weal -= 3;
                DataBaseManager.painkillers += 1;
            }
        }
    }
    //상점페이지 열기
    public void OpenPage_Firearm()
    {
        SoundManager.Instance.ClickSound_Play();
        Firearm_Page.SetActive(true);
        Ammo_Page.SetActive(false);
        Swords_Page.SetActive(false);
        Throwable_Page.SetActive(false);
        AidKit_Page.SetActive(false);
    }
    public void OpenPage_Ammo()
    {
        SoundManager.Instance.ClickSound_Play();
        Firearm_Page.SetActive(false);
        Ammo_Page.SetActive(true);
        Swords_Page.SetActive(false);
        Throwable_Page.SetActive(false);
        AidKit_Page.SetActive(false);
    }
    public void OpenPage_Sowrds()
    {
        SoundManager.Instance.ClickSound_Play();
        Firearm_Page.SetActive(false);
        Ammo_Page.SetActive(false);
        Swords_Page.SetActive(true);
        Throwable_Page.SetActive(false);
        AidKit_Page.SetActive(false);
    }
    public void OpenPage_AidKit()
    {
        SoundManager.Instance.ClickSound_Play();
        Firearm_Page.SetActive(false);
        Ammo_Page.SetActive(false);
        Swords_Page.SetActive(false);
        Throwable_Page.SetActive(false);
        AidKit_Page.SetActive(true);
    }
    public void OpenPage_Throwable()
    {
        SoundManager.Instance.ClickSound_Play();
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
            SoundManager.Instance.PaperClip_Play();
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
