using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordButton : MonoBehaviour
{
    string input;
    string A;
    string B;

    int index_upper;
    int index_down;
    public void SeparateString()
    {
        SoundManager.Instance.ClickSound_Play();
        input = this.transform.name;


        string[] parts = input.Split('_');
        if (parts.Length == 2)
        {
            A = parts[0];
            B = parts[1];
        }

        // 키워드 - EVENT
        if (A == "PlanetaryParade")
        {
            DataBaseManager.keyword_main = "Event";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];


            index_down = FindStringIndex(KeyWordManager.Instance.planetaryParadeList, B);
            KeyWordManager.Instance.planetaryParadeKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.planetaryParadeList[index_down];
        }
        if (A == "Paranoia")
        {
            DataBaseManager.keyword_main = "Event";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.paranoiaList, B);
            KeyWordManager.Instance.paranoiaKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.paranoiaList[index_down];
        }
        if (A == "Insomnia")
        {
            DataBaseManager.keyword_main = "Event";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.insomniaList, B);
            KeyWordManager.Instance.insomniaKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.insomniaList[index_down];
        }
        if (A == "Nightmare")
        {
            DataBaseManager.keyword_main = "Event";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.nightmareList, B);
            KeyWordManager.Instance.nightmareKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.nightmareList[index_down];
        }
        if (A == "FishySmell")
        {
            DataBaseManager.keyword_main = "Event";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.fishySmellList, B);
            KeyWordManager.Instance.fishySmellKeywordNum = index_down;
           DataBaseManager.keyword_downer = KeyWordManager.Instance.fishySmellList[index_down];
            
        }
        if (A == "MissingPeople")
        {
            DataBaseManager.keyword_main = "Event";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.missingPeopleList, B);
            KeyWordManager.Instance.missingPeopleKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.missingPeopleList[index_down];
        }
        if (A == "SewerGhostStory")
        {
            DataBaseManager.keyword_main = "Event";

            index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.sewerGhostStoryList, B);
            KeyWordManager.Instance.sewerGhostStoryKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.sewerGhostStoryList[index_down];
        }
        if (A == "Safe")
        {
            DataBaseManager.keyword_main = "Event";

            index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.safeList, B);
            KeyWordManager.Instance.safeKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.safeList[index_down];
        }
        if (A == "CreepyEyes")
        {
            DataBaseManager.keyword_main = "Event";

            index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.creepyEyesList, B);
            KeyWordManager.Instance.creepyEyesKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.creepyEyesList[index_down];
        }
        if (A == "RiverWaterQuality")
        {
            DataBaseManager.keyword_main = "Event";

            index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.riverWaterQualityList, B);
            KeyWordManager.Instance.riverWaterQualityKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.riverWaterQualityList[index_down];
        }
        if (A == "MurderCase")
        {
            DataBaseManager.keyword_main = "Event";

            index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.murderCaseList, B);
            KeyWordManager.Instance.murderCaseKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.murderCaseList[index_down];
        }

        // 키워드 - PLACE
        if (A == "Wharf")
        {
            DataBaseManager.keyword_main = "Place";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.wharfList, B);
            KeyWordManager.Instance.wharfKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.wharfList[index_down];
        }
        if (A == "QuestHouse")
        {
            A = "Client'shouse";
            DataBaseManager.keyword_main = "Place";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.questHouseList, B);
            KeyWordManager.Instance.questHouseKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.questHouseList[index_down];
        }
        if (A == "DailyNews")
        {
            DataBaseManager.keyword_main = "Place";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.newspaperList, B);
            KeyWordManager.Instance.newspaperKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.newspaperList[index_down];
        }
        if (A == "Hospital")
        {
            DataBaseManager.keyword_main = "Place";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.hospitalList, B);
            KeyWordManager.Instance.hospitalKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.hospitalList[index_down];
        }
        if (A == "Univ of Miscatonic")
        {
            DataBaseManager.keyword_main = "Place";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.universityList, B);
            KeyWordManager.Instance.universityKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.universityList[index_down];
        }
        if (A == "Sewage MA Office")
        {
            DataBaseManager.keyword_main = "Place";

            index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.sewageMaintenanceOfficeList, B);
            KeyWordManager.Instance.sewageMaintenanceOfficeKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.sewageMaintenanceOfficeList[index_down];
        }
        if (A == "Sewer")
        {
            DataBaseManager.keyword_main = "Place";

            index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.sewerList, B);
            KeyWordManager.Instance.sewerKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.sewerList[index_down];
        }

        // 키워드 - Character
        if (A == "Ella Triss")
        {
            DataBaseManager.keyword_main = "Character";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperCharacterKewordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.ellaList, B);
            KeyWordManager.Instance.ellaKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.ellaList[index_down];
        }
        if (A == "Aiden Triss")
        {
            DataBaseManager.keyword_main = "Character";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperCharacterKewordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.aidenList, B);
            KeyWordManager.Instance.aidenKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.aidenList[index_down];
        }
        if (A == "Albert Bradley")
        {
            DataBaseManager.keyword_main = "Character";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperCharacterKewordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.albertList, B);
            KeyWordManager.Instance.albertKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.albertList[index_down];
        }
        if (A == "Maeve A Rossi")
        {
            DataBaseManager.keyword_main = "Character";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperCharacterKewordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.meivList, B);
            KeyWordManager.Instance.meivKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.meivList[index_down];
        }

        if (A == "SewerWorker")
        {
            DataBaseManager.keyword_main = "Character";

            index_upper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperCharacterKewordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.sewerWorkerList, B);
            KeyWordManager.Instance.sewerWorkerKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.sewerWorkerList[index_down];
        }
        if (A == "Dave")
        {
            DataBaseManager.keyword_main = "Character";

            index_upper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperCharacterKewordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.daveList, B);
            KeyWordManager.Instance.daveKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.daveList[index_down];
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
