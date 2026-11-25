using SCoW.Core.Sewer;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

/// <summary>
/// 하수도 시야 마스크와 전구 깜빡임(SEWER-1 S3, View). 마스크 텍스처는 코드로 만든 계단형 원이다(새 그림 없음).
/// 규칙·수치는 Core(SewerSight·BulbFlicker). 하수도 안의 맵 화면에서만 켜고, 전투·다른 장소에서는 끈다.
/// SAN 은 VitalsChanged 로만 읽는다. 전구 깜빡임은 설정의 화면 흔들림 끄기를 따른다. 그래픽은 임시(사용자 검토 대기).
/// </summary>
public sealed class SewerSightView : MonoBehaviour
{
    private const int TexSize = 512;
    private const float PollSeconds = 0.25f;
    /// <summary>화면 6px = 도트 1칸(320x180 을 6배, 1080p 기준). 중심·반경을 이 격자에 맞춘다.</summary>
    private const float GridPx = 6f;
    private const float DimRadiusShrink = 0.2f;
    /// <summary>하수도 방은 맵 좌표 x &lt; -1150, y &lt; -150 영역에 있다(SewerFlow 의 도착 좌표 기준). 이 안의 LightBulb 조명만 깜빡인다.</summary>
    private const float SewerAreaMaxX = -1150f;
    private const float SewerAreaMaxY = -150f;

    private static SewerSightView _instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _instance = null;
        DimCount = 0;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Boot()
    {
        if (_instance != null)
        {
            return;
        }
        GameObject go = new GameObject("SewerSightView");
        DontDestroyOnLoad(go);
        go.AddComponent<SewerSightView>();
    }

    /// <summary>마스크가 지금 켜져 있나(검증용).</summary>
    public static bool IsOn
    {
        get { return _instance != null && _instance._on; }
    }

    /// <summary>현재 시야 반경(화면 높이 절반 대비, 검증용). 꺼져 있으면 0.</summary>
    public static float RadiusNow
    {
        get { return _instance != null && _instance._on ? _instance._radius : 0f; }
    }

    /// <summary>현재 전구 밝기 배율(검증용).</summary>
    public static float BulbLevel
    {
        get { return _instance != null ? _instance._level : 1f; }
    }

    /// <summary>지금까지 전구가 깜빡인 횟수(검증용).</summary>
    public static int DimCount { get; private set; }

    /// <summary>켜짐 여부와 SAN 을 즉시 다시 계산한다(진입·이동 직후 호출).</summary>
    public static void Refresh()
    {
        if (_instance != null)
        {
            _instance.Evaluate();
        }
    }

