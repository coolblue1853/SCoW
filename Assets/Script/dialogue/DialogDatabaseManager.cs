using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogDatabaseManager : MonoBehaviour
{
    public bool check = false;
    public static DialogDatabaseManager instance;
    private void Update()
    {
        if (DataBaseManager.isItemUI == false)
        {
            // PlaceDef(Resources/Story/places.json)가 정의한 장소(DetectiveOffice·Bar·Client'shouse·DailyNews·University·Riverside·Hospital·Policeoffice·Slum·NoonEvent)는 데이터로 CSV 를 고른다(C-2 P9).
            string placeCsv = check ? StoryDatabase.ScriptForPlace(DataBaseManager.nowPlace) : null;
            if (placeCsv != null)
            {
                check = false;
                csvFileName = placeCsv;
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "SewerOffice" && check == true)
            {
                check = false;
                csvFileName = "SewerOffice";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "InSewer" && check == true)
            {
                check = false;
                csvFileName = "InSewer";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "BattleRoad" && check == true)
            {
                check = false;
                csvFileName = "1st_Battle";
                SetDialog();
            }
        }
        else
        {
            if (csvFileName != "1st_Item")
            {
                check = true;
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
        if (DataBaseManager.nowPlace == "InSewer")
        {
            csvFileName = "InSewer";
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
