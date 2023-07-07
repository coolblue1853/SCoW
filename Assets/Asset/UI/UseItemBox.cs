using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UseItemBox : MonoBehaviour
{

    Image imageComponent;
    public Sprite Bandages;
    public Sprite Firstaidkit;
    public Sprite Painkillers;
    public Sprite None;

    public GameObject UseUI;
    public GameObject SISUI;
    public TextMeshProUGUI UseUI_Text;
    private void Start()
    {
        imageComponent = this.GetComponent<Image>();
    }
    public void ClickYes()
    {
        DataBaseManager.isOpenUi = false;
        UseUI.SetActive(false);
        SISUI.SetActive(false);
        if (DataBaseManager.nowItem == "First aid kit")
        {

            Rollet.Instance.setRollet("Fabian : Medicines", "Medicine+20", DataBaseManager.medicinePoint + 20, "Item");
        }
        else if (DataBaseManager.nowItem == "Bandages")
        {

            Rollet.Instance.setRollet("Fabian : Medicines", "Medicine+20", DataBaseManager.medicinePoint + 20, "Item");
        }
        else if (DataBaseManager.nowItem == "Painkillers")
        {

            Rollet.Instance.setRollet("Fabian : Psychotherapy", "Psychotherapy+20", DataBaseManager.medicinePoint + 20, "Item");
        }

    }



    public void OpenUseUI()
    {
        ClickYes();

        }


        // Update is called once per frame
        void Update()
        {
            if (DataBaseManager.nowItem == "Bandages" && DataBaseManager.Bandages > 0)
            {
                imageComponent.sprite = Bandages;
            }
            else if (DataBaseManager.nowItem == "First aid kit" && DataBaseManager.Firstaidkit > 0)
            {
                imageComponent.sprite = Firstaidkit;
            }
            else if (DataBaseManager.nowItem == "Painkillers" && DataBaseManager.Painkillers > 0)
            {
                imageComponent.sprite = Painkillers;
            }

            else
            {
                imageComponent.sprite = None;
            }
        }
    }

