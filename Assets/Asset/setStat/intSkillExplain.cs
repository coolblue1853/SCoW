using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class intSkillExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update
    public void Medicine_expl()
    {
        nameEX.text = "< 의학지식 >";
        mainEX.text = "이 기능치는 플레이어의 의학지식의 정도를 나타냅니다.  이 능력치가 높을수록 상처를 분석하여 도구를 찾아내거나, 더 많은 병명을 기억할 수 있습니다. 또한 해당 기능을 사용해서 부상을 치료할 수 있습니다.";
    }
    public void analysis_expl()
    {
        nameEX.text = "< 자료분석 >";
        mainEX.text = "이 기능치는 플레이어의 정보 분석 능력을 나타냅니다.  이 능력치가 높을수록 책이나 신문등에서 쉽게, 많은 정보를 얻을 수 있습니다.";
    }
    public void psychology_expl()
    {
        nameEX.text = "< 심리판단 >";
        mainEX.text = "이 기능치는 플레이어의 타인의 심리상태를 분석하는 능력을 나타냅니다.  이 능력치가 높을수록 상대방이 초조한지, 거짓말을 하고있는지 아닌지 정확하게 판단할 수 있습니다.";
    }
    public void psychotherapy_expl()
    {
        nameEX.text = "< 정신치료 >";
        mainEX.text = "이 기능치는 플레이어가 심리,정신 치료 능력을 나타냅니다.  이 능력치가 높을수록 정신이 붕괴해 있는 사람을 안정시킬 수 있습니다. 해당 기능을 사용해서 자신의 정신붕괴 수치를 감소시킬 수 있습니다.";
    }
    public void occult_expl()
    {
        nameEX.text = "< 오컬트학 >";
        mainEX.text = "이 기능치는 플레이어의 오컬트 지식의 수준을 나타냅니다. 이 능력치가 높을수록 비밀스러운 문양, 민간 전승, 혹은 신비학에 능통합니다.";
    }
}