    private readonly BulbFlicker _flicker = new BulbFlicker(UnityRng.Instance);
    private Canvas _canvas;
    private Image _img;
    private RectTransform _rt;
    private Texture2D _tex;
    private Sprite _sprite;
    private SpriteRenderer[] _bulbs;
    private Light2D[] _lights = new Light2D[0];
    private float[] _lightBase = new float[0];
    private Camera _cam;
    private Transform _player;
    private bool _on;
    private bool _bulbOn;
    private bool _wasDim;
    private float _ratio = 1f;
    private float _radius;
    private float _level = 1f;
    private float _poll;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
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
        SetOn(false);
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
        SetOn(false);
        _cam = null;
        _player = null;
        _bulbs = null;
        Evaluate();
    }

    private void OnVitals(int hpDelta, int sanDelta)
    {
        if (sanDelta != 0)
        {
            ReadSan();
            Layout();
        }
    }

    private void Update()
    {
        _poll -= Time.unscaledDeltaTime;
        if (_poll <= 0f)
        {
            _poll = PollSeconds;
            Evaluate();
        }
        if (!_on)
        {
            return;
        }
        _level = _flicker.Tick(Time.unscaledDeltaTime, _ratio, SewerSight.FlickerAllowed(SettingsService.Current));
        bool dim = _level < 1f;
        if (dim && !_wasDim)
        {
            DimCount++;
        }
        _wasDim = dim;
        ApplyBulbLevel();
        Layout();
    }

    private void Evaluate()
    {
        bool inSewer = DataBaseManager.nowPlace == "InSewer";
        bool main = SceneRouter.IsActive(SceneIds.Main);
        bool want = SewerSight.Active(inSewer, main, DataBaseManager.sewerBattleChecker) && FindPlayer();
        if (want)
        {
            ReadSan();
            _bulbOn = AnyBulbOn();
        }
        SetOn(want);
    }

    private void ReadSan()
    {
        _ratio = SewerSight.Ratio(DataBaseManager.nowSan, DataBaseManager.san);
    }

    private bool FindPlayer()
    {
        if (_player == null)
        {
            TimeManagere t = TimeManagere.Instance;
            _player = t != null && t.player != null ? t.player.transform : null;
        }
        if (_cam == null)
        {
            _cam = Camera.main;
        }
        return _player != null && _player.gameObject.activeInHierarchy && _cam != null;
    }

    private bool AnyBulbOn()
    {
        EnsureBulbs();
        for (int i = 0; i < _bulbs.Length; i++)
        {
            if (_bulbs[i] != null && _bulbs[i].gameObject.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }

    private void EnsureBulbs()
    {
        if (_bulbs != null)
        {
            return;
        }
        CollectLights();
        TimeManagere t = TimeManagere.Instance;
        GameObject[] objs = t == null
            ? new GameObject[0]
            : new GameObject[] { t.burbRound1, t.burbRound2, t.burbFlet1, t.burbFlet2 };
        _bulbs = new SpriteRenderer[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _bulbs[i] = objs[i] != null ? objs[i].GetComponent<SpriteRenderer>() : null;
        }
    }

    /// <summary>하수도 영역(맵 좌표)에 있는 전구 조명(Light2D)과 원래 세기를 모은다. 씬 진입·첫 사용 때 한 번만.</summary>
    private void CollectLights()
    {
        Light2D[] all = FindObjectsByType<Light2D>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        System.Collections.Generic.List<Light2D> found = new System.Collections.Generic.List<Light2D>();
        for (int i = 0; i < all.Length; i++)
        {
            Vector3 p = all[i].transform.position;
            if (all[i].lightType == Light2D.LightType.Point && all[i].name.StartsWith("LightBulb", System.StringComparison.Ordinal)
                && p.x < SewerAreaMaxX && p.y < SewerAreaMaxY)
            {
                found.Add(all[i]);
            }
        }
        _lights = found.ToArray();
        _lightBase = new float[_lights.Length];
        for (int i = 0; i < _lights.Length; i++)
        {
            _lightBase[i] = _lights[i].intensity;
        }
    }

    private void ApplyBulbLevel()
    {
        EnsureBulbs();
        for (int i = 0; i < _lights.Length; i++)
        {
            if (_lights[i] != null)
            {
                _lights[i].intensity = _lightBase[i] * _level;
            }
        }
        Color c = new Color(_level, _level, _level, 1f);
        for (int i = 0; i < _bulbs.Length; i++)
        {
            if (_bulbs[i] != null)
            {
                _bulbs[i].color = c;
            }
        }
    }

    private void SetOn(bool on)
    {
        if (on == _on)
        {
            return;
        }
        _on = on;
        if (on)
        {
            EnsureBuilt();
            _flicker.Reset();
            Layout();
        }
        else
        {
            _level = 1f;
            if (_bulbs != null)
            {
                ApplyBulbLevel();
            }
            _flicker.Reset();
        }
        if (_canvas != null)
        {
            _canvas.enabled = on;
        }
    }

    private void Layout()
    {
        if (!_on || _img == null || _player == null || _cam == null)
        {
            return;
        }
        float shrink = 1f - DimRadiusShrink * (1f - _level);
        _radius = SewerSight.Radius(_ratio, _bulbOn) * shrink;
        float radiusPx = Snap(_radius * Screen.height * 0.5f);
        float halfSize = radiusPx / SewerSight.HoleEdge;
        Vector3 sp = _cam.WorldToScreenPoint(_player.position);
        _rt.sizeDelta = new Vector2(halfSize * 2f, halfSize * 2f);
        _rt.anchoredPosition = new Vector2(Snap(sp.x), Snap(sp.y));
        Color c = Color.black;
        c.a = SewerSight.Darkness(_ratio);
        _img.color = c;
    }

    private static float Snap(float v)
    {
        return Mathf.Round(v / GridPx) * GridPx;
    }

    private void EnsureBuilt()
    {
        if (_canvas != null)
        {
            return;
        }
        _canvas = gameObject.AddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        // 씬의 UI 캔버스(0~10)와 대사·HUD 보다 아래: 월드만 어둡게 하고 UI 는 그대로 둔다. 위험 비네트(70)보다도 아래.
        _canvas.sortingOrder = -10;
        GameObject go = new GameObject("SightMask", typeof(RectTransform), typeof(Image));
        go.transform.SetParent(transform, false);
        _rt = (RectTransform)go.transform;
        _rt.anchorMin = Vector2.zero;
        _rt.anchorMax = Vector2.zero;
        _rt.pivot = new Vector2(0.5f, 0.5f);
        _tex = MakeMaskTexture();
        _sprite = Sprite.Create(_tex, new Rect(0, 0, TexSize, TexSize), new Vector2(0.5f, 0.5f), 100f);
        _img = go.GetComponent<Image>();
        _img.sprite = _sprite;
        _img.raycastTarget = false;
    }

    /// <summary>가운데 투명, 바깥으로 4칸 계단으로 어두워지는 흰색 텍스처(색은 Image.color 로 입힌다). 점 필터라 칸이 또렷하다.</summary>
    private static Texture2D MakeMaskTexture()
    {
        Texture2D t = new Texture2D(TexSize, TexSize, TextureFormat.RGBA32, false);
        t.wrapMode = TextureWrapMode.Clamp;
        t.filterMode = FilterMode.Point;
        Color32[] px = new Color32[TexSize * TexSize];
        float half = (TexSize - 1) * 0.5f;
        for (int y = 0; y < TexSize; y++)
        {
            for (int x = 0; x < TexSize; x++)
            {
                float dx = (x - half) / half;
                float dy = (y - half) / half;
                float d = Mathf.Sqrt(dx * dx + dy * dy);
                byte a = (byte)Mathf.RoundToInt(SewerSight.MaskAlpha(d) * 255f);
                px[y * TexSize + x] = new Color32(255, 255, 255, a);
            }
        }
        t.SetPixels32(px);
        t.Apply(false, true);
        return t;
    }
}
