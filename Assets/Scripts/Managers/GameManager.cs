using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action<int,int> OnAmmoChangeUI;
    
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

    public void ChangeCursor(int value)
    {
        Cursor.SetCursor(cursorList[value], new Vector2(cursorList[value].width / 2, cursorList[value].height / 2), CursorMode.Auto);
    }

    public void ResetCursor()
    {
        Cursor.SetCursor(cursorList[0], Vector2.zero, CursorMode.Auto);
        
    }

    public void BulletResiver(int value,int value2)
    {
        OnAmmoChangeUI?.Invoke(value,value2);
    }

}
