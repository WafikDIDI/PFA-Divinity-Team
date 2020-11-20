using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysUI : MonoBehaviour
{

    private void Awake()
    {
        Keys.OnKeyPickUp += CreateImage;
    }

    private void OnEnable()
    {
        OpenDoor.OnKeyUse += RemoveImage;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //CreateImage();
        }
    }

    
    private void CreateImage(Keys.KeysType keyType, Image keysSprite)
    {
        Instantiate(keysSprite, transform);
    }

    public void RemoveImage(Image image)
    {
        Debug.Log("<<<<<remove>>>>>");
        image.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Keys.OnKeyPickUp -= CreateImage;
        OpenDoor.OnKeyUse -= RemoveImage;
    }
}
