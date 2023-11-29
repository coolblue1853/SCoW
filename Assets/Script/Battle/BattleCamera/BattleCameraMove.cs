using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
public class BattleCameraMove : MonoBehaviour
{
    private static BattleCameraMove instance = null;
    Transform cameraTransform;
    Camera camera;
    float upSide = 122f;
    float downSide = 119f;
    float originPoint = 120.1f;
    float originPointx = -774.6f;
    bool once = false;
    string nowCam;
    bool isSetTurn = false;

    private void Awake()
    {
        cameraTransform = this.GetComponent<Transform>();
        camera = this.GetComponent<Camera>();
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static BattleCameraMove Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    void Update()
    {
        if (BattleManager.Instance != null)
        {
            if (DataBaseManager.nowPlace != "InSewer")
            {
                if (BattleManager.Instance.battleState == "setTrun" && isSetTurn == false)
                {
                    isSetTurn = true;
                    ResetCam();
                }
                if (BattleManager.Instance.battleState == "selectEnemy")
                {
                    isSetTurn = false;
                    if (BattleManager.Instance.onPointerEnemy == "" && once == false)
                    {
                        once = true;
                        nowCam = BattleManager.Instance.onPointerEnemy;
                        ResetCam();
                    }
                    if (BattleManager.Instance.onPointerEnemy == "DeepOneHybrid1" && once == false)
                    {
                        once = true;
                        nowCam = BattleManager.Instance.onPointerEnemy;
                        ZoomMidle();
                    }
                    if (BattleManager.Instance.onPointerEnemy == "DeepOneHybrid2" && once == false)
                    {
                        once = true;
                        nowCam = BattleManager.Instance.onPointerEnemy;
                        ZoomDown();
                    }
                    if (BattleManager.Instance.onPointerEnemy == "DeepOneHybrid3" && once == false)
                    {
                        once = true;
                        nowCam = BattleManager.Instance.onPointerEnemy;
                        ZoomUp();
                    }
                    if (nowCam != BattleManager.Instance.onPointerEnemy)
                    {
                        once = false;
                    }
                }
            }
        }
    }
    public void ZoomPlayer()
    {
        if (DataBaseManager.nowPlace != "InSewer")
        {
            camera.DOOrthoSize(11, 1).SetAutoKill(true);
            cameraTransform.transform.DOMoveY(120.7f, 1).SetAutoKill(true);
            cameraTransform.transform.DOMoveX(-779.2f, 1).SetAutoKill(true);
        }
    }
    public void ZoomDown()
    {
        if (DataBaseManager.nowPlace != "InSewer")
        {
            camera.DOOrthoSize(13, 1).SetAutoKill(true);
            cameraTransform.transform.DOMoveY(downSide, 1).SetAutoKill(true);
        }
    }
    public void ZoomMidle()
    {
        if (DataBaseManager.nowPlace != "InSewer")
        {
            camera.DOOrthoSize(13, 1).SetAutoKill(true);
            cameraTransform.transform.DOMoveY(originPoint, 1).SetAutoKill(true);
        }
    }
    public void ZoomUp()
    {
        if (DataBaseManager.nowPlace != "InSewer")
        {
            camera.DOOrthoSize(13, 1).SetAutoKill(true);
            cameraTransform.transform.DOMoveY(upSide, 1).SetAutoKill(true);
        }
    }
    public void ResetCam()
    {
        if (DataBaseManager.nowPlace != "InSewer")
        {
            camera.DOOrthoSize(14.5216f, 1).SetAutoKill(true);
            cameraTransform.transform.DOMoveX(originPointx, 1).SetAutoKill(true);
            cameraTransform.transform.DOMoveY(originPoint, 1).SetAutoKill(true);
            cameraTransform.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        }
    }
}
