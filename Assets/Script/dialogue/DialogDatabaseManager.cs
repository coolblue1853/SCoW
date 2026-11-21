using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class DialogDatabaseManager : MonoBehaviour
{
    // 장소 CSV 를 다시 읽을 차례인가. 장소 도착 코드가 true 로 만든다. 옛 매 프레임 Update 폴링은 없앴다(C-2 P12):
    // check 를 true 로 만들 때·nowPlace 가 바뀔 때·아이템 UI 가 열리고 닫힐 때 다음 프레임에 한 번 평가한다
    // (장소·시각 같은 같은 프레임의 다른 상태 변경이 끝난 뒤 읽던 옛 타이밍 유지).
    [FormerlySerializedAs("check")]
    [SerializeField] private bool _check = false;
    public bool check
    {
        get { return _check; }
        set
        {
            _check = value;
            if (value) { RequestRefresh(); }
        }
    }
    public static DialogDatabaseManager instance;
    private bool _refreshPending;

    private void OnEnable()
    {
        DataBaseManager.PlaceChanged += RequestRefresh;
        DataBaseManager.ItemUiChanged += RequestRefresh;
        if (_check || DataBaseManager.isItemUI) { RequestRefresh(); }
    }

    private void OnDisable()
    {
        DataBaseManager.PlaceChanged -= RequestRefresh;
        DataBaseManager.ItemUiChanged -= RequestRefresh;
        _refreshPending = false;
    }

    private void RequestRefresh()
    {
        if (_refreshPending || !isActiveAndEnabled) { return; }
        _refreshPending = true;
        StartCoroutine(RefreshNextFrame());
    }

    private IEnumerator RefreshNextFrame()
    {
        yield return null;
        _refreshPending = false;
        Refresh();
    }

    /// <summary>옛 Update 본문. 아이템 UI 가 열려 있으면 아이템 CSV, 아니면 check 가 true 일 때 장소의 CSV(PlaceDef)를 읽는다.</summary>
    private void Refresh()
    {
        if (DataBaseManager.isItemUI == false)
        {
            if (!_check) { return; }
            // PlaceDef(Resources/Story/places.json)가 정의한 장소는 데이터로 CSV 를 고른다(C-2 P9~P12).
            if (!StoryDatabase.PlacesAvailable)
            {
                // places.json 로드 실패: 장소 대사를 고를 수 없다(오류는 StoryDatabase 가 이미 로그). 재시도하지 않도록 내린다.
                Debug.LogError("DialogDatabaseManager: places.json 없음 - 장소 대사 건너뜀: " + DataBaseManager.nowPlace);
                _check = false;
                return;
            }
            string placeCsv = StoryDatabase.ScriptForPlace(DataBaseManager.nowPlace);
            if (placeCsv != null)
            {
                _check = false;
                csvFileName = placeCsv;
                SetDialog();
            }
        }
        else
        {
            if (csvFileName != "1st_Item")
            {
                _check = true;
                csvFileName = "1st_Item";
                SetDialog();
            }
        }
    }
    Dialog[] dialogs;
    void SetDialog()
    {
        dialogs = new Dialog[0];
        dialogDic.Clear();
        DialogPaser theParser = GetComponent<DialogPaser>();
        dialogs = theParser.Prase(csvFileName);
        for (int i = 0; i < dialogs.Length; i++)
        {
            dialogDic.Add(i + 1, dialogs[i]);
        }
        isFinish = true;
    }
    public string csvFileName;
    Dictionary<int, Dialog> dialogDic = new Dictionary<int, Dialog>();
    public static bool isFinish = false;

    private void Awake()
    {
        string placeCsv = StoryDatabase.ScriptForPlace(DataBaseManager.nowPlace);
        if (placeCsv != null)
        {
            csvFileName = placeCsv;
        }
        if (DataBaseManager.nowPlace == "Client'shouse")
        {
            csvFileName = "1st_Client'sHouse";
        }
        if (instance == null)
        {
            instance = this;
            SetDialog();
            isFinish = true;
        }
    }
    public Dialog[] GetDialog(int _startNum, int _EndNum)
    {
        List<Dialog> dialogList = new List<Dialog>();
        for (int i = 0; i <= _EndNum - _startNum; i++)
        {
            dialogList.Add(dialogDic[_startNum + i]);
        }
        return dialogList.ToArray();
    }
}
