using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UseItemBox : MonoBehaviour
{
    Image imageComponent;
    public Sprite bandages;
    public Sprite firstaidkit;
    public Sprite painkillers;
    public Sprite none;
    public GameObject useUI;
    public GameObject sisUI;
    public TextMeshProUGUI useUITet;
    private void Start()
    {
        imageComponent = this.GetComponent<Image>();
    }
    public void ClickYes()
    {
        if (DataBaseManager.isDebuffDrugPhobia != true)
        {
            DataBaseManager.isOpenUi = false;
            useUI.SetActive(false);
            sisUI.SetActive(false);
            if (DataBaseManager.nowItem == "First aid kit")
            {
                Rollet.Instance.SetRollet("Fabian : Medicines", "Medicine+20", DataBaseManager.medicinePoint + 20, "Item");
            }
            else if (DataBaseManager.nowItem == "Bandages")
            {
                Rollet.Instance.SetRollet("Fabian : Medicines", "Medicine+20", DataBaseManager.medicinePoint + 20, "Item");
            }
            else if (DataBaseManager.nowItem == "Painkillers")
            {
                int Heal = Random.Range(1, 4) * 5;
                if ((DataBaseManager.san - DataBaseManager.nowSan) < Heal)
                {
                    Heal = DataBaseManager.san - DataBaseManager.nowSan;
                }
                BillowUIManager.Instance.SanUp(Heal);
                DataBaseManager.painkillers -= 1;
            }
        }
    }
    public void OpenUseUI()
    {
        ClickYes();
    }
    // Update is called once per frame
    void Update()
    {
        if (DataBaseManager.nowItem == "Bandages" && DataBaseManager.bandages > 0)
        {
            imageComponent.sprite = bandages;
        }
        else if (DataBaseManager.nowItem == "First aid kit" && DataBaseManager.firstaidkit > 0)
        {
            imageComponent.sprite = firstaidkit;
        }
        else if (DataBaseManager.nowItem == "Painkillers" && DataBaseManager.painkillers > 0)
        {
            imageComponent.sprite = painkillers;
        }
        else
        {
            imageComponent.sprite = none;
        }
    }
}

