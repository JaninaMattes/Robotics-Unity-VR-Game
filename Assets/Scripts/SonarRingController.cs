using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SonarRingController : MonoBehaviour
{
    // Passing Array
    private static readonly Vector4[] defaultEmptyVector = new Vector4[] { new Vector4(0, 0, 0, 0) };
    public Vector4[] points;
    public Material sonarMaterial;

    // Use this for initialization
    void Start()
    {
        // Max 50 Sonarrings can be created/rendered, 
        // this is predefined in the array of the shader
        points = new Vector4[50];
    }

    // Update is called once per frame
    void Update()
    {

        sonarMaterial.SetInt("_PointsSize", points.Length);
        if (points.Length <= 0) // Shader cant have zero values
        {
            sonarMaterial.SetVectorArray("_Points", defaultEmptyVector);
        }
        else
        {
            sonarMaterial.SetVectorArray("_Points", points);
        }

    }
}
