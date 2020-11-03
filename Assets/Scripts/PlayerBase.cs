using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    public Action PlayerUpdate = null;

    private void Awake () {
        inventory.Awake(this.gameObject);
    }

    private void Update () {
        PlayerUpdate?.Invoke();
    }

    private void OnDestroy () {
        inventory.Unsubscribe();
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Item")) {
            inventory.AddItem(other.GetComponentInParent<WorldItem>().ItemToSpawnInUI);
            Destroy(other.transform.parent.gameObject);
        }
    }

}
