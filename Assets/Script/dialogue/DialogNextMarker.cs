using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>▼ 위아래 2px 반복(켜질 때 시작, 꺼지면 정리).</summary>
public sealed class DialogNextMarker : MonoBehaviour
{
    private static Sprite _sprite;
    private Tween _tween;
    private float _baseY;

    public static Sprite TriangleSprite
    {
        get
        {
            if (_sprite == null)
            {
                const int w = 28;
                const int h = 20;
                Texture2D t = new Texture2D(w, h, TextureFormat.RGBA32, false);
                t.filterMode = FilterMode.Point;
                Color32[] px = new Color32[w * h];
                for (int y = 0; y < h; y++)
                {
                    // 위쪽(y 큰 쪽)이 넓고 아래로 갈수록 좁아지는 역삼각형.
                    float half = (w * 0.5f) * (y / (float)(h - 1));
                    for (int x = 0; x < w; x++)
                    {
                        bool inside = Mathf.Abs(x + 0.5f - w * 0.5f) <= half;
                        px[y * w + x] = new Color32(255, 255, 255, (byte)(inside ? 255 : 0));
                    }
                }
                t.SetPixels32(px);
                t.Apply(false, true);
                _sprite = Sprite.Create(t, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f), 100f);
            }
            return _sprite;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _sprite = null;
    }

    private void OnEnable()
    {
        RectTransform rt = (RectTransform)transform;
        _baseY = rt.anchoredPosition.y;
        _tween = rt.DOAnchorPosY(_baseY + 4f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true).SetLink(gameObject);
    }

    private void OnDisable()
    {
        if (_tween != null)
        {
            _tween.Kill();
            _tween = null;
        }
        ((RectTransform)transform).anchoredPosition = new Vector2(((RectTransform)transform).anchoredPosition.x, _baseY);
    }
}
