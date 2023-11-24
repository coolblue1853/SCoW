using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SwordsDetail : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI detail;
    // Update is called once per frame
    void Update()
    {
        if (DataBaseManager.BattleWeapon == "Dagger")
        {
            name.text = "Dagger";
            detail.text = "Damage:(1D4+2)x5\nSkillPoint:" + DataBaseManager.swordPoint;
        }
        else if (DataBaseManager.BattleWeapon == "Axe")
        {
            name.text = "Axe";
            detail.text = "Damage:(1D6+2)x5\nSkillPoint:" + DataBaseManager.swordPoint;
        }
        else if (DataBaseManager.BattleWeapon == "Bat")
        {
            name.text = "Bat";
            detail.text = "Damage:(1D8)x5\nSkillPoint:" + DataBaseManager.swordPoint;
        }
        else
        {
            name.text = "";
            detail.text = "";
        }
    }
}
