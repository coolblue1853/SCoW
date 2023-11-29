using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ManageMain : MonoBehaviour
{
    public GameObject back;
    public Image backGroundImg;
    public GameObject select1;
    public GameObject select2;
    public GameObject select3;
    bool isSelectOn1 = false;
    bool isSelectOn2 = false;
    bool isSelectOn3 = false;
    Sequence mySequence;
    public void FadeIn()
    {
        SoundManager.Instance.PenCircle();
        back.SetActive(true);
        Tween fadeTween = backGroundImg.DOFade(1, 1.5f);
        fadeTween.OnComplete(GoSetCharacter);
    }
    public void GameQuit()
    {
        SoundManager.Instance.PenCircle();
        Application.Quit();
    }
    public void GoSetCharacter()
    {
        SceneManager.LoadScene("SetCharacter");
    }
    public void OpenTile()
    {
        if (isSelectOn1 == false)
        {
            isSelectOn1 = true;
            select1.SetActive(true);
        }
    }
    public void CloseTile()
    {
        if (isSelectOn1 == true)
        {
            isSelectOn1 = false;
            select1.SetActive(false);
        }
    }
    public void OpenTile2()
    {
        if (isSelectOn2 == false)
        {
            isSelectOn2 = true;
            select2.SetActive(true);
        }
    }
    public void CloseTile2()
    {
        if (isSelectOn2 == true)
        {
            isSelectOn2 = false;
            select2.SetActive(false);
        }
    }
    public void OpenTile3()
    {
        if (isSelectOn3 == false)
        {
            isSelectOn3 = true;
            select3.SetActive(true);
        }
    }
    public void CloseTile3()
    {
        if (isSelectOn3 == true)
        {
            isSelectOn3 = false;
            select3.SetActive(false);
        }
    }
}
