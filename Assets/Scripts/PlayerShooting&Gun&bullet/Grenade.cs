using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Rigidbody rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rigidbody.AddForce(Vector3.up*10f);
        
    }

}
