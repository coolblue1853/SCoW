using System;

namespace SCoW.Core.Save
{
    /// <summary>저장 읽기·쓰기 실패 분류. UI 는 이 값으로 오류 문구를 고른다.</summary>
    public enum SaveError
    {
        None = 0,
        Missing,
        Corrupt,
        NewerVersion,
        IoError,
    }

    /// <summary>슬롯 목록 화면용 요약. state 를 읽지 않고 meta 만으로 그린다.</summary>
    [Serializable]
    public sealed class SaveMeta
    {
        public int SchemaVersion;
        public long SavedAtUtcTicks;
        public long PlayTimeSec;
        public string PlaceId = string.Empty;
        public int TimeCount;

        /// <summary>일차(1부터). 시간대는 timeCount % 4 (1 아침, 2 낮, 3 오후, 0 밤).</summary>
        public int Day
        {
            get { return TimeCount <= 0 ? 1 : (TimeCount - 1) / 4 + 1; }
        }

        public DateTime SavedAtUtc
        {
            get { return new DateTime(SavedAtUtcTicks, DateTimeKind.Utc); }
        }
    }
}
