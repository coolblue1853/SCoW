using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TimeManagere : MonoBehaviour
{

    public LightColorController lightColorController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public GameObject BFNoonEvent;
    public GameObject AFNoonEvent;
    public GameObject Witness;
    public GameObject DeadBody;
    public GameObject Site;

    void Update()
    {
        if(DataBaseManager.TimeCount >= 6 && AFNoonEvent.transform.localPosition.y > 30)
        {
            BFNoonEvent.transform.localPosition = new Vector3(0, 60, 0);
            AFNoonEvent.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (DataBaseManager.TimeCount >= 7 && Witness.activeSelf == true)
        {

            Witness.SetActive(false);
            DeadBody.SetActive(false);
            Site.transform.localPosition = new Vector3(-250.6001f, 2.840187f, 0);

  
        }
        if ((DataBaseManager.TimeCount % 4 )== 1)
        {
            lightColorController.time = 0.2f;
        }
        if ((DataBaseManager.TimeCount % 4) == 2)
        {
            lightColorController.time = 0.4f;
        }
        if ((DataBaseManager.TimeCount % 4) == 3)
        {
            lightColorController.time = 0.64f;
        }
        if ((DataBaseManager.TimeCount % 4) == 0)
        {
            lightColorController.time = 0.9f;
        }
    }
}
