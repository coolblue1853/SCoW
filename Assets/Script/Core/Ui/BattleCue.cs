using System;
using System.Collections.Generic;
using SCoW.Core.Rules;

namespace SCoW.Core.Ui
{
    /// <summary>전투 연출 한 동작의 종류. View 의 재생기가 종류별로 DOTween 을 건다.</summary>
    public enum BattleCueKind
    {
        /// <summary>차례 배너(UnitId = 행위자).</summary>
        Banner,
        /// <summary>행위자가 대상 쪽으로 돌진(UnitId = 행위자, OtherId = 대상).</summary>
        Lunge,
        /// <summary>대상이 맞아 번쩍·흔들림(UnitId = 대상).</summary>
        Hit,
        /// <summary>대상 머리 위 글자(Text, Tone).</summary>
        Popup,
        /// <summary>대상 HP·SAN 막대 갱신(Hp, San 은 이벤트가 알려 준 변화 후 값).</summary>
        Bars,
        /// <summary>쓰러짐(다운)·이탈 표시.</summary>
        Down,
        /// <summary>칸에서 사라짐(적 처치·동료 Fallen).</summary>
        Remove,
        /// <summary>칸 이동(UnitId = 유닛, Rank = 새 칸).</summary>
        Move,
        /// <summary>상태 글자(방어·발작·독 등) 갱신.</summary>
        Status,
        /// <summary>전투 로그 한 줄.</summary>
        Text
    }

    /// <summary>팝업 색 의미. 실제 색은 View 가 정한다.</summary>
    public enum CueTone { Neutral, Damage, SanLoss, Heal, Miss }

    /// <summary>재생기가 한 번에 처리하는 연출 단위(순수 데이터).</summary>
    public readonly struct BattleCue
    {
        public readonly BattleCueKind Kind;
        public readonly string UnitId;
        public readonly string OtherId;
        public readonly int Value;
        public readonly int Rank;
        public readonly int Hp;
        public readonly int San;
        public readonly string Text;
        public readonly CueTone Tone;
        /// <summary>true 면 앞 동작과 같은 박자에 함께 재생한다(번쩍 + 숫자 + 막대).</summary>
        public readonly bool Parallel;
        /// <summary>이 동작을 낳은 이벤트 번호(순서 규칙 검증용).</summary>
        public readonly int Source;

        public BattleCue(BattleCueKind kind, int source, string unitId, string otherId = null, int value = 0, int rank = -1,
            int hp = -1, int san = -1, string text = null, CueTone tone = CueTone.Neutral, bool parallel = false)
        {
            Kind = kind;
            Source = source;
            UnitId = unitId;
            OtherId = otherId;
            Value = value;
            Rank = rank;
            Hp = hp;
            San = san;
            Text = text;
            Tone = tone;
            Parallel = parallel;
        }

        public override string ToString()
        {
            return Kind + (Parallel ? "|" : ">") + UnitId + (OtherId == null ? string.Empty : "->" + OtherId) + (Text == null ? string.Empty : " '" + Text + "'");
        }
    }

    /// <summary>
    /// BattleEvent 목록 -> 연출 목록(순서 규칙, Party.md PT-5). 순수 C#.
    /// 규칙: (1) 이벤트 순서를 바꾸지 않는다(Source 오름차순). (2) 맞은 동작은 돌진 -> 번쩍 -> (숫자·막대·로그 함께) 순이고 쓰러짐은 그 뒤.
    /// (3) 연속한 칸 이동은 한 박자로 묶는다. (4) 로그 한 줄은 이벤트의 첫 동작과 함께 나온다.
    /// </summary>
    public static class BattleCueBuilder
    {
        public static List<BattleCue> Build(IReadOnlyList<BattleEvent> events, Func<string, string> nameOf = null)
        {
            List<BattleCue> cues = new List<BattleCue>();
            Build(events, cues, nameOf);
            return cues;
        }

