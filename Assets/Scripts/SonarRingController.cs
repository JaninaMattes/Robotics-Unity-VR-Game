using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SonarRingController : MonoBehaviour
{
    // Passing Array
    public Vector4[] points;
    public Material sonarMaterial;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sonarMaterial.SetInt("_PointsSize", points.Length);
        sonarMaterial.SetVectorArray("_Points", points);
    }
}
