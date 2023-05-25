using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class FadingBackGround : MonoBehaviour
{
    public GameObject Back;
   public Image BackGround;

    public void goToMain()
    {
        DataBaseManager.instance.ResetData();
        SceneManager.LoadScene("Insert");
    }
    public GameObject DemoEnd_Back;
    public Image DemoEnd_BackGround;

    public GameObject EndMenu;
    public void DemoEnd_FadeIn()
    {

        DemoEnd_Back.SetActive(true);
        Tween fadeTween = DemoEnd_BackGround.DOFade(1, 1.5f);
        fadeTween.OnComplete(EndDemo);
    }
    private void EndDemo()
    {
        EndMenu.SetActive(true);
    }
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
        DataBaseManager.isDirecting = false;
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
        DataBaseManager.isDirecting = false;
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
