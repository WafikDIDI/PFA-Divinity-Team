using UnityEngine;

public class ItemsCreator : MonoBehaviour {

    private static PlayerBase playerBaseReference = null;

    private void Awake () {
        playerBaseReference = FindObjectOfType<PlayerBase>();
    }

    /// <summary>
    /// Creating an Item from the UI to the Scene (Used to drop items)
    /// </summary>
    public static void CreateItemInWorldSpace (ItemUI item) {
        item.inSlot.EmptySlot();

        var itemToCreatePrefab = item.ItemSO.itemWorldPrefab;
        var objectCreatedReference = Instantiate(itemToCreatePrefab, playerBaseReference.transform.position + Vector3.forward, Quaternion.identity);

        item.SelfDestroy();
    }

    public static ItemUI CreateItemInUI(GameObject itemPrefabToCreate, Transform container) {
        var itemAddedToUI = Instantiate(itemPrefabToCreate, container);

        return itemAddedToUI.GetComponent<ItemUI>();
    }

}
