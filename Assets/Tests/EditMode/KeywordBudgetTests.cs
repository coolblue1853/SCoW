using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.Keyword;
using UnityEngine;

namespace SCoW.Tests
{
    /// <summary>KEYWORD-1 K7. 옛 ResetKeyCount 의 NPC 11분기 값이 KeywordBudget.json 으로 그대로 옮겨졌는지 본다.</summary>
    public sealed class KeywordBudgetTests
    {
        private static KeywordBudget LoadResource()
        {
            TextAsset ta = Resources.Load<TextAsset>("Story/KeywordBudget");
            Assert.IsNotNull(ta, "Story/KeywordBudget.json 없음");
            List<string> errors = new List<string>();
            KeywordBudget b = KeywordBudget.Load(ta.text, errors);
            Assert.IsNotNull(b, string.Join(" | ", errors));
            return b;
        }

        [Test]
        public void Resource_MatchesOldHardcodedBranches()
        {
            KeywordBudget b = LoadResource();
            string[] npcs = { "Ella Triss", "Aiden Triss", "Berkeley Swain", "University student", "Kane", "Maeve", "Kate Lizzie", "Alan Parks", "Albert Bradley", "BlackWell", "Dusty Dave" };
            int[] want = { 3, 3, 4, 3, 3, 5, 4, 4, 4, 3, 3 };
            Assert.AreEqual(npcs.Length, b.Count);
            for (int i = 0; i < npcs.Length; i++)
            {
                int got;
                Assert.IsTrue(b.TryGet(npcs[i], out got), npcs[i]);
                Assert.AreEqual(want[i], got, npcs[i]);
            }
        }

        [Test]
        public void UnknownNpc_NotInTable()
        {
            int got;
            Assert.IsFalse(LoadResource().TryGet("GunshopMan", out got));
            Assert.IsFalse(LoadResource().TryGet(null, out got));
        }

        [Test]
        public void Load_RejectsBrokenInput()
        {
            List<string> e1 = new List<string>();
            Assert.IsNull(KeywordBudget.Load("{", e1));
            Assert.IsNotEmpty(e1);
            List<string> e2 = new List<string>();
            Assert.IsNull(KeywordBudget.Load("{\"budgets\":{\"A\":-1}}", e2));
            List<string> e3 = new List<string>();
            Assert.IsNull(KeywordBudget.Load("{\"budgets\":{\"A\":2.5}}", e3));
            List<string> e4 = new List<string>();
            Assert.IsNull(KeywordBudget.Load("{}", e4));
        }
    }
}
