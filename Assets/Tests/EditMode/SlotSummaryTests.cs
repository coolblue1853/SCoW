using System;
using NUnit.Framework;
using SCoW.Core.Save;

namespace SCoW.Tests
{
    public sealed class SlotSummaryTests
    {
        [Test]
        public void Line_FormatsDayTimePlaceAndSavedAt()
        {
            string s = SlotSummary.Line(3, "Noon", "Slum", new DateTime(2026, 11, 25, 14, 5, 0));
            Assert.AreEqual("Day 3 Noon / Slum / 11-25 14:05", s);
        }

        [Test]
        public void VersionLabel_PrefixesV_EmptyStaysEmpty()
        {
            Assert.AreEqual("v0.1.1", SlotSummary.VersionLabel("0.1.1"));
            Assert.AreEqual(string.Empty, SlotSummary.VersionLabel(null));
            Assert.AreEqual(string.Empty, SlotSummary.VersionLabel(string.Empty));
        }
    }
}
