using System;
using SCoW.Core.State;
using UnityEngine;

/// <summary>
/// 읽은 대사 기록(ReadLog) 보관(P8). 슬롯과 무관한 전역 값이라 슬롯과 다른 파일 readlog.es3 의 "readlog" 키 한 문자열로 둔다.
/// 세이브 슬롯 형식은 건드리지 않는다. 대사가 끝날 때 바뀐 게 있으면 Save() 한다.
/// </summary>
public static class ReadLogService
{
    private const string FileNameDefault = "readlog.es3";
    private const string Key = "readlog";

    public static string FilePrefix = string.Empty;

    private static ReadLog _current;
    private static int _savedCount = -1;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _current = null;
        _savedCount = -1;
    }

    private static string Path
    {
        get { return FilePrefix + FileNameDefault; }
    }

    public static ReadLog Current
    {
        get
        {
            if (_current == null)
            {
                _current = Read();
                _savedCount = _current.Count;
            }
            return _current;
        }
    }

    private static ReadLog Read()
    {
        try
        {
            if (ES3.FileExists(Path) && ES3.KeyExists(Key, Path))
            {
                return ReadLog.Decode(ES3.Load<string>(Key, Path));
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("ReadLogService 읽기 실패, 빈 기록으로 시작: " + e.Message);
        }
        return new ReadLog();
    }

    /// <summary>마지막 저장 뒤 늘어난 게 있을 때만 쓴다.</summary>
    public static void SaveIfChanged()
    {
        if (_current == null || _current.Count == _savedCount)
        {
            return;
        }
        try
        {
            ES3.Save<string>(Key, _current.Encode(), Path);
            _savedCount = _current.Count;
        }
        catch (Exception e)
        {
            Debug.LogWarning("ReadLogService 저장 실패: " + e.Message);
        }
    }

    /// <summary>테스트·초기화용: 메모리와 파일을 지운다.</summary>
    public static void Reset()
    {
        _current = new ReadLog();
        _savedCount = 0;
        try
        {
            if (ES3.FileExists(Path))
            {
                ES3.DeleteFile(Path);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("ReadLogService 삭제 실패: " + e.Message);
        }
    }
}
