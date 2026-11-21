using System.Collections.Generic;

namespace SCoW.Core.Rules
{
    public enum Side { Ally, Enemy }

    /// <summary>전투 광기(발작) 종류. 정수 1~4 는 1d4 굴림과 같다.</summary>
    public enum BoutKind { None = 0, Flee = 1, Freeze = 2, Lash = 3, Despair = 4 }

    /// <summary>전투 행동. 아군은 앞 7개, 적은 Melee/SanAttack/PoisonAttack. Throw·Special·Reload·Item 은 이후 단계(PT-5/7).</summary>
    public enum ActionKind { Melee, Ranged, Guard, FirstAid, Calm, Swap, Run, SanAttack, PoisonAttack }

    public enum BattleOutcome { None, Win, Loss, Fled }

    public enum BattleEventKind
    {
        Turn, Skip, Hit, Miss, Evade, SanResist, SanLoss, BoutStart, BoutEnd, Down, Countdown, Stable, Fallen,
        EnemyDown, Heal, Calm, Swap, Guard, PoisonApplied, PoisonTick, Broken, RunSuccess, RunFail, Recovering, Died, Invalid
    }

    /// <summary>해결 결과 한 줄. View 재생(PT-5)과 골든 로그가 읽는다.</summary>
    public readonly struct BattleEvent
    {
        public readonly BattleEventKind Kind;
        public readonly string ActorId;
        public readonly string TargetId;
        public readonly int Value;
        public readonly string Text;

        public BattleEvent(BattleEventKind kind, string actorId, string targetId, int value = 0, string text = null)
        {
            Kind = kind;
            ActorId = actorId;
            TargetId = targetId;
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Kind + " " + ActorId + ">" + TargetId + " " + Value + (string.IsNullOrEmpty(Text) ? string.Empty : " " + Text);
        }
    }

    /// <summary>행동 지시. 대상 쪽은 행동 종류로 정해진다(공격류 = 반대편, 그 외 = 자기편). 대상 없는 행동은 자기 칸을 넣는다.</summary>
    public readonly struct BattleCommand
    {
        public readonly IBattleUnit Actor;
        public readonly ActionKind Kind;
        public readonly int TargetRank;

        public BattleCommand(IBattleUnit actor, ActionKind kind, int targetRank)
        {
            Actor = actor;
            Kind = kind;
            TargetRank = targetRank;
        }
    }

    /// <summary>파비안·동료·적 공통 행위자(Party.md 4-8절).</summary>
    public interface IBattleUnit
    {
        string Id { get; }
        Side Side { get; }
        int Dex { get; }
        int Hp { get; }
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
        public UnitStatus Status => State.IsDead ? UnitStatus.Fallen : UnitStatus.Ready;
        public bool IsActing => !State.IsDead;
        public bool HasTurns => !State.IsDead;
        public float Gauge { get; set; }
        public int Rank { get; set; }
    }
}
