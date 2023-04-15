using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StressManager : MonoBehaviour
{
    float maxStress;
    public Image StressBar;
    private static StressManager instance = null;
    private void Awake()
    {
        StressBar.fillAmount = 0;
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static StressManager Instance
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
    // Start is called before the first frame update
    void Start()
    {
        maxStress = DataBaseManager.mp;
    }

    bool First = false;
    // Update is called once per frame
    void Update()
    {

        if(StressBar.fillAmount  >= (float)DataBaseManager.mp / 100 && First == false)
        {
            First = true;
            Rollet.Instance.setRollet("이성 : 광기", "광기억제", DataBaseManager.san, "stress");
        }
    }
    
    public void UPStress(float Up)
    {
        if(StressBar.fillAmount < (float)DataBaseManager.mp / 100)
            StressBar.DOFillAmount(StressBar.fillAmount+(Up / 100), 0.3f);
    }

    public void Result(string result_End)
    {
        if (result_End== "판정 : 대성공" || result_End == "판정 : 성공") 
        {
            Debug.Log("광기 없음!");
            StressBar.fillAmount = 0;
        }
        else
        {
            StressBar.fillAmount = StressBar.fillAmount /2;
            Debug.Log("광기 발현!");
        }
        First = false;
    }

}
