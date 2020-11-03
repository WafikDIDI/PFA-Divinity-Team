using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {

    public ItemUI ItemInSlot = null;
    public bool isUnlocked = false;
    public int slotId = 0;

    private Slot (ItemUI item) {
        this.ItemInSlot = item;
    }

    private void Awake () {
        if (ItemInSlot != null) {
            ItemInSlot.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void OnDrop (PointerEventData eventData) {
        var item = eventData.pointerDrag.GetComponent<ItemUI>();

        if (item != null) {
            item.IsDroppedOnSlot = true;
            item.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            if (ItemInSlot == null) {
                item.inSlot.EmptySlot();
                ItemInSlot = item;
                item.inSlot = this;
            } else {
                ItemInSlot.GetComponent<RectTransform>().anchoredPosition = item.inSlot.GetComponent<RectTransform>().anchoredPosition;

                var slot = item.inSlot.SwapItems(ItemInSlot);
                ItemInSlot = slot.ItemInSlot;
                ItemInSlot.inSlot = this;
            }
        }
    }

    public void ReturnItemToSlot () {
        ItemInSlot.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    private Slot SwapItems(ItemUI item) {
        var slotToReturn = new Slot(ItemInSlot);

        ItemInSlot = item;
        ItemInSlot.inSlot = this;

        return slotToReturn;
    }

    public void EmptySlot () {
        ItemInSlot = null;
    }

}
