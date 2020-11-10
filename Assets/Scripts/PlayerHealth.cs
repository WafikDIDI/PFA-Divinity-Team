using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar=null;
    HealthSystem healthSystem = new HealthSystem(100);
    
    
    // Start is called before the first frame update
    void Start()
    {
        healthBar.Setup(healthSystem);
    }

    // Update is called once per frame
    void Update()
    {
        HealthChange();
    }

    private void HealthChange()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            healthSystem.Heal(10);
            Debug.Log("healed " + healthSystem.GetHealth());
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            healthSystem.Damage(10);
            Debug.Log("damaged " + healthSystem.GetHealth());
        }
    }

}
