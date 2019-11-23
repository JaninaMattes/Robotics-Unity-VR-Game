using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarCreator : MonoBehaviour
{
    private SonarState state;
    private Collision col = null;
    
    // Update is called once per frame
    void Update()
    {
        // For debugging
        if (col != null)
        {
            // Takes collission position and translates it into a ray
            // which then can be projected into 3D space
            var ray = Camera.main.ScreenPointToRay(col.contacts[0].point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Overwrites the position of raycast with the following
                state.SonarOrigin = hit.point;
            }
        }
        else
        {
            Debug.Log("The collision object is empty.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"A collision has been detected and stored in object {col}");
        col = collision; 
    }
}
