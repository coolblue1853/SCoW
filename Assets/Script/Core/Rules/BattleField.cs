using System;
using System.Collections.Generic;

namespace SCoW.Core.Rules
{
    /// <summary>
    /// 아군·적 각 3칸(Rank 0 앞, 1 중, 2 뒤) 전투 판(Party.md 4절). 순수 C#, 헤드리스로 해결해 BattleEvent 를 낸다.
    /// 이 파일은 구조(칸·순서·대상·교환·승패·정산), 행동 해결은 BattleFieldActions.cs.
    /// </summary>
    public sealed partial class BattleField
    {
        public const int Ranks = 3;

        public readonly BattleSlot[] Allies = new BattleSlot[Ranks];
        public readonly BattleSlot[] Enemies = new BattleSlot[Ranks];

        /// <summary>파비안이 쓰러지면 카운트다운을 받는다(아군이 더 있을 때). 솔로는 HP 0 즉시 패배(옛 경로 불변).</summary>
        public bool LeaderDownRule;
        /// <summary>파비안도 발작을 일으키는가. 기본 꺼짐(파비안 SAN 연출은 View 룰렛이 맡는다, Party.md 4-6절).</summary>
        public bool LeaderBoutEnabled;

        private readonly List<PartyMember> _roster = new List<PartyMember>();
        private readonly PartyMember _leader;
        private bool _fled;
        private bool _settled;

        /// <summary>아군·적을 앞(Rank 0)부터 채운다. 각 3명 이하.</summary>
        public BattleField(IList<PartyMember> allies, IList<EnemyUnit> enemies)
        {
            if (allies.Count > Ranks || enemies.Count > Ranks)
            {
                throw new ArgumentException("칸은 편마다 3개다.");
            }
            for (int i = 0; i < Ranks; i++)
            {
                Allies[i] = new BattleSlot(Side.Ally, i);
                Enemies[i] = new BattleSlot(Side.Enemy, i);
            }
            for (int i = 0; i < allies.Count; i++)
            {
                Place(Allies[i], allies[i]);
                _roster.Add(allies[i]);
                if (allies[i].IsLeader)
                {
                    _leader = allies[i];
                }
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                Place(Enemies[i], enemies[i]);
            }
            LeaderDownRule = allies.Count > 1;
        }

        public PartyMember Leader => _leader;
        public IReadOnlyList<PartyMember> Roster => _roster;

        public IBattleUnit At(Side side, int rank)
        {
            BattleSlot[] slots = SlotsOf(side);
            return rank >= 0 && rank < Ranks ? slots[rank].Unit : null;
        }

        public int CountOn(Side side)
        {
            int n = 0;
            BattleSlot[] slots = SlotsOf(side);
            for (int i = 0; i < Ranks; i++)
            {
                if (slots[i].Unit != null)
                {
                    n++;
                }
            }
            return n;
        }

        private BattleSlot[] SlotsOf(Side side)
        {
            return side == Side.Ally ? Allies : Enemies;
        }

        private static void Place(BattleSlot slot, IBattleUnit unit)
        {
            slot.Unit = unit;
            if (unit != null)
            {
                unit.Rank = slot.Rank;
            }
        }

        // ---------- 순서 ----------

        /// <summary>칸에 선 행위자 전원. 아군 0..2, 적 0..2(옛 1:1 은 플레이어 -> 적 슬롯 순).</summary>
        public List<IBattleUnit> Order()
        {
            List<IBattleUnit> list = new List<IBattleUnit>();
            for (int i = 0; i < Ranks; i++)
            {
                if (Allies[i].Unit != null)
                {
                    list.Add(Allies[i].Unit);
                }
            }
            for (int i = 0; i < Ranks; i++)
            {
                if (Enemies[i].Unit != null)
                {
                    list.Add(Enemies[i].Unit);
                }
            }
            return list;
        }

        public IBattleUnit NextActor()
        {
            return BattleTimeline.Next(Order());
        }

        public List<IBattleUnit> Preview(int n)
        {
            return BattleTimeline.Preview(Order(), n);
        }

        // ---------- 대상·위치 규칙 ----------

