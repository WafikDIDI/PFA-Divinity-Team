using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeysBag : MonoBehaviour
{
    [SerializeField] private List <Keys> keysPickedUpList= new List<Keys>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Keys"))
        {
            var keyRef = other.GetComponent<Keys>();
            keysPickedUpList.Add(keyRef);

            Keys.OnKeyPicked(keyRef);
            other.gameObject.SetActive(false);
        }
    }

    public Keys IsHasKey(Keys.KeysType keyTypetoLookFor)
    {
        for (int i = 0; i < keysPickedUpList.Count; i++)
        {
            if(keysPickedUpList[i].keysType == keyTypetoLookFor)
            {
                return keysPickedUpList[i];
            }
        }

        return null;
    }
    
}
