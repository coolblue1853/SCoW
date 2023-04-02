using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterctionController : MonoBehaviour
{


    //멜리사 대사

    public GameObject oldProfecer_1stNoon_1stDailog;



    // Start is called before the first frame update

    public DialogManager theDM;
    public DialogManager JudgeDM;


    // Update is called once per frame

    void Start()
    {
        OldProfecer_1stNoon_1stDailog();
    }
    void Update()
    {

    }
    public void OldProfecer_1stNoon_1stDailog()
    {

        if (theDM.isDialogON == false)
        {
            theDM.isDialogON = true;

            theDM.ShowDialog(oldProfecer_1stNoon_1stDailog.transform.GetComponent<interactionEvent>().GetDialogs());

        }
    }
  

}
