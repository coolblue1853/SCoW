using System;
using System.Collections;
using System.Collections.Generic;
using SCoW.Core.Rules;
using SCoW.Core.State;
using UnityEngine;

/// <summary>
/// 다인원 전투 진행(PT-5, View). BattleField(Core)를 돌린다: 차례 -> BeginTurn -> 행동 선택(메뉴·대상) -> 해결 -> 이벤트 재생.
/// 파비안의 판정은 기존 룰렛(Rollet.SetRollet, 주제 "PT_Fabian")을 거쳐 PreRolled 로 주입하고, 동료·적은 Core 가 자동으로 굴린다.
/// 동료가 없으면(명단 1명) 시작하지 않는다: 솔로 전투는 옛 BattleManager 경로 그대로다.
/// </summary>
public sealed class PartyBattleDirector : MonoBehaviour
{
    public static PartyBattleDirector Instance { get; private set; }

    public static bool Running
    {
        get { return Instance != null && Instance._running; }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        Instance = null;
    }

    private bool _running;
    private bool _waitingRoll;
    private bool _rollDone;
    private SuccessLevel _rollLevel;
    private BattleField _field;
    private PartyBattleView _view;
    private BattleEventPlayer _player;
    private List<PartyMember> _roster;
    private List<int> _enemyIndices;
    private BattleManager _manager;
    private Action<BattleOutcome> _onEnd;
    private readonly List<BattleEvent> _events = new List<BattleEvent>();
    private readonly Dictionary<string, int> _enemyIndexById = new Dictionary<string, int>();

    /// <summary>플레이 확인용 한 줄 요약.</summary>
    public string DebugState
    {
        get { return "running=" + _running + " rolling=" + _waitingRoll + " view={" + (_view == null ? "-" : _view.DebugSummary() + " " + _view.DebugMode) + "}"; }
    }

    public BattleField Field
    {
        get { return _field; }
    }

    public PartyBattleView View
    {
        get { return _view; }
    }

    /// <summary>룰렛이 끝났을 때 Rollet.EndRollet 이 부른다(주제 PT_Fabian). 기다리는 전투가 없으면 무시한다.</summary>
    public static void OnLeaderRoll(SuccessLevel level)
    {
        if (Instance != null && Instance._waitingRoll)
        {
            Instance._rollLevel = level;
            Instance._rollDone = true;
        }
    }

    /// <summary>
    /// 옛 전투가 막 시작될 때 부른다. 명단(파비안 + 합류한 동료)이 2명 이상이면 View 가 전투를 넘겨받고 true.
    /// 솔로이거나 이미 돌고 있으면 false(돌고 있을 때는 Running 으로 먼저 거른다).
    /// </summary>
    public static bool TryStart(BattleManager manager)
    {
        if (Running || GameRoot.Instance == null)
        {
            return false;
        }
        GameState s = GameRoot.Instance.State;
        List<PartyMember> roster = PartyBattleBridge.BuildRoster(s, BuildLeader(s), DataBaseManager.timeCount);
        if (roster.Count < 2)
        {
            return false;
        }
        List<int> indices = new List<int>();
        List<EnemyState> states = manager.PartyEnemies(indices);
        if (states.Count == 0)
        {
            return false;
        }
        List<EnemyUnit> enemies = new List<EnemyUnit>();
        for (int i = 0; i < states.Count; i++)
        {
            enemies.Add(new EnemyUnit(states[i]));
        }
        if (Instance == null)
        {
            GameObject go = new GameObject("PartyBattleDirector");
            DontDestroyOnLoad(go);
            Instance = go.AddComponent<PartyBattleDirector>();
        }
        Instance.Begin(roster, enemies, indices, manager, null);
        return true;
    }

