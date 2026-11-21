using System;
using System.Collections.Generic;

namespace SCoW.Core.State
{
    /// <summary>
    /// 저장 가능한 진행 상태(순수 데이터). 키는 옛 DataBaseManager 정적 필드명 그대로.
    /// 값이 기본(false/0/"")이면 보관하지 않아 Equals 가 "보관 여부" 에 흔들리지 않는다.
    /// </summary>
    [Serializable]
    public sealed class GameState : IEquatable<GameState>
    {
        public const int CurrentSchemaVersion = 1;

        /// <summary>값이 실제로 바뀐 키(플래그·카운터·텍스트 공통, C-2 P13). 월드 동기화·대사 장소 갱신이 구독한다. 직렬화·복제·비교 대상이 아니다. 상태를 통째로 갈아 끼울 때의 알림은 GameRoot 가 맡는다.</summary>
        [field: NonSerialized]
        public event Action<string> Changed;

        public int SchemaVersion = CurrentSchemaVersion;

        /// <summary>true 인 플래그 키만 보관. 없으면 false.</summary>
        public HashSet<string> Flags = new HashSet<string>();

        /// <summary>0 이 아닌 카운터만 보관.</summary>
        public Dictionary<string, int> Counters = new Dictionary<string, int>();

        /// <summary>빈 문자열이 아닌 텍스트만 보관.</summary>
        public Dictionary<string, string> Texts = new Dictionary<string, string>();

        /// <summary>새 게임 기본값(옛 필드 초기화식과 동일).</summary>
        public static GameState New()
        {
            GameState s = new GameState();
            foreach (string k in FlagKeys.DefaultTrue)
            {
                s.Flags.Add(k);
            }
            foreach (KeyValuePair<string, int> kv in CounterKeys.NonZeroDefaults)
            {
                s.Counters[kv.Key] = kv.Value;
            }
            foreach (KeyValuePair<string, string> kv in TextKeys.NonEmptyDefaults)
            {
                s.Texts[kv.Key] = kv.Value;
            }
            return s;
        }

        public bool GetFlag(string key)
        {
            return Flags.Contains(key);
        }

        public void SetFlag(string key, bool value)
        {
            bool changed = value ? Flags.Add(key) : Flags.Remove(key);
            if (changed)
            {
                RaiseChanged(key);
            }
        }

        public int GetCounter(string key)
        {
            int v;
            return Counters.TryGetValue(key, out v) ? v : 0;
        }

        public void SetCounter(string key, int value)
        {
            if (GetCounter(key) == value)
            {
                return;
            }
            if (value == 0)
            {
                Counters.Remove(key);
            }
            else
            {
                Counters[key] = value;
            }
            RaiseChanged(key);
        }

        public string GetText(string key)
        {
            string v;
            return Texts.TryGetValue(key, out v) ? v : string.Empty;
        }

        public void SetText(string key, string value)
        {
            if (GetText(key) == (value ?? string.Empty))
            {
                return;
            }
            if (string.IsNullOrEmpty(value))
            {
                Texts.Remove(key);
            }
            else
            {
                Texts[key] = value;
            }
            RaiseChanged(key);
        }

        private void RaiseChanged(string key)
        {
            Action<string> handler = Changed;
            if (handler != null)
            {
                handler(key);
            }
        }

        public GameState Clone()
        {
            GameState c = new GameState();
            c.SchemaVersion = SchemaVersion;
            c.Flags = new HashSet<string>(Flags);
            c.Counters = new Dictionary<string, int>(Counters);
            c.Texts = new Dictionary<string, string>(Texts);
            return c;
        }

        public bool Equals(GameState other)
        {
            if (other == null || SchemaVersion != other.SchemaVersion)
            {
                return false;
            }
            if (!Flags.SetEquals(other.Flags) || Counters.Count != other.Counters.Count || Texts.Count != other.Texts.Count)
            {
                return false;
            }
            foreach (KeyValuePair<string, int> kv in Counters)
            {
                int v;
                if (!other.Counters.TryGetValue(kv.Key, out v) || v != kv.Value)
                {
                    return false;
                }
            }
            foreach (KeyValuePair<string, string> kv in Texts)
            {
                string v;
                if (!other.Texts.TryGetValue(kv.Key, out v) || v != kv.Value)
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GameState);
        }

        public override int GetHashCode()
        {
            return SchemaVersion ^ Flags.Count ^ (Counters.Count << 8) ^ (Texts.Count << 16);
        }
    }
}
