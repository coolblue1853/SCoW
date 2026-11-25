using System;
using SCoW.Core.Save;
using UnityEngine;

/// <summary>
/// 본 엔딩 기록(UX-12). 세이브 슬롯과 독립인 전역 파일 endings.es3 의 "endingsSeen" 키. 새 파일·새 키라 출시판 세이브와 충돌하지 않는다.
/// 엔딩 장면이 시작될 때 StoryDatabase 가 Note* 를 부르면 즉시 저장한다(처음 본 엔딩일 때만).
/// </summary>
public static class EndingService
{
    private const string FileNameDefault = "endings.es3";
    private const string Key = "endingsSeen";

    public static string FilePrefix = string.Empty;

    private static EndingRecord _current;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _current = null;
    }

    public static EndingRecord Current
    {
        get
        {
            if (_current == null)
            {
                _current = Read();
            }
            return _current;
        }
    }

    private static string Path
    {
        get { return FilePrefix + FileNameDefault; }
    }

    private static EndingRecord Read()
    {
        EndingRecord r = null;
        try
        {
            if (ES3.FileExists(Path) && ES3.KeyExists(Key, Path))
            {
                r = ES3.Load<EndingRecord>(Key, Path);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("EndingService 읽기 실패, 빈 기록 사용: " + e.Message);
        }
        if (r == null)
        {
            r = new EndingRecord();
        }
        r.Sanitize();
        return r;
    }

    public static bool IsSeen(string id)
    {
        return Current.IsSeen(id);
    }

    /// <summary>엔딩을 봤다고 기록한다. 처음이면 저장하고 true.</summary>
    public static bool Mark(string id)
    {
        if (!Current.Mark(id))
        {
            return false;
        }
        Save();
        return true;
    }

    /// <summary>StoryDatabase 훅: 대사 토픽이 엔딩 장면이면 기록한다.</summary>
    public static void NoteTopic(string scriptId, string topicId)
    {
        string id = EndingCatalog.FromTopic(scriptId, topicId);
        if (id != null)
        {
            Mark(id);
        }
    }

    public static void NoteJudge(string key)
    {
        string id = EndingCatalog.FromJudge(key);
        if (id != null)
        {
            Mark(id);
        }
    }

    public static void Save()
    {
        try
        {
            ES3.Save<EndingRecord>(Key, Current, Path);
        }
        catch (Exception e)
        {
            Debug.LogWarning("EndingService 저장 실패: " + e.Message);
        }
    }

    /// <summary>테스트용: 메모리 상태를 버리고 다시 읽게 한다.</summary>
    public static void ForgetLoaded()
    {
        _current = null;
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
            Debug.LogWarning("EndingService 삭제 실패: " + e.Message);
        }
    }
}
