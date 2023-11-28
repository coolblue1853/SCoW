using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpperUiChecker : MonoBehaviour
{
    public TextMeshProUGUI conditionTxt;
    public TextMeshProUGUI dateTxt;

    void Update()
    {
        conditionTxt.text = "Condition : " + DataBaseManager.condition;
        if (DataBaseManager.timeCount == 1 && dateTxt.text != "Day1-Morning")
        {
            dateTxt.text = "Day1-Morning";
        }
        else if (DataBaseManager.timeCount == 2 && dateTxt.text != "Day1-Noon")
        {
            dateTxt.text = "Day1-Noon";
        }
        else if (DataBaseManager.timeCount == 3 && dateTxt.text != "Day1-Afternoon")
        {
            dateTxt.text = "Day1-Afternoon";
        }
        else if (DataBaseManager.timeCount == 4 && dateTxt.text != "Day1-Night")
        {
            dateTxt.text = "Day1-Night";
        }
        else if (DataBaseManager.timeCount == 5 && dateTxt.text != "Day2-Morning")
        {
            dateTxt.text = "Day2-Morning";
        }
        else if (DataBaseManager.timeCount == 6 && dateTxt.text != "Day2-Noon")
        {
            dateTxt.text = "Day2-Noon";
        }
        else if (DataBaseManager.timeCount == 7 && dateTxt.text != "Day2-Afternoon")
        {
            dateTxt.text = "Day2-Afternoon";
        }
        else if (DataBaseManager.timeCount == 8 && dateTxt.text != "Day2-Night")
        {
            dateTxt.text = "Day2-Night";
        }
        else if (DataBaseManager.timeCount == 9 && dateTxt.text != "Day3-Morning")
        {
            dateTxt.text = "Day3-Morning";
        }
        else if (DataBaseManager.timeCount == 10 && dateTxt.text != "Day3-Noon")
        {
            dateTxt.text = "Day3-Noon";
        }
        else if (DataBaseManager.timeCount == 11 && dateTxt.text != "Day3-Afternoon")
        {
            dateTxt.text = "Day3-Afternoon";
        }
        else if (DataBaseManager.timeCount == 12 && dateTxt.text != "Day3-Night")
        {
            dateTxt.text = "Day3-Night";
        }
        else if (DataBaseManager.timeCount == 13 && dateTxt.text != "Day4-Morning")
        {
            dateTxt.text = "Day4-Morning";
        }
        else if (DataBaseManager.timeCount == 14 && dateTxt.text != "Day4-Noon")
        {
            dateTxt.text = "Day4-Noon";
        }
        else if (DataBaseManager.timeCount == 15 && dateTxt.text != "Day4-Afternoon")
        {
            dateTxt.text = "Day4-Afternoon";
        }
        else if (DataBaseManager.timeCount == 16 && dateTxt.text != "Day4-Night")
        {
            dateTxt.text = "Day4-Night";
        }
    }
}
