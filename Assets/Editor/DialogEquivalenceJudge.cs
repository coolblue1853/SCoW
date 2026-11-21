using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SCoW.Core.State;
using SCoW.Core.Story;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 동치 도구 P12 부분. (1) 하수도 안·하수도 사무실 장소 대사(InSewerDialog·StartSewerOffice, Target 방식 그대로)
/// (2) 판정 결과 대사(InteractionController.RetrunDialogResult): 판정 이름(Sub_Dialog) x SAN 키 x 결과 문자열 x 상태 프리셋.
/// 판정 줄 형식: `J|sub|san|result|preset|calls=..|..|post=..|ext=..`. 한 번 호출이 대사를 여러 번 띄울 수 있어(옛 if 블록이 겹침)
/// 호출별로 " ;; " 로 잇는다. ext = 상태 밖 씬 부수효과(DataBaseManager.condition·storyDirecting).
/// 옛 경로는 DialogDatabaseManager 가 읽어 둔 현재 CSV 를 쓰므로 판정마다 그 CSV(JudgeCsv)를 먼저 로드한다.
/// 에뮬레이션 2건: "Aiden : TakeAway" 실패(BillowUIManager.HpDown 이 편집기 시험에서 부작용이 커서 씬 필드 줄만 읽음),
/// "Fabian : Swimming" 실패(대사 없음, HP·재판정만).
/// </summary>
public static partial class DialogEquivalenceTool
{
    public const string GoldenPathP12 = "Assets/Tests/EditMode/Golden/DialogP12.golden.txt";
    public const string CurrentPathP12 = "Logs/playtest/DialogP12.current.txt";
    public const string GoldenPathP12J = "Assets/Tests/EditMode/Golden/DialogP12Judge.golden.txt";
    public const string CurrentPathP12J = "Logs/playtest/DialogP12Judge.current.txt";

    [MenuItem("SCoW/Dialog/Dump P12 Golden (Play Mode)")]
    private static void DumpGoldenP12() { Dump(GoldenPathP12, 4); DumpJudge(GoldenPathP12J); }

    [MenuItem("SCoW/Dialog/Dump P12 Current (Play Mode)")]
    private static void DumpCurrentP12() { Dump(CurrentPathP12, 4); DumpJudge(CurrentPathP12J); }

    private static readonly string[] InSewerPlain =
    {
        "Sewer_LadderUP", "Sewer_EnemySpawn", "Sewer_StealthSucc", "Sewer_StealthFail", "Sewer_DisgauseFail", "Sewer_MapDialog",
        "Sewer_MapLook", "Sewer_JournalDialog", "Sewer_JornalLookAfterSanCheck", "Sewer_BookDialog", "Sewer_JornalLookSanCheck",
        "Sewer_SymbolDialog", "Sewer_FirstIn", "Sewer_FirstDeepOne", "Sewer_LadderDialog", "Ending_BreakSymbol",
        "Ending_IfNoPromiseAlbertDeath", "Ending_SwainDeathCheck", "Ending_Killing", "Ending_DoorEnd", "NoSuchTopic"
    };

