using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;
using Febucci.UI.Core;
using System;
using System.Linq;
using UnityEngine.Assertions;

public class DialogManager : MonoBehaviour
{
    public GameObject FloatPosition;
    // 선택지 등장시  해당변수 활성화로 멈추기
    public bool ShowChoice = false;

    public GameObject SkipButton;
    public GameObject SkipStopButton;
    // Start is called before the first frame update
    public GameObject DialogFace;
    public GameObject Fabian_1;

    // ⓦ 이건 색깔  , 쉼표는 ' 로  \n으로 해서 작성하면 됨 엑셀

    public GameObject PlayerChar;

    public selectionUIManager selectionUIManager;
    public bool isDialogON = false;
    [SerializeField] GameObject go_dialogBar;
    [SerializeField] GameObject go_dialogNameBar;

    [SerializeField] TextMeshProUGUI txt_Dialog;
    [SerializeField] Text txt_name;
    Dialog[] dialogs;
   // public Mins min;
    bool isDialog = false;
    bool isNext = false; // 특정 키 입력 대기.
    public bool isSelectButton = false;
    public bool FirstClick = true;

    int lineCount = 0; // 대화 카운트(사람)
    int contextCount = 0; // 대사 카운트 (줄)

    //public 아침사무소연출 연출매니저;
    
    //기능판정 대화창
    public GameObject JudgeDialog;
    bool z_next2 = true;
    bool z_next1;
    SpriteManager theSpriteManager;
  //  public GameObject 엘라심리학선택지버튼;

