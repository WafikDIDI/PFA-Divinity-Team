using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    private static PlayerKeysBag playerKeysBag;

    [SerializeField] private Animator animator;
    [SerializeField] private Keys.KeysType keyOfDoor= Keys.KeysType.green;
    [SerializeField] private bool doorLocket = false;

    public static Action<List<Keys>> OnKeyUse = null;

    private void Awake()
    {
        if (playerKeysBag == null)
            playerKeysBag = FindObjectOfType<PlayerKeysBag>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var isHasKeyToRightDoor = playerKeysBag.IsHasKey(keyOfDoor);
            

           if (isHasKeyToRightDoor != null || doorLocket)
           {
                animator.SetBool("open", true);
                if (doorLocket) return;
                doorLocket = true;
                OnKeyUse?.Invoke(playerKeysBag.KeysPickedUpList);
           }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("open", false);
        }
    }

    private void CheckIfplayerHadKey(List<Keys.KeysType> keysTypes)
    {
        for (int i = 0; i < keysTypes.Count ; i++)
        {
            if (keysTypes[i] == keyOfDoor)
            {
                doorLocket = true;
                
                keysTypes.Remove(keysTypes[i]);
            }
        }
    }

}
