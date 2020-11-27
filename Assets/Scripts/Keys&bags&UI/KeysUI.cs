using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysUI : MonoBehaviour
{
    
    private void Awake()
    {
        Keys.OnKeyPickUp += CreateImage;
        OpenDoor.OnKeyUse += CreateImage;
    }
    
    private void CreateImage(List<Keys> keysPickedUpList)
    {
        foreach(Transform tran in transform)
        {
            Destroy(tran.gameObject);
        }

        for (int i = 0; i < keysPickedUpList.Count; i++)
        {
            var create = Instantiate(keysPickedUpList[i].GetImage(), transform);
            create.gameObject.SetActive(true);
        }

    }
    

    private void OnDestroy()
    {
        Keys.OnKeyPickUp -= CreateImage;
    }
}
