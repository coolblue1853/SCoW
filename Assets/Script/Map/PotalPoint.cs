using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalPoint : MonoBehaviour
{
    float ChInRommSize = 2.383215f; // -이면 왼쪽 +면 오른쪽
    public GameObject player;
    public GameObject camera;

    bool PlayerInTrigger;
    //강가
    Vector2 player_RtoL = new Vector3(-289.31f, 1.56f);
    Vector3 Cam_RtoL = new Vector3(-309.79f, 1.5f, -15);

    Vector2 player_LtoR = new Vector3(-196.4f, 1.56f);
    Vector3 Cam_LtoR = new Vector3(-173.9f, 1.5f, -15);

    Vector2 player_UtoD = new Vector3(-463.1f, 8.9f);
    Vector3 Cam_UtoD = new Vector3(-468.6f, 1.5f, -15);

    Vector2 player_DtoU = new Vector3(-335.7f, 1.56f);
    Vector3 Cam_DtoU = new Vector3(-328.1f, 1.5f, -15);

    //Client's Houst
    Vector2 player_OutsideTo1st = new Vector3(-1217.79f, 2.13f);
    Vector3 Cam_OutsideTo1st = new Vector3(-1198, 1.5f, -15);

    Vector2 player_1stToOutside = new Vector3(-997.65f, 3.45f);
    Vector3 Cam_1stToOutside = new Vector3(-996.8f, 1.5f, -15);

    Vector2 player_1stTo2st = new Vector3(-1380.6f, 7f);
    Vector3 Cam_1stTo2st = new Vector3(-1368.6f, 1.5f, -15);

    Vector2 player_2stTo1st = new Vector3(-1195.8f, 1.25088f);
    Vector3 Cam_2stTo1st = new Vector3(-1192.4f, 1.5f, -15);

    Vector2 player_RoomTo2st = new Vector3(-1365.4f, 7f);
    Vector3 Cam_RoomTo2st = new Vector3(-1368.6f, 1.5f, -15);

    Vector2 player_2stToRoom = new Vector3(-1567.91f, 2.86f);
    Vector3 Cam_2stTo2Room = new Vector3(-1551.49f, 1.5f, -15);

    // 하수정비시설
    Vector2 player_SewerInside = new Vector3(-1363.46f, -78.46f);
    Vector3 Cam_SewerInside = new Vector3(-1374.97f, -81.1f, -15);
    Vector2 player_SewerOutside = new Vector3(-1214.113f, -81f);
    Vector3 Cam_SewerOutside = new Vector3(-1222.3f, -81.1f, -15);

    // 해당 이동은 강가 하부의 통로를 통해 하수도에 들어갔을 때
    Vector2 player_Sewer = new Vector3(-1241.24f, -193.57f);
    Vector3 Cam_Sewer = new Vector3(-1221.3f, -195f, -15);
    Vector2 player_SewerRight = new Vector3(-1194.19f, -193.57f);
    Vector3 Cam_SewerRight = new Vector3(-1217.02f, -195f, -15);
    Vector2 player_SewerLeft = new Vector3(-1243.64f, -193.57f);
    Vector3 Cam_SewerLeft = new Vector3(-1221.5f, -195f, -15);
    Vector2 player_SecretHouse = new Vector3(-1291.44f, -194.02f);
    Vector3 Cam_SecretHouse = new Vector3(-1303f, -195f, -15);
    Vector2 player_InSewer = new Vector3(-1211.05f, -194.02f);
    Vector3 Cam_InSewer = new Vector3(-1217.72f, -195f, -15);
    public void Update()
    {
        if (PlayerInTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.F) && DataBaseManager.isDirecting == false && DataBaseManager.isActiveDialog1 == false && DataBaseManager.potalWait == false)
            {
                if ((DataBaseManager.nowPlace == "Client'shouse" || DataBaseManager.nowPlace == "NoonEvent") && DataBaseManager.TimeCount >= 6)
                {
                    if (DataBaseManager.NoonEvent_AccessAuthorization == true)
                    {
                        if (DataBaseManager.TimeCount == 6)
                        {
                            if (DataBaseManager.NoonEventFirstDoor == false)
                            {
                                DataBaseManager.NoonEventFirstDoor = true;
                                DataBaseManager.StoryDirecting = true;
                                Invoke("NoonEventFirstDoor", 3f);
                            }
                        }
                        DataBaseManager.isDirecting = true;
                        FadingBackGround.Instance.FadeInOut();
                        Invoke("MovePlayer", 1.5f);
                    }
                    else
                    {
                    }
                }
                else
                {
                    if (DataBaseManager.nowPlace == "Client'shouse" && (DataBaseManager.isFirstDoor == true && DataBaseManager.isFirst2st == true))
                    {
                        if (this.name == "2sttoRoom" && DataBaseManager.AidenKeywordCount < 2)
                        {
                            DataBaseManager.isDirecting = true;
                            FadingBackGround.Instance.FadeInOut();
                            Invoke("MovePlayer", 2);
                        }
                        else if (this.name == "2sttoRoom" && DataBaseManager.AidenKeywordCount >= 2)
                        {
                        }
                        else
                        {
                            DataBaseManager.isDirecting = true;
                            FadingBackGround.Instance.FadeInOut();
                            Invoke("MovePlayer", 2);
                        }
                    }
                    else if (DataBaseManager.nowPlace == "Client'shouse" && this.name == "to1st" && DataBaseManager.isFirstDoor == false)
                    {
                        DataBaseManager.isDirecting = true;
                        DataBaseManager.isFirstDoor = true;
                        DirectingManager.Instance.DoorNocking();
                    }
                    else if (DataBaseManager.nowPlace == "Client'shouse" && this.name == "1stto2st" && DataBaseManager.isFirst2st == false)
                    {
                        DataBaseManager.isDirecting = true;
                        DataBaseManager.isFirst2st = true;
                        DirectingManager.Instance.FirstUpsair();
                    }
                    else if (DataBaseManager.nowPlace == "SewerOffice" && this.name == "GoInside" && DataBaseManager.SewerDoorOpenDeftness == false && DataBaseManager.SewerDoorOpen == false)
                    {
                    }
                    else
                    {
                        DataBaseManager.isDirecting = true;
                        FadingBackGround.Instance.FadeInOut();
                        Invoke("MovePlayer", 1.5f);
                    }
                }
            }
        }
    }

    void NoonEventFirstDoor()
    {
        InteractionController.Instance.Start_2nd_NoonEvent("NoonEvent_FirstDoor");
    }
    void MovePlayer()
    {
        DataBaseManager.potalWait = true;
        //강가
        if (this.name == "RtoL")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_RtoL;
            camera.transform.localPosition = Cam_RtoL;
        }
        else if (this.name == "LtoR")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_LtoR;
            camera.transform.localPosition = Cam_LtoR;
        }
        else if (this.name == "UtoD")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_UtoD;
            camera.transform.localPosition = Cam_UtoD;
        }
        else if (this.name == "DtoU")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_DtoU;
            camera.transform.localPosition = Cam_DtoU;
        }
        //집
        else if (this.name == "to1st")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_OutsideTo1st;
            camera.transform.localPosition = Cam_OutsideTo1st;
        }
        else if (this.name == "toOustside")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_1stToOutside;
            camera.transform.localPosition = Cam_1stToOutside;
        }
        else if (this.name == "1stto2st")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_1stTo2st;
            camera.transform.localPosition = Cam_1stTo2st;
        }
        else if (this.name == "2stto1st")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = false;
            player.transform.localPosition = player_2stTo1st;
            camera.transform.localPosition = Cam_2stTo1st;
        }
        else if (this.name == "Roomto2st")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_RoomTo2st;
            camera.transform.localPosition = Cam_RoomTo2st;
        }
        else if (this.name == "2sttoRoom")
        {
            if (DataBaseManager.isFirstRoom == true || DataBaseManager.TimeCount >= 6)
            {
                DataBaseManager.isDirecting = true;
                player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
                CameraManager.Instance.isCheckEnd = true;
                player.transform.localPosition = player_2stToRoom;
                camera.transform.localPosition = Cam_2stTo2Room;
            }
            else
            {
                DataBaseManager.isDirecting = true;
                player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
                DataBaseManager.isFirstRoom = true;
                CameraManager.Instance.isCheckEnd = true;
                player.transform.localPosition = player_2stToRoom;
                camera.transform.localPosition = Cam_2stTo2Room;
                Invoke("Ex2Room", 2f);
            }
        }
        else if (this.name == "GoInside")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_SewerInside;
            camera.transform.localPosition = Cam_SewerInside;
        }
        else if (this.name == "GoOutside")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_SewerOutside;
            camera.transform.localPosition = Cam_SewerOutside;
        }
        else if (this.name == "Sewer_Potal")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_Sewer;
            camera.transform.localPosition = Cam_Sewer;

            TimeManagere.Instance.Sewer_ByRiverSide();
            DataBaseManager.nowPlace = "InSewer";
            DialogDatabaseManager.instance.Check = true;
            DataBaseManager.StoryDirecting = true;
            Invoke("FirstSewerIn", 2f);
        }
        else if (this.name == "Sewer_Left" || this.name == "Sewer_Right")
        {
            int RandInt = Random.Range(0, 2);
            if (RandInt == 0)
            {
                DataBaseManager.isDirecting = true;
                player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
                CameraManager.Instance.isCheckEnd = true;
                player.transform.localPosition = player_SewerRight;
                camera.transform.localPosition = Cam_SewerRight;
                TimeManagere.Instance.DeletRightSewerPotal();
                TimeManagere.Instance.MakeSewer();
            }
            else
            {
                DataBaseManager.isDirecting = true;
                player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
                CameraManager.Instance.isCheckEnd = true;
                player.transform.localPosition = player_SewerLeft;
                camera.transform.localPosition = Cam_SewerLeft;
                TimeManagere.Instance.DeletLeftSewerPotal();
                TimeManagere.Instance.MakeSewer();
            }
        }
        else if (this.name == "Sewer_Middle")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_SewerLeft;
            camera.transform.localPosition = Cam_SewerLeft;
            TimeManagere.Instance.DeletLeftSewerPotal();
            TimeManagere.Instance.MakeSewer();
        }
        else if (this.name == "MoveSecretHouse")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_SecretHouse;
            camera.transform.localPosition = Cam_SecretHouse;
        }
        else if (this.name == "MoveSewer")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player_InSewer;
            camera.transform.localPosition = Cam_InSewer;


        }
        Invoke("EndIsDerecting", 1f);
        Invoke("AblePotal", 2f);
    }
    public void FirstSewerIn()
    {
        DataBaseManager.Sewer_FirstIn = true;
    }
    public void Force_2st()
    {
        InteractionController.Instance.Start_1st_ClientsHouse("Aiden_key_FailForce");
    }
    public void Ex2Room()
    {
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.Start_1st_ClientsHouse("into_FirstRoom");
    }
    public void Ex_Outto1st()
    {
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = player_OutsideTo1st;
        camera.transform.localPosition = Cam_OutsideTo1st;
    }
    void EndIsDerecting()
    {
        SoundManager.Instance.irRunSoundOn = false;
        DataBaseManager.isDirecting = false;
    }
    void AblePotal()
    {
        DataBaseManager.potalWait = false;
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
