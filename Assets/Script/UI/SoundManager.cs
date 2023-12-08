using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
public class SoundManager : MonoBehaviour
{
    public AudioClip battleBgm;
    public AudioClip clickSound;
    public AudioClip setTrunSound;
    //Bgm 
    public AudioClip counterAttack;
    public AudioClip evaision;
    public AudioClip martialArts;

    public AudioClip batReady;
    public AudioClip daggerReady;
    public AudioClip axeReady;
    public AudioClip smallPisolReady;
    public AudioClip revolverReady;
    public AudioClip rifleReady;
    public AudioClip shotgunReady;

    public AudioClip rockReady;
    public AudioClip molotovReady;

    public AudioClip batAttack;
    public AudioClip daggerAttack;
    public AudioClip axeAttack;
    public AudioClip smallPisolAttack;
    public AudioClip revolverAttack;
    public AudioClip rifleAttack;
    public AudioClip shotgunAttack;

    public AudioClip rockAttack;
    public AudioClip molotovAttack;

    public AudioClip deftnessMiss;

    public AudioClip smallPisolHit;
    public AudioClip revolverHit;
    public AudioClip rifleHit;
    public AudioClip shotgunHit;

    public AudioClip smallPisolReload;
    public AudioClip revolverReload;
    public AudioClip rifleReload;
    public AudioClip shotgunReload;

    public void BattleSound(string sound)
    {
        if (sound == "Evaision")
        {
            sfxSecendSource.PlayOneShot(evaision);
        }
        else if (sound == "MartialArts")
        {
            sfxSecendSource.PlayOneShot(martialArts);
        }
        else if (sound == "Bat_Reddy")
        {
            sfxSecendSource.PlayOneShot(batReady);
        }
        else if (sound == "Dagger_Reddy")
        {
            sfxSecendSource.PlayOneShot(daggerReady);
        }
        else if (sound == "Axe_Reddy")
        {
            sfxSecendSource.PlayOneShot(axeReady);
        }
        else if (sound == "SmallPisol_Reddy")
        {
            sfxSecendSource.PlayOneShot(smallPisolReady);
        }
        else if (sound == "Revolver_Reddy")
        {
            sfxSecendSource.PlayOneShot(revolverReady);
        }
        else if (sound == "Rifle_Reddy")
        {
            sfxSecendSource.PlayOneShot(rifleReady);
        }
        else if (sound == "Shotgun_Reddy")
        {
            sfxSecendSource.PlayOneShot(shotgunReady);
        }
        else if (sound == "Bat_Attack")
        {
            sfxSecendSource.PlayOneShot(batAttack);
        }
        else if (sound == "Dagger_Attack")
        {
            sfxSecendSource.PlayOneShot(daggerAttack);
        }
        else if (sound == "Axe_Attack")
        {
            sfxSecendSource.PlayOneShot(axeAttack);
        }
        else if (sound == "SmallPisol_Attack")
        {
            sfxSecendSource.PlayOneShot(smallPisolAttack);
        }
        else if (sound == "Revolver_Attack")
        {
            sfxSecendSource.PlayOneShot(revolverAttack);
        }
        else if (sound == "Rifle_Attack")
        {
            sfxSecendSource.PlayOneShot(rifleAttack);
        }
        else if (sound == "Shotgun_Attack")
        {
            sfxSecendSource.PlayOneShot(shotgunAttack);
        }
        else if (sound == "SmallPisol_Hit")
        {
            sfxSecendSource.PlayOneShot(smallPisolHit);
        }
        else if (sound == "Revolver_Hit")
        {
            sfxSecendSource.PlayOneShot(revolverHit);
        }
        else if (sound == "Rifle_Hit")
        {
            sfxSecendSource.PlayOneShot(rifleHit);
        }
        else if (sound == "Shotgun_Hit")
        {
            sfxSecendSource.PlayOneShot(shotgunHit);
        }
        else if (sound == "SmallPisol_Reload")
        {
            sfxSecendSource.PlayOneShot(smallPisolReload);
        }
        else if (sound == "Revolver_Reload")
        {
            sfxSecendSource.PlayOneShot(revolverReload);
        }
        else if (sound == "Rifle_Reload")
        {
            sfxSecendSource.PlayOneShot(rifleReload);
        }
        else if (sound == "Shotgun_Reload")
        {
            sfxSecendSource.PlayOneShot(shotgunReload);
        }
        else if (sound == "Rock_Reddy")
        {
            sfxSecendSource.PlayOneShot(rockReady);
        }
        else if (sound == "Molotov_Reddy")
        {
            sfxSecendSource.PlayOneShot(molotovReady);
        }
        else if (sound == "Rock_Attack")
        {
            sfxSecendSource.PlayOneShot(rockAttack);
        }
        else if (sound == "Molotov_Attack")
        {
            sfxSecendSource.PlayOneShot(molotovAttack);
        }
        else if (sound == "DeftNess_Miss")
        {
            sfxSecendSource.PlayOneShot(deftnessMiss);
        }
        else if (sound == "Posion_Attack")
        {
            sfxSecendSource.PlayOneShot(deftnessMiss); // 이거 독 소리로 바꾸어 줘야 함
        }
        else if (sound == "Posion_Miss")
        {
            sfxSecendSource.PlayOneShot(deftnessMiss); // 이거 독 소리로 바꾸어 줘야 함
        }
    }
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public Slider bgmVolum;
    public AudioSource mainBgmSource;
    public AudioClip setSfxClip;
    public Slider sfxVolum;
    public AudioSource effectBgm;
    public AudioClip runSound;
    public AudioSource sfxSource;
    public AudioSource sfxSecendSource;
    public AudioSource sfxGearSource;
    public void PlayDice()
    {
        sfxGearSource.Play();
    }
    public void EndDice()
    {
        sfxGearSource.Stop();
    }
    public bool irRunSoundOn = false;
    public AudioClip doorOpenClip;
    public AudioClip nockClip;
    public AudioClip gearClip;
    public AudioClip penLineClip;
    public AudioClip penCircleClip;
    public AudioClip paperClip;
    public AudioClip dialogClip;
    public void DalogSound()
    {
        sfxSecendSource.Stop();
        sfxSecendSource.PlayOneShot(dialogClip);
    }
    public void PenLine()
    {
        sfxSecendSource.Stop();
        sfxSecendSource.PlayOneShot(penLineClip);
    }
    public void PenCircle()
    {
        sfxSecendSource.Stop();
        sfxSecendSource.PlayOneShot(penCircleClip);
    }
    public void PaperClipPlay()
    {
        sfxSecendSource.Stop();
        sfxSecendSource.PlayOneShot(paperClip);
    }
    public void DoorSound()
    {
        sfxSecendSource.PlayOneShot(doorOpenClip);
    }
    public void NockSound()
    {
        sfxSecendSource.PlayOneShot(nockClip);
    }
    public void ClickSound()
    {
        sfxSecendSource.PlayOneShot(setSfxClip);
    }
    public void ClickSoundPlay()
    {
        sfxSecendSource.PlayOneShot(clickSound);
    }
    public void SetTrunSoundPlay()
    {
        sfxSecendSource.PlayOneShot(setTrunSound);
    }
    void ManageSfx()
    {
        if ((DataBaseManager.isActiveDialog1 == true || DataBaseManager.isRollet == true || DataBaseManager.isDirecting == true || DataBaseManager.isOpenUi == true) && sfxSource.isPlaying == true)
        {
            sfxSource.Stop();
        }
        if (DataBaseManager.workSound == true && irRunSoundOn == false)
        {
            irRunSoundOn = true;
            sfxSource.clip = runSound;
            sfxSource.PlayOneShot(runSound);
        }
        else if (DataBaseManager.workSound == false)
        {
            sfxSource.clip = null;
            irRunSoundOn = false;
            sfxSource.Stop();
            if (DataBaseManager.isDirecting == false)
            {
            }
        }
    }

