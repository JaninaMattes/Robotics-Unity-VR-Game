using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public Boolean CircleMovement = false;
    public Boolean HorizontalSinusX = false;
    public Boolean VerticalSinusZ = false;

    private Boolean flag_circle = false; 
    private Boolean flag_sin_hor = false;
    private Boolean flag_sin_vert = false;

    //Variablen für CircleMovement
    float TimeCounter = 0;
    public float speed;
    public float height;
    public float width;

    //Variablen für Wavemovement
    public float amplitude;
    public float wavespeed;
    public float waveheight;
    private float randomdirection;

    //Globale Variablen
    public Vector3 tempPosition;
    public Vector3 startPosition_fixed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition_fixed = transform.position;
        tempPosition = transform.position;

        speed = UnityEngine.Random.Range(0f, 0.1f);
        height = UnityEngine.Random.Range(0f, 1f) * 10;
        width = UnityEngine.Random.Range(0f, 1f) * 10;

        amplitude = UnityEngine.Random.Range(0f, 1f) * 10;
        wavespeed = UnityEngine.Random.Range(-0.1f, 0.1f);
        waveheight = UnityEngine.Random.Range(0.8f, 2f);

        //randomdirection = UnityEngine.Random.Range(-1, 1);

    }

// Update is called once per frame
void FixedUpdate()
    {
        

        if (CircleMovement == true)
        {
            if (VerticalSinusZ == true || HorizontalSinusX == true)
            {
                startPosition_fixed = tempPosition;
            }
            if (flag_sin_hor == true || flag_sin_vert == true)
            {
                HorizontalSinusX = false;
                VerticalSinusZ = false;
            }
            
            FlyingCircle();
        }
        
        if (HorizontalSinusX == true)
        {
            if (CircleMovement == true || VerticalSinusZ == true)
            {
                startPosition_fixed = tempPosition;

            }
            if (flag_circle == true || flag_sin_vert == true)
            {
                CircleMovement = false;
                VerticalSinusZ = false;
            }
            WaveHorizontalX();
        }

        if (VerticalSinusZ == true)
        {
            if (CircleMovement == true || HorizontalSinusX == true )
            { 
                startPosition_fixed = tempPosition;
            }
            if (flag_circle == true || flag_sin_hor == true)
            {
                CircleMovement = false;
                HorizontalSinusX = false;
            }
            WaveVerticalZ();
        }

    }

    void FlyingCircle()
    {
        flag_circle = true;
        flag_sin_hor = false;
        flag_sin_vert = false;
        
        TimeCounter += Time.deltaTime * speed * 2;
        tempPosition.x = Mathf.Sin(TimeCounter) * height;
        tempPosition.x = tempPosition.x + startPosition_fixed.x;

        tempPosition.z = Mathf.Cos(TimeCounter) * width;
        tempPosition.z = tempPosition.z + startPosition_fixed.z;

        tempPosition.y = startPosition_fixed.y;

        transform.position = tempPosition;
    }

    void WaveHorizontalX()
    {
        TimeCounter += Time.deltaTime * speed;

        flag_circle = false;
        flag_sin_hor = true;
        flag_sin_vert = false;

        tempPosition.z = Mathf.Sin(TimeCounter * waveheight) * amplitude;
        tempPosition.z = tempPosition.z + startPosition_fixed.z;

        tempPosition.x += wavespeed;
        tempPosition.y = startPosition_fixed.y;

        transform.position = tempPosition;

        if (tempPosition.x >= 10 || tempPosition.x <= -10)
        {
            wavespeed = wavespeed * -1;
            amplitude = UnityEngine.Random.Range(0f, 1f) * 10;

        }
    }

    void WaveVerticalZ()
    {
        flag_circle = false;
        flag_sin_hor = false;
        flag_sin_vert = true; 


        TimeCounter += Time.deltaTime * speed; 

        tempPosition.y = Mathf.Sin(TimeCounter * waveheight) * amplitude;
        tempPosition.y = tempPosition.y + startPosition_fixed.y;

        tempPosition.z += wavespeed; 
        tempPosition.x = startPosition_fixed.x; 

        transform.position = tempPosition;

        if (tempPosition.z >= 10 || tempPosition.z <= -10)
        {
            wavespeed = wavespeed * -1;
            amplitude = UnityEngine.Random.Range(0f, 1f) * 10;

        }
    }
}
