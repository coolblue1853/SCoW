using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDatabaseManager : MonoBehaviour
{
    public static DialogDatabaseManager instance;
    private void Update()
    {
        if (DataBaseManager.nowPlace == "DetectiveOffice")
        {
            csv_FileName = "1st_DetectiveOffice";
        }
        if (DataBaseManager.nowPlace == "Client'sHouse")
        {
            csv_FileName = "1st_Client'sHouse";
        }
    }
    public string csv_FileName;
    Dictionary<int, Dialog> dialogDic = new Dictionary<int, Dialog>();

    public static bool isFinish = false;

    private void Awake()
    {
        if (DataBaseManager.nowPlace == "DetectiveOffice")
        {
            csv_FileName = "1st_DetectiveOffice";
        }
        if (DataBaseManager.nowPlace == "Client'sHouse")
        {
            csv_FileName = "1st_Client'sHouse";
        }
        if (instance == null)
        {
            instance = this;
            DialogPaser theParser = GetComponent<DialogPaser>();
            Dialog[] dialogs = theParser.Prase(csv_FileName);

            for(int i =0; i < dialogs.Length; i++)
            {
                dialogDic.Add(i + 1, dialogs[i]);
            }

            isFinish = true;
        }
    }


    public Dialog[] GetDialog(int _startNum, int _EndNum)
    {
        List<Dialog> dialogList = new List<Dialog>();
        for(int i = 0; i <= _EndNum - _startNum; i++)
        {
            dialogList.Add(dialogDic[_startNum + i]);
        }

        return dialogList.ToArray();
    }
}
