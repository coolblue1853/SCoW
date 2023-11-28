using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KeywordButton : MonoBehaviour
{
    string input;
    string subjectUpper;
    string subjectDetail;
    int indexUpper;
    int indexDetail;
    public void SeparateString()
    {
        SoundManager.Instance.ClickSound_Play();
        input = this.transform.name;

        string[] parts = input.Split('_');
        if (parts.Length == 2)
        {
            subjectUpper = parts[0];
            subjectDetail = parts[1];
        }
        // 키워드 - EVENT
        if (subjectUpper == "PlanetaryParade")
        {
            DataBaseManager.keywordMain = "Event";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperEventKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.planetaryParadeList, subjectDetail);
            KeyWordManager.Instance.planetaryParadeKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.planetaryParadeList[indexDetail];
        }
        if (subjectUpper == "Paranoia")
        {
            DataBaseManager.keywordMain = "Event";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperEventKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.paranoiaList, subjectDetail);
            KeyWordManager.Instance.paranoiaKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.paranoiaList[indexDetail];
        }
        if (subjectUpper == "Insomnia")
        {
            DataBaseManager.keywordMain = "Event";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperEventKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.insomniaList, subjectDetail);
            KeyWordManager.Instance.insomniaKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.insomniaList[indexDetail];
        }
        if (subjectUpper == "Nightmare")
        {
            DataBaseManager.keywordMain = "Event";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperEventKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.nightmareList, subjectDetail);
            KeyWordManager.Instance.nightmareKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.nightmareList[indexDetail];
        }
        if (subjectUpper == "FishySmell")
        {
            DataBaseManager.keywordMain = "Event";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperEventKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.fishySmellList, subjectDetail);
            KeyWordManager.Instance.fishySmellKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.fishySmellList[indexDetail];
        }
        if (subjectUpper == "MissingPeople")
        {
            DataBaseManager.keywordMain = "Event";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperEventKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.missingPeopleList, subjectDetail);
            KeyWordManager.Instance.missingPeopleKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.missingPeopleList[indexDetail];
        }
        if (subjectUpper == "SewerGhostStory")
        {
            DataBaseManager.keywordMain = "Event";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperEventKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.sewerGhostStoryList, subjectDetail);
            KeyWordManager.Instance.sewerGhostStoryKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.sewerGhostStoryList[indexDetail];
        }
        if (subjectUpper == "Safe")
        {
            DataBaseManager.keywordMain = "Event";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperEventKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.safeList, subjectDetail);
            KeyWordManager.Instance.safeKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.safeList[indexDetail];
        }
        if (subjectUpper == "CreepyEyes")
        {
            DataBaseManager.keywordMain = "Event";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperEventKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.creepyEyesList, subjectDetail);
            KeyWordManager.Instance.creepyEyesKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.creepyEyesList[indexDetail];
        }
        if (subjectUpper == "RiverWaterQuality")
        {
            DataBaseManager.keywordMain = "Event";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperEventKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.riverWaterQualityList, subjectDetail);
            KeyWordManager.Instance.riverWaterQualityKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.riverWaterQualityList[indexDetail];
        }
        if (subjectUpper == "MurderCase")
        {
            DataBaseManager.keywordMain = "Event";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperEventKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.murderCaseList, subjectDetail);
            KeyWordManager.Instance.murderCaseKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.murderCaseList[indexDetail];
        }

        // 키워드 - PLACE
        if (subjectUpper == "Wharf")
        {
            DataBaseManager.keywordMain = "Place";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperPlaceKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.wharfList, subjectDetail);
            KeyWordManager.Instance.wharfKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.wharfList[indexDetail];
        }
        if (subjectUpper == "QuestHouse")
        {
            subjectUpper = "Client'shouse";
            DataBaseManager.keywordMain = "Place";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperPlaceKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.questHouseList, subjectDetail);
            KeyWordManager.Instance.questHouseKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.questHouseList[indexDetail];
        }
        if (subjectUpper == "DailyNews")
        {
            DataBaseManager.keywordMain = "Place";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperPlaceKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.newspaperList, subjectDetail);
            KeyWordManager.Instance.newspaperKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.newspaperList[indexDetail];
        }
        if (subjectUpper == "Hospital")
        {
            DataBaseManager.keywordMain = "Place";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperPlaceKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.hospitalList, subjectDetail);
            KeyWordManager.Instance.hospitalKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.hospitalList[indexDetail];
        }
        if (subjectUpper == "Univ of Miscatonic")
        {
            DataBaseManager.keywordMain = "Place";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperPlaceKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.universityList, subjectDetail);
            KeyWordManager.Instance.universityKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.universityList[indexDetail];
        }
        if (subjectUpper == "Sewage MA Office")
        {
            DataBaseManager.keywordMain = "Place";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperPlaceKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.sewageMaintenanceOfficeList, subjectDetail);
            KeyWordManager.Instance.sewageMaintenanceOfficeKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.sewageMaintenanceOfficeList[indexDetail];
        }
        if (subjectUpper == "Sewer")
        {
            DataBaseManager.keywordMain = "Place";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperPlaceKeywordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.sewerList, subjectDetail);
            KeyWordManager.Instance.sewerKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.sewerList[indexDetail];
        }

        // 키워드 - Character
        if (subjectUpper == "Ella Triss")
        {
            DataBaseManager.keywordMain = "Character";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperCharacterKewordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.ellaList, subjectDetail);
            KeyWordManager.Instance.ellaKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.ellaList[indexDetail];
        }
        if (subjectUpper == "Aiden Triss")
        {
            DataBaseManager.keywordMain = "Character";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperCharacterKewordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.aidenList, subjectDetail);
            KeyWordManager.Instance.aidenKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.aidenList[indexDetail];
        }
        if (subjectUpper == "Albert Bradley")
        {
            DataBaseManager.keywordMain = "Character";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperCharacterKewordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.albertList, subjectDetail);
            KeyWordManager.Instance.albertKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.albertList[indexDetail];
        }
        if (subjectUpper == "Maeve A Rossi")
        {
            DataBaseManager.keywordMain = "Character";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperCharacterKewordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.meivList, subjectDetail);
            KeyWordManager.Instance.meivKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.meivList[indexDetail];
        }
        if (subjectUpper == "SewerWorker")
        {
            DataBaseManager.keywordMain = "Character";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperCharacterKewordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.sewerWorkerList, subjectDetail);
            KeyWordManager.Instance.sewerWorkerKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.sewerWorkerList[indexDetail];
        }
        if (subjectUpper == "Dave")
        {
            DataBaseManager.keywordMain = "Character";
            indexUpper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, subjectUpper);
            DataBaseManager.keywordUpper = KeyWordManager.Instance.upperCharacterKewordList[indexUpper];
            indexDetail = FindStringIndex(KeyWordManager.Instance.daveList, subjectDetail);
            KeyWordManager.Instance.daveKeywordNum = indexDetail;
            DataBaseManager.keywordDowner = KeyWordManager.Instance.daveList[indexDetail];
        }
    }
    int FindStringIndex(List<string> list, string target)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == target)
            {
                return i; // 해당 문자열을 찾으면 인덱스 반환
            }
        }
        return -1; // 해당 문자열이 리스트에 없을 경우 -1 반환
    }
}
