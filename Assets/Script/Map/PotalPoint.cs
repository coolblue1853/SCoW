using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SCoW.Core.Sewer;
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
    Vector2 player2stToRoom = new Vector3(-1573.03f, 1.1f);
    Vector3 cam2stTo2Room = new Vector3(-1554.97f, 1.3f, -15);

    // 하수정비시설
    Vector2 playerSewerInside = new Vector3(-1363.46f, -78.46f);
    Vector3 camSewerInside = new Vector3(-1374.97f, -81.1f, -15);
    Vector2 playerSewerOutside = new Vector3(-1214.113f, -81f);
    Vector3 camSewerOutside = new Vector3(-1222.3f, -81.1f, -15);

    // 해당 이동은 강가 하부의 통로를 통해 하수도에 들어갔을 때
    Vector2 playerSewer = new Vector3(-1241.24f, -193.57f);
    Vector3 camSewer = new Vector3(-1221.3f, -195f, -15);
    Vector2 playerSecretHouse = new Vector3(-1291.44f, -194.02f);
    Vector3 camSecretHouse = new Vector3(-1303f, -195f, -15);
    Vector2 playerInSewer = new Vector3(-1211.05f, -194.02f);
    Vector3 camInSewer = new Vector3(-1217.72f, -195f, -15);
    public void Update()
    {
        if (playerInTrigger == true)
        {
            if (GameInput.PortalDown && UiGate.CanUsePortal && !KeywordPicker.IsOpen)
            {
                if ((DataBaseManager.nowPlace == "Client'shouse" || DataBaseManager.nowPlace == "NoonEvent") && DataBaseManager.timeCount >= 6)
                {
                    if (DataBaseManager.noonEventAccessAuthorization == true)
                    {
                        if (DataBaseManager.timeCount == 6)
                        {
                            if (DataBaseManager.noonEventFirstDoor == false)
                            {
                                Sequence sequence = DOTween.Sequence()
                                .AppendCallback(() => DialogManager.Instance.AlphaChange(0, 0.01f))
                                .AppendInterval(0.1f) // 2초 대기
                                .AppendCallback(() => DialogManager.Instance.ActiveBlackImage())
                                .AppendCallback(() => DialogManager.Instance.AlphaChange(1, 1f));



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
                    else if (SewerFlow.IsBlocked(this.name))
                    {
                        // 하수도: 잠긴 문(문양실 길, 비밀기지 단서 필요)·없는 문은 페이드 없이 막는다. TODO(I2): 잠김 안내 문구.
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
        SewerDoor sewerDoor;
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

            SewerFlow.Enter();
            DataBaseManager.nowPlace = "InSewer";
            DialogDatabaseManager.instance.check = true;
            DataBaseManager.storyDirecting = true;
            SoundManager.Instance.BGMManage();
            Invoke("FirstSewerIn", 2f);
        }
        else if (SewerFlow.TryDoor(this.name, out sewerDoor))
        {
            // 고정 그래프 이동(SEWER-1 S1): 문 방향과 도착 방향이 일치하고, 돌아가면 직전 방이 같은 외형으로 복원된다.
            SewerMove sewerMove = SewerFlow.Move(sewerDoor);
            if (sewerMove != null)
            {
                DataBaseManager.isDirecting = true;
                player.transform.localScale = new Vector3(SewerFlow.Facing(sewerMove.Arrival), chInRommSize, 1);
                CameraManager.Instance.isCheckEnd = true;
                player.transform.localPosition = SewerFlow.PlayerPos(sewerMove.Arrival);
                camera.transform.localPosition = SewerFlow.CamPos(sewerMove.Arrival);
            }
            else
            {
                DataBaseManager.potalWait = false;
            }
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
            // TODO(I2): 문구를 I2 키로.
            InteractHintView.Enter(transform, SCoW.Core.Ui.HintAction.Portal, "Move", 2.2f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInTrigger = false;
            InteractHintView.Exit(transform);
        }
    }
}
