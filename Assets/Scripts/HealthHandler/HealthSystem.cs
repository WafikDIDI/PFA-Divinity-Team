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

    public void Damage(int damage,int id)
    {
        switch (id)
        {
            case 1:
                AudioManger.instance.Play("PlayerHit");
                break;
            case 2:
                var x = UnityEngine.Random.Range(1, 3);
                if (x == 1)
                {
                    AudioManger.instance.Play("EnemyHit1");
                }
                else 
                {
                    AudioManger.instance.Play("EnemyHit2");
                }
                break;
        }

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
