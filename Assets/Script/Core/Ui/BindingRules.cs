using System;
using System.Collections.Generic;

namespace SCoW.Core.Ui
{
    /// <summary>입력 바인딩 한 칸(UX-12). 엔진 없는 순수 값: GameInput 이 입력 에셋에서 채운다.</summary>
    public struct BindingEntry
    {
        public string Map;
        public string Action;
        /// <summary>"Keyboard"(키보드·마우스) 또는 "Gamepad".</summary>
        public string Scheme;
        /// <summary>에셋 원본 경로(리바인딩 전).</summary>
        public string DefaultPath;
        /// <summary>지금 유효한 경로(리바인딩 반영).</summary>
        public string Path;

        public BindingEntry(string map, string action, string scheme, string defaultPath, string path)
        {
            Map = map;
            Action = action;
            Scheme = scheme;
            DefaultPath = defaultPath;
            Path = path;
        }
    }

    /// <summary>같은 맵·같은 장치 종류에서 서로 다른 동작이 같은 컨트롤을 쓰는 한 쌍.</summary>
    public struct BindingConflict
    {
        public string ActionA;
        public string ActionB;
        public string Scheme;
        public string Path;

        public BindingConflict(string a, string b, string scheme, string path)
        {
            ActionA = a;
            ActionB = b;
            Scheme = scheme;
            Path = path;
        }
    }

    /// <summary>
    /// 키 충돌 검출(UX-12). 같은 맵·같은 장치 종류만 본다: Gameplay 와 UI 맵은 동시에 켜져 있지만 A/D 등 기본 표에서도
    /// 맵 사이 공유가 있어 전면 검출은 노이즈다(맵 사이 쌍 경고는 후속 과제). "새 충돌"만 돌려준다: 출시 기본 바인딩에도 이미 같은 키를 나눠 쓰는 쌍
    /// (F=포털/제시, Q=할 일/교체, Esc=취소/메뉴 등 상황이 달라 동시에 쓰이지 않는 것)은 기준선으로 보고 경고하지 않는다.
    /// </summary>
    public static class BindingRules
    {
        public static List<BindingConflict> NewConflicts(IList<BindingEntry> entries)
        {
            List<BindingConflict> now = Find(entries, false);
            List<BindingConflict> baseline = Find(entries, true);
            HashSet<string> known = new HashSet<string>();
            for (int i = 0; i < baseline.Count; i++)
            {
                known.Add(Key(baseline[i]));
            }
            List<BindingConflict> result = new List<BindingConflict>();
            for (int i = 0; i < now.Count; i++)
            {
                if (!known.Contains(Key(now[i])))
                {
                    result.Add(now[i]);
                }
            }
            return result;
        }

        private static string Key(BindingConflict c)
        {
            string a = c.ActionA;
            string b = c.ActionB;
            if (string.CompareOrdinal(a, b) > 0)
            {
                string t = a;
                a = b;
                b = t;
            }
            return a + "|" + b + "|" + c.Scheme + "|" + c.Path.ToLowerInvariant();
        }

        private static List<BindingConflict> Find(IList<BindingEntry> entries, bool useDefault)
        {
            List<BindingConflict> list = new List<BindingConflict>();
            for (int i = 0; i < entries.Count; i++)
            {
                string pi = useDefault ? entries[i].DefaultPath : entries[i].Path;
                if (string.IsNullOrEmpty(pi))
                {
                    continue;
                }
                for (int j = i + 1; j < entries.Count; j++)
                {
                    if (entries[i].Map != entries[j].Map || entries[i].Scheme != entries[j].Scheme || entries[i].Action == entries[j].Action)
                    {
                        continue;
                    }
                    string pj = useDefault ? entries[j].DefaultPath : entries[j].Path;
                    if (string.Equals(pi, pj, StringComparison.OrdinalIgnoreCase))
                    {
                        list.Add(new BindingConflict(entries[i].Action, entries[j].Action, entries[i].Scheme, pi));
                    }
                }
            }
            return list;
        }
    }

    /// <summary>컨트롤 경로(예 "&lt;Keyboard&gt;/e")를 키 안내용 짧은 글자로 바꾼다(UX-12).</summary>
    public static class BindingLabel
    {
        public static string Friendly(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "-";
            }
            int close = path.IndexOf('>');
            if (path[0] != '<' || close < 0 || close + 2 > path.Length)
            {
                return path;
            }
            string device = path.Substring(1, close - 1);
            string rest = path.Substring(close + 2);
            if (device == "Keyboard")
            {
                return Keyboard(rest);
            }
            if (device == "Gamepad")
            {
                return Gamepad(rest);
            }
            if (device == "Mouse")
            {
                return Mouse(rest);
            }
            return rest;
        }

        private static string Keyboard(string k)
        {
            switch (k)
            {
                case "escape": return "Esc";
                case "leftCtrl": return "LCtrl";
                case "rightCtrl": return "RCtrl";
                case "leftShift": return "LShift";
                case "rightShift": return "RShift";
                case "leftAlt": return "LAlt";
                case "rightAlt": return "RAlt";
                case "upArrow": return "Up";
                case "downArrow": return "Down";
                case "leftArrow": return "Left";
                case "rightArrow": return "Right";
                case "enter": return "Enter";
                case "space": return "Space";
                case "tab": return "Tab";
                case "backspace": return "Backspace";
            }
            if (k.Length == 1)
            {
                return k.ToUpperInvariant();
            }
            return char.ToUpperInvariant(k[0]) + k.Substring(1);
        }

        private static string Gamepad(string k)
        {
            switch (k)
            {
                case "buttonSouth": return "A";
                case "buttonEast": return "B";
                case "buttonWest": return "X";
                case "buttonNorth": return "Y";
                case "leftShoulder": return "LB";
                case "rightShoulder": return "RB";
                case "leftTrigger": return "LT";
                case "rightTrigger": return "RT";
                case "start": return "Start";
                case "select": return "Select";
                case "dpad/up": return "D-Pad Up";
                case "dpad/down": return "D-Pad Down";
                case "dpad/left": return "D-Pad Left";
                case "dpad/right": return "D-Pad Right";
                case "leftStickPress": return "L3";
                case "rightStickPress": return "R3";
            }
            return k;
        }

        private static string Mouse(string k)
        {
            switch (k)
            {
                case "leftButton": return "Mouse L";
                case "rightButton": return "Mouse R";
                case "middleButton": return "Mouse M";
                case "scroll/up": return "Wheel Up";
                case "scroll/down": return "Wheel Down";
            }
            return k;
        }
    }
}
