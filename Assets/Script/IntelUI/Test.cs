using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Test : MonoBehaviour
{
    string nowPage;
    GameObject detailWharf1;
    GameObject detailWharf2;
    GameObject detailWharf3;
    GameObject detailWharf4;
    GameObject detailWharf5;
    GameObject detailWharf6;
    bool onWharf1;
    bool onWharf2;
    bool onWharf3;
    bool onWharf4;
    bool onWharf5;
    bool onWharf6;
    int countWharf = 0;
    public GameObject wharfContents;
    public GameObject wharfDetail;
    public static int wharfIntelInt;
    public GameObject nextButtonWharf;
    public GameObject prevButtonWharf; 

    void WharfDetail()
    {
        if (onWharf1 == false && DataBaseManager.intelWharf1 == true)
        {
            onWharf1 = true;
            detailWharf1.SetActive(true);
            detailWharf1.transform.SetAsLastSibling();
            countWharf += 1;
        }
        if (onWharf2 == false && DataBaseManager.intelWharf2 == true)
        {
            onWharf2 = true;
            detailWharf2.SetActive(true);
            detailWharf2.transform.SetAsLastSibling();
            countWharf += 1;
        }
        if (onWharf3 == false && DataBaseManager.intelWharf3 == true)
        {
            onWharf3 = true;
            detailWharf3.SetActive(true);
            detailWharf3.transform.SetAsLastSibling();
            countWharf += 1;
        }
        if (onWharf4 == false && DataBaseManager.intelWharf4 == true)
        {
            onWharf4 = true;
            detailWharf4.SetActive(true);
            detailWharf4.transform.SetAsLastSibling();
            countWharf += 1;
        }
        if (onWharf5 == false && DataBaseManager.intelWharf5 == true)
        {
            onWharf5 = true;
            detailWharf5.SetActive(true);
            detailWharf5.transform.SetAsLastSibling();
            countWharf += 1;
        }
        if (onWharf6 == false && DataBaseManager.intelWharf6 == true)
        {
            onWharf6 = true;
            detailWharf6.SetActive(true);
            detailWharf6.transform.SetAsLastSibling();
            countWharf += 1;
        }
        if (countWharf > 2 && DataBaseManager.nowPageWharf == 1)
        {
            nextButtonWharf.SetActive(true);
        }
        else if (countWharf > 4 && DataBaseManager.nowPageWharf == 2)
        {
            nextButtonWharf.SetActive(true);
        }
        else
        {
            nextButtonWharf.SetActive(false);
        }
        if (DataBaseManager.nowPageWharf == 2)
        {
            prevButtonWharf.SetActive(true);
        }
        else if (DataBaseManager.nowPageWharf == 3)
        {
            prevButtonWharf.SetActive(true);
        }
        else
        {
            prevButtonWharf.SetActive(false);
        }
    }
    public void NextPage()
    {
        if (nowPage == "Wharf")
        {
            if (DataBaseManager.nowPageWharf == 1)
            {
                if (countWharf >= 6)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(2).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countWharf >= 5)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(3).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (countWharf >= 4)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(4).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countWharf >= 3)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageWharf = 2;
            }
            else if (DataBaseManager.nowPageWharf == 2)
            {
                if (countWharf >= 6)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(4).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (countWharf >= 5)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageWharf = 3;
            }
        }
    }
    public void PrevPage()
    {
        if (nowPage == "Wharf")
        {
            if (DataBaseManager.nowPageWharf == 2)
            {
                if (countWharf >= 6)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(0).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (countWharf >= 5)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(1).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (countWharf >= 4)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(2).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countWharf >= 3)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(3).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageWharf = 1;
            }
            else if (DataBaseManager.nowPageWharf == 3)
            {
                if (countWharf >= 6)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(2).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (countWharf >= 5)
                {
                    Reset_Wharf();
                    wharfContents.transform.GetChild(3).gameObject.SetActive(true);
                    wharfContents.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.nowPageWharf = 2;
            }
        }
    }
    void Reset_Wharf()
    {
        wharfContents.transform.GetChild(0).gameObject.SetActive(false);
        wharfContents.transform.GetChild(1).gameObject.SetActive(false);
        wharfContents.transform.GetChild(2).gameObject.SetActive(false);
        wharfContents.transform.GetChild(3).gameObject.SetActive(false);
        wharfContents.transform.GetChild(4).gameObject.SetActive(false);
        wharfContents.transform.GetChild(5).gameObject.SetActive(false);
    }
}
