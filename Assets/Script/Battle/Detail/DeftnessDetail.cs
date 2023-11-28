using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeftnessDetail : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI detail;
    void Update()
    {
        if (DataBaseManager.battleWeapon == "Rock")
        {
            name.text = "Rock";
            detail.text = "Damage:(1D3)x5\nSkillPoint:" + DataBaseManager.deftnessPoint;
        }
        else if (DataBaseManager.battleWeapon == "Molotov")
        {
            name.text = "Molotov";
            detail.text = "Damage:(2D6+2)x5\nSkillPoint:" + DataBaseManager.deftnessPoint;
        }

        else
        {
            name.text = "";
            detail.text = "";
        }
    }
}
