using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Barrel : MonoBehaviour
{
    public event Action<bool> OnBarrelExplosion;

    public GameObject exploseEffect;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Got hit");
            OnBarrelExplosion?.Invoke(true);
            //detect
            var explose=Instantiate(exploseEffect, transform.position, Quaternion.identity);
            Destroy(gameObject.transform.parent.gameObject,0.1f);
            Destroy(explose.gameObject,1f);

            AudioManger.instance.Play("BarrelExplosion");
            //ScreenShake.instance.ShakeCamera(15f, 1f);
        }
    }

    private void OnDestroy()
    {
        ScreenShake.instance.ShakeCamera(15f, 1f);
        //AudioManger.instance.Sounds[0].source = 0f;
    }

}
