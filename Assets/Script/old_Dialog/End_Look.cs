using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Look : MonoBehaviour
{
    public bool CanJudge;

    public GameObject End_Back;
    public GameObject Look_Back;
    public GameObject Checker;
    private bool previousState;
    public GameObject End;
    public GameObject Look;
    public void Active()
    {

        Checker.SetActive(true);
    }
    public void Inactive()
    {
        Checker.SetActive(false);
    }
    private void Res()
    {
        DataBaseManager.isSelect = false;
        End_Back.SetActive(true);
        Look_Back.SetActive(false);
        //DataBaseManager.NowSelecter = "End";
        End.SetActive(true);
        Look.SetActive(true);
    }
    void DialogSetter()
    {
        if (Checker.gameObject.activeSelf == true && DataBaseManager.isSelect == false)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if (End_Back.activeSelf == true)
                {             
                    DataBaseManager.NowSelecter = "Look";
                    End_Back.SetActive(false);
                    Look_Back.SetActive(true);
                }
                else if (Look_Back.activeSelf == true)
                {
                    DataBaseManager.NowSelecter = "End";
                    End_Back.SetActive(true);
                    Look_Back.SetActive(false);
                }
            }
        }
        else if (DataBaseManager.isSelect == true)
        {
            End_Back.SetActive(false);
            Look_Back.SetActive(false);

            End.SetActive(false);
            Look.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Checker.gameObject.activeSelf && !previousState)
        {
            Res();
        }

        previousState = Checker.gameObject.activeSelf;
        DialogSetter();
    }
    private static End_Look instance = null;
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
    public static End_Look Instance
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