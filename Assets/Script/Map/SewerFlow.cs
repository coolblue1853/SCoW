using SCoW.Core.Sewer;
using SCoW.Core.State;
using UnityEngine;

/// <summary>
/// 하수도 이동 흐름(SEWER-1 S1). 규칙은 Core(SewerNav·SewerState), 이 클래스는 GameState 와 씬을 잇는다.
/// 진입·문 이동·불러오기 복원·지도 순간이동·전투 승리 기록. 전투 진입·SAN 판정 대사 호출은 옛 지점(PrintBattleDialog 등)을 그대로 쓴다.
/// </summary>
public static class SewerFlow
{
    private const float RoomSize = 2.383215f;

    // 옛 PotalPoint 의 도착 좌표(문별). Left = 왼쪽 문 옆, Right = 오른쪽 문 옆, Middle = 샛길 앞.
    private static readonly Vector3 PlayerLeft = new Vector3(-1243.64f, -193.57f);
    private static readonly Vector3 CamLeft = new Vector3(-1221.5f, -195f, -15f);
    private static readonly Vector3 PlayerRight = new Vector3(-1194.19f, -193.57f);
    private static readonly Vector3 CamRight = new Vector3(-1217.02f, -195f, -15f);
    private static readonly Vector3 PlayerMiddle = new Vector3(-1211.05f, -194.02f);
    private static readonly Vector3 CamMiddle = new Vector3(-1217.72f, -195f, -15f);

    private static SewerGraph _graph;

    private static GameState State
    {
        get { return GameRoot.Instance.State; }
    }

    /// <summary>현재 시드의 그래프(시드가 바뀌면 다시 만든다).</summary>
    public static SewerGraph Graph
    {
        get
        {
            int seed = SewerState.EnsureSeed(State, UnityRng.Instance);
            if (_graph == null || _graph.Seed != seed)
            {
                _graph = SewerGraph.Create(seed);
            }
            return _graph;
        }
    }

    public static Vector3 PlayerPos(SewerDoor arrival)
    {
        return arrival == SewerDoor.Right ? PlayerRight : (arrival == SewerDoor.Middle ? PlayerMiddle : PlayerLeft);
    }

    public static Vector3 CamPos(SewerDoor arrival)
    {
        return arrival == SewerDoor.Right ? CamRight : (arrival == SewerDoor.Middle ? CamMiddle : CamLeft);
    }

    /// <summary>플레이어가 바라볼 방향(localScale.x 부호 포함 크기). 오른쪽 문 옆에서는 왼쪽을 본다.</summary>
    public static float Facing(SewerDoor arrival)
    {
        return arrival == SewerDoor.Right ? -RoomSize : RoomSize;
    }

    /// <summary>강변 맨홀로 진입. 항상 입구(z0)에서 시작하고 진행은 보존한다.</summary>
    public static void Enter()
    {
        SewerRoomDef d = SewerNav.Begin(State, Graph);
        SewerRoomApplier.Apply(TimeManagere.Instance, d);
    }

    /// <summary>지금 방을 다시 그린다(연출로 플레이어만 옮긴 뒤 등).</summary>
    public static void ApplyCurrent()
    {
        SewerRoomDef d = Graph.Get(SewerState.Room(State));
        if (d == null)
        {
            d = SewerNav.Begin(State, Graph);
        }
        SewerRoomApplier.Apply(TimeManagere.Instance, d);
    }

    /// <summary>문 이름(Sewer_Left/Right/Middle)이 하수도 문이면 true 와 문 종류.</summary>
    public static bool TryDoor(string potalName, out SewerDoor door)
    {
        door = SewerDoor.Left;
        if (potalName == "Sewer_Left")
        {
            return true;
        }
        if (potalName == "Sewer_Right")
        {
            door = SewerDoor.Right;
            return true;
        }
        if (potalName == "Sewer_Middle")
        {
            door = SewerDoor.Middle;
            return true;
        }
        return false;
    }

    /// <summary>하수도 안에서 이 문을 지날 수 없으면 true(출구 없음·잠김). 포탈 입력 단계에서 페이드 전에 부른다.</summary>
    public static bool IsBlocked(string potalName)
    {
        SewerDoor door;
        if (DataBaseManager.nowPlace != SewerLegacy.SewerPlace || !TryDoor(potalName, out door))
        {
            return false;
        }
        return SewerNav.Check(State, Graph, door, DataBaseManager.gainMap) != SewerMoveStatus.Moved;
    }

