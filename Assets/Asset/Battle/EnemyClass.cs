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
       
      
        public int MP = 0;
        public int FullHP =0;
        public int NowHP =0;
        public int San = 0;

    }

    public class DeepOneHybrid : everyEnemy
    {
        public int DeepOneHybrid_MatialArts = 0;
        public int DeepOneHybrid_Evasion = 0 ;
 

        public void SetStatDeepOneHybrid()
        {
            str = Random.Range(45, 80);
            intl = Random.Range(45, 80);
            dex = Random.Range(45, 80);

            MP = Random.Range(5, 10);
            FullHP = Random.Range(40, 55);
            NowHP = FullHP;
            San = Random.Range(30, 60);
            DeepOneHybrid_MatialArts = 50;
            DeepOneHybrid_Evasion = 25;
        }



    }







    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
