using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast_gunflyingobjects : MonoBehaviour
{
    //public float maxRayDistance = 300; // alter Versuch Raycast

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 100f, Color.red);


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity) && OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log("erschossen");

            //hit.collider.gameObject.GetComponent<EnemyHit>().BeenShot();

        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection (Vector3.up), out hit, Mathf.Infinity))
        {
            Debug.Log("getroffen");

           // hit.collider.gameObject.GetComponent<EnemyHit>().BeenHit();

            /*if (OVRInput.Get(OVRInput.Button.One))
            {
                hit.collider.gameObject.GetComponent<EnemyHit>().BeenShot();
                Debug.Log("Erschossen");*/
        }




        //Alter Versuch Raycast
        /*
        Ray ray = new Ray(transform.position, Vector3.right);
        RaycastHit hit;

        Debug.DrawLine(transform.position, transform.position + Vector3.right * maxRayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, maxRayDistance))
        {
            Debug.Log("Treffer");
            Debug.DrawLine(hit.point,hit.point + Vector3.up * 5, Color.blue);
        }
        */
    }
}
