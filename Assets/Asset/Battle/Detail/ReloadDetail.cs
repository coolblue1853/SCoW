using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ReloadDetail : MonoBehaviour
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
        if (BattleManager.Instance.nowReloadWeapon == "Revolver")
        {
            Name.text = "Revolver";
            Detail.text = "Damage:(1D10+2)x5\nMagazine:" + DataBaseManager.nowRevolver+"\nAmmo:"+DataBaseManager.PistolAmmo;
        }
        else if (BattleManager.Instance.nowReloadWeapon == "Shotgun")
        {
            Name.text = "Shotgun";
            Detail.text = "Damage:(5D4)x5\nMagazine:" + DataBaseManager.nowShotgun + "\nAmmo:" + DataBaseManager.ShotgunAmmo;
        }
        else if (BattleManager.Instance.nowReloadWeapon == "Rifle")
        {
            Name.text = "Rifle";
            Detail.text = "Damage:(2D6+2)x5\nMagazine:" + DataBaseManager.nowRifle + "\nAmmo:" + DataBaseManager.RifleAmmo;
        }
        else if (BattleManager.Instance.nowReloadWeapon == "SmallPistol")
        {
            Name.text = "Small Pistol";
            Detail.text = "Damage:(1D6)x5\nMagazine:" + DataBaseManager.nowSmallPistol + "\nAmmo:" + DataBaseManager.PistolAmmo;
        }
        else
        {
            Name.text = "";
            Detail.text = "";
        }
    }
}
