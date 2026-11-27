using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SCoW.Core.Sewer;

public class TimeManagere : MonoBehaviour
{

    public GameObject detectiveNightBulb;
    public LightColorController lightColorController;
    public GameObject enemyOb;
    public GameObject enemy2Ob;
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
    public GameObject holeBattle;
    public GameObject houseBattle;
    public GameObject land2Battle;
    public GameObject pipeBattle;
    public GameObject cageBattle;
    public GameObject groundWaterBattle;
    public GameObject groundWaterLand2Battle;
    public GameObject groundWater;
    public GameObject groundWaterLand2;

    // 월드 동기화(옛 Update·BulbCheck)는 WorldBinder + Core/World/WorldRules 로 옮겼다(C-2 P13). 이 클래스는 하수도 생성·인스펙터 참조를 맡는다.
    public void CloseOldMap()
    {
        oldMap.SetActive(false);
    }
    public void PrintBattleDialog()
    {
        // 조우별 도착 대사는 Core 데이터(SewerEncounters)에서: E1 = Sewer_FirstDeepOne, 그 외 Sewer_EnemySpawn.
        InteractionController.Instance.InSewerDialog(SewerEncounters.DialogFor(DataBaseManager.sewerEnemyCounter));
    }


    public GameObject player;
    public GameObject camera;
    float chInRommSize = 2.383215f; // -이면 왼쪽 +면 오른쪽
    // 지도로 문양실 이동: 방 상태는 SewerFlow 가 바꾼다(고정 그래프의 z5_2).
    public void SewerMapMove()
    {
        SewerDoor arrival = SewerFlow.JumpToSymbol();
        DataBaseManager.isDirecting = true;
        player.transform.localScale = new Vector3(SewerFlow.Facing(arrival), chInRommSize, 1);
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = SewerFlow.PlayerPos(arrival);
        camera.transform.localPosition = SewerFlow.CamPos(arrival);
    }
    public GameObject policeLine;
    public GameObject bfNoonEvent;
    public GameObject afNoonEvent;
    public GameObject witness;
    public GameObject deadBody;
    public GameObject site;
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

    public SpriteRenderer clientsHouseRoom;
    public Sprite messClientsHouseRoom;

    public GameObject Manhole;

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
            if (GetComponent<WorldBinder>() == null)
            {
                gameObject.AddComponent<WorldBinder>();
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
