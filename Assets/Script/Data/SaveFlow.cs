using System;
using System.Collections;
using SCoW.Core.Save;
using SCoW.Core.State;
using UnityEngine;

/// <summary>
/// 저장·불러오기 흐름. 허용 지점 판단(CanSave), 슬롯 저장, 자동 저장, 불러오기 → Main 씬 → 장소 복원.
/// 복원 자체는 DirectingManager.OnSceneLoaded 가 PendingRestore 를 보고 MapManager.RestorePlace 를 부른다.
/// </summary>
public static class SaveFlow
{
    private const float AutoSaveTimeoutSec = 180f;
    private static readonly WaitForSecondsRealtime AutoSaveWait = new WaitForSecondsRealtime(0.5f);

    public static bool InMainScene
    {
        get { return SceneRouter.IsActive(SceneIds.Main); }
    }

    /// <summary>불러오기로 Main 씬을 열었고 아직 장소 복원 전이면 true.</summary>
    public static bool PendingRestore { get; private set; }

    private static int _autoSaveGeneration;

    public static void ResetStatics()
    {
        PendingRestore = false;
        _autoSaveGeneration = 0;
    }

    /// <summary>저장 가능한 장소: 장소 이동 직후의 안정 상태가 되는 11곳. 하수구 안·전투 길·낮 이벤트는 제외.</summary>
    public static bool IsSavePlace(string place)
    {
        switch (place)
        {
            case "DetectiveOffice":
            case "Client'shouse":
            case "DailyNews":
            case "University":
            case "Riverside":
            case "Hospital":
            case "Policeoffice":
            case "Slum":
            case "Bar":
            case "Gunshop":
            case "SewerOffice":
                return true;
            default:
                return false;
        }
    }

    /// <summary>대사·전투·판정·연출·지도 이동 중이 아닌가. 이유는 reason(한글/영문 선택은 UI).</summary>
    public static bool CanSave(out string reasonKo, out string reasonEn)
    {
        reasonKo = null;
        reasonEn = null;
        if (!InMainScene)
        {
            reasonKo = "게임 중에만 저장할 수 있습니다.";
            reasonEn = "You can only save during the game.";
        }
        else if (!IsSavePlace(DataBaseManager.nowPlace))
        {
            reasonKo = "이 장소에서는 저장할 수 없습니다.";
            reasonEn = "You cannot save at this place.";
        }
        else if (IsBusy)
        {
            reasonKo = "대화·판정·연출 중에는 저장할 수 없습니다.";
            reasonEn = "You cannot save during dialogue, checks or scenes.";
        }
        return reasonKo == null;
    }

    public static bool CanSave()
    {
        string ko;
        string en;
        return CanSave(out ko, out en);
    }

    /// <summary>불러오기 복원 전이거나 연출·대기 중(UiGate.IsBusy → BusyRules)이면 바쁨. 이때는 저장·불러오기 모두 금지.</summary>
    public static bool IsBusy
    {
        get { return PendingRestore || UiGate.IsBusy; }
    }

    /// <summary>수동·자동 공용 저장. CanSave 를 통과해야 한다.</summary>
    public static SaveError Save(int slot)
    {
        if (!CanSave())
        {
            return SaveError.NotAllowed;
        }
        GameRoot root = GameRoot.Instance;
        return SaveService.Save(slot, root.State, root.PlayTimeSec);
    }

    /// <summary>장소 이동이 끝난 뒤 안정 상태가 되면 자동 슬롯에 저장하는 코루틴을 만든다. 이동이 또 있으면 이전 요청은 취소된다.</summary>
    public static IEnumerator AutoSaveWhenStable()
    {
        int gen = ++_autoSaveGeneration;
        float waited = 0f;
        while (gen == _autoSaveGeneration && waited < AutoSaveTimeoutSec)
        {
            if (CanSave())
            {
                SaveError e = Save(SaveService.AutoSlot);
                if (e != SaveError.None)
                {
                    Debug.LogWarning("자동 저장 실패: " + e);
                    SaveToast.Show(SaveText.Pick("자동 저장 실패: ", "Auto-save failed: ") + SaveText.ErrorMessage(e), true);
                }
                else
                {
                    SaveToast.Show(SaveText.Pick("자동 저장됨", "Auto-saved"), false);
                }
                yield break;
            }
            waited += 0.5f;
            yield return AutoSaveWait;
        }
    }

    /// <summary>빠른 저장(F5). 저장 불가 사유·성공·실패를 토스트로 알린다. 빠른 슬롯 전용 파일을 쓴다.</summary>
    public static SaveError QuickSave()
    {
        string ko;
        string en;
        if (!CanSave(out ko, out en))
        {
            SaveToast.Show(SaveText.Pick(ko, en), true);
            return SaveError.NotAllowed;
        }
        SaveError e = Save(SaveService.QuickSlot);
        if (e == SaveError.None)
        {
            SaveToast.Show(SaveText.Pick("빠른 저장됨", "Quick saved"), false);
        }
        else
        {
            SaveToast.Show(SaveText.Pick("빠른 저장 실패: ", "Quick save failed: ") + SaveText.ErrorMessage(e), true);
        }
        return e;
    }

    /// <summary>사망 후 재개용 "마지막 저장": 자동·수동·빠른 슬롯 중 읽히는 가장 최근 것. 없으면 -1.</summary>
    public static int LatestResumeSlot()
    {
        return LatestSlot();
    }

    /// <summary>새 게임을 시작할 때 빠른 슬롯을 비운다. 빠른 저장은 한 판 안에서만 쓰므로, 새 판이 이전 판의 빠른 저장으로 재개되지 않게 한다.</summary>
    public static void InvalidateQuickSlot()
    {
        SaveService.Delete(SaveService.QuickSlot);
    }

    /// <summary>슬롯을 읽어 GameState 를 주입하고 Main 씬을 연다. 실패하면 오류만 돌려주고 아무것도 바꾸지 않는다.</summary>
    public static SaveError LoadAndEnter(int slot)
    {
        if (PendingRestore)
        {
            return SaveError.NotAllowed;
        }
        SaveLoadResult r = SaveService.Load(slot);
        if (r.Error != SaveError.None)
        {
            return r.Error;
        }
        string loadedPlace = r.State.GetText(TextKeys.nowPlace);
        if (!IsSavePlace(loadedPlace) && loadedPlace != SCoW.Core.Sewer.SewerLegacy.SewerPlace)
        {
            return SaveError.PlaceUnsupported;
        }
        GameRoot.Instance.LoadState(r.State, r.Meta.PlayTimeSec);
        PendingRestore = true;
        _autoSaveGeneration++;
        SceneRouter.Go(SceneIds.Main);
        return SaveError.None;
    }

    public static void CompleteRestore()
    {
        PendingRestore = false;
    }

    /// <summary>손상 여부와 관계없이 저장 파일이 하나라도 있나(타이틀 "이어하기" 표시용).</summary>
    public static bool AnySave()
    {
        for (int s = 0; s <= SaveService.QuickSlot; s++)
        {
            if (SaveService.Exists(s))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>이어하기·사망 재개 후보: 자동·수동·빠른 슬롯 중 읽히는 가장 최근 저장. 없으면 -1.</summary>
    public static int LatestSlot()
    {
        long[] ticks = new long[SaveService.QuickSlot + 1];
        for (int s = 0; s < ticks.Length; s++)
        {
            SaveMeta m;
            ticks[s] = SaveService.ReadMeta(s, out m) == SaveError.None ? m.SavedAtUtcTicks : 0L;
        }
        return ResumeRules.LatestIndex(ticks);
    }
}
