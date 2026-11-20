using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SCoW.Core.Story;

namespace SCoW.Tests
{
    public sealed class StoryCsvTests
    {
        [Test]
        public void Basic_HeaderSkipped_ColumnsMapped()
        {
            StoryCsvResult r = StoryCsvParser.Parse("ID,캐릭터이름,대사,메모\r\n1,Maeve,Hello,note\r\n2,Fab,Bye,\r\n");
            Assert.IsTrue(r.Ok, string.Join("|", r.Errors));
            Assert.AreEqual(2, r.Lines.Count);
            Assert.AreEqual("1", r.Lines[0].Id);
            Assert.AreEqual("Maeve", r.Lines[0].Speaker);
            Assert.AreEqual("Hello", r.Lines[0].Text);
            Assert.AreEqual("note", r.Lines[0].Memo);
            Assert.AreEqual("", r.Lines[1].Memo);
            Assert.AreEqual(2, r.Lines[0].SourceLine);
        }

        [Test]
        public void BlankRowSkipped_EmptyIdIsError_ExtraColumnsWarn()
        {
            StoryCsvResult r = StoryCsvParser.Parse("ID,n,t,m\n,,,\n1,A,x,y,extra\n,B,z\n,Fab,,\n");
            Assert.AreEqual(1, r.Lines.Count);
            Assert.AreEqual(1, r.Errors.Count, string.Join("|", r.Errors));
            StringAssert.Contains("ID", r.Errors[0]);
            Assert.AreEqual(2, r.Warnings.Count);
            Assert.AreEqual("y", r.Lines[0].Memo);
        }

        [Test]
        public void QuotedComma_Quote_AndNewline()
        {
            StoryCsvResult r = StoryCsvParser.Parse("ID,n,t\n1,A,\"a, b\"\n2,B,\"say \"\"hi\"\"\"\n3,C,\"l1\nl2\"\n4,D,x\n");
            Assert.IsTrue(r.Ok, string.Join("|", r.Errors));
            Assert.AreEqual(4, r.Lines.Count);
            Assert.AreEqual("a, b", r.Lines[0].Text);
            Assert.AreEqual("say \"hi\"", r.Lines[1].Text);
            Assert.AreEqual("l1\nl2", r.Lines[2].Text);
            Assert.AreEqual("4", r.Lines[3].Id);
            Assert.AreEqual(6, r.Lines[3].SourceLine);
        }

        [Test]
        public void LineEndings_CrLfLfCr_And_BlankLines()
        {
            StoryCsvResult r = StoryCsvParser.Parse("ID,n,t\r\n\r\n1,A,x\r2,B,y\n\n\n3,C,z");
            Assert.IsTrue(r.Ok, string.Join("|", r.Errors));
            Assert.AreEqual(3, r.Lines.Count);
            Assert.AreEqual("z", r.Lines[2].Text);
        }

        [Test]
        public void Bom_IsStripped()
        {
            StoryCsvResult r = StoryCsvParser.Parse("﻿ID,n,t\n1,A,x\n");
            Assert.AreEqual(1, r.Lines.Count);
            Assert.AreEqual("1", r.Lines[0].Id);
        }

        [Test]
        public void LegacyMarkers_KeptVerbatim()
        {
            string t = "@Who is this^ Fab.@ⓖ(fear)ⓦ ◈⑪ ⒳(x) 'q' \"mid\" tail";
            StoryCsvResult r = StoryCsvParser.Parse("ID,n,t\n1,A," + t + "\n");
            Assert.IsTrue(r.Ok, string.Join("|", r.Errors));
            Assert.AreEqual(t, r.Lines[0].Text);
        }

        [Test]
        public void Errors_ReturnedNotThrown()
        {
            StoryCsvResult a = StoryCsvParser.Parse("ID,n,t\n1,only\n2,B,ok\n");
            Assert.AreEqual(1, a.Errors.Count);
            Assert.AreEqual(1, a.Lines.Count);
            StoryCsvResult b = StoryCsvParser.Parse("ID,n,t\n1,A,\"open\n");
            Assert.IsFalse(b.Ok);
            Assert.IsFalse(StoryCsvParser.Parse(null).Ok);
            StoryCsvResult c = StoryCsvParser.Parse("ID,n,t\n1,A,\"x\"y\n");
            Assert.IsFalse(c.Ok);
        }

        [Test]
        public void Empty_NoLines()
        {
            Assert.AreEqual(0, StoryCsvParser.Parse("").Lines.Count);
            Assert.IsTrue(StoryCsvParser.Parse("").Ok);
        }

        [Test]
        public void LegacyCsvFiles_AllParseWithoutErrors()
        {
            string dir = Path.GetFullPath(Path.Combine(UnityEngine.Application.dataPath, "Script/Asset/Resources"));
            string[] files = Directory.GetFiles(dir, "*.csv");
            Assert.AreEqual(15, files.Length);
            foreach (string f in files)
            {
                StoryCsvResult r = StoryCsvParser.Parse(File.ReadAllText(f, new UTF8Encoding(false)));
                Assert.IsTrue(r.Ok, Path.GetFileName(f) + ": " + string.Join("|", r.Errors));
                Assert.Greater(r.Lines.Count, 100, Path.GetFileName(f));
            }
        }
    }
}
