using System.Collections.Generic;
using SCoW.Core.Rules;
using SCoW.Core.State;

namespace SCoW.Core.Sewer
{
    /// <summary>하수도 세이브 키. 전부 GameState 의 Counters/Texts 에 얹는다(SaveCodec 변경 없음). 추가만 하고 바꾸지 않는다.</summary>
    public static class SewerKeys
    {
        public const string Room = "sewer.room";
        public const string Visited = "sewer.visited";
        public const string Defeated = "sewer.defeated";
        public const string Seed = "sewer.seed";
        /// <summary>조사한 포인트 목록(쉼표, "방ID:포인트ID"). S2 에서 추가.</summary>
        public const string Looked = "sewer.looked";
        public const string EncPrefix = "sewer.enc.";

        public static string Enc(string roomId) { return EncPrefix + roomId; }
    }

    /// <summary>GameState 위의 하수도 진행(현재 방·방문·처치·조우 번호·외형 시드). 키가 없는 구세이브는 "하수도 상태 없음"으로 읽힌다.</summary>
    public static class SewerState
    {
        public static string Room(GameState s) { return s.GetText(SewerKeys.Room); }

        public static bool HasRoom(GameState s) { return !string.IsNullOrEmpty(Room(s)); }

        public static void SetRoom(GameState s, string roomId) { s.SetText(SewerKeys.Room, roomId); }

        public static int Seed(GameState s) { return s.GetCounter(SewerKeys.Seed); }

        /// <summary>시드가 없으면 rng 로 정해 기록한다(0 은 "없음"이라 1 이상). 있으면 그대로.</summary>
        public static int EnsureSeed(GameState s, IRng rng)
        {
            int seed = Seed(s);
            if (seed == 0)
            {
                seed = rng.Next(1, 1000000);
                s.SetCounter(SewerKeys.Seed, seed);
            }
            return seed;
        }

        public static bool IsVisited(GameState s, string roomId) { return InList(s.GetText(SewerKeys.Visited), roomId); }

        public static void Visit(GameState s, string roomId) { AddTo(s, SewerKeys.Visited, roomId); }

        public static bool IsDefeated(GameState s, string roomId) { return InList(s.GetText(SewerKeys.Defeated), roomId); }

        public static void Defeat(GameState s, string roomId) { AddTo(s, SewerKeys.Defeated, roomId); }

        public static bool IsLooked(GameState s, string roomId, string pointId) { return InList(s.GetText(SewerKeys.Looked), roomId + ":" + pointId); }

        /// <summary>포인트를 조사했다고 기록한다(판정 성공 여부와 무관, 한 번만 시도).</summary>
        public static void Look(GameState s, string roomId, string pointId) { AddTo(s, SewerKeys.Looked, roomId + ":" + pointId); }

        /// <summary>방의 조우 번호(1~3). 아직 안 나왔으면 0.</summary>
        public static int EncounterOf(GameState s, string roomId) { return s.GetCounter(SewerKeys.Enc(roomId)); }

        public static void SetEncounter(GameState s, string roomId, int no) { s.SetCounter(SewerKeys.Enc(roomId), no); }

        /// <summary>지금까지 나온 조우 번호의 최댓값.</summary>
        public static int MaxEncounter(GameState s)
        {
            int max = 0;
            for (int i = 1; i <= SewerGraph.EncounterCount; i++)
            {
                int no = EncounterOf(s, SewerGraph.EncounterRoom(i));
                if (no > max)
                {
                    max = no;
                }
            }
            return max;
        }

        /// <summary>하수도 진행을 지운다(새 게임 외에는 쓰지 않는다).</summary>
        public static void Clear(GameState s)
        {
            s.SetText(SewerKeys.Room, null);
            s.SetText(SewerKeys.Visited, null);
            s.SetText(SewerKeys.Defeated, null);
            s.SetText(SewerKeys.Looked, null);
            s.SetCounter(SewerKeys.Seed, 0);
            s.SetCounter(SewerRest.RestKey, 0);
            for (int i = 1; i <= SewerGraph.EncounterCount; i++)
            {
                s.SetCounter(SewerKeys.Enc(SewerGraph.EncounterRoom(i)), 0);
                s.SetCounter(SewerRewards.RewardKey(i), 0);
            }
        }

        private static bool InList(string csv, string id)
        {
            if (string.IsNullOrEmpty(csv) || string.IsNullOrEmpty(id))
            {
                return false;
            }
            string[] parts = csv.Split(',');
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == id)
                {
                    return true;
                }
            }
            return false;
        }

        private static void AddTo(GameState s, string key, string id)
        {
            string csv = s.GetText(key);
            if (InList(csv, id))
            {
                return;
            }
            s.SetText(key, string.IsNullOrEmpty(csv) ? id : csv + "," + id);
        }
    }
}
