using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class intSkillExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update
    public void MedicineExplain()
    {
        nameEX.text = "< Medicine >";
        mainEX.text = "This stat represents the player's level of medical knowledge.  The higher this stat is, the more tools you can find by analyzing wounds, the more medical names you can remember.";
    }
    public void AnalysisExplain()
    {
        nameEX.text = "< Analysis >";
        mainEX.text = "This stat represents the player's ability to analyze information.  The higher this stat is, the easier it is to get a lot of information from books, newspapers, etc.";
    }
    public void ListeningExplain()
    {
        nameEX.text = "< Listening >";
        mainEX.text = "This stat represents the acuity of the player's hearing.  The higher this stat is, the better the player can hear small, unnoticed sounds.";
    }
    public void PsychotherapyExplain()
    {
        nameEX.text = "< Psychotherapy > ";
        mainEX.text = "This stat indicates the player's psychological and psychiatric healing abilities.  The higher this stat is, the more you can stabilize someone who is having a mental breakdown.";
    }
    public void OccultExplain()
    {
        nameEX.text = "< Occult >";
        mainEX.text = "This stat represents the player's level of occult knowledge. The higher this stat is, the more fluent the player is in secret glyphs, folklore, or occultism.";
    }
}