    [Header("텍스트 출력 딜레이.")]
    [SerializeField] float textDelay;
    public TAnimPlayerBase TypeA;
    public TextAnimatorPlayer TextAnimatorPlayer;
    private void Start()
    {
        FirstClick = true;
        isSelectButton = false;
        theSpriteManager = FindObjectOfType<SpriteManager>();
    }
    void Update()
    {
        settingPlayerCon();
        TextCheker();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (z_next2 == true)
            {
                z_next1 = true;
            }
            z_next2 = true;
        }
    }


    void TextCheker()
    {
        if (isDialog)
        {
            if (isNext)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (FirstClick == true)
                    {
                        TypeA.SkipTypewriter();
                        FirstClick = false;
                    }
                    else
                    {
                        isNext = false;
                        txt_Dialog.text = "";
                        if (++contextCount < dialogs[lineCount].contexts.Length)
                        {
                            StartCoroutine(TypeWriter());
                        }
                        else
                        {
                            contextCount = 0;
                            if (++lineCount < dialogs.Length)
                            {
                                //selectionUIManager.대화중기능판정버튼제거();
                                StartCoroutine(TypeWriter());
                            }
                            else
                            {
                                EndDialog();
                            }
                        }
                    }
                }
            }
        }
    }







    


    public void SkipOn()
    {
        if (isSelectButton == false)
        {
            SkipButton.SetActive(false);
            SkipStopButton.SetActive(true);
            DataBaseManager.skipActive = true;

            //TypeA.wait = 1;
            //DataBaseManager.textDelay = 0.00001f;
            TextAnimatorPlayer.waitForNormalChars = 0.003f;
            TextAnimatorPlayer.waitLong = 0.003f;
            TextAnimatorPlayer.waitMiddle = 0.003f;

            StartCoroutine(Skip());

        }
    }

    public void SkipStopON()
    {   
        TextAnimatorPlayer.waitForNormalChars = 0.03f;
        TextAnimatorPlayer.waitLong = 0.03f;
        TextAnimatorPlayer.waitMiddle = 0.03f;
        DataBaseManager.skipActive = false;
        SkipStopButton.SetActive(false);
        ShowChoice = true;
        StartCoroutine(SkipNomalize());
    }

    IEnumerator SkipNomalize()
    {
        yield return new WaitForSeconds(0.5f);
        ShowChoice = false;
        SkipButton.SetActive(true);
    }


    IEnumerator Skip()
    {
        z_next1 = true;   //이 부분을 true로 두면  스킵이 엄청 빨라지고 false로 두면 적당해짐
        z_next2 = true;
        yield return new WaitForSeconds(0.08f);
        ChoiceEx_NextPage_t();
        yield return new WaitForSeconds(0.08f);
        if (go_dialogBar.activeSelf == false)
        {
            SkipStopON();
            DataBaseManager.skipActive = false;
        }
        else if (ShowChoice == false)
        {
            StartCoroutine(Skip());
        }
        else if (ShowChoice == true)
        {
            DataBaseManager.skipActive = false;
        }
    }


    // 선택지 선택시 외부에서 페이지 넘기기 t
    public void ChoiceEx_NextPage_t()
    {
        if (isDialog)
        {
            if (isNext)
            {
                isNext = false;
                txt_Dialog.text = "";
                if (++contextCount < dialogs[lineCount].contexts.Length)
                {
                    StartCoroutine(TypeWriter());
                }
                else
                {
                    contextCount = 0;
                    if (++lineCount < dialogs.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else
                    {
                        EndDialog();
                    }
                }
            }
        }
    }
    public void ChoiceEx_NextPage()
    {
        SkipButton.SetActive(true);
        SkipStopButton.SetActive(false);
        DataBaseManager.skipActive = false;
        ShowChoice = false;
        if (isDialog)
        {
            if (isNext)
            {
                isNext = false;
                txt_Dialog.text = "";
                if (++contextCount < dialogs[lineCount].contexts.Length)
                {
                    StartCoroutine(TypeWriter());
                }
                else
                {
                    contextCount = 0;
                    if (++lineCount < dialogs.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }

                    else
                    {
                        EndDialog();
                    }
                }
            }
        }
    }


    public void DeleteChilds(GameObject stressBackground)
    {
        Transform[] childList = stressBackground.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 2; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }

    }


    void EndDialog()
    {
        DataBaseManager.endDialogLine = false;
        DataBaseManager.isActiveDialog1 = false;
        //DeleteChilds(정보플로팅위치);
        FirstClick = true;
        DataBaseManager.isActiveDialog2 = false;
        //DataBaseManager.연출중움직임제한 = false;
        //PlayerChar.transform.GetComponent<Mins>().다이얼로그다운();
        DialogFace.SetActive(false);
        DialogFace.SetActive(false);

        isDialog = false;
        contextCount = 0;
        lineCount = 0;
        dialogs = null;
        isDialogON = false;
        isNext = false;
        SettingUI(false);
    }

    public void ShowDialog(Dialog[] P_dialogs)
    {
        DataBaseManager.isActiveDialog2 = true;
        //PlayerChar.transform.GetComponent<Mins>().다이얼로그온();
        DialogFace.SetActive(true);
        isDialog = true;
        txt_Dialog.text = "";
        txt_name.text = "";
        dialogs = P_dialogs;
        StartCoroutine(TypeWriter());
    }


