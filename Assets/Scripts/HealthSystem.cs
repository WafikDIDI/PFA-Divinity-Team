using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem 
{
    public event EventHandler OnHealChanged;

    private int health;
    private int healthMax;

    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth()
    {
        return health;
    }

    public float HealthPrecent()
    {
        return (float) health / healthMax;
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
        OnHealChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health > healthMax) health = healthMax;
        OnHealChanged?.Invoke(this, EventArgs.Empty);

    }

}
