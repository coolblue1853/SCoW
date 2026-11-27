using System;

namespace SCoW.Core.Story
{
    /// <summary>문 경찰에게 지금 쓸 수 있는 방법(StoryImprove C-4).</summary>
    [Flags]
    public enum DoorPoliceOption
    {
        None = 0,
        /// <summary>말로 설득(언변 판정, 한 번만).</summary>
        Persuade = 1,
        /// <summary>"알버트가 보냈다"(비판정, 알버트에게 실종자 정보를 들었을 때).</summary>
        AlbertSent = 2,
    }

    /// <summary>
    /// 2일차 낮 강제 사건과 시신 입장 규칙(순수 C#, 스토리 P1 C-1·C-4).
    /// C-1: 설계표의 `time>=6 && (flag:aidenFirstDialog || time>=7)`(이동 후 시각 기준)를 "사건 칸" 계산으로 옮겼다.
    /// 에이든 접촉(aidenFirstDialog, 의뢰인 집·신문사 첫 대화)을 놓친 채 2일차 아침(5)이 되면 사건 칸을 6 에서 7 로 미뤄 낮(6)에 의뢰인의 집을 다녀올 수 있게 한다
    /// (ShouldDelay 는 접촉 여부만 본다. 사건 칸에 묶인 코드·데이터는 이 클래스의 IsEventTime·IsAfterEventSlot·IsForcedDeparture 를 쓴다).
    /// 미룬다는 결정은 2일차 아침 첫 지도 열기에서 한 번 내려 영구 플래그(noonEventDelayed)에 남기고, 이후 사건 시각 판정은 이 플래그만 읽는다
    /// (낮에 에이든을 만나도 방문 중간에 집이 잠기지 않는다).
    /// </summary>
    public static class NoonEventRules
    {
        public const int BaseSlot = 6;
        public const int DelayedSlot = 7;

        /// <summary>2일차 아침에 사건을 미룰까. 에이든 접촉(aidenFirstDialog)이 없으면 미룬다.</summary>
        public static bool ShouldDelay(bool aidenFirstDialog)
        {
            return !aidenFirstDialog;
        }

        /// <summary>사건이 일어나는 timeCount(이동 후 값).</summary>
        public static int EventSlot(bool delayed)
        {
            return delayed ? DelayedSlot : BaseSlot;
        }

        /// <summary>사건이 시작됐거나 지났다(의뢰인의 집·사건 현장이 경찰 통제 상태, 옛 timeCount>=6).</summary>
        public static bool IsEventTime(int timeCount, bool delayed)
        {
            return timeCount >= EventSlot(delayed);
        }

        /// <summary>사건 칸이 지났다(옛 timeCount>=7): 목격자·시신이 사라지고 사고 현장이 보인다.</summary>
        public static bool IsAfterEventSlot(int timeCount, bool delayed)
        {
            return timeCount > EventSlot(delayed);
        }

        /// <summary>지도에서 이동할 때 강제로 사건 장소로 보내는가(옛 timeCount==5 && noonMorningMove).</summary>
        public static bool IsForcedDeparture(int timeCount, bool noonMorningMove, bool delayed)
        {
            return noonMorningMove && timeCount == EventSlot(delayed) - 1;
        }

        /// <summary>문 경찰 앞에서 고를 수 있는 방법. 입장 권한이 이미 있으면 없다.</summary>
        public static DoorPoliceOption DoorPoliceOptions(bool hasAccess, bool albertToldMissing, bool persuadeTried)
        {
            if (hasAccess)
            {
                return DoorPoliceOption.None;
            }
            DoorPoliceOption o = DoorPoliceOption.None;
            if (!persuadeTried)
            {
                o |= DoorPoliceOption.Persuade;
            }
            if (albertToldMissing)
            {
                o |= DoorPoliceOption.AlbertSent;
            }
            return o;
        }
    }
}
