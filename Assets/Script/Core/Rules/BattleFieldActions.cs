using System;
using System.Collections.Generic;
using SCoW.Core.State;

namespace SCoW.Core.Rules
{
    /// <summary>BattleField 의 차례 시작·행동 해결(헤드리스). 난수는 전부 인자 rng 로, 이벤트는 호출자 버퍼(events)에 쌓는다.</summary>
    public sealed partial class BattleField
    {
        private EnemyUnit _pendingAttacker;
        private int _pendingRank;

        /// <summary>ManualLeaderDefense 에서 파비안의 대응 선택을 기다리는 중인가. 이때 Resolve 는 Invalid, ResolveResponse 만 받는다.</summary>
        public bool HasPendingResponse => _pendingAttacker != null;

        // ---------- 차례 시작 ----------

        /// <summary>
        /// 차례를 받은 행위자의 시작 처리: 방어 해제, 독 틱, 쓰러짐 카운트다운, 발작 자동 행동.
        /// 반환 true = 지시(Resolve)를 받을 수 있다. false = 이번 차례는 자동으로 끝났다(쓰러짐·광기·이탈 등).
        /// 적은 살아 있으면 true(행동 선택은 DecideEnemy).
        /// </summary>
        public bool BeginTurn(IBattleUnit actor, IRng rng, List<BattleEvent> events)
        {
            events.Add(Make(BattleEventKind.Turn, actor.Id, actor));
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
                events.Add(Make(BattleEventKind.Skip, m.Id, m, (int)m.Status));
                return false;
            }
            if (m.Poison > 0 && !TickPoison(m, rng, events))
            {
                return false;
            }
            if (m.Bout != BoutKind.None)
            {
                RunBout(m, rng, events);
                return false;
            }
            return true;
        }

        /// <summary>독 틱. 이 틱으로 쓰러지면 false.</summary>
        private bool TickPoison(PartyMember m, IRng rng, List<BattleEvent> events)
        {
            int dmg = BattleDamage.Poison.Roll(rng);
            m.Poison--;
            DealDamage(m, dmg, m.Id, rng, events, BattleEventKind.PoisonTick, BattleDetail.None, -1, -1);
            return m.Status == UnitStatus.Ready;
        }

