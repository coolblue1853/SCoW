using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpriteManager : MonoBehaviour
{
    public GameObject master1;
    public GameObject master2;
    public GameObject master3;
    public GameObject master4;
    public GameObject master5;
    public GameObject master6;
    public GameObject fabian1;
    public GameObject fabian2;
    public GameObject fabian3;
    public GameObject fabian4;
    public GameObject fabian5;
    public GameObject fabian6;
    public GameObject fabian7;
    public GameObject fabian8;
    public GameObject fabian9;
    public GameObject ella1;
    GameObject beforeImage = null;
    bool isFirstShow = true;
    bool isFirstImage = false;
    public GameObject ella2;

    public void Master1()
    {
        PrintImg(master1);
    }
    public void Master2()
    {
        PrintImg(master2);
    }
    public void Master3()
    {
        PrintImg(master3);
    }
    public void Master4()
    {
        PrintImg(master4);
    }
    public void Master5()
    {
        PrintImg(master5);
    }
    public void Master6()
    {
        PrintImg(master6);
    }
    public void Albert1()
    {
        PrintImg(fabian1);
    }
    public void Albert2()
    {

        PrintImg(fabian2);
    }
    public void Albert3()
    {

        PrintImg(fabian3);
    }
    public void Albert4()
    {
        PrintImg(fabian4);
    }
    public void Albert5()
    {
        PrintImg(fabian5);
    }
    public void Albert6()
    {
        PrintImg(fabian6);
    }
    public void Albert7()
    {
        PrintImg(fabian7);
    }
    public void Albert8()
    {
        PrintImg(fabian8);
    }
    public void Albert9()
    {
        PrintImg(fabian9);
    }

    public void Ella1()
    {
        PrintImg(ella1);
    }
    public void Ella2()
    {
        PrintImg(ella2);
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
    void PrintImg(GameObject subject)
    {
        if (isFirstShow)
        {
            subject.SetActive(true);
            beforeImage = subject;
            isFirstShow = false;
        }

        else
        {
            beforeImage.SetActive(false);
            subject.SetActive(true);
            beforeImage = subject;
        }
    }
}
