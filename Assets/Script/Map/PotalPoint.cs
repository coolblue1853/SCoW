using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PotalPoint : MonoBehaviour
{
    float chInRommSize = 2.383215f; // -이면 왼쪽 +면 오른쪽
    public GameObject player;
    public GameObject camera;
    bool playerInTrigger;
    //강가
    Vector2 playerRtoL = new Vector3(-289.31f, 1.56f);
    Vector3 camRtoL = new Vector3(-309.79f, 1.5f, -15);
    Vector2 playerLtoR = new Vector3(-196.4f, 1.56f);
    Vector3 camLtoR = new Vector3(-173.9f, 1.5f, -15);
    Vector2 playerUtoD = new Vector3(-463.1f, 8.9f);
    Vector3 camUtoD = new Vector3(-468.6f, 1.5f, -15);
    Vector2 playerDtoU = new Vector3(-335.7f, 1.56f);
    Vector3 camDtoU = new Vector3(-328.1f, 1.5f, -15);
    //Client's Houst
    Vector2 playerOutsideTo1st = new Vector3(-1217.79f, 2.13f);
    Vector3 camOutsideTo1st = new Vector3(-1198, 1.5f, -15);
    Vector2 player1stToOutside = new Vector3(-997.65f, 3.45f);
    Vector3 cam1stToOutside = new Vector3(-996.8f, 1.5f, -15);
    Vector2 player1stTo2st = new Vector3(-1380.6f, 7f);
    Vector3 cam1stTo2st = new Vector3(-1368.6f, 1.5f, -15);
    Vector2 player2stTo1st = new Vector3(-1195.8f, 1.25088f);
    Vector3 cam2stTo1st = new Vector3(-1192.4f, 1.5f, -15);
    Vector2 playerRoomTo2st = new Vector3(-1365.4f, 7f);
    Vector3 camRoomTo2st = new Vector3(-1368.6f, 1.5f, -15);
    Vector2 player2stToRoom = new Vector3(-1567.91f, 2.86f);
    Vector3 cam2stTo2Room = new Vector3(-1551.49f, 1.5f, -15);

    // 하수정비시설
    Vector2 playerSewerInside = new Vector3(-1363.46f, -78.46f);
    Vector3 camSewerInside = new Vector3(-1374.97f, -81.1f, -15);
    Vector2 playerSewerOutside = new Vector3(-1214.113f, -81f);
    Vector3 camSewerOutside = new Vector3(-1222.3f, -81.1f, -15);

    // 해당 이동은 강가 하부의 통로를 통해 하수도에 들어갔을 때
    Vector2 playerSewer = new Vector3(-1241.24f, -193.57f);
    Vector3 camSewer = new Vector3(-1221.3f, -195f, -15);
    Vector2 playerSewerRight = new Vector3(-1194.19f, -193.57f);
    Vector3 camSewerRight = new Vector3(-1217.02f, -195f, -15);
    Vector2 playerSewerLeft = new Vector3(-1243.64f, -193.57f);
    Vector3 camSewerLeft = new Vector3(-1221.5f, -195f, -15);
    Vector2 playerSecretHouse = new Vector3(-1291.44f, -194.02f);
    Vector3 camSecretHouse = new Vector3(-1303f, -195f, -15);
    Vector2 playerInSewer = new Vector3(-1211.05f, -194.02f);
    Vector3 camInSewer = new Vector3(-1217.72f, -195f, -15);
    public void Update()
    {
        if (playerInTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.F) && DataBaseManager.isDirecting == false && DataBaseManager.isActiveDialog1 == false && DataBaseManager.potalWait == false)
            {
                if ((DataBaseManager.nowPlace == "Client'shouse" || DataBaseManager.nowPlace == "NoonEvent") && DataBaseManager.timeCount >= 6)
                {
                    if (DataBaseManager.noonEventAccessAuthorization == true)
                    {
                        if (DataBaseManager.timeCount == 6)
                        {
                            if (DataBaseManager.noonEventFirstDoor == false)
                            {
                                DataBaseManager.noonEventFirstDoor = true;
                                DataBaseManager.storyDirecting = true;
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
                        if (this.name == "2sttoRoom" && DataBaseManager.aidenKeywordCount < 2)
                        {
                            DataBaseManager.isDirecting = true;
                            FadingBackGround.Instance.FadeInOut();
                            Invoke("MovePlayer", 2);
                        }
                        else if (this.name == "2sttoRoom" && DataBaseManager.aidenKeywordCount >= 2)
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
                    else if (DataBaseManager.nowPlace == "SewerOffice" && this.name == "GoInside" && DataBaseManager.sewerDoorOpenDeftness == false && DataBaseManager.sewerDoorOpen == false)
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
        InteractionController.Instance.Start2ndNoonEvent("NoonEvent_FirstDoor");
    }
    void MovePlayer()
    {
        DataBaseManager.potalWait = true;
        //강가
        if (this.name == "RtoL")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = playerRtoL;
            camera.transform.localPosition = camRtoL;
        }
        else if (this.name == "LtoR")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = playerLtoR;
            camera.transform.localPosition = camLtoR;
        }
        else if (this.name == "UtoD")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = playerUtoD;
            camera.transform.localPosition = camUtoD;
        }
        else if (this.name == "DtoU")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = playerDtoU;
            camera.transform.localPosition = camDtoU;
        }
        //집
        else if (this.name == "to1st")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = playerOutsideTo1st;
            camera.transform.localPosition = camOutsideTo1st;
        }
        else if (this.name == "toOustside")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player1stToOutside;
            camera.transform.localPosition = cam1stToOutside;
        }
        else if (this.name == "1stto2st")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = player1stTo2st;
            camera.transform.localPosition = cam1stTo2st;
        }
        else if (this.name == "2stto1st")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = false;
            player.transform.localPosition = player2stTo1st;
            camera.transform.localPosition = cam2stTo1st;
        }
        else if (this.name == "Roomto2st")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = playerRoomTo2st;
            camera.transform.localPosition = camRoomTo2st;
        }
        else if (this.name == "2sttoRoom")
        {
            if (DataBaseManager.isFirstRoom == true || DataBaseManager.timeCount >= 6)
            {
                DataBaseManager.isDirecting = true;
                player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
                CameraManager.Instance.isCheckEnd = true;
                player.transform.localPosition = player2stToRoom;
                camera.transform.localPosition = cam2stTo2Room;
            }
            else
            {
                DataBaseManager.isDirecting = true;
                player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
                DataBaseManager.isFirstRoom = true;
                CameraManager.Instance.isCheckEnd = true;
                player.transform.localPosition = player2stToRoom;
                camera.transform.localPosition = cam2stTo2Room;
                Invoke("Ex2Room", 2f);
            }
        }
        else if (this.name == "GoInside")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = playerSewerInside;
            camera.transform.localPosition = camSewerInside;
        }
        else if (this.name == "GoOutside")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = playerSewerOutside;
            camera.transform.localPosition = camSewerOutside;
        }
        else if (this.name == "Sewer_Potal")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = playerSewer;
            camera.transform.localPosition = camSewer;

            TimeManagere.Instance.SewerByRiverSide();
            DataBaseManager.nowPlace = "InSewer";
            DialogDatabaseManager.instance.check = true;
            DataBaseManager.storyDirecting = true;
            Invoke("FirstSewerIn", 2f);
        }
        else if (this.name == "Sewer_Left" || this.name == "Sewer_Right")
        {
            int RandInt = Random.Range(0, 2);
            if (RandInt == 0)
            {
                DataBaseManager.isDirecting = true;
                player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
                CameraManager.Instance.isCheckEnd = true;
                player.transform.localPosition = playerSewerRight;
                camera.transform.localPosition = camSewerRight;
                TimeManagere.Instance.DeletRightSewerPotal();
                TimeManagere.Instance.MakeSewer();
            }
            else
            {
                DataBaseManager.isDirecting = true;
                player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
                CameraManager.Instance.isCheckEnd = true;
                player.transform.localPosition = playerSewerLeft;
                camera.transform.localPosition = camSewerLeft;
                TimeManagere.Instance.DeletLeftSewerPotal();
                TimeManagere.Instance.MakeSewer();
            }
        }
        else if (this.name == "Sewer_Middle")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = playerSewerLeft;
            camera.transform.localPosition = camSewerLeft;
            TimeManagere.Instance.DeletLeftSewerPotal();
            TimeManagere.Instance.MakeSewer();
        }
        else if (this.name == "MoveSecretHouse")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = playerSecretHouse;
            camera.transform.localPosition = camSecretHouse;
        }
        else if (this.name == "MoveSewer")
        {
            DataBaseManager.isDirecting = true;
            player.transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
            CameraManager.Instance.isCheckEnd = true;
            player.transform.localPosition = playerInSewer;
            camera.transform.localPosition = camInSewer;


        }
        Invoke("EndIsDerecting", 1f);
        Invoke("AblePotal", 2f);
    }
    public void FirstSewerIn()
    {
        DataBaseManager.sewerFirstIn = true;
    }
    public void Force2st()
    {
        InteractionController.Instance.Start1stClientsHouse("Aiden_key_FailForce");
    }
    public void Ex2Room()
    {
        DataBaseManager.isDirecting = false;
        InteractionController.Instance.Start1stClientsHouse("into_FirstRoom");
    }
    public void ExOutto1st()
    {
        CameraManager.Instance.isCheckEnd = true;
        player.transform.localPosition = playerOutsideTo1st;
        camera.transform.localPosition = camOutsideTo1st;
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
            playerInTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInTrigger = false;
        }
    }
}
