using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SIS_Debuff : MonoBehaviour
{

    public GameObject DetailObject;
    public TextMeshProUGUI detailText;

    public void Open_Detail()
    {
        if(DetailObject.activeSelf == false)
        {
            DetailObject.SetActive(true);
            if (this.name == "Health -5")
            {
                detailText.text = "Carelessness\n\nYou lose 5 HP and max HP Point.";
            }
            else if (this.name == "wealth -5")
            {
                detailText.text = "ThinWallets\n\nYou lose 5 of your wealth Point.";
            }
            else if (this.name == "str -10")
            {
                detailText.text = "Debilitation\n\nYou lose 10 Str Point";
            }
            else if (this.name == "int -10")
            {
                detailText.text = "Dizziness\n\nYou lose 10 Int Point";
            }
            else if (this.name == "dex -10")
            {
                detailText.text = "Sprains\n\nYou lose 10 Dex Point";
            }
            else if (this.name == "luck -10")
            {
                detailText.text = "Unlucky\n\nYou lose 10 Luk Point";
            }
            else if (this.name == "str_SKill -10")
            {
                detailText.text = "MusclePain\n\nAll skills based on Str are reduced by 10 Point.";
            }
            else if (this.name == "int_SKill -10")
            {
                detailText.text = "Migraines\n\nAll skills based on Int are reduced by 10 Point.";
            }
            else if (this.name == "dex_SKill -10")
            {
                detailText.text = "EyeDisease\n\nAll skills based on Dex are reduced by 10 Point.";
            }
            else if (this.name == "Masochism")
            {
                detailText.text = "Masochism\n\nThe player takes an additional 5 HP damage when hit.(100%)";
            }
            else if (this.name == "Careless")
            {
                detailText.text = "Careless\n\nYou lose one of the items you're currently carrying.";
            }
            else if (this.name == "MentalWeakness")
            {
                detailText.text = "MentalWeakness\n\nThe player takes an additional 5 SAN damage when hit.(100%)";
            }
            else if (this.name == "Helplessness")
            {
                detailText.text = "Helplessness\n\nMovement speed is reduced by 30%.";
            }
            else if (this.name == "Extravagant")
            {
                detailText.text = "Extravagant\n\nThe cost of store purchases increases.";
            }
            else if (this.name == "PanicAttack")
            {
                detailText.text = "PanicAttack\n\nChance to 5 HP damage when traveling through an area.(50%)";
            }
            else if (this.name == "Medicaldistrust")
            {
                detailText.text = "";
            }
            else if (this.name == "Hallucinations")
            {
                detailText.text = "";
            }
            else if (this.name == "Tightwad")
            {
                detailText.text = "Tightwad\n\nThe store is no longer available.";
            }
            else if (this.name == "NightPhobia")
            {
                detailText.text = "NightPhobia\n\nThe player can't walk around at night anymore.";
            }

        }

    }

    public void closeDetail()
    {
        DetailObject.SetActive(false);
        detailText.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
