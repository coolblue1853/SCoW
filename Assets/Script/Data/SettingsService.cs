using System;
using SCoW.Core.Save;
using UnityEngine;

/// <summary>
/// settings.es3 의 "settings"(SettingsData) 보관·적용. 옛 정적 필드(DataBaseManager.soundVolume 등)와 Screen 설정에 반영한다.
/// 변경은 SettingsService.Save() 로 즉시 저장한다(설정창을 닫을 때·슬라이더 놓을 때 호출).
/// </summary>
public static class SettingsService
{
    private const string FileNameDefault = "settings.es3";
    private const string Key = "settings";

    public static string FilePrefix = string.Empty;

    /// <summary>6칸 해상도 표(옛 settingManager 의 changeInt 0~5 와 같은 순서).</summary>
    public static readonly int[] ResolutionWidth = { 2560, 1920, 1600, 1280, 960, 640 };
    public static readonly int[] ResolutionHeight = { 1440, 1080, 900, 720, 540, 360 };

    private static SettingsData _current;
    private static bool _loaded;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _current = null;
        _loaded = false;
    }

    /// <summary>현재 설정(처음 접근 때 파일에서 읽는다. 없거나 손상이면 기본값).</summary>
    public static SettingsData Current
    {
        get
        {
            if (!_loaded)
            {
                _current = Read();
                _loaded = true;
            }
            return _current;
        }
    }

    private static string Path
    {
        get { return FilePrefix + FileNameDefault; }
    }

    private static SettingsData Read()
    {
        SettingsData d = null;
        try
        {
            if (ES3.FileExists(Path) && ES3.KeyExists(Key, Path))
            {
                d = ES3.Load<SettingsData>(Key, Path);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("SettingsService 읽기 실패, 기본값 사용: " + e.Message);
        }
        if (d == null)
        {
            d = new SettingsData();
        }
        d.Sanitize();
        return d;
    }

    /// <summary>정적 필드(옛 DataBaseManager)의 현재 값을 Current 로 모아 파일에 쓴다.</summary>
    public static void Save()
    {
        SettingsData d = Current;
        // 디버프(isDebuffDeftness)가 음량을 0 으로 눌러 둔 동안은 그 0 을 설정으로 저장하지 않는다.
        if (!DataBaseManager.isDebuffDeftness)
        {
            d.SoundVolume = DataBaseManager.soundVolume;
            d.SfxVolume = DataBaseManager.sfxVolume;
        }
        d.TextDelay = DataBaseManager.textDelay;
        d.Sanitize();
        try
        {
            ES3.Save<SettingsData>(Key, d, Path);
        }
        catch (Exception e)
        {
            Debug.LogWarning("SettingsService 저장 실패: " + e.Message);
        }
    }

    /// <summary>Current 를 정적 필드·화면 설정에 적용한다(앱 시작 때 1회).</summary>
    public static void Apply()
    {
        SettingsData d = Current;
        DataBaseManager.soundVolume = d.SoundVolume;
        DataBaseManager.sfxVolume = d.SfxVolume;
        DataBaseManager.textDelay = d.TextDelay;
        ApplyScreen(d);
    }

    public static void ApplyScreen(SettingsData d)
    {
        int i = d.ResolutionIndex;
        if (Application.isEditor)
        {
            return;
        }
        Screen.SetResolution(ResolutionWidth[i], ResolutionHeight[i], d.FullScreen);
    }

    /// <summary>테스트용: 메모리 상태를 버리고 다시 읽게 한다.</summary>
    public static void ForgetLoaded()
    {
        _current = null;
        _loaded = false;
    }

    public static void DeleteFile()
    {
        try
        {
            if (ES3.FileExists(Path))
            {
                ES3.DeleteFile(Path);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("SettingsService 삭제 실패: " + e.Message);
        }
    }
}
