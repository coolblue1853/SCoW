using System.Text;

namespace SCoW.Core.Story
{
    public struct BacklogEntry
    {
        public string Speaker;
        public string Text;
    }

    /// <summary>지나간 대사 기록(링버퍼). 가득 차면 가장 오래된 것부터 덮어쓴다. [0] = 가장 오래된 것.</summary>
    public sealed class Backlog
    {
        public const int DefaultCapacity = 100;

        private readonly BacklogEntry[] _items;
        private int _start;
        private int _count;

        public Backlog(int capacity)
        {
            _items = new BacklogEntry[capacity < 1 ? 1 : capacity];
        }

        public Backlog() : this(DefaultCapacity)
        {
        }

        public int Capacity
        {
            get { return _items.Length; }
        }

        public int Count
        {
            get { return _count; }
        }

        public BacklogEntry this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    throw new System.ArgumentOutOfRangeException("index");
                }
                return _items[(_start + index) % _items.Length];
            }
        }

        public void Add(string speaker, string text)
        {
            BacklogEntry e;
            e.Speaker = speaker ?? "";
            e.Text = text ?? "";
            if (_count < _items.Length)
            {
                _items[(_start + _count) % _items.Length] = e;
                _count++;
                return;
            }
            _items[_start] = e;
            _start = (_start + 1) % _items.Length;
        }

        public void Clear()
        {
            _start = 0;
            _count = 0;
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i] = default(BacklogEntry);
            }
        }

        /// <summary>TMP·Text Animator 태그(꺾쇠로 감싼 부분)를 뺀 순수 글. 닫히지 않은 여는 꺾쇠는 글자로 둔다.</summary>
        public static string Plain(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            StringBuilder sb = new StringBuilder(s.Length);
            int i = 0;
            while (i < s.Length)
            {
                char c = s[i];
                if (c == '<')
                {
                    int close = s.IndexOf('>', i + 1);
                    if (close > i)
                    {
                        i = close + 1;
                        continue;
                    }
                }
                sb.Append(c);
                i++;
            }
            return sb.ToString();
        }
    }
}