    void SetBgmVolum()
    {
        mainBgmSource.volume = DataBaseManager.soundVolume;
        effectBgm.volume = DataBaseManager.sfxVolume;
        sfxSource.volume = DataBaseManager.sfxVolume;
        sfxSecendSource.volume = DataBaseManager.sfxVolume;
        sfxGearSource.volume = DataBaseManager.sfxVolume;
        //  오디오소스_기어.volume = DataBaseManager.sfx_Volume;
    }

    public void SetSlideBgm()
    {
        DataBaseManager.soundVolume = bgmVolum.value;
    }
    public void SetSlideSfx()
    {
        DataBaseManager.sfxVolume = sfxVolum.value;
    }

    // Start is called before the first frame update
    void Start()
    {
        bgmVolum.value = DataBaseManager.soundVolume;
        sfxVolum.value = DataBaseManager.sfxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (DataBaseManager.isDebuffDeftness == true && (DataBaseManager.sfxVolume != 0 || DataBaseManager.soundVolume != 0))
        {
            DataBaseManager.sfxVolume = 0;
            DataBaseManager.soundVolume = 0;
            bgmVolum.value = 0;
            sfxVolum.value = 0;
        }

        BGMManage();
        ManageSfx();
        SetSlideSfx();
        SetSlideBgm();
        SetBgmVolum();
    }

    public AudioClip mainBgmClip;
    public AudioClip setCharBgmClip;
    public AudioClip detectiveBgmClip;
    public AudioClip hospitalBgmClip;
    public AudioClip slumBgmClip;
    public AudioClip newspaperBgmClip;
    public AudioClip clientHouseBgmClip;
    public AudioClip universityBgmClip;
    public AudioClip riverBgmClip;
    public AudioClip policeOfficeBgmClip;
    public AudioClip barBgmClip;

