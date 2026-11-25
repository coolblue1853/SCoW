using System.Collections;
using System.Collections.Generic;
using SCoW.Core.State;
using SCoW.Core.World;
using UnityEngine;

/// <summary>
/// 월드 동기화(C-2 P13): 상태 → 씬 오브젝트 목표 상태. 옛 TimeManagere.Update·BulbCheck 의 매 프레임 실행을 대신한다.
/// WorldRules(Core 표)가 규칙을, 이 컴포넌트가 씬 연결(TimeManagere 의 인스펙터 참조)을 맡는다. TimeManagere.Awake 가 같은 오브젝트에 붙인다(씬·프리팹 변경 없음).
/// GameRoot.StateChanged 에서 월드에 영향을 주는 키(WorldRules.Affects)가 바뀌었을 때만, 같은 프레임의 다른 상태 변경이 끝난 다음 프레임에 한 번 평가한다.
/// 상태를 통째로 바꿀 때(키 null)와 켜질 때도 한 번 평가한다.
/// </summary>
public sealed class WorldBinder : MonoBehaviour, IWorldScene
{
    private TimeManagere _refs;
    private readonly Dictionary<string, GameObject> _objects = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, Intel_ObtoUI> _intels = new Dictionary<string, Intel_ObtoUI>();
    private bool _pending;
    private bool _evaluating;
    private bool _valid;
    // 안전망(P13 리뷰 권장): 옛 코드는 매 프레임 덮어써서 판정 후 false 로 굳은 canJudge(하수도 창문·사물함 등)를
    // 밤마다 되살렸다. 알림만으로는 그 복구가 없으므로 0.5초마다 한 번 재평가해 옛 동작에 맞춘다(평가는 순수·가벼움).
    private const float SafetyNetInterval = 0.5f;
    private static readonly WaitForSeconds SafetyNetWait = new WaitForSeconds(SafetyNetInterval);
    private Coroutine _safetyNet;

    private void Awake()
    {
        _refs = GetComponent<TimeManagere>();
        BuildMap();
    }

    private void OnEnable()
    {
        GameRoot.StateChanged += OnStateChanged;
        RequestEvaluate();
        _safetyNet = StartCoroutine(SafetyNetLoop());
    }

    private void OnDisable()
    {
        GameRoot.StateChanged -= OnStateChanged;
        _pending = false;
        if (_safetyNet != null)
        {
            StopCoroutine(_safetyNet);
            _safetyNet = null;
        }
    }

    private IEnumerator SafetyNetLoop()
    {
        while (true)
        {
            yield return SafetyNetWait;
            if (!_pending)
            {
                EvaluateNow();
            }
        }
    }

    private void OnStateChanged(string key)
    {
        // 평가 중 스스로 바꾼 상태(문 열림 플래그 등)는 다시 평가하지 않는다(평가는 같은 입력에 같은 결과라 한 번이면 충분).
        if (_evaluating) { return; }
        if (key == null || WorldRules.Affects(key)) { RequestEvaluate(); }
    }

    private void RequestEvaluate()
    {
        if (_pending || !isActiveAndEnabled) { return; }
        _pending = true;
        StartCoroutine(EvaluateNextFrame());
    }

    private IEnumerator EvaluateNextFrame()
    {
        yield return null;
        _pending = false;
        EvaluateNow();
    }

    /// <summary>지금 상태로 월드를 한 번 평가한다(동치 도구·테스트 진입점).</summary>
    public void EvaluateNow()
    {
        if (_refs == null || !_valid) { return; }
        _evaluating = true;
        try
        {
            WorldRules.Apply(GameRoot.Instance.State, this);
        }
        finally
        {
            _evaluating = false;
        }
    }

    private void BuildMap()
    {
        TimeManagere r = _refs;
        if (r == null) { return; }
        _objects[WorldIds.DetectiveNightBulb] = r.detectiveNightBulb;
        _objects[WorldIds.EndDoorDetective] = r.endDoorDetective;
        _objects[WorldIds.Manhole] = r.Manhole;
        _objects[WorldIds.RiverActive] = r.riverActive;
        _objects[WorldIds.RiverPotal] = r.riverPotal;
        _objects[WorldIds.BlackWellOb] = r.blackWellOb;
        _objects[WorldIds.SewerDoorPotal] = r.sewerDoorPotal;
        _objects[WorldIds.SewerDoorDialog] = r.sewerDoorDialog;
        _objects[WorldIds.BfNoonEvent] = r.bfNoonEvent;
        _objects[WorldIds.AfNoonEvent] = r.afNoonEvent;
        _objects[WorldIds.PoliceLine] = r.policeLine;
        _objects[WorldIds.Witness] = r.witness;
        _objects[WorldIds.DeadBody] = r.deadBody;
        _objects[WorldIds.Site] = r.site;
        _objects[WorldIds.Kane] = r.kane;
        _objects[WorldIds.Student] = r.student;
        _objects[WorldIds.BarPeople1] = r.barPeople1;
        _objects[WorldIds.BarPeople2] = r.barPeople2;
        _objects[WorldIds.Albert] = r.albert;
        _objects[WorldIds.MeaveNpc] = r.meave != null ? r.meave.gameObject : null;
        _objects[WorldIds.Dave] = r.dave;
        _objects[WorldIds.Swain] = r.swain;
        _objects[WorldIds.Journalist] = r.journalist;
        _intels[WorldIds.BlackWell] = r.blackWell;
        _intels[WorldIds.SewerOfficeWindow] = r.sewerOfficeWindow;
        _intels[WorldIds.SewerOfficeLocker] = r.sewerOfficeLocker;
        _intels[WorldIds.Meave] = r.meave;
        _valid = r.lightColorController != null && r.clientsHouseRoom != null;
        foreach (KeyValuePair<string, GameObject> kv in _objects)
        {
            if (kv.Value == null) { Debug.LogError("WorldBinder: TimeManagere 참조 없음 - " + kv.Key + " (월드 동기화 중단)"); _valid = false; }
        }
        foreach (KeyValuePair<string, Intel_ObtoUI> kv in _intels)
        {
            if (kv.Value == null) { Debug.LogError("WorldBinder: TimeManagere 참조 없음 - " + kv.Key + " (월드 동기화 중단)"); _valid = false; }
        }
    }

    private GameObject Obj(string id)
    {
        return _objects[id];
    }

    public bool IsActive(string id) { return Obj(id).activeSelf; }

    public void SetActive(string id, bool active) { Obj(id).SetActive(active); }

    public void MoveTo(string id, float x, float y, float z) { Obj(id).transform.localPosition = new Vector3(x, y, z); }

    public float LocalY(string id) { return Obj(id).transform.localPosition.y; }

    public bool GetCanJudge(string id) { return _intels[id].canJudge; }

    public void SetCanJudge(string id, bool value) { _intels[id].canJudge = value; }

    public bool GetCanKeyword(string id) { return _intels[id].canKeyword; }

    public void SetCanKeyword(string id, bool value) { _intels[id].canKeyword = value; }

    public void SetLightTime(float time) { _refs.lightColorController.time = time; }

    public void SetClientsHouseRoomMessy() { _refs.clientsHouseRoom.sprite = _refs.messClientsHouseRoom; }
}
