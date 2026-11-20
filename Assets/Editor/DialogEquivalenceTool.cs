using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SCoW.Core.State;
using SCoW.Core.Story;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 대사 동치 도구(C-2 P9). 플레이 모드 Main 씬에서 InteractionController 의 장소 메서드를 상태 프리셋마다 실제로 돌려
/// 나온 대사 줄(CSV 파일:ID)과 상태 변화를 텍스트로 덤프한다. DialogManager.ShowDialogProbe 이음새로 대사창 없이 가로챈다.
/// - Dump Golden: 옛 경로(씬 interactionEvent 범위)의 기대값을 Assets/Tests/EditMode/Golden 에 쓴다(P9 에서 한 번, 커밋).
/// - Dump Current: 지금 코드가 내는 값을 Logs/playtest 에 쓴다. 위임 뒤에 골든과 diff 로 통합 확인.
/// EditMode 테스트(DialogPilotEquivalenceTests)는 같은 골든을 새 경로(StoryResolver)와 대조한다.
/// 줄 형식: 스크립트|토픽|프리셋|n=줄수|first=|last=|ids=압축ID|post=상태변화
/// </summary>
public static class DialogEquivalenceTool
{
    public const string GoldenPath = "Assets/Tests/EditMode/Golden/DialogPilot.golden.txt";
    public const string CurrentPath = "Logs/playtest/DialogPilot.current.txt";

    private static readonly string[] BarTopics =
    {
        "Maeve_FirstDialog", "Maeve_Look", "Worker_FirstDialog", "Worker_Look", "BarArrive", "Meave2ndDialog", "NoSuchTopic"
    };

    private static readonly string[] DetectiveTopics =
    {
        "start", "NewsPaper_Active", "NewsPaper_Look", "Desk_Active", "Desk_Look", "Stove_Active", "Stove_Look",
        "Directing_Nock", "Directing_Door", "Directing_AfterDoor", "Select_AboutCrime", "Select_AboutHospital",
        "Select_AboutRunAway", "Accept_Request", "Accept_AfterSelectYes", "Reject_Request", "Accept_AfterSelectNo",
        "Fab_FirstDialog", "Fab_Aiden1", "Fab_Safe1", "Fab_Nightmare1", "Fab_Nightmare2", "Fab_Nightmare3",
        "Fab_Nightmare4", "Fab_Insomnia1", "Fab_Insomnia2", "Fab_PlanetarySequence1", "Fab_Univ1", "Fab_Meiv2",
        "Fab_MissingPeople", "Fab_GhostStory1", "Fab_GhostStory2", "Fab_CreepyEye", "Fab_Worker1", "Fab_FishySmell1",
        "Fab_FishySmell2", "Fab_FishySmell3", "Fab_EndDay", "Fab_Connect", "Fab_Witness", "Fab_OwnerofUnifrom",
        "Fab_CauseofDeath", "Fab_WebbedFeet", "Fab_Runway", "Fab_FishySmellMissing", "Fab_TheClupet", "Fab_Both",
        "Fab_WarnOrRoot", "Fab_SwainSewer", "Fab_2ndConnectStart", "SecondDream", "ThirdNightmare", "FourthNightmare",
        "NoSuchTopic"
    };

    [MenuItem("SCoW/Dialog/Dump Pilot Golden (Play Mode)")]
    private static void DumpGolden() { Dump(GoldenPath); }

    [MenuItem("SCoW/Dialog/Dump Pilot Current (Play Mode)")]
    private static void DumpCurrent() { Dump(CurrentPath); }

    private sealed class Preset
    {
        public string Name;
        public string Spec;
    }

    private static List<Preset> BarPresets()
    {
        List<Preset> list = new List<Preset>();
        string[] fishy = { "", "flag:intelFishySmell1=1", "flag:intelFishySmell4=1" };
        string[] fishyName = { "n", "f1", "f4" };
        for (int mf = 0; mf < 2; mf++)
        for (int wf = 0; wf < 2; wf++)
        for (int q = 0; q < 2; q++)
        for (int t = 0; t < 2; t++)
        for (int f = 0; f < 3; f++)
        for (int b = 0; b < 2; b++)
        {
            string spec = "flag:maeveFirstDialog=" + mf + ";flag:workerFirstDialog=" + wf + ";flag:intelQuestHouse1=" + q
                + ";counter:timeCount=" + (t == 1 ? 12 : 1) + ";flag:isBar=" + b + (fishy[f].Length > 0 ? ";" + fishy[f] : "");
            list.Add(new Preset { Name = "bar_m" + mf + "w" + wf + "q" + q + "t" + (t == 1 ? 12 : 1) + fishyName[f] + "b" + b, Spec = spec });
        }
        return list;
    }

