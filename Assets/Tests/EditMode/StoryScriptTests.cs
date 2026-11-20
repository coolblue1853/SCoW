using NUnit.Framework;
using SCoW.Core.State;
using SCoW.Core.Story;

namespace SCoW.Tests
{
    public sealed class StoryScriptTests
    {
        private const string Json = @"{
  ""id"": ""1st_Bar"",
  ""topics"": [
    { ""id"": ""Maeve_FirstDialog"", ""cases"": [
      { ""when"": ""flag:maeveFirstDialog"", ""node"": ""again"" },
      { ""when"": ""time>=12 && !flag:x"", ""pre"": ""set:maeveFirstDialog;add:san,-1"", ""node"": ""first"" },
      { ""node"": ""fallback"" }
    ] }
  ],
  ""nodes"": {
    ""first"": { ""speaker"": ""Maeve"", ""text"": ""1"", ""onEnter"": ""set:met"", ""next"": ""ask"" },
    ""ask"": { ""speaker"": ""Fab"", ""text"": ""2"", ""choices"": [
        { ""text"": ""3"", ""next"": ""again"" },
        { ""text"": ""4"", ""when"": ""flag:met"", ""next"": ""fallback"" } ],
      ""check"": ""observation,hard"", ""onSuccess"": ""again"", ""onFail"": ""fallback"" },
    ""again"": { ""text"": ""5 é\n"" },
    ""fallback"": { ""text"": ""6"" }
  }
}";

        [Test]
        public void Load_BuildsModel()
        {
            StoryLoadResult r = StoryScriptLoader.Load(Json);
            Assert.IsTrue(r.Ok, string.Join("|", r.Errors));
            StoryScript sc = r.Script;
            Assert.AreEqual("1st_Bar", sc.Id);
            Assert.AreEqual(4, sc.Nodes.Count);
            Assert.AreEqual("ask", sc.Nodes["first"].Next);
            Assert.AreEqual(2, sc.Nodes["ask"].Choices.Length);
            Assert.AreEqual("observation,hard", sc.Nodes["ask"].CheckId);
            Assert.AreEqual("5 é\n", sc.Nodes["again"].TextKey);
            Assert.AreEqual(1, sc.Nodes["first"].OnEnter.Length);
        }

        [Test]
        public void Pick_FirstMatchingCase_WithFallback()
        {
            StoryScript sc = StoryScriptLoader.Load(Json).Script;
            GameState gs = new GameState();
            GameStateAccess a = new GameStateAccess(gs);
            Assert.AreEqual("fallback", sc.Pick("Maeve_FirstDialog", a).NodeId);
            gs.SetCounter("time", 13);
            StoryCase c = sc.Pick("Maeve_FirstDialog", a);
            Assert.AreEqual("first", c.NodeId);
            EffectRunner.ApplyAll(c.Pre, a, a, null);
            Assert.AreEqual("again", sc.Pick("Maeve_FirstDialog", a).NodeId);
            Assert.AreEqual(-1, gs.GetCounter("san"));
            Assert.IsNull(sc.Pick("nope", a));
        }

        [Test]
        public void Load_ReportsBadJsonDslAndDanglingRefs()
        {
            Assert.IsFalse(StoryScriptLoader.Load("{ bad").Ok);
            Assert.IsFalse(StoryScriptLoader.Load("[]").Ok);
            Assert.IsFalse(StoryScriptLoader.Load(null).Ok);
            StoryLoadResult r = StoryScriptLoader.Load(
                "{\"topics\":[{\"id\":\"t\",\"cases\":[{\"when\":\"flag:\",\"node\":\"zz\"}]}],\"nodes\":{\"a\":{\"onEnter\":\"zap:1\",\"next\":\"qq\"}}}");
            Assert.AreEqual(4, r.Errors.Count, string.Join("|", r.Errors));
        }

        [Test]
        public void Load_FailedWhen_IsFalseAndScriptNull()
        {
            StoryLoadResult r = StoryScriptLoader.Load(
                "{\"topics\":[{\"id\":\"t\",\"cases\":[{\"when\":\"flag:\",\"node\":\"a\"}]}],\"nodes\":{\"a\":{\"text\":\"x\"}}}");
            Assert.IsFalse(r.Ok);
            Assert.IsNull(r.Script, "오류가 있으면 Script 는 null");
            Assert.IsNotNull(StoryScriptLoader.Load("{\"nodes\":{\"a\":{}}}").Script);
        }

        [Test]
        public void MiniJson_UnicodeEscape_StrictHex()
        {
            string err;
            Assert.AreEqual("Aé", MiniJson.Parse("\"" + @"\u0041\u00E9" + "\"", out err));
            Assert.IsNull(err);
            MiniJson.Parse("\"" + @"\u+041" + "\"", out err);
            Assert.IsNotNull(err);
            MiniJson.Parse("\"" + @"\u 041" + "\"", out err);
            Assert.IsNotNull(err);
            MiniJson.Parse("\"" + @"\u00" + "\"", out err);
            Assert.IsNotNull(err);
        }

        [Test]
        public void MiniJson_Basics()
        {
            string err;
            object v = MiniJson.Parse("{\"a\":[1,2.5,-3e1,true,false,null,\"x\\\"y\"],\"b\":{}}", out err);
            Assert.IsNull(err);
            System.Collections.Generic.Dictionary<string, object> d = (System.Collections.Generic.Dictionary<string, object>)v;
            System.Collections.Generic.List<object> l = (System.Collections.Generic.List<object>)d["a"];
            Assert.AreEqual(7, l.Count);
            Assert.AreEqual(-30.0, (double)l[2]);
            Assert.AreEqual("x\"y", l[6]);
            MiniJson.Parse("{\"a\":1,}", out err);
            Assert.IsNotNull(err);
            MiniJson.Parse("[1] x", out err);
            Assert.IsNotNull(err);
            MiniJson.Parse("\"abc", out err);
            Assert.IsNotNull(err);
        }
    }
}
