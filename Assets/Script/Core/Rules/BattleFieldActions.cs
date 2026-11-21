using System;
using System.Collections.Generic;
using SCoW.Core.State;

namespace SCoW.Core.Rules
{
    /// <summary>BattleField 의 차례 시작·행동 해결(헤드리스). 난수는 전부 인자 rng 로 소비한다.</summary>
    public sealed partial class BattleField
    {
        // ---------- 차례 시작 ----------

        /// <summary>
        /// 차례를 받은 행위자의 시작 처리: 방어 해제, 독 틱, 쓰러짐 카운트다운, 발작 자동 행동.
        /// 반환 true = 지시(Resolve)를 받을 수 있다. false = 이번 차례는 자동으로 끝났다(쓰러짐·광기·이탈 등).
        /// 적은 살아 있으면 true(행동 선택은 DecideEnemy).
        /// </summary>
        public bool BeginTurn(IBattleUnit actor, IRng rng, List<BattleEvent> events)
        {
            events.Add(new BattleEvent(BattleEventKind.Turn, actor.Id, actor.Id));
            PartyMember m = actor as PartyMember;
            if (m == null)
            {
                return actor.IsActing;
            }
            m.Guarding = false;
            if (m.Status == UnitStatus.Downed)
            {
                TickDownedCountdown(m, rng, events);
                return false;
            }
            if (m.Status != UnitStatus.Ready)
            {
                events.Add(new BattleEvent(BattleEventKind.Skip, m.Id, m.Id, 0, m.Status.ToString()));
                return false;
            }
            if (m.Poison > 0)
            {
                int dmg = BattleDamage.Poison.Roll(rng);
                m.Poison--;
                events.Add(new BattleEvent(BattleEventKind.PoisonTick, m.Id, m.Id, dmg));
                ApplyDamage(m, dmg, m.Id, rng, events);
                if (m.Status != UnitStatus.Ready)
                {
                    return false;
                }
            }
            if (m.Bout != BoutKind.None)
            {
                RunBout(m, rng, events);
                return false;
            }
            return true;
        }

        private void TickDownedCountdown(PartyMember m, IRng rng, List<BattleEvent> events)
        {
            m.DownedCountdown--;
            events.Add(new BattleEvent(BattleEventKind.Countdown, m.Id, m.Id, m.DownedCountdown));
            if (m.DownedCountdown <= 0)
            {
                MakeFallen(m, rng, events);
            }
        }

        /// <summary>발작 중 자기 차례. 지속 턴을 하나 쓰고, 0 이 되면 해소.</summary>
        private void RunBout(PartyMember m, IRng rng, List<BattleEvent> events)
        {
            switch (m.Bout)
            {
                case BoutKind.Flee:
                    int back = CountOn(Side.Ally) - 1;
                    if (back > m.Rank)
                    {
                        IBattleUnit other = Allies[back].Unit;
                        ExchangeUnits(Allies, m.Rank, back);
                        events.Add(new BattleEvent(BattleEventKind.Swap, m.Id, other.Id, 0, "Flee"));
                    }
                    else
                    {
                        events.Add(new BattleEvent(BattleEventKind.Skip, m.Id, m.Id, 0, "Flee"));
                    }
                    break;
                case BoutKind.Freeze:
                    events.Add(new BattleEvent(BattleEventKind.Skip, m.Id, m.Id, 0, "Freeze"));
                    break;
                case BoutKind.Lash:
                    RunLash(m, rng, events);
                    break;
                case BoutKind.Despair:
                    m.Guarding = true;
                    events.Add(new BattleEvent(BattleEventKind.Guard, m.Id, m.Id, 0, "Despair"));
                    break;
            }
            m.BoutTurns--;
            if (m.BoutTurns <= 0)
            {
                m.Bout = BoutKind.None;
                m.BoutTurns = 0;
                events.Add(new BattleEvent(BattleEventKind.BoutEnd, m.Id, m.Id));
            }
        }

