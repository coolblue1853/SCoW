using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Look : MonoBehaviour
{
    public GameObject End_Back;
    public GameObject Look_Back;
    public GameObject Checker;
    private bool previousState;

    public void Active()
    {
        Checker.SetActive(true);
    }
    public void Inactive()
    {
        Checker.SetActive(false);
    }
    private void Reset()
    {
       // DataBaseManager.NowSelecter = "End";
        End_Back.SetActive(true);
        Look_Back.SetActive(false);
    }
    void DialogSetter()
    {
        if (Checker.gameObject.activeSelf == true)
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


    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
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