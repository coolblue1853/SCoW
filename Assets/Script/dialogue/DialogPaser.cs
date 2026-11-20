using System.Collections.Generic;
using SCoW.Core.Story;
using UnityEngine;

/// <summary>
/// 옛 CSV 대사 파일을 Dialog[] 로 바꾼다. 파싱은 Core 의 StoryCsvParser(P7)가 맡는다(null·열 부족 안전).
/// 옛 표식(@ ^ ⓖ..ⓦ 등)은 해석하지 않고 원문 그대로 둔다 — DialogManager 가 표시할 때 해석한다(옛 동작 그대로).
/// 컴포넌트 이름·메서드 이름(Prase)은 씬 직렬화와 호출부 호환을 위해 유지한다.
/// </summary>
public class DialogPaser : MonoBehaviour
{
    public Dialog[] Prase(string _CVSFileName)
    {
        TextAsset csvData = Resources.Load<TextAsset>(_CVSFileName);
        if (csvData == null)
        {
            Debug.LogError("DialogPaser: CSV 를 찾지 못했다: " + _CVSFileName);
            return new Dialog[0];
        }
        StoryCsvResult result = StoryCsvParser.Parse(csvData.text);
        for (int i = 0; i < result.Errors.Count; i++)
        {
            Debug.LogWarning("DialogPaser " + _CVSFileName + ": " + result.Errors[i]);
        }
        List<Dialog> dialogList = new List<Dialog>(result.Lines.Count);
        for (int i = 0; i < result.Lines.Count; i++)
        {
            StoryLine line = result.Lines[i];
            Dialog dialog = new Dialog();
            dialog.name = line.Speaker;
            dialog.contexts = new string[] { line.Text };
            dialog.spriteName = new string[0];
            dialog.scriptId = _CVSFileName;
            dialog.lineId = line.Id;
            dialogList.Add(dialog);
        }
        return dialogList.ToArray();
    }
}
