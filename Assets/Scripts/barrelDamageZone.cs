using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelDamageZone : MonoBehaviour
{
    public List<GameObject> gameObjectsInZone = new List<GameObject>();

    private Barrel barrel;
    public bool barrelHasbeenExplosed=false;
    private bool damageOnceTime = true;

    
    // Start is called before the first frame update
    void Start()
    {
        barrel = transform.GetChild(0).GetComponent<Barrel>();
        barrel.OnBarrelExplosion += ListOfObjectInsidetheZone;
    }


    private void ListOfObjectInsidetheZone(bool obj) => barrelHasbeenExplosed = obj;
    

    private void OnTriggerStay(Collider other)
    {
        if (barrelHasbeenExplosed == true)
        {

            if (other.CompareTag("Enemy"))
            {
                gameObjectsInZone.Add(other.gameObject);
                other.gameObject.SetActive(false);


            }

            if (other.CompareTag("Player"))
            {
                if (damageOnceTime)
                {
                    var playerHealth = other.GetComponent<PlayerHealth>().healthSystem;
                    Debug.Log("healed " + playerHealth.GetHealth());
                    playerHealth.Damage(70);
                    Debug.Log("Damaged " + playerHealth.GetHealth());
                    //barrelHasbeenExplosed = false;
                    damageOnceTime = false;
                }
            }
        }
    }
}
