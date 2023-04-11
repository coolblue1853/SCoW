using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{



    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed; //카메라 이동 속도

    private Vector3 targetPosition; // 대상의 현재위치

    public bool isCheckEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    public void CameraMove()
    {
        isCheckEnd = false;
    }

    public void CameraStop()
    {
        isCheckEnd = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "endStage")
        {
            isCheckEnd = true;
        }

    }



    // Update is called once per frame
    void Update()
    {
        if(target.gameObject != null)
        {
            if (isCheckEnd == false)
            {
                targetPosition.Set(target.transform.position.x, this.transform.position.y, this.transform.position.z);
                this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }

        }
        
    }
}
