using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform Playertransform;

    private Vector3 _cameraOffest;

   // [Range(0,01f,1.0f)]
    public float SmoothFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffest = transform.position - Playertransform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Playertransform.position + _cameraOffest;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

    }
}
