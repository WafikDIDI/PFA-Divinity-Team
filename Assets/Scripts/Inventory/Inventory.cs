using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory {

    [SerializeField] private KeyCode KeytoOpenAndClose = KeyCode.I;

    [Space]
    [SerializeField] private Transform container = null;
    [SerializeField] private List<Slot> slots = new List<Slot>();

    [Header("General Settings")]
    [SerializeField] private int maxSlots = 40;
    [SerializeField] private int amountUnlocked = 1;

    private GameObject inventoryOwner = null;

    public void Awake (GameObject inventoryOwner) {
        foreach (Transform slotTrans in container) {
            if (slotTrans.TryGetComponent(out Slot slot)) {
                slots.Add(slot);
            }
        }

        var currentUnlocked = amountUnlocked;
        int id = 1;
        foreach (Slot slot in slots) {
            if (currentUnlocked > 0) {
                slot.transform.Find("Lock_Icon").gameObject.SetActive(false);
                slot.isUnlocked = true;
                currentUnlocked--;
            }

            slot.slotId = id;
            id++;
        }

        this.inventoryOwner = inventoryOwner;
        inventoryOwner.GetComponent<PlayerBase>().PlayerUpdate += Update;
    }

    public void AddItem (GameObject itemToAddGameObject) {
        var itemtoAdd = itemToAddGameObject.GetComponent<ItemUI>();

        bool itemNotIsFound = true;
        foreach (Slot slot in slots) {
            if (slot.ItemInSlot == null) {
                continue;
            } else if (slot.ItemInSlot == itemtoAdd) {
                if (slot.ItemInSlot.currentAmount < slot.ItemInSlot.ItemSO.maxCount) {
                    itemNotIsFound = false;
                    slot.ItemInSlot.currentAmount++;
                }
            }
        }

        if (itemNotIsFound) {
            var emptySlot = LookForEmptySlot();
            if (emptySlot != null) {
                emptySlot.ItemInSlot = ItemsCreator.CreateItemInUI(itemToAddGameObject, container);
                emptySlot.ReturnItemToSlot();
                emptySlot.ItemInSlot.inSlot = emptySlot;
            } else {
                Debug.Log("Inventory is full");
            }
        }
    }

    private Slot LookForEmptySlot () {
        foreach (Slot slot in slots) {
            if (slot.ItemInSlot == null) {
                return slot;
            }
        }
        return null;
    }

    public void Update () => CheckIfKeyPressed();

    private void CheckIfKeyPressed () {
        if (Input.GetKeyDown(KeytoOpenAndClose)) {
            var inventoryDisplay = container.parent.gameObject;
            inventoryDisplay.SetActive(!inventoryDisplay.activeSelf);
        }
    }

    public void Unsubscribe () {
        inventoryOwner.GetComponent<PlayerBase>().PlayerUpdate -= Update;
    }
}