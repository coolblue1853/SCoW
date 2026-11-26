using SCoW.Core.Save;
using UnityEngine;

/// <summary>
/// 빠른 저장(F5 / 패드 L1+십자 아래)·빠른 불러오기(F9 / 패드 L1+십자 위) 입력(UX-5). 패널이 열려 있거나 Main 씬이 아니면 읽지 않는다.
/// 빠른 불러오기는 한 번 더 눌러야 실행된다(저장하지 않은 진행 손실 확인). 문구는 임시, TODO(I2).
/// </summary>
public sealed class QuickSaveInput : MonoBehaviour
{
    private const float ConfirmWindowSec = 3f;

    private static float _loadArmedUntil = -1f;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _loadArmedUntil = -1f;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Boot()
    {
        GameObject go = new GameObject("QuickSaveInput");
        Persist.Keep(go);
        go.AddComponent<QuickSaveInput>();
    }

    private void Update()
    {
        bool save = GameInput.QuickSaveDown;
        bool load = GameInput.QuickLoadDown;
        if (!save && !load)
        {
            return;
        }
        if (GameRoot.Instance == null || !SaveFlow.InMainScene || UiGate.AnyOpen)
        {
            return;
        }
        if (save)
        {
            SaveFlow.QuickSave();
        }
        else
        {
            TryQuickLoad();
        }
    }

    /// <summary>F9 한 번째는 확인 토스트, 제한 시간 안의 두 번째에 불러온다. 입력 없이도 호출 가능(플레이 검증용).</summary>
    public static void TryQuickLoad()
    {
        if (SaveFlow.IsBusy)
        {
            SaveToast.Show(SaveText.Pick("대화·판정·연출 중에는 불러올 수 없습니다.", "You cannot load during dialogue, checks or scenes."), true);
            return;
        }
        if (!SaveService.Exists(SaveService.QuickSlot))
        {
            SaveToast.Show(SaveText.Pick("빠른 저장이 없습니다.", "No quick save yet."), true);
            return;
        }
        if (Time.unscaledTime > _loadArmedUntil)
        {
            _loadArmedUntil = Time.unscaledTime + ConfirmWindowSec;
            SaveToast.Show(SaveText.Pick("한 번 더 누르면 빠른 저장을 불러옵니다.", "Press again to load the quick save."), false);
            return;
        }
        _loadArmedUntil = -1f;
        SaveError e = SaveFlow.LoadAndEnter(SaveService.QuickSlot);
        if (e != SaveError.None)
        {
            SaveToast.Show(SaveText.ErrorMessage(e), true);
        }
    }
}
