using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
public class BattleItemButton : MonoBehaviour
{
    Image imageComponent;
    public Sprite noGuide;
    public Sprite guide;
    public void ClickItem()
    {
        SoundManager.Instance.ClickSound_Play();
        DataBaseManager.battleWeapon = this.name;
    }
    public void ReloadItem()
    {
        SoundManager.Instance.ClickSound_Play();
        BattleManager.Instance.OpenReloadUI(this.name);
    }
    private void Start()
    {
        imageComponent = this.GetComponent<Image>();
    }
    private void Update()
    {
        if (DataBaseManager.battleWeapon == this.name)
        {
            imageComponent.sprite = guide;
        }
        else
        {
            imageComponent.sprite = noGuide;
        }
    }
}
