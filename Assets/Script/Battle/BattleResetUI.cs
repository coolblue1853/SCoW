using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BattleResetUI : MonoBehaviour
{
    public TextMeshProUGUI swordName;
    public TextMeshProUGUI swordDetail;
    public TextMeshProUGUI deftName;
    public TextMeshProUGUI deftDetail;
    public TextMeshProUGUI markDetail;
    public TextMeshProUGUI markName;
    public Image batImage;
    public Image daggerImage;
    public Image axeImage;
    public Image smallPisolImage;
    public Image revolverImage;
    public Image rifleImage;
    public Image shotgunImage;
    public Image rockImage;
    public Image molotovImage;
    public Sprite batSprite;
    public Sprite daggerSprite;
    public Sprite axeSprite;
    public Sprite smallPisolSprite;
    public Sprite revolverSprite;
    public Sprite rifleSprite;
    public Sprite shotgunSprite;
    public Sprite rockSprite;
    public Sprite molotovSprite;
    public void EndTrunReset()
    {
        swordName.text = "";
        swordDetail.text = "";
        deftName.text = "";
        deftDetail.text = "";
        markDetail.text = "";
        markName.text = "";

        batImage.sprite = batSprite;
        daggerImage.sprite = daggerSprite;
        axeImage.sprite = axeSprite;
        smallPisolImage.sprite = smallPisolSprite;
        revolverImage.sprite = revolverSprite;
        rifleImage.sprite = rifleSprite;
        shotgunImage.sprite = shotgunSprite;
        rockImage.sprite = rockSprite;
        molotovImage.sprite = molotovSprite;
    }
}
