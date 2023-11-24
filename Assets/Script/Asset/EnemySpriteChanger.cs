using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteChanger : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    public Sprite Stand;
    public Sprite Hitted;
    public Sprite DeepOneHybrid_Punch;
    public Sprite DeepOneHybrid_SanAttack;
    public Sprite DeepOneHybrid_PosionAttack;

    public GameObject HittedByNife;
    public GameObject HittedByBat;
    public GameObject HittedByAxe;
    public GameObject HittedBySmallPistol;
    public GameObject HittedByRevolver;
    public GameObject HittedByRifle;
    public GameObject HittedByShotgun;
    public GameObject HittedByRock;
    public GameObject HittedByMolotov;

    void OnSpriteChangeComplete(SpriteRenderer subject, Sprite sprite)
    {
        subject.sprite = sprite;
    }
    public void StandChange()
    {
        ResetAll();
        SpriteRenderer.sprite = Stand;
    }
    public void HittedByPunchChange()
    {
        SpriteRenderer.sprite = Hitted;
    }
    public void HittedByNifeChange()
    {
        SpriteRenderer.sprite = Hitted;
        ResetAll();
        HittedByNife.SetActive(true);
    }
    public void HittedByBatChange()
    {
        SpriteRenderer.sprite = Hitted;
        ResetAll();
        HittedByBat.SetActive(true);
    }
    public void HittedByAxeChange()
    {
        SpriteRenderer.sprite = Hitted;
        ResetAll();
        HittedByAxe.SetActive(true);
    }
    public void HittedBySmallPistolChange()
    {
        SpriteRenderer.sprite = Hitted;
        ResetAll();
        HittedBySmallPistol.SetActive(true);
    }
    public void HittedByRevolverChange()
    {
        SpriteRenderer.sprite = Hitted;
        ResetAll();
        HittedByRevolver.SetActive(true);
    }
    public void HittedByRifleChnage()
    {
        SpriteRenderer.sprite = Hitted;
        ResetAll();
        HittedByRifle.SetActive(true);
    }
    public void HittedByShotgunChange()
    {
        SpriteRenderer.sprite = Hitted;
        ResetAll();
        HittedByShotgun.SetActive(true);
    }
    public void HittedByRockChange()
    {
        SpriteRenderer.sprite = Hitted;
        ResetAll();
        HittedByRock.SetActive(true);
    }
    public void HittedByMolotovChange()
    {
        SpriteRenderer.sprite = Hitted;
        ResetAll();
        HittedByMolotov.SetActive(true);
    }
    public void DeepOneHybrid_PunchChange()
    {
        SpriteRenderer.sprite = DeepOneHybrid_Punch;
    }
    public void DeepOne_PoisonChange()
    {
        SpriteRenderer.sprite = DeepOneHybrid_SanAttack;
    }
    public void DeepOne_SanAttackChange()
    {
        SpriteRenderer.sprite = DeepOneHybrid_SanAttack;
    }
    private void ResetAll()
    {
        HittedByNife.SetActive(false);
        HittedByBat.SetActive(false);
        HittedByAxe.SetActive(false);
        HittedBySmallPistol.SetActive(false);
        HittedByRevolver.SetActive(false);
        HittedByRifle.SetActive(false);
        HittedByShotgun.SetActive(false);
        HittedByRock.SetActive(false);
        HittedByMolotov.SetActive(false);
    }
}
