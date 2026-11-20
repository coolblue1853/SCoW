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
        private static readonly Dictionary<string, IList<StoryLine>> CsvCache = new Dictionary<string, IList<StoryLine>>();

        private static IList<StoryLine> Csv(string name)
        {
            IList<StoryLine> lines;
            if (CsvCache.TryGetValue(name, out lines)) { return lines; }
            TextAsset ta = Resources.Load<TextAsset>(name);
            if (ta == null) { return null; }
            lines = StoryCsvParser.Parse(ta.text).Lines;
            CsvCache[name] = lines;
            return lines;
        }

        private static StoryScript Script(string id)
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

        private static void CheckGolden(string goldenPath, int minRows)
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
            Assert.IsNull(cat.ScriptFor("InSewer", s));
            Assert.IsNull(cat.ScriptFor(null, s));
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
