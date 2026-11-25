using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using SCoW.Core.State;
using SCoW.Core.Story;
using UnityEngine;

namespace SCoW.Tests
{
    /// <summary>
    /// C-2 P12 동치 테스트. 옛 InteractionController(InSewerDialog·StartSewerOffice·RetrunDialogResult)가 낸 줄·상태 변화를 덤프한 골든을
    /// 새 경로(InSewer·SewerOffice·Judge JSON + StoryResolver)와 대조한다.
    /// 옛 버그 보존(의도된 차이 없음): 강가에서 KaneSan 판정은 sub 가 "SAN : Check" 라 Riverside 블록(27-28/24-25)과 KaneSan 대사(64-65)를 연달아 띄운다.
    /// 옛 코드의 "Aiden : Calming down" 실패는 의학 판정 실패 줄(100)을 재사용한다. 결과가 판정 결과가 아닌 문자열("Result : --")이면 결과와 무관한 효과만 적용된다.
    /// </summary>
    public sealed class DialogP12EquivalenceTests
    {
        private const string GoldenPathSewer = "Assets/Tests/EditMode/Golden/DialogP12.golden.txt";
        private const string GoldenPathJudge = "Assets/Tests/EditMode/Golden/DialogP12Judge.golden.txt";
        private const string JudgeScript = "Judge";

        [Test]
        public void GoldenRows_P12_SewerPlaces_MatchNewPath()
        {
            DialogPilotEquivalenceTests.CheckGolden(GoldenPathSewer, 60);
        }

