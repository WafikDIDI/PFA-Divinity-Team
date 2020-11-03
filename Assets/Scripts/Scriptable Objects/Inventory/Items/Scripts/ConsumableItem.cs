
using UnityEngine;

[CreateAssetMenu(menuName = "InventorySystem/Items/Consumable")]
public class ConsumableItem : ItemSO {
    public override void Interact () {

    }

    private void Awake () {
        itemType = ItemType.Consumable;
    }
}
