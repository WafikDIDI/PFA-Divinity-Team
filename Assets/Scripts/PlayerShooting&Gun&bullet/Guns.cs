using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Guns
{
    public string gunAudioName;
    public int maxAmmo=30;
    public int currentAmmoCounter;
    public float timeBetweenShoot;
    public int gunDamage;
    public float gunRoloadTime;
    public GameObject bulletPrefab;

    public GameObject gunRef;

    public Image gunImage;

    public void Init()
    {
        currentAmmoCounter = maxAmmo;
    }

    public static int SwitchWeapon (KeyCode keyPressed, int currentIndex, int weaponsCount)
    {
        if (keyPressed == KeyCode.E)
        {
            if (currentIndex < weaponsCount)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
        }

        if (keyPressed == KeyCode.A)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            else
            {
                currentIndex = weaponsCount;
            }
        }

        return currentIndex;
    }

}
