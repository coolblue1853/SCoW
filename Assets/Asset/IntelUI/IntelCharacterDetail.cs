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
        if (��������������.activeSelf == true)
        {
            ���纸���ִ������� = "Ella";
        }
        else if (���̵�����������.activeSelf == true)
        {
            ���纸���ִ������� = "Aiden";
        }

    }

     string ���纸���ִ�������;


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
    public GameObject �����ڽ�������;
    public GameObject ��������������;
    public static int ��������ȹ�淮;
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
    public GameObject ���̵��ڽ�������;
    public GameObject ���̵�����������;
    public static int ���̵�����ȹ�淮;
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
        if(���纸���ִ������� == "Ella")
        {
            if(DataBaseManager.NowPage_Ella == 1)
            {
                if (Count_Ella >= 6)
                {
                    �����ʱ�ȭ();
                    �����ڽ�������.transform.GetChild(2).gameObject.SetActive(true);
                    �����ڽ�������.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 5)
                {
                    �����ʱ�ȭ();
                    �����ڽ�������.transform.GetChild(3).gameObject.SetActive(true);
                    �����ڽ�������.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 4)
                {
                    �����ʱ�ȭ();
                    �����ڽ�������.transform.GetChild(4).gameObject.SetActive(true);
                    �����ڽ�������.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 3)
                {
                    �����ʱ�ȭ();
                    �����ڽ�������.transform.GetChild(5).gameObject.SetActive(true);   
                }
                DataBaseManager.NowPage_Ella = 2;
            }
            else if (DataBaseManager.NowPage_Ella == 2)
            {
                if (Count_Ella >= 6)
                {
                    �����ʱ�ȭ();
                    �����ڽ�������.transform.GetChild(4).gameObject.SetActive(true);
                    �����ڽ�������.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 5)
                {
                    �����ʱ�ȭ();
                    �����ڽ�������.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Ella = 3;
            }
        }
        if (���纸���ִ������� == "Aiden")
        {
            if (DataBaseManager.NowPage_Aiden == 1)
            {
                if (Count_Aiden >= 6)
                {
                    ���̵��ʱ�ȭ();
                    ���̵��ڽ�������.transform.GetChild(2).gameObject.SetActive(true);
                    ���̵��ڽ�������.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 5)
                {
                    ���̵��ʱ�ȭ();
                    ���̵��ڽ�������.transform.GetChild(3).gameObject.SetActive(true);
                    ���̵��ڽ�������.transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 4)
                {
                    ���̵��ʱ�ȭ();
                    ���̵��ڽ�������.transform.GetChild(4).gameObject.SetActive(true);
                    ���̵��ڽ�������.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 3)
                {
                    ���̵��ʱ�ȭ();
                    ���̵��ڽ�������.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Aiden = 2;
            }
            else if (DataBaseManager.NowPage_Aiden == 2)
            {
                if (Count_Aiden >= 6)
                {
                    ���̵��ʱ�ȭ();
                    ���̵��ڽ�������.transform.GetChild(4).gameObject.SetActive(true);
                    ���̵��ڽ�������.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 5)
                {
                    ���̵��ʱ�ȭ();
                    ���̵��ڽ�������.transform.GetChild(5).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Aiden = 3;
            }
        }
    }

    public void PrevPage()
    {
        if (���纸���ִ������� == "Ella")
        {
            if (DataBaseManager.NowPage_Ella == 2)
            {
                if (Count_Ella >= 6)
                {
                    �����ʱ�ȭ();
                    �����ڽ�������.transform.GetChild(0).gameObject.SetActive(true);
                    �����ڽ�������.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 5)
                {
                    �����ʱ�ȭ();
                    �����ڽ�������.transform.GetChild(1).gameObject.SetActive(true);
                    �����ڽ�������.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 4)
                {
                    �����ʱ�ȭ();
                    �����ڽ�������.transform.GetChild(2).gameObject.SetActive(true);
                    �����ڽ�������.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 3)
                {
                    �����ʱ�ȭ();
                    �����ڽ�������.transform.GetChild(3).gameObject.SetActive(true);
                    �����ڽ�������.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Ella = 1;
            }
            else if (DataBaseManager.NowPage_Ella == 3)
            {
                if (Count_Ella >= 6)
                {
                    �����ʱ�ȭ();
                    �����ڽ�������.transform.GetChild(2).gameObject.SetActive(true);
                    �����ڽ�������.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Ella >= 5)
                {
                    �����ʱ�ȭ();
                    �����ڽ�������.transform.GetChild(3).gameObject.SetActive(true);
                    �����ڽ�������.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Ella = 2;
            }
        }
        if (���纸���ִ������� == "Aiden")
        {
            if (DataBaseManager.NowPage_Aiden == 2)
            {
                if (Count_Aiden >= 6)
                {
                    ���̵��ʱ�ȭ();
                    ���̵��ڽ�������.transform.GetChild(0).gameObject.SetActive(true);
                    ���̵��ڽ�������.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 5)
                {
                    ���̵��ʱ�ȭ();
                    ���̵��ڽ�������.transform.GetChild(1).gameObject.SetActive(true);
                    ���̵��ڽ�������.transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 4)
                {
                    ���̵��ʱ�ȭ();
                    ���̵��ڽ�������.transform.GetChild(2).gameObject.SetActive(true);
                    ���̵��ڽ�������.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 3)
                {
                    ���̵��ʱ�ȭ();
                    ���̵��ڽ�������.transform.GetChild(3).gameObject.SetActive(true);
                    ���̵��ڽ�������.transform.GetChild(4).gameObject.SetActive(true);
                }

                DataBaseManager.NowPage_Aiden = 1;
            }
            else if (DataBaseManager.NowPage_Aiden == 3)
            {
                if (Count_Aiden >= 6)
                {
                    ���̵��ʱ�ȭ();
                    ���̵��ڽ�������.transform.GetChild(2).gameObject.SetActive(true);
                    ���̵��ڽ�������.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Count_Aiden >= 5)
                {
                    ���̵��ʱ�ȭ();
                    ���̵��ڽ�������.transform.GetChild(3).gameObject.SetActive(true);
                    ���̵��ڽ�������.transform.GetChild(4).gameObject.SetActive(true);
                }
                DataBaseManager.NowPage_Aiden = 2;
            }
        }
    }






    void �����ʱ�ȭ()
    {
        �����ڽ�������.transform.GetChild(0).gameObject.SetActive(false);
        �����ڽ�������.transform.GetChild(1).gameObject.SetActive(false);
        �����ڽ�������.transform.GetChild(2).gameObject.SetActive(false);
        �����ڽ�������.transform.GetChild(3).gameObject.SetActive(false);
        �����ڽ�������.transform.GetChild(4).gameObject.SetActive(false);
        �����ڽ�������.transform.GetChild(5).gameObject.SetActive(false);
    }

    void ���̵��ʱ�ȭ()
    {
        ���̵��ڽ�������.transform.GetChild(0).gameObject.SetActive(false);
        ���̵��ڽ�������.transform.GetChild(1).gameObject.SetActive(false);
        ���̵��ڽ�������.transform.GetChild(2).gameObject.SetActive(false);
        ���̵��ڽ�������.transform.GetChild(3).gameObject.SetActive(false);
        ���̵��ڽ�������.transform.GetChild(4).gameObject.SetActive(false);
        ���̵��ڽ�������.transform.GetChild(5).gameObject.SetActive(false);
    }
}
