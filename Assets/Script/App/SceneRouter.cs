using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>씬 식별자. 코드의 철자 그대로("SetCharacter" 와 폴더 오타 SetCharcter 는 별개).</summary>
public static class SceneIds
{
    public const string Insert = "Insert";
    public const string SetCharacter = "SetCharacter";
    public const string Main = "Main";
}

/// <summary>
/// 씬 전환·진입 알림 한 곳(P5). 게임 코드는 SceneManager.LoadScene/sceneLoaded 를 직접 쓰지 않고 여기로 온다.
/// </summary>
public static class SceneRouter
{
    /// <summary>씬이 로드될 때(sceneLoaded) 씬 id 와 함께 발생. 구독자는 OnEnable/OnDisable 쌍으로 관리한다.</summary>
    public static event Action<string> SceneEntered;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        SceneEntered = null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 안전망: 씬이 바뀌면 이전 씬 패널은 모두 사라졌으므로 스택을 비운다(P5 리뷰 권장).
        GameRoot.Instance.Panels.Clear();
        Action<string> handler = SceneEntered;
        if (handler != null)
        {
            handler(scene.name);
        }
    }

    public static void Go(string sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public static string ActiveId
    {
        get { return SceneManager.GetActiveScene().name; }
    }

    public static bool IsActive(string sceneId)
    {
        return SceneManager.GetActiveScene().name == sceneId;
    }
}
