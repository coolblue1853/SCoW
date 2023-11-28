using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class End_Look_Judge : MonoBehaviour
{
    public GameObject checker;
    public GameObject endBackOb;
    public GameObject lookBackOb;
    public GameObject judgeBackOb;
    public GameObject keywordBackOb;
    public Image endBack;
    public Image lookBack;
    public Image judgeBack;
    public Image keywordBack;
    public GameObject end;
    public GameObject look;
    public GameObject judge;
    public GameObject keyword;

    private bool previousState;

    private void Reset()
    {
        DataBaseManager.isSelect = false;
        end.SetActive(true);
        look.SetActive(true);
    
        DataBaseManager.nowSelecter = "End";
        endBack.DOFade(1, 0.00001f).SetAutoKill();
        lookBack.DOFade(0, 0.00001f).SetAutoKill();
    }
    public void Active()
    {
        checker.SetActive(true);
    }
    public void DaveRandActive()
    {
        DataBaseManager.isKeyword = true;
        keywordBack.DOFade(0, 0.00001f).SetAutoKill();
        keywordBackOb.SetActive(true);
        keyword.SetActive(true);
    }
    public void Inactive()
    {
        checker.SetActive(false);
        lookBack.DOFade(0, 0.00001f).SetAutoKill();
        keywordBack.DOFade(0, 0.00001f).SetAutoKill();
        judgeBack.DOFade(0, 0.00001f).SetAutoKill();
        judgeBackOb.SetActive(false);
        keywordBackOb.SetActive(false);
        judge.SetActive(false);
        keyword.SetActive(false);
    }
    public void OnEnd()
    {
        DataBaseManager.nowSelecter = "End";
    }
    public void OffEnd()
    {
        DataBaseManager.nowSelecter = "End";
    }
    public void OnLook()
    {
        DataBaseManager.nowSelecter = "Look";
    }
    public void OffLook()
    {
        DataBaseManager.nowSelecter = "End";
    }
    public void OnJudge()
    {
        DataBaseManager.nowSelecter = "Judge";
    }
    public void OffJudge()
    {
        DataBaseManager.nowSelecter = "End";
    }
    public void OnKey()
    {
        DataBaseManager.nowSelecter = "Keyword";
    }
    public void OffKey()
    {
        DataBaseManager.conntectMouse = false;
    }
    void DialogSetter()
    {
        if (checker.gameObject.activeSelf == true && DataBaseManager.isSelect == false)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (endBack.color.a > 0)
                {
                    DataBaseManager.nowSelecter = "Look";
                    endBack.DOFade(0, 0.00001f).SetAutoKill();
                    lookBack.DOFade(1, 0.00001f).SetAutoKill();
                    judgeBack.DOFade(0, 0.00001f).SetAutoKill();
                    keywordBack.DOFade(0, 0.00001f).SetAutoKill();

                }
                else if (lookBack.color.a > 0)
                {
                    if (DataBaseManager.isJudge == true)
                    {
                        DataBaseManager.nowSelecter = "Judge";
                        endBack.DOFade(0, 0.00001f).SetAutoKill();
                        lookBack.DOFade(0, 0.00001f).SetAutoKill();
                        judgeBack.DOFade(1, 0.00001f).SetAutoKill();
                        keywordBack.DOFade(0, 0.00001f).SetAutoKill();
                    }
                    else if (DataBaseManager.isKeyword == true)
                    {
                        DataBaseManager.nowSelecter = "Keyword";
                        endBack.DOFade(0, 0.00001f).SetAutoKill();
                        lookBack.DOFade(0, 0.00001f).SetAutoKill();
                        judgeBack.DOFade(0, 0.00001f).SetAutoKill();
                        keywordBack.DOFade(1, 0.00001f).SetAutoKill();
                    }
                    else
                    {
                        DataBaseManager.nowSelecter = "End";
                        endBack.DOFade(1, 0.00001f).SetAutoKill();
                        lookBack.DOFade(0, 0.00001f).SetAutoKill();
                        judgeBack.DOFade(0, 0.00001f).SetAutoKill();
                        keywordBack.DOFade(0, 0.00001f).SetAutoKill();
                    }


                }
                else if (judgeBack.color.a > 0)
                {
                    if (DataBaseManager.isKeyword == true)
                    {
                        DataBaseManager.nowSelecter = "Keyword";
                        endBack.DOFade(0, 0.00001f).SetAutoKill();
                        lookBack.DOFade(0, 0.00001f).SetAutoKill();
                        judgeBack.DOFade(0, 0.00001f).SetAutoKill();
                        keywordBack.DOFade(1, 0.00001f).SetAutoKill();
                    }
                    else
                    {
                        DataBaseManager.nowSelecter = "End";
                        endBack.DOFade(1, 0.00001f).SetAutoKill();
                        lookBack.DOFade(0, 0.00001f).SetAutoKill();
                        judgeBack.DOFade(0, 0.00001f).SetAutoKill();
                        keywordBack.DOFade(0, 0.00001f).SetAutoKill();
                    }

                }
                else if (keywordBack.color.a > 0)
                {
                    DataBaseManager.nowSelecter = "End";
                    endBack.DOFade(1, 0.00001f).SetAutoKill();
                    lookBack.DOFade(0, 0.00001f).SetAutoKill();
                    judgeBack.DOFade(0, 0.00001f).SetAutoKill();
                    keywordBack.DOFade(0, 0.00001f).SetAutoKill();

                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (endBack.color.a > 0)
                {
                    if (DataBaseManager.isKeyword == true)
                    {
                        DataBaseManager.nowSelecter = "Keyword";
                        endBack.DOFade(0, 0.00001f).SetAutoKill();
                        lookBack.DOFade(0, 0.00001f).SetAutoKill();
                        judgeBack.DOFade(0, 0.00001f).SetAutoKill();
                        keywordBack.DOFade(1, 0.00001f).SetAutoKill();
                    }
                    else if (DataBaseManager.isJudge == true)
                    {
                        DataBaseManager.nowSelecter = "Judge";
                        endBack.DOFade(0, 0.00001f).SetAutoKill();
                        lookBack.DOFade(0, 0.00001f).SetAutoKill();
                        judgeBack.DOFade(1, 0.00001f).SetAutoKill();
                        keywordBack.DOFade(0, 0.00001f).SetAutoKill();
                    }
                    else
                    {
                        DataBaseManager.nowSelecter = "Look";
                        endBack.DOFade(0, 0.00001f).SetAutoKill();
                        lookBack.DOFade(1, 0.00001f).SetAutoKill();
                        judgeBack.DOFade(0, 0.00001f).SetAutoKill();
                        keywordBack.DOFade(0, 0.00001f).SetAutoKill();
                    }


                }
                else if (lookBack.color.a > 0)
                {
                    DataBaseManager.nowSelecter = "End";
                    endBack.DOFade(1, 0.00001f).SetAutoKill();
                    lookBack.DOFade(0, 0.00001f).SetAutoKill();
                    judgeBack.DOFade(0, 0.00001f).SetAutoKill();
                    keywordBack.DOFade(0, 0.00001f).SetAutoKill();


                }
                else if (judgeBack.color.a > 0)
                {
                    DataBaseManager.nowSelecter = "Look";
                    endBack.DOFade(0, 0.00001f).SetAutoKill();
                    lookBack.DOFade(1, 0.00001f).SetAutoKill();
                    judgeBack.DOFade(0, 0.00001f).SetAutoKill();
                    keywordBack.DOFade(0, 0.00001f).SetAutoKill();

                }
                else if (keywordBack.color.a > 0)
                {

                    if (DataBaseManager.isJudge == true)
                    {
                        DataBaseManager.nowSelecter = "Judge";
                        endBack.DOFade(0, 0.00001f).SetAutoKill();
                        lookBack.DOFade(0, 0.00001f).SetAutoKill();
                        judgeBack.DOFade(1, 0.00001f).SetAutoKill();
                        keywordBack.DOFade(0, 0.00001f).SetAutoKill();
                    }
                    else
                    {
                        DataBaseManager.nowSelecter = "Look";
                        endBack.DOFade(0, 0.00001f).SetAutoKill();
                        lookBack.DOFade(1, 0.00001f).SetAutoKill();
                        judgeBack.DOFade(0, 0.00001f).SetAutoKill();
                        keywordBack.DOFade(0, 0.00001f).SetAutoKill();
                    }

                }
            }
        }
        else if (DataBaseManager.isSelect == true)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (checker.gameObject.activeSelf && !previousState)
        {
            Reset();
        }

        previousState = checker.gameObject.activeSelf;
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