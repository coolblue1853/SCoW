using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    public GameObject fabian1;
    public GameObject fabian2;
    public GameObject fabian3;
    public GameObject fabian4;
    public GameObject fabian5;
    public GameObject fabian6;
    public GameObject fabian7;
    public GameObject fabian8;
    public GameObject fabian9;

    GameObject beforObj = null;
    bool isFirstShowILL = true;

    public void Fabian1Out()
    {
        ImageOut(fabian1);
    }
    public void Fabian2Out()
    {
        ImageOut(fabian2);
    }
    public void Fabian3Out()
    {
        ImageOut(fabian3);
    }
    public void Fabian4Out()
    {
        ImageOut(fabian4);
    }
    public void Fabian5Out()
    {
        ImageOut(fabian5);
    }
    public void Fabian6Out()
    {
        ImageOut(fabian6);
    }
    public void Fabian7Out()
    {
        ImageOut(fabian7);
    }
    public void Fabian8Out()
    {
        ImageOut(fabian8);
    }
    public void Fabian9Out()
    {
        ImageOut(fabian9);
    }

    public void AllReset()
    {
        fabian1.SetActive(false);
        fabian2.SetActive(false);
        fabian3.SetActive(false);
        fabian4.SetActive(false);
        fabian5.SetActive(false);
        fabian6.SetActive(false);
        fabian7.SetActive(false);
        fabian8.SetActive(false);
        fabian9.SetActive(false);
    }
    void ImageOut(GameObject subject)
    {
        if (isFirstShowILL)
        {
            subject.SetActive(true);
             beforObj = subject;
            isFirstShowILL = false;
        }
        else
        {
            beforObj.SetActive(false);
            subject.SetActive(true);
            beforObj = subject;
        }
    }
}