    private static List<Target> P12Targets()
    {
        InteractionController ic = InteractionController.Instance;
        List<Target> list = new List<Target>();
        list.Add(T("InSewer", InSewerPlain, One("is_default", "counter:timeCount=1"), ic.InSewerDialog, "InSewer"));
        list.Add(T("InSewer", new[] { "Sewer_JornalLook" }, Combos("ij", new[] { "firstLookJournal" }, new[] { 1 }), ic.InSewerDialog, "InSewer"));
        list.Add(T("InSewer", new[] { "Sewer_BookLook" }, Combos("ib", new[] { "bookLookFirst" }, new[] { 1 }), ic.InSewerDialog, "InSewer"));
        list.Add(T("InSewer", new[] { "Sewer_SymbolLook" }, Combos("iy", new[] { "journalFirstSanCheck", "symbolOccultFirst" }, new[] { 1 }), ic.InSewerDialog, "InSewer"));
        list.Add(T("InSewer", new[] { "Ending_isPromise" }, Combos("ip", new[] { "intelMeiv2", "albertDeathKeyword" }, new[] { 1 }), ic.InSewerDialog, "InSewer"));
        list.Add(T("InSewer", new[] { "Ending_RequestCheck" }, Combos("ir", new[] { "ifGotRequset" }, new[] { 1 }), ic.InSewerDialog, "InSewer"));
        int[] times = { 1, 2, 3, 4 };
        list.Add(T("SewerOffice", new[] { "SewerDoordialog", "SewerDoorLook", "BlackWell_look", "NoSuchTopic" }, One("so_default", "counter:timeCount=1"), ic.StartSewerOffice, "SewerOffice"));
        list.Add(T("SewerOffice", new[] { "Window_dialog", "Window_look", "Map_dialog", "Map_look", "Locker_dialog" }, Combos("sw", new string[0], times), ic.StartSewerOffice, "SewerOffice"));
        list.Add(T("SewerOffice", new[] { "Locker_look" }, Combos("sk", new[] { "blackRechSuccCanTalck" }, times), ic.StartSewerOffice, "SewerOffice"));
        list.Add(T("SewerOffice", new[] { "BlackWell_dialog" }, Combos("sb", new[] { "blackFirstDialog" }, new[] { 1 }), ic.StartSewerOffice, "SewerOffice"));
        return list;
    }

    private const string Det = "1st_DetectiveOffice";
    private const string Ch = "1st_Client'sHouse";
    private const string Noon = "2nd_NoonEvent";

    private sealed class JudgeCase
    {
        public string Sub;
        public string San;
        public string Csv;
        public string Place;
        public List<string> Specs = new List<string>();
    }

    private static readonly string[] Results =
    {
        "Result : Success", "Result : Critical Success", "Result : Failure", "Result : Fumble", "Result : --"
    };

    private static JudgeCase JC(string sub, string san, string csv, string place, params string[] extraSpecs)
    {
        JudgeCase c = new JudgeCase { Sub = sub, San = san, Csv = csv, Place = place };
        string baseSpec = "text:nowPlace=" + place + ";counter:timeCount=1";
        if (extraSpecs.Length == 0) { c.Specs.Add(baseSpec); }
        for (int i = 0; i < extraSpecs.Length; i++) { c.Specs.Add(baseSpec + (extraSpecs[i].Length > 0 ? ";" + extraSpecs[i] : "")); }
        return c;
    }

