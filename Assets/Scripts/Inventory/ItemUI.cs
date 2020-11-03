using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public ItemSO ItemSO = null;
    public Slot inSlot = null;
    public int currentAmount = 1;

    public bool IsDroppedOnSlot = true;

    private static List<ItemUI> itemUIs = new List<ItemUI>();
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Text currentAmoutText = null;


    private void Awake () {
        itemUIs.Add(this);

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = FindObjectOfType<Canvas>();

        currentAmoutText = transform.Find("Text").GetComponent<Text>();
        currentAmoutText.text = currentAmount.ToString();
    }

    public void OnBeginDrag (PointerEventData eventData) {
        foreach(ItemUI itemUI in itemUIs) {
            itemUI.canvasGroup.blocksRaycasts = false;
        }
        IsDroppedOnSlot = false;
    }

    public void OnDrag (PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag (PointerEventData eventData) {
        foreach(ItemUI itemUI in itemUIs) {
            itemUI.canvasGroup.blocksRaycasts = true;
        }

        if (!IsDroppedOnSlot) {
            ItemsCreator.CreateItemInWorldSpace(this);
        }
    }

    public void SelfDestroy () {
        itemUIs.Remove(this);

        Destroy(this.gameObject);
    }
}
