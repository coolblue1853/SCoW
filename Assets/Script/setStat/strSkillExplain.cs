using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class strSkillExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update
    public void MartialartsExplain()
    {
        nameEX.text = "< MartialArts >";
        mainEX.text = "This stat represents the player's fighting ability.  The higher this stat is, the better the melee advantage.";
    }
    public void GunshotExplain()
    {
        nameEX.text = "< Marksmanship > ";
        mainEX.text = "This stat represents the player's ability to handle a firearm.  The higher this stat is, the more accurate the aim.";
    }
    public void SwordExplain()
    {
        nameEX.text = "< Swordsmanship > ";
        mainEX.text = "This stat represents the player's ability to wield a bladed weapon.  The higher this stat is, the better the player is at melee combat with tools.";
    }
    public void ObservationExplain()
    {
        nameEX.text = "< Observation >";
        mainEX.text = "This stat represents the acuity of the player's vision.  The higher this stat is, the better you can spot small, hard-to-see objects.";
    }
    public void SwimingExplain()
    {
        nameEX.text = "< Swim >";
        mainEX.text = "This stat represents the player's swimming ability.  The higher this stat is, the longer the player can dive and the easier it is to move through the water.";
    }
}
