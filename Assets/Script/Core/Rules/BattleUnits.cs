using System.Collections.Generic;

namespace SCoW.Core.Rules
{
    public enum Side { Ally, Enemy }

    /// <summary>전투 광기(발작) 종류. 정수 1~4 는 1d4 굴림과 같다.</summary>
    public enum BoutKind { None = 0, Flee = 1, Freeze = 2, Lash = 3, Despair = 4 }

    /// <summary>
    /// 전투 행동. 아군은 앞 7개, 적은 Melee/SanAttack/PoisonAttack.
    /// Throw·Special·Reload·Item 은 자리만 확정한 것으로 PT-5 에서 구현(지금 Resolve 는 Invalid).
    /// </summary>
    public enum ActionKind { Melee, Ranged, Guard, FirstAid, Calm, Swap, Run, SanAttack, PoisonAttack, Throw, Special, Reload, Item }

    public enum BattleOutcome { None, Win, Loss, Fled }

    /// <summary>파비안이 피격당했을 때 고르는 대응(View 가 룰렛을 돌린 뒤 ResolveResponse 로 넘긴다).</summary>
    public enum DefenseChoice { Evasion, Counter }

    public enum BattleEventKind
    {
        Turn, Skip, Hit, Miss, Evade, SanResist, SanLoss, BoutStart, BoutEnd, Down, Countdown, Stable, Fallen,
        EnemyDown, Heal, Calm, Swap, Guard, PoisonApplied, PoisonTick, Broken, RunSuccess, RunFail, Recovering, Died,
        Invalid,
        /// <summary>슬롯 압축으로 칸이 바뀐 유닛(Value = 옛 칸, Rank = 새 칸).</summary>
        Compact,
        /// <summary>Broken 동료가 전투 종료 후 파티를 떠난다(Settle).</summary>
        Departed,
        /// <summary>적의 이성·독 공격 시작(연출 시작점).</summary>
        ActionStart,
        /// <summary>파비안 대응 선택 대기. 이후 ResolveResponse.</summary>
        ResponseNeeded
    }

    /// <summary>BattleEvent 의 부가 설명(문자열 대신).</summary>
    public enum BattleDetail { None, Lash, Despair, Flee, Freeze, FirstAid, Calm, Poison, SanAttack, PoisonAttack, Counter }

    /// <summary>
    /// 해결 결과 한 줄(View 재생·골든 로그). Rank/Hp/San 은 대상의 변화 후 값(해당 없으면 -1),
    /// ActorLevel/TargetLevel 은 판정 단계(SuccessLevel 정수, 없으면 -1).
    /// </summary>
    public readonly struct BattleEvent
    {
        public readonly BattleEventKind Kind;
        public readonly string ActorId;
        public readonly string TargetId;
        public readonly int Value;
        public readonly BattleDetail Detail;
        public readonly int ActorLevel;
        public readonly int TargetLevel;
        public readonly int Rank;
        public readonly int Hp;
        public readonly int San;

        public BattleEvent(BattleEventKind kind, string actorId, string targetId, int value = 0, BattleDetail detail = BattleDetail.None,
            int actorLevel = -1, int targetLevel = -1, int rank = -1, int hp = -1, int san = -1)
        {
            Kind = kind;
            ActorId = actorId;
            TargetId = targetId;
            Value = value;
            Detail = detail;
            ActorLevel = actorLevel;
            TargetLevel = targetLevel;
            Rank = rank;
            Hp = hp;
            San = san;
        }

        public override string ToString()
        {
            string levels = ActorLevel >= 0 ? " " + (SuccessLevel)ActorLevel + "/" + (TargetLevel >= 0 ? ((SuccessLevel)TargetLevel).ToString() : "-") : string.Empty;
            return Kind + " " + ActorId + ">" + TargetId + " " + Value + (Detail == BattleDetail.None ? string.Empty : " " + Detail) + levels
                + " r" + Rank + " hp" + Hp + " san" + San;
        }
    }

    /// <summary>
    /// 행동 지시. TargetSide 는 행동 종류로 정해진다(공격류 = 반대편, 그 외 = 자기편). 대상 없는 행동은 자기 칸을 넣는다.
    /// PreRolled 가 있으면 행위자 자신의 판정은 굴리지 않고 이 단계를 쓴다(파비안 룰렛 결과 주입).
    /// </summary>
    public readonly struct BattleCommand
    {
        public readonly IBattleUnit Actor;
        public readonly ActionKind Kind;
        public readonly int TargetRank;
        public readonly Side TargetSide;
        public readonly string Special;
        public readonly SuccessLevel? PreRolled;

        public BattleCommand(IBattleUnit actor, ActionKind kind, int targetRank, SuccessLevel? preRolled = null, string special = null)
        {
            Actor = actor;
            Kind = kind;
            TargetRank = targetRank;
            Special = special;
            PreRolled = preRolled;
            TargetSide = PartyBattleRules.TargetSideOf(actor == null ? Side.Ally : actor.Side, kind);
        }
    }

    /// <summary>파비안·동료·적 공통 행위자(Party.md 4-8절).</summary>
    public interface IBattleUnit
    {
        string Id { get; }
        Side Side { get; }
        int Dex { get; }
        int Hp { get; }
        int San { get; }
        UnitStatus Status { get; }
        /// <summary>자기 차례에 행동할 수 있다(Ready, 살아 있는 적).</summary>
        bool IsActing { get; }
        /// <summary>게이지가 도는가. Ready 와 Downed(카운트다운이 자기 차례 도달로 줄어든다), 살아 있는 적.</summary>
        bool HasTurns { get; }
        float Gauge { get; set; }
        int Rank { get; set; }
    }

    public sealed class BattleSlot
    {
        public readonly Side Side;
        public readonly int Rank;
        public IBattleUnit Unit;

        public BattleSlot(Side side, int rank)
        {
            Side = side;
            Rank = rank;
        }
    }

    /// <summary>EnemyState 를 IBattleUnit 으로 감싼 어댑터. 같은 Def 의 적이 여럿이면 id 로 구분한다.</summary>
    public sealed class EnemyUnit : IBattleUnit
    {
        public readonly EnemyState State;
        private readonly string _id;

        public EnemyUnit(EnemyState state, string id = null)
        {
            State = state;
            _id = id ?? state.Def.Id;
            Gauge = BattleEngine.GaugeReset;
        }

        public string Id => _id;
        public Side Side => Side.Enemy;
        public int Dex => State.Dex;
        public int Hp => State.NowHp;
        public int San => State.San;
        public UnitStatus Status => State.IsDead ? UnitStatus.Fallen : UnitStatus.Ready;
        public bool IsActing => !State.IsDead;
        public bool HasTurns => !State.IsDead;
        public float Gauge { get; set; }
        public int Rank { get; set; }
    }
}
