using System.Collections.Generic;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 지도 화면 키보드·패드 포커스(UX-9). 지도가 열려 있고 맨 위 패널일 때, 방향(W/A/S/D·방향키·십자키)으로 목적지 단추를 옮기고
/// E/A 로 누른다. 마우스 호버·클릭은 기존 그대로. 선택 표시는 FocusScope.Highlight → UxOverlay 포커스 링(마우스 사용 중에는 숨김).
/// 문구는 영문 임시. TODO(I2): 새 I2 키로 옮긴다.
/// </summary>
public sealed class MapFocusNav : MonoBehaviour
{
    private static readonly Hint[] MapHints =
    {
        new Hint(HintAction.Navigate, "Move"),
        new Hint(HintAction.Interact, "Go"),
        new Hint(HintAction.Cancel, "Close"),
    };

    private static MapFocusNav _instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        _instance = null;
    }

    /// <summary>지도를 열 때 부른다. root 아래 눌릴 수 있는 단추가 포커스 후보가 된다.</summary>
    public static void Begin(GameObject root)
    {
        if (_instance == null)
        {
            GameObject go = new GameObject("MapFocusNav");
            Persist.Keep(go);
            _instance = go.AddComponent<MapFocusNav>();
        }
        _instance.Open(root);
    }

    public static void End()
    {
        if (_instance != null)
        {
            _instance.Close();
        }
    }

    private readonly List<Button> _buttons = new List<Button>(16);
    private float[] _xs = new float[32];
    private float[] _ys = new float[32];
    private GameObject _root;
    private bool _open;
    private int _index = -1;

    private void Open(GameObject root)
    {
        _root = root;
        _open = true;
        _index = -1;
        HintBar.Push(this, MapHints);
        FocusScope.Enter(this, null);
    }

    private void Close()
    {
        if (!_open)
        {
            return;
        }
        _open = false;
        HintBar.Remove(this);
        FocusScope.Exit(this);
        _index = -1;
    }

    private void Update()
    {
        if (!_open)
        {
            return;
        }
        if (_root == null || !_root.activeInHierarchy)
        {
            Close();
            return;
        }
        if (!UiGate.Stack.IsTop(PanelKind.Map))
        {
            return;
        }
        Collect();
        if (_buttons.Count == 0)
        {
            return;
        }
        if (_index < 0 || _index >= _buttons.Count)
        {
            _index = DirectionalFocus.First(_xs, _ys, _buttons.Count);
        }
        int dx = 0;
        int dy = 0;
        if (GameInput.NavUpDown)
        {
            dy = 1;
        }
        else if (GameInput.NavDownDown)
        {
            dy = -1;
        }
        else if (GameInput.PageNextDown)
        {
            dx = 1;
        }
        else if (GameInput.PagePrevDown)
        {
            dx = -1;
        }
        if (dx != 0 || dy != 0)
        {
            _index = DirectionalFocus.Pick(_xs, _ys, _buttons.Count, _index, dx, dy);
        }
        FocusScope.Highlight(this, _buttons[_index].transform as RectTransform);
        if (GameInput.InteractDown)
        {
            _buttons[_index].onClick.Invoke();
        }
    }

    private void Collect()
    {
        // 포커스 중인 단추를 기억해 두었다가, 목록이 바뀌어도 같은 단추를 유지한다.
        Button keep = _index >= 0 && _index < _buttons.Count ? _buttons[_index] : null;
        _buttons.Clear();
        _root.GetComponentsInChildren(false, _buttons);
        for (int i = _buttons.Count - 1; i >= 0; i--)
        {
            if (!_buttons[i].IsInteractable())
            {
                _buttons.RemoveAt(i);
            }
        }
        if (_xs.Length < _buttons.Count)
        {
            _xs = new float[_buttons.Count * 2];
            _ys = new float[_buttons.Count * 2];
        }
        _index = -1;
        for (int i = 0; i < _buttons.Count; i++)
        {
            Vector3 p = _buttons[i].transform.position;
            _xs[i] = p.x;
            _ys[i] = p.y;
            if (_buttons[i] == keep)
            {
                _index = i;
            }
        }
    }
}
