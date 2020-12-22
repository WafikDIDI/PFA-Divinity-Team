using DivinityPFA.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        healthSystem.OnHealChanged += HealthSystem_OnHealChanged;
        transform.Find("Bar").localScale = new Vector3(healthSystem.HealthPrecent(), 1);
    }

    private void HealthSystem_OnHealChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(healthSystem.HealthPrecent(), 1);
    }
}
