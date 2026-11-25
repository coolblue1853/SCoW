using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 툴팁을 띄울 UI 항목에 붙인다(UX-11). 마우스 호버(PointerEnter/Exit)와 키보드·패드 포커스(Select/Deselect, EventSystem 선택)
/// 둘 다 같은 TooltipView 로 보낸다. 글은 Provider 가 그때그때 만든다(HP 가 바뀌어도 최신).
/// </summary>
public sealed class TooltipTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public Func<string> Provider;

    private RectTransform _rect;

    private void Awake()
    {
        _rect = (RectTransform)transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipView.Hide(this);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Show();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        TooltipView.Hide(this);
    }

    private void OnDisable()
    {
        TooltipView.Hide(this);
    }

    private void Show()
    {
        if (Provider != null)
        {
            TooltipView.Show(this, Provider(), _rect);
        }
    }
}
