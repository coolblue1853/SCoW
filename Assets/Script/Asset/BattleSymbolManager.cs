using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleSymbolManager : MonoBehaviour
{
    public Image symbol1;
    public Image symbol2;
    public RectTransform symbolRect1;
    public Sprite deepOneCoatSymbol;
    public Sprite deepOneSymbol;
    // Update is called once per frame
    void Update()
    {
        if(DataBaseManager.nowPlace == "InSewer" && symbol1.sprite != deepOneCoatSymbol)
        {
            symbolRect1.localScale = new Vector3(1.4f, 1.4f, 1);
            symbol1.sprite = deepOneCoatSymbol;
            symbol2.sprite = deepOneSymbol;
        }
    }
}
