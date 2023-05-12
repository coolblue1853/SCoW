using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Look_Judge : MonoBehaviour
{


    public GameObject Checker;
    public GameObject End_Back;
    public GameObject Look_Back;
    public GameObject Judge_Back;

    public GameObject End;
    public GameObject Look;
    public GameObject Judge;

    private bool previousState;

    private void Reset()
    {
        DataBaseManager.isSelect = false;
        End.SetActive(true);
        Look.SetActive(true);
        Judge.SetActive(true);
        //DataBaseManager.NowSelecter = "End";
        End_Back.SetActive(true);
        Look_Back.SetActive(false);
        Judge_Back.SetActive(false);
    }


    public void Active()
    {
        Checker.SetActive(true);
    }
    public void Inactive()
    {
        Checker.SetActive(false);
    }


    void DialogSetter()
    {
        if (Checker.gameObject.activeSelf == true && DataBaseManager.isSelect == false)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (End_Back.activeSelf == true)
                {
                    DataBaseManager.NowSelecter = "Look";
                    End_Back.SetActive(false);
                    Look_Back.SetActive(true);
                    Judge_Back.SetActive(false);

                }
                else if (Look_Back.activeSelf == true)
                {
                    DataBaseManager.NowSelecter = "Judge";
                    End_Back.SetActive(false);
                    Look_Back.SetActive(false);
                    Judge_Back.SetActive(true);

                }
                else if (Judge_Back.activeSelf == true)
                {
                    DataBaseManager.NowSelecter = "End";
                    End_Back.SetActive(true);
                    Look_Back.SetActive(false);
                    Judge_Back.SetActive(false);

                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (End_Back.activeSelf == true)
                {
                    DataBaseManager.NowSelecter = "Judge";
                    End_Back.SetActive(false);
                    Look_Back.SetActive(false);
                    Judge_Back.SetActive(true);

                }
                else if (Look_Back.activeSelf == true)
                {
                    DataBaseManager.NowSelecter = "End";
                    End_Back.SetActive(true);
                    Look_Back.SetActive(false);
                    Judge_Back.SetActive(false);

                }
                else if (Judge_Back.activeSelf == true)
                {
                    DataBaseManager.NowSelecter = "Look";
                    End_Back.SetActive(false);
                    Look_Back.SetActive(true);
                    Judge_Back.SetActive(false);

                }
            }
        }
        else if(DataBaseManager.isSelect == true)
        {
            End_Back.SetActive(false);
            Look_Back.SetActive(false);
            Judge_Back.SetActive(false);
            End.SetActive(false);
            Look.SetActive(false);
            Judge.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Checker.gameObject.activeSelf && !previousState)
        {
            Reset();
        }

        previousState = Checker.gameObject.activeSelf;
        DialogSetter();
    }

    private static End_Look_Judge instance = null;
    private void Awake()
    {

        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static End_Look_Judge Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
}