    /// <summary>명단·적으로 전투를 시작한다(플레이 확인·테스트용 직접 입구). onEnd 는 정산 뒤 한 번.</summary>
    public void Begin(List<PartyMember> roster, List<EnemyUnit> enemies, List<int> enemyIndices, BattleManager manager, Action<BattleOutcome> onEnd)
    {
        if (_running)
        {
            return;
        }
        _roster = roster;
        _enemyIndices = enemyIndices ?? new List<int>();
        _manager = manager;
        _onEnd = onEnd;
        _field = new BattleField(roster, enemies, new BattleOptions { ManualLeaderDefense = true });
        _enemyIndexById.Clear();
        for (int i = 0; i < enemies.Count && i < _enemyIndices.Count; i++)
        {
            _enemyIndexById[enemies[i].Id] = _enemyIndices[i];
        }
        if (_view == null)
        {
            _view = PartyBattleView.Create();
        }
        _player = new BattleEventPlayer(_view);
        _view.Setup(_field, SpriteOf);
        _running = true;
        StartCoroutine(Run());
    }

    private static PartyMember BuildLeader(GameState s)
    {
        Dictionary<string, int> skills = new Dictionary<string, int>();
        for (int i = 0; i < CompanionDefs.SkillKeys.Length; i++)
        {
            skills[CompanionDefs.SkillKeys[i]] = s.GetCounter(CompanionDefs.SkillKeys[i]);
        }
        List<string> weapons = new List<string>();
        AddIf(weapons, s, CounterKeys.dagger, "Dagger");
        AddIf(weapons, s, CounterKeys.bat, "Bat");
        AddIf(weapons, s, CounterKeys.axe, "Axe");
        AddIf(weapons, s, CounterKeys.nowSmallPistol, "SmallPistol");
        AddIf(weapons, s, CounterKeys.nowRevolver, "Revolver");
        AddIf(weapons, s, CounterKeys.nowRifle, "Rifle");
        AddIf(weapons, s, CounterKeys.nowShotgun, "Shotgun");
        int maxHp = Mathf.Max(1, s.GetCounter(CounterKeys.hp));
        int maxSan = Mathf.Max(1, s.GetCounter(CounterKeys.san));
        return PartyMember.CreateLeader(CompanionDefs.LeaderId, s.GetCounter(CounterKeys.dex), s.GetCounter(CounterKeys.intl), maxHp,
            Mathf.Clamp(s.GetCounter(CounterKeys.nowHP), 0, maxHp), maxSan, Mathf.Clamp(s.GetCounter(CounterKeys.nowSan), 0, maxSan), skills, weapons);
    }

    private static void AddIf(List<string> list, GameState s, string key, string weapon)
    {
        if (s.GetCounter(key) > 0)
        {
            list.Add(weapon);
        }
    }

    /// <summary>기존 스프라이트 재사용: 파비안 = 전투 맵 플레이어, 적 = 전투 맵 적 오브젝트. 동료는 임시 사각형.</summary>
    private Sprite SpriteOf(IBattleUnit u)
    {
        if (_manager == null)
        {
            return null;
        }
        PartyMember m = u as PartyMember;
        if (m != null)
        {
            return m.IsLeader ? _manager.PlayerSprite() : null;
        }
        int index;
        return _enemyIndexById.TryGetValue(u.Id, out index) ? _manager.EnemySprite(index) : null;
    }

    // ---------- 진행 ----------

    private IEnumerator Run()
    {
        BattleOutcome outcome;
        int guard = 0;
        while (!_field.IsOver(out outcome) && guard++ < 500)
        {
            IBattleUnit actor = _field.NextActor();
            if (actor == null)
            {
                break;
            }
            _events.Clear();
            bool acts = _field.BeginTurn(actor, UnityRng.Instance, _events);
            yield return _player.Play(_events);
            if (!acts || _field.IsOver(out outcome))
            {
                continue;
            }
            EnemyUnit enemy = actor as EnemyUnit;
            yield return enemy != null ? EnemyTurn(enemy) : AllyTurn((PartyMember)actor);
        }
        _field.IsOver(out outcome);
        if (outcome == BattleOutcome.None)
        {
            Debug.LogWarning("PartyBattleDirector: 전투가 끝나지 않은 채 반복 한도에 닿아 도주로 정산한다.");
            outcome = BattleOutcome.Fled;
        }
        _events.Clear();
        _field.Settle(_events);
        yield return _player.Play(_events);
        yield return new WaitForSeconds(0.6f);
        Finish(outcome);
    }