        [Test]
        public void GoldenRows_P12_Judge_MatchNewPath()
        {
            string[] rows = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Application.dataPath), GoldenPathJudge));
            Dictionary<string, string> presets = new Dictionary<string, string>();
            StoryScript script = DialogPilotEquivalenceTests.Script(JudgeScript);
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
                string[] f = row.Split(new[] { '|' }, 6);
                string sub = f[1];
                string san = f[2];
                string result = f[3];
                string expected = f[5];
                int ex = expected.IndexOf("|EX=", System.StringComparison.Ordinal);
                if (ex >= 0) { expected = expected.Substring(0, ex); }
                string actual = RunNew(script, sub, san, result, presets[f[4]], reached);
                compared++;
                if (actual != expected && mismatches.Count < 15)
                {
                    mismatches.Add(row.Substring(0, row.IndexOf("|calls=", System.StringComparison.Ordinal)) + "\n  기대 " + expected + "\n  실제 " + actual);
                }
            }
            Assert.IsTrue(compared > 300, "골든 행이 너무 적다: " + compared);
            Assert.AreEqual(0, mismatches.Count, "동치 불일치:\n" + string.Join("\n", mismatches.ToArray()));
            foreach (string nodeId in script.Nodes.Keys)
            {
                if (DialogPilotEquivalenceTests.SewerS2Nodes.Contains(nodeId)) { continue; }
                Assert.IsTrue(reached.Contains(nodeId), "프리셋이 덮지 못한 노드: " + nodeId);
            }
        }

        /// <summary>RetrunDialogResult 의 새 경로: sub 판정 → SAN 키 판정 순(옛 if 블록 순서).</summary>
        private static string RunNew(StoryScript script, string sub, string san, string result, string preset, HashSet<string> reached)
        {
            GameState gs = StatePreset.Build(preset);
            GameState before = gs.Clone();
            GameStateAccess acc = new GameStateAccess(gs);
            JudgeOutcome outcome = JudgeOutcomes.FromLegacy(result);
            List<IList<string>> calls = new List<IList<string>>();
            string[] keys = san.Length > 0 ? new[] { sub, "SanCheck/" + san } : new[] { sub };
            JudgeScene plan = JudgeScene.None;
            for (int k = 0; k < keys.Length; k++)
            {
                List<string> errors = new List<string>();
                StoryResolution res = StoryResolver.ResolveJudge(script, keys[k], outcome, acc, acc, DialogPilotEquivalenceTests.Csv, errors);
                Assert.AreEqual(0, errors.Count, keys[k] + " -> " + string.Join(" | ", errors));
                if (res == null) { continue; }
                for (int n = 0; n < res.NodeIds.Count; n++) { reached.Add(res.NodeIds[n]); }
                if (res.Lines.Count > 0)
                {
                    List<string> refs = new List<string>();
                    for (int i = 0; i < res.Lines.Count; i++) { refs.Add(res.Lines[i].ToString()); }
                    calls.Add(refs);
                }
            }
            plan |= JudgeSceneRules.Plan(sub, outcome);
            string ext = "";
            if ((plan & JudgeScene.ConditionGood) != 0) { ext += "cond:Good"; }
            if ((plan & JudgeScene.StoryDirecting) != 0) { ext += (ext.Length > 0 ? "," : "") + "sd"; }
            if (ext.Length == 0) { ext = "-"; }
            return StatePreset.DescribeCalls(calls, StatePreset.Diff(before, gs)) + "|ext=" + ext;
        }

        /// <summary>
        /// 골든에서 뺀 에뮬레이션 2건의 새 경로: "Aiden : TakeAway" 실패는 줄 75-78 만(상태 변화 없음)·HP 5 감소, "Fabian : Swimming" 실패는 대사 없음·HP 5 감소 + 재판정.
        /// </summary>
        [Test]
        public void Judge_TakeAwayFail_And_SwimmingFail()
        {
            StoryScript script = DialogPilotEquivalenceTests.Script(JudgeScript);
            GameState gs = StatePreset.Build("");
            GameStateAccess acc = new GameStateAccess(gs);
            List<string> errors = new List<string>();
            StoryResolution r = StoryResolver.ResolveJudge(script, "Aiden : TakeAway", JudgeOutcome.CritFail, acc, acc, DialogPilotEquivalenceTests.Csv, errors);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(4, r.Lines.Count);
            Assert.AreEqual("1st_Client'sHouse:75", r.Lines[0].ToString());
            Assert.AreEqual("1st_Client'sHouse:78", r.Lines[3].ToString());
            Assert.IsFalse(gs.GetFlag("getSafe"));
            Assert.AreEqual(JudgeScene.HpDown5, JudgeSceneRules.Plan("Aiden : TakeAway", JudgeOutcome.Fail));
            StoryResolution s = StoryResolver.ResolveJudge(script, "Fabian : Swimming", JudgeOutcome.Fail, acc, acc, DialogPilotEquivalenceTests.Csv, errors);
            Assert.AreEqual(0, s.Lines.Count);
            Assert.AreEqual(JudgeScene.HpDown5 | JudgeScene.SwimRetry, JudgeSceneRules.Plan("Fabian : Swimming", JudgeOutcome.CritFail));
            Assert.AreEqual(JudgeScene.None, JudgeSceneRules.Plan("Fabian : Swimming", JudgeOutcome.Success));
        }

        [Test]
        public void JudgeOutcomes_FromLegacy_MapsAllRollerStrings()
        {
            Assert.AreEqual(JudgeOutcome.CritSuccess, JudgeOutcomes.FromLegacy("Result : Critical Success"));
            Assert.AreEqual(JudgeOutcome.Success, JudgeOutcomes.FromLegacy("Result : Success"));
            Assert.AreEqual(JudgeOutcome.Fail, JudgeOutcomes.FromLegacy("Result : Failure"));
            Assert.AreEqual(JudgeOutcome.CritFail, JudgeOutcomes.FromLegacy("Result : Fumble"));
            Assert.AreEqual(JudgeOutcome.None, JudgeOutcomes.FromLegacy("Result : --"));
            Assert.AreEqual(JudgeOutcome.None, JudgeOutcomes.FromLegacy(null));
        }

        /// <summary>대성공/대실패 칸이 있으면 그쪽, 비면 성공/실패 칸(옛 코드의 대성공=성공, 대실패=실패).</summary>
        [Test]
        public void JudgeBranch_CritSlots_FallBackToPlain()
        {
            string json = "{\"id\":\"t\",\"topics\":[{\"id\":\"j\",\"cases\":[{\"node\":\"j\"}]}],\"nodes\":{"
                + "\"j\":{\"onSuccess\":\"s\",\"onFail\":\"f\",\"onCritSuccess\":\"cs\",\"onCritFail\":\"cf\",\"onEnter\":\"set:always\"},"
                + "\"s\":{\"onEnter\":\"set:s\"},\"f\":{\"onEnter\":\"set:f\"},\"cs\":{\"onEnter\":\"set:cs\"},\"cf\":{\"onEnter\":\"set:cf\"}}}";
            StoryLoadResult lr = StoryScriptLoader.Load(json);
            Assert.IsTrue(lr.Ok, string.Join("|", lr.Errors));
            JudgeOutcome[] all = { JudgeOutcome.Success, JudgeOutcome.CritSuccess, JudgeOutcome.Fail, JudgeOutcome.CritFail, JudgeOutcome.None };
            string[] flag = { "s", "cs", "f", "cf", null };
            for (int i = 0; i < all.Length; i++)
            {
                GameState gs = GameState.New();
                GameStateAccess acc = new GameStateAccess(gs);
                StoryResolution r = StoryResolver.ResolveJudge(lr.Script, "j", all[i], acc, acc, DialogPilotEquivalenceTests.Csv, new List<string>());
                Assert.IsNotNull(r);
                Assert.IsTrue(gs.GetFlag("always"), "결과와 무관한 효과는 항상 적용");
                if (flag[i] != null) { Assert.IsTrue(gs.GetFlag(flag[i]), all[i].ToString()); }
                Assert.AreEqual(flag[i] == null ? 1 : 2, r.NodeIds.Count);
            }
            // 대성공/대실패 칸을 비우면 성공/실패로
            string json2 = json.Replace(",\"onCritSuccess\":\"cs\",\"onCritFail\":\"cf\"", "");
            StoryLoadResult lr2 = StoryScriptLoader.Load(json2);
            GameState g2 = GameState.New();
            GameStateAccess a2 = new GameStateAccess(g2);
            StoryResolver.ResolveJudge(lr2.Script, "j", JudgeOutcome.CritSuccess, a2, a2, DialogPilotEquivalenceTests.Csv, new List<string>());
            Assert.IsTrue(g2.GetFlag("s"));
            StoryResolver.ResolveJudge(lr2.Script, "j", JudgeOutcome.CritFail, a2, a2, DialogPilotEquivalenceTests.Csv, new List<string>());
            Assert.IsTrue(g2.GetFlag("f"));
        }

        [Test]
        public void JudgeLoader_UnknownBranch_ReportsError()
        {
            StoryLoadResult lr = StoryScriptLoader.Load("{\"id\":\"t\",\"topics\":[],\"nodes\":{\"j\":{\"onSuccess\":\"nope\"}}}");
            Assert.IsFalse(lr.Ok);
        }
    }
}
