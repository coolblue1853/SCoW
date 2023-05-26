using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class End_Look_Judge : MonoBehaviour
{


    public GameObject Checker;


    public GameObject End_BackOb;
    public GameObject Look_BackOb;
    public GameObject Judge_BackOb;
    public GameObject Keyword_BackOb;

    public Image End_Back;
    public Image Look_Back;
    public Image Judge_Back;
    public Image Keyword_Back;

    public GameObject End;
    public GameObject Look;
    public GameObject Judge;
    public GameObject Keyword;

    private bool previousState;

    private void Reset()
    {
        DataBaseManager.isSelect = false;
        End.SetActive(true);
        Look.SetActive(true);
        if (DataBaseManager.isJudge)
        {
            Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
            Judge.SetActive(true);
            Judge_BackOb.SetActive(true);
        }
        else
        {
            Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
            Judge.SetActive(false);
            Judge_BackOb.SetActive(false);
        }
        if (DataBaseManager.isKeyword)
        {
            Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();
            Keyword.SetActive(true);
            Keyword_BackOb.SetActive(true);
        }
        else
        {
            Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();
            Keyword.SetActive(false);
            Keyword_BackOb.SetActive(false);
        }

        //DataBaseManager.NowSelecter = "End";
        End_Back.DOFade(1,0.00001f).SetAutoKill();
        Look_Back.DOFade(0, 0.00001f).SetAutoKill();



    }


    public void Active()
    {
        Checker.SetActive(true);
    }
    public void Inactive()
    {
        Checker.SetActive(false);
    }

    public void OnEnd()
    {
        DataBaseManager.NowSelecter = "End";

    }
    public void OffEnd()
    {
        DataBaseManager.NowSelecter = "End";

    }
    public void OnLook()
    {
        DataBaseManager.NowSelecter = "Look";

    }
    public void OffLook()
    {
        DataBaseManager.NowSelecter = "End";

    }
    public void OnJudge()
    {
        DataBaseManager.NowSelecter = "Judge";

    }
    public void OffJudge()
    {
        DataBaseManager.NowSelecter = "End";

    }
    public void OnKey()
    {
        DataBaseManager.NowSelecter = "Keyword";

    }
    public void OffKey()
    {
        DataBaseManager.ConntectMouse = false;


    }
    void DialogSetter()
    {
        if (Checker.gameObject.activeSelf == true && DataBaseManager.isSelect == false)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (End_Back.color.a > 0 )
                {
                    DataBaseManager.NowSelecter = "Look";
                    End_Back.DOFade(0, 0.00001f).SetAutoKill();
                    Look_Back.DOFade(1, 0.00001f).SetAutoKill();
                    Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
                    Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();

                }
                else if (Look_Back.color.a > 0)
                {
                    if(DataBaseManager.isJudge == true)
                    {
                        DataBaseManager.NowSelecter = "Judge";
                        End_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Look_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Judge_Back.DOFade(1, 0.00001f).SetAutoKill();
                        Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();
                    }
                    else if(DataBaseManager.isKeyword == true)
                    {
                        DataBaseManager.NowSelecter = "Keyword";
                        End_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Look_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Keyword_Back.DOFade(1, 0.00001f).SetAutoKill();
                    }
                    else
                    {
                        DataBaseManager.NowSelecter = "End";
                        End_Back.DOFade(1, 0.00001f).SetAutoKill();
                        Look_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();
                    }


                }
                else if (Judge_Back.color.a > 0)
                {
                   if (DataBaseManager.isKeyword == true)
                    {
                        DataBaseManager.NowSelecter = "Keyword";
                        End_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Look_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Keyword_Back.DOFade(1, 0.00001f).SetAutoKill();
                    }
                    else
                    {
                        DataBaseManager.NowSelecter = "End";
                        End_Back.DOFade(1, 0.00001f).SetAutoKill();
                        Look_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();
                    }

                }
                else if (Keyword_Back.color.a > 0)
                {
                    DataBaseManager.NowSelecter = "End";
                    End_Back.DOFade(1, 0.00001f).SetAutoKill();
                    Look_Back.DOFade(0, 0.00001f).SetAutoKill();
                    Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
                    Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();

                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (End_Back.color.a > 0)
                {
                    if (DataBaseManager.isKeyword == true)
                    {
                        DataBaseManager.NowSelecter = "Keyword";
                        End_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Look_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Keyword_Back.DOFade(1, 0.00001f).SetAutoKill();
                    }
                   else if (DataBaseManager.isJudge == true)
                    {
                        DataBaseManager.NowSelecter = "Judge";
                        End_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Look_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Judge_Back.DOFade(1, 0.00001f).SetAutoKill();
                        Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();
                    }
                    else
                    {
                        DataBaseManager.NowSelecter = "Look";
                        End_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Look_Back.DOFade(1, 0.00001f).SetAutoKill();
                        Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();
                    }


                }
                else if (Look_Back.color.a > 0)
                {
                    DataBaseManager.NowSelecter = "End";
                    End_Back.DOFade(1, 0.00001f).SetAutoKill();
                    Look_Back.DOFade(0, 0.00001f).SetAutoKill();
                    Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
                    Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();


                }
                else if (Judge_Back.color.a > 0)
                {
                    DataBaseManager.NowSelecter = "Look";
                    End_Back.DOFade(0, 0.00001f).SetAutoKill();
                    Look_Back.DOFade(1, 0.00001f).SetAutoKill();
                    Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
                    Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();

                }
                else if (Keyword_Back.color.a > 0)
                {

                    if (DataBaseManager.isJudge == true)
                    {
                        DataBaseManager.NowSelecter = "Judge";
                        End_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Look_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Judge_Back.DOFade(1, 0.00001f).SetAutoKill();
                        Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();
                    }
                    else
                    {
                        DataBaseManager.NowSelecter = "Look";
                        End_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Look_Back.DOFade(1, 0.00001f).SetAutoKill();
                        Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
                        Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();
                    }

                }
            }
        }
        else if(DataBaseManager.isSelect == true)
        {
            End_Back.DOFade(0, 0.00001f).SetAutoKill();
            Look_Back.DOFade(0, 0.00001f).SetAutoKill();
            Judge_Back.DOFade(0, 0.00001f).SetAutoKill();
            Keyword_Back.DOFade(0, 0.00001f).SetAutoKill();

            End.SetActive(false);
            Look.SetActive(false);
            Judge.SetActive(false);
            Keyword.SetActive(false);
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