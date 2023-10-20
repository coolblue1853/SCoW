using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TimeManagere : MonoBehaviour
{

    public LightColorController lightColorController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public GameObject BFNoonEvent;
    public GameObject AFNoonEvent;
    public GameObject Witness;
    public GameObject DeadBody;
    public GameObject Site;

    public GameObject Kane;
    public GameObject Student;
    public GameObject BarPeople1;
    public GameObject BarPeople2;
    public GameObject Albert;
    public GameObject Dave;
    public GameObject Swain;


    public Intel_ObtoUI Meave;

    

    void Update()
    {
        

        if(DataBaseManager.TimeCount >= 6 && AFNoonEvent.transform.localPosition.y > 30)
        {
            BFNoonEvent.transform.localPosition = new Vector3(0, 60, 0);
            AFNoonEvent.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (DataBaseManager.TimeCount >= 7 && Witness.activeSelf == true)
        {
            Meave.CanJudge = true;
            Witness.SetActive(false);
            DeadBody.SetActive(false);
            Site.transform.localPosition = new Vector3(-250.6001f, 2.840187f, 0);
            Kane.transform.localPosition = new Vector3(253.27f, -79.6f, 0);
            Student.transform.localPosition = new Vector3(-41.67f, 40.9f, 0);

        }
        if (DataBaseManager.TimeCount == 8 && BarPeople1.activeSelf == true)
        {
            BarPeople1.SetActive(false);
            BarPeople2.SetActive(false);
        }
        if (DataBaseManager.AlbertDeathKeyword && Albert.activeSelf == true && DataBaseManager.TimeCount % 4  == 0)
        {
            DataBaseManager.ThirdDayPoliceADialog = true;
            DataBaseManager.ThirdDayPoliceBDialog = true;

            Albert.SetActive(false);
        }
        if (DataBaseManager.DaveDeathKeyword && Dave.activeSelf == true && DataBaseManager.TimeCount % 4 == 0)
        {
            Dave.SetActive(false);
        }
        if (DataBaseManager.SwainDeathKeyword && Swain.activeSelf == true && DataBaseManager.TimeCount % 4 == 0)
        {
            Swain.SetActive(false);
        }


        if ((DataBaseManager.TimeCount % 4 )== 1)
        {
            lightColorController.time = 0.2f;
        }
        if ((DataBaseManager.TimeCount % 4) == 2)
        {
            lightColorController.time = 0.4f;
        }
        if ((DataBaseManager.TimeCount % 4) == 3)
        {
            lightColorController.time = 0.64f;
        }
        if ((DataBaseManager.TimeCount % 4) == 0)
        {
            lightColorController.time = 0.9f;
        }
    }
}
