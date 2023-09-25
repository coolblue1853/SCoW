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


    public void TypeSound()
    {
        SoundManager.Instance.DalogSound();
    }

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
        if (Input.GetKeyDown(KeyCode.E) || (Input.GetMouseButtonDown(0)))
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
                if (Input.GetKeyDown(KeyCode.E) || (Input.GetMouseButtonDown(0)))
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
        DataBaseManager.isItemUI = false;
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
                case 'ⓦ':
                    if (t_ReplaceText[i - 1] == ')')
                    {
                        t_white = true; t_yellow = false; t_red = false; t_green = false; t_blue = false; t_ignore = true; break;
                    }
                    t_ignore = true;
                    break;
                case 'ⓨ': t_white = false; t_yellow = true; t_red = false; t_green = false; t_blue = false; t_ignore = true; break;
                case 'ⓡ': t_white = false; t_yellow = false; t_red = true; t_green = false; t_blue = false; t_ignore = true; 
                    if (t_ReplaceText[i + 1] == '(')
                    {
                        if (t_ReplaceText[i + 2] == '●')
                        {
                            if (t_ReplaceText[i + 3] == '1')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓟ')
                                {
                                    if (DataBaseManager.Intel_PlanetaryParade1 == false)
                                    {
                                        IntelManager.Instance.AddIntelFloating("Event", "PlanetaryParade - Contents of a Newspaper");
                                        DataBaseManager.Intel_PlanetaryParade1 = true;
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓘ')
                                {
                                    if (DataBaseManager.Intel_Insomnia1 == false)
                                    {
                                        DataBaseManager.Intel_Insomnia1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "Insomnia - Newspaper Article");

                                    }
                              
                                }
                                if (t_ReplaceText[i + 4] == 'ⓝ')
                                {
                                    if (DataBaseManager.Intel_Nightmare1 == false)
                                    {
                                        DataBaseManager.Intel_Nightmare1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "Nightmare - Aiden's Nightmare");
                                    }
                               
                                }
                                if (t_ReplaceText[i + 4] == 'ⓞ')
                                {
                                    if (DataBaseManager.Intel_Paranoia1 == false)
                                    {
                                        DataBaseManager.Intel_Paranoia1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "Paranoia - Husband's Condition");
                                    }
                                       
                                }
                                if (t_ReplaceText[i + 4] == 'ⓐ')
                                {
                                    if (DataBaseManager.Intel_Safe1 == false)
                                    {
                                        DataBaseManager.Intel_Safe1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "Safe - Aiden's Safe");
                                    }
   
                                }
                                if (t_ReplaceText[i + 4] == 'ⓢ') // 하수도 괴담
                                {
                                    if (DataBaseManager.Intel_SewerGhostStory1 == false)
                                    {
                                        DataBaseManager.Intel_SewerGhostStory1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "SewerGhostStory - Contents of a GhostStory");
                                    }
                                        
                                }
                                if (t_ReplaceText[i + 4] == 'ⓕ') // 비릿한 냄새
                                {
                                    if (DataBaseManager.Intel_FishySmell1 == false)
                                    {
                                        DataBaseManager.Intel_FishySmell1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "FishySmell - Rumor");
                                    }
                    
                                }
                                if (t_ReplaceText[i + 4] == 'ⓜ') // 연속실종사건
                                {
                                    if (DataBaseManager.Intel_MissingPeople2 == false) 
                                    {
                                     DataBaseManager.Intel_MissingPeople2 = true;
                                    IntelManager.Instance.AddIntelFloating("Event", "MissingPeople - Reporting by Swain");
                                    }

                                }
                                if (t_ReplaceText[i + 4] == 'ⓡ') // 강의 수질
                                {
                                    if (DataBaseManager.Intel_RiverWaterQuality1 == false)
                                    {
                                        DataBaseManager.Intel_RiverWaterQuality1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "RiverWaterQuality - Strange Point");
                                    }
 
                                }
                                if (t_ReplaceText[i + 4] == 'ⓒ') // 섬뜩한 눈
                                {
                                    if (DataBaseManager.Intel_CreepyEyes1 == false)
                                    {
                                        DataBaseManager.Intel_CreepyEyes1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "CreepyEyes - Saw in the sewer");
                                    }

                                }
                            }
                            if (t_ReplaceText[i + 3] == '2')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓟ')
                                {
                                    if (DataBaseManager.Intel_PlanetaryParade1 == false)
                                        IntelManager.Instance.AddIntelFloating("Event", "planetaryparade - 조간신문 기사");
                                    DataBaseManager.Intel_PlanetaryParade1 = true;


                                }
                                if (t_ReplaceText[i + 4] == 'ⓘ')
                                {
                                    if (DataBaseManager.Intel_Insomnia2 == false)
                                        DataBaseManager.Intel_Insomnia2 = true;
                                    IntelManager.Instance.AddIntelFloating("Event", "Insomnia - Doctor's opinion");
                                }
                                if (t_ReplaceText[i + 4] == 'ⓝ')
                                {
                                    if (DataBaseManager.Intel_Nightmare2 == false)
                                        DataBaseManager.Intel_Nightmare2 = true;
                                    IntelManager.Instance.AddIntelFloating("Event", "Nightmare - Content of a Nightmare");
                                }
                                if (t_ReplaceText[i + 4] == 'ⓞ')
                                {
                                    if (DataBaseManager.Intel_Paranoia2 == false)
                                    {
                                        DataBaseManager.Intel_Paranoia2 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "Paranoia - Cause of Paranoia");
                                    }
            
                                }
                                if (t_ReplaceText[i + 4] == 'ⓐ')
                                {
                                    if (DataBaseManager.Intel_Safe2 == false)
                                    {
                                        DataBaseManager.Intel_Safe2 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "Safe - Acquiring a Safe");
                                    }
                
                                }
                                if (t_ReplaceText[i + 4] == 'ⓜ') // 연속실종사건
                                {
                                    if (DataBaseManager.Intel_MissingPeople1 == false)
                                    {
                                        DataBaseManager.Intel_MissingPeople1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "MissingPeople - Albert's Information");
                                    }
                            
                                }
                                if (t_ReplaceText[i + 4] == 'ⓕ') // 비릿한 냄새
                                {
                                    if (DataBaseManager.Intel_FishySmell2 == false)
                                    {
                                        DataBaseManager.Intel_FishySmell2 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "FishySmell - Source of the smell?");
                                    }
   
                                }
                                if (t_ReplaceText[i + 4] == 'ⓡ') // 강의 수질
                                {
                                    if (DataBaseManager.Intel_RiverWaterQuality2 == false)
                                    {
                                        DataBaseManager.Intel_RiverWaterQuality2 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "RiverWaterQuality - Details");
                                    }

                                }
                                if (t_ReplaceText[i + 4] == 'ⓢ') // 하수도 괴담
                                {
                                    if (DataBaseManager.Intel_SewerGhostStory2 == false)
                                    {
                                        DataBaseManager.Intel_SewerGhostStory2 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "SewerGhostStory - Truth of a rumor");
                                    }

                                }
                            }
                            if (t_ReplaceText[i + 3] == '3')
                            {

                                if (t_ReplaceText[i + 4] == 'ⓝ')
                                {
                                    if (DataBaseManager.Intel_Nightmare3 == false)
                                        DataBaseManager.Intel_Nightmare3 = true;
                                    IntelManager.Instance.AddIntelFloating("Event", "Nightmare - Aiden's Detail");
                                }
                                if (t_ReplaceText[i + 4] == 'ⓜ') // 연속실종사건
                                {
                                    if (DataBaseManager.Intel_MissingPeople3 == false)
                                    {
                                        DataBaseManager.Intel_MissingPeople3 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "MissingPeople - Kidnap");
                                    }

                                }

                                if (t_ReplaceText[i + 4] == 'ⓕ') // 비릿한 냄새
                                {
                                    if (DataBaseManager.Intel_FishySmell3 == false)
                                    {
                                        DataBaseManager.Intel_FishySmell3 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "FishySmell -Source speculation");
                                    }

                                }
                            }
                            if (t_ReplaceText[i + 3] == '4')
                            {

                                if (t_ReplaceText[i + 4] == 'ⓝ')
                                {
                                    if (DataBaseManager.Intel_Nightmare4 == false)
                                        DataBaseManager.Intel_Nightmare4 = true;
                                    IntelManager.Instance.AddIntelFloating("Event", "Nightmare - Water and Nightmare");
                                }
                                if (t_ReplaceText[i + 4] == 'ⓕ') // 비릿한 냄새
                                {
                                    if (DataBaseManager.Intel_FishySmell4 == false)
                                    {
                                        DataBaseManager.Intel_FishySmell4 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "FishySmell - Sewer & FishySmell");
                                    }

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
                                if (t_ReplaceText[i + 4] == 'ⓧ')
                                {
                                    if (DataBaseManager.Intel_Sewer1 == false)
                                    {
                                        DataBaseManager.Intel_Sewer1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Sewer - Aiden's warning");
                                    }

                                }
                                if (t_ReplaceText[i + 4] == 'ⓦ')
                                {
                                    if (DataBaseManager.Intel_Wharf1 == false)
                                    {
                                        DataBaseManager.Intel_Wharf1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Wharf - Work");
                                    }
                                
                                }
                                if (t_ReplaceText[i + 4] == 'ⓗ')
                                {
                                    if (DataBaseManager.Intel_Hospital1 == false)
                                    {
                                        DataBaseManager.Intel_Hospital1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Hospital - State of congestion");
                                    }
                            
                                }
                                if (t_ReplaceText[i + 4] == 'ⓠ')
                                {
                                    if (DataBaseManager.Intel_QuestHouse1 == false)
                                    {
                                        DataBaseManager.Intel_QuestHouse1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Client'shouse - Adress");
                                    }
                               
                                }
                                if (t_ReplaceText[i + 4] == 'ⓝ')
                                {
                                    if (DataBaseManager.Intel_Newspaper1 == false)
                                    {
                                        DataBaseManager.Intel_Newspaper1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "DailyNews - Husband's job");
                                    }
                              
                                }
                                if (t_ReplaceText[i + 4] == 'ⓢ')
                                {
                                    if (DataBaseManager.Intel_SewageMaintenanceOffice1 == false)
                                    {
                                        DataBaseManager.Intel_SewageMaintenanceOffice1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "SewageTreatmentOffice - Location");
                                    }

                                }
                                if (t_ReplaceText[i + 4] == 'ⓤ' )
                                {
                                    if (DataBaseManager.Intel_University1 == false)
                                    {
                                        DataBaseManager.Intel_University1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Univ of Miscatonic - Astronomical show");
                                    }
                            
                                }

                            }

                            if (t_ReplaceText[i + 3] == '2')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓧ')
                                {
                                    if (DataBaseManager.Intel_Sewer2 == false)
                                    {
                                        DataBaseManager.Intel_Sewer2 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Sewer - Root of problem");
                                    }

                                }
                             

                            }
                            if (t_ReplaceText[i + 3] == '3')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓧ')
                                {
                                    if (DataBaseManager.Intel_Sewer3 == false)
                                    {
                                        DataBaseManager.Intel_Sewer3 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Sewer - Manhole");
                                    }

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
                                if (t_ReplaceText[i + 4] == 'ⓛ')
                                {
                                    if (DataBaseManager.Intel_Albert1 == false)
                                    {
                                        DataBaseManager.Intel_Albert1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Albert Bradley - Police friend");
                                    }

                                }

                                if (t_ReplaceText[i + 4] == 'ⓜ')
                                {
                                    if (DataBaseManager.Intel_Meiv1 == false)
                                    {
                                        DataBaseManager.Intel_Meiv1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Maeve A Rossi - Mafia friend");
                                    }

                                }
                                if (t_ReplaceText[i + 4] == 'ⓔ')
                                {
                                    if (DataBaseManager.Intel_Ella1 == false)
                                    {
                                        DataBaseManager.Intel_Ella1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Ella Triss - Client");
                                    }

                                }
                                if (t_ReplaceText[i + 4] == 'ⓐ')
                                {
                                    if (DataBaseManager.Intel_Aiden1 == false)
                                    {
                                        DataBaseManager.Intel_Aiden1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Aiden Triss - Subject to request");
                                    }

                                }
                                if (t_ReplaceText[i + 4] == 'ⓦ')
                                {
                                    if (DataBaseManager.Intel_SewerWorker1 == false)
                                    {
                                        DataBaseManager.Intel_SewerWorker1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "SewerWorker - Owner of Eye?");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓧ')
                                {
                                    if (DataBaseManager.Intel_Swain1 == false)
                                    {
                                        DataBaseManager.Intel_Swain1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Berkeley Swain - Sewer Survey");
                                    }
                                }

                            }
                            if (t_ReplaceText[i + 3] == '2')
                            {

                                if (t_ReplaceText[i + 4] == 'ⓐ')
                                {
                                    if (DataBaseManager.Intel_Aiden2 == false)
                                    {
                                        DataBaseManager.Intel_Aiden2 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Aiden Triss - Aiden and Pandemic");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓦ')
                                {
                                    if (DataBaseManager.Intel_SewerWorker2 == false)
                                    {
                                        DataBaseManager.Intel_SewerWorker2 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "SewerWorker - Unfair dismissal");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓜ')
                                {
                                    if (DataBaseManager.Intel_Meiv2 == false)
                                    {
                                        DataBaseManager.Intel_Meiv2 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Maeve A Rossi - Promise");
                                    }

                                }
                            }
                            if (t_ReplaceText[i + 3] == '3')
                            {

                                if (t_ReplaceText[i + 4] == 'ⓦ' || t_ReplaceText[i + 4] == '비')
                                {
                                    if (DataBaseManager.Intel_SewerWorker3 == false)
                                    {
                                        DataBaseManager.Intel_SewerWorker3 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "SewerWorker - Cause of Fishy smell");
                                    }

                                }
                            }
                        }
                    }
                    break;

                case '◎':  // 연출관련
                    t_ignore = true; // 
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
                    if (t_ReplaceText[i + 1] == '⑦')
                    {
                        DataBaseManager.Kate_Disapear = true;
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
                    if (t_ReplaceText[i + 1] == '③')
                    {
                        DataBaseManager.SelectionOn = true;
                        selectionUIManager.Instance.Open_1st_Slum_Dave();
                    }
                    break;

                case '♠'://대화중 기능판단 출현
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        DataBaseManager.StrDialogOn = true;
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        if (DataBaseManager.Sewer_ObservationEnd == false)
                            DataBaseManager.Sewer_ObservationEnd = true;
                            DataBaseManager.Sewer_Observation = true;
                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {

                        DataBaseManager.Sewer_San = true;
                    }
                    if (t_ReplaceText[i + 1] == '④')
                    {
                        DataBaseManager.ConnectKey = true;

                    }
                    if (t_ReplaceText[i + 1] == '⑤')
                    {
                        DataBaseManager.EndDemo = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑥')
                    {
                        DataBaseManager.Battle_San = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑦')
                    {
                        DataBaseManager.isBattleBeforeDialog = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑧')
                    {
                        DataBaseManager.EndBattle = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑨')
                    {
                        DataBaseManager.Case2IntCheck = true;
                    }
                    t_ignore = true;
                    break;
                case '♣'://대화중 이성판정 2일차 이후
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        DataBaseManager.AfterSanCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {

                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {

                    }
                    if (t_ReplaceText[i + 1] == '④')
                    {


                    }
                    if (t_ReplaceText[i + 1] == '⑤')
                    {

                    }
                    if (t_ReplaceText[i + 1] == '⑥')
                    {

                    }
                    if (t_ReplaceText[i + 1] == '⑦')
                    {

                    }
                    if (t_ReplaceText[i + 1] == '⑧')
                    {

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
                case '①': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '☆' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '★' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '②': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 1] == '★' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '☆' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '③': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 1] == '★' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎' || t_ReplaceText[i - 1] == '♠') { t_ignore = true; } break;
                case '④': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '⑤': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑥': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑦': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑧': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑨': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑩': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i + 2] == '§' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑫': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i + 2] == '§' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                
                case 'ⓐ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓒ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓓ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓔ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓕ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓗ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓘ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓙ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓚ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓛ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓜ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓝ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓞ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓟ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓠ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓢ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓣ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓤ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓥ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓩ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;
                case 'ⓧ': if (t_ReplaceText[i  -2] == '●') { t_ignore = true; } break;

            }




            string t_letter = t_ReplaceText[i].ToString();

            if (!t_ignore)
            {//<color=#000000>
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
