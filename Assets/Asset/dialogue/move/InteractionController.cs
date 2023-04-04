using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionController : MonoBehaviour
{

    public GameObject Test;

    public DialogManager theDM;
    public DialogManager JudgeDM;


    // Update is called once per frame


    private void Start()
    {
        //TestNar();
    }
    public void TestNar()
    {

        theDM.ShowDialog(Test.transform.GetComponent<interactionEvent>().GetDialogs());
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
  


}
