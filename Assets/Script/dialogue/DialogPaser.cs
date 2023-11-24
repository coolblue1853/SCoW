using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPaser : MonoBehaviour
{
    public Dialog[] Prase(string _CVSFileName)
    {
        List<Dialog> dialogList = new List<Dialog>(); // 대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_CVSFileName); // csv파일 가져옴
        string[] data = csvData.text.Split(new char[] { '\n' });
        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });
            Dialog dialog = new Dialog();
            dialog.name = row[1];
            List<string> contextList = new List<string>();
            List<string> spriteList = new List<string>();
            do
            {
                contextList.Add(row[2]);
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");
            dialog.contexts = contextList.ToArray();
            dialog.spriteName = spriteList.ToArray();
            dialogList.Add(dialog);
        }
        return dialogList.ToArray();
    }
}
