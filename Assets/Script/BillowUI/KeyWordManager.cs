using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyWordManager : MonoBehaviour
{
    private static KeyWordManager instance = null;
    public Text mainSetting;
    public Text upperSetting;
    public Text downerSetting;
    public GameObject dialogWindow;
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
    public void EndterKey()
    {
        DataBaseManager.conntectMouse = true;
    }
    public void OutKey()
    {
        DataBaseManager.conntectMouse = false;
    }

    public void MainRight()
    {
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
        {
            if (DataBaseManager.keywordMain == "Event")
            {
                DataBaseManager.keywordMain = "Character";
                DataBaseManager.keywordUpper = upperCharacterKewordList[nowCharacterUpperKeywordNum];
            }
            else if (DataBaseManager.keywordMain == "Character")
            {
                DataBaseManager.keywordMain = "Place";
                DataBaseManager.keywordUpper = upperPlaceKeywordList[nowPlaceUpperKeywordNum];
            }
            else if (DataBaseManager.keywordMain == "Place")
            {
                if (upperEventKeywordList.Count != 0)
                {
                    DataBaseManager.keywordMain = "Event";
                    DataBaseManager.keywordUpper = upperEventKeywordList[nowEventUpperKeywordNum];
                }
                else
                {
                    DataBaseManager.keywordMain = "Character";
                    DataBaseManager.keywordUpper = upperCharacterKewordList[nowCharacterUpperKeywordNum];
                }
            }
        }

    }
    public void MainLeft()
    {
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
        {
            if (DataBaseManager.keywordMain == "Event")
            {
                DataBaseManager.keywordMain = "Place";
                DataBaseManager.keywordUpper = upperPlaceKeywordList[nowPlaceUpperKeywordNum];
            }
            else if (DataBaseManager.keywordMain == "Character")
            {
                if (upperEventKeywordList.Count != 0)
                {
                    DataBaseManager.keywordMain = "Event";
                    DataBaseManager.keywordUpper = upperEventKeywordList[nowEventUpperKeywordNum];
                }
                else
                {
                    DataBaseManager.keywordMain = "Place";
                    DataBaseManager.keywordUpper = upperPlaceKeywordList[nowPlaceUpperKeywordNum];
                }
            }
            else if (DataBaseManager.keywordMain == "Place")
            {
                DataBaseManager.keywordMain = "Character";
                DataBaseManager.keywordUpper = upperCharacterKewordList[nowCharacterUpperKeywordNum];
            }
        }
    }
    public void UpperRight()
    {
        if (DataBaseManager.keywordMain == "Event")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                nowEventUpperKeywordNum += 1;
                if (upperEventKeywordCount < nowEventUpperKeywordNum)
                {
                    nowEventUpperKeywordNum = 0;
                }
                DataBaseManager.keywordUpper = upperEventKeywordList[nowEventUpperKeywordNum];
            }
        }
        else if (DataBaseManager.keywordMain == "Place")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                nowPlaceUpperKeywordNum += 1;
                if (upperPlaceKeywordCount < nowPlaceUpperKeywordNum)
                {
                    nowPlaceUpperKeywordNum = 0;
                }
                DataBaseManager.keywordUpper = upperPlaceKeywordList[nowPlaceUpperKeywordNum];
            }
        }
        else if (DataBaseManager.keywordMain == "Character")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                nowCharacterUpperKeywordNum += 1;
                if (upperCharacterKeywordCount < nowCharacterUpperKeywordNum)
                {
                    nowCharacterUpperKeywordNum = 0;
                }
                DataBaseManager.keywordUpper = upperCharacterKewordList[nowCharacterUpperKeywordNum];
            }
        }
    }
    public void UpperLeft()
    {
        if (DataBaseManager.keywordMain == "Event")
        {
            nowEventUpperKeywordNum -= 1;
            if (0 > nowEventUpperKeywordNum)
            {
                nowEventUpperKeywordNum = upperEventKeywordCount;
            }
            DataBaseManager.keywordUpper = upperEventKeywordList[nowEventUpperKeywordNum];
        }
        else if (DataBaseManager.keywordMain == "Place")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                nowPlaceUpperKeywordNum -= 1;
                if (0 > nowPlaceUpperKeywordNum)
                {
                    nowPlaceUpperKeywordNum = upperPlaceKeywordCount;
                }
                DataBaseManager.keywordUpper = upperPlaceKeywordList[nowPlaceUpperKeywordNum];
            }
        }
        else if (DataBaseManager.keywordMain == "Character")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                nowCharacterUpperKeywordNum -= 1;
                if (0 > nowCharacterUpperKeywordNum)
                {
                    nowCharacterUpperKeywordNum = upperCharacterKeywordCount;
                }
                DataBaseManager.keywordUpper = upperCharacterKewordList[nowCharacterUpperKeywordNum];
            }
        }
    }
    public void DownerRight()
    {

        if (DataBaseManager.keywordUpper == "Insomnia")
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
        if (DataBaseManager.keywordUpper == "Nightmare")
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
        if (DataBaseManager.keywordUpper == "Paranoia")
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
        if (DataBaseManager.keywordUpper == "SewerGhostStory")
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
        if (DataBaseManager.keywordUpper == "Aiden Triss")
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
        if (DataBaseManager.keywordUpper == "Ella Triss")
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
        if (DataBaseManager.keywordUpper == "Maeve A Rossi")
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
        if (DataBaseManager.keywordUpper == "Albert Bradley")
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
        if (DataBaseManager.keywordUpper == "SewerWorker")
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
        if (DataBaseManager.keywordUpper == "Dave")
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
        if (DataBaseManager.keywordUpper == "MissingPeople")
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

        if (DataBaseManager.keywordUpper == "Wharf")
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
        if (DataBaseManager.keywordUpper == "Sewage MA Office")
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
        if (DataBaseManager.keywordUpper == "Sewer")
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
        if (DataBaseManager.keywordUpper == "Client'shouse")
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
        if (DataBaseManager.keywordUpper == "Univ of Miscatonic")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (universityKeywordCount <= universityKeywordNum)
                {
                    universityKeywordNum = 0;
                }
                else
                {
                    universityKeywordNum += 1;
                }
            }
        }
        if (DataBaseManager.keywordUpper == "DailyNews")
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
        if (DataBaseManager.keywordUpper == "Safe")
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
        if (DataBaseManager.keywordUpper == "RiverWaterQuality")
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
        if (DataBaseManager.keywordUpper == "FishySmell")
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
        if (DataBaseManager.keywordUpper == "MurderCase")
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
        if (DataBaseManager.keywordUpper == "Insomnia")
        {
            if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                if (insomniaKeywordNum <= 0)
                {
                    insomniaKeywordNum = insomniaKeywordCount;
                }
                else
                {
                    insomniaKeywordNum -= 1;
                }
            }
        }
        if (DataBaseManager.keywordUpper == "Nightmare")
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
        if (DataBaseManager.keywordUpper == "Paranoia")
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
        if (DataBaseManager.keywordUpper == "SewerGhostStory")
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
        if (DataBaseManager.keywordUpper == "Aiden Triss")
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
        if (DataBaseManager.keywordUpper == "Ella Triss")
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
        if (DataBaseManager.keywordUpper == "Maeve A Rossi")
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
        if (DataBaseManager.keywordUpper == "Albert Bradley")
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
        if (DataBaseManager.keywordUpper == "SewerWorker")
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
        if (DataBaseManager.keywordUpper == "Dave")
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
        if (DataBaseManager.keywordUpper == "MissingPeople")
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
        if (DataBaseManager.keywordUpper == "Wharf")
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
        if (DataBaseManager.keywordUpper == "DailyNews")
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
        if (DataBaseManager.keywordUpper == "Client'shouse")
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
        if (DataBaseManager.keywordUpper == "Univ of Miscatonic")
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
        if (DataBaseManager.keywordUpper == "Safe")
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
        if (DataBaseManager.keywordUpper == "Sewage MA Office")
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
        if (DataBaseManager.keywordUpper == "Sewer")
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
        if (DataBaseManager.keywordUpper == "RiverWaterQuality")
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
        if (DataBaseManager.keywordUpper == "FishySmell")
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
        if (DataBaseManager.keywordUpper == "MurderCase")
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
        mainSetting.text = DataBaseManager.keywordMain;
        upperSetting.text = DataBaseManager.keywordUpper;
        downerSetting.text = DataBaseManager.keywordDowner;

        // 상위키워드 업데이트
        upperEventKeywordCount = upperEventKeywordList.Count - 1;
        upperCharacterKeywordCount = upperCharacterKewordList.Count - 1;
        upperPlaceKeywordCount = upperPlaceKeywordList.Count - 1;

        // 사건
        if (isPlanetaryParadeAddList == false)
        {
            if (DataBaseManager.intelPlanetaryParade1 == true || DataBaseManager.intelPlanetaryParade2 == true || DataBaseManager.intelPlanetaryParade3 == true || DataBaseManager.intelPlanetaryParade4 == true || DataBaseManager.intelPlanetaryParade5 == true || DataBaseManager.intelPlanetaryParade6 == true)
            {
                isPlanetaryParadeAddList = true;
                upperEventKeywordList.Add("PlanetaryParade");
            }
        }
        if (isNightmareAddList == false)
        {
            if (DataBaseManager.intelNightmare1 == true || DataBaseManager.intelNightmare2 == true || DataBaseManager.intelNightmare3 == true || DataBaseManager.intelNightmare4 == true || DataBaseManager.intelNightmare5 == true || DataBaseManager.intelNightmare6 == true)
            {
                isNightmareAddList = true;
                upperEventKeywordList.Add("Nightmare");
            }
        }
        if (isInsomniaAddList == false)
        {
            if (DataBaseManager.intelInsomnia1 == true || DataBaseManager.intelInsomnia2 == true || DataBaseManager.intelInsomnia3 == true || DataBaseManager.intelInsomnia4 == true || DataBaseManager.intelInsomnia5 == true || DataBaseManager.intelInsomnia6 == true)
            {
                isInsomniaAddList = true;
                upperEventKeywordList.Add("Insomnia");
            }
        }
        if (isParanoiaAddList == false)
        {
            if (DataBaseManager.intelParanoia1 == true || DataBaseManager.intelParanoia2 == true || DataBaseManager.intelParanoia3 == true || DataBaseManager.intelParanoia4 == true || DataBaseManager.intelParanoia5 == true || DataBaseManager.intelParanoia6 == true)
            {
                isParanoiaAddList = true;
                upperEventKeywordList.Add("Paranoia");
            }
        }
        if (isMissingPeopleAddList == false)
        {
            if (DataBaseManager.intelMissingPeople1 == true || DataBaseManager.intelMissingPeople2 == true || DataBaseManager.intelMissingPeople3 == true || DataBaseManager.intelMissingPeople4 == true || DataBaseManager.intelMissingPeople5 == true || DataBaseManager.intelMissingPeople6 == true)
            {
                isMissingPeopleAddList = true;
                upperEventKeywordList.Add("MissingPeople");
            }
        }
        if (isSewerGhostStoryAddList == false)
        {
            if (DataBaseManager.intelSewerGhostStory1 == true || DataBaseManager.intelSewerGhostStory2 == true || DataBaseManager.intelSewerGhostStory3 == true || DataBaseManager.intelSewerGhostStory4 == true || DataBaseManager.intelSewerGhostStory5 == true || DataBaseManager.intelSewerGhostStory6 == true)
            {
                isSewerGhostStoryAddList = true;
                upperEventKeywordList.Add("SewerGhostStory");
            }
        }
        if (isFishySmellAddList == false)
        {
            if (DataBaseManager.intelFishySmell1 == true || DataBaseManager.intelFishySmell2 == true || DataBaseManager.intelFishySmell3 == true || DataBaseManager.intelFishySmell4 == true || DataBaseManager.intelFishySmell5 == true || DataBaseManager.intelFishySmell6 == true)
            {
                isFishySmellAddList = true;
                upperEventKeywordList.Add("FishySmell");
            }
        }
        if (isRiverWaterQualityAddList == false)
        {
            if (DataBaseManager.intelRiverWaterQuality1 == true || DataBaseManager.intelRiverWaterQuality2 == true || DataBaseManager.intelRiverWaterQuality3 == true || DataBaseManager.intelRiverWaterQuality4 == true || DataBaseManager.intelRiverWaterQuality5 == true || DataBaseManager.intelRiverWaterQuality6 == true)
            {
                isRiverWaterQualityAddList = true;
                upperEventKeywordList.Add("RiverWaterQuality");
            }
        }
        if (isCreepyEyesAddList == false)
        {
            if (DataBaseManager.intelCreepyEyes1 == true || DataBaseManager.intelCreepyEyes2 == true || DataBaseManager.intelCreepyEyes3 == true || DataBaseManager.intelCreepyEyes4 == true || DataBaseManager.intelCreepyEyes5 == true || DataBaseManager.intelCreepyEyes6 == true)
            {
                isCreepyEyesAddList = true;
                upperEventKeywordList.Add("CreepyEyes");
            }
        }
        if (isMurderCaseAddList == false)
        {
            if (DataBaseManager.intelMurderCase1 == true || DataBaseManager.intelMurderCase2 == true || DataBaseManager.intelMurderCase3 == true || DataBaseManager.intelMurderCase4 == true || DataBaseManager.intelMurderCase5 == true || DataBaseManager.intelMurderCase6 == true)
            {
                isMurderCaseAddList = true;
                upperEventKeywordList.Add("MurderCase");
            }
        }
        if (isSafeAddList == false)
        {
            if (DataBaseManager.intelSafe1 == true || DataBaseManager.intelSafe2 == true || DataBaseManager.intelSafe3 == true || DataBaseManager.intelSafe4 == true || DataBaseManager.intelSafe5 == true || DataBaseManager.intelSafe6 == true)
            {
                isSafeAddList = true;
                upperEventKeywordList.Add("Safe");
                Debug.Log(upperEventKeywordList);
            }
        }
        //장소
        if (isHospitalAddList == false)
        {
            if (DataBaseManager.intelHospital1 == true || DataBaseManager.intelHospital2 == true || DataBaseManager.intelHospital3 == true || DataBaseManager.intelHospital4 == true || DataBaseManager.intelHospital5 == true || DataBaseManager.intelHospital6 == true)
            {
                isHospitalAddList = true;
                upperPlaceKeywordList.Add("Hospital");
            }
        }
        if (isQuestHouseAddList == false)
        {
            if (DataBaseManager.intelQuestHouse1 == true || DataBaseManager.intelQuestHouse2 == true || DataBaseManager.intelQuestHouse3 == true || DataBaseManager.intelQuestHouse4 == true || DataBaseManager.intelQuestHouse5 == true || DataBaseManager.intelQuestHouse6 == true)
            {
                isQuestHouseAddList = true;
                upperPlaceKeywordList.Add("Client'shouse");
            }
        }
        if (isNewspaperAddList == false)
        {
            if (DataBaseManager.intelNewspaper1 == true || DataBaseManager.intelNewspaper2 == true || DataBaseManager.intelNewspaper3 == true || DataBaseManager.intelNewspaper4 == true || DataBaseManager.intelNewspaper5 == true || DataBaseManager.intelNewspaper6 == true)
            {
                isNewspaperAddList = true;
                upperPlaceKeywordList.Add("DailyNews");
            }
        }
        if (isRiversideAddList == false)
        {
            if (DataBaseManager.intelRiverside1 == true || DataBaseManager.intelRiverside2 == true || DataBaseManager.intelRiverside3 == true || DataBaseManager.intelRiverside4 == true || DataBaseManager.intelRiverside5 == true || DataBaseManager.intelRiverside6 == true)
            {
                isRiversideAddList = true;
                upperPlaceKeywordList.Add("Riverside");
            }
        }
        if (isSewageMaintenanceOfficeAddList == false)
        {
            if (DataBaseManager.intelSewageMaintenanceOffice1 == true || DataBaseManager.intelSewageMaintenanceOffice2 == true || DataBaseManager.intelSewageMaintenanceOffice3 == true || DataBaseManager.intelSewageMaintenanceOffice4 == true || DataBaseManager.intelSewageMaintenanceOffice5 == true || DataBaseManager.intelSewageMaintenanceOffice6 == true)
            {
                isSewageMaintenanceOfficeAddList = true;
                upperPlaceKeywordList.Add("Sewage MA Office");
            }
        }
        if (isSewerAddList == false)
        {
            if (DataBaseManager.intelSewer1 == true || DataBaseManager.intelSewer2 == true || DataBaseManager.intelSewer3 == true || DataBaseManager.intelSewer4 == true || DataBaseManager.intelSewer5 == true || DataBaseManager.intelSewer6 == true)
            {
                isSewerAddList = true;
                upperPlaceKeywordList.Add("Sewer");
            }
        }
        if (isWharfAddList == false)
        {
            if (DataBaseManager.intelWharf1 == true || DataBaseManager.intelWharf2 == true || DataBaseManager.intelWharf3 == true || DataBaseManager.intelWharf4 == true || DataBaseManager.intelWharf5 == true || DataBaseManager.intelWharf6 == true)
            {
                isWharfAddList = true;
                upperPlaceKeywordList.Add("Wharf");
            }
        }
        if (isUniversityAddList == false)
        {
            if (DataBaseManager.intelUniversity1 == true || DataBaseManager.intelUniversity2 == true || DataBaseManager.intelUniversity3 == true || DataBaseManager.intelUniversity4 == true || DataBaseManager.intelUniversity5 == true || DataBaseManager.intelUniversity6 == true)
            {
                isUniversityAddList = true;
                upperPlaceKeywordList.Add("Univ of Miscatonic");
            }
        }

        //인물
        if (isAidenAddList == false)
        {
            if (DataBaseManager.intelAiden1 == true || DataBaseManager.intelAiden2 == true || DataBaseManager.intelAiden3 == true || DataBaseManager.intelAiden4 == true || DataBaseManager.intelAiden5 == true || DataBaseManager.intelAiden6 == true)
            {
                isAidenAddList = true;
                upperCharacterKewordList.Add("Aiden Triss");
            }
        }
        if (isEllaAddList == false)
        {
            if (DataBaseManager.intelElla1 == true || DataBaseManager.intelElla2 == true || DataBaseManager.intelElla3 == true || DataBaseManager.intelElla4 == true || DataBaseManager.intelElla5 == true || DataBaseManager.intelElla6 == true)
            {
                isEllaAddList = true;
                upperCharacterKewordList.Add("Ella Triss");
            }
        }
        if (isSwainAddList == false)
        {
            if (DataBaseManager.intelSwain1 == true || DataBaseManager.intelSwain2 == true || DataBaseManager.intelSwain3 == true || DataBaseManager.intelSwain4 == true || DataBaseManager.intelSwain5 == true || DataBaseManager.intelSwain6 == true)
            {
                isSwainAddList = true;
                upperCharacterKewordList.Add("Berkeley Swain");
            }
        }
        if (isCaneAddList == false)
        {
            if (DataBaseManager.intelCane1 == true || DataBaseManager.intelCane2 == true || DataBaseManager.intelCane3 == true || DataBaseManager.intelCane5 == true || DataBaseManager.intelCane4 == true || DataBaseManager.intelCane6 == true)
            {
                isCaneAddList = true;
                upperCharacterKewordList.Add("Osborn Louis Kane");
            }
        }
        if (isMeivAddList == false)
        {
            if (DataBaseManager.intelMeiv1 == true || DataBaseManager.intelMeiv2 == true || DataBaseManager.intelMeiv3 == true || DataBaseManager.intelMeiv4 == true || DataBaseManager.intelMeiv5 == true || DataBaseManager.intelMeiv6 == true)
            {
                isMeivAddList = true;
                upperCharacterKewordList.Add("Maeve A Rossi");
            }
        }
        if (isAlberAddList == false)
        {
            if (DataBaseManager.intelAlbert1 == true || DataBaseManager.intelAlbert2 == true || DataBaseManager.intelAlbert3 == true || DataBaseManager.intelAlbert4 == true || DataBaseManager.intelAlbert5 == true || DataBaseManager.intelAlbert6 == true)
            {
                isAlberAddList = true;
                upperCharacterKewordList.Add("Albert Bradley");
            }
        }
        if (isSewerWorkerAddList == false)
        {
            if (DataBaseManager.intelSewerWorker1 == true || DataBaseManager.intelSewerWorker2 == true || DataBaseManager.intelSewerWorker3 == true || DataBaseManager.intelSewerWorker4 == true || DataBaseManager.intelSewerWorker5 == true || DataBaseManager.intelSewerWorker6 == true)
            {
                isSewerWorkerAddList = true;
                upperCharacterKewordList.Add("SewerWorker");
            }
        }
        if (isDaveAddList == false)
        {
            if (DataBaseManager.intelDave1 == true || DataBaseManager.intelDave2 == true || DataBaseManager.intelDave3 == true || DataBaseManager.intelDave4 == true || DataBaseManager.intelDave5 == true || DataBaseManager.intelDave6 == true)
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
    public void DownKeywordUpdater()
    {

        planetaryParadeKeywordCount = planetaryParadeList.Count - 1;
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
        if (DataBaseManager.keywordUpper == "PlanetaryParade" && planetaryParadeKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = planetaryParadeList[planetaryParadeKeywordNum];
        }
        if (DataBaseManager.intelPlanetaryParade1 == true && isPlanetaryParade1Add == false)
        {
            isPlanetaryParade1Add = true;
            planetaryParadeList.Add("Contents of a Newspaper");
        }
        if (DataBaseManager.intelPlanetaryParade2 == true && isPlanetaryParade2Add == false)
        {
            isPlanetaryParade2Add = true;
            planetaryParadeList.Add("Meetings");
        }
        // 악몽
        else if (DataBaseManager.keywordUpper == "Nightmare" && nightmareKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = nightmareList[nightmareKeywordNum];
        }
        if (DataBaseManager.intelNightmare1 == true && isNightmare1Add == false)
        {
            isNightmare1Add = true;
            nightmareList.Add("Aiden's Nightmare");
        }
        if (DataBaseManager.intelNightmare2 == true && isNightmare2Add == false)
        {
            isNightmare2Add = true;
            nightmareList.Add("Content of a Nightmare");
        }
        if (DataBaseManager.intelNightmare3 == true && isNightmare3Add == false)
        {
            isNightmare3Add = true;
            nightmareList.Add("Aiden's Detail");
        }
        if (DataBaseManager.intelNightmare4 == true && isNightmare4Add == false)
        {
            isNightmare4Add = true;
            nightmareList.Add("Water and Nightmare");
        }
        // 불면증
        else if (DataBaseManager.keywordUpper == "Insomnia" && insomniaKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = insomniaList[insomniaKeywordNum];
        }
        if (DataBaseManager.intelInsomnia1 == true && isInsomnia1Add == false)
        {
            isInsomnia1Add = true;
            insomniaList.Add("Newspaper Article");
        }
        if (DataBaseManager.intelInsomnia2 == true && isInsomnia2Add == false)
        {
            isInsomnia2Add = true;
            insomniaList.Add("Doctor's opinion");
        }
        if (DataBaseManager.intelInsomnia3 == true && isInsomnia3Add == false)
        {
            isInsomnia3Add = true;
            // InsomniaList.Add("Cause of Insomnia");
        }
        // 편집증
        else if (DataBaseManager.keywordUpper == "Paranoia" && paranoiaKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = paranoiaList[paranoiaKeywordNum];
        }
        if (DataBaseManager.intelParanoia1 == true && isParanoia1Add == false)
        {
            isParanoia1Add = true;
            paranoiaList.Add("Husband's Condition");
        }
        if (DataBaseManager.intelParanoia2 == true && isParanoia2Add == false)
        {
            isParanoia2Add = true;
            paranoiaList.Add("Cause of Paranoia");
        }
        // 실종사건
        else if (DataBaseManager.keywordUpper == "MissingPeople" && missingPeopleKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = missingPeopleList[missingPeopleKeywordNum];
        }
        if (DataBaseManager.intelMissingPeople1 == true && isMissingPeople1Add == false)
        {
            isMissingPeople1Add = true;
            missingPeopleList.Add("Albert's Information");
        }
        if (DataBaseManager.intelMissingPeople2 == true && isMissingPeople2Add == false)
        {
            isMissingPeople2Add = true;
            missingPeopleList.Add("Reporting by Swain");
        }
        if (DataBaseManager.intelMissingPeople3 == true && isMissingPeople3Add == false)
        {
            isMissingPeople3Add = true;
            missingPeopleList.Add("Kidnap");
        }
        // 하수도괴담.
        else if (DataBaseManager.keywordUpper == "SewerGhostStory" && sewerGhostStoryKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = sewerGhostStoryList[sewerGhostStoryKeywordNum];
        }

        if (DataBaseManager.intelSewerGhostStory1 == true && isSewerGhostStory1Add == false)
        {
            isSewerGhostStory1Add = true;
            sewerGhostStoryList.Add("Contents of a GhostStory");
        }
        if (DataBaseManager.intelSewerGhostStory2 == true && isSewerGhostStory2Add == false)
        {
            isSewerGhostStory2Add = true;
            sewerGhostStoryList.Add("Truth of a rumor");
        }
        // 비릿한냄새
        else if (DataBaseManager.keywordUpper == "FishySmell" && fishySmellKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = fishySmellList[fishySmellKeywordNum];
        }

        if (DataBaseManager.intelFishySmell1 == true && isFishySmell1Add == false)
        {
            isFishySmell1Add = true;
            fishySmellList.Add("Rumor");
        }
        if (DataBaseManager.intelFishySmell2 == true && isFishySmell2Add == false)
        {
            isFishySmell2Add = true;
            fishySmellList.Add("Source of the smell?");
        }
        if (DataBaseManager.intelFishySmell3 == true && isFishySmell3Add == false)
        {
            isFishySmell3Add = true;
            fishySmellList.Add("Source speculation");
        }
        if (DataBaseManager.intelFishySmell4 == true && isFishySmell4Add == false)
        {
            isFishySmell4Add = true;
            fishySmellList.Add("Sewer & FishySmell");
        }
        // 강의수질
        else if (DataBaseManager.keywordUpper == "RiverWaterQuality" && riverWaterQualityKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = riverWaterQualityList[riverWaterQualityKeywordNum];
        }

        if (DataBaseManager.intelRiverWaterQuality1 == true && isRiverWaterQuality1Add == false)
        {
            isRiverWaterQuality1Add = true;
            riverWaterQualityList.Add("Strange Point");
        }
        if (DataBaseManager.intelRiverWaterQuality2 == true && isRiverWaterQuality2Add == false)
        {
            isRiverWaterQuality2Add = true;
            riverWaterQualityList.Add("Details");
        }
        if (DataBaseManager.intelRiverWaterQuality3 == true && isRiverWaterQuality3Add == false)
        {
            isRiverWaterQuality3Add = true;
            riverWaterQualityList.Add("Fishy Slime");
        }
        // 섬뜩한눈
        else if (DataBaseManager.keywordUpper == "CreepyEyes" && creepyEyesKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = creepyEyesList[creepyEyesKeywordNum];
        }
        if (DataBaseManager.intelCreepyEyes1 == true && isCreepyEyes1Add == false)
        {
            isCreepyEyes1Add = true;
            creepyEyesList.Add("Saw in the sewer");
        }
        // 살인사건
        else if (DataBaseManager.keywordUpper == "MurderCase" && murderCaseKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = murderCaseList[murderCaseKeywordNum];
        }
        if (DataBaseManager.intelMurderCase1 == true && isMurderCase1Add == false)
        {
            isMurderCase1Add = true;
            murderCaseList.Add("Witness Testimony");
        }
        if (DataBaseManager.intelMurderCase2 == true && isMurderCase2Add == false)
        {
            isMurderCase2Add = true;
            murderCaseList.Add("Missing People & Fishy Smell");
        }
        if (DataBaseManager.intelMurderCase3 == true && isMurderCase3Add == false)
        {
            isMurderCase3Add = true;
            murderCaseList.Add("The Culprit");
        }
        if (DataBaseManager.intelMurderCase4 == true && isMurderCase4Add == false)
        {
            isMurderCase4Add = true;
            murderCaseList.Add("Cause of Death");
        }
        if (DataBaseManager.intelMurderCase5 == true && isMurderCase5Add == false)
        {
            isMurderCase5Add = true;
            murderCaseList.Add("Webbed Feet");
        }
        if (DataBaseManager.intelMurderCase6 == true && isMurderCase6Add == false)
        {
            isMurderCase6Add = true;
            murderCaseList.Add("Way to Escape");
        }
        // 금고
        else if (DataBaseManager.keywordUpper == "Safe" && safeKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = safeList[safeKeywordNum];
        }
        if (DataBaseManager.intelSafe1 == true && isSafe1Add == false)
        {
            isSafe1Add = true;
            safeList.Add("Aiden's Safe");
        }
        if (DataBaseManager.intelSafe2 == true && isSafe2Add == false)
        {
            isSafe2Add = true;
            safeList.Add("Acquiring a Safe");
        }
        // 인물
        // 에이든
        if (DataBaseManager.keywordUpper == "Aiden Triss" && aidenKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = aidenList[aidenKeywordNum];
        }
        if (DataBaseManager.intelAiden1 == true && isAiden1Add == false)
        {
            isAiden1Add = true;
            aidenList.Add("Subject to request");
        }
        if (DataBaseManager.intelAiden2 == true && isAiden2Add == false)
        {
            isAiden2Add = true;
            aidenList.Add("Aiden & Pandemic");
        }
        if (DataBaseManager.intelAiden3 == true && isAiden3Add == false)
        {
            isAiden3Add = true;
            aidenList.Add("Missing Subject");
        }
        // 엘라
        if (DataBaseManager.keywordUpper == "Ella Triss" && ellaKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = ellaList[ellaKeywordNum];
        }
        if (DataBaseManager.intelElla1 == true && isElla1Add == false)
        {
            isElla1Add = true;
            ellaList.Add("Client");
        }
        if (DataBaseManager.intelElla2 == true && isElla2Add == false)
        {
            isElla2Add = true;
            ellaList.Add("Murdered Clients");
        }
        // 스탠리
        if (DataBaseManager.keywordUpper == "Berkeley Swain" && swainKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = swainList[swainKeywordNum];
        }
        if (DataBaseManager.intelSwain1 == true && isSwain1Add == false)
        {
            isSwain1Add = true;
            swainList.Add("Sewer Survey");
        }
        // 노교수
        if (DataBaseManager.keywordUpper == "노교수" && caneKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = caneList[caneKeywordNum];
        }
        if (DataBaseManager.intelCane1 == true && isCane1Add == false)
        {
            isCane1Add = true;
            caneList.Add("신이난 노교수");
        }
        // 메이브
        if (DataBaseManager.keywordUpper == "Maeve A Rossi" && meivKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = meivList[meivKeywordNum];
        }
        if (DataBaseManager.intelMeiv1 == true && isMeiv1Add == false)
        {
            isMeiv1Add = true;
            meivList.Add("Mafia friend");
        }
        if (DataBaseManager.intelMeiv2 == true && isMeiv2Add == false)
        {
            isMeiv2Add = true;
            meivList.Add("Promise");
        }
        // 알버트
        if (DataBaseManager.keywordUpper == "Albert Bradley" && albertKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = albertList[albertKeywordNum];
        }
        if (DataBaseManager.intelAlbert1 == true && isAlbert1Add == false)
        {
            isAlbert1Add = true;
            albertList.Add("Police friend");
        }
        if (DataBaseManager.keywordUpper == "SewerWorker" && sewerWorkerKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = sewerWorkerList[sewerWorkerKeywordNum];
        }
        if (DataBaseManager.intelSewerWorker1 == true && isSewerWorker1Add == false)
        {
            isSewerWorker1Add = true;
            sewerWorkerList.Add("Owner of Eye?");
        }
        if (DataBaseManager.intelSewerWorker2 == true && isSewerWorker2Add == false)
        {
            isSewerWorker2Add = true;
            sewerWorkerList.Add("Unfair dismissal");
        }
        if (DataBaseManager.intelSewerWorker3 == true && isSewerWorker3Add == false)
        {
            isSewerWorker3Add = true;
            sewerWorkerList.Add("Cause of Fishy smell");
        }
        if (DataBaseManager.intelSewerWorker4 == true && isSewerWorker4Add == false)
        {
            isSewerWorker4Add = true;
            sewerWorkerList.Add("Owner of Uniform");
        }
        if (DataBaseManager.keywordUpper == "Dave" && daveKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = daveList[daveKeywordNum];
        }
        if (DataBaseManager.intelDave1 == true && isDave1Add == false)
        {
            isDave1Add = true;
            daveList.Add("Bracelet");
        }

        // 장소 
        // 병원
        if (DataBaseManager.keywordUpper == "Hospital" && hospitalKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = hospitalList[hospitalKeywordNum];
        }
        if (DataBaseManager.intelHospital1 == true && isHospital1Add == false)
        {
            isHospital1Add = true;
            hospitalList.Add("State of congestion");
        }
        // 신문사
        if (DataBaseManager.keywordUpper == "DailyNews" && newspaperKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = newspaperList[newspaperKeywordNum];
        }
        if (DataBaseManager.intelNewspaper1 == true && isNewspaper1Add == false)
        {
            isNewspaper1Add = true;
            newspaperList.Add("Husband's job");
        }
        // Client'shouse
        if (DataBaseManager.keywordUpper == "Client'shouse" && questHouseKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = questHouseList[questHouseKeywordNum];
        }
        if (DataBaseManager.intelQuestHouse1 == true && isQuestHouse1Add == false)
        {
            isQuestHouse1Add = true;
            questHouseList.Add("Adress");
        }
        // 강가
        if (DataBaseManager.keywordUpper == "미스캐토닉 강" && riversideKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = riversideList[riversideKeywordNum];
        }
        if (DataBaseManager.intelRiverside1 == true && isRiverside1Add == false)
        {
            isRiverside1Add = true;
            riversideList.Add("강과 교수");
        }
        // 하수정비사무소
        if (DataBaseManager.keywordUpper == "Sewage MA Office" && sewageMaintenanceOfficeKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = sewageMaintenanceOfficeList[sewageMaintenanceOfficeKeywordNum];
        }
        if (DataBaseManager.intelSewageMaintenanceOffice1 == true && isSewageMaintenanceOffice1Add == false)
        {
            isSewageMaintenanceOffice1Add = true;
            sewageMaintenanceOfficeList.Add("Location");
        }
        // 하수도
        if (DataBaseManager.keywordUpper == "Sewer" && sewerKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = sewerList[sewerKeywordNum];
        }
        if (DataBaseManager.intelSewer1 == true && isSewer1Add == false)
        {
            isSewer1Add = true;
            sewerList.Add("Aiden's warning");
        }
        if (DataBaseManager.intelSewer2 == true && isSewer2Add == false)
        {
            isSewer2Add = true;
            sewerList.Add("Root of problem");
        }
        if (DataBaseManager.intelSewer3 == true && isSewer3Add == false)
        {
            isSewer3Add = true;
            sewerList.Add("Manhole");
        }
        if (DataBaseManager.intelSewer4 == true && isSewer4Add == false)
        {
            isSewer4Add = true;
            sewerList.Add("Advent");
        }
        // 부두
        if (DataBaseManager.keywordUpper == "Wharf" && wharfKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = wharfList[wharfKeywordNum];
        }
        if (DataBaseManager.intelWharf1 == true && isWharf1Add == false)
        {
            isWharf1Add = true;
            wharfList.Add("Work");
        }
        // 대학
        if (DataBaseManager.keywordUpper == "Univ of Miscatonic" && universityKeywordCount >= 0)
        {
            DataBaseManager.keywordDowner = universityList[universityKeywordNum];
        }
        if (DataBaseManager.intelUniversity1 == true && isUniversity1Add == false)
        {
            isUniversity1Add = true;
            universityList.Add("Astronomical show");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DataBaseManager.keywordMain = "Character";
        DataBaseManager.keywordUpper = "Maeve A Rossi";
        DataBaseManager.keywordDowner = "Mafia friend";
    }

    // Update is called once per frame
    void Update()
    {
        DownKeywordUpdater();
        KeywordUpdate();
    }
    public GameObject daveIntel1;
    public GameObject daveIntel2;
    public GameObject daveIntel3;
    public GameObject daveIntel4;
    public GameObject daveIntel5;
    public GameObject daveIntel6;
}
