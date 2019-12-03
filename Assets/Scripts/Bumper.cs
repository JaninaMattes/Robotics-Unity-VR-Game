using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    private Camera targetCamera;

    private void Awake()
    {
        targetCamera = Camera.main;
       // transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.left);
    }

    /// <summary>
    /// Update func is called once per frame
    /// To transform the bumper object so that it 
    /// always faces the player camera.
    /// </summary>
    void Update()
    {
       transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.left);
    }

    
}
