using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalPoint : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;

    bool PlayerInTrigger;
    //Client's Houst
    Vector2 player_OutsideTo1st = new Vector3(-1217.79f, 2.13f);
    Vector3 Cam_OutsideTo1st = new Vector3(-1198, 1.5f, -15);

    Vector2 player_1stToOutside = new Vector3(-997.65f, 3.45f);
    Vector3 Cam_1stToOutside = new Vector3(-996.8f, 1.5f, -15);

    Vector2 player_1stTo2st = new Vector3(-1380.6f, 7f);
    Vector3 Cam_1stTo2st = new Vector3(-1368.6f, 1.5f, -15);

    Vector2 player_2stTo1st = new Vector3(-1195.8f, 3.45f);
    Vector3 Cam_2stTo1st = new Vector3(-1192.4f, 1.5f, -15);

    Vector2 player_RoomTo2st = new Vector3(-1365.4f, 7f);
    Vector3 Cam_RoomTo2st = new Vector3(-1368.6f, 1.5f, -15);


    Vector2 player_2stToRoom = new Vector3(-1567.91f, 2.86f);
    Vector3 Cam_2stTo2Room = new Vector3(-1551.49f, 1.5f, -15);

    public void Update()
    {


  

        if (PlayerInTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.F) && DataBaseManager.isDirecting == false && DataBaseManager.isActiveDialog1 == false)
            {

                if(DataBaseManager.nowPlace == "Client'sHouse" && DataBaseManager.isFirstDoor == true)
                {
                    DataBaseManager.isDirecting = true;
                    FadingBackGround.Instance.FadeInOut();
                    Invoke("MovePlayer", 2);
                }
                else if(DataBaseManager.nowPlace == "Client'sHouse" && DataBaseManager.isFirstDoor == false)
                {
                    DataBaseManager.isDirecting = true;
                    DataBaseManager.isFirstDoor = true;
                    DirectingManager.Instance.DoorNocking();
                }
                else
                {
                    DataBaseManager.isDirecting = true;
                    FadingBackGround.Instance.FadeInOut();
                    Invoke("MovePlayer", 2);
                }
            }
        }
    }


    void MovePlayer()
    {

        if (this.name == "to1st")
        {

            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_OutsideTo1st;
            camera.transform.localPosition = Cam_OutsideTo1st;

        }
        if (this.name == "toOustside")
        {
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_1stToOutside;
            camera.transform.localPosition = Cam_1stToOutside;

        }
        if (this.name == "1stto2st")
        {
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_1stTo2st;
            camera.transform.localPosition = Cam_1stTo2st;

        }
        if (this.name == "2stto1st")
        {
            CameraManager.Instance.isCheckEnd = false;
            player.transform.localPosition = player_2stTo1st;
            camera.transform.localPosition = Cam_2stTo1st;

        }
        if (this.name == "Roomto2st")
        {
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_RoomTo2st;
            camera.transform.localPosition = Cam_RoomTo2st;

        }
        if (this.name == "2sttoRoom")
        {
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_2stToRoom;
            camera.transform.localPosition = Cam_2stTo2Room;

        }
        Invoke("ablePotal", 3);
    }

    public void Ex_Outto1st()
    {
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = player_OutsideTo1st;
        camera.transform.localPosition = Cam_OutsideTo1st;
    }

    void ablePotal()
    {
        DataBaseManager.isDirecting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            PlayerInTrigger = true;

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            PlayerInTrigger = false;

        }
    }

}
