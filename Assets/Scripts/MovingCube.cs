using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Speed of two
        transform.position += Time.deltaTime * transform.forward * 2;
    }
}
