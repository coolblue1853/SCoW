using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Intro : MonoBehaviour
{
    public GameObject White_ob;

    public GameObject RogoImage_ob;
    public Image White;

    public Image RogoImage;

    public Animator Rogo;
    private void Awake()
    {

        Invoke("RogoStart", 1);
        Rogo.SetInteger("Rogo", 1);
    }

    public void RogoStart()
    {
        Rogo.SetInteger("Rogo", -1);
        Invoke("End", 4);
    }

    public void End()
    {
        Tween fadeTween = White.DOFade(0, 1.5f);
        Tween fadeTween2 = RogoImage.DOFade(0, 1.5f);
        fadeTween.OnComplete(Close);


    }

    public void Close()
    {
        White_ob.SetActive(false);

        RogoImage_ob.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
