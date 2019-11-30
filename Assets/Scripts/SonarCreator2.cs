using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarCreator2 : MonoBehaviour
{
    private Collision col = null;
    public Vector4 firstCollision;
    
    public List <Vector4> contacts = new List<Vector4>(20);
    public bool switchSonar = true;

    void OnCollisionEnter(Collision collision)
    {
        //contacts.Dequeue();
        if(contacts.ToArray().Length < 20)
        {
            contacts.Add(collision.contacts[0].point);
        }

        //contacts.Enqueue(collision.contacts[0].point);
        switchSonar = true;
        col = collision;
        ContactPoint contact = col.contacts[0];
        firstCollision = contact.point;
    }

    void OnCollisionStay(Collision collision)
    {
        switchSonar = false;
    }

    void OnCollisionExit(Collision collision)
    {
        switchSonar = false;
    }


}
