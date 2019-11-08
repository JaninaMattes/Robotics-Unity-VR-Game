using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarSpawner : MonoBehaviour
{
    public SonarState state;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // For debugging
        //if (Input.GetMouseButtonDown(0))
        if(Input.GetKeyDown(KeyCode.X))
        {
            var pos = SensorWorldPos();
            // Takes mouse position and translates it into a ray
            // which then can be projected into 3D space
            var ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Overwrites the position of raycast with the following
                state.SonarOrigin = hit.point;
            }
        }
    }

    Vector3 SensorWorldPos()
    {
        Vector3 pos = OVRManager.tracker.GetPose(0).position;
        Debug.Log($"worldpos X: {pos.x}, Y: {pos.y}, Z: {pos.z}");
        return pos;
    }

    Vector3 OnCollisionEnter(Collision col)
    {
        Vector3 pos = col.transform.position;
        return pos;
    }
}
