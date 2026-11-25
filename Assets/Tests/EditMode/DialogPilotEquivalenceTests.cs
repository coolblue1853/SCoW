using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using SCoW.Core.State;
using SCoW.Core.Story;
using UnityEngine;

namespace SCoW.Tests
{
    /// <summary>
    /// C-2 P9 동치 테스트. 옛 경로(InteractionController + 씬 interactionEvent 범위)가 프리셋마다 낸 줄·상태 변화를 덤프한 골든
    /// (Golden/DialogPilot.golden.txt, 도구: SCoW/Dialog/Dump Pilot Golden)을 새 경로(Resources/Story JSON + StoryResolver)와 대조한다.
    /// </summary>
    public sealed class DialogPilotEquivalenceTests
    {
        private const string GoldenPath = "Assets/Tests/EditMode/Golden/DialogPilot.golden.txt";
        private const string GoldenPathP10a = "Assets/Tests/EditMode/Golden/DialogP10a.golden.txt";
        private const string GoldenPathP10b = "Assets/Tests/EditMode/Golden/DialogP10b.golden.txt";
        private const string GoldenPathP11 = "Assets/Tests/EditMode/Golden/DialogP11.golden.txt";
        private static readonly Dictionary<string, IList<StoryLine>> CsvCache = new Dictionary<string, IList<StoryLine>>();

        internal static IList<StoryLine> Csv(string name)
        {
            IList<StoryLine> lines;
            if (CsvCache.TryGetValue(name, out lines)) { return lines; }
            TextAsset ta = Resources.Load<TextAsset>(name);
            if (ta == null) { return null; }
            lines = StoryCsvParser.Parse(ta.text).Lines;
            CsvCache[name] = lines;
            return lines;
        }

        internal static StoryScript Script(string id)
        {
            TextAsset ta = Resources.Load<TextAsset>("Story/" + id);
            Assert.IsNotNull(ta, "Story/" + id + ".json 없음");
            StoryLoadResult r = StoryScriptLoader.Load(ta.text);
            Assert.IsTrue(r.Ok, id + ": " + string.Join(" | ", r.Errors));
            return r.Script;
        }

        private static string Describe(StoryResolution res, GameState before, GameState after)
        {
            string post = StatePreset.Diff(before, after);
            if (res == null || res.Lines.Count == 0) { return "n=0|first=-|last=-|ids=-|post=" + post; }
            List<string> ids = new List<string>();
            for (int i = 0; i < res.Lines.Count; i++) { ids.Add(res.Lines[i].Id); }
            return "n=" + res.Lines.Count + "|first=" + res.Lines[0] + "|last=" + res.Lines[res.Lines.Count - 1]
                + "|ids=" + StatePreset.CompactIds(ids) + "|post=" + post;
        }

        [Test]
        public void GoldenRows_MatchNewPath()
        {
            CheckGolden(GoldenPath, 800);
        }

        /// <summary>P10a 5장소(ClientsHouse·DailyNews·University·Riverside·Hospital). 옛 경로에서 예외(씬 필드 미할당)로 끝난 행은 "아무것도 안 띄움"으로 본다.</summary>
        [Test]
        public void GoldenRows_P10a_MatchNewPath()
        {
            CheckGolden(GoldenPathP10a, 1000);
        }

        /// <summary>P10b 5장소(Policeoffice·Slum·2nd_NoonEvent·2ndDetectiveOffice·SafeLook).</summary>
        [Test]
        public void GoldenRows_P10b_MatchNewPath()
        {
            CheckGolden(GoldenPathP10b, 200);
        }

        /// <summary>
        /// P11 키워드 대사(Keyword.json, 토픽 "NPC/키워드")와 전투 대사(1st_Battle.json). 키워드 x 플래그·시각 조합 전부.
        /// 의도된 차이: 없음. 옛 코드의 죽은 가지 하나는 그대로 보존했다(Maeve "Missing People &amp; Fishy Smell" 안의 중첩 키워드 비교는 항상 거짓이라 CauseX 만 나온다).
        /// </summary>
        [Test]
        public void GoldenRows_P11_Keyword_MatchNewPath()
        {
            CheckGolden(GoldenPathP11, 1500);
        }

