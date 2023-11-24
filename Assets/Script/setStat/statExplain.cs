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
        nameEX.text = "< Strength >";
        mainEX.text = "This stat represents the player's strength. The higher this stat is, the more force can generate and the heavier they can lift. Strength skill point is based on the value of this stat.";
    }
    public void int_expl()
    {
        nameEX.text = "< Intelligence >";
        mainEX.text = "This stat represents the player's Intelligence. The higher this stat is, the more educated and the better understand the problem. Intelligence skill point is based on the value of this stat.";
    }
    public void dex_expl()
    {
        nameEX.text = "< Dexterity >";
        mainEX.text = "This stat represents the player's dexterity.  The higher this stat is, the more flexible, delicate, and fast . Dexterity skill point is based on the value of this stat.";
    }

    //-------
    public void hp_expl()
    {
        nameEX.text = "< Health Point >";
        mainEX.text = "This stat represents the player's physical health.  The higher this stat is, the stronger and the more resistant to pain and fatigue. When this number reaches zero, the player is dead.";
    }
    public void mp_expl()
    {
        nameEX.text = "< Mental Point >";
        mainEX.text = "This stat represents a player's mental capacity to withstand sanity reduction. The higher this stat is, the more shocks the player can withstand."; 
    }
    public void san_expl()
    {
        nameEX.text = "< Sanity >";
        mainEX.text = "This stat represents the mental health of the player. The higher this stat is, the more mental trauma the player can take. When this number reaches zero, the player s dead.";

    }
    public void luck_expl()
    {
        nameEX.text = "< Luck >";
        mainEX.text = "This stat represents how lucky the player is.  The higher this stat is, the greater the chance of avoiding something unfortunate.";
    }

    public void wealth_expl()
    {
        nameEX.text = "< Wealth >";
        mainEX.text = "This stat represents the player's wealth.  Player start with the number of dollars equal to that stat.";
    }




    public void rest_expl ()
    {
        nameEX.text = "< - >";
        mainEX.text = "";
    }
}
