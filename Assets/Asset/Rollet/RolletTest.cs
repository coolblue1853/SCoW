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
            DataBaseManager.Intel_Ella1 = true;
            NewsRollet();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DataBaseManager.Intel_Aiden1 = true;
            DataBaseManager.Condition = "Good";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DataBaseManager.Intel_Albert1 = true;
            DataBaseManager.Condition = "Best";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DataBaseManager.Intel_Cane1 = true;
            DataBaseManager.Condition = "Bad";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            DataBaseManager.Intel_Meiv1 = true;
            DataBaseManager.Condition = "Worst";
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            DataBaseManager.Intel_Swain1 = true;
            BillowUIManager.Instance.HP_down(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            DataBaseManager.Intel_BlackWell1 = true;
        }
    }

    public void NewsRollet()
    {
        Rollet.Instance.setRollet("신문 : 살펴보기", "자료조사", 80);
    }
}