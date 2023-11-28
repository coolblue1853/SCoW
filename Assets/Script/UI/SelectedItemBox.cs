using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectedItemBox : MonoBehaviour
{
    Image imageComponent;
    public Sprite shotgun;
    public Sprite rifle;
    public Sprite revolver;
    public Sprite smallPistol;
    public Sprite bat;
    public Sprite axe;
    public Sprite molotov;
    public Sprite dagger;
    public Sprite none;
    public Sprite pistolAmmo;
    public Sprite rifleAmmo;
    public Sprite shotgunAmmo;
    private void Start()
    {
        imageComponent = this.GetComponent<Image>();
    }

    void Update()
    {
        if (DataBaseManager.nowItem == "Shotgun")
        {
            imageComponent.sprite = shotgun;
        }
        else if (DataBaseManager.nowItem == "Rifle")
        {
            imageComponent.sprite = rifle;
        }
        else if (DataBaseManager.nowItem == "Revolver")
        {
            imageComponent.sprite = revolver;
        }
        else if (DataBaseManager.nowItem == "SmallPistol")
        {
            imageComponent.sprite = smallPistol;
        }
        else if (DataBaseManager.nowItem == "Bat")
        {
            imageComponent.sprite = bat;
        }
        else if (DataBaseManager.nowItem == "Axe")
        {
            imageComponent.sprite = axe;
        }
        else if (DataBaseManager.nowItem == "Molotov")
        {
            imageComponent.sprite = molotov;
        }
        else if (DataBaseManager.nowItem == "Dagger")
        {
            imageComponent.sprite = dagger;
        }
        else if (DataBaseManager.nowItem == "PistolAmmo")
        {
            imageComponent.sprite = pistolAmmo;
        }
        else if (DataBaseManager.nowItem == "RifleAmmo")
        {
            imageComponent.sprite = rifleAmmo;
        }
        else if (DataBaseManager.nowItem == "ShotgunAmmo")
        {
            imageComponent.sprite = shotgunAmmo;
        }
        else
        {
            imageComponent.sprite = none;
        }
    }
}