    private static List<JudgeCase> JudgeCases()
    {
        List<JudgeCase> l = new List<JudgeCase>();
        string[] det = { "Newspaper : Read", "Stove : Making coffee", "Case2IntCheck" };
        for (int i = 0; i < det.Length; i++) { l.Add(JC(det[i], "", Det, "DetectiveOffice")); }
        l.Add(JC("Aiden : Diagnosis", "", Ch, "Client'shouse"));
        l.Add(JC("Aiden : Look", "", Ch, "Client'shouse"));
        l.Add(JC("Aiden : Calming down", "", Ch, "Client'shouse", "flag:getSafe=0", "flag:getSafe=1"));
        l.Add(JC("Aiden : Persuasion", "", Ch, "Client'shouse"));
        l.Add(JC("Aiden : TakeAway", "", Ch, "Client'shouse"));
        l.Add(JC("Swain : Look", "", "1st_DailyNews", "DailyNews"));
        List<string> wit = new List<string>();
        for (int m = 0; m < 8; m++) { wit.Add("flag:intelQuestHouse1=" + (m & 1) + ";flag:intelMissingPeople1=" + ((m >> 1) & 1) + ";flag:intelMissingPeople2=" + ((m >> 2) & 1)); }
        l.Add(JC("Witness : Eavesdropping", "", Noon, "NoonEvent", wit.ToArray()));
        string[] noon = { "Police : Persuasion", "DeadBody : Diagnosis", "DeadBody : Look", "DeadBody : Check", "DeadBody : Stealing", "Accident Site : Check" };
        for (int i = 0; i < noon.Length; i++) { l.Add(JC(noon[i], "", Noon, "NoonEvent")); }
        l.Add(JC("Maeve : Persuasion", "", "1st_Bar", "Bar"));
        string[] so = { "Door : Picking doors", "Window : Break", "Locker : Take a Look", "Warden : Steal", "Warden : Persuade" };
        for (int i = 0; i < so.Length; i++) { l.Add(JC(so[i], "", "SewerOffice", "SewerOffice")); }
        string[] rv = { "Sewer : Look", "Sewer : Picking" };
        for (int i = 0; i < rv.Length; i++) { l.Add(JC(rv[i], "", "1st_Riverside", "Riverside")); }
        l.Add(JC("Sewer : Look", "", "1st_DetectiveOffice", "DetectiveOffice"));
        l.Add(JC("Safe : Open", "", "1st_Item", "DetectiveOffice"));
        l.Add(JC("Coat : Disguise", "", "1st_Item", "DetectiveOffice"));
        string[] ins = { "Fabian : Sneak Out", "??? : Give Excuse", "Book : Decryption", "Symbol : Decryption", "Symbol : Break", "Fabian : Swimming" };
        for (int i = 0; i < ins.Length; i++) { l.Add(JC(ins[i], "", "InSewer", "InSewer")); }
        // SAN : Check 는 장소별로 다른 대사(Riverside·BattleRoad·DetectiveOffice 5시)
        l.Add(JC("SAN : Check", "", "1st_Riverside", "Riverside"));
        l.Add(JC("SAN : Check", "", "1st_Battle", "BattleRoad"));
        l.Add(JC("SAN : Check", "", Det, "DetectiveOffice", "counter:timeCount=5", "counter:timeCount=1"));
        l.Add(JC("SAN : Check", "", "InSewer", "InSewer"));
        // SAN 키: 옛 Rollet 은 sub 로 "SAN : Check" 를 같이 넘긴다.
        l.Add(JC("SAN : Check", "NoonEvent_FirstDoor", Noon, "NoonEvent"));
        l.Add(JC("SAN : Check", "KaneSan", "1st_Riverside", "Riverside"));
        l.Add(JC("SAN : Check", "DrinkSan", "1st_Bar", "Bar"));
        string[] nm = { "flag:intelNightmare3=0;flag:intelNightmare4=0", "flag:intelNightmare3=1;flag:intelNightmare4=0", "flag:intelNightmare3=0;flag:intelNightmare4=1", "flag:intelNightmare3=1;flag:intelNightmare4=1" };
        l.Add(JC("SAN : Check", "ThirdDayStartSan", Det, "DetectiveOffice", nm));
        l.Add(JC("SAN : Check", "FourthDayStartSan", Det, "DetectiveOffice", nm));
        l.Add(JC("SAN : Check", "DayTwoCase2San", Det, "DetectiveOffice", "", "counter:timeCount=5"));
        l.Add(JC("SAN : Check", "Fab_4th_QuestO_SanCheck", Det, "DetectiveOffice"));
        l.Add(JC("SAN : Check", "Day_4_case1Health", Det, "DetectiveOffice"));
        string[] insKeys = { "JournalSan", "SymbolStrSan", "Sewer_FirstInSan", "BookSan", "DeepOneFirstSan" };
        for (int i = 0; i < insKeys.Length; i++) { l.Add(JC("SAN : Check", insKeys[i], "InSewer", "InSewer")); }
        List<string> ladder = new List<string>();
        for (int k = 0; k <= 10; k++) { ladder.Add("counter:endingLadderSanInt=" + k); }
        l.Add(JC("SAN : Check", "EndingLedderSan", "InSewer", "InSewer", ladder.ToArray()));
        // 판정 이름이 모르는 값이어도 아무것도 안 해야 한다
        l.Add(JC("Unknown : Check", "", "InSewer", "InSewer"));
        return l;
    }

