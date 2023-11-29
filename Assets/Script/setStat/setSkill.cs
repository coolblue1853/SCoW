using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class setSkill : MonoBehaviour
{
    Sequence mySequence;
    public GameObject backButton;
    public Image backButtonImg;
    public float clickTime;
    float minClickTime = 0.5f;
    bool isClick = false;
    string nowSkill;
    public Text strPointTxt;
    public Text intPointTxt;
    public Text dexPointTxt;

    public GameObject setStatWindow;
    public GameObject setSkillWindow;
    public GameObject setStatButton;
    public GameObject allRandButton;

    //skill - str
    public Text martialArtsPointTxt;
    public Text gunShotPointTxt;
    public Text swordPointTxt;
    public Text observationPointTxt;
    public Text swimingPointTxt;

    public Image martialArtsPointImg;
    public Image gunShotPointImg;
    public Image swordPointImg;
    public Image observationPointImg;
    public Image swimingPointImg;

    //skill - int
    public Text medicineTxt;
    public Text analysisTxt;
    public Text listeningTxt;
    public Text psychotherapyTxt;
    public Text occultTxt;

    public Image medicineImg;
    public Image analysisImg;
    public Image listeningImg;
    public Image psychotherapyImg;
    public Image occultImg;

    //skill - dex
    public Text evasionTxt;
    public Text deftnessTxt;
    public Text rhetoricTxt;
    public Text stealthTxt;
    public Text disguiseTxt;

    public Image evasionImg;
    public Image deftnessImg;
    public Image rhetoricImg;
    public Image stealthImg;
    public Image disguiseImg;
    public void FadeIn()
    {
        backButton.SetActive(true);
        Tween fadeTween2 = backButtonImg.DOFade(1, 1.5f);
        fadeTween2.OnComplete(GoToGame);
    }
    public void GoToGame()
    {
        SceneManager.LoadScene("Main");
    }

    private void Update()
    {
        if (isClick == true)
        {
            clickTime += Time.deltaTime;
        }
        else
        {
            clickTime = 0;
        }
        SkillUpButton();
    }

    void Start()
    {
        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();

        martialArtsPointTxt.text = DataBaseManager.martialArtsPoint.ToString();
        martialArtsPointImg.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
        gunShotPointTxt.text = DataBaseManager.gunShotPoint.ToString();
        gunShotPointImg.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
        swordPointTxt.text = DataBaseManager.swordPoint.ToString();
        swordPointImg.fillAmount = (float)DataBaseManager.swordPoint / 100;
        observationPointTxt.text = DataBaseManager.observationPoint.ToString();
        observationPointImg.fillAmount = (float)DataBaseManager.observationPoint / 100;
        swimingPointTxt.text = DataBaseManager.swimingPoint.ToString();
        swimingPointImg.fillAmount = (float)DataBaseManager.swimingPoint / 100;

        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();

        medicineTxt.text = DataBaseManager.medicinePoint.ToString();
        medicineImg.fillAmount = (float)DataBaseManager.medicinePoint / 100;
        analysisTxt.text = DataBaseManager.analysisPoint.ToString();
        analysisImg.fillAmount = (float)DataBaseManager.analysisPoint / 100;
        occultTxt.text = DataBaseManager.occultPoint.ToString();
        occultImg.fillAmount = (float)DataBaseManager.occultPoint / 100;
        listeningTxt.text = DataBaseManager.listeningPoint.ToString();
        listeningImg.fillAmount = (float)DataBaseManager.listeningPoint / 100;
        psychotherapyTxt.text = DataBaseManager.psychotherapyPoint.ToString();
        psychotherapyImg.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;

        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();

        evasionTxt.text = DataBaseManager.evasionPoint.ToString();
        evasionImg.fillAmount = (float)DataBaseManager.evasionPoint / 100;
        deftnessTxt.text = DataBaseManager.deftnessPoint.ToString();
        deftnessImg.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
        rhetoricTxt.text = DataBaseManager.rhetoricPoint.ToString();
        rhetoricImg.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
        stealthTxt.text = DataBaseManager.stealthPoint.ToString();
        stealthImg.fillAmount = (float)DataBaseManager.stealthPoint / 100;
        disguiseTxt.text = DataBaseManager.disguisePoint.ToString();
        disguiseImg.fillAmount = (float)DataBaseManager.disguisePoint / 100;
    }
    public void AllRand()
    {
        while (DataBaseManager.dexSkillPoint >= 5)
        {
            int randNum = Random.Range(1, 6);
            switch (randNum)
            {
                case 1:
                    if (DataBaseManager.evasionPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.evasionPoint += 5;
                        evasionTxt.text = DataBaseManager.evasionPoint.ToString();
                        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
                        evasionImg.fillAmount = (float)DataBaseManager.evasionPoint / 100;
                    }
                    break;
                case 2:
                    if (DataBaseManager.deftnessPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.deftnessPoint += 5;
                        deftnessTxt.text = DataBaseManager.deftnessPoint.ToString();
                        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
                        deftnessImg.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
                    }
                    break;
                case 3:
                    if (DataBaseManager.rhetoricPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.rhetoricPoint += 5;
                        rhetoricTxt.text = DataBaseManager.rhetoricPoint.ToString();
                        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
                        rhetoricImg.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
                    }
                    break;
                case 4:
                    if (DataBaseManager.stealthPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.stealthPoint += 5;
                        stealthTxt.text = DataBaseManager.stealthPoint.ToString();
                        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
                        stealthImg.fillAmount = (float)DataBaseManager.stealthPoint / 100;
                    }
                    break;
                case 5:
                    if (DataBaseManager.disguisePoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.disguisePoint += 5;
                        disguiseTxt.text = DataBaseManager.disguisePoint.ToString();
                        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
                        disguiseImg.fillAmount = (float)DataBaseManager.disguisePoint / 100;
                    }
                    break;
            }
        }
        while (DataBaseManager.intSkillPoint >= 5)
        {
            int randNum = Random.Range(1, 6);
            switch (randNum)
            {
                case 1:
                    if (DataBaseManager.medicinePoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.medicinePoint += 5;
                        medicineTxt.text = DataBaseManager.medicinePoint.ToString();
                        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
                        medicineImg.fillAmount = (float)DataBaseManager.medicinePoint / 100;
                    }
                    break;
                case 2:
                    if (DataBaseManager.analysisPoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.analysisPoint += 5;
                        analysisTxt.text = DataBaseManager.analysisPoint.ToString();
                        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
                        analysisImg.fillAmount = (float)DataBaseManager.analysisPoint / 100;
                    }
                    break;
                case 3:
                    if (DataBaseManager.occultPoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.occultPoint += 5;
                        occultTxt.text = DataBaseManager.occultPoint.ToString();
                        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
                        occultImg.fillAmount = (float)DataBaseManager.occultPoint / 100;
                    }
                    break;
                case 4:
                    if (DataBaseManager.listeningPoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.listeningPoint += 5;
                        listeningTxt.text = DataBaseManager.listeningPoint.ToString();
                        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
                        listeningImg.fillAmount = (float)DataBaseManager.listeningPoint / 100;
                    }
                    break;
                case 5:
                    if (DataBaseManager.psychotherapyPoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.psychotherapyPoint += 5;
                        psychotherapyTxt.text = DataBaseManager.psychotherapyPoint.ToString();
                        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
                        psychotherapyImg.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;
                    }
                    break;
            }
        }
        while (DataBaseManager.strSkillPoint >= 5)
        {
            int randNum = Random.Range(1, 6);
            switch (randNum)
            {
                case 1:
                    if (DataBaseManager.martialArtsPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {
                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.martialArtsPoint += 5;
                        martialArtsPointTxt.text = DataBaseManager.martialArtsPoint.ToString();
                        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
                        martialArtsPointImg.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
                    }
                    break;
                case 2:
                    if (DataBaseManager.gunShotPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {
                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.gunShotPoint += 5;
                        gunShotPointTxt.text = DataBaseManager.gunShotPoint.ToString();
                        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
                        gunShotPointImg.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
                    }
                    break;
                case 3:
                    if (DataBaseManager.swordPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {

                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.swordPoint += 5;
                        swordPointTxt.text = DataBaseManager.swordPoint.ToString();
                        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
                        swordPointImg.fillAmount = (float)DataBaseManager.swordPoint / 100;

                    }
                    break;
                case 4:
                    if (DataBaseManager.observationPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {
                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.observationPoint += 5;
                        observationPointTxt.text = DataBaseManager.observationPoint.ToString();
                        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
                        observationPointImg.fillAmount = (float)DataBaseManager.observationPoint / 100;
                    }
                    break;
                case 5:
                    if (DataBaseManager.swimingPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {
                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.swimingPoint += 5;
                        swimingPointTxt.text = DataBaseManager.swimingPoint.ToString();
                        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
                        swimingPointImg.fillAmount = (float)DataBaseManager.swimingPoint / 100;
                    }
                    break;
            }
        }
    }

    // ������ ����� 
    public void RandStr()
    {
        SoundManager.Instance.ClickSoundPlay();
        while (DataBaseManager.strSkillPoint >= 5)
        {
            int randNum = Random.Range(1, 6);
            switch (randNum)
            {
                case 1:
                    if (DataBaseManager.martialArtsPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {
                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.martialArtsPoint += 5;
                        martialArtsPointTxt.text = DataBaseManager.martialArtsPoint.ToString();
                        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
                        martialArtsPointImg.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
                    }
                    break;
                case 2:
                    if (DataBaseManager.gunShotPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {
                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.gunShotPoint += 5;
                        gunShotPointTxt.text = DataBaseManager.gunShotPoint.ToString();
                        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
                        gunShotPointImg.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
                    }
                    break;
                case 3:
                    if (DataBaseManager.swordPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {

                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.swordPoint += 5;
                        swordPointTxt.text = DataBaseManager.swordPoint.ToString();
                        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
                        swordPointImg.fillAmount = (float)DataBaseManager.swordPoint / 100;

                    }
                    break;
                case 4:
                    if (DataBaseManager.observationPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {
                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.observationPoint += 5;
                        observationPointTxt.text = DataBaseManager.observationPoint.ToString();
                        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
                        observationPointImg.fillAmount = (float)DataBaseManager.observationPoint / 100;
                    }
                    break;
                case 5:
                    if (DataBaseManager.swimingPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {
                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.swimingPoint += 5;
                        swimingPointTxt.text = DataBaseManager.swimingPoint.ToString();
                        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
                        swimingPointImg.fillAmount = (float)DataBaseManager.swimingPoint / 100;
                    }
                    break;
            }
        }
    }
    public void ResetStr()
    {
        SoundManager.Instance.ClickSoundPlay();
        DataBaseManager.martialArtsPoint = 25;
        martialArtsPointTxt.text = DataBaseManager.martialArtsPoint.ToString();
        martialArtsPointImg.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
        DataBaseManager.gunShotPoint = 15;
        gunShotPointTxt.text = DataBaseManager.gunShotPoint.ToString();
        gunShotPointImg.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
        DataBaseManager.swordPoint = 20;
        swordPointTxt.text = DataBaseManager.swordPoint.ToString();
        swordPointImg.fillAmount = (float)DataBaseManager.swordPoint / 100;
        DataBaseManager.observationPoint = 20;
        observationPointTxt.text = DataBaseManager.observationPoint.ToString();
        observationPointImg.fillAmount = (float)DataBaseManager.observationPoint / 100;
        DataBaseManager.swimingPoint = 10;
        swimingPointTxt.text = DataBaseManager.swimingPoint.ToString();
        swimingPointImg.fillAmount = (float)DataBaseManager.swimingPoint / 100;
        DataBaseManager.strSkillPoint = DataBaseManager.str;
        strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
    }
    public void SkillUpButton()
    {
        if (clickTime > minClickTime)
        {
            if (nowSkill == "martialArtsUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                MartialArtsUP();
            }
            if (nowSkill == "martialArtsDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                MartialArtsDown();
            }
            if (nowSkill == "gunShotUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                GunShotUP();
            }
            if (nowSkill == "gunShotDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                GunShotDown();
            }
            if (nowSkill == "swordUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                SwordUP();
            }
            if (nowSkill == "swordDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                SwordDown();
            }
            if (nowSkill == "ObservationUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                ObservationUP();
            }
            if (nowSkill == "ObservationDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                ObservationDown();
            }
            if (nowSkill == "swimingUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                SwimingUP();
            }
            if (nowSkill == "swimingDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                SwimingDown();
            }

            if (nowSkill == "medicineUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                MedicineUP();
            }
            if (nowSkill == "medicineDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                MedicineDown();
            }
            if (nowSkill == "analysisUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                AnalysisUP();
            }
            if (nowSkill == "analysisDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                AnalysisDown();
            }
            if (nowSkill == "occultUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                OccultUP();
            }
            if (nowSkill == "occultDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                OccultDown();
            }
            if (nowSkill == "listeningUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                ListeningUP();
            }
            if (nowSkill == "listeningDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                ListeningDown();
            }
            if (nowSkill == "psychotherapyUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                PsychotherapyUP();
            }
            if (nowSkill == "psychotherapyDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                PsychotherapyDown();
            }

            if (nowSkill == "evasionUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                EvasionUP();
            }
            if (nowSkill == "evasionDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                EvasionDown();
            }
            if (nowSkill == "deftnessUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                DeftnessUP();
            }
            if (nowSkill == "deftnessDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                DeftnessDown();
            }
            if (nowSkill == "rhetoricUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                RhetoricUP();
            }
            if (nowSkill == "rhetoricDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                RhetoricDown();
            }
            if (nowSkill == "stealthUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                StealthUP();
            }
            if (nowSkill == "stealthDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                StealthDown();
            }
            if (nowSkill == "DisguiseUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                DisguiseUP();
            }
            if (nowSkill == "DisguiseDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                DisguiseDown();
            }
        }
    }
    //str
    public void MartialArtsUP()
    {
        if (DataBaseManager.martialArtsPoint < 90 && DataBaseManager.strSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.strSkillPoint -= 5;
            DataBaseManager.martialArtsPoint += 5;
            martialArtsPointTxt.text = DataBaseManager.martialArtsPoint.ToString();
            strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
            martialArtsPointImg.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
            nowSkill = "martialArtsUP";
        }
    }
    public void MartialArtsDown()
    {
        if (DataBaseManager.martialArtsPoint > 25)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.strSkillPoint += 5;
            DataBaseManager.martialArtsPoint -= 5;
            martialArtsPointTxt.text = DataBaseManager.martialArtsPoint.ToString();
            strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
            martialArtsPointImg.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
            nowSkill = "martialArtsDown";
        }
    }
    public void GunShotUP()
    {
        if (DataBaseManager.gunShotPoint < 90 && DataBaseManager.strSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.strSkillPoint -= 5;
            DataBaseManager.gunShotPoint += 5;
            gunShotPointTxt.text = DataBaseManager.gunShotPoint.ToString();
            strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
            gunShotPointImg.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
            nowSkill = "gunShotUP";
        }
    }
    public void GunShotDown()
    {
        if (DataBaseManager.gunShotPoint > 15)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;

            DataBaseManager.strSkillPoint += 5;
            DataBaseManager.gunShotPoint -= 5;
            gunShotPointTxt.text = DataBaseManager.gunShotPoint.ToString();
            strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
            gunShotPointImg.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
            nowSkill = "gunShotDown";
        }
    }
    public void SwordUP()
    {
        if (DataBaseManager.swordPoint < 90 && DataBaseManager.strSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.strSkillPoint -= 5;
            DataBaseManager.swordPoint += 5;
            swordPointTxt.text = DataBaseManager.swordPoint.ToString();
            strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
            swordPointImg.fillAmount = (float)DataBaseManager.swordPoint / 100;
            nowSkill = "swordUP";
        }
    }
    public void SwordDown()
    {
        if (DataBaseManager.swordPoint > 20)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.strSkillPoint += 5;
            DataBaseManager.swordPoint -= 5;
            swordPointTxt.text = DataBaseManager.swordPoint.ToString();
            strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
            swordPointImg.fillAmount = (float)DataBaseManager.swordPoint / 100;
            nowSkill = "swordDown";
        }
    }
    public void ObservationUP()
    {
        if (DataBaseManager.observationPoint < 90 && DataBaseManager.strSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.strSkillPoint -= 5;
            DataBaseManager.observationPoint += 5;
            observationPointTxt.text = DataBaseManager.observationPoint.ToString();
            strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
            observationPointImg.fillAmount = (float)DataBaseManager.observationPoint / 100;
            nowSkill = "ObservationUP";
        }
    }
    public void ObservationDown()
    {
        if (DataBaseManager.observationPoint > 20)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.strSkillPoint += 5;
            DataBaseManager.observationPoint -= 5;
            observationPointTxt.text = DataBaseManager.observationPoint.ToString();
            strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
            observationPointImg.fillAmount = (float)DataBaseManager.observationPoint / 100;
            nowSkill = "ObservationDown";
        }
    }
    public void SwimingUP()
    {
        if (DataBaseManager.swimingPoint < 90 && DataBaseManager.strSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.strSkillPoint -= 5;
            DataBaseManager.swimingPoint += 5;
            swimingPointTxt.text = DataBaseManager.swimingPoint.ToString();
            strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
            swimingPointImg.fillAmount = (float)DataBaseManager.swimingPoint / 100;
            nowSkill = "swimingUP";
        }
    }
    public void SwimingDown()
    {
        if (DataBaseManager.swimingPoint > 10)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.strSkillPoint += 5;
            DataBaseManager.swimingPoint -= 5;
            swimingPointTxt.text = DataBaseManager.swimingPoint.ToString();
            strPointTxt.text = DataBaseManager.strSkillPoint.ToString();
            swimingPointImg.fillAmount = (float)DataBaseManager.swimingPoint / 100;
            nowSkill = "swimingDown";
        }
    }
    public void RandInt()
    {
        SoundManager.Instance.ClickSoundPlay();
        while (DataBaseManager.intSkillPoint >= 5)
        {
            int randNum = Random.Range(1, 6);
            switch (randNum)
            {
                case 1:
                    if (DataBaseManager.medicinePoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.medicinePoint += 5;
                        medicineTxt.text = DataBaseManager.medicinePoint.ToString();
                        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
                        medicineImg.fillAmount = (float)DataBaseManager.medicinePoint / 100;
                    }
                    break;
                case 2:
                    if (DataBaseManager.analysisPoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.analysisPoint += 5;
                        analysisTxt.text = DataBaseManager.analysisPoint.ToString();
                        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
                        analysisImg.fillAmount = (float)DataBaseManager.analysisPoint / 100;
                    }
                    break;
                case 3:
                    if (DataBaseManager.occultPoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.occultPoint += 5;
                        occultTxt.text = DataBaseManager.occultPoint.ToString();
                        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
                        occultImg.fillAmount = (float)DataBaseManager.occultPoint / 100;
                    }
                    break;
                case 4:
                    if (DataBaseManager.listeningPoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.listeningPoint += 5;
                        listeningTxt.text = DataBaseManager.listeningPoint.ToString();
                        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
                        listeningImg.fillAmount = (float)DataBaseManager.listeningPoint / 100;
                    }
                    break;
                case 5:
                    if (DataBaseManager.psychotherapyPoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.psychotherapyPoint += 5;
                        psychotherapyTxt.text = DataBaseManager.psychotherapyPoint.ToString();
                        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
                        psychotherapyImg.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;
                    }
                    break;
            }
        }
    }
    public void ResetInt()
    {
        SoundManager.Instance.ClickSoundPlay();
        DataBaseManager.medicinePoint = 10;
        medicineTxt.text = DataBaseManager.medicinePoint.ToString();
        medicineImg.fillAmount = (float)DataBaseManager.medicinePoint / 100;
        DataBaseManager.analysisPoint = 30;
        analysisTxt.text = DataBaseManager.analysisPoint.ToString();
        analysisImg.fillAmount = (float)DataBaseManager.analysisPoint / 100;
        DataBaseManager.listeningPoint = 30;
        listeningTxt.text = DataBaseManager.listeningPoint.ToString();
        listeningImg.fillAmount = (float)DataBaseManager.listeningPoint / 100;
        DataBaseManager.psychotherapyPoint = 5;
        psychotherapyTxt.text = DataBaseManager.psychotherapyPoint.ToString();
        psychotherapyImg.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;
        DataBaseManager.occultPoint = 5;
        occultTxt.text = DataBaseManager.occultPoint.ToString();
        occultImg.fillAmount = (float)DataBaseManager.occultPoint / 100;
        DataBaseManager.intSkillPoint = DataBaseManager.intl;
        intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
    }
    //int
    public void MedicineUP()
    {
        if (DataBaseManager.medicinePoint < 90 && DataBaseManager.intSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.intSkillPoint -= 5;
            DataBaseManager.medicinePoint += 5;
            medicineTxt.text = DataBaseManager.medicinePoint.ToString();
            intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
            medicineImg.fillAmount = (float)DataBaseManager.medicinePoint / 100;
            nowSkill = "medicineUP";
        }
    }
    public void MedicineDown()
    {
        if (DataBaseManager.medicinePoint > 10)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;

            DataBaseManager.intSkillPoint += 5;
            DataBaseManager.medicinePoint -= 5;
            medicineTxt.text = DataBaseManager.medicinePoint.ToString();
            intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
            medicineImg.fillAmount = (float)DataBaseManager.medicinePoint / 100;
            nowSkill = "medicineDown";
        }
    }
    public void AnalysisUP()
    {
        if (DataBaseManager.analysisPoint < 90 && DataBaseManager.intSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.intSkillPoint -= 5;
            DataBaseManager.analysisPoint += 5;
            analysisTxt.text = DataBaseManager.analysisPoint.ToString();
            intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
            analysisImg.fillAmount = (float)DataBaseManager.analysisPoint / 100;
            nowSkill = "analysisUP";
        }
    }
    public void AnalysisDown()
    {
        if (DataBaseManager.analysisPoint > 30)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;

            DataBaseManager.intSkillPoint += 5;
            DataBaseManager.analysisPoint -= 5;
            analysisTxt.text = DataBaseManager.analysisPoint.ToString();
            intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
            analysisImg.fillAmount = (float)DataBaseManager.analysisPoint / 100;
            nowSkill = "analysisDown";
        }
    }
    public void OccultUP()
    {
        if (DataBaseManager.occultPoint < 90 && DataBaseManager.intSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.intSkillPoint -= 5;
            DataBaseManager.occultPoint += 5;
            occultTxt.text = DataBaseManager.occultPoint.ToString();
            intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
            occultImg.fillAmount = (float)DataBaseManager.occultPoint / 100;
            nowSkill = "occultUP";
        }
    }
    public void OccultDown()
    {
        if (DataBaseManager.occultPoint > 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.intSkillPoint += 5;
            DataBaseManager.occultPoint -= 5;
            occultTxt.text = DataBaseManager.occultPoint.ToString();
            intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
            occultImg.fillAmount = (float)DataBaseManager.occultPoint / 100;
            nowSkill = "occultDown";
        }
    }
    public void ListeningUP()
    {
        if (DataBaseManager.listeningPoint < 90 && DataBaseManager.intSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;

            DataBaseManager.intSkillPoint -= 5;
            DataBaseManager.listeningPoint += 5;
            listeningTxt.text = DataBaseManager.listeningPoint.ToString();
            intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
            listeningImg.fillAmount = (float)DataBaseManager.listeningPoint / 100;
            nowSkill = "listeningUP";
        }
    }
    public void ListeningDown()
    {
        if (DataBaseManager.listeningPoint > 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.intSkillPoint += 5;
            DataBaseManager.listeningPoint -= 5;
            listeningTxt.text = DataBaseManager.listeningPoint.ToString();
            intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
            listeningImg.fillAmount = (float)DataBaseManager.listeningPoint / 100;
            nowSkill = "listeningDown";
        }
    }
    public void PsychotherapyUP()
    {
        if (DataBaseManager.psychotherapyPoint < 90 && DataBaseManager.intSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.intSkillPoint -= 5;
            DataBaseManager.psychotherapyPoint += 5;
            psychotherapyTxt.text = DataBaseManager.psychotherapyPoint.ToString();
            intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
            psychotherapyImg.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;
            nowSkill = "psychotherapyUP";
        }
    }
    public void PsychotherapyDown()
    {
        if (DataBaseManager.psychotherapyPoint > 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;

            DataBaseManager.intSkillPoint += 5;
            DataBaseManager.psychotherapyPoint -= 5;
            psychotherapyTxt.text = DataBaseManager.psychotherapyPoint.ToString();
            intPointTxt.text = DataBaseManager.intSkillPoint.ToString();
            psychotherapyImg.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;
            nowSkill = "psychotherapyDown";
        }
    }

    public void RandDex()
    {
        SoundManager.Instance.ClickSoundPlay();
        while (DataBaseManager.dexSkillPoint >= 5)
        {
            int randNum = Random.Range(1, 6);
            switch (randNum)
            {
                case 1:
                    if (DataBaseManager.evasionPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.evasionPoint += 5;
                        evasionTxt.text = DataBaseManager.evasionPoint.ToString();
                        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
                        evasionImg.fillAmount = (float)DataBaseManager.evasionPoint / 100;
                    }
                    break;
                case 2:
                    if (DataBaseManager.deftnessPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.deftnessPoint += 5;
                        deftnessTxt.text = DataBaseManager.deftnessPoint.ToString();
                        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
                        deftnessImg.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
                    }
                    break;
                case 3:
                    if (DataBaseManager.rhetoricPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.rhetoricPoint += 5;
                        rhetoricTxt.text = DataBaseManager.rhetoricPoint.ToString();
                        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
                        rhetoricImg.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
                    }
                    break;
                case 4:
                    if (DataBaseManager.stealthPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.stealthPoint += 5;
                        stealthTxt.text = DataBaseManager.stealthPoint.ToString();
                        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
                        stealthImg.fillAmount = (float)DataBaseManager.stealthPoint / 100;
                    }
                    break;
                case 5:
                    if (DataBaseManager.disguisePoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.disguisePoint += 5;
                        disguiseTxt.text = DataBaseManager.disguisePoint.ToString();
                        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
                        disguiseImg.fillAmount = (float)DataBaseManager.disguisePoint / 100;
                    }
                    break;
            }
        }
    }
    public void ResetDex()
    {
        SoundManager.Instance.ClickSoundPlay();
        DataBaseManager.evasionPoint = 30;
        evasionTxt.text = DataBaseManager.evasionPoint.ToString();
        evasionImg.fillAmount = (float)DataBaseManager.evasionPoint / 100;
        DataBaseManager.deftnessPoint = 20;
        deftnessTxt.text = DataBaseManager.deftnessPoint.ToString();
        deftnessImg.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
        DataBaseManager.rhetoricPoint = 20;
        rhetoricTxt.text = DataBaseManager.rhetoricPoint.ToString();
        rhetoricImg.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
        DataBaseManager.stealthPoint = 15;
        stealthTxt.text = DataBaseManager.stealthPoint.ToString();
        stealthImg.fillAmount = (float)DataBaseManager.stealthPoint / 100;
        DataBaseManager.disguisePoint = 10;
        disguiseTxt.text = DataBaseManager.disguisePoint.ToString();
        disguiseImg.fillAmount = (float)DataBaseManager.disguisePoint / 100;
        DataBaseManager.dexSkillPoint = DataBaseManager.dex;
        dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
    }

    //dex
    public void EvasionUP()
    {
        if (DataBaseManager.evasionPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.dexSkillPoint -= 5;
            DataBaseManager.evasionPoint += 5;
            evasionTxt.text = DataBaseManager.evasionPoint.ToString();
            dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
            evasionImg.fillAmount = (float)DataBaseManager.evasionPoint / 100;
            nowSkill = "evasionUP";
        }
    }
    public void EvasionDown()
    {
        if (DataBaseManager.evasionPoint > 30)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.dexSkillPoint += 5;
            DataBaseManager.evasionPoint -= 5;
            evasionTxt.text = DataBaseManager.evasionPoint.ToString();
            dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
            evasionImg.fillAmount = (float)DataBaseManager.evasionPoint / 100;
            nowSkill = "evasionDown";
        }
    }
    public void DeftnessUP()
    {
        if (DataBaseManager.deftnessPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.dexSkillPoint -= 5;
            DataBaseManager.deftnessPoint += 5;
            deftnessTxt.text = DataBaseManager.deftnessPoint.ToString();
            dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
            deftnessImg.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
            nowSkill = "deftnessUP";
        }
    }
    public void DeftnessDown()
    {
        if (DataBaseManager.deftnessPoint > 20)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.dexSkillPoint += 5;
            DataBaseManager.deftnessPoint -= 5;
            deftnessTxt.text = DataBaseManager.deftnessPoint.ToString();
            dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
            deftnessImg.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
            nowSkill = "deftnessDown";
        }
    }
    public void RhetoricUP()
    {
        if (DataBaseManager.rhetoricPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.dexSkillPoint -= 5;
            DataBaseManager.rhetoricPoint += 5;
            rhetoricTxt.text = DataBaseManager.rhetoricPoint.ToString();
            dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
            rhetoricImg.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
            nowSkill = "rhetoricUP";
        }
    }
    public void RhetoricDown()
    {
        if (DataBaseManager.rhetoricPoint > 20)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.dexSkillPoint += 5;
            DataBaseManager.rhetoricPoint -= 5;
            rhetoricTxt.text = DataBaseManager.rhetoricPoint.ToString();
            dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
            rhetoricImg.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
            nowSkill = "rhetoricDown";
        }
    }
    public void StealthUP()
    {
        if (DataBaseManager.stealthPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();

            isClick = true;
            DataBaseManager.dexSkillPoint -= 5;
            DataBaseManager.stealthPoint += 5;
            stealthTxt.text = DataBaseManager.stealthPoint.ToString();
            dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
            stealthImg.fillAmount = (float)DataBaseManager.stealthPoint / 100;
            nowSkill = "stealthUP";
        }
    }
    public void StealthDown()
    {
        if (DataBaseManager.stealthPoint > 15)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.dexSkillPoint += 5;
            DataBaseManager.stealthPoint -= 5;
            stealthTxt.text = DataBaseManager.stealthPoint.ToString();
            dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
            stealthImg.fillAmount = (float)DataBaseManager.stealthPoint / 100;
            nowSkill = "stealthDown";
        }
    }
    public void DisguiseUP()
    {
        if (DataBaseManager.disguisePoint < 90 && DataBaseManager.dexSkillPoint >= 5)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.dexSkillPoint -= 5;
            DataBaseManager.disguisePoint += 5;
            disguiseTxt.text = DataBaseManager.disguisePoint.ToString();
            dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
            disguiseImg.fillAmount = (float)DataBaseManager.disguisePoint / 100;
            nowSkill = "DisguiseUP";
        }
    }
    public void DisguiseDown()
    {
        if (DataBaseManager.disguisePoint > 10)
        {
            SoundManager.Instance.ClickSoundPlay();
            isClick = true;
            DataBaseManager.dexSkillPoint += 5;
            DataBaseManager.disguisePoint -= 5;
            disguiseTxt.text = DataBaseManager.disguisePoint.ToString();
            dexPointTxt.text = DataBaseManager.dexSkillPoint.ToString();
            disguiseImg.fillAmount = (float)DataBaseManager.disguisePoint / 100;
            nowSkill = "DisguiseDown";
        }
    }
    public void ButtonUP()
    {
        isClick = false;
        minClickTime = 0.5f;
        clickTime = 0;
        nowSkill = "";
    }
    public void BackToStat()
    {
        SoundManager.Instance.PaperClipPlay();
        setStatButton.SetActive(true);
        setStatWindow.SetActive(true);
        setSkillWindow.SetActive(false);
        allRandButton.SetActive(true);
    }
    public void ResetSkillPoint()
    {
        DataBaseManager.martialArtsPoint = 25;
        martialArtsPointTxt.text = DataBaseManager.martialArtsPoint.ToString();
        martialArtsPointImg.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
        DataBaseManager.gunShotPoint = 15;
        gunShotPointTxt.text = DataBaseManager.gunShotPoint.ToString();
        gunShotPointImg.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
        DataBaseManager.swordPoint = 20;
        swordPointTxt.text = DataBaseManager.swordPoint.ToString();
        swordPointImg.fillAmount = (float)DataBaseManager.swordPoint / 100;
        DataBaseManager.observationPoint = 20;
        observationPointTxt.text = DataBaseManager.observationPoint.ToString();
        observationPointImg.fillAmount = (float)DataBaseManager.observationPoint / 100;
        DataBaseManager.swimingPoint = 10;
        swimingPointTxt.text = DataBaseManager.swimingPoint.ToString();
        swimingPointImg.fillAmount = (float)DataBaseManager.swimingPoint / 100;

        DataBaseManager.medicinePoint = 10;
        medicineTxt.text = DataBaseManager.medicinePoint.ToString();
        medicineImg.fillAmount = (float)DataBaseManager.medicinePoint / 100;
        DataBaseManager.analysisPoint = 30;
        analysisTxt.text = DataBaseManager.analysisPoint.ToString();
        analysisImg.fillAmount = (float)DataBaseManager.analysisPoint / 100;
        DataBaseManager.listeningPoint = 30;
        listeningTxt.text = DataBaseManager.listeningPoint.ToString();
        listeningImg.fillAmount = (float)DataBaseManager.listeningPoint / 100;
        DataBaseManager.psychotherapyPoint = 5;
        psychotherapyTxt.text = DataBaseManager.psychotherapyPoint.ToString();
        psychotherapyImg.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;
        DataBaseManager.occultPoint = 5;
        occultTxt.text = DataBaseManager.occultPoint.ToString();
        occultImg.fillAmount = (float)DataBaseManager.occultPoint / 100;

        DataBaseManager.evasionPoint = 30;
        evasionTxt.text = DataBaseManager.evasionPoint.ToString();
        evasionImg.fillAmount = (float)DataBaseManager.evasionPoint / 100;
        DataBaseManager.deftnessPoint = 20;
        deftnessTxt.text = DataBaseManager.deftnessPoint.ToString();
        deftnessImg.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
        DataBaseManager.rhetoricPoint = 20;
        rhetoricTxt.text = DataBaseManager.rhetoricPoint.ToString();
        rhetoricImg.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
        DataBaseManager.stealthPoint = 15;
        stealthTxt.text = DataBaseManager.stealthPoint.ToString();
        stealthImg.fillAmount = (float)DataBaseManager.stealthPoint / 100;
        DataBaseManager.disguisePoint = 10;
        disguiseTxt.text = DataBaseManager.disguisePoint.ToString();
        disguiseImg.fillAmount = (float)DataBaseManager.disguisePoint / 100;
    }
}
