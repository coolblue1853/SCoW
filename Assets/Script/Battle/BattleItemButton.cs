using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleItemButton : MonoBehaviour
{
    Image imageComponent;
    public Sprite NoGuide;
    public Sprite Guided;
    public void ClickItem()
    {
        SoundManager.Instance.ClickSound_Play();
        DataBaseManager.BattleWeapon = this.name;
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
        if (DataBaseManager.BattleWeapon == this.name)
        {
            imageComponent.sprite = Guided;
        }
        else
        {
            imageComponent.sprite = NoGuide;
        }
    }
}
