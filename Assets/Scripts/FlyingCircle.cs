using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCircle : MonoBehaviour
{

    float TimeCounter = 0;
    public float speed = 1;
    public float height = 1;
    public float width = 1;

    Vector3 tempPosition;

    // Start is called before the first frame update
    void Start()
    {
        tempPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter += Time.deltaTime*speed;
        tempPosition.x = Mathf.Sin(TimeCounter)*height;
        tempPosition.z = Mathf.Cos(TimeCounter)*width;

        transform.position = tempPosition;
    }
}
