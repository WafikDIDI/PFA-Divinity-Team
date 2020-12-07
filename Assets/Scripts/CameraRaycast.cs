using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(playerTransform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, 1000,LayerMask.GetMask("HouseZone")))
        {
            Debug.Log(hit.transform.name);

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        }

    }
}
