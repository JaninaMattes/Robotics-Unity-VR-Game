using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarState : MonoBehaviour
{
    // Properties
    public float speed;
    public Material material;
    // sonarOrigin as a Vector4 type will store the 
    // sonar rings original position, which can later be
    // retrieved via a Hit ray cast
   
    private Vector4 sonarOrigin = Vector4.one;

    // Setter and Getter
    public Vector3 SonarOrigin { set { sonarOrigin = new Vector4(value.x, value.y, value.z, 0); } }

    // Update is called once per frame
    void Update()
    {
        // If sonar ring exceedes one it gets reduced 
        // so that colors are not inverted
        // w = is the variable for time
        // if w = 1, the sonar ring will be transparent as it gets reduced to 0
        sonarOrigin.w = Mathf.Min(sonarOrigin.w + (Time.deltaTime * speed), 1);
        material.SetVector("_SonarOrigin", sonarOrigin);
    }
}