        /// <summary>난동: 무작위 다른 아군 1명을 무술 피해로 치고 그 사람 SAN -5.</summary>
        private void RunLash(PartyMember m, IRng rng, List<BattleEvent> events)
        {
            List<PartyMember> others = new List<PartyMember>();
            for (int i = 0; i < Ranks; i++)
            {
                PartyMember t = Allies[i].Unit as PartyMember;
                if (t != null && t != m)
                {
                    others.Add(t);
                }
            }
            if (others.Count == 0)
            {
                events.Add(new BattleEvent(BattleEventKind.Skip, m.Id, m.Id, 0, "Lash"));
                return;
            }
            PartyMember target = others.Count == 1 ? others[0] : others[rng.Next(0, others.Count)];
            int dmg = BattleDamage.Martial.Roll(rng);
            events.Add(new BattleEvent(BattleEventKind.Hit, m.Id, target.Id, dmg, "Lash"));
            ApplyDamage(target, dmg, m.Id, rng, events);
            if (target.Status != UnitStatus.Fallen)
            {
                LoseSan(target, PartyBattleRules.WitnessSanLoss, rng, events, false);
            }
        }

        // ---------- 행동 해결 ----------

        /// <summary>적 행동 선택: 의도(ChooseIntent) -> 아군 칸(TargetWeights 3종). 대상이 없으면 TargetRank -1(Resolve 가 Invalid).</summary>
        public BattleCommand DecideEnemy(EnemyUnit enemy, IRng rng)
        {
            EnemyIntent intent = BattleEngine.ChooseIntent(enemy.State.Def, rng);
            bool[] occupied = new bool[Ranks];
            for (int i = 0; i < Ranks; i++)
            {
                occupied[i] = Allies[i].Unit != null;
            }
            int rank = PartyBattleRules.PickWeighted(enemy.State.Def.TargetWeightsFor(intent), occupied, rng);
            ActionKind kind = intent == EnemyIntent.SanAttack ? ActionKind.SanAttack
                : (intent == EnemyIntent.PoisonAttack ? ActionKind.PoisonAttack : ActionKind.Melee);
            return new BattleCommand(enemy, kind, rank);
        }

        public BattleEvent[] Resolve(BattleCommand cmd, IRng rng)
        {
            List<BattleEvent> ev = new List<BattleEvent>();
            IBattleUnit actor = cmd.Actor;
            if (actor == null || !ValidTargets(actor, cmd.Kind).Contains(cmd.TargetRank))
            {
                ev.Add(new BattleEvent(BattleEventKind.Invalid, actor == null ? null : actor.Id, null, cmd.TargetRank, cmd.Kind.ToString()));
                return ev.ToArray();
            }
            PartyMember a = actor as PartyMember;
            EnemyUnit e = actor as EnemyUnit;
            switch (cmd.Kind)
            {
                case ActionKind.Melee:
                    if (a != null) { AllyAttack(a, cmd.TargetRank, false, rng, ev); }
                    else { EnemyMelee(e, cmd.TargetRank, rng, ev); }
                    break;
                case ActionKind.Ranged:
                    AllyAttack(a, cmd.TargetRank, true, rng, ev);
                    break;
                case ActionKind.SanAttack:
                    EnemySanAttack(e, cmd.TargetRank, rng, ev);
                    break;
                case ActionKind.PoisonAttack:
                    EnemyPoison(e, cmd.TargetRank, ev);
                    break;
                case ActionKind.Guard:
                    a.Guarding = true;
                    ev.Add(new BattleEvent(BattleEventKind.Guard, a.Id, a.Id));
                    break;
                case ActionKind.FirstAid:
                    FirstAid(a, (PartyMember)Allies[cmd.TargetRank].Unit, rng, ev);
                    break;
                case ActionKind.Calm:
                    Calm(a, (PartyMember)Allies[cmd.TargetRank].Unit, rng, ev);
                    break;
                case ActionKind.Swap:
                    IBattleUnit other = Allies[cmd.TargetRank].Unit;
                    Swap(Side.Ally, a.Rank, cmd.TargetRank);
                    ev.Add(new BattleEvent(BattleEventKind.Swap, a.Id, other.Id));
                    break;
                case ActionKind.Run:
                    Run(a, rng, ev);
                    break;
            }
            return ev.ToArray();
        }

