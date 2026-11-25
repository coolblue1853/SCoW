using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 대사창 보조 표시(UX-8, View). (1) log·auto·skip 버튼 ON/OFF 모양, (2) 글이 다 나온 뒤 입력을 기다릴 때의 ▼ 표시.
/// 그래픽은 임시(ART-2). 문구는 UiText(TODO I2).
/// </summary>
public static class DialogHudStyle
{
    private static readonly Color OffBg = new Color(0.55f, 0.55f, 0.58f, 1f);
    private static readonly Color OnBg = new Color(1f, 0.82f, 0.3f, 1f);
    private static readonly Color OffText = new Color(0.95f, 0.95f, 0.95f, 1f);

    /// <summary>버튼 배경·글자를 ON(강조)/OFF 로 칠한다. label 이 null 이 아니면 글자도 바꾼다.</summary>
    public static void Paint(GameObject button, bool on, string label)
    {
        if (button == null)
        {
            return;
        }
        Image bg = button.GetComponent<Image>();
        if (bg != null)
        {
            bg.color = on ? OnBg : OffBg;
        }
        Text t = button.GetComponentInChildren<Text>(true);
        if (t != null)
        {
            t.color = on ? OnBg : OffText;
            t.horizontalOverflow = HorizontalWrapMode.Overflow;
            t.fontStyle = on ? FontStyle.Bold : FontStyle.Normal;
            if (label != null)
            {
                t.text = label;
            }
        }
    }

    /// <summary>parent 오른쪽 아래에 ▼ 마커를 만든다(처음엔 꺼 둔다).</summary>
    public static GameObject BuildNextMarker(Transform parent)
    {
        GameObject go = new GameObject("NextMarker", typeof(RectTransform), typeof(Image), typeof(DialogNextMarker));
        go.transform.SetParent(parent, false);
        RectTransform rt = (RectTransform)go.transform;
        rt.anchorMin = new Vector2(1f, 0f);
        rt.anchorMax = new Vector2(1f, 0f);
        rt.pivot = new Vector2(1f, 0f);
        rt.sizeDelta = new Vector2(28f, 20f);
        rt.anchoredPosition = new Vector2(-24f, 14f);
        Image img = go.GetComponent<Image>();
        img.sprite = DialogNextMarker.TriangleSprite;
        img.color = OnBg;
        img.raycastTarget = false;
        go.SetActive(false);
        return go;
    }
}
