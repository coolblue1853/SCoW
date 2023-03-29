using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class setSkill : MonoBehaviour
{
    public Text strPoint_t;
    public GameObject setStatWindow;
    public GameObject setSkillWindow;

    public Text martialArtsPoint_t;
    public Image martialArtsPoint_g;


    public float clickTime;
    float minClickTime =0.5f;
    bool isClick = false;
    string nowSkill;


    private void Update()
    {
        if(isClick == true)
        {
            clickTime += Time.deltaTime;
        }
        else
        {
            clickTime = 0;
        }

        skillUpButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
        martialArtsPoint_t.text = DataBaseManager.martialArtsPoint.ToString();
        martialArtsPoint_g.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
    }

    // 힘관련 기술들 


    public void skillUpButton()
    {
        if (clickTime > minClickTime )
        {
            if(nowSkill == "martialArtsUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                martialArtsUP();
            }
            if (nowSkill == "martialArtsDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                martialArtsDown();
            }

        }
    }

    public void martialArtsUP()
    {
        if (DataBaseManager.martialArtsPoint < 90 && DataBaseManager.strSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.strSkillPoint -= 5;
            DataBaseManager.martialArtsPoint += 5;
            martialArtsPoint_t.text = DataBaseManager.martialArtsPoint.ToString();
            strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
            martialArtsPoint_g.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
            nowSkill = "martialArtsUP";
        }

       

    }
    public void martialArtsDown()
    {
        if (DataBaseManager.martialArtsPoint > 25)
        {
            isClick = true;
            DataBaseManager.strSkillPoint += 5;
            DataBaseManager.martialArtsPoint -= 5;
            martialArtsPoint_t.text = DataBaseManager.martialArtsPoint.ToString();
            strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
            martialArtsPoint_g.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
            nowSkill = "martialArtsDown";
        }
    }





    public void buttonUP()
    {
        isClick = false;
        minClickTime = 0.5f;
        clickTime = 0;
    }

    public void Back2stat()
    {
        setStatWindow.SetActive(true);
        setSkillWindow.SetActive(false);
    }
}
