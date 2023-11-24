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
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void EnterKey()
    {
        DataBaseManager.ConntectMouse = true;
    }
    public void OutKey()
    {
        DataBaseManager.ConntectMouse = false;
    }
    public Text mainSetting;
    public Text upperSetting;
    public Text downerSetting;
    public GameObject dialogWindow;
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
                if (insomniaKeywordCount <= insomniaKeywordNum)
                {
                    insomniaKeywordNum = 0;
                }
                else
                {
                    insomniaKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Nightmare")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (nightmareKeywordCount <= nightmareKeywordNum)
                {
                    nightmareKeywordNum = 0;
                }
                else
                {
                    nightmareKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Paranoia")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (paranoiaKeywordCount <= paranoiaKeywordNum)
                {
                    paranoiaKeywordNum = 0;
                }
                else
                {
                    paranoiaKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "SewerGhostStory")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (sewerGhostStoryKeywordCount <= sewerGhostStoryKeywordNum)
                {
                    sewerGhostStoryKeywordNum = 0;
                }
                else
                {
                    sewerGhostStoryKeywordNum += 1;
                }
            }
        }
        // 인물
        if (DataBaseManager.keyword_upper == "Aiden Triss")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (aidenKeywordCount <= aidenKeywordNum)
                {
                    aidenKeywordNum = 0;
                }
                else
                {
                    aidenKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Ella Triss")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (ellaKeywordCount <= ellaKeywordNum)
                {
                    ellaKeywordNum = 0;
                }
                else
                {
                    ellaKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Maeve A Rossi")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (meivKeywordCount <= meivKeywordNum)
                {
                    meivKeywordNum = 0;
                }
                else
                {
                    meivKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Albert Bradley")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (albertKeywordCount <= albertKeywordNum)
                {
                    albertKeywordNum = 0;
                }
                else
                {
                    albertKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "SewerWorker")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (sewerWorkerKeywordCount <= sewerWorkerKeywordNum)
                {
                    sewerWorkerKeywordNum = 0;
                }
                else
                {
                    sewerWorkerKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Dave")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (daveKeywordCount <= daveKeywordNum)
                {
                    daveKeywordNum = 0;
                }
                else
                {
                    daveKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "MissingPeople")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (missingPeopleKeywordCount <= missingPeopleKeywordNum)
                {
                    missingPeopleKeywordNum = 0;
                }
                else
                {
                    missingPeopleKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Wharf")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (wharfKeywordCount <= wharfKeywordNum)
                {
                    wharfKeywordNum = 0;
                }
                else
                {
                    wharfKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Sewage MA Office")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (sewageMaintenanceOfficeKeywordCount <= sewageMaintenanceOfficeKeywordNum)
                {
                    sewageMaintenanceOfficeKeywordNum = 0;
                }
                else
                {
                    sewageMaintenanceOfficeKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Sewer")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (sewerKeywordCount <= sewerKeywordNum)
                {
                    sewerKeywordNum = 0;
                }
                else
                {
                    sewerKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Client'shouse")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (questHouseKeywordCount <= questHouseKeywordNum)
                {
                    questHouseKeywordNum = 0;
                }
                else
                {
                    questHouseKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Univ of Miscatonic")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (universityKeywordCount<= universityKeywordNum)
                {
                    universityKeywordNum = 0;
                }
                else
                {
                    universityKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "DailyNews")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (newspaperKeywordCount <= newspaperKeywordNum)
                {
                    newspaperKeywordNum = 0;
                }
                else
                {
                    newspaperKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Safe")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (safeKeywordCount <= safeKeywordNum)
                {
                    safeKeywordNum = 0;
                }
                else
                {
                    safeKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "RiverWaterQuality")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (riverWaterQualityKeywordCount <= riverWaterQualityKeywordNum)
                {
                    riverWaterQualityKeywordNum = 0;
                }
                else
                {
                    riverWaterQualityKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "FishySmell")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (fishySmellKeywordCount <= fishySmellKeywordNum)
                {
                    fishySmellKeywordNum = 0;
                }
                else
                {
                    fishySmellKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "MurderCase")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (murderCaseKeywordCount <= murderCaseKeywordNum)
                {
                    murderCaseKeywordNum = 0;
                }
                else
                {
                    murderCaseKeywordNum += 1;
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
                if ( insomniaKeywordNum<=0)
                {
                    insomniaKeywordNum = insomniaKeywordCount;
                }
                else
                {
                    insomniaKeywordNum -= 1;
                }   
            }
        }
        if (DataBaseManager.keyword_upper == "Nightmare")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (nightmareKeywordNum <= 0)
                {
                    nightmareKeywordNum = nightmareKeywordCount;
                }
                else
                {
                    nightmareKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Paranoia")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (paranoiaKeywordNum <= 0)
                {
                    paranoiaKeywordNum = paranoiaKeywordCount;
                }
                else
                {
                    paranoiaKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "SewerGhostStory")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (sewerGhostStoryKeywordNum <= 0)
                {
                    sewerGhostStoryKeywordNum = sewerGhostStoryKeywordCount;
                }
                else
                {
                    sewerGhostStoryKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Aiden Triss")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (aidenKeywordNum <= 0)
                {
                    aidenKeywordNum = aidenKeywordCount;
                }
                else
                {
                    aidenKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Ella Triss")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (ellaKeywordNum <= 0)
                {
                    ellaKeywordNum = ellaKeywordCount;
                }
                else
                {
                    ellaKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Maeve A Rossi")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (meivKeywordNum <= 0)
                {
                    meivKeywordNum = meivKeywordCount;
                }
                else
                {
                    meivKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Albert Bradley")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (albertKeywordNum <= 0)
                {
                    albertKeywordNum = albertKeywordCount;
                }
                else
                {
                    albertKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "SewerWorker")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (sewerWorkerKeywordNum <= 0)
                {
                    sewerWorkerKeywordNum = sewerWorkerKeywordCount;
                }
                else
                {
                    sewerWorkerKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Dave")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (daveKeywordNum <= 0)
                {
                    daveKeywordNum = daveKeywordCount;
                }
                else
                {
                    daveKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "MissingPeople")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (missingPeopleKeywordNum <= 0)
                {
                    missingPeopleKeywordNum = missingPeopleKeywordCount;
                }
                else
                {
                    missingPeopleKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Wharf")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (wharfKeywordNum <= 0)
                {
                    wharfKeywordNum = wharfKeywordCount;
                }
                else
                {
                    wharfKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "DailyNews")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (newspaperKeywordNum <= 0)
                {
                    newspaperKeywordNum = newspaperKeywordCount;
                }
                else
                {
                    newspaperKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Client'shouse")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (questHouseKeywordNum <= 0)
                {
                    questHouseKeywordNum = questHouseKeywordCount;
                }
                else
                {
                    questHouseKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Univ of Miscatonic")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (universityKeywordNum <= 0)
                {
                    universityKeywordNum = universityKeywordCount;
                }
                else
                {
                    universityKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Safe")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (safeKeywordNum <= 0)
                {
                    safeKeywordNum = safeKeywordCount;
                }
                else
                {
                    safeKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Sewage MA Office")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (sewageMaintenanceOfficeKeywordNum <= 0)
                {
                    sewageMaintenanceOfficeKeywordNum = sewageMaintenanceOfficeKeywordCount;
                }
                else
                {
                    sewageMaintenanceOfficeKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "Sewer")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (sewerKeywordNum <= 0)
                {
                    sewerKeywordNum = sewerKeywordCount;
                }
                else
                {
                    sewerKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "RiverWaterQuality")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (riverWaterQualityKeywordNum <= 0)
                {
                    riverWaterQualityKeywordNum = riverWaterQualityKeywordCount;
                }
                else
                {
                    riverWaterQualityKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "FishySmell")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (fishySmellKeywordNum <= 0)
                {
                    fishySmellKeywordNum = fishySmellKeywordCount;
                }
                else
                {
                    fishySmellKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keyword_upper == "MurderCase")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (murderCaseKeywordNum <= 0)
                {
                    murderCaseKeywordNum = murderCaseKeywordCount;
                }
                else
                {
                    murderCaseKeywordNum -= 1;
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
    public List<string> planetaryParadeList = new List<string>();//string들어가야하고
    int planetaryParadeKeywordCount = 0;
    public int planetaryParadeKeywordNum = 0;

    public List<string> nightmareList = new List<string>();//string들어가야하고
    int nightmareKeywordCount = 0;
    public int nightmareKeywordNum = 0;

    public List<string> insomniaList = new List<string>();//string들어가야하고
    int insomniaKeywordCount;
    public int insomniaKeywordNum = 0;

    public List<string> paranoiaList = new List<string>();//string들어가야하고
    int paranoiaKeywordCount;
    public int paranoiaKeywordNum = 0;

    public List<string> missingPeopleList = new List<string>();//string들어가야하고
    int missingPeopleKeywordCount = 0;
    public int missingPeopleKeywordNum = 0;

    public List<string> sewerGhostStoryList = new List<string>();//string들어가야하고
    int sewerGhostStoryKeywordCount = 0;
    public int sewerGhostStoryKeywordNum = 0;

    public List<string> fishySmellList = new List<string>();//string들어가야하고
    int fishySmellKeywordCount = 0;
    public int fishySmellKeywordNum = 0;

    public List<string> riverWaterQualityList = new List<string>();//string들어가야하고
    int riverWaterQualityKeywordCount = 0;
    public int riverWaterQualityKeywordNum = 0;

    public List<string> creepyEyesList = new List<string>();//string들어가야하고
    int creepyEyesKeywordCount = 0;
    public int creepyEyesKeywordNum = 0;

    public List<string> murderCaseList = new List<string>();//string들어가야하고
    int murderCaseKeywordCount = 0;
    public int murderCaseKeywordNum = 0;

    public List<string> safeList = new List<string>();//string들어가야하고
    int safeKeywordCount = 0;
    public int safeKeywordNum = 0;
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
    public List<string> aidenList = new List<string>();//string들어가야하고
    int aidenKeywordCount = 0;
    public int aidenKeywordNum = 0;

    public List<string> ellaList = new List<string>();//string들어가야하고
    int ellaKeywordCount = 0;
    public int ellaKeywordNum = 0;

    public List<string> swainList = new List<string>();//string들어가야하고
    int swainKeywordCount = 0;
    public int swainKeywordNum = 0;

    public List<string> caneList = new List<string>();//string들어가야하고
    int caneKeywordCount = 0;
    public int caneKeywordNum = 0;

    public List<string> albertList = new List<string>();//string들어가야하고
    int albertKeywordCount = 0;
    public int albertKeywordNum = 0;

    public List<string> meivList = new List<string>();//string들어가야하고
    int meivKeywordCount = 0;
    public int meivKeywordNum = 0;

    public List<string> sewerWorkerList = new List<string>();//string들어가야하고
    int sewerWorkerKeywordCount = 0;
    public int sewerWorkerKeywordNum = 0;

    public List<string> daveList = new List<string>();//string들어가야하고
    int daveKeywordCount = 0;
    public int daveKeywordNum = 0;
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
    public List<string> hospitalList = new List<string>();//string들어가야하고
    int hospitalKeywordCount = 0;
    public int hospitalKeywordNum = 0;

    public List<string> newspaperList = new List<string>();//string들어가야하고
    int newspaperKeywordCount = 0;
    public int newspaperKeywordNum = 0;

    public List<string> questHouseList = new List<string>();//string들어가야하고
    int questHouseKeywordCount = 0;
    public int questHouseKeywordNum = 0;

    public List<string> riversideList = new List<string>();//string들어가야하고
    int riversideKeywordCount = 0;
    public int riversideKeywordNum = 0;

    public List<string> sewageMaintenanceOfficeList = new List<string>();//string들어가야하고
    int sewageMaintenanceOfficeKeywordCount = 0;
    public int sewageMaintenanceOfficeKeywordNum = 0;

    public List<string> sewerList = new List<string>();//string들어가야하고
    int sewerKeywordCount = 0;
    public int sewerKeywordNum = 0;

    public List<string> wharfList = new List<string>();//string들어가야하고
    int wharfKeywordCount = 0;
    public int wharfKeywordNum = 0;

    public List<string> universityList = new List<string>();//string들어가야하고
    int universityKeywordCount = 0;
    public int universityKeywordNum = 0;
    //사건
    bool isSewer1Add = false;
    bool isSewer2Add = false;
    bool isSewer3Add = false;
    bool isSewer4Add = false;
    bool isHospital1Add = false;
    bool isNewspaper1Add = false;
    bool isQuestHouse1Add = false;
    bool isRiverside1Add = false;
    bool isSewageMaintenanceOffice1Add = false;
    bool isWharf1Add = false;
    bool isUniversity1Add = false;
    public void DownerKeywrodUpdater()
    {        
        planetaryParadeKeywordCount = planetaryParadeList.Count-1;
        nightmareKeywordCount = nightmareList.Count - 1;
        insomniaKeywordCount = insomniaList.Count - 1;
        paranoiaKeywordCount = paranoiaList.Count - 1;
        missingPeopleKeywordCount = missingPeopleList.Count - 1;
        sewerGhostStoryKeywordCount = sewerGhostStoryList.Count - 1;
        fishySmellKeywordCount = fishySmellList.Count - 1;
        riverWaterQualityKeywordCount = riverWaterQualityList.Count - 1;
        creepyEyesKeywordCount = creepyEyesList.Count - 1;
        murderCaseKeywordCount = murderCaseList.Count - 1;
        safeKeywordCount = safeList.Count - 1;

        ellaKeywordCount = ellaList.Count - 1;
        aidenKeywordCount = aidenList.Count - 1;
        swainKeywordCount = swainList.Count - 1;
        caneKeywordCount = caneList.Count - 1;
        meivKeywordCount = meivList.Count - 1;
        albertKeywordCount = albertList.Count - 1;
        sewerWorkerKeywordCount = sewerWorkerList.Count - 1;

        hospitalKeywordCount = hospitalList.Count - 1;
        questHouseKeywordCount = questHouseList.Count - 1;
        newspaperKeywordCount = newspaperList.Count - 1;
        riversideKeywordCount = riversideList.Count - 1;
        sewageMaintenanceOfficeKeywordCount = sewageMaintenanceOfficeList.Count - 1;
        sewerKeywordCount = sewerList.Count - 1;
        wharfKeywordCount = wharfList.Count - 1;
        universityKeywordCount = universityList.Count - 1;
        // 사건 
        // 행성대직렬
        if (DataBaseManager.keyword_upper == "PlanetaryParade" && planetaryParadeKeywordCount>=0)
        {
            DataBaseManager.keyword_downer = planetaryParadeList[planetaryParadeKeywordNum];
        }
        if (DataBaseManager.Intel_PlanetaryParade1 == true&&isPlanetaryParade1Add == false)
        {
            isPlanetaryParade1Add = true;
            planetaryParadeList.Add("Contents of a Newspaper");
        }
        if (DataBaseManager.Intel_PlanetaryParade2 == true && isPlanetaryParade2Add == false)
        {
            isPlanetaryParade2Add = true;
            planetaryParadeList.Add("Meetings");
        }
        // 악몽
        else if (DataBaseManager.keyword_upper == "Nightmare" && nightmareKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = nightmareList[nightmareKeywordNum];
        }
        if (DataBaseManager.Intel_Nightmare1 == true && isNightmare1Add == false)
        {
            isNightmare1Add = true;
            nightmareList.Add("Aiden's Nightmare");
        }
        if (DataBaseManager.Intel_Nightmare2 == true && isNightmare2Add == false)
        {
            isNightmare2Add = true;
            nightmareList.Add("Content of a Nightmare");
        }
        if (DataBaseManager.Intel_Nightmare3 == true && isNightmare3Add == false)
        {
            isNightmare3Add = true;
            nightmareList.Add("Aiden's Detail");
        }
        if (DataBaseManager.Intel_Nightmare4 == true && isNightmare4Add == false)
        {
            isNightmare4Add = true;
            nightmareList.Add("Water and Nightmare");
        }
        // 불면증
        else if (DataBaseManager.keyword_upper == "Insomnia" && insomniaKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = insomniaList[insomniaKeywordNum];
        }
        if (DataBaseManager.Intel_Insomnia1 == true && isInsomnia1Add == false)
        {
            isInsomnia1Add = true;
            insomniaList.Add("Newspaper Article");
        }
        if (DataBaseManager.Intel_Insomnia2 == true && isInsomnia2Add == false)
        {
            isInsomnia2Add = true;
            insomniaList.Add("Doctor's opinion");
        }
        if (DataBaseManager.Intel_Insomnia3 == true && isInsomnia3Add == false)
        {
            isInsomnia3Add = true;
           // InsomniaList.Add("Cause of Insomnia");
        }
        // 편집증
        else if (DataBaseManager.keyword_upper == "Paranoia" && paranoiaKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = paranoiaList[paranoiaKeywordNum];
        }

        if (DataBaseManager.Intel_Paranoia1 == true && isParanoia1Add == false)
        {
            isParanoia1Add = true;
            paranoiaList.Add("Husband's Condition");
        }
        if (DataBaseManager.Intel_Paranoia2 == true && isParanoia2Add == false)
        {
            isParanoia2Add = true;
            paranoiaList.Add("Cause of Paranoia");
        }
        // 실종사건
        else if (DataBaseManager.keyword_upper == "MissingPeople" && missingPeopleKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = missingPeopleList[missingPeopleKeywordNum];
        }
        if (DataBaseManager.Intel_MissingPeople1 == true && isMissingPeople1Add == false)
        {
            isMissingPeople1Add = true;
            missingPeopleList.Add("Albert's Information");
        }
        if (DataBaseManager.Intel_MissingPeople2 == true && isMissingPeople2Add == false)
        {
            isMissingPeople2Add = true;
            missingPeopleList.Add("Reporting by Swain");
        }
        if (DataBaseManager.Intel_MissingPeople3 == true && isMissingPeople3Add == false)
        {
            isMissingPeople3Add = true;
            missingPeopleList.Add("Kidnap");
        }
        // 하수도괴담.
        else if (DataBaseManager.keyword_upper == "SewerGhostStory" && sewerGhostStoryKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = sewerGhostStoryList[sewerGhostStoryKeywordNum];
        }
        if (DataBaseManager.Intel_SewerGhostStory1 == true && isSewerGhostStory1Add == false)
        {
            isSewerGhostStory1Add = true;
            sewerGhostStoryList.Add("Contents of a GhostStory");
        }
        if (DataBaseManager.Intel_SewerGhostStory2 == true && isSewerGhostStory2Add == false)
        {
            isSewerGhostStory2Add = true;
            sewerGhostStoryList.Add("Truth of a rumor");
        }
        // 비릿한냄새
        else if (DataBaseManager.keyword_upper == "FishySmell" && fishySmellKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = fishySmellList[fishySmellKeywordNum];
        }
        if (DataBaseManager.Intel_FishySmell1 == true && isFishySmell1Add == false)
        {
            isFishySmell1Add = true;
            fishySmellList.Add("Rumor");
        }
        if (DataBaseManager.Intel_FishySmell2 == true && isFishySmell2Add == false)
        {
            isFishySmell2Add = true;
            fishySmellList.Add("Source of the smell?");
        }
        if (DataBaseManager.Intel_FishySmell3 == true && isFishySmell3Add == false)
        {
            isFishySmell3Add = true;
            fishySmellList.Add("Source speculation");
        }
        if (DataBaseManager.Intel_FishySmell4 == true && isFishySmell4Add == false)
        {
            isFishySmell4Add = true;
            fishySmellList.Add("Sewer & FishySmell");
        }
        // 강의수질
        else if (DataBaseManager.keyword_upper == "RiverWaterQuality" && riverWaterQualityKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = riverWaterQualityList[riverWaterQualityKeywordNum];
        }
        if (DataBaseManager.Intel_RiverWaterQuality1 == true && isRiverWaterQuality1Add == false)
        {
            isRiverWaterQuality1Add = true;
            riverWaterQualityList.Add("Strange Point");
        }
        if (DataBaseManager.Intel_RiverWaterQuality2 == true && isRiverWaterQuality2Add == false)
        {
            isRiverWaterQuality2Add = true;
            riverWaterQualityList.Add("Details");
        }
        if (DataBaseManager.Intel_RiverWaterQuality3 == true && isRiverWaterQuality3Add == false)
        {
            isRiverWaterQuality3Add = true;
            riverWaterQualityList.Add("Fishy Slime");
        }
        // 섬뜩한눈
        else if (DataBaseManager.keyword_upper == "CreepyEyes" && creepyEyesKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = creepyEyesList[creepyEyesKeywordNum];
        }

        if (DataBaseManager.Intel_CreepyEyes1 == true && isCreepyEyes1Add == false)
        {
            isCreepyEyes1Add = true;
            creepyEyesList.Add("Saw in the sewer");
        }
        // 살인사건
        else if (DataBaseManager.keyword_upper == "MurderCase" && murderCaseKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = murderCaseList[murderCaseKeywordNum];
        }
        if (DataBaseManager.Intel_MurderCase1 == true && isMurderCase1Add == false)
        {
            isMurderCase1Add = true;
            murderCaseList.Add("Witness Testimony");
        }
        if (DataBaseManager.Intel_MurderCase2 == true && isMurderCase2Add == false)
        {
            isMurderCase2Add = true;
            murderCaseList.Add("Missing People & Fishy Smell");
        }
        if (DataBaseManager.Intel_MurderCase3 == true && isMurderCase3Add == false)
        {
            isMurderCase3Add = true;
            murderCaseList.Add("The Culprit");
        }
        if (DataBaseManager.Intel_MurderCase4 == true && isMurderCase4Add == false)
        {
            isMurderCase4Add = true;
            murderCaseList.Add("Cause of Death");
        }
        if (DataBaseManager.Intel_MurderCase5 == true && isMurderCase5Add == false)
        {
            isMurderCase5Add = true;
            murderCaseList.Add("Webbed Feet");
        }
        if (DataBaseManager.Intel_MurderCase6 == true && isMurderCase6Add == false)
        {
            isMurderCase6Add = true;
            murderCaseList.Add("Way to Escape");
        }
        // 금고
        else if (DataBaseManager.keyword_upper == "Safe" && safeKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = safeList[safeKeywordNum];
        }
        if (DataBaseManager.Intel_Safe1 == true && isSafe1Add == false)
        {
            isSafe1Add = true;
            safeList.Add("Aiden's Safe");
        }
        if (DataBaseManager.Intel_Safe2 == true && isSafe2Add == false)
        {
            isSafe2Add = true;
            safeList.Add("Acquiring a Safe");
        }
        // 인물
        // 에이든
        if (DataBaseManager.keyword_upper == "Aiden Triss" && aidenKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = aidenList[aidenKeywordNum];
        }
        if (DataBaseManager.Intel_Aiden1 == true && isAiden1Add == false)
        {
            isAiden1Add = true;
            aidenList.Add("Subject to request");
        }
        if (DataBaseManager.Intel_Aiden2 == true && isAiden2Add == false)
        {
            isAiden2Add = true;
            aidenList.Add("Aiden & Pandemic");
        }
        if (DataBaseManager.Intel_Aiden3 == true && isAiden3Add == false)
        {
            isAiden3Add = true;
            aidenList.Add("Missing Subject");
        }
        // 엘라
        if (DataBaseManager.keyword_upper == "Ella Triss" && ellaKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = ellaList[ellaKeywordNum];
        }
        if (DataBaseManager.Intel_Ella1 == true && isElla1Add == false)
        {
            isElla1Add = true;
            ellaList.Add("Client");
        }
        if (DataBaseManager.Intel_Ella2 == true && isElla2Add == false)
        {
            isElla2Add = true;
            ellaList.Add("Murdered Clients");
        }
        // 스탠리
        if (DataBaseManager.keyword_upper == "Berkeley Swain" && swainKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = swainList[swainKeywordNum];
        }
        if (DataBaseManager.Intel_Swain1 == true && isSwain1Add == false)
        {
            isSwain1Add = true;
            swainList.Add("Sewer Survey");
        }
        // 노교수
        if (DataBaseManager.keyword_upper == "노교수" && caneKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = caneList[caneKeywordNum];
        }
        if (DataBaseManager.Intel_Cane1 == true && isCane1Add == false)
        {
            isCane1Add = true;
            caneList.Add("신이난 노교수");
        }
        // 메이브
        if (DataBaseManager.keyword_upper == "Maeve A Rossi" && meivKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = meivList[meivKeywordNum];
        }
        if (DataBaseManager.Intel_Meiv1 == true && isMeiv1Add == false)
        {
            isMeiv1Add = true;
            meivList.Add("Mafia friend");
        }
        if (DataBaseManager.Intel_Meiv2 == true && isMeiv2Add == false)
        {
            isMeiv2Add = true;
            meivList.Add("Promise");
        }
        // 알버트
        if (DataBaseManager.keyword_upper == "Albert Bradley" && albertKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = albertList[albertKeywordNum];
        }
        if (DataBaseManager.Intel_Albert1 == true && isAlbert1Add == false)
        {
            isAlbert1Add = true;
            albertList.Add("Police friend");
        }
        if (DataBaseManager.keyword_upper == "SewerWorker" && sewerWorkerKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = sewerWorkerList[sewerWorkerKeywordNum];
        }
        if (DataBaseManager.Intel_SewerWorker1 == true && isSewerWorker1Add == false)
        {
            isSewerWorker1Add = true;
            sewerWorkerList.Add("Owner of Eye?");
        }
        if (DataBaseManager.Intel_SewerWorker2 == true && isSewerWorker2Add == false)
        {
            isSewerWorker2Add = true;
            sewerWorkerList.Add("Unfair dismissal");
        }
        if (DataBaseManager.Intel_SewerWorker3 == true && isSewerWorker3Add == false)
        {
            isSewerWorker3Add = true;
            sewerWorkerList.Add("Cause of Fishy smell");
        }
        if (DataBaseManager.Intel_SewerWorker4 == true && isSewerWorker4Add == false)
        {
            isSewerWorker4Add = true;
            sewerWorkerList.Add("Owner of Uniform");
        }
        if (DataBaseManager.keyword_upper == "Dave" && daveKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = daveList[daveKeywordNum];
        }
        if (DataBaseManager.Intel_Dave1 == true && isDave1Add == false)
        {
            isDave1Add = true;
            daveList.Add("Bracelet");
        }
        // 장소 
        // 병원
        if (DataBaseManager.keyword_upper == "Hospital" && hospitalKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = hospitalList[hospitalKeywordNum];
        }
        if (DataBaseManager.Intel_Hospital1 == true && isHospital1Add == false)
        {
            isHospital1Add = true;
            hospitalList.Add("State of congestion");
        }
        // 신문사
        if (DataBaseManager.keyword_upper == "DailyNews" && newspaperKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = newspaperList[newspaperKeywordNum];
        }
        if (DataBaseManager.Intel_Newspaper1 == true && isNewspaper1Add == false)
        {
            isNewspaper1Add = true;
            newspaperList.Add("Husband's job");
        }
        // Client'shouse
        if (DataBaseManager.keyword_upper == "Client'shouse" && questHouseKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = questHouseList[questHouseKeywordNum];
        }
        if (DataBaseManager.Intel_QuestHouse1 == true && isQuestHouse1Add == false)
        {
            isQuestHouse1Add = true;
            questHouseList.Add("Adress");
        }
        // 강가
        if (DataBaseManager.keyword_upper == "미스캐토닉 강" && riversideKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = riversideList[riversideKeywordNum];
        }
        if (DataBaseManager.Intel_Riverside1 == true && isRiverside1Add == false)
        {
            isRiverside1Add = true;
            riversideList.Add("강과 교수");
        }
        // 하수정비사무소
        if (DataBaseManager.keyword_upper == "Sewage MA Office" && sewageMaintenanceOfficeKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = sewageMaintenanceOfficeList[sewageMaintenanceOfficeKeywordNum];
        }
        if (DataBaseManager.Intel_SewageMaintenanceOffice1 == true && isSewageMaintenanceOffice1Add == false)
        {
            isSewageMaintenanceOffice1Add = true;
            sewageMaintenanceOfficeList.Add("Location");
        }
        // 하수도
        if (DataBaseManager.keyword_upper == "Sewer" && sewerKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = sewerList[sewerKeywordNum];
        }
        if (DataBaseManager.Intel_Sewer1 == true && isSewer1Add == false)
        {
            isSewer1Add = true;
            sewerList.Add("Aiden's warning");
        }
        if (DataBaseManager.Intel_Sewer2 == true && isSewer2Add == false)
        {
            isSewer2Add = true;
            sewerList.Add("Root of problem");
        }
        if (DataBaseManager.Intel_Sewer3 == true && isSewer3Add == false)
        {
            isSewer3Add = true;
            sewerList.Add("Manhole");
        }
        if (DataBaseManager.Intel_Sewer4 == true && isSewer4Add == false)
        {
            isSewer4Add = true;
            sewerList.Add("Advent");
        }
        // 부두
        if (DataBaseManager.keyword_upper == "Wharf" && wharfKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = wharfList[wharfKeywordNum];
        }
        if (DataBaseManager.Intel_Wharf1 == true && isWharf1Add == false)
        {
            isWharf1Add = true;
            wharfList.Add("Work");
        }
        // 대학
        if (DataBaseManager.keyword_upper == "Univ of Miscatonic" && universityKeywordCount >= 0)
        {
            DataBaseManager.keyword_downer = universityList[universityKeywordNum];
        }
        if (DataBaseManager.Intel_University1 == true && isUniversity1Add == false)
        {
            isUniversity1Add = true;
            universityList.Add("Astronomical show");
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
        DownerKeywrodUpdater();
        KeywordUpdate();
    }
    public GameObject daveIntel_1;
    public GameObject daveIntel_2;
    public GameObject daveIntel_3;
    public GameObject daveIntel_4;
    public GameObject daveIntel_5;
    public GameObject daveIntel_6;
}