    private IEnumerator EnemyTurn(EnemyUnit enemy)
    {
        _events.Clear();
        BattleCommand cmd = _field.DecideEnemy(enemy, UnityRng.Instance);
        _field.Resolve(cmd, UnityRng.Instance, _events);
        yield return _player.Play(_events);
        while (_field.HasPendingResponse)
        {
            PartyMember leader = _field.Leader;
            ViewPick pick = new ViewPick();
            List<ActionOption> options = new List<ActionOption>
            {
                new ActionOption { Label = "Dodge", Tip = "Evasion check. Guard gives a bonus.", Enabled = true },
                new ActionOption { Label = "Counter", Tip = "Strike back. Success hits the foe, failure takes the blow.", Enabled = true },
            };
            _view.OpenMenu(leader, options, pick);
            yield return new WaitUntil(() => pick.Done);
            DefenseChoice choice = pick.Index == 1 ? DefenseChoice.Counter : DefenseChoice.Evasion;
            string key = choice == DefenseChoice.Counter ? PartyBattleRules.AttackSkillKey(leader, false) : CounterKeys.evasionPoint;
            SuccessLevel level = SuccessLevel.Failure;
            yield return RollLeader(choice == DefenseChoice.Counter ? "Fabian : Counter" : "Fabian : Dodge", key, leader.GetSkill(key),
                Condition.Nomal, l => level = l);
            _events.Clear();
            _field.ResolveResponse(choice, level, UnityRng.Instance, _events);
            yield return _player.Play(_events);
        }
    }

    private IEnumerator AllyTurn(PartyMember actor)
    {
        _view.SetActiveUnit(actor.Id);
        while (true)
        {
            List<ActionOption> options = BuildOptions(actor);
            ViewPick menu = new ViewPick();
            _view.OpenMenu(actor, options, menu);
            yield return new WaitUntil(() => menu.Done);
            ActionOption opt = options[menu.Index];
            int rank = actor.Rank;
            if (menu.QuickRank >= 0)
            {
                rank = menu.QuickRank;
            }
            else if (!opt.NoTarget)
            {
                ViewPick target = new ViewPick();
                _view.OpenTargets(actor, opt.Kind, PartyBattleRules.TargetSideOf(Side.Ally, opt.Kind), _field.ValidTargets(actor, opt.Kind), target);
                yield return new WaitUntil(() => target.Done);
                if (target.Cancelled)
                {
                    continue;
                }
                rank = target.Rank;
            }
            SuccessLevel? pre = null;
            if (actor.IsLeader && NeedsRoll(opt.Kind))
            {
                SuccessLevel level = SuccessLevel.Failure;
                string key = SkillKeyOf(actor, opt.Kind);
                Condition cond = opt.Kind == ActionKind.Melee || opt.Kind == ActionKind.Ranged ? _field.PreviewAttack(actor, opt.Kind, rank).Condition : Condition.Nomal;
                yield return RollLeader("Fabian : " + opt.Label, key, opt.Kind == ActionKind.Run ? actor.Dex / Mathf.Max(1, _field.CountOn(Side.Enemy)) : actor.GetSkill(key),
                    cond, l => level = l);
                pre = level;
                if (opt.Kind == ActionKind.Ranged)
                {
                    SpendAmmo(actor);
                }
            }
            _events.Clear();
            _field.Resolve(new BattleCommand(actor, opt.Kind, rank, pre), UnityRng.Instance, _events);
            yield return _player.Play(_events);
            yield break;
        }
    }

    private static bool NeedsRoll(ActionKind kind)
    {
        return kind == ActionKind.Melee || kind == ActionKind.Ranged || kind == ActionKind.FirstAid || kind == ActionKind.Calm || kind == ActionKind.Run;
    }

    private static string SkillKeyOf(PartyMember actor, ActionKind kind)
    {
        switch (kind)
        {
            case ActionKind.Melee: return PartyBattleRules.AttackSkillKey(actor, false);
            case ActionKind.Ranged: return PartyBattleRules.AttackSkillKey(actor, true);
            case ActionKind.FirstAid: return CounterKeys.medicinePoint;
            case ActionKind.Calm: return CounterKeys.psychotherapyPoint;
            default: return CounterKeys.dex;
        }
    }

