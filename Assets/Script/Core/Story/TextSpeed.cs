namespace SCoW.Core.Story
{
    /// <summary>대사 글자 출력 속도 5단계(설정 슬라이더). 값은 글자 사이 대기 시간(초).</summary>
    public static class TextSpeed
    {
        public const int MinLevel = 1;
        public const int MaxLevel = 5;
        public const int DefaultLevel = 3;

        private static readonly float[] Delays = { 0.06f, 0.045f, 0.03f, 0.018f, 0.008f };

        public static int Clamp(int level)
        {
            return level < MinLevel ? MinLevel : (level > MaxLevel ? MaxLevel : level);
        }

        public static float DelayOf(int level)
        {
            return Delays[Clamp(level) - MinLevel];
        }
    }

    /// <summary>옛 CSV 대사 본문에서 줄 종류를 미리 알아본다(선택지·판정 표식).</summary>
    public static class LineMarks
    {
        /// <summary>선택지 표식(별, 검은별)이 있으면 Choice, 대화 중 판정 표식(스페이드, 클럽)이 있으면 Check.</summary>
        public static LineKind Classify(string raw)
        {
            if (string.IsNullOrEmpty(raw))
            {
                return LineKind.Normal;
            }
            if (raw.IndexOf('☆') >= 0 || raw.IndexOf('★') >= 0)
            {
                return LineKind.Choice;
            }
            if (raw.IndexOf('♠') >= 0 || raw.IndexOf('♣') >= 0)
            {
                return LineKind.Check;
            }
            return LineKind.Normal;
        }
    }
}
