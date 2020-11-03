using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action<int> OnAmmoChangeUI;
    

    public HealthBar healthBar;
    HealthSystem healthSystem = new HealthSystem(100);


    [SerializeField] private List<Texture2D> cursorList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } 
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Debug.Log("damaged " + healthSystem.HealthPrecent());
        healthSystem.Damage(10);
        Debug.Log("damaged " + healthSystem.HealthPrecent());
        healthBar.Setup(healthSystem);
    }

    private void Update()
    {
        
        HealCHange();
    }

    public void ChangeCursor(int value)
    {
        Cursor.SetCursor(cursorList[value], Vector2.zero, CursorMode.Auto);
    }

    public void ResetCursor()
    {
        Cursor.SetCursor(cursorList[0], Vector2.zero, CursorMode.Auto);
    }

    public void BulletResiver(int value)
    {
        OnAmmoChangeUI?.Invoke(value);
    }

    private void HealCHange()
    {

        //Debug.Log("health " + healthSystem.GetHealth());
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
