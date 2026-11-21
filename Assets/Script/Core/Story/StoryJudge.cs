namespace SCoW.Core.Story
{
    /// <summary>판정 결과 단계. 옛 Rollet 결과 문자열은 JudgeOutcomes.FromLegacy 로 바꾼다.</summary>
    public enum JudgeOutcome
    {
        /// <summary>결과 문자열이 판정 결과가 아님("Result : --" 등). 결과와 무관한 효과만 적용된다.</summary>
        None,
        CritSuccess,
        Success,
        Fail,
        CritFail
    }

    public static class JudgeOutcomes
    {
        // Rollet.cs 가 resultEnd.text 에 쓰는 옛 문자열(LegacyString). 문구는 바꾸지 않는다.
        public const string LegacyCritSuccess = "Result : Critical Success";
        public const string LegacySuccess = "Result : Success";
        public const string LegacyFail = "Result : Failure";
        public const string LegacyCritFail = "Result : Fumble";

        public static JudgeOutcome FromLegacy(string text)
        {
            switch (text)
            {
                case LegacyCritSuccess: return JudgeOutcome.CritSuccess;
                case LegacySuccess: return JudgeOutcome.Success;
                case LegacyFail: return JudgeOutcome.Fail;
                case LegacyCritFail: return JudgeOutcome.CritFail;
            }
            return JudgeOutcome.None;
        }

        public static bool IsSuccess(JudgeOutcome o) { return o == JudgeOutcome.Success || o == JudgeOutcome.CritSuccess; }

        public static bool IsFail(JudgeOutcome o) { return o == JudgeOutcome.Fail || o == JudgeOutcome.CritFail; }

        /// <summary>결과에 맞는 가지(노드·토픽 id). 대성공/대실패 칸이 비면 성공/실패 칸으로 간다. 없으면 "".</summary>
        public static string Branch(StoryNode node, JudgeOutcome o)
        {
            switch (o)
            {
                case JudgeOutcome.CritSuccess: return node.OnCritSuccess.Length > 0 ? node.OnCritSuccess : node.OnSuccess;
                case JudgeOutcome.Success: return node.OnSuccess;
                case JudgeOutcome.CritFail: return node.OnCritFail.Length > 0 ? node.OnCritFail : node.OnFail;
                case JudgeOutcome.Fail: return node.OnFail;
            }
            return "";
        }
    }

    /// <summary>판정 결과가 일으키는, 상태(플래그·카운터)로 못 옮긴 씬 쪽 부수효과 목록. 실행은 App(JudgeSceneEffects)이 한다.</summary>
    [System.Flags]
    public enum JudgeScene
    {
        None = 0,
        /// <summary>DataBaseManager.condition = "Good"(커피 판정 성공).</summary>
        ConditionGood = 1,
        /// <summary>DataBaseManager.storyDirecting = true(상징 부수기 판정).</summary>
        StoryDirecting = 2,
        /// <summary>HP 5 감소(BillowUIManager.HpDown(5)).</summary>
        HpDown5 = 4,
        /// <summary>0.5초 뒤 수영 판정 다시(Rollet "Fabian : Swimming").</summary>
        SwimRetry = 8
    }

    public static class JudgeSceneRules
    {
        /// <summary>sub = 판정 이름(옛 Sub_Dialog). 옛 RetrunDialogResult 가 결과에 따라 직접 하던 씬 부수효과.</summary>
        public static JudgeScene Plan(string sub, JudgeOutcome o)
        {
            switch (sub)
            {
                case "Stove : Making coffee":
                    return JudgeOutcomes.IsSuccess(o) ? JudgeScene.ConditionGood : JudgeScene.None;
                case "Aiden : TakeAway":
                    return JudgeOutcomes.IsFail(o) ? JudgeScene.HpDown5 : JudgeScene.None;
                case "Symbol : Break":
                    return JudgeScene.StoryDirecting;
                case "Fabian : Swimming":
                    return JudgeOutcomes.IsFail(o) ? (JudgeScene.HpDown5 | JudgeScene.SwimRetry) : JudgeScene.None;
            }
            return JudgeScene.None;
        }
    }
}
