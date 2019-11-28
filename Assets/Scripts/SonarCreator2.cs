using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarCreator2 : MonoBehaviour
{
    private Collision col = null;
    public Vector4 firstCollision;
    public bool switchSonar = true;

    void OnCollisionEnter(Collision collision)
    {
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
