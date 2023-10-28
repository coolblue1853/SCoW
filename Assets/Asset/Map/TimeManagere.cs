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
    public GameObject Sewer_Flat;
 public GameObject Sewer_Round;
 public GameObject Burb_Round1;
 public GameObject Burb_Round2;
 public GameObject Burb_Flet1;
 public GameObject Burb_Flet2;


 public GameObject Ledder;
 public GameObject Hole;
 public GameObject House;
 public GameObject Rand2;
 public GameObject SideRoad;
 public GameObject Pipe;
 public GameObject Cage;


    public void MakeCageLand()
    {
        Pipe.SetActive(false);
        Sewer_Flat.SetActive(false);
        Sewer_Round.SetActive(false);
        Burb_Round1.SetActive(false);
        Burb_Round2.SetActive(false);
        Burb_Flet1.SetActive(false);
        Burb_Flet2.SetActive(false);
        Ledder.SetActive(false);
        Hole.SetActive(false);
        Cage.SetActive(false);
        SideRoad.SetActive(false);

        Sewer_Flat.SetActive(true);
        Burb_Flet1.SetActive(true);
        Burb_Flet2.SetActive(true);
        Pipe.SetActive(true);
        Rand2.SetActive(true);
        Cage.SetActive(true);
    }
    public void MakeHouseLand()
    {
        Pipe.SetActive(false);
        Sewer_Flat.SetActive(false);
        Sewer_Round.SetActive(false);
        Burb_Round1.SetActive(false);
        Burb_Round2.SetActive(false);
        Burb_Flet1.SetActive(false);
        Burb_Flet2.SetActive(false);
        Ledder.SetActive(false);
        Hole.SetActive(false);
        Cage.SetActive(false);
        SideRoad.SetActive(false);


        Sewer_Flat.SetActive(true);
        Burb_Flet2.SetActive(true);
        Rand2.SetActive(true);
        House.SetActive(true);
    }
    public void MakeSewer()
    {
        Pipe.SetActive(false);
        Sewer_Flat.SetActive(false);
        Sewer_Round.SetActive(false);
        Burb_Round1.SetActive(false);
        Burb_Round2.SetActive(false);
        Burb_Flet1.SetActive(false);
        Burb_Flet2.SetActive(false);
        Ledder.SetActive(false);
        Hole.SetActive(false);
        Cage.SetActive(false);
        SideRoad.SetActive(false);



        int Sewer = Random.Range(0, 2);
        if(Sewer == 1)
        {
            Sewer_Flat.SetActive(true);
            Sewer_Round.SetActive(false);

            int FlatLight1 = Random.Range(0, 2);
            if(FlatLight1 == 0)
            {
                Burb_Flet1.SetActive(true);
            }
            else
            {
                Burb_Flet1.SetActive(false);
            }

            int FlatLight2 = Random.Range(0, 2);
            if (FlatLight2 == 0)
            {
                Burb_Flet2.SetActive(true);
            }
            else
            {
                Burb_Flet2.SetActive(false);
            }
            int Side = Random.Range(0, 2);
            if (Side == 0)
            {
                SideRoad.SetActive(true);
            }
            else
            {
                SideRoad.SetActive(false);

                int HoleInt = Random.Range(0, 2);
                if (HoleInt == 0)
                {
                    Hole.SetActive(true);
                }
                else
                {
                    Hole.SetActive(false);
                }

                int Ledderint = Random.Range(0, 2);
                if (Ledderint == 0)
                {
                    Ledder.SetActive(true);
                }
                else
                {
                    Ledder.SetActive(false);
                }


            }
        }
        else
        {
            Sewer_Flat.SetActive(false);
            Sewer_Round.SetActive(true);
            int RoundLight1 = Random.Range(0, 2);
            if (RoundLight1 == 0)
            {
                Burb_Round1.SetActive(true);
            }
            else
            {
                Burb_Round1.SetActive(false);
            }
            int RoundLight2 = Random.Range(0, 2);
            if (RoundLight2 == 0)
            {
                Burb_Round2.SetActive(true);
            }
            else
            {
                Burb_Round2.SetActive(false);
            }
        }

      
        int Pipeint = Random.Range(0, 2);
        if (Pipeint == 0)
        {
            Pipe.SetActive(true);
        }
        else
        {
            Pipe.SetActive(false);
        }
    }




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

    public GameObject SewerDoor_Dialog;
    public GameObject SewerDoor_Potal;

    public Intel_ObtoUI SewerOfficeWindow;
    public Intel_ObtoUI SewerOfficeLocker;
    public Intel_ObtoUI BlackWell;
    public GameObject BlackWell_OB;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // MakeSewer();
            //
              MakeCageLand();

         //   MakeHouseLand();
        }

        if(DataBaseManager.Black_RechSucc_CanTalck == true && BlackWell.CanKeyword ==false)
        {
            BlackWell.CanKeyword = true;
        }

        if((DataBaseManager.TimeCount % 4 == 1 )|| (DataBaseManager.TimeCount % 4 == 2) && DataBaseManager.SewerDoorOpen == false)
        {
            DataBaseManager.SewerDoorOpen = true;
            SewerDoor_Potal.transform.localPosition = new Vector3(-65.10986f, 5, 0);
            SewerDoor_Dialog.transform.localPosition = new Vector3(-65.10986f, 33, 0);
            BlackWell_OB.transform.localPosition = new Vector3(-224.6f, 4.08f, 0);
            SewerOfficeWindow.CanJudge = false;
            SewerOfficeLocker.CanJudge = false;
        }
        if ((DataBaseManager.TimeCount % 4 == 3) || (DataBaseManager.TimeCount % 4 == 0) && DataBaseManager.SewerDoorOpen == true)
        {
            BlackWell_OB.transform.localPosition = new Vector3(-224.6f, 29.6f, 0);
            DataBaseManager.SewerDoorOpen = false;
            if (DataBaseManager.SewerDoorOpenDeftness == true)
            {
                SewerDoor_Potal.transform.localPosition = new Vector3(-65.10986f, 5, 0);
                SewerDoor_Dialog.transform.localPosition = new Vector3(-65.10986f, 33, 0);
            }
            else
            {
                SewerDoor_Potal.transform.localPosition = new Vector3(-65.10986f, 33, 0);
                SewerDoor_Dialog.transform.localPosition = new Vector3(-65.10986f, 5, 0);
            }

            if (DataBaseManager.TryBrokenWindow == true)
            {
                SewerOfficeWindow.CanJudge = false;
            }
            else
            {
                SewerOfficeWindow.CanJudge = true;
            }
            if (DataBaseManager.TryObserLokcer == true)
            {
                SewerOfficeLocker.CanJudge = false;
            }
            else
            {
                SewerOfficeLocker.CanJudge = true;
            }
        }
        if ( DataBaseManager.SewerDoorOpenCheck == true)
        {
            DataBaseManager.SewerDoorOpenCheck = false;
            SewerDoor_Potal.transform.localPosition = new Vector3(-65.10986f, 5, 0);
            SewerDoor_Dialog.transform.localPosition = new Vector3(-65.10986f, 33, 0);

        }
        
        if (DataBaseManager.TimeCount >= 6 && AFNoonEvent.transform.localPosition.y > 30)
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
        if (DataBaseManager.TimeCount >= 11 && Meave.CanJudge == true)
        {
            Meave.CanJudge = false;


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