        /// <summary>
        /// 행동이 닿는 칸 번호 목록(Party.md 4-3절). 아군: 근접은 앞·중 칸만 쓸 수 있고 적 앞·중에 닿는다(앞이 살아 있는 한 뒤 칸은 막힘),
        /// 사격은 사격 무기가 있어야 하며 적 전 칸. 진정은 중·뒤 칸만. 대상 없는 행동(방어·도주)은 자기 칸 하나. 행동 불가면 빈 목록.
        /// </summary>
        public List<int> ValidTargets(IBattleUnit actor, ActionKind kind)
        {
            List<int> result = new List<int>();
            if (actor == null || !actor.IsActing)
            {
                return result;
            }
            PartyMember m = actor as PartyMember;
            bool ally = actor.Side == Side.Ally;
            switch (kind)
            {
                case ActionKind.Melee:
                    if (ally)
                    {
                        if (actor.Rank < 2)
                        {
                            AddOccupied(Enemies, result, 2);
                        }
                    }
                    else
                    {
                        AddOccupied(Allies, result, Ranks);
                    }
                    break;
                case ActionKind.Ranged:
                    if (ally && m != null && PartyBattleRules.FindWeapon(m, true) != null)
                    {
                        AddOccupied(Enemies, result, Ranks);
                    }
                    break;
                case ActionKind.SanAttack:
                case ActionKind.PoisonAttack:
                    if (!ally)
                    {
                        AddOccupied(Allies, result, Ranks);
                    }
                    break;
                case ActionKind.Guard:
                    if (ally)
                    {
                        result.Add(actor.Rank);
                    }
                    break;
                case ActionKind.FirstAid:
                    if (ally)
                    {
                        AddFirstAidTargets(result);
                    }
                    break;
                case ActionKind.Calm:
                    if (ally && actor.Rank >= 1)
                    {
                        AddCalmTargets(actor, result);
                    }
                    break;
                case ActionKind.Swap:
                    if (ally)
                    {
                        AddAdjacent(actor.Rank, result);
                    }
                    break;
                case ActionKind.Run:
                    if (m != null && m.IsLeader)
                    {
                        result.Add(actor.Rank);
                    }
                    break;
            }
            return result;
        }

        private void AddOccupied(BattleSlot[] slots, List<int> result, int maxRankExclusive)
        {
            for (int i = 0; i < maxRankExclusive; i++)
            {
                if (slots[i].Unit != null)
                {
                    result.Add(i);
                }
            }
        }

        /// <summary>쓰러진 사람 먼저, 그 다음 HP 가 깎인 사람.</summary>
        private void AddFirstAidTargets(List<int> result)
        {
            for (int i = 0; i < Ranks; i++)
            {
                PartyMember t = Allies[i].Unit as PartyMember;
                if (t != null && t.Status == UnitStatus.Downed)
                {
                    result.Add(i);
                }
            }
            for (int i = 0; i < Ranks; i++)
            {
                PartyMember t = Allies[i].Unit as PartyMember;
                if (t != null && (t.Status == UnitStatus.Ready || t.Status == UnitStatus.Broken) && t.Hp < t.MaxHp)
                {
                    result.Add(i);
                }
            }
        }

        private void AddCalmTargets(IBattleUnit actor, List<int> result)
        {
            for (int i = 0; i < Ranks; i++)
            {
                PartyMember t = Allies[i].Unit as PartyMember;
                if (t != null && t != actor && t.Status == UnitStatus.Ready && t.Bout != BoutKind.None)
                {
                    result.Add(i);
                }
            }
        }

        private void AddAdjacent(int rank, List<int> result)
        {
            if (rank - 1 >= 0 && Allies[rank - 1].Unit != null)
            {
                result.Add(rank - 1);
            }
            if (rank + 1 < Ranks && Allies[rank + 1].Unit != null)
            {
                result.Add(rank + 1);
            }
        }

        /// <summary>인접한 두 칸의 위치 교환(둘 다 점유). 쓰러짐·광기 상태도 교환 가능. 행동이 곧 자기 차례라는 처리는 호출 측(Resolve).</summary>
        public bool Swap(Side side, int rankA, int rankB)
        {
            if (rankA < 0 || rankB < 0 || rankA >= Ranks || rankB >= Ranks || Math.Abs(rankA - rankB) != 1)
            {
                return false;
            }
            BattleSlot[] slots = SlotsOf(side);
            if (slots[rankA].Unit == null || slots[rankB].Unit == null)
            {
                return false;
            }
            ExchangeUnits(slots, rankA, rankB);
            return true;
        }

