﻿/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using CodeMonkey;

public class HealthBarShrink : MonoBehaviour {

    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = .6f;

    public Image barImage;
    public Image damagedBarImage;
    public bool isHP;


    private float damagedHealthShrinkTimer;


    public HealthSystem healthSystem;
    public HealthSystem MentalSystem;
    private void Awake() {

    }

    private void Start() {
        if (isHP)
        {
            healthSystem = new HealthSystem(DataBaseManager.hp);
        }
        else
        {
            healthSystem = new HealthSystem(DataBaseManager.mp);
        }
        SetHealth(healthSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = barImage.fillAmount;



        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    private void Update() {
        damagedHealthShrinkTimer -= Time.deltaTime;
        if (damagedHealthShrinkTimer < 0) {
            if (barImage.fillAmount < damagedBarImage.fillAmount) {
                float shrinkSpeed = 1f;
                damagedBarImage.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e) {
        SetHealth(healthSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = barImage.fillAmount;
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        SetHealth(healthSystem.GetHealthNormalized());
    }

    private void SetHealth(float healthNormalized) {
        barImage.fillAmount = healthNormalized;
    }

}
