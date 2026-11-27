using System.Collections.Generic;
using SCoW.Core.Sewer;
using SCoW.Core.State;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 하수도 미니맵(SEWER-1 S5, View). 하수도 안 맵 화면에서 오른쪽 위에 방 노드·연결선을 그리고, M·패드 십자 아래·클릭으로 크게/작게 바꾼다.
/// 배치·가시성 규칙은 Core(SewerMapLayout). 그래픽은 코드 생성 임시(단색 사각형·선·TMP 영문 라벨). TODO(I2): 구역 라벨 현지화.
/// 대사·패널·전투 중에는 숨긴다.
/// </summary>
public sealed class SewerMinimapView : MonoBehaviour
{
    private const float PollSeconds = 0.2f;
    private const float SmallStep = 22f;
    private const float SmallNode = 12f;
    private const float LargeStep = 90f;
    private const float LargeNode = 40f;
    private const float Margin = 24f;
    private const float Pad = 16f;

    private static readonly Color ColUnknown = new Color(0.35f, 0.35f, 0.35f, 0.7f);
    private static readonly Color ColVisited = new Color(0.78f, 0.72f, 0.5f, 1f);
    private static readonly Color ColCurrent = new Color(1f, 0.85f, 0.2f, 1f);
    private static readonly Color ColEdge = new Color(0.62f, 0.62f, 0.56f, 0.8f);
    private static readonly Color ColPanel = new Color(0.04f, 0.05f, 0.05f, 0.6f);
    private const float FaintAlpha = 0.4f;

    private static SewerMinimapView _instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _instance = null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Boot()
    {
        if (_instance != null)
        {
            return;
        }
        GameObject go = new GameObject("SewerMinimapView");
        Persist.Keep(go);
        go.AddComponent<SewerMinimapView>();
    }

    /// <summary>미니맵이 지금 보이나(검증용).</summary>
    public static bool IsVisible
    {
        get { return _instance != null && _instance._shown; }
    }

    /// <summary>크게 보기 중인가(검증용).</summary>
    public static bool IsExpanded
    {
        get { return _instance != null && _instance._expanded; }
    }

    /// <summary>마지막으로 그린 노드·라벨 수(검증용).</summary>
    public static int NodeCount
    {
        get { return _instance != null ? _instance._nodes : 0; }
    }

    public static int LabelCount
    {
        get { return _instance != null ? _instance._labels : 0; }
    }

    /// <summary>크게/작게 전환(마우스 클릭·검증용).</summary>
    public static void Toggle()
    {
        if (_instance != null)
        {
            _instance.ToggleSize();
        }
    }

    private Canvas _canvas;
    private RectTransform _panel;
    private Image _panelImg;
    private RectTransform _content;
    private bool _shown;
    private bool _expanded;
    private string _sig;
    private float _poll;
    private int _nodes;
    private int _labels;
    private int _screenH;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void Update()
    {
        _poll -= Time.unscaledDeltaTime;
        bool poll = _poll <= 0f;
        if (poll)
        {
            _poll = PollSeconds;
            SetShown(WantShown());
        }
        if (!_shown)
        {
            return;
        }
        if (GameInput.SewerMinimapDown)
        {
            ToggleSize();
        }
        if (poll || _screenH != Screen.height)
        {
            Rebuild();
        }
    }

    private static bool WantShown()
    {
        if (GameRoot.Instance == null || DataBaseManager.nowPlace != "InSewer")
        {
            return false;
        }
        if (!SceneRouter.IsActive(SceneIds.Main) || DataBaseManager.sewerBattleChecker)
        {
            return false;
        }
        return !DataBaseManager.isActiveDialog1 && !UiGate.AnyOpen;
    }

    private void SetShown(bool on)
    {
        if (on == _shown)
        {
            return;
        }
        _shown = on;
        if (on)
        {
            EnsureBuilt();
            _sig = null;
        }
        else
        {
            _expanded = false;
        }
        if (_canvas != null)
        {
            _canvas.enabled = on;
        }
    }

    private void ToggleSize()
    {
        _expanded = !_expanded;
        _sig = null;
        Rebuild();
    }

    private void EnsureBuilt()
    {
        if (_canvas != null)
        {
            return;
        }
        _canvas = gameObject.AddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        // 씬 UI(0~10)보다 위, 대사 UI(4)와는 겹치지 않게 평소엔 숨기므로 8. 패널·전투 캔버스(20 이상)보다 아래.
        _canvas.sortingOrder = 8;
        gameObject.AddComponent<GraphicRaycaster>();
        GameObject p = new GameObject("Panel", typeof(RectTransform), typeof(Image), typeof(Button));
        p.transform.SetParent(transform, false);
        _panel = (RectTransform)p.transform;
        _panelImg = p.GetComponent<Image>();
        _panelImg.raycastTarget = true;
        Button b = p.GetComponent<Button>();
        b.transition = Selectable.Transition.None;
        b.navigation = new Navigation { mode = Navigation.Mode.None };
        b.onClick.AddListener(ToggleSize);
        GameObject c = new GameObject("Content", typeof(RectTransform));
        c.transform.SetParent(_panel, false);
        _content = (RectTransform)c.transform;
        _content.anchorMin = Vector2.zero;
        _content.anchorMax = Vector2.zero;
        _content.pivot = Vector2.zero;
        _canvas.enabled = false;
    }

