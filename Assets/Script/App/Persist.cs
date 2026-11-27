using UnityEngine;

/// <summary>
/// 씬 전환에도 살아남는 오브젝트의 DontDestroyOnLoad 호출 단일 지점(P15a). 새 영속 오브젝트는 직접 호출하지 말고 이것으로 등록한다.
/// GameRoot 는 순수 C# 이라 씬 오브젝트가 없으므로(Architecture.md P15) 자식 붙이기 대신 호출 창구만 하나로 모았다.
/// 씬 직렬화·Awake 싱글턴 중복 파괴 순서는 기존과 같다(호출 위치·시점 그대로).
/// </summary>
public static class Persist
{
    public static void Keep(GameObject go)
    {
        // DontDestroyOnLoad 는 루트 오브젝트에만 동작한다. 비루트는 원래 효과가 없었으므로 경고 없이 건너뛴다(동작 불변).
        if (go != null && go.transform.parent == null)
        {
            Object.DontDestroyOnLoad(go);
        }
    }
}
