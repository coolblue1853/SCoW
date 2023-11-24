using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SetCharacter_ : MonoBehaviour
{
    public GameObject Back;
    public Image BackGround;


    public GameObject DemoEnd_Back;
    public Image DemoEnd_BackGround;
    public void DemoEnd_FadeOut()
    {
        DemoEnd_Back.SetActive(true);
        mySequence = DOTween.Sequence()
        .Append(DemoEnd_BackGround.DOFade(0, 1.5f).SetAutoKill());

    }

    Sequence mySequence;
    Sequence mySequence2;
    // Start is called before the first frame update
    void Start()
    {
        FadeOut();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeInOut()
    {
        StartCoroutine(FadeInOut_IEnumerator());
    }
    IEnumerator FadeInOut_IEnumerator()
    {
        Back.SetActive(true);
        mySequence = DOTween.Sequence()
        .Append(BackGround.DOFade(1, 1.5f).SetAutoKill());
        yield return mySequence.WaitForCompletion();
        mySequence2 = DOTween.Sequence()

        .SetDelay(1.5f)
        .Append(BackGround.DOFade(0, 1.5f).SetAutoKill());
        yield return mySequence2.WaitForCompletion();
        Back.SetActive(false);
    }
    public void FadeIn()
    {
        Back.SetActive(true);
        mySequence = DOTween.Sequence()
        .Append(BackGround.DOFade(1, 1.5f).SetAutoKill());

    }

    public void FadeOut()
    {
        Back.SetActive(true);
        Tween fadeTween2 = BackGround.DOFade(0, 1.5f);
        fadeTween2.OnComplete(goToGame);
       //    StartCoroutine(FadeOut_IEnumerator());
    }
    IEnumerator FadeOut_IEnumerator()
    {
        mySequence2 = DOTween.Sequence()
        .SetDelay(1.5f)
        .Append(BackGround.DOFade(0, 1.5f).SetAutoKill());
        yield return mySequence2.WaitForCompletion();
        Back.SetActive(false);

    }

    public void goToGame()
    {
        Back.SetActive(false);
    }
}
