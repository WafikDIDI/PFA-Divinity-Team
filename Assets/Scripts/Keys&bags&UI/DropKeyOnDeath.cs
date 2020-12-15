using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropKeyOnDeath : MonoBehaviour
{
    [SerializeField] private Transform keyToDrop;

    private void OnDisable () {
        Instantiate(keyToDrop, transform.position, Quaternion.identity);
    }
}
