﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IntelManager : MonoBehaviour
{

    public GameObject Character_B;
    public GameObject Event_B;
    public GameObject Place_B;

    public GameObject CharacterWindow;
    public GameObject EventWindow;
    public GameObject PlaceWindow;

    public GameObject UI;
    public GameObject IntelWindow;
    public GameObject FloatingTextObject;
    public GameObject FloatingPlace;

    public void AddIntelFloating(string Category, string tag, string detail)
    {
        if( tag == "행성대직렬")
        {
            //DataBaseManager.행성대직렬알리미 = true;
        }

        if(Category == "Event")
        {
            var clone = Instantiate(FloatingTextObject, FloatingPlace.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<IntelFloating>().Text.text = "<color=#B30000>("+ detail + ")</color>정보 획득".ToString();
            clone.transform.SetParent(UI.transform);
            clone.transform.localScale = new Vector3(1, 1, 1);
        }

    }
    public void QuitIntelWindow()
    {
        //DataBaseManager.옵션창여부 = false;
        IntelWindow.SetActive(false);
    }
    

    public GameObject EventWindow_ON;
    public GameObject CharacterWindow_ON;
    public GameObject PlaceWindow_ON;

    public void Open_Character()
    {
        CharacterWindow_ON.SetActive(true);
        EventWindow_ON.SetActive(false);
        PlaceWindow_ON.SetActive(false);

        CharacterWindow.SetActive(true);
        EventWindow.SetActive(false);
        PlaceWindow.SetActive(false);
    }

    public void Open_Event()
    {
        CharacterWindow_ON.SetActive(false);
        EventWindow_ON.SetActive(true);
        PlaceWindow_ON.SetActive(false);

        CharacterWindow.SetActive(false);
        EventWindow.SetActive(true);
        PlaceWindow.SetActive(false);
    }
    public void OpenPlace()
    {
        CharacterWindow_ON.SetActive(false);
        EventWindow_ON.SetActive(false);
        PlaceWindow_ON.SetActive(true);

        CharacterWindow.SetActive(false);
        EventWindow.SetActive(false);
        PlaceWindow.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

 
    }

    private static IntelManager instance = null;
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

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static IntelManager Instance
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
}
