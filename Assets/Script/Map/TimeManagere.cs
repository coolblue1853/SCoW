using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TimeManagere : MonoBehaviour
{
    public GameObject DetectiveNightBulb;
    void BulbCheck()
    {
        if (DetectiveNightBulb.activeSelf == false && DataBaseManager.TimeCount % 4 == 0)
        {
            DetectiveNightBulb.SetActive(true);
        }
        else if (DetectiveNightBulb.activeSelf == true && DataBaseManager.TimeCount % 4 != 0)
        {
            DetectiveNightBulb.SetActive(false);
        }
    }
    public LightColorController lightColorController;
    public GameObject Enemy;
    public GameObject Enemy2;
    public GameObject OldMap;
    public GameObject EndDoorDetective;
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

    public GameObject Symbol;
    //전투 맵
    public GameObject Sewer_Flat_Battle;
    public GameObject Sewer_Round_Battle;
    public GameObject Burb_Round1_Battle;
    public GameObject Burb_Round2_Battle;
    public GameObject Burb_Flet1_Battle;
    public GameObject Burb_Flet2_Battle;
    public GameObject Ledder_Battle;
    public GameObject Hole_Battle;
    public GameObject House_Battle;
    public GameObject Rand2_Battle;
    public GameObject SideRoad_Battle;
    public GameObject Pipe_Battle;
    public GameObject Cage_Battle;
    public GameObject GroundWater_Battle;
    public GameObject GroundWaterRand2_Battle;

    public GameObject GroundWater;
    public GameObject GroundWaterRand2;
    public void CloseOldMap()
    {
        OldMap.SetActive(false);
    }
    public void MakeCageLand()
    {
        Symbol.transform.localPosition = new Vector3(-61.96f, 34.7f, 0);
        Pipe.SetActive(false);
        Sewer_Flat.SetActive(false);
        Sewer_Round.SetActive(false);
        Burb_Round1.SetActive(false);
        Burb_Round2.SetActive(false);
        Burb_Flet1.SetActive(false);
        Burb_Flet2.SetActive(false);
        Ledder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0);
        Hole.SetActive(false);
        Cage.SetActive(false);
        SideRoad.SetActive(false);
        House.SetActive(false);

        Sewer_Flat.SetActive(true);
        Burb_Flet1.SetActive(true);
        Burb_Flet2.SetActive(true);
        Pipe.SetActive(true);
        Rand2.SetActive(true);
        Cage.SetActive(true);
        GroundWater.SetActive(false);
        GroundWaterRand2.SetActive(true);
    }
    public void MakeHouseLand()
    {
        Symbol.transform.localPosition = new Vector3(-61.96f, 34.7f, 0);
        Pipe.SetActive(false);
        Sewer_Flat.SetActive(false);
        Sewer_Round.SetActive(false);
        Burb_Round1.SetActive(false);
        Burb_Round2.SetActive(false);
        Burb_Flet1.SetActive(false);
        Burb_Flet2.SetActive(false);
        Ledder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0);
        Hole.SetActive(false);
        Cage.SetActive(false);
        SideRoad.SetActive(false);

        Sewer_Flat.SetActive(true);
        Burb_Flet2.SetActive(true);
        Rand2.SetActive(true);
        House.SetActive(true);
        GroundWater.SetActive(false);
        GroundWaterRand2.SetActive(true);
    }
    public void TestLand()
    {
        Ledder.transform.localPosition = new Vector3(-60.49f, 8.79f, 0);
    }
    public void MakeSighnLand()
    {
        Symbol.transform.localPosition = new Vector3(-61.96f, 9.48f, 0);
        Pipe.SetActive(false);
        Sewer_Flat.SetActive(false);
        Sewer_Round.SetActive(false);
        Burb_Round1.SetActive(false);
        Burb_Round2.SetActive(false);
        Burb_Flet1.SetActive(false);
        Burb_Flet2.SetActive(false);
        Ledder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0);
        Hole.SetActive(false);
        Cage.SetActive(false);
        SideRoad.SetActive(false);
        House.SetActive(false);

        Rand2.SetActive(false);
        Sewer_Flat.SetActive(true);
        Burb_Flet1.SetActive(true);
        Burb_Flet2.SetActive(true);
        Pipe.SetActive(true);
        GroundWater.SetActive(true);
        GroundWaterRand2.SetActive(false);
    }
    public void EnemySpawn()
    {
        DataBaseManager.StoryDirecting = true;
    }
    public void MakeSewer()
    {
        Enemy.SetActive(false);
        // 지도가 없다면
        if (DataBaseManager.GainMap == false)
        {
            int RandLuk = Random.Range(1, 101);
            Debug.Log("럭 : " + DataBaseManager.luk + "// 판정값 : " + RandLuk);
            //성공
            if (RandLuk < DataBaseManager.luk)
            {
                if (RandLuk < (DataBaseManager.luk / 3)) // 극단적 성공일시
                {
                    MakeSighnLand();
                    Debug.Log("문양 도착");
                }
                else
                {
                    int RandInt = Random.Range(0, 6);
                    if (RandInt == 0)
                    {
                        MakeHouseLand();
                        Debug.Log("비밀기지 도착");
                    }
                    else if (RandInt == 1)
                    {
                        MakeSewerMap_Nomal();
                        Debug.Log("아이템이 떨어진 곳 도착");
                    }
                    else if (RandInt == 2)
                    {
                        MakeCageLand();
                        Debug.Log("케이지가 있는곳 도착");
                    }
                    else
                    {
                        MakeSewerMap_Nomal();
                        Debug.Log("빈방 도착");
                    }
                }
            }
            //실패
            else
            {
                if (DataBaseManager.SewerEnemyCounter > 2 && DataBaseManager.SewerHouseIn == false)  // 최초로 비밀기지 도착
                {
                    MakeHouseLand();
                    Debug.Log("비밀기지 도착");
                }
                else if (DataBaseManager.SewerEnemyCounter > 2)
                {
                    int RandInt = Random.Range(0, 3);
                    if (RandInt == 0)
                    {
                        MakeHouseLand();
                        Debug.Log("비밀기지 도착");
                    }
                    else
                    {
                        MakeSewerMap_Nomal();
                        Debug.Log("빈방 도착");
                    }
                }
                else
                {
                    DataBaseManager.SewerEnemyCounter += 1;
                    if (DataBaseManager.SewerEnemyCounter == 1)
                    {
                        Enemy.SetActive(true);
                    }
                    else if (DataBaseManager.SewerEnemyCounter == 2)
                    {
                        Enemy2.SetActive(true);
                    }
                    else
                    {
                        Enemy.SetActive(true);
                        Enemy2.SetActive(true);
                    }
                    int RandInt = Random.Range(0, 3);
                    MakeSewerMap_Enemy();
                    Debug.Log("적 등장");
                    if (DataBaseManager.WearCoat == true)
                    {
                        DataBaseManager.IsInsmusMeetSewer = false;
                    }
                    else
                    {
                        Invoke("PrintBattleDialog", 1f);
                    }
                }
            }
        }
        // 지도가 있다면
        else
        {
            int RandLuk = Random.Range(1, 101);
            Debug.Log("럭 : " + DataBaseManager.luk + "// 판정값 : " + RandLuk);

            int RandIntel = Random.Range(1, 101);
            Debug.Log("지능 : " + DataBaseManager.intl + "// 판정값 : " + RandIntel);

            //성공
            if (RandLuk < DataBaseManager.luk || RandIntel < DataBaseManager.intl)
            {
                if (RandLuk < (DataBaseManager.luk / 3) || RandIntel < (DataBaseManager.intl / 3)) // 극단적 성공일시
                {
                    MakeSighnLand();
                    Debug.Log("문양 도착");
                }
                else
                {
                    int RandInt = Random.Range(0, 5);
                    if (RandInt == 0)
                    {
                        MakeHouseLand();
                        Debug.Log("비밀기지 도착");
                    }
                    else if (RandInt == 1)
                    {
                        MakeSewerMap_Nomal();
                        Debug.Log("아이템이 떨어진 곳 도착");
                    }
                    else
                    {
                        MakeSewerMap_Nomal();
                        Debug.Log("빈방 도착");
                    }
                }
            }
            //실패
            else
            {
                if (DataBaseManager.SewerEnemyCounter > 2 && DataBaseManager.SewerHouseIn == false)  // 최초로 비밀기지 도착
                {
                    MakeHouseLand();
                    Debug.Log("비밀기지 도착");
                }
                else if (DataBaseManager.SewerEnemyCounter > 2)
                {
                    int RandInt = Random.Range(0, 3);
                    if (RandInt == 0)
                    {
                        MakeHouseLand();
                        Debug.Log("비밀기지 도착");
                    }
                    else
                    {
                        MakeSewerMap_Nomal();
                        Debug.Log("빈방 도착");
                    }
                }
                else
                {
                    DataBaseManager.SewerEnemyCounter += 1;
                    int RandInt = Random.Range(0, 3);

                    MakeSewerMap_Enemy();
                    Debug.Log("적 등장");

                    if (DataBaseManager.WearCoat == true)
                    {
                        Enemy.SetActive(true);
                        DataBaseManager.IsInsmusMeetSewer = false;
                    }
                    else
                    {
                        Enemy.SetActive(true);
                        Invoke("PrintBattleDialog", 1f);
                    }
                }
            }
        }
    }
    public void PrintBattleDialog()
    {
        if (DataBaseManager.SewerEnemyCounter == 2)
        {
            InteractionController.Instance.InSewerDialog("Sewer_FirstDeepOne");
        }
        else
        {
            InteractionController.Instance.InSewerDialog("Sewer_EnemySpawn");
        }
    }
    public GameObject player;
    public GameObject camera;
    Vector2 player_SewerRight = new Vector3(-1194.19f, -193.57f);
    Vector3 Cam_SewerRight = new Vector3(-1217.02f, -195f, -15);
    float ChInRommSize = 2.383215f; // -이면 왼쪽 +면 오른쪽
    public void SewerMapMove()
    {
        TimeManagere.Instance.MakeSighnLand();
        DataBaseManager.isDirecting = true;
        player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = player_SewerRight;
        camera.transform.localPosition = Cam_SewerRight;
        TimeManagere.Instance.DeletRightSewerPotal();
    }
    public void MakeSewerMap_Nomal()
    {
        Symbol.transform.localPosition = new Vector3(-61.96f, 34.7f, 0);
        Enemy.SetActive(false);
        Enemy2.SetActive(false);
        Pipe.SetActive(false);
        Sewer_Flat.SetActive(false);
        Sewer_Round.SetActive(false);
        Burb_Round1.SetActive(false);
        Burb_Round2.SetActive(false);
        Burb_Flet1.SetActive(false);
        Burb_Flet2.SetActive(false);
        Ledder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0);
        Hole.SetActive(false);
        Cage.SetActive(false);
        SideRoad.SetActive(false);
        House.SetActive(false);
        Rand2.SetActive(false);

        GroundWater.SetActive(true);
        GroundWaterRand2.SetActive(false);

        int Sewer = Random.Range(0, 2);
        if (Sewer == 1)
        {
            Sewer_Flat.SetActive(true);
            Sewer_Round.SetActive(false);

            int FlatLight1 = Random.Range(0, 2);
            if (FlatLight1 == 0)
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
                    Ledder.transform.localPosition = new Vector3(-60.49f, 8.79f, 0);
                }
                else
                {
                    Ledder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0);
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
    public void MakeSewerMap_Enemy()
    {
        Symbol.transform.localPosition = new Vector3(-61.96f, 34.7f, 0);
        GroundWater.SetActive(true);
        Pipe.SetActive(false);
        Sewer_Flat.SetActive(false);
        Sewer_Round.SetActive(false);
        Burb_Round1.SetActive(false);
        Burb_Round2.SetActive(false);
        Burb_Flet1.SetActive(false);
        Burb_Flet2.SetActive(false);
        Ledder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0);
        Hole.SetActive(false);
        Cage.SetActive(false);
        SideRoad.SetActive(false);
        House.SetActive(false);
        Rand2.SetActive(false);

        GroundWater_Battle.SetActive(true);
        GroundWaterRand2_Battle.SetActive(false);

        GroundWater_Battle.SetActive(true);
        Pipe_Battle.SetActive(false);
        Sewer_Flat_Battle.SetActive(false);
        Sewer_Round_Battle.SetActive(false);
        Burb_Round1_Battle.SetActive(false);
        Burb_Round2_Battle.SetActive(false);
        Burb_Flet1_Battle.SetActive(false);
        Burb_Flet2_Battle.SetActive(false);
        Ledder_Battle.SetActive(false);
        Hole_Battle.SetActive(false);
        Cage_Battle.SetActive(false);
        SideRoad_Battle.SetActive(false);
        House_Battle.SetActive(false);
        Rand2_Battle.SetActive(false);

        GroundWater_Battle.SetActive(true);
        GroundWaterRand2_Battle.SetActive(false);

        int Sewer = Random.Range(0, 2);
        if (Sewer == 1)
        {
            Sewer_Flat.SetActive(true);
            Sewer_Round.SetActive(false);

            Sewer_Flat_Battle.SetActive(true);
            Sewer_Round_Battle.SetActive(false);

            int FlatLight1 = Random.Range(0, 2);
            if (FlatLight1 == 0)
            {
                Burb_Flet1.SetActive(true);
                Burb_Flet1_Battle.SetActive(true);
            }
            else
            {
                Burb_Flet1.SetActive(false);
                Burb_Flet1_Battle.SetActive(false);
            }
            int FlatLight2 = Random.Range(0, 2);
            if (FlatLight2 == 0)
            {
                Burb_Flet2.SetActive(true);
                Burb_Flet2_Battle.SetActive(true);
            }
            else
            {
                Burb_Flet2.SetActive(false);
                Burb_Flet2_Battle.SetActive(false);
            }

            int HoleInt = Random.Range(0, 2);
            if (HoleInt == 0)
            {
                Hole.SetActive(true);
                Hole_Battle.SetActive(true);
            }
            else
            {
                Hole.SetActive(false);
                Hole_Battle.SetActive(false);
            }
        }
        else
        {
            Sewer_Flat.SetActive(false);
            Sewer_Round.SetActive(true);

            Sewer_Flat_Battle.SetActive(false);
            Sewer_Round_Battle.SetActive(true);
            int RoundLight1 = Random.Range(0, 2);
            if (RoundLight1 == 0)
            {
                Burb_Round1.SetActive(true);
                Burb_Round1_Battle.SetActive(true);
            }
            else
            {
                Burb_Round1.SetActive(false);
                Burb_Round1_Battle.SetActive(false);
            }
            int RoundLight2 = Random.Range(0, 2);
            if (RoundLight2 == 0)
            {
                Burb_Round2.SetActive(true);
                Burb_Round2_Battle.SetActive(true);
            }
            else
            {
                Burb_Round2.SetActive(false);
                Burb_Round2_Battle.SetActive(false);
            }
        }
        int Pipeint = Random.Range(0, 2);
        if (Pipeint == 0)
        {
            Pipe.SetActive(true);
            Pipe_Battle.SetActive(true);
        }
        else
        {
            Pipe.SetActive(false);
            Pipe_Battle.SetActive(false);
        }
    }
    public void Sewer_ByRiverSide()
    {
        Symbol.transform.localPosition = new Vector3(-61.96f, 34.7f, 0);
        Pipe.SetActive(false);
        Sewer_Flat.SetActive(false);
        Sewer_Round.SetActive(false);
        Burb_Round1.SetActive(false);
        Burb_Round2.SetActive(false);
        Burb_Flet1.SetActive(false);
        Burb_Flet2.SetActive(false);
        Ledder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0); ;
        Hole.SetActive(false);
        Cage.SetActive(false);
        SideRoad.SetActive(false);
        House.SetActive(false);
        GroundWater.SetActive(true);
        GroundWaterRand2.SetActive(false);

        int Sewer = Random.Range(0, 2);
        if (Sewer == 1)
        {
            Sewer_Flat.SetActive(true);
            Sewer_Round.SetActive(false);

            int FlatLight1 = Random.Range(0, 2);
            if (FlatLight1 == 0)
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

    public GameObject PoliceLine;

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

    public GameObject Journalist;

    public Intel_ObtoUI Meave;

    public GameObject SewerDoor_Dialog;
    public GameObject SewerDoor_Potal;

    public Intel_ObtoUI SewerOfficeWindow;
    public Intel_ObtoUI SewerOfficeLocker;
    public Intel_ObtoUI BlackWell;
    public GameObject BlackWell_OB;

    public GameObject RiverActive;
    public GameObject RiverPotal;

    public GameObject LeftSewerPotal;
    public GameObject RightSewerPotal;
    public void DeletRightSewerPotal()
    {
        LeftSewerPotal.SetActive(true);
        RightSewerPotal.SetActive(false);
    }
    public void DeletLeftSewerPotal()
    {
        LeftSewerPotal.SetActive(false);
        RightSewerPotal.SetActive(true);
    }
    public GameObject Manhole;

    void Update()
    {
        BulbCheck();
        if (EndDoorDetective.activeSelf == false && DataBaseManager.EndingDoorCheck == true)
        {
            EndDoorDetective.SetActive(true);
        }
        if (Manhole.activeSelf == false && DataBaseManager.DeadBody_GotoSwere == true)
        {
            Manhole.SetActive(true);
        }
        if (RiverActive.activeSelf == true && (DataBaseManager.SewerPicingSucc == true || DataBaseManager.SewerageKey == true))
        {
            RiverPotal.SetActive(true);
            RiverActive.SetActive(false);
        }
        if (DataBaseManager.Black_RechSucc_CanTalck == true && BlackWell.CanKeyword == false)
        {
            BlackWell.CanKeyword = true;
        }
        if ((DataBaseManager.TimeCount % 4 == 1) || (DataBaseManager.TimeCount % 4 == 2) && DataBaseManager.SewerDoorOpen == false)
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
        if (DataBaseManager.SewerDoorOpenCheck == true)
        {
            DataBaseManager.SewerDoorOpenCheck = false;
            SewerDoor_Potal.transform.localPosition = new Vector3(-65.10986f, 5, 0);
            SewerDoor_Dialog.transform.localPosition = new Vector3(-65.10986f, 33, 0);
        }

        if (DataBaseManager.TimeCount >= 6 && AFNoonEvent.transform.localPosition.y > 30)
        {
            BFNoonEvent.transform.localPosition = new Vector3(0, 60, 0);
            AFNoonEvent.transform.localPosition = new Vector3(0, 0, 0);
            PoliceLine.transform.localPosition = new Vector3(-65.88f, 4.96f, 0);
        }
        if (DataBaseManager.TimeCount >= 7 && Witness.activeSelf == true)
        {
            Meave.CanJudge = true;
            Witness.SetActive(false);
            DeadBody.SetActive(false);
            Site.transform.localPosition = new Vector3(-250.6001f, 0.4400024f, 0);
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
        if (DataBaseManager.AlbertDeathKeyword && Albert.activeSelf == true && DataBaseManager.TimeCount % 4 == 0 && DataBaseManager.TimeCount >= 8)
        {
            DataBaseManager.ThirdDayPoliceADialog = true;
            DataBaseManager.ThirdDayPoliceBDialog = true;
            Albert.SetActive(false);
        }
        if (DataBaseManager.DaveDeathKeyword && Dave.activeSelf == true && DataBaseManager.TimeCount % 4 == 0)
        {
            DataBaseManager.AfterDaveDeathFirstSlum = true;
            Dave.SetActive(false);
        }
        if (DataBaseManager.SwainDeathKeyword && Swain.activeSelf == true && DataBaseManager.TimeCount % 4 == 0)
        {
            Journalist.transform.localPosition = new Vector3(-61.37f, 3.4f, 0);
            Swain.SetActive(false);
        }
        if ((DataBaseManager.TimeCount % 4) == 1)
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
    private static TimeManagere instance = null;
    public static TimeManagere Instance
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
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
