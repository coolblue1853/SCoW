using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class statExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update


    public void str_expl()
    {
        nameEX.text = "< 힘 >";
        mainEX.text = "이 기능치는 플레이어의 근력을 나타냅니다. 이 능력치가 높을수록 더 강한 힘을 낼 수 있으며 무거운 물건을 들 수 있습니다. 힘 기술 배분치는 해당 기능치의 수치를 기반으로 합니다.";
    }
    public void int_expl()
    {
        nameEX.text = "< 지능 >";
        mainEX.text = "이 기능치는 플레이어의 지적능력을 나타냅니다.  이 능력치가 높을수록 더 많이 교육 받았으며, 문제에 대한 이해도가 높습니다. 지능 기술 배분치는 해당 기능치의 수치를 기반으로 합니다.";
    }
    public void dex_expl()
    {
        nameEX.text = "< 민첩 >";
        mainEX.text = "이 기능치는 플레이어의 재빠른 정도를 나타냅니다.  이 능력치가 높을수록 더 유연하고 섬세하며 빠릅니다. 민첩 기술 배분치는 해당 기능치의 수치를 기반으로 합니다.";
    }

    //-------
    public void hp_expl()
    {
        nameEX.text = "< 체력 >";
        mainEX.text = "이 기능치는 플레이어의 신체적 건강함을 나타냅니다.  이 능력치가 높을수록 더 튼튼하며 외부의 고통, 피로로부터 더 잘 버틸 수 있습니다. 이 수치가 0이 되면 플레이어는 사망합니다.";
    }
    public void mp_expl()
    {
        nameEX.text = "< 정신력 >";
        mainEX.text = "이 기능치는 플레이어가 이성 감소를 감당할 수 있는 정신력을 나타냅니다. 이 능력치가 높을 수록 더 많은 충격을 버틸수 있습니다."; 
    }
    public void san_expl()
    {
        nameEX.text = "< 이성 >";
        mainEX.text = "이 기능치는 플레이어의 정신적 건강함을 나타냅니다.  이 능력치가 높을수록 더 많은 양의 정신적 충격을 감당할 수 있습니다. 이 수치가 0이 되면 플레이어는 사망합니다.";

    }
    public void luck_expl()
    {
        nameEX.text = "< 행운 >";
        mainEX.text = "이 기능치는 플레이어가 얼마나 운이 있는지를 나타냅니다.  이 능력치가 높을수록 불행한 일을 회피할 확률이 늘어납니다.";
    }

    public void wealth_expl()
    {
        nameEX.text = "< 재력 >";
        mainEX.text = "이 기능치는 플레이어의 부유함 나타냅니다.  해당 능력치의 수만큼 달러($)를 가지고 시작합니다.";
    }




    public void rest_expl ()
    {
        nameEX.text = "< - >";
        mainEX.text = "";
    }
}
