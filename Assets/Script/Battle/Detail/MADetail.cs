using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MADetail : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI detail;
    void Start()
    {
        name.text = "Martial Arts";
        detail.text = "Damage:(1D3)x5\nSkillPoint:" + DataBaseManager.martialArtsPoint;
    }
}
