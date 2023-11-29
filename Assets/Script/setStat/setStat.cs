using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
public class setStat : MonoBehaviour
{
    public Text strTxt;
    public Text intlTxt;
    public Text dexTxt;
    public Text hpTxt;
    public Text mpTxt;
    public Text sanTxt;
    public Text lukTxt;
    public Text wealTxt;

    public Image strGuage;
    public Image intlGuage;
    public Image dexGuage;
    public Image hpGuage;
    public Image mpGuage;
    public Image sanGuage;
    public Image lukGuage;
    public Image wealGuage;

    public GameObject nextButton;
    public GameObject setStatWindow;
    public GameObject setSkillWindow;
    public GameObject setStatButton;
    public GameObject allRandButton;
    public GameObject checkUI;

    public Text strPointTxt;
    public Text intPointTxt;
    public Text dexPointTxt;

    public setSkill setSkill;

    public bool firstSetStat = true;
    public void SetStatButton()
    {
        if ((DataBaseManager.strSkillPoint == DataBaseManager.str && DataBaseManager.intSkillPoint == DataBaseManager.intl && DataBaseManager.dexSkillPoint == DataBaseManager.dex) || firstSetStat == true)
        {
            SoundManager.Instance.ClickSound_Play();

            firstSetStat = false;
            DataBaseManager.str = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
            strTxt.text = DataBaseManager.str.ToString();
            strGuage.fillAmount = ((float)DataBaseManager.str / 100);
            DataBaseManager.strSkillPoint = DataBaseManager.str;

            DataBaseManager.intl = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
            intlTxt.text = DataBaseManager.intl.ToString();
            intlGuage.fillAmount = ((float)DataBaseManager.intl / 100);
            DataBaseManager.intSkillPoint = DataBaseManager.intl;

            DataBaseManager.dex = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
            dexTxt.text = DataBaseManager.dex.ToString();
            dexGuage.fillAmount = ((float)DataBaseManager.dex / 100);
            DataBaseManager.dexSkillPoint = DataBaseManager.dex;

            DataBaseManager.hp = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
            hpTxt.text = DataBaseManager.hp.ToString();
            hpGuage.fillAmount = ((float)DataBaseManager.hp / 100);

            DataBaseManager.mp = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
            mpTxt.text = DataBaseManager.mp.ToString();
            mpGuage.fillAmount = ((float)DataBaseManager.mp / 100);

            DataBaseManager.san = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
            sanTxt.text = DataBaseManager.san.ToString();
            sanGuage.fillAmount = ((float)DataBaseManager.san / 100);

            DataBaseManager.luk = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
            lukTxt.text = DataBaseManager.luk.ToString();
            lukGuage.fillAmount = ((float)DataBaseManager.luk / 100);

            DataBaseManager.weal = (Random.Range(5, 36));
            wealTxt.text = DataBaseManager.weal.ToString();
            wealGuage.fillAmount = ((float)DataBaseManager.weal / 35);

            DataBaseManager.nowHP = DataBaseManager.hp;
            DataBaseManager.nowSan = DataBaseManager.san;
            setSkill.resetSkillPoint();
        }
        else
        {
            SoundManager.Instance.ClickSound_Play();
            checkUI.SetActive(true);
        }
    }
    public void CloseCheckUI()
    {
        SoundManager.Instance.ClickSound_Play();
        isAll = false;
        checkUI.SetActive(false);
    }
    public void Okay2CheckUI()
    {
        SoundManager.Instance.ClickSound_Play();
        DataBaseManager.str = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        strTxt.text = DataBaseManager.str.ToString();
        strGuage.fillAmount = ((float)DataBaseManager.str / 100);
        DataBaseManager.strSkillPoint = DataBaseManager.str;

        DataBaseManager.intl = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        intlTxt.text = DataBaseManager.intl.ToString();
        intlGuage.fillAmount = ((float)DataBaseManager.intl / 100);
        DataBaseManager.intSkillPoint = DataBaseManager.intl;

        DataBaseManager.dex = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        dexTxt.text = DataBaseManager.dex.ToString();
        dexGuage.fillAmount = ((float)DataBaseManager.dex / 100);
        DataBaseManager.dexSkillPoint = DataBaseManager.dex;

        DataBaseManager.hp = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        hpTxt.text = DataBaseManager.hp.ToString();
        hpGuage.fillAmount = ((float)DataBaseManager.hp / 100);

        DataBaseManager.mp = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        mpTxt.text = DataBaseManager.mp.ToString();
        mpGuage.fillAmount = ((float)DataBaseManager.mp / 100);

        DataBaseManager.san = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        sanTxt.text = DataBaseManager.san.ToString();
        sanGuage.fillAmount = ((float)DataBaseManager.san / 100);

        DataBaseManager.luk = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        lukTxt.text = DataBaseManager.luk.ToString();
        lukGuage.fillAmount = ((float)DataBaseManager.luk / 100);

        DataBaseManager.weal = (Random.Range(5, 36));
        wealTxt.text = DataBaseManager.weal.ToString();
        wealGuage.fillAmount = ((float)DataBaseManager.weal / 35);
        DataBaseManager.nowHP = DataBaseManager.hp;
        DataBaseManager.nowSan = DataBaseManager.san;

        setSkill.resetSkillPoint();
        checkUI.SetActive(false);

        if (isAll == true)
        {
            setSkill.AllRand();

            isAll = false;
        }
    }
    public void SetSkill()
    {
        SoundManager.Instance.PaperClip_Play();
        setStatWindow.SetActive(false);
        setSkillWindow.SetActive(true);
        setStatButton.SetActive(false);
        allRandButton.SetActive(false);

        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
    }

    void Start()
    {
        firstSetStat = true;
    }

    void Update()
    {
        if (DataBaseManager.str > 5)
        {
            nextButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(false);
        }
    }

    bool isAll;
    public void AllRandChar()
    {
        isAll = true;
        SetStatButton();
        setSkill.AllRand();
    }
}
