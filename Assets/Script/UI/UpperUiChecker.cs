using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpperUiChecker : MonoBehaviour
{
    public TextMeshProUGUI conditionText;
    public TextMeshProUGUI DateText;

    void Update()
    {
        conditionText.text = "Condition : " + DataBaseManager.condition;
        if (DataBaseManager.timeCount == 1 && DateText.text != "Day1-Morning")
        {
            DateText.text = "Day1-Morning";
        }
        else if (DataBaseManager.timeCount == 2 && DateText.text != "Day1-Noon")
        {
            DateText.text = "Day1-Noon";
        }
        else if (DataBaseManager.timeCount == 3 && DateText.text != "Day1-Afternoon")
        {
            DateText.text = "Day1-Afternoon";
        }
        else if (DataBaseManager.timeCount == 4 && DateText.text != "Day1-Night")
        {
            DateText.text = "Day1-Night";
        }
        else if (DataBaseManager.timeCount == 5 && DateText.text != "Day2-Morning")
        {
            DateText.text = "Day2-Morning";
        }
        else if (DataBaseManager.timeCount == 6 && DateText.text != "Day2-Noon")
        {
            DateText.text = "Day2-Noon";
        }
        else if (DataBaseManager.timeCount == 7 && DateText.text != "Day2-Afternoon")
        {
            DateText.text = "Day2-Afternoon";
        }
        else if (DataBaseManager.timeCount == 8 && DateText.text != "Day2-Night")
        {
            DateText.text = "Day2-Night";
        }
        else if (DataBaseManager.timeCount == 9 && DateText.text != "Day3-Morning")
        {
            DateText.text = "Day3-Morning";
        }
        else if (DataBaseManager.timeCount == 10 && DateText.text != "Day3-Noon")
        {
            DateText.text = "Day3-Noon";
        }
        else if (DataBaseManager.timeCount == 11 && DateText.text != "Day3-Afternoon")
        {
            DateText.text = "Day3-Afternoon";
        }
        else if (DataBaseManager.timeCount == 12 && DateText.text != "Day3-Night")
        {
            DateText.text = "Day3-Night";
        }
        else if (DataBaseManager.timeCount == 13 && DateText.text != "Day4-Morning")
        {
            DateText.text = "Day4-Morning";
        }
        else if (DataBaseManager.timeCount == 14 && DateText.text != "Day4-Noon")
        {
            DateText.text = "Day4-Noon";
        }
        else if (DataBaseManager.timeCount == 15 && DateText.text != "Day4-Afternoon")
        {
            DateText.text = "Day4-Afternoon";
        }
        else if (DataBaseManager.timeCount == 16 && DateText.text != "Day4-Night")
        {
            DateText.text = "Day4-Night";
        }
    }
}
