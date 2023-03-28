using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class statExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void str_expl()
    {
        nameEX.text = "< 힘 >";
        mainEX.text = "이 기능치는 플레이어의 근력을 나타냅니다. 이 능력치가 높을수록 더 강한 힘을 낼 수 있으며 무거운 물건을 들 수 있습니다.    힘 기술 배분치는 해당 기능치의 수치를 기반으로 합니다.";
    }

    public void rest_expl ()
    {
        nameEX.text = "< - >";
        mainEX.text = "";
    }
}