        /// <summary>모든 NPC 는 "NPC/*" 기본 토픽을 가진다(목록 밖 키워드의 마지막 else).</summary>
        [Test]
        public void Keyword_EveryNpcHasDefaultTopic()
        {
            StoryScript sc = Script("Keyword");
            HashSet<string> npcs = new HashSet<string>();
            HashSet<string> defaults = new HashSet<string>();
            for (int i = 0; i < sc.Topics.Count; i++)
            {
                string id = sc.Topics[i].Id;
                int slash = id.IndexOf('/');
                Assert.Greater(slash, 0, "NPC/키워드 형식 아님: " + id);
                npcs.Add(id.Substring(0, slash));
                if (id.EndsWith("/*", System.StringComparison.Ordinal)) { defaults.Add(id.Substring(0, slash)); }
            }
            Assert.AreEqual(12, npcs.Count);
            Assert.AreEqual(npcs.Count, defaults.Count, "기본 토픽(NPC/*) 없는 NPC 가 있다");
        }

        // PT-3 신규 노드: 옛 코드에 없어 골든 행이 없다. PartyStoryTests 가 따로 덮는다.
        private static readonly HashSet<string> PartyOnlyNodes = new HashSet<string>
        {
            "Keyword/albertKeyJoin", "Keyword/maeveKeyJoin",
            "InSewer/Ending_PartyAlbert", "InSewer/Ending_PartyMaeve", "InSewer/Ending_PartyMaeveDead",
        };

        // SEWER S2 신규 노드(조사 포인트): 옛 코드에 없어 골든 행이 없다. SewerPointTests 가 배치·플래그를 따로 덮는다.
        internal static readonly HashSet<string> SewerS2Nodes = new HashSet<string>
        {
            "InSewer/Sewer_Z2a_Log",
            "Sewer : Graffiti@0", "Sewer : Graffiti@0.ok", "Sewer : Graffiti@0.fail",
            "Sewer : Workwear@0", "Sewer : Workwear@0.ok", "Sewer : Workwear@0.fail",
            "Sewer : Footprints@0", "Sewer : Footprints@0.ok", "Sewer : Footprints@0.fail",
            "Sewer : Signboard@0", "Sewer : Signboard@0.ok", "Sewer : Signboard@0.fail",
        };