//public 정보창Manager 정보창매니저;



    // 타입라이터 강제
    public void TypeWriter_Force()
    {
        SettingUI(true);
        string t_ReplaceText = dialogs[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("^", ",");
        txt_Dialog.text = t_ReplaceText;
        isNext = true;
    }



    IEnumerator TypeWriter()
    {
        SettingUI(true);
        string t_ReplaceText = dialogs[lineCount].contexts[contextCount];
        //t_ReplaceText = t_ReplaceText.Replace(" ", " ");  //자간늘리기
        t_ReplaceText = t_ReplaceText.Replace("^", ",");
        t_ReplaceText = t_ReplaceText.Replace('@', '"');
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");
        bool t_white = false, t_yellow = false, t_red = false, t_green = false, t_blue = false;   // 색 추가 지점
        bool t_ignore = false;
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            switch (t_ReplaceText[i])
            {
                case 'ⓦ': t_white = true; t_yellow = false; t_red = false; t_green = false; t_blue = false; t_ignore = true; break;
                case 'ⓨ': t_white = false; t_yellow = true; t_red = false; t_green = false; t_blue = false; t_ignore = true; break;
                case 'ⓡ': t_white = false; t_yellow = false; t_red = true; t_green = false; t_blue = false; t_ignore = true; 
                    if (t_ReplaceText[i + 1] == '(')
                    {
                        if (t_ReplaceText[i + 2] == '●')
                        {
                            if (t_ReplaceText[i + 3] == '행')
                            {
                              //  DataBaseManager.행성대직렬정보1 = true;
                              //  정보창매니저.행성대직렬정보추가();
                            }
                            if (t_ReplaceText[i + 3] == '악')
                            {
                               // DataBaseManager.악몽정보1 = true;
                               // 정보창매니저.악몽정보추가();
                            }
                            if (t_ReplaceText[i + 3] == '불')
                            {
                                //DataBaseManager.불면증정보1 = true;
                               // 정보창매니저.불면증정보추가();
                            }
                        }
                    }
                    break;

                case 'ⓑ': t_white = false; t_yellow = false; t_red = false; t_green = false; t_blue = true; t_ignore = true;
                    if (t_ReplaceText[i + 1] == '(')
                    {
                        if (t_ReplaceText[i + 2] == '●')
                        {
                            if (t_ReplaceText[i + 3] == '병')
                            {
                              //  DataBaseManager.병원정보1 = true;
                              //  정보창매니저.병원정보추가();
                            }
                            if (t_ReplaceText[i + 3] == '서')
                            {
                             //   DataBaseManager.의뢰자의집정보1 = true;
                               // 정보창매니저.의뢰자의집정보추가();
                            }
                            if (t_ReplaceText[i + 3] == '아')
                            {
                               // DataBaseManager.아컴역정보1 = true;
                               // 정보창매니저.아캄역정보추가();
                            }
                            if (t_ReplaceText[i + 3] == '데')
                            {
                               // DataBaseManager.신문사정보1 = true;
                               // 정보창매니저.신문사정보추가();
                            }
                        }
  
                    }
                    break;

                case 'ⓖ': t_white = false; t_yellow = false; t_red = false; t_green = true; t_blue = false; t_ignore = true; 
                    if (t_ReplaceText[i + 1] == '(')
                    {
                        if (t_ReplaceText[i + 2] == '●')
                        {
                            if (t_ReplaceText[i + 3] == '엘')
                            {
                              //  DataBaseManager.엘라정보1 = true;
                              //  정보창매니저.엘라정보추가();
                            }
                            if (t_ReplaceText[i + 3] == '에')
                            {
                               // DataBaseManager.에이든정보1 = true;
                                //정보창매니저.에이든정보추가();
                            }
                        }
                    }
                    break;

                case '◎':  // 연출관련
                    t_ignore = true; // 의뢰자 들어오는연출
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        t_ignore = true;
                         DirectingManager.Instance.End_Directing();
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        t_ignore = true;
                        //연출매니저.첫번째의뢰인들어오는연출 = false;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {
                        t_ignore = true;
                      //  연출매니저.의뢰인앉는연출 = false;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '④')
                    {
                        t_ignore = true;
                     //   연출매니저.의뢰인나감연출 = false;

                        break;
                    }
                    if (t_ReplaceText[i + 1] == '⑤')
                    {
                        t_ignore = true;
                        //연출매니저.연출끝();
                        break;
                    }
                    break;

                case '☆':  // 별에 선택지 출현.
                    t_ignore = true;
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        ShowChoice = true;
                        SkipButton.SetActive(false);
                        SkipStopButton.SetActive(false);
                        t_ignore = true;
                        //selectionUIManager.선택지출력1(); // 1번선택지.
                        isSelectButton = true;
                        break;
                    }

                    if (t_ReplaceText[i + 1] == '⑵')
                    {
                        SkipButton.SetActive(false);
                        SkipStopButton.SetActive(false);
                        ShowChoice = true;
                        t_ignore = true;
                       // selectionUIManager.선택지출력2(); // 2번선택지.
                        isSelectButton = true;
                        break;

                    }
                    if (t_ReplaceText[i + 1] == '⑶')
                    {
                        SkipButton.SetActive(false);
                        SkipStopButton.SetActive(false);
                        ShowChoice = true;
                        t_ignore = true;
                        //selectionUIManager.선택지출력3(); // 2번선택지.
                        isSelectButton = true;
                        break;

                    }


                    if (t_ReplaceText[i + 1] == '②')//행성대직렬 정보 1 추가.
                    {
                        if (t_ReplaceText[i + 2] == '㉧')
                        {
                            if (t_ReplaceText[i + 3] == '㉤')
                            {
                                t_ignore = true;
                                //DataBaseManager.악몽정보2 = true;
                                break;
                            }
                        }
                    }
                    break;

                case '★':  // 검은별은 선택지이후 연결파트 표시
                    t_ignore = true;
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        SkipButton.SetActive(false);
                        SkipStopButton.SetActive(false);
                        ShowChoice = true;
                        t_ignore = true;
                        //selectionUIManager.일번선택지넘기기();
                        isSelectButton = true;
                        break;
                    }
                    break;

                case '♠'://기능판단오프
                    //selectionUIManager.엘라심리학선택지끄기();
                    t_ignore = true;
                    break;
                // 최초 심리학 기능판정 선택지 출력창.
                case '※'://기능판단온
                    if(DataBaseManager.skipActive == false)
                    {
                        //if (t_ReplaceText[i + 1] == '㉦') { selectionUIManager.엘라심리학선택지출력(); };
                    }
                    t_ignore = true;
                    break;

                case '⒭':
                    theSpriteManager.allReset();
                    t_ignore = true;
                    break;

                case '§':

                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '①' && t_ReplaceText[i + 4] == '§') { theSpriteManager.노아일러스트1(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '②' && t_ReplaceText[i + 4] == '§') { theSpriteManager.노아일러스트2(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '③' && t_ReplaceText[i + 4] == '§') { theSpriteManager.노아일러스트3(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '④' && t_ReplaceText[i + 4] == '§') { theSpriteManager.노아일러스트4(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑤' && t_ReplaceText[i + 4] == '§') { theSpriteManager.노아일러스트5(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑥' && t_ReplaceText[i + 4] == '§') { theSpriteManager.노아일러스트6(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑦' && t_ReplaceText[i + 4] == '§') { theSpriteManager.노아일러스트7(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑧' && t_ReplaceText[i + 4] == '§') { theSpriteManager.노아일러스트8(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑨' && t_ReplaceText[i + 4] == '§') { theSpriteManager.노아일러스트9(); };
         
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑩' && t_ReplaceText[i + 4] == '§') { theSpriteManager.엘라일러스트2(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑩' && t_ReplaceText[i + 4] == '①' && t_ReplaceText[i + 5] == '§') { theSpriteManager.엘라일러스트1(); };
                
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑩' && t_ReplaceText[i + 4] == '②' && t_ReplaceText[i + 5] == '§') { theSpriteManager.멜리사일러스트(); };
                    /*
        if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑩' && t_ReplaceText[i + 4] == '③' && t_ReplaceText[i + 5] == '§') { theSpriteManager.노아일러스트13(); };
         if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑩' && t_ReplaceText[i + 4] == '④' && t_ReplaceText[i + 5] == '§') { theSpriteManager.노아일러스트14(); };
         if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑩' && t_ReplaceText[i + 4] == '⑤' && t_ReplaceText[i + 5] == '§') { theSpriteManager.노아일러스트15(); };
         if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑩' && t_ReplaceText[i + 4] == '⑥' && t_ReplaceText[i + 5] == '§') { theSpriteManager.노아일러스트16(); };
         if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑩' && t_ReplaceText[i + 4] == '⑦' && t_ReplaceText[i + 5] == '§') { theSpriteManager.노아일러스트17(); };
         if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑩' && t_ReplaceText[i + 4] == '⑧' && t_ReplaceText[i + 5] == '§') { theSpriteManager.노아일러스트18(); };
         if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑩' && t_ReplaceText[i + 4] == '⑨' && t_ReplaceText[i + 5] == '§') { theSpriteManager.노아일러스트19(); };
         if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑫' && t_ReplaceText[i + 4] == '§') { theSpriteManager.노아일러스트20(); };
         if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑫' && t_ReplaceText[i + 4] == '①' && t_ReplaceText[i + 5] == '§') { theSpriteManager.노아일러스트21(); };
         if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑫' && t_ReplaceText[i + 4] == '②' && t_ReplaceText[i + 5] == '§') { theSpriteManager.노아일러스트22(); };
         if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑫' && t_ReplaceText[i + 4] == '③' && t_ReplaceText[i + 5] == '§') { theSpriteManager.노아일러스트23(); };
         if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑫' && t_ReplaceText[i + 4] == '④' && t_ReplaceText[i + 5] == '§') { theSpriteManager.노아일러스트24(); }; t_ignore = true; break; // 표정변경.
         */

                    if (t_ReplaceText[i + 1] == '엘' && t_ReplaceText[i + 2] == '라' && t_ReplaceText[i + 3] == '1' && t_ReplaceText[i + 4] == '§') { theSpriteManager.엘라일러스트1(); }; t_ignore = true; break; // 표정변경.

                //㉠ ㉡ ㉢ ㉣ ㉤ ㉥ ㉦ ㉧ ㉨ ㉩ ㉪ ㉫ ㉬ ㉭
                //① ② ③ ④ ⑤ ⑥ ⑦ ⑧ ⑨ ⑩ ⑪ ⑫
                case '㉡': if (t_ReplaceText[i - 1] == '§') { t_ignore = true; } break;
                case '㉧': if (t_ReplaceText[i - 2] == '§' || t_ReplaceText[i + -2] == '☆') { t_ignore = true; } break;
                case '㉦': if (t_ReplaceText[i - 1] == '※') { t_ignore = true; } break;
                case '㉤': if (t_ReplaceText[i + -3] == '☆') { t_ignore = true; } break;

                case '●': if (t_ReplaceText[i + -1] == '(') { t_ignore = true; } break;

                //case '0': if (t_ReplaceText[i + 1] == '§') { t_ignore = true; } if (t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '①': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '☆' || t_ReplaceText[i - 1] == '★' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '②': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '☆' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '③': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '④': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '⑤': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '⑥': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '⑦': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '⑧': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '⑨': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '⑩': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i + 2] == '§') { t_ignore = true; } break;
                case '⑫': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i + 2] == '§') { t_ignore = true; } break;


            }




            string t_letter = t_ReplaceText[i].ToString();

            if (!t_ignore)
            {
                if (t_white) { t_letter = "<color=#FFFFFF>" + t_letter + "</color>"; }
                else if (t_yellow) { t_letter = "<color=#FFFF00>" + t_letter + "</color>"; }
                else if (t_red) { t_letter = "<color=#B30000>" + t_letter + "</color>"; }
                else if (t_blue) { t_letter = "<color=#0026A9>" + t_letter + "</color>"; }
                else if (t_green) { t_letter = "<color=#1A6800>" + t_letter + "</color>"; }
                txt_Dialog.text += t_letter;
            }
            t_ignore = false;


            /*
           if(z키로넘기기여부 == false)
           {
               yield return new WaitForSecondsRealtime(textDelay);
           }
           */

        }
        FirstClick = true;
        isNext = true;
        yield return null;

    }

    public void EndTypeSKip()
    {
        DataBaseManager.endDialogLine = true;
        FirstClick = false;
    }
    public void StartTypeSKip()
    {
        FirstClick = true;
    }
    void SettingUI(bool P_flag)
    {
        go_dialogBar.SetActive(P_flag);


        if (P_flag)
        {
            if (dialogs[lineCount].name == "")
            {
                go_dialogNameBar.SetActive(false);

            }
            else
            {
                go_dialogNameBar.SetActive(true);

                txt_name.text = dialogs[lineCount].name;
            }
        }


    }

    void settingPlayerCon()
    {

        if (go_dialogBar.activeSelf == true)
        {

            // PlayerChar.transform.GetComponent<Mins>().isDialogOnChangeT();
        }

        if (go_dialogBar.activeSelf == false)
        {

            // PlayerChar.transform.GetComponent<Mins>().isDialogOnChangeF();
        }


    }

    public void onButtonSetterT()
    {
        isSelectButton = true;
    }
    public void onButtonSetterF()
    {
        isSelectButton = false;
    }
}