        public static void Build(IReadOnlyList<BattleEvent> events, List<BattleCue> cues, Func<string, string> nameOf = null)
        {
            Func<string, string> name = nameOf ?? (id => id);
            for (int i = 0; i < events.Count; i++)
            {
                BattleEvent e = events[i];
                int first = cues.Count;
                AddCues(i, e, cues);
                string line = Describe(e, name);
                if (line != null)
                {
                    cues.Add(new BattleCue(BattleCueKind.Text, i, e.ActorId, e.TargetId, text: line, parallel: cues.Count > first));
                }
            }
            MarkMoveGroups(cues);
        }

        private static void AddCues(int s, BattleEvent e, List<BattleCue> c)
        {
            switch (e.Kind)
            {
                case BattleEventKind.Turn:
                    c.Add(new BattleCue(BattleCueKind.Banner, s, e.ActorId));
                    break;
                case BattleEventKind.Hit:
                    c.Add(new BattleCue(BattleCueKind.Lunge, s, e.ActorId, e.TargetId));
                    c.Add(new BattleCue(BattleCueKind.Hit, s, e.TargetId, e.ActorId));
                    c.Add(new BattleCue(BattleCueKind.Popup, s, e.TargetId, value: e.Value, text: "-" + e.Value, tone: CueTone.Damage, parallel: true));
                    c.Add(new BattleCue(BattleCueKind.Bars, s, e.TargetId, hp: e.Hp, san: e.San, parallel: true));
                    break;
                case BattleEventKind.Evade:
                    // 회피: 행위자 = 피한 쪽, 대상 = 공격한 적. 돌진은 적이 한다.
                    c.Add(new BattleCue(BattleCueKind.Lunge, s, e.TargetId, e.ActorId));
                    c.Add(new BattleCue(BattleCueKind.Popup, s, e.ActorId, text: "Evade", tone: CueTone.Miss, parallel: true));
                    break;
                case BattleEventKind.Miss:
                    c.Add(new BattleCue(BattleCueKind.Lunge, s, e.ActorId, e.TargetId));
                    c.Add(new BattleCue(BattleCueKind.Popup, s, e.TargetId, text: "Miss", tone: CueTone.Miss, parallel: true));
                    break;
                case BattleEventKind.SanLoss:
                    c.Add(new BattleCue(BattleCueKind.Popup, s, e.TargetId, value: e.Value, text: "SAN -" + e.Value, tone: CueTone.SanLoss));
                    c.Add(new BattleCue(BattleCueKind.Bars, s, e.TargetId, hp: e.Hp, san: e.San, parallel: true));
                    break;
                case BattleEventKind.Heal:
                case BattleEventKind.Calm:
                case BattleEventKind.Stable:
                    c.Add(new BattleCue(BattleCueKind.Popup, s, e.TargetId, value: e.Value, text: e.Kind == BattleEventKind.Calm ? "SAN +" + e.Value : "+" + e.Value, tone: CueTone.Heal));
                    c.Add(new BattleCue(BattleCueKind.Bars, s, e.TargetId, hp: e.Hp, san: e.San, parallel: true));
                    break;
                case BattleEventKind.PoisonTick:
                    c.Add(new BattleCue(BattleCueKind.Popup, s, e.TargetId, value: e.Value, text: "-" + e.Value, tone: CueTone.Damage));
                    c.Add(new BattleCue(BattleCueKind.Bars, s, e.TargetId, hp: e.Hp, san: e.San, parallel: true));
                    break;
                case BattleEventKind.ActionStart:
                    c.Add(new BattleCue(BattleCueKind.Lunge, s, e.ActorId, e.TargetId));
                    break;
                case BattleEventKind.Down:
                case BattleEventKind.Fallen:
                case BattleEventKind.Died:
                    c.Add(new BattleCue(BattleCueKind.Down, s, e.TargetId, rank: e.Rank));
                    if (e.Kind != BattleEventKind.Down)
                    {
                        c.Add(new BattleCue(BattleCueKind.Remove, s, e.TargetId, parallel: true));
                    }
                    break;
                case BattleEventKind.EnemyDown:
                    c.Add(new BattleCue(BattleCueKind.Remove, s, e.TargetId));
                    break;
                case BattleEventKind.Compact:
                    c.Add(new BattleCue(BattleCueKind.Move, s, e.TargetId, rank: e.Rank));
                    break;
                case BattleEventKind.Swap:
                    c.Add(new BattleCue(BattleCueKind.Move, s, e.ActorId, rank: e.Value));
                    c.Add(new BattleCue(BattleCueKind.Move, s, e.TargetId, rank: e.Rank, parallel: true));
                    break;
                case BattleEventKind.Guard:
                case BattleEventKind.BoutStart:
                case BattleEventKind.BoutEnd:
                case BattleEventKind.PoisonApplied:
                case BattleEventKind.Broken:
                case BattleEventKind.Recovering:
                case BattleEventKind.Countdown:
                    c.Add(new BattleCue(BattleCueKind.Status, s, e.TargetId ?? e.ActorId, value: e.Value));
                    break;
                case BattleEventKind.SanResist:
                    c.Add(new BattleCue(BattleCueKind.Popup, s, e.TargetId, text: "Resist", tone: CueTone.Neutral));
                    break;
            }
        }

