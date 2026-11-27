using NUnit.Framework;
using SCoW.Core.Rules;

namespace SCoW.Tests
{
    /// <summary>
    /// P14b F0: 슬롯별 연출 표(EnemyFxProfiles)가 출시판 BattleManager.RetrunRolletResult 에서 읽은 값과 같은지 고정한다.
    /// 기대값은 원본 블록(evasion 773~1020, counterattack 1021~1307, MA 1308~1582, SS 1583~2426 등)에서 읽어 하드코딩했다.
    /// </summary>
    public sealed class EnemyFxProfileDataTests
    {
        // 순서: H1, H2, H3, H4, D1 (EnemyDefs.All 과 같다)
        private static readonly CamZoom[] Zoom = { CamZoom.Midle, CamZoom.Down, CamZoom.Up, CamZoom.Up, CamZoom.None };
        private static readonly float[] Rotate = { 5f, 5f, 5f, 1f, 1f };
        private static readonly float[] EvadeSec = { 0.7f, 1f, 1f, 1f, 1f };
        private static readonly bool[] EvadeYFromEnemy = { true, false, false, false, false };
        private static readonly bool[] ZoomInEvadeFail = { true, true, true, false, false };
        private static readonly bool[] CamResetOnAttack = { false, false, false, true, true };

        // evasion·counterattack 실패
        private static readonly PlayerHitStyle[] EvadeHit = { PlayerHitStyle.None, PlayerHitStyle.Dagger, PlayerHitStyle.None, PlayerHitStyle.Dagger, PlayerHitStyle.Dagger };
        private static readonly string[] EvadeSound = { "MartialArts", "Dagger_Attack", "MartialArts", "Dagger_Attack", "Dagger_Attack" };
        private static readonly bool[] EvadeHitted = { true, true, true, false, true };
        private static readonly bool[] EvadeReset = { false, true, false, true, true };

        // MA_attack_Counter 실패
        private static readonly PlayerHitStyle[] MartialHit = { PlayerHitStyle.None, PlayerHitStyle.None, PlayerHitStyle.None, PlayerHitStyle.Dagger, PlayerHitStyle.None };
        private static readonly string[] MartialSound = { "MartialArts", "Dagger_Attack", "MartialArts", "Dagger_Attack", "Dagger_Attack" };
        private static readonly bool[] MartialHitted = { true, true, true, false, true };
        private static readonly bool[] MartialReset = { false, true, false, true, true };

        // SS_attack_Counter 실패(무기 3종 공통)
        private static readonly PlayerHitStyle[] MeleeHit = { PlayerHitStyle.None, PlayerHitStyle.None, PlayerHitStyle.None, PlayerHitStyle.Dagger, PlayerHitStyle.None };
        private static readonly string[] MeleeSound = { "MartialArts", "MartialArts", "MartialArts", "Dagger_Attack", "Dagger_Attack" };
        private static readonly bool[] MeleeHitted = { true, true, true, false, true };
        private static readonly bool[] MeleeReset = { false, true, false, true, true };

        [Test]
        public void SameLengthAsEnemyDefs()
        {
            Assert.AreEqual(EnemyDefs.All.Length, EnemyFxProfiles.All.Length);
            Assert.AreEqual(5, EnemyFxProfiles.All.Length);
        }

        [Test]
        public void CameraAndMoveValuesMatchRelease()
        {
            for (int i = 0; i < 5; i++)
            {
                EnemyFxProfile p = EnemyFxProfiles.All[i];
                string id = EnemyDefs.All[i].Id;
                Assert.AreEqual(Zoom[i], p.Zoom, id);
                Assert.AreEqual(Rotate[i], p.RotateDeg, id);
                Assert.AreEqual(EvadeSec[i], p.EvadeMoveSec, id);
                Assert.AreEqual(EvadeYFromEnemy[i], p.EvadeYFromEnemy, id);
                Assert.AreEqual(ZoomInEvadeFail[i], p.ZoomInEvadeFail, id);
                Assert.AreEqual(CamResetOnAttack[i], p.CamRotateResetOnAttack, id);
            }
        }

        [Test]
        public void EvadeFailHitMatchesRelease()
        {
            for (int i = 0; i < 5; i++)
            {
                AssertReaction(EnemyFxProfiles.All[i].EvadeFailHit, EvadeHit[i], EvadeSound[i], EvadeHitted[i], EvadeReset[i], "evade " + i);
            }
        }

        [Test]
        public void MartialFailHitMatchesRelease()
        {
            for (int i = 0; i < 5; i++)
            {
                AssertReaction(EnemyFxProfiles.All[i].MartialFailHit, MartialHit[i], MartialSound[i], MartialHitted[i], MartialReset[i], "martial " + i);
            }
        }

        [Test]
        public void MeleeFailHitMatchesRelease()
        {
            for (int i = 0; i < 5; i++)
            {
                AssertReaction(EnemyFxProfiles.All[i].MeleeFailHit, MeleeHit[i], MeleeSound[i], MeleeHitted[i], MeleeReset[i], "melee " + i);
            }
        }

        private static void AssertReaction(PlayerHitReaction r, PlayerHitStyle hit, string sound, bool hitted, bool reset, string label)
        {
            Assert.AreEqual(hit, r.PlayerHit, label);
            Assert.AreEqual(sound, r.HitSound, label);
            Assert.AreEqual(hitted, r.HittedSprite, label);
            Assert.AreEqual(reset, r.ResetPlayer, label);
        }
    }
}
