using UnityEngine;

public class WorldItem : MonoBehaviour {

    [SerializeField] private GameObject itemToSpawnInUI = null;
    public GameObject ItemToSpawnInUI { get => itemToSpawnInUI; }

}
