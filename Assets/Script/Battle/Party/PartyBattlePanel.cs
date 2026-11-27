using DG.Tweening;
using SCoW.Core.Rules;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 전투 초상화 한 칸(PT-5, View). 초상화(기존 스프라이트 재사용, 없으면 색 사각형)·이름·HP/SAN 막대·상태 글자.
/// 코드로 만든다(씬 수정 없음). 글자·색은 임시(그래픽은 사용자 검토 후 교체).
/// </summary>
public sealed class PartyBattlePanel
{
    public const float Width = 220f;
    public const float Height = 290f;
    private const float BarWidth = 200f;

    public readonly string Id;
    public readonly Side Side;
    public readonly RectTransform Rect;
    public int Rank;
    public bool Gone;

    private readonly Image _portrait;
    private readonly Image _outline;
    private readonly Image _hpFill;
    private readonly Image _sanFill;
    private readonly Text _name;
    private readonly Text _status;
    private readonly Text _hpText;
    private readonly Color _baseColor;
    private readonly CanvasGroup _group;
    private int _maxHp;
    private int _maxSan;

    public PartyBattlePanel(Transform parent, Font font, IBattleUnit unit, Sprite sprite, Color color)
    {
        Id = unit.Id;
        Side = unit.Side;
        Rank = unit.Rank;
        _baseColor = color;
        GameObject go = new GameObject("Panel_" + unit.Id, typeof(RectTransform), typeof(CanvasGroup));
        go.transform.SetParent(parent, false);
        Rect = (RectTransform)go.transform;
        Rect.sizeDelta = new Vector2(Width, Height);
        _group = go.GetComponent<CanvasGroup>();

        _outline = PartyBattleUi.Box(Rect, "Outline", new Vector2(Width + 12f, Height + 12f), new Vector2(0f, 0f), new Color(1f, 0.85f, 0.3f, 0f));
        Image back = PartyBattleUi.Box(Rect, "Back", new Vector2(Width, Height), Vector2.zero, new Color(0.08f, 0.08f, 0.1f, 0.85f));
        _portrait = PartyBattleUi.Box(Rect, "Portrait", new Vector2(Width - 20f, 130f), new Vector2(0f, 38f), sprite == null ? color : Color.white);
        if (sprite != null)
        {
            _portrait.sprite = sprite;
            _portrait.preserveAspect = true;
        }
        _name = PartyBattleUi.Label(Rect, font, "Name", new Vector2(Width, 34f), new Vector2(0f, 122f), 26, TextAnchor.MiddleCenter);
        _status = PartyBattleUi.Label(Rect, font, "Status", new Vector2(Width, 28f), new Vector2(0f, -46f), 20, TextAnchor.MiddleCenter);
        _hpFill = PartyBattleUi.Bar(Rect, "Hp", new Vector2(0f, -72f), BarWidth, new Color(0.75f, 0.15f, 0.15f, 1f));
        _sanFill = PartyBattleUi.Bar(Rect, "San", new Vector2(0f, -92f), BarWidth, new Color(0.25f, 0.45f, 0.85f, 1f));
        _hpText = PartyBattleUi.Label(Rect, font, "Vitals", new Vector2(Width, 44f), new Vector2(0f, -120f), 16, TextAnchor.MiddleCenter);
        _hpText.lineSpacing = 1.35f; // HP·SAN 두 줄이 붙어 겹쳐 보이던 문제
        back.raycastTarget = true;
        _name.text = BattleTooltipText.UnitName(unit);
        Refresh(unit);
    }

    public void Refresh(IBattleUnit unit)
    {
        PartyMember m = unit as PartyMember;
        EnemyUnit e = unit as EnemyUnit;
        _maxHp = m != null ? m.MaxHp : (e != null ? e.State.FullHp : 1);
        _maxSan = m != null ? m.MaxSan : 0;
        SetBars(unit.Hp, unit.San, false);
        _status.text = BattleTooltipText.StatusLabel(unit);
    }

