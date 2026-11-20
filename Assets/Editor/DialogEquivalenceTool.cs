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
    public const string GoldenPathP10a = "Assets/Tests/EditMode/Golden/DialogP10a.golden.txt";
    public const string CurrentPathP10a = "Logs/playtest/DialogP10a.current.txt";

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

    private static readonly string[] ClientsHouseTopics =
    {
        "FirstArrive", "NockDoor", "Ella_fistDialog", "Ella_Dialog1", "Ella_Dialog2", "Ella_FirstUpstair", "into_FirstRoom",
        "Aiden_FirstDialog", "Aiden_Active", "Aiden_Look", "Aiden_LookJudge", "Aiden_Dialog1", "Aiden_Dialog2",
        "Aiden_key_Nightmare1", "Aiden_key_EllaTriss1", "Aiden_key_Safe1", "Aiden_key_Safe_Str", "Aiden_key_Safe_Str_Sucsses",
        "Aiden_key_Safe_Str_Fail", "Aiden_key_Safe_rhetoric_sucsses", "Aiden_key_Safe_rhetoric_Fail", "Aiden_key_Safe_Nothing",
        "Aiden_key_Fail1", "Aiden_key_Fail2", "Aiden_Judge_medicine_Sucsses", "Aiden_Judge_medicine_Fail",
        "Aiden_Judge_psychotherapy_Sucsses", "Aiden_Judge_psychotherapy_Sucsses_Safe", "Aiden_Judge_psychotherapy_Sucsses_NoSafe",
        "Aiden_Judge_psychotherapy_Sucsses_End", "Aiden_Judge_psychotherapy_Fail", "Aiden_Judge_Observation_Sucsses",
        "Aiden_Judge_Observation_Fail", "Aiden_key_FailForce", "EllaEnd_NoSafe", "EllaEnd_GetSafe", "NoSuchTopic"
    };

    private static readonly string[] DailyNewsTopics =
    {
        "Swain_FirstDialog", "Swain_Look", "Swain_LookJudge_Sucsses", "Swain_LookJudge_Fail", "Swain_Key_PlanetarySequence1",
        "Swain_Key_Insomnia1", "Swain_Key_Nightmare2", "Swain_Key_Paranoia1", "Swain_Key_EllaTriss1", "Swain_Key_AidenTriss1",
        "Swain_Key_DailyNews1", "Swain_Key_Nothing", "Journalist_FirstDialog", "NoSuchTopic"
    };

    private static readonly string[] UniversityTopics =
    {
        "Univ_FirstDialog", "Univ_Look", "Univ_Key_PlanetarySequence1", "Univ_Key_Insomnia1", "Kane_FirstDialog", "Kane_Look", "NoSuchTopic"
    };

    private static readonly string[] RiversideTopics =
    {
        "Kane_FirstDialog", "Kane_Look", "Kane_Nothing", "Sewer_FirstDialog", "Sewer_Look", "Sewer_Observation_SucssesAfter",
        "Sewer_Observation_Sucsses", "Sewer_Observation_Fail", "Sewer_Observation_SucssesAfterSanSuc", "NoSuchTopic"
    };

    private static readonly string[] HospitalTopics =
    {
        "Kate_NoonLook", "Alan_NoonLook", "Hospital_NoonFirst", "Kate_FirstDialog", "Alan_FirstDialog", "NoSuchTopic"
    };

    [MenuItem("SCoW/Dialog/Dump Pilot Golden (Play Mode)")]
    private static void DumpGolden() { Dump(GoldenPath, false); }

    [MenuItem("SCoW/Dialog/Dump Pilot Current (Play Mode)")]
    private static void DumpCurrent() { Dump(CurrentPath, false); }

    [MenuItem("SCoW/Dialog/Dump P10a Golden (Play Mode)")]
    private static void DumpGoldenP10a() { Dump(GoldenPathP10a, true); }

    [MenuItem("SCoW/Dialog/Dump P10a Current (Play Mode)")]
    private static void DumpCurrentP10a() { Dump(CurrentPathP10a, true); }

    /// <summary>플래그 이름들의 모든 0/1 조합 x 시각(timeCount) 값들. 이름은 접두+시각+조합.</summary>
    private static List<Preset> Combos(string prefix, string[] flags, int[] times)
    {
        List<Preset> list = new List<Preset>();
        for (int t = 0; t < times.Length; t++)
        {
            for (int mask = 0; mask < (1 << flags.Length); mask++)
            {
                string spec = "counter:timeCount=" + times[t];
                string name = prefix + "_t" + times[t] + "_";
                for (int f = 0; f < flags.Length; f++)
                {
                    int on = (mask >> f) & 1;
                    spec += ";flag:" + flags[f] + "=" + on;
                    name += on;
                }
                list.Add(new Preset { Name = name, Spec = spec });
            }
        }
        return list;
    }

    private sealed class Target
    {
        public string Script;
        public string[] Topics;
        public List<Preset> Presets;
        public Action<string> Run;
    }

    private static List<Target> P10aTargets()
    {
        InteractionController ic = InteractionController.Instance;
        List<Target> list = new List<Target>();
        list.Add(new Target
        {
            Script = "1st_Client'sHouse", Topics = ClientsHouseTopics, Run = ic.Start1stClientsHouse,
            Presets = Combos("ch", new[] { "ellaEndDialog", "aidenFirstDialog", "getSafe" }, new[] { 1 })
        });
        list.Add(new Target
        {
            Script = "1st_DailyNews", Topics = DailyNewsTopics, Run = ic.Start1stDailyNews,
            Presets = Combos("dn", new[] { "aidenFirstDialog" }, new[] { 5, 6 })
        });
        list.Add(new Target
        {
            Script = "1st_University", Topics = UniversityTopics, Run = ic.Start1stUniversity,
            Presets = Combos("un", new[] { "univFirstDialog", "kaneFirstDialog", "intelRiverWaterQuality2" }, new[] { 1 })
        });
        list.Add(new Target
        {
            Script = "1st_Riverside", Topics = RiversideTopics, Run = ic.Start1stRiverside,
            Presets = Combos("rv", new[] { "kaneFirstDialog", "sewerFirstDialog", "sewerObservationEnd" }, new[] { 1 })
        });
        list.Add(new Target
        {
            Script = "1st_Hospital", Topics = HospitalTopics, Run = ic.Start1stHospital,
            Presets = Combos("hp", new[] { "kateNoonFirstDialog", "alanFirstDialog", "thirdDayAlanDialog", "alanNoonFirstDialog" }, new[] { 2, 6, 3, 7, 9, 12 })
        });
        return list;
    }

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

    private static void Dump(string path, bool p10a)
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
            List<Target> targets;
            if (p10a)
            {
                targets = P10aTargets();
            }
            else
            {
                targets = new List<Target>();
                targets.Add(new Target { Script = "1st_Bar", Topics = BarTopics, Presets = BarPresets(), Run = InteractionController.Instance.Start1stBar });
                targets.Add(new Target { Script = "1st_DetectiveOffice", Topics = DetectiveTopics, Presets = DetectivePresets(), Run = InteractionController.Instance.Start1stDetectiveOffice });
            }
            for (int k = 0; k < targets.Count; k++)
            {
                List<Preset> ps = targets[k].Presets;
                for (int i = 0; i < ps.Count; i++) { sb.Append("@preset ").Append(ps[i].Name).Append('|').Append(ps[i].Spec).Append('\n'); }
            }
            for (int k = 0; k < targets.Count; k++)
            {
                DumpScript(sb, targets[k].Script, targets[k].Topics, targets[k].Presets, targets[k].Run);
            }
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
                string exception = null;
                try
                {
                    run(topics[t]);
                }
                catch (Exception e)
                {
                    exception = e.GetType().Name;
                }
                DialogManager.ShowDialogProbe = null;
                sb.Append(script).Append('|').Append(topics[t]).Append('|').Append(presets[p].Name).Append('|');
                AppendResult(sb, captured, StatePreset.Diff(before, GameRoot.Instance.State) + (exception != null ? "|EX=" + exception : ""));
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
