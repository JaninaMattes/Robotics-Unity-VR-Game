using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBelt : MonoBehaviour
{
    public Transform camTransform;

    public float yOffset;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(camTransform.position.x, camTransform.position.y+yOffset, camTransform.position.z);
        Quaternion newRot = Quaternion.Euler(0, camTransform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.time * speed);
        //transform.rotation = Quaternion.Euler(0, camTransform.rotation.eulerAngles.y, 0);
    }
}