    /// <summary>막대 길이를 바꾼다. animate 면 짧게 줄어든다.</summary>
    public void SetBars(int hp, int san, bool animate)
    {
        float h = _maxHp <= 0 ? 0f : Mathf.Clamp01((float)hp / _maxHp);
        float s = _maxSan <= 0 ? 0f : Mathf.Clamp01((float)san / _maxSan);
        _hpFill.rectTransform.DOKill();
        _sanFill.rectTransform.DOKill();
        if (animate)
        {
            _hpFill.rectTransform.DOScaleX(h, 0.2f).SetLink(_hpFill.gameObject);
            _sanFill.rectTransform.DOScaleX(s, 0.2f).SetLink(_sanFill.gameObject);
        }
        else
        {
            _hpFill.rectTransform.localScale = new Vector3(h, 1f, 1f);
            _sanFill.rectTransform.localScale = new Vector3(s, 1f, 1f);
        }
        _hpText.text = _maxSan > 0 ? "HP " + hp + "/" + _maxHp + "\nSAN " + san + "/" + _maxSan : "HP " + hp + "/" + _maxHp;
        _sanFill.transform.parent.gameObject.SetActive(_maxSan > 0);
    }

    public void SetStatusText(string text)
    {
        _status.text = text ?? string.Empty;
    }

    public void SetFocus(bool on)
    {
        _outline.color = new Color(1f, 0.85f, 0.3f, on ? 0.95f : 0f);
    }

    public void SetActive(bool on)
    {
        _name.color = on ? new Color(1f, 0.9f, 0.4f) : Color.white;
    }

    public void SetDimmed(bool dimmed)
    {
        Color normal = _portrait.sprite == null ? _baseColor : Color.white;
        _portrait.color = dimmed ? normal * new Color(0.4f, 0.4f, 0.4f, 1f) : normal;
    }

    public void SetAlpha(float a)
    {
        _group.alpha = a;
    }

    public Tween FlashRed(float duration)
    {
        Color keep = _portrait.sprite == null ? _baseColor : Color.white;
        _portrait.DOKill();
        _portrait.color = new Color(1f, 0.25f, 0.25f, 1f);
        return _portrait.DOColor(keep, duration).SetLink(_portrait.gameObject);
    }

    public bool Contains(Vector2 screenPoint)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(Rect, screenPoint, null);
    }
}

/// <summary>PartyBattleView 가 쓰는 uGUI 조립 도우미.</summary>
public static class PartyBattleUi
{
    public static Image Box(RectTransform parent, string name, Vector2 size, Vector2 pos, Color color)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        RectTransform r = (RectTransform)go.transform;
        r.sizeDelta = size;
        r.anchoredPosition = pos;
        Image img = go.GetComponent<Image>();
        img.color = color;
        img.raycastTarget = false;
        return img;
    }

    public static Text Label(RectTransform parent, Font font, string name, Vector2 size, Vector2 pos, int fontSize, TextAnchor align)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Text));
        go.transform.SetParent(parent, false);
        RectTransform r = (RectTransform)go.transform;
        r.sizeDelta = size;
        r.anchoredPosition = pos;
        Text t = go.GetComponent<Text>();
        t.font = font;
        t.fontSize = fontSize;
        t.alignment = align;
        t.color = Color.white;
        t.horizontalOverflow = HorizontalWrapMode.Overflow;
        t.verticalOverflow = VerticalWrapMode.Overflow;
        t.raycastTarget = false;
        return t;
    }

    /// <summary>막대(배경 + 왼쪽 기준으로 줄어드는 채움). 채움 Image 를 돌려준다.</summary>
    public static Image Bar(RectTransform parent, string name, Vector2 pos, float width, Color fill)
    {
        GameObject root = new GameObject(name + "Bar", typeof(RectTransform), typeof(Image));
        root.transform.SetParent(parent, false);
        RectTransform rr = (RectTransform)root.transform;
        rr.sizeDelta = new Vector2(width, 16f);
        rr.anchoredPosition = pos;
        UnityEngine.UI.Image bg = root.GetComponent<UnityEngine.UI.Image>();
        bg.color = new Color(0f, 0f, 0f, 0.7f);
        bg.raycastTarget = false;
        GameObject f = new GameObject(name + "Fill", typeof(RectTransform), typeof(Image));
        f.transform.SetParent(root.transform, false);
        RectTransform fr = (RectTransform)f.transform;
        fr.anchorMin = Vector2.zero;
        fr.anchorMax = Vector2.one;
        fr.pivot = new Vector2(0f, 0.5f);
        fr.offsetMin = new Vector2(2f, 2f);
        fr.offsetMax = new Vector2(-2f, -2f);
        fr.anchorMin = new Vector2(0f, 0f);
        fr.anchorMax = new Vector2(1f, 1f);
        UnityEngine.UI.Image img = f.GetComponent<UnityEngine.UI.Image>();
        img.color = fill;
        img.raycastTarget = false;
        return img;
    }
}
