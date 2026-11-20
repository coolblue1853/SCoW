using System;
using SCoW.Core.Save;
using I2.Loc;
using UnityEngine;

/// <summary>
/// 저장 UI 문구(한글+영문 하드코딩, I2 용어를 늘리지 않는다). 현재 I2 언어가 Korean 이면 한글, 아니면 영문.
/// </summary>
public static class SaveText
{
    private static bool? _fontHasHangul;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _fontHasHangul = null;
    }

    /// <summary>
    /// 언어가 Korean 이고 UI 글꼴(pcsenior)이 한글 글리프를 가질 때만 한글을 쓴다.
    /// 현재 프로젝트에 한글 글꼴이 없어 대부분 영문으로 떨어진다(글꼴이 들어오면 자동으로 한글이 된다).
    /// </summary>
    public static bool IsKorean
    {
        get
        {
            string lang = LocalizationManager.CurrentLanguage;
            return !string.IsNullOrEmpty(lang) && lang.StartsWith("Ko", StringComparison.OrdinalIgnoreCase) && FontHasHangul();
        }
    }

    private static bool FontHasHangul()
    {
        if (_fontHasHangul.HasValue)
        {
            return _fontHasHangul.Value;
        }
        if (settingManager.Instance == null)
        {
            return false;
        }
        Font font;
        Sprite sprite;
        settingManager.Instance.GetButtonStyle(out font, out sprite);
        if (font == null)
        {
            return false;
        }
        _fontHasHangul = font.HasCharacter('가');
        return _fontHasHangul.Value;
    }

    public static string Pick(string ko, string en)
    {
        return IsKorean ? ko : en;
    }

    public static string PlaceName(string place)
    {
        switch (place)
        {
            case "DetectiveOffice": return Pick("탐정 사무소", "Detective Office");
            case "Client'shouse": return Pick("의뢰인의 집", "Client's House");
            case "DailyNews": return Pick("신문사", "Daily News");
            case "University": return Pick("대학교", "University");
            case "Riverside": return Pick("강변", "Riverside");
            case "Hospital": return Pick("병원", "Hospital");
            case "Policeoffice": return Pick("경찰서", "Police Office");
            case "Slum": return Pick("빈민가", "Slum");
            case "Bar": return Pick("술집", "Bar");
            case "Gunshop": return Pick("총기점", "Gunshop");
            case "SewerOffice": return Pick("하수도 관리소", "Sewer Office");
            default: return place;
        }
    }

    /// <summary>timeCount % 4 : 1 아침, 2 낮, 3 오후, 0 밤.</summary>
    public static string TimeOfDay(int timeCount)
    {
        switch (((timeCount % 4) + 4) % 4)
        {
            case 1: return Pick("아침", "Morning");
            case 2: return Pick("낮", "Noon");
            case 3: return Pick("오후", "Afternoon");
            default: return Pick("밤", "Night");
        }
    }

    public static string SlotName(int slot)
    {
        return slot == SaveService.AutoSlot ? Pick("자동 저장", "Auto Save") : Pick("슬롯 ", "Slot ") + slot;
    }

    public static string MetaLine(SaveMeta m)
    {
        string day = Pick(m.Day + "일차 ", "Day " + m.Day + " ") + TimeOfDay(m.TimeCount);
        long t = m.PlayTimeSec;
        string play = (t / 3600).ToString("00") + ":" + ((t / 60) % 60).ToString("00") + ":" + (t % 60).ToString("00");
        string when = m.SavedAtUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
        return day + "  " + PlaceName(m.PlaceId) + "\n" + play + "  " + when;
    }

    public static string ErrorMessage(SaveError e)
    {
        switch (e)
        {
            case SaveError.Missing: return Pick("저장 파일이 없습니다.", "No save file.");
            case SaveError.Corrupt: return Pick("저장 파일이 손상되어 불러올 수 없습니다.", "This save file is damaged and cannot be loaded.");
            case SaveError.NewerVersion: return Pick("더 새로운 버전의 저장 파일입니다.", "This save was made by a newer version.");
            case SaveError.NotAllowed: return Pick("지금은 저장하거나 불러올 수 없습니다.", "You cannot save or load right now.");
            case SaveError.PlaceUnsupported: return Pick("이 저장의 장소는 불러올 수 없습니다.", "The place in this save cannot be restored.");
            default: return Pick("저장 또는 불러오기에 실패했습니다.", "Saving or loading failed.");
        }
    }
}
