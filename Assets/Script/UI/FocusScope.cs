using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 패널 포커스 도우미(UX-1). 패널이 열릴 때 Enter(owner, 첫 항목)로 첫 항목을 선택하고,
/// 닫힐 때 Exit(owner)로 열기 전 선택을 되돌린다. 선택 표시는 UxOverlay 의 포커스 링이 그린다(마우스 사용 중에는 숨김).
/// </summary>
public static class FocusScope
{
    private struct Entry
    {
        public object Owner;
        public GameObject Previous;
    }

    private static readonly List<Entry> _stack = new List<Entry>(4);
    private static object _targetOwner;
    private static RectTransform _target;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _stack.Clear();
        _targetOwner = null;
        _target = null;
    }

    /// <summary>포커스 링을 그릴 대상(패널이 직접 고른 항목). 없으면 UxOverlay 가 EventSystem 선택을 쓴다.</summary>
    public static RectTransform Target
    {
        get { return _target; }
    }

    /// <summary>owner 가 지금 포커스 항목을 알린다(EventSystem 선택을 건드리지 않는다: 이중 제출 방지).</summary>
    public static void Highlight(object owner, RectTransform target)
    {
        _targetOwner = owner;
        _target = target;
    }

    /// <summary>owner 의 포커스 범위를 시작한다. first 가 null 이면 현재 선택을 그대로 둔다.</summary>
    public static void Enter(object owner, GameObject first)
    {
        if (owner == null)
        {
            return;
        }
        EventSystem es = EventSystem.current;
        int existing = IndexOf(owner);
        GameObject previous = existing >= 0 ? _stack[existing].Previous : (es != null ? es.currentSelectedGameObject : null);
        Remove(owner);
        Entry e;
        e.Owner = owner;
        e.Previous = previous;
        _stack.Add(e);
        if (es != null && first != null)
        {
            es.SetSelectedGameObject(first);
        }
    }

    /// <summary>범위를 끝내고, 맨 위 범위였다면 열기 전 선택을 되돌린다(사라졌거나 못 누르는 항목이면 선택을 비운다).</summary>
    public static void Exit(object owner)
    {
        int index = IndexOf(owner);
        if (index < 0)
        {
            return;
        }
        if (ReferenceEquals(_targetOwner, owner))
        {
            _targetOwner = null;
            _target = null;
        }
        bool wasTop = index == _stack.Count - 1;
        GameObject previous = _stack[index].Previous;
        _stack.RemoveAt(index);
        EventSystem es = EventSystem.current;
        if (!wasTop || es == null)
        {
            return;
        }
        bool usable = previous != null && previous.activeInHierarchy;
        if (usable)
        {
            Selectable sel = previous.GetComponent<Selectable>();
            usable = sel == null || sel.IsInteractable();
        }
        es.SetSelectedGameObject(usable ? previous : null);
    }

    private static void Remove(object owner)
    {
        int index = IndexOf(owner);
        if (index >= 0)
        {
            _stack.RemoveAt(index);
        }
    }

    private static int IndexOf(object owner)
    {
        for (int i = 0; i < _stack.Count; i++)
        {
            if (ReferenceEquals(_stack[i].Owner, owner))
            {
                return i;
            }
        }
        return -1;
    }
}
