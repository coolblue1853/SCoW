using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BattleCameraMove : MonoBehaviour
{
    private static BattleCameraMove instance = null;
    Transform Cam;
    Camera camera;

     float UpSide = 122f;
     float DownSide = 119f;

    float OriginPoint = 120.1f;
    // Start is called before the first frame update
    private void Awake()
    {
        Cam = this.GetComponent<Transform>();
        camera = this.GetComponent<Camera>();
        
        if (null == instance)
        {
            instance = this;
          //  DontDestroyOnLoad(this.gameObject);
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

    void Start()
    {
        
    }
    bool once = false;
    string nowCam;
    bool IsSetTurn = false;


    // Update is called once per frame
    void Update()
    {
        if(BattleManager.Instance != null)
        {
            if (BattleManager.Instance.BattleState == "setTrun" && IsSetTurn == false)
            {
                IsSetTurn = true;
                ResetCam();
            }


            if (BattleManager.Instance.BattleState == "selectEnemy")
            {
                IsSetTurn = false;
                if (BattleManager.Instance.onPointerEnemy == "" && once == false)
                {
                    once = true;
                    nowCam = BattleManager.Instance.onPointerEnemy;
                    ResetCam();

                }
                if (BattleManager.Instance.onPointerEnemy == "DeepOneHybrid" && once == false)
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

    public void ZoomDown()
    {
        camera.DOOrthoSize(13, 1).SetAutoKill(true);
        Cam.transform.DOMoveY(DownSide, 1).SetAutoKill(true);
    }
    public void ZoomMidle()
    {
        camera.DOOrthoSize(13, 1).SetAutoKill(true);
        Cam.transform.DOMoveY(OriginPoint, 1).SetAutoKill(true);
    }
    public void ZoomUp()
    {
        camera.DOOrthoSize(13, 1).SetAutoKill(true);
        Cam.transform.DOMoveY(UpSide, 1).SetAutoKill(true);
    }
    public void ResetCam()
    {
        camera.DOOrthoSize(14.5216f, 1).SetAutoKill(true);
        Cam.transform.DOMoveY(OriginPoint, 1).SetAutoKill(true);
        Cam.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
    }
}
