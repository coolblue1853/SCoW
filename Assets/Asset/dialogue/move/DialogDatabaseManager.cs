using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDatabaseManager : MonoBehaviour
{
    public bool Check = false;
    public static DialogDatabaseManager instance;
    private void Update()
    {
     if(DataBaseManager.isItemUI == false)
        {
            if (DataBaseManager.nowPlace == "DetectiveOffice" && Check == true)
            {
                Check = false;
                csv_FileName = "1st_DetectiveOffice";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "Client'shouse" && Check == true)
            {
                Check = false;
                csv_FileName = "1st_Client'sHouse";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "DailyNews" && Check == true)
            {
                Check = false;
                csv_FileName = "1st_DailyNews";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "University" && Check == true)
            {
                Check = false;
                csv_FileName = "1st_University";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "Riverside" && Check == true)
            {
                Check = false;
                csv_FileName = "1st_Riverside";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "Hospital" && Check == true)
            {
                Check = false;
                csv_FileName = "1st_Hospital";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "Policeoffice" && Check == true)
            {
                Check = false;
                csv_FileName = "1st_PoliceOffice";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "Slum" && Check == true)
            {
                Check = false;
                csv_FileName = "1st_Slum";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "Bar" && Check == true)
            {
                Check = false;
                csv_FileName = "1st_Bar";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "BattleRoad" && Check == true)
            {
                Check = false;
                csv_FileName = "1st_Battle";
                setDialog();
            }
            else if (DataBaseManager.nowPlace == "NoonEvent" && Check == true)
            {
                Check = false;
                csv_FileName = "2nd_NoonEvent";
                setDialog();
            }
        }
        else
        {
            if(csv_FileName != "1st_Item")
            {
                Check = true;
                csv_FileName = "1st_Item";
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
        dialogs = theParser.Prase(csv_FileName);

        for (int i = 0; i < dialogs.Length; i++)
        {
            dialogDic.Add(i + 1, dialogs[i]);
        }

        isFinish = true;
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
        if (DataBaseManager.nowPlace == "Client'shouse")
        {
            csv_FileName = "1st_Client'sHouse";
        }
        if (DataBaseManager.nowPlace == "DailyNews")
        {
            csv_FileName = "1st_DailyNews";
        }
        if (DataBaseManager.nowPlace == "University")
        {
            csv_FileName = "1st_University";
        }
        if (DataBaseManager.nowPlace == "Riverside")
        {
            csv_FileName = "1st_Riverside";
        }
        if (DataBaseManager.nowPlace == "Hospital")
        {
            csv_FileName = "1st_Hospital";
        }
        else if (DataBaseManager.nowPlace == "Policeoffice")
        {
            csv_FileName = "1st_PoliceOffice";
        }
        else if (DataBaseManager.nowPlace == "Slum")
        {
            csv_FileName = "1st_Slum";
        }
        else if (DataBaseManager.nowPlace == "Bar")
        {
            csv_FileName = "1st_Bar";
        }
        if (instance == null)
        {
            instance = this;
            /*
            DialogPaser theParser = GetComponent<DialogPaser>();
            Dialog[] dialogs = theParser.Prase(csv_FileName);

            for(int i =0; i < dialogs.Length; i++)
            {
                dialogDic.Add(i + 1, dialogs[i]);
            }
            */
            setDialog();
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
