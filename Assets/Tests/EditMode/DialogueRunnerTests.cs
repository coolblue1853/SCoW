using NUnit.Framework;
using SCoW.Core.State;
using SCoW.Core.Story;

namespace SCoW.Tests
{
    /// <summary>DialogueRunner·Backlog·ReadLog·TextSpeed: 스킵 정지 조건·자동 대기·링버퍼·단일 진행 경로(Architecture.md P8).</summary>
    public class DialogueRunnerTests
    {
        private static DialogueRunner Make(out ReadLog read)
        {
            read = new ReadLog();
            return new DialogueRunner(read, new Backlog(5));
        }

        private static void Show(DialogueRunner r, string id, LineKind k = LineKind.Normal, string text = "abc")
        {
            r.BeginLine("s", id, "A", text, k);
            r.TypingDone();
        }

        [Test]
        public void BacklogOverwritesOldestWhenFull()
        {
            Backlog b = new Backlog(3);
            for (int i = 1; i <= 5; i++)
            {
                b.Add("n", "t" + i);
            }
            Assert.AreEqual(3, b.Count);
            Assert.AreEqual("t3", b[0].Text);
            Assert.AreEqual("t5", b[2].Text);
        }

        [Test]
        public void BacklogIndexOutOfRangeThrows()
        {
            Backlog b = new Backlog(3);
            b.Add("n", "x");
            Assert.Throws<System.ArgumentOutOfRangeException>(() => { BacklogEntry e = b[1]; });
        }

        [Test]
        public void BacklogPlainStripsTags()
        {
            Assert.AreEqual("hi there", Backlog.Plain("<color=#FFFFFF>hi</color> <i>there</i>"));
            Assert.AreEqual("a < b", Backlog.Plain("a < b"));
        }

        [Test]
        public void ReadLogRoundTripAndCorruption()
        {
            ReadLog r = new ReadLog();
            r.Add("a#2");
            r.Add("a#1");
            r.Add("a#1");
            Assert.AreEqual(2, r.Count);
            ReadLog d = ReadLog.Decode(r.Encode());
            Assert.IsTrue(d.Contains("a#1"));
            Assert.IsTrue(d.Contains("a#2"));
            Assert.AreEqual(r.Encode(), d.Encode());
            Assert.AreEqual(0, ReadLog.Decode("garbage\na#1").Count);
            Assert.AreEqual(0, ReadLog.Decode(null).Count);
        }

        [Test]
        public void AdvanceCompletesTypingThenGoesNext()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            r.BeginLine("s", "1", "A", "abc", LineKind.Normal);
            Assert.AreEqual(RunnerAction.CompleteTyping, r.Advance());
            r.TypingDone();
            Assert.AreEqual(RunnerAction.NextLine, r.Advance());
        }

