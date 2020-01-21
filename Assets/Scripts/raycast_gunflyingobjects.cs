using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast_gunflyingobjects : MonoBehaviour
{
    public float maxRayDistance = 300;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, Vector3.left);
        RaycastHit hit;

        Debug.DrawLine(transform.position, transform.position + Vector3.left * maxRayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, maxRayDistance))
        {
            Debug.Log("Treffer");
            Debug.DrawLine(transform.position, transform.position + Vector3.right * maxRayDistance, Color.blue);
        }

    }
}
