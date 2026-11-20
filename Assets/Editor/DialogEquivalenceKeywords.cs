using System;
using System.Collections.Generic;

/// <summary>
/// 동치 도구 P11 부분: 키워드 대사(InteractionController.KeywordDialog)와 전투 대사(BattleDialog).
/// 키워드 대사는 NPC 이름(setDialog)·키워드(DataBaseManager.keywordDowner)·상태 플래그로 줄이 정해진다.
/// 토픽 이름은 "NPC/키워드"(키워드 밖 값은 "NPC/*"). 옛 경로 CSV 는 NPC 가 서 있는 장소의 CSV(호출 시점에 로드돼 있던 것).
/// 프리셋은 그 NPC 분기가 읽는 플래그의 모든 0/1 조합 x 시각 x (Aiden 의 aidenKeywordCount 0/1).
/// </summary>
public static partial class DialogEquivalenceTool
{
    public const string GoldenPathP11 = "Assets/Tests/EditMode/Golden/DialogP11.golden.txt";
    public const string CurrentPathP11 = "Logs/playtest/DialogP11.current.txt";
    private const string NoSuchKeyword = "NoSuchKeyword";

    [UnityEditor.MenuItem("SCoW/Dialog/Dump P11 Golden (Play Mode)")]
    private static void DumpGoldenP11() { Dump(GoldenPathP11, 3); }

    [UnityEditor.MenuItem("SCoW/Dialog/Dump P11 Current (Play Mode)")]
    private static void DumpCurrentP11() { Dump(CurrentPathP11, 3); }

    private sealed class KeywordSpec
    {
        public string Npc;
        public string Csv;
        public string[] Keywords;
        public string[] Flags;
        public string[] Counters;
    }

    private static KeywordSpec Spec(string npc, string csv, string[] keywords, string[] flags, string[] counters)
    {
        return new KeywordSpec { Npc = npc, Csv = csv, Keywords = keywords, Flags = flags, Counters = counters };
    }

    private static readonly KeywordSpec[] KeywordSpecs =
    {
        Spec("Ella Triss", "1st_Client'sHouse", new[] { "Subject to request","Rumor","Contents of a Newspaper","Content of a Nightmare","Aiden's Detail","Water and Nightmare","Doctor's opinion","Aiden's Safe","Acquiring a Safe","Aiden & Pandemic" }, new[] { "intelSewerGhostStory1" }, new string[0]),
        Spec("Aiden Triss", "1st_Client'sHouse", new[] { "Aiden's Nightmare","Client","Saw in the sewer","Albert's Information","Reporting by Swain","Contents of a GhostStory","Source speculation","Sewer & FishySmell","Cause of Fishy smell","Owner of Eye?","Aiden's Safe" }, new[] { "getSafe" }, new[] { "aidenKeywordCount" }),
        Spec("Berkeley Swain", "1st_DailyNews", new[] { "Contents of a Newspaper","Newspaper Article","Aiden's Nightmare","Husband's Condition","Client","Subject to request","Husband's job","Albert's Information","Source of the smell?","Contents of a GhostStory","Saw in the sewer","Content of a Nightmare","Water and Nightmare","Source speculation","Sewer & FishySmell","Cause of Fishy smell","Details","Aiden & Pandemic","Unfair dismissal","Kidnap","Fishy Slime","Manhole","Aiden's warning","Murdered Clients","Missing Subject","Witness Testimony","The Culprit","Missing People & Fishy Smell","Cause of Death","Webbed Feet","Way to Escape" }, new[] { "intelMissingPeople1","intelMissingPeople2","isSwainTalkMissingpeople","swainDeathKeyword" }, new string[0]),
        Spec("University student", "1st_University", new[] { "Contents of a Newspaper","Newspaper Article","Content of a Nightmare","Water and Nightmare","Rumor","Source speculation","Sewer & FishySmell","Cause of Fishy smell","Details" }, new string[0], new string[0]),
        Spec("Kane", "1st_Riverside", new[] { "Strange Point","Fishy Slime" }, new[] { "intelCreepyEyes1","intelFishySmell1" }, new string[0]),
        Spec("Albert Bradley", "1st_PoliceOffice", new[] { "Contents of a Newspaper","Newspaper Article","Mafia friend","Contents of a GhostStory","Rumor","Saw in the sewer","Reporting by Swain","Water and Nightmare","Source speculation","Sewer & FishySmell","Cause of Fishy smell","Details","Promise","Unfair dismissal","Root of problem","Murdered Clients","Missing Subject","Witness Testimony","The Culprit","Cause of Death","Webbed Feet","Way to Escape","Aiden's warning","Owner of Uniform","Missing People & Fishy Smell" }, new[] { "albertDeathKeyword","intelFishySmell3","intelFishySmell4","intelSewerWorker3" }, new[] { "timeCount" }),
        Spec("Kate Lizzie", "1st_Hospital", new[] { "Newspaper Article","Aiden's Nightmare","Husband's Condition","State of congestion","Rumor","Contents of a GhostStory","Saw in the sewer","Subject to request","Murdered Clients","Cause of Death","","Webbed Feet","Witness Testimony" }, new string[0], new string[0]),
        Spec("Alan Parks", "1st_Hospital", new[] { "Aiden's Nightmare","Content of a Nightmare","Aiden's Detail","Newspaper Article","Husband's Condition","Subject to request","Details","State of congestion","Murdered Clients","Cause of Death","","Webbed Feet" }, new string[0], new string[0]),
        Spec("Maeve", "1st_Bar", new[] { "Contents of a Newspaper","Astronomical show","Newspaper Article","Police friend","Reporting by Swain","Albert's Information","Saw in the sewer","Rumor","Source of the smell?","Subject to request","Water and Nightmare","Contents of a GhostStory","Truth of a rumor","Details","Root of problem","Murdered Clients","Missing Subject","Witness Testimony","Missing People & Fishy Smell","Source speculation","Sewer & FishySmell","Cause of Fishy smell" }, new string[0], new string[0]),
        Spec("Dusty Dave", "1st_Slum", new[] { "Witness Testimony","","Acquiring a Safe","Bracelet" }, new[] { "daveAfterBreclet","daveAfterSafe","daveSafeOpen" }, new string[0]),
        Spec("BlackWell", "SewerOffice", new[] { "Source speculation","Sewer & FishySmell","Cause of Fishy smell","Root of problem","Aiden's warning","Witness Testimony","Way to Escape" }, new[] { "blackRechSuccCanTalck" }, new string[0]),
        Spec("Symbol", "InSewer", new[] { "Advent" }, new string[0], new string[0]),
    };

