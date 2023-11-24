using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class ManageMain : MonoBehaviour
{
    public GameObject Back;
    public Image BackGround;
    public GameObject tile;
    public GameObject tile2;
    public GameObject tile3;
    bool on = false;
    bool on2 = false;
    bool on3 = false;
    Sequence mySequence;
    public void FadeIn()
    {
        SoundManager.Instance.pen_Circle();
        Back.SetActive(true);
        Tween fadeTween = BackGround.DOFade(1, 1.5f);
        fadeTween.OnComplete(Go_SetCharacter);
    }
    public void Game_Quit()
    {
        SoundManager.Instance.pen_Circle();
        Application.Quit();
    }
    public void Go_SetCharacter()
    {
        SceneManager.LoadScene("SetCharacter");
    }
    public void Open_tile()
    {
        if (on == false)
        {
            on = true;
            tile.SetActive(true);
        }
    }
    public void Close_tile()
    {
        if (on == true)
        {
            on = false;
            tile.SetActive(false);
        }
    }
    public void Open_tile2()
    {
        if (on2 == false)
        {
            on2 = true;
            tile2.SetActive(true);
        }
    }
    public void Close_tile2()
    {
        if (on2 == true)
        {
            on2 = false;
            tile2.SetActive(false);
        }
    }
    public void Open_tile3()
    {
        if (on3 == false)
        {
            on3 = true;
            tile3.SetActive(true);
        }
    }
    public void Close_tile3()
    {
        if (on3 == true)
        {
            on3 = false;
            tile3.SetActive(false);
        }
    }
}