        [Test]
        public void NextLineCannotFireTwiceForOneLine()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            Show(r, "1");
            Assert.AreEqual(RunnerAction.NextLine, r.Advance());
            Assert.AreEqual(RunnerAction.None, r.Advance());
            Assert.AreEqual(RunnerAction.None, r.Tick(10f));
        }

        [Test]
        public void ChoicePendingBlocksAdvanceSkipAndAuto()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            r.AutoOn = true;
            r.SkipAll = true;
            r.SetSkipToggle(true);
            Show(r, "1");
            r.SetPending(Pending.Choice);
            Assert.AreEqual(RunnerAction.None, r.Advance());
            Assert.AreEqual(RunnerAction.None, r.Tick(100f));
            Assert.IsFalse(r.SkipActive);
            Assert.AreEqual("choice", r.LastStopReason);
        }

        [Test]
        public void CheckPendingStopsSkipButAllowsManualAdvance()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            r.SkipAll = true;
            r.SetSkipToggle(true);
            Show(r, "1");
            r.SetPending(Pending.Check);
            Assert.IsFalse(r.SkipActive);
            Assert.AreEqual(RunnerAction.None, r.Tick(100f));
            Assert.AreEqual(RunnerAction.NextLine, r.Advance());
        }

        [Test]
        public void SkipStopsAtUnreadLineInReadOnlyMode()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            read.Add(ReadLog.KeyOf("s", "1"));
            r.SetSkipToggle(true);
            r.BeginLine("s", "1", "A", "abc", LineKind.Normal);
            Assert.IsTrue(r.SkipActive);
            Assert.IsTrue(r.CurrentWasRead);
            r.TypingDone();
            r.BeginLine("s", "2", "A", "abc", LineKind.Normal);
            Assert.IsFalse(r.SkipActive);
            Assert.AreEqual("unread", r.LastStopReason);
        }

        [Test]
        public void SkipAllModeDoesNotStopAtUnread()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            r.SkipAll = true;
            r.SetSkipToggle(true);
            r.BeginLine("s", "9", "A", "abc", LineKind.Normal);
            Assert.IsTrue(r.SkipActive);
        }

        [Test]
        public void SkipStopsAtChoiceAndCheckLinesEvenIfRead()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            r.SkipAll = true;
            r.SetSkipToggle(true);
            r.BeginLine("s", "1", "A", "x", LineKind.Choice);
            Assert.IsFalse(r.SkipActive);
            r.SetSkipToggle(true);
            r.BeginLine("s", "2", "A", "x", LineKind.Check);
            Assert.IsFalse(r.SkipActive);
            Assert.AreEqual("check", r.LastStopReason);
        }

        [Test]
        public void SkipTickCompletesTypingThenAdvancesAfterInterval()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            r.SkipAll = true;
            r.SetSkipToggle(true);
            r.BeginLine("s", "1", "A", "abc", LineKind.Normal);
            Assert.AreEqual(RunnerAction.CompleteTyping, r.Tick(0.01f));
            r.TypingDone();
            Assert.AreEqual(RunnerAction.None, r.Tick(DialogueRunner.SkipInterval * 0.5f));
            Assert.AreEqual(RunnerAction.NextLine, r.Tick(DialogueRunner.SkipInterval));
        }

        [Test]
        public void HeldSkipSuppressedAfterStopUntilReleased()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            r.SetSkipHeld(true);
            r.BeginLine("s", "new", "A", "abc", LineKind.Normal);
            Assert.IsFalse(r.SkipActive);
            r.SetSkipHeld(false);
            r.SetSkipHeld(true);
            Assert.IsTrue(r.SkipActive);
        }

        [Test]
        public void AutoWaitsProportionalToLengthAndCaps()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            r.AutoOn = true;
            Show(r, "1", LineKind.Normal, new string('a', 10));
            float need = DialogueRunner.AutoWait(10);
            Assert.AreEqual(RunnerAction.None, r.Tick(need - 0.01f));
            Assert.AreEqual(RunnerAction.NextLine, r.Tick(0.02f));
            Assert.AreEqual(DialogueRunner.AutoMax, DialogueRunner.AutoWait(100000));
            Assert.Less(DialogueRunner.AutoWait(5), DialogueRunner.AutoWait(50));
        }

        [Test]
        public void AutoDoesNotRunWhileTyping()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            r.AutoOn = true;
            r.BeginLine("s", "1", "A", "abc", LineKind.Normal);
            Assert.AreEqual(RunnerAction.None, r.Tick(100f));
        }

        [Test]
        public void EndTurnsSkipOffKeepsAuto()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            r.AutoOn = true;
            r.SetSkipToggle(true);
            r.End();
            Assert.IsFalse(r.SkipActive);
            Assert.IsTrue(r.AutoOn);
            Assert.AreEqual(RunnerState.Idle, r.State);
        }

        [Test]
        public void BeginLineFillsBacklogAndReadLog()
        {
            ReadLog read;
            DialogueRunner r = Make(out read);
            r.BeginLine("s", "1", "Fabian", "hello", LineKind.Normal);
            Assert.AreEqual(1, r.Backlog.Count);
            Assert.AreEqual("Fabian", r.Backlog[0].Speaker);
            Assert.IsFalse(r.CurrentWasRead);
            Assert.IsTrue(read.Contains("s#1"));
        }

        [Test]
        public void TextSpeedClampsAndIsMonotonic()
        {
            Assert.AreEqual(TextSpeed.DelayOf(1), TextSpeed.DelayOf(-5));
            Assert.AreEqual(TextSpeed.DelayOf(5), TextSpeed.DelayOf(99));
            for (int l = 1; l < 5; l++)
            {
                Assert.Greater(TextSpeed.DelayOf(l), TextSpeed.DelayOf(l + 1));
            }
            Assert.AreEqual(0.03f, TextSpeed.DelayOf(TextSpeed.DefaultLevel));
        }

        [Test]
        public void LineMarksClassifyChoiceAndCheck()
        {
            Assert.AreEqual(LineKind.Choice, LineMarks.Classify("그래서 ★①"));
            Assert.AreEqual(LineKind.Choice, LineMarks.Classify("a☆b"));
            Assert.AreEqual(LineKind.Check, LineMarks.Classify("x♠②"));
            Assert.AreEqual(LineKind.Normal, LineMarks.Classify("plain"));
            Assert.AreEqual(LineKind.Normal, LineMarks.Classify(null));
        }
    }
}
