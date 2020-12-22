using DivinityPFA.Systems;
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ISaveable
{
    public HealthBar healthBar = null;
    public HealthSystem healthSystem = new HealthSystem(100);

    void Start ()
    {
        healthBar.Setup(healthSystem);
        healthSystem.isPlayerHealth = true;
    }

    void Update ()
    {
        HealthChange();
    }

    private void HealthChange ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            healthSystem.Heal(10);
            Debug.Log("healed " + healthSystem.GetHealth());
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            healthSystem.Damage(10, 1);
            Debug.Log("damaged " + healthSystem.GetHealth());
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("BulletE"))
        {
            Destroy(other.gameObject);
            var bulletDamage = other.GetComponent<Bullet>().bulletDamage;
            healthSystem.Damage(bulletDamage, 1);
        }
    }

    public object CaptureState ()
    {
        return new SavaData
        {
            Health = healthSystem.Health,
            MaxHealth = healthSystem.HealthMax
        };
    }

    public void RestoreState (object state)
    {
        var data = (SavaData)state;

        healthSystem = new HealthSystem(data.MaxHealth, data.Health);
    }

    [Serializable]
    private struct SavaData
    {
        public int Health;
        public int MaxHealth;
    }
}
