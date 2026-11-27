using NUnit.Framework;
using SCoW.Core.Rules;

namespace SCoW.Tests
{
    /// <summary>
    /// P14b: BattleManager 가 적 종류 대신 "행동 중인 슬롯의 Def"로 연출·판정을 고르므로,
    /// 행동 종류(손재주·독)가 옛 슬롯과 같은 슬롯에서만 나오는지 데이터로 고정한다.
    /// </summary>
    public sealed class EnemyDefDataTests
    {
        [Test]
        public void DeftnessOnlyHybrid3()
        {
            for (int i = 0; i < EnemyDefs.All.Length; i++)
            {
                Assert.AreEqual(i == 2, EnemyDefs.All[i].UsesDeftness, EnemyDefs.All[i].Id);
            }
        }

        [Test]
        public void SanAndPoisonOnlyDeepOne1()
        {
            for (int i = 0; i < EnemyDefs.All.Length; i++)
            {
                IntentWeights w = EnemyDefs.All[i].Intent;
                bool deepOne1 = i == 4;
                Assert.AreEqual(deepOne1, w.SanAttack > 0, EnemyDefs.All[i].Id);
                Assert.AreEqual(deepOne1, w.PoisonAttack > 0, EnemyDefs.All[i].Id);
            }
        }

        [Test]
        public void GetAndIndexOfMatchAll()
        {
            for (int i = 0; i < EnemyDefs.All.Length; i++)
            {
                Assert.AreSame(EnemyDefs.All[i], EnemyDefs.Get(EnemyDefs.All[i].Id));
                Assert.AreEqual(i, EnemyDefs.IndexOf(EnemyDefs.All[i].Id));
            }
            Assert.IsNull(EnemyDefs.Get("none"));
            Assert.AreEqual(-1, EnemyDefs.IndexOf("none"));
        }
    }
}
