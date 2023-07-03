using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public void ClickItem()
    {
        DataBaseManager.nowItem = this.name;
        SIS_UIManager.Instance.OpenItemDetail();
    }

}