        private void TickDownedCountdown(PartyMember m, IRng rng, List<BattleEvent> events)
        {
            m.DownedCountdown--;
            events.Add(Make(BattleEventKind.Countdown, m.Id, m, m.DownedCountdown));
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
                    RunFlee(m, events);
                    break;
                case BoutKind.Freeze:
                    events.Add(Make(BattleEventKind.Skip, m.Id, m, 0, BattleDetail.Freeze));
                    break;
                case BoutKind.Lash:
                    RunLash(m, rng, events);
                    break;
                case BoutKind.Despair:
                    m.Guarding = true;
                    events.Add(Make(BattleEventKind.Guard, m.Id, m, 0, BattleDetail.Despair));
                    break;
            }
            m.BoutTurns--;
            if (m.BoutTurns <= 0)
            {
                m.Bout = BoutKind.None;
                m.BoutTurns = 0;
                events.Add(Make(BattleEventKind.BoutEnd, m.Id, m));
            }
        }

        /// <summary>도주 발작: 맨 뒤 아군과 자리 교환(이미 맨 뒤면 건너뜀).</summary>
        private void RunFlee(PartyMember m, List<BattleEvent> events)
        {
            int back = CountOn(Side.Ally) - 1;
            if (back <= m.Rank)
            {
                events.Add(Make(BattleEventKind.Skip, m.Id, m, 0, BattleDetail.Flee));
                return;
            }
            IBattleUnit other = Allies[back].Unit;
            ExchangeUnits(Allies, m.Rank, back);
            events.Add(Make(BattleEventKind.Swap, m.Id, other, m.Rank, BattleDetail.Flee));
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
                events.Add(Make(BattleEventKind.Skip, m.Id, m, 0, BattleDetail.Lash));
                return;
            }
            PartyMember target = others.Count == 1 ? others[0] : others[rng.Next(0, others.Count)];
            int dmg = BattleDamage.Martial.Roll(rng);
            DealDamage(target, dmg, m.Id, rng, events, BattleEventKind.Hit, BattleDetail.Lash, -1, -1);
            if (target.Status != UnitStatus.Fallen)
            {
                LoseSan(target, PartyBattleRules.WitnessSanLoss, rng, events, false, m.Id);
            }
        }

        // ---------- 적 행동 선택 ----------

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

        // ---------- 행동 해결 ----------

        /// <summary>행동 하나를 해결해 events 에 쌓는다. 잘못된 지시는 Invalid 한 줄(난수 소비 없음).</summary>
        public void Resolve(BattleCommand cmd, IRng rng, List<BattleEvent> events)
        {
            IBattleUnit actor = cmd.Actor;
            if (HasPendingResponse || actor == null || !IsValidTarget(actor, cmd.Kind, cmd.TargetRank))
            {
                events.Add(new BattleEvent(BattleEventKind.Invalid, actor == null ? null : actor.Id, null, (int)cmd.Kind, BattleDetail.None, -1, -1, cmd.TargetRank));
                return;
            }
            PartyMember a = actor as PartyMember;
            if (a != null)
            {
                ResolveAlly(a, cmd, rng, events);
            }
            else
            {
                ResolveEnemy((EnemyUnit)actor, cmd, rng, events);
            }
        }

        private void ResolveAlly(PartyMember a, BattleCommand cmd, IRng rng, List<BattleEvent> ev)
        {
            switch (cmd.Kind)
            {
                case ActionKind.Melee:
                case ActionKind.Ranged:
                    AllyAttack(a, cmd.TargetRank, cmd.Kind == ActionKind.Ranged, cmd.PreRolled, rng, ev);
                    break;
                case ActionKind.Guard:
                    a.Guarding = true;
                    ev.Add(Make(BattleEventKind.Guard, a.Id, a));
                    break;
                case ActionKind.FirstAid:
                    FirstAid(a, (PartyMember)Allies[cmd.TargetRank].Unit, cmd.PreRolled, rng, ev);
                    break;
                case ActionKind.Calm:
                    Calm(a, (PartyMember)Allies[cmd.TargetRank].Unit, cmd.PreRolled, rng, ev);
                    break;
                case ActionKind.Swap:
                    IBattleUnit other = Allies[cmd.TargetRank].Unit;
                    Swap(Side.Ally, a.Rank, cmd.TargetRank);
                    ev.Add(Make(BattleEventKind.Swap, a.Id, other, a.Rank));
                    break;
                case ActionKind.Run:
                    Run(a, cmd.PreRolled, rng, ev);
                    break;
            }
        }

        private void ResolveEnemy(EnemyUnit e, BattleCommand cmd, IRng rng, List<BattleEvent> ev)
        {
            PartyMember t = (PartyMember)Allies[cmd.TargetRank].Unit;
            switch (cmd.Kind)
            {
                case ActionKind.Melee:
                    if (ManualLeaderDefense && t.IsLeader && t.Status == UnitStatus.Ready)
                    {
                        _pendingAttacker = e;
                        _pendingRank = cmd.TargetRank;
                        ev.Add(Make(BattleEventKind.ResponseNeeded, e.Id, t));
                        return;
                    }
                    EnemyHit(e, t, DefenseChoice.Evasion, null, rng, ev);
                    break;
                case ActionKind.SanAttack:
                    ev.Add(Make(BattleEventKind.ActionStart, e.Id, t, 0, BattleDetail.SanAttack));
                    EnemySanAttack(e, t, rng, ev);
                    break;
                case ActionKind.PoisonAttack:
                    ev.Add(Make(BattleEventKind.ActionStart, e.Id, t, 0, BattleDetail.PoisonAttack));
                    EnemyPoison(e, t, ev);
                    break;
            }
        }

        /// <summary>
        /// 파비안 대응(회피·반격) 적용. View 가 룰렛을 돌린 단계를 preRolled 로 넘기면 굴리지 않는다.
        /// 대기 중이 아니면 Invalid. 반격은 성공 시 파비안이 적을 치고, 실패하면 맞는다(옛 반격 규칙).
        /// </summary>
        public void ResolveResponse(DefenseChoice choice, SuccessLevel? preRolled, IRng rng, List<BattleEvent> events)
        {
            if (_pendingAttacker == null)
            {
                events.Add(new BattleEvent(BattleEventKind.Invalid, null, null));
                return;
            }
            EnemyUnit e = _pendingAttacker;
            PartyMember t = (PartyMember)Allies[_pendingRank].Unit;
            _pendingAttacker = null;
            EnemyHit(e, t, choice, preRolled, rng, events);
        }

        /// <summary>
        /// 적 일반 공격 적용: 방어자가 회피(방어 태세 +1, 절망 이웃 -1) 또는 반격으로 대응. 쓰러진·안정 상태는 대응 못 하고 바로 맞는다.
        /// 이벤트의 판정 단계는 그 이벤트의 행위자/대상 기준(Hit 은 적 = ActorLevel, 아군 = TargetLevel).
        /// </summary>
        private void EnemyHit(EnemyUnit e, PartyMember t, DefenseChoice choice, SuccessLevel? pre, IRng rng, List<BattleEvent> ev)
        {
            int pl = -1;
            int el = -1;
            if (t.Status == UnitStatus.Ready || t.Status == UnitStatus.Broken)
            {
                bool counter = choice == DefenseChoice.Counter;
                int net = NeighborPenalty(t) + (!counter && t.Guarding ? 1 : 0);
                string key = counter ? PartyBattleRules.AttackSkillKey(t, false) : CounterKeys.evasionPoint;
                SuccessLevel p = pre ?? SkillCheck.Roll(rng, SkillCheck.ClampTarget(t.GetSkill(key)), PartyBattleRules.ConditionFromNet(net)).Level;
                SuccessLevel q = PartyBattleRules.RollPlain(rng, e.State.MartialArts);
                pl = (int)p;
                el = (int)q;
                ContestKind kind = counter ? ContestKind.CounterAttack : ContestKind.Evasion;
                if (BattleEngine.ResolveContest(kind, p, q))
                {
                    if (counter)
                    {
                        DamageEnemy(t, e, PartyBattleRules.AttackDamage(t, false).Roll(rng), pl, el, BattleDetail.Counter, ev);
                    }
                    else
                    {
                        ev.Add(Make(BattleEventKind.Evade, t.Id, e, 0, BattleDetail.None, pl, el));
                    }
                    return;
                }
            }
            int dmg = e.State.Def.AttackDamage.Roll(rng);
            DealDamage(t, dmg, e.Id, rng, ev, BattleEventKind.Hit, BattleDetail.None, el, pl);
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

        /// <summary>행위자 자신의 판정 단계. pre 가 있으면 굴리지 않는다(파비안 룰렛 주입).</summary>
        private static SuccessLevel Level(SuccessLevel? pre, IRng rng, int skill, int net)
        {
            return pre ?? SkillCheck.Roll(rng, SkillCheck.ClampTarget(skill), PartyBattleRules.ConditionFromNet(net)).Level;
        }

        /// <summary>아군 공격: 기능 vs 적 방어(근접 = 적 무술, 사격 = 적 회피). 앞 칸 사격은 불리 다이스. 실패는 빗나감(반격 없음).</summary>
        private void AllyAttack(PartyMember a, int rank, bool ranged, SuccessLevel? pre, IRng rng, List<BattleEvent> ev)
        {
            EnemyUnit t = (EnemyUnit)Enemies[rank].Unit;
            int net = NeighborPenalty(a) - (ranged && a.Rank == 0 ? 1 : 0);
            SuccessLevel pl = Level(pre, rng, a.GetSkill(PartyBattleRules.AttackSkillKey(a, ranged)), net);
            SuccessLevel el = PartyBattleRules.RollPlain(rng, ranged ? t.State.Evasion : t.State.MartialArts);
            if (!BattleEngine.ResolveContest(ContestKind.MartialArts, pl, el))
            {
                ev.Add(Make(BattleEventKind.Miss, a.Id, t, 0, BattleDetail.None, (int)pl, (int)el));
                return;
            }
            DamageEnemy(a, t, PartyBattleRules.AttackDamage(a, ranged).Roll(rng), (int)pl, (int)el, BattleDetail.None, ev);
        }

        /// <summary>적에게 피해. 죽으면 칸에서 빠지고(압축 이벤트) EnemyDown.</summary>
        private void DamageEnemy(PartyMember by, EnemyUnit t, int dmg, int byLevel, int targetLevel, BattleDetail detail, List<BattleEvent> ev)
        {
            t.State.TakeDamage(dmg);
            ev.Add(Make(BattleEventKind.Hit, by.Id, t, dmg, detail, byLevel, targetLevel));
            if (t.State.IsDead)
            {
                t.State.DeathHandled = true;
                ev.Add(Make(BattleEventKind.EnemyDown, by.Id, t));
                RemoveFromSlot(t, ev);
            }
        }

        private void EnemyPoison(EnemyUnit e, PartyMember t, List<BattleEvent> ev)
        {
            if (t.Status != UnitStatus.Ready)
            {
                ev.Add(Make(BattleEventKind.Miss, e.Id, t, 0, BattleDetail.Poison));
                return;
            }
            t.Poison = PartyMember.PoisonStacks;
            ev.Add(Make(BattleEventKind.PoisonApplied, e.Id, t, t.Poison));
        }

        /// <summary>이성 공격(시선): 대상이 SAN 판정(목표 = 현재 SAN). 실패 시 손실 5 또는 10.</summary>
        private void EnemySanAttack(EnemyUnit e, PartyMember t, IRng rng, List<BattleEvent> ev)
        {
            RollResult r = SkillCheck.Roll(rng, SkillCheck.ClampTarget(t.San), Condition.Nomal);
            if (r.IsSuccess)
            {
                ev.Add(Make(BattleEventKind.SanResist, e.Id, t, 0, BattleDetail.None, -1, (int)r.Level));
                return;
            }
            LoseSan(t, SanCheck.RollLoss(rng), rng, ev, true, e.Id);
        }

        private void FirstAid(PartyMember a, PartyMember t, SuccessLevel? pre, IRng rng, List<BattleEvent> ev)
        {
            SuccessLevel lv = Level(pre, rng, a.GetSkill(CounterKeys.medicinePoint), NeighborPenalty(a));
            if (!PartyBattleRules.IsSuccess(lv))
            {
                ev.Add(Make(BattleEventKind.Miss, a.Id, t, 0, BattleDetail.FirstAid, (int)lv));
                return;
            }
            if (t.Status == UnitStatus.Downed)
            {
                t.Status = UnitStatus.Stable;
                t.Hp = PartyBattleRules.StableHp;
                t.DownedCountdown = 0;
                ev.Add(Make(BattleEventKind.Stable, a.Id, t, t.Hp, BattleDetail.FirstAid, (int)lv));
                return;
            }
            int before = t.Hp;
            t.Hp = Math.Min(t.MaxHp, t.Hp + PartyBattleRules.FirstAidHeal);
            ev.Add(Make(BattleEventKind.Heal, a.Id, t, t.Hp - before, BattleDetail.FirstAid, (int)lv));
        }

        /// <summary>진정: 심리치료·화술 중 높은 쪽으로 판정. 성공 시 발작 즉시 해소 + SAN +5.</summary>
        private void Calm(PartyMember a, PartyMember t, SuccessLevel? pre, IRng rng, List<BattleEvent> ev)
        {
            int skill = Math.Max(a.GetSkill(CounterKeys.psychotherapyPoint), a.GetSkill(CounterKeys.rhetoricPoint));
            SuccessLevel lv = Level(pre, rng, skill, NeighborPenalty(a));
            if (!PartyBattleRules.IsSuccess(lv))
            {
                ev.Add(Make(BattleEventKind.Miss, a.Id, t, 0, BattleDetail.Calm, (int)lv));
                return;
            }
            t.Bout = BoutKind.None;
            t.BoutTurns = 0;
            t.San = Math.Min(t.MaxSan, t.San + PartyBattleRules.CalmSanGain);
            ev.Add(Make(BattleEventKind.Calm, a.Id, t, PartyBattleRules.CalmSanGain, BattleDetail.None, (int)lv));
        }

        /// <summary>
        /// 도주(파비안만): 파비안 DEX / 적 수 vs 가장 빠른 적 DEX(옛 RunAway). 정수 나눗셈은 옛 식 그대로(적이 많을수록 목표치가 내림).
        /// 성공하면 전투 종료(Fled).
        /// </summary>
        private void Run(PartyMember a, SuccessLevel? pre, IRng rng, List<BattleEvent> ev)
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
            SuccessLevel pl = Level(pre, rng, a.Dex / count, 0);
            SuccessLevel el = PartyBattleRules.RollPlain(rng, fastest);
            bool ok = BattleEngine.ResolveContest(ContestKind.RunAway, pl, el);
            if (ok)
            {
                _forcedOutcome = BattleOutcome.Fled;
            }
            ev.Add(Make(ok ? BattleEventKind.RunSuccess : BattleEventKind.RunFail, a.Id, a, 0, BattleDetail.None, (int)pl, (int)el));
        }

        // ---------- 피해·SAN·쓰러짐 ----------

        /// <summary>
        /// 아군 피해와 그 결과를 순서대로 기록한다: 피해 이벤트(hitKind) -> 쓰러짐(Down) 또는 이탈(Fallen).
        /// 쓰러진(Downed)·안정(Stable) 상태에서 또 맞으면 즉시 Fallen. HP 0 이 되면 Downed(카운트다운 3),
        /// 단 솔로 파비안(LeaderDownRule 꺼짐)은 상태를 바꾸지 않고 IsOver 가 패배로 본다.
        /// </summary>
        private void DealDamage(PartyMember m, int dmg, string sourceId, IRng rng, List<BattleEvent> ev,
            BattleEventKind hitKind, BattleDetail detail, int actorLevel, int targetLevel)
        {
            bool alreadyDown = m.Status == UnitStatus.Downed || m.Status == UnitStatus.Stable;
            bool canBleed = m.Status == UnitStatus.Ready || m.Status == UnitStatus.Broken;
            if (canBleed)
            {
                m.Hp = Math.Max(0, m.Hp - dmg);
            }
            ev.Add(Make(hitKind, sourceId, m, dmg, detail, actorLevel, targetLevel));
            if (alreadyDown)
            {
                MakeFallen(m, rng, ev);
            }
            else if (canBleed && m.Hp <= 0 && !(m.IsLeader && !LeaderDownRule))
            {
                m.Status = UnitStatus.Downed;
                m.DownedCountdown = PartyMember.DownedCountdownStart;
                m.Bout = BoutKind.None;
                m.BoutTurns = 0;
                m.Poison = 0;
                m.Guarding = false;
                ev.Add(Make(BattleEventKind.Down, sourceId, m, m.DownedCountdown));
            }
        }

        /// <summary>전선 이탈. 동료는 칸에서 빠지고(뒤가 앞당겨짐) 남은 아군이 목격 SAN 판정. 파비안은 칸에 남고 IsOver 가 패배로 본다.</summary>
        private void MakeFallen(PartyMember m, IRng rng, List<BattleEvent> ev)
        {
            m.Status = UnitStatus.Fallen;
            m.Hp = 0;
            m.DownedCountdown = 0;
            ev.Add(Make(BattleEventKind.Fallen, m.Id, m));
            if (m.IsLeader)
            {
                return;
            }
            RemoveFromSlot(m, ev);
            for (int i = 0; i < Ranks; i++)
            {
                PartyMember w = Allies[i].Unit as PartyMember;
                if (w == null || w.Status != UnitStatus.Ready)
                {
                    continue;
                }
                if (!SkillCheck.Roll(rng, SkillCheck.ClampTarget(w.San), Condition.Nomal).IsSuccess)
                {
                    LoseSan(w, PartyBattleRules.WitnessSanLoss, rng, ev, false, m.Id);
                }
            }
        }

        /// <summary>
        /// SAN 감소(sourceId = 원인). 0 이 되면 동료는 Broken(행동 불가, 파비안은 IsOver 가 패배로 봄). 한 번에 10 이상이면 발작 판정.
        /// 발작은 Ready 동료만, 이미 발작 중이면 새로 걸리지 않는다. 파비안은 LeaderBoutEnabled 일 때만.
        /// </summary>
        private void LoseSan(PartyMember m, int loss, IRng rng, List<BattleEvent> ev, bool allowBout, string sourceId)
        {
            m.San = Math.Max(0, m.San - loss);
            ev.Add(Make(BattleEventKind.SanLoss, sourceId, m, loss));
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
                    ev.Add(Make(BattleEventKind.Broken, m.Id, m));
                }
                return;
            }
            if (allowBout && m.Bout == BoutKind.None && (!m.IsLeader || LeaderBoutEnabled))
            {
                TryStartBout(m, loss, rng, ev);
            }
        }

        private static BattleDetail BoutDetail(BoutKind kind)
        {
            switch (kind)
            {
                case BoutKind.Flee: return BattleDetail.Flee;
                case BoutKind.Freeze: return BattleDetail.Freeze;
                case BoutKind.Lash: return BattleDetail.Lash;
                case BoutKind.Despair: return BattleDetail.Despair;
                default: return BattleDetail.None;
            }
        }

        private static void TryStartBout(PartyMember m, int loss, IRng rng, List<BattleEvent> ev)
        {
            BoutKind kind;
            int turns;
            if (PartyBattleRules.RollBout(rng, m.Intl, loss, out kind, out turns))
            {
                m.Bout = kind;
                m.BoutTurns = turns;
                ev.Add(Make(BattleEventKind.BoutStart, m.Id, m, turns, BoutDetail(kind)));
            }
        }
    }
}
