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
            if (DataBaseManager.nowPlace == "DetectiveOffice" && check == true)
            {
                check = false;
                csvFileName = "1st_DetectiveOffice";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "Client'shouse" && check == true)
            {
                if (DataBaseManager.timeCount < 6)
                {
                    check = false;
                    csvFileName = "1st_Client'sHouse";
                    setDialog();
                }
                else
                {
                    check = false;
                    csvFileName = "2nd_NoonEvent";
                    setDialog();
                }
            }
            else if (DataBaseManager.nowPlace == "DailyNews" && check == true)
            {
                check = false;
                csvFileName = "1st_DailyNews";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "SewerOffice" && check == true)
            {
                check = false;
                csvFileName = "SewerOffice";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "University" && check == true)
            {
                check = false;
                csvFileName = "1st_University";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "Riverside" && check == true)
            {
                check = false;
                csvFileName = "1st_Riverside";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "Hospital" && check == true)
            {
                check = false;
                csvFileName = "1st_Hospital";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "Policeoffice" && check == true)
            {
                check = false;
                csvFileName = "1st_PoliceOffice";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "Slum" && check == true)
            {
                check = false;
                csvFileName = "1st_Slum";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "Bar" && check == true)
            {
                check = false;
                csvFileName = "1st_Bar";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "InSewer" && check == true)
            {
                check = false;
                csvFileName = "InSewer";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "BattleRoad" && check == true)
            {
                check = false;
                csvFileName = "1st_Battle";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "NoonEvent" && check == true)
            {
                check = false;
                csvFileName = "2nd_NoonEvent";
                setDialog();
            }
        }
        else
        {
            if (csvFileName != "1st_Item")
            {
                check = true;
                csvFileName = "1st_Item";
                setDialog();
            }
        }
    }
    Dialog[] dialogs;
    void setDialog()
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
        if (DataBaseManager.nowPlace == "DetectiveOffice")
        {
            csvFileName = "1st_DetectiveOffice";
        }
        if (DataBaseManager.nowPlace == "Client'shouse")
        {
            csvFileName = "1st_Client'sHouse";
        }
        if (DataBaseManager.nowPlace == "DailyNews")
        {
            csvFileName = "1st_DailyNews";
        }
        if (DataBaseManager.nowPlace == "University")
        {
            csvFileName = "1st_University";
        }
        if (DataBaseManager.nowPlace == "Riverside")
        {
            csvFileName = "1st_Riverside";
        }
        if (DataBaseManager.nowPlace == "Hospital")
        {
            csvFileName = "1st_Hospital";
        }
        else if (DataBaseManager.nowPlace == "Policeoffice")
        {
            csvFileName = "1st_PoliceOffice";
        }
        else if (DataBaseManager.nowPlace == "Slum")
        {
            csvFileName = "1st_Slum";
        }
        else if (DataBaseManager.nowPlace == "Bar")
        {
            csvFileName = "1st_Bar";
        }
        else if (DataBaseManager.nowPlace == "InSewer")
        {
            csvFileName = "InSewer";
        }
        if (instance == null)
        {
            instance = this;
            setDialog();
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
