using SCoW.Core.Sewer;
using UnityEngine;

/// <summary>
/// SewerRoomDef 를 기존 씬 소품(TimeManagere 의 오브젝트 필드)에 적용한다. 옛 MakeSewerMapNomal/Enemy/HouseLand/CageLand/SighnLand 의
/// if/else 와 난수를 대체하고, 전투 배경 복제(…Battle)도 같은 방 데이터로 한 번에 맞춘다. 새 그래픽은 쓰지 않는다.
/// </summary>
public static class SewerRoomApplier
{
    private static readonly Vector3 SymbolLow = new Vector3(-61.96f, 34.7f, 0f);
    private static readonly Vector3 SymbolFloor = new Vector3(-61.96f, 9.48f, 0f);
    private static readonly Vector3 LadderHigh = new Vector3(-60.49f, 30.9f, 0f);
    private static readonly Vector3 LadderLow = new Vector3(-60.49f, 8.79f, 0f);

    /// <summary>방 외형·문·적 소품을 적용한다. 적은 항상 끄고, 조우가 있는 방은 ShowEnemies 로 따로 켠다.</summary>
    public static void Apply(TimeManagere t, SewerRoomDef d)
    {
        Reset(t);
        switch (d.Look)
        {
            case SewerLook.Symbol:
                ApplySymbol(t);
                break;
            case SewerLook.House:
                ApplyHouse(t);
                break;
            case SewerLook.Cage:
                ApplyCage(t);
                break;
            default:
                ApplyNormal(t, d);
                break;
        }
        SyncBattle(t);
        SewerInvestigatePoint.Rebuild(d);
        SewerSightView.Refresh();
        t.leftSewerPotal.SetActive(d.HasDoor(SewerDoor.Left));
        t.rightSewerPotal.SetActive(d.HasDoor(SewerDoor.Right));
    }

    /// <summary>조우 번호에 따라 맵 위 적 표시. 옛 규칙: 1=적1, 2=적2, 3=둘 다.</summary>
    public static void ShowEnemies(TimeManagere t, int encounter)
    {
        t.enemyOb.SetActive(encounter == 1 || encounter >= 3);
        t.enemy2Ob.SetActive(encounter >= 2);
    }

    public static void HideEnemies(TimeManagere t)
    {
        t.enemyOb.SetActive(false);
        t.enemy2Ob.SetActive(false);
    }

    private static void Reset(TimeManagere t)
    {
        t.symbol.transform.localPosition = SymbolLow;
        HideEnemies(t);
        t.pipe.SetActive(false);
        t.sewerFlat.SetActive(false);
        t.sewerRound.SetActive(false);
        t.burbRound1.SetActive(false);
        t.burbRound2.SetActive(false);
        t.burbFlet1.SetActive(false);
        t.burbFlet2.SetActive(false);
        t.ladder.transform.localPosition = LadderHigh;
        t.hole.SetActive(false);
        t.cage.SetActive(false);
        t.sideRoad.SetActive(false);
        t.house.SetActive(false);
        t.land2.SetActive(false);
        t.groundWater.SetActive(true);
        t.groundWaterLand2.SetActive(false);
    }

    private static void ApplyNormal(TimeManagere t, SewerRoomDef d)
    {
        if (d.Tunnel == SewerTunnel.Flat)
        {
            t.sewerFlat.SetActive(true);
            t.burbFlet1.SetActive(d.Bulb1);
            t.burbFlet2.SetActive(d.Bulb2);
            if (d.SideRoad)
            {
                t.sideRoad.SetActive(true);
            }
            else
            {
                t.hole.SetActive(d.Hole);
                t.ladder.transform.localPosition = d.LadderHigh ? LadderHigh : LadderLow;
            }
        }
        else
        {
            t.sewerRound.SetActive(true);
            t.burbRound1.SetActive(d.Bulb1);
            t.burbRound2.SetActive(d.Bulb2);
        }
        t.pipe.SetActive(d.Pipe);
    }

    private static void ApplySymbol(TimeManagere t)
    {
        t.symbol.transform.localPosition = SymbolFloor;
        t.sewerFlat.SetActive(true);
        t.burbFlet1.SetActive(true);
        t.burbFlet2.SetActive(true);
        t.pipe.SetActive(true);
    }

    private static void ApplyHouse(TimeManagere t)
    {
        t.sewerFlat.SetActive(true);
        t.burbFlet2.SetActive(true);
        t.land2.SetActive(true);
        t.house.SetActive(true);
        t.groundWater.SetActive(false);
        t.groundWaterLand2.SetActive(true);
    }

    private static void ApplyCage(TimeManagere t)
    {
        t.sewerFlat.SetActive(true);
        t.burbFlet1.SetActive(true);
        t.burbFlet2.SetActive(true);
        t.pipe.SetActive(true);
        t.land2.SetActive(true);
        t.cage.SetActive(true);
        t.groundWater.SetActive(false);
        t.groundWaterLand2.SetActive(true);
    }

    // 전투 배경 복제를 지금 방 소품과 같게(옛 MakeSewerMapEnemy 가 따로 난수로 맞추던 것). 사다리·샛길은 전투 배경에 쓰지 않는다.
    private static void SyncBattle(TimeManagere t)
    {
        t.groundWaterBattle.SetActive(t.groundWater.activeSelf);
        t.groundWaterLand2Battle.SetActive(t.groundWaterLand2.activeSelf);
        t.sewerFlatBattle.SetActive(t.sewerFlat.activeSelf);
        t.sewerRoundBattle.SetActive(t.sewerRound.activeSelf);
        t.burbFlet1Battle.SetActive(t.burbFlet1.activeSelf);
        t.burbFlet2Battle.SetActive(t.burbFlet2.activeSelf);
        t.burbRound1Battle.SetActive(t.burbRound1.activeSelf);
        t.burbRound2Battle.SetActive(t.burbRound2.activeSelf);
        t.holeBattle.SetActive(t.hole.activeSelf);
        t.pipeBattle.SetActive(t.pipe.activeSelf);
        t.houseBattle.SetActive(t.house.activeSelf);
        t.cageBattle.SetActive(t.cage.activeSelf);
        t.land2Battle.SetActive(t.land2.activeSelf);
        t.ladderBattle.SetActive(false);
        t.sideRoadBattle.SetActive(false);
    }
}
