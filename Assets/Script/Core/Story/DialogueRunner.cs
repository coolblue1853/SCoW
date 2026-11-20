using SCoW.Core.State;

namespace SCoW.Core.Story
{
    public enum RunnerState
    {
        /// <summary>줄이 없다(대사 밖, 또는 다음 줄로 넘기는 중).</summary>
        Idle,
        /// <summary>글자가 나오는 중.</summary>
        Typing,
        /// <summary>글자가 다 나왔고 다음 입력을 기다린다.</summary>
        Waiting,
    }

    public enum LineKind
    {
        Normal,
        /// <summary>이 줄에서 선택지가 뜬다. 스킵은 여기서 멈춘다.</summary>
        Choice,
        /// <summary>이 줄에서 판정이 뜬다. 스킵은 여기서 멈춘다.</summary>
        Check,
    }

    public enum Pending
    {
        None,
        /// <summary>선택지 응답 대기: 입력·스킵·자동 모두 막는다.</summary>
        Choice,
        /// <summary>판정·연출 대기: 스킵·자동만 막는다.</summary>
        Check,
    }

    public enum RunnerAction
    {
        None,
        /// <summary>글자 출력을 즉시 끝낸다(뷰가 Text Animator SkipTypewriter 호출).</summary>
        CompleteTyping,
        /// <summary>다음 줄로 간다.</summary>
        NextLine,
    }

    /// <summary>
    /// 대사 진행 상태 기계(순수 C#). 줄 진행의 유일한 입구는 Advance(플레이어 입력)와 Tick(스킵·자동)이고,
    /// 둘 다 같은 규칙으로 RunnerAction 하나를 돌려준다. NextLine 을 돌려주면 즉시 Idle 이 되어 같은 줄에서 두 번 넘어가지 못한다.
    /// 스킵은 안 읽은 줄(읽은 것만 모드)·선택지/판정 줄·대기 상태 앞에서 멈춘다.
    /// </summary>
    public sealed class DialogueRunner
    {
        public const float SkipInterval = 0.05f;
        public const float AutoBase = 1.0f;
        public const float AutoPerChar = 0.06f;
        public const float AutoMax = 8f;

        private readonly ReadLog _read;
        private readonly Backlog _backlog;

        private bool _skipToggle;
        private bool _skipHeld;
        private bool _heldSuppressed;
        private float _timer;
        private int _textLength;

        public DialogueRunner(ReadLog read, Backlog backlog)
        {
            _read = read;
            _backlog = backlog;
        }

        public RunnerState State { get; private set; }
        public Pending Pending { get; private set; }
        public bool AutoOn { get; set; }
        /// <summary>true 면 안 읽은 줄도 스킵한다(설정). 기본은 읽은 것만.</summary>
        public bool SkipAll { get; set; }
        public Backlog Backlog { get { return _backlog; } }
        public ReadLog ReadLog { get { return _read; } }
        /// <summary>마지막으로 BeginLine 에 들어온 줄이 이전에 읽은 줄이었나.</summary>
        public bool CurrentWasRead { get; private set; }
        /// <summary>마지막 스킵 정지 사유(디버그·표시용). 정지가 없었으면 null.</summary>
        public string LastStopReason { get; private set; }

        public bool SkipToggled { get { return _skipToggle; } }

        /// <summary>토글 또는 홀드(Ctrl) 중 어느 쪽이든 지금 스킵 중인가.</summary>
        public bool SkipActive
        {
            get { return _skipToggle || (_skipHeld && !_heldSuppressed); }
        }

        public void SetSkipToggle(bool on)
        {
            _skipToggle = on;
            if (on)
            {
                LastStopReason = null;
            }
        }

        public void SetSkipHeld(bool held)
        {
            _skipHeld = held;
            if (!held)
            {
                _heldSuppressed = false;
            }
        }

        /// <summary>새 줄이 시작될 때 뷰가 호출. 백로그·읽음 기록을 갱신하고 스킵 정지 여부를 정한다.</summary>
        public void BeginLine(string scriptId, string lineId, string speaker, string plainText, LineKind kind)
        {
            string key = ReadLog.KeyOf(scriptId, lineId);
            CurrentWasRead = _read.Contains(key);
            _read.Add(key);
            _backlog.Add(speaker, plainText);
            _textLength = plainText == null ? 0 : plainText.Length;
            _timer = 0f;
            State = RunnerState.Typing;
            if (SkipActive)
            {
                if (kind != LineKind.Normal)
                {
                    StopSkip(kind == LineKind.Choice ? "choice" : "check");
                }
                else if (!SkipAll && !CurrentWasRead)
                {
                    StopSkip("unread");
                }
            }
        }

        /// <summary>글자 출력이 끝났다(onTextShowed 또는 즉시 완료 뒤). 여러 번 불러도 안전.</summary>
        public void TypingDone()
        {
            if (State == RunnerState.Typing)
            {
                State = RunnerState.Waiting;
                _timer = 0f;
            }
        }

        /// <summary>대사가 끝났다. 스킵은 꺼지고 자동은 사용자 설정이라 유지.</summary>
        public void End()
        {
            State = RunnerState.Idle;
            Pending = Pending.None;
            _skipToggle = false;
            _heldSuppressed = _skipHeld;
            _timer = 0f;
        }

        /// <summary>뷰가 프레임마다 현재 대기 상태를 알려준다. 새로 대기에 들어가면 스킵을 멈춘다.</summary>
        public void SetPending(Pending p)
        {
            if (p != Pending.None && Pending == Pending.None && SkipActive)
            {
                StopSkip(p == Pending.Choice ? "choice" : "check");
            }
            Pending = p;
        }

        /// <summary>플레이어 입력(E·클릭·패드). 글자 중이면 완료, 대기 중이면 다음 줄.</summary>
        public RunnerAction Advance()
        {
            if (Pending == Pending.Choice)
            {
                return RunnerAction.None;
            }
            return Step();
        }

        /// <summary>매 프레임(대사 중). 스킵·자동 진행.</summary>
        public RunnerAction Tick(float dt)
        {
            if (Pending != Pending.None || State == RunnerState.Idle)
            {
                return RunnerAction.None;
            }
            if (SkipActive)
            {
                if (State == RunnerState.Typing)
                {
                    return RunnerAction.CompleteTyping;
                }
                _timer += dt;
                return _timer >= SkipInterval ? Step() : RunnerAction.None;
            }
            if (AutoOn && State == RunnerState.Waiting)
            {
                _timer += dt;
                if (_timer >= AutoWait(_textLength))
                {
                    return Step();
                }
            }
            return RunnerAction.None;
        }

        public static float AutoWait(int textLength)
        {
            float w = AutoBase + (textLength < 0 ? 0 : textLength) * AutoPerChar;
            return w > AutoMax ? AutoMax : w;
        }

        private RunnerAction Step()
        {
            if (State == RunnerState.Typing)
            {
                return RunnerAction.CompleteTyping;
            }
            if (State == RunnerState.Waiting)
            {
                State = RunnerState.Idle;
                return RunnerAction.NextLine;
            }
            return RunnerAction.None;
        }

        private void StopSkip(string reason)
        {
            _skipToggle = false;
            if (_skipHeld)
            {
                _heldSuppressed = true;
            }
            LastStopReason = reason;
        }
    }
}
