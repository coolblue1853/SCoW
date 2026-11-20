using SCoW.Core.Ui;
using UnityEngine;

/// <summary>
/// UI 열림·막힘 질의의 App 쪽 문(P5). 옛 DataBaseManager 정적 bool 을 BusyFlags 로 읽어 Core 의 BusyRules 에 넘기고,
/// 열린 패널은 GameRoot.Panels(UiStack) 에 올린다. Esc(Cancel) 는 프레임당 맨 위 패널 하나만 닫는다.
/// </summary>
public static class UiGate
{
    private static int _cancelFrame = -1;
    private static int _closeFrame = -1;

    public static void ResetStatics()
    {
        _cancelFrame = -1;
        _closeFrame = -1;
    }

    public static UiStack Stack
    {
        get { return GameRoot.Instance.Panels; }
    }

    public static BusyFlags Flags()
    {
        BusyFlags f = new BusyFlags();
        f.Dialog1 = DataBaseManager.isActiveDialog1;
        f.Dialog2 = DataBaseManager.isActiveDialog2;
        f.StoryDirecting = DataBaseManager.storyDirecting;
        f.SecondDirecting = DataBaseManager.secondisDirecting;
        f.Directing = DataBaseManager.isDirecting;
        f.Rollet = DataBaseManager.isRollet;
        f.BattleRollet = DataBaseManager.isBattleRollet;
        f.PortalWait = DataBaseManager.potalWait;
        f.MapMoving = DataBaseManager.isOpenMap;
        f.Scene = DataBaseManager.isScene;
        f.Select = DataBaseManager.isSelect;
        f.SelectionOn = DataBaseManager.selectionOn;
        f.BattleSan = DataBaseManager.battleSan;
        f.StrDialogOn = DataBaseManager.strDialogOn;
        f.BarMove = DataBaseManager.barMove;
        f.BattleBeforeDialog = DataBaseManager.isBattleBeforeDialog;
        f.AfterBattle = DataBaseManager.afterBattle;
        return f;
    }

    /// <summary>저장·불러오기를 막는 연출·대기 중인가(열린 패널은 제외).</summary>
    public static bool IsBusy
    {
        get { return BusyRules.IsBusy(Flags()); }
    }

    public static bool CanOpen(PanelKind kind)
    {
        return BusyRules.CanOpen(kind, Flags(), Stack, DataBaseManager.nowPlace, DataBaseManager.isDebuffDyslexia);
    }

    public static bool CanMove
    {
        get { return BusyRules.CanMove(Flags(), Stack); }
    }

    public static bool CanInteractWorld
    {
        get { return BusyRules.CanInteractWorld(Flags(), Stack); }
    }

    public static bool CanUsePortal
    {
        get { return BusyRules.CanUsePortal(Flags()); }
    }

    /// <summary>패널이 하나라도 열려 있나(옛 isOpenUi).</summary>
    public static bool AnyOpen
    {
        get { return Stack.Any; }
    }

    /// <summary>설정·슬롯창 등이 열려 있거나 이번 프레임에 닫혔다면 대사 진행 입력(E·클릭)을 삼켜야 한다.</summary>
    public static bool BlocksDialogueInput
    {
        get { return Stack.BlocksDialogueInput || _closeFrame == Time.frameCount; }
    }

    public static void Push(PanelKind kind)
    {
        Stack.Push(kind);
    }

    public static void Remove(PanelKind kind)
    {
        if (Stack.Remove(kind))
        {
            _closeFrame = Time.frameCount; // 패널을 닫은 입력이 같은 프레임에 대사를 넘기지 않게
        }
    }

    /// <summary>장소 이동·새 게임 등 전체 초기화(옛 isOpenUi = false).</summary>
    public static void Clear()
    {
        Stack.Clear();
    }

    /// <summary>이번 프레임에 Cancel 로 패널이 이미 닫혔나. 같은 키로 설정창이 곧바로 열리는 것을 막을 때 쓴다.</summary>
    public static bool CancelConsumedThisFrame
    {
        get { return _cancelFrame == Time.frameCount; }
    }

    /// <summary>
    /// Cancel(Esc/패드 B) 이 눌렸고 kind 가 맨 위이며 이번 프레임에 아직 소비되지 않았으면 true 를 돌려주고 소비한다.
    /// menuToo 면 Menu 키도 같은 취소로 본다(설정창·저장창: Esc 가 Cancel 과 Menu 둘 다).
    /// 호출한 쪽이 패널을 닫는다(Remove 호출은 호출자 몫).
    /// </summary>
    public static bool TryCancel(PanelKind kind, bool menuToo = false)
    {
        if (CancelConsumedThisFrame)
        {
            return false;
        }
        bool pressed = GameInput.CancelDown || (menuToo && GameInput.MenuDown);
        if (!pressed || !Stack.IsTop(kind))
        {
            return false;
        }
        _cancelFrame = Time.frameCount;
        return true;
    }
}
