using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SCoW.Core.Rules;
using SCoW.Core.Ui;
using UnityEngine;

/// <summary>
/// BattleEvent 재생기(PT-5, View). Core 가 낸 이벤트를 BattleCueBuilder(순서 규칙)로 연출 목록으로 바꾸고,
/// 박자(beat)마다 DOTween 시퀀스로 재생한다. 흔들림은 ShakeFx.Position(설정의 흔들림 끄기 존중), 빠른 판정 설정이면 2.5배속.
/// 재생은 코루틴으로 기다린다(호출 측 `yield return player.Play(events)`).
/// </summary>
public sealed class BattleEventPlayer
{
    private const float LungeDistance = 70f;
    private const float LungeSeconds = 0.2f;
    private const float HitSeconds = 0.25f;
    private const float MoveSeconds = 0.3f;
    private const float FadeSeconds = 0.35f;
    private const float BannerSeconds = 0.25f;
    private const float PopupHold = 0.35f;

    private readonly PartyBattleView _view;
    private readonly List<BattleCue> _cues = new List<BattleCue>();

    public BattleEventPlayer(PartyBattleView view)
    {
        _view = view;
    }

    public IEnumerator Play(IReadOnlyList<BattleEvent> events)
    {
        _cues.Clear();
        BattleCueBuilder.Build(events, _cues, NameOf);
        int i = 0;
        while (i < _cues.Count)
        {
            Sequence beat = DOTween.Sequence().SetLink(_view.gameObject);
            beat.timeScale = SettingsService.Current.FastRoll ? 2.5f : 1f;
            float length = 0f;
            do
            {
                length = Mathf.Max(length, AddCue(beat, _cues[i]));
                i++;
            }
            while (i < _cues.Count && _cues[i].Parallel);
            if (length > 0f)
            {
                beat.AppendInterval(Mathf.Max(0f, length - beat.Duration()));
                yield return beat.WaitForCompletion();
            }
            else
            {
                beat.Kill();
            }
        }
        _view.RefreshAll();
    }

    /// <summary>플레이 확인용: 마지막 재생의 연출 목록 요약.</summary>
    public string LastCueSummary()
    {
        return string.Join(" ", _cues);
    }

    private string NameOf(string id)
    {
        IBattleUnit u = _view.FindUnit(id);
        return u == null ? id : BattleTooltipText.UnitName(u);
    }

    /// <summary>cue 한 동작을 beat 에 넣고, 이 동작이 박자에 요구하는 길이(초)를 돌려준다.</summary>
    private float AddCue(Sequence beat, BattleCue c)
    {
        PartyBattlePanel unit = _view.PanelOf(c.UnitId);
        switch (c.Kind)
        {
            case BattleCueKind.Banner:
                _view.SetActiveUnit(c.UnitId);
                return BannerSeconds;
            case BattleCueKind.Lunge:
                return Lunge(beat, unit, _view.PanelOf(c.OtherId));
            case BattleCueKind.Hit:
                if (unit == null)
                {
                    return 0f;
                }
                beat.Insert(0f, unit.FlashRed(HitSeconds));
                beat.Insert(0f, ShakeFx.Position(unit.Rect, HitSeconds, 14f, 25));
                return HitSeconds;
            case BattleCueKind.Popup:
                if (unit != null)
                {
                    _view.SpawnPopup(unit, c.Text, c.Tone);
                }
                return PopupHold;
            case BattleCueKind.Bars:
                if (unit != null)
                {
                    unit.SetBars(c.Hp, c.San >= 0 ? c.San : 0, true);
                }
                return 0f;
            case BattleCueKind.Down:
                if (unit != null)
                {
                    unit.SetDimmed(true);
                    unit.SetStatusText("Down");
                }
                return PopupHold;
            case BattleCueKind.Remove:
                return Remove(beat, unit);
            case BattleCueKind.Move:
                return Move(beat, unit, c.Rank);
            case BattleCueKind.Status:
                if (unit != null)
                {
                    unit.SetStatusText(BattleTooltipText.StatusLabel(_view.FindUnit(c.UnitId)));
                }
                return 0f;
            case BattleCueKind.Text:
                _view.AppendLog(c.Text);
                return 0f;
        }
        return 0f;
    }

    private float Lunge(Sequence beat, PartyBattlePanel actor, PartyBattlePanel target)
    {
        if (actor == null || target == null || actor == target)
        {
            return 0f;
        }
        Vector2 home = _view.SlotPos(actor.Side, actor.Rank);
        float dir = actor.Side == Side.Ally ? 1f : -1f;
        actor.Rect.DOKill();
        Sequence s = DOTween.Sequence();
        s.Append(actor.Rect.DOAnchorPos(home + new Vector2(dir * LungeDistance, 0f), LungeSeconds * 0.5f).SetEase(Ease.OutQuad));
        s.Append(actor.Rect.DOAnchorPos(home, LungeSeconds * 0.5f).SetEase(Ease.InQuad));
        beat.Insert(0f, s);
        return LungeSeconds;
    }

    private float Remove(Sequence beat, PartyBattlePanel unit)
    {
        if (unit == null)
        {
            return 0f;
        }
        unit.Gone = true;
        CanvasGroup group = unit.Rect.GetComponent<CanvasGroup>();
        beat.Insert(0f, DOTween.To(() => group.alpha, a => group.alpha = a, 0f, FadeSeconds).SetTarget(unit.Rect));
        return FadeSeconds;
    }

    private float Move(Sequence beat, PartyBattlePanel unit, int rank)
    {
        if (unit == null || rank < 0)
        {
            return 0f;
        }
        unit.Rank = rank;
        unit.Rect.DOKill();
        beat.Insert(0f, unit.Rect.DOAnchorPos(_view.SlotPos(unit.Side, rank), MoveSeconds).SetEase(Ease.OutQuad));
        return MoveSeconds;
    }
}
