using UnityEngine;

public class DropKeyOnDeath : MonoBehaviour
{
    [SerializeField] private Transform keyToDrop;

    private void OnDisable ()
    {
        Instantiate(keyToDrop, transform.position, Quaternion.identity);
    }
}
