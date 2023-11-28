using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopUI : MonoBehaviour
{
    public GameObject shopUIOb;
    public GameObject fireArmPage;
    public GameObject ammoPage;
    public GameObject swordsPage;
    public GameObject throwablePage;
    public GameObject aidKitPage;
    public GameObject smallPistolSoldOut;
    public GameObject revolverSoldOut;
    public GameObject shotgunSoldOut;
    public GameObject rifleSoldOut;
    public GameObject daggerSoldOut;
    public GameObject axeSoldOut;
    public GameObject batSoldOut;
    public Text wealthTxt;
    public Text pistolAmmoTxt;
    public Text rifleAmmoTxt;
    public Text shotgunAmmoTxt;
    public Text molotovTxt;
    public Text bandageTxt;
    public Text aidKitTxt;
    public Text painkillerTxt;
    public Text smallPistolPrice;
    public Text revolverPrice;
    public Text shotgunPrice;
    public Text riflePrice;
    public Text daggerPrice;
    public Text axePrice;
    public Text batPrice;
    public Text pistolAmmoPrice;
    public Text rifleAmmoPrice;
    public Text shotgunAmmoPrice;
    public Text molotovPrice;
    public Text bandagePrice;
    public Text aidKitPrice;
    public Text painkillerPrice;

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
        if (Input.GetKeyDown(KeyCode.Escape) && shopUIOb.activeSelf == true)
        {
            SoundManager.Instance.ClickSound_Play();
            DataBaseManager.isOpenUi = false;
            shopUIOb.SetActive(false);
        }
        if (DataBaseManager.smallPistol > 0 && smallPistolSoldOut.activeSelf == false)
        {
            smallPistolSoldOut.SetActive(true);
        }
        if (shopUIOb.activeSelf == true)
        {
            wealthTxt.text = "wealth : " + DataBaseManager.weal;
            pistolAmmoTxt.text = "Hold:" + DataBaseManager.pistolAmmo + "Pcs";
            rifleAmmoTxt.text = "Hold:" + DataBaseManager.rifleAmmo + "Pcs";
            shotgunAmmoTxt.text = "Hold:" + DataBaseManager.shotgunAmmo + "Pcs";
            molotovTxt.text = "Hold:" + DataBaseManager.molotov + "Pcs";
            bandageTxt.text = "Hold:" + DataBaseManager.bandages + "Pcs";
            aidKitTxt.text = "Hold:" + DataBaseManager.firstaidkit + "Pcs";
            painkillerTxt.text = "Hold:" + DataBaseManager.painkillers + "Pcs";
        }
    }
    public void ExtravoltON()
    {
        smallPistolPrice.text = "Price : 12$";
        revolverPrice.text = "Price : 14$";
        shotgunPrice.text = "Price : 20$";
        riflePrice.text = "Price : 22$";
        daggerPrice.text = "Price : 10$";
        axePrice.text = "Price : 12$";
        batPrice.text = "Price : 7$";
        pistolAmmoPrice.text = "Price : 3$";
        rifleAmmoPrice.text = "Price : 3$";
        shotgunAmmoPrice.text = "Price : 5$";
        molotovPrice.text = "Price : 4$";
        bandagePrice.text = "Price : 3$";
        aidKitPrice.text = "Price : 6$";
        painkillerPrice.text = "Price : 4$";
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
                smallPistolSoldOut.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 10 && DataBaseManager.smallPistol == 0)
            {
                DataBaseManager.weal -= 10;
                DataBaseManager.smallPistol = 1;
                smallPistolSoldOut.SetActive(true);
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
                revolverSoldOut.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 12 && DataBaseManager.revolver == 0)
            {
                DataBaseManager.weal -= 12;
                DataBaseManager.revolver = 1;
                revolverSoldOut.SetActive(true);
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
                rifleSoldOut.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 20 && DataBaseManager.rifle == 0)
            {
                DataBaseManager.weal -= 20;
                DataBaseManager.rifle = 1;
                rifleSoldOut.SetActive(true);
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
                shotgunSoldOut.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 18 && DataBaseManager.shotgun == 0)
            {
                DataBaseManager.weal -= 18;
                DataBaseManager.shotgun = 1;
                shotgunSoldOut.SetActive(true);
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
                batSoldOut.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 5 && DataBaseManager.bat == 0)
            {
                DataBaseManager.weal -= 5;
                DataBaseManager.bat = 1;
                batSoldOut.SetActive(true);
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
                daggerSoldOut.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 8 && DataBaseManager.dagger == 0)
            {
                DataBaseManager.weal -= 8;
                DataBaseManager.dagger = 1;
                daggerSoldOut.SetActive(true);
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
                axeSoldOut.SetActive(true);
            }
        }
        else
        {
            if (DataBaseManager.weal >= 10 && DataBaseManager.axe == 0)
            {
                DataBaseManager.weal -= 10;
                DataBaseManager.axe = 1;
                axeSoldOut.SetActive(true);
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
        fireArmPage.SetActive(true);
        ammoPage.SetActive(false);
        swordsPage.SetActive(false);
        throwablePage.SetActive(false);
        aidKitPage.SetActive(false);
    }
    public void OpenPage_Ammo()
    {
        SoundManager.Instance.ClickSound_Play();
        fireArmPage.SetActive(false);
        ammoPage.SetActive(true);
        swordsPage.SetActive(false);
        throwablePage.SetActive(false);
        aidKitPage.SetActive(false);
    }
    public void OpenPage_Sowrds()
    {
        SoundManager.Instance.ClickSound_Play();
        fireArmPage.SetActive(false);
        ammoPage.SetActive(false);
        swordsPage.SetActive(true);
        throwablePage.SetActive(false);
        aidKitPage.SetActive(false);
    }
    public void OpenPage_AidKit()
    {
        SoundManager.Instance.ClickSound_Play();
        fireArmPage.SetActive(false);
        ammoPage.SetActive(false);
        swordsPage.SetActive(false);
        throwablePage.SetActive(false);
        aidKitPage.SetActive(true);
    }
    public void OpenPage_Throwable()
    {
        SoundManager.Instance.ClickSound_Play();
        fireArmPage.SetActive(false);
        ammoPage.SetActive(false);
        swordsPage.SetActive(false);
        throwablePage.SetActive(true);
        aidKitPage.SetActive(false);
    }
    public void OpenShopUI()
    {
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false)
        {
            SoundManager.Instance.PaperClip_Play();
            DataBaseManager.workSound = false;
            DataBaseManager.isOpenUi = true;
            shopUIOb.SetActive(true);
        }
    }
    public void CloseShop()
    {
        DataBaseManager.isOpenUi = false;
        shopUIOb.SetActive(false);
    }
}
