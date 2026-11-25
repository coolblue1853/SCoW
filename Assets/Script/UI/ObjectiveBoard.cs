using System.Collections.Generic;
using SCoW.Core.Story;
using TMPro;
using UnityEngine;

/// <summary>
/// 현재 목표 조회(UX-3, View 쪽 얇은 입구). Resources/Story/objectives.json 을 한 번 읽어 캐시하고,
/// 지금 상태에서 맞는 목표를 현재 언어 문자열로 돌려 준다. 시간 HUD 한 줄과 수첩 "할 일" 탭이 같이 쓴다.
/// </summary>
public static class ObjectiveBoard
{
    private const string Resource = "Story/objectives";

    private static ObjectiveCatalog _catalog;
    private static bool _failed;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _catalog = null;
        _failed = false;
    }

    private static ObjectiveCatalog Catalog
    {
        get
        {
            if (_catalog != null || _failed)
            {
                return _catalog;
            }
            TextAsset ta = Resources.Load<TextAsset>(Resource);
            if (ta == null)
            {
                Debug.LogError("ObjectiveBoard: Resources/" + Resource + " 를 찾지 못했다.");
                _failed = true;
                return null;
            }
            ObjectiveCatalogLoadResult res = ObjectiveCatalog.Load(ta.text);
            if (!res.Ok)
            {
                Debug.LogError("ObjectiveBoard: 목표 규칙 오류 - " + string.Join(" | ", res.Errors));
                _failed = true;
                return null;
            }
            _catalog = res.Catalog;
            return _catalog;
        }
    }

    /// <summary>
    /// TMP 글꼴(DialogFont)이 한글을 그릴 수 있을 때만 한글을 쓴다. SaveText.IsKorean 은 옛 Font 기준이라 TMP 정적 글꼴엔 맞지 않다.
    /// 글꼴에 한글이 없으면 영문(UX-10 한글 글꼴 도입 전까지).
    /// </summary>
    public static string Pick(string ko, string en)
    {
        TMP_FontAsset font = DialogManager.Instance != null ? DialogManager.Instance.DialogFont : null;
        bool hangul = SaveText.IsKorean && font != null && font.HasCharacter('가', true);
        return hangul && !string.IsNullOrEmpty(ko) ? ko : en;
    }

    private static string Text(ObjectiveRule r)
    {
        return Pick(r.Ko, r.En);
    }

    /// <summary>지금 가장 우선인 목표 한 줄. 데이터를 못 읽었으면 빈 문자열.</summary>
    public static string CurrentLine()
    {
        ObjectiveCatalog c = Catalog;
        if (c == null || GameRoot.Instance == null)
        {
            return string.Empty;
        }
        ObjectiveRule r = c.Current(new GameStateAccess(GameRoot.Instance.State));
        return r != null ? Text(r) : string.Empty;
    }

    /// <summary>지금 맞는 목표 전부(우선순위 순). 수첩 "할 일" 탭용.</summary>
    public static List<string> AllLines()
    {
        List<string> lines = new List<string>();
        ObjectiveCatalog c = Catalog;
        if (c == null || GameRoot.Instance == null)
        {
            return lines;
        }
        List<ObjectiveRule> active = c.Active(new GameStateAccess(GameRoot.Instance.State));
        for (int i = 0; i < active.Count; i++)
        {
            lines.Add(Text(active[i]));
        }
        return lines;
    }
}
