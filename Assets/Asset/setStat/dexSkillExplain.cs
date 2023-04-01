using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dexSkillExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update
    public void evasion_expl()
    {
        nameEX.text = "< 회피 >";
        mainEX.text = "이 기능치는 플레이어의 전투시 회피 능력을 나타냅니다.  이 능력치가 높을수록 적의 공격에서 쉽게 벗어날 수 있습니다. 전투 라운드당 최대 한 번 사용이 가능합니다.";
    }
    public void deftness_expl()
    {
        nameEX.text = "< 손재주 >";
        mainEX.text = "이 기능치는 플레이어의 손의 재빠름, 은밀함을 느타냅니다 .  이 능력치가 높을수록 손쉽게 상대방이 모르게 물건을 훔치거나 소리없이 가진 물건을 숨기는 행동이 가능합니다.";
    }
    public void rhetoric_expl()
    {
        nameEX.text = "< 언변술 >";
        mainEX.text = "이 기능치는 플레이어의 타인을 말로 설득하는 능력을 나타냅니다.  이 능력치가 높을수록 대화를 통해 타인을 설득하기 쉬워집니다.";
    }
    public void stealth_expl()
    {
        nameEX.text = "< 은신술 >";
        mainEX.text = "이 기능치는 플레이어의 행동의 기민함의 정도를 나타냅니다.  이 능력치가 높을수록 다른 사람들에게 들키지 않게 이동 할 수 있습니다.";
    }
    public void Disguise_expl()
    {
        nameEX.text = "< 변장술 >";
        mainEX.text = "이 기능치는 플레이어 변장 능력의 수준을 나타냅니다. 이 능력치가 높을수록 더 능숙하게 타인의 행동이나 직업을 흉내낼 수 있습니다.";
    }
}
