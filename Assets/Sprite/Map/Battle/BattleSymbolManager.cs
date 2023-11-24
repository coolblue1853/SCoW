using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSymbolManager : MonoBehaviour
{
    public Image SB1;
    public Image SB2;
    public RectTransform RT1;
    public Sprite DeepOneCoatSB;
    public Sprite DeepOneSB;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DataBaseManager.nowPlace == "InSewer" && SB1.sprite != DeepOneCoatSB)
        {
            RT1.localScale = new Vector3(1.4f, 1.4f, 1);
            SB1.sprite = DeepOneCoatSB;
            SB2.sprite = DeepOneSB;


        }
    }
}
