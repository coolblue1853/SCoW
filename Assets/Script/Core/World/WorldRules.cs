using System.Collections.Generic;
using SCoW.Core.State;

namespace SCoW.Core.World
{
    /// <summary>
    /// 상태 → 월드 오브젝트 목표 상태 표(순수 C#, C-2 P13). 옛 TimeManagere.Update·BulbCheck 본문을 같은 순서·같은 의미로 옮겼다.
    /// 좌표(localPosition)는 옛 코드의 값 그대로다. 같은 프레임에 한 번 돌려도, 여러 번 돌려도 같은 결과가 나온다(옛 매 프레임 실행과 동치).
    /// 의미 보존: 옛 식의 `A || B && C` 는 괄호 없이 `A || (B && C)` 로 평가되던 그대로 둔다(시간대 1 은 문 상태와 무관하게 매번 적용, 시간대 3 도 마찬가지).
    /// </summary>
    public static class WorldRules
    {
        /// <summary>이 키가 바뀌면 월드를 다시 평가해야 한다(WorldBinder 가 변경 통지를 거를 때 쓴다).</summary>
        public static bool Affects(string key)
        {
            return key != null && AffectingKeys.Contains(key);
        }

        private static readonly HashSet<string> AffectingKeys = new HashSet<string>
        {
            CounterKeys.timeCount,
            WorldIds.RefreshKey,
            // 장소가 바뀌면 안전망으로 다시 평가한다(다른 스크립트가 월드 오브젝트를 되살린 경우, 옛 매 프레임 실행이 덮어쓰던 것에 대응).
            TextKeys.nowPlace,
            FlagKeys.endingDoorCheck, FlagKeys.deadBodyGotoSwere, FlagKeys.sewerPicingSucc, FlagKeys.sewerageKey,
            FlagKeys.blackRechSuccCanTalck, FlagKeys.sewerDoorOpen, FlagKeys.sewerDoorOpenDeftness, FlagKeys.sewerDoorOpenCheck,
            FlagKeys.tryBrokenWindow, FlagKeys.tryObserLokcer,
            FlagKeys.albertDeathKeyword, FlagKeys.daveDeathKeyword, FlagKeys.swainDeathKeyword
        };

        public static void Apply(GameState s, IWorldScene w)
        {
            int t = s.GetCounter(CounterKeys.timeCount);
            int phase = WorldClock.Phase(t);

            // 밤 전구(옛 BulbCheck)
            bool bulbOn = w.IsActive(WorldIds.DetectiveNightBulb);
            if (!bulbOn && WorldClock.IsNight(t))
            {
                w.SetActive(WorldIds.DetectiveNightBulb, true);
            }
            else if (bulbOn && !WorldClock.IsNight(t))
            {
                w.SetActive(WorldIds.DetectiveNightBulb, false);
            }

            if (w.IsActive(WorldIds.EndDoorDetective) == false && s.GetFlag(FlagKeys.endingDoorCheck))
            {
                w.SetActive(WorldIds.EndDoorDetective, true);
            }
            if (w.IsActive(WorldIds.Manhole) == false && s.GetFlag(FlagKeys.deadBodyGotoSwere))
            {
                w.SetActive(WorldIds.Manhole, true);
            }
            if (w.IsActive(WorldIds.RiverActive) && (s.GetFlag(FlagKeys.sewerPicingSucc) || s.GetFlag(FlagKeys.sewerageKey)))
            {
                w.SetActive(WorldIds.RiverPotal, true);
                w.SetActive(WorldIds.RiverActive, false);
            }
            if (s.GetFlag(FlagKeys.blackRechSuccCanTalck) && w.GetCanKeyword(WorldIds.BlackWell) == false)
            {
                w.SetCanKeyword(WorldIds.BlackWell, true);
            }

            ApplySewerDoor(s, w, phase);

            if (t >= 6 && w.LocalY(WorldIds.AfNoonEvent) > 30)
            {
                w.MoveTo(WorldIds.BfNoonEvent, 0, 60, 0);
                w.MoveTo(WorldIds.AfNoonEvent, 0, 0, 0);
                w.MoveTo(WorldIds.PoliceLine, -58.1f, 4.06f, 0);
                w.SetClientsHouseRoomMessy();
            }
            if (t >= 7 && w.IsActive(WorldIds.Witness))
            {
                w.SetCanJudge(WorldIds.Meave, true);
                w.SetActive(WorldIds.Witness, false);
                w.SetActive(WorldIds.DeadBody, false);
                w.MoveTo(WorldIds.Site, -250.6001f, 0.4400024f, 0);
                w.MoveTo(WorldIds.Kane, 253.27f, -79.6f, 0);
                w.MoveTo(WorldIds.Student, -41.67f, 40.9f, 0);
            }
            if (t >= 11 && w.GetCanJudge(WorldIds.Meave))
            {
                w.SetCanJudge(WorldIds.Meave, false);
            }
            if (t == 8 && w.IsActive(WorldIds.BarPeople1))
            {
                w.SetActive(WorldIds.BarPeople1, false);
                w.SetActive(WorldIds.BarPeople2, false);
            }
            if (s.GetFlag(FlagKeys.albertDeathKeyword) && w.IsActive(WorldIds.Albert) && phase == 0 && t >= 8)
            {
                s.SetFlag(FlagKeys.thirdDayPoliceADialog, true);
                s.SetFlag(FlagKeys.thirdDayPoliceBDialog, true);
                w.SetActive(WorldIds.Albert, false);
            }
            if (s.GetFlag(FlagKeys.daveDeathKeyword) && w.IsActive(WorldIds.Dave) && phase == 0)
            {
                s.SetFlag(FlagKeys.afterDaveDeathFirstSlum, true);
                w.SetActive(WorldIds.Dave, false);
            }
            if (s.GetFlag(FlagKeys.swainDeathKeyword) && w.IsActive(WorldIds.Swain) && phase == 0)
            {
                w.MoveTo(WorldIds.Journalist, -61.37f, 3.4f, 0);
                w.SetActive(WorldIds.Swain, false);
            }

            float lightTime;
            if (WorldClock.TryLightTime(t, out lightTime))
            {
                w.SetLightTime(lightTime);
            }
        }

