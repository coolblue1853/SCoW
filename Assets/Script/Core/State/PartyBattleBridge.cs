using System;
using System.Collections.Generic;
using SCoW.Core.Rules;

namespace SCoW.Core.State
{
    /// <summary>
    /// 전투 판(PartyMember)과 세이브 키(party.*, PartyState)를 잇는 환원 경로(Party.md 6절).
    /// 전투 시작에 BuildRoster 로 명단을 만들고, 종료(Settle 뒤)에 WriteBack 으로 한 번 환원한다. 전투 중에는 GameState 를 건드리지 않는다.
    /// 파비안(리더)의 HP·SAN 은 옛 전역(DataBaseManager)이 원본이라 여기서 읽고 쓰지 않는다.
    /// </summary>
    public static class PartyBattleBridge
    {
        /// <summary>Recovering 은 다음 시간대(timeCount + 1)부터 다시 나선다.</summary>
        public const int RecoveringReturnPhases = 1;
        /// <summary>복귀할 때 HP 는 최소 최대치의 절반(MaxHp / 2). 정수 나눗셈은 내림.</summary>
        public const int RecoveringReturnHpDivisor = 2;
        /// <summary>Broken 이탈 후 재합류 때 1회 SAN 회복.</summary>
        public const int BrokenReturnSan = 5;

        /// <summary>다음 일차 아침의 timeCount. 4칸이 하루, 나머지 1 = 아침(WorldClock). 음수 timeCount 는 지원하지 않는다.</summary>
        public static int NextMorning(int timeCount)
        {
            return timeCount + 4 - ((timeCount + 3) % 4);
        }

        /// <summary>
        /// 전투에 나설 명단. party.order 순서(파비안 자리는 leader)를 따르고 최대 3명.
        /// 합류했고 죽지 않았고 대기(standby)가 아니며 Ready 인 동료만. 복귀 시각이 지난 Recovering·Broken 은 먼저 되살려(상태를 쓴다) 포함한다.
        /// leader 가 null 이면 동료만. 동료 상태 Downed·Stable 은 전투 밖에 남지 않으므로(Settle 이 정리) 만나면 Ready 로 본다.
        /// </summary>
        public static List<PartyMember> BuildRoster(GameState s, PartyMember leader, int timeCount)
        {
            List<PartyMember> roster = new List<PartyMember>();
            List<string> order = PartyState.GetOrder(s);
            bool leaderPlaced = false;
            for (int i = 0; i < order.Count && roster.Count < BattleField.Ranks; i++)
            {
                if (order[i] == CompanionDefs.LeaderId)
                {
                    if (leader != null)
                    {
                        roster.Add(leader);
                        leaderPlaced = true;
                    }
                    continue;
                }
                PartyMember m = LoadCompanion(s, CompanionDefs.Get(order[i]), timeCount);
                if (m != null)
                {
                    roster.Add(m);
                }
            }
            if (leader != null && !leaderPlaced && roster.Count < BattleField.Ranks)
            {
                roster.Insert(0, leader);
            }
            return roster;
        }

        private static PartyMember LoadCompanion(GameState s, CompanionDef def, int timeCount)
        {
            if (def == null || !PartyState.IsJoined(s, def.Id) || PartyState.IsDead(s, def.Id) || PartyState.IsStandby(s, def.Id))
            {
                return null;
            }
            UnitStatus status = PartyState.GetStatus(s, def.Id);
            if (status == UnitStatus.Recovering || status == UnitStatus.Broken)
            {
                if (timeCount < s.GetCounter(PartyKeys.BackAt(def.Id)))
                {
                    return null;
                }
                Revive(s, def, status);
            }
            else if (status == UnitStatus.Fallen)
            {
                return null;
            }
            return PartyMember.FromCompanion(def, PartyState.GetHp(s, def.Id), PartyState.GetSan(s, def.Id));
        }

        private static void Revive(GameState s, CompanionDef def, UnitStatus from)
        {
            if (from == UnitStatus.Recovering)
            {
                PartyState.SetHp(s, def, Math.Max(PartyState.GetHp(s, def.Id), def.MaxHp / RecoveringReturnHpDivisor));
            }
            else
            {
                PartyState.SetSan(s, def, PartyState.GetSan(s, def.Id) + BrokenReturnSan);
            }
            PartyState.SetStatus(s, def.Id, UnitStatus.Ready);
            s.SetCounter(PartyKeys.BackAt(def.Id), 0);
        }

        /// <summary>
        /// Settle 뒤 환원: 동료 HP·SAN·상태, 사망(PartyMember.Dead 가 원본 -> PartyState.Kill), 복귀 시각(backAt).
        /// Recovering = timeCount + 1, Broken = 다음 일차 아침. 파비안은 건드리지 않는다.
        /// </summary>
        public static void WriteBack(GameState s, IReadOnlyList<PartyMember> roster, int timeCount)
        {
            for (int i = 0; i < roster.Count; i++)
            {
                PartyMember m = roster[i];
                CompanionDef def = m.IsLeader ? null : CompanionDefs.Get(m.Id);
                if (def == null)
                {
                    continue;
                }
                if (m.Dead)
                {
                    PartyState.Kill(s, def.Id);
                    PartyState.SetSan(s, def, m.San);
                    continue;
                }
                PartyState.SetHp(s, def, m.Hp);
                PartyState.SetSan(s, def, m.San);
                PartyState.SetStatus(s, def.Id, m.Status);
                s.SetCounter(PartyKeys.BackAt(def.Id), BackAt(m.Status, timeCount));
            }
        }

        private static int BackAt(UnitStatus status, int timeCount)
        {
            if (status == UnitStatus.Recovering)
            {
                return timeCount + RecoveringReturnPhases;
            }
            return status == UnitStatus.Broken ? NextMorning(timeCount) : 0;
        }
    }
}
