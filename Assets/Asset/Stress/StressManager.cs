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


    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
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
            Rollet.Instance.setRollet("�̼� : ����", "�������", DataBaseManager.san, "stress");
        }
    }
    
    public void UPStress(float Up)
    {
        if(StressBar.fillAmount < (float)DataBaseManager.mp / 100)
            StressBar.DOFillAmount(StressBar.fillAmount+(Up / 100), 0.3f);
    }

    public void Result(string result_End)
    {
        if (result_End== "���� : �뼺��" || result_End == "���� : ����") 
        {
            Debug.Log("���� ����!");
            StressBar.fillAmount = 0;
        }
        else
        {
            StressBar.fillAmount = StressBar.fillAmount /2;
            Debug.Log("���� ����!");
        }
        First = false;
    }

}
