﻿using UnityEngine;

public interface ColorSetterInterface
{
    void Refresh();

    void SetColor(float time);
}

[ExecuteInEditMode]
public class LightColorController : MonoBehaviour
{
    public  float time;

    private ColorSetterInterface[] setters;
    private float currentTime = 0;

    public float timeValue => currentTime;


    private void OnEnable()
    {
        time = 0;
        GetSetters();
        UpdateSetters();
    }

    private void OnDisable()
    {
        time = 0;
        UpdateSetters();
    }

    public void GetSetters()
    {
        setters = GetComponentsInChildren<ColorSetterInterface>();
        foreach (var setter in setters)
            setter.Refresh();
    }

    private void Update()
    {
        if (currentTime != time)
            UpdateSetters();
    }

    public void UpdateSetters()
    {
        currentTime = time;

        foreach (var setter in setters)
            setter.SetColor(time);
    }
}