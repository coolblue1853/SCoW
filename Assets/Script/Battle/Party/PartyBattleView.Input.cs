using System.Collections.Generic;
using SCoW.Core.Ui;
using SCoW.Core.Rules;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>PartyBattleView 의 입력 처리(마우스·키보드·패드 같은 경로, GameInput 만 사용). 대열 교체 단축키 포함(PT-6).</summary>
public sealed partial class PartyBattleView
{
    // ---------- 입력 ----------

    private void Update()
    {
        if (_blocked)
        {
            return;
        }
        if (_mode == Mode.None || Time.frameCount < _armedFrame)
        {
            HoverTooltipOnly();
            return;
        }
        bool confirm = GameInput.InteractDown;
        bool cancel = GameInput.CancelDown;
        Mouse mouse = Mouse.current;
        bool click = mouse != null && mouse.leftButton.wasPressedThisFrame;
        if (mouse != null && mouse.rightButton.wasPressedThisFrame)
        {
            cancel = true;
        }
        Vector2 pointer = GameInput.PointerPosition;
        bool moved = (pointer - _lastPointer).sqrMagnitude > 4f;
        _lastPointer = pointer;
        float mx = GameInput.MoveX;
        int step = mx != 0f && mx != _prevMoveX ? (int)mx : 0;
        _prevMoveX = mx;
        if (_mode == Mode.Menu)
        {
            UpdateMenu(confirm, click, moved, pointer);
        }
        else
        {
            UpdateTarget(confirm, cancel, click, moved, pointer, step);
        }
    }

    private void HoverTooltipOnly()
    {
        if (_mode != Mode.None || Panels.Count == 0)
        {
            return;
        }
        Vector2 pointer = GameInput.PointerPosition;
        foreach (KeyValuePair<string, PartyBattlePanel> kv in Panels)
        {
            if (!kv.Value.Gone && kv.Value.Contains(pointer))
            {
                TooltipView.Show(this, BattleTooltipText.ForUnit(FindUnit(kv.Key)), kv.Value.Rect);
                return;
            }
        }
        TooltipView.Hide(this);
    }

    private void UpdateMenu(bool confirm, bool click, bool moved, Vector2 pointer)
    {
        int move = (GameInput.NavDownDown ? 1 : 0) - (GameInput.NavUpDown ? 1 : 0);
        if (_menu.Count == 0)
        {
            return;
        }
        if (move != 0)
        {
            // 메뉴는 위에서 아래 순서로 그려지므로 아래 = 다음 줄. 비활성 줄은 건너뛴다.
            _menuEnabled.Clear();
            for (int i = 0; i < _menu.Count; i++)
            {
                _menuEnabled.Add(_menu[i].Enabled);
            }
            _cursor = BattleSelectNav.MoveMenu(_menuEnabled, _cursor, move);
            PaintMenu();
        }
        if (TryQuickSwap())
        {
            return;
        }
        int hover = -1;
        if (moved || click)
        {
            for (int i = 0; i < _menuRects.Count; i++)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(_menuRects[i], pointer, null))
                {
                    hover = i;
                }
            }
            if (hover >= 0 && hover != _cursor)
            {
                _cursor = hover;
                PaintMenu();
            }
        }
        if ((confirm || (click && hover >= 0)) && _menu[_cursor].Enabled)
        {
            Finish(_cursor, false);
        }
    }

    /// <summary>대열 교체 단축키(Q·R / LB·RB): 행동 메뉴에서 바로 이웃 칸과 자리를 바꾼다. 쓸 수 없으면 false.</summary>
    private bool TryQuickSwap()
    {
        int dir = GameInput.SwapLeftDown ? -1 : (GameInput.SwapRightDown ? 1 : 0);
        if (dir == 0 || _swapMenuIndex < 0 || _actor == null)
        {
            return false;
        }
        int rank = BattleSelectNav.SwapTargetRank(_actor.Rank, dir, _field.ValidTargets(_actor, ActionKind.Swap));
        if (rank < 0)
        {
            return false;
        }
        QuickSwap(rank);
        return true;
    }

    private void QuickSwap(int rank)
    {
        _pick.Side = Side.Ally;
        _pick.Rank = rank;
        _pick.QuickRank = rank;
        Finish(_swapMenuIndex, false);
    }

    /// <summary>플레이 확인용: 대열 교체 단축키를 입력 없이 누른 것처럼 처리한다(dir -1 왼쪽, +1 오른쪽).</summary>
    public bool DebugSwap(int dir)
    {
        if (_mode != Mode.Menu || _swapMenuIndex < 0 || _actor == null)
        {
            return false;
        }
        int rank = BattleSelectNav.SwapTargetRank(_actor.Rank, dir, _field.ValidTargets(_actor, ActionKind.Swap));
        if (rank < 0)
        {
            return false;
        }
        QuickSwap(rank);
        return true;
    }

    private void UpdateTarget(bool confirm, bool cancel, bool click, bool moved, Vector2 pointer, int step)
    {
        if (cancel)
        {
            Finish(-1, true);
            return;
        }
        if (_targets.Count == 0)
        {
            return;
        }
        if (step != 0)
        {
            _cursor = BattleSelectNav.MoveTarget(_cursor, _targets.Count, step, _targetKeys[0].side);
            PaintTarget();
        }
        int hover = -1;
        for (int i = 0; i < _targets.Count; i++)
        {
            if (_targets[i].Contains(pointer))
            {
                hover = i;
            }
        }
        if (moved && hover >= 0 && hover != _cursor)
        {
            _cursor = hover;
            PaintTarget();
        }
        if (confirm || (click && hover >= 0))
        {
            if (click && hover >= 0)
            {
                _cursor = hover;
            }
            _pick.Side = _targetKeys[_cursor].side;
            _pick.Rank = _targetKeys[_cursor].rank;
            Finish(_cursor, false);
        }
    }

    private void Finish(int index, bool cancelled)
    {
        ViewPick pick = _pick;
        SetMode(Mode.None);
        pick.Index = index;
        pick.Cancelled = cancelled;
        pick.Done = true;
    }

    /// <summary>플레이 확인용(스크립트로 선택): 입력 없이 결과를 정한다.</summary>
    public void DebugAnswer(int index)
    {
        if (_mode == Mode.Menu && index >= 0 && index < _menu.Count && _menu[index].Enabled)
        {
            Finish(index, false);
        }
        else if (_mode == Mode.Target && index >= 0 && index < _targets.Count)
        {
            _pick.Side = _targetKeys[index].side;
            _pick.Rank = _targetKeys[index].rank;
            Finish(index, false);
        }
    }

    public string DebugMode
    {
        get { return _mode.ToString(); }
    }

    public int DebugCount
    {
        get { return _mode == Mode.Menu ? _menu.Count : _targets.Count; }
    }

    /// <summary>대기 화면에서 아무 입력이나 기다리지 않도록 막을 때 쓴다(룰렛 중에는 View 입력을 닫는다).</summary>
    public void SetInputBlocked(bool blocked)
    {
        _blocked = blocked; _root.gameObject.SetActive(!blocked);
        if (blocked)
        {
            TooltipView.HideAll();
        }
    }
}
