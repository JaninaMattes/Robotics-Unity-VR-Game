using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public Camera targetCamera;

    /// <summary>
    /// Update func is called once per frame
    /// To transform the bumper object so that it 
    /// always faces the player camera.
    /// </summary>
    void Update()
    {
        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.left,
        targetCamera.transform.rotation * Vector3.up);
    }
}
