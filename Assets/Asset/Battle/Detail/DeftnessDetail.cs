using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeftnessDetail : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI detail;
    // Update is called once per frame
    void Update()
    {
        if (DataBaseManager.BattleWeapon == "Rock")
        {
            name.text = "Rock";
            detail.text = "Damage:(1D3)x5\nSkillPoint:" + DataBaseManager.deftnessPoint;
        }
        else if (DataBaseManager.BattleWeapon == "Molotov")
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
