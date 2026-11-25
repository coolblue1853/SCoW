using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.Keyword;
using UnityEngine;

namespace SCoW.Tests
{
    /// <summary>KEYWORD-1 K6. 수첩 상세 패널 이름 → 상위 키워드 표가 카탈로그와 어긋나지 않는지 본다.</summary>
    public sealed class IntelUpperMapTests
    {
        private static KeywordCatalog LoadCatalog()
        {
            TextAsset ta = Resources.Load<TextAsset>("Story/KeywordCatalog");
            Assert.IsNotNull(ta, "Story/KeywordCatalog.json 없음");
            KeywordCatalogLoadResult r = KeywordCatalog.Load(ta.text);
            Assert.IsTrue(r.Ok, string.Join(" | ", r.Errors));
            return r.Catalog;
        }

        [Test]
        public void EveryMappedUpper_ExistsInCatalog_AndEveryCatalogUpper_IsMapped()
        {
            KeywordCatalog cat = LoadCatalog();
            HashSet<string> catalogIds = new HashSet<string>();
            for (int i = 0; i < cat.Uppers.Count; i++)
            {
                catalogIds.Add(cat.Uppers[i].Id);
            }
            HashSet<string> mapped = new HashSet<string>(IntelUpperMap.AllUpperIds());
            foreach (string id in mapped)
            {
                Assert.IsTrue(catalogIds.Contains(id), "표의 상위가 카탈로그에 없음: " + id);
            }
            foreach (string id in catalogIds)
            {
                Assert.IsTrue(mapped.Contains(id), "카탈로그 상위가 수첩 표에 없음: " + id);
            }
        }

        [Test]
        public void TryGetUpper_HandlesNamesAndUnknown()
        {
            string up;
            Assert.IsTrue(IntelUpperMap.TryGetUpper("Ella_Detail", out up));
            Assert.AreEqual("Ella Triss", up);
            Assert.IsTrue(IntelUpperMap.TryGetUpper("PlanetarySequence_Detail", out up));
            Assert.AreEqual("PlanetaryParade", up);
            Assert.IsFalse(IntelUpperMap.TryGetUpper("Gunsmith_Detail", out up));
            Assert.IsFalse(IntelUpperMap.TryGetUpper("Ella", out up));
            Assert.IsFalse(IntelUpperMap.TryGetUpper(null, out up));
        }
    }
}
