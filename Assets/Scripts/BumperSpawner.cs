using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperSpawner : MonoBehaviour
{
    //Cube prefab
    public GameObject bumper;
    private Color alphaColor;
    private float timeToFade = 1.0f;
    private static float life = 100.0f; 

    /// <summary>
    /// Load on start
    /// </summary>
    public void Start()
    {
        alphaColor = bumper.GetComponent<MeshRenderer>().material.color;
        alphaColor.a = 0;
        bumper.SetActive(false);
    }

    /// <summary>
    /// On Collision Enter a new bumper object will be 
    /// instantiated and attached to the player.
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        bumper.SetActive(true);
        Vector3 contact = col.contacts[0].point;
        Instantiate(bumper, contact, Quaternion.identity);
        
        // Debugging purpose
        Debug.Log("Object in contact on x = " 
            + contact.x + "y = " + contact.y + " z = " + contact.z);
    }

    /// <summary>
    /// OnCollisionExit is called if Collision with object
    /// has ended and the bumper instance gets destroied. 
    /// </summary>
    /// <param name="collisionInfo"></param>
    void OnCollisionExit(Collision collisionInfo)
    {
        bumper.GetComponent<MeshRenderer>().material.color = 
            Color.Lerp(bumper.GetComponent<MeshRenderer>().
            material.color, alphaColor, timeToFade * Time.deltaTime);
        GameObject.Destroy(bumper);
        // Debugging purpose
        Debug.Log("No longer in contact with " + collisionInfo.transform.name);
    }
}
