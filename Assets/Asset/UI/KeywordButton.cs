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


            index_down = FindStringIndex(KeyWordManager.Instance.PlanetaryParadeList, B);
            KeyWordManager.Instance.PlanetaryParadeKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.PlanetaryParadeList[index_down];
        }
        if (A == "Paranoia")
        {
            DataBaseManager.keyword_main = "Event";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.ParanoiaList, B);
            KeyWordManager.Instance.ParanoiaKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.ParanoiaList[index_down];
        }
        if (A == "Insomnia")
        {
            DataBaseManager.keyword_main = "Event";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.InsomniaList, B);
            KeyWordManager.Instance.InsomniaKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.InsomniaList[index_down];
        }
        if (A == "Nightmare")
        {
            DataBaseManager.keyword_main = "Event";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.NightmareList, B);
            KeyWordManager.Instance.NightmareKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.NightmareList[index_down];
        }
        if (A == "FishySmell")
        {
            DataBaseManager.keyword_main = "Event";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.FishySmellList, B);
            KeyWordManager.Instance.FishySmellKeywordNum = index_down;
           DataBaseManager.keyword_downer = KeyWordManager.Instance.FishySmellList[index_down];
            
        }
        if (A == "MissingPeople")
        {
            DataBaseManager.keyword_main = "Event";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.MissingPeopleList, B);
            KeyWordManager.Instance.MissingPeopleKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.MissingPeopleList[index_down];
        }
        if (A == "SewerGhostStory")
        {
            DataBaseManager.keyword_main = "Event";

            index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.SewerGhostStoryList, B);
            KeyWordManager.Instance.SewerGhostStoryKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.SewerGhostStoryList[index_down];
        }
        if (A == "Safe")
        {
            DataBaseManager.keyword_main = "Event";

            index_upper = FindStringIndex(KeyWordManager.Instance.upperEventKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperEventKeywordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.SafeList, B);
            KeyWordManager.Instance.SafeKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.SafeList[index_down];
        }


        // 키워드 - PLACE
        if (A == "Wharf")
        {
            DataBaseManager.keyword_main = "Place";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.WharfList, B);
            KeyWordManager.Instance.WharfKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.WharfList[index_down];
        }
        if (A == "QuestHouse")
        {
            A = "Client'shouse";
            DataBaseManager.keyword_main = "Place";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.QuestHouseList, B);
            KeyWordManager.Instance.QuestHouseKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.QuestHouseList[index_down];
        }
        if (A == "DailyNews")
        {
            DataBaseManager.keyword_main = "Place";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.NewspaperList, B);
            KeyWordManager.Instance.NewspaperKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.NewspaperList[index_down];
        }
        if (A == "Hospital")
        {
            DataBaseManager.keyword_main = "Place";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.HospitalList, B);
            KeyWordManager.Instance.HospitalKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.HospitalList[index_down];
        }
        if (A == "Univ of Miscatonic")
        {
            DataBaseManager.keyword_main = "Place";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.UniversityList, B);
            KeyWordManager.Instance.UniversityKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.UniversityList[index_down];
        }


        // 키워드 - Character
        if (A == "Ella Triss")
        {
            DataBaseManager.keyword_main = "Character";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperCharacterKewordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.EllaList, B);
            KeyWordManager.Instance.EllaKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.EllaList[index_down];
        }
        if (A == "Aiden Triss")
        {
            DataBaseManager.keyword_main = "Character";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperCharacterKewordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.AidenList, B);
            KeyWordManager.Instance.AidenKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.AidenList[index_down];
        }
        if (A == "Albert Bradley")
        {
            DataBaseManager.keyword_main = "Character";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperCharacterKewordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.AlbertList, B);
            KeyWordManager.Instance.AlbertKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.AlbertList[index_down];
        }
        if (A == "Maeve A Rossi")
        {
            DataBaseManager.keyword_main = "Character";

             index_upper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperCharacterKewordList[index_upper];

             index_down = FindStringIndex(KeyWordManager.Instance.MeivList, B);
            KeyWordManager.Instance.MeivKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.MeivList[index_down];
        }

        if (A == "SewerWorker")
        {
            DataBaseManager.keyword_main = "Character";

            index_upper = FindStringIndex(KeyWordManager.Instance.upperCharacterKewordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperCharacterKewordList[index_upper];

            index_down = FindStringIndex(KeyWordManager.Instance.SewerWorkerList, B);
            KeyWordManager.Instance.SewerWorkerKeywordNum = index_down;
            DataBaseManager.keyword_downer = KeyWordManager.Instance.SewerWorkerList[index_down];
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
