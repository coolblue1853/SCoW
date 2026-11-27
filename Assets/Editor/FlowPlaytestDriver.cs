using System.IO;
using System.Reflection;
using System.Text;
using SCoW.Core.Sewer;
using SCoW.Core.State;
using UnityEngine;

/// <summary>
/// 흐름 실플레이 검증 도구 2차(에디터 플레이 모드 전용). 하수도 문 이동·조사 포인트·미니맵·동료 합류·P0 구제·엔딩을
/// 실제 공개 메서드(PotalPoint.MovePlayer, SewerInvestigatePoint.Investigate 등)로 진행한다. 세이브 슬롯은 읽기만 한다.
/// execute_code 에서 FlowPlaytestDriver.* 를 부른다.
/// </summary>
public static class FlowPlaytestDriver
{
    private const BindingFlags Bf = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    private static GameState State { get { return GameRoot.Instance.State; } }

    /// <summary>세이브 폴더 파일 목록과 수정 시각(쓰기 감시용).</summary>
    public static string SaveSnapshot()
    {
        StringBuilder sb = new StringBuilder();
        string dir = Application.persistentDataPath;
        if (Directory.Exists(dir))
        {
            foreach (string f in Directory.GetFiles(dir, "*.es3"))
            {
                sb.Append(Path.GetFileName(f)).Append('=').Append(File.GetLastWriteTimeUtc(f).Ticks).Append(';');
            }
        }
        return sb.ToString();
    }

    /// <summary>정상 캐릭터 상태를 만들고 하수도 입구에 선다(Sewer_Potal 의 위치 이동 + SewerFlow.Enter).</summary>
    public static string SewerSetup(int hp, int san)
    {
        GameState s = State;
        s.SetCounter("str", 60); s.SetCounter("dex", 60); s.SetCounter("intl", 70); s.SetCounter("luk", 55); s.SetCounter("weal", 50);
        s.SetCounter("hp", 60); s.SetCounter("nowHP", hp); s.SetCounter("san", 65); s.SetCounter("nowSan", san);
        SewerState.Clear(s);
        DataBaseManager.nowPlace = "InSewer";
        DataBaseManager.isDirecting = false;
        DataBaseManager.storyDirecting = false;
        DataBaseManager.potalWait = false;
        HideDialog();
        SewerFlow.Enter();
        PotalPoint any = FindPotal(null);
        any.player.transform.localPosition = new Vector3(-1241.24f, -193.57f, 0f);
        any.camera.transform.localPosition = new Vector3(-1221.3f, -195f, -15f);
        return Info();
    }

    public static void HideDialog()
    {
        GameObject dm = (GameObject)typeof(DirectingManager).GetField("dialogManager").GetValue(DirectingManager.Instance);
        if (dm != null) { dm.SetActive(false); }
        DataBaseManager.isActiveDialog1 = false;
    }

    private static PotalPoint FindPotal(string name)
    {
        PotalPoint[] all = Object.FindObjectsByType<PotalPoint>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        PotalPoint inactive = null;
        for (int i = 0; i < all.Length; i++)
        {
            if (name == null || all[i].name == name)
            {
                if (all[i].gameObject.activeInHierarchy) { return all[i]; }
                inactive = inactive != null ? inactive : all[i];
            }
        }
        return inactive;
    }

    /// <summary>문을 지난다(Sewer_Left/Right/Middle). 막힌 문이면 트리거와 같이 IsBlocked 로 막고, 아니면 MovePlayer 를 부른다.</summary>
    public static string Door(string potalName)
    {
        PotalPoint p = FindPotal(potalName);
        if (p == null) { return "NO_POTAL " + potalName; }
        if (!p.gameObject.activeInHierarchy) { return "POTAL_INACTIVE " + potalName + " | " + Info(); }
        if (SewerFlow.IsBlocked(potalName)) { return "BLOCKED " + potalName + " | " + Info(); }
        string before = SewerState.Room(State);
        typeof(PotalPoint).GetMethod("MovePlayer", Bf).Invoke(p, null);
        DataBaseManager.isDirecting = false;
        DataBaseManager.potalWait = false;
        return before + " -" + potalName + "-> " + Info();
    }

    public static string Info()
    {
        GameState s = State;
        string room = SewerState.Room(s);
        SewerRoomDef d = SewerFlow.Graph.Get(room);
        StringBuilder sb = new StringBuilder();
        sb.Append("room=").Append(room);
        if (d != null)
        {
            sb.Append(" look=").Append(d.Look).Append(" tunnel=").Append(d.Tunnel).Append(" b1=").Append(d.Bulb1).Append(" b2=").Append(d.Bulb2);
            sb.Append(" rest=").Append(d.Rest).Append(" enemy=").Append(d.HasEnemy).Append(" pts=").Append(d.Points.Count);
            sb.Append(" L=").Append(d.HasDoor(SewerDoor.Left)).Append(" R=").Append(d.HasDoor(SewerDoor.Right)).Append(" M=").Append(d.HasDoor(SewerDoor.Middle));
        }
        PotalPoint any = FindPotal(null);
        sb.Append(" playerX=").Append(any.player.transform.localPosition.x.ToString("F1"));
        sb.Append(" visited=").Append(s.GetText(SewerKeys.Visited));
        sb.Append(" looked=").Append(s.GetText(SewerKeys.Looked));
        sb.Append(" san=").Append(DataBaseManager.nowSan).Append(" hp=").Append(DataBaseManager.nowHP);
        sb.Append(" map=").Append(SewerMinimapView.IsVisible).Append("/nodes=").Append(SewerMinimapView.NodeCount).Append("/labels=").Append(SewerMinimapView.LabelCount);
        return sb.ToString();
    }

    /// <summary>방 안 조사 포인트 위치로 플레이어를 옮기고 Investigate 를 부른다(index 번째).</summary>
    public static string Investigate(int index)
    {
        SewerInvestigatePoint[] pts = Object.FindObjectsByType<SewerInvestigatePoint>(FindObjectsSortMode.None);
        if (index >= pts.Length) { return "NO_POINT count=" + pts.Length; }
        SewerInvestigatePoint p = pts[index];
        MethodInfo m = typeof(SewerInvestigatePoint).GetMethod("Investigate", Bf);
        m.Invoke(p, null);
        return "investigated " + p.name + " | " + Info();
    }

    /// <summary>룰렛을 한 단계 밀어 준다(BattlePlaytestDriver.PumpRollet 재사용). 밀 게 있으면 true.</summary>
    public static bool PumpRollet()
    {
        MethodInfo m = typeof(BattlePlaytestDriver).GetMethod("PumpRollet", BindingFlags.Static | BindingFlags.NonPublic);
        return (bool)m.Invoke(null, null);
    }

    public static string Flag(string name)
    {
        FieldInfo f = typeof(DataBaseManager).GetField(name, BindingFlags.Static | BindingFlags.Public);
        return f == null ? "NO_FIELD" : name + "=" + f.GetValue(null);
    }
}
