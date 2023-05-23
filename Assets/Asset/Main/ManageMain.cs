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
    bool on = false;
    bool on2 = false;
    Sequence mySequence;

    public void FadeIn()
    {
        Back.SetActive(true);
        mySequence = DOTween.Sequence()
        .Append(BackGround.DOFade(1, 1.5f).SetAutoKill());

        Invoke("Go_SetCharacter", 1.8f);
    }


    public void Game_Quit()
    {
        Application.Quit();
    }

    public void Go_SetCharacter()
    {
        SceneManager.LoadScene("SetCharacter");
    }

    public void Open_tile()
    {
        if(on == false)
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
}
