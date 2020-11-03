using UnityEngine;

[CreateAssetMenu(menuName = "InventorySystem/Items/Equiqable")]
public class EquiqableItem : ItemSO {
    public override void Interact () {
        
    }

    private void Awake () {
        itemType = ItemType.Equiqable;
    }
}
