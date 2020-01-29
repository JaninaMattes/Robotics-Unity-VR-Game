using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class IconMovement : MonoBehaviour
{
    float TimeCounter = 0;
    public float waveheight = 0.1f;
    public float speed = 1;
    Vector3 tempPosition;
    Vector3 startPosition;
    public Camera[] cameras;
    Camera cameraRig;

    private void Start()
    {
        startPosition= transform.position;
        cameras = GameObject.FindObjectsOfType<Camera>();
        Debug.Log(cameras);
        foreach (Camera cam in cameras)
        {
            if (cam.name == "CenterEyeAnchor")
            {
                cameraRig = cam;
                break;
            }
            else if (cam.name == "Camera")
            {
                cameraRig = cam;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Pos: " + cameraRig.transform.position + " Rot: " + cameraRig.transform.rotation);
        TimeCounter += Time.deltaTime * speed;

        tempPosition.y = startPosition.y + Mathf.Sin(TimeCounter)*waveheight;
        tempPosition.z = startPosition.z;
        tempPosition.x = startPosition.x;

        transform.position = tempPosition;
    }
}
