using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class setSkill : MonoBehaviour
{
    Sequence mySequence;
    public GameObject Back;
    public Image BackGround;
    public float clickTime;
    float minClickTime = 0.5f;
    bool isClick = false;
    string nowSkill;
    public Text strPoint_t;
    public Text intPoint_t;
    public Text dexPoint_t;

    public GameObject setStatWindow;
    public GameObject setSkillWindow;
    public GameObject setStatButton;
    public GameObject allRandButton;

    //skill - str
    public Text martialArtsPoint_t;
    public Text gunShotPoint_t;
    public Text swordPoint_t;
    public Text ObservationPoint_t;
    public Text swimingPoint_t;

    public Image martialArtsPoint_g;
    public Image gunShotPoint_g;
    public Image swordPoint_g;
    public Image ObservationPoint_g;
    public Image swimingPoint_g;

    //skill - int
    public Text medicine_t;
    public Text analysis_t;
    public Text listening_t;
    public Text psychotherapy_t;
    public Text occult_t;

    public Image medicine_g;
    public Image analysis_g;
    public Image listening_g;
    public Image psychotherapy_g;
    public Image occult_g;

    //skill - dex
    public Text evasion_t;
    public Text deftness_t;
    public Text rhetoric_t;
    public Text stealth_t;
    public Text Disguise_t;

    public Image evasion_g;
    public Image deftness_g;
    public Image rhetoric_g;
    public Image stealth_g;
    public Image Disguise_g;
    public void FadeIn()
    {
        Back.SetActive(true);
        mySequence = DOTween.Sequence()
        .Append(BackGround.DOFade(1, 1.5f).SetAutoKill());
        Invoke("goToGame", 1.8f);
    }
    public void goToGame()
    {
        SceneManager.LoadScene("Main");
    }

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
        gunShotPoint_t.text = DataBaseManager.gunShotPoint.ToString();
        gunShotPoint_g.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
        swordPoint_t.text = DataBaseManager.swordPoint.ToString();
        swordPoint_g.fillAmount = (float)DataBaseManager.swordPoint / 100;
        ObservationPoint_t.text = DataBaseManager.ObservationPoint.ToString();
        ObservationPoint_g.fillAmount = (float)DataBaseManager.ObservationPoint / 100;
        swimingPoint_t.text = DataBaseManager.swimingPoint.ToString();
        swimingPoint_g.fillAmount = (float)DataBaseManager.swimingPoint / 100;

        intPoint_t.text = DataBaseManager.intSkillPoint.ToString();

        medicine_t.text = DataBaseManager.medicinePoint.ToString();
        medicine_g.fillAmount = (float)DataBaseManager.medicinePoint / 100;
        analysis_t.text = DataBaseManager.analysisPoint.ToString();
        analysis_g.fillAmount = (float)DataBaseManager.analysisPoint / 100;
        occult_t.text = DataBaseManager.occultPoint.ToString();
        occult_g.fillAmount = (float)DataBaseManager.occultPoint / 100;
        listening_t.text = DataBaseManager.listeningPoint.ToString();
        listening_g.fillAmount = (float)DataBaseManager.listeningPoint / 100;
        psychotherapy_t.text = DataBaseManager.psychotherapyPoint.ToString();
        psychotherapy_g.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;

        dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();

        evasion_t.text = DataBaseManager.evasionPoint.ToString();
        evasion_g.fillAmount = (float)DataBaseManager.evasionPoint / 100;
        deftness_t.text = DataBaseManager.deftnessPoint.ToString();
        deftness_g.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
        rhetoric_t.text = DataBaseManager.rhetoricPoint.ToString();
        rhetoric_g.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
        stealth_t.text = DataBaseManager.stealthPoint.ToString();
        stealth_g.fillAmount = (float)DataBaseManager.stealthPoint / 100;
        Disguise_t.text = DataBaseManager.DisguisePoint.ToString();
        Disguise_g.fillAmount = (float)DataBaseManager.DisguisePoint / 100;
    }

    // 힘관련 기술들 
    public void RandStr()
    {
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
                        martialArtsPoint_t.text = DataBaseManager.martialArtsPoint.ToString();
                        strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
                        martialArtsPoint_g.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
                    }
                    break;
                case 2:
                    if (DataBaseManager.gunShotPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {
                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.gunShotPoint += 5;
                        gunShotPoint_t.text = DataBaseManager.gunShotPoint.ToString();
                        strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
                        gunShotPoint_g.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
                    }
                    break;
                case 3:
                    if (DataBaseManager.swordPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {

                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.swordPoint += 5;
                        swordPoint_t.text = DataBaseManager.swordPoint.ToString();
                        strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
                        swordPoint_g.fillAmount = (float)DataBaseManager.swordPoint / 100;

                    }
                    break;
                case 4:
                    if (DataBaseManager.ObservationPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {
                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.ObservationPoint += 5;
                        ObservationPoint_t.text = DataBaseManager.ObservationPoint.ToString();
                        strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
                        ObservationPoint_g.fillAmount = (float)DataBaseManager.ObservationPoint / 100;
                    }
                    break;
                case 5:
                    if (DataBaseManager.swimingPoint < 90 && DataBaseManager.strSkillPoint >= 5)
                    {
                        DataBaseManager.strSkillPoint -= 5;
                        DataBaseManager.swimingPoint += 5;
                        swimingPoint_t.text = DataBaseManager.swimingPoint.ToString();
                        strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
                        swimingPoint_g.fillAmount = (float)DataBaseManager.swimingPoint / 100;
                    }
                    break;
            }
        }
    }
    public void ResetStr()
    {
        DataBaseManager.martialArtsPoint = 25;
        martialArtsPoint_t.text = DataBaseManager.martialArtsPoint.ToString();
        martialArtsPoint_g.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
        DataBaseManager.gunShotPoint = 15;
        gunShotPoint_t.text = DataBaseManager.gunShotPoint.ToString();
        gunShotPoint_g.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
        DataBaseManager.swordPoint = 20;
        swordPoint_t.text = DataBaseManager.swordPoint.ToString();
        swordPoint_g.fillAmount = (float)DataBaseManager.swordPoint / 100;
        DataBaseManager.ObservationPoint = 20;
        ObservationPoint_t.text = DataBaseManager.ObservationPoint.ToString();
        ObservationPoint_g.fillAmount = (float)DataBaseManager.ObservationPoint / 100;
        DataBaseManager.swimingPoint = 10;
        swimingPoint_t.text = DataBaseManager.swimingPoint.ToString();
        swimingPoint_g.fillAmount = (float)DataBaseManager.swimingPoint / 100;
        DataBaseManager.strSkillPoint = DataBaseManager.str;
        strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
    }




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
            if (nowSkill == "gunShotUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                gunShotUP();
            }
            if (nowSkill == "gunShotDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                gunShotDown();
            }
            if (nowSkill == "swordUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                swordUP();
            }
            if (nowSkill == "swordDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                swordDown();
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
                swimingUP();
            }
            if (nowSkill == "swimingDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                swimingDown();
            }

            if (nowSkill == "medicineUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                medicineUP();
            }
            if (nowSkill == "medicineDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                medicineDown();
            }
            if (nowSkill == "analysisUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                analysisUP();
            }
            if (nowSkill == "analysisDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                analysisDown();
            }
            if (nowSkill == "occultUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                occultUP();
            }
            if (nowSkill == "occultDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                occultDown();
            }
            if (nowSkill == "listeningUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                listeningUP();
            }
            if (nowSkill == "listeningDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                listeningDown();
            }
            if (nowSkill == "psychotherapyUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                psychotherapyUP();
            }
            if (nowSkill == "psychotherapyDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                psychotherapyDown();
            }

            if (nowSkill == "evasionUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                evasionUP();
            }
            if (nowSkill == "evasionDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                evasionDown();
            }
            if (nowSkill == "deftnessUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                deftnessUP();
            }
            if (nowSkill == "deftnessDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                deftnessDown();
            }
            if (nowSkill == "rhetoricUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                rhetoricUP();
            }
            if (nowSkill == "rhetoricDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                rhetoricDown();
            }
            if (nowSkill == "stealthUP")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                stealthUP();
            }
            if (nowSkill == "stealthDown")
            {
                clickTime = 0;
                minClickTime = 0.1f;
                stealthDown();
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
    public void gunShotUP()
    {
        if (DataBaseManager.gunShotPoint < 90 && DataBaseManager.strSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.strSkillPoint -= 5;
            DataBaseManager.gunShotPoint += 5;
            gunShotPoint_t.text = DataBaseManager.gunShotPoint.ToString();
            strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
            gunShotPoint_g.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
            nowSkill = "gunShotUP";
        }
    }
    public void gunShotDown()
    {
        if (DataBaseManager.gunShotPoint > 15)
        {
            isClick = true;

            DataBaseManager.strSkillPoint += 5;
            DataBaseManager.gunShotPoint -= 5;
            gunShotPoint_t.text = DataBaseManager.gunShotPoint.ToString();
            strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
            gunShotPoint_g.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
            nowSkill = "gunShotDown";
        }
    }
    public void swordUP()
    {
        if (DataBaseManager.swordPoint < 90 && DataBaseManager.strSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.strSkillPoint -= 5;
            DataBaseManager.swordPoint += 5;
            swordPoint_t.text = DataBaseManager.swordPoint.ToString();
            strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
            swordPoint_g.fillAmount = (float)DataBaseManager.swordPoint / 100;
            nowSkill = "swordUP";
        }
    }
    public void swordDown()
    {
        if (DataBaseManager.swordPoint > 20)
        {
            isClick = true;
            DataBaseManager.strSkillPoint += 5;
            DataBaseManager.swordPoint -= 5;
            swordPoint_t.text = DataBaseManager.swordPoint.ToString();
            strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
            swordPoint_g.fillAmount = (float)DataBaseManager.swordPoint / 100;
            nowSkill = "swordDown";
        }
    }
    public void ObservationUP()
    {
        if (DataBaseManager.ObservationPoint < 90 && DataBaseManager.strSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.strSkillPoint -= 5;
            DataBaseManager.ObservationPoint += 5;
            ObservationPoint_t.text = DataBaseManager.ObservationPoint.ToString();
            strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
            ObservationPoint_g.fillAmount = (float)DataBaseManager.ObservationPoint / 100;
            nowSkill = "ObservationUP";
        }
    }
    public void ObservationDown()
    {
        if (DataBaseManager.ObservationPoint > 20)
        {
            isClick = true;
            DataBaseManager.strSkillPoint += 5;
            DataBaseManager.ObservationPoint -= 5;
            ObservationPoint_t.text = DataBaseManager.ObservationPoint.ToString();
            strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
            ObservationPoint_g.fillAmount = (float)DataBaseManager.ObservationPoint / 100;
            nowSkill = "ObservationDown";
        }
    }
    public void swimingUP()
    {
        if (DataBaseManager.swimingPoint < 90 && DataBaseManager.strSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.strSkillPoint -= 5;
            DataBaseManager.swimingPoint += 5;
            swimingPoint_t.text = DataBaseManager.swimingPoint.ToString();
            strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
            swimingPoint_g.fillAmount = (float)DataBaseManager.swimingPoint / 100;
            nowSkill = "swimingUP";
        }
    }
    public void swimingDown()
    {
        if (DataBaseManager.swimingPoint > 10)
        {
            isClick = true;
            DataBaseManager.strSkillPoint += 5;
            DataBaseManager.swimingPoint -= 5;
            swimingPoint_t.text = DataBaseManager.swimingPoint.ToString();
            strPoint_t.text = DataBaseManager.strSkillPoint.ToString();
            swimingPoint_g.fillAmount = (float)DataBaseManager.swimingPoint / 100;
            nowSkill = "swimingDown";
        }
    }



    public void RandInt()
    {
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
                        medicine_t.text = DataBaseManager.medicinePoint.ToString();
                        intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
                        medicine_g.fillAmount = (float)DataBaseManager.medicinePoint / 100;
                    }
                    break;
                case 2:
                    if (DataBaseManager.analysisPoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.analysisPoint += 5;
                        analysis_t.text = DataBaseManager.analysisPoint.ToString();
                        intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
                        analysis_g.fillAmount = (float)DataBaseManager.analysisPoint / 100;
                    }
                    break;
                case 3:
                    if (DataBaseManager.occultPoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.occultPoint += 5;
                        occult_t.text = DataBaseManager.occultPoint.ToString();
                        intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
                        occult_g.fillAmount = (float)DataBaseManager.occultPoint / 100;                    }
                    break;
                case 4:
                    if (DataBaseManager.listeningPoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.listeningPoint += 5;
                        listening_t.text = DataBaseManager.listeningPoint.ToString();
                        intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
                        listening_g.fillAmount = (float)DataBaseManager.listeningPoint / 100;
                    }
                    break;
                case 5:
                    if (DataBaseManager.psychotherapyPoint < 90 && DataBaseManager.intSkillPoint >= 5)
                    {
                        DataBaseManager.intSkillPoint -= 5;
                        DataBaseManager.psychotherapyPoint += 5;
                        psychotherapy_t.text = DataBaseManager.psychotherapyPoint.ToString();
                        intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
                        psychotherapy_g.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;
                    }
                    break;
            }
        }
    }
    public void ResetInt()
    {
        DataBaseManager.medicinePoint = 10;
        medicine_t.text = DataBaseManager.medicinePoint.ToString();
        medicine_g.fillAmount = (float)DataBaseManager.medicinePoint / 100;
        DataBaseManager.analysisPoint = 30;
        analysis_t.text = DataBaseManager.analysisPoint.ToString();
        analysis_g.fillAmount = (float)DataBaseManager.analysisPoint / 100;
        DataBaseManager.listeningPoint = 30;
        listening_t.text = DataBaseManager.listeningPoint.ToString();
        listening_g.fillAmount = (float)DataBaseManager.listeningPoint / 100;
        DataBaseManager.psychotherapyPoint = 5;
        psychotherapy_t.text = DataBaseManager.psychotherapyPoint.ToString();
        psychotherapy_g.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;
        DataBaseManager.occultPoint = 5;
        occult_t.text = DataBaseManager.occultPoint.ToString();
        occult_g.fillAmount = (float)DataBaseManager.occultPoint / 100;
        DataBaseManager.intSkillPoint = DataBaseManager.intl;
        intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
    }

    //int
    public void medicineUP()
    {
        if (DataBaseManager.medicinePoint < 90 && DataBaseManager.intSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.intSkillPoint -= 5;
            DataBaseManager.medicinePoint += 5;
            medicine_t.text = DataBaseManager.medicinePoint.ToString();
            intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
            medicine_g.fillAmount = (float)DataBaseManager.medicinePoint / 100;
            nowSkill = "medicineUP";
        }
    }
    public void medicineDown()
    {
        if (DataBaseManager.medicinePoint > 10)
        {
            isClick = true;

            DataBaseManager.intSkillPoint += 5;
            DataBaseManager.medicinePoint -= 5;
            medicine_t.text = DataBaseManager.medicinePoint.ToString();
            intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
            medicine_g.fillAmount = (float)DataBaseManager.medicinePoint / 100;
            nowSkill = "medicineDown";
        }
    }
    public void analysisUP()
    {
        if (DataBaseManager.analysisPoint < 90 && DataBaseManager.intSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.intSkillPoint -= 5;
            DataBaseManager.analysisPoint += 5;
            analysis_t.text = DataBaseManager.analysisPoint.ToString();
            intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
            analysis_g.fillAmount = (float)DataBaseManager.analysisPoint / 100;
            nowSkill = "analysisUP";
        }
    }
    public void analysisDown()
    {
        if (DataBaseManager.analysisPoint > 30)
        {
            isClick = true;

            DataBaseManager.intSkillPoint += 5;
            DataBaseManager.analysisPoint -= 5;
            analysis_t.text = DataBaseManager.analysisPoint.ToString();
            intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
            analysis_g.fillAmount = (float)DataBaseManager.analysisPoint / 100;
            nowSkill = "analysisDown";
        }
    }
    public void occultUP()
    {
        if (DataBaseManager.occultPoint < 90 && DataBaseManager.intSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.intSkillPoint -= 5;
            DataBaseManager.occultPoint += 5;
            occult_t.text = DataBaseManager.occultPoint.ToString();
            intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
            occult_g.fillAmount = (float)DataBaseManager.occultPoint / 100;
            nowSkill = "occultUP";
        }
    }
    public void occultDown()
    {
        if (DataBaseManager.occultPoint > 5)
        {
            isClick = true;
            DataBaseManager.intSkillPoint += 5;
            DataBaseManager.occultPoint -= 5;
            occult_t.text = DataBaseManager.occultPoint.ToString();
            intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
            occult_g.fillAmount = (float)DataBaseManager.occultPoint / 100;
            nowSkill = "occultDown";
        }
    }
    public void listeningUP()
    {
        if (DataBaseManager.listeningPoint < 90 && DataBaseManager.intSkillPoint >= 5)
        {
            isClick = true;

            DataBaseManager.intSkillPoint -= 5;
            DataBaseManager.listeningPoint += 5;
            listening_t.text = DataBaseManager.listeningPoint.ToString();
            intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
            listening_g.fillAmount = (float)DataBaseManager.listeningPoint / 100;
            nowSkill = "listeningUP";
        }
    }
    public void listeningDown()
    {
        if (DataBaseManager.listeningPoint > 5)
        {
            isClick = true;
            DataBaseManager.intSkillPoint += 5;
            DataBaseManager.listeningPoint -= 5;
            listening_t.text = DataBaseManager.listeningPoint.ToString();
            intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
            listening_g.fillAmount = (float)DataBaseManager.listeningPoint / 100;
            nowSkill = "listeningDown";
        }
    }
    public void psychotherapyUP()
    {
        if (DataBaseManager.psychotherapyPoint < 90 && DataBaseManager.intSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.intSkillPoint -= 5;
            DataBaseManager.psychotherapyPoint += 5;
            psychotherapy_t.text = DataBaseManager.psychotherapyPoint.ToString();
            intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
            psychotherapy_g.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;
            nowSkill = "psychotherapyUP";
        }
    }
    public void psychotherapyDown()
    {
        if (DataBaseManager.psychotherapyPoint > 5)
        {
            isClick = true;

            DataBaseManager.intSkillPoint += 5;
            DataBaseManager.psychotherapyPoint -= 5;
            psychotherapy_t.text = DataBaseManager.psychotherapyPoint.ToString();
            intPoint_t.text = DataBaseManager.intSkillPoint.ToString();
            psychotherapy_g.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;
            nowSkill = "psychotherapyDown";
        }
    }

    public void RandDex()
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
                        evasion_t.text = DataBaseManager.evasionPoint.ToString();
                        dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
                        evasion_g.fillAmount = (float)DataBaseManager.evasionPoint / 100;
                    }
                    break;
                case 2:
                    if (DataBaseManager.deftnessPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.deftnessPoint += 5;
                        deftness_t.text = DataBaseManager.deftnessPoint.ToString();
                        dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
                        deftness_g.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
                    }
                    break;
                case 3:
                    if (DataBaseManager.rhetoricPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.rhetoricPoint += 5;
                        rhetoric_t.text = DataBaseManager.rhetoricPoint.ToString();
                        dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
                        rhetoric_g.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
                    }
                    break;
                case 4:
                    if (DataBaseManager.stealthPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.stealthPoint += 5;
                        stealth_t.text = DataBaseManager.stealthPoint.ToString();
                        dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
                        stealth_g.fillAmount = (float)DataBaseManager.stealthPoint / 100;
                    }
                    break;
                case 5:
                    if (DataBaseManager.DisguisePoint < 90 && DataBaseManager.dexSkillPoint >= 5)
                    {
                        DataBaseManager.dexSkillPoint -= 5;
                        DataBaseManager.DisguisePoint += 5;
                        Disguise_t.text = DataBaseManager.DisguisePoint.ToString();
                        dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
                        Disguise_g.fillAmount = (float)DataBaseManager.DisguisePoint / 100;
                    }
                    break;
            }
        }
    }
    public void ResetDex()
    {
        DataBaseManager.evasionPoint = 30;
        evasion_t.text = DataBaseManager.evasionPoint.ToString();
        evasion_g.fillAmount = (float)DataBaseManager.evasionPoint / 100;
        DataBaseManager.deftnessPoint = 20;
        deftness_t.text = DataBaseManager.deftnessPoint.ToString();
        deftness_g.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
        DataBaseManager.rhetoricPoint = 20;
        rhetoric_t.text = DataBaseManager.rhetoricPoint.ToString();
        rhetoric_g.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
        DataBaseManager.stealthPoint = 15;
        stealth_t.text = DataBaseManager.stealthPoint.ToString();
        stealth_g.fillAmount = (float)DataBaseManager.stealthPoint / 100;
        DataBaseManager.DisguisePoint = 10;
        Disguise_t.text = DataBaseManager.DisguisePoint.ToString();
        Disguise_g.fillAmount = (float)DataBaseManager.DisguisePoint / 100;
        DataBaseManager.dexSkillPoint = DataBaseManager.dex;
        dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
    }


    //dex
    public void evasionUP()
    {
        if (DataBaseManager.evasionPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.dexSkillPoint -= 5;
            DataBaseManager.evasionPoint += 5;
            evasion_t.text = DataBaseManager.evasionPoint.ToString();
            dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
            evasion_g.fillAmount = (float)DataBaseManager.evasionPoint / 100;
            nowSkill = "evasionUP";
        }
    }
    public void evasionDown()
    {
        if (DataBaseManager.evasionPoint > 30)
        {
            isClick = true;
            DataBaseManager.dexSkillPoint += 5;
            DataBaseManager.evasionPoint -= 5;
            evasion_t.text = DataBaseManager.evasionPoint.ToString();
            dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
            evasion_g.fillAmount = (float)DataBaseManager.evasionPoint / 100;
            nowSkill = "evasionDown";
        }
    }
    public void deftnessUP()
    {
        if (DataBaseManager.deftnessPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.dexSkillPoint -= 5;
            DataBaseManager.deftnessPoint += 5;
            deftness_t.text = DataBaseManager.deftnessPoint.ToString();
            dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
            deftness_g.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
            nowSkill = "deftnessUP";
        }
    }
    public void deftnessDown()
    {
        if (DataBaseManager.deftnessPoint > 20)
        {
            isClick = true;
            DataBaseManager.dexSkillPoint += 5;
            DataBaseManager.deftnessPoint -= 5;
            deftness_t.text = DataBaseManager.deftnessPoint.ToString();
            dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
            deftness_g.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
            nowSkill = "deftnessDown";
        }
    }
    public void rhetoricUP()
    {
        if (DataBaseManager.rhetoricPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.dexSkillPoint -= 5;
            DataBaseManager.rhetoricPoint += 5;
            rhetoric_t.text = DataBaseManager.rhetoricPoint.ToString();
            dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
            rhetoric_g.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
            nowSkill = "rhetoricUP";
        }
    }
    public void rhetoricDown()
    {
        if (DataBaseManager.rhetoricPoint > 20)
        {
            isClick = true;
            DataBaseManager.dexSkillPoint += 5;
            DataBaseManager.rhetoricPoint -= 5;
            rhetoric_t.text = DataBaseManager.rhetoricPoint.ToString();
            dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
            rhetoric_g.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
            nowSkill = "rhetoricDown";
        }
    }
    public void stealthUP()
    {
        if (DataBaseManager.stealthPoint < 90 && DataBaseManager.dexSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.dexSkillPoint -= 5;
            DataBaseManager.stealthPoint += 5;
            stealth_t.text = DataBaseManager.stealthPoint.ToString();
            dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
            stealth_g.fillAmount = (float)DataBaseManager.stealthPoint / 100;
            nowSkill = "stealthUP";
        }
    }
    public void stealthDown()
    {
        if (DataBaseManager.stealthPoint > 15)
        {
            isClick = true;
            DataBaseManager.dexSkillPoint += 5;
            DataBaseManager.stealthPoint -= 5;
            stealth_t.text = DataBaseManager.stealthPoint.ToString();
            dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
            stealth_g.fillAmount = (float)DataBaseManager.stealthPoint / 100;
            nowSkill = "stealthDown";
        }
    }
    public void DisguiseUP()
    {
        if (DataBaseManager.DisguisePoint < 90 && DataBaseManager.dexSkillPoint >= 5)
        {
            isClick = true;
            DataBaseManager.dexSkillPoint -= 5;
            DataBaseManager.DisguisePoint += 5;
            Disguise_t.text = DataBaseManager.DisguisePoint.ToString();
            dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
            Disguise_g.fillAmount = (float)DataBaseManager.DisguisePoint / 100;
            nowSkill = "DisguiseUP";
        }
    }
    public void DisguiseDown()
    {
        if (DataBaseManager.DisguisePoint > 10)
        {
            isClick = true;
            DataBaseManager.dexSkillPoint += 5;
            DataBaseManager.DisguisePoint -= 5;
            Disguise_t.text = DataBaseManager.DisguisePoint.ToString();
            dexPoint_t.text = DataBaseManager.dexSkillPoint.ToString();
            Disguise_g.fillAmount = (float)DataBaseManager.DisguisePoint / 100;
            nowSkill = "DisguiseDown";
        }
    }

    public void buttonUP()
    {
        isClick = false;
        minClickTime = 0.5f;
        clickTime = 0;
        nowSkill = "";
    }
    public void Back2stat()
    {
        setStatButton.SetActive(true);
        setStatWindow.SetActive(true);
        setSkillWindow.SetActive(false);
        allRandButton.SetActive(true);
    }

    public void resetSkillPoint()
    {
        DataBaseManager.martialArtsPoint = 25;
        martialArtsPoint_t.text = DataBaseManager.martialArtsPoint.ToString();
        martialArtsPoint_g.fillAmount = (float)DataBaseManager.martialArtsPoint / 100;
        DataBaseManager.gunShotPoint = 15;
        gunShotPoint_t.text = DataBaseManager.gunShotPoint.ToString();
        gunShotPoint_g.fillAmount = (float)DataBaseManager.gunShotPoint / 100;
        DataBaseManager.swordPoint = 20;
        swordPoint_t.text = DataBaseManager.swordPoint.ToString();
        swordPoint_g.fillAmount = (float)DataBaseManager.swordPoint / 100;
        DataBaseManager.ObservationPoint = 20;
        ObservationPoint_t.text = DataBaseManager.ObservationPoint.ToString();
        ObservationPoint_g.fillAmount = (float)DataBaseManager.ObservationPoint / 100;
        DataBaseManager.swimingPoint = 10;
        swimingPoint_t.text = DataBaseManager.swimingPoint.ToString();
        swimingPoint_g.fillAmount = (float)DataBaseManager.swimingPoint / 100;

        DataBaseManager.medicinePoint = 10;
        medicine_t.text = DataBaseManager.medicinePoint.ToString();
        medicine_g.fillAmount = (float)DataBaseManager.medicinePoint / 100;
        DataBaseManager.analysisPoint = 30;
        analysis_t.text = DataBaseManager.analysisPoint.ToString();
        analysis_g.fillAmount = (float)DataBaseManager.analysisPoint / 100;
        DataBaseManager.listeningPoint = 30;
        listening_t.text = DataBaseManager.listeningPoint.ToString();
        listening_g.fillAmount = (float)DataBaseManager.listeningPoint / 100;
        DataBaseManager.psychotherapyPoint = 5;
        psychotherapy_t.text = DataBaseManager.psychotherapyPoint.ToString();
        psychotherapy_g.fillAmount = (float)DataBaseManager.psychotherapyPoint / 100;
        DataBaseManager.occultPoint = 5;
        occult_t.text = DataBaseManager.occultPoint.ToString();
        occult_g.fillAmount = (float)DataBaseManager.occultPoint / 100;

        DataBaseManager.evasionPoint = 30;
        evasion_t.text = DataBaseManager.evasionPoint.ToString();
        evasion_g.fillAmount = (float)DataBaseManager.evasionPoint / 100;
        DataBaseManager.deftnessPoint = 20;
        deftness_t.text = DataBaseManager.deftnessPoint.ToString();
        deftness_g.fillAmount = (float)DataBaseManager.deftnessPoint / 100;
        DataBaseManager.rhetoricPoint = 20;
        rhetoric_t.text = DataBaseManager.rhetoricPoint.ToString();
        rhetoric_g.fillAmount = (float)DataBaseManager.rhetoricPoint / 100;
        DataBaseManager.stealthPoint = 15;
        stealth_t.text = DataBaseManager.stealthPoint.ToString();
        stealth_g.fillAmount = (float)DataBaseManager.stealthPoint / 100;
        DataBaseManager.DisguisePoint = 10;
        Disguise_t.text = DataBaseManager.DisguisePoint.ToString();
        Disguise_g.fillAmount = (float)DataBaseManager.DisguisePoint / 100;
    }


}
