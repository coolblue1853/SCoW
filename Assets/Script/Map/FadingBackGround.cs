using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class FadingBackGround : MonoBehaviour
{
    public GameObject backgroundOb;
    public Image backGroundImg;
    public GameObject castIn;
    public Image castInBackground;
    public GameObject castOut;
    public Image castOutBackground;
    public void GoToMain()
    {
        DataBaseManager.instance.ResetData();
        SceneManager.LoadScene("Insert");
    }
    public GameObject demoEndBack;
    public Image demoEndBackGround;
    public GameObject endMenu;
    public void DemoEndFadeIn()
    {
        demoEndBack.SetActive(true);
        Tween fadeTween = demoEndBackGround.DOFade(1, 1.5f);
        fadeTween.OnComplete(EndDemo);
    }
    private void EndDemo()
    {
        endMenu.SetActive(true);
    }
    Sequence mySequence;
    Sequence mySequence2;

    public void CastInOut()
    {
        StartCoroutine(CastInOutIEnumerator());
    }
    IEnumerator CastInOutIEnumerator()
    {
        castIn.SetActive(true);
        mySequence = DOTween.Sequence()
        .Append(castInBackground.DOFillAmount(1, 0.7f).SetAutoKill());
        yield return mySequence.WaitForCompletion();
        castIn.SetActive(false);
        castOut.SetActive(true);
        mySequence2 = DOTween.Sequence()
        .SetDelay(0.1f)
        .Append(castOutBackground.DOFillAmount(0, 0.7f).SetAutoKill());
        yield return mySequence2.WaitForCompletion();
        DataBaseManager.isDirecting = false;
        castOut.SetActive(false);
        castInBackground.fillAmount = 0;
        castOutBackground.fillAmount = 1;
    }
    public void FadeInOut()
    {
        StartCoroutine(FadeInOutIEnumerator());
    }
    IEnumerator FadeInOutIEnumerator()
    {
        backgroundOb.SetActive(true);
        mySequence = DOTween.Sequence()
        .Append(backGroundImg.DOFade(1, 1f).SetAutoKill());
        yield return mySequence.WaitForCompletion();
        mySequence2 = DOTween.Sequence()
        .SetDelay(1f)
        .Append(backGroundImg.DOFade(0, 1f).SetAutoKill());
        yield return mySequence2.WaitForCompletion();
        DataBaseManager.isDirecting = false;
        backgroundOb.SetActive(false);
    }
    public void FadeIn()
    {
        backgroundOb.SetActive(true);
        mySequence = DOTween.Sequence()
        .Append(backGroundImg.DOFade(1, 1f).SetAutoKill());
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutIEnumerator());
    }
    IEnumerator FadeOutIEnumerator()
    {
        mySequence2 = DOTween.Sequence()
        .SetDelay(1.5f)
        .Append(backGroundImg.DOFade(0, 1f).SetAutoKill());
        yield return mySequence2.WaitForCompletion();
        DataBaseManager.isDirecting = false;
        backgroundOb.SetActive(false);

    }
    private static FadingBackGround instance = null;
    private void Awake()
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

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static FadingBackGround Instance
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
}
