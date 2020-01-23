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
        rb.AddRelativeForce(Vector3.back * speed);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Scanner")
        {
            Hit();
        }
    }

    void Hit()
    {
        Debug.Log("Boom Hit");
        Instantiate(explosionPrefab, gameObject.transform.position-rb.velocity.normalized*0.2f, Quaternion.identity);
        Destroy(gameObject);
    }


}
