using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 파티 전투 화면(PartyBattleView) 동안 옛 전투 화면 요소를 감춘다(BATTLE-UI-1).
/// 옛 날짜·컨디션 박스(UpperUI)·옛 턴 게이지(TurnTime)는 CanvasGroup 알파 0 으로, 옛 전투 스프라이트(플레이어·적)는 렌더러 enabled 로 가린다.
/// 오브젝트를 끄지 않으므로 옛 전투 로직(activeSelf 검사)은 그대로 돈다. 끝나면 원래대로 되돌린다.
/// </summary>
public static class PartyBattleOldUi
{
    private static readonly string[] HiddenUiPaths = { "UI/UpperUI", "BattleCanvus/TurnTime", "BattleCanvus/Action", "UI/Uibuttn", "UI/Back", "BillowUICanvus/BillowUI" };

    private static readonly List<CanvasGroup> Groups = new List<CanvasGroup>();
    private static readonly List<bool> GroupAdded = new List<bool>();
    private static readonly List<float> GroupAlpha = new List<float>();
    private static readonly List<Renderer> Renderers = new List<Renderer>();
    private static bool _hidden;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        Groups.Clear();
        GroupAdded.Clear();
        GroupAlpha.Clear();
        Renderers.Clear();
        _hidden = false;
    }

    /// <summary>지금 감춰져 있는지(검증용).</summary>
    public static bool IsHidden
    {
        get { return _hidden; }
    }

    public static void Hide(BattleManager manager)
    {
        // Persist 로 씬이 바뀌어도 이전 상태가 남을 수 있으니, 이미 가려 있으면 먼저 되돌리고 목록을 새로 만든다.
        Restore();
        _hidden = true;
        for (int i = 0; i < HiddenUiPaths.Length; i++)
        {
            GameObject go = GameObject.Find(HiddenUiPaths[i]);
            if (go == null)
            {
                continue;
            }
            CanvasGroup g = go.GetComponent<CanvasGroup>();
            bool added = g == null;
            if (added)
            {
                g = go.AddComponent<CanvasGroup>();
            }
            Groups.Add(g);
            GroupAdded.Add(added);
            GroupAlpha.Add(g.alpha);
            g.alpha = 0f;
        }
        if (manager == null)
        {
            return;
        }
        HideSprites(manager.player);
        HideSprites(manager.deepOneHybrid1Ob);
        HideSprites(manager.deepOneHybrid2Ob);
        HideSprites(manager.deepOneHybrid3Ob);
        HideSprites(manager.deepOneHybrid4Ob);
        HideSprites(manager.deepOne1Ob);
    }

    public static void Restore()
    {
        if (!_hidden)
        {
            return;
        }
        _hidden = false;
        for (int i = 0; i < Groups.Count; i++)
        {
            if (Groups[i] == null)
            {
                continue;
            }
            Groups[i].alpha = GroupAlpha[i];
            if (GroupAdded[i])
            {
                Object.Destroy(Groups[i]);
            }
        }
        Groups.Clear();
        GroupAdded.Clear();
        GroupAlpha.Clear();
        for (int i = 0; i < Renderers.Count; i++)
        {
            if (Renderers[i] != null)
            {
                Renderers[i].enabled = true;
            }
        }
        Renderers.Clear();
    }

    private static void HideSprites(GameObject root)
    {
        if (root == null)
        {
            return;
        }
        SpriteRenderer[] rs = root.GetComponentsInChildren<SpriteRenderer>(true);
        for (int i = 0; i < rs.Length; i++)
        {
            if (rs[i].enabled)
            {
                rs[i].enabled = false;
                Renderers.Add(rs[i]);
            }
        }
    }
}
