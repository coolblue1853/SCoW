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


    private static DialogManager instance = null;
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
    public static DialogManager Instance
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
        if (isDialog && DataBaseManager.SelectionOn == false)
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
        if (DataBaseManager.SelectionOn == false)
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
        else if (DataBaseManager.SelectionOn == true)
        {
            SkipStopON();
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
        if(DataBaseManager.SelectionOn == false)
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
        else if (DataBaseManager.SelectionOn == true)
        {
            SkipStopON();
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


    public void EndDialog()
    {
        DataBaseManager.endDialogLine = false;
        DataBaseManager.isActiveDialog1 = false;
        //DeleteChilds(정보플로팅위치);
        FirstClick = true;
        DataBaseManager.isActiveDialog2 = false;
        //DataBaseManager.연출중움직임제한 = false;
        //PlayerChar.transform.GetComponent<Mins>().다이얼로그다운();
        DialogFace.SetActive(false);

        isDialog = false;
        contextCount = 0;
        lineCount = 0;
        dialogs = null;
        isDialogON = false;
        isNext = false;
        SettingUI(false);
        DataBaseManager.NowSelecter = "End";
        End_Look.Instance.Inactive();
        End_Look_Judge.Instance.Inactive();


    }

    public void ShowDialog(Dialog[] P_dialogs)
    {
        DataBaseManager.isActiveDialog1 = true;
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
                            if (t_ReplaceText[i + 3] == '1')
                            {
                                if (t_ReplaceText[i + 4] == '행')
                                {
                                    if (DataBaseManager.Intel_PlanetarySequence1 == false)
                                        IntelManager.Instance.AddIntelFloating("Event", "행성대직렬 - 조간신문 기사");
                                    DataBaseManager.Intel_PlanetarySequence1 = true;
     

                                }
                                if (t_ReplaceText[i + 4] == '불')
                                {
                                    if (DataBaseManager.Intel_Insomnia1 == false)
                                        DataBaseManager.Intel_Insomnia1 = true;
                                    IntelManager.Instance.AddIntelFloating("Event", "불면증 - 조간신문 기사");
                                }
                                if (t_ReplaceText[i + 4] == '악')
                                {
                                    if (DataBaseManager.Intel_Nightmare1 == false)
                                        DataBaseManager.Intel_Nightmare1 = true;
                                    IntelManager.Instance.AddIntelFloating("Event", "악몽 - 남편의 상태");
                                }
                                if (t_ReplaceText[i + 4] == '편')
                                {
                                    if (DataBaseManager.Intel_Paranoia1 == false)
                                        DataBaseManager.Intel_Paranoia1 = true;
                                    IntelManager.Instance.AddIntelFloating("Event", "편집증 - 남편의 상태");
                                }
                            }

                        }
                    }
                    break;

                case 'ⓑ': t_white = false; t_yellow = false; t_red = false; t_green = false; t_blue = true; t_ignore = true;
                    if (t_ReplaceText[i + 1] == '(')
                    {
                        if (t_ReplaceText[i + 2] == '●')
                        {
                            if (t_ReplaceText[i + 3] == '1')
                            {
                                if (t_ReplaceText[i + 4] == '부')
                                {
                                    if (DataBaseManager.Intel_Wharf1 == false)
                                        DataBaseManager.Intel_Wharf1 = true;
                                    IntelManager.Instance.AddIntelFloating("Place", "부두 - 일거리");
                                }
                                if (t_ReplaceText[i + 4] == '병')
                                {
                                    if (DataBaseManager.Intel_Hospital1 == false)
                                        DataBaseManager.Intel_Hospital1 = true;
                                    IntelManager.Instance.AddIntelFloating("Place", "병원 - 혼잡 상태");
                                }
                                if (t_ReplaceText[i + 4] == '서')
                                {
                                    if (DataBaseManager.Intel_QuestHouse1 == false)
                                        DataBaseManager.Intel_QuestHouse1 = true;
                                    IntelManager.Instance.AddIntelFloating("Place", "의뢰자의 집 - 집 주소");
                                }
                                if (t_ReplaceText[i + 4] == '데')
                                {
                                    if (DataBaseManager.Intel_Newspaper1 == false)
                                        DataBaseManager.Intel_Newspaper1 = true;
                                    IntelManager.Instance.AddIntelFloating("Place", "데일리뉴스 신문사 - 직장");
                                }
                            }

                        }
                    }
                    break;

                case 'ⓖ': t_white = false; t_yellow = false; t_red = false; t_green = true; t_blue = false; t_ignore = true; 
                    if (t_ReplaceText[i + 1] == '(')
                    {
                        if (t_ReplaceText[i + 2] == '●')
                        {
                            if (t_ReplaceText[i + 3] == '1')
                            {
                                if (t_ReplaceText[i + 4] == '알')
                                {
                                    if (DataBaseManager.Intel_Albert1 == false)
                                        DataBaseManager.Intel_Albert1 = true;
                                    IntelManager.Instance.AddIntelFloating("Character", "알버트 브래들리 - 친구");
                                }

                                if (t_ReplaceText[i + 4] == '메')
                                {
                                    if (DataBaseManager.Intel_Meiv1 == false)
                                        DataBaseManager.Intel_Meiv1 = true;
                                    IntelManager.Instance.AddIntelFloating("Character", "메이브 A 로시 - 친구");
                                }
                                if (t_ReplaceText[i + 4] == '엘')
                                {
                                    if (DataBaseManager.Intel_Ella1 == false)
                                        DataBaseManager.Intel_Ella1 = true;
                                    IntelManager.Instance.AddIntelFloating("Character", "엘라 트리스 - 의뢰자");
                                }
                                if (t_ReplaceText[i + 4] == '에')
                                {
                                    if (DataBaseManager.Intel_Aiden1 == false)
                                        DataBaseManager.Intel_Aiden1 = true;
                                    IntelManager.Instance.AddIntelFloating("Character", "에이든 트리스 - 의뢰 대상");
                                }
                            }
                                
                        }
                    }
                    break;

                case '◎':  // 연출관련
                    t_ignore = true; // 의뢰자 들어오는연출
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        t_ignore = true;
                        DataBaseManager.fst_Detective_EndFirstDialog = true;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        t_ignore = true;
                        DataBaseManager.fst_Detective_NockDialog = true;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {
                        t_ignore = true;
                        DataBaseManager.fst_Detective_AfterSelect = true;
                      //  연출매니저.의뢰인앉는연출 = false;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '④')
                    {
                        t_ignore = true;
                        DataBaseManager.fst_Detective_EndSelect = true;
                        //   연출매니저.의뢰인나감연출 = false;

                        break;
                    }
                    if (t_ReplaceText[i + 1] == '⑤')
                    {
                        DataBaseManager.fst_Detective_AfterSelectNo = true;
                        t_ignore = true;
                        //연출매니저.연출끝();
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '⑥')
                    {
                        DataBaseManager.FailTwoKeyword = true;
                        t_ignore = true;
                        //연출매니저.연출끝();
                        break;
                    }
                    break;

                case '☆':  // 별에 선택지 출현.
                    t_ignore = true;
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        SkipStopON();
                        End_Look_Judge.Instance.Active();
                     
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

                    break;

                case '★':  // 검은별은 선택지이후 연결파트 표시
                    t_ignore = true;
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        DataBaseManager.SelectionOn = true;
                        selectionUIManager.Instance.Open_1st_DetectiveOffice_Select1();
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        DataBaseManager.SelectionOn = true;
                        selectionUIManager.Instance.Open_1st_ClientsOffic_SafeSelect();
                    }
                    break;

                case '♠'://대화중 기능판단 출현
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        DataBaseManager.StrDialogOn = true;
                    }
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
                case '1': if (t_ReplaceText[i + -1] == '●') { t_ignore = true; } break;
                case '2': if (t_ReplaceText[i + -1] == '●') { t_ignore = true; } break;
                case '3': if (t_ReplaceText[i + -1] == '●') { t_ignore = true; } break;
                case '4': if (t_ReplaceText[i + -1] == '●') { t_ignore = true; } break;
                case '5': if (t_ReplaceText[i + -1] == '●') { t_ignore = true; } break;
                case '6': if (t_ReplaceText[i + -1] == '●') { t_ignore = true; } break;
                //case '0': if (t_ReplaceText[i + 1] == '§') { t_ignore = true; } if (t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '①': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '☆' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '★' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '②': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 1] == '★' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '☆' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '③': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '④': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '⑤': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑥': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑦': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑧': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑨': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑩': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i + 2] == '§' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑫': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i + 2] == '§' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;


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
