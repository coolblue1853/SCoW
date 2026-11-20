using System.Collections.Generic;

namespace SCoW.Core.Ui
{
    /// <summary>스택에 올라가는 패널 종류. 순서 = 의미 없음(저장하지 않는다).</summary>
    public enum PanelKind
    {
        Setting,
        Intel,
        Map,
        Inventory,
        Shop,
        Rollet,
        SlotMenu,
        Backlog,
    }

    /// <summary>
    /// 열린 패널 스택(P5). 옛 DataBaseManager.isOpenUi(bool 하나) 대체. 맨 위 하나만 Cancel 로 닫힌다.
    /// 순수 C#: 같은 프레임에 여러 번 닫히는 것을 막는 일(프레임 소비)은 App 의 UiGate 가 맡는다.
    /// </summary>
    public sealed class UiStack
    {
        private readonly List<PanelKind> _items = new List<PanelKind>(4);

        public int Count
        {
            get { return _items.Count; }
        }

        public bool Any
        {
            get { return _items.Count > 0; }
        }

        /// <summary>맨 위 패널. 비어 있으면 false.</summary>
        public bool TryPeek(out PanelKind top)
        {
            if (_items.Count == 0)
            {
                top = default(PanelKind);
                return false;
            }
            top = _items[_items.Count - 1];
            return true;
        }

        public bool IsTop(PanelKind kind)
        {
            PanelKind top;
            return TryPeek(out top) && top == kind;
        }

        public bool Contains(PanelKind kind)
        {
            return _items.Contains(kind);
        }

        /// <summary>
        /// 대사 진행 입력(E·클릭)을 삼켜야 하는 패널이 열려 있나. 판정창(Rollet)은 자체 입력으로 흐름을 끌고 가므로 제외.
        /// </summary>
        public bool BlocksDialogueInput
        {
            get
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i] != PanelKind.Rollet)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>올린다. 이미 있으면 맨 위로 옮긴다(중복 없음).</summary>
        public void Push(PanelKind kind)
        {
            _items.Remove(kind);
            _items.Add(kind);
        }

        /// <summary>스택 어디에 있든 뺀다. 있었으면 true.</summary>
        public bool Remove(PanelKind kind)
        {
            return _items.Remove(kind);
        }

        /// <summary>맨 위 하나만 뺀다. 비어 있으면 false.</summary>
        public bool PopTop(out PanelKind popped)
        {
            if (!TryPeek(out popped))
            {
                return false;
            }
            _items.RemoveAt(_items.Count - 1);
            return true;
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
