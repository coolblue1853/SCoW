using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.Rules;
using SCoW.Core.State;
using SCoW.Core.Ui;

namespace SCoW.Tests
{
    /// <summary>PT-6: 다인원 전투 선택 입력 규칙(BattleSelectNav) — 메뉴 이동, 대상 이동, 대열 교체 단축키.</summary>
    public sealed class PartyInputTests
    {
        private sealed class ZeroRng : IRng
        {
            public int Next(int minInclusive, int maxExclusive)
            {
                return minInclusive;
            }
        }

        private static PartyMember Fabian()
        {
            Dictionary<string, int> skills = new Dictionary<string, int>();
            for (int i = 0; i < CompanionDefs.SkillKeys.Length; i++)
            {
                skills[CompanionDefs.SkillKeys[i]] = 20;
            }
            return PartyMember.CreateLeader("fabian", 52, 52, 50, 50, 52, 52, skills, new string[0]);
        }

        [Test]
        public void MoveMenu_SkipsDisabledAndWraps()
        {
            bool[] enabled = { true, false, true, false };
            Assert.AreEqual(2, BattleSelectNav.MoveMenu(enabled, 0, 1));
            Assert.AreEqual(0, BattleSelectNav.MoveMenu(enabled, 2, 1), "끝에서 처음으로");
            Assert.AreEqual(2, BattleSelectNav.MoveMenu(enabled, 0, -1), "처음에서 위로 = 마지막 활성");
            Assert.AreEqual(1, BattleSelectNav.MoveMenu(new[] { false, false }, 1, 1), "전부 비활성이면 그대로");
            Assert.AreEqual(0, BattleSelectNav.MoveMenu(enabled, 0, 0));
        }

        [Test]
        public void MoveTarget_AllyFlipsScreenDirection_EnemyDoesNot_BothClamp()
        {
            Assert.AreEqual(1, BattleSelectNav.MoveTarget(0, 3, 1, Side.Enemy), "적: 오른쪽 = 칸 번호 증가");
            Assert.AreEqual(0, BattleSelectNav.MoveTarget(0, 3, -1, Side.Enemy), "왼쪽 끝에서 멈춤");
            Assert.AreEqual(1, BattleSelectNav.MoveTarget(0, 3, -1, Side.Ally), "아군: 왼쪽 = 칸 번호 증가");
            Assert.AreEqual(0, BattleSelectNav.MoveTarget(0, 3, 1, Side.Ally), "아군 오른쪽 끝에서 멈춤");
            Assert.AreEqual(2, BattleSelectNav.MoveTarget(2, 3, 1, Side.Enemy), "오른쪽 끝에서 멈춤");
            Assert.AreEqual(0, BattleSelectNav.MoveTarget(5, 0, 1, Side.Ally));
        }

        [Test]
        public void SwapTargetRank_ScreenDirectionToNeighbor_OrMinusOne()
        {
            Assert.AreEqual(2, BattleSelectNav.SwapTargetRank(1, -1, new[] { 0, 2 }), "왼쪽 = 칸 번호 +1");
            Assert.AreEqual(0, BattleSelectNav.SwapTargetRank(1, 1, new[] { 0, 2 }), "오른쪽 = 칸 번호 -1");
            Assert.AreEqual(1, BattleSelectNav.SwapTargetRank(0, -1, new[] { 1 }));
            Assert.AreEqual(-1, BattleSelectNav.SwapTargetRank(0, 1, new[] { 1 }), "맨 오른쪽에서 오른쪽은 불가");
            Assert.AreEqual(-1, BattleSelectNav.SwapTargetRank(0, -1, new int[0]), "빈 대상");
            Assert.AreEqual(-1, BattleSelectNav.SwapTargetRank(0, 0, new[] { 1 }));
            Assert.AreEqual(-1, BattleSelectNav.SwapTargetRank(0, 1, null));
        }

        [Test]
        public void FindEnabledKind_IgnoresDisabled()
        {
            ActionKind[] kinds = { ActionKind.Melee, ActionKind.Swap, ActionKind.Run };
            Assert.AreEqual(1, BattleSelectNav.FindEnabledKind(kinds, new[] { true, true, true }, ActionKind.Swap));
            Assert.AreEqual(-1, BattleSelectNav.FindEnabledKind(kinds, new[] { true, false, true }, ActionKind.Swap));
        }

        [Test]
        public void QuickSwap_RealField_MiddleMemberSwapsLeft_ThenRightIsNeighbor()
        {
            PartyMember albert = PartyMember.FromCompanion(CompanionDefs.Albert, 75, 55);
            PartyMember maeve = PartyMember.FromCompanion(CompanionDefs.Maeve, 55, 65);
            PartyMember fabian = Fabian();
            BattleField f = new BattleField(new[] { albert, maeve, fabian }, new EnemyUnit[0]);
            int left = BattleSelectNav.SwapTargetRank(maeve.Rank, -1, f.ValidTargets(maeve, ActionKind.Swap));
            Assert.AreEqual(2, left);
            List<BattleEvent> ev = new List<BattleEvent>();
            f.Resolve(new BattleCommand(maeve, ActionKind.Swap, left, null), new ZeroRng(), ev);
            Assert.AreSame(maeve, f.At(Side.Ally, 2));
            Assert.AreSame(fabian, f.At(Side.Ally, 1));
            Assert.AreEqual(-1, BattleSelectNav.SwapTargetRank(maeve.Rank, -1, f.ValidTargets(maeve, ActionKind.Swap)), "이미 맨 왼쪽");
            Assert.AreEqual(1, BattleSelectNav.SwapTargetRank(maeve.Rank, 1, f.ValidTargets(maeve, ActionKind.Swap)));
        }
    }
}
