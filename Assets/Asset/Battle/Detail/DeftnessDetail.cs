using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeftnessDetail : MonoBehaviour
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
        if (DataBaseManager.BattleWeapon == "Rock")
        {
            Name.text = "Rock";
            Detail.text = "Damage:(1D3)x5\nSkillPoint:" + DataBaseManager.deftnessPoint;
        }
        else if (DataBaseManager.BattleWeapon == "Molotov")
        {
            Name.text = "Molotov";
            Detail.text = "Damage:(2D6+2)x5\nSkillPoint:" + DataBaseManager.deftnessPoint;
        }

        else
        {
            Name.text = "";
            Detail.text = "";
        }
    }
}