        private int NeighborPenalty(PartyMember m)
        {
            for (int d = -1; d <= 1; d += 2)
            {
                PartyMember n = At(Side.Ally, m.Rank + d) as PartyMember;
                if (n != null && n.Status == UnitStatus.Ready && n.Bout == BoutKind.Despair)
                {
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>아군 공격: 기능 vs 적 방어(근접 = 적 무술, 사격 = 적 회피). 앞 칸 사격은 불리 다이스. 실패는 빗나감(반격 없음).</summary>
        private void AllyAttack(PartyMember a, int rank, bool ranged, IRng rng, List<BattleEvent> ev)
        {
            EnemyUnit t = Enemies[rank].Unit as EnemyUnit;
            int net = NeighborPenalty(a) - (ranged && a.Rank == 0 ? 1 : 0);
            int skill = SkillCheck.ClampTarget(a.GetSkill(PartyBattleRules.AttackSkillKey(a, ranged)));
            SuccessLevel pl = SkillCheck.Roll(rng, skill, PartyBattleRules.ConditionFromNet(net)).Level;
            SuccessLevel el = PartyBattleRules.RollPlain(rng, ranged ? t.State.Evasion : t.State.MartialArts);
            string text = pl + "/" + el;
            if (!BattleEngine.ResolveContest(ContestKind.MartialArts, pl, el))
            {
                ev.Add(new BattleEvent(BattleEventKind.Miss, a.Id, t.Id, 0, text));
                return;
            }
            int dmg = PartyBattleRules.AttackDamage(a, ranged).Roll(rng);
            t.State.TakeDamage(dmg);
            ev.Add(new BattleEvent(BattleEventKind.Hit, a.Id, t.Id, dmg, text));
            if (t.State.IsDead)
            {
                t.State.DeathHandled = true;
                RemoveFromSlot(t);
                ev.Add(new BattleEvent(BattleEventKind.EnemyDown, a.Id, t.Id));
            }
        }

        /// <summary>적 일반 공격: 아군이 회피로 대응(방어 태세 +1, 절망 이웃 -1). 쓰러진·안정 상태는 회피 못 하고 바로 맞는다.</summary>
        private void EnemyMelee(EnemyUnit e, int rank, IRng rng, List<BattleEvent> ev)
        {
            PartyMember t = (PartyMember)Allies[rank].Unit;
            string text = null;
            if (t.Status == UnitStatus.Ready || t.Status == UnitStatus.Broken)
            {
                int net = NeighborPenalty(t) + (t.Guarding ? 1 : 0);
                int skill = SkillCheck.ClampTarget(t.GetSkill(CounterKeys.evasionPoint));
                SuccessLevel pl = SkillCheck.Roll(rng, skill, PartyBattleRules.ConditionFromNet(net)).Level;
                SuccessLevel el = PartyBattleRules.RollPlain(rng, e.State.MartialArts);
                text = pl + "/" + el;
                if (BattleEngine.ResolveContest(ContestKind.Evasion, pl, el))
                {
                    ev.Add(new BattleEvent(BattleEventKind.Evade, t.Id, e.Id, 0, text));
                    return;
                }
            }
            int dmg = e.State.Def.AttackDamage.Roll(rng);
            ev.Add(new BattleEvent(BattleEventKind.Hit, e.Id, t.Id, dmg, text));
            ApplyDamage(t, dmg, e.Id, rng, ev);
        }

        private void EnemyPoison(EnemyUnit e, int rank, List<BattleEvent> ev)
        {
            PartyMember t = (PartyMember)Allies[rank].Unit;
            if (t.Status != UnitStatus.Ready)
            {
                ev.Add(new BattleEvent(BattleEventKind.Miss, e.Id, t.Id, 0, "Poison"));
                return;
            }
            t.Poison = PartyMember.PoisonStacks;
            ev.Add(new BattleEvent(BattleEventKind.PoisonApplied, e.Id, t.Id, t.Poison));
        }

        /// <summary>이성 공격(시선): 대상이 SAN 판정(목표 = 현재 SAN). 실패 시 손실 5 또는 10.</summary>
        private void EnemySanAttack(EnemyUnit e, int rank, IRng rng, List<BattleEvent> ev)
        {
            PartyMember t = (PartyMember)Allies[rank].Unit;
            RollResult r = SkillCheck.Roll(rng, SkillCheck.ClampTarget(t.San), Condition.Nomal);
            if (r.IsSuccess)
            {
                ev.Add(new BattleEvent(BattleEventKind.SanResist, e.Id, t.Id));
                return;
            }
            LoseSan(t, SanCheck.RollLoss(rng), rng, ev, true);
        }

        private void FirstAid(PartyMember a, PartyMember t, IRng rng, List<BattleEvent> ev)
        {
            int skill = SkillCheck.ClampTarget(a.GetSkill(CounterKeys.medicinePoint));
            RollResult r = SkillCheck.Roll(rng, skill, PartyBattleRules.ConditionFromNet(NeighborPenalty(a)));
            if (!r.IsSuccess)
            {
                ev.Add(new BattleEvent(BattleEventKind.Miss, a.Id, t.Id, 0, "FirstAid"));
                return;
            }
            if (t.Status == UnitStatus.Downed)
            {
                t.Status = UnitStatus.Stable;
                t.Hp = PartyBattleRules.StableHp;
                t.DownedCountdown = 0;
                ev.Add(new BattleEvent(BattleEventKind.Stable, a.Id, t.Id, t.Hp));
                return;
            }
            int before = t.Hp;
            t.Hp = Math.Min(t.MaxHp, t.Hp + PartyBattleRules.FirstAidHeal);
            ev.Add(new BattleEvent(BattleEventKind.Heal, a.Id, t.Id, t.Hp - before));
        }

        /// <summary>진정: 심리치료·화술 중 높은 쪽으로 판정. 성공 시 발작 즉시 해소 + SAN +5.</summary>
        private void Calm(PartyMember a, PartyMember t, IRng rng, List<BattleEvent> ev)
        {
            int skill = Math.Max(a.GetSkill(CounterKeys.psychotherapyPoint), a.GetSkill(CounterKeys.rhetoricPoint));
            RollResult r = SkillCheck.Roll(rng, SkillCheck.ClampTarget(skill), PartyBattleRules.ConditionFromNet(NeighborPenalty(a)));
            if (!r.IsSuccess)
            {
                ev.Add(new BattleEvent(BattleEventKind.Miss, a.Id, t.Id, 0, "Calm"));
                return;
            }
            t.Bout = BoutKind.None;
            t.BoutTurns = 0;
            t.San = Math.Min(t.MaxSan, t.San + PartyBattleRules.CalmSanGain);
            ev.Add(new BattleEvent(BattleEventKind.Calm, a.Id, t.Id, PartyBattleRules.CalmSanGain));
        }

        /// <summary>도주(파비안만): 파비안 DEX / 적 수 vs 가장 빠른 적 DEX(옛 RunAway). 성공하면 전투 종료(Fled).</summary>
        private void Run(PartyMember a, IRng rng, List<BattleEvent> ev)
        {
            int count = Math.Max(1, CountOn(Side.Enemy));
            int fastest = 0;
            for (int i = 0; i < Ranks; i++)
            {
                if (Enemies[i].Unit != null && Enemies[i].Unit.Dex > fastest)
                {
                    fastest = Enemies[i].Unit.Dex;
                }
            }
            SuccessLevel pl = SkillCheck.Roll(rng, SkillCheck.ClampTarget(a.Dex / count), Condition.Nomal).Level;
            SuccessLevel el = PartyBattleRules.RollPlain(rng, fastest);
            if (BattleEngine.ResolveContest(ContestKind.RunAway, pl, el))
            {
                _fled = true;
                ev.Add(new BattleEvent(BattleEventKind.RunSuccess, a.Id, a.Id, 0, pl + "/" + el));
            }
            else
            {
                ev.Add(new BattleEvent(BattleEventKind.RunFail, a.Id, a.Id, 0, pl + "/" + el));
            }
        }

        // ---------- 피해·SAN·쓰러짐 ----------

        /// <summary>
        /// 아군 피해. 쓰러진(Downed)·안정(Stable) 상태에서 또 맞으면 즉시 Fallen. HP 0 이 되면 Downed(카운트다운 3),
        /// 단 솔로 파비안(LeaderDownRule 꺼짐)은 상태를 바꾸지 않고 IsOver 가 패배로 본다.
        /// </summary>
        private void ApplyDamage(PartyMember m, int dmg, string sourceId, IRng rng, List<BattleEvent> ev)
        {
            if (m.Status == UnitStatus.Downed || m.Status == UnitStatus.Stable)
            {
                MakeFallen(m, rng, ev);
                return;
            }
            if (m.Status != UnitStatus.Ready && m.Status != UnitStatus.Broken)
            {
                return;
            }
            m.Hp = Math.Max(0, m.Hp - dmg);
            if (m.Hp > 0 || (m.IsLeader && !LeaderDownRule))
            {
                return;
            }
            m.Status = UnitStatus.Downed;
            m.DownedCountdown = PartyMember.DownedCountdownStart;
            m.Bout = BoutKind.None;
            m.BoutTurns = 0;
            m.Poison = 0;
            m.Guarding = false;
            ev.Add(new BattleEvent(BattleEventKind.Down, sourceId, m.Id, m.DownedCountdown));
        }

        /// <summary>전선 이탈. 동료는 칸에서 빠지고(뒤가 앞당겨짐) 남은 아군이 목격 SAN 판정. 파비안은 칸에 남고 IsOver 가 패배로 본다.</summary>
        private void MakeFallen(PartyMember m, IRng rng, List<BattleEvent> ev)
        {
            m.Status = UnitStatus.Fallen;
            m.Hp = 0;
            m.DownedCountdown = 0;
            ev.Add(new BattleEvent(BattleEventKind.Fallen, m.Id, m.Id));
            if (m.IsLeader)
            {
                return;
            }
            RemoveFromSlot(m);
            for (int i = 0; i < Ranks; i++)
            {
                PartyMember w = Allies[i].Unit as PartyMember;
                if (w == null || w.Status != UnitStatus.Ready)
                {
                    continue;
                }
                if (!SkillCheck.Roll(rng, SkillCheck.ClampTarget(w.San), Condition.Nomal).IsSuccess)
                {
                    LoseSan(w, PartyBattleRules.WitnessSanLoss, rng, ev, false);
                }
            }
        }

        /// <summary>
        /// SAN 감소. 0 이 되면 동료는 Broken(행동 불가, 파비안은 IsOver 가 패배로 봄). 한 번에 10 이상이면 발작 판정(RollBout).
        /// 발작은 Ready 동료만, 이미 발작 중이면 새로 걸리지 않는다. 파비안은 LeaderBoutEnabled 일 때만.
        /// </summary>
        private void LoseSan(PartyMember m, int loss, IRng rng, List<BattleEvent> ev, bool allowBout)
        {
            m.San = Math.Max(0, m.San - loss);
            ev.Add(new BattleEvent(BattleEventKind.SanLoss, null, m.Id, loss));
            if (m.Status != UnitStatus.Ready)
            {
                return;
            }
            if (m.San <= 0)
            {
                if (!m.IsLeader)
                {
                    m.Status = UnitStatus.Broken;
                    m.Bout = BoutKind.None;
                    m.BoutTurns = 0;
                    ev.Add(new BattleEvent(BattleEventKind.Broken, m.Id, m.Id));
                }
                return;
            }
            if (!allowBout || m.Bout != BoutKind.None || (m.IsLeader && !LeaderBoutEnabled))
            {
                return;
            }
            BoutKind kind;
            int turns;
            if (PartyBattleRules.RollBout(rng, m.Intl, loss, out kind, out turns))
            {
                m.Bout = kind;
                m.BoutTurns = turns;
                ev.Add(new BattleEvent(BattleEventKind.BoutStart, m.Id, m.Id, turns, kind.ToString()));
            }
        }
    }
}
