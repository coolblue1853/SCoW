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
        WealthText.text = "wealth : " + DataBaseManager.weal;
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
