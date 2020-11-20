using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Keys :MonoBehaviour 
{

    public static Action<KeysType, Image> OnKeyPickUp;

    public enum KeysType
    {
        red,
        blue,
        yellow,
        green
    }

    public KeysType keysType = KeysType.green;
    [SerializeField] private Image imageKey;

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
        OnKeyPickUp?.Invoke(keyPickedUp.keysType, keyPickedUp.imageKey);
    }
    
}
