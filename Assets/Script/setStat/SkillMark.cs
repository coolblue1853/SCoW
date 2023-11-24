using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SkillMark : MonoBehaviour
{
    public GameObject strMark;
    public GameObject intMark;
    public GameObject dexMark;

    public GameObject CompleteButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DataBaseManager.strSkillPoint == 0) 
            {
                strMark.SetActive(false);
            }
        else
            {
                strMark.SetActive(true);
            }
        if (DataBaseManager.intSkillPoint == 0)
        {
            intMark.SetActive(false);
        }
        else
        {
            intMark.SetActive(true);
        }
        if (DataBaseManager.dexSkillPoint == 0)
        {
            dexMark.SetActive(false);
        }
        else
        {
            dexMark.SetActive(true);
        }

        if(strMark.activeSelf == false && intMark.activeSelf == false && dexMark.activeSelf == false)
        {
            CompleteButton.SetActive(true);
        }
        else
        {
            CompleteButton.SetActive(false);
        }
    }


    public void CompleteButton_F()
    {
        // 메인 게임으로 씬 이동
    }
}