        internal static void CheckGolden(string goldenPath, int minRows)
        {
            string[] rows = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Application.dataPath), goldenPath));
            Dictionary<string, string> presets = new Dictionary<string, string>();
            Dictionary<string, StoryScript> scripts = new Dictionary<string, StoryScript>();
            HashSet<string> reached = new HashSet<string>();
            List<string> mismatches = new List<string>();
            int compared = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                string row = rows[i];
                if (row.StartsWith("#") || row.Length == 0) { continue; }
                if (row.StartsWith("@preset "))
                {
                    int bar = row.IndexOf('|');
                    presets[row.Substring(8, bar - 8)] = row.Substring(bar + 1);
                    continue;
                }
                string[] f = row.Split(new[] { '|' }, 4);
                string scriptId = f[0];
                string topic = f[1];
                string presetName = f[2];
                string expected = f[3];
                int ex = expected.IndexOf("|EX=", System.StringComparison.Ordinal);
                if (ex >= 0) { expected = expected.Substring(0, ex); }
                if (!scripts.ContainsKey(scriptId)) { scripts[scriptId] = Script(scriptId); }
                GameState gs = StatePreset.Build(presets[presetName]);
                GameState before = gs.Clone();
                GameStateAccess acc = new GameStateAccess(gs);
                List<string> errors = new List<string>();
                StoryResolution res = StoryResolver.Resolve(scripts[scriptId], topic, acc, acc, Csv, errors);
                Assert.AreEqual(0, errors.Count, row + " -> " + string.Join(" | ", errors));
                if (res != null) { reached.Add(scriptId + "/" + res.NodeId); }
                string actual = Describe(res, before, gs);
                compared++;
                if (actual != expected && mismatches.Count < 15)
                {
                    mismatches.Add(scriptId + "|" + topic + "|" + presetName + "\n  기대 " + expected + "\n  실제 " + actual);
                }
            }
            Assert.IsTrue(compared > minRows, "골든 행이 너무 적다: " + compared);
            Assert.AreEqual(0, mismatches.Count, "동치 불일치:\n" + string.Join("\n", mismatches.ToArray()));
            // 프리셋이 모든 노드를 덮는지(도달 못 하는 노드가 있으면 그 분기는 검증되지 않은 것).
            foreach (KeyValuePair<string, StoryScript> kv in scripts)
            {
                foreach (string nodeId in kv.Value.Nodes.Keys)
                {
                    if (PartyOnlyNodes.Contains(kv.Key + "/" + nodeId) || SewerS2Nodes.Contains(kv.Key + "/" + nodeId)) { continue; }
                    Assert.IsTrue(reached.Contains(kv.Key + "/" + nodeId), "프리셋이 덮지 못한 노드: " + kv.Key + "/" + nodeId);
                }
            }
        }

        [Test]
        public void PlaceCatalog_MapsPilotPlaces()
        {
            TextAsset ta = Resources.Load<TextAsset>("Story/places");
            Assert.IsNotNull(ta);
            PlaceCatalog cat = PlaceCatalog.Load(ta.text);
            Assert.IsTrue(cat.Ok, string.Join(" | ", cat.Errors));
            GameStateAccess s = new GameStateAccess(GameState.New());
            Assert.AreEqual("1st_Bar", cat.ScriptFor("Bar", s));
            Assert.AreEqual("1st_DetectiveOffice", cat.ScriptFor("DetectiveOffice", s));
            Assert.AreEqual("1st_Hospital", cat.ScriptFor("Hospital", s));
            Assert.AreEqual("1st_DailyNews", cat.ScriptFor("DailyNews", s));
            Assert.AreEqual("1st_University", cat.ScriptFor("University", s));
            Assert.AreEqual("1st_Riverside", cat.ScriptFor("Riverside", s));
            Assert.AreEqual("1st_Client'sHouse", cat.ScriptFor("Client'shouse", s));
            GameState late = GameState.New();
            late.SetCounter("timeCount", 6);
            Assert.AreEqual("2nd_NoonEvent", cat.ScriptFor("Client'shouse", new GameStateAccess(late)));
            Assert.AreEqual("1st_Slum", cat.ScriptFor("Slum", s));
            Assert.AreEqual("1st_PoliceOffice", cat.ScriptFor("Policeoffice", s));
            Assert.AreEqual("2nd_NoonEvent", cat.ScriptFor("NoonEvent", s));
            Assert.AreEqual("InSewer", cat.ScriptFor("InSewer", s));
            Assert.AreEqual("SewerOffice", cat.ScriptFor("SewerOffice", s));
            Assert.IsNull(cat.ScriptFor("Home", s));
            Assert.IsNull(cat.ScriptFor(null, s));
        }

        /// <summary>
        /// 의도된 차이(옛 버그 수정, C-2 P10 리뷰): Client'shouse 를 timeCount==6 에서 열면 옛 경로는 Intel_ObtoUI(time&lt;7)가 Start1stClientsHouse 를 불렀지만
        /// DialogDatabaseManager 는 이미 2nd_NoonEvent CSV 를 로드해 1st 줄번호로 읽어 틀린 텍스트·KeyNotFound 였다.
        /// 새 경로는 1st_Client'sHouse JSON 이 1st CSV 를 직접 지정하므로 time 과 무관하게 time1 과 같은 줄을 낸다.
        /// 골든의 ch_t6_* 행 전부가 이 "옛 경로와 다른 줄" 목록이며(옛 골든 없음), 새 동작은 ch_t1_* 와 동일해야 한다.
        /// 같은 경로: selectionUIManager.cs(Aiden_key_Safe_Str·Nothing), PotalPoint.cs(Aiden_key_FailForce·into_FirstRoom).
        /// </summary>
        [Test]
        public void ClientsHouse_Time6_IntentionalFix_SameAsTime1()
        {
            string[] rows = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Application.dataPath), GoldenPathP10a));
            Dictionary<string, string> t1 = new Dictionary<string, string>();
            List<string[]> t6 = new List<string[]>();
            for (int i = 0; i < rows.Length; i++)
            {
                if (!rows[i].StartsWith("1st_Client'sHouse|")) { continue; }
                string[] f = rows[i].Split(new[] { '|' }, 4);
                int ex = f[3].IndexOf("|EX=", System.StringComparison.Ordinal);
                string result = ex >= 0 ? f[3].Substring(0, ex) : f[3];
                if (f[2].StartsWith("ch_t1_")) { t1[f[1] + "|" + f[2].Substring(6)] = result; }
                else if (f[2].StartsWith("ch_t6_")) { t6.Add(new[] { f[1], f[2].Substring(6), result }); }
            }
            Assert.IsTrue(t6.Count > 100, "ch_t6 행이 너무 적다: " + t6.Count);
            for (int i = 0; i < t6.Count; i++)
            {
                string key = t6[i][0] + "|" + t6[i][1];
                Assert.IsTrue(t1.ContainsKey(key), "t1 짝 없음: " + key);
                Assert.AreEqual(t1[key], t6[i][2], "time6 가 time1 과 다르다: " + key);
            }
        }

        /// <summary>Resources/Story/*.json(places.json 제외) 전부가 오류 0·Script 非 null 로 로드돼야 한다(오타가 조용히 실패하지 않게).</summary>
        [Test]
        public void AllStoryJson_LoadWithoutErrors()
        {
            TextAsset[] all = Resources.LoadAll<TextAsset>("Story");
            int scripts = 0;
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].name == "places") { continue; }
                StoryLoadResult r = StoryScriptLoader.Load(all[i].text);
                Assert.AreEqual(0, r.Errors.Count, all[i].name + ": " + string.Join(" | ", r.Errors));
                Assert.IsNotNull(r.Script, all[i].name + ": Script null");
                scripts++;
            }
            Assert.IsTrue(scripts >= 12, "스토리 JSON 이 너무 적다: " + scripts);
        }

        /// <summary>places.json 오류 0, 그리고 모든 PlaceDef.ScriptId 가 같은 이름의 Story JSON 과 CSV 로 풀린다(규약: script id = CSV 이름).</summary>
        [Test]
        public void PlaceCatalog_AllScriptIds_HaveJsonAndCsv()
        {
            TextAsset ta = Resources.Load<TextAsset>("Story/places");
            Assert.IsNotNull(ta);
            PlaceCatalog cat = PlaceCatalog.Load(ta.text);
            Assert.AreEqual(0, cat.Errors.Count, string.Join(" | ", cat.Errors));
            foreach (KeyValuePair<string, PlaceDef> kv in cat.Places)
            {
                for (int i = 0; i < kv.Value.Scripts.Count; i++)
                {
                    string id = kv.Value.Scripts[i].ScriptId;
                    Assert.IsNotNull(Resources.Load<TextAsset>("Story/" + id), kv.Key + ": Story/" + id + ".json 없음");
                    Assert.IsNotNull(Csv(id), kv.Key + ": CSV " + id + " 없음");
                }
            }
        }

        [Test]
        public void PlaceCatalog_BadWhen_ReportsError()
        {
            PlaceCatalog cat = PlaceCatalog.Load("{\"places\":[{\"id\":\"X\",\"scripts\":[{\"when\":\"time<<6\",\"script\":\"s\"}]}]}");
            Assert.IsFalse(cat.Ok);
        }

        [Test]
        public void TextRef_ParsesRangeAndSingle()
        {
            string err;
            StoryTextRef r = StoryTextRef.Parse("1st_Bar:21-39", out err);
            Assert.AreEqual("1st_Bar", r.File);
            Assert.AreEqual("21", r.FromId);
            Assert.AreEqual("39", r.ToId);
            StoryTextRef one = StoryTextRef.Parse("1st_Bar:41", out err);
            Assert.AreEqual("41", one.FromId);
            Assert.AreEqual("41", one.ToId);
            Assert.IsNull(StoryTextRef.Parse("1st_Bar", out err));
            Assert.IsNull(StoryTextRef.Parse(":3", out err));
        }
    }
}
