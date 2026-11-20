using System;
using SCoW.Core.Save;
using SCoW.Core.State;
using UnityEngine;

/// <summary>저장 슬롯 읽기 결과. Error 가 None 일 때만 State·Meta 가 유효하다.</summary>
public struct SaveLoadResult
{
    public SaveError Error;
    public GameState State;
    public SaveMeta Meta;
}

/// <summary>
/// ES3 어댑터. 슬롯 파일 slot_auto.es3 / slot_1~3.es3 에 meta·state 두 키를 문자열(SaveCodec)로 쓴다.
/// 어떤 실패도 예외로 밖에 내보내지 않고 SaveError 로 돌려준다(손상 파일이 크래시를 일으키지 않게).
/// </summary>
public static class SaveService
{
    public const int AutoSlot = 0;
    public const int ManualSlotCount = 3;
    public const int SlotCount = ManualSlotCount + 1;

    private const string MetaKey = "meta";
    private const string StateKey = "state";

    /// <summary>파일명 접두. 테스트가 "test_" 로 바꿔 실제 세이브를 건드리지 않는다.</summary>
    public static string FilePrefix = string.Empty;

    public static string FileName(int slot)
    {
        return FilePrefix + (slot == AutoSlot ? "slot_auto.es3" : "slot_" + slot + ".es3");
    }

    public static bool IsValidSlot(int slot)
    {
        return slot >= AutoSlot && slot < SlotCount;
    }

    public static bool Exists(int slot)
    {
        try
        {
            return IsValidSlot(slot) && ES3.FileExists(FileName(slot));
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static SaveError Save(int slot, GameState state, long playTimeSec)
    {
        if (!IsValidSlot(slot) || state == null)
        {
            return SaveError.IoError;
        }
        try
        {
            string path = FileName(slot);
            if (ES3.FileExists(path))
            {
                ES3.CreateBackup(path);
            }
            SaveMeta meta = new SaveMeta
            {
                SchemaVersion = state.SchemaVersion,
                SavedAtUtcTicks = DateTime.UtcNow.Ticks,
                PlayTimeSec = playTimeSec < 0 ? 0 : playTimeSec,
                PlaceId = state.GetText(TextKeys.nowPlace),
                TimeCount = state.GetCounter(CounterKeys.timeCount),
            };
            ES3.Save<string>(MetaKey, SaveCodec.EncodeMeta(meta), path);
            ES3.Save<string>(StateKey, SaveCodec.EncodeState(state), path);
            return SaveError.None;
        }
        catch (Exception e)
        {
            Debug.LogWarning("SaveService.Save 실패 slot=" + slot + ": " + e.Message);
            return SaveError.IoError;
        }
    }

    /// <summary>슬롯 목록용. meta 만 읽는다.</summary>
    public static SaveError ReadMeta(int slot, out SaveMeta meta)
    {
        meta = null;
        if (!IsValidSlot(slot))
        {
            return SaveError.IoError;
        }
        try
        {
            string path = FileName(slot);
            if (!ES3.FileExists(path))
            {
                return SaveError.Missing;
            }
            if (!ES3.KeyExists(MetaKey, path))
            {
                return SaveError.Corrupt;
            }
            return SaveCodec.TryDecodeMeta(ES3.Load<string>(MetaKey, path), out meta);
        }
        catch (Exception e)
        {
            Debug.LogWarning("SaveService.ReadMeta 실패 slot=" + slot + ": " + e.Message);
            meta = null;
            return SaveError.Corrupt;
        }
    }

    public static SaveLoadResult Load(int slot)
    {
        SaveLoadResult r = TryLoad(slot);
        if (r.Error == SaveError.Corrupt && IsValidSlot(slot))
        {
            // 쓰기 직전에 만든 백업(.bac)이 있으면 한 번만 복구해 다시 읽는다.
            try
            {
                if (ES3.RestoreBackup(FileName(slot)))
                {
                    SaveLoadResult retry = TryLoad(slot);
                    if (retry.Error == SaveError.None)
                    {
                        return retry;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("SaveService 백업 복구 실패 slot=" + slot + ": " + e.Message);
            }
        }
        return r;
    }

    private static SaveLoadResult TryLoad(int slot)
    {
        SaveLoadResult r = new SaveLoadResult();
        SaveMeta meta;
        r.Error = ReadMeta(slot, out meta);
        if (r.Error != SaveError.None)
        {
            return r;
        }
        try
        {
            string path = FileName(slot);
            if (!ES3.KeyExists(StateKey, path))
            {
                r.Error = SaveError.Corrupt;
                return r;
            }
            GameState state;
            r.Error = SaveCodec.TryDecodeState(ES3.Load<string>(StateKey, path), out state);
            if (r.Error != SaveError.None)
            {
                return r;
            }
            if (state.SchemaVersion != meta.SchemaVersion)
            {
                r.Error = SaveError.Corrupt;
                return r;
            }
            r.Error = SaveMigrator.Default.Migrate(state);
            if (r.Error != SaveError.None)
            {
                return r;
            }
            r.State = state;
            r.Meta = meta;
            return r;
        }
        catch (Exception e)
        {
            Debug.LogWarning("SaveService.Load 실패 slot=" + slot + ": " + e.Message);
            r.Error = SaveError.Corrupt;
            return r;
        }
    }

    public static void Delete(int slot)
    {
        try
        {
            string path = FileName(slot);
            if (ES3.FileExists(path))
            {
                ES3.DeleteFile(path);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("SaveService.Delete 실패 slot=" + slot + ": " + e.Message);
        }
    }
}
