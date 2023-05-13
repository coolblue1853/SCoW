using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangeDate : MonoBehaviour
{



    public TextMeshProUGUI text;
    public Animator ChaingUiAnim;
    private void Start()
    {

    }
    public void ChainingDate()
    {
        if(DataBaseManager.TimeCount == 1)
        {
            text.text = "11 / 7 / 1921 - Noon";
        }
        else if (DataBaseManager.TimeCount == 2)
        {
            text.text = "11 / 7 / 1921 - Afternoon";
        }
        else if (DataBaseManager.TimeCount == 3)
        {
            text.text = "11 / 7 / 1921 - Night";
        }
        else if (DataBaseManager.TimeCount == 4)
        {
            text.text = "11 / 7 / 1921 - Morning";
        }
        else if (DataBaseManager.TimeCount == 5)
        {
            text.text = "11 / 7 / 1921 - Noon";
        }
        else if (DataBaseManager.TimeCount == 6)
        {
            text.text = "11 / 7 / 1921 - Afternoon";
        }
        else if (DataBaseManager.TimeCount == 7)
        {
            text.text = "11 / 7 / 1921 - Night";
        }
    }

    public void EndEvent()
    {
        ChaingUiAnim.SetBool("Change", false);
        Invoke("WaitFade", 2f);
    }

    void WaitFade()
    {
        FadingBackGround.Instance.FadeInOut();
        Invoke("MoveChar", 2f);
    }
    void MoveChar()
    {

        MapManager.Instance.MoveChar();
    }
}
