using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SwordsDetail : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Detail;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DataBaseManager.BattleWeapon == "Dagger")
        {
            Name.text = "Dagger";
            Detail.text = "Damage:(1D4+2)x5\nSkillPoint:" + DataBaseManager.swordPoint;
        }
        else if (DataBaseManager.BattleWeapon == "Axe")
        {
            Name.text = "Axe";
            Detail.text = "Damage:(1D6+2)x5\nSkillPoint:" + DataBaseManager.swordPoint;
        }
        else if (DataBaseManager.BattleWeapon == "Bat")
        {
            Name.text = "Bat";
            Detail.text = "Damage:(1D8)x5\nSkillPoint:" + DataBaseManager.swordPoint;
        }
        else
        {
            Name.text = "";
            Detail.text = "";
        }
    }
}
