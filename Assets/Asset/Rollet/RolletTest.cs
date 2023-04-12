using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolletTest : MonoBehaviour
{

    // setRollet(string skill, string point_sting, int point_int)

    public int TEXT;
    private void Awake()
    {
        DataBaseManager.str = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.intl = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.dex = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.hp = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.mp = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.san = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.luk = (Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)) * 5;
        DataBaseManager.weal = (Random.Range(5, 26));
        DataBaseManager.nowHP = DataBaseManager.hp;
        DataBaseManager.nowMP = DataBaseManager.mp;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DataBaseManager.Intel_Cane1 = true;
            DataBaseManager.Intel_Aiden1 = true;
            DataBaseManager.Intel_Ella1 = true;
            DataBaseManager.Intel_Swain1 = true;
            DataBaseManager.Intel_BlackWell1 = true;
            DataBaseManager.Intel_Albert1 = true;
            DataBaseManager.Intel_Meiv1 = true;
            NewsRollet();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DataBaseManager.Intel_Cane2 = true;
            DataBaseManager.Intel_Aiden2 = true;
            DataBaseManager.Intel_Ella2 = true;
            DataBaseManager.Intel_Swain2 = true;
            DataBaseManager.Intel_BlackWell2 = true;
            DataBaseManager.Intel_Albert2 = true;
            DataBaseManager.Intel_Meiv2 = true;
            DataBaseManager.Condition = "Good";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DataBaseManager.Intel_Cane3 = true;
            DataBaseManager.Intel_Aiden3 = true;
            DataBaseManager.Intel_Ella3 = true;
            DataBaseManager.Intel_Swain3 = true;
            DataBaseManager.Intel_BlackWell3 = true;
            DataBaseManager.Intel_Albert3 = true;
            DataBaseManager.Intel_Meiv3 = true;
            DataBaseManager.Condition = "Best";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DataBaseManager.Intel_Cane4 = true;
            DataBaseManager.Intel_Aiden4 = true;
            DataBaseManager.Intel_Ella4 = true;
            DataBaseManager.Intel_Swain4 = true;
            DataBaseManager.Intel_BlackWell4 = true;
            DataBaseManager.Intel_Albert4 = true;
            DataBaseManager.Intel_Meiv4 = true;
            DataBaseManager.Condition = "Bad";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            DataBaseManager.Intel_Cane5 = true;
            DataBaseManager.Intel_Aiden5 = true;
            DataBaseManager.Intel_Ella5 = true;
            DataBaseManager.Intel_Swain5 = true;
            DataBaseManager.Intel_BlackWell5 = true;
            DataBaseManager.Intel_Albert5 = true;
            DataBaseManager.Intel_Meiv5 = true;
            DataBaseManager.Condition = "Worst";
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            DataBaseManager.Intel_Cane6 = true;
            DataBaseManager.Intel_Aiden6 = true;
            DataBaseManager.Intel_Ella6 = true;
            DataBaseManager.Intel_Swain6 = true;
            DataBaseManager.Intel_BlackWell6 = true;
            DataBaseManager.Intel_Albert6 = true;
            DataBaseManager.Intel_Meiv6 = true;
            BillowUIManager.Instance.HP_down(5);
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            DataBaseManager.Intel_Aiden1 = true;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            DataBaseManager.Intel_Aiden2 = true;

        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            DataBaseManager.Intel_Aiden3 = true;

        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            DataBaseManager.Intel_Aiden4 = true;

        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            DataBaseManager.Intel_Aiden5 = true;

        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            DataBaseManager.Intel_Aiden6 = true;

        }
    }

    public void NewsRollet()
    {
        Rollet.Instance.setRollet("신문 : 살펴보기", "자료조사", 80);
    }
}