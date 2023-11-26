using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class CamReMove : MonoBehaviour
{
    CameraManager cameraManager;
    // Start is called before the first frame update
    void Start()
    {
        cameraManager = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cameraManager.CameraMove();
        }
    }

}
