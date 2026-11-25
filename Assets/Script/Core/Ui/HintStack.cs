using System.Collections.Generic;
using System.Text;

namespace SCoW.Core.Ui
{
    /// <summary>마지막으로 입력이 들어온 장치 종류(UX-1). GameInput.LastDevice 가 채운다.</summary>
    public enum InputDeviceKind
    {
        Keyboard,
        Mouse,
        Gamepad,
    }

    /// <summary>화면 하단 키 안내에 쓰는 동작 이름. 글리프는 HintGlyphs 가 장치별로 정한다.</summary>
    public enum HintAction
    {
        Confirm,
        Cancel,
        Navigate,
        Page,
        Adjust,
        Menu,
        Notebook,
        Present,
        Backlog,
        Scroll,
    }

    public struct Hint
    {
        public readonly HintAction Action;
        public readonly string Label;

        public Hint(HintAction action, string label)
        {
            Action = action;
            Label = label;
        }
    }

    /// <summary>동작 → 장치별 글리프 글자. 입력 에셋 바인딩(SCoWInput) 기준. 바인딩이 바뀌면 여기만 고친다.</summary>
    public static class HintGlyphs
    {
        public static string Get(HintAction action, InputDeviceKind device)
        {
            bool pad = device == InputDeviceKind.Gamepad;
            bool mouse = device == InputDeviceKind.Mouse;
            switch (action)
            {
                case HintAction.Confirm:
                    return pad ? "A" : (mouse ? "Click" : "E");
                case HintAction.Cancel:
                    return pad ? "B" : "Esc";
                case HintAction.Navigate:
                    return pad ? "D-Pad" : "W/S";
                case HintAction.Page:
                    return pad ? "LB/RB" : "A/D";
                case HintAction.Adjust:
                    return pad ? "D-Pad" : "A/D";
                case HintAction.Menu:
                    return pad ? "Start" : "Esc";
                case HintAction.Notebook:
                    return pad ? "Y" : "1";
                case HintAction.Present:
                    return pad ? "Y" : "F";
                case HintAction.Backlog:
                    return pad ? "LT" : "B";
                case HintAction.Scroll:
                    return mouse ? "Wheel" : (pad ? "D-Pad" : "W/S");
                default:
                    return "?";
            }
        }
    }

    /// <summary>
    /// 키 안내 스택(UX-1). 패널이 열릴 때 Push(owner, 안내들), 닫힐 때 Remove(owner). 맨 위 항목만 보인다.
    /// 순수 C#: 그리는 일은 View(HintBar)가 한다. Version 이 바뀔 때만 다시 그리면 된다.
    /// </summary>
    public sealed class HintStack
    {
        private struct Entry
        {
            public object Owner;
            public Hint[] Hints;
        }

        private readonly List<Entry> _items = new List<Entry>(4);
        private readonly StringBuilder _sb = new StringBuilder(96);

        public int Version { get; private set; }

        public int Count
        {
            get { return _items.Count; }
        }

        /// <summary>owner 의 안내를 맨 위에 올린다. 이미 있으면 내용을 바꾸고 맨 위로 옮긴다.</summary>
        public void Push(object owner, Hint[] hints)
        {
            if (owner == null || hints == null)
            {
                return;
            }
            RemoveEntry(owner);
            Entry e;
            e.Owner = owner;
            e.Hints = hints;
            _items.Add(e);
            Version++;
        }

        public bool Remove(object owner)
        {
            if (!RemoveEntry(owner))
            {
                return false;
            }
            Version++;
            return true;
        }

        public void Clear()
        {
            if (_items.Count > 0)
            {
                _items.Clear();
                Version++;
            }
        }

        public bool IsTop(object owner)
        {
            return _items.Count > 0 && ReferenceEquals(_items[_items.Count - 1].Owner, owner);
        }

        private bool RemoveEntry(object owner)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (ReferenceEquals(_items[i].Owner, owner))
                {
                    _items.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>맨 위 안내를 "[E] Select    [Esc] Back" 꼴로 만든다. 비어 있으면 빈 문자열.</summary>
        public string Format(InputDeviceKind device)
        {
            _sb.Length = 0;
            if (_items.Count == 0)
            {
                return string.Empty;
            }
            Hint[] hints = _items[_items.Count - 1].Hints;
            for (int i = 0; i < hints.Length; i++)
            {
                if (i > 0)
                {
                    _sb.Append("    ");
                }
                _sb.Append('[').Append(HintGlyphs.Get(hints[i].Action, device)).Append("] ").Append(hints[i].Label);
            }
            return _sb.ToString();
        }
    }
}
