using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KeyItemBox : MonoBehaviour
{

    Image imageComponent;
    public Sprite Safe;
    public Sprite Bracelet;
    public Sprite Coat;
    public Sprite SewergeKey;
    public Sprite Document;
    public Sprite None; 
   public Sprite Map;
    public Sprite OldMap;

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
        if (DataBaseManager.nowItem == "Document")
        {
            DataBaseManager.isOpenUi = false;

            InteractionController.Instance.Start_Safe_Look("Document_Look");
            SISUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "Safe")
        {
            DataBaseManager.isOpenUi = false;

            InteractionController.Instance.Start_Safe_Look("Safe_Look");
            SISUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "Bracelet")
        {
            DataBaseManager.isOpenUi = false;

            InteractionController.Instance.Start_Safe_Look("Bracelet_Look");
            SISUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "Coat")
        {
            DataBaseManager.isOpenUi = false;

            InteractionController.Instance.Start_Safe_Look("Coat_Look");
            SISUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "Sewerge Key")
        {
            DataBaseManager.isOpenUi = false;

            InteractionController.Instance.Start_Safe_Look("SewergeKey_Look");
            SISUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "Map")
        {
            DataBaseManager.isOpenUi = false;

            InteractionController.Instance.Start_Safe_Look("Map_Look");
            SISUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "OldMap")
        {
            DataBaseManager.isOpenUi = false;

            InteractionController.Instance.Start_Safe_Look("OldMap_Look");
            SISUI.SetActive(false);
        }
    }

    public void UseButton()
    {
        if (DataBaseManager.nowItem == "Safe" && DataBaseManager.JudgeSafe_TryOpen == false)
        {
            DataBaseManager.isOpenUi = false;

            Rollet.Instance.setRollet("Safe : Open", "Deftness-20", DataBaseManager.deftnessPoint - 20, "Item");
            SISUI.SetActive(false);
        }
        if (DataBaseManager.nowItem == "Coat" && DataBaseManager.JudgeCoat_TryDisguise == false)
        {
            DataBaseManager.isOpenUi = false;

            Rollet.Instance.setRollet("Coat : Disguise", "Disguise", DataBaseManager.DisguisePoint, "Item");
            SISUI.SetActive(false);
        }

        //오래된 지도 -> 사용하면 아예 지도창으로 바로 보내주기.
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
        else if (DataBaseManager.nowItem == "Bracelet")
        {
            imageComponent.sprite = Bracelet;
            Use_B.SetActive(false);

        }
        else if (DataBaseManager.nowItem == "Sewerge Key")
        {
            imageComponent.sprite = SewergeKey;
            Use_B.SetActive(false);

        }
        else if (DataBaseManager.nowItem == "Coat")
        {
            imageComponent.sprite = Coat;
            if (DataBaseManager.JudgeCoat_TryDisguise == true)
            {
                Use_B.SetActive(false);
            }
            else
            {
                Use_B.SetActive(true);
            }

        }
        else if (DataBaseManager.nowItem == "Map")
        {
            imageComponent.sprite = Map;
            Use_B.SetActive(false);

        }
        else if (DataBaseManager.nowItem == "Document")
        {
            imageComponent.sprite = Document;
            Use_B.SetActive(false);

        }
        else if (DataBaseManager.nowItem == "OldMap")
        {
            imageComponent.sprite = OldMap;
            Use_B.SetActive(false);

        }
        else
        {
            imageComponent.sprite = None;
        }
        


  
    }
}

