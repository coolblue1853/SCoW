using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{

    //Bgm 

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
    public Animator 주인공움직임;
    public Slider bgm볼륨;
    public AudioSource mainBgm;


    public AudioClip 찰칵소리효과음;
    public Slider sfx볼륨;
    public AudioSource effectBgm;

    public AudioClip 달리기효과음;
    public AudioSource 효과음창;
    public AudioSource 효과음창_sec;

    public AudioSource 오디오소스_기어;
    public void PlayDice()
    {
        오디오소스_기어.Play();
    }



    public void EndDice()
    {
        오디오소스_기어.Stop();
    }

    bool 달리기효과음켜짐 = false;

    public AudioClip 문열고닫기효과음;
    public AudioClip 놑크효과음;

    public AudioClip 기계돌아가는소리효과음;



    public AudioClip PenLineClip;
    public AudioClip PenCircleClip;
    public AudioClip PaperClip;
    public void pen_Line()
    {
        효과음창_sec.Stop();
        효과음창_sec.PlayOneShot(PenLineClip);
    }
    public void pen_Circle()
    {
        효과음창_sec.Stop();
        효과음창_sec.PlayOneShot(PenCircleClip);
    }
    public void PaperClip_Play()
    {
        효과음창_sec.Stop();
        효과음창_sec.PlayOneShot(PaperClip);
    }
    public void Door_Sound()
    {
        //효과음창.Play();

        효과음창_sec.PlayOneShot(문열고닫기효과음);
    }
    public void Nock_Sound()
    {
        //효과음창.Play();

        효과음창_sec.PlayOneShot(놑크효과음);
    }

    public void 찰칵효과음함수()
    {
        효과음창_sec.PlayOneShot(찰칵소리효과음);

    }
    void 효과음관리()
    {

        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false)
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
            if (DataBaseManager.isDirecting == false )
            {

            }


        }
    }

    void bgm소리설정()
    {
        mainBgm.volume = DataBaseManager.sound_Volume;
         effectBgm.volume = DataBaseManager.sfx_Volume;
      //  오디오소스_기어.volume = DataBaseManager.sfx_Volume;
    }

    public void 슬라이드bgm관리()
    {
        DataBaseManager.sound_Volume = bgm볼륨.value;


    }
    public void 슬라이드효과관리()
    {

        DataBaseManager.sfx_Volume = sfx볼륨.value;

    }

    // Start is called before the first frame update
    void Start()
    {
        bgm볼륨.value = DataBaseManager.sound_Volume;
        sfx볼륨.value = DataBaseManager.sfx_Volume;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (SceneManager.GetActiveScene().name == "Insert" && mainBgm.clip != Main_Bgm)
        {

            mainBgm.clip = Main_Bgm;
            mainBgm.Play();

        }
       
        if (SceneManager.GetActiveScene().name == "SetCharacter" && mainBgm.clip != SetChar_Bgm)
        {

            mainBgm.clip = SetChar_Bgm;
            mainBgm.Play();

        }


        if (SceneManager.GetActiveScene().name == "Main")
        {
            if(DataBaseManager.nowPlace == "DetectiveOffice" && mainBgm.clip != Detective_Bgm)
            {
                if(mainBgm.clip == SetChar_Bgm)
                {
                    mainBgm.clip = Detective_Bgm;
                    Invoke("PlayBgm", 2);
                }
                else
                {
                    mainBgm.clip = Detective_Bgm;
                    Invoke("PlayBgm", 9);
                }

            }
            else if (DataBaseManager.nowPlace == "Client'shouse" && mainBgm.clip != ClientHouse_Bgm)
            {
                mainBgm.clip = ClientHouse_Bgm;
                Invoke("PlayBgm", 9);
            }
            else if (DataBaseManager.nowPlace == "DailyNews" && mainBgm.clip != Newspaper_Bgm)
            {
                mainBgm.clip = Newspaper_Bgm;
                Invoke("PlayBgm", 9);

            }
            else if (DataBaseManager.nowPlace == "University" && mainBgm.clip != University_Bgm)
            {
                mainBgm.clip = University_Bgm;
                Invoke("PlayBgm", 9);
            }
            else if (DataBaseManager.nowPlace == "Riverside" && mainBgm.clip != River_Bgm)
            {
                mainBgm.clip = River_Bgm;
                Invoke("PlayBgm", 9);
            }
            else if (DataBaseManager.nowPlace == "Hospital" && mainBgm.clip != Hospital_Bgm)
            {
                mainBgm.clip = Hospital_Bgm;
                Invoke("PlayBgm", 9);
            }
            else if (DataBaseManager.nowPlace == "Policeoffice" && mainBgm.clip != PoliceOffice_Bgm)
            {
                mainBgm.clip = PoliceOffice_Bgm;
                Invoke("PlayBgm", 9);
            }
            else if (DataBaseManager.nowPlace == "Slum" && mainBgm.clip != Slum_Bgm)
            {
                mainBgm.clip = Slum_Bgm;
                Invoke("PlayBgm", 9);
            }
            else if (DataBaseManager.nowPlace == "Bar" && mainBgm.clip != Bar_Bgm)
            {
                mainBgm.clip = Bar_Bgm;
                Invoke("PlayBgm", 9);
            }

        }

    }

    void PlayBgm()
    {
   
        mainBgm.Play();
    }
}