        // 하수도 사무실 문: 아침·낮에는 열린 상태(문 포털 y=5, 대사 y=33), 저녁·밤에는 열림 여부에 따라 위치를 바꾼다.
        private static void ApplySewerDoor(GameState s, IWorldScene w, int phase)
        {
            bool open = s.GetFlag(FlagKeys.sewerDoorOpen);
            // 옛 식: (phase == 1) || (phase == 2 && 문 닫힘)
            if (phase == 1 || (phase == 2 && open == false))
            {
                s.SetFlag(FlagKeys.sewerDoorOpen, true);
                w.MoveTo(WorldIds.SewerDoorPotal, -65.10986f, 5, 0);
                w.MoveTo(WorldIds.SewerDoorDialog, -65.10986f, 33, 0);
                w.MoveTo(WorldIds.BlackWellOb, -224.6f, 4.08f, 0);
                w.SetCanJudge(WorldIds.SewerOfficeWindow, false);
                w.SetCanJudge(WorldIds.SewerOfficeLocker, false);
            }
            // 옛 식: (phase == 3) || (phase == 0 && 문 열림). 앞 블록이 문 플래그를 바꿨을 수 있어 다시 읽는다.
            if (phase == 3 || (phase == 0 && s.GetFlag(FlagKeys.sewerDoorOpen)))
            {
                w.MoveTo(WorldIds.BlackWellOb, -224.6f, 29.6f, 0);
                s.SetFlag(FlagKeys.sewerDoorOpen, false);
                if (s.GetFlag(FlagKeys.sewerDoorOpenDeftness))
                {
                    w.MoveTo(WorldIds.SewerDoorPotal, -65.10986f, 5, 0);
                    w.MoveTo(WorldIds.SewerDoorDialog, -65.10986f, 33, 0);
                }
                else
                {
                    w.MoveTo(WorldIds.SewerDoorPotal, -65.10986f, 33, 0);
                    w.MoveTo(WorldIds.SewerDoorDialog, -65.10986f, 5, 0);
                }
                w.SetCanJudge(WorldIds.SewerOfficeWindow, !s.GetFlag(FlagKeys.tryBrokenWindow));
                w.SetCanJudge(WorldIds.SewerOfficeLocker, !s.GetFlag(FlagKeys.tryObserLokcer));
            }
            if (s.GetFlag(FlagKeys.sewerDoorOpenCheck))
            {
                s.SetFlag(FlagKeys.sewerDoorOpenCheck, false);
                w.MoveTo(WorldIds.SewerDoorPotal, -65.10986f, 5, 0);
                w.MoveTo(WorldIds.SewerDoorDialog, -65.10986f, 33, 0);
            }
        }
    }
}