    private static void SpendAmmo(PartyMember actor)
    {
        string weapon = PartyBattleRules.FindWeapon(actor, true);
        string key = weapon == "SmallPistol" ? CounterKeys.nowSmallPistol : weapon == "Revolver" ? CounterKeys.nowRevolver
            : weapon == "Rifle" ? CounterKeys.nowRifle : weapon == "Shotgun" ? CounterKeys.nowShotgun : null;
        if (key != null && GameRoot.Instance != null)
        {
            GameRoot.Instance.State.SetCounter(key, Mathf.Max(0, GameRoot.Instance.State.GetCounter(key) - 1));
        }
    }

    private List<ActionOption> BuildOptions(PartyMember a)
    {
        List<ActionOption> list = new List<ActionOption>();
        Add(list, a, ActionKind.Melee, "Attack", "Close combat. Front and middle rank only; reaches the foe's front and middle.", false);
        if (PartyBattleRules.FindWeapon(a, true) != null)
        {
            Add(list, a, ActionKind.Ranged, "Shoot", "Fire at any foe. A front-rank shooter has a penalty.", false);
        }
        Add(list, a, ActionKind.Guard, "Guard", "Bonus to evade until your next turn.", true);
        Add(list, a, ActionKind.FirstAid, "First Aid", "Medicine check: stabilize a downed ally or heal 5 HP.", false);
        Add(list, a, ActionKind.Calm, "Calm", "Calm a panicking ally. Middle or back rank only.", false);
        Add(list, a, ActionKind.Swap, "Swap", "Trade places with a neighbor. Uses your turn.", false);
        if (a.IsLeader)
        {
            Add(list, a, ActionKind.Run, "Run", "Dex check to flee for the whole party.", true);
        }
        return list;
    }

    private void Add(List<ActionOption> list, PartyMember a, ActionKind kind, string label, string tip, bool noTarget)
    {
        list.Add(new ActionOption { Label = label, Tip = tip, Kind = kind, NoTarget = noTarget, Enabled = _field.ValidTargets(a, kind).Count > 0 });
    }

    // ---------- 룰렛 ----------

    /// <summary>파비안 판정은 기존 룰렛 한 번. 보너스·페널티 컨디션은 룰렛이 읽는 DataBaseManager.condition 을 잠깐 바꿔 반영한다.</summary>
    private IEnumerator RollLeader(string label, string pointName, int point, Condition condition, Action<SuccessLevel> result)
    {
        string keep = DataBaseManager.condition;
        if (condition == Condition.Best || condition == Condition.Worst)
        {
            DataBaseManager.condition = condition.ToString();
        }
        _view.SetInputBlocked(true);
        _waitingRoll = true;
        _rollDone = false;
        try
        {
            // 룰렛을 취소(Esc)로 닫으면 EndRollet 이 오지 않으므로 같은 판정을 다시 연다.
            while (!_rollDone)
            {
                Rollet.Instance.SetRollet(label, pointName, point, "PT_Fabian");
                yield return new WaitUntil(() => _rollDone || !DataBaseManager.isRollet);
            }
        }
        finally
        {
            _waitingRoll = false;
            DataBaseManager.condition = keep;
            _view.SetInputBlocked(false);
        }
        result(_rollLevel);
    }

    // ---------- 종료 ----------

    private void Finish(BattleOutcome outcome)
    {
        GameState s = GameRoot.Instance.State;
        PartyBattleBridge.WriteBack(s, _roster, DataBaseManager.timeCount);
        PartyMember leader = _field.Leader;
        if (leader != null)
        {
            DataBaseManager.nowHP = outcome == BattleOutcome.Loss ? Mathf.Min(0, leader.Hp) : leader.Hp;
            DataBaseManager.nowSan = leader.San;
        }
        _view.Close();
        _running = false;
        if (_manager != null)
        {
            _manager.PartyBattleEnd(_enemyIndices, outcome);
        }
        if (_onEnd != null)
        {
            _onEnd(outcome);
        }
    }
}
