using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class setStat : MonoBehaviour
{
    public Text str_txt;
    public Text intl_txt;
    public Text dex_txt;
    public Text hp_txt;
    public Text mp_txt;
    public Text san_txt;
    public Text luk_txt;
    public Text weal_txt;

    public Image str_image;
    public Image intl_image;
    public Image dex_image;
    public Image hp_image;
    public Image mp_image;
    public Image san_image;
    public Image luk_image;
    public Image weal_image;

    public GameObject next_B;
    public GameObject setStatWindow;
    public GameObject setSkillWindow;
    public void SetStatButton()
    {
        DataBaseManager.str = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        str_txt.text = DataBaseManager.str.ToString();
        str_image.fillAmount = ((float)DataBaseManager.str / 100);
        DataBaseManager.strSkillPoint = DataBaseManager.str;

        DataBaseManager.intl = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        intl_txt.text = DataBaseManager.intl.ToString();
        intl_image.fillAmount = ((float)DataBaseManager.intl / 100);

        DataBaseManager.dex = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        dex_txt.text = DataBaseManager.dex.ToString();
        dex_image.fillAmount = ((float)DataBaseManager.dex / 100);

        DataBaseManager.hp = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        hp_txt.text = DataBaseManager.hp.ToString();
        hp_image.fillAmount = ((float)DataBaseManager.hp / 100);

        DataBaseManager.mp = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        mp_txt.text = DataBaseManager.mp.ToString();
        mp_image.fillAmount = ((float)DataBaseManager.mp / 100);

        DataBaseManager.san = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        san_txt.text = DataBaseManager.san.ToString();
        san_image.fillAmount = ((float)DataBaseManager.san / 100);

        DataBaseManager.luk = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        luk_txt.text = DataBaseManager.luk.ToString();
        luk_image.fillAmount = ((float)DataBaseManager.luk / 100);

        DataBaseManager.weal = (Random.Range(5, 26));
        weal_txt.text = DataBaseManager.weal.ToString();
        weal_image.fillAmount = ((float)DataBaseManager.weal / 25);




    }

    public void setSkill_B()
    {
        setStatWindow.SetActive(false);
        setSkillWindow.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DataBaseManager.str > 5)
        {
            next_B.SetActive(true);
        }
        else
        {
            next_B.SetActive(false);
        }
    }



}
