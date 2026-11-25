using System;
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
        Keyword,
    }

    /// <summary>
    /// 열린 패널 스택(P5). 옛 DataBaseManager.isOpenUi(bool 하나) 대체. 맨 위 하나만 Cancel 로 닫힌다.
    /// 순수 C#: 같은 프레임에 여러 번 닫히는 것을 막는 일(프레임 소비)은 App 의 UiGate 가 맡는다.
    /// </summary>
    public sealed class UiStack
    {
        private struct Closer
        {
            public Action Close;
            public bool MenuToo;
        }

        private readonly List<PanelKind> _items = new List<PanelKind>(4);
        private readonly Dictionary<PanelKind, Closer> _closers = new Dictionary<PanelKind, Closer>();

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

        /// <summary>
        /// 올리면서 닫기 동작을 등록한다(UX-1). Cancel(Esc/패드 B) 은 맨 위 패널의 close 하나만 부른다(TryCloseTop).
        /// menuToo 면 Menu 키도 같은 취소로 본다. 이미 있는 패널을 다시 올리면 맨 위로 옮기고 닫기 동작을 바꾼다.
        /// </summary>
        public void Push(PanelKind kind, Action close, bool menuToo = false)
        {
            Push(kind);
            if (close != null)
            {
                Closer c;
                c.Close = close;
                c.MenuToo = menuToo;
                _closers[kind] = c;
            }
        }

        public bool HasCloser(PanelKind kind)
        {
            return _closers.ContainsKey(kind);
        }

        /// <summary>
        /// 맨 위 패널에 닫기 동작이 등록돼 있고 cancel(또는 menu 이면서 MenuToo)이면 그 하나만 닫는다.
        /// 맨 위가 닫기 미등록이면 아무것도 닫지 않고 false(아래 패널을 건드리지 않는다).
        /// </summary>
        public bool TryCloseTop(bool cancel, bool menu)
        {
            PanelKind top;
            Closer c;
            if (!TryPeek(out top) || !_closers.TryGetValue(top, out c))
            {
                return false;
            }
            if (!(cancel || (menu && c.MenuToo)))
            {
                return false;
            }
            try
            {
                c.Close();
            }
            finally
            {
                Remove(top); // close 가 이미 뺐으면 아무 일도 없다. close 가 던져도 스택이 막히지 않게 한다.
            }
            return true;
        }

        /// <summary>Cancel 입력으로 맨 위 패널 하나를 닫는다.</summary>
        public bool TryCloseTop()
        {
            return TryCloseTop(true, false);
        }

        /// <summary>스택 어디에 있든 뺀다. 있었으면 true.</summary>
        public bool Remove(PanelKind kind)
        {
            _closers.Remove(kind);
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
            _closers.Remove(popped);
            return true;
        }

        public void Clear()
        {
            _items.Clear();
            _closers.Clear();
        }
    }
}
