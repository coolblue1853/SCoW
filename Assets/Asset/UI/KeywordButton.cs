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
                return i; // �ش� ���ڿ��� ã���� �ε��� ��ȯ
            }
        }

        return -1; // �ش� ���ڿ��� ����Ʈ�� ���� ��� -1 ��ȯ
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
