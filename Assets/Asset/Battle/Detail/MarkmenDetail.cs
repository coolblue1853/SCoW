using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MarkmenDetail : MonoBehaviour
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
        if (DataBaseManager.BattleWeapon == "Revolver")
        {
            Name.text = "Revolver";
            Detail.text = "Damage:(1D10+2)x5\nMagazine:" + DataBaseManager.nowRevolver+ "\nSkillPoint:" + DataBaseManager.gunShotPoint;
        }
        else if (DataBaseManager.BattleWeapon == "Shotgun")
        {
            Name.text = "Shotgun";
            Detail.text = "Damage:(5D4)x5\nMagazine:" + DataBaseManager.nowShotgun + "\nSkillPoint:" + DataBaseManager.gunShotPoint;
        }
        else if (DataBaseManager.BattleWeapon == "Rifle")
        {
            Name.text = "Rifle";
            Detail.text = "Damage:(2D6+2)x5\nMagazine:" + DataBaseManager.nowRifle + "\nSkillPoint:" + DataBaseManager.gunShotPoint;
        }
        else if (DataBaseManager.BattleWeapon == "SmallPistol")
        {
            Name.text = "Small Pistol";
            Detail.text = "Damage:(1D6)x5\nMagazine:" + DataBaseManager.nowSmallPistol + "\nSkillPoint:" + DataBaseManager.gunShotPoint;
        }
        else
        {
            Name.text = "";
            Detail.text = "";
        }
    }
}
