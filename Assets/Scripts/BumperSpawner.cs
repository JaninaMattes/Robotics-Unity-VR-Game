using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperSpawner : MonoBehaviour
{
    //Cube prefab
    public GameObject bumper;
    public GameObject player;
    private Color alphaColor = new Color(0.0f,0.0f,0.0f);
    private float timeToFade = 1.0f;

    public void Start()
    {
        alphaColor = bumper.GetComponent<MeshRenderer>().material.color;
        alphaColor.a = 0;
    }

    void OnCollisionEnter(Collision col)
    {
        Vector3 contact = col.contacts[0].point;
        Instantiate(bumper, contact, Quaternion.identity);
        Debug.Log("Object in contact on x = " 
            + contact.x + "y = " + contact.y + " z = " + contact.z);
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        bumper.GetComponent<MeshRenderer>().material.color = 
            Color.Lerp(bumper.GetComponent<MeshRenderer>().
            material.color, alphaColor, timeToFade * Time.deltaTime);
        GameObject.Destroy(bumper);
        Debug.Log("No longer in contact with " + collisionInfo.transform.name);
    }
}
