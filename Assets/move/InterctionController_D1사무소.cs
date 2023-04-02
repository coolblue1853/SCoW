using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterctionController_D1사무소 : MonoBehaviour
{
    //public GameObject 선1예;
    //public GameObject 선1아니오;
    public GameObject 엘라;
    public GameObject 독백;
    public GameObject 의뢰자등장후;
    public GameObject 의뢰자앉은후;
    public GameObject 의뢰자나간후;

    public GameObject 신문살펴보기;

    public GameObject 신문_정보조사_실패;
    public GameObject 신문_정보조사_대실패;
    public GameObject 신문_정보조사_성공;
    public GameObject 신문_정보조사_대성공;

    //멜리사 대사

    public GameObject 멜리사_1일차아침_최초대사;
    public GameObject 멜리사_1일차아침_반복대사1;
    public GameObject 멜리사_1일차아침_반복대사2;
    public GameObject 멜리사_1일차아침_반복대사3;

    public GameObject 멜리사_1일차아침_불면증;
    public GameObject 멜리사_1일차아침_행성대직렬;
    public GameObject 멜리사_1일차아침_악몽;
    public GameObject 멜리사_1일차아침_에이든;
    public GameObject 멜리사_1일차아침_엘라;
    public GameObject 멜리사_1일차아침_병원;
    public GameObject 멜리사_1일차아침_신문사;
    public GameObject 멜리사_1일차아침_의뢰자의집;


    /*
    public GameObject 선택지1성공;
    public GameObject 선택지1실패;

    public GameObject 선택1커피줘;
    public GameObject 선택1직접타기;
    public GameObject 선택1가만히있기;


    public GameObject 선택2_1오브젝트;
    public GameObject 선택2_2오브젝트;
    public GameObject 선택2_3오브젝트;

    public GameObject 선택3_거절_심리X;
    public GameObject 선택3_거절_심리O;
    public GameObject 선택3_거절_선택1에1선택;
    public GameObject 선택3_수락;
    public GameObject 선택3_수락_선택1에1선택;


    public GameObject 선1이후;

    */


    //1 심리학판정 성공
    public GameObject 엘라심리학성공;
    public GameObject 엘라심리학실패;
    // Start is called before the first frame update

    //public DialogManager_D1사무소 theDM;
    //public DialogManager_D1사무소 기능판정DM;

    public DialogManager_D1사무소 theDM;
    public DialogManager_D1사무소 기능판정DM;


    // Update is called once per frame


    private void Start()
    {
        startNaertion();
    }
    public void startNaertion()
    {

        theDM.ShowDialog(독백.transform.GetComponent<interactionEvent>().GetDialogs());
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



    public void 의뢰자등장후대화()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(의뢰자등장후.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }

    public void 의뢰자앉은후대화()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(의뢰자앉은후.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }

    public void 의뢰자나간후대화()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(의뢰자나간후.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }

    public void 신문살펴보기대화()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(신문살펴보기.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }


    public void 신문정보조사실패()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(신문_정보조사_실패.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }

    public void 신문정보조사대실패()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(신문_정보조사_대실패.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }

    public void 신문정보조사성공()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(신문_정보조사_성공.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }

    public void 신문정보조사대성공()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(신문_정보조사_대성공.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
    // //////////////////////////////////////////////// 멜리사 상호작용
    public void 멜리사1일차아침최초대화()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(멜리사_1일차아침_최초대사.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
    public void 멜리사기본대화1()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(멜리사_1일차아침_반복대사1.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }

    public void 멜리사기본대화2()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(멜리사_1일차아침_반복대사2.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }

    public void 멜리사기본대화3()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(멜리사_1일차아침_반복대사3.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }


    // 멜리사 키워드 대화

    public void 멜리사_행성대직렬_키워드()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(멜리사_1일차아침_행성대직렬.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }

    public void 멜리사_악몽_키워드()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(멜리사_1일차아침_악몽.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
    public void 멜리사_불면증_키워드()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(멜리사_1일차아침_불면증.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
    public void 멜리사_에이든_키워드()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(멜리사_1일차아침_에이든.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
    public void 멜리사_엘라_키워드()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(멜리사_1일차아침_엘라.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
    public void 멜리사_병원_키워드()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(멜리사_1일차아침_병원.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
    public void 멜리사_신문사_키워드()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(멜리사_1일차아침_신문사.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
    public void 멜리사_의뢰자의집_키워드()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(멜리사_1일차아침_의뢰자의집.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }






    //참고이후 삭제


    public void 선1예선택()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;
            theDM.isDialogON = true;
           // theDM.ShowDialog(선1예.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
    public void 선1아니오선택()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;
            theDM.isDialogON = true;
           // theDM.ShowDialog(선1아니오.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }

    public void checkObject()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;
            theDM.ShowDialog(엘라.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }







    //이하 기능판정, 중간삽입물.


    public void 엘라심리학성공출력대사()
    {

        if (기능판정DM.isDialogON == false)
        {
            theDM.isDialogON = true;
            기능판정DM.isDialogON = true;
            기능판정DM.ShowDialog(엘라심리학성공.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
    public void 엘라심리학실패출력대사()
    {

        if (기능판정DM.isDialogON == false)
        {
            theDM.isDialogON = true;
            기능판정DM.isDialogON = true;
            기능판정DM.ShowDialog(엘라심리학실패.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }


}
