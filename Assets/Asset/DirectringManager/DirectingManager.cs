using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectingManager : MonoBehaviour
{
    private static DirectingManager instance = null;
    public GameObject player;

    //탐정사무소
    public GameObject sit_player;
    public static DirectingManager Instance
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
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("start"); 
    }

    // Update is called once per frame
    void Update()
    {
        if(DataBaseManager.fst_Detectiv_TimeOn >= 2)
        {
            if(DataBaseManager.isActiveDialog1 == false && DataBaseManager.isActiveDialog2 == false)
            {
                // 노크소리 발동
                DataBaseManager.isActiveDialog2 = true;
                Invoke("After_NockSound", 1);
            }
        }

    }


    void After_NockSound()
    {
        InteractionController.Instance.Start_1st_DetectiveOffice("Directing_Nock");
    }
    public void End_Directing()
    {
        sit_player.SetActive(false);
        player.SetActive(true);
    }
}
