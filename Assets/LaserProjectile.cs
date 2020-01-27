using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class LaserProjectile : MonoBehaviour
{

    Rigidbody rb;
    [HideInInspector]
    public float speed=1;
    [HideInInspector]
    public GameObject explosionPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.AddRelativeForce(Vector3.up * speed);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Scanner")
        {
            Hit();
            //Debug.Log("Hit Object: " + other.name + " with Tag: "+ other.tag);
        }
    }

    void Hit()
    {
        //Debug.Log("Boom!");
        Instantiate(explosionPrefab, gameObject.transform.position-rb.velocity.normalized*0.5f, Quaternion.identity);
        Destroy(gameObject);
    }


}
