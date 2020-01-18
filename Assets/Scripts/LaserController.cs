using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For debugging purpose use ExecuteInEditMode

public class LaserController : MonoBehaviour
{
    // Passing Array
    private static readonly Vector4[] defaultEmptyVector = new Vector4[] { new Vector4(0, 0, 0, 0) };
    public Vector4[] sonarHits; // for debugging
    public Material material;

    // Use this for initialization
    void Start()
    {
        // Max 50 Sonarrings can be created/rendered, 
        // this is predefined in the array of the shader
        sonarHits = new Vector4[50];
    }

    // Update is called once per frame
    void Update()
    {

        material.SetInt("_PointsSize", sonarHits.Length);
        if (sonarHits.Length <= 0) // Shader cant have zero values
        {
            material.SetVectorArray("_Points", defaultEmptyVector);
        }
        else
        {
            material.SetVectorArray("_Points", sonarHits);
        }

    }
}
