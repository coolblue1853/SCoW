using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dexSkillExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update
    public void EvasionExplain()
    {
        nameEX.text = "< Evasion >";
        mainEX.text = "This stat represents the player's ability to evade in combat. The higher this stat is, the easier it is to escape from enemy attacks.";
    }
    public void DeftnessExplain()
    {
        nameEX.text = "< Deftness >";
        mainEX.text = "This stat measures the dexterity and stealth of the player's hands. The higher this stat is, the easier it is to steal items without noticing, or to hide items without making a sound.";
    }
    public void RhetoricExplain()
    {
        nameEX.text = "< Rhetoric >";
        mainEX.text = "This stat represents the player's ability to persuade others verbally. The higher this stat is, the easier it is to persuade others through conversation.";
    }
    public void StealthExplain()
    {
        nameEX.text = "< Stealth >";
        mainEX.text = "This stat indicates the degree of agility of the player's actions. The higher this stat is, the easier it is to move around undetected by others.";
    }
    public void DisguiseExplain()
    {
        nameEX.text = "< Disguise >";
        mainEX.text = "This stat represents the level of the player's ability to disguise themselves. The higher this stat is, the more skillfully you can mimic the behavior or occupation of others.";
    }
}
