using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class LidarPointCloud : MonoBehaviour
{

    private Mesh mesh;
    private LidarScannerData lidarData;
    private int numPoints;
    private int[] indecies;
    private Color[] colors;
    private Vector3[] points;

    // Use this for initialization
    void Start()
    {
        mesh = new Mesh();
        lidarData = GameObject.FindGameObjectWithTag("Scanner").GetComponent<LidarScanner>().lidarDataDict;
        numPoints = lidarData.size;
        GetComponent<MeshFilter>().mesh = mesh;
        CreateMesh();
    }


    private void Update()
    {
        updateMesh();
    }

    //Updates mesh with new data from scanner.
    //Currently limited by Unity's 65k vertice limit.
    //try multi mesh next or interpolation next?
    void updateMesh()
    {
        points = lidarData.returnDictAsArray();
        mesh.vertices = points;
    }

    //Initializes mesh.
    void CreateMesh()
    {
        points = new Vector3[numPoints];
        indecies = new int[numPoints];
        colors = new Color[numPoints];
        points = lidarData.returnDictAsArray();
        for (int i = 0; i < points.Length; ++i)
        {
            indecies[i] = i;
            colors[i] = Color.white;
        }

        mesh.vertices = points;
        mesh.colors = colors;
        mesh.SetIndices(indecies, MeshTopology.Points, 0);
    }
}
