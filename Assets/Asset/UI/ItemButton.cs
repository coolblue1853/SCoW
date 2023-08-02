    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
     Image imageComponent;
    public Sprite NoGuide;
    public Sprite Guided;
    public void ClickItem()
    {
        DataBaseManager.nowItem = this.name;
        SIS_UIManager.Instance.OpenItemDetail();
    }
    private void Start()
    {
        imageComponent = this.GetComponent<Image>(); 
    }

    private void Update()
    {
        if(DataBaseManager.nowItem == this.name)
        {
            imageComponent.sprite = Guided;
        }
        else
        {
            imageComponent.sprite = NoGuide;
        }
    }
}
