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
        nameEX.text = "< Medicine >";
        mainEX.text = "This stat represents the player's level of medical knowledge.  The higher this stat is, the more tools you can find by analyzing wounds, the more medical names you can remember.";
    }
    public void analysis_expl()
    {
        nameEX.text = "< Analysis >";
        mainEX.text = "This stat represents the player's ability to analyze information.  The higher this stat is, the easier it is to get a lot of information from books, newspapers, etc.";
    }
    public void Listening_expl()
    {
        nameEX.text = "< Listening >";
        mainEX.text = "This stat represents the acuity of the player's hearing.  The higher this stat is, the better the player can hear small, unnoticed sounds.";
    }
    public void psychotherapy_expl()
    {
        nameEX.text = "< Psychotherapy > ";
        mainEX.text = "This stat indicates the player's psychological and psychiatric healing abilities.  The higher this stat is, the more you can stabilize someone who is having a mental breakdown.";
    }
    public void occult_expl()
    {
        nameEX.text = "< Occult >";
        mainEX.text = "This stat represents the player's level of occult knowledge. The higher this stat is, the more fluent the player is in secret glyphs, folklore, or occultism.";
    }
}