    private static void DumpJudge(string path)
    {
        InteractionController ic = InteractionController.Instance;
        DialogDatabaseManager ddm = DialogDatabaseManager.instance;
        System.Reflection.MethodInfo setDialog = typeof(DialogDatabaseManager).GetMethod("SetDialog", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (ic == null || ddm == null || setDialog == null) { throw new InvalidOperationException("InteractionController/DialogDatabaseManager 없음"); }
        GameState saved = GameRoot.Instance.State.Clone();
        long savedPlay = GameRoot.Instance.PlayTimeSec;
        string savedCondition = DataBaseManager.condition;
        bool savedDirecting = DataBaseManager.storyDirecting;
        try
        {
            List<JudgeCase> cases = JudgeCases();
            StringBuilder rows = new StringBuilder();
            Dictionary<string, string> presetNames = new Dictionary<string, string>();
            string curCsv = null;
            for (int c = 0; c < cases.Count; c++)
            {
                JudgeCase jc = cases[c];
                if (jc.Csv != curCsv)
                {
                    ddm.check = false;
                    ddm.csvFileName = jc.Csv;
                    setDialog.Invoke(ddm, null);
                    curCsv = jc.Csv;
                }
                for (int s = 0; s < jc.Specs.Count; s++)
                {
                    string spec = jc.Specs[s];
                    string pname;
                    if (!presetNames.TryGetValue(spec, out pname)) { pname = "jd" + presetNames.Count; presetNames[spec] = pname; }
                    for (int r = 0; r < Results.Length; r++)
                    {
                        rows.Append("J|").Append(jc.Sub).Append('|').Append(jc.San).Append('|').Append(Results[r]).Append('|').Append(pname).Append('|');
                        rows.Append(RunJudgeOld(ic, jc, spec, Results[r])).Append('\n');
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("# DialogP12 judge golden v1\n");
            foreach (KeyValuePair<string, string> kv in presetNames) { sb.Append("@preset ").Append(kv.Value).Append('|').Append(kv.Key).Append('\n'); }
            sb.Append(rows);
            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir)) { Directory.CreateDirectory(dir); }
            File.WriteAllText(path, sb.ToString(), new UTF8Encoding(false));
            Debug.Log("DialogEquivalenceTool: 판정 덤프 완료 " + path);
        }
        finally
        {
            DialogManager.ShowDialogProbe = null;
            DataBaseManager.condition = savedCondition;
            DataBaseManager.storyDirecting = savedDirecting;
            GameRoot.Instance.LoadState(saved, savedPlay);
        }
    }

    private static string RunJudgeOld(InteractionController ic, JudgeCase jc, string spec, string result)
    {
        GameState gs = StatePreset.Build(spec);
        GameRoot.Instance.LoadState(gs, 0);
        GameState before = gs.Clone();
        DataBaseManager.condition = "Nomal";
        DataBaseManager.storyDirecting = false;
        List<IList<string>> calls = new List<IList<string>>();
        DialogManager.ShowDialogProbe = d =>
        {
            List<string> refs = new List<string>();
            for (int i = 0; i < d.Length; i++) { refs.Add(d[i].scriptId + ":" + d[i].lineId); }
            calls.Add(refs);
        };
        string exception = null;
        bool fail = result == "Result : Failure" || result == "Result : Fumble";
        try
        {
            if (jc.Sub == "Aiden : TakeAway" && fail)
            {
                // 에뮬레이션: 옛 코드는 BillowUIManager.HpDown(5) 뒤에 이 필드의 줄을 띄운다.
                ic.theDM.ShowDialog(ic.aidenKeySafeStrFail.transform.GetComponent<interactionEvent>().GetDialogs());
            }
            else if (jc.Sub == "Fabian : Swimming" && fail)
            {
                // 에뮬레이션: 옛 코드는 HP 5 감소 + 0.5초 뒤 재판정만(대사 없음).
            }
            else
            {
                ic.RetrunDialogResult(jc.Sub, result, jc.San);
            }
        }
        catch (Exception e)
        {
            exception = e.GetType().Name;
        }
        DialogManager.ShowDialogProbe = null;
        string ext = "";
        if (DataBaseManager.condition == "Good") { ext += "cond:Good"; }
        if (DataBaseManager.storyDirecting) { ext += (ext.Length > 0 ? "," : "") + "sd"; }
        if (ext.Length == 0) { ext = "-"; }
        return StatePreset.DescribeCalls(calls, StatePreset.Diff(before, GameRoot.Instance.State)) + "|ext=" + ext + (exception != null ? "|EX=" + exception : "");
    }
}
