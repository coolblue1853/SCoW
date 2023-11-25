using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Intro : MonoBehaviour
{
    public GameObject whiteOb;
    public GameObject rogoImageOb;
    public Image white;
    public Image rogoImage;
    public Animator rogoAnim;
    private void Awake()
    {
        Invoke("RogoStart", 1);
        rogoAnim.SetInteger("Rogo", 1);
    }
    public void RogoStart()
    {
        rogoAnim.SetInteger("Rogo", -1);
        Invoke("End", 4);
    }
    public void End()
    {
        Tween fadeTween = white.DOFade(0, 1.5f);
        Tween fadeTween2 = rogoImage.DOFade(0, 1.5f);
        fadeTween.OnComplete(Close);
    }
    public void Close()
    {
        whiteOb.SetActive(false);
        rogoImageOb.SetActive(false);
    }
}
