using System;
using System.Collections;
using SCoW.Core.Save;
using SCoW.Core.State;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 저장·불러오기 흐름. 허용 지점 판단(CanSave), 슬롯 저장, 자동 저장, 불러오기 → Main 씬 → 장소 복원.
/// 복원 자체는 DirectingManager.OnSceneLoaded 가 PendingRestore 를 보고 MapManager.RestorePlace 를 부른다.
/// </summary>
public static class SaveFlow
{
    public const string MainScene = "Main";
    private const float AutoSaveTimeoutSec = 180f;
    private static readonly WaitForSecondsRealtime AutoSaveWait = new WaitForSecondsRealtime(0.5f);

    /// <summary>씬 전환 이음새. P5 SceneRouter 가 생기면 이 대리자만 갈아끼운다(이 파일의 유일한 LoadScene 호출).</summary>
    public static Action<string> SceneLoader = DefaultSceneLoader;

    /// <summary>"연출·대기 중인가" 판정 이음새. P5 에서 상태 기계 질의로 바꾼다. 기본은 기존 정적 플래그 묶음.</summary>
    public static Func<bool> BusyProbe = DefaultBusy;

    private static void DefaultSceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static bool InMainScene
    {
        get { return SceneManager.GetActiveScene().name == MainScene; }
    }

    /// <summary>불러오기로 Main 씬을 열었고 아직 장소 복원 전이면 true.</summary>
    public static bool PendingRestore { get; private set; }

    private static int _autoSaveGeneration;

    public static void ResetStatics()
    {
        PendingRestore = false;
        _autoSaveGeneration = 0;
        SceneLoader = DefaultSceneLoader;
        BusyProbe = DefaultBusy;
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

    /// <summary>불러오기 복원 전이거나 BusyProbe 가 참이면 바쁨. 이때는 저장·불러오기 모두 금지.</summary>
    public static bool IsBusy
    {
        get { return PendingRestore || BusyProbe(); }
    }

    /// <summary>연출·대기 중 플래그(전부 기존 정적 필드). isOpenUi 는 설정창 자신이 켜므로 보지 않는다.</summary>
    private static bool DefaultBusy()
    {
        return DataBaseManager.isActiveDialog1 || DataBaseManager.isActiveDialog2
            || DataBaseManager.storyDirecting || DataBaseManager.secondisDirecting || DataBaseManager.isDirecting
            || DataBaseManager.isRollet || DataBaseManager.isBattleRollet
            || DataBaseManager.potalWait || DataBaseManager.isOpenMap
            || DataBaseManager.isScene || DataBaseManager.isSelect || DataBaseManager.selectionOn
            || DataBaseManager.battleSan || DataBaseManager.strDialogOn || DataBaseManager.barMove
            || DataBaseManager.isBattleBeforeDialog || DataBaseManager.afterBattle;
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
                }
                yield break;
            }
            waited += 0.5f;
            yield return AutoSaveWait;
        }
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
        if (!IsSavePlace(r.State.GetText(TextKeys.nowPlace)))
        {
            return SaveError.PlaceUnsupported;
        }
        GameRoot.Instance.LoadState(r.State, r.Meta.PlayTimeSec);
        PendingRestore = true;
        _autoSaveGeneration++;
        SceneLoader(MainScene);
        return SaveError.None;
    }

    public static void CompleteRestore()
    {
        PendingRestore = false;
    }

    /// <summary>손상 여부와 관계없이 저장 파일이 하나라도 있나(타이틀 "이어하기" 표시용).</summary>
    public static bool AnySave()
    {
        for (int s = 0; s < SaveService.SlotCount; s++)
        {
            if (SaveService.Exists(s))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>이어하기 후보: 존재하고 읽히는 슬롯 중 가장 최근 저장. 없으면 -1.</summary>
    public static int LatestSlot()
    {
        int best = -1;
        long bestTicks = long.MinValue;
        for (int s = 0; s < SaveService.SlotCount; s++)
        {
            SaveMeta m;
            if (SaveService.ReadMeta(s, out m) == SaveError.None && m.SavedAtUtcTicks > bestTicks)
            {
                best = s;
                bestTicks = m.SavedAtUtcTicks;
            }
        }
        return best;
    }
}
