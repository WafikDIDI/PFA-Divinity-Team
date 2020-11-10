using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 hitPoint;

    [SerializeField] private float bulletSpeed=20f;
    private Rigidbody rigidbody;
    private Vector3 mouPoOnClick;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    public void OnStart()
    {
        
        var direction = hitPoint-transform.position;
        rigidbody.velocity=(direction * bulletSpeed);
    }

    private void Updates()
    {
        transform.LookAt(hitPoint);
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, mouPoOnClick, bulletSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
            print("wall");
        }
    }

    public void OnCreate(Vector3 mouPoOnClick)
    {
        this.mouPoOnClick = mouPoOnClick;
    }
}
