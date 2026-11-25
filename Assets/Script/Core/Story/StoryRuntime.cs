namespace SCoW.Core.Story
{
    /// <summary>조건 평가가 읽는 상태. GameState 는 GameStateAccess 로 감싼다.</summary>
    public interface IStateReader
    {
        bool GetFlag(string key);
        int GetCounter(string key);
        string GetText(string key);
    }

    /// <summary>효과가 쓰는 상태.</summary>
    public interface IStateWriter
    {
        void SetFlag(string key, bool value);
        void SetCounter(string key, int value);
        void SetText(string key, string value);
    }

    /// <summary>상태 밖 부작용(노드 이동·판정·전투). 엔진(P8)이 구현한다.</summary>
    public interface IStoryHost
    {
        void Goto(string nodeId);
        void StartBattle(string battleId);
        void RequestCheck(string skill, string difficulty, string okNode, string failNode);
    }

    /// <summary>GameState 를 IStateReader/IStateWriter 로 보이게 하는 어댑터.</summary>
    public sealed class GameStateAccess : IStateReader, IStateWriter
    {
        private readonly SCoW.Core.State.GameState _s;

        public GameStateAccess(SCoW.Core.State.GameState s)
        {
            _s = s;
        }

        public bool GetFlag(string key) { return _s.GetFlag(key); }
        public int GetCounter(string key) { return _s.GetCounter(key); }
        public string GetText(string key) { return _s.GetText(key); }
        public void SetFlag(string key, bool value) { _s.SetFlag(key, value); }
        public void SetCounter(string key, int value) { _s.SetCounter(key, value); }
        public void SetText(string key, string value) { _s.SetText(key, value); }
    }

    /// <summary>파서 결과. Error 가 null 이 아니면 Value 는 신뢰하지 않는다.</summary>
    public sealed class ParseOutcome<T>
    {
        public T Value;
        public string Error;
        public bool Ok { get { return Error == null; } }
    }
}
