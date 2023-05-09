using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionController : MonoBehaviour
{

    public GameObject Start_1st_Detective;

    public DialogManager theDM;
    public DialogManager JudgeDM;


    // Update is called once per frame


    private void Start()
    {
        //TestNar();
    }
    public void Start_1st_DetectiveOffice()
    {
        theDM.ShowDialog(Start_1st_Detective.transform.GetComponent<interactionEvent>().GetDialogs());
    }




    void OnEnable()
    {

    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    void Update()
    {

    }


    /*
    public void 의뢰자등장후대화()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(의뢰자등장후.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
    */


   public void RetrunDialogResult(string Sub_Dialog, string result_End)
    {
        if(Sub_Dialog == "신문 : 살펴보기")
        {
            //판정 이후에 결과 보여주는 곳임.    
            //원래는 여기에 결과값도 받아서 구분해줘야함
        }

    }



    private static InteractionController instance = null;
    private void Awake()
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
    public static InteractionController Instance
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
