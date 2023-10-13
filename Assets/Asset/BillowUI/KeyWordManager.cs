using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyWordManager : MonoBehaviour
{
    private static KeyWordManager instance = null;
    public static KeyWordManager Instance
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
    void Awake()
    {


        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void endterKey()
    {
        DataBaseManager.ConntectMouse = true;
    }
    public void outKey()
    {

        DataBaseManager.ConntectMouse = false;
    }


    public Text mainSetting;
    public Text upperSetting;
    public Text downerSetting;
    public GameObject DialogWindow;
    public void MainRight()
    {
        if (DataBaseManager.isActiveDialog1 == false  && DataBaseManager.isActiveDialog2 == false)
        {
            if (DataBaseManager.keyword_main == "Event")
            {
                DataBaseManager.keyword_main = "Character";
                DataBaseManager.keyword_upper = upperCharacterKewordList[nowCharacterUpperKeywordNum];
            }
            else if (DataBaseManager.keyword_main == "Character")
            {
                DataBaseManager.keyword_main = "Place";
                DataBaseManager.keyword_upper = upperPlaceKeywordList[nowPlaceUpperKeywordNum];
            }
            else if (DataBaseManager.keyword_main == "Place")
            {
                if(upperEventKeywordList.Count != 0)
                    {
                        DataBaseManager.keyword_main = "Event";
                        DataBaseManager.keyword_upper = upperEventKeywordList[nowEventUpperKeywordNum];
                    }
                else
                {
                    DataBaseManager.keyword_main = "Character";
                    DataBaseManager.keyword_upper = upperCharacterKewordList[nowCharacterUpperKeywordNum];

                }
       

            }
        }

    }
    public void MainLeft()
    {
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
        {
            if (DataBaseManager.keyword_main == "Event")
            {
                DataBaseManager.keyword_main = "Place";
                DataBaseManager.keyword_upper = upperPlaceKeywordList[nowPlaceUpperKeywordNum];
            }
            else if (DataBaseManager.keyword_main == "Character")
            {
                if (upperEventKeywordList.Count != 0)
                {
                    DataBaseManager.keyword_main = "Event";
                    DataBaseManager.keyword_upper = upperEventKeywordList[nowEventUpperKeywordNum];


                }
                else
                {
                    DataBaseManager.keyword_main = "Place";
                    DataBaseManager.keyword_upper = upperPlaceKeywordList[nowPlaceUpperKeywordNum];
                }
            }
            else if (DataBaseManager.keyword_main == "Place")
            {
                DataBaseManager.keyword_main = "Character";
                DataBaseManager.keyword_upper = upperCharacterKewordList[nowCharacterUpperKeywordNum];
            }

        }

    }

    public void UpperRight()
    {

        if (DataBaseManager.keyword_main == "Event")
        {

            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                nowEventUpperKeywordNum += 1;
                if (upperEventKeywordCount < nowEventUpperKeywordNum)
                {
                    nowEventUpperKeywordNum = 0;
                }

                DataBaseManager.keyword_upper = upperEventKeywordList[nowEventUpperKeywordNum];


            }

        }

        else if (DataBaseManager.keyword_main == "Place")
        {

            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                nowPlaceUpperKeywordNum += 1;
                if (upperPlaceKeywordCount < nowPlaceUpperKeywordNum)
                {
                    nowPlaceUpperKeywordNum = 0;
                }


                DataBaseManager.keyword_upper = upperPlaceKeywordList[nowPlaceUpperKeywordNum];

            }

        }
        else if (DataBaseManager.keyword_main == "Character")
        {

            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                nowCharacterUpperKeywordNum += 1;
                if (upperCharacterKeywordCount < nowCharacterUpperKeywordNum)
                {
                    nowCharacterUpperKeywordNum = 0;
                }


                DataBaseManager.keyword_upper = upperCharacterKewordList[nowCharacterUpperKeywordNum];

            }

        }
    }
    public void UpperLeft()
    {
        if (DataBaseManager.keyword_main == "Event")
        {
            nowEventUpperKeywordNum -= 1;
            if (0 > nowEventUpperKeywordNum)
            {
                nowEventUpperKeywordNum = upperEventKeywordCount;
            }


            DataBaseManager.keyword_upper = upperEventKeywordList[nowEventUpperKeywordNum];



        }
        else if (DataBaseManager.keyword_main == "Place")
        {

            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                nowPlaceUpperKeywordNum -= 1;
                if (0 > nowPlaceUpperKeywordNum)
                {
                    nowPlaceUpperKeywordNum = upperPlaceKeywordCount;
                }


                DataBaseManager.keyword_upper = upperPlaceKeywordList[nowPlaceUpperKeywordNum];

            }


        }
        else if (DataBaseManager.keyword_main == "Character")
        {

            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                nowCharacterUpperKeywordNum -= 1;
                if (0 > nowCharacterUpperKeywordNum)
                {
                    nowCharacterUpperKeywordNum = upperCharacterKeywordCount;
                }


                DataBaseManager.keyword_upper = upperCharacterKewordList[nowCharacterUpperKeywordNum];

            }


        }
    }






    public void DownerRight()
    {


        if (DataBaseManager.keyword_upper == "Insomnia")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (InsomniaKeywordCount <= InsomniaKeywordNum)
                {
                    InsomniaKeywordNum = 0;
                }
                else
                {
                    InsomniaKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Nightmare")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (NightmareKeywordCount <= NightmareKeywordNum)
                {
                    NightmareKeywordNum = 0;
                }
                else
                {
                    NightmareKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Paranoia")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (ParanoiaKeywordCount <= ParanoiaKeywordNum)
                {
                    ParanoiaKeywordNum = 0;
                }
                else
                {
                    ParanoiaKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "SewerGhostStory")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (SewerGhostStoryKeywordCount <= SewerGhostStoryKeywordNum)
                {
                    SewerGhostStoryKeywordNum = 0;
                }
                else
                {
                    SewerGhostStoryKeywordNum += 1;
                }
            }
        }
        
        // 인물
        if (DataBaseManager.keyword_upper == "Aiden Triss")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (AidenKeywordCount <= AidenKeywordNum)
                {
                    AidenKeywordNum = 0;
                }
                else
                {
                    AidenKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Ella Triss")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (EllaKeywordCount <= EllaKeywordNum)
                {
                    EllaKeywordNum = 0;
                }
                else
                {
                    EllaKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Maeve A Rossi")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (MeivKeywordCount <= MeivKeywordNum)
                {
                    MeivKeywordNum = 0;
                }
                else
                {
                    MeivKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Albert Bradley")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (AlbertKeywordCount <= AlbertKeywordNum)
                {
                    AlbertKeywordNum = 0;
                }
                else
                {
                    AlbertKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "SewerWorker")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (SewerWorkerKeywordCount <= SewerWorkerKeywordNum)
                {
                    SewerWorkerKeywordNum = 0;
                }
                else
                {
                    SewerWorkerKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Dave")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (DaveKeywordCount <= DaveKeywordNum)
                {
                    DaveKeywordNum = 0;
                }
                else
                {
                    DaveKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "MissingPeople")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (MissingPeopleKeywordCount <= MissingPeopleKeywordNum)
                {
                    MissingPeopleKeywordNum = 0;
                }
                else
                {
                    MissingPeopleKeywordNum += 1;
                }
            }
        }

        if (DataBaseManager.keyword_upper == "Wharf")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (WharfKeywordCount <= WharfKeywordNum)
                {
                    WharfKeywordNum = 0;
                }
                else
                {
                    WharfKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Sewage MA Office")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (SewageMaintenanceOfficeKeywordCount <= SewageMaintenanceOfficeKeywordNum)
                {
                    SewageMaintenanceOfficeKeywordNum = 0;
                }
                else
                {
                    SewageMaintenanceOfficeKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Sewer")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (SewerKeywordCount <= SewerKeywordNum)
                {
                    SewerKeywordNum = 0;
                }
                else
                {
                    SewerKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Client'shouse")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (QuestHouseKeywordCount <= QuestHouseKeywordNum)
                {
                    QuestHouseKeywordNum = 0;
                }
                else
                {
                    QuestHouseKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Univ of Miscatonic")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (UniversityKeywordCount<= UniversityKeywordNum)
                {
                    UniversityKeywordNum = 0;
                }
                else
                {
                    UniversityKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "DailyNews")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (NewspaperKeywordCount <= NewspaperKeywordNum)
                {
                    NewspaperKeywordNum = 0;
                }
                else
                {
                    NewspaperKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Safe")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (SafeKeywordCount <= SafeKeywordNum)
                {
                    SafeKeywordNum = 0;
                }
                else
                {
                    SafeKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "RiverWaterQuality")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (RiverWaterQualityKeywordCount <= RiverWaterQualityKeywordNum)
                {
                    RiverWaterQualityKeywordNum = 0;
                }
                else
                {
                    RiverWaterQualityKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "FishySmell")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (FishySmellKeywordCount <= FishySmellKeywordNum)
                {
                    FishySmellKeywordNum = 0;
                }
                else
                {
                    FishySmellKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "MurderCase")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (MurderCaseKeywordCount <= MurderCaseKeywordNum)
                {
                    MurderCaseKeywordNum = 0;
                }
                else
                {
                    MurderCaseKeywordNum += 1;
                }
            }
        }
    }
    public void DownerLeft()
    {
        if (DataBaseManager.keyword_upper == "Insomnia")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if ( InsomniaKeywordNum<=0)
                {
                    InsomniaKeywordNum = InsomniaKeywordCount;
                }
                else
                {
                    InsomniaKeywordNum -= 1;
                }   

            }
        }
        if (DataBaseManager.keyword_upper == "Nightmare")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (NightmareKeywordNum <= 0)
                {
                    NightmareKeywordNum = NightmareKeywordCount;
                }
                else
                {
                    NightmareKeywordNum -= 1;
                }

            }
        }
        if (DataBaseManager.keyword_upper == "Paranoia")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (ParanoiaKeywordNum <= 0)
                {
                    ParanoiaKeywordNum = ParanoiaKeywordCount;
                }
                else
                {
                    ParanoiaKeywordNum -= 1;
                }

            }
        }
        if (DataBaseManager.keyword_upper == "SewerGhostStory")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (SewerGhostStoryKeywordNum <= 0)
                {
                    SewerGhostStoryKeywordNum = SewerGhostStoryKeywordCount;
                }
                else
                {
                    SewerGhostStoryKeywordNum -= 1;
                }

            }
        }
        
        if (DataBaseManager.keyword_upper == "Aiden Triss")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (AidenKeywordNum <= 0)
                {
                    AidenKeywordNum = AidenKeywordCount;
                }
                else
                {
                    AidenKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Ella Triss")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (EllaKeywordNum <= 0)
                {
                    EllaKeywordNum = EllaKeywordCount;
                }
                else
                {
                    EllaKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Maeve A Rossi")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (MeivKeywordNum <= 0)
                {
                    MeivKeywordNum = MeivKeywordCount;
                }
                else
                {
                    MeivKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Albert Bradley")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (AlbertKeywordNum <= 0)
                {
                    AlbertKeywordNum = AlbertKeywordCount;
                }
                else
                {
                    AlbertKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "SewerWorker")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (SewerWorkerKeywordNum <= 0)
                {
                    SewerWorkerKeywordNum = SewerWorkerKeywordCount;
                }
                else
                {
                    SewerWorkerKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Dave")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (DaveKeywordNum <= 0)
                {
                    DaveKeywordNum = DaveKeywordCount;
                }
                else
                {
                    DaveKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "MissingPeople")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (MissingPeopleKeywordNum <= 0)
                {
                    MissingPeopleKeywordNum = MissingPeopleKeywordCount;
                }
                else
                {
                    MissingPeopleKeywordNum -= 1;
                }

            }
        }
        if (DataBaseManager.keyword_upper == "Wharf")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (WharfKeywordNum <= 0)
                {
                    WharfKeywordNum = WharfKeywordCount;
                }
                else
                {
                    WharfKeywordNum -= 1;
                }

            }
        }
        if (DataBaseManager.keyword_upper == "DailyNews")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (NewspaperKeywordNum <= 0)
                {
                    NewspaperKeywordNum = NewspaperKeywordCount;
                }
                else
                {
                    NewspaperKeywordNum -= 1;
                }

            }
        }
        if (DataBaseManager.keyword_upper == "Client'shouse")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (QuestHouseKeywordNum <= 0)
                {
                    QuestHouseKeywordNum = QuestHouseKeywordCount;
                }
                else
                {
                    QuestHouseKeywordNum -= 1;
                }

            }
        }
        if (DataBaseManager.keyword_upper == "Univ of Miscatonic")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (UniversityKeywordNum <= 0)
                {
                    UniversityKeywordNum = UniversityKeywordCount;
                }
                else
                {
                    UniversityKeywordNum -= 1;
                }

            }
        }
        
        if (DataBaseManager.keyword_upper == "Safe")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (SafeKeywordNum <= 0)
                {
                    SafeKeywordNum = SafeKeywordCount;
                }
                else
                {
                    SafeKeywordNum -= 1;
                }

            }
        }
        if (DataBaseManager.keyword_upper == "Sewage MA Office")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (SewageMaintenanceOfficeKeywordNum <= 0)
                {
                    SewageMaintenanceOfficeKeywordNum = SewageMaintenanceOfficeKeywordCount;
                }
                else
                {
                    SewageMaintenanceOfficeKeywordNum -= 1;
                }

            }
        }
        if (DataBaseManager.keyword_upper == "Sewer")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (SewerKeywordNum <= 0)
                {
                    SewerKeywordNum = SewerKeywordCount;
                }
                else
                {
                    SewerKeywordNum -= 1;
                }

            }
        }
        if (DataBaseManager.keyword_upper == "RiverWaterQuality")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (RiverWaterQualityKeywordNum <= 0)
                {
                    RiverWaterQualityKeywordNum = RiverWaterQualityKeywordCount;
                }
                else
                {
                    RiverWaterQualityKeywordNum -= 1;
                }

            }
        }
        if (DataBaseManager.keyword_upper == "FishySmell")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (FishySmellKeywordNum <= 0)
                {
                    FishySmellKeywordNum = FishySmellKeywordCount;
                }
                else
                {
                    FishySmellKeywordNum -= 1;
                }

            }
        }
        if (DataBaseManager.keyword_upper == "MurderCase")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (MurderCaseKeywordNum <= 0)
                {
                    MurderCaseKeywordNum = MurderCaseKeywordCount;
                }
                else
                {
                    MurderCaseKeywordNum -= 1;
                }

            }
        }
    }


   public List<string> upperEventKeywordList = new List<string>();//string들어가야하고
     int upperEventKeywordCount = 0;
     int nowEventUpperKeywordNum = 0;

    public List<string> upperCharacterKewordList = new List<string>();//string들어가야하고
     int upperCharacterKeywordCount = 0;
     int nowCharacterUpperKeywordNum = 0;

    public List<string> upperPlaceKeywordList = new List<string>();//string들어가야하고
     int upperPlaceKeywordCount = 0;
     int nowPlaceUpperKeywordNum = 0;

    //사건
    bool isPlanetaryParadeAddList = false;
    bool isNightmareAddList = false;
    bool isInsomniaAddList = false;
    bool isParanoiaAddList = false;
    bool isMissingPeopleAddList = false;
    bool isSewerGhostStoryAddList = false;
    bool isFishySmellAddList = false;
    bool isRiverWaterQualityAddList = false;
    bool isCreepyEyesAddList = false;
    bool isMurderCaseAddList = false;
    bool isSafeAddList = false;

    //장소
    bool isHospitalAddList = false;
    bool isQuestHouseAddList = false;
    bool isNewspaperAddList = false;
    bool isRiversideAddList = false;
    bool isSewageMaintenanceOfficeAddList = false;
    bool isSewerAddList = false;
    bool isWharfAddList = false;
    bool isUniversityAddList = false;
    //인물
    bool isAidenAddList = false;
    bool isEllaAddList = false;
    bool isSwainAddList = false;
    bool isCaneAddList = false;
    bool isMeivAddList = false;
    bool isAlberAddList = false;
    bool isSewerWorkerAddList = false;
    bool isDaveAddList = false;


    public void KeywordUpdate()
    {


        mainSetting.text =  DataBaseManager.keyword_main ;
        upperSetting.text =  DataBaseManager.keyword_upper ;
        downerSetting.text =  DataBaseManager.keyword_downer ;

        // 상위키워드 업데이트

        upperEventKeywordCount = upperEventKeywordList.Count-1;
        upperCharacterKeywordCount = upperCharacterKewordList.Count - 1;
        upperPlaceKeywordCount = upperPlaceKeywordList.Count - 1;



        // 사건
        if (isPlanetaryParadeAddList == false)
        {
            if (DataBaseManager.Intel_PlanetaryParade1 == true || DataBaseManager.Intel_PlanetaryParade2 == true || DataBaseManager.Intel_PlanetaryParade3 == true || DataBaseManager.Intel_PlanetaryParade4 == true || DataBaseManager.Intel_PlanetaryParade5 == true || DataBaseManager.Intel_PlanetaryParade6 == true)
            {
                isPlanetaryParadeAddList = true;
                upperEventKeywordList.Add("PlanetaryParade");
            }
        }
        if(isNightmareAddList == false)
        {
            if (DataBaseManager.Intel_Nightmare1 == true || DataBaseManager.Intel_Nightmare2 == true || DataBaseManager.Intel_Nightmare3 == true || DataBaseManager.Intel_Nightmare4 == true || DataBaseManager.Intel_Nightmare5 == true || DataBaseManager.Intel_Nightmare6 == true)
            {
                isNightmareAddList = true;
                upperEventKeywordList.Add("Nightmare");
            }
        }
        if (isInsomniaAddList == false)
        {
            if (DataBaseManager.Intel_Insomnia1 == true || DataBaseManager.Intel_Insomnia2 == true || DataBaseManager.Intel_Insomnia3 == true || DataBaseManager.Intel_Insomnia4 == true || DataBaseManager.Intel_Insomnia5 == true || DataBaseManager.Intel_Insomnia6 == true)
            {
                isInsomniaAddList = true;
                upperEventKeywordList.Add("Insomnia");
            }
        }
        if (isParanoiaAddList == false)
        {
            if (DataBaseManager.Intel_Paranoia1 == true || DataBaseManager.Intel_Paranoia2 == true || DataBaseManager.Intel_Paranoia3 == true || DataBaseManager.Intel_Paranoia4 == true || DataBaseManager.Intel_Paranoia5 == true || DataBaseManager.Intel_Paranoia6 == true)
            {
                isParanoiaAddList = true;
                upperEventKeywordList.Add("Paranoia");
            }
        }
        if (isMissingPeopleAddList == false)
        {
            if (DataBaseManager.Intel_MissingPeople1 == true || DataBaseManager.Intel_MissingPeople2 == true || DataBaseManager.Intel_MissingPeople3 == true || DataBaseManager.Intel_MissingPeople4 == true || DataBaseManager.Intel_MissingPeople5 == true || DataBaseManager.Intel_MissingPeople6 == true)
            {
                isMissingPeopleAddList = true;
                upperEventKeywordList.Add("MissingPeople");
            }
        }
        if (isSewerGhostStoryAddList == false)
        {
            if (DataBaseManager.Intel_SewerGhostStory1 == true || DataBaseManager.Intel_SewerGhostStory2 == true || DataBaseManager.Intel_SewerGhostStory3 == true || DataBaseManager.Intel_SewerGhostStory4 == true || DataBaseManager.Intel_SewerGhostStory5 == true || DataBaseManager.Intel_SewerGhostStory6 == true)
            {
                isSewerGhostStoryAddList = true;
                upperEventKeywordList.Add("SewerGhostStory");
            }
        }
        if (isFishySmellAddList == false)
        {
            if (DataBaseManager.Intel_FishySmell1 == true || DataBaseManager.Intel_FishySmell2 == true || DataBaseManager.Intel_FishySmell3 == true || DataBaseManager.Intel_FishySmell4 == true || DataBaseManager.Intel_FishySmell5 == true || DataBaseManager.Intel_FishySmell6 == true)
            {
                isFishySmellAddList = true;
                upperEventKeywordList.Add("FishySmell");
            }
        }
        if (isRiverWaterQualityAddList == false)
        {
            if (DataBaseManager.Intel_RiverWaterQuality1 == true || DataBaseManager.Intel_RiverWaterQuality2 == true || DataBaseManager.Intel_RiverWaterQuality3 == true || DataBaseManager.Intel_RiverWaterQuality4 == true || DataBaseManager.Intel_RiverWaterQuality5 == true || DataBaseManager.Intel_RiverWaterQuality6 == true)
            {
                isRiverWaterQualityAddList = true;
                upperEventKeywordList.Add("RiverWaterQuality");
            }
        }
        if (isCreepyEyesAddList == false)
        {
            if (DataBaseManager.Intel_CreepyEyes1 == true || DataBaseManager.Intel_CreepyEyes2 == true || DataBaseManager.Intel_CreepyEyes3 == true || DataBaseManager.Intel_CreepyEyes4 == true || DataBaseManager.Intel_CreepyEyes5 == true || DataBaseManager.Intel_CreepyEyes6 == true)
            {
                isCreepyEyesAddList = true;
                upperEventKeywordList.Add("CreepyEyes");
            }
        }
        if (isMurderCaseAddList == false)
        {
            if (DataBaseManager.Intel_MurderCase1 == true || DataBaseManager.Intel_MurderCase2 == true || DataBaseManager.Intel_MurderCase3 == true || DataBaseManager.Intel_MurderCase4 == true || DataBaseManager.Intel_MurderCase5 == true || DataBaseManager.Intel_MurderCase6 == true)
            {
                isMurderCaseAddList = true;
                upperEventKeywordList.Add("MurderCase");
            }
        }

        if (isSafeAddList == false)
        {
            if (DataBaseManager.Intel_Safe1 == true || DataBaseManager.Intel_Safe2 == true || DataBaseManager.Intel_Safe3 == true || DataBaseManager.Intel_Safe4 == true || DataBaseManager.Intel_Safe5 == true || DataBaseManager.Intel_Safe6 == true)
            {
      
                isSafeAddList = true;
                upperEventKeywordList.Add("Safe");
                Debug.Log(upperEventKeywordList);
            }
        }
        /*

        */
        //장소
        if (isHospitalAddList == false)
        {
            if (DataBaseManager.Intel_Hospital1 == true || DataBaseManager.Intel_Hospital2 == true || DataBaseManager.Intel_Hospital3 == true || DataBaseManager.Intel_Hospital4 == true || DataBaseManager.Intel_Hospital5 == true || DataBaseManager.Intel_Hospital6 == true)
            {
                isHospitalAddList = true;
                upperPlaceKeywordList.Add("Hospital");
            }
        }
        if (isQuestHouseAddList == false)
        {
            if (DataBaseManager.Intel_QuestHouse1 == true || DataBaseManager.Intel_QuestHouse2 == true || DataBaseManager.Intel_QuestHouse3 == true || DataBaseManager.Intel_QuestHouse4 == true || DataBaseManager.Intel_QuestHouse5 == true || DataBaseManager.Intel_QuestHouse6 == true)
            {
                isQuestHouseAddList = true;
                upperPlaceKeywordList.Add("Client'shouse");
            }
        }
        if (isNewspaperAddList == false)
        {
            if (DataBaseManager.Intel_Newspaper1 == true || DataBaseManager.Intel_Newspaper2 == true || DataBaseManager.Intel_Newspaper3 == true || DataBaseManager.Intel_Newspaper4 == true || DataBaseManager.Intel_Newspaper5 == true || DataBaseManager.Intel_Newspaper6 == true)
            {
                isNewspaperAddList = true;
                upperPlaceKeywordList.Add("DailyNews");
            }
        }
        if (isRiversideAddList == false)
        {
            if (DataBaseManager.Intel_Riverside1 == true || DataBaseManager.Intel_Riverside2 == true || DataBaseManager.Intel_Riverside3 == true || DataBaseManager.Intel_Riverside4 == true || DataBaseManager.Intel_Riverside5 == true || DataBaseManager.Intel_Riverside6 == true)
            {
                isRiversideAddList = true;
                upperPlaceKeywordList.Add("Riverside");
            }
        }
        if (isSewageMaintenanceOfficeAddList == false)
        {
            if (DataBaseManager.Intel_SewageMaintenanceOffice1 == true || DataBaseManager.Intel_SewageMaintenanceOffice2 == true || DataBaseManager.Intel_SewageMaintenanceOffice3 == true || DataBaseManager.Intel_SewageMaintenanceOffice4 == true || DataBaseManager.Intel_SewageMaintenanceOffice5 == true || DataBaseManager.Intel_SewageMaintenanceOffice6 == true)
            {
                isSewageMaintenanceOfficeAddList = true;
                upperPlaceKeywordList.Add("Sewage MA Office");
            }
        }
        if (isSewerAddList == false)
        {
            if (DataBaseManager.Intel_Sewer1 == true || DataBaseManager.Intel_Sewer2 == true || DataBaseManager.Intel_Sewer3 == true || DataBaseManager.Intel_Sewer4 == true || DataBaseManager.Intel_Sewer5 == true || DataBaseManager.Intel_Sewer6 == true)
            {
                isSewerAddList = true;
                upperPlaceKeywordList.Add("Sewer");
            }
        }
        if (isWharfAddList == false)
        {
            if (DataBaseManager.Intel_Wharf1 == true || DataBaseManager.Intel_Wharf2 == true || DataBaseManager.Intel_Wharf3 == true || DataBaseManager.Intel_Wharf4 == true || DataBaseManager.Intel_Wharf5 == true || DataBaseManager.Intel_Wharf6 == true)
            {
                isWharfAddList = true;
                upperPlaceKeywordList.Add("Wharf");
            }
        }
        if (isUniversityAddList == false)
        {
            if (DataBaseManager.Intel_University1 == true || DataBaseManager.Intel_University2 == true || DataBaseManager.Intel_University3 == true || DataBaseManager.Intel_University4 == true || DataBaseManager.Intel_University5 == true || DataBaseManager.Intel_University6 == true)
            {
                isUniversityAddList = true;
                upperPlaceKeywordList.Add("Univ of Miscatonic");
            }
        }

        //인물
        if (isAidenAddList == false)
        {
            if (DataBaseManager.Intel_Aiden1 == true || DataBaseManager.Intel_Aiden2 == true || DataBaseManager.Intel_Aiden3 == true || DataBaseManager.Intel_Aiden4 == true || DataBaseManager.Intel_Aiden5 == true || DataBaseManager.Intel_Aiden6 == true)
            {
                isAidenAddList = true;
                upperCharacterKewordList.Add("Aiden Triss");
            }
        }

        if (isEllaAddList == false)
        {
            if (DataBaseManager.Intel_Ella1 == true || DataBaseManager.Intel_Ella2 == true || DataBaseManager.Intel_Ella3 == true || DataBaseManager.Intel_Ella4 == true || DataBaseManager.Intel_Ella5 == true || DataBaseManager.Intel_Ella6 == true)
            {
                isEllaAddList = true;
                upperCharacterKewordList.Add("Ella Triss");
            }
        }
        if (isSwainAddList == false)
        {
            if (DataBaseManager.Intel_Swain1 == true || DataBaseManager.Intel_Swain2 == true || DataBaseManager.Intel_Swain3 == true || DataBaseManager.Intel_Swain4 == true || DataBaseManager.Intel_Swain5 == true || DataBaseManager.Intel_Swain6 == true)
            {
                isSwainAddList = true;
                upperCharacterKewordList.Add("Berkeley Swain");
            }
        }
        if (isCaneAddList == false)
        {
            if (DataBaseManager.Intel_Cane1 == true || DataBaseManager.Intel_Cane2 == true || DataBaseManager.Intel_Cane3 == true || DataBaseManager.Intel_Cane5 == true || DataBaseManager.Intel_Cane4 == true || DataBaseManager.Intel_Cane6 == true)
            {
                isCaneAddList = true;
                upperCharacterKewordList.Add("Osborn Louis Kane");
            }
        }
        if ( isMeivAddList == false)
        {
            if (DataBaseManager.Intel_Meiv1 == true || DataBaseManager.Intel_Meiv2 == true || DataBaseManager.Intel_Meiv3 == true || DataBaseManager.Intel_Meiv4 == true || DataBaseManager.Intel_Meiv5 == true || DataBaseManager.Intel_Meiv6 == true)
            {
                isMeivAddList = true;
                upperCharacterKewordList.Add("Maeve A Rossi");
            }
        }
        if (isAlberAddList == false)
        {
            if (DataBaseManager.Intel_Albert1 == true || DataBaseManager.Intel_Albert2 == true || DataBaseManager.Intel_Albert3 == true || DataBaseManager.Intel_Albert4 == true || DataBaseManager.Intel_Albert5 == true || DataBaseManager.Intel_Albert6 == true)
            {
                isAlberAddList = true;
                upperCharacterKewordList.Add("Albert Bradley");
            }
        }
        if (isSewerWorkerAddList == false)
        {
            if (DataBaseManager.Intel_SewerWorker1 == true || DataBaseManager.Intel_SewerWorker2 == true || DataBaseManager.Intel_SewerWorker3 == true || DataBaseManager.Intel_SewerWorker4 == true || DataBaseManager.Intel_SewerWorker5 == true || DataBaseManager.Intel_SewerWorker6 == true)
            {
                isSewerWorkerAddList = true;
                upperCharacterKewordList.Add("SewerWorker");
            }
        }
        if (isDaveAddList == false)
        {
            if (DataBaseManager.Intel_Dave1 == true || DataBaseManager.Intel_Dave2 == true || DataBaseManager.Intel_Dave3 == true || DataBaseManager.Intel_Dave4 == true || DataBaseManager.Intel_Dave5 == true || DataBaseManager.Intel_Dave6 == true)
            {
                isDaveAddList = true;
                upperCharacterKewordList.Add("Dave");
            }
        }
    }
    // ------------------------------- 사건하위
    public List<string> PlanetaryParadeList = new List<string>();//string들어가야하고
    int PlanetaryParadeKeywordCount = 0;
    public int PlanetaryParadeKeywordNum = 0;

    public List<string> NightmareList = new List<string>();//string들어가야하고
    int NightmareKeywordCount = 0;
    public int NightmareKeywordNum = 0;

    public List<string> InsomniaList = new List<string>();//string들어가야하고
    int InsomniaKeywordCount;
    public int InsomniaKeywordNum = 0;

    public List<string> ParanoiaList = new List<string>();//string들어가야하고
    int ParanoiaKeywordCount;
    public int ParanoiaKeywordNum = 0;

    public List<string> MissingPeopleList = new List<string>();//string들어가야하고
    int MissingPeopleKeywordCount = 0;
    public int MissingPeopleKeywordNum = 0;

    public List<string> SewerGhostStoryList = new List<string>();//string들어가야하고
    int SewerGhostStoryKeywordCount = 0;
    public int SewerGhostStoryKeywordNum = 0;

    public List<string> FishySmellList = new List<string>();//string들어가야하고
    int FishySmellKeywordCount = 0;
    public int FishySmellKeywordNum = 0;

    public List<string> RiverWaterQualityList = new List<string>();//string들어가야하고
    int RiverWaterQualityKeywordCount = 0;
    public int RiverWaterQualityKeywordNum = 0;

    public List<string> CreepyEyesList = new List<string>();//string들어가야하고
    int CreepyEyesKeywordCount = 0;
    public int CreepyEyesKeywordNum = 0;

    public List<string> MurderCaseList = new List<string>();//string들어가야하고
    int MurderCaseKeywordCount = 0;
    public int MurderCaseKeywordNum = 0;

    public List<string> SafeList = new List<string>();//string들어가야하고
    int SafeKeywordCount = 0;
    public int SafeKeywordNum = 0;
    //사건
    bool isPlanetaryParade1Add = false;
    bool isPlanetaryParade2Add = false;
    bool isNightmare1Add = false;
    bool isNightmare2Add = false;
    bool isNightmare3Add = false;
    bool isNightmare4Add = false;
    bool isInsomnia1Add = false;
    bool isInsomnia2Add = false;
    bool isInsomnia3Add = false;
    bool isInsomnia4Add = false;
    bool isParanoia1Add = false;
    bool isParanoia2Add = false;
    bool isParanoia3Add = false;
    bool isMissingPeople1Add = false;
    bool isMissingPeople2Add = false;
    bool isMissingPeople3Add = false;
    bool isSewerGhostStory1Add = false;
    bool isSewerGhostStory2Add = false;
    bool isSewerGhostStory3Add = false;
    bool isFishySmell1Add = false;
    bool isFishySmell2Add = false;
    bool isFishySmell3Add = false;
    bool isFishySmell4Add = false;
    bool isRiverWaterQuality1Add = false;
    bool isRiverWaterQuality2Add = false;
    bool isRiverWaterQuality3Add = false;
    bool isCreepyEyes1Add = false;
    bool isMurderCase1Add = false;
    bool isMurderCase2Add = false;
    bool isMurderCase3Add = false;
    bool isMurderCase4Add = false;
    bool isMurderCase5Add = false;
    bool isMurderCase6Add = false;
    bool isSafe1Add = false;
    bool isSafe2Add = false;
    // ------------------------------- 인물하위
    public List<string> AidenList = new List<string>();//string들어가야하고
    int AidenKeywordCount = 0;
    public int AidenKeywordNum = 0;

    public List<string> EllaList = new List<string>();//string들어가야하고
    int EllaKeywordCount = 0;
    public int EllaKeywordNum = 0;

    public List<string> SwainList = new List<string>();//string들어가야하고
    int SwainKeywordCount = 0;
    public int SwainKeywordNum = 0;

    public List<string> CaneList = new List<string>();//string들어가야하고
    int CaneKeywordCount = 0;
    public int CaneKeywordNum = 0;

    public List<string> AlbertList = new List<string>();//string들어가야하고
    int AlbertKeywordCount = 0;
    public int AlbertKeywordNum = 0;

    public List<string> MeivList = new List<string>();//string들어가야하고
    int MeivKeywordCount = 0;
    public int MeivKeywordNum = 0;

    public List<string> SewerWorkerList = new List<string>();//string들어가야하고
    int SewerWorkerKeywordCount = 0;
    public int SewerWorkerKeywordNum = 0;

    public List<string> DaveList = new List<string>();//string들어가야하고
    int DaveKeywordCount = 0;
    public int DaveKeywordNum = 0;
    //사건
    bool isAiden1Add = false;
    bool isAiden2Add = false;
    bool isAiden3Add = false;
    bool isAiden4Add = false;
    bool isElla1Add = false;
    bool isElla2Add = false;
    bool isSwain1Add = false;
    bool isCane1Add = false;
    bool isMeiv1Add = false;
    bool isMeiv2Add = false;
    bool isAlbert1Add = false;
    bool isSewerWorker1Add = false;
    bool isSewerWorker2Add = false;
    bool isSewerWorker3Add = false;
    bool isSewerWorker4Add = false;
    bool isDave1Add = false;
    // ------------------------------- 장소하위
    public List<string> HospitalList = new List<string>();//string들어가야하고
    int HospitalKeywordCount = 0;
    public int HospitalKeywordNum = 0;

    public List<string> NewspaperList = new List<string>();//string들어가야하고
    int NewspaperKeywordCount = 0;
    public int NewspaperKeywordNum = 0;

    public List<string> QuestHouseList = new List<string>();//string들어가야하고
    int QuestHouseKeywordCount = 0;
    public int QuestHouseKeywordNum = 0;

    public List<string> RiversideList = new List<string>();//string들어가야하고
    int RiversideKeywordCount = 0;
    public int RiversideKeywordNum = 0;

    public List<string> SewageMaintenanceOfficeList = new List<string>();//string들어가야하고
    int SewageMaintenanceOfficeKeywordCount = 0;
    public int SewageMaintenanceOfficeKeywordNum = 0;

    public List<string> SewerList = new List<string>();//string들어가야하고
    int SewerKeywordCount = 0;
    public int SewerKeywordNum = 0;


    public List<string> WharfList = new List<string>();//string들어가야하고
    int WharfKeywordCount = 0;
    public int WharfKeywordNum = 0;

    public List<string> UniversityList = new List<string>();//string들어가야하고
    int UniversityKeywordCount = 0;
    public int UniversityKeywordNum = 0;
    //사건
    bool isSewer1Add = false;
    bool isSewer2Add = false;
    bool isSewer3Add = false;
    bool isHospital1Add = false;
    bool isNewspaper1Add = false;
    bool isQuestHouse1Add = false;
    bool isRiverside1Add = false;
    bool isSewageMaintenanceOffice1Add = false;
    bool isWharf1Add = false;
    bool isUniversity1Add = false;
    public void downerKeywrodUpdater()
    {        

        PlanetaryParadeKeywordCount = PlanetaryParadeList.Count-1;
        NightmareKeywordCount = NightmareList.Count - 1;
        InsomniaKeywordCount = InsomniaList.Count - 1;
        ParanoiaKeywordCount = ParanoiaList.Count - 1;
        MissingPeopleKeywordCount = MissingPeopleList.Count - 1;
        SewerGhostStoryKeywordCount = SewerGhostStoryList.Count - 1;
        FishySmellKeywordCount = FishySmellList.Count - 1;
        RiverWaterQualityKeywordCount = RiverWaterQualityList.Count - 1;
        CreepyEyesKeywordCount = CreepyEyesList.Count - 1;
        MurderCaseKeywordCount = MurderCaseList.Count - 1;
        SafeKeywordCount = SafeList.Count - 1;

        EllaKeywordCount = EllaList.Count - 1;
        AidenKeywordCount = AidenList.Count - 1;
        SwainKeywordCount = SwainList.Count - 1;
        CaneKeywordCount = CaneList.Count - 1;
        MeivKeywordCount = MeivList.Count - 1;
        AlbertKeywordCount = AlbertList.Count - 1;
        SewerWorkerKeywordCount = SewerWorkerList.Count - 1;

        HospitalKeywordCount = HospitalList.Count - 1;
        QuestHouseKeywordCount = QuestHouseList.Count - 1;
        NewspaperKeywordCount = NewspaperList.Count - 1;
        RiversideKeywordCount = RiversideList.Count - 1;
        SewageMaintenanceOfficeKeywordCount = SewageMaintenanceOfficeList.Count - 1;
        SewerKeywordCount = SewerList.Count - 1;
        WharfKeywordCount = WharfList.Count - 1;
        UniversityKeywordCount = UniversityList.Count - 1;

        // 사건 

        // 행성대직렬
        if (DataBaseManager.keyword_upper == "PlanetaryParade" && PlanetaryParadeKeywordCount>=0)
        {
            DataBaseManager.keyword_downer = PlanetaryParadeList[PlanetaryParadeKeywordNum];
        }
 
        if (DataBaseManager.Intel_PlanetaryParade1 == true&&isPlanetaryParade1Add == false)
        {
            isPlanetaryParade1Add = true;
            PlanetaryParadeList.Add("Contents of a Newspaper");
        }
        if (DataBaseManager.Intel_PlanetaryParade2 == true && isPlanetaryParade2Add == false)
        {
            isPlanetaryParade2Add = true;
            PlanetaryParadeList.Add("Meetings");
        }

        // 악몽
        else if (DataBaseManager.keyword_upper == "Nightmare" && NightmareKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = NightmareList[NightmareKeywordNum];
        }

        if (DataBaseManager.Intel_Nightmare1 == true && isNightmare1Add == false)
        {
            isNightmare1Add = true;
            NightmareList.Add("Aiden's Nightmare");
        }

        if (DataBaseManager.Intel_Nightmare2 == true && isNightmare2Add == false)
        {
            isNightmare2Add = true;
            NightmareList.Add("Content of a Nightmare");
        }
        if (DataBaseManager.Intel_Nightmare3 == true && isNightmare3Add == false)
        {
            isNightmare3Add = true;
            NightmareList.Add("Aiden's Detail");
        }
        if (DataBaseManager.Intel_Nightmare4 == true && isNightmare4Add == false)
        {
            isNightmare4Add = true;
            NightmareList.Add("Water and Nightmare");
        }
        // 불면증
        else if (DataBaseManager.keyword_upper == "Insomnia" && InsomniaKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = InsomniaList[InsomniaKeywordNum];
        }

        if (DataBaseManager.Intel_Insomnia1 == true && isInsomnia1Add == false)
        {
            isInsomnia1Add = true;
            InsomniaList.Add("Newspaper Article");
        }
        if (DataBaseManager.Intel_Insomnia2 == true && isInsomnia2Add == false)
        {
            isInsomnia2Add = true;
            InsomniaList.Add("Doctor's opinion");
        }
        if (DataBaseManager.Intel_Insomnia3 == true && isInsomnia3Add == false)
        {
            isInsomnia3Add = true;
           // InsomniaList.Add("Cause of Insomnia");
        }
        // 편집증
        else if (DataBaseManager.keyword_upper == "Paranoia" && ParanoiaKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = ParanoiaList[ParanoiaKeywordNum];
        }

        if (DataBaseManager.Intel_Paranoia1 == true && isParanoia1Add == false)
        {
            isParanoia1Add = true;
            ParanoiaList.Add("Husband's Condition");
        }
        if (DataBaseManager.Intel_Paranoia2 == true && isParanoia2Add == false)
        {
            isParanoia2Add = true;
            ParanoiaList.Add("Cause of Paranoia");
        }
        // 실종사건
        else if (DataBaseManager.keyword_upper == "MissingPeople" && MissingPeopleKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = MissingPeopleList[MissingPeopleKeywordNum];
        }
        if (DataBaseManager.Intel_MissingPeople1 == true && isMissingPeople1Add == false)
        {
            isMissingPeople1Add = true;
            MissingPeopleList.Add("Albert's Information");
        }
        if (DataBaseManager.Intel_MissingPeople2 == true && isMissingPeople2Add == false)
        {
            isMissingPeople2Add = true;
            MissingPeopleList.Add("Reporting by Swain");
        }
        if (DataBaseManager.Intel_MissingPeople3 == true && isMissingPeople3Add == false)
        {
            isMissingPeople3Add = true;
            MissingPeopleList.Add("Kidnap");
        }

        // 하수도괴담.
        else if (DataBaseManager.keyword_upper == "SewerGhostStory" && SewerGhostStoryKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = SewerGhostStoryList[SewerGhostStoryKeywordNum];
        }

        if (DataBaseManager.Intel_SewerGhostStory1 == true && isSewerGhostStory1Add == false)
        {
            isSewerGhostStory1Add = true;
            SewerGhostStoryList.Add("Contents of a GhostStory");
        }
        if (DataBaseManager.Intel_SewerGhostStory2 == true && isSewerGhostStory2Add == false)
        {
            isSewerGhostStory2Add = true;
            SewerGhostStoryList.Add("Truth of a rumor");
        }
        // 비릿한냄새
        else if (DataBaseManager.keyword_upper == "FishySmell" && FishySmellKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = FishySmellList[FishySmellKeywordNum];
        }

        if (DataBaseManager.Intel_FishySmell1 == true && isFishySmell1Add == false)
        {
            isFishySmell1Add = true;
            FishySmellList.Add("Rumor");
        }
        if (DataBaseManager.Intel_FishySmell2 == true && isFishySmell2Add == false)
        {
            isFishySmell2Add = true;
            FishySmellList.Add("Source of the smell?");
        }
        if (DataBaseManager.Intel_FishySmell3 == true && isFishySmell3Add == false)
        {
            isFishySmell3Add = true;
            FishySmellList.Add("Source speculation");
        }
        if (DataBaseManager.Intel_FishySmell4 == true && isFishySmell4Add == false)
        {
            isFishySmell4Add = true;
            FishySmellList.Add("Sewer & FishySmell");
        }
        // 강의수질
        else if (DataBaseManager.keyword_upper == "RiverWaterQuality" && RiverWaterQualityKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = RiverWaterQualityList[RiverWaterQualityKeywordNum];
        }

        if (DataBaseManager.Intel_RiverWaterQuality1 == true && isRiverWaterQuality1Add == false)
        {
            isRiverWaterQuality1Add = true;
            RiverWaterQualityList.Add("Strange Point");
        }
        if (DataBaseManager.Intel_RiverWaterQuality2 == true && isRiverWaterQuality2Add == false)
        {
            isRiverWaterQuality2Add = true;
            RiverWaterQualityList.Add("Details");
        }
        if (DataBaseManager.Intel_RiverWaterQuality3 == true && isRiverWaterQuality3Add == false)
        {
            isRiverWaterQuality3Add = true;
            RiverWaterQualityList.Add("Fishy Slime");
        }

        // 섬뜩한눈
        else if (DataBaseManager.keyword_upper == "CreepyEyes" && CreepyEyesKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = CreepyEyesList[CreepyEyesKeywordNum];
        }

        if (DataBaseManager.Intel_CreepyEyes1 == true && isCreepyEyes1Add == false)
        {
            isCreepyEyes1Add = true;
            CreepyEyesList.Add("Saw in the sewer");
        }

        // 살인사건
        else if (DataBaseManager.keyword_upper == "MurderCase" && MurderCaseKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = MurderCaseList[MurderCaseKeywordNum];
        }
        if (DataBaseManager.Intel_MurderCase1 == true && isMurderCase1Add == false)
        {
            isMurderCase1Add = true;
            MurderCaseList.Add("Witness Testimony");
        }
        if (DataBaseManager.Intel_MurderCase2 == true && isMurderCase2Add == false)
        {
            isMurderCase2Add = true;
            MurderCaseList.Add("Missing People & Fishy Smell");
        }
        if (DataBaseManager.Intel_MurderCase3 == true && isMurderCase3Add == false)
        {
            isMurderCase3Add = true;
            MurderCaseList.Add("The Culprit");
        }
        if (DataBaseManager.Intel_MurderCase4 == true && isMurderCase4Add == false)
        {
            isMurderCase4Add = true;
            MurderCaseList.Add("Cause of Death");
        }
        if (DataBaseManager.Intel_MurderCase5 == true && isMurderCase5Add == false)
        {
            isMurderCase5Add = true;
            MurderCaseList.Add("Webbed Feet");
        }
        if (DataBaseManager.Intel_MurderCase6 == true && isMurderCase6Add == false)
        {
            isMurderCase6Add = true;
            MurderCaseList.Add("Way to Escape");
        }




        // 금고
        else if (DataBaseManager.keyword_upper == "Safe" && SafeKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = SafeList[SafeKeywordNum];
        }
        if (DataBaseManager.Intel_Safe1 == true && isSafe1Add == false)
        {
            isSafe1Add = true;
            SafeList.Add("Aiden's Safe");
        }
        if (DataBaseManager.Intel_Safe2 == true && isSafe2Add == false)
        {
            isSafe2Add = true;
            SafeList.Add("Acquiring a Safe");
        }

        // 인물

        // 에이든
        if (DataBaseManager.keyword_upper == "Aiden Triss" && AidenKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = AidenList[AidenKeywordNum];
        }
        if (DataBaseManager.Intel_Aiden1 == true && isAiden1Add == false)
        {
            isAiden1Add = true;
            AidenList.Add("Subject to request");
        }
        if (DataBaseManager.Intel_Aiden2 == true && isAiden2Add == false)
        {
            isAiden2Add = true;
            AidenList.Add("Aiden & Pandemic");
        }
        if (DataBaseManager.Intel_Aiden3 == true && isAiden3Add == false)
        {
            isAiden3Add = true;
            AidenList.Add("Missing Subject");
        }
        // 엘라
        if (DataBaseManager.keyword_upper == "Ella Triss" && EllaKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = EllaList[EllaKeywordNum];
        }
        if (DataBaseManager.Intel_Ella1 == true && isElla1Add == false)
        {
            isElla1Add = true;
            EllaList.Add("Client");
        }
        if (DataBaseManager.Intel_Ella2 == true && isElla2Add == false)
        {
            isElla2Add = true;
            EllaList.Add("Murdered Clients");
        }

        // 스탠리
        if (DataBaseManager.keyword_upper == "Berkeley Swain" && SwainKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = SwainList[SwainKeywordNum];
        }
        if (DataBaseManager.Intel_Swain1 == true && isSwain1Add == false)
        {
            isSwain1Add = true;
            SwainList.Add("Sewer Survey");
        }

        // 노교수
        if (DataBaseManager.keyword_upper == "노교수" && CaneKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = CaneList[CaneKeywordNum];
        }
        if (DataBaseManager.Intel_Cane1 == true && isCane1Add == false)
        {
            isCane1Add = true;
            CaneList.Add("신이난 노교수");
        }
        // 메이브
        if (DataBaseManager.keyword_upper == "Maeve A Rossi" && MeivKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = MeivList[MeivKeywordNum];
        }
        if (DataBaseManager.Intel_Meiv1 == true && isMeiv1Add == false)
        {
            isMeiv1Add = true;
            MeivList.Add("Mafia friend");
        }
        if (DataBaseManager.Intel_Meiv2 == true && isMeiv2Add == false)
        {
            isMeiv2Add = true;
            MeivList.Add("Promise");
        }
        // 알버트
        if (DataBaseManager.keyword_upper == "Albert Bradley" && AlbertKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = AlbertList[AlbertKeywordNum];
        }
        if (DataBaseManager.Intel_Albert1 == true && isAlbert1Add == false)
        {
            isAlbert1Add = true;
            AlbertList.Add("Police friend");
        }
        if (DataBaseManager.keyword_upper == "SewerWorker" && SewerWorkerKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = SewerWorkerList[SewerWorkerKeywordNum];
        }
        if (DataBaseManager.Intel_SewerWorker1 == true && isSewerWorker1Add == false)
        {
            isSewerWorker1Add = true;
            SewerWorkerList.Add("Owner of Eye?");
        }
        if (DataBaseManager.Intel_SewerWorker2 == true && isSewerWorker2Add == false)
        {
            isSewerWorker2Add = true;
            SewerWorkerList.Add("Unfair dismissal");
        }
        if (DataBaseManager.Intel_SewerWorker3 == true && isSewerWorker3Add == false)
        {
            isSewerWorker3Add = true;
            SewerWorkerList.Add("Cause of Fishy smell");
        }
        if (DataBaseManager.Intel_SewerWorker4 == true && isSewerWorker4Add == false)
        {
            isSewerWorker4Add = true;
            SewerWorkerList.Add("Owner of Uniform");
        }


        if (DataBaseManager.keyword_upper == "Dave" && DaveKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = DaveList[DaveKeywordNum];
        }
        if (DataBaseManager.Intel_Dave1 == true && isDave1Add == false)
        {
            isDave1Add = true;
            DaveList.Add("Bracelet");
        }

        // 장소 

        // 병원
        if (DataBaseManager.keyword_upper == "Hospital" && HospitalKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = HospitalList[HospitalKeywordNum];
        }
        if (DataBaseManager.Intel_Hospital1 == true && isHospital1Add == false)
        {
            isHospital1Add = true;
            HospitalList.Add("State of congestion");
        }



        // 신문사
        if (DataBaseManager.keyword_upper == "DailyNews" && NewspaperKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = NewspaperList[NewspaperKeywordNum];
        }
        if (DataBaseManager.Intel_Newspaper1 == true && isNewspaper1Add == false)
        {
            isNewspaper1Add = true;
            NewspaperList.Add("Husband's job");
        }



        // Client'shouse
        if (DataBaseManager.keyword_upper == "Client'shouse" && QuestHouseKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = QuestHouseList[QuestHouseKeywordNum];
        }
        if (DataBaseManager.Intel_QuestHouse1 == true && isQuestHouse1Add == false)
        {
            isQuestHouse1Add = true;
            QuestHouseList.Add("Adress");
        }

        // 강가
        if (DataBaseManager.keyword_upper == "미스캐토닉 강" && RiversideKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = RiversideList[RiversideKeywordNum];
        }
        if (DataBaseManager.Intel_Riverside1 == true && isRiverside1Add == false)
        {
            isRiverside1Add = true;
            RiversideList.Add("강과 교수");
        }
        // 하수정비사무소
        if (DataBaseManager.keyword_upper == "Sewage MA Office" && SewageMaintenanceOfficeKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = SewageMaintenanceOfficeList[SewageMaintenanceOfficeKeywordNum];
        }
        if (DataBaseManager.Intel_SewageMaintenanceOffice1 == true && isSewageMaintenanceOffice1Add == false)
        {
            isSewageMaintenanceOffice1Add = true;
            SewageMaintenanceOfficeList.Add("Location");
        }
        // 하수도
        if (DataBaseManager.keyword_upper == "Sewer" && SewerKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = SewerList[SewerKeywordNum];
        }
        if (DataBaseManager.Intel_Sewer1 == true && isSewer1Add == false)
        {
            isSewer1Add = true;
            SewerList.Add("Aiden's warning");
        }
        if (DataBaseManager.Intel_Sewer2 == true && isSewer2Add == false)
        {
            isSewer2Add = true;
            SewerList.Add("Root of problem");
        }
        if (DataBaseManager.Intel_Sewer3 == true && isSewer3Add == false)
        {
            isSewer3Add = true;
            SewerList.Add("Manhole");
        }



        // 부두
        if (DataBaseManager.keyword_upper == "Wharf" && WharfKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = WharfList[WharfKeywordNum];
        }
        if (DataBaseManager.Intel_Wharf1 == true && isWharf1Add == false)
        {
            isWharf1Add = true;
            WharfList.Add("Work");
        }

        // 대학
        if (DataBaseManager.keyword_upper == "Univ of Miscatonic" && UniversityKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = UniversityList[UniversityKeywordNum];
        }
        if (DataBaseManager.Intel_University1 == true && isUniversity1Add == false)
        {
            isUniversity1Add = true;
            UniversityList.Add("Astronomical show");
        }




    }

    // Start is called before the first frame update
    void Start()
    {


        DataBaseManager.keyword_main = "Character";
        DataBaseManager.keyword_upper = "Maeve A Rossi";
        DataBaseManager.keyword_downer = "Mafia friend";


    }

    // Update is called once per frame
    void Update()
    {


        downerKeywrodUpdater();
        KeywordUpdate();
    }


  
}
