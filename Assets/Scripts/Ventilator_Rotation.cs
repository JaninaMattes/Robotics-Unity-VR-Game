using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilator_Rotation : MonoBehaviour
{
    public float speed = 1;



    // Update is called once per frame
    void Update()
    {
        Vector3 rotAmount = new Vector3 (0,0,speed*Time.deltaTime);

   
        transform.Rotate(rotAmount);
    }
}
