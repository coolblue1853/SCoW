using System.Collections.Generic;
using SCoW.Core.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 상호작용 힌트 말풍선(UX-9, View). 플레이어가 조사 대상(E/A)이나 포털(F/십자키 위) 범위에 들어서면 대상 머리 위에
/// "[E] Inspect" 를 띄운다. 글리프는 GameInput.LastDevice 에 따라 HintGlyphs 가 정한다. 대상은 Enter/Exit 로 등록하고,
/// 패널·대사·연출 중(UiGate.CanInteractWorld false)이면 숨긴다. 그래픽은 임시(ART-2). 코드로 만들어 씬 수정이 없다.
/// 문구는 영문 임시(TMP 정적 글꼴에 한글 없음). TODO(I2): 새 I2 키로 옮긴다.
/// </summary>
public sealed class InteractHintView : MonoBehaviour
{
    private struct Entry
    {
        public Transform Owner;
        public HintAction Action;
        public string Label;
        public float Height;
    }

    private static readonly List<Entry> Entries = new List<Entry>(4);
    private static InteractHintView _instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        Entries.Clear();
        _instance = null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Boot()
    {
        if (_instance != null)
        {
            return;
        }
        GameObject go = new GameObject("InteractHintView");
        Persist.Keep(go);
        go.AddComponent<InteractHintView>();
    }

    /// <summary>owner 머리 위(height 만큼 위)에 힌트를 올린다. 가장 나중에 등록한 대상이 보인다.</summary>
    public static void Enter(Transform owner, HintAction action, string label, float height)
    {
        if (owner == null)
        {
            return;
        }
        Exit(owner);
        Entry e;
        e.Owner = owner;
        e.Action = action;
        e.Label = label;
        e.Height = height;
        Entries.Add(e);
    }

    public static void Exit(Transform owner)
    {
        for (int i = Entries.Count - 1; i >= 0; i--)
        {
            if (Entries[i].Owner == owner)
            {
                Entries.RemoveAt(i);
            }
        }
    }

    /// <summary>지금 말풍선이 보여야 하는 항목 수(검증용).</summary>
    public static int Count
    {
        get { return Entries.Count; }
    }

    /// <summary>검증용: 현재 말풍선 문구(숨김이면 빈 문자열).</summary>
    public static string CurrentText
    {
        get { return _instance != null && _instance._root.activeSelf ? _instance._text.text : string.Empty; }
    }

    private RectTransform _canvasRect;
    private GameObject _root;
    private RectTransform _rootRect;
    private TextMeshProUGUI _text;
    private string _lastText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        Build();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void LateUpdate()
    {
        Entry top = default(Entry);
        bool has = false;
        for (int i = Entries.Count - 1; i >= 0 && !has; i--)
        {
            if (Entries[i].Owner == null || !Entries[i].Owner.gameObject.activeInHierarchy)
            {
                Entries.RemoveAt(i);
                continue;
            }
            top = Entries[i];
            has = true;
        }
        Camera cam = Camera.main;
        bool show = has && cam != null && UiGate.CanInteractWorld && SettingsService.Current.ShowHints;
        if (_root.activeSelf != show)
        {
            _root.SetActive(show);
        }
        if (!show)
        {
            _lastText = null;
            return;
        }
        string text = "[" + HintGlyphs.Get(top.Action, GameInput.LastDevice) + "] " + top.Label;
        if (text != _lastText)
        {
            _lastText = text;
            _text.text = text;
        }
        Vector3 screen = cam.WorldToScreenPoint(top.Owner.position + Vector3.up * top.Height);
        Vector2 local;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, screen, null, out local);
        _rootRect.anchoredPosition = local;
    }

    private void Build()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 120;
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        _canvasRect = (RectTransform)transform;

        _root = new GameObject("Bubble", typeof(RectTransform), typeof(Image));
        _root.transform.SetParent(transform, false);
        _rootRect = (RectTransform)_root.transform;
        _rootRect.anchorMin = new Vector2(0.5f, 0.5f);
        _rootRect.anchorMax = new Vector2(0.5f, 0.5f);
        _rootRect.pivot = new Vector2(0.5f, 0f);
        _rootRect.sizeDelta = new Vector2(230f, 46f);
        Image bg = _root.GetComponent<Image>();
        bg.color = new Color(0.96f, 0.92f, 0.8f, 0.92f);
        bg.raycastTarget = false;

        GameObject textGo = new GameObject("Text", typeof(RectTransform));
        textGo.transform.SetParent(_root.transform, false);
        _text = textGo.AddComponent<TextMeshProUGUI>();
        _text.fontSize = 26;
        _text.alignment = TextAlignmentOptions.Center;
        _text.color = new Color(0.12f, 0.1f, 0.08f, 1f);
        _text.textWrappingMode = TextWrappingModes.NoWrap;
        _text.raycastTarget = false;
        RectTransform tr = _text.rectTransform;
        tr.anchorMin = Vector2.zero;
        tr.anchorMax = Vector2.one;
        tr.offsetMin = Vector2.zero;
        tr.offsetMax = Vector2.zero;
        _root.SetActive(false);
    }
}
