using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject ShopUI_object;

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
