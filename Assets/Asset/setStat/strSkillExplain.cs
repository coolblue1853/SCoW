using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class strSkillExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update
    public void martialarts_expl()
    {
        nameEX.text = "< 격투술 >";
        mainEX.text = "이 기능치는 플레이어의 맨손 전투 능력을 나타냅니다.  이 능력치가 높을수록 근접전에 유리합니다.";
    }
    public void Gunshot_expl()
    {
        nameEX.text = "< 사격술 >";
        mainEX.text = "이 기능치는 플레이어의 총기를 다루는 능력을 나타냅니다.  이 능력치가 높을수록 조준의 정확도가 높아집니다..";
    }
    public void sword_expl()
    {
        nameEX.text = "< 검술 >";
        mainEX.text = "이 기능치는 플레이어의 도검류를 다루는 능력을 나타냅니다.  이 능력치가 높을수록 도구를 사용한 근접전에 유리합니다.";
    }
    public void Listening_expl()
    {
        nameEX.text = "< 듣기 >";
        mainEX.text = "이 기능치는 플레이어의 청력의 예민함을 나타냅니다.  이 능력치가 높을수록 작은, 의식하지 못하는 소리를 잘 들을 수 있습니다.";
    }
    public void Swiming_expl()
    {
        nameEX.text = "< 수영 >";
        mainEX.text = "이 기능치는 플레이어의 수영능력치를 나타냅니다.  이 능력치가 높을수록 오래 잠수할 수 있으며 물 속에서 손쉽게 움직입니다.";
    }



}
