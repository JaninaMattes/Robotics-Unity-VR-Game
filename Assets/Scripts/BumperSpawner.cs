using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperSpawner : MonoBehaviour
{
    //Cube prefab
    public GameObject bumper;
    public GameObject player;
    private Color alphaColor;
    private float timeToFade = 1.0f;

    public void Start()
    {
        alphaColor = bumper.GetComponent<MeshRenderer>().material.color;
        alphaColor.a = 0;
    }
    /// <summary>
    /// On Collision Enter a new bumper object will be 
    /// instantiated and attached to the player.
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        Vector3 contact = col.contacts[0].point;
        Instantiate(bumper, contact, Quaternion.identity);
        Debug.Log("Object in contact on x = " 
            + contact.x + "y = " + contact.y + " z = " + contact.z);
    }

    /// <summary>
    /// OnCollisionExit as soon as the player does move
    /// forward from the collided other object the bumper instance
    /// will be faded out and deleted. 
    /// </summary>
    /// <param name="collisionInfo"></param>
    void OnCollisionExit(Collision collisionInfo)
    {
        bumper.GetComponent<MeshRenderer>().material.color = 
            Color.Lerp(bumper.GetComponent<MeshRenderer>().
            material.color, alphaColor, timeToFade * Time.deltaTime);
        GameObject.Destroy(bumper);
        Debug.Log("No longer in contact with " + collisionInfo.transform.name);
    }
}
