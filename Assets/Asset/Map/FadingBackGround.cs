using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FadingBackGround : MonoBehaviour
{
    public GameObject Back;
   public Image BackGround;
    Sequence mySequence;
    Sequence mySequence2;
    // Start is called before the first frame update
    void Start()
    {


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
        StartCoroutine(FadeOut_IEnumerator());
    }
    IEnumerator FadeOut_IEnumerator()
    {
        mySequence2 = DOTween.Sequence()
        .SetDelay(1.5f)
        .Append(BackGround.DOFade(0, 1.5f).SetAutoKill());
        yield return mySequence2.WaitForCompletion();
        Back.SetActive(false);

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


    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
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