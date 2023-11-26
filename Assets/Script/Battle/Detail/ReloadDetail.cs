using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ReloadDetail : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI detail;
    void Update()
    {
        if (BattleManager.Instance.nowReloadWeapon == "Revolver")
        {
            name.text = "Revolver";
            detail.text = "Damage:(1D10+2)x5\nMagazine:" + DataBaseManager.nowRevolver+"\nAmmo:"+DataBaseManager.PistolAmmo;
        }
        else if (BattleManager.Instance.nowReloadWeapon == "Shotgun")
        {
            name.text = "Shotgun";
            detail.text = "Damage:(5D4)x5\nMagazine:" + DataBaseManager.nowShotgun + "\nAmmo:" + DataBaseManager.ShotgunAmmo;
        }
        else if (BattleManager.Instance.nowReloadWeapon == "Rifle")
        {
            name.text = "Rifle";
            detail.text = "Damage:(2D6+2)x5\nMagazine:" + DataBaseManager.nowRifle + "\nAmmo:" + DataBaseManager.RifleAmmo;
        }
        else if (BattleManager.Instance.nowReloadWeapon == "SmallPistol")
        {
            name.text = "Small Pistol";
            detail.text = "Damage:(1D6)x5\nMagazine:" + DataBaseManager.nowSmallPistol + "\nAmmo:" + DataBaseManager.PistolAmmo;
        }
        else
        {
            name.text = "";
            detail.text = "";
        }
    }
}
