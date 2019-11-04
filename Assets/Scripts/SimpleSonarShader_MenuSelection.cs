// SimpleSonarShader scripts and shaders were written by Drew Okenfuss.

using System.Collections;
using UnityEngine;


public class SimpleSonarShader_MenuSelection : MonoBehaviour
{
    // For testing purpose only
    void OnMenuSelection(Vector4 playerPos)
    {
        // Start sonar ring from the contact point
        SimpleSonarShader_Parent parent = GetComponentInParent<SimpleSonarShader_Parent>();
        if (parent) parent.StartSonarRing(playerPos, playerPos.magnitude / 10.0f);
    }

    // Original function for colliding objects
    //void OnCollisionEnter(Collision collision)
    //{
    //    // Start sonar ring from the contact point
    //    SimpleSonarShader_Parent parent = GetComponentInParent<SimpleSonarShader_Parent>();
    //    if (parent) parent.StartSonarRing(collision.contacts[0].point, collision.impulse.magnitude / 10.0f);
    //}
}
