using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BattleResetUI : MonoBehaviour
{
    public TextMeshProUGUI Name_sword;
    public TextMeshProUGUI Detail_sword;
    public TextMeshProUGUI Name_Deft;
    public TextMeshProUGUI Detail_Deft;
    public TextMeshProUGUI Detail_Mark;
    public TextMeshProUGUI Name_Mark;

    public Image Bat_Image;
    public Image Dagger_Image;
    public Image Axe_Image;
    public Image SmallPisol_Image;
    public Image Revolver_Image;
    public Image Rifle_Image;
    public Image Shotgun_Image;
    public Image Rock_Image;
    public Image Molotov_Image;

    public Sprite Bat_Sprite;
    public Sprite Dagger_Sprite;
    public Sprite Axe_Sprite;
    public Sprite SmallPisol_Sprite;
    public Sprite Revolver_Sprite;
    public Sprite Rifle_Sprite;
    public Sprite Shotgun_Sprite;
    public Sprite Rock_Sprite;
    public Sprite Molotov_Sprite;
    public void EndTrun_Reset()
    {
        Name_sword.text = "";
        Detail_sword.text = "";
        Name_Deft.text = "";
        Detail_Deft.text = "";
        Detail_Mark.text = "";
        Name_Mark.text = "";

        Bat_Image.sprite = Bat_Sprite;
        Dagger_Image.sprite = Dagger_Sprite;
        Axe_Image.sprite = Axe_Sprite;
        SmallPisol_Image.sprite = SmallPisol_Sprite;
        Revolver_Image.sprite = Revolver_Sprite;
        Rifle_Image.sprite = Rifle_Sprite;
        Shotgun_Image.sprite = Shotgun_Sprite;
        Rock_Image.sprite = Rock_Sprite;
        Molotov_Image.sprite = Molotov_Sprite;
    }
}
