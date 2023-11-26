using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpriteChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite stand;
    public Sprite hitted;
    public Sprite deepOneHybridPunch;
    public Sprite deepOneHybridSanAttack;
    public Sprite deepOneHybridPosionAttack;
    public GameObject hittedByNife;
    public GameObject hittedByBat;
    public GameObject hittedByAxe;
    public GameObject hittedBySmallPistol;
    public GameObject hittedByRevolver;
    public GameObject hittedByRifle;
    public GameObject hittedByShotgun;
    public GameObject hittedByRock;
    public GameObject hittedByMolotov;

    void OnSpriteChangeComplete(SpriteRenderer subject, Sprite sprite)
    {
        subject.sprite = sprite;
    }
    public void StandChange()
    {
        ResetAll();
        spriteRenderer.sprite = stand;
    }
    public void HittedByPunchChange()
    {
        spriteRenderer.sprite = hitted;
    }
    public void HittedByNifeChange()
    {
        spriteRenderer.sprite = hitted;
        ResetAll();
        hittedByNife.SetActive(true);
    }
    public void HittedByBatChange()
    {
        spriteRenderer.sprite = hitted;
        ResetAll();
        hittedByBat.SetActive(true);
    }
    public void HittedByAxeChange()
    {
        spriteRenderer.sprite = hitted;
        ResetAll();
        hittedByAxe.SetActive(true);
    }
    public void HittedBySmallPistolChange()
    {
        spriteRenderer.sprite = hitted;
        ResetAll();
        hittedBySmallPistol.SetActive(true);
    }
    public void HittedByRevolverChange()
    {
        spriteRenderer.sprite = hitted;
        ResetAll();
        hittedByRevolver.SetActive(true);
    }
    public void HittedByRifleChnage()
    {
        spriteRenderer.sprite = hitted;
        ResetAll();
        hittedByRifle.SetActive(true);
    }
    public void HittedByShotgunChange()
    {
        spriteRenderer.sprite = hitted;
        ResetAll();
        hittedByShotgun.SetActive(true);
    }
    public void HittedByRockChange()
    {
        spriteRenderer.sprite = hitted;
        ResetAll();
        hittedByRock.SetActive(true);
    }
    public void HittedByMolotovChange()
    {
        spriteRenderer.sprite = hitted;
        ResetAll();
        hittedByMolotov.SetActive(true);
    }
    public void DeepOneHybrid_PunchChange()
    {
        spriteRenderer.sprite = deepOneHybridPunch;
    }
    public void DeepOne_PoisonChange()
    {
        spriteRenderer.sprite = deepOneHybridSanAttack;
    }
    public void DeepOne_SanAttackChange()
    {
        spriteRenderer.sprite = deepOneHybridSanAttack;
    }
    private void ResetAll()
    {
        hittedByNife.SetActive(false);
        hittedByBat.SetActive(false);
        hittedByAxe.SetActive(false);
        hittedBySmallPistol.SetActive(false);
        hittedByRevolver.SetActive(false);
        hittedByRifle.SetActive(false);
        hittedByShotgun.SetActive(false);
        hittedByRock.SetActive(false);
        hittedByMolotov.SetActive(false);
    }
}
