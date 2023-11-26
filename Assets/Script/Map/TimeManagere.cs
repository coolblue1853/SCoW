using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TimeManagere : MonoBehaviour
{


    public GameObject detectiveNightBulb;
    public LightColorController lightColorController;
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject oldMap;
    public GameObject endDoorDetective;
    public GameObject sewerFlat;
    public GameObject sewerRound;
    public GameObject burbRound1;
    public GameObject burbRound2;
    public GameObject burbFlet1;
    public GameObject burbFlet2;

    public GameObject ladder;
    public GameObject hole;
    public GameObject house;
    public GameObject land2;
    public GameObject sideRoad;
    public GameObject pipe;
    public GameObject cage;

    public GameObject symbol;
    //전투 맵
    public GameObject sewerFlatBattle;
    public GameObject sewerRoundBattle;
    public GameObject burbRound1Battle;
    public GameObject burbRound2Battle;
    public GameObject burbFlet1Battle;
    public GameObject burbFlet2Battle;

    public GameObject ladderBattle;
    public GameObject holeBattle;
    public GameObject houseBattle;
    public GameObject land2Battle;
    public GameObject sideRoadBattle;
    public GameObject pipeBattle;
    public GameObject cageBattle;
    public GameObject groundWaterBattle;
    public GameObject groundWaterLand2Battle;

    public GameObject groundWater;
    public GameObject groundWaterLand2;

    void BulbCheck()
    {
        if (detectiveNightBulb.activeSelf == false && DataBaseManager.TimeCount % 4 == 0)
        {
            detectiveNightBulb.SetActive(true);
        }
        else if (detectiveNightBulb.activeSelf == true && DataBaseManager.TimeCount % 4 != 0)
        {
            detectiveNightBulb.SetActive(false);
        }
    }
    public void CloseOldMap()
    {
        oldMap.SetActive(false);
    }
    public void MakeCageLand()
    {
        symbol.transform.localPosition = new Vector3(-61.96f, 34.7f, 0);
        pipe.SetActive(false);
        sewerFlat.SetActive(false);
        sewerRound.SetActive(false);
        burbRound1.SetActive(false);
        burbRound2.SetActive(false);
        burbFlet1.SetActive(false);
        burbFlet2.SetActive(false);
        ladder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0);
        hole.SetActive(false);
        cage.SetActive(false);
        sideRoad.SetActive(false);
        house.SetActive(false);

        sewerFlat.SetActive(true);
        burbFlet1.SetActive(true);
        burbFlet2.SetActive(true);
        pipe.SetActive(true);
        land2.SetActive(true);
        cage.SetActive(true);
        groundWater.SetActive(false);
        groundWaterLand2.SetActive(true);
    }
    public void MakeHouseLand()
    {
        symbol.transform.localPosition = new Vector3(-61.96f, 34.7f, 0);
        pipe.SetActive(false);
        sewerFlat.SetActive(false);
        sewerRound.SetActive(false);
        burbRound1.SetActive(false);
        burbRound2.SetActive(false);
        burbFlet1.SetActive(false);
        burbFlet2.SetActive(false);
        ladder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0);
        hole.SetActive(false);
        cage.SetActive(false);
        sideRoad.SetActive(false);

        sewerFlat.SetActive(true);
        burbFlet2.SetActive(true);
        land2.SetActive(true);
        house.SetActive(true);
        groundWater.SetActive(false);
        groundWaterLand2.SetActive(true);
    }
    public void TestLand()
    {
        ladder.transform.localPosition = new Vector3(-60.49f, 8.79f, 0);
    }
    public void MakeSighnLand()
    {
        symbol.transform.localPosition = new Vector3(-61.96f, 9.48f, 0);
        pipe.SetActive(false);
        sewerFlat.SetActive(false);
        sewerRound.SetActive(false);
        burbRound1.SetActive(false);
        burbRound2.SetActive(false);
        burbFlet1.SetActive(false);
        burbFlet2.SetActive(false);
        ladder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0);
        hole.SetActive(false);
        cage.SetActive(false);
        sideRoad.SetActive(false);
        house.SetActive(false);

        land2.SetActive(false);
        sewerFlat.SetActive(true);
        burbFlet1.SetActive(true);
        burbFlet2.SetActive(true);
        pipe.SetActive(true);
        groundWater.SetActive(true);
        groundWaterLand2.SetActive(false);
    }
    public void EnemySpawn()
    {
        DataBaseManager.StoryDirecting = true;
    }
    public void MakeSewer()
    {
        enemy.SetActive(false);
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
                        enemy.SetActive(true);
                    }
                    else if (DataBaseManager.SewerEnemyCounter == 2)
                    {
                        enemy2.SetActive(true);
                    }
                    else
                    {
                        enemy.SetActive(true);
                        enemy2.SetActive(true);
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
                        enemy.SetActive(true);
                        DataBaseManager.IsInsmusMeetSewer = false;
                    }
                    else
                    {
                        enemy.SetActive(true);
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
    Vector2 playerSewerRight = new Vector3(-1194.19f, -193.57f);
    Vector3 camSewerRight = new Vector3(-1217.02f, -195f, -15);
    float chInRommSize = 2.383215f; // -이면 왼쪽 +면 오른쪽
    public void SewerMapMove()
    {
        TimeManagere.Instance.MakeSighnLand();
        DataBaseManager.isDirecting = true;
        player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = playerSewerRight;
        camera.transform.localPosition = camSewerRight;
        TimeManagere.Instance.DeletRightSewerPotal();
    }
    public void MakeSewerMap_Nomal()
    {
        symbol.transform.localPosition = new Vector3(-61.96f, 34.7f, 0);
        enemy.SetActive(false);
        enemy2.SetActive(false);
        pipe.SetActive(false);
        sewerFlat.SetActive(false);
        sewerRound.SetActive(false);
        burbRound1.SetActive(false);
        burbRound2.SetActive(false);
        burbFlet1.SetActive(false);
        burbFlet2.SetActive(false);
        ladder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0);
        hole.SetActive(false);
        cage.SetActive(false);
        sideRoad.SetActive(false);
        house.SetActive(false);
        land2.SetActive(false);

        groundWater.SetActive(true);
        groundWaterLand2.SetActive(false);

        int Sewer = Random.Range(0, 2);
        if (Sewer == 1)
        {
            sewerFlat.SetActive(true);
            sewerRound.SetActive(false);

            int FlatLight1 = Random.Range(0, 2);
            if (FlatLight1 == 0)
            {
                burbFlet1.SetActive(true);
            }
            else
            {
                burbFlet1.SetActive(false);
            }
            int FlatLight2 = Random.Range(0, 2);
            if (FlatLight2 == 0)
            {
                burbFlet2.SetActive(true);
            }
            else
            {
                burbFlet2.SetActive(false);
            }
            int Side = Random.Range(0, 2);
            if (Side == 0)
            {
                sideRoad.SetActive(true);
            }
            else
            {
                sideRoad.SetActive(false);

                int HoleInt = Random.Range(0, 2);
                if (HoleInt == 0)
                {
                    hole.SetActive(true);
                }
                else
                {
                    hole.SetActive(false);
                }
                int Ledderint = Random.Range(0, 2);
                if (Ledderint == 0)
                {
                    ladder.transform.localPosition = new Vector3(-60.49f, 8.79f, 0);
                }
                else
                {
                    ladder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0);
                }
            }
        }
        else
        {
            sewerFlat.SetActive(false);
            sewerRound.SetActive(true);
            int RoundLight1 = Random.Range(0, 2);
            if (RoundLight1 == 0)
            {
                burbRound1.SetActive(true);
            }
            else
            {
                burbRound1.SetActive(false);
            }
            int RoundLight2 = Random.Range(0, 2);
            if (RoundLight2 == 0)
            {
                burbRound2.SetActive(true);
            }
            else
            {
                burbRound2.SetActive(false);
            }
        }
        int Pipeint = Random.Range(0, 2);
        if (Pipeint == 0)
        {
            pipe.SetActive(true);
        }
        else
        {
            pipe.SetActive(false);
        }
    }
    public void MakeSewerMap_Enemy()
    {
        symbol.transform.localPosition = new Vector3(-61.96f, 34.7f, 0);
        groundWater.SetActive(true);
        pipe.SetActive(false);
        sewerFlat.SetActive(false);
        sewerRound.SetActive(false);
        burbRound1.SetActive(false);
        burbRound2.SetActive(false);
        burbFlet1.SetActive(false);
        burbFlet2.SetActive(false);
        ladder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0);
        hole.SetActive(false);
        cage.SetActive(false);
        sideRoad.SetActive(false);
        house.SetActive(false);
        land2.SetActive(false);

        groundWaterBattle.SetActive(true);
        groundWaterLand2Battle.SetActive(false);

        groundWaterBattle.SetActive(true);
        pipeBattle.SetActive(false);
        sewerFlatBattle.SetActive(false);
        sewerRoundBattle.SetActive(false);
        burbRound1Battle.SetActive(false);
        burbRound2Battle.SetActive(false);
        burbFlet1Battle.SetActive(false);
        burbFlet2Battle.SetActive(false);
        ladderBattle.SetActive(false);
        holeBattle.SetActive(false);
        cageBattle.SetActive(false);
        sideRoadBattle.SetActive(false);
        houseBattle.SetActive(false);
        land2Battle.SetActive(false);

        groundWaterBattle.SetActive(true);
        groundWaterLand2Battle.SetActive(false);

        int Sewer = Random.Range(0, 2);
        if (Sewer == 1)
        {
            sewerFlat.SetActive(true);
            sewerRound.SetActive(false);

            sewerFlatBattle.SetActive(true);
            sewerRoundBattle.SetActive(false);

            int FlatLight1 = Random.Range(0, 2);
            if (FlatLight1 == 0)
            {
                burbFlet1.SetActive(true);
                burbFlet1Battle.SetActive(true);
            }
            else
            {
                burbFlet1.SetActive(false);
                burbFlet1Battle.SetActive(false);
            }
            int FlatLight2 = Random.Range(0, 2);
            if (FlatLight2 == 0)
            {
                burbFlet2.SetActive(true);
                burbFlet2Battle.SetActive(true);
            }
            else
            {
                burbFlet2.SetActive(false);
                burbFlet2Battle.SetActive(false);
            }

            int HoleInt = Random.Range(0, 2);
            if (HoleInt == 0)
            {
                hole.SetActive(true);
                holeBattle.SetActive(true);
            }
            else
            {
                hole.SetActive(false);
                holeBattle.SetActive(false);
            }
        }
        else
        {
            sewerFlat.SetActive(false);
            sewerRound.SetActive(true);

            sewerFlatBattle.SetActive(false);
            sewerRoundBattle.SetActive(true);
            int RoundLight1 = Random.Range(0, 2);
            if (RoundLight1 == 0)
            {
                burbRound1.SetActive(true);
                burbRound1Battle.SetActive(true);
            }
            else
            {
                burbRound1.SetActive(false);
                burbRound1Battle.SetActive(false);
            }
            int RoundLight2 = Random.Range(0, 2);
            if (RoundLight2 == 0)
            {
                burbRound2.SetActive(true);
                burbRound2Battle.SetActive(true);
            }
            else
            {
                burbRound2.SetActive(false);
                burbRound2Battle.SetActive(false);
            }
        }
        int Pipeint = Random.Range(0, 2);
        if (Pipeint == 0)
        {
            pipe.SetActive(true);
            pipeBattle.SetActive(true);
        }
        else
        {
            pipe.SetActive(false);
            pipeBattle.SetActive(false);
        }
    }
    public void Sewer_ByRiverSide()
    {
        symbol.transform.localPosition = new Vector3(-61.96f, 34.7f, 0);
        pipe.SetActive(false);
        sewerFlat.SetActive(false);
        sewerRound.SetActive(false);
        burbRound1.SetActive(false);
        burbRound2.SetActive(false);
        burbFlet1.SetActive(false);
        burbFlet2.SetActive(false);
        ladder.transform.localPosition = new Vector3(-60.49f, 30.9f, 0); ;
        hole.SetActive(false);
        cage.SetActive(false);
        sideRoad.SetActive(false);
        house.SetActive(false);
        groundWater.SetActive(true);
        groundWaterLand2.SetActive(false);

        int Sewer = Random.Range(0, 2);
        if (Sewer == 1)
        {
            sewerFlat.SetActive(true);
            sewerRound.SetActive(false);

            int FlatLight1 = Random.Range(0, 2);
            if (FlatLight1 == 0)
            {
                burbFlet1.SetActive(true);
            }
            else
            {
                burbFlet1.SetActive(false);
            }

            int FlatLight2 = Random.Range(0, 2);
            if (FlatLight2 == 0)
            {
                burbFlet2.SetActive(true);
            }
            else
            {
                burbFlet2.SetActive(false);
            }
            int Side = Random.Range(0, 2);
            if (Side == 0)
            {
                sideRoad.SetActive(true);
            }
            else
            {
                sideRoad.SetActive(false);

                int HoleInt = Random.Range(0, 2);
                if (HoleInt == 0)
                {
                    hole.SetActive(true);
                }
                else
                {
                    hole.SetActive(false);
                }
            }
        }
        else
        {
            sewerFlat.SetActive(false);
            sewerRound.SetActive(true);
            int RoundLight1 = Random.Range(0, 2);
            if (RoundLight1 == 0)
            {
                burbRound1.SetActive(true);
            }
            else
            {
                burbRound1.SetActive(false);
            }
            int RoundLight2 = Random.Range(0, 2);
            if (RoundLight2 == 0)
            {
                burbRound2.SetActive(true);
            }
            else
            {
                burbRound2.SetActive(false);
            }
        }

        int Pipeint = Random.Range(0, 2);
        if (Pipeint == 0)
        {
            pipe.SetActive(true);
        }
        else
        {
            pipe.SetActive(false);
        }
    }

    public GameObject policeLine;

    public GameObject bfNoonEvent;
    public GameObject afNoonEvent;
    public GameObject witness;
    public GameObject deadBody;
    public GameObject deadSite;

    public GameObject kane;
    public GameObject student;
    public GameObject barPeople1;
    public GameObject barPeople2;
    public GameObject albert;
    public GameObject dave;
    public GameObject swain;
    public GameObject journalist;

    public Intel_ObtoUI meave;

    public GameObject sewerDoorDialog;
    public GameObject sewerDoorPotal;

    public Intel_ObtoUI sewerOfficeWindow;
    public Intel_ObtoUI sewerOfficeLocker;
    public Intel_ObtoUI blackWell;
    public GameObject blackWellOb;

    public GameObject riverActive;
    public GameObject riverPotal;

    public GameObject leftSewerPotal;
    public GameObject rightSewerPotal;
    public void DeletRightSewerPotal()
    {
        leftSewerPotal.SetActive(true);
        rightSewerPotal.SetActive(false);
    }
    public void DeletLeftSewerPotal()
    {
        leftSewerPotal.SetActive(false);
        rightSewerPotal.SetActive(true);
    }
    public GameObject manhole;

    void Update()
    {
        BulbCheck();
        if (endDoorDetective.activeSelf == false && DataBaseManager.EndingDoorCheck == true)
        {
            endDoorDetective.SetActive(true);
        }
        if (manhole.activeSelf == false && DataBaseManager.DeadBody_GotoSwere == true)
        {
            manhole.SetActive(true);
        }
        if (riverActive.activeSelf == true && (DataBaseManager.SewerPicingSucc == true || DataBaseManager.SewerageKey == true))
        {
            riverPotal.SetActive(true);
            riverActive.SetActive(false);
        }
        if (DataBaseManager.Black_RechSucc_CanTalck == true && blackWell.CanKeyword == false)
        {
            blackWell.CanKeyword = true;
        }
        if ((DataBaseManager.TimeCount % 4 == 1) || (DataBaseManager.TimeCount % 4 == 2) && DataBaseManager.SewerDoorOpen == false)
        {
            DataBaseManager.SewerDoorOpen = true;
            sewerDoorPotal.transform.localPosition = new Vector3(-65.10986f, 5, 0);
            sewerDoorDialog.transform.localPosition = new Vector3(-65.10986f, 33, 0);
            blackWellOb.transform.localPosition = new Vector3(-224.6f, 4.08f, 0);
            sewerOfficeWindow.CanJudge = false;
            sewerOfficeLocker.CanJudge = false;
        }
        if ((DataBaseManager.TimeCount % 4 == 3) || (DataBaseManager.TimeCount % 4 == 0) && DataBaseManager.SewerDoorOpen == true)
        {
            blackWellOb.transform.localPosition = new Vector3(-224.6f, 29.6f, 0);
            DataBaseManager.SewerDoorOpen = false;
            if (DataBaseManager.SewerDoorOpenDeftness == true)
            {
                sewerDoorPotal.transform.localPosition = new Vector3(-65.10986f, 5, 0);
                sewerDoorDialog.transform.localPosition = new Vector3(-65.10986f, 33, 0);
            }
            else
            {
                sewerDoorPotal.transform.localPosition = new Vector3(-65.10986f, 33, 0);
                sewerDoorDialog.transform.localPosition = new Vector3(-65.10986f, 5, 0);
            }

            if (DataBaseManager.TryBrokenWindow == true)
            {
                sewerOfficeWindow.CanJudge = false;
            }
            else
            {
                sewerOfficeWindow.CanJudge = true;
            }
            if (DataBaseManager.TryObserLokcer == true)
            {
                sewerOfficeLocker.CanJudge = false;
            }
            else
            {
                sewerOfficeLocker.CanJudge = true;
            }
        }
        if (DataBaseManager.SewerDoorOpenCheck == true)
        {
            DataBaseManager.SewerDoorOpenCheck = false;
            sewerDoorPotal.transform.localPosition = new Vector3(-65.10986f, 5, 0);
            sewerDoorDialog.transform.localPosition = new Vector3(-65.10986f, 33, 0);
        }

        if (DataBaseManager.TimeCount >= 6 && afNoonEvent.transform.localPosition.y > 30)
        {
            bfNoonEvent.transform.localPosition = new Vector3(0, 60, 0);
            afNoonEvent.transform.localPosition = new Vector3(0, 0, 0);
            policeLine.transform.localPosition = new Vector3(-65.88f, 4.96f, 0);
        }
        if (DataBaseManager.TimeCount >= 7 && witness.activeSelf == true)
        {
            meave.CanJudge = true;
            witness.SetActive(false);
            deadBody.SetActive(false);
            deadSite.transform.localPosition = new Vector3(-250.6001f, 0.4400024f, 0);
            kane.transform.localPosition = new Vector3(253.27f, -79.6f, 0);
            student.transform.localPosition = new Vector3(-41.67f, 40.9f, 0);
        }
        if (DataBaseManager.TimeCount >= 11 && meave.CanJudge == true)
        {
            meave.CanJudge = false;
        }
        if (DataBaseManager.TimeCount == 8 && barPeople1.activeSelf == true)
        {
            barPeople1.SetActive(false);
            barPeople2.SetActive(false);
        }
        if (DataBaseManager.AlbertDeathKeyword && albert.activeSelf == true && DataBaseManager.TimeCount % 4 == 0 && DataBaseManager.TimeCount >= 8)
        {
            DataBaseManager.ThirdDayPoliceADialog = true;
            DataBaseManager.ThirdDayPoliceBDialog = true;
            albert.SetActive(false);
        }
        if (DataBaseManager.DaveDeathKeyword && dave.activeSelf == true && DataBaseManager.TimeCount % 4 == 0)
        {
            DataBaseManager.AfterDaveDeathFirstSlum = true;
            dave.SetActive(false);
        }
        if (DataBaseManager.SwainDeathKeyword && swain.activeSelf == true && DataBaseManager.TimeCount % 4 == 0)
        {
            journalist.transform.localPosition = new Vector3(-61.37f, 3.4f, 0);
            swain.SetActive(false);
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
