using System.Collections.Generic;
using SCoW.Core.Story;
using UnityEngine;

/// <summary>
/// 데이터 주도 대사의 App 쪽 입구(C-2 P9). Resources/Story/&lt;scriptId&gt;.json(StoryScript)과 places.json(PlaceDef)을 읽어
/// Topic 을 현재 GameState 로 풀고(Pre·OnEnter 효과 적용), 본문 CSV 줄을 Dialog[] 로 돌려준다.
/// CSV·JSON 은 한 번 읽어 캐시한다. 해석 규칙 자체는 Core 의 StoryResolver·PlaceCatalog 에 있다.
/// </summary>
public static class StoryDatabase
{
    public const string ResourceFolder = "Story/";
    public const string PlacesResource = "Story/places";

    private static readonly Dictionary<string, StoryScript> Scripts = new Dictionary<string, StoryScript>();
    private static readonly Dictionary<string, IList<StoryLine>> CsvLines = new Dictionary<string, IList<StoryLine>>();
    private static readonly Dictionary<string, Dictionary<string, Dialog>> CsvDialogs = new Dictionary<string, Dictionary<string, Dialog>>();
    private static PlaceCatalog _places;
    private static bool _placesLoaded;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void ResetStatics()
    {
        Scripts.Clear();
        CsvLines.Clear();
        CsvDialogs.Clear();
        _places = null;
        _placesLoaded = false;
    }

    /// <summary>장소 → 대사 스크립트(PlaceDef). 없으면 null(옛 분기 그대로 쓴다).</summary>
    public static string ScriptForPlace(string placeId)
    {
        PlaceCatalog cat = Places();
        if (cat == null || placeId == null || !cat.Places.ContainsKey(placeId)) { return null; }
        return cat.ScriptFor(placeId, new GameStateAccess(GameRoot.Instance.State));
    }

    /// <summary>
    /// scriptId 의 topic 을 풀어 Dialog[] 를 돌려준다. 맞는 Topic·Case 가 없거나 데이터 오류면 false(오류는 콘솔).
    /// true 면 Pre·OnEnter 효과가 이미 GameState 에 적용됐다.
    /// </summary>
    public static bool TryRun(string scriptId, string topicId, out Dialog[] dialogs)
    {
        dialogs = null;
        StoryScript script = LoadScript(scriptId);
        if (script == null) { return false; }
        GameStateAccess state = new GameStateAccess(GameRoot.Instance.State);
        List<string> errors = new List<string>();
        StoryResolution res = StoryResolver.Resolve(script, topicId, state, state, Lines, errors);
        for (int i = 0; i < errors.Count; i++)
        {
            Debug.LogError("StoryDatabase " + scriptId + "/" + topicId + ": " + errors[i]);
        }
        if (res == null) { return false; }
        dialogs = new Dialog[res.Lines.Count];
        for (int i = 0; i < dialogs.Length; i++)
        {
            dialogs[i] = CsvDialogs[res.Lines[i].File][res.Lines[i].Id];
        }
        return true;
    }

    private static PlaceCatalog Places()
    {
        if (_placesLoaded) { return _places; }
        _placesLoaded = true;
        TextAsset ta = Resources.Load<TextAsset>(PlacesResource);
        if (ta == null)
        {
            Debug.LogError("StoryDatabase: places.json 없음");
            return null;
        }
        PlaceCatalog cat = PlaceCatalog.Load(ta.text);
        for (int i = 0; i < cat.Errors.Count; i++) { Debug.LogError("StoryDatabase places: " + cat.Errors[i]); }
        _places = cat;
        return cat;
    }

    private static StoryScript LoadScript(string scriptId)
    {
        StoryScript sc;
        if (Scripts.TryGetValue(scriptId, out sc)) { return sc; }
        Scripts[scriptId] = null; // 실패도 캐시해 매 호출 로그가 반복되지 않게 한다(성공하면 아래에서 덮어씀)
        TextAsset ta = Resources.Load<TextAsset>(ResourceFolder + scriptId);
        if (ta == null)
        {
            Debug.LogError("StoryDatabase: 스크립트 JSON 없음: " + scriptId);
            return null;
        }
        StoryLoadResult r = StoryScriptLoader.Load(ta.text);
        for (int i = 0; i < r.Errors.Count; i++) { Debug.LogError("StoryDatabase " + scriptId + ": " + r.Errors[i]); }
        Scripts[scriptId] = r.Script;
        return r.Script;
    }

    private static IList<StoryLine> Lines(string csvName)
    {
        IList<StoryLine> lines;
        if (CsvLines.TryGetValue(csvName, out lines)) { return lines; }
        TextAsset ta = Resources.Load<TextAsset>(csvName);
        if (ta == null) { return null; }
        StoryCsvResult r = StoryCsvParser.Parse(ta.text);
        for (int i = 0; i < r.Errors.Count; i++) { Debug.LogWarning("StoryDatabase " + csvName + ": " + r.Errors[i]); }
        Dictionary<string, Dialog> byId = new Dictionary<string, Dialog>();
        for (int i = 0; i < r.Lines.Count; i++)
        {
            byId[r.Lines[i].Id] = DialogPaser.ToDialog(r.Lines[i], csvName);
        }
        CsvLines[csvName] = r.Lines;
        CsvDialogs[csvName] = byId;
        return r.Lines;
    }
}