    private static List<Preset> KeywordPresets(KeywordSpec k, int index)
    {
        List<Preset> list = new List<Preset>();
        bool usesTime = Array.IndexOf(k.Counters, "timeCount") >= 0;
        int[] times = usesTime ? new[] { 1, 8, 9 } : new[] { 1 };
        List<string> others = new List<string>();
        for (int c = 0; c < k.Counters.Length; c++)
        {
            if (k.Counters[c] != "timeCount") { others.Add(k.Counters[c]); }
        }
        int counterCombos = 1 << others.Count;
        for (int t = 0; t < times.Length; t++)
        {
            for (int cm = 0; cm < counterCombos; cm++)
            {
                for (int mask = 0; mask < (1 << k.Flags.Length); mask++)
                {
                    string spec = "counter:timeCount=" + times[t];
                    string name = "kw" + index + "_t" + times[t] + "_c" + cm + "_";
                    for (int c = 0; c < others.Count; c++) { spec += ";counter:" + others[c] + "=" + ((cm >> c) & 1); }
                    for (int f = 0; f < k.Flags.Length; f++)
                    {
                        int on = (mask >> f) & 1;
                        spec += ";flag:" + k.Flags[f] + "=" + on;
                        name += on;
                    }
                    list.Add(new Preset { Name = name, Spec = spec });
                }
            }
        }
        return list;
    }

    private static List<Target> P11Targets()
    {
        InteractionController ic = InteractionController.Instance;
        List<Target> list = new List<Target>();
        for (int i = 0; i < KeywordSpecs.Length; i++)
        {
            KeywordSpec k = KeywordSpecs[i];
            string[] topics = new string[k.Keywords.Length + 1];
            for (int j = 0; j < k.Keywords.Length; j++) { topics[j] = k.Npc + "/" + k.Keywords[j]; }
            topics[k.Keywords.Length] = k.Npc + "/*";
            string npc = k.Npc;
            Action<string> run = topic =>
            {
                string kw = topic.Substring(npc.Length + 1);
                DataBaseManager.keywordDowner = kw == "*" ? NoSuchKeyword : kw;
                ic.KeywordDialog(npc);
            };
            list.Add(T("Keyword", topics, KeywordPresets(k, i), run, k.Csv));
        }
        list.Add(T("1st_Battle", new[] { "start", "End", "NoSuchTopic" }, One("bt_default", "counter:timeCount=1"), ic.BattleDialog, null));
        return list;
    }
}
