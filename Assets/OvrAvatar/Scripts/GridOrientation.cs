using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOrientation : MonoBehaviour
{
    public Material material;
    public float floorDistance = 0.001f;
    public Vector3 position;

    // Update is called once per frame
    void Update()
    {
        position.x = transform.position.x;
        position.z = transform.position.z;
        position.y = floorDistance;
        material.SetVector("_Position", position);
    }
}
