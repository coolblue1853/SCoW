using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpperUiChecker : MonoBehaviour
{
    public TextMeshProUGUI conditionText;
    public TextMeshProUGUI DateText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        conditionText.text = "Condition : " + DataBaseManager.Condition;
        if (DataBaseManager.TimeCount == 1)
        {
            DateText.text = "Day1-Morning";
        }
        else if (DataBaseManager.TimeCount == 2)
        {
            DateText.text = "Day1-Noon";
        }
        else if (DataBaseManager.TimeCount == 3)
        { 
            DateText.text = "Day1-Afternoon";
        }
        else if (DataBaseManager.TimeCount == 4)
        {
            DateText.text = "Day1-Night";
        }
        else if (DataBaseManager.TimeCount == 5)
        {
            DateText.text = "Day2-Morning";
        }
        else if (DataBaseManager.TimeCount == 6)
        { 
            DateText.text = "Day2-Noon";
        }
    }
}
