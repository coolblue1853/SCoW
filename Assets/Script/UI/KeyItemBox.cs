using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KeyItemBox : MonoBehaviour
{
    Image imageComponent;
    public Sprite safe;
    public Sprite bracelet;
    public Sprite coat;
    public Sprite sewergeKey;
    public Sprite document;
    public Sprite none;
    public Sprite map;
    public Sprite oldMap;
    public GameObject sisUI;
    public TextMeshProUGUI useUIText;
    public GameObject useButton;
    private void Start()
    {
        imageComponent = this.GetComponent<Image>();
    }
    public void ClickYes()
    {
        DataBaseManager.isOpenUi = false;

        sisUI.SetActive(false);
        if (DataBaseManager.nowItem == "First aid kit")
        {
            Rollet.Instance.SetRollet("Fabian : Medicines", "Medicine+20", DataBaseManager.medicinePoint + 20, "Item");
        }
    }
    public void LookButton()
    {
        if (DataBaseManager.nowItem == "Document")
        {
            DataBaseManager.isOpenUi = false;
            InteractionController.Instance.StartSafeLook("Document_Look");
            sisUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "Safe")
        {
            DataBaseManager.isOpenUi = false;
            InteractionController.Instance.StartSafeLook("Safe_Look");
            sisUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "Bracelet")
        {
            DataBaseManager.isOpenUi = false;
            InteractionController.Instance.StartSafeLook("Bracelet_Look");
            sisUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "Coat")
        {
            DataBaseManager.isOpenUi = false;
            InteractionController.Instance.StartSafeLook("Coat_Look");
            sisUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "Sewerge Key")
        {
            DataBaseManager.isOpenUi = false;
            InteractionController.Instance.StartSafeLook("SewergeKey_Look");
            sisUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "Map")
        {
            DataBaseManager.isOpenUi = false;
            InteractionController.Instance.StartSafeLook("Map_Look");
            sisUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "OldMap")
        {
            DataBaseManager.isOpenUi = false;
            InteractionController.Instance.StartSafeLook("OldMap_Look");
            sisUI.SetActive(false);
        }
    }
    public void UseButton()
    {
        if (DataBaseManager.nowItem == "Safe" && DataBaseManager.judgeSafeTryOpen == false)
        {
            DataBaseManager.isOpenUi = false;
            Rollet.Instance.SetRollet("Safe : Open", "Deftness-20", DataBaseManager.deftnessPoint - 20, "Item");
            sisUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "Coat" && DataBaseManager.judgeCoatTryDisguise == false)
        {
            DataBaseManager.isOpenUi = false;
            Rollet.Instance.SetRollet("Coat : Disguise", "Disguise", DataBaseManager.disguisePoint, "Item");
            sisUI.SetActive(false);
        }
        //오래된 지도 -> 사용하면 아예 지도창으로 바로 보내주기.
    }
    // Update is called once per frame
    void Update()
    {
        if (DataBaseManager.nowItem == "Safe")
        {
            imageComponent.sprite = safe;
            if (DataBaseManager.isUseSafe == true)
            {
                useButton.SetActive(false);
            }
            else
            {
                useButton.SetActive(true);
            }
        }
        else if (DataBaseManager.nowItem == "Bracelet")
        {
            imageComponent.sprite = bracelet;
            useButton.SetActive(false);
        }
        else if (DataBaseManager.nowItem == "Sewerge Key")
        {
            imageComponent.sprite = sewergeKey;
            useButton.SetActive(false);
        }
        else if (DataBaseManager.nowItem == "Coat")
        {
            imageComponent.sprite = coat;
            if (DataBaseManager.judgeCoatTryDisguise == true)
            {
                useButton.SetActive(false);
            }
            else
            {
                useButton.SetActive(true);
            }
        }
        else if (DataBaseManager.nowItem == "Map")
        {
            imageComponent.sprite = map;
            useButton.SetActive(false);
        }
        else if (DataBaseManager.nowItem == "Document")
        {
            imageComponent.sprite = document;
            useButton.SetActive(false);
        }
        else if (DataBaseManager.nowItem == "OldMap")
        {
            imageComponent.sprite = oldMap;
            useButton.SetActive(false);
        }
        else
        {
            imageComponent.sprite = none;
        }
    }
}

