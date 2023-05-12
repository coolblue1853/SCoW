using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ConditionCheck : MonoBehaviour
{
    public TextMeshProUGUI conditionText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        conditionText.text = "ÄÁµð¼Ç : " + DataBaseManager.Condition;
    }
}
