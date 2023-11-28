using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
public class ItemButton : MonoBehaviour
{
    Image imageComponent;
    public Sprite noGuide;
    public Sprite guided;
    public void ClickItem()
    {
        SoundManager.Instance.ClickSound_Play();
        DataBaseManager.nowItem = this.name;
        SIS_UIManager.Instance.OpenItemDetail();
    }
    private void Start()
    {
        imageComponent = this.GetComponent<Image>();
    }
    private void Update()
    {
        if (DataBaseManager.nowItem == this.name)
        {
            imageComponent.sprite = guided;
        }
        else
        {
            imageComponent.sprite = noGuide;
        }
    }
}