        private static void ExchangeUnits(BattleSlot[] slots, int a, int b)
        {
            IBattleUnit ua = slots[a].Unit;
            IBattleUnit ub = slots[b].Unit;
            Place(slots[a], ub);
            Place(slots[b], ua);
        }

        private void RemoveFromSlot(IBattleUnit unit)
        {
            BattleSlot[] slots = SlotsOf(unit.Side);
            for (int i = 0; i < Ranks; i++)
            {
                if (slots[i].Unit == unit)
                {
                    slots[i].Unit = null;
                }
            }
            Compact(slots);
        }

        /// <summary>빈 칸을 메워 뒤가 앞당겨진다(앞이 비면 압축).</summary>
        private static void Compact(BattleSlot[] slots)
        {
            List<IBattleUnit> kept = new List<IBattleUnit>();
            for (int i = 0; i < Ranks; i++)
            {
                if (slots[i].Unit != null)
                {
                    kept.Add(slots[i].Unit);
                }
            }
            for (int i = 0; i < Ranks; i++)
            {
                Place(slots[i], i < kept.Count ? kept[i] : null);
            }
        }

        // ---------- 승패·정산 ----------

        /// <summary>패배 조건을 먼저 본다. 승리 = 적 전멸, 패배 = 파비안 이탈·SAN 0·(솔로) HP 0 또는 행동할 아군이 없음, 도주 = Run 성공.</summary>
        public bool IsOver(out BattleOutcome outcome)
        {
            outcome = BattleOutcome.None;
            if (_fled)
            {
                outcome = BattleOutcome.Fled;
                return true;
            }
            if (IsLost())
            {
                outcome = BattleOutcome.Loss;
                return true;
            }
            if (CountOn(Side.Enemy) == 0)
            {
                outcome = BattleOutcome.Win;
                return true;
            }
            return false;
        }

        private bool IsLost()
        {
            if (_leader != null)
            {
                if (_leader.Status == UnitStatus.Fallen || _leader.San <= 0 || (!LeaderDownRule && _leader.Hp <= 0))
                {
                    return true;
                }
            }
            for (int i = 0; i < _roster.Count; i++)
            {
                if (_roster[i].IsActing)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 전투 종료 정산(사망은 이 한 곳에서만, Party.md 9절). 승리: 전선 이탈자는 Recovering(HP 1), 쓰러진·안정 동료는 Ready 로 회복(HP 최소 5).
        /// 도주·패배: 쓰러짐·안정·이탈 동료는 영구 사망. 파비안은 건드리지 않는다. 두 번째 호출은 빈 목록.
        /// </summary>
        public List<BattleEvent> Settle()
        {
            List<BattleEvent> events = new List<BattleEvent>();
            BattleOutcome outcome;
            if (_settled || !IsOver(out outcome))
            {
                return events;
            }
            _settled = true;
            for (int i = 0; i < _roster.Count; i++)
            {
                PartyMember m = _roster[i];
                if (m.IsLeader)
                {
                    continue;
                }
                bool down = m.Status == UnitStatus.Fallen || m.Status == UnitStatus.Downed || m.Status == UnitStatus.Stable;
                m.Poison = 0;
                m.Guarding = false;
                m.Bout = BoutKind.None;
                m.BoutTurns = 0;
                if (!down)
                {
                    continue;
                }
                if (outcome == BattleOutcome.Win)
                {
                    SettleWinner(m, events);
                }
                else
                {
                    m.Dead = true;
                    m.Status = UnitStatus.Fallen;
                    m.Hp = 0;
                    events.Add(new BattleEvent(BattleEventKind.Died, m.Id, m.Id));
                }
            }
            return events;
        }

        private static void SettleWinner(PartyMember m, List<BattleEvent> events)
        {
            if (m.Status == UnitStatus.Fallen)
            {
                m.Status = UnitStatus.Recovering;
                m.Hp = 1;
                events.Add(new BattleEvent(BattleEventKind.Recovering, m.Id, m.Id, 1));
            }
            else
            {
                m.Status = UnitStatus.Ready;
                m.Hp = Math.Max(m.Hp, PartyBattleRules.StableHp);
            }
        }
    }
}