    private void Rebuild()
    {
        GameState s = GameRoot.Instance.State;
        string sig = SewerState.Room(s) + "|" + s.GetText(SewerKeys.Visited) + "|" + SewerMapLayout.HasMapItem(s) + "|" + _expanded + "|" + Screen.height;
        if (sig == _sig)
        {
            return;
        }
        _sig = sig;
        _screenH = Screen.height;
        SewerMapLayout m = SewerMapLayout.Build(SewerFlow.Graph, s);
        Draw(m);
    }

    private void Draw(SewerMapLayout m)
    {
        for (int i = _content.childCount - 1; i >= 0; i--)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
        float scale = Mathf.Min(Screen.height / 1080f, Screen.width / 1920f);
        float step = (_expanded ? LargeStep : SmallStep) * scale;
        float node = (_expanded ? LargeNode : SmallNode) * scale;
        float pad = Pad * scale + node * 0.5f;
        float labelRoom = _expanded ? 72f * scale : 0f;
        float w = (m.MaxX - m.MinX) * step + pad * 2f;
        float h = (m.MaxY - m.MinY) * step + pad * 2f + labelRoom;
        _panel.anchorMin = _panel.anchorMax = _expanded ? new Vector2(0.5f, 0.5f) : new Vector2(1f, 1f);
        _panel.pivot = _expanded ? new Vector2(0.5f, 0.5f) : new Vector2(1f, 1f);
        _panel.anchoredPosition = _expanded ? Vector2.zero : new Vector2(-Margin * scale, -Margin * scale);
        _panel.sizeDelta = new Vector2(w, h);
        _panelImg.color = ColPanel;
        _content.anchoredPosition = Vector2.zero;
        Dictionary<string, Vector2> pos = new Dictionary<string, Vector2>();
        for (int i = 0; i < m.Nodes.Count; i++)
        {
            SewerMapNode n = m.Nodes[i];
            // Y 는 아래로 증가하므로 뒤집는다.
            pos[n.Id] = new Vector2(pad + (n.X - m.MinX) * step, pad + (m.MaxY - n.Y) * step);
        }
        for (int i = 0; i < m.Edges.Count; i++)
        {
            Vector2 a = pos[m.Edges[i].A];
            Vector2 b = pos[m.Edges[i].B];
            bool faint = !m.HasMap;
            Color c = ColEdge;
            c.a *= faint ? FaintAlpha : 1f;
            AddLine(a, b, Mathf.Max(2f, node * 0.18f), c);
        }
        for (int i = 0; i < m.Nodes.Count; i++)
        {
            SewerMapNode n = m.Nodes[i];
            Color c = n.State == SewerMapNodeState.Current ? ColCurrent : (n.State == SewerMapNodeState.Visited ? ColVisited : ColUnknown);
            if (n.Faint)
            {
                c.a *= FaintAlpha;
            }
            float size = n.State == SewerMapNodeState.Current ? node * 1.35f : node;
            AddRect(pos[n.Id], new Vector2(size, size), c, 0f);
        }
        _nodes = m.Nodes.Count;
        _labels = m.Labels.Count;
        if (_expanded)
        {
            for (int i = 0; i < m.Labels.Count; i++)
            {
                SewerMapZoneLabel l = m.Labels[i];
                Vector2 p = new Vector2(pad + (l.X - m.MinX) * step, pad + (m.MaxY - l.Y) * step + node * 0.5f + 14f * scale);
                AddText(p, l.Name, 22f * scale, new Color(0.9f, 0.88f, 0.75f, 1f));
            }
            AddText(new Vector2(w * 0.5f, h - 20f * scale), "SEWER MAP", 24f * scale, new Color(0.9f, 0.88f, 0.75f, 0.9f));
        }
    }

    private void AddRect(Vector2 center, Vector2 size, Color color, float angle)
    {
        GameObject go = new GameObject("R", typeof(RectTransform), typeof(Image));
        go.transform.SetParent(_content, false);
        RectTransform rt = (RectTransform)go.transform;
        rt.anchorMin = rt.anchorMax = Vector2.zero;
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = size;
        rt.anchoredPosition = center;
        rt.localRotation = Quaternion.Euler(0f, 0f, angle);
        Image img = go.GetComponent<Image>();
        img.color = color;
        img.raycastTarget = false;
    }

    private void AddLine(Vector2 a, Vector2 b, float thickness, Color color)
    {
        Vector2 d = b - a;
        float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        AddRect((a + b) * 0.5f, new Vector2(d.magnitude, thickness), color, angle);
    }

    private void AddText(Vector2 center, string text, float size, Color color)
    {
        GameObject go = new GameObject("T", typeof(RectTransform));
        go.transform.SetParent(_content, false);
        TextMeshProUGUI t = go.AddComponent<TextMeshProUGUI>();
        t.text = text;
        t.fontSize = size;
        t.alignment = TextAlignmentOptions.Center;
        t.color = color;
        t.textWrappingMode = TextWrappingModes.NoWrap;
        t.raycastTarget = false;
        RectTransform rt = t.rectTransform;
        rt.anchorMin = rt.anchorMax = Vector2.zero;
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(size * 12f, size * 1.4f);
        rt.anchoredPosition = center;
    }
}
