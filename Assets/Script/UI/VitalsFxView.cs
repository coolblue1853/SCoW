using DG.Tweening;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HP·SAN 변화 연출(UX-7, View). 전체 화면 비네트 한 장을 코드로 만든다(씬 수정 없음).
/// HP 감소=붉은 플래시 0.15초, SAN 감소=청록 플래시, SAN 이 위험 구간이면 옅은 보라 비네트가 계속 켜진다.
/// 흔들림 설정과 무관(비네트만 남는다). 그래픽은 임시(ART-2).
/// </summary>
public sealed class VitalsFxView : MonoBehaviour
{
    private const float FlashIn = 0.15f;
    private const float FlashOut = 0.35f;
    private const float LowAlpha = 0.35f;

    private static readonly Color HpColor = new Color(0.85f, 0.1f, 0.1f, 1f);
    private static readonly Color SanColor = new Color(0.1f, 0.75f, 0.8f, 1f);
    private static readonly Color LowColor = new Color(0.35f, 0.1f, 0.5f, 1f);

    private static VitalsFxView _instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _instance = null;
        FlashCount = 0;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Boot()
    {
        if (_instance != null)
        {
            return;
        }
        GameObject go = new GameObject("VitalsFxView");
        DontDestroyOnLoad(go);
        go.AddComponent<VitalsFxView>();
    }

    /// <summary>지금까지 나간 플래시 횟수(검증용).</summary>
    public static int FlashCount { get; private set; }

    /// <summary>비네트가 지금 보이는 알파(검증용).</summary>
    public static float ShownAlpha
    {
        get { return _instance != null && _instance._img != null ? _instance._img.color.a : 0f; }
    }

    private Image _img;
    private Texture2D _tex;
    private Sprite _sprite;
    private Tween _tween;
    private float _flashAlpha;
    private Color _flashColor;
    private bool _lowOn;

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

    private void OnEnable()
    {
        BillowUIManager.VitalsChanged += OnVitals;
        SceneRouter.SceneEntered += OnScene;
    }

    private void OnDisable()
    {
        BillowUIManager.VitalsChanged -= OnVitals;
        SceneRouter.SceneEntered -= OnScene;
        KillTween();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
        if (_sprite != null)
        {
            Destroy(_sprite);
        }
        if (_tex != null)
        {
            Destroy(_tex);
        }
    }

    private void OnScene(string sceneId)
    {
        UpdateLow();
    }

    private void OnVitals(int hpDelta, int sanDelta)
    {
        if (!SceneRouter.IsActive(SceneIds.Main))
        {
            UpdateLow();
            return;
        }
        if (hpDelta < 0)
        {
            Flash(HpColor, 0.55f);
        }
        else if (sanDelta < 0)
        {
            Flash(SanColor, 0.5f);
        }
        UpdateLow();
    }

    private void Flash(Color c, float alpha)
    {
        FlashCount++;
        KillTween();
        _flashColor = c;
        _flashAlpha = 0f;
        _tween = DOTween.Sequence()
            .Append(DOTween.To(() => _flashAlpha, v => { _flashAlpha = v; Paint(); }, alpha, FlashIn))
            .Append(DOTween.To(() => _flashAlpha, v => { _flashAlpha = v; Paint(); }, 0f, FlashOut))
            .SetUpdate(true)
            .SetLink(gameObject);
    }

    private void UpdateLow()
    {
        BillowUIManager b = BillowUIManager.Instance;
        _lowOn = b != null && b.Vitals.IsSanLow && SceneRouter.IsActive(SceneIds.Main);
        Paint();
    }

    private void Paint()
    {
        if (_img == null)
        {
            return;
        }
        Color c;
        float a;
        if (_flashAlpha > 0f)
        {
            c = _flashColor;
            a = Mathf.Max(_flashAlpha, _lowOn ? LowAlpha : 0f);
        }
        else
        {
            c = LowColor;
            a = _lowOn ? LowAlpha : 0f;
        }
        c.a = a;
        _img.color = c;
        _img.enabled = a > 0.001f;
    }

    private void KillTween()
    {
        if (_tween != null)
        {
            _tween.Kill();
            _tween = null;
        }
    }

    private void Build()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 70;
        GameObject go = new GameObject("Vignette", typeof(RectTransform), typeof(Image));
        go.transform.SetParent(transform, false);
        RectTransform rt = (RectTransform)go.transform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        _tex = MakeVignetteTexture(64);
        _sprite = Sprite.Create(_tex, new Rect(0, 0, _tex.width, _tex.height), new Vector2(0.5f, 0.5f), 100f);
        _img = go.GetComponent<Image>();
        _img.sprite = _sprite;
        _img.raycastTarget = false;
        _img.enabled = false;
    }

    /// <summary>가운데 투명, 가장자리로 갈수록 불투명한 흰색 텍스처(색은 Image.color 로 입힌다).</summary>
    private static Texture2D MakeVignetteTexture(int size)
    {
        Texture2D t = new Texture2D(size, size, TextureFormat.RGBA32, false);
        t.wrapMode = TextureWrapMode.Clamp;
        t.filterMode = FilterMode.Bilinear;
        Color32[] px = new Color32[size * size];
        float half = (size - 1) * 0.5f;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float dx = (x - half) / half;
                float dy = (y - half) / half;
                float d = Mathf.Sqrt(dx * dx + dy * dy) / 1.4142f;
                float a = Mathf.Clamp01((d - 0.35f) / 0.65f);
                a *= a;
                px[y * size + x] = new Color32(255, 255, 255, (byte)Mathf.RoundToInt(a * 255f));
            }
        }
        t.SetPixels32(px);
        t.Apply(false, true);
        return t;
    }
}
