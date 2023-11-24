using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MarkmenDetail : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI detail;
    // Update is called once per frame
    void Update()
    {
        if (DataBaseManager.BattleWeapon == "Revolver")
        {
            name.text = "Revolver";
            detail.text = "Damage:(1D10+2)x5\nMagazine:" + DataBaseManager.nowRevolver+ "\nSkillPoint:" + DataBaseManager.gunShotPoint;
        }
        else if (DataBaseManager.BattleWeapon == "Shotgun")
        {
            name.text = "Shotgun";
            detail.text = "Damage:(5D4)x5\nMagazine:" + DataBaseManager.nowShotgun + "\nSkillPoint:" + DataBaseManager.gunShotPoint;
        }
        else if (DataBaseManager.BattleWeapon == "Rifle")
        {
            name.text = "Rifle";
            detail.text = "Damage:(2D6+2)x5\nMagazine:" + DataBaseManager.nowRifle + "\nSkillPoint:" + DataBaseManager.gunShotPoint;
        }
        else if (DataBaseManager.BattleWeapon == "SmallPistol")
        {
            name.text = "Small Pistol";
            detail.text = "Damage:(1D6)x5\nMagazine:" + DataBaseManager.nowSmallPistol + "\nSkillPoint:" + DataBaseManager.gunShotPoint;
        }
        else
        {
            name.text = "";
            detail.text = "";
        }
    }
}
