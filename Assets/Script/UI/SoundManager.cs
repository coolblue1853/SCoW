using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{
    public AudioClip Battle_Bgm;
    public AudioClip ClickSound;
    public AudioClip SetTrunSound;
    //Bgm 
    public AudioClip CounterAttack;
    public AudioClip Evaision;
    public AudioClip MartialArts;

    public AudioClip Bat_Reddy;
    public AudioClip Dagger_Reddy;
    public AudioClip Axe_Reddy;
    public AudioClip SmallPisol_Reddy;
    public AudioClip Revolver_Reddy;
    public AudioClip Rifle_Reddy;
    public AudioClip Shotgun_Reddy;

    public AudioClip Rock_Reddy;
    public AudioClip Molotov_Reddy;

    public AudioClip Bat_Attack;
    public AudioClip Dagger_Attack;
    public AudioClip Axe_Attack;
    public AudioClip SmallPisol_Attack;
    public AudioClip Revolver_Attack;
    public AudioClip Rifle_Attack;
    public AudioClip Shotgun_Attack;

    public AudioClip Rock_Attack;
    public AudioClip Molotov_Attack;

    public AudioClip DeftNess_Miss;

    public AudioClip SmallPisol_Hit;
    public AudioClip Revolver_Hit;
    public AudioClip Rifle_Hit;
    public AudioClip Shotgun_Hit;

    public AudioClip SmallPisol_Reload;
    public AudioClip Revolver_Reload;
    public AudioClip Rifle_Reload;
    public AudioClip Shotgun_Reload;

    public void Battle_Sound(string sound)
    {
        if (sound == "Evaision")
        {
            SFX_Secend.PlayOneShot(Evaision);
        }
        else if (sound == "MartialArts")
        {
            SFX_Secend.PlayOneShot(MartialArts);
        }
        else if (sound == "Bat_Reddy")
        {
            SFX_Secend.PlayOneShot(Bat_Reddy);
        }
        else if (sound == "Dagger_Reddy")
        {
            SFX_Secend.PlayOneShot(Dagger_Reddy);
        }
        else if (sound == "Axe_Reddy")
        {
            SFX_Secend.PlayOneShot(Axe_Reddy);
        }
        else if (sound == "SmallPisol_Reddy")
        {
            SFX_Secend.PlayOneShot(SmallPisol_Reddy);
        }
        else if (sound == "Revolver_Reddy")
        {
            SFX_Secend.PlayOneShot(Revolver_Reddy);
        }
        else if (sound == "Rifle_Reddy")
        {
            SFX_Secend.PlayOneShot(Rifle_Reddy);
        }
        else if (sound == "Shotgun_Reddy")
        {
            SFX_Secend.PlayOneShot(Shotgun_Reddy);
        }
        else if (sound == "Bat_Attack")
        {
            SFX_Secend.PlayOneShot(Bat_Attack);
        }
        else if (sound == "Dagger_Attack")
        {
            SFX_Secend.PlayOneShot(Dagger_Attack);
        }
        else if (sound == "Axe_Attack")
        {
            SFX_Secend.PlayOneShot(Axe_Attack);
        }
        else if (sound == "SmallPisol_Attack")
        {
            SFX_Secend.PlayOneShot(SmallPisol_Attack);
        }
        else if (sound == "Revolver_Attack")
        {
            SFX_Secend.PlayOneShot(Revolver_Attack);
        }
        else if (sound == "Rifle_Attack")
        {
            SFX_Secend.PlayOneShot(Rifle_Attack);
        }
        else if (sound == "Shotgun_Attack")
        {
            SFX_Secend.PlayOneShot(Shotgun_Attack);
        }
        else if (sound == "SmallPisol_Hit")
        {
            SFX_Secend.PlayOneShot(SmallPisol_Hit);
        }
        else if (sound == "Revolver_Hit")
        {
            SFX_Secend.PlayOneShot(Revolver_Hit);
        }
        else if (sound == "Rifle_Hit")
        {
            SFX_Secend.PlayOneShot(Rifle_Hit);
        }
        else if (sound == "Shotgun_Hit")
        {
            SFX_Secend.PlayOneShot(Shotgun_Hit);
        }
        else if (sound == "SmallPisol_Reload")
        {
            SFX_Secend.PlayOneShot(SmallPisol_Reload);
        }
        else if (sound == "Revolver_Reload")
        {
            SFX_Secend.PlayOneShot(Revolver_Reload);
        }
        else if (sound == "Rifle_Reload")
        {
            SFX_Secend.PlayOneShot(Rifle_Reload);
        }
        else if (sound == "Shotgun_Reload")
        {
            SFX_Secend.PlayOneShot(Shotgun_Reload);
        }
        else if (sound == "Rock_Reddy")
        {
            SFX_Secend.PlayOneShot(Rock_Reddy);
        }
        else if (sound == "Molotov_Reddy")
        {
            SFX_Secend.PlayOneShot(Molotov_Reddy);
        }
        else if (sound == "Rock_Attack")
        {
            SFX_Secend.PlayOneShot(Rock_Attack);
        }
        else if (sound == "Molotov_Attack")
        {
            SFX_Secend.PlayOneShot(Molotov_Attack);
        }
        else if (sound == "DeftNess_Miss")
        {
            SFX_Secend.PlayOneShot(DeftNess_Miss);
        }
        else if (sound == "Posion_Attack")
        {
            SFX_Secend.PlayOneShot(DeftNess_Miss); // 이거 독 소리로 바꾸어 줘야 함
        }
        else if (sound == "Posion_Miss")
        {
            SFX_Secend.PlayOneShot(DeftNess_Miss); // 이거 독 소리로 바꾸어 줘야 함
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
    public Animator PlayerMove;
    public Slider BGMVolum;

    public AudioSource MainBgmSource;
    public AudioClip SetSfxClip;
    public Slider sfx볼륨;
    public AudioSource effectBgm;
    public AudioClip 달리기효과음;
    public AudioSource 효과음창;
    public AudioSource SFX_Secend;
    public AudioSource 오디오소스_기어;
    public void PlayDice()
    {
        오디오소스_기어.Play();
    }
    public void EndDice()
    {
        오디오소스_기어.Stop();
    }
    public bool 달리기효과음켜짐 = false;
    public AudioClip 문열고닫기효과음;
    public AudioClip 놑크효과음;
    public AudioClip 기계돌아가는소리효과음;
    public AudioClip PenLineClip;
    public AudioClip PenCircleClip;
    public AudioClip PaperClip;
    public AudioClip DialogClip;
    public void DalogSound()
    {
        SFX_Secend.Stop();
        SFX_Secend.PlayOneShot(DialogClip);
    }
    public void pen_Line()
    {
        SFX_Secend.Stop();
        SFX_Secend.PlayOneShot(PenLineClip);
    }
    public void pen_Circle()
    {
        SFX_Secend.Stop();
        SFX_Secend.PlayOneShot(PenCircleClip);
    }
    public void PaperClip_Play()
    {
        SFX_Secend.Stop();
        SFX_Secend.PlayOneShot(PaperClip);
    }
    public void Door_Sound()
    {
        SFX_Secend.PlayOneShot(문열고닫기효과음);
    }
    public void Nock_Sound()
    {
        SFX_Secend.PlayOneShot(놑크효과음);
    }
    public void 찰칵효과음함수()
    {
        SFX_Secend.PlayOneShot(SetSfxClip);
    }
    public void ClickSound_Play()
    {
        SFX_Secend.PlayOneShot(ClickSound);
    }
    public void SetTrunSound_Play()
    {
        SFX_Secend.PlayOneShot(SetTrunSound);
    }
    void 효과음관리()
    {
        if ((DataBaseManager.isActiveDialog1 == true || DataBaseManager.isRollet == true || DataBaseManager.isDirecting == true || DataBaseManager.isOpenUi == true) && 효과음창.isPlaying == true)
        {
            효과음창.Stop();
        }
        if (DataBaseManager.workSound == true && 달리기효과음켜짐 == false)
        {
            달리기효과음켜짐 = true;
            효과음창.clip = 달리기효과음;
            효과음창.PlayOneShot(달리기효과음);
        }
        else if (DataBaseManager.workSound == false)
        {
            효과음창.clip = null;
            달리기효과음켜짐 = false;
            효과음창.Stop();
            if (DataBaseManager.isDirecting == false)
            {
            }
        }
    }

    void bgm소리설정()
    {
        MainBgmSource.volume = DataBaseManager.sound_Volume;
        effectBgm.volume = DataBaseManager.sfx_Volume;
        효과음창.volume = DataBaseManager.sfx_Volume;
        SFX_Secend.volume = DataBaseManager.sfx_Volume;
        오디오소스_기어.volume = DataBaseManager.sfx_Volume;
        //  오디오소스_기어.volume = DataBaseManager.sfx_Volume;
    }

    public void 슬라이드bgm관리()
    {
        DataBaseManager.sound_Volume = BGMVolum.value;
    }
    public void 슬라이드효과관리()
    {
        DataBaseManager.sfx_Volume = sfx볼륨.value;
    }

    // Start is called before the first frame update
    void Start()
    {
        BGMVolum.value = DataBaseManager.sound_Volume;
        sfx볼륨.value = DataBaseManager.sfx_Volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (DataBaseManager.isDebuff_Deftness == true && (DataBaseManager.sfx_Volume != 0 || DataBaseManager.sound_Volume != 0))
        {
            DataBaseManager.sfx_Volume = 0;
            DataBaseManager.sound_Volume = 0;
            BGMVolum.value = 0;
            sfx볼륨.value = 0;
        }

        BGM_Manage();
        효과음관리();
        슬라이드효과관리();
        슬라이드bgm관리();
        bgm소리설정();
    }

    public AudioClip Main_Bgm;
    public AudioClip SetChar_Bgm;
    public AudioClip Detective_Bgm;
    public AudioClip Hospital_Bgm;
    public AudioClip Slum_Bgm;
    public AudioClip Newspaper_Bgm;
    public AudioClip ClientHouse_Bgm;
    public AudioClip University_Bgm;
    public AudioClip River_Bgm;
    public AudioClip PoliceOffice_Bgm;
    public AudioClip Bar_Bgm;
    void BGM_Manage()
    {
        if (SceneManager.GetActiveScene().name == "Insert" && MainBgmSource.clip != Main_Bgm)
        {
            MainBgmSource.clip = Main_Bgm;
            MainBgmSource.Play();
        }

        else if (SceneManager.GetActiveScene().name == "SetCharacter" && MainBgmSource.clip != SetChar_Bgm)
        {
            MainBgmSource.clip = SetChar_Bgm;
            MainBgmSource.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Main")
        {
            if (DataBaseManager.BGMChangeChecker == true)
            {
                DataBaseManager.BGMChangeChecker = false;
                if (DataBaseManager.nowPlace == "DetectiveOffice" && MainBgmSource.clip != Detective_Bgm)
                {
                    if (MainBgmSource.clip == SetChar_Bgm)
                    {
                        MainBgmSource.clip = Detective_Bgm;
                        Invoke("PlayBgm", 2);
                    }
                    else
                    {
                        MainBgmSource.clip = Detective_Bgm;
                        Invoke("PlayBgm", 9);
                    }
                }
                else if (DataBaseManager.nowPlace == "Client'shouse" && MainBgmSource.clip != ClientHouse_Bgm)
                {
                    MainBgmSource.clip = ClientHouse_Bgm;
                    Invoke("PlayBgm", 9);
                }
                else if (DataBaseManager.nowPlace == "DailyNews" && MainBgmSource.clip != Newspaper_Bgm)
                {
                    MainBgmSource.clip = Newspaper_Bgm;
                    Invoke("PlayBgm", 9);
                }
                else if (DataBaseManager.nowPlace == "University" && MainBgmSource.clip != University_Bgm)
                {
                    MainBgmSource.clip = University_Bgm;
                    Invoke("PlayBgm", 9);
                }
                else if (DataBaseManager.nowPlace == "Riverside" && MainBgmSource.clip != River_Bgm)
                {
                    MainBgmSource.clip = River_Bgm;
                    Invoke("PlayBgm", 9);
                }
                else if (DataBaseManager.nowPlace == "Hospital" && MainBgmSource.clip != Hospital_Bgm)
                {
                    MainBgmSource.clip = Hospital_Bgm;
                    Invoke("PlayBgm", 9);
                }
                else if (DataBaseManager.nowPlace == "Policeoffice" && MainBgmSource.clip != PoliceOffice_Bgm)
                {
                    MainBgmSource.clip = PoliceOffice_Bgm;
                    Invoke("PlayBgm", 9);
                }
                else if (DataBaseManager.nowPlace == "Slum" && MainBgmSource.clip != Slum_Bgm)
                {
                    MainBgmSource.clip = Slum_Bgm;
                    Invoke("PlayBgm", 9);
                }
                else if (DataBaseManager.nowPlace == "Bar" && MainBgmSource.clip != Bar_Bgm)
                {
                    MainBgmSource.clip = Bar_Bgm;
                    Invoke("PlayBgm", 9);
                }
                else if ((DataBaseManager.nowPlace == "BattleRoad" && MainBgmSource.clip != Battle_Bgm))
                {
                    MainBgmSource.clip = Battle_Bgm;
                    Invoke("PlayBgm", 0.5f);
                }
            }
            else if ((DataBaseManager.nowPlace == "InSewer" && MainBgmSource.clip != ClientHouse_Bgm))
            {
                MainBgmSource.clip = ClientHouse_Bgm;
                Invoke("PlayBgm", 0.5f);
            }
        }
    }

    void PlayBgm()
    {
        MainBgmSource.Play();
    }
}
