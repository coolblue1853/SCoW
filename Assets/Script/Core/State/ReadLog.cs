using System.Collections.Generic;
using System.Text;

namespace SCoW.Core.State
{
    /// <summary>읽은 대사 기록. 키 = "스크립트#줄ID". 슬롯과 무관한 전역 값(별도 파일 한 키에 문자열로 보관).</summary>
    public sealed class ReadLog
    {
        private const string Header = "READLOG 1";

        private readonly HashSet<string> _keys = new HashSet<string>();

        public int Count
        {
            get { return _keys.Count; }
        }

        public static string KeyOf(string scriptId, string lineId)
        {
            return (scriptId ?? "") + "#" + (lineId ?? "");
        }

        public bool Contains(string key)
        {
            return key != null && _keys.Contains(key);
        }

        /// <summary>새로 추가됐으면 true.</summary>
        public bool Add(string key)
        {
            if (string.IsNullOrEmpty(key) || key.IndexOf('\n') >= 0)
            {
                return false;
            }
            return _keys.Add(key);
        }

        public void Clear()
        {
            _keys.Clear();
        }

        /// <summary>정렬된 결정적 텍스트(첫 줄 헤더, 이후 키 한 줄씩).</summary>
        public string Encode()
        {
            List<string> list = new List<string>(_keys);
            list.Sort(System.StringComparer.Ordinal);
            StringBuilder sb = new StringBuilder();
            sb.Append(Header);
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append('\n').Append(list[i]);
            }
            return sb.ToString();
        }

        /// <summary>손상·빈 문자열에도 예외 없이 빈 기록을 돌려준다. 헤더를 모르면 통째로 버린다.</summary>
        public static ReadLog Decode(string text)
        {
            ReadLog r = new ReadLog();
            if (string.IsNullOrEmpty(text))
            {
                return r;
            }
            string[] lines = text.Replace("\r", "").Split('\n');
            if (lines.Length == 0 || lines[0] != Header)
            {
                return r;
            }
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Length > 0)
                {
                    r._keys.Add(lines[i]);
                }
            }
            return r;
        }
    }
}