        /// <summary>연속한 Move 는 앞 Move 와 같은 박자로 묶는다(압축이 여러 칸 동시에 일어난다).</summary>
        private static void MarkMoveGroups(List<BattleCue> cues)
        {
            for (int i = 1; i < cues.Count; i++)
            {
                BattleCue a = cues[i - 1];
                BattleCue b = cues[i];
                if (a.Kind == BattleCueKind.Move && b.Kind == BattleCueKind.Move && !b.Parallel)
                {
                    cues[i] = new BattleCue(b.Kind, b.Source, b.UnitId, b.OtherId, b.Value, b.Rank, b.Hp, b.San, b.Text, b.Tone, true);
                }
            }
        }

        /// <summary>판정 단계 꼬리표(자동 판정 결과를 보여 준다). 판정이 없으면 빈 문자열.</summary>
        private static string Roll(BattleEvent e)
        {
            return e.ActorLevel < 0 ? string.Empty : " (" + (SuccessLevel)e.ActorLevel + ")";
        }

        /// <summary>로그 한 줄(임시 영문). 줄이 필요 없는 이벤트는 null. TODO(I2): 문구를 I2 키로 옮긴다.</summary>
        public static string Describe(BattleEvent e, Func<string, string> name)
        {
            string a = name(e.ActorId);
            string t = name(e.TargetId);
            switch (e.Kind)
            {
                case BattleEventKind.Turn: return a + "'s turn";
                case BattleEventKind.Hit: return a + " hits " + t + " for " + e.Value + Roll(e);
                case BattleEventKind.Miss: return a + " misses " + t + Roll(e);
                case BattleEventKind.Evade: return a + " evades " + t + Roll(e);
                case BattleEventKind.SanLoss: return t + " loses " + e.Value + " SAN";
                case BattleEventKind.Heal: return t + " recovers " + e.Value + " HP";
                case BattleEventKind.Calm: return t + " calms down";
                case BattleEventKind.Stable: return t + " is stabilized";
                case BattleEventKind.Down: return t + " is down";
                case BattleEventKind.Fallen: return t + " has fallen";
                case BattleEventKind.EnemyDown: return t + " is slain";
                case BattleEventKind.Guard: return t + " guards";
                case BattleEventKind.Swap: return a + " swaps with " + t;
                case BattleEventKind.BoutStart: return t + " panics";
                case BattleEventKind.BoutEnd: return t + " recovers";
                case BattleEventKind.PoisonApplied: return t + " is poisoned";
                case BattleEventKind.RunSuccess: return a + " escapes";
                case BattleEventKind.RunFail: return a + " fails to escape";
                case BattleEventKind.Skip: return a + " skips";
                case BattleEventKind.Broken: return t + " breaks down";
                case BattleEventKind.Died: return t + " is lost";
                default: return null;
            }
        }
    }
}
