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
    private void Start()
    {
        imageComponent = this.GetComponent<Image>();
    }
    // Update is called once per frame
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
        else if (DataBaseManager.nowItem == "Small Pistol")
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
        else
        {
            imageComponent.sprite = None;
        }
    }
}
