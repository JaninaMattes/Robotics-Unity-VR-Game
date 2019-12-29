using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidarLaser : MonoBehaviour
{

    const float laserMaxLength = 100f;
    Vector3 endPosition;

    //Returns a raycasthit point if the ray "laser" encounters a physics collider, (0,0,0) if it doesn't.
    public Vector3 getRay()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        //int layerMask = 1 << 8;
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        // Generate new instance of a Laser Ray
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit raycastHit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out raycastHit, laserMaxLength)) //, layerMask))
        {
            // if hit is detected within max distance set the new endpoint
            endPosition = raycastHit.point;
        }
        else
        {
            endPosition = Vector3.zero;
        }
        return endPosition;
    }
}