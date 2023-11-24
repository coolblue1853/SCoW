using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDatabaseManager : MonoBehaviour
{
    public bool check = false;
    public static DialogDatabaseManager instance;
    private void Update()
    {
     if(DataBaseManager.isItemUI == false)
        {
            if (DataBaseManager.nowPlace == "DetectiveOffice" && check == true)
            {
                check = false;
                csv_FileName = "1st_DetectiveOffice";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "Client'shouse" && check == true)
            {
                if(DataBaseManager.TimeCount < 6)
                {
                    check = false;
                    csv_FileName = "1st_Client'sHouse";
                    SetDialog();
                }
                else
                {
                    check = false;
                    csv_FileName = "2nd_NoonEvent";
                    SetDialog();
                }
            }
            else if (DataBaseManager.nowPlace == "DailyNews" && check == true)
            {
                check = false;
                csv_FileName = "1st_DailyNews";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "SewerOffice" && check == true)
            {
                check = false;
                csv_FileName = "SewerOffice";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "University" && check == true)
            {
                check = false;
                csv_FileName = "1st_University";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "Riverside" && check == true)
            {
                check = false;
                csv_FileName = "1st_Riverside";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "Hospital" && check == true)
            {
                check = false;
                csv_FileName = "1st_Hospital";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "Policeoffice" && check == true)
            {
                check = false;
                csv_FileName = "1st_PoliceOffice";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "Slum" && check == true)
            {
                check = false;
                csv_FileName = "1st_Slum";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "Bar" && check == true)
            {
                check = false;
                csv_FileName = "1st_Bar";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "InSewer" && check == true)
            {
                check = false;
                csv_FileName = "InSewer";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "BattleRoad" && check == true)
            {
                check = false;
                csv_FileName = "1st_Battle";
                SetDialog();
            }
            else if (DataBaseManager.nowPlace == "NoonEvent" && check == true)
            {
                check = false;
                csv_FileName = "2nd_NoonEvent";
                SetDialog();
            }
        }
        else
        {
            if(csv_FileName != "1st_Item")
            {
                check = true;
                csv_FileName = "1st_Item";
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
        else if (DataBaseManager.nowPlace == "InSewer")
        {
            csv_FileName = "InSewer";
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
        for(int i = 0; i <= _EndNum - _startNum; i++)
        {
            dialogList.Add(dialogDic[_startNum + i]);
        }
        return dialogList.ToArray();
    }
}
