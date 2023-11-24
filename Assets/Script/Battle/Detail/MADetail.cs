using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MADetail : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Detail;
    void Start()
    {
        Name.text = "Martial Arts";
        Detail.text = "Damage:(1D3)x5\nSkillPoint:" + DataBaseManager.martialArtsPoint;
    }
}
