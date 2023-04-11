using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IntelCharacterDetail : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EllaDetail();
        AidenDetail();
        if (엘라정보컨텐츠.activeSelf == true)
        {
            현재보고있는페이지 = "Ella";
        }
        else if (에이든정보컨텐츠.activeSelf == true)
        {
            현재보고있는페이지 = "Aiden";
        }

    }

     string 현재보고있는페이지;


    public GameObject Detail_Ella1;
    public GameObject Detail_Ella2;
    public GameObject Detail_Ella3;
    public GameObject Detail_Ella4;
    public GameObject Detail_Ella5;
    public GameObject Detail_Ella6;
    bool ON_Ella1;
    bool ON_Ella2;
    bool ON_Ella3;
    bool ON_Ella4;
    bool ON_Ella5;
    bool ON_Ella6;
     int Count_Ella = 0;
    public GameObject 엘라자식컨텐츠;
    public GameObject 엘라정보컨텐츠;
    public static int 엘라정보획득량;
    public GameObject NextButton_Ella;
    public GameObject PrevButton_Ella;

    //

    public GameObject Detail_Aiden1;
    public GameObject Detail_Aiden2;
    public GameObject Detail_Aiden3;
    public GameObject Detail_Aiden4;
    public GameObject Detail_Aiden5;
    public GameObject Detail_Aiden6;
    bool ON_Aiden1;
    bool ON_Aiden2;
    bool ON_Aiden3;
    bool ON_Aiden4;
    bool ON_Aiden5;
    bool ON_Aiden6;
     int Count_Aiden = 0;
    public GameObject 에이든자식컨텐츠;
    public GameObject 에이든정보컨텐츠;
    public static int 에이든정보획득량;
    public GameObject NextButton_Aiden;
    public GameObject PrevButton_Aiden;




    void AidenDetail()
    {
        if (ON_Aiden1 == false && DataBaseManager.Intel_Aiden1 == true)
        {
            ON_Aiden1 = true;
            Detail_Aiden1.SetActive(true);
            Detail_Aiden1.transform.SetAsLastSibling();
            Count_Aiden += 1;
        }
        if (ON_Aiden2 == false && DataBaseManager.Intel_Aiden2 == true)
        {
            ON_Aiden2 = true;
            Detail_Aiden2.SetActive(true);
            Detail_Aiden2.transform.SetAsLastSibling();
            Count_Aiden += 1;
        }
        if (ON_Aiden3 == false && DataBaseManager.Intel_Aiden3 == true)
        {
            ON_Aiden3 = true;
            Detail_Aiden3.SetActive(true);
            Detail_Aiden3.transform.SetAsLastSibling();
            Count_Aiden += 1;
        }
        if (ON_Aiden4 == false && DataBaseManager.Intel_Aiden4 == true)
        {
            ON_Aiden4 = true;
            Detail_Aiden4.SetActive(true);
            Detail_Aiden4.transform.SetAsLastSibling();
            Count_Aiden += 1;
        }
        if (ON_Aiden5 == false && DataBaseManager.Intel_Aiden5 == true)
        {
            ON_Aiden5 = true;
            Detail_Aiden5.SetActive(true);
            Detail_Aiden5.transform.SetAsLastSibling();
            Count_Aiden += 1;
        }
        if (ON_Aiden6 == false && DataBaseManager.Intel_Aiden6 == true)
        {
            ON_Aiden6 = true;
            Detail_Aiden6.SetActive(true);
            Detail_Aiden6.transform.SetAsLastSibling();
            Count_Aiden += 1;
        }

        if (Count_Aiden > 2 && DataBaseManager.NowPage_Aiden == 1)
        {
            NextButton_Aiden.SetActive(true);
        }
        else if (Count_Aiden > 4 && DataBaseManager.NowPage_Aiden == 2)
        {
            NextButton_Aiden.SetActive(true);
        }
        else
        {
            NextButton_Aiden.SetActive(false);
        }

        if (DataBaseManager.NowPage_Aiden == 2)
        {
            PrevButton_Aiden.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Aiden == 3)
        {
            PrevButton_Aiden.SetActive(true);
        }
        else
        {
            PrevButton_Aiden.SetActive(false);
        }
    }


    void EllaDetail()
    {
        if (ON_Ella1 == false && DataBaseManager.Intel_Ella1 == true)
        {
            ON_Ella1 = true;
            Detail_Ella1.SetActive(true);
            Detail_Ella1.transform.SetAsLastSibling();
            Count_Ella += 1;
        }
        if (ON_Ella2 == false && DataBaseManager.Intel_Ella2 == true)
        {
            ON_Ella2 = true;
            Detail_Ella2.SetActive(true);
            Detail_Ella2.transform.SetAsLastSibling();
            Count_Ella += 1;
        }
        if (ON_Ella3 == false && DataBaseManager.Intel_Ella3 == true)
        {
            ON_Ella3 = true;
            Detail_Ella3.SetActive(true);
            Detail_Ella3.transform.SetAsLastSibling();
            Count_Ella += 1;
        }
        if (ON_Ella4 == false && DataBaseManager.Intel_Ella4 == true)
        {
            ON_Ella4 = true;
            Detail_Ella4.SetActive(true);
            Detail_Ella4.transform.SetAsLastSibling();
            Count_Ella += 1;
        }
        if (ON_Ella5 == false && DataBaseManager.Intel_Ella5 == true)
        {
            ON_Ella5 = true;
            Detail_Ella5.SetActive(true);
            Detail_Ella5.transform.SetAsLastSibling();
            Count_Ella += 1;
        }
        if (ON_Ella6 == false && DataBaseManager.Intel_Ella6 == true)
        {
            ON_Ella6 = true;
            Detail_Ella6.SetActive(true);
            Detail_Ella6.transform.SetAsLastSibling();
            Count_Ella += 1;
        }

        if (Count_Ella > 2 && DataBaseManager.NowPage_Ella == 1)
        {
            NextButton_Ella.SetActive(true);
        }
        else if (Count_Ella > 4 && DataBaseManager.NowPage_Ella == 2)
        {
            NextButton_Ella.SetActive(true);
        }
        else
        {
            NextButton_Ella.SetActive(false);
        }

        if (DataBaseManager.NowPage_Ella == 2)
        {
            PrevButton_Ella.SetActive(true);
        }
        else if (DataBaseManager.NowPage_Ella == 3)
        {
            PrevButton_Ella.SetActive(true);
        }
        else
        {
            PrevButton_Ella.SetActive(false);
        }
    }



    public void NextPage()
    {
        if(현재보고있는페이지 == "Ella")
        {
            if(DataBaseManager.NowPage_Ella == 1)
            {
                if (Count_Ella >= 6)
                {
                    엘라초기화();
                    엘라자식컨텐츠.transform.GetChild(2).gameObject.SetActive(true);
                    엘라자식컨텐츠.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 5)
                {
                    엘라초기화();
                    엘라자식컨텐츠.transform.GetChild(3).gameObject.SetActive(true);
                    엘라자식컨텐츠.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 4)
                {
                    엘라초기화();
                    엘라자식컨텐츠.transform.GetChild(4).gameObject.SetActive(true);
                    엘라자식컨텐츠.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 3)
                {
                    엘라초기화();
                    엘라자식컨텐츠.transform.GetChild(5).gameObject.SetActive(true);   
                }
                DataBaseManager.NowPage_Ella = 2;
            }
            else if (DataBaseManager.NowPage_Ella == 2)
            {
                if (Count_Ella >= 6)
                {
                    엘라초기화();
                    엘라자식컨텐츠.transform.GetChild(4).gameObject.SetActive(true);
                    엘라자식컨텐츠.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 5)
                {
                    엘라초기화();
                    엘라자식컨텐츠.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Ella = 3;
            }
        }
        if (현재보고있는페이지 == "Aiden")
        {
            if (DataBaseManager.NowPage_Aiden == 1)
            {
                if (Count_Aiden >= 6)
                {
                    에이든초기화();
                    에이든자식컨텐츠.transform.GetChild(2).gameObject.SetActive(true);
                    에이든자식컨텐츠.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 5)
                {
                    에이든초기화();
                    에이든자식컨텐츠.transform.GetChild(3).gameObject.SetActive(true);
                    에이든자식컨텐츠.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 4)
                {
                    에이든초기화();
                    에이든자식컨텐츠.transform.GetChild(4).gameObject.SetActive(true);
                    에이든자식컨텐츠.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 3)
                {
                    에이든초기화();
                    에이든자식컨텐츠.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Aiden = 2;
            }
            else if (DataBaseManager.NowPage_Aiden == 2)
            {
                if (Count_Aiden >= 6)
                {
                    에이든초기화();
                    에이든자식컨텐츠.transform.GetChild(4).gameObject.SetActive(true);
                    에이든자식컨텐츠.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 5)
                {
                    에이든초기화();
                    에이든자식컨텐츠.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Aiden = 3;
            }
        }
    }

    public void PrevPage()
    {
        if (현재보고있는페이지 == "Ella")
        {
            if (DataBaseManager.NowPage_Ella == 2)
            {
                if (Count_Ella >= 6)
                {
                    엘라초기화();
                    엘라자식컨텐츠.transform.GetChild(0).gameObject.SetActive(true);
                    엘라자식컨텐츠.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 5)
                {
                    엘라초기화();
                    엘라자식컨텐츠.transform.GetChild(1).gameObject.SetActive(true);
                    엘라자식컨텐츠.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 4)
                {
                    엘라초기화();
                    엘라자식컨텐츠.transform.GetChild(2).gameObject.SetActive(true);
                    엘라자식컨텐츠.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 3)
                {
                    엘라초기화();
                    엘라자식컨텐츠.transform.GetChild(3).gameObject.SetActive(true);
                    엘라자식컨텐츠.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Ella = 1;
            }
            else if (DataBaseManager.NowPage_Ella == 3)
            {
                if (Count_Ella >= 6)
                {
                    엘라초기화();
                    엘라자식컨텐츠.transform.GetChild(2).gameObject.SetActive(true);
                    엘라자식컨텐츠.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 5)
                {
                    엘라초기화();
                    엘라자식컨텐츠.transform.GetChild(3).gameObject.SetActive(true);
                    엘라자식컨텐츠.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Ella = 2;
            }
        }
        if (현재보고있는페이지 == "Aiden")
        {
            if (DataBaseManager.NowPage_Aiden == 2)
            {
                if (Count_Aiden >= 6)
                {
                    에이든초기화();
                    에이든자식컨텐츠.transform.GetChild(0).gameObject.SetActive(true);
                    에이든자식컨텐츠.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 5)
                {
                    에이든초기화();
                    에이든자식컨텐츠.transform.GetChild(1).gameObject.SetActive(true);
                    에이든자식컨텐츠.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 4)
                {
                    에이든초기화();
                    에이든자식컨텐츠.transform.GetChild(2).gameObject.SetActive(true);
                    에이든자식컨텐츠.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 3)
                {
                    에이든초기화();
                    에이든자식컨텐츠.transform.GetChild(3).gameObject.SetActive(true);
                    에이든자식컨텐츠.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Aiden = 1;
            }
            else if (DataBaseManager.NowPage_Aiden == 3)
            {
                if (Count_Aiden >= 6)
                {
                    에이든초기화();
                    에이든자식컨텐츠.transform.GetChild(2).gameObject.SetActive(true);
                    에이든자식컨텐츠.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 5)
                {
                    에이든초기화();
                    에이든자식컨텐츠.transform.GetChild(3).gameObject.SetActive(true);
                    에이든자식컨텐츠.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Aiden = 2;
            }
        }
    }






    void 엘라초기화()
    {
        엘라자식컨텐츠.transform.GetChild(0).gameObject.SetActive(false);
        엘라자식컨텐츠.transform.GetChild(1).gameObject.SetActive(false);
        엘라자식컨텐츠.transform.GetChild(2).gameObject.SetActive(false);
        엘라자식컨텐츠.transform.GetChild(3).gameObject.SetActive(false);
        엘라자식컨텐츠.transform.GetChild(4).gameObject.SetActive(false);
        엘라자식컨텐츠.transform.GetChild(5).gameObject.SetActive(false);
    }

    void 에이든초기화()
    {
        에이든자식컨텐츠.transform.GetChild(0).gameObject.SetActive(false);
        에이든자식컨텐츠.transform.GetChild(1).gameObject.SetActive(false);
        에이든자식컨텐츠.transform.GetChild(2).gameObject.SetActive(false);
        에이든자식컨텐츠.transform.GetChild(3).gameObject.SetActive(false);
        에이든자식컨텐츠.transform.GetChild(4).gameObject.SetActive(false);
        에이든자식컨텐츠.transform.GetChild(5).gameObject.SetActive(false);
    }
}
