using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KeyItemBox : MonoBehaviour
{

    Image imageComponent;
    public Sprite Safe;

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


    }


    public void OpenUseUI()
    {

        if (DataBaseManager.nowItem == "First aid kit")
        {
            UseUI.SetActive(true);

            if (DataBaseManager.medicinePoint + 20 >= 90)
            {
                int stat = 90;
                UseUI_Text.text = "Do you want to use First aid kit ?\n\nUse Skill : Medicine+20(" + (stat) + ")";
            }
            else
            {
                UseUI_Text.text = "Do you want to use First aid kit ?\n\nUse Skill : Medicine+20(" + (DataBaseManager.medicinePoint + 20) + ")";
            }

        }
      


        // Update is called once per frame
        void Update()
        {
            if (DataBaseManager.nowItem == "Safe")
            {
                imageComponent.sprite = Safe;
            }
            else
            {
                imageComponent.sprite = None;
            }
        }
    }
}
