using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectedItemBox : MonoBehaviour
{
    Image imageComponent;
    public Sprite Shotgun;
    public Sprite Rifle;
    public Sprite Revolver;
    public Sprite SmallPistol;
    public Sprite Bat;
    public Sprite Axe;
    public Sprite Molotov;
    public Sprite Dagger;
    public Sprite None;

    public Sprite PistolAmmo;
    public Sprite RifleAmmo;
    public Sprite ShotgunAmmo;
    private void Start()
    {
        imageComponent = this.GetComponent<Image>();
    }

    void Update()
    {
        if (DataBaseManager.nowItem == "Shotgun")
        {
            imageComponent.sprite = Shotgun;
        }
        else if (DataBaseManager.nowItem == "Rifle")
        {
            imageComponent.sprite = Rifle;
        }
        else if (DataBaseManager.nowItem == "Revolver")
        {
            imageComponent.sprite = Revolver;
        }
        else if (DataBaseManager.nowItem == "SmallPistol")
        {
            imageComponent.sprite = SmallPistol;
        }
        else if (DataBaseManager.nowItem == "Bat")
        {
            imageComponent.sprite = Bat;
        }
        else if (DataBaseManager.nowItem == "Axe")
        {
            imageComponent.sprite = Axe;
        }
        else if (DataBaseManager.nowItem == "Molotov")
        {
            imageComponent.sprite = Molotov;
        }
        else if (DataBaseManager.nowItem == "Dagger")
        {
            imageComponent.sprite = Dagger;
        }
        else if (DataBaseManager.nowItem == "PistolAmmo")
        {
            imageComponent.sprite = PistolAmmo;
        }
        else if (DataBaseManager.nowItem == "RifleAmmo")
        {
            imageComponent.sprite = RifleAmmo;
        }
        else if (DataBaseManager.nowItem == "ShotgunAmmo")
        {
            imageComponent.sprite = ShotgunAmmo;
        }
        else
        {
            imageComponent.sprite = None;
        }
    }
}
