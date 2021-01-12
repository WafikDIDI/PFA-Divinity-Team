using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Text ammoText;
    [SerializeField] private Text maxAmmoText;

    private void OnEnable()
    {
        GameManager.OnAmmoChangeUI += AmmoChangeUI;
    }

    private void OnDisable ()
    {
        GameManager.OnAmmoChangeUI -= AmmoChangeUI;
    }

    private void AmmoChangeUI(int value,int value2)
    {
        ammoText.text = value.ToString();
        maxAmmoText.text = value2.ToString();
    }
}
