using System.Collections.Generic;
using SCoW.Core.Rules;
using SCoW.Core.Story;

namespace SCoW.Core.State
{
    /// <summary>파티 세이브 키(Party.md 6절). 전부 GameState 의 Flags/Counters/Texts 에 얹는다(SaveCodec 변경 없음). 추가만 하고 바꾸지 않는다.</summary>
    public static class PartyKeys
    {
        public const string Prefix = "party.";
        public const string Order = "party.order";

        public static string Joined(string id) { return Prefix + id + ".joined"; }
        public static string Dead(string id) { return Prefix + id + ".dead"; }
        public static string Declined(string id) { return Prefix + id + ".declined"; }
        public static string Standby(string id) { return Prefix + id + ".standby"; }
        public static string Hp(string id) { return Prefix + id + ".hp"; }
        public static string San(string id) { return Prefix + id + ".san"; }
        public static string Status(string id) { return Prefix + id + ".status"; }
        public static string BackAt(string id) { return Prefix + id + ".backAt"; }
    }

    /// <summary>GameState 위의 파티 읽기·쓰기. 키가 없는 구세이브는 파티 없음으로 읽힌다.
    /// 합류 후의 0 은 진짜 0 이다(Join 이 hp/san 을 최대로 기록하고, 읽기는 합류 여부만 본다).</summary>
    public static class PartyState
    {
        public static bool IsJoined(GameState s, string id) { return s.GetFlag(PartyKeys.Joined(id)); }
        public static bool IsDead(GameState s, string id) { return s.GetFlag(PartyKeys.Dead(id)); }
        public static bool IsDeclined(GameState s, string id) { return s.GetFlag(PartyKeys.Declined(id)); }
        public static bool IsStandby(GameState s, string id) { return s.GetFlag(PartyKeys.Standby(id)); }

        /// <summary>합류. HP·SAN 을 최대로 채우고 상태 Ready, 거절 해제, 대열 끝에 추가(처음이면 파비안 먼저).</summary>
        public static void Join(GameState s, CompanionDef def)
        {
            GameStateAccess a = new GameStateAccess(s);
            Join(a, a, def);
        }

        /// <summary>스토리 효과(join:id)용. 읽기·쓰기 창구만 쓴다.</summary>
        public static void Join(IStateReader r, IStateWriter w, CompanionDef def)
        {
            w.SetFlag(PartyKeys.Joined(def.Id), true);
            w.SetFlag(PartyKeys.Declined(def.Id), false);
            w.SetFlag(PartyKeys.Standby(def.Id), false);
            w.SetCounter(PartyKeys.Hp(def.Id), def.MaxHp);
            w.SetCounter(PartyKeys.San(def.Id), def.MaxSan);
            w.SetCounter(PartyKeys.Status(def.Id), (int)UnitStatus.Ready);
            w.SetCounter(PartyKeys.BackAt(def.Id), 0);
            List<string> order = ParseOrder(r.GetText(PartyKeys.Order));
            if (order.Count == 0)
            {
                order.Add(CompanionDefs.LeaderId);
            }
            if (!order.Contains(def.Id))
            {
                order.Add(def.Id);
            }
            w.SetText(PartyKeys.Order, string.Join(",", order));
        }

        /// <summary>영구 사망. 합류 기록은 남기고 활성 목록에서만 빠진다.</summary>
        public static void Kill(GameState s, string id)
        {
            s.SetFlag(PartyKeys.Dead(id), true);
            s.SetCounter(PartyKeys.Hp(id), 0);
            s.SetCounter(PartyKeys.Status(id), (int)UnitStatus.Fallen);
        }

        public static int GetHp(GameState s, string id) { return IsJoined(s, id) ? s.GetCounter(PartyKeys.Hp(id)) : 0; }
        public static int GetSan(GameState s, string id) { return IsJoined(s, id) ? s.GetCounter(PartyKeys.San(id)) : 0; }

        public static void SetHp(GameState s, CompanionDef def, int value)
        {
            s.SetCounter(PartyKeys.Hp(def.Id), Clamp(value, def.MaxHp));
        }

        public static void SetSan(GameState s, CompanionDef def, int value)
        {
            s.SetCounter(PartyKeys.San(def.Id), Clamp(value, def.MaxSan));
        }

        public static UnitStatus GetStatus(GameState s, string id)
        {
            return IsJoined(s, id) ? (UnitStatus)s.GetCounter(PartyKeys.Status(id)) : UnitStatus.Ready;
        }

        public static void SetStatus(GameState s, string id, UnitStatus status)
        {
            s.SetCounter(PartyKeys.Status(id), (int)status);
        }

        /// <summary>대열 순서(파비안 포함, 콤마 목록). 없으면 빈 목록.</summary>
        public static List<string> GetOrder(GameState s)
        {
            return ParseOrder(s.GetText(PartyKeys.Order));
        }

        private static List<string> ParseOrder(string text)
        {
            List<string> list = new List<string>();
            if (string.IsNullOrEmpty(text))
            {
                return list;
            }
            string[] parts = text.Split(',');
            for (int i = 0; i < parts.Length; i++)
            {
                string p = parts[i].Trim();
                if (p.Length > 0 && !list.Contains(p))
                {
                    list.Add(p);
                }
            }
            return list;
        }

        public static void SetOrder(GameState s, IList<string> order)
        {
            s.SetText(PartyKeys.Order, string.Join(",", order));
        }

        /// <summary>전투·탐색에 나설 동료(합류했고 죽지 않은 사람). 대열 순서를 따르고, 목록에 없는 합류자는 뒤에 붙인다.</summary>
        public static List<CompanionDef> ActiveCompanions(GameState s)
        {
            List<CompanionDef> result = new List<CompanionDef>();
            List<string> order = GetOrder(s);
            for (int i = 0; i < order.Count; i++)
            {
                AddIfActive(s, CompanionDefs.Get(order[i]), result);
            }
            for (int i = 0; i < CompanionDefs.All.Length; i++)
            {
                AddIfActive(s, CompanionDefs.All[i], result);
            }
            return result;
        }

        private static void AddIfActive(GameState s, CompanionDef def, List<CompanionDef> result)
        {
            if (def != null && IsJoined(s, def.Id) && !IsDead(s, def.Id) && !result.Contains(def))
            {
                result.Add(def);
            }
        }

        private static int Clamp(int value, int max)
        {
            return value < 0 ? 0 : (value > max ? max : value);
        }
    }
}
