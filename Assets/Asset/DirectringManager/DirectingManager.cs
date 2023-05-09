using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectingManager : MonoBehaviour
{
    private static DirectingManager instance = null;
    public GameObject player;

    //Å½Á¤»ç¹«¼Ò
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
        InteractionController.Instance.Start_1st_DetectiveOffice(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void End_Directing()
    {
        sit_player.SetActive(false);
        player.SetActive(true);
    }
}
