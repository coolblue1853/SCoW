using NUnit.Framework;
using SCoW.Core.Rules;

namespace SCoW.Tests
{
    /// <summary>UX-4: 남은 점수 경고·요약 문구.</summary>
    public sealed class CharacterCreationTests
    {
        [Test]
        public void AllSpent_OnlyWhenEveryPoolIsZero()
        {
            Assert.IsTrue(CharacterCreation.AllSpent(0, 0, 0));
            Assert.IsFalse(CharacterCreation.AllSpent(5, 0, 0));
            Assert.IsFalse(CharacterCreation.AllSpent(0, 0, 10));
        }

        [Test]
        public void Remaining_ListsOnlyUnspentPools()
        {
            Assert.AreEqual(string.Empty, CharacterCreation.Remaining(0, 0, 0, false));
            Assert.AreEqual("Points left STR 10 DEX 5", CharacterCreation.Remaining(10, 0, 5, false));
            Assert.AreEqual("남은 점수 INT 15", CharacterCreation.Remaining(0, 15, 0, true));
        }

        [Test]
        public void Summary_ContainsEveryStat()
        {
            string s = CharacterCreation.Summary(60, 45, 55, 50, 40, 65, 30, 22);
            StringAssert.Contains("STR 60", s);
            StringAssert.Contains("DEX 55", s);
            StringAssert.Contains("SAN 65", s);
            StringAssert.Contains("$22", s);
        }
    }
}
