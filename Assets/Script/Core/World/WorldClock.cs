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
