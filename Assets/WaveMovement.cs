using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public float amplitude;
    public float wavespeed;
    public float waveheight;

    public Vector3 tempPosition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        tempPosition = transform.position;
        amplitude = UnityEngine.Random.Range(0f, 1f) * 10;
        wavespeed = 0.05f;
        waveheight = 0.8f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tempPosition.x = Mathf.Sin(Time.realtimeSinceStartup * waveheight) * amplitude;
        tempPosition.z += wavespeed;

        transform.position = tempPosition;

        if(tempPosition.z >= 60 || tempPosition.z <= -60)
        {
            wavespeed = wavespeed * -1;
            amplitude = UnityEngine.Random.Range(0f, 1f)*10;

        }

    
    }

}
