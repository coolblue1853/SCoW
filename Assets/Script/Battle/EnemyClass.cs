using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyClass : MonoBehaviour
{
    public class everyEnemy : MonoBehaviour
    {
        public int str = 0;
        public int intl = 0;
        public int dex = 0;
        public int mp = 0;
        public int fullHP = 0;
        public int nowHP = 0;
        public int san = 0;
    }
    public class DeepOneHybrid : everyEnemy
    {
        public int deepOneHybridMatialArts = 0;
        public int deepOneHybridEvasion = 0;
        public void SetStatDeepOneHybrid()
        {
            str = Random.Range(45, 80);
            intl = Random.Range(45, 80);
            dex = Random.Range(45, 80);
            mp = Random.Range(5, 10);
            fullHP = Random.Range(40, 55);
            nowHP = fullHP;
            san = Random.Range(30, 60);
            deepOneHybridMatialArts = 50;
            deepOneHybridEvasion = 25;
        }
    }
    public class DeepOne : everyEnemy
    {
        public int deepOneMatialArts = 0;
        public int deepOneSewerdManShip = 0;
        public int deepOneEvasion = 0;
        public void SetStatDeepOne()
        {
            str = Random.Range(60, 80);
            intl = Random.Range(55, 75);
            dex = Random.Range(45, 55);
            mp = Random.Range(40, 60);
            fullHP = Random.Range(100, 120);
            nowHP = fullHP;
            san = Random.Range(30, 60);
            deepOneMatialArts = 45;
            deepOneSewerdManShip = 50;
            deepOneEvasion = 25;
        }

    }
}
