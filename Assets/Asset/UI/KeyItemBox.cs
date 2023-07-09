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


    public GameObject SISUI;
    public TextMeshProUGUI UseUI_Text;

    public GameObject Use_B;
    private void Start()
    {
        imageComponent = this.GetComponent<Image>();
    }
    public void ClickYes()
    {
        DataBaseManager.isOpenUi = false;

        SISUI.SetActive(false);
        if (DataBaseManager.nowItem == "First aid kit")
        {

            Rollet.Instance.setRollet("Fabian : Medicines", "Medicine+20", DataBaseManager.medicinePoint + 20, "Item");
        }


    }
    public void LookButton()
    {
        if (DataBaseManager.nowItem == "Safe")
        {
            DataBaseManager.isOpenUi = false;

            InteractionController.Instance.Start_Safe_Look("Safe_Look");
            SISUI.SetActive(false);
        }
    }

    public void UseButton()
    {
        if (DataBaseManager.nowItem == "Safe")
        {
            DataBaseManager.isOpenUi = false;

            Rollet.Instance.setRollet("Safe : Open", "Deftness-20", DataBaseManager.deftnessPoint - 20, "Item");
            SISUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DataBaseManager.nowItem == "Safe")
        {
            imageComponent.sprite = Safe;
            if (DataBaseManager.isUseSafe == true)
            {
                Use_B.SetActive(false);
            }
            else
            {
                Use_B.SetActive(true);
            }

        }
        else
        {
            imageComponent.sprite = None;
        }
    }
}

