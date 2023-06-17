using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordButton : MonoBehaviour
{
    string input;
    string A;
    string B;



    public void SeparateString()
    {
        input = this.transform.name;


        string[] parts = input.Split('_');
        if (parts.Length == 2)
        {
            A = parts[0];
            B = parts[1];
        }


        if(A == "Wharf")
        {
            DataBaseManager.keyword_main = "Place";

            int index_upper = FindStringIndex(KeyWordManager.Instance.upperPlaceKeywordList, A);
            DataBaseManager.keyword_upper = KeyWordManager.Instance.upperPlaceKeywordList[index_upper];

            int index_down = FindStringIndex(KeyWordManager.Instance.WharfList, B);
            DataBaseManager.keyword_downer = KeyWordManager.Instance.WharfList[index_down];
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
