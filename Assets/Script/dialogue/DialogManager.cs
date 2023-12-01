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
    public GameObject backGround;
    public GameObject image001;
    public GameObject image002;
    public GameObject image003;
    public GameObject image004;
    public GameObject image005;
    public GameObject image006;
    public GameObject image007;
    public GameObject image008;
    public GameObject image009;
    public GameObject image010;
    public GameObject image011;
    public GameObject image012;
    public GameObject image013;
    public GameObject image014;



    public void ActiveImage(GameObject obj)
    {
        if (obj.activeSelf == false)
        {
            obj.SetActive(true);
        }
        else if (obj.activeSelf == true)
        {
            obj.SetActive(false);
        }
    }
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
    public GameObject floatPosition;
    // 선택지 등장시  해당변수 활성화로 멈추기
    public bool showChoice = false;
    public GameObject skipButton;
    public GameObject skipStopButton;
    // Start is called before the first frame update
    public GameObject dialogFace;
    public GameObject fabian1;
    // ⓦ 이건 색깔  , 쉼표는 ' 로  \n으로 해서 작성하면 됨 엑셀
    public GameObject playerChar;
    public selectionUIManager selectionUIManager;
    public bool isDialogON = false;
    [SerializeField] GameObject goDialogBar;
    [SerializeField] GameObject goDialogNameBar;
    [SerializeField] TextMeshProUGUI txtDialog;
    [SerializeField] TextMeshProUGUI txtName;
    Dialog[] dialogs;
   // public Mins min;
    bool isDialog = false;
    bool isNext = false; // 특정 키 입력 대기.
    public bool isSelectButton = false;
    public bool firstClick = true;
    int lineCount = 0; // 대화 카운트(사람)
    int contextCount = 0; // 대사 카운트 (줄)
                          //기능판정 대화창
    public GameObject judgeDialog;
    bool zNext2 = true;
    bool zNext1;
    SpriteManager theSpriteManager;

    [Header("텍스트 출력 딜레이.")]
    [SerializeField] float textDelay;
    public TAnimPlayerBase typeA;
    public TextAnimatorPlayer textAnimatorPlayer;
    private void Start()
    {
        firstClick = true;
        isSelectButton = false;
        theSpriteManager = FindObjectOfType<SpriteManager>();
    }
    void Update()
    {
        SettingPlayerCon();
        TextCheker();
        if (Input.GetKeyDown(KeyCode.E) || (Input.GetMouseButtonDown(0)))
        {
            if (zNext2 == true)
            {
                zNext1 = true;
            }
            zNext2 = true;
        }
    }
    void TextCheker()
    {
        if (isDialog && DataBaseManager.selectionOn == false)
        {
            if (isNext)
            {
                if (Input.GetKeyDown(KeyCode.E) || (Input.GetMouseButtonDown(0) && DataBaseManager.daveCheck == false ))
                {
                    if (firstClick == true)
                    {
                        typeA.SkipTypewriter();
                        firstClick = false;
                    }
                    else
                    {
                        isNext = false;
                        txtDialog.text = "";
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
        if (DataBaseManager.selectionOn == false)
        {
            skipButton.SetActive(false);
            skipStopButton.SetActive(true);
            DataBaseManager.skipActive = true;
            textAnimatorPlayer.waitForNormalChars = 0.003f;
            textAnimatorPlayer.waitLong = 0.003f;
            textAnimatorPlayer.waitMiddle = 0.003f;
            StartCoroutine(Skip());
        }
        else if (DataBaseManager.selectionOn == true)
        {
            SkipStopON();
        }
    }
    public void SkipStopON()
    {
        textAnimatorPlayer.waitForNormalChars = 0.03f;
        textAnimatorPlayer.waitLong = 0.03f;
        textAnimatorPlayer.waitMiddle = 0.03f;
        DataBaseManager.skipActive = false;
        skipStopButton.SetActive(false);
        showChoice = true;
        StartCoroutine(SkipNomalize());
    }
    IEnumerator SkipNomalize()
    {
        yield return new WaitForSeconds(0.5f);
        showChoice = false;
        skipButton.SetActive(true);
    }
    IEnumerator Skip()
    {
        if(DataBaseManager.selectionOn == false)
        {
            zNext1 = true;   //이 부분을 true로 두면  스킵이 엄청 빨라지고 false로 두면 적당해짐
            zNext2 = true;
            yield return new WaitForSeconds(0.08f);
            ChoiceExNextPageT();
            yield return new WaitForSeconds(0.08f);
            if (goDialogBar.activeSelf == false)
            {
                SkipStopON();
                DataBaseManager.skipActive = false;
            }
            else if (showChoice == false)
            {
                StartCoroutine(Skip());
            }
            else if (showChoice == true)
            {
                DataBaseManager.skipActive = false;
            }
        }
        else if (DataBaseManager.selectionOn == true)
        {
            SkipStopON();
        }
    }
    public void ChoiceExNextPageT()
    {
        if (isDialog)
        {
            if (isNext)
            {
                isNext = false;
                txtDialog.text = "";
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
    public void ChoiceExNextPage()
    {
        skipButton.SetActive(true);
        skipStopButton.SetActive(false);
        DataBaseManager.skipActive = false;
        showChoice = false;
        if (isDialog)
        {
            if (isNext)
            {
                isNext = false;
                txtDialog.text = "";
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
        DataBaseManager.isJudge = false;
        DataBaseManager.isKeyword = false;
        firstClick = true;
        DataBaseManager.isActiveDialog2 = false;
        dialogFace.SetActive(false);
        isDialog = false;
        contextCount = 0;
        lineCount = 0;
        dialogs = null;
        isDialogON = false;
        isNext = false;
        SettingUI(false);
        DataBaseManager.nowSelecter = "End";
       
        End_Look_Judge.Instance.Inactive();
    }

    public void ShowDialog(Dialog[] P_dialogs)
    {
        DataBaseManager.isActiveDialog1 = true;
        DataBaseManager.isActiveDialog2 = true;
        dialogFace.SetActive(true);
        isDialog = true;
        txtDialog.text = "";
        txtName.text = "";
        dialogs = P_dialogs;
        StartCoroutine(TypeWriter());
    }
    public void TypeWriterForce()
    {
        SettingUI(true);
        string t_ReplaceText = dialogs[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("^", ",");
        txtDialog.text = t_ReplaceText;
        isNext = true;
    }
    IEnumerator TypeWriter()
    {
        SettingUI(true);
        string t_ReplaceText = dialogs[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("^", ",");
        t_ReplaceText = t_ReplaceText.Replace('@', '"');
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");
        bool t_white = false, t_yellow = false, t_red = false, t_green = false, t_blue = false, t_grey = false;   // 색 추가 지점
        bool t_ignore = false;
        if (DataBaseManager.isDebuffDeftness == true)
        {
            t_ReplaceText = t_ReplaceText.Replace("A", ".");
            t_ReplaceText = t_ReplaceText.Replace("B", ".");
            t_ReplaceText = t_ReplaceText.Replace("C", ".");
            t_ReplaceText = t_ReplaceText.Replace("D", ".");
            t_ReplaceText = t_ReplaceText.Replace("E", ".");
            t_ReplaceText = t_ReplaceText.Replace("F", ".");
            t_ReplaceText = t_ReplaceText.Replace("G", ".");
            t_ReplaceText = t_ReplaceText.Replace("H", ".");
            t_ReplaceText = t_ReplaceText.Replace("I", ".");
            t_ReplaceText = t_ReplaceText.Replace("J", ".");
            t_ReplaceText = t_ReplaceText.Replace("K", ".");
            t_ReplaceText = t_ReplaceText.Replace("L", ".");
            t_ReplaceText = t_ReplaceText.Replace("M", ".");
            t_ReplaceText = t_ReplaceText.Replace("N", ".");
            t_ReplaceText = t_ReplaceText.Replace("O", ".");
            t_ReplaceText = t_ReplaceText.Replace("P", ".");
            t_ReplaceText = t_ReplaceText.Replace("Q", ".");
            t_ReplaceText = t_ReplaceText.Replace("R", ".");
            t_ReplaceText = t_ReplaceText.Replace("S", ".");
            t_ReplaceText = t_ReplaceText.Replace("T", ".");
            t_ReplaceText = t_ReplaceText.Replace("U", ".");
            t_ReplaceText = t_ReplaceText.Replace("V", ".");
            t_ReplaceText = t_ReplaceText.Replace("W", ".");
            t_ReplaceText = t_ReplaceText.Replace("X", ".");
            t_ReplaceText = t_ReplaceText.Replace("Y", ".");
            t_ReplaceText = t_ReplaceText.Replace("Z", ".");

            t_ReplaceText = t_ReplaceText.Replace("a", ".");
            t_ReplaceText = t_ReplaceText.Replace("b", ".");
            t_ReplaceText = t_ReplaceText.Replace("c", ".");
            t_ReplaceText = t_ReplaceText.Replace("d", ".");
            t_ReplaceText = t_ReplaceText.Replace("e", ".");
            t_ReplaceText = t_ReplaceText.Replace("f", ".");
            t_ReplaceText = t_ReplaceText.Replace("g", ".");
            t_ReplaceText = t_ReplaceText.Replace("h", ".");
            t_ReplaceText = t_ReplaceText.Replace("i", ".");
            t_ReplaceText = t_ReplaceText.Replace("j", ".");
            t_ReplaceText = t_ReplaceText.Replace("k", ".");
            t_ReplaceText = t_ReplaceText.Replace("l", ".");
            t_ReplaceText = t_ReplaceText.Replace("m", ".");
            t_ReplaceText = t_ReplaceText.Replace("n", ".");
            t_ReplaceText = t_ReplaceText.Replace("o", ".");
            t_ReplaceText = t_ReplaceText.Replace("p", ".");
            t_ReplaceText = t_ReplaceText.Replace("q", ".");
            t_ReplaceText = t_ReplaceText.Replace("r", ".");
            t_ReplaceText = t_ReplaceText.Replace("s", ".");
            t_ReplaceText = t_ReplaceText.Replace("t", ".");
            t_ReplaceText = t_ReplaceText.Replace("u", ".");
            t_ReplaceText = t_ReplaceText.Replace("v", ".");
            t_ReplaceText = t_ReplaceText.Replace("w", ".");
            t_ReplaceText = t_ReplaceText.Replace("x", ".");
            t_ReplaceText = t_ReplaceText.Replace("y", ".");
            t_ReplaceText = t_ReplaceText.Replace("z", ".");
        } // 언어 추가시 변경 필요
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            switch (t_ReplaceText[i])
            {
                case 'ⓦ':
                    if (t_ReplaceText[i - 1] == ')')
                    {
                        t_white = true; t_yellow = false; t_red = false; t_green = false; t_blue = false; t_ignore = true; t_grey = false; break;
                    }
                    t_ignore = true;
                    break;
                case 'ⓨ': t_white = false; t_yellow = true; t_red = false; t_green = false; t_blue = false; t_ignore = true; t_grey = false; break;
                case '⒳': t_white = false; t_yellow = false; t_red = false; t_green = false; t_blue = false; t_ignore = true; t_grey = true; break;
                case 'ⓡ': t_white = false; t_yellow = false; t_red = true; t_green = false; t_blue = false; t_grey = false; t_ignore = true; 
                    if (t_ReplaceText[i + 1] == '(')
                    {
                        if (t_ReplaceText[i + 2] == '●')
                        {
                            if (t_ReplaceText[i + 3] == '1')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓟ')
                                {
                                    if (DataBaseManager.intelPlanetaryParade1 == false)
                                    {
                                        IntelManager.Instance.AddIntelFloating("Event", "PlanetaryParade - Contents of a Newspaper");
                                        DataBaseManager.intelPlanetaryParade1 = true;
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓘ')
                                {
                                    if (DataBaseManager.intelInsomnia1 == false)
                                    {
                                        DataBaseManager.intelInsomnia1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "Insomnia - Newspaper Article");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓝ')
                                {
                                    if (DataBaseManager.intelNightmare1 == false)
                                    {
                                        DataBaseManager.intelNightmare1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "Nightmare - Aiden's Nightmare");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓞ')
                                {
                                    if (DataBaseManager.intelParanoia1 == false)
                                    {
                                        DataBaseManager.intelParanoia1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "Paranoia - Husband's Condition");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓐ')
                                {
                                    if (DataBaseManager.intelSafe1 == false)
                                    {
                                        DataBaseManager.intelSafe1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "Safe - Aiden's Safe");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓢ') // 하수도 괴담
                                {
                                    if (DataBaseManager.intelSewerGhostStory1 == false)
                                    {
                                        DataBaseManager.intelSewerGhostStory1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "SewerGhostStory - Contents of a GhostStory");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓕ') // 비릿한 냄새
                                {
                                    if (DataBaseManager.intelFishySmell1 == false)
                                    {
                                        DataBaseManager.intelFishySmell1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "FishySmell - Rumor");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓜ') // 연속실종사건
                                {
                                    if (DataBaseManager.intelMissingPeople2 == false)
                                    {
                                        DataBaseManager.intelMissingPeople2 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "MissingPeople - Reporting by Swain");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓡ') // 강의 수질
                                {
                                    if (DataBaseManager.intelRiverWaterQuality1 == false)
                                    {
                                        DataBaseManager.intelRiverWaterQuality1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "RiverWaterQuality - Strange Point");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓒ') // 섬뜩한 눈
                                {
                                    if (DataBaseManager.intelCreepyEyes1 == false)
                                    {
                                        DataBaseManager.intelCreepyEyes1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "CreepyEyes - Saw in the sewer");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓩ')
                                {
                                    if (DataBaseManager.intelMurderCase1 == false)
                                    {
                                        IntelManager.Instance.AddIntelFloating("Event", "MurderCase - Witness Testimony");
                                        DataBaseManager.intelMurderCase1 = true;
                                    }
                                }
                            }
                            if (t_ReplaceText[i + 3] == '2')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓟ')
                                {
                                    if (DataBaseManager.intelPlanetaryParade2 == false)
                                        IntelManager.Instance.AddIntelFloating("Event", "planetaryparade - Meetings");
                                    DataBaseManager.intelPlanetaryParade2 = true;
                                }
                                if (t_ReplaceText[i + 4] == 'ⓘ')
                                {
                                    if (DataBaseManager.intelInsomnia2 == false)
                                        DataBaseManager.intelInsomnia2 = true;
                                    IntelManager.Instance.AddIntelFloating("Event", "Insomnia - Doctor's opinion");
                                }
                                if (t_ReplaceText[i + 4] == 'ⓝ')
                                {
                                    if (DataBaseManager.intelNightmare2 == false)
                                        DataBaseManager.intelNightmare2 = true;
                                    IntelManager.Instance.AddIntelFloating("Event", "Nightmare - Content of a Nightmare");
                                }
                                if (t_ReplaceText[i + 4] == 'ⓞ')
                                {
                                    if (DataBaseManager.intelParanoia2 == false)
                                    {
                                        DataBaseManager.intelParanoia2 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "Paranoia - Cause of Paranoia");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓐ')
                                {
                                    if (DataBaseManager.intelSafe2 == false)
                                    {
                                        DataBaseManager.intelSafe2 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "Safe - Acquiring a Safe");
                                        IntelManager.Instance.AddIntelFloating("Item", "KeyItem - Safe");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓜ') // 연속실종사건
                                {
                                    if (DataBaseManager.intelMissingPeople1 == false)
                                    {
                                        DataBaseManager.intelMissingPeople1 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "MissingPeople - Albert's Information");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓕ') // 비릿한 냄새
                                {
                                    if (DataBaseManager.intelFishySmell2 == false)
                                    {
                                        DataBaseManager.intelFishySmell2 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "FishySmell - Source of the smell?");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓡ') // 강의 수질
                                {
                                    if (DataBaseManager.intelRiverWaterQuality2 == false)
                                    {
                                        DataBaseManager.intelRiverWaterQuality2 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "RiverWaterQuality - Details");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓢ') // 하수도 괴담
                                {
                                    if (DataBaseManager.intelSewerGhostStory2 == false)
                                    {
                                        DataBaseManager.intelSewerGhostStory2 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "SewerGhostStory - Truth of a rumor");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓩ')
                                {
                                    if (DataBaseManager.intelMurderCase2 == false)
                                    {
                                        IntelManager.Instance.AddIntelFloating("Event", "MurderCase - Missing People & Fishy Smell");
                                        DataBaseManager.intelMurderCase2 = true;
                                    }
                                }
                            }
                            if (t_ReplaceText[i + 3] == '3')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓝ')
                                {
                                    if (DataBaseManager.intelNightmare3 == false)
                                        DataBaseManager.intelNightmare3 = true;
                                    IntelManager.Instance.AddIntelFloating("Event", "Nightmare - Aiden's Detail");
                                }
                                if (t_ReplaceText[i + 4] == 'ⓜ') // 연속실종사건
                                {
                                    if (DataBaseManager.intelMissingPeople3 == false)
                                    {
                                        DataBaseManager.intelMissingPeople3 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "MissingPeople - Kidnap");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓕ') // 비릿한 냄새
                                {
                                    if (DataBaseManager.intelFishySmell3 == false)
                                    {
                                        DataBaseManager.intelFishySmell3 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "FishySmell -Source speculation");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓡ') // 강의 수질
                                {
                                    if (DataBaseManager.intelRiverWaterQuality3 == false)
                                    {
                                        DataBaseManager.intelRiverWaterQuality3 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "RiverWaterQuality - Fishy Slime");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓩ')
                                {
                                    if (DataBaseManager.intelMurderCase3 == false)
                                    {
                                        IntelManager.Instance.AddIntelFloating("Event", "MurderCase - The Culprit");
                                        DataBaseManager.intelMurderCase3 = true;
                                    }
                                }
                            }
                            if (t_ReplaceText[i + 3] == '4')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓝ')
                                {
                                    if (DataBaseManager.intelNightmare4 == false)
                                        DataBaseManager.intelNightmare4 = true;
                                    IntelManager.Instance.AddIntelFloating("Event", "Nightmare - Water and Nightmare");
                                }
                                if (t_ReplaceText[i + 4] == 'ⓕ') // 비릿한 냄새
                                {
                                    if (DataBaseManager.intelFishySmell4 == false)
                                    {
                                        DataBaseManager.intelFishySmell4 = true;
                                        IntelManager.Instance.AddIntelFloating("Event", "FishySmell - Sewer & FishySmell");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓩ')
                                {
                                    if (DataBaseManager.intelMurderCase4 == false)
                                    {
                                        IntelManager.Instance.AddIntelFloating("Event", "MurderCase - Cause of Death");
                                        DataBaseManager.intelMurderCase4 = true;
                                    }
                                }
                            }
                            if (t_ReplaceText[i + 3] == '5')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓩ')
                                {
                                    if (DataBaseManager.intelMurderCase5 == false)
                                    {
                                        IntelManager.Instance.AddIntelFloating("Event", "MurderCase - Webbed Feet");
                                        DataBaseManager.intelMurderCase5 = true;
                                    }
                                }
                            }
                            if (t_ReplaceText[i + 3] == '6')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓩ')
                                {
                                    if (DataBaseManager.intelMurderCase6 == false)
                                    {
                                        IntelManager.Instance.AddIntelFloating("Event", "MurderCase - Way to Escape");
                                        DataBaseManager.intelMurderCase6 = true;
                                    }
                                }
                            }
                        }
                        }
                    break;
                case 'ⓑ': t_white = false; t_yellow = false; t_red = false; t_green = false; t_blue = true; t_grey = false; t_ignore = true;
                    if (t_ReplaceText[i + 1] == '(')
                    {
                        if (t_ReplaceText[i + 2] == '●')
                        {
                            if (t_ReplaceText[i + 3] == '1')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓧ')
                                {
                                    if (DataBaseManager.intelSewer1 == false)
                                    {
                                        DataBaseManager.intelSewer1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Sewer - Aiden's warning");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓦ')
                                {
                                    if (DataBaseManager.intelWharf1 == false)
                                    {
                                        DataBaseManager.intelWharf1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Wharf - Work");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓗ')
                                {
                                    if (DataBaseManager.intelHospital1 == false)
                                    {
                                        DataBaseManager.intelHospital1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Hospital - State of congestion");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓠ')
                                {
                                    if (DataBaseManager.intelQuestHouse1 == false)
                                    {
                                        DataBaseManager.intelQuestHouse1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Client'shouse - Adress");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓝ')
                                {
                                    if (DataBaseManager.intelNewspaper1 == false)
                                    {
                                        DataBaseManager.intelNewspaper1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "DailyNews - Husband's job");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓢ')
                                {
                                    if (DataBaseManager.intelSewageMaintenanceOffice1 == false)
                                    {
                                        DataBaseManager.intelSewageMaintenanceOffice1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "SewageTreatmentOffice - Location");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓤ' )
                                {
                                    if (DataBaseManager.intelUniversity1 == false)
                                    {
                                        DataBaseManager.intelUniversity1 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Univ of Miscatonic - Astronomical show");
                                    }
                                }
                            }
                            if (t_ReplaceText[i + 3] == '2')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓧ')
                                {
                                    if (DataBaseManager.intelSewer2 == false)
                                    {
                                        DataBaseManager.intelSewer2 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Sewer - Root of problem");
                                    }
                                }
                            }
                            if (t_ReplaceText[i + 3] == '3')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓧ')
                                {
                                    if (DataBaseManager.intelSewer3 == false)
                                    {
                                        DataBaseManager.intelSewer3 = true;
                                        IntelManager.Instance.AddIntelFloating("Place", "Sewer - Manhole");
                                    }
                                }
                            }
                            if (t_ReplaceText[i + 3] == '4')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓧ')
                                {
                                    if (DataBaseManager.intelSewer4 == false)
                                    {
                                        DataBaseManager.intelSewer4 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "SewerWorker - Advent");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓦ')
                                {
                                    if (DataBaseManager.intelSewerWorker4 == false)
                                    {
                                        DataBaseManager.intelSewerWorker4 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "SewerWorker - Owner of Uniform");
                                    }
                                }
                            }
                        }
                    }
                    break;

                case 'ⓖ': t_white = false; t_yellow = false; t_red = false; t_green = true; t_blue = false; t_grey = false; t_ignore = true; 
                    if (t_ReplaceText[i + 1] == '(')
                    {
                        if (t_ReplaceText[i + 2] == '●')
                        {
                            if (t_ReplaceText[i + 3] == '1')
                            {
                                   if (t_ReplaceText[i + 4] == 'ⓥ')
                                {
                                    if (DataBaseManager.intelDave1 == false)
                                    {
                                        DataBaseManager.intelDave1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Dave - Bracelet");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓛ')
                                {
                                    if (DataBaseManager.intelAlbert1 == false)
                                    {
                                        DataBaseManager.intelAlbert1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Albert Bradley - Police friend");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓜ')
                                {
                                    if (DataBaseManager.intelMeiv1 == false)
                                    {
                                        DataBaseManager.intelMeiv1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Maeve A Rossi - Mafia friend");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓔ')
                                {
                                    if (DataBaseManager.intelElla1 == false)
                                    {
                                        DataBaseManager.intelElla1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Ella Triss - Client");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓐ')
                                {
                                    if (DataBaseManager.intelAiden1 == false)
                                    {
                                        DataBaseManager.intelAiden1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Aiden Triss - Subject to request");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓦ')
                                {
                                    if (DataBaseManager.intelSewerWorker1 == false)
                                    {
                                        DataBaseManager.intelSewerWorker1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "SewerWorker - Owner of Eye?");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓧ')
                                {
                                    if (DataBaseManager.intelSwain1 == false)
                                    {
                                        DataBaseManager.intelSwain1 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Berkeley Swain - Sewer Survey");
                                    }
                                }
                            }
                            if (t_ReplaceText[i + 3] == '2')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓔ')
                                {
                                    if (DataBaseManager.intelElla2 == false)
                                    {
                                        DataBaseManager.intelElla2 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Ella Triss - Murdered Clients");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓐ')
                                {
                                    if (DataBaseManager.intelAiden2 == false)
                                    {
                                        DataBaseManager.intelAiden2 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Aiden Triss - Aiden and Pandemic");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓦ')
                                {
                                    if (DataBaseManager.intelSewerWorker2 == false)
                                    {
                                        DataBaseManager.intelSewerWorker2 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "SewerWorker - Unfair dismissal");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓜ')
                                {
                                    if (DataBaseManager.intelMeiv2 == false)
                                    {
                                        DataBaseManager.intelMeiv2 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Maeve A Rossi - Promise");
                                    }
                                }
                            }
                            if (t_ReplaceText[i + 3] == '3')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓐ')
                                {
                                    if (DataBaseManager.intelAiden3 == false)
                                    {
                                        DataBaseManager.intelAiden3 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "Aiden Triss - Missing Subject");
                                    }
                                }
                                if (t_ReplaceText[i + 4] == 'ⓦ' || t_ReplaceText[i + 4] == '비')
                                {
                                    if (DataBaseManager.intelSewerWorker3 == false)
                                    {
                                        DataBaseManager.intelSewerWorker3 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "SewerWorker - Cause of Fishy smell");
                                    }
                                }
                            }
                            if (t_ReplaceText[i + 3] == '4')
                            {
                                if (t_ReplaceText[i + 4] == 'ⓦ')
                                {
                                    if (DataBaseManager.intelSewerWorker4 == false)
                                    {
                                        DataBaseManager.intelSewerWorker4 = true;
                                        IntelManager.Instance.AddIntelFloating("Character", "SewerWorker - Owner of Uniform");
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
                        DataBaseManager.fstDetectiveEndFirstDialog = true;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        t_ignore = true;
                        DataBaseManager.fstDetectiveNockDialog = true;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {
                        t_ignore = true;
                        DataBaseManager.fstDetectiveAfterSelect = true;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '④')
                    {
                        t_ignore = true;
                        DataBaseManager.fstDetectiveEndSelect = true;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '⑤')
                    {
                        DataBaseManager.fstDetectiveAfterSelectNo = true;
                        t_ignore = true;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '⑥')
                    {
                        DataBaseManager.failTwoKeyword = true;
                        t_ignore = true;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '⑦')
                    {
                        DataBaseManager.kateDisapear = true;
                        t_ignore = true;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '⑧')
                    {
                        DataBaseManager.noonEventMovetoClient = true;
                        t_ignore = true;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '⑨')
                    {
                        t_ignore = true;
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
                        skipButton.SetActive(false);
                        skipStopButton.SetActive(false);
                        showChoice = true;
                        t_ignore = true;
                        isSelectButton = true;
                        break;
                    }
                    if (t_ReplaceText[i + 1] == '⑶')
                    {
                        skipButton.SetActive(false);
                        skipStopButton.SetActive(false);
                        showChoice = true;
                        t_ignore = true;
                        isSelectButton = true;
                        break;
                    }
                    break;
                case '★':  // 검은별은 선택지이후 연결파트 표시
                    t_ignore = true;
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        DataBaseManager.selectionOn = true;
                        selectionUIManager.Instance.Open1stDetectiveOfficeSelect1();
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        DataBaseManager.selectionOn = true;
                        selectionUIManager.Instance.Open1stClientsOfficSafeSelect();
                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {
                        //DataBaseManager.SelectionOn = true;
                        selectionUIManager.Instance.Open1stSlumDave();
                        DataBaseManager.daveCheck = true;
                    }
                    break;

                case '♠'://대화중 기능판단 출현
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        DataBaseManager.strDialogOn = true;
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        if (DataBaseManager.sewerObservationEnd == false)
                            DataBaseManager.sewerObservationEnd = true;
                            DataBaseManager.sewerObservation = true;
                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {

                        DataBaseManager.sewerSan = true;
                    }
                    if (t_ReplaceText[i + 1] == '④')
                    {
                        DataBaseManager.connectKey = true;

                    }
                    if (t_ReplaceText[i + 1] == '⑤')
                    {
                        DataBaseManager.endDemo = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑥')
                    {
                        DataBaseManager.battleSan = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑦')
                    {
                        DataBaseManager.isBattleBeforeDialog = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑧')
                    {
                        DataBaseManager.endBattle = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑨')
                    {
                        DataBaseManager.case2IntCheck = true;
                    }
                    t_ignore = true;
                    break;
                case '♣'://대화중 이성판정 2일차 이후
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        DataBaseManager.afterSanCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        DataBaseManager.noonEventSanCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {
                        DataBaseManager.deadBodyGotoSwere = true;
                    }
                    if (t_ReplaceText[i + 1] == '④')
                    {
                        DataBaseManager.swainDie = transform;
                    }
                    if (t_ReplaceText[i + 1] == '⑤')
                    {
                        DataBaseManager.kaneSlimeSan = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑥')
                    {
                        DataBaseManager.daveDeathKeyword = true;
                        DataBaseManager.getSafe = false;
                        DataBaseManager.intelSafe2 = false;
                    }
                    if (t_ReplaceText[i + 1] == '⑦')
                    {
                        DataBaseManager.getSafe = false;
                        DataBaseManager.intelSafe2 = false;
                        DataBaseManager.daveSafeOpen = true;
                        //여기에 금고 성공시 나오는 대사
                    }
                    if (t_ReplaceText[i + 1] == '⑧')
                    {
                        DataBaseManager.weal += 10;
                    }
                    t_ignore = true;
                    break;
                // 최초 심리학 기능판정 선택지 출력창.
                case '※':// 연출 관련
                    if (t_ReplaceText[i + 1] == '①')
                    {
                      //  DataBaseManager.isDirecting = false;
                       // DataBaseManager.secondisDirecting = false;
                        DataBaseManager.storyDirecting = false; 
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        DataBaseManager.barMove = true;
                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {
                        DataBaseManager.barDrinkSanCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '④')
                    {
                        DataBaseManager.thirdDayDreamSan = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑤')
                    {
                        DataBaseManager.day4Case1San = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑥')
                    {
                        DataBaseManager.day4Case1Health = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑦')
                    {
                        DataBaseManager.moveSewerOfficeWindow = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑧')
                    {
                        BillowUIManager.Instance.HpDown(5);
                    }
                    t_ignore = true;
                    break;
                case '⊙':// 연출 관련⊙⑨

                    if (t_ReplaceText[i + 1] == '①')
                    {
                        if (DataBaseManager.gainCoat == false)
                        {
                            IntelManager.Instance.AddIntelFloating("Item", "KeyItem - Coat");
                            DataBaseManager.gainCoat = true;
                        }
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        if (DataBaseManager.sewerageKey == false)
                        {
                            IntelManager.Instance.AddIntelFloating("Item", "KeyItem - SewerageKey");
                            DataBaseManager.sewerageKey = true;
                        }
                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {
                        if (DataBaseManager.gainMap == false)
                        {
                            IntelManager.Instance.AddIntelFloating("Item", "KeyItem - Map");
                            DataBaseManager.gainMap = true;
                        }
                    }
                    if (t_ReplaceText[i + 1] == '④')
                    {
                        //적 발견시 선택지 출력
                        DataBaseManager.selectionOn = true;
                        selectionUIManager.Instance.OpenSelectSewerEnemy();
                        DataBaseManager.storyDirecting = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑤')
                    {
                        // 은밀행동 성공시
                        DataBaseManager.storyDirecting = false;
                        DataBaseManager.inSewerStealthSucc = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑥')
                    {
                        // 은밀행동 실패시
                        DataBaseManager.inSewerStealthFail = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑦')
                    {
                        DataBaseManager.insmusRhtoric = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑧')
                    {
                        IntelManager.Instance.AddIntelFloating("Item", "KeyItem - Document");
                        DataBaseManager.safeDocument = true;
                        DataBaseManager.weal += 10;
                    }
                    if (t_ReplaceText[i + 1] == '⑨')
                    {
                        //지도 획득
                        DataBaseManager.gainOldMap = transform;
                        IntelManager.Instance.AddIntelFloating("Item", "KeyItem - Old Map");
                        TimeManagere.Instance.CloseOldMap();
                        DataBaseManager.sewerMap = true;
                    }

                    t_ignore = true;
                    break;
                case '◐'://◐⑦⑥⑤④
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        if(DataBaseManager.journalFirstSanCheck == false)
                        {
                            DataBaseManager.journalSanCheck = true;
                        }
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        DataBaseManager.sewerBookSanCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {
                        DataBaseManager.sewerBookOccultCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '④')
                    {
                        DataBaseManager.sewerSymbolOccultCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑤')
                    {
                        DataBaseManager.symbolStrSanCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑥')
                    {
                        DataBaseManager.sewerFirstInSanCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑦')
                    {
                        DataBaseManager.sewerDeepOneFirstSanCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑧') // 사다리 선택지
                    {
                        DataBaseManager.selectionOn = true;
                        selectionUIManager.Instance.OpenLadderSelectUI();
                    }
                    if (t_ReplaceText[i + 1] == '⑨')
                    {
                        DataBaseManager.endingSwin = true;
                    }
                    t_ignore = true;
                    break;
                case '◈'://◐◈⑩⑪⑨⑧⑦⑥④③⑦⑥◈⑤④
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        DataBaseManager.endingLadderSan = true;
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        DataBaseManager.endingBreakSymbol = true;
                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {
                        DataBaseManager.endingisPromise = true;
                    }
                    if (t_ReplaceText[i + 1] == '④')
                    {
                        DataBaseManager.endingAfterPromiseDialog = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑤')
                    {
                        DataBaseManager.endingSwainDeathCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑥')
                    {
                        DataBaseManager.endingDeathCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑦')
                    {
                        DataBaseManager.endingDoorCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑧') // 사다리 선택지
                    {
                        DataBaseManager.endingRequestCheck = true;
                    }
                    if (t_ReplaceText[i + 1] == '⑨')
                    {
                        ActiveImage(image001);
                    }
                    if (t_ReplaceText[i + 1] == '⑩')
                    {
                        backGround.SetActive(false);
                    }
                    if (t_ReplaceText[i + 1] == '⑪')
                    {
                        backGround.SetActive(true);
                    }
                    t_ignore = true;
                    break;
                case '▒'://▒⑨⑧⑦⑥①
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        ActiveImage(image002);
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        ActiveImage(image003);
                    }
                    if (t_ReplaceText[i + 1] == '③')
                    {
                        ActiveImage(image004);
                    }
                    if (t_ReplaceText[i + 1] == '④')
                    {
                        ActiveImage(image005);
                    }
                    if (t_ReplaceText[i + 1] == '⑤')
                    {
                        ActiveImage(image006);
                    }
                    if (t_ReplaceText[i + 1] == '⑥')
                    {
                        ActiveImage(image007);
                    }
                    if (t_ReplaceText[i + 1] == '⑦')
                    {
                        ActiveImage(image008);
                    }
                    if (t_ReplaceText[i + 1] == '⑧')
                    {
                        ActiveImage(image009);
                    }
                    if (t_ReplaceText[i + 1] == '⑨')
                    {
                        ActiveImage(image010);
                    }
                    if (t_ReplaceText[i + 1] == '⑩')
                    {
                        ActiveImage(image011);
                    }
                    if (t_ReplaceText[i + 1] == '⑪')
                    {
                        ActiveImage(image012);
                    }
                    t_ignore = true;
                    break;
                case '◑'://◑
                    if (t_ReplaceText[i + 1] == '①')
                    {
                        ActiveImage(image013);
                    }
                    if (t_ReplaceText[i + 1] == '②')
                    {
                        ActiveImage(image014);
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
                    if (t_ReplaceText[i + 1] == '⑧') // 사다리 선택지
                    {

                    }
                    if (t_ReplaceText[i + 1] == '⑨')
                    {

                    }
                    if (t_ReplaceText[i + 1] == '⑩')
                    {

                    }
                    if (t_ReplaceText[i + 1] == '⑪')
                    {

                    }
                    t_ignore = true;
                    break;
                case '⒭':
                    theSpriteManager.AllReset();
                    t_ignore = true;
                    break;
                case '§':
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '①' && t_ReplaceText[i + 4] == '§') { theSpriteManager.Albert1(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '②' && t_ReplaceText[i + 4] == '§') { theSpriteManager.Albert2(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '③' && t_ReplaceText[i + 4] == '§') { theSpriteManager.Albert3(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '④' && t_ReplaceText[i + 4] == '§') { theSpriteManager.Albert4(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑤' && t_ReplaceText[i + 4] == '§') { theSpriteManager.Albert5(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑥' && t_ReplaceText[i + 4] == '§') { theSpriteManager.Albert6(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑦' && t_ReplaceText[i + 4] == '§') { theSpriteManager.Albert7(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑧' && t_ReplaceText[i + 4] == '§') { theSpriteManager.Albert8(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑨' && t_ReplaceText[i + 4] == '§') { theSpriteManager.Albert9(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑩' && t_ReplaceText[i + 4] == '§') { theSpriteManager.Ella2(); };
                    if (t_ReplaceText[i + 1] == '㉡' && t_ReplaceText[i + 2] == '㉧' && t_ReplaceText[i + 3] == '⑩' && t_ReplaceText[i + 4] == '①' && t_ReplaceText[i + 5] == '§') { theSpriteManager.Ella1(); };
                    if (t_ReplaceText[i + 1] == '엘' && t_ReplaceText[i + 2] == '라' && t_ReplaceText[i + 3] == '1' && t_ReplaceText[i + 4] == '§') { theSpriteManager.Ella1(); }; t_ignore = true; break; // 표정변경.

                //㉠ ㉡ ㉢ ㉣ ㉤ ㉥ ㉦ ㉧ ㉨ ㉩ ㉪ ㉫ ㉬ ㉭▒
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
                //case '0': if (t_ReplaceText[i + 1] == '§') { t_ignore = true; } if (t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;◈
                case '①': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 1] == '◑' || t_ReplaceText[i - 1] == '▒' || t_ReplaceText[i - 1] == '◈' || t_ReplaceText[i - 1] == '◐' || t_ReplaceText[i - 1] == '※' || t_ReplaceText[i - 1] == '⊙' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '☆' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '★' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '②': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 1] == '◑' || t_ReplaceText[i - 1] == '▒' || t_ReplaceText[i - 1] == '◈' || t_ReplaceText[i - 1] == '◐' || t_ReplaceText[i - 1] == '※' || t_ReplaceText[i - 1] == '⊙' || t_ReplaceText[i - 1] == '★' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '☆' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '③': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 1] == '◑' || t_ReplaceText[i - 1] == '▒' || t_ReplaceText[i - 1] == '◈' || t_ReplaceText[i - 1] == '◐' || t_ReplaceText[i - 1] == '※' || t_ReplaceText[i - 1] == '⊙' || t_ReplaceText[i - 1] == '★' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◎' || t_ReplaceText[i - 1] == '♠') { t_ignore = true; } break;
                case '④': if (t_ReplaceText[i - 1] == '⑩' || t_ReplaceText[i - 1] == '◑' || t_ReplaceText[i - 1] == '▒' || t_ReplaceText[i - 1] == '◈' || t_ReplaceText[i - 1] == '◐' || t_ReplaceText[i - 1] == '※' || t_ReplaceText[i - 1] == '⊙' || t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '◎') { t_ignore = true; } break;
                case '⑤': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◑' || t_ReplaceText[i - 1] == '▒' || t_ReplaceText[i - 1] == '◈' || t_ReplaceText[i - 1] == '◐' || t_ReplaceText[i - 1] == '※' || t_ReplaceText[i - 1] == '⊙' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑥': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◑' || t_ReplaceText[i - 1] == '▒' || t_ReplaceText[i - 1] == '◈' || t_ReplaceText[i - 1] == '◐' || t_ReplaceText[i - 1] == '※' || t_ReplaceText[i - 1] == '⊙' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑦': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◑' || t_ReplaceText[i - 1] == '▒' || t_ReplaceText[i - 1] == '◈' || t_ReplaceText[i - 1] == '◐' || t_ReplaceText[i - 1] == '※' || t_ReplaceText[i - 1] == '⊙' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑧': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◑' || t_ReplaceText[i - 1] == '▒' || t_ReplaceText[i - 1] == '◈' || t_ReplaceText[i - 1] == '◐' || t_ReplaceText[i - 1] == '※' || t_ReplaceText[i - 1] == '⊙' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑨': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◑' || t_ReplaceText[i - 1] == '▒' || t_ReplaceText[i - 1] == '◈' || t_ReplaceText[i - 1] == '◐' || t_ReplaceText[i - 1] == '※' || t_ReplaceText[i - 1] == '⊙' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i - 1] == '◎' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑩': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◑' || t_ReplaceText[i - 1] == '▒' || t_ReplaceText[i - 1] == '◈' || t_ReplaceText[i - 1] == '◐' || t_ReplaceText[i - 1] == '※' || t_ReplaceText[i - 1] == '⊙' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i + 2] == '§' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑪': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◑' || t_ReplaceText[i - 1] == '▒' || t_ReplaceText[i - 1] == '◈' || t_ReplaceText[i - 1] == '◐' || t_ReplaceText[i - 1] == '※' || t_ReplaceText[i - 1] == '⊙' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i + 2] == '§' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
                case '⑫': if (t_ReplaceText[i - 3] == '§' || t_ReplaceText[i - 1] == '◑' || t_ReplaceText[i - 1] == '▒' || t_ReplaceText[i - 1] == '◈' || t_ReplaceText[i - 1] == '◐' || t_ReplaceText[i - 1] == '※' || t_ReplaceText[i - 1] == '⊙' || t_ReplaceText[i - 1] == '♠' || t_ReplaceText[i - 1] == '♣' || t_ReplaceText[i + 2] == '§' | t_ReplaceText[i - 1] == '☆') { t_ignore = true; } break;
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
                if (t_white) { t_letter = "<color=#FFFFFF></i>" + t_letter + "</color>"; }
                else if (t_yellow) { t_letter = "<color=#FFFF00>" + t_letter + "</color>"; }
                else if (t_red) { t_letter = "<color=#B30000>" + t_letter + "</color>"; }
                else if (t_blue) { t_letter = "<color=#0026A9>" + t_letter + "</color>"; }
                else if (t_green) { t_letter = "<color=#1A6800>" + t_letter + "</color>"; }
                else if (t_grey) { t_letter = "<i><color=#C8C2C2>" + t_letter + "</color>"; }
                txtDialog.text += t_letter;
            }
            t_ignore = false;
        }
        firstClick = true;
        isNext = true;
        yield return null;
    }
    public void EndTypeSKip()
    {
        DataBaseManager.endDialogLine = true;
        firstClick = false;
    }
    public void StartTypeSKip()
    {
        firstClick = true;
    }
    void SettingUI(bool P_flag)
    {
        goDialogBar.SetActive(P_flag);
        if (P_flag)
        {
            if (dialogs[lineCount].name == "")
            {
                goDialogNameBar.SetActive(false);
            }
            else
            {
                goDialogNameBar.SetActive(true);
                txtName.text = dialogs[lineCount].name;
            }
        }
    }
    void SettingPlayerCon()
    {
        if (goDialogBar.activeSelf == true)
        {
            // PlayerChar.transform.GetComponent<Mins>().isDialogOnChangeT();
        }

        if (goDialogBar.activeSelf == false)
        {
            // PlayerChar.transform.GetComponent<Mins>().isDialogOnChangeF();
        }
    }

    public void OnButtonSetterT()
    {
        isSelectButton = true;
    }
    public void OnButtonSetterF()
    {
        isSelectButton = false;
    }
}
