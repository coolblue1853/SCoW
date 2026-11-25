using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// KEYWORD-1 K6. 수첩이 키워드 제시 창 위로 열려 있을 때만 뜨는 "질문에 사용" 단추(임시 그래픽, TODO I2).
/// 코드로 만드는 오버레이라 씬 수정이 없다. 키보드·패드는 Present 키(IntelManager 가 처리)가 같은 일을 한다.
/// </summary>
public sealed class IntelKeywordUseButton : MonoBehaviour
{
    private static IntelKeywordUseButton _current;

    public static void Show(Action onClick)
    {
        Hide();
        GameObject go = new GameObject("IntelKeywordUseButton");
        _current = go.AddComponent<IntelKeywordUseButton>();
        _current.Build(onClick);
    }

    public static void Hide()
    {
        if (_current != null)
        {
            Destroy(_current.gameObject);
            _current = null;
        }
    }

    private void Build(Action onClick)
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 40;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        gameObject.AddComponent<GraphicRaycaster>();
        if (EventSystem.current == null)
        {
            GameObject es = new GameObject("EventSystem(IntelKeywordUse)");
            es.AddComponent<EventSystem>();
            es.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
        }

        GameObject btn = new GameObject("Button", typeof(RectTransform), typeof(Image), typeof(Button));
        btn.transform.SetParent(transform, false);
        RectTransform rt = (RectTransform)btn.transform;
        rt.anchorMin = new Vector2(0.5f, 0f);
        rt.anchorMax = new Vector2(0.5f, 0f);
        rt.pivot = new Vector2(0.5f, 0f);
        rt.anchoredPosition = new Vector2(0f, 40f);
        rt.sizeDelta = new Vector2(720f, 64f);
        btn.GetComponent<Image>().color = new Color(0.1f, 0.09f, 0.08f, 0.95f);
        btn.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (onClick != null)
            {
                onClick();
            }
        });

        GameObject txt = new GameObject("Text", typeof(RectTransform));
        txt.transform.SetParent(btn.transform, false);
        TextMeshProUGUI t = txt.AddComponent<TextMeshProUGUI>();
        if (DialogManager.Instance != null && DialogManager.Instance.DialogFont != null)
        {
            t.font = DialogManager.Instance.DialogFont;
        }
        t.text = "Use in question  (F / Y)";
        t.fontSize = 24;
        t.alignment = TextAlignmentOptions.Center;
        t.color = Color.white;
        t.raycastTarget = false;
        RectTransform trt = t.rectTransform;
        trt.anchorMin = Vector2.zero;
        trt.anchorMax = Vector2.one;
        trt.offsetMin = Vector2.zero;
        trt.offsetMax = Vector2.zero;
    }

    private void OnDestroy()
    {
        if (_current == this)
        {
            _current = null;
        }
    }
}
