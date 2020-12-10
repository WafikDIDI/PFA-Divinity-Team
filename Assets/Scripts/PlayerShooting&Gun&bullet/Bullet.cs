using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 hitPoint;

    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private float bulletSpeed=20f;
    private Rigidbody rigidbody;
    private Vector3 mouPoOnClick;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    private void Updatet()
    {
    }

    public void OnStart()
    {
        
        transform.LookAt(hitPoint);
        //rigidbody.velocity=(direction * bulletSpeed);
    }

    private void Update()
    {
        //transform.LookAt(hitPoint);
        Destroy(gameObject, 3f);
        

        transform.Translate( Vector3.forward * bulletSpeed * Time.deltaTime);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var blood = Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(blood.gameObject, 1f);
            Debug.Log("<<<Blood>>>");
        }
    }

    public void OnCreate(Vector3 mouPoOnClick)
    {
        this.mouPoOnClick = mouPoOnClick;
    }
}
