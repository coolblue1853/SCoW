using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SetCharacter_ : MonoBehaviour
{
    public GameObject backOb;
    public Image backGroundImg;
    public GameObject demoEndBackOb;
    public Image demoEndBackGroundImg;
    public void DemoEnd_FadeOut()
    {
        demoEndBackOb.SetActive(true);
        mySequence = DOTween.Sequence()
        .Append(demoEndBackGroundImg.DOFade(0, 1.5f).SetAutoKill());
    }
    Sequence mySequence;
    Sequence mySequence2;
    // Start is called before the first frame update
    void Start()
    {
        FadeOut();
    }

    public void FadeInOut()
    {
        StartCoroutine(FadeInOut_IEnumerator());
    }
    IEnumerator FadeInOut_IEnumerator()
    {
        backOb.SetActive(true);
        mySequence = DOTween.Sequence()
        .Append(backGroundImg.DOFade(1, 1.5f).SetAutoKill());
        yield return mySequence.WaitForCompletion();
        mySequence2 = DOTween.Sequence()
        .SetDelay(1.5f)
        .Append(backGroundImg.DOFade(0, 1.5f).SetAutoKill());
        yield return mySequence2.WaitForCompletion();
        backOb.SetActive(false);
    }
    public void FadeIn()
    {
        backOb.SetActive(true);
        mySequence = DOTween.Sequence()
        .Append(backGroundImg.DOFade(1, 1.5f).SetAutoKill());
    }
    public void FadeOut()
    {
        backOb.SetActive(true);
        Tween fadeTween2 = backGroundImg.DOFade(0, 1.5f);
        fadeTween2.OnComplete(goToGame);
    }
    IEnumerator FadeOut_IEnumerator()
    {
        mySequence2 = DOTween.Sequence()
        .SetDelay(1.5f)
        .Append(backGroundImg.DOFade(0, 1.5f).SetAutoKill());
        yield return mySequence2.WaitForCompletion();
        backOb.SetActive(false);
    }
    public void goToGame()
    {
        backOb.SetActive(false);
    }
}
