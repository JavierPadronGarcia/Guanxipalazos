using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public int maxHP = 100;
    public PlayerHealth healthScript;
    public int currentHP;
    public float damageInterval = 5f;
    public int damageAmount = 10;

    public Image hpBarForeground;

    private void Start()
    {
        currentHP = maxHP;
        UpdateHPBar();
    }

    private void Update()
    {
        UpdateHPBar();
    }

    private void UpdateHPBar()
    {
        float hpPercentage = (float)healthScript.health / healthScript.maxHealth;

        // Update the fill amount of the Image component
        hpBarForeground.fillAmount = hpPercentage;
    }
}