    /// <summary>문 이동. 방을 바꾸고 외형을 적용하고 조우가 있으면 옛 방식으로 예고한다. 이동하지 못하면 null.</summary>
    public static SewerMove Move(SewerDoor door)
    {
        SewerMove m = SewerNav.Move(State, Graph, door, DataBaseManager.gainMap);
        if (m.Status != SewerMoveStatus.Moved)
        {
            return null;
        }
        SewerRoomApplier.Apply(TimeManagere.Instance, m.To);
        Present(m.Encounter);
        OnArrived(m.To, m.Encounter);
        return m;
    }

    /// <summary>지도로 문양실에 간다(옛 SewerMapMove). 도착 문(오른쪽)을 돌려준다.</summary>
    public static SewerDoor JumpToSymbol()
    {
        SewerMove m = SewerNav.Jump(State, Graph, SewerGraph.SymbolRoomId);
        SewerRoomApplier.Apply(TimeManagere.Instance, m.To);
        return m.Arrival;
    }

    /// <summary>
    /// 하수도에서 불러온 뒤 방을 복원한다. 출시판 세이브(방 정보 없음)는 SewerLegacy 로 대응 방에 옮긴다.
    /// 플레이어를 세울 문을 돌려준다(입구는 위쪽 맨홀 쪽 왼쪽, 나머지는 돌아온 사람처럼 오른쪽이 아닌 왼쪽).
    /// </summary>
    public static SewerDoor RestoreCurrent()
    {
        SewerLegacy.Adopt(State, UnityRng.Instance);
        SewerRoomDef d = Graph.Get(SewerState.Room(State));
        if (d == null)
        {
            d = SewerNav.Begin(State, Graph);
        }
        SewerRoomApplier.Apply(TimeManagere.Instance, d);
        Present(SewerNav.PendingEncounter(State, d));
        return SewerDoor.Left;
    }

    /// <summary>전투 승리: 지금 방의 적을 처치로 기록하고 맵의 적 표시를 끈다.</summary>
    public static void OnBattleWon()
    {
        if (DataBaseManager.nowPlace != SewerLegacy.SewerPlace)
        {
            return;
        }
        if (SewerNav.DefeatCurrent(State, Graph))
        {
            SewerRoomApplier.HideEnemies(TimeManagere.Instance);
        }
    }

    /// <summary>지금 방이 휴식 구역인가(시야 마스크가 읽는다).</summary>
    public static bool InRestRoom()
    {
        SewerRoomDef d = Graph.Get(SewerState.Room(State));
        return d != null && d.Rest;
    }

    // 새 방에 도착한 뒤: 휴식 구역이면 SAN 회복(처음 한 번), 아니면 조우 전조(SAN 연동 깜빡임·흔들림).
    private static void OnArrived(SewerRoomDef room, int encounter)
    {
        int gain = SewerRest.Recover(State, room, UnityRng.Instance);
        if (gain > 0)
        {
            SaveToast.Show("Rested a moment. SAN +" + gain, false); // TODO(I2)
        }
        if (encounter > 0 || room.Rest)
        {
            return;
        }
        int distance = SewerEncounters.OmenDistance(State, Graph, room.Id);
        float ratio = SewerSight.Ratio(DataBaseManager.nowSan, DataBaseManager.san);
        SewerSightView.PlayOmen(SewerEncounters.Omen(distance, ratio));
    }

    // 옛 MakeSewer 의 적 등장 처리: 적 표시, 코트면 변장 판정만, 아니면 1초 뒤 조우 대사.
    private static void Present(int encounter)
    {
        if (encounter <= 0)
        {
            return;
        }
        TimeManagere t = TimeManagere.Instance;
        SewerRoomApplier.ShowEnemies(t, encounter);
        if (DataBaseManager.wearCoat)
        {
            DataBaseManager.isInsmusMeetSewer = false;
        }
        else
        {
            t.Invoke("PrintBattleDialog", 1f);
        }
    }
}
