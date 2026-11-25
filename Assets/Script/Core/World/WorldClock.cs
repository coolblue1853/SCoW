namespace SCoW.Core.World
{
    /// <summary>
    /// 시간대 계산(순수 C#, C-2 P13). timeCount 는 한 번 흐를 때마다 1 오르고 4 칸이 하루다.
    /// 나머지 1 = 아침, 2 = 낮, 3 = 저녁, 0 = 밤. 옛 TimeManagere 의 timeCount % 4 식을 그대로(음수는 C# % 의 음수 나머지 그대로) 옮겼다.
    /// </summary>
    public static class WorldClock
    {
        public const int PhasesPerDay = 4;

        /// <summary>시간대(0 = 밤, 1 = 아침, 2 = 낮, 3 = 저녁).</summary>
        public static int Phase(int timeCount)
        {
            return timeCount % PhasesPerDay;
        }

        /// <summary>마지막 칸(4일차 밤). 이 칸까지 하수도로 못 가면 악몽 엔딩(Story.md 엔딩 D).</summary>
        public const int LastSlot = 16;

        /// <summary>일차(1부터). timeCount 가 1 미만이면 1(SaveMeta.Day 와 같은 식).</summary>
        public static int Day(int timeCount)
        {
            return timeCount <= 0 ? 1 : (timeCount - 1) / PhasesPerDay + 1;
        }

        /// <summary>마감까지 남은 칸 수(지금 칸은 제외). 마지막 칸 이후는 0.</summary>
        public static int SlotsLeft(int timeCount)
        {
            int left = LastSlot - timeCount;
            return left < 0 ? 0 : left;
        }

        /// <summary>하루 안에서 지금까지 지난 칸 수(지금 칸 포함, 1~4). 음수·0 이면 1.</summary>
        public static int SlotInDay(int timeCount)
        {
            return timeCount <= 0 ? 1 : (timeCount - 1) % PhasesPerDay + 1;
        }

        /// <summary>
        /// 수첩 날짜 한 줄(스토리 P0 B-3). 사건은 7/11~7/14 나흘이고 행성이 늘어서는 건 15일 밤이다. 오늘 날짜(1일차 = 7/11)를 같이 적는다.
        /// 4일을 넘기는 칸은 4일차로 본다.
        /// </summary>
        public static string DateLine(int timeCount, bool korean)
        {
            int day = Day(timeCount);
            if (day > 4) { day = 4; }
            int date = 10 + day;
            if (korean) { return "날짜: 7/11~7/14 (오늘 7/" + date + ")"; }
            return "Date: Jul 11-14 (today Jul " + date + ")";
        }

        public static bool IsNight(int timeCount)
        {
            return Phase(timeCount) == 0;
        }

        /// <summary>LightColorController.time 에 넣을 값. 아침 0.2, 낮 0.4, 저녁 0.64, 밤 0.9. 시간대가 아니면(음수 나머지) false.</summary>
        public static bool TryLightTime(int timeCount, out float time)
        {
            switch (Phase(timeCount))
            {
                case 1: time = 0.2f; return true;
                case 2: time = 0.4f; return true;
                case 3: time = 0.64f; return true;
                case 0: time = 0.9f; return true;
                default: time = 0f; return false;
            }
        }
    }
}
