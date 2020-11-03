using UnityEngine;

public abstract class ItemSO : ScriptableObject {

    public enum ItemType {
        Consumable,
        Equiqable,
        Default,
    }

    protected ItemType itemType = ItemType.Default;

    [Header("General")]
    public string itemName = string.Empty;
    public bool isStackable = false;
    public int maxCount = 1;

    [Space]
    [Header("Visual")]
    public Transform itemWorldPrefab = null;
    public Transform itemUIPrefab = null;
    public Sprite itemSprite = null;

    public abstract void Interact ();

}

