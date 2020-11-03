using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Text ammoText;
    

    private void OnEnable()
    {
        GameManager.OnAmmoChangeUI += AmmoChangeUI;
    }

    private void AmmoChangeUI(int value)
    {
        ammoText.text = value.ToString();
    }
}