    private static List<Preset> DetectivePresets()
    {
        List<Preset> list = new List<Preset>();
        list.Add(new Preset { Name = "det_default", Spec = "" });
        list.Add(new Preset { Name = "det_timeOn5", Spec = "counter:fstDetectivTimeOn=5" });
        list.Add(new Preset { Name = "det_day3", Spec = "counter:timeCount=12;flag:intelQuestHouse1=1" });
        return list;
    }

    private static void Dump(string path)
    {
        if (!EditorApplication.isPlaying || InteractionController.Instance == null)
        {
            Debug.LogError("DialogEquivalenceTool: Main 씬 플레이 모드에서만 실행한다.");
            return;
        }
        GameState saved = GameRoot.Instance.State.Clone();
        long savedPlay = GameRoot.Instance.PlayTimeSec;
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("# DialogPilot golden v1\n");
            List<Preset> bar = BarPresets();
            List<Preset> det = DetectivePresets();
            for (int i = 0; i < bar.Count; i++) { sb.Append("@preset ").Append(bar[i].Name).Append('|').Append(bar[i].Spec).Append('\n'); }
            for (int i = 0; i < det.Count; i++) { sb.Append("@preset ").Append(det[i].Name).Append('|').Append(det[i].Spec).Append('\n'); }
            DumpScript(sb, "1st_Bar", BarTopics, bar, InteractionController.Instance.Start1stBar);
            DumpScript(sb, "1st_DetectiveOffice", DetectiveTopics, det, InteractionController.Instance.Start1stDetectiveOffice);
            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir)) { Directory.CreateDirectory(dir); }
            File.WriteAllText(path, sb.ToString(), new UTF8Encoding(false));
            Debug.Log("DialogEquivalenceTool: 덤프 완료 " + path);
        }
        finally
        {
            DialogManager.ShowDialogProbe = null;
            GameRoot.Instance.LoadState(saved, savedPlay);
        }
    }

    private static void DumpScript(StringBuilder sb, string script, string[] topics, List<Preset> presets, Action<string> run)
    {
        // 옛 경로는 DialogDatabaseManager 가 현재 장소 CSV 를 읽어 둔 상태를 전제한다(장소 도착 때 하던 일을 흉내).
        DialogDatabaseManager ddm = DialogDatabaseManager.instance;
        System.Reflection.MethodInfo setDialog = typeof(DialogDatabaseManager).GetMethod("SetDialog", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (ddm == null || setDialog == null) { throw new InvalidOperationException("DialogDatabaseManager 를 찾지 못했다"); }
        ddm.check = false;
        ddm.csvFileName = script;
        setDialog.Invoke(ddm, null);
        for (int p = 0; p < presets.Count; p++)
        {
            for (int t = 0; t < topics.Length; t++)
            {
                GameState gs = StatePreset.Build(presets[p].Spec);
                GameRoot.Instance.LoadState(gs, 0);
                GameState before = gs.Clone();
                Dialog[] captured = null;
                DialogManager.ShowDialogProbe = d => captured = d;
                run(topics[t]);
                DialogManager.ShowDialogProbe = null;
                sb.Append(script).Append('|').Append(topics[t]).Append('|').Append(presets[p].Name).Append('|');
                AppendResult(sb, captured, StatePreset.Diff(before, GameRoot.Instance.State));
            }
        }
    }

    private static void AppendResult(StringBuilder sb, Dialog[] d, string post)
    {
        if (d == null || d.Length == 0)
        {
            sb.Append("n=0|first=-|last=-|ids=-|post=").Append(post).Append('\n');
            return;
        }
        List<string> ids = new List<string>(d.Length);
        for (int i = 0; i < d.Length; i++) { ids.Add(d[i].lineId); }
        sb.Append("n=").Append(d.Length).Append("|first=").Append(d[0].scriptId).Append(':').Append(d[0].lineId)
          .Append("|last=").Append(d[d.Length - 1].scriptId).Append(':').Append(d[d.Length - 1].lineId)
          .Append("|ids=").Append(StatePreset.CompactIds(ids)).Append("|post=").Append(post).Append('\n');
    }
}
