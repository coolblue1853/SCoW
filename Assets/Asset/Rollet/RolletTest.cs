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
            DataBaseManager.Intel_PlanetarySequence1 = true;
            DataBaseManager.Intel_Nightmare1 = true;
            DataBaseManager.Intel_Paranoia1 = true;
            DataBaseManager.Intel_Insomnia1 = true;
            DataBaseManager.Intel_SewerGhostStory1 = true;
            DataBaseManager.Intel_MissingPeople1 = true;
            DataBaseManager.Intel_FishySmell1 = true;
            DataBaseManager.Intel_RiverWaterQuality1 = true;
            DataBaseManager.Intel_CreepyEyes1 = true;
            DataBaseManager.Intel_SewerWorker1 = true;
            DataBaseManager.Intel_Safe1 = true;
            DataBaseManager.Intel_SuddenChange1 = true;
            NewsRollet();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DataBaseManager.Intel_PlanetarySequence2 = true;
            DataBaseManager.Intel_Nightmare2 = true;
            DataBaseManager.Intel_Paranoia2 = true;
            DataBaseManager.Intel_Insomnia2 = true;
            DataBaseManager.Intel_SewerGhostStory2 = true;
            DataBaseManager.Intel_MissingPeople2 = true;
            DataBaseManager.Intel_FishySmell2 = true;
            DataBaseManager.Intel_RiverWaterQuality2 = true;
            DataBaseManager.Intel_CreepyEyes2 = true;
            DataBaseManager.Intel_SewerWorker2 = true;
            DataBaseManager.Intel_Safe2 = true;
            DataBaseManager.Intel_SuddenChange2 = true;
            DataBaseManager.Condition = "Good";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DataBaseManager.Intel_PlanetarySequence3 = true;
            DataBaseManager.Intel_Nightmare3 = true;
            DataBaseManager.Intel_Paranoia3 = true;
            DataBaseManager.Intel_Insomnia3 = true;
            DataBaseManager.Intel_SewerGhostStory3 = true;
            DataBaseManager.Intel_MissingPeople3 = true;
            DataBaseManager.Intel_FishySmell3 = true;
            DataBaseManager.Intel_RiverWaterQuality3 = true;
            DataBaseManager.Intel_CreepyEyes3 = true;
            DataBaseManager.Intel_SewerWorker3 = true;
            DataBaseManager.Intel_Safe3 = true;
            DataBaseManager.Intel_SuddenChange3 = true;
            DataBaseManager.Condition = "Best";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DataBaseManager.Intel_PlanetarySequence4 = true;
            DataBaseManager.Intel_Nightmare4 = true;
            DataBaseManager.Intel_Paranoia4 = true;
            DataBaseManager.Intel_Insomnia4 = true;
            DataBaseManager.Intel_SewerGhostStory4 = true;
            DataBaseManager.Intel_MissingPeople4 = true;
            DataBaseManager.Intel_FishySmell4 = true;
            DataBaseManager.Intel_RiverWaterQuality4 = true;
            DataBaseManager.Intel_CreepyEyes4 = true;
            DataBaseManager.Intel_SewerWorker4 = true;
            DataBaseManager.Intel_Safe4 = true;
            DataBaseManager.Intel_SuddenChange4 = true;
            DataBaseManager.Condition = "Bad";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            DataBaseManager.Intel_PlanetarySequence5 = true;
            DataBaseManager.Intel_Nightmare5 = true;
            DataBaseManager.Intel_Paranoia5 = true;
            DataBaseManager.Intel_Insomnia5 = true;
            DataBaseManager.Intel_SewerGhostStory5 = true;
            DataBaseManager.Intel_MissingPeople5 = true;
            DataBaseManager.Intel_FishySmell5 = true;
            DataBaseManager.Intel_RiverWaterQuality5 = true;
            DataBaseManager.Intel_CreepyEyes5 = true;
            DataBaseManager.Intel_SewerWorker5= true;
            DataBaseManager.Intel_Safe5 = true;
            DataBaseManager.Intel_SuddenChange5 = true;
            DataBaseManager.Condition = "Worst";
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            DataBaseManager.Intel_PlanetarySequence6 = true;
            DataBaseManager.Intel_Nightmare6 = true;
            DataBaseManager.Intel_Paranoia6 = true;
            DataBaseManager.Intel_Insomnia6 = true;
            DataBaseManager.Intel_SewerGhostStory6 = true;
            DataBaseManager.Intel_MissingPeople6 = true;
            DataBaseManager.Intel_FishySmell6 = true;
            DataBaseManager.Intel_RiverWaterQuality6 = true;
            DataBaseManager.Intel_CreepyEyes6 = true;
            DataBaseManager.Intel_SewerWorker6 = true;
            DataBaseManager.Intel_Safe6 = true;
            DataBaseManager.Intel_SuddenChange6 = true;
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