using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarState : MonoBehaviour
{
    // Properties
    public float speed;
    public Material material;
    public Vector4 sonarOrigin = Vector4.one;

    // Setter and Getter
    public Vector3 SonarOrigin { set { sonarOrigin = new Vector4(value.x, value.y, value.z, 0); } }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<SonarCreator>().switchSonar)
        {
            sonarOrigin = gameObject.GetComponent<SonarCreator>().firstCollision;
        }
        // If sonar ring exceedes one it gets reduced 
        // so that colors are not inverted
        sonarOrigin.w = Mathf.Min(sonarOrigin.w + (Time.deltaTime * speed), 1);
        material.SetVector("_SonarOrigin", sonarOrigin);
    }
}
