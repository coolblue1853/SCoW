using SCoW.Core.Sewer;
using UnityEngine;

/// <summary>
/// 하수도 조사 포인트(SEWER-1 S2). 플레이어가 겹친 채 상호작용 키를 누르면 대사 토픽(InSewer) 또는 판정(Rollet -> Judge.json)을 연다.
/// 말풍선은 기존 InteractHintView, 입력은 GameInput.InteractDown. 조사 기록은 SewerState(세이브 포함).
/// 조사한 포인트는 콜라이더가 꺼져 다시 못 누른다. 새 그래픽은 없다(말풍선만).
/// </summary>
public sealed class SewerInvestigatePoint : MonoBehaviour
{
    private const float Width = 3f;
    private const float Height = 6f;

    private static GameObject _root;

    private SewerPoint _def;
    private string _roomId;
    private bool _inside;

    /// <summary>방 데이터대로 포인트를 다시 만든다(이전 포인트는 지운다). 이미 조사한 포인트는 만들지 않는다.</summary>
    public static void Rebuild(SewerRoomDef room)
    {
        if (_root != null)
        {
            foreach (SewerInvestigatePoint old in _root.GetComponentsInChildren<SewerInvestigatePoint>())
            {
                InteractHintView.Exit(old.transform);
            }
            Object.Destroy(_root);
            _root = null;
        }
        if (room == null || room.Points.Count == 0)
        {
            return;
        }
        _root = new GameObject("SewerPoints");
        for (int i = 0; i < room.Points.Count; i++)
        {
            SewerPoint p = room.Points[i];
            if (SewerState.IsLooked(GameRoot.Instance.State, room.Id, p.Id))
            {
                continue;
            }
            GameObject go = new GameObject("SewerPoint_" + p.Id);
            go.transform.SetParent(_root.transform, false);
            go.transform.position = new Vector3(p.X, -193.5f, 0f);
            BoxCollider2D col = go.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
            col.size = new Vector2(Width, Height);
            SewerInvestigatePoint sp = go.AddComponent<SewerInvestigatePoint>();
            sp._def = p;
            sp._roomId = room.Id;
        }
    }

    private void Update()
    {
        if (_inside && DataBaseManager.nowPlace == "InSewer" && GameInput.InteractDown && UiGate.CanInteractWorld && !KeywordPicker.IsOpen)
        {
            Investigate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _inside = true;
            InteractHintView.Enter(transform, SCoW.Core.Ui.HintAction.Interact, _def.Label, 2.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _inside = false;
            InteractHintView.Exit(transform);
        }
    }

    private void Investigate()
    {
        _inside = false;
        InteractHintView.Exit(transform);
        SewerState.Look(GameRoot.Instance.State, _roomId, _def.Id);
        GetComponent<Collider2D>().enabled = false;
        switch (_def.Check)
        {
            case SewerCheck.Observation:
                Rollet.Instance.SetRollet(_def.Topic, "Observation", DataBaseManager.observationPoint, "dialog");
                break;
            case SewerCheck.Luck:
                Rollet.Instance.SetRollet(_def.Topic, "Luck", DataBaseManager.luk, "dialog");
                break;
            case SewerCheck.Intelligence:
                Rollet.Instance.SetRollet(_def.Topic, "Intelligence", DataBaseManager.intl, "dialog");
                break;
            default:
                InteractionController.Instance.InSewerDialog(_def.Topic);
                break;
        }
    }
}
