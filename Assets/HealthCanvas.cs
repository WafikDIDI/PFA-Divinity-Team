using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCanvas : MonoBehaviour
{
    public HealthBar healthBar = null;
    public HealthSystem healthSystem = new HealthSystem(100);


    void Start()
    {
        healthBar.Setup(healthSystem);
    }

    void Update()
    {
        HealthChange();
    }

    private void HealthChange()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            healthSystem.Heal(10);
            Debug.Log("healed " + healthSystem.GetHealth());

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            healthSystem.Damage(10, 1);
            Debug.Log("damaged " + healthSystem.GetHealth());
        }
    }
}