    public AudioClip gunshopBgmClip;
    public AudioClip sewerOfficeBgmClip;
    public AudioClip inSewerBgmClip;
    public AudioClip nightmareBgmClip;
    public void BGMManage()
    {
        if (SceneManager.GetActiveScene().name == "Insert" && mainBgmSource.clip != mainBgmClip)
        {
            mainBgmSource.clip = mainBgmClip;
            mainBgmSource.Play();
        }

        else if (SceneManager.GetActiveScene().name == "SetCharacter" && mainBgmSource.clip != setCharBgmClip)
        {
            mainBgmSource.clip = setCharBgmClip;
            mainBgmSource.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Main")
        {
            if (DataBaseManager.nowPlace == "DetectiveOffice" && mainBgmSource.clip != detectiveBgmClip && DataBaseManager.isNightmare == false)
            {
                if (mainBgmSource.clip == setCharBgmClip)
                {
                    mainBgmSource.clip = detectiveBgmClip;
                    Invoke("PlayBgm", 2);
                }
                else
                {
                    mainBgmSource.clip = detectiveBgmClip;
                    Invoke("PlayBgm", 6);
                }
            }
            if (DataBaseManager.nowPlace == "DetectiveOffice" && mainBgmSource.clip != nightmareBgmClip && DataBaseManager.isNightmare == true)
            {
                mainBgmSource.clip = nightmareBgmClip;
                Invoke("PlayBgm", 6);
            }
            else if (DataBaseManager.nowPlace == "Client'shouse" && mainBgmSource.clip != clientHouseBgmClip)
            {
                mainBgmSource.clip = clientHouseBgmClip;
                Invoke("PlayBgm", 6);
            }

            else if (DataBaseManager.nowPlace == "DailyNews" && mainBgmSource.clip != newspaperBgmClip)
            {
                mainBgmSource.clip = newspaperBgmClip;
                Invoke("PlayBgm", 0.5f);
            }
            else if (DataBaseManager.nowPlace == "University" && mainBgmSource.clip != universityBgmClip)
            {
                mainBgmSource.clip = universityBgmClip;
                Invoke("PlayBgm", 6);
            }
            else if (DataBaseManager.nowPlace == "Riverside" && mainBgmSource.clip != riverBgmClip)
            {
                mainBgmSource.clip = riverBgmClip;
                Invoke("PlayBgm", 6);
            }
            else if (DataBaseManager.nowPlace == "Hospital" && mainBgmSource.clip != hospitalBgmClip)
            {
                mainBgmSource.clip = hospitalBgmClip;
                Invoke("PlayBgm", 6);
            }
            else if (DataBaseManager.nowPlace == "Policeoffice" && mainBgmSource.clip != policeOfficeBgmClip)
            {
                mainBgmSource.clip = policeOfficeBgmClip;
                Invoke("PlayBgm", 6);
            }
            else if (DataBaseManager.nowPlace == "Slum" && mainBgmSource.clip != slumBgmClip)
            {
                mainBgmSource.clip = slumBgmClip;
                Invoke("PlayBgm", 6);
            }
            else if (DataBaseManager.nowPlace == "Bar" && mainBgmSource.clip != barBgmClip)
            {
                mainBgmSource.clip = barBgmClip;
                Invoke("PlayBgm", 6);
            }
            else if ((DataBaseManager.nowPlace == "BattleRoad" && mainBgmSource.clip != battleBgm))
            {
                mainBgmSource.clip = battleBgm;
                Invoke("PlayBgm", 0.5f);
            }
            else if ((DataBaseManager.nowPlace == "Gunshop" && mainBgmSource.clip != gunshopBgmClip))
            {
                mainBgmSource.clip = gunshopBgmClip;
                Invoke("PlayBgm", 6);
            }
            else if ((DataBaseManager.nowPlace == "SewerOffice" && mainBgmSource.clip != sewerOfficeBgmClip))
            {
                mainBgmSource.clip = sewerOfficeBgmClip;
                Invoke("PlayBgm", 6);
            }
            else if ((DataBaseManager.nowPlace == "InSewer" && mainBgmSource.clip != inSewerBgmClip))
            {
                mainBgmSource.clip = inSewerBgmClip;
                Invoke("PlayBgm", 6);
            }
            else if ((DataBaseManager.nowPlace == "NoonEvent" && mainBgmSource.clip != nightmareBgmClip))
            {
                mainBgmSource.clip = nightmareBgmClip;
                Invoke("PlayBgm", 6);
            }
            else if ((DataBaseManager.nowPlace == "InSewer" && mainBgmSource.clip != inSewerBgmClip))
            {
                mainBgmSource.clip = inSewerBgmClip;
                Invoke("PlayBgm", 0.5f);
            }
        }
    }

    void PlayBgm()
    {
        Debug.Log("작동중");
        mainBgmSource.Play();
    }
}
