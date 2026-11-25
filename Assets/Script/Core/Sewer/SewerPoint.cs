using SCoW.Core.State;

namespace SCoW.Core.Sewer
{
    /// <summary>조사 포인트의 판정 종류. None 이면 판정 없이 대사만 나온다.</summary>
    public enum SewerCheck
    {
        None,
        Observation,
        Luck,
        Intelligence,
    }

    /// <summary>
    /// 하수도 방 안의 조사 포인트(SEWER-1 S2). 대사는 InSewer(대사만) 또는 Judge(판정) 토픽으로 연결하고,
    /// 단서는 ClueFlag 로 기록한다. 이미 조사했는지는 SewerState.IsLooked 가 기억한다.
    /// </summary>
    public sealed class SewerPoint
    {
        public readonly string Id;
        public readonly string Label;
        /// <summary>판정 없음: InSewer 토픽 이름. 판정 있음: Judge.json 의 판정 이름(Rollet 제목).</summary>
        public readonly string Topic;
        public readonly SewerCheck Check;
        /// <summary>대사 노드가 켜는 단서 플래그(FlagKeys). 실제로 켜는 쪽은 Judge/InSewer.json 의 onEnter 이고, 이 값은 같은지 확인하는 데이터(테스트용 기록)다. 판정이면 성공 때만 켜진다.</summary>
        public readonly string ClueFlag;
        /// <summary>방 안 가로 위치(씬 월드 x).</summary>
        public readonly float X;

        public SewerPoint(string id, string label, string topic, SewerCheck check, string clueFlag, float x)
        {
            Id = id;
            Label = label;
            Topic = topic;
            Check = check;
            ClueFlag = clueFlag;
            X = x;
        }
    }
}
