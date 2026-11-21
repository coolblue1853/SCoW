using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using SCoW.Core.State;
using SCoW.Core.Story;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 월드 스냅샷 동치 도구(C-2 P13). 플레이 모드 Main 씬에서 TimeManagere 가 가리키는 씬 오브젝트(활성·localPosition·canJudge·canKeyword·스프라이트·조명 시간)를
/// 상태 프리셋(timeCount 0~12 × 플래그 조합)마다 "월드 동기화 1회" 돌린 뒤 기준 상태 대비 바뀐 것만 한 줄로 덤프한다.
/// 월드 동기화 1회 = 옛 코드는 TimeManagere.Update(리플렉션), 새 코드는 WorldBinder.EvaluateNow. 같은 도구로 골든(옛 코드)과 현재(새 코드)를 diff 한다.
/// 줄: 프리셋|f1=(1회 평가로 바뀐 것)|f2=(2회째에 더 바뀐 것, 없으면 same)|state=(상태 변화). 마지막 chain 구간은 프리셋을 되돌리지 않고 이어서 돌린다.
/// 실행: execute_code 로 WorldSnapshotTool.DumpTo(경로) 호출 또는 SCoW/World 메뉴.
/// </summary>
public static class WorldSnapshotTool
{
    public const string GoldenPath = "Assets/Tests/EditMode/Golden/WorldP13.golden.txt";
    public const string CurrentPath = "Logs/playtest/WorldP13.current.txt";

    [MenuItem("SCoW/World/Dump P13 Golden (Play Mode)")]
    private static void MenuGolden() { DumpTo(GoldenPath); }

    [MenuItem("SCoW/World/Dump P13 Current (Play Mode)")]
    private static void MenuCurrent() { DumpTo(CurrentPath); }

    private static readonly string[] DoorFlags =
    {
        FlagKeys.sewerDoorOpen, FlagKeys.sewerDoorOpenDeftness, FlagKeys.sewerDoorOpenCheck
    };

    private static readonly string[] OtherFlags =
    {
        FlagKeys.endingDoorCheck, FlagKeys.deadBodyGotoSwere, FlagKeys.sewerPicingSucc, FlagKeys.sewerageKey,
        FlagKeys.blackRechSuccCanTalck, FlagKeys.tryBrokenWindow, FlagKeys.tryObserLokcer,
        FlagKeys.albertDeathKeyword, FlagKeys.daveDeathKeyword, FlagKeys.swainDeathKeyword
    };

    private static readonly string[] WatchedFlags =
    {
        FlagKeys.sewerDoorOpen, FlagKeys.sewerDoorOpenCheck, FlagKeys.thirdDayPoliceADialog,
        FlagKeys.thirdDayPoliceBDialog, FlagKeys.afterDaveDeathFirstSlum
    };

    private sealed class Member
    {
        public string Name;
        public UnityEngine.Object Target;
    }

    private sealed class Snap
    {
        public Dictionary<string, string> Text = new Dictionary<string, string>();
        public List<Action> Restore = new List<Action>();
    }

