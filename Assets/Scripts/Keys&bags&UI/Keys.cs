using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Keys :MonoBehaviour 
{
    private static PlayerKeysBag playerKeysBag;

    public static Action<List<Keys>> OnKeyPickUp;

    public enum KeysType
    {
        red,
        blue,
        yellow,
        green
    }

    public KeysType keysType = KeysType.green;
    [SerializeField] private Image imageKey;

    private void Awake()
    {
        if (playerKeysBag == null)
            playerKeysBag = FindObjectOfType<PlayerKeysBag>();
    }

    public KeysType GetKeyType()
    {
        return keysType;
    }

    public Image GetImage()
    {
        return imageKey;
    }
    
    public Keys(KeysType type, Image image)
    {
        keysType = type;
        imageKey = image; 
    }

    public static void OnKeyPicked(Keys keyPickedUp)
    {
        OnKeyPickUp?.Invoke(playerKeysBag.KeysPickedUpList);
    }
    
}
