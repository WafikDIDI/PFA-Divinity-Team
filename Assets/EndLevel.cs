using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheRevenger.Levels
{
    [RequireComponent(typeof(Collider))]
    public class EndLevel : MonoBehaviour
    {
        [SerializeField] private Canvas endScreenLevel = null;
        [SerializeField] private GameObject bossGameObject = null;

        private void OnTriggerEnter (Collider other)
        {
            if (other.CompareTag("Player") && bossGameObject.activeSelf == false)
            {
                if (endScreenLevel)
                {
                    endScreenLevel.gameObject.SetActive(true);
                }
            }
        }
    }
}

