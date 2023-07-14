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
        DataBaseManager.BattleWeapon = this.name;
      //  SIS_UIManager.Instance.OpenItemDetail(); 비슷한 함수를 만들어서 전투 좌측 UI에 삽입해 주어야함.
    }
    private void Start()
    {
        imageComponent = this.GetComponent<Image>(); 
    }

    private void Update()
    {
        if(DataBaseManager.BattleWeapon == this.name)
        {
            imageComponent.sprite = Guided;
        }
        else
        {
            imageComponent.sprite = NoGuide;
        }
    }
}
