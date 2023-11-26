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

    public void 수호자1()
    {
        일러스트송출(master1);
    }
    public void 수호자2()
    {
        일러스트송출(master2);
    }
    public void 수호자3()
    {
        일러스트송출(master3);
    }
    public void 수호자4()
    {
        일러스트송출(master4);
    }
    public void 수호자5()
    {
        일러스트송출(master5);
    }
    public void 수호자6()
    {
        일러스트송출(master6);
    }
    public void 노아일러스트1()
    {
        일러스트송출(fabian1);
    }
    public void 노아일러스트2()
    {

        일러스트송출(fabian2);
    }
    public void 노아일러스트3()
    {

        일러스트송출(fabian3);
    }
    public void 노아일러스트4()
    {
        일러스트송출(fabian4);
    }
    public void 노아일러스트5()
    {
        일러스트송출(fabian5);
    }
    public void 노아일러스트6()
    {
        일러스트송출(fabian6);
    }
    public void 노아일러스트7()
    {
        일러스트송출(fabian7);
    }
    public void 노아일러스트8()
    {
        일러스트송출(fabian8);
    }
    public void 노아일러스트9()
    {
        일러스트송출(fabian9);
    }

    public void 엘라일러스트1()
    {
        일러스트송출(ella1);
    }
    public void 엘라일러스트2()
    {
        일러스트송출(ella2);
    }

    public void allReset()
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
    void 일러스트송출(GameObject 대상)
    {
        if (isFirstShow)
        {
            대상.SetActive(true);
            beforeImage = 대상;
            isFirstShow = false;
        }

        else
        {
            beforeImage.SetActive(false);
            대상.SetActive(true);
            beforeImage = 대상;
        }
    }
}