    public static void DumpTo(string path)
    {
        TimeManagere tm = TimeManagere.Instance;
        if (!EditorApplication.isPlaying || tm == null)
        {
            Debug.LogError("WorldSnapshotTool: Main 씬 플레이 모드에서만 실행한다.");
            return;
        }
        GameState saved = GameRoot.Instance.State.Clone();
        long savedPlay = GameRoot.Instance.PlayTimeSec;
        // 기준 상태 = 지금 상태로 월드를 한 번 평가한 것(옛 코드는 매 프레임 Update 가 이미 한 것과 같다).
        Evaluate(tm);
        Snap baseline = Capture(tm);
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("# WorldP13 golden v1\n");
            List<string[]> presets = BuildPresets();
            for (int i = 0; i < presets.Count; i++)
            {
                Restore(baseline);
                RunPreset(sb, tm, baseline, presets[i][0], presets[i][1]);
            }
            // chain: 되돌리지 않고 이어서 돌린다(시간 흐름·상태 누적)
            Restore(baseline);
            Snap prev = Capture(tm);
            for (int t = 0; t <= 13; t++)
            {
                string spec = "counter:timeCount=" + t + (t >= 5 ? ";flag:albertDeathKeyword=1;flag:daveDeathKeyword=1;flag:swainDeathKeyword=1" : "");
                prev = RunPreset(sb, tm, prev, "chain" + t, spec);
            }
            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir)) { Directory.CreateDirectory(dir); }
            File.WriteAllText(path, sb.ToString(), new UTF8Encoding(false));
            Debug.Log("WorldSnapshotTool: 덤프 완료 " + path + " (" + presets.Count + " 프리셋)");
        }
        finally
        {
            Restore(baseline);
            GameRoot.Instance.LoadState(saved, savedPlay);
            Evaluate(tm);
        }
    }

    private static List<string[]> BuildPresets()
    {
        List<string[]> list = new List<string[]>();
        for (int t = 0; t <= 12; t++)
        {
            string ts = "counter:timeCount=" + t;
            list.Add(new[] { "t" + t + "/base", ts });
            string all = ts;
            for (int i = 0; i < DoorFlags.Length; i++) { all += ";flag:" + DoorFlags[i] + "=1"; }
            for (int i = 0; i < OtherFlags.Length; i++) { all += ";flag:" + OtherFlags[i] + "=1"; }
            list.Add(new[] { "t" + t + "/all", all });
            for (int i = 0; i < OtherFlags.Length; i++)
            {
                list.Add(new[] { "t" + t + "/" + OtherFlags[i], ts + ";flag:" + OtherFlags[i] + "=1" });
            }
            for (int m = 1; m < 8; m++)
            {
                string spec = ts;
                string name = "t" + t + "/door";
                for (int b = 0; b < 3; b++)
                {
                    if ((m & (1 << b)) != 0) { spec += ";flag:" + DoorFlags[b] + "=1"; name += "+" + DoorFlags[b]; }
                }
                list.Add(new[] { name, spec });
                // 창·서랍 시도와 문 상태 조합
                list.Add(new[] { name + "+tries", spec + ";flag:" + FlagKeys.tryBrokenWindow + "=1;flag:" + FlagKeys.tryObserLokcer + "=1" });
            }
        }
        return list;
    }

    private static Snap RunPreset(StringBuilder sb, TimeManagere tm, Snap before, string name, string spec)
    {
        GameState gs = StatePreset.Build(spec);
        GameRoot.Instance.LoadState(gs, 0);
        GameState stateBefore = GameRoot.Instance.State.Clone();
        Evaluate(tm);
        Snap f1 = Capture(tm);
        GameState stateAfter1 = GameRoot.Instance.State.Clone();
        Evaluate(tm);
        Snap f2 = Capture(tm);
        sb.Append(name).Append('|').Append("f1=").Append(Diff(before, f1))
            .Append("|f2=").Append(Diff(f1, f2))
            .Append("|state=").Append(StateDiff(stateBefore, stateAfter1))
            .Append('\n');
        return f1;
    }

    private static void Evaluate(TimeManagere tm)
    {
        MethodInfo old = typeof(TimeManagere).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly);
        if (old != null)
        {
            old.Invoke(tm, null);
            return;
        }
        Type binder = Type.GetType("WorldBinder, Assembly-CSharp");
        Component c = binder == null ? null : tm.GetComponent(binder);
        if (c == null) { throw new InvalidOperationException("월드 동기화 진입점 없음(Update/WorldBinder)"); }
        binder.GetMethod("EvaluateNow").Invoke(c, null);
    }

    private static string StateDiff(GameState a, GameState b)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < WatchedFlags.Length; i++)
        {
            bool x = a.GetFlag(WatchedFlags[i]);
            bool y = b.GetFlag(WatchedFlags[i]);
            if (x != y) { sb.Append(WatchedFlags[i]).Append(y ? "+" : "-").Append(';'); }
        }
        return sb.Length == 0 ? "same" : sb.ToString();
    }

    private static string Diff(Snap a, Snap b)
    {
        StringBuilder sb = new StringBuilder();
        foreach (KeyValuePair<string, string> kv in b.Text)
        {
            string old;
            if (!a.Text.TryGetValue(kv.Key, out old) || old != kv.Value)
            {
                sb.Append(kv.Key).Append('=').Append(kv.Value).Append(';');
            }
        }
        return sb.Length == 0 ? "same" : sb.ToString();
    }

    private static List<Member> Members(TimeManagere tm)
    {
        List<Member> list = new List<Member>();
        FieldInfo[] fields = typeof(TimeManagere).GetFields(BindingFlags.Public | BindingFlags.Instance);
        for (int i = 0; i < fields.Length; i++)
        {
            Type ft = fields[i].FieldType;
            if (ft != typeof(GameObject) && ft != typeof(Intel_ObtoUI) && ft != typeof(SpriteRenderer) && ft != typeof(LightColorController)) { continue; }
            UnityEngine.Object o = fields[i].GetValue(tm) as UnityEngine.Object;
            if (o == null) { continue; }
            list.Add(new Member { Name = fields[i].Name, Target = o });
        }
        return list;
    }

    private static string F(float v) { return v.ToString("R", CultureInfo.InvariantCulture); }

    private static Snap Capture(TimeManagere tm)
    {
        Snap s = new Snap();
        List<Member> ms = Members(tm);
        for (int i = 0; i < ms.Count; i++)
        {
            string n = ms[i].Name;
            GameObject go = ms[i].Target as GameObject;
            Intel_ObtoUI intel = ms[i].Target as Intel_ObtoUI;
            SpriteRenderer sr = ms[i].Target as SpriteRenderer;
            LightColorController lc = ms[i].Target as LightColorController;
            if (go != null)
            {
                Vector3 p = go.transform.localPosition;
                bool active = go.activeSelf;
                s.Text[n] = "a" + (active ? 1 : 0) + " p" + F(p.x) + "," + F(p.y) + "," + F(p.z);
                s.Restore.Add(() => { go.SetActive(active); go.transform.localPosition = p; });
            }
            else if (intel != null)
            {
                bool j = intel.canJudge;
                bool k = intel.canKeyword;
                s.Text[n] = "j" + (j ? 1 : 0) + " k" + (k ? 1 : 0);
                s.Restore.Add(() => { intel.canJudge = j; intel.canKeyword = k; });
            }
            else if (sr != null)
            {
                Sprite sp = sr.sprite;
                s.Text[n] = "spr:" + (sp == null ? "null" : sp.name);
                s.Restore.Add(() => { sr.sprite = sp; });
            }
            else if (lc != null)
            {
                float t = lc.time;
                s.Text[n] = "time" + F(t);
                s.Restore.Add(() => { lc.time = t; });
            }
        }
        return s;
    }

    private static void Restore(Snap s)
    {
        for (int i = 0; i < s.Restore.Count; i++) { s.Restore[i](); }
    }
}